using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClickOnce.Lib;
using System.Deployment.Application;
using ClickOnce.Shell.Properties;

namespace ClickOnce.Shell
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Is Network Deployed: " + ApplicationDeployment.IsNetworkDeployed);
            MessageBox.Show("Is First Run: " + ApplicationDeployment.CurrentDeployment.IsFirstRun);

            if (ApplicationDeployment.IsNetworkDeployed && ApplicationDeployment.CurrentDeployment.IsFirstRun)
            {
                Settings.Default.IsFirstRun = false;    
                Settings.Default.Save(); 
            }




        }
    }
}
