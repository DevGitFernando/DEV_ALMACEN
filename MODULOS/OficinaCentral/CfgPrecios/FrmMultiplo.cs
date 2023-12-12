using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;

namespace OficinaCentral.CfgPrecios
{
    public partial class FrmMultiplo : FrmBaseExt
    {
        public bool MultiploAsignado = false;
        public int iMultiplo = 1;
        public bool Afecta_Venta = false;
        public bool Afecta_Consigna = false; 

        public FrmMultiplo(int Multiplo, bool Venta, bool Consigna)
        {
            InitializeComponent();

            iMultiplo = Multiplo;
            Afecta_Venta = Venta;
            Afecta_Consigna = Consigna; 
        }

        private void FrmMultiplo_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
            //txtMultiplo.Text = iMultiplo.ToString();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtMultiplo.Text = iMultiplo.ToString();
            
            chk_Venta.Checked = Afecta_Venta;
            chk_Consigna.Checked = Afecta_Consigna; 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bRegresa = true; 

            if (bRegresa && Convert.ToInt32(txtMultiplo.Text) <= 0)
            {
                bRegresa = false;
                General.msjAviso("No ha puesto un Múltiplo válido, verifique.");
            }

            if(bRegresa && ( !chk_Consigna.Checked && !chk_Venta.Checked  ) )
            {
                General.msjAviso("La configuración es invalida, debe aplicar para Venta ó Consigna, verifique.");
                bRegresa = false;
            }


            if ( bRegresa ) 
            {
                MultiploAsignado = true;

                Afecta_Venta = chk_Venta.Checked;
                Afecta_Consigna = chk_Consigna.Checked;

                iMultiplo = Convert.ToInt32(txtMultiplo.Text);
                this.Hide();
            }
        }
    }
}
