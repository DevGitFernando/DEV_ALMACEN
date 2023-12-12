using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.SistemaOperativo;

using SC_SolutionsSystem.Reportes;
using DllPedidosClientes.Reporteador;
using DllPedidosClientes.Usuarios_y_Permisos;

using DllFarmaciaSoft.SistemaOperativo; 

namespace DllPedidosClientes
{
    #region Enumeradores Publicos
    /// <summary>
    /// Especifica el Tipo de Cliente Externo Conectado 
    /// </summary>
    public enum TipoDeClienteExterno
    {
        Ninguno = 0,
        Administracion_Regional = 1,
        Administracion_Unidad = 2
    }

    public enum TipoDeConexion 
    {
        Ninguno = 0,
        Regional = 1,
        Unidad = 2,
        Unidad_Directo = 3

    }
    #endregion Enumeradores Publicos

    public static class DtGeneralPedidos
    {
        #region Declaracion de Variables
        //private static string sModulo = "DllPedidosClientes";
        //private static string sVersion = "1.0.0.0";

        private static basGenerales Fg = new basGenerales();
        private static clsEdoIniManager myXmlEdoInformacion;
        // private static string sIdOficinaCentral = "OC";
        // private static string sIdOficinaCentralNombre = "Oficina Central";
        // private static string sIdFarmaciaCentral = "00SC";
        // private static string sIdFarmaciaCentralNombre = "Central";

        // private static string sIdEmpresa = "001";
        // private static string sNombreEmpresa = "Intercontinental de Medicamentos";

        private static string sIdEstado = "";
        private static string sNombreEstado = "";
        private static string sClaveRenapo = "";
        private static string sIdFarmacia = "";
        private static string sNombreFarmacia = "";
        private static string sIdJurisdiccion = "";
        private static string sNombreJurisdiccion = "";

        private static string sIdCliente = "";
        private static string sClienteNombre = "";
        private static string sIdSubCliente = "";
        private static string sSubClienteNombre = "";

        // private static bool bManejaVentaPublicoGral = false;
        private static bool bManejaControlados = false;
        // private static bool bEsTipoAlmacen = false;
        // private static bool bEsEmpDeConsignacion = false;

        // private static bool bOficinaCentral = false;
        // private static bool bConfOficinaCentral = false;

        private static string sIdPersonalConectado = "";
        private static string sNombrePersonal = "";
        private static string sLoginUsuario = "";
        private static string sPassUser = "";
        private static bool bEsUsuarioTipoAdministrador = false;
        private static string sRutaReportes = "";

        private static DateTime dFechaMenorSistema = new DateTime();
        private static string[] sListaArboles = { "CFGC", "CFGN", "CFGS" };

        private static clsDatosApp dpDatosApp = new clsDatosApp("DllFarmaciaSoft", Application.ProductVersion);
        public static string RutaLogo = Application.StartupPath + @"\SII_LOGO.jpg";
        // private static clsInfoUsuario infUsuario = new clsInfoUsuario();  

        public static string CfgIniOficinaCentral = "OficinaCentral";
        public static string CfgIniPuntoDeVenta = "FarmaciaPtoVta";
        public static string CfgIniSII_Regional = "SII-Regional";
        public static string CfgIniSII_Unidad = "SII-Unidad";

        private static string sUrl_Servidor_Regional = "";
        public static bool bUrlServidorCentralRegional = false;

        private static string sUrl_Servidor_Central = "";
        public static bool bUrlServidorCentral = false;
        private static bool bEsEquipoDeDesarrollo = File.Exists(General.UnidadSO + @":\\Dev.xml");
        private static string sIdPublicoGeneral = "0001";//Esta variable se utiliza para saber el Id del Cliente Publico General.

        private static TipoDeClienteExterno tpTipoDeClienteConectado = TipoDeClienteExterno.Ninguno;
        private static TipoDeConexion tpTipoConexion = TipoDeConexion.Ninguno;

        private static string sEncPrincipal = "";
        private static string sEncSecundario = "";
        private static DataSet dtsListaProductosSectorSalud;

        // Para Auditoria 
        private static string sIdSesion = "";

        private static clsDatosWebService datosDeWebService = new clsDatosWebService();
        private static clsDatosWebService datosDeWebServiceUpdater = new clsDatosWebService();

        // private static string MensajeContinuar = "Este Proceso puede tardar varios minutos.. ¿ Desea Continuar ?";
        private static string sMensajeDeProceso = "Este proceso puede demorar algunos minutos, ¿ Desea continuar ?";
        #endregion Declaracion de Variables

        static DtGeneralPedidos()
        {
            dFechaMenorSistema = Convert.ToDateTime("2009-05-01");
            //// SC_SolutionsSystem.Usuarios_y_Permisos.clsAbrirForma.AssemblyActual("DllFarmaciaSoft"); 
            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());
            //dpDatosApp = new clsDatosApp("DllFarmaciaSoft", "1.0.5.1");
        }

        #region Propiedades
        #region Urls
        public static void InformacionConexion()
        {
            FrmInformacionConexion f = new FrmInformacionConexion(General.Url, General.DatosConexion);
            f.ShowDialog();
        }

        public static string UrlServidorRegional
        {
            get
            {
                if (bUrlServidorCentralRegional)
                {
                    Obtener_Url_ServidorCentralRegional();
                    bUrlServidorCentralRegional = false;
                }
                return sUrl_Servidor_Regional;
            }
            set { sUrl_Servidor_Regional = value; }
        }

        private static void Obtener_Url_ServidorCentralRegional()
        {
            ////clsConsultas Consultas;
            ////Consultas = new clsConsultas(General.DatosConexion, General.Modulo, "DtGeneral", General.Version, false);
            ////sUrl_Servidor_Regional = Consultas.Obtener_Url_ServidorCentralRegional("Obtener_Url_ServidorCentralRegional");
        }

        public static string UrlServidorCentral
        {
            get
            {
                if (bUrlServidorCentral)
                {
                    Obtener_Url_ServidorCentral();
                    bUrlServidorCentral = false;
                }
                return sUrl_Servidor_Central;
            }
            set { sUrl_Servidor_Central = value; }
        }

        private static void Obtener_Url_ServidorCentral()
        {
            //////clsDatosCliente DatosCliente;
            //////DatosCliente = new clsDatosCliente(dpDatosApp, "DtGeneral.cs", "");
            //////string sQuery = "", sInicio = "Set DateFormat YMD ";

            //////SC_SolutionsSystem.Errores.clsGrabarError Error;
            //////Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, "DtGeneral.cs");

            //////Obtener_Url_ServidorCentralRegional();
            //////clsLeerWebExt myLeerWeb = new clsLeerWebExt(sUrl_Servidor_Regional, "FarmaciaPtoVta", DatosCliente);

            //////sQuery = sInicio + string.Format(" Select * From vw_OficinaCentral_Url(NoLock)");
            //////if (myLeerWeb.Exec(sQuery))
            //////{
            //////    if (myLeerWeb.Leer())
            //////    {
            //////        sUrl_Servidor_Central = myLeerWeb.Campo("Url");
            //////    }
            //////    else
            //////    {
            //////        General.msjUser("Url de Servidor Central no encontrada, verifique.");
            //////    }
            //////}
            //////else
            //////{
            //////    Error.GrabarError(myLeerWeb, "Obtener_Url_ServidorCentral");
            //////    General.msjUser("Ocurrió un error al obtener la Url del Servidor Central");
            //////}
        }
        #endregion Urls

        #region Reportes
        public static string RutaReportes
        {
            get { return sRutaReportes; }
            set { sRutaReportes = value; }
        }

        public static string EncabezadoPrincipal
        {
            get { return sEncPrincipal; }
            set { sEncPrincipal = value; }
        }

        public static string EncabezadoSecundario
        {
            get { return sEncSecundario; }
            set { sEncSecundario = value; }
        }
        #endregion Reportes

        #region Servicios Web
        public static clsDatosWebService DatosDeServicioWeb
        {
            get { return datosDeWebService; }
            set { datosDeWebService = value; }
        }

        public static clsDatosWebService DatosDeServicioWebUpdater
        {
            get { return datosDeWebServiceUpdater; }
            set { datosDeWebServiceUpdater = value; }
        }
        #endregion Servicios Web

        #region Informacion Cliente - SubCliente
        /* 
        private static string sIdCliente = "";
        private static string sClienteNombre = "";
        private static string sIDSubCliente = "";
        private static string sSubClienteNombre = "";
        */

        public static string Cliente
        {
            get { return sIdCliente; }
            set
            {
                if (sIdCliente == "")
                {
                    sIdCliente = value;
                }
            }
        }

        public static string ClienteNombre
        {
            get { return sClienteNombre; }
            set
            {
                if (sClienteNombre == "")
                {
                    sClienteNombre = value;
                }
            }
        }

        public static string SubCliente
        {
            get { return sIdSubCliente; }
            set
            {
                if (sIdSubCliente == "")
                {
                    sIdSubCliente = value;
                }
            }
        }

        public static string SubClienteNombre
        {
            get { return sSubClienteNombre; }
            set
            {
                if (sSubClienteNombre == "")
                {
                    sSubClienteNombre = value;
                }
            }
        }
        #endregion Informacion Cliente - SubCliente

        #region Informacion
        public static clsDatosApp DatosApp
        {
            get { return dpDatosApp; }
            set { dpDatosApp = value; }
        }

        public static string Modulo
        {
            get { return dpDatosApp.Modulo; }
        }

        public static string Version
        {
            get { return dpDatosApp.Version; }
        }

        public static string EstadoConectado
        {
            get { return sIdEstado; }
            set { sIdEstado = Fg.PonCeros(value, 2); }
        }

        public static string EstadoConectadoNombre
        {
            get { return sNombreEstado; }
            set { sNombreEstado = value; }
        }

        public static string ClaveRENAPO
        {
            get { return sClaveRenapo; }
            set { sClaveRenapo = value.ToUpper(); }
        }

        public static string FarmaciaConectada
        {
            get { return sIdFarmacia; }
            set { sIdFarmacia = Fg.PonCeros(value, 4); }
        }

        public static string FarmaciaConectadaNombre
        {
            get { return sNombreFarmacia; }
            set { sNombreFarmacia = value; }
        }

        public static string Jurisdiccion
        {
            get { return sIdJurisdiccion; }
            set { sIdJurisdiccion = value; }
        }

        public static string JurisdiccionNombre
        {
            get { return sNombreJurisdiccion; }
            set { sNombreJurisdiccion = value; }
        }

        public static clsEdoIniManager XmlEdoConfig
        {
            get
            {
                if (myXmlEdoInformacion == null)
                    myXmlEdoInformacion = new clsEdoIniManager();

                return myXmlEdoInformacion;
            }
            set { myXmlEdoInformacion = value; }
        }

        public static string IdPersonal
        {
            get { return sIdPersonalConectado; }
            set { sIdPersonalConectado = value; }
        }

        public static string NombrePersonal
        {
            get { return sNombrePersonal; }
            set { sNombrePersonal = value; }
        }

        public static string LoginUsuario
        {
            get { return sLoginUsuario; }
            set { sLoginUsuario = value; }
        }

        public static string PasswordUsuario
        {
            get { return sPassUser; }
            set
            {
                if (sPassUser == "")
                    sPassUser = value;
            }
        }

        public static bool EsAdministrador
        {
            get { return bEsUsuarioTipoAdministrador; }
            set { bEsUsuarioTipoAdministrador = value; }
        }

        public static DateTime FechaMinimaSistema
        {
            get { return dFechaMenorSistema; }
            set { dFechaMenorSistema = value; }
        }

        public static TipoDeClienteExterno ClienteConectado
        {
            get { return tpTipoDeClienteConectado; }
            set { tpTipoDeClienteConectado = value; }
        }

        public static bool ManejaControlados
        {
            get { return bManejaControlados; }
            set { bManejaControlados = value; }
        }

        public static bool EsEquipoDeDesarrollo
        {
            get { return bEsEquipoDeDesarrollo; }
        }

        public static string PublicoGeneral
        {
            get { return sIdPublicoGeneral; }
            set { sIdPublicoGeneral = Fg.PonCeros(value, 4); }
        }

        public static string IdSesion
        {
            get
            {
                sIdSesion = sIdSesion == "" ? "*" : sIdSesion;
                return sIdSesion;
            }
            set
            {
                sIdSesion = value == "" ? "*" : value;
            }
        }
        #endregion Informacion
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public static void RevisarVersion()
        {
            string[] s = { "" };
            RevisarVersion(s);
        }

        public static void RevisarVersion(string[] Modulos)
        {
            // SC_SolutionsSystem.SistemaOperativo.FrmInformacion x = new SC_SolutionsSystem.SistemaOperativo.FrmInformacion(true);
            DllFarmaciaSoft.SistemaOperativo.FrmInformacion FrmVersion = new DllFarmaciaSoft.SistemaOperativo.FrmInformacion(true); 
 
            FrmVersion.Add("SC_CompressLib");
            FrmVersion.Add("SC_ControlsCS");
            FrmVersion.Add("SC_SolutionsSystem");
            FrmVersion.Add("DllFarmaciaSoft");
            FrmVersion.Add("DllTransferenciaSoft");

            foreach (string z in Modulos)
            {
                if (z != "")
                {
                    FrmVersion.Add(z);
                }
            }

            FrmVersion.Procesar();
        }

        public static void MostrarCambiarPasswordUsuario()
        {
            FrmCambiarPassword pass = new FrmCambiarPassword();
            pass.ShowDialog();
        }

        ////public static void MostrarLogErrores()
        ////{
        ////    if (DtGeneral.EsAdministrador)
        ////    {
        ////        FrmListadoDeErrores ex = new FrmListadoDeErrores();
        ////        ex.ShowDialog();
        ////    }
        ////    else
        ////    {
        ////        General.msjUser("El usuario conectado no tiene permisos para accesar a esta opción.");
        ////    }  
        ////}
        #endregion Funciones y Procedimientos Publicos

        #region Reporteador
        public static bool GenerarReporte(string Url, clsImprimir Reporte, string Estado, string Farmacia, DataSet InformacionReporteWeb, DataSet InformacionCliente)
        {
            return GenerarReporte(Url, Reporte, Estado, Farmacia, InformacionReporteWeb, InformacionCliente, false);
        }

        public static bool GenerarReporte(string Url, clsImprimir Reporte, string Estado, string Farmacia, DataSet InformacionReporteWeb, DataSet InformacionCliente, bool EsRegional)
        {
            bool bRegresa = false;

            clsReporteador Reporteador = new clsReporteador(Reporte, Url, Estado, Farmacia, InformacionReporteWeb, InformacionCliente, EsRegional);
            Reporteador.ImpresionViaWeb = true;
            Reporteador.Url = Url;
            bRegresa = Reporteador.GenerarReporte();

            return bRegresa;
        }

        ////public static bool GenerarReporteRemoto(clsConexionClienteUnidad ConexionUnidad, clsImprimir Reporte, clsDatosCliente DatosTerminal)
        ////{
        ////    return GenerarReporteRemoto(General.Url, ConexionUnidad, Reporte, DatosTerminal);
        ////}

        ////public static bool GenerarReporteRemoto(string Url, 
        ////    clsConexionClienteUnidad ConexionUnidad, clsImprimir Reporte, clsDatosCliente DatosTerminal)
        ////{
        ////    bool bRegresa = false;

        ////    clsReporteador Reporteador = new clsReporteador(Reporte, DatosTerminal, ConexionUnidad);
        ////    Reporteador.ImpresionViaWeb = true;
        ////    Reporteador.Url = Url;
        ////    bRegresa = Reporteador.GenerarReporteRemoto();

        ////    return bRegresa;
        ////}

        #endregion Reporteador

        #region Obtener Informacion de Memoria
        public static DataSet ListadoDeProductosSectorSalud
        {
            get { return dtsListaProductosSectorSalud; }
            set { dtsListaProductosSectorSalud = value; }
        }
        #endregion Obtener Informacion de Memoria

        #region Estados y Farmacias
        static DataSet dtsEstados = new DataSet();
        static DataSet dtsFarmacias = new DataSet();
        static DataSet dtsJurisdicciones = new DataSet();

        public static bool ObtenerEstados_Farmacias()
        {
            bool bRegresa = false;
            string sFiltroUnidades = ""; //" and F.IdTipoUnidad In ( '0001', '0002', '0003', '0004' ) "; 
 
            try
            {
                clsDatosCliente DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, "DtGeneralPedidos", "ObtenerEstados_Farmacias");
                clsLeerWeb leerWeb = new clsLeerWeb(General.Url, General.ArchivoIni, DatosCliente);
                string sSql =
                    "Select distinct IdEstado, Estado, EdoStatus From vw_Farmacias (NoLock) " +
                    " Where EdoStatus = 'A' Order By IdEstado ";

                if (!leerWeb.Exec(sSql))
                {
                    // Error.GrabarError(leerWeb, "CargarEstados()");
                    // General.msjError("Ocurrió un error al obtener la lista de Estados.");
                }
                else
                {
                    dtsEstados = leerWeb.DataSetClase;

                    // Cliente Administracion Regional 
                    sSql = string.Format(" Select F.IdFarmacia, F.Farmacia as NombreFarmacia, (F.IdFarmacia + ' -- ' + F.Farmacia) as Farmacia, 0 as Procesar " +
                        " From vw_Farmacias F (NoLock) " +
                        " Inner Join vw_Farmacias_Urls U (NoLock) On ( F.IdEstado = U.IdEstado and F.IdFarmacia = U.IdFarmacia and U.StatusUrl = 'A' ) " +
                        " Where F.IdEstado = '{0}' And F.IdFarmacia <> '{1}' and F.Status = 'A' {2} " +
                        " Order By F.IdFarmacia ", DtGeneralPedidos.EstadoConectado, General.EntidadConectada, sFiltroUnidades);

                    if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
                    {
                        sSql = string.Format(" Select F.IdFarmacia, F.Farmacia as NombreFarmacia, (F.IdFarmacia + ' -- ' + F.Farmacia) as Farmacia, 0 as Procesar " +
                            " From vw_Farmacias F (NoLock) " +
                            " Inner Join vw_Farmacias_Urls U (NoLock) On ( F.IdEstado = U.IdEstado and F.IdFarmacia = U.IdFarmacia and U.StatusUrl = 'A' ) " +
                            " Where F.IdEstado = '{0}' And F.IdFarmacia = '{1}' and F.Status = 'A' {2} " +
                            " Order By F.IdFarmacia ", DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada, sFiltroUnidades);
                    }

                    if (!leerWeb.Exec(sSql))
                    {
                    }
                    else
                    {
                        dtsFarmacias = leerWeb.DataSetClase;
                        sSql = string.Format(
                            " Select IdEstado, IdJurisdiccion, Descripcion as Jurisdiccion, (IdJurisdiccion + '  -- ' + Descripcion ) as NombreJurisdiccion" +
                            " From CatJurisdicciones (NoLock) " +
                            " Where IdEstado = '{0}' Order by IdJurisdiccion ",
                            DtGeneralPedidos.EstadoConectado);
                        if (!leerWeb.Exec(sSql))
                        {
                        }
                        else
                        {
                            dtsJurisdicciones = leerWeb.DataSetClase;
                        }
                    }
                }


                bRegresa = true;
            }
            catch { }

            return bRegresa;
        }

        public static DataSet Estados
        {
            get { return dtsEstados; }
            set { ; }
        }

        public static DataSet Farmacias
        {
            get { return dtsFarmacias; }
            set { ; }
        }

        public static DataSet Jurisdiscciones
        {
            get { return dtsJurisdicciones; }
            set { ; }
        }
        #endregion Estados y Farmacias

        #region Tipo de Conexion
        public static TipoDeConexion TipoDeConexion
        {
            get { return tpTipoConexion; }
            set
            {
                if (value != TipoDeConexion.Ninguno)
                {
                    tpTipoConexion = value;
                }
            }
        }
        #endregion Tipo de Conexion

        #region MensajeContinuar 
        public static DialogResult MensajeProceso()
        {
            DialogResult rRespuesta = MensajeProceso(sMensajeDeProceso);
            return rRespuesta;
        }

        public static DialogResult MensajeProceso(string Mensaje)
        {
            DialogResult rRespuesta = General.msjConfirmar(Mensaje);
            return rRespuesta;
        }
        #endregion MensajeContinuar
    }
}
