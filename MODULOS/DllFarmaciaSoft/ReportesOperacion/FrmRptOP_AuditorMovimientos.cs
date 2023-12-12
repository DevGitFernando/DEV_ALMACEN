using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
//using DllFarmaciaSoft.Lotes;
//using DllFarmaciaSoft.Procesos;

namespace DllFarmaciaSoft.ReportesOperacion
{
    public partial class FrmRptOP_AuditorMovimientos : FrmBaseExt
    {
        //clsDatosConexion datosDeConexion;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer;
        clsConsultas consultas;
        clsAyudas ayuda;
        
        clsListView lst; 

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\.xls";
        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;


        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        //Thread _workerThread;

        //bool bEjecutando = false;
        //bool bSeEncontroInformacion = false;
        //bool bSeEjecuto = false;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmRptOP_AuditorMovimientos()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmRptOP_AuditorMovimientos");
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 
            
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            leer = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            lst = new clsListView(listResultado);
            lst.PermitirAjusteDeColumnas = true;
        }

        #region Form 
        private void FrmRptOP_AuditorMovimientos_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
        }
        #endregion Form

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles(this, true);
            IniciaToolBar(false, false);
            lst.LimpiarItems();
            txtIdPersonal.Focus();
        }

        private void IniciaToolBar(bool Ejecutar, bool Imprimir)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (txtIdPersonal.Text.Trim() != "")
            {
                CargarMovimientosPersonal();
            }
            else
            {
                General.msjAviso("No ha capturado el personal... Verifique..");
                txtIdPersonal.Focus();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones 

        #region Funciones y Procedimientos Privados
        private void CargaDatosPersonal()
        {
            txtIdPersonal.Text = leer.Campo("IdPersonal");
            lblPersonal.Text = leer.Campo("NombreCompleto");
        }

        private void CargarMovimientosPersonal()
        {
            string sSql = "";

            sSql = string.Format(" Select 'Fecha Movimiento' = FechaReg, 'Folio' = Folio, 'Tipo Movimiento' = DescTipoMovto " +
                                " From vw_MovtosInv_Enc (NoLock) " +
                                " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdPersonal = '{2}' " +
                                " and convert(varchar(10), FechaReg, 120) Between '{3}' and '{4}' " +
                                " Order By FechaReg ", sEstado, sFarmacia, Fg.PonCeros(txtIdPersonal.Text, 4),
                                General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

            lst.LimpiarItems();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarMovimientosPersonal");
                General.msjError("Ocurrió un error al consultar los Movimientos del Personal..");
            }
            else
            {
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    IniciaToolBar(true, true);
                }
                else
                {
                    General.msjAviso("No se encontró información bajo los criterios especificados...");
                }
            }

        }
        #endregion Funciones y Procedimientos Privados

        #region Funciones y Procedimientos Publicos
        #endregion Funciones y Procedimientos Publicos

        #region Eventos_Personal
        private void txtIdPersonal_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdPersonal.Text.Trim() == "")
            {
                txtIdPersonal.Focus();
                IniciaToolBar(false, false);
            }
            else
            {
                leer.DataSetClase = consultas.Personal(sEstado, sFarmacia, txtIdPersonal.Text, "txtIdPersonal_Validating");

                if (leer.Leer())
                {
                    CargaDatosPersonal();
                    IniciaToolBar(true, false);
                }
                else
                {
                    txtIdPersonal.Focus();
                }
            }
        }

        private void txtIdPersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Personal("txtIdPersonal_KeyDown", sEstado, sFarmacia);

                if (leer.Leer())
                {
                    CargaDatosPersonal();
                    IniciaToolBar(true, false);
                }
                else
                {
                    txtIdPersonal.Focus();
                }
            }
        }

        private void txtIdPersonal_TextChanged(object sender, EventArgs e)
        {
            lblPersonal.Text = "";
        }
        #endregion Eventos_Personal                

        #region Impresion
        private void Imprimir()
        {
            bool bRegresa = false;
            
            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);            

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_OP_Movimientos_Personal.rpt";

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("IdPersonal", Fg.PonCeros(txtIdPersonal.Text, 4));
            myRpt.Add("FechaIni", General.FechaYMD(dtpFechaInicial.Value, "-"));
            myRpt.Add("FechaFin", General.FechaYMD(dtpFechaFinal.Value, "-"));

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)            
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }            
        }
        #endregion Impresion
    }
}
