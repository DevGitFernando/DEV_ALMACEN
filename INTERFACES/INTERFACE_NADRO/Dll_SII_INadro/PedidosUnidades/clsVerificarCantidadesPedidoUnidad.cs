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
using DllFarmaciaSoft.Lotes;

namespace Dll_SII_INadro.PedidosUnidades
{
    public partial class clsVerificarCantidadesPedidoUnidad  
    {
        public DataSet dtsLotes = clsLotes.PreparaDtsLotes();
        string sFolioOrden = "";

        public clsVerificarCantidadesPedidoUnidad()
        {
        }

        public bool VerificarCantidadesConExceso(clsLotes Lotes, string FolioOrden)
        {
            bool bRegresa = false;

            sFolioOrden = FolioOrden;
            bRegresa = VerificarCantidadesConExceso(Lotes.DataSetLotes, false);            

            return bRegresa;
        }

        public bool VerificarCantidadesConExceso(clsLotes Lotes, bool MostrarMsj)
        {
            bool bRegresa = false;

            bRegresa = VerificarCantidadesConExceso(Lotes.DataSetLotes, MostrarMsj);
            
            return bRegresa;  
        }


        private bool VerificarCantidadesConExceso(DataSet Lotes)
        {
            return VerificarCantidadesConExceso(Lotes, false); 
        }

        private bool VerificarCantidadesConExceso(DataSet Lotes, bool MostrarMsj) 
        {
            bool bRegresa = false;

            FrmProductosCantidadesExcedidas VerificarCantidadesExceso = new FrmProductosCantidadesExcedidas();
            bRegresa = VerificarCantidadesExceso.VerificarCantidadesConExceso(Lotes, sFolioOrden); 
            
            return bRegresa; 
        }
    }
}
