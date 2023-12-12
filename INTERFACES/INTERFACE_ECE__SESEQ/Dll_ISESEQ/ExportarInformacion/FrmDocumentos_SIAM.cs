﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft; 
using DllFarmaciaSoft.Inventario; 
using DllFarmaciaSoft.Reporteador;

namespace Dll_ISESEQ.ExportarInformacion
{
    public enum TipoDeDocumento
    {
        Ninguno = 0, 
        Ventas = 1, 
        Pedido_Ventas = 2, 
        Transferencias = 3, 
        Pedido_Transferencias = 4 
    }

    public partial class FrmDocumentos_SIAM : FrmBaseExt 
    {
        enum Cols 
        {
            Fecha = 1, Folio = 2, 
            IdCliente = 3, IdSubCliente = 4, 
            IdPrograma = 5, IdSubPrograma = 6, 
            Programa = 7, Procesar = 8, Procesado = 9   
        }


        clsDatosConexion DatosDeConexion = new clsDatosConexion(); 
        clsConexionSQL cnn;  // = new clsConexionSQL(General.DatosConexion); 
        clsConexionSQL cnnUnidad;  
        clsLeer leer;
        clsLeer leerLocal;
        clsLeer leerCuadrosDeAtencion;
        clsLeer leer_ActualizarPrecios; 
        clsLeerWebExt leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        clsGrid grid;
        FrmListaDeSubFarmacias SubFarmacias;
        DataSet dtsProgramas, dtsSubProgramas;        

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion.xls";
        clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;
        clsLeer leerSubFarmacias;
        clsLeer leerProgramas_Catalogo;
        clsLeer leerSubProgramas_Catalogo;
        clsLeer leerProgramas;
        clsLeer leerSubProgramas;

        string sSqlFarmacias = "";
        // string sUrl;
        // string sHost = "";
        // string sUrl_RutaReportes = "";
        string sSubFarmacias = "";

        clsDatosCliente DatosCliente;
        //wsFarmacia.wsCnnCliente conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        DataSet dtsEstados = new DataSet();
        DataSet dtsJurisdicciones = new DataSet(); 

        string sIdPublicoGral = GnFarmacia.PublicoGral;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;


        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        // bool bSeEjecuto = false; 

        FolderBrowserDialog folder; // = new FolderBrowserDialog(); 
        string sRutaDestino = "";
        string sRutaDestino_Archivos = ""; 
        bool bFolderDestino = false;

        string sImpresion_Precios = "IMPRESION_VENTA_DETALLADA_CON_PRECIOS";
        bool bImpresion_Precios = false;
        TipoDeDocumento tpTipoDeDocumento = TipoDeDocumento.Ninguno;
        int iTipoDeProceso = 0;
        int iTipoDeDocumento = 0; 


        public FrmDocumentos_SIAM(): this(TipoDeDocumento.Ventas) 
        {
        }

        public FrmDocumentos_SIAM(TipoDeDocumento Tipo)
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            tpTipoDeDocumento = Tipo;
            iTipoDeProceso = 1;
            iTipoDeDocumento = 1;


            if ( Tipo == TipoDeDocumento.Ventas )
            {
                this.Text = "Generar documentos de Ventas"; 
            }

            if ( Tipo == TipoDeDocumento.Pedido_Ventas )
            {
                iTipoDeProceso = 2;
                this.Text = "Generar documentos de Pedido-Ventas";
                this.Name = "FrmDocumentos_Pedidos_SIAM";

                FrameFolios.Text = "Folios de pedido";
                FrameFechas.Text = "Rango de fechas de entrega"; 
            }


            if(Tipo == TipoDeDocumento.Transferencias)
            {
                iTipoDeDocumento = 2; 
                this.Text = "Generar documentos de Transferencias";
                this.Name = "FrmDocumentos_Transferencias_SIAM";

                FrameFolios.Text = "Folios de transferencia";
                FrameFechas.Text = "Rango de fechas de entrega";
            }

            if(Tipo == TipoDeDocumento.Pedido_Transferencias)
            {
                iTipoDeDocumento = 2;
                iTipoDeProceso = 2;
                this.Text = "Generar documentos de Pedido-Transferencias";
                this.Name = "FrmDocumentos_TransferenciasPedidos_SIAM";

                FrameFolios.Text = "Folios de pedido";
                FrameFechas.Text = "Rango de fechas de entrega";
            }



            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            //conexionWeb = new wsFarmacia.wsCnnCliente();
            //conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");
            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);


            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario= General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            leerSubFarmacias = new clsLeer(ref cnn);
            leerProgramas_Catalogo = new clsLeer(ref cnn);
            leerSubProgramas_Catalogo = new clsLeer(ref cnn);
            leerProgramas = new clsLeer(ref cnn); 
            leerSubProgramas = new clsLeer(ref cnn);
            leerCuadrosDeAtencion = new clsLeer(ref cnn);
            leer_ActualizarPrecios = new clsLeer(ref cnn);


            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            SolicitarPermisosUsuario(); 

            //this.Width = 710;
            //this.Height = 570;

            FrameProceso.Left = 178; 
            FrameProceso.Top = 145;
            MostrarEnProceso(false);

            ////this.Width = 0; 
            ////this.Height = 0; 

            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += string.Format(@"\DOCUMENTOS_SESEQ\");
            lblDirectorioTrabajo.Text = sRutaDestino; 

            grid = new clsGrid(ref grdUnidades, this); 
            CargarListaReportes();
            CargarPerfilesDeAtencion();
        }

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            ////// Valida si el usuario conectado tiene permiso sobre las opcione especiales 
            ////Permisos = new clsOperacionesSupervizadas(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            ////Permisos.Personal = DtGeneral.IdPersonal;
            ////bPermisoClaveSSA = Permisos.TienePermiso(sPermisoClaveSSA);
            ////bPermisoContenidoPaquete = Permisos.TienePermiso(sPermisoContenidoPaquete);

            bImpresion_Precios = DtGeneral.PermisosEspeciales.TienePermiso(sImpresion_Precios);

        }
        #endregion Permisos de Usuario

        private void CargarListaReportes()
        {
            ////////cboReporte.Clear();
            ////////cboReporte.Add(); // Agrega Item Default 

            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumos", "Concentrado de Dispensación");
            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumosDesglozado", "Concentrado de Dispensación desglozado");
            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumosPrograma", "Concentrado de Dispensación Por Programa");
            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumosProgramaTotalizado", "Concentrado de Dispensación Por Programa Totalizado");  

            ////////cboReporte.Add("PtoVta_Admon_Validacion", "Detallado de Dispensación (Validación)", "");
            ////////cboReporte.Add("PtoVta_Admon_Validacion_GruposPrecios", "Detallado de Dispensación (Validación no licitado)", "1");
            ////////cboReporte.Add("PtoVta_Admon_Validacion_GruposPrecios", "Detallado de Dispensación (Validación licitado)", "2");


            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumosSinPrecio", "Claves SSA sin precio asignado");
            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumosSinPrecioDetallado", "Claves SSA sin precio asignado detallado"); 
            ////////cboReporte.SelectedIndex = 0; 
        }

        private void MostrarEnProceso(bool Mostrar)
        {
            if (Mostrar)
            {
                FrameProceso.Left = 178;
            }
            else
            {
                FrameProceso.Left = this.Width + 100; 
            } 
        }

        private void FrmReporteadorCEDIS_Load(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        #region Cargar Combos  
        private void thCargar_Folios_De_Venta()
        {
            _workerThread = new Thread(this.Cargar_Folios_De_Venta);
            _workerThread.Name = "Obteniendo_Folios_De_Venta";
            _workerThread.Start();
        }

        private void Cargar_Folios()
        {

        }

        private void Cargar_Folios_De_Venta()
        {
            bool bRegresa = false;
            string sSql = "";
            int iLargo_Folios = 8; 
            string sFiltro_CampoFolio = "";
            string sFiltro_CampoFolio_TS = ""; 
            string sFiltroDispensacion = "";
            string sFiltroFolios = ""; 
            string sFiltroFecha = string.Format(" and convert(varchar(10), FechaRegistro, 120) between '{0}' and '{1}' ",
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value));

            sFiltro_CampoFolio = " FolioVenta ";
            

            if(tpTipoDeDocumento == TipoDeDocumento.Transferencias)
            {
                sFiltro_CampoFolio_TS = "TS";
                sFiltro_CampoFolio = " FolioTransferencia ";
            }

            if (tpTipoDeDocumento == TipoDeDocumento.Pedido_Ventas || tpTipoDeDocumento == TipoDeDocumento.Pedido_Transferencias)
            {
                iLargo_Folios = 6; 

                sFiltro_CampoFolio = " FolioPedido ";

                sFiltroFecha = string.Format(" and convert(varchar(10), FechaEntrega, 120) between '{0}' and '{1}' ",
                                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value));
            }

            if(tpTipoDeDocumento == TipoDeDocumento.Ventas || tpTipoDeDocumento == TipoDeDocumento.Pedido_Ventas)
            {
                if(txtCte.Text != "")
                {
                    sFiltroDispensacion = string.Format(" And V.IdCliente = '{0}' ", txtCte.Text);
                    if(txtSubCte.Text != "")
                    {
                        sFiltroDispensacion += string.Format(" And V.IdSubCliente = '{0}' ", txtSubCte.Text);
                        if(txtPro.Text != "")
                        {
                            sFiltroDispensacion += string.Format(" And V.IdPrograma = '{0}' ", txtPro.Text);
                            if(txtSubPro.Text != "")
                            {
                                sFiltroDispensacion += string.Format(" And V.IdSubPrograma = '{0}' ", txtSubPro.Text);
                            }
                        }
                    }
                }
            }

            if (!chkFechas.Checked)
            {
                sFiltroFecha = " "; 
            }

            if ( chkFolios.Checked ) 
            {
                if (txtFolioInicial.Text.Trim() != "" && txtFolioFinal.Text.Trim() != "")
                {
                    sFiltroFolios = string.Format(" and {2} Between '{3}{0}' and '{3}{1}' ",
                        Fg.PonCeros(txtFolioInicial.Text.Trim(), iLargo_Folios),
                        Fg.PonCeros(txtFolioFinal.Text.Trim(), iLargo_Folios),
                        sFiltro_CampoFolio, sFiltro_CampoFolio_TS
                        ); 
                }
                else
                {
                    if (txtFolioInicial.Text.Trim() != "" )
                    {
                        sFiltroFolios = string.Format(" and {1} >= '{2}{0}' ",
                            Fg.PonCeros(txtFolioInicial.Text.Trim(), iLargo_Folios), sFiltro_CampoFolio, sFiltro_CampoFolio_TS);
                    }

                    if (txtFolioFinal.Text.Trim() != "")
                    {
                        sFiltroFolios = string.Format(" and {1} <= '{2}{0}' ",
                            Fg.PonCeros(txtFolioFinal.Text.Trim(), iLargo_Folios), sFiltro_CampoFolio, sFiltro_CampoFolio_TS);
                    } 
                }
            }


            //////////////////////////////   
            if (tpTipoDeDocumento == TipoDeDocumento.Ventas)
            {
                sSql = string.Format("Select convert(varchar(10), FechaRegistro, 120) as FechaRegistro, FolioVenta as Folio, \n" +
                    " V.IdCliente, V.IdSubCliente, " +
                    " V.IdPrograma, V.IdSubPrograma, (Programa + ' - ' + SubPrograma ) as ProgramaDispensacion, 0 as Procesar, 0 as Procesado \n" +
                    "From VentasEnc V (NoLock) \n" +
                    "Inner Join vw_Programas_SubProgramas C (NoLock) On ( V.IdPrograma = C.IdPrograma and V.IdSubPrograma = C.IdSubPrograma ) \n" +
                    "Where V.IdEmpresa = '{0}' and V.IdEstado = '{1}' and V.IdFarmacia = '{2}' {3} {4} {5}  \n" +
                    "Order By FechaRegistro \n",
                    sEmpresa, sEstado, sFarmacia, sFiltroFecha, sFiltroDispensacion, sFiltroFolios);
            }

            if (tpTipoDeDocumento == TipoDeDocumento.Pedido_Ventas)
            {
                sSql = string.Format("Select convert(varchar(10), FechaRegistro, 120) as FechaRegistro, FolioPedido as Folio, \n" +
                    " V.IdCliente, V.IdSubCliente, " +
                    " V.IdPrograma, V.IdSubPrograma, (Programa + ' - ' + SubPrograma ) as ProgramaDispensacion, 0 as Procesar, 0 as Procesado \n" +
                    "From Pedidos_Cedis_Enc V (NoLock) \n" +
                    "Inner Join vw_Programas_SubProgramas C (NoLock) On ( V.IdPrograma = C.IdPrograma and V.IdSubPrograma = C.IdSubPrograma ) \n" +
                    "Where V.EsTransferencia = 0 and V.IdEmpresa = '{0}' and V.IdEstado = '{1}' and V.IdFarmacia = '{2}' {3} {4} {5}  \n" +
                    "Order By FechaRegistro \n",
                    sEmpresa, sEstado, sFarmacia, sFiltroFecha, sFiltroDispensacion, sFiltroFolios);
            }

            if (tpTipoDeDocumento == TipoDeDocumento.Transferencias)
            {
                sSql = string.Format("Select convert(varchar(10), FechaRegistro, 120) as FechaRegistro, right(FolioTransferencia, 8) as Folio, \n" +
                    " '' as IdCliente, '' as IdSubCliente, " +
                    " '' as IdPrograma, '' as IdSubPrograma, 'TRANSFERENCIA' as ProgramaDispensacion, 0 as Procesar, 0 as Procesado \n" +
                    "From TransferenciasEnc V (NoLock) \n" +
                    "Where TipoTransferencia = 'TS' and V.IdEmpresa = '{0}' and V.IdEstado = '{1}' and V.IdFarmacia = '{2}' {3} {4} {5}  \n" +
                    "Order By FechaRegistro \n",
                    sEmpresa, sEstado, sFarmacia, sFiltroFecha, sFiltroDispensacion, sFiltroFolios);
            }

            if(tpTipoDeDocumento == TipoDeDocumento.Pedido_Transferencias)
            {
                sSql = string.Format("Select convert(varchar(10), FechaRegistro, 120) as FechaRegistro, FolioPedido as Folio, \n" +
                    " '' as IdCliente, '' as IdSubCliente, " +
                    " '' as IdPrograma, '' as IdSubPrograma, 'TRANSFERENCIAS POR PEDIDO' as ProgramaDispensacion, 0 as Procesar, 0 as Procesado \n" +
                    "From Pedidos_Cedis_Enc V (NoLock) \n" +
                    "Where V.EsTransferencia = 1 and V.IdEmpresa = '{0}' and V.IdEstado = '{1}' and V.IdFarmacia = '{2}' {3} {4} {5}  \n" +
                    "Order By FechaRegistro \n",
                    sEmpresa, sEstado, sFarmacia, sFiltroFecha, sFiltroDispensacion, sFiltroFolios);
            }
            //////////////////////////////   


            grid.Limpiar(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Cargar_Folios_De_Venta()");
                General.msjError("Ocurrió un error al obtener la lista de folios.");
            }
            else
            {
                bRegresa = leer.Leer(); 
                grid.LlenarGrid(leer.DataSetClase); 
            }

            IniciarToolBar(true, !bRegresa, bRegresa); 
        }

        private void CargarPerfilesDeAtencion()
        {
        } 
        #endregion Cargar Combos  

        #region Llenar Combo Reporte
        private void LlenarCombo()
        {
        }
        #endregion Llenar Combo Reporte

        #region Buscar Informacion Dispensacion  
        #region Cliente -- Sub-Cliente 
        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, "txtCliente_Validating");
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
                else
                {
                    General.msjUser("Clave de Cliente no encontrada, ó el Cliente no pertenece al Almacén.");
                    txtCte.Text = ""; 
                    lblCte.Text = "";
                    txtCte.Focus();
                }
            }
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, "txtCte_KeyDown");
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
            } 
        }

        private void CargarDatosCliente()
        {
            txtCte.Enabled = false; 
            txtCte.Text = leer.Campo("IdCliente");
            lblCte.Text = leer.Campo("NombreCliente");
            //lblCte.Text = leer.Campo("Nombre");
        }

        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCte.Text != "")
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, "txtCte_Validating");
                if (leer.Leer())
                {
                    CargarDatosSubCliente();
                }
                else
                {
                    General.msjUser("Clave de Sub-Cliente no encontrada, ó el Sub-Cliente no pertenece a la Farmacia.");
                    txtSubCte.Text = ""; 
                    lblSubCte.Text = "";
                    txtSubCte.Focus();
                }
            }
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "")
                {
                    leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, "txtSubCte_KeyDown");
                    if (leer.Leer())
                    {
                        CargarDatosSubCliente();
                    }
                }
            }
        }

        private void CargarDatosSubCliente()
        {
            txtSubCte.Enabled = false; 
            txtSubCte.Text = leer.Campo("IdSubCliente");
            lblSubCte.Text = leer.Campo("NombreSubCliente");
        }
        #endregion Cliente -- Sub-Cliente 

        #region Programa -- Sub-Programa
        private void txtPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtPro.Text.Trim() != "")
            {
                if ( txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" ) 
                {
                    //leer.DataSetClase = Consultas.Programas(txtPro.Text, "txtPro_Validating");
                    leer.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), "txtPro_Validating");
                    if (leer.Leer())
                    {
                        CargarDatosProgramas();
                    }
                    else
                    {
                        txtPro.Text = ""; 
                        lblPro.Text = "";
                        txtPro.Focus();
                    }
                }
            }
        }

        private void txtPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "")
                {
                    leer.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text.Trim(), txtSubCte.Text.Trim(), "txtPro_KeyDown");
                    if (leer.Leer())
                    {
                        CargarDatosProgramas();
                    }
                }
            }
        }

        private void CargarDatosProgramas()
        {
            txtPro.Enabled = false; 
            txtPro.Text = leer.Campo("IdPrograma");
            lblPro.Text = leer.Campo("Programa");
        }

        private void txtSubPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubPro.Text.Trim() != "")
            {
                if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "")
                {
                    //leer.DataSetClase = Consultas.SubProgramas(txtSubPro.Text, txtPro.Text, "txtSubPro_Validating");
                    leer.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), txtSubPro.Text.Trim(), "txtSubPro_Validating");
                    if (leer.Leer())
                    {
                        CargarDatosSubProgramas();
                    }
                    else
                    {
                        txtSubPro.Text = ""; 
                        lblSubPro.Text = "";
                        txtSubPro.Focus();
                    }
                }
            }
        } 

        private void txtSubPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "")
                {
                    leer.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), "txtPro_KeyDown");
                    if (leer.Leer())
                    {
                        CargarDatosSubProgramas();
                    }
                }
            }
        }

        private void CargarDatosSubProgramas()
        {
            txtSubPro.Enabled = false;
            txtSubPro.Text = leer.Campo("IdSubPrograma");
            lblSubPro.Text = leer.Campo("SubPrograma");
        }
        #endregion Programa -- Sub-Programa
        #endregion Buscar Informacion Dispensacion

        #region Impresion
        private void ObtenerRutaReportes()
        {
            //string sSql = string.Format(" Select ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema " +
            //    " From Net_CFGC_Parametros (NoLock) " +
            //    " Where IdEstado = '{0}' and IdFarmacia = '{1}' and NombreParametro = '{2}' ", 
            //    cboEstados.Data, cboFarmacias.Data, "RutaReportes");
            //if (!leerWeb.Exec(sSql))
            //{
            //    Error.GrabarError(leer, "ObtenerRutaReportes");
            //    General.msjError("Ocurrió un error al obtener la Ruta de Reportes de la Farmacia."); 
            //}
            //else
            //{
            //    leerWeb.Leer();
            //    sUrl_RutaReportes = leerWeb.Campo("Valor");     
            //}
        }

        private bool validarImpresion()
        {
            bool bRegresa = true;

            if (!bSeEncontroInformacion)
            {
                bRegresa = false;
                General.msjUser("No existe información para generar el reporte, verifique."); 
            }

            ////////if (bRegresa && cboReporte.SelectedIndex == 0)
            ////////{
            ////////    bRegresa = false;
            ////////    General.msjUser("No ha seleccionado un reporte de la lista, verifique.");
            ////////    cboReporte.Focus(); 
            ////////}

            return bRegresa; 
        }

        private void ImprimirInformacion()
        {
            // int iTipoInsumo = 0;
            // int iTipoDispensacion = 0;
            bool bRegresa = false;  

            if (validarImpresion())
            {
                // El reporte se localiza fisicamente en el Servidor Regional ó Central. 
                // Se utilizan los datos de Conexión de la farmacia seleccionada. 

                DatosCliente.Funcion = "Imprimir()"; 
                clsImprimir myRpt = new clsImprimir(DatosDeConexion);
                // byte[] btReporte = null;

                ////General.Url = "http://lapjesus/wsCompras/wsOficinaCentral.asmx";
                ////DtGeneral.RutaReportes = @"D:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES"; 

                myRpt.RutaReporte = DtGeneral.RutaReportes;
                myRpt.NombreReporte = ""; // cboReporte.Data + "";

                string sValor = "";
                ////try
                ////{
                ////    sValor = (string)cboReporte.ItemActual.Item;
                ////}
                ////catch { }

                if (sValor == "1" || sValor == "2")
                {
                    myRpt.Add("IdGrupoPrecios", Convert.ToInt32(sValor)); 
                }

                bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente); 


                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Impresion

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void InicializarPantalla()
        {
            bool bValor = true;
            IniciarToolBar(true, true, false);

            sRutaDestino = "";
            bFolderDestino = false; 

            btnExportarExcel.Enabled = false;
            // iBusquedasEnEjecucion = 0;

            grid.Limpiar(); 
            Fg.IniciaControles(this, true);
            chkFechas.Checked = true;
            //chkMostrarPrecios.Enabled = bImpresion_Precios;


            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;
            sSubFarmacias = ""; 

            FrameCliente_old.Enabled = bValor;
            FrameFechas.Enabled = bValor;

            ////txtPro.Enabled = false;
            ////txtSubPro.Enabled = false; 

            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += string.Format(@"\DOCUMENTOS_SESEQ\");
            lblDirectorioTrabajo.Text = sRutaDestino;
            bFolderDestino = true;


            if (!DtGeneral.EsAdministrador)
            {
                //////cboEmpresas.Data = DtGeneral.EmpresaConectada;
                //////cboEstados.Data = DtGeneral.EstadoConectado;

                //////cboEmpresas.Enabled = false;
                //////cboEstados.Enabled = false; 
            } 

            txtCte.Focus(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            thCargar_Folios_De_Venta();
        }

        private void btnGenerarDocumentos_Click(object sender, EventArgs e)
        {
            if (validarProcesamiento())
            {
                CrearDirectorioDestino(); 
                IniciarProcesamiento(); 
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Imprimir(); 
        }

        private void btnDirectorio_Click(object sender, EventArgs e)
        {
            folder = new FolderBrowserDialog();
            folder.Description = "Directorio destino para los documentos generados.";
            folder.RootFolder = Environment.SpecialFolder.Desktop;
            folder.ShowNewFolderButton = true;  

            if (folder.ShowDialog() == DialogResult.OK) 
            {
                sRutaDestino = folder.SelectedPath +  @"\";
                lblDirectorioTrabajo.Text = sRutaDestino;
                bFolderDestino = true; 
            } 

        } 
        #endregion Botones

        #region Eventos
        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            lblCte.Text = ""; 
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCte.Text = "";
            //grid.Limpiar();
        }

        private void txtPro_TextChanged(object sender, EventArgs e)
        {
            lblPro.Text = "";
            grid.Limpiar();
        }

        private void txtSubPro_TextChanged(object sender, EventArgs e)
        {
            lblSubPro.Text = "";
            grid.Limpiar();
        } 

        #endregion Eventos

        #region Procesar Informacion  
        private void IniciarToolBar(bool Nuevo, bool Ejecutar, bool Generar)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnGenerarDocumentos.Enabled = Generar; 
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;


            return bRegresa; 
        }

        private bool validarProcesamiento()
        {
            bool bRegresa = true;
           
            if (!bFolderDestino)
            {
                bRegresa = false;
                General.msjUser("No ha especificado el directorio donde se generaran los documentos, verifique."); 
            }

            if (bRegresa && grid.Rows == 0)
            {
                bRegresa = false;
                General.msjUser("No existe información disponibe para procesar, verifique.");
            }

            return bRegresa; 
        }

        private void CrearDirectorioDestino()
        {
            string sMarcaTiempo = General.FechaSinDelimitadores(General.FechaSistema); 
            string sDir = string.Format("FI_{0}___FF_{1}",
                General.FechaYMD(dtpFechaInicial.Value, ""), General.FechaYMD(dtpFechaFinal.Value, ""));

            if (!chkFechas.Checked)
            {
                sDir = string.Format("FI_{0}___FF_{1}",
                    General.FechaYMD(General.FechaSistema, ""), General.FechaYMD(General.FechaSistema, ""));
            }


            sRutaDestino_Archivos = Path.Combine(sRutaDestino, sDir) + "____" + sMarcaTiempo; 
            if (!Directory.Exists(sRutaDestino_Archivos)) 
            {
                Directory.CreateDirectory(sRutaDestino_Archivos);
            }
        }

        private void BloquearControles(bool Habilitar)
        {
            txtCte.Enabled = Habilitar;
            txtSubCte.Enabled = Habilitar;
            txtPro.Enabled = Habilitar;
            txtSubPro.Enabled = Habilitar;

            dtpFechaInicial.Enabled = Habilitar;
            dtpFechaFinal.Enabled = Habilitar;
            chkFechas.Enabled = Habilitar;
            //chkMostrarPrecios.Enabled = Habilitar; 

            txtFolioInicial.Enabled = Habilitar;
            txtFolioFinal.Enabled = Habilitar;
            chkFolios.Enabled = Habilitar;

            btnDirectorio.Enabled = Habilitar;

            chkMarcarDesmarcar.Enabled = Habilitar;
        }

        private void IniciarProcesamiento()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false;
            btnExportarExcel.Enabled = false;

            BloquearControles(false); 

            // bloqueo principal 
            IniciarToolBar(false, false, false); 
            grid.BloqueaColumna(true, (int)Cols.Procesar);
            grid.SetValue((int)Cols.Procesado, 0); 

            MostrarEnProceso(true);

            ////txtCte.Enabled = false;
            ////txtSubCte.Enabled = false; 
            ////btnDirectorio.Enabled = false; 
            ////FrameFechas.Enabled = false;
            ////FrameFolios.Enabled = false; 

            // bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoValidacion"; 
            _workerThread.Start();
            // LlenarGrid(); 
        }

        private void ObtenerInformacion()
        {
            string sTitulo = ""; 
            string sFolio = "";
            Color cColor = Color.GreenYellow;



            bEjecutando = true; 

            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, (int)Cols.Procesar))
                {

                    sFolio = grid.GetValue(i, (int)Cols.Folio);

                    cColor = Generar_Documento(sFolio) ? Color.GreenYellow : cColor = Color.Red;

                    grid.SetValue(i, (int)Cols.Procesado, true);
                    grid.ColorRenglon(i, cColor); 
                }
            }

            bEjecutando = false;

            // bloqueo principal 
            Cursor.Current = Cursors.Default;
            IniciarToolBar(true, true, true);
            grid.BloqueaColumna(false, (int)Cols.Procesar);
            MostrarEnProceso(false);
        }

        private bool Generar_DocumentoTXT(string GUID, DataTable Informacion)
        {
            bool bRegresa = false;
            clsLeer datos = new clsLeer();
            StreamWriter sw; 

            datos.DataTableClase = Informacion;

            try
            {
                if (datos.Registros > 0)
                {
                    sw = new StreamWriter(Path.Combine(sRutaDestino, GUID), false);
                    while (datos.Leer())
                    {
                        sw.WriteLine(datos.Campo("Registro"));
                    }
                    sw.Close();
                    sw = null;

                    bRegresa = true;
                }
            }
            catch
            {
                bRegresa = false;
            }

            return bRegresa; 
        }

        private bool Generar_Documento(string Folio)
        {
            bool bRegresa = false;
            clsLeer dtsResultado = new clsLeer();

            string sGUID = "";
            string sSql = string.Format("Exec spp_INT_SESEQ__CEDIS_0001_GenerarDocumento_SIAM  \n " 
                    + " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @TipoDeDocumento = '{4}', @TipoDeProceso = '{5}' ", 
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Folio, iTipoDeDocumento, iTipoDeProceso ); 


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Generar_Documento");
            }
            else 
            {
                while (leer.Leer())
                {
                    dtsResultado = new clsLeer();

                    sGUID = leer.Campo("GUID");
                    dtsResultado.DataRowsClase = leer.Tabla(2).Select(string.Format(" GUID = '{0}' ", sGUID));
                    
                    bRegresa = Generar_DocumentoTXT(sGUID, dtsResultado.DataTableClase); 
                }
            }

            return bRegresa; 

            //string sFile = Folio;
            //string sFecha = string.Format("__{0}_{1}_{2}",
            //    Fg.PonCeros(dtpFechaInicial.Value.Year, 4),
            //    Fg.PonCeros(dtpFechaInicial.Value.Month, 2),
            //    General.FechaNombreMes(dtpFechaInicial.Value).ToUpper());

            //string TipoRpt = EsConsignacion == 0 ? "Venta" : "Consignación"; 


            //sFile = string.Format("{0}_{1}___P{2}_SP{3}___{4}_{5}_{6}", 
            //    sFarmacia, Folio, IdPrograma, IdSubPrograma, Fg.PonCeros(IdPerfilAtencion, 2), Titulo, TipoRpt); 
            //sFile = Path.Combine(sRutaDestino_Archivos, sFile + ".pdf");


            //bool bRegresa = false;
            //clsImprimir Reporte = new clsImprimir(General.DatosConexion);
            //clsReporteador Reporteador;  // = new clsReporteador(Reporte, DatosTerminal);

            //Reporte.RutaReporte = DtGeneral.RutaReportes;
            //Reporte.NombreReporte = "PtoVta_TicketCredito_Detallado_Precios___AtencionClavesSSA"; 
            //Reporte.Add("@IdEmpresa", sEmpresa);
            //Reporte.Add("@IdEstado", sEstado);
            //Reporte.Add("@IdFarmacia", sFarmacia);
            //Reporte.Add("@Folio", Folio);
            //Reporte.Add("@IdPerfilDeAtencion", IdPerfilAtencion);
            //Reporte.Add("@EsConsignacion", EsConsignacion);
            //Reporte.Add("@MostrarPrecios", MostrarPrecios);


            //Reporteador = new clsReporteador(Reporte, DatosCliente);
            //Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
            //Reporteador.Url = General.Url;
            //Reporteador.MostrarInterface = false;
            //Reporteador.MostrarMensaje_ReporteSinDatos = false;

            //bRegresa = Reporteador.ExportarReporte(sFile, FormatosExportacion.PortableDocFormat, true);


        }
        #endregion Procesar Informacion 

        #region Funciones y Procedimientos Privados 
        private void ActivarControles()
        { 
            this.Cursor = Cursors.Default;
            
            btnNuevo.Enabled = true; 
            btnEjecutar.Enabled = true;
            btnExportarExcel.Enabled = false;

            BloquearControles(true); 

            ////txtCte.Enabled = true;
            ////txtSubCte.Enabled = true;
            ////txtPro.Enabled = true;
            ////txtSubPro.Enabled = true;

            ////chkFolios.Enabled = true;
            ////chkMarcarDesmarcar.Enabled = true; 


            ////btnDirectorio.Enabled = true; 
            ////FrameFechas.Enabled = true;
            ////FrameFolios.Enabled = true; 

        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando) 
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false; 

                // btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
                btnExportarExcel.Enabled = true;
                ActivarControles(); 
            }
        }


        private void FrmReporteadorCEDIS_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void ObtenerInformacionExcel()
        {
            string sSql = string.Format("Select *, Convert(varchar(16), GetDate(), 120) as FechaImpresion From tmpRptAdmonDispensacion (NoLock) ");

            if (validarDatosDeConexion())
            {
                cnnUnidad = new clsConexionSQL(DatosDeConexion);
                cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite300;

                leerExportarExcel = new clsLeer(ref cnnUnidad);
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leerLocal.DatosConexion, leerExportarExcel.Error, "ObtenerInformacionExcel()");
                    General.msjError("Ocurrió un error al obtener la información del reporte.");
                }
                else
                {
                    leerExportarExcel.DataSetClase = leer.DataSetClase;
                }

                // bSeEjecuto = true;
                bEjecutando = false; // Cursor.Current  
            }
            this.Cursor = Cursors.Default;
        } 

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            int iRow = 2;
            string sNombreFile = "PtoVta_Admon_Validacion" + ".xls";
            string sPeriodo = "";

            this.Cursor = Cursors.WaitCursor;
            bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Admon_Validacion.xls", DatosCliente);

            xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            xpExcel.AgregarMarcaDeTiempo = false;

            if (!bRegresa)
            {
                this.Cursor = Cursors.Default;
                General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            }
            else
            {
                if (xpExcel.PrepararPlantilla(sNombreFile))
                {
                    xpExcel.GeneraExcel();

                    //Se pone el encabezado
                    leerExportarExcel.RegistroActual = 1;
                    leerExportarExcel.Leer();
                    xpExcel.Agregar(leerExportarExcel.Campo("Empresa"), iRow, 2);
                    iRow++;
                    xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRow, 2);
                    iRow++;

                    sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
                        General.FechaYMD(leerExportarExcel.CampoFecha("FechaInicial"), "-"), General.FechaYMD(leerExportarExcel.CampoFecha("FechaFinal"), "-"));
                    xpExcel.Agregar(sPeriodo, iRow, 2);

                    iRow = 6;
                    xpExcel.Agregar(leerExportarExcel.Campo("FechaImpresion"), iRow, 3);

                    // Se ponen los detalles
                    leerExportarExcel.RegistroActual = 1;
                    iRow = 9;
                    while (leerExportarExcel.Leer())
                    {
                        xpExcel.Agregar(leerExportarExcel.Campo("IdCliente"), iRow, 2);
                        xpExcel.Agregar(leerExportarExcel.Campo("NombreCliente"), iRow, 3);
                        xpExcel.Agregar(leerExportarExcel.Campo("IdPrograma"), iRow, 4);
                        xpExcel.Agregar(leerExportarExcel.Campo("Programa"), iRow, 5);
                        xpExcel.Agregar(leerExportarExcel.Campo("IdSubPrograma"), iRow, 6);
                        xpExcel.Agregar(leerExportarExcel.Campo("SubPrograma"), iRow, 7);
                        xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRow, 8);
                        xpExcel.Agregar(leerExportarExcel.Campo("NumReceta"), iRow, 9);
                        xpExcel.Agregar(leerExportarExcel.Campo("FechaReceta"), iRow, 10);
                        xpExcel.Agregar(leerExportarExcel.Campo("FolioReferencia"), iRow, 11);
                        xpExcel.Agregar(leerExportarExcel.Campo("Beneficiario"), iRow, 12);
                        xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRow, 13);
                        xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCorta"), iRow, 14);
                        xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRow, 15);
                        xpExcel.Agregar(leerExportarExcel.Campo("PrecioLicitacion"), iRow, 16);
                        xpExcel.Agregar(leerExportarExcel.Campo("ImporteEAN"), iRow, 17);

                        iRow++;
                    }

                    // Finalizar el Proceso 
                    xpExcel.CerrarDocumento();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }
                }
                this.Cursor = Cursors.Default;
            }
        } 
        #endregion Funciones y Procedimientos Privados

        private void chkMarcarDesmarcar_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Procesar, chkMarcarDesmarcar.Checked); 
        }
    } 
}
