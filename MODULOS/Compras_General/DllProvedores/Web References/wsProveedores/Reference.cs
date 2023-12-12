﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.269
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.269.
// 
#pragma warning disable 1591

namespace DllProveedores.wsProveedores {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="wsCnnProveedoresSoap", Namespace="http://SC-Solutions/ServiciosWeb/")]
    public partial class wsCnnProveedores : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ExecuteExtOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetExecuteOperationCompleted;
        
        private System.Threading.SendOrPostCallback ReporteOperationCompleted;
        
        private System.Threading.SendOrPostCallback ProbarConexionOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConfirmarPedidoProveedorOperationCompleted;
        
        private System.Threading.SendOrPostCallback EmbarcarOrdenCompraOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsCnnProveedores() {
            this.Url = global::DllProveedores.Properties.Settings.Default.DllProveedores_wsProveedores_wsCnnProveedores;
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
        public event ExecuteExtCompletedEventHandler ExecuteExtCompleted;
        
        /// <remarks/>
        public event GetExecuteCompletedEventHandler GetExecuteCompleted;
        
        /// <remarks/>
        public event ReporteCompletedEventHandler ReporteCompleted;
        
        /// <remarks/>
        public event ProbarConexionCompletedEventHandler ProbarConexionCompleted;
        
        /// <remarks/>
        public event ConfirmarPedidoProveedorCompletedEventHandler ConfirmarPedidoProveedorCompleted;
        
        /// <remarks/>
        public event EmbarcarOrdenCompraCompletedEventHandler EmbarcarOrdenCompraCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/ExecuteExt", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet ExecuteExt(System.Data.DataSet InformacionCliente, string Solicitud, string Sentencia) {
            object[] results = this.Invoke("ExecuteExt", new object[] {
                        InformacionCliente,
                        Solicitud,
                        Sentencia});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void ExecuteExtAsync(System.Data.DataSet InformacionCliente, string Solicitud, string Sentencia) {
            this.ExecuteExtAsync(InformacionCliente, Solicitud, Sentencia, null);
        }
        
        /// <remarks/>
        public void ExecuteExtAsync(System.Data.DataSet InformacionCliente, string Solicitud, string Sentencia, object userState) {
            if ((this.ExecuteExtOperationCompleted == null)) {
                this.ExecuteExtOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExecuteExtOperationCompleted);
            }
            this.InvokeAsync("ExecuteExt", new object[] {
                        InformacionCliente,
                        Solicitud,
                        Sentencia}, this.ExecuteExtOperationCompleted, userState);
        }
        
        private void OnExecuteExtOperationCompleted(object arg) {
            if ((this.ExecuteExtCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ExecuteExtCompleted(this, new ExecuteExtCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/GetExecute", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetExecute(System.Data.DataSet InformacionCliente, string Solicitud) {
            object[] results = this.Invoke("GetExecute", new object[] {
                        InformacionCliente,
                        Solicitud});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetExecuteAsync(System.Data.DataSet InformacionCliente, string Solicitud) {
            this.GetExecuteAsync(InformacionCliente, Solicitud, null);
        }
        
        /// <remarks/>
        public void GetExecuteAsync(System.Data.DataSet InformacionCliente, string Solicitud, object userState) {
            if ((this.GetExecuteOperationCompleted == null)) {
                this.GetExecuteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetExecuteOperationCompleted);
            }
            this.InvokeAsync("GetExecute", new object[] {
                        InformacionCliente,
                        Solicitud}, this.GetExecuteOperationCompleted, userState);
        }
        
        private void OnGetExecuteOperationCompleted(object arg) {
            if ((this.GetExecuteCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetExecuteCompleted(this, new GetExecuteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/Reporte", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] Reporte(System.Data.DataSet InformacionReporteWeb, System.Data.DataSet InformacionCliente) {
            object[] results = this.Invoke("Reporte", new object[] {
                        InformacionReporteWeb,
                        InformacionCliente});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void ReporteAsync(System.Data.DataSet InformacionReporteWeb, System.Data.DataSet InformacionCliente) {
            this.ReporteAsync(InformacionReporteWeb, InformacionCliente, null);
        }
        
        /// <remarks/>
        public void ReporteAsync(System.Data.DataSet InformacionReporteWeb, System.Data.DataSet InformacionCliente, object userState) {
            if ((this.ReporteOperationCompleted == null)) {
                this.ReporteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReporteOperationCompleted);
            }
            this.InvokeAsync("Reporte", new object[] {
                        InformacionReporteWeb,
                        InformacionCliente}, this.ReporteOperationCompleted, userState);
        }
        
        private void OnReporteOperationCompleted(object arg) {
            if ((this.ReporteCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReporteCompleted(this, new ReporteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/ProbarConexion", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ProbarConexion(string ArchivoIni) {
            object[] results = this.Invoke("ProbarConexion", new object[] {
                        ArchivoIni});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ProbarConexionAsync(string ArchivoIni) {
            this.ProbarConexionAsync(ArchivoIni, null);
        }
        
        /// <remarks/>
        public void ProbarConexionAsync(string ArchivoIni, object userState) {
            if ((this.ProbarConexionOperationCompleted == null)) {
                this.ProbarConexionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnProbarConexionOperationCompleted);
            }
            this.InvokeAsync("ProbarConexion", new object[] {
                        ArchivoIni}, this.ProbarConexionOperationCompleted, userState);
        }
        
        private void OnProbarConexionOperationCompleted(object arg) {
            if ((this.ProbarConexionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ProbarConexionCompleted(this, new ProbarConexionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/ConfirmarPedidoProveedor", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet ConfirmarPedidoProveedor(System.Data.DataSet InformacionCliente, System.Data.DataSet dtsInformacionWeb, int iTipo) {
            object[] results = this.Invoke("ConfirmarPedidoProveedor", new object[] {
                        InformacionCliente,
                        dtsInformacionWeb,
                        iTipo});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void ConfirmarPedidoProveedorAsync(System.Data.DataSet InformacionCliente, System.Data.DataSet dtsInformacionWeb, int iTipo) {
            this.ConfirmarPedidoProveedorAsync(InformacionCliente, dtsInformacionWeb, iTipo, null);
        }
        
        /// <remarks/>
        public void ConfirmarPedidoProveedorAsync(System.Data.DataSet InformacionCliente, System.Data.DataSet dtsInformacionWeb, int iTipo, object userState) {
            if ((this.ConfirmarPedidoProveedorOperationCompleted == null)) {
                this.ConfirmarPedidoProveedorOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConfirmarPedidoProveedorOperationCompleted);
            }
            this.InvokeAsync("ConfirmarPedidoProveedor", new object[] {
                        InformacionCliente,
                        dtsInformacionWeb,
                        iTipo}, this.ConfirmarPedidoProveedorOperationCompleted, userState);
        }
        
        private void OnConfirmarPedidoProveedorOperationCompleted(object arg) {
            if ((this.ConfirmarPedidoProveedorCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConfirmarPedidoProveedorCompleted(this, new ConfirmarPedidoProveedorCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/EmbarcarOrdenCompra", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet EmbarcarOrdenCompra(System.Data.DataSet InformacionCliente, System.Data.DataSet dtsInformacionWeb) {
            object[] results = this.Invoke("EmbarcarOrdenCompra", new object[] {
                        InformacionCliente,
                        dtsInformacionWeb});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void EmbarcarOrdenCompraAsync(System.Data.DataSet InformacionCliente, System.Data.DataSet dtsInformacionWeb) {
            this.EmbarcarOrdenCompraAsync(InformacionCliente, dtsInformacionWeb, null);
        }
        
        /// <remarks/>
        public void EmbarcarOrdenCompraAsync(System.Data.DataSet InformacionCliente, System.Data.DataSet dtsInformacionWeb, object userState) {
            if ((this.EmbarcarOrdenCompraOperationCompleted == null)) {
                this.EmbarcarOrdenCompraOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEmbarcarOrdenCompraOperationCompleted);
            }
            this.InvokeAsync("EmbarcarOrdenCompra", new object[] {
                        InformacionCliente,
                        dtsInformacionWeb}, this.EmbarcarOrdenCompraOperationCompleted, userState);
        }
        
        private void OnEmbarcarOrdenCompraOperationCompleted(object arg) {
            if ((this.EmbarcarOrdenCompraCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EmbarcarOrdenCompraCompleted(this, new EmbarcarOrdenCompraCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void ExecuteExtCompletedEventHandler(object sender, ExecuteExtCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExecuteExtCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ExecuteExtCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetExecuteCompletedEventHandler(object sender, GetExecuteCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetExecuteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetExecuteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void ReporteCompletedEventHandler(object sender, ReporteCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReporteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ReporteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void ProbarConexionCompletedEventHandler(object sender, ProbarConexionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ProbarConexionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ProbarConexionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void ConfirmarPedidoProveedorCompletedEventHandler(object sender, ConfirmarPedidoProveedorCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConfirmarPedidoProveedorCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConfirmarPedidoProveedorCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void EmbarcarOrdenCompraCompletedEventHandler(object sender, EmbarcarOrdenCompraCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EmbarcarOrdenCompraCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EmbarcarOrdenCompraCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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