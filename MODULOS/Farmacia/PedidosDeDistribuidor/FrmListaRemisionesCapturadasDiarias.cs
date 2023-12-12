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

namespace Farmacia.PedidosDeDistribuidor
{
    public partial class FrmListaRemisionesCapturadasDiarias : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        clsDatosCliente DatosCliente;
        clsListView lst;

        clsConsultas Consultas;
        clsAyudas Ayudas;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmListaRemisionesCapturadasDiarias()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            leer = new clsLeer(ref cnn);

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            lst = new clsListView(lstFoliosRemisiones);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;
        }

        private void FrmListadoFoliosRemisiones_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarFoliosRemisiones();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirFoliosRemisiones();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            lst.Limpiar();
            HabilitarBotones(true, true, false);
            dtpFechaInicio.Focus();
        }

        private void CargarFoliosRemisiones()
        {
            string sSql = "";

            sSql = string.Format(" Select Folio, 'Fecha Registro' = Convert(varchar(10), FechaRegistro, 120),'Núm. Distribuidor' = IdDistribuidor, " +
                                " 'Distribuidor' = Distribuidor, " +
                                " 'Codigo Cliente' = CodigoCliente, Cliente, 'Referencia Documento' = ReferenciaPedido, " +
                                " 'Fecha Documento' = Convert(varchar(10), FechaDocumento, 120),  " +
                                " 'Tipo Remision' = Case When EsConsignacion = 1 Then 'CONSIGNACION' Else 'VENTA' End, Observaciones " +
                                " From vw_RemisionesDistEnc (Nolock) " +
                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}'  " +
                                " and Convert(varchar(10), FechaRegistro, 120) Between '{3}' and '{4}' " +
                                " Order By Folio ", sEmpresa, sEstado, sFarmacia,
                                General.FechaYMD(dtpFechaInicio.Value, "-"), General.FechaYMD(dtpFechaFin.Value, "-"));

            lst.Limpiar();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarFoliosRemisiones()");
                General.msjError("Ocurrió un error al obtener la lista de remisiones.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("No se encontro información con los criterios especificados.");
                }
                else
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    HabilitarBotones(true, true, true);
                }
            }

        }

        private void HabilitarBotones(bool Nuevo, bool Ejecutar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }

        private void HabilitarControles(bool Valor)
        {
            dtpFechaInicio.Enabled = Valor;
            dtpFechaFin.Enabled = Valor;
            rdoConcentrado.Enabled = Valor;
            rdoDetallado.Enabled = Valor;
        }
        #endregion Funciones

        #region Eventos_ListView
        private void lstFoliosRemisiones_DoubleClick(object sender, EventArgs e)
        {
            ////string sFolio = "";

            ////sFolio = lst.GetValue(1);

            ////FrmRemisionesDistribuidor f = new FrmRemisionesDistribuidor();
            ////f.LevantaForma(sFolio);
        }
        #endregion Eventos_ListView

        #region Impresion
        private void ImprimirFoliosRemisiones()
        {
            bool bRegresa = false;

            DatosCliente.Funcion = "ImprimirFoliosRemisiones()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;

            if (rdoConcentrado.Checked)
            {
                myRpt.NombreReporte = "PtoVta_ConcentradoRemisionesDiaras.rpt";
            }

            if (rdoDetallado.Checked)
            {
                myRpt.NombreReporte = "PtoVta_DetalladoRemisionesDiarias.rpt";
            }

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("FechaInicio", General.FechaYMD(dtpFechaInicio.Value, "-"));
            myRpt.Add("FechaFin", General.FechaYMD(dtpFechaFin.Value, "-"));

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Impresion
    }
}
