using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using Dll_SII_INadro; 

namespace Dll_SII_INadro.Informacion
{
    public partial class FrmIntegrarRemisiones : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeerExcel excel;

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
        Thread thGeneraFolios;

        string sFormato = "###, ###, ###, ##0";
        int iFolioCargaMasiva = 0;
        string sMensaje = "";

        clsAyudas Ayuda;        
        clsConsultas Consultas;

        DataSet dtsEAN;
        DataSet dtsCTE;

        string sFolioPedido = "";
        string sNombreDocumento = "";
        string sContenidoDocumento = "";
        string sSegmentoDocumento = "";

        bool bValidandoInformacion = false;
        bool bCargandoInformacion = false;
        bool bCargandoInformacion_Exito = false;
        bool bSeEncontraronIndicencias = false;
        bool bActivarProceso = false;
        bool bErrorAlValidar = false; 

        public FrmIntegrarRemisiones()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnDll_SII_INadro.DatosApp, this.Name, "");

            ////this.sIdDistribuidor = Fg.PonCeros(IdDistribuidor, 4); 
            leer = new clsLeer(ref cnn);
            excel = new clsLeerExcel();

            Consultas = new clsConsultas(General.DatosConexion, General.Modulo, this.Name, General.Version);
            Ayuda = new clsAyudas(General.DatosConexion, General.Modulo, this.Name, General.Version);

            lst = new clsListView(lstVwInformacion);
            lst.OrdenarColumnas = false; 

            FrameResultado.Height = 330;
            FrameResultado.Width = 800;

            FrameProceso.Top = 250;
            FrameProceso.Left = 116; 
            MostrarEnProceso(false, 0);

            dtsEAN = new DataSet();
            dtsCTE = new DataSet();
        }

        private void FrmIntegrarRemisiones_Load(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        #region Botones 
        private void IniciaToolBar()
        {
            IniciaToolBar(true, true, false, false, false, false, true);
        }

        private void IniciaToolBar(bool Nuevo, bool Abrir, bool Ejecutar, bool Guardar, bool Procesar, bool IntegrarDocumento,  bool Salir)
        {
            btnNuevo.Enabled = Nuevo;
            btnAbrir.Enabled = Abrir;
            btnEjecutar.Enabled = Ejecutar;
            btnGuardar.Enabled = Guardar;
            btnProcesarRemisiones.Enabled = Procesar;
            btnIntegrarDocumento.Enabled = IntegrarDocumento; 
            btnSalir.Enabled = Salir;
        } 

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void InicializarPantalla()
        {
            IniciaToolBar();

            // int iRegistrosHoja = 0;
            // int iRegistrosProcesados = 0;

            lblProcesados.Visible = false;
            lblProcesados.Text = "";

            dtpFechaRegistro.Enabled = false; 
            sFile_In = "";  
            cboHojas.Clear();
            cboHojas.Add();

            sTitulo = "Información "; 
            FrameResultado.Text = sTitulo; 
            Fg.IniciaControles();
            lst.Limpiar();

            sFolioPedido = "";
            sNombreDocumento = "";
            sContenidoDocumento = "";
            sSegmentoDocumento = "";

            ////btnEjecutar.Enabled = false;
            ////btnGuardar.Enabled = false;
            ////btnProcesarRemisiones.Enabled = false;
            ////btnIntegrarDocumento.Visible = false;
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            openExcel.Title = "Archivos de remisiones";
            openExcel.Filter = "Archivos de Excel (*.xlsx)| *.xlsx|Archivos de Excel (*.xls)| *.xls"; 
            openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openExcel.AddExtension = true;
            lblProcesados.Visible = false;

            // if (openExcel.FileName != "")
            if (openExcel.ShowDialog() == DialogResult.OK) 
            {
                sFile_In = openExcel.FileName;

                FileInfo docto = new FileInfo(openExcel.FileName);
                
                sContenidoDocumento = Fg.ConvertirArchivoEnStringB64(openExcel.FileName);
                sNombreDocumento = docto.Name;
                lblDocumentoAIntegrar.Text = sFile_In;

                //CargarArchivo(); 
                IniciaToolBar(false, false, false, false, false, false, false); 
                thReadFile = new Thread(this.CargarArchivo);
                thReadFile.Name = "LeerEstructuraExcel";
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            thGuardarInformacion = new Thread(this.GuardarInformacion_Remisiones);
            thGuardarInformacion.Name = "Guardar información seleccionada";
            thGuardarInformacion.Start(); 
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //////FrmListaFoliosCargaMasivaRemisiones f = new FrmListaFoliosCargaMasivaRemisiones(sIdDistribuidor);
            //////f.ShowDialog();            
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
            IniciaToolBar(true, true, bHabilitar, false, false, false, true);

            BloqueaHojas(false);
            MostrarEnProceso(false, 1); 
            // btnGuardar.Enabled = bHabilitar; 

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
                sTituloProceso = "Generando Folios Remisiones, Carga Masiva";
            }

            FrameProceso.Text = sTituloProceso; 

        }
        #endregion Funciones y Procedimientos Privados 

        #region Guardar Informacion 
        private void thGuardarInformacion_Remisiones()
        {
            ////BloqueaHojas(true);
            ////MostrarEnProceso(true, 3); 
            ////Thread.Sleep(1000);
            ////this.Refresh(); 

            GuardarInformacion_Remisiones();

            ////BloqueaHojas(false);
            ////MostrarEnProceso(false, 3);
            // IniciaToolBar(true, true, true, true, true); 
        }

        private string DarFormato(int Valor)
        {
            return DarFormato(Valor.ToString());
        }

        private string DarFormato(double Valor)
        {
            return DarFormato(Valor.ToString());
        }

        private string DarFormato(string Valor)
        {
            string sRegresa = Valor.Trim();

            sRegresa = sRegresa.Replace("'", "");
            sRegresa = sRegresa.Replace(",", "");

            return sRegresa;
        }

        private void GuardarInformacion_Remisiones()
        {
            bool bRegresa = false;
            string sSql = ""; 
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

            leerGuardar.Exec("Truncate Table INT_ND_Pedidos_CargaMasiva ");
            while (excel.Leer())
            {
                sSql = string.Format("Insert Into INT_ND_Pedidos_CargaMasiva " +
                    " ( CodigoCliente, ReferenciaPedido, CodigoProducto, CodigoEAN, " +
                    " Cantidad, Precio ) \n");
                sSql += string.Format("Select '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                    DarFormato(excel.Campo("Codigo Cliente")), DarFormato(excel.Campo("Referencia Pedido")), 
                    DarFormato(excel.Campo("Codigo Producto")), DarFormato(excel.Campo("Codigo EAN")), 
                    DarFormato(excel.CampoInt("Cantidad")), 
                    DarFormato(excel.CampoDouble("Precio")));

                ////BloqueaHojas(true);
                ////MostrarEnProceso(true, 3);

                if (!leerGuardar.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leerGuardar, "GuardarInformacion_Remisiones"); 
                    break; 
                }
                iRegistrosProcesados++;
                lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato)); 
            }

            BloqueaHojas(false);
            MostrarEnProceso(false, 3);

            if (!bRegresa)
            { 
                General.msjError("Ocurrio un error al guardar la información.");
                IniciaToolBar(true, true, true, true, false, false, true);
            } 
            else 
            {
                General.msjUser("Información guardada satisgactoriamente."); 
                IniciaToolBar(true, true, true, false, true, false, true);
            }
        }
        #endregion Guardar Informacion

        #region Boton_ProcesarRemisiones
        private void btnProcesarRemisiones_Click(object sender, EventArgs e)
        {
            tmValidacion.Enabled = true;
            tmValidacion.Interval = 1000;
            tmValidacion.Start();

            thGeneraFolios = new Thread(this.ValidarRemisiones);
            thGeneraFolios.Name = "Generar Folios Remisiones";
            thGeneraFolios.Start();
        }

        ////private void thGeneraFolios_Remisiones()
        ////{
        ////    ProcesarRemisiones();
        ////}

        private void ValidarRemisiones() 
        {
            bValidandoInformacion = true;
            bActivarProceso = false;
            bErrorAlValidar = false;
            clsLeer leerValidacion = new clsLeer();

            IniciaToolBar(false, false, false, false, false, bActivarProceso, false);
            BloqueaHojas(true);
            MostrarEnProceso(true, 4);
            lblProcesados.Visible = false;

            string sSql = string.Format("Exec spp_Proceso_INT_ND_Pedidos_ValidarDatosDeEntrada '{0}', '{1}', '{2}' ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);


            if (!leer.Exec(sSql))
            {
                bErrorAlValidar = true;
                bActivarProceso = !bActivarProceso;

                Error.GrabarError(leer, "ValidarInformacion()");
                General.msjError("Ocurrió un error al verificar los Pedidos a integrar.");
            }
            else
            {
                leer.RenombrarTabla(1, "Clientes No Encontrados");
                leer.RenombrarTabla(2, "Códigos EAN No Encontrados");
                leer.RenombrarTabla(3, "Productos con Costo Cero");


                leerValidacion.DataTableClase = leer.Tabla(1);   //// Clientes No Encontrados 
                bActivarProceso = leerValidacion.Registros > 0;

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(2);   //// Códigos EAN No Encontrados 
                    bActivarProceso = leerValidacion.Registros > 0;
                }

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(3);   //// Productos con Costo Cero 
                    bActivarProceso = leerValidacion.Registros > 0; 
                }

            }

            bValidandoInformacion = false;
            bActivarProceso = !bActivarProceso;
            BloqueaHojas(false);
            MostrarEnProceso(false, 4);
        }

        private void IntegrarRemisiones()
        {
            bool bContinua = false;
            string sSql = "";
            int iTipoFarmacias = 0;

            bValidandoInformacion = true;
            bActivarProceso = false;
            bErrorAlValidar = false;

            dtsEAN = new DataSet();
            dtsCTE = new DataSet();

            if (rdoAdministradas.Checked)
            {
                iTipoFarmacias = 1;
            }

            if (rdoNoAdministradas.Checked)
            {
                iTipoFarmacias = 2;
            }

            clsLeer leer = new clsLeer(ref cnn);
            leer.Conexion.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            if (!cnn.Abrir())
            {
                Error.LogError(cnn.MensajeError);
                General.msjErrorAlAbriConexion(); 
            }
            else 
            {
                cnn.IniciarTransaccion();

                BloqueaHojas(true);
                MostrarEnProceso(true, 4);
                IniciaToolBar(false, false, false, false, false, false, false);

                sSql = string.Format(" Exec spp_Proceso_INT_ND_Pedidos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado,
                DtGeneral.FarmaciaConectada, Fg.PonCeros(txtIdProveedor.Text, 4), DtGeneral.IdPersonal, iTipoFarmacias,
                General.FechaYMD(dtpFechaPromesaEntrega.Value, "-"));

                if (!leer.Exec(sSql))
                {
                    bContinua = false;
                    //General.msjError("Ocurrio un Error al Procesar las Remisiones");
                    //Error.GrabarError(leer, "ProcesarRemisiones");
                }
                else
                {
                    if (leer.Leer())
                    {
                        iFolioCargaMasiva = leer.CampoInt("FolioPedido");
                        sFolioPedido = leer.Campo("FolioPedido");
                        sMensaje = leer.Campo("Mensaje");

                        //////dtsEAN.Tables.Add(leer.Tabla(2).Copy());
                        //////dtsCTE.Tables.Add(leer.Tabla(3).Copy());
                        bContinua = Integrar_Documento(); 
                    }
                }

                BloqueaHojas(false);
                MostrarEnProceso(false, 4);

                if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    cnn.CompletarTransaccion();  
                    General.msjUser(sMensaje); //Este mensaje lo genera el SP
                    IniciaToolBar(true, false, false, false, false, false, true);

                    ////if (iFolioCargaMasiva == 0)
                    ////{
                    ////    Mostrar_Productos_Clientes_NoEncontrados();
                    ////    IniciaToolBar(true, true, false, false, false, false, true);
                    ////}
                    ////else
                    ////{
                    ////    IniciaToolBar(true, true, true, false, false, true, true);
                    ////    btnIntegrarDocumento.Visible = true;
                    ////}                   

                    
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnProcesarRemisiones_Click");
                    General.msjError("Ocurrio un error al guardar la informacion.");
                    IniciaToolBar(true, true, true, false, false, true, true);
                }

                cnn.Cerrar();
            }

            bValidandoInformacion = false;
            bActivarProceso = !bActivarProceso;

        }

        private bool Integrar_Documento()
        {
            bool bRegresa = true;
            string sSql = "";

            sSegmentoDocumento = cboHojas.Data;

            sSql = string.Format(" Exec spp_Mtto_INT_ND_Pedidos_Enviados_Doctos @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioPedido = '{3}', " +
                " @IdPersonal = '{4}', @Observaciones = '{5}', @NombreDocumento = '{6}', @ContenidoDocumento = '{7}', @SegmentoDocumento = '{8}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioPedido,
                DtGeneral.IdPersonal, txtObservaciones.Text, sNombreDocumento, sContenidoDocumento, sSegmentoDocumento);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true;
                    ////sMensaje = leer.Campo("Mensaje");
                }
            }

            return bRegresa;
        }

        private void ImprimirFoliosRemisionesCargaMasiva()
        {
            bool bRegresa = false;

            DatosCliente.Funcion = "ImprimirFoliosRemisionesCargaMasiva()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "PtoVta_FolioRemisionesCargaMasiva.rpt"; 

            myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("IdDistribuidor", sIdDistribuidor);
            myRpt.Add("FolioCargaMasiva", iFolioCargaMasiva);

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }

        private void ImprimirCtesNoRegistrados()
        {
            bool bRegresa = false;

            DatosCliente.Funcion = "ImprimirCtesNoRegistrados()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);           

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "PtoVta_RemisionesDist_ClientesNoRegistrados.rpt";

            myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("IdDistribuidor", sIdDistribuidor);           

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Boton_ProcesarRemisiones 

        #region Eventos_Proveedor
        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdProveedor.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Proveedores(Fg.PonCeros(txtIdProveedor.Text, 4), "txtIdProveedor_Validating");

                if (leer.Leer())
                {
                    txtIdProveedor.Text = leer.Campo("IdProveedor");
                    lblProveedor.Text = leer.Campo("Nombre");
                }
            }
        }

        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Proveedores("txtIdProveedor_KeyDown");

                if (leer.Leer())
                {
                    txtIdProveedor.Text = leer.Campo("IdProveedor");
                    lblProveedor.Text = leer.Campo("Nombre");
                }
            }
        }
        #endregion Eventos_Proveedor

        private void Mostrar_Productos_Clientes_NoEncontrados()
        {
            FrmProductosClientesNoEncontrados f = new FrmProductosClientesNoEncontrados();

            f.Mostrar_Productos_Clientes(dtsEAN, dtsCTE);
        }

        #region Integrar_Documento_de_Pedidos
        private void btnIntegrarDocumento_Click(object sender, EventArgs e)
        {            
            if (ValidaDatos())
            {
                IntegrarRemisiones(); 
            }
        } 

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado las observaciones, verifique.");
                txtObservaciones.Focus();
            }

            if (bRegresa && !rdoAdministradas.Checked && !rdoNoAdministradas.Checked)
            {
                bRegresa = false;
                General.msjAviso("No ha seleccionado el tipo de Unidad, verifique.");
            }

            if (bRegresa && !rdoTraspasos.Checked && !rdoVtaDirecta.Checked)
            {
                bRegresa = false;
                General.msjAviso("No ha seleccionado el tipo de Remisión, verifique.");
            }

            if (bRegresa && lblDocumentoAIntegrar.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha selecionado el archivo a integrar, verifique.");               
            }

            if (bRegresa && cboHojas.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjAviso("No ha selecionado la hoja del archivo, verifique.");
                cboHojas.Focus();
            }

            return bRegresa;
        }
        #endregion Integrar_Documento_de_Pedidos

        private void tmValidacion_Tick(object sender, EventArgs e)
        {
            tmValidacion.Stop();
            tmValidacion.Enabled = false;

            if (!bValidandoInformacion)
            {
                if (bActivarProceso)
                {
                    IniciaToolBar(true, true, true, false, false, true, true);
                }
                else
                {
                    IniciaToolBar(true, true, false, false, true, false, true);
                    if (!bErrorAlValidar)
                    {
                        FrmIncidencias f = new FrmIncidencias("Incidencias encontradas en archivo de Pedidos", leer.DataSetClase, true);
                        f.ShowDialog();
                        IniciaToolBar(true, true, true, false, !f.ExcluirIncidencias, f.ExcluirIncidencias, true);    
                    }
                }
            }
            else
            {
                tmValidacion.Enabled = true;
                tmValidacion.Start();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Dll_SII_INadro.ObtenerInformacion.clsCliente_INadro f = new ObtenerInformacion.clsCliente_INadro(DtGeneral.CfgIniPuntoDeVenta, General.DatosConexion);

            f.Pedidos_Unidades(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "00000001");

        }
    }
}
