﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dll_IFacturacion.xsasvrCFDService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfString", Namespace="http://ws.cfds.action.manager.xsa.tralix.com", ItemName="string")]
    [System.SerializableAttribute()]
    public class ArrayOfString : System.Collections.Generic.List<string> {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ws.cfds.action.manager.xsa.tralix.com", ConfigurationName="xsasvrCFDService.CFDServicePortType")]
    public interface CFDServicePortType {
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="out")]
        string mensaje();
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="out")]
        int cfdTotalRows(string in0, string in1, string in2, string in3, string in4);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="out")]
        Dll_IFacturacion.xsasvrCFDService.ArrayOfString[] cfdsByFilter(string in0, string in1, string in2, string in3, System.Nullable<int> in4, System.Nullable<int> in5, string in6, string in7);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CFDServicePortTypeChannel : Dll_IFacturacion.xsasvrCFDService.CFDServicePortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CFDServicePortTypeClient : System.ServiceModel.ClientBase<Dll_IFacturacion.xsasvrCFDService.CFDServicePortType>, Dll_IFacturacion.xsasvrCFDService.CFDServicePortType {
        
        public CFDServicePortTypeClient() {
        }
        
        public CFDServicePortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CFDServicePortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CFDServicePortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CFDServicePortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string mensaje() {
            return base.Channel.mensaje();
        }
        
        public int cfdTotalRows(string in0, string in1, string in2, string in3, string in4) {
            return base.Channel.cfdTotalRows(in0, in1, in2, in3, in4);
        }
        
        public Dll_IFacturacion.xsasvrCFDService.ArrayOfString[] cfdsByFilter(string in0, string in1, string in2, string in3, System.Nullable<int> in4, System.Nullable<int> in5, string in6, string in7) {
            return base.Channel.cfdsByFilter(in0, in1, in2, in3, in4, in5, in6, in7);
        }
    }
}
