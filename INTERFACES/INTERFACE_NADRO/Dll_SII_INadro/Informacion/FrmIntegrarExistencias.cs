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
    public partial class FrmIntegrarExistencias : FrmBaseExt
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

        clsAyudas Ayuda;        
        clsConsultas Consultas;

        DataSet dtsEAN;
        DataSet dtsCTE;

        string sFolioIntegracion = "";
        string sNombreDocumento = "";
        string sContenidoDocumento = "";
        string sSegmentoDocumento = "";

        bool bValidandoInformacion = false;
        bool bCargandoInformacion = false;
        bool bCargandoInformacion_Exito = false;
        bool bSeEncontraronIndicencias = false;
        bool bActivarProceso = false;
        bool bErrorAlValidar = false; 

        public FrmIntegrarExistencias()
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

            FrameResultado.Height = 390;
            FrameResultado.Width = 800;

            FrameProceso.Top = 170;
            FrameProceso.Left = 116; 
            MostrarEnProceso(false, 0);

            dtsEAN = new DataSet();
            dtsCTE = new DataSet();
        }

        private void FrmIntegrarExistencias_Load(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        #region Botones 
        private void IniciaToolBar()
        {
            IniciaToolBar(true, true, false, false, false, false, true); 
        }

        private void IniciaToolBar(bool Nuevo, bool Abrir, bool Ejecutar, bool Guardar, bool Validar, bool Procesar, bool Salir)
        {
            btnNuevo.Enabled = Nuevo;
            btnAbrir.Enabled = Abrir;
            btnEjecutar.Enabled = Ejecutar;
            btnGuardar.Enabled = Guardar;
            btnValidarDatos.Enabled = Validar; 
            btnIntegrarDocumento.Enabled = Procesar; 
            btnSalir.Enabled = Salir;
        } 

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void InicializarPantalla()
        {
            // int iRegistrosHoja = 0;
            // int iRegistrosProcesados = 0;
            IniciaToolBar(); 

            lblProcesados.Visible = false;
            lblProcesados.Text = ""; 

            sFile_In = "";  
            cboHojas.Clear();
            cboHojas.Add();

            sTitulo = "Información "; 
            FrameResultado.Text = sTitulo; 
            Fg.IniciaControles();
            lst.Limpiar();

            sFolioIntegracion = "";
            sNombreDocumento = "";
            sContenidoDocumento = "";
            sSegmentoDocumento = "";

            ////btnEjecutar.Enabled = false;
            ////btnGuardar.Enabled = false;
            ////btnIntegrarDocumento.Enabled = false;
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
            tmCargaBase.Enabled = true;
            tmCargaBase.Interval = 1000;
            tmCargaBase.Start(); 

            thGuardarInformacion = new Thread(this.GuardarInformacion_Existencias);
            thGuardarInformacion.Name = "Guardar información seleccionada";
            thGuardarInformacion.Start(); 
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

        private void ValidarInformacion()
        {
            bValidandoInformacion = true; 
            bActivarProceso = false;
            bErrorAlValidar = false;
            clsLeer leerValidacion = new clsLeer();

            IniciaToolBar(false, false, false, false, false, bActivarProceso, false);
            BloqueaHojas(true);
            MostrarEnProceso(true, 4);
            lblProcesados.Visible = false;

            string sSql = string.Format("Exec spp_Proceso_INT_ND_Existencias_ValidarDatosDeEntrada '{0}', '{1}', '{2}' ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);


            if (!leer.Exec(sSql))
            {
                bErrorAlValidar = true;
                bActivarProceso = !bActivarProceso; 

                Error.GrabarError(leer, "ValidarInformacion()");
                General.msjError("Ocurrió un error al verificar la Existencia a integrar.");
            }
            else
            {
                leer.RenombrarTabla(1, "Códigos EAN No Encontrados");
                leer.RenombrarTabla(2, "Claves SSA No Concordantes"); 



                leerValidacion.DataTableClase = leer.Tabla(1);   // Codigos EAN No Encontrados  
                bActivarProceso = leerValidacion.Registros > 0;

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(2);   // Claves SSA No Concordantes 
                    bActivarProceso = leerValidacion.Registros > 0;
                }
            }

            bValidandoInformacion = false;
            bActivarProceso = !bActivarProceso;
            BloqueaHojas(false);
            MostrarEnProceso(false, 4);

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
        private void thGuardarInformacion_Existencias()
        {
            ////BloqueaHojas(true);
            ////MostrarEnProceso(true, 3); 
            ////Thread.Sleep(1000);
            ////this.Refresh(); 

            GuardarInformacion_Existencias();

            ////BloqueaHojas(false);
            ////MostrarEnProceso(false, 3);
            // IniciaToolBar(true, true, true, true, true); 
        }

        private string DarFormato(int Valor)
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

        private void GuardarInformacion_Existencias()
        {
            bCargandoInformacion = true;
            bCargandoInformacion_Exito = false; 

            bool bRegresa = false;
            string sSql = ""; 
            clsLeer leerGuardar = new clsLeer(ref cnn);
            clsLeer leerDatos = new clsLeer(); 

            BloqueaHojas(true);
            MostrarEnProceso(true, 3);
            IniciaToolBar(false, false, false, false, false, false, false); 

            lblProcesados.Visible = true;
            lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato));


            // leerGuardar.DataSetClase = excel.DataSetClase;
            leerDatos.DataSetClase = excel.DataSetClase;
            leerDatos.RegistroActual = 1;
            bRegresa = leerDatos.Registros > 0;
            iRegistrosProcesados = 0;

            leerGuardar.Exec("Truncate Table INT_ND_Existencias_CargaMasiva ");
            while (leerDatos.Leer()) 
            {
                sSql = string.Format("Insert Into INT_ND_Existencias_CargaMasiva " +
                    " ( ClaveSSA_ND, CodigoEAN_ND, Cantidad ) \n");
                sSql += string.Format("Select '{0}', '{1}', '{2}' ",
                    DarFormato(leerDatos.Campo("Clave SSA")),
                    DarFormato(leerDatos.Campo("Codigo EAN")),
                    DarFormato(leerDatos.CampoInt("Cantidad")));

                ////BloqueaHojas(true);
                ////MostrarEnProceso(true, 3);

                if (!leerGuardar.Exec(sSql))
                {
                    bRegresa = false; 
                    Error.GrabarError(leerGuardar, "GuardarInformacion_Existencias"); 
                    break; 
                }
                iRegistrosProcesados++;
                lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato)); 
            }

            BloqueaHojas(false);
            MostrarEnProceso(false, 3);

            ////if (!bRegresa)
            ////{ 
            ////    //// General.msjError("Ocurrio un error al guardar la información.");
            ////    IniciaToolBar(true, true, true, true, false, false, true);
            ////} 
            ////else 
            ////{
            ////    //// General.msjUser("Información guardada satisgactoriamente.");
            ////    IniciaToolBar(true, true, true, false, true, false, true);
            ////}

            bCargandoInformacion_Exito = bRegresa; 
            bCargandoInformacion = false; 
        }
        #endregion Guardar Informacion

        #region Boton_ProcesarRemisiones
        private void btnIntegrarDocumento_Click(object sender, EventArgs e)
        {
            thGeneraFolios = new Thread(this.ProcesoIntegrarExistencia);
            thGeneraFolios.Name = "Integrar Existencias";
            thGeneraFolios.Start();
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

        private void Mostrar_Productos_Clientes_NoEncontrados()
        {
            FrmProductosClientesNoEncontrados f = new FrmProductosClientesNoEncontrados();

            f.Mostrar_Productos_Clientes(dtsEAN, dtsCTE);
        }

        #region Integrar documentos de Existencias
        private void ProcesoIntegrarExistencia()
        {
            bool bContinua = false;

            if (ValidaDatos())
            {
                if (!cnn.Abrir())
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }
                else
                {
                    cnn.IniciarTransaccion();

                    bContinua = Integrar_Existencias();

                    if (bContinua) // Si no Ocurrió ningun error se llevan a cabo las transacciones.
                    {                        
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP                        
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        Error.GrabarError(leer, "btnGuardar_Click");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la información.");

                    }

                    cnn.Cerrar();
                }
            }
        }

        private bool Integrar_Existencias()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_Mtto_INT_ND_Existencias @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdPersonal = '{3}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal );

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true;
                    sFolioIntegracion = leer.Campo("FolioIntegracion");
                    sMensaje = leer.Campo("Mensaje");
                }
            }

            return bRegresa;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

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
        #endregion Integrar documentos de Existencias  

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
                        FrmIncidencias f = new FrmIncidencias("Incidencias encontradas en archivo de Existencias", leer.DataSetClase, true);
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

        private void tmCargaBase_Tick(object sender, EventArgs e)
        {
            tmCargaBase.Stop();
            tmCargaBase.Enabled = false;

            if (!bCargandoInformacion)
            {
                if (!bCargandoInformacion_Exito)
                {
                    IniciaToolBar(true, true, true, true, false, false, true); 
                    General.msjError("Ocurrio un error al cargar la información.");
                }
                else
                {
                    IniciaToolBar(true, true, true, false, true, false, true);
                    ////General.msjUser("Información cargarda satisgactoriamente."); 
                }
            }
            else
            {
                tmCargaBase.Enabled = true;
                tmCargaBase.Start();
            }
        }
    }
}
