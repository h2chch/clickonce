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
        const string CustomerKey = "ThisIsACustomerKey";
        const string ConnectionString = "ThisIsAConnectionString";

        static IList<Tuple<string, string, string>> _validUsers;
        static IList<Tuple<string, string, string>> ValidUsers
        {
            get
            {
                if (_validUsers == null)
                {
                    _validUsers = new List<Tuple<string, string, string>>();
                    _validUsers.Add(new Tuple<string, string, string>("test", "C2143DC3B443", CustomerKey));
                }
                return _validUsers;
            }

            set
            {
                _validUsers = value;
            }
        }

        static IList<Tuple<string, string, string>> _validCustomerKeys;
        static IList<Tuple<string, string, string>> ValidCustomerKeys
        {
            get
            {
                if (_validCustomerKeys == null)
                {
                    _validCustomerKeys = new List<Tuple<string, string, string>>();
                    _validCustomerKeys.Add(new Tuple<string, string, string>(CustomerKey, "C2143DC3B443", ConnectionString));
                }
                return _validCustomerKeys;
            }

            set
            {
                _validCustomerKeys = value;
            }
        }
        

        public string GetCustomerKey(string userName, string macAddress)
        {
            Console.WriteLine("Receiving request, username: " + userName + " mac address: " + macAddress);

            Tuple<string, string, string> key = ValidUsers.FirstOrDefault(k => 
                k.Item1.Equals(userName, StringComparison.InvariantCultureIgnoreCase) 
                && k.Item2.Equals(macAddress, StringComparison.InvariantCultureIgnoreCase));

            if (key == null)
            {
                throw new FaultException("invalid user");
            }
            
            return key.Item3;
        }

        public string AuthenticateCustomerKey(string customerKey, string macAddress)
        {
            Console.WriteLine("Receiving request, customer key: " + customerKey + " mac address: " + macAddress);

            Tuple<string, string, string> key = ValidCustomerKeys.FirstOrDefault(k =>
                k.Item1.Equals(customerKey, StringComparison.InvariantCultureIgnoreCase)
                && k.Item2.Equals(macAddress, StringComparison.InvariantCultureIgnoreCase));

            if (key == null)
            {
                return "ThisIsInvalidConnectionString";
            }

            return key.Item3;

        }
    }
}
