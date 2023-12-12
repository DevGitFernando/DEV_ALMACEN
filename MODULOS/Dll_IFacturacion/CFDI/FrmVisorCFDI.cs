using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;

namespace Dll_IFacturacion.CFDI
{
    public partial class FrmVisorCFDI : FrmBaseExt
    {
        public FrmVisorCFDI(string Titulo, string Documento)
        {
            InitializeComponent();

            Fg.IniciaControles(); 

            this.Text = "Comprobante : " + Titulo;
            webBrowser.Navigate(Documento);
        }

        private void btnImprimirComprobante_Click(object sender, EventArgs e)
        {
            string sError = "";

            try
            {
                webBrowser.Print();
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            } 
        }
    }
}
