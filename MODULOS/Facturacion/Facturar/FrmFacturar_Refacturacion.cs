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
using System.Xml.Schema;

namespace Facturacion.Facturar
{
    public partial class FrmFacturar_Refacturacion : FrmBaseExt
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
            TipoDeRemision_Descripcion,
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

        public FrmFacturar_Refacturacion()
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


            this.FrameProceso.Height = 50;
            this.FrameProceso.Top = 170;
            this.FrameProceso.Left = 230;

            General.Pantalla.AjustarAlto(this, 80); 

        }

        #region Form 
        private void FrmFacturar_Refacturacion_Load( object sender, EventArgs e )
        {
            InicialiarPantalla();
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

            txtRelacion__Folio.ReadOnly = false;
            txtRelacion__Serie.ReadOnly = false; 

            Fg.IniciaControles();
            grid.Limpiar(true);

            InicializarToolBar();
            MostrarEnProceso(false);

            chkTieneDocumentoRelacionado.Checked = true;
            chkTieneDocumentoRelacionado.Enabled = false; 


            lblTotal.Text = dCERO.ToString(sFormato);
            lblTotal_Seleccionado.Text = dCERO.ToString(sFormato);
            lblRemisiones_Seleccionadas.Text = dCERO.ToString(sFormato_Decimal);

        }

        private void btnNuevo_Click( object sender, EventArgs e )
        {
            InicialiarPantalla();
        }

        private void btnEjecutar_Click( object sender, EventArgs e )
        {
            if(validarDatos())
            {
                GetRemisiones();
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
        #endregion Botones 

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
            tipoRemision = 0;// rdoRM_Producto.Checked ? eTipoRemision.Insumo : eTipoRemision.Administracion; 
            tipoRemision = (eTipoRemision)grid.GetValueInt(1, (int)Cols.TipoDeRemision); 

            bRegresa = false;
            sFolioFacturaElectronica = General.MacAddress + General.FechaSistema.ToString();

            dSubTotalSinGrabar = 0; //Convert.ToDouble(lblSubTotalSinGrabar.Text.Replace(",", ""));
            dSubTotalGrabado = 0; //Convert.ToDouble(lblSubTotalGrabado.Text.Replace(",", ""));
            dIva = 0; //Convert.ToDouble(lblIva.Text.Replace(",", ""));
            dTotal = 0; //Convert.ToDouble(lblTotal.Text.Replace(",", ""));

            FrmFacturarRemision f = new FrmFacturarRemision(sIdEmpresa, sIdEstado, sIdFarmacia, sListaDeRemisiones_A_Facturar, tipoRemision,
                dSubTotalSinGrabar, dSubTotalGrabado, dIva, dTotal, "FACTURA CONCENTRADA", true, 
                txtRelacion__Serie.Text, Convert.ToInt32(txtRelacion__Folio.Text), txtRelacion__UUID.Text 

                );
            f.ShowDialog();
            if(f.FacturaGenerada)
            {
                bSeGeneroFolioFacturaElectronica = true;
                bRegresa = true;
                sFolioFacturaElectronica = f.FolioFacturaElectronica;

                GetRemisiones();
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


            return bRegresa;  
        }

        private void GetRemisiones()
        {
            string sSql = "";
            sListaDeRemisiones_A_Facturar = "";


            sSql = string.Format(
            "Exec spp_FACT_Remisiones_Concentrado__ReFacturacion \n" +
                " \t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', " + 
                " \t@Serie = '{3}', @Folio = '{4}' \n", 

                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                txtRelacion__Serie.Text, txtRelacion__Folio.Text 
            );

            grid.Limpiar(true);
            Application.DoEvents();
            Application.DoEvents();
            lblTotal_CFDI.Text = grid.TotalizarColumnaDou(3).ToString(sFormato);

            chkMarcarDesmarcarTodo.Checked = true;

            Application.DoEvents();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Obtener_Remisiones()");
                General.msjError("Ocurrió un error al obtener el listado de remisiones.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información con los criterios seleccionados.");
                }
                else
                {
                    InicializarToolBar(true, true);

                    grid.LlenarGrid(leer.DataSetClase);

                    btnEjecutar.Enabled = false;
                    btnEjecutar.Enabled = true; 
                }
            }

            lblTotal_CFDI.Text = grid.TotalizarColumnaDou(3).ToString("#,###,###,###,##0.###0");
        }

        #endregion Obtener Remisiones 

        private void grdRemisiones_EditModeOff( object sender, EventArgs e )
        {
           
        }

        private void grdRemisiones_ButtonClicked( object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e )
        {
            Contabilizar_Remisiones();
        }

                    #region Sustitucion de CFDI
        private void txtRelacion__Serie_TextChanged(object sender, EventArgs e)
        {
            txtRelacion__Folio.Text = "";
            txtRelacion__UUID.Text = "";
        }

        private void txtRelacion__Folio_TextChanged(object sender, EventArgs e)
        {
            txtRelacion__UUID.Text = "";
        }

        private void txtRelacion__Serie_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtRelacion__Folio_Validating(object sender, CancelEventArgs e)
        {
            if (txtRelacion__Serie.Text.Trim() != "")
            {
                if (txtRelacion__Folio.Text.Trim() != "")
                {
                    if (!validar__SerieFolio_Relacionado())
                    {
                        txtRelacion__Serie.Text = "";

                        e.Cancel = true;
                    }
                }
            }
        }

        private bool validar__SerieFolio_Relacionado()
        {
            bool bRegresa = true;
            string sSql = "";

            // IdTipoDocumento, Serie, Folio, RFC, Status, CFDI_Relacionado_CPago, Serie_Relacionada_CPago, Folio_Relacionado_CPago  

            sSql = string.Format(
                "Select E.* -- , IsNull(X.uf_CanceladoSAT, 0) as CanceladoSAT  \n " +
                "From vw_FACT_CFD_DocumentosElectronicos E (NoLock) \n" +
                //"Inner Join FACT_CFDI_XML X (NoLock) On( E.IdEmpresa = X.IdEmpresa and E.IdEstado = X.IdEstado and E.IdFarmacia = X.IdFarmacia and E.Serie = X.Serie And E.Folio = X.Folio)  \n " +
                "Where E.IdEmpresa = '{0}' and E.IdEstado = '{1}' and E.IdFarmacia = '{2}' and E.Serie = '{3}' and E.Folio = '{4}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtRelacion__Serie.Text.Trim(), txtRelacion__Folio.Text.Trim()
            );

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "validar__SerieFolio_Relacionado");
            }
            else
            {
                if (!leer.Leer())
                {
                    bRegresa = false;
                    General.msjAviso("No se encontro información del Documento solicitado.");
                }
                else
                {
                    ////if (bRegresa && sRFC_Receptor != leer.Campo("RFC"))
                    ////{
                    ////    bRegresa = false;
                    ////    General.msjAviso("El CFDI Pago pertenece a un cliente distinto al seleccionado.");
                    ////}

                    ////if (bRegresa && sTipoDoctoCFDI != leer.Campo("IdTipoDocumento"))
                    ////{
                    ////    bRegresa = false;
                    ////    General.msjAviso("El CFDI no es un Complemento de Pago, verifique.");
                    ////}

                    if (bRegresa && leer.CampoInt("CanceladoSAT") == 1)
                    {
                        bRegresa = false;
                        General.msjAviso("El CFDI tiene Status 'CANCELADO'.\nNo es posible realizar la refacturación, verifique.");
                    }

                    if (bRegresa)
                    {
                        txtRelacion__Serie.ReadOnly = true;
                        txtRelacion__Folio.ReadOnly = true;
                        txtRelacion__UUID.ReadOnly = true;
                        txtRelacion__UUID.Text = leer.Campo("UUID");

                        lblCliente.Text = leer.Campo("NombreReceptor");

                        lblSubTotalGrabado.Text = leer.CampoDouble("SubTotal").ToString(sFormato);
                        lblIva.Text = leer.CampoDouble("Iva").ToString(sFormato);
                        lblTotal.Text = leer.CampoDouble("Total").ToString(sFormato);

                        GetRemisiones(); 
                    }
                }
            }

            return bRegresa;
        }
                    #endregion Sustitucion de CFDI
    }
}
