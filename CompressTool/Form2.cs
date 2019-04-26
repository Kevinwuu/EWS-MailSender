using Microsoft.Exchange.WebServices.Data;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using CompressTool.Properties;
using log4net;

namespace CompressTool
{

    public partial class EmailSender : Form
    {
        // 取得Logger(Logger以Program的Type Name命名)
        public static ILog Log = LogManager.GetLogger(typeof(Program));


        public EmailSender()
        {           
            InitializeComponent();
            txtAccount.Focus();
            //帶入記錄值
            txtAccount.Text = Settings.Default.accountCache;
            txtReceiverName.Text = Settings.Default.receiverNameCache;
            txtReceiverHost.Text = Settings.Default.receiverHostCache;


        }
        public string accountCache,receiverNameCache,receiverHostCache;



        //private static MemoryCache cache = MemoryCache.Default;


        // 帶入壓縮檔所在路徑
        public void changeAttachPath(string str)
        {
            txtFolder.Text = str;
        }

        #region 送出信件
        private void btnsend_Click(object sender, EventArgs e)
        {
            Log.Info("開始傳送...");
            txtFinished.Text = "";
            txtSendStatus.Text = "";
            if (CheckDataGrid())
            {
                string user = txtAccount.Text;
                string password = txtPass.Text;
                string receiver = txtReceiverName.Text + "@" + txtReceiverHost.Text;
                string body = txtBody.Text;
                string subject = txtSubject.Text;

                // 記錄快取
                accountCache = user;
                receiverNameCache = txtReceiverName.Text;
                receiverHostCache = txtReceiverHost.Text;

                Cursor.Current = Cursors.WaitCursor;

                try 
                {
                    // Setting Credential 
                    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
                    service.Credentials = new NetworkCredential(user, password);
                    service.Url = new Uri("https://casarray.systex.tw/EWS/Exchange.asmx");

                    // 設定信件屬性
                    EmailMessage message = new EmailMessage(service);
                    message.Subject = subject;
                    message.Body = body;
                    message.ToRecipients.Add(receiver);


                    // 判斷是否有附加檔案
                    if (txtFolder.Text != "")
                    {
                        string[] files = Directory.GetFiles(txtFolder.Text, "*");

                        for (int i = 0; i < files.Length; i++)
                        {
                            UpdateSendStatus(0, files.Length);

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
                            message.ToRecipients.Add(receiver);
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
                    Log.Info("傳送完成.");
                } 
                catch(Exception ex) 
                {
                    Log.Fatal(ex);
                    MessageBox.Show(ex.Message, "Error");
                    return;
                }

            }
        }
        #endregion

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            Log.Info("選擇附件資料夾.");
            CommonOpenFileDialog folder = new CommonOpenFileDialog();
            folder.IsFolderPicker = true;
            if (folder.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string folderPath = folder.FileName;
                txtFolder.Text = folderPath;
            }
        }

        // 返回
        private void btnBack_Click(object sender, EventArgs e)
        {
            Log.Info("Hide Dialog.");
            this.Hide();
        }

        // 檢視附件資料夾
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            Log.Info("檢視附件資料夾.");
            string myPath = @"";
            myPath += txtFolder.Text;
            if (!string.IsNullOrEmpty(myPath))
            {
                System.Diagnostics.Process prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = myPath;
                prc.Start();
            }
        }

        // 信件傳送狀態
        public void UpdateSendStatus(int x, int total)
        {
            txtSendStatus.Text = $"{x + 1} / {total} 已傳送";
        }

        //判斷是否有空白輸入
        public bool CheckDataGrid()
        {
            foreach (Control cur in Controls)
            {
                if (cur is TextBox && cur.Text == string.Empty)
                {
                    // 排除非必要輸入欄位
                    if (cur.Name != "txtBody" && cur.Name != "txtFolder")
                    {
                        MessageBox.Show("請檢查是否有資料遺漏", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
