using System;
using System.Collections.Generic;
using System.Text;

namespace Dll_SII_IMediaccess.wsClases
{
    public class Producto
    {
        string iId = "0";
        string sGlosa = "";
        string sGlosaSubClase = "";
        int iExistencias = 0;
        int iRanking = 0;
        string sEAN = "";

        #region Propiedades Publicas 
        public string Id
        {
            get { return iId; }
            set { iId = value; }
        }

        public string Glosa
        {
            get { return sGlosa; }
            set { sGlosa = value; }
        }

        public string GlosaSubClase
        {
            get { return sGlosaSubClase; }
            set { sGlosaSubClase = value; }
        }

        public int Existencias
        {
            get { return iExistencias; }
            set { iExistencias = value; }
        }

        public int Ranking
        {
            get { return iRanking; }
            set { iRanking = value; }
        }

        public string EAN
        {
            get { return sEAN; }
            set { sEAN = value; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos 
        public string GetString()
        {
            string sRegresa = "";

            sRegresa += string.Format("<{0}>", "Producto");

            sRegresa += string.Format("<{0}>{1}</{0}>", "Id", iId);
            sRegresa += string.Format("<{0}>{1}</{0}>", "Glosa", sGlosa);
            sRegresa += string.Format("<{0}>{1}</{0}>", "Glosa_SubClase", sGlosaSubClase);
            sRegresa += string.Format("<{0}>{1}</{0}>", "Existencias", iExistencias);
            sRegresa += string.Format("<{0}>{1}</{0}>", "Ranking", iRanking);
            sRegresa += string.Format("<{0}>{1}</{0}>", "EAN", sEAN);
            
            sRegresa += string.Format("</Producto>");

            return sRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos
    }
}
