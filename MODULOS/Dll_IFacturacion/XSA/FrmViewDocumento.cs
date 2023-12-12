using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem; 

namespace Dll_IFacturacion.XSA
{
    internal partial class FrmViewDocumento : FrmBaseExt 
    {
        public FrmViewDocumento(string Titulo, string Documento)
        {
            InitializeComponent();

            this.Text = "Documento : " + Titulo; 
            webBrowser.Navigate(Documento); 
        }
    }
}
