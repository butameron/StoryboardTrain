using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoshopFile;
using System.Drawing;
using System.Drawing.Imaging;

namespace StoryboardTrain.Core
{
    public static class PsdFileExtension
    {
        public static Bitmap ToBitmap(this PsdFile psd)
        {
            return psd.BaseLayer.ToBitmap();
        }

        public unsafe static Bitmap ToBitmap(this Layer layer)
        {
            if (layer.Channels.Count != 4
                || layer.PsdFile.BitDepth != 8
                || layer.PsdFile.ColorMode != PsdColorMode.RGB)
            {
                throw new NotSupportedException("未対応のPSD形式です。8bit ARGBのPSDを使用してください。");
            }

            var outputBitmap = new Bitmap(layer.Rect.Width, layer.Rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Imaging.BitmapData outputBitmapData = null;

            try
            {

                outputBitmapData = outputBitmap.LockBits(new Rectangle(Point.Empty, outputBitmap.Size), ImageLockMode.ReadWrite, outputBitmap.PixelFormat);
                
                const int pixelDataLength = 4;

                int pixelCount = layer.Rect.Width * layer.Rect.Height;

                byte* outputPtrStart = (byte*)outputBitmapData.Scan0;
                byte*[] outputPtrs = {
                            outputPtrStart + 3, //A
                            outputPtrStart + 2, //R
                            outputPtrStart + 1, //G
                            outputPtrStart + 0  //B
                };


                fixed (
                    byte* inputPtrA = layer.Channels.SingleOrDefault(x => x.ID == -1).ImageData,
                          inputPtrR = layer.Channels.SingleOrDefault(x => x.ID == 0).ImageData,
                          inputPtrG = layer.Channels.SingleOrDefault(x => x.ID == 1).ImageData,
                          inputPtrB = layer.Channels.SingleOrDefault(x => x.ID == 2).ImageData
                    )
                {
                    byte*[] inputPtrs = { inputPtrA, inputPtrR, inputPtrG, inputPtrB };

                    Parallel.For(0, 4, i =>
                    {
                        byte* inputPtr = inputPtrs[i];
                        byte* inputPtrEnd = inputPtr + pixelCount - 1;
                        byte* outputPtr = outputPtrs[i];

                        while (inputPtr <= inputPtrEnd)
                        {
                            *outputPtr = *inputPtr;
                            inputPtr++;
                            outputPtr += pixelDataLength;
                        }
                    });

                }
                    
            }
            finally
            {
                if (outputBitmapData != null) { outputBitmap.UnlockBits(outputBitmapData); }

            }
            return outputBitmap;
        }

    }
}
