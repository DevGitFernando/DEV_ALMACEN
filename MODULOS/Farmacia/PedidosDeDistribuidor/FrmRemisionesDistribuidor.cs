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

namespace Farmacia.PedidosDeDistribuidor
{
    public partial class FrmRemisionesDistribuidor : FrmBaseExt 
    {
        // clsDatosConexion DatosDeConexion; 
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnRegional;
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsLeer myLeerLotes;
        DllFarmaciaSoft.clsAyudas Ayuda;

        clsLeerWebExt leerWeb;
        clsLeer leerPedido;
        clsLeer leer;
        // TiposDeInventario tpInventarioModulo = TiposDeInventario.Venta; 

        DataSet dtsCargarDatos = new DataSet();
        DllFarmaciaSoft.clsConsultas Consultas;
        clsGrid myGrid; 
        clsCodigoEAN EAN = new clsCodigoEAN();

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        //DataSet para ejecutar los pedidos de distribuidor en Regional
        DataSet dtsPedido = new DataSet();

        bool bEsConsultaExterna = false; 
        bool bContinua = true;
        // bool bModificarCaptura = true;
        string sFolioRemision = "", sMensaje = "";
        string sEmpresa = DtGeneral.EmpresaConectada; 
        string sEstado = DtGeneral.EstadoConectado; 
        string sFarmacia = DtGeneral.FarmaciaConectada;
        // string sFolioMovto = "";
        string sFormato = "###,###,##0";
        string sStatus = "";

        bool bFolioGuardado = false;
        int iFolioCargaMasiva = 0;
        //////string sUrlAlmacenRegional = GnFarmacia.UrlAlmacenRegional;
        //////string sHostAlmacenRegional = GnFarmacia.HostAlmacenRegional;
        //////string sIdFarmaciaAlmacenRegional = GnFarmacia.IdFarmaciaAlmacenRegional;

        private enum Cols
        {
            Ninguna = 0, 
            ClaveSSA = 1, IdClaveSSA = 2, Descripcion = 3, Presentacion = 4,  Precio = 5, 
            Cantidad = 6, Cantidad_Recibida = 7   
        }


        public FrmRemisionesDistribuidor()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            myLeerLotes = new clsLeer(ref ConexionLocal);

            leerWeb = new clsLeerWebExt(General.Url, General.ArchivoIni, DatosCliente);
            leerPedido = new clsLeer(ref ConexionLocal);
            leer = new clsLeer(ref ConexionLocal);

            cnnRegional = new clsConexionSQL();
            cnnRegional.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnnRegional.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnnRegional.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnnRegional.DatosConexion.Password = General.DatosConexion.Password;
            cnnRegional.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnnRegional.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.BackColorColsBlk = Color.White;
            grdProductos.EditModeReplace = true;

            myGrid.SetOrder((int)Cols.ClaveSSA, (int)Cols.Descripcion, true);

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
        }

        private void FrmPedidosDistribuidor_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this,null);             
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
            if (e.Control)
            {
                TeclasRapidas(e);
            }

            switch (e.KeyCode)
            {
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        #region Limpiar 
        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false); 
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void LimpiarPantalla()
        {
            // bModificarCaptura = false;
            //Fg.IniciaControles(this, true);
            bFolioGuardado = true;
            IniciarToolBar(false, false, false);
            myGrid.EstiloGrid(eModoGrid.Normal);
            myGrid.Limpiar(true);
            rdoVenta.Enabled = true;
            rdoConsignacion.Enabled = true;
            sStatus = "";
            // Fg.BloqueaControles(this, true, FrameTipoDisp);
            
            rdoVenta.Checked = false; 
            rdoConsignacion.Checked = false; 
            rdoVenta.BackColor = Color.Transparent; 
            rdoConsignacion.BackColor = Color.Transparent;

            iFolioCargaMasiva = 0;

            if (!bEsConsultaExterna)
            {
                bFolioGuardado = false;

                Fg.IniciaControles(this, true);
                grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.

                // Estos campos deben ir deshabilitados son campos controlados 
                dtpFechaRegistro.Enabled = false;

                // Reiniciar Grid por Completo 
                myGrid = new clsGrid(ref grdProductos, this);
                myGrid.Limpiar(true);
                myGrid.BackColorColsBlk = Color.White;
                grdProductos.EditModeReplace = true;
                myGrid.BloqueaColumna(false, (int)Cols.Cantidad);


                // myGrid.Limpiar(true);
                txtIdPersonal.Text = DtGeneral.IdPersonal;
                lblPersonal.Text = DtGeneral.NombrePersonal;
                txtIdPersonal.Enabled = false;

                ////Fg.BloqueaControles(this, true, FrameTipoDisp);
                rdoVenta.Checked = false;
                rdoConsignacion.Checked = false;

                dtpFechaDocumento.MaxDate = General.FechaSistema;

                dtsPedido = null;
                dtsPedido = PreparaDtsPedidos();
                txtFolio.Focus();
            }
            else
            {
                txtFolio_Validating(null, null);                
                //grdProductos.Focus();
                //myGrid.BloqueaColumna(false, (int)Cols.Cantidad_Recibida);                
            }
        }
        #endregion Limpiar

        #region Buscar Folio  
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = true;
            // bModificarCaptura = true;
            bFolioGuardado = false; 
            IniciarToolBar(false, false, false);
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                IniciarToolBar(true, false, false);
            }
            else
            {
                myLeer.DataSetClase = Consultas.RemisionesDistEnc(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
                if (!myLeer.Leer())
                {
                    bContinua = false;
                    txtFolio.Text = ""; 
                }
                else
                {
                    txtFolio.Enabled = false; 
                    bFolioGuardado = true;
                    IniciarToolBar(false, false, true);
                    // bModificarCaptura = false;
                    CargaEncabezadoFolio();
                }

                if (bContinua)
                {
                    if (!CargaDetallesFolio())
                    {
                        bContinua = false;
                    }
                    CalcularCantidadRecida();
                }
            }

            if (!bContinua)
            {
                txtFolio.Focus();
            }

            if (bEsConsultaExterna)
            {
                txtObservaciones.Enabled = true;
                rdoVenta.Enabled = true;
                rdoConsignacion.Enabled = true;
                chkEsExcepcion.Enabled = true;
            }
        }

        private void CargaEncabezadoFolio()
        {
            // Inicializar el Control 
            DateTimePicker dtpPaso = new DateTimePicker(); 
            
            
            //Se hace de esta manera para la ayuda.
            txtFolio.Text = myLeer.Campo("Folio"); // FolioCompra
            sFolioRemision = txtFolio.Text;
            txtIdProveedor.Text = myLeer.Campo("IdDistribuidor");
            lblProveedor.Text = myLeer.Campo("Distribuidor"); 
            txtReferenciaDocto.Text = myLeer.Campo("ReferenciaPedido");
            dtpFechaDocumento.Value = myLeer.CampoFecha("FechaDocumento"); 
            txtCliente.Text = myLeer.Campo("CodigoCliente");

            // lblCliente.Text = myLeer.Campo("NombreCliente");
            lblCliente.Text = string.Format("{0} -- {1}", myLeer.Campo("NombreCliente"), myLeer.Campo("FarmaciaRelacionada"));

            txtObservaciones.Text = myLeer.Campo("Observaciones");
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");            
            txtIdPersonal.Enabled = false; 
            txtIdPersonal.Text = myLeer.Campo("IdPersonal");
            lblPersonal.Text = myLeer.Campo("NombrePersonal");
            sStatus = myLeer.Campo("Status");
            chkEsExcepcion.Checked = myLeer.CampoBool("EsExcepcion");

            iFolioCargaMasiva = myLeer.CampoInt("FolioCargaMasiva");

            if (myLeer.CampoBool("EsConsignacion"))
            {
                rdoConsignacion.Checked = true;
            }
            else
            {
                rdoVenta.Checked = true;
            }

            //////////Se bloquea el encabezado del Folio 
            ////////Fg.BloqueaControles(this, false, FrameEncabezado);
            ////////Fg.BloqueaControles(this, false, FrameTipoDisp);

            if (sStatus.Trim() == "C")
            {
                lblCancelado.Visible = true;
                lblCancelado.Text = "CANCELADA";
                IniciarToolBar(false, false, true);
            }

            if (sStatus.Trim() == "A")
            {
                IniciarToolBar(true, true, false);
            }

            if (sStatus.Trim() == "T")
            {
                lblCancelado.Visible = true;
                lblCancelado.Text = "TERMINADA";
                IniciarToolBar(false, true, true);
            }

            BloqueaControles(false);

            if (iFolioCargaMasiva > 0)
            {
                bFolioGuardado = false;
            }

            if (bEsConsultaExterna)
            {
                txtReferenciaDocto.Enabled = true;
            }
        }

        private void BloqueaControles(bool Bloquear)
        {
            txtFolio.Enabled = Bloquear; 
            txtIdProveedor.Enabled = Bloquear; 

            txtReferenciaDocto.Enabled = Bloquear;
            dtpFechaDocumento.Enabled = Bloquear;

            txtCliente.Enabled = Bloquear;
            txtObservaciones.Enabled = Bloquear;

            rdoVenta.Enabled = Bloquear;
            rdoConsignacion.Enabled = Bloquear;

            chkEsExcepcion.Enabled = Bloquear;

            //myGrid.BloqueaColumna(Bloquear, (int)Cols.Cantidad); 
            //myGrid.BloqueaColumna(Bloquear, (int)Cols.Cantidad_Recibida); 
        } 

        private bool CargaDetallesFolio()
        {
            bool bRegresa = false;

            myLlenaDatos.DataSetClase = Consultas.RemisionesDistDet(sEmpresa, sEstado, sFarmacia, sFolioRemision, "txtFolio_Validating");
            if (myLlenaDatos.Leer())
            {
                myGrid.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
                bRegresa = true;

                if (sStatus.Trim() == "A")
                {
                    //myGrid.SetValue((int)Cols.Cantidad_Recibida, 0);
                }
            }

            // Bloquear grid completo 
            //myGrid.BloqueaRenglon(true);
            //myGrid.EstiloGrid(eModoGrid.ModoRow);

            if (sStatus.Trim() == "A")
            {
                myGrid.BloqueaColumna(true, (int)Cols.ClaveSSA);
                myGrid.BloqueaColumna(true, (int)Cols.Cantidad);

                if (iFolioCargaMasiva > 0)
                {
                    myGrid.BloqueaColumna(false, (int)Cols.ClaveSSA);
                    myGrid.BloqueaColumna(false, (int)Cols.Cantidad);
                }
            }
            else
            {
                myGrid.BloqueaColumna(true, (int)Cols.ClaveSSA);
                myGrid.BloqueaColumna(true, (int)Cols.Cantidad);
                myGrid.BloqueaColumna(true, (int)Cols.Cantidad_Recibida);
            }

            

            CalcularCantidadRecida();
            return bRegresa;                 
        } 
        #endregion Buscar Folio

        #region Grabar_Informacion_Regional

        public static DataSet PreparaDtsPedidos()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtClave = new DataTable("Sentencias");

            dtClave.Columns.Add("Query", Type.GetType("System.String"));            
            dts.Tables.Add(dtClave);

            return dts.Clone();
        }

        //////private bool validarDatosDeConexion()
        //////{
        //////    bool bRegresa = false;

        //////    try
        //////    {
        //////        leerWeb = new clsLeerWebExt(sUrlAlmacenRegional, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

        //////        conexionWeb.Url = sUrlAlmacenRegional;
        //////        DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

        //////        DatosDeConexion.Servidor = sHostAlmacenRegional;
        //////        bRegresa = true;
        //////    }
        //////    catch (Exception ex1)
        //////    {
        //////        Error.GrabarError(myLeer.DatosConexion, ex1, "validarDatosDeConexion()");
        //////        General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
                
        //////    }

        //////    return bRegresa;
        //////}

        #endregion Grabar_Informacion_Regional

        #region Guardar/Actualizar Folio 
        private void btnGuardar_Click(object sender, EventArgs e)
        {    
            bContinua = false;
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled; 

            EliminarRenglonesVacios();
            if (validaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    IniciarToolBar(); 
                    ConexionLocal.IniciarTransaccion();

                    bContinua = Guarda_Encabezado_Pedido();                        

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        txtFolio.Text = sFolioRemision;
                        ConexionLocal.CompletarTransaccion(); 
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        IniciarToolBar(false, false, true);
                        ImprimirRemision();
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        //txtFolio.Text = "*"; 
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                        IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir); 
                        //btnNuevo_Click(null, null);

                    }

                    ConexionLocal.Cerrar();
                }
                else
                {
                    Error.LogError(ConexionLocal.MensajeError);
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                }

            }            

        }

        private bool Guarda_Encabezado_Pedido()
        {
            bool bRegresa = true;
            string sSql = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            int iEsConsignacion = 0, iEsExcepcion = 0;

            if (rdoVenta.Checked)
            {
                iEsConsignacion = 0;
            }
            if (rdoConsignacion.Checked)
            {
                iEsConsignacion = 1;
            }

            if (chkEsExcepcion.Checked)
            {
                iEsExcepcion = 1;
            }

            sSql = String.Format("Set DateFormat YMD Exec spp_Mtto_RemisionesDistEnc " + 
                                "'{0}', '{1}', '{2}', " + 
                                "'{3}', '{4}', '{5}', " + 
                                "'{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                            sEmpresa, sEstado, sFarmacia, txtFolio.Text,
                            txtIdProveedor.Text, txtReferenciaDocto.Text, txtCliente.Text.Trim(), General.FechaYMD(dtpFechaDocumento.Value), 
                            txtObservaciones.Text.Trim(), txtIdPersonal.Text, iEsConsignacion, iEsExcepcion, iOpcion);            

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolioRemision = myLeer.Campo("Clave");
                sMensaje = myLeer.Campo("Mensaje");

                bRegresa = Guarda_Detalles_Pedido();
            }

            return bRegresa;
        }

        private bool Guarda_Detalles_Pedido()
        {
            bool bRegresa = true;
            string sSql = "", sIdClaveSSA = "";
            int iCantidadRecibida = 0, iCant_Recibida = 0;
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            double dPrecio = 0;
            
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                // Se obtienen los datos para la insercion.
                sIdClaveSSA = myGrid.GetValue(i, (int)Cols.IdClaveSSA);
                iCant_Recibida = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                iCantidadRecibida = myGrid.GetValueInt(i, (int)Cols.Cantidad_Recibida);
                dPrecio = myGrid.GetValueDou(i, (int)Cols.Precio);

                if (sIdClaveSSA != "" && iCant_Recibida > 0)
                {
                    sSql = String.Format("Set DateFormat YMD Exec spp_Mtto_RemisionesDistDet " +
                                "'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8} ",
                            sEmpresa, sEstado, sFarmacia, sFolioRemision, sIdClaveSSA, iCant_Recibida, iCantidadRecibida, iOpcion, dPrecio);

                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        #endregion Guardar/Actualizar Folio

        #region Eliminar 
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bContinua = false;
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;

            EliminarRenglonesVacios();
            if (validaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    IniciarToolBar();
                    ConexionLocal.IniciarTransaccion();

                    bContinua = Cancela_Remision();

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        txtFolio.Text = sFolioRemision;
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        IniciarToolBar(false, false, true);
                        //ImprimirRemision();
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        txtFolio.Text = "*";
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnCancelar_Click");
                        General.msjError("Ocurrió un error al cancelar la información.");
                        IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir);
                        //btnNuevo_Click(null, null);

                    }

                    ConexionLocal.Cerrar();
                }
                else
                {
                    Error.LogError(ConexionLocal.MensajeError);
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                }

            }
        }

        private bool Cancela_Remision()
        {
            bool bRegresa = true;
            string sSql = "";
            int iOpcion = 2; //La opcion 2 indica que es una Cancelacion
            int iEsConsignacion = 0, iEsExcepcion = 0;

            if (rdoVenta.Checked)
            {
                iEsConsignacion = 0;
            }
            if (rdoConsignacion.Checked)
            {
                iEsConsignacion = 1;
            }

            if (chkEsExcepcion.Checked)
            {
                iEsExcepcion = 1;
            }

            sSql = String.Format("Set DateFormat YMD Exec spp_Mtto_RemisionesDistEnc " +
                                "'{0}', '{1}', '{2}', " +
                                "'{3}', '{4}', '{5}', " +
                                "'{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                            sEmpresa, sEstado, sFarmacia, txtFolio.Text,
                            txtIdProveedor.Text, txtReferenciaDocto.Text, txtCliente.Text.Trim(), General.FechaYMD(dtpFechaDocumento.Value),
                            txtObservaciones.Text.Trim(), txtIdPersonal.Text, iEsConsignacion, iEsExcepcion, iOpcion);

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolioRemision = myLeer.Campo("Clave");
                sMensaje = myLeer.Campo("Mensaje");
            }

            return bRegresa;
        }
        #endregion Eliminar  

        #region Imprimir
        private bool validarImpresion(bool Confirmar)
        {
            bool bRegresa = true;

            if (Confirmar)
            {
                if (General.msjConfirmar(" ¿ Desea imprimir la información en pantalla ? ") == DialogResult.No)
                {
                    bRegresa = false;
                    LimpiarPantalla();
                }
            }

            if (bRegresa)
            {
                if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
                {
                    bRegresa = false;
                    General.msjUser("Folio de Entrada inválido, verifique.");
                    txtFolio.Focus(); 
                }
            }

            return bRegresa;
        }

        private void ImprimirRemision()
        {
            bool bRegresa = false;

            DatosCliente.Funcion = "ImprimirRemision()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_RemisionesDistribuidor.rpt";

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("Folio", txtFolio.Text);

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)            
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
            
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirRemision();
        }
        #endregion Imprimir

        #region Importar Datos 
        private void btnImportarDatos_Click(object sender, EventArgs e)
        {
            bool bEsDST_Valido = false;

            if (txtIdProveedor.Text != "")
            {
                bEsDST_Valido = true; 
            }

            if (!bEsDST_Valido) 
            {
                General.msjError("Debe seleccionar un Distribuidor válido, verifique."); 
            }
            else 
            {
                FrmImportarRemisiones f = new FrmImportarRemisiones(txtIdProveedor.Text);
                f.ShowDialog();
            }

        }
        #endregion Importar Datos

        #region Validaciones de Controles
        private bool validaDatos()
        {
            bool bRegresa = true;
                        
            if (txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Compra inválido, verifique.");
                txtFolio.Focus();                
            }

            if (bRegresa && txtIdProveedor.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Proveedor inválida, verifique.");
                txtIdProveedor.Focus();
            }

            if (bRegresa && txtReferenciaDocto.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Referencia inválida, verifique.");
                txtReferenciaDocto.Focus();
            }

            if (bRegresa && txtCliente.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Cliente inválida, verifique.");
                txtCliente.Focus();
            }

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las Observaciones, verifique.");
                txtObservaciones.Focus();
            }

            if (!rdoVenta.Checked && !rdoConsignacion.Checked)
            {
                bRegresa = false;
                General.msjUser("No ha especificado el tipo de Remisión, verifique.");
            }

            if (bRegresa)
            {
                bRegresa = validarCapturaClaves(); 
            } 

            return bRegresa;
        }

        private bool validarCapturaClaves()
        {
            bool bRegresa = true;
            string sMensaje = "";
            
            if (myGrid.GetValue(1, (int)Cols.Descripcion) == "")
            {
                bRegresa = false;
                sMensaje = "Debe capturar al menos una ClaveSSA, verifique.";
            }
            else
            {
                for (int i = 1; i <= myGrid.Rows; i++)
                {
                    if (myGrid.GetValue(i, (int)Cols.ClaveSSA) != "" && myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0) 
                    {
                        sMensaje = "La cantidad debe ser mayor a cero, verifique.";
                        bRegresa = false;
                        break;
                    }
                }                    
            }
            

            if (!bRegresa)
                General.msjUser(sMensaje);

            return bRegresa;

        }

        #endregion Validaciones de Controles

        #region Eventos
        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // Checar por incongruencias 
                myLeer.DataSetClase = Ayuda.RemisionesDistEnc(sEmpresa, sEstado, sFarmacia, "txtFolio_KeyDown");

                if (myLeer.Leer())
                {
                    CargaEncabezadoFolio();
                    CargaDetallesFolio();
                }
            }

        }

        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLlenaDatos.DataSetClase = Ayuda.Distribuidores("txtIdProveedor_KeyDown");
                
                if (myLlenaDatos.Leer())
                {
                    CargaDatosProveedor();
                }
            }
        }

        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {
            myLlenaDatos = new clsLeer(ref ConexionLocal); 
            if (txtIdProveedor.Text.Trim() != "")
            {
                myLlenaDatos.DataSetClase = Consultas.Distribuidores(txtIdProveedor.Text.Trim(), "txtIdProveedor_Validating");
                if (myLlenaDatos.Leer())
                {
                    CargaDatosProveedor();
                    //myGrid.Limpiar(true);
                }
                else
                {
                    txtIdProveedor.Focus();
                }
            }

        }

        private void CargaDatosProveedor()
        {
            //Se hace de esta manera para la ayuda. 

            if (myLlenaDatos.Campo("Status").ToUpper() == "A")
            {
                txtIdProveedor.Text = myLlenaDatos.Campo("IdDistribuidor");
                lblProveedor.Text = myLlenaDatos.Campo("NombreDistribuidor"); 
            }
            else
            {
                General.msjUser("El Distribuidor " + myLlenaDatos.Campo("NombreDistribuidor") + " actualmente se encuentra cancelado, verifique. "); 
                txtIdProveedor.Text = "";
                lblProveedor.Text = "";
                txtIdProveedor.Focus(); 
            }
        }

        private void txtIdProveedor_TextChanged(object sender, EventArgs e)
        {
            lblProveedor.Text = "";
        }

        private void txtCliente_Validating(object sender, CancelEventArgs e)
        {
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            if (txtCliente.Text.Trim() != "")
            {
                myLlenaDatos.DataSetClase = Consultas.Distribuidor_Clientes(sEstado, txtIdProveedor.Text.Trim(), txtCliente.Text.Trim(), "txtCliente_Validating");
                if (myLlenaDatos.Leer())
                {
                    CargaDatosCliente();
                    ////myGrid.Limpiar(true);
                }
                else
                {
                    txtCliente.Focus();
                }
            }
        }

        private void txtCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLlenaDatos.DataSetClase = Ayuda.Distribuidor_Clientes(sEstado, txtIdProveedor.Text.Trim(), "txtCliente_KeyDown");

                if (myLlenaDatos.Leer())
                {
                    CargaDatosCliente();
                }
            }
        }

        private void CargaDatosCliente()
        {
            if (myLlenaDatos.Campo("Status").ToUpper() == "A")
            {
                string sNombre = "";
                sNombre = string.Format("{0} -- {1}", myLlenaDatos.Campo("NombreCliente"), myLlenaDatos.Campo("Farmacia"));

                txtCliente.Text = myLlenaDatos.Campo("CodigoCliente");
                lblCliente.Text = sNombre;
            }
            else
            {
                General.msjUser("El Distribuidor " + myLlenaDatos.Campo("NombreCliente") + " actualmente se encuentra cancelado, verifique. ");
                txtCliente.Text = "";
                lblCliente.Text = "";
                txtCliente.Focus();
            }
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            lblCliente.Text = "";
        }
        
        #endregion Eventos  

        #region Grid 
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);
            if (myGrid.ActiveCol == 1)
            {
                if (e.KeyCode == Keys.F1)
                {
                    myLeer.DataSetClase = Ayuda.ClavesSSA_Sales("grdProductos_KeyDown");
                    if (myLeer.Leer())
                    {
                        CargaDatosClave();
                    }
                }
            }

            if (!bFolioGuardado)
            {
                if (e.KeyCode == Keys.Delete)
                { 
                    try
                    {
                        int iRow = myGrid.ActiveRow; 
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

        private void CargaDatosClave()
        {
            int iRowActivo = myGrid.ActiveRow;

            if (lblCancelado.Visible == false)
            {
                if (!myGrid.BuscaRepetido(myLeer.Campo("ClaveSSA"), iRowActivo, 1))
                {
                    myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));
                    myGrid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, myLeer.Campo("IdClaveSSA_Sal"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Presentacion, myLeer.Campo("Presentacion"));
                    CargaPrecioClave();                    
                    myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                    myGrid.SetActiveCell(iRowActivo, (int)Cols.Cantidad); 

                    //////// Marcar el Renglon como Codigo de Robot si la Farmacia conectada tiene Robot 
                    ////if (IMach4.EsClienteIMach4)
                    ////    GnFarmacia.ValidarCodigoIMach4(myGrid, myLeer.CampoBool("EsMach4"), iRowActivo); 

                    //CargarLotesCodigoEAN();
                }
                else
                {
                    General.msjUser("Esta Clave ya se encuentra capturada en otro renglon.");
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA, "");
                    limpiarColumnas();
                    myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                    myGrid.EnviarARepetido(); 
                }

            }

            // grdProductos.EditMode = false;
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (!bFolioGuardado)
            {
                if (lblCancelado.Visible == false)
                {
                    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                    {
                        if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA) != "" )
                        {
                            myGrid.Rows = myGrid.Rows + 1;
                            myGrid.ActiveRow = myGrid.Rows;
                            myGrid.SetActiveCell(myGrid.Rows, (int)Cols.ClaveSSA);
                        }
                    }
                    CalcularCantidadRecida();
                }
            }

            CalcularCantidadRecida();
        }

        private void CalcularCantidadRecida()
        {
            double dTotal = 0, dCant_Total = 0;

            dTotal = myGrid.TotalizarColumnaDou((int)Cols.Cantidad_Recibida);
            lblTotal.Text = dTotal.ToString(sFormato);

            dCant_Total = myGrid.TotalizarColumnaDou((int)Cols.Cantidad);
            lblCantTotal.Text = dCant_Total.ToString(sFormato);
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            switch (myGrid.ActiveCol)
            {
                case 1: // Si se cambia el Codigo, se limpian las columnas
                    {
                        limpiarColumnas();
                    }
                    break;
            }

        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            string sValor = "";
            myLeer = new clsLeer(ref ConexionLocal);
            switch (myGrid.ActiveCol)
            {
                case 1:                    
                    {
                        sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA);

                        if (sValor != "")
                        {                            
                            myLeer.DataSetClase = Consultas.ClavesSSA_Sales(sValor, true,"grdProductos_EditModeOff");
                            if (myLeer.Leer())
                            {
                                CargaDatosClave();
                            }
                            else
                            {
                                myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.ClaveSSA);
                            }                            
                        }
                    }

                    break;                
            }

            CalcularCantidadRecida();
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= myGrid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                myGrid.SetValue( myGrid.ActiveRow, i, "");
            } 
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                if (myGrid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                    myGrid.DeleteRow(i);
            }

            if (myGrid.Rows == 0) // Si No existen renglones, se inserta 1.
                myGrid.AddRow();
        }

        private void CargaPrecioClave()
        {
            string sSql = "";
            
            sSql = string.Format(" Select (P.Precio * C.ContenidoPaquete) As Precio " +
	                            " From CFG_ClavesSSA_Precios P (Nolock) " +
	                            " Inner Join CatClavesSSA_Sales C (Nolock) On (P.IdClaveSSA_Sal = C.IdClaveSSA_Sal) " +
                                " Where P.IdEstado = '{0}' and P.IdCliente = '{1}' and P.IdSubCliente = '{2}' and C.ClaveSSA_Base = '{3}'",
                                sEstado, GnFarmacia.SeguroPopular, GnFarmacia.SubCliente, myLeer.Campo("ClaveSSA"));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargaPrecioClave()");
                General.msjError("Ocurrió un error al obtener el precio de la Clave.");
            }
            else
            {
                if (leer.Leer())
                {
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Precio, leer.CampoDouble("Precio"));
                }
                else
                {
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Precio, 0);
                }
            }
        }
        #endregion Grid           

        #region MostrarPantalla
        public void LevantaForma(string FolioRemision)
        {
            bEsConsultaExterna = true;
            txtFolio.Text = FolioRemision;            
            this.ShowDialog();            
        }
        #endregion MostrarPantalla

    } // Llaves de la Clase
} // Llaves del NameSpace
