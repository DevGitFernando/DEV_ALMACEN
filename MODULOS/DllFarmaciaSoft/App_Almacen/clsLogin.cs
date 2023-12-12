using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllFarmaciaSoft.wsFarmacia;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace DllFarmaciaSoft.App_Almacen
{
    public class clsLogin
    {

        private string sLoginAdmin = "ADMINISTRADOR";

        private clsErrorManager error = new clsErrorManager();
        private clsLogError errorLog = new clsLogError();

        private DataSet dtsUsuarios;
        private bool bEsAdminSys = false;
        private clsConexionSQL Conexion;
        wsCnnCliente cnnCte = new wsCnnCliente();
        private clsLeer leer;
        private clsLeer leerSesion = new clsLeer();
        private clsCriptografo crypto = new clsCriptografo();
        private string sEmpresa = "000", sEstado = "00", sSucursal = "0001", sIdPersonal = "", sUsuario = "", sPassword = "";
        private string sTest = "";
        
        //Inicio Sesión
        public string AutenticarUsuarioLogin(string sEmpresas, string sEstados, string sSucursals, string sUsuarios, string sPasswords)
        {
            string mySucursal = sSucursal;
            errorLog = new clsLogError();
            string datoP = string.Empty;
            DataSet dt = new DataSet();

            sEmpresa = sEmpresas;
            sEstado = sEstados;
            sSucursal = sSucursals;
            sUsuario = sUsuarios;
            sPassword = sPasswords;

            CargarUsuarios(sEstado,sSucursal);

                // Verificar SA
                bEsAdminSys = false;
                if (sLoginAdmin == sUsuario.ToUpper())
                {
                    if (sPassword.Trim().ToUpper() != General.DatosConexion.Password.ToUpper())
                    {
                        errorLog.AgregarError("La contraseña especificada para el inicio de sesión es inválida.", "", "", "AutenticarUsuario()");
                        error = new clsErrorManager(errorLog.ListaErrores);

                        //if (!bCanceladoPorUsuario)
                        //{
                        //    myResult = error.MostrarVentanaError(true, false, errorLog.ListaErrores);
                        //}

                        //bRegresa = false;
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
                    datoP  = UsuarioValido();
                }

            return datoP;
        }
        private void CargarUsuarios(string sEstado, string sSucursal)
        {
            //if (!bUsuariosCargados)
            //{
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
                    " Where N.IdEstado = '{0}' and N.IdSucursal = '{1}' and N.LoginUser = '{2}' ", sEstado, sSucursal, sUsuario);

                //dtsUsuarios = (DataSet)EjecutarQuery(sSql, "Usuarios");
                dtsUsuarios = (DataSet)EjecutarQuery(sSql, "Usuarios");
                //if ( bEjecuto ) 
                //    bUsuariosCargados = true;
            //}
        }

        private string UsuarioValido()
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
                        sIdPersonal = "";
                        errorLog.AgregarError("El usuario no esta dado de alta en el sistema.", "", "", "AutenticarUsuario()");
                    }

                    if (bRegresa)
                    {
                        sPass_Read = crypto.PasswordDesencriptar(sPass).Substring(10);
                        sPassAux = crypto.PasswordEncriptar(sEstado + sSucursal + sIdPersonal + sPassword.ToUpper());

                        if (sPass == "" || sPass != sPassAux)
                        {
                            bRegresa = false;
                            sIdPersonal = "";
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
                        sIdPersonal = "";
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
            }

            return sIdPersonal;
        }

        //Carga inicial
        public DataSet ObtenerDatosIniciarSesion()
        {
            bool bRegresa = false;
            //clsLeerWeb Query; // = new clsLeerWeb(General.Url, General.DatosConexion, new SC_SolutionsSystem.Data.clsDatosCliente("DllFarmaciaSoft", this.Name, "FrmEdoLogin_Load"));
            //clsDatosCliente datosCliente;
            clsLeer Query;

            string sSql = "";
            string sFiltroAlmacen = " and F.EsAlmacen = 1 "; // Solo farmacias  
            string sFiltroUnidosis = " and F.EsUnidosis = 0 ";
            DataSet dtsDatosDeSesion = new DataSet();
            DataSet dtsAux = new DataSet();

            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            Query = new clsLeer(ref cnn);

            try
            {
                    //datosCliente = new clsDatosCliente(DtGeneral.DatosApp, "", "ObtenerDatosParaIniciarSesion");
                    //Query = new clsLeerWeb(General.Url, General.DatosConexion, datosCliente);
                    //General.msjUser(Query.DatosConexion.Servidor + " " + Query.DatosConexion.BaseDeDatos + " " + Query.DatosConexion.Usuario + " " + Query.DatosConexion.Password);

                    try
                    {
                        sSql = "Select IdEmpresa, Nombre, NombreCorto, EsDeConsignacion, RFC, EdoCiudad, Colonia, Domicilio, CodigoPostal, Status " +
                               "From vw_CFG_Empresas (NoLock) Where Status = 'A' Order by IdEmpresa ";

                        if (!Query.Exec(sSql))
                        {
                            General.Error.GrabarError("Error en el vw_CFG_Empresas " + Query.Error, General.DatosConexion, "", Application.ProductVersion, "Login", "Login", Query.QueryEjecutado);
                        }
                        else
                        {
                            Query.RenombrarTabla(1, "Empresas");
                            dtsDatosDeSesion.Tables.Add(Query.DataTableClase);

                            sSql = "Select IdEstado, NombreEstado, ClaveRenapo, IdEmpresa " +
                                   "From vw_EmpresasEstados (NoLock) " +
                                   "Where StatusEdo = 'A'  And StatusEdo = 'A' And StatusRelacion = 'A' " +
                                   "Order by IdEstado ";  // ACTIVAR  

                            if (!Query.Exec(sSql))
                            {
                                General.Error.GrabarError("Error en el Query vw_EmpresasEstados " +  Query.Error, General.DatosConexion, "", Application.ProductVersion, "Login", "Login", Query.QueryEjecutado);
                            }
                            else
                            {
                                Query.RenombrarTabla(1, "Estados");
                                dtsDatosDeSesion.Tables.Add(Query.DataTableClase);

                                //sSql = "Select F.IdFarmacia, F.Farmacia, (F.IdFarmacia + ' - ' + F.Farmacia) as NombreFarmacia, F.IdJurisdiccion, F.Jurisdiccion, " +
                                //    " F.IdEstado, F.IdEmpresa, F.ManejaVtaPubGral, F.ManejaControlados, F.EsAlmacen, F.EsUnidosis " +
                                //    " From vw_EmpresasFarmacias F (NoLock) " +
                                //    " Where StatusRelacion  = 'A' " +
                                //    " Order by IdEstado, IdFarmacia ";

                                //////// Habilitar solo las Unidades requeridas en el Servidor. 
                                //if (DtGeneral.SoloMostrarUnidadesConfiguradas)
                                //{

                                    sSql = string.Format("Select Distinct F.IdFarmacia, F.Farmacia, (F.IdFarmacia + ' - ' + F.Farmacia) as NombreFarmacia, F.IdJurisdiccion, F.Jurisdiccion, " +
                                        " F.IdEstado, F.IdEmpresa, F.ManejaVtaPubGral, F.ManejaControlados, F.EsAlmacen, F.EsUnidosis " +
                                        " From vw_EmpresasFarmacias F (NoLock) " +
                                        " Inner Join CFG_Svr_UnidadesRegistradas L (NoLock) On ( F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and L.Status = 'A' ) " +
                                        " Where F.Status = 'A' {0}  {1} " +
                                        " Order by F.IdEstado, F.IdFarmacia ", sFiltroAlmacen, sFiltroUnidosis);
                                //}


                                if (!Query.Exec(sSql))
                                {
                                    General.Error.GrabarError("Error en el query último " +  Query.Error, General.DatosConexion, "", Application.ProductVersion, "Login", "Login", Query.QueryEjecutado);
                                }
                                else
                                {
                                    Query.RenombrarTabla(1, "Farmacias");
                                    dtsDatosDeSesion.Tables.Add(Query.DataTableClase);

                                    bRegresa = true; // si no llega a este punto falla toda la funcion 
                                }
                            }
                        }
                    }
                    catch
                    {
                        General.Error.GrabarError("Error en el Query 1" + Query.Error, General.DatosConexion, "", Application.ProductVersion, "Login", "Login", Query.QueryEjecutado);
                    }
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                General.Error.GrabarError("Error de inicio " + General.DatosConexion.CadenaConexion , General.DatosConexion, "", Application.ProductVersion, "Login", "Login", "");
            }

            if(dtsDatosDeSesion.Tables.Count > 0)
            {
                leerSesion.DataSetClase = dtsDatosDeSesion;
            }
            
            return leerSesion.DataSetClase;
        }

        private DataSet EjecutarQuery(string prtQuery, string prtTabla)
        {
            DataSet dtsResultado = new DataSet();
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
            leer = new clsLeer(ref cnn);

                if (leer.Exec(prtTabla,prtQuery))
                {
                    if (leer.Leer())
                    {
                        dtsResultado = leer.DataSetClase;
                    }
                    else
                    {
                        sTest = "Ejecutó pero no encontró datos";
                    }
                }
                else
                {
                    General.Error.GrabarError(leer.Error, General.DatosConexion, "", Application.ProductVersion, "Login", "Login", leer.QueryEjecutado);
                }

            return dtsResultado;
        }
    }
}
