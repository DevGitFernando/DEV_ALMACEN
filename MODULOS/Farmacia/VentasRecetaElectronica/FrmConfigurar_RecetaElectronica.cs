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
using DllFarmaciaSoft.Devoluciones; 


namespace Farmacia.VentasRecetaElectronica
{
    public partial class FrmConfigurar_RecetaElectronica : FrmBaseExt
    {
        clsDatosCliente DatosCliente; 
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        public FrmConfigurar_RecetaElectronica()
        {
            InitializeComponent();

            con.SetConnectionString();
            leer = new clsLeer(ref con);

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name); 
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
        }

        private void FrmConfigurar_RecetaElectronica_Load(object sender, EventArgs e)
        {

        }
    }
}
