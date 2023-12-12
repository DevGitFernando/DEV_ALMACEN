using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using DllProveedores;
using DllProveedores.wsProveedores;

namespace DllProveedores.Usuarios_y_Permisos
{
    internal partial class FrmConect : FrmBaseExt
    {
        #region Declaracion de variables
        //basGenerales Fg = new basGenerales();
        DllProveedores.Usuarios_y_Permisos.clsIniManager Ini; // = new clsIniManager();
        DllProveedores.wsProveedores.wsCnnProveedores webService = null;
        clsDatosConexion DatosCnn = null;
        clsPing Ping = new clsPing();
        clsCriptografo cryp = new clsCriptografo();
        clsFileConfig File;

        // bool bConexionWeb = false;
        bool bConectando = true;
        public bool bExisteFileConfig = true;
        public bool bConexionEstablecida = false;
        #endregion

        public FrmConect()
        {
            InitializeComponent();
        }

        #region Funciones y Procedimientos Privados 
        private void FrmEdoConect_Load(object sender, EventArgs e)
        {
            Ini = new DllProveedores.Usuarios_y_Permisos.clsIniManager();

            if (!Ini.ExisteArchivo())
            {
                bExisteFileConfig = false;
                this.Close();
            }
            else
            {
                GnProveedores.XmlConfig = Ini;
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
                    File = new clsFileConfig();

                    if (!File.BuscarConfiguracion())
                    {
                        bExisteFileConfig = false;
                        this.Close();
                    }
                }
                else
                {

                    ObtenerConfiguracion(); 

                    timer2.Enabled = false;
                    bConectando = false;
                    this.Close();
                }
            }
        }

        private void ObtenerConfiguracion()
        {
            try
            {
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

                General.PaginaASMX = sPaginaASMX;
                General.Url = sDireccion + General.PaginaASMX;
                bConexionEstablecida = true;
            }
            catch 
            {
                bConexionEstablecida = false; 
            }
            ////General.WebService = null;
            ////webService = new DllProveedores.wsProveedores.wsCnnProveedores();
            ////General.PaginaASMX = sPaginaASMX;

            ////if (sConexionWeb.ToUpper() == "SI")
            ////{
            ////    webService.Url = sDireccion + General.PaginaASMX;
            ////    General.Url = webService.Url;
            ////    General.UsarWebService = true;

            ////    // bConexionEstablecida = true;
            ////    try
            ////    {
            ////        dtsCnn = webService.ConexionEx(General.ArchivoIni);
            ////        bConexionEstablecida = true;
            ////    }
            ////    catch (Exception ex)
            ////    {
            ////        Error.LogError(ex.Message, System.IO.FileAttributes.Hidden);
            ////        General.msjAviso("No se pudo establecer conexión con el servidor web, intente de nuevo por favor.");
            ////        // Application.Exit();
            ////    }
            ////}

            ////if (bConexionEstablecida)
            ////{
            ////    // Inicializar el resto de las variables General.es
            ////    DatosCnn = new clsDatosConexion(dtsCnn);

            ////    General.CadenaDeConexion = DatosCnn.CadenaDeConexion;
            ////    General.ServidorEnRedLocal = Ping.Ping(DatosCnn.ServidorPing);
            ////    General.DatosConexion = DatosCnn;
            ////}

        }

        private void ObtenerConfiguracionSO()
        {
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
            webService = new DllProveedores.wsProveedores.wsCnnProveedores();
            General.PaginaASMX = sPaginaASMX;

            if (sConexionWeb.ToUpper() == "SI")
            {
                webService.Url = sDireccion + General.PaginaASMX;
                General.Url = webService.Url;
                General.UsarWebService = true; 
                bConexionEstablecida = true;
            } 
        } 

        #endregion Funciones y Procedimientos Privados

        public void ConectarServicioSO()
        {
            Ini = new DllProveedores.Usuarios_y_Permisos.clsIniManager();

            if (!Ini.ExisteArchivo())
            {
                bExisteFileConfig = false;
                this.Close();
            }
            else
            {
                GnProveedores.XmlConfig = Ini;
                ObtenerConfiguracionSO(); 
            } 
        } 
    
    }
}