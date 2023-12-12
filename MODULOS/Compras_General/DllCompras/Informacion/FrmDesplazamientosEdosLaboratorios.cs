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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.wsFarmacia;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace DllCompras.Informacion
{
    public partial class FrmDesplazamientosEdosLaboratorios : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
        clsLeer myLeer;
        clsLeer leer;
        clsLeerWebExt leerWeb;

        wsCnnCliente conexionWeb;
        Thread _workerThread;
                
        clsDatosCliente DatosCliente;
        clsListView lst;

        string sRutaPlantilla = "";
        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        DataSet dtsEstados = new DataSet();
        DataSet dtsJurisdiccion = new DataSet();
        DataSet dtsFarmacias = new DataSet();
        clsConsultas Consultas;
        clsAyudas ayuda;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(General.FechaSistema, "-");

        string sUrl = "";
        string sHost = "";
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        public FrmDesplazamientosEdosLaboratorios()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, "");
            conexionWeb = new wsCnnCliente();

            CheckForIllegalCrossThreadCalls = false;
            leerWeb = new clsLeerWebExt(General.Url, DtGeneral.CfgIniOficinaCentral, DatosCliente);

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            myLeer = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnCompras.DatosApp, this.Name);

            Consultas = new clsConsultas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            
            Cargar_Empresas();
        }

        private void FrmDesplazamientosEdosLaboratorios_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Carga_Combos
        private void Cargar_Empresas()
        {
            string sSql = "";

            cboEmpresas.Add("0", "<< Seleccione >>");

            sSql = " Select IdEmpresa, Nombre, EsDeConsignacion From CatEmpresas (NoLock) Where Status = 'A' Order by IdEmpresa ";
            if (myLeer.Exec(sSql))
            {
                cboEmpresas.Clear();
                cboEmpresas.Add();
                cboEmpresas.Add(myLeer.DataSetClase, true, "IdEmpresa", "Nombre");
                cboEmpresas.SelectedIndex = 0;
            }
            else
            {
                Error.LogError(myLeer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
        }

        private void Cargar_Estados()
        {
            string sSql = "", sEmpresa = "";

            sEmpresa = cboEmpresas.Data;
            sEmpresa = Fg.PonCeros(sEmpresa, 3);
            cboEdo.Add("0", "<< Seleccione >>");

            sSql = string.Format("Select Distinct U.IdEstado, (U.IdEstado + ' - ' + U.Estado) as Estado, U.UrlFarmacia, C.Servidor " +
                                " From vw_Regionales_Urls U (Nolock) " +
                                " Inner Join CFGSC_ConfigurarConexiones C (Nolock) On ( U.IdEstado = C.IdEstado And U.IdFarmacia = C.IdFarmacia ) " +
                                " Where U.IdEmpresa = '{0}' Order By U.IdEstado ", sEmpresa);
            if (myLeer.Exec(sSql))
            {
                cboEdo.Clear();
                cboEdo.Add();
                cboEdo.Add(myLeer.DataSetClase, true, "IdEstado", "Estado");
                cboEdo.SelectedIndex = 0;
            }
            else
            {
                Error.LogError(myLeer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Estados.");
            }

        }
        #endregion Carga_Combos

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            
            FrameDatos.Enabled = true;            
            FrameFechas.Enabled = true;            
            btnExportarExcel.Enabled = false;           

            if (DtGeneral.Modulo_Compras_EnEjecucion != TipoModuloCompras.Central)
            {
                cboEmpresas.Data = DtGeneral.EmpresaConectada;
                cboEdo.Data = DtGeneral.EstadoConectado;

                if (!DtGeneral.EsAdministrador)
                {
                    cboEmpresas.Enabled = false;
                    cboEdo.Enabled = false;
                }
            }

        }

        private void ObtenerInformacion()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false;
            btnExportarExcel.Enabled = false;
            FrameDatos.Enabled = false;
            FrameFechas.Enabled = false;
            
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.CargarGrid);
            _workerThread.Name = "Cargando Información";
            _workerThread.Start();
        }

        private void CargarGrid()
        {
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;
            int iTipoReporte = 0;
            string sSql = "";
            string sLaboratorio = chkTodosLosLaboratorios.Checked ? "" : txtLaboratorio.Text.Trim();



            sLaboratorio = sLaboratorio.Replace(" ", "%");
            sSql = string.Format(" Exec spp_Rpt_ConsumoEstados_Claves_Laboratorios " + // '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'  ",
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @FechaInicio = '{2}', @FechaFin = '{3}', @TipoReporte = '{4}', @IdLaboratorio = '{5}'  ", 
                cboEmpresas.Data, cboEdo.Data, General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), iTipoReporte, sLaboratorio);
                  
            leerExportarExcel = new clsLeer();
            leer = new clsLeer();
            leer.Reset();
            leer.DataSetClase = GetInformacionUnidad_Directo(sSql);
            if (leer.SeEncontraronErrores())
            {
                Error.GrabarError(leer, sSql, "CargarGrid()", "");
            }
            else
            {
                bSeEncontroInformacion = true;
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información con los criterios especificados");
                    bSeEjecuto = true;
                }
                else
                {
                    leerExportarExcel.DataSetClase = leer.DataSetClase;
                    ActivarControles();
                    btnExportarExcel.Enabled = true;
                    General.msjAviso("La informacion se genero satisfactoriamente..");
                }
            }

            bEjecutando = false;
            this.Cursor = Cursors.Default;
        }

        private void HabilitarControles(bool bValor)
        {
            btnNuevo.Enabled = bValor;
            btnEjecutar.Enabled = bValor;
            
            FrameDatos.Enabled = bValor;            
            FrameFechas.Enabled = bValor;            
        }

        private void ActivarControles()
        {
            btnNuevo.Enabled = true;
            btnEjecutar.Enabled = true;

            FrameDatos.Enabled = true;
            FrameFechas.Enabled = true;
        }
        #endregion Funciones

        #region Conexiones
        private DataSet GetInformacionRegional(string Cadena)
        {
            DataSet dts = new DataSet();

            leer.Exec(Cadena);
            dts = leer.DataSetClase;

            return dts;
        }

        private DataSet GetInformacionUnidad_Directo(string Cadena)
        {
            DataSet dts = new DataSet();

            if (validarDatosDeConexion())
            {
                clsConexionSQL cnnRemota = new clsConexionSQL(DatosDeConexion);
                cnnRemota.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
                cnnRemota.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

                clsLeer leerDatos = new clsLeer(ref cnnRemota);
                leerDatos.Exec(Cadena);
                dts = leerDatos.DataSetClase;

                ////leerWeb.Exec(Cadena); 
                ////dts = leerWeb.DataSetClase;
            }

            return dts;
        }
        #endregion Conexiones

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                ObtenerInformacion();
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            GeneraReporte();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Validaciones
        private bool validarDatos()
        {
            bool bRegresa = true;
            
            if (cboEmpresas.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una empresa válida, verifique.");
                cboEmpresas.Focus();
            }

            if (bRegresa && cboEdo.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un estado válido, verifique.");
                cboEdo.Focus();
            }

            if (bRegresa && !chkTodosLosLaboratorios.Checked && txtLaboratorio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha Capturado el laboratorio, verifique.");
                txtLaboratorio.Focus();
            }

            if(bRegresa && chkTodosLosLaboratorios.Checked)
            {
                if(General.msjConfirmar("¿ Desea generar el reporte de todos los laboratorios ?, el proceso podría demorar tiempo considerable.") != DialogResult.Yes)
                {
                    bRegresa = false;  
                } 
            }

            return bRegresa;
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniOficinaCentral, DatosCliente);

                conexionWeb.Url = sUrl;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniOficinaCentral));

                //DatosDeConexion.Servidor = sHost;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
                ActivarControles();
            }

            return bRegresa;
        }
        #endregion Validaciones       

        #region Eventos_Combos
        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargar_Estados();            
        }

        private void cboEdo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEdo.SelectedIndex != 0)
            {
                sUrl = ((DataRow)cboEdo.ItemActual.Item)["UrlFarmacia"].ToString();
                sHost = ((DataRow)cboEdo.ItemActual.Item)["Servidor"].ToString();
                if (sHost.Contains(":"))
                {
                    string[] sServidor = sHost.Split(':');
                    sHost = sServidor[0];
                }                
            }
        }
        #endregion Eventos_Combos

        #region Evento_Hilos
        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                FrameDatos.Enabled = true;
                FrameFechas.Enabled = true;
                btnNuevo.Enabled = true;
                btnEjecutar.Enabled = true;
                btnExportarExcel.Enabled = leerExportarExcel.Registros > 0;
                            
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
        #endregion Evento_Hilos

        #region Reportes
        private void GeneraReporte()
        {
            int iRow = 2;
            int iCol = 2;
            string sNombreFile = "";
            string sPeriodo = "";
            string sRutaReportes = "";



            HabilitarControles(false);
            //sRutaReportes = GnCompras.RutaReportes;
            //DtGeneral.RutaReportes = sRutaReportes;

            //sNombreFile = "Desplazamientos_Laboratorios" + "_" + txtLaboratorio.Text + "_" + cboEdo.Data + ".xls";
            //sRutaPlantilla = Application.StartupPath + @"\Plantillas\COM_DesplazamientoEdo_Laboratorio.xls";
            //DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_DesplazamientoEdo_Laboratorio.xls", DatosCliente);

            //xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //xpExcel.AgregarMarcaDeTiempo = false;


            //if (xpExcel.PrepararPlantilla(sNombreFile))
            //{
            //    xpExcel.GeneraExcel();

            //    //Se pone el encabezado
            //    leerExportarExcel.RegistroActual = 1;
            //    leerExportarExcel.Leer();
            //    xpExcel.Agregar(((DataRow)cboEmpresas.ItemActual.Item)["Nombre"].ToString(), iRow, 3);
            //    iRow++;
            //    xpExcel.Agregar(((DataRow)cboEdo.ItemActual.Item)["Estado"].ToString(), iRow, 3);
            //    iRow++;

            //    sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
            //       General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            //    xpExcel.Agregar(sPeriodo, iRow, 3);

            //    iRow = 6;
            //    xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 4);

            //    // Se ponen los detalles
            //    leerExportarExcel.RegistroActual = 1;
            //    iRow = 9;

            //    while (leerExportarExcel.Leer())
            //    {
            //        iCol = 2;
            //        xpExcel.Agregar(leerExportarExcel.Campo("IdEstado"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("Estado"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("Jurisdicción"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("Id Farmacia"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("Beneficiario"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("IdLaboratorio"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("Laboratorio"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("Clave SSA"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("Descripción Clave SSA"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("Tipo de Dispensación"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("Presentación"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("Envase"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("Año"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("Mes"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("Piezas"), iRow, iCol++);
            //        xpExcel.Agregar(leerExportarExcel.Campo("Cajas"), iRow, iCol++);

            //        iRow++;
            //    }

            //    // Finalizar el Proceso 
            //    xpExcel.CerrarDocumento();

            //    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //    {
            //        xpExcel.AbrirDocumentoGenerado();
            //    }

            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 0;
            string sNombreHoja = "";
            string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

            leer.DataSetClase = leerExportarExcel.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRow + leer.Columnas.Length - 1;
            iColsEncabezado = iRow + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombre))
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 16, sNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);


                iRenglon = 8;
                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }
            HabilitarControles(true);
        }
        
        #endregion Reportes
    }
}
