#region USING
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Threading;
using System.ServiceModel;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;
using Dll_IFacturacion.Configuracion;
#endregion USING

using Dll_IFacturacion.CFDI;
using Dll_IFacturacion.CFDI.CFDFunctions;
using Dll_IFacturacion.CFDI.CFDFunctionsEx;
using Dll_IFacturacion.CFDI.geCFD; 

namespace Dll_IFacturacion.XSA
{
    public partial class FrmCFDI_Anticipos : FrmBaseExt
    {
        #region Declaracion de Variables
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnFacturar = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerFacturar;
        clsListView list;
        clsAyudas ayuda;
        clsConsultas consulta;

        clsFormaMetodoPago formaMetodoPago;
        DataSet dtsUnidadesDeMedida = new DataSet();
        DataSet dtsListaImpuestos = new DataSet(); 

        private bool bProcesando = false; 
        private string sSerieDocumento = "";
        private string sFolioDocumento = "";
        private string sIdCFDI = "";
        private string sNombreReceptor = ""; 

        private string sIdEmpresa = DtGeneral.EmpresaConectada;
        private string sIdEstado = DtGeneral.EstadoConectado;
        private string sIdFarmacia = DtGeneral.FarmaciaConectada;
        private string sFolioRemision = "";
        private bool bInformacionEmisor = false;

        //Thread thRemision;
        Thread thFacturaElectronica;
        cfdTipoDePlantilla tipoDocumento = cfdTipoDePlantilla.FAC;
        string sTipoDoctoFactura = "001"; 

        string sFormato = "###,###,###,###,##0.#0";
        string sFormatoIva = "###,###,###,###,##0.#0";
        #endregion Declaracion de Variables

        #region Declaracion de Variables de Facturacion 
        xsaCFDI cfdi;
        string xsaDireccionServicioTimbrado = "";
        string sRFC = "";
        string sRFC_Receptor = ""; 
        string sKey = "";
        string sNombreEmpresa_o_Sucursal = "";
        string sNombreDocumento = ""; 
        string sPlantillaDocumento = "";
        //string sTipoDoctoFactura = "001"; 

        string sTipoDeDocumento = ""; 
        bool bErrorAlGenerarFolio = false;
        bool bDocumentoElectronico_Generado = false; 

        double dSubTotal = 0;
        double dTasaIva = 0; 
        double dIva = 0;
        double dTotal = 0;
        double dImporteACobrar = 0;
        string sObservacionesDocumento = "";
        string sObservacionesCondicionesDePago = "";
        string sObservacionesPago = "";

        bool VistaPrevia = false;
        long PK = 0;
        string sReferencia = "";
        string sObservaciones_01 = "";
        string sObservaciones_02 = "";
        string sObservaciones_03 = "";
        int iTipoDocumento = 0;
        int iTipoInsumo = 0;
        string sRubroFinanciamiento = "";
        string sFuenteFinanciamiento = "";
        bool bErrorEnImportes = false; 

        string sXMLFormaPago = "PAGO EN UNA SOLA EXHIBICIÓN";
        string sXMLCondicionesPago = "Crédito";
        string sXMLMetodoPago = "No identificado";
        string sXMLObservacionesPago = ""; 
        PACs_Timbrado tpPAC = PACs_Timbrado.PAX;
        bool bImplementa_ClaveSSA_Base___Identificador = DtIFacturacion.ImplementaClaveSSA_Base__Identificador;

        clsInfoEmisor infoEmisor = null; //new clsInfoEmisor();
        Dll_IFacturacion.CFDI.CFDFunctions.cMain CFDFunct = null; //new CFDFunctions.cMain();
        clsGenCFD CFD = null; //new clsGenCFD();      
        clsGenCFDI_Ex CFDI_FD = null; //new clsGenCFDI_Ex();

        DataSet dtsSeries = new DataSet(); 
        #endregion Declaracion de Variables de Facturacion

        public FrmCFDI_Anticipos()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            this.sIdEmpresa = IdEmpresa;
            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
            this.sFolioRemision = ""; //FolioRemision;

            // Inicializar el CFDI 
            cfdi = new xsaCFDI(IdEmpresa, IdEstado, IdFarmacia);

            // Inicializar el CFDI 
            cfdi = new xsaCFDI(IdEmpresa, IdEstado, IdFarmacia);
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            cnnFacturar.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnnFacturar.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;


            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            consulta = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);


            //list = new clsListView(listvClaves);
            tpPAC = DtIFacturacion.PAC_Informacion.PAC;
        }

        #region Propiedades
        public string IdEmpresa
        {
            get { return sIdEmpresa; }
        }

        public string IdEstado
        {
            get { return sIdEstado; }
        }

        public string IdFarmacia
        {
            get { return sIdFarmacia; }
        }

        public bool DocumentoGenerado
        {
            get { return bDocumentoElectronico_Generado; }
        }

        public string Serie
        {
            get { return sSerieDocumento; }
        }

        public string Folio
        {
            get { return sFolioDocumento; }
        }

        public string FolioDocumentoElectronico
        {
            get { return (sSerieDocumento + " - " + sFolioDocumento); }
        }
        #endregion Propiedades 

        private void FrmCFDI_Anticipos_Load(object sender, EventArgs e)
        {

        }
    }
}
