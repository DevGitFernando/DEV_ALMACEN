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
    public partial class clsVerificarSalidaLotes  
    {
        public DataSet dtsLotes = clsLotes.PreparaDtsLotes(); 

        public clsVerificarSalidaLotes()
        {
        }

        public bool VerificarExistenciasConError(clsLotes Lotes)
        {
            bool bRegresa = false;

            if (DtGeneral.EsAlmacen)
            {
                bRegresa = VerificarExistenciasConError(Lotes.DataSetUbicaciones, false); 
            }
            else
            {
                bRegresa = VerificarExistenciasConError(Lotes.DataSetLotes, false);
            }

            return bRegresa;
        }

        public bool VerificarExistenciasConError(clsLotes Lotes, bool MostrarMsj)
        {
            bool bRegresa = false;

            if (DtGeneral.EsAlmacen) 
            {
                bRegresa = VerificarExistenciasConError(Lotes.DataSetUbicaciones, MostrarMsj);
            }
            else
            {
                bRegresa = VerificarExistenciasConError(Lotes.DataSetLotes, MostrarMsj);
            }

            return bRegresa;  
        }


        private bool VerificarExistenciasConError(DataSet Lotes)
        {
            return VerificarExistenciasConError(Lotes, false); 
        }

        private bool VerificarExistenciasConError(DataSet Lotes, bool MostrarMsj) 
        {
            bool bRegresa = false;

            if (DtGeneral.EsAlmacen)
            {
                FrmVerificarSalidaLotes_Almacen VerificarLotes_Almacen = new FrmVerificarSalidaLotes_Almacen();
                bRegresa = VerificarLotes_Almacen.VerificarExistenciasConError(Lotes); 
            }
            else
            {
                FrmVerificarSalidaLotes_Farmacia VerificarLotes_Unidad = new FrmVerificarSalidaLotes_Farmacia();
                bRegresa = VerificarLotes_Unidad.VerificarExistenciasConError(Lotes); 
            }

            return bRegresa; 
        }
    }
}
