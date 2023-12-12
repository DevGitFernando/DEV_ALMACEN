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

using ClosedXML.Excel;

namespace Facturacion.Informacion
{
    public partial class FrmInformacion_SabanaDispensacion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leer2, leerExcel, leerExcelFact;
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;
        //clsListView lst;
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

        public FrmInformacion_SabanaDispensacion()
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

            CargarTiposDeUnidades();

        }

        private void CargarTiposDeUnidades()
        {
            cboTiposDeUnidades.Clear();
            cboTiposDeUnidades.Add("0", "Todas las unidades");
            cboTiposDeUnidades.Add("1", "Farmacias");
            cboTiposDeUnidades.Add("2", "Farmacias de dosis unitaria");
            cboTiposDeUnidades.Add("3", "Almacenes");
            cboTiposDeUnidades.Add("4", "Almacenes de dosis unitaria");

            cboTiposDeUnidades.SelectedIndex = 0;
        }
        private void FrmInformacion_SabanaDispensacion_Load(object sender, EventArgs e)
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

            if (validarDatos())
            {
                ObtenerInformacion();
            }
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            return bRegresa; 
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
            ExportarExcel();
        }

        private void FrameFechaDeProceso_Enter(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            IniciaToolBar(true, false);
        }

        private void ObtenerInformacion()
        {
            clsLeer leerResultado = new clsLeer();

            string sSql = "", sWhereFecha = "";
            bool bRegresa = true; 
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            IniciaToolBar(false, false);


            sSql = string.Format("Exec spp_FACT_Rpt_ReporteDeOperacion_00_PreSabanaValidacion \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', \n" + 
                "\t@IdCliente = '{3}', @IdSubCliente = '{4}', \n" + 
                "\t@TipoDeUnidad = '{5}', @IdFarmacia = '{6}', \n" + 
                "\t@FechaInicial = '{7}', @FechaFinal = '{8}' \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                txtCte.Text, txtSubCte.Text, 
                cboTiposDeUnidades.Data, "", 
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value) 
                );


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

                leerExportarExcel.RenombrarTabla(1, "Resultados");
                leerResultado.DataTableClase = leerExportarExcel.Tabla(1);
                while (leerResultado.Leer())
                {
                    leerExportarExcel.RenombrarTabla(leerResultado.CampoInt("Orden"), leerResultado.Campo("NombreTabla"));
                }
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

        #region Cliente -- Sub-Cliente
        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            lblCliente.Text = "";
            txtSubCte.Text = "";
        }

        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Clientes(txtCte.Text, "txtCliente_Validating");
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
                else
                {
                    General.msjUser("Clave de Cliente no encontrada.");
                    txtCte.Text = "";
                    lblCliente.Text = "";
                    txtCte.Focus();
                }
            }
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                leer.DataSetClase = Ayudas.Clientes("txtCte_KeyDown");
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
            }
        }

        private void CargarDatosCliente()
        {
            //txtCte.Enabled = false;
            txtCte.Text = leer.Campo("IdCliente");
            lblCliente.Text = leer.Campo("NombreCliente");
            //lblCte.Text = leer.Campo("Nombre");
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCliente.Text = "";
        }

        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCte.Text != "")
            {
                leer.DataSetClase = Consultas.SubClientes(txtCte.Text, txtSubCte.Text, "txtCte_Validating");
                if (leer.Leer())
                {
                    CargarDatosSubCliente();
                }
                else
                {
                    General.msjUser("Clave de Sub-Cliente no encontrada");
                    txtSubCte.Text = "";
                    lblSubCliente.Text = "";
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
                    leer.DataSetClase = Ayudas.SubClientes("txtSubCte_KeyDown", txtCte.Text);
                    if (leer.Leer())
                    {
                        CargarDatosSubCliente();
                    }
                }
            }
        }

        private void CargarDatosSubCliente()
        {
            //txtSubCte.Enabled = false;
            txtSubCte.Text = leer.Campo("IdSubCliente");
            lblSubCliente.Text = leer.Campo("NombreSubCliente");
        }
        #endregion Cliente -- Sub-Cliente

        #region Exportar Excel 
        private void ExportarExcel()
        {

            bEjecutando = true;
            bEjecutando = false;
            string sAño = "", sNombre = "", sNombreHoja = "";
            int iRow = 2, iColBase = 2, iColsEncabezado = 0, iRenglon = 0;
            int iHojas_Totales = 0;
            int iHojas_Agregadas = 0;
            string sFileName = "";

            Cursor.Current = Cursors.Default;
            //IniciarToolBar(true, true, true);

            clsGenerarExcel generarExcel = new clsGenerarExcel();
            leer.RegistroActual = 1;

            clsLeer exportarExcel = new clsLeer();
            clsLeer dtsLocal = new clsLeer();


            sNombre = "SABANA DE DISPENSACIÓN";
            sNombreHoja = sNombre;

            if (sNombre.Trim().Length >= 10)
            {
                sNombreHoja = sNombre.Substring(0, 10);
            }


            iColsEncabezado = iRow + leer.Columnas.Length - 1;
            iColsEncabezado = iRow + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = sNombre;
            generarExcel.AgregarMarcaDeTiempo = true;
            GC.Collect();

            if (generarExcel.PrepararPlantilla(sNombre))
            {
                exportarExcel.DataTableClase = leerExportarExcel.Tabla("Resultados");
                iHojas_Totales = exportarExcel.Registros;

                while (exportarExcel.Leer())
                {
                    if (iHojas_Agregadas > 0)
                    {
                        //generarExcel.CerraArchivo();
                        //generarExcel.AbrirArchivo(generarExcel.RutaArchivo, generarExcel.NombreArchivo);
                    }

                    sNombreHoja = exportarExcel.Campo("NombreTabla");
                    dtsLocal.DataTableClase = leerExportarExcel.Tabla(sNombreHoja);

                    sFileName = string.Format(@"{0}_{1}{2}", generarExcel.NombreDocumento, sNombreHoja, generarExcel.Extension);
                    generarExcel.CrearArchivo(generarExcel.RutaArchivo, sFileName);

                    generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                    generarExcel.Theme = XLTableTheme.None;

                    ////generarExcel.ArchivoExcelOpenXml.Workbook.Worksheets.Add(sNombreHoja);


                    //generarExcel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                    //generarExcel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, string.Format(cboReporte.SelectedItem.ToString() + " " + sNombreHoja));
                    //generarExcel.EscribirCeldaEncabezado(sNombreHoja, 5, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);

                    iRenglon = 1;
                    //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                    generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, dtsLocal.DataSetClase);

                    //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                    generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                    //GC.AddMemoryPressure(100000);
                    GC.Collect();

                    //generarExcel.CerraArchivo();
                    iHojas_Agregadas++;

                    generarExcel.GuardarDocumento(true);
                }

                //generarExcel.CerraArchivo();
                if (iHojas_Agregadas > 0)
                {
                    //generarExcel.CerraArchivo_Stream(); 
                    //generarExcel.CerrarArchivo();
                    generarExcel.AbrirDirectorioDestino(true);
                }
            }

            //BloquearControles(false);

            //MostrarEnProceso(false);

            Application.DoEvents();
        }
        #endregion Exportar Excel 

    }
}
