using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FP;
using SC_SolutionsSystem.FP.Huellas;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;

//using Dll_IMach4;
//using Dll_IMach4.Interface;

using DllTransferenciaSoft.ObtenerInformacion;
using DllFarmaciaSoft.Usuarios_y_Permisos;


namespace Farmacia.Transferencias
{
    public partial class FrmTransferenciaEntradas : Farmacia.Transferencias.FrmTransferenciaEntradas_Base
    {
        public FrmTransferenciaEntradas(): base(TipoDeTransferencia.Farmacia_Normal)
        {
        }
    }
}
