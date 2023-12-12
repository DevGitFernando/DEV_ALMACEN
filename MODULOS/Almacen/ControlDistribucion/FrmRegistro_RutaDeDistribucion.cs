using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.ExportarDatos;
using DllFarmaciaSoft;

using DllFarmaciaSoft.Reporteador;
using SC_SolutionsSystem.FP;
using SC_SolutionsSystem.FP.Huellas;
using SC_SolutionsSystem.Comun;

namespace Almacen.ControlDistribucion
{
    public partial class FrmRegistro_RutaDeDistribucion : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            Ruta = 1,
            Folio, Fecha, Referencia, Piezas, Modificado, Agregar
        }


        private enum ColsDoctos
        {
            Ninguna = 0 , 
            Ruta = 1,  
            Folio, Fecha, Referencia, Bultos, Piezas 
        }


        //private enum ColsA
        //{
        //    Folio = 1, Fecha = 2, Referencia = 3, Bultos = 4, Piezas = 5
        //}
        bool bCancelado, bGuardado;
        string sFolio;

        clsConsultas Consultas;
        clsLeer Leer, LeerAux, LeerAux2, leerClientePrograma, leerCuadrosDeAtencion;

        clsGrid myGridTrasf;
        clsGrid myGridVent;

        clsDatosCliente DatosCliente;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);

        FolderBrowserDialog folder;
        string sRutaDestino = "";
        string sRutaDestino_Archivos = "";
        string sIdPuesto = "02";
        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;

        bool bEjecutando = false;

        bool bFolderDestino = false;
        int iTipoDatos = 0;

        public FrmRegistro_RutaDeDistribucion()
        {
            InitializeComponent();

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            Leer = new clsLeer(ref cnn);
            LeerAux = new clsLeer(ref cnn);
            LeerAux2 = new clsLeer(ref cnn);
            leerClientePrograma = new clsLeer(ref cnn);
            leerCuadrosDeAtencion = new clsLeer(ref cnn);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            myGridTrasf = new clsGrid(ref grdTransferencias, this);
            myGridVent = new clsGrid(ref grdVentas, this);

            myGridTrasf.AjustarAnchoColumnasAutomatico = true;
            myGridVent.AjustarAnchoColumnasAutomatico = true;

            myGridTrasf.EstiloDeGrid = eModoGrid.ModoRow;
            myGridVent.EstiloDeGrid = eModoGrid.ModoRow;

            CargarCombos();
            CargarRutas();
            Cargar_Prioridades();

            LimpiarPantalla();
        }

        private void FrmRegistro_RutaDeDistribucion_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();

            FP_General.Conexion = General.DatosConexion;
            FP_General.TablaHuellas = "FP_Huellas_Cedis";
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                Guardar(1);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                Guardar(2);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        //private void btnDirectorio_Click(object sender, EventArgs e)
        //{
        //    folder = new FolderBrowserDialog();
        //    folder.Description = "Directorio destino para los documentos generados.";
        //    folder.RootFolder = Environment.SpecialFolder.Desktop;
        //    folder.ShowNewFolderButton = true;

        //    if (folder.ShowDialog() == DialogResult.OK)
        //    {
        //        sRutaDestino = folder.SelectedPath + @"\";
        //        //lblDirectorioTrabajo.Text = sRutaDestino;
        //        bFolderDestino = true;
        //        CrearDirectorioDestino();
        //        btnGenerarDocumentos.Enabled = true;
        //    } 
        //}
        #endregion Botones        

        #region Funciones y procedimientos
        private void Imprimir()
        {
            bool bRegresa = true;            

            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("@Folio", txtFolio.Text);
            myRpt.NombreReporte = "PtoVta_RutasDistribuciones";

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            
            if (!bRegresa)
            {
                General.msjError("Error al generar Informe.");
            }
        }       

        private void CargarCombos()
        {
            cboChofer.Clear();
            cboChofer.Add();
            string sSql = string.Format("SELECT IdPersonal, IdPersonal + ' -- ' + Personal As Personal FROM vw_PersonalCEDIS WHERE IdPuesto = '{0}' AND IdEstado = '{1}' AND Idfarmacia = '{2}' ", 
                Fg.PonCeros((int)Puestos_CEDIS.Chofer, 2), DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if(Leer.Exec(sSql))
            {
                if (Leer.Leer())
                {
                    cboChofer.Add(Leer.DataSetClase , true, "IdPersonal", "Personal");
                }
            }
            else
            {
                Error.GrabarError(Leer, "CargarCombos");
                General.msjError("Error al consultar información de choferes.");
            }
            
            cboChofer.SelectedIndex = 0;

            cboVehiculo.Clear();
            cboVehiculo.Add();
            sSql = String.Format("Select IdVehiculo, (Idvehiculo + ' -- ' + Descripcion) As Vehiculo From vw_Vehiculos Where IdEstado = '{0}' And Idfarmacia = '{1}'",
                                 DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (Leer.Exec(sSql))
            {
                if (Leer.Leer())
                {
                    cboVehiculo.Add(Leer.DataSetClase, true, "Idvehiculo", "Vehiculo");
                }
            }
            else
            {
                Error.GrabarError(Leer, "CargarCombos");
                General.msjError("Error al consultar información de vehículos.");
            }

            cboVehiculo.SelectedIndex = 0; 
        }

        private void LimpiarPantalla()
        {
            Fg.IniciaControles();

            rdoMovimientos.Enabled = true;
            rdoPedidos.Enabled = true;
            rdoAmbos.Enabled = true;
            nmDias.Enabled = true;

            lblCancelado.Text = "CANCELADO";
            lblCancelado.Visible = false;
            dtpFechaRegistro.Enabled = false;
            bGuardado = false;
            bFolderDestino = false;
            txtFolio.Text = "";
            txtFolio.Focus();
            cboChofer.SelectedIndex = 0;
            cboVehiculo.SelectedIndex = 0;
            txtObservaciones.Text = "";
            myGridTrasf.Limpiar(false);
            myGridVent.Limpiar(false);

            bCancelado = false;

            chkDesglosado.Visible = GnFarmacia.ImplementaImpresionDesglosada_VtaTS;

            rdoAmbos.Checked = true;
            nmDias.Value = 7; 

            IniciarToolBar(true, false, false);
            btnGenerarDocumentos.Enabled = false;
            btnFirmarDoctos.Enabled = false;
        }

        private void Guardar(int i)
        {
            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion(); 
            }
            else 
            {

                cnn.IniciarTransaccion();

                if (GuardarEnc(i))
                {
                    cnn.CompletarTransaccion();
                    if (Leer.Leer())
                    {
                        txtFolio.Text = Leer.Campo("Folio");
                        General.msjUser(Leer.Campo("Mensaje"));
                        Imprimir();
                        LimpiarPantalla();
                    }
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    txtFolio.Text = "*";
                    Error.GrabarError(Leer, "btnGuardar_Click");
                    General.msjError("Error al guardar información de la ruta de distribución.");
                }

                cnn.Cerrar();
            }
        }

        private bool GuardarEnc(int i)
        {
            bool bRegresa = true;

            string sSql = string.Format("Exec spp_Mtto_RutasDistribucionEnc " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @IdVehiculo = '{4}', " + 
                " @IdPersonal = '{5}', @Observaciones = '{6}', @iOpcion = '{7}', @IdPersonalCaptura = '{8}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text, cboVehiculo.Data, 
                cboChofer.Data, txtObservaciones.Text, i, DtGeneral.IdPersonal);

            if (!Leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (Leer.Leer())
                {
                    sFolio = Leer.Campo("Folio");
                }

                bRegresa = GuardarDetTransf(i);

                if (bRegresa)
                {
                    bRegresa = GuardarDetVent(i);
                }
                                        
            }

            return bRegresa;
        }

        private bool Validar()
        {
            bool bRegresa = true;

            if (cboChofer.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjAviso("Seleccione un chofer. Favor de verificar.");
                cboChofer.Focus(); 
            }

            if (bRegresa && cboVehiculo.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjAviso("Seleccione un vehículo. Favor de verificar.");
                cboVehiculo.Focus(); 
            }

            if (bRegresa && txtObservaciones.Text == "")
            {
                bRegresa = false;
                General.msjAviso("Capture observaciones. Favor de verificar.");
                txtObservaciones.Focus(); 
            }

            if (bRegresa && (myGridTrasf.Rows == 0 && myGridVent.Rows == 0))
            {
                bRegresa = false;
                General.msjAviso("Ingrese algun Traspaso o Dispersión para continuar!");
            }


            //if (!bFolderDestino)
            //{
            //    bRegresa = false;
            //    General.msjUser("No ha especificado el directorio donde se generaran los documentos, verifique.");
            //    btnDirectorio.Focus();
            //}

            for (int i = 1; i <= myGridTrasf.Rows && bRegresa; i++)
            {
                if (myGridTrasf.GetValueInt(i, Cols.Piezas) == 0 && myGridTrasf.GetValue(i, Cols.Folio) != "")
                {
                    bRegresa = false;
                    General.msjAviso("Algun Traspaso no se le capturo el numero de bultos. Favor de verificar.");
                }
            }

            for (int i = 1; i <= myGridVent.Rows && bRegresa; i++)
            {
                if (myGridVent.GetValueInt(i, Cols.Piezas) == 0 && myGridVent.GetValue(i, Cols.Folio) != "")
                {
                    bRegresa = false;
                    General.msjAviso("Alguna Dispersión no se le capturo el numero de bultos. Favor de verificar.");
                }
            }

            return bRegresa;
        }

        private void CargarDatosRuta()
        {
            IniciarToolBar(false, true, true);
            btnFirmarDoctos.Enabled = true;

            txtFolio.Text = Leer.Campo("Folio");
            txtFolio.Enabled = false;
            cboChofer.Data = Leer.Campo("IdPersonal");
            cboChofer.Enabled = false;
            cboVehiculo.Data = Leer.Campo("IdVehiculo");
            cboVehiculo.Enabled = false;
            txtObservaciones.Text = Leer.Campo("Observaciones");
            txtObservaciones.Enabled = false;
            dtpFechaRegistro.Value = Leer.CampoFecha("FechaRegistro");

            myGridTrasf.Limpiar(false);
            myGridVent.Limpiar(false);

            if (Leer.Campo("Status").ToUpper() == "C")
            {
                lblCancelado.Visible = true;
                bCancelado = true;
                IniciarToolBar(false, false, false);
            }

            if(Leer.CampoBool("Firmado") || !DtGeneral.ConfirmacionConHuellas)
            {
                btnFirmarDoctos.Enabled = false;
                btnGenerarDocumentos.Enabled = true;
            }


            Leer.DataSetClase = Consultas.RutasDistribucionDet(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");
            if (Leer.Leer())
            {
                bGuardado = true;
                LeerAux.Reset();
                LeerAux.DataRowsClase  = Leer.DataTableClase.Select("Tipo = 'T'");

                if (LeerAux.Leer())
                {
                    myGridTrasf.LlenarGrid(LeerAux.DataSetClase);
                    myGridTrasf.BloqueaGrid(true);
                }

                LeerAux.Reset();
                LeerAux.DataRowsClase = Leer.DataTableClase.Select("Tipo = 'V'");

                if (LeerAux.Leer())
                {
                    myGridVent.LlenarGrid(LeerAux.DataSetClase);
                    myGridVent.BloqueaGrid(true);
                }
            }
        }

        #region Transferencias
        private bool BuscarRepetidaTransf()
        {
            bool bRegresa = true;

            string sFolio = "";

            sFolio = Fg.PonCeros(myGridTrasf.GetValue(myGridTrasf.ActiveRow, Cols.Folio), 8);

            string sSql = string.Format("Select * From  RutasDistribucionDet Where Status = 'A' And Tipo = 'T' And IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioTransferenciaVenta = '{3}'",
                                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolio);
            if (Leer.Exec(sSql))
            {
                if (Leer.Leer())
                {
                    bRegresa = false;
                    General.msjAviso("Traspaso se encuentra en la ruta: " + Leer.Campo("Folio") + "  !");
                    myGridTrasf.LimpiarRenglon(myGridTrasf.ActiveRow);
                    myGridTrasf.SetActiveCell(myGridTrasf.ActiveRow, Cols.Folio);
                }
            }
            else
            {
                bRegresa = false;
                General.msjError("Error al consultar información de Traspaso.");
                Error.GrabarError(Leer, "BuscarTransferencia");
            }

            return bRegresa;
        }

        private bool GuardarDetTransf(int b)
        {
            bool bRegresa = true;

            string sSql = "";
            string sTransferencia = "";

            for (int i = 1; i <= myGridTrasf.Rows; i++)
            {
                sTransferencia = myGridTrasf.GetValue(i, Cols.Folio);

                if (sTransferencia != "")
                {
                    ////sSql = string.Format("Exec spp_Mtto_RutasDistribucionDet '{0}', '{1}', '{2}', '{3}', '{4}', {5}, 'T', '{6}', {7} ", 
                    sSql = string.Format("Exec spp_Mtto_RutasDistribucionDet " + 
                        "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @FolioTransferenciaVenta = '{4}', " + 
                        " @bultos = '{5}', @Tipo = '{6}', @IdPersonal = '{7}', @iOpcion = '{8}' ", 
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                        sFolio, sTransferencia, myGridTrasf.GetValueInt(i, Cols.Piezas), 'T', cboChofer.Data, b);
                    if (!Leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }


            return bRegresa;
        }

        #endregion Transferencias

        #region Ventas

        private bool BuscarRepetidaVent()
        {
            bool bRegresa = true;

            string sFolio = "";

            sFolio = Fg.PonCeros(myGridVent.GetValue(myGridVent.ActiveRow, Cols.Folio), 8);

            string sSql = string.Format("Select * " + 
                " From  RutasDistribucionDet " + 
                " Where Status = 'A' And Tipo = 'V' And IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioTransferenciaVenta = '{3}'",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolio);
            if (Leer.Exec(sSql))
            {
                if (Leer.Leer())
                {
                    bRegresa = false;
                    General.msjAviso("Dispersión se encuentra en la ruta: " + Leer.Campo("Folio") + "  !");
                    myGridTrasf.LimpiarRenglon(myGridTrasf.ActiveRow);
                    myGridTrasf.SetActiveCell(myGridTrasf.ActiveRow, Cols.Folio);
                }
            }
            else
            {
                bRegresa = false;
                General.msjError("Error al consultar información de Dispersión.");
                Error.GrabarError(Leer, "BuscarRepetidaVent");
            }

            return bRegresa;
        }

        private bool GuardarDetVent(int b)
        {
            bool bRegresa = true;

            string sSql = "";
            string sVenta = "";

            for (int i = 1; i <= myGridVent.Rows; i++)
            {
                sVenta = myGridVent.GetValue(i, Cols.Folio);

                if (sVenta != "")
                {
                    sSql = string.Format("Exec spp_Mtto_RutasDistribucionDet " + // '{0}', '{1}', '{2}', '{3}', '{4}', {5}, 'V', '{6}', {7} ",
                        "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @FolioTransferenciaVenta = '{4}', " + 
                        " @bultos = '{5}', @Tipo = '{6}', @IdPersonal = '{7}', @iOpcion = '{8}' ", 
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                        sFolio, sVenta, myGridVent.GetValueInt(i, Cols.Piezas), 'V', cboChofer.Data, b);
                    if (!Leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }


            return bRegresa;
        }

        //private void CargarDatosRenglonVent()
        //{
        //    int iCol = 1, Row = 0;

        //    myGridVent.Rows = myGridVent.Rows + 1;

        //    Row = myGridVent.Rows;

        //    //if (!myGridVent.BuscaRepetido(Leer.Campo("Folio"), myGridVent.ActiveRow, iCol))
        //    //{

        //        myGridVent.SetValue(Row, iCol++, Leer.Campo("Folio"));
        //        myGridVent.SetValue(Row, iCol++, Leer.Campo("Fecha"));
        //        myGridVent.SetValue(Row, iCol++, Leer.Campo("Beneficiario"));
        //        myGridTrasf.SetValue(Row, iCol++, Leer.Campo("Bultos"));
        //        myGridVent.SetValue(Row, iCol++, Leer.Campo("Piezas"));
        //    //}
        //    //else
        //    //{
        //    //    General.msjAviso("La venta ya se encuentra en la lista...");
        //    //    myGridVent.LimpiarRenglon(myGridVent.ActiveRow);
        //    //    myGridVent.SetActiveCell(myGridVent.ActiveRow, 1);
        //    //}
        //}

        //private bool BuscarVentas(string Folio)
        //{
        //    bool bRegresa = true;

        //    string sSql = string.Format("Select Folio, Convert(Varchar(10), EV.FechaRegistro, 120) As Fecha, Beneficiario, 0 As Bultos, Sum(CantidadVendida) As Piezas " +
        //                  "From VentasEnc EV (NoLock) " +
        //                  "Inner Join VentasDet DV (NoLock) " +
        //                  "    On (EV.IdEmpresa = DV.IdEmpresa And EV.IdEstado = DV.IdEstado And EV.IdFarmacia = DV.IdFarmacia And EV.FolioVenta = DV.FolioVenta) " +
        //                  "Inner Join vw_VentasDispensacion_InformacionAdicional A (NoLock) " +
        //                  "     On (EV.IdEmpresa = A.IdEmpresa And EV.IdEstado = A.IdEstado And EV.IdFarmacia = A.IdFarmacia And EV.FolioVenta = A.Folio " +
        //                  "         And EV.IdCliente = A.IdCliente And EV.IdSubCliente = A.IdSubCliente) " +
        //                  "Where EV.IdEmpresa = '{0}' And EV.IdEstado = '{1}' And EV.IdFarmacia = '{2}' And EV.FolioVenta = '{3}' " +
        //                  "	Group By Folio, EV.FechaRegistro, Beneficiario",
        //                                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Folio);
        //    if (Leer.Exec(sSql))
        //    {
        //        if (!Leer.Leer())
        //        {
        //            bRegresa = false;
        //            General.msjAviso("No se encontro la venta, Verifique porfavor...");
        //            myGridVent.LimpiarRenglon(myGridVent.ActiveRow);
        //            myGridVent.SetActiveCell(myGridVent.ActiveRow, 1);
        //        }
        //        else
        //        {
        //            CargarDatosRenglonVent();
        //        }
        //    }
        //    else
        //    {
        //        bRegresa = false;
        //        General.msjError("Ocurrió un error al obtener la información de la venta...");
        //        Error.GrabarError(Leer, "BuscarVentas");
        //    }

        //    return bRegresa;
        //}

        #endregion Ventas

        #endregion Funciones y procedimientos

        #region Eventos

        #region Grids
        #region Transferencias
        private void grdTransferencias_EditModeOff(object sender, EventArgs e)
        {
            //if (myGridTrasf.ActiveCol == 1)
            //{
            //        string sValor = myGridTrasf.GetValue(myGridTrasf.ActiveRow, 1);
            //        if (sValor != "")
            //        {
            //            if (BuscarRepetidaTransf())
            //            {
            //                if (!BuscarTransferencia())
            //                {
            //                    myGridTrasf.LimpiarRenglon(myGridTrasf.ActiveRow);
            //                    myGridTrasf.SetActiveCell(myGridTrasf.ActiveRow, 1);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            myGridTrasf.LimpiarRenglon(myGridTrasf.ActiveRow);
            //        }
            //}
        }

        private void grdTransferencias_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            //if (!bCancelado)
            //{
            //    if ((myGridTrasf.ActiveRow == myGridTrasf.Rows) && e.AdvanceNext)
            //    {
            //        if (myGridTrasf.GetValue(myGridTrasf.ActiveRow, 1) != "" )
            //        {
            //            myGridTrasf.Rows = myGridTrasf.Rows + 1;
            //            myGridTrasf.ActiveRow = myGridTrasf.Rows;
            //            myGridTrasf.SetActiveCell(myGridTrasf.Rows, 1);
            //        }
            //    }
            //}
        }

        private void grdTransferencias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                myGridTrasf.DeleteRow(myGridTrasf.ActiveRow);
            }

            //if (myGridTrasf.Rows == 0)
            //    myGridTrasf.Limpiar(true);
        }
        #endregion Transferencias

        #region Ventas

        private void grdVentas_EditModeOff(object sender, EventArgs e)
        {
            //if (myGridVent.ActiveCol == 1)
            //{
            //    string sValor = myGridVent.GetValue(myGridVent.ActiveRow, 1);
            //    if (sValor != "")
            //    {
            //        if (BuscarRepetidaVent())
            //        {
            //            if (!BuscarVentas())
            //            {
            //                myGridVent.LimpiarRenglon(myGridVent.ActiveRow);
            //                myGridVent.SetActiveCell(myGridVent.ActiveRow, 1);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        myGridVent.LimpiarRenglon(myGridVent.ActiveRow);
            //    }
            //}
        }

        private void grdVentas_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            //if (!bCancelado)
            //{
            //    if ((myGridVent.ActiveRow == myGridVent.Rows) && e.AdvanceNext)
            //    {
            //        if (myGridVent.GetValue(myGridVent.ActiveRow, 1) != "")
            //        {
            //            myGridVent.Rows = myGridVent.Rows + 1;
            //            myGridVent.ActiveRow = myGridVent.Rows;
            //            myGridVent.SetActiveCell(myGridVent.Rows, 1);
            //        }
            //    }
            //}
        }

        private void grdVentas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                myGridVent.DeleteRow(myGridVent.ActiveRow);
            }

            //if (myGridVent.Rows == 0)
            //    myGridVent.Limpiar(true);
        }

        #endregion Ventas

        #endregion Grids

        private void TeclasRapidas(KeyEventArgs e)
        {
            ////switch (e.KeyCode)
            ////{
            ////    case Keys.G:
            ////        if(btnGuardar.Enabled)
            ////        {
            ////            btnGuardar_Click(null, null);
            ////        }
            ////        break;

            ////    case Keys.N:
            ////        if(btnNuevo.Enabled)
            ////        {
            ////            btnNuevo_Click(null, null);
            ////        }
            ////        break;

            ////    case Keys.P:
            ////        if(btnImprimir.Enabled)
            ////        {
            ////            btnImprimir_Click(null, null);
            ////        }
            ////        break;

            ////    default:
            ////        break;
            ////}
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control)
            {
                TeclasRapidas(e);
            }

            switch (e.KeyCode)
            {
                case Keys.F5:

                    if (!bGuardado)
                    {
                        MostrarDetalles_Folios();
                    }


                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
            }
            else
            {
                Leer.DataSetClase = Consultas.RutasDistribucionEnc(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");

                if (Leer.Leer())
                {
                    CargarDatosRuta();
                }
            }
        }
        #endregion Eventos

        #region Mostrar detalles 
        private void MostrarDetalles_Folios()
        {

            iTipoDatos = 0;
            if(rdoPedidos.Checked)
            {
                iTipoDatos = 1;
            }

            if(rdoMovimientos.Checked)
            {
                iTipoDatos = 2;
            }

            FrmRegistro_RutaDeDistribucionDetalles f = new FrmRegistro_RutaDeDistribucionDetalles(tabControl.SelectedIndex, iTipoDatos, (int)nmDias.Value, cboRuta.Data, cboPrioridades.Data);
            f.ShowDialog();

            if(rdoMovimientos.Enabled)
            {
                rdoMovimientos.Enabled = false;
                rdoPedidos.Enabled = false;
                rdoAmbos.Enabled = false;
                nmDias.Enabled = false;
            }

            if(tabControl.SelectedIndex == 0)
            {
                myGridTrasf.Limpiar(false);

                for(int i = 1; i <= f.myGrid.Rows; i++)
                {
                    if(f.myGrid.GetValueBool(i, (int)Cols.Modificado))
                    {
                        if(f.myGrid.GetValueBool(i, (int)Cols.Agregar))
                        {
                            int iCol = 1, Row = 0;

                            myGridTrasf.Rows = myGridTrasf.Rows + 1;
                            Row = myGridTrasf.Rows;

                            myGridTrasf.SetValue(Row, (int)ColsDoctos.Ruta, f.myGrid.GetValue(i, (int)Cols.Ruta));
                            myGridTrasf.SetValue(Row, (int)ColsDoctos.Folio, f.myGrid.GetValue(i, (int)Cols.Folio));
                            myGridTrasf.SetValue(Row, (int)ColsDoctos.Fecha, f.myGrid.GetValue(i, (int)Cols.Fecha));
                            myGridTrasf.SetValue(Row, (int)ColsDoctos.Referencia, f.myGrid.GetValue(i, (int)Cols.Referencia));
                            myGridTrasf.SetValue(Row, (int)ColsDoctos.Bultos, 0);
                            myGridTrasf.SetValue(Row, (int)ColsDoctos.Piezas, f.myGrid.GetValueInt(i, (int)Cols.Piezas));
                        }
                    }
                }
            }
            else
            {
                myGridVent.Limpiar(false);

                for(int i = 1; i <= f.myGrid.Rows; i++)
                {
                    if(f.myGrid.GetValueBool(i, (int)Cols.Modificado))
                    {
                        if(f.myGrid.GetValueBool(i, (int)Cols.Agregar))
                        {
                            int iCol = 1, Row = 0;

                            myGridVent.Rows = myGridVent.Rows + 1;
                            Row = myGridVent.Rows;

                            ////myGridVent.SetValue(Row, iCol++, f.myGrid.GetValue(i, (int)Cols.Folio));
                            ////myGridVent.SetValue(Row, iCol++, f.myGrid.GetValue(i, (int)Cols.Fecha));
                            ////myGridVent.SetValue(Row, iCol++, f.myGrid.GetValue(i, (int)Cols.Referencia));
                            ////myGridVent.SetValue(Row, iCol++, 0);
                            ////myGridVent.SetValue(Row, iCol++, f.myGrid.GetValueInt(i, (int)Cols.Piezas));

                            myGridVent.SetValue(Row, (int)ColsDoctos.Ruta, f.myGrid.GetValue(i, (int)Cols.Ruta));
                            myGridVent.SetValue(Row, (int)ColsDoctos.Folio, f.myGrid.GetValue(i, (int)Cols.Folio));
                            myGridVent.SetValue(Row, (int)ColsDoctos.Fecha, f.myGrid.GetValue(i, (int)Cols.Fecha));
                            myGridVent.SetValue(Row, (int)ColsDoctos.Referencia, f.myGrid.GetValue(i, (int)Cols.Referencia));
                            myGridVent.SetValue(Row, (int)ColsDoctos.Bultos, 0);
                            myGridVent.SetValue(Row, (int)ColsDoctos.Piezas, f.myGrid.GetValueInt(i, (int)Cols.Piezas));
                        }
                    }
                }
            }

        }

        private void CargarRutas()
        {
            cboRuta.Clear();
            cboRuta.Add("*", "<< Todas las Rutas >>");
            cboRuta.Add("0000", "<< Sin Ruta Asignada >>");

            cboRuta.Add(Consultas.Rutas(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "CargarRutas()"), true, "IdRuta", "Descripcion");

            cboRuta.SelectedIndex = 0;

        }

        private void Cargar_Prioridades()
        {
            string sSql = "Select Prioridad, (cast(Prioridad as varchar(10)) + ' - ' + Descripcion ) as Descripcion From Pedidos_Prioridades (NoLock) Where Status = 'A' Order by Prioridad ";

            cboPrioridades.Clear();
            cboPrioridades.Add("*", "<< Seleccione >>");

            if(!Leer.Exec(sSql))
            {
                Error.GrabarError(Leer, "Cargar_Prioridades");
                General.msjError("Error al consultar listado de Prioridades");
            }
            else
            {
                cboPrioridades.Add(Leer.DataSetClase, true, "Prioridad", "Descripcion");
            }
            cboPrioridades.SelectedIndex = 0;
        }
        #endregion Mostrar detalles 

        #region Generacion_Cartas_Canje
        private void CrearDirectorioDestino()
        {
            string sMarcaTiempo = General.FechaSinDelimitadores(General.FechaSistema) + "_" + General.Hora(General.FechaSistema, "_");
            string sDir = string.Format("Distribucion_{0}_{1}", txtFolio.Text.Trim(), sMarcaTiempo);
                //General.FechaYMD(General.FechaSistema, "-"));            

            sRutaDestino_Archivos = Path.Combine(sRutaDestino, sDir);
            if (!Directory.Exists(sRutaDestino_Archivos))
            {
                Directory.CreateDirectory(sRutaDestino_Archivos);
            }
        }

        private void GenerarCartasCanje()
        {            
            string sSql = "", sTipo = "", sFolio = "";
            bool bExitisteCarta = false;
            sSql = string.Format(" Select * From RutasDistribucionDet " +
	                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Fg.PonCeros(txtFolio.Text, 8));

            if (!Leer.Exec(sSql))
            {
                Error.GrabarError(Leer, "GenerarCartasCanje");
                General.msjError("Error al consultar folio.");
            }
            else
            {
                while (Leer.Leer())
                {
                    sTipo = Leer.Campo("Tipo");
                    sFolio = Leer.Campo("FolioTransferenciaVenta");
                    GenerarDocto(sTipo, sFolio);
                    //if (GenerarDocto(sTipo, sFolio))
                    //{
                    //    bExitisteCarta = true;
                    //}
                }

                //if (!bExitisteCarta)
                //{
                //    General.msjAviso("No existe información para mostrar...");
                //}
                //else
                //{
                //    General.AbrirDirectorio(sRutaDestino_Archivos); 
                //}
            }

        }


        private void GenerarPDFCajas()
        {
            DatosCliente.Funcion = "ImprimirRptCajas()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            clsReporteador Reporteador;

            bool bRegresa = true;

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.TituloReporte = "Impresión de vale";

            myRpt.Add("@IdEmpresa", sIdEmpresa);
            myRpt.Add("@IdEstado", sIdEstado);
            myRpt.Add("@IdFarmacia", sIdFarmacia);
            myRpt.NombreReporte = "PtoVta_Caja_Embarque";

            Reporteador = new clsReporteador(myRpt, DatosCliente);
            Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
            Reporteador.Url = General.Url;
            Reporteador.MostrarInterface = false;
            Reporteador.MostrarMensaje_ReporteSinDatos = false;


            for (int iRows = 1; myGridTrasf.Rows >= iRows && bRegresa; iRows++)
            {
                sFolio = myGridTrasf.GetValue(iRows, (int)Cols.Folio);
                sFolio = "TS" + sFolio;
                string sFile = sFolio + "___Relación de contenido por caja";
                sFile = Path.Combine(sRutaDestino_Archivos, sFile + ".pdf");

                myRpt.Add("@Folioreferencia", sFolio);
                bRegresa = Reporteador.ExportarReporte(sFile, FormatosExportacion.PortableDocFormat, true);
            }

            for (int iRows = 1; myGridVent.Rows >= iRows && bRegresa; iRows++)
            {
                sFolio = myGridVent.GetValue(iRows, (int)Cols.Folio);
                sFolio = "SV" + sFolio;
                string sFile = sFolio + "___Relación de contenido por caja";
                sFile = Path.Combine(sRutaDestino_Archivos, sFile + ".pdf");

                myRpt.Add("@Folioreferencia", sFolio);
                bRegresa = Reporteador.ExportarReporte(sFile, FormatosExportacion.PortableDocFormat, true);
            }

            if (!bRegresa)
            {
                General.msjError("Error al cargar Informe de Vale.");
            }
        }


        private void GenerarPdfVentas()
        {
            int iPerfil = 0;
            string sTitulo = "";
            string sFolio = "";
            string sIdCliente = "";
            string sIdSubCliente = "";
            string sIdPrograma = "";
            string sIdSubPrograma = "";
            bool bTieneVenta = false;
            bool bTieneConsignacion = false;

            bEjecutando = true;
            for (int i = 1; i <= myGridVent.Rows; i++)
            {
                sFolio = myGridVent.GetValue(i, (int)Cols.Folio);

                Obtener_Cliente_Programa(sFolio);

                leerClientePrograma.Leer();
                sIdCliente = leerClientePrograma.Campo("IdCliente");
                sIdSubCliente = leerClientePrograma.Campo("IdSubCliente");
                sIdPrograma = leerClientePrograma.Campo("IdPrograma");
                sIdSubPrograma = leerClientePrograma.Campo("IdsubPrograma");

                //Se procesa cada Perfil de atencion  
                Obtener_Cuadros_De_Atencion(sFolio);
                leerCuadrosDeAtencion.RegistroActual = 1;
                while (leerCuadrosDeAtencion.Leer())
                {
                    iPerfil = leerCuadrosDeAtencion.CampoInt("IdPerfilAtencion");
                    sTitulo = leerCuadrosDeAtencion.Campo("Titulo");
                    bTieneVenta = leerCuadrosDeAtencion.CampoBool("TieneVenta");
                    bTieneConsignacion = leerCuadrosDeAtencion.CampoBool("TieneConsignacion");

                    Generar_PDF(sFolio, sIdPrograma, sIdSubPrograma, iPerfil, sTitulo, bTieneVenta, bTieneConsignacion);
                }
            }
            bEjecutando = false;

            // bloqueo principal 
            Cursor.Current = Cursors.Default;
            IniciarToolBar(true, true, true);
            //grid.BloqueaColumna(false, (int)Cols.Procesar);
            //MostrarEnProceso(false);
        }

        private void Generar_PDF(string Folio, string IdPrograma, string IdSubPrograma, int IdPerfilAtencion, string Titulo, bool TieneVenta, bool TieneConsignacion)
        {
            if (TieneVenta)
            {
                Generar_PDF(Folio, IdPrograma, IdSubPrograma, IdPerfilAtencion, Titulo, 0);
            }

            if (TieneConsignacion)
            {
                Generar_PDF(Folio, IdPrograma, IdSubPrograma, IdPerfilAtencion, Titulo, 1);
            }
        }

        private void Generar_PDF(string Folio, string IdPrograma, string IdSubPrograma, int IdPerfilAtencion, string Titulo, int EsConsignacion)
        {
            string sFile = Folio;
            //string sFecha = string.Format("__{0}_{1}_{2}",
            //    Fg.PonCeros(dtpFechaInicial.Value.Year, 4),
            //    Fg.PonCeros(dtpFechaInicial.Value.Month, 2),
            //    General.FechaNombreMes(dtpFechaInicial.Value).ToUpper());

            string TipoRpt = EsConsignacion == 0 ? "Venta" : "Consignación";


            sFile = string.Format("SV{1}___P{2}_SP{3}___{4}_{5}_{6}",
                sIdFarmacia, Folio, IdPrograma, IdSubPrograma, Fg.PonCeros(IdPerfilAtencion, 2), Titulo, TipoRpt);
            sFile = Path.Combine(sRutaDestino_Archivos, sFile + ".pdf");


            bool bRegresa = false;
            clsImprimir Reporte = new clsImprimir(General.DatosConexion);
            clsReporteador Reporteador;  // = new clsReporteador(Reporte, DatosTerminal);

            Reporte.RutaReporte = DtGeneral.RutaReportes;
            Reporte.NombreReporte = "PtoVta_TicketCredito_Detallado_Precios___AtencionClavesSSA";
            Reporte.Add("@IdEmpresa", sIdEmpresa);
            Reporte.Add("@IdEstado", sIdEstado);
            Reporte.Add("@IdFarmacia", sIdFarmacia);
            Reporte.Add("@Folio", Folio);
            Reporte.Add("@IdPerfilDeAtencion", IdPerfilAtencion);
            Reporte.Add("@EsConsignacion", EsConsignacion);
            Reporte.Add("@MostrarPrecios", 0);

            Reporteador = new clsReporteador(Reporte, DatosCliente);
            Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
            Reporteador.Url = General.Url;
            Reporteador.MostrarInterface = false;
            Reporteador.MostrarMensaje_ReporteSinDatos = false;

            bRegresa = Reporteador.ExportarReporte(sFile, FormatosExportacion.PortableDocFormat, true);
        }

        private bool Obtener_Cliente_Programa(string Folio)
        {
            bool bRegresa = false;

            string sSql = string.Format("Select * From VentasEnc Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioVenta = '{3}'",
                            sIdEmpresa, sIdEstado, sIdFarmacia, Folio);

            bRegresa = leerClientePrograma.Exec(sSql);

            return bRegresa;
        }

        private bool Obtener_Cuadros_De_Atencion(string FolioVenta)
        {
            bool bRegresa = false;

            string sSql = string.Format("Exec spp_Rpt_ALMN_Impresion_Ventas__Obtener_NivelesAtencion_ClavesSSA '{0}', '{1}', '{2}', '{3}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, FolioVenta);

            bRegresa = leerCuadrosDeAtencion.Exec(sSql);

            return bRegresa;
        }

        private bool GenerarDocto(string Tipo, string Folio)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_MTTO_ALMN_Impresion_CartasDeCanje '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                             DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Fg.PonCeros(txtFolio.Text, 8),
                             Folio, Tipo);

            if (!LeerAux2.Exec(sSql))
            {
                Error.GrabarError(LeerAux2, "GenerarDocto");
                General.msjError("Error al consultar folio.");
            }
            else
            {
                if (LeerAux2.Leer())
                {
                    if (LeerAux2.CampoBool("Contiene"))
                    {
                        string sFile = "Carta_Canje";

                        sFile = string.Format("{0}_{1}__{2}{3}",
                            sFile, Fg.PonCeros(txtFolio.Text, 8), Tipo, Folio);
                        sFile = Path.Combine(sRutaDestino_Archivos, sFile + ".pdf");

                        bRegresa = true;
                        //bool bRegresa = false;
                        clsImprimir Reporte = new clsImprimir(General.DatosConexion);
                        clsReporteador Reporteador;  // = new clsReporteador(Reporte, DatosTerminal);

                        Reporte.RutaReporte = DtGeneral.RutaReportes;
                        Reporte.NombreReporte = "PtoVta_Impresion_CartaDeCanje";
                        Reporte.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
                        Reporte.Add("@IdEstado", DtGeneral.EstadoConectado);
                        Reporte.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
                        Reporte.Add("@TipoFolio", Tipo);
                        Reporte.Add("@Folio", Folio);

                        Reporteador = new clsReporteador(Reporte, DatosCliente);
                        Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
                        Reporteador.Url = General.Url;
                        Reporteador.MostrarInterface = false;
                        Reporteador.MostrarMensaje_ReporteSinDatos = false;

                        Reporteador.ExportarReporte(sFile, FormatosExportacion.PortableDocFormat, true);
                    }
                }
            }
            return bRegresa;
        }

        private void GenerarPdfTransferencias()
        {
            bool bRegresa = true;
            DatosCliente.Funcion = "Imprimir()";

            Farmacia.Transferencias.ClsImprimirTransferencias imprimir = new Farmacia.Transferencias.ClsImprimirTransferencias(cnn.DatosConexion, DatosCliente, sRutaDestino_Archivos, true,
                                                                                                                               Farmacia.Transferencias.TipoReporteTransferencia.Detallado);

            

            //clsImprimir Reporte = new clsImprimir(General.DatosConexion);
            //clsReporteador Reporteador;  // = new clsReporteador(Reporte, DatosTerminal);

            //Reporte.RutaReporte = DtGeneral.RutaReportes;

            //Reporte.NombreReporte = "PtoVta_Transferencias.rpt";
            //Reporte.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            //Reporte.Add("IdEstado", DtGeneral.EstadoConectado);
            //Reporte.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            //Reporteador = new clsReporteador(Reporte, DatosCliente);
            //Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
            //Reporteador.Url = General.Url;
            //Reporteador.MostrarInterface = false;
            //Reporteador.MostrarMensaje_ReporteSinDatos = false;

            for (int i = 1; i <= myGridTrasf.Rows && bRegresa; i++)
            {
                sFolio = myGridTrasf.GetValue(i, (int)Cols.Folio);
                //string sFile = "TS" + sFolio;
                //sFile = Path.Combine(sRutaDestino_Archivos, sFile + ".pdf");
                //Reporte.Add("Folio", "TS" + sFolio);

                //Reporteador.ExportarReporte(sFile, FormatosExportacion.PortableDocFormat, true);

                bRegresa = imprimir.Imprimir("TS" + sFolio, chkDesglosado.Checked);
            }
        }
 

        private void btnGenerarDocumentos_Click(object sender, EventArgs e)
        {
            folder = new FolderBrowserDialog();
            folder.Description = "Directorio destino para los documentos generados.";
            folder.RootFolder = Environment.SpecialFolder.Desktop;
            folder.ShowNewFolderButton = true;

            if (folder.ShowDialog() == DialogResult.OK)
            {
                sRutaDestino = folder.SelectedPath + @"\";
                //lblDirectorioTrabajo.Text = sRutaDestino;
                //bFolderDestino = true;
                CrearDirectorioDestino();
                //btnGenerarDocumentos.Enabled = true;
                
                GenerarCartasCanje();
                GenerarPdfVentas();
                GenerarPdfTransferencias();
                GenerarPDFCajas();
                General.AbrirDirectorio(sRutaDestino_Archivos);
            }
        }
        #endregion Generacion_Cartas_Canje

        #region Firmar_Recepcion_de_Documentos
        private void btnFirmarDoctos_Click(object sender, EventArgs e)
        {
            string sReferenciaHuella = "";

            sReferenciaHuella = DtGeneral.EstadoConectado + DtGeneral.FarmaciaConectada + cboChofer.Data + sIdPuesto;
            //FP_General.TablaHuellas = "FP_Huellas_Cedis";
            clsVerificarHuella f = new clsVerificarHuella();
            f.MostrarMensaje = false;
            f.Show();

            if (FP_General.HuellaCapturada)
            {
                if (FP_General.ExisteHuella)
                {
                    if (sReferenciaHuella == FP_General.Referencia_Huella)
                    {
                        FirmarRecepcionDoctos();
                    }
                    else
                    {
                        General.msjAviso("Huella invalida.");
                    }
                }
            }
        }

        private void FirmarRecepcionDoctos()
        {
            string sSql = "";

            sSql = string.Format(" Update RutasDistribucionEnc Set Firmado = 1  " +
                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Fg.PonCeros(txtFolio.Text, 8));

            if (!Leer.Exec(sSql))
            {
                Error.GrabarError(Leer, "FirmarRecepcionDoctos");
                General.msjError("Error al firmar la recepción de documentos.");
            }
            else
            {
                General.msjAviso("Firma satisfactoria de recepción de documentos");
                btnFirmarDoctos.Enabled = false;
                btnGenerarDocumentos.Enabled = true;
            }
        }
        #endregion Firmar_Recepcion_de_Documentos

    }
}
