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
    public partial class FrmDevoluciones_Compras_General : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid GridEstados, GridFarmacias;
        clsConsultas query;
        clsAyudas ayuda;
        FrmDevoluciones_Compras_Estado EstadosDevoluciones;
        FrmDevoluciones_Compras_Farmacia FarmaciasDevoluciones;
        clsDatosCliente DatosCliente;
        string sFormato = "#,###,##0.###0";
        DataSet dtsComprasFarmacias;        
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();



        private bool bLimpiar = true;

        public FrmDevoluciones_Compras_General()
        {
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            GridEstados = new clsGrid(ref grdEstados, this);
            GridEstados.EstiloGrid(eModoGrid.SeleccionSimple);
            GridEstados.Limpiar(false);

            GridFarmacias = new clsGrid(ref grdFarmacias, this);
            GridFarmacias.EstiloGrid(eModoGrid.SeleccionSimple);
            GridFarmacias.Limpiar(false);

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;
        }

        private void FrmDevoluciones_Compras_General_Load(object sender, EventArgs e)
        {
            if (bLimpiar) 
                btnNuevo_Click(null, null);
        }

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnImprimir.Enabled = false;
            tmExistencias.Stop();
            tmExistencias.Enabled = false;

            Fg.IniciaControles(this, true);

            GridEstados.Limpiar(false);
            GridFarmacias.Limpiar(false);

            BloqueaDatosConsulta(true);
            rdoTodas.Checked = true;

            query.MostrarMsjSiLeerVacio = true;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            int iTipoDeCompra = 0;

            //Se indica el tipo de Compra
            if (rdoSinPromocion.Checked)
                iTipoDeCompra = 1;
            else if (rdoConPromocion.Checked)
                iTipoDeCompra = 2;

            if (GridEstados.Rows == 0)
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
                myRpt.Add("@IdEstado", "");
                myRpt.Add("@IdFarmacia", "");
                myRpt.Add("@IdProveedor", "");
                myRpt.Add("@TipoDeCompra", iTipoDeCompra);
                myRpt.Add("@Mostrar", 1);
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
            LlenarGridEstados();
        }

        #endregion Botones

        #region Grid       

        private void LlenarGridEstados()
        {
            string sTipoPromocion = "";

            if (rdoConPromocion.Checked)
                sTipoPromocion = "And EsPromocionRegalo = '1' ";
            else if (rdoSinPromocion.Checked)
                sTipoPromocion = "And EsPromocionRegalo = '0' ";

            string sSql = string.Format("Select IdEstado, Estado, Sum(Total) as Total " +
                " From vw_DevolucionesEnc_Compras (NoLock) " +
                " Where Convert( varchar(10), FechaSistemaDevol, 120 ) Between '{0}' And '{1}' " + sTipoPromocion +
                " And TipoDeDevolucion = '2' " +
                " Group by IdEstado, Estado " +
                " Order by Estado ",
                General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

            GridEstados.Limpiar(false);
            GridFarmacias.Limpiar(false);

            tmExistencias.Stop();
            tmExistencias.Enabled = false;

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de los Estados.");
            }
            else
            {
                if (leer.Leer())
                {
                    GridEstados.LlenarGrid(leer.DataSetClase);
                    LlenarGridFarmacias();
                    LlenarGridFarmacias(1);

                    tmExistencias.Start();
                    tmExistencias.Enabled = true;

                    BloqueaDatosConsulta(false);//Esto es para forzar al usuario usar el boton nuevo.
                }
                else
                    General.msjUser("No existe información para mostrar bajo los criterios seleccionados");
            }
            lblTotal.Text = GridEstados.TotalizarColumnaDou(3).ToString(sFormato);
        }

        private void LlenarGridFarmacias()
        {
            string sTipoPromocion = "";

            if (rdoConPromocion.Checked)
                sTipoPromocion = "And EsPromocionRegalo = '1' ";
            else if (rdoSinPromocion.Checked)
                sTipoPromocion = "And EsPromocionRegalo = '0' ";

            string sSql = string.Format("Select IdEstado, IdFarmacia, Farmacia, Sum(Total) as Total " +
                " From vw_DevolucionesEnc_Compras (NoLock) " +
                " Where Convert( varchar(10), FechaSistemaDevol, 120 ) Between '{0}' And '{1}' " + sTipoPromocion +
                " And TipoDeDevolucion = '2' " +
                " Group by IdEstado, IdFarmacia, Farmacia " +
                " Order by IdFarmacia ",
                General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de las farmacias.");
            }
            else
            {
                dtsComprasFarmacias = leer.DataSetClase;
            }

        }

        private void LlenarGridFarmacias(int Renglon)
        {
            GridFarmacias.Limpiar(false);
            try
            {
                leer.DataSetClase = dtsComprasFarmacias;
                string sEstado = GridEstados.GetValue(Renglon, 1);
                GridFarmacias.AgregarRenglon(leer.DataTableClase.Select(string.Format("IdEstado = '{0}'", sEstado)), 4, false);
            }
            catch //( Exception ex )
            {
                //General.msjUser(ex.Message);
            }
            lblTotalFarmacias.Text = GridFarmacias.TotalizarColumnaDou(4).ToString(sFormato);
        }

        private void grdEstados_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sEstado = GridEstados.GetValue(GridEstados.ActiveRow, 1);
            int iTipoPromocion = 1;

            if (rdoSinPromocion.Checked)
                iTipoPromocion = 2;
            else if (rdoConPromocion.Checked)
                iTipoPromocion = 3;

            if (sEstado != "")
            {
                EstadosDevoluciones = new FrmDevoluciones_Compras_Estado();
                EstadosDevoluciones.MostrarDetalle(sEstado, dtpFechaInicial.Text, dtpFechaFinal.Text, iTipoPromocion);
            }
        }

        private void grdFarmacias_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sEstado = GridEstados.GetValue(GridEstados.ActiveRow, 1);
            string sFarmacia = GridFarmacias.GetValue(e.Row + 1, 2);
            int iTipoPromocion = 1;

            if (rdoSinPromocion.Checked)
                iTipoPromocion = 2;
            else if (rdoConPromocion.Checked)
                iTipoPromocion = 3;

            if (sEstado != "" && sFarmacia != "")
            {
                FarmaciasDevoluciones = new FrmDevoluciones_Compras_Farmacia();
                FarmaciasDevoluciones.MostrarDetalle(sEstado, sFarmacia, dtpFechaInicial.Text, dtpFechaFinal.Text, iTipoPromocion);
            }
        }

        #endregion Grid        

        private void BloqueaDatosConsulta( bool bEnable )
        {
            btnEjecutar.Enabled = bEnable;

            dtpFechaInicial.Enabled = bEnable;
            dtpFechaFinal.Enabled = bEnable;

            rdoSinPromocion.Enabled = bEnable;
            rdoConPromocion.Enabled = bEnable;
            rdoTodas.Enabled = bEnable;
            
        }

        private void tmExistencias_Tick(object sender, EventArgs e)
        {
            if (this.ActiveControl.Name.ToUpper() == grdEstados.Name.ToUpper())
            {
                if (GridEstados.GetValue(GridEstados.ActiveRow, 1) != "")
                {
                    LlenarGridFarmacias(GridEstados.ActiveRow);
                }
            }
        }
  
    }
}
