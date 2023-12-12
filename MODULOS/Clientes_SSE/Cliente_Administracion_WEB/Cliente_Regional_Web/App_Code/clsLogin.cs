using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

/// <summary>
/// Descripción breve de clsLogin
/// </summary>
public static class clsLogin
{
    static clsLogin()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    #region Declaración de variables

    static DataSet dtsClase = new DataSet();

    static string sError = string.Empty;

    static clsLeer myLeer = new clsLeer();
    static clsCriptografo crypto = new clsCriptografo();

    #endregion Declaración de variables


    #region Funciones Públicas
    public static bool AutenticarUsuarioLogin(string sUsuario, string sPassword)
    {
        bool bUsuarioValido = true;
        myLeer.DataSetClase = CargarUsuarios(sUsuario);

        if (myLeer.Leer())
        {
            //string sPasswordEncriptado = crypto.PasswordEncriptar(DtGeneral.IdEstado + DtGeneral.IdSucursal + sUsuario + sPassword);
            string sPasswordEncriptado = crypto.PasswordEncriptar(DtGeneral.IdEstado + myLeer.Campo("IdSucursal") + sUsuario + sPassword);
            //string stmpPasswordEncriptado = crypto.PasswordEncriptar(DtGeneral.IdEstado + DtGeneral.IdSucursal + sUsuario + sPassword);
            bUsuarioValido = UsuarioValido(sPasswordEncriptado);
        }
        else
        {
            bUsuarioValido = false;
            sError = "No existen usuarios dados de alta en el sistema para esta unidad.";
        }

        return bUsuarioValido;
    }
    #endregion Funciones Públicas

    #region Funciones privadas
    private static DataSet CargarUsuarios(string sUsuario)
    {
        DataSet dtsUsuarios = new DataSet("dtsUsuarios");
        if (DtGeneral.DatosConexion == null)
        {
            DtGeneral.Init();
        }

        string sSql = string.Format("Select " +
                                        "IdEstado, IdFarmacia as IdSucursal, IdUsuario as IdPersonal, Login as LoginUser, Nombre, Password, Status " +
                                    "From Net_Regional_Usuarios (NoLock) " +
                                    //"Where IdEstado = '{0}' And IdFarmacia = '{1}' And Login = '{2}' ", DtGeneral.IdEstado, DtGeneral.IdSucursal, sUsuario);
                                    "Where IdEstado = '{0}' And Login = '{1}' ", DtGeneral.IdEstado, sUsuario);
        
        dtsUsuarios = DtGeneral.ExecQuery(sSql, "Usuarios");

        return dtsUsuarios.Copy();
    }

    private static bool UsuarioValido(string sPasswordEncriptado)
    {
        bool bUsuarioValido = true;
        string sUser = "", sPass = "", sStatus = "";
            
        
        sUser = myLeer.Campo("LoginUser");
        sPass = myLeer.Campo("Password");
        sStatus = myLeer.Campo("Status");

        if (sUser == "")
        {
            bUsuarioValido = false;
            sError = "El usuario no esta dado de alta en el sistema.";
        }

        if (bUsuarioValido)
        {
            if (sPass == "" || sPass != sPasswordEncriptado)
            {
                bUsuarioValido = false;
                sError = "La contraseña especificada para el inicio de sesión es inválida.";
            }
        }

        if (bUsuarioValido && sStatus != "A")
        {
            bUsuarioValido = false;
            sError = "El usuario no es válido en el sistema";
        }

        if (bUsuarioValido)
        {
            DateTime now = DateTime.Now;

            if (HttpContext.Current.Request.Cookies[DtGeneral.NombreCookie] != null)
            {
                HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies[DtGeneral.NombreCookie];
                HttpContext.Current.Response.Cookies.Remove(DtGeneral.NombreCookie);
                currentUserCookie.Expires = now.AddDays(-1);
                HttpContext.Current.Response.SetCookie(currentUserCookie);
            }

            HttpCookie MyCookie = new HttpCookie(DtGeneral.NombreCookie)
            {
                Expires = now.AddHours(DtGeneral.TiempoSesion)
            };

            MyCookie.Expires = now.AddHours(DtGeneral.TiempoSesion);
            ClsDictionary myDictionary = new ClsDictionary();
            myDictionary.Add("IdSucursal", myLeer.Campo("IdSucursal"));
            myDictionary.Add("IdPersonal", myLeer.Campo("IdPersonal"));
            myDictionary.Add("LoginUser", sUser);
            myDictionary.Add("NombrePersonal", myLeer.Campo("Nombre"));
            myDictionary.Add("Session", MyCookie.Expires.ToString("yyyy-MM-dd HH:mm:ss"));
            myDictionary.Add("SesionActual", "");
            myDictionary.Add("Autenticado", (true).ToString());

            MyCookie.Values["Value"] = myDictionary.Cifrar();
            HttpContext.Current.Response.SetCookie(MyCookie);
        }

        return bUsuarioValido;
    }
    #endregion Fuciones privadas

    #region Propiedades
    public static string ErrorAutenticacion
    {
        get { return sError; }
    }
    #endregion Propiedades
}