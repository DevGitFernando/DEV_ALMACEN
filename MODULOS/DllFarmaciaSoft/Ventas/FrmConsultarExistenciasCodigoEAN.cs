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
    public partial class FrmConsultarExistenciasCodigoEAN : FrmBaseExt
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
        Color colorEjecucionExitoSinResultados = Color.LightSlateGray;


        int iBusquedasEnEjecucion = 0;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);

        string sSqlFarmacias = "";
        string sIdEstado = "";
        string sIdProducto = ""; 


        public FrmConsultarExistenciasCodigoEAN()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");

            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, datosCliente);
            leer = new clsLeer(ref cnn);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            grid = new clsGrid(ref grdExistencia, this);
            grid.EstiloGrid(eModoGrid.SeleccionSimple);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            ////sSqlFarmacias = string.Format(" Select IdFarmacia, (IdFarmacia + ' - ' + Farmacia) as Farmacia, UrlFarmacia " +
            ////                " From vw_Farmacias_Urls (NoLock) " +
            ////                " Where IdEstado = '{1}' and ( IdFarmacia <> '{2}' ) " +
            ////                " and FarmaciaStatus = 'A' and StatusRelacion = 'A' ",
            ////                cboEstados.Data );

            lblConsultando.BackColor = colorEjecutando;
            lblFinExito.BackColor = colorEjecucionExito;
            lblFinError.BackColor = colorEjecucionError;
            lblSinUtilizar.BackColor = colorEjecucionExitoSinResultados; 
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            iBusquedasEnEjecucion = 0;
            grid.Limpiar(false);
            Fg.IniciaControles();

            txtId.Enabled = false; 
            txtClaveSSA.Enabled = false;
            txtCodEAN.Enabled = true; 
            txtCodEAN.Focus();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarFarmaciasGrid();
            IniciarConsultaExistencias();
        }
        #endregion Botones

        private void FrmConsultarExistenciasCodigoEAN_Load(object sender, EventArgs e)
        {
            CargarEstados();
            btnNuevo_Click(null, null);
        }

        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>"); 

            string sSql = "Select Distinct IdEstado, NombreEstado From vw_EmpresasEstados (NoLock) Order By IdEstado ";
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEstados()");
                General.msjError("Ocurrió un error al obtener la lista de Estados.");
            }
            else
            {
                cboEstados.Add(leer.DataSetClase, true); 
            }
            cboEstados.SelectedIndex = 0;
        }

        private void CargarFarmaciasGrid()
        {
            //////if (rdoFarmacia.Checked)
            //////{
            //////    //sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
            //////    //    " From vw_Farmacias_Urls (NoLock) " +
            //////    //    " Where IdEstado = '{0}' and IdFarmacia = '{1}' and FarmaciaStatus = 'A' ",
            //////    //    DtGeneral.EstadoConectado, cboFarmacias.Data);

            //////    sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
            //////                    " From vw_Farmacias_Urls (NoLock) " +
            //////                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and ( IdFarmacia <> '{2}' ) and IdFarmacia = '{3}' " +
            //////                    " and FarmaciaStatus = 'A' and StatusRelacion = 'A' and EsDeConsignacion = '{4}' ",
            //////        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, cboFarmacias.Data, iEsEmpresaConsignacion); 
            //////}
            //////else
            //////{
            //////    //sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
            //////    //    " From vw_Farmacias_Urls (NoLock) " +
            //////    //    " Where IdEstado = '{0}' and FarmaciaStatus = 'A' ",
            //////    //    DtGeneral.EstadoConectado );
                
            //////    sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
            //////                    " From vw_Farmacias_Urls (NoLock) " +
            //////                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and ( IdFarmacia <> '{2}' ) " +
            //////                    " and FarmaciaStatus = 'A' and StatusRelacion = 'A' and EsDeConsignacion = '{3}' ",
            //////        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iEsEmpresaConsignacion ); 
            //////}


            sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
                            " From vw_Farmacias_Urls (NoLock) " +
                            " Where IdEstado = '{0}' and ( IdFarmacia <> '{1}' ) " +
                            " and FarmaciaStatus = 'A' and StatusRelacion = 'A' ",
                            cboEstados.Data, DtGeneral.FarmaciaConectada);

            if (!leer.Exec(sSqlFarmacias))
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de farmacias.");
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

            sIdEstado = cboEstados.Data;
            sIdProducto = Fg.PonCeros(txtId.Text.Trim(), 8);
            sIdProducto = txtCodEAN.Text; 

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
            string sValor = "-- " + sIdEstado + "-" + sIdFarmacia;

            string sSql = string.Format(" Select IdEstado, Estado, IdFarmacia, Farmacia, IdProducto, sum(Existencia) as Existencia  " + 
	            " from vw_ExistenciaPorCodigoEAN_Lotes " +
                " where IdEstado = '{0}' and IdFarmacia = '{1}' and IdProducto = '{2}' " +
                " group by IdEstado, Estado, IdFarmacia, Farmacia, IdProducto ", 
                sIdEstado, sIdFarmacia, sIdProducto );


            sSql = string.Format(" Select IdEstado, Estado, IdFarmacia, Farmacia, sum(Existencia) as Existencia  " +
                            " from vw_ExistenciaPorCodigoEAN_Lotes " +
                            " where IdEstado = '{0}' and IdFarmacia = '{1}' " +
                            " and ( CodigoEAN = '{2}' or right('0000000000000' + IdProducto, 13) = '{3}' ) " +
                            " group by IdEstado, Estado, IdFarmacia, Farmacia ",
                            sIdEstado, sIdFarmacia, sIdProducto, Fg.PonCeros(sIdProducto, 13)); 



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
                if (!myWeb.Leer())
                { 
                    grid.SetValue(iRow, 4, 0);
                    grid.ColorRenglon(iRow, colorEjecucionExitoSinResultados); 
                }
                else 
                {
                    grid.SetValue(iRow, 4, myWeb.Campo("Existencia"));
                    grid.ColorRenglon(iRow, colorEjecucionExito); 
                }
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
                    General.msjUser("Clave SSA no encontrada, verifique.");
                }
            }
        }

        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.ClavesSSA_Sales("txtClaveSSA_KeyDown");
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
                    txtClaveSSA.Text = leer.Campo("IdClaveSSA_Sal");
                    lblClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblDescripcionClave.Text = leer.Campo("DescripcionSal");

                    txtCodEAN.Enabled = false;
                }
                else
                {
                    e.Cancel = true;
                    General.msjUser("Clave de Producto no encontrada, verifique.");
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
                    General.msjError("Ocurió un error al válidar el Codigo EAN");
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
                        General.msjUser("Codigo EAN no encontrado, verifique.");
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

        private void FrmConsultarExistenciasCodigoEAN_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iBusquedasEnEjecucion != 0)
            {
                e.Cancel = true;
            }
        }

        private void FrmConsultarExistenciasCodigoEAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
