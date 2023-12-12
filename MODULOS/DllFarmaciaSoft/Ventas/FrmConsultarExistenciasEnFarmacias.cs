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

namespace DllFarmaciaSoft.Ventas
{
    public partial class FrmConsultarExistenciasEnFarmacias : FrmBaseExt
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
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);

        string sSqlFarmacias = "";
        string sFiltroUnidades = "";

        public FrmConsultarExistenciasEnFarmacias()
        {
            InitializeComponent();
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");

            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, datosCliente);
            leer = new clsLeer(ref cnn);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            grid = new clsGrid(ref grdExistencia, this);
            grid.EstiloGrid(eModoGrid.SeleccionSimple);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            sFiltroUnidades = " and F.IdTipoUnidad <> '005'     "; 
            if (!DtGeneral.EsAlmacen)
            {
                sFiltroUnidades += string.Format(" and F.EsAlmacen = 0 ");
            }

            sSqlFarmacias = string.Format(" Select E.IdFarmacia, (E.IdFarmacia + ' - ' + E.Farmacia) as Farmacia, E.UrlFarmacia " +
                " From vw_Farmacias_Urls E (NoLock) " +
                " Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia and F.Status = 'A' ) " + 
                " Where E.IdEmpresa = '{0}' and E.IdEstado = '{1}' and ( E.IdFarmacia <> '{2}' ) " +
                " and E.FarmaciaStatus = 'A' and E.StatusRelacion = 'A'     {3}" + 
                " Order By E.IdFarmacia ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFiltroUnidades);//, iEsEmpresaConsignacion);


            lblConsultando.BackColor = colorEjecutando;
            lblFinExito.BackColor = colorEjecucionExito;
            lblFinError.BackColor = colorEjecucionError;
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            iBusquedasEnEjecucion = 0;
            grid.Limpiar(false);
            Fg.IniciaControles();
            rdoTodas.Checked = true;
            cboFarmacias.Enabled = false;

            txtClaveSSA.Focus();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarFarmaciasGrid();
            IniciarConsultaExistencias();
        }
        #endregion Botones

        private void FrmConsultarExistenciasEnFarmacias_Load(object sender, EventArgs e)
        {
            CargarFarmacias();
            btnNuevo_Click(null, null);
        }

        private void CargarFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");

            if (!leer.Exec(sSqlFarmacias))
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Error en consulta de Farmacias.");
            }
            else
            {
                cboFarmacias.Add(leer.DataRowsClase, true, "IdFarmacia", "Farmacia");
            }
            cboFarmacias.SelectedIndex = 0;
        }

        private void rdoFarmacia_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoFarmacia.Checked)
            {
                cboFarmacias.Enabled = true;
            }
        }

        private void rdoTodas_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTodas.Checked)
            {
                cboFarmacias.Enabled = false;
            }
        }

        private void CargarFarmaciasGrid()
        {
            if (!DtGeneral.EsAlmacen)
            {
                sFiltroUnidades = string.Format(" and F.EsAlmacen = 0 "); 
            }


            if (rdoFarmacia.Checked)
            {
                sSqlFarmacias = string.Format(" Select E.IdFarmacia, E.Farmacia, E.UrlFarmacia " +
                                " From vw_Farmacias_Urls E (NoLock) " +
                                " Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia and F.Status = 'A' ) " + 
                                " Where E.IdEmpresa = '{0}' and E.IdEstado = '{1}' and ( E.IdFarmacia <> '{2}' ) and E.IdFarmacia = '{3}' " +
                                " and E.FarmaciaStatus = 'A' and E.StatusRelacion = 'A'  {4} " + 
                                " Order By E.IdFarmacia ", 
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, cboFarmacias.Data, sFiltroUnidades);//, iEsEmpresaConsignacion); 
            }
            else
            {                
                sSqlFarmacias = string.Format(" Select E.IdFarmacia, E.Farmacia, E.UrlFarmacia " +
                                " From vw_Farmacias_Urls E (NoLock) " + 
                                " Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia and F.Status = 'A' ) " + 
                                " Where E.IdEmpresa = '{0}' and E.IdEstado = '{1}' and ( E.IdFarmacia <> '{2}' ) " +
                                " and E.FarmaciaStatus = 'A' and E.StatusRelacion = 'A'  {3} " +
                                " Order By E.IdFarmacia ", 
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFiltroUnidades);//, iEsEmpresaConsignacion ); 
            }


            if (!leer.Exec(sSqlFarmacias))
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Error en consulta de Farmacias.");
            }
            else
            {
                grid.Limpiar(false);
                grid.LlenarGrid(leer.DataSetClase);
            }
        }

        private void IniciarConsultaExistencias()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            for (int i = 1; i <= grid.Rows; i++)
            {
                Thread _workerThread = new Thread(this.ConsultarExistenciaFarmacia);
                _workerThread.Name = grid.GetValue(i, 2);
                _workerThread.Start(i);
            }
        }

        private void ConsultarExistenciaFarmacia(object Renglon)
        {
            int iRow = (int)Renglon;
            string sIdFarmacia = grid.GetValue(iRow, 1);
            string sUrl = grid.GetValue(iRow, 3);
            string sValor = "-- " + DtGeneral.EstadoConectado + "-" + sIdFarmacia;

            string sSql = string.Format(" Select IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, sum(Existencia) as Existencia  " + 
	            " from vw_ExistenciaPorCodigoEAN_Lotes " + 
	            " where IdEstado = '{0}' and IdFarmacia = '{1}' and ClaveSSA = '{2}' " + 
	            " group by IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal ", 
                DtGeneral.EstadoConectado, sIdFarmacia, txtClaveSSA.Text ); 

            grid.ColorRenglon(iRow, colorEjecutando);
            grid.SetValue(iRow, 4, "0");
            iBusquedasEnEjecucion++;

            clsLeerWebExt myWeb = new clsLeerWebExt(ref cnn, sUrl, DtGeneral.CfgIniPuntoDeVenta, datosCliente);
            if (!myWeb.Exec(sSql))
            {
                // Error.GrabarError(leer.DatosConexion, new Exception(sValor + " -- " + sUrl), "ConsultarExistenciaFarmacia()");
                Error.LogError(sValor + " -- " + sUrl + " ----  " + myWeb.Error.Message); 
                grid.ColorRenglon(iRow, colorEjecucionError);
            }
            else
            {
                if (myWeb.Leer())
                {
                    grid.SetValue(iRow, 4, myWeb.Campo("Existencia"));
                }

                grid.ColorRenglon(iRow, colorEjecucionExito); 
            }
            iBusquedasEnEjecucion--;
            // grid.SetValue(iRow, 4, sIdFarmacia);
        }


        #region Datos para consulta 
        private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveSSA.Text.Trim() != "")
            {
                leer.DataSetClase = query.ClavesSSA_Sales(txtClaveSSA.Text, true, "txtClaveSSA_Validating");
                if (leer.Leer())
                {
                    txtClaveSSA.Enabled = false;
                    txtClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblDescripcionClave.Text = leer.Campo("Descripcion");

                    txtId.Enabled = false;
                    txtCodEAN.Enabled = false;
                }
                else
                {
                    e.Cancel = true;
                    General.msjUser("No se encontro ClaveSSA, Favor de verificar.");
                }
            }
        }

        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.ClavesSSA_Sales(2, 2, true, "txtClaveSSA_KeyDown");
                if (leer.Leer())
                {
                    // txtClaveSSA.Enabled = false;
                    txtClaveSSA.Text = leer.Campo("ClaveSSA");
                    txtClaveSSA_Validating(null, null);
                }
            }
        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text.Trim() != "")
            {
                leer.DataSetClase = query.Productos(txtId.Text, "txtId_Validating");
                if (leer.Leer())
                {
                    txtId.Enabled = false;
                    txtClaveSSA.Enabled = false;
                    txtId.Text = leer.Campo("IdProducto");
                    lblDescripcion.Text = leer.Campo("Descripcion");
                    txtClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblDescripcionClave.Text = leer.Campo("DescripcionSal");

                    txtCodEAN.Enabled = false;
                }
                else
                {
                    e.Cancel = true;
                    General.msjUser("No se encontro Insumo, Favor de verificar.");
                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Productos("txtId_KeyDown");
                if (leer.Leer())
                {
                    txtId.Text = leer.Campo("IdProducto");
                    txtId_Validating(null, null);
                }
            }
        }

        private void txtCodEAN_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodEAN.Text.Trim() != "")
            {
                string sSql = string.Format(" Select IdProducto, CodigoEAN, CodigoEAN_Interno " +
                    " From CatProductos_CodigosRelacionados (NoLock) Where CodigoEAN = '{0}' or CodigoEAN_Interno = '{1}' ",
                    txtCodEAN.Text, Fg.PonCeros(txtCodEAN.Text, 13));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtCodEAN_Validating");
                    General.msjError("Error en consulta de Insumo.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtCodEAN.Enabled = false;
                        txtCodEAN.Text = leer.Campo("CodigoEAN");
                        txtId.Text = leer.Campo("IdProducto");
                        txtId_Validating(null, null);
                    }
                    else
                    {
                        e.Cancel = true;
                        General.msjUser("No se encontro Insumo, Favor de verificar.");
                    }
                }
            }
        }
        #endregion Datos para consulta

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
            }
        }

        private void FrmConsultarExistenciasEnFarmacias_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iBusquedasEnEjecucion != 0)
            {
                e.Cancel = true;
            }
        }

        private void FrmConsultarExistenciasEnFarmacias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
