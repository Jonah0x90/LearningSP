using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LSP
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(ConfigurationManager.AppSettings["CurrentLanguage"]);
            //if (VersionHelper.HasNewVersion("47.93.200.67", 19921))
            //{
            //    string updateExePath = AppDomain.CurrentDomain.BaseDirectory + "AutoUpdate.exe";
            //    System.Diagnostics.Process myProcess = System.Diagnostics.Process.Start(updateExePath);
            //    System.Threading.Thread.Sleep(500);
            //    Application.Exit();
            //}
            //else
            //{
                Application.Run(new LSP());
            //}
        }
    }
}
