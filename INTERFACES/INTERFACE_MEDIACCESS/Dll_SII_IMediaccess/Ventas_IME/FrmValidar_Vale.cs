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

using Dll_SII_IMediaccess;
using Dll_SII_IMediaccess.Ventas; 

using DllFarmaciaSoft;

namespace Dll_SII_IMediaccess.Ventas_IME
{
    public partial class FrmValidar_Vale : FrmBaseExt
    {
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        clsValidar_Vale validar_Vale = new clsValidar_Vale();
        //bool bAtencionDeRecetasManuales = GnDll_SII_IMediaccess.AtencionRecetasManuales; 

        public FrmValidar_Vale()
        {
            InitializeComponent();
            leer = new clsLeer(ref con);
            Iniciar_Pantalla();
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
        }

        private void FrmValidar_Vale_Load(object sender, EventArgs e)
        {
            Iniciar_Pantalla();
        }

        #region Botones
        private void Iniciar_Pantalla()
        {
            Fg.IniciaControles();
            ////lblFolioRecetaAsociado.Enabled = false; 
            IniciarToolBar(); 

            //////if(DtGeneral.EsEquipoDeDesarrollo)
            //////{
            //////    txtFolioEligibilidad.Text = "E006493943"; 
            //////}

            txtSocioComercial.Focus();
        }

        private void IniciarToolBar()
        {
            IniciarToolBar(true, false); 
        }

        private void IniciarToolBar(bool Validar, bool SurtirReceta)
        {
            btnSolicitarInformacionVale.Enabled = Validar;
            btnSurtirReceta.Enabled = SurtirReceta;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Iniciar_Pantalla(); 
        }

        private void btnSolicitarInformacionVale_Click(object sender, EventArgs e)
        {
            bool bExito = false;
            Fg.IniciaControles(this, true, FrameInformacion);
            Application.DoEvents();
            System.Threading.Thread.Sleep(100); 

            txtSocioComercial.Enabled = false;
            txtIdSucursal.Enabled = false;
            txtFolioVale.Enabled = false;
            btnSurtirReceta.Enabled = false;
            IniciarToolBar(false, false);

            bExito = Validar_Vale();

            validar_Vale.IdSocioComercial = txtSocioComercial.Text;
            validar_Vale.IdSucursalSocioComercial = txtIdSucursal.Text;

            validar_Vale.NombreSocioComercial = lblSocioComercial.Text;
            validar_Vale.NombreSucursalSocioComercial = lblSucursal.Text;
            txtFolioVale.Text = Fg.PonCeros(txtFolioVale.Text.Trim(), 8);
            validar_Vale.FolioVale = txtFolioVale.Text;

            if (bExito)
            {
                if (!validar_Vale.Vale_Valido)
                {
                    FrmRegistroDeVales f = new FrmRegistroDeVales(validar_Vale);
                    f.ShowDialog();

                    if (f.bValeManualGuardado)
                    {
                        validar_Vale.Vale_Valido_ParaSurtido = true;
                        btnSurtirReceta.Enabled = true;
                        validar_Vale.ConsultarEncabezado();

                        btnSurtirReceta_Click(this, null);
                    }
                }
                else
                {
                    if (!validar_Vale.Vale_Con_Surtido_Completo)
                    {
                        btnSurtirReceta.Enabled = true;
                    }
                }
            }

            lblResultadoValidacion.ForeColor = validar_Vale.Color__Vale_Valido_ParaSurtido;
            lblResultadoValidacion.Text = validar_Vale.Mensaje__Vale_Valido_ParaSurtido; 
        }

        private void btnSurtirReceta_Click(object sender, EventArgs e)
        {
            FrmVentas_MA f = new FrmVentas_MA(validar_Vale, false);
            f.ShowDialog();

            Iniciar_Pantalla();
        }

        private bool validarReceta(bool MostrarCapturaRecetaManual)
        {
            bool bRespuesta = false; 

            ////if (!validarElegibilidad.Validar_FolioDeReceta(txtFolioEligibilidad.Text, txtFolioReceta.Text))
            ////{
            ////    if (validarElegibilidad.RecetaSurtida)
            ////    {
            ////        General.msjUser("El folio de receta ya fue surtido, receta invalida para surtido."); 
            ////    }
            ////    else 
            ////    {
            ////        if (!bAtencionDeRecetasManuales)
            ////        {
            ////            General.msjUser("El folio de receta invalido para surtido."); 
            ////        }
            ////        else
            ////        {
            ////            bRespuesta = General.msjConfirmar("Folio de receta electrónica no encontrado.\n\n¿ Desea habilitar la captura manual de receta ?") == System.Windows.Forms.DialogResult.Yes;
            ////        }
            ////    }

            ////    btnSurtirReceta.Enabled = !bRespuesta;

            ////    if (bRespuesta)
            ////    {
            ////        validarElegibilidad.MA_FolioDeReceta_Manual = txtFolioReceta.Text; 
            ////        FrmRecetasManuales f = new FrmRecetasManuales(validarElegibilidad);
            ////        f.ShowDialog();

            ////        if (!f.RecetaManualGuardada)
            ////        {
            ////            btnSurtirReceta.Enabled = true; 
            ////        }
            ////        else 
            ////        {
            ////            Validar_Elegibilidad();
            ////            if (validarElegibilidad.Validar_FolioDeReceta(txtFolioEligibilidad.Text, txtFolioReceta.Text))
            ////            {
            ////                FrmVentas_MA fm = new FrmVentas_MA(validarElegibilidad);
            ////                fm.ShowDialog();
            ////            }
            ////        }
            ////    }
            ////}
            ////else
            ////{
            ////    FrmVentas_MA f = new FrmVentas_MA(validarElegibilidad);
            ////    f.ShowDialog(); 
            ////}

            return bRespuesta; 
        }

  
        private bool Validar_Vale()
        {
            bool bRegresa = false;
            validar_Vale = new clsValidar_Vale();
            bRegresa = validar_Vale.Validar_Vale(txtSocioComercial.Text.Trim(), txtIdSucursal.Text.Trim(), txtFolioVale.Text.Trim());
            ////lblEmpresa.Text = validarElegibilidad.EmpresaRazonSocial;
            ////lblBeneficiario.Text = validarElegibilidad.NombreBeneficiario;
            ////lblClaveProveedor.Text = validarElegibilidad.ReferenciaProveedor;
            ////lblProveedor.Text = validarElegibilidad.NombreProveedor;
            ////lblCopago.Text = string.Format("En {0}, {1} ", validarElegibilidad.CopagoEN, validarElegibilidad.Copago);
            ////lblCopago.Text = validarElegibilidad.TituloCopago;
            ////lblFolioRecetaAsociado.Text = validarElegibilidad.MA_FolioDeReceta_Asociado;

            return bRegresa;
        }
        

        private void txtSocioComercial_Validating(object sender, CancelEventArgs e)
        {
            if (txtSocioComercial.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.SociosComerciales(txtSocioComercial.Text, "txtIdSocioComercial_Validating()");

                if (leer.Leer())
                {
                    txtSocioComercial.Enabled = false;
                    txtSocioComercial.Text = leer.Campo("IdSocioComercial");
                    lblSocioComercial.Text = leer.Campo("Nombre");
                }
                else
                {
                    txtSocioComercial.Text = "";
                    txtSocioComercial.Focus();
                }
            }
        }

        private void txtSocioComercial_TextChanged(object sender, EventArgs e)
        {
            lblSocioComercial.Text = "";
            txtIdSucursal.Text = "";
            lblSucursal.Text = "";
        }

        private void txtSocioComercial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.SociosComerciales("txtCte_KeyDown");
                if (leer.Leer())
                {
                    txtSocioComercial.Text = leer.Campo("IdSocioComercial");
                    lblSocioComercial.Text = leer.Campo("Nombre");
                    txtIdSucursal.Focus();
                }
                else
                {
                    txtSocioComercial.Text = "";
                    txtSocioComercial.Focus();
                }
            }
        }

        private void txtIdSucursal_Validating(object sender, CancelEventArgs e)
        {
            if (txtSocioComercial.Text.Trim() != "" || txtIdSucursal.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.SociosComerciales_Sucursales(txtSocioComercial.Text.Trim(), txtIdSucursal.Text.Trim(), "txtIdSocioComercial_Validating()");

                if (leer.Leer())
                {
                    txtIdSucursal.Enabled = false;
                    txtIdSucursal.Text = leer.Campo("IdSucursal");
                    lblSucursal.Text = leer.Campo("NombreSucursal");
                }
                else
                {
                    txtIdSucursal.Text = "";
                    txtIdSucursal.Focus();
                }
            }
            else
            {
                txtIdSucursal.Text = "";
                txtIdSucursal.Enabled = true;
            }
        }

        private void txtIdSucursal_TextChanged(object sender, EventArgs e)
        {
            lblSucursal.Text = "";
        }

        private void txtIdSucursal_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSocioComercial.Text.Trim() != "")
            {
                if (e.KeyCode == Keys.F1)
                {
                    // leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                    leer.DataSetClase = Ayuda.SociosComerciales_Sucursales(txtSocioComercial.Text.Trim(), "txtCte_KeyDown");
                    if (leer.Leer())
                    {
                        txtIdSucursal.Text = leer.Campo("IdSucursal");
                        lblSucursal.Text = leer.Campo("NombreSucursal");
                        txtFolioVale.Focus();
                    }
                    else
                    {
                        txtIdSucursal.Text = "";
                        txtIdSucursal.Focus();
                    }
                }
            }
        }

        #endregion Botones
    }
}
