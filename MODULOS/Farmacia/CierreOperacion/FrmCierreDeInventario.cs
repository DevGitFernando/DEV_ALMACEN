using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;
using System.Diagnostics; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Reporteador; 


using DllTransferenciaSoft;
using DllTransferenciaSoft.ReplicacionSQL;

namespace Farmacia.CierreOperacion 
{
    delegate void Function();	// a simple delegate for marshalling calls from event handlers to the GUI thread

    public partial class FrmCierreDeInventario : FrmBaseExt 
    {

        enum TipoDeProcesoCierreDeInventario
        {
            Ninguno = 0, 
            CierreDeOperacion = 1,
            CambioDeContrato = 2, 
            CambioDeOperacion = 3  
        }

        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnAvance = new clsConexionSQL(General.DatosConexion);

        clsLeer leer;
        // clsLeer leerAvance; 
        clsConsultas query;
        clsAyudas ayuda;
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        string sEmpresaNueva = "";
        string sFarmaciaNueva = "";

        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
        string sPersonal = DtGeneral.IdPersonal;
        string sNombrePersonal = DtGeneral.NombrePersonal;

        TipoDeProcesoCierreDeInventario tpProceso = TipoDeProcesoCierreDeInventario.Ninguno; 

        Thread _workerThread;
        // Thread _wT_Avance;
        bool bSeGeneroCierre = false;
        bool bSeGeneroRespaldo = false;
        bool bProcesando = false;
        bool bGenerando_PDFs = false; 

        string sFolioSalidaVenta = "", sFolioSalidaConsignacion = "", sFolioEntradaVenta = "", sFolioEntradaConsignacion = "";
        // string sWhereCierre = "";
        // string sWhereInicialVenta = "", sWhereInicialConsignacion = "";
        string sMensaje_Folios = "";


        string sRutaDeReportes_PDF = "";

        clsReplicacionSQL replicacion = null; //// = new clsReplicacionSQL();

        public FrmCierreDeInventario()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent(); 
            
            this.Width = 490;
            this.Height = 540;
            this.Height = 450 + 90; 

            // Máximo tiempo de espera 
            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.Puerto = General.DatosConexion.Puerto; 

            
            ConexionLocal.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            ConexionLocal.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite; 


            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            // leerAvance = new clsLeer(ref ConexionLocal); 

            leer = new clsLeer(ref ConexionLocal);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name, true);
            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

        }

        private void FrmCierreDeInventario_Load(object sender, EventArgs e)
        {
            Inicializa();


            lblFarmacia.Text = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            ObtenerFarmaciaNueva();

            tmPantalla.Enabled = true;
            tmPantalla.Start();
        }

        #region Botones 
        private void btnGenerarCierre_Click(object sender, EventArgs e)
        {
            if (validaDatos())
            {
                GenerarCierre(); 
            }
        }

        private void btnGenerarRespaldo_Click(object sender, EventArgs e)
        {
            bSeGeneroRespaldo = Transferencia.ExportarInformacionBD(false); 
        }

        private void GenerarCierre()
        {
            bProcesando = true; 
            bSeGeneroCierre = false;
            btnGenerarCierre.Enabled = false;
            btnGenerarRespaldo.Enabled = false;
            btnExportarReportes.Enabled = false;

            FrameCerrarInventario.Visible = false;
            FrameAvance.Visible = true;

            this.Refresh(); 
            lblAvance.Text = "Iniciando proceso";
            this.Refresh(); 

            tmAvance.Interval = 1000;  
            tmAvance.Enabled = true;
            tmAvance.Start();

            tmProceso.Interval = 2000;
            tmProceso.Enabled = true;
            tmProceso.Start();
            

            System.Threading.Thread.Sleep(500); 
            this.Refresh();

            /////ReiniciarSistema(); 

            _workerThread = new Thread(this.CerrarInventario);
            _workerThread.Name = "GenerandoCierre"; 
            _workerThread.Start(); 
        }
        #endregion Botones 

        #region Get Avance 
        private void GetAvance()
        {
            if (!bGenerando_PDFs)
            {
                GetAvanceInterno();
            }
        }

        private void GetAvanceInterno()
        {
            clsLeer leerAvance = new clsLeer(ref cnnAvance);   

            string sSql = 
                " Select Top 1 IdProceso, Descripcion, Porcentaje " +
                " From tmpAvance_CierreInventario (NoLock) " + 
                " Order by IdProceso Desc ";

            cnnAvance.Abrir(); 

            if (!leerAvance.Exec(sSql))
            {
                SetAvance(string.Format("Error al obtener el avance del proceso"));
            }
            else
            {
                if (leerAvance.Leer())
                {
                    ////lblAvance.Text = string.Format("{0}  ==>    {1} %", leerAvance.Campo("Descripcion"), leerAvance.CampoDouble("Porcentaje").ToString("##0.#0") );
                    SetAvance(string.Format("{0}  ==>    {1} %", leerAvance.Campo("Descripcion"), leerAvance.CampoDouble("Porcentaje").ToString("##0.#0")));
                }
            }

            cnnAvance.Cerrar(); 
        }

        protected void SetAvance( string Avance )
        {
            try
            {
                this.Invoke(new Function(delegate ()
                {
                    lblAvance.Text = Avance;
                }));
            }
            catch { }
        }
        #endregion Get Avance

        #region Funciones
        public bool validaDatos()
        {
            bool bRegresa = true;

            sRutaDeReportes_PDF = lblDirectorioExportar.Text; 


            if (General.msjConfirmar("¿ Desea generar el cierre de inventarios ?") == DialogResult.No)
            {
                bRegresa = false; 
            }

            if (!DtGeneral.EsEquipoDeDesarrollo)
            {
                if (bRegresa & !bSeGeneroRespaldo)
                {
                    bRegresa = false;
                    General.msjUser("No se ha generado el respaldo previo a la ejecución del Proceso de Cierre.");
                }
            }

            if (bRegresa)
            {
                if (lblDirectorioExportar.Text.Trim() == "")
                {
                    bRegresa = false;
                    General.msjUser("No ha especificado la ruta donde de generaran los reportes, verifique."); 
                    btnExportarReportes.Focus(); 
                }
            }

            if (bRegresa)
            {
                bRegresa = generarTablaAvance();
            }            

            if (bRegresa)
            {
                bRegresa = validarCierres(); 
            }

            if (bRegresa)
            {
                bRegresa = ObtenerFarmaciaNueva();
            }

            if (bRegresa)
            {
                bRegresa = validarSurtidos_Almacen();
            }

            return bRegresa; 
        }

        private bool generarTablaAvance()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_Mtto_CierreDeInventario_Avance ");

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "validarCierres()");
                General.msjError("Ocurrió un error al generar el control de avance.");
            }

            return bRegresa;
        }

        private bool validarSurtidos_Almacen()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_ALM_ValidarGeneracionExistenciaDistribucion  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen)
            {
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leer, "validarSurtidos()");
                    General.msjError("Ocurrió un error al validar las Ordenes de Surtido.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        bRegresa = false;
                        General.msjAviso("Se encontrarón Ordenes de Surtido activas, verifique."); 
                    }
                }
            }

            return bRegresa;
        }

        private bool validarCierres()
        {
            bool bRegresa = false;
            string sSql = string.Format("Select * " +
                "From CtlCortesParciales (NoLock) " + 
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and convert(varchar(10), FechaSistema, 120) = '{3}' and Status = 'A' ",
                sEmpresa, sEstado,   sFarmacia, sFechaSistema );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "validarCierres()");
                General.msjError("Ocurrió un error al validar los Cortes parciales."); 
            }
            else
            {
                bRegresa = !leer.Leer();

                if (!bRegresa)
                {
                    General.msjAviso("Se encontrarón Cortes Parciales abiertos, es necesario generar el cierre para continuar con el proceso.");
                }
                else
                {
                    bRegresa = validarCierresDia(); 
                }
            } 

            return bRegresa; 
        }

        private bool validarCierresDia()
        {
            bool bRegresa = false;
            string sSql = string.Format(
                "Declare @sFecha varchar(10) " + 
                "\n" +
                "       Select @sFecha = convert(varchar(10), Max(FechaSistema), 120) From CtlCortesParciales (NoLock) " +
                "              Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " + 
                "\n" + 
                "Select * " +
                "From CtlCortesDiarios (NoLock) " +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
                " and convert(varchar(10), FechaSistema, 120) = convert(varchar(10), @sFecha, 120) " +
                " and Status = 'A' ",
                sEmpresa, sEstado, sFarmacia, sFechaSistema);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "validarCierresDia()");
                General.msjError("Ocurrió un error al validar los Cambios de Dia.");
            }
            else
            {
                bRegresa = leer.Leer();

                if (!bRegresa)
                {
                    General.msjAviso(string.Format("No se encontraro el Cambio de dia del '{0}' , es necesario generar el Cambio de dia para continuar con el proceso.", sFechaSistema));
                }
            }

            return bRegresa; 
        }

        private void Inicializa()
        {
            sFarmaciaNueva = "";
            bProcesando = false;
            bSeGeneroCierre = false;

            lblTipoDeProceso.Text = ""; 

            FrameCerrarInventario.Visible = true; 
            btnGenerarCierre.Enabled = DtGeneral.EsServidorDeRedLocal;
            btnGenerarRespaldo.Enabled = DtGeneral.EsServidorDeRedLocal; 

            if (DtGeneral.EsEquipoDeDesarrollo)
            {
                btnGenerarCierre.Enabled = true;
                btnGenerarRespaldo.Enabled = true; 
            } 

            FrameAvance.Visible = false;
            FrameAvance.Left = FrameCerrarInventario.Left;
            FrameAvance.Top = FrameCerrarInventario.Top; 


            //////////  
            if (DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen)
            {
                FrameUnidad.Text = "Información de Almacén"; 
            }

            if (DtGeneral.ModuloEnEjecucion == TipoModulo.Farmacia)
            {
                FrameUnidad.Text = "Información de Farmacia"; 
            }
        }

        private void CerrarInventario()
        {
            string sSql = ""; // , sMensaje = ""; 
            int iManejaUbicaciones = 0;
            string sFoliosVenta = "";
            string sFoliosConsigna = "";
            sMensaje_Folios = "";


            int iTipoDeEnvio = 1, iTipoDeProceso = 1, iDiasRevision = 10;


            ////if (!ObtenerFarmaciaNueva())
            ////{
            ////    btnGuardar.Enabled = true;
            ////    btnGuardar.Focus();
            ////}
            ////else 
            {
                if (!ConexionLocal.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    ConexionLocal.IniciarTransaccion();

                    if (GnFarmacia.ManejaUbicaciones)
                    {
                        iManejaUbicaciones = 1;
                    }

                    sSql = string.Format("Exec spp_Mtto_CierreDeInventario  \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdEmpresaNueva = '{3}', @IdFarmaciaNueva = '{4}', \n" +
                        "\t@IdPersonal = '{5}', @FechaSistema = '{6}', @ManejaUbicaciones = '{7}', @TipoDeProceso = '{8}' \n",
                        sEmpresa, sEstado, sFarmacia, sEmpresaNueva, sFarmaciaNueva, sPersonal, sFechaSistema, iManejaUbicaciones, (int)tpProceso);

                    if (!leer.Exec(sSql))
                    {
                        tmAvance.Stop();
                        tmAvance.Enabled = false;

                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(leer, "CerrarInventario");

                        General.msjError("Ocurrió un error al generar el Cierre de Inventario.");
                        Inicializa();
                    }
                    else
                    {
                        if (!leer.Leer())
                        {
                            tmAvance.Stop();
                            tmAvance.Enabled = false;

                            ConexionLocal.DeshacerTransaccion();
                            General.msjError("Ocurrió un error al generar el Cierre de Inventario.");
                            Inicializa();
                        }
                        else
                        {
                            bSeGeneroCierre = true;


                            ////tmAvance.Stop(); 
                            ////tmAvance.Enabled = false;
                            ////lblAvance.Text = "Proceso finalizado."; 

                            sFolioSalidaVenta = leer.Campo("FolioSalidaVenta");
                            sFolioSalidaConsignacion = leer.Campo("FolioSalidaConsignacion");

                            sFolioEntradaVenta = leer.Campo("FolioEntradaVenta");
                            sFolioEntradaConsignacion = leer.Campo("FolioEntradaConsignacion");

                            // Armar los Folios 
                            sFoliosVenta = sFolioSalidaVenta;
                            sFoliosVenta += sFolioEntradaVenta != "" ? ", " + sFolioEntradaVenta : "";

                            sFoliosConsigna = sFolioSalidaConsignacion;
                            sFoliosConsigna += sFolioEntradaConsignacion != "" ? ", " + sFolioEntradaConsignacion : "";


                            sMensaje_Folios = string.Format("La información se guardó exitosamente con los siguientes Folios : \n");
                            // sMensaje += "\n";
                            // sMensaje += string.Format("     Folios de Cierre : {0} "); 
                            sMensaje_Folios += "\n";
                            sMensaje_Folios += string.Format("     Folios de Venta : {0} ", sFoliosVenta);
                            sMensaje_Folios += "\n";
                            sMensaje_Folios += string.Format("     Folios de Consignación : {0} ", sFoliosConsigna);

                            // sWhereCierre, sWhereInicialVenta, sWhereInicialConsignacion);

                            //ConexionLocal.DeshacerTransaccion(); 
                            ConexionLocal.CompletarTransaccion();
                            // General.msjUser(sMensaje); //Este mensaje lo genera el SP 


                            ////Imprimir(sFolioEntradaVenta); 
                            ////Imprimir(sFolioEntradaConsignacion); 

                            ////ReiniciarSistema(); 
                        }
                    }

                        
                    if ( bSeGeneroCierre )
                    {
                        ////if (!DtGeneral.EsEquipoDeDesarrollo)
                        ////{
                        ////    replicacion = new clsReplicacionSQL(iTipoDeEnvio, iTipoDeProceso, iDiasRevision);

                        ////    replicacion.RutaArchivo = lblDirectorioExportar.Text;

                        ////    if (replicacion.GenerarArchivos())
                        ////    {
                        ////        //replicacion.EnviarArchivos();
                        ////    }
                        ////}

                    }


                    if( bSeGeneroCierre )
                    {
                        ///////lblAvance.Text = "Generando reportes ...";  
                        SetAvance("Generando reportes ...");
                        Generar_PDFS();
                    }

                    if( bSeGeneroCierre )
                    {
                        //lblAvance.Text = "Finalizando proceso de cierre de operación ...";
                        SetAvance("Finalizando proceso de cierre de operación ...");
                        sSql = string.Format("Exec spp_Mtto_CierreDeInventario_006 \t" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TipoDeProceso = '{3}' \n\n", 
                            sEmpresaNueva, sEstado, sFarmaciaNueva, (int)tpProceso);


                        /// Este proceso no se realiza en la db centralizada 
                        if (DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Ninguno)
                        {
                            sSql += string.Format("Exec spp_Mtto_CierreDeInventario_004 \n" +
                                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', \n" +
                                "\t@IdEmpresaNueva = '{3}', @IdFarmaciaNueva = '{4}', @dPorce = '{5}', @GenerarRespaldo = 1, @RutaRespaldo = '{6}', @TipoDeProceso = '{7}' \n",
                                sEmpresa, sEstado, sFarmacia, sEmpresaNueva, sFarmaciaNueva, 75, Transferencia.RutaExportarInformacionBD, (int)tpProceso);
                        }

                        if (!leer.Exec(sSql))
                        {
                            tmAvance.Stop();
                            tmAvance.Enabled = false;
                            Error.GrabarError(leer, "CerrarInventario_Depurando");
                        }
                        else
                        {
                            // bSeGeneroCierre = true;
                            tmAvance.Stop();
                            tmAvance.Enabled = false;
                            //lblAvance.Text = "Proceso finalizado.";
                            SetAvance("Proceso finalizado.");
                        }
                    }

                    ConexionLocal.Cerrar();
                }
            }
            bProcesando = false;
        }

        private void ReiniciarSistema()
        {
            // Cerrar la aplicación y abrirla de nuevo. 
            General.msjUser("Se reiniciara el Sistema para completar el Cierre de Inventario.");

            Process proceso = new Process();
            proceso.StartInfo.FileName = Application.ExecutablePath;
            proceso.Start();

            this.Close(); 
            Application.Exit(); 
        }

        private bool ObtenerFarmaciaNueva()
        {
            return ObtenerFarmaciaNueva(true); 
        }

        private bool ObtenerFarmaciaNueva(bool MensajeError)
        {
            bool bRegresa = false;
            string sSql = string.Format(
                "Select  M.IdEmpresaNueva, F.IdEstado, F.Estado, F.IdFarmacia, F.IdFarmacia as IdFarmaciaNueva, F.Farmacia, M.TipoDeProceso, M.Observaciones " + 
                " From CatFarmacias_Migracion M (NoLock) " + 
                " Inner Join vw_Farmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmaciaNueva = F.IdFarmacia ) " + 
                " Where M.IdEmpresa = '{0}' and M.IdEstado = '{1}' And M.IdFarmacia = '{2}' ", 
                sEmpresa, sEstado, sFarmacia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerFarmaciaNueva()"); 
                General.msjError("Ocurrió un error al obtener la información de la nueva unidad.");
            }
            else
            {
                if (!leer.Leer()) 
                {
                    if (MensajeError)
                    {
                        General.msjError("No se encontro información de la nueva unidad, verifique.");
                    }
                }
                else 
                {

                    tpProceso = (TipoDeProcesoCierreDeInventario)leer.CampoInt("TipoDeProceso");


                    switch(tpProceso)
                    {
                        case TipoDeProcesoCierreDeInventario.CambioDeContrato:
                            lblTipoDeProceso.Text = "CAMBIO DE CONTRATO";
                            break;

                        case TipoDeProcesoCierreDeInventario.CambioDeOperacion:
                            lblTipoDeProceso.Text = "CAMBIO DE OPERACIÓN";
                            break;

                        case TipoDeProcesoCierreDeInventario.CierreDeOperacion:
                            lblTipoDeProceso.Text = "CIERRE DE OPERACIÓN";
                            break;
                    }

                    if(tpProceso == TipoDeProcesoCierreDeInventario.CierreDeOperacion)
                    {
                        sFarmaciaNueva = DtGeneral.FarmaciaConectada;
                        sEmpresaNueva = DtGeneral.EmpresaConectada;
                        lblFarmaciaNueva.Text = sFarmaciaNueva + " -- " + DtGeneral.FarmaciaConectadaNombre;
                    }
                    else 
                    {
                        sFarmaciaNueva = leer.Campo("IdFarmaciaNueva");
                        sEmpresaNueva = leer.Campo("IdEmpresaNueva");
                        lblFarmaciaNueva.Text = sFarmaciaNueva + " -- " + leer.Campo("Farmacia");
                    }

                    bRegresa = true;
                }
            }

            return bRegresa;
        }
        #endregion Funciones

        #region Impresion de informacion
        private void Generar_PDFS()
        {
            bGenerando_PDFs = true; 

            if (sFolioSalidaVenta != "") Imprimir_PDF(sFolioSalidaVenta, 1);
            if (sFolioSalidaConsignacion != "") Imprimir_PDF(sFolioSalidaConsignacion, 2);
            if (sFolioEntradaVenta != "") Imprimir_PDF(sFolioEntradaVenta, 3);
            if (sFolioEntradaConsignacion != "") Imprimir_PDF(sFolioEntradaConsignacion, 4);

            bGenerando_PDFs = false;

            //////sFolioSalidaVenta = leer.Campo("FolioSalidaVenta");
            //////sFolioSalidaConsignacion = leer.Campo("FolioSalidaConsignacion");

            //////sFolioEntradaVenta = leer.Campo("FolioEntradaVenta");
            //////sFolioEntradaConsignacion = leer.Campo("FolioEntradaConsignacion");

        }

        private void Imprimir(string FolioInventario)
        {
            bool bRegresa = false;

            DatosCliente.Funcion = "ImprimirInventario()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "PtoVta_InventarioInicial.rpt";

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmaciaNueva);
            myRpt.Add("Folio", FolioInventario);

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }

        private string NombrePDF_Mostrar(int TipoReporte)
        {
            string sRegresa = "";

            switch (TipoReporte)
            {
                case 1:
                    sRegresa = "SALIDA_DE_VENTA";
                    break;

                case 2:
                    sRegresa = "SALIDA_DE_CONSIGNACION";
                    break;

                case 3:
                    sRegresa = "ENTRADA_DE_VENTA";
                    break;

                case 4:
                    sRegresa = "ENTRADA_DE_CONSIGNACION";
                    break;
            }

            return sRegresa;
        }

        private string NombrePDF(int TipoReporte)
        {
            DateTime fecha = DateTime.Now; 
            string sRegresa = "";
            string sMarcaTiempo = string.Format("{0}{1}{2}_{3}{4}{5}", 
                Fg.PonCeros(fecha.Year, 4), 
                Fg.PonCeros(fecha.Month, 2), 
                Fg.PonCeros(fecha.Day, 2), 
                Fg.PonCeros(fecha.Hour, 2), 
                Fg.PonCeros(fecha.Minute, 2), 
                Fg.PonCeros(fecha.Second, 2)
                );

            switch (TipoReporte)
            {
                case 1:
                    sRegresa = "SALIDA_DE_VENTA";
                    break;

                case 2:
                    sRegresa = "SALIDA_DE_CONSIGNACION";
                    break;

                case 3:
                    sRegresa = "ENTRADA_DE_VENTA";
                    break;

                case 4:
                    sRegresa = "ENTRADA_DE_CONSIGNACION";
                    break; 
            }

            sRegresa = string.Format("{0}___{1}___F{2}", Fg.PonCeros(TipoReporte, 2), sRegresa, sMarcaTiempo);

            return sRegresa; 
        }

        private void Imprimir_PDF(string FolioInventario, int TipoReporte)
        {
            bool bRegresa = false;
            string sNombre = Path.Combine(sRutaDeReportes_PDF, NombrePDF(TipoReporte) + ".pdf");
            string sNombre_Simple = NombrePDF_Mostrar(TipoReporte);

            DatosCliente.Funcion = "ImprimirInventario()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            clsReporteador Reporteador;  // = new clsReporteador(Reporte, DatosTerminal);

            // byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "PtoVta_InventarioInicial.rpt";

            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("Folio", FolioInventario);

            //// Especificar el reporte a generar 
            switch (TipoReporte)
            {
                case 1:
                case 2:
                    myRpt.Add("IdEmpresa", sEmpresa);
                    myRpt.Add("IdFarmacia", sFarmacia);
                    break; 

                case 3:
                case 4:
                    myRpt.Add("IdEmpresa", sEmpresaNueva);
                    myRpt.Add("IdFarmacia", sFarmaciaNueva);
                    break; 
            }

            //bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, sNombre, FormatosExportacion.PortableDocFormat);

            SetAvance(string.Format("Generando pdf {0} con folio {1}", sNombre_Simple, FolioInventario));
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);


            Reporteador = new clsReporteador(myRpt, DatosCliente);
            Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
            Reporteador.Url = General.Url;
            Reporteador.MostrarInterface = false;
            Reporteador.MostrarMensaje_ReporteSinDatos = false;

            bRegresa = Reporteador.ExportarReporte(sNombre, FormatosExportacion.PortableDocFormat, true);


            ////if (!bRegresa)
            ////{
            ////    General.msjError("Ocurrió un error al cargar el reporte.");
            ////}
        }
        #endregion Impresion de informacion

        private void tmAvance_Tick(object sender, EventArgs e)
        {
            //Thread _wT = new Thread(this.GetAvance);
            //_wT.Name = "GetAdvance";
            //wT.Start();
            GetAvance(); 
        }

        private void tmProceso_Tick(object sender, EventArgs e)
        {
            if (!bProcesando)
            {
                tmProceso.Stop();
                tmProceso.Enabled = false; 

                if (bSeGeneroCierre) 
                {
                    General.msjUser(sMensaje_Folios); //Este mensaje lo genera el SP 

                    //Transferencia.ExportarInformacionBD(false);  

                    //Imprimir(sFolioEntradaVenta);
                    //Imprimir(sFolioEntradaConsignacion);

                    //Generar_PDFS(); 

                    ReiniciarSistema();  
                }
            }
        }

        private void btnExportarReportes_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fx = new FolderBrowserDialog();
            string sFolder = Environment.SpecialFolder.Desktop.ToString(); 
            sFolder = sFolder == "" ? sFolder : lblDirectorioExportar.Text;

            fx.SelectedPath = sFolder;
            fx.ShowDialog();
            if (fx.SelectedPath != "")
            {
                lblDirectorioExportar.Text = fx.SelectedPath;
            }

        }

        private void tmPantalla_Tick(object sender, EventArgs e)
        {
            tmPantalla.Enabled = false;
            if (!DtGeneral.ValidaTransferenciasTransito("No es posible realizar el cierre de operación, se encontraron productos en Tránsito."))
            {
                this.Close();
            }
        }
    }
}
