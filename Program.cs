using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace AgreementTrac
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.Name = "Main Thread";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AgreementTrac());
        }
    }
}