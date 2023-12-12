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
    public partial class FrmReactivar_Elegibilidad : FrmBaseExt
    {
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsValidar_Elegibilidad validarElegibilidad = new clsValidar_Elegibilidad();

        public FrmReactivar_Elegibilidad()
        {
            InitializeComponent();
            leer = new clsLeer(ref con);
        }

        private void FrmReactivar_Elegibilidad_Load(object sender, EventArgs e)
        {
            Iniciar_Pantalla();
        }

        private void Iniciar_Pantalla()
        {
            Fg.IniciaControles();
            //lblFolioRecetaAsociado.Enabled = false;
            IniciarToolBar();

            //////if(DtGeneral.EsEquipoDeDesarrollo)
            //////{
            //////    txtFolioEligibilidad.Text = "E006493943"; 
            //////}

            txtFolioReceta.Enabled = false;
            txtFolioEligibilidad.Focus();
        }

        private void IniciarToolBar()
        {
            IniciarToolBar(true, false);
        }

        private void IniciarToolBar(bool Validar, bool Reactivar)
        {
            btnvalidarElegibilidad.Enabled = Validar;
            btnReactivar.Enabled = Reactivar;
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

            if (!validarElegibilidad.Elegilidad_Valida_ParaSurtido && txtFolioReceta.Text != "")
            {
                txtFolioReceta.Enabled = false;
                txtFolioEligibilidad.Enabled = false;
                IniciarToolBar(false, true);
            }
            else
            {
                txtFolioEligibilidad.Enabled = true;
                IniciarToolBar(true, false);
            }

            lblResultadoValidacion.ForeColor = validarElegibilidad.Color__Elegilidad_Valida_ParaSurtido;
            lblResultadoValidacion.Text = validarElegibilidad.Mensaje__Elegilidad_Valida_ParaSurtido;

        }

        private void Validar_Elegibilidad()
        {
            validarElegibilidad = new clsValidar_Elegibilidad();
            validarElegibilidad.Validar_Elegibilidad(GnDll_SII_IMediaccess.IdFarmacia_MA, txtFolioEligibilidad.Text.Trim());
            lblEmpresa.Text = validarElegibilidad.EmpresaRazonSocial;
            lblBeneficiario.Text = validarElegibilidad.NombreBeneficiario;
            lblClaveProveedor.Text = validarElegibilidad.ReferenciaProveedor;
            lblProveedor.Text = validarElegibilidad.NombreProveedor;
            lblCopago.Text = string.Format("En {0}, {1} ", validarElegibilidad.CopagoEN, validarElegibilidad.Copago);
            lblCopago.Text = validarElegibilidad.TituloCopago;
            txtFolioReceta.Text = validarElegibilidad.MA_FolioDeReceta_Asociado;

        }

        private void btnReactivar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;
            string sMensaje = ""; 
            string  EsAdministrador = DtGeneral.EsAdministrador ? "1":"0";
            string sSql = string.Format("Exec spp_INT_MA__Reactivar_Elegibilidad '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                txtFolioReceta.Text, txtFolioEligibilidad.Text, DtGeneral.IdPersonal, EsAdministrador);


            if (!con.Abrir())
            {
                General.msjErrorAlAbrirConexion(); 
            }
            else 
            {
                con.IniciarTransaccion();

                if (leer.Exec(sSql))
                {
                    leer.Leer();
                    bContinua = leer.CampoBool("Valor");
                    sMensaje = leer.Campo("Mensaje");
                }
                else
                {
                    sMensaje = "Ocurrió un error al reactivar el folio.";
                    bContinua = false;
                }

                if (bContinua)
                {
                    con.CompletarTransaccion(); 
                    General.msjUser(sMensaje);
                    btnNuevo_Click(this, null);
                }
                else
                {
                    Error.GrabarError(leer, "btnGuardar_Click");
                    con.DeshacerTransaccion();
                    General.msjUser(sMensaje);
                }

                con.Cerrar(); 
            }
        }
    }
}
