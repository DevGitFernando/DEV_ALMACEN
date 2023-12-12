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
using SC_SolutionsSystem.Errores;
//using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FP;
using SC_SolutionsSystem.FP.Huellas;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

using DllCompras;


namespace DllCompras.ListasDePrecioClaves
{
    public partial class FrmCodigosEAN_ListaPrecios : FrmBaseExt 
    {
        private enum Cols
        {
            ClaveSSA = 1, Desc_Sal = 2, CodigoEAN = 3, IdClaveSSA = 4, Status = 5, Descripcion = 6, Precio = 7, 
            Descuento = 8, TasaIva = 9, Iva = 10, PrecioUnitario = 11, FechaRegistro = 12, FechaVigencia = 13
        }

        private clsConexionSQL myCnn = new clsConexionSQL(General.DatosConexion);
        private clsConexionSQL CnnHuellas = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
        DllFarmaciaSoft.wsHuellas.wsHuellas validarHuella = null;
        private clsGrabarError myError = new clsGrabarError();
        private clsAyudas myAyuda;
        private clsConsultas myQuery;
        private clsLeer myLeer, myLeerSancion;
        clsLeer leer, leerChecador;
        private clsGrid grid;
        private DateTime dtFechaServer = DateTime.Now;
        private DateTime dtFechaMin = DateTime.Now;        
        private bool bCargoProveedor = false;
        string sFormato = "#,###,###,##0.###0";
        string sUrlChecador = "";
        string sHost = "";
        bool bProveedorSancionado = false;
        bool bPrecioGuardado = false;

        clsDatosCliente datosCliente;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdPersonalConectado = DtGeneral.IdPersonal;
        string sMsjNoEncontrado = "";
        ///clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        string sAutorizar_Modificacion_Precios = "MODIFICACION_DE_PRECIOS_CENTRAL";
        string sExportar_Listado_Precios = "EXPORTAR_LISTA_PRECIOS_EXCEL";

        string sRutaPlantilla = "";
        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        // Clase de Auditoria de Movimientos
        clsAuditoria auditoria;

        public FrmCodigosEAN_ListaPrecios()
        {
            InitializeComponent();

            datosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, ""); 

            myCnn = new clsConexionSQL(General.DatosConexion);
            myLeer = new clsLeer(ref myCnn);
            myLeerSancion = new clsLeer(ref myCnn);
            leer = new clsLeer(ref ConexionLocal);
            leerChecador = new clsLeer(ref CnnHuellas);
            validarHuella = new DllFarmaciaSoft.wsHuellas.wsHuellas();
            validarHuella.Url = General.Url;
            leerExportarExcel = new clsLeer(ref myCnn);
            myAyuda = new clsAyudas(General.DatosConexion, GnCompras.Modulo, this.Name, GnCompras.Version);
            myQuery = new clsConsultas(General.DatosConexion, "DllCompras", this.Name, Application.ProductVersion);
            grid = new clsGrid(ref grdListaDePrecios, this);
            grid.EstiloGrid(eModoGrid.ModoRow);

            grid.SetOrder((int)Cols.ClaveSSA, 1, true);
            grid.SetOrder((int)Cols.CodigoEAN, 1, true);
            grid.SetOrder((int)Cols.Status, 1, true);
            grid.SetOrder((int)Cols.Descripcion, 1, true);
            grid.SetOrder((int)Cols.Precio, 1, true);

            grid.ResizeColumns = true;

            dtFechaMin = Convert.ToDateTime("2010-05-01");

            // Clase de Movimientos de Auditoria
            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                            DtGeneral.IdPersonal, DtGeneral.IdSesion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
        }

        private void FrmListaPreciosClaveSSA_Load(object sender, EventArgs e)
        {
            //Limpiar();

            /////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref myCnn, sEmpresa, sEstado, sFarmacia, sIdPersonalConectado);
            btnNuevo_Click(null, null);
        }

        #region Teclas Rápidas
        ////protected override void OnKeyDown(KeyEventArgs e)
        ////{
        ////    if (e.Control)
        ////    {
        ////        TeclasRapidas(e);
        ////    }
        ////}

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.N:
                    if (btnNuevo.Enabled)
                    {
                        btnNuevo_Click(null, null);
                    }
                    break;
                case Keys.G:
                    if (btnGuardar.Enabled)
                    {
                        btnGuardar_Click(null, null);
                    }
                    break;
                case Keys.C:
                    if (btnCancelar.Enabled)
                    {
                        btnCancelar_Click(null, null);
                    }
                    break;
                case Keys.E:
                    if (btnEjecutar.Enabled)
                    {
                        btnEjecutar_Click(null, null);
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
        #endregion

        #region Botones
        private void Limpiar()
        {           
            Fg.IniciaControles(this, true);
            grid.Limpiar(false);
            InicializaToolBar(true, false, false, false, false);
            dtpFechaRegistro.Enabled = false;
            bCargoProveedor = false;
            BloqueaPrecios();
            nudTasaIva.Value = 0M;
            //btnGuardar.Enabled = true;
            btnExportarExcel.Enabled = false;
            txtIdProveedor.Focus();

            if (DtGeneral.PermisosEspeciales.TienePermiso(sAutorizar_Modificacion_Precios))
            {
                PermisosPreciosControles(true);
            }
            else
            {
                PermisosPreciosControles(false);
            }
           
        }

        private void PermisosPreciosControles(bool Valor)
        {
            btnGuardar.Enabled = Valor;
            btnCancelar.Enabled = Valor;

            txtPrecio.Enabled = Valor;
            txtDescuento.Enabled = Valor;
            nudTasaIva.Enabled = Valor;
        }

        private void ActualizarDatosVigencias()
        {                 
            dtpFechaRegistro.Enabled = false;
            dtpFechaRegistro.Value = General.FechaSistema;
            //dtpFechaVigencia.MinDate = General.FechaSistema.AddDays(15);            
        }

        private void BloqueaPrecios()
        {           
            txtIva.Enabled = false;
            txtPUnitario.Enabled = false; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                ////if (bPrecioGuardado)
                {                 
                    GuardarInformacion(1);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                GuardarInformacion(2);
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            //txtIdProveedor_Validating(null, null);

            if (bCargoProveedor)
            {
                CargarListaPrecios();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;

            if (grid.Rows > 0)
            {
                datosCliente.Funcion = "Imprimir"; 
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnCompras.RutaReportes;
                myRpt.NombreReporte = "COM_OCEN_ListaDePrecios_Claves_Proveedor.rpt";
                myRpt.NombreReporte = "COM_OCEN_ListadoClaves_Proveedor.rpt"; 

                myRpt.Add("IdProveedor", txtIdProveedor.Text);                            

                bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, datosCliente); 

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

        #region Funciones y Procedimientos Privados 
        private bool validarDatos()
        {
            bool bRegresa = true;

            if (txtIdProveedor.Text == "")
            {
                General.msjAviso("No ha capturado un Proveedor válido, verfique.");
                txtIdProveedor.Focus();
                bRegresa = false;
                return bRegresa;
            } 

            if (txtCodigoEAN.Text == "")
            {
                General.msjAviso("No ha capturado un CodigoEAN válido, verifique.");
                txtCodigoEAN.Focus();
                bRegresa = false;
                return bRegresa;
            }

            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "El usuario no tienen permisos para cambiar precios de compra, verifique por favor.";
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("CAMBIAR_PRECIOS_COMP", sMsjNoEncontrado);
            }

            return bRegresa;
        }

        private void GuardarInformacion(int iOpcion)
        {
            string Precio = "", PrecioUnitario = "", Iva = "", sCadena = "";
            string sSql = "";

            sCadena = txtPrecio.Text.Replace(",", "");
            Precio = sCadena;
            sCadena = txtPUnitario.Text.Replace(",", "");
            PrecioUnitario = sCadena;
            sCadena = txtIva.Text.Replace(",", "");
            Iva = sCadena;

            if (!myCnn.Abrir())
            {
                General.msjAviso(General.MsjErrorAbrirConexion);
            }
            else 
            {
                myCnn.IniciarTransaccion();

                sSql = string.Format("Set Dateformat YMD \nExec spp_Mtto_COM_OCEN_ListaDePrecios \n" +
                    "\t@IdProveedor = '{0}', @IdClaveSSA = '{1}', @CodigoEAN = '{2}', \n" +
                    "\t@Precio = '{3}', @Descuento = '{4}', @TasaIva = '{5}', @Iva = '{6}', @PrecioUnitario = '{7}', \n" +
                    "\t@FechaRegistro = '{8}', @FechaFinVigencia = '{9}', @iOpcion = '{10}' \n",
                    txtIdProveedor.Text, txtIdClaveSSA.Text, txtCodigoEAN.Text, Precio, txtDescuento.Text, nudTasaIva.Value, Iva,
                    PrecioUnitario, General.FechaYMD(dtpFechaRegistro.Value), General.FechaYMD(dtpFechaVigencia.Value), iOpcion);

                if (!myLeer.Exec(sSql))
                {
                    myCnn.DeshacerTransaccion();
                    myError.GrabarError("Ocurrió un error al guardar la información.", "GuardarInformacion()");
                    General.msjError("Ocurrió un error al guardar la información del Producto.");
                }
                else 
                {
                    myCnn.CompletarTransaccion();

                    if (myLeer.Leer())
                    {
                        General.msjUser(myLeer.Campo("Mensaje"));
                        Limpiar();
                    }
                }               

                myCnn.Cerrar();
            }
        }

        private void CalcularPrecio()
        {
            double iDescto = 0;
            double iPrecio = 0;
            double iIva = 0, iTotal = 0;

            try
            {
                iDescto = Convert.ToDouble(txtDescuento.Text) / 100;
            }
            catch
            {
                iDescto = 0;
            }
            try
            {
                iPrecio = Convert.ToDouble(txtPrecio.Text);
            }
            catch
            {
                iPrecio = 0;
            }

            iPrecio = iPrecio - (iPrecio * iDescto);
            iIva = (Convert.ToDouble(nudTasaIva.Value) / 100) * iPrecio;
            iTotal = iPrecio + iIva;

            txtIva.Text = iIva.ToString();
            txtPUnitario.Text = iTotal.ToString(); 
        }

        private void BuscarDatosPrecio()
        {
            double dPrecioPza = 0, dPrecioUniPza = 0;

            // bool bPuedeModificar = true;
            string sSql = string.Format(" Select *, Case When FechaFinVigencia < GetDate() Then '1' Else '0' End As EsVigente " +
                                        "  From COM_OCEN_ListaDePrecios (Nolock) Where IdProveedor = '{0}' And CodigoEAN = '{1}' ", 
                                        txtIdProveedor.Text, txtCodigoEAN.Text);

            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    txtPrecio.Text = myLeer.CampoDouble("Precio").ToString();
                    txtDescuento.Text = myLeer.CampoDouble("Descuento").ToString();
                    nudTasaIva.Value = myLeer.CampoDec("TasaIva");                   
                    txtIva.Text = myLeer.CampoDouble("Iva").ToString();
                    txtPUnitario.Text = myLeer.CampoDouble("PrecioUnitario").ToString();
                    dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro"); 
                    dtpFechaVigencia.Value = myLeer.CampoFecha("FechaFinVigencia");

                    dPrecioPza = myLeer.CampoDouble("Precio") / (Convert.ToInt32(lblContPaquete.Text));
                    dPrecioUniPza = myLeer.CampoDouble("PrecioUnitario") / (Convert.ToInt32(lblContPaquete.Text));

                    lblPrecioPza.Text = dPrecioPza.ToString(sFormato);
                    lblPrecioUniPza.Text = dPrecioUniPza.ToString(sFormato);


                    if (DtGeneral.PermisosEspeciales.TienePermiso(sAutorizar_Modificacion_Precios))
                    {
                        InicializaToolBar(true, true, true, true, false);
                    }
                    bPrecioGuardado = true;

                    //bPuedeModificar = myLeer.CampoBool("EsVigente");

                    //if (!bPuedeModificar)
                    //{
                    //    BloquearDatosCodigoEAN();
                    //}
                }
                else
                {
                    //Significa que es la primera vez que se captura el codigo.
                    PermisosPreciosControles(true);
                    InicializaToolBar(true, true, false, true, false);
                }
            }
        }

        private void BloquearDatosCodigoEAN()
        {
            InicializaToolBar(true, false, false, true, false);
            txtPrecio.Enabled = false;
            txtDescuento.Enabled = false;
            nudTasaIva.Enabled = false;
            txtIva.Enabled = false;
            txtPUnitario.Enabled = false;
            dtpFechaRegistro.Enabled = false;
            dtpFechaVigencia.Enabled = false;
        }

        private void CargarListaPrecios()
        {
            string sSql = string.Format(" Select P.ClaveSSA, P.DescripcionSal, L.CodigoEAN, L.IdClaveSSA, Case When L.Status = 'A' Then 'ACTIVO' Else 'CANCELADO' End As Status, " +
	                            " P.Descripcion, L.Precio, L.Descuento, L.TasaIva, L.Iva, L.PrecioUnitario, " + 
	                            " Convert(varchar(10), FechaRegistro, 120) As FechaRegistro,  " +
                                " Convert(varchar(10), FechaFinVigencia, 120) As FechaFinVigencia  " +
	                            " From COM_OCEN_ListaDePrecios L (Nolock) " +
                                " Inner Join vw_Productos_CodigoEAN P (NoLock) On( L.IdClaveSSA = P.IdClaveSSA_Sal And L.CodigoEAN = P.CodigoEAN ) " +
                                " Where L.IdProveedor = '{0}'  Order By P.Descripcion ", txtIdProveedor.Text);

            grid.Limpiar(false); 

            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    grid.LlenarGrid(myLeer.DataSetClase);
                    grid.BloqueaGrid(true);

                    if (DtGeneral.PermisosEspeciales.TienePermiso(sExportar_Listado_Precios))
                    {
                        leerExportarExcel.DataSetClase = myLeer.DataSetClase;
                        btnExportarExcel.Enabled = true;
                    }
                }

                if (!bProveedorSancionado)
                {
                    if (DtGeneral.PermisosEspeciales.TienePermiso(sAutorizar_Modificacion_Precios))
                    {
                        InicializaToolBar(true, true, true, true, true);
                    }
                }
                else
                {
                    InicializaToolBar(true, false, false, true, true);
                }
            }
        }

        private void InicializaToolBar(bool bNuevo, bool bGuardar, bool bCancelar, bool bEjecutar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
            btnEjecutar.Enabled = bEjecutar;
            btnImprimir.Enabled = bImprimir;
        }

        private bool ProveedorSancionado()
        {
            bool bRegresa = true;

            myQuery.MostrarMsjSiLeerVacio = false;
            myLeerSancion.DataSetClase = myQuery.Proveedores_Sancionados(txtIdProveedor.Text.Trim(), "ProveedorSancionado");
            myQuery.MostrarMsjSiLeerVacio = true;

            if (!myLeerSancion.Leer())
            {
                bRegresa = false;
            }
            else
            {
                bProveedorSancionado = true;
                General.msjUser("El proveedor ingresado se encuentra sancionado. Verifique");
            }

            return bRegresa;
        }

        #endregion Funciones y Procedimientos Privados        

        #region Eventos
        private void txtPrecio_Validating(object sender, CancelEventArgs e)
        {
            CalcularPrecio();
        }

        private void txtDescuento_Validating(object sender, CancelEventArgs e)
        {
            CalcularPrecio(); 
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            CalcularPrecio();
            //ActualizarDatosVigencias(); 
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            CalcularPrecio();
            //ActualizarDatosVigencias();
        }

        private void dtpFechaVigencia_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dtpFechaVigencia_Validating(object sender, CancelEventArgs e)
        {
            dtpFechaRegistro.Enabled = false;            
        }

        private void grdListaDePrecios_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            txtCodigoEAN.Text = "";

            txtCodigoEAN.Text = grid.GetValue(grid.ActiveRow, (int)Cols.CodigoEAN);
            txtIdClaveSSA.Text = grid.GetValue(grid.ActiveRow, (int)Cols.IdClaveSSA);
            lblDescripcion.Text = grid.GetValue(grid.ActiveRow, (int)Cols.Descripcion);
            
            txtCodigoEAN_Validating(null, null);

            if (grid.GetValueObj(grid.ActiveRow, (int)Cols.Status).ToString() == "CANCELADO")
            {
                btnCancelar.Enabled = false;
            }
        }

        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdProveedor.Text != "")
            {
                myQuery.MostrarMsjSiLeerVacio = false;

                myLeer.DataSetClase = myQuery.Proveedores(Fg.PonCeros(txtIdProveedor.Text, 4), "txtIdProveedor_Validating");

                if (myLeer.Leer())
                {
                    txtIdProveedor.Text = myLeer.Campo("IdProveedor");
                    txtIdProveedor.Enabled = false;
                    bCargoProveedor = true;
                    bPrecioGuardado = false;

                    if (ProveedorSancionado())
                    {
                        lblNombreProveedor.Text = myLeer.Campo("Nombre") + " - SANCIONADO";
                    }
                    else
                    {
                        lblNombreProveedor.Text = myLeer.Campo("Nombre");
                    }

                    InicializaToolBar(true, false, false, true, false);
                    txtCodigoEAN.Focus();
                    btnImprimir.Enabled = true;
                }
                
            }
            else
            {
                Fg.IniciaControles(this, true, FrameDatosProveedor);
            }
        }

        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            clsLeer myLeerProveedores = new clsLeer(ref myCnn);

            if (e.KeyCode == Keys.F1)
            {
                myLeerProveedores.DataSetClase = myAyuda.Proveedores("txtIdProveedor_KeyDown()");

                if (myLeerProveedores.Leer())
                {
                    txtIdProveedor.Text = myLeerProveedores.Campo("IdProveedor");
                    lblNombreProveedor.Text = myLeerProveedores.Campo("Nombre");
                    txtIdProveedor.Enabled = false;
                }
            }
        }

        private void nudTasaIva_ValueChanged(object sender, EventArgs e)
        {
            CalcularPrecio();
        }

        private void txtCodClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            clsLeer myLeerClavesSSA = new clsLeer(ref myCnn);

            if (e.KeyCode == Keys.F1)
            {
                myLeerClavesSSA.DataSetClase = myAyuda.Productos_CodigoEAN("txtCodClaveSSA_KeyDown()");

                if (myLeerClavesSSA.Leer())
                {
                    txtCodigoEAN.Text = myLeerClavesSSA.Campo("CodigoEAN");
                    lblDescripcion.Text = myLeerClavesSSA.Campo("Producto");

                    //lblClaveSSA.Text = myLeer.Campo("ClaveSSA");
                    //lblPresentacion.Text = myLeer.Campo("Presentacion");
                    //lblContPaquete.Text = myLeer.Campo("ContenidoPaquete");

                    txtCodigoEAN.Enabled = false;
                }
            }
        }

        private void txtCodigoEAN_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodigoEAN.Text != "")
            {
                myQuery.MostrarMsjSiLeerVacio = false;

                myLeer.DataSetClase = myQuery.Productos_CodigosEAN_Datos(txtCodigoEAN.Text, "txtCodClaveSSA_Validating");

                if (myLeer.Leer())
                {
                    txtCodigoEAN.Text = myLeer.Campo("CodigoEAN");
                    lblDescripcion.Text = myLeer.Campo("Descripcion");
                    txtIdClaveSSA.Text = myLeer.Campo("IdClaveSSA_Sal");
                    lblClaveSSA.Text = myLeer.Campo("ClaveSSA");
                    lblPresentacion.Text = myLeer.Campo("Presentacion");
                    lblTipoInsumo.Text = myLeer.Campo("TipoDeProducto");
                    lblContPaquete.Text = myLeer.Campo("ContenidoPaquete");
                    BuscarDatosPrecio();
                }
            }
            //else
            //{
            //    Fg.IniciaControles(this, true, FrameDatosClave);
            //    //ActualizarDatosVigencias();                
            //}
            //BloqueaPrecios();
        }

        #endregion Eventos            

        #region Reportes
        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string sCadena = "";

            sCadena = myLeer.QueryEjecutado;
            sCadena = sCadena.Replace("'", "\"");

            tsBarraMenu.Enabled = false;
            GenerarReporteExcel();
            auditoria.GuardarAud_MovtosUni("*", sCadena);
            tsBarraMenu.Enabled = true;
        }

        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = "LISTA DE PRECIOS DE PRODUCTOS DE " + lblNombreProveedor.Text; 
            string sNombreFile = "Listado_Precios_Proveedor" + "_" + lblNombreProveedor.Text;

            leer.DataSetClase = leerExportarExcel.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombreFile))
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.EstadoConectadoNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }

        //private void ExportarListaPreciosProveedor()
        //{
        //    int iRow = 2;
        //    string sNombreFile = "";            
        //    string sRutaReportes = "";
        //    string sTitulo = "LISTA DE PRECIOS DE PRODUCTOS DE " + lblNombreProveedor.Text;
            
        //    sRutaReportes = GnCompras.RutaReportes;
        //    DtGeneral.RutaReportes = sRutaReportes;

        //    sNombreFile = "Listado_Precios_Proveedor" + "_" + lblNombreProveedor.Text + ".xls";
        //    sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_OCEN_ListadoPreciosProveedor.xls";
        //    DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_OCEN_ListadoPreciosProveedor.xls", datosCliente);

        //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //    xpExcel.AgregarMarcaDeTiempo = false;

        //    if (xpExcel.PrepararPlantilla(sNombreFile))
        //    {
        //        xpExcel.GeneraExcel();

        //        //Se pone el encabezado
        //        leerExportarExcel.RegistroActual = 1;
        //        leerExportarExcel.Leer();
        //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow, 2);
        //        iRow++;
        //        xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, iRow, 2);
        //        iRow++;

        //        xpExcel.Agregar(sTitulo, iRow, 2);

        //        iRow = 6;
        //        xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);

        //        // Se ponen los detalles
        //        leerExportarExcel.RegistroActual = 1;
        //        iRow = 9;

        //        while (leerExportarExcel.Leer())
        //        {
        //            xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRow, 2);
        //            xpExcel.Agregar(leerExportarExcel.Campo("DescripcionSal"), iRow, 3);
        //            xpExcel.Agregar(leerExportarExcel.Campo("CodigoEAN"), iRow, 4);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Descripcion"), iRow, 5);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Status"), iRow, 6);                    
        //            xpExcel.Agregar(leerExportarExcel.Campo("Precio"), iRow, 7);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Descuento"), iRow, 8);
        //            xpExcel.Agregar(leerExportarExcel.Campo("TasaIva"), iRow, 9);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Iva"), iRow, 10);
        //            xpExcel.Agregar(leerExportarExcel.Campo("PrecioUnitario"), iRow, 11);
        //            xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRow, 12);
        //            xpExcel.Agregar(leerExportarExcel.Campo("FechaFinVigencia"), iRow, 13);

        //            iRow++;
        //        }

        //        // Finalizar el Proceso 
        //        xpExcel.CerrarDocumento();

        //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //        {
        //            xpExcel.AbrirDocumentoGenerado();
        //        }                
        //    }
        //}
        #endregion Reportes        

    }
}
