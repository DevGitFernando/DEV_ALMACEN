using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid; 
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.OfficeOpenXml;
using SC_SolutionsSystem.OfficeOpenXml.Data;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Ayudas;
using DllFarmaciaSoft.ExportarExcel; 
using Almacen.wsAlmacen;

using Farmacia.Catalogos;
using Farmacia.Ventas;

namespace Almacen.PedidosEspeciales
{
    public partial class FrmRegistroPedidosEspecialesVenta : FrmBaseExt 
    {
        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, IdClaveSSA = 2, Descripcion = 3, Presentacion = 4, Existencia = 5, Cantidad = 6, ContenidoPaquete = 7, CantidadEnCajas = 8
        }

        clsDatosConexion DatosDeConexion; 
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnRegional;
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsLeer leer;
        clsLeer leer2;
        clsConsultas Consultas;
        clsAyudas ayuda;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;
        clsLeer leerPedido;
        clsLeerWebExt leerWeb;


        string sIdPublicoGral = GnFarmacia.PublicoGral;
        string sIdSeguroPopular = GnFarmacia.SeguroPopular;
        bool bEsSeguroPopular = false;
        DataSet dtsFarmacias = new DataSet();

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
        string sFolioPedido = "", sMensaje = "", sValorGrid = "";
        bool bFolio_Guardado = false;
        bool bEsActivo = false;
        public bool bVigenciaValida = true;
        private bool bValidarBeneficariosAlertas = GnFarmacia.ValidarBeneficariosAlertas;
        FrmHelpBeneficiarios helpBeneficiarios;

        bool bPermitirCapturaBeneficiariosNuevos = false;
        bool bImportarBeneficiarios = false;

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PEDIDOS_COMPRAS.xls";
        OpenFileDialog openExcel = new OpenFileDialog();
        clsLeerExcelOpenOficce excel;
        clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;
        clsLeer leerExportarPlantilla_Excel;
        string sFile_In = "";
        bool bValidandoInformacion = false;
        bool bActivarProceso = false;
        bool bErrorAlValidar = false;
        int iRegistrosHoja = 0;
        int iRegistrosProcesados = 0;

        Thread thLoadFile;
        Thread thReadFile;
        Thread thGuardarInformacion;
        Thread thValidarInformacion;

        //DataSet para ejecutar los pedidos de distribuidor en Regional
        DataSet dtsPedido = new DataSet();
        string sUrlAlmacenCEDIS = GnFarmacia.UrlAlmacenCEDIS;
        string sHostAlmacenCEDIS = GnFarmacia.HostAlmacenCEDIS;
        string sIdFarmaciaAlmacenCEDIS = GnFarmacia.IdFarmaciaAlmacenCEDIS;

        string sFormato_INT = "###, ###, ###, ##0";
        wsAlmacen.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        public FrmRegistroPedidosEspecialesVenta()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Almacen.wsAlmacen.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            leerExportarExcel = new clsLeer(ref ConexionLocal);
            leerExportarPlantilla_Excel = new clsLeer(ref ConexionLocal);
            leerWeb = new clsLeerWebExt(General.Url, General.ArchivoIni, DatosCliente);
            leer = new clsLeer(ref ConexionLocal);
            leer2 = new clsLeer(ref ConexionLocal);

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White;
            myGrid.AjustarAnchoColumnasAutomatico = true; 

            CargarTipoPedidos();

            MostrarEnProceso(false, 0); 
        }

        private void FrmRegistroPedidosEspecialesVenta_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Limpiar 
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;

            btnImportarPedidoMasivo.Enabled = Guardar;
            btnActualizarReferencia.Enabled = !Guardar;
        }

        private void IniciaToolBar2(bool Abrir, bool Ejecutar, bool Guardar, bool Validar)
        {
            btnAbrir.Enabled = Abrir;
            btnEjecutar.Enabled = Ejecutar;
            btnGuardar_CargaMasiva.Enabled = Guardar;
            btnValidarDatos.Enabled = Validar;
        }

        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            IniciarToolBar(false, false, false);
            IniciaToolBar2(true, false, false, false);

            chkCajasCompletas.Checked = false;

            cboHojas.Clear();
            cboHojas.Add();
            cboHojas.SelectedIndex = 0;


            bFolio_Guardado = false; 
            lblStatus.Text = "CANCELADA"; //Se pone aqui ya que el IniciaControles le borra el texto.
            lblStatus.Visible = false;
            //lblStatus.Text = "";
            //lblStatus.Visible = false;
            //rdoTransferencia.Checked = false;
            //rdoVenta.Checked = false;
            //rdoTransferencia.Enabled = true;
            //rdoVenta.Enabled = true;

            myGrid.Limpiar(false);
            grdProductos.Enabled = false;
            dtpFechaRegistro.Enabled = false;
            lblTituloHoja.BackColor = toolStripBarraImportacion.BackColor;

            dtpFechaEntrega.MaxDate = General.FechaSistemaObtener().AddMonths(1);
            dtpFechaEntrega.MinDate = General.FechaSistemaObtener();
            dtpFechaEntrega.Value = General.FechaSistemaObtener();


            //cboFarmacias.Enabled = false;
            //cboJurisdicciones.Enabled = false;
            //dtpFechaRegistro.Value = GnFarmacia.FechaOperacionSistema;
            //dtpFechaRecepcion.MaxDate = GnFarmacia.FechaOperacionSistema;

            dtsPedido = null;
            lblCanceladoBen.Visible = false;
            lblCanceladoBen.Text = "CANCELADO";
            txtIdBenificiario.Enabled = false;
            txtFolio.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }        
        #endregion Limpiar

        #region Menu de Pedidos
        private void CargarTipoPedidos()
        {
            cboTipoPedido.Clear();
            cboTipoPedido.Add();

            cboTipoPedido.Add("1", "Material de Curación");
            cboTipoPedido.Add("2", "Medicamentos Generales");
            cboTipoPedido.Add("3", "Medicamentos Controlados");
            cboTipoPedido.Add("4", "Medicamentos Antibióticos");
            cboTipoPedido.Add("5", " Todos ");

            cboTipoPedido.SelectedIndex = 0;
        }

        private void cboTipoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoPedido.SelectedIndex != 0)
            {
                //cboTipoPedido.Enabled = false;
                myGrid.Limpiar(true);
                grdProductos.Enabled = true;
                IniciarToolBar(true, false, false);
            }
        }

        private void cboTipoPedido_Validating(object sender, CancelEventArgs e)
        {
            if (cboTipoPedido.SelectedIndex != 0)
            {
                cboTipoPedido.Enabled = false;
            }
        }

        private void btnExportarPlantilla_Click(object sender, EventArgs e)
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            string sWhere = "  ", sTipoPed = "";
            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PEDIDOS_COMPRAS_CARGA.xls";
            bool bTieneDatos = false;
            int iRenglon = 1;
            int iColBase = 2;
            string sNombreHoja = "PLANTILLA";

            sTipoPed = cboTipoPedido.Data;
            if (sTipoPed == "1")
            {
                sWhere = " and C.IdTipoProducto = '01' ";
            }

            if (sTipoPed == "2")
            {
                sWhere = " and C.IdTipoProducto = '02' ";
            }

            if (sTipoPed == "3")
            {
                sWhere = " and C.IdTipoProducto = '02' and C.EsControlado = 1 ";
            }

            if (sTipoPed == "4")
            {
                sWhere = " and C.IdTipoProducto = '02' and C.EsAntibiotico = 1 ";
            }

            if (sTipoPed == "5")
            {
                sWhere = " ";
            }
            //             string[] SCols = { "ClaveSSA", "ClaveSSA Base", "Descripcion Clave", "Presentacion", "ContenidoPaquete", "CantidadPiezas" };
            string sSql = string.Format(
                    "Select '{0}' as IdEstado, '{1}' as Estado, '{2}' as IdFarmacia, '{3}' as Farmacia, \n" +
                    "(char(39) + C.ClaveSSA_Aux + char(39)) As ClaveSSA, 'ClaveSSA Base' = (char(39) + C.ClaveSSA_Base + char(39)), \n" +
                    "'Descripcion Clave' = C.DescripcionClave, C.Presentacion, C.ContenidoPaquete, 0 as CantidadPiezas \n" +
                    "From vw_ClavesSSA_Sales C (NoLock) \n" +
                    "Where 1 = 1   \n{4} ", sEstado, DtGeneral.EstadoConectadoNombre, sFarmacia, DtGeneral.FarmaciaConectadaNombre, sWhere);

            if (!leerExportarPlantilla_Excel.Exec(sSql))
            {
                Error.GrabarError(leerExportarPlantilla_Excel, "");
                General.msjError("Ocurrió un error al obtener los datos de plantilla para pedido.");
            }
            else
            {
                bTieneDatos = leerExportarPlantilla_Excel.Registros > 0 ? true : false;

                if (!bTieneDatos)
                {
                    General.msjAviso("No se encontro información para generar la plantilla para pedido");
                }
                else
                {
                    string sNombreFile = "PEDIDO_ESPECIAL_VENTAS____CEDIS_" + sFarmacia;

                    this.Cursor = Cursors.WaitCursor;
                    bool bRegresa = true; // DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PEDIDOS_COMPRAS_CARGA.xls", DatosCliente);

                    generarExcel = new clsGenerarExcel();
                    generarExcel.RutaArchivo = @"C:\\Excel";
                    generarExcel.NombreArchivo = sNombreFile;
                    generarExcel.AgregarMarcaDeTiempo = true;

                    if(generarExcel.PrepararPlantilla(sNombreFile))
                    {
                        generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                        iRenglon = 1;
                        //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                        generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leerExportarPlantilla_Excel.DataSetClase);

                        //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                        generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                        generarExcel.CerraArchivo();

                        generarExcel.AbrirDocumentoGenerado(true);
                    }


                    this.Cursor = Cursors.Default;

                    ////if (!bRegresa)
                    ////{
                    ////    this.Cursor = Cursors.Default;
                    ////    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
                    ////}
                    ////else
                    ////{
                    ////    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                    ////    xpExcel.AgregarMarcaDeTiempo = true;

                    ////    if (xpExcel.PrepararPlantilla(sNombreFile))
                    ////    {
                    ////        xpExcel.GeneraExcel(true);
                    ////        xpExcel.NumeroDeRenglonesAProcesar = leerExportarPlantilla_Excel.Registros > 0 ? leerExportarPlantilla_Excel.Registros : -1;

                    ////        leerExportarPlantilla_Excel.RegistroActual = 1;
                    ////        for (int iRow = 2; leerExportarPlantilla_Excel.Leer(); iRow++)
                    ////        {
                    ////            xpExcel.Agregar(leerExportarPlantilla_Excel.Campo("ClaveSSA"), iRow, 1);
                    ////            xpExcel.Agregar(leerExportarPlantilla_Excel.Campo("ClaveSSA_Base"), iRow, 2);
                    ////            xpExcel.Agregar(leerExportarPlantilla_Excel.Campo("DescripcionClave"), iRow, 3);
                    ////            xpExcel.Agregar(leerExportarPlantilla_Excel.Campo("Presentacion"), iRow, 4);
                    ////            xpExcel.Agregar(leerExportarPlantilla_Excel.Campo("ContenidoPaquete"), iRow, 5);
                    ////            xpExcel.Agregar(0, iRow, 6);

                    ////            xpExcel.NumeroRenglonesProcesados++;
                    ////        }

                    ////        xpExcel.CerrarDocumento();

                    ////        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    ////        {
                    ////            xpExcel.AbrirDocumentoGenerado();
                    ////        }
                    ////    }
                    ////    this.Cursor = Cursors.Default;
                    ////}
                }
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            openExcel.Title = "Archivos de pedido";
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

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            thLoadFile = new Thread(this.thLeerHoja);
            thLoadFile.Name = "LeerDocumentoExcel";
            thLoadFile.Start();
        }

        private void btnGuardar_CargaMasiva_Click(object sender, EventArgs e)
        {
            string[] SCols = { "ClaveSSA", "ClaveSSA Base", "Descripcion Clave", "Presentacion", "ContenidoPaquete", "CantidadPiezas" };
            if (excel.ValidarExistenCampos(SCols))
            {
                thGuardarInformacion = new Thread(this.GuardarInformacion);
                thGuardarInformacion.Name = "Guardar información seleccionada";
                thGuardarInformacion.Start();
            }
            else
            {
                General.msjAviso("No se encontraron todas las columnas requeridas en la plantilla para la integración del pedido, verifique. ");
            }
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

        private void tmValidacion_Tick(object sender, EventArgs e)
        {

        }

        private void CargarArchivo()
        {
            string sHoja = "";
            bool bHabilitar = false;

            BloqueaHojas(true);
            MostrarEnProceso(true, 1);

            excel = new clsLeerExcelOpenOficce(sFile_In);
            excel.GetEstructura();

            cboHojas.Clear();
            cboHojas.Add();
            myGrid.Limpiar(false);
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
                FrameProceso.Left = 180;
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
            myGrid.Limpiar();
            excel.LeerHoja(cboHojas.Data);

            if (excel.Leer())
            {
                cboHojas.Enabled = false;
                bRegresa = true;
                iRegistrosHoja = excel.Registros;
                //myGrid.LlenarGrid(excel.DataSetClase);
            }

            IniciaToolBar2(true, true, bRegresa, false);
            return bRegresa;
        }

        private void GuardarInformacion()
        {
            bool bRegresa = false;
            string sSql = "";
            clsLeer leerGuardar = new clsLeer(ref ConexionLocal);

            BloqueaHojas(true);
            MostrarEnProceso(true, 3);
            IniciaToolBar2(false, false, false, false);


            // leerGuardar.DataSetClase = excel.DataSetClase;
            excel.RegistroActual = 1;
            bRegresa = excel.Registros > 0;
            iRegistrosProcesados = 0;

            ////leerGuardar.Exec("Truncate Table Registro_Pedidos_CargaMasiva ");
            ////while (excel.Leer())
            ////{
            ////    sSql = string.Format("Insert Into Registro_Pedidos_CargaMasiva " +
            ////        " ( ClaveSSA, DescripcionClaveSSA, Presentacion, ContenidoPaquete, CantidadPzas ) \n");
            ////    sSql += string.Format("Select '{0}', '{1}', '{2}', '{3}', '{4}'  ",
            ////        DarFormato(excel.Campo("ClaveSSA")), DarFormato(excel.Campo("Descripcion Sal")),
            ////        DarFormato(excel.Campo("Presentacion")), DarFormato(excel.Campo("ContenidoPaquete")),
            ////        excel.Campo("CantidadPiezas") == "" ? "0" : excel.Campo("CantidadPiezas"));

            ////    if (!leerGuardar.Exec(sSql))
            ////    {
            ////        bRegresa = false;
            ////        Error.GrabarError(leerGuardar, "GuardarInformacion()");
            ////        break;
            ////    }
            ////    iRegistrosProcesados++;
            ////}

            clsBulkCopy bulk = new clsBulkCopy(General.DatosConexion);

            bulk.NotifyAfter = 500;
            bulk.RowsCopied += new clsBulkCopy.RowsCopiedEventHandler(bulk_RowsCopied);
            bulk.OnCompled += new clsBulkCopy.RowsCopiedEventHandler(bulk_Compled);
            bulk.OnError += new clsBulkCopy.RowsCopiedEventHandler(bulk_Error);

            bulk.ClearColumns();
            bulk.DestinationTableName = "Registro_Pedidos_CargaMasiva";


            if(excel.ExisteTablaColumna(1, "ClaveSSA".ToUpper()))
            {
                bulk.AddColumn("ClaveSSA", "ClaveSSA");
            }

            if(excel.ExisteTablaColumna(1, "Descripcion Clave".ToUpper()))
            {
                bulk.AddColumn("Descripcion Clave", "DescripcionClaveSSA");
            }

            if(excel.ExisteTablaColumna(1, "Presentacion".ToUpper()))
            {
                bulk.AddColumn("Presentacion", "Presentacion");
            }

            if(excel.ExisteTablaColumna(1, "ContenidoPaquete".ToUpper()))
            {
                bulk.AddColumn("ContenidoPaquete", "ContenidoPaquete");
            }

            if(excel.ExisteTablaColumna(1, "CantidadPiezas".ToUpper()))
            {
                bulk.AddColumn("CantidadPiezas", "CantidadPzas");
            }

            leerGuardar.Exec("Truncate table Registro_Pedidos_CargaMasiva ");
            bRegresa = bulk.WriteToServer(excel.DataSetClase); //, System.Data.SqlClient.SqlBulkCopyOptions.Default); 

            BloqueaHojas(false);
            MostrarEnProceso(false, 3);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar la información del pedido.");
                IniciaToolBar2(true, true, true, false);
            }
            else
            {
                leerGuardar.Exec("Exec spp_FormatearTabla 'Registro_Pedidos_CargaMasiva'  ");
                //General.msjUser("Información de inventario cargada satisfactoriamente.");
                IniciaToolBar2(true, true, false, true);
            }
        }

        private void bulk_RowsCopied( RowsCopiedEventArgs e )
        {
            lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Compled( RowsCopiedEventArgs e )
        {
            lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Error( RowsCopiedEventArgs e )
        {
            lblProcesados.Text = string.Format("Ocurrio un error al procesar {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
            Error.GrabarError(e.Error, "bulk_Error");
        }

        private string DarFormato(string Valor)
        {
            string sRegresa = Valor.Trim();

            sRegresa = sRegresa.Replace("'", "");
            sRegresa = sRegresa.Replace(",", "");

            return sRegresa;
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

            string sSql = string.Format("Exec sp_Proceso_Registro_Pedidos_CargaMasiva_000_Validar_Datos_De_Entrada " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TipoPedido = '{3}' ",
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
                    sSql = string.Format("Select ClaveSSA, IdClaveSSA, DescripcionClaveSSA, Presentacion, Existencia, " +
                            "CantidadPzas, ContenidoPaquete, CantidadCajas, Existencia " +
                            "From Registro_Pedidos_CargaMasiva " +
                            "Where CantidadPzas > 0 ");

                    if (leer.Exec(sSql))
                    {
                        if (leer.Leer())
                        {
                            grdProductos.Enabled = true;
                            myGrid.LlenarGrid(leer.DataSetClase);
                        }
                    }
                }
            }

            bValidandoInformacion = false;
            bActivarProceso = !bActivarProceso;
            BloqueaHojas(false);
            MostrarEnProceso(false, 4);
        }
        #endregion Menu de Pedidos

        #region Buscar Folio 
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = false;
            IniciarToolBar(false, false, false);

            myLeer = new clsLeer(ref ConexionLocal);
            bFolio_Guardado = false; 

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                IniciarToolBar(true, false, false);
            }
            else
            {
                myLeer.DataSetClase = Consultas.Folio_Pedidos_CEDIS_Enc(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
                if (myLeer.Leer()) 
                {
                    bContinua = true;
                    bFolio_Guardado = true;
                    if (myLeer.CampoBool("EsTransferencia"))
                    {
                        General.msjAviso("El Folio " + myLeer.Campo("Folio") + " es un pedido tipo transferencia.");
                        txtFolio.Text = "";
                        bContinua = false;
                    }
                    else
                    {
                        CargaEncabezadoFolio();
                    }
                }

                if (bContinua)
                {
                    bContinua = CargaDetallesFolio(); 
                }
            }

            if (!bContinua)
            {
                txtFolio.Focus();
            }
        }

        private void CargaEncabezadoFolio()
        {            
            //Se hace de esta manera para la ayuda.
            txtFolio.Text = myLeer.Campo("Folio");
            sFolioPedido = txtFolio.Text;
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");

            dtpFechaEntrega.MinDate = dtpFechaEntrega.MinDate.AddYears(-10);
            dtpFechaEntrega.MaxDate = dtpFechaEntrega.MaxDate.AddYears(10);
            dtpFechaEntrega.Value = myLeer.CampoFecha("FechaEntrega");


            txtCte.Text = myLeer.Campo("IdCliente");
            lblCte.Text = myLeer.Campo("NombreCliente");
            //txtCte_Validating(this, null);
            txtSubCte.Text = myLeer.Campo("IdsubCliente");
            lblSubCte.Text = myLeer.Campo("NombreSubCliente");
            //txtSubCte_Validating(this, null);
            txtPro.Text = myLeer.Campo("IdPrograma");
            lblPro.Text = myLeer.Campo("Programa");
            //txtPro_Validating(this, null);
            txtSubPro.Text = myLeer.Campo("IdSubPrograma");
            lblSubPro.Text = myLeer.Campo("SubPrograma");
            //txtSubPro_Validating(this, null);
            txtIdBenificiario.Text = myLeer.Campo("IdBeneficiario");
            lblNombre.Text = myLeer.Campo("Beneficiario");
            lblFechaVigencia.Text = General.FechaYMD(leer.CampoFecha("FechaFinVigencia"), "-");
            lblFolioReferencia.Text = myLeer.Campo("FolioReferencia");
            txtObservaciones.Text = myLeer.Campo("Observaciones");
            txtReferenciaPedido.Text = myLeer.Campo("ReferenciaInternaPedido");

            ////// Se bloquea el encabezado del Folio 
            Fg.BloqueaControles(this, false, FrameEncabezado);
            //Fg.BloqueaControles(this, false, FrameTipoAtencion); 

            ////if ( myLeer.CampoInt("StatusPedido") != 0)
            ////    IniciarToolBar(false, false, true);
            ////else
            ////    IniciarToolBar(false, true, true);

            IniciarToolBar(false, false, true); 
            if (myLeer.Campo("Status") == "C")
            {   
                IniciarToolBar(false, false, true);
                lblStatus.Text = "CANCELADO";
                lblStatus.Visible = true; 
            }
        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = false;

            myLlenaDatos.DataSetClase = Consultas.Folio_Pedidos_CEDIS_Det(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
            if (myLlenaDatos.Leer())
            {
                bRegresa = true; 
                myGrid.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
            }

            // Bloquear grid completo 
            myGrid.BloqueaRenglon(true);

            return bRegresa;
        } 
        #endregion Buscar Folio
        
        #region Guardar Informacion 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;
            SendKeys.Send("{TAB}");

            if (txtFolio.Text != "*")
            {
                MessageBox.Show("Este Folio ya ha sido guardado por lo tanto no puede ser modificado");
            }
            else
            {
                EliminarRenglonesVacios();
                if (ValidaDatos())
                {
                    if (!ConexionLocal.Abrir())
                    {
                        General.msjErrorAlAbrirConexion(); 
                    }
                    else
                    {
                        ConexionLocal.IniciarTransaccion();

                        bContinua = GrabarEncabezado();

                        if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                        {
                            txtFolio.Text = sFolioPedido;
                            ConexionLocal.CompletarTransaccion();

                            //////if (GrabarPedidoDistribuidorRegional())
                            //////{
                            //////    ActualizarPedidosDistribuidor();
                            //////}

                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            IniciarToolBar(false, false, true);
                            Imprimir(true);
                            btnNuevo_Click(this, null);
                        }
                        else
                        {
                            Error.GrabarError(myLeer, "btnGuardar_Click");
                            ConexionLocal.DeshacerTransaccion(); 
                            General.msjError("Ocurrió un error al guardar la Información.");

                        }

                        ConexionLocal.Cerrar();
                    }
                }
            }
        }

        private bool GrabarEncabezado()
        {
            bool bRegresa = true;
            string sQuery = "";
            string sSql = "";
            int bEsTransferencia = 0;//rdoTransferencia.Checked;
            int iTipoPed = Convert.ToInt32("0" + cboTipoPedido.Data);
            int iCajaCompleta = chkCajasCompletas.Checked ? 1 : 0;
            int TipoPedido = 3;

            sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Enc  " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " +
                " @IdEstadoSolicita = '{3}', @IdFarmaciaSolicita = '{4}', @FolioPedido = '{5}', @IdPersonal = '{6}', " +
                " @Observaciones = '{7}', @Status = '{8}', @EsTransferencia = '{9}', " +
                " @Cliente = '{10}', @SubCliente = '{11}', @Programa = '{12}', @SubPrograma = '{13}', @PedidoNoAdministrado = '{14}', " +
                " @TipoDeClavesDePedido = '{15}', @ReferenciaPedido = '{16}', @FechaEntrega = '{17}', @IdBeneficiario = '{18}', @CajasCompletas = '{19}', @TipoPedido = {20} ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text.Trim(), DtGeneral.IdPersonal, 
                txtObservaciones.Text, "A", bEsTransferencia,
                txtCte.Text, txtSubCte.Text, txtPro.Text, txtSubPro.Text, 1, iTipoPed, txtReferenciaPedido.Text.Trim(), 
                General.FechaYMD(dtpFechaEntrega.Value, "-"), txtIdBenificiario.Text.Trim(), iCajaCompleta, TipoPedido
                );


            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolioPedido = myLeer.Campo("Folio");
                sMensaje = myLeer.Campo("Mensaje");

                sQuery = sSql;
                sQuery = sQuery.Replace("*", sFolioPedido);

                bRegresa = GrabarDetalle(); 
            }

            return bRegresa;
        }

        private bool GrabarDetalle()
        {
            bool bRegresa = true;
            string sSql = "", sQuery = "";
            string sIdClaveSSA = "", sClaveSSA = "";
            int iCantidad = 0, iContenidoPaquete = 0, iCantidadEnCajas = 0; // , iOpcion = 1;
            int iExistencia = 0; 

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdClaveSSA = myGrid.GetValue(i, (int)Cols.IdClaveSSA);
                sClaveSSA = myGrid.GetValue(i, (int)Cols.ClaveSSA);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                iExistencia = myGrid.GetValueInt(i, (int)Cols.Existencia); 
                iCantidadEnCajas = myGrid.GetValueInt(i, (int)Cols.CantidadEnCajas);
                iContenidoPaquete = myGrid.GetValueInt(i, (int)Cols.ContenidoPaquete);

                if (chkCajasCompletas.Checked)
                {
                    iCantidad = iCantidadEnCajas * iContenidoPaquete;
                }

                if (sIdClaveSSA != "")
                {
                    sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Det " + 
                        " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioPedido = '{3}', @IdClaveSSA = '{4}', " + 
                        " @Existencia = '{5}', @CantidadSolicitada = '{6}', @CantidadEnCajas = '{7}', @ClaveSSA = '{8}', " + 
                        " @ContenidoPaquete = '{9}', @ExistenciaSugerida = '{10}' ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                        sFolioPedido, sIdClaveSSA, iExistencia, iCantidad, iCantidadEnCajas, sClaveSSA, iContenidoPaquete, 0);

                    sQuery = sSql;
                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false; 
                        break;
                    }
                }
            }

            return bRegresa;
        }

        #endregion Guardar Informacion

        #region Eliminar Informacion 
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //string sSql = "", sMensaje = "";
            //int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            //string message = "¿ Desea cancelar el Folio seleccionado ?";

            //if (General.msjCancelar(message) == DialogResult.Yes)
            //{
            //    if (ConexionLocal.Abrir())
            //    {
            //        ConexionLocal.IniciarTransaccion();

            //        sSql = string.Format("Set Dateformat YMD Exec spp_Mtto_ALMJ_Pedidos_RC '{0}', '{1}', '{2}', '{3}', '{4}', '', '', '', '', '', '{5}' ",
            //            sEmpresa, sEstado, sJurisdiccion, sFarmacia, txtFolio.Text, iOpcion);

            //        if (myLeer.Exec(sSql))
            //        {
            //            if (myLeer.Leer())
            //                sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));

            //            ConexionLocal.CompletarTransaccion();
            //            General.msjUser(sMensaje); //Este mensaje lo genera el SP
            //            btnNuevo_Click(null, null);
            //        }
            //        else
            //        {
            //            ConexionLocal.DeshacerTransaccion();
            //            Error.GrabarError(myLeer, "btnCancelar_Click");
            //            General.msjError("Ocurrió un error al cancelar el Folio.");
            //            //btnNuevo_Click(null, null);
            //        }

            //        ConexionLocal.Cerrar();
            //    }
            //    else
            //    {
            //        General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
            //    }
            //}            
        }
        #endregion Eliminar Informacion

        #region Imprimir Informacion
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(false);
        }

        private bool validarImpresion(bool Confirmar)
        {
            bool bRegresa = true;

            if (Confirmar)
            {
                if (General.msjConfirmar(" ¿ Desea imprimir la información en pantalla ? ") == DialogResult.No)
                {
                    bRegresa = false;
                }
            }

            if (bRegresa)
            {
                if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
                {
                    bRegresa = false;
                    General.msjUser("Folio de Pedido Inicial inválido, verifique.");
                }
            }

            return bRegresa;
        }

        private void Imprimir(bool Confirmacion)
        {
            bool bRegresa = true;
            //dImporte = Importe; 

            if (validarImpresion(Confirmacion))
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = DtGeneral.RutaReportes; 

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", General.Fg.PonCeros(txtFolio.Text, 6));
                myRpt.NombreReporte = "PtoVta_Pedidos_CEDIS";

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
                // bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, @"PRUEBA.pdf", FormatosExportacion.PortableDocFormat); 

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Imprimir Informacion

        #region Eventos 
        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            //bEsSeguroPopular = false;
            //bPermitirCapturaBeneficiariosNuevos = false;
            //bImportarBeneficiarios = false;

            if (txtCte.Text.Trim() == "")
            {
                txtCte.Text = "";
                lblCte.Text = "";
                txtSubCte.Text = "";
                lblSubCte.Text = "";
                txtPro.Text = "";
                lblPro.Text = "";
                txtSubPro.Text = "";
                lblSubCte.Text = "";
                //toolTip.SetToolTip(lblCte, "");
                //toolTip.SetToolTip(lblSubCte, "");
                //toolTip.SetToolTip(lblPro, "");
                //toolTip.SetToolTip(lblSubPro, "");
            }
            else
            {
                if (Fg.PonCeros(txtCte.Text, 4) == sIdPublicoGral)
                {
                    General.msjAviso("El Cliente Publico General es exclusivo de Venta a Contado, no puede ser utilizado en Venta a Credito");
                    txtCte.Text = "";
                    lblCte.Text = "";
                    //toolTip.SetToolTip(lblCte, "");
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
                        txtCte.Enabled = false;
                        txtCte.Text = leer.Campo("IdCliente");
                        lblCte.Text = leer.Campo("NombreCliente");
                        txtSubCte.Text = "";
                        lblSubCte.Text = "";
                        txtPro.Text = "";
                        lblPro.Text = "";
                        txtSubPro.Text = "";
                        lblSubCte.Text = "";

                        //toolTip.SetToolTip(lblCte, lblCte.Text);

                        //// Exigir la informacion de Seguro Popular solo si esta activo.
                        //if (bValidarSeguroPopular)
                        {
                            if (sIdSeguroPopular == txtCte.Text.Trim())
                            {
                                bEsSeguroPopular = true;
                            }
                        }
                    }
                }
            }

        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                leer2.DataSetClase = ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, "txtCte_KeyDown");
                if (leer2.Leer())
                {
                    txtCte.Text = leer2.Campo("IdCliente");
                    lblCte.Text = leer2.Campo("NombreCliente");
                    //toolTip.SetToolTip(lblCte, lblCte.Text);
                    txtSubCte.Focus();
                }
            }
        }

        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            //bPermitirCapturaBeneficiariosNuevos = false;
            //bImportarBeneficiarios = false;

            if (txtSubCte.Text.Trim() == "")
            {
                txtSubCte.Text = "";
                lblSubCte.Text = "";
                txtPro.Text = "";
                lblPro.Text = "";
                txtSubPro.Text = "";
                lblSubCte.Text = "";
                //toolTip.SetToolTip(lblSubCte, "");
                //toolTip.SetToolTip(lblPro, "");
                //toolTip.SetToolTip(lblSubPro, "");
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

                    txtSubCte.Enabled = false;
                    txtSubCte.Text = leer.Campo("IdSubCliente");
                    lblSubCte.Text = leer.Campo("NombreSubCliente");
                    bPermitirCapturaBeneficiariosNuevos = leer.CampoBool("PermitirCapturaBeneficiarios");
                    bImportarBeneficiarios = leer.CampoBool("PermitirImportaBeneficiarios");

                    btnRegistrarBeneficiarios.Enabled = bPermitirCapturaBeneficiariosNuevos;
                    btnRegistrarBeneficiarios.Visible = bPermitirCapturaBeneficiariosNuevos;

                    txtPro.Text = "";
                    lblPro.Text = "";
                    txtSubPro.Text = "";
                    lblSubPro.Text = "";

                    txtIdBenificiario.Enabled = true;

                    //toolTip.SetToolTip(lblSubCte, lblSubCte.Text);

                    ////// Exclusivo Seguro Popular 
                    //if (bEsSeguroPopular)
                    //    MostrarInfoVenta(); 

                    //////// Inicializar el Grid 
                    //////myGrid.Limpiar(true); 

                }
            }
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "")
                {
                    leer2.DataSetClase = ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, "txtSubCte_KeyDown_1");
                    if (leer2.Leer())
                    {
                        txtSubCte.Text = leer2.Campo("IdSubCliente");
                        lblSubCte.Text = leer2.Campo("NombreSubCliente");
                        //toolTip.SetToolTip(lblSubCte, lblSubCte.Text);
                        txtPro.Focus();
                    }
                }
            }
        }

        private void txtPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtPro.Text, "txtPro_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Programa no encontrada, ó el Programa no pertenece al Cliente ó Farmacia.");
                    txtPro.Text = "";
                    lblPro.Text = "";
                    txtSubPro.Text = "";
                    lblSubPro.Text = "";
                    //toolTip.SetToolTip(lblPro, "");
                    //toolTip.SetToolTip(lblSubPro, "");
                    e.Cancel = true;
                }
                else
                {
                    txtPro.Enabled = false;
                    txtPro.Text = leer.Campo("IdPrograma");
                    lblPro.Text = leer.Campo("Programa");
                    txtSubPro.Text = "";
                    lblSubPro.Text = "";
                    //toolTip.SetToolTip(lblPro, lblPro.Text);
                    //toolTip.SetToolTip(lblSubPro, "");
                }
            }
            else
            {
                txtPro.Text = "";
                lblPro.Text = "";
                txtSubPro.Text = "";
                lblSubPro.Text = "";
                //toolTip.SetToolTip(lblPro, "");
                //toolTip.SetToolTip(lblSubPro, "");
            }
        }

        private void txtPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "")
                {
                    //leer2.DataSetClase = Ayuda.Programas("txtPro_KeyDown");
                    leer2.DataSetClase = ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, "txtPro_KeyDown");
                    if (leer2.Leer())
                    {
                        txtPro.Text = leer2.Campo("IdPrograma");
                        lblPro.Text = leer2.Campo("Programa");
                        //toolTip.SetToolTip(lblPro, lblPro.Text);
                        txtSubPro.Focus();
                    }
                }
            }
        }

        private void txtSubPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "" && txtSubPro.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtPro.Text, txtSubPro.Text, "txtPro_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Sub-Programa no encontrada, ó el Sub-Programa no pertenece al Cliente.");
                    e.Cancel = true;
                }
                else
                {
                    txtSubPro.Enabled = false;
                    txtSubPro.Text = leer.Campo("IdSubPrograma");
                    lblSubPro.Text = leer.Campo("SubPrograma");
                    //toolTip.SetToolTip(lblSubPro, lblSubPro.Text);

                    // Obtener datos de IMach 
                    //sFolioSolicitud = IMachPtoVta.ObtenerFolioSolicitud();

                    // Exclusivo Seguro Popular 
                    //if (bEsSeguroPopular)
                    //{
                    //    MostrarInfoVenta();
                    //}

                    //if (!bEsSurtimientoPedido)
                    //{
                    ////myGrid.Limpiar(true);
                    //}
                }
            }
            else
            {
                txtSubPro.Text = "";
                lblSubPro.Text = "";
                //toolTip.SetToolTip(lblSubPro, "");
            }
        }

        private void txtSubPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "")
                {
                    leer2.DataSetClase = ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtPro.Text, "txtPro_KeyDown");
                    if (leer2.Leer())
                    {
                        txtSubPro.Text = leer2.Campo("IdSubPrograma");
                        lblSubPro.Text = leer2.Campo("SubPrograma");
                        //toolTip.SetToolTip(lblSubPro, lblSubPro.Text);
                    }
                }
            }
        }

        #endregion Eventos

        #region Grid 
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (!bFolio_Guardado)
            {
                if (myGrid.ActiveCol == 1)
                {
                    if (e.KeyCode == Keys.F1)
                    {
                        myLeer.DataSetClase = ayuda.ClavesSSA_Sales_Existencia("grdProductos_KeyDown");
                        if (myLeer.Leer())
                        {
                            myGrid.SetValue(myGrid.ActiveRow, 1, myLeer.Campo("IdClaveSSa_Sal"));
                            CargaDatosSal();
                        }
                    }

                    if (e.KeyCode == Keys.Delete)
                    {
                        myGrid.DeleteRow(myGrid.ActiveRow);

                        if (myGrid.Rows == 0)
                        {
                            myGrid.Limpiar(true);
                        }
                    }
                }
            }
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (!bFolio_Guardado)
            {
                if (lblStatus.Visible == false)
                {
                    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                    {
                        if (myGrid.GetValue(myGrid.ActiveRow, 1) != "" && myGrid.GetValue(myGrid.ActiveRow, 3) != "")
                        {
                            myGrid.Rows = myGrid.Rows + 1;
                            myGrid.ActiveRow = myGrid.Rows;
                            myGrid.SetActiveCell(myGrid.Rows, 1);
                        }
                    }
                }
            }
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA); 
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA) != "")
            {
                switch (myGrid.ActiveCol)
                {
                    case 1:
                        {
                            ObtenerDatosSal();
                        }

                        break;
                }
            }
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= myGrid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                myGrid.SetValue(myGrid.ActiveRow, i, "");
            }
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                if (myGrid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina. 
                {
                    myGrid.DeleteRow(i);
                }
            }

            if (myGrid.Rows == 0) // Si No existen renglones, se inserta 1.
            {
                myGrid.AddRow();
            }
        }

        private void ObtenerDatosSal()
        {
            string sCodigo = "";  // , sSql = "";
            // int iCantidad = 0;

            sCodigo = myGrid.GetValue(myGrid.ActiveRow, 1);

            if ( sCodigo.Trim() == "" )
            {
                General.msjUser("Sal no encontrada ó no esta Asignada a la Farmacia.");
                myGrid.LimpiarRenglon(myGrid.ActiveRow);
            }
            else
            {
                ////sSql = string.Format("Exec Spp_SalesCapturaPedidosCentros '{0}', '{1}' ",
                ////    Fg.PonCeros(sCodigo, 4), sCodigo);
                ////if (!myLeer.Exec(sSql))
                ////{
                ////    Error.GrabarError(myLeer, "ObtenerDatosSal()");
                ////    General.msjError("Ocurrió un error al obtener la información de la Sal.");
                ////}
                ////else 
                {
                    GnFarmacia.ValidarSeleccionClaveSSA(ref sCodigo);

                    myLeer.DataSetClase = Consultas.PedidosEspeciales_ClavesSSA_Existencia(sCodigo, "ObtenerDatosSal()"); 
                    if (!myLeer.Leer())
                    {
                        //General.msjUser("Sal no encontrada ó no esta Asignada a la Farmacia.");
                        myGrid.LimpiarRenglon(myGrid.ActiveRow);
                    }
                    else
                    {
                        CargaDatosSal();
                    }
                }
            }
        }

        private void CargaDatosSal()
        {
            int iRowActivo = myGrid.ActiveRow;

            if (lblStatus.Visible == false)
            {
                if (!myGrid.BuscaRepetido(myLeer.Campo("ClaveSSA"), iRowActivo, 1))
                {
                    myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));
                    myGrid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, myLeer.Campo("IdClaveSSA_Sal"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Presentacion, myLeer.Campo("Presentacion"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Existencia, myLeer.Campo("Existencia"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, myLeer.CampoInt("Cantidad"));
                    myGrid.SetValue(iRowActivo, (int)Cols.ContenidoPaquete, myLeer.Campo("ContenidoPaquete"));
                    myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                    myGrid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);
                }
                else
                {
                    General.msjUser("Esta Clave ya se encuentra capturada en otro renglon.");
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Cantidad, "");
                    limpiarColumnas();
                    myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                }

            }
        }
        #endregion Grid

        #region Validaciones de Controles 
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Pedido inválido, verifique.");
                txtFolio.Focus();
            }

            //if (cboJurisdicciones.Data == "*" && bRegresa)
            //{
            //    bRegresa = false;
            //    General.msjUser("Seleccione una jurisdicción porfavor.");
            //    cboJurisdicciones.Select();
            //}
            
            //if (cboFarmacias.Data == "*" && bRegresa)
            //{
            //    bRegresa = false;
            //    General.msjUser("Seleccione una farmacia porfavor.");
            //    cboFarmacias.Select();
            //}

            if (bRegresa && txtCte.Enabled == true)
            {
                bRegresa = false;
                General.msjUser("Clave de Cliente no autenticada, verifique.");
                txtCte.Focus();
            }

            if (bRegresa && txtSubCte.Enabled == true)
            {
                bRegresa = false;
                General.msjUser("Clave de SubCliente no autenticada, verifique.");
                txtSubCte.Focus();
            }

            if (bRegresa && txtPro.Enabled == true)
            {
                bRegresa = false;
                General.msjUser("Clave de Programa no autenticada, verifique.");
                txtPro.Focus();
            }

            if (bRegresa && txtSubPro.Enabled == true)
            {
                bRegresa = false;
                General.msjUser("Clave de SubPrograma no autenticada, verifique.");
                txtSubPro.Focus();
            }

            if (bRegresa && txtObservaciones.Text == "")
            {
                bRegresa = false;
                General.msjUser("No ah capturado observaciones, verifique.");
                txtObservaciones.Focus();
            }

            if (bRegresa && txtReferenciaPedido.Text == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la referencia del pedido, verifique.");
                txtReferenciaPedido.Focus();
            }

            if (bRegresa && !bVigenciaValida)
            {
                bRegresa = false;
                General.msjUser("La Vigencia del Beneficiario expiró,\nno es posible generar la venta, verifique.");
            }

            if (bRegresa && !bEsActivo)
            {
                bRegresa = false;
                General.msjUser("El Beneficiario se encuentra cancelado, no es posible generar la venta.");
            }

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
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
                    ////if ( int.Parse( lblUnidades.Text ) == 0 )
                    ////{
                    ////    bRegresa = false;
                    ////}
                    ////else
                    {
                        for (int i = 1; i <= myGrid.Rows; i++)
                        {
                            if (myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos una Clave para el Pedido\n y/o capturar cantidades para al menos una Clave, verifique.");
            }

            return bRegresa;

        } 

        private void txtIdBenificiario_Validating(object sender, CancelEventArgs e)
        {
            bEsActivo = false;
            lblStatus.Visible = false;
            lblStatus.Text = "CANCELADO";

            if (txtIdBenificiario.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Beneficiarios(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtCte.Text.Trim(), txtSubCte.Text.Trim(),
                                                            txtIdBenificiario.Text, "txtIdBenificiario_Validating");
                if (leer.Leer())
                {
                    CargarDatosBenefiario();
                }
                else
                {
                    General.msjUser("Clave de Beneficiario no encontrada, verifique.");
                    txtIdBenificiario.Text = "";
                    lblNombre.Text = "";
                    lblFolioReferencia.Text = "";
                    lblFechaVigencia.Text = "";
                    txtIdBenificiario.Focus();
                }
            }
            else
            {
                txtIdBenificiario.Text = "";
                lblNombre.Text = "";
                lblFolioReferencia.Text = "";
                lblFechaVigencia.Text = "";
                txtIdBenificiario.Focus();
            }
        }

        private void txtIdBenificiario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                helpBeneficiarios = new FrmHelpBeneficiarios();
                leer.DataSetClase = helpBeneficiarios.ShowHelp(txtCte.Text.Trim(), txtSubCte.Text.Trim(), bImportarBeneficiarios);
                if (leer.Leer())
                {
                    CargarDatosBenefiario();
                    //txtIdBenificiario.Text = leer.Campo("IdBeneficiario");
                    //lblNombre.Text = leer.Campo("NombreCompleto");
                }
            }
        }

        private void btnRegistrarBeneficiarios_Click(object sender, EventArgs e)
        {
            FrmBeneficiarios f = new FrmBeneficiarios();
            f.MostrarDetalle(txtCte.Text.Trim(), lblCte.Text, txtSubCte.Text.Trim(), lblSubCte.Text); 
        }

        private void btnImportarPedidoMasivo_Click( object sender, EventArgs e )
        {
            FrmRegistroPedidosEspeciales_Importacion f = new FrmRegistroPedidosEspeciales_Importacion(TiposDePedidosCEDIS.Venta, TipoDePedidoElectronico.Ventas);
            f.ShowInTaskbar = false;
            f.ShowDialog(this);
        }

        private void btnActualizarReferencia_Click( object sender, EventArgs e )
        {
            FrmRegistroPedidosEspeciales_ModificarReferencia f = new FrmRegistroPedidosEspeciales_ModificarReferencia(TiposDePedidosCEDIS.Venta, txtFolio.Text, txtReferenciaPedido.Text);
            f.ShowInTaskbar = false;
            f.ShowDialog(this);

            if (f.ReferenciaActualizada)
            {
                txtReferenciaPedido.Text = f.Referencia;
            }
        }
        #endregion Validaciones de Controles

        #region funciones y Procedimientos

        private void CargarDatosBenefiario()
        {
            lblCanceladoBen.Visible = false;
            txtIdBenificiario.Text = leer.Campo("IdBeneficiario");
            lblNombre.Text = leer.Campo("NombreCompleto");

            lblFolioReferencia.Text = leer.Campo("FolioReferencia");
            lblFechaVigencia.Text = General.FechaYMD(leer.CampoFecha("FechaFinVigencia"), "-");

            bVigenciaValida = leer.CampoBool("EsVigente");
            if (!bVigenciaValida)
            {
                General.msjAviso("La Vigencia del Beneficiario ha expirado, no es posible generar la venta.");
            }

            bEsActivo = true;
            if (leer.Campo("Status").ToUpper() == "C")
            {
                bEsActivo = false;
                lblCanceladoBen.Visible = true;
                General.msjUser("El Beneficiario se encuentra cancelado, no es posible generar la venta.");
            }

            if (bEsActivo)
            {
                if (bValidarBeneficariosAlertas)
                {
                    ValidarBeneficiarioCuentaConAlertas();
                }
            }
        }

        private void ValidarBeneficiarioCuentaConAlertas()
        {
            string sReferencia = lblFolioReferencia.Text.Replace(".", "-");
            int iPosicion = 0;

            iPosicion = sReferencia.IndexOf("-", 0);
            sReferencia = sReferencia.Substring(0, iPosicion).Trim();


            string sSql = string.Format(
                "Select 'Nombre de Beneficiario' = NombreBeneficiario " +
                "From CFG_VAL_BeneficiariosAlertas (NoLock) " +
                "Where Status = 'A' and FolioReferencia like '%{0}%' ", sReferencia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidarBeneficiarioCuentaConAlertas");
                General.msjError("Ocurrió un error al validar las alertas de beneficiarios.");
            }
            else
            {
                if (leer.Leer())
                {
                    FrmBeneficiarios_Alertas alertas = new FrmBeneficiarios_Alertas(leer.DataSetClase);
                    alertas.ShowDialog(this);
                }
            }
        }

        #endregion funciones y Procedimientos



    }
}
