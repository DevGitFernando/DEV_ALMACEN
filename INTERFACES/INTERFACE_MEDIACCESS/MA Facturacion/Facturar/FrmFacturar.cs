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

using Dll_MA_IFacturacion; 
using Dll_MA_IFacturacion.XSA; 


namespace MA_Facturacion.Facturar
{
    public partial class FrmFacturar : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas ayuda;
        clsConsultas query;

        xsaImprimirDocumento print; //  = new xsaImprimirDocumento(sIdEmpresa, sIdEstado, sIdFarmacia, General.DatosConexion);
        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;
        string sIdPersonal = DtGeneral.IdPersonal; 

        string sFuncionError = ""; 
        string sFolioFactura = "";
        string sMensaje = "";
        bool bSeGeneroFolioFacturaElectronica = false; 
        string sFolioFacturaElectronica = "";
        double dSubTotalSinGrabar = 0;
        double dSubTotalGrabado = 0;
        double dIva = 0;
        double dTotal = 0;
        bool bRemisionFacturada = false;
        bool bEsFacturable = false;
        string sIdFarmaciaReferencia = "";
        string sFarmaciaReferencia = "";
        string sIdEstadoReferencia = "";
        eTipoDeFacturacion tipoFactura = eTipoDeFacturacion.Ninguna;
        eTipoRemision tipoRemision = eTipoRemision.Ninguno; 
        eTipoInsumo tipoInsumo= eTipoInsumo.Ninguno;

        string sValor_Cero = "0"; 
        string sFormato = "###,###,###,###,##0.###0";


        public FrmFacturar()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, General.DatosApp, this.Name);

            print = new xsaImprimirDocumento(sIdEmpresa, sIdEstado, sIdFarmacia, General.DatosConexion); 
        }

        #region Form 
        private void FrmFacturar_Load(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }
        #endregion Form 

        #region Botones  
        private void InicializarPantalla()
        {
            sValor_Cero = (0).ToString(sFormato);
            bSeGeneroFolioFacturaElectronica = false;
            bRemisionFacturada = false; 
            sFuncionError = ""; 
            sFolioFactura = "";
            sMensaje = ""; 
            sFolioFacturaElectronica = "";
            dSubTotalSinGrabar = 0;
            dSubTotalGrabado = 0;
            dIva = 0;
            dTotal = 0;

            Fg.IniciaControles();
            FormatearImportes(0); 
            IniciarToolBar(false, false, false); 

            dtpFechaRegistro.Enabled = false; 
            dtpFechaRegistroRemision.Enabled = false;

            bEsFacturable = false; 
            lblFacturada.Visible = false;
            lblFacturada.Text = "FACTURADA";  

            txtFolioFactura.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                if (GenerarFacturaElectronica())
                {
                    leer.DataSetClase = query.FacturasRemisiones(sIdEmpresa, sIdEstado, sIdFarmacia, txtFolioFactura.Text, "txtFolioFactura_Validating");
                    if (leer.Leer())
                    {
                        CargarDatosDeRemision(true);
                    }
                }
            } 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            print.Imprimir(lblFacturaElectronica.Text, this); 
        } 
        #endregion Botones 

        #region Funciones y Procedimientos Privados 
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir; 
        }

        private void FormatearImportes(double Valor)
        {
            sValor_Cero = Valor.ToString(sFormato); 
            lblSubTotalSinGrabar.Text = sValor_Cero; 
            lblSubTotalGrabado.Text = sValor_Cero;
            lblIva.Text = sValor_Cero;
            lblTotal.Text = sValor_Cero;  
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            if ( txtFolioFactura.Text.Trim() == "" )
            {
                bRegresa = false;
                General.msjUser("Folio de factura inválido, verifique."); 
                txtFolioFactura.Focus(); 
            }

            if (bRegresa && txtFolioRemision.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Folio de remisión a facturar, verifique.");
                txtFolioRemision.Focus();
            }

            if (bRegresa && !bEsFacturable)
            {
                bRegresa = false;
                General.msjUser("El folio de remisión no esta marcado como facturable, verifique.");
                txtFolioRemision.Focus(); 
            }

            if (bRegresa && bRemisionFacturada)
            {
                bRegresa = false;
                General.msjUser("El Folio de remisión capturado ya se encuentra facturado, no es posible asignarlo a otra factura.");
                txtFolioRemision.Focus(); 
            }

            //if (bRegresa && txtObservaciones.Text.Trim() == "")
            //{
            //    bRegresa = false;
            //    General.msjUser("No ha capturado las observaciones de la factura, verifique.");
            //    txtObservaciones.Focus();
            //}

            return bRegresa; 
        }

        private bool GuardarFactura()
        {
            bool bRegresa = false;

            if (!cnn.Abrir())
            {
                General.msjAviso(General.MsjErrorAbrirConexion); 
            }
            else
            {
                cnn.IniciarTransaccion();

                bRegresa = GuardarDatosFactura(); 

                if (!bRegresa) 
                {
                    Error.GrabarError(leer, "GuardarFactura()___" + sFuncionError);
                    cnn.DeshacerTransaccion(); 
                }
                else
                {
                    cnn.CompletarTransaccion();
                    txtFolioFactura.Text = sFolioFactura;
                    lblFacturaElectronica.Text = sFolioFacturaElectronica; 
                    General.msjUser(sMensaje);
                    btnImprimir_Click(null, null); 
                    IniciarToolBar(false, false, true); 
                } 

                cnn.Cerrar(); 
            }

            return bRegresa; 
        }

        private bool GuardarDatosFactura()
        {
            bool bRegresa = false;
            string sSql = ""; 

            {
                dSubTotalSinGrabar = Convert.ToDouble(lblSubTotalSinGrabar.Text.Replace(",", ""));
                dSubTotalGrabado = Convert.ToDouble(lblSubTotalGrabado.Text.Replace(",", ""));
                dIva = Convert.ToDouble(lblIva.Text.Replace(",", "")); 
                dTotal = Convert.ToDouble(lblTotal.Text.Replace(",", "")); 

                sSql = string.Format("Exec spp_Mtto_FACT_Facturar_Remisiones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}'  ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, txtFolioFactura.Text.Trim(), (int)tipoFactura, txtFolioRemision.Text.Trim(),
                    sFolioFacturaElectronica, sIdPersonal, dSubTotalSinGrabar, dSubTotalGrabado, dIva, dTotal, txtObservaciones.Text, 'A' ); 
                if (!leer.Exec(sSql)) 
                {
                    sFuncionError = "GuardarDatosFactura()"; 
                }
                else
                {
                    if (leer.Leer())
                    {
                        bRegresa = true;
                        sFolioFactura = leer.Campo("Folio");
                        sMensaje = leer.Campo("Mensaje");  
                    }
                }
            }

            return bRegresa;
        }

        private bool GenerarFacturaElectronica()
        {
            bool bRegresa = true;

            if (!bSeGeneroFolioFacturaElectronica) 
            {
                bRegresa = false; 
                sFolioFacturaElectronica = General.MacAddress + General.FechaSistema.ToString();
                // bRegresa = true;

                FrmFacturarRemision f = new FrmFacturarRemision(sIdEmpresa, sIdEstado, sIdFarmacia, txtFolioRemision.Text, tipoRemision,
                    txtObservaciones.Text.Trim(), dSubTotalSinGrabar, dSubTotalGrabado, dIva, dTotal, sIdEstadoReferencia, sIdFarmaciaReferencia, sFarmaciaReferencia);    
                f.ShowDialog();
                if (f.FacturaGenerada)
                {
                    if (!f.VistaPrevia)
                    {
                        bSeGeneroFolioFacturaElectronica = true;
                        bRegresa = true;
                        sFolioFacturaElectronica = f.FolioFacturaElectronica;
                        txtFolioFactura.Text = f.FolioFactura_Interna;
                    }
                }
            }

            return bRegresa;
        } 

        private bool ActualizarInformacionFacturaElectronica()
        {
            bool bRegresa = false;
            string sSql = ""; 

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Privados

        #region Eventos 
        private void txtFolioFactura_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtFolioFactura_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolioFactura.Text.Trim() == "")
            {
                txtFolioFactura.Enabled = false;
                txtFolioFactura.Text = "*";
                IniciarToolBar(true, false, false); 
            }
            else
            {
                leer.DataSetClase = query.FacturasRemisiones(sIdEmpresa, sIdEstado, sIdFarmacia, txtFolioFactura.Text, "txtFolioFactura_Validating");
                if (leer.Leer())
                {
                    CargarDatosDeFactura(); 
                }
            }
        }

        private void CargarDatosDeFactura()
        {
            IniciarToolBar(false, false, true);
            leer.RegistroActual = 1;
            leer.Leer(); 

            txtFolioFactura.Enabled = false; 
            txtFolioFactura.Text = leer.Campo("FolioFactura");
            lblFacturaElectronica.Text = leer.Campo("FolioFacturaElectronica");

            txtObservaciones.Enabled = false; 
            txtObservaciones.Text = leer.Campo("Observaciones"); 
            dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");
            dtpFechaRegistro.Enabled = false;

            tipoFactura = (eTipoDeFacturacion)leer.CampoInt("TipoDeFactura");
            tipoRemision = (eTipoRemision)leer.CampoInt("TipoDeFactura"); 
            lblTipoFactura.Text = leer.Campo("TipoDeFacturaDesc"); 

            txtFolioRemision.Text = leer.Campo("FolioRemision");
            txtFolioRemision.Enabled = false; 
            dtpFechaRegistroRemision.Value = leer.CampoFecha("FechaRemision");
            dtpFechaRegistroRemision.Enabled = false;

            dSubTotalSinGrabar = leer.CampoDouble("SubTotalSinGrabar");
            dSubTotalGrabado = leer.CampoDouble("SubTotalGrabado");
            dIva = leer.CampoDouble("Iva");
            dTotal = leer.CampoDouble("Total");
            sIdFarmaciaReferencia = leer.Campo("IdFarmaciaReferencia");
            sFarmaciaReferencia = leer.Campo("FarmaciaReferencia");
            //txtFarmacia.Text = sIdFarmaciaReferencia;
            //txtFarmacia.Enabled = false;

            lblEstado.Text = leer.Campo("EstadoReferencia"); 
            lblFarmacia.Text = sFarmaciaReferencia;


            lblSubTotalSinGrabar.Text = dSubTotalSinGrabar.ToString(sFormato);
            lblSubTotalGrabado.Text = dSubTotalGrabado.ToString(sFormato);
            lblIva.Text = dIva.ToString(sFormato);
            lblTotal.Text = dTotal.ToString(sFormato);  
        }

        private void txtFolioRemision_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                lblFacturada.Visible = false;
            }
        }

        private void txtFolioRemision_TextChanged(object sender, EventArgs e)
        {
            lblEstado.Text = "";
            lblFarmacia.Text = ""; 

            FormatearImportes(0);
        } 

        private void txtFolioRemision_Validating(object sender, CancelEventArgs e)
        {
            lblFacturada.Visible = false; 
            if (txtFolioRemision.Text.Trim() != "")
            {
                leer.DataSetClase = query.Facturacion_Remisiones(sIdEmpresa, sIdEstado, sIdFarmacia, txtFolioRemision.Text, "txtFolioFactura_Validating");
                if (leer.Leer())
                {
                    CargarDatosDeRemision(false);
                }
            }
        }

        private void CargarDatosDeRemision(bool Facturado)
        {
            if (Facturado)
            {
                txtFolioFactura.Text = leer.Campo("FolioFactura");
            }

            txtFolioRemision.Enabled = false;
            txtFolioRemision.Text = leer.Campo("FolioRemision");
            dtpFechaRegistroRemision.Value = leer.CampoFecha("FechaRemision");

            tipoFactura = (eTipoDeFacturacion)leer.CampoInt("TipoDeRemision");
            tipoRemision = (eTipoRemision)leer.CampoInt("TipoDeRemision");
            tipoInsumo = (eTipoInsumo)leer.CampoInt("TipoInsumo");
            lblTipoFactura.Text = leer.Campo("TipoDeRemisionDesc");

            dSubTotalSinGrabar = leer.CampoDouble("SubTotalSinGrabar");
            dSubTotalGrabado = leer.CampoDouble("SubTotalGrabado");
            dIva = leer.CampoDouble("Iva");
            dTotal = leer.CampoDouble("Total");

            lblSubTotalSinGrabar.Text = dSubTotalSinGrabar.ToString(sFormato);
            lblSubTotalGrabado.Text = dSubTotalGrabado.ToString(sFormato);
            lblIva.Text = dIva.ToString(sFormato);
            lblTotal.Text = dTotal.ToString(sFormato);
            bEsFacturable = leer.CampoBool("EsFacturable");
            sIdFarmaciaReferencia = leer.Campo("IdFarmacia_Remision");
            sIdEstadoReferencia = leer.Campo("IdEstado_Remision");
            sFarmaciaReferencia = leer.Campo("Farmacia_Remision");

            //txtFarmacia.Text = sIdFarmaciaReferencia;
            //txtFarmacia.Enabled = false;
            lblEstado.Text = leer.Campo("Estado_Remision");
            lblFarmacia.Text = sFarmaciaReferencia;


            leer.DataSetClase = query.RemisionFacturada(sIdEmpresa, sIdEstado, sIdFarmacia, txtFolioRemision.Text, "A", "CargarDatosDeRemision()");
            if (leer.Leer())
            {
                bRemisionFacturada = true;
                lblFacturada.Visible = true;
                lblFacturaElectronica.Text = leer.Campo("FolioFacturaElectronica");
                txtFolioFactura.Text = leer.Campo("FolioFactura");
                IniciarToolBar(false, false, true);
            }

            if (!bEsFacturable)
            {
                lblFacturada.Text = "NO FACTURABLE"; 
                lblFacturada.Visible = true;
                txtFolioRemision.Enabled = true;
                txtFolioRemision.Focus(); 
            }

            txtObservaciones.Focus(); 
        } 
        #endregion Eventos 
    }
}
