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

using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using Farmacia.Transferencias;

namespace Almacen.Pedidos
{
    public partial class FrmGenerarTransferencia : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, Cantidad = 4
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        DllFarmaciaSoft.clsConsultas query;
        DllFarmaciaSoft.clsAyudas ayuda;

        clsGrid myGrid;

        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        clsVerificarSalidaLotes VerificarLotes; 

        clsDatosCliente DatosCliente;
        //wsFarmacia.wsCnnCliente conexionWeb;

        // Se agrego para la verificacion de la Transferencia en el Destino
        clsLeerWebExt leerWeb;
        clsDatosConexion DatosDeConexion;

        string sFolioTransferencia = "";
        string sMensajeGrabar = "";

        bool bEstaCancelado = false;
        bool bDestinoEsAlmacen = false;

        string sFolioMovto = "";
        string sFolioSurtido = "";
        string sFolioPedido = ""; 
        string sFormato = "#,###,###,##0.###0";
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sEstado_Destino = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sCveRenapo = DtGeneral.ClaveRENAPO;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        string sIdTipoMovtoInv = "TS";
        string sTipoES = "S";
        string sIdProGrid = "";
        string sObservaciones = "";
        string sReferencia = "";

        string sUrlFarmacia = "";
        string sHost = "";

        bool bFolioGuardado = false;
        bool bEsCancelacionDeTransferencia = false;

        // string sFormato = "#,###,###,##0.###0";

        // Manejo automatico de Transferencias 
        DllTransferenciaSoft.ObtenerInformacion.clsCliente ClienteTransferencias =
            new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniOficinaCentral, General.DatosConexion); 

        public FrmGenerarTransferencia()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            //conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            //conexionWeb.Url = General.Url;

            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);

            leer = new clsLeer(ref cnn);
            query = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;

            //GnFarmacia.AjustaColumnasImportes(grdProductos, (int)Cols.Costo, (int)Cols.Importe, (int)Cols.Descripcion);

        }

        private void FrmGenerarTransferencia_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
            Lotes.ManejoLotes = OrigenManejoLotes.Transferencias;

            LlenarEncabezadoFolio();
            LlenarChoferes(); 
            IniciaToolbar(false, true, false, false, false);
        }

        #region Limpiar 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ////txtIdPersonal.Enabled = false;
            ////txtIdPersonal.Text = DtGeneral.IdPersonal;
            ////lblPersonal.Text = DtGeneral.NombrePersonal;
            ///
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
        }
        #endregion Limpiar 

        #region Guardar Informacion  
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;
            bool bBtnTransferencia = btnGenerarPaqueteDeDatos.Enabled;

            bEsCancelacionDeTransferencia = false;
            sIdTipoMovtoInv = "TS";
            sTipoES = "S";

            if (ValidaDatos())
            {
                if (!cnn.Abrir())
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                }
                else 
                {
                    bool bExito = false;
                    cnn.IniciarTransaccion();

                    // Generar el Movimiento de Inventario 
                    if (GrabarEncabezado())
                    {
                        // Generar la informacion de la transferencia de entrada 
                        if (GrabarDetalleEnvioTransferencia())
                        {
                            //bExito = AfectarExistencia(true, false);

                            //// Atencion de pedidos especiales 

                            bExito = ActualizarEstatusPedido();

                            if (bExito)
                            {
                                bExito = RevisarPedidoCompleto();

                                if (bExito)
                                {
                                    bExito = RegistrarAtencion();
                                }

                                if (bExito)
                                {
                                    bExito = AfectarExistenciaSurtidos();
                                }
                            }
                            
                            if (bExito)
                            {
                                bExito = AfectarExistenciaEnTransito(1);
                            }
                        }
                        
                    }

                    //if (bExito)
                    //{
                    //    bExito = AfectarExistenciaSurtidos();
                    //}

                    if (!bExito)
                    {
                        cnn.DeshacerTransaccion();
                        txtFolio.Text = "*";
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar la transferencia.");
                        IniciaToolbar(false, true, false, false, false);
                    }
                    else
                    {
                        cnn.CompletarTransaccion();

                        // IMach  // Enlazar el folio de inventario 
                        //IMachPtoVta.TerminarSolicitud(sFolioMovto);

                        General.msjUser(sMensajeGrabar);
                        // EnvioAutomaticoDeTransferencias(); 
                        IniciaToolbar(false,false,false,true,false);
                        ImprimirInformacion();

                        this.Hide(); 
                    }

                    cnn.Cerrar();
                }
            }
        }

        private bool GrabarEncabezado()
        {
            bool bRegresa = true;
            // string sObservaciones = txtObservaciones.Text.Trim(); 
            sFolioMovto = "";

            string sSql = string.Format("Exec spp_ALMN_GenerarTransferenciaSalida \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @IdPersonal = '{4}', @Observaciones = '{5}', @IdEstadoRecibe = '{6}', @IdFarmaciaRecibe = '{7}' \n",
                sEmpresa, sEstado, sFarmacia, sFolioSurtido, DtGeneral.IdPersonal, txtObservaciones.Text.Trim(), sEstado_Destino, txtFarmaciaDestino.Text.Trim());
            
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sFolioTransferencia = leer.Campo("FolioTransferencia");
                sFolioMovto = leer.Campo("FolioMovtoInv");
                txtFolio.Text = sFolioTransferencia.Substring(2);
                sMensajeGrabar = leer.Campo("Mensaje");

                bRegresa = GrabarDetalle_AtencionSurtido(); 
                if ( bRegresa ) 
                {
                    bRegresa = RevisarPedidoCompleto(); 
                }

                if ( bRegresa  ) 
                {
                    bRegresa = Grabar_Personal_Transito();
                }
            }

            return bRegresa;
        }

        private bool GrabarDetalle_AtencionSurtido()
        {
            string sSql = string.Format(" Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @IdPersonal = '{4}', @Observaciones = '{5}' \n",
                sEmpresa, sEstado, sFarmacia, sFolioSurtido, DtGeneral.IdPersonal, ""); 
            return leer.Exec(sSql);
        }

        private bool AfectarExistenciaSurtidos()
        {
            bool bRegresa = true;

            string sSql = string.Format("Exec spp_ALM_AplicaDesaplica_ExistenciaSurtidos " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @TipoFactor = 2, @Validacion_Especifica = 1  ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido);

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private bool Grabar_Personal_Transito()
        {
            bool bRegresa = true; 

            ////string sSql = string.Format(" Update Pedidos_Cedis_Enc_Surtido Set IdPersonalTransporte = '{4}' " + 
            ////    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioSurtido = '{3}' ",
            ////    sEmpresa, sEstado, sFarmacia, sFolioSurtido, cboChoferes.Data);
            //////return leer.Exec(sSql);

            return bRegresa; 
        }

        private bool GrabarDetalleEnvioTransferencia()
        {
            string sSql = string.Format(" Exec spp_Mtto_TransferenciasEnvioGenerar " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioTransferencia = '{3}' ", 
                sEmpresa, sEstado, sFarmacia, sFolioTransferencia);
            return leer.Exec(sSql);
        }

        private bool ActualizarEstatusPedido()
        {
            bool bRegresa = true;
            string sSql = string.Format("Update Pedidos_Cedis_Enc_Surtido " +
                "Set Status = 'E', FolioTransferenciaReferencia = '{0}' " +
                "Where IdEmpresa = '{1}' And IdEstado = '{2}' And IdFarmacia = '{3}' And FolioSurtido = '{4}'",
                sFolioTransferencia, sEmpresa, sEstado, sFarmacia, sFolioSurtido);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        private bool RegistrarAtencion()
        {
            bool bRegresa = true;
            string sSql = string.Format(" Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @IdPersonal = '{4}', @Observaciones = '{5}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, DtGeneral.IdPersonal, "");

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private bool RevisarPedidoCompleto()
        {
            bool bRegresa = true;

            string sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_RevisarPedidoCompleto  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdFarmaciaPedido = '{3}', @FolioPedido = '{4}' ",
                sEmpresa, sEstado, sFarmacia, txtFarmaciaDestino.Text, sFolioPedido);

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private bool AfectarExistencia(bool Aplicar, bool AfectarCosto)
        {
            AfectarInventario Inv = AfectarInventario.Ninguno;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            if (Aplicar)
                Inv = AfectarInventario.Aplicar;

            if (AfectarCosto)
                Costo = AfectarCostoPromedio.Afectar;

            string sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                sEmpresa, sEstado, sFarmacia, sFolioMovto, (int)Inv, (int)Costo);

            bool bRegresa = leer.Exec(sSql);
            return bRegresa;
        }
        #endregion Guardar Informacion

        #region Cancelar Informacion
        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        #endregion Cancelar Informacion

        #region Imprimir 
        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Imprimir 

        #region Generar Paquete de Datos 
        private void btnGenerarPaqueteDeDatos_Click(object sender, EventArgs e)
        {
            GenerarPaqueteDeDatos();
        }

        private void GenerarPaqueteDeDatos()
        {
            string sMsj = string.Format("¿ Desea generar el paquete de datos para la Transferencia {0} ?", sFolioTransferencia);
            sMsj = string.Format("¿ Desea generar el paquete de datos para la Transferencia {0} ?", txtFolio.Text);

            if (General.msjConfirmar(sMsj) == DialogResult.Yes)
            {
                if (PreparaTransferenciaReenvio())
                {
                    ClienteTransferencias = new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniPuntoDeVenta, General.DatosConexion);
                    ClienteTransferencias.TransferenciasAutomaticas(sEstado, Fg.PonCeros(txtFarmaciaDestino.Text, 4));
                    ActualizaStatusTransferencia();
                    General.msjAviso("Generación de Paquete de Datos terminada.");
                }
                // ClienteTransferencias.EnviarArchivos(); 
            }
        }

        private bool PreparaTransferenciaReenvio()
        {
            bool bRegresa = true;
            string sSql = "";
            string sFolioRev = "TS" + txtFolio.Text;

            // sSql = string.Format("Update  Set Actualizado = 0 Where IdEstadoEnvia = '20' and IdFarmaciaEnvia = '0011' and IdFarmaciaRecibe = '0010' ");
            sSql = string.Format("Update TransferenciasEnvioEnc Set Actualizado = 0 " +
                " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and IdFarmaciaRecibe = '{2}' and FolioTransferencia = '{3}' \n",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFarmaciaDestino.Text, sFolioRev);

            sSql = string.Format("Update TransferenciasEnvioDet Set Actualizado = 0 " +
                " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and IdFarmaciaRecibe = '{2}' and FolioTransferencia = '{3}' \n",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFarmaciaDestino.Text, sFolioRev);

            sSql = string.Format("Update TransferenciasEnvioDet_Lotes Set Actualizado = 0 " +
                " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and IdFarmaciaRecibe = '{2}' and FolioTransferencia = '{3}' \n",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFarmaciaDestino.Text, sFolioRev);

            sSql = string.Format("Update TransferenciasEnvioDet_LotesRegistrar Set Actualizado = 0 " +
                " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and IdFarmaciaRecibe = '{2}' and FolioTransferencia = '{3}' \n",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFarmaciaDestino.Text, sFolioRev);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "PreparaTransferenciaReenvio()");
                General.msjError("Ocurrió un error al preparar la transferencia para empaquetado.");
            }

            return bRegresa;
        }
        
        private void ActualizaStatusTransferencia()
        {
            string sSql = string.Format(" Update TransferenciasEnvioEnc Set Status = 'T' " +
                          " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' ",
                          sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ActualizaStatusTransferencia()");
                General.msjError("Ocurrió un error al actualizar el status de la Transferencia");
            }
        }
        
        #endregion Generar Paquete de Datos

        #region Funciones 
        private void LlenarChoferes()
        {
            //////string sSql = string.Format(" Select IdPersonal, Personal From vw_PersonalCEDIS (NoLock) Where IdPuesto = '02' Order By Personal ");
            //////cboChoferes.Add("0", "<< Seleccione >>");

            //////if (!leer.Exec(sSql))
            //////{
            //////    Error.GrabarError(leer, "LlenarChoferes()");
            //////    General.msjError("Ocurrió un error al obtener la Lista de Surtidores.");
            //////}
            //////else
            //////{
            //////    if (leer.Leer())
            //////    {
            //////        cboChoferes.Add(leer.DataSetClase, true);
            //////    }
            //////    cboChoferes.SelectedIndex = 0;

            //////}
        }

        public void CargarPedido(string FolioSurtido, string FolioPedido)
        {
            sFolioSurtido = FolioSurtido;
            sFolioPedido = FolioPedido; 
            this.ShowDialog();
        }

        private void IniciaToolbar(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir, bool Generar)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnGenerarPaqueteDeDatos.Enabled = Generar;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            int iCantidadTotal = myGrid.TotalizarColumna((int)Cols.Cantidad);

            if (txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese las Observaciones por favor.");
                txtObservaciones.Focus();
            }

            if (bRegresa && iCantidadTotal == 0)
            {
                bRegresa = false;
                General.msjUser("Debe capturar al menos un producto con cantidad mayor a cero.");
                grdProductos.Focus();
            }

            //////if (bRegresa && cboChoferes.SelectedIndex == 0)
            //////{
            //////    bRegresa = false;
            //////    General.msjUser("No ha seleccionado el personal para el traslado de la transferencia.");
            //////    cboChoferes.Focus();
            //////}

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            if (bRegresa)
            {
                bRegresa = validarCantidadesCapturadas();
            }

            if (bRegresa)
            {
                if (!bEsCancelacionDeTransferencia)
                {
                    ////VerificarLotes = new FrmVerificarSalidaLotes();
                    ////bRegresa = VerificarLotes.VerificarExistenciasConError(Lotes.DataSetLotes);
                    bRegresa = validarExistenciasCantidadesLotes(false);
                }
            }

            return bRegresa;
        }

        private bool validarExistenciasCantidadesLotes(bool MostrarMsj)
        {
            bool bRegresa = true;

            VerificarLotes = new clsVerificarSalidaLotes();
            bRegresa = VerificarLotes.VerificarExistenciasConError(Lotes, MostrarMsj);

            return bRegresa;
        }

        private bool validarCapturaProductos()
        {
            bool bRegresa = true;

            if (myGrid.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                if (myGrid.GetValue(1, (int)Cols.Descripcion) == "")
                {
                    bRegresa = false;
                }
                else
                {
                    if (Lotes.CantidadTotal == 0)
                    {
                        bRegresa = false;
                    }
                    else
                    {
                        for (int i = 1; i <= myGrid.Rows; i++)
                        {
                            if (myGrid.GetValue(i, (int)Cols.CodEAN) != "" && myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (!bRegresa)
                General.msjUser("Debe capturar al menos un producto para la transferencia\n y/o capturar cantidades para al menos un lote, verifique.");

            return bRegresa;

        }

        private bool validarCantidadesCapturadas()
        {
            bool bRegresa = true;
            DataSet dtsProductosDiferencias = clsLotes.PreparaDtsRevision();
            // string sSql = "", 
            string sIdProducto = "", sCodigoEAN = "", sDescripcion = "";
            // int iRenglon = 0, 
            int iCantidad = 0, iCantidadLotes = 0;

            for (int i = 1; i < myGrid.Rows; i++)
            {
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sDescripcion = myGrid.GetValue(i, (int)Cols.Descripcion);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);

                iCantidadLotes = 0;
                //ObtieneClaveLote(sIdProducto, sCodigoEAN, ref sClaveLote);

                clsLotes[] ListaLotes = Lotes.Lotes(sIdProducto, sCodigoEAN);
                foreach (clsLotes L in ListaLotes)
                {
                    iCantidadLotes += L.Cantidad;
                }

                if (iCantidad != iCantidadLotes)
                {
                    bRegresa = false;
                    object[] obj = { sIdProducto, sCodigoEAN, sDescripcion, iCantidad, iCantidadLotes };
                    dtsProductosDiferencias.Tables[0].Rows.Add(obj);
                }
            }

            // Se encontraron diferencias 
            if (!bRegresa)
            {
                General.msjAviso("Se detecto una ó mas diferencias en la captura de productos, la Transferencia no puede ser completada.");
                FrmProductosConDiferencias f = new FrmProductosConDiferencias(dtsProductosDiferencias);
                f.ShowDialog();
            }

            return bRegresa;
        }


        private void LlenarEncabezadoFolio()
        {
            string sSql = string.Format("Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdEstadoSolicita, E.IdFarmaciaSolicita As IdFarmaciaPedido " +
                "From Pedidos_Cedis_Enc_Surtido S (NoLock) " +
                "Inner Join Pedidos_Cedis_Enc  E (NoLock) On (S.IdEmpresa = E.IdEmpresa And S.IdEstado = E.IdEstado And S.IdFarmaciaPedido = E.Idfarmacia And S.FolioPedido = E.FolioPedido)" +
                " Where S.IdEmpresa = '{0}' and S.IdEstado = '{1}' and S.IdFarmacia = '{2}' and S.FolioSurtido = '{3}' ",
                sEmpresa, sEstado, sFarmacia, sFolioSurtido);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "LlenarEncabezadoFolio()");
                General.msjError("Ocurró un error al cargar el Encabezado del Folio de surtido.");
                this.Close();
            }
            else
            {
                if (leer.Leer())
                {
                    sEstado_Destino = leer.Campo("IdEstadoSolicita");
                    CargarDatosEncabezado();
                    LlenarDetalleFolio();
                }
                else
                {
                    General.msjUser("Ocurrió un error al cargar el Encabezado del Folio de surtido");
                }
            }
        }

        private void CargarDatosEncabezado()
        {
            txtFolio.Text = "*";
            txtFolio.Enabled = false;
            txtFarmaciaDestino.Text = leer.Campo("IdFarmaciaPedido");
            txtFarmaciaDestino_Validating(null, null);
            txtFarmaciaDestino.Enabled = false;

            //if (leer.Campo("Status") == "C")
            //{
            //    lblCancelado.Visible = true;
            //    txtId.Enabled = false;
            //    txtDescripcion.Enabled = false;
            //}
        }

        private void LlenarDetalleFolio()
        {

            string sSql = string.Format("Select	S.CodigoEAN, S.IdProducto, P.Descripcion, Sum( S.CantidadAsignada ) as Cantidad	" +
                "From Pedidos_Cedis_Det_Surtido_Distribucion S(NoLock) " +
                "Inner Join vw_Productos_CodigoEAN P(NoLock) On ( S.IdProducto = P.IdProducto And S.CodigoEAN = P.CodigoEAN ) " +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' and IdFarmacia = '{2}' and FolioSurtido = '{3}' and S.CantidadAsignada > 0  " +
                "Group By S.IdProducto, S.CodigoEAN, P.Descripcion ",
                sEmpresa, sEstado, sFarmacia, sFolioSurtido);

            myGrid.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener el detalle del Folio de Surtido.");
            }
            else
            {
                if (leer.Leer())
                {
                    myGrid.LlenarGrid(leer.DataSetClase);
                    myGrid.BloqueaGrid(true);
                    CargarDetallesLotesTransferencia();
                }
                else
                {
                    General.msjUser("El Folio de Surtido seleccionado no contiene detalles. Verifique");
                }
            }

            myGrid.BloqueaColumna(true, (int)Cols.CodEAN); 
        }

        private void CargarDetallesLotesTransferencia()
        {
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.GenerarTransferenciaDetallesLotes(sEmpresa, sEstado, sFarmacia, sFolioSurtido, "CargarDetallesTransferencia");
            Lotes.AddLotes(leer.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                string sFolio = sIdTipoMovtoInv + Fg.PonCeros(txtFolio.Text, 8);
                leer.DataSetClase = query.GenerarTransferenciaDetallesLotesUbicacion(sEmpresa, sEstado, sFarmacia, sFolioSurtido, "CargarDetallesLotesMovimiento");
                Lotes.AddLotesUbicaciones(leer.DataSetClase);
            }
        }
        #endregion Funciones 

        #region Impresion
        private void ImprimirInformacion()
        {
            bool bRegresa = false;

            TipoReporteTransferencia TipoImpresion = !chkTipoImpresion.Checked ? TipoReporteTransferencia.Detallado : TipoReporteTransferencia.Ticket;

            string sFolio = "TS" + txtFolio.Text;

            if (validarImpresion())
            {
                DatosCliente.Funcion = "Imprimir()";

                Farmacia.Transferencias.ClsImprimirTransferencias imprimir = new Farmacia.Transferencias.ClsImprimirTransferencias(cnn.DatosConexion, DatosCliente, "", false, TipoImpresion);

                bRegresa = imprimir.Imprimir(sFolio, false);

                //clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                //// byte[] btReporte = null;

                //myRpt.RutaReporte = GnFarmacia.RutaReportes;

                //if (!chkTipoImpresion.Checked)
                //{
                //    myRpt.NombreReporte = "PtoVta_Transferencias.rpt";
                //}
                //else
                //{
                //    myRpt.NombreReporte = "PtoVta_TransferenciasTicket.rpt";
                //}

                //myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                //myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                //myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                //myRpt.Add("Folio", "TS" + txtFolio.Text);

                //bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                ////if (General.ImpresionViaWeb)
                ////{
                ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////    DataSet datosC = DatosCliente.DatosCliente();

                ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                ////}
                ////else
                ////{
                ////    myRpt.CargarReporte(true);
                ////    bRegresa = !myRpt.ErrorAlGenerar;
                ////}

                if (bRegresa)
                {
                    ImprimirRptCajas();
                }

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
                
            }
        }

        private void ImprimirRptCajas()
        {
            DatosCliente.Funcion = "ImprimirRptCajas()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.TituloReporte = "Impresión de Cajas";

            myRpt.Add("@IdEmpresa", sEmpresa);
            myRpt.Add("@IdEstado", sEstado);
            myRpt.Add("@IdFarmacia", sFarmacia);
            myRpt.Add("@Folioreferencia", sFolioTransferencia);
            myRpt.NombreReporte = "PtoVta_Caja_Embarque";

            bool bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte de las Cajas.");
            }
        }

        private bool validarImpresion()
        {
            bool bRegresa = true;
            return bRegresa;
        }
        #endregion Impresion 

        #region Buscar Farmacia Destino 
        private void txtFarmaciaDestino_Validating(object sender, CancelEventArgs e)
        {
            bool bExito = false;
            myGrid.Limpiar(false);

            if (txtFarmaciaDestino.Text.Trim() != "")
            {
                if (Fg.PonCeros(txtFarmaciaDestino.Text, 4) == DtGeneral.FarmaciaConectada)
                {
                    General.msjUser("No se puede generar transferencia a la farmacia origen, verifique.");
                    e.Cancel = true;
                }
                else
                {
                    leer.DataSetClase = query.FarmaciasTransferencia(sEmpresa, iEsEmpresaConsignacion, sEstado_Destino, sFarmacia, txtFarmaciaDestino.Text, "txtFarmaciaDestino_Validating");
                    if (leer.Leer())
                    {
                        //sFolioSolicitud = IMachPtoVta.ObtenerFolioSolicitud();
                        bExito = CargarDatosFarmacia();
                        // myGrid.Limpiar(true); 
                    }
                }
            }

            if (!bExito)
            {
                txtFarmaciaDestino.Text = "";
                lblFarmaciaDestino.Text = "";
                // e.Cancel = true;
                //txtFarmaciaDestino.Focus(); 
            }
            else
            {
                myGrid.Limpiar(true);
            }
        }
        private bool CargarDatosFarmacia()
        {
            bool bRegresa = false;
            if (leer.Campo("FarmaciaStatus").ToUpper() == "C")
            {
                General.msjUser("La Farmacia seleccionada actualmente se encuentra cancelada,\nno es posible generar la transferencia.");
                txtFarmaciaDestino.Text = "";
                lblFarmaciaDestino.Text = "";
            }
            else
            {
                bDestinoEsAlmacen = leer.CampoBool("EsAlmacen");
                {
                    bRegresa = true;
                    txtFarmaciaDestino.Enabled = false;
                    txtFarmaciaDestino.Text = leer.Campo("IdFarmacia");
                    lblFarmaciaDestino.Text = leer.Campo("Farmacia");
                }
            }
            return bRegresa;
        }

        #endregion Buscar Farmacia Destino

        #region AfectarExistenciaEnTransito
        private bool AfectarExistenciaEnTransito(int TipoFactor)
        {
            string sSql = string.Format("Exec spp_INV_AplicaDesaplicaExistenciaTransito \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioTransferencia = '{3}', @TipoFactor = '{4}' ",
                sEmpresa, sEstado, sFarmacia, sFolioTransferencia, TipoFactor);

            bool bRegresa = leer.Exec(sSql);
            return bRegresa;
        }
        #endregion AfectarExistenciaEnTransito
    }
}
