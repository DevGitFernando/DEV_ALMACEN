using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_ControlsCS;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;

namespace DllFarmaciaSoft.Lotes
{
    public partial class FrmProductosConDiferencias : FrmBaseExt 
    {
        public DataSet dtsLotes = clsLotes.PreparaDtsLotes();
        clsGrid myGrid;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        // clsLeer leer;
        // clsConsultas query;

        DataSet dtsProductosDiferencias;

        // string sTabla = "";
        public bool ErrorAlValidarSalida = false; 

        public FrmProductosConDiferencias(DataSet DtsProductos)
        {
            InitializeComponent();

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            dtsProductosDiferencias = DtsProductos; 
        }

        private void FrmProductosConDiferencias_Load(object sender, EventArgs e)
        {
            myGrid.LlenarGrid(dtsProductosDiferencias); 
        }
    }
}
