using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_ControlsCS;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllFarmaciaSoft.Consultas
{
    public partial class FrmRevisarClaveSSA : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsListView list;

        string sCLaveSSA_Seleccionada = "";
        public bool bClaveSeleccionada = false;

        public FrmRevisarClaveSSA()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name); 

            list = new clsListView(listClaves); 
        }

        private void FrmRevisarClaveSSA_Load(object sender, EventArgs e)
        {

        }

        
        #region Botones
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bClaveSeleccionada = false;
            this.Hide();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            sCLaveSSA_Seleccionada = list.GetValue(1); //listCodigoEAN.FocusedItem.SubItems[0].Text.ToString();

            if (sCLaveSSA_Seleccionada == "")
            {
                General.msjUser("No ha seleccionado una claveSSA valida."); 
            }
            else
            {
                bClaveSeleccionada = true;
                this.Hide();
            }
        }
        #endregion Botones

        #region Lista de Codigos EAN
        private void listCodigoEAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnAceptar_Click(null, null);
            }
        }

        private void listCodigoEAN_DoubleClick(object sender, EventArgs e)
        {
            sCLaveSSA_Seleccionada = list.GetValue(1); //listCodigoEAN.FocusedItem.SubItems[0].Text.ToString();

            if (sCLaveSSA_Seleccionada == "")
            {
                General.msjUser("No ha seleccionado una claveSSA valida.");
            }
            else
            {
                bClaveSeleccionada = true;
                this.Hide();
            }

            ////CodigoSeleccionado = true; 
            ////sCLaveSSA_Seleccionada = list.GetValue(1); //listCodigoEAN.FocusedItem.SubItems[0].Text.ToString();
            ////this.Hide();
        }
        #endregion Lista de Codigos EAN

        #region Funciones y Procedimientos Publicos
        public string VerificarCodigosEAN(string sClaveSSA)
        {
            sCLaveSSA_Seleccionada = sClaveSSA;

            if (RevisarClaveSSA())
            {
                this.ShowDialog();
            }
            else
            {
                this.Hide();
            }

            return sCLaveSSA_Seleccionada;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private bool RevisarClaveSSA()
        {
            bool bMostrarPantalla = true; 

            string sEsSectorSalud = "";
            string sSql = "";

            sSql = string.Format(" Select ClaveSSA, ClaveSSa_Base, DescripcionSal, Presentacion, ContenidoPaquete As 'Contenido Paquete' " +
                                 "From vw_ClavesSSA_Sales (NoLock) " +
                                 "Where ClaveSSa_Base = '{0}' " +
                                 "Order By ClaveSSA  ", 
                                 sCLaveSSA_Seleccionada);

            if (!leer.Exec(sSql)) 
            {
                bMostrarPantalla = false;
                Error.GrabarError(leer, "RevisarClaveSSA");
                General.msjError("Ocurrió un error al validar la ClaveSSA."); 
            }
            else
            {
                if (!leer.Leer())
                {
                    bMostrarPantalla = false;
                }
                else
                {
                    if (leer.Registros == 1)
                    {
                        sCLaveSSA_Seleccionada = leer.Campo("ClaveSSA");
                        bClaveSeleccionada = true;
                        bMostrarPantalla = false;
                    } 
                    else 
                    { 
                        bMostrarPantalla = true; 
                        list.CargarDatos(leer.DataSetClase, true, true);
                        list.AjustarColumnas();
                        list.FocusItem(1); 
                    } 
                }
            }

            return bMostrarPantalla; 
        }
        #endregion Funciones y Procedimientos Privados

        private void listCodigoEAN_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
