using System;
using System.Collections.Generic;
using System.Text;

namespace Dll_IRE_AMPM.wsClases
{
    public class SurteOtraUnidad
    {
        string sSurteOtraUnidad = "";
        string sCodigoUnidad = "";

        int iTab_00 = (int)Tabuladores.T00;
        int iTab_01 = (int)Tabuladores.T01;
        int iTab_02 = (int)Tabuladores.T02;
        int iTab_03 = (int)Tabuladores.T03;
        int iTab_04 = (int)Tabuladores.T04;
        int iTab_05 = (int)Tabuladores.T05;

        List<string> sListaMedicamentos = new List<string>(); 

        #region Propiedades Publicas
        public string SurteEnOtraUnidad 
        {
            get { return sSurteOtraUnidad; }
            set { sSurteOtraUnidad = value; }
        }

        public string CodigoUnidad
        {
            get { return sCodigoUnidad; }
            set { sCodigoUnidad = value; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        public void AddMedicamento(string Medicamento)
        {
            sListaMedicamentos.Add(Medicamento);
        }

        private string getTabs(int Tabs)
        {
            return GnDll_SII_AMPM.getTabs(Tabs);
        }

        public string GetString()
        {
            string sRegresa = "";

            ////sRegresa += string.Format("\t<{0}>\n", "otraUnidad");
            ////sRegresa += string.Format("\t\t<{0}>{1}</{0}>\n", "surteOtraUnidad", sSurteOtraUnidad);
            ////sRegresa += string.Format("\t\t<{0}>{1}</{0}>\n", "codigoUnidad", sCodigoUnidad);

            sRegresa += string.Format("{0}<{1}>\n", getTabs(iTab_03), "otraUnidad");
            sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_04), "surteOtraUnidad", sSurteOtraUnidad);
            sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_04), "codigoUnidad", sCodigoUnidad);

            sRegresa += string.Format("{0}<{1}>\n", getTabs(iTab_04), "medicamentosSurtidos");
            if (sListaMedicamentos != null)
            {
                if (sListaMedicamentos.Count > 0)
                {
                    foreach (string sMedicamento in sListaMedicamentos)
                    {
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_05), "claveMedicamento", sMedicamento);
                        //sRegresa += string.Format("\t\t\t<{0}>{1}</{0}>\n", "claveMedicamento", sMedicamento);
                    }
                }
            }
            sRegresa += string.Format("{0}</{1}>\n", getTabs(iTab_04), "medicamentosSurtidos");
            
            sRegresa += string.Format("{0}</{1}>\n", getTabs(iTab_03), "otraUnidad");
            return sRegresa;
        }
        #endregion Funciones y Procedimientos Publicos
    }
}
