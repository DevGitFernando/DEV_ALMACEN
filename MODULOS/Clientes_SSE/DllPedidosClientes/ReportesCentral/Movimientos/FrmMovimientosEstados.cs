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
//using SC_SolutionsSystem.ExportarDatos;

namespace DllPedidosClientes.ReportesCentral
{
    public partial class FrmMovimientosEstados : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;        

        //clsExportarExcelPlantilla xpExcel;
        clsListView lst;
        clsListView lstTotales;
        Thread _workerThread;

        DataSet dtsMovtos = new DataSet();
        DataSet dtsTotales = new DataSet();

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;


        public FrmMovimientosEstados()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, General.DatosApp, this.Name);

            lst = new clsListView(lstMovtos);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;

            lstTotales = new clsListView(lstTotalizado);
            lstTotales.OrdenarColumnas = true;
            lstTotales.PermitirAjusteDeColumnas = true;
        }

        private void FrmMovimientosEstados_Load(object sender, EventArgs e)
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
            FrameMovimientos.Enabled = false;

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
            /*
            if (rdoEntradas.Checked)
            {
                ExportaEntradas();
            }
            if (rdoDispensacion.Checked)
            {
                ExportaSalidas();
            }
            if (rdoDistribucion.Checked)
            {
                ExportaTransferencias();
            }
            */
        }
        #endregion Botones

        #region Cargar_Combos        
        private void Cargar_Estados()
        {
            if (cboEstados.NumeroDeItems == 0)
            {
                cboEstados.Clear();
                cboEstados.Add();

                cboEstados.Add(DtGeneralPedidos.Estados, true, "IdEstado", "Estado");

            }
            cboEstados.SelectedIndex = 0;
            cboEstados.Data = DtGeneralPedidos.EstadoConectado;
            cboEstados.Enabled = false;
        }

        private void CargarJurisdicciones()
        {
            if (cboJurisdicciones.NumeroDeItems == 0)
            {
                cboJurisdicciones.Clear();
                cboJurisdicciones.Add("*", "Todas las jurisdicciones");

                cboJurisdicciones.Add(DtGeneralPedidos.Jurisdiscciones, true, "IdJurisdiccion", "NombreJurisdiccion");
            }
            cboJurisdicciones.SelectedIndex = 0;
        }
        #endregion Cargar_Combos

        #region Funciones
        private void LimpiarPantalla()
        {
            Fg.IniciaControles(this, true);
            dtsMovtos = new DataSet();
            dtsTotales = new DataSet();
            Cargar_Estados();
            CargarJurisdicciones();
            cboEstados.Enabled = false;
            btnExportarExcel.Enabled = false;

            lst.Limpiar();
            lst.LimpiarItems();

            lstTotales.Limpiar();
            lstTotales.LimpiarItems();

            cboJurisdicciones.Focus();
        }

        private void CargarDatos()
        {
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            string sSql = "", sStore = "";
            int iAnio = 0, iMes = 0;

            dtsMovtos = new DataSet();
            dtsTotales = new DataSet();
            
            if (rdoEntradas.Checked)
            {
                sStore = "spp_Rpt_CteReg_ProvExternos_Entradas";
            }
            if (rdoDispensacion.Checked)
            {
                sStore = "spp_Rpt_CteReg_ProvExternos_Dispensacion";
            }
            if (rdoDistribucion.Checked)
            {
                sStore = "spp_Rpt_CteReg_ProvExternos_Distribucion";
            }

            iAnio = dtpFechaInicial.Value.Year;
            iMes = dtpFechaInicial.Value.Month;

            sSql = string.Format(" Exec  {0} '{1}', {2}, {3}, '{4}' ", sStore, cboEstados.Data, 
                                 iAnio, iMes, cboJurisdicciones.Data);

            lst.Limpiar();
            lst.LimpiarItems();

            lstTotales.Limpiar();
            lstTotales.LimpiarItems();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDatos()");
                General.msjAviso("No fue posible obtener la información solicitada, intente de nuevo.");
            }
            else
            {
                if (leer.Leer())
                {
                    dtsMovtos.Tables.Add(leer.Tabla(1).Copy());
                    dtsTotales.Tables.Add(leer.Tabla(2).Copy());
                    //lst.CargarDatos(leer.DataSetClase, true, true);
                    lst.CargarDatos(dtsMovtos, true, true);
                    lstTotales.CargarDatos(dtsTotales, true, true);
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

        #region Funciones_Hilos
        private void ActivarControles()
        {
            //this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true;
            btnEjecutar.Enabled = true;
            btnExportarExcel.Enabled = bSeEncontroInformacion;
            FrameDatos.Enabled = false;
            FrameFechas.Enabled = true;
            FrameMovimientos.Enabled = true;

        }
        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                FrameDatos.Enabled = true;
                FrameFechas.Enabled = true;
                FrameMovimientos.Enabled = true;
                btnNuevo.Enabled = true;
                btnEjecutar.Enabled = true;
                btnExportarExcel.Enabled = true;

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

        private ListaPlantillas Plantilla_A_Generar()
        {
            int iValor = 0;

            if (rdoEntradas.Checked)
            {
                iValor = 1;
            }
            if (rdoDispensacion.Checked)
            {
                iValor = 2;
            }
            if (rdoDistribucion.Checked)
            {
               iValor = 3;
            }
            ListaPlantillas myPlantilla = ListaPlantillas.SurmientoClavesDispensada;            

            switch (iValor)
            {
                case 1:
                    myPlantilla = ListaPlantillas.EdoMovtos_Entradas;
                    break;

                case 2:
                    myPlantilla = ListaPlantillas.EdoMovtos_Salidas;
                    break;

                case 3:
                    myPlantilla = ListaPlantillas.EdoMovtos_Transferencias;
                    break;

            }

            return myPlantilla;
        }

        /*
        private void ExportaEntradas()
        {
            bool bGenerar = false;
            clsLeer leerToExcel = new clsLeer();
            clsLeer leerPte = new clsLeer();
            // int iColInicial = 0;
            // int iColActiva = 0;
            // int iNumDias = 0;
            string sTituloPeriodo = "";

            leerPte.DataSetClase = dtsMovtos;
            leerToExcel.DataTableClase = dtsMovtos.Tables[0].Copy();


            bGenerar = GnPlantillas.GenerarPlantilla(Plantilla_A_Generar(), "PLANTILLA_006");

            if (bGenerar)
            {
                xpExcel = new clsExportarExcelPlantilla(GnPlantillas.Documento);
                xpExcel.AgregarMarcaDeTiempo = true;

                sTituloPeriodo = General.FechaYMD(dtpFechaInicial.Value);
                ////if (!chkDiaEspecificado.Checked)
                {
                    sTituloPeriodo = General.FechaNombreMes(dtpFechaInicial.Value) + ' ' + dtpFechaInicial.Value.Year.ToString();
                }


                int iRow = 9;
                // int iRowInicial = 9;

                if (xpExcel.PrepararPlantilla())
                {
                    xpExcel.GeneraExcel();
                    leerToExcel.Leer();

                    xpExcel.Agregar("INTERCONTINENTAL DE MEDICAMENTOS", 2, 2);
                    xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneralPedidos.EstadoConectadoNombre, 3, 2);
                    //xpExcel.Agregar("Analísis de claves negadas de " + sTituloPeriodo, 4, 2);

                    //xpExcel.Agregar("Fecha de reporte : " + leerToExcel.CampoFecha("FechaReporte").ToString(), 6, 2);
                    xpExcel.Agregar("Fecha de reporte : " + General.FechaSistema.ToLongDateString(), 6, 2);

                    leerToExcel.RegistroActual = 1;
                    while (leerToExcel.Leer())
                    {
                        xpExcel.Agregar(leerToExcel.Campo("Núm. Jurisdicción"), iRow, 2);
                        xpExcel.Agregar(leerToExcel.Campo("Nombre Jurisdicción"), iRow, 3);
                        xpExcel.Agregar(leerToExcel.Campo("Núm. Unidad"), iRow, 4);
                        xpExcel.Agregar(leerToExcel.Campo("Nombre unidad"), iRow, 5);
                        xpExcel.Agregar(leerToExcel.Campo("Folio"), iRow, 6);
                        xpExcel.Agregar(leerToExcel.Campo("Fecha registro"), iRow, 7);
                        xpExcel.Agregar(leerToExcel.Campo("Id Proveedor"), iRow, 8);
                        xpExcel.Agregar(leerToExcel.Campo("Nombre proveedor"), iRow, 9);
                        xpExcel.Agregar(leerToExcel.Campo("Clave SSA"), iRow, 10);
                        xpExcel.Agregar(leerToExcel.Campo("Descripción clave"), iRow, 11);
                        xpExcel.Agregar(leerToExcel.Campo("Piezas"), iRow, 12);
                        

                        //iNumDias = iDiasPeriodo;
                        //iColActiva = 12;
                        //for (int i = 1; i <= iNumDias; i++)
                        //{
                        //    xpExcel.Agregar(leerToExcel.Campo(i.ToString()), iRow, iColActiva);
                        //    iColActiva++;
                        //}
                        //xpExcel.Agregar(leerToExcel.Campo("Total"), iRow, iColActiva);
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
        

        private void ExportaSalidas()
        {
            bool bGenerar = false;
            clsLeer leerToExcel = new clsLeer();
            clsLeer leerPte = new clsLeer();
            // int iColInicial = 0;
            // int iColActiva = 0;
            // int iNumDias = 0;
            string sTituloPeriodo = "";

            leerPte.DataSetClase = dtsMovtos;
            leerToExcel.DataTableClase = dtsMovtos.Tables[0].Copy();


            bGenerar = GnPlantillas.GenerarPlantilla(Plantilla_A_Generar(), "PLANTILLA_006");

            if (bGenerar)
            {
                xpExcel = new clsExportarExcelPlantilla(GnPlantillas.Documento);
                xpExcel.AgregarMarcaDeTiempo = true;

                sTituloPeriodo = General.FechaYMD(dtpFechaInicial.Value);
                ////if (!chkDiaEspecificado.Checked)
                {
                    sTituloPeriodo = General.FechaNombreMes(dtpFechaInicial.Value) + ' ' + dtpFechaInicial.Value.Year.ToString();
                }


                int iRow = 9;
                // int iRowInicial = 9;

                if (xpExcel.PrepararPlantilla())
                {
                    xpExcel.GeneraExcel();
                    leerToExcel.Leer();

                    xpExcel.Agregar("INTERCONTINENTAL DE MEDICAMENTOS", 2, 2);
                    xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneralPedidos.EstadoConectadoNombre, 3, 2);
                    //xpExcel.Agregar("Analísis de claves negadas de " + sTituloPeriodo, 4, 2);

                    //xpExcel.Agregar("Fecha de reporte : " + leerToExcel.CampoFecha("FechaReporte").ToString(), 6, 2);
                    xpExcel.Agregar("Fecha de reporte : " + General.FechaSistema.ToLongDateString(), 6, 2);

                    leerToExcel.RegistroActual = 1;
                    while (leerToExcel.Leer())
                    {
                        xpExcel.Agregar(leerToExcel.Campo("Núm. Jurisdicción"), iRow, 2);
                        xpExcel.Agregar(leerToExcel.Campo("Nombre Jurisdicción"), iRow, 3);
                        xpExcel.Agregar(leerToExcel.Campo("Núm. Unidad"), iRow, 4);
                        xpExcel.Agregar(leerToExcel.Campo("Nombre unidad"), iRow, 5);

                        //xpExcel.Agregar(leerToExcel.Campo("Folio"), iRow, 4);
                        xpExcel.Agregar(leerToExcel.Campo("Fecha registro"), iRow, 6);
                        xpExcel.Agregar(leerToExcel.Campo("Id Proveedor"), iRow, 7);
                        xpExcel.Agregar(leerToExcel.Campo("Nombre proveedor"), iRow, 8);
                        xpExcel.Agregar(leerToExcel.Campo("Clave SSA"), iRow, 9);
                        xpExcel.Agregar(leerToExcel.Campo("Descripción clave"), iRow, 10);
                        xpExcel.Agregar(leerToExcel.Campo("Piezas"), iRow, 11);

                        //iNumDias = iDiasPeriodo;
                        //iColActiva = 12;
                        //for (int i = 1; i <= iNumDias; i++)
                        //{
                        //    xpExcel.Agregar(leerToExcel.Campo(i.ToString()), iRow, iColActiva);
                        //    iColActiva++;
                        //}
                        //xpExcel.Agregar(leerToExcel.Campo("Total"), iRow, iColActiva);
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
        

        private void ExportaTransferencias()
        {
            bool bGenerar = false;
            clsLeer leerToExcel = new clsLeer();
            clsLeer leerPte = new clsLeer();
            // int iColInicial = 0;
            // int iColActiva = 0;
            // int iNumDias = 0;
            string sTituloPeriodo = "";

            leerPte.DataSetClase = dtsMovtos;
            leerToExcel.DataTableClase = dtsMovtos.Tables[0].Copy();


            bGenerar = GnPlantillas.GenerarPlantilla(Plantilla_A_Generar(), "PLANTILLA_006");

            if (bGenerar)
            {
                xpExcel = new clsExportarExcelPlantilla(GnPlantillas.Documento);
                xpExcel.AgregarMarcaDeTiempo = true;

                sTituloPeriodo = General.FechaYMD(dtpFechaInicial.Value);
                ////if (!chkDiaEspecificado.Checked)
                {
                    sTituloPeriodo = General.FechaNombreMes(dtpFechaInicial.Value) + ' ' + dtpFechaInicial.Value.Year.ToString();
                }


                int iRow = 9;
                // int iRowInicial = 9;

                if (xpExcel.PrepararPlantilla())
                {
                    xpExcel.GeneraExcel();
                    leerToExcel.Leer();

                    xpExcel.Agregar("INTERCONTINENTAL DE MEDICAMENTOS", 2, 2);
                    xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneralPedidos.EstadoConectadoNombre, 3, 2);
                    //xpExcel.Agregar("Analísis de claves negadas de " + sTituloPeriodo, 4, 2);

                    //xpExcel.Agregar("Fecha de reporte : " + leerToExcel.CampoFecha("FechaReporte").ToString(), 6, 2);
                    xpExcel.Agregar("Fecha de reporte : " + General.FechaSistema.ToLongDateString(), 6, 2);

                    leerToExcel.RegistroActual = 1;
                    while (leerToExcel.Leer())
                    {
                        xpExcel.Agregar(leerToExcel.Campo("Núm. Jurisdicción"), iRow, 2);
                        xpExcel.Agregar(leerToExcel.Campo("Nombre Jurisdicción"), iRow, 3);
                        xpExcel.Agregar(leerToExcel.Campo("Núm. Unidad"), iRow, 4);
                        xpExcel.Agregar(leerToExcel.Campo("Nombre unidad"), iRow, 5);
                        xpExcel.Agregar(leerToExcel.Campo("Folio"), iRow, 6);
                        xpExcel.Agregar(leerToExcel.Campo("Fecha registro"), iRow, 7);
                        xpExcel.Agregar(leerToExcel.Campo("Núm. Unidad recibe"), iRow, 8);
                        xpExcel.Agregar(leerToExcel.Campo("Nombre unidad recibe"), iRow, 9);
                        xpExcel.Agregar(leerToExcel.Campo("Id Proveedor"), iRow, 10);
                        xpExcel.Agregar(leerToExcel.Campo("Nombre proveedor"), iRow, 11);
                        xpExcel.Agregar(leerToExcel.Campo("Clave SSA"), iRow, 12);
                        xpExcel.Agregar(leerToExcel.Campo("Descripción clave"), iRow, 13);
                        xpExcel.Agregar(leerToExcel.Campo("Piezas"), iRow, 14);
                        

                        //iNumDias = iDiasPeriodo;
                        //iColActiva = 12;
                        //for (int i = 1; i <= iNumDias; i++)
                        //{
                        //    xpExcel.Agregar(leerToExcel.Campo(i.ToString()), iRow, iColActiva);
                        //    iColActiva++;
                        //}
                        //xpExcel.Agregar(leerToExcel.Campo("Total"), iRow, iColActiva);
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
        */
        #endregion Exportar_Excel
    }
}
