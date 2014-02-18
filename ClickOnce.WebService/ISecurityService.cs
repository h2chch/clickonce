using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ClickOnce.WebService
{
    [ServiceContract]
    public interface ISecurityService
    {
        [OperationContract]
        string GetCustomerKey(string userName, string macAddress);

        [OperationContract]
        string AuthenticateCustomerKey(string userName, string macAddress);
    }
}
