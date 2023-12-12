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
    public partial class FrmComprasPorProveedor_Farmacia : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid GridFarmacias;
        clsConsultas query;
        clsAyudas ayuda;
        FrmComprasPorProveedor_Dia FarmaciasCompras;
        DataSet dtsFarmacias;
        string sFormato = "#,###,##0.###0";

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();

        private bool bLimpiar = true;

        public FrmComprasPorProveedor_Farmacia()
        {
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            GridFarmacias = new clsGrid(ref grdFarmacias, this);
            GridFarmacias.EstiloGrid(eModoGrid.SeleccionSimple);
            GridFarmacias.Limpiar(false);

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;

            CargarEstados();
            CargarFarmacias();
        }

        private void FrmComprasPorProveedor_Farmacia_Load(object sender, EventArgs e)
        {
            if (bLimpiar) 
                btnNuevo_Click(null, null);
        }

        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(query.EstadosConFarmacias("FrmExistenciaPorClaveSSA_EstadoFarmacias"), true, "IdEstado", "Estado");
            cboEstados.SelectedIndex = 0;

            ////string sSql = "Select * From CatEstados (NoLock) Where Status = 'A'";

            ////cboEstados.Clear();
            ////cboEstados.Add("0", "<< Seleccione >>");

            ////if (!leer.Exec(sSql))
            ////{
            ////    Error.GrabarError(leer, "");
            ////    General.msjError("Ocurrió un error al obtener los Estados.");
            ////}
            ////else
            ////{
            ////    if (leer.Leer())
            ////    {
            ////        if (leer.Leer())
            ////        {
            ////            cboEstados.Add(leer.DataSetClase, true);
            ////        }
            ////        cboEstados.SelectedIndex = 0;
            ////    }
            ////}
        }

        private void CargarFarmacias()
        {
            dtsFarmacias = new DataSet();

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            cboFarmacias.SelectedIndex = 0;

            dtsFarmacias = query.Farmacias("CargarFarmacias()");
        }


        #region Buscar Proveedor

        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {
            leer = new clsLeer(ref cnn);

            if (txtIdProveedor.Text.Trim() != "")
            {
                leer.DataSetClase = query.Proveedores(txtIdProveedor.Text.Trim(), "txtIdProveedor_Validating");
                if (leer.Leer())
                    CargaDatosProveedor();
                else
                    txtIdProveedor.Focus();
            }
        }

        private void CargaDatosProveedor()
        {
            //Se hace de esta manera para la ayuda.
            txtIdProveedor.Text = leer.Campo("IdProveedor");
            lblProveedor.Text = leer.Campo("Nombre");
        }

        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Proveedores("txtIdProveedor_KeyDown");

                if (leer.Leer())
                {
                    CargaDatosProveedor();
                }
            }
        }

        #endregion Buscar Proveedor

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            tmExistencias.Stop();
            tmExistencias.Enabled = false;

            query.MostrarMsjSiLeerVacio = false;
            Fg.IniciaControles(this, true);
            GridFarmacias.Limpiar(false);

            BloqueaDatosConsulta(true);
            rdoTodas.Checked = true;            

            query.MostrarMsjSiLeerVacio = true;

            txtIdProveedor.Focus();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            int iTipoDeCompra = 0;

            //Se indica el tipo de Compra
            if (rdoSinPromocion.Checked)
                iTipoDeCompra = 1;
            else if (rdoConPromocion.Checked)
                iTipoDeCompra = 2;

            if (GridFarmacias.Rows == 0)
            {
                General.msjUser("No ha información en pantalla para generar la impresión.");
            }
            else
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                byte[] btReporte = null;

                myRpt.RutaReporte = GnOficinaCentral.RutaReportes;
                myRpt.NombreReporte = "Central_Movimientos_Compras";

                myRpt.Add("@IdEmpresa", "");
                myRpt.Add("@IdEstado", cboEstados.Data);
                myRpt.Add("@IdFarmacia", cboFarmacias.Data);
                myRpt.Add("@IdProveedor", txtIdProveedor.Text.Trim());
                myRpt.Add("@TipoDeCompra", iTipoDeCompra);
                myRpt.Add("@Mostrar", 2);
                myRpt.Add("@FechaInicial", dtpFechaInicial.Text);
                myRpt.Add("@FechaFinal", General.FechaYMD(dtpFechaFinal.Value, "-")); //Se le agregan 3 meses a la Fecha Final.

                DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                DataSet datosC = DatosCliente.DatosCliente();

                btReporte = conexionWeb.Reporte(InfoWeb, datosC);

                if (!myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true))
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            LlenarGridFarmacias();
        }

        #endregion Botones

        #region Grid       

        private void LlenarGridFarmacias()
        {
            string sTipoPromocion = "";

            if (rdoConPromocion.Checked)
                sTipoPromocion = "And EsPromocionRegalo = '1' ";
            else if (rdoSinPromocion.Checked)
                sTipoPromocion = "And EsPromocionRegalo = '0' ";

            string sSql = string.Format("Select Convert( varchar(10), V.FechaSistema, 120 ) as FechaSistema, Sum(V.Total) as Total " +
                " From vw_ComprasEnc V (NoLock) " +
                " Inner Join vw_Farmacias F(NoLock) On ( V.IdEstado = F.IdEstado And V.IdFarmacia = F.IdFarmacia ) " +
                " Where V.IdEstado = '{0}' And V.IdFarmacia = '{1}' And V.IdProveedor = '{2}' " + sTipoPromocion +
                " And Convert( varchar(10), V.FechaSistema, 120 )  Between '{3}' And '{4}' " +
                " Group by Convert( varchar(10), V.FechaSistema, 120 ) " +
                " Order by FechaSistema ",
                cboEstados.Data, cboFarmacias.Data, txtIdProveedor.Text.Trim(),
                General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

            GridFarmacias.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de las Farmacias.");
            }
            else
            {
                if (leer.Leer())
                {
                    GridFarmacias.LlenarGrid(leer.DataSetClase);

                    BloqueaDatosConsulta(false);//Esto es para forzar al usuario usar el boton nuevo.
                }
                else
                    General.msjUser("No existe información para mostrar bajo los criterios seleccionados");
            }
            lblTotalFarmacias.Text = GridFarmacias.TotalizarColumnaDou(2).ToString(sFormato);
        }

        private void grdFarmacias_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sEstado = cboEstados.Data;
            string sFarmacia = cboFarmacias.Data;
            string sFecha = GridFarmacias.GetValue(GridFarmacias.ActiveRow, 1);

            int iTipoVenta = 1;

            if (rdoSinPromocion.Checked)
                iTipoVenta = 2;
            else if (rdoConPromocion.Checked)
                iTipoVenta = 3;

            if (sEstado != "" && sFarmacia != "")
            {
                FarmaciasCompras = new FrmComprasPorProveedor_Dia();
                FarmaciasCompras.MostrarDetalle(sEstado, sFarmacia, txtIdProveedor.Text, sFecha, iTipoVenta);
            }
        }

        #endregion Grid        

        #region Eventos

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
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

        #endregion Eventos

        private void BloqueaDatosConsulta( bool bEnable )
        {
            btnEjecutar.Enabled = bEnable;
            txtIdProveedor.Enabled = bEnable;
            cboEstados.Enabled = bEnable;
            cboFarmacias.Enabled = bEnable;

            dtpFechaInicial.Enabled = bEnable;
            dtpFechaFinal.Enabled = bEnable;

            rdoTodas.Enabled = bEnable;
            rdoSinPromocion.Enabled = bEnable;
            rdoConPromocion.Enabled = bEnable;
            
        }

        public void MostrarDetalle(string sEstado, string sFarmacia, string sIdProveedor, string FechaInicial, string FechaFinal, int iTipoPromocion)
        {
            bLimpiar = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;

            txtIdProveedor.Text = sIdProveedor;
            txtIdProveedor_Validating(this, null);
            cboEstados.Data = sEstado;
            cboFarmacias.Data = sFarmacia;

            if (iTipoPromocion == 2)
                rdoSinPromocion.Checked = true;
            else if (iTipoPromocion == 3)
                rdoConPromocion.Checked = true;
            else
                rdoTodas.Checked = true;


            dtpFechaInicial.Value = DateTime.Parse(FechaInicial);
            dtpFechaFinal.Value = DateTime.Parse(FechaFinal);
            BloqueaDatosConsulta(false);

            LlenarGridFarmacias();

            this.ShowDialog();
        }


    }
}
