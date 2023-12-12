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
    public partial class FrmVentasPorFarmacia : FrmBaseExt
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

        //FrmVentasPorFarmaciaFarmacias Codigo;
        //FrmVentasPorFarmaciaFarmaciasCodigos FarmaciasCodigos;

        private bool bLimpiar = true;

        public FrmVentasPorFarmacia()
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

            //cboEstados.Clear();
            //cboEstados.Add("0", "<< Seleccione >>");
            //cboEstados.Add(query.Estados("FrmExistenciaPorClaveSSA_EstadoFarmacias"), true, "IdEstado", "Nombre");
            //cboEstados.SelectedIndex = 0;

            CargarEstados();
            CargarFarmacias();
        }

        private void FrmVentasPorFarmacia_Load(object sender, EventArgs e)
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

            //string sSql = "Select * From CatEstados (NoLock) Where Status = 'A'";

            //cboEstados.Clear();
            //cboEstados.Add("0", "<< Seleccione >>");

            //if (!leer.Exec(sSql))
            //{
            //    Error.GrabarError(leer, "");
            //    General.msjError("Ocurrió un error al obtener los Estados.");
            //}
            //else
            //{
            //    if (leer.Leer())
            //    {
            //        if (leer.Leer())
            //        {
            //            cboEstados.Add(leer.DataSetClase, true);
            //        }
            //        cboEstados.SelectedIndex = 0;
            //    }
            //}
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
            string sSql = "", sEsDeConsignacion = "0", sLote = "";
            string sWhereConsignacion = "Inner Join VentasDet_Lotes L(NoLock) On ( V.IdEmpresa = L.IdEmpresa And V.IdEstado = L.IdEstado And V.IdFarmacia = L.IdFarmacia And V.Folio = L.FolioVenta ) ";

            if (rdoVenta.Checked)
            {
                sEsDeConsignacion = "1";
                sLote = "And L.ClaveLote Not Like '%*%' ";
            }
            else if (rdoConsignacion.Checked)
            {
                sEsDeConsignacion = "2";
                sLote = "And L.ClaveLote Like '%*%' ";
            }
            else
            {
                sWhereConsignacion = "";
            }

            if (rdoPublicoGral.Checked)
            {
                sSql = string.Format("Select Convert( varchar(10), V.FechaSistema, 120 ) as FechaSistema, Sum(V.Total) as Total " +
                    " From vw_VentasEnc V (NoLock) " + sWhereConsignacion +
                    " Where V.IdEstado = '{0}' And V.IdFarmacia = '{1}' And V.TipoDeVenta = '1' " +
                    " And Convert( varchar(10), V.FechaSistema, 120 )  Between '{2}' And '{3}' " + sLote +
                    " Group by Convert( varchar(10), V.FechaSistema, 120 ) " +
                    " Order by FechaSistema ",
                    cboEstados.Data, cboFarmacias.Data,
                    General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            }
            else
            {
                sSql = String.Format("Exec spp_Rpt_Consulta_Ventas_Farmacia '{0}', '{1}', '{2}', '{3}', '{4}' ",
                    cboEstados.Data, cboFarmacias.Data, General.FechaYMD(dtpFechaInicial.Value, "-"), 
                    General.FechaYMD(dtpFechaFinal.Value, "-"), sEsDeConsignacion);
            }

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
