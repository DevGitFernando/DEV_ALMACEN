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
    public partial class FrmResumenMovimientos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        clsListView lst;
        Thread _workerThread;

        clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        clsDatosCliente DatosCliente;

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        string sRutaPlantilla = "";

        public FrmResumenMovimientos()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnAdministracion.DatosApp, this.Name, "");
            CheckForIllegalCrossThreadCalls = false;

            leer = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, General.DatosApp, this.Name);

            lst = new clsListView(lstMovtos);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;

            Cargar_Empresas();
        }

        private void FrmResumenMovimientos_Load(object sender, EventArgs e)
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

            _workerThread = new Thread(this.CargarDatos);
            _workerThread.Name = "Cargando Información";
            _workerThread.Start();
            
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportaMovtosEdos();
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            ExportarPDF();
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
            Fg.IniciaControles(this, true);

            cboEmpresas.Data = DtGeneral.EmpresaConectada;
            cboEstados.Data = DtGeneral.EstadoConectado;

            FrameDatos.Enabled = false;
            btnExportarExcel.Enabled = false;
            btnExportarPDF.Enabled = false;

            lst.Limpiar();
            lst.LimpiarItems();

            cboEmpresas.Focus();
        }

        private void CargarDatos()
        {
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            string sSql = "";

            sSql = string.Format(" Exec spp_Rpt_ConsultaTipoMovtos_Estados '{0}', '{1}' ", cboEstados.Data, General.FechaYMD(dtpFechaInicial.Value));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDatos()");
                General.msjAviso("No fue posible obtener la información solicitada, intente de nuevo.");
            }
            else
            {                
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
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
            btnExportarPDF.Enabled = bSeEncontroInformacion;
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
                FrameFechas.Enabled = true;               
                btnNuevo.Enabled = true;
                btnEjecutar.Enabled = true;
                btnExportarExcel.Enabled = true;
                btnExportarPDF.Enabled = true;

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

        #region Reportes
        private void ExportaMovtosEdos()
        {
            int iRow = 2;
            string sNombreFile = "";
            string sRutaReportes = "";
            
            sRutaReportes = GnAdministracion.RutaReportes;
            DtGeneral.RutaReportes = sRutaReportes;

            sNombreFile = "ADMON_Rpt_TiposMovtosEstados" + "_" + DtGeneral.EstadoConectado + ".xls";
            sRutaPlantilla = Application.StartupPath + @"\\Plantillas\ADMON_Rpt_TiposMovtosEstados.xls"; 
            bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "ADMON_Rpt_TiposMovtosEstados.xls", DatosCliente);

            if (bRegresa)
            {
                xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                xpExcel.AgregarMarcaDeTiempo = false;

                if (xpExcel.PrepararPlantilla(sNombreFile))
                {
                    xpExcel.GeneraExcel();

                    //Se pone el encabezado
                    leerExportarExcel.RegistroActual = 1;
                    leerExportarExcel.Leer();
                    xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow, 2);
                    iRow++;
                    xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, iRow, 2);
                    iRow++;

                    iRow = 6;
                    xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);

                    // Se ponen los detalles
                    leerExportarExcel.RegistroActual = 1;
                    iRow = 9;

                    while (leerExportarExcel.Leer())
                    {
                        xpExcel.Agregar(leerExportarExcel.Campo("Id SubFarmacia"), iRow, 2);
                        xpExcel.Agregar(leerExportarExcel.Campo("SubFarmacia"), iRow, 3);
                        xpExcel.Agregar(leerExportarExcel.Campo("Tipo Movimiento"), iRow, 4);
                        xpExcel.Agregar(leerExportarExcel.Campo("Descripción Movimiento"), iRow, 5);
                        xpExcel.Agregar(leerExportarExcel.Campo("Año"), iRow, 6);
                        xpExcel.Agregar(leerExportarExcel.Campo("Núm. Mes"), iRow, 7);
                        xpExcel.Agregar(leerExportarExcel.Campo("Mes"), iRow, 8);
                        xpExcel.Agregar(leerExportarExcel.Campo("Piezas"), iRow, 9);
                        xpExcel.Agregar(leerExportarExcel.Campo("Claves"), iRow, 10);

                        iRow++;
                    }

                    // Finalizar el Proceso 
                    xpExcel.CerrarDocumento();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }
                }
            }
        }

        private void ExportarPDF()
        {
            bool bRegresa = true;
            int iAnio = 0, iMes = 0;

            iAnio = dtpFechaInicial.Value.Year;
            iMes = dtpFechaInicial.Value.Month;
            
            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            byte[] btReporte = null;
            string sNombre = "Resumen_Movimientos_Estado_" + DtGeneral.EstadoConectadoNombre + ".pdf";

            myRpt.RutaReporte = GnAdministracion.RutaReportes;
            
            myRpt.Add("IdEstado", cboEstados.Data);
            myRpt.Add("Año", iAnio);
            myRpt.Add("Mes", iMes);
            myRpt.Add("Empresa", DtGeneral.EmpresaConectadaNombre);
            myRpt.NombreReporte = "ADMON_Rpt_TiposMovtosEstados";

           
            bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, sNombre, FormatosExportacion.PortableDocFormat);
            
            
            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }            
        }
        #endregion Reportes
        
    }
}
