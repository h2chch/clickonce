using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace ClickOnce.Lib
{
    public static class DpapiHelper
    {
        public static string EncryptData(string input)
        {
            var data = Encoding.Unicode.GetBytes(input);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.LocalMachine);
            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptData(string input)
        {
            byte[] data = Convert.FromBase64String(input);
            byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.LocalMachine);
            return Encoding.Unicode.GetString(decrypted);
        }
    }
}
