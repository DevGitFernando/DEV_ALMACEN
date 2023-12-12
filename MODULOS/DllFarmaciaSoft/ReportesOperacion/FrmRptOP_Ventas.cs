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
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Ayudas;
using DllFarmaciaSoft.ExportarExcel;

using ClosedXML.Excel;

namespace DllFarmaciaSoft.ReportesOperacion
{
    public partial class FrmRptOP_Ventas : FrmBaseExt
    {
        //clsDatosConexion datosDeConexion;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer;
        clsLeer leer2;
        clsConsultas consultas;
        clsAyudas ayuda;
        //clsGrid grid;
        clsListView lst; 

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\.xls";
        //clsExportarExcelPlantilla xpExcel;
        clsGenerarExcel excel;
        clsLeer leerExportarExcel;

        FrmHelpBeneficiarios helpBeneficiarios;

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        //Thread _workerThread;

        //bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        //bool bSeEjecuto = false; 

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdPublicoGral = GnFarmacia.PublicoGral;

        public FrmRptOP_Ventas()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmRptOP_Ventas");
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite; 
            
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            lst = new clsListView(lstResultado); 
        }

        #region Form 
        private void FrmRptOP_Ventas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }
        #endregion Form

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles(this, true);
            IniciaToolBar(false, false);
            IniciaFrames(true);
            chkMascaras.Checked = false;
            rdoSalidas.Checked = false;
            rdoDevoluciones.Checked = false;
            lst.LimpiarItems();
            txtCte.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                if (rdoSalidas.Checked || rdoDevoluciones.Checked || rdoNoSurtido.Checked || rdoDemanda.Checked)
                {
                    CargarDatos_Salidas();
                }
                else
                {
                    General.msjAviso("Seleccione el tipo de Reporte....");
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            CargarDetalles_Salidas();

            if (bSeEncontroInformacion)
            {
                GenerarExcel_XML();
            }
        }
        #endregion Botones 

        #region Funciones y Procedimientos Privados
        private void IniciaToolBar(bool Ejecutar, bool Exportar)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnExportarExcel.Enabled = Exportar;
        }

        private void IniciaFrames(bool Valor)
        {
            FrameParametros.Enabled = Valor;
            FrameFechas.Enabled = Valor;
            FrameTipoDeReporte.Enabled = Valor;
        }

        private void CargarDatos_Salidas()
        {
            string sSql = "", sWherePrograma = "", sWhereSubPrograma = "", sWhereBeneficiario = "";
            int iTipoReporte = 1;
            int iAplicarMascara = 0;
            IniciaToolBar(false, false);
            IniciaFrames(false);
            btnNuevo.Enabled = false;

            if (txtPro.Text.Trim() != "")
            {
                sWherePrograma = string.Format(" and IdPrograma = '{0}' ", Fg.PonCeros(txtPro.Text, 4));
            }

            if (txtSubPro.Text.Trim() != "")
            {
                sWhereSubPrograma = string.Format(" and IdSubPrograma = '{0}' ", Fg.PonCeros(txtSubPro.Text, 4));
            }

            if (txtBeneficiario.Text.Trim() != "")
            {
                sWhereBeneficiario = string.Format(" and IdBeneficiario = '{0}' ", Fg.PonCeros(txtBeneficiario.Text, 8));
            }

            if (rdoSalidas.Checked || rdoDemanda.Checked)
            {
                sSql = string.Format(" Select 'Fecha Salida' = Convert(varchar(10), FechaRegistro, 120), 'Folio Salida' = Folio, " +
	                                " 'Núm. Beneficiario' = IdBeneficiario, Beneficiario, 'Núm. Programa' = IdPrograma, Programa, " +
	                                " 'Núm. Sub-Programa' = IdSubPrograma, 'Sub-Programa' = SubPrograma " +
                                    " From vw_Impresion_Ventas_Credito (Nolock) " +
                                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdCliente = '{3}' and IdSubCliente = '{4}' " +
                                    " and Convert(varchar(10), FechaRegistro, 120) Between '{5}' and '{6}'  {7} {8} {9} " +
                                     " Group By FechaRegistro, Folio, IdBeneficiario, Beneficiario, " +
                                    " IdPrograma, Programa, IdSubPrograma, SubPrograma  Order By Folio, FechaRegistro ",
                                    sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4), General.FechaYMD(dtpFechaInicial.Value, "-"), 
                                    General.FechaYMD(dtpFechaFinal.Value, "-"), sWherePrograma, sWhereSubPrograma, sWhereBeneficiario);
            }

            if (rdoDevoluciones.Checked)
            {
                 sSql = string.Format(" Select 'Fecha Devolución' = Convert(varchar(10), FechaRegistro, 120), 'Folio Devolución' = Folio, 'Folio Salida' = FolioVenta,  " +
	                                " 'Núm. Beneficiario' = IdBeneficiario, Beneficiario, 'Núm. Programa' = IdPrograma, Programa,  " +
	                                " 'Núm. Sub-Programa' = IdSubPrograma, 'Sub-Programa' = SubPrograma " +
                                    " From vw_Impresion_Devolucion_Ventas_Credito (Nolock) " +
                                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdCliente = '{3}' and IdSubCliente = '{4}' " +
                                    " and Convert(varchar(10), FechaRegistro, 120) Between '{5}' and '{6}'  {7} {8} {9} " +
                                    " Group By FechaRegistro, Folio, FolioVenta, IdBeneficiario, Beneficiario, " +
	                                " IdPrograma, Programa, IdSubPrograma, SubPrograma  Order By Folio, FechaRegistro ",
                                    sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4), General.FechaYMD(dtpFechaInicial.Value, "-"),
                                    General.FechaYMD(dtpFechaFinal.Value, "-"), sWherePrograma, sWhereSubPrograma, sWhereBeneficiario);
            }

            if (rdoNoSurtido.Checked)
            {
                if (txtPro.Text.Trim() != "")
                {
                    sWherePrograma = string.Format(" and V.IdPrograma = '{0}' ", Fg.PonCeros(txtPro.Text, 4));
                }

                if (txtSubPro.Text.Trim() != "")
                {
                    sWhereSubPrograma = string.Format(" and V.IdSubPrograma = '{0}' ", Fg.PonCeros(txtSubPro.Text, 4));
                }

                if (txtBeneficiario.Text.Trim() != "")
                {
                    sWhereBeneficiario = string.Format(" and V.IdBeneficiario = '{0}' ", Fg.PonCeros(txtBeneficiario.Text, 8));
                }


                sSql = string.Format(" Select 'Fecha No Surtido' = Convert(varchar(10), N.FechaRegistro, 120), 'Folio No Surtido' = N.Folio, " +
		                        " 'Núm. Beneficiario' = V.IdBeneficiario, V.Beneficiario, 'Núm. Programa' = V.IdPrograma, V.Programa, " + 
		                        " 'Núm. Sub-Programa' = V.IdSubPrograma, 'Sub-Programa' = V.SubPrograma " + 
		                        " From  vw_Impresion_Ventas_ClavesSolicitadas N (Nolock) " +
		                        " Inner Join vw_Impresion_Ventas_Credito V (Nolock) " +
			                        " On ( V.IdEmpresa = N.IdEmpresa and V.IdEstado = N.IdEstado and V.IdFarmacia = N.IdFarmacia and V.Folio = N.Folio ) " +
		                        " Where N.IdEmpresa = '{0}' and N.IdEstado = '{1}' and N.IdFarmacia = '{2}' and V.IdCliente = '{3}' and V.IdSubCliente = '{4}'  " +
		                        " and Convert(varchar(10), N.FechaRegistro, 120) Between '{5}' and '{6}'  " +
		                        " and N.EsCapturada = 1  {7} {8} {9}  " +
		                        " Group By N.FechaRegistro, N.Folio, V.IdBeneficiario, V.Beneficiario, V.IdPrograma, V.Programa, V.IdSubPrograma, V.SubPrograma " +
                                " Order By N.Folio, N.FechaRegistro  ",
                                sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4), General.FechaYMD(dtpFechaInicial.Value, "-"),
                                General.FechaYMD(dtpFechaFinal.Value, "-"), sWherePrograma, sWhereSubPrograma, sWhereBeneficiario);


            }

            lst.LimpiarItems();


            iTipoReporte = rdoSalidas.Checked ? 1 : iTipoReporte;
            iTipoReporte = rdoDevoluciones.Checked ? 2 : iTipoReporte;
            iTipoReporte = rdoNoSurtido.Checked ? 3 : iTipoReporte;
            iTipoReporte = rdoDemanda.Checked ? 4 : iTipoReporte;

            iAplicarMascara = chkMascaras.Checked ? 1 : 0;

            sSql = string.Format("Exec spp_Rpt_OP_Ventas \n" + 
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', \n" + 
                    "\t@IdCliente = '{3}', @IdSubCliente = '{4}', @IdPrograma = '{5}', @IdSubPrograma = '{6}', \n" + 
                    "\t@FechaIncial = '{7}', @FechaFinal = '{8}', @IdBeneficiario = '{9}',  @TipoReporte = {10}, " +
                    "\t@ConcentradoReporte = {11}, @AplicarMascara = {12} ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                    txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), txtSubPro.Text.Trim(),
                    General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"),
                    txtBeneficiario.Text.Trim(), iTipoReporte, 1, iAplicarMascara);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDatos_Salidas");
                General.msjError("Ocurrió un error al consultar los datos de Salidas..");
                IniciaToolBar(true, false);
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
                    IniciaToolBar(true, false);
                }
            }

            IniciaFrames(true);
            btnNuevo.Enabled = true;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtCte.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado el Cliente.. Verifique..");
                txtCte.Focus();
            }

            if (bRegresa && txtSubCte.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado el Sub-Cliente.. Verifique..");
                txtSubCte.Focus();
            }

            return bRegresa;
        }

        private void CargarDetalles_Salidas()
        {
            string sSql = "", sWherePrograma = "", sWhereSubPrograma = "", sWhereBeneficiario = "";
            int iTipoReporte = 1;
            int iAplicarMascara = 0;
            leerExportarExcel = new clsLeer(ref cnn);

            this.Cursor = Cursors.Default;

            if (txtPro.Text.Trim() != "")
            {
                sWherePrograma = string.Format(" and IdPrograma = '{0}' ", Fg.PonCeros(txtPro.Text, 4));
            }

            if (txtSubPro.Text.Trim() != "")
            {
                sWhereSubPrograma = string.Format(" and IdSubPrograma = '{0}' ", Fg.PonCeros(txtSubPro.Text, 4));
            }

            if (txtBeneficiario.Text.Trim() != "")
            {
                sWhereBeneficiario = string.Format(" and IdBeneficiario = '{0}' ", Fg.PonCeros(txtBeneficiario.Text, 8));
            }

            if (rdoSalidas.Checked)
            {
                iTipoReporte = 1;
                sSql = string.Format(" Select * " +
                                    " From vw_Impresion_Ventas_Credito_Lotes (Nolock) " +
                                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdCliente = '{3}' and IdSubCliente = '{4}' " +
                                    " and Convert(varchar(10), FechaRegistro, 120) Between '{5}' and '{6}' {7} {8} {9} Order By Folio, FechaRegistro ",
                                    sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4), General.FechaYMD(dtpFechaInicial.Value, "-"),
                                    General.FechaYMD(dtpFechaFinal.Value, "-"), sWherePrograma, sWhereSubPrograma, sWhereBeneficiario);
            }

            if (rdoDevoluciones.Checked)
            {
                iTipoReporte = 2;
                sSql = string.Format(" Select * " +
                                    " From vw_Impresion_Devolucion_Ventas_Credito (Nolock) " +
                                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdCliente = '{3}' and IdSubCliente = '{4}' " +
                                    " and Convert(varchar(10), FechaRegistro, 120) Between '{5}' and '{6}' {7} {8} {9}  Order By Folio, FechaRegistro ",
                                    sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4), General.FechaYMD(dtpFechaInicial.Value, "-"),
                                    General.FechaYMD(dtpFechaFinal.Value, "-"), sWherePrograma, sWhereSubPrograma, sWhereBeneficiario);
            }

            if (rdoNoSurtido.Checked)
            {
                iTipoReporte = 3;
                if (txtPro.Text.Trim() != "")
                {
                    sWherePrograma = string.Format(" and V.IdPrograma = '{0}' ", Fg.PonCeros(txtPro.Text, 4));
                }

                if (txtSubPro.Text.Trim() != "")
                {
                    sWhereSubPrograma = string.Format(" and V.IdSubPrograma = '{0}' ", Fg.PonCeros(txtSubPro.Text, 4));
                }

                if (txtBeneficiario.Text.Trim() != "")
                {
                    sWhereBeneficiario = string.Format(" and V.IdBeneficiario = '{0}' ", Fg.PonCeros(txtBeneficiario.Text, 8));
                }

                sSql = string.Format(" Select N.FechaRegistro, N.Folio, V.NumReceta, V.IdBeneficiario, V.Beneficiario, V.IdPrograma, V.Programa, " + 
		                        " V.IdSubPrograma, V.SubPrograma, V.IdPersonal, V.NombrePersonal, N.ClaveSSA, N.DescripcionCortaClave, N.CantidadRequerida " + 
		                        " From vw_Impresion_Ventas_ClavesSolicitadas N (Nolock) " +
		                        " Inner Join vw_Impresion_Ventas_Credito V (Nolock) " +
			                        " On ( V.IdEmpresa = N.IdEmpresa and V.IdEstado = N.IdEstado and V.IdFarmacia = N.IdFarmacia and V.Folio = N.Folio ) " +
		                        " Where N.IdEmpresa = '{0}' and N.IdEstado = '{1}' and N.IdFarmacia = '{2}' and V.IdCliente = '{3}' and V.IdSubCliente = '{4}'  " +
		                        " and Convert(varchar(10), N.FechaRegistro, 120) Between '{5}' and '{6}' " + 
		                        " and N.EsCapturada = 1  {7} {8} {9} " + 
		                        " Group By N.FechaRegistro, N.Folio, V.NumReceta, V.IdBeneficiario, V.Beneficiario, V.IdPrograma, V.Programa, " + 
		                        " V.IdSubPrograma, V.SubPrograma, V.IdPersonal, V.NombrePersonal, N.ClaveSSA, N.DescripcionCortaClave, N.CantidadRequerida " +
                                " Order By N.Folio, N.FechaRegistro ",
                                sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4), General.FechaYMD(dtpFechaInicial.Value, "-"),
                                General.FechaYMD(dtpFechaFinal.Value, "-"), sWherePrograma, sWhereSubPrograma, sWhereBeneficiario);
            }

            if (rdoDemanda.Checked)
            {
                iTipoReporte = 4;
            }


            iAplicarMascara = chkMascaras.Checked ? 1 : 0;

            sSql = string.Format("Exec spp_Rpt_OP_Ventas @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdCliente = '{3}', @IdSubCliente = '{4}', " +
                "@IdPrograma = '{5}', @IdSubPrograma = '{6}', @FechaIncial = '{7}', @FechaFinal = '{8}', @IdBeneficiario = '{9}',  @TipoReporte = {10}, " +
                "@ConcentradoReporte = {11} , @AplicarMascara = {12} ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), txtSubPro.Text.Trim(),
                General.FechaYMD(dtpFechaInicial.Value, "-"),  General.FechaYMD(dtpFechaFinal.Value, "-"),
                txtBeneficiario.Text.Trim(), iTipoReporte, 0, iAplicarMascara);

            if (!leerExportarExcel.Exec(sSql))
            {
                Error.GrabarError(leerExportarExcel, "CargarDetalles_Salidas");
                General.msjError("Ocurrió un error al consultar los detalles..");
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;

                if (!leerExportarExcel.Leer())
                {
                    bSeEncontroInformacion = false;
                    General.msjAviso("No existe información para mostrar...");
                }
                else
                {
                    bSeEncontroInformacion = true;
                }
            }

            this.Cursor = Cursors.Default;

        }
        #endregion Funciones y Procedimientos Privados

        #region Eventos_Cliente
        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            leer2 = new clsLeer(ref cnn);
            if (txtCte.Text.Trim() == "")
            {
                txtCte.Text = "";
                lblCte.Text = "";
                txtSubCte.Text = "";
                lblSubCte.Text = "";
                txtPro.Text = "";
                lblPro.Text = "";
                txtSubPro.Text = "";
                lblSubCte.Text = "";
                txtCte.Focus();
            }
            else
            {
                leer2.DataSetClase = consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, "txtCte_Validating");
                if (!leer2.Leer())
                {
                    txtCte.Focus();                   
                }
                else
                {
                    txtCte.Enabled = false;
                    txtCte.Text = leer2.Campo("IdCliente");
                    lblCte.Text = leer2.Campo("NombreCliente");
                }
            }
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.F1)
            {
                leer = new clsLeer(ref cnn);

                leer.DataSetClase = ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, "txtCte_KeyDown");
                if (leer.Leer())
                {
                    txtCte.Enabled = false;
                    txtCte.Text = leer.Campo("IdCliente");
                    lblCte.Text = leer.Campo("NombreCliente");
                }
                else
                {
                    txtCte.Focus();
                }
            }
        }

        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            lblCte.Text = "";
            txtSubCte.Text = "";
            lblSubCte.Text = "";
            txtPro.Text = "";
            lblPro.Text = "";
            txtSubPro.Text = "";
            lblSubPro.Text = "";
        }        
        #endregion Eventos_Cliente

        #region Eventos_Sub-Cliente
        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {            
            if (txtSubCte.Text.Trim() == "")
            {
                txtSubCte.Text = "";
                lblSubCte.Text = "";
                txtPro.Text = "";
                lblPro.Text = "";
                txtSubPro.Text = "";
                lblSubPro.Text = "";
                txtSubPro.Focus();
            }
            else
            {
                leer2 = new clsLeer(ref cnn);

                leer2.DataSetClase = consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, "txtSubCte_Validating");
                if (!leer2.Leer())
                {
                    txtSubCte.Focus();                   
                }
                else
                {
                    txtSubCte.Enabled = false;
                    txtSubCte.Text = leer2.Campo("IdSubCliente");
                    lblSubCte.Text = leer2.Campo("NombreSubCliente");
                    IniciaToolBar(true, false);
                }
            }
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "")
                {
                    leer = new clsLeer(ref cnn);

                    leer.DataSetClase = ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, "txtSubCte_KeyDown");
                    if (leer.Leer())
                    {
                        txtSubCte.Enabled = false;
                        txtSubCte.Text = leer.Campo("IdSubCliente");
                        lblSubCte.Text = leer.Campo("NombreSubCliente");
                        IniciaToolBar(true, false);
                    }
                    else
                    {
                        txtSubCte.Focus();
                    }
                }
            }
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCte.Text = "";
            txtPro.Text = "";
            lblPro.Text = "";
            txtSubPro.Text = "";
            lblSubPro.Text = "";
        }
        #endregion Eventos_Sub-Cliente

        #region Eventos_Programa
        private void txtPro_Validating(object sender, CancelEventArgs e)
        {           

            if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "")
            {
                leer2 = new clsLeer(ref cnn); 

                leer2.DataSetClase = consultas.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtPro.Text, "txtPro_Validating");
                if (!leer2.Leer())
                {
                    txtPro.Focus();                    
                }
                else
                {
                    txtPro.Text = leer2.Campo("IdPrograma");
                    lblPro.Text = leer2.Campo("Programa");
                    txtSubPro.Text = "";
                    lblSubPro.Text = "";
                    txtSubPro.Focus();
                }
            }
            else
            {
                txtPro.Text = "";
                lblPro.Text = "";
                txtSubPro.Text = "";
                lblSubPro.Text = "";                
            }
        }

        private void txtPro_KeyDown(object sender, KeyEventArgs e)
        {       
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "")
                {
                    leer = new clsLeer(ref cnn);
                  
                    leer.DataSetClase = ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, "txtPro_KeyDown");
                    if (leer.Leer())
                    {
                        txtPro.Text = leer.Campo("IdPrograma");
                        lblPro.Text = leer.Campo("Programa");
                        txtSubPro.Text = "";
                        lblSubPro.Text = "";
                        txtSubPro.Focus();
                    }
                }
            }
        }

        private void txtPro_TextChanged(object sender, EventArgs e)
        {
            lblPro.Text = "";
            txtSubPro.Text = "";
            lblSubPro.Text = ""; 
        }       
        #endregion Eventos_Programa

        #region Eventos_SubPrograma
        private void txtSubPro_Validating(object sender, CancelEventArgs e)
        {            
            if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "" && txtSubPro.Text.Trim() != "")
            {
                leer2 = new clsLeer(ref cnn);

                leer2.DataSetClase = consultas.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtPro.Text, txtSubPro.Text, "txtSubPro_Validating");
                if (!leer2.Leer())
                {
                    txtSubPro.Focus();                   
                }
                else
                {
                    txtSubPro.Text = leer2.Campo("IdSubPrograma");
                    lblSubPro.Text = leer2.Campo("SubPrograma");                                         
                }
            }
            else
            {
                txtSubPro.Text = "";
                lblSubPro.Text = "";                
            }
        }

        private void txtSubPro_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.F1)
            {                
                if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "")
                {
                    leer = new clsLeer(ref cnn);

                    leer.DataSetClase = ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtPro.Text, "txtPro_KeyDown");
                    if (leer.Leer())
                    {
                        txtSubPro.Text = leer.Campo("IdSubPrograma");
                        lblSubPro.Text = leer.Campo("SubPrograma");                    
                    }
                }
            }
        }

        private void txtSubPro_TextChanged(object sender, EventArgs e)
        {
            lblSubPro.Text = "";
        }        
        #endregion Eventos_SubPrograma        

        #region Eventos_Beneficiario
        private void txtBeneficiario_Validating(object sender, CancelEventArgs e)
        {
            if (txtBeneficiario.Text.Trim() != "")
            {
                leer.DataSetClase = consultas.Beneficiarios(sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtBeneficiario.Text, "txtBeneficiario_Validating");
                if (leer.Leer())
                {
                    txtBeneficiario.Text = leer.Campo("IdBeneficiario");
                    lblBeneficiario.Text = leer.Campo("NombreCompleto");                   
                }
                else
                {
                    General.msjUser("Clave de Beneficiario no encontrada, verifique.");
                    txtBeneficiario.Focus();
                }
            }
            else
            {                
                //txtBeneficiario.Focus();
            }
        }

        private void txtBeneficiario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                helpBeneficiarios = new FrmHelpBeneficiarios();
                leer.DataSetClase = helpBeneficiarios.ShowHelp(Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4), false);
                if (leer.Leer())
                {
                    txtBeneficiario.Text = leer.Campo("IdBeneficiario");
                    lblBeneficiario.Text = leer.Campo("NombreCompleto");
                }
            }
        }

        private void txtBeneficiario_TextChanged(object sender, EventArgs e)
        {
            lblBeneficiario.Text = "";
        }        
        #endregion Eventos_Beneficiario

        #region Exportar_A_Excel
        private void GenerarExcel_XML()
        {
            string sConcepto = "";
            string sHoja = "";

            excel = new clsGenerarExcel();


            if (rdoSalidas.Checked)
            {
                sConcepto = "REPORTE DETALLADO DE SALIDAS DE DISPENSACION";
                sHoja = "Salidas";
            }

            if (rdoDevoluciones.Checked)
            {
                sConcepto = "REPORTE DETALLADO DE DEVOLUCIONES DE SALIDAS DE DISPENSACION";
                sHoja = "Devoluciones";
            }

            if (rdoNoSurtido.Checked)
            {
                sConcepto = "REPORTE CONCENTRADO DE NO SURTIDO";
                sHoja = "NoSurtidoConcentrado";
            }

            if (rdoDemanda.Checked)
            {
                sConcepto = "REPORTE DE DEMANDA";
                sHoja = "DEMANDA";
            }

            if(excel.PrepararPlantilla())
            {

                GenearExcel_XML_Detalles(sHoja, sConcepto, leerExportarExcel);

                if(rdoNoSurtido.Checked)
                {
                    sConcepto = "REPORTE DETALLADO DE NO SURTIDO";
                    sHoja = "NoSurtidoDetallado";
                    leerExportarExcel.DataTableClase = leerExportarExcel.Tabla(2);
                    GenearExcel_XML_Detalles(sHoja, sConcepto, leerExportarExcel);
                }

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);

            }
        }

        private void GenearExcel_XML_Detalles(string sNombreHoja, string Concepto, clsLeer Detalles)
        {
            int iRen = 2, iCol = 2, iColEnc = iCol + Detalles.Columnas.Length - 1;

            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            string sEstadoNom = DtGeneral.EstadoConectadoNombre;
            string sFarmaciaNom = sFarmacia + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
            string sFechaImpresion = "Fecha de Impresión: " + General.FechaSistemaFecha.ToString();
            string sCliente = "Cliente: " + txtCte.Text + " -- " + lblCte.Text;
            string sSubCliente = "Sub-Cliente: " + txtSubCte.Text + " -- " + lblSubCte.Text;

            excel.ArchivoExcel.Worksheets.Add(sNombreHoja);

            excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sEmpresaNom);
            excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sFarmaciaNom);
            excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, Concepto);
            excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, 6, sCliente, XLAlignmentHorizontalValues.Left);
            excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, 6, sSubCliente, XLAlignmentHorizontalValues.Left);
            iRen ++;
            excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sFechaImpresion, XLAlignmentHorizontalValues.Left);
            iRen++;
            excel.InsertarTabla(sNombreHoja, iRen, 2, Detalles.DataSetClase);
            excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);


        }
        //private void GenerarExcel()
        //{
        //    string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_OP_Ventas_Devoluciones.xls";
        //    this.Cursor = Cursors.WaitCursor;
        //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_OP_Ventas_Devoluciones.xls", DatosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = true;
        //        //leer.DataSetClase = dtsExistencias;

        //        this.Cursor = Cursors.Default;
        //        if (xpExcel.PrepararPlantilla())
        //        {
        //            IniciaToolBar(false, false);
        //            this.Cursor = Cursors.WaitCursor;
        //            if (rdoSalidas.Checked)
        //            {
        //                xpExcel.EliminarHoja("DEVOLUCIONES");
        //                xpExcel.EliminarHoja("NoSurtidoConcentrado");
        //                xpExcel.EliminarHoja("NoSurtidoDetallado");
        //                xpExcel.EliminarHoja("DEMANDA");
        //                ExportarSalidas();
        //            }

        //            if (rdoDevoluciones.Checked)
        //            {
        //                xpExcel.EliminarHoja("SALIDAS");
        //                xpExcel.EliminarHoja("NoSurtidoConcentrado");
        //                xpExcel.EliminarHoja("NoSurtidoDetallado");
        //                xpExcel.EliminarHoja("DEMANDA");
        //                ExportarDevoluciones();
        //            }

        //            if (rdoNoSurtido.Checked)
        //            {
        //                xpExcel.EliminarHoja("SALIDAS");
        //                xpExcel.EliminarHoja("DEVOLUCIONES");
        //                xpExcel.EliminarHoja("DEMANDA");
        //                ExportarNoSurtidoConcentrado();
        //                ExportarNoSurtidoDetalle();
        //            }

        //            if (rdoDemanda.Checked)
        //            {
        //                xpExcel.EliminarHoja("SALIDAS");
        //                xpExcel.EliminarHoja("DEVOLUCIONES");
        //                xpExcel.EliminarHoja("NoSurtidoConcentrado");
        //                xpExcel.EliminarHoja("NoSurtidoDetallado");
        //                ExportarDemanda();
        //            }

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }

        //            IniciaToolBar(true, true);
        //        }
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        //private void ExportarSalidas()
        //{
        //    int iHoja = 1, iRenglon = 11, iCol = 2;
        //    string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
        //    string sEstadoNom = DtGeneral.EstadoConectadoNombre;
        //    string sFarmaciaNom = sFarmacia + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
        //    string sConcepto = "REPORTE DETALLADO DE SALIDAS DE DISPENSACION";
        //    string sFechaImpresion = General.FechaSistemaFecha.ToString();
        //    string sCliente = txtCte.Text + " -- " + lblCte.Text;
        //    string sSubCliente = txtSubCte.Text + " -- " + lblSubCte.Text;

        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(sEmpresaNom, 2, 2);
        //    xpExcel.Agregar(sFarmaciaNom, 3, 2);
        //    xpExcel.Agregar(sConcepto, 4, 2);
        //    xpExcel.Agregar(sCliente, 5, 3);
        //    xpExcel.Agregar(sSubCliente, 6, 3);

        //    //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
        //    xpExcel.Agregar(sFechaImpresion, 8, 3);

        //    leerExportarExcel.RegistroActual = 1;

        //    while (leerExportarExcel.Leer())
        //    {
        //        iCol = 2;
        //        xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("RefObservaciones"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("NumReceta"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdBeneficiario"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Beneficiario"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdPrograma"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Programa"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdSubPrograma"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("SubPrograma"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdPersonal"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("NombrePersonal"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA_Aux"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCortaClave"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdProducto"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCorta"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("CodigoEAN"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("ClaveLote"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("FechaCad"), iRenglon, iCol++);
        //        //xpExcel.Agregar(leerExportarExcel.Campo("PrecioCosto_Unitario"), iRenglon, 20);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdPasillo"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdEstante"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdEntrepaño"), iRenglon, iCol++);
        //        iRenglon++;
        //    }

        //    // Finalizar el Proceso 
        //    xpExcel.CerrarDocumento();

        //}

        //private void ExportarDevoluciones()
        //{
        //    int iHoja = 1, iRenglon = 11, icol = 2;
        //    string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
        //    string sEstadoNom = DtGeneral.EstadoConectadoNombre;
        //    string sFarmaciaNom = sFarmacia + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
        //    string sConcepto = "REPORTE DETALLADO DE DEVOLUCIONES DE SALIDAS DE DISPENSACION";
        //    string sFechaImpresion = General.FechaSistemaFecha.ToString();
        //    string sCliente = txtCte.Text + " -- " + lblCte.Text;
        //    string sSubCliente = txtSubCte.Text + " -- " + lblSubCte.Text;

        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(sEmpresaNom, 2, 2);
        //    xpExcel.Agregar(sFarmaciaNom, 3, 2);
        //    xpExcel.Agregar(sConcepto, 4, 2);
        //    xpExcel.Agregar(sCliente, 5, 3);
        //    xpExcel.Agregar(sSubCliente, 6, 3);

        //    //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
        //    xpExcel.Agregar(sFechaImpresion, 8, 3);

        //    leerExportarExcel.RegistroActual = 1;

        //    while (leerExportarExcel.Leer())
        //    {
        //        icol = 2;
        //        xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("FolioVenta"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Observaciones"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("FechaReceta"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("NumReceta"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdBeneficiario"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Beneficiario"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdPrograma"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Programa"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdSubPrograma"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("SubPrograma"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdPersonal"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("NombrePersonal"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCortaClave"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdProducto"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCorta"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("CodigoEAN"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("ClaveLote"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("FechaCaducidad"), iRenglon, icol++);
        //        //xpExcel.Agregar(leerExportarExcel.Campo("PrecioCosto_Unitario"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRenglon, icol++);
        //        //xpExcel.Agregar(leerExportarExcel.Campo("TasaIva"), iRenglon, icol++);
        //        //xpExcel.Agregar(leerExportarExcel.Campo("SubTotalLote"), iRenglon, icol++);
        //        //xpExcel.Agregar(leerExportarExcel.Campo("ImpteIvaLote"), iRenglon, icol++);
        //        //xpExcel.Agregar(leerExportarExcel.Campo("ImporteLote"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdPasillo"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdEstante"), iRenglon, icol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdEntrepaño"), iRenglon, icol++);
        //        iRenglon++;
        //    }

        //    // Finalizar el Proceso 
        //    xpExcel.CerrarDocumento();

        //}

        //private void ExportarNoSurtidoConcentrado()
        //{
        //    int iHoja = 1, iRenglon = 11;
        //    string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
        //    string sEstadoNom = DtGeneral.EstadoConectadoNombre;
        //    string sFarmaciaNom = sFarmacia + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
        //    string sConcepto = "REPORTE CONCENTRADO DE NO SURTIDO";
        //    string sFechaImpresion = General.FechaSistemaFecha.ToString();
        //    string sCliente = txtCte.Text + " -- " + lblCte.Text;
        //    string sSubCliente = txtSubCte.Text + " -- " + lblSubCte.Text;

        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(sEmpresaNom, 2, 2);
        //    xpExcel.Agregar(sFarmaciaNom, 3, 2);
        //    xpExcel.Agregar(sConcepto, 4, 2);
        //    xpExcel.Agregar(sCliente, 5, 3);
        //    xpExcel.Agregar(sSubCliente, 6, 3);

        //    //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
        //    xpExcel.Agregar(sFechaImpresion, 8, 3);

        //    leerExportarExcel.RegistroActual = 1;

        //    while (leerExportarExcel.Leer())
        //    {
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdClaveSSA_Base"), iRenglon, 2);
        //        xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA_Base"), iRenglon, 3);
        //        xpExcel.Agregar(leerExportarExcel.Campo("DescripcionClave_Base"), iRenglon, 4);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Presentacion_Base"), iRenglon, 5);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdClaveSSA"), iRenglon, 6);
        //        xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, 7);
        //        xpExcel.Agregar(leerExportarExcel.Campo("DescripcionClave"), iRenglon, 8);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Presentacion"), iRenglon, 9);
        //        xpExcel.Agregar(leerExportarExcel.Campo("CantidadSolicitada"), iRenglon, 10);
        //        xpExcel.Agregar(leerExportarExcel.Campo("CantidadDispensada"), iRenglon, 11);
        //        xpExcel.Agregar(leerExportarExcel.Campo("CantidadNoSurtida"), iRenglon, 12);
        //        //xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, 13);
        //        //xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCortaClave"), iRenglon, 14);
        //        //xpExcel.Agregar(leerExportarExcel.Campo("CantidadRequerida"), iRenglon, 15);
                
        //        iRenglon++;
        //    }

        //    // Finalizar el Proceso 
        //    xpExcel.CerrarDocumento();

        //    leerExportarExcel.DataTableClase = leerExportarExcel.Tabla(2);

        //}

        //private void ExportarNoSurtidoDetalle()
        //{
        //    int iHoja = 2, iCol = 11;
        //    string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
        //    string sEstadoNom = DtGeneral.EstadoConectadoNombre;
        //    string sFarmaciaNom = sFarmacia + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
        //    string sConcepto = "REPORTE DETALLADO DE NO SURTIDO";
        //    string sFechaImpresion = General.FechaSistemaFecha.ToString();
        //    string sCliente = txtCte.Text + " -- " + lblCte.Text;
        //    string sSubCliente = txtSubCte.Text + " -- " + lblSubCte.Text;

        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(sEmpresaNom, 2, 2);
        //    xpExcel.Agregar(sFarmaciaNom, 3, 2);
        //    xpExcel.Agregar(sConcepto, 4, 2);
        //    xpExcel.Agregar(sCliente, 5, 3);
        //    xpExcel.Agregar(sSubCliente, 6, 3);

        //    //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
        //    xpExcel.Agregar(sFechaImpresion, 8, 3);

        //    leerExportarExcel.RegistroActual = 1;

        //    for (int iRenglon = 11; leerExportarExcel.Leer(); iRenglon++)
        //    {
        //        iCol = 2;
        //        xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("FolioVenta"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("NumReceta"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdBeneficiario"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("NombreBeneficiario"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdPrograma"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Programa"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("IdSubPrograma"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("SubPrograma"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA_Base"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("DescripcionClave_Base"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Presentacion_Base"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("DescripcionClave"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Presentacion"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("ContenidoPaquete"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("CantidadSolicitada"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("CantidadDispensada"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("CantidadNoSurtida"), iRenglon, iCol++);
        //    }

        //    // Finalizar el Proceso 
        //    xpExcel.CerrarDocumento();

        //}

        //private void ExportarDemanda()
        //{
        //    int iHoja = 1, iRenglon = 11, iCol = 2;
        //    string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
        //    string sEstadoNom = DtGeneral.EstadoConectadoNombre;
        //    string sFarmaciaNom = sFarmacia + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
        //    string sConcepto = "REPORTE DE DEMANDA";
        //    string sFechaImpresion = General.FechaSistemaFecha.ToString();
        //    string sCliente = txtCte.Text + " -- " + lblCte.Text;
        //    string sSubCliente = txtSubCte.Text + " -- " + lblSubCte.Text;

        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(sEmpresaNom, 2, 2);
        //    xpExcel.Agregar(sFarmaciaNom, 3, 2);
        //    xpExcel.Agregar(sConcepto, 4, 2);
        //    xpExcel.Agregar(sCliente, 5, 3);
        //    xpExcel.Agregar(sSubCliente, 6, 3);

        //    //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
        //    xpExcel.Agregar(sFechaImpresion, 8, 3);

        //    leerExportarExcel.RegistroActual = 1;

        //    while (leerExportarExcel.Leer())
        //    {
        //        iCol = 2;
        //        xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Descripcion"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("CantidadNegada"), iRenglon, iCol++);
        //        xpExcel.Agregar(leerExportarExcel.Campo("Demanda"), iRenglon, iCol++);

        //        iRenglon++;
        //    }

        //    // Finalizar el Proceso 
        //    xpExcel.CerrarDocumento();

        //}
        #endregion Exportar_A_Excel   

    }
}
