using System;
using System.Collections.Generic;
using System.Text;

namespace Dll_ISIADISSEP.wsClases
{
    public class Vale
    {
        ////string iId = "0";
        ////string sGlosa = "";
        ////string sGlosaSubClase = "";
        ////int iExistencias = 0;
        ////int iRanking = 0;
        ////int iEAN = 0;

        string sSurtidoConVale = "";
        string sFolioVale = "";
        int iPiezas = 0;

        int iTab_00 = (int)Tabuladores.T00;
        int iTab_01 = (int)Tabuladores.T01;
        int iTab_02 = (int)Tabuladores.T02;
        int iTab_03 = (int)Tabuladores.T03;
        int iTab_04 = (int)Tabuladores.T04;
        int iTab_05 = (int)Tabuladores.T05;

        #region Propiedades Publicas
        public string SurtidoConVale 
        {
            get { return sSurtidoConVale; }
            set { sSurtidoConVale = value; }
        }

        public string FolioVale
        {
            get { return sFolioVale; }
            set { sFolioVale = value; }
        }

        public int Piezas
        {
            get { return iPiezas; }
            set { iPiezas = value; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        private string getTabs(int Tabs)
        {
            return GnDll_SII_SIADISSEP.getTabs(Tabs);
        }

        public string GetString()
        {
            string sRegresa = "";

            //sRegresa += string.Format("{0}<{1}>\n", getTabs(iTab_03), "vale");
            sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_03), "surteVale", sSurtidoConVale);
            sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_03), "codigoVale", sFolioVale);
            sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_03), "piezasVale", iPiezas.ToString("###########0"));
            //sRegresa += string.Format("{0}</{1}>\n", getTabs(iTab_03), "vale");

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Publicos
    }
}
