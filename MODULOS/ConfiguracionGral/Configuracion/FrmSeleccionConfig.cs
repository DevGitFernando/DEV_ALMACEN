using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Configuracion.Configuracion
{
    public partial class FrmSeleccionConfig : Form
    {
        bool bEsOficinaCentral = GnConfiguracion.EsOficinaCentral;

        public FrmSeleccionConfig()
        {
            InitializeComponent();
        }

        private void FrmSeleccionConfig_Activated(object sender, EventArgs e)
        {
            if (bEsOficinaCentral)
            {
            }
            else
            { 
            }
        }
    }
}
