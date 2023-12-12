using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using SC_SolutionsSystem.ExportarDatos;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using DllFarmaciaSoft;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using Farmacia.Procesos;

using DllFarmaciaSoft.Inventario;

namespace Farmacia.Pedidos
{
    public partial class FrmPedido_OrdenDeCompra : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, IdClaveSSA = 2, Descripcion = 3, Presentacion = 4, Contenido = 5, Piezas = 6, Cajas = 7
        }
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsLeer leer;
        clsGrid Grid;

        clsCodigoEAN EAN = new clsCodigoEAN();
        clsAyudas Ayuda = new clsAyudas();
        clsConsultas Consultas;

        clsImprimirPedidos PedidosImprimir;
        clsDatosCliente DatosCliente;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sRutaReportes = General.RutaDeReportes;
        string sMensaje = "", sFolioPedido = "", sTipoPedido = "";
        string sValorGrid = "";
        bool bContinua = true;
        string sIdPersonalConectado = DtGeneral.IdPersonal;
        string sMsjNoEncontrado = "";
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        OpenFileDialog openExcel = new OpenFileDialog();
        clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;
        clsLeerExcel excel;

        Thread thLoadFile;
        Thread thReadFile;
        Thread thGuardarInformacion;
        Thread thValidarInformacion;
        bool bValidandoInformacion = false;
        bool bActivarProceso = false;
        bool bErrorAlValidar = false;
        int iRegistrosHoja = 0;
        int iRegistrosProcesados = 0;
        string sFile_In = "";

        // bool bGeneraPedidoValidado = false;
        // Cols ColActiva = Cols.Ninguna;

        public FrmPedido_OrdenDeCompra()
        {
            InitializeComponent();
            myLeer = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);


            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, false);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            PedidosImprimir = new clsImprimirPedidos(General.DatosConexion, DatosCliente,
               sEmpresa, sEstado, sFarmacia, General.Url, GnFarmacia.RutaReportes, TipoReportePedido.Credito);

            grdProductos.EditModeReplace = true;
            Grid = new clsGrid(ref grdProductos, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.Limpiar(false);

            dtpFecha.Enabled = false;
            txtIdPersonal.Enabled = false;
            txtObservaciones.Enabled = false;
            txtFolio.Focus();

            sTipoPedido = Fg.PonCeros((int)TipoPedidosFarmacia.Pedido_Especial_Claves, 2);

            leerExportarExcel = new clsLeer(ref cnn);
            excel = new clsLeerExcel();
            this.Height = 615;
            FrameProceso.Top = 345;
            //FrameProceso.Left = 116;
            MostrarEnProceso(false, 0);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sIdPersonalConectado);
        }

        private void FrmPedido_OrdenDeCompra_Load(object sender, EventArgs e)
        {
            CargarTipoPedidos();
            btnNuevo_Click(null, null);
            tmPedidos.Enabled = true;
            tmPedidos.Start();
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            IniciarToolBar(false, false);
            btnCalcularPedidosPorPCM.Enabled = false;
            Grid.Limpiar(false);

            cboHojas.Clear();
            cboHojas.Add();
            cboHojas.SelectedIndex = 0;

            dtpFecha.Enabled = false;
            txtIdPersonal.Enabled = false;
            txtIdPersonal.Text = DtGeneral.IdPersonal;
            lblPersonal.Text = DtGeneral.NombrePersonal;
            txtObservaciones.Enabled = false;
            txtFolio.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bContinua = false;

            if (txtFolio.Text != "*")
            {
                General.msjUser("Este Folio ya ha sido guardado por lo tanto no puede ser modificado.");
            }
            else
            {
                if (ValidaDatos())
                {
                    if (cnn.Abrir())
                    {
                        IniciarToolBar(false, true);
                        cnn.IniciarTransaccion();

                        if (GuardaPedido())
                            bContinua = GuardaDetallePedido();

                        if (bContinua)
                        {
                            cnn.CompletarTransaccion();
                            IniciarToolBar(false, false);
                            txtFolio.Text = sFolioPedido;
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            ImprimirInformacion();
                        }
                        else
                        {
                            cnn.DeshacerTransaccion();
                            Error.GrabarError(myLeer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al guardar la información.");
                            IniciarToolBar(true, true);

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
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion();
        }

        private void btnCalcularPedidosPorPCM_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("Exec spp_Calcular_Pedido_OrdenDeCompra '{0}', '{1}', '{2}', '{3}'", sEmpresa, sEstado, sFarmacia, cboTipoPedido.Data);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "btnCalcularPedidosPorPCM_Click");
                General.msjError("Error al calcular pedidos por PCM");
            }
            else
            {
                if (myLeer.Leer())
                {
                    Grid.LlenarGrid(myLeer.DataSetClase);
                    btnCalcularPedidosPorPCM.Enabled = false;
                    cboTipoPedido.Enabled = false;
                }
                else
                {
                    General.msjAviso("No se encontro información.");
                }
            }
        }

        #endregion Botones

        #region Funciones

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "";

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                IniciarToolBar(true, false);
                txtFolio.Text = "*";
                txtFolio.Enabled = false;
                Grid.Limpiar(true);
                Grid.EstiloGrid(eModoGrid.ModoRow);
                txtIdPersonal.Text = DtGeneral.IdPersonal;
                lblPersonal.Text = DtGeneral.NombrePersonal;
                txtObservaciones.Text = "";
                txtObservaciones.Enabled = true;
                txtObservaciones.Focus();
            }
            else
            {
                sSql = string.Format(" SELECT p.FolioPedido, p.Observaciones, p.IdPersonal, p.FechaRegistro, vwp.NombreCompleto, P.TipoDeClavesDePedido " +
                     " FROM  COM_FAR_Pedidos P (nolock) " + 
                     " INNER JOIN vw_Personal vwp (nolock) " +
                     "      ON ( p.IdPersonal = vwp.IdPersonal AND p.IdEstado = vwp.IdEstado AND p.IdFarmacia = vwp.IdFarmacia ) " +
                     " WHERE p.IdEmpresa = '{0}' AND p.IdEstado = '{1}' AND p.IdFarmacia = '{2}' AND p.FolioPedido = '{3}' " +
                     " AND p.IdTipoPedido = '{4}' and p.TipoDePedido = {5} ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado,
                     DtGeneral.FarmaciaConectada, Fg.PonCeros(txtFolio.Text, 6), sTipoPedido, (int)TipoDePedido.Claves);

                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "txtFolio_Validating");
                    General.msjError("Error al buscar Folio de Pedido");
                    btnNuevo_Click(this, null);
                }
                else
                {
                    if (!myLeer.Leer())
                    {
                        General.msjUser("Folio de pedido no encontrado, verifique.");
                        btnNuevo_Click(this, null);
                    }
                    else 
                    {
                        txtFolio.Text = myLeer.Campo("FolioPedido");
                        txtObservaciones.Text = myLeer.Campo("Observaciones");
                        txtIdPersonal.Text = myLeer.Campo("IdPersonal");
                        lblPersonal.Text = myLeer.Campo("NombreCompleto");
                        dtpFecha.Value = myLeer.CampoFecha("FechaRegistro");
                        cboTipoPedido.Data = myLeer.Campo("TipoDeClavesDePedido");

                        CargaDetallePedido();

                        sFolioPedido = txtFolio.Text;
                        IniciarToolBar(false, true);
                        txtFolio.Enabled = false;
                        cboTipoPedido.Enabled = false;
                        Grid.BloqueaGrid(true);
                    }
                }
            }
        }

        private void IniciarToolBar(bool Guardar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnImprimir.Enabled = Imprimir;
            frameImportacion.Enabled = Guardar;
        }

        private bool CargaDetallePedido()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" SELECT ccs.ClaveSSA, ccs.IdClaveSSA_Sal, ccs.DescripcionClave, ccs.Presentacion, ccs.ContenidoPaquete, " +
                 " pc.CantidadPiezas, pc.Cantidad_Pedido " +
                 " FROM COM_FAR_Pedidos ( nolock ) AS p " +
                 " INNER JOIN COM_FAR_Pedidos_Claves ( nolock ) AS pc " +
                 "      ON ( p.IdEstado = pc.IdEstado And p.IdFarmacia = pc.IdFarmacia And p.FolioPedido = pc.FolioPedido " +
                 "          AND p.IdTipoPedido = pc.IdTipoPedido ) " +
                 " INNER JOIN vw_ClavesSSA_Sales ( nolock ) AS ccs " +
                 "      ON ( pc.IdClaveSSA = ccs.IdClaveSSA_Sal ) " +
                 " WHERE p.IdEmpresa = '{0}' AND p.IdEstado = '{1}' AND p.IdFarmacia = '{2}' " +
                 "  AND p.FolioPedido = '{3}' AND p.IdTipoPedido = '{4}' ",
                 DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado,
                 DtGeneral.FarmaciaConectada, Fg.PonCeros(txtFolio.Text, 6), sTipoPedido);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "CargaDetallePedido");
                General.msjError("Error al buscar el Detalle de Pedido");
                btnNuevo_Click(null, null);
            }
            else
            {
                if (myLeer.Leer())
                {
                    Grid.LlenarGrid(myLeer.DataSetClase);
                }
                else
                {
                    General.msjError("El Detalle de Pedido no Existe");
                    txtFolio.Text = "";
                    txtFolio.Focus();
                }
            }
            return bRegresa;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Pedido inválido, verifique.");
                txtFolio.Focus();
            }

            if (bRegresa)
            {
                bRegresa = validarCapturaSales();
            }

            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "El usuario no tiene permiso para realizar un pedido orden de compra, verifique por favor.";
                ////bRegresa = opPermisosEspeciales.VerificarPermisos("PEDIDO_ORDEN_DE_COMPRA", sMsjNoEncontrado);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("PEDIDO_ORDEN_DE_COMPRA", sMsjNoEncontrado);
            }

            return bRegresa;
        }

        private bool validarCapturaSales()
        {
            bool bRegresa = true;

            if (Grid.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                if (Grid.GetValue(1, (int)Cols.Descripcion) == "")
                {
                    bRegresa = false;
                }
                else
                {
                    for (int i = 1; i <= Grid.Rows; i++)
                    {
                        if (Grid.GetValue(i, (int)Cols.ClaveSSA) != "" && Grid.GetValueInt(i, (int)Cols.Cajas) == 0)
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }

            if (bRegresa)
            {
                if (Grid.TotalizarColumna((int)Cols.Cajas) == 0)
                {
                    bRegresa = false;
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos una Clave para el pedido\n y/o capturar cantidades mayor a cero, verifique.");
            }
            return bRegresa;

        }

        private bool GuardaPedido()
        {
            bool bRegresa = true;
            string sSql = "";
            sFolioPedido = txtFolio.Text;
            int iTipoPed = 0;

            iTipoPed = Convert.ToInt32(cboTipoPedido.Data);

            sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_COM_FAR_Pedidos  '{0}', '{1}', '{2}', '{3}', '{4}', '{5}','{6}', '{7}', '{8}', '{9}' ",
                    DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                    Fg.PonCeros((int)TipoPedidosFarmacia.Pedido_Especial_Claves, 2), sFolioPedido,
                    Fg.PonCeros(txtIdPersonal.Text, 4), General.FechaYMD(GnFarmacia.FechaOperacionSistema),
                    txtObservaciones.Text, (int)TipoDePedido.Claves, iTipoPed);

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (!myLeer.Leer())
                {
                    bRegresa = false;
                }
                else
                {
                    sFolioPedido = String.Format("{0}", myLeer.Campo("FolioPedido"));
                    sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                }
            }
            return bRegresa;
        }

        private bool GuardaDetallePedido()
        {
            bool bRegresa = true;
            string sSql = "", sIdClaveSSA_Sal = "";

            int iCant_Pedido = 0, iCantPiezas = 0;

            for (int i = 1; i <= Grid.Rows; i++)
            {
                sIdClaveSSA_Sal = Grid.GetValue(i, (int)Cols.IdClaveSSA);
                iCant_Pedido = Grid.GetValueInt(i, (int)Cols.Cajas);
                iCantPiezas = Grid.GetValueInt(i, (int)Cols.Piezas);

                if (sIdClaveSSA_Sal != "")
                {
                    if (iCant_Pedido > 0)
                    {
                        sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_COM_FAR_Pedidos_Claves '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                                             DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                                             sTipoPedido, sFolioPedido, sIdClaveSSA_Sal, iCant_Pedido, iCantPiezas);
                        if (!myLeer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }
            return bRegresa;
        }

        private void ImprimirInformacion()
        {
            PedidosImprimir.MostrarVistaPrevia = true;
            if (PedidosImprimir.Imprimir(sFolioPedido, sTipoPedido, TipoDePedido.Claves))
            {
                btnNuevo_Click(null, null);
            }
        }
        #endregion Funciones

        #region Grid
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            // if (Grid.ActiveCol == (int)Cols.CodigoEAN )
            {
                // Habilitar la Ayuda en todo el Renglon 
                if (e.KeyCode == Keys.F1)
                {
                    myLeer.DataSetClase = Ayuda.ClavesSSA_Sales("grdProductos_KeyDown");
                    if (myLeer.Leer())
                    {
                        Grid.SetValue(Grid.ActiveRow, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));
                        if (Validar_Clave_TipoPedido(myLeer.Campo("ClaveSSA")))
                        {
                            CargaDatosProducto();
                        }
                    }

                }
            }

            // Es posible borrar el renglon desde cualquier columna 
            if (e.KeyCode == Keys.Delete)
            {
                Grid.DeleteRow(Grid.ActiveRow);

                if (Grid.Rows == 0)
                    Grid.Limpiar(true);
            }
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            Cols iCol = (Cols)Grid.ActiveCol;

            switch (iCol)
            {
                case Cols.ClaveSSA:
                    ObtenerDatosProducto();
                    break;
            }
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = Grid.GetValue(Grid.ActiveRow, (int)Cols.ClaveSSA);
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((Grid.ActiveRow == Grid.Rows) && e.AdvanceNext)
            {
                if (Grid.GetValue(Grid.ActiveRow, 1) != "" && Grid.GetValue(Grid.ActiveRow, 3) != "")
                {
                    Grid.Rows = Grid.Rows + 1;
                    Grid.ActiveRow = Grid.Rows;
                    Grid.SetActiveCell(Grid.Rows, 1);
                }
            }
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= Grid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                Grid.SetValue(Grid.ActiveRow, i, "");
            }
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= Grid.Rows; i++) //Renglones.
            {
                if (Grid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                    Grid.DeleteRow(i);
            }

            if (Grid.Rows == 0) // Si No existen renglones, se inserta 1.
                Grid.AddRow();
        }

        private void ObtenerDatosProducto()
        {
            string sCodigo = "";  // , sSql = "";
            // int iCantidad = 0;

            sCodigo = Grid.GetValue(Grid.ActiveRow, (int)Cols.ClaveSSA);


            if (sCodigo.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.ClavesSSA_Sales(sCodigo, true, "ObtenerDatosProducto()");
                {
                    if (!myLeer.Leer())
                    {
                        General.msjUser("La Clave no Existe ó no esta Asignada a la Farmacia.");
                        Grid.LimpiarRenglon(Grid.ActiveRow);
                    }
                    else
                    {
                        if (Validar_Clave_TipoPedido(myLeer.Campo("ClaveSSA")))
                        {
                            CargaDatosProducto();
                        }
                    }
                }
            }
        }

        private void CargaDatosProducto()
        {
            int iRowActivo = Grid.ActiveRow;

            if (!Grid.BuscaRepetido(myLeer.Campo("ClaveSSA"), iRowActivo, (int)Cols.ClaveSSA))
            {
                Grid.SetValue(iRowActivo, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));
                Grid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, myLeer.Campo("IdClaveSSA_Sal"));
                //Grid.SetValue(iRowActivo, (int)Cols.CodigoEAN, myLeer.Campo("CodigoEAN"));
                Grid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
                Grid.SetValue(iRowActivo, (int)Cols.Presentacion, myLeer.Campo("Presentacion"));
                Grid.SetValue(iRowActivo, (int)Cols.Contenido, myLeer.Campo("ContenidoPaquete"));
                Grid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                Grid.SetActiveCell(iRowActivo, (int)Cols.Piezas);

            }
            else
            {
                General.msjUser("Esta Clave ya se encuentra capturada en otro renglón.");
                Grid.SetValue(Grid.ActiveRow, (int)Cols.Piezas, "");
                limpiarColumnas();
                Grid.SetActiveCell(Grid.ActiveRow, 2);
            }
        }

        private bool Validar_Clave_TipoPedido(string ClaveSSA)
        {
            bool bRegresa = true;
            bool bError = false;
            string sWhere = "", sTipoPed = "";

            sTipoPed = cboTipoPedido.Data;

            if (sTipoPed == "1")
            {
                sWhere = " and TipoDeClave = '01' ";
            }
            if (sTipoPed == "2")
            {
                sWhere = " and TipoDeClave = '02' ";
            }
            if (sTipoPed == "3")
            {
                sWhere = " and TipoDeClave = '02' and EsControlado = 1 ";
            }
            if (sTipoPed == "4")
            {
                sWhere = " and TipoDeClave = '02' and EsAntibiotico = 1 ";
            }

            clsLeer leerValidar = new clsLeer(ref cnn);
            string sSql = string.Format(" Select * From vw_ClavesSSA_Sales (NoLock) " +
                " Where ClaveSSA = '{0}'  {1} ", ClaveSSA, sWhere);

            if (!leerValidar.Exec(sSql))
            {
                bRegresa = false;
                bError = true;
                Error.GrabarError(leerValidar, "Validar_Clave_TipoPedido");
            }
            else
            {
                if (!leerValidar.Leer())
                {
                    bRegresa = false;
                    // La clave no es valida para generarle pedidos 
                }
            }

            if (bError)
            {
                General.msjError("Ocurrió un error al validar la clave para la generación de pedidos.");
            }
            else
            {
                if (!bRegresa)
                {
                    General.msjAviso(string.Format("La Clave {0} no es valida para el tipo de pedido seleccionado.", ClaveSSA));
                    Grid.LimpiarRenglon(Grid.ActiveRow);
                }
            }

            return bRegresa;
        }
        #endregion Grid

        private void tmPedidos_Tick(object sender, EventArgs e)
        {
            tmPedidos.Stop();
            tmPedidos.Enabled = false;
            ////if (!GnFarmacia.GeneraPedidosEspeciales)
            ////{
            ////    General.msjUser("La Farmacia no esta configurada para Generar Pedidos Especiales.");
            ////    this.Close();
            ////}
        }

        #region Importacion

        private void IniciaToolBar2(bool Abrir, bool Ejecutar, bool Guardar2, bool Validar)
        {
            btnAbrir.Enabled = Abrir;
            btnEjecutar.Enabled = Ejecutar;
            btnGuardar2.Enabled = Guardar2;
            btnValidarDatos.Enabled = Validar;
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            openExcel.Title = "Archivos de inventario";
            openExcel.Filter = "Archivos de Excel (*.xls;*.xlsx)| *.xls;*.xlsx";
            openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openExcel.AddExtension = true;
            //lblProcesados.Visible = false; /t

            if (openExcel.ShowDialog() == DialogResult.OK)
            {
                sFile_In = openExcel.FileName;

                IniciaToolBar2(false, false, false, false);
                thReadFile = new Thread(this.CargarArchivo);
                thReadFile.Name = "LeerArchivo";
                thReadFile.Start();
            }
        }

        private void CargarArchivo()
        {
            string sHoja = "";
            bool bHabilitar = false;

            BloqueaHojas(true);
            MostrarEnProceso(true, 1);

            excel = new clsLeerExcel(sFile_In);
            excel.GetEstructura();

            cboHojas.Clear();
            cboHojas.Add();
            Grid.Limpiar(false);
            Thread.Sleep(1000);

            bHabilitar = excel.Hojas.Registros > 0;
            while (excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja");
                cboHojas.Add(sHoja, sHoja);
            }

            cboHojas.SelectedIndex = 0;
            btnEjecutar.Enabled = bHabilitar;
            IniciaToolBar2(true, bHabilitar, false, false);

            BloqueaHojas(false);
            MostrarEnProceso(false, 1);
        }

        private void BloqueaHojas(bool Bloquear)
        {
            cboHojas.Enabled = !Bloquear;
        }

        private void MostrarEnProceso(bool Mostrar, int Proceso)
        {
            string sTituloProceso = "";

            if (Mostrar)
            {
                FrameProceso.Left = 116;
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
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
                sTituloProceso = "Integrando inventario ..... ";
            }

            FrameProceso.Text = sTituloProceso;

        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            thLoadFile = new Thread(this.thLeerHoja);
            thLoadFile.Name = "LeerDocumentoExcel";
            thLoadFile.Start();
        }

        private void thLeerHoja()
        {
            BloqueaHojas(true);
            MostrarEnProceso(true, 2);

            LeerHoja();

            BloqueaHojas(false);
            MostrarEnProceso(false, 2);
        }

        private bool LeerHoja()
        {
            bool bRegresa = false;

            IniciaToolBar2(false, false, bRegresa, false);
            Grid.Limpiar();
            excel.LeerHoja(cboHojas.Data);

            if (excel.Leer())
            {
                bRegresa = true;
                iRegistrosHoja = excel.Registros;
                //myGrid.LlenarGrid(excel.DataSetClase);
            }

            IniciaToolBar2(true, true, bRegresa, false);
            return bRegresa;
        }

        private void btnGuardar2_Click(object sender, EventArgs e)
        {
            string[] SCols = { "ClaveSSA", "ClaveSSA Base", "Descripcion Sal", "Presentacion", "ContenidoPaquete", "CantidadPiezas" };
            if (excel.ValidarExistenCampos(SCols))
            {
                thGuardarInformacion = new Thread(this.GuardarInformacion);
                thGuardarInformacion.Name = "Guardar información seleccionada";
                thGuardarInformacion.Start();
            }
            else
            {
                General.msjAviso("No se encontraron todas las columnas requeridas en la plantilla para la integración del inventario, verifique. ");
            }
        }

        private void GuardarInformacion()
        {
            bool bRegresa = false;
            string sSql = "";
            clsLeer leerGuardar = new clsLeer(ref cnn);

            BloqueaHojas(true);
            MostrarEnProceso(true, 3);
            IniciaToolBar2(false, false, false, false);


            // leerGuardar.DataSetClase = excel.DataSetClase;
            excel.RegistroActual = 1;
            bRegresa = excel.Registros > 0;
            iRegistrosProcesados = 0;

            leerGuardar.Exec("Truncate Table Registro_Pedidos_CargaMasiva ");
            while (excel.Leer())
            {
                sSql = string.Format("Insert Into Registro_Pedidos_CargaMasiva " +
                    " ( ClaveSSA, DescripcionClaveSSA, Presentacion, ContenidoPaquete, CantidadPzas ) \n");
                sSql += string.Format("Select '{0}', '{1}', '{2}', '{3}', '{4}'  ",
                    DarFormato(excel.Campo("ClaveSSA")), DarFormato(excel.Campo("Descripcion Sal")),
                    DarFormato(excel.Campo("Presentacion")), DarFormato(excel.Campo("ContenidoPaquete ")),
                    excel.Campo("CantidadPiezas") == "" ? "0" : excel.Campo("CantidadPiezas"));

                if (!leerGuardar.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leerGuardar, "GuardarInformacion()");
                    break;
                }
                iRegistrosProcesados++;
            }

            BloqueaHojas(false);
            MostrarEnProceso(false, 3);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar la información del inventario.");
                IniciaToolBar2(true, true, true, false);
            }
            else
            {
                leerGuardar.Exec("Exec spp_FormatearTabla 'Registro_Pedidos_CargaMasiva'  ");
                General.msjUser("Información de inventario cargada satisfactoriamente.");
                IniciaToolBar2(true, true, false, true);
            }
        }

        private string DarFormato(string Valor)
        {
            string sRegresa = Valor.Trim();

            sRegresa = sRegresa.Replace("'", "");
            sRegresa = sRegresa.Replace(",", "");

            return sRegresa;
        }

        private void btnValidarDatos_Click(object sender, EventArgs e)
        {
            tmValidacion.Enabled = true;
            tmValidacion.Interval = 1000;
            tmValidacion.Start();

            thValidarInformacion = new Thread(this.ValidarInformacion);
            thValidarInformacion.Name = "Validar informacion";
            thValidarInformacion.Start();
            System.Threading.Thread.Sleep(200);
        }

        private void ValidarInformacion()
        {

            bValidandoInformacion = true;
            bActivarProceso = false;
            bErrorAlValidar = false;
            clsLeer leerValidacion = new clsLeer();

            IniciaToolBar2(false, false, false, false);
            BloqueaHojas(true);
            MostrarEnProceso(true, 4);

            string sTipoPed = "";

            sTipoPed = cboTipoPedido.Data;

            string sSql = string.Format("Exec sp_Proceso_Registro_Pedidos_CargaMasiva_000_Validar_Datos_De_Entrada '{0}', '{1}', '{2}', '{3}'",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sTipoPed);


            if (!leer.Exec(sSql))
            {
                bErrorAlValidar = true;
                bActivarProceso = !bActivarProceso;

                Error.GrabarError(leer, "ValidarInformacion()");
                General.msjError("Ocurrió un error al verificar el inventario a integrar.");
            }
            else
            {

                leer.RenombrarTabla(1, "Clave");
                //leer.RenombrarTabla(2, "Costo");
                //leer.RenombrarTabla(3, "Cantidad");
                //leer.RenombrarTabla(4, "Iva");
                //leer.RenombrarTabla(5, "Descripcion");

                leerValidacion.DataTableClase = leer.Tabla(1); //Clave
                bActivarProceso = leer.Registros > 0;

                //if (!bActivarProceso)
                //{
                //    leerValidacion.DataTableClase = leer.Tabla(2);   // Costo 
                //    bActivarProceso = leerValidacion.Registros > 0;
                //}

                //if (!bActivarProceso)
                //{
                //    leerValidacion.DataTableClase = leer.Tabla(3);   // Cantidad  
                //    bActivarProceso = leerValidacion.Registros > 0;
                //}

                //if (!bActivarProceso)
                //{
                //    leerValidacion.DataTableClase = leer.Tabla(4);   // Iva  
                //    bActivarProceso = leerValidacion.Registros > 0;
                //}

                //if (!bActivarProceso)
                //{
                //    leerValidacion.DataTableClase = leer.Tabla(5);   // Descripcion
                //    bActivarProceso = leerValidacion.Registros > 0;
                //}

                if (!bActivarProceso)
                {
                    sSql = string.Format("Select ClaveSSA, IdClaveSSA, DescripcionClaveSSA, Presentacion, ContenidoPaquete, CantidadPzas " +
                            "From Registro_Pedidos_CargaMasiva " +
                            "Where CantidadPzas > 0 ");

                    if (leer.Exec(sSql))
                    {
                        if (leer.Leer())
                        {
                            Grid.LlenarGrid(leer.DataSetClase);
                        }
                    }

                }
            }

            bValidandoInformacion = false;
            bActivarProceso = !bActivarProceso;
            BloqueaHojas(false);
            MostrarEnProceso(false, 4);
        }

        private void tmValidacion_Tick(object sender, EventArgs e)
        {
            tmValidacion.Stop();
            tmValidacion.Enabled = false;

            if (!bValidandoInformacion)
            {
                if (bActivarProceso)
                {
                    IniciaToolBar2(true, true, false, false);
                }
                else
                {
                    IniciaToolBar2(true, true, false, true);
                    if (!bErrorAlValidar)
                    {
                        FrmIncidencias f = new FrmIncidencias(leer.DataSetClase);
                        f.ShowDialog();
                    }
                }
            }
            else
            {
                tmValidacion.Enabled = true;
                tmValidacion.Start();
            }
        }

        #endregion Importacion

        #region Cargar_Combo_TipoPedidos
        private void CargarTipoPedidos()
        {
            cboTipoPedido.Clear();
            cboTipoPedido.Add();

            cboTipoPedido.Add("1", "Material de Curación");
            cboTipoPedido.Add("2", "Medicamentos Generales");
            cboTipoPedido.Add("3", "Medicamentos Controlados");
            cboTipoPedido.Add("4", "Medicamentos Antibióticos");

            cboTipoPedido.SelectedIndex = 0;
        }
        #endregion Cargar_Combo_TipoPedidos

        #region Eventos_Combo_Pedido
        private void cboTipoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoPedido.SelectedIndex != 0)
            {
                //cboTipoPedido.Enabled = false;
                Grid.Limpiar(true);
                btnCalcularPedidosPorPCM.Enabled = true;
            }
        }

        private void cboTipoPedido_Validating(object sender, CancelEventArgs e)
        {
            if (cboTipoPedido.SelectedIndex != 0)
            {
                cboTipoPedido.Enabled = false;
            }
        }
        #endregion Eventos_Combo_Pedido

        private void frameImportacion_Enter(object sender, EventArgs e)
        {

        }
       
    }
}
