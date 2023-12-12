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

namespace Farmacia.wsRecetarioElectronico {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="wsRecetarioSoap", Namespace="http://SC-Solutions/ServiciosWeb/")]
    public partial class wsRecetario : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback InformacionOperationCompleted;
        
        private System.Threading.SendOrPostCallback ExecuteOperationCompleted;
        
        private System.Threading.SendOrPostCallback ExecuteExtendedOperationCompleted;
        
        private System.Threading.SendOrPostCallback GenerarReporteExcelOperationCompleted;
        
        private System.Threading.SendOrPostCallback GenerarReporteOperationCompleted;
        
        private System.Threading.SendOrPostCallback TestEnlaceOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetRecetaOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetRecetaInformacionOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsRecetario() {
            this.Url = global::Farmacia.Properties.Settings.Default.Farmacia_wsRecetarioElectronico_wsRecetario;
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
        public event InformacionCompletedEventHandler InformacionCompleted;
        
        /// <remarks/>
        public event ExecuteCompletedEventHandler ExecuteCompleted;
        
        /// <remarks/>
        public event ExecuteExtendedCompletedEventHandler ExecuteExtendedCompleted;
        
        /// <remarks/>
        public event GenerarReporteExcelCompletedEventHandler GenerarReporteExcelCompleted;
        
        /// <remarks/>
        public event GenerarReporteCompletedEventHandler GenerarReporteCompleted;
        
        /// <remarks/>
        public event TestEnlaceCompletedEventHandler TestEnlaceCompleted;
        
        /// <remarks/>
        public event GetRecetaCompletedEventHandler GetRecetaCompleted;
        
        /// <remarks/>
        public event GetRecetaInformacionCompletedEventHandler GetRecetaInformacionCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/Informacion", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet Informacion(string Datos) {
            object[] results = this.Invoke("Informacion", new object[] {
                        Datos});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void InformacionAsync(string Datos) {
            this.InformacionAsync(Datos, null);
        }
        
        /// <remarks/>
        public void InformacionAsync(string Datos, object userState) {
            if ((this.InformacionOperationCompleted == null)) {
                this.InformacionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnInformacionOperationCompleted);
            }
            this.InvokeAsync("Informacion", new object[] {
                        Datos}, this.InformacionOperationCompleted, userState);
        }
        
        private void OnInformacionOperationCompleted(object arg) {
            if ((this.InformacionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.InformacionCompleted(this, new InformacionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/Execute", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet Execute(System.Data.DataSet Parametros, System.Data.DataSet InformacionCliente, bool UsarTransaccion, string Contenedor, string Sentencia) {
            object[] results = this.Invoke("Execute", new object[] {
                        Parametros,
                        InformacionCliente,
                        UsarTransaccion,
                        Contenedor,
                        Sentencia});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void ExecuteAsync(System.Data.DataSet Parametros, System.Data.DataSet InformacionCliente, bool UsarTransaccion, string Contenedor, string Sentencia) {
            this.ExecuteAsync(Parametros, InformacionCliente, UsarTransaccion, Contenedor, Sentencia, null);
        }
        
        /// <remarks/>
        public void ExecuteAsync(System.Data.DataSet Parametros, System.Data.DataSet InformacionCliente, bool UsarTransaccion, string Contenedor, string Sentencia, object userState) {
            if ((this.ExecuteOperationCompleted == null)) {
                this.ExecuteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExecuteOperationCompleted);
            }
            this.InvokeAsync("Execute", new object[] {
                        Parametros,
                        InformacionCliente,
                        UsarTransaccion,
                        Contenedor,
                        Sentencia}, this.ExecuteOperationCompleted, userState);
        }
        
        private void OnExecuteOperationCompleted(object arg) {
            if ((this.ExecuteCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ExecuteCompleted(this, new ExecuteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/ExecuteExtended", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet ExecuteExtended(System.Data.DataSet InformacionCliente, string Solicitud, string Sentencia) {
            object[] results = this.Invoke("ExecuteExtended", new object[] {
                        InformacionCliente,
                        Solicitud,
                        Sentencia});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void ExecuteExtendedAsync(System.Data.DataSet InformacionCliente, string Solicitud, string Sentencia) {
            this.ExecuteExtendedAsync(InformacionCliente, Solicitud, Sentencia, null);
        }
        
        /// <remarks/>
        public void ExecuteExtendedAsync(System.Data.DataSet InformacionCliente, string Solicitud, string Sentencia, object userState) {
            if ((this.ExecuteExtendedOperationCompleted == null)) {
                this.ExecuteExtendedOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExecuteExtendedOperationCompleted);
            }
            this.InvokeAsync("ExecuteExtended", new object[] {
                        InformacionCliente,
                        Solicitud,
                        Sentencia}, this.ExecuteExtendedOperationCompleted, userState);
        }
        
        private void OnExecuteExtendedOperationCompleted(object arg) {
            if ((this.ExecuteExtendedCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ExecuteExtendedCompleted(this, new ExecuteExtendedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/GenerarReporteExcel", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] GenerarReporteExcel(string NombrePlantilla, [System.Xml.Serialization.XmlElementAttribute("Informacion")] string Informacion1) {
            object[] results = this.Invoke("GenerarReporteExcel", new object[] {
                        NombrePlantilla,
                        Informacion1});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void GenerarReporteExcelAsync(string NombrePlantilla, string Informacion1) {
            this.GenerarReporteExcelAsync(NombrePlantilla, Informacion1, null);
        }
        
        /// <remarks/>
        public void GenerarReporteExcelAsync(string NombrePlantilla, string Informacion1, object userState) {
            if ((this.GenerarReporteExcelOperationCompleted == null)) {
                this.GenerarReporteExcelOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGenerarReporteExcelOperationCompleted);
            }
            this.InvokeAsync("GenerarReporteExcel", new object[] {
                        NombrePlantilla,
                        Informacion1}, this.GenerarReporteExcelOperationCompleted, userState);
        }
        
        private void OnGenerarReporteExcelOperationCompleted(object arg) {
            if ((this.GenerarReporteExcelCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GenerarReporteExcelCompleted(this, new GenerarReporteExcelCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/GenerarReporte", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] GenerarReporte(System.Data.DataSet InformacionReporteWeb, System.Data.DataSet InformacionCliente) {
            object[] results = this.Invoke("GenerarReporte", new object[] {
                        InformacionReporteWeb,
                        InformacionCliente});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void GenerarReporteAsync(System.Data.DataSet InformacionReporteWeb, System.Data.DataSet InformacionCliente) {
            this.GenerarReporteAsync(InformacionReporteWeb, InformacionCliente, null);
        }
        
        /// <remarks/>
        public void GenerarReporteAsync(System.Data.DataSet InformacionReporteWeb, System.Data.DataSet InformacionCliente, object userState) {
            if ((this.GenerarReporteOperationCompleted == null)) {
                this.GenerarReporteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGenerarReporteOperationCompleted);
            }
            this.InvokeAsync("GenerarReporte", new object[] {
                        InformacionReporteWeb,
                        InformacionCliente}, this.GenerarReporteOperationCompleted, userState);
        }
        
        private void OnGenerarReporteOperationCompleted(object arg) {
            if ((this.GenerarReporteCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GenerarReporteCompleted(this, new GenerarReporteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/TestEnlace", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string TestEnlace(string Datos) {
            object[] results = this.Invoke("TestEnlace", new object[] {
                        Datos});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void TestEnlaceAsync(string Datos) {
            this.TestEnlaceAsync(Datos, null);
        }
        
        /// <remarks/>
        public void TestEnlaceAsync(string Datos, object userState) {
            if ((this.TestEnlaceOperationCompleted == null)) {
                this.TestEnlaceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTestEnlaceOperationCompleted);
            }
            this.InvokeAsync("TestEnlace", new object[] {
                        Datos}, this.TestEnlaceOperationCompleted, userState);
        }
        
        private void OnTestEnlaceOperationCompleted(object arg) {
            if ((this.TestEnlaceCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TestEnlaceCompleted(this, new TestEnlaceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/GetReceta", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetReceta(string IdEstado, string CLUES, string Consecutivo) {
            object[] results = this.Invoke("GetReceta", new object[] {
                        IdEstado,
                        CLUES,
                        Consecutivo});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetRecetaAsync(string IdEstado, string CLUES, string Consecutivo) {
            this.GetRecetaAsync(IdEstado, CLUES, Consecutivo, null);
        }
        
        /// <remarks/>
        public void GetRecetaAsync(string IdEstado, string CLUES, string Consecutivo, object userState) {
            if ((this.GetRecetaOperationCompleted == null)) {
                this.GetRecetaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetRecetaOperationCompleted);
            }
            this.InvokeAsync("GetReceta", new object[] {
                        IdEstado,
                        CLUES,
                        Consecutivo}, this.GetRecetaOperationCompleted, userState);
        }
        
        private void OnGetRecetaOperationCompleted(object arg) {
            if ((this.GetRecetaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetRecetaCompleted(this, new GetRecetaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/GetRecetaInformacion", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetRecetaInformacion(string IdEstado, string CLUES, string Consecutivo) {
            object[] results = this.Invoke("GetRecetaInformacion", new object[] {
                        IdEstado,
                        CLUES,
                        Consecutivo});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetRecetaInformacionAsync(string IdEstado, string CLUES, string Consecutivo) {
            this.GetRecetaInformacionAsync(IdEstado, CLUES, Consecutivo, null);
        }
        
        /// <remarks/>
        public void GetRecetaInformacionAsync(string IdEstado, string CLUES, string Consecutivo, object userState) {
            if ((this.GetRecetaInformacionOperationCompleted == null)) {
                this.GetRecetaInformacionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetRecetaInformacionOperationCompleted);
            }
            this.InvokeAsync("GetRecetaInformacion", new object[] {
                        IdEstado,
                        CLUES,
                        Consecutivo}, this.GetRecetaInformacionOperationCompleted, userState);
        }
        
        private void OnGetRecetaInformacionOperationCompleted(object arg) {
            if ((this.GetRecetaInformacionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetRecetaInformacionCompleted(this, new GetRecetaInformacionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void InformacionCompletedEventHandler(object sender, InformacionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class InformacionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal InformacionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void ExecuteCompletedEventHandler(object sender, ExecuteCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExecuteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ExecuteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void ExecuteExtendedCompletedEventHandler(object sender, ExecuteExtendedCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExecuteExtendedCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ExecuteExtendedCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void GenerarReporteExcelCompletedEventHandler(object sender, GenerarReporteExcelCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GenerarReporteExcelCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GenerarReporteExcelCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void GenerarReporteCompletedEventHandler(object sender, GenerarReporteCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GenerarReporteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GenerarReporteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void TestEnlaceCompletedEventHandler(object sender, TestEnlaceCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TestEnlaceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TestEnlaceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetRecetaCompletedEventHandler(object sender, GetRecetaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetRecetaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetRecetaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void GetRecetaInformacionCompletedEventHandler(object sender, GetRecetaInformacionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetRecetaInformacionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetRecetaInformacionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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