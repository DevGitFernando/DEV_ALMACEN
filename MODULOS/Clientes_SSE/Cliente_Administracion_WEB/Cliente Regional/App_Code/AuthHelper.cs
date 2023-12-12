using System;
using System.Web;
using Cliente_Regional.Code;


namespace Cliente_Regional.Model {
    public class ApplicationUser {
         public string IdSucursal { get; set; }
        public string IdPersonal { get; set; }
        public string LoginUser { get; set; }
        public string NombrePersonal { get; set; }
        public string Session { get; set; }
        public bool Autenticado { get; set; }
    }

    public static class AuthHelper {
        public static bool SignIn(string userName, string password) {
            //HttpContext.Current.Session["User"] = CreateDefualtUser();  // Mock user data
            //return true;
            return ClsLogin.AutenticarUsuarioLogin(userName.ToUpper(), password.ToUpper());
        }
        public static void SignOut() {
            HttpContext.Current.Session["User"] = null;
            HttpContext.Current.Response.Cookies[DtGeneral.NombreCookie].Expires = DateTime.Now.AddDays(-1);
        }
        public static bool IsAuthenticated() {
            //return GetLoggedInUserInfo() != null;
            HttpCookie myCookie = DtGeneral.ObtenerCookie();
            return myCookie != null;
        }

        public static ApplicationUser GetLoggedInUserInfo() {
            return HttpContext.Current.Session["User"] as ApplicationUser;
        }        
    }
}