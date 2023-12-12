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
    public partial class FrmIntegrarCuadrosBasicos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerDts;
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
        bool bArchivoDeTextoCargado = false; 

        public FrmIntegrarCuadrosBasicos()
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

        private void FrmIntegrarCuadrosBasicos_Load(object sender, EventArgs e)
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

            bArchivoDeTextoCargado = false; 
            lblProcesados.Visible = false;
            lblProcesados.Text = ""; 

            sFile_In = "";  
            ////cboHojas.Clear();
            ////cboHojas.Add();

            sTitulo = "Información "; 
            FrameResultado.Text = sTitulo; 
            Fg.IniciaControles();
            nmDepurarPrioridad.Enabled = false; 
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

        private void btnLeerArchivoDeTexto_Click(object sender, EventArgs e)
        {
            FrmConvertir_TXT_To_XLS f = new FrmConvertir_TXT_To_XLS();
            f.ShowDialog();

            bArchivoDeTextoCargado = false; 
            if (f.ArchivoCargado)
            {
                bArchivoDeTextoCargado = true; 
                leerDts = new clsLeer();
                leerDts.DataSetClase = f.Informacion;
                excel.DataSetClase = f.Informacion; 

                thLoadFile = new Thread(this.thLeerDataset);
                thLoadFile.Name = "LeerExcelDataset";
                thLoadFile.Start(); 
            }
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
                ////lblDocumentoAIntegrar.Text = sFile_In;

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

            thGuardarInformacion = new Thread(this.GuardarInformacion_CuadrosBasicos);
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

            ////cboHojas.Clear();
            ////cboHojas.Add();
            lst.Limpiar();
            Thread.Sleep(1000);

            bHabilitar = excel.Hojas.Registros > 0;
            while (excel.Hojas.Leer()) 
            {
                sHoja = excel.Hojas.Campo("Hoja");
                ////cboHojas.Add(sHoja, sHoja); 
            } 

            ////cboHojas.SelectedIndex = 0;
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
            //////excel.LeerHoja(cboHojas.Data);

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


        private void thLeerDataset()
        {
            BloqueaHojas(true);
            MostrarEnProceso(true, 2);
            lblProcesados.Visible = false;

            LeerDataset();

            BloqueaHojas(false);
            MostrarEnProceso(false, 2);
        }

        private bool LeerDataset()
        {
            bool bRegresa = false;

            IniciaToolBar(false, false, false, bRegresa, false, false, false);
            FrameResultado.Text = sTitulo;
            lst.Limpiar();
            ////excel.LeerHoja(cboHojas.Data);

            FrameResultado.Text = sTitulo;
            if (leerDts.Leer())
            {
                bRegresa = true;
                iRegistrosHoja = leerDts.Registros;
                FrameResultado.Text = string.Format("{0}: {1} registros ", sTitulo, iRegistrosHoja.ToString(sFormato));
                lst.CargarDatos(leerDts.DataSetClase, true, true);
            }

            // btnGuardar.Enabled = bRegresa;
            IniciaToolBar(true, true, true, bRegresa, false, false, true);
            return bRegresa;
        }

        private void BloqueaHojas(bool Bloquear)
        {
            ////cboHojas.Enabled = !Bloquear; 
        }

        private void ValidarInformacion()
        {
            bValidandoInformacion = true; 
            bActivarProceso = false;
            bErrorAlValidar = false;
            clsLeer leerValidacion = new clsLeer();

            IniciaToolBar(false, false, false, false, false, bActivarProceso, false);
            BloqueaHojas(true);
            MostrarEnProceso(true, 3);
            lblProcesados.Visible = false;

            string sSql = string.Format("Exec spp_Proceso_INT_ND_CuadrosBasicos_ValidarDatosDeEntrada  " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Prioridades_Excluir = '{3}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, (int)nmDepurarPrioridad.Value);


            if (!leer.Exec(sSql))
            {
                bErrorAlValidar = true;
                bActivarProceso = !bActivarProceso; 

                Error.GrabarError(leer, "ValidarInformacion()");
                General.msjError("Ocurrió un error al verificar la Existencia a integrar.");
            }
            else
            {
                leer.RenombrarTabla(1, "Diferencias en descripciones");
                leer.RenombrarTabla(2, "Diferencias en precios de venta");
                leer.RenombrarTabla(3, "Diferencias en precios de servicio");
                leer.RenombrarTabla(4, "Claves SSA - Anexo nuevas");
                leer.RenombrarTabla(5, "Claves SSA - Anexo eliminadas");
                leer.RenombrarTabla(6, "Claves SSA nuevas");
                leer.RenombrarTabla(7, "Claves SSA eliminadas");


                leerValidacion.DataTableClase = leer.Tabla(1);   ////// Diferencias en descripciones 
                bActivarProceso = leerValidacion.Registros > 0;

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(2);   ////// Diferencias en precios de venta 
                    bActivarProceso = leerValidacion.Registros > 0;
                }

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(3);   ////// Diferencias en precios de servicio 
                    bActivarProceso = leerValidacion.Registros > 0;
                }

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(4);   ////// Claves SSA - Anexo nuevas  
                    bActivarProceso = leerValidacion.Registros > 0;
                }

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(5);   ////// Claves SSA - Anexo eliminadas  
                    bActivarProceso = leerValidacion.Registros > 0;
                }

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(6);   ////// Claves SSA nuevas
                    bActivarProceso = leerValidacion.Registros > 0;
                }

                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = leer.Tabla(7);   ////// Claves SSA eliminadas  
                    bActivarProceso = leerValidacion.Registros > 0;
                }
            }

            bValidandoInformacion = false;
            bActivarProceso = !bActivarProceso;
            BloqueaHojas(false);
            MostrarEnProceso(false, 3);

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
        private void thGuardarInformacion_CuadrosBasicos()
        {
            ////BloqueaHojas(true);
            ////MostrarEnProceso(true, 3); 
            ////Thread.Sleep(1000);
            ////this.Refresh(); 

            GuardarInformacion_CuadrosBasicos();

            ////BloqueaHojas(false);
            ////MostrarEnProceso(false, 3);
            // IniciaToolBar(true, true, true, true, true); 
        }

        private string DarFormato(double Valor)
        {
            return DarFormato(Valor.ToString());
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

        private string DarFormatoTexto(string Valor)
        {
            string sRegresa = Valor.Trim();

            if (Fg.Left(sRegresa, 1) == "'")
            {
                ////sRegresa = sRegresa.Replace("'", "");
                sRegresa = Fg.Mid(sRegresa, 2); 
            }

            if (Fg.Right(sRegresa, 1) == "'")
            {
                ////sRegresa = sRegresa.Replace("'", "");
                sRegresa = Fg.Mid(sRegresa, 1, sRegresa.Length-1); 
            }

            if (sRegresa.Contains("'"))
            {
                sRegresa = sRegresa.Replace("'", "´´´");
            }

            return sRegresa;
        }

        private void GuardarInformacion_CuadrosBasicos()
        {
            string sContrato = "";
            string sPrioridad = "";
            string sNombrePrograma = "";
            string sIdAnexo = "";
            string sNombreAnexo;
            string sClaveSSA = "";
            string sClaveSSA_Mascara = "";
            string sDescripcion_Mascara = "";
            int iIva = 0;
            double dPrecioVenta = 0;
            double dPrecioServicio = 0;

            string sIva = "";
            string sPrecioVenta = "";
            string sPrecioServicio = "";

            string sLote = "";
            string sUnidadDeMedida = "";
            double dContenidoPaquete = 0;
            string sContenidoPaquete = "0"; 
            string sFechaVigencia = ""; 

            bCargandoInformacion = true;
            bCargandoInformacion_Exito = false; 

            bool bRegresa = false;
            string sSql = ""; 
            clsLeer leerGuardar = new clsLeer(ref cnn);

            BloqueaHojas(true);
            MostrarEnProceso(true, 3);
            IniciaToolBar(false, false, false, false, false, false, false); 

            lblProcesados.Visible = true;
            lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato));


            //// leerGuardar.DataSetClase = excel.DataSetClase;
            excel.RegistroActual = 1;
            bRegresa = excel.Registros > 0;
            iRegistrosProcesados = 0;

            leerGuardar.Exec("Truncate Table INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva       "); 
            while (excel.Leer()) 
            {
                sContrato = excel.Campo("Contrato");
                sPrioridad = excel.Campo("Prioridad");
                sNombrePrograma = excel.Campo("Nombre Programa");
                sIdAnexo = excel.Campo("Id Anexo");
                sNombreAnexo = excel.Campo("Nombre Anexo");

                sClaveSSA = excel.Campo("Clave SSA");
                sClaveSSA_Mascara = excel.Campo("Clave SSA Mascara");
                sDescripcion_Mascara = excel.Campo("Descripcion Mascara");
                iIva = excel.CampoInt("Iva");
                dPrecioVenta = excel.CampoDouble("Precio Venta");
                dPrecioServicio = excel.CampoDouble("Precio Servicio");


                sIva = excel.Campo("Iva");
                sPrecioVenta = excel.Campo("Precio Venta");
                sPrecioServicio = excel.Campo("Precio Servicio");

                sLote = excel.Campo("Lote");
                sUnidadDeMedida = excel.Campo("UnidadDeMedida");

                sContenidoPaquete = excel.Campo("ContenidoPaquete");
                sFechaVigencia = excel.Campo("Vigencia");


                if (sClaveSSA.Contains("600820054") || sClaveSSA.Contains("9990001019"))
                {
                    sClaveSSA = excel.Campo("Clave SSA");
                }

                sSql = string.Format("Insert Into INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva " +  
                    " ( IdEstado, Contrato, Prioridad, NombrePrograma, IdAnexo, NombreAnexo, ClaveSSA_ND, ClaveSSA_Mascara, " + 
                    " ManejaIva, PrecioVenta, PrecioServicio, Descripcion_Mascara ) \n");

                sSql = "Exec spp_Mtto_INT_ND_CuadrosBasicos_CargaMasiva ";
                sSql += string.Format(" @IdEstado = '{0}', @Contrato = '{1}', @Prioridad = '{2}', @NombrePrograma = '{3}', " + 
                    " @IdAnexo = '{4}', @NombreAnexo = '{5}', @ClaveSSA_ND = '{6}', @ClaveSSA_Mascara = '{7}', @ManejaIva = '{8}', " + 
                    " @PrecioVenta = '{9}', @PrecioServicio = '{10}', @Descripcion_Mascara = '{11}', " +
                    " @Lote = '{12}', @UnidadDeMedida = '{13}', @ContenidoPaquete = '{14}', @Vigencia = '{15}' ", 
                    DtGeneral.EstadoConectado,
                    DarFormato(sContrato),
                    DarFormato(sPrioridad),
                    DarFormato(sNombrePrograma),
                    DarFormato(sIdAnexo),
                    DarFormato(sNombreAnexo),

                    DarFormato(sClaveSSA),
                    DarFormato(sClaveSSA_Mascara),
                    DarFormato(sIva),
                    DarFormato(sPrecioVenta),
                    DarFormato(sPrecioServicio),
                    DarFormatoTexto(sDescripcion_Mascara), 
                    DarFormatoTexto(sLote), 
                    DarFormatoTexto(sUnidadDeMedida),
                    DarFormato(sContenidoPaquete),
                    DarFormatoTexto(sFechaVigencia) 
                    );


                ////BloqueaHojas(true);
                ////MostrarEnProceso(true, 3);

                if (sIdAnexo != "") 
                {
                    if (!leerGuardar.Exec(sSql)) 
                    {
                        bRegresa = false; 
                        Error.GrabarError(leerGuardar, "GuardarInformacion_CuadrosBasicos"); 
                        break; 
                    }
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
            thGeneraFolios = new Thread(this.ProcesoIntegrar_CuadrosBasicos);
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
        private void ProcesoIntegrar_CuadrosBasicos()
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

                    bContinua = Integrar_CuadrosBasicos();

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

        private bool Integrar_CuadrosBasicos()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_Mtto_INT_ND_CuadrosBasicos @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdPersonal = '{3}' ",
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

            if (!bArchivoDeTextoCargado)
            {
                ////if (bRegresa && lblDocumentoAIntegrar.Text.Trim() == "")
                ////{
                ////    bRegresa = false;
                ////    General.msjAviso("No ha selecionado el archivo a integrar, verifique.");
                ////}

                ////if (bRegresa && cboHojas.SelectedIndex == 0)
                ////{
                ////    bRegresa = false;
                ////    General.msjAviso("No ha selecionado la hoja del archivo, verifique.");
                ////    cboHojas.Focus();
                ////}
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
                        FrmIncidencias f = new FrmIncidencias("Incidencias encontradas en archivo de Cuadros Básicos", leer.DataSetClase, true);
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
                    BloqueaHojas(false);
                    MostrarEnProceso(false, 0); 
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

        private void chkDepurarPrioridades_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDepurarPrioridades.Checked)
            {
                nmDepurarPrioridad.Value = 0;
                nmDepurarPrioridad.Enabled = false;
            }
            else
            {
                nmDepurarPrioridad.Enabled = true;
                nmDepurarPrioridad.Focus(); 
            }
        }
    }
}
