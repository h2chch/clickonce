﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClickOnce.Lib.SecurityService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SecurityService.ISecurityService")]
    public interface ISecurityService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/GetCustomerKey", ReplyAction="http://tempuri.org/ISecurityService/GetCustomerKeyResponse")]
        string GetCustomerKey(string userName, string macAddress);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISecurityServiceChannel : ClickOnce.Lib.SecurityService.ISecurityService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SecurityServiceClient : System.ServiceModel.ClientBase<ClickOnce.Lib.SecurityService.ISecurityService>, ClickOnce.Lib.SecurityService.ISecurityService {
        
        public SecurityServiceClient() {
        }
        
        public SecurityServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SecurityServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SecurityServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SecurityServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetCustomerKey(string userName, string macAddress) {
            return base.Channel.GetCustomerKey(userName, macAddress);
        }
    }
}