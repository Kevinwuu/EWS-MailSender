﻿using Ionic.Zip;
using System;
using System.Collections;
using System.ComponentModel;
//新增
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CompressTool
{
    public partial class CompressTool : Form
    {

        public EmailSender email = new EmailSender();
        public string target_address;
        ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        ManualResetEvent _pauseEvent = new ManualResetEvent(true);
        public delegate void MyInvoke(string str);

        // 頁面初始化
        public CompressTool()
        {
            InitializeComponent();
            Closing += new CancelEventHandler(this.CompressTool_Closing); //設定要觸發的事件
            email.Hide();
            txtCompressPass.UseSystemPasswordChar = true;
            cbData.Text = "";
            cbData1.Text = "KB";

        }

        //開啟寄信頁面
        private void showEmail()
        {
            email.changeText(target_address);
            email.ShowDialog();
        }

        private void CompressTool_Closing(object sender, CancelEventArgs e)
        {
            DialogResult dr = MessageBox.Show("確定要關閉程式嗎?",
            "Closing event!", MessageBoxButtons.YesNo);
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
                textFile.Text = compressFilePath;
                long length = new FileInfo(ofd.FileName).Length;

                Calculate(length);
                Cursor.Current = Cursors.Default;

            }
        }

        //選擇來源資料夾
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {

                string folderPath = folder.SelectedPath;
                textFolder.Text = folderPath;
                Cursor.Current = Cursors.WaitCursor;
                DirectoryInfo di = new DirectoryInfo(folderPath);
                long length = GetDirSize(di);
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
            if(radioButton1.Checked)
            {
                if(String.IsNullOrEmpty(textFile.Text))
                {
                    textFileSize.Text = "";
                }
                else
                {
                    string path = textFile.Text;
                    long length = new FileInfo(path).Length;
                    Calculate(length);
                }
            }
            else
            {
                if (String.IsNullOrEmpty(textFolder.Text))
                {
                    textFileSize.Text = "";
                }
                else
                {
                    string folderPath = textFolder.Text;
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

            string sourceFilePath = textFile.Text;
            string sourceFolderPath = textFolder.Text;
            string password = txtCompressPass.Text;

            bool hasFile = !string.IsNullOrEmpty(sourceFilePath);
            bool hasFolder = !string.IsNullOrEmpty(sourceFolderPath);



            //判斷是否有空欄位
            if (radioButton1.Checked)
            {
                string targetPath = Path.GetDirectoryName(sourceFilePath);
                string name = Path.GetFileNameWithoutExtension(sourceFilePath);
                //target_address = targetPath + @"\" + name;
                if (!hasFile)
                {
                    MessageBox.Show("請選擇來源路徑.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textFile.Focus();
                    return;
                }
                else
                {
                    ZipFiles("File", sourceFilePath, targetPath, password, string.Empty);
                }
            }
            else
            {
                string targetPath = Path.GetDirectoryName(sourceFolderPath);
                //target_address = sourceFolderPath;
                if (!hasFolder)
                {
                    MessageBox.Show("請選擇來源路徑.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textFolder.Focus();
                    return;
                }
                else
                {
                    ZipFiles("Folder", sourceFolderPath, targetPath, password, string.Empty);
                }
            }
        }

        //壓縮功能
        //sourcePath: 來源路徑，targetPath: 存檔路徑，password: 密碼，comment: 註解或壓縮等級
        private void ZipFiles(string type,string sourcePath ,string targetPath, string password, string comment)
        {
            string fragment = cbData.Text;
            bool isSeperate = !String.IsNullOrEmpty(fragment);
            bool hasPassword = !String.IsNullOrEmpty(password);
            int sepSize = 0;
            int maxsize = 2 *1024 * 1024 * 1023;
            int minsize = 65536;
            //btnPause.Visible = true;
            //btnResume.Visible = true;

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


                if (cbData1.Text == "KB")
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
                    cbData.Focus();
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
                        MyInvoke mi = new MyInvoke(UpdateStatus);
                        this.BeginInvoke(mi, new Object[] { "OVER" });
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
                            //txtLoading.Text = " ";
                        }
                    });
                    thread.IsBackground = true;
                    thread.Start();
                    //txtLoading.Text = "Loading";
                }

                if (type == "File")
                {
                    string name = Path.GetFileNameWithoutExtension(sourcePath);
                    string newPath = targetPath + @"\" + name;
                    CheckDirectory(newPath);
                    string zipPath = newPath + "_壓縮檔" + @"\" + name + ".zip";


                    Thread thread = new Thread(t =>
                    {
                        //MyInvoke mi = new MyInvoke(UpdateStatus);
                        //this.BeginInvoke(mi, new Object[] { "OVER" });
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

                            //zip.UpdateFile(zipPath);


                            zip.Save(zipPath);



                            MessageBox.Show("Success!", "Message");
                        }
                    });

                    thread.IsBackground = true;
                    thread.Start();
                    //txtLoading.Text = "Loading";
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

                    // 初始化為true, stop時轉為false
                    _pauseEvent.WaitOne(Timeout.Infinite);

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
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true);
                }

                catch (IOException e)
                {
                    MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

        }

                   

        

            Directory.CreateDirectory(path + "_壓縮檔");

            target_address = path + "_壓縮檔";
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
                txtCompressPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtCompressPass.UseSystemPasswordChar = true;
            }
        }


        private void btnPause_Click(object sender, EventArgs e)
        {
            //    _pauseEvent.Reset();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            //    MessageBox.Show("Hello", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    _pauseEvent.Set();
        }


        public void Stop()
        {
            //    // Signal the shutdown event
            //    _shutdownEvent.Set();
            //    Console.WriteLine("Thread Stopped ");

            //    // Make sure to resume any paused threads
            //    _pauseEvent.Set();

            //    // Wait for the thread to exit
            //    _thread.Join();
        }

    public void UpdateStatus(string param1)
        {
            //this.txtLoading.Text = param1;
        }
    }
}
