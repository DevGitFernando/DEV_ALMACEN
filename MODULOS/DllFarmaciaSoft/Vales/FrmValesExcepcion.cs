using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Devoluciones;
using DllFarmaciaSoft.LimitesConsumoClaves;
using DllFarmaciaSoft.Usuarios_y_Permisos;


namespace DllFarmaciaSoft.Vales
{
    public partial class FrmValesExcepcion : FrmBaseExt 
    {
        public FrmValesExcepcion()
        {
            InitializeComponent();
        }

        private void FrmValesExcepcion_Load(object sender, EventArgs e)
        {
            IniciarPantalla();
        }

        #region Botones 
        private void IniciarPantalla()
        {
            Fg.IniciaControles();
            lblDecodificado.Visible = false;

            txtLlaveGenerada.ReadOnly = true;
            txtScriptGenerado.ReadOnly = true; 

            txtIdEstado.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            IniciarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileSave = new SaveFileDialog();
            System.IO.StreamWriter outPut; 
            string sFileName = "ExcepcionVales.sql";


            fileSave.Title = "Guardar archivo de excepción de vales";
            fileSave.Filter = "Scripts SQL Server (*.sql)|*.sql";
            fileSave.FileName = sFileName;
            fileSave.AddExtension = true;
            fileSave.CheckPathExists = true;


            if (fileSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                outPut = new System.IO.StreamWriter(fileSave.FileName);

                outPut.WriteLine(txtScriptGenerado.Text); 

                outPut.Close();
                outPut = null; 
            }

        }

        private void btnConfirmarHuella_Click(object sender, EventArgs e)
        {
            string sCadena = "";
            string sScriptGenerado = "";
            string sWhere = "Where NombreParametro = 'EmiteValesExcepcion' ";

            lblDecodificado.Visible = false; 


            if (txtIdEstado.Text.Trim() != "")
            {
                sWhere += string.Format("    and IdEstado = '{0}' ", Fg.PonCeros(txtIdEstado.Text, 2));
                sCadena = Fg.PonCeros(txtIdEstado.Text, 2);
                if ( txtIdFarmacia.Text.Trim() != "")
                {
                    sWhere += string.Format("    And IdFarmacia = '{0}' ", Fg.PonCeros(txtIdFarmacia.Text, 4));
                    sCadena += Fg.PonCeros(txtIdFarmacia.Text, 4);
                }
            }

            sCadena += General.FechaYMD(dtpFechaDeSistema.Value);
            txtLlaveGenerada.Text = GnFarmacia.GetExcepcionVales(sCadena);

            sScriptGenerado = string.Format(
                "Update P Set Valor = '{1}' \n" +
                "From Net_CFGC_Parametros P (NoLock) \n     {0} ", sWhere, txtLlaveGenerada.Text);

            txtScriptGenerado.Text = sScriptGenerado; 
        }

        private void btnDecodificar_Click(object sender, EventArgs e)
        {
            lblDecodificado.Visible = true; 
            lblDecodificado.Text = GnFarmacia.GetExcepcionValesDecode(txtLlaveGenerada.Text); 
        }
        #endregion Botones
    }
}
