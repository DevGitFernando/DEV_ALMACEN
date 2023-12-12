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
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace DllFarmaciaSoft.ReportesQFB
{
    public partial class FrmListadoProductosControlados : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente DatosCliente;
        clsGrid myGrid;
        clsLeer myLeer;
        wsFarmacia.wsCnnCliente conexionWeb;
        
        public FrmListadoProductosControlados()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;
            myLeer = new clsLeer(ref ConexionLocal);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.SoloLectura);
            myGrid.AjustarAnchoColumnasAutomatico = true; 
        }

        private void FrmListadoProductosControlados_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            ObtenerProductos();
        }

        private void ObtenerProductos()
        {
            string sSql = "Select IdProducto, ClaveSSA, Descripcion, ( Case When EsSectorSalud = '1' Then 'SI' Else 'NO' End ) as EsControlado " + 
                " From vw_Productos " + 
                "Where EsControlado = 1 " +
                "Order By ClaveSSA";

            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    myGrid.LlenarGrid(myLeer.DataSetClase);
                }
                else
                {
                    General.msjUser("No existe informacion para mostrar. Verifique");
                }


            }
            else
            {
                General.msjUser("Ocurrió un error al obtener el listado de productos");
            }
        }

        #region Botones 

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            myGrid.Limpiar(false);
            rdoProducto.Checked = true;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DatosCliente.Funcion = "btnImprimir_Click()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false; 

            myRpt.RutaReporte = DtGeneral.RutaReportes;

            if (rdoProducto.Checked)
            {
                myRpt.NombreReporte = "Central_Listado_Productos_Controlados";
            }
            else
            {
                myRpt.NombreReporte = "Central_Listado_Claves_Controlados";
            }

            bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);

            ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            ////DataSet datosC = DatosCliente.DatosCliente();

            ////conexionWeb.Timeout = 300000;
            ////btReporte = conexionWeb.Reporte(InfoWeb, datosC);

            if(!bRegresa && !DtGeneral.CanceladoPorUsuario)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Botones

    }
}
