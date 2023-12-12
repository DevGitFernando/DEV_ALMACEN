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
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Conexiones;

namespace Compras_Cuentas_x_Pagar.Registros
{
    public partial class FrmRegistraDePagos_A_Proveedores : FrmBaseExt
    {
        enum Cols 
        { 
            IdEstado = 1, Estado = 2, OrdenComp = 3, Tipo = 4, Factura = 5, 
            FechaReg = 6, FechaColocacion = 7, FechaVenc = 8, Total = 9, 
            Abonos = 10, Dif = 11, PagAct = 12, PagReg = 13, AplicarPago = 14  
        }
        
        
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        clsConsultas Consultas;
        clsAyudas Ayudas;
        clsGrid myGrid;

        string sFormato = "#,###,###,##0.###0";
        string sFolio = "", sMensajeGrabar = "";
        bool bEditandoImportes = false; 
        FrmHistoriales H;

        Color colorSinPago = Color.White;
        Color colorConPago = Color.Yellow;
        Color colorAplicar = Color.White;

        public FrmRegistraDePagos_A_Proveedores()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            
            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloDeGrid = eModoGrid.ModoRow;

            myGrid.OcultarColumna(true, (int)Cols.Abonos, (int)Cols.Abonos);
            myGrid.OcultarColumna(true, (int)Cols.Dif, (int)Cols.Dif);
            myGrid.OcultarColumna(true, (int)Cols.PagAct, (int)Cols.PagAct); 
            myGrid.OcultarColumna(true, (int)Cols.PagReg, (int)Cols.PagReg); 

            myGrid.AjustarAnchoColumnasAutomatico = true;
            myGrid.SetOrder(true); 
        }

        private void FrmRegistraDePagos_A_Proveedores_Load(object sender, EventArgs e)
        {
            Limpiar();
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = "";//, sWhereEstado = "", sWhereTipoDeCompra = "";
            int iTipoDeCompra = 2;

            if (!rdoAmbos.Checked)
            {
                if (rdoCentral.Checked)
                {
                    iTipoDeCompra = 1;
                }

                if (rdoRegional.Checked)
                {
                    iTipoDeCompra = 0;
                }

                //sWhereTipoDeCompra = string.Format(" And dbo.fg_EsCentralFolioOrdenDeCompra(E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.OrdenCompra) = {0}", iTipoDeCompra);
            }

            if(ValidarEjecucion())
            {
                bEditandoImportes = false;
                myGrid.Limpiar(false);
                Application.DoEvents(); 

                sSql = string.Format("Exec spp_Consiliacion_AgregarActualizar @IdEmpresa = '{0}', @IdEstado = '{1}', @IdProveedor = '{2}', @TipoDeOrden = {3}, @CantidadPagos = {4}",
                        txtIdEmpresa.Text.Trim(), txtIdEstado.Text.Trim(), txtIdProveedor.Text.Trim(), iTipoDeCompra, txtImporteADistribuir.Text.Trim().Replace(",", ""));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "btnEjecutar_Click");
                    General.msjError("Ocurrio un error al obtener la información.");
                }

                else
                {
                    if (leer.Leer())
                    {
                        myGrid.LlenarGrid(leer.DataSetClase);
                        txtImporteADistribuir.Enabled = false;
                        for (int iRow = 1; iRow <= myGrid.Rows; iRow++)
                        {
                            if ((myGrid.GetValueDou(iRow, (int)Cols.Total) - myGrid.GetValueDou(iRow, (int)Cols.PagAct)) < 0)
                            {
                                myGrid.ColorRenglon(iRow, Color.Red);
                            }
                        }
                        CalcularImporteDistribuido();
                        toolBar(true, true);

                        bEditandoImportes = true; 
                        tmUpdateCaptura.Enabled = true;
                        tmUpdateCaptura.Start(); 
                    }
                    else
                    {
                        myGrid.Limpiar(false);
                        General.msjAviso("No existe Información con los parámetros seleccionados.");
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarGuardar())
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    bool bExito = false;
                    cnn.IniciarTransaccion();

                    if (GrabarEncabezado(1))
                    {
                        bExito = GrabarDetalle(1);
                    }

                    if (!bExito)
                    {
                        cnn.DeshacerTransaccion();
                        txtFolio.Text = "*";
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();

                        General.msjUser(sMensajeGrabar);
                        //ImprimirInformacion();
                        Limpiar();
                    }

                    cnn.Cerrar();
                }
            }
        }

        private void Limpiar()
        {
            colorAplicar = Color.White;
            bEditandoImportes = false;
            tmUpdateCaptura.Stop();
            tmUpdateCaptura.Enabled = false; 

            Fg.IniciaControles();
            dtpFechaRegistro.Enabled = false; 

            toolBar(false, false);
            HabiliarEncabezado(false);
            txtFolio.Text = "";
            myGrid.Limpiar(false);
            
            rdoAmbos.Checked = true;
            rdoAmbos.Visible = false;
            rdoCentral.Checked = true; 


            txtImporteADistribuir.Enabled = false;
            txtImporteDistribuido.Enabled = false;
            txtIdProveedor.Enabled = true;

            txtIdEmpresa.Enabled = true;
            txtIdEmpresa.Focus();

            if (!General.EsEquipoDeDesarrollo)
            {
                txtIdEmpresa.Text = DtGeneral.EmpresaConectada;
                lblEmpresa.Text = DtGeneral.EmpresaConectadaNombre;
                txtIdEmpresa.Enabled = false;
                txtIdProveedor.Focus();
            }
        }

        private void toolBar(bool Ejecutar, bool Guardar)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnGuardar.Enabled = Guardar;
        }

        #endregion Botones

        #region Eventos Encabezado

        private void txtFolio_Validated(object sender, EventArgs e)
        {
            if (txtFolio.Text == "" || txtFolio.Text == "*")
            {
                txtFolio.Text = "*";
                txtObservaciones.Focus();
            }
            else
            {
                string sSql = string.Format("Exec spp_CPXP_Pagos @IdEmpresa = '{0}', @IdProveedor = '{1}', @Folio = '{2}'",
                                    txtIdEmpresa.Text.Trim(), txtIdProveedor.Text.Trim(), Fg.PonCeros(txtFolio.Text.Trim(), 8));

                if (!leer.Exec(sSql))
                {
                    General.msjError("Error");
                }
                else
                {
                    if (!leer.Leer())
                    {
                        General.msjAviso("No se encontro Información con los parametros seleccionados.");
                    }
                    else
                    {
                        btnGuardar.Enabled = true;
                        btnEjecutar.Enabled = false;
                        txtFolio.Text = leer.Campo("Folio");
                        txtFolio.Enabled = false;
                        txtIdEstado.Enabled = false;
                        txtImporteADistribuir.Text = leer.Campo("Importe");
                        txtImporteDistribuido.Text = leer.Campo("Importe");
                        txtObservaciones.Text = leer.Campo("Observaciones");
                        dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");

                        rdoAmbos.Checked = false;
                        rdoCentral.Checked = false;
                        rdoRegional.Checked = false;
                        leer.DataTableClase = leer.Tabla(2);
                        myGrid.LlenarGrid(leer.DataSetClase);
                    }
                }
            }
        }

        private void txtIdEmpresa_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdEmpresa.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Empresas(txtIdEmpresa.Text.Trim(), "txtIdEmpresa_Validating()");
                if (leer.Leer())
                {
                    CargarEmpresa();
                }
                else
                {
                    txtIdEmpresa.Text = "";
                    lblEmpresa.Text = "";
                    txtIdEmpresa.Focus();
                }
            }
        }

        private void txtIdEmpresa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Empresas("txtIdEmpresa_KeyDown()");
                if (leer.Leer())
                {
                    CargarEmpresa();
                }
            }
        }

        private void CargarEmpresa()
        {
            txtIdEmpresa.Text = leer.Campo("IdEmpresa");
            lblEmpresa.Text = leer.Campo("Nombre");
            txtIdEmpresa.Enabled = false;

            if (txtIdProveedor.Text.Trim() != "")
            {
                HabiliarEncabezado(true);
            }
        }

        private void Cargarproveedor()
        {
            txtIdProveedor.Text = leer.Campo("IdProveedor");
            lblProveedor.Text = leer.Campo("Nombre");
            txtIdProveedor.Enabled = false;

            if (txtIdEmpresa.Text.Trim() != "")
            {
                HabiliarEncabezado(true);
            }
        }

        private void txtIdEmpresa_TextChanged(object sender, EventArgs e)
        {
            lblEmpresa.Text = "";
        }

        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdProveedor.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Proveedores(txtIdProveedor.Text.Trim(), "txtIdProveedor_Validating()");
                if (leer.Leer())
                {
                    Cargarproveedor();
                }
                else
                {
                    txtIdProveedor.Text = "";
                    lblProveedor.Text = "";
                    txtIdProveedor.Focus();
                }
            }
        }

        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Proveedores("txtIdProveedor_KeyDown()");
                if (leer.Leer())
                {
                    Cargarproveedor();
                }
            }
        }

        private void txtIdProveedor_TextChanged(object sender, EventArgs e)
        {
            lblProveedor.Text = "";
        }

        private void txtIdEstado_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdEstado.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Estados(txtIdEstado.Text.Trim(), "txtIdEstado_Validating()");
                if (leer.Leer())
                {
                    txtIdEstado.Text = leer.Campo("IdEstado");
                    lblEstado.Text = leer.Campo("Nombre");
                }
                else
                {
                    txtIdEstado.Text = "";
                    lblEstado.Text = "";
                }
            }
        }

        private void txtIdEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Estados("txtIdEstado_KeyDown()");
                if (leer.Leer())
                {
                    txtIdEstado.Text = leer.Campo("IdEstado");
                    lblEstado.Text = leer.Campo("Nombre");
                    txtIdEstado.Focus();
                }
            }
        }

        private void txtIdEstado_TextChanged(object sender, EventArgs e)
        {
            lblEstado.Text = "";
        }

        #endregion Eventos Encabezado

        #region Eventos
        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            if (myGrid.GetValueDou(myGrid.ActiveRow, (int)Cols.Dif) < myGrid.GetValueDou(myGrid.ActiveRow, (int)Cols.PagAct))
            {
                General.msjAviso("El pago no puede ser mayor al total de la factura.");
                myGrid.SetValue(myGrid.ActiveRow, (int)Cols.PagAct, (myGrid.GetValueDou(myGrid.ActiveRow, (int)Cols.Dif)));
            }

            CalcularImporteDistribuido();
        }

        private void txtImporteADistribuir_Validating(object sender, CancelEventArgs e)
        {
            double ImporteADistribuir = Convert.ToDouble(txtImporteADistribuir.Text.Trim().Replace(",", ""));
            txtImporteADistribuir.Text = ImporteADistribuir.ToString(sFormato);
        }

        #endregion Eventos

        #region Funciones y procedimientos

        private void HabiliarEncabezado(bool Habilitar)
        {
            txtObservaciones.Enabled = Habilitar;
            txtIdEstado.Enabled = Habilitar;
            txtImporteADistribuir.Enabled = Habilitar;
            btnEjecutar.Enabled = Habilitar;
            txtFolio.Enabled = Habilitar;

            if (Habilitar)
            {
                txtFolio.Focus();
            }
        }

        private bool ValidarEjecucion()
        {
            bool bRegresa = true;

            if (txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado observaciones, verifique.");
                txtObservaciones.Focus();
            }

            if (bRegresa && txtIdEmpresa.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado una empresa valida, verifique.");
                txtIdEmpresa.Focus();
            }

            if (bRegresa && txtIdProveedor.Text == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado un proveedor valido, verifique.");
                txtIdProveedor.Focus();
            }

            if (bRegresa && txtIdEstado.Text == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado un estado valido, verifique.");
                txtIdEstado.Focus();
            }

            if (bRegresa && Convert.ToDouble("0" + txtImporteADistribuir.Text.Trim().Replace(",", "")) == 0.0000)
            {
                bRegresa = false;
                General.msjAviso("No ha capturado un importe a distribuir, verifique.");
                txtImporteADistribuir.Focus();
            }

            return bRegresa;
        }

        private bool ValidarGuardar()
        {
            bool bRegresa = true;

            double ImporteDistribuido = 0, ImporteADistribuir = 0;
            CalcularImporteDistribuido();
            ImporteDistribuido = Convert.ToDouble("0" + txtImporteDistribuido.Text.Trim().Replace(",", ""));
            ImporteADistribuir = Convert.ToDouble("0" + txtImporteADistribuir.Text.Trim().Replace(",", ""));

            if (ImporteDistribuido != ImporteADistribuir)
            {
                bRegresa = General.msjConfirmar("El importe a distribuir y el importe distribuido deben ser iguales, ¿ Desea continuar ?.") == System.Windows.Forms.DialogResult.Yes; 
            }


            return bRegresa;
        }

        private void CalcularImporteDistribuido()
        {
            double ImporteDistribuido = 0, ImporteADistribuir = 0;
            
            ImporteDistribuido = myGrid.TotalizarColumnaDou((int)Cols.PagAct);
            ImporteADistribuir = Convert.ToDouble("0" + txtImporteADistribuir.Text.Trim().Replace(",", ""));

            txtImporteDistribuido.Text = ImporteDistribuido.ToString(sFormato);

            txtImporteDistribuido.BackColor = Color.White;
            if (ImporteDistribuido > ImporteADistribuir)
            {
                txtImporteDistribuido.BackColor = Color.Red;
            }
        }

        private bool GrabarEncabezado(int iOpcion)
        {
            bool bRegresa = true;
            sFolio = "";

            string sSql = string.Format("Exec spp_Mtto_CPXP_PagosEnc " +
                                        "@IdEmpresa = '{0}', @IdProveedor = '{1}', @Folio = '{2}', @Observaciones = '{3}', @Importe = '{4}', @iOpcion = '{5}'",
                txtIdEmpresa.Text.Trim(), txtIdProveedor.Text.Trim(), txtFolio.Text.Trim(), txtObservaciones.Text.Trim(), txtImporteDistribuido.Text.Replace(",", ""), iOpcion);
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sFolio = leer.Campo("Folio");
                txtFolio.Text = sFolio;
                sMensajeGrabar = leer.Campo("Mensaje");
            }

            return bRegresa;
        }

        private bool GrabarDetalle(int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "";
            sFolio = "";

            for (int iRow = 1; iRow <= myGrid.Rows && bRegresa; iRow++)
            {
                ////if (myGrid.GetValueDou(iRow, (int)Cols.PagReg) != myGrid.GetValueDou(iRow, (int)Cols.PagAct))
                if ( myGrid.GetValueBool(iRow, (int)Cols.AplicarPago) ) 
                {
                    sSql = string.Format("Exec spp_Mtto_CPXP_PagosDet " +
                                            "@IdEmpresa = '{0}', @IdProveedor = '{1}', @Folio = '{2}', @IdEstado = '{3}', @FolioOrdeneCompra = '{4}', " +
                                            "@TipoDeCompra  = '{5}', @Factura  = '{6}', @Pago = '{7}', @iOpcion = '{8}', " +
                                            "@IdEstado_PersonalRegistra = '{9}', @IdFarmacia_PersonalRegistra = '{10}', @IdPersonal_PersonalRegistra = '{11}'",
                    txtIdEmpresa.Text.Trim(), txtIdProveedor.Text.Trim(), txtFolio.Text.Trim(), myGrid.GetValue(iRow, (int)Cols.IdEstado),
                    myGrid.GetValues(iRow, (int)Cols.OrdenComp), myGrid.GetValues(iRow, (int)Cols.Tipo), myGrid.GetValues(iRow, (int)Cols.Factura),
                    myGrid.GetValueDou(iRow, (int)Cols.Total), iOpcion,
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                    }
                }
            }

            return bRegresa;
        }

        #endregion  Funciones y procedimientos

        private void dtpFechaRegistro_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblTitulo__FechaRegistro_Click(object sender, EventArgs e)
        {

        }

        private void ConciliacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            H = new FrmHistoriales();
            H.CargarHistorialConciliacion(txtIdEmpresa.Text.Trim(), myGrid.GetValue(myGrid.ActiveRow, (int)Cols.IdEstado),
                                        myGrid.GetValue(myGrid.ActiveRow, (int)Cols.OrdenComp), txtIdProveedor.Text.Trim(), myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Factura));
            H.ShowDialog();
        }

        private void PagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            H = new FrmHistoriales();
            H.CargarHistorialPagos(txtIdEmpresa.Text.Trim(), myGrid.GetValue(myGrid.ActiveRow, (int)Cols.IdEstado),
                                        myGrid.GetValue(myGrid.ActiveRow, (int)Cols.OrdenComp), txtIdProveedor.Text.Trim(), myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Factura));
            H.ShowDialog();
        }


        private void tmUpdateCaptura_Tick(object sender, EventArgs e)
        {
            if (!bEditandoImportes)
            {
                tmUpdateCaptura.Stop();
                tmUpdateCaptura.Enabled = false;
            }
            else
            {
                ActualizarInterface(); 
            }
        }

        private void ActualizarInterface()
        {
            Color colorSinPago = Color.White;
            Color colorConPago = Color.Yellow;
            Color colorAplicar = colorSinPago;
            double dImporteDistribuido = 0; 

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                colorAplicar = myGrid.GetValueBool(i, (int)Cols.AplicarPago) ? colorConPago : colorSinPago;
                myGrid.ColorRenglon(i, colorAplicar);

                dImporteDistribuido += myGrid.GetValueBool(i, (int)Cols.AplicarPago) ? myGrid.GetValueDou(i, (int)Cols.Total) : 0; 
            }

            //dImporteDistribuido = myGrid.TotalizarColumnaDou((int)Cols.Total); 
            txtImporteDistribuido.Text = dImporteDistribuido.ToString(sFormato);
        }
    }
}
