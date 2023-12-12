using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;



using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.SQL;

using DllFarmaciaSoft;

namespace Dll_IFacturacion.XSA.ObservacionesCancelacion
{
    internal partial class FrmObservaciones : FrmBaseExt
    {

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnFacturar = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
        clsLeer leer;


        public string Encabezado = "Cancelación de CFDI";
        public string Observaciones = "";
        public bool Aceptar = false;
        public int LargoTexto = 200;

        public string RFC_Receptor = ""; 
        public string ClaveMotivoCancelacion = "";
        public string TipoDocumento = "";
        public string Serie = "";
        public string Folio = "";
        public string UUID_Relacionado = "";


        bool bEsCancelacionConRelacion = false; 

        clsConsultas consulta;

        public FrmObservaciones()
        {
            InitializeComponent();


            leer = new clsLeer(ref cnn);

            consulta = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
        }

        private void FrmObservaciones_Load(object sender, EventArgs e)
        {
            this.Text = Encabezado;
            txtObservaciones.MaxLength = LargoTexto;

            CargarMotivosDeCancelacion();
            cboMotivosCancelacion.Data = ClaveMotivoCancelacion;

            InicializarPantalla();
        }

        private void CargarMotivosDeCancelacion()
        {
            cboMotivosCancelacion.Clear();
            cboMotivosCancelacion.Add("", "<< Seleccione >>");
            cboMotivosCancelacion.Add(consulta.CFDI_MotivosDeCancelacion("CargarMotivosDeCancelacion()"), true, "Clave", "MotivoSAT");

            cboMotivosCancelacion.SelectedIndex = 0;
        }

        private void btnNuevo_Click( object sender, EventArgs e )
        {
            InicializarPantalla();
            bEsCancelacionConRelacion = false;

            Frame_UUID_Relacionado.Enabled = false; 
        }

        private void InicializarPantalla()
        {
            Fg.IniciaControles();

            txtRelacion__UUID.ReadOnly = true;

            cboMotivosCancelacion.Focus(); 
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Serie = "";
            Folio = "";
            UUID_Relacionado = "";


            if (txtObservaciones.Text.Trim() == "")
            {
                General.msjUser("No ha capturado el motivo de cancelación del CFDI, verifique.");
                txtObservaciones.Focus();
            }
            else
            {
                ClaveMotivoCancelacion = cboMotivosCancelacion.Data;

                if(bEsCancelacionConRelacion)
                {
                    Serie = txtRelacion__Serie.Text;
                    Folio = txtRelacion__Folio.Text;
                    UUID_Relacionado = txtRelacion__UUID.Text;
                }

                Observaciones = txtObservaciones.Text.Trim();
                Aceptar = true;
                this.Hide();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Aceptar = false;
            this.Hide();
        }


        #region Sustitucion de CFDI
        private void txtRelacion__Serie_TextChanged( object sender, EventArgs e )
        {
            txtRelacion__Folio.Text = "";
            txtRelacion__UUID.Text = "";
        }

        private void txtRelacion__Folio_TextChanged( object sender, EventArgs e )
        {
            txtRelacion__UUID.Text = "";
        }

        private void txtRelacion__Serie_Validating( object sender, CancelEventArgs e )
        {

        }

        private void txtRelacion__Folio_Validating( object sender, CancelEventArgs e )
        {
            if(txtRelacion__Serie.Text.Trim() != "")
            {
                if(txtRelacion__Folio.Text.Trim() != "")
                {
                    if(!validar__SerieFolio_Relacionado())
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
                "Where E.IdEmpresa = '{0}' and E.IdEstado = '{1}' and E.IdFarmacia = '{2}' and E.Serie = '{3}' and E.Folio = '{4}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtRelacion__Serie.Text.Trim(), txtRelacion__Folio.Text.Trim()
            );

            if(!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "validar__SerieFolio_Relacionado");
            }
            else
            {
                if(!leer.Leer())
                {
                    bRegresa = false;
                    General.msjAviso("No se encontro información del Documento solicitado.");
                }
                else
                {
                    if(bRegresa && RFC_Receptor != leer.Campo("RFC"))
                    {
                        bRegresa = false;
                        General.msjAviso("El CFDI pertenece a un RFC distinto al seleccionado.");
                    }

                    if(bRegresa && TipoDocumento != leer.Campo("IdTipoDocumento"))
                    {
                        bRegresa = false;
                        General.msjAviso("El CFDI no es del mismo tipo de CFDI a cancelar, verifique.");
                    }

                    //if(bRegresa && leer.CampoInt("CanceladoSAT") == 0)
                    //{
                    //    bRegresa = false;
                    //    General.msjAviso("El CFDI tiene Status 'ACTIVO'.\nSe requiere que este cancelado para relacionarlo a la cancelación, verifique.");
                    //}

                    if(bRegresa)
                    {
                        txtRelacion__Serie.ReadOnly = true;
                        txtRelacion__Folio.ReadOnly = true;
                        txtRelacion__UUID.ReadOnly = true;
                        txtRelacion__UUID.Text = leer.Campo("UUID");
                    }
                }
            }

            return bRegresa;
        }
        #endregion Sustitucion de CFDI

        private void cboMotivosCancelacion_SelectedIndexChanged( object sender, EventArgs e )
        {
            bEsCancelacionConRelacion = cboMotivosCancelacion.Data == "01";
            Frame_UUID_Relacionado.Enabled = bEsCancelacionConRelacion;
        }
    }
}
