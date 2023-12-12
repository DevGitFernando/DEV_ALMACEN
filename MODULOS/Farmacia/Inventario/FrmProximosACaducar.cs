using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace Farmacia.Inventario
{
    public partial class FrmProximosACaducar : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        clsConsultas query;
        clsAyudas ayuda;
        // DataSet dtsFarmacias;
        DataSet dtsProximosACaducar = new DataSet(); 

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();

        private bool bLimpiar = true;

        public FrmProximosACaducar()
        {
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            Grid = new clsGrid(ref grdExistencia, this);
            Grid.EstiloGrid(eModoGrid.SeleccionSimple);
            Grid.Limpiar(false);
            Grid.AjustarAnchoColumnasAutomatico = true; 

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

        }

        private void FrmExistenciaPorClaveSSA_Load(object sender, EventArgs e)
        {
            if (bLimpiar)
            {
                btnNuevo_Click(null, null);
            }
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            dtsProximosACaducar = new DataSet();  
            query.MostrarMsjSiLeerVacio = false;
            Fg.IniciaControles(this, true);
            //grdExistencia.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            Grid.Limpiar(false);

            btnExportarExcel.Enabled = false; 
            rdoConcentrado.Checked = true; 

            query.MostrarMsjSiLeerVacio = true;
        }
        #endregion Botones

        #region Grid 
        private void LlenarGrid()
        {
            string sSql = string.Format(" Select 'Id ClaveSSA' = IdClaveSSA_Sal, ClaveSSA, 'Descripción Clave' = DescripcionSal, Sum(Existencia) as Existencia " + 
                " From vw_ExistenciaPorCodigoEAN_Lotes (NoLock) " +
                " Where IdEstado = '{0}' and IdFarmacia = '{1}'  " +
                " And Convert( varchar(10), FechaCaducidad, 120 )  Between '{2}' And '{3}'  " +
                " Group by IdClaveSSA_Sal, ClaveSSA, DescripcionSal " +
                " Having Sum(Existencia) > 0 " +
                " Order by IdClaveSSA_Sal ", DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value.AddMonths(3), "-") ); //Se le agregan 3 meses a la Fecha Final

            Grid.Limpiar(false);
            dtsProximosACaducar = new DataSet();  
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de existencias.");
            }
            else
            {
                dtpFechaInicial.Enabled = false;
                dtpFechaFinal.Enabled = false;

                if (leer.Leer())
                {
                    Grid.LlenarGrid(leer.DataSetClase);
                    dtsProximosACaducar = leer.DataSetClase; 
                    btnExportarExcel.Enabled = true; 
                } 

                if (Grid.GetValue(1, 1).Trim() == "") //Si la primer columna del primer renglon esta vacio significa que no leyo.
                {
                    Grid.DeleteRow(1);
                    General.msjUser("No existe información para mostrar bajo los criterios seleccionados");
                }
            }
            lblTotal.Text = Grid.TotalizarColumna(4).ToString("##,###,###,##0");
        }

        private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            ////Codigo.ShowDialog();
            //string sClaveInterna = Grid.GetValue(e.Row + 1, 1);
            //Codigo = new FrmCaducarPorClaveSSA_EstadoFarmaciasCodigos();
            //Codigo.MostrarDetalle(cboEstados.Data, cboFarmacias.Data, sClaveInterna, dtpFechaInicial.Text, dtpFechaFinal.Text );
        }
        #endregion Grid        

        public void MostrarDetalle(string IdEstado, string ClaveInternaSal)
        {
            bLimpiar = false;
            btnNuevo.Enabled = false;
            btnEjecutar_Click(null, null);

            this.ShowDialog();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa = false; 
            if (Grid.Rows == 0)
            {
                General.msjUser("No ha información en pantalla para generar la impresión.");
            }
            else
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_CaducarSales_Farmacias";

                if (rdoDetallado.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_CaducarSales_Farmacias_Detallado";
                }

                myRpt.Add("@IdEmpresa", "");
                myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("@IdClaveSSA_Sal", "");
                myRpt.Add("@IdProducto", "");
                myRpt.Add("@CodigoEAN", "");
                myRpt.Add("@Mostrar", 2); // Solo se muestran las sales que han tenido movimiento
                myRpt.Add("@FechaInicial", General.FechaYMD(dtpFechaInicial.Value));
                myRpt.Add("@FechaFinal", General.FechaYMD(dtpFechaFinal.Value.AddMonths(3), "-")); //Se le agregan 3 meses a la Fecha Final.

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            LlenarGrid();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            // Grid.ExportarExcel(true); 
            FrmExportarExcel f = new FrmExportarExcel(dtsProximosACaducar);
            f.AbrirDocumento = true;
            f.Exportar(); 
        }
    }
}
