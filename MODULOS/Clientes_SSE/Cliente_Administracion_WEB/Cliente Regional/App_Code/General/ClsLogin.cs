using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using Cliente_Regional.Model;

namespace Cliente_Regional.Code
{
    /// <summary>
    /// Descripción breve de ClsLogin
    /// </summary>
    public static class ClsLogin
    {
        static ClsLogin()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        #region Declaración de variables
        static clsLeer myLeer = new clsLeer();
        static string sError = string.Empty;
        static clsCriptografo crypto = new clsCriptografo();
        static basGenerales Fg = new basGenerales();
        #endregion Declaración de variables


        #region Funciones Públicas
        public static bool AutenticarUsuarioLogin(string sUsuario, string sPassword)
        {
            bool bUsuarioValido = true;
            //if (sUsuario == DtGeneral.UsuarioAdministrador)
            //{
            //    //Agregar datos para el administrador
            //    if (sPassword.Trim().ToUpper() == General.DatosConexion.Password)
            //    {
            //        //Correcto
            //    }
            //    else
            //    {
            //        bUsuarioValido = false;
            //    }

            //}
            //else
            {
                myLeer.DataSetClase = CargarUsuario(sUsuario);

                if (myLeer.Leer())
                {
                    if (myLeer.Campo("LoginUser") == "" && myLeer.Campo("Password") == "")
                    {
                        bUsuarioValido = false;
                        sError = "El Usuario no tiene asignado un Usuario y Contraseña";
                    }
                    else
                    {
                        string sPasswordEncriptado = crypto.PasswordEncriptar(DtGeneral.IdEstado + myLeer.Campo("IdSucursal") + sUsuario + sPassword);
                        bUsuarioValido = UsuarioValido(sUsuario, sPasswordEncriptado);
                    }
                }
                else
                {
                    bUsuarioValido = false;
                    sError = "No existen usuarios dados de alta en el sistema para esta unidad.";
                }

            }
            return bUsuarioValido;
        }


        #endregion Funciones Públicas

        #region Funciones privadas
        private static DataSet CargarUsuario(string sUsuario)
        {
            return ClsConsultas.InformacionUsuario(sUsuario);
        }

        private static bool UsuarioValido(string sUsuario, string sPasswordEncriptado)
        {
            bool bUsuarioValido = true;
            string sStatus = myLeer.Campo("Status");
            string sPassword = myLeer.Campo("Password");

            if (sUsuario == "")
            {
                bUsuarioValido = false;
                sError = "El usuario no esta dado de alta en el sistema.";
            }

            if (bUsuarioValido)
            {
                if (sPassword == "" || sPassword != sPasswordEncriptado)
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
                    HttpContext.Current.Response.Cookies[DtGeneral.NombreCookie].Expires = DateTime.Now.AddDays(-1);
                }

                HttpCookie MyCookie = new HttpCookie(DtGeneral.NombreCookie)
                {
                    Expires = now.AddHours(DtGeneral.TiempoSesion)
                };

                ClsDictionary myDictionary = new ClsDictionary();
                myDictionary.Add("IdSucursal", myLeer.Campo("IdSucursal"));
                myDictionary.Add("IdPersonal", myLeer.Campo("IdPersonal"));
                myDictionary.Add("LoginUser", sUsuario);
                myDictionary.Add("NombrePersonal", myLeer.Campo("Nombre"));
                myDictionary.Add("Session", MyCookie.Expires.ToString("yyyy-MM-dd HH:mm:ss"));
                myDictionary.Add("Autenticado", (true).ToString());

                MyCookie.Values["value"] = myDictionary.Cifrar();
                HttpContext.Current.Response.SetCookie(MyCookie);

                HttpContext.Current.Session["User"] = new ApplicationUser
                {
                    IdSucursal = myDictionary.Search("IdSucursal"),
                    IdPersonal = myDictionary.Search("IdPersonal"),
                    LoginUser = myDictionary.Search("LoginUser"),
                    NombrePersonal = myDictionary.Search("NombrePersonal"),
                    Session = myDictionary.Search("Session"),
                    Autenticado = true
                };
            }

            return bUsuarioValido;
        }
        #endregion Funciones privadas
    }
}