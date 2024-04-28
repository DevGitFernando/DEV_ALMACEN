using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

namespace DllFarmaciaSoft.Informacion
{
    public partial class FrmListadoFarmacias : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
        // clsDatosCliente datosCliente; 
        clsLeer leer;
        // clsGrid grid;
        DataSet dtsEstados = new DataSet();
        clsListView list; 


        clsDatosCliente DatosCliente;
        wsFarmaciaSoftGn.wsConexion conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        public FrmListadoFarmacias()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmaciaSoftGn.wsConexion();
            conexionWeb.Url = General.Url;

            leer = new clsLeer(ref cnn); 
            // grid = new clsGrid(ref grdFarmacias, this);
            // grid = new clsGrid(ref gridListaFarmacias, this);
            
            // grid.EstiloGrid(eModoGrid.SeleccionSimple); 

            list = new clsListView(listFarmacias); 

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name); 
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            list.Limpiar(); 
            cboEmpresas.Focus();

            if (!DtGeneral.ConexionOficinaCentral)
            {
                cboEmpresas.Data = DtGeneral.EmpresaConectada;
                cboEstados.Data = DtGeneral.EstadoConectado;

                btnEjecutar_Click(null, null); 

                cboEmpresas.Enabled = false;
                cboEstados.Enabled = false; 
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format( " Select IdFarmacia, Farmacia, Telefonos, Email " + 
                " From vw_Farmacias (NoLock) " + 
                " Where IdEstado = '{0}' AND Status = 'A'  ORDER BY IdFarmacia ", cboEstados.Data ); 

            //grid.Limpiar();
            list.Limpiar(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
            }
            else
            {
                cboEmpresas.Enabled = false; 
                cboEstados.Enabled = false; 
                if (leer.Leer())
                {
                    //DataSet dtTest = leer.DataSetClase.Copy(); 
                    //grdFarmacias.DataSource = leer.DataSetClase;
                    //grid.AgregarRenglon(leer.DataTableClase.Select("1=1"), 4, false); 

                    // grid.LlenarGrid(leer.DataSetClase, false, false); 
                    list.CargarDatos(leer.DataSetClase, true, true); 
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion(); 
        }
        #endregion Botones 

        private void FrmListadoFarmacias_Load(object sender, EventArgs e)
        {
            CargarEmpresas(); 
            LimpiarPantalla(); 
        }

        #region Impresion de informacion
        private void ImprimirInformacion()
        {
            // if (validarImpresion())
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;
                bool bRegresa = false; 

                myRpt.RutaReporte = DtGeneral.RutaReportes;
                myRpt.NombreReporte = "Central_ListadoDeUnidades.rpt";
                myRpt.TituloReporte = "Listado de Unidades";

                myRpt.Add("Empresa", DtGeneral.EmpresaConectadaNombre);
                myRpt.Add("IdEstado", cboEstados.Data);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

                ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////DataSet datosC = DatosCliente.DatosCliente();

                ////btReporte = conexionWeb.Reporte(InfoWeb, datosC);

                if (!bRegresa) 
                {
                    General.msjError("Error al cargar Informe.");
                }
            }

        }
        #endregion Impresion de informacion

        private void CargarEmpresas()
        {
            cboEmpresas.Clear();
            cboEmpresas.Add();

            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            string sSql = "Select Distinct IdEmpresa, NombreEmpresa From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEmpresas()");
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
            else
            {
                cboEmpresas.Add(leer.DataSetClase, true, "IdEmpresa", "NombreEmpresa");
                sSql = "Select IdEstado, NombreEstado, IdEmpresa, StatusEdo From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CargarEmpresas()");
                    General.msjError("Ocurrió un error al obtener la lista de Estados por Empresas.");
                }
                else
                {
                    dtsEstados = leer.DataSetClase;
                }

            }
            cboEmpresas.SelectedIndex = 0;
        }

        private void CargarEstados()
        {
            string sFiltro = string.Format(" IdEmpresa = '{0}' and StatusEdo = '{1}' ", cboEmpresas.Data, "A");
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.Add(dtsEstados.Tables[0].Select(sFiltro), true, "IdEstado", "NombreEstado");
            cboEstados.SelectedIndex = 0;
        }

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEstados.Clear();
            cboEstados.Add();
            list.Limpiar(); 

            if (cboEmpresas.SelectedIndex != 0)
            {
                cboEmpresas.Enabled = false;
                CargarEstados();
            } 
        }

        private void cboEstados_Validating(object sender, CancelEventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false; 
            }
        }
    }
}
