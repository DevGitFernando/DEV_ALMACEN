using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft; 


namespace Farmacia.PedidosDeDistribuidor
{
    public partial class FrmListaFoliosCargaMasivaRemisiones : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer; 
        clsDatosCliente DatosCliente;
        clsListView lst; 

        string sIdDistribuidor = "";

        public FrmListaFoliosCargaMasivaRemisiones(string IdDistribuidor)
        {
            InitializeComponent(); 
            this.sIdDistribuidor = IdDistribuidor; 


            leer = new clsLeer(ref cnn);
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");

            lst = new clsListView(listVwFolios);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;
        }

        private void FrmListaFoliosCargaMasivaRemisiones_Load(object sender, EventArgs e)
        {
            CargarFolios(); 
        } 

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            CargarFolios(); 
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string sFolio = lst.GetValue(1);
            if (sFolio != "") 
            {
                ImprimirFoliosRemisionesCargaMasiva(sFolio); 
            }
        }

        private void CargarFolios()
        {
            lst.Limpiar();
            string sSql = string.Format("Select Distinct 'Folio' = FolioCargaMasiva, 'Fecha registro' = convert(varchar(10), FechaRegistro, 120) " + 
                "From RemisionesDistEnc (NoLock) " +
                "Where FolioCargaMasiva <> 0 " + 
                "  and IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdDistribuidor = '{3}' " + 
                " Order By FolioCargaMasiva ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sIdDistribuidor) ;

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarFolios()"); 
            }
            else
            {
                lst.CargarDatos(leer.DataSetClase, true, true); 
            } 
        } 
        #endregion Botones

        #region Impresion 
        private void ImprimirFoliosRemisionesCargaMasiva(string Folio)
        {
            bool bRegresa = false;

            DatosCliente.Funcion = "ImprimirFoliosRemisionesCargaMasiva()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "PtoVta_FolioRemisionesCargaMasiva.rpt";

            myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("IdDistribuidor", sIdDistribuidor);
            myRpt.Add("FolioCargaMasiva", Folio);

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Impresion
    
    }
}
