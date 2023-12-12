using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;

using DllFarmaciaSoft;


namespace Dll_SII_INadro.Configuracion
{
    public partial class FrmFirmasParaValidaciones : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmFirmasParaValidaciones()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, General.Modulo, this.Name, General.Version);
            Ayuda = new clsAyudas(General.DatosConexion, General.Modulo, this.Name, General.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.Modulo, General.Version, this.Name);         
        }

        private void FrmFirmasParaValidaciones_Load(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        #region Botones 
        private void InicializarPantalla()
        {
            Fg.IniciaControles();

            ////txtNombre_Administrador.Enabled = false;
            ////txtNombre_Director.Enabled = false;
            ////txtNombre_PersonalFarmacia.Enabled = false;

            btnGuardar.Enabled = false; 
            txtIdEstado.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("Exec spp_Mtto_INT_ND_FirmasValidaciones " + 
                " @IdEstado = '{0}', @IdFarmacia = '{1}', @NombreEncargado = '{2}', @NombreDirector = '{3}', @NombreAdministrador = '{4}' ", 
                txtIdEstado.Text, txtIdFarmacia.Text, txtNombre_PersonalFarmacia.Text.Trim(), txtNombre_Director.Text.Trim(), txtNombre_Administrador.Text.Trim()); 

            if (validarDatos())
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbriConexion();
                }
                else
                {
                    cnn.IniciarTransaccion();

                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        leer.Leer();
                        General.msjUser(leer.Campo("Mensaje"));
                        InicializarPantalla();
                    }

                    cnn.Cerrar(); 
                }
            }
        }
        #endregion Botones

        #region Estados 
        private void txtIdEstado_TextChanged(object sender, EventArgs e)
        {
            lblEstado.Text = "";
            txtIdFarmacia.Text = "";
            lblFarmacia.Text = "";
            txtNombre_PersonalFarmacia.Text = "";
            txtNombre_Director.Text = "";
            txtNombre_Administrador.Text = ""; 
        }

        private void txtIdEstado_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdEstado.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Estados(txtIdEstado.Text, "");
                if (leer.Leer())
                {
                    Cargar_InformacionDelEstado(); 
                }
            }
        }

        private void txtIdEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Estados("txtIdEstado_KeyDown()");
                if (leer.Leer())
                {
                    Cargar_InformacionDelEstado();
                }
            }
        }

        private void Cargar_InformacionDelEstado()
        {
            txtIdEstado.Enabled = false;
            txtIdEstado.Text = leer.Campo("IdEstado");
            lblEstado.Text = leer.Campo("Nombre"); 
        }
        #endregion Estados 
    
        #region Farmacias 
        private void txtIdFarmacia_TextChanged(object sender, EventArgs e)
        {
            lblFarmacia.Text = "";
            txtNombre_PersonalFarmacia.Text = "";
            txtNombre_Director.Text = "";
            txtNombre_Administrador.Text = ""; 
        }

        private void txtIdFarmacia_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdFarmacia.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacias(txtIdEstado.Text, txtIdFarmacia.Text, "");
                if (leer.Leer())
                {
                    Cargar_InformacionDeFarmacia();
                }
            }
        }

        private void txtIdFarmacia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Farmacias("txtIdEstado_KeyDown()", txtIdEstado.Text);
                if (leer.Leer())
                {
                    Cargar_InformacionDeFarmacia();
                }
            }
        }

        private void Cargar_InformacionDeFarmacia()
        {
            txtIdFarmacia.Enabled = false;
            txtIdFarmacia.Text = leer.Campo("IdFarmacia");
            lblFarmacia.Text = leer.Campo("Farmacia");


            txtNombre_Administrador.Enabled = true;
            txtNombre_Director.Enabled = true;
            txtNombre_PersonalFarmacia.Enabled = true;
            
            Cargar_InformacionDeFirmas();

            btnGuardar.Enabled = true; 
        }

        private void Cargar_InformacionDeFirmas()
        {
            leer.DataSetClase = Consultas.FirmasDeValidaciones(txtIdEstado.Text, txtIdFarmacia.Text, "");
            if (leer.Leer())
            {
                txtNombre_PersonalFarmacia.Text = leer.Campo("NombreEncargado");
                txtNombre_Director.Text = leer.Campo("NombreDirector");
                txtNombre_Administrador.Text = leer.Campo("NombreAdministrador"); 
            }
        }

        #endregion Farmacias

        #region Guardar informacion 
        private bool validarDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtNombre_PersonalFarmacia.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Nombre del Personal responsable de la Farmacia, verifique"); 
                txtNombre_PersonalFarmacia.Focus(); 
            }

            if (bRegresa && txtNombre_Director.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Nombre del Director de la Unidad, verifique");
                txtNombre_Director.Focus();
            }

            if (bRegresa && txtNombre_Administrador.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Nombre del Administrador de la Unidad, verifique");
                txtNombre_Administrador.Focus();
            }


            
            return bRegresa;
        }
        #endregion Guardar informacion
    }
}
