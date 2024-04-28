using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using DllFarmaciaSoft.wsFarmaciaSoftGn;

namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    internal partial class FrmEdoConect : FrmBaseExt
    {
        #region Declaracion de variables
        //basGenerales Fg = new basGenerales();
        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoIniManager Ini; // = new clsIniManager();
        DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion webService = null;
        clsDatosConexion DatosCnn = null;
        clsPing Ping = new clsPing();
        clsCriptografo cryp = new clsCriptografo();
        clsEdoFileConfig File;

        public string sQuery = "";
        public string sTabla = "";
        clsCriptografo Cryp = new clsCriptografo(); 
        clsLeer leerSesion = new clsLeer(); 

        Thread thrConexion; 

        // bool bConexionWeb = false; 
        private bool bEsLoginServicio = false; 
        bool bConectando = true;
        public bool bExisteFileConfig = true;
        public bool bConexionEstablecida = false;
        private bool bCanceladoPorUsurario = false;
        private bool bXmlEnDirectorioApp = false;
        #endregion Declaracion de variables 

        #region Conexion  
        string sAlias = ""; 
        string sConexionWeb = "";
        string sServidor = "";
        string sWebService = "";
        string sDireccion = "";
        string sPaginaASMX = "";
        string sImpresionViaWeb = "";
        string sSSL = "";
        int iItemSeleccionado = 1;


        private bool bEsIntercalcionCorrecta = false;
        string sIntercalacion_Standard = "Latin1-General, case-insensitive, accent-insensitive, kanatype-insensitive, width-insensitive";
        string sIntercalacion_Servidor = ""; 
        #endregion Conexion

        //public FrmEdoConect():this(1) 
        //{ 
        //}

        public FrmEdoConect(int ItemConexion)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            iItemSeleccionado = ItemConexion; 
        }

        public FrmEdoConect(bool EsServicio)
        {
            InitializeComponent();
            bEsLoginServicio = true; 
            CheckForIllegalCrossThreadCalls = false; 
        }

        #region Propiedades Publicas 
        public DataSet Empresas
        {
            get
            {
                DataSet dts = new DataSet();
                dts.Tables.Add(leerSesion.Tabla("Empresas"));
                return dts;
            }
        }

        public DataSet Estados
        {
            get 
            {
                DataSet dts = new DataSet();
                dts.Tables.Add(leerSesion.Tabla("Estados")); 
                return dts; 
            }
        }

        public DataSet Farmacias
        {
            get
            {
                DataSet dts = new DataSet();
                dts.Tables.Add(leerSesion.Tabla("Farmacias"));
                return dts;
            }
        }

        public bool XmlEnDirectorioApp
        {
            get { return bXmlEnDirectorioApp; }
            set { bXmlEnDirectorioApp = value; }
        }

        public int ConexionSeleccionada
        {
            get { return iItemSeleccionado; }
            set { iItemSeleccionado = value; }
        }

        public bool EsIntercalacion_Valida
        {
            get 
            {
                bEsIntercalcionCorrecta = sIntercalacion_Standard.ToUpper() == sIntercalacion_Servidor.ToUpper(); 
                return bEsIntercalcionCorrecta; 
            }
        }

        public string Intercalacion_Detectada
        {
            get { return sIntercalacion_Servidor; } 
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Privados
        private void FrmEdoConect_Load(object sender, EventArgs e)
        {
            toolTip.SetToolTip(btnCancelarConexion, "Cancelar conexión");
            toolTip.SetToolTip(lblCancelarConexion, "Cancelar conexión");

            Ini = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoIniManager();
            Ini.Item_Seleccionado = iItemSeleccionado; 
            Ini.XmlEnDirectorioApp = bXmlEnDirectorioApp; 

            if (!Ini.ExisteArchivo())
            {
                bExisteFileConfig = false;
                this.Close();
            }
            else
            {
                DtGeneral.XmlEdoConfig = Ini;
                timer2.Enabled = true;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            bConexionEstablecida = false;

            if (bConectando)
            {
                bConectando = false;
                //timer2.Enabled = false;

                if (General.SolicitarConfiguracionLocal)
                {
                    File = new clsEdoFileConfig();

                    if (!File.BuscarConfiguracion())
                    {
                        bExisteFileConfig = false;
                        this.Close();
                    }
                }
                else
                {
                    timer2.Stop();
                    ObtenerConfiguracionBackGround(); 
                    
                    ////ObtenerConfiguracion(); 

                    ////timer2.Enabled = false;
                    ////bConectando = false;
                    ////this.Close();
                }
            }
        }

        private void ObtenerConfiguracionBackGround()
        {
            thrConexion = new Thread(this.ObtenerConfiguracion);
            thrConexion.Name = "ObtenerConfiguracion";
            thrConexion.Start(); 
        }

        private void ObtenerConfiguracion()
        {
            FrmSeleccionarConexion seleccionarConexion;
            ////int iItemSeleccionado = 1; 

            clsLeer leerIni = new clsLeer();
            clsLeer leerIniSeleccion = new clsLeer(); 
            bool bConexionSeleccionada = false;

            try
            {

                ////// Metodo viejo, forzar agregar las columnas en caso de no existir 
                try
                {
                    sAlias = Ini.GetValues("Alias", "Default");
                    sConexionWeb = Ini.GetValues("ConexionWeb");
                    sServidor = Ini.GetValues("Servidor");
                    sWebService = Ini.GetValues("WebService");
                    sDireccion = "http://" + sServidor + "/" + sWebService;
                    sPaginaASMX = Ini.GetValues("PaginaASMX");
                    sImpresionViaWeb = Ini.GetValues("TipoImpresion", "1").Trim();

                    Ini.GetValues("ImpresoraTickets", "").Trim();
                    Ini.GetValues("ImpresoraDocumentos", "").Trim();


                    SC_SolutionsSystem.Idiomas.Idiomas.Idioma = Ini.GetValues("Lenguaje", "es-mx").Trim();
                    sSSL = Ini.GetValues("SSL", "0").Trim();
                }
                catch { }


                Ini.Item_Seleccionado = iItemSeleccionado; 
                leerIni.ReadXml(Ini.XmlFile);

                leerIni.DataRowClase = leerIni.Tabla(1).Rows[iItemSeleccionado - 1];
                leerIni.Leer();



                sConexionWeb = leerIni.Campo("ConexionWeb");
                sSSL = leerIni.Campo("SSL");
                sSSL = sSSL == "1" ? "s" : "";

                sServidor = leerIni.Campo("Servidor");
                sWebService = leerIni.Campo("WebService");
                sDireccion = string.Format("http{0}://{1}/{2}", sSSL, sServidor, sWebService);
                sPaginaASMX = leerIni.Campo("PaginaASMX");
                sImpresionViaWeb = leerIni.Campo("TipoImpresion");
                SC_SolutionsSystem.Idiomas.Idiomas.Idioma = leerIni.Campo("Lenguaje");

                bConexionSeleccionada = true;

            }
            catch { }


            if (!bConexionSeleccionada)
            {
                this.Close(); 
            }
            else
            {
                ObtenerConfiguracion_Conectar(); 
            }
        }

        private void ObtenerConfiguracion_Conectar()
        {
            clsLeer leerIni = new clsLeer(); 

            ////string sConexionWeb = ""; 
            ////string sServidor = "";
            ////string sWebService = "";
            ////string sDireccion = ""; 
            ////string sPaginaASMX = "";
            ////string sImpresionViaWeb = "";
            ////string sSSL = ""; 

            //////// Metodo viejo 
            //try 
            //{
            //    sConexionWeb = Ini.GetValues("ConexionWeb"); 
            //    sServidor = Ini.GetValues("Servidor"); 
            //    sWebService = Ini.GetValues("WebService"); 
            //    sDireccion = "http://" + sServidor + "/" + sWebService; 
            //    sPaginaASMX = Ini.GetValues("PaginaASMX"); 
            //    sImpresionViaWeb = Ini.GetValues("TipoImpresion", "1").Trim();
            //    SC_SolutionsSystem.Idiomas.Idiomas.Idioma = Ini.GetValues("Lenguaje", "es-mx").Trim();
            //    sSSL = Ini.GetValues("SSL", "0").Trim(); 
            //}
            //catch{ }


            //leerIni.ReadXml(Ini.XmlFile);
            //leerIni.Leer(); 
            //sConexionWeb = leerIni.Campo("ConexionWeb");
            //sSSL = leerIni.Campo("SSL");
            //sSSL = sSSL == "1" ? "s" : "";
            
            //sServidor = leerIni.Campo("Servidor");
            //sWebService = leerIni.Campo("WebService");
            //sDireccion = string.Format("http{0}://{1}/{2}", sSSL, sServidor, sWebService);
            //sPaginaASMX = leerIni.Campo("PaginaASMX");
            //sImpresionViaWeb = leerIni.Campo("TipoImpresion");
            //SC_SolutionsSystem.Idiomas.Idiomas.Idioma = leerIni.Campo("Lenguaje");



            // sPaginaASMX = "wsConexion.asmx";

            DataSet dtsCnn = new DataSet();
            DataSet dtsP = new DataSet();

            // Crear la conexion al servicio web, el servicio web siempre debe ser instalado
            // aunque este no sea utilizado por la aplicacion, esto para administrar en un solo
            // lugar el usuario y password de acceso al servidor de base de datos.

            General.WebService = null;
            webService = new DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion();
            General.PaginaASMX = sPaginaASMX;

            General.DatosDeServicioWeb.Servidor = sServidor; 
            General.DatosDeServicioWeb.WebService = sWebService;
            General.DatosDeServicioWeb.PaginaASMX = sPaginaASMX;

            DtGeneral.DatosDeServicioWeb.Servidor = sServidor;
            DtGeneral.DatosDeServicioWeb.WebService = sWebService;
            DtGeneral.DatosDeServicioWeb.PaginaASMX = sPaginaASMX;

            DtGeneral.DatosDeServicioWebUpdater.Servidor = sServidor; 
            DtGeneral.DatosDeServicioWebUpdater.WebService =  "wsVersiones"; 
            DtGeneral.DatosDeServicioWebUpdater.PaginaASMX = "wsUpdater";


            sConexionWeb = "SI";  //// Forzar la conexión web 
            if (sConexionWeb.ToUpper() == "SI")
            {
                webService.Url = sDireccion + General.PaginaASMX;
                webService.Timeout = 60000; 
                General.Url = webService.Url;
                General.UsarWebService = true;

                // bConexionEstablecida = true;
                try
                {
                    dtsCnn = webService.ConexionEx(General.ArchivoIni); 
                    bConexionEstablecida = true;
                }
                catch (Exception ex)
                {
                    if (!bCanceladoPorUsurario)
                    {
                        Error.LogError("{ ObtenerConfiguracion() } -- " + ex.Message, System.IO.FileAttributes.Hidden);
                        General.msjAviso("No se pudo establecer conexión con el servidor web, intente de nuevo por favor.");
                        // Application.Exit();
                    }
                }
            }

            if (bConexionEstablecida)
            {
                //// Inicializar el resto de las variables General.es
                DatosCnn = new clsDatosConexion(dtsCnn);
                DatosCnn.ConectionTimeOut = TiempoDeEspera.SinLimite;
                DatosCnn.SetInformacion(DtGeneral.ItemSeguridad);


                General.CadenaDeConexion = DatosCnn.CadenaDeConexion;
                General.ServidorEnRedLocal = true; //  Ping.Ping(DatosCnn.ServidorPing);
                General.DatosConexion = DatosCnn;
                General.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite; 


                //// Las impresiones tienen como primer opcion las Impresion Via WebService 
                General.ImpresionViaWeb = false;
                try
                {
                    if (sImpresionViaWeb == "" || sImpresionViaWeb == "0")
                    {
                        General.ImpresionViaWeb = false;
                    }
                    else if (sImpresionViaWeb == "1")
                    {
                        General.ImpresionViaWeb = true;
                    }
                }
                catch { }

                if (bEsLoginServicio) 
                {
                    bConexionEstablecida = true; 
                }
                else 
                {
                    // Precargar los datos de conexion 
                    bConexionEstablecida = ObtenerDatosParaIniciarSesion();
                }
            }

            //// Terminar el Sub-Proceso 
            // this.thrConexion = null; 
            this.Close(); 
        }

        private bool ObtenerDatosParaIniciarSesion()
        {
            bool bRegresa = false; 
            clsLeerWeb Query; // = new clsLeerWeb(General.Url, General.DatosConexion, new SC_SolutionsSystem.Data.clsDatosCliente("DllFarmaciaSoft", this.Name, "FrmEdoLogin_Load"));
            clsDatosCliente datosCliente; 

            string sSql = "";
            string sFiltroAlmacen = " and F.EsAlmacen = 0 "; // Solo farmacias  
            string sFiltroUnidosis = " and F.EsUnidosis = 0 "; 
            DataSet dtsDatosDeSesion = new DataSet(); 
             
            try
            {
                if (!General.UsarWebService)
                {
                    wsConexionDB myWeb = new wsConexionDB();
                    dtsDatosDeSesion = myWeb.ObtenerDataset(Cryp.Encriptar(General.CadenaDeConexion), sQuery, sTabla);
                }
                else 
                {
                    datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ObtenerDatosParaIniciarSesion");
                    Query = new clsLeerWeb(General.Url, General.DatosConexion, datosCliente);
                    //General.msjUser(Query.DatosConexion.Servidor + " " + Query.DatosConexion.BaseDeDatos + " " + Query.DatosConexion.Usuario + " " + Query.DatosConexion.Password);

                    try
                    {
                        sSql = "Select IdEmpresa, Nombre, NombreCorto, EsDeConsignacion, RFC, EdoCiudad, Colonia, Domicilio, CodigoPostal, Status \n" + 
                               "From vw_CFG_Empresas (NoLock) \n" +  
                               "Where Status = 'A' \n" + 
                               "Order by IdEmpresa \n";  
                        if (!Query.Exec(sSql)) 
                        {
                            if (!bCanceladoPorUsurario)
                            {
                                Error.LogError(Query.MensajeError); 
                                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
                            }
                        }
                        else 
                        {
                            Query.RenombrarTabla(1, "Empresas"); 
                            dtsDatosDeSesion.Tables.Add(Query.DataTableClase);

                            sSql = "Select IdEstado, NombreEstado, ClaveRenapo, IdEmpresa \n" + 
                                   "From vw_EmpresasEstados (NoLock) \n" + 
                                   "Where StatusEdo = 'A'  And StatusEdo = 'A' And StatusRelacion = 'A' \n" + 
                                   "Order by IdEstado \n";  // ACTIVAR  
                            if (!Query.Exec(sSql)) 
                            {
                                if (!bCanceladoPorUsurario)
                                {
                                    Error.LogError(Query.MensajeError);
                                    General.msjError("Ocurrió un error al obtener la lista de Estados.");
                                }
                            }
                            else 
                            {
                                Query.RenombrarTabla(1, "Estados"); 
                                dtsDatosDeSesion.Tables.Add(Query.DataTableClase);

                                sSql = "Select \n" + 
                                    "\tF.IdFarmacia, F.Farmacia, (F.IdFarmacia + ' - ' + F.Farmacia) as NombreFarmacia, F.IdJurisdiccion, F.Jurisdiccion, \n" + 
                                    "\tF.IdEstado, F.IdEmpresa, F.ManejaVtaPubGral, F.ManejaControlados, F.EsAlmacen, F.EsUnidosis \n" + 
                                    "From vw_EmpresasFarmacias F (NoLock) \n" + 
                                    "Where StatusRelacion  = 'A' \n" + 
                                    "Order by IdEstado, IdFarmacia \n"; 

                                ////// Habilitar solo las Unidades requeridas en el Servidor. 
                                if (DtGeneral.SoloMostrarUnidadesConfiguradas)
                                {
                                    if (DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen)
                                    {
                                        // Solo almacenes 
                                        sFiltroAlmacen = " and F.EsAlmacen = 1 "; // Solo Almacenes 
                                    }

                                    if (DtGeneral.ModuloEnEjecucion == TipoModulo.FarmaciaUnidosis || DtGeneral.ModuloEnEjecucion == TipoModulo.AlmacenUnidosis)
                                    {
                                        sFiltroUnidosis = " and F.EsUnidosis = 1 ";
                                        if (DtGeneral.ModuloEnEjecucion == TipoModulo.AlmacenUnidosis)
                                        {
                                            sFiltroAlmacen = " and F.EsAlmacen = 1 "; // Solo Almacenes Unidosis 
                                        }
                                    }

                                    if (DtGeneral.ModuloEnEjecucion == TipoModulo.Auditor)
                                    {
                                        sFiltroAlmacen = " ";
                                        sFiltroUnidosis = " "; 
                                    }

                                    if (DtGeneral.ModuloEnEjecucion == TipoModulo.RFID)
                                    {
                                        sFiltroAlmacen = " ";
                                        sFiltroUnidosis = " "; 
                                    }

                                    sSql = string.Format(
                                        "Select Distinct \n " + 
                                        "\tF.IdFarmacia, F.Farmacia, (F.IdFarmacia + ' - ' + F.Farmacia) as NombreFarmacia, F.IdJurisdiccion, F.Jurisdiccion, \n" +
                                        "\tF.IdEstado, F.IdEmpresa, F.ManejaVtaPubGral, F.ManejaControlados, F.EsAlmacen, F.EsUnidosis \n" +
                                        "From vw_EmpresasFarmacias F (NoLock) \n" +
                                        "Inner Join CFG_Svr_UnidadesRegistradas L (NoLock) On ( F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and L.Status = 'A' ) \n" +
                                        "Where F.Status = 'A' {0}  {1} \n" +
                                        "Order by F.IdEstado, F.IdFarmacia \n", sFiltroAlmacen, sFiltroUnidosis);
                                }

                                // Habilitar solo las Unidades requeridas en el Servidor Regional (Modulo Distribuidor). 
                                if (DtGeneral.SoloMostrarUnidadesConfiguradasRegionales)
                                {
                                    sSql = "Select Distinct \n" +
                                        "\tF.IdFarmacia, F.Farmacia, (F.IdFarmacia + ' - ' + F.Farmacia) as NombreFarmacia, F.IdJurisdiccion, F.Jurisdiccion, \n" +
                                        "\tF.IdEstado, F.IdEmpresa, F.ManejaVtaPubGral, F.ManejaControlados, F.EsAlmacen, F.EsUnidosis \n" +
                                        "From vw_EmpresasFarmacias F (NoLock) \n" +
                                        "Inner Join CFG_Svr_UnidadesRegistradas L (NoLock) On ( F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia ) \n" +
                                        "Where F.Status = 'A' and L.EsRegional = 1 \n"+ 
                                        "Order by F.IdEstado, F.IdFarmacia \n";
                                }

                                if (DtGeneral.ModuloEnEjecucion == TipoModulo.Facturacion
                                    || DtGeneral.ModuloEnEjecucion == TipoModulo.Regional
                                    || DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central
                                    || DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Regional
                                    )
                                {
                                    sSql = 
                                        "Select \n" +
                                        "\nF.IdFarmacia, F.Farmacia, (F.IdFarmacia + ' - ' + F.Farmacia) as NombreFarmacia, F.IdJurisdiccion, F.Jurisdiccion, \n" +
                                        "\nF.IdEstado, F.IdEmpresa, F.ManejaVtaPubGral, F.ManejaControlados, F.EsAlmacen, F.EsUnidosis \n" +
                                        "From vw_EmpresasFarmacias F (NoLock) \n" +
                                        "Inner Join CatFarmacias C (NoLock) \n" +
                                        "\t   On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia and C.IdTipoUnidad = '005' ) \n" +
                                        "Where StatusRelacion  = 'A' \n" +
                                        "Order by IdEstado, IdFarmacia \n"; 
                                }

                                if (!Query.Exec(sSql))
                                {
                                    if (!bCanceladoPorUsurario)
                                    {
                                        Error.LogError(Query.MensajeError);
                                        General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
                                    }
                                }
                                else 
                                {
                                    Query.RenombrarTabla(1, "Farmacias");
                                    dtsDatosDeSesion.Tables.Add(Query.DataTableClase);

                                    sSql = "Exec sp_helpsort ";
                                    if(Query.Exec(sSql))
                                    {
                                        Query.Leer();
                                        sIntercalacion_Servidor = Query.Campo("Server default collation");
                                    }


                                    bRegresa = true; // si no llega a este punto falla toda la funcion 
                                }
                            }
                        }
                    }
                    catch
                    {
                        if (!bCanceladoPorUsurario)
                        {
                            Error.LogError(Query.MensajeError);
                            General.msjAviso("Ocurrió un error al obtener la información de configuración.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (!bCanceladoPorUsurario)
                {
                    Error.LogError(ex.Message.ToString());
                    General.msjAviso("No fue posible establecer conexión con el servidor. Intente de nuevo.");
                }
            }

            leerSesion.DataSetClase = dtsDatosDeSesion; 
            return bRegresa; 
        }

        private void ObtenerConfiguracionSO()
        {
            bConexionEstablecida = false;

            string sConexionWeb = Ini.GetValues("ConexionWeb");
            string sServidor = Ini.GetValues("Servidor");
            string sWebService = Ini.GetValues("WebService");
            string sDireccion = "http://" + sServidor + "/" + sWebService;

            // sDireccion = "http://" + "localhost:1264" + "/" + "wsSeguridad";

            string sPaginaASMX = Ini.GetValues("PaginaASMX");
            // sPaginaASMX = "wsConexion.asmx";

            DataSet dtsCnn = new DataSet();
            DataSet dtsP = new DataSet();

            // Crear la conexion al servicio web, el servicio web siempre debe ser instalado
            // aunque este no sea utilizado por la aplicacion, esto para administrar en un solo
            // lugar el usuario y password de acceso al servidor de base de datos.

            General.WebService = null;
            webService = new DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion();
            General.PaginaASMX = sPaginaASMX;

            if (sConexionWeb.ToUpper() == "SI")
            {
                webService.Url = sDireccion + General.PaginaASMX;
                General.Url = webService.Url;
                General.UsarWebService = true;

                try
                {
                    System.Console.WriteLine(string.Format("Conectando con : {0} ", webService.Url));
                    dtsCnn = webService.ConexionEx(General.ArchivoIni);
                    bConexionEstablecida = true; 
                }
                catch (Exception ex)
                {
                    Error.LogError(ex.Message, System.IO.FileAttributes.Normal); 
                }
            }

            if (bConexionEstablecida)
            {
                try
                {
                    // Inicializar el resto de las variables General.es
                    DatosCnn = new clsDatosConexion(dtsCnn);

                    General.CadenaDeConexion = DatosCnn.CadenaDeConexion;
                    General.ServidorEnRedLocal = true; //  Ping.Ping(DatosCnn.ServidorPing);
                    General.DatosConexion = DatosCnn;
                    General.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite; 
                }
                catch
                {
                    bConexionEstablecida = false;
                }
            }
        } 

        #endregion Funciones y Procedimientos Privados

        public void ConectarServicioSO()
        {
            Ini = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoIniManager();
            Ini.XmlEnDirectorioApp = bXmlEnDirectorioApp;

            if (!Ini.ExisteArchivo())
            {
                bExisteFileConfig = false;
                this.Close();
            }
            else
            {
                DtGeneral.XmlEdoConfig = Ini;
                ObtenerConfiguracionSO(); 
            } 
        }

        private void FrmEdoConect_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Terminar el Sub-Proceso 
                this.thrConexion = null;
            }
            catch { }
            finally
            {
                // Terminar el Sub-Proceso 
                this.thrConexion = null; 
            }
        }

        private void CancelarConexion()
        {
            bCanceladoPorUsurario = true;
            bConexionEstablecida = false;

            try
            {
                this.thrConexion.Abort();
            }
            catch { }

            this.Hide(); 
        }

        private void btnCancelarConexion_Click(object sender, EventArgs e)
        {
            CancelarConexion(); 
        }

        private void lblCancelarConexion_Click(object sender, EventArgs e)
        {
            CancelarConexion(); 
        }

        private void lblCancelarConexion_MouseHover(object sender, EventArgs e)
        {
            lblCancelarConexion.BackColor = Color.Gainsboro; 
        }

        private void lblCancelarConexion_MouseLeave(object sender, EventArgs e)
        {
            lblCancelarConexion.BackColor = this.BackColor; 
        }  
    }
}