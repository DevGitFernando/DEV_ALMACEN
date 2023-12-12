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
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace DllAdministracion.Reportes
{
    public partial class FrmConsumosEstados : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        Thread _workerThread;        

        clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;        

        clsDatosCliente DatosCliente;        

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;       

        string sRutaPlantilla = "";
        string sFormato = "###, ###, ###, ##0";

        public FrmConsumosEstados()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnAdministracion.DatosApp, this.Name, "");
            CheckForIllegalCrossThreadCalls = false;

            leer = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, General.DatosApp, this.Name);

            Cargar_Empresas();

            FrameProceso.Top = 70;
            FrameProceso.Left = 73;
            MostrarEnProceso(false);
        }

        private void FrmConsumosEstados_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnExportarExcel.Enabled = false;

            FrameDatos.Enabled = false;
            FrameFechas.Enabled = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerConsumosEstado);
            _workerThread.Name = "Obteniendo Información";
            _workerThread.Start();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnExportarExcel.Enabled = false;
            FrameFechas.Enabled = false;
            MostrarEnProceso(true);

            GenerarExcel();

            MostrarEnProceso(false);
            btnNuevo.Enabled = true;
            btnEjecutar.Enabled = true;
            btnExportarExcel.Enabled = true;
            FrameFechas.Enabled = true;
        }
        #endregion Botones

        #region Cargar_Combos
        private void Cargar_Empresas()
        {
            string sSql = "";
            cboEmpresas.Clear();
            cboEmpresas.Add();

            sSql = "Select IdEmpresa, Nombre, EsDeConsignacion From CatEmpresas (NoLock) Where Status = 'A' Order by IdEmpresa ";

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    cboEmpresas.Add(leer.DataSetClase, true, "IdEmpresa", "Nombre");
                }
            }
            else
            {
                Error.LogError(leer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
        }

        private void Cargar_Estados()
        {
            string sSql = "", sEmpresa = "";

            sEmpresa = cboEmpresas.Data;
            sEmpresa = Fg.PonCeros(sEmpresa, 3);
            cboEstados.Clear();
            cboEstados.Add();

            sSql = string.Format(" Select IdEstado, NombreEstado, ClaveRenapo From vw_EmpresasEstados (NoLock) " +
                                " Where IdEmpresa = '{0}' AND StatusEdo = 'A' Order by IdEstado ", sEmpresa);
            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    cboEstados.Add(leer.DataSetClase, true, "IdEstado", "NombreEstado");
                }
            }
            else
            {
                Error.LogError(leer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Estados.");
            }

        }
        #endregion Cargar_Combos

        #region Funciones
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();

            cboEmpresas.Data = DtGeneral.EmpresaConectada;
            cboEstados.Data = DtGeneral.EstadoConectado;            

            FrameDatos.Enabled = false;
            btnExportarExcel.Enabled = false;
            FrameFechas.Enabled = true;
            lblExportados.Text = "";

            dtpFechaInicial.Focus();
        }

        private void ObtenerConsumosEstado()
        {
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;            

            string sSql = "", sStore = "";
            int iMostrarTipoRes = 0;

            sSql = string.Format(" Exec spp_Rpt_VentasPorClaveMensual '{0}', '*', '*', '*', '{1}', '{2}', 0, 0, '', 0, 0, 0, '*', '*', 0, {3} ", cboEstados.Data,
                        Fg.PonCeros(dtpFechaInicial.Value.Year, 4) + "-" + Fg.PonCeros(dtpFechaInicial.Value.Month, 2) + "-01",
                        Fg.PonCeros(dtpFechaFinal.Value.Year, 4) + "-" + Fg.PonCeros(dtpFechaFinal.Value.Month, 2) + "-01", iMostrarTipoRes);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerConsumosEstado()");
                General.msjAviso("No fue posible obtener la información solicitada, intente de nuevo.");
            }
            else
            {
                if (leer.Leer())
                {
                    leerExportarExcel.DataSetClase = leer.DataSetClase;                    
                    bSeEncontroInformacion = true;
                }
                else
                {
                    bSeEjecuto = true;
                }
            }

            bEjecutando = false;
            this.Cursor = Cursors.Default;
        }

        private void MostrarEnProceso(bool Mostrar)
        {

            if (Mostrar)
            {
                FrameProceso.Left = 73;
            }
            else
            {
                FrameProceso.Left = this.Width + 10;
            }

        }
        #endregion Funciones

        #region Eventos
        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEmpresas.SelectedIndex != 0)
            {
                Cargar_Estados();
            }
        }
        #endregion Eventos

        #region Funciones_Hilos
        private void ActivarControles()
        {
            //this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true;
            btnEjecutar.Enabled = true;
            btnExportarExcel.Enabled = bSeEncontroInformacion;
            FrameDatos.Enabled = false;
            FrameFechas.Enabled = true;
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                FrameDatos.Enabled = false;
                btnNuevo.Enabled = true;
                btnEjecutar.Enabled = true;
                btnExportarExcel.Enabled = true;
                FrameFechas.Enabled = true;

                if (!bSeEncontroInformacion)
                {
                    _workerThread.Interrupt();
                    _workerThread = null;

                    ActivarControles();

                    if (bSeEjecuto)
                    {
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                    }
                }
            }
        }
        #endregion Funciones_Hilos

        #region Exportar_Excel
        private void GenerarExcel()
        {
            string sRutaPlantilla = "";
            bool bRegresa = false;

            sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Admon_Consumos_Mensuales_Estado.xls";
            bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Admon_Consumos_Mensuales_Estado.xls", DatosCliente);
            

            if (bRegresa)
            {
                xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                xpExcel.AgregarMarcaDeTiempo = true;

                this.Cursor = Cursors.Default;
                if (xpExcel.PrepararPlantilla())
                {

                    //this.Cursor = Cursors.WaitCursor;
                    ExportarConsumosEstado();                 
                                        
                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void ExportarConsumosEstado()
        {
            int iHoja = 1, iRenglon = 9, iRegistros = 0;
            int iColumnaActual = 0, iColumna = 0;
            string sPeriodo = "", sNombreColumna = "";
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            iRegistros = leerExportarExcel.Registros;

            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
            xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, 3, 2);

            xpExcel.Agregar(sFechaImpresion, 6, 3);

            leerExportarExcel.RegistroActual = 1;
            
            while (leerExportarExcel.Leer())
            {
                xpExcel.Agregar(leerExportarExcel.Campo("IdEstado"), iRenglon, 2);
                xpExcel.Agregar(leerExportarExcel.Campo("IdFarmacia"), iRenglon, 3);
                xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRenglon, 4);
                xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, 5);
                xpExcel.Agregar(leerExportarExcel.Campo("DescripcionClave"), iRenglon, 6);
                xpExcel.Agregar(leerExportarExcel.Campo("Tipoinsumo"), iRenglon, 7);
                xpExcel.Agregar(leerExportarExcel.Campo("Año"), iRenglon, 8);
                xpExcel.Agregar(leerExportarExcel.Campo("Enero"), iRenglon, 9);
                xpExcel.Agregar(leerExportarExcel.Campo("Febrero"), iRenglon, 10);

                xpExcel.Agregar(leerExportarExcel.Campo("Marzo"), iRenglon, 11);
                xpExcel.Agregar(leerExportarExcel.Campo("Abril"), iRenglon, 12);
                xpExcel.Agregar(leerExportarExcel.Campo("Mayo"), iRenglon, 13);
                xpExcel.Agregar(leerExportarExcel.Campo("Junio"), iRenglon, 14);
                xpExcel.Agregar(leerExportarExcel.Campo("Julio"), iRenglon, 15);
                xpExcel.Agregar(leerExportarExcel.Campo("Agosto"), iRenglon, 16);
                xpExcel.Agregar(leerExportarExcel.Campo("Septiembre"), iRenglon, 17);
                xpExcel.Agregar(leerExportarExcel.Campo("Octubre"), iRenglon, 18);
                xpExcel.Agregar(leerExportarExcel.Campo("Noviembre"), iRenglon, 19);
                xpExcel.Agregar(leerExportarExcel.Campo("Diciembre"), iRenglon, 20);
                xpExcel.Agregar(leerExportarExcel.Campo("Total"), iRenglon, 21);
                
                iRenglon++;

                lblExportados.Text = iRenglon.ToString(sFormato) + "  de " + iRegistros.ToString(sFormato);
            }

            lblExportados.Text = " Terminada ";

            xpExcel.CerrarDocumento();

        }        
        #endregion Exportar_Excel
    }
}
