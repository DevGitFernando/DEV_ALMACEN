﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Dll_INT_CNNSIADISSEP.ws_Interface_SII_SIADISSEP {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="wsISIADISSEPSoap", Namespace="http://SC-Solutions/ServiciosWeb/")]
    public partial class wsISIADISSEP : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback RecepcionDeRecetaElectronicaOperationCompleted;
        
        private System.Threading.SendOrPostCallback CancelacionDeRecetaElectronicaOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsISIADISSEP() {
            this.Url = global::Dll_INT_CNNSIADISSEP.Properties.Settings.Default.Dll_INT_CNNSIADISSEP_ws_Interface_SII_SIADISSEP_wsISIADISSEP;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event RecepcionDeRecetaElectronicaCompletedEventHandler RecepcionDeRecetaElectronicaCompleted;
        
        /// <remarks/>
        public event CancelacionDeRecetaElectronicaCompletedEventHandler CancelacionDeRecetaElectronicaCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/RecepcionDeRecetaElectronica", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string RecepcionDeRecetaElectronica(string Informacion_XML, string GUID) {
            object[] results = this.Invoke("RecepcionDeRecetaElectronica", new object[] {
                        Informacion_XML,
                        GUID});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void RecepcionDeRecetaElectronicaAsync(string Informacion_XML, string GUID) {
            this.RecepcionDeRecetaElectronicaAsync(Informacion_XML, GUID, null);
        }
        
        /// <remarks/>
        public void RecepcionDeRecetaElectronicaAsync(string Informacion_XML, string GUID, object userState) {
            if ((this.RecepcionDeRecetaElectronicaOperationCompleted == null)) {
                this.RecepcionDeRecetaElectronicaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRecepcionDeRecetaElectronicaOperationCompleted);
            }
            this.InvokeAsync("RecepcionDeRecetaElectronica", new object[] {
                        Informacion_XML,
                        GUID}, this.RecepcionDeRecetaElectronicaOperationCompleted, userState);
        }
        
        private void OnRecepcionDeRecetaElectronicaOperationCompleted(object arg) {
            if ((this.RecepcionDeRecetaElectronicaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RecepcionDeRecetaElectronicaCompleted(this, new RecepcionDeRecetaElectronicaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/CancelacionDeRecetaElectronica", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CancelacionDeRecetaElectronica(string Informacion_XML, string GUID) {
            object[] results = this.Invoke("CancelacionDeRecetaElectronica", new object[] {
                        Informacion_XML,
                        GUID});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CancelacionDeRecetaElectronicaAsync(string Informacion_XML, string GUID) {
            this.CancelacionDeRecetaElectronicaAsync(Informacion_XML, GUID, null);
        }
        
        /// <remarks/>
        public void CancelacionDeRecetaElectronicaAsync(string Informacion_XML, string GUID, object userState) {
            if ((this.CancelacionDeRecetaElectronicaOperationCompleted == null)) {
                this.CancelacionDeRecetaElectronicaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCancelacionDeRecetaElectronicaOperationCompleted);
            }
            this.InvokeAsync("CancelacionDeRecetaElectronica", new object[] {
                        Informacion_XML,
                        GUID}, this.CancelacionDeRecetaElectronicaOperationCompleted, userState);
        }
        
        private void OnCancelacionDeRecetaElectronicaOperationCompleted(object arg) {
            if ((this.CancelacionDeRecetaElectronicaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CancelacionDeRecetaElectronicaCompleted(this, new CancelacionDeRecetaElectronicaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void RecepcionDeRecetaElectronicaCompletedEventHandler(object sender, RecepcionDeRecetaElectronicaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RecepcionDeRecetaElectronicaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RecepcionDeRecetaElectronicaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void CancelacionDeRecetaElectronicaCompletedEventHandler(object sender, CancelacionDeRecetaElectronicaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CancelacionDeRecetaElectronicaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CancelacionDeRecetaElectronicaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591