using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DtUtileriasBD
{
    public partial class FrmGenerarPassword : Form
    {
        public string Password = ""; 

        public FrmGenerarPassword()
        {
            InitializeComponent(); 
        }

        private void FrmGenerarPassword_Load(object sender, EventArgs e)
        {
            btnLimpiar_Click(null, null);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Password = ""; 
            cboTipo.SelectedIndex = 1;
            cboTipo.Enabled = false; 
            txtServidor.Text = "";
            //txtUsuario.Text = "sa";
            //txtUsuario.Enabled = false; 
            txtPassword.Text = "";
            lblStatus.Visible = false;
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            ////if (cboTipo.SelectedIndex == 0)
            ////{
            ////    bRegresa = false;
            ////    MessageBox.Show("Tipo inválido, verifique.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////}

            if ( bRegresa && txtServidor.Text.Trim() == "" )
            {
                bRegresa = false;
                MessageBox.Show("No ha especificado el Servidor, verifique.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ////if (bRegresa && txtUsuario.Text.Trim() == "" )
            ////{
            ////    bRegresa = false;
            ////    MessageBox.Show("No ha especificado el Usuario, verifique.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////}

            return bRegresa;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            lblStatus.Visible = false;
            Password = ""; 
            if (validarDatos())
            {
                ////if (cboTipo.SelectedIndex == 1)
                ////    txtPassword.Text = clsPassword.SistemaOperativo(txtServidor.Text.Trim(), txtUsuario.Text.Trim());

                ////if (cboTipo.SelectedIndex == 2)
                txtPassword.Text = clsPassword.BaseDeDatos(txtServidor.Text.Trim(), txtUsuario.Text.Trim());
                Password = txtPassword.Text; 

                ////if (cboTipo.SelectedIndex == 3)
                ////    txtPassword.Text = clsPassword.FTP(txtServidor.Text.Trim(), txtUsuario.Text.Trim());

                lblStatus.Visible = true; 
            }
        }

        private void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////if (cboTipo.SelectedIndex == 3)
            ////{
            ////    // txtServidor = modificado;
            ////    txtServidor.PasswordChar = '\0';
            ////}
            ////else
            ////{
            ////    txtServidor.PasswordChar = '*';
            ////}
        }
    }
}
