#region USING
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Threading;
using System.ServiceModel;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;
using Dll_IFacturacion.Configuracion;
#endregion USING

namespace Dll_IFacturacion.XSA
{
    internal partial class FrmFormaMetodoPago : FrmBaseExt 
    {
        enum Cols
        {
            Clave = 1, Descripcion = 2, RequiereReferencia = 3, ImportePago = 4, Referencia = 5
        }

        DataSet dtsFormaPago = new DataSet();
        DataSet dtsMetodoPago = new DataSet();
        clsGrid grid;

        public string IdFormaDePago = "";
        public string FormaDePago = "";
        public string ObservacionesPago = "";
        public string IdMetodoDePago = "";
        public string MetodoDePago = "";
        public string ListaMetodoDePago = "";
        public string ListaCuentaDePago = ""; 
        public DataSet ListadMetodosDePago; 
        public bool TieneFechaVencimiento = false;
        public DateTime FechaVencimiento = DateTime.Now;
        public string Observaciones = "";
        public bool InformacionGuardada = false;
        public bool CierreAutomatico = false;
        public eTipoDeFacturacion ModuloFacturacion = eTipoDeFacturacion.Ninguna;

        string sXMLFormaPago = "PAGO EN UNA SOLA EXHIBICIÓN";
        string sXMLCondicionesPago = "Crédito";
        string sXMLMetodoPago = "No identificado";
        string sXMLObservacionesPago = "";

        double dImporteACobrar_Base = 0;
        double dImporteACobrar = 0;
        double dImporteCobrado = 0;
        double dImportePorCobrar = 0;
        double dImporte_0 = 0;
        string sFormato = "$ #,#0.00";
        eVersionCFDI tpVersionCFDI = eVersionCFDI.Ninguna; 

        public FrmFormaMetodoPago(eVersionCFDI VersionCFDI, DataSet FormaDePago, DataSet MetodoDePago, double ImporteACobrar)
        {
            InitializeComponent();
            tpVersionCFDI = VersionCFDI;

            dtsFormaPago = FormaDePago;
            dtsMetodoPago = MetodoDePago;

            ////dtsFormaPago = MetodoDePago;
            ////dtsMetodoPago = FormaDePago;


            ////if (tpVersionCFDI == eVersionCFDI.Version__3_2)
            ////{
            ////    dtsFormaPago = FormaDePago;
            ////    dtsMetodoPago = MetodoDePago;
            ////}

            ////if (tpVersionCFDI == eVersionCFDI.Version__3_3)
            ////{
            ////    dtsFormaPago = MetodoDePago;
            ////    dtsMetodoPago = FormaDePago;
            ////}

            grdMetodosDePago.EditModeReplace = true;
            grid = new clsGrid(ref grdMetodosDePago, this);

            dImporteACobrar = Math.Round(ImporteACobrar, 4); 
            lblImporteACobrar.Text = dImporteACobrar.ToString(sFormato);
            lblImporteCobrado.Text = dImporte_0.ToString(sFormato);
            lblImportePorCobrar.Text = dImporte_0.ToString(sFormato);

            CargarCombos();
            Totalizar(); 
        }

        public bool ImportePagadoCompleto
        {
            get { return dImportePorCobrar == 0; }
        }

        private void CargarCombos()
        {
            cboFormasDePago.Clear();
            cboFormasDePago.Add();
            ////cboMetodosDePago.Clear();
            ////cboMetodosDePago.Add();


            ////if (tpVersionCFDI == eVersionCFDI.Version__3_2)
            ////{
            ////    cboFormasDePago.Add(dtsFormaPago, true, "IdFormaDePago", "Descripcion");
            ////}

            ////if (tpVersionCFDI == eVersionCFDI.Version__3_3)
            ////{
            ////    cboFormasDePago.Add(dtsFormaPago, true, "IdFormaDePago", "Descripcion");
            ////}

            cboFormasDePago.Add(dtsMetodoPago, true, "IdFormaDePago", "Descripcion");



            ////cboMetodosDePago.Add(dtsMetodoPago, true, "IdMetodoPago", "Descripcion");

            cboFormasDePago.SelectedIndex = 0;
            ////cboMetodosDePago.SelectedIndex = 0; 

            if (cboFormasDePago.NumeroDeItems > 1 ) 
            {
                cboFormasDePago.SelectedIndex = 1;
            }

            ////if (cboMetodosDePago.NumeroDeItems > 1)
            ////{
            ////    cboMetodosDePago.SelectedIndex = 1;
            ////}
        }

        #region Form 
        private void FrmFormaMetodoPago_Load(object sender, EventArgs e)
        {
            ////ListadMetodosDePago = dtsMetodoPago; 
            CargarInformacionInicial(true);
        }
        #endregion Form

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            CargarInformacionInicial(false); 
        }

        private void CargarInformacionInicial( bool ValidarCierreAutomatico )
        {
            string sValorBusqueda = DtIFacturacion.FormaDePago;
            string sValorBusqueda_MetodoDePago = DtIFacturacion.MetodoDePago;

            cboFormasDePago.Data = IdFormaDePago == "" ? sValorBusqueda_MetodoDePago : IdFormaDePago;
            if(cboFormasDePago.Data == "0")
            {
                //cboFormasDePago.SelectedIndex = 1;
            }

            ////cboMetodosDePago.Data = IdMetodoDePago;
            ////if (cboMetodosDePago.Data == "0")
            ////{
            ////    cboMetodosDePago.SelectedIndex = 1;
            ////}
            ///

            if(ObservacionesPago == null)
            {
                ObservacionesPago = "";
            }

            txtCondicionesPago.Text = ObservacionesPago;
            chkVencimiento.Checked = TieneFechaVencimiento;
            dtpFechaVencimiento.Value = FechaVencimiento;
            txtObservaciones.Text = Observaciones;

            grid.LlenarGrid(ListadMetodosDePago);
            ConfigurarGrid();

            lblImporteACobrar.Text = dImporteACobrar.ToString(sFormato);
            lblImporteCobrado.Text = dImporte_0.ToString(sFormato);
            lblImportePorCobrar.Text = dImporte_0.ToString(sFormato);

            if (ModuloFacturacion == eTipoDeFacturacion.Manual_Excel || ModuloFacturacion == eTipoDeFacturacion.Manual)
            {
                if (DtIFacturacion.ImplementaInformacionPredeterminada)
                {
                    grid.BloqueaGrid(true);
                    cboFormasDePago.Enabled = false;
                    cboFormasDePago.Data = DtIFacturacion.FormaDePago;
                    cboFormasDePago.Data = sValorBusqueda_MetodoDePago; 
                    dImporteCobrado = dImporteACobrar;

                    if (DtIFacturacion.PlazoDiasVenceFactura > 0 && tpVersionCFDI == eVersionCFDI.Version__3_2)
                    {
                        chkVencimiento.Enabled = false;
                        chkVencimiento.Checked = true;

                        dtpFechaVencimiento.Enabled = false;
                        dtpFechaVencimiento.Value = dtpFechaVencimiento.Value.AddDays(DtIFacturacion.PlazoDiasVenceFactura);

                        txtCondicionesPago.Enabled = false;
                        txtCondicionesPago.Text = string.Format("{0} {1} días", cboFormasDePago.Text, DtIFacturacion.CondicionesDePago);
                        txtCondicionesPago.Text = string.Format("{0}", DtIFacturacion.CondicionesDePago);
                    }

                    txtCondicionesPago.Text = string.Format("{0}", DtIFacturacion.CondicionesDePago);
                    txtCondicionesPago.Enabled = !(txtCondicionesPago.Text != ""); 

                    for (int i = 1; i <= grid.Rows; i++)
                    {
                        if (grid.GetValue(i, (int)Cols.Clave) == sValorBusqueda) //DtIFacturacion.MetodoDePago)
                        {
                            grid.SetValue(i, (int)Cols.ImportePago, dImporteACobrar);
                            grid.SetValue(i, (int)Cols.Referencia, DtIFacturacion.MetodoDePagoReferencia);
                            break;
                        }
                    }

                    dImporteCobrado = grid.TotalizarColumnaDou((int)Cols.ImportePago);
                    dImportePorCobrar = dImporteACobrar - dImporteCobrado;
                }
            }

            lblImporteACobrar.Text = dImporteACobrar.ToString(sFormato);
            lblImporteCobrado.Text = dImporteCobrado.ToString(sFormato);
            lblImportePorCobrar.Text = dImportePorCobrar.ToString(sFormato);

            Totalizar();

            if (ValidarCierreAutomatico)
            {
                if (CierreAutomatico)
                {
                    GuardarInformacion(); 
                }
            }
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            GuardarInformacion(); 
        }

        private void GuardarInformacion()
        {
            if (validar())
            {

                GetMetodosDePago();

                IdFormaDePago = cboFormasDePago.Data;
                FormaDePago = cboFormasDePago.Text;
                ObservacionesPago = txtCondicionesPago.Text;
                ////IdMetodoDePago = cboMetodosDePago.Data;
                ////MetodoDePago = cboMetodosDePago.Text; 
                TieneFechaVencimiento = chkVencimiento.Checked;
                FechaVencimiento = dtpFechaVencimiento.Value;
                Observaciones = txtObservaciones.Text;

                InformacionGuardada = true;

                if (CierreAutomatico)
                {
                    this.Close(); 
                }
                else
                {
                    this.Hide();
                }
            }
        }
        #endregion Botones

        #region Validaciones 
        private bool validar()
        {
            bool bRegresa = true;

            try
            {

                if (bRegresa && dImportePorCobrar != 0)
                {
                    bRegresa = false;

                    if (dImporteCobrado < dImporteACobrar)
                    {
                        General.msjUser("No se ha pagado el importe total, verifique.");
                    }

                    if (dImporteCobrado > dImporteACobrar)
                    {
                        General.msjUser("El importe pagado excede el importe total, verifique.");
                    }

                }

                if (bRegresa)
                {
                    bRegresa = validarReferencias();

                    if (!bRegresa)
                    {
                        General.msjUser("No ha capturado la Referencia de algunos métodos de pago, verifique.");
                    }
                }

                //return true;
            }
            catch (Exception exception)
            {
                General.msjError(exception.Message);
                //return false;
            }

            return bRegresa;
        }

        public void ConfigurarGrid()
        {
            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValue(i, (int)Cols.RequiereReferencia).ToUpper() == "NO")
                {
                    grid.BloqueaCelda(true, i, (int)Cols.Referencia);
                }
            }
        }

        public bool validarReferencias()
        {
            bool bRegresa = true;

            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValue(i, (int)Cols.RequiereReferencia).ToUpper() == "SI")
                {
                    if (grid.GetValueDou(i, (int)Cols.ImportePago) > 0)
                    {
                        if (grid.GetValue(i, (int)Cols.Referencia) == "")
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private void GetMetodosDePago()
        {
            DataSet dtsPaso = ListadMetodosDePago.Clone();
            ListaMetodoDePago = "";
            ListaCuentaDePago = ""; 

            try
            {
                for (int i = 1; i <= grid.Rows; i++)
                {
                    object[] objRow = {
                    grid.GetValue(i, (int)Cols.Clave),  
                    grid.GetValue(i, (int)Cols.Descripcion),  
                    grid.GetValue(i, (int)Cols.RequiereReferencia),  
                    grid.GetValueDou(i, (int)Cols.ImportePago),  
                    grid.GetValue(i, (int)Cols.Referencia)
                                  };
                    dtsPaso.Tables[0].Rows.Add(objRow);

                    if (grid.GetValueDou(i, (int)Cols.ImportePago) > 0)
                    {
                        ListaMetodoDePago += string.Format("{0}, ", grid.GetValue(i, (int)Cols.Clave));

                        if (grid.GetValue(i, (int)Cols.Referencia) != "")
                        {
                            ListaCuentaDePago += string.Format("{0}, ", grid.GetValue(i, (int)Cols.Referencia)); 
                        }
                    }
                }

            }
            catch
            {
            }


            if (ListaMetodoDePago != "")
            {
                ListaMetodoDePago = ListaMetodoDePago.Trim();
                ListaMetodoDePago = Fg.Left(ListaMetodoDePago, ListaMetodoDePago.Length - 1); 
            }

            if (ListaCuentaDePago != "")
            {
                ListaCuentaDePago = ListaCuentaDePago.Trim();
                ListaCuentaDePago = Fg.Left(ListaCuentaDePago, ListaCuentaDePago.Length - 1);
            }

            

            ListadMetodosDePago = dtsPaso.Copy();

        }

        private void Totalizar()
        {
            dImporteCobrado = grid.TotalizarColumnaDou((int)Cols.ImportePago);
            dImportePorCobrar = dImporteACobrar - dImporteCobrado;

            lblImporteACobrar.Text = dImporteACobrar.ToString(sFormato);
            lblImporteCobrado.Text = dImporteCobrado.ToString(sFormato);
            lblImportePorCobrar.Text = dImportePorCobrar.ToString(sFormato);
        }

        private void grdMetodosDePago_EditModeOff(object sender, EventArgs e)
        {
            Totalizar();
        }
        #endregion Validaciones
    }
}
