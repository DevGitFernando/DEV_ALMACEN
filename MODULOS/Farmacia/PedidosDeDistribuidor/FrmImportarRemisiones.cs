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

using DllFarmaciaSoft; 

namespace Farmacia.PedidosDeDistribuidor
{
    public partial class FrmImportarRemisiones : FrmBaseExt
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

        public FrmImportarRemisiones(string IdDistribuidor)
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            this.sIdDistribuidor = Fg.PonCeros(IdDistribuidor, 4); 
            leer = new clsLeer(ref cnn);
            excel = new clsLeerExcel();

            lst = new clsListView(lstVwInformacion);
            lst.OrdenarColumnas = false; 

            FrameResultado.Height = 350;
            FrameResultado.Width = 800;

            FrameProceso.Top = 170;
            FrameProceso.Left = 116; 
            MostrarEnProceso(false, 0); 
        }

        private void FrmImportarRemisiones_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
        }

        #region Botones 
        private void IniciaToolBar(bool Nuevo, bool Abrir, bool Ejecutar, bool Guardar, bool Procesar, bool Salir)
        {
            btnNuevo.Enabled = Nuevo;
            btnAbrir.Enabled = Abrir;
            btnEjecutar.Enabled = Ejecutar;
            btnGuardar.Enabled = Guardar;
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
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            openExcel.Title = "Archivos de remisiones";
            openExcel.Filter = "Archivos de Excel (*.xls;*.xlsx)| *.xls;*.xlsx";
            openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openExcel.AddExtension = true;
            lblProcesados.Visible = false;

            // if (openExcel.FileName != "")
            if (openExcel.ShowDialog() == DialogResult.OK) 
            {
                sFile_In = openExcel.FileName;
                
                //CargarArchivo(); 
                IniciaToolBar(false, false, false, false, false, false); 
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
            FrmListaFoliosCargaMasivaRemisiones f = new FrmListaFoliosCargaMasivaRemisiones(sIdDistribuidor);
            f.ShowDialog();            
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
            IniciaToolBar(true, true, bHabilitar, false, false, true);

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
            
            IniciaToolBar(false, false, false, bRegresa, false, false);
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
            IniciaToolBar(true, true, true, bRegresa, false, true); 
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

        private void GuardarInformacion_Remisiones()
        {
            bool bRegresa = false;
            string sSql = ""; 
            clsLeer leerGuardar = new clsLeer(ref cnn);

            BloqueaHojas(true);
            MostrarEnProceso(true, 3);
            IniciaToolBar(false, false, false, false, false, false); 

            lblProcesados.Visible = true;
            lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato));


            // leerGuardar.DataSetClase = excel.DataSetClase;
            excel.RegistroActual = 1;
            bRegresa = excel.Registros > 0;
            iRegistrosProcesados = 0; 

            leerGuardar.Exec("Truncate Table Remisiones_CargaMasiva ");
            while (excel.Leer())
            {
                sSql = string.Format("Insert Into Remisiones_CargaMasiva " + 
                    " ( IdEmpresa, IdEstado, IdFarmacia, IdDistribuidor, " +
                    " CodigoCliente, Referencia, FechaDocumento, EsConsignacion, ClaveSSA, Cantidad ) \n");
                sSql += string.Format("Select '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ", 
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sIdDistribuidor,
                    excel.Campo("Cliente"), excel.Campo("Referencia"), 
                    General.FechaYMD(excel.CampoFecha("FechaFactura")), 
                    excel.Campo("TipoRemision"), excel.Campo("ClaveSSA"), excel.CampoInt("Cantidad"));

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
                General.msjError("Ocurrió un error al guardar la información.");
                IniciaToolBar(true, true, true, true, false, true);
            } 
            else 
            {
                General.msjUser("Información guardada satisfactoriamente."); 
                IniciaToolBar(true, true, true, false, true, true);
            }
        }
        #endregion Guardar Informacion

        #region Boton_ProcesarRemisiones
        private void btnProcesarRemisiones_Click(object sender, EventArgs e)
        {
            thGeneraFolios = new Thread(this.ProcesarRemisiones);
            thGeneraFolios.Name = "Generar Folios Remisiones";
            thGeneraFolios.Start();
        }

        private void thGeneraFolios_Remisiones()
        {
            ProcesarRemisiones();
        }

        private void ProcesarRemisiones() 
        {
            bool bContinua = true;
            string sSql = "";            
            
            clsLeer leer = new clsLeer(ref cnn);
            leer.Conexion.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                BloqueaHojas(true);
                MostrarEnProceso(true, 4);
                IniciaToolBar(false, false, false, false, false, false);

                sSql = string.Format(" Exec spp_Proceso_RemisionesDeDistribuidor '{0}', '{1}', '{2}', '{3}', '{4}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado,
                DtGeneral.FarmaciaConectada, sIdDistribuidor, DtGeneral.IdPersonal);

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
                        iFolioCargaMasiva = leer.CampoInt("FolioCargaMasiva");
                        sMensaje = leer.Campo("Mensaje");
                    }
                }

                BloqueaHojas(false);
                MostrarEnProceso(false, 4);

                if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    cnn.CompletarTransaccion();
                    General.msjUser(sMensaje); //Este mensaje lo genera el SP
                    if (iFolioCargaMasiva > 0)
                    {
                        ImprimirFoliosRemisionesCargaMasiva();//Imprimir el Listado de Folios de Remisiones que Genero el Proceso
                    }

                    ImprimirCtesNoRegistrados();
                    IniciaToolBar(true, true, true, false, false, true);
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnProcesarRemisiones_Click");
                    General.msjError("Ocurrió un error al guardar la información.");
                    IniciaToolBar(true, true, true, false, false, true);
                }

                cnn.Cerrar();
            }
            else
            {
                Error.LogError(cnn.MensajeError);
                General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
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

    }
}
