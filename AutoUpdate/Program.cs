using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutoUpdate
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
            var serverIP = "47.93.200.67";
            var serverPort = 19921;
            var callBackExeName = "LSP.exe";
            var title = "在线更新";
            var processName = callBackExeName.Substring(0, callBackExeName.Length - 4);
            bool haveRun = ESBasic.Helpers.ApplicationHelper.IsAppInstanceExist(processName);
            if (haveRun)
            {
                MessageBox.Show(Properties.Resources.TargetIsRunning);
                return;
            }

            var form = new AutoUpdate(serverIP, serverPort, callBackExeName, title);
            Application.Run(form);
        }
    }
}
