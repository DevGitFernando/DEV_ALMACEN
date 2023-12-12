using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Dll_SII_IMediaccess.Ventas
{
    public partial class FrmVentas_MA_Asociados : FrmVentas_MA
    {
        public FrmVentas_MA_Asociados():base(true, DllFarmaciaSoft.TiposDeInventario.Venta)
        {
            //InitializeComponent();
        }
    }
}
