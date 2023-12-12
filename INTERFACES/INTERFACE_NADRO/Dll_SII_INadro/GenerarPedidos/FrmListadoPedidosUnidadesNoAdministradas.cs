using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using Dll_SII_INadro;
using Dll_SII_INadro.GenerarArchivos; 

namespace Dll_SII_INadro.GenerarPedidos
{
    public partial class FrmListadoPedidosUnidadesNoAdministradas : FrmBaseExt 
    {
        enum Cols
        {
            Fecha = 1, FolioPedido = 2, IdEstado = 3, Estado = 4, IdFarmacia = 5, Farmacia = 6, Cliente = 7, TipoUnidad = 8
        } 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;
        clsConsultas Consultas;

        clsListView lst; 

        clsDatosCliente DatosCliente;

        public FrmListadoPedidosUnidadesNoAdministradas()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnDll_SII_INadro.DatosApp, this.Name, "");

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, General.Modulo, this.Name, General.Version);
            Ayuda = new clsAyudas(General.DatosConexion, General.Modulo, this.Name, General.Version);

            lst = new clsListView(lstvUnidades); 
        }

        #region Form 
        private void FrmListadoPedidosUnidadesNoAdministradas_Load(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }
        #endregion Form

        #region Botones
        private void InicializarPantalla()
        {
            lst.LimpiarItems(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            ObtenerUnidadesConPedidosPendientes(); 
        }

        private void btnIntegrarDocumento_Click(object sender, EventArgs e)
        {
            Pedidos gn = new Pedidos(1);
            gn.GenerarPedidos();
            ObtenerUnidadesConPedidosPendientes();
        }
        #endregion Botones 

        #region Informacion 
        private void ObtenerUnidadesConPedidosPendientes()
        {
            string sSql = string.Format("Exec spp_INT_ND_ListadoUnidadesPedidos " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @CodigoCliente = '{3}', @EsDeSurtimiento = '{4}', @GenerarPedido = '{5}'  ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "*", 1, 0);

            lst.Limpiar(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerUnidadesConPedidosPendientes()");
                General.msjError("Ocurrió un error al obtener el listado de pedidos a generar.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontrarón pedidos pendientes de generar."); 
                }
                else
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                }
            }
        }
        #endregion Informacion

        #region Menu
        private void btnGenerarPedidoGeneral_Click(object sender, EventArgs e)
        {
            Pedidos gn = new Pedidos(0);
            gn.GenerarPedidos(1, 0);
            ObtenerUnidadesConPedidosPendientes();
        }

        private void btnGenerarPedidoPorFarmacia_Click(object sender, EventArgs e)
        {
            string sCliente = lst.GetValue((int)Cols.Cliente);

            Pedidos gn = new Pedidos(0);
            gn.GenerarPedidos(sCliente);
            ObtenerUnidadesConPedidosPendientes();
        }

        private void lstvUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGenerarPedidoGeneral.Enabled = lst.Registros > 0;
            btnGenerarPedidoPorFarmacia.Enabled = lst.Registros > 0; 
        }
        #endregion Menu 
    }
}
