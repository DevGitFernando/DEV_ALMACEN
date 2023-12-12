using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.OfficeOpenXml;
using SC_SolutionsSystem.OfficeOpenXml.Data;


using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllFarmaciaSoft.Informacion;


namespace Almacen.PedidosEspeciales
{
    public partial class FrmRegistroPedidosEspeciales_Importacion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeerExcelOpenOficce excel;

        clsDatosCliente DatosCliente;

        string sIdDistribuidor = ""; 
        string sFile_In = "";
        string sTitulo = "";

        int iRegistrosHoja = 0; 
        int iRegistrosProcesados = 0;  

        clsListView lst;

        OpenFileDialog openExcel = new OpenFileDialog();
        Thread thLoadFile;
        Thread thReadFile;
        Thread thGuardarInformacion;
        Thread thValidarInformacion; 
        Thread thGeneraFolios;

        string sFormato = "###, ###, ###, ##0";
        string sFormato_INT = "###, ###, ###, ##0";
        int iFolioCargaMasiva = 0;
        string sMensaje = "";

        bool bValidandoInformacion = false;
        bool bSeEncontraronIndicencias = false;
        bool bActivarProceso = false; 
        bool bErrorAlValidar = false;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;
        string sMsjNoEncontrado = "";
        string sGUID = Guid.NewGuid().ToString();
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;
        TiposDePedidosCEDIS tpTipoDePedido = TiposDePedidosCEDIS.Ninguno;
        TipoDePedidoElectronico tpTipoDeTransferencia = TipoDePedidoElectronico.Ninguno;

        public FrmRegistroPedidosEspeciales_Importacion( TiposDePedidosCEDIS TipoDePedido, TipoDePedidoElectronico TipoDeTransferencia )
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            tpTipoDePedido = TipoDePedido;
            tpTipoDeTransferencia = TipoDeTransferencia;


            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.Puerto = General.DatosConexion.Puerto;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 


            this.sIdDistribuidor = Fg.PonCeros("", 4); 
            leer = new clsLeer(ref cnn);
            excel = new clsLeerExcelOpenOficce();

            lst = new clsListView(lstVwInformacion);
            lst.OrdenarColumnas = false; 

            FrameResultado.Height = 460;
            FrameResultado.Width = 800;

            FrameProceso.Top = 350;
            FrameProceso.Left = 116; 
            MostrarEnProceso(false, 0);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sPersonal);
        }

        private void FrmRegistroPedidosEspeciales_Importacion_Load(object sender, EventArgs e)
        {
            InicializarPantalla(); 

            GetDriverExcel(); 
        }

        #region Botones 
        private void IniciaToolBar(bool Nuevo, bool Abrir, bool Ejecutar, bool Guardar, bool Validar, bool Procesar, bool Salir)
        {
            btnNuevo.Enabled = Nuevo;
            btnAbrir.Enabled = Abrir;
            btnEjecutar.Enabled = Ejecutar;
            btnGuardar.Enabled = Guardar;
            btnValidarDatos.Enabled = Validar; 
            btnProcesarRemisiones.Enabled = Procesar; 
            btnSalir.Enabled = Salir;
        }

        private void btnNuevo_Click( object sender, EventArgs e )
        {
            InicializarPantalla(); 
        }

        private void InicializarPantalla()
        {
            // int iRegistrosHoja = 0;
            // int iRegistrosProcesados = 0;

            sGUID = Guid.NewGuid().ToString();
            chkPermitirCantidades_EnCero.Checked = false;
            lblProcesados.Visible = false;
            lblProcesados.Text = ""; 

            sFile_In = "";  
            cboHojas.Clear();
            cboHojas.Add();

            sTitulo = "Información "; 
            FrameResultado.Text = sTitulo;
            
            ////FrameTipoInv.Enabled = true;
            ////rdoTransferencia.Checked = false;
            ////rdoVenta.Checked = false;

            Fg.IniciaControles();
            lst.Limpiar();

            btnEjecutar.Enabled = false;
            btnGuardar.Enabled = false;

            IniciaToolBar(true, true, false, false, false, false, true);

            rdo_Desgloze_01__MEDyMC.Checked = true; 

            if (!DtGeneral.EsAlmacen)
            {
                IniciaToolBar( false, false, false, false, false, false, true);
                General.msjAviso("Este módulo es exclusivo para Almacenes, se deshabilitaran todas las opciones."); 
            }

        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            openExcel.Title = "Archivos de Pedidos";
            openExcel.Filter = "Archivos de Excel (*.xls;*.xlsx)| *.xls;*.xlsx";
            //openExcel.Filter = "Archivos de Texto (*.txt)| *.txt"; 
            openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openExcel.AddExtension = true;
            lblProcesados.Visible = false;

            // if (openExcel.FileName != "")
            if (openExcel.ShowDialog() == DialogResult.OK) 
            {
                sFile_In = openExcel.FileName;
                
                //CargarArchivo(); 
                IniciaToolBar(false, false, false, false, false, false, false); 
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
            //LeerHoja(); 
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            thGuardarInformacion = new Thread(this.GuardarInformacion_Pedidos);
            thGuardarInformacion.Name = "Guardar información seleccionada";
            thGuardarInformacion.Start(); 
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion Botones 

        #region Funciones y Procedimientos Privados 
        private void CargarArchivo()
        {
            string sHoja = "";
            bool bHabilitar = false;

            BloqueaHojas(true);
            MostrarEnProceso(true, 1);
            FrameResultado.Text = sTitulo;

            excel = new clsLeerExcelOpenOficce(sFile_In);
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
            IniciaToolBar(true, true, bHabilitar, false, false, false, true);

            BloqueaHojas(false);
            MostrarEnProceso(false, 1);
            // btnGuardar.Enabled = bHabilitar;

        }

        private void LeerExcel()
        {
            string sHoja = "";
            bool bHabilitar = false;
            excel = new clsLeerExcelOpenOficce(sFile_In);
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
        }

        private void thLeerHoja()
        {
            BloqueaHojas(true);
            MostrarEnProceso(true, 2);
            lblProcesados.Visible = false;
            
            LeerHoja();

            ////if (rdoVenta.Checked || rdoTransferencia.Checked)
            ////{
            ////    FrameTipoInv.Enabled = false;
            ////    LeerHoja();
            ////}
            ////else
            ////{
            ////    General.msjAviso("Seleccione un tipo de pedido... ");
            ////}

            BloqueaHojas(false);
            MostrarEnProceso(false, 2); 
        }

        private bool LeerHoja()
        {
            bool bRegresa = false;
            
            IniciaToolBar(false, false, false, bRegresa, false, false, false);
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

            // btnGuardar.Enabled = bRegresa;
            IniciaToolBar(true, true, true, bRegresa, false, false, true); 
            return bRegresa; 
        }

        private void BloqueaHojas(bool Bloquear)
        {
            cboHojas.Enabled = !Bloquear; 
        }

        private void ValidarInformacion()
        {
            string sSql = ""; 
            int iTipo = 0;
            int iTipoTransferencia = 0;
            int iDesgloze = rdo_Desgloze_01__MEDyMC.Checked ? 1 : 2;
            int iPermitirCantidades_EnCeros = chkPermitirCantidades_EnCero.Checked ? 1 : 0;

            ////tmValidacion.Enabled = true;
            ////tmValidacion.Interval = 1000;
            ////tmValidacion.Start(); 

            bValidandoInformacion = true; 
            bActivarProceso = false;
            bErrorAlValidar = false; 
            clsLeer leerValidacion = new clsLeer();
            clsLeer leerRows = new clsLeer();
            leer = new clsLeer(ref cnn);
            DataSet dtsResultados = new DataSet();

            IniciaToolBar(false, false, false, false, false, bActivarProceso, false); 
            BloqueaHojas(true);
            MostrarEnProceso(true, 4);
            lblProcesados.Visible = false;

            //iTipo = rdoVenta.Checked ? 1 : 2;
            iTipo = (int)tpTipoDePedido;
            iTipoTransferencia = (int)tpTipoDeTransferencia; 


            sSql = string.Format("Exec sp_Proceso_Pedidos_Cedis__CargaMasiva_000_Validar_Datos_De_Entrada \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Tipo = '{3}', @GUID = '{4}', @Desglozar = '{5}', \n" +
                "\t@TipoTransferencia = '{6}', @Permitir_Cantidades_en_Ceros = '{7}'\n",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iTipo, sGUID, iDesgloze, 
                iTipoTransferencia, iPermitirCantidades_EnCeros);


            if (!leer.Exec(sSql))
            {
                bErrorAlValidar = true; 
                bActivarProceso = !bActivarProceso;

                Error.GrabarError(leer, "ValidarInformacion()"); 
                General.msjError("Ocurrió un error al verificar el Pedido a integrar."); 
            }
            else
            {
                leer.RenombrarTabla(1, "Resultados");

                leerValidacion.DataTableClase = leer.Tabla("Resultados");

                dtsResultados = leer.DataSetClase;
                dtsResultados.Tables.Remove("Resultados");
                leer.DataSetClase = dtsResultados;

                for (int i = 1; leerValidacion.Leer();i++)
                {
                    leer.RenombrarTabla(i, leerValidacion.Campo("Descripcion"));

                    leerRows.DataTableClase = leer.Tabla(leerValidacion.Campo("Descripcion"));

                    if (!bActivarProceso)
                    bActivarProceso = leerRows.Registros > 0 ? true : false;

                }
            }


            bValidandoInformacion = false; 
            bActivarProceso = !bActivarProceso; 
            BloqueaHojas(false);
            MostrarEnProceso(false, 4);

            ////if(bActivarProceso)
            ////{
            ////    IniciaToolBar(true, true, true, false, false, false, true);
            ////}
            ////else
            ////{
            ////    IniciaToolBar(true, true, true, true, true, true, true);
            ////}

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
                sTituloProceso = "Integrando Pedidos ..... ";
            }

            FrameProceso.Text = sTituloProceso; 

        }
        #endregion Funciones y Procedimientos Privados 

        #region Guardar Informacion 
        private void thGuardarInformacion_Pedidos()
        {
            ////BloqueaHojas(true);
            ////MostrarEnProceso(true, 3); 
            ////Thread.Sleep(1000);
            ////this.Refresh(); 

            GuardarInformacion_Pedidos();

            ////BloqueaHojas(false);
            ////MostrarEnProceso(false, 3);
            // IniciaToolBar(true, true, true, true, true); 
        }

        private string DarFormato(string Valor)
        {
            string sRegresa = Valor.Trim();

            sRegresa = sRegresa.Replace("'", "");
            sRegresa = sRegresa.Replace(",", "");

            return sRegresa;
        }

        private void GuardarInformacion_Pedidos()
        {
            bool bRegresa = false;
            string sSql = "";
            int iTipo = 1;
            clsLeer leerGuardar = new clsLeer(ref cnn);

            BloqueaHojas(true);
            MostrarEnProceso(true, 3);
            IniciaToolBar(false, false, false, false, false, false, false);

            lblProcesados.Visible = true;
            lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato));


            // leerGuardar.DataSetClase = excel.DataSetClase;
            excel.RegistroActual = 1;
            bRegresa = excel.Registros > 0;
            iRegistrosProcesados = 0;

            //iTipo = rdoTransferencia.Checked ? 1 : 0;
            iTipo = tpTipoDePedido == TiposDePedidosCEDIS.Transferencia ? 1 : 0;




            clsBulkCopy bulk = new clsBulkCopy(General.DatosConexion);

            bulk.NotifyAfter = 500;
            bulk.RowsCopied += new clsBulkCopy.RowsCopiedEventHandler(bulk_RowsCopied);
            bulk.OnCompled += new clsBulkCopy.RowsCopiedEventHandler(bulk_Compled);
            bulk.OnError += new clsBulkCopy.RowsCopiedEventHandler(bulk_Error);

            bulk.ClearColumns();
            bulk.DestinationTableName = "Pedidos_Cedis__CargaMasiva";

            lblProcesados.Text = string.Format("Agregando información de control...");
            //// Agregar columnas 
            if(!excel.ExisteTablaColumna(1, "IdEmpresa"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, cboHojas.Data, "IdEmpresa", "String", DtGeneral.EmpresaConectada);
            }

            if(!excel.ExisteTablaColumna(1, "IdEstado"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, cboHojas.Data, "IdEstado", "String", DtGeneral.EstadoConectado);
            }

            if(!excel.ExisteTablaColumna(1, "IdFarmacia"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, cboHojas.Data, "IdFarmacia", "String", DtGeneral.FarmaciaConectada);
            }

            if(!excel.ExisteTablaColumna(1, "IdPersonal"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, cboHojas.Data, "IdPersonal", "String", DtGeneral.IdPersonal);
            }

            if(!excel.ExisteTablaColumna(1, "EsTransferencia"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, cboHojas.Data, "EsTransferencia", "Boolean", iTipo.ToString());
            }

            if(!excel.ExisteTablaColumna(1, "IdEstadoSolicita"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, cboHojas.Data, "IdEstadoSolicita", "String", DtGeneral.EstadoConectado);
            }

            if(!excel.ExisteTablaColumna(1, "TipoDeClavesDePedido"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, cboHojas.Data, "TipoDeClavesDePedido", "String", "5");
            }

            if(!excel.ExisteTablaColumna(1, "UUID_Unique"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, cboHojas.Data, "UUID_Unique", "String", DarFormato(sGUID));
            }
            //// Agregar columnas 


            //// Asignacion de Columnas 
            lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato));
            bulk.AddColumn("IdEmpresa", "IdEmpresa");
            bulk.AddColumn("IdEstado", "IdEstado");
            bulk.AddColumn("IdFarmacia", "IdFarmacia");
            bulk.AddColumn("IdPersonal", "IdPersonal");
            bulk.AddColumn("EsTransferencia", "EsTransferencia");
            bulk.AddColumn("TipoDeClavesDePedido", "TipoDeClavesDePedido");
            bulk.AddColumn("UUID_Unique", "GUID");

            bulk.AddColumn("IdEstadoSolicita", "IdEstadoSolicita");
            bulk.AddColumn("IdFarmaciaSolicita", "IdFarmaciaSolicita");
            bulk.AddColumn("IdCliente", "IdCliente");
            bulk.AddColumn("IdSubCliente", "IdSubCliente");
            bulk.AddColumn("IdPrograma", "IdPrograma");
            bulk.AddColumn("IdSubPrograma", "IdSubPrograma");
            bulk.AddColumn("Observaciones", "Observaciones");
            bulk.AddColumn("ReferenciaInterna", "ReferenciaInterna");
            bulk.AddColumn("IdBeneficiario", "IdBeneficiario");
            bulk.AddColumn("FechaEntrega", "FechaEntrega");
            bulk.AddColumn("ClaveSSA", "ClaveSSA");
            bulk.AddColumn("ContenidoPaquete", "ContenidoPaquete");
            bulk.AddColumn("Cantidad", "Cantidad");
            bulk.AddColumn("CantidadRequerida", "CantidadRequerida");
            //// Asignacion de Columnas 


            leerGuardar.Exec(string.Format( "Delete From Pedidos_Cedis__CargaMasiva Where GUID = '{0}' ", sGUID));
            bRegresa = bulk.WriteToServer(excel.DataSetClase); //, System.Data.SqlClient.SqlBulkCopyOptions.Default); 




            BloqueaHojas(false);
            MostrarEnProceso(false, 3);

            if (!bRegresa)
            {
                Error.GrabarError(leerGuardar.DatosConexion, bulk.Error, "GuardarInformacion_Pedidos");  
                General.msjError("Ocurrió un error al cargar la información de los pedidos.");
                IniciaToolBar(true, false, false, true, false, false, true);
            } 
            else 
            {
                leerGuardar.Exec("Exec spp_FormatearTabla 'Pedidos_Cedis__CargaMasiva'  ");
                General.msjUser("Información de Pedidos cargada satisfactoriamente."); 
                IniciaToolBar(true, false, false, false, true, false, true);
            }
        }

        private static DataSet AgregarColumna( DataSet Datos, string Tabla, string Columna, string TipoDeDatos, string ValorDefault )
        {
            DataSet dts = Datos.Copy();
            DataTable dtConceptos;
            DataColumn dtColumnaNueva; 
            clsLeer leer = new clsLeer();

            leer.DataSetClase = Datos;
            if(leer.ExisteTabla(Tabla))
            {
                dtConceptos = leer.Tabla(Tabla);
                if(!leer.ExisteTablaColumna(Tabla, Columna))
                {
                    dtColumnaNueva = new DataColumn(Columna, System.Type.GetType(string.Format("System.{0}", TipoDeDatos)));
                    dtColumnaNueva.DefaultValue = ValorDefault; 
                    dtConceptos.Columns.Add(dtColumnaNueva);

                    ////leer.DataTableClase = dtConceptos;
                    ////while(leer.Leer())
                    ////{
                    ////    leer.GuardarDatos(Columna, ValorDefault);
                    ////}
                    ////dtConceptos = leer.DataTableClase;


                    dts.Tables.Remove(Tabla);
                    dts.Tables.Add(dtConceptos.Copy());
                }
            }

            return dts.Copy();
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
        #endregion Guardar Informacion

        #region Boton_ProcesarRemisiones
        private void btnProcesarRemisiones_Click(object sender, EventArgs e)
        {
            //if (validarIntegracionInventario())
            {
                thGeneraFolios = new Thread(this.IntegrarPedidos);
                thGeneraFolios.Name = "Generar Folios";
                thGeneraFolios.Start();
            }
        }

        //private bool validarIntegracionInventario()
        //{
        //    bool bRegresa = false;
        //    string sMsj = "El proceso de integración de Pedidos generara una salida general de existencias, y dara ingreso como inventario final al contenido del archivo cargado, ¿ Desea continuar ? ";

        //    if (General.msjConfirmar(sMsj) == DialogResult.Yes)
        //    {
        //        bRegresa = true; 
        //    }

        //    return bRegresa; 
        //}

        private void thGeneraFolios_Remisiones()
        {
            IntegrarPedidos();
        }

        private void IntegrarPedidos() 
        {
            bool bContinua = true;
            string sSql = "";
            int iTipo;

 
            
            clsLeer leer = new clsLeer(ref cnn);
            leer.Conexion.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            //if (DtGeneral.ConfirmacionConHuellas)
            //{
            //    sMsjNoEncontrado = "El usuario no tiene permiso para aplicar un ajuste de inventario, verifique por favor.";
            //    ////bContinua = opPermisosEspeciales.VerificarPermisos("AJUSTEINVENTARIO", sMsjNoEncontrado);
            //    bContinua = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("AJUSTEINVENTARIO", sMsjNoEncontrado);
            //}

            //iTipo = rdoVenta.Checked ? 1 : 2;
            iTipo = (int)tpTipoDePedido; 

            if (bContinua)
            {
                if (!cnn.Abrir())
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    cnn.IniciarTransaccion();

                    BloqueaHojas(true);
                    MostrarEnProceso(true, 5);
                    IniciaToolBar(false, false, false, false, false, false, false);

                    sSql = string.Format(
                    "Exec sp_Proceso_Pedidos_Cedis__CargaMasiva_001_Integrar \n" + 
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', \n"+
                    "\t@IdPersonal = '{3}', @Tipo = '{4}', @GUID = '{5}' \n",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado,
                    DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, iTipo, sGUID );

                    if (!leer.Exec(sSql))
                    {
                        bContinua = false;
                        //General.msjError("Ocurrió un Error al Procesar las Remisiones");
                        //Error.GrabarError(leer, "ProcesarRemisiones");
                    }
                    else
                    {
                        if (leer.Leer())
                        {
                            // iFolioCargaMasiva = leer.Campo("FolioCargaMasiva");
                            //sMensaje = string.Format("Poliza de Salida '{0}'.\n\nFolio de Entrada '{1}' ",
                            //    leer.Campo("Folio_Salida"), leer.Campo("Folio_Entrada"));
                            sMensaje = "Información integrada...";
                        }
                    }

                    //if (DtGeneral.ConfirmacionConHuellas && bContinua)
                    //{
                    //    bContinua = opPermisosEspeciales.GrabarPropietarioDeHuella(leer.Campo("Folio_Salida"));
                    //}

                    //if (DtGeneral.ConfirmacionConHuellas && bContinua)
                    //{
                    //    bContinua = opPermisosEspeciales.GrabarPropietarioDeHuella(leer.Campo("Folio_Entrada"));
                    //}

                    BloqueaHojas(false);
                    MostrarEnProceso(false, 5);

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        ////if (iFolioCargaMasiva > 0)
                        ////{
                        ////    ImprimirFoliosRemisionesCargaMasiva();//Imprimir el Listado de Folios de Remisiones que Genero el Proceso
                        ////}

                        //ImprimirCtesNoRegistrados();
                        IniciaToolBar(true, true, true, false, false, false, true);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnProcesar_Click");
                        General.msjError("Ocurrió un error al guardar la Información.");
                        IniciaToolBar(true, true, true, true, true, true, true);
                    }

                    cnn.Cerrar();
                }
            }
        }

        //private void ImprimirFoliosRemisionesCargaMasiva()
        //{
        //    bool bRegresa = false;

        //    DatosCliente.Funcion = "ImprimirFoliosRemisionesCargaMasiva()";
        //    clsImprimir myRpt = new clsImprimir(General.DatosConexion);
        //    // byte[] btReporte = null;

        //    myRpt.RutaReporte = DtGeneral.RutaReportes;
        //    myRpt.NombreReporte = "PtoVta_FolioRemisionesCargaMasiva.rpt"; 

        //    myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
        //    myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
        //    myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
        //    myRpt.Add("IdDistribuidor", sIdDistribuidor);
        //    myRpt.Add("FolioCargaMasiva", iFolioCargaMasiva);

        //    bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

        //    if (!bRegresa)
        //    {
        //        General.msjError("Ocurrió un error al cargar el reporte.");
        //    }
        //}

        private void tmValidacion_Tick(object sender, EventArgs e)
        {
            tmValidacion.Stop();
            tmValidacion.Enabled = false;


            if (!bValidandoInformacion)
            {
                if (bActivarProceso)
                {
                    IniciaToolBar(true, false, false, false, false, true, true);
                }
                else
                {
                    IniciaToolBar(true, false, false, false, true, false, true);
                    if (!bErrorAlValidar)
                    {
                        DllFarmaciaSoft.Inventario.FrmIncidencias f = new DllFarmaciaSoft.Inventario.FrmIncidencias(leer.DataSetClase);
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

        //////private void ImprimirCtesNoRegistrados()
        //////{
        //////    bool bRegresa = false;

        //////    DatosCliente.Funcion = "ImprimirCtesNoRegistrados()";
        //////    clsImprimir myRpt = new clsImprimir(General.DatosConexion);           

        //////    myRpt.RutaReporte = DtGeneral.RutaReportes;
        //////    myRpt.NombreReporte = "PtoVta_RemisionesDist_ClientesNoRegistrados.rpt";

        //////    myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
        //////    myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
        //////    myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
        //////    myRpt.Add("IdDistribuidor", sIdDistribuidor);           

        //////    bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

        //////    if (!bRegresa)
        //////    {
        //////        General.msjError("Ocurrió un error al cargar el reporte.");
        //////    }
        //////}
        #endregion Boton_ProcesarRemisiones 

        private bool GetDriverExcel()
        {
            bool llretval = false; 

            //////string AccessDBAsValue = string.Empty;
            //////RegistryKey rkACDBKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\Installer\Products");
            
            //////if (rkACDBKey != null)
            //////{
            //////    //int lnSubKeyCount = 0;
            //////    //lnSubKeyCount =rkACDBKey.SubKeyCount; 
            //////    foreach (string subKeyName in rkACDBKey.GetSubKeyNames())
            //////    {
            //////        using (RegistryKey RegSubKey = rkACDBKey.OpenSubKey(subKeyName))
            //////        {
            //////            foreach (string valueName in RegSubKey.GetValueNames())
            //////            {
            //////                if (valueName.ToUpper() == "PRODUCTNAME")
            //////                {
            //////                    AccessDBAsValue = (string)RegSubKey.GetValue(valueName.ToUpper());
            //////                    if (AccessDBAsValue.Contains("Access database engine"))
            //////                    {
            //////                        llretval = true;
            //////                        break;
            //////                    }
            //////                }
            //////            }
            //////        }
            //////        if (llretval)
            //////        {
            //////            break;
            //////        }
            //////    }
            //////}

            return llretval; 
        }

        private void tmPantalla_Tick(object sender, EventArgs e)
        {
            //tmPantalla.Enabled = false;
            //if (!DtGeneral.ValidaTransferenciasTransito())
            //{
            //    this.Close();
            //}
        }
    }
}
