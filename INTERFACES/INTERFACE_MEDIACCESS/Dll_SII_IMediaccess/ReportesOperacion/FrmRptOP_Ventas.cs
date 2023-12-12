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

namespace Dll_SII_IMediaccess.ReportesOperacion
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
        clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        clsDatosCliente DatosCliente;
        //Thread _workerThread;

        //bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        //bool bSeEjecuto = false; 

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sIdPublicoGral = GnFarmacia.PublicoGral;

        public FrmRptOP_Ventas()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmRptOP_Ventas");
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;            

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
            IniciaToolBar(true, true);
            IniciaFrames(true);
            rdoSalidas.Checked = false;
            rdoDevoluciones.Checked = false;
            lst.LimpiarItems();
            txtEstado.Focus();
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
                GenerarExcel();
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
            string sSql = "", sWhereEstadoFarmacia = "", sWhereCliente = "", sWhereSubCliente = "", sWherePrograma = "", sWhereSubPrograma = "", sWhereBeneficiario = "";

            IniciaToolBar(false, false);
            IniciaFrames(false);
            btnNuevo.Enabled = false;

            if (txtEstado.Text.Trim() != "")
            {
                sWhereEstadoFarmacia = string.Format(" And N.IdEstado = '{0}'", txtEstado.Text);
            }

            if (txtFarmacia.Text.Trim() != "")
            {
                sWhereEstadoFarmacia += string.Format(" And N.IdFarmacia = '{0}'", txtFarmacia.Text);
            }

            if (txtCte.Text.Trim() != "")
            {
                sWhereCliente = string.Format(" and IdCliente = '{0}' ", Fg.PonCeros(txtCte.Text, 4));
            }

            if (txtSubCte.Text.Trim() != "")
            {
                sWhereSubCliente = string.Format(" and IdSubCliente = '{0}' ", Fg.PonCeros(txtSubCte.Text, 4));
            }

            if (txtPro.Text.Trim() != "")
            {
                sWherePrograma = string.Format(" and IdPrograma = '{0}' ", Fg.PonCeros(txtPro.Text, 4));
            }

            if (txtSubPro.Text.Trim() != "")
            {
                sWhereSubPrograma = string.Format(" and IdSubPrograma = '{0}' ", Fg.PonCeros(txtSubPro.Text, 4));
            }

            if (rdoSalidas.Checked || rdoDemanda.Checked)
            {
                sSql = string.Format(" Select N.Farmacia, 'Fecha Salida' = Convert(varchar(10), FechaRegistro, 120), 'Folio Salida' = Folio, " +
	                                " 'Núm. Beneficiario' = IdBeneficiario, Beneficiario, 'Núm. Programa' = IdPrograma, Programa, " +
	                                " 'Núm. Sub-Programa' = IdSubPrograma, 'Sub-Programa' = SubPrograma " +
                                    " From vw_Impresion_Ventas_Credito N (Nolock) " +
                                    " Where IdEmpresa = '{0}' {1} {2} {3} " +
                                    " and Convert(varchar(10), FechaRegistro, 120) Between '{4}' and '{5}'  {6} {7} {8} " +
                                     " Group By N.Farmacia, FechaRegistro, Folio, IdBeneficiario, Beneficiario, " +
                                    " IdPrograma, Programa, IdSubPrograma, SubPrograma  Order By N.Farmacia, Folio, FechaRegistro ",
                                    sEmpresa, sWhereEstadoFarmacia, sWhereCliente, sWhereSubCliente, General.FechaYMD(dtpFechaInicial.Value, "-"), 
                                    General.FechaYMD(dtpFechaFinal.Value, "-"), sWherePrograma, sWhereSubPrograma, sWhereBeneficiario);
            }

            if (rdoDevoluciones.Checked)
            {
                sSql = string.Format(" Select N.Farmacia, 'Fecha Devolución' = Convert(varchar(10), FechaRegistro, 120), 'Folio Devolución' = Folio, 'Folio Salida' = FolioVenta,  " +
	                                " 'Núm. Beneficiario' = IdBeneficiario, Beneficiario, 'Núm. Programa' = IdPrograma, Programa,  " +
	                                " 'Núm. Sub-Programa' = IdSubPrograma, 'Sub-Programa' = SubPrograma " +
                                    " From vw_Impresion_Devolucion_Ventas_Credito N (Nolock) " +
                                    " Where IdEmpresa = '{0}' {1} {2} {3} " +
                                    " and Convert(varchar(10), FechaRegistro, 120) Between '{4}' and '{5}' {6} {7} {8} " +
                                    " Group By N.Farmacia, FechaRegistro, Folio, FolioVenta, IdBeneficiario, Beneficiario, " +
                                    " IdPrograma, Programa, IdSubPrograma, SubPrograma  Order By N.Farmacia, Folio, FechaRegistro ",
                                    sEmpresa, sWhereEstadoFarmacia, sWhereCliente, sWhereSubCliente, General.FechaYMD(dtpFechaInicial.Value, "-"),
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


                sSql = string.Format(" Select N.Farmacia, 'Fecha No Surtido' = Convert(varchar(10), N.FechaRegistro, 120), 'Folio No Surtido' = N.Folio, " +
		                        " 'Núm. Beneficiario' = V.IdBeneficiario, V.Beneficiario, 'Núm. Programa' = V.IdPrograma, V.Programa, " + 
		                        " 'Núm. Sub-Programa' = V.IdSubPrograma, 'Sub-Programa' = V.SubPrograma " + 
		                        " From  vw_Impresion_Ventas_ClavesSolicitadas N (Nolock) " +
		                        " Inner Join vw_Impresion_Ventas_Credito V (Nolock) " +
			                        " On ( V.IdEmpresa = N.IdEmpresa and V.IdEstado = N.IdEstado and V.IdFarmacia = N.IdFarmacia and V.Folio = N.Folio ) " +
		                        " Where N.IdEmpresa = '{0}' {1} {2} {3}  " +
		                        " and Convert(varchar(10), N.FechaRegistro, 120) Between '{4}' and '{5}'  " +
		                        " and N.EsCapturada = 1  {6} {7} {8}  " +
                                " Group By N.Farmacia, N.FechaRegistro, N.Folio, V.IdBeneficiario, V.Beneficiario, V.IdPrograma, V.Programa, V.IdSubPrograma, V.SubPrograma " +
                                " Order By N.Farmacia, N.Folio, N.FechaRegistro  ",
                                sEmpresa, sWhereEstadoFarmacia, sWhereCliente, sWhereSubCliente, General.FechaYMD(dtpFechaInicial.Value, "-"),
                                General.FechaYMD(dtpFechaFinal.Value, "-"), sWherePrograma, sWhereSubPrograma, sWhereBeneficiario);


            }

            
            leerExportarExcel = new clsLeer(ref cnn);

            bSeEncontroInformacion = false;

            this.Cursor = Cursors.Default;

            int iTipoReporte = 1;
            iTipoReporte = rdoDevoluciones.Checked ? 2 : iTipoReporte;
            iTipoReporte = rdoNoSurtido.Checked ? 3 : iTipoReporte;
            iTipoReporte = rdoDemanda.Checked ? 4 : iTipoReporte;

            sSql = string.Format("Exec spp_Rpt_OP_Ventas___EPharma @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdCliente = '{3}', @IdSubCliente = '{4}', " +
                "@IdPrograma = '{5}', @IdSubPrograma = '{6}', @FechaIncial = '{7}', @FechaFinal = '{8}', @IdBeneficiario = '{9}',  @EsReporte = {10}, @TipoReporte = {11} ",
                DtGeneral.EmpresaConectada, txtEstado.Text, txtFarmacia.Text,
                txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), txtSubPro.Text.Trim(),
                General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"),
                "", 1, iTipoReporte);

            lst.LimpiarItems();

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

            //if (txtCte.Text.Trim() == "")
            //{
            //    bRegresa = false;
            //    General.msjAviso("No ha capturado el Cliente.. Verifique..");
            //    txtCte.Focus();
            //}

            //if (bRegresa && txtSubCte.Text.Trim() == "")
            //{
            //    bRegresa = false;
            //    General.msjAviso("No ha capturado el Sub-Cliente.. Verifique..");
            //    txtSubCte.Focus();
            //}

            return bRegresa;
        }

        private void CargarDetalles_Salidas()
        {
            string sSql = "";
            int iTipoReporte = 1;
            leerExportarExcel = new clsLeer(ref cnn);

            bSeEncontroInformacion = false;

            this.Cursor = Cursors.Default;


            iTipoReporte = rdoDevoluciones.Checked ? 2 : iTipoReporte;
            iTipoReporte = rdoNoSurtido.Checked ? 3 : iTipoReporte;
            iTipoReporte = rdoDemanda.Checked ? 4 : iTipoReporte;

            sSql = string.Format("Exec spp_Rpt_OP_Ventas___EPharma @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdCliente = '{3}', @IdSubCliente = '{4}', " +
                "@IdPrograma = '{5}', @IdSubPrograma = '{6}', @FechaIncial = '{7}', @FechaFinal = '{8}', @IdBeneficiario = '{9}',  @EsReporte = {10}, @TipoReporte = {11} ",
                DtGeneral.EmpresaConectada, txtEstado.Text, txtFarmacia.Text,
                txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), txtSubPro.Text.Trim(),
                General.FechaYMD(dtpFechaInicial.Value, "-"),  General.FechaYMD(dtpFechaFinal.Value, "-"),
                "", 0, iTipoReporte);

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
            if (txtFarmacia.Text.Trim() != "")
            {
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
                }
                else
                {
                    leer2.DataSetClase = consultas.Farmacia_Clientes(sIdPublicoGral, txtEstado.Text.Trim(), txtFarmacia.Text.Trim(), txtCte.Text, "txtCte_Validating");
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
            else
            {
                General.msjUser("Seleccione primero Estado-Farmacia");
            }
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtFarmacia.Text.Trim() != "")
            {
                if (e.KeyCode == Keys.F1)
                {
                    leer = new clsLeer(ref cnn);

                    leer.DataSetClase = ayuda.Farmacia_Clientes(sIdPublicoGral, false, txtEstado.Text.Trim(), txtFarmacia.Text.Trim(), "txtCte_KeyDown");
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
            else
            {
                General.msjUser("Seleccione primero Estado-Farmacia");
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
            if (txtFarmacia.Text.Trim() != "")
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

                    leer2.DataSetClase = consultas.Farmacia_Clientes(sIdPublicoGral, txtEstado.Text.Trim(), txtFarmacia.Text.Trim(), txtCte.Text, txtSubCte.Text, "txtSubCte_Validating");
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
            else
            {
                General.msjUser("Seleccione primero Estado-Farmacia");
            }
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtFarmacia.Text.Trim() != "")
            {
                if (e.KeyCode == Keys.F1)
                {
                    if (txtCte.Text.Trim() != "")
                    {
                        leer = new clsLeer(ref cnn);

                        leer.DataSetClase = ayuda.Farmacia_Clientes(sIdPublicoGral, false, txtEstado.Text.Trim(), txtFarmacia.Text.Trim(), txtCte.Text, "txtSubCte_KeyDown");
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
            else
            {
                General.msjUser("Seleccione primero Estado-Farmacia");
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

                leer2.DataSetClase = consultas.Farmacia_Clientes_Programas(sIdPublicoGral, txtEstado.Text.Trim(), txtFarmacia.Text.Trim(), txtCte.Text, txtSubCte.Text, txtPro.Text, "txtPro_Validating");
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

                    leer.DataSetClase = ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, txtEstado.Text.Trim(), txtFarmacia.Text.Trim(), txtCte.Text, txtSubCte.Text, "txtPro_KeyDown");
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

                leer2.DataSetClase = consultas.Farmacia_Clientes_Programas(sIdPublicoGral, txtEstado.Text.Trim(), txtFarmacia.Text.Trim(), txtCte.Text, txtSubCte.Text, txtPro.Text, txtSubPro.Text, "txtSubPro_Validating");
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

                    leer.DataSetClase = ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, txtEstado.Text.Trim(), txtFarmacia.Text.Trim(), txtCte.Text, txtSubCte.Text, txtPro.Text, "txtPro_KeyDown");
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

        #region Eventos_Estado
        private void txtEstado_Validating(object sender, CancelEventArgs e)
        {
            if (txtEstado.Text.Trim() != "")
            {
                leer.DataSetClase = consultas.Estados(txtEstado.Text, "txtEstado_Validating");
                if (leer.Leer())
                {
                    txtEstado.Text = leer.Campo("IdEstado");
                    lblEstado.Text = leer.Campo("Nombre");
                    txtEstado.Enabled = false;
                }
                else
                {
                    txtEstado.Focus();
                }
            }
        }

        private void txtEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Estados("txtEstado_KeyDown()");
                if (leer.Leer())
                {
                    txtEstado.Text = leer.Campo("IdEstado");
                    lblEstado.Text = leer.Campo("Nombre");
                    txtEstado.Enabled = false;
                }
            }
        }

        private void txtEstado_TextChanged(object sender, EventArgs e)
        {
            lblEstado.Text = "";
            txtFarmacia.Text = "";
        }

        private void txtFarmacia_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtEstado.Text.Trim() != "")
            {
                if (e.KeyCode == Keys.F1)
                {
                    leer.DataSetClase = ayuda.Farmacias("txtEstado_KeyDown()", txtEstado.Text.Trim());
                    if (leer.Leer())
                    {
                        txtFarmacia.Text = leer.Campo("IdFarmacia");
                        lblFarmacia.Text = leer.Campo("Farmacia");
                        txtFarmacia.Enabled = false;
                    }
                }
            }
        }

        private void txtFarmacia_Validating(object sender, CancelEventArgs e)
        {
            if (txtEstado.Text.Trim() != "" || txtFarmacia.Text.Trim() != "")
            {
                leer.DataSetClase = consultas.Farmacias(txtEstado.Text, txtFarmacia.Text, "txtEstado_Validating");
                if (leer.Leer())
                {
                    txtFarmacia.Text = leer.Campo("IdFarmacia");
                    lblFarmacia.Text = leer.Campo("Farmacia");
                    txtFarmacia.Enabled = false;
                }
                else
                {
                    txtFarmacia.Focus();
                }
            }
        }

        private void txtFarmacia_TextChanged(object sender, EventArgs e)
        {
            lblFarmacia.Text = "";
            txtCte.Text = "";
        }

        #endregion Eventos_Estado

        #region Exportar_A_Excel
        private void GenerarExcel()
        {
            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_OP_Ventas_Devoluciones___EPharma.xls";
            this.Cursor = Cursors.WaitCursor;
            bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_OP_Ventas_Devoluciones___EPharma.xls", DatosCliente);

            if (!bRegresa)
            {
                this.Cursor = Cursors.Default;
                General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            }
            else
            {
                xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                xpExcel.AgregarMarcaDeTiempo = true;
                //leer.DataSetClase = dtsExistencias;

                this.Cursor = Cursors.Default;
                if (xpExcel.PrepararPlantilla())
                {
                    IniciaToolBar(false, false);
                    this.Cursor = Cursors.WaitCursor;
                    if (rdoSalidas.Checked)
                    {
                        xpExcel.EliminarHoja("DEVOLUCIONES");
                        xpExcel.EliminarHoja("NOSURTIDO");
                        xpExcel.EliminarHoja("DEMANDA");
                        ExportarSalidas();
                    }

                    if (rdoDevoluciones.Checked)
                    {
                        xpExcel.EliminarHoja("SALIDAS");
                        xpExcel.EliminarHoja("NOSURTIDO");
                        xpExcel.EliminarHoja("DEMANDA");
                        xpExcel.EliminarHoja("TiposDePago");
                        ExportarDevoluciones();
                    }

                    if (rdoNoSurtido.Checked)
                    {
                        xpExcel.EliminarHoja("SALIDAS");
                        xpExcel.EliminarHoja("DEVOLUCIONES");
                        xpExcel.EliminarHoja("DEMANDA");
                        xpExcel.EliminarHoja("TiposDePago");
                        ExportarNoSurtido();
                    }

                    if (rdoDemanda.Checked)
                    {
                        xpExcel.EliminarHoja("SALIDAS");
                        xpExcel.EliminarHoja("DEVOLUCIONES");
                        xpExcel.EliminarHoja("NOSURTIDO");
                        xpExcel.EliminarHoja("TiposDePago");
                        ExportarDemanda();
                    }

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }

                    IniciaToolBar(true, true);
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void ExportarSalidas()
        {
            int iHoja = 1, iCol = 2;
            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            //string sEstadoNom = lblEstado.Text;
            //string sFarmaciaNom = txtFarmacia.Text.Trim() + " -- " + lblFarmacia.Text + ", " + sEstadoNom;
            string sConcepto = "REPORTE DETALLADO DE SALIDAS DE DISPENSACION";
            string sFechaImpresion = General.FechaSistemaFecha.ToString();
            //string sCliente = txtCte.Text + " -- " + lblCte.Text;
            //string sSubCliente = txtSubCte.Text + " -- " + lblSubCte.Text;

            xpExcel.GeneraExcel(iHoja++);

            xpExcel.Agregar(sEmpresaNom, 2, 2);
            //xpExcel.Agregar(sFarmaciaNom, 3, 2);
            xpExcel.Agregar(sConcepto, 3, 2);
            //xpExcel.Agregar(sCliente, 5, 3);
            //xpExcel.Agregar(sSubCliente, 6, 3);

            //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            xpExcel.Agregar(sFechaImpresion, 5, 3);

            leerExportarExcel.RegistroActual = 1;

            for (int iRenglon = 8;leerExportarExcel.Leer();iRenglon++)
            {
                iCol = 2;
                
                xpExcel.Agregar(leerExportarExcel.Campo("IdEstado"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Estado"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdFarmacia"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdCliente"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("NombreCliente"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdSubCliente"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("NombreSubCliente"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdPrograma"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Programa"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdSubPrograma"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("SubPrograma"), iRenglon, iCol++);

                xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("RefObservaciones"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("NumReceta"), iRenglon, iCol++);

                xpExcel.Agregar(leerExportarExcel.Campo("Documento"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdMedico"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("NombreMedico"), iRenglon, iCol++);

                xpExcel.Agregar(leerExportarExcel.Campo("IdBeneficiario"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Beneficiario"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdPersonal"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("NombrePersonal"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCortaClave"), iRenglon, iCol++);

                //xpExcel.Agregar(leerExportarExcel.Campo("IdFamilia"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Familia"), iRenglon, iCol++);
                //xpExcel.Agregar(leerExportarExcel.Campo("IdGrupoTerapeutico"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("GrupoTerapeutico"), iRenglon, iCol++);

                xpExcel.Agregar(leerExportarExcel.Campo("IdProducto"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCorta"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("CodigoEAN"), iRenglon, iCol++);

                xpExcel.Agregar(leerExportarExcel.Campo("IdSubFarmacia"), iRenglon, iCol++);

                xpExcel.Agregar(leerExportarExcel.Campo("ClaveLote"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("FechaCad"), iRenglon, iCol++);
                
                xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRenglon, iCol++);

                xpExcel.Agregar(leerExportarExcel.CampoDouble("Porcentaje_Paciente"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.CampoDouble("Porcentaje_Cliente"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.CampoDouble("Importe_Paciente"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.CampoDouble("Importe_Cliente"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.CampoDouble("Importe_Total_SinIVA"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.CampoDouble("TasaIVA"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.CampoDouble("IVA_Paciente"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.CampoDouble("IVA_Cliente"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.CampoDouble("Importe_Paciente_con_IVA"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.CampoDouble("Importe_Cliente_con_IVA"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.CampoDouble("Importe_Total_con_IVA"), iRenglon, iCol++);


            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();

            leerExportarExcel.DataTableClase = leerExportarExcel.Tabla(2);


            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(sEmpresaNom, 2, 2);
            //xpExcel.Agregar(sFarmaciaNom, 3, 2);
            xpExcel.Agregar(sConcepto, 3, 2);
            //xpExcel.Agregar(sCliente, 5, 3);
            //xpExcel.Agregar(sSubCliente, 6, 3);

            //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            xpExcel.Agregar(sFechaImpresion, 5, 3);

            leerExportarExcel.RegistroActual = 1;

            for (int iRenglon = 8; leerExportarExcel.Leer(); iRenglon++)
            {
                iCol = 2;

                xpExcel.Agregar(leerExportarExcel.Campo("IdEstado"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Estado"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdFarmacia"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("FolioVenta"), iRenglon, iCol++);

                xpExcel.Agregar(leerExportarExcel.Campo("IdFormasdePago"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("FormasdePago"), iRenglon, iCol++);

 
                xpExcel.Agregar(leerExportarExcel.CampoDouble("Importe"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.CampoDouble("PagoCon"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.CampoDouble("Cambio"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Referencia"), iRenglon, iCol++);
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();

        }

        private void ExportarDevoluciones()
        {
            int iHoja = 1, icol = 2;
            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            //string sEstadoNom = lblEstado.Text;
            //string sFarmaciaNom = txtFarmacia.Text.Trim() + " -- " + lblFarmacia.Text + ", " + sEstadoNom;
            string sConcepto = "REPORTE DETALLADO DE DEVOLUCIONES DE SALIDAS DE DISPENSACION";
            string sFechaImpresion = General.FechaSistemaFecha.ToString();
            //string sCliente = txtCte.Text + " -- " + lblCte.Text;
            //string sSubCliente = txtSubCte.Text + " -- " + lblSubCte.Text;

            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(sEmpresaNom, 2, 2);
            //xpExcel.Agregar(sFarmaciaNom, 3, 2);
            xpExcel.Agregar(sConcepto, 3, 2);
            //xpExcel.Agregar(sCliente, 5, 3);
            //xpExcel.Agregar(sSubCliente, 6, 3);

            //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            xpExcel.Agregar(sFechaImpresion, 5, 3);

            leerExportarExcel.RegistroActual = 1;

            for (int iRenglon = 8; leerExportarExcel.Leer(); iRenglon++)
            {
                icol = 2;
                xpExcel.Agregar(leerExportarExcel.Campo("IdEstado"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Estado"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdFarmacia"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdCliente"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("NombreCliente"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdSubCliente"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("NombreSubCliente"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdPrograma"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Programa"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdSubPrograma"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("SubPrograma"), iRenglon, icol++);

                xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("FolioVenta"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("FechaReceta"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("NumReceta"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdBeneficiario"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Beneficiario"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdPersonal"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("NombrePersonal"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCortaClave"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdProducto"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCorta"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("CodigoEAN"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("ClaveLote"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("FechaCaducidad"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Cant_Devuelta"), iRenglon, icol++);

            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();

        }

        private void ExportarNoSurtido()
        {
            int iHoja = 1, icol = 2;
            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            //string sEstadoNom = lblEstado.Text;
            //string sFarmaciaNom = txtFarmacia.Text.Trim() + " -- " + lblFarmacia.Text + ", " + sEstadoNom;
            string sConcepto = "REPORTE DETALLADO DE NO SURTIDO";
            string sFechaImpresion = General.FechaSistemaFecha.ToString();
            //string sCliente = txtCte.Text + " -- " + lblCte.Text;
            //string sSubCliente = txtSubCte.Text + " -- " + lblSubCte.Text;

            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(sEmpresaNom, 2, 2);
            //xpExcel.Agregar(sFarmaciaNom, 3, 2);
            xpExcel.Agregar(sConcepto, 3, 2);
            //xpExcel.Agregar(sCliente, 5, 3);
            //xpExcel.Agregar(sSubCliente, 6, 3);

            //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            xpExcel.Agregar(sFechaImpresion, 5, 3);

            leerExportarExcel.RegistroActual = 1;

            for (int iRenglon = 8; leerExportarExcel.Leer(); iRenglon++)
            {
                icol = 2;

                xpExcel.Agregar(leerExportarExcel.Campo("IdEstado"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Estado"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdFarmacia"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRenglon, icol++);

                xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("NumReceta"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdBeneficiario"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Beneficiario"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdPrograma"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Programa"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdSubPrograma"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("SubPrograma"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdPersonal"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("NombrePersonal"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCortaClave"), iRenglon, icol++);
                xpExcel.Agregar(leerExportarExcel.Campo("CantidadRequerida"), iRenglon, icol++);
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();

        }

        private void ExportarDemanda()
        {
            int iHoja = 1, iCol = 2;
            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            //string sEstadoNom = lblEstado.Text;
            //string sFarmaciaNom = txtFarmacia.Text.Trim() + " -- " + lblFarmacia.Text + ", " + sEstadoNom;
            string sConcepto = "REPORTE DE DEMANDA";
            string sFechaImpresion = General.FechaSistemaFecha.ToString();
            //string sCliente = txtCte.Text + " -- " + lblCte.Text;
            //string sSubCliente = txtSubCte.Text + " -- " + lblSubCte.Text;

            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(sEmpresaNom, 2, 2);
            //xpExcel.Agregar(sFarmaciaNom, 3, 2);
            xpExcel.Agregar(sConcepto, 3, 2);
            //xpExcel.Agregar(sCliente, 5, 3);
            //xpExcel.Agregar(sSubCliente, 6, 3);

            //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            xpExcel.Agregar(sFechaImpresion, 5, 3);

            leerExportarExcel.RegistroActual = 1;

            for (int iRenglon = 8; leerExportarExcel.Leer(); iRenglon++)
            {
                iCol = 2;
                xpExcel.Agregar(leerExportarExcel.Campo("IdEstado"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Estado"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdFarmacia"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRenglon, iCol++);

                xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Descripcion"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("CantidadNegada"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Demanda"), iRenglon, iCol++);
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();

        }
        #endregion Exportar_A_Excel
    }
}
