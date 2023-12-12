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
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;


namespace Dll_MA_IFacturacion.Catalogos
{
    public partial class FrmDireccion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda = new clsAyudas();
        clsConsultas Consultas;

        public bool Exito = false;
        int iTipoProceso = 0;
        public string sIdEstado = "";
        public string sEstado = "";
        public string sIdMunicipio = "";
        public string sMunicipio = "";
        public string sIdColonia = "";
        public string sColonia = "";
        public string sDireccion = "";
        public string sCodigoPostal = "";
        public string sStatus = ""; 

        public FrmDireccion()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            Fg.IniciaControles(); 
        }

        #region Form 
        private void FrmDireccion_Load(object sender, EventArgs e)
        {
            InicializaPantalla();
            CargaInicial(); 
        }

        private void CargaInicial()
        {
            if ( iTipoProceso != 1 ) 
            {
                txtIdEstado.Text = sIdEstado;
                txtIdMunicipio.Text = sIdMunicipio;
                txtIdColonia.Text = sIdColonia;
                txtDomicilio.Text = sDireccion;
                txtCodigoPostal.Text = sCodigoPostal;
                chkActivo.Checked = sStatus == "A" ? true : false;

                txtIdEstado_Validating(null, null);
                txtIdMunicipio_Validating(null, null);
                txtIdColonia_Validating(null, null); 
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                ////case Keys.F5:
                ////    if (btnNuevo.Enabled)
                ////    {
                ////        btnNuevo_Click(null, null);
                ////    }
                ////    break;

                ////case Keys.F6:
                ////    if (btnGuardar.Enabled)
                ////    {
                ////        btnGuardar_Click(null, null);
                ////    }
                ////    break;

                ////case Keys.F7:
                ////    if (btnCancelar.Enabled)
                ////    {
                ////        btnCancelar_Click(null, null);
                ////    }
                ////    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }
        #endregion Form

        #region Botones 
        private void InicializaPantalla()
        {
            Fg.IniciaControles();
            txtCodigoPostal.Enabled = false; 
            txtIdEstado.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializaPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Exito = true;
                sIdEstado = txtIdEstado.Text.Trim();
                sEstado = lblEstado.Text;
                sIdMunicipio = txtIdMunicipio.Text.Trim();
                sMunicipio = lblMunicipio.Text;
                sIdColonia = txtIdColonia.Text.Trim();
                sColonia = lblColonia.Text;
                sDireccion = txtDomicilio.Text.Trim();
                sCodigoPostal = txtCodigoPostal.Text.Trim();
                sStatus = chkActivo.Checked ? "ACTIVO" : "CANCELADO";
                this.Hide();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones 

        private bool validarDatos()        
        {
            bool bRegresa = true;

            if (bRegresa && txtIdEstado.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Estado válido, verifique.");
                txtIdEstado.Focus(); 
            }

            if (bRegresa && txtIdMunicipio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Municipio válido, verifique.");
                txtIdMunicipio.Focus();
            }

            if (bRegresa && txtIdColonia.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado una Colonia válida, verifique.");
                txtIdColonia.Focus();
            }

            if (bRegresa && txtCodigoPostal.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Código Postal válido, verifique.");
                txtCodigoPostal.Focus();
            }

            return bRegresa; 
        }

        public bool MostrarInterface(int TipoProceso, string IdEstado, string IdMunicipio, string IdColonia, string Direccion, string CodigoPostal, string Status)
        {
            iTipoProceso = TipoProceso;
            sIdEstado = IdEstado;
            sIdMunicipio = IdMunicipio;
            sIdColonia = IdColonia;
            sDireccion = Direccion;
            sCodigoPostal = CodigoPostal;
            sStatus = Status; 

            this.ShowDialog(); 
            return Exito; 
        }

        #region Geograficos
        private void CargarInfEstado()
        {
            txtIdEstado.Text = leer.Campo("IdEstado");
            lblEstado.Text = leer.Campo("Descripcion");
        }

        private void CargarInfMunicipio()
        {
            txtIdMunicipio.Text = leer.Campo("IdMunicipio");
            lblMunicipio.Text = leer.Campo("Descripcion");
        }

        private void CargarInfColonia()
        {
            txtIdColonia.Text = leer.Campo("IdColonia");
            lblColonia.Text = leer.Campo("Descripcion");
            txtCodigoPostal.Text = leer.Campo("CodigoPostal");
        }

        private void txtIdEstado_TextChanged(object sender, EventArgs e)
        {
            lblEstado.Text = "";
            txtIdMunicipio.Text = "";
            lblMunicipio.Text = "";
            lblColonia.Text = "";
        }

        private void txtIdEstado_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdEstado.Text != "")
            {
                leer.DataSetClase = Consultas.Estados(txtIdEstado.Text, "txtIdEstado_KeyDown");
                if (!leer.Leer())
                {
                    txtIdEstado.Focus();
                }
                else
                {
                    CargarInfEstado();
                }
            }
        }

        private void txtIdEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Estados("txtIdEstado_KeyDown");
                if (leer.Leer())
                {
                    CargarInfEstado();
                }
            }
        }

        private void txtIdMunicipio_TextChanged(object sender, EventArgs e)
        {
            lblMunicipio.Text = "";
            txtIdColonia.Text = "";
            lblColonia.Text = "";
        }

        private void txtIdMunicipio_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdEstado.Text.Trim()!= "" && txtIdMunicipio.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Municipios(txtIdEstado.Text, txtIdMunicipio.Text, "txtIdMunicipio_Validating");
                if (!leer.Leer())
                {
                    txtIdMunicipio.Focus();
                }
                else
                {
                    CargarInfMunicipio();
                }
            }
        }

        private void txtIdMunicipio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtIdEstado.Text != "")
                {
                    leer.DataSetClase = Ayuda.Municipios(txtIdEstado.Text, "txtIdEstado_KeyDown");
                    if (leer.Leer())
                    {
                        CargarInfMunicipio();
                    }
                }
            }
        }

        private void txtIdColonia_TextChanged(object sender, EventArgs e)
        {
            lblColonia.Text = "";
            txtCodigoPostal.Text = ""; 
        }

        private void txtIdColonia_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdMunicipio.Text.Trim() != "" && txtIdColonia.Text != "")
            {
                leer.DataSetClase = Consultas.Colonias(txtIdEstado.Text, txtIdMunicipio.Text, txtIdColonia.Text, "txtIdColonia_Validating");
                if (!leer.Leer())
                {
                    txtIdColonia.Focus();
                }
                else
                {
                    CargarInfColonia();
                }
            }
        }

        private void txtIdColonia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtIdMunicipio.Text != "")
                {
                    leer.DataSetClase = Ayuda.Colonias(txtIdEstado.Text, txtIdMunicipio.Text, "txtIdEstado_KeyDown");
                    if (leer.Leer())
                    {
                        CargarInfColonia();
                    }
                }
            }
        }
        #endregion Geograficos

        #region Botones auxiliares
        private void btnEstados_Click(object sender, EventArgs e)
        {
            FrmEstados f = new FrmEstados();
            f.ShowDialog();
        }

        private void btnMunicipios_Click(object sender, EventArgs e)
        {
            FrmMunicipios f = new FrmMunicipios();
            f.ShowDialog();
        }

        private void btnColonias_Click(object sender, EventArgs e)
        {
            FrmColonias f = new FrmColonias();
            f.ShowDialog();
        }
        #endregion Botones auxiliares
    }
}
