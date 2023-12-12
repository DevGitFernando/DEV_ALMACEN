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

namespace Dll_INT_EPharma.wsEPharmaInformacionDeVales {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="wsEPharma_RecepcionValesSoap", Namespace="http://SC-Solutions/ServiciosWeb/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ItemPersona))]
    public partial class wsEPharma_RecepcionVales : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback EnviarInformacionDeValeOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsEPharma_RecepcionVales() {
            this.Url = global::Dll_INT_EPharma.Properties.Settings.Default.Dll_INT_EPharma_wsEPharmaInformacionDeVales_wsEPharma_RecepcionVales;
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
        public event EnviarInformacionDeValeCompletedEventHandler EnviarInformacionDeValeCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SC-Solutions/ServiciosWeb/EnviarInformacionDeVale", RequestNamespace="http://SC-Solutions/ServiciosWeb/", ResponseNamespace="http://SC-Solutions/ServiciosWeb/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ResponseSolicitud EnviarInformacionDeVale(ValesRecepcionRegistrarInformacion Solicitud) {
            object[] results = this.Invoke("EnviarInformacionDeVale", new object[] {
                        Solicitud});
            return ((ResponseSolicitud)(results[0]));
        }
        
        /// <remarks/>
        public void EnviarInformacionDeValeAsync(ValesRecepcionRegistrarInformacion Solicitud) {
            this.EnviarInformacionDeValeAsync(Solicitud, null);
        }
        
        /// <remarks/>
        public void EnviarInformacionDeValeAsync(ValesRecepcionRegistrarInformacion Solicitud, object userState) {
            if ((this.EnviarInformacionDeValeOperationCompleted == null)) {
                this.EnviarInformacionDeValeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnviarInformacionDeValeOperationCompleted);
            }
            this.InvokeAsync("EnviarInformacionDeVale", new object[] {
                        Solicitud}, this.EnviarInformacionDeValeOperationCompleted, userState);
        }
        
        private void OnEnviarInformacionDeValeOperationCompleted(object arg) {
            if ((this.EnviarInformacionDeValeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnviarInformacionDeValeCompleted(this, new EnviarInformacionDeValeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://SC-Solutions/ServiciosWeb/")]
    public partial class ValesRecepcionRegistrarInformacion {
        
        private string idSocioComercialField;
        
        private string idSucursalField;
        
        private string folio_ValeField;
        
        private System.DateTime fechaEmision_ValeField;
        
        private string idEmpresaField;
        
        private string idEstadoField;
        
        private string idFarmaciaField;
        
        private string idPersonalField;
        
        private string idTipoDeDispensacionField;
        
        private string numRecetaField;
        
        private System.DateTime fechaRecetaField;
        
        private string idBeneficioField;
        
        private string idDiagnosticoField;
        
        private string refObservacionesField;
        
        private ItemBeneficiario beneficiarioField;
        
        private ItemMedico medicoField;
        
        private ItemInsumo[] insumosField;
        
        /// <comentarios/>
        public string IdSocioComercial {
            get {
                return this.idSocioComercialField;
            }
            set {
                this.idSocioComercialField = value;
            }
        }
        
        /// <comentarios/>
        public string IdSucursal {
            get {
                return this.idSucursalField;
            }
            set {
                this.idSucursalField = value;
            }
        }
        
        /// <comentarios/>
        public string Folio_Vale {
            get {
                return this.folio_ValeField;
            }
            set {
                this.folio_ValeField = value;
            }
        }
        
        /// <comentarios/>
        public System.DateTime FechaEmision_Vale {
            get {
                return this.fechaEmision_ValeField;
            }
            set {
                this.fechaEmision_ValeField = value;
            }
        }
        
        /// <comentarios/>
        public string IdEmpresa {
            get {
                return this.idEmpresaField;
            }
            set {
                this.idEmpresaField = value;
            }
        }
        
        /// <comentarios/>
        public string IdEstado {
            get {
                return this.idEstadoField;
            }
            set {
                this.idEstadoField = value;
            }
        }
        
        /// <comentarios/>
        public string IdFarmacia {
            get {
                return this.idFarmaciaField;
            }
            set {
                this.idFarmaciaField = value;
            }
        }
        
        /// <comentarios/>
        public string IdPersonal {
            get {
                return this.idPersonalField;
            }
            set {
                this.idPersonalField = value;
            }
        }
        
        /// <comentarios/>
        public string IdTipoDeDispensacion {
            get {
                return this.idTipoDeDispensacionField;
            }
            set {
                this.idTipoDeDispensacionField = value;
            }
        }
        
        /// <comentarios/>
        public string NumReceta {
            get {
                return this.numRecetaField;
            }
            set {
                this.numRecetaField = value;
            }
        }
        
        /// <comentarios/>
        public System.DateTime FechaReceta {
            get {
                return this.fechaRecetaField;
            }
            set {
                this.fechaRecetaField = value;
            }
        }
        
        /// <comentarios/>
        public string IdBeneficio {
            get {
                return this.idBeneficioField;
            }
            set {
                this.idBeneficioField = value;
            }
        }
        
        /// <comentarios/>
        public string IdDiagnostico {
            get {
                return this.idDiagnosticoField;
            }
            set {
                this.idDiagnosticoField = value;
            }
        }
        
        /// <comentarios/>
        public string RefObservaciones {
            get {
                return this.refObservacionesField;
            }
            set {
                this.refObservacionesField = value;
            }
        }
        
        /// <comentarios/>
        public ItemBeneficiario Beneficiario {
            get {
                return this.beneficiarioField;
            }
            set {
                this.beneficiarioField = value;
            }
        }
        
        /// <comentarios/>
        public ItemMedico Medico {
            get {
                return this.medicoField;
            }
            set {
                this.medicoField = value;
            }
        }
        
        /// <comentarios/>
        public ItemInsumo[] Insumos {
            get {
                return this.insumosField;
            }
            set {
                this.insumosField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://SC-Solutions/ServiciosWeb/")]
    public partial class ItemBeneficiario : ItemPersona {
    }
    
    /// <comentarios/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ItemMedico))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ItemBeneficiario))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://SC-Solutions/ServiciosWeb/")]
    public partial class ItemPersona {
        
        private string nombreField;
        
        private string apPaternoField;
        
        private string apMaternoField;
        
        private string referenciaField;
        
        private string direccionField;
        
        private string telefonoField;
        
        private string sexoField;
        
        private System.DateTime fechaNacimientoField;
        
        private System.DateTime fechaFinVigenciaField;
        
        /// <comentarios/>
        public string Nombre {
            get {
                return this.nombreField;
            }
            set {
                this.nombreField = value;
            }
        }
        
        /// <comentarios/>
        public string ApPaterno {
            get {
                return this.apPaternoField;
            }
            set {
                this.apPaternoField = value;
            }
        }
        
        /// <comentarios/>
        public string ApMaterno {
            get {
                return this.apMaternoField;
            }
            set {
                this.apMaternoField = value;
            }
        }
        
        /// <comentarios/>
        public string Referencia {
            get {
                return this.referenciaField;
            }
            set {
                this.referenciaField = value;
            }
        }
        
        /// <comentarios/>
        public string Direccion {
            get {
                return this.direccionField;
            }
            set {
                this.direccionField = value;
            }
        }
        
        /// <comentarios/>
        public string Telefono {
            get {
                return this.telefonoField;
            }
            set {
                this.telefonoField = value;
            }
        }
        
        /// <comentarios/>
        public string Sexo {
            get {
                return this.sexoField;
            }
            set {
                this.sexoField = value;
            }
        }
        
        /// <comentarios/>
        public System.DateTime FechaNacimiento {
            get {
                return this.fechaNacimientoField;
            }
            set {
                this.fechaNacimientoField = value;
            }
        }
        
        /// <comentarios/>
        public System.DateTime FechaFinVigencia {
            get {
                return this.fechaFinVigenciaField;
            }
            set {
                this.fechaFinVigenciaField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://SC-Solutions/ServiciosWeb/")]
    public partial class ResponseSolicitud {
        
        private bool errorField;
        
        private int estatusField;
        
        private string mensajeField;
        
        /// <comentarios/>
        public bool Error {
            get {
                return this.errorField;
            }
            set {
                this.errorField = value;
            }
        }
        
        /// <comentarios/>
        public int Estatus {
            get {
                return this.estatusField;
            }
            set {
                this.estatusField = value;
            }
        }
        
        /// <comentarios/>
        public string Mensaje {
            get {
                return this.mensajeField;
            }
            set {
                this.mensajeField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://SC-Solutions/ServiciosWeb/")]
    public partial class ItemInsumo {
        
        private string claveInsumoField;
        
        private string descripcionField;
        
        private int piezasField;
        
        /// <comentarios/>
        public string ClaveInsumo {
            get {
                return this.claveInsumoField;
            }
            set {
                this.claveInsumoField = value;
            }
        }
        
        /// <comentarios/>
        public string Descripcion {
            get {
                return this.descripcionField;
            }
            set {
                this.descripcionField = value;
            }
        }
        
        /// <comentarios/>
        public int Piezas {
            get {
                return this.piezasField;
            }
            set {
                this.piezasField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://SC-Solutions/ServiciosWeb/")]
    public partial class ItemMedico : ItemPersona {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void EnviarInformacionDeValeCompletedEventHandler(object sender, EnviarInformacionDeValeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EnviarInformacionDeValeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EnviarInformacionDeValeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ResponseSolicitud Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ResponseSolicitud)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591