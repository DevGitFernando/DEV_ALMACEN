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
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;

using Dll_IFacturacion;
using Dll_IFacturacion.XSA;

namespace Facturacion.Facturar
{
    public partial class FrmFacturar_Concentrado : FrmBaseExt
    {
        enum Cols
        {
            Ninguna = 0, 
            FolioRemision = 1, 
            FechaDeRemisionado, 
            ImporteRemision, 
            FechaInicial_Proceso, 
            FechaFinal_Proceso, 
            Farmacia, 
            TipoDeRemision, 
            Referencia_01, 
            Referencia_02, 
            Facturar 
        }


        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;

        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;
        string sIdPersonal = DtGeneral.IdPersonal;

        string sIdCliente = "";
        string sIdSubCliente = "";

        bool bSeGeneroFolioFacturaElectronica = false;
        string sFolioFacturaElectronica = "";
        double dSubTotalSinGrabar = 0;
        double dSubTotalGrabado = 0;
        double dIva = 0;
        double dTotal = 0;
        bool bRemisionFacturada = false;
        bool bEsFacturable = false;
        string sListaDeRemisiones_A_Facturar = ""; 
        eTipoRemision tipoRemision = eTipoRemision.Ninguno;

        double dCERO = 0;
        string sFormato = "#,###,###,###,##0.###0";
        string sFormato_Decimal = "#,###,###,###,##0";

        string sListaRemisiones_Excel = "";
        int iRemisiones_Seleccionadas = 0;
        double dImporte_Remisiones = 0;

        #region Parametros 
        int i_01_TipoDeRemision = 0; // rdoRM_Producto.Checked ? 1 : 2;
        int i_01_TipoDeRemision_Complemento = 0; //chkRM_Complemento.Checked ? 1 : 0;
        int i_02_OrigenInsumos = 0; //rdoOIN_Venta.Checked ? 1 : 2;
        int i_03_TipoDeInsumos = 0; //rdoInsumoMedicamento.Checked ? 1 : 2;
        int i_04_TipoDispensacion = 0; //rdoTipoDispensacion_01_Dispensacion.Checked ? 1 : 2;
        int i_05_BaseRemision = 0; //rdoBaseRemision_Normal.Checked ? 1 : 2;
        int i_06_FiltroFolios = 0; //chkFiltro_Folios.Checked ? 1 : 0;
        int i_07_Filtro_FechaPeriodoRemisionado = 0; //chkFiltro_PeriodoRemisionado.Checked ? 1 : 0;
        int i_08_Filtro_FechaEmisionRemision = 0; //chkFiltro_FechaRemisionado.Checked ? 1 : 0;
        string s_09_Referencia_01 = ""; //cboReferencias_01.SelectedIndex == 0 ? "" : cboReferencias_01.Data;
        string s_10_Referencia_02 = ""; //cboReferencias_02.SelectedIndex == 0 ? "" : cboReferencias_02.Data;
        int i_11_PartidaGeneral = 0; //Convert.ToInt32(nmPartidaGeneral.Value);
        DateTime d_12_FechaInicial_PeriodoRemisionado = DateTime.Now; //dtpFechaPeriodoRemisionado_Inicial.Value;
        DateTime d_13_FechaFinal_PeriodoRemisionado = DateTime.Now; //dtpFechaPeriodoRemisionado_Final.Value;
        DateTime d_14_FechaInicial_FechaRemision = DateTime.Now; //dtpFechaRemision_Inicial.Value;
        DateTime d_15_FechaFinal_FechaRemision = DateTime.Now; //dtpFechaRemision_Final.Value;
        string s_16_FolioInicial = ""; //txtFolioInicial.Text;
        string s_17_FolioFinal = ""; //txtFolioFinal.Text;
        #endregion Parametros 

        public FrmFacturar_Concentrado()
        {
            InitializeComponent();

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);


            grid = new clsGrid(ref grdRemisiones, this);
            grid.BackColorColsBlk = Color.White;
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            grid.AjustarAnchoColumnasAutomatico = true;
            grid.SetOrder(true); 

            this.FrameProceso.Height = 50;
            this.FrameProceso.Top = 170;
            this.FrameProceso.Left = 230;

            General.Pantalla.AjustarAlto(this, 80); 

        }

        #region Form 
        private void FrmFacturar_Concentrado_Load( object sender, EventArgs e )
        {
            InicialiarPantalla();
        }

        private void IniciarlizarReferencias()
        {
            cboReferencias_01.Clear();
            cboReferencias_01.Add();
            cboReferencias_01.SelectedIndex = 0;

            cboReferencias_02.Clear();
            cboReferencias_02.Add();
            cboReferencias_02.SelectedIndex = 0;
        }
        #endregion Form 

        #region Botones 
        private void InicializarToolBar()
        {
            InicializarToolBar(false, false);
        }

        private void InicializarToolBar( bool Ejecutar, bool Facturar )
        {
            btnEjecutar.Enabled = Ejecutar;
            btnFacturar.Enabled = Facturar;
        }

        private void InicialiarPantalla()
        {
            sListaRemisiones_Excel = "";
            sListaDeRemisiones_A_Facturar = "";
            lblArchivo.Text = "...";
            lblNumeroDeRemisiones.Text = "";

            cboHojas.Items.Clear();
            cboHojas.Items.Add("<< Seleccionar >>");
            cboHojas.SelectedIndex = 0;


            Fg.IniciaControles();
            grid.Limpiar(true);
            IniciarlizarReferencias();
            IniciaToolBarExcel(true, true, true, true, true); 

            InicializarToolBar();
            MostrarEnProceso(false);


            rdoRM_Producto.Checked = true;
            rdoOIN_Venta.Checked = true;
            rdoInsumoMedicamento.Checked = true; 
            rdoTipoDispensacion_01_Dispensacion.Checked = true;
            rdoBaseRemision_Normal.Checked = true;
            chkFiltro_PeriodoRemisionado.Checked = true;
            chkFiltro_FechaRemisionado.Checked = false;

            rdoInsumo_Ambos.Enabled = false; 

            lblTotal.Text = dCERO.ToString(sFormato);
            lblTotal_Seleccionado.Text = dCERO.ToString(sFormato);
            lblRemisiones_Seleccionadas.Text = dCERO.ToString(sFormato_Decimal);


            txtFinanciamiento.Focus();
        }

        private void btnNuevo_Click( object sender, EventArgs e )
        {
            InicialiarPantalla();
        }

        private void btnEjecutar_Click( object sender, EventArgs e )
        {
            if(validarDatos())
            {
                Obtener_Remisiones(true);
            }
        }

        private void btnFacturar_Click( object sender, EventArgs e )
        {
            sListaDeRemisiones_A_Facturar = GetListaDeRemisiones();

            if(sListaDeRemisiones_A_Facturar == "")
            {
                General.msjAviso("No se ha seleccionado Remisiones para generar la Factura.");
            }
            else
            {
                GenerarFacturaElectronica();
            }
        }

        private void btnNuevoExcel_Click( object sender, EventArgs e )
        {
            sListaRemisiones_Excel = "";
            sListaDeRemisiones_A_Facturar = "";
            lblArchivo.Text = "...";
            lblNumeroDeRemisiones.Text = ""; 

            cboHojas.Items.Clear();
            cboHojas.Items.Add("<< Seleccionar >>");
            cboHojas.SelectedIndex = 0;

            IniciarEtiquetasExcel();
            IniciaToolBarExcel(true, true, true, true, true);
        }

        private void btnAbrirExcel_Click( object sender, EventArgs e )
        {
            openExcel.Title = "Archivos concentrados de remisiones";
            openExcel.Filter = "Archivos de Excel (*.xls;*.xlsx)| *.xls;*.xlsx";
            //openExcel.Filter = "Archivos de Texto (*.txt)| *.txt"; 
            openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openExcel.AddExtension = true;
            //lblProcesados.Visible = false;

            // if (openExcel.FileName != "")
            if(openExcel.ShowDialog() == DialogResult.OK)
            {
                sFile_In = openExcel.FileName;
                lblArchivo.Text = openExcel.FileName;

                cboHojas.Items.Clear();
                cboHojas.Items.Add("<< Seleccione >>");
                cboHojas.SelectedIndex = 0;
                this.Refresh();

                //IniciaToolBarExcel(false, false, false, false, false);
                thReadFile = new Thread(this.CargarArchivo);
                thReadFile.Name = "LeerArchivo";
                thReadFile.Start();
            }
        }

        private void btnLeerHoja_Click( object sender, EventArgs e )
        {
            IniciaToolBarExcel(false, false, false, false, false);
            thLoadFile = new Thread(this.thLeerHoja);
            thLoadFile.Name = "LeerDocumentoExcel";
            thLoadFile.Start();
        }
        #endregion Botones 

        #region Informacion de Fuente de Financiamiento 
        private void txtFinanciamiento_TextChanged( object sender, EventArgs e )
        {
            lblFinanciamiento.Text = "";
            lblFuentesFinanciamiento.Text = "";

        }

        private void txtFinanciamiento_KeyDown( object sender, KeyEventArgs e )
        {
            if(e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Encabezado("txtFinanciamiento_KeyDown");
                if(leer.Leer())
                {
                    CargarInformacion_Financiamiento();
                }
            }
        }

        private void txtFinanciamiento_Validating( object sender, CancelEventArgs e )
        {
            if(txtFinanciamiento.Text != "")
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Encabezado(txtFinanciamiento.Text.Trim(), "txtFinanciamiento_Validating");
                if(leer.Leer())
                {
                    CargarInformacion_Financiamiento();
                }
            }
        }

        private void CargarInformacion_Financiamiento()
        {
            txtFinanciamiento.Enabled = false;
            txtFinanciamiento.Text = leer.Campo("IdFuenteFinanciamiento");
            lblFinanciamiento.Text = leer.Campo("Estado") + " -- " + leer.Campo("NumeroDeContrato");


            if(leer.Campo("Status") == "C")
            {
                General.msjUser("La Configuración seleccionada se encuentra cancelada, verifique.");
                txtFinanciamiento.Text = "";
                txtFinanciamiento.Enabled = true;
            }
        }

        private void CargarInformacion_FuenteDeFinanciamiento()
        {
            txtFuenteDeFinanciamiento.Enabled = false;
            txtFuenteDeFinanciamiento.Text = leer.Campo("IdFinanciamiento");
            lblFuentesFinanciamiento.Text = leer.Campo("Financiamiento");
            lblCliente.Text = string.Format("{0}  -  {1}", leer.Campo("Cliente"), leer.Campo("SubCliente"));
            //lblSubCliente.Text = leer.Campo("SubCliente");

            lblNumeroDeContrato.Text = leer.Campo("NumeroDeContrato");
            lblNumeroDeLicitacion.Text = leer.Campo("NumeroDeLicitacion");
            lblVigencia.Text = string.Format("Del       {0}     al      {1} ", General.FechaYMD(leer.CampoFecha("FechaInicial")), General.FechaYMD(leer.CampoFecha("FechaFinal")));

            sIdCliente = leer.Campo("IdCliente");
            sIdSubCliente = leer.Campo("IdSubCliente"); 

            // FechaInicial, FechaFinal 

            InicializarToolBar(true, false);
            if(leer.Campo("Status") == "C")
            {
                InicializarToolBar(false, false);
                General.msjUser("La Fuente de Financiamiento seleccionada se encuentra cancelada, verifique.");
                txtFuenteDeFinanciamiento.Text = "";
            }

            if(btnEjecutar.Enabled)
            {
                Cargar_Referencias_01();
                Cargar_Referencias_02();
            }
        }

        private void txtFuenteDeFinanciamiento_TextChanged( object sender, EventArgs e )
        {
            lblFuentesFinanciamiento.Text = "";
            lblCliente.Text = "";
            //lblSubCliente.Text = "";
            lblNumeroDeContrato.Text = "";
            lblNumeroDeLicitacion.Text = "";
            lblVigencia.Text = "";
        }

        private void txtFuenteDeFinanciamiento_KeyDown( object sender, KeyEventArgs e )
        {
            if(e.KeyCode == Keys.F1 && txtFinanciamiento.Text != "")
            {
                leer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Detalle("txtFinanciamiento_KeyDown", txtFinanciamiento.Text);
                if(leer.Leer())
                {
                    CargarInformacion_FuenteDeFinanciamiento();
                }
            }
        }

        private void txtFuenteDeFinanciamiento_Validating( object sender, CancelEventArgs e )
        {
            //myLeer.DataSetClase = Consultas.FuentesDeFinanciamiento_Detalle(txtRubro.Text.Trim(), txtConcepto.Text.Trim(), "txtRubro_Validating");

            if(txtFinanciamiento.Text != "" && txtFuenteDeFinanciamiento.Text != "")
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Detalle(txtFinanciamiento.Text.Trim(), txtFuenteDeFinanciamiento.Text, "txtFinanciamiento_Validating");
                if(leer.Leer())
                {
                    CargarInformacion_FuenteDeFinanciamiento();
                }
            }
        }

        private void Cargar_Referencias_01()
        {
            string sSql = "";
            cboReferencias_01.Clear();
            cboReferencias_01.Add();

            sSql = string.Format(
                "Select IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01, Status \n" +
                "From FACT_Fuentes_De_Financiamiento_Detalles_Referencias (NoLock) \n" +
                "Where IdFuenteFinanciamiento = '{0}' and IdFinanciamiento = '{1}' and Status = 'A' \n" +
                "Group by IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01, Status \n", 
                txtFinanciamiento.Text, txtFuenteDeFinanciamiento.Text
                );

            sSql = string.Format(
                "Select Referencia_01, Status \n" +
                "From FACT_Fuentes_De_Financiamiento_Detalles_Referencias (NoLock) \n" +
                "Where Status = 'A' \n" +
                "Group by Referencia_01, Status \n",
                txtFinanciamiento.Text, txtFuenteDeFinanciamiento.Text
                );


            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener las Referencias");
            }
            else
            {
                cboReferencias_01.Add(leer.DataSetClase, true, "Referencia_01", "Referencia_01");
            }

            cboReferencias_01.SelectedIndex = 0;
        }

        private void Cargar_Referencias_02()
        {
            string sSql = "";
            cboReferencias_02.Clear();
            cboReferencias_02.Add();

            sSql = string.Format(
                "Select IdFuenteFinanciamiento, IdFinanciamiento, NombreDocumento as Referencia_01, Status \n" +
                "From FACT_Fuentes_De_Financiamiento_Detalles_Documentos (NoLock) \n" +
                "Where IdFuenteFinanciamiento = '{0}' and IdFinanciamiento = '{1}' and Status = 'A' ",
                txtFinanciamiento.Text, txtFuenteDeFinanciamiento.Text
                );
            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener las Referencias");
            }
            else
            {
                cboReferencias_01.Add(leer.DataSetClase, true, "", "");
            }

            cboReferencias_02.SelectedIndex = 0;
        }
        #endregion Informacion de Fuente de Financiamiento 

        #region Interface
        private void MostrarEnProceso( bool Mostrar )
        {
            if(Mostrar)
            {
                FrameProceso.Left = 230;
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
            }
        }

        private void chkMarcarDesmarcarTodo_CheckedChanged( object sender, EventArgs e )
        {
            grid.SetValue(Cols.Facturar, chkMarcarDesmarcarTodo.Checked);

            Contabilizar_Remisiones(); 
        }

        private void Contabilizar_Remisiones()
        {
            iRemisiones_Seleccionadas = 0;
            dImporte_Remisiones = 0;

            for(int i = 1; i <= grid.Rows; i++)
            {
                if(grid.GetValueBool(i, Cols.Facturar))
                {
                    iRemisiones_Seleccionadas++;
                    dImporte_Remisiones += grid.GetValueDou(i, Cols.ImporteRemision);
                }
            }

            lblTotal_Seleccionado.Text = dImporte_Remisiones.ToString(sFormato);
            lblRemisiones_Seleccionadas.Text = iRemisiones_Seleccionadas.ToString(sFormato_Decimal);
        }
        #endregion Interface

        #region Obtener Remisiones 
        private bool GenerarFacturaElectronica()
        {
            bool bRegresa = true;
            tipoRemision = rdoRM_Producto.Checked ? eTipoRemision.Insumo : eTipoRemision.Administracion; 


            bRegresa = false;
            sFolioFacturaElectronica = General.MacAddress + General.FechaSistema.ToString();

            dSubTotalSinGrabar = 0; //Convert.ToDouble(lblSubTotalSinGrabar.Text.Replace(",", ""));
            dSubTotalGrabado = 0; //Convert.ToDouble(lblSubTotalGrabado.Text.Replace(",", ""));
            dIva = 0; //Convert.ToDouble(lblIva.Text.Replace(",", ""));
            dTotal = 0; //Convert.ToDouble(lblTotal.Text.Replace(",", ""));

            FrmFacturarRemision f = new FrmFacturarRemision(sIdEmpresa, sIdEstado, sIdFarmacia, sListaDeRemisiones_A_Facturar, tipoRemision,
                dSubTotalSinGrabar, dSubTotalGrabado, dIva, dTotal, "FACTURA CONCENTRADA", true);
            f.ShowDialog();
            if(f.FacturaGenerada)
            {
                bSeGeneroFolioFacturaElectronica = true;
                bRegresa = true;
                sFolioFacturaElectronica = f.FolioFacturaElectronica;

                Obtener_Remisiones(false);
            }

            return bRegresa;
        }
        private string GetListaDeRemisiones()
        {
            string sRegresa = "";
            string sSegmento = "";
            string sValor = "";
            int iItems = 0;

            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, Cols.Facturar))
                {
                    sValor = grid.GetValue(i, Cols.FolioRemision);
                    sSegmento += string.Format("''{0}'', ", sValor);

                    iItems++;
                    if (iItems == 5)
                    {
                        sRegresa += "\t\t" + sSegmento + "\n";
                        iItems = 0;
                        sSegmento = "";
                    }
                }
            }

            if (sSegmento != "")
            {
                sRegresa += "\t\t" + sSegmento + "\n";
            }

            if(sRegresa != "")
            {
                sRegresa = sRegresa.Trim();
                sRegresa = Fg.Left(sRegresa, sRegresa.Length - 1);
            }

            return sRegresa; 
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            if(cboReferencias_01.NumeroDeItems > 1)
            {
                if(bRegresa && cboReferencias_01.SelectedIndex == 0)
                {
                    bRegresa = false;
                    General.msjAviso("No ha seleccionado una Referencia 01 válida, verifiue.");
                    cboReferencias_01.Focus(); 
                } 
            }

            if(cboReferencias_02.NumeroDeItems > 1)
            {
                if(bRegresa && cboReferencias_02.SelectedIndex == 0)
                {
                    bRegresa = false;
                    General.msjAviso("No ha seleccionado una Referencia 02 válida, verifiue.");
                    cboReferencias_02.Focus();
                }
            }


            return bRegresa;  
        }

        private void Obtener_Remisiones(bool ActualizarParametros)
        {
            string sSql = "";
            sListaDeRemisiones_A_Facturar = ""; 

            if(ActualizarParametros)
            {
                i_01_TipoDeRemision = rdoRM_Producto.Checked ? 1 : 2;
                i_01_TipoDeRemision_Complemento = chkRM_Complemento.Checked ? 1 : 0;
                i_02_OrigenInsumos = rdoOIN_Venta.Checked ? 1 : 2;

                i_03_TipoDeInsumos = rdoInsumoMedicamento.Checked ? 1 : 2;
                if (i_01_TipoDeRemision == 2)
                {
                    i_03_TipoDeInsumos = rdoInsumo_Ambos.Checked ? 0 : i_03_TipoDeInsumos;
                }

                i_04_TipoDispensacion = rdoTipoDispensacion_01_Dispensacion.Checked ? 1 : 2;
                i_05_BaseRemision = rdoBaseRemision_Normal.Checked ? 1 : 2;
                i_06_FiltroFolios = chkFiltro_Folios.Checked ? 1 : 0;
                i_07_Filtro_FechaPeriodoRemisionado = chkFiltro_PeriodoRemisionado.Checked ? 1 : 0;
                i_08_Filtro_FechaEmisionRemision = chkFiltro_FechaRemisionado.Checked ? 1 : 0;
                s_09_Referencia_01 = cboReferencias_01.SelectedIndex == 0 ? "" : cboReferencias_01.Data;
                s_10_Referencia_02 = cboReferencias_02.SelectedIndex == 0 ? "" : cboReferencias_02.Data;
                i_11_PartidaGeneral = Convert.ToInt32(nmPartidaGeneral.Value);
                d_12_FechaInicial_PeriodoRemisionado = dtpFechaPeriodoRemisionado_Inicial.Value;
                d_13_FechaFinal_PeriodoRemisionado = dtpFechaPeriodoRemisionado_Final.Value;
                d_14_FechaInicial_FechaRemision = dtpFechaRemision_Inicial.Value;
                d_15_FechaFinal_FechaRemision = dtpFechaRemision_Final.Value;
                s_16_FolioInicial = txtFolioInicial.Text;
                s_17_FolioFinal = txtFolioFinal.Text;
            }

            sSql = string.Format(
            "Exec spp_FACT_Remisiones_Concentrado_Facturacion \n" + 
                " \t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', @IdFuenteFinanciamiento = '{3}', @IdFinanciamiento = '{4}', @IdCliente = '{5}', @IdSubCliente = '{6}', \n" +
                " \t@TipoDeRemision = '{7}', @TipoDeRemision_Complemento = '{8}', @TipoOrigenInsumos = '{9}', @TipoDeInsumos = '{10}', @TipoDispensacion = '{11}', @BaseRemision = '{12}', \n" +
                " \t@Filtro_Folios = '{13}', @Folio_Inicial = '{14}', @Folio_Final = '{15}', \n" +
                " \t@Filtro_FechaPeriodoRemisionado = '{16}', @FechaInicial_PeriodoRemisionado = '{17}', @FechaFinal_PeriodoRemisionado = '{18}', \n" +
                " \t@Filtro_FechaEmisionRemision = '{19}', @FechaInicial_EmisionRemision = '{20}', @FechaFinal_EmisionRemision = '{21}', \n" +
                " \t@Referencia_01 = '{22}', @Referencia_02 = '{23}', @PartidaGeneral = '{24}', \n" +
                " \t@Listado_de_Folios = '{25}' \n" 

                , DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                txtFinanciamiento.Text, txtFuenteDeFinanciamiento.Text, sIdCliente, sIdSubCliente,
                i_01_TipoDeRemision, i_01_TipoDeRemision_Complemento, i_02_OrigenInsumos, i_03_TipoDeInsumos, i_04_TipoDispensacion, i_05_BaseRemision,
                i_06_FiltroFolios, s_16_FolioInicial, s_17_FolioFinal,
                i_07_Filtro_FechaPeriodoRemisionado, General.FechaYMD(d_12_FechaInicial_PeriodoRemisionado), General.FechaYMD(d_13_FechaFinal_PeriodoRemisionado),
                i_08_Filtro_FechaEmisionRemision, General.FechaYMD(d_14_FechaInicial_FechaRemision), General.FechaYMD(d_15_FechaFinal_FechaRemision),
                s_09_Referencia_01, s_10_Referencia_02, i_11_PartidaGeneral, sListaDeRemisiones_A_Facturar
            );

            grid.Limpiar(true);
            Application.DoEvents();
            Application.DoEvents();
            lblTotal.Text = grid.TotalizarColumnaDou(3).ToString(sFormato);

            chkMarcarDesmarcarTodo.Checked = false; 
            //Contabilizar_Remisiones();

            Application.DoEvents();

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Obtener_Remisiones()");
                General.msjError("Ocurrió un error al obtener el listado de remisiones.");
            }
            else
            {
                if(!leer.Leer())
                {
                    General.msjUser("No se encontro información con los criterios seleccionados."); 
                }
                else 
                {
                    InicializarToolBar(true, true);

                    grid.LlenarGrid(leer.DataSetClase);  
                } 
            }

            lblTotal.Text = grid.TotalizarColumnaDou(3).ToString("#,###,###,###,##0.###0");
        }

        #endregion Obtener Remisiones 

        #region Importar Excel 
        clsLeerExcel excel;

        OpenFileDialog openExcel = new OpenFileDialog();
        Thread thLoadFile;
        Thread thReadFile;
        Thread thGuardarInformacion;
        Thread thValidarInformacion;
        Thread thGeneraFolios;

        bool bLeyendoHoja = false;
        string sFile_In = "";
        string sTitulo = "";

        int iRegistrosHoja = 0;
        int iRegistrosProcesados = 0;

        private void IniciarEtiquetasExcel()
        {
            int iValor = 0;
        }


        private void IniciaToolBarExcel( bool Nuevo, bool Abrir, bool LeerArchivo, bool LeerHoja, bool BloquearHojas )
        {
            btnNuevoExcel.Enabled = Nuevo;
            btnAbrirExcel.Enabled = Abrir;
            cboHojas.Enabled = Abrir;
            btnLeerHoja.Enabled = LeerHoja; 
        }

        private void CargarArchivo()
        {
            string sHoja = "";
            bool bHabilitar = false;

            BloqueaHojas(true);
            //////MostrarEnProceso(true, 1);
            //////FrameResultado.Text = sTitulo;

            excel = new clsLeerExcel(sFile_In);
            excel.GetEstructura();

            cboHojas.Items.Clear();
            cboHojas.Items.Add("<< Seleccione >>");
            cboHojas.SelectedIndex = 0;
            Thread.Sleep(1000);

            bHabilitar = excel.Hojas.Registros > 0;
            while(excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja");
                cboHojas.Items.Add(sHoja);
            }

            cboHojas.SelectedIndex = 0;
            btnLeerHoja.Enabled = bHabilitar;
            IniciaToolBarExcel(true, true, bHabilitar, bHabilitar, false);

            BloqueaHojas(false);
            //MostrarEnProceso(false, 1);
            // btnGuardar.Enabled = bHabilitar;

        }

        private void LeerExcel()
        {
            string sHoja = "";
            bool bHabilitar = false;
            excel = new clsLeerExcel(sFile_In);
            excel.GetEstructura();

            cboHojas.Items.Clear();
            cboHojas.Items.Add("<< Seleccione >>");
            cboHojas.SelectedIndex = 0;
            Thread.Sleep(1000);

            bHabilitar = excel.Hojas.Registros > 0;
            while(excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja");
                cboHojas.Items.Add(sHoja);
            }

            cboHojas.SelectedIndex = 0;
            btnLeerHoja.Enabled = bHabilitar;
        }

        private void thLeerHoja()
        {
            BloqueaHojas(true);
            ////MostrarEnProceso(true, 2);
            ////lblProcesados.Visible = false;

            //lblCantidadConLetra.Text = "";
            LeerHoja();

            BloqueaHojas(false);

            bLeyendoHoja = false;
            ////MostrarEnProceso(false, 2);
        }

        private bool LeerHoja()
        {
            bool bRegresa = false;
            int iNumeroDeRemisiones = 0;
            bool bExisteColumna = false; 

            excel.LeerHoja(cboHojas.Text);
            IniciarEtiquetasExcel();


            iRegistrosHoja = excel.Registros;
            bRegresa = iRegistrosHoja > 0;

            sListaDeRemisiones_A_Facturar = "";

            bExisteColumna = excel.ExisteTablaColumna(1, "FolioRemision");
            if(!bExisteColumna)
            {
                lblNumeroDeRemisiones.Text = string.Format("No se encontró la columna [ FolioRemision ] ");
            }
            else 
            {
                while(excel.Leer())
                {
                    iNumeroDeRemisiones++;
                    sListaDeRemisiones_A_Facturar += string.Format("''{0}'', ", Fg.PonCeros(excel.Campo("FolioRemision"), 10));
                }

                sListaDeRemisiones_A_Facturar = sListaDeRemisiones_A_Facturar.Trim();
                sListaDeRemisiones_A_Facturar = Fg.Left(sListaDeRemisiones_A_Facturar, sListaDeRemisiones_A_Facturar.Length - 1);

                if(iNumeroDeRemisiones == 0)
                {
                    sListaDeRemisiones_A_Facturar = "";
                }
                lblNumeroDeRemisiones.Text = string.Format("Remisiones leidas: {0} ", iNumeroDeRemisiones);
            }

            if(bRegresa && bExisteColumna)
            {
                btnNuevoExcel.Enabled = true;
            }
            else
            {
                cboHojas.Enabled = false;
                btnLeerHoja.Enabled = false;  
            }

            return bRegresa;
        }

        public static double Truncate( double Valor, int Decimales )
        {
            double dRegresa = 0;
            double dRegresa_Auxiliar = Valor;
            decimal step = (decimal)Math.Pow(10, Decimales);
            int tmp = (int)Math.Truncate(step * (decimal)Valor);

            dRegresa = (double)(tmp / step);

            return dRegresa; // (double)(tmp / step);
        }

        private void BloqueaHojas( bool Bloquear )
        {
            cboHojas.Enabled = !Bloquear;
        }
        #endregion Importar Excel

        private void grdRemisiones_EditModeOff( object sender, EventArgs e )
        {
           
        }

        private void grdRemisiones_ButtonClicked( object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e )
        {
            Contabilizar_Remisiones();
        }

        private void rdoRM_Servicio_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdoRM_Servicio.Checked)
            {
                rdoInsumoMedicamento.Checked = true;
                rdoInsumo_Ambos.Enabled = false;
            }
            else
            {
                rdoInsumo_Ambos.Enabled = true;
                rdoInsumo_Ambos.Checked = true;
            }
        }
    }
}
