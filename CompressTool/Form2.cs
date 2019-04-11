using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using Microsoft.Exchange.WebServices.Data;
using System.Net;
using Attachment = Microsoft.Exchange.WebServices.Data.Attachment;

namespace CompressTool
{
    public partial class EmailSender : Form
    {
        public EmailSender()
        {
            
            InitializeComponent();
            txtAccount.Focus();
        }

        private void btnsend_Click(object sender, EventArgs e)
        {

            try 
            {
                Cursor.Current = Cursors.WaitCursor;
                string user = txtAccount.Text;
                string pass = txtPass.Text;
                string senderEmail = txtSender.Text + txtSenderHoset.Text;
                string reciever = txtReciever.Text + txtRecieverHost.Text;
                string body = txtBody.Text;
                string subject = txtSubject.Text;

                //判斷輸入框是否為空
                foreach (Control cur in Controls)           
                {
                    if (cur is TextBox && cur.Text == string.Empty)            
                    {
                        if(cur.Name != "txtSubject" || cur.Name != "txtBody" || cur.Name != "textFolder")
                        MessageBox.Show("請檢查是否有資料遺漏", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Setting Credential of gmail account
                ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
                service.Credentials = new NetworkCredential(user, pass);
                service.AutodiscoverUrl(senderEmail);
                //service.enable
                EmailMessage message = new EmailMessage(service);
                
                // Add properties to the email message.
                message.Subject = subject;
                message.Body = body;
                message.ToRecipients.Add(reciever);

                // 是否有附加檔案
                if (textFolder.Text != "")
                {
                    string[] files = Directory.GetFiles(textFolder.Text, "*");
                    string number = "";

                    for(int i = 0; i < files.Length; i++)
                    {
                        //message.Attachments.AddItemAttachment
                        message.Attachments.AddFileAttachment(files[i]);
                        number = $"({i.ToString()})" ;
                        message.Subject = subject + number;
                        
                        message.SendAndSaveCopy();

                        message = null; // Free the memory
                        message = new EmailMessage(service);
                        message.Subject = subject;
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
                message = null; // Free the memory
            } 
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
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
    }
}
