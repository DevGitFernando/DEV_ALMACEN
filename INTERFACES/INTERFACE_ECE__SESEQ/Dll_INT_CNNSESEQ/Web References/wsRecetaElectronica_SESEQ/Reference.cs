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

namespace Dll_INT_CNNSESEQ.wsRecetaElectronica_SESEQ {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name=" Web Service SESEQBinding", Namespace="urn:webServiceSESEQ")]
    public partial class WebServiceSESEQ : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback recepcionSurtidoFarmaciaOperationCompleted;
        
        private System.Threading.SendOrPostCallback recepcionImagenFarmaciaOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WebServiceSESEQ() {
            this.Url = global::Dll_INT_CNNSESEQ.Properties.Settings.Default.Dll_INT_CNNSESEQ_wsRecetaElectronica_SESEQ__Web_Service_SESEQ;
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
        public event recepcionSurtidoFarmaciaCompletedEventHandler recepcionSurtidoFarmaciaCompleted;
        
        /// <remarks/>
        public event recepcionImagenFarmaciaCompletedEventHandler recepcionImagenFarmaciaCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:seseq#recepcionSurtidoFarmacia", RequestNamespace="urn:webServiceSESEQ", ResponseNamespace="urn:webServiceSESEQ")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public datos_salida recepcionSurtidoFarmacia(datos_entrada []datos_entrada) {
            object[] results = this.Invoke("recepcionSurtidoFarmacia", new object[] {
                        datos_entrada});
            return ((datos_salida)(results[0]));
        }
        
        /// <remarks/>
        public void recepcionSurtidoFarmaciaAsync(datos_entrada datos_entrada) {
            this.recepcionSurtidoFarmaciaAsync(datos_entrada, null);
        }
        
        /// <remarks/>
        public void recepcionSurtidoFarmaciaAsync(datos_entrada datos_entrada, object userState) {
            if ((this.recepcionSurtidoFarmaciaOperationCompleted == null)) {
                this.recepcionSurtidoFarmaciaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnrecepcionSurtidoFarmaciaOperationCompleted);
            }
            this.InvokeAsync("recepcionSurtidoFarmacia", new object[] {
                        datos_entrada}, this.recepcionSurtidoFarmaciaOperationCompleted, userState);
        }
        
        private void OnrecepcionSurtidoFarmaciaOperationCompleted(object arg) {
            if ((this.recepcionSurtidoFarmaciaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.recepcionSurtidoFarmaciaCompleted(this, new recepcionSurtidoFarmaciaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:seseq#recepcionImagenFarmacia", RequestNamespace="urn:webServiceSESEQ", ResponseNamespace="urn:webServiceSESEQ")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public datos_salida recepcionImagenFarmacia(datos_imagen []datos_imagen) {
            object[] results = this.Invoke("recepcionImagenFarmacia", new object[] {
                        datos_imagen});
            return ((datos_salida)(results[0]));
        }
        
        /// <remarks/>
        public void recepcionImagenFarmaciaAsync(datos_imagen datos_imagen) {
            this.recepcionImagenFarmaciaAsync(datos_imagen, null);
        }
        
        /// <remarks/>
        public void recepcionImagenFarmaciaAsync(datos_imagen datos_imagen, object userState) {
            if ((this.recepcionImagenFarmaciaOperationCompleted == null)) {
                this.recepcionImagenFarmaciaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnrecepcionImagenFarmaciaOperationCompleted);
            }
            this.InvokeAsync("recepcionImagenFarmacia", new object[] {
                        datos_imagen}, this.recepcionImagenFarmaciaOperationCompleted, userState);
        }
        
        private void OnrecepcionImagenFarmaciaOperationCompleted(object arg) {
            if ((this.recepcionImagenFarmaciaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.recepcionImagenFarmaciaCompleted(this, new recepcionImagenFarmaciaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:webServiceSESEQ")]
    public partial class datos_entrada {
        
        private string noRecetaField;
        
        private string claveField;
        
        private string loteField;
        
        private string caducidadField;
        
        private string surtidoField;
        
        private string fechaSurtidoField;
        
        /// <remarks/>
        public string noReceta {
            get {
                return this.noRecetaField;
            }
            set {
                this.noRecetaField = value;
            }
        }
        
        /// <remarks/>
        public string clave {
            get {
                return this.claveField;
            }
            set {
                this.claveField = value;
            }
        }
        
        /// <remarks/>
        public string lote {
            get {
                return this.loteField;
            }
            set {
                this.loteField = value;
            }
        }
        
        /// <remarks/>
        public string caducidad {
            get {
                return this.caducidadField;
            }
            set {
                this.caducidadField = value;
            }
        }
        
        /// <remarks/>
        public string surtido {
            get {
                return this.surtidoField;
            }
            set {
                this.surtidoField = value;
            }
        }
        
        /// <remarks/>
        public string fechaSurtido {
            get {
                return this.fechaSurtidoField;
            }
            set {
                this.fechaSurtidoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:webServiceSESEQ")]
    public partial class datos_imagen {
        
        private string noRecetaField;
        
        private string idImagenField;
        
        private string tipoImagenField;
        
        private string imagenB64Field;
        
        /// <remarks/>
        public string noReceta {
            get {
                return this.noRecetaField;
            }
            set {
                this.noRecetaField = value;
            }
        }
        
        /// <remarks/>
        public string idImagen {
            get {
                return this.idImagenField;
            }
            set {
                this.idImagenField = value;
            }
        }
        
        /// <remarks/>
        public string tipoImagen {
            get {
                return this.tipoImagenField;
            }
            set {
                this.tipoImagenField = value;
            }
        }
        
        /// <remarks/>
        public string imagenB64 {
            get {
                return this.imagenB64Field;
            }
            set {
                this.imagenB64Field = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:webServiceSESEQ")]
    public partial class datos_salida {
        
        private string mensajeField;
        
        /// <remarks/>
        public string mensaje {
            get {
                return this.mensajeField;
            }
            set {
                this.mensajeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void recepcionSurtidoFarmaciaCompletedEventHandler(object sender, recepcionSurtidoFarmaciaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class recepcionSurtidoFarmaciaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal recepcionSurtidoFarmaciaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public datos_salida Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((datos_salida)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void recepcionImagenFarmaciaCompletedEventHandler(object sender, recepcionImagenFarmaciaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class recepcionImagenFarmaciaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal recepcionImagenFarmaciaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public datos_salida Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((datos_salida)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591