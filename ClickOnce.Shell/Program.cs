using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Deployment.Application;
using ClickOnce.Shell.Properties;
using Microsoft.Win32;
using System.Threading;
using System.Reflection;
using ClickOnce.Lib;

namespace ClickOnce.Shell
{
    static class Program
    {
        private static Mutex instanceMutex;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            OpenMutex();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            OnInstall();

            Application.Run(new MainForm());

            CloseMutex();
        }


        static void OpenMutex()
        {
            bool createdNew;
            string mutexName = @"Local\" + Assembly.GetExecutingAssembly().GetType().GUID;
            instanceMutex = new Mutex(true, mutexName, out createdNew);

            if (!createdNew)
            {
                instanceMutex = null;
                return;
            }
        }

        static void CloseMutex()
        {
            if (instanceMutex == null)
                return;

            instanceMutex.ReleaseMutex();
            instanceMutex.Close();
            instanceMutex = null;
        }

        static void OnInstall()
        {
            //MessageBox.Show("Is Network Deployed: " + ApplicationDeployment.IsNetworkDeployed +
            //    "Is First Run: " + ApplicationDeployment.CurrentDeployment.IsFirstRun);

            Startup startup = new Startup(Settings.Default.ApplicationName);
            string customerKey = startup.ReadRegistry();

            if (ApplicationDeployment.IsNetworkDeployed && ApplicationDeployment.CurrentDeployment.IsFirstRun)
            {
                if (string.IsNullOrEmpty(customerKey))
                {
                    customerKey = startup.RetrieveCustomerKey();
                    startup.WriteRegistry(customerKey);
                }

                Settings.Default.IsFirstRun = false;
                Settings.Default.Save();
            }

            MessageBox.Show(customerKey);
        }
    }

}
