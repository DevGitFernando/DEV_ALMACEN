using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;

namespace DllCompras.PerfilesPersonal
{
    public partial class FrmPersonalCompras : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsCriptografo crypto = new clsCriptografo();

        clsConsultas query; // = new clsConsultas(General.DatosConexion, "Configuracion", "FrmPersonal", Application.ProductVersion, true);
        clsAyudas Ayuda;

        public string IdEstado = "";
        public string IdFarmacia = "";
        public string Estado = "";
        public string Farmacia = "";
        public string IdPersonal = "";

        public FrmPersonalCompras()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, "Configuracion", this.Name, Application.ProductVersion, true);
            Ayuda = new clsAyudas(General.DatosConexion, "Configuracion", this.Name, Application.ProductVersion);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError("Configuracion", Application.ProductVersion, this.Name);

        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);

            if (IdPersonal != "")
            {
                txtIdPersonal.Enabled = false;
                txtIdPersonal.Text = IdPersonal;
                txtIdPersonal_Validating(null, null);
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            lblCancelado.Visible = false;
            Fg.IniciaControles(this, true);
            
            lblEstado.Text = Estado;
            lblFarmacia.Text = Farmacia;

            txtIdPersonal.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if ( validarDatos() )
                GuardarInformacion(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            GuardarInformacion(2);
        }

        private bool validarDatos()
        {
            bool bRegresa = true;           

            if (bRegresa && txtIdPersonal.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No se ha capturado la Clave de personal, verifique.");
                txtIdPersonal.Focus();
            }
            

            return bRegresa;
        }        

        private void GuardarInformacion(int Tipo)
        {
            string sStatus = "A";

            if (Tipo != 1)
            {
                sStatus = "C";
            }

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                string sSql = string.Format("Exec spp_Mtto_CFG_COM_Perfiles_Personal '{0}', '{1}', '{2}', '{3}' ",
                    IdEstado, IdFarmacia, txtIdPersonal.Text, sStatus);

                if (!leer.Exec(sSql))
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "GuardarInformacion()");
                    General.msjError("Ocurrió un error al grabar el usuario.");
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamente.");
                    btnNuevo_Click(null, null);
                }

                cnn.Cerrar();
            }
            else
                General.msjAviso("No se pudo conectar con el servidor, intente de nuevo.");
        }

        private void txtIdPersonal_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdPersonal.Text.Trim() != "" )
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
            lblCancelado.Visible = false;
            string sNombre = leer.Campo("Nombre") + " " + leer.Campo("ApPaterno") + " " + leer.Campo("ApMaterno");

            txtIdPersonal.Text = leer.Campo("IdPersonal");
            lblNombrePersonal.Text = sNombre;            

            if (leer.Campo("Status").ToUpper() == "C")
            {
                //btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                lblCancelado.Visible = true;
                General.msjUser("El personal " + sNombre + " actualmente se encuentra cancelado.");
            }
            
        }        

        private void txtIdPersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Personal("txtIdPersonal_KeyDown", IdEstado, IdFarmacia);
                if (leer.Leer())
                {
                    CargarDatosPersonal();
                }
            }
        }

        
    }
}
