﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión del motor en tiempo de ejecución:2.0.50727.5456
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=2.0.50727.5456.
// 
#pragma warning disable 1591

namespace TestInformacion.wsInformacion {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="wsInformacionSoap", Namespace="http://SC-Solutions/ServiciosWeb/")]
    public partial class wsInformacion : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ConectividadOperationCompleted;
        
        private System.Threading.SendOrPostCallback CuadroBasicoOperationCompleted;
        
        private System.Threading.SendOrPostCallback Existencia_ClaveOperationCompleted;
        
        private System.Threading.SendOrPostCallback Existencia_Clave_GrupoOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsInformacion() {
            this.Url = global::TestInformacion.Properties.Settings.Default.TestInformacion_wsInformacion_wsInformacion;
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
        public event ConectividadCompletedEventHandler ConectividadCompleted;
        
        /// <remarks/>
        public event CuadroBasicoCompletedEventHandler CuadroBasicoCompleted;
        
        /// <remarks/>
        public event Existencia_ClaveCompletedEventHandler Existencia_ClaveCompleted;
        
        /// <remarks/>
        public event Existencia_Clave_GrupoCompletedEventHandler Existencia_Clave_GrupoCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/Conectividad", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Conectividad() {
            object[] results = this.Invoke("Conectividad", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ConectividadAsync() {
            this.ConectividadAsync(null);
        }
        
        /// <remarks/>
        public void ConectividadAsync(object userState) {
            if ((this.ConectividadOperationCompleted == null)) {
                this.ConectividadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConectividadOperationCompleted);
            }
            this.InvokeAsync("Conectividad", new object[0], this.ConectividadOperationCompleted, userState);
        }
        
        private void OnConectividadOperationCompleted(object arg) {
            if ((this.ConectividadCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConectividadCompleted(this, new ConectividadCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/CuadroBasico", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet CuadroBasico(string IdEstado, string IdFarmacia) {
            object[] results = this.Invoke("CuadroBasico", new object[] {
                        IdEstado,
                        IdFarmacia});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void CuadroBasicoAsync(string IdEstado, string IdFarmacia) {
            this.CuadroBasicoAsync(IdEstado, IdFarmacia, null);
        }
        
        /// <remarks/>
        public void CuadroBasicoAsync(string IdEstado, string IdFarmacia, object userState) {
            if ((this.CuadroBasicoOperationCompleted == null)) {
                this.CuadroBasicoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCuadroBasicoOperationCompleted);
            }
            this.InvokeAsync("CuadroBasico", new object[] {
                        IdEstado,
                        IdFarmacia}, this.CuadroBasicoOperationCompleted, userState);
        }
        
        private void OnCuadroBasicoOperationCompleted(object arg) {
            if ((this.CuadroBasicoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CuadroBasicoCompleted(this, new CuadroBasicoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/Existencia_Clave", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet Existencia_Clave(string IdEstado, string IdFarmacia, string ClaveSSA) {
            object[] results = this.Invoke("Existencia_Clave", new object[] {
                        IdEstado,
                        IdFarmacia,
                        ClaveSSA});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void Existencia_ClaveAsync(string IdEstado, string IdFarmacia, string ClaveSSA) {
            this.Existencia_ClaveAsync(IdEstado, IdFarmacia, ClaveSSA, null);
        }
        
        /// <remarks/>
        public void Existencia_ClaveAsync(string IdEstado, string IdFarmacia, string ClaveSSA, object userState) {
            if ((this.Existencia_ClaveOperationCompleted == null)) {
                this.Existencia_ClaveOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExistencia_ClaveOperationCompleted);
            }
            this.InvokeAsync("Existencia_Clave", new object[] {
                        IdEstado,
                        IdFarmacia,
                        ClaveSSA}, this.Existencia_ClaveOperationCompleted, userState);
        }
        
        private void OnExistencia_ClaveOperationCompleted(object arg) {
            if ((this.Existencia_ClaveCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Existencia_ClaveCompleted(this, new Existencia_ClaveCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/Existencia_Clave_Grupo", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet Existencia_Clave_Grupo(string IdEstado, string IdFarmacia, string ClaveSSA) {
            object[] results = this.Invoke("Existencia_Clave_Grupo", new object[] {
                        IdEstado,
                        IdFarmacia,
                        ClaveSSA});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void Existencia_Clave_GrupoAsync(string IdEstado, string IdFarmacia, string ClaveSSA) {
            this.Existencia_Clave_GrupoAsync(IdEstado, IdFarmacia, ClaveSSA, null);
        }
        
        /// <remarks/>
        public void Existencia_Clave_GrupoAsync(string IdEstado, string IdFarmacia, string ClaveSSA, object userState) {
            if ((this.Existencia_Clave_GrupoOperationCompleted == null)) {
                this.Existencia_Clave_GrupoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExistencia_Clave_GrupoOperationCompleted);
            }
            this.InvokeAsync("Existencia_Clave_Grupo", new object[] {
                        IdEstado,
                        IdFarmacia,
                        ClaveSSA}, this.Existencia_Clave_GrupoOperationCompleted, userState);
        }
        
        private void OnExistencia_Clave_GrupoOperationCompleted(object arg) {
            if ((this.Existencia_Clave_GrupoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Existencia_Clave_GrupoCompleted(this, new Existencia_Clave_GrupoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void ConectividadCompletedEventHandler(object sender, ConectividadCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConectividadCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConectividadCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void CuadroBasicoCompletedEventHandler(object sender, CuadroBasicoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CuadroBasicoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CuadroBasicoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void Existencia_ClaveCompletedEventHandler(object sender, Existencia_ClaveCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Existencia_ClaveCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Existencia_ClaveCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void Existencia_Clave_GrupoCompletedEventHandler(object sender, Existencia_Clave_GrupoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Existencia_Clave_GrupoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Existencia_Clave_GrupoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591