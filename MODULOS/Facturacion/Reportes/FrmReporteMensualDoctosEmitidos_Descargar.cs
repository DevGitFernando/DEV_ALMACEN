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

using Dll_IFacturacion; 
using DllFarmaciaSoft;
using Dll_IFacturacion.Configuracion;

using Dll_IFacturacion.CFDI.Timbrar; 
#endregion USING

#region USING WEB SERVICES
using Dll_IFacturacion.xsasvrCancelCFDService;
using Dll_IFacturacion.xsasvrCFDService;
using Dll_IFacturacion.xsasvrFileReceiverService;
#endregion USING WEB SERVICES 


namespace Facturacion 
{
    internal partial class FrmReporteMensualDoctosEmitidos_Descargar : FrmBaseExt 
    {
        string sUrl = "";
        public bool ExisteDocumento = false; 
        private string sDocumento = "";
        //HttpRequest http;
        WebClient web;

        Thread thDescarga;

        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        bool bGenerarReporteMensualGeneral = false;
        string sAño = "";
        string sMes = "";
        string sRutaDeDescarga = "";
        bool bDescargarPDF = false; 
        clsConexionSQL cnn;

        public FrmReporteMensualDoctosEmitidos_Descargar(string IdEmpresa, string IdEstado, string IdFarmacia,
            bool GenerarReporteMensualGeneral, string Año, string Mes, string RutaDeDocumentos):
            this(IdEmpresa, IdEstado, IdFarmacia, GenerarReporteMensualGeneral,Año, Mes, RutaDeDocumentos, false) 
        {
        }

        public FrmReporteMensualDoctosEmitidos_Descargar(string IdEmpresa, string IdEstado, string IdFarmacia, 
            bool GenerarReporteMensualGeneral, string Año, string Mes, string RutaDeDocumentos, bool DescargarPDF)
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            sIdEmpresa = IdEmpresa;
            sIdEstado = IdEstado;
            sIdFarmacia = IdFarmacia;
            bGenerarReporteMensualGeneral = GenerarReporteMensualGeneral;
            sAño = Año;
            sMes = Mes;
            sRutaDeDescarga = RutaDeDocumentos + string.Format(@"\{0}_{1}", Fg.PonCeros(sAño, 4), Fg.PonCeros(sMes,2));
            bDescargarPDF = DescargarPDF; 

            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);

            this.ControlBox = DtGeneral.EsAdministrador; 
        }

        private void FrmReporteMensualDoctosEmitidos_Descargar_Load(object sender, EventArgs e)
        {
            lblAvance.Text = "Descargando información";
            tmDescarga.Interval = 1000;
            tmDescarga.Enabled = true;
            tmDescarga.Start(); 
        }

        private void tmDescarga_Tick(object sender, EventArgs e)
        {
            tmDescarga.Stop();
            tmDescarga.Enabled = false;

            thDescarga = new Thread(DescargarDocumentos);
            thDescarga.Name = "Descargando_Documentos";
            thDescarga.Start(); 
        }

        private void DescargarDocumentos()
        {
            bool bRegresa = false;

            cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer;
            clsLeer leerDescarga;
            clsLeer leerFolios;
            string sSql = "";
            string sFiltro = "";
            string sFiltroPeriodo = "";
            string sFile = "";
            string sFile_PDF = "";
            string sFileContenido = ""; 
            int iRow = 1;
            int iRegistros = 0;
            int iKeyx_Inicial = 0;
            int iKeyx = 0;
            int iKeyx_Docto = 0; 

            string sIdEmpresa = "";
            string sIdEstado = "";
            string sIdFarmacia = "";
            string sSerie = "";
            int iFolio = 0;
            int iDescargarPDF = bDescargarPDF ? 1 : 0;
            bool bEsPAC = false;
            string sFileContenido_PDF = "";
            bool bArchivoGenerado = false; 

            sSql = string.Format("Exec spp_Rpt_FACT_Documentos_Emitidos @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " +
                " @Año = {3}, @Mes = {4}, @EsGeneral = {5} ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sAño, sMes, 
                Convert.ToInt32(bGenerarReporteMensualGeneral));

            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 
            leer = new clsLeer(ref cnn); 
            leerDescarga = new clsLeer(ref cnn);
            leerFolios = new clsLeer(ref cnn); 

            DtIFacturacion.CrearDirectorio(sRutaDeDescarga);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "DescargarDocumentos()");
                General.msjError("Ocurrió un error al descargar la información solicitada.");
            }
            else
            {
                leer.Leer(); 
                iRegistros = leer.CampoInt("TotalFolios"); 
                //lblAvance.Text = string.Format("Descargando {0} de {1}", iRow, iRegistros);

                leerDescarga.DataTableClase = leer.Tabla(2); 
                while(leerDescarga.Leer()) 
                {
                    sFiltro = leerDescarga.Campo("Folios"); 
                    sSql = string.Format(
                    "Select    \n " +
                    " F.FechaRegistro, \n " +
                    " F.IdEmpresa, F.IdEstado, f.IdFarmacia, F.RFC,    \n " +
                    " (F.Serie + '___' + cast(F.Folio as varchar)  + '___' + F.RFC ) as NombreDocumento,   \n " +
                    " F.Serie, F.Folio, ISNULL(X.IdPAC, F.IdEmpresa) as IdPAC, IsNull(X.uf_Xml_Timbrado, F.FormatoXML) as XML, F.Status  \n " +
                    " From FACT_CFD_Documentos_Generados F (NoLock)    \n " + 
                    " Left Join FACT_CFDI_XML X (NoLock)    \n" +
                    "   On ( F.IdEmpresa = X.IdEmpresa and F.IdEstado = X.IdEstado and F.IdFarmacia = X.IdFarmacia and F.Serie = X.Serie And F.Folio = X.Folio ) \n " +
                    "Where F.Keyx in ( {0} )  \n " +
                    "Order By F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.Serie, F.Folio, F.FechaRegistro ", sFiltro);


                    sSql = string.Format(
                    "Select F.Keyx as Keyx_Docto, X.Keyx as Keyx_XML, \n " + 
                    " F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.Serie, F.Folio, \n " + 
                    " (F.Serie + '___' + cast(F.Folio as varchar)  + '___' + F.RFC ) as NombreDocumento, \n " + 
                    " IsNull(X.uf_Xml_Timbrado, F.FormatoXML) as XML, F.Status,  \n " + 
                    " cast((case when IsNull(X.IdPac, '') = '' then 0 else 1 end) as bit) as EsPAC, \n" +
                    " (case when {1} = 1 then F.FormatoPDF else '' end) as FormatoPDF,  \n " +
                    " (case when {1} = 1 then IsNull(X.uf_Xml_Impresion, '') else '' end) as PDF_PAC \n " + 
                    " From FACT_CFD_Documentos_Generados F (NoLock)    \n " + 
                    " Left Join FACT_CFDI_XML X (NoLock)    \n" + 
                    "   On ( F.IdEmpresa = X.IdEmpresa and F.IdEstado = X.IdEstado and F.IdFarmacia = X.IdFarmacia and F.Serie = X.Serie And F.Folio = X.Folio ) \n " + 
                    "Where F.Keyx in ( {0} )  \n " +
                    "Order By F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.Serie, F.Folio, F.FechaRegistro ", sFiltro, iDescargarPDF); 

                    if (!leerFolios.Exec(sSql))
                    {
                        Error.GrabarError(leerFolios, "DescargarDocumentos()");
                        General.msjError("Ocurrió un error al descargar la información solicitada.");
                    }
                    else
                    {
                        while (leerFolios.Leer())
                        {
                            lblAvance.Text = string.Format("Descargando {0} de {1}", iRow, iRegistros.ToString("###, ###, ###, ###, ###, ###"));
                            iRow++;

                            sIdEmpresa = leerFolios.Campo("IdEmpresa");
                            sIdEstado = leerFolios.Campo("IdEstado");
                            sIdFarmacia = leerFolios.Campo("IdFarmacia");
                            sSerie = leerFolios.Campo("Serie");
                            iFolio = Convert.ToInt32(leerFolios.CampoInt("Folio").ToString().Replace(",", "").Replace(" ", ""));

                            iKeyx_Docto = Convert.ToInt32(leerFolios.CampoInt("Keyx_Docto").ToString().Replace(",", "").Replace(" ", ""));
                            iKeyx = leerFolios.CampoInt("Keyx_XML"); 
                            sFile = leerFolios.Campo("NombreDocumento");
                            sFile_PDF = leerFolios.Campo("NombreDocumento") + "";
                            sFileContenido = leerFolios.Campo("XML");
                            bEsPAC = leerFolios.CampoBool("EsPAC");
                            sFileContenido_PDF = bEsPAC ? leerFolios.Campo("PDF_PAC") : leerFolios.Campo("FormatoPDF"); 


                            try
                            {
                                if (sFileContenido.ToUpper().Contains("UTF-8"))
                                {
                                    sFileContenido = clsTimbrar.toUTF8(sFileContenido);
                                    if (!Fg.ConvertirStringEnArchivo(sFile, sRutaDeDescarga, sFileContenido, true))
                                    {
                                        bArchivoGenerado = Fg.ConvertirStringB64EnArchivo(sFile, sRutaDeDescarga, sFileContenido, true);

                                        if (!bArchivoGenerado)
                                        {
                                            using (StreamWriter writer = new StreamWriter(string.Format(@"{0}", Path.Combine(sRutaDeDescarga, sFile + ".xml"))))
                                            {
                                                writer.Write(sFileContenido);
                                                writer.Close();
                                                //writer = null;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    byte[] buffer = Fg.ConvertirBytesFromStringB64(sFileContenido);
                                    buffer = System.Convert.FromBase64String(sFileContenido);

                                    ////sFileContenido = Fg.ConvertirStringB64(buffer);
                                    ////sFileContenido = clsTimbrar.toUTF8(sFileContenido);

                                    if (Fg.ConvertirStringB64EnArchivo(sFile, sRutaDeDescarga, sFileContenido, true))
                                    {
                                        StreamReader sr = new StreamReader(Path.Combine(sRutaDeDescarga, sFile), System.Text.Encoding.Default);

                                        sFileContenido = sr.ReadToEnd();
                                        sFileContenido = clsTimbrar.toUTF8(sFileContenido);
                                        sr.Close();
                                        sr = null;

                                        StreamWriter sw = new StreamWriter(Path.Combine(sRutaDeDescarga, sFile));
                                        sw.Write(sFileContenido);
                                        sw.Close();
                                        sw = null;
                                    }

                                    if (bEsPAC)
                                    {
                                        Actualizar_XML(sIdEmpresa, sIdEstado, sIdFarmacia, sSerie, iFolio, sFileContenido);
                                    }
                                }
                            }
                            catch (Exception ex_xml) 
                            { 
                            }


                            if (bDescargarPDF)
                            {
                                if (!bEsPAC)
                                {
                                    sFile_PDF += ".pdf";
                                    bRegresa = Fg.ConvertirStringB64EnArchivo(sFile_PDF, sRutaDeDescarga, sFileContenido_PDF, true); 
                                }
                                else
                                {
                                    ////DtIFacturacion.Generar_PDF(sFile_PDF, sRutaDeDescarga, sFileContenido_PDF);
                                    Descargar__PDF(sIdEmpresa, sIdEstado, sIdFarmacia, iKeyx_Docto, sFile_PDF, sFileContenido_PDF); 
                                }
                            }
                        }
                    }
                }
            }

            this.Hide(); 
        }

        private bool Descargar__PDF(string Empresa, string Estado, string Farmacia, int Identificador, string File_PDF, string FileContenido_PDF)
        {
            bool bRegresa = false;
            string sFileName = "";
            string sXML = "";
            string sPDF = "";
            string sEmail = "";
            clsLeer leerPDF = new clsLeer(ref cnn); 

            string sImpresion = " spp_FACT_CFDI_GetListaComprobantes ";
            string sSql = string.Format("Exec {0} ", sImpresion) + string.Format(" @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Identificador = {3} ",
                Empresa, Estado, Farmacia, Identificador);

            if ( leerPDF.Exec(sSql) ) 
            {
                DtIFacturacion.Generar_PDF(File_PDF, sRutaDeDescarga, FileContenido_PDF, GetExtras(leerPDF.DataSetClase)); 
            }

            return bRegresa; 
        }

        private ArrayList GetExtras(DataSet CamposAdicionales)
        {
            ArrayList Datos = new ArrayList();
            DatosExtraImpresion dato = new DatosExtraImpresion();
            clsLeer datosAdicionales = new clsLeer();
            datosAdicionales.DataSetClase = CamposAdicionales;
            datosAdicionales.Leer();

            dato = new DatosExtraImpresion();
            dato.Nombre = "StatusDocumento";
            dato.Valor = datosAdicionales.Campo("StatusDocto");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_01";
            dato.Valor = datosAdicionales.Campo("Observaciones_01");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_02";
            dato.Valor = datosAdicionales.Campo("Observaciones_02");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_03";
            dato.Valor = datosAdicionales.Campo("Observaciones_03");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Referencia";
            dato.Valor = datosAdicionales.Campo("Referencia");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "TipoDeDocumento";
            dato.Valor = datosAdicionales.Campo("TipoDocumentoDescripcion");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "TipoDeInsumo";
            dato.Valor = datosAdicionales.Campo("TipoInsumoDescripcion");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "RubroFinanciamiento";
            dato.Valor = datosAdicionales.Campo("RubroFinanciamento");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "FuenteFinanciamiento";
            dato.Valor = datosAdicionales.Campo("Financiamiento");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "CantidadLetra";
            dato.Valor = General.LetraMoneda(datosAdicionales.CampoDouble("Importe"));
            Datos.Add(dato);


            dato = new DatosExtraImpresion();
            dato.Nombre = "FechaCancelacionCFDI";
            dato.Valor = datosAdicionales.Campo("FechaCancelacion");
            Datos.Add(dato);

            return Datos;
        }

        private void Actualizar_XML(string IdEmpresa, string IdEstado, string IdFarmacia, string Serie, int Folio , string Contenido)
        {
            string sSql = "";
            clsLeer leerUpdate_xml = new clsLeer(ref cnn);

            sSql = string.Format(
                "Set DateFormat YMD Update X Set uf_Xml_Timbrado = '{5}' " +
                "From FACT_CFDI_XML X (NoLock) " +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' ",
                IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Contenido); 

            leerUpdate_xml.Exec(sSql);           
        }

        private void FrmReporteMensualDoctosEmitidos_Descargar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thDescarga.IsAlive)
            {
                TerminarProceso(3);
            }
        }

        private void TerminarProceso(int Ejecuciones)
        {
            for(int i= 1; i<= Ejecuciones; i++)
            {
                try
                {
                    thDescarga.Interrupt(); 
                }
                catch
                {
                }
            }

            try
            {
                thDescarga = null;
            }
            catch { }
        }
    }
}
