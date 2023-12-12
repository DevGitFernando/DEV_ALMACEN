using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
//using SC_SolutionsSystem.Reportes;
//using SC_SolutionsSystem.Errores;

//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;

namespace Dll_SII_IRFID.Registro
{
    public partial class FrmConfirmacionRegistroDePresalida : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid myGrid;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFolioMovimiento = ""; 

        public FrmConfirmacionRegistroDePresalida()
        {
            InitializeComponent();
        }
    }
}
