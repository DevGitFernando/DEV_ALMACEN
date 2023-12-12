using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace DllFarmaciaSoft.Facturacion
{
    public partial class FrmCierrePeriodoUnidad : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        // Manejo de reportes  
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        Thread _workerThread;


        //clsOperacionesSupervizadas Permisos;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sFolio = "";
        string sMensaje = "";
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        bool bPermisoDiasCierreTickets = false;
        string sPermisoDiasCierreTickets = "GENERAR_CIERRES_TICKETS_DIAS_ADICIONAL";
        int iDiasCierreTickets = 0;
        int iEsPrecierre = 0;

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        // bool bSeEjecuto = false;

        string sFolioInv = "";
        string sMensajeInv = "";

        public FrmCierrePeriodoUnidad()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            //cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            SolicitarPermisosUsuario();
        }

        private void FrmCierrePeriodoUnidad_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            iEsPrecierre = 0;  
            Fg.IniciaControles(this, true);
            btnGuardar.Enabled = true;
            btnPrecierre.Enabled = true; 

            //IniciaToolBar(true, true, false);
            // btnImprimir.Visible = false;
            dtpFechaCierre.MaxDate = General.FechaSistema;

            lblMensaje.Text = "Todos los tickets menor ó igual a la fecha seleccionada se cerrarán, no podra hacer cambios a la información de un ticket cerrado.";

            if( bPermisoDiasCierreTickets )
            {
                iDiasCierreTickets = DtGeneral.DiasAdicionalesCierreTickets;
            }

            if (!DtGeneral.EsAdministrador)
            {
                dtpFechaCierre.MinDate = General.FechaSistema.AddMonths(-1);
                dtpFechaCierre.MinDate = dtpFechaCierre.MinDate.AddDays(-iDiasCierreTickets);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            iEsPrecierre = 0;
            Procesar(); 
        }

        private void btnPrecierre_Click(object sender, EventArgs e)
        {
            iEsPrecierre = 1;
            Procesar(); 
        }
        #endregion Botones

        #region Funciones 
        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            ////// Valida si el usuario conectado tiene permiso sobre las opcione especiales 
            ////Permisos = new clsOperacionesSupervizadas(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            ////Permisos.Personal = DtGeneral.IdPersonal; 

            bPermisoDiasCierreTickets = DtGeneral.PermisosEspeciales.TienePermiso(sPermisoDiasCierreTickets);
            bPermisoDiasCierreTickets = true; 
        }
        #endregion Permisos de Usuario

        private void IniciaToolBar( bool bNuevo)
        {
            btnNuevo.Enabled = bNuevo; 
        }

        private void Procesar()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = false;
            btnPrecierre.Enabled = false;

            // bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoValidacion";
            _workerThread.Start();
            //AplicarCierrePeriodo();  
        }

        private void ObtenerInformacion()
        {
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            //cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.Limite90;

            leer = new clsLeer(ref cnn);

            bSeEncontroInformacion = AplicarCierrePeriodo();

            // bSeEjecuto = true; 
            bEjecutando = false; // Cursor.Current  

            this.Cursor = Cursors.Default;
        } 

        private bool AplicarCierrePeriodo()
        {
            bool bRegresa = false;

            if (VerificarPeriodoAbierto())
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {

                    cnn.IniciarTransaccion();

                    string sSql = string.Format("Exec spp_Mtto_Ctl_CierresDePeriodos  " +
                        " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioCierre = '{3}', @IdPersonal = '{4}', @FechaCierre = '{5}', " + 
                        " @IdEstadoRegistra = '{6}', @IdFarmaciaRegistra = '{7}', @IdPersonalRegistra = '{8}', @EsVistaPrevia = '{9}' ",
                        sEmpresa, sEstado, sFarmacia, sFolio, DtGeneral.IdPersonal, General.FechaYMD(dtpFechaCierre.Value, "-"),
                        sEstado, sFarmacia, DtGeneral.IdPersonal, iEsPrecierre);

                    if (!leer.Exec(sSql)) 
                    {
                        bRegresa = false;
                    }
                    else
                    {
                        if (leer.Leer())
                        {
                            sMensaje = leer.Campo("Mensaje");
                            sFolio = leer.Campo("Folio");
                            bRegresa = true;
                        }

                        //// Solo aplica para las farmacias 
                        if (!DtGeneral.EsAlmacen)
                        {
                            bRegresa = GeneraInventarioAleatorio();
                        }
                    }
                    
                    if (!bRegresa)
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "AplicarCierrePeriodo()");
                        General.msjError("Ocurrió un error al Generar Cierre de Periodo.");
                        //IniciaToolBar(true, true, false);
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje);

                        if (!DtGeneral.EsAlmacen)
                        {
                            General.msjUser(sMensajeInv);// este mensaje lo regresa el sp que genera el Folio de inventario aleatorio.
                        }

                        
                        ////if (iEsPrecierre == 0)
                        ////{
                        ////    ImprimirConcentradoCierrePeriodo(1);
                        ////    ImprimirConcentradoCierrePeriodo(2);
                        ////}

                        ////if (iEsPrecierre == 1)
                        ////{
                        ////    ImprimirConcentradoCierrePeriodoVistaPrevia(1);
                        ////    ImprimirConcentradoCierrePeriodoVistaPrevia(2);
                        ////}

                        // btnNuevo_Click(null, null);
                    }

                    cnn.Cerrar();
                }
            }

            return bRegresa;
        }
        
        private bool VerificarPeriodoAbierto()
        {
            bool bRegresa = false;

            string sSql = string.Format(" Select top 1 * From VentasEnc (Nolock) " +
                                        " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioCierre = 0 " +
                                        " And Convert( varchar(10), FechaRegistro, 120) <= '{3}' ",
                                      sEmpresa, sEstado, sFarmacia, General.FechaYMD(dtpFechaCierre.Value, "-"));

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    bRegresa = true;
                }
                else
                {
                    General.msjUser(" No Existen Periodos Abiertos.... ");
                }

            }
            else
            {
                Error.GrabarError(leer, "VerificarPeriodoAbierto()");
                General.msjError("Ocurrió un error al Verificar Cierre de Periodos.");
            }

            return bRegresa;
        }
        
        #endregion Funciones

        #region Impresion
        private void ImprimirCierrePeriodo()
        {
            bool bRegresa = false;

            DatosCliente.Funcion = "ImprimirCorteDiario()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "PtoVta_CierreDePeriodo.rpt";

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("@FolioCierre", sFolio);    
            
            DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            DataSet datosC = DatosCliente.DatosCliente();
            bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);

            
            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }

        private void ImprimirConcentradoCierrePeriodo(int Tipo)
        {
            bool bRegresa = false;
            int iFolio = 0;

            iFolio = Convert.ToInt32(sFolio);

            DatosCliente.Funcion = "ImprimirConcentradoCierrePeriodo()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "PtoVta_CierrePeriodoFacturacion.rpt";

            if (Tipo == 2)
            {
                myRpt.NombreReporte = "PtoVta_CierrePeriodoFacturacionDetallado.rpt";
            }

            myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("Folio", Fg.PonCeros(sFolio, 8));

            DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            DataSet datosC = DatosCliente.DatosCliente();
            bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);


            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }

        private void ImprimirConcentradoCierrePeriodoVistaPrevia(int Tipo)
        {
            bool bRegresa = false;
            int iFolio = 0;

            iFolio = Convert.ToInt32(sFolio);

            DatosCliente.Funcion = "ImprimirConcentradoCierrePeriodoDetallado()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "PtoVta_CierrePeriodoFacturacion_VP.rpt";

            if (Tipo == 2)
            {
                myRpt.NombreReporte = "PtoVta_CierrePeriodoFacturacionDetallado_VP.rpt";
            }

            myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("Folio", Fg.PonCeros(sFolio, 8));

            DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            DataSet datosC = DatosCliente.DatosCliente();
            bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente); 

            if (!bRegresa) 
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Impresion       

        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = true;
            btnPrecierre.Enabled = true; 
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                //FrameListaReportes.Enabled = true; 
                // btnEjecutar.Enabled = true;
                //btnNuevo.Enabled = true; 
                ActivarControles();

                if (bSeEncontroInformacion)
                {
                    if (iEsPrecierre == 0)
                    {
                        ImprimirConcentradoCierrePeriodo(1);
                        ImprimirConcentradoCierrePeriodo(2);
                    }

                    if (iEsPrecierre == 1)
                    {
                        ImprimirConcentradoCierrePeriodoVistaPrevia(1);
                        ImprimirConcentradoCierrePeriodoVistaPrevia(2);
                    } 
                }
                else
                {
                    _workerThread.Interrupt();
                    _workerThread = null;                    

                    //if (bSeEjecuto)
                    //{
                    //    General.msjUser("No existe informacion para mostrar bajo los criterios seleccionados.");
                    //}
                }
            }
        }

        #region Genera_INV_Aleatorio
        private bool GeneraInventarioAleatorio()
        {
            bool bRegresa = true;
            string sSql = "";
            int iTipoInv = 0, iNumClaves = 0;

            iTipoInv = 3;
            iNumClaves = GnFarmacia.Claves_Inventario_Aleatorio__CierrePeriodo;

            sSql = String.Format(" Exec spp_INV_Aleatorios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                            sEmpresa, sEstado, sFarmacia, iTipoInv, iNumClaves, DtGeneral.IdPersonal,
                            General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-"), Convert.ToInt32(sFolio));

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    sFolioInv = leer.Campo("Folio");
                    sMensajeInv = leer.Campo("Mensaje");
                }
            }

            return bRegresa;
        }
        #endregion Genera_INV_Aleatorio
    }
}
