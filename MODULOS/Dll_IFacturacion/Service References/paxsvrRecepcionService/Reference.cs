﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dll_IFacturacion.paxsvrRecepcionService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://www.paxfacturacion.com.mx:453", ConfigurationName="paxsvrRecepcionService.wcfRecepcionASMXSoap")]
    public interface wcfRecepcionASMXSoap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento psComprobante del espacio de nombres https://www.paxfacturacion.com.mx:453 no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="https://www.paxfacturacion.com.mx:453/fnEnviarXML", ReplyAction="*")]
        Dll_IFacturacion.paxsvrRecepcionService.fnEnviarXMLResponse fnEnviarXML(Dll_IFacturacion.paxsvrRecepcionService.fnEnviarXMLRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class fnEnviarXMLRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="fnEnviarXML", Namespace="https://www.paxfacturacion.com.mx:453", Order=0)]
        public Dll_IFacturacion.paxsvrRecepcionService.fnEnviarXMLRequestBody Body;
        
        public fnEnviarXMLRequest() {
        }
        
        public fnEnviarXMLRequest(Dll_IFacturacion.paxsvrRecepcionService.fnEnviarXMLRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="https://www.paxfacturacion.com.mx:453")]
    public partial class fnEnviarXMLRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string psComprobante;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string psTipoDocumento;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int pnId_Estructura;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string sNombre;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string sContraseña;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string sVersion;
        
        public fnEnviarXMLRequestBody() {
        }
        
        public fnEnviarXMLRequestBody(string psComprobante, string psTipoDocumento, int pnId_Estructura, string sNombre, string sContraseña, string sVersion) {
            this.psComprobante = psComprobante;
            this.psTipoDocumento = psTipoDocumento;
            this.pnId_Estructura = pnId_Estructura;
            this.sNombre = sNombre;
            this.sContraseña = sContraseña;
            this.sVersion = sVersion;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class fnEnviarXMLResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="fnEnviarXMLResponse", Namespace="https://www.paxfacturacion.com.mx:453", Order=0)]
        public Dll_IFacturacion.paxsvrRecepcionService.fnEnviarXMLResponseBody Body;
        
        public fnEnviarXMLResponse() {
        }
        
        public fnEnviarXMLResponse(Dll_IFacturacion.paxsvrRecepcionService.fnEnviarXMLResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="https://www.paxfacturacion.com.mx:453")]
    public partial class fnEnviarXMLResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string fnEnviarXMLResult;
        
        public fnEnviarXMLResponseBody() {
        }
        
        public fnEnviarXMLResponseBody(string fnEnviarXMLResult) {
            this.fnEnviarXMLResult = fnEnviarXMLResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface wcfRecepcionASMXSoapChannel : Dll_IFacturacion.paxsvrRecepcionService.wcfRecepcionASMXSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class wcfRecepcionASMXSoapClient : System.ServiceModel.ClientBase<Dll_IFacturacion.paxsvrRecepcionService.wcfRecepcionASMXSoap>, Dll_IFacturacion.paxsvrRecepcionService.wcfRecepcionASMXSoap {
        
        public wcfRecepcionASMXSoapClient() {
        }
        
        public wcfRecepcionASMXSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public wcfRecepcionASMXSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public wcfRecepcionASMXSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public wcfRecepcionASMXSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Dll_IFacturacion.paxsvrRecepcionService.fnEnviarXMLResponse Dll_IFacturacion.paxsvrRecepcionService.wcfRecepcionASMXSoap.fnEnviarXML(Dll_IFacturacion.paxsvrRecepcionService.fnEnviarXMLRequest request) {
            return base.Channel.fnEnviarXML(request);
        }
        
        public string fnEnviarXML(string psComprobante, string psTipoDocumento, int pnId_Estructura, string sNombre, string sContraseña, string sVersion) {
            Dll_IFacturacion.paxsvrRecepcionService.fnEnviarXMLRequest inValue = new Dll_IFacturacion.paxsvrRecepcionService.fnEnviarXMLRequest();
            inValue.Body = new Dll_IFacturacion.paxsvrRecepcionService.fnEnviarXMLRequestBody();
            inValue.Body.psComprobante = psComprobante;
            inValue.Body.psTipoDocumento = psTipoDocumento;
            inValue.Body.pnId_Estructura = pnId_Estructura;
            inValue.Body.sNombre = sNombre;
            inValue.Body.sContraseña = sContraseña;
            inValue.Body.sVersion = sVersion;
            Dll_IFacturacion.paxsvrRecepcionService.fnEnviarXMLResponse retVal = ((Dll_IFacturacion.paxsvrRecepcionService.wcfRecepcionASMXSoap)(this)).fnEnviarXML(inValue);
            return retVal.Body.fnEnviarXMLResult;
        }
    }
}
