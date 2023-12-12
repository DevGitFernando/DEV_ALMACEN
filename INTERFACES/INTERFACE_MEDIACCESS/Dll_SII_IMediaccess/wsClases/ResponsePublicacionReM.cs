using System;
using System.Collections.Generic;
using System.Text;

namespace Dll_SII_IMediaccess.wsClases
{
    public class ResponsePublicacionReM
    {
        int iEstatus = 0;
        string sError = "";

        public ResponsePublicacionReM()
        {
        }

        #region Propiedades Publicas
        public int Estatus
        {
            get { return iEstatus; }
            set { iEstatus = value; }
        }

        public string Error
        {
            get { return sError; }
            set { sError = value; }
        }
        #endregion Propiedades Publicas 

        #region Funciones y Procedimientos Publicos
        public string GetString()
        {
            string sRegresa = "";

            sRegresa += string.Format("<{0}>{1}</{0}>", "Estatus", iEstatus);
            sRegresa += string.Format("<{0}>{1}</{0}>", "Error", sError);

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Publicos 
    }
}
