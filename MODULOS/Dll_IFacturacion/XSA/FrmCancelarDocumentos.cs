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
using System.Net; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;
using Dll_IFacturacion.Configuracion;
using Dll_IFacturacion.CFDI.Timbrar; 

#endregion USING

#region USING WEB SERVICES
using Dll_IFacturacion.xsasvrCancelCFDService;
using Dll_IFacturacion.xsasvrCFDService;
using Dll_IFacturacion.xsasvrFileReceiverService;
#endregion USING WEB SERVICES 


namespace Dll_IFacturacion.XSA
{
    internal partial class FrmCancelarDocumentos : FrmBaseExt 
    {       
        public bool ExisteDocumento = false; 
        private string sDocumento = "";
        //HttpRequest http;
        WebClient web;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer; 

        xsawsCancelarDocumento cancelarDocto;
        string xsaDireccionServicioTimbrado = ""; 
        string sRFC = "";
        string sKey = "";
        string sSerie = "";
        string sFolio = "";
        string sUUID = "";
        string sRFC_Receptor = ""; 
        double dImporte = 0; 
        string sMotivoCancelacion = ""; 
        public bool CancelacionExito = false;
        Thread thCancelarDocto;
        PACs_Timbrado tpPAC = PACs_Timbrado.Tralix;

        string sClaveMotivoCancelacion_SAT = "";
        string sUUID_RelacionadoCancelacion = "";


        public FrmCancelarDocumentos(string Url, string Key, string RFC, string Serie, string Folio, 
            string ClaveMotivoCancelacion_SAT, string UUID_RelacionadoCancelacion, 
            string MotivoCancelacion, PACs_Timbrado PAC, 
            string UUID, string RFC_Receptor, double Importe)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            xsaDireccionServicioTimbrado = Url;
            sRFC = RFC;
            sKey = Key;
            sSerie = Serie;
            sFolio = Folio;
            tpPAC = PAC; 
            sUUID = UUID;
            sRFC_Receptor = RFC_Receptor; 
            dImporte = Importe; 
            sMotivoCancelacion = MotivoCancelacion;

            sClaveMotivoCancelacion_SAT = ClaveMotivoCancelacion_SAT;
            sUUID_RelacionadoCancelacion = UUID_RelacionadoCancelacion; 

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);

            this.Text += "  " + sSerie + "-" + sFolio;

            switch(tpPAC)
            {
                case PACs_Timbrado.Tralix:
                    cancelarDocto = new xsawsCancelarDocumento(xsaDireccionServicioTimbrado, General.DatosConexion);
                    break;

                default:
                    break;  
            }
        } 

        private void FrmCancelarDocumentos_Load(object sender, EventArgs e)
        {
            tmDescarga.Interval = 1000; 
            tmDescarga.Enabled = true;
            tmDescarga.Start(); 
        }

        private void tmDescarga_Tick(object sender, EventArgs e)
        {
            bool bRegresa = false; 
            tmDescarga.Stop();
            tmDescarga.Enabled = false;

            thCancelarDocto = new Thread(thCancelarDocumento);
            thCancelarDocto.Name = "Cancelando documento electrónico";
            thCancelarDocto.Start(); 
        }

        private void thCancelarDocumento()
        {
            bool bRegresa = false;

            try
            {
                switch(tpPAC)
                {
                    case PACs_Timbrado.Tralix: 
                        ////// xsawsCancelarDocumento cancelarDocto = new xsawsCancelarDocumento(xsaDireccionServicioTimbrado, General.DatosConexion);
                        bRegresa = cancelarDocto.CancelarDocumento(sKey, sRFC, sSerie, sFolio);
                    break;

                    default:
                        clsTimbrar.PAC = tpPAC;
                        clsTimbrar.Usuario = sRFC;
                        clsTimbrar.Password = sKey;
                        clsTimbrar.Url = xsaDireccionServicioTimbrado; 

                        bRegresa = clsTimbrar.CancelarCFDI(sRFC_Receptor, dImporte, sUUID, sClaveMotivoCancelacion_SAT, sUUID_RelacionadoCancelacion);
                        break;
                }

                if (bRegresa)
                {
                    bRegresa = GrabarCancelacion();
                }
            }
            catch { }

            CancelacionExito = bRegresa;

            this.Hide(); 
        }

        private bool GrabarCancelacion()
        {
            bool bRegresa = true;
            string sSql = string.Format("Update F Set Status = 'C', FechaCancelacion = getdate(), " + 
                " IdPersonalCancela = '{5}', MotivoCancelacion = '{6}' \n" +
                "From FACT_CFD_Documentos_Generados f (nolock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' \n\n",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sSerie, sFolio.Replace(",", ""), 
                DtGeneral.IdPersonal, sMotivoCancelacion); 

            ////if (!leer.Exec(sSql)) 
            ////{
            ////    bRegresa = false;
            ////    Error.GrabarError(leer, "GrabarCancelacion()"); 
            ////}
            ////else 
            {
                if (tpPAC != PACs_Timbrado.Tralix)
                {
                    sSql += string.Format("Update F Set uf_CanceladoSAT = 1, uf_ackCancelacion_SAT = '{1}' \n" +
                    "From FACT_CFDI_XML F (NoLock) \n" +
                    "Where uf_FolioSAT = '{0}' \n\n", sUUID, Fg.ConvertirStringB64FromString(clsTimbrar.XmlAcuseCancelacion));


                    sSql += string.Format("Exec spp_FACT_Cancelar_Factura___HabilitarRemisiones " +
                        " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Serie = '{3}', @FolioFacturaElectronica = '{4}' ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                        sSerie, sFolio.Replace(",", ""));

                    //sSql += string.Format("Exec spp_INT_MA__FACT_Cancelar_Factura___HabilitarRemisiones " +
                    //    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Serie = '{3}', @FolioFacturaElectronica = '{4}' \n\n ",
                    //    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                    //    sSerie, sFolio.Replace(",", ""));
                }

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leer, "GrabarCancelacion()");
                } 
            }

            return bRegresa; 
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
        }

        private void webView_FileDownload(object sender, EventArgs e)
        {
        }
    }
}
