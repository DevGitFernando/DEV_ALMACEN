using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGrid; 
using SC_ControlsCS;
using SC_CompressLib;
using DllFarmaciaSoft;
using DllTransferenciaSoft; 

namespace DllTransferenciaSoft.Informacion
{
    public partial class FrmInformacion : FrmBaseExt
    {
        clsGridView grid; 

        public FrmInformacion()
        {
            InitializeComponent();
            grid = new clsGridView(ref grdModulos); 
        }

        private void FrmInformacion_Load(object sender, EventArgs e)
        {
            grid.Limpiar();

            grdModulos.Rows.Add(General.DatosApp.Modulo, General.DatosApp.Version); 
            grdModulos.Rows.Add(GnControls.DatosApp.Modulo, GnControls.DatosApp.Version); 
            grdModulos.Rows.Add(GnCompress.DatosApp.Modulo, GnCompress.DatosApp.Version); 

            grdModulos.Rows.Add(DtGeneral.DatosApp.Modulo, DtGeneral.DatosApp.Version); 
            grdModulos.Rows.Add(Transferencia.DatosApp.Modulo, Transferencia.DatosApp.Version); 
        }
    }
}
