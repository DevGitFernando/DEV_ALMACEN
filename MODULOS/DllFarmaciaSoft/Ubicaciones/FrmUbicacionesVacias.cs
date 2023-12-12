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

namespace DllFarmaciaSoft.Ubicaciones
{
    public partial class FrmUbicacionesVacias : FrmBaseExt
    {
        enum Cols
        {
            Pasillo = 0, Estante = 2, Entrepano = 4 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsListView list;

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;

        bool bEsLecturaDeUbicaciones = false;
        bool bUbicacionSeleccionada = false; 
        public int Pasillo = 0;
        public int Estante = 0;
        public int Entrepano = 0; 

        public FrmUbicacionesVacias()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            leer = new clsLeer(ref cnn); 

            list = new clsListView(lstUbicaciones); 
        }

        private void FrmUbicacionesVacias_Load(object sender, EventArgs e)
        {
            if (bEsLecturaDeUbicaciones)
            {
                btnImprimir.Enabled = false;
                btnImprimir.Visible = false;
                btnEjecutar_Click(null, null); 
            } 
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        { 
            list.Limpiar(); 
            btnImprimir.Enabled = false;

            if (bUbicacionSeleccionada)
            {
                btnImprimir.Visible = false;
            } 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string[] sColumnas = { "IdRack", "Rack", "IdNivel", "Nivel", "IdEntrepaño", "Entrepaño" }; 
            string sSql = string.Format("Exec spp_Rpt_UbicacionesVacias '{0}', '{1}', '{2}', 2 ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada );

            list.Limpiar(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer.Error, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al obtener la información.");
            }
            else
            {
                if (leer.Leer())
                {
                    btnImprimir.Enabled = true;
                    leer.FiltrarColumnas(1, sColumnas); 
                    list.CargarDatos(leer.DataSetClase, true, true); 
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.TituloReporte = "Reporte de Ubicaciones vacias";
            myRpt.RutaReporte = @GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_UbicacionesVacias.rpt";


            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada); 
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado); 
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada); 
            myRpt.Add("@TipoReporte", "1"); 

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Botones 

        private void lstUbicaciones_DoubleClick(object sender, EventArgs e)
        {
            // strResultado = lwAyuda.FocusedItem.SubItems[intColumnas].Text.ToString(); 

            //Pasillo = Convert.ToInt32(lstUbicaciones.FocusedItem.SubItems["IdPasillo"].Text.ToString());
            //Estante = Convert.ToInt32(lstUbicaciones.FocusedItem.SubItems["IdEstante"].Text.ToString());
            //Entrepano = Convert.ToInt32(lstUbicaciones.FocusedItem.SubItems["IdEntrepaño"].Text.ToString());

            Pasillo = Convert.ToInt32(lstUbicaciones.FocusedItem.SubItems[(int)Cols.Pasillo].Text.ToString());
            Estante = Convert.ToInt32(lstUbicaciones.FocusedItem.SubItems[(int)Cols.Estante].Text.ToString());
            Entrepano = Convert.ToInt32(lstUbicaciones.FocusedItem.SubItems[(int)Cols.Entrepano].Text.ToString());
            bUbicacionSeleccionada = true; 

            this.Hide();
        }

        #region Funciones y Procedimientos Publicos 
        public bool MostrarUbicacioensVacias() 
        {
            bEsLecturaDeUbicaciones = true; 

            this.ShowDialog(); 

            return bUbicacionSeleccionada; 
        } 
        #endregion Funciones y Procedimientos Publicos 


    }
}
