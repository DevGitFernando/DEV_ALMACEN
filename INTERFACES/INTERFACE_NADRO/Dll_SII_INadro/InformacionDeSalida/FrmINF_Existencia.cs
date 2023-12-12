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

namespace Dll_SII_INadro.InformacionDeSalida
{
    public partial class FrmINF_Existencia : FrmBaseExt 
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

        public FrmINF_Existencia()
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
        private void FrmINF_Existencia_Load(object sender, EventArgs e)
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
            ObtenerListadoDeClientes();
        }

        private void btnIntegrarDocumento_Click(object sender, EventArgs e)
        {
            ////Existencias gn = new Existencias();
            ////gn.GenerarExistencias();
            ////ObtenerListadoDeClientes();
        }
        #endregion Botones

        #region Informacion
        private void ObtenerListadoDeClientes()
        {
            string sSql = string.Format("Exec spp_INT_ND_ListadoDeClientes " +
                " @IdEstado = '{0}', @EsDeSurtimiento = '{1}', @TipoDeCliente = '{2}' ", DtGeneral.EstadoConectado, 1, 1 );

            lst.Limpiar();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerListadoDeClientes()");
                General.msjError("Ocurrió un error al obtener el listado de clientes.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontrarón clientes para la generacion de existencias.");
                }
                else
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                }
            }
        }
        #endregion Informacion

        #region Menu 
        private void btnGenerarExistenciaGeneral_Click(object sender, EventArgs e)
        {
            ////Existencias gn = new Existencias();
            ////gn.GenerarExistencias();
            ////ObtenerListadoDeClientes();
        }

        private void btnGenerarExistenciaPorFarmacia_Click(object sender, EventArgs e)
        {
            string sCliente = lst.GetValue((int)Cols.Cliente);

            ////Existencias gn = new Existencias();
            ////gn.GenerarExistencias(sCliente);
            ////General.msjUser("Archivos de existencia generado satisfactoriamente."); 
            ////ObtenerListadoDeClientes();
        }

        private void lstvUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGenerarExistenciaGeneral.Enabled = lst.Registros > 0;
            btnGenerarExistenciaPorFarmacia.Enabled = lst.Registros > 0; 
        }

        ////private void btnGenerarExistenciaGeneral_Click(object sender, EventArgs e)
        ////{
        ////    Pedidos gn = new Pedidos(0);
        ////    gn.GenerarPedidos();
        ////    ObtenerListadoDeClientes();
        ////}

        ////private void btnGenerarExistenciaPorFarmacia_Click(object sender, EventArgs e)
        ////{
        ////    string sCliente = lst.GetValue((int)Cols.Cliente);

        ////    Pedidos gn = new Pedidos(0);
        ////    gn.GenerarPedidos(sCliente);
        ////    ObtenerListadoDeClientes();
        ////}

        ////private void lstvUnidades_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    btnGenerarExistenciaGeneral.Enabled = lst.Registros > 0;
        ////    btnGenerarExistenciaPorFarmacia.Enabled = lst.Registros > 0;
        ////}
        #endregion Menu
    }
}
