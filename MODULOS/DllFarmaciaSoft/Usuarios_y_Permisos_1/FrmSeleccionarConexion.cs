using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_ControlsCS; 

using DllFarmaciaSoft;

namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    internal partial class FrmSeleccionarConexion : FrmBaseExt 
    {
        public bool NodoSeleccionado = false;
        public int NumNodoSeleccionado = 0;
        string sRutaXml = ""; 

        public FrmSeleccionarConexion(string RutaXml)
        {
            InitializeComponent();

            sRutaXml = RutaXml;
        }

        private void FrmSeleccionarConexion_Load(object sender, EventArgs e)
        {
            CargarNodos(); 
        }

        private void CargarNodos()
        {
            clsLeer leer = new clsLeer();
            DataSet dts = new DataSet();

            cboNodos.Items.Clear();
            cboNodos.Items.Add("<<Seleccione>>");

            dts.ReadXml(sRutaXml);
            leer.DataSetClase = dts;
            leer.DataTableClase = leer.Tabla(1);

            while (leer.Leer())
            {
                cboNodos.Items.Add(leer.Campo("Alias"));
            }

            cboNodos.SelectedIndex = 0;
            cboNodos.MaxDropDownItems = cboNodos.Items.Count;
            cboNodos.DropDownHeight = cboNodos.MaxDropDownItems * 14; 
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            NodoSeleccionado = cboNodos.SelectedIndex > 0;
            NumNodoSeleccionado = cboNodos.SelectedIndex;
            this.Hide(); 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            NodoSeleccionado = false;
            NumNodoSeleccionado = 0;
            this.Hide(); 
        }

        private void cboNodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarValores();
            if (cboNodos.SelectedIndex != 0)
            {
                NumNodoSeleccionado = cboNodos.SelectedIndex - 1;
                CargarValores(cboNodos.Text);
                //CargarNodos();
            }
        }

        private void LimpiarValores()
        {
            try
            {
                panel.Controls.Clear();
            }
            catch { }
        }

        private void CargarValores(string AliasSeleccionado)
        {
            clsLeer leerValores = new clsLeer();
            clsLeer leer = new clsLeer();
            DataSet dts = new DataSet();
            bool bColumnaValida = false;
            string sColumna = ""; 
            int iTop = 10;
            int iLeft = 10;
            int iLeft_txt = 160;

            try
            {                

                dts.ReadXml(sRutaXml);
                //dtsValores = dts.Copy(); 

                leer.DataSetClase = dts;
                leerValores.DataTableClase = leer.Tabla(1);

                try
                {
                    leerValores.DataRowsClase = leer.Tabla(1).Select(string.Format(" Alias = '{0}' ", AliasSeleccionado));
                }
                catch { }

                leerValores.Leer();
                foreach (string sCol in leerValores.ColumnasNombre)
                {
                    sColumna = sCol.ToUpper();
                    bColumnaValida = sColumna == "Alias".ToUpper() || sColumna == "Servidor".ToUpper() || sColumna == "WebService".ToUpper() || sColumna == "PaginaAsmx".ToUpper();
                    if (bColumnaValida)
                    {
                        scLabelExt lbl = new scLabelExt();
                        lbl.Name = "lbl" + sCol;
                        lbl.TextAlign = ContentAlignment.MiddleRight;
                        lbl.Text = sCol + " : ";
                        lbl.Visible = true;
                        lbl.Height = 20;
                        lbl.Width = 150;
                        lbl.Left = iLeft;
                        lbl.Top = iTop;

                        TextBox txt = new TextBox();
                        txt.Name = sCol;
                        txt.Text = leerValores.Campo(sCol);
                        txt.Visible = true;
                        txt.ReadOnly = true;
                        txt.Height = 20;
                        txt.Width = 390;
                        txt.Left = iLeft_txt;
                        txt.Top = iTop;
                        ////txt.Enabled = false; 

                        iTop += 23;

                        panel.Controls.Add(lbl);
                        panel.Controls.Add(txt);
                    }
                }
            }
            catch { }

            btnAceptar.Enabled = panel.Controls.Count > 0;
        }
    }
}
