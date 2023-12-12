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

namespace Dll_SII_IMediaccess.Validaciones_MA
{
    public partial class FrmValidar_Eligibilidad : FrmBaseExt
    {
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsValidar_Elegibilidad validarElegibilidad = new clsValidar_Elegibilidad();
        bool bAtencionDeRecetasManuales = GnDll_SII_IMediaccess.AtencionRecetasManuales; 

        public FrmValidar_Eligibilidad()
        {
            InitializeComponent();
            Iniciar_Pantalla(); 
        }

        private void FrmValidar_Eligibilidad_Load(object sender, EventArgs e)
        {
            Iniciar_Pantalla();
        }

        #region Botones
        private void Iniciar_Pantalla()
        {
            Fg.IniciaControles();
            lblFolioRecetaAsociado.Enabled = false; 
            IniciarToolBar(); 

            //////if(DtGeneral.EsEquipoDeDesarrollo)
            //////{
            //////    txtFolioEligibilidad.Text = "E006493943"; 
            //////}

            txtFolioEligibilidad.Focus();
        }

        private void IniciarToolBar()
        {
            IniciarToolBar(true, false); 
        }

        private void IniciarToolBar(bool Validar, bool SurtirReceta)
        {
            btnvalidarElegibilidad.Enabled = Validar;
            btnSurtirReceta.Enabled = SurtirReceta;
            txtFolioReceta.Enabled = SurtirReceta; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Iniciar_Pantalla(); 
        }

        private void btnvalidarElegibilidad_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true, FrameInformacion);
            Application.DoEvents();
            System.Threading.Thread.Sleep(100); 

            txtFolioEligibilidad.Enabled = false;
            IniciarToolBar(false, false);

            Validar_Elegibilidad();

            if (validarElegibilidad.Elegilidad_Valida_ParaSurtido)
            {
                lblFolioRecetaAsociado.Enabled = true; 
                txtFolioEligibilidad.Enabled = false;
                IniciarToolBar(false, true);
                txtFolioReceta.Focus(); 
            }
            else
            {
                txtFolioEligibilidad.Enabled = true;
                IniciarToolBar(true, false);
            }

            lblResultadoValidacion.ForeColor = validarElegibilidad.Color__Elegilidad_Valida_ParaSurtido; 
            lblResultadoValidacion.Text = validarElegibilidad.Mensaje__Elegilidad_Valida_ParaSurtido; 
        }

        private void btnSurtirReceta_Click(object sender, EventArgs e)
        {
            validarReceta(true); 
        }

        private bool validarReceta(bool MostrarCapturaRecetaManual)
        {
            bool bRespuesta = false;


            bRespuesta = !validarElegibilidad.Validar_FolioDeReceta(txtFolioEligibilidad.Text, txtFolioReceta.Text, validarElegibilidad.MA_FolioDeConsecutivo);

            if (bRespuesta  || validarElegibilidad.MA_EsRecetaManual)
            {
                if (validarElegibilidad.RecetaSurtida)
                {
                    General.msjUser("El folio de receta ya fue surtido, receta invalida para surtido."); 
                }
                else 
                {
                    if (!bAtencionDeRecetasManuales)
                    {
                        General.msjUser("El folio de receta invalido para surtido."); 
                    }
                    else
                    {
                        bRespuesta = General.msjConfirmar("Folio de receta electrónica no encontrado.\n\n¿ Desea habilitar la captura manual de receta ?") == System.Windows.Forms.DialogResult.Yes;
                    }
                }

                btnSurtirReceta.Enabled = !bRespuesta;

                if (bRespuesta && validarElegibilidad.MA_EsRecetaManual)
                {
                    validarElegibilidad.MA_FolioDeReceta_Manual = txtFolioReceta.Text; 
                    FrmRecetasManuales f = new FrmRecetasManuales(validarElegibilidad);
                    f.ShowDialog();

                    if (!f.RecetaManualGuardada)
                    {
                        btnSurtirReceta.Enabled = true; 
                    }
                    else 
                    {
                        Validar_Elegibilidad();
                        validarElegibilidad.MA_FolioDeConsecutivo = f.localElegibilidad.MA_FolioDeConsecutivo;

                        if (validarElegibilidad.Validar_FolioDeReceta(txtFolioEligibilidad.Text, txtFolioReceta.Text, validarElegibilidad.MA_FolioDeConsecutivo))
                        {
                            FrmVentas_MA fm = new FrmVentas_MA(validarElegibilidad, false);
                            fm.ShowDialog();
                        }
                    }
                }
            }
            else
            {
                FrmVentas_MA f = new FrmVentas_MA(validarElegibilidad, false);
                f.ShowDialog(); 
            }

            return bRespuesta; 
        }

        private void btnAbrirDispensacion_Click(object sender, EventArgs e)
        {
            FrmVentas_MA f = new FrmVentas_MA();
            f.ShowDialog(); 
        }

        ////private void btnSurtirReceta_Manual_Click(object sender, EventArgs e)
        ////{

        ////}

        private void Validar_Elegibilidad()
        {
            validarElegibilidad = new clsValidar_Elegibilidad();
            validarElegibilidad.Validar_Elegibilidad(GnDll_SII_IMediaccess.IdFarmacia_MA, txtFolioEligibilidad.Text.Trim());
            lblEmpresa.Text = validarElegibilidad.EmpresaRazonSocial;
            lblProducto.Text = validarElegibilidad.PlanProducto; 
            lblBeneficiario.Text = validarElegibilidad.NombreBeneficiario;
            lblClaveProveedor.Text = validarElegibilidad.ReferenciaProveedor;
            lblProveedor.Text = validarElegibilidad.NombreProveedor;
            lblCopago.Text = string.Format("En {0}, {1} ", validarElegibilidad.CopagoEN, validarElegibilidad.Copago);
            lblCopago.Text = validarElegibilidad.TituloCopago;
            lblFolioRecetaAsociado.Text = validarElegibilidad.MA_FolioDeReceta_Asociado; 

        }
        #endregion Botones
    }
}
