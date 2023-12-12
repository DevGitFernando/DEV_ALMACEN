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

namespace Almacen.TraspasosEstatales
{
    public partial class FrmEdoCambioTraspasosSalidas : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        DllFarmaciaSoft.clsConsultas query;
        DllFarmaciaSoft.clsAyudas ayuda;

        clsDatosCliente DatosCliente;
        wsAlmacen.wsCnnCliente conexionWeb;

        // Se agrego para la verificacion de la Transferencia en el Destino
        clsLeerWebExt leerWeb;
        clsDatosConexion DatosDeConexion;

        string sEmpresa = DtGeneral.EmpresaConectada;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sCveRenapo = DtGeneral.ClaveRENAPO;
        string sIdTipoMovtoInv = "TS";
        string sFolioTransferencia = "";
        string sUrlFarmacia = "";
        string sHost = "";

        bool TransferenciaAplicada = false;

        public FrmEdoCambioTraspasosSalidas()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new wsAlmacen.wsCnnCliente();
            conexionWeb.Url = General.Url;

            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);

            leer = new clsLeer(ref cnn);
            query = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
        }

        private void FrmEdoCambioTraspasosSalidas_Load( object sender, EventArgs e )
        {
            CargarEstadosFiliales();
            btnNuevo_Click(null, null);
        }

        #region Control de Estados Filiales
        private void CargarEstadosFiliales()
        {
            string sSql = string.Format(
                "Select IdEstado, NombreEstado as Estado \n" +
                "From vw_EmpresasEstados \n " +
                "Where IdEmpresa = '{0}' and IdEstado not in ( '{1}' ) \n " +
                " Order by IdEstado ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado);

            cboEstados.Clear();
            cboEstados.Add();

            cboEstadoDestino.Clear();
            cboEstadoDestino.Add();

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEstadosFiliales()");
                General.msjError("Ocurrió un error al obtener la lista de Estados filiales.");
            }
            else
            {
                if(leer.Leer())
                {
                    cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Estado");

                    cboEstadoDestino.Add(leer.DataSetClase, true, "IdEstado", "Estado");
                }
            }

            cboEstados.SelectedIndex = 0;
            cboEstadoDestino.SelectedIndex = 0;
        }
        #endregion Control de Estados Filiales

        #region Botones
        private void btnNuevo_Click( object sender, EventArgs e )
        {
            LimpiaPantalla();
        }

        private void btnGuardar_Click( object sender, EventArgs e )
        {
            //if (ConsultarTransferenciaDestino())
            if(ValidaStatusIntegrada())
            {
                if(validarDatos())
                {
                    if(!cnn.Abrir())
                    {
                        Error.LogError(cnn.MensajeError);
                        General.msjErrorAlAbrirConexion();
                    }
                    else
                    {
                        bool bExito = false;
                        cnn.IniciarTransaccion();

                        // Actualizar la Farmacia Destino del Encabezado de la Transferencia  
                        if(ActualizarEncabezadoTransferencia())
                        {
                            // Borrar las Transferencias Envio 
                            if(BorraTransferenciasEnvio())
                            {
                                // Generar la informacion de la transferencia de entrada 
                                bExito = GrabarDetalleEnvioTransferencia();
                            }
                        }

                        if(!bExito)
                        {
                            cnn.DeshacerTransaccion();
                            Error.GrabarError(leer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al Actualizar la transferencia.");
                        }
                        else
                        {
                            cnn.CompletarTransaccion();

                            General.msjUser(" La Información se Actualizo con Exito... ");

                            IniciarToolBar(false, false);
                            Imprimir();
                        }

                        cnn.Cerrar();
                    }
                }
            }
        }

        private void btnImprimir_Click( object sender, EventArgs e )
        {
            Imprimir();
        }
        #endregion Botones

        #region Funciones
        private void IniciarToolBar( bool Guardar, bool Imprimir )
        {
            btnGuardar.Enabled = Guardar;
            btnImprimir.Enabled = Imprimir;
        }

        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            IniciarToolBar(false, true);
            cboEstados.Enabled = false;
            cboEstadoDestino.Enabled = false;

            sFolioTransferencia = "";
            sUrlFarmacia = "";
            sHost = "";
            dtpFechaRegistro.Enabled = false;
            TransferenciaAplicada = false;

            txtFolio.Focus();
        }

        private bool CargarDatosFarmacia()
        {
            bool bRegresa = true;
            bool bEsunidosisDestino = leer.CampoBool("EsUnidosis");

            if(leer.Campo("FarmaciaStatus").ToUpper() == "C")
            {
                General.msjUser("El almacén seleccionado actualmente se encuentra cancelado,\nno es posible generar la transferencia.");
                txtFarmaciaDes.Text = "";
                lblDesFarmaciaDes.Text = "";
                txtFarmaciaDes.Focus();
                bRegresa = false;
            }

            if(bEsunidosisDestino && bRegresa)
            {
                General.msjUser("El Almacen seleccionado es unidosis, no es posible generar la transferencia.");
                txtFarmaciaDes.Text = "";
                txtFarmaciaDes.Focus();
                lblDesFarmaciaDes.Text = "";
                bRegresa = false;
            }

            if(bRegresa)
            {
                //////////// bDestinoEsAlmacen = leer.CampoBool("EsAlmacen"); 
                //////////// Jesus Diaz 2K111107.1510 
                //////////// Si la Farmacia Conectada Es Almacen no se valida la Farmacia Destino 
                //////if (!DtGeneral.EsAlmacen)
                //////{
                //////    if (leer.CampoBool("EsAlmacen"))
                //////    {
                //////        General.msjUser("La Farmacia seleccionada esta configurada como Almacén,\nno es posible generar la transferencia de Farmacia a Almacén.");
                //////        txtFarmaciaDes.Text = "";
                //////        lblDesFarmaciaDes.Text = "";
                //////    }
                //////    else
                //////    {
                //////        bRegresa = true;
                //////        txtFarmaciaDes.Enabled = false;
                //////        txtFarmaciaDes.Text = leer.Campo("IdFarmacia");
                //////        lblDesFarmaciaDes.Text = leer.Campo("Farmacia");
                //////    }
                //////}
                //////else
                {
                    bRegresa = true;
                    cboEstadoDestino.Enabled = false;
                    txtFarmaciaDes.Enabled = false;
                    txtFarmaciaDes.Text = leer.Campo("IdFarmacia");
                    lblDesFarmaciaDes.Text = leer.Campo("Farmacia");
                }

                if(lblFarmaciaDes.Text.Trim() == txtFarmaciaDes.Text.Trim())
                {
                    General.msjUser("El nuevo almacén destino no puede ser igual al almacén destino anterior, verifique.");
                    bRegresa = false;
                    txtFarmaciaDes.Enabled = true;
                    txtFarmaciaDes.Text = "";
                    lblDesFarmaciaDes.Text = "";
                    txtFarmaciaDes.Focus();
                }
            }
            return bRegresa;
        }
        private bool validarDatos()
        {
            bool bRegresa = true;

            if(txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de transferencia inválido, verifique.");
                txtFolio.Focus();
            }

            if(bRegresa && cboEstadoDestino.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado el Nuevo Estado destino de la transferencia, verifique.");
                cboEstadoDestino.Focus();
            }

            if(bRegresa && txtFarmaciaDes.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Nueva Farmacia destino de la transferencia, verifique.");
                txtFarmaciaDes.Focus();
            }

            if(bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las Observaciones de la transferencia, verifique.");
                txtObservaciones.Focus();
            }

            return bRegresa;
        }
        #endregion Funciones

        #region Eventos
        ////private void txtFolio_Validating(object sender, CancelEventArgs e)
        ////{

        ////}

        private void txtFolio_Validating_1( object sender, CancelEventArgs e )
        {
            string sStatus = "";

            if(txtFolio.Text.Trim() != "")
            {
                leer.DataSetClase = query.FolioTransferencia(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, true, "txtFolio_Validating");
                if(leer.Leer())
                {
                    IniciarToolBar(true, false);
                    txtFolio.Enabled = false;

                    sFolioTransferencia = leer.Campo("Folio");
                    txtFolio.Text = leer.Campo("Folio");
                    dtpFechaRegistro.Value = leer.CampoFecha("FechaReg");

                    cboEstados.Enabled = false;
                    cboEstados.Data = leer.Campo("IdEstadoRecibe");
                    lblFarmaciaDes.Text = leer.Campo("IdFarmaciaRecibe");
                    lblFarmaciaDestino.Text = leer.Campo("FarmaciaRecibe");
                    txtObservaciones.Text = leer.Campo("Observaciones");
                    sStatus = leer.Campo("Status");


                    cboEstadoDestino.Enabled = true;
                    cboEstadoDestino.Data = leer.Campo("IdEstadoRecibe");


                    if(sStatus.Trim() == "C")
                    {
                        lblCancelado.Visible = true;
                        lblCancelado.Text = "CANCELADA";
                        General.msjUser("La Transferencia de Salida Esta cancelada.");
                        IniciarToolBar(false, true);
                    }
                    else
                    {
                        IniciarToolBar(true, true);
                    }
                    txtFarmaciaDes.Focus();
                    ObtenerUrlFarmacia();
                }
                else
                {
                    txtFolio.SelectAll();
                    txtFolio.Focus();
                }
            }
        }

        private void txtFarmaciaDes_Validating( object sender, CancelEventArgs e )
        {
            bool bExito = false;

            if(txtFarmaciaDes.Text.Trim() != "")
            {
                //if (Fg.PonCeros(txtFarmaciaDes.Text, 4) == DtGeneral.FarmaciaConectada)
                //{
                //    General.msjUser("No se puede generar transferencia a la farmacia origen, verifique.");
                //    e.Cancel = true;
                //}
                //else
                {
                    leer.DataSetClase = query.FarmaciasTransferencia(sEmpresa, iEsEmpresaConsignacion, cboEstadoDestino.Data, sFarmacia, txtFarmaciaDes.Text, true, "txtFarmaciaDes_Validating");
                    if(leer.Leer())
                    {
                        bExito = CargarDatosFarmacia();
                    }
                }
            }

            if(!bExito)
            {
                txtFarmaciaDes.Text = "";
                lblDesFarmaciaDes.Text = "";
                //txtFarmaciaDes.Focus(); 
            }
            else
            {
                txtObservaciones.Focus();
            }
        }
        #endregion Eventos

        #region Validar_Existe_Transferencia_Destino
        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWebExt(sUrlFarmacia, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

                conexionWeb.Url = sUrlFarmacia;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

                DatosDeConexion.Servidor = sHost;
                bRegresa = true;
            }
            catch(Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
                //ActivarControles();
            }

            return bRegresa;
        }

        private bool ConsultarTransferenciaDestino()
        {
            bool bCancela = false;
            bool bBorrarTransf = false;

            string sValor = "-- " + DtGeneral.EstadoConectado + "-" + lblFarmaciaDes.Text;

            string sSql = string.Format(" SELECT IdEmpresa, Folio, FolioReferenciaEntrada, FechaRegistroEntrada, " +
                        " (case when Folio = '*' Then Status Else 'E' End ) as Status, StatusTransferencia " +
                        " FROM vw_TransferenciaEnvio_Enc (NoLock) " +
                        " WHERE IdEstadoEnvia = '{0}' AND IdFarmaciaEnvia = '{1}' AND IdFarmaciaRecibe = '{2}' AND Folio = '{3}' ",
                        sEstado, sFarmacia, Fg.PonCeros(lblFarmaciaDes.Text, 4), sFolioTransferencia);

            if(validarDatosDeConexion())
            {
                if(!leerWeb.Exec(sSql))
                {
                    Error.GrabarError(sValor + " -- " + sUrlFarmacia, "ConsultarTransferenciaDestino()");
                    General.msjError("No fue posible verificar el Estado de la Transferencia, intente de nuevo.");
                }
                else
                {
                    if(!leerWeb.Leer())
                    {
                        bCancela = true;
                    }
                    else
                    {
                        if(leerWeb.Campo("Status") == "A")
                        {
                            bCancela = true;
                            bBorrarTransf = true;
                        }
                        if(leerWeb.Campo("Status") == "E")
                        {
                            General.msjAviso(" La Transferencia ya fue Registrada en la Farmacia Destino");
                            // bCancela = false;
                            IniciarToolBar(false, false);
                        }
                    }
                }
            }

            if(bCancela && bBorrarTransf)
            {
                bCancela = BorrarTransferenciasEnvioDestino();
            }
            return bCancela;
        }

        private bool BorrarTransferenciasEnvioDestino()
        {
            bool bRegresa = true;

            string sSql = string.Format(
                " Delete From TransferenciasEnvioDet_LotesRegistrar " +
                " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n " +
                " " +
                " Delete From TransferenciasEnvioDet_Lotes " +
                " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n " +
                " " +
                " Delete From TransferenciasEnvioDet " +
                " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n " +
                " " +
                " Delete From TransferenciasEnvioEnc " +
                " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' ",
                                sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            //if (!leerWeb.Exec(sSql))
            //{
            //    bRegresa = false;
            //}
            bRegresa = leerWeb.Exec(sSql);
            if(!bRegresa)
            {
                Error.GrabarError(leerWeb.MensajeError, "BorrarTransferenciasEnvioDestino");
                General.msjError("Ocurrió un error al Cancelar la Transferencia en el Destino, intente de nuevo por favor.");
            }


            return bRegresa;
        }
        #endregion Validar_Existe_Transferencia_Destino

        #region Actualizar_Transferencia
        private bool ActualizarEncabezadoTransferencia()
        {
            string sSql = "";

            sSql = string.Format(
                "Update TransferenciasEnc Set IdEstadoRecibe = '{4}', IdFarmaciaRecibe = '{5}', Observaciones = '{6}', Actualizado = 0 \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioTransferencia = '{3}' \n\n",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text,
                cboEstadoDestino.Data, txtFarmaciaDes.Text,
                txtObservaciones.Text
                                        );

            return leer.Exec(sSql);
        }

        private bool BorraTransferenciasEnvio()
        {
            bool bRegresa = true;

            string sSql = "";

            if(GnFarmacia.ImplementaCodificacion_DM)
            {
                sSql = string.Format(
                    "Delete Transferencias_UUID_Registrar \n" +
                    "Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n\n",
                    sEmpresa, sEstado, sFarmacia, txtFolio.Text);
            }

            sSql += string.Format(
                "Delete From TransferenciasEnvioDet_LotesRegistrar \n" +
                "Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n\n" +
                "Delete From TransferenciasEnvioDet_Lotes \n" +
                "Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n\n" +
                "Delete From TransferenciasEnvioDet \n" +
                "Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n\n" +
                "Delete From TransferenciasEnvioEnc \n" +
                "Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n\n",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text);

            if(!leer.Exec(sSql))
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        private bool GrabarDetalleEnvioTransferencia()
        {
            string sSql = string.Format("Exec spp_Mtto_TransferenciasEnvioGenerar \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioTransferencia = '{3}' ",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text);
            return leer.Exec(sSql);
        }
        #endregion Actualizar_Transferencia

        #region Obtener_Url_Farmacia
        private bool ObtenerUrlFarmacia()
        {
            bool bRegresa = true;

            string sSql = string.Format(
                "Select F.UrlFarmacia, C.Servidor \n" +
                "From vw_Farmacias_Urls F (Nolock) \n" +
                "Inner Join CFGS_ConfigurarConexiones C (NoLock) \n" +
                "\tOn ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia ) \n" +
                "Where F.IdEmpresa = '{0}' And F.IdEstado = '{1}' And F.IdFarmacia = '{2}' ",
                sEmpresa, cboEstadoDestino.Data, lblFarmaciaDes.Text);

            if(!leer.Exec(sSql))
            {
                bRegresa = false;
                General.msjError("Ocurrió un Error al Obtener la Url de Farmacia Destino");
                Error.GrabarError(leer, "ObtenerUrlFarmacia()");
            }
            else
            {
                if(leer.Leer())
                {
                    sUrlFarmacia = leer.Campo("UrlFarmacia");
                    sHost = leer.Campo("Servidor");
                }
            }

            return bRegresa;
        }
        #endregion Obtener_Url_Farmacia

        #region Impresion
        private void Imprimir()
        {
            bool bRegresa = false;

            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            // myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.RutaReporte = DtGeneral.RutaReportes;

            myRpt.NombreReporte = "PtoVta_Transferencias.rpt";

            myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("Folio", txtFolio.Text);

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            //////if (General.ImpresionViaWeb)
            //////{
            //////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            //////    DataSet datosC = DatosCliente.DatosCliente();

            //////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
            //////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
            //////}
            //////else
            //////{
            //////    myRpt.CargarReporte(true);
            //////    bRegresa = !myRpt.ErrorAlGenerar;
            //////}

            if(bRegresa)
            {
                LimpiaPantalla();
            }
            else
            {
                if(!DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }

        }
        #endregion Impresion

        #region Validar_Status_Integracion
        private bool ValidaStatusIntegrada()
        {
            bool bRegresa = false;
            string sSql = "", Status = "";

            sSql = string.Format(" Select * From TransferenciasEnvioEnc (Nolock) Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia = '{2}' " +
                                 " and FolioTransferencia = '{3}' ", sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidaStatusIntegrada()");
                General.msjError("Ocurrió un error al buscar el status.");
            }
            else
            {
                if(leer.Leer())
                {
                    Status = leer.Campo("Status");

                    if(Status == "I" || Status == "T")
                    {
                        General.msjAviso("La Transferencia ha sido integrada en el destino ó se encuentra en Transito, no es posible realizar cambios de la Transferencia ");
                    }
                    else
                    {
                        bRegresa = true;
                    }
                }
            }

            return bRegresa;
        }
        #endregion Validar_Status_Integracion
    }
}
