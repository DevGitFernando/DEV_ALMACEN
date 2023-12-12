using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Dll_SII_IMediaccess.Ventas_IME; 

namespace Dll_SII_IMediaccess
{
    public partial class Form1 : Form
    {
        clsValidar_Vale vale;
        FrmRegistroDeVales frmVales; 
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vale = new clsValidar_Vale();
            vale.IdSocioComercial = "1";
            vale.NombreSocioComercial = "INTERMED";
            vale.IdSucursalSocioComercial = "1";
            vale.NombreSucursalSocioComercial = "HOSPITAL GENERAL DE LEÓN";
            vale.FolioVale = "11";

            vale.IdCliente = "1";
            vale.ClienteNombre = "INTERMED";
            vale.IdSubCliente = "1";
            vale.SubClienteNombre = "GUANAJUATO";



            frmVales = new FrmRegistroDeVales(vale);
            frmVales.Show(); 
        }
    }
}
