using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dll_ISESEQ.ExportarInformacion
{
    public partial class FrmDocumentos_Pedidos_SIAM : FrmDocumentos_SIAM 
    {
        public FrmDocumentos_Pedidos_SIAM(): base(TipoDeDocumento.Pedido_Ventas) 
        {
            InitializeComponent();
        }
    }
}
