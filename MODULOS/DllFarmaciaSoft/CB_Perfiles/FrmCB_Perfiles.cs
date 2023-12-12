using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
//using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.LimitesConsumoClaves;

namespace DllFarmaciaSoft.CB_Perfiles
{
    public partial class FrmCB_Perfiles : FrmBaseExt
    {
        clsConsultas query;
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente DatosCliente;
        clsLeer myLeer;
        clsListView list;
        private enum cols
        {
            Ninguno = 0, ClaveSSA = 1, Descripcion = 2, Presentacion = 3
        }

        public FrmCB_Perfiles()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            myLeer = new clsLeer(ref ConexionLocal);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");

            list = new clsListView(listFarmacias);
            list.PermitirAjusteDeColumnas = true;


            Inicializar();

            btnObtenerProgamacionConsumos.Visible = GnFarmacia.ValidarConsumoClaves_Programacion;
            btnObtenerProgamacionConsumos.Enabled = GnFarmacia.ValidarConsumoClaves_Programacion; 
        }

        private void Inicializar()
        {

            Fg.IniciaControles(this, true);
            IniciarToolBar(true, true, false);
            rdoGeneral.Checked = true;
            rdoGeneral.Enabled = true;
            rdoPorFarmacia.Enabled = true;
            list.Limpiar(); 
        }

        private void IniciarToolBar(bool Nuevo, bool Ejecutar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = "";

            list.Limpiar(); 
            if (rdoGeneral.Checked)
            {
                sSql = string.Format(
                    "Select 'Clave SSA' = ClaveSSA, 'Descripción' =  DescripcionClave, 'Presentación' = Presentacion " +
                    "From vw_CB_CuadroBasico_Claves CB (NoLock) " +
                    "Where StatusMiembro =  'A' and StatusClave = 'A' and CB.idestado = {0}" +
                    "Group by ClaveSSA, DescripcionClave, Presentacion " +
                    "Order by DescripcionClave ", 
                    DtGeneral.EstadoConectado );
            }
            else
            {
                sSql = string.Format(
                    "Select 'Clave SSA' = ClaveSSA, 'Descripción' =  DescripcionClave, 'Presentación' = Presentacion " +
                    "From vw_CB_CuadroBasico_Farmacias CB (NoLock)" +
                    "Where StatusMiembro = 'A' and StatusClave = 'A' and " + 
                    "   CB.IdEstado = {0} and CB.IdFarmacia = {1} " + 
                    "Group by ClaveSSA, DescripcionClave, Presentacion "+
                    "Order by DescripcionClave ", 
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            }
            //grid.Limpiar();
            list.Limpiar();
            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al obtener la lista de Claves.");
            }
            else
            {
                if (myLeer.Leer())
                {
                    lblClaves.Text = Convert.ToString(myLeer.Registros);
                    list.CargarDatos(myLeer.DataSetClase, true, true);
                    list.AnchoColumna((int)cols.Descripcion, 500);
                    IniciarToolBar(true, false, true);
                    rdoGeneral.Enabled = false;
                    rdoPorFarmacia.Enabled = false;
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion();
        }

        private void btnObtenerProgamacionConsumos_Click(object sender, EventArgs e)
        {
            FrmDescargarProgramacionConsumos f = new FrmDescargarProgramacionConsumos();
            f.ShowDialog(); 
        }

        private void ImprimirInformacion()
        {
            // if (validarImpresion())
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                //byte[] btReporte = null;
                bool bRegresa = false;

                myRpt.RutaReporte = DtGeneral.RutaReportes;
                myRpt.Add("Empresa", DtGeneral.EmpresaConectadaNombre);
                myRpt.Add("IdEstado", DtGeneral.EstadoConectado);

                if (rdoGeneral.Checked == true)
                {
                    myRpt.NombreReporte = "Ptovta_CB_PorEstado.rpt";
                }
                else
                {
                    myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                    myRpt.NombreReporte = "PtoVta_CB_PorFarmacia.rpt";
                }

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////DataSet datosC = DatosCliente.DatosCliente();

                ////btReporte = conexionWeb.Reporte(InfoWeb, datosC);

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }

        }
        private void rdoGeneral_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void toolStripBarraMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void lblClaves_Click(object sender, EventArgs e)
        {

        }

    }
}
