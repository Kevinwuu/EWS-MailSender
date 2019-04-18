using Microsoft.Exchange.WebServices.Data;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace CompressTool
{
    public partial class EmailSender : Form
    {
        public delegate void MyInvoke(string str1, string str2);
        public EmailSender()
        {           
            InitializeComponent();
            txtAccount.Focus();
        }

        public string testString;


        public void changeText(string str)
        {
            textFolder.Text = str;
        }

        #region 送出信件
        private void btnsend_Click(object sender, EventArgs e)
        {
            txtFinished.Text = "";
            txtSendStatus.Text = "";
            Cursor.Current = Cursors.WaitCursor;


            string user = txtAccount.Text;
            string pass = txtPass.Text;
            string senderEmail = txtSender.Text + txtSenderHoset.Text;
            string reciever = txtReciever.Text + txtRecieverHost.Text;
            string body = txtBody.Text;
            string subject = txtSubject.Text;

            try 
            {
                // Setting Credential 
                ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
                service.Credentials = new NetworkCredential(user, pass);
                service.AutodiscoverUrl(senderEmail);

                EmailMessage message = new EmailMessage(service);

                // 加入屬性
                message.Subject = subject;
                message.Body = body;
                message.ToRecipients.Add(reciever);

                //判斷是否有空白輸入
                foreach (Control cur in Controls)           
                {
                    if (cur is TextBox && cur.Text == string.Empty)            
                    {
                        if (cur.Name != "txtBody" && cur.Name != "textFolder")
                        {
                            MessageBox.Show("請檢查是否有資料遺漏", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                // 判斷是否有附加檔案
                if (textFolder.Text != "")
                {
                    string[] files = Directory.GetFiles(textFolder.Text, "*");

                    for (int i = 0; i < files.Length; i++)
                    {

                        message.Attachments.AddFileAttachment(files[i]);

                        // 改變主旨編號
                        message.Subject = subject + $"({i+1})";
                        
                        // 即時刷新頁面
                        this.Refresh();

                        message.SendAndSaveCopy();
                        UpdateSendStatus(i, files.Length);

                        // Free the memory and continue
                        message = null;
                        message = new EmailMessage(service);
                        message.Body = body;
                        message.ToRecipients.Add(reciever);
                    }
                }
                else
                {
                    message.SendAndSaveCopy();
                }
                txtFinished.Text = "傳送完畢";
                MessageBox.Show("已寄出!");
                Cursor.Current = Cursors.Default;
                // Free the memory
                message = null;
            } 
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }
        }
        #endregion

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog folder = new CommonOpenFileDialog();
            folder.IsFolderPicker = true;
            if (folder.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string folderPath = folder.FileName;
                textFolder.Text = folderPath;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        // 檢視附件資料夾
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            string myPath = @"";
            myPath += textFolder.Text;
            if (!string.IsNullOrEmpty(myPath))
            {
                System.Diagnostics.Process prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = myPath;
                prc.Start();
            }
        }

        public void UpdateSendStatus(int x, int total)
        {
            txtSendStatus.Text = $"已傳送 {x + 1} / {total} 封郵件";
        }

    }
}
