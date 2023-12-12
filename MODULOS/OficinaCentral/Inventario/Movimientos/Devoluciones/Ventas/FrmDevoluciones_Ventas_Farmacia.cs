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
    public partial class FrmDevoluciones_Ventas_Farmacia : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid GridFarmacias;
        clsConsultas query;
        clsAyudas ayuda;
        DataSet dtsFarmacias;
        string sFormato = "#,###,##0.###0";

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();

        //FrmDevoluciones_Ventas_FarmaciaFarmacias Codigo;
        //FrmDevoluciones_Ventas_FarmaciaFarmaciasCodigos FarmaciasCodigos;

        private bool bLimpiar = true;

        public FrmDevoluciones_Ventas_Farmacia()
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

        private void FrmDevoluciones_Ventas_Farmacia_Load(object sender, EventArgs e)
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

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            tmExistencias.Stop();
            tmExistencias.Enabled = false;

            query.MostrarMsjSiLeerVacio = false;
            Fg.IniciaControles(this, true);
            GridFarmacias.Limpiar(false);

            BloqueaDatosConsulta(true);
            rdoVenta.Checked = true;
            rdoCredito.Checked = true;            

            query.MostrarMsjSiLeerVacio = true;

            cboEstados.Focus();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            int iTipoDeFarmacia = 0, iTipoDeVenta = 0;

            //Se indica el Tipo de Farmacia
            if (rdoVenta.Checked)
                iTipoDeFarmacia = 1;
            else if (rdoConsignacion.Checked)
                iTipoDeFarmacia = 2;

            //Se indica el Tipo de Venta
            if (rdoCredito.Checked)
                iTipoDeVenta = 1;
            else if (rdoPublicoGral.Checked)
                iTipoDeVenta = 2;

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
                myRpt.NombreReporte = "Central_Movimientos_Ventas";

                myRpt.Add("@IdEmpresa", "");
                myRpt.Add("@IdEstado", cboEstados.Data);
                myRpt.Add("@IdFarmacia", cboFarmacias.Data);
                myRpt.Add("@TipoDeFarmacia", iTipoDeFarmacia);
                myRpt.Add("@TipoDeVenta", iTipoDeVenta);
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
            string sTipoDeVenta = "", sEsDeConsignacion = "";

            if (rdoPublicoGral.Checked)
                sTipoDeVenta = "And V.TipoDeVenta = '1' ";
            else if (rdoCredito.Checked)
                sTipoDeVenta = "And V.TipoDeVenta = '2' ";

            if (rdoVenta.Checked)
                sEsDeConsignacion = "And F.EsDeConsignacion = '0' ";
            else if (rdoConsignacion.Checked)
                sEsDeConsignacion = "And F.EsDeConsignacion = '1' ";

            string sSql = string.Format("Select Convert( varchar(10), V.FechaSistemaDevol, 120 ) as FechaSistemaDevol, Sum(V.Total) as Total " +
                " From vw_DevolucionesEnc_Ventas V (NoLock) " +
                " Inner Join vw_Farmacias F(NoLock) On ( V.IdEstado = F.IdEstado And V.IdFarmacia = F.IdFarmacia ) " +
                " Where V.IdEstado = '{0}' And V.IdFarmacia = '{1}' And V.TipoDeDevolucion = '1' " + sTipoDeVenta + sEsDeConsignacion +
                " And Convert( varchar(10), V.FechaSistemaDevol, 120 )  Between '{2}' And '{3}' " +
                " Group by Convert( varchar(10), V.FechaSistemaDevol, 120 ) " +
                " Order by FechaSistemaDevol ",
                cboEstados.Data, cboFarmacias.Data,
                General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

            GridFarmacias.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de las Ventas.");
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
            cboEstados.Enabled = bEnable;
            cboFarmacias.Enabled = bEnable;
            rdoVenta.Enabled = bEnable;
            rdoConsignacion.Enabled = bEnable;
            rdoConsultaAmbas.Enabled = bEnable;

            dtpFechaInicial.Enabled = bEnable;
            dtpFechaFinal.Enabled = bEnable;

            rdoCredito.Enabled = bEnable;
            rdoPublicoGral.Enabled = bEnable;
            rdoVentaAmbos.Enabled = bEnable;
            
        }

        public void MostrarDetalle(string sEstado, string sFarmacia, string FechaInicial, string FechaFinal, int iTipoFarmacia, int iTipoVenta)
        {
            bLimpiar = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;

            cboEstados.Data = sEstado;
            cboFarmacias.Data = sFarmacia;

            if (iTipoFarmacia == 2)
                rdoConsignacion.Checked = true;
            else if(iTipoFarmacia == 3)
                rdoConsultaAmbas.Checked = true;
            else
                rdoVenta.Checked = true;

            if (iTipoVenta == 2)
                rdoPublicoGral.Checked = true;
            else if (iTipoVenta == 3)
                rdoVentaAmbos.Checked = true;
            else
                rdoCredito.Checked = true;


            dtpFechaInicial.Value = DateTime.Parse(FechaInicial);
            dtpFechaFinal.Value = DateTime.Parse(FechaFinal);
            BloqueaDatosConsulta(false);

            LlenarGridFarmacias();

            this.ShowDialog();
        }
  
    }
}
