using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Deployment.Application;
using ClickOnce.Shell.Properties;
using ClickOnce.Lib;

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
            Startup startup = new Startup(Settings.Default.ApplicationName);
            string customerKey = startup.ReadRegistry();
            string connectionString = startup.RetrieveConnectionString(customerKey);

            textBoxCustomerKey.Text = customerKey;
            textBoxConnectionString.Text = connectionString;
        }
    }
}
