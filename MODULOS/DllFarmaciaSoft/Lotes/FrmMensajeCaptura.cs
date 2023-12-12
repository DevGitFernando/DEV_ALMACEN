using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using FarPoint.Win.Spread.CellType;

using SC_ControlsCS;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft; 


namespace DllFarmaciaSoft.Lotes
{
    public partial class FrmMensajeCaptura : FrmBaseExt 
    {
        private bool bConfirmacionDeCantidadCapturada = false;
        string sMensaje = "";
        int iMultiplo = 0;
        int iMultiplo_Comercial = 0;
        int iCantidad = 0;
        string sRTF_Mensaje = "";

        string sFormato = "##,###,###,###,##0"; 
        Color colorActivo = Color.White;
        Color colorDefault = Color.White;

        public FrmMensajeCaptura(int Multiplo, int MultiploComercial, int CantidadCaptura, string Mensaje)
        {
            InitializeComponent();

            this.iMultiplo = Multiplo;
            this.iMultiplo_Comercial = MultiploComercial; 
            this.iCantidad = CantidadCaptura;
            this.sMensaje = Mensaje; 
        }

        private void FrmMensajeCaptura_KeyDown(object sender, KeyEventArgs e)
        {
            switch( e.KeyCode )
            {
                case Keys.Enter:
                    break; 

                case Keys.F5:
                    bConfirmacionDeCantidadCapturada = true;
                    this.Hide(); 
                    break;

                case Keys.F12:
                    this.Hide(); 
                    break;
            }
        }

        private void FrmMensajeCaptura_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();

            Color colorActivo = Color.NavajoWhite;
            Color colorDefault = this.BackColor;

            ////sMensaje = string.Format("La cantidad capturada son {0} 'Paquetes - Bolsas - Envases'\n¿ Es correcto ? ",
            ////    (iCantidad / iMultiplo));
            lblMensaje.Text = sMensaje; 

            this.Text += string.Format(" en Multiplos de {0}", iMultiplo);


            sRTF_Mensaje = @"{\rtf1\ansi ";
            sRTF_Mensaje += string.Format(@"Si esta entregando \b {0}\b0   'Paquetes - Bolsas - Envases' cerrado(s) se debe capturar en multiplos de  \b {1}\b0    " +
                @"lo cual representa  \b {2}\b0     pieza(s)", iCantidad, iMultiplo_Comercial, (iCantidad * iMultiplo_Comercial));
            sRTF_Mensaje += "}"; 


            sRTF_Mensaje = @"{\rtf1\ansi "; 
            sRTF_Mensaje += string.Format(@"Si esta entregando \ul\b {0}\b0\ul0   'Paquetes - Bolsas - Envases' cerrado(s) se debe capturar en multiplos de  \ul\b {1}\b0\ul0    " +
                @"lo cual representa  \ul\b {2}\b0\ul0     pieza(s)",
                iCantidad.ToString(sFormato), iMultiplo_Comercial.ToString(sFormato), (iCantidad * iMultiplo_Comercial).ToString(sFormato));
            sRTF_Mensaje += "}"; 


            ////txtchMensaje02.AgregarTexto(string.Format("Si esta entregando "), "", true);
            ////txtchMensaje02.AgregarTexto(string.Format(" {0} ", iCantidad), "", true, false, FontStyle.Bold);
            ////txtchMensaje02.AgregarTexto(string.Format("   'Paquetes - Bolsas - Envases' cerrado(s) se debe capturar en multiplos de  "), "", true);
            ////txtchMensaje02.AgregarTexto(string.Format(" {0} ", iMultiplo_Comercial), "", true, false, FontStyle.Bold);
            ////txtchMensaje02.AgregarTexto(string.Format("   lo cual representa  "), "", true);
            ////txtchMensaje02.AgregarTexto(string.Format(" {0} ", (iCantidad * iMultiplo_Comercial)), "", true, false, FontStyle.Bold);
            ////txtchMensaje02.AgregarTexto(string.Format("   pieza(s)  "), "", true);


            //txtchMensaje.Rtf = string.Format(@"Si esta entregando \b0\b0   'Paquetes - Bolsas - Envases' cerrado(s) se debe capturar en multiplos de  \b1\b0    " +
            //    "lo cual representa  \b2\b0 pieza(s)", iCantidad, iMultiplo_Comercial, (iCantidad * iMultiplo_Comercial));

            txtchMensaje.Rtf = sRTF_Mensaje; 

            btnCerrar.Focus(); 
        }

        public bool SolicitarConfirmacionDeCantidadCapturada()
        {
            this.ShowDialog(); 
            return bConfirmacionDeCantidadCapturada; 
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Hide(); 
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bConfirmacionDeCantidadCapturada = true;
            this.Hide(); 
        }


        private void btnCerrar_Enter(object sender, EventArgs e)
        {
            btnAceptar.BackColor = colorDefault;
            btnCerrar.BackColor = colorActivo; 
        }

        private void btnAceptar_Enter(object sender, EventArgs e)
        {
            btnAceptar.BackColor = colorActivo;
            btnCerrar.BackColor = colorDefault; 
        }
    }
}
