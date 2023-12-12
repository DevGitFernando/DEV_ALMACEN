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

namespace Almacen.Ubicaciones
{
    public partial class FrmRptClavesSinUbicaciones : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayudas;
        clsGrid grid; 

        clsDatosCliente DatosCliente;
        wsAlmacen.wsCnnCliente conexionWeb;

        string sEmpresa = DtGeneral.EmpresaConectada;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion); // iRow = 0;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        

        public FrmRptClavesSinUbicaciones()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Almacen.wsAlmacen.wsCnnCliente();
            conexionWeb.Url = General.Url;

            leer = new clsLeer(ref cnn);

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);

            grid = new clsGrid(ref grdClavesSinUbicaciones, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            grid.AjustarAnchoColumnasAutomatico = true; 
        }

        private void FrmRptClavesSinUbicaciones_Load(object sender, EventArgs e)
        {
            LimpiaPantalla(); 
        }

        #region Botones
        private void LimpiaPantalla()
        {
            grid.Limpiar(false);
            btnImprimir.Enabled = false; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format(" Select IdClaveSSA_Sal, ClaveSSA, DescripcionClave, " +
                " IdProducto, DescripcionProducto, CodigoEAN, ClaveLote, FechaCaducidad " + 
                " From vw_Claves_Lotes_SinUbicacionesAsignadas P (NoLock) " +
                " Where P.IdEmpresa = '{0}' and P.IdEstado = '{1}' and P.IdFarmacia = '{2}' " +
                " Order by DescripcionClave, DescripcionProducto  ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            grid.Limpiar(false); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al cargar la lista de Claves-Lotes sin Ubicaciones."); 
            }
            else
            {
                if (leer.Leer())
                {
                    btnImprimir.Enabled = true; 
                    grid.LlenarGrid(leer.DataSetClase); 
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        } 
        #endregion Botones

        #region Impresion 
        private void Imprimir()
        {
            bool bRegresa = false;
            // string sSubFarmacia = "", sPasillos = "", sEstante = "", sEntrepaño = ""; 

            //if (validarImpresion())
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.TituloReporte = "Reporte de Ubicaciones de Claves";

                myRpt.NombreReporte = "PtoVta_ClavesSinUbicaciones.rpt";

                myRpt.RutaReporte = @GnFarmacia.RutaReportes; 
                myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

                ////if (General.ImpresionViaWeb)
                ////{
                ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////    DataSet datosC = DatosCliente.DatosCliente();

                ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                ////}
                ////else
                ////{
                ////    myRpt.CargarReporte(true);
                ////    bRegresa = !myRpt.ErrorAlGenerar;
                ////}

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        } 
        #endregion Impresion        
    }
}
