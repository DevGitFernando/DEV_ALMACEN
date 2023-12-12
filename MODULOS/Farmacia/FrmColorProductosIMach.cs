using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales; 
using SC_SolutionsSystem.FuncionesGrid; 
using SC_SolutionsSystem.Usuarios_y_Permisos;
using Farmacia;

using DllFarmaciaSoft; 

namespace Farmacia
{
    public partial class FrmColorProductosIMach : FrmBaseExt 
    {
        clsGenerarMenu myMenu;
        clsGrid grid; 

        public FrmColorProductosIMach()
        {
            InitializeComponent(); 

            grid = new clsGrid(ref grdColor, this); 
        }

        public FrmColorProductosIMach(clsGenerarMenu menu)
        {
            InitializeComponent();
            myMenu = menu; 
        }

        private void FrmTestColor_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles(); 


            cboColores.Clear();
            cboColores.Add("0", "<< Seleccione >>");

            // int index = 0;
            string[] systemEnvironmentColors = new string[(typeof(System.Drawing.SystemColors)).GetProperties().Length];

            foreach (MemberInfo m in (typeof(System.Drawing.SystemColors)).GetProperties())
            {
                // cboColores.Add(m.Name, m.Name);
            }

            // Colores 
            foreach (MemberInfo m in (typeof(System.Drawing.Color)).GetProperties())
            {
                cboColores.Add(m.Name, m.Name);
            }

            cboColores.SelectedIndex = 0; 
            lblColor.BackColor = GnFarmacia.ColorProductosIMach;
            cboColores.Text = GnFarmacia.ColorProductosIMach.Name;

            // General.FormaBackColor = Color.LightBlue;
            this.BackColor = Color.LightBlue;
            toolStripBarraMenu.BackColor = Color.SteelBlue;
            lblColor.BackColor = Color.LightBlue;

        }

        private void cboColores_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblColor.BackColor = this.BackColor;
            if (cboColores.SelectedIndex != 0)
            {
                lblColor.BackColor = Color.FromName(cboColores.Text);

                if (myMenu != null)
                {
                    myMenu.SetBackColor(Color.FromName(cboColores.Text));
                }

                // General.FormaBackColor = Color.FromName(cboColores.Text);
                try
                {
                    cboColores.BackColor = lblColor.BackColor;
                    cboColores.BackColorEnabled = lblColor.BackColor;
                }
                catch { }

                try
                {
                    grid.ColorCelda(1, 1, lblColor.BackColor);
                }
                catch { }


                try
                {
                    button1.BackColor = lblColor.BackColor;
                }
                catch { }

                try
                {
                    this.BackColor = General.FormaBackColor;
                }
                catch { }

                try
                {
                    toolStripBarraMenu.BackColor = this.BackColor;
                }
                catch { }
            }
        }

        private void FrmTestColor_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GnFarmacia.ColorProductosIMach = Color.FromName(cboColores.Text);
            this.Hide(); 
        }
    }
}
