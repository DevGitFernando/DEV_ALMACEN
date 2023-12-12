using System;
using System.Collections.Generic;
using System.Text;

namespace Dll_IRE_AMPM.wsClases
{
    public class Medicamento
    {
        public enum TipoDeMedicamento
        {
            NoEspecificado = 0, 
            MedicamentoSurtido = 1,
            MedicamentoVale = 2,
            Colectivo_Medicamento = 11,
            Colectivo_Solucion = 12
        }

        string sClaveLote = "";  
		string sCaducidad = "";
        string sClaveSSA ="";
        string sDecripcionSal = "";
        int iCantidadRecetada = 0;
        int iCantidadSurtida = 0;
        Vale valeMedicamento = new Vale();
        SurteOtraUnidad surtidoEnOtraUnidad = new SurteOtraUnidad();

        List<Medicamento> listaMedicamentos = new List<Medicamento>();
        TipoDeMedicamento tpTipoDeMedicamento = TipoDeMedicamento.NoEspecificado; 

        int iTab_00 = (int)Tabuladores.T00;
        int iTab_01 = (int)Tabuladores.T01;
        int iTab_02 = (int)Tabuladores.T02;
        int iTab_03 = (int)Tabuladores.T03;
        int iTab_04 = (int)Tabuladores.T04;
        int iTab_05 = (int)Tabuladores.T05;


        #region Propiedades Publicas
        public TipoDeMedicamento Tipo
        {
            get { return tpTipoDeMedicamento; }
            set { tpTipoDeMedicamento = value; }
        }

        public string ClaveLote
        {
            get { return sClaveLote; }
            set { sClaveLote = value; }
        }

        public string Caducidad 
        {
            get { return sCaducidad; }
            set { sCaducidad = value; }
        }

        public string ClaveSSA
        {
            get { return sClaveSSA; }
            set { sClaveSSA = value; }
        }

        public string DecripcionSal
        {
            get { return sDecripcionSal; }
            set { sDecripcionSal = value; }
        }

        public int CantidadRecetada
        {
            get { return iCantidadRecetada; }
            set { iCantidadRecetada = value; }
        }

        public int CantidadSurtida
        {
            get { return iCantidadSurtida; }
            set { iCantidadSurtida = value; }
        }

        public Vale ValeEmitido
        {
            get { return valeMedicamento; }
            set { valeMedicamento = value; }
        }

        public SurteOtraUnidad SurtidoEnOtraUnidad
        {
            get { return surtidoEnOtraUnidad; }
            set { surtidoEnOtraUnidad = value; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        private string getTabs(int Tabs)
        {
            return GnDll_SII_AMPM.getTabs(Tabs);
        }

        public void AgregarMedicamento(Medicamento MedicamentoReceta)       
        {
            if (listaMedicamentos == null)
            {
                listaMedicamentos = new List<Medicamento>(); 
            }

            listaMedicamentos.Add(MedicamentoReceta);
        }

        public string GetString()
        {
            string sRegresa = "";

            switch (tpTipoDeMedicamento)
            {
                case TipoDeMedicamento.MedicamentoSurtido:
                    sRegresa = GetString__Surtido();
                    break;

                case TipoDeMedicamento.MedicamentoVale:
                    sRegresa = GetString__Vale();
                    break;

                case TipoDeMedicamento.Colectivo_Medicamento:
                    sRegresa = GetString__Colectivo_Medicamento();
                    break;

                case TipoDeMedicamento.Colectivo_Solucion:
                    sRegresa = GetString__Colectivo_Solucion();
                    break;

                default:
                    break;
            }

            return sRegresa; 
        }

        private string GetString__Surtido()
        {
            string sRegresa = "";

            sRegresa += string.Format("{0}<{1}>\n", getTabs(iTab_01), "Medicamentos");
            if (listaMedicamentos != null)
            {
                if (listaMedicamentos.Count > 0)
                {
                    foreach (Medicamento itemMedicamento in listaMedicamentos)
                    {
                        sRegresa += string.Format("{0}<{1}>\n", getTabs(iTab_02), "Medicamento");
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_03), "lote", itemMedicamento.ClaveLote);
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_03), "caducidad", itemMedicamento.Caducidad);
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_03), "clave", itemMedicamento.ClaveSSA);
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_03), "cantidadRecetada", itemMedicamento.CantidadRecetada.ToString("###########0"));
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_03), "cantidadSurtida", itemMedicamento.CantidadSurtida.ToString("###########0"));

                        ////sRegresa += itemMedicamento.valeMedicamento.GetString();
                        ////sRegresa += itemMedicamento.surtidoEnOtraUnidad.GetString();

                        sRegresa += string.Format("{0}</{1}>\n", getTabs(iTab_02), "Medicamento");
                    }
                }
            }

            sRegresa += string.Format("{0}</{1}>\n", getTabs(iTab_01), "Medicamentos");

            return sRegresa;
        }


        private string GetString__Colectivo_Medicamento()
        {
            string sRegresa = "";

            sRegresa += string.Format("{0}<{1}>\n", getTabs(iTab_02), "Medicamentos");
            if (listaMedicamentos != null)
            {
                if (listaMedicamentos.Count > 0)
                {
                    foreach (Medicamento itemMedicamento in listaMedicamentos)
                    {
                        sRegresa += string.Format("{0}<{1}>\n", getTabs(iTab_03), "Medicamento");
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_04), "claveMed", itemMedicamento.ClaveSSA);
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_04), "descripcionMed", itemMedicamento.DecripcionSal);
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_04), "loteMed", itemMedicamento.ClaveLote);
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_04), "caducidadMed", itemMedicamento.Caducidad);
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_04), "cantidadSolicitadaMed", itemMedicamento.CantidadRecetada.ToString("###########0"));
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_04), "cantidadSurtidaMed", itemMedicamento.CantidadSurtida.ToString("###########0"));

                        ////sRegresa += itemMedicamento.valeMedicamento.GetString();
                        ////sRegresa += itemMedicamento.surtidoEnOtraUnidad.GetString();

                        sRegresa += string.Format("{0}</{1}>\n", getTabs(iTab_03), "Medicamento");
                    }
                }
            }

            sRegresa += string.Format("{0}</{1}>\n", getTabs(iTab_02), "Medicamentos");

            return sRegresa;
        }

        private string GetString__Colectivo_Solucion()
        {
            string sRegresa = "";

            sRegresa += string.Format("{0}<{1}>\n", getTabs(iTab_02), "Soluciones");
            if (listaMedicamentos != null)
            {
                if (listaMedicamentos.Count > 0)
                {
                    foreach (Medicamento itemMedicamento in listaMedicamentos)
                    {
                        sRegresa += string.Format("{0}<{1}>\n", getTabs(iTab_03), "Solucion");
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_04), "claveSol", itemMedicamento.ClaveSSA);
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_04), "descripcionSol", itemMedicamento.DecripcionSal);
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_04), "loteSol", itemMedicamento.ClaveLote);
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_04), "caducidadSol", itemMedicamento.Caducidad);
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_04), "cantidadSolicitadaSol", itemMedicamento.CantidadRecetada.ToString("###########0"));
                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_04), "cantidadSurtidaSol", itemMedicamento.CantidadSurtida.ToString("###########0"));

                        ////sRegresa += itemMedicamento.valeMedicamento.GetString();
                        ////sRegresa += itemMedicamento.surtidoEnOtraUnidad.GetString();

                        sRegresa += string.Format("{0}</{1}>\n", getTabs(iTab_03), "Solucion");
                    }
                }
            }

            sRegresa += string.Format("{0}</{1}>\n", getTabs(iTab_02), "Soluciones");

            return sRegresa;
        }

        private string GetString__Vale()
        {
            string sRegresa = "";

            sRegresa += string.Format("{0}<{1}>\n", getTabs(iTab_01), "MedicamentosVale");
            if (listaMedicamentos != null)
            {
                if (listaMedicamentos.Count > 0)
                {
                    foreach (Medicamento itemMedicamento in listaMedicamentos)
                    {
                        sRegresa += string.Format("{0}<{1}>\n", getTabs(iTab_02), "Medicamento");
                        //sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_03), "lote", itemMedicamento.ClaveLote);
                        //sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_03), "caducidad", itemMedicamento.Caducidad);
                        //sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_03), "clave", itemMedicamento.ClaveSSA);
                        //sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_03), "cantidadRecetada", itemMedicamento.CantidadRecetada.ToString("###########0"));
                        //sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_03), "cantidadSurtida", itemMedicamento.CantidadSurtida.ToString("###########0"));

                        sRegresa += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_03), "clave", itemMedicamento.ClaveSSA);
                        sRegresa += itemMedicamento.valeMedicamento.GetString();
                        sRegresa += itemMedicamento.surtidoEnOtraUnidad.GetString();

                        sRegresa += string.Format("{0}</{1}>\n", getTabs(iTab_02), "Medicamento");
                    }
                }
            }

            sRegresa += string.Format("{0}</{1}>\n", getTabs(iTab_01), "MedicamentosVale");

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Publicos
    }
}
