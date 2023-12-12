using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using DllPedidosClientes; 

namespace DllPedidosClientes.Usuarios_y_Permisos
{
    /// <summary>
    /// Clase encarga de validar los usuarios del sistema
    /// </summary>
    public class clsEdoLogin
    {
        #region Declaración de variables
        FrmEdoLogin FrmLoginUser;
        FrmEdoLoginFarmacia FrmLoginUserUnidad;
        // FrmConect Conect;
        clsDatosCliente datosCliente; 

        //private wsConexion.wsConexionDB cnnWebServ = new wsConexion.wsConexionDB();
        private clsErrorManager error = new clsErrorManager();
        private clsLogError errorLog = new clsLogError();
        private DialogResult myResult = new DialogResult();

        private DataSet dtsError = new DataSet();
        private DataSet dtsClase = new DataSet();

        private string sEmpresa = "000", sEstado = "00", sSucursal = "0001", sIdPersonal = "", sUsuario = "", sPassword = "";
        private string sArbol = "";
        private string sLoginAdmin = "ADMINISTRADOR";
        private bool bEsAdminSys = false;
        private DataSet dtsUsuarios;
        private DataSet dtsPermisos;
        private DataSet dtsArbol;
        private string Name = "clsEdoLogin"; 
        // private DataSet dtsGruposUsuario;
        // private DataSet dtsSeguridadGrupo;

        private basGenerales Fg = new basGenerales();
        private Cls_Acceso_a_Datos_Sql Datos = new Cls_Acceso_a_Datos_Sql();

        private clsConexionSQL Conexion;
        private clsLeer Leer;
        private clsLeerWeb leerWeb; 
        private bool bEjecuto = false;
        private clsCriptografo crypto = new clsCriptografo();

        private string sQuerySucursales = "";
        private string sTabla = "";
        private bool bCanceladoPorUsuario = false; 

        // private bool bUsuariosCargados = false;
        #endregion

        #region Constructores de clase y destructor
        public clsEdoLogin()
        {
        } 

        ~clsEdoLogin()
        {
            FrmLoginUser = null;
            FrmLoginUserUnidad = null; 
            //Conect = null;
            Fg = null;
        }
        #endregion

        #region Propiedades publicas

        public DataSet Permisos
        {
            get{ return dtsPermisos; }
            set{ dtsPermisos = value; }
        }

        public bool EsAdmin
        {
            get { return bEsAdminSys; }
        }

        public string Arbol
        {
            get{ return sArbol; }
            set{ sArbol = value; }
        }

        public string Empresa
        {
            get { return sEmpresa; }
            set { sEmpresa = value; }
        }

        public string Estado
        {
            get { return sEstado; }
            set { sEstado = value; } 
        }

        public string Sucursal
        {
            get { return sSucursal; }
            set { sSucursal = value; } 
        }

        public string Personal
        {
            get { return sIdPersonal; }
            set { sIdPersonal = value; }
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

        public string QuerySucursales
        {
            get
            {
                return sQuerySucursales;
            }
            set
            {
                sQuerySucursales = value;
            }
        }

        public string TablaSucursales
        {
            get
            {
                return sTabla;
            }
            set
            {
                sTabla = value;
            }
        } 
        #endregion

        #region Funciones y procedimientos publicos 
        public void CancelarAutenticacion()
        {
            bCanceladoPorUsuario = true;
        }

        public bool AutenticarServicioSO() 
        {
            bool bRegresa = false;
            ////Conect = new FrmConect();

            ////Conect.ConectarServicioSO(); 
            ////if (Conect.bExisteFileConfig)
            ////{
            ////    if (Conect.bConexionEstablecida)
            ////    {
            ////        bRegresa = true;
            ////        DtGeneralPedidos.XmlEdoConfig.SetValues("UltimoUsuarioConectado", "ADMINISTRADOR"); 
            ////    }
            ////}

            return bRegresa;
        }

        public bool AutenticarServicio()
        {
            bool bRegresa = false;
            ////Conect = new FrmConect();
            ////Fg.CentrarForma(Conect);
            ////Conect.ShowDialog();

            ////if (Conect.bExisteFileConfig)
            ////{
            ////    if (Conect.bConexionEstablecida)
            ////    {
            ////        bRegresa = true;
            ////        DtGeneralPedidos.XmlEdoConfig.SetValues("UltimoUsuarioConectado", "ADMINISTRADOR"); 
            ////    }
            ////}

            return bRegresa;
        }

        public bool AutenticarUsuario()
        {
            bool bRegresa = false; //, bValidando = true;

            if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Regional)
            {
                FrmLoginUser = new FrmEdoLogin();
                Fg.CentrarForma(FrmLoginUser);

                FrmLoginUser.sQuery = sQuerySucursales;
                FrmLoginUser.sTabla = sTabla;
                FrmLoginUser.sArbol = sArbol;
                FrmLoginUser.dtsPermisos = dtsPermisos;
                FrmLoginUser.ShowDialog();

                bRegresa = FrmLoginUser.bUsuarioLogeado;

                DtGeneralPedidos.EstadoConectado = FrmLoginUser.sEstado;
                DtGeneralPedidos.FarmaciaConectada = FrmLoginUser.sSucursal;
                dtsPermisos = FrmLoginUser.dtsPermisos;
            }

            if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
            {
                FrmLoginUserUnidad = new FrmEdoLoginFarmacia();
                Fg.CentrarForma(FrmLoginUserUnidad);

                FrmLoginUserUnidad.sQuery = sQuerySucursales;
                FrmLoginUserUnidad.sTabla = sTabla;
                FrmLoginUserUnidad.sArbol = sArbol;
                FrmLoginUserUnidad.dtsPermisos = dtsPermisos;
                FrmLoginUserUnidad.ShowDialog();

                bRegresa = FrmLoginUserUnidad.bUsuarioLogeado;

                DtGeneralPedidos.EstadoConectado = FrmLoginUserUnidad.sEstado;
                DtGeneralPedidos.FarmaciaConectada = FrmLoginUserUnidad.sSucursal;
                dtsPermisos = FrmLoginUserUnidad.dtsPermisos;
            }


            return bRegresa;
        }

        private void CargarUsuarios()
        {
            //if (!bUsuariosCargados)
            {
                Conexion = new clsConexionSQL(General.DatosConexion);
                Conexion.SetConnectionString();

                dtsUsuarios = new DataSet();
                string sSql = "";
                    
                //sSql = string.Format( "Select IdEstado, IdSucursal, LoginUser, Password, Status " +
                //    " From Net_Usuarios (NoLock) Where IdEstado = '{0}' and IdSucursal = '{1}' ", sEstado, sSucursal);

                sSql = string.Format("Select IdEstado, IdSucursal, IdPersonal, LoginUser, Password, Status " +
                    " From Net_Usuarios (NoLock) Where IdEstado = '{0}' and IdSucursal = '{1}' ", sEstado, sSucursal);


                ///////////////////////////////////////////////////////////////////////////////////// 
                ////if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Regional)
                {
                        sSql = string.Format("Select IdEstado, IdFarmacia as IdSucursal, IdUsuario as IdPersonal, Login as LoginUser, Password, Status " +
                        " From Net_Regional_Usuarios (NoLock) Where IdEstado = '{0}' and IdFarmacia = '{1}' ", sEstado, sSucursal);
                }

                ////if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
                ////{
                ////    sSql = string.Format("Select IdEstado, IdFarmacia as IdSucursal, IdUsuario as IdPersonal, Login as LoginUser, Password, Status " +
                ////        " From Net_Unidad_Usuarios (NoLock) Where IdEstado = '{0}' and IdFarmacia = '{1}' ", sEstado, sSucursal);
                ////}

                dtsUsuarios = (DataSet)EjecutarQuery(sSql, "Usuarios");

                //if ( bEjecuto ) 
                //    bUsuariosCargados = true;
            }
        }

        private void ArbolNavegacion()
        {
            dtsPermisos = new DataSet();
            string sSql = "";

            if (bEsAdminSys)
            {
                sSql = string.Format(" Exec sp_Navegacion '{0}' ", sArbol);
            }
            else
            {
                sSql = string.Format(" Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", sEstado, sSucursal, sArbol, sUsuario);
            }

            ////// Revisar manejo de Permisos a detalle 
            ////sSql = string.Format(" Exec sp_Navegacion '{0}' ", sArbol);
            dtsPermisos = (DataSet)EjecutarQuery(sSql, "Arbol");
        }

        public bool AutenticarUsuarioLogin()
        {
            bool bRegresa = true;
            string mySucursal = sSucursal;

            CargarUsuarios();

            if (!bEjecuto)
            {
                bRegresa = false;
                errorLog.AgregarError("Ocurrió un error al autenticar el usuario.", "", "", "AutenticarUsuario()");
                error = new clsErrorManager(errorLog.ListaErrores);

                if (!bCanceladoPorUsuario)
                {
                    myResult = error.MostrarVentanaError(true, false, errorLog.ListaErrores);
                }
            }
            else
            {
                // Verificar SA
                bEsAdminSys = false;
                if (sLoginAdmin == sUsuario.ToUpper())
                {
                    if (sPassword.Trim().ToUpper() != General.DatosConexion.Password.ToUpper())
                    {
                        errorLog = new clsLogError();
                        errorLog.AgregarError("La contraseña especificada para el inicio de sesión es inválida.", "", "", "AutenticarUsuario()");
                        error = new clsErrorManager(errorLog.ListaErrores);
                        myResult = error.MostrarVentanaError(true, false, errorLog.ListaErrores);

                        if (!bCanceladoPorUsuario)
                        {
                            myResult = error.MostrarVentanaError(true, false, errorLog.ListaErrores);
                        }

                        bRegresa = false;
                    }
                    else
                    {
                        bEsAdminSys = true;
                        DtGeneralPedidos.IdPersonal = "0000";
                        DtGeneralPedidos.NombrePersonal = "ADMINISTRADOR";
                    }
                }
                else
                {
                    bRegresa = UsuarioValido();
                }

                if (bRegresa)
                {
                    ArbolNavegacion();
                    //GetPermisosUsuarios();
                    DtGeneralPedidos.XmlEdoConfig.SetValues("UltimoUsuarioConectado", sUsuario);
                    DtGeneralPedidos.XmlEdoConfig.SetValues("IdEmpresa", sEmpresa);
                    DtGeneralPedidos.XmlEdoConfig.SetValues("IdEstado", sEstado);
                    DtGeneralPedidos.XmlEdoConfig.SetValues("IdSucursal", sSucursal);
                }
            }

            return bRegresa;
        } 
        #endregion 
        
        #region Funciones y procedimientos privados
        private bool UsuarioValido()
        {
            bool bRegresa = true;
            DataRow[] myDr;
            string sUser = "", sPass = "", sStatus = "";
            errorLog = new clsLogError();

            if (dtsUsuarios.Tables.Count > 0)
            {
                if (dtsUsuarios.Tables["Usuarios"].Rows.Count > 0)
                {
                    myDr = dtsUsuarios.Tables["Usuarios"].Select("1=0");
                    //////////////////////////////////////////////////////// 
                    ////if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Regional)
                    ////{
                    ////    myDr = dtsUsuarios.Tables["Usuarios"].Select("IdEstado = '" + sEstado + "' and LoginUser = '" + sUsuario + "'");
                    ////}

                    ////if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
                    {
                        myDr = dtsUsuarios.Tables["Usuarios"].Select("IdEstado = '" + sEstado + "' and IdSucursal = '" + sSucursal + "' and LoginUser = '" + sUsuario + "'");
                    }


                    if (myDr.Length > 0)
                    {
                        sIdPersonal = myDr[0]["IdPersonal"].ToString();
                        sUser = myDr[0]["LoginUser"].ToString();
                        sPass = myDr[0]["Password"].ToString();
                        sStatus = myDr[0]["Status"].ToString().ToUpper();
                    }

                    if (sUser == "")
                    {
                        bRegresa = false;
                        errorLog.AgregarError("El usuario no esta dado de alta en el sistema.", "", "", "AutenticarUsuario()");
                    }

                    if ( bRegresa )
                    {
                        string sPassAux = ""; // crypto.PasswordEncriptar(sEstado + sSucursal + sIdPersonal + sPassword.ToUpper()); 

                        //////////////////////////////////////////////// 
                        ////if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Regional)
                        ////{
                        ////    sPassAux = crypto.PasswordEncriptar(sEstado + sUsuario + sPassword.ToUpper());
                        ////}

                        //////if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
                        {
                            sPassAux = crypto.PasswordEncriptar(sEstado + sSucursal + sUsuario + sPassword.ToUpper());
                        }

                        if (sPass == "" || sPass != sPassAux)
                        {
                            bRegresa = false;
                            errorLog.AgregarError("La contraseña especificada para el inicio de sesión es inválida.", "", "", "AutenticarUsuario()");
                        }
                        else
                        {
                            DtGeneralPedidos.PasswordUsuario = sPassAux;
                        }

                    }

                    if (bRegresa && sStatus != "A")
                    {
                        bRegresa = false;
                        errorLog.AgregarError("El usuario no es válido en el sistema", "", "", "AutenticarUsuario()");
                    }
                }
                else
                {
                    bRegresa = false;
                    errorLog.AgregarError("No existen usuarios dados de alta en el sistema para esta sucursal.", "", "", "AutenticarUsuario()");
                }
            }

            if (!bRegresa)
            {
                error = new clsErrorManager(errorLog.ListaErrores);

                if (!bCanceladoPorUsuario)
                {
                    myResult = error.MostrarVentanaError(true, false, errorLog.ListaErrores);
                }
            }

            return bRegresa;
        }

        private bool ArbolDeNavegacion()
        {
            bool bRegresa = false;
            string sSql = "";

            sSql = "Select A.Nombre as NombreArbol, N.* " +
                 " From Net_Navegacion N (NoLock) " +
                 " Inner Join Net_Arboles A (NoLock) On (N.Arbol = A.Arbol)" +
                 " Where N.Arbol = '" + sArbol + "' Order By Padre, IdOrden ";

            dtsArbol = new DataSet();
            dtsArbol = (DataSet)EjecutarQuery(sSql, "Arbol");
            bRegresa = ExistenDatosEnDataset(dtsArbol);

            return bRegresa;
        }

        private DataSet EjecutarQuery(string prtQuery, string prtTabla)
        {
            DataSet dtsResultado = new DataSet();
            Leer = new clsLeer(ref Conexion);

            bEjecuto = false;
            datosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, Name, "EjecutarQuery");
            leerWeb = new clsLeerWeb(General.Url, General.ArchivoIni, datosCliente);

            if (!bCanceladoPorUsuario)
            {
                if (!leerWeb.Exec(prtTabla, prtQuery))
                {
                    if (!bCanceladoPorUsuario)
                    {
                        General.Error.GrabarError(leerWeb.Error, General.DatosConexion, "", Application.ProductVersion, "Login", "Login", Leer.QueryEjecutado);
                    }
                }
                else
                {
                    leerWeb.RenombrarTabla(1, prtTabla);
                    dtsResultado = leerWeb.DataSetClase;
                    bEjecuto = true;
                }
            }

            return dtsResultado;
        }

        private object EjecutarQuery(string prtQuery, string prtTabla, string Nada)
        {
            object objRetorno = null;
            DataSet dtsRetorno = new DataSet("Vacio");
            Datos.CadenaDeConexion = General.CadenaDeConexion;

            try
            {
                if (General.ServidorEnRedLocal)
                {
                    objRetorno = (object)Datos.ObtenerDataset(prtQuery, prtTabla);
                }

            }
            catch (Exception e)
            {
                e = (Exception)objRetorno;
                dtsRetorno = new DataSet("Vacio");
                objRetorno = (object)dtsRetorno;

                errorLog = new clsLogError(e);
                error = new clsErrorManager(errorLog.ListaErrores);
                myResult = error.MostrarVentanaError(true, false, errorLog.ListaErrores);
            }

            return objRetorno;
        }

        private bool ExistenDatosEnDataset(DataSet dtsRevisar)
        {
            bool bRegresa = false;

            if (dtsRevisar.Tables.Count > 0)
            {
                if (dtsRevisar.Tables[0].Rows.Count > 0)
                    bRegresa = true;
            }

            return bRegresa;
        }
        #endregion

    }
}
