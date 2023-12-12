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
using System.Text.RegularExpressions;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.SistemaOperativo;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft.Conexiones;
using DllFarmaciaSoft.GetInformacionManual; 
using DllFarmaciaSoft.Reporteador; 
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllFarmaciaSoft.SistemaOperativo;
using DllFarmaciaSoft.SesionDeUsuarios;

namespace DllFarmaciaSoft
{
    #region Enumeradores Publicos 
    /// <summary>
    /// Especifica el efecto de un movimiento de inventario sobre la existencia
    /// </summary>
    public enum AfectarInventario
    {
        Ninguno = 0, 
        /// <summary>
        /// Aplica la Cantidad especificada en el Movimiento de inventario, el tipo de movimiento 
        /// determina si es Positivo(E) ó Negativo(S) 
        /// </summary>
        Aplicar = 1,
        /// <summary>
        /// Desaplica la Cantidad especificada en el Movimiento de inventario, el tipo de movimiento 
        /// determina si es Positivo(E) ó Negativo(S) 
        /// </summary>
        Desaplicar = 2
    }

    /// <summary>
    /// Especifica el efecto de un movimiento de inventario sobre los Costos promedio de las existencias
    /// </summary>
    public enum AfectarCostoPromedio
    {
        /// <summary>
        /// No afecta el Costo Promedio 
        /// </summary>
        NoAfectar = 0,
        /// <summary>
        /// Al aplicar un Movimiento de inventario tambien afectara el Costo Promedio de las existencias
        /// tomando en cuanta el Valor anterior y el Nuevo Valor
        /// </summary>
        Afectar = 1 
    }

    /// <summary>
    /// Especifica el Tipo de Venta realizada
    /// </summary>
    public enum TipoDeVenta
    {
        Ninguna = 0,
        Publico = 1, 
        Credito = 2 
    }

    public enum TipoDevolucion
    {
        Ninguna = 0,
        Venta = 1,
        Compras = 2,
        EntradasDeConsignacion = 3,
        PedidosVenta = 4,
        PedidosConsignacion = 5,
        OrdenCompra = 6,
        Dev_Proveedor = 7,
        TransferenciaDeEntrada = 8,
        TransferenciaDeSalida = 9,
        VentasSocioComercial = 10
    }

    public enum TipoDePuntoDeVenta
    {
        Ninguno = 0, 
        Farmacia_Almacen = 1, 
        AlmacenJurisdiccional = 2 
    }

    public enum OrigenManejoLotes
    {
        Default = 0, 
        Ventas_Dispensacion = 1, 
        Ventas_PublicoGeneral = 2, 
        Compras = 3, 
        Transferencias = 4, 
        Inventarios = 5, 
        Transferencias_Estados = 6, 
        OrdenesDeCompra = 7, 
        EntradaPorConsignacion = 8, 
        PedidoDistribuidor = 9 
    } 

    public enum EncabezadosManejoLotes
    {
        Default = 0,
        EsDevolucionDeCompras = 1,
        EsDevolucionDeVentas = 2,
        EsTransferenciaDeEntrada = 3,  
        EsDevolucionDeOrdenesDeCompras = 4,
        EsDevolucionEntradaConsignacion = 5, 
        EsDevolucionPedidoDistribuidor = 6,
        EsDevolucion_Dev_A_Proveedor = 7
    }

    public enum TipoPedidosFarmacia
    {
        Ninguno = 0,
        Pedido_Automatico = 1,
        Pedido_Especial = 2,  
        Pedido_Automatico_Claves = 3,
        Pedido_Especial_Claves = 4
    }

    public enum TipoDePedido
    {
        Ninguno = 0,
        Productos = 1,
        Claves = 2
    } 

    /// <summary>
    /// Enumerador de módulos para SII Operativo
    /// </summary>
    public enum TipoModulo : int
    {
        Ninguno = 0, Farmacia = 1, Regional = 2, Almacen = 3, Auditor = 4, 
  
        SolicitudesDeAyuda = 5,
        InsumosOficina = 6, 
        AdministracionEquipos = 7,
        ServiciosInternos = 8,   
        LogisticaDistribucion = 9, 
        GastosOperativos = 10,  
        Facturacion = 11, 
   
        FarmaciaUnidosis = 21, AlmacenUnidosis = 22, 
        RFID = 23 
    }

    /// <summary>
    /// Enumerador de módulos para MEDIACCESS
    /// </summary>
    public enum TipoModulo_MA : int
    {
        Ninguno = 0, 
        Farmacia = 1, 
        Almacen = 2,
        Regional = 3
    }

    public enum TipoModuloCompras : int
    {
        Ninguno = 0, Central = 1, Regional = 2
    }

    public enum StatusDeRegistro
    {
        SinActualizar = 0, Actualizado = 1, MarcadoParaEnvio = 2, ActualizacionSuspendida = 3 
    }

    public enum TiposDeInventario
    {
        Todos = 0, Venta = 1, Consignacion = 2 
    }

    public enum TiposDeCargaMasiva
    {
        Ninguna = 0, InventarioInicial = 1, CompraDirecta = 2, EntradaDeConsignacion = 3 
    }

    public enum TiposDeSubFarmacia 
    {
        Todos = 0, Venta = 1, Consignacion = 2, ConsignacionEmulaVenta = 3 
    }

    public enum TiposDeUbicaciones
    {
        Todas = 100, Picking = 1, Almacenaje = 0 
    }

    public enum StatusRegistrosSanitarios : int
    {
        Ninguno = 0, Vigente = 1, Renovacion = 2, Prorroga = 3, Revocado = 4, Vencido = 5, Oficio = 6  
    }

    public enum Cols_Pago 
    { 
        Ninguna = 0, Codigo, Descripcion, Importe, PagoCon, Cambio, Referencia, PermiteDuplicidad 
    }

    ////public enum TipoDeFacturacion
    ////{
    ////    Ninguna = 0, Insumos = 1, Administracion = 2  
    ////}
    public enum FormatosImagen
    {
        Ninguno = 0, Jpeg = 1, Bmp = 2, Gif = 3, Png = 4
    }

    public enum TipoDeTransferencia
    {
        Ninguno = 0, 
        Almacen_Normal = 1, 
        Almacen_Controlados = 2, 
        Farmacia_Normal = 3, 
        Farmacia_Controlados = 4 
    }

    public enum TipoMonitorDeSurtido
    {
        Ninguno = 0,
        Surtimiento = 1, 
        Entrega_Validacion = 2, 
        Validacion = 3,
        Entrega_Documentacion = 4, 
        Documentacion = 5,
        Entrega_Embarques = 6, 
        Embarques = 7 
    }

    public enum TipoDePedidoElectronico
    {
        Ninguno = 0,
        Transferencias = 1,
        Transferencias_InterEstatales = 2,
        Ventas = 3, 
        SociosComerciales = 4 
    }


    public enum TiposDePedidosCEDIS
    {
        Ninguno = 0, 
        Venta = 1, 
        Transferencia = 2 
    }

    public enum Vales_TipoDeImpresion
    {
        Ninguno = 1,
        General = 1,
        Clave = 2,
        Clave_Pieza = 3 
    }

    public enum TipoReporteVenta
    {
        Ninguno = 0,
        Contado = 1,
        Credito = 2
    }

    public enum Puestos_CEDIS
    {
        Ninguno = 0,
        NoEspecificado = 0,
        Surtidor = 1,
        Chofer = 2,
        Montacarguista = 3,
        InventariosCiclicos = 4
    }
    #endregion Enumeradores Publicos

    public static class FormatoCampos
    {
        static basGenerales Fg = new basGenerales();

        public static string Formatear_QuitarAsterisco(string Cadena)
        {
            string sRegresa = "";

            sRegresa = Cadena.Replace("*", "");

            return sRegresa;
        }

        public static string Formatear_QuitarPipes(string Cadena)
        {
            string sRegresa = Cadena;

            if (Cadena.Contains("|"))
            {
                sRegresa = Cadena.Replace("|", "");
            }

            return sRegresa;
        }

        public static string Formatear_QuitarCaracteres(string Cadena)
        {
            string sRegresa = Cadena;

            string consignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
            string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";

            try
            {
                for (int i = 0; i <= consignos.Length - 1; i++)
                {
                    sRegresa = sRegresa.Replace(consignos[i], sinsignos[i]);
                }
            }
            catch
            {
                sRegresa = Cadena;
            }

            sRegresa = Formatear_QuitarPipes(sRegresa);

            return sRegresa;
        }

        public static string Formato_Digitos_Izquierda(string Valor, int Caracteres, string Caracter)
        {
            string sRegresa = "";

            sRegresa = Fg.PonFormato("", Caracter, Caracteres) + Valor;
            sRegresa = Fg.Right(sRegresa, Caracteres);

            return sRegresa;
        }

        public static string Formato_Digitos_Derecha(string Valor, int Caracteres, string Caracter)
        {
            string sRegresa = Valor;

            sRegresa = sRegresa + Fg.PonFormato("", Caracter, Caracteres);
            sRegresa = Fg.Left(sRegresa, Caracteres);

            return sRegresa;
        }

        public static string Formato_Caracter_Derecha(string Valor, int Caracteres, string Caracter)
        {
            string sRegresa = "";

            sRegresa = Formatear_QuitarCaracteres(Valor);

            ////if (Caracter == " " ) 
            ////{
            ////    Caracter = ""; 
            ////}

            sRegresa = sRegresa + Fg.PonFormato("", Caracter, Caracteres);
            sRegresa = Fg.Left(sRegresa, Caracteres);

            return sRegresa;
        }

        public static string Formato_Caracter_Izquierda(string Valor, int Caracteres, string Caracter)
        {
            string sRegresa = "";

            sRegresa = Formatear_QuitarCaracteres(Valor);

            sRegresa = Fg.PonFormato("", Caracter, Caracteres) + sRegresa;
            sRegresa = Fg.Right(sRegresa, Caracteres);

            return sRegresa;
        }

        public static string Formatear_Nombre(string Cadena)
        {
            string sRegresa = Cadena;

            string consignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
            string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";

            sRegresa = sRegresa.Replace(Fg.Comillas(), "");
            sRegresa = sRegresa.Replace("|", "");
            sRegresa = sRegresa.Replace("/", "");
            sRegresa = sRegresa.Replace("&", "");
            sRegresa = sRegresa.Replace("%", "");
            sRegresa = sRegresa.Replace("$", "");
            sRegresa = sRegresa.Replace("#", "");
            sRegresa = sRegresa.Replace("'", "");
            sRegresa = sRegresa.Replace("{", "");
            sRegresa = sRegresa.Replace("}", "");


            try
            {
                for (int i = 0; i <= consignos.Length - 1; i++)
                {
                    sRegresa = sRegresa.Replace(consignos[i], sinsignos[i]);
                }
            }
            catch
            {
                sRegresa = Cadena;
            }

            return sRegresa;
        }
    }

    public static class DtGeneral
    {
        #region Puerto Referencias Web 
        public static readonly string sPuertoWebDiseño = "31905";
        #endregion Puerto Referencias Web 

        #region Declaracion de Variables
        ////private static string sModulo = "DllFarmaciaSoft";
        ////private static string sVersion = "1.0.0.0";

        private static basGenerales Fg = new basGenerales();
        private static clsEdoIniManager myXmlEdoInformacion;

        private static clsCriptografo crypto = new clsCriptografo();

        private static string sArbolModulo = "";
        private static string sIdOficinaCentral = "OC";
        private static string sIdOficinaCentralNombre = "Oficina Central";
        private static string sIdFarmaciaCentral = "00SC";
        private static string sIdFarmaciaCentralNombre = "Central";

        private static string sIdEmpresa = "001";
        private static string sNombreEmpresa = "Intercontinental de Medicamentos";
        private static string sDireccionEmpresa = "";
        private static clsInformacionEmpresa datosEmpresa = new clsInformacionEmpresa();

        private static string sIdEstado = "";
        private static string sNombreEstado = "";
        private static string sClaveRenapo = "";
        private static string sIdFarmacia = "";
        private static string sNombreFarmacia = "";
        private static string sIdJurisdiccion = "";
        private static string sNombreJurisdiccion = "";
        private static bool bManejaVentaPublicoGral = false;
        private static bool bManejaControlados = false;
        private static bool bEsTipoAlmacen = false;
        private static bool bConfirmacionConHuellas = false;
        private static bool bEsEmpDeConsignacion = false;
        private static int iDiasARevisarpedidosCedis = 60;

        private static bool bOficinaCentral = false;
        private static bool bConfOficinaCentral = false;

        private static string sIdPersonalCEDIS_Conectado = "";
        private static string sIdPersonalCEDIS_Relacionado_Conectado = "";

        private static string sIdPersonalConectado = "";
        private static string sNombrePersonal = "";
        private static string sLoginUsuario = "";
        private static string sPassUser = "";
        private static bool bEsUsuarioTipoAdministrador = false;
        private static string sRutaReportes = "";
        // private static bool bSesionCorteCerrado = false;
        private static int iDiasAdicionalesCierreTickets = 0;

        private static DateTime dFechaMenorSistema = new DateTime();
        private static string[] sListaArboles = { "CFGC", "CFGN", "CFGS", "CIM4", "ISNO" };

        private static clsDatosApp dpDatosApp = new clsDatosApp("DllFarmaciaSoft", Application.ProductVersion);
        public static string RutaLogo = Application.StartupPath + @"\SII_LOGO.jpg";
        // private static clsInfoUsuario infUsuario = new clsInfoUsuario();  

        public static readonly string CfgIniReplicacionSQL = "Replicacion SQL";
        public static readonly string CfgIniOficinaCentral = "OficinaCentral";
        public static readonly string CfgIniPuntoDeVentaAlmacen = "AlmacenPtoVta";
        public static readonly string CfgIniPuntoDeVenta = "FarmaciaPtoVta";
        public static readonly string CfgIniComprasCentral = "Compras";
        public static readonly string CfgIniComprasRegional = "ComprasRegional";
        public static readonly string CfgIniComprasInformacion = "Compras-Informacion";


        public static readonly string CfgIniPuntoDeVentaAlmacen_MA = "MA Almacen";
        public static readonly string CfgIniPuntoDeVenta_MA = "MA Farmacia";


        // 2K120504.1154 Jesús Díaz 
        public static readonly string CfgIni_Administrativo = "SII-Administrativo";

        //2k121217.1016 Fernando Aragón
        public static readonly string CfgIniChecadorPersonal = "Checador";

        public static readonly string CfgIniValidarHuellas = "ValidacionDeHuellas";
        public static readonly string CfgIniAutorizacionConHuellas = "AutorizacionConHuellas";

        private static string sUrl_Servidor_Regional = "";
        public static bool bUrlServidorCentralRegional = false;

        private static string sUrl_Servidor_Central = "";
        public static bool bUrlServidorCentral = false;

        private static bool bEsServidorDeRedLocal_Validado = false;
        private static bool bEsServidorDeRedLocal = false;

        private static string sEsEquipoDeDesarrollo = "";
        private static string sUsuarioConectando = "";
        private static int iIntentosPassword = 0;
        private static bool bEsEquipoDeDesarrollo = File.Exists(General.UnidadSO + @":\\Dev.xml");
        private static TipoModulo tpModuloActivo = TipoModulo.Ninguno;
        private static TipoModulo_MA tpModuloActivo_MA = TipoModulo_MA.Ninguno;

        private static TipoModuloCompras tpModuloActivoCompras = TipoModuloCompras.Ninguno;
        private static bool bCargarSoloUnidadesConfiguradas = false;

        private static bool bCargarSoloUnidadesConfiguradasRegionales = false;
        private static bool bUnidadConServidorDedicado = false;

        private static int iLargoCodigoEAN = 15;

        //// Manejo de Inventarios 
        private static string sIdTipoMovtoEntradaPorConsignacion = "EPC";

        //// Para Auditoria 
        private static string sIdSesion = "";

        private static clsIniImpresoras listaImpresoras = new clsIniImpresoras();
        private static clsIniCamaras listaCamaras = new clsIniCamaras();

        // Operaciones Supervizadas 
        private static clsOperacionesSupervizadas opPermisosEspeciales;
        private static clsOperacionesSupervizadasHuellas opPermisosEspeciales_Huellas;


        private static clsDatosWebService datosDeWebService = new clsDatosWebService();
        private static clsDatosWebService datosDeWebServiceUpdater = new clsDatosWebService();
        private static clsDatosWebService datosDeWebServiceCEDIS = new clsDatosWebService();

        private static bool bUbicacionesEstandar = false;
        private static DataSet dtsUBI_Estandar;

        private static clsSeguridad seguridad;
        private static string sIP = "";
        private static string sMacAddress = "";
        private static List<string> sListaMacAddress = new List<string>();
        #endregion Declaracion de Variables

        #region Constructores 
        static DtGeneral()
        {
            LoadAssemblies_Neodynamic();


            dFechaMenorSistema = Convert.ToDateTime("2009-05-01");
            //// SC_SolutionsSystem.Usuarios_y_Permisos.clsAbrirForma.AssemblyActual("DllFarmaciaSoft"); 
            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());
            //dpDatosApp = new clsDatosApp("DllFarmaciaSoft", "1.0.5.1");

            ConexionesRed();

            //sImpresoraPredeterminada_SO = General.Impresoras.GetImpresoraDefault();

            //MenuEnFormaDeArbol = false;
        }

        public static void DoEvents()
        {
            DoEvents(null);
        }

        public static void DoEvents( Form Pantalla )
        {
            Application.DoEvents();

            if(Pantalla != null)
            {
                Pantalla.Refresh();
            }
        }
        #endregion Constructores 

        #region Interface 
        private static bool bEsMenu_Form = true; 

        public static bool MenuEnFormaDeArbol
        { 
            get { return bEsMenu_Form; }
            set 
            { 
                bEsMenu_Form = value;

                General.FormaBackColor = Color.WhiteSmoke; 
                if(!value)
                {
                    General.FormaBackColor = Color.LightBlue;
                }
                

                General.BackColorBarraMenu = General.FormaBackColor;

            }
        }
        #endregion Interface 

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

        public static clsDatosWebService DatosDeServicioWebCEDIS
        {
            get { return datosDeWebServiceCEDIS; }
            set { datosDeWebServiceCEDIS = value; }
        }
        #endregion Servicios Web

        #region Interface IMach4 
        #region Declaracion de Variables 
        private static bool bEsClienteDeInterface = false;
        private static bool bEsClienteDeInterface_Validado = false;
        #endregion Declaracion de Variables

        #region Propiedades 
        private static bool EsClienteIMach4
        {
            get { return bEsClienteDeInterface; }
            set
            {
                if(!bEsClienteDeInterface_Validado)
                {
                    bEsClienteDeInterface_Validado = true;
                    bEsClienteDeInterface = value;
                }
            }
        }
        #endregion Propiedades 
        #endregion Interface  IMach4

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
                if(!bUrlServidorCentralRegional)
                {
                    Obtener_Url_ServidorCentralRegional();
                    // bUrlServidorCentralRegional = false;
                }
                return sUrl_Servidor_Regional;
            }
            set { sUrl_Servidor_Regional = value; }
        }

        private static void Obtener_Url_ServidorCentralRegional()
        {
            clsConsultas Consultas;
            Consultas = new clsConsultas(General.DatosConexion, General.Modulo, "DtGeneral", General.Version, false);
            sUrl_Servidor_Regional = Consultas.Obtener_Url_ServidorCentralRegional("Obtener_Url_ServidorCentralRegional");

            bUrlServidorCentralRegional = Consultas.ExistenDatos;
        }

        public static string UrlServidorCentral
        {
            get
            {
                if(!bUrlServidorCentral)
                {
                    Obtener_Url_ServidorCentral();
                    //bUrlServidorCentral = false;
                }
                return sUrl_Servidor_Central;
            }
            set { sUrl_Servidor_Central = value; }
        }

        public static string UrlServidorCentral_Regional
        {
            get
            {
                if(!bUrlServidorCentral)
                {
                    Obtener_Url_ServidorCentral_Regional();
                    //bUrlServidorCentral = false;
                }
                return sUrl_Servidor_Central;
            }
            set { sUrl_Servidor_Central = value; }
        }

        private static void Obtener_Url_ServidorCentral_Regional()
        {
            bUrlServidorCentral = false;

            clsConsultas Consultas;
            Consultas = new clsConsultas(General.DatosConexion, General.Modulo, "DtGeneral", General.Version, false);
            sUrl_Servidor_Central = Consultas.Obtener_Url_ServidorCentralRegional("Obtener_Url_ServidorCentral_Regional");

            bUrlServidorCentral = Consultas.ExistenDatos;
        }

        private static void Obtener_Url_ServidorCentral()
        {
            bUrlServidorCentral = false;
            clsDatosCliente DatosCliente;
            DatosCliente = new clsDatosCliente(dpDatosApp, "DtGeneral", "");
            string sQuery = "", sInicio = "Set DateFormat YMD ";

            SC_SolutionsSystem.Errores.clsGrabarError Error;
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, "DtGeneral");

            Obtener_Url_ServidorCentralRegional();
            clsLeerWebExt myLeerWeb = new clsLeerWebExt(sUrl_Servidor_Regional, DtGeneral.CfgIniOficinaCentral, DatosCliente);

            sQuery = sInicio + string.Format(" Select * From vw_OficinaCentral_Url (NoLock) ");
            if(!myLeerWeb.Exec(sQuery))
            {
                Error.GrabarError(myLeerWeb, "Obtener_Url_ServidorCentral");
                //General.msjUser("Ocurrió un error al obtener la Url del Servidor Central");
            }
            else
            {
                if(myLeerWeb.Leer())
                {
                    sUrl_Servidor_Central = myLeerWeb.Campo("Url");
                    bUrlServidorCentral = true;
                }
                else
                {
                    General.msjUser("Url de Servidor Central no encontrada, verifique.");
                }
            }
        }

        public static bool EsServidorDeRedLocal
        {
            get
            {
                if(!bEsServidorDeRedLocal_Validado)
                {
                    bEsServidorDeRedLocal_Validado = true;
                    ObtenerServidorLocal();
                }
                return bEsServidorDeRedLocal;
            }
        }
        #endregion Urls  

        //public static clsInfoUsuario InfoUsuario
        //{
        //    get { return infUsuario; }
        //    set { infUsuario = value; }
        //}

        public static string ArbolModulo
        {
            get { return sArbolModulo; }
            set
            {
                if(sArbolModulo == "")
                {
                    sArbolModulo = value;
                }
            }
        }

        public static TipoModulo ModuloEnEjecucion
        {
            get { return tpModuloActivo; }
            set { tpModuloActivo = value; }
        }

        public static TipoModulo_MA ModuloMA_EnEjecucion
        {
            get { return tpModuloActivo_MA; }
            set { tpModuloActivo_MA = value; }
        }

        public static TipoModuloCompras Modulo_Compras_EnEjecucion
        {
            get { return tpModuloActivoCompras; }
            set { tpModuloActivoCompras = value; }
        }

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

        public static clsInformacionEmpresa EmpresaDatos
        {
            get { return datosEmpresa; }
            set { datosEmpresa = value; }
        }

        public static string EmpresaConectada
        {
            get { return sIdEmpresa; }
            set { sIdEmpresa = Fg.PonCeros(value, 3); }
        }

        public static string EmpresaConectadaNombre
        {
            get { return sNombreEmpresa; }
            set { sNombreEmpresa = value; }
        }

        public static string EmpresaDomicilio
        {
            get { return sDireccionEmpresa; }
            set { sDireccionEmpresa = value; }
        }

        public static bool EsEmpresaDeConsignacion
        {
            get { return bEsEmpDeConsignacion; }
            set { bEsEmpDeConsignacion = value; }
        }

        public static string ClaveMovtoEntradaPorConsignacion
        {
            get { return sIdTipoMovtoEntradaPorConsignacion; }
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
                if(myXmlEdoInformacion == null)
                {
                    myXmlEdoInformacion = new clsEdoIniManager();
                }

                return myXmlEdoInformacion;
            }
            set { myXmlEdoInformacion = value; }
        }

        public static bool ConexionOficinaCentral
        {
            //Para diferenciar si esta conectada Oficina Central ó Punto de Venta.
            get { return bOficinaCentral; }
            set { bOficinaCentral = value; }
        }

        public static bool ConfiguracionOficinaCentral
        {
            //Para diferenciar si esta conectada Oficina Central ó Clientes.
            get { return bConfOficinaCentral; }
            set { bConfOficinaCentral = value; }
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

        public static string IdPersonalCEDIS
        {
            get { return sIdPersonalCEDIS_Conectado; }
            set { sIdPersonalCEDIS_Conectado = value; }
        }


        public static string IdPersonalCEDIS_Relacionado
        {
            get { return sIdPersonalCEDIS_Relacionado_Conectado; }
            set { sIdPersonalCEDIS_Relacionado_Conectado = value; }
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
                //if (sPassUser == "")
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

        public static string IdOficinaCentral
        {
            get { return sIdOficinaCentral; }
        }

        public static string IdOficinaCentralNombre
        {
            get { return sIdOficinaCentralNombre; }
        }

        public static string IdFarmaciaCentral
        {
            get { return sIdFarmaciaCentral; }
        }

        public static string IdFarmaciaCentralNombre
        {
            get { return sIdFarmaciaCentralNombre; }
        }

        public static string[] ArbolesConfiguracion
        {
            get { return sListaArboles; }
            set { sListaArboles = value; }
        }

        public static bool ManejaVentaPublico
        {
            get { return bManejaVentaPublicoGral; }
            set { bManejaVentaPublicoGral = value; }
        }

        public static bool ManejaControlados
        {
            get { return bManejaControlados; }
            set { bManejaControlados = value; }
        }

        public static bool EsAlmacen
        {
            get { return bEsTipoAlmacen; }
            set { bEsTipoAlmacen = value; }
        }

        public static string RutaReportes
        {
            get { return sRutaReportes; }
            set { sRutaReportes = value; }
        }

        public static clsSeguridad ItemSeguridad 
        {
            get 
            {
                if(DtGeneral.EsEquipoDeDesarrollo)
                {
                    ;
                }
                return seguridad; 
            }
        }

        public static string UsuarioConectando
        {
            get { return sUsuarioConectando; }
            set { sUsuarioConectando = value;  }
        }

        public static int IntentosPassword
        {
            get { return iIntentosPassword; }
            set { iIntentosPassword = value; }
        }

        public static bool EsEquipoDeDesarrollo
        {
            get
            {
                if(sEsEquipoDeDesarrollo == "")
                {
                    seguridad = new clsSeguridad(); 
                    bEsEquipoDeDesarrollo = seguridad.Validar("SIIxIIS");

                    sEsEquipoDeDesarrollo = bEsEquipoDeDesarrollo.ToString();
                }
                return bEsEquipoDeDesarrollo;
            }
        }

        public static bool ConfirmacionConHuellas
        {
            get { return bConfirmacionConHuellas; }
            set { bConfirmacionConHuellas = value; }
        }

        /// <summary>
        /// Determina si la Pantalla de Login muestra solamente las Unidades Configuradas para el Servidor Especificado 
        /// </summary>
        public static bool SoloMostrarUnidadesConfiguradas
        {
            get { return bCargarSoloUnidadesConfiguradas; }
            set { bCargarSoloUnidadesConfiguradas = value; }
        }

        /// <summary>
        /// Determina si la Pantalla de Login muestra solamente las Unidades Configuradas para el Servidor Especificado 
        /// </summary>
        public static bool SoloMostrarUnidadesConfiguradasRegionales
        {
            get { return bCargarSoloUnidadesConfiguradasRegionales; }
            set { bCargarSoloUnidadesConfiguradasRegionales = value; }
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

        public static int DiasAdicionalesCierreTickets
        {
            get { return iDiasAdicionalesCierreTickets; }
            set
            {
                iDiasAdicionalesCierreTickets = value;
                if(iDiasAdicionalesCierreTickets <= 0)
                {
                    iDiasAdicionalesCierreTickets = 30;
                }
            }
        }

        public static bool UnidadConServidorDedicado
        {
            get { return bUnidadConServidorDedicado; }
            set { bUnidadConServidorDedicado = value; }
        }

        public static clsOperacionesSupervizadas PermisosEspeciales
        {
            get
            {
                if(opPermisosEspeciales == null)
                {
                    opPermisosEspeciales = new clsOperacionesSupervizadas(General.DatosConexion, sIdEstado, sIdFarmacia, sIdPersonalConectado);
                    opPermisosEspeciales.CargarPermisos();
                }
                return opPermisosEspeciales;
            }
            set { opPermisosEspeciales = value; }
        }

        public static clsOperacionesSupervizadasHuellas PermisosEspeciales_Biometricos
        {
            get
            {
                if(opPermisosEspeciales_Huellas == null)
                {
                    ////opPermisosEspeciales_Huellas = new clsOperacionesSupervizadasHuellas(General.DatosConexion, sIdEstado, sIdFarmacia, sIdPersonalConectado);
                    opPermisosEspeciales_Huellas = new clsOperacionesSupervizadasHuellas();
                    opPermisosEspeciales_Huellas.CargarPermisos();
                }
                return opPermisosEspeciales_Huellas;
            }
            set { opPermisosEspeciales_Huellas = value; }
        }

        public static bool CFG_UbicacionesEstandar
        {
            //Para obtener si el almacen que maneja ubicaciones tiene configuradas las Posiciones Estandar.
            get { return bUbicacionesEstandar; }
            set { bUbicacionesEstandar = value; }
        }

        public static DataSet CFG_UBI_Estandar
        {
            //Para obtener si el almacen que maneja ubicaciones tiene configuradas las Posiciones Estandar.
            get { return dtsUBI_Estandar; }
            set { dtsUBI_Estandar = value; }
        }

        public static int DiasARevisarpedidosCedis
        {
            get { return iDiasARevisarpedidosCedis; }
            set { iDiasARevisarpedidosCedis = value; }
        }

        #endregion Propiedades 

        #region Codigos EAN 
        /// <summary>
        /// Determina la longitud a la cual se formatearan los Códigos EAN 
        /// </summary>
        public static int LargoCodigoEAN
        {
            get { return iLargoCodigoEAN; }
            set { iLargoCodigoEAN = value; }
        }
        #endregion Codigos EAN 

        #region Redondeo 
        private static int iDecimales = 2;
        public static double Redondear( double Valor )
        {
            return Redondear(Valor, iDecimales);
        }

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
        #endregion Redondeo

        #region Control de Salida del Sistema
        static bool bValidarModulosAbiertosAlSalirDeSistema = true;

        public static bool ValidarSalirDeSistema
        {
            get { return bValidarModulosAbiertosAlSalirDeSistema; }
            set { bValidarModulosAbiertosAlSalirDeSistema = value; }
        }

        public static bool ValidarModulosAbiertos()
        {
            bool bRegresa = true;
            //////string sMain = "FrmMain".ToUpper();
            //////string sNav = "FrmNavegador".ToUpper();
            //////bool bMain = false;
            //////bool bNav = false; 

            //////int iFormsValidos = 0;
            //////int iForms = 0; 

            //////if (bValidarModulosAbiertosAlSalirDeSistema)
            //////{
            //////    FormCollection fs = Application.OpenForms;
            //////    iForms = fs.Count;

            //////    foreach (Form f in fs)
            //////    {
            //////        if (!bMain)
            //////        {
            //////            if (f.Name.ToUpper() != sMain)
            //////            {
            //////                iFormsValidos++;
            //////                bMain = true; 
            //////            }
            //////        }

            //////        if (!bNav)
            //////        {
            //////            if (f.Name.ToUpper() != sNav)
            //////            {
            //////                iFormsValidos++;
            //////                bNav = true; 
            //////            }
            //////        }
            //////    } 
            //////}

            //////if ((iForms - iFormsValidos) > 0)
            //////{
            //////    bRegresa = false; 
            //////}

            return bRegresa;
        }
        #endregion Control de Salida del Sistema

        #region PADRON DE AFILIADOS
        private static bool bPadronLocal = false;
        private static bool bBusquedaPadron = false;

        public static void BuscarConfiguracionPadron()
        {
            // 2K110713.2221 Jesus Diaz 
            string sSql = string.Format(" Select * From CFGS_PADRON_ESTADOS (NoLock) Where EsLocal = 1 ");
            clsConexionSQL cnn;
            clsLeer leer;

            if(!bBusquedaPadron)
            {
                bBusquedaPadron = true;
                cnn = new clsConexionSQL(General.DatosConexion);
                leer = new clsLeer(ref cnn);

                if(leer.Exec(sSql))
                {
                    bPadronLocal = leer.Leer();
                }
            }
        }

        public static bool PadronLocal
        {
            get { return bPadronLocal; }
        }
        #endregion PADRON DE AFILIADOS

        #region Reporteador 
        public static clsIniImpresoras Impresoras
        {
            get { return listaImpresoras; }
            set { listaImpresoras = value; }
        }

        public static clsIniCamaras Camaras
        {
            get { return listaCamaras; }
            set { listaCamaras = value; }
        }

        #region Generar Reporte 
        static bool bCanceladoPorUsuario = false;
        private static string sImpresoraPredeterminada_SO = "";
        public static bool CanceladoPorUsuario
        {
            get { return bCanceladoPorUsuario; }
        }
        public static bool GenerarReporteExcel(string Url, bool ImpresionWeb, string Reporte, clsDatosCliente DatosTerminal)
        {
            bool bRegresa = false;
            string sRutaRtpExcel = "";
            string sReporte = Reporte.ToUpper();

            if (!sReporte.Contains(".XLS"))
            {
                Reporte = Reporte + ".xls"; 
            }

            sRutaRtpExcel = Path.Combine(Application.StartupPath + @"\\Plantillas\", Reporte);  

            // if ( !File.Exists(sRutaRtpExcel))  
            {
                clsReporteadorExcel Reporteador = new clsReporteadorExcel(Reporte, sRutaReportes, DatosTerminal);
                Reporteador.ImpresionViaWeb = ImpresionWeb;
                Reporteador.Url = Url;
                bRegresa = Reporteador.GenerarReporte();
            }

            return bRegresa;
        }


        public static bool GenerarReporte(clsImprimir Reporte, clsDatosCliente DatosTerminal)
        {
            return GenerarReporte(General.Url, General.ImpresionViaWeb, Reporte, DatosTerminal); 
        }

        public static bool GenerarReporte(string Url, bool ImpresionWeb, clsImprimir Reporte, clsDatosCliente DatosTerminal)
        {
            bool bRegresa = false;
            bool bCambiarImpresora = sImpresoraPredeterminada_SO != Reporte.Impresora;

            ////// Establecer la impresora para el diseño del reporte 
            //if(bCambiarImpresora)
            //{
            //    General.ImpresorasManager.SetDefault_Printer(Reporte.Impresora);
            //}

            clsReporteador Reporteador = new clsReporteador(Reporte, DatosTerminal);
            Reporteador.ImpresionViaWeb = ImpresionWeb;
            Reporteador.Url = Url;
            bRegresa = Reporteador.GenerarReporte();
            bCanceladoPorUsuario = Reporteador.CanceladoPorUsuario;

            ////// Establecer la impresora para el diseño del reporte 
            //if(bCambiarImpresora)
            //{
            //    General.ImpresorasManager.SetDefault_Printer(sImpresoraPredeterminada_SO);
            //}

            return bRegresa; 
        }

        public static bool GenerarReporteRemoto(clsConexionClienteUnidad ConexionUnidad, clsImprimir Reporte, clsDatosCliente DatosTerminal)
        {
            return GenerarReporteRemoto(General.Url, General.ImpresionViaWeb, ConexionUnidad, Reporte, DatosTerminal);
        }

        public static bool GenerarReporteRemoto(string Url, bool ImpresionWeb, 
            clsConexionClienteUnidad ConexionUnidad, clsImprimir Reporte, clsDatosCliente DatosTerminal)
        {
            bool bRegresa = false;

            clsReporteador Reporteador = new clsReporteador(Reporte, DatosTerminal, ConexionUnidad);
            Reporteador.ImpresionViaWeb = ImpresionWeb;
            Reporteador.Url = Url;
            bRegresa = Reporteador.GenerarReporteRemoto();
            bCanceladoPorUsuario = Reporteador.CanceladoPorUsuario;

            return bRegresa;
        }
        #endregion Generar Reporte

        #region Exportar Reporte 
        public static bool ExportarReporte(clsImprimir Reporte, clsDatosCliente DatosTerminal, string Nombre, FormatosExportacion Formato)
        {
            return ExportarReporte(General.Url, General.ImpresionViaWeb, Reporte, DatosTerminal, Nombre, Formato);
        }

        public static bool ExportarReporte(string Url, bool ImpresionWeb, clsImprimir Reporte, clsDatosCliente DatosTerminal, string Nombre, FormatosExportacion Formato)
        {
            bool bRegresa = false;

            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "Seleccione la ruta donde se guardara el documento";

            if (folder.ShowDialog() == DialogResult.OK)
            {
                clsReporteador Reporteador = new clsReporteador(Reporte, DatosTerminal);
                Reporteador.ImpresionViaWeb = ImpresionWeb;
                Reporteador.Url = Url;
                bRegresa = Reporteador.ExportarReporte(Path.Combine(folder.SelectedPath, Nombre), Formato);
            }

            return bRegresa;
        }
        #endregion Exportar Reporte          
        #endregion Reporteador

        #region Base de Datos
        public static void ExportarInformacionBD()
        {
            ////FrmExportarBD f = new FrmExportarBD(); 
            ////f.ShowDialog(); 
        }
        #endregion Base de Datos

        #region UNIDOSIS 
        public static bool EsModuloUnidosisEnEjecucion
        {
            get
            {
                bool bEsModuloUnidosis = DtGeneral.ModuloEnEjecucion == TipoModulo.AlmacenUnidosis | DtGeneral.ModuloEnEjecucion == TipoModulo.FarmaciaUnidosis;
                return bEsModuloUnidosis;
            }
        }

        public static bool EsModuloUnidosis()
        {
            bool bEsModuloValido = DtGeneral.ModuloEnEjecucion == TipoModulo.AlmacenUnidosis | DtGeneral.ModuloEnEjecucion == TipoModulo.FarmaciaUnidosis;

            if (!bEsModuloValido)
            {
                General.msjAviso("El opción solicitada no esta habilitada para este módulo.");
            }

            return bEsModuloValido; 
        }
        #endregion UNIDOSIS

        #region Actualizaciones Automaticas
        private static bool bEquipoDesarrollo_SolicitarConfirmacion = true;

        public static bool ConfirmarBusquedaDeActualizaciones()
        {
            bool bConfirmarDescarga = true;

            if (DtGeneral.EsEquipoDeDesarrollo)
            {
                bConfirmarDescarga = false; 
                ////if (bEquipoDesarrollo_SolicitarConfirmacion)
                ////{
                ////    bConfirmarDescarga = General.msjConfirmar("El equipo actual pertenece al STAFF, ¿ Desea buscar actualizaciones recientes ?") == System.Windows.Forms.DialogResult.Yes;
                ////    bEquipoDesarrollo_SolicitarConfirmacion = bConfirmarDescarga;
                ////}
            }

            return bConfirmarDescarga; 
        }
        #endregion Actualizaciones Automaticas

        #region Validar Surtimiento De Pedidos 
        public static bool TieneSurtimientosActivos()
        {
            bool bRegresa = true;

            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            clsGrabarError Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, "DtGeneral()"); 


            string sSql = string.Format("Exec spp_ALM_ValidarGeneracionExistenciaDistribucion  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "validarSurtimientos()");
            }
            else
            {
                bRegresa = leer.Leer();
            }

            return bRegresa;
        }

        #endregion Validar Surtimiento De Pedidos

        #region Validar Permiso Especial Almacen

        public static bool Almacen_PermisoEspecial()
        {
            bool bRegresa = false;


            string sMD5 = "", sDesencripcion = "";
            string[] sArreglo;

            string sFechaVigencia = "";

            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            clsGrabarError Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, "DtGeneral()");


            string sSql = string.Format(" Select C.* " +
                " From CFG_Almacenes_PermisosEspeciales  C (NoLock) " +
                " Where C.IdEstado = '{0}' and C.IdFarmacia = '{1}'",
                 DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);


            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "Almacen_PermisosEspecial()");
            }
            else
            {
                if (leer.Leer())
                {
                    sMD5 = leer.Campo("MD5");
                    sDesencripcion = crypto.Desencriptar(sMD5, 17);

                    sArreglo = sDesencripcion.Split('|');

                    sFechaVigencia = sArreglo[3];

                    try
                    {

                        if (sArreglo[0] == DtGeneral.EstadoConectado &&
                            sArreglo[1] == DtGeneral.FarmaciaConectada &&
                            sArreglo[2].ToUpper() == "A"&&
                            Convert.ToDateTime(sFechaVigencia) >= General.FechaSistemaObtener())
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

        #region Validar Transferencias en Transito
        private static bool bExistenTransferencias_EnTransito__AplicacionExpirada = false;

        public static bool ExistenTransferencias_EnTransito__AplicacionExpirada
        { 
            get { return bExistenTransferencias_EnTransito__AplicacionExpirada; }
        }

        public static bool ValidaTransferenciasTransito_DiasConfirmacion()
        {
            return ValidaTransferenciasTransito_DiasConfirmacion("Se encontraron Transferencias en Tránsito por aplicar que excedieron el periodo de confirmación,se deshabilitara la Dispensación y Transferencias de Salida hasta que sean aplicadas dichas transferencias.");
        }

        public static bool ValidaTransferenciasTransito_DiasConfirmacion( string Mensaje )
        {
            bool bRegresa = true;
            string sSql = "";
            int iTransferencias_Dias_ConfirmacionTransitos = GnFarmacia.Transferencias_Dias_ConfirmacionTransitos;

            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            clsGrabarError Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, "DtGeneral()");

            sSql = string.Format(
                "Select IdEstado, IdFarmacia, FolioTransferencia \n" +
                "From TransferenciasEnc (Nolock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and TipoTransferencia = 'TS' \n" +
                "   and TransferenciaAplicada = 0 and Status = 'A' and datediff(dd, FechaTransferencia, getdate()) > {3} \n\n " +
                "Union All \n" +
                "Select IdEstado, IdFarmacia, FolioDevolucion \n" +
                "From DevolucionTransferenciasEnc(Nolock) \n" +
                 "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and TipoTransferencia = 'EDT' \n" +
                "   and TransferenciaAplicada = 0 and Status = 'A' and datediff(dd, FechaTransferencia, getdate()) > {3} \n\n ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iTransferencias_Dias_ConfirmacionTransitos);

            if(!DtGeneral.EsEquipoDeDesarrollo)
            {
                if(!leer.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leer, "ValidaTransferenciasTransito_DiasConfirmacion()");
                    General.msjError("Ocurrió un error al validar transferencias en tránsito.");
                }
                else
                {
                    if(leer.Leer())
                    {
                        bRegresa = false;
                        General.msjAviso(Mensaje);
                    }
                }
            }

            bExistenTransferencias_EnTransito__AplicacionExpirada = !bRegresa; 

            return bRegresa;
        }

        public static bool ValidaTransferenciasTransito()
        {
            return ValidaTransferenciasTransito("No es posible realizar ajustes de inventario, se encontraron productos en Tránsito."); 
        }

        public static bool ValidaTransferenciasTransito(string Mensaje)
        {
            bool bRegresa = true;
            string sSql = "";
            bool bPermitirAjustesInventario_Con_ExistenciaEnTransito = GnFarmacia.PermitirAjustesInventario_Con_ExistenciaEnTransito;

            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            clsGrabarError Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, "DtGeneral()");  

            sSql = string.Format(
                "Select \n" + 
                " IsNull(\n" + 
                "       (\n" +
                "           Select cast(count(*) as bit) as Transferencias \n" +
                "           From TransferenciasEnc (Nolock) \n" +
                "           Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and TipoTransferencia = 'TS' \n" +
                "           and TransferenciaAplicada = 0 and Status = 'A' \n" + 
                "   ), 0) as Transferencias, \n\n " + 
                " " +
                "   IsNull(\n" + 
                "         (\n" + 
                "           Select cast((case when sum(ExistenciaEnTransito) > 0 then 1 else 0 end) as bit) as ExistenciaEnTransito \n" +
                "           From FarmaciaProductos_CodigoEAN_Lotes (NoLock) \n" +
                "           Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' \n" +
                "   ), 0) as ExistenciaEnTransito \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "ValidaTransferenciasTransito()"); 
                General.msjError("Ocurrió un error al validar transferencias en tránsito.");
            }
            else
            {
                if (leer.Leer())
                {
                    if (leer.CampoBool("Transferencias") || leer.CampoBool("ExistenciaEnTransito"))
                    {
                        //////if (bPermitirAjustesInventario_Con_ExistenciaEnTransito)
                        //////{
                        //////    General.msjAviso("Se encontraron productos en Tránsito, no es posible generar ajustes de los productos en Tránsito.");
                        //////}
                        //////else
                        {
                            bRegresa = false;
                            //General.msjAviso("No es posible realizar ajustes de inventario, se encontraron productos en Tránsito.");
                            General.msjAviso(Mensaje);                            
                        }
                    }
                }
            }

            return bRegresa;
        }
        #endregion Validar Transferencias en Transito

        #region Validar Versión de BD vs Versión de Modulos 
        public static bool ValidarVersion_Modulo_vs_BaseDeDatos(clsDatosApp InformacionModulo)
        {
            bool bRegresa = false;
            string sSql = ""; 
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            clsGrabarError Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, "DtGeneral()");

            sSql = string.Format(
                "Select (case when dbo.fg_FormatoVersion('{0}') >= Version then 1 else 0 end) as VersionValida \n" + 
                "From \n" + 
                "(\n " +
                "   Select max(dbo.fg_FormatoVersion(Version)) as Version \n " + 
                "   From Net_Versiones (NoLock) Where Tipo = 1 \n " + 
                ") V \n", InformacionModulo.Version);

            if (DtGeneral.EsEquipoDeDesarrollo) 
            {
                bRegresa = true; 
            }
            else
            {
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leer, "ValidarVersion_Modulo_vs_BaseDeDatos()");
                    General.msjError("Ocurrió un error al validar la versión Base de datos vs Módulos.");
                }
                else
                {
                    leer.Leer();
                    bRegresa = leer.CampoBool("VersionValida");
                    if (!bRegresa)
                    {
                        General.msjAviso(string.Format("El módulo {0} no es válido para su ejecución.", InformacionModulo.Modulo.ToUpper()), "Versión incorrecta");
                    }
                }
            }

            return bRegresa; 
        }
        #endregion Validar Versión de BD vs Versión de Modulos

        #region Funciones y Procedimientos Publicos
        private static string ListaDeMacAddress( string Sepador )
        {
            string sRegresa = "";

            foreach(string sMac in sListaMacAddress)
            {
                sRegresa += string.Format("{0}{1}{0}, ", Sepador, sMac);
            }

            if(sRegresa != "")
            {
                sRegresa = sRegresa.Trim();
                sRegresa = Fg.Left(sRegresa, sRegresa.Length - 1);
            }

            return sRegresa;
        }

        private static void ConexionesRed()
        {
            // Ip del host
            // IPAddress[] myIp = Dns.GetHostAddresses(sNombreHost);
            // sIp = myIp[0].ToString();

            try
            {
                sMacAddress = ""; 

                //// Ip del host
                foreach (IPAddress myIp in Dns.GetHostAddresses(General.NombreEquipo))
                {
                    if (!myIp.IsIPv6LinkLocal)
                    {
                        sIP = myIp.ToString();
                        break;
                    }
                }

                sListaMacAddress = new List<string>();
                //// Mac del host
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

                foreach (NetworkInterface adaptador in nics)
                {
                    PhysicalAddress physical = adaptador.GetPhysicalAddress(); 
                    sMacAddress = physical.ToString();

                    sListaMacAddress.Add(sMacAddress); 
                }

            }
            catch
            {
                sMacAddress = "";
            }
        }

        public static List<string> ListaMacAddress
        {
            get { return sListaMacAddress; }
        }

        public static void RevisarVersion()
        {
            string []s = {""}; 
            RevisarVersion(s); 
        }

        public static void RevisarVersion(string[] Modulos)
        {
            RevisarVersion(Modulos, false); 
        }

        public static void RevisarVersion(string []Modulos, bool MostrarInterface) 
        {
            // SC_SolutionsSystem.SistemaOperativo.FrmInformacion x = new SC_SolutionsSystem.SistemaOperativo.FrmInformacion(true);
            DllFarmaciaSoft.SistemaOperativo.FrmInformacion FrmVersion = new DllFarmaciaSoft.SistemaOperativo.FrmInformacion(true);

            ////FrmVersion.Add("SC_CompressLib");
            ////FrmVersion.Add("SC_ControlsCS");
            ////FrmVersion.Add("SC_SolutionsSystem");
            ////FrmVersion.Add("SC_SolutionsSystem.FP");
            ////FrmVersion.Add("SC_SolutionsSystem.QRCode"); 
            ////FrmVersion.Add("DllFarmaciaSoft");
            ////FrmVersion.Add("DllFarmaciaSoft.QRCode");
            ////FrmVersion.Add("DllFarmaciaAuditor"); 
            ////FrmVersion.Add("DllTransferenciaSoft");

            ////FrmVersion.Add("DllCompras"); 
            ////FrmVersion.Add("Dll_IFacturacion");


            FrmVersion.Add("SC_CompressLib");
            FrmVersion.Add("SC_ControlsCS");
            FrmVersion.Add("SC_SolutionsSystem");
            FrmVersion.Add("DllFarmaciaSoft");
            FrmVersion.Add("DllTransferenciaSoft");


            foreach(string z in Modulos )
            {
                if (z != "")
                {
                    FrmVersion.Add(z); 
                }
            }

            // Realizar el registro de módulos con las versiones encontradas 
            FrmVersion.Procesar();

            if (MostrarInterface)
            {
                FrmVersion.ShowDialog();
            }

        }

        public static void MostrarCambiarPasswordUsuario()
        {
            FrmCambiarPassword pass = new FrmCambiarPassword();
            pass.ShowDialog();
        }

        public static void MostrarLogErrores()
        {
            if (DtGeneral.EsAdministrador)
            {
                FrmListadoDeErrores ex = new FrmListadoDeErrores();
                ex.ShowDialog();
            }
            else
            {
                General.msjUser("El usuario conectado no tiene permisos para accesar a esta opción.");
            }  
        }

        private static void ObtenerServidorLocal()
        {
            bEsServidorDeRedLocal = false; 
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            clsGrabarError Error = new clsGrabarError(cnn.DatosConexion, dpDatosApp, "DtGeneral");

            string sSql = string.Format(" Select IdTerminal, Nombre, MAC_Address, EsServidor " +
                " From CFGC_Terminales (NoLock) " +
                " Where replace(MAC_Address, '-', '') in ( {0} )  and EsServidor = 1 ", ListaDeMacAddress("'"));
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerServidorLocal()"); 
            }
            else
            {
                bEsServidorDeRedLocal = leer.Leer(); 
            }
        }

        public static void CFG_Ubicaciones_Estandar() 
        {
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            clsGrabarError Error = new clsGrabarError(cnn.DatosConexion, dpDatosApp, "DtGeneral");

            dtsUBI_Estandar = new DataSet();            

            string sSql = 
                string.Format(
                " Select dbo.fg_Tiene_CFG_UbicacionesEstandar( '{0}', '{1}', '{2}' ) as Resultado \n " + 
                " Select * From CFG_ALMN_Ubicaciones_Estandar Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' ", 
                EmpresaConectada, EstadoConectado, FarmaciaConectada);

            ////if (!GnFarmacia.ManejaUbicacionesEstandar)
            ////{
            ////    bUbicacionesEstandar = true; 
            ////}

            if (GnFarmacia.ManejaUbicacionesEstandar)
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CFG_Ubicaciones_Estandar()");
                }
                else
                {
                    if (leer.Leer())
                    {
                        if (leer.CampoInt("Resultado") == 1)
                        {
                            bUbicacionesEstandar = false;
                            ////dtsUBI_Estandar = null;
                        }
                        else
                        {
                            bUbicacionesEstandar = true;
                            ////dtsUBI_Estandar.Tables.Add(leer.Tabla(2).Copy());
                        }
                        dtsUBI_Estandar.Tables.Add(leer.Tabla(2).Copy());
                    }
                    else
                    {
                        dtsUBI_Estandar = null;
                    }
                }
            }
        }
        #endregion Funciones y Procedimientos Publicos

        #region Directorios 
        public static void CrearDirectorio(string NombreDirectorio)
        {
            try
            {
                if (!Directory.Exists(NombreDirectorio))
                {
                    Directory.CreateDirectory(NombreDirectorio); 
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion Directorios

        #region Expresiones regulares 
        public static string RFC_Formato( string RFC )
        {
            string sRegresa = "";

            sRegresa = RFC.Trim().Replace(" ", "");

            return sRegresa;
        }

        public static bool ValidarEstructura_RFC( string RFC )
        {
            bool bRegresa = false;
            string
            lsPatron = @"^[A-ZÑ&] {3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9] [A-Z,0-9][0-9A]$";
            lsPatron = "[A-Z,Ñ,&]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?";

            RFC = RFC_Formato(RFC);
            Regex loRE = new Regex(lsPatron);
            bRegresa = loRE.IsMatch(RFC);

            return bRegresa;
        }

        public static bool ValidarEstructura_CURP( string CURP )
        {
            bool bRegresa = false;
            string
            lsPatron = @"^[A-ZÑ&] {3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9] [A-Z,0-9][0-9A]$";
            lsPatron = "^[A-Z]{1}[AEIOUX]{1}[A-Z]{2}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[HM]{1}(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)[B-DF-HJ-NP-TV-Z]{3}[0-9A-Z]{1}[0-9]{1}$";

            CURP = RFC_Formato(CURP).ToUpper();
            Regex loRE = new Regex(lsPatron);
            bRegresa = loRE.IsMatch(CURP);

            return bRegresa;
        }


        #endregion Expresiones regulares 

        #region Carga dinamica de dlls
        private static void LoadAssemblies_Neodynamic()
        {
            byte[] byFile;
            int iAssembliesCargados = 0;
            string sError = "";
            string sFileName = "";


            // BINDING FLAGS
            System.Reflection.BindingFlags flags = (System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);

            // CULTURE INFO
            System.Globalization.CultureInfo clinf = new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name);

            //if (!bSDK_Inicializado)
            {
                //bSDK_Inicializado = true;
                try
                {
                    sFileName = "Neodynamic.SDK.ThermalLabel.Dll";
                    byFile = DllFarmaciaSoft.Properties.Resources.Neodynamic_SDK_ThermalLabel;
                    Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sFileName, byFile, false);
                    iAssembliesCargados++;
                }
                catch (System.Exception ex)
                {
                    sError = ex.Message;
                }

                try
                {
                    sFileName = "Neodynamic.Windows.ThermalLabelEditor.Dll";
                    byFile = DllFarmaciaSoft.Properties.Resources.Neodynamic_Windows_ThermalLabelEditor;
                    Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sFileName, byFile, false);
                    iAssembliesCargados++;
                }
                catch (System.Exception ex)
                {
                    sError = ex.Message;
                }
            }

            //bSDK_Inicializado = iAssembliesCargados == 2;
        }
        #endregion Carga dinamica de dlls
    }
}
