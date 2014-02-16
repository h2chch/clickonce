using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using ClickOnce.Lib.SecurityService;
using System.ServiceModel;
using System.Collections.Specialized;
using System.Deployment.Application;
using System.Web;
using System.Windows.Forms;

namespace ClickOnce.Lib
{
    public class Startup
    {
        private string applicationName;

        public Startup(string applicationName)
        {
            this.applicationName = applicationName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string RetrieveCustomerKey()
        {
            NameValueCollection nameValues = GetQueryStringParameters();
            string userName = nameValues["username"];
            string macAddress = string.Empty;
            string customerKey = string.Empty;

            if (!string.IsNullOrEmpty(userName))
            {
                macAddress = NetHelper.GetMacAddress();
                customerKey = GetCustomerKey(userName, macAddress);
            }

            MessageBox.Show("customerKey : "+ customerKey);
            return customerKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ReadRegistry()
        {
            string customerKey = string.Empty;
            using (RegistryKey parent = RegistryHelper.OpenProductKey(this.applicationName))
            {
                string encryptedData = RegistryHelper.GetValue(parent, "customerkey");
                if (string.IsNullOrEmpty(encryptedData))
                    return string.Empty;

                customerKey = DpapiHelper.DecryptData(encryptedData);
            }
            return customerKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerKey"></param>
        public void WriteRegistry(string customerKey)
        {
            string encryptedData = DpapiHelper.EncryptData(customerKey);
            using (RegistryKey parent = RegistryHelper.OpenProductKey(this.applicationName))
            {
                RegistryHelper.SetValue(parent, "customerkey", encryptedData);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private NameValueCollection GetQueryStringParameters()
        {
            NameValueCollection nameValues = new NameValueCollection();

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                string queryString = ApplicationDeployment.CurrentDeployment.ActivationUri.Query;
                nameValues = HttpUtility.ParseQueryString(queryString);
            }

            return nameValues;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="macAddress"></param>
        /// <returns></returns>
        private string GetCustomerKey(string username, string macAddress)
        {
            string customerKey = string.Empty;
            using (SecurityServiceClient client = new SecurityServiceClient())
            {
                try
                {
                    customerKey = client.GetCustomerKey(username, macAddress);
                }
                catch (FaultException)
                {
                    return null;
                }
            }
            return customerKey;
        }

    }
}
