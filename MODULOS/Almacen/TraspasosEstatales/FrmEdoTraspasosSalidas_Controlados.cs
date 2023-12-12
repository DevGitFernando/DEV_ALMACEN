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

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Usuarios_y_Permisos;

using DllTransferenciaSoft.ObtenerInformacion;
using Farmacia.Transferencias; 

namespace Almacen.TraspasosEstatales
{
    public partial class FrmEdoTraspasosSalidas_Controlados : FrmTransferenciaSalidas_Base 
    {
        public FrmEdoTraspasosSalidas_Controlados(): base(TipoDeTransferencia.Almacen_Controlados)
        {
        }
    }
}
