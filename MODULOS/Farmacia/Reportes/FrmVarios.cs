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
    public partial class FrmVarios : FrmBaseExt
    {

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        // clsGrid Grid;

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; 

        public FrmVarios()
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

        #region Impresion
        private void ImprimirInformacion()
        {
            int iTipoInsumo = 0;
            int iTipoDispensacion = 0;
            string sMsj = ""; 


            // if (validarImpresion())
            {

                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;


                if (rdoUnidades.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_Admon_ListadoDeUnidades.rpt";
                    myRpt.Add("Empresa", DtGeneral.EmpresaConectadaNombre);
                    myRpt.Add("IdEstado", DtGeneral.EstadoConectado); 
                }

                if (rdoUnidadStock.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_Admon_UnidadesStock.rpt";
                    myRpt.Add("Empresa", DtGeneral.EmpresaConectadaNombre);
                    myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                    myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada); 
                } 

                if (rdoClaveNoAbastecidas.Checked)
                {
                    sMsj = "El Reporte de Claves no Abastecidas requiere comunicación con el Sistema de Información de la Unidad" +
                        "para determinar las Claves no Abastecidas," + 
                        "dicha comunicaión no se encuentra Configurada por tal motivo las Cantidades aparecerán en Cero.";
                    General.msjAviso(sMsj); 

                    myRpt.NombreReporte = "PtoVta_Admon_ClavesNoAbastecidas.rpt";
                    myRpt.Add("Empresa", DtGeneral.EmpresaConectadaNombre);
                    myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                    myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);

                    myRpt.Add("FechaInicial", dtpFechaInicial.Value);
                    myRpt.Add("FechaFinal", dtpFechaFinal.Value); 
                }

                if (rdoDesviacion.Checked)
                {
                    sMsj = "El Reporte de Desviación de Dispensación requiere que se cuente con información" +
                        "de al menos 4 meses de dispensación, el Sistema no cuenta con la información suficiente" +
                        "para realizar los calculos necesarios, por tal motivo las Cantidades aparecerán en Cero.";
                    General.msjAviso(sMsj);

                    myRpt.NombreReporte = "PtoVta_Admon_DesviacionDispensacion.rpt";
                    myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
                    myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
                    myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);

                    myRpt.Add("@IdCliente", "*");
                    myRpt.Add("@IdSubCliente", "*");
                    myRpt.Add("@IdPrograma", "*");
                    myRpt.Add("@IdSubPrograma", "*");

                    myRpt.Add("@FechaInicial", General.FechaYMD(dtpFechaInicial.Value));
                    myRpt.Add("@FechaFinal", General.FechaYMD(dtpFechaFinal.Value));
                    myRpt.Add("@TipoInsumo", iTipoInsumo);
                    myRpt.Add("@TipoDispensacion", iTipoDispensacion); 
                } 

                DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                DataSet datosC = DatosCliente.DatosCliente();

                btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                if (myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true))
                {
                    //btnNuevo_Click(null, null); 
                }
                else
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Impresion
    }
}
