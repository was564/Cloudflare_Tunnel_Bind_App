using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudflareTunnelBindApp
{
    internal static class Program
    {
        private static Mutex mutex = null;

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string processName = Process.GetCurrentProcess().ProcessName;
            if (isExistProcessMutex(processName))
            {
                MessageBox.Show(LanguageManager.Translate(LanguageManager.TranslationKey.DuplicateExecution), "Duplicate Execution", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static bool isExistProcessMutex(string processName)
        {
            mutex = new Mutex(true, processName, out bool isNewInstance);
            return !isNewInstance;
        }
    }
}
