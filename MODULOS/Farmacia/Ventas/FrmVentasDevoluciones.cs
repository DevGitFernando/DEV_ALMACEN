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
using DllFarmaciaSoft.Devoluciones;
using Farmacia.Procesos;

namespace Farmacia.Ventas
{
    public partial class FrmVentasDevoluciones : FrmBaseExt
    {
        #region variables
        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5, CantDev = 6,
            Precio = 7, Importe = 8, ImporteIva = 9, ImporteTotal = 10, TipoCaptura = 11
        }

        DllFarmaciaSoft.Ventas.clsImprimirVentas VtasImprimir;
        clsDatosCliente DatosCliente;

        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leer2;
        clsLeer leer3;
        clsDevoluciones Dev;
        TipoDevolucion tpDevolucion = TipoDevolucion.Venta; 
        
        clsGrid myGrid;
        // Variables Globales  ****************************************************
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        // string sMensaje = "", 
        string sFolioVenta = "", sFolioVale = "";

        // bool bContinua = true;
        bool bGeneroVale = false;
        //bool bEmiteVales = true; //AQUI DEBE IR LA VARIABLE GLOBAL GnFarmacia.EmiteVales. 
        bool bEmiteVales = GnFarmacia.EmisionDeValesCompletos;  
        bool bValeRegistrado = false;
        double fSubTotal = 0, fIva = 0, fTotal = 0;
        int iAnchoColPrecio = 0;
        int iAnchoColDescripcion = 0;
        int iTipoDeVenta = 0;
       //***************************************************************************

        clsAyudas Ayuda = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        // bool bExisteMovto = false;
        // bool bEstaCancelado = false;
        // bool bMovtoAplicado = false;

        // string sFolioMovtoInv = "";
        // string sFormato = "#,###,###,##0.###0";
        // Cols ColActiva = Cols.Ninguna;
        // string sValorGrid = "";

        // string sIdTipoMovtoInv = "SV";
        // string sTipoES = "S";
        // string sIdProGrid = "";

        FrmIniciarSesionEnCaja Sesion;
        // bool bSesionIniciada = false;

        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);

        clsMotivosDevoluciones motivodev;


        #endregion variables

        public FrmVentasDevoluciones()
        {
            InitializeComponent();
            con.SetConnectionString();
            leer = new clsLeer(ref con);
            leer2 = new clsLeer(ref con);
            leer3 = new clsLeer(ref con);

            txtIdPersonal.Text = DtGeneral.IdPersonal;
            lblPersonal.Text = DtGeneral.NombrePersonal;

            //Para obtener Empresam Estado y Farmacia
            sEmpresa = DtGeneral.EmpresaConectada;
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;


            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloDeGrid = eModoGrid.ModoRow;

            if(!DtGeneral.EsAdministrador)
            {
                if(!GnFarmacia.MostrarPrecios_y_Costos)
                {
                    // Reestablecer el Grid 
                    grdProductos.Sheets[0].Columns[(int)Cols.Precio - 1].Visible = false;
                    grdProductos.Sheets[0].Columns[(int)Cols.Importe - 1].Visible = false;
                    ////grdProductos.Sheets[0].Columns[(int)Cols.Descripcion - 1].Width = iAncho;
                }
            }

            myGrid.AjustarAnchoColumnasAutomatico = true;



            cboTipoVenta.Clear();
            cboTipoVenta.Add("0", "<< Seleccione >>");
            cboTipoVenta.Add("1", "Contado");
            cboTipoVenta.Add("2", "Credito");
            cboTipoVenta.SelectedIndex = 0;

            // Manejo de columnas 
            iAnchoColPrecio = (int)grdProductos.Sheets[0].Columns[(int)Cols.Precio - 1].Width;
            iAnchoColPrecio += (int)grdProductos.Sheets[0].Columns[(int)Cols.Importe - 1].Width;
            iAnchoColDescripcion = (int)grdProductos.Sheets[0].Columns[(int)Cols.Descripcion - 1].Width;

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            VtasImprimir = new DllFarmaciaSoft.Ventas.clsImprimirVentas(General.DatosConexion, DatosCliente,
                sEmpresa, sEstado, sFarmacia, General.Url, GnFarmacia.RutaReportes, TipoReporteVenta.Contado);

            motivodev = new clsMotivosDevoluciones(General.DatosConexion, TipoDevolucion.Venta, sEmpresa, sEstado, sFarmacia);
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            InicializarPantalla();

            tmSesion.Enabled = true;
            tmSesion.Start();
        }

        private void tmSesion_Tick(object sender, EventArgs e)
        {
            tmSesion.Enabled = false;
            Sesion = new FrmIniciarSesionEnCaja();
            Sesion.VerificarSesion();

            GnFarmacia.ValidarSesionUsuario = true; 
            if (!Sesion.AbrirVenta)
            {
                this.Close();
            }
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

        #region Botones 
        private void InicializarPantalla()
        {
            Fg.IniciaControles(this, true);
            grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            myGrid.Limpiar(true);
            dtpFechaRegistro.Enabled = false;
            bGeneroVale = false;
            bValeRegistrado = false;
            sFolioVale = "";

            txtIdPersonal.Enabled = false; // Debe estar inhabilitado todo el tiempo 

            CambiaEstado(true);
            fSubTotal = 0;
            fIva = 0;
            fTotal = 0;
            txtCte.Enabled = false;
            cboTipoVenta.Enabled = false;

            txtIdPersonal.Text = DtGeneral.IdPersonal;
            lblPersonal.Text = DtGeneral.NombrePersonal;

            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;
            Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento);

            btnMotivosDev.Enabled = false;
            motivodev = new clsMotivosDevoluciones(General.DatosConexion, TipoDevolucion.Venta, sEmpresa, sEstado, sFarmacia);

            // Reestablecer el Grid 
            grdProductos.Sheets[0].Columns[(int)Cols.Precio - 1].Visible = true;
            grdProductos.Sheets[0].Columns[(int)Cols.Importe - 1].Visible = true;
            grdProductos.Sheets[0].Columns[(int)Cols.Descripcion - 1].Width = iAnchoColDescripcion;

            InicializaToolBar(true, false, false, false);
            btnEjecutar.Enabled = false;
            txtFolio.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // bContinua = true;
            //if (txtFolio.Text != "*")
            //{
            //    General.msjUser("Este Folio ya ha sido guardado por lo tanto no puede ser modificado");
            //}
            //else
            {
                
                if (ValidaDatos())
                {
                    Dev = new clsDevoluciones(sEmpresa, sEstado, sFarmacia, con.DatosConexion);

                    // Agregar los datos 
                    Dev.Folio = "*";
                    //Dev.FolioCompra = txtFolio.Text;
                    Dev.Tipo = tpDevolucion;
                    Dev.Referencia = txtFolio.Text;
                    Dev.FechaOperacionDeSistema = GnFarmacia.FechaOperacionSistema;
                    Dev.IdPersonal = DtGeneral.IdPersonal;
                    Dev.Observaciones = txtObservaciones.Text;
                    Dev.SubTotal = myGrid.TotalizarColumnaDou((int)Cols.Importe);
                    Dev.Iva = myGrid.TotalizarColumnaDou((int)Cols.ImporteIva);
                    Dev.Total = myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal);
                    Dev.NombreOperacion = "DEVOLUCION_DE_VENTAS";
                    Dev.MsjSinPermiso = "Usuario sin permiso para realizar devolución, Favor de verificar."; 

                    // Agregar los Productos 
                    for (int i = 1; i <= myGrid.Rows; i++)
                    {
                        clsProducto P = new clsProducto();
                        P.IdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                        P.CodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                        P.Unidad = 1;
                        P.TasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                        P.Cantidad = myGrid.GetValueDou(i, (int)Cols.CantDev);
                        P.Valor = myGrid.GetValueDou(i, (int)Cols.Precio);
                        Dev.AddProducto(P);
                    }

                    // Agregar los Lotes 
                    Dev.Lotes = Lotes;

                    Dev.MotivosDev = motivodev.Retorno;
                    // Se agrega el Vale.
                    Dev.Vale = sFolioVale;
                    if (Dev.Guardar())
                    {
                        InicializaToolBar(true, false, false, false);
                        Dev.ImprimirReubicacion(1);
                        Dev.ImprimirReubicacion(2);
                        ImprimirInformacion();                        
                        // btnNuevo_Click(null, null);
                    }
                }
            }
        }

        private void CalcularTotales()
        {            
            double sSubTotal = 0, sIva = 0, sTotal = 0;

            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                sSubTotal = myGrid.GetValueDou(i, 7);
                fSubTotal = fSubTotal + sSubTotal;
                sIva = myGrid.GetValueDou(i, 8);
                fIva = fIva + sIva;
                sTotal = myGrid.GetValueDou(i, 9);
                fTotal = fTotal + sTotal;
            }               
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion();
        }

        private void ImprimirInformacion()
        {
            // cboTipoVenta.Add("1", "Contado");
            // cboTipoVenta.Add("2", "Credito");
            double dTotal = 0; 

            leer.DataSetClase = Consultas.FolioEnc_VentasDev(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text.Trim(), 8), "CargaDetallesVenta");
            if (leer.Leer())
            {
                dTotal = leer.CampoDouble("Total");
            }

            // VtasImprimir.MostrarVistaPrevia = true; 
            VtasImprimir.TipoDeReporte = (TipoReporteVenta)Convert.ToInt32(cboTipoVenta.Data);
            if (VtasImprimir.Imprimir(txtFolio.Text, dTotal))
            {                
                btnNuevo_Click(null, null);

            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            FrmDevolucionesImpresion Devoluciones;
            Devoluciones = new FrmDevolucionesImpresion();
            Devoluciones.MostrarPantalla(txtFolio.Text.Trim(), tpDevolucion, iTipoDeVenta);
        }

        #endregion Botones

        #region Validaciones
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            // string sSql = "", 
            string sFolio = "";
            int iAncho = iAnchoColDescripcion;
            bool bMostrar = true;

            //////// Reestablecer el Grid 
            ////grdProductos.Sheets[0].Columns[(int)Cols.Precio - 1].Visible = bMostrar;
            ////grdProductos.Sheets[0].Columns[(int)Cols.Importe - 1].Visible = bMostrar;
            ////grdProductos.Sheets[0].Columns[(int)Cols.Descripcion - 1].Width = iAncho; 

            if (txtFolio.Text.Trim() == "" || txtFolio.Text == "*")
            {
                txtFolio.Text = "";
                txtFolio.Focus();
            }
            else
            {
                //sSql = string.Format("SELECT * FROM VentasEnc (nolock) WHERE FolioVenta= '{0}' AND IdEstado='{1}' AND IdFarmacia='{2}'  ", Fg.PonCeros(txtFolio.Text, 8), Fg.PonCeros(sEstado,2), Fg.PonCeros(sFarmacia,4)); 
                leer.DataSetClase = Consultas.FolioEnc_VentasDev(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text.Trim(), 8), "CargaDetallesVenta");                
                if (leer.Leer())
                {
                    sFolio = leer.Campo("Folio");
                    sFolioVenta = sFolio;

                    txtFolio.Text = sFolio;
                    txtCte.Text = leer.Campo("IdCliente");
                    lblCte.Text = leer.Campo("NombreCliente");
                    cboTipoVenta.Data = leer.CampoInt("TipoDeVenta").ToString();
                    iTipoDeVenta = leer.CampoInt("TipoDeVenta");

                    btnMotivosDev.Enabled = true;

                    if (leer.Campo("Status").ToUpper() == "C")
                    {
                        lblCancelado.Visible = true;
                    }

                    if (leer.Campo("Status").ToUpper() == "D")
                    {
                        btnEjecutar.Enabled = true;
                        General.msjUser("Folio con devolución parcial.");
                    }                   

                    InicializaToolBar(true, true, false, false);
                    CargaDetallesVenta();
                    CambiaEstado(false); 

                    // Determinar si se Muestran las Columas de Precios 
                    if (cboTipoVenta.Data == "2" && !GnFarmacia.MostrarPrecios_y_Costos)
                    {
                        bMostrar = false;
                        iAncho = iAnchoColDescripcion + iAnchoColPrecio;
                    }

                    // 2K110813.1153 Habilitar el cierre de periodos 
                    // 2K111214.1900 Habilitar el cierre de periodos 
                    if (leer.CampoInt("FolioCierre") != 0)
                    {
                        InicializaToolBar(true, false, false, false);
                        General.msjAviso("Folio bloqueado para devolución.");
                    }
                    else
                    {
                        // if (bEmiteVales) 
                        {
                            if (BuscarVale())
                            {
                                if (bValeRegistrado)
                                {
                                    InicializaToolBar(true, false, false, false);
                                    General.msjAviso("Folio con entrega de vale. Bloqueado para devolución");
                                }
                                else
                                {
                                    General.msjAviso("Folio con asignación de vale, Realizar devolución total!");
                                }
                            }
                        }
                    }
                }
                else
                {
                    txtFolio.Text = "";
                    txtFolio.Focus();
                }
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (GnFarmacia.UsuarioConSesionCerrada(false))
            {
                bRegresa = false; 
                Application.Exit();
            }

            if (bRegresa && txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio no encontrado, Favor de verificar.");
                txtFolio.Focus();
            }

            if (bRegresa && txtObservaciones.Text == "")
            {
                bRegresa = false;
                General.msjUser("Capturar observaciones, Favor de verificar.");
                txtObservaciones.Focus();
            }

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            if (bRegresa && !motivodev.Marco)
            {
                bRegresa = false;
                General.msjUser("Capture los motivos de la devolución, Favor de verificar.");
            }

            return bRegresa;
        }

        private bool validarCapturaProductos()
        {
            bool bRegresa = true;
            bool bErrorVale = false;

            if (myGrid.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                if (myGrid.TotalizarColumna((int)Cols.CantDev) == 0)
                {
                    bRegresa = false;
                }

                if (bRegresa)
                {
                    if (bEmiteVales)
                    {
                        if (bGeneroVale)//Si tiene vale, se deberan capturar todos los productos.
                        {
                            bErrorVale = true;
                            if (Lotes.CantidadTotal == 0)
                            {
                                bRegresa = false;
                            }
                            else
                            {
                                for (int i = 1; i <= myGrid.Rows; i++)
                                {
                                    if (myGrid.GetValueInt(i, (int)Cols.CantDev) != myGrid.GetValueInt(i, (int)Cols.Cantidad))
                                    {
                                        bRegresa = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                }
            }

            if (!bRegresa)
            {
                if (bErrorVale)
                {
                    General.msjUser("Folio con un vale, Realizar devolución completa.");
                }
                else
                {
                    General.msjUser("No se han modificado los datos para la devolución, Favor de verificar.");
                }
            }

            return bRegresa;

        }

        private bool CargaDetallesVenta()
        {
            bool bRegresa = true;

            leer2.DataSetClase = Consultas.FolioDet_VentasDev(sEmpresa, sEstado, sFarmacia, sFolioVenta, "CargaDetallesVenta"); 
            if (leer2.Leer())
            {
                myGrid.LlenarGrid(leer2.DataSetClase, false, false);
            }
            else
            {
                bRegresa = false;
            }

            CargarDetallesLotesVenta();
            return bRegresa;
        }

        private void CargarDetallesLotesVenta()
        {
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = Consultas.FolioDetLotes_VentasDev(sEmpresa, sEstado, sFarmacia, sFolioVenta, "CargarDetallesLotesVenta()");
            Lotes.AddLotes(leer.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                leer.DataSetClase = Consultas.FolioDetLotes_VentasDev_Ubicaciones(sEmpresa, sEstado, sFarmacia, sFolioVenta, "CargarDetallesLotesVenta");
                Lotes.AddLotesUbicaciones(leer.DataSetClase);
            }
        }
        #endregion Validaciones

        #region Funciones
        private bool LlenaCliente()
        {
            bool bRegresa = false;
            string sSql = "";
            leer2 = new clsLeer(ref con);

            sSql = string.Format(" SELECT * FROM CatClientes (nolock) WHERE IdCliente='{0}' ", Fg.PonCeros(txtCte.Text, 4)); 
            if (!leer2.Exec(sSql))
            {
                Error.GrabarError(leer2, "LlenaCliente()");
                General.msjError("Error en consulta de Cliente.");
            }
            else
            {
                if (leer2.Leer())
                {
                    bRegresa = true;
                    txtCte.Text = leer2.Campo("IdCliente");
                    lblCte.Text = leer2.Campo("Nombre");
                }
            }

            return bRegresa;
        }

        private void CambiaEstado(bool bValor)
        {
            txtFolio.Enabled = bValor;
            txtCte.Enabled = bValor;
        }
        
        private void ObtieneClaveLote(string sIdProducto, string sCodigoEAN, ref string sClaveLote )
        {            
            string sSql = "";
            leer3 = new clsLeer(ref con);

            sSql = string.Format(" SELECT TOP 1 ClaveLote FROM FarmaciaProductos_CodigoEAN_Lotes (nolock) " +
		                           " WHERE CodigoEAN = '{0}' AND IdProducto = '{1}'  ", sCodigoEAN, Fg.PonCeros(sIdProducto, 8) );

            if (!leer3.Exec(sSql))
            {
                Error.GrabarError(leer3, "ObtieneClaveLote()");
                General.msjError("Error en consulta de Lote.");
            }
            else
            {
                if (leer3.Leer())
                {
                    sClaveLote = leer3.Campo("ClaveLote");
                }
            }            
        }

        private void InicializaToolBar(bool bNuevo, bool bGuardar, bool bCancelar, bool bImprimir)
        {
            if (GnFarmacia.ManejaUbicaciones)
            {
                if (GnFarmacia.ManejaUbicacionesEstandar)
                {
                    if (!DtGeneral.CFG_UbicacionesEstandar)
                    {
                        bGuardar = false;
                    }
                }
            }

            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
            btnImprimir.Enabled = bImprimir;
            
        }

        private bool BuscarVale()
        {
            bool bRegresa = false;
            clsLeer leerVale = new clsLeer(); 

            bGeneroVale = false;
            if (txtFolio.Text.Trim() != "" || txtFolio.Text.Trim() != "*")
            {
                Consultas.MostrarMsjSiLeerVacio = false;
                leer.DataSetClase = Consultas.Ventas_ObtenerVale(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), "BuscarVale");
                if (leer.Leer())
                {
                    sFolioVale = leer.Campo("Folio");

                    leerVale.DataRowsClase = leer.DataTableClase.Select(" Status = 'R' ");
                    if (leerVale.Leer())
                    {
                        bGeneroVale = true;
                        bRegresa = true;
                        bValeRegistrado = true;
                    }
                    else
                    {
                        leerVale.DataRowsClase = leer.DataTableClase.Select(" Status = 'C' ");
                        if (leerVale.Leer())
                        {
                            bGeneroVale = false;
                            bRegresa = false;
                            bValeRegistrado = false;
                        } 
                    }
                }
            }

            Consultas.MostrarMsjSiLeerVacio = true;

            return bRegresa;
        }
        #endregion Funciones             

        #region Eventos
        #endregion Eventos    

        #region Grid
        #endregion Grid

        #region Manejo de lotes
        private void CargarLotesCodigoEAN()
        {
            int iRow = myGrid.ActiveRow;
            string sCodigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
            string sCodEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);

            leer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Todos, false, "CargarLotesCodigoEAN()");
            if (Consultas.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                leer.Leer();
                Lotes.AddLotes(leer.DataSetClase);

                if (GnFarmacia.ManejaUbicaciones)
                {
                    leer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, false, "CargarLotesCodigoEAN()");
                    if (Consultas.Ejecuto)
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
            //int iRow = myGrid.ActiveRow;
            //Lotes.RemoveLotes(myGrid.GetValue(iRow, (int)Cols.Codigo), myGrid.GetValue(iRow, (int)Cols.CodEAN));
            //myGrid.DeleteRow(iRow);
           

            //if (myGrid.Rows == 0)
            //    myGrid.Limpiar(true);

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
                    Lotes.EsEntrada = false;// para las ventas
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = false;
                    Lotes.ModificarCantidades = true;

                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.EsDevolucionDeVentas;

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;
                    Lotes.Show();

                    myGrid.SetValue(iRow, (int)Cols.CantDev, Lotes.Cantidad);
                    myGrid.SetValue(iRow, (int)Cols.TipoCaptura, Lotes.TipoCaptura);
                    myGrid.SetActiveCell(iRow, (int)Cols.Precio);                     
                }
                else
                {
                    myGrid.SetActiveCell(iRow, (int)Cols.CodEAN);
                }
            }
        }
        #endregion Manejo de lotes

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnMotivosDev_Click(object sender, EventArgs e)
        {
            motivodev.MotivosDevolucion();
        }

    }
}
