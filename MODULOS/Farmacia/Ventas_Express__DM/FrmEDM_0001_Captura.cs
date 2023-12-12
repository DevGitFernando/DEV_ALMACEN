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
using DllFarmaciaSoft.LimitesConsumoClaves;
using DllFarmaciaSoft.Usuarios_y_Permisos;

using Farmacia.Procesos;
using Farmacia.Vales;

//using Dll_IMach4;
//using Dll_IMach4.Interface; 
using DllRobotDispensador;

namespace Farmacia.Ventas_Express__DM
{
    public partial class FrmEDM_0001_Captura : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5,
            Precio = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10,
            EsIMach4 = 11, UltimoCosto = 12
        }

        clsDatosCliente DatosCliente;
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leer2;
        clsLeer leer3;

        DllFarmaciaSoft.clsAyudas Ayuda; 
        DataSet dtsCargarDatos = new DataSet();
        DllFarmaciaSoft.clsConsultas Consultas;

        clsGrid myGrid;
        TiposDeInventario tpTipoDeInventario = TiposDeInventario.Todos;
        TiposDeUbicaciones tpUbicacion = TiposDeUbicaciones.Todas;
        clsLotes Lotes;
        clsCodigoEAN EAN = new clsCodigoEAN();
        Cols ColActiva = Cols.Ninguna;
        string sCodigoEAN_Seleccionado = "";

        string sFolioCaptura = ""; 
        string sFolioVenta = "", sMensaje = "";
        bool bFolioGuardado = false;
        string sValorGrid = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdClienteDefault = GnFarmacia.Cliente_DefaultOperacion;
        string sIdSubClienteDefault = GnFarmacia.SubCliente_DefaultOperacion;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
        double fSubTotal = 0, fIva = 0, fTotal = 0;

        bool bContinua = true;
        bool bEsIdProducto_Ctrl = false;
        string sIdPublicoGral = GnFarmacia.PublicoGral;
        bool bEsSeguroPopular = false;
        bool bValidarSeguroPopular = GnFarmacia.ValidarInformacionSeguroPopular;
        bool bValidarBeneficioSeguroPopular = GnFarmacia.ValidarBeneficioSeguroPopular;
        bool bDispensarSoloCuadroBasico = GnFarmacia.DispensarSoloCuadroBasico;
        bool bImplementaCodificacion = GnFarmacia.ImplementaCodificacion_DM; 

        public FrmEDM_0001_Captura()
        {
            InitializeComponent();

            leer = new clsLeer(ref con);
            leer2 = new clsLeer(ref con);
            leer3 = new clsLeer(ref con);

            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);
            Error = new clsGrabarError(GnFarmacia.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White;
        }

        #region Form 
        private void FrmEDM_0001_Captura_Load(object sender, EventArgs e)
        {
            IniciarPantalla(); 
        }

        private void FrmEDM_0001_Captura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                TeclasRapidas(e);
            }

            switch (e.KeyCode)
            {

                case Keys.F5:
                    break;

                case Keys.F7:
                    mostrarOcultarLotes();
                    break;

                case Keys.F9:
                    break;

                default:
                    // base.OnKeyDown(e);
                    break;
            }
        }

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

                case Keys.L:
                    if (btnNuevo_Captura.Enabled)
                    {
                        bntNuevo_Captura_Click(null, null);
                    }
                    break;

                case Keys.C:
                    if (btnMostrarLectorDM.Enabled)
                    {
                        btnMostrarLectorDM_Click(null, null);
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion Form
        
        #region Botones
        private void IniciarToolBar()
        {
            IniciarToolBar(false, false);
        }

        private void IniciarToolBar(bool Guardar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnImprimir.Enabled = Imprimir;
        }

        private void IniciarPantalla()
        {
            Fg.IniciaControles(this, true);
            grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            bFolioGuardado = false;

            myGrid.BloqueaColumna(false, 1);
            myGrid.Limpiar(false);

            dtpFechaRegistro.Enabled = false;
            dtpFechaRegistro.Value = GnFarmacia.FechaOperacionSistema;

            CambiaEstado(true);            

            Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpTipoDeInventario);
            Lotes.ManejoLotes = OrigenManejoLotes.Ventas_Dispensacion;
            Lotes.MostrarLotesExistencia_0 = GnFarmacia.MostrarLotesSinExistencia;

            txtFolio.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            IniciarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bContinua = false;
            bool bBtnGuardar = btnGuardar.Enabled;
            //bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;

            if (txtFolio.Text != "*")
            {
                General.msjUser("Este Folio ya ha sido guardado por lo tanto no puede ser modificado");
            }
            else
            {
                if (ValidaDatos())
                {
                    if (con.Abrir())
                    {
                        IniciarToolBar();
                        con.IniciarTransaccion();

                        ////GuardaVenta();
                        ////if (tpPuntoDeVenta != TipoDePuntoDeVenta.AlmacenJurisdiccional)
                        ////{
                        ////    GuardaVentaInformacionAdicional();  // Guarda la informacion Adicional sobre Servicio, Area, Medico, Diagnostico, etc. 
                        ////}
                        ////else
                        ////{
                        ////    GuardaVenta_ALMJ_PedidosRC_Surtido();
                        ////}

                        if (GuardaVenta())
                        {
                            if (GuardaDetallesVenta())
                            {
                                if (GuardaVentasDet_Lotes())
                                {
                                    if (bImplementaCodificacion)
                                    {
                                        if (Guardar_UUIDS(sFolioVenta, true, true))
                                        {
                                            bContinua = true;
                                        }
                                    }
                                }
                            }
                        }


                        if (bContinua)
                        {
                            con.CompletarTransaccion();

                            txtFolio.Text = sFolioVenta;
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        }
                        else
                        {
                            con.DeshacerTransaccion();
                            txtFolio.Text = "*";
                            Error.GrabarError(leer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al guardar la información.");
                            btnGuardar.Enabled = true;
                        }
                        con.Cerrar();
                    }
                    else
                    {
                        Error.LogError(con.MensajeError);
                        General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                    }
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void bntNuevo_Captura_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;

            bRegresa = General.msjConfirmar("¿ Desea borrar toda la información de los productos capturados ?") == System.Windows.Forms.DialogResult.Yes; 

            if (bRegresa)
            {
                Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpTipoDeInventario);
                Lotes.ManejoLotes = OrigenManejoLotes.Ventas_Dispensacion;
                Lotes.MostrarLotesExistencia_0 = GnFarmacia.MostrarLotesSinExistencia;
                myGrid.Limpiar(false);
            }
        }

        private void btnMostrarLectorDM_Click(object sender, EventArgs e)
        {
            Codificacion(); 
        }

        #region Codificacion Datamatrix
        private void Codificacion()
        {
            int iRows = 0;
            string sProducto = "", sCodigoEAN = "", sDescripcion = "";
            clsLeer leerUUIDS = new clsLeer();
            clsLotes lotes_Aux = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpTipoDeInventario);

            sProducto = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Codigo);
            sCodigoEAN = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
            sDescripcion = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Descripcion);

            FrmLotesSNK f = new FrmLotesSNK();
            f.MostrarPantalla(sProducto, sCodigoEAN, sDescripcion, bEsIdProducto_Ctrl, Lotes);
            Lotes = f.LotesCodigos;

            if (Lotes.ListadoDeCodigosEAN().Count > 0)
            {
                myGrid.Limpiar(false);
                lotes_Aux = Lotes;

                foreach (string sCodigoEAN_SNK in Lotes.ListadoDeCodigosEAN())
                {
                    myGrid.Rows = myGrid.Rows + 1;
                    iRows = myGrid.Rows;

                    myGrid.ActiveRow = iRows;
                    myGrid.SetActiveCell(iRows, 1);
                    myGrid.SetValue((int)Cols.CodEAN, sCodigoEAN_SNK);

                    ObtenerDatosProducto(iRows, false);
                    sProducto = myGrid.GetValue(iRows, (int)Cols.Codigo);
                    sCodigoEAN = myGrid.GetValue(iRows, (int)Cols.CodEAN);

                    myGrid.SetValue((int)Cols.Cantidad, lotes_Aux.Totalizar(sProducto, sCodigoEAN));

                }

                Lotes = lotes_Aux;
                ////Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpTipoDeInventario); 
                ////Lotes.AddLotes(lotes_Aux.DataSetLotes); 
                ////Lotes.UUID_UpdateList(lotes_Aux.UUID_List().DataSetClase); 
                leerUUIDS.DataSetClase = Lotes.UUID_List.DataSetClase;

            }
        }
        #endregion Codificacion Datamatrix
        #endregion Botones

        #region Folio 
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            string sFolio = "";  // sSql = "", 
            bFolioGuardado = false;

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                IniciarToolBar(true, false);
                txtFolio.Text = "*";
                txtFolio.Enabled = false;

                CargarDatosDefault();
                txtCte.Focus();
            }
            else
            {
                string sSql = string.Format(" SELECT *  FROM vw_Ventas_EDM_Enc (NoLock) " +
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And Folio = '{3}' ",
                    sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8));
                //leer.DataSetClase = Consultas.FolioEnc_Ventas(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");

                if (!leer.Exec(sSql))
                {
                    General.msjError("Ocurrió un error al obtener los datos del Folio");
                    Error.GrabarError(leer, "txtFolio_Validating()");
                }
                else
                {
                    if (!leer.Leer())
                    {
                        General.msjUser("Folio de dispensación rápida no encontrado, Verifique.");
                        txtFolio.Text = "";
                        txtFolio.Focus();
                    }
                    else
                    {
                        bFolioGuardado = true;
                        IniciarToolBar(false, true);
                        sFolio = leer.Campo("Folio");
                        sFolioVenta = sFolio;
                        txtFolio.Text = sFolio;

                        dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");

                        txtCte.Text = leer.Campo("IdCliente");
                        lblCte.Text = leer.Campo("NombreCliente");
                        txtSubCte.Text = leer.Campo("IdSubCliente");
                        lblSubCte.Text = leer.Campo("NombreSubCliente");
                        txtNumReceta.Text = leer.Campo("NumReceta");


                        if (leer.Campo("Status") == "C")
                        {
                            lblCancelado.Visible = true;
                        }
                        CargaDetallesVenta();
                        CambiaEstado(false);
                    }
                }
            }
        }

        private bool CargaDetallesVenta()
        {
            bool bRegresa = true;

            string sSql = string.Format("Select D.CodigoEAN, D.IdProducto, Descripcion, D.TasaIva, D.CantidadVendida " +
                " From Ventas_EDM_Det D (NoLock) Inner Join vw_Productos_CodigoEAN P (NoLock) On (D.CodigoEAN = P.CodigoEAN) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia  = '{2}' and Folio = '{3}' " +
                " Order By Renglon ", sEmpresa, sEstado, sFarmacia, sFolioVenta);

            //leer2.DataSetClase = Consultas.FolioDet_Ventas(sEmpresa, sEstado, sFarmacia, sFolioVenta, "CargaDetallesVenta");

            if (!leer2.Exec(sSql))
            {
                General.msjError("Ocurrió un error al obtener los datos de los Detalles del Folio");
                Error.GrabarError(leer2, "txtFolio_Validating()");
            }
            else
            {
                if (leer2.Leer())
                {
                    myGrid.LlenarGrid(leer2.DataSetClase, false, false);
                }
                else
                {
                    bRegresa = false;
                }
            }
            // myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.BloqueaColumna(true, 1);

            //CargarDetallesLotesVenta();
            return bRegresa;
        }

        private void CambiaEstado(bool bValor)
        {
            txtFolio.Enabled = bValor;
            txtCte.Enabled = bValor;
            txtSubCte.Enabled = bValor;
            txtNumReceta.Enabled = bValor;
            dtpFechaDeReceta.Enabled = bValor; 
        }
        #endregion Folio 

        #region GuardarVenta
        private bool GuardaVenta()
        {
            bool bRegresa = true;
            string sSql = "", sCaja = GnFarmacia.NumCaja;
            int iOpcion = 1;

            // Asignar el valor a la variable global 
            sFolioVenta = txtFolio.Text;

            CalcularTotales();

            sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_Ventas_EDM_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                    DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                    sFolioVenta, DtGeneral.IdPersonal, Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4),
                        txtNumReceta.Text.Trim(), General.FechaYMD(dtpFechaDeReceta.Value, "-"),
                    //fSubTotal, fIva, fTotal, (int)TipoDeVenta.Credito,
                    iOpcion);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (!leer.Leer())
                {
                    bRegresa = false;
                }
                else
                {
                    sFolioVenta = String.Format("{0}", leer.Campo("Clave"));
                    sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                }
            }

            return bRegresa;
        }

        private bool GuardaDetallesVenta()
        {
            bool bRegresa = true;
            string sSql = "", sIdProducto = "", sCodigoEAN = "";
            int iRenglon = 0, iUnidadDeSalida = 0, iCant_Entregada = 0, iCant_Devuelta = 0, iCantVendida = 0, iOpcion = 0;
            double dCostoUnitario = 0, dPrecioUnitario = 0, dImpteIva = 0, dTasaIva = 0, dPorcDescto = 0, dImpteDescto = 0;

            iOpcion = 1;

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                ////sCodigoEAN = myGrid.GetValue(i, 1);
                ////sIdProducto = myGrid.GetValue(i, 2);
                ////iCantVendida = myGrid.GetValueInt(i, 5);
                ////iCant_Entregada = myGrid.GetValueInt(i, 5);
                ////dPrecioUnitario = myGrid.GetValueDou(i, 6);
                ////dImpteIva = myGrid.GetValueDou(i, 8);
                ////dTasaIva = myGrid.GetValueDou(i, 4);

                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                iCantVendida = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                iCant_Entregada = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                dPrecioUnitario = myGrid.GetValueDou(i, (int)Cols.Precio);
                dImpteIva = myGrid.GetValueDou(i, (int)Cols.ImporteIva);
                dTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                dCostoUnitario = myGrid.GetValueDou(i, (int)Cols.UltimoCosto);

                iRenglon = i;

                if (sIdProducto != "")
                {
                    sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_Ventas_EDM_Det '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}' ",
                                         DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), sFolioVenta, Fg.PonCeros(sIdProducto, 8),
                                         sCodigoEAN, iRenglon, iUnidadDeSalida, iCant_Entregada, iCant_Devuelta, iCantVendida, dCostoUnitario,
                                         dPrecioUnitario, dImpteIva, dTasaIva, dPorcDescto, dImpteDescto, iOpcion);
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private bool GuardaVentasDet_Lotes()
        {
            bool bRegresa = true;
            string sSql = "", sIdProducto = "", sCodigoEAN = "";  // , sClaveLote = "";
            int iRenglon = 0, iCantVendida = 0, iOpcion = 0;
            double dCostoUnitario = 0; // , dPrecioUnitario = 0, dImpteIva = 0, dTasaIva = 0, dPorcDescto = 0, dImpteDescto = 0;

            iOpcion = 1;

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                iCantVendida = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                dCostoUnitario = myGrid.GetValueDou(i, (int)Cols.UltimoCosto);

                iRenglon = i;
                //ObtieneClaveLote(sIdProducto, sCodigoEAN, ref sClaveLote);

                clsLotes[] ListaLotes = Lotes.Lotes(sIdProducto, sCodigoEAN);

                foreach (clsLotes L in ListaLotes)
                {
                    if (sIdProducto != "" && L.Cantidad > 0)
                    {
                        sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_Ventas_EDM_Det_Lotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
                                                DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                                                L.IdSubFarmacia, sFolioVenta, Fg.PonCeros(sIdProducto, 8),
                                                sCodigoEAN, L.ClaveLote, iRenglon, L.Cantidad, iOpcion);
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

        private bool Guardar_UUIDS(string Folio, bool Validar_UUID, bool EsSalida)
        {
            bool bRegresa = true;
            string sSql = "";
            clsLeer leer_UUIDS = new clsLeer();
            int iValidar = Validar_UUID ? 1 : 0;
            int iEsSalida = EsSalida ? 1 : 0;

            leer_UUIDS.DataSetClase = Lotes.UUID_List.DataSetClase;
            bRegresa = leer_UUIDS.Registros > 0;

            while (leer_UUIDS.Leer())
            {
                sSql = string.Format("Exec spp_Mtto_Ventas_EDM_Det_UUID " +
                    " @UUID = '{0}', @IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', @Folio = '{4}', " +
                    " @ValidarUUID = '{5}', @TipoDeProceso = '{6}' " ,
                    leer_UUIDS.Campo("UUID"), DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Folio, iValidar, iEsSalida);
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
            }


            return bRegresa;
        }

        #endregion GuardarVenta

        #region Informacion
        private bool ValidaDatos()
        {
            bool bRegresa = true;
            // string sIdProducto = "";

            if (GnFarmacia.UsuarioConSesionCerrada(false))
            {
                bRegresa = false;
                Application.Exit();
            }

            if (bRegresa && txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Venta inválido, Verifique.");
                txtFolio.Focus();
            }

            if (bRegresa && txtNumReceta.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Receta inválido, Verifique.");
                txtNumReceta.Focus();
            }

            //if (bRegresa)
            //{
            //    bRegresa = validarCapturaProductos();
            //}

            return bRegresa;
        }

        private void CalcularTotales()
        {
            double sSubTotal = 0, sIva = 0, sTotal = 0;

            //fSubTotalIva_0 = 0;
            //fSubTotal = myGrid.TotalizarColumnaDou((int)Cols.Importe);
            //fIva = myGrid.TotalizarColumnaDou((int)Cols.ImporteIva);
            //fTotal = myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal);

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

        private void CargarDatosDefault()
        {
            txtCte.Text = sIdClienteDefault;
            txtSubCte.Text = sIdSubClienteDefault;

            txtCte_Validating(null, null);
            txtSubCte_Validating(null, null); 
        }

        private void ObtenerDatosProducto()
        {
            ObtenerDatosProducto(myGrid.ActiveRow, true);
        }

        private void ObtenerDatosProducto(int Renglon, bool BuscarInformacion)
        {
            string sCodigo = "", sSql = "";
            bool bCargarDatosProducto = true;
            string sMsj = "";

            sCodigo = myGrid.GetValue(Renglon, (int)Cols.CodEAN);
            if (EAN.EsValido(sCodigo) && sCodigo != "")
            {
                if (!GnFarmacia.ValidarSeleccionCodigoEAN(sCodigo, ref sCodigoEAN_Seleccionado))
                {
                    myGrid.LimpiarRenglon(Renglon);
                    myGrid.SetActiveCell(Renglon, (int)Cols.CodEAN);
                }
                else
                {
                    sCodigo = sCodigoEAN_Seleccionado;
                    sSql = string.Format("Exec Spp_ProductoVentasFarmacia " +
                        " @Tipo = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdCodigo = '{3}', @CodigoEAN = '{4}', " +
                        " @IdEstado = '{5}', @IdFarmacia = '{6}', @EsSectorSalud = '{7}', @EsClienteIMach = '{8}', @ClavesRecetaElectronica = '{9}',  " +
                        " @INT_OPM_ProcesoActivo = '{10}' ",
                        (int)TipoDeVenta.Credito, txtCte.Text.Trim(), txtSubCte.Text.Trim(),
                        Fg.PonCeros(sCodigo, 13), sCodigo.Trim(),
                        Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), 1, Convert.ToInt32(RobotDispensador.Robot.EsClienteInterface), "",
                        Convert.ToInt32(GnFarmacia.INT_OPM_ProcesoActivo));
                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "ObtenerDatosProducto()");
                        General.msjError("Ocurrió un error al obtener la información del Producto.");
                    }
                    else
                    {
                        if (!leer.Leer())
                        {
                            General.msjUser("Producto no encontrado ó no esta Asignado a la Farmacia.");
                            myGrid.LimpiarRenglon(Renglon);
                        }
                        else
                        {
                            if (!leer.CampoBool("EsDeFarmacia"))
                            {
                                bCargarDatosProducto = false;
                                sMsj = "El Producto " + leer.Campo("Descripcion") + " no esta registrado en la Farmacia, verifique.";
                            }
                            else
                            {
                                if (bDispensarSoloCuadroBasico)
                                {
                                    if (!leer.CampoBool("DCB"))
                                    {
                                        bCargarDatosProducto = false;
                                        sMsj = "El Producto " + leer.Campo("Descripcion") + " no esta dentro del Cuadro Básico Autorizado, verifique.";
                                    }
                                }
                            }

                            if (!bCargarDatosProducto)
                            {
                                General.msjUser(sMsj);
                                myGrid.LimpiarRenglon(Renglon);
                                myGrid.SetActiveCell(Renglon, (int)Cols.CodEAN);
                            }
                            else
                            {
                                CargaDatosProducto(Renglon, BuscarInformacion);
                            }
                        }
                    }
                }
            }
            else
            {
                //General.msjError(sMsjEanInvalido);
                myGrid.LimpiarRenglon(Renglon);
                myGrid.ActiveCelda(Renglon, (int)Cols.CodEAN);
                SendKeys.Send("");
            }
        }

        private bool validarProductoCtrlVales(string CodigoEAN)
        {
            bool bRegresa = true;
            bool bEsCero = false;
            // string sDato = "";

            bEsCero = CodigoEAN == "0000000000000" ? true : false;
            if (bEsCero)
            {
                bEsIdProducto_Ctrl = true;
                if (!GnFarmacia.EmisionDeValesCompletos)
                {
                    bEsIdProducto_Ctrl = false;
                    bRegresa = false;
                    General.msjUser("La unidad no esta configurada para manejar este Producto, verifique.");
                }
            }

            return bRegresa;
        }

        private void CargaDatosProducto()
        {
            CargaDatosProducto(myGrid.ActiveRow, true);
        }

        private void CargaDatosProducto(int Renglon, bool BuscarInformacion)
        {
            int iRowActivo = Renglon; //// myGrid.ActiveRow;           
            int iColEAN = (int)Cols.CodEAN;
            bool bEsMach4 = false;
            string sCodEAN = leer.Campo("CodigoEAN");

            ////if (lblCancelado.Visible == false)
            {
                if (sValorGrid != sCodEAN)
                {
                    if (validarProductoCtrlVales(sCodEAN))
                    {
                        if (!myGrid.BuscaRepetido(sCodEAN, iRowActivo, iColEAN))
                        {
                            myGrid.SetValue(iRowActivo, iColEAN, sCodEAN);
                            myGrid.SetValue(iRowActivo, (int)Cols.Codigo, leer.Campo("IdProducto"));
                            myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, leer.Campo("Descripcion"));
                            myGrid.SetValue(iRowActivo, (int)Cols.TasaIva, leer.Campo("PorcIva"));
                            myGrid.SetValue(iRowActivo, (int)Cols.Precio, leer.Campo("PrecioVenta"));
                            myGrid.SetValue(iRowActivo, (int)Cols.UltimoCosto, leer.Campo("UltimoCosto"));
                            myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, 0);

                            bEsMach4 = leer.CampoBool("EsMach4");
                            myGrid.SetValue(iRowActivo, (int)Cols.EsIMach4, bEsMach4);

                            myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.CodEAN);

                            ////////// Marcar el Renglon como Codigo de Robot si la Farmacia conectada tiene Robot 
                            ////if (IMach4.EsClienteIMach4)
                            ////{
                            ////    if (bEsMach4)
                            ////    {
                            ////        GnFarmacia.ValidarCodigoIMach4(myGrid, bEsMach4, iRowActivo);
                            ////        IMachPtoVta.Show(leer.Campo("IdProducto"), sCodEAN);
                            ////    }
                            ////}

                            Application.DoEvents(); //// Asegurar que se refresque la pantalla 
                            CargarLotesCodigoEAN(BuscarInformacion);


                            // myGrid.SetActiveCell(myGrid.iRowActivo, 1);
                            myGrid.SetActiveCell(iRowActivo, (int)Cols.Precio);
                        }
                        else
                        {
                            General.msjUser("El artículo ya se encuentra capturado en otro renglón.");
                            myGrid.SetValue(myGrid.ActiveRow, 1, "");
                            myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                            myGrid.EnviarARepetido();
                        }
                    }
                }
                else
                {
                    // Asegurar que no cambie el CodigoEAN
                    myGrid.SetValue(iRowActivo, iColEAN, sCodEAN);
                }
            }

            grdProductos.EditMode = false;
        }
        #endregion Informacion

        #region Manejo de lotes
        private void CargarLotesCodigoEAN()
        {
            CargarLotesCodigoEAN(true);
        }

        private void CargarLotesCodigoEAN(bool BuscarInformacion)
        {
            int iRow = myGrid.ActiveRow;
            string sCodigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
            string sCodEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);

            if (BuscarInformacion)
            {
                leer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpTipoDeInventario, false, "CargarLotesCodigoEAN()");
                if (Consultas.Ejecuto)
                {
                    // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                    leer.Leer();
                    Lotes.AddLotes(leer.DataSetClase);

                    if (GnFarmacia.ManejaUbicaciones)
                    {
                        leer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN_Ubicaciones__Ventas(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpTipoDeInventario, tpUbicacion, false, "CargarLotesCodigoEAN()");
                        if (Consultas.Ejecuto)
                        {
                            leer.Leer();
                            Lotes.AddLotesUbicaciones(leer.DataSetClase);
                        }
                    }

                    mostrarOcultarLotes();
                }
            }
        }

        private void removerLotes()
        {
            if (!bFolioGuardado)
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
                    if (!bImplementaCodificacion)
                    {
                        myGrid.BloqueaGrid(true);
                    }
                }
            }
        }

        private void mostrarOcultarLotes()
        {
            int iRow = myGrid.ActiveRow;
            string sCodigoEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);

            //////if (sCodigoEAN == Fg.PonCeros(0, 13))
            //////{
            //////    MostrarCapturaDeClavesRequeridas();
            //////}
            //////else
            {
                mostrarOcultarLotes_General();
            }
        }

        private void mostrarOcultarLotes_General()
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
                    Lotes.ModificarCantidades = bEsIdProducto_Ctrl ? false : true;

                    if (bFolioGuardado)
                    {
                        Lotes.ModificarCantidades = false;
                    }
                    
                    if ( bImplementaCodificacion ) 
                    {
                        Lotes.ModificarCantidades = false;
                    }

                    //// 2K120105.2025 
                    //// Control de Vales para Puebla 

                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.Default;
                    // Lotes.ManejoLotes = OrigenManejoLotes.Ventas_Dispensacion; 

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;

                    {
                        Lotes.IdCliente = txtCte.Text;
                        Lotes.IdSubCliente = txtSubCte.Text;
                        Lotes.Show();
                    }


                    myGrid.SetValue(iRow, (int)Cols.Cantidad, Lotes.Cantidad);
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

        #region Cliente - SubCliente 
        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                leer2.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, "txtCte_KeyDown");
                if (leer2.Leer())
                {
                    txtCte.Text = leer2.Campo("IdCliente");
                    lblCte.Text = leer2.Campo("NombreCliente");
                    txtSubCte.Focus();
                }
            }
        }

        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            bEsSeguroPopular = false;

            if (txtCte.Text.Trim() == "")
            {
                txtCte.Text = "";
                lblCte.Text = "";
                txtSubCte.Text = "";
                lblSubCte.Text = "";
                lblSubCte.Text = "";
            }
            else
            {
                if (Fg.PonCeros(txtCte.Text, 4) == sIdPublicoGral)
                {
                    General.msjAviso("El Cliente Público General es exclusivo de Venta a Contado, no puede ser utilizado en Dispensación Express");
                    txtCte.Text = "";
                    lblCte.Text = "";
                    e.Cancel = true;
                }
                else
                {
                    leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, "txtCte_Validating");
                    if (!leer.Leer())
                    {
                        General.msjUser("Clave de Cliente no encontrada, ó el Cliente no pertenece a la Farmacia.");
                        e.Cancel = true;
                    }
                    else
                    {
                        ////txtCte.Enabled = false;
                        txtCte.Text = leer.Campo("IdCliente");
                        lblCte.Text = leer.Campo("NombreCliente"); 
                    }
                }
            }
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "")
                {
                    leer2.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, "txtSubCte_KeyDown");
                    if (leer2.Leer())
                    {
                        txtSubCte.Text = leer2.Campo("IdSubCliente");
                        lblSubCte.Text = leer2.Campo("NombreSubCliente");
                        txtNumReceta.Focus();
                    }
                }
            }
        }

        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCte.Text.Trim() == "")
            {
                txtSubCte.Text = "";
                lblSubCte.Text = "";
                lblSubCte.Text = ""; 
            }
            else
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, "txtCte_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Sub-Cliente no encontrada, ó el Sub-Cliente no pertenece a la Farmacia.");
                    e.Cancel = true;
                }
                else
                {
                    ////// Obtener datos de IMach 
                    ////sFolioSolicitud = IMachPtoVta.ObtenerFolioSolicitud(); 

                    ////txtSubCte.Enabled = false;
                    txtSubCte.Text = leer.Campo("IdSubCliente");
                    lblSubCte.Text = leer.Campo("NombreSubCliente");
                }
            }

        }
        #endregion Cliente - SubCliente

        #region Control del Grid 
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            int iRowActivo = myGrid.ActiveRow;

            switch (ColActiva)
            {
                case Cols.Precio:
                    break;

                case Cols.CodEAN:
                case Cols.Descripcion:
                case Cols.Cantidad:
                case Cols.Importe:
                    if (e.KeyCode == Keys.Delete)
                    {
                        if (!bEsIdProducto_Ctrl)
                        {
                            removerLotes();
                        }
                    }
                    break;

                default:
                    break; 
            }
        }
        #endregion Control del Grid
    }
}
