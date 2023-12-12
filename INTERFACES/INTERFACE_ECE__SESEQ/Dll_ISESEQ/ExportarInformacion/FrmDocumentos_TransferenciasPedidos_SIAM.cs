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
    public partial class FrmDocumentos_TransferenciasPedidos_SIAM : FrmDocumentos_SIAM 
    {
        public FrmDocumentos_TransferenciasPedidos_SIAM(): base(TipoDeDocumento.Pedido_Transferencias) 
        {
            InitializeComponent();
        }
    }
}
