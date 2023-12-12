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
using DllFarmaciaSoft.LimitesConsumoClaves;
using DllFarmaciaSoft.Usuarios_y_Permisos;

using Farmacia.Procesos;
using Farmacia.Vales;
using Farmacia.Ventas;


namespace Farmacia.Ventas_Express__DM
{
    public partial class FrmEDM_0002_ListadoDeFolios_GenerarVenta : FrmBaseExt
    {
        clsDatosCliente DatosCliente;
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leer2;
        clsLeer leer3;

        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        clsListView lst;

        FrmVentas InfVtas;

        private enum cols
        {
            Ninguna, Folio, PersonalRegistra, FechaRegistro, FechaReceta, Receta
        }

        public FrmEDM_0002_ListadoDeFolios_GenerarVenta()
        {
            InitializeComponent();

            leer = new clsLeer(ref con);
            leer2 = new clsLeer(ref con);
            leer3 = new clsLeer(ref con);

            lst = new clsListView(lstFolios);

            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);
            Error = new clsGrabarError(GnFarmacia.DatosApp, this.Name);
        }

        private void FrmEDM_0002_ListadoDeFolios_GenerarVenta_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format(" Select Folio, IdPersona + ' -- ' + NombrePersonal As 'PersonalRegistra', " +
		        "   Convert(Varchar(10), FechaRegistro, 120) As FechaRegistro, Convert(Varchar(10), FechaReceta, 120)  As FechaReceta, NumReceta As Receta, " +
                "   Idcliente, NombreCliente, IdSubCliente, NombreSubCliente " +
	            "From vw_Ventas_EDM_Enc (NoLock) " +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And Convert(Varchar(10), FechaRegistro, 120) between '{3}' And '{4}' And " +
                        "FolioVenta = ''",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value));

            //Grid.Limpiar(false);
            lst.LimpiarItems();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información.");
            }
            else
            {
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase);
                }
                else
                {
                    General.msjUser("No existe Información Para mostrar.");
                    btnNuevo_Click(this, null);
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            chkConUbicaciones.Checked = false;
            lst.LimpiarItems();
            //InfVtas = new FrmEDM_0003_GenerarVenta();
        }

        private void lstFolios_DoubleClick(object sender, EventArgs e)
        {
            InfVtas = new FrmVentas();
            if (lst.GetValue((int)cols.Folio) != "")
            {
                //InfVtas.ClienteSeguroPopular = true;
                //InfVtas.PermitirBeneficiariosNuevos = true;
                //InfVtas.PermitirImportarBeneficiarios = true;
                //InfVtas.Show(lst.LeerItem().Campo("Folio"), lst.LeerItem().Campo("Idcliente"), lst.LeerItem().Campo("NombreCliente"),
                //                lst.LeerItem().Campo("IdSubCliente"), lst.LeerItem().Campo("NombreSubCliente"), lst.LeerItem().Campo("Receta"));
                InfVtas.VentaEDM(lst.LeerItem().Campo("Receta"), lst.LeerItem().Campo("Folio"));
                btnEjecutar_Click(this, null);
            } 
        }
    }
}
