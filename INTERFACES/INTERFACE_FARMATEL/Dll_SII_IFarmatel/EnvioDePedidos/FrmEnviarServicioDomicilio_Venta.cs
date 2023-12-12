using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using Dll_SII_IFarmatel; 


namespace Dll_SII_IFarmatel.EnvioDePedidos
{
    public partial class FrmEnviarServicioDomicilio_Venta : FrmBaseExt
    {
        #region Declaracion de Variables
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsDatosCliente DatosCliente;

        clsGrid grid;
        Thread thEnvio;
        bool bEnvioHabilitado = false;

        #endregion Declaracion de Variables

        public FrmEnviarServicioDomicilio_Venta()
        {
            InitializeComponent();
        }
    }
}
