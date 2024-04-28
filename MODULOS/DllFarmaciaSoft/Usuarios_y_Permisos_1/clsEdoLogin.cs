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
using SC_SolutionsSystem.Idiomas;

////using Dll_IMach4;
using DllFarmaciaSoft; 

namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    /// <summary>
    /// Clase encarga de validar los usuarios del sistema
    /// </summary>
    public class clsEdoLogin
    {
        #region Declaración de variables
        FrmEdoLogin FrmLoginUser;
        FrmEdoConect Conect;
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
        private bool bXmlEnDirectorioApp = false; 


        private string sQuerySucursales = "";
        private string sTabla = "";
        // private bool bUsuariosCargados = false;

        private bool bCanceladoPorUsuario = false;


        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoIniManager Ini; // = new clsIniManager();
        FrmSeleccionarConexion seleccionarConexion;

        #endregion

        #region Constructores de clase y destructor 
        public clsEdoLogin():this("", "") 
        { 
        }

        public clsEdoLogin(string sQuery, string sTablaQuery)
        {
            sQuerySucursales = sQuery;
            sTabla = sTablaQuery;

            Idiomas.LeerXmlFromString(DllFarmaciaSoft.Properties.Resources.Lenguajes);  

            Idiomas.GetItem(1, ""); 
        }

        ~clsEdoLogin()
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
            set
            { 
                sArbol = value;
                DtGeneral.ArbolModulo = sArbol; 
            }
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

        public bool XmlEnDirectorioApp
        {
            get { return bXmlEnDirectorioApp; }
            set { bXmlEnDirectorioApp = value; }
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
            Conect = new FrmEdoConect(true);

            Conect.XmlEnDirectorioApp = bXmlEnDirectorioApp; 
            Conect.ConectarServicioSO(); 
            if (Conect.bExisteFileConfig)
            {
                if (Conect.bConexionEstablecida)
                {
                    bRegresa = true;
                    ////DtGeneral.XmlEdoConfig.SetValues("UltimoUsuarioConectado", "ADMINISTRADOR"); 
                }
            }

            return bRegresa;
        }

        public bool AutenticarServicio()
        {
            bool bRegresa = false;
            Conect = new FrmEdoConect(true);
            Fg.CentrarForma(Conect);
            Conect.ShowDialog();

            if (Conect.bExisteFileConfig)
            {
                if (Conect.bConexionEstablecida)
                {
                    bRegresa = true;
                    ////DtGeneral.XmlEdoConfig.SetValues("UltimoUsuarioConectado", "ADMINISTRADOR"); 
                }
            }

            return bRegresa;
        }

        public bool AutenticarUsuario()
        {
            bool bRegresa = false; //, bValidando = true;
            int iItemSeleccionado = 1;
            string sAlias = "";

            clsLeer leerIni = new clsLeer();
            clsLeer leerIniSeleccion = new clsLeer(); 
            bool bConexionSeleccionada = false;

            try
            {
                Ini = new clsEdoIniManager();
                Ini.XmlEnDirectorioApp = bXmlEnDirectorioApp;
                if (Ini.ExisteArchivo())
                {
                    leerIni.ReadXml(Ini.XmlFile); 
                    if (!leerIni.Leer())
                    {
                        General.msjError("El archivo de Conexión no contiene información, verifique.");
                    }
                    else
                    {
                        sAlias = Ini.GetValues("Alias", "Default");
                        bConexionSeleccionada = true; 
                        if (leerIni.Registros > 1)
                        {
                            seleccionarConexion = new FrmSeleccionarConexion(Ini.XmlFile);
                            seleccionarConexion.ShowDialog();

                            bConexionSeleccionada = seleccionarConexion.NodoSeleccionado;
                            iItemSeleccionado = seleccionarConexion.NumNodoSeleccionado;
                        }
                    }
                }
            }
            catch 
            { 
            }

            if (bConexionSeleccionada)
            {
                FrmLoginUser = new FrmEdoLogin(iItemSeleccionado);
                Fg.CentrarForma(FrmLoginUser);

                FrmLoginUser.sQuery = sQuerySucursales;
                FrmLoginUser.sTabla = sTabla;
                FrmLoginUser.sArbol = sArbol;
                FrmLoginUser.dtsPermisos = dtsPermisos;
                FrmLoginUser.ShowDialog();

                bRegresa = FrmLoginUser.bUsuarioLogeado;
                DtGeneral.EmpresaConectada = FrmLoginUser.sEmpresa;
                DtGeneral.EstadoConectado = FrmLoginUser.sEstado;
                DtGeneral.FarmaciaConectada = FrmLoginUser.sSucursal;
                dtsPermisos = FrmLoginUser.dtsPermisos;
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

                sSql = string.Format("Select Distinct N.IdEstado, N.IdSucursal, N.IdPersonal, N.LoginUser, N.Password, " +
                    " (case when (N.Status = 'A' and P.Status = 'A') Then 'A' Else 'A' End) as Status, " +
                    " N.Status as StatusUsuario, P.Status as StatusPersonal " + 
                    " From Net_Usuarios N (NoLock) " + 
                    " Inner Join CatPersonal P (NoLock) On ( N.IdEstado = P.IdEstado and N.IdSucursal = P.IdFarmacia )" + 
                    " Where N.IdEstado = '{0}' and N.IdSucursal = '{1}' ", sEstado, sSucursal); 

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
                sSql = string.Format(" Exec sp_Permisos '{0}', '{1}', '{2}', '{3}' ", sEstado, sSucursal, sArbol, sUsuario);
            }

            dtsPermisos = (DataSet)EjecutarQuery(sSql, "Arbol");
        }

        public bool AutenticarUsuarioLogin()
        {
            bool bRegresa = true;
            string mySucursal = sSucursal;
            errorLog = new clsLogError(); 

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
                        errorLog.AgregarError("La contraseña especificada para el inicio de sesión es inválida.", "", "", "AutenticarUsuario()");
                        error = new clsErrorManager(errorLog.ListaErrores);

                        if (!bCanceladoPorUsuario)
                        {
                            myResult = error.MostrarVentanaError(true, false, errorLog.ListaErrores);
                        }
                        
                        bRegresa = false;
                    }
                    else
                    {
                        bEsAdminSys = true;
                        DtGeneral.EsAdministrador = true; 
                        DtGeneral.IdPersonal = "0000";
                        DtGeneral.NombrePersonal = "ADMINISTRADOR";
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
                    DtGeneral.XmlEdoConfig.SetValues("UltimoUsuarioConectado", sUsuario);
                    DtGeneral.XmlEdoConfig.SetValues("IdEmpresa", sEmpresa);
                    DtGeneral.XmlEdoConfig.SetValues("IdEstado", sEstado);
                    DtGeneral.XmlEdoConfig.SetValues("IdSucursal", sSucursal);
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
            string sUser = "";
            string sPass = "";
            string sPass_Read = "";
            string sPassAux = ""; 
            string sStatus = ""; 

            errorLog = new clsLogError();

            if (dtsUsuarios.Tables.Count > 0)
            {
                if (dtsUsuarios.Tables["Usuarios"].Rows.Count > 0)
                {
                    myDr = dtsUsuarios.Tables["Usuarios"].Select("IdEstado = '" + sEstado + "' and IdSucursal = '" + sSucursal + "' and LoginUser = '" + sUsuario + "'");

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
                        try
                        {
                            sPass_Read = crypto.PasswordDesencriptar(sPass).Substring(10);
                        }
                        catch { }

                        sPassAux = crypto.PasswordEncriptar(sEstado + sSucursal + sIdPersonal + sPassword.ToUpper());

                        //// Equipos de desarrollo ingresan suplantando la contraseña del usuario logeado 
                        if ( DtGeneral.EsEquipoDeDesarrollo )
                        {
                            sPassAux = sPass;
                        }

                        if (sPass == "" || sPass != sPassAux)
                        {
                            bRegresa = false;

                            if (DtGeneral.ModuloEnEjecucion == TipoModulo.AlmacenUnidosis || DtGeneral.ModuloEnEjecucion == TipoModulo.FarmaciaUnidosis ||
                                DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen || DtGeneral.ModuloEnEjecucion == TipoModulo.Farmacia)
                            {
                                if (DtGeneral.UsuarioConectando == "")
                                {
                                    DtGeneral.IntentosPassword += 1;
                                    DtGeneral.UsuarioConectando = sUser; 
                                }
                                else
                                {
                                    if (DtGeneral.UsuarioConectando == sUser)
                                    {
                                        DtGeneral.IntentosPassword += 1;
                                    }
                                    else
                                    {
                                        DtGeneral.IntentosPassword = 1;
                                        DtGeneral.UsuarioConectando = sUser;
                                    }
                                }
                            }
                            errorLog.AgregarError("La contraseña especificada para el inicio de sesión es inválida.", "", "", "AutenticarUsuario()");
                        }
                        else
                        {
                            DtGeneral.PasswordUsuario = sPassAux;
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
            if (!bCanceladoPorUsuario)
            {
                if (!Leer.Exec(prtTabla, prtQuery))
                {
                    if (!bCanceladoPorUsuario)
                    {
                        General.Error.GrabarError(Leer.Error, General.DatosConexion, "", Application.ProductVersion, "Login", "Login", Leer.QueryEjecutado);
                    }
                }
                else
                {
                    dtsResultado = Leer.DataSetClase;
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
