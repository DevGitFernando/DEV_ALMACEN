using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
//using SC_SolutionsSystem.FuncionesGrid;
//using SC_SolutionsSystem.Reportes;
//using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;

namespace MA_Facturacion.CuentasPorPagar.Cheques
{
    public partial class FrmListadoDeCheques : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsListView lst;
        clsLeer MyLeer, leerExcel;
        clsExportarExcelPlantilla xpExcel;
        clsDatosCliente DatosCliente;

        private enum Cols
        {
            Folio = 1, Cheque = 2, Chequera = 3, Banco = 4, Beneficiario = 5, Foliocheque = 6, Cantidad = 7, FechaRegistro = 8, Status = 9

        }

        public FrmListadoDeCheques()
        {
            InitializeComponent();

            lst = new clsListView(lstCheques);
            MyLeer = new clsLeer(ref cnn);
            leerExcel = new clsLeer(ref cnn);
            Error = new  clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");
        }
        private void FrmListadoDeCheques_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            IniciaToolBar(true, false);
            lst.Limpiar();
            rdoTodos.Checked = true;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            lst.Limpiar();
            string sfechas = "", sStatus = "";


            if (!chkTodasFechas.Checked)
            {
                sfechas = string.Format(" And Convert(Varchar(10), Fecharegistro, 120) between '{0}' And '{1}'", dtpFechaInicio.Text, dtpFechaFin.Text);
            }

            if (rdoActivos.Checked == true)
            {
                sStatus = "And Status = 'A'";
            }

            if (rdoCancelado.Checked == true)
            {
                sStatus = "And Status = 'C'";
            }


            string sSql = string.Format("Select IdCheque As Folio, Cheque, Chequera, Banco, Beneficiario, FolioCheque As 'Folio cheque', Cantidad, " +
                                        "Convert(Varchar(10), FechaRegistro, 120) As FechaRegistro, StatusNombre As Status " +
                                        "From vw_Cheque Where IdEmpresa = '{0}' And IdEstado = '{1}' {2} {3}",
                                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sfechas, sStatus);

            if (MyLeer.Exec(sSql))
            {
                if (MyLeer.Leer())
                {
                    lst.CargarDatos(MyLeer.DataSetClase, true, true);
                    lst.AnchoColumna(4, 210);
                    lst.AnchoColumna(5, 210);
                    lst.AnchoColumna(7, 90);
                    IniciaToolBar(true, true);
                }
                else
                {
                    IniciaToolBar(true, false);
                    General.msjAviso("No se encontro información con los criterios especificados..");
                }
                lblTotal.Text = lst.TotalizarColumnaDouble(7).ToString();
            }
            else
            {
                Error.GrabarError(MyLeer, "btnEjecutar_Click()");
                General.msjError("A ocurrido un error al obtener el listado de cheques.");
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            GenerarExcel();
        }

        private void IniciaToolBar(bool Ejecutar, bool Excel)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnExportar.Enabled = Excel;
        }
        #endregion Botones

        #region Funciones y Procedimientos

        private void GenerarExcel()
        {

            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\CNT_LISTADO_DE_CHEQUES.xls";
            bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "CNT_LISTADO_DE_CHEQUES.xls", DatosCliente);
            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, false, "CNT_LISTADO_DE_CHEQUES.xls", DatosCliente);

            if (bRegresa)
            {
                xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                xpExcel.AgregarMarcaDeTiempo = true;
                //leer.DataSetClase = dtsExistencias;

                this.Cursor = Cursors.Default;
                if (xpExcel.PrepararPlantilla())
                {
                    IniciaToolBar(false, false);
                    this.Cursor = Cursors.WaitCursor;

                    ExportarFolios();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }

                    IniciaToolBar(true, true);
                }
                this.Cursor = Cursors.Default;
            }
        }
        private void ExportarFolios()
        {
            string sFechaImpresion = General.FechaSistemaFecha.ToString();
            int iHoja = 1;

            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 1);
            xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, 3, 1);
            //xpExcel.Agregar(sConcepto, 4, 2);


            //xpExcel.Agregar(sFechaImpresion, 6, 3);

            MyLeer.RegistroActual = 1;

            for (int iRenglon = 7; MyLeer.Leer(); iRenglon++)
            {
                xpExcel.Agregar(MyLeer.Campo("Folio"), iRenglon, (int)Cols.Folio);
                xpExcel.Agregar(MyLeer.Campo("Cheque"), iRenglon, (int)Cols.Cheque);
                xpExcel.Agregar(MyLeer.Campo("Chequera"), iRenglon, (int)Cols.Chequera);
                xpExcel.Agregar(MyLeer.Campo("Banco"), iRenglon, (int)Cols.Banco);
                xpExcel.Agregar(MyLeer.Campo("Beneficiario"), iRenglon, (int)Cols.Beneficiario);
                xpExcel.Agregar(MyLeer.Campo("Folio cheque"), iRenglon, (int)Cols.Foliocheque);
                xpExcel.Agregar(MyLeer.CampoDouble("Cantidad"), iRenglon, (int)Cols.Cantidad);
                xpExcel.Agregar(MyLeer.CampoFecha("FechaRegistro"), iRenglon, (int)Cols.FechaRegistro);
                xpExcel.Agregar(MyLeer.Campo("Status"), iRenglon, (int)Cols.Status);
            }
            xpExcel.CerrarDocumento();

        }

        #endregion Funciones y Procedimientos
    }
}
