﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dll_IFacturacion.paxsvrCancelacionService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfString", Namespace="https://www.paxfacturacion.com.mx:453", ItemName="string")]
    [System.SerializableAttribute()]
    public class ArrayOfString : System.Collections.Generic.List<string> {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://www.paxfacturacion.com.mx:453", ConfigurationName="paxsvrCancelacionService.wcfCancelaASMXSoap")]
    public interface wcfCancelaASMXSoap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento sListaUUID del espacio de nombres https://www.paxfacturacion.com.mx:453 no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="https://www.paxfacturacion.com.mx:453/fnCancelarXML", ReplyAction="*")]
        Dll_IFacturacion.paxsvrCancelacionService.fnCancelarXMLResponse fnCancelarXML(Dll_IFacturacion.paxsvrCancelacionService.fnCancelarXMLRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class fnCancelarXMLRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="fnCancelarXML", Namespace="https://www.paxfacturacion.com.mx:453", Order=0)]
        public Dll_IFacturacion.paxsvrCancelacionService.fnCancelarXMLRequestBody Body;
        
        public fnCancelarXMLRequest() {
        }
        
        public fnCancelarXMLRequest(Dll_IFacturacion.paxsvrCancelacionService.fnCancelarXMLRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="https://www.paxfacturacion.com.mx:453")]
    public partial class fnCancelarXMLRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Dll_IFacturacion.paxsvrCancelacionService.ArrayOfString sListaUUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string psRFC;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int pnId_Estructura;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string sNombre;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string sContraseña;
        
        public fnCancelarXMLRequestBody() {
        }
        
        public fnCancelarXMLRequestBody(Dll_IFacturacion.paxsvrCancelacionService.ArrayOfString sListaUUID, string psRFC, int pnId_Estructura, string sNombre, string sContraseña) {
            this.sListaUUID = sListaUUID;
            this.psRFC = psRFC;
            this.pnId_Estructura = pnId_Estructura;
            this.sNombre = sNombre;
            this.sContraseña = sContraseña;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class fnCancelarXMLResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="fnCancelarXMLResponse", Namespace="https://www.paxfacturacion.com.mx:453", Order=0)]
        public Dll_IFacturacion.paxsvrCancelacionService.fnCancelarXMLResponseBody Body;
        
        public fnCancelarXMLResponse() {
        }
        
        public fnCancelarXMLResponse(Dll_IFacturacion.paxsvrCancelacionService.fnCancelarXMLResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="https://www.paxfacturacion.com.mx:453")]
    public partial class fnCancelarXMLResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string fnCancelarXMLResult;
        
        public fnCancelarXMLResponseBody() {
        }
        
        public fnCancelarXMLResponseBody(string fnCancelarXMLResult) {
            this.fnCancelarXMLResult = fnCancelarXMLResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface wcfCancelaASMXSoapChannel : Dll_IFacturacion.paxsvrCancelacionService.wcfCancelaASMXSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class wcfCancelaASMXSoapClient : System.ServiceModel.ClientBase<Dll_IFacturacion.paxsvrCancelacionService.wcfCancelaASMXSoap>, Dll_IFacturacion.paxsvrCancelacionService.wcfCancelaASMXSoap {
        
        public wcfCancelaASMXSoapClient() {
        }
        
        public wcfCancelaASMXSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public wcfCancelaASMXSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public wcfCancelaASMXSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public wcfCancelaASMXSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Dll_IFacturacion.paxsvrCancelacionService.fnCancelarXMLResponse Dll_IFacturacion.paxsvrCancelacionService.wcfCancelaASMXSoap.fnCancelarXML(Dll_IFacturacion.paxsvrCancelacionService.fnCancelarXMLRequest request) {
            return base.Channel.fnCancelarXML(request);
        }
        
        public string fnCancelarXML(Dll_IFacturacion.paxsvrCancelacionService.ArrayOfString sListaUUID, string psRFC, int pnId_Estructura, string sNombre, string sContraseña) {
            Dll_IFacturacion.paxsvrCancelacionService.fnCancelarXMLRequest inValue = new Dll_IFacturacion.paxsvrCancelacionService.fnCancelarXMLRequest();
            inValue.Body = new Dll_IFacturacion.paxsvrCancelacionService.fnCancelarXMLRequestBody();
            inValue.Body.sListaUUID = sListaUUID;
            inValue.Body.psRFC = psRFC;
            inValue.Body.pnId_Estructura = pnId_Estructura;
            inValue.Body.sNombre = sNombre;
            inValue.Body.sContraseña = sContraseña;
            Dll_IFacturacion.paxsvrCancelacionService.fnCancelarXMLResponse retVal = ((Dll_IFacturacion.paxsvrCancelacionService.wcfCancelaASMXSoap)(this)).fnCancelarXML(inValue);
            return retVal.Body.fnCancelarXMLResult;
        }
    }
}