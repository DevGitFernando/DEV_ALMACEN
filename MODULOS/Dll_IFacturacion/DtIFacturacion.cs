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

using Dll_IFacturacion;
using Dll_IFacturacion.CFDI;  
using Dll_IFacturacion.XSA;

using DllFarmaciaSoft;
using Dll_IFacturacion.CFDI.Timbrar; 

namespace Dll_IFacturacion
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
        Version__3_3 = 33, 
        Version__4_0 = 40
    }

    public enum eTipoDeFacturacion
    {
        Ninguna = 0, Insumos = 1, Administracion = 2, 
        Manual = 3, Manual_Excel = 4 
    }

    public enum eTipoRemision
    {
        Ninguno = 0, 
        Insumo = 1, 
        Administracion = 2, 
        InsumoIncremento = 3, 
        VentaDirecta = 4, 
        InsumoIncrementoVentaDirecta = 5, 
        AdministracionVentaDirecta = 6, 
        IncrementoAdministracion = 7, 
        IncrementoAdministracionVentaDirecta = 8  
    } 

    public enum eTipoDeUnidades
    {
        Ninguna = 0, 
        Farmacias = 1, 
        FarmaciasUnidosis = 2,
        Almacenes = 3, 
        AlmacenesUnidosis = 4
    }

    public enum eTipoInsumo
    {
        Ninguno = 0, MaterialDeCuracion = 1, Medicamento = 2, Todos = 3  
    }

    public enum FuentesDeFinanciamiento_Configuracion
    {
        Ninguna = 0,

        Insumo = 1,
        Servicio,
        Documentos,

        Insumo_Clave_Farmacia,
        Servicio_Clave_Farmacia,

        Insumo_Clave_Jurisdiccion,
        Servicio_Clave_Jurisdiccion,

        ExcepcionPrecios_Insumos,
        ExcepcionPrecios_Servicio, 

        BeneficiariosJurisdiccion,
        BeneficiariosRelacionados_Jurisdiccion,
        
        Grupos_De_Remisiones
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
        private static clsDatosApp dpDatosApp = new clsDatosApp("DllFarmaciaSoft", Application.ProductVersion);

        private static bool bUrlServicioTimbrado = false;
        private static string sUrlServicioTimbrado = "";
        private static string sNombreProveedorTimbrado = "";
        private static xsaWebServices xsaServicios = new xsaWebServices();

        public static string sFormato_02 = "##############0.#0";
        public static string sFormato_04 = "##############0.####0";
        private static clsParametrosFacturacion pParametros;
        private static clsCriptografo crypto = new clsCriptografo();
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
            set {; }
        }

        public static string UrlServicioTimbrado
        {
            get
            {
                if(!bUrlServicioTimbrado)
                {
                    GetUrlServicioTimbrado();
                }

                return sUrlServicioTimbrado;
            }
            set {; }
        }
        #endregion Propiedades 

        #region Parametros 
        private static string sBaseDeDatosOperacion = "";
        private static string sRutaReportes = "";
        private static eEsquemaDeFacturacion tpEsquemaDeFacturacion = eEsquemaDeFacturacion.Ninguno;
        private static eDocumentoAdministracion tpDocumentoAdministracion = eDocumentoAdministracion.Ninguno;
        private static string sReutilizarObservacionesCapturadas = "";
        private static bool bReutilizarObservacionesCapturadas = false;

        private static string sImplementaClaveSSA_Base__Identificador = "";
        private static bool bImplementaClaveSSA_Base__Identificador = false;

        private static string sImplementaInformacionPredeterminada = "";
        private static bool bImplementaInformacionPredeterminada = false;

        private static string sImplementaMascaras = "";
        private static bool bImplementaMascaras = false;
        private static bool bForzarImplementacionMascaras = false;

        private static string sFactura_UsoCDFI = "";
        private static string sFactura_SAT_ClaveProducto__Servicio = "";
        private static string sFactura_SAT_UnidadDeMedida__Servicio = "";


        private static string sFormaDePago = "";
        private static bool bFormaDePago = false;

        private static string sPlazoDiasVenceFactura = "";
        private static int iPlazoDiasVenceFactura = 15;

        private static string sMetodoDePago = "";
        private static bool bMetodoDePago = false;

        private static string sMetodoDePagoReferencia = "";
        private static bool bMetodoDePagoReferencia = false;

        private static string sCondicionesDePago = "";


        private static string sPago_UsoCDFI = "";
        private static string sPago_FormaDePago = "";
        private static string sPago_Moneda = "";


        private static string sNotaDeCredito_UsoCDFI = "";
        private static string sNotaDeCredito_TipoDeRelacionCFDI = "";
        private static string sNotaDeCredito_MetodoDePago = "";
        private static string sNotaDeCredito_FormaDePago = "";
        private static string sNotaDeCredito_SAT_ClaveProducto = "";
        private static string sNotaDeCredito_SAT_UnidadDeMedida = "";



        private static string sVta_Plantilla_Personalizada_Remision = "";
        private static string sVta_Plantilla_Personalizada_FacturaRemision = "";
        private static string sFormatoImpresion_Facturas = "";
        private static string sFormatoImpresion_ComplementoDePagos = "";
        private static string sFormatoImpresion_NotasDeCredito = "";
        private static string sFormatoImpresion_Traslados = "";
        private static string sFormatoImpresion_Anticipo = "";


        private static string sFactura_Requiere_FF = "";
        private static string sFactura_Requiere_TipoDocto = "";
        private static string sFactura_Requiere_TipoInsumo = "";


        private static bool bFactura_Requiere_FF = false;
        private static bool bFactura_Requiere_TipoDocto = false;
        private static bool bFactura_Requiere_TipoInsumo = false;


        public static clsParametrosFacturacion Parametros
        {
            get { return pParametros; }
            set { pParametros = value; }
        }

        #region General 
        public static string BaseDeDatosOperacion
        {
            get
            {
                if (sBaseDeDatosOperacion == "")
                {
                    sBaseDeDatosOperacion = pParametros.GetValor("BaseDeDatosOperacion");
                }
                return sBaseDeDatosOperacion;
            }
            set { sBaseDeDatosOperacion = value; }
        }

        public static string RutaReportes
        {
            get
            {
                if(sRutaReportes == "")
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
                if(tpEsquemaDeFacturacion == eEsquemaDeFacturacion.Ninguno)
                {
                    tpEsquemaDeFacturacion = (eEsquemaDeFacturacion)pParametros.GetValorInt("EsquemaDeFacturacion");
                }
                return tpEsquemaDeFacturacion;
            }
        }

        public static eDocumentoAdministracion DocumentoAdministracion
        {
            get
            {
                if(tpDocumentoAdministracion == eDocumentoAdministracion.Ninguno)
                {
                    tpDocumentoAdministracion = (eDocumentoAdministracion)pParametros.GetValorInt("DocumentoAdministracion");
                }
                return tpDocumentoAdministracion;
            }
        }
        #endregion General 

        #region Remisiones 
        public static bool Factura_Requiere_FuenteDeFinanciamiento
        {
            get
            {
                if (sFactura_Requiere_FF == "")
                {
                    bFactura_Requiere_FF = pParametros.GetValorBool("Factura_Requiere_FF");
                    sFactura_Requiere_FF = bFactura_Requiere_FF.ToString();
                }
                return bFactura_Requiere_FF;
            }
            set { bFactura_Requiere_FF = value; }
        }

        public static bool Factura_Requiere_TipoDocto
        {
            get
            {
                if (sFactura_Requiere_TipoDocto == "")
                {
                    bFactura_Requiere_TipoDocto = pParametros.GetValorBool("Factura_Requiere_TipoDocto");
                    sFactura_Requiere_TipoDocto = bFactura_Requiere_TipoDocto.ToString();
                }
                return bFactura_Requiere_TipoDocto;
            }
            set { bFactura_Requiere_TipoDocto = value; }
        }

        public static bool Factura_Requiere_TipoInsumo
        {
            get
            {
                if (sFactura_Requiere_TipoInsumo == "")
                {
                    bFactura_Requiere_TipoInsumo = pParametros.GetValorBool("Factura_Requiere_TipoInsumo");
                    sFactura_Requiere_TipoInsumo = bFactura_Requiere_TipoInsumo.ToString();
                }
                return bFactura_Requiere_TipoDocto;
            }
            set { bFactura_Requiere_TipoInsumo = value; }
        }

        public static string Vta_Impresion_Personalizada_Remision
        {
            get
            {
                if (sVta_Plantilla_Personalizada_Remision == "")
                {
                    Vta_Impresion_Personalizada_Configurar();
                }
                return sVta_Plantilla_Personalizada_Remision;
            }
            set { sVta_Plantilla_Personalizada_Remision = value; }
        }

        public static string Vta_Impresion_Personalizada_FacturaRemision
        {
            get
            {
                if (sVta_Plantilla_Personalizada_FacturaRemision == "")
                {
                    Vta_Impresion_Personalizada_Configurar();
                }
                return sVta_Plantilla_Personalizada_FacturaRemision;
            }
            set { sVta_Plantilla_Personalizada_FacturaRemision = value; }
        }


        #endregion Remisiones 

        #region Facturas 
        public static bool ReutilizarObservacionesCapturadas
        {
            get
            {
                if(sReutilizarObservacionesCapturadas == "")
                {
                    bReutilizarObservacionesCapturadas = pParametros.GetValorBool("UtilizarUltimasObservaciones_FacturacionManual");
                    sReutilizarObservacionesCapturadas = bReutilizarObservacionesCapturadas.ToString();
                }
                return bReutilizarObservacionesCapturadas;
            }
        }

        public static bool ImplementaClaveSSA_Base__Identificador
        {
            get
            {
                if(sImplementaClaveSSA_Base__Identificador == "")
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
                if(sImplementaInformacionPredeterminada == "")
                {
                    bImplementaInformacionPredeterminada = pParametros.GetValorBool("ImplementaInformacionPredeterminada");
                    sImplementaInformacionPredeterminada = bImplementaInformacionPredeterminada.ToString();
                }
                return bImplementaInformacionPredeterminada;
            }
            set { bImplementaInformacionPredeterminada = value; }
        }

        public static bool ImplementaMascaras
        {
            get
            {
                if(sImplementaMascaras == "")
                {
                    bImplementaMascaras = pParametros.GetValorBool("ImplementaMascaras");
                    bForzarImplementacionMascaras = pParametros.GetValorBool("ForzarImplementacionDeMascaras");
                    sImplementaMascaras = bImplementaMascaras.ToString();
                }
                return bImplementaMascaras;
            }
            set { bImplementaMascaras = value; }
        }

        public static bool ForzarImplementacionDeMascaras
        {
            get
            {
                if(sImplementaMascaras == "")
                {
                    bImplementaMascaras = pParametros.GetValorBool("ImplementaMascaras");
                    bForzarImplementacionMascaras = pParametros.GetValorBool("ForzarImplementacionDeMascaras");
                    sImplementaMascaras = bForzarImplementacionMascaras.ToString();
                }
                return bForzarImplementacionMascaras;
            }
            set { bForzarImplementacionMascaras = value; }
        }

        public static string Factura_UsoCDFI
        {
            get
            {
                if(sFactura_UsoCDFI == "")
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
                if(sFactura_SAT_ClaveProducto__Servicio == "")
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
                if(sFactura_SAT_UnidadDeMedida__Servicio == "")
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
                if(sFormaDePago == "")
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
                if(sPlazoDiasVenceFactura == "")
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
                if(sMetodoDePago == "")
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
                if(sMetodoDePagoReferencia == "")
                {
                    sMetodoDePagoReferencia = pParametros.GetValor("Factura_MetodoDePagoReferencia");
                }
                return sMetodoDePagoReferencia;
            }
            set { sMetodoDePagoReferencia = value; }
        }

        public static string CondicionesDePago
        {
            get
            {
                if(sCondicionesDePago == "")
                {
                    sCondicionesDePago = pParametros.GetValor("Factura_CondicionesDePago");
                }
                return sCondicionesDePago;
            }
            set { sCondicionesDePago = value; }
        }
        #endregion Facturas 

        #region Complemento de Pago 
        public static string Pago_UsoCDFI
        {
            get
            {
                if(sPago_UsoCDFI == "")
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
                if(sPago_FormaDePago == "")
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
                if(sPago_Moneda == "")
                {
                    sPago_Moneda = pParametros.GetValor("Pago_Moneda");
                }
                return sPago_Moneda;
            }
            set { sPago_Moneda = value; }
        }
        #endregion Complemento de Pago 

        #region Notas de Credito 
        public static string NotaDeCredito_UsoCDFI
        {
            get
            {
                if(sNotaDeCredito_UsoCDFI == "")
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
                if(sNotaDeCredito_TipoDeRelacionCFDI == "")
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
                if(sNotaDeCredito_MetodoDePago == "")
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
                if(sNotaDeCredito_FormaDePago == "")
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
                if(sNotaDeCredito_SAT_ClaveProducto == "")
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
                if(sNotaDeCredito_SAT_UnidadDeMedida == "")
                {
                    sNotaDeCredito_SAT_UnidadDeMedida = pParametros.GetValor("NotaDeCredito_SAT_UnidadDeMedida");
                }
                return sNotaDeCredito_SAT_UnidadDeMedida;
            }
            set { sNotaDeCredito_SAT_UnidadDeMedida = value; }
        }
        #endregion Notas de Credito 
        
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

        public static void CrearDirectorio( string Directorio )
        {
            if(!Directory.Exists(Directorio))
            {
                Directory.CreateDirectory(Directorio);
            }
        }

        public static void EliminarArchivo( string Archivo )
        {
            try
            {
                if(File.Exists(Archivo))
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

        private static bool bEsTimbradoMasivo = false;
        private static string sDirectorioAlternoArchivosGenerados = "";

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

        public static string Ruta_XSLT_CadenaOriginal_Base
        {
            get { return sRutaCFDI_Estilos; }
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

        public static bool EsTimbradoMasivo
        {
            get { return bEsTimbradoMasivo; }
            set { bEsTimbradoMasivo = value; }
        }

        public static string DirectorioAlternoArchivosGenerados
        {
            ///get { }
            set { sDirectorioAlternoArchivosGenerados = value; }
        }

        public static string VersionCFDI_ToString( eVersionCFDI VersionCFDI )
        {
            string sRegresa = "";

            switch(VersionCFDI)
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

        public static double VersionCFDI_ToDouble( eVersionCFDI VersionCFDI )
        {
            double dRegresa = 0;

            switch(VersionCFDI)
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
                " From FACT_CFD_Certificados C (NoLock) " +
                " Inner Join FACT_CFD_Empresas E (NoLock) On ( C.IdEmpresa = E.IdEmpresa ) " +
                " Where C.IdEmpresa = '{0}' ", DtGeneral.EmpresaConectada);


            if(!leer.Exec(sSql))
            {
                clsGrabarError.LogFileError(leer.MensajeError);
            }
            else
            {
                if(leer.Leer())
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
                    sRuta_XSLT_CadenaOriginal = sRutaCFDI_Estilos + @"\cadenaoriginal_4_0.xslt";
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

            try
            {
                InicializarInformacionEmisor_GenerarPEM(iPEM);
            }
            catch { }

            InicializarInformacionEmisor_PAC();

            Generar_Estilos();
        }

        private static void Preparar__diCrPKI( string RutaDestino )
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
                " From FACT_CFDI_Emisores_Logos (NoLock) " +
                " Where IdEmpresa = '{0}' ", DtGeneral.EmpresaConectada);

            imgLogo = null;
            dtLogo = new DataTable("Logo");
            dtLogo.Columns.Add("Logo", System.Type.GetType("System.Byte[]"));

            if(leer.Exec(sSql))
            {
                leer.RenombrarTabla(1, "Logo");
                if(leer.Leer())
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

                    ////dtLogo.Rows.Add(objImagenes);
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
            //////clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            //////clsLeer leer = new clsLeer(ref cnn);

            //////string sSql = string.Format("Select IdEmisor, Servidor, Puerto, Usuario, Password, EnableSSL, " +
            //////    " EmailRespuesta, NombreParaMostrar, CC, Asunto, MensajePredeterminado " +
            //////    " From CFDI_Emisores_Mail (NoLock) " +
            //////    " Where IdEmisor = '{0}' ", DtGeneral.EmpresaConectada);


            //////if (leer.Exec(sSql))
            //////{
            //////    if (leer.Leer())
            //////    {
            //////        bEmailConfigurado = true;
            //////        datosEmail.Servidor = leer.Campo("Servidor");
            //////        datosEmail.Puerto = leer.CampoInt("Puerto");
            //////        datosEmail.Usuario = leer.Campo("Usuario");
            //////        datosEmail.Password = leer.Campo("Password");
            //////        datosEmail.EnableSSL = leer.CampoBool("EnableSSL");

            //////        datosEmail.EmailRespuesta = leer.Campo("EmailRespuesta");
            //////        datosEmail.NombreParaMostrar = leer.Campo("NombreParaMostrar");
            //////        datosEmail.CC = leer.Campo("CC");
            //////        datosEmail.Asunto = leer.Campo("Asunto");
            //////        datosEmail.MensajePredeterminado = leer.Campo("MensajePredeterminado");
            //////    }
            //////}
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
                " From FACT_CFDI_Emisores_PAC E (NoLock) " +
                " Inner Join FACT_CFDI_PACs P (NoLock) On ( E.IdPAC = P.IdPAC )  " +
                " Where E.IdEmpresa = '{0}' ", DtGeneral.EmpresaConectada);

            bPACConfigurado = false;
            if(leer.Exec(sSql))
            {
                if(leer.Leer())
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

                    if(pacInfo.PAC == PACs_Timbrado.VirtualSoft)
                    {
                        pacInfo.Certificado = GetFileContenido(DtIFacturacion.RutaCertificadoPEM);
                        pacInfo.Key = GetFileContenido(DtIFacturacion.RutaKeyPEM);

                    }
                }
            }
        }

        private static byte[] GetFileContenido( string RutaArchivo )
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

        private static byte[] stringToBase64ByteArray( string input )
        {
            Byte[] ret = Encoding.UTF8.GetBytes(input);
            String s = Convert.ToBase64String(ret);
            ret = Convert.FromBase64String(s);
            return ret;
        }

        public static void InicializarInformacionEmisor_GenerarPEM( int Vueltas )
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
                if(DtGeneral.EsEquipoDeDesarrollo)
                {
                    clsGrabarError.LogFileError(sParametros_Certificado);
                }

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
                if(DtGeneral.EsEquipoDeDesarrollo)
                {
                    clsGrabarError.LogFileError(sParametros_Key);
                }

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


            if(iVueltas >= 3)
            {
                clsGrabarError.LogFileError("Intentos para generar archivos .PEM excedidos.");
            }
            else
            {
                if(!File.Exists(sRutaCertificadoPEM) || !File.Exists(sRutaKeyPEM))
                {

                    System.Threading.Thread.Sleep(100);
                    iVueltas++;
                    try
                    {
                        InicializarInformacionEmisor_GenerarPEM(iVueltas);
                    }
                    catch { }
                }
            }

        }

        private static void Generar_Estilos()
        {
            ////string sCadenaOriginal_3_2 = Dll_IFacturacion.Properties.Resources.cadenaoriginal_3_2;
            ////string sCadenaOriginal_TFD_1_0 = Dll_IFacturacion.Properties.Resources.cadenaoriginal_TFD_1_0;
            ////basGenerales Fg = new basGenerales(); 

            ////Fg.ConvertirStringEnArchivo("cadenaoriginal_3_2.xslt", sRutaCFDI_Estilos, sCadenaOriginal_3_2, true);
            ////Fg.ConvertirStringEnArchivo("cadenaoriginal_TFD_1_0.xslt", sRutaCFDI_Estilos, sCadenaOriginal_TFD_1_0, true);  

        }
        #endregion CFDI

        #region Impresion CFDI
        #region Formatos de Facturas 
        public static void GenerarImpresionWebBrowser( bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 1, Visor, TipoDeDocumentoElectronico.Factura, EsTimbradoMasivo);
        }

        public static void GenerarImpresionWebBrowser( bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionWebBrowser(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, Visor, EsTimbradoMasivo, TipoDeDocumentoElectronico.Factura);
        }

        public static void GenerarImpresionWebBrowser( bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo, TipoDeDocumentoElectronico Tipo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 1, Visor, Tipo, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal( bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.Factura, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal( bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.Factura, EsTimbradoMasivo);
        }
        #endregion Formatos de Facturas

        #region Formatos de Complementos de Pago 
        public static void GenerarImpresionWebBrowser_ComplementoDePagos( bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 1, Visor, TipoDeDocumentoElectronico.ComplementoDePago, EsTimbradoMasivo);
        }

        public static void GenerarImpresionWebBrowser_ComplementoDePagos( bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 1, Visor, TipoDeDocumentoElectronico.ComplementoDePago, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal_ComplementoDePagos( bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.ComplementoDePago, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal_ComplementoDePagos( bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.ComplementoDePago, EsTimbradoMasivo);
        }
        #endregion Formatos de Complementos de Pago

        #region Formatos de Traslados 
        public static void GenerarImpresionWebBrowser_Traslados( bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 1, Visor, TipoDeDocumentoElectronico.Traslado, EsTimbradoMasivo);
        }

        public static void GenerarImpresionWebBrowser_Traslados( bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 1, Visor, TipoDeDocumentoElectronico.Traslado, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal_Traslados( bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.Traslado, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal_Traslados( bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.Traslado, EsTimbradoMasivo);
        }
        #endregion Formatos de Traslados

        #region Formatos de Notas de Credito 
        public static void GenerarImpresionWebBrowser_NotasDeCredito( bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 1, Visor, TipoDeDocumentoElectronico.NotaDeCredito, EsTimbradoMasivo);
        }

        public static void GenerarImpresionWebBrowser_NotasDeCredito( bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 1, Visor, TipoDeDocumentoElectronico.NotaDeCredito, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal_NotasDeCredito( bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.NotaDeCredito, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal_NotasDeCredito( bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.NotaDeCredito, EsTimbradoMasivo);
        }
        #endregion Formatos de Notas de Credito

        #region Formatos de Anticipos
        public static void GenerarImpresionWebBrowser_Anticipos( bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 1, Visor, TipoDeDocumentoElectronico.Anticipo, EsTimbradoMasivo);
        }

        public static void GenerarImpresionWebBrowser_Anticipos( bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, WebBrowser Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 1, Visor, TipoDeDocumentoElectronico.Anticipo, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal_Anticipos( bool VistaPrevia, string FileName, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, false, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.Anticipo, EsTimbradoMasivo);
        }

        public static void GenerarImpresionCrystal_Anticipos( bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos, string PDF, string XML, bool Visualizar, CrystalReportViewer Visor, bool EsTimbradoMasivo )
        {
            GenerarImpresionInterna(VistaPrevia, FileName, ForzarDatosAuxiliares, Datos, PDF, XML, Visualizar, 2, Visor, TipoDeDocumentoElectronico.Anticipo, EsTimbradoMasivo);
        }
        #endregion Formatos de Anticipos

        public static string FormatoImpresion_Facturas
        {
            get
            {
                if(sFormatoImpresion_Facturas == "")
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
                if(sFormatoImpresion_ComplementoDePagos == "")
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
                if(sFormatoImpresion_NotasDeCredito == "")
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
                if(sFormatoImpresion_Traslados == "")
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
                if(sFormatoImpresion_Anticipo == "")
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

        public static DataTable GetExtras( ArrayList Datos )
        {
            DataTable dt = new DataTable("Extras");
            object[] obj = new object[Datos.Count];
            int i = -1;

            foreach(DatosExtraImpresion d in Datos)
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

        public static ArrayList GetExtras( DataSet CamposAdicionales )
        {
            ArrayList Datos = new ArrayList();
            DatosExtraImpresion dato = new DatosExtraImpresion();
            clsLeer datosAdicionales = new clsLeer();
            datosAdicionales.DataSetClase = CamposAdicionales;
            datosAdicionales.Leer();

            dato = new DatosExtraImpresion();
            dato.Nombre = "StatusDocumento";
            dato.Valor = datosAdicionales.Campo("StatusDocumento");
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
            dato.Nombre = "Referencia_02";
            dato.Valor = datosAdicionales.Campo("Referencia_02");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Referencia_03";
            dato.Valor = datosAdicionales.Campo("Referencia_03");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Referencia_04";
            dato.Valor = datosAdicionales.Campo("Referencia_04");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Referencia_05";
            dato.Valor = datosAdicionales.Campo("Referencia_05");
            Datos.Add(dato);


            dato = new DatosExtraImpresion();
            dato.Nombre = "NombreDelEstablecimiento";
            dato.Valor = datosAdicionales.Campo("NombreDelEstablecimiento");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "DomicilioDelEstablecimiento";
            dato.Valor = datosAdicionales.Campo("DomicilioDelEstablecimiento");
            Datos.Add(dato);


            dato = new DatosExtraImpresion();
            dato.Nombre = "NombreDelEstablecimiento_Receptor";
            dato.Valor = datosAdicionales.Campo("NombreDelEstablecimiento_Receptor");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "DomicilioDelEstablecimiento_Receptor";
            dato.Valor = datosAdicionales.Campo("DomicilioDelEstablecimiento_Receptor");
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
            dato.Valor = General.LetraMoneda(datosAdicionales.CampoDouble("Total"));
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "FechaCancelacionCFDI";
            dato.Valor = datosAdicionales.Campo("FechaCancelacionDocumento");
            Datos.Add(dato);

            return Datos;
        }

        private static string GetNombrePlantillaImpresion( TipoDeDocumentoElectronico TipoDocumento )
        {
            string sRegresa = "";

            switch(TipoDocumento)
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

        private static string GenerarImpresionInterna( bool VistaPrevia, string FileName, bool ForzarDatosAuxiliares, ArrayList Datos,
            string PDF, string XML, bool Visualizar, int Tipo, object Visor, TipoDeDocumentoElectronico TipoDocumento, bool EsTimbradoMasivo )
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

            if(EsTimbradoMasivo)
            {
                Visualizar = false;
            }


            try
            {

                bArchivo = Fg.ConvertirStringB64EnArchivo(FileName + ".xmlpdf", sRutaTmpImpresion, PDF, true);

                if(!bArchivo)
                {
                    if(PDF.Contains("<?xml version="))
                    {
                        using(StreamWriter writer = new StreamWriter(string.Format(@"{0}", Path.Combine(sRutaTmpImpresion, FileName + ".xmlpdf"))))
                        {
                            writer.Write(PDF);
                            writer.Close();
                            //writer = null;
                        }
                    }
                }


                if(XML.ToUpper().Contains("UTF-8"))
                {
                    //XML = clsTimbrar.toUTF8(XML); 
                    //bArchivo = Fg.ConvertirStringEnArchivo(FileName + ".xml", sRutaTmpImpresion, XML, true);

                    XML = clsTimbrar.toUTF8(XML);
                    bArchivo = Fg.ConvertirStringEnArchivo(FileName + ".xml", sRutaTmpImpresion, XML, true);

                    if(!bArchivo)
                    {
                        using(StreamWriter writer = new StreamWriter(string.Format(@"{0}", Path.Combine(sRutaTmpImpresion, FileName + ".xml"))))
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

                    if(!bArchivo)
                    {
                        using(StreamWriter writer = new StreamWriter(string.Format(@"{0}", Path.Combine(sRutaTmpImpresion, FileName + ".xml"))))
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

                if(leerDts.ExisteTabla("Extras"))
                {
                    if(ForzarDatosAuxiliares)
                    {
                        dts.Tables.Remove("Extras");
                        leerDts.DataSetClase = dts;
                    }
                }

                if(!leerDts.ExisteTabla("Extras"))
                {
                    dts.Tables.Add(GetExtras(Datos).Copy());
                }
                leerDts.DataSetClase = dts;

                try
                {
                    sFileName_Auxiliar = ValidarStatusDocumento(leerDts.Tabla("Extras"));
                }
                catch { }


                ////////// Actualizar el CBB 
                if(leerDts.ExisteTabla("CBB"))
                {
                    dts.Tables.Remove("CBB");
                    dts.Tables.Add(GetCBB(dts));
                }


                dts = ValidarEstructura(dts, TipoDocumento);
                if(File.Exists(sRutaTrabajo))
                {
                    File.Delete(sRutaTrabajo);
                }

                dts.WriteXml(sRutaTrabajo, XmlWriteMode.WriteSchema);
                if(File.Exists(sRutaTrabajo))
                {
                    if(!DtGeneral.EsEquipoDeDesarrollo)
                    {
                        File.Delete(sRutaTrabajo);
                    }
                }

                if(sFormatoImpresion_Facturas == "")
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

                if(Tipo == 1)
                {
                    System.Threading.Thread.Sleep(100);
                    reporte.CargarReporte(false, false);
                    FileName += sFileName_Auxiliar;
                    sRutaTrabajo = sRutaTmpImpresion + @"\" + FileName + ".pdf";
                    System.Threading.Thread.Sleep(100);
                    reporte.ExportarReporteSilencioso(sRutaTrabajo, FormatosExportacion.PortableDocFormat);
                }

                if(Tipo == 2)
                {
                    System.Threading.Thread.Sleep(100);
                    sRutaTrabajo = sRutaTmpImpresion + @"\tmp.rpt";
                    System.Threading.Thread.Sleep(100);
                    reporte.ExportarReporteSilencioso(sRutaTrabajo, FormatosExportacion.CrystalReport);
                }

                if(VistaPrevia || !pacInfo.EnProduccion)
                {
                    iTextSharpUtil.AddTextWatermark(sRutaTrabajo, "SIN VALIDEZ");
                }


                if(EsTimbradoMasivo)
                {
                    string sCopia = sDirectorioAlternoArchivosGenerados + @"\" + FileName + ".pdf";
                    File.Copy(sRutaTrabajo, sCopia, true);

                    sCopia = sDirectorioAlternoArchivosGenerados + @"\" + FileName + ".xml";
                    File.Copy(sRuta, sCopia, true);
                }

                sRegresa = sRutaTrabajo;
            }
            catch(Exception ex)
            {
                clsGrabarError.LogFileError(ex.Message);
            }

            if(Visualizar)
            {
                System.Threading.Thread.Sleep(100);
                if(Tipo == 1)
                {
                    sRutaTrabajo = sRutaTmpImpresion + @"\" + FileName + ".pdf";
                    if(Visor == null)
                    {
                        FrmVisorCFDI f = new FrmVisorCFDI(FileName, sRutaTrabajo);
                        if(frmPadreVisor != null)
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

                if(Tipo == 2)
                {
                    if(Visor == null)
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

        public static string Generar_PDF( string FileName, string RutaDeDescarga, string PDF, ArrayList Datos )
        {
            return Generar_PDF(FileName, RutaDeDescarga, PDF, Datos, TipoDeDocumentoElectronico.Factura);
        }

        public static string Generar_PDF( string FileName, string RutaDeDescarga, string PDF, ArrayList Datos, TipoDeDocumentoElectronico TipoDocumento )
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

                if(leerDts.ExisteTabla("Extras"))
                {
                    dts.Tables.Remove("Extras");
                    leerDts.DataSetClase = dts;
                }

                if(!leerDts.ExisteTabla("Extras"))
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
                if(File.Exists(sRutaTrabajo))
                {
                    File.Delete(sRutaTrabajo);
                }
                dts.WriteXml(sRutaTrabajo, XmlWriteMode.WriteSchema);
                if(File.Exists(sRutaTrabajo))
                {
                    if(!DtGeneral.EsEquipoDeDesarrollo)
                    {
                        File.Delete(sRutaTrabajo);
                    }
                }


                //dts.WriteXml(sRuta, XmlWriteMode.WriteSchema); 


                if(sFormatoImpresion_Facturas == "")
                {
                    sFormatoImpresion_Facturas = FormatoImpresion_Facturas;
                }

                reporte.RutaReporte = DtIFacturacion.RutaCFDI_Reportes;
                reporte.NombreReporte = "CFDI_Factura";
                reporte.NombreReporte = sFormatoImpresion_Facturas;
                reporte.NombreReporte = GetNombrePlantillaImpresion(TipoDocumento);
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
            catch(Exception ex)
            {
                clsGrabarError.LogFileError(ex.Message);
            }

            return sRegresa;
        }

        private static string ValidarStatusDocumento( DataTable DatosExtraDocumento )
        {
            string sRegresa = "";
            clsLeer datosExtra = new clsLeer();

            datosExtra.DataTableClase = DatosExtraDocumento;
            if(datosExtra.Leer())
            {
                if(datosExtra.Campo("StatusDocumento") != "")
                {
                    sRegresa = datosExtra.Campo("StatusDocumento").ToUpper() == "C" ? "__CANCELADO" : "";
                }
            }

            return sRegresa;
        }

        public static DataTable GetCBB( DataSet Datos )
        {
            ////DataTable dtCBB = Datos.Tables["CBB"].Clone();
            Dll_IFacturacion.CFDI.CFDFunctions.cMain getCBB = new Dll_IFacturacion.CFDI.CFDFunctions.cMain();
            Dll_IFacturacion.CFDI.geCFD.clsProccess proccess = new Dll_IFacturacion.CFDI.geCFD.clsProccess();


            string sURL_Validacion_SAT = @"https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx";
            string strSelloDigital_Segmento = "";
            string sRFC_Emisor = "";
            string sRFC_Receptor = "";
            double dTotal = 0;
            string sUUID = "";

            bool bRegresa = false;
            string sXmlTimbrado = "";
            string sCadenaOriginalSAT = "";
            string ruta = "";


            object[] objImagenes = new object[1];
            DataTable leerCBB = new DataTable();
            clsLeer datosCFDI_Base = new clsLeer();
            clsLeer datosCFDI = new clsLeer();
            leerCBB = new DataTable("CBB");
            leerCBB.Columns.Add("CBB", System.Type.GetType("System.Byte[]"));


            try
            {
                datosCFDI_Base.DataSetClase = Datos;

                datosCFDI.DataTableClase = datosCFDI_Base.Tabla("Emisor");
                datosCFDI.Leer();
                sRFC_Emisor = datosCFDI.Campo("RFC");


                datosCFDI.DataTableClase = datosCFDI_Base.Tabla("Receptor");
                datosCFDI.Leer();
                sRFC_Receptor = datosCFDI.Campo("RFC");

                datosCFDI.DataTableClase = datosCFDI_Base.Tabla("Comprobante");
                datosCFDI.Leer();
                dTotal = datosCFDI.CampoDouble("Total");


                datosCFDI.DataTableClase = datosCFDI_Base.Tabla("TimbreFiscal");
                datosCFDI.Leer();

                sUUID = datosCFDI.Campo("UUID");
                strSelloDigital_Segmento = datosCFDI.Campo("SelloCFD");
                strSelloDigital_Segmento = General.Fg.Right(strSelloDigital_Segmento, 8);

                ruta = proccess.getTempFileName();

                if(!getCBB.uiCBB.getImgCBB(sURL_Validacion_SAT, sRFC_Emisor, sRFC_Receptor, dTotal.ToString(), sUUID, ruta, strSelloDigital_Segmento, eVersionCFDI.Version__3_3))
                {
                    bRegresa = false;
                }
                else
                {
                    objImagenes[0] = File.ReadAllBytes(ruta);
                    leerCBB.Rows.Add(objImagenes);
                }

                DtIFacturacion.EliminarArchivo(ruta);
            }
            catch
            {
                bRegresa = false;
            }

            return leerCBB;
        }

        /// <summary>
        /// Validar que existan los campos agregados posteriormente a la liberación del módulo
        /// </summary>
        /// <param name="Datos"></param>
        /// <returns></returns>
        private static DataSet ValidarEstructura( DataSet Datos, TipoDeDocumentoElectronico TipoDocumento )
        {
            DataSet dts = Datos.Copy();
            DataTable dtConceptos;
            clsLeer leer = new clsLeer();
            leer.DataSetClase = Datos;

            switch(TipoDocumento)
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

        private static DataSet ValidarEstructura__Factura_NotaDeCredito( DataSet Datos )
        {
            DataSet dts = Datos.Copy();
            DataTable dtConceptos;
            clsLeer leer = new clsLeer();
            leer.DataSetClase = Datos;


            dts = ValidarEstructura(dts, "Comprobante", "MetodoDePagoDescripcion", "String", "");
            dts = ValidarEstructura(dts, "Comprobante", "FormaDePago_Descripcion", "String", "");
            dts = ValidarEstructura(dts, "Comprobante", "EfectoComprobante", "String", "");
            dts = ValidarEstructura(dts, "Comprobante", "EfectoComprobante_Descripcion", "String", "");
            dts = ValidarEstructura(dts, "Comprobante", "TipoDeRelacion", "String", "");
            dts = ValidarEstructura(dts, "Comprobante", "TipoDeRelacion_Descripcion", "String", "");



            dts = ValidarEstructura(dts, "Receptor", "UsoDeCFDI", "String", "");
            dts = ValidarEstructura(dts, "Receptor", "UsoDeCFDI_Descripcion", "String", "");
            dts = ValidarEstructura(dts, "Receptor", "RegimenFiscal", "String", "");
            dts = ValidarEstructura(dts, "Receptor", "RegimenFiscal_Descripcion", "String", "");



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

            dts = ValidarEstructura(dts, "Extras", "Referencia_02", "String", "");
            dts = ValidarEstructura(dts, "Extras", "Referencia_03", "String", "");
            dts = ValidarEstructura(dts, "Extras", "Referencia_04", "String", "");
            dts = ValidarEstructura(dts, "Extras", "Referencia_05", "String", "");
            dts = ValidarEstructura(dts, "Extras", "NombreDelEstablecimiento", "String", "");
            dts = ValidarEstructura(dts, "Extras", "DomicilioDelEstablecimiento", "String", "");
            dts = ValidarEstructura(dts, "Extras", "NombreDelEstablecimiento_Receptor", "String", "");
            dts = ValidarEstructura(dts, "Extras", "DomicilioDelEstablecimiento_Receptor", "String", "");

            return dts.Copy();
        }

        private static DataSet ValidarEstructura__ComplementoDePago( DataSet Datos )
        {
            DataSet dts = Datos.Copy();
            DataTable dtConceptos;
            clsLeer leer = new clsLeer();
            leer.DataSetClase = Datos;


            dts = ValidarEstructura(dts, "Receptor", "RegimenFiscal", "String", "");
            dts = ValidarEstructura(dts, "Receptor", "RegimenFiscal_Descripcion", "String", "");


            return dts.Copy();
        }

        private static DataSet ValidarEstructura( DataSet Datos, string Tabla, string Columna, string TipoDeDatos, string ValorDefault )
        {
            DataSet dts = Datos.Copy();
            DataTable dtConceptos;
            clsLeer leer = new clsLeer();

            leer.DataSetClase = Datos;
            if(leer.ExisteTabla(Tabla))
            {
                dtConceptos = leer.Tabla(Tabla);
                if(!leer.ExisteTablaColumna(Tabla, Columna))
                {
                    dtConceptos.Columns.Add(Columna, System.Type.GetType(string.Format("System.{0}", TipoDeDatos)));

                    leer.DataTableClase = dtConceptos;
                    while(leer.Leer())
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

        #region Redondeos 
        ////public static double Redondear( double Valor )
        ////{
        ////    return Redondear(Valor, iDecimales);
        ////}

        public static double Redondear( double Valor, int Decimales )
        {
            double dRetorno = 0;
            string sValor = Valor.ToString("###################0.#####0");
            int iPunto = sValor.IndexOf(".") + (1 + Decimales);

            try
            {
                sValor = General.Fg.Mid(sValor, 1, iPunto);
                Valor = Convert.ToDouble("0" + sValor);
                dRetorno = Math.Round(Valor, Decimales);
            }
            catch { }

            return dRetorno;
        }

        public static double Truncate( double Valor, int Decimales )
        {
            return (double)TruncateDecimal(Valor, Decimales);
        }

        public static decimal TruncateDecimal( double Valor, int Decimales )
        {
            decimal dRegresa = 0;

            double dRegresa_aux = 0;
            decimal power = 0;

            ////double dRegresa_Auxiliar = Valor;
            ////decimal step = (decimal)Math.Pow(10, Decimales);
            ////long tmp = (long)Math.Truncate(step * (decimal)Valor);

            ////try
            ////{
            ////    dRegresa = (double)(tmp / step);
            ////}
            ////catch { }

            try
            {
                power = (decimal)Math.Pow(10, Decimales);
                dRegresa = (decimal)(Math.Truncate((decimal)Valor * power) / power);

                //dRegresa = (decimal)(Math.Truncate(Valor * Math.Pow(10, Decimales)) / Math.Pow(10, Decimales));
            }
            catch { }

            return dRegresa; // (double)(tmp / step);
        }
        #endregion Redondeos 


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

        public static string QuitarTabuladores(string Cadena)
        {
            string sRegresa = "";
            string replaceWith = " ";

            if (Cadena == null)
            {
                Cadena = "";
            }

            //sRegresa = Cadena.Replace("\t", replaceWith);
            sRegresa = Cadena.Replace("\n\t", replaceWith).Replace("\t\n", replaceWith).Replace("\t", replaceWith);

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

        #region Validar Permiso Especial Almacen

        public static bool Facturacion_PermisoEspecial()
        {
            bool bRegresa = false;


            string sMD5 = "", sDesencripcion = "";
            string[] sArreglo;

            string sFechaVigencia = "";

            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            clsGrabarError Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, "DtGeneral()");


            string sSql = string.Format(" Select C.* " +
                " From CFG_CFDI_PermisosEspeciales  C (NoLock) " +
                " Where C.IdEstado = '{0}' and C.IdFarmacia = '{1}'",
                 DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if(!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "Facturacion_PermisoEspecial()");
            }
            else
            {
                if(leer.Leer())
                {
                    sMD5 = leer.Campo("MD5");
                    sDesencripcion = crypto.Desencriptar(sMD5, 17);

                    sArreglo = sDesencripcion.Split('|');

                    sFechaVigencia = sArreglo[3];

                    try
                    {
                        if(
                            sArreglo[0] == DtGeneral.EstadoConectado &&
                            sArreglo[1] == DtGeneral.FarmaciaConectada &&
                            sArreglo[2].ToUpper() == "A" 
                            )
                        {
                            bRegresa = true;
                        }
                    }
                    catch { }
                }
            }

            return bRegresa;
        }

        #endregion Validar Permiso Especial Almacen

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

        private static void Vta_Impresion_Personalizada_Configurar()
        {
            sVta_Plantilla_Personalizada_Remision = pParametros.GetValor("FormatoImpresion_Remisiones");
            sVta_Plantilla_Personalizada_FacturaRemision = pParametros.GetValor("FormatoImpresion_FacturasRemisiones");
        }
        #endregion Funciones y Procedimientos Privados 
    }
}
