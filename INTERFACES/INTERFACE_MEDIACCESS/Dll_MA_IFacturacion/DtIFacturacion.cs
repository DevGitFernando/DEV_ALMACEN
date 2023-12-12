using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.SistemaOperativo;
using SC_SolutionsSystem.Reportes;

//// Espacios de nombres para impresiones CFDI 
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows;
using CrystalDecisions.Windows.Forms;

using Dll_MA_IFacturacion;
using Dll_MA_IFacturacion.CFDI;  
using Dll_MA_IFacturacion.XSA;

using DllFarmaciaSoft;
using Dll_MA_IFacturacion.CFDI.Timbrar; 

namespace Dll_MA_IFacturacion
{
    public enum eEsquemaDeFacturacion
    {
        Ninguno = 0, Libre = 1, Montos = 2
    }

    public enum eDocumentoAdministracion
    {
        Ninguno = 0, Detallado = 1, Concentrado = 2 
    }

    public enum eVersionCFDI
    {
        Ninguna = 0,
        Version__3_2 = 32,
        Version__3_3 = 33
    }

    public enum eTipoDeFacturacion
    {
        Ninguna = 0, Insumos = 1, Administracion = 2, 
        Manual = 3, Manual_Excel = 4 
    }

    public enum eTipoRemision
    {
        Ninguno = 0, Insumos = 1, Administracion = 2 
    }

    public enum eTipoInsumo
    {
        Ninguno = 0, MaterialDeCuracion = 1, Medicamento = 2 
    }

    public class EMailCFDI
    {
        #region Declaracion de variables

        public string EmailRespuesta = "";
        public string NombreParaMostrar = "";
        public string CC = "";
        public string Asunto = "";
        public string MensajePredeterminado = "";

        public string Servidor = "";
        public int Puerto = 0;
        public string Usuario = "";
        public string Password = "";
        public bool EnableSSL = true;
        public int TimeOut = 240;

        #endregion Declaracion de variables
    }

    public class PAC_Info
    {
        string sRFC_Emisor = ""; 
        string sUrl = ""; 
        string sUsuario = "";
        string sPassword = "";
        byte[] byCertificado = null;
        byte[] byKey = null;
        string sCertificadoPKCS12_Base64 = "";
        string sPasswordPKCS12 = "";
        bool bEnProduccion = false;
        PACs_Timbrado tpPAC = PACs_Timbrado.Ninguno; 

        public PAC_Info()
        {
        }

        public string RFC_Emisor
        {
            get { return sRFC_Emisor; }
            set { sRFC_Emisor = value; }
        }

        public string Url
        {
            get { return sUrl; }
            set { sUrl = value; }
        }

        public string Usuario
        {
            get { return sUsuario; }
            set { sUsuario = value; }
        }

        public string Password
        {
            get { return sPassword; }
            set { sPassword = value; }
        }

        public byte[] Certificado
        {
            get { return byCertificado; }
            set { byCertificado = value; }
        }

        public byte[] Key
        {
            get { return byKey; }
            set { byKey = value; }
        }

        public string CertificadoPKCS12
        {
            get { return sCertificadoPKCS12_Base64; }
            set { sCertificadoPKCS12_Base64 = value; }
        }

        public string PasswordPKCS12
        {
            get { return sPasswordPKCS12; }
            set { sPasswordPKCS12 = value; }
        }

        public bool EnProduccion
        {
            get { return bEnProduccion; }
            set { bEnProduccion = value; }
        }

        public PACs_Timbrado PAC
        {
            get { return tpPAC; }
            set { tpPAC = value; }
        }
    }

    public class DatosExtraImpresion
    {
        public string Nombre = "";
        public string Valor = "";
    }

    public static class DtIFacturacion 
    {

        #region Declaracion de Variables
        private static Modulo_CFDI moduloActivo = Modulo_CFDI.Ninguno;

        private static clsDatosApp dpDatosApp = new clsDatosApp("DllFarmaciaSoft", Application.ProductVersion);

        private static bool bUrlServicioTimbrado = false; 
        private static string sUrlServicioTimbrado = "";
        private static string sNombreProveedorTimbrado = "";
        private static xsaWebServices xsaServicios = new xsaWebServices();

        public static string sFormato_02 = "##############0.#0";
        public static string sFormato_04 = "##############0.####0";
        private static clsParametrosFacturacion pParametros;

        private static EMailCFDI datosEmail = new EMailCFDI();


        private static bool bEmisorValido = false;
        private static bool bEmisorActivo = false;
        private static string sIdEmisorRFC = "";
        private static string sIdRegistroPatronal = "";
        private static string sIdEmisorClaveRegistroPatronal = "";
        private static string sIdEmisorConectado = "";
        private static string sIdEmisorConectadoNombre = "";
        private static string sIdEmisorTelefonos = "";
        private static string sIdEmisorFax = "";
        private static string sIdEmisorMail = "";

        private static bool bEsPersonaFisica = false;
        private static bool bPublicoGeneral_AplicaIva = false; 

        public static bool bEsTimbradoMasivo = false;
        public static string sDirectorioAlternoArchivosGenerados = ""; 

        #endregion Declaracion de Variables

        #region Constructores 
        static DtIFacturacion()
        {
            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());

            DirectoriosDeTrabajo();
            Preparar__diCrPKI(sRutaCFDI_Tools);
        }
        #endregion Constructores 

        #region Propiedades 
        public static Modulo_CFDI ModuloActivo
        {
            get { return moduloActivo; }
            set { moduloActivo = value; }
        }

        public static clsDatosApp DatosApp
        {
            get { return dpDatosApp; }
            set { dpDatosApp = value; }
        }

        public static xsaWebServices Servicios
        {
            get { return xsaServicios; }
            set { xsaServicios = value; }
        }

        public static string NombreProveedorTimbrado
        {
            get { return sNombreProveedorTimbrado; }
            set { ; }
        }

        public static string UrlServicioTimbrado
        {
            get 
            {
                if (!bUrlServicioTimbrado)
                {
                    GetUrlServicioTimbrado(); 
                }

                return sUrlServicioTimbrado; 
            }
            set { ; }
        }

        public static bool EmisorValido
        {
            get { return bEmisorValido; }
            set { bEmisorValido = value; }
        }
        public static bool EmisorActivo
        {
            get { return bEmisorActivo; }
            set { bEmisorActivo = value; }
        }

        public static string EmisorRFC
        {
            get { return sIdEmisorRFC; }
            set { sIdEmisorRFC = value; }
        }

        public static bool EmisorRFC_EsPersonaFisica
        {
            get { return bEsPersonaFisica; }
            set { bEsPersonaFisica = value; }
        }

        public static bool EmisorRFC_PublicoGeneral_NoAplicaIva
        {
            get { return !bPublicoGeneral_AplicaIva; }
            set { bPublicoGeneral_AplicaIva = value; }
        }

        public static string EmisorIdRegistroPatronal
        {
            get { return sIdRegistroPatronal; }
            set { sIdRegistroPatronal = value; }
        }

        public static string EmisorClaveRegistroPatronal
        {
            get { return sIdEmisorClaveRegistroPatronal; }
            set { sIdEmisorClaveRegistroPatronal = value; }
        }

        public static string EmisorConectado
        {
            get { return sIdEmisorConectado; }
            set
            {
                sIdEmisorConectado = value;
                bPACConfigurado = sIdEmisorConectado != "" ? true : false;
            }
        }

        public static string EmisorConectadoNombre
        {
            get { return sIdEmisorConectadoNombre; }
            set { sIdEmisorConectadoNombre = value; }
        }

        public static string EmisorTelefonos
        {
            get { return sIdEmisorTelefonos; }
            set { sIdEmisorTelefonos = value; }
        }

        public static string EmisorFax
        {
            get { return sIdEmisorFax; }
            set { sIdEmisorFax = value; }
        }

        public static string EmisorMail
        {
            get { return sIdEmisorMail; }
            set { sIdEmisorMail = value; }
        }

        public static bool EsTimbradoMasivo
        {
            get { return bEsTimbradoMasivo; }
            set { bEsTimbradoMasivo = value; }
        }
        #endregion Propiedades 

        #region Parametros 
        private static string sRutaReportes = "";
        private static eEsquemaDeFacturacion tpEsquemaDeFacturacion = eEsquemaDeFacturacion.Ninguno;
        private static eDocumentoAdministracion tpDocumentoAdministracion = eDocumentoAdministracion.Ninguno;
        private static string sReutilizarObservacionesCapturadas = "";  
        private static bool bReutilizarObservacionesCapturadas = false;

        private static string sImplementaClaveSSA_Base__Identificador = "";
        private static bool bImplementaClaveSSA_Base__Identificador = false;

        private static string sImplementaInformacionPredeterminada = "";
        private static bool bImplementaInformacionPredeterminada = false;
        private static string sFormaDePago = "";
        private static bool bFormaDePago = false;
        private static string sPlazoDiasVenceFactura = "";
        private static int iPlazoDiasVenceFactura = 15;

        private static string sMetodoDePago = "";
        private static bool bMetodoDePago = false;
        private static string sMetodoDePagoReferencia = "";
        private static bool bMetodoDePagoReferencia = false;


        private static string sFactura_UsoCDFI = "";
        private static string sFactura_SAT_ClaveProducto__Servicio = "";
        private static string sFactura_SAT_UnidadDeMedida__Servicio = "";

        private static string sPago_UsoCDFI = "";
        private static string sPago_FormaDePago = "";
        private static string sPago_Moneda = "";


        private static string sNotaDeCredito_UsoCDFI = "";
        private static string sNotaDeCredito_TipoDeRelacionCFDI = "";
        private static string sNotaDeCredito_MetodoDePago = "";
        private static string sNotaDeCredito_FormaDePago = "";
        private static string sNotaDeCredito_SAT_ClaveProducto = "";
        private static string sNotaDeCredito_SAT_UnidadDeMedida = "";

        private static string sFormatoImpresion_Facturas = "";
        private static string sFormatoImpresion_ComplementoDePagos = "";
        private static string sFormatoImpresion_NotasDeCredito = "";
        private static string sFormatoImpresion_Traslados = "";
        private static string sFormatoImpresion_Anticipo = "";


        public static clsParametrosFacturacion Parametros
        {
            get { return pParametros; }
            set { pParametros = value; }
        }

        public static string RutaReportes
        {
            get
            {
                if (sRutaReportes == "")
                {
                    sRutaReportes = pParametros.GetValor("RutaReportes");
                }
                return sRutaReportes;
            }
            set { sRutaReportes = value; }
        }

        public static eEsquemaDeFacturacion EsquemaDeFacturacion 
        {
            get 
            {
                if (tpEsquemaDeFacturacion == eEsquemaDeFacturacion.Ninguno)
                {
                    tpEsquemaDeFacturacion = (eEsquemaDeFacturacion)pParametros.GetValorInt("EsquemaDeFacturacion");
                }
                return tpEsquemaDeFacturacion; 
            }

            set { tpEsquemaDeFacturacion = value; }
        }

        public static eDocumentoAdministracion DocumentoAdministracion
        {
            get
            {
                if (tpDocumentoAdministracion == eDocumentoAdministracion.Ninguno)
                {
                    tpDocumentoAdministracion = (eDocumentoAdministracion)pParametros.GetValorInt("DocumentoAdministracion");
                }
                return tpDocumentoAdministracion;
            }
        }

        public static bool ImplementaClaveSSA_Base__Identificador
        {
            get
            {
                if (sImplementaClaveSSA_Base__Identificador == "")
                {
                    bImplementaClaveSSA_Base__Identificador = pParametros.GetValorBool("ImplementaClaveSSA_Base__Identificador");
                    sImplementaClaveSSA_Base__Identificador = bImplementaClaveSSA_Base__Identificador.ToString();
                }
                return bImplementaClaveSSA_Base__Identificador;
            }
        }

        public static bool ImplementaInformacionPredeterminada
        {
            get
            {
                if (sImplementaInformacionPredeterminada == "")
                {
                    bImplementaInformacionPredeterminada = pParametros.GetValorBool("ImplementaInformacionPredeterminada");
                    sImplementaInformacionPredeterminada = bImplementaInformacionPredeterminada.ToString(); 
                }
                return bImplementaInformacionPredeterminada;
            }
            set { bImplementaInformacionPredeterminada = value; }
        }

        public static string Factura_UsoCDFI
        {
            get
            {
                if (sFactura_UsoCDFI == "")
                {
                    sFactura_UsoCDFI = pParametros.GetValor("Factura_UsoCDFI");
                }
                return sFactura_UsoCDFI;
            }
            set { sFactura_UsoCDFI = value; }
        }

        public static string Factura_SAT_ClaveProducto__Servicio
        {
            get
            {
                if (sFactura_SAT_ClaveProducto__Servicio == "")
                {
                    sFactura_SAT_ClaveProducto__Servicio = pParametros.GetValor("Factura_SAT_ClaveProducto__Servicio");
                }
                return sFactura_SAT_ClaveProducto__Servicio;
            }
            set { sFactura_SAT_ClaveProducto__Servicio = value; }
        }

        public static string Factura_SAT_UnidadDeMedida__Servicio
        {
            get
            {
                if (sFactura_SAT_UnidadDeMedida__Servicio == "")
                {
                    sFactura_SAT_UnidadDeMedida__Servicio = pParametros.GetValor("Factura_SAT_UnidadDeMedida__Servicio");
                }
                return sFactura_SAT_UnidadDeMedida__Servicio;
            }
            set { sFactura_SAT_UnidadDeMedida__Servicio = value; }
        }

        public static string FormaDePago
        {
            get
            {
                if (sFormaDePago == "")
                {
                    sFormaDePago = pParametros.GetValor("Factura_FormaDePago");
                }
                return sFormaDePago;
            }
            set { sFormaDePago = value; }
        }

        public static int PlazoDiasVenceFactura
        {
            get
            {
                if (sPlazoDiasVenceFactura == "")
                {
                    iPlazoDiasVenceFactura = pParametros.GetValorInt("Factura_PlazoDiasVenceFactura");
                    sPlazoDiasVenceFactura = iPlazoDiasVenceFactura.ToString();
                }
                return iPlazoDiasVenceFactura;
            }
            set { iPlazoDiasVenceFactura = value; }
        }

        public static string MetodoDePago
        {
            get
            {
                if (sMetodoDePago == "")
                {
                    sMetodoDePago = pParametros.GetValor("Factura_MetodoDePago");
                }
                return sMetodoDePago;
            }
            set { sMetodoDePago = value; }
        }

        public static string MetodoDePagoReferencia
        {
            get
            {
                if (sMetodoDePagoReferencia == "")
                {
                    sMetodoDePagoReferencia = pParametros.GetValor("Factura_MetodoDePagoReferencia");
                }
                return sMetodoDePagoReferencia;
            }
            set { sMetodoDePagoReferencia = value; }
        }

        public static bool ReutilizarObservacionesCapturadas
        {
            get
            {
                if (sReutilizarObservacionesCapturadas == "")
                {
                    bReutilizarObservacionesCapturadas = pParametros.GetValorBool("UtilizarUltimasObservaciones_FacturacionManual");
                    sReutilizarObservacionesCapturadas = bReutilizarObservacionesCapturadas.ToString(); 
                }
                return bReutilizarObservacionesCapturadas;
            }
        }

        public static string Pago_UsoCDFI
        {
            get
            {
                if (sPago_UsoCDFI == "")
                {
                    sPago_UsoCDFI = pParametros.GetValor("Pago_UsoCDFI");
                }
                return sPago_UsoCDFI;
            }
            set { sPago_UsoCDFI = value; }
        }

        public static string Pago_FormaDePago
        {
            get
            {
                if (sPago_FormaDePago == "")
                {
                    sPago_FormaDePago = pParametros.GetValor("Pago_FormaDePago");
                }
                return sPago_FormaDePago;
            }
            set { sPago_FormaDePago = value; }
        }

        public static string Pago_Moneda
        {
            get
            {
                if (sPago_Moneda == "")
                {
                    sPago_Moneda = pParametros.GetValor("Pago_Moneda");
                }
                return sPago_Moneda;
            }
            set { sPago_Moneda = value; }
        }

        public static string NotaDeCredito_UsoCDFI
        {
            get
            {
                if (sNotaDeCredito_UsoCDFI == "")
                {
                    sNotaDeCredito_UsoCDFI = pParametros.GetValor("NotaDeCredito_UsoCDFI");
                }
                return sNotaDeCredito_UsoCDFI;
            }
            set { sNotaDeCredito_UsoCDFI = value; }
        }

        public static string NotaDeCredito_TipoDeRelacionCFDI
        {
            get
            {
                if (sNotaDeCredito_TipoDeRelacionCFDI == "")
                {
                    sNotaDeCredito_TipoDeRelacionCFDI = pParametros.GetValor("NotaDeCredito_TipoDeRelacionCFDI");
                }
                return sNotaDeCredito_TipoDeRelacionCFDI;
            }
            set { sNotaDeCredito_TipoDeRelacionCFDI = value; }
        }

        public static string NotaDeCredito_MetodoDePago
        {
            get
            {
                if (sNotaDeCredito_MetodoDePago == "")
                {
                    sNotaDeCredito_MetodoDePago = pParametros.GetValor("NotaDeCredito_MetodoDePago");
                }
                return sNotaDeCredito_MetodoDePago;
            }
            set { sNotaDeCredito_MetodoDePago = value; }
        }

        public static string NotaDeCredito_FormaDePago
        {
            get
            {
                if (sNotaDeCredito_FormaDePago == "")
                {
                    sNotaDeCredito_FormaDePago = pParametros.GetValor("NotaDeCredito_FormaDePago");
                }
                return sNotaDeCredito_FormaDePago;
            }
            set { sNotaDeCredito_FormaDePago = value; }
        }

        public static string NotaDeCredito_SAT_ClaveProducto
        {
            get
            {
                if (sNotaDeCredito_SAT_ClaveProducto == "")
                {
                    sNotaDeCredito_SAT_ClaveProducto = pParametros.GetValor("NotaDeCredito_SAT_ClaveProducto");
                }
                return sNotaDeCredito_SAT_ClaveProducto;
            }
            set { sNotaDeCredito_SAT_ClaveProducto = value; }
        }

        public static string NotaDeCredito_SAT_UnidadDeMedida
        {
            get
            {
                if (sNotaDeCredito_SAT_UnidadDeMedida == "")
                {
                    sNotaDeCredito_SAT_UnidadDeMedida = pParametros.GetValor("NotaDeCredito_SAT_UnidadDeMedida");
                }
                return sNotaDeCredito_SAT_UnidadDeMedida;
            }
            set { sNotaDeCredito_SAT_UnidadDeMedida = value; }
        }
        #endregion Parametros 

        #region Directorios CFDI
        private static string sRutaCFDI = Application.StartupPath + @"\CFDI";
        private static string sRutaCFDI_Certificados = Application.StartupPath + @"\CFDI\Certificados";
        private static string sRutaCFDI_Estilos = Application.StartupPath + @"\CFDI\Estilos";
        private static string sRutaCFDI_Documentos = Application.StartupPath + @"\CFDI\Documentos";
        private static string sRutaCFDI_Reportes = Application.StartupPath + @"\CFDI\Reportes";
        private static string sRutaCFDI_DocumentosGenerados = Application.StartupPath + @"\CFDI\Documentos\Generados";
        private static string sRutaCFDI_DocumentosImpresion = Application.StartupPath + @"\CFDI\Documentos\Impresion";
        private static string sRutaCFDI_Tools = Application.StartupPath + @"\CFDI\TOOLS";
        private static string sRutaCFDI_Log = Application.StartupPath + @"\CFDI\Log";

        public static string RutaCFDI
        {
            get { return sRutaCFDI; }
        }

        public static string RutaCFDI_Certificados
        {
            get { return sRutaCFDI_Certificados; }
        }

        public static string RutaCFDI_Reportes
        {
            get { return sRutaCFDI_Reportes; }
        }

        public static string RutaCFDI_Estilos
        {
            get { return sRutaCFDI_Estilos; }
        }

        public static string RutaCFDI_Documentos
        {
            get { return sRutaCFDI_Documentos; }
        }

        public static string RutaCFDI_DocumentosGenerados
        {
            get { return sRutaCFDI_DocumentosGenerados; }
        }

        public static string RutaCFDI_DocumentosImpresion
        {
            get { return sRutaCFDI_DocumentosImpresion; }
        }

        public static string RutaCFDI_Tools
        {
            get { return sRutaCFDI_Tools; }
        }

        public static string RutaCFDI_Log
        {
            get { return sRutaCFDI_Log; }
        }

        private static void DirectoriosDeTrabajo()
        {
            CrearDirectorio(sRutaCFDI);
            CrearDirectorio(sRutaCFDI_Certificados);
            CrearDirectorio(sRutaCFDI_Estilos);
            CrearDirectorio(sRutaCFDI_Documentos);
            CrearDirectorio(sRutaCFDI_Reportes);
            CrearDirectorio(sRutaCFDI_DocumentosGenerados);
            CrearDirectorio(sRutaCFDI_DocumentosImpresion);
            CrearDirectorio(sRutaCFDI_Tools);
            CrearDirectorio(sRutaCFDI_Log); 
        }

        public static void CrearDirectorio(string Directorio)
        {
            if (!Directory.Exists(Directorio))
            {
                Directory.CreateDirectory(Directorio);
            }
        }

        public static void EliminarArchivo(string Archivo)
        {
            try
            {
                if (File.Exists(Archivo))
                {
                    File.Delete(Archivo);
                }
            }
            catch { }
        }
        #endregion Directorios CFDI

        #region CFDI
        static string sRutaCertificado = "";
        static string sRutaKey = "";
        static string sRutaCertificadoPEM = "";
        static string sRutaKeyPEM = "";
        static string sRutaCertificadoPfx = "";
        static string sCertificadoPfx_B64 = "";
        static string sPassword = "";
        static string sRuta_XSLT_CadenaOriginal = "";
        static string sRuta_XSLT_CadenaOriginal_TFD = "";
        static Image imgLogo = null;
        static DataTable dtLogo = new DataTable();
        static byte[] objImagenLogo = new byte[1];
        static bool bEmailConfigurado = false;
        ///static EMailCFDI datosEmail = new EMailCFDI();

        static string sXMLFormaPago = "PAGO EN UNA SOLA EXHIBICIÓN";
        static string sXMLCondicionesPago = "Crédito";
        static string sXMLMetodoPago = "No identificado";

        static bool bPACConfigurado = false;
        static PAC_Info pacInfo = new PAC_Info();
        static Form frmPadreVisor;

        static bool bEsEnvioDeFactura_EMail = false; 
        static string sNombreFormatoFacturaCFDI = "CFDI_Factura";
        static string sNombreFormatoFacturaCFDI_Sucursal = "CFDI_Factura_Farmacia_Ticket";
        static string sNombreFormatoFacturaCFDI_Sucursal_EMail = "CFDI_Factura_Farmacia";

        public static Form InvocaVisor
        {
            get { return frmPadreVisor; }
            set { frmPadreVisor = value; }
        }

        public static string XMLFormaPago_Default
        {
            get { return sXMLFormaPago.ToUpper(); }
        }

        public static string XMLCondicionesPago_Default
        {
            get { return sXMLCondicionesPago.ToUpper(); }
        }

        public static string XMLMetodoPago_Default
        {
            get { return sXMLMetodoPago.ToUpper(); }
        }

        public static string RutaCertificado
        {
            get { return sRutaCertificado; }
        }

        public static string RutaKey
        {
            get { return sRutaKey; }
        }

        public static string RutaCertificadoPEM
        {
            get { return sRutaCertificadoPEM; }
        }

        public static string RutaKeyPEM
        {
            get { return sRutaKeyPEM; }
        }

        public static string RutaCertificadoPfx
        {
            get { return sRutaCertificadoPfx; }
        }

        public static string Password_Certificado
        {
            get { return sPassword; }
        }

        public static string Ruta_XSLT_CadenaOriginal
        {
            get { return sRuta_XSLT_CadenaOriginal; }
        }

        public static string Ruta_XSLT_CadenaOriginal_TFD
        {
            get { return sRuta_XSLT_CadenaOriginal_TFD; }
        }

        public static Image LogoImagen
        {
            get { return imgLogo; }
        }

        public static DataTable Logo
        {
            get
            {
                return dtLogo;
            }
        }

        public static bool EmailConfigurado
        {
            get { return bEmailConfigurado; }
        }

        public static bool PAC_Configurado
        {
            get { return bPACConfigurado; }
        }

        public static PAC_Info PAC_Informacion
        {
            get { return pacInfo; }
            set { pacInfo = value; }
        }

        public static bool EsEnvioDeFactura_EMail
        {
            get { return bEsEnvioDeFactura_EMail; }
            set { bEsEnvioDeFactura_EMail = value; }
        }

        public static EMailCFDI DatosEmail
        {
            get { return datosEmail; }
        }

        public static string VersionCFDI_ToString(eVersionCFDI VersionCFDI)
        {
            string sRegresa = "";

            switch (VersionCFDI)
            {
                case eVersionCFDI.Ninguna:
                    sRegresa = "Invalida";
                    break;

                case eVersionCFDI.Version__3_2:
                    sRegresa = "3.2";
                    break;

                case eVersionCFDI.Version__3_3:
                    sRegresa = "3.3";
                    break;
            }

            return sRegresa;
        }

        public static double VersionCFDI_ToDouble(eVersionCFDI VersionCFDI)
        {
            double dRegresa = 0;

            switch (VersionCFDI)
            {
                case eVersionCFDI.Ninguna:
                    dRegresa = 0;
                    break;

                case eVersionCFDI.Version__3_2:
                    dRegresa = 3.2;
                    break;

                case eVersionCFDI.Version__3_3:
                    dRegresa = 3.3;
                    break;
            }

            return dRegresa;
        }

        public static void InicializarInformacionEmisor()
        {
            //DtGeneral.EmpresaDatos.RFC = "AAA010101AAA"; 
            basGenerales Fg = new basGenerales();
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            string sRuta = Path.Combine(sRutaCFDI_Certificados, DtGeneral.EmpresaDatos.RFC);
            int iPEM = 0;
            //sRuta = Path.Combine(sRutaCFDI_Certificados, "AAA010101AAA"); 

            string sSql = string.Format("Select C.IdEmpresa, E.RFC, C.NumeroDeCertificado, C.NombreCertificado, C.Certificado, " +
                " C.ValidoDesde, C.ValidoHasta, C.FechaInicio, C.FechaFinal, C.Serie, C.Serial, C.NombreLlavePrivada, C.LlavePrivada, " +
                " C.PasswordPublico, C.NombreCertificadoPfx, C.CertificadoPfx, C.AvisoVencimiento, C.TiempoAviso " +
                " From CFDI_Emisores_Certificados C (NoLock) " +
                " Inner Join CFDI_Emisores E (NoLock) On ( C.IdEmpresa = E.IdEmpresa ) " + 
                " Where C.IdEmpresa = '{0}' ", DtGeneral.EmpresaConectada);

            
            if (!leer.Exec(sSql))
            {
                clsGrabarError.LogFileError(leer.MensajeError);
            }
            else
            {
                if (leer.Leer())
                {
                    DtGeneral.EmpresaDatos.RFC = leer.Campo("RFC"); 
                    sRuta = Path.Combine(sRutaCFDI_Certificados, DtGeneral.EmpresaDatos.RFC);
                    
                    CrearDirectorio(sRuta);
                    CrearDirectorio(DtIFacturacion.RutaCFDI_DocumentosGenerados + @"\" + DtGeneral.EmpresaDatos.RFC);
                    CrearDirectorio(DtIFacturacion.RutaCFDI_DocumentosImpresion + @"\" + DtGeneral.EmpresaDatos.RFC); 

                    sRutaCertificado = Path.Combine(sRuta, leer.Campo("NombreCertificado"));
                    sRutaKey = Path.Combine(sRuta, leer.Campo("NombreLlavePrivada"));
                    sRutaCertificadoPfx = Path.Combine(sRuta, leer.Campo("NombreCertificadoPfx"));

                    sRutaCertificadoPEM = Path.Combine(sRuta, "");
                    sRutaKeyPEM = Path.Combine(sRuta, "");

                    sCertificadoPfx_B64 = leer.Campo("CertificadoPfx");
                    sPassword = leer.Campo("PasswordPublico");
                    sRuta_XSLT_CadenaOriginal = sRutaCFDI_Estilos + @"\cadenaoriginal_3_3.xslt";
                    sRuta_XSLT_CadenaOriginal_TFD = sRutaCFDI_Estilos + @"\cadenaoriginal_TFD_1_1.xslt";

                    Fg.ConvertirStringB64EnArchivo(leer.Campo("NombreCertificado"), sRuta, leer.Campo("Certificado"), true);
                    Fg.ConvertirStringB64EnArchivo(leer.Campo("NombreLlavePrivada"), sRuta, leer.Campo("LlavePrivada"), true);
                    Fg.ConvertirStringB64EnArchivo(leer.Campo("NombreCertificadoPfx"), sRuta, leer.Campo("CertificadoPfx"), true);

                    //Fg.ConvertirStringEnArchivo("cadenaoriginal_3_2.xslt", sRutaCFDI_Estilos, CFDI.Properties.Resources.cadenaoriginal_3_2, true); 

                }
            }

            //Preparar__diCrPKI(); 

            InicializarInformacionEmisor_Logo();
            InicializarInformacionEmisor_Email();
            InicializarInformacionEmisor_Contacto();

            InicializarInformacionEmisor_GenerarPEM(iPEM); 
            InicializarInformacionEmisor_PAC();

            Generar_Estilos();
        }

        public static void InicialiarParametros__Facturacion()
        {
            if (DtIFacturacion.Parametros == null)
            {
                DtIFacturacion.Parametros = new clsParametrosFacturacion(General.DatosConexion, GnFarmacia.DatosApp,
                    DtGeneral.EstadoConectado, "0001", DtGeneral.ArbolModulo);
                DtIFacturacion.Parametros.CargarParametros();

                try
                {
                    //// Pasar la ruta de reportes al General 
                    DtGeneral.RutaReportes = DtIFacturacion.RutaReportes != "" ? DtIFacturacion.RutaReportes : DtGeneral.RutaReportes;
                }
                catch { }
            }
        }

        private static void Preparar__diCrPKI(string RutaDestino)
        {
            SC_DllImports diCrPKI_dll = new SC_DllImports();

            diCrPKI_dll.VerificaLibrerias(RutaDestino);
            //diCrPKI_dll.CargarDll(); 
        }

        private static void InicializarInformacionEmisor_Logo()
        {
            basGenerales Fg = new basGenerales();
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            byte[] bytes;
            object[] objImagenes = new object[1];


            string sSql = string.Format("Select Logo  " +
                " From CFDI_Emisores_Logos (NoLock) " +
                " Where IdEmpresa = '{0}' ", DtGeneral.EmpresaConectada);

            imgLogo = null;
            dtLogo = new DataTable("Logo");
            dtLogo.Columns.Add("Logo", System.Type.GetType("System.Byte[]"));

            if (leer.Exec(sSql))
            {
                leer.RenombrarTabla(1, "Logo");
                if (leer.Leer())
                {
                    try
                    {
                        bytes = leer.CampoByte("Logo");
                        objImagenes[0] = bytes;

                        IntPtr intr = new IntPtr(0);

                        MemoryStream ms = new MemoryStream(bytes);
                        imgLogo = Image.FromStream(ms);
                    }
                    catch { }

                    dtLogo.Rows.Add(objImagenes);
                }

                leer.DataTableClase = dtLogo;
            }
        }

        private static void InicializarInformacionEmisor_Contacto()
        {
            ////////clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            ////////clsLeer leer = new clsLeer(ref cnn);

            ////////string sSql = string.Format("Select IdEmisor, Telefonos, Fax, Email " +
            ////////    " From CFDI_Emisores (NoLock) " +
            ////////    " Where IdEmisor = '{0}' ", DtGeneral.EmpresaConectada);


            ////////if (leer.Exec(sSql))
            ////////{
            ////////    if (leer.Leer())
            ////////    {
            ////////        sIdEmisorTelefonos = leer.Campo("Telefonos");
            ////////        sIdEmisorFax = leer.Campo("Fax");
            ////////        sIdEmisorMail = leer.Campo("Email");
            ////////    }
            ////////}
        }

        private static void InicializarInformacionEmisor_Email()
        {
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);

            string sSql = string.Format("Select IdEmpresa, IdEstado, IdFarmacia, Servidor, Puerto, Usuario, Password, EnableSSL, " +
                " EmailRespuesta, NombreParaMostrar, CC, Asunto, MensajePredeterminado " +
                " From CFDI_Emisores_Mail (NoLock) " + 
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}'  ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);


            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    bEmailConfigurado = true;
                    datosEmail.Servidor = leer.Campo("Servidor");
                    datosEmail.Puerto = leer.CampoInt("Puerto");
                    datosEmail.Usuario = leer.Campo("Usuario");
                    datosEmail.Password = leer.Campo("Password");
                    datosEmail.EnableSSL = leer.CampoBool("EnableSSL");

                    datosEmail.EmailRespuesta = leer.Campo("EmailRespuesta");
                    datosEmail.NombreParaMostrar = leer.Campo("NombreParaMostrar"); 
                    datosEmail.CC = leer.Campo("CC");
                    datosEmail.Asunto = leer.Campo("Asunto");
                    datosEmail.MensajePredeterminado = leer.Campo("MensajePredeterminado");
                }
            }
        }

        public static void InicializarInformacionEmisor_PAC()
        {
            basGenerales Fg = new basGenerales();
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);

            string sSql = string.Format(
                " Select E.IdEmpresa, E.IdPAC, P.NombrePAC, " +
                " (case when E.EnProduccion = 1 then P.UrlProduccion else P.UrlPruebas End) as Url, " + 
                " E.Usuario, E.Password, E.EnProduccion " +
                " From CFDI_Emisores_PAC E (NoLock) " +
                " Inner Join CFDI_PACs P (NoLock) On ( E.IdPAC = P.IdPAC )  " +
                " Where E.IdEmpresa = '{0}' ", DtGeneral.EmpresaConectada);

            bPACConfigurado = false;
            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    bPACConfigurado = true;
                    pacInfo.PAC = (PACs_Timbrado)leer.CampoInt("IdPAC");
                    pacInfo.RFC_Emisor = DtGeneral.EmpresaDatos.RFC;
                    pacInfo.Url = leer.Campo("Url");
                    pacInfo.Usuario = leer.Campo("Usuario");
                    pacInfo.Password = leer.Campo("Password");

                    pacInfo.PasswordPKCS12 = sPassword;
                    pacInfo.EnProduccion = leer.CampoBool("EnProduccion");

                    pacInfo.CertificadoPKCS12 = sCertificadoPfx_B64;
                    pacInfo.Certificado = Fg.ConvertirArchivoEnBytes(DtIFacturacion.RutaCertificado);
                    pacInfo.Key = Fg.ConvertirArchivoEnBytes(DtIFacturacion.RutaKey);

                    if (pacInfo.PAC == PACs_Timbrado.VirtualSoft)
                    {
                        pacInfo.Certificado = GetFileContenido(DtIFacturacion.RutaCertificadoPEM);
                        pacInfo.Key = GetFileContenido(DtIFacturacion.RutaKeyPEM);

                    }
                }
            }
        }

        private static byte[] GetFileContenido(string RutaArchivo)
        {
            basGenerales Fg = new basGenerales();
            string sFileContenido = "";
            byte[] byFileContenido = null;
            byte[] byFileContenido_Aux = null;

            try
            {
                StreamReader reader = new StreamReader(RutaArchivo);
                sFileContenido = reader.ReadToEnd();
                reader.Close();
                reader = null;
            }
            catch { }

            //byFileContenido_Aux = Fg.ConvertirBytesFromStringB64(Fg.ConvertirStringB64FromString(sFileContenido));
            byFileContenido = stringToBase64ByteArray(sFileContenido);

            return byFileContenido;
        }

        private static byte[] stringToBase64ByteArray(string input)
        {
            Byte[] ret = Encoding.UTF8.GetBytes(input);
            String s = Convert.ToBase64String(ret);
            ret = Convert.FromBase64String(s);
            return ret;
        }

        public static void InicializarInformacionEmisor_GenerarPEM(int Vueltas)
        {
            int iVueltas = Vueltas;
            string sTool_SSL = Path.Combine(sRutaCFDI_Tools, "openssl.exe");
            string sParametros_Certificado = "";
            string sParametros_Key = "";
            string sRuta = "";
            FileInfo infoCertificado = new FileInfo(DtIFacturacion.RutaCertificado);
            FileInfo infoKey = new FileInfo(DtIFacturacion.RutaKey);
            Process proceso = null;


            sRuta = Path.Combine(sRutaCFDI_Certificados, DtGeneral.EmpresaDatos.RFC);
            sRutaCertificadoPEM = Path.Combine(sRuta, General.Fg.Mid(infoCertificado.Name, 1, infoCertificado.Name.Length - infoCertificado.Extension.Length)) + "__cer.PEM";
            sRutaKeyPEM = Path.Combine(sRuta, General.Fg.Mid(infoKey.Name, 1, infoKey.Name.Length - infoKey.Extension.Length)) + "__key.PEM";

            EliminarArchivo(sRutaCertificadoPEM);
            EliminarArchivo(sRutaKeyPEM);

            // openssl.exe 
            sParametros_Certificado = "  x509 -inform DER -outform PEM -in " + DtIFacturacion.RutaCertificado + " -pubkey -out " + DtIFacturacion.RutaCertificadoPEM;
            sParametros_Key = "  pkcs8 -inform DER -in " + DtIFacturacion.RutaKey + " -passin pass:" + DtIFacturacion.Password_Certificado.Trim() + " -out " + DtIFacturacion.RutaKeyPEM;

            sParametros_Certificado = string.Format("  x509 -inform DER -outform PEM -in {0}{1}{0}  -pubkey -out {0}{2}{0} ",
                General.Fg.Comillas(), DtIFacturacion.RutaCertificado, DtIFacturacion.RutaCertificadoPEM);
            sParametros_Key = string.Format("  pkcs8 -inform DER -in {0}{1}{0} -passin pass:{2}  -out {0}{3}{0}",
                General.Fg.Comillas(), DtIFacturacion.RutaKey, DtIFacturacion.Password_Certificado.Trim(), DtIFacturacion.RutaKeyPEM);

            try
            {
                clsGrabarError.LogFileError(sParametros_Certificado);

                proceso = new Process();
                proceso.StartInfo.FileName = sTool_SSL;
                proceso.StartInfo.Arguments = sParametros_Certificado;
                proceso.StartInfo.CreateNoWindow = true;
                proceso.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //proceso.WaitForExit();
                proceso.Start();
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);

            }
            catch
            {
            }

            try
            {
                clsGrabarError.LogFileError(sParametros_Key);
                proceso = new Process();
                proceso.StartInfo.FileName = sTool_SSL;
                proceso.StartInfo.Arguments = sParametros_Key;
                proceso.StartInfo.CreateNoWindow = true;
                proceso.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //proceso.WaitForExit();
                proceso.Start();
                proceso.Close();

                Application.DoEvents();
                System.Threading.Thread.Sleep(100);

            }
            catch
            {
            }


            if (iVueltas >= 3)
            {
                clsGrabarError.LogFileError("Intentos para generar archivos .PEM excedidos.");
            }
            else
            {
                if (!File.Exists(sRutaCertificadoPEM) || !File.Exists(sRutaKeyPEM))
                {

                    System.Threading.Thread.Sleep(100);
                    iVueltas++;
                    InicializarInformacionEmisor_GenerarPEM(iVueltas);
                }
            }

        }

        private static void Generar_Estilos()
        {
            ////string sCadenaOriginal_3_2 = Dll_MA_IFacturacion.Properties.Resources.cadenaoriginal_3_2;
            ////string sCadenaOriginal_TFD_1_0 = Dll_MA_IFacturacion.Properties.Resources.cadenaoriginal_TFD_1_0;
            ////basGenerales Fg = new basGenerales(); 

            ////Fg.ConvertirStringEnArchivo("cadenaoriginal_3_2.xslt", sRutaCFDI_Estilos, sCadenaOriginal_3_2, true);
            ////Fg.ConvertirStringEnArchivo("cadenaoriginal_TFD_1_0.xslt", sRutaCFDI_Estilos, sCadenaOriginal_TFD_1_0, true);  

        }        
        #endregion CFDI

        #region Impresion CFDI
        //////public static void GenerarImpresionWebBrowser(string FileName, string PDF, string XML, bool Visualizar)
        //////{
        //////    GenerarImpresionWebBrowser(FileName, null, PDF, XML, Visualizar, null);
        //////}

        //////public static void GenerarImpresionCrystal(string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar)
        //////{
        //////    GenerarImpresionCrystal(FileName, Datos, PDF, XML, Visualizar, null);
        //////}

        public static DataTable GetExtras(ArrayList Datos)
        {
            DataTable dt = new DataTable("Extras");
            object[] obj = new object[Datos.Count];
            int i = -1;

            foreach (DatosExtraImpresion d in Datos)
            {
                i++;
                obj[i] = d.Valor.ToString();
                //dt.Columns.Add(d.Nombre, System.Type.GetType("System.String"));

                try
                {
                    dt.Columns.Add(d.Nombre, System.Type.GetType("System.String"));
                }
                catch
                {
                    dt.Columns.Add(d.Nombre + "__" + i.ToString(), System.Type.GetType("System.String"));
                }
            }

            dt.Rows.Add(obj);

            return dt;
        }

        public static bool GenerarImpresionWebBrowser(bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor)
        {
            string sFormato = GetFormato_CFDI();  
            return GenerarImpresionInterna(sFormato, VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 1, Visor);
        }

        public static bool GenerarImpresionWebBrowser(bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor)
        {
            string sFormato = GetFormato_CFDI();  
            return GenerarImpresionInterna(sFormato, VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 1, Visor);
        }

        public static bool GenerarImpresionCrystal(bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor)
        {
            string sFormato = GetFormato_CFDI();  
            return GenerarImpresionInterna(sFormato, VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 2, Visor);
        }

        public static bool GenerarImpresionCrystal(bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor)
        {
            string sFormato = GetFormato_CFDI();  
            return GenerarImpresionInterna(sFormato, VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 2, Visor);
        }
        
        private static bool GenerarImpresionInterna(bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, int Tipo, object Visor)
        {
            string sFormato = GetFormato_CFDI();  ////DtCFDI.ModuloActivo == Modulo_CFDI.Facturacion ? sNombreFormatoFacturaCFDI : sNombreFormatoNominaCFDI;
            return GenerarImpresionInterna(sFormato, VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, Tipo, Visor);
        }

        private static string GetFormato_CFDI()
        {
            string sRegresa = "";

            if (DtIFacturacion.ModuloActivo == Modulo_CFDI.Facturacion_Centralizada)
            {
                sRegresa = sNombreFormatoFacturaCFDI;
            }
            else 
            {
                if (bEsEnvioDeFactura_EMail)
                {
                    sRegresa = sNombreFormatoFacturaCFDI_Sucursal_EMail;
                }
                else 
                {
                    sRegresa = sNombreFormatoFacturaCFDI_Sucursal;
                }
                bEsEnvioDeFactura_EMail = false;
            }

            return sRegresa; 
        }

        private static string RemoveInvalidChars(string strSource)
        {
            return Regex.Replace(strSource, @"[^0-9a-zA-Z=+\/]", " ");
        }

        private static bool GenerarImpresionInterna(string FormatoCFDI, bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, int Tipo, object Visor)
        {
            FileName = VistaPrevia ? "VISTA_PREVIA" : FileName;
            string sRegresa = "";
            string sPDF_Auxiliar = ""; 
            string sFileName_Auxiliar = ""; 
            string sRutaTmpImpresion = DtIFacturacion.RutaCFDI_DocumentosImpresion + @"\" + DtGeneral.EmpresaDatos.RFC;
            string sRuta = sRutaTmpImpresion + @"\" + FileName + ".xml";
            string sRutaTrabajo = sRutaTmpImpresion + @"\" + FileName + ".xmlpdf";
            string sXml = "";
            basGenerales Fg = new basGenerales();
            clsImprimir reporte = new clsImprimir(General.DatosConexion);
            clsLeer leerDts = new clsLeer();
            string sFile_Base = Path.Combine(sRutaTmpImpresion, FileName + ".xml");
            bool bReporteGenerado = false;

            try
            {
                bool bArchivo = Fg.ConvertirStringB64EnArchivo(FileName + ".xmlpdf", sRutaTmpImpresion, PDF, true);

                if (!bArchivo)
                {
                    if ( PDF.Contains("<?xml version="))
                    {
                        using (StreamWriter writer = new StreamWriter(string.Format(@"{0}", Path.Combine(sRutaTmpImpresion, FileName + ".xmlpdf"))))
                        {
                            writer.Write(PDF);
                            writer.Close();
                            //writer = null;
                        }
                    }
                    ////sPDF_Auxiliar = RemoveInvalidChars(PDF);
                    ////bArchivo = Fg.ConvertirStringB64EnArchivo(FileName + ".xmlpdf", sRutaTmpImpresion, sPDF_Auxiliar, true);
                }

                if (XML.Contains("UTF-8"))
                {
                    XML = clsTimbrar.toUTF8(XML); 
                    bArchivo = Fg.ConvertirStringEnArchivo(FileName + ".xml", sRutaTmpImpresion, XML, true);

                    if (!bArchivo)
                    {
                        using (StreamWriter writer = new StreamWriter(string.Format(@"{0}", Path.Combine(sRutaTmpImpresion, FileName + ".xml"))))
                        {
                            writer.Write(XML);
                            writer.Close();
                            //writer = null;
                        }
                    }
                        
                }
                else
                {
                    bArchivo = Fg.ConvertirStringB64EnArchivo(FileName + ".xml", sRutaTmpImpresion, XML, true);

                    if (!bArchivo)
                    {
                        using (StreamWriter writer = new StreamWriter(string.Format(@"{0}", Path.Combine(sRutaTmpImpresion, FileName + ".xml"))))
                        {
                            writer.Write(XML);
                            writer.Close();
                            //writer = null;
                        }
                    }
                }

                DataSet dts = new DataSet();
                dts.ReadXml(sRutaTrabajo);
                leerDts.DataSetClase = dts;

                if (leerDts.ExisteTabla("Extras"))
                {
                    if (ForzarDatosAuxiliares)
                    {
                        dts.Tables.Remove("Extras");
                        leerDts.DataSetClase = dts; 
                    }
                }

                if (!leerDts.ExisteTabla("Extras"))
                {
                    dts.Tables.Add(GetExtras(Datos).Copy());
                }
                leerDts.DataSetClase = dts;

                try
                {
                    sFileName_Auxiliar = ValidarStatusDocumento(leerDts.Tabla("Extras"));
                }
                catch { } 

                ////sRuta = sRutaTmpImpresion + @"\" + FileName + ".xml"; 
                if (File.Exists(sRutaTrabajo))
                {
                    File.Delete(sRutaTrabajo);
                }
                ////dts.WriteXml(sRutaTrabajo, XmlWriteMode.WriteSchema);
                ////if (File.Exists(sRutaTrabajo))
                ////{
                ////    if (!DtGeneral.EsEquipoDeDesarrollo)
                ////    {
                ////        File.Delete(sRutaTrabajo);
                ////    }
                ////}


                dts = ValidarEstructura(dts);
                dts.WriteXml(sRutaTrabajo, XmlWriteMode.WriteSchema); 
                if (File.Exists(sRutaTrabajo))
                {
                    if (!DtGeneral.EsEquipoDeDesarrollo)
                    {
                        File.Delete(sRutaTrabajo);
                    }
                }



                reporte.RutaReporte = DtIFacturacion.RutaCFDI_Reportes;
                reporte.NombreReporte = FormatoCFDI; //// "CFDI_Factura";
                reporte.OrigenDeDatosDataSet = true;
                reporte.OrigenDeDatosReporte = dts;
                reporte.EnviarAImpresora = false;
                reporte.TituloReporte = "Documento electrónico";
                //reporte.Add("CantidadConLetra", General.LetraMoneda(Total)); 

                if (Tipo == 1)
                {
                    reporte.CargarReporte(false, false);
                    FileName += sFileName_Auxiliar;
                    sRutaTrabajo = sRutaTmpImpresion + @"\" + FileName + ".pdf";
                    bReporteGenerado = reporte.ExportarReporteSilencioso(sRutaTrabajo, FormatosExportacion.PortableDocFormat);
                }

                if (Tipo == 2)
                {
                    sRutaTrabajo = sRutaTmpImpresion + @"\tmp.rpt";
                    bReporteGenerado = reporte.ExportarReporteSilencioso(sRutaTrabajo, FormatosExportacion.CrystalReport);
                }

                sRegresa = sRutaTrabajo;
            }
            catch (Exception ex)
            {
                clsGrabarError.LogFileError(ex.Message); 
            }

            if (Visualizar && bReporteGenerado) 
            {
                if (Tipo == 1)
                {
                    sRutaTrabajo = sRutaTmpImpresion + @"\" + FileName + ".pdf";
                    if (Visor == null)
                    {
                        FrmVisorCFDI f = new FrmVisorCFDI(FileName, sRutaTrabajo); 
                        if (frmPadreVisor != null)
                        {
                            f.ShowDialog(frmPadreVisor);
                            frmPadreVisor = null; 
                        }
                        else
                        {
                            f.ShowDialog();
                        }
                    }
                    else
                    {
                        WebBrowser browser = (WebBrowser)Visor;
                        browser.Navigate(sRutaTrabajo);
                    }
                }

                if (Tipo == 2)
                {
                    if (Visor == null)
                    {
                        reporte.CargarReporte(true, false);
                    }
                    else
                    {
                        CrystalReportViewer crViewer = (CrystalReportViewer)Visor;
                        crViewer.ReportSource = sRutaTrabajo;
                    }
                }
            }

            return bReporteGenerado;
        }


        public static string Generar_PDF(string FileName, string RutaDeDescarga, string PDF, ArrayList Datos)
        {
            string sRegresa = "";
            string sFileName_Auxiliar = "";
            string sRutaTmpImpresion = RutaDeDescarga + @"\";
            string sRuta = sRutaTmpImpresion + @"\" + FileName + ".xml"; 
            string sRutaTrabajo = sRutaTmpImpresion + @"\" + FileName + ".xmlpdf";
            string sXml = "";
            basGenerales Fg = new basGenerales();
            clsImprimir reporte = new clsImprimir(General.DatosConexion);
            clsLeer leerDts = new clsLeer();
            string sFile_Base = Path.Combine(sRutaTmpImpresion, FileName + ".xml");

            try
            {

                bool bArchivo = Fg.ConvertirStringB64EnArchivo(FileName + ".xmlpdf", sRutaTmpImpresion, PDF, true);

                //////if (XML.Contains("UTF-8"))
                //////{
                //////    XML = clsTimbrar.toUTF8(XML);
                //////    bArchivo = Fg.ConvertirStringEnArchivo(FileName + ".xml", sRutaTmpImpresion, XML, true);
                //////}
                //////else
                //////{
                //////    bArchivo = Fg.ConvertirStringB64EnArchivo(FileName + ".xml", sRutaTmpImpresion, XML, true); 
                //////}

                DataSet dts = new DataSet();
                dts.ReadXml(sRutaTrabajo);
                leerDts.DataSetClase = dts;

                if (leerDts.ExisteTabla("Extras"))
                {
                    dts.Tables.Remove("Extras");
                    leerDts.DataSetClase = dts;
                }

                if (!leerDts.ExisteTabla("Extras"))
                {
                    dts.Tables.Add(GetExtras(Datos).Copy());
                }
                leerDts.DataSetClase = dts;

                try
                {
                    sFileName_Auxiliar = ValidarStatusDocumento(leerDts.Tabla("Extras"));
                }
                catch { }

                ////sRuta = sRutaTmpImpresion + @"\" + FileName + ".xml"; 
                if (File.Exists(sRutaTrabajo))
                {
                    File.Delete(sRutaTrabajo);
                }
                dts.WriteXml(sRutaTrabajo, XmlWriteMode.WriteSchema);
                if (File.Exists(sRutaTrabajo))
                {
                    File.Delete(sRutaTrabajo);
                }


                //dts.WriteXml(sRuta, XmlWriteMode.WriteSchema); 

                dts = ValidarEstructura(dts);
                reporte.RutaReporte = DtIFacturacion.RutaCFDI_Reportes;
                reporte.NombreReporte = "CFDI_Factura";
                reporte.OrigenDeDatosDataSet = true;
                reporte.OrigenDeDatosReporte = dts;
                reporte.EnviarAImpresora = false;
                reporte.TituloReporte = "Documento electrónico";
                //reporte.Add("CantidadConLetra", General.LetraMoneda(Total)); 


                reporte.CargarReporte(false, false);
                FileName += sFileName_Auxiliar;
                sRutaTrabajo = sRutaTmpImpresion + @"\" + FileName + ".pdf";
                reporte.ExportarReporteSilencioso(sRutaTrabajo, FormatosExportacion.PortableDocFormat);


                sRegresa = sRutaTrabajo;
            }
            catch (Exception ex)
            {
                clsGrabarError.LogFileError(ex.Message);
            }

            return sRegresa;
        }


        private static string ValidarStatusDocumento(DataTable DatosExtraDocumento)
        {
            string sRegresa = "";
            clsLeer datosExtra = new clsLeer();

            datosExtra.DataTableClase = DatosExtraDocumento;
            if (datosExtra.Leer())
            {
                if (datosExtra.Campo("StatusDocumento") != "")
                {
                    sRegresa = datosExtra.Campo("StatusDocumento").ToUpper() == "C" ? "__CANCELADO" : "";
                }
            }

            return sRegresa; 
        }

        private static DataSet ValidarEstructura(DataSet Datos)
        {
            DataSet dts = Datos.Copy();
            DataTable dtConceptos;
            clsLeer leer = new clsLeer();
            leer.DataSetClase = Datos;

            ////dts = ValidarEstructura(dts, "Conceptos", "Notas", "String", "");
            ////dts = ValidarEstructura(dts, "Receptor", "Telefono", "String", "");
            //dts = ValidarEstructura(dts, "Comprobante", "MetodoDePagoDescripcion", "String", "");


            dts = ValidarEstructura(dts, "Comprobante", "MetodoDePagoDescripcion", "String", "");
            dts = ValidarEstructura(dts, "Comprobante", "FormaDePago_Descripcion", "String", "");
            dts = ValidarEstructura(dts, "Comprobante", "EfectoComprobante", "String", "");
            dts = ValidarEstructura(dts, "Comprobante", "EfectoComprobante_Descripcion", "String", "");

            dts = ValidarEstructura(dts, "Receptor", "UsoDeCFDI", "String", "");
            dts = ValidarEstructura(dts, "Receptor", "UsoDeCFDI_Descripcion", "String", "");

            dts = ValidarEstructura(dts, "Regimen", "Regimen_Descripcion", "String", "");


            dts = ValidarEstructura(dts, "Conceptos", "Descuento", "Double", "0");
            dts = ValidarEstructura(dts, "Conceptos", "Impuestos_Base", "Double", "0");
            dts = ValidarEstructura(dts, "Conceptos", "Impuesto_ImpuestoClave", "String", "");
            dts = ValidarEstructura(dts, "Conceptos", "Impuesto_Impuesto", "String", "");
            dts = ValidarEstructura(dts, "Conceptos", "Impuestos_TipoFactor", "String", "");
            dts = ValidarEstructura(dts, "Conceptos", "Impuestos_TasaOCuota", "Double", "0");
            dts = ValidarEstructura(dts, "Conceptos", "Impuestos_Importe", "Double", "0");


            dts = ValidarEstructura(dts, "Traslados", "ClaveImpuesto", "String", "");
            dts = ValidarEstructura(dts, "Traslados", "TasaOCuota", "Double", "0");

            dts = ValidarEstructura(dts, "TimbreFiscal", "RfcProveedorCertificacion", "String", "");


            return dts.Copy();
        }

        private static DataSet ValidarEstructura(DataSet Datos, string Tabla, string Columna, string TipoDeDatos, string ValorDefault)
        {
            DataSet dts = Datos.Copy();
            DataTable dtConceptos;
            clsLeer leer = new clsLeer();

            leer.DataSetClase = Datos;
            if (leer.ExisteTabla(Tabla))
            {
                dtConceptos = leer.Tabla(Tabla);
                if (!leer.ExisteTablaColumna(Tabla, Columna))
                {
                    dtConceptos.Columns.Add(Columna, System.Type.GetType(string.Format("System.{0}", TipoDeDatos)));

                    leer.DataTableClase = dtConceptos;
                    while (leer.Leer())
                    {
                        leer.GuardarDatos(Columna, ValorDefault);
                    }
                    dtConceptos = leer.DataTableClase;
                    dts.Tables.Remove(Tabla);
                    dts.Tables.Add(dtConceptos.Copy());
                }
            }

            return dts.Copy();
        }
        #endregion Impresion CFDI 

        #region Formatos de Facturas 
        public static void GenerarImpresionWebBrowser(bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo)
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 1, Visor, TipoDeDocumentoElectronico.Factura, EsTimbradoMasivo);
        }

        public static void GenerarImpresionWebBrowser(bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo)
        {
            GenerarImpresionWebBrowser(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, Visor, EsTimbradoMasivo, TipoDeDocumentoElectronico.Factura);
        }

        public static void GenerarImpresionWebBrowser(bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo, TipoDeDocumentoElectronico Tipo)
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 1, Visor, Tipo, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal(bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo)
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.Factura, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal(bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo)
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.Factura, EsTimbradoMasivo);
        }
        #endregion Formatos de Facturas

        #region Formatos de Notas de Credito 
        public static void GenerarImpresionWebBrowser_NotasDeCredito(bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo)
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 1, Visor, TipoDeDocumentoElectronico.NotaDeCredito, EsTimbradoMasivo);
        }

        public static void GenerarImpresionWebBrowser_NotasDeCredito(bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo)
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 1, Visor, TipoDeDocumentoElectronico.NotaDeCredito, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal_NotasDeCredito(bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo)
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.NotaDeCredito, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal_NotasDeCredito(bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo)
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.NotaDeCredito, EsTimbradoMasivo);
        }
        #endregion Formatos de Notas de Credito

        #region Formatos de Complementos de Pago 
        public static void GenerarImpresionWebBrowser_ComplementoDePagos(bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo)
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 1, Visor, TipoDeDocumentoElectronico.ComplementoDePago, EsTimbradoMasivo);
        }

        public static void GenerarImpresionWebBrowser_ComplementoDePagos(bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo)
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 1, Visor, TipoDeDocumentoElectronico.ComplementoDePago, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal_ComplementoDePagos(bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo)
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.ComplementoDePago, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal_ComplementoDePagos(bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo)
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.ComplementoDePago, EsTimbradoMasivo);
        }
        #endregion Formatos de Complementos de Pago



        public static string FormatoImpresion_Facturas
        {
            get
            {
                if (sFormatoImpresion_Facturas == "")
                {
                    sFormatoImpresion_Facturas = pParametros.GetValor("FormatoImpresion_Facturas");
                }
                return sFormatoImpresion_Facturas;
            }
            set
            {
                sFormatoImpresion_Facturas = value;
            }
        }

        public static string FormatoImpresion_ComplementoDePagos
        {
            get
            {
                if (sFormatoImpresion_ComplementoDePagos == "")
                {
                    sFormatoImpresion_ComplementoDePagos = pParametros.GetValor("FormatoImpresion_ComplementoDePagos");
                }
                return sFormatoImpresion_ComplementoDePagos;
            }
            set
            {
                sFormatoImpresion_ComplementoDePagos = value;
            }
        }

        public static string FormatoImpresion_NotasDeCredito
        {
            get
            {
                if (sFormatoImpresion_NotasDeCredito == "")
                {
                    sFormatoImpresion_NotasDeCredito = pParametros.GetValor("FormatoImpresion_NotasDeCredito");
                }
                return sFormatoImpresion_NotasDeCredito;
            }
            set
            {
                sFormatoImpresion_NotasDeCredito = value;
            }
        }

        public static string FormatoImpresion_Traslados
        {
            get
            {
                if (sFormatoImpresion_Traslados == "")
                {
                    sFormatoImpresion_Traslados = pParametros.GetValor("FormatoImpresion_Traslados");
                }
                return sFormatoImpresion_Traslados;
            }
            set
            {
                sFormatoImpresion_Traslados = value;
            }
        }

        public static string FormatoImpresion_Anticipo
        {
            get
            {
                if (sFormatoImpresion_Anticipo == "")
                {
                    sFormatoImpresion_Anticipo = pParametros.GetValor("FormatoImpresion_Anticipo");
                }
                return sFormatoImpresion_Anticipo;
            }
            set
            {
                sFormatoImpresion_Anticipo = value;
            }
        }

        private static string GetNombrePlantillaImpresion(TipoDeDocumentoElectronico TipoDocumento)
        {
            string sRegresa = "";

            switch (TipoDocumento)
            {
                case TipoDeDocumentoElectronico.Factura:
                    sRegresa = FormatoImpresion_Facturas;
                    break;

                case TipoDeDocumentoElectronico.NotaDeCredito:
                    sRegresa = FormatoImpresion_NotasDeCredito;
                    break;

                case TipoDeDocumentoElectronico.ComplementoDePago:
                    sRegresa = FormatoImpresion_ComplementoDePagos;
                    break;

                case TipoDeDocumentoElectronico.Traslado:
                    sRegresa = FormatoImpresion_Traslados;
                    break;

                case TipoDeDocumentoElectronico.Anticipo:
                    sRegresa = FormatoImpresion_Anticipo;
                    break;
            }

            return sRegresa;
        }

        private static DataSet ValidarEstructura(DataSet Datos, TipoDeDocumentoElectronico TipoDocumento)
        {
            DataSet dts = Datos.Copy();
            DataTable dtConceptos;
            clsLeer leer = new clsLeer();
            leer.DataSetClase = Datos;

            switch (TipoDocumento)
            {
                case TipoDeDocumentoElectronico.Factura:
                case TipoDeDocumentoElectronico.NotaDeCredito:
                    dts = ValidarEstructura__Factura_NotaDeCredito(Datos);
                    break;

                case TipoDeDocumentoElectronico.ComplementoDePago:
                    dts = ValidarEstructura__ComplementoDePago(Datos);
                    break;
            }

            return dts.Copy();
        }

        private static DataSet ValidarEstructura__Factura_NotaDeCredito(DataSet Datos)
        {
            DataSet dts = Datos.Copy();
            DataTable dtConceptos;
            clsLeer leer = new clsLeer();
            leer.DataSetClase = Datos;


            dts = ValidarEstructura(dts, "Comprobante", "MetodoDePagoDescripcion", "String", "");
            dts = ValidarEstructura(dts, "Comprobante", "FormaDePago_Descripcion", "String", "");
            dts = ValidarEstructura(dts, "Comprobante", "EfectoComprobante", "String", "");
            dts = ValidarEstructura(dts, "Comprobante", "EfectoComprobante_Descripcion", "String", "");

            dts = ValidarEstructura(dts, "Receptor", "UsoDeCFDI", "String", "");
            dts = ValidarEstructura(dts, "Receptor", "UsoDeCFDI_Descripcion", "String", "");

            dts = ValidarEstructura(dts, "Regimen", "Regimen_Descripcion", "String", "");


            dts = ValidarEstructura(dts, "Conceptos", "Impuestos_Base", "Double", "0");
            dts = ValidarEstructura(dts, "Conceptos", "Impuesto_ImpuestoClave", "String", "");
            dts = ValidarEstructura(dts, "Conceptos", "Impuesto_Impuesto", "String", "");
            dts = ValidarEstructura(dts, "Conceptos", "Impuestos_TipoFactor", "String", "");
            dts = ValidarEstructura(dts, "Conceptos", "Impuestos_TasaOCuota", "Double", "0");
            dts = ValidarEstructura(dts, "Conceptos", "Impuestos_Importe", "Double", "0");


            dts = ValidarEstructura(dts, "Traslados", "ClaveImpuesto", "String", "");
            dts = ValidarEstructura(dts, "Traslados", "TasaOCuota", "Double", "0");

            dts = ValidarEstructura(dts, "TimbreFiscal", "RfcProveedorCertificacion", "String", "");



            return dts.Copy();
        }

        private static DataSet ValidarEstructura__ComplementoDePago(DataSet Datos)
        {
            DataSet dts = Datos.Copy();
            DataTable dtConceptos;
            clsLeer leer = new clsLeer();
            leer.DataSetClase = Datos;

            return dts.Copy();
        }

        private static string GenerarImpresionInterna(bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos,
    string PDF, string XML, bool Visualizar, int Tipo, object Visor, TipoDeDocumentoElectronico TipoDocumento, bool EsTimbradoMasivo)
        {
            FileName = VistaPrevia ? "VISTA_PREVIA" : FileName;
            string sRegresa = "";
            string sFileName_Auxiliar = "";
            string sRutaTmpImpresion = DtIFacturacion.RutaCFDI_DocumentosImpresion + @"\" + DtGeneral.EmpresaDatos.RFC;
            string sRuta = sRutaTmpImpresion + @"\" + FileName + ".xml";
            string sRutaTrabajo = sRutaTmpImpresion + @"\" + FileName + ".xmlpdf";
            string sXml = "";
            string sNombrePlantilla_Impresion = GetNombrePlantillaImpresion(TipoDocumento);

            basGenerales Fg = new basGenerales();
            clsImprimir reporte = new clsImprimir(General.DatosConexion);
            clsLeer leerDts = new clsLeer();
            string sFile_Base = Path.Combine(sRutaTmpImpresion, FileName + ".xml");
            bool bArchivo = false;

            if (EsTimbradoMasivo)
            {
                Visualizar = false;
            }


            try
            {

                bArchivo = Fg.ConvertirStringB64EnArchivo(FileName + ".xmlpdf", sRutaTmpImpresion, PDF, true);

                if (!bArchivo)
                {
                    if (PDF.Contains("<?xml version="))
                    {
                        using (StreamWriter writer = new StreamWriter(string.Format(@"{0}", Path.Combine(sRutaTmpImpresion, FileName + ".xmlpdf"))))
                        {
                            writer.Write(PDF);
                            writer.Close();
                            //writer = null;
                        }
                    }
                }


                if (XML.ToUpper().Contains("UTF-8"))
                {
                    //XML = clsTimbrar.toUTF8(XML); 
                    //bArchivo = Fg.ConvertirStringEnArchivo(FileName + ".xml", sRutaTmpImpresion, XML, true);

                    XML = clsTimbrar.toUTF8(XML);
                    bArchivo = Fg.ConvertirStringEnArchivo(FileName + ".xml", sRutaTmpImpresion, XML, true);

                    if (!bArchivo)
                    {
                        using (StreamWriter writer = new StreamWriter(string.Format(@"{0}", Path.Combine(sRutaTmpImpresion, FileName + ".xml"))))
                        {
                            writer.Write(XML);
                            writer.Close();
                            //writer = null;
                        }
                    }

                }
                else
                {
                    bArchivo = Fg.ConvertirStringB64EnArchivo(FileName + ".xml", sRutaTmpImpresion, XML, true);

                    if (!bArchivo)
                    {
                        using (StreamWriter writer = new StreamWriter(string.Format(@"{0}", Path.Combine(sRutaTmpImpresion, FileName + ".xml"))))
                        {
                            writer.Write(XML);
                            writer.Close();
                            //writer = null;
                        }
                    }

                }

                DataSet dts = new DataSet();
                dts.ReadXml(sRutaTrabajo);
                leerDts.DataSetClase = dts;

                if (leerDts.ExisteTabla("Extras"))
                {
                    if (ForzarDatosAuxiliares)
                    {
                        dts.Tables.Remove("Extras");
                        leerDts.DataSetClase = dts;
                    }
                }

                if (!leerDts.ExisteTabla("Extras"))
                {
                    dts.Tables.Add(GetExtras(Datos).Copy());
                }
                leerDts.DataSetClase = dts;

                try
                {
                    sFileName_Auxiliar = ValidarStatusDocumento(leerDts.Tabla("Extras"));
                }
                catch { }



                dts = ValidarEstructura(dts, TipoDocumento);
                if (File.Exists(sRutaTrabajo))
                {
                    File.Delete(sRutaTrabajo);
                }

                dts.WriteXml(sRutaTrabajo, XmlWriteMode.WriteSchema);
                if (File.Exists(sRutaTrabajo))
                {
                    if (!DtGeneral.EsEquipoDeDesarrollo)
                    {
                        File.Delete(sRutaTrabajo);
                    }
                }

                if (sFormatoImpresion_Facturas == "")
                {
                    sFormatoImpresion_Facturas = sNombrePlantilla_Impresion;
                }

                reporte.RutaReporte = DtIFacturacion.RutaCFDI_Reportes;
                reporte.NombreReporte = "CFDI_Factura";
                reporte.NombreReporte = sFormatoImpresion_Facturas;
                reporte.NombreReporte = sNombrePlantilla_Impresion;
                reporte.OrigenDeDatosDataSet = true;
                reporte.OrigenDeDatosReporte = dts;
                reporte.EnviarAImpresora = false;
                reporte.TituloReporte = "Documento electrónico";
                //reporte.Add("CantidadConLetra", General.LetraMoneda(Total)); 

                if (Tipo == 1)
                {
                    System.Threading.Thread.Sleep(100);
                    reporte.CargarReporte(false, false);
                    FileName += sFileName_Auxiliar;
                    sRutaTrabajo = sRutaTmpImpresion + @"\" + FileName + ".pdf";
                    System.Threading.Thread.Sleep(100);
                    reporte.ExportarReporteSilencioso(sRutaTrabajo, FormatosExportacion.PortableDocFormat);
                }

                if (Tipo == 2)
                {
                    System.Threading.Thread.Sleep(100);
                    sRutaTrabajo = sRutaTmpImpresion + @"\tmp.rpt";
                    System.Threading.Thread.Sleep(100);
                    reporte.ExportarReporteSilencioso(sRutaTrabajo, FormatosExportacion.CrystalReport);
                }

                if (VistaPrevia || !pacInfo.EnProduccion)
                {
                    iTextSharpUtil.AddTextWatermark(sRutaTrabajo, "SIN VALIDEZ");
                }


                if (EsTimbradoMasivo)
                {
                    string sCopia = sDirectorioAlternoArchivosGenerados + @"\" + FileName + ".pdf";
                    File.Copy(sRutaTrabajo, sCopia, true);

                    sCopia = sDirectorioAlternoArchivosGenerados + @"\" + FileName + ".xml";
                    File.Copy(sRuta, sCopia, true);
                }

                sRegresa = sRutaTrabajo;
            }
            catch (Exception ex)
            {
                clsGrabarError.LogFileError(ex.Message);
            }

            if (Visualizar)
            {
                System.Threading.Thread.Sleep(100);
                if (Tipo == 1)
                {
                    sRutaTrabajo = sRutaTmpImpresion + @"\" + FileName + ".pdf";
                    if (Visor == null)
                    {
                        FrmVisorCFDI f = new FrmVisorCFDI(FileName, sRutaTrabajo);
                        if (frmPadreVisor != null)
                        {
                            f.ShowDialog(frmPadreVisor);
                            frmPadreVisor = null;
                        }
                        else
                        {
                            f.ShowDialog();
                        }
                    }
                    else
                    {
                        WebBrowser browser = (WebBrowser)Visor;
                        browser.Navigate(sRutaTrabajo);
                    }
                }

                if (Tipo == 2)
                {
                    if (Visor == null)
                    {
                        reporte.CargarReporte(true, false);
                    }
                    else
                    {
                        CrystalReportViewer crViewer = (CrystalReportViewer)Visor;
                        crViewer.ReportSource = sRutaTrabajo;
                    }
                }
            }

            return sRegresa;
        }

        #region Validaciones RFC y Otros
        public static string RFC_Formato(string RFC)
        {
            string sRegresa = "";

            sRegresa = RFC.Trim().Replace(" ", "");

            return sRegresa;
        }

        public static bool RFC_Valido(string RFC)
        {
            bool bRegresa = false;
            string
            lsPatron = @"^[A-ZÑ&] {3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9] [A-Z,0-9][0-9A]$";
            lsPatron = "[A-Z,Ñ,&]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?";

            Regex loRE = new Regex(lsPatron);
            bRegresa = loRE.IsMatch(RFC);

            return bRegresa;
        }
        #endregion Validaciones RFC y Otros

        #region Validacion de EMails
        public static bool ProveedorEMail_Valido(string ProveedorEmail_A_Validar)
        {
            bool bRegresa = false;
            string strAcceptableEmailAddressPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

            try
            {
                Regex regPatternEmail = new Regex(strAcceptableEmailAddressPattern);
                Match match = Regex.Match(ProveedorEmail_A_Validar.Trim(), pattern, RegexOptions.IgnoreCase);

                bRegresa = regPatternEmail.IsMatch(ProveedorEmail_A_Validar);
                bRegresa = match.Success;
            }
            catch { }

            return bRegresa;
        }

        public static bool EMail_Valido(string Email_A_Validar)
        {
            bool bRegresa = false;
            string strAcceptableEmailAddressPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            try
            {
                pattern = strAcceptableEmailAddressPattern;
                Regex regPatternEmail = new Regex(strAcceptableEmailAddressPattern);
                Match match = Regex.Match(Email_A_Validar.Trim(), pattern, RegexOptions.IgnoreCase);

                bRegresa = regPatternEmail.IsMatch(Email_A_Validar);
                bRegresa = match.Success;
            }
            catch { }

            return bRegresa;
        }

        ////public static EMail EMail_Validacion
        ////{
        ////    get
        ////    {
        ////        if (pEmail_Validacion__Emails == null)
        ////        {
        ////            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        ////            clsLeer leer = new clsLeer(ref cnn);
        ////            pEmail_Validacion__Emails = new EMail();

        ////            string sSql = string.Format(
        ////                " Select Top 1 IdSucursal, Sucursal, IdEmail, IdTipoEMail, TipoMail, Email, DescripcionDeUso, " +
        ////                " Servidor, Puerto, TiempoDeEspera, Usuario, Password, EnableSSL, EmailRespuesta, " +
        ////                " NombreParaMostrar, CC, Asunto, MensajePredeterminado, StatusEmail, StatusEmailDescripcion " +
        ////                "From vw_Sucursales_EMails (NoLock) " +
        ////                "Where IdSucursal = '{0}' and StatusEmail = 'A' ", DtGeneral.SucursalConectada);

        ////            if (leer.Exec(sSql))
        ////            {
        ////                if (leer.Leer())
        ////                {
        ////                    pEmail_Validacion__Emails = new EMail();
        ////                    pEmail_Validacion__Emails.Servidor = leer.Campo("Servidor");
        ////                    pEmail_Validacion__Emails.Puerto = leer.CampoInt("Puerto");
        ////                    pEmail_Validacion__Emails.Usuario = leer.Campo("Usuario");
        ////                    pEmail_Validacion__Emails.Password = leer.Campo("Password");
        ////                    pEmail_Validacion__Emails.EnableSSL = leer.CampoBool("EnableSSL");

        ////                    pEmail_Validacion__Emails.EmailRespuesta = leer.Campo("EmailRespuesta");
        ////                    pEmail_Validacion__Emails.NombreParaMostrar = leer.Campo("NombreParaMostrar");
        ////                    pEmail_Validacion__Emails.CC = leer.Campo("CC");
        ////                    pEmail_Validacion__Emails.Asunto = leer.Campo("Asunto");
        ////                    pEmail_Validacion__Emails.MensajePredeterminado = leer.Campo("MensajePredeterminado");
        ////                }
        ////            }

        ////        }
        ////        return pEmail_Validacion__Emails;
        ////    }
        ////    set
        ////    {
        ////        pEmail_Validacion__Emails = value;
        ////    }
        ////}
        #endregion Validacion de EMails

        #region Funciones y Procedimientos Publicos 
        public static string MarcaDeTiempo()
        {
            string sRegresa = "";
            DateTime dttime = DateTime.Now;
            basGenerales Fg = new basGenerales();

            sRegresa = Fg.PonCeros(dttime.Year, 4);
            sRegresa += Fg.PonCeros(dttime.Month, 2);
            sRegresa += Fg.PonCeros(dttime.Day, 2);

            sRegresa += "_";

            sRegresa += Fg.PonCeros(dttime.Hour, 2);
            sRegresa += Fg.PonCeros(dttime.Minute, 2);
            sRegresa += Fg.PonCeros(dttime.Second, 2);

            return sRegresa;
        }

        public static string QuitarSaltoDeLinea(string Cadena)
        {
            string sRegresa = "";
            string replaceWith = " ";

            if (Cadena == null)
            {
                Cadena = "";
            } 

            sRegresa = Cadena.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);

            return sRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private static void GetUrlServicioTimbrado()
        {
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn); 
            string sSql = "Select * From FACT_CFD_Proveedores (NoLock) Where Status = 'A' ";

            bUrlServicioTimbrado = true; 
            if (!leer.Exec(sSql))
            {
            }
            else
            {
                if (leer.Leer())
                {
                    sNombreProveedorTimbrado = leer.Campo("Nombre");
                    sUrlServicioTimbrado = leer.Campo("DireccionUrl"); 
                }
            } 
        }
        #endregion Funciones y Procedimientos Privados 
    }
}
