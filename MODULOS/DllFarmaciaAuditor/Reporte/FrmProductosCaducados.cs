using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
//using DllFarmaciaSoft.wsFarmacia;

namespace DllFarmaciaAuditor.Reporte
{
    public partial class FrmProductosCaducados : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        DllFarmaciaSoft.clsConsultas Consultas;
        DllFarmaciaSoft.clsAyudas Ayuda;
        clsLeer myLeer;
        clsLeer myLeerLotes;
        clsGrid myGrid;

        clsDatosCliente DatosCliente;
        DllFarmaciaAuditor.wsAuditorFarmacia.wsCnnCliente conexionWeb;
               
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        

        public FrmProductosCaducados()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref ConexionLocal);
            myLeerLotes = new clsLeer(ref ConexionLocal);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, General.DatosApp, this.Name, true);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, General.DatosApp, this.Name, true);

            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "FrmProductosCaducados()");
            conexionWeb = new DllFarmaciaAuditor.wsAuditorFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            grdProductosCad.EditModeReplace = true;
            myGrid = new clsGrid(ref grdProductosCad, this);
            myGrid.EstiloGrid(eModoGrid.SoloLectura);
            myGrid.AjustarAnchoColumnasAutomatico = true;
        }

        private void FrmProductosCaducados_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;
            rdoConcentrado.Checked = true;
            myGrid.Limpiar(false);

        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            LlenarGrid();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Impresion();
        }

        #endregion Botones

        #region Funciones

        private void LlenarGrid()
        {
            string sSql = string.Format(" Select IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Sum(Existencia) as Existencia " +
                " From vw_ExistenciaPorCodigoEAN_Lotes (NoLock) " +
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' And MesesParaCaducar <= 0  " +                
                " Group by IdClaveSSA_Sal, ClaveSSA, DescripcionSal " +
                " Having Sum(Existencia) > 0 " +
                " Order by IdClaveSSA_Sal ", DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada ); 

            myGrid.Limpiar(false);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "");
                General.msjError("Ocurrió un error al obtener la información de Productos Caducados.");
            }
            else
            {
                if (myLeer.Leer())
                {
                    myGrid.LlenarGrid(myLeer.DataSetClase);
                    btnImprimir.Enabled = true;
                    btnEjecutar.Enabled = false;
                }
                

                ////if (myGrid.GetValue(1, 1).Trim() == "") //Si la primer columna del primer renglon esta vacio significa que no leyo.
                ////{
                ////    myGrid.DeleteRow(1);
                ////    General.msjUser("No existe informacion para mostrar bajo los criterios seleccionados");
                ////}
            }
            lblTotal.Text = myGrid.TotalizarColumna(4).ToString();
        }

        private void Impresion()
        {
            bool bRegresa = false;
            if (myGrid.Rows == 0)
            {
                General.msjUser("No hay información en pantalla para generar la impresión.");
            }
            else
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                byte[] btReporte = null;

                myRpt.RutaReporte = GnAuditorFarmacia.RutaReportes;

                if (rdoConcentrado.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_SalesCaducadas_Farmacias";
                }

                if (rdoDetallado.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_SalesCaducadas_Farmacias_Detallado";
                }

                myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);  
                              

                if (General.ImpresionViaWeb)
                {
                    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                    DataSet datosC = DatosCliente.DatosCliente();

                    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                }
                else
                {
                    myRpt.CargarReporte(true);
                    bRegresa = !myRpt.ErrorAlGenerar;
                }

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }


        #endregion Funciones
    }
}
