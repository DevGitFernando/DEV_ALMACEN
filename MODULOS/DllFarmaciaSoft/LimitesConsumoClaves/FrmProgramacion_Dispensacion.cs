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

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FTP;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.wsOficinaCentral;

namespace DllFarmaciaSoft.LimitesConsumoClaves
{
    public partial class FrmProgramacion_Dispensacion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeerWebExt leerWeb;
        clsLeer leer;
        clsGrid grid;
        clsDatosCliente datosCliente;
        clsConsultas query;
        DataTable dtTablas;

        string sIdPublicoGral = GnFarmacia.PublicoGral;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        DllFarmaciaSoft.clsAyudas Ayuda;  
        DataSet dtsCargarDatos = new DataSet();
        DllFarmaciaSoft.clsConsultas Consultas;

        //clsExportarExcelPlantilla xpExcel;
        clsDatosCliente DatosCliente; 

        string sURL = "";
        Thread thDescargar; 

        public FrmProgramacion_Dispensacion()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn); 

            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, false);
            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
            
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, ""); 


            grid = new clsGrid(ref grdProgramacion, this);
            grid.AjustarAnchoColumnasAutomatico = true;
            grid.EstiloDeGrid = eModoGrid.ModoRow; 
        }

        private void FrmProgramacion_Dispensacion_Load(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        #region Botones 
        private void InicializarPantalla()
        {
            btnEjecutar.Enabled = true;
            btnExportarExcel.Enabled = false;
            Fg.IniciaControles();
            grid.Limpiar(false);

            txtCte.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (validaDatos())
            {
                ObtenerInformacion(); 
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            Generar_Excel(); 
        }
        #endregion Botones

        #region Validaciones
        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            lblCte.Text = "";
            txtSubCte.Text = "";
            lblSubCte.Text = "";
        }

        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() == "")
            {
                txtCte.Text = "";
                lblCte.Text = "";
                txtSubCte.Text = "";
                lblSubCte.Text = "";
                lblSubCte.Text = "";
            }
            else
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, "txtCte_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Cliente no encontrada, ó el Cliente no pertenece a la Farmacia.");
                    e.Cancel = true;
                }
                else
                {
                    CargarInformacion_Cliente(); 
                }
            }
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, "txtCte_KeyDown");
                if (leer.Leer())
                {
                    CargarInformacion_Cliente();
                }
            }
        }

        private void CargarInformacion_Cliente()
        {
            txtCte.Enabled = false;
            txtCte.Text = leer.Campo("IdCliente");
            lblCte.Text = leer.Campo("NombreCliente");
            txtSubCte.Text = "";
            lblSubCte.Text = "";
            lblSubCte.Text = "";
            txtSubCte.Focus();
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCte.Text = "";
        }

        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCte.Text.Trim() == "")
            {
                txtSubCte.Text = "";
                lblSubCte.Text = "";
                lblSubCte.Text = "";
            }
            else
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, "txtCte_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Sub-Cliente no encontrada, ó el Sub-Cliente no pertenece a la Farmacia.");
                    e.Cancel = true;
                }
                else
                {
                    CargarInformacion_SubCliente(); 
                }
            }

        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "")
                {
                    leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, "txtSubCte_KeyDown_1");
                    if (leer.Leer())
                    {
                        CargarInformacion_SubCliente();
                    }
                }
            }
        }

        private void CargarInformacion_SubCliente()
        {
            txtSubCte.Enabled = false;
            txtSubCte.Text = leer.Campo("IdSubCliente");
            lblSubCte.Text = leer.Campo("NombreSubCliente"); 
        }
        #endregion Validaciones

        #region Informacion 
        private bool validaDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtCte.Text == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado una Clave válida de Cliente, verifique.");
                txtCte.Focus(); 
            }

            if (bRegresa && txtSubCte.Text == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado una Clave válida de Sub-Cliente, verifique.");
                txtSubCte.Focus();
            }

            
            return bRegresa; 
        }

        private void ObtenerInformacion()
        {
            bool bActivar = false; 
            string sSql = string.Format("Exec spp_RPT_CB_Programacion_Dispensacion  " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdCliente = '{3}', @IdSubCliente = '{4}', @Año = '{5}', @Mes = '{6}' ", 
                sEmpresa, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, nmAño.Value, nmMes.Value);



            grid.Limpiar(false);          
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerInformacion");
                General.msjError("Ocurrió un error al obtener la información solicitada.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontró información con los criterios especificados, verifique."); 
                }
                else
                {
                    grid.LlenarGrid(leer.DataSetClase, false, false);
                    bActivar = true;
                }
            }

            btnExportarExcel.Enabled = bActivar;
            btnEjecutar.Enabled = !bActivar; 
        }
        #endregion Informacion

        #region Exportar Excel 
        private void Generar_Excel()
        {
            // int iRenglon = 8;
            string sNombreHoja = "Hoja1";
            string sConcepto = "REPORTE DE PROGRACIÓN DE CONSUMOS DEL AÑO " + nmAño.Value + " DEL MES " + nmMes.Value;
            bool bRegresa = false;
            int iRow = 2;
            int iCol = 2;
            int iColsEncabezado = 7;
            //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_ProgramacionConsumo.xlsx";

            this.Cursor = Cursors.WaitCursor;
            DllFarmaciaSoft.ExportarExcel.clsGenerarExcel excel;
            //bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_ProgramacionConsumo.xlsx", DatosCliente);

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            //{
                excel = new DllFarmaciaSoft.ExportarExcel.clsGenerarExcel();
                excel.RutaArchivo = @"C:\\Excel";
                //excel.NombreArchivo = sNombreDocumento;
                excel.AgregarMarcaDeTiempo = true;

            if (excel.PrepararPlantilla())
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow++, iCol, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow++, iCol, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow++, iCol, iColsEncabezado, 14, sConcepto);
                iRow++;
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow, iCol, iColsEncabezado, 11, string.Format("Fecha generación: {0} ", General.FechaSistemaObtener()), ClosedXML.Excel.XLAlignmentHorizontalValues.Left);

                iRow += 2;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                excel.InsertarTabla(sNombreHoja, iRow, iCol, leer.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }

                //xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                //xpExcel.AgregarMarcaDeTiempo = true;
                //xpExcel.FormInvoca = this;

                //if (xpExcel.PrepararPlantilla())
                //{
                //    this.Cursor = Cursors.WaitCursor;

                //    xpExcel.GeneraExcel(true);

                //    leer.RegistroActual = 1;
                //    leer.Leer();
                //    xpExcel.NumeroDeRenglonesAProcesar = leer.Registros > 0 ? leer.Registros : -1;

                //    xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
                //    xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
                //    xpExcel.Agregar(leer.Campo("TituloReporte"), 4, 2);
                //    xpExcel.Agregar(leer.CampoFecha("FechaEmisionReporte").ToString(), 6, 3);

                //    leer.RegistroActual = 1;
                //    while (leer.Leer())
                //    {
                //        iCol = 2;
                //        xpExcel.Agregar(leer.Campo("ClaveSSA"), iRow, iCol++);
                //        xpExcel.Agregar(leer.Campo("DescripcionClave"), iRow, iCol++);


                //        xpExcel.Agregar(leer.Campo("Programacion"), iRow, iCol++);
                //        xpExcel.Agregar(leer.Campo("Ampliacion"), iRow, iCol++);
                //        xpExcel.Agregar(leer.Campo("Dispensacion"), iRow, iCol++);
                //        xpExcel.Agregar(leer.Campo("PorcentajeDispensado"), iRow, iCol++);
                //        iRow++;

                //        xpExcel.NumeroRenglonesProcesados++;
                //    }

                //    // Finalizar el Proceso 
                //    xpExcel.CerrarDocumento();

                //    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                //    {
                //        xpExcel.AbrirDocumentoGenerado();
                //    }
                //}
            //}

            this.Cursor = Cursors.Default;
        }
        #endregion Exportar Excel 
    }
}
