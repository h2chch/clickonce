using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace ClickOnce.WebService
{
    public class SecurityService : ISecurityService
    {
        static IList<Tuple<string, string, string>> _validKeys;
        static IList<Tuple<string, string, string>> ValidKeys
        {
            get
            {
                if (_validKeys == null)
                {
                    _validKeys = new List<Tuple<string, string, string>>();
                    _validKeys.Add(new Tuple<string, string, string>("test", "C2143DC3B443", "valid customer key"));
                }

                return _validKeys;
            }

            set
            {
                _validKeys = value;
            }
        }

        public string GetCustomerKey(string userName, string macAddress)
        {
            Console.WriteLine("Receiving request, username: " + userName + " mac address: " + macAddress);

            Tuple<string, string, string> key = ValidKeys.FirstOrDefault(k => 
                k.Item1.Equals(userName, StringComparison.InvariantCultureIgnoreCase) 
                && k.Item2.Equals(macAddress, StringComparison.InvariantCultureIgnoreCase));

            if (key == null)
            {
                throw new FaultException("invalid user");
            }
            
            return key.Item3;
        }
    }
}
