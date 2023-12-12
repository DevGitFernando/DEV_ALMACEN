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

namespace Farmacia.Reportes
{
    public partial class FrmStatusPedidosClientes : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        // clsGrid Grid;

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; 

        public FrmStatusPedidosClientes()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;


            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            // Grid = new clsGrid(ref grdReporte, this);
            // CargarListaReportes(); 
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion();
        }

        private void AgregarReportes()
        {
            cboStatusPedidos.Clear();
            cboStatusPedidos.Add("0", "Pedidos Elaborados");
            cboStatusPedidos.Add("1", "Pedidos en Proceso");
            cboStatusPedidos.Add("2", "Pedidos surtidos en 10 dias");
            cboStatusPedidos.Add("3", "Pedidos surtidos en más de 10 dias");
            cboStatusPedidos.SelectedIndex = 0;
            cboStatusPedidos.Enabled = false; 

            cboTipoReporte.Clear(); 
            cboTipoReporte.Add("PtoVta_Admon_ConcentradoDePedidosEspeciales.rpt", "Concentrando de Pedidos");
            cboTipoReporte.Add("PtoVta_Admon_DetalladoPedidosEspeciales.rpt", "Detallado de Pedidos");
            cboTipoReporte.Add("PtoVta_Admon_DetalladoPedidosEspecialesStatus.rpt", "Detallado de Pedidos por Status"); 
            cboTipoReporte.SelectedIndex = 1;
            cboTipoReporte.SelectedIndex = 0;
        }

        private void FrmStatusPedidosClientes_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            AgregarReportes();
        }

        #region Impresion
        private void ImprimirInformacion()
        {
            // int iTipoInsumo = 0;
            // int iTipoDispensacion = 0;
            // string sMsj = "";
            bool bRegresa = false; 

            //// if (validarImpresion())
            {

                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;

                myRpt.Add("Empresa", DtGeneral.EmpresaConectadaNombre);
                myRpt.NombreReporte = cboTipoReporte.Data; 


                if (cboTipoReporte.SelectedIndex == 2)
                {
                    myRpt.Add("TituloReporte", cboStatusPedidos.Text.ToUpper());
                    myRpt.Add("StatusPedido", cboStatusPedidos.Data);
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
        #endregion Impresion 

        private void cboTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboStatusPedidos.Enabled = false;
            if (cboTipoReporte.SelectedIndex == 2)
                cboStatusPedidos.Enabled = true; 
        }
    }
}
