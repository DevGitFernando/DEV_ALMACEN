using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Dll_SII_INadro.PedidosUnidades
{
    public partial class FrmPedidosDeUnidad_NoSolicitado : Dll_SII_INadro.PedidosUnidades.FrmPedidosDeUnidad
    {
        public FrmPedidosDeUnidad_NoSolicitado():base(true) 
        {
            InitializeComponent();
        }
    }
}
