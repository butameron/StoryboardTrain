namespace StoryboardImportTest
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.startImportButton = new System.Windows.Forms.Button();
            this.importFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.outputDirectory = new System.Windows.Forms.TextBox();
            this.startShotNumber = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.sceneNumber = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.startShotNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sceneNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // startImportButton
            // 
            this.startImportButton.Location = new System.Drawing.Point(604, 302);
            this.startImportButton.Name = "startImportButton";
            this.startImportButton.Size = new System.Drawing.Size(245, 103);
            this.startImportButton.TabIndex = 0;
            this.startImportButton.Text = "インポート開始";
            this.startImportButton.UseVisualStyleBackColor = true;
            this.startImportButton.Click += new System.EventHandler(this.startImportButton_Click);
            // 
            // importFileName
            // 
            this.importFileName.Location = new System.Drawing.Point(174, 85);
            this.importFileName.Name = "importFileName";
            this.importFileName.Size = new System.Drawing.Size(603, 28);
            this.importFileName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "インポートPSD";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "出力先";
            // 
            // outputDirectory
            // 
            this.outputDirectory.Location = new System.Drawing.Point(174, 143);
            this.outputDirectory.Name = "outputDirectory";
            this.outputDirectory.Size = new System.Drawing.Size(603, 28);
            this.outputDirectory.TabIndex = 1;
            // 
            // startShotNumber
            // 
            this.startShotNumber.Location = new System.Drawing.Point(174, 242);
            this.startShotNumber.Name = "startShotNumber";
            this.startShotNumber.Size = new System.Drawing.Size(120, 28);
            this.startShotNumber.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "開始カット番号";
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(794, 80);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(89, 36);
            this.openFileButton.TabIndex = 4;
            this.openFileButton.Text = "参照";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "PSDファイル|*.psd";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "シーン番号";
            // 
            // sceneNumber
            // 
            this.sceneNumber.Location = new System.Drawing.Point(174, 199);
            this.sceneNumber.Name = "sceneNumber";
            this.sceneNumber.Size = new System.Drawing.Size(120, 28);
            this.sceneNumber.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 487);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.sceneNumber);
            this.Controls.Add(this.startShotNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputDirectory);
            this.Controls.Add(this.importFileName);
            this.Controls.Add(this.startImportButton);
            this.Name = "MainForm";
            this.Text = "StoryboardImportTest";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.startShotNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sceneNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startImportButton;
        private System.Windows.Forms.TextBox importFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox outputDirectory;
        private System.Windows.Forms.NumericUpDown startShotNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown sceneNumber;
    }
}

