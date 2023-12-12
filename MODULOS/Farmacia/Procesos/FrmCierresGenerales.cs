using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using DllFarmaciaSoft;

namespace Farmacia.Procesos
{
    public partial class FrmCierresGenerales : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsGrid grid;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;

        // Manejo de reportes  
        clsDatosCliente DatosCliente;

        public FrmCierresGenerales()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");            

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            grid = new clsGrid(ref grdFolios, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            grid.AjustarAnchoColumnasAutomatico = true; 
        }

        private void FrmCierresGenerales_Load(object sender, EventArgs e)
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
            BuscarCierres();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);

            grid.Limpiar(true);
            rdoIndividual.Checked = true;
            dtpFechaInicio.Focus();
        }

        private void BuscarCierres()
        {
            string sSql = "";

            sSql = string.Format(" Select FolioCierre, convert(varchar(10), FechaRegistro, 120) as FechaRegistro  From Ctl_CierresGeneral (Nolock) " +
	                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
	                            " and convert(varchar(10), FechaRegistro, 120) Between '{3}' and '{4}' ", sEmpresa, sEstado, sFarmacia,
                                General.FechaYMD(dtpFechaInicio.Value, "-"), General.FechaYMD(dtpFechaFin.Value, "-") );

            grid.Limpiar(true);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "BuscarCierres()");
                General.msjError("Ocurrió un error al buscar los cierres generales..");
            }
            else
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase, false, false);
                }
                else
                {
                    General.msjAviso("No se encontro información con los criterios especificados...");
                }
            }
        }
        #endregion Funciones

        #region Impresion
        private void Imprimir()
        {
            string sFolioCierre = "";
            bool bRegresa = false;
            
            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            
            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_CierresGeneralesMovtos";
                    

            if (rdoIndividual.Checked)
            {
                sFolioCierre = grid.GetValueInt(grid.ActiveRow, 1).ToString();
            }                

            myRpt.Add("@IdEmpresa", sEmpresa);
            myRpt.Add("@IdEstado", sEstado);
            myRpt.Add("@IdFarmacia", sFarmacia);
            myRpt.Add("@FolioCierre", sFolioCierre);
            myRpt.Add("@FechaInicio", General.FechaYMD(dtpFechaInicio.Value, "-"));
            myRpt.Add("@FechaFin", General.FechaYMD(dtpFechaFin.Value, "-"));            

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
                                
            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }           
        }
        #endregion Impresion

        #region Eventos_Grid
        private void grdFolios_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (rdoIndividual.Checked)
            {
                btnImprimir_Click(null, null);
            }
        }
        #endregion Eventos_Grid
    }
}
