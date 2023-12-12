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
using SC_SolutionsSystem.OfficeOpenXml;
using SC_SolutionsSystem.OfficeOpenXml.Data;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllFarmaciaSoft.ExportarExcel; 

namespace OficinaCentral.CfgPrecios
{
    public partial class FrmPreciosClavesSSA : FrmBaseExt
    {
        private enum Cols
        {
            IdClave = 1, ClaveSSA = 2, Descripcion = 3, 
            TipoInsumo = 4, ContenidoPaquete = 5, PrecioUnitario = 6, Precio = 7,
            ContenidoPaquete_Licitacion = 8, IdPresentacionLicitacion = 9, PresentacionLicitacion = 10, Dispensacion_CajasCompletas = 11,
            Status = 12
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;
        clsLeer reader, reader2;

        clsGrid myGrid;
        FrmPreciosClavesSSA_Programas f;
        // Thread _workerThread;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb;

        //clsLeerExcel excel;
        clsLeerExcelOpenOficce excel;
        //clsExportarExcelPlantilla xpExcel;
        OpenFileDialog openExcel = new OpenFileDialog();
        Thread thLoadFile;

        string sNombreHoja = "CuadroBasico".ToUpper();
        bool bExisteHoja = false;

        int iRegistrosHoja = 0;
        int iRegistrosProcesados = 0;

        string sTitulo = "";
        string sFile_In = "";
        string sFormato_INT = "###, ###, ###, ##0";
        int iFolioCargaMasiva = 0;

        bool bValidandoInformacion = false;
        bool bSeEncontraronIndicencias = false;
        bool bActivarProceso = false;
        bool bErrorAlValidar = false;
        bool bErrorAlCargarPlantilla = false;
        string sMensajeError_CargarPlantilla = "";

        int iPosicion_Oculta = 0;
        int iPosicion_Mostrar_Top = 0;
        int iPosicion_Mostrar_Left = 0;

        #region Permisos Especiales 
        //clsOperacionesSupervizadas Permisos;
        bool bPermiteCambiarPrecios = GnOficinaCentral.SePermiteModificarPreciosLicitacion;

        string sPermisoCambiarPreciosLicitacion = "ASIGNAR_MODIFICAR_PRECIOS_LICITACION";
        bool bPuedeCambiarPrecios = false;

        bool bHabilitarGuardar = false;
        bool bClaveGuardada = false;
        #endregion Permisos Especiales


        // bool bExito = true;
        bool bGuardando = false;
        bool bLimpiarGrid = false;
        // bool bInicioPantalla = true;
        // bool bEjecucionEnHilo = false;
        int iVacio = 0; 
        string sFormato = "#,###,###,##0.###0"; 

        public FrmPreciosClavesSSA()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false);
            query.MostrarMsjSiLeerVacio = true;
            ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name); 
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            reader = new clsLeer(ref cnn);
            reader2 = new clsLeer(ref cnn);

            myGrid = new clsGrid(ref grpProductos, this);
            myGrid.BackColorColsBlk = Color.White;
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.AjustarAnchoColumnasAutomatico = true; 

            grpProductos.EditModeReplace = true;
            myGrid.SetOrder(true);

            // grpProductos.Sheets[0].Columns[3-1].AllowAutoFilter = true; 

            SolicitarPermisosUsuario();
            ValidarPermisosParaModificarPrecios();
            LlenaPresentaciones();


            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;


            FrameProceso.Left = 816;

            iPosicion_Oculta = (int)General.Pantalla.Ancho;
            iPosicion_Mostrar_Top = (FrameListaProductos.Height / 2) - (FrameProceso.Height / 2);
            iPosicion_Mostrar_Left = (FrameListaProductos.Width / 2) - (FrameProceso.Width / 2);

            MostrarEnProceso(false, 0);
            this.Width = 1206;

            General.Pantalla.AjustarTamaño(this, 90, 80); 

            // Permitir ordenar Columnas 

            //if (DtGeneral.EsAdministrador)
            {
                btnExportarExcel.Visible = DtGeneral.EsAdministrador;
                btnAbrir.Visible = DtGeneral.EsAdministrador;
                btnGuardarMasivo.Visible = DtGeneral.EsAdministrador; 
            }

        }

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            ////// Valida si el usuario conectado tiene permiso sobre las opcione especiales 
            ////Permisos = new clsOperacionesSupervizadas(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            ////Permisos.Personal = DtGeneral.IdPersonal;
            bPuedeCambiarPrecios = DtGeneral.PermisosEspeciales.TienePermiso(sPermisoCambiarPreciosLicitacion); 
        }

        private void CargarEstados()
        {
            if (cboEstados.NumeroDeItems == 0)
            {
                cboEstados.Clear();
                cboEstados.Add("0", "<< Seleccione >>");
                cboEstados.Add(query.EstadosConFarmacias("FrmPreciosClavesSSA"), true, "IdEstado", "NombreEstado");
            }

            cboEstados.SelectedIndex = 0; 
            if (!DtGeneral.EsAdministrador)
            {
                if (!bPuedeCambiarPrecios)
                {
                    cboEstados.Data = DtGeneral.EstadoConectado;
                    cboEstados.Enabled = false;
                }
            }

        }

        private void ValidarPermisosParaModificarPrecios()
        {
            bHabilitarGuardar = false;
            if (bPermiteCambiarPrecios || bPuedeCambiarPrecios || DtGeneral.EsAdministrador)
            {
                bHabilitarGuardar = true;
            }
        }
        #endregion Permisos de Usuario 

        private void FrmPreciosClavesSSA_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Botones 
        private void IniciarToolBar()
        {
            IniciarToolBar(true, true, false);
        }

        //private void IniciaToolBar(bool Nuevo, bool Exportar, bool Abrir, bool Guardar)
        //{
        //    btnNuevo.Enabled = Nuevo;
        //    btnExportarExcel.Enabled = Exportar;
        //    btnAbrir.Enabled = Abrir;
        //    btnGuardar.Enabled = Guardar;
        //}

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Ejecutar)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnEjecutar.Enabled = Ejecutar;

            if (!bHabilitarGuardar)
            {
                btnGuardar.Enabled = bHabilitarGuardar;
                btnCancelar.Enabled = bHabilitarGuardar;
            }

            btnExportarExcel.Enabled = btnGuardar.Enabled;
            btnAbrir.Enabled = btnGuardar.Enabled;
            btnGuardarMasivo.Enabled = btnGuardar.Enabled; 
        }

        private void LimpiarPantalla()
        {
            //lblMensaje.BorderStyle = BorderStyle.None;
            //lblMensaje.Text = "GUARDANDO INFORMACIÓN";
            //lblMensaje.Visible = false;
            //pgBar.Visible = false;

            IniciarToolBar(false, false, false); 
            Fg.IniciaControles();

            CargarEstados();
            cboEstados.Focus(); 
        }

        private void LlenaPresentaciones()
        {
            cboPresentaciones.Clear();
            cboPresentaciones.Add("0", "<< Seleccione >>");
            leer.DataSetClase = query.ComboPresentaciones("LlenaPresentaciones");
            if (leer.Leer())
            {
                cboPresentaciones.Add(leer.DataSetClase, true);
            }
            cboPresentaciones.SelectedIndex = 0;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar(1); 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Guardar(2); 
        }

        private void Guardar(int Opcion)
        {
            string sSql = "";
            int iDispensarCaja = chkDispensarCaja.Checked ? 1 : 0;

            if (validaDatos())
            {
                if(!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else 
                {
                    cnn.IniciarTransaccion();

                    sSql = string.Format(" Exec spp_CFG_AsignarPrecios_ClavesSSA \n" +
                        "\t@IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdClaveSSA_Sal = '{3}', \n" +
                        "\t@Precio = '{4}', @IdEstadoPersonal = '{5}', @IdFarmaciaPersonal = '{6}', @IdPersonal = '{7}', @iOpcion = '{8}', @Factor = '{9}', \n" +
                        "\t@ContenidoPaquete_Licitado = '{10}', @IdPresentacion_Licitado = '{11}', @Dispensacion_CajasCompletas = '{12}', \n" +
                        "\t@SAT_ClaveDeProducto_Servicio  = '{13}', @SAT_UnidadDeMedida = '{14}' \n", 
                        cboEstados.Data, txtCte.Text, txtSubCte.Text, txtClaveSSA.Text,
                        txtPrecio.NumericText, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, Opcion, nudFactor.Value,
                        txtContenidoPaqLic.NumericText, cboPresentaciones.Data, iDispensarCaja,
                        txtProductoSAT.Text.Trim(), txtUnidadMedidaSAT.Text.Trim());

                    if (!leer.Exec(sSql))
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Información guardada satisfactoriamente."); 
                        
                        CargarClavesSSA_Asignadas_Precio(); 
                    }

                    cnn.Cerrar();
                }
            } 
        }

        private bool validaDatos()
        {
            bool bRegresa = true;

            SendKeys.Send("{TAB}");

            if (txtClaveSSA.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave SSA inválida, verifique."); 
                txtClaveSSA.Focus(); 
            }

            return bRegresa;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarClavesSSA_Asignadas_Precio(); 
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DatosCliente.Funcion = "btnImprimir_Click()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            string sEstado = "";
            bool bRegresa = false; 

            if (cboEstados.SelectedIndex != 0)
            {
                sEstado = cboEstados.Data;
            }

            myRpt.RutaReporte = GnOficinaCentral.RutaReportes;
            myRpt.NombreReporte = "Central_Listado_ClavesSSA_PreciosAsignados";
            myRpt.Add("@IdEstado", sEstado);
            myRpt.Add("@IdCliente", txtCte.Text);
            myRpt.Add("@IdSubCliente", txtSubCte.Text);

            bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente); 

            ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            ////DataSet datosC = DatosCliente.DatosCliente();

            ////conexionWeb.Timeout = 300000;
            ////btReporte = conexionWeb.Reporte(InfoWeb, datosC);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (validarParametros())
            {
                openExcel.Title = "Archivos de Cuadro Básico";
                openExcel.Filter = "Archivos de Excel (*.xls;*.xlsx)| *.xls;*.xlsx";
                //openExcel.Filter = "Archivos de Texto (*.txt)| *.txt"; 
                openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
                openExcel.AddExtension = true;
                lblProcesados.Visible = false;

                // if (openExcel.FileName != "")
                if (openExcel.ShowDialog() == DialogResult.OK)
                {
                    sFile_In = openExcel.FileName;

                    BloqueaControles(true);
                    IniciarToolBar(false, false, false);

                    tmValidacion.Enabled = true;
                    tmValidacion.Interval = 1000;
                    tmValidacion.Start();


                    thLoadFile = new Thread(this.CargarArchivo);
                    thLoadFile.Name = "LeerArchivo";
                    thLoadFile.Start();
                }
            }
        }

        private void btnGuardarMasivo_Click(object sender, EventArgs e)
        {
            string sMensaje = "¿ Desea integrar la información de Cuadro Básico Precios, este proceso no se podra deshacer ?";

            if (General.msjConfirmar(sMensaje) == System.Windows.Forms.DialogResult.Yes)
            {
                IntegrarInformacion();
            }
        }
        #endregion Botones
        
        #region Buscar ClaveSSA 
        private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveSSA.Text.Trim() != "")
            {
                leer.DataSetClase = query.ClavesSSA_Asignadas_A_Clientes(txtCte.Text, txtSubCte.Text, txtClaveSSA.Text, "txtClaveSSA_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Producto no encontrada, verifique.");
                    //e.Cancel = true;
                }
                else
                {
                    CargarDatosClaveSSA(); 
                }
            }
            else
            {
                lblDescripcion.Text = "";
            }

        } 
        #endregion Buscar ClaveSSA
        
        #region Buscar Cliente 
        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            //leer = new clsLeer(ref cnn);

            if (txtCte.Text.Trim() != "")
            {
                leer.DataSetClase = query.ClientesClavesSSA_Asignadas(txtCte.Text.Trim(), "txtId_Validating");
                if (leer.Leer())
                    CargaDatosCliente();
            }
        }

        private void CargaDatosCliente()
        {   
            //Se hace de esta manera para la ayuda. 
            txtCte.Enabled = false; 
            txtCte.Text = leer.Campo("IdCliente");
            lblCte.Text = leer.Campo("NombreCliente");
        } 
        #endregion Buscar Cliente

        #region Buscar SubCliente 
        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            //leer = new clsLeer(ref cnn);

            if (txtSubCte.Text.Trim() != "")
            {
                leer.DataSetClase = query.ClientesClavesSSA_Asignadas(txtCte.Text.Trim(), txtSubCte.Text, "txtSubCte_Validating");
                if (leer.Leer())
                {
                    CargaDatosSubCliente();
                }
            }
        }

        private void CargaDatosSubCliente()
        {
            //Se hace de esta manera para la ayuda. 
            txtSubCte.Enabled = false; 
            txtSubCte.Text = leer.Campo("IdSubCliente");
            lblSubCte.Text = leer.Campo("NombreSubCliente");

            IniciarToolBar(true, false, true); 
            CargarClavesSSA_Asignadas_Precio(); 
        }

        private void CargarClavesSSA_Asignadas_Precio()
        {
            txtClaveSSA.Text = "";
            lblClaveSSA.Text = "";
            lblDescripcion.Text = "";
            txtPrecio.Text = iVacio.ToString(sFormato);
            nudFactor.Value = nudFactor.Minimum;
            txtContenidoPaqLic.Text = "";
            chkDispensarCaja.Checked = false;
            cboPresentaciones.SelectedIndex = 0;

            txtUnidadMedidaSAT.Text = "";
            lblUnidadMedidaSAT.Text = "";

            txtProductoSAT.Text = "";
            lblProductoSAT.Text = "";

            myGrid.Limpiar();
            // if ( query.ExistenDatos ) 
            System.Threading.Thread.Sleep(200);
            this.Refresh(); 

            myGrid.LlenarGrid(query.ClientesClavesSSA_Asignadas_Precios(cboEstados.Data, txtCte.Text, txtSubCte.Text, "CargarClavesSSA_Asignadas_Precio()()"));
            txtClaveSSA.Focus();

        }

        private void CargarDatosClaveSSA()
        {
            txtClaveSSA.Text = leer.Campo("IdClaveSSA_Sal");
            lblClaveSSA.Text = leer.Campo("ClaveSSA");
            lblDescripcion.Text = leer.Campo("DescripcionSal");
            lblTipoInsumo.Text = leer.Campo("TipoDeClaveDescripcion"); 
            txtPrecio.Text = leer.CampoDouble("Precio").ToString(sFormato);
            lblContenidoPaquete.Text = leer.Campo("ContenidoPaquete");
            lblPrecioUnitario.Text = leer.CampoDouble("PrecioUnitario").ToString(sFormato);
            nudFactor.Value = leer.CampoInt("Factor");

            txtContenidoPaqLic.Text = leer.Campo("ContenidoPaquete_Licitado");
            cboPresentaciones.Data = leer.Campo("IdPresentacion_Licitado");
            chkDispensarCaja.Checked = leer.CampoBool("Dispensacion_CajasCompletas");

            txtUnidadMedidaSAT.Text = leer.Campo("SAT_UnidadDeMedida");
            lblUnidadMedidaSAT.Text = leer.Campo("Descripcion_SAT_UnidadDeMedida");

            txtProductoSAT.Text = leer.Campo("SAT_ClaveDeProducto_Servicio");
            lblProductoSAT.Text = leer.Campo("Descripcion_SAT_ClaveDeProducto_Servicio");

            if (leer.CampoBool("PrecioAsignado"))
            {
                IniciarToolBar(true, true, true);
            }
        } 
        #endregion Buscar SubCliente

        #region Grid 
        private void LlenaGrid()
        {
            ////string sSql = " Select IdProducto, Descripcion, UtilidadProducto, PrecioMaxPublico, DescuentoGral, " +
            ////    " UtilidadProducto as UtilidadProductoBase, PrecioMaxPublico as PrecioMaxPublicoBase, DescuentoGral as DescuentoGralBase " +
            ////    " From CatProductos (NoLock) Order by IdProducto ";
            btnGuardar.Enabled = false;

            //if (validaDatosExec())
            //{
            //    myGrid.Limpiar();
            //    if (!leer.Exec(sSql))
            //    {
            //        Error.GrabarError(leer, "btnEjecutar_Click");
            //        General.msjError("Ocurrió un error al obtener la lista de productos.");
            //    }
            //    else
            //    {
            //        btnGuardar.Enabled = true;
            //        txtClaveSSA.Enabled = false;
            //        myGrid.LlenarGrid(leer.DataSetClase);
            //    }
            //}
        } 
        #endregion Grid

        #region Eventos 
        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.ClavesSSA_Asignadas_A_Clientes(txtCte.Text, txtSubCte.Text, "txtClaveSSA_Validating");
                if (leer.Leer())
                {
                    CargarDatosClaveSSA(); 
                }
            }
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.ClientesClavesSSA_Asignadas("txtCte_KeyDown");
                if (leer.Leer())
                {
                    CargaDatosCliente();
                }
            }
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.ClientesClavesSSA_Asignadas(txtCte.Text, "txtSubCte_KeyDown");
                if (leer.Leer())
                {
                    CargaDatosSubCliente();
                }
            } 
        }

        private void tmEjecucion_Tick(object sender, EventArgs e)
        {
            if (!bGuardando)
            {
                tmEjecucion.Enabled = false;
                tmEjecucion.Stop();

                grpProductos.Enabled = true;
                if (bLimpiarGrid)
                {
                    myGrid.Limpiar();
                }
            }
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            myGrid.Limpiar();
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;
            }
        }

        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            lblCte.Text = "";
            txtSubCte.Text = "";
            lblSubCte.Text = "";
            txtClaveSSA.Text = "";
            myGrid.Limpiar();
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCte.Text = "";
            txtClaveSSA.Text = "";
            myGrid.Limpiar();
        }

        private void txtClaveSSA_TextChanged(object sender, EventArgs e)
        {
            lblClaveSSA.Text = "";
            lblDescripcion.Text = "";
            lblTipoInsumo.Text = "";
            lblContenidoPaquete.Text = iVacio.ToString(); 
            txtPrecio.Text = iVacio.ToString(sFormato); 
            lblPrecioUnitario.Text = iVacio.ToString(sFormato);
            bClaveGuardada = false;
        } 
        #endregion Eventos

        private void LevantarClaveSSA()
        {
            string sClave = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.IdClave);

            if (sClave != "")
            {
                txtClaveSSA.Text = sClave;
                txtClaveSSA_Validating(null, null);
                txtPrecio.Focus(); 
            } 
        }

        private void grpProductos_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            LevantarClaveSSA();
            bClaveGuardada = true;
        }

        private void grpProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                LevantarClaveSSA();
            }
        }

        private void ActualizarPrecio()
        {
            int _0 = 0;
            double dPrecio = Convert.ToDouble("0" + txtPrecio.Text.Replace(",", ""));
            double dPrecioCaja = Convert.ToDouble("0" + lblContenidoPaquete.Text) * dPrecio;

            //lblContenidoPaquete.Text = "0";
            lblPrecioUnitario.Text = dPrecioCaja.ToString(sFormato);
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            ActualizarPrecio();
        }

        private void txtPrecio_Validating(object sender, CancelEventArgs e)
        {
            ActualizarPrecio();

            if (txtPrecio.Text.Trim() == "")
            {
                txtPrecio.Text = "0"; 
            } 
        }

        private void FrmPreciosClavesSSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                if (bClaveGuardada)
                {
                    f = new FrmPreciosClavesSSA_Programas();
                    f.show(cboEstados.Data, txtCte.Text, txtSubCte.Text, txtClaveSSA.Text);
                }
            }
        }

        #region Funciones y Procedimientos Privados
        private void BloqueaControles(bool Bloquear)
        {
            bool bBloquear = !Bloquear;

            cboEstados.Enabled = bBloquear;
            txtCte.Enabled = bBloquear;
            txtSubCte.Enabled = bBloquear;
        }

        private bool validarParametros()
        {
            bool bRegresa = true;

            if (bRegresa && txtCte.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Cliente válido.");
                txtCte.Focus();
            }

            if (bRegresa && txtSubCte.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Sub-Cliente válido.");
                txtSubCte.Focus();
            }

            return bRegresa;
        }

        private void MostrarEnProceso(bool Mostrar, int Proceso)
        {
            string sTituloProceso = "";

            iPosicion_Mostrar_Top = (FrameListaProductos.Height / 2) - (FrameProceso.Height / 2);
            iPosicion_Mostrar_Left = (FrameListaProductos.Width / 2) - (FrameProceso.Width / 2);

            FrameProceso.Top = iPosicion_Mostrar_Top;
            if(Mostrar)
            {
                FrameProceso.Left = iPosicion_Mostrar_Left;
            }
            else
            {
                FrameProceso.Left = iPosicion_Oculta * 2;
            }


            if (Proceso == 1)
            {
                sTituloProceso = "Leyendo estructura del documento";
            }

            if (Proceso == 2)
            {
                sTituloProceso = "Leyendo información de hoja seleccionada";
            }

            if (Proceso == 3)
            {
                sTituloProceso = "Guardando información de hoja seleccionada";
            }

            if (Proceso == 4)
            {
                sTituloProceso = "Verificando información a integrar";
            }

            if (Proceso == 5)
            {
                sTituloProceso = "Integrando información ..... ";
            }

            FrameProceso.Text = sTituloProceso;

        }

        private void CargarArchivo()
        {
            string sHoja = "";
            bool bRegresa = false;

            bValidandoInformacion = true;
            bErrorAlCargarPlantilla = false;
            sMensajeError_CargarPlantilla = "";
            bErrorAlValidar = false; 

            MostrarEnProceso(true, 1);
            //FrameResultado.Text = sTitulo;

            //excel = new clsLeerExcel(sFile_In); 
            excel = new clsLeerExcelOpenOficce(sFile_In);
            excel.GetEstructura();

            //lst.Limpiar();
            Thread.Sleep(1000);

            //bRegresa = excel.Hojas.Registros > 0;
            while (excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja").ToUpper();
                if (sHoja == sNombreHoja)
                {
                    bRegresa = true;
                    break;
                }
            }

            bExisteHoja = bRegresa;
            if (bRegresa)
            {
                bRegresa = LeerHoja();
            }

            MostrarEnProceso(false, 1);
            bValidandoInformacion = false;
            bActivarProceso = bRegresa;


            ////IniciaToolBar(true, true, true, false); 


            //////if (!bRegresa)
            //////{
            //////    IniciaToolBar(true, true, true, false); 
            //////}
            //////else
            //////{
            //////    IniciaToolBar(true, true, false, true); 
            //////    //General.msjUser("Información precargada");
            //////}

            //return bRegresa;
        }

        private bool LeerHoja()
        {
            bool bRegresa = false;

            //FrameResultado.Text = sTitulo;
            //lst.Limpiar();
            excel.LeerHoja(sNombreHoja);

            //FrameResultado.Text = sTitulo;
            if (excel.Leer())
            {
                bRegresa = true;
                iRegistrosHoja = excel.Registros;
                //FrameResultado.Text = string.Format("{0}: {1} registros ", sTitulo, iRegistrosHoja.ToString(sFormato));
                //lst.CargarDatos(excel.DataSetClase, true, true);
            }

            if (bRegresa)
            {
                bRegresa = CargarInformacionDeHoja();
            }

            return bRegresa;
        }

        private DataSet FormatearCampo(DataSet Datos, string NombreColumna, TypeCode typeCode )
        {
            clsLeer datosProceso = new clsLeer();

            try
            {
                datosProceso.DataSetClase = Datos;

                if(datosProceso.ExisteTablaColumna(sNombreHoja, NombreColumna))
                {
                    datosProceso.DataSetClase = TipoColumna(Datos, sNombreHoja, NombreColumna, typeCode);
                }
            }
            catch 
            {
                datosProceso.DataSetClase = Datos;
            }

            return datosProceso.DataSetClase; 
        }

        private DataSet FormatearDatos()
        {
            DataSet datosProceso = excel.DataSetClase;

            datosProceso = FormatearCampo(datosProceso, "IdEstado", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "IdCliente", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "IdSubCliente", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "IdClaveSSA", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "ClaveSSA", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "Precio", TypeCode.Decimal);
            datosProceso = FormatearCampo(datosProceso, "Status", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "Factor", TypeCode.Double);
            datosProceso = FormatearCampo(datosProceso, "ContenidoPaquete_Licitado", TypeCode.Double);
            datosProceso = FormatearCampo(datosProceso, "IdPresentacion_Licitado", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "Dispensacion_CajasCompletas", TypeCode.Int32);
            datosProceso = FormatearCampo(datosProceso, "SAT_UnidadDeMedida", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "SAT_ClaveDeProducto_Servicio", TypeCode.String);

            return datosProceso; 

        }

        private bool CargarInformacionDeHoja()
        {
            bool bRegresa = false;
            lblProcesados.Visible = true;
            lblProcesados.Text = "";
            Application.DoEvents();


            excel.DataSetClase = FormatearDatos();

            clsBulkCopy bulk = new clsBulkCopy(General.DatosConexion);

            bulk.NotifyAfter = 500;
            bulk.RowsCopied += new clsBulkCopy.RowsCopiedEventHandler(bulk_RowsCopied);
            bulk.OnCompled += new clsBulkCopy.RowsCopiedEventHandler(bulk_Compled);
            bulk.OnError += new clsBulkCopy.RowsCopiedEventHandler(bulk_Error);

            bulk.ClearColumns();
            bulk.DestinationTableName = "CFG_ClavesSSA_Precios__CargaMasiva";
            bulk.AddColumn("IdEstado", "IdEstado");
            bulk.AddColumn("IdCliente", "IdCliente");
            bulk.AddColumn("IdSubCliente", "IdSubCliente");


            if (excel.ExisteTablaColumna(1, "IdClaveSSA".ToUpper()))
            {
                bulk.AddColumn("IdClaveSSA", "IdClaveSSA_Sal");
            }

            if (excel.ExisteTablaColumna(1, "ClaveSSA".ToUpper()))
            {
                bulk.AddColumn("ClaveSSA", "ClaveSSA");
            }

            if (excel.ExisteTablaColumna(1, "Precio".ToUpper()))
            {
                bulk.AddColumn("Precio", "Precio");
            }

            bulk.AddColumn("Status", "Status");
            bulk.AddColumn("Factor", "Factor");



            if (excel.ExisteTablaColumna(1, "ContenidoPaquete_Licitado".ToUpper()))
            {
                bulk.AddColumn("ContenidoPaquete_Licitado", "ContenidoPaquete_Licitado");
            }

            if (excel.ExisteTablaColumna(1, "IdPresentacion_Licitado".ToUpper()))
            {
                bulk.AddColumn("IdPresentacion_Licitado", "IdPresentacion_Licitado");
            }

            if (excel.ExisteTablaColumna(1, "Dispensacion_CajasCompletas".ToUpper()))
            {
                bulk.AddColumn("Dispensacion_CajasCompletas", "Dispensacion_CajasCompletas");
            }

            if (excel.ExisteTablaColumna(1, "SAT_UnidadDeMedida".ToUpper()))
            {
                bulk.AddColumn("SAT_UnidadDeMedida", "SAT_UnidadDeMedida");
            }

            if (excel.ExisteTablaColumna(1, "SAT_ClaveDeProducto_Servicio".ToUpper()))
            {
                bulk.AddColumn("SAT_ClaveDeProducto_Servicio", "SAT_ClaveDeProducto_Servicio");
            }


            reader.Exec("Truncate table CFG_ClavesSSA_Precios__CargaMasiva ");
            bRegresa = bulk.WriteToServer(excel.DataSetClase); //, System.Data.SqlClient.SqlBulkCopyOptions.Default); 


            if (bRegresa)
            {
                bRegresa = validarInformacion();
            }
            else
            {
                bErrorAlCargarPlantilla = true;
                sMensajeError_CargarPlantilla = bulk.Error.Message;
            }

            return bRegresa;
        }

        private static DataSet TipoColumna( DataSet Datos, string Tabla, string NombreColumna, TypeCode typeCode )
        {
            DataSet dts = Datos.Copy();
            DataTable dt;
            clsLeer leer = new clsLeer();
            int iRenglones = 0;

            leer.DataSetClase = Datos;
            dt = leer.Tabla(Tabla);

            try
            {
                Type type = Type.GetType("System." + typeCode);


                // Agregar columna Temporal
                DataColumn dc = new DataColumn(NombreColumna + "_new", type);
                string sValue = ""; 

                //Darle la posición de a la columna nueva de la original
                int ordinal = dt.Columns[NombreColumna].Ordinal;
                dt.Columns.Add(dc);
                dc.SetOrdinal(ordinal);

                //leer el valor y convertirlo 
                foreach(DataRow dr in dt.Rows)
                {
                    iRenglones++;

                    try
                    {
                        if(typeCode == TypeCode.Boolean)
                        {
                            switch(dr[NombreColumna].ToString())
                            {
                                case "0":
                                    dr[dc.ColumnName] = false;
                                    break;
                                case "1":
                                    dr[dc.ColumnName] = true;
                                    break;
                                default:
                                    dr[dc.ColumnName] = Convert.ChangeType(dr[NombreColumna], typeCode);
                                    break;
                            }
                        }
                        else
                        {
                            if(typeCode == TypeCode.Decimal || typeCode == TypeCode.Double || typeCode == TypeCode.Int16 || typeCode == TypeCode.Int32 || typeCode == TypeCode.Int64)
                            {
                                sValue = dr[NombreColumna].ToString().Replace(",", "");

                                //dr[dc.ColumnName] = Convert.ChangeType(dr[NombreColumna], typeCode);
                                dr[dc.ColumnName] = Convert.ChangeType(sValue, typeCode);
                            }
                            else
                            {
                                dr[dc.ColumnName] = Convert.ChangeType(dr[NombreColumna], typeCode);
                            }
                        }
                    }
                    catch(Exception exception_01)
                    {
                        exception_01 = null;
                    }
                }


                // remover columna vieja
                dt.Columns.Remove(NombreColumna);


                // Cambiar nombre a columna nueva
                dc.ColumnName = NombreColumna;
            }
            catch(Exception exception)
            {
                exception = null;
            }

            leer.DataTableClase = dt;

            return leer.DataSetClase.Copy();
        }

        private void bulk_RowsCopied(RowsCopiedEventArgs e)
        {
            lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Compled(RowsCopiedEventArgs e)
        {
            lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Error(RowsCopiedEventArgs e)
        {
            lblProcesados.Text = string.Format("Ocurrio un error al procesar {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
            Error.GrabarError(e.Error, "bulk_Error");
        }

        private bool validarInformacion()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_PRCS_OCEN__IntegracionDePrecios_000_Validar_Datos  @IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @Tipo = '{3}'   ",
                cboEstados.Data, txtCte.Text, txtSubCte.Text, 1);

            if (!reader.Exec(sSql))
            {
                bErrorAlValidar = true;
                sMensajeError_CargarPlantilla = "Ocurrió un error al validar los datos de la plantilla."; 
                Error.GrabarError(reader, "validarInformacion()");
            }
            else
            {
                bRegresa = ValidarInformacion_Entrada();
            }

            return bRegresa;
        }

        private bool ValidarInformacion_Entrada()
        {
            bool bRegresa = true;
            clsLeer leerValidacion = new clsLeer();
            clsLeer leerResultado = new clsLeer();
            DataSet dtsResultados = new DataSet();

            ////leer.RenombrarTabla(1, "IMSS no registrados");
            ////leer.RenombrarTabla(2, "Condiciones de pago");
            ////leer.RenombrarTabla(3, "Método de pago");
            ////leer.RenombrarTabla(4, "Cuenta de Pago");
            reader.RenombrarTabla(1, "Resultados");
            leerResultado.DataTableClase = reader.Tabla(1);
            while (leerResultado.Leer())
            {
                reader.RenombrarTabla(leerResultado.CampoInt("Orden"), leerResultado.Campo("NombreTabla"));
            }

            dtsResultados = reader.DataSetClase;
            dtsResultados.Tables.Remove("Resultados");
            reader.DataSetClase = dtsResultados;

            foreach (DataTable dt in reader.DataSetClase.Tables)
            {
                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = dt.Copy();
                    bActivarProceso = leerValidacion.Registros > 0;
                }
            }

            bRegresa = !bActivarProceso;

            return bRegresa;
        }

        private bool IntegrarInformacion()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_PRCS_OCEN__IntegracionDePrecios  @IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @Tipo = '{3}' ",
                cboEstados.Data, txtCte.Text, txtSubCte.Text, 1);

            btnGuardar.Enabled = false;

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion(); 
            }
            else
            {
                cnn.IniciarTransaccion();

                if (!reader.Exec(sSql))
                {
                    Error.GrabarError(reader, "IntegrarInformacion()");
                    btnGuardar.Enabled = true;
                }
                else
                {
                    bRegresa = true;
                }


                if (bRegresa)
                {
                    cnn.CompletarTransaccion(); 
                    General.msjUser("Cuadro Básico integrado satisfactoriamente.");
                    btnEjecutar_Click(null, null); 
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(reader, "IntegrarInformacion()"); 
                    General.msjError("Ocurrió un error al integrar la información.");
                }

                cnn.Cerrar(); 
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Privados

        private void tmValidacion_Tick(object sender, EventArgs e)
        {
            tmValidacion.Stop();
            tmValidacion.Enabled = false;

            if (!bValidandoInformacion)
            {
                if (!bExisteHoja)
                {
                    General.msjAviso("El archivo cargado no contiene la hoja llamada CuadroBasico.");
                }
                else
                {
                    if (bActivarProceso)
                    {
                        BloqueaControles(true);
                        IniciarToolBar(true, true, true);
                    }
                    else
                    {
                        BloqueaControles(false);
                        IniciarToolBar(true, true, false);
                        if (bErrorAlCargarPlantilla)
                        {
                            General.msjAviso(sMensajeError_CargarPlantilla); 
                        }
                        else 
                        {
                            if (!bErrorAlValidar)
                            {
                                FrmIncidencias f = new FrmIncidencias("Cuadro Básico Precios", reader.DataSetClase);
                                f.ShowDialog(this);
                            }
                            else
                            {
                                General.msjError(sMensajeError_CargarPlantilla); 
                            }
                        }
                    }
                }
            }
            else
            {
                tmValidacion.Enabled = true;
                tmValidacion.Start();
            }
        }

        private void txtUnidadMedidaSAT_Validating(object sender, CancelEventArgs e)
        {
            if (txtUnidadMedidaSAT.Text.Trim() != "")
            {
                leer.DataSetClase = query.CFD_UnidadesDeMedida(txtUnidadMedidaSAT.Text.Trim(), "txtUnidadMedidaSAT");
                if(leer.Leer())
                {
                    CargaDatosUnidadDeMedida();
                }
            }
        }

        private void txtUnidadMedidaSAT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.CFD_UnidadesDeMedida("txtUnidadMedidaSAT_KeyDown");
                if (leer.Leer())
                {
                    CargaDatosUnidadDeMedida();
                }
            }
        }

        private void txtUnidadMedidaSAT_TextChanged(object sender, EventArgs e)
        {
            lblUnidadMedidaSAT.Text = "";
        } 

        private void CargaDatosUnidadDeMedida()
        {
            txtUnidadMedidaSAT.Text = leer.Campo("IdUnidad");
            lblUnidadMedidaSAT.Text = leer.Campo("Descripcion");
        }

        private void txtProductoSAT_Validating(object sender, CancelEventArgs e)
        {
            if (txtProductoSAT.Text.Trim() != "")
            {
                leer.DataSetClase = query.CFDI_Productos_Servicios(txtProductoSAT.Text.Trim(), "txtUnidadMedidaSAT");
                if(leer.Leer())
                {
                    CargaDatosProductoSAT();
                }
                else
                {
                    txtProductoSAT.Text = "";
                }
            }
        }

        private void txtProductoSAT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.CFDI_Productos_Servicios("txtUnidadMedidaSAT_KeyDown");
                if (leer.Leer())
                {
                    CargaDatosProductoSAT();
                }
            }
        }

        private void txtProductoSAT_TextChanged(object sender, EventArgs e)
        {
            lblProductoSAT.Text = "";
        }

        private void CargaDatosProductoSAT()
        {
            txtProductoSAT.Text = leer.Campo("Clave");
            lblProductoSAT.Text = leer.Campo("Descripcion");
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("Exec spp_PRCS_OCEN__Plantilla_CargaDePrecios  @IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @Tipo = '{3}' ", 
                cboEstados.Data, txtCte.Text, txtSubCte.Text, 1);

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnExportarExcel_Click");
                General.msjError("Ocurrió un error al obtener la información para generar la plantilla de Carga de Cuadro Básico.");
            }
            else
            {
                Generar_Excel(); 
            }
        }

        private void Generar_Excel()
        {
            clsGenerarExcel excel = new clsGenerarExcel();
            string sNombreDocumento = string.Format("Plantilla_CuadroBasico___{0}", cboEstados.ItemActual.GetItem("Nombre"));
            string sNombreHoja = "CuadroBasico";
            string sConcepto = "";

            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;

            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
            //int iHoja = 1;
            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            excel = new clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;

            if(excel.PrepararPlantilla(sNombreDocumento))
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                
                iRenglon = 1;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }
        }
    }
}
