using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;

namespace Farmacia.Seguimientos
{
    public partial class FrmSeguimiento_PedidosCEDIS : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeerWebExt leerWeb;
        clsLeer leer;
        clsAyudas ayuda;
        clsConsultas query;
        clsGrid grid;
        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;
        string sUrlFarmacia = "";

        public FrmSeguimiento_PedidosCEDIS()
        {
            InitializeComponent();
            Fg.IniciaControles();
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaFoliosDePedidos");
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            
            leer = new clsLeer(ref cnn); 
            grid = new clsGrid(ref grdPedidos, this);
            grid.EstiloDeGrid = eModoGrid.Normal; 
            
            lblConsultando.BackColor = colorEjecutando;
            lblFinExito.BackColor = colorEjecucionExito;
            lblFinError.BackColor = colorEjecucionError;
        }

        private void FrmSeguimiento_PedidosCEDIS_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            CargarAlmacenes();
            grid.Limpiar(false);
            cboAlmacen.Enabled = true;
            chkTodas.Checked = false;
            dtpFechaInicial.Value = dtpFechaFinal.Value = General.FechaSistemaObtener();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            grid.Limpiar(false);
            chkTodas.Checked = false;
            cboAlmacen.Enabled = false;
            IniciarToolBar(true, true, false, false);
            leer.DataSetClase = query.Farmacias_Urls(DtGeneral.EstadoConectado, cboAlmacen.Data, "cboAlmacen_SelectedIndexChanged");
            if (leer.Leer())
            {
                sUrlFarmacia = leer.Campo("UrlFarmacia");
                string sSql = string.Format("Select FolioPedido, Convert(Varchar(10) ,FechaRegistro, 120) FechaRegistro " +
                                    "From Pedidos_Cedis_Enc (NoLock)" +
                                    "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And " +
                                    "Convert(Varchar(10), FechaRegistro, 120) Between '{3}' And '{4}'",
                                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, dtpFechaInicial.Text, dtpFechaFinal.Text);
                if (leer.Exec(sSql))
                {
                    if (leer.Leer())
                    {
                        grid.LlenarGrid(leer.DataSetClase);
                        IniciarToolBar(true, true, true, false);
                    }
                    else
                    {
                        General.msjUser("No existe información para mostrar.");
                    }
                }
            }
        }

        private void btnActivarServicios_Click(object sender, EventArgs e)
        {
            btnActivarServicios.Enabled = false;
            IniciarServicios();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {

        }

        #endregion Botones

        #region Funciones y procedimientos

        private void CargarAlmacenes()
        {
            cboAlmacen.Clear();
            cboAlmacen.Add("0", "<< Seleccione >>");
            query.MostrarMsjSiLeerVacio = false;

            leer.DataSetClase = query.Farmacias(DtGeneral.EstadoConectado, "CargarAlmacenes");

            if (leer.Leer())
            {
                cboAlmacen.Filtro = "EsAlmacen = 1";
                cboAlmacen.Add(leer.DataSetClase, true, "IdFarmacia", "NombreFarmacia");
            }

            cboAlmacen.SelectedIndex = 0;

            query.MostrarMsjSiLeerVacio = true;
        }

        private void IniciarServicios()
        {
            IniciarToolBar(false, false, false, false);
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            for (int i = 1; i <= grid.Rows; i++)
            {
                grid.SetValue(i, 4, " ");
                grid.SetValue(i, 5, " ");
                grid.ColorRenglon(colorEjecucionExito);
                if (grid.GetValueBool(i, 3))
                {
                    Thread _workerThread = new Thread(this.ConsultarpedidosFarmacia);
                    _workerThread.Name = grid.GetValue(i, 1);
                    _workerThread.Start(i);
                }
            }
        }

        private void ConsultarpedidosFarmacia(object Renglon)
        {
            int iRow = (int)Renglon;
            string sFolio = grid.GetValue(iRow, 1);

            string sSql = string.Format("Exec SPP_MTTO_ObtenerStatusPedido '{0}', '{1}', '{2}', '{3}', '{4}'",
                                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, cboAlmacen.Data, DtGeneral.FarmaciaConectada, sFolio);


            grid.ColorRenglon(iRow, colorEjecutando);
            iBusquedasEnEjecucion++;

            clsLeerWebExt myWeb = new clsLeerWebExt(sUrlFarmacia, DtGeneral.CfgIniPuntoDeVenta, datosCliente);
            if (!myWeb.Exec(sSql))
            {
                Error.GrabarError(myWeb, sFolio + " -- " + sUrlFarmacia, "ConsultarpedidosFarmacia()", "");
                grid.ColorRenglon(iRow, colorEjecucionError);
            }
            else
            {
                if (myWeb.Leer())
                {
                    grid.SetValue(iRow, 4, myWeb.Campo("Status"));
                    grid.SetValue(iRow, 5, myWeb.Campo("Fecha"));
                }
                else
                {
                    grid.SetValue(iRow, 4, "SIN ATENDER");
                }
                grid.ColorRenglon(iRow, colorEjecucionExito);

            }
            iBusquedasEnEjecucion--;
        }

        #endregion Funciones y procedimientos

        #region funciones de sistema

        private void IniciarToolBar(bool Nuevo, bool Ejecutar,bool Servicio, bool ExportarExel)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnActivarServicios.Enabled = Servicio;
            btnExportarExcel.Enabled = ExportarExel;
        }

        private void cboAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAlmacen.SelectedIndex != 0)
            {
                IniciarToolBar(true, true, false, false);
            }
            else
            {
                IniciarToolBar(true, false, false, false);
            }
        }

        private void chkTodas_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue(3, chkTodas.Checked);
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnActivarServicios.Enabled = true;
                btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
            }
        }

        #endregion funciones de sistema
    }
}
