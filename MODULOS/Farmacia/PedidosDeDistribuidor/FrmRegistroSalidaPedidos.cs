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
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;

//using Dll_IMach4;
//using Dll_IMach4.Interface;

using DllTransferenciaSoft.ObtenerInformacion;



namespace Farmacia.PedidosDeDistribuidor
{
    public partial class FrmRegistroSalidaPedidos : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5,
            Costo = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10 
        }

        #region Variables 
        //PuntoDeVenta IMachPtoVta = new PuntoDeVenta();

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        DllFarmaciaSoft.clsConsultas query;
        DllFarmaciaSoft.clsAyudas ayuda;
        clsCodigoEAN EAN = new clsCodigoEAN();

        clsGrid myGrid;
        clsLotes Lotes; 
        clsLotes LotesEntrada; 
        clsVerificarSalidaLotes VerificarLotes; 

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;

        // Se agrego para la verificacion de la Salida en el Destino
        clsLeerWebExt leerWeb;        
        // clsDatosConexion DatosDeConexion;

        string sFolioSalida = "";
        string sMensajeGrabar = "";
        bool bCargarPedido = false;
        // bool bPermitirCapturaDeSalidas = false;
        bool bFolioGuardado = false; 

        string sFolioMovto = "", sFolioMovtoEntrada = "";
        string sFormato = "#,###,###,##0.###0";
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        string sCveRenapo = DtGeneral.ClaveRENAPO;
        string sIdTipoMovtoInv = "SPD";
        string sIdTipoMovtoInv_Entrada = "EDD";
        string sTipoES = "S";
        string sIdProGrid = "";
        string sObservaciones = "";
        string sReferencia = "";

        string sEmpresaEnvia = "";
        string sEstadoEnvia = "";
        string sFarmaciaEnvia = "";
        bool bEsCancelacionDeSalida = false;
        
        // Manejo automatico de Salidas 
        DllTransferenciaSoft.ObtenerInformacion.clsCliente ClienteTransferencias = 
            new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniOficinaCentral, General.DatosConexion);

        #endregion Variables

        public FrmRegistroSalidaPedidos()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);

            leer = new clsLeer(ref cnn);
            query = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name); 

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.Normal);
            grdProductos.EditModeReplace = true;

            GnFarmacia.AjustaColumnasImportes(grdProductos, (int)Cols.Costo, (int)Cols.Importe, (int)Cols.Descripcion);
        }

        private void FrmRegistroSalidaPedidos_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null,null);
        }

        #region Buscar Folio 
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            lblCancelado.Visible = false;
            IniciarToolBar(false, false, false, false); 

            if (bCargarPedido)
            {
                CargarPedido();
            }
            else
            {
                IniciarToolBar(true, false, false, false);
                CargarSalida();
            }

        }

        #region Cargar Pedido 
        private void CargarPedido()
        {
            string sStatus = "A";

            if (txtFolio.Text.Trim() != "")
            {
                txtFolio.Enabled = false;
                leer.DataSetClase = query.PedidosEnvioEnc(sEmpresaEnvia, sEstadoEnvia, sFarmaciaEnvia, txtFolio.Text, "CargarPedido");
                if (leer.Leer())
                {
                    IniciarToolBar(false, false, false, false);
                    txtFolio.Enabled = false;

                    sFolioSalida = leer.Campo("Folio");
                    txtFolio.Text = Fg.PonCeros(sFolioSalida, 8);

                    txtFarmaciaDestino.Enabled = false;
                    txtFarmaciaDestino.Text = leer.Campo("IdFarmacia");
                    lblFarmaciaDestino.Text = leer.Campo("Farmacia");
                    txtObservaciones.Text = leer.Campo("Observaciones");
                    sStatus = leer.Campo("Status");

                    if (sStatus.Trim() == "C")
                    {
                        lblCancelado.Visible = true;
                        lblCancelado.Text = "CANCELADA";
                        General.msjUser("El Folio de Salida ha sido cancelada.");
                        IniciarToolBar(false, false, true, false);
                    }

                    if (CargarDetallesPedido())
                    {
                        IniciarToolBar(false, true, false, false);
                    }
                }
                else
                {
                    txtFolio.Enabled = false;
                    txtFolio.Focus();
                }
            }
        }

        private bool CargarDetallesPedido()
        {
            bool bRegresa = false;

            leer.DataSetClase = query.PedidosEnvioDet(sEmpresaEnvia, sEstadoEnvia, sFarmaciaEnvia, txtFolio.Text, "CargarDetallesPedido");
            if (leer.Leer())
            {
                myGrid.LlenarGrid(leer.DataSetClase);
                bRegresa = CargarLotesPedido();
            }
            Totalizar();

            return bRegresa;
        }

        private bool CargarLotesPedido()
        {
            bool bRegresa = false;

            Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
            Lotes.ManejoLotes = OrigenManejoLotes.Transferencias;

            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.PedidosEnvioLotes(sEmpresaEnvia, sEstadoEnvia, sFarmaciaEnvia, txtFolio.Text, "CargarLotesPedido");
            Lotes.AddLotes(leer.DataSetClase);

            if (leer.Leer())
            {
                bRegresa = true;
                CargarLotesPedido_Entrada();
            }

            return bRegresa;
        }

        private bool CargarLotesPedido_Entrada()
        {
            bool bRegresa = false;

            LotesEntrada = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
            LotesEntrada.ManejoLotes = OrigenManejoLotes.Transferencias;

            query.MostrarMsjSiLeerVacio = false;
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.PedidosEnvioLotes_Entrada(sEmpresaEnvia, sEstadoEnvia, sFarmaciaEnvia, txtFolio.Text, sFarmacia, "CargarLotesPedido_Entrada");
            LotesEntrada.AddLotes(leer.DataSetClase);
            query.MostrarMsjSiLeerVacio = true;

            if (leer.Leer())
            {
                bRegresa = true;
            }

            return bRegresa;
        }
        #endregion Cargar Pedido

        #region Cargar Salida 
        private void CargarSalida()
        {
            string sStatus = "A";

            if (txtFolio.Text.Trim() == "")
            {
                IniciarToolBar(true, true, false, false);
                txtFolio.Text = "*";
                txtFolio.Enabled = false; 
            }
            else 
            {
                txtFolio.Enabled = false;
                leer.DataSetClase = query.PedidosDistEnc(sEmpresa, sEstado, sFarmacia, txtFolio.Text, "CargarSalida");
                if (!leer.Leer())
                {
                    btnNuevo_Click(null, null);
                    txtFolio.Focus();
                }
                else 
                {
                    IniciarToolBar(true, false, false, true);
                    txtFolio.Enabled = false;
                    bFolioGuardado = true; 

                    sFolioSalida = leer.Campo("Folio");
                    txtFolio.Text = Fg.PonCeros(sFolioSalida, 8);

                    txtFarmaciaDestino.Enabled = false;
                    txtFarmaciaDestino.Text = leer.Campo("IdFarmacia");
                    lblFarmaciaDestino.Text = leer.Campo("Farmacia");
                    txtObservaciones.Text = leer.Campo("Observaciones");
                    sStatus = leer.Campo("Status");

                    if (sStatus.Trim() == "C")
                    {
                        lblCancelado.Visible = true;
                        lblCancelado.Text = "CANCELADA";
                        General.msjUser("El Folio de Salida ha sido cancelada.");
                    }

                    if (!CargarDetallesSalida())
                    {
                        btnNuevo_Click(null, null);
                    } 
                } 
            }
        }

        private bool CargarDetallesSalida()
        {
            bool bRegresa = false;

            leer.DataSetClase = query.PedidosDistDet(sEmpresa, sEstado, sFarmacia, txtFolio.Text, "CargarDetallesSalida");
            if (leer.Leer())
            {
                myGrid.LlenarGrid(leer.DataSetClase);
                bRegresa = CargarLotesSalida();
            }

            myGrid.BloqueaGrid(true); 
            Totalizar();

            return bRegresa;
        }

        private bool CargarLotesSalida()
        {
            bool bRegresa = false;

            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.PedidosDistDet_Lotes(sEmpresa, sEstado, sFarmacia, txtFolio.Text, "CargarLotesSalida");
            Lotes.AddLotes(leer.DataSetClase);

            if (leer.Leer())
            {
                bRegresa = true;
            }

            return bRegresa;
        }
        #endregion Cargar Salida

        #endregion Buscar Folio

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            lblCancelado.Visible = false;

            if (!bCargarPedido)
            {
                // Inicializar el manejo de Lotes 
                Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
                Lotes.ModificarCantidades = false;
                Lotes.ManejoLotes = OrigenManejoLotes.Transferencias;

                myGrid.Limpiar(false);
                Fg.IniciaControles();
                IniciarToolBar(true, false, false, false);
            ////}
            ////else
            ////{
                ////txtFarmaciaDestino.Enabled = false; 
                ////txtObservaciones.Enabled = false;
                myGrid.Limpiar(true);
            }


            dtpFechaRegistro.Enabled = false;
            txtIdPersonal.Enabled = false;
            txtIdPersonal.Text = DtGeneral.IdPersonal;
            lblPersonal.Text = DtGeneral.NombrePersonal;

            txtSubTotal.Enabled = false;
            txtIva.Enabled = false;
            txtTotal.Enabled = false;

            txtFolio.Focus();

            bEsCancelacionDeSalida = false; 
            sReferencia = ""; 
            sIdTipoMovtoInv = "SPD";
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bEsCancelacionDeSalida = false; 
            sIdTipoMovtoInv = "SPD";
            sTipoES = "S";

            if (validarDatos())
            {
                if (cnn.Abrir())
                {
                    bool bExito = false;
                    IniciarToolBar(false,false,false,false); 
                    cnn.IniciarTransaccion();

                    if (GrabarMovtoEncabezado_Entrada())
                    {
                        // Generar el Movimiento de Inventario 
                        if (GrabarMovtoEncabezado())
                        {
                            // Generar el Folio de salida 
                            if (GrabarSalidaEncabezado())
                            {
                                bExito = AfectarExistencia(true, false);
                            }
                        }
                    }

                    if (!bExito)
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar la Salida.");
                        IniciarToolBar(false, true, false, false); 
                    }
                    else
                    {
                        cnn.CompletarTransaccion();

                        // IMach  // Enlazar el folio de inventario 
                        //IMachPtoVta.TerminarSolicitud(sFolioMovto);

                        General.msjUser(sMensajeGrabar);
                        // EnvioAutomaticoDeSalidas(); 
                        IniciarToolBar(true, false, false, true);
                        btnImprimir_Click(null, null);
                    }

                    cnn.Cerrar();
                }
                else
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo."); 
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            if (validarImpresion())
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_PedidoDistribucion.rpt";                

                myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("Folio", txtFolio.Text);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

                if (bRegresa)
                {
                    btnNuevo_Click(null, null);
                }
                else
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Botones

        #region Validacion de informacion
        private bool validarDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de transferencia inválido, verifique.");
                txtFolio.Focus();
            }

            if (bRegresa && txtFarmaciaDestino.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Farmacia destino de la Salida, verifique.");
                txtFarmaciaDestino.Focus();
            }

            if (bRegresa && ( DtGeneral.FarmaciaConectada == Fg.PonCeros(txtFarmaciaDestino.Text, 4)) )
            {
                bRegresa = false;
                General.msjUser("La farmacia destino no puede ser la misma que la farmacia conectada, verifique.");
                txtFarmaciaDestino.Focus();
            }

            if (bRegresa)
            {
                // if (sIdTipoMovtoInv == "SPD")
                if ( !bEsCancelacionDeSalida )
                {
                    if (txtObservaciones.Text.Trim() == "")
                    {
                        bRegresa = false;
                        General.msjUser("No ha capturado las observaciones para la Salida, verifique.");
                        txtObservaciones.Focus();
                    }
                }
                else
                {
                    sReferencia = "SPD" + txtFolio.Text;

                    clsObservaciones ob = new clsObservaciones();
                    ob.Encabezado = "Observaciones de Cancelación de Salida de Salida";
                    ob.MaxLength = 150;
                    ob.Show();
                    sObservaciones += "\n\n" + ob.Observaciones + "\t" + sReferencia;
                    bRegresa = ob.Exito;
                }
            }
             
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
                if (!bEsCancelacionDeSalida)
                {
                    ////VerificarLotes = new FrmVerificarSalidaLotes();
                    ////bRegresa = VerificarLotes.VerificarExistenciasConError(Lotes.DataSetLotes);
                    //bRegresa = validarExistenciasCantidadesLotes(false);
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

        private bool validarCancelacion()
        {
            bool bRegresa = true;
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
                General.msjUser("Debe capturar al menos un producto para la Salida\n y/o capturar cantidades para al menos un lote, verifique.");

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
                General.msjAviso("Se detecto una ó mas diferencias en la captura de productos, la Salida no puede ser completada.");
                FrmProductosConDiferencias f = new FrmProductosConDiferencias(dtsProductosDiferencias);
                f.ShowDialog();
            }

            return bRegresa;
        }

        private bool validarImpresion()
        {
            bool bRegresa = true;
            return bRegresa;
        }
        #endregion Validacion de informacion

        #region Guardar movimiento de inventario

        #region Movimientos de Entrada 
        private bool GrabarMovtoEncabezado_Entrada()
        {
            bool bRegresa = true;            
            string sTipoEntrada = "E";

            sFolioMovtoEntrada = "";


            if (bCargarPedido)
            {
                //Si existen lotes a registrar.
                if (LotesEntrada.DataSetLotes.Tables[0].Rows.Count > 0)
                {
                    string sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                        sEmpresa, sEstado, sFarmacia, "*", sIdTipoMovtoInv_Entrada, sTipoEntrada, sReferencia,
                        DtGeneral.IdPersonal, sObservaciones,
                        txtSubTotal.Text.Trim().Replace(",", ""), txtIva.Text.Trim().Replace(",", ""), txtTotal.Text.Trim().Replace(",", ""), 1);
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                    }
                    else
                    {
                        leer.Leer();
                        sFolioMovtoEntrada = leer.Campo("Folio");
                        bRegresa = GrabarMovtoDetalle_Entrada();

                        if (bRegresa)
                        {
                            if (!AfectarExistencia_Entrada(true, false))
                            {
                                bRegresa = false;
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarMovtoDetalle_Entrada()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdProducto = "", sCodigoEAN = "";
            int iCantidad = 0;
            double nCosto = 0, nImporte = 0, nTasaIva = 0;
            int iUnidadDeSalida = 0;


            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                nTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                nCosto = myGrid.GetValueDou(i, (int)Cols.Costo);
                nImporte = myGrid.GetValueDou(i, (int)Cols.Importe);
                iUnidadDeSalida = myGrid.GetValueInt(i, (int)Cols.TipoCaptura);

                if (sIdProducto != "")
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos '{0}', '{1}', '{2}', '{3}' " +
                                         "Exec spp_Mtto_FarmaciaProductos_CodigoEAN '{0}', '{1}', '{2}', '{3}', '{4}' ",
                                         sEmpresa, sEstado, sFarmacia, sIdProducto, sCodigoEAN);
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_Det '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                            sEmpresa, sEstado, sFarmacia, sFolioMovtoEntrada, sIdProducto, sCodigoEAN, iUnidadDeSalida,
                            nTasaIva, iCantidad, nCosto, nImporte, 'A');
                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                        else
                        {
                            if (!GrabarMovtoDetalleLotes_Entrada(sIdProducto, sCodigoEAN, nCosto))
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarMovtoDetalleLotes_Entrada(string IdProducto, string CodigoEAN, double Costo)
        {
            bool bRegresa = true;
            string sSql = "";

            clsLotes[] ListaLotes = LotesEntrada.Lotes(IdProducto, CodigoEAN);

            foreach (clsLotes L in ListaLotes)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
                                         sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, IdProducto, CodigoEAN, L.ClaveLote, General.FechaYMD(L.FechaCaducidad, "-"), DtGeneral.IdPersonal);
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        // Registrar el producto en las tablas de existencia 
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                            sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovtoEntrada, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A');
                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool AfectarExistencia_Entrada(bool Aplicar, bool AfectarCosto)
        {
            AfectarInventario Inv = AfectarInventario.Ninguno;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            if (Aplicar)
                Inv = AfectarInventario.Aplicar;

            if (AfectarCosto)
                Costo = AfectarCostoPromedio.Afectar;

            string sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                sEmpresa, sEstado, sFarmacia, sFolioMovtoEntrada, (int)Inv, (int)Costo);

            bool bRegresa = leer.Exec(sSql);
            return bRegresa;
        }

        #endregion Movimientos de Entrada 

        #region Movimientos de Salida
        private bool GrabarMovtoEncabezado()
        {
            bool bRegresa = true;
            // string sObservaciones = txtObservaciones.Text.Trim(); 
            sFolioMovto = "";

            string sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                sEmpresa, sEstado, sFarmacia, "*", sIdTipoMovtoInv, sTipoES, sReferencia,
                DtGeneral.IdPersonal, sObservaciones,
                txtSubTotal.Text.Trim().Replace(",", ""), txtIva.Text.Trim().Replace(",", ""), txtTotal.Text.Trim().Replace(",", ""), 1);
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sFolioMovto = leer.Campo("Folio");
                bRegresa = GrabarMovtoDetalle();
            }

            return bRegresa;
        }

        private bool GrabarMovtoDetalle()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdProducto = "", sCodigoEAN = "";
            int iCantidad = 0;
            double nCosto = 0, nImporte = 0, nTasaIva = 0;
            int iUnidadDeSalida = 0;


            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                nTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                nCosto = myGrid.GetValueDou(i, (int)Cols.Costo);
                nImporte = myGrid.GetValueDou(i, (int)Cols.Importe);
                iUnidadDeSalida = myGrid.GetValueInt(i, (int)Cols.TipoCaptura);

                if (sIdProducto != "")
                {                    
                    sSql = string.Format("Exec spp_Mtto_MovtosInv_Det '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                        sEmpresa, sEstado, sFarmacia, sFolioMovto, sIdProducto, sCodigoEAN, iUnidadDeSalida,
                        nTasaIva, iCantidad, nCosto, nImporte, 'A');
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        if (!GrabarMovtoDetalleLotes(sIdProducto, sCodigoEAN, nCosto))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                    
                }
            }

            return bRegresa;
        }

        private bool GrabarMovtoDetalleLotes(string IdProducto, string CodigoEAN, double Costo)
        {
            bool bRegresa = true;
            string sSql = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            foreach (clsLotes L in ListaLotes)
            {
                if (L.Cantidad > 0)
                {                    
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A');
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }                    
                }
            }

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
        #endregion Movimientos de Salida 

        #endregion Guardar movimiento de inventario

        #region Guardar datos Salida
        private bool GrabarSalidaEncabezado()
        {
            bool bRegresa = true;
            string sFolio = "*", sFolioReferencia = txtFolio.Text.Trim();

            string sSql = string.Format("Exec spp_Mtto_PedidosDistEnc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', " + 
                                      " '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}' ",
                                      sEmpresa, sEstado, sCveRenapo, sFarmacia, "00", 0, sFolio, sFolioMovto, "", sFolioReferencia, sIdTipoMovtoInv,
                                      0, DtGeneral.IdPersonal, txtObservaciones.Text, txtSubTotal.Text, txtIva.Text, txtTotal.Text,
                                      sEstado, sCveRenapo, txtFarmaciaDestino.Text, "00");

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "GrabarSalidaEncabezado()");
            }
            else
            {
                leer.Leer();
                sFolioSalida = leer.Campo("FolioDistribucion");
                txtFolio.Text = sFolioSalida;
                sMensajeGrabar = leer.Campo("Mensaje");
                bRegresa = GrabarSalidaDetalle();
            }

            return bRegresa;
        }

        private bool GrabarSalidaDetalle()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdProducto = "", sCodigoEAN = "";
            int iCantidad = 0;
            double nCosto = 0, nTasaIva = 0, nSubTotal = 0, nImporteIva = 0, nImporte = 0;
            int iUnidadDeSalida = 0;
            

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                nTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                nCosto = myGrid.GetValueDou(i, (int)Cols.Costo);

                nSubTotal = myGrid.GetValueDou(i, (int)Cols.Importe);
                nImporteIva = myGrid.GetValueDou(i, (int)Cols.ImporteIva);
                nImporte = myGrid.GetValueDou(i, (int)Cols.ImporteTotal);
                iUnidadDeSalida = myGrid.GetValueInt(i, (int)Cols.TipoCaptura);

                if (sIdProducto != "")
                {
                    sSql = string.Format("Exec spp_Mtto_PedidosDistDet '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', " +
                              " '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}' ",
                              sEmpresa, sEstado, sFarmacia, sFolioSalida,
                              sIdProducto, sCodigoEAN, i, iUnidadDeSalida, iCantidad, 0, iCantidad, nCosto, nTasaIva, nSubTotal, nImporteIva, nImporte);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        if (!GrabarSalidaDetalleLotes(i, sIdProducto, sCodigoEAN, nCosto))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarSalidaDetalleLotes(int Renglon, string IdProducto, string CodigoEAN, double Costo)
        {
            bool bRegresa = true;
            string sSql = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            foreach (clsLotes L in ListaLotes)
            {
                if (L.Cantidad > 0)
                {
                    sSql = string.Format("Exec spp_Mtto_PedidosDistDet_Lotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                       sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioSalida, IdProducto, CodigoEAN, L.ClaveLote, Renglon.ToString(), L.Cantidad.ToString());
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }
        #endregion Guardar datos Salida

        #region Manejo Grid
        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (!bFolioGuardado)
            {
                if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                {
                    if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "" && myGrid.GetValueInt(myGrid.ActiveRow, (int)Cols.Cantidad) > 0)
                    {
                        myGrid.Rows = myGrid.Rows + 1;
                        myGrid.ActiveRow = myGrid.Rows;
                        myGrid.SetActiveCell(myGrid.Rows, 1);
                    }
                }
            }
        } 

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            switch (ColActiva)
            {
                case Cols.CodEAN:
                    string sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                    if (sValor != "")
                    {
                        if (EAN.EsValido(sValor))
                        {
                            leer.DataSetClase = query.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sValor, "grdProductos_EditModeOff");
                            if (leer.Leer())
                            {
                                CargarDatosProducto();
                            }
                            else
                            {
                                myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN);
                            }
                        }
                        else
                        {
                            //General.msjError(sMsjEanInvalido);
                            myGrid.LimpiarRenglon(myGrid.ActiveRow);
                            myGrid.ActiveCelda(myGrid.ActiveRow, (int)Cols.CodEAN);
                            SendKeys.Send("");
                        }
                    }
                    else
                    {
                        myGrid.LimpiarRenglon(myGrid.ActiveRow);
                    }
                    break;
            }
            Totalizar();
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
        }

        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            int iRowActivo = myGrid.ActiveRow;

            switch (ColActiva)
            {
                case Cols.CodEAN:
                case Cols.Descripcion:
                case Cols.Cantidad:
                case Cols.Importe:

                    if (!bFolioGuardado)
                    {
                        if (e.KeyCode == Keys.F1)
                        {
                            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                            leer.DataSetClase = ayuda.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, false, "grdProductos_KeyDown_1");
                            if (leer.Leer())
                            {
                                myGrid.SetValue(myGrid.ActiveRow, 1, leer.Campo("CodigoEAN"));
                                CargarDatosProducto();
                            }
                        }

                        if (e.KeyCode == Keys.Delete)
                        {
                            if (!bCargarPedido)
                            {
                                removerLotes();
                            }
                        }
                    }

                    break;
            }
        }

        private void removerLotes()
        {
            if (!bFolioGuardado)
            {
                if (!bCargarPedido)
                {
                    try
                    {
                        int iRow = myGrid.ActiveRow;
                        Lotes.RemoveLotes(myGrid.GetValue(iRow, (int)Cols.Codigo), myGrid.GetValue(iRow, (int)Cols.CodEAN));
                        myGrid.DeleteRow(iRow);
                    }
                    catch { }

                    if (myGrid.Rows == 0)
                    {
                        myGrid.Limpiar(true);
                    }
                }
            }
        }

        private bool CargarDatosProducto()
        {
            bool bRegresa = true;
            int iRow = myGrid.ActiveRow;
            int iColEAN = (int)Cols.CodEAN;
            bool bEsMach4 = false; 
            string sCodEAN = leer.Campo("CodigoEAN");

            if (sValorGrid != sCodEAN)
            {
                if (!myGrid.BuscaRepetido(sCodEAN, iRow, iColEAN))
                {
                    // No modificar la informacion capturada en el renglon si este ya existia
                    myGrid.SetValue(iRow, iColEAN, sCodEAN);
                    myGrid.SetValue(iRow, (int)Cols.Descripcion, leer.Campo("Descripcion"));
                    myGrid.SetValue(iRow, (int)Cols.TasaIva, leer.Campo("TasaIva"));

                    //if (sIdProGrid != leer.Campo("CodigoEAN"))
                    //if (sValorGrid != leer.Campo("CodigoEAN"))
                    {
                        sIdProGrid = leer.Campo("IdProducto");
                        myGrid.SetValue(iRow, (int)Cols.Codigo, sIdProGrid);
                        myGrid.SetValue(iRow, (int)Cols.Cantidad, 0);
                        myGrid.SetValue(iRow, (int)Cols.Costo, leer.CampoDouble("CostoPromedio"));
                        myGrid.SetValue(iRow, (int)Cols.TipoCaptura, "0");

                        ////bEsMach4 = leer.CampoBool("EsMach4");
                        ////myGrid.SetValue(iRow, (int)Cols.EsIMach4, bEsMach4);

                        myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRow, (int)Cols.CodEAN);
                    }

                    ////// Marcar el Renglon como Codigo de Robot si la Farmacia conectada tiene Robot 
                    //if (IMach4.EsClienteIMach4)
                    //{
                    //    if (bEsMach4)
                    //    {
                    //        GnFarmacia.ValidarCodigoIMach4(myGrid, bEsMach4, iRow);
                    //        IMachPtoVta.Show(leer.Campo("IdProducto"), sCodEAN);
                    //    }
                    //}

                    CargarLotesCodigoEAN();
                }
                else
                {
                    General.msjUser("El producto ya fue capturado en otro renglon, verifique.");
                    myGrid.LimpiarRenglon(iRow);
                    myGrid.SetActiveCell(iRow, iColEAN);
                    myGrid.EnviarARepetido(); 
                }
            }
            else
            {
                // Asegurar que no cambie el CodigoEAN
                myGrid.SetValue(iRow, iColEAN, sCodEAN);
            }

            return bRegresa;
        }

        private void Totalizar()
        {
            txtSubTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.Importe).ToString(sFormato);
            txtIva.Text = myGrid.TotalizarColumnaDou((int)Cols.ImporteIva).ToString(sFormato);
            txtTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal).ToString(sFormato);
        }
        #endregion Manejo Grid

        #region Manejo de lotes
        private void CargarLotesCodigoEAN()
        {
            int iRow = myGrid.ActiveRow;
            string sCodigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
            string sCodEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);

            leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Todos, false, "CargarLotesCodigoEAN()");
            if (query.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                leer.Leer();
                Lotes.AddLotes(leer.DataSetClase);
                mostrarOcultarLotes();
            }
        }

        private void mostrarOcultarLotes()
        {
            // Asegurar que el Grid tenga el Foco.
            if (this.ActiveControl.Name.ToUpper() == grdProductos.Name.ToUpper())
            {
                int iRow = myGrid.ActiveRow;

                if (myGrid.GetValue(iRow, (int)Cols.Codigo) != "")
                {
                    Lotes.Codigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
                    Lotes.CodigoEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);
                    Lotes.Descripcion = myGrid.GetValue(iRow, (int)Cols.Descripcion);
                    Lotes.EsEntrada = false;
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = false;
                    Lotes.ModificarCantidades = !bCargarPedido;

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;
                    Lotes.Show(); 

                    //////// Administracion de Mach4
                    //////if (IMach4.EsClienteIMach4 && myGrid.GetValueBool(iRow, (int)Cols.EsIMach4))
                    //////{
                    //////    if (IMachPtoVta.RequisicionRegistrada)
                    //////    {
                    //////        Lotes.Show();
                    //////    } 
                    //////} 
                    //////else 
                    //////{ 
                    //////    Lotes.Show(); 
                    //////}

                    myGrid.SetValue(iRow, (int)Cols.Cantidad, Lotes.Cantidad);
                    myGrid.SetValue(iRow, (int)Cols.TipoCaptura, Lotes.TipoCaptura);
                    myGrid.SetActiveCell(iRow, (int)Cols.Costo);

                    Totalizar();
                }
                else
                {
                    myGrid.SetActiveCell(iRow, (int)Cols.CodEAN);
                }
            }
        } 
        #endregion Manejo de lotes 

        #region Funciones
        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.G:
                    if (btnGuardar.Enabled)
                    {
                        btnGuardar_Click(null, null);
                    }
                    break;

                case Keys.N:
                    if (btnNuevo.Enabled)
                    {
                        btnNuevo_Click(null, null);
                    }
                    break;

                case Keys.P:
                    if (btnImprimir.Enabled)
                    {
                        btnImprimir_Click(null, null);
                    }
                    break;

                default:
                    break;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control)
                TeclasRapidas(e);

            switch (e.KeyCode)
            {
                case Keys.F7:
                    mostrarOcultarLotes();
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        private void txtSubTotal_Validating(object sender, CancelEventArgs e)
        {
            txtSubTotal.Text = Fg.Format(txtSubTotal.Text, 4);
        }

        private void txtIva_Validating(object sender, CancelEventArgs e)
        {
            txtIva.Text = Fg.Format(txtIva.Text, 4);
        }

        private void txtTotal_Validating(object sender, CancelEventArgs e)
        {
            txtTotal.Text = Fg.Format(txtTotal.Text, 4);
        }

        private void IniciarToolBar( bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        public void MostrarDetalles(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio)
        {
            bCargarPedido = true;
            // bPermitirCapturaDeSalidas = false; 

            sEmpresaEnvia = IdEmpresa;
            sEstadoEnvia = IdEstado;
            sFarmaciaEnvia = IdFarmacia;
            txtFolio.Text = Folio;

            IniciarToolBar(false, false, false, false);
            txtFolio_Validating(null, null);
            this.ShowDialog();
        }
        #endregion Funciones 

        #region Farmacias Destino 
        private void txtFarmaciaDestino_Validating(object sender, CancelEventArgs e)
        {
            if (txtFarmaciaDestino.Text != "")
            {
                leer.DataSetClase = query.Farmacias(sEstado, txtFarmaciaDestino.Text, "txtFarmaciaDestino_Validating");
                if (!leer.Leer())
                {
                    txtFarmaciaDestino.Text = "";
                    ////General.msjUser("Clave de Farmacia no encontrada, verifique."); 
                    txtFarmaciaDestino.Focus(); 
                }
                else
                {
                    CargarFarmaciaDestino(); 
                }
            }
        } 

        private void txtFarmaciaDestino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Farmacias("", sEstado);
                if (leer.Leer())
                {
                    CargarFarmaciaDestino(); 
                }
            }
        }

        private void CargarFarmaciaDestino()
        {
            txtFarmaciaDestino.Text = leer.Campo("IdFarmacia");
            lblFarmaciaDestino.Text = leer.Campo("Farmacia");
        }
        #endregion Farmacias Destino 

    } // Clase 
}
