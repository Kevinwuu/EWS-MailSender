using Microsoft.Exchange.WebServices.Data;
using System;
using System.IO;
using System.Net;
using System.Threading;
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

        private void btnsend_Click(object sender, EventArgs e)
        {

            try 
            {
                //progressBar1.Visible = true;


                Cursor.Current = Cursors.WaitCursor;
                string user = txtAccount.Text;
                string pass = txtPass.Text;
                string senderEmail = txtSender.Text + txtSenderHoset.Text;
                string reciever = txtReciever.Text + txtRecieverHost.Text;
                string body = txtBody.Text;
                string subject = txtSubject.Text;

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
                        if(cur.Name != "txtSubject" || cur.Name != "txtBody" || cur.Name != "textFolder")
                        MessageBox.Show("請檢查是否有資料遺漏", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }




                //Thread thread = new Thread(new ThreadStart(addAttachment));
                //thread.Start();

                // 是否有附加檔案 AddAttachment
                if (textFolder.Text != "")
                {
                    string[] files = Directory.GetFiles(textFolder.Text, "*");
                    string number = "";

                    for (int i = 0; i < files.Length; i++)
                    {

                        message.Attachments.AddFileAttachment(files[i]);

                        // 改變主旨編號
                        number = $"({i.ToString()})";
                        message.Subject = subject + number;
                        this.Refresh();


                        message.SendAndSaveCopy();

                        txtSendStatus.Text = $"{i + 1}/{files.Length}已傳送";
                        //Thread thread = new Thread(new ThreadStart(DoWor k));
                        //thread.Start();
                        //MyInvoke mi = new MyInvoke(UpdateSendStatus);
                        //this.BeginInvoke(mi, new Object[] { i + 1, files.Length });


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
                MessageBox.Show("已寄出!");
                Cursor.Current = Cursors.Default;
                // Free the memory
                message = null;
            } 
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Message");
                return;
            }
        }
        public void addAttachment()
        {
            string user = txtAccount.Text;
            string pass = txtPass.Text;
            string senderEmail = txtSender.Text + txtSenderHoset.Text;
            string reciever = txtReciever.Text + txtRecieverHost.Text;
            string body = txtBody.Text;
            string subject = txtSubject.Text;

            // Setting Credential 
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
            service.Credentials = new NetworkCredential(user, pass);
            service.AutodiscoverUrl(senderEmail);

            EmailMessage message = new EmailMessage(service);

            // 加入屬性
            message.Subject = subject;
            message.Body = body;
            message.ToRecipients.Add(reciever);



            // 是否有附加檔案 AddAttachment
            if (textFolder.Text != "")
            {
                string[] files = Directory.GetFiles(textFolder.Text, "*");
                string number = "";

                for (int i = 0; i < files.Length; i++)
                {

                    message.Attachments.AddFileAttachment(files[i]);

                    // 改變主旨編號
                    number = $"({i.ToString()})";
                    message.Subject = subject + number;
                    message.SendAndSaveCopy();

                    //MyInvoke mi = new MyInvoke(UpdateSendStatus);
                    //this.BeginInvoke(mi, new Object[] { i + 1, files.Length });
                    txtSendStatus.Text = $"已傳送{i+1}/{files.Length}封郵件";
                    this.Refresh();

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
            MessageBox.Show("已寄出!");
            Cursor.Current = Cursors.Default;
            // Free the memory
            message = null;
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folder.SelectedPath;
                textFolder.Text = folderPath;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

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

        public void UpdateSendStatus(string x, string total)
        {
            txtSendStatus.Text = $"已傳送{x + 1}/{total}封郵件";
        }
    }
}
