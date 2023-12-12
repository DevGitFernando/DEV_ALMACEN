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
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;

namespace Facturacion.DocumentosDeComprobacion
{
    public partial class FrmRPT_DoctosDocumentos_A_Comprobacion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leer2, leerExcel, leerExcelFact;
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;
        clsListView lst;
        clsExportarExcelPlantilla xpExcel;

        DataSet dtsExportarExcel = new DataSet();
        clsLeer leerExportarExcel = new clsLeer();

        Thread _workerThread;

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;

        clsAuditoria auditoria;

        private enum Cols
        {
            Ninguna = 0,
            Folio = 2, Factura = 3, Fecha = 4, TipoFactura = 5, Importe = 6, Status = 7, Insumo = 8
        }

        public FrmRPT_DoctosDocumentos_A_Comprobacion()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();


            CheckForIllegalCrossThreadCalls = false;

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.Puerto = General.DatosConexion.Puerto;
            cnn.DatosConexion.ForzarImplementarPuerto = General.DatosConexion.ForzarImplementarPuerto;
            cnn.DatosConexion.ConexionDeConfianza = General.DatosConexion.ConexionDeConfianza;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;


            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            leerExcel = new clsLeer(ref cnn);
            leerExcelFact = new clsLeer(ref cnn);

            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);
            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            lst = new clsListView(lstFacturas);
            lst.OrdenarColumnas = false;

            CargarTiposDeUnidades();

        }

        private void CargarTiposDeUnidades()
        {
            ////cboTiposDeUnidades.Clear();
            ////cboTiposDeUnidades.Add("0", "Todas las unidades");
            ////cboTiposDeUnidades.Add("1", "Farmacias");
            ////cboTiposDeUnidades.Add("2", "Farmacias de dosis unitaria");
            ////cboTiposDeUnidades.Add("3", "Almacenes");
            ////cboTiposDeUnidades.Add("4", "Almacenes de dosis unitaria");

            ////cboTiposDeUnidades.SelectedIndex = 0;
        }
        private void FrmRPT_DoctosDocumentos_A_Comprobacion_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            //Cargar_Folios_Facturas();
            //IniciarProcesamiento();

            ObtenerInformacion(); 
        }

        private void IniciarProcesamiento()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            IniciaToolBar(false, false);

            bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "ObteniendoDatos";
            _workerThread.Start();

            btnNuevo.Enabled = true;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            Generar_Excel();
        }

        private void FrameFechaDeProceso_Enter(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);

            rdo_01_PendienteDeComprobar.Checked = true;

            IniciaToolBar(true, false);
            lst.Limpiar();

        }

        private void ObtenerInformacion()
        {
            string sSql = "", sWhereFecha = "";
            bool bRegresa = true;
            int iValor = 0; 

            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            IniciaToolBar(false, false);


            if (rdo_01_PendienteDeComprobar.Checked)
            {
                iValor = 1; 
            }

            if (rdo_02_ComprobacionCompleta.Checked)
            {
                iValor = 2;
            }


            sSql = string.Format("Exec spp_FACT_INFO_Comprobacion_Documentos_Detalles \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', \n" +
                "\t@Status_Distribucion = '{3}' \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                iValor
                );

            lst.LimpiarItems();
            if (!leer.Exec(sSql))
            {
                bRegresa = false; 
                bSeEncontroInformacion = false;
                Error.GrabarError(leer, "ObtenerInformacion()");
                General.msjError("Ocurrió un error al obtener la información.");
            }
            else
            {
                leerExportarExcel.DataSetClase = leer.DataSetClase;
                lst.CargarDatos(leer.DataSetClase, true, true); 
            }


            bEjecutando = false;

            IniciaToolBar(true, bRegresa);
            this.Cursor = Cursors.Default;
        }
        private void IniciaToolBar(bool Ejecutar, bool Exportar)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnExportar.Enabled = Exportar;
        }
        #endregion Funciones

        #region Exportar Excel 
        private void Generar_Excel()
        {
            clsGenerarExcel excel = new clsGenerarExcel();
            string sNombreDocumento = "COMPROBACION_DE_DOCUMENTOS";
            string sNombreHoja = "EXISTENCIAS";
            string sConcepto = "REPORTE DE EXISTENCIAS";

            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;

            clsLeer exportarExcel = new clsLeer();
            clsLeer dtsLocal = new clsLeer();

            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
            //int iHoja = 1;
            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            excel = new clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;

            if (excel.PrepararPlantilla(sNombreDocumento))
            {

                sNombreHoja = "Comprobacion_Doctos"; 
                //dtsLocal.DataTableClase = leerExportarExcel.Tabla(sNombreHoja);

                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, "REPORTE DE COMPROBACIÓN DE DOCUMENTOS");
                excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 14, sNombreHoja);
                excel.EscribirCeldaEncabezado(sNombreHoja, 5, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                iRenglon = 8;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leerExportarExcel.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();


                excel.AbrirDocumentoGenerado(true);
            }
        }
        #endregion Exportar Excel 
    }
}
