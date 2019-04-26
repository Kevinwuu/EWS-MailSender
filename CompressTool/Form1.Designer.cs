using System;
using System.ComponentModel;

namespace CompressTool
{
    partial class CompressTool
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.cbUnit = new System.Windows.Forms.ComboBox();
            this.Compress = new System.Windows.Forms.Button();
            this.btnMail = new System.Windows.Forms.Button();
            this.cbSeparateSize = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.radFolder = new System.Windows.Forms.RadioButton();
            this.textFileSize = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.radFile = new System.Windows.Forms.RadioButton();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.textUnit = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.showPass = new System.Windows.Forms.CheckBox();
            this.txtEncryptPass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(93, 324);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(212, 20);
            this.label7.TabIndex = 23;
            this.label7.Text = "(只接受64KB~2GB的輸入值)";
            // 
            // cbUnit
            // 
            this.cbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUnit.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbUnit.FormattingEnabled = true;
            this.cbUnit.Items.AddRange(new object[] {
            "KB",
            "MB"});
            this.cbUnit.Location = new System.Drawing.Point(96, 282);
            this.cbUnit.Name = "cbUnit";
            this.cbUnit.Size = new System.Drawing.Size(119, 24);
            this.cbUnit.TabIndex = 20;
            // 
            // Compress
            // 
            this.Compress.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Compress.Location = new System.Drawing.Point(276, 431);
            this.Compress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Compress.Name = "Compress";
            this.Compress.Size = new System.Drawing.Size(82, 33);
            this.Compress.TabIndex = 14;
            this.Compress.Text = "壓縮";
            this.Compress.UseVisualStyleBackColor = true;
            this.Compress.Click += new System.EventHandler(this.Compress_Click);
            // 
            // btnMail
            // 
            this.btnMail.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnMail.Location = new System.Drawing.Point(242, 519);
            this.btnMail.Name = "btnMail";
            this.btnMail.Size = new System.Drawing.Size(145, 29);
            this.btnMail.TabIndex = 15;
            this.btnMail.Text = "填寫信箱資料";
            this.btnMail.UseVisualStyleBackColor = true;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // cbSeparateSize
            // 
            this.cbSeparateSize.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbSeparateSize.FormattingEnabled = true;
            this.cbSeparateSize.Items.AddRange(new object[] {
            "5",
            "10",
            "14"});
            this.cbSeparateSize.Location = new System.Drawing.Point(95, 251);
            this.cbSeparateSize.Name = "cbSeparateSize";
            this.cbSeparateSize.Size = new System.Drawing.Size(120, 25);
            this.cbSeparateSize.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(71, 174);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "Step2: 分割壓縮檔(非必要)";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSelectFolder.Location = new System.Drawing.Point(525, 98);
            this.btnSelectFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(36, 25);
            this.btnSelectFolder.TabIndex = 4;
            this.btnSelectFolder.Text = "...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtFolderPath.Location = new System.Drawing.Point(189, 98);
            this.txtFolderPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.ReadOnly = true;
            this.txtFolderPath.Size = new System.Drawing.Size(321, 25);
            this.txtFolderPath.TabIndex = 7;
            // 
            // radFolder
            // 
            this.radFolder.AutoSize = true;
            this.radFolder.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radFolder.Location = new System.Drawing.Point(94, 100);
            this.radFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radFolder.Name = "radFolder";
            this.radFolder.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radFolder.Size = new System.Drawing.Size(74, 20);
            this.radFolder.TabIndex = 6;
            this.radFolder.Text = "資料夾";
            this.radFolder.UseVisualStyleBackColor = true;
            // 
            // textFileSize
            // 
            this.textFileSize.AutoSize = true;
            this.textFileSize.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textFileSize.Location = new System.Drawing.Point(461, 141);
            this.textFileSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.textFileSize.Name = "textFileSize";
            this.textFileSize.Size = new System.Drawing.Size(63, 16);
            this.textFileSize.TabIndex = 9;
            this.textFileSize.Text = "           ";
            this.textFileSize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(417, 138);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "大小";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtFilePath.Location = new System.Drawing.Point(189, 60);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.ShortcutsEnabled = false;
            this.txtFilePath.Size = new System.Drawing.Size(321, 25);
            this.txtFilePath.TabIndex = 1;
            // 
            // radFile
            // 
            this.radFile.AutoSize = true;
            this.radFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radFile.Checked = true;
            this.radFile.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radFile.Location = new System.Drawing.Point(94, 63);
            this.radFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radFile.Name = "radFile";
            this.radFile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radFile.Size = new System.Drawing.Size(75, 24);
            this.radFile.TabIndex = 5;
            this.radFile.TabStop = true;
            this.radFile.Text = "    檔案";
            this.radFile.UseVisualStyleBackColor = true;
            this.radFile.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSelectFile.Location = new System.Drawing.Point(526, 61);
            this.btnSelectFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(36, 27);
            this.btnSelectFile.TabIndex = 0;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // textUnit
            // 
            this.textUnit.AutoSize = true;
            this.textUnit.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textUnit.Location = new System.Drawing.Point(534, 138);
            this.textUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.textUnit.Name = "textUnit";
            this.textUnit.Size = new System.Drawing.Size(27, 19);
            this.textUnit.TabIndex = 9;
            this.textUnit.Text = "KB";
            this.textUnit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(94, 379);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(473, 29);
            this.progressBar.TabIndex = 24;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.showPass);
            this.groupBox1.Controls.Add(this.txtEncryptPass);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(342, 216);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(225, 139);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "加密";
            // 
            // showPass
            // 
            this.showPass.AutoSize = true;
            this.showPass.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.showPass.Location = new System.Drawing.Point(26, 94);
            this.showPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.showPass.Name = "showPass";
            this.showPass.Size = new System.Drawing.Size(88, 23);
            this.showPass.TabIndex = 2;
            this.showPass.Text = "顯示密碼";
            this.showPass.UseVisualStyleBackColor = true;
            this.showPass.CheckedChanged += new System.EventHandler(this.showPass_CheckedChanged);
            // 
            // txtEncryptPass
            // 
            this.txtEncryptPass.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtEncryptPass.Location = new System.Drawing.Point(26, 61);
            this.txtEncryptPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEncryptPass.Name = "txtEncryptPass";
            this.txtEncryptPass.Size = new System.Drawing.Size(181, 25);
            this.txtEncryptPass.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(22, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "輸入密碼 :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(71, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(191, 24);
            this.label5.TabIndex = 27;
            this.label5.Text = "Step1 : 選取資料來源";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(71, 469);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 24);
            this.label3.TabIndex = 28;
            this.label3.Text = "Step3: 開始寄信";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(92, 221);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 19);
            this.label6.TabIndex = 29;
            this.label6.Text = "分割大小 :";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(489, 411);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(21, 19);
            this.status.TabIndex = 30;
            this.status.Text = "   ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Location = new System.Drawing.Point(539, 411);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 19);
            this.label8.TabIndex = 31;
            this.label8.Text = "%";
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(0, 0);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFolder.TabIndex = 32;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(391, 431);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 33;
            this.button1.Text = "檢視";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CompressTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 564);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.status);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.textUnit);
            this.Controls.Add(this.textFileSize);
            this.Controls.Add(this.btnMail);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.Compress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.cbSeparateSize);
            this.Controls.Add(this.cbUnit);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.radFolder);
            this.Controls.Add(this.radFile);
            this.Controls.Add(this.btnSelectFolder);
            this.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CompressTool";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CompressTool_FormClosed);
            this.Load += new System.EventHandler(this.CompressTool_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbUnit;
        private System.Windows.Forms.Button Compress;
        private System.Windows.Forms.Button btnMail;
        private System.Windows.Forms.ComboBox cbSeparateSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.RadioButton radFolder;
        private System.Windows.Forms.Label textFileSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.RadioButton radFile;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label textUnit;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox showPass;
        private System.Windows.Forms.TextBox txtEncryptPass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button button1;
    }
}

