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

using DllFarmaciaSoft;

namespace Almacen.Pedidos
{
    public partial class FrmSegmento : FrmBaseExt
    {
        //int iIdGrupo = 0;
        public bool bAceptar = false;
        public string sFolioSurtido = "";

        // clsGuardarSC Guardar = new clsGuardarSC();
        // clsGruposDeUsuarios GrupoUsuario;

        clsConsultas query;
        clsAyudas Ayuda;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        string IdEmpresa = DtGeneral.EmpresaConectada;
        string IdEstado = DtGeneral.EstadoConectado;
        string IdFarmacia = DtGeneral.FarmaciaConectada;
        string IdPersonal = DtGeneral.IdPersonal;

        public FrmSegmento()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);

            query = new clsConsultas(General.DatosConexion, "Segmento", this.Name, Application.ProductVersion, true);
            Ayuda = new clsAyudas(General.DatosConexion, "Segmento", this.Name, Application.ProductVersion);

        }

        private void FrmSegmento_Load(object sender, EventArgs e)
        {
            txtIdPersonal.Text = "";
            lblNombrePersonal.Text = "";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bAceptar = false;
            this.Hide();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            string sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Segmentos @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @IdPersonal_Segmento = '{4}'",
                    IdEmpresa, IdEstado, IdFarmacia, sFolioSurtido, txtIdPersonal.Text);

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un errro al registrar el segmento.");
            }
            else
            {
                bAceptar = true;
                this.Hide();
            }


            //GrupoUsuario = new clsGruposDeUsuarios();

            //GrupoUsuario.IdSucursal = General.EntidadConectada;
            //GrupoUsuario.IdGrupo = iIdGrupo;
            //GrupoUsuario.NombreGrupo = txtNombreGrupo.Text;

            //if (Guardar.Exec(GrupoUsuario.PreparaSp()))
            //{
            //    bAceptar = true;
            //    this.Hide();
            //}
        }

        private void txtIdPersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Personal("txtIdPersonal_KeyDown", IdEstado, IdFarmacia);
                if (leer.Leer())
                    CargarDatosPersonal();
            }
        }

        private void txtIdPersonal_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdPersonal.Text.Trim() != "")
            {
                query.MostrarMsjSiLeerVacio = true;
                leer.DataSetClase = query.Personal(IdEstado, IdFarmacia, txtIdPersonal.Text, "txtIdPersonal_Validating");

                if (leer.Leer())
                {
                    txtIdPersonal.Enabled = false;
                    CargarDatosPersonal();
                }
            }
        }

        private void CargarDatosPersonal()
        {
            string sNombre = leer.Campo("Nombre") + " " + leer.Campo("ApPaterno") + " " + leer.Campo("ApMaterno");

            txtIdPersonal.Text = leer.Campo("IdPersonal");
            lblNombrePersonal.Text = sNombre;

            if (leer.Campo("Status").ToUpper() == "C")
            {
                //btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                General.msjUser("El personal " + sNombre + " actualmente se encuentra cancelado.");
            }
        }
    }
}
