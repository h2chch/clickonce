using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ClickOnce.WebService
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri[] baseAddresses = new Uri[] {  
                new Uri("net.tcp://localhost:8888"),
                new Uri("http://localhost:8889")
            };


            using (ServiceHost host = new ServiceHost(typeof(SecurityService), baseAddresses))
            {
                host.AddServiceEndpoint(typeof(ISecurityService), new NetTcpBinding(), "");

                ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
                serviceBehavior.HttpGetEnabled = true;

                host.Description.Behaviors.Add(serviceBehavior);

                ServiceDebugBehavior debugBehavior = host.Description.Behaviors.Find<ServiceDebugBehavior>();

                if (debugBehavior != null)
                {
                    debugBehavior.IncludeExceptionDetailInFaults = true;
                }
                else
                {
                    debugBehavior = new ServiceDebugBehavior();
                    debugBehavior.IncludeExceptionDetailInFaults = true;

                    host.Description.Behaviors.Add(debugBehavior);
                }

                host.Open();

                Console.WriteLine("Host is running ... Press <Enter> key to stop");
                Console.ReadKey();
            }
        }
    }
}
