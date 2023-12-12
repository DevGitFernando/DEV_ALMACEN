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

using DllPedidosClientes;
using DllPedidosClientes.wsCnnClienteAdmin;

namespace DllPedidosClientes.Usuarios_y_Permisos
{
    internal partial class FrmConect : FrmBaseExt
    {
        #region Declaracion de variables
        //basGenerales Fg = new basGenerales();
        DllPedidosClientes.Usuarios_y_Permisos.clsEdoIniManager Ini; // = new clsIniManager();
        DllPedidosClientes.wsCnnClienteAdmin.wsCnnClientesAdmin webService = null;
        clsDatosConexion DatosCnn = null;
        clsPing Ping = new clsPing();
        clsCriptografo cryp = new clsCriptografo();
        clsFileConfig File;

        Thread thrConexion;
        clsLeer leerSesion = new clsLeer(); 

        // bool bConexionWeb = false;
        bool bConectando = true;
        public bool bExisteFileConfig = true;
        public bool bConexionEstablecida = false;
        private bool bCanceladoPorUsurario = false; 

        #endregion

        public FrmConect()
        {
            InitializeComponent();
        }

        #region Propiedades Publicas 
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
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Privados 
        private void FrmEdoConect_Load(object sender, EventArgs e)
        {
            toolTip.SetToolTip(lblCancelarConexion, "Cancelar conexión");
            Ini = new DllPedidosClientes.Usuarios_y_Permisos.clsEdoIniManager();

            if (!Ini.ExisteArchivo())
            {
                bExisteFileConfig = false;
                this.Close();
            }
            else
            {
                DtGeneralPedidos.XmlEdoConfig = Ini;
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
            clsLeer leerIni = new clsLeer();

            string sConexionWeb = ""; 
            string sServidor = ""; 
            string sWebService = ""; 
            string sDireccion = ""; 
            string sPaginaASMX = ""; 


            //string sConexionWeb = Ini.GetValues("ConexionWeb");
            //string sServidor = Ini.GetValues("Servidor");
            //string sWebService = Ini.GetValues("WebService");
            //string sDireccion = "http://" + sServidor + "/" + sWebService;
            //string sPaginaASMX = Ini.GetValues("PaginaASMX");

            // sDireccion = "http://" + "localhost:1264" + "/" + "wsSeguridad";

            //////// Metodo viejo 
            try
            {
                sConexionWeb = Ini.GetValues("ConexionWeb");
                sServidor = Ini.GetValues("Servidor");
                sWebService = Ini.GetValues("WebService");
                sDireccion = "http://" + sServidor + "/" + sWebService;
                sPaginaASMX = Ini.GetValues("PaginaASMX"); 
            }
            catch { }


            leerIni.ReadXml(Ini.XmlFile);
            leerIni.Leer();
            sConexionWeb = leerIni.Campo("ConexionWeb");
            sServidor = leerIni.Campo("Servidor");
            sWebService = leerIni.Campo("WebService");
            sDireccion = "http://" + sServidor + "/" + sWebService;
            sPaginaASMX = leerIni.Campo("PaginaASMX");

            // sPaginaASMX = "wsConexion.asmx";

            DataSet dtsCnn = new DataSet();
            DataSet dtsP = new DataSet();

            // Crear la conexion al servicio web, el servicio web siempre debe ser instalado
            // aunque este no sea utilizado por la aplicacion, esto para administrar en un solo
            // lugar el usuario y password de acceso al servidor de base de datos.

            General.WebService = null;
            webService = new DllPedidosClientes.wsCnnClienteAdmin.wsCnnClientesAdmin();
            General.PaginaASMX = sPaginaASMX;

            //// 2K111026.1511 Jesus Diaz 
            General.DatosDeServicioWeb.Servidor = sServidor;
            General.DatosDeServicioWeb.WebService = "wsVersiones"; 
            General.DatosDeServicioWeb.PaginaASMX = "wsUpdater.asmx";


            DtGeneralPedidos.DatosDeServicioWeb.Servidor = sServidor;
            DtGeneralPedidos.DatosDeServicioWeb.WebService = sWebService;
            DtGeneralPedidos.DatosDeServicioWeb.PaginaASMX = General.PaginaASMX; 

            DtGeneralPedidos.DatosDeServicioWebUpdater.Servidor = sServidor; 
            DtGeneralPedidos.DatosDeServicioWebUpdater.WebService = "wsVersiones";
            DtGeneralPedidos.DatosDeServicioWebUpdater.WebService = sWebService;
            
            // DtGeneralPedidos.DatosDeServicioWebUpdater.WebService = "wsVersionesTest"; 
            DtGeneralPedidos.DatosDeServicioWebUpdater.PaginaASMX = "wsUpdater"; 

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
                // Inicializar el resto de las variables General.es
                DatosCnn = new clsDatosConexion(dtsCnn);

                General.CadenaDeConexion = DatosCnn.CadenaDeConexion;
                General.ServidorEnRedLocal = Ping.Ping(DatosCnn.ServidorPing);
                General.DatosConexion = DatosCnn; 

                ////// Las impresiones tienen como primer opcion las Impresion Via WebService 
                ////General.ImpresionViaWeb = false;
                ////try
                ////{
                ////    if (sImpresionViaWeb == "" || sImpresionViaWeb == "0")
                ////    {
                ////        General.ImpresionViaWeb = false;
                ////    }
                ////    else if (sImpresionViaWeb == "1") 
                ////    {
                ////        General.ImpresionViaWeb = true; 
                ////    }
                ////}
                ////catch { }

                // Precargar los datos de conexion 
                bConexionEstablecida = ObtenerDatosParaIniciarSesion_Regional();
                ////if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Regional)
                ////{
                ////    bConexionEstablecida = ObtenerDatosParaIniciarSesion_Regional();
                ////}
                ////else 
                ////{ 
                ////}
            }

            //// Terminar el Sub-Proceso 
            // this.thrConexion = null; 
            this.Close(); 
        }

        private bool ObtenerDatosParaIniciarSesion_Regional()
        {
            bool bRegresa = false;
            clsLeer leerEstados = new clsLeer(); 
            clsLeerWeb Query; // = new clsLeerWeb(General.Url, General.DatosConexion, new SC_SolutionsSystem.Data.clsDatosCliente("DllFarmaciaSoft", this.Name, "FrmEdoLogin_Load"));
            clsDatosCliente datosCliente;

            string sSql = "";
            DataSet dtsDatosDeSesion = new DataSet();

            try
            {
                if (General.UsarWebService)
                {
                    //wsDb.Url = General.Url;
                    //dtsListaSucursales = wsDb.ObtenerDataset(Cryp.Encriptar(General.CadenaDeConexion), sQuery, sTabla);

                    datosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "FrmEdoLogin_Load");
                    Query = new clsLeerWeb(General.Url, General.ArchivoIni, datosCliente);

                    ////string myUrl = "http://lapjesus/wsCompras/wsCompras.asmx";
                    ////Query = new clsLeerWeb(myUrl, General.ArchivoIni, datosCliente);
                    //General.msjUser(Query.DatosConexion.Servidor + " " + Query.DatosConexion.BaseDeDatos + " " + Query.DatosConexion.Usuario + " " + Query.DatosConexion.Password);

                    try
                    {
                        sSql = "Select Distinct N.IdEstado, E.Nombre as NombreEstado, E.ClaveRenapo " +
                            " From Net_Regional_Usuarios N (NoLock) " +
                            " Inner Join CatEstados E (NoLock) On ( N.IdEstado = E.IdEstado ) " +
                            " Where N.Status = 'A' " +
                            " Order by N.IdEstado ";

                        if (!Query.Exec(sSql))
                        {
                            if (!bCanceladoPorUsurario)
                            {
                                Error.LogError(Query.MensajeError);
                                General.msjAviso("Ocurrió un error al obtener la información de configuración.");
                            }
                        }
                        else
                        {
                            //cboEstados.Add(Query.DataSetClase, true);
                            //cboEstados.SelectedIndex = 0;

                            Query.RenombrarTabla(1, "Estados"); 
                            dtsDatosDeSesion.Tables.Add(Query.DataTableClase);
                            leerEstados.DataSetClase = dtsDatosDeSesion; 
                            bRegresa = true; 

                            if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
                            {
                                bRegresa = false;
                                leerEstados.Leer(); 

                                sSql = string.Format("Select Distinct N.IdFarmacia, E.Farmacia, " +
                                    " (E.IdFarmacia + ' - ' + E.Farmacia) as NombreFarmacia, " +
                                    " N.IdEstado, E.Estado as NombreEstado, E.ClaveRenapo " +
                                    " From Net_Unidad_Usuarios N (NoLock) " +
                                    " Inner Join vw_Farmacias E (NoLock) On ( N.IdEstado = E.IdEstado and N.IdFarmacia = E.IdFarmacia ) " +
                                    " Where N.Status = 'A' and N.IdEstado = '{0}' " +
                                    " Order by N.IdEstado ", leerEstados.Campo("IdEstado"));

                                if (!Query.Exec(sSql))
                                {
                                    if (!bCanceladoPorUsurario)
                                    {
                                        Error.LogError(Query.MensajeError);
                                        General.msjAviso("Ocurrió un error al obtener la información de configuración.");
                                    }
                                }
                                else
                                {
                                    Query.RenombrarTabla(1, "Farmacias");
                                    dtsDatosDeSesion.Tables.Add(Query.DataTableClase);
                                    bRegresa = true; 
                                }
                            }

                            ////if (Query.Leer())
                            ////{
                            ////    dtsEstados = Query.DataSetClase; 
                            ////    cboEstados.Add(Query.DataSetClase, true, "IdEstado", "NombreEstado");
                            ////    cboEstados.SelectedIndex = 1;
                            ////}
                            ////cboEstados.Enabled = false;
                        }
                    }
                    catch
                    {
                        if (!bCanceladoPorUsurario)
                        {
                            ////cboEstados.SelectedIndex = 0;
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
                    ////cboEstados.SelectedIndex = 0;
                    Error.LogError(ex.Message.ToString());
                    General.msjAviso("No fue posible establecer conexión con el servidor. Intente de nuevo.");
                }
            }

            leerSesion.DataSetClase = dtsDatosDeSesion; 
            return bRegresa; 
        }

        private void ObtenerConfiguracionSO()
        {
            ////string sConexionWeb = Ini.GetValues("ConexionWeb");
            ////string sServidor = Ini.GetValues("Servidor");
            ////string sWebService = Ini.GetValues("WebService");
            ////string sDireccion = "http://" + sServidor + "/" + sWebService;

            ////// sDireccion = "http://" + "localhost:1264" + "/" + "wsSeguridad";

            ////string sPaginaASMX = Ini.GetValues("PaginaASMX");
            ////// sPaginaASMX = "wsConexion.asmx";

            ////DataSet dtsCnn = new DataSet();
            ////DataSet dtsP = new DataSet();

            ////// Crear la conexion al servicio web, el servicio web siempre debe ser instalado
            ////// aunque este no sea utilizado por la aplicacion, esto para administrar en un solo
            ////// lugar el usuario y password de acceso al servidor de base de datos.

            ////General.WebService = null;
            ////webService = new DllProveedores.wsProveedores.wsCnnProveedores();
            ////General.PaginaASMX = sPaginaASMX;

            ////if (sConexionWeb.ToUpper() == "SI")
            ////{
            ////    webService.Url = sDireccion + General.PaginaASMX;
            ////    General.Url = webService.Url;
            ////    General.UsarWebService = true; 
            ////    bConexionEstablecida = true;
            ////} 
        } 

        #endregion Funciones y Procedimientos Privados

        public void ConectarServicioSO()
        {
            ////Ini = new DllProveedores.Usuarios_y_Permisos.clsIniManager();

            ////if (!Ini.ExisteArchivo())
            ////{
            ////    bExisteFileConfig = false;
            ////    this.Close();
            ////}
            ////else
            ////{
            ////    GnProveedores.XmlConfig = Ini;
            ////    ObtenerConfiguracionSO(); 
            ////} 
        }


        private void lblCancelarConexion_Click(object sender, EventArgs e)
        {
            CancelarConexion(); 
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