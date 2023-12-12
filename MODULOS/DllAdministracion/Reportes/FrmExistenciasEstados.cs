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
    public partial class FrmExistenciasEstados : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        
        Thread _workerThread;
        Thread _ExportThread;

        clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;
        clsLeer leerFarmacias;

        clsDatosCliente DatosCliente;
        DataSet dtsFarmacias = new DataSet();

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;
        bool bExportando = false;
        bool bExporTerminado = false;

        string sRutaPlantilla = "";
        string sFormato = "###, ###, ###, ##0";

        int iUnidades = 0;

        public FrmExistenciasEstados()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnAdministracion.DatosApp, this.Name, "");
            CheckForIllegalCrossThreadCalls = false;

            leer = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            leerFarmacias = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, General.DatosApp, this.Name);

            Cargar_Empresas();

            FrameProceso.Top = 70;
            FrameProceso.Left = 73;
            MostrarEnProceso(false); 
        }

        private void FrmExistenciasEstados_Load(object sender, EventArgs e)
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
            grpReporte.Enabled = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacionExistencias);
            _workerThread.Name = "Cargando Información";
            _workerThread.Start();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            
            ToolBarControles(false);
            MostrarEnProceso(true);

            GenerarExcel();

            MostrarEnProceso(false);
            ToolBarControles(true);
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

            dtsFarmacias = new DataSet();
            iUnidades = 0;

            FrameDatos.Enabled = false;
            btnExportarExcel.Enabled = false;
            grpReporte.Enabled = true;
            lblExportados.Text = "";

            cboEmpresas.Focus();
        }

        private void ObtenerInformacionExistencias()
        {
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            dtsFarmacias = new DataSet();

            string sSql = "", sStore = "";

            if (rdoExistUnidad.Checked)
            {
                sStore = "sp_Rpt_Admon_Claves_Existencia_Unidades_Estado";
            }

            if (rdoExistEstado.Checked)
            {
                sStore = "sp_Rpt_Admon_Claves_Existencia_Estado_Concentrado";
            }

            sSql = string.Format(" Exec {0}  '{1}', '{2}' ", sStore, cboEmpresas.Data, cboEstados.Data);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerInformacionExistencias()");
                General.msjAviso("No fue posible obtener la información solicitada, intente de nuevo.");
            }
            else
            {
                if (leer.Leer())
                {                    
                    leerExportarExcel.DataSetClase = leer.DataSetClase;
                    if (rdoExistUnidad.Checked)
                    {
                        dtsFarmacias.Tables.Add(leer.DataSetClase.Tables[1].Copy());
                        leerFarmacias.DataSetClase = dtsFarmacias;
                        leerFarmacias.Leer();
                        iUnidades = leerFarmacias.CampoInt("NumFarmacias");
                    }
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

        private void ToolBarControles(bool Valor)
        {
            btnNuevo.Enabled = Valor;
            btnEjecutar.Enabled = Valor;
            btnExportarExcel.Enabled = Valor;
            grpReporte.Enabled = Valor;
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
            grpReporte.Enabled = true;
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
                grpReporte.Enabled = true;
                
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

            if (rdoExistEstado.Checked)
            {
                sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Admon_Existencias_Claves_Estado.xls";
                bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Admon_Existencias_Claves_Estado.xls", DatosCliente);
            }

            if (rdoExistUnidad.Checked)
            {
                sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Admon_Existencias_Claves_Unidades_Estado.xls";
                bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Admon_Existencias_Claves_Unidades_Estado.xls", DatosCliente);
            }

            if (bRegresa)
            {
                xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                xpExcel.AgregarMarcaDeTiempo = true;               

                this.Cursor = Cursors.Default;
                if (xpExcel.PrepararPlantilla())
                {
                    
                    //this.Cursor = Cursors.WaitCursor;
                    if (rdoExistEstado.Checked)
                    {
                        ExportarExistenciasEstado();
                    }

                    if (rdoExistUnidad.Checked)
                    {
                        ExportarExistenciaUnidades();
                    }

                    bExportando = false;
                    bExporTerminado = true;
                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void ExportarExistenciasEstado()
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
            //MostrarEnProceso(true);
            while (leerExportarExcel.Leer())
            {
                xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, 2);
                xpExcel.Agregar(leerExportarExcel.Campo("DescripcionSal"), iRenglon, 3);
                xpExcel.Agregar(leerExportarExcel.Campo("Presentacion_ClaveSSA"), iRenglon, 4);
                xpExcel.Agregar(leerExportarExcel.Campo("ContenidoPaquete"), iRenglon, 5);
                xpExcel.Agregar(leerExportarExcel.Campo("Admon_Causes"), iRenglon, 6);
                xpExcel.Agregar(leerExportarExcel.Campo("Admon_NoCauses"), iRenglon, 7);
                xpExcel.Agregar(leerExportarExcel.Campo("Venta_Causes"), iRenglon, 8);
                xpExcel.Agregar(leerExportarExcel.Campo("Venta_NoCauses"), iRenglon, 9);
                xpExcel.Agregar(leerExportarExcel.Campo("ExistenciaGeneral"), iRenglon, 10);           


                iRenglon++;

                lblExportados.Text = iRenglon.ToString(sFormato) + " de " + iRegistros.ToString(sFormato);
            }

            lblExportados.Text = " Terminada ";
            //MostrarEnProceso(false);
            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();

        }

        private void ExportarExistenciaUnidades()
        {
            int iHoja = 1, iRenglon = 9, iRegistros = 0;
            int iColumnaActual = 0, iColumna = 0;
            string sPeriodo = "", sNombreColumna = "";
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            iRegistros = leerExportarExcel.Registros;
            //leer.DataSetClase = dtsMovimientos;
            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
            xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, 3, 2);


            xpExcel.Agregar(sFechaImpresion, 6, 3);

            //MostrarEnProceso(true);

            AgregarColumnasDinamicas();
            leerExportarExcel.RegistroActual = 1;
            while (leerExportarExcel.Leer())
            {
                xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, 2);
                xpExcel.Agregar(leerExportarExcel.Campo("DescripcionSal"), iRenglon, 3);
                xpExcel.Agregar(leerExportarExcel.Campo("Presentacion_ClaveSSA"), iRenglon, 4);
                xpExcel.Agregar(leerExportarExcel.Campo("ContenidoPaquete"), iRenglon, 5);
                xpExcel.Agregar(leerExportarExcel.Campo("ExistenciaGeneral"), iRenglon, 6);


                iColumnaActual = 0;
                iColumna = 7;
                //Se agregan los campos dinamicos.
                foreach (DataColumn dtCol in leerExportarExcel.DataSetClase.Tables[0].Columns)
                {
                    iColumnaActual++;

                    if (iColumnaActual >= 6)
                    {
                        sNombreColumna = dtCol.ColumnName;
                        xpExcel.Agregar(leerExportarExcel.Campo(sNombreColumna), iRenglon, iColumna);
                        iColumna++;
                    }
                }

                iRenglon++;
                lblExportados.Text = iRenglon.ToString(sFormato) + " de " + iRegistros.ToString(sFormato);
            }

            lblExportados.Text = " Terminada ";
            //MostrarEnProceso(false);
            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();

        }

        private void AgregarColumnasDinamicas()
        {
            int iColumnaActual = 0;
            int iColumna = 7;
            string sNombre = "";            

            foreach (DataColumn dtCol in leerExportarExcel.DataSetClase.Tables[0].Columns)
            {
                iColumnaActual++;

                if (iColumnaActual >= 6)
                {
                    sNombre = dtCol.ColumnName;
                    xpExcel.Agregar(sNombre, 8, iColumna);
                    iColumna++;
                }
            }

        }
        #endregion Exportar_Excel
        
    }
}
