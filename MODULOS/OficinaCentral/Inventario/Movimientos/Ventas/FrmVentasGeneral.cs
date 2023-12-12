﻿using System;
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
    public partial class FrmVentasGeneral : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid GridEstados, GridFarmacias;
        clsConsultas query;
        clsAyudas ayuda;
        FrmVentasPorEstado EstadosVentas;
        FrmVentasPorFarmacia FarmaciasVentas;
        clsDatosCliente DatosCliente;
        string sFormato = "#,###,##0.###0";
        DataSet dtsVentasFarmacias;        
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();



        private bool bLimpiar = true;

        public FrmVentasGeneral()
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

        private void FrmVentasGeneral_Load(object sender, EventArgs e)
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
            rdoVenta.Checked = true;
            rdoCredito.Checked = true;

            query.MostrarMsjSiLeerVacio = true;
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
                myRpt.NombreReporte = "Central_Movimientos_Ventas";

                myRpt.Add("@IdEmpresa", "");
                myRpt.Add("@IdEstado", "");
                myRpt.Add("@IdFarmacia", "");
                myRpt.Add("@TipoDeFarmacia", iTipoDeFarmacia);
                myRpt.Add("@TipoDeVenta", iTipoDeVenta);
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
                sSql = String.Format("Select V.IdEstado, V.Estado, Sum(V.Total) as Total " +
                    " From vw_VentasEnc V(NoLock) " + sWhereConsignacion + 
                    " Where Convert( varchar(10), V.FechaSistema, 120 )  Between '{0}' And '{1}' And TipoDeVenta = '1' " + sLote + 
                    " Group by V.IdEstado, V.Estado " + 
                    " Order by V.Estado ",
                    General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            }
            else
            {
                sSql = String.Format("Exec spp_Rpt_Consulta_Ventas_General '{0}', '{1}', '{2}' ",
                    General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), sEsDeConsignacion);
            }

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
                sSql = String.Format("Select V.IdEstado, V.IdFarmacia, V.Farmacia, Sum(V.Total) as Total " +
                    " From vw_VentasEnc V (NoLock) " + sWhereConsignacion +
                    " Where Convert( varchar(10), V.FechaSistema, 120 ) Between '{0}' And '{1}' And V.TipoDeVenta = '1' " + sLote +
                    " Group by V.IdEstado, V.IdFarmacia, V.Farmacia " +
                    " Order by V.IdFarmacia ",
                    General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            }
            else
            {
                sSql = String.Format("Exec spp_Rpt_Consulta_Ventas_General_Farmacia '{0}', '{1}', '{2}' ",
                    General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), sEsDeConsignacion);
            }

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de las farmacias.");
            }
            else
            {
                dtsVentasFarmacias = leer.DataSetClase;
            }

        }

        private void LlenarGridFarmacias(int Renglon)
        {
            GridFarmacias.Limpiar(false);
            try
            {
                leer.DataSetClase = dtsVentasFarmacias;
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
            int iTipoFarmacia = 1, iTipoVenta = 1;

            if (rdoConsignacion.Checked)
                iTipoFarmacia = 2;
            else if (rdoConsultaAmbas.Checked)
                iTipoFarmacia = 3;

            if (rdoPublicoGral.Checked)
                iTipoVenta = 2;

            if (sEstado != "")
            {
                EstadosVentas = new FrmVentasPorEstado();
                EstadosVentas.MostrarDetalle(sEstado, dtpFechaInicial.Text, dtpFechaFinal.Text, iTipoFarmacia, iTipoVenta);
            }
        }

        private void grdFarmacias_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sEstado = GridEstados.GetValue(GridEstados.ActiveRow, 1);
            string sFarmacia = GridFarmacias.GetValue(e.Row + 1, 2);
            int iTipoFarmacia = 1, iTipoVenta = 1;

            if (rdoConsignacion.Checked)
                iTipoFarmacia = 2;
            else if (rdoConsultaAmbas.Checked)
                iTipoFarmacia = 3;

            if (rdoPublicoGral.Checked)
                iTipoVenta = 2;

            if (sEstado != "" && sFarmacia != "")
            {
                FarmaciasVentas = new FrmVentasPorFarmacia();
                FarmaciasVentas.MostrarDetalle(sEstado, sFarmacia, dtpFechaInicial.Text, dtpFechaFinal.Text, iTipoFarmacia, iTipoVenta);
            }
        }

        #endregion Grid        

        private void BloqueaDatosConsulta( bool bEnable )
        {
            btnEjecutar.Enabled = bEnable;
            rdoVenta.Enabled = bEnable;
            rdoConsignacion.Enabled = bEnable;
            rdoConsultaAmbas.Enabled = bEnable;

            dtpFechaInicial.Enabled = bEnable;
            dtpFechaFinal.Enabled = bEnable;

            rdoCredito.Enabled = bEnable;
            rdoPublicoGral.Enabled = bEnable;
            
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
