using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Globalization;

using SC_SolutionsSystem;

namespace DllFarmaciaSoft
{
    public class ConfiguracionRegional
    {
        static string sEsMX = "es-mx";

        public static bool Revisar()
        {
            bool bRegresa = true;
            string sCulture = Thread.CurrentThread.CurrentCulture.Name.ToLower();
            CultureInfo cultureInfo = new CultureInfo(sEsMX, true);

            if (sEsMX != sCulture)
            {
                //// Forzar la Region Español-Mexico 
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;

                Application.CurrentCulture = cultureInfo;
                Application.CurrentInputLanguage = InputLanguage.FromCulture(cultureInfo);


                //////General.msjAviso("La configuración regional del equipo no es la requerida por el Sistema, se cerrara la aplicación");
                ////////General.msjAviso("La configuración regional del equipo no es la requerida por el Sistema, se modificará la configuración.");

                ////////Thread.CurrentThread.CurrentCulture = new CultureInfo(sEsMX);
                ////////Thread.CurrentThread.CurrentUICulture = new CultureInfo(sEsMX);
                bRegresa = false;
                bRegresa = true;

                bRegresa = Thread.CurrentThread.CurrentCulture.Name.ToLower() == sEsMX;

                ////// Application.Exit(); 
            }

            return bRegresa;
        }
    }
}
