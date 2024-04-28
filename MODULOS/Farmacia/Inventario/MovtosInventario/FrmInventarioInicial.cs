using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario; 
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace Farmacia.Inventario
{
    public partial class FrmInventarioInicial : FrmBaseExt
    {
        private enum Cols 
        {
            Ninguna = 0, 
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5, 
            Costo = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10
        }

        #region variables 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leer2;
        clsGrid myGrid;
        clsCodigoEAN EAN = new clsCodigoEAN();
        // string sMsjEanInvalido = "";

        clsConsultas query;
        clsAyudas ayuda;
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        // bool bExisteMovto = false;
        bool bEstaCancelado = false;
        bool bMovtoAplicado = false;
        bool bPermitirSacarCaducados = false;

        string sFolioMovto = "";
        string sFormato = "#,###,###,##0.###0";
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdTipoMovtoInv = "II";
        string sTipoES = "E";
        string sIdProGrid = "";
        string sMsjNoEncontrado = "";
        string sPersonal = DtGeneral.IdPersonal;
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;


        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        clsSKU SKU; // = new clsSKU(); 

        #region Variables SKU
        string sIdLicitacion = "";
        string sIdFuente = "";
        string sOrden = "OC1";
        string sFolioPresup = "F1";
        #endregion Variables SKU

        #endregion variables

        public FrmInventarioInicial()
        {
            InitializeComponent();

            int i_PresentHeight = (int)((double)Screen.PrimaryScreen.Bounds.Height * 0.98);   //Present Resolution Height
            int i_PresentWidth = (int)((double)Screen.PrimaryScreen.Bounds.Width * 0.85); //Presnet Resolution Width

            //ResizeForm(this, i_PresentHeight, i_PresentWidth); 

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myGrid = new clsGrid( ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.BackColorColsBlk = Color.White;      
            grdProductos.EditModeReplace = true;
            myGrid.AjustarAnchoColumnasAutomatico = true; 

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);

            // Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sPersonal);

            //btnAbrir.Visible = !DtGeneral.EsAlmacen;
            ////btnExportarExcel.Visible = btnAbrir.Visible;
            ////toolStripSeparator_Plantilla.Visible = btnAbrir.Visible;

            if(DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Farmacia || DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Almacen)
            {
                btnAbrir.Visible = true; 
            }
        }

        #region Manejo de Caducados 
        private void PermiteCaducados()
        {
            bPermitirSacarCaducados = false; 
            leer.DataSetClase = query.MovtosTiposInventario(sIdTipoMovtoInv, "PermiteCaducados()"); 
            if (leer.Leer())
            {
                bPermitirSacarCaducados = leer.CampoBool("PermiteCaducados"); 
            }
        }
        #endregion Manejo de Caducados

        #region Redimensionar Form
        float f_HeightRatio = new float();
        float f_WidthRatio = new float();

        public void ResizeForm(Form ObjForm, int DesignerHeight, int DesignerWidth)
        {
            #region Code for Resizing and Font Change According to Resolution
            //Specify Here the Resolution Y component in which this form is designed
            //For Example if the Form is Designed at 800 * 600 Resolution then DesignerHeight=600
            int i_StandardHeight = DesignerHeight;

            //Specify Here the Resolution X component in which this form is designed
            //For Example if the Form is Designed at 800 * 600 Resolution then DesignerWidth=800
            int i_StandardWidth = DesignerWidth;

            int i_PresentHeight = Screen.PrimaryScreen.Bounds.Height;//Present Resolution Height
            int i_PresentWidth = Screen.PrimaryScreen.Bounds.Width;//Presnet Resolution Width
            

            f_HeightRatio = (float)((float)i_PresentHeight / (float)i_StandardHeight);
            f_WidthRatio = (float)((float)i_PresentWidth / (float)i_StandardWidth);

            ObjForm.AutoScaleMode = AutoScaleMode.None;//Make the Autoscale Mode=None
            ObjForm.Scale(new SizeF(f_WidthRatio, f_HeightRatio)); 


            //////foreach (Control c in ObjForm.Controls)
            //////{
            //////    if (c.HasChildren)
            //////    {
            //////        ResizeControlStore(c);
            //////    }
            //////    else
            //////    {
            //////        c.Font = new Font(c.Font.FontFamily, c.Font.Size * f_HeightRatio, c.Font.Style, c.Font.Unit, ((byte)(0)));
            //////    }
            //////}
            //////ObjForm.Font = new Font(ObjForm.Font.FontFamily, ObjForm.Font.Size * f_HeightRatio, ObjForm.Font.Style, ObjForm.Font.Unit, ((byte)(0)));
            #endregion 
        }

        private void ResizeControlStore(Control objCtl)
        {
            if (objCtl.HasChildren)
            {
                foreach (Control cChildren in objCtl.Controls)
                {
                    if (cChildren.HasChildren)
                    {
                        ResizeControlStore(cChildren);
                    }
                    else
                    {
                        cChildren.Font = new Font(cChildren.Font.FontFamily, cChildren.Font.Size * f_HeightRatio, cChildren.Font.Style, cChildren.Font.Unit, ((byte)(0)));
                    }
                }
                objCtl.Font = new Font(objCtl.Font.FontFamily, objCtl.Font.Size * f_HeightRatio, objCtl.Font.Style, objCtl.Font.Unit, ((byte)(0)));
            }
            else
            {
                objCtl.Font = new Font(objCtl.Font.FontFamily, objCtl.Font.Size * f_HeightRatio, objCtl.Font.Style, objCtl.Font.Unit, ((byte)(0)));
            }

        }
        #endregion Redimensionar Form 

        #region CargaDatosSKU
        private void CargaFuente()
        {
            leer2 = new clsLeer(ref cnn);
            string sSql = "";
            string sFiltroConsignacion = "";
            string sFiltroSubFarmacia = "";
            string sIdEstado = DtGeneral.EstadoConectado;
            string sIdFarmacia = DtGeneral.FarmaciaConectada;
           
            sFiltroConsignacion = " and EsConsignacion = 0 AND SubFarmacia = 'DISPERSION' ";
            sFiltroConsignacion += " and EmulaVenta = 0 ";

            sSql = string.Format("Select IdEstado, IdFarmacia, IdSubFarmacia, SubFarmacia, EsConsignacion, EmulaVenta " +
                    " From vw_Farmacias_SubFarmacias (NoLock) " +
                    " Where IdEstado = '{0}' and IdFarmacia = '{1}' {2} {3} ",
                    sIdEstado, sIdFarmacia, sFiltroConsignacion, sFiltroSubFarmacia);


            if (leer2.Exec(sSql))
            {
                if(leer2.Leer())
                {
                    sIdFuente = leer2.Campo("IdSubFarmacia");
                }                
            }            
        }

        private void CargaLicitacion()
        {
            leer2 = new clsLeer(ref cnn);
            string sSql = "";
            string sIdEstado = DtGeneral.EstadoConectado;
            

            sSql = string.Format("SELECT IdEstado, IdLicitacion " +
                    " FROM Ctrl_Licitaciones (NoLock) " +
                    " Where IdEstado = '{0}' AND Status = 'A' ", sIdEstado);


            if (leer2.Exec(sSql))
            {
                if (leer2.Leer())
                {
                    sIdLicitacion = leer2.Campo("IdLicitacion");
                }
            }
        }
        #endregion CargaDatosSKU

        #region Botones
        private void LimpiarPantalla(bool Confirmar)
        {
            bool bExito = true;

            if (Confirmar)
            {
                if (General.msjConfirmar("¿ Desea limpiar la información en pantalla, se perdera lo que este capturado. ?") == DialogResult.No)
                {
                    bExito = false;
                }
            }

            if (bExito)
            {
                // Inicializar el manejo de Lotes 
                Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento, TiposDeInventario.Venta);
                Lotes.ManejoLotes = OrigenManejoLotes.Inventarios;
                Lotes.IdSubFarmacia = sIdFuente;
                SKU = new clsSKU();
                SKU.IdEmpresa = sEmpresa;
                SKU.IdEstado = sEstado;
                SKU.IdFarmacia = sFarmacia;
                SKU.TipoDeMovimiento = sIdTipoMovtoInv; 


                // Acitvar barra de menu 
                IniciarToolBar(false, false, false);

                myGrid.Limpiar(false);
                myGrid.BloqueaColumna(false, (int)Cols.CodEAN);
                myGrid.BloqueaColumna(false, (int)Cols.Costo);

                // bExisteMovto = false;
                bEstaCancelado = false;
                bMovtoAplicado = false;

                myGrid.Limpiar(false);
                Fg.IniciaControles();
                Fg.IniciaControles(this, false, FrameValorInventario);
                lblCancelado.Visible = false;
                chkAplicarInv.Enabled = true;
                dtpFechaRegistro.Enabled = false;

                txtIdPersonal.Text = DtGeneral.IdPersonal;
                lblPersonal.Text = DtGeneral.NombrePersonal;
                txtIdPersonal.Enabled = false;

                txtFolio.Focus();
            }
        }

        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false);
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            //btnNuevo.Enabled = true;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = false; // Cancelar;
            btnImprimir.Enabled = Imprimir;

            btnAbrir.Enabled = Guardar;
            //btnAbrir.Enabled = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bExito = false;
            bool bContinua = false;
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;


            if (validarDatos())
            {
                if (!cnn.Abrir())
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    // Apagar la barra completa 
                    IniciarToolBar();

                    cnn.IniciarTransaccion();

                    if (GrabarEncabezado())
                    {
                        if (chkAplicarInv.Checked)
                        {
                            // Eliminar los Detalles que se hayan Cancelado 
                            // if (Eliminar_Detalles_NoRequeridos())
                            {
                                bExito = AfectarExistencia(true, true);
                            }
                        }
                        else
                        {
                            // bExito = true; 
                            bExito = MarcarEnvioDeInformacion(StatusDeRegistro.ActualizacionSuspendida);
                        }
                    }

                    if (bExito)
                    {
                        cnn.CompletarTransaccion();
                        txtFolio.Text = SKU.Foliador; // sFolioMovto.Substring(2);
                        IniciarToolBar(false, false, true);
                        General.msjUser("Información guardada satisfactoriamente con el folio " + sFolioMovto);
                        ImprimirInventario(true);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        txtFolio.Text = sFolioMovto != "" ? SKU.Foliador : "*";
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar la información del inventario.");
                        IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir);
                    }

                    cnn.Cerrar();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bool bExito = false;
            if (validarDatosCancelacion())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    if (GrabarCancelacion())
                    {
                        if (bMovtoAplicado)
                        {
                            bExito = AfectarExistencia(true, false);
                        }
                        else
                        {
                            bExito = true;
                        }
                    }

                    if (bExito)
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Información cancelada satisfactoriamente con el folio " + sFolioMovto);
                        ImprimirInventario(true);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnCancelar_Click");
                        General.msjError("Ocurrió un error al cancelar la información del inventario.");
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                }
            }
        }

        private bool validarImpresion(bool Confirmar)
        {
            bool bRegresa = true;

            if (Confirmar)
            {
                if (General.msjConfirmar(" ¿ Desea imprimir la información en pantalla ? ") == DialogResult.No)
                {
                    bRegresa = false;
                    LimpiarPantalla(false); 
                }
            }

            if (bRegresa)
            {
                if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
                {
                    bRegresa = false;
                    General.msjUser("Folio de Invetario Inicial inválido, verifique.");
                }
            }

            return bRegresa;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInventario(false);
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            FrmIntegrarInventario_Inicial inventario = new FrmIntegrarInventario_Inicial(txtFolio.Text.Trim(), TiposDeInventario.Venta);
            inventario.ShowDialog();

            if (inventario.InformacionIntegrada)
            {
                txtFolio.Text = inventario.FolioGenerado;
                txtFolio_Validating(null, null);
            }
        }
        #endregion Botones

        #region Validacion de datos 
        private bool validarDatos()
        {
            bool bRegresa = true;


            if (bRegresa && txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de movimiento inválido, verifique.");
                txtFolio.Focus();
            }

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las observaciones para el movimiento de inventario, verifique.");
                txtObservaciones.Focus();
            }

            if (bRegresa && bEstaCancelado)
            {
                bRegresa = false;
                General.msjUser("El movimiento de Inventario Inicial se encuentra cancelado,\n no es posible guardar la información.");
            }

            if (bRegresa && !chkAplicarInv.Enabled)
            {
                bRegresa = false;
                General.msjUser("El movimiento de Inventario Inicial ya fue aplicado a la existencia,\n no es posible guardar la información.");
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
                bRegresa = validarCostosProductos();
            }

            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "El usuario no tiene permiso para aplicar un inventario inicial, verifique por favor.";
                ////bRegresa = opPermisosEspeciales.VerificarPermisos("INVENTARIO_INICIAL", sMsjNoEncontrado);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("INVENTARIO_INICIAL", sMsjNoEncontrado);
            }
            return bRegresa;
        }

        /// <summary>
        /// Revisar que todos los Productos tengan un Costo Inicial válido
        /// </summary>
        /// <returns></returns>
        private bool validarCostosProductos()
        {
            bool bRegresa = true;

            if (chkAplicarInv.Checked)
            {
                for (int i = 1; i <= myGrid.Rows; i++)
                {
                    if (myGrid.GetValueDou(i, (int)Cols.Costo) == 0)
                    {
                        bRegresa = false;
                        break; 
                    }
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Alguno de los Productos registrados tienen Costo 0.\n\n" + 
                    "'NO ES POSIBLE APLICAR EL INVENTARIO'"); 
            }

            return bRegresa;
        }

        private bool validarDatosCancelacion()
        {
            bool bRegresa = true;

            if (bRegresa && (txtFolio.Text.Trim() == "*" || txtFolio.Text.Trim() == ""))
            {
                bRegresa = false;
                General.msjUser("Folio de Inventario inválido, verifique.");
            }

            if (bRegresa && bEstaCancelado)
            {
                bRegresa = false;
                General.msjUser("El folio de Inventario ya se encuentra cancelado,\n no es posible cancelarlo de nuevo");
            }

            if (bRegresa && General.msjCancelar("¿ Desea cancelar el folio de Inventario Inicial ?") == DialogResult.No)
            {
                bRegresa = false;
            }

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

            if ( !bRegresa )
                General.msjUser("Debe capturar al menos un producto para el inventario\n y/o capturar cantidades para al menos un lote, verifique.");

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
                General.msjAviso("Se detecto una ó mas diferencias en la captura de productos, el Inventario no puede ser completado.");
                FrmProductosConDiferencias f = new FrmProductosConDiferencias(dtsProductosDiferencias);
                f.ShowDialog();
            }

            return bRegresa;
        }

        #endregion Validacion de datos

        #region Impresion de informacion 
        private void ImprimirInventario(bool ConfirmarImpresion)
        {
            bool bRegresa = false;
            if (validarImpresion(ConfirmarImpresion))
            {
                DatosCliente.Funcion = "ImprimirInventario()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_InventarioInicial.rpt";

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("Folio", "II" + txtFolio.Text);

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


                if (bRegresa)
                {
                    LimpiarPantalla(false);
                }
                else
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Impresion de informacion

        #region Grabar informacion 
        #region Marcado Especial de Informacion 
        private bool Eliminar_Detalles_NoRequeridos()
        {
            bool bRegresa = true;
            string sSql = "";

            if (GnFarmacia.ManejaUbicaciones)
            {
                sSql += string.Format(" Delete From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones " +
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' and Status = 'C'  ",
                    sEmpresa, sEstado, sFarmacia, sFolioMovto);
            }                

            sSql += string.Format(" Delete From MovtosInv_Det_CodigosEAN_Lotes " +
            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' and Status = 'C'  \n\n",
            sEmpresa, sEstado, sFarmacia, sFolioMovto);

            sSql += string.Format(" Delete From MovtosInv_Det_CodigosEAN " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' and Status = 'C'  \n\n",
                sEmpresa, sEstado, sFarmacia, sFolioMovto); 

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private bool CancelarDetallesNoRequeridos()
        {
            bool bRegresa = true;
            string sSql = string.Format(" Update MovtosInv_Det_CodigosEAN Set Status = 'C' " + 
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' \n\n",
                sEmpresa, sEstado, sFarmacia, sFolioMovto);

            sSql += string.Format(" Update MovtosInv_Det_CodigosEAN_Lotes Set Status = 'C' " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' \n\n",
                sEmpresa, sEstado, sFarmacia, sFolioMovto);

            if (GnFarmacia.ManejaUbicaciones)
            {
                sSql += string.Format(" Update MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones Set Status = 'C' " +
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' ",
                    sEmpresa, sEstado, sFarmacia, sFolioMovto);
            }

            bRegresa = leer.Exec(sSql); 

            return bRegresa; 
        }

        private bool MarcarEnvioDeInformacion(StatusDeRegistro Actualizado)
        {
            bool bRegresa = true;
            string sSql = string.Format(" Update MovtosInv_Enc Set Actualizado = {4} " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' \n\n",
                sEmpresa, sEstado, sFarmacia, sFolioMovto, (int)Actualizado);

            sSql += string.Format(" Update MovtosInv_Det_CodigosEAN Set Actualizado = {4} " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' \n\n",
                sEmpresa, sEstado, sFarmacia, sFolioMovto, (int)Actualizado);

            sSql += string.Format(" Update MovtosInv_Det_CodigosEAN_Lotes Set Actualizado = {4} " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' \n\n",
                sEmpresa, sEstado, sFarmacia, sFolioMovto, (int)Actualizado);

            if (GnFarmacia.ManejaUbicaciones) 
            {
                sSql += string.Format(" Update MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones Set Actualizado = {4} " +
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' ",
                    sEmpresa, sEstado, sFarmacia, sFolioMovto, (int)Actualizado);
            }

            bRegresa = leer.Exec(sSql); 

            return bRegresa; 
        }

        #endregion Marcado Especial de Informacion

        private bool AfectarExistencia(bool Aplicar, bool AfectarCosto)
        {
            // Aplicar los Costos Promedios en la tabla de Configuracion para el Calculo de Precios de Ventas 
            bool bRegresa = false;
            string sSql = ""; 
            
            AfectarInventario Inv = AfectarInventario.Ninguno;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            if (Aplicar)
                Inv = AfectarInventario.Aplicar;

            if (AfectarCosto)
                Costo = AfectarCostoPromedio.Afectar;

            sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}' " + 
                "\n" +
                "Exec spp_INV_ActualizarCostoPromedio @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}' \n",
                sEmpresa, sEstado, sFarmacia, sFolioMovto, (int)Inv, (int)Costo);

            bRegresa = leer.Exec(sSql);

            if (DtGeneral.ConfirmacionConHuellas && bRegresa)
            {
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.GrabarPropietarioDeHuella(cnn, sFolioMovto);
            }

            return bRegresa;
        }

        private bool GrabarCancelacion()
        {
            bool bRegresa = false; 
            ////string sSql = ""; 
            ////string sSqlAux = ""; 
            ////string sFolioMovtoAux = sFolioMovto; 

            ////sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
            ////    sEstado, sFarmacia, "*", "IC", "S", sFolioMovto,
            ////    DtGeneral.IdPersonal, "Cancelación de Movimiento de Inventario Inicial : " + sFolioMovto,
            ////    txtSubTotal.Text.Trim().Replace(",", ""), txtIva.Text.Trim().Replace(",", ""), txtTotal.Text.Trim().Replace(",", ""), 1);
            ////if (!leer.Exec(sSql))
            ////{
            ////    bRegresa = false;
            ////}
            ////else
            ////{
            ////    leer.Leer();
            ////    sFolioMovto = leer.Campo("Folio");

            ////    sSqlAux = string.Format("Update MovtosInv_Enc Set Referencia = '{3}', Status = 'C', Actualizado = 0 Where IdEstado = '{0}' and IdFarmacia = '{1}' and FolioMovtoInv = '{2}'  " +
            ////                    " Update MovtosInv_Det_CodigosEAN Set Status = 'C', Actualizado = 0 Where IdEstado = '{0}' and IdFarmacia = '{1}' and FolioMovtoInv = '{2}' " +
            ////                    " Update MovtosInv_Det_CodigosEAN_Lotes Set Status = 'C', Actualizado = 0 Where IdEstado = '{0}' and IdFarmacia = '{1}' and FolioMovtoInv = '{2}' ",
            ////                    sEstado, sFarmacia, sFolioMovtoAux, sFolioMovto);
            ////    if (!leer.Exec(sSqlAux))
            ////    {
            ////        bRegresa = false;
            ////    }
            ////    else
            ////    {
            ////        // Guardar el resto de la informacion 
            ////        bRegresa = GrabarDetalle();
            ////    }
            ////}

            return bRegresa;
        }

        private bool GrabarEncabezado()
        {
            bool bRegresa = false;
            string sSql = ""; 

            ////SKU.Reset(); 

            sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc \n" +  // " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', \n" +
                "\t@IdTipoMovto_Inv = '{4}', @TipoES = '{5}', @Referencia = '{6}', @IdPersonal = '{7}', @Observaciones = '{8}', \n" +
                "\t@SubTotal = '{9}', @Iva = '{10}', @Total = '{11}', @iOpcion = '{12}', @SKU = '{13}', \n" +
                " \t@IdLicitacion = '{14}', @Orden = '{15}', @FolioPresup = '{16}', @IdFuente = '{17}' ",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, sTipoES, "", 
                DtGeneral.IdPersonal, txtObservaciones.Text.Trim(),
                General.GetFormatoNumerico_Double(txtSubTotal.Text),
                General.GetFormatoNumerico_Double(txtIva.Text),
                General.GetFormatoNumerico_Double(txtTotal.Text), 1, SKU.SKU,
                sIdLicitacion, sOrden, sFolioPresup, sIdFuente);
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sFolioMovto = leer.Campo("Folio");
                // txtFolio.Text = sFolioMovto.Substring(2);
                //  bRegresa = GrabarDetalle(); 

                SKU.FolioMovimiento = leer.Campo("Folio");
                SKU.Foliador = leer.Campo("Foliador");
                SKU.SKU = leer.Campo("SKU");

                //// Cancelar todos los CodigosEAN y Lotes 
                if (CancelarDetallesNoRequeridos())
                {
                    if (Eliminar_Detalles_NoRequeridos())
                    {
                        bRegresa = GrabarDetalle();
                    }
                }
            }
            
            return bRegresa;
        }

        private bool GrabarDetalle()
        {
            bool bRegresa = false;
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
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdProducto = '{3}' \n" +
                        "Exec spp_Mtto_FarmaciaProductos_CodigoEAN @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdProducto = '{3}', @CodigoEAN = '{4}' \n",
                        sEmpresa, sEstado, sFarmacia, sIdProducto, sCodigoEAN);
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_Det \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', \n" + 
                            "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @UnidadDeSalida = '{6}', @TasaIva = '{7}', @Cantidad = '{8}', \n" + 
                            "\t@Costo = '{9}', @Importe = '{10}', @Status = '{11}' \n",
                            sEmpresa, sEstado, sFarmacia, sFolioMovto, sIdProducto, sCodigoEAN, iUnidadDeSalida, 
                            nTasaIva, iCantidad, nCosto, nImporte, 'A');
                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                        else
                        {
                            bRegresa = GrabarDetalleLotes(sIdProducto, sCodigoEAN, nCosto);
                            if (!bRegresa)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarDetalleLotes(string IdProducto, string CodigoEAN, double Costo)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            foreach(clsLotes L in ListaLotes)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                            "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', @FechaCaduca = '{7}', @IdPersonal = '{8}',@SKU = '{9}' \n",
                            sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, IdProducto, CodigoEAN, L.ClaveLote,
                            General.FechaYMD(L.FechaCaducidad, "-"), DtGeneral.IdPersonal, SKU.SKU); 

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                            "\t@FolioMovtoInv = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Cantidad = '{8}',\n" +
                            "\t@Costo = '{9}', @Importe = '{10}', @Status = '{11}', @SKU = '{12}' \n",
                            sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A', SKU.SKU);
                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                        else
                        {
                            bRegresa = true;
                            if (GnFarmacia.ManejaUbicaciones) 
                            {
                                bRegresa = GrabarDetalleLotesUbicaciones(L);
                                if(!bRegresa)
                                {
                                    break; 
                                }
                            } 
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarDetalleLotesUbicaciones(clsLotes Lote)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', @IdPasillo = '{7}', @IdEstante = '{8}',\n" +
                        "\t@IdEntrepano = '{9}', @SKU = '{10}' \n",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote,
                        L.Pasillo, L.Estante, L.Entrepano, SKU.SKU);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false; 
                        break;
                    }
                    else
                    {
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes_Ubicaciones \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                            "\t@FolioMovtoInv = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Cantidad = '{8}', \n" +
                            "\t@IdPasillo = '{9}', @IdEstante = '{10}', @IdEntrepano = '{11}', @Status = '{12}', @SKU = '{13}' \n",
                            sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto, 
                            L.IdProducto, L.CodigoEAN, L.ClaveLote, L.Cantidad, L.Pasillo, L.Estante, L.Entrepano, 'A', SKU.SKU);

                        bRegresa = leer.Exec(sSql); 
                        if (!bRegresa)
                        {
                            break;
                        }
                    }
                } 
            }

            return bRegresa; 
        }

        #endregion Grabar informacion

        #region Eventos de Formulario 
        private void FrmInventarioInicial_Load(object sender, EventArgs e)
        {
            PermiteCaducados(); 
            CargaLicitacion();
            CargaFuente();
            LimpiarPantalla(false);
        }

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.G:
                    if (btnGuardar.Enabled)
                        btnGuardar_Click(null, null);
                    break;

                case Keys.N:
                    if (btnNuevo.Enabled)
                        btnNuevo_Click(null, null);
                    break;

                case Keys.P:
                    if (btnImprimir.Enabled)
                        btnImprimir_Click(null, null);
                    break;

                default:
                    break;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if(e.Control)
            {
                TeclasRapidas(e);
            }

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

        #endregion Eventos de Formulario 

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            lblCancelado.Visible = false;
            // bExisteMovto = false;
            bEstaCancelado = false;
            myGrid.Limpiar(false); 

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                IniciarToolBar(true, false, false);
                myGrid.Limpiar(true); 
            }
            else
            {
                if (e == null)
                {
                    e = new CancelEventArgs();
                }

                myGrid.Limpiar(true); 
                leer.DataSetClase = query.FolioMovtoInventario(sEmpresa, sEstado, sFarmacia, 
                    sIdTipoMovtoInv + Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");
                if (leer.Leer())
                {
                    e.Cancel = !CargarDatosMovimiento();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private bool CargarDatosMovimiento()
        {
            bool bRegresa = true;
            // bExisteMovto = true;
            bMovtoAplicado = false;
            bEstaCancelado = false;

            IniciarToolBar(true, true, true);

            sFolioMovto = leer.Campo("Folio");
            txtFolio.Enabled = false;
            txtFolio.Text = Fg.Right(sFolioMovto, 8);

            SKU.SKU = leer.Campo("SKU");
            SKU.Foliador = leer.Campo("Folio");
            SKU.TipoDeMovimiento = leer.Campo("TipoMovto");


            chkAplicarInv.Checked = false;
            if (leer.Campo("MovtoAplicado").ToUpper() == "S")
            {
                chkAplicarInv.Checked = true;
                bMovtoAplicado = true;
            }
            chkAplicarInv.Enabled = !chkAplicarInv.Checked;

            dtpFechaRegistro.Value = leer.CampoFecha("FechaReg");
            txtObservaciones.Text = leer.Campo("Observaciones");
            txtSubTotal.Text = leer.CampoDouble("SubTotal").ToString(sFormato);
            txtIva.Text = leer.CampoDouble("Iva").ToString(sFormato);
            txtTotal.Text = leer.CampoDouble("Total").ToString(sFormato);

            if (leer.Campo("Status").ToUpper() == "C")
            {
                bEstaCancelado = true;
            }

            CargarDetallesMovimiento();
            // No permitir la edición de la informacion 
            myGrid.BloqueaColumna(!chkAplicarInv.Enabled, (int)Cols.Costo);
            myGrid.BloqueaColumna(!chkAplicarInv.Enabled, (int)Cols.CodEAN);
            btnGuardar.Enabled = chkAplicarInv.Enabled;
            btnAbrir.Enabled = btnGuardar.Enabled; 


            // Cargar toda la informacion antes de mostrar el mensaje 
            if (bEstaCancelado)
            {
                lblCancelado.Text = "CANCELADO";
                lblCancelado.Visible = true;
                General.msjUser("El movimiento de inventario inicial actualmente se encuentra cancelado.");
            }
            else if( bMovtoAplicado )
            {
                lblCancelado.Text = "APLICADO";
                lblCancelado.Visible = true;
                General.msjUser("El movimiento de Inventario Inicial ya fue aplicado a la existencia,\n no es posible hacer modificaciones.");
            }

            return bRegresa;
        }

        private void CargarDetallesMovimiento()
        {
            myGrid.Limpiar(true);
            leer.DataSetClase = query.FolioMovtoInventarioDetalle(sEmpresa, sEstado, sFarmacia, sFolioMovto, "CargarDetallesMovimiento");
            if (leer.Leer())
            {
                myGrid.LlenarGrid(leer.DataSetClase);
                CargarDetallesLotesMovimiento();
            }
            Totalizar();
        }

        private void CargarDetallesLotesMovimiento()
        {
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.FolioMovtoInventarioDetalleLotes(sEmpresa, sEstado, sFarmacia, sFolioMovto, "CargarDetallesLotesMovimiento");
            Lotes.AddLotes(leer.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones_Movimiento(sEmpresa, sEstado, sFarmacia, sFolioMovto, "CargarDetallesLotesMovimiento");
                Lotes.AddLotesUbicaciones(leer.DataSetClase);
            }
        }

        #region Manejo Grid 
        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (!bMovtoAplicado)
            {
                if (!bEstaCancelado)
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
        }

        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            switch (ColActiva)
            {
                case Cols.Costo:
                    break;

                case Cols.CodEAN:
                case Cols.Descripcion:
                case Cols.Cantidad:
                case Cols.Importe:
                    if (chkAplicarInv.Enabled)
                    {
                        if (e.KeyCode == Keys.F1)
                        {
                            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN); 
                            leer.DataSetClase = ayuda.ProductosEstado(sEmpresa, sEstado, "grdProductos_KeyDown");
                            if (leer.Leer())
                            {
                                CargarDatosProducto();
                            }
                        }

                        if (e.KeyCode == Keys.Delete)
                        {
                            removerLotes();
                        }

                        //else
                        //{
                        //    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, sValorGrid); 
                        //}
                    }
                    break;
            }
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            bool bEsEAN_Unico = true;

            switch (ColActiva)
            {
                case Cols.CodEAN:
                    string sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                    if (sValor != "")
                    {
                        if (EAN.EsValido(sValor))
                        {
                            leer.DataSetClase = query.ProductosEstado(sEmpresa, sEstado, sValor, "grdProductos_EditModeOff");
                            if (leer.Leer())
                            {
                                if (!GnFarmacia.ValidarSeleccionCodigoEAN(sValor, ref sValor, ref bEsEAN_Unico))
                                {
                                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                    myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN);
                                }
                                else
                                {
                                    if (!bEsEAN_Unico)
                                    {
                                        leer.GuardarDatos(1, "CodigoEAN", sValor); 
                                        //leer.DataSetClase = query.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sValor, "grdProductos_EditModeOff");
                                    }

                                    CargarDatosProducto();
                                }
                            }
                            else
                            {
                                myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                myGrid.ActiveCelda(myGrid.ActiveRow, (int)Cols.CodEAN);
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

        private bool CargarDatosProducto()
        {
            bool bRegresa = true;
            int iRow = myGrid.ActiveRow;
            int iColEAN = (int)Cols.CodEAN;
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
                        myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRow, (int)Cols.CodEAN);
                    }
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

            leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Venta, true, "CargarLotesCodigoEAN()");
            if (query.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                leer.Leer();
                Lotes.AddLotes(leer.DataSetClase);

                if (GnFarmacia.ManejaUbicaciones)
                {
                    leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Venta, true, "CargarLotesCodigoEAN()");
                    if (query.Ejecuto)
                    {
                        leer.Leer();
                        Lotes.AddLotesUbicaciones(leer.DataSetClase);
                    }
                }

                mostrarOcultarLotes();
            }
        }

        private void removerLotes()
        {
            if (chkAplicarInv.Enabled)
            {
                if (!bEstaCancelado)
                {
                    try
                    {
                        int iRow = myGrid.ActiveRow;
                        Lotes.RemoveLotes(myGrid.GetValue(iRow, (int)Cols.Codigo), myGrid.GetValue(iRow, (int)Cols.CodEAN));
                        myGrid.DeleteRow(iRow);
                        Totalizar();
                    }
                    catch { }

                    if (myGrid.Rows == 0)
                    {
                        myGrid.Limpiar(true);
                    }
                }
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
                    Lotes.EsEntrada = true;
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    Lotes.PermitirLotesNuevosConsignacion = false;
                    Lotes.EsConsignacion = false;
                    Lotes.IdSubFarmacia = sIdFuente;
                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = chkAplicarInv.Enabled;
                    Lotes.ModificarCantidades = chkAplicarInv.Enabled;

                    // Solo para Movientos Especiales   // Jesus Diaz 2K120217.1520 
                    Lotes.PermitirSalidaCaducados = bPermitirSacarCaducados; 

                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.Default;

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;
                    Lotes.Show();

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
    }
}
