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

namespace OficinaCentral.Inventario
{
    public partial class FrmCaducarPorCodigoInterno : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        clsConsultas query;
        clsAyudas ayuda;
        DataSet dtsFarmacias;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();

        // FrmExistenciaPorCodigoEAN Ean;

        // private string Codigo = "";
        // private string Tipo = "";
        private bool bLimpiar = true;

        public FrmCaducarPorCodigoInterno()
        {
            InitializeComponent();

            cnn.SetConnectionString();          
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            Grid = new clsGrid(ref grdExistencia, this);
            Grid.EstiloGrid(eModoGrid.SeleccionSimple);
            Grid.Limpiar(false);

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;

            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(query.EstadosConFarmacias("FrmCaducarPorCodigoInterno"), true, "IdEstado", "Nombre");
            cboEstados.SelectedIndex = 0;

            CargarFarmacias();

        }

        private void FrmCaducarPorCodigoInterno_Load(object sender, EventArgs e)
        {
            if ( bLimpiar ) 
                btnNuevo_Click(null, null);
        }

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            query.MostrarMsjSiLeerVacio = false;
            Fg.IniciaControles(this, true);
            //grdExistencia.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            Grid.Limpiar(false);

            txtClaveSSA.Enabled = false;

            cboEstados.Focus();
            //txtId.Focus();

            query.MostrarMsjSiLeerVacio = true;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            txtId_Validating(null, null);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (Grid.Rows == 0)
            {
                General.msjUser("No ha información en pantalla para generar la impresión.");
            }
            else
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                byte[] btReporte = null;

                myRpt.RutaReporte = GnOficinaCentral.RutaReportes;
                myRpt.NombreReporte = "PtoVta_ExistenciasSalesDetallado";

                myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("@IdClaveSSA_Sal", txtClaveSSA.Text);
                myRpt.Add("@IdProducto", txtId.Text);
                myRpt.Add("@CodigoEAN", "");


                DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                DataSet datosC = DatosCliente.DatosCliente();

                btReporte = conexionWeb.Reporte(InfoWeb, datosC);

                if (!myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true))
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        #endregion Botones

        #region Eventos

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Productos("txtId_KeyDown");
                if (leer.Leer())
                {
                    txtId.Text = leer.Campo("IdProducto");
                    txtId_Validating(null, null);
                }
            }
        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text.Trim() != "")            
            {
                Grid.Limpiar(false);
                leer.DataSetClase = query.Productos(txtId.Text, "txtId_Validating");
                {
                    if (leer.Leer())
                    {
                        txtId.Enabled = false;
                        txtClaveSSA.Enabled = false;
                        cboEstados.Enabled = false;
                        cboFarmacias.Enabled = false;
                        dtpFechaInicial.Enabled = false;
                        dtpFechaFinal.Enabled = false;

                        txtId.Text = leer.Campo("IdProducto");
                        lblDescripcion.Text = leer.Campo("Descripcion");
                        txtClaveSSA.Text = leer.Campo("IdClaveSSA_Sal");
                        lblDescripcionClave.Text = leer.Campo("ClaveSSA") + " -- " + leer.Campo("DescripcionSal"); 
                        LlenarGrid();
                    }
                    else
                    {
                        txtId.Text = "";
                        txtId.Focus();
                    }
                }
            }
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFarmaciasEstados();
        }


        #endregion Eventos

        #region Grid 
        private void LlenarGrid()
        {
            string sSql = "";
            sSql = string.Format(" Select CodigoEAN, Existencia From vw_ExistenciaPorCodigoEAN_Lotes (nolock) " +
                " Where IdEstado = '{0}' AND IdFarmacia = '{1}' AND IdProducto = '{2}' " +
                " And Convert( varchar(10), FechaCaducidad, 120 )  Between '{3}' And '{4}'  " + 
                " And Existencia > 0 ",
                cboEstados.Data, cboFarmacias.Data , Fg.PonCeros(txtId.Text, 8),
                General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value.AddMonths(3), "-")); // Se le agregan 3 meses a la Fecha Final

            Grid.Limpiar(false);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "LlenarGrid");
                General.msjError("Error al consultar los datos");
            }
            else
            {
                if (leer.Leer())
                    Grid.LlenarGrid(leer.DataSetClase);
                else
                    General.msjUser("No existe información para mostrar bajo los criterios seleccionados");
            }

            lblTotal.Text = Grid.TotalizarColumna(2).ToString();
        }

        private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //string sDato = Grid.GetValue(e.Row + 1, 1);
            //Ean = new FrmExistenciaPorCodigoEAN();
            //Ean.MostrarDetalle(txtId.Text, sDato );
        }

        #endregion Grid

        public void MostrarDetalle(string IdProducto, string IdEstado, string IdFarmacia, string FechaInicial, string FechaFinal )
        {
            bLimpiar = false;
            btnNuevo.Enabled = false;
            txtId.Text = IdProducto;

            cboEstados.Data = IdEstado;
            cboEstados.Enabled = false;

            cboFarmacias.Data = IdFarmacia;
            cboFarmacias.Enabled = false;

            dtpFechaInicial.Value = DateTime.Parse(FechaInicial);
            dtpFechaFinal.Value = DateTime.Parse(FechaFinal);
            dtpFechaInicial.Enabled = false;
            dtpFechaFinal.Enabled = false;

            txtId_Validating(null, null);
            this.ShowDialog();
        }
        
        private void CargarFarmacias()
        {
            dtsFarmacias = new DataSet();

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            cboFarmacias.SelectedIndex = 0;

            dtsFarmacias = query.Farmacias("CargarFarmacias()");
        }

        private void CargarFarmaciasEstados()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            if (cboEstados.SelectedIndex != 0)
            {
                try
                {
                    cboFarmacias.Add(dtsFarmacias.Tables[0].Select(string.Format("IdEstado = '{0}'", cboEstados.Data)), true, "IdFarmacia", "NombreFarmacia");
                }
                catch { }
            }
            cboFarmacias.SelectedIndex = 0;
        }

        


    }
}
