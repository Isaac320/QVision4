using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QVision.Tools;
using System.Threading;

namespace QVision
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Thread t = new Thread(ShowSplashFrm);
                t.Start();
                Thread.Sleep(100);

                Application.Run(new MainFrm());
            }
            catch (Exception ee)
            {
                LogManager.WriteLog(ee.Message);
                LogManager.WriteLog(ee.StackTrace);
            }
        }

        static void ShowSplashFrm()
        {
            Frm.SplashFrm splash = new Frm.SplashFrm();
            splash.ShowDialog();
        }
    }
}
