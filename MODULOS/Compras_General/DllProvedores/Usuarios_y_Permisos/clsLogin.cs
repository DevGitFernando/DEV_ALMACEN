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

namespace DllProveedores.Usuarios_y_Permisos
{
    /// <summary>
    /// Clase encarga de validar los usuarios del sistema
    /// </summary>
    public class clsLogin
    {
        #region Declaración de variables
        FrmLogin FrmLoginUser;
        FrmConect Conect;
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
        // private DataSet dtsGruposUsuario;
        // private DataSet dtsSeguridadGrupo;

        private basGenerales Fg = new basGenerales();
        private Cls_Acceso_a_Datos_Sql Datos = new Cls_Acceso_a_Datos_Sql();

        private clsConexionSQL Conexion;
        private clsLeer Leer;
        private bool bEjecuto = false;
        private clsCriptografo crypto = new clsCriptografo();

        private string sQuerySucursales = "";
        private string sTabla = "";
        private bool bUsuariosCargados = false;
        #endregion

        #region Constructores de clase y destructor
        public clsLogin(string sQuery, string sTablaQuery)
        {
            sQuerySucursales = sQuery;
            sTabla = sTablaQuery;
        }

        ~clsLogin()
        {
            FrmLoginUser = null;
            Conect = null;
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
        public bool AutenticarServicioSO() 
        {
            bool bRegresa = false;
            Conect = new FrmConect();

            Conect.ConectarServicioSO(); 
            if (Conect.bExisteFileConfig)
            {
                if (Conect.bConexionEstablecida)
                {
                    bRegresa = true;
                    GnProveedores.XmlConfig.SetValues("UltimoUsuarioConectado", "ADMINISTRADOR"); 
                }
            }

            return bRegresa;
        }

        public bool AutenticarServicio()
        {
            bool bRegresa = false;
            Conect = new FrmConect();
            Fg.CentrarForma(Conect);
            Conect.ShowDialog();

            if (Conect.bExisteFileConfig)
            {
                if (Conect.bConexionEstablecida)
                {
                    bRegresa = true;
                    GnProveedores.XmlConfig.SetValues("UltimoUsuarioConectado", "ADMINISTRADOR"); 
                }
            }

            return bRegresa;
        }

        public bool AutenticarUsuario()
        {
            bool bRegresa = false; //, bValidando = true;

            FrmLoginUser = new FrmLogin();
            Fg.CentrarForma(FrmLoginUser);

            FrmLoginUser.sQuery = sQuerySucursales;
            FrmLoginUser.sTabla = sTabla;
            FrmLoginUser.sArbol = sArbol;
            FrmLoginUser.dtsPermisos = dtsPermisos;
            FrmLoginUser.ShowDialog();
            
            bRegresa = FrmLoginUser.bUsuarioLogeado;
            ////DtGeneral.EmpresaConectada = FrmLoginUser.sEmpresa;
            ////DtGeneral.EstadoConectado = FrmLoginUser.sEstado;
            ////DtGeneral.FarmaciaConectada= FrmLoginUser.sSucursal;
            dtsPermisos = FrmLoginUser.dtsPermisos;

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

                sSql = string.Format("Select IdProveedor, LoginProv, Password, Status " +
                    " From Net_Proveedores (NoLock) " );

                dtsUsuarios = (DataSet)EjecutarQuery(sSql, "Usuarios");

                //if ( bEjecuto ) 
                //    bUsuariosCargados = true;
            }
        }

        private void ArbolNavegacion()
        {
            dtsPermisos = new DataSet();
            string sSql = "";

            if ( bEsAdminSys ) 
                sSql = string.Format(" Exec sp_Navegacion '{0}' ", sArbol);
            else
                sSql = string.Format(" Exec sp_Permisos '{0}', '{1}', '{2}', '{3}' ", sEstado, sSucursal, sArbol, sUsuario);

            // Los Proveedores levantan el Arbol Completo 
            sSql = string.Format(" Exec sp_Navegacion '{0}' ", sArbol);
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
                        bRegresa = false;
                    }
                    else
                    {
                        bEsAdminSys = true;
                        ////DtGeneral.IdPersonal = "0000";
                        ////DtGeneral.NombrePersonal = "ADMINISTRADOR";
                    }
                }
                else
                {
                    bRegresa = UsuarioValido();
                }

                if (bRegresa)
                {
                    ArbolNavegacion();
                    //////GetPermisosUsuarios();
                    ////DtGeneral.XmlEdoConfig.SetValues("UltimoUsuarioConectado", sUsuario);
                    ////DtGeneral.XmlEdoConfig.SetValues("IdEmpresa", sEmpresa);
                    ////DtGeneral.XmlEdoConfig.SetValues("IdEstado", sEstado);
                    ////DtGeneral.XmlEdoConfig.SetValues("IdSucursal", sSucursal);
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
                    myDr = dtsUsuarios.Tables["Usuarios"].Select(" LoginProv = '" + sUsuario + "'");

                    if (myDr.Length > 0)
                    {
                        sIdPersonal = myDr[0]["IdProveedor"].ToString();
                        sUser = myDr[0]["LoginProv"].ToString();
                        sPass = myDr[0]["Password"].ToString();
                        sStatus = myDr[0]["Status"].ToString().ToUpper();

                        // Tomar el Id del Proveedor 
                        GnProveedores.IdProveedor = sIdPersonal; 
                    }

                    if (sUser == "")
                    {
                        bRegresa = false;
                        errorLog.AgregarError("El usuario no esta dado de alta en el sistema.", "", "", "AutenticarUsuario()");
                    }

                    if ( bRegresa )
                    {
                        string sPassAux = crypto.PasswordEncriptar(sIdPersonal + sUser + sPassword.ToUpper());
                        if (sPass == "" || sPass != sPassAux)
                        {
                            bRegresa = false;
                            errorLog.AgregarError("La contraseña especificada para el inicio de sesión es inválida.", "", "", "AutenticarUsuario()");
                        }
                        else
                        {
                            ////DtGeneral.PasswordUsuario = sPassAux;
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
                myResult = error.MostrarVentanaError(true, false, errorLog.ListaErrores); 
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
            DataTable dtTabla = new DataTable(prtTabla); 
            Leer = new clsLeer(ref Conexion);

            clsDatosCliente datosCliente = new clsDatosCliente(GnProveedores.DatosApp, "Login", "LOGIN");
            clsLeerWeb Query = new clsLeerWeb(General.Url, datosCliente);

            bEjecuto = false;
            if (!Query.Exec(prtQuery))
            {
                General.Error.LogError(Query.Error.Message); 
                // General.Error.GrabarError(Leer.Error, General.DatosConexion, "", Application.ProductVersion, "Login", "Login", Leer.QueryEjecutado);
            }
            else
            {
                dtTabla.Merge(Query.DataTableClase);
                Query.DataTableClase = dtTabla; 
                dtsResultado = Query.DataSetClase;
                bEjecuto = true;
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
