using Ionic.Zip;
using System;
using System.Collections;
using System.ComponentModel;
//新增
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using log4net;
using log4net.Appender;
using log4net.Config;
using CompressTool.Properties;

namespace CompressTool
{

    public partial class CompressTool : Form
    {

        public EmailSender email = new EmailSender();


        // 壓縮完成路徑
        public string target_address;

        // 頁面初始化
        public CompressTool()
        {
            InitializeComponent();
            Closing += new CancelEventHandler(this.CompressTool_Closing); //設定要觸發的事件
            email.Hide();
            txtEncryptPass.UseSystemPasswordChar = true;
            cbSeparateSize.Text = "";
            cbUnit.Text = "KB";

        }
        private static log4net.ILog Log = log4net.LogManager.GetLogger("");

        //開啟寄信頁面
        private void showEmail()
        {
            email.changeAttachPath(target_address);
            email.ShowDialog();
        }

        // 視窗關閉
        private void CompressTool_Closing(object sender, CancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show("確定要關閉程式嗎?",
            "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = false;
                Application.Exit();
            }   
            throw new NotImplementedException();
        }

        //選擇來源檔案
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "请选择文件";
           if (ofd.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string fileName = ofd.SafeFileName;
                string compressFilePath = ofd.FileName;
                txtFilePath.Text = compressFilePath;
                long length = new FileInfo(ofd.FileName).Length;
                txtFolderPath.Text = "";
                radFile.Checked = true;
                radFolder.Checked = false;

                Calculate(length);
                Cursor.Current = Cursors.Default;

            }
        }

        //選擇來源資料夾
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog folder = new CommonOpenFileDialog();
            folder.IsFolderPicker = true;
            if (folder.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string folderPath = folder.FileName;
                txtFolderPath.Text = folderPath;
                Cursor.Current = Cursors.WaitCursor;
                DirectoryInfo di = new DirectoryInfo(folderPath);
                long length = GetDirSize(di);
                txtFilePath.Text = "";
                radFile.Checked = false;
                radFolder.Checked = true;

                Calculate(length);
                Cursor.Current = Cursors.Default;
            }
        }



        //計算資料夾大小
        public static long GetDirSize(DirectoryInfo d)
        {
            long Size = 0;
            try
            {
                // Add file sizes.
                FileInfo[] fis = d.GetFiles();
                foreach (FileInfo fi in fis)
                {
                    Size += fi.Length;
                }
                // Add subdirectory sizes.
                DirectoryInfo[] dis = d.GetDirectories();
                foreach (DirectoryInfo di in dis)
                {
                    Size += GetDirSize(di);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(),"Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            return (Size);
        }

        //計算檔案大小
        public void Calculate(long length)
        {
            //先換算成KB
            double size = Math.Round(length / (double)1024, 2);

            if (size > 1024)
            {
                size = Math.Round(size / (double)1024, 2);
                if (size > 1024)
                {
                    size = Math.Round(size / (double)1024, 2);
                    textFileSize.Text = size.ToString();
                    textUnit.Text = "GB";
                }
                else
                {
                    textFileSize.Text = size.ToString();
                    textUnit.Text = "MB";
                }
            }
            else
            {
                textFileSize.Text = size.ToString();
                textUnit.Text = "KB";
            }
        }



        //讀取目錄下所有檔案
        private static ArrayList GetFiles(string path)
        {
            ArrayList files = new ArrayList();

            if (Directory.Exists(path))
            {
                files.AddRange(Directory.GetFiles(path));
            }

            return files;
        }

        //單選按鈕變換事件
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //設定文字框顯示字串and計算檔案大小
            if(radFile.Checked)
            {
                if(String.IsNullOrEmpty(txtFilePath.Text))
                {
                    textFileSize.Text = "";
                }
                else
                {
                    string path = txtFilePath.Text;
                    long length = new FileInfo(path).Length;
                    Calculate(length);
                }
            }
            else
            {
                if (String.IsNullOrEmpty(txtFolderPath.Text))
                {
                    textFileSize.Text = "";
                }
                else
                {
                    string folderPath = txtFolderPath.Text;
                    DirectoryInfo di = new DirectoryInfo(folderPath);
                    Cursor.Current = Cursors.WaitCursor;
                    long length = GetDirSize(di);
                    Calculate(length);
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        //確認資料後開始壓縮
        private void Compress_Click(object sender, EventArgs e)
        {

            string sourceFilePath = txtFilePath.Text;
            string sourceFolderPath = txtFolderPath.Text;
            string password = txtEncryptPass.Text;
            bool hasFile = !string.IsNullOrEmpty(sourceFilePath);
            bool hasFolder = !string.IsNullOrEmpty(sourceFolderPath);

            try
            {
                //判斷是否有空欄位
                if (radFile.Checked == true)
                {
                    string targetPath = Path.GetDirectoryName(sourceFilePath);
                    string name = Path.GetFileNameWithoutExtension(sourceFilePath);
                    if (hasFile)
                    {
                        ZipFiles("File", sourceFilePath, targetPath, password, string.Empty);
                    }
                    else
                    {
                        MessageBox.Show("請選擇來源路徑.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFilePath.Focus();
                        return;
                    }
                }
                if (radFolder.Checked)
                {
                    string targetPath = Path.GetDirectoryName(sourceFolderPath);
                    if (hasFolder)
                    {
                        ZipFiles("Folder", sourceFolderPath, targetPath, password, string.Empty);
                    }
                    else
                    {
                        MessageBox.Show("請選擇來源路徑.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFolderPath.Focus();
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Info(" info ");
                MessageBox.Show("請選擇資料來源路徑 !", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Info(" info ");
                return;
            }
        }

        //壓縮功能
        //sourcePath: 來源路徑，targetPath: 存檔路徑，password: 密碼，comment: 註解或壓縮等級
        private void ZipFiles(string type,string sourcePath ,string targetPath, string password, string comment)
        {
            string fragment = cbSeparateSize.Text;
            bool isSeperate = !String.IsNullOrEmpty(fragment);
            bool hasPassword = !String.IsNullOrEmpty(password);
            int sepSize = 0;
            int maxsize = 2 *1024 * 1024 * 1023;
            int minsize = 65536;

            //判斷是否切割，單位為byte
            if (isSeperate)
            {
                int size = 0;

                try
                {
                    size = int.Parse(fragment);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(),"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cbUnit.Text == "KB")
                {
                    sepSize = size * 1024;
                }
                else
                {
                    sepSize = size * 1024 * 1024;
                }

                //分片大小只接受63KB~2GB
                if (sepSize > maxsize || sepSize < minsize)
                {
                    MessageBox.Show("請輸入64KB~2GB的數值!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbSeparateSize.Focus();
                    return;
                }
            }
            try
            {
                //判斷資料來源&開始壓縮
                if (type == "Folder")
                {
                    string name = Path.GetFileName(sourcePath);
                    string newPath = targetPath + @"\" + name;
                    CheckDirectory(newPath);
                    string zipPath = newPath + "_壓縮檔" + @"\" + name + ".zip";

                    Thread thread = new Thread(t =>
                    {
                        using (ZipFile zip = new ZipFile(Encoding.UTF8))
                        {
                            if (isSeperate)
                            {
                                zip.MaxOutputSegmentSize = sepSize;
                            }
                            if (hasPassword)
                            {
                                zip.Password = password;
                            }
                            zip.AddDirectory(sourcePath);
                            zip.SaveProgress += Zip_saveProgress;
                            zip.Save(zipPath);
                            MessageBox.Show("Success!", "Message");
                        }
                    });
                    thread.IsBackground = true;
                    thread.Start();
                }

                if (type == "File")
                {
                    string name = Path.GetFileNameWithoutExtension(sourcePath);
                    string newPath = targetPath + @"\" + name;
                    CheckDirectory(newPath);
                    string zipPath = newPath + "_壓縮檔" + @"\" + name + ".zip";

                    Thread thread = new Thread(t =>
                    {
                        using (ZipFile zip = new ZipFile(Encoding.UTF8))
                        {                                                    
                            if (isSeperate)
                            {
                                zip.MaxOutputSegmentSize = sepSize;
                            }
                            if (hasPassword)
                            {
                                zip.Password = password;
                            }
                            zip.AddFile(sourcePath, string.Empty);
                            zip.SaveProgress += Zip_saveFileProgress;
                            zip.Save(zipPath);
                            MessageBox.Show("Success!", "Message");
                        }
                    });
                    thread.IsBackground = true;
                    thread.Start();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Exception", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
        }


        #region 進度條設定
        public void Zip_saveProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry)


                progressBar.Invoke(new MethodInvoker(delegate
                {

                    //progressBar.Maximum = e.EntriesTotal;
                    //progressBar.Value = e.EntriesSaved + 1;
                    progressBar.Maximum = 100;
                    progressBar.Value = (int)((e.EntriesSaved + 1)*100 / e.EntriesTotal);
                    status.Text = progressBar.Value.ToString();
                    progressBar.Update();
                    
                }));
        }
        //計算進度百分比
        public void Zip_saveFileProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_EntryBytesRead)

                progressBar.Invoke(new MethodInvoker(delegate
                {
                    progressBar.Maximum = 100;
                    progressBar.Value = (int)((e.BytesTransferred * 100) / e.TotalBytesToTransfer);
                    status.Text = progressBar.Value.ToString();
                    progressBar.Update();                   
                }));
        }
        #endregion

        //建立目錄
        private void CheckDirectory(string path)
        {
            string newDir = path + "_壓縮檔";
            if (Directory.Exists(newDir))
            {
                DirectoryInfo di = new DirectoryInfo(newDir);
                ClearReadOnly(di);
                di.Delete(true);
            }                     
            Directory.CreateDirectory(newDir);
            target_address = newDir;
        }

        // 清除唯讀屬性
        private void ClearReadOnly(DirectoryInfo parentDirectory)
        {
            if (parentDirectory != null)
            {
                parentDirectory.Attributes = FileAttributes.Normal;
                foreach (FileInfo fi in parentDirectory.GetFiles())
                {
                    fi.Attributes = FileAttributes.Normal;
                }
                foreach (DirectoryInfo di in parentDirectory.GetDirectories())
                {
                    ClearReadOnly(di);
                }
            }
        }




        //開啟寄信頁面EmailSender
        public void btnMail_Click(object sender, EventArgs e)
        {
            showEmail();
        }


        //顯示密碼
        private void showPass_CheckedChanged(object sender, EventArgs e)
        {
            if (showPass.Checked)
            {
                txtEncryptPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtEncryptPass.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string myPath = @"";
            if (radFolder.Checked && !string.IsNullOrEmpty(txtFolderPath.Text))
            {
                myPath += txtFolderPath.Text;
            }
            if (radFile.Checked && !string.IsNullOrEmpty(txtFilePath.Text))
            {
                myPath += Path.GetDirectoryName(txtFilePath.Text);
            }

            if (!string.IsNullOrEmpty(myPath))
            {
                System.Diagnostics.Process prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = myPath;
                prc.Start();
            }
            else
            {
                MessageBox.Show("空白的路徑", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 記錄寄件資料
        private void CompressTool_FormClosed(object sender, FormClosedEventArgs e)
        {

            Settings.Default.accountCache = email.accountCache;
            Settings.Default.receiverNameCache = email.receiverNameCache;
            Settings.Default.receiverHostCache = email.receiverHostCache;
            Settings.Default.Save();
        }
    }
}
