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

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace DllFarmaciaSoft.Inventario
{
    public partial class FrmIntegrarInventario_Externo : FrmBaseExt
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
        Thread thValidarInformacion; 
        Thread thGeneraFolios;

        string sFormato = "###, ###, ###, ##0";
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
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        public FrmIntegrarInventario_Externo()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

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
            excel = new clsLeerExcel();

            lst = new clsListView(lstVwInformacion);
            lst.OrdenarColumnas = false; 

            FrameResultado.Height = 350;
            FrameResultado.Width = 800;

            FrameProceso.Top = 170;
            FrameProceso.Left = 116; 
            MostrarEnProceso(false, 0);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sPersonal);
        }

        private void FrmIntegrarInventario_Externo_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);

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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // int iRegistrosHoja = 0;
            // int iRegistrosProcesados = 0;

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

            IniciaToolBar(true, true, false, false, false, false, true);

            if (!DtGeneral.EsAlmacen)
            {
                IniciaToolBar( false, false, false, false, false, false, true);
                General.msjAviso("Este módulo es exclusivo para Almacenes, se deshabilitaran todas las opciones."); 
            }

        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            openExcel.Title = "Archivos de inventario";
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
            thGuardarInformacion = new Thread(this.GuardarInformacion_Inventario);
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

        private void LeerExcel()
        {
            string sHoja = "";
            bool bHabilitar = false;
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

        private void ValidarInformacion()
        {
            ////tmValidacion.Enabled = true;
            ////tmValidacion.Interval = 1000;
            ////tmValidacion.Start(); 

            bValidandoInformacion = true; 
            bActivarProceso = false;
            bErrorAlValidar = false; 
            clsLeer leerValidacion = new clsLeer(); 

            IniciaToolBar(false, false, false, false, false, bActivarProceso, false); 
            BloqueaHojas(true);
            MostrarEnProceso(true, 4);
            lblProcesados.Visible = false; 

            string sSql = string.Format("Exec sp_Proceso_IntegrarInventarioExterno_000_Validar_Datos_De_Entrada '{0}', '{1}', '{2}' ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);


            if (!leer.Exec(sSql))
            {
                bErrorAlValidar = true; 
                bActivarProceso = !bActivarProceso;

                Error.GrabarError(leer, "ValidarInformacion()"); 
                General.msjError("Ocurrió un error al verificar el inventario a integrar."); 
            }
            else
            {
                leer.RenombrarTabla(1, "Pasillos");
                leer.RenombrarTabla(2, "Estantes");
                leer.RenombrarTabla(3, "Entrepaños");
                leer.RenombrarTabla(4, "Error de caducidades");
                leer.RenombrarTabla(5, "EAN Multiples caducidades");
                leer.RenombrarTabla(6, "EAN no registrados"); 


                leerValidacion.DataTableClase = leer.Tabla(1);   // Pasillos  
                bActivarProceso = leerValidacion.Registros > 0;

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(2);   // Estantes 
                    bActivarProceso = leerValidacion.Registros > 0;
                }

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(3);   // Entrepaños  
                    bActivarProceso = leerValidacion.Registros > 0;
                }

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(4);   // Error Caducidades  
                    bActivarProceso = leerValidacion.Registros > 0;
                }

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(5);   // EAN multiples Caducidades  
                    bActivarProceso = leerValidacion.Registros > 0;
                }

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(6);   // EAN No En Catalogo 
                    bActivarProceso = leerValidacion.Registros > 0;
                }
            }


            bValidandoInformacion = false; 
            bActivarProceso = !bActivarProceso; 
            BloqueaHojas(false);
            MostrarEnProceso(false, 4);

            ////if (bActivarProceso)
            ////{
            ////    IniciaToolBar(true, true, true, false, false, true, true); 
            ////}
            ////else
            ////{
            ////    IniciaToolBar(true, true, true, false, true, false, true);
            ////    if (!bErrorAlValidar)
            ////    {
            ////        FrmIncidencias f = new FrmIncidencias(leer.DataSetClase);
            ////        f.ShowDialog(); 
            ////    }
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
                sTituloProceso = "Integrando inventario ..... ";
            }

            FrameProceso.Text = sTituloProceso; 

        }
        #endregion Funciones y Procedimientos Privados 

        #region Guardar Informacion 
        private void thGuardarInformacion_Inventario()
        {
            ////BloqueaHojas(true);
            ////MostrarEnProceso(true, 3); 
            ////Thread.Sleep(1000);
            ////this.Refresh(); 

            GuardarInformacion_Inventario();

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

        private void GuardarInformacion_Inventario()
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

            leerGuardar.Exec("Truncate Table INV__Inventario_CargaMasiva ");
            while (excel.Leer())
            {
                sSql = string.Format("Insert Into INV__Inventario_CargaMasiva " + 
                    " ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, " +
                    " IdPasillo, IdEstante, IdEntrepaño, CodigoEAN, ClaveLote, Caducidad, Cantidad ) \n");
                sSql += string.Format("Select '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}'  ", 
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                    DarFormato("01"),  // Sub-Farmacia de Venta
                    DarFormato(excel.Campo("IdPasillo")),
                    DarFormato(excel.Campo("IdEstante")),
                    DarFormato(excel.Campo("IdEntrepaño")),
                    DarFormato(excel.Campo("CodigoEAN")),
                    DarFormato(excel.Campo("ClaveLote")),
                    DarFormato(excel.Campo("Caducidad")),
                    excel.CampoInt("Cantidad")
                    );

                ////BloqueaHojas(true);
                ////MostrarEnProceso(true, 3);

                if (!leerGuardar.Exec(sSql))
                {
                    bRegresa = false;
                    //leerGuardar.Exec("Truncate Table INV__Inventario_CargaMasiva ");
                    Error.GrabarError(leerGuardar, "GuardarInformacion_Inventario"); 
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
                IniciaToolBar(true, true, true, true, false, false, true);
            } 
            else 
            {
                leerGuardar.Exec("Exec spp_FormatearTabla 'INV__Inventario_CargaMasiva'  ");
                General.msjUser("Información de inventario cargada satisfactoriamente."); 
                IniciaToolBar(true, true, true, false, true, false, true);
            }
        }
        #endregion Guardar Informacion

        #region Boton_ProcesarRemisiones
        private void btnProcesarRemisiones_Click(object sender, EventArgs e)
        {
            if (validarIntegracionInventario())
            {
                thGeneraFolios = new Thread(this.IntegrarInventarioExterno);
                thGeneraFolios.Name = "Generar Folios Remisiones";
                thGeneraFolios.Start();
            }
        }

        private bool validarIntegracionInventario()
        {
            bool bRegresa = false;
            string sMsj = "El proceso de integración de inventario generara una salida general de existencias, y dara ingreso como inventario final al contenido del archivo cargado, ¿ Desea continuar ? ";

            if (General.msjConfirmar(sMsj) == DialogResult.Yes)
            {
                bRegresa = true; 
            }

            return bRegresa; 
        }

        private void thGeneraFolios_Remisiones()
        {
            IntegrarInventarioExterno();
        }

        private void IntegrarInventarioExterno() 
        {
            bool bContinua = true;
            string sSql = ""; 
 
            
            clsLeer leer = new clsLeer(ref cnn);
            leer.Conexion.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            if (DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "El usuario no tiene permiso para aplicar un ajuste de inventario, verifique por favor.";
                ////bContinua = opPermisosEspeciales.VerificarPermisos("AJUSTEINVENTARIO", sMsjNoEncontrado);
                bContinua = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("AJUSTEINVENTARIO", sMsjNoEncontrado);
            }

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

                    sSql = string.Format(" Exec sp_Proceso_IntegrarInventarioExterno '{0}', '{1}', '{2}', '{3}' ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado,
                    DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal);

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
                            sMensaje = string.Format("Poliza de Salida '{0}'.\n\nFolio de Entrada '{1}' ",
                                leer.Campo("Folio_Salida"), leer.Campo("Folio_Entrada"));
                        }
                    }

                    if (DtGeneral.ConfirmacionConHuellas && bContinua)
                    {
                        bContinua = opPermisosEspeciales.GrabarPropietarioDeHuella(leer.Campo("Folio_Salida"));
                    }

                    if (DtGeneral.ConfirmacionConHuellas && bContinua)
                    {
                        bContinua = opPermisosEspeciales.GrabarPropietarioDeHuella(leer.Campo("Folio_Entrada"));
                    }

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
                        Error.GrabarError(leer, "btnProcesarRemisiones_Click");
                        General.msjError("Ocurrió un error al guardar la Información.");
                        IniciaToolBar(true, true, true, true, true, true, true);
                    }

                    cnn.Cerrar();
                }
            }
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
                    IniciaToolBar(true, true, true, false, true, false, true);
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
            tmPantalla.Enabled = false;
            if (!DtGeneral.ValidaTransferenciasTransito())
            {
                this.Close();
            }
        }
    }
}
