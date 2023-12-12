using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.SistemaOperativo;

using DllTransferenciaSoft.Zip;

namespace DllTransferenciaSoft.ReplicacionSQL
{
    public partial class FrmControlReplicacion : FrmBaseExt
    {
        public FrmControlReplicacion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clsReplicacionSQL replicacion = new clsReplicacionSQL();

            replicacion.GenerarArchivos();

            replicacion.EnviarArchivos();
            General.msjUser("Replicación terminada..");
        }
    }
}
