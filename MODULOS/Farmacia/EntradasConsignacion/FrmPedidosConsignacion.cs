using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace Farmacia.EntradasConsignacion
{
    public partial class FrmPedidosConsignacion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        string sFile_In = "";
        string sTitulo = "";
        string sFormato = "###, ###, ###, ##0";
        string sMensaje = "";
        string sMsjGuardar = "";
        string sMsjError = "";
        string sFolio = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdPersonalConectado = DtGeneral.IdPersonal;
        string sMsjNoEncontrado = "";
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        int iRegistrosHoja = 0;
        int iRegistrosProcesados = 0;

        bool bValidandoInformacion = false;
        bool bSeEncontraronIndicencias = false;
        bool bActivarProceso = false;
        bool bErrorAlValidar = false;
        bool bFolioGuardado = false;
        bool bModificarCaptura = true;

        private bool bEsModuloValido = false;

        clsExportarExcelPlantilla xpExcel;
        clsListView lst;
        OpenFileDialog openExcel = new OpenFileDialog();
        Thread thLoadFile;
        Thread thReadFile;
        Thread thGuardarInformacion;
        Thread thValidarInformacion;
        Thread thGeneraFolios;

        clsLeer leer;
        clsLeerExcel excel;
        clsLeer myLlenaDatos;
        clsDatosCliente DatosCliente;
        clsConsultas Consultas;
        clsAyudas Ayuda;

        public FrmPedidosConsignacion()
        {
            InitializeComponent();

            lst = new clsListView(lstVwInformacion);
            leer = new clsLeer(ref cnn);
            myLlenaDatos = new clsLeer(ref cnn);
            excel = new clsLeerExcel();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            myLlenaDatos = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sIdPersonalConectado);



            lst.OrdenarColumnas = false;

            FrameResultado.Height = 300;
            FrameResultado.Width = 800;

            FrameProceso.Top = 345;
            FrameProceso.Left = 116;
            MostrarEnProceso(false, 0);

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            toolStripBarraImportacion.BackColor = Color.WhiteSmoke;
            lblProcesados.Text = "";
            lblProcesados.Visible = false;
            dtpFechaRegistro.Enabled = false;
            IniciarToolBar();
            lst.Limpiar();
            IniciaToolBar2(true, false, false, false, false);
            txtFolio.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar(1);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            DatosCliente.Funcion = "ImprimirCompra()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_PedidosDeConsignacion.rpt";

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("Folio", txtFolio.Text);

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (bRegresa)
            {
                btnNuevo_Click(null, null);
            }
            else
            {
                if(!DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Guardar(2);
        }

        private void btnNuevo2_Click(object sender, EventArgs e)
        {

            lblProcesados.Visible = false;
            lblProcesados.Text = "";

            sFile_In = "";
            cboHojas.Clear();
            cboHojas.Add();

            sTitulo = "Información ";
            FrameResultado.Text = sTitulo;
            Fg.IniciaControles();
            lst.Limpiar();

            btnEjecutar.Enabled = false;
            btnGuardar.Enabled = false;

            IniciaToolBar2(true, false, false, false, false);
            if (!bEsModuloValido)
            {
                IniciaToolBar2(false, false, false, false, false);
                General.msjAviso("Este módulo es exclusivo para Almacenes y/o Farmacias, se deshabilitaran todas las opciones.");
            }

        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            openExcel.Title = "Pedidos de consignación";
            openExcel.Filter = "Archivos de Excel (*.xls;*.xlsx)| *.xls;*.xlsx";
            openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openExcel.AddExtension = true;
            lblProcesados.Visible = false;

            if (openExcel.ShowDialog() == DialogResult.OK)
            {
                sFile_In = openExcel.FileName;

                IniciaToolBar2(false, false, false, false, false);
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

        private void btnGuardar2_Click(object sender, EventArgs e)
        {
            string[] SCols = { "ClaveSSA", "Descripcion", "Costo", "Cantidad", "Iva" };
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

        private void IniciaToolBar2(bool Abrir, bool Ejecutar, bool Guardar2, bool Validar, bool Guardar)
        {

            btnAbrir.Enabled = Abrir;
            btnEjecutar.Enabled = Ejecutar;
            btnGuardar2.Enabled = Guardar2;
            btnValidarDatos.Enabled = Validar;
            btnGuardar.Enabled = Guardar;
        }

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

        private void CargarArchivo()
        {
            string sHoja = "";
            bool bHabilitar = false;

            BloqueaHojas(true);
            MostrarEnProceso(true, 1);
            FrameResultado.Text = sTitulo;

            excel = new clsLeerExcel(sFile_In);
            excel.GetEstructura();

            cboHojas.Clear();
            cboHojas.Add();
            lst.Limpiar();
            Thread.Sleep(1000);

            bHabilitar = excel.Hojas.Registros > 0;
            while (excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja");
                cboHojas.Add(sHoja, sHoja);
            }

            cboHojas.SelectedIndex = 0;
            btnEjecutar.Enabled = bHabilitar;
            IniciaToolBar2(true, bHabilitar, false, false, false);

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

        private void thLeerHoja()
        {
            BloqueaHojas(true);
            MostrarEnProceso(true, 2);
            lblProcesados.Visible = false;

            LeerHoja();

            BloqueaHojas(false);
            MostrarEnProceso(false, 2);
        }

        private bool LeerHoja()
        {
            bool bRegresa = false;

            IniciaToolBar2(false, false, bRegresa, false, false);
            FrameResultado.Text = sTitulo;
            lst.Limpiar();
            excel.LeerHoja(cboHojas.Data);

            FrameResultado.Text = sTitulo;
            if (excel.Leer())
            {
                bRegresa = true;
                iRegistrosHoja = excel.Registros;
                FrameResultado.Text = string.Format("{0}: {1} registros ", sTitulo, iRegistrosHoja.ToString(sFormato));
                lst.CargarDatos(excel.DataSetClase, true, true);
            }

            IniciaToolBar2(true, true, bRegresa, false, false);
            return bRegresa;
        }

        private void GuardarInformacion()
        {
            bool bRegresa = false;
            string sSql = "";
            clsLeer leerGuardar = new clsLeer(ref cnn);

            BloqueaHojas(true);
            MostrarEnProceso(true, 3);
            IniciaToolBar2(false, false, false, false, false);

            lblProcesados.Visible = true;
            lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato));


            // leerGuardar.DataSetClase = excel.DataSetClase;
            excel.RegistroActual = 1;
            bRegresa = excel.Registros > 0;
            iRegistrosProcesados = 0;

            leerGuardar.Exec("Truncate Table Pedidos_CargaMasiva ");
            while (excel.Leer())
            {
                sSql = string.Format("Insert Into Pedidos_CargaMasiva ( ClaveSSA, DescripcionClaveSSA, Costo, Cantidad, Iva ) \n");
                sSql += string.Format("Select '{0}', '{1}', '{2}', '{3}', '{4}'  ",
                    DarFormato(excel.Campo("ClaveSSA")), DarFormato(excel.Campo("Descripcion")),
                    excel.Campo("Costo") == "" ? "0" : excel.Campo("Costo"),
                    excel.CampoInt("Cantidad"), excel.CampoInt("Iva"));

                if (!leerGuardar.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leerGuardar, "GuardarInformacion()");
                    break;
                }
                iRegistrosProcesados++;
                lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato));
            }

            BloqueaHojas(false);
            MostrarEnProceso(false, 3);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar la información del inventario.");
                IniciaToolBar2(true, true, true, false, false);
            }
            else
            {
                leerGuardar.Exec("Exec spp_FormatearTabla 'Pedidos_CargaMasiva'  ");
                General.msjUser("Información de inventario cargada satisfactoriamente.");
                IniciaToolBar2(true, true, false, true, false);
            }
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

            IniciaToolBar2(false, false, false, false, bActivarProceso);
            BloqueaHojas(true);
            MostrarEnProceso(true, 4);
            lblProcesados.Visible = false;

            string sSql = string.Format("Exec sp_Proceso_Pedidos_CargaMasiva_000_Validar_Datos_De_Entrada ");


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
                leer.RenombrarTabla(2, "Costo");
                leer.RenombrarTabla(3, "Cantidad");
                leer.RenombrarTabla(4, "Iva");
                leer.RenombrarTabla(5, "Descripcion");

                leerValidacion.DataTableClase = leer.Tabla(1); //Clave
                bActivarProceso = leer.Registros > 0;

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(2);   // Costo 
                    bActivarProceso = leerValidacion.Registros > 0;
                }

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(3);   // Cantidad  
                    bActivarProceso = leerValidacion.Registros > 0;
                }

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(4);   // Iva  
                    bActivarProceso = leerValidacion.Registros > 0;
                }

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(5);   // Descripcion
                    bActivarProceso = leerValidacion.Registros > 0;
                }
            }

            bValidandoInformacion = false;
            bActivarProceso = !bActivarProceso;
            BloqueaHojas(false);
            MostrarEnProceso(false, 4);
        }

        private bool validarIntegracionInventario()
        {
            bool bRegresa = false;
            string sMsj = "El proceso de integración de inventario generara una salida general de existencias, y dara ingreso como inventario final al contenido del archivo cargado, ¿ Desea continuar ? ";

            sMsj = "Este proceso integrará el inventario cargado como un ajuste de inventario,\n\n¿ Desea continuar ?";
            if (General.msjConfirmar(sMsj) == DialogResult.Yes)
            {
                bRegresa = true;
            }

            return bRegresa;
        }

        private void FrmPedidosConsignacion_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        private void tmValidacion_Tick_1(object sender, EventArgs e)
        {
            tmValidacion.Stop();
            tmValidacion.Enabled = false;
            string sMsj = "Existen Claves SSA sin identificar. \nDesea generar reporte ? ";


            if (!bValidandoInformacion)
            {
                if (bActivarProceso)
                {
                    IniciaToolBar2(true, true, false, false, true);
                }
                else
                {
                    IniciaToolBar2(true, true, false, true, false);
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

        private void txtIdProveedor_TextChanged(object sender, EventArgs e)
        {
            lblProveedor.Text = "";
        }

        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdProveedor.Text.Trim() != "")
            {
                myLlenaDatos.DataSetClase = Consultas.ProveedoresDePedidos(DtGeneral.EstadoConectado, txtIdProveedor.Text.Trim(), "txtIdProveedor_Validating");
                if (myLlenaDatos.Leer())
                {
                    CargaDatosProveedor();
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
                txtIdProveedor.Text = myLlenaDatos.Campo("IdProveedor");
                lblProveedor.Text = myLlenaDatos.Campo("Nombre");
            }
            else
            {
                General.msjUser("El Proveedor " + myLlenaDatos.Campo("Nombre") + " actualmente se encuentra cancelado, verifique. ");
                txtIdProveedor.Text = "";
                lblProveedor.Text = "";
                txtIdProveedor.Focus();
            }
        }

        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLlenaDatos.DataSetClase = Ayuda.ProveedoresDePedidos(DtGeneral.EstadoConectado, "txtIdProveedor_KeyDown");

                if (myLlenaDatos.Leer())
                {
                    CargaDatosProveedor();
                }
            }
        }

        private void Guardar(int iOpcion)
        {
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;

            if (txtFolio.Text != "*")
            {
                MessageBox.Show("Este Folio ya ha sido guardado por lo tanto no puede ser modificado");
            }
            else
            {
                if (ValidaDatos())
                {
                    if (!cnn.Abrir())
                    {
                        Error.LogError(cnn.MensajeError);
                        General.msjErrorAlAbrirConexion(); 
                    }
                    else 
                    {
                        IniciarToolBar();
                        cnn.IniciarTransaccion();

                        if (GrabarEncabezado(iOpcion)) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                        {
                            txtFolio.Text = sFolio;
                            cnn.CompletarTransaccion();
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            // btnNuevo_Click(null, null);
                            IniciarToolBar(false, false, true);
                            btnImprimir_Click(this, null);
                        }
                        else
                        {
                            txtFolio.Text = "*";
                            cnn.DeshacerTransaccion();
                            Error.GrabarError(leer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al guardar la información.");
                            IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir);
                            //btnNuevo_Click(null, null);

                        }

                        cnn.Cerrar();
                    }
                }
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtIdProveedor.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Proveedor inválida, verifique.");
                txtIdProveedor.Focus();
            }

            if (bRegresa && txtReferenciaDocto.Text == "")
            {
                bRegresa = false;
                General.msjUser("Referencia inválida, verifique.");
                txtReferenciaDocto.Focus();
            }

            if (bRegresa && txtObservaciones.Text == "")
            {
                bRegresa = false;
                General.msjUser("Observaciones inválidas, verifique.");
                txtObservaciones.Focus();
            }

            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "El usuario no tiene permiso para realizar un pedido de consignación, verifique por favor.";
                ////bRegresa = opPermisosEspeciales.VerificarPermisos("PEDIDO_CONSIGNACION", sMsjNoEncontrado);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("PEDIDO_CONSIGNACION", sMsjNoEncontrado);
            }

            return bRegresa;
        }

        private bool GrabarEncabezado(int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "";  // , sQuery = "";


            sSql = string.Format("Exec spp_Mtto_PedidosEnc_Consignacion \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', \n" +
                "\t@IdPersonal = '{4}', @IdProveedor = '{5}', @FechaPedido = '{6}', @FechaEntrega = '{7}', @ReferenciaPedido = '{8}', \n" +
                "\t@Observaciones = '{9}', @iOpcion = '{10}' \n",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text, DtGeneral.IdPersonal, txtIdProveedor.Text.Trim(),
                General.FechaYMD(dtpFechaPedido.Value), General.FechaYMD(dtpFechaEntrega.Value),
                txtReferenciaDocto.Text, txtObservaciones.Text.Trim(), iOpcion);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (iOpcion == 1)
                {
                    leer.Leer();
                    sFolio = leer.Campo("Folio");
                    sMensaje = leer.Campo("Mensaje");

                    sSql = string.Format("Exec spp_Mtto_PedidosDet_Consignacion @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}' ",
                             sEmpresa, sEstado, sFarmacia, sFolio);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                    }
                }
            }

            return bRegresa;
        }

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = true;
            bModificarCaptura = true;
            bFolioGuardado = false;
            IniciarToolBar(false, false, false);

            //LeerHoja = new clsLeer(ref cnn);

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                IniciarToolBar(true, false, false);
            }
            else
            {
                string sSql = string.Format("Select * \nFrom vw_PedidosEnc_Consignacion P (NoLock) \n" +
                        "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' \n",
                        sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text.Trim(), 8));


                if (leer.Exec(sSql))
                {

                    if (!leer.Leer())
                    {
                        General.msjAviso("Folio de Pedido no encontrado, verifique.");
                        bContinua = false;
                        txtFolio.Text = "";
                    }
                    else
                    {
                        bFolioGuardado = true;
                        IniciarToolBar(false, false, true);
                        bModificarCaptura = false;
                        CargaEncabezadoFolio();
                    }
                }
                else
                {
                    General.msjAviso("Ocurrió un error al obtener los datos del Encabezado del Pedido.");
                    Error.GrabarError(leer, "txtFolio_Validating()");
                    General.msjError("Ocurrió un error al verificar el inventario a integrar.");
                }

                if (bContinua)
                {
                    if (!CargaDetallesFolio())
                    {
                        bContinua = false;
                    }
                    //else
                    //    Fg.BloqueaControles(this, false);// Se bloquea todo ya que una Compra guardada no se puede modificar.                    
                }
            }

            if (!bContinua)
            {
                txtFolio.Focus();
            }

        }

        private void CargaEncabezadoFolio()
        {
            DateTimePicker dtpPaso = new DateTimePicker();

            txtFolio.Text = leer.Campo("Folio");
            //sFolioCompra = txtFolio.Text;
            txtReferenciaDocto.Text = leer.Campo("ReferenciaPedido");

            txtIdProveedor.Text = leer.Campo("IdProveedor");
            lblProveedor.Text = leer.Campo("Proveedor");
            txtObservaciones.Text = leer.Campo("Observaciones");
            dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");
            dtpFechaPedido.Value = leer.CampoFecha("FechaPedido");
            dtpFechaEntrega.Value = leer.CampoFecha("FechaEntrega");

            //Se bloquea el encabezado del Folio 
            Fg.BloqueaControles(this, false, FrameEncabezado);

            if (leer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
            }
        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = true;

            string sSql = string.Format("Select ClaveSSA, DescripcionClaveSSA, Costo, Cantidad, Iva \n\tFrom vw_PedidosDet_Consignacion P (NoLock) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' \n",
                sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text.Trim(), 8));


            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    IniciaToolBar2(false, false, false, false, false);
                }
                else
                {
                    bRegresa = false;
                }
            }
            else
            {
                General.msjAviso("Ocurrió un error al obtener los datos del Encabezado del Pedido.");
                Error.GrabarError(leer, "ValidarInformacion()");
                General.msjError("Ocurrió un error al verificar el inventario a integrar.");
            }

            return bRegresa;
        }
    }
}
