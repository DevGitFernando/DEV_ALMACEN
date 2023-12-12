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

using DllFarmaciaSoft;

namespace Farmacia.AlmacenJuris
{
    public partial class FrmConcentrarPedidosRC : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;

        clsGrid grid;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
        string sFolio = "";

        public FrmConcentrarPedidosRC()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            grid = new clsGrid(ref grdPedidos, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            grdPedidos.EditModeReplace = true;
            grid.BackColorColsBlk = Color.White;

        }

        #region Botones 
        private void IniciarToolBar(bool Guardar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnImprimir.Enabled = Imprimir;
        }

        private void LimpiarPantalla()
        {
            grid.Limpiar();
            Fg.IniciaControles();
            IniciarToolBar(false, false);

            //txtIdPersonal.Enabled = false;
            //txtIdPersonal.Text = DtGeneral.IdPersonal;
            //lblPersonal.Text = DtGeneral.NombrePersonal;

            dtpFechaRegistro.Enabled = false;
            dtpFechaRegistro.Value = GnFarmacia.FechaOperacionSistema;

            txtFolio.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            grid.Limpiar();

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
                CargarPedidosPorConcentrar();
            else
                CargarPedidosConcentradosFolio(); 
        }

        private bool validarDatos()
        {
            bool bRegresa = true;
            return bRegresa;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bExito = false;

            if (validarDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    if (GrabarEncabezadoConcentrado())
                    {
                        bExito = true;
                    }

                    if (!bExito)
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar la información.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Información guardada satisfactoriamente.");
                        LimpiarPantalla();
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No fue posible establecer conexión con el servidor, intente de nuevo.");
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Guardar informacion 
        private bool GrabarEncabezadoConcentrado()
        {
            bool bRegresa = true; 
            string sSql = string.Format(" Exec spp_Mtto_ALMJ_Concentrado_PedidosRC '{0}', '{1}', '{2}', '{3}', '{4}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text, DtGeneral.IdPersonal);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sFolio = leer.Campo("FolioConcentrado");
                bRegresa = GrabarDetalleConcentrado(sFolio); 
            }

            return bRegresa;
        }

        private bool GrabarDetalleConcentrado(string Folio)
        {
            bool bRegresa = true;
            string sSql = "";
            string sEmp = "", sEdo = "", sFar = "", sPed = "";

            for (int i = 1; i <= grid.Rows; i++)
            {
                sEmp = grid.GetValue(i, 1);
                sEdo = grid.GetValue(i, 2);
                sFar = grid.GetValue(i, 3);
                sPed = grid.GetValue(i, 5);

                sSql = string.Format(" Exec spp_Mtto_ALMJ_Concentrado_PedidosRC_Pedidos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Folio, sEmp, sEdo, sFar, sPed);  

                if ( grid.GetValueBool(i, 11) )
                {
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa; 
        }

        #endregion Guardar informacion

        private void FrmConcentrarPedidosRC_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();

            //grdPedidos.Sheets[0].Rows.Count = 10;

        }

        private void CargarPedidosPorConcentrar()
        {
            txtFolio.Enabled = false;
            txtFolio.Text = "*";

            // string sSql = string.Format(" Exec spp_AMLJ_ObtenerPedidosPendientesConcentrar '{0}', '{1}', '{2}' ", sEmpresa, sEstado, sFarmacia);
            string sSql = string.Format(" Exec spp_AMLJ_ObtenerPedidosPendientesConcentrar " );
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarPedidosPorConcentrar()");
            }
            else
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                    btnGuardar.Enabled = true;
                }
            }
        }

        private void CargarPedidosConcentradosFolio()
        {
            txtFolio.Enabled = false;
            txtFolio.Text = Fg.PonCeros(txtFolio.Text, 6);

            string sSql = string.Format(" Exec spp_AMLJ_ObtenerPedidosConcentradosFolio '{0}', '{1}', '{2}', '{3}' ", sEmpresa, sEstado, sFarmacia, txtFolio.Text);

        }
    }
}
