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

namespace Almacen.Pedidos
{
    public partial class FrmActualizarSurtidoPedidos : FrmBaseExt
    {
        #region Redimension de controles
        int iAnchoPantalla = (int)(General.Pantalla.Ancho * 1.0);
        int iAltoPantalla = (int)(General.Pantalla.Alto * 0.80);
        int iAnchoGrid_Concentrado = 0;
        int iAnchoGrid_Detallado = 0; 
        int iAnchoGrid_Ajuste = 0;
        int iAnchoGrid_Ajuste_Detalle = 0; 
        int iDiferenciaGrid = 0;
        int iDiferenciaGrid_Detalle = 0;

        int iAnchoFrameInformacion = 0;
        int iAnchoFrameInformacion_Ajuste = 0;
        int iDiferenciaFrameInformacion = 0;
        #endregion Redimension de controles 

        private enum ColsConcentrado
        {
            Ninguna = 0,
            ClaveSSA = 1, DescripcionSal = 2, Cant_Requerida = 3, Cant_Asignada = 4
        }

        private enum ColsDetalles
        {
            Ninguna = 0,
            Id, IdSurtimiento, ClaveSSA, DescripcionSal, IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, Presentacion, 
            SKU, 
            Lote, Caducidad, 
            IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño,

            Caja, Caja_Final, 

            Cant_Requerida_Caja, Cant_Requerida, Cant_Asignada,
            Observaciones, Validado, segmento

        }
        
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);        
        clsLeer leer, leerGuardar;             
        clsGrid GridDetalles, GridConcentrado_;
        clsDatosCliente DatosCliente;
        DataSet dtsDetalles, dtsGridDetalles;
        DataSet dtsDetalles_CajasEmbalaje; 
        DataSet dtsDetalles_Incidencias;
        DataSet dtsNuevo;
        int iRenglonActivo = 0;
        int iPedidoManual = 0;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");        
        string sFolioSurtido = "";
        int iTipoDeCaptura = 0;
        string sIdPersonalSurtido = "";

        int iCantRequerida = 0;
        int iCantAsignada = 0;
        string sFormato = "###,###,##0";
        bool bEsSurtimiento = true;
        bool bEsValidacion = false;
        bool bEnviar_A_Distribucion = false;
        int iCantidadesInvalidas = 0;
        bool bActivar_RegistroDeIncidencias = false; 

        clsConsultas Consultas;
        clsAyudas Ayudas;

        Color cColor_Validado = Color.LimeGreen;
        Color cColor_NoValidado = Color.White;

        bool bMostrar_SKU_Surtidos = false;
        float fAncho_SKU = 0;

        string sClaveSSA = "";
        string sSubFarmacia = "";
        string sProducto = "";
        string sCodigoEAN = "";
        string sLote = "";
        int iSurtimiento = 0;
        int iPasillo = 0;
        int iEstante = 0;
        int iEntrepaño = 0;
        int iCantidadAsignada = 0;
        int iCaja_Inicial = 0;
        int iCaja_Final = 0;
        string sObservaciones = "";
        int iValidado = 0;
        string sSKU = "";

        public FrmActualizarSurtidoPedidos()
        {
            InitializeComponent();

            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(1200, 500);


            General.Pantalla.AjustarTamaño(this, 90, 80);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            GrupoProductos.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top; 
            
            
            leer = new clsLeer(ref cnn);
            leerGuardar = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);

            chkMostrarSKU.BackColor = General.BackColorBarraMenu;

            lblMensaje_03.BackColor = lblMensaje.BackColor; 

            GridDetalles = new clsGrid(ref grdDetalles, this);
            GridDetalles.EstiloGrid(eModoGrid.ModoRow);
            grdDetalles.EditModeReplace = true;
            GridDetalles.BackColorColsBlk = Color.WhiteSmoke;
            
            GridDetalles.AjustarAnchoColumnasAutomatico = true;
            fAncho_SKU = GridDetalles.GetAnchoColuma(ColsDetalles.SKU);


            //GridConcentrado = new clsGrid(ref grdConcentrado, this);
            //GridConcentrado.EstiloGrid(eModoGrid.ModoRow);
            grdConcentrado.EditModeReplace = true;
            //GridConcentrado.BackColorColsBlk = Color.WhiteSmoke;


            GridDetalles.SetOrder((int)ColsDetalles.ClaveSSA, 1, true);
            GridDetalles.SetOrder((int)ColsDetalles.CodigoEAN, 1, true); 

            //AjustarTamaño_Pantalla();
        }

        private void FrmActualizarSurtidoPedidos_Load(object sender, EventArgs e)
        {
            LimpiaPantalla();

            string sMensaje_Tools = " << F10 >> Registrar incidencia de surtido  "; 

            //////btnImprimir.Enabled = false;
            //////btnImprimir.Visible = false;
            //////toolStripSeparator3.Visible = false; 
            lblMensajeMenuCaptura.Visible = false;
            // bEsSurtimiento = true; 

            GridDetalles.OcultarColumna(true, (int)ColsDetalles.Validado, (int)ColsDetalles.Validado);

            if (iTipoDeCaptura == 1) 
            {
                this.Text = "Registro de surtimiento de pedidos";

                ////sMensaje_Tools += string.Format("               << F10 >> Registrar incidencia de surtido"); 
            }

            if (iTipoDeCaptura == 2) 
            {
                this.Text = "Modificación de surtimiento de pedidos";

                ////sMensaje_Tools += string.Format("               << F10 >> Registrar incidencia de surtido");
            }

            if (iTipoDeCaptura == 3) 
            {
                GridDetalles.OcultarColumna(false, (int)ColsDetalles.Validado, (int)ColsDetalles.Validado);
                GridDetalles.BloqueaColumna(true, (int)ColsDetalles.Cant_Asignada);

                this.Text = "Validación de surtimiento de pedidos"; 

                bEsSurtimiento = false;
                bEsValidacion = true;
                lblMensajeMenuCaptura.Visible = true;
                grdDetalles.ContextMenuStrip = menuCantidades; 
                ////btnImprimir.Enabled = true;
                ////btnImprimir.Visible = true;
            }

            if (iPedidoManual == 1)
            {
                this.Text += " (Manual) ";
                GridDetalles.PonerEncabezado((int)ColsDetalles.Cant_Requerida, "Existencia");
                txtCodifoEAN.Enabled = false;
            }

            //////if (GnFarmacia.ManejaCajasDeDistribucion)
            //////{
            //////    ValidaCajas();
            //////}

            ////GridDetalles.BloqueaColumna(!GnFarmacia.ManejaCajasDeDistribucion, (int)ColsDetalles.Caja);

            bActivar_RegistroDeIncidencias = (iTipoDeCaptura == 1 || iTipoDeCaptura == 2);
            //lblMensaje.Text = sMensaje_Tools; 
            lblMensaje_02.Text = bActivar_RegistroDeIncidencias ? sMensaje_Tools : "";


            btnAsignarCajas.Enabled = GnFarmacia.ManejaCajasDeDistribucion; 
            btnAsignarCajas.Visible = GnFarmacia.ManejaCajasDeDistribucion;

            btnAsignarCajas.Enabled = false;
            btnAsignarCajas.Visible = false; 
        }

        #region Redimensionar form
        private void AjustarTamaño_Pantalla()
        {
            double iPorcentajeAnchoPantalla = 0.98;
            double iPorcentajeAltoPantalla = 0.85;
            iAnchoGrid_Concentrado = grdConcentrado.Width;
            iAnchoGrid_Detallado = grdDetalles.Width;

            iAnchoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Width * iPorcentajeAnchoPantalla);
            iAltoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Height * iPorcentajeAltoPantalla);


            this.Width = iAnchoPantalla;
            this.Height = iAltoPantalla;

            //int iAnchoFrameInformacion = 0;
            //int iAnchoFrameInformacion_Ajuste = 0;
            //int iDiferenciaFrameInformacion = 0; 

            grdDetalles.Width = (int)(grdDetalles.Width * .85);

            iAnchoGrid_Ajuste = grdConcentrado.Width;
            iAnchoGrid_Ajuste_Detalle = grdDetalles.Width; 
            iDiferenciaGrid = iAnchoGrid_Ajuste - iAnchoGrid_Concentrado;
            iDiferenciaGrid_Detalle = iAnchoGrid_Ajuste_Detalle - iAnchoGrid_Detallado; 

            ////iAnchoFrameInformacion_Ajuste = FrameInformacion.Width;
            ////iDiferenciaFrameInformacion = iAnchoFrameInformacion_Ajuste - iAnchoFrameInformacion;


            AjustarGrid_Concentrado();
            //AjustarGrid_Detalles();
            GridDetalles.AjustarAnchoColumnasAutomatico = true;
        }

        private void AjustarGrid_Concentrado()
        {
            double dPorcentajeAjuste = 0;
            double dAnchoActual = 0;
            double dAnchoAjuste = 0;
            double dAnchoNuevo = 0;

            double iClaveSSA = iDiferenciaGrid * 0.075; 
            double iCantidadRequerida = iDiferenciaGrid * 0.075;
            double iCantidadAsignada = iDiferenciaGrid * 0.075;


            dPorcentajeAjuste = ((double)iAnchoGrid_Concentrado / (double)iAnchoGrid_Ajuste);
            dPorcentajeAjuste = ((double)iDiferenciaGrid / (double)iAnchoGrid_Concentrado);

            dAnchoActual = grdConcentrado.Sheets[0].Columns[(int)ColsConcentrado.ClaveSSA - 1].Width + iClaveSSA;
            grdConcentrado.Sheets[0].Columns[(int)ColsConcentrado.ClaveSSA - 1].Width = (float)dAnchoActual;

            dAnchoActual = grdConcentrado.Sheets[0].Columns[(int)ColsConcentrado.Cant_Requerida - 1].Width + iCantidadRequerida;
            grdConcentrado.Sheets[0].Columns[(int)ColsConcentrado.Cant_Requerida - 1].Width = (float)dAnchoActual;

            dAnchoActual = grdConcentrado.Sheets[0].Columns[(int)ColsConcentrado.Cant_Asignada - 1].Width + iCantidadAsignada;
            grdConcentrado.Sheets[0].Columns[(int)ColsConcentrado.Cant_Asignada - 1].Width = (float)dAnchoActual; 

        }

        private void AjustarGrid_Detalles()
        {
            int iCols = 0; 
            double dPorcentajeAjuste = 0;
            double dAnchoActual = 0;
            double dAnchoAjuste = 0;
            double dAnchoNuevo = 0; 
            double dAnchoTotal_Columas = 0 ;

            double iPorcentaje = iDiferenciaGrid_Detalle / GridDetalles.ColumnasVisibles;

            iPorcentaje *= .58;

            for (int i = 0; i <= grdDetalles.Sheets[0].Columns.Count - 1; i++)
            {
                if (grdDetalles.Sheets[0].Columns[i].Visible)
                {
                    dAnchoActual = grdDetalles.Sheets[0].Columns[i].Width + iPorcentaje;
                    grdDetalles.Sheets[0].Columns[i].Width = (float)dAnchoActual;
                }
            }
        }
        #endregion Redimensionar form

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            //GridConcentrado.Limpiar(false);
            GridDetalles.Limpiar(false);

            Mostrar_Columna_SKU();

            iCantRequerida = 0;
            iCantAsignada = 0;

            CargaEncabezado();
            //CargarConcentrado();
            CargarDetalles();
            

            ////////CargarGridDetalles();
            ////// Asegurar la carga de la pantalla 
            //dtsDetalles = dtsNuevo != null ? dtsNuevo.Copy(): new DataSet();
            //dtsDetalles_Incidencias = dtsNuevo != null ? dtsNuevo.Copy() : new DataSet();

        }

        //private void CalcularCantidades()
        //{
        //    int iCant_Requerida = 0, iCant_Asignada = 0;
        //    string sClaveSSA;

        //    sClaveSSA = lblClaveSSA.Text;

        //    for (int i = 1; i <= GridConcentrado.Rows; i++)
        //    {

        //        if (sClaveSSA == GridConcentrado.GetValue(i, (int)ColsConcentrado.ClaveSSA))
        //        {
        //            iCant_Requerida = GridConcentrado.GetValueInt(i, (int)ColsConcentrado.Cant_Requerida);
        //            iCant_Asignada = GridConcentrado.GetValueInt(i, (int)ColsConcentrado.Cant_Asignada);
        //        }
        //    }

        //    lblCantReq.Text = iCant_Requerida.ToString(sFormato);
        //    lblCantAsignada.Text = iCant_Asignada.ToString(sFormato);
        //}
        #endregion Funciones

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            if(ValidarGuardar()) 
            {
                GrabarInformacion_General(); 
            }

        }

        private bool ValidarGuardar()
        {
            bool bRegresa = true;
            int iRegistros = 0;
            string sMensaje = "";

            //SendKeys.Send("{TAB}");
            //ActualizarInformacion_Memoria();

            int iCant_Requerida = GridDetalles.TotalizarColumna((int)ColsDetalles.Cant_Requerida);
            int iCant_Surtida = GridDetalles.TotalizarColumna((int)ColsDetalles.Cant_Asignada);

            if (iCant_Requerida < iCant_Surtida)
            {
                bRegresa = false;
                General.msjAviso("Se detectaron Claves con cantidades asignadas mayores a las solicitadas, verifique.");
            }

            if (bRegresa)
            {
                if (iCant_Surtida == 0)
                {
                    bRegresa = false;
                    General.msjAviso("No se puede guardar si no tiene una cantidad asignada.");
                }
            }

            if (!bEsSurtimiento)
            {
                for(int i = 1; i <= GridDetalles.Rows; i++)
                {
                    iRegistros += GridDetalles.GetValueBool(i, (int)ColsDetalles.Validado) ? 0: 1 ;
                }

                if (iRegistros > 0)
                {
                    bRegresa = false;
                    sMensaje = string.Format("Se detectaron {0} registros sin validar. \n\n¿ Desea guardar sin enviar a Documentación ?", iRegistros);

                    if(General.msjConfirmar(sMensaje) == DialogResult.Yes)
                    {
                        bRegresa = true;
                    }
                    else
                    {
                        VerificarValidado();
                    }
                }
                else
                {
                    bEnviar_A_Distribucion = Confirmar_Envio_Distribucion();
                }
            }

            return bRegresa;
        }
       

        private void GrabarInformacion_General()
        {
            if (bEsSurtimiento)
            {
                if (AsignarSurtidor())
                {
                    if (Grabar_Personal_Surtido())
                    {
                        if (GuardarSurtimiento())
                        {
                            Graba_Atencion_Surtido();
                        }
                    }
                }
            }
            else
            {
                if (GuardarSurtimiento())
                {
                    if (bEnviar_A_Distribucion)
                    {
                        if (Enviar_A_Distribucion())
                        {
                            Graba_Atencion_Surtido();
                        }
                    }
                    else
                    {
                        this.Hide();
                    }
                }
            }

        }

        private bool Confirmar_Envio_Distribucion()
        {
            bool bRegresa = false; 
            string message = "El folio de surtido se enviara de Validación a Documentación, ¿ Desea continuar ? ";

            bRegresa = General.msjConfirmar(message) == DialogResult.Yes; 

            return bRegresa; 
        }

        private bool Enviar_A_Distribucion()
        {
            bool bRegresa = false;
            string message = "El folio de surtido se enviara de Validación a Distribución, ¿ Desea continuar ? ";

            ////if (General.msjConfirmar(message) == DialogResult.Yes)
            {
                string sSql = string.Format(
                    "Update Pedidos_Cedis_Enc_Surtido Set Status = 'D' " +
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioSurtido = '{3}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CargarFoliosSurtido()");
                    General.msjError("Ocurró un error al actualizar el status del Folio de Surtido.");
                    // this.Close();
                }
                else
                {
                    bRegresa = true;
                    General.msjAviso("El folio de surtido ha sido enviado a distribución exitosamente.");
                    this.Hide(); 
                }
            }

            return bRegresa;
        }

        private bool AsignarSurtidor()
        {
            bool bRegresa = false;

            if(DtGeneral.IdPersonal == DtGeneral.IdPersonalCEDIS_Relacionado)
            {
                bRegresa = true;
                sIdPersonalSurtido = DtGeneral.IdPersonalCEDIS;
            }
            else
            {
                FrmAsignarSurtidor f = new FrmAsignarSurtidor();
                f.ShowDialog(this);

                if(f.SurtidorAsignado)
                {
                    bRegresa = true;
                    sIdPersonalSurtido = f.IdPersonalSurtido;
                }
            }
            return bRegresa; 
        }

        private bool GuardarSurtimiento() 
        {
            bool bRegresa = false;

            iRenglonActivo = 0; 
            //CargarGridDetalles(GridConcentrado.ActiveRow);
            //CalcularCantidades(GridConcentrado.ActiveRow);
            //ActualizarConcentrado();

            if (validarCantidades())
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    cnn.IniciarTransaccion();

                    if (GuardaInformacion())
                    {
                        bRegresa = true;

                        if (bEsSurtimiento)
                        {
                            bRegresa = ActualizarStatusSurtimiento();
                        }
                    }

                    if ( bRegresa ) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        cnn.CompletarTransaccion();

                        if (bEsSurtimiento)
                        {
                            General.msjUser("La información guardada satisfactoriamente.");
                            this.Close();
                        }
                    }
                    else
                    {
                        Error.GrabarError(leer, "btnGuardar_Click");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la información.");
                    }

                    cnn.Cerrar();
                }
            }

            return bRegresa; 
        }

        private bool Grabar_Personal_Surtido()
        {
            bool bRegresa = false;

            string sSql = string.Format(" Update Pedidos_Cedis_Enc_Surtido Set IdPersonalSurtido = '{4}' " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioSurtido = '{3}' ",
                sEmpresa, sEstado, sFarmacia, sFolioSurtido, sIdPersonalSurtido );

            bRegresa = leer.Exec(sSql); 

            return bRegresa;
        }

        private void Graba_Atencion_Surtido()
        {
            bool bRegresa = true;
            
            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                cnn.IniciarTransaccion();
                string sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones \n" +
                    "@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @IdPersonal = '{4}', @Observaciones = '{5}' \n",
                    sEmpresa, sEstado, sFarmacia, sFolioSurtido, DtGeneral.IdPersonal, "");

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                }

                if (bRegresa) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    cnn.CompletarTransaccion();
                }
                else
                {
                    Error.GrabarError(leer, "btnGuardar_Click");
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al guardar la información de Atencion Surtido.");
                }

                cnn.Cerrar();
            }            
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones
        
        #region CargarPedido
        public void CargarPedido(string FolioSurtido, int TipoCaptura)
        {
            sFolioSurtido = FolioSurtido;
            iTipoDeCaptura = TipoCaptura;
            bEsSurtimiento = true;
            bEsValidacion = false; 

            if (TipoCaptura == 3)
            {
                bEsSurtimiento = false;
                bEsValidacion = true; 
            }

            this.ShowDialog();
        }

        private void CargaEncabezado()
        {
            string sSql = string.Format(
                "Select \n" + 
                "\tV.*" + 
                "From vw_PedidosCedis_Surtimiento V (NoLock) \n" + 
                "Where V.IdEmpresa = '{0}' and V.IdEstado = '{1}' and V.IdFarmacia = '{2}' and V.FolioSurtido = '{3}' \n" + 
                "Order by V.FechaRegistro \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sFarmacia, sFolioSurtido); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargaEncabezado()");
                General.msjError("Ocurró un error al cargar el folio de surtido.");
                this.Close();
            }
            else
            {
                if (leer.Leer())
                {
                    lblFolioSurtido.Text = leer.Campo("FolioSurtido");
                    lblFarmaciaSurtido.Text = leer.Campo("Farmacia");
                    lblFechaRegistro.Text = General.FechaYMD(leer.CampoFecha("FechaRegistro"));
                    lblFolioPedido.Text = leer.Campo("FolioPedido");
                    lblFarmaciaPedido.Text = leer.Campo("FarmaciaSolicita");
                    lblFechaPedido.Text = General.FechaYMD(leer.CampoFecha("FechaRegistro"));
                    lblStatusSurtimiento.Text = leer.Campo("StatusPedido");
                    GrupoProductos.Text = string.Format("Detalle de Surtido                                  Surtido por : {0} ", leer.Campo("PersonalSurtido"));
                    iPedidoManual = leer.CampoBool("Esmanual") ? 1 : 0;

                    if (leer.Campo("Status").ToUpper() != "A")
                    {
                        if (iTipoDeCaptura == 1)
                        {
                            btnGuardar.Enabled = false; 
                            General.msjUser("El Folio de Surtido no esta habilitado para surtimiento.");
                        }
                    }

                    // Habilitar las correcciones de captura 
                    if (leer.Campo("Status").ToUpper() == "S")
                    {
                        if (iTipoDeCaptura == 2)
                        {
                            btnGuardar.Enabled = true;
                        }
                    }

                    if (leer.Campo("Status").ToUpper() == "F")
                    {
                        btnGuardar.Enabled = false;
                        General.msjUser("El Folio de Surtido esta completo, no es posible realizar modificaciones.");
                    }
                }
            }
        }

        //private void CargarConcentrado()
        //{
        //    string sSql = string.Format(
        //        " Select ClaveSSA, DescripcionSal, Sum(CantidadRequerida) as CantidadRequerida, Sum(CantidadAsignada) as CantidadAsignada " +
        //        " From vw_Pedidos_Cedis_Det_Surtido_Distribucion (NoLock) " +
        //        " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioSurtido = '{3}' " +
        //        " Group By ClaveSSA, DescripcionSal " +
        //        " Order by ClaveSSA ",
        //        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sFarmacia, sFolioSurtido);

        //    if (iPedidoManual == 1)
        //    {
        //        sSql = string.Format(" Select P.ClaveSSA, S.DescripcionSal, P.CantidadAsignada As CantidadRequerida,SUM(D.CantidadAsignada) As CantidadAsignada " +
        //                "From Pedidos_Cedis_Det_Surtido P (NoLock) " +
        //                "Inner Join vw_ClavesSSA_Sales S  (NoLock) ON (P.IdClaveSSA = S.IdClaveSSA_Sal) " +
        //                "Inner Join Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock) " +
        //                "   On (P.IdEmpresa = D.IdEmpresa and P.IdEstado = D.IdEstado and P.IdFarmacia = D.IdFarmacia and P.FolioSurtido = D.FolioSurtido And S.ClaveSSA = D.ClaveSSA) " +
        //                "Where P.IdEmpresa = '{0}' and P.IdEstado = '{1}' and P.IdFarmacia = '{2}' and P.FolioSurtido = '{3}' " +
        //                "Group By P.ClaveSSA, DescripcionSal, P.CantidadAsignada " +
        //                "Order By P.ClaveSSA ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sFarmacia, sFolioSurtido);
        //    }

        //    GridConcentrado.Limpiar(false);
        //    if (!leer.Exec(sSql))
        //    {
        //        Error.GrabarError(leer, "CargarDetalles()");
        //        General.msjError("Ocurró un error al cargar el concentrado del folio de surtido.");
        //        this.Close();
        //    }
        //    else
        //    {
        //        if (leer.Leer())
        //        {
        //            GridConcentrado.LlenarGrid(leer.DataSetClase);
        //            GridConcentrado.SetActiveCell(GridConcentrado.ActiveRow, (int)ColsConcentrado.Cant_Asignada);
        //            //lblClaveSSA.Text = Grid.GetValue(Grid.ActiveRow, (int)ColsDetalles.ClaveSSA);
        //            //lblDescripcionSal.Text = Grid.GetValue(Grid.ActiveRow, (int)ColsDetalles.DescripcionSal);
        //        }
        //        else
        //        {
        //            General.msjAviso("No se encontraron claves para el Folio de Surtido. Verifique");
        //            this.Close();
        //        }
        //    }


        //}

        private void CargarDetalles()
        {
            string sFiltro = "";
            int iEsValidacion = bEsValidacion ? 1 : 0;
            string sSql = "";
            DataTable dtDatos; 

            if (bEsValidacion) 
            {
                sFiltro = " and CantidadAsignada > 0 "; 
            }


            ////sSql = string.Format( 
            ////    " Select IdSurtimiento, ClaveSSA, DescripcionSal, IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, ClaveLote, " + 
            ////    " Convert(varchar(7), FechaCaducidad, 120) as Caducidad, IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño, " +
            ////    " Caja, CantidadRequerida, CantidadAsignada, ObservacionesSurtimiento as Observaciones, cast(Validado as int) as Validado  " +
            ////    " From vw_Pedidos_Cedis_Det_Surtido_Distribucion (NoLock) " +
            ////    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioSurtido = '{3}' {4} " +
            ////    " Order by IdOrden ", 
            ////    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sFarmacia, sFolioSurtido, sFiltro);
            
            ////if (iPedidoManual == 1)
            ////{
            ////    sSql = string.Format( 
            ////        " Select IdSurtimiento, ClaveSSA, DescripcionSal, IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, ClaveLote, " +
            ////        " Convert(varchar(7), FechaCaducidad, 120) as Caducidad, IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño, " +
            ////        " Caja, Existencia As CantidadRequerida,  (Case When Existencia < CantidadAsignada Then Existencia Else CantidadAsignada End) As CantidadAsignada, " +
            ////        " ObservacionesSurtimiento as Observaciones, cast(Validado as int) as   " +
            ////        " From vw_Pedidos_Cedis_Det_Surtido_Distribucion (NoLock) " +
            ////        " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioSurtido = '{3}' {4} " +
            ////        " Order by IdOrden ",
            ////        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sFarmacia, sFolioSurtido, sFiltro);
            ////}

            ////// 20191106.1754 
            sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Cargar__OrdenDeSurtido \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @EsManual = '{4}', @EsValidacion = '{5}', @IdPersonal = '{6}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sFarmacia, sFolioSurtido, iPedidoManual, iEsValidacion, DtGeneral.IdPersonal);

            dtsDetalles = new DataSet();
            dtsDetalles_Incidencias = new DataSet();
            dtsDetalles_CajasEmbalaje = new DataSet();

            GridDetalles.Limpiar(false);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDetalles()");
                General.msjError("Ocurró un error al cargar los detalles del folio de surtido.");
                this.Close();
            }
            else
            {
                if (leer.Leer())
                {
                    GridDetalles.LlenarGrid(leer.DataSetClase);
                    GridDetalles.SetActiveCell(GridDetalles.ActiveRow, (int)ColsDetalles.Cant_Asignada);
                    lblClaveSSA.Text = GridDetalles.GetValue(GridDetalles.ActiveRow, (int)ColsDetalles.ClaveSSA);
                    lblDescripcionSal.Text = GridDetalles.GetValue(GridDetalles.ActiveRow, (int)ColsDetalles.DescripcionSal) + " -- " + GridDetalles.GetValue(GridDetalles.ActiveRow, (int)ColsDetalles.Presentacion);
                    //CalcularCantidades(1);

                    leer.RenombrarTabla(1, "Distribucion");
                    leer.RenombrarTabla(2, "Incidencias");
                    leer.RenombrarTabla(3, "Embalaje");

                    dtsDetalles = leer.DataSetClase;
                    dtsGridDetalles = dtsDetalles.Clone();
                    //dtsNuevo = dtsDetalles.Copy();

                    dtDatos = leer.Tabla(2); 
                    dtsDetalles_Incidencias = new DataSet();
                    dtsDetalles_Incidencias.Tables.Add(dtDatos.Copy() );

                    dtDatos = leer.Tabla(3);
                    dtsDetalles_CajasEmbalaje = new DataSet();
                    dtsDetalles_CajasEmbalaje.Tables.Add(dtDatos.Copy());


                    //CargarGridDetalles(1, true);


                    VerificarValidado();

                    //ActualizarCantidadAsignada();
                }
                else
                {
                    General.msjAviso("No existe información para mostrar.");
                    //General.msjAviso("No se ha Generado la Distribucion al Folio de Surtido.");
                    this.Close();
                }
            }

            
            
        }

        //private void ActualizarCantidadAsignada()
        //{
        //    clsLeer leerActualizar = new clsLeer();
        //    double dAsignado = 0;

        //    for (int i = 1; i <= GridConcentrado.Rows; i++)
        //    {
        //        dAsignado = 0;
        //        leerActualizar.DataRowsClase = dtsDetalles.Tables[0].Select(string.Format(" ClaveSSA = '{0}'", GridConcentrado.GetValue(i, (int)ColsConcentrado.ClaveSSA)));
        //        while (leerActualizar.Leer())
        //        {
        //            dAsignado += leerActualizar.CampoDouble("CantidadAsignada");
        //        }
        //        GridConcentrado.SetValue(i, (int)ColsConcentrado.Cant_Asignada, dAsignado);
        //    }
        //}
        #endregion CargarPedido        

        #region Eventos_Grid
        private void grdProductos_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            lblClaveSSA.Text = GridDetalles.GetValue(GridDetalles.ActiveRow, (int)ColsDetalles.ClaveSSA);
            lblDescripcionSal.Text = GridDetalles.GetValue(GridDetalles.ActiveRow, (int)ColsDetalles.DescripcionSal) + " -- " + GridDetalles.GetValue(GridDetalles.ActiveRow, (int)ColsDetalles.Presentacion);
            //CalcularCantidades();
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            clsLeer datosClave = new clsLeer();
            int iRow = GridDetalles.ActiveRow;
            int iValor = 0;
            string sCodigoEAN = GridDetalles.GetValue(iRow, (int)ColsDetalles.CodigoEAN);
            string sLote = GridDetalles.GetValue(iRow, (int)ColsDetalles.Lote);
            string sIdPasillo = GridDetalles.GetValue(iRow, (int)ColsDetalles.IdPasillo);
            string sIdEstante = GridDetalles.GetValue(iRow, (int)ColsDetalles.IdEstante);
            string sIdEntrepaño = GridDetalles.GetValue(iRow, (int)ColsDetalles.IdEntrepaño);

            switch (GridDetalles.ActiveCol)
            {
                case (int)ColsDetalles.Cant_Asignada:
                    {
                        //datosClave.DataRowsClase = dtsDetalles.Tables[0].Select(string.Format(
                        //            " CodigoEAN = '{0}' And ClaveLote = '{1}' And IdPasillo = '{2}' And IdEstante = '{3}' And IdEntrepaño  = '{4}'",
                        //             sCodigoEAN, sLote, sIdPasillo, sIdEstante, sIdEntrepaño));

                        //foreach (DataRow dtRow in dtsDetalles.Tables[0].Select(string.Format(" CodigoEAN = '{0}' ", sCodigoEAN)))
                        //{
                        //    dtsDetalles.Tables[0].Rows.Remove(dtRow);
                        //}

                        //dtsDetalles.Tables[0].Merge(Lista.Tables[0]);



                        int iCant_Requerida = GridDetalles.GetValueInt(iRow, (int)ColsDetalles.Cant_Requerida);
                        int iCant_Asignada = GridDetalles.GetValueInt(iRow, (int)ColsDetalles.Cant_Asignada);
                        int iCant_Asignada_por_Sal = GridDetalles.TotalizarColumna((int)ColsDetalles.Cant_Asignada);
                        int iCant_Requerida_Por_Sal = GridDetalles.GetValueInt(GridDetalles.ActiveRow, (int)ColsDetalles.Cant_Requerida);


                        if (iCant_Asignada > iCant_Requerida)
                        {
                            if (iPedidoManual == 0)
                            {
                                General.msjAviso("Cantidad Asignada no puede ser mayor a la cantidad Requerida, verifique.");
                            }
                            else
                            {
                                General.msjAviso("Cantidad Asignada no puede ser mayor a la existencia, verifique.");
                            }
                            GridDetalles.SetValue(iRow, (int)ColsDetalles.Cant_Asignada, iCant_Requerida);
                            GridDetalles.SetActiveCell(iRow, (int)ColsDetalles.Cant_Asignada);
                        }

                        if (iPedidoManual == 1)
                        {
                            if (iCant_Asignada_por_Sal > iCant_Requerida_Por_Sal)
                            {
                                General.msjAviso("Cantidad Asignada no puede ser mayor a la cantidad Requerida, verifique.");
                                GridDetalles.SetValue(iRow, (int)ColsDetalles.Cant_Asignada, (iCant_Asignada - (iCant_Asignada_por_Sal - iCant_Requerida_Por_Sal)));
                                GridDetalles.SetActiveCell(iRow, (int)ColsDetalles.Cant_Asignada);
                            }
                        }

                        //ObtenerDatosDetalles();
                        //IntegrarInformacion(dtsGridDetalles);
                        //ActualizarConcentrado();
                        //CalcularCantidades();
                    }
                    break;

                case (int)ColsDetalles.Caja:
                    {
                        iValor = GridDetalles.GetValueInt(iRow, (int)ColsDetalles.Caja);
                        GridDetalles.SetValue(iRow, (int)ColsDetalles.Caja, iValor); 
                        ////CargarCajas();
                    }
                    break;
            }
        }

        private void grdProductos_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            lblClaveSSA.Text = GridDetalles.GetValue(e.NewRow + 1, (int)ColsDetalles.ClaveSSA);
            lblDescripcionSal.Text = GridDetalles.GetValue(e.NewRow + 1, (int)ColsDetalles.DescripcionSal) + " -- " + GridDetalles.GetValue(e.NewRow + 1, (int)ColsDetalles.Presentacion);

            //CalcularCantidades();
        }
        #endregion Eventos_Grid

        #region GuardarInformacion
        private bool validarCantidades()
        {
            bool bRegresa = true;

            if (iCantAsignada > iCantRequerida)
            {
                General.msjAviso("La Cantidad Asignada no puede ser mayor a la Cantidad Requerida, verifique.");
                bRegresa = false;
            }            

            return bRegresa;
        }

        private bool GuardaInformacion()
        {
            bool bRegresa = true;
            int iId_Procesando = 0;
            string sSql_Completo = "";
            string sSql = "";
            string sSql_Incidencias = "";
            string sSql_Embalaje = "";
            int iSegmento = 0;

            sClaveSSA = "";
            sSubFarmacia = "";
            sProducto = "";
            sCodigoEAN = "";
            sLote = "";
            iSurtimiento = 0;
            iPasillo = 0;
            iEstante = 0;
            iEntrepaño = 0;
            iCantidadAsignada = 0;
            iCaja_Inicial = 0;
            iCaja_Final = 0;
            sObservaciones = "";
            iValidado = 0;
            sSKU = "";


            ObtenerDatosDetalles();

            leerGuardar.DataSetClase = dtsDetalles;


            bRegresa = leer.Exec(sSql); 
            while(leerGuardar.Leer() && bRegresa)
            {
                iId_Procesando = leerGuardar.CampoInt("Id");


                iCantidadAsignada = leerGuardar.CampoInt("CantidadAsignada");

                iSurtimiento = leerGuardar.CampoInt("IdSurtimiento");
                sClaveSSA = leerGuardar.Campo("ClaveSSA");
                sSubFarmacia = leerGuardar.Campo("IdSubFarmacia");
                sProducto = leerGuardar.Campo("IdProducto");
                sCodigoEAN = leerGuardar.Campo("CodigoEAN");
                sLote = leerGuardar.Campo("ClaveLote");
                iPasillo = leerGuardar.CampoInt("IdPasillo");
                iEstante = leerGuardar.CampoInt("IdEstante");
                iEntrepaño = leerGuardar.CampoInt("IdEntrepaño");
                sObservaciones = leerGuardar.Campo("Observaciones");

                iCaja_Inicial = leerGuardar.CampoInt("Caja");
                iCaja_Final = leerGuardar.CampoInt("Caja_Final");

                iValidado = leerGuardar.CampoInt("Validado");
                sSKU = leerGuardar.Campo("SKU");

                iSegmento = leerGuardar.CampoInt("Segmento");

                sSql = string.Format("Update Pedidos_Cedis_Det_Surtido_Distribucion Set CantidadAsignada = 0 \n" +
                                     "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioSurtido = '{3}' And ClaveSSA = '{4}' And CodigoEAN = '{5}' And ClaveLote = '{6}' And sku = '{7}' \n" +
                                     "  And IdPasillo = {8} And IdEstante = {9} And IdEntrepaño = {10} \n\n",
                                     sEmpresa, sEstado, sFarmacia, sFolioSurtido, sClaveSSA, sCodigoEAN, sLote, sSKU, iPasillo, iEstante, iEntrepaño);

                sSql += string.Format("Update E Set IdCaja_Inicial = '0', IdCaja_Final = '0'  \n" +
                                     "From Pedidos_Cedis_Det_Surtido_Distribucion_Embalaje E \n" +
                                     "Inner Join Pedidos_Cedis_Det_Surtido_Distribucion D(NoLock) \n" +
                                     "    On(E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia and E.FolioSurtido = D.FolioSurtido And E.ClaveSSA = D.ClaveSSA) \n" +
                                     "Where E.IdEmpresa = '{0}' and E.IdEstado = '{1}' and E.IdFarmacia = '{2}' and E.FolioSurtido = '{3}' And D.Segmento = {4} And E.ClaveSSA = '{5}'",
                                     sEmpresa, sEstado, sFarmacia, sFolioSurtido, iSegmento, sClaveSSA);


                if (iCantidadAsignada > 0)
                {
                    sSql += string.Format(" Exec spp_Actualiza_Pedidos_Cedis_Det_Surtido_Distribucion \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @IdSurtimiento = '{4}', @ClaveSSA = '{5}', \n" +
                        "\t@IdSubFarmacia = '{6}', @IdProducto = '{7}', @CodigoEAN = '{8}', @ClaveLote = '{9}', \n" +
                        "\t@IdPasillo = '{10}', @IdEstante = '{11}', @IdEntrepaño = '{12}', @CantidadAsignada = '{13}', @Observaciones = '{14}', \n" +
                        "\t@sStatus = '{15}', @IdCaja = '{16}', @IdCaja_Final = '{17}', @Validado = '{18}', @SKU = '{19}' \n\n",
                    sEmpresa, sEstado, sFarmacia, sFolioSurtido, iSurtimiento, sClaveSSA, sSubFarmacia,
                    sProducto, sCodigoEAN, sLote, iPasillo, iEstante, iEntrepaño, iCantidadAsignada, sObservaciones, 'A',
                    iCaja_Inicial, iCaja_Final,
                    iValidado, sSKU);



                    sSql_Incidencias = GuardaInformacion__Incidencias(iId_Procesando);
                    sSql_Embalaje = GuardaInformacion__Embalaje(iId_Procesando);




                    ////if(!leer.Exec(sSql))
                    ////{
                    ////    bRegresa = false;
                    ////}


                }

                sSql += sSql_Incidencias + "\n";
                sSql += sSql_Embalaje + "\n";


                sSql_Completo += sSql + "\n\n\n\n";

                ////if(bRegresa)
                ////{
                ////    if(!GuardaInformacion__Incidencias(iId_Procesando))
                ////    {
                ////        bRegresa = false;
                ////        //break;
                ////    }
                ////}
            }

            //// Ejecutar de forma masiva
            if(bRegresa)
            {
                bRegresa = leer.Exec(sSql_Completo);
            }

            return bRegresa;
        }

        private string GuardaInformacion__Incidencias(int Id)
        {
            bool bRegresa = true;
            string sSql_Completo = "";
            string sSql = "";
            clsLeer incidencias = new clsLeer();
            string sStatus_Incidencia = "A"; 

            incidencias.DataRowsClase = dtsDetalles_Incidencias.Tables[0].Select(string.Format(" Id = '{0}' and Status_Incidencia = 1", Id));

            while(incidencias.Leer())
            {
                sSql = string.Format("\tExec spp_Pedidos_Cedis_Surtido__Incidencias \n" +
                    "\t\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}',\n" +
                    "\t\t@IdSurtimiento = '{4}', @IdIncidencia = '{5}', @IdPersonal = '{6}', @ClaveSSA = '{7}', @IdSubFarmacia = '{8}',\n" +
                    "\t\t@IdProducto = '{9}', @SKU = '{10}', @CodigoEAN = '{11}', @ClaveLote = '{12}', @IdPasillo = '{13}',\n" +
                    "\t\t@IdEstante = '{14}', @IdEntrepaño = '{15}', @Status = '{16}', @Observaciones = '{17}'\n",
                    sEmpresa, sEstado, sFarmacia, sFolioSurtido,
                    iSurtimiento, incidencias.Campo("IdIncidencia"), DtGeneral.IdPersonal, sClaveSSA, sSubFarmacia,
                    sProducto, sSKU, sCodigoEAN, sLote, iPasillo, iEstante, iEntrepaño,
                    sStatus_Incidencia, incidencias.Campo("Observaciones_Adicionales")
                    ) ;

                sSql_Completo += sSql + "\n\n";

                ////if(!leer.Exec(sSql))
                ////{
                ////    bRegresa = false;
                ////    break;
                ////}
            }

            return sSql_Completo;
        }

        private string GuardaInformacion__Embalaje( int Id )
        {
            bool bRegresa = true;
            string sSql_Completo = ""; 
            string sSql = "";
            clsLeer embalaje = new clsLeer();
            string sStatus_Incidencia = "A";

            embalaje.DataRowsClase = dtsDetalles_CajasEmbalaje.Tables[0].Select(string.Format(" Id = '{0}' ", Id));

            while(embalaje.Leer())
            {
                sSql = string.Format("\tExec spp_Mtto_Pedidos_Cedis_Det_Surtido_Distribucion_Embalaje \n" +
                    "\t\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @IdSurtimiento = '{4}', @ClaveSSA = '{5}', \n" +
                    "\t\t@SKU = '{6}', @IdSubFarmacia = '{7}', @IdProducto = '{8}', @CodigoEAN = '{9}', @ClaveLote = '{10}', \n" +
                    "\t\t@IdPasillo = '{11}', @IdEstante = '{12}', @IdEntrepaño = '{13}', @sStatus = '{14}', \n" +
                    "\t\t@Renglon = '{15}', @IdCaja_Inicial = '{16}', @IdCaja_Final = '{17}', @FechaModificacion = '{18}' \n",
                    sEmpresa, sEstado, sFarmacia, sFolioSurtido, iSurtimiento, sClaveSSA,
                    sSKU, sSubFarmacia, sProducto, sCodigoEAN, sLote,
                    iPasillo, iEstante, iEntrepaño, sStatus_Incidencia,
                    embalaje.CampoInt("Renglon"), embalaje.CampoInt("IdCaja_Inicial"), embalaje.CampoInt("IdCaja_Final"), embalaje.CampoFecha("FechaModificacion").ToString("yyyy/MM/dd HH: mm:ss")
                    );

                sSql_Completo += sSql + "\n\n";

                ////if(!leer.Exec(sSql))
                ////{
                ////    bRegresa = false;
                ////    break;
                ////}
            }

            return sSql_Completo;
        }

        private bool ActualizarStatusSurtimiento()
        {
            bool bRegresa = false;
            string sSql = "", sStatus = "";
            string sModificaciones = "";

            if (iTipoDeCaptura == 2)
            {
                sModificaciones = " , ModificacionesCaptura = ModificacionesCaptura + 1 ";
            }

            sStatus = "S"; 
            sSql = string.Format(" Update Pedidos_Cedis_Enc_Surtido Set Status = '{0}' {1} \n" +
                                    "Where IdEmpresa = '{2}' and IdEstado = '{3}' and IdFarmacia = '{4}' and FolioSurtido = '{5}' \n",
                                    sStatus, sModificaciones, sEmpresa, sEstado, sFarmacia, sFolioSurtido);

            bRegresa = leer.Exec(sSql);
            ////if (!leer.Exec(sSql))
            ////{
            ////    bRegresa = false;                
            ////} 

            return bRegresa;
        }
        #endregion GuardarInformacion       

        #region Funciones Grid 
        private void grdConcentrado_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            // CargarGridDetalles(GridConcentrado.ActiveRow);
        }

        private void grdConcentrado_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            //CargarGridDetalles(e.NewRow + 1); 
        }

        private void grdConcentrado_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
        }

        //private void ActualizarInformacion_Memoria()
        //{
        //    GridConcentrado.ColorRenglones(Color.White); 

        //    iCantidadesInvalidas = 0; 
        //    for(int i = 1; i <= GridConcentrado.Rows; i++)
        //    {
        //        CargarGridDetalles(i, false);

        //        if (GridConcentrado.GetValueInt(i, (int)ColsConcentrado.Cant_Asignada) > GridConcentrado.GetValueInt(i, (int)ColsConcentrado.Cant_Requerida))
        //        {
        //            GridConcentrado.ColorRenglon(i, Color.Red); 
        //        }
        //    }

        //    CargarGridDetalles(1, false);
        //    GridConcentrado.SetActiveCell(1, (int)ColsConcentrado.Cant_Asignada); 
        //}

        //private void CargarGridDetalles(int Renglon)
        //{
        //    //if (iRenglonActivo != Renglon)
        //    //{
        //    //    CargarGridDetalles(Renglon, false);
        //    //}
        //}

        //private void CargarGridDetalles(int Renglon, bool EsInicio) 
        //{
        //    string sClaveSSA = GridConcentrado.GetValue(Renglon, (int)ColsConcentrado.ClaveSSA);
        //    clsLeer datosClave = new clsLeer();

        //    iRenglonActivo = Renglon;
        //    //lblClaveSSA.Text = GridConcentrado.GetValue(Renglon, (int)ColsConcentrado.ClaveSSA);
        //    //lblDescripcionSal.Text = GridConcentrado.GetValue(Renglon, (int)ColsConcentrado.DescripcionSal) + " -- " + GridConcentrado.GetValue(Renglon, (int)ColsConcentrado.DescripcionSal);

        //    if (!EsInicio)
        //    {
        //        ObtenerDatosDetalles();
        //        //IntegrarInformacion(dtsGridDetalles);
        //    }

        //    //datosClave.DataRowsClase = dtsDetalles.Tables[0].Select(string.Format(" ClaveSSA = '{0}'", sClaveSSA)); 
        //    datosClave.DataSetClase = dtsDetalles; 
        //    GridDetalles.Limpiar(false); 
        //    //GridDetalles.LlenarGrid(datosClave.DataSetClase, false, true);
        //    try
        //    {
        //        GridDetalles.AgregarRenglon(datosClave.DataRowsClase, dtsDetalles.Tables[0].Columns.Count, false);
        //    }
        //    catch { }

        //    //CalcularCantidades(Renglon);
        //    //ActualizarConcentrado();


        //    if (bEsValidacion)
        //    {
        //        GridDetalles.BloqueaColumna(true, (int)ColsDetalles.Cant_Asignada);  
        //    }
        //}

        private void ObtenerDatosDetalles()
        {
            int iCaja = 0;
             
            dtsGridDetalles.Tables[0].Rows.Clear();
            

            try
            {
                for (int i = 1; i <= GridDetalles.Rows; i++)
                {
                    iCaja = GridDetalles.GetValueInt(i, (int)ColsDetalles.Caja); 

                    object[] objRow = 
                    {
                        GridDetalles.GetValueInt(i, (int)ColsDetalles.Id),
                        GridDetalles.GetValueInt(i, (int)ColsDetalles.IdSurtimiento),  
                        GridDetalles.GetValue(i, (int)ColsDetalles.ClaveSSA),  
                        GridDetalles.GetValue(i, (int)ColsDetalles.DescripcionSal),  
                        GridDetalles.GetValue(i, (int)ColsDetalles.IdSubFarmacia),  
                        GridDetalles.GetValue(i, (int)ColsDetalles.IdProducto), 
                        GridDetalles.GetValue(i, (int)ColsDetalles.CodigoEAN),
                        GridDetalles.GetValue(i, (int)ColsDetalles.Descripcion),
                        GridDetalles.GetValue(i, (int)ColsDetalles.Presentacion),
                        GridDetalles.GetValue(i, (int)ColsDetalles.SKU),
                        GridDetalles.GetValue(i, (int)ColsDetalles.Lote),  
                        GridDetalles.GetValue(i, (int)ColsDetalles.Caducidad), 

                        GridDetalles.GetValue(i, (int)ColsDetalles.IdPasillo), 
                        GridDetalles.GetValue(i, (int)ColsDetalles.Pasillo),
                        GridDetalles.GetValue(i, (int)ColsDetalles.IdEstante),  
                        GridDetalles.GetValue(i, (int)ColsDetalles.Estante),  
                        GridDetalles.GetValue(i, (int)ColsDetalles.IdEntrepaño), 
                        GridDetalles.GetValue(i, (int)ColsDetalles.Entrepaño),

                        GridDetalles.GetValueInt(i, (int)ColsDetalles.Caja),
                        GridDetalles.GetValueInt(i, (int)ColsDetalles.Caja_Final), 

                        GridDetalles.GetValueInt(i, (int)ColsDetalles.Cant_Requerida_Caja),
                        GridDetalles.GetValueInt(i, (int)ColsDetalles.Cant_Requerida), 
                        GridDetalles.GetValueInt(i, (int)ColsDetalles.Cant_Asignada), 
                        GridDetalles.GetValue(i, (int)ColsDetalles.Observaciones), 
                        GridDetalles.GetValueInt(i, (int)ColsDetalles.Validado),
                        GridDetalles.GetValueInt(i, (int)ColsDetalles.segmento)
                    };

                    dtsGridDetalles.Tables[0].Rows.Add(objRow);
                }
                dtsDetalles = dtsGridDetalles;
            }
            catch (Exception ex)
            {
                Error.GrabarError(ex, "CargarGridDetalles()");
                General.msjError("Error al asignar las cantidad los detalles.");
            }

            iCaja = 0;

        }

        //private void IntegrarInformacion(DataSet Lista)
        //{
        //    foreach (DataRow dtRow in Rows())
        //    {
        //        dtsDetalles.Tables[0].Rows.Remove(dtRow);
        //    }
        //    dtsDetalles.Tables[0].Merge(Lista.Tables[0]);

        //}

        //private DataRow[] Rows()
        //{
        //    string sSelect = string.Format("1=1");

        //    try
        //    {
        //        sSelect = string.Format(" ClaveSSA = '{0}' ", dtsGridDetalles.Tables[0].Rows[0][1]);
        //    }
        //    catch
        //    {
        //        sSelect = string.Format("1=0");
        //    }

        //    return dtsDetalles.Tables[0].Select(sSelect);
        //}

        #endregion Funciones Grid     

        private void ModificarCantidades()
        {
            int iRow = GridDetalles.ActiveRow;
            int iCantidad = GridDetalles.GetValueInt(iRow, (int)ColsDetalles.Cant_Asignada);
            string sObservaciones = GridDetalles.GetValue(iRow, (int)ColsDetalles.Observaciones);

            FrmModificarCantidadSurtida f = new FrmModificarCantidadSurtida();
            f.CantidadAnterior = iCantidad;
            f.Observaciones = sObservaciones; 
            f.ShowDialog();

            if (f.AplicarCambio)
            {
                iCantidad = f.CantidadNueva; 
                sObservaciones = f.Observaciones;
                GridDetalles.SetValue(iRow, (int)ColsDetalles.Cant_Asignada, iCantidad);
                GridDetalles.SetValue(iRow, (int)ColsDetalles.Observaciones, sObservaciones);
            }
        }

        private void btnModificarCantidadSurtida_Click(object sender, EventArgs e)
        {
            int iRow = GridDetalles.ActiveRow;
            int iCantRequerida = GridDetalles.GetValueInt(iRow, (int)ColsDetalles.Cant_Requerida);
            int iCantidad = GridDetalles.GetValueInt(iRow, (int)ColsDetalles.Cant_Asignada);
            string sObservaciones = GridDetalles.GetValue(iRow, (int)ColsDetalles.Observaciones);

            FrmModificarCantidadSurtida f = new FrmModificarCantidadSurtida();
            f.CantidadRequerida = iCantRequerida; 
            f.CantidadAnterior = iCantidad;
            f.Observaciones = sObservaciones; 
            f.ShowDialog();

            if (f.AplicarCambio)
            {
                iCantidad = f.CantidadNueva; 
                sObservaciones = f.Observaciones;
                GridDetalles.SetValue(iRow, (int)ColsDetalles.Cant_Asignada, iCantidad);
                GridDetalles.SetValue(iRow, (int)ColsDetalles.Observaciones, sObservaciones);
                //CalcularCantidades(iRenglonActivo);
                //ActualizarConcentrado();
            }
        }

        private void grdDetalles_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F5)
            {
                int iRow = GridDetalles.ActiveRow;
                int idUnique = GridDetalles.GetValueInt(iRow, ColsDetalles.Id);


                ////int iCaja_Inicial = GridDetalles.GetValueInt(iRow, ColsDetalles.Caja);
                ////int iCaja_Final = GridDetalles.GetValueInt(iRow, ColsDetalles.Caja_Final);

                FrmCapturarCajas f = new FrmCapturarCajas( idUnique, dtsDetalles_CajasEmbalaje );
                f.ShowInTaskbar = false;
                f.ShowDialog(this);

                dtsDetalles_CajasEmbalaje = f.InformacionEmbalaje; 

                ////if ( f.AplicarCambio ) 
                ////{
                ////    GridDetalles.SetValue(iRow, ColsDetalles.Caja, f.Caja_Inicial);
                ////    GridDetalles.SetValue(iRow, ColsDetalles.Caja_Final, f.Caja_Final);
                ////}

                f = null;
            }

            if(bActivar_RegistroDeIncidencias)
            {
                if(e.KeyCode == Keys.F10)
                {
                    int iId = GridDetalles.GetValueInt(GridDetalles.ActiveRow, ColsDetalles.Id);

                    FrmPedidos_RegistroDeIncidencias f = new FrmPedidos_RegistroDeIncidencias(iId, dtsDetalles_Incidencias);
                    f.ShowInTaskbar = false; 
                    f.ShowDialog(this);

                    dtsDetalles_Incidencias = f.Detalles_Incidencias; 
                }
            }
        }

        private void grdConcentrado_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F5)
            //{
            //    grdDetalles.Focus();
            //    GridDetalles.SetActiveCell(1, (int)ColsDetalles.Cant_Asignada);
            //    //GridDetalles.ActiveCol = (int)ColsDetalles.Cant_Asignada; 
            //}
        }

        private void CargarCajas()
        {
            ////string sSql = "";
            ////int iRow = GridDetalles.ActiveRow;

            ////sSql = string.Format(" Select *, Convert(int, IdCaja) as Caja " +
            ////                     " From Pedidos_Cedis_Cajas_Surtido_Distribucion (Nolock) Where IdEstado = '{0}' and IdFarmacia = '{1}' " +
            ////                     " and FolioPedido = '{2}' and FolioSurtido = '{3}' and Convert(int, IdCaja) = {4} ", sEstado, sFarmacia, 
            ////                      Fg.PonCeros(lblFolioPedido.Text, 6), Fg.PonCeros(lblFolioSurtido.Text, 8), 
            ////                      GridDetalles.GetValueInt(iRow, (int)ColsDetalles.Caja) );

            

            ////if (!leer.Exec(sSql))
            ////{
            ////    Error.GrabarError(leer, "CargarCajas()");
            ////    General.msjError("Ocurró un error al cargar el número de caja.");               
            ////}
            ////else
            ////{
            ////    if (leer.Leer())
            ////    {
            ////        GridDetalles.SetValue(iRow, (int)ColsDetalles.Caja, leer.Campo("Caja"));
            ////        GridDetalles.SetActiveCell(iRow, (int)ColsDetalles.Cant_Asignada);
            ////    }
            ////    else
            ////    {
            ////        General.msjAviso("No se encontro el número de caja ó no esta asignada al Surtido.");
            ////        GridDetalles.SetValue(iRow, (int)ColsDetalles.Caja, "");
            ////        GridDetalles.SetActiveCell(iRow, (int)ColsDetalles.Caja);
            ////    }
            ////}
        }

        private void btnAsignarCajas_Click(object sender, EventArgs e)
        {
            FrmCajasSurtidoPedidos f = new FrmCajasSurtidoPedidos();
            f.CargaPantalla(lblFolioPedido.Text, lblFolioSurtido.Text);
        }

        private void btnSalir_Click( object sender, EventArgs e )
        {
            this.Close(); 
        }
        #region Validaciones
        private void ValidaCajas()
        {
            string sSql = "";

            sSql = string.Format(" Select * From Pedidos_Cedis_Cajas_Surtido_Distribucion (Nolock) " +
                                 " Where IdEmpresa = '{0}' and IdEstado = '{1}' " +
                                 " and IdFarmacia = '{2}' and FolioPedido = '{3}' and FolioSurtido = '{4}' Order By IdCaja ",
                                 sEmpresa, sEstado, sFarmacia, lblFolioPedido.Text, sFolioSurtido);
                        
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidaCajas()");
                General.msjError("Ocurrió un error al obtener los detalles de cajas..");
            }
            else
            {
                if (!leer.Leer())
                {
                    FrmCajasSurtidoPedidos f = new FrmCajasSurtidoPedidos();
                    f.CargaPantalla(lblFolioPedido.Text, lblFolioSurtido.Text);
                }
            }
            
        }
        #endregion Validaciones

        private void FrameDatosGenerales_Enter(object sender, EventArgs e)
        {

        }

        private void txtCodifoEAN_Validating(object sender, CancelEventArgs e)
        {
            clsLeer datosClave = new clsLeer();
            clsLeer leerDetalles = new clsLeer();

            int iCantidad = 0, iValidado = 0, iIdDataSet = 0, iIdGrid = 0;

            
            string sCodigoEAN = txtCodifoEAN.Text.Trim();

            if (txtCodifoEAN.Text.Trim() != "")
            {

                ObtenerDatosDetalles();

                datosClave.DataRowsClase = dtsDetalles.Tables[0].Select(string.Format(" CodigoEAN = '{0}'", sCodigoEAN));

                if (datosClave.Registros == 0)
                {
                    General.msjAviso("El código EAN no se encuentra en el surtimiento.");
                }
                else 
                {
                    FrmActulizarSurtidoPedido_CodigoEAN f = new FrmActulizarSurtidoPedido_CodigoEAN(datosClave.DataSetClase, sCodigoEAN, iTipoDeCaptura, dtsDetalles_CajasEmbalaje);
                    f.ShowDialog();

                    dtsDetalles_CajasEmbalaje = f.InformacionEmbalaje;


                    //foreach (DataRow dtRow in dtsDetalles.Tables[0].Select(string.Format(" CodigoEAN = '{0}' ", sCodigoEAN)))
                    //{
                    //    dtsDetalles.Tables[0].Rows.Remove(dtRow);
                    //}

                    datosClave.DataSetClase = f.ObtenerDatosDetalles();


                    while(datosClave.Leer())
                    {
                        iIdDataSet = datosClave.CampoInt("Id");
                        iCantidad = datosClave.CampoInt("CantidadAsignada");
                        iValidado = datosClave.CampoInt("Validado");

                        for (int i = 1; i <= GridDetalles.Rows; i++)
                        {
                            iIdGrid = GridDetalles.GetValueInt(i, (int)ColsDetalles.Id);                             

                            if (iIdDataSet == iIdGrid)
                            {
                                GridDetalles.SetValue(i, (int)ColsDetalles.Cant_Asignada, iCantidad);
                                GridDetalles.SetValue(i, (int)ColsDetalles.Validado, iValidado);

                                GridDetalles.SetValue(i, ColsDetalles.Caja, datosClave.CampoInt("Caja"));
                                GridDetalles.SetValue(i, ColsDetalles.Caja_Final, datosClave.CampoInt("Caja_Final"));

                                break;
                            }
                        }
                    }

                    //dtsDetalles.Tables[0].Merge(datosClave.DataSetClase.Tables[0]);

                    //GridDetalles.LlenarGrid(dtsDetalles);
                    //ActualizarConcentrado();

                    VerificarValidado(); 

                    //leerDetalles.DataSetClase = dtsDetalles;

                    //string sClaveSSA = GridConcentrado.GetValue(1, (int)ColsConcentrado.ClaveSSA);
                    //datosClave.DataRowsClase = dtsDetalles.Tables[0].Select(string.Format(" ClaveSSA = '{0}'", sClaveSSA));

                    //GridConcentrado.ActiveRow = 1;



                    //GridDetalles.Limpiar(false);
                    ////GridDetalles.LlenarGrid(datosClave.DataSetClase, false, true);
                    //try
                    //{
                    //    GridDetalles.AgregarRenglon(datosClave.DataRowsClase, dtsDetalles.Tables[0].Columns.Count, false);
                    //}
                    //catch { }


                    txtCodifoEAN.Focus(); 
                }

                txtCodifoEAN.Text = "";
            }
        }

        private void grdDetalles_ButtonClicked( object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e )
        {
            MarcarValidado(); 
        }

        //private void ActualizarConcentrado()
        //{
        //    int iCantidad = 0;
        //    string sClaveSSA;
        //    clsLeer leerDetalles = new clsLeer();

        //    for (int i = 1; i <= GridConcentrado.Rows; i++)
        //    {
        //        iCantidad = 0;
        //        sClaveSSA = GridConcentrado.GetValue(i, (int)ColsConcentrado.ClaveSSA);
        //        leerDetalles.DataRowsClase = dtsDetalles.Tables[0].Select(string.Format(" ClaveSSA = '{0}'", sClaveSSA));

        //        while (leerDetalles.Leer())
        //        {
        //            iCantidad += leerDetalles.CampoInt("CantidadAsignada");
        //        }

        //        GridConcentrado.SetValue(i, (int)ColsConcentrado.Cant_Asignada, iCantidad);

        //    }
        //}

        private void toolStripBarraMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void chkMostrarSKU_CheckedChanged( object sender, EventArgs e )
        {
            Mostrar_Columna_SKU();

        }

        private void Mostrar_Columna_SKU()
        {
            GnFarmacia.Mostrar_SKU_Surtidos = !GnFarmacia.Mostrar_SKU_Surtidos;
            bMostrar_SKU_Surtidos = GnFarmacia.Mostrar_SKU_Surtidos;

            if(bMostrar_SKU_Surtidos)
            {
                //chkMostrarSKU.Text = "Ocultar SKU";
                GridDetalles.AnchoColumna(ColsDetalles.SKU, (int)fAncho_SKU);
            }
            else
            {
                //chkMostrarSKU.Text = "Mostrar SKU";
            }

            GridDetalles.OcultarColumna(bMostrar_SKU_Surtidos, ColsDetalles.SKU);

            GridDetalles.AjustarAnchoColumnasAutomatico = true;

            GridDetalles.AjustsarColumnas_Grid();

            

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void MarcarValidado()
        {
            int iRenglon = GridDetalles.ActiveRow;

            MarcarValidado(iRenglon);
        }

        private void MarcarValidado( int Renglon )
        {
            bool bEsValidado = false;
            Color color = cColor_NoValidado;

            switch(GridDetalles.ActiveCol)
            {
                case (int)ColsDetalles.Validado:
                    bEsValidado = GridDetalles.GetValueBool(Renglon, ColsDetalles.Validado);
                    break;
            }

            color = bEsValidado ? cColor_Validado : cColor_NoValidado;

            GridDetalles.ColorRenglon(Renglon, color);

        }

        private void VerificarValidado()
        {
            bool bEsValidado = false;
            Color color = cColor_NoValidado;

            for(int i = 1; i <= GridDetalles.Rows; i++)
            {
                bEsValidado = GridDetalles.GetValueBool(i, ColsDetalles.Validado);
                color = bEsValidado ? cColor_Validado : cColor_NoValidado;
                GridDetalles.ColorRenglon(i, color);
            }
        }
    }
}
