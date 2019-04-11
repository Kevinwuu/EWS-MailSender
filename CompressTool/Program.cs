using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CompressTool
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CompressTool compress = new CompressTool();
            //EmailSender email = new EmailSender();
            //email.Hide();
            compress.Text = "壓縮寄信小工具";
            Application.Run(compress);
        }
    }
}
