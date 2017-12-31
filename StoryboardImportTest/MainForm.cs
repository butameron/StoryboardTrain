using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PhotoshopFile;
using StoryboardTrain.Core;

namespace StoryboardImportTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DataBind(openFileDialog.FileName);
            }
        }

        private void DataBind(string fileName)
        {
            if (fileName is null)
            {
                return;
            }

            importFileName.Text = fileName;
            outputDirectory.Text = Path.GetDirectoryName(fileName);


        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            DataBind(fileNames.FirstOrDefault());

        }


        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) { 
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void startImportButton_Click(object sender, EventArgs e)
        {
            const int rowHight = 1380;

            var context = new LoadContext();
            var inputPsd = new PsdFile(importFileName.Text, context);

            if (inputPsd.ColumnCount != 2220 || inputPsd.RowCount % rowHight != 0)
            {
                MessageBox.Show("エラー（仮）", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var cropper = new PsdCropper(inputPsd);

            var rowCount = inputPsd.RowCount / rowHight;
            for(int i = 0; i < rowCount; i++)
            {
                var croppedPsd = cropper.GenerateCroppedPsd(new Rectangle(0, rowHight * i, 2220, rowHight));

                var shotNumber = startShotNumber.Value + i;
                var outputPsdFileName = Path.Combine(outputDirectory.Text, string.Format("C{0:00}.psd", shotNumber));
                var outputMarkdownFileName = Path.Combine(outputDirectory.Text, string.Format("C{0:00}.md", shotNumber));

                if (File.Exists(outputPsdFileName))
                {
                    MessageBox.Show($"{outputPsdFileName}が既に存在するため中断しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (File.Exists(outputMarkdownFileName))
                {
                    MessageBox.Show($"{outputMarkdownFileName}が既に存在するため中断しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                croppedPsd.Save(outputPsdFileName, Encoding.UTF8);


                var sceneNumberText = sceneNumber.Value;

                var markdown = $@"---
layout: storyboard-cut
scene: S{sceneNumber.Value:00}
cut: C{shotNumber:00}
length: 
jira_cut: 
---
<section class='action' markdown='1'>
アクション
===


</section>

<section class='dialogue' markdown='1'>
セリフ
===

</section>";
                File.WriteAllText(outputMarkdownFileName, markdown);
            }


        }
    }
}
