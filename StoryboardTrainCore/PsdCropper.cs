using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoshopFile;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace StoryboardTrain.Core
{
    public class PsdCropper
    {

        public PsdFile BasePsd { get; set; }

        public PsdCropper(PsdFile basePsd)
        {
            this.BasePsd = basePsd;
        }

        /// <summary>
        /// 指定した位置とサイズで切り抜いたPSDを生成する
        /// </summary>
        /// <param name="cropRect">切り抜く位置とサイズ</param>
        /// <returns>切り抜かれたPSDファイル</returns>
        public PsdFile GenerateCroppedPsd(Rectangle cropRect)
        {
            var newPsd = ClonePsdFile(BasePsd);

            foreach (var layer in newPsd.Layers)
            {
                CropLayer(layer, cropRect);
            }

            CropLayer(newPsd.BaseLayer, cropRect);

            newPsd.ColumnCount = cropRect.Width;
            newPsd.RowCount = cropRect.Height;

            newPsd.PrepareSave();

            return newPsd;
        }


        /// <summary>
        /// PsdFileオブジェクトを複製する
        /// </summary>
        /// <param name="psd">複製元</param>
        /// <returns></returns>
        private static PsdFile ClonePsdFile(PsdFile psd)
        {

            using (MemoryStream ms = new MemoryStream()) {
                psd.PrepareSave();
                psd.Save(ms, Encoding.UTF8);

                ms.Seek(0, SeekOrigin.Begin);
                var newPsd = new PsdFile(ms, new LoadContext());
                
                return newPsd;

            }
        }


        /// <summary>
        /// レイヤーを指定した位置とサイズで切り抜く
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="cropRect">画像全体の左上を基準とした、切り出す位置とサイズ</param>
        private static void CropLayer(Layer layer, Rectangle cropRect)
        {
            var originalRect = layer.Rect;

            var translatedRect = new Rectangle(originalRect.X - cropRect.X, originalRect.Y - cropRect.Y, originalRect.Width, originalRect.Height);


            if (layer.AdditionalInfo.OfType<LayerSectionInfo>().Any())
            {
                //レイヤーグループ（レイヤーセット）の場合は処理しない
                return;
            }
            else if (Rectangle.Intersect(cropRect, originalRect) == Rectangle.Empty)
            {
                // 完全にはみ出している場合
                foreach (var c in layer.Channels)
                {
                    CropChannel(c, originalRect.Size, Rectangle.Empty);
                }
                layer.Rect = Rectangle.Empty;
            }
            else
            {
                // 一部がはみ出している場合

                // はみ出し部分を計算する
                int overflowLeft = Math.Max(0 - translatedRect.X, 0);
                int overflowRight = Math.Max(translatedRect.X + translatedRect.Width - cropRect.Width, 0);
                int overflowTop = Math.Max(0 - translatedRect.Y, 0);
                int overflowBottom = Math.Max(translatedRect.Y + translatedRect.Height - cropRect.Height, 0);

                // はみ出し部分を削った場合の位置とサイズを求める
                var cropLayerRect = new Rectangle(
                        overflowLeft,
                        overflowTop,
                        translatedRect.Width - overflowLeft - overflowRight,
                        translatedRect.Height - overflowTop - overflowBottom
                    );

                // はみ出し部分のデータを削除する
                foreach (var c in layer.Channels)
                {
                    CropChannel(c, originalRect.Size, cropLayerRect);
                }

                // はみ出し部分の削除後の位置補正
                var newRect = new Rectangle(translatedRect.X + cropLayerRect.X, translatedRect.Y + cropLayerRect.Y, cropLayerRect.Width, cropLayerRect.Height);
                layer.Rect = newRect;

            }


            if (layer.Rect.Size == Size.Empty)
            {
                //空のレイヤーか、完全にはみ出したレイヤーの場合

                // CLIP STUDIO (Version 1.7.2)との相性問題の暫定的な回避策
                // サイズゼロのレイヤーを出力すると、それより前面のレイヤーで正常に表示されない（空白になる等）不具合が発生するため、
                // 暫定的な回避策として、1x1ピクセルの透明ピクセルを含むレイヤーを出力する。
                layer.Rect = new Rectangle(-1, -1, 1, 1);
                foreach (var c in layer.Channels)
                {
                    c.ImageData = new byte[] { 0 };
                    c.ImageDataRaw = null;

                }
            }
        }

        /// <summary>
        /// チャンネルデータを指定したサイズで切り抜く
        /// </summary>
        /// <param name="c">チャンネル</param>
        /// <param name="originalLayerSize">変更前のレイヤーサイズ</param>
        /// <param name="croppingRect">レイヤーデータの左上を基準とした、切り出す位置とサイズ</param>
        private static void CropChannel(Channel c, Size originalLayerSize, Rectangle croppingRect)
        {
            if (croppingRect.Size == originalLayerSize)
            {
                return;
            }

            if (croppingRect.Width == 0 || croppingRect.Height == 0)
            {
                c.ImageData = new byte[0];
                return;
            }

            if (croppingRect.Width > originalLayerSize.Width || croppingRect.Height > originalLayerSize.Height)
            {
                throw new InvalidOperationException();
            }


            byte[] originalData = c.ImageData;

            int newDataLength = croppingRect.Width * croppingRect.Height;
            byte[] newData = new byte[newDataLength];

            int originalDataIndex = croppingRect.Y * originalLayerSize.Width + croppingRect.X;
            int newDataIndex = 0;

            while (newDataIndex < newDataLength)
            {

                Array.Copy(originalData, originalDataIndex, newData, newDataIndex, croppingRect.Width);

                originalDataIndex += originalLayerSize.Width;
                newDataIndex += croppingRect.Width;
            }

            c.ImageData = newData;
            c.ImageDataRaw = null; //圧縮処理を再実行させるためにnullを設定
        }

    }
}
