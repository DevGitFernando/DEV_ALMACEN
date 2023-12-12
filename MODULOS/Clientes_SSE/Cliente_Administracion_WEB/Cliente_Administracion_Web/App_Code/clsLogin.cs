using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.Odbc;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using System.IO;
using System.Collections;
using Microsoft.VisualBasic.FileIO;
using wsConexion;

    /// <summary>
    /// Clase encarga de validar los usuarios del sistema
    /// </summary>
    public static class clsLogin
    {
        #region Declaración de variables
     
        static DataSet dtsClase = new DataSet();

        static string sEmpresa = "000", sEstado = "00", sSucursal = "0001", sIdPersonal = "", sUsuario = "", sNombrePersonal, sPassword = "";
        static string sArbol = "";
        static string sLoginAdmin = "ADMIN";
        //static string sLoginAdmin = "ADMINISTRADOR";
        static bool bEsAdminSys = false;
        static DataSet dtsUsuarios;
        static DataSet dtsPermisos;
        static DataSet dtsArbol;

        static Cls_Acceso_a_Datos_Sql Datos = new Cls_Acceso_a_Datos_Sql();

        static clsConexionSQL Conexion;
        static clsLeer leerWeb;
        static bool bEjecuto = false;
        static clsCriptografo crypto = new clsCriptografo();

        static string sQuerySucursales = "";
        static string sTabla = "";

        static string sError = string.Empty;
        #endregion
                
        #region Propiedades public staticas

        public static DataSet Permisos
        {
            get { return dtsPermisos; }
            set { dtsPermisos = value; }
        }

        public static bool EsAdmin
        {
            get { return bEsAdminSys; }
        }

        public static string Arbol
        {
            get { return sArbol; }
            set { sArbol = value; }
        }

        public static string Empresa
        {
            get { return sEmpresa; }
            set { sEmpresa = value; }
        }

        public static string Estado
        {
            get { return sEstado; }
            set { sEstado = value.ToUpper(); }
        }

        public static string Sucursal
        {
            get { return sSucursal; }
            set { sSucursal = value.ToUpper(); }
        }

        public static string Personal
        {
            get { return sIdPersonal; }
            set { sIdPersonal = value.ToUpper(); }
        }

        public static string Usuario
        {
            get { return sUsuario; }
            set { sUsuario = value.ToUpper(); }
        }

        public static string NombrePersonal
        {
            get { return sNombrePersonal; }
            set { sNombrePersonal = value.ToUpper(); }
        }

        public static string Password
        {
            get { return sPassword; }
            set { sPassword = value.ToUpper(); }
        }

        public static string QuerySucursales
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

        public static string TablaSucursales
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
        public static string ErrorAutenticacion
        {
            get { return sError; }
        }
        #endregion

        #region Funciones y procedimientos public staticos
        public static bool AutenticarServicioSO()
        {
            bool bRegresa = false;
            return bRegresa;
        }

        public static bool AutenticarServicio()
        {
            bool bRegresa = false;
            return bRegresa;
        }

        public static bool AutenticarUsuario()
        {
            bool bRegresa = true;
            return bRegresa;
        }

        public static void CargarUsuarios()
        {
            Conexion = DtGeneral.DatosConexion;
            //Conexion.SetConnectionString();

            dtsUsuarios = new DataSet();
            
            //string sSql = string.Format("Select IdEstado, IdFarmacia as IdSucursal, IdUsuario as IdPersonal, Login as LoginUser, Password, Status " +
            //    " From Net_Regional_Usuarios (NoLock) Where IdEstado = '{0}' and IdFarmacia = '{1}' ", sEstado, sSucursal);

            string sSql = string.Format("Select IdEstado, IdFarmacia as IdSucursal, IdUsuario as IdPersonal, Login as LoginUser, Nombre, Password, Status " +
                " From Net_Regional_Usuarios (NoLock) Where IdEstado = '{0}' ", sEstado);
            
            dtsUsuarios = (DataSet)EjecutarQuery(sSql, "Usuarios");
            

        }

        public static void ArbolNavegacion()
        {
            dtsPermisos = new DataSet();
            string sSql = "";

            if (bEsAdminSys)
            {
                sSql = string.Format(" Exec sp_Navegacion '{0}' ", sArbol);
            }
            else
            {
                //sSql = string.Format(" Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", sEstado, sSucursal, sArbol, sUsuario);
                sSql = string.Format(" Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", sEstado, HttpContext.Current.Session["IdSucursal"].ToString(), sArbol, HttpContext.Current.Session["Personal"].ToString());
                //sSql = string.Format(" Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", sEstado, DtGeneral.Sucursal, sArbol, HttpContext.Current.Session["Personal"].ToString());
            }

            dtsPermisos = (DataSet)EjecutarQuery(sSql, "Arbol");
            DtGeneral.Permisos = dtsPermisos;
            HttpContext.Current.Session["Permisos"] = dtsPermisos;
        }

        public static bool AutenticarUsuarioLogin()
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
                        //Error
                        //errorLog.AgregarError("La contraseña especificada para el inicio de sesión es inválida.", "", "", "AutenticarUsuario()");
                        sError = "La contraseña especificada para el inicio de sesión es inválida.";
                        bRegresa = false;
                    }
                    else
                    {
                        bEsAdminSys = true;
                    }
                }
                else
                {
                    bRegresa = UsuarioValido();
                }

                if (bRegresa)
                {
                    ArbolNavegacion();
                }
            }

            return bRegresa;
        }
        #endregion

        #region Funciones y procedimientos privados
        public static bool UsuarioValido()
        {
            bool bRegresa = true;
            DataRow[] myDr;
            HttpContext.Current.Session["Personal"] = "";
            if (dtsUsuarios.Tables.Count > 0)
            {
                if (dtsUsuarios.Tables["Usuarios"].Rows.Count > 0)
                {
                    myDr = dtsUsuarios.Tables["Usuarios"].Select("1=0");
                    myDr = dtsUsuarios.Tables["Usuarios"].Select("IdEstado = '" + sEstado + "' and LoginUser = '" + sUsuario + "'");
                    
                    if (myDr.Length > 0)
                    {
                        HttpContext.Current.Session["IdPersonal"] = myDr[0]["IdPersonal"].ToString();
                        HttpContext.Current.Session["Personal"] = myDr[0]["LoginUser"].ToString();
                        HttpContext.Current.Session["NombrePersonal"] = myDr[0]["Nombre"].ToString();
                        HttpContext.Current.Session["Password"] = myDr[0]["Password"].ToString();
                        HttpContext.Current.Session["IdSucursal"] = myDr[0]["IdSucursal"].ToString();
                        HttpContext.Current.Session["StatusPersonal"] = myDr[0]["Status"].ToString();
                    }

                    if (HttpContext.Current.Session["Personal"].ToString() == "")
                    {
                        bRegresa = false;
                        sError = "El usuario no esta dado de alta en el sistema.";
                    }

                    if (bRegresa)
                    {
                        string sPassAux = ""; 
                        
                        sPassAux = crypto.PasswordEncriptar(sEstado + HttpContext.Current.Session["IdSucursal"].ToString() + sUsuario + sPassword);
                        if (HttpContext.Current.Session["Password"].ToString() == "" || HttpContext.Current.Session["Password"].ToString() != sPassAux)
                        {
                            bRegresa = false;
                            sError = "La contraseña especificada para el inicio de sesión es inválida.";
                        }
                        else
                        {
                            HttpContext.Current.Session["PasswordLogin"] = sPassword;
                            //DtGeneral.GetInfoUnidad();
                            //DtGeneral.GetJurisdicciones(DtGeneral.EstadoConectado);
                            ////Cargar Claves SSA Sales
                            //DtGeneral.GetClaves();
                        }

                    }

                    //if (bRegresa && sStatus != "A")
                    if (bRegresa && HttpContext.Current.Session["StatusPersonal"].ToString() != "A")
                    {
                        bRegresa = false;
                        //errorLog.AgregarError("El usuario no es válido en el sistema", "", "", "AutenticarUsuario()");
                        sError = "El usuario no es válido en el sistema";
                    }
                }
                else
                {
                    bRegresa = false;
                    //errorLog.AgregarError("No existen usuarios dados de alta en el sistema para esta sucursal.", "", "", "AutenticarUsuario()");
                    sError = "No existen usuarios dados de alta en el sistema para esta sucursal.";
                }
            }

            if (!bRegresa)
            {
                //error
            }

            return bRegresa;
        }

        public static bool ArbolDeNavegacion()
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

         public static DataSet EjecutarQuery(string prtQuery, string prtTabla)
        {
            DataSet dtsResultado = new DataSet();
            bEjecuto = false;
            leerWeb = new clsLeer(ref Conexion);

            if (!leerWeb.Exec(prtQuery))
            {
                //Error
            }
            else
            {
                leerWeb.RenombrarTabla(1, prtTabla);
                dtsResultado = leerWeb.DataSetClase;
                bEjecuto = true;
            }

            return dtsResultado;
        }

         public static object EjecutarQuery(string prtQuery, string prtTabla, string Nada)
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
            }

            return objRetorno;
        }

         public static bool ExistenDatosEnDataset(DataSet dtsRevisar)
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