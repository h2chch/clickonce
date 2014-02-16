using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace ClickOnce.Lib
{
    public static class RegistryHelper
    {
        private const string RootKey = "Software";

        public static RegistryKey OpenProductKey(string productName)
        {
            string keyPath = string.Format("{0}\\{1}", RootKey, productName);

            RegistryKey productKey = Registry.CurrentUser.OpenSubKey(keyPath, true);

            if (productKey == null)
            {
                productKey = Registry.CurrentUser.CreateSubKey(keyPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
            }
            return productKey; 
        }


        public static string GetValue(RegistryKey parent, string key)
        {
            object value = parent.GetValue(key, null);
            return value as string;
        }

        public static void SetValue(RegistryKey parent, string key, string value)
        {
            parent.SetValue(key, value, RegistryValueKind.String);
        }

    }
}
