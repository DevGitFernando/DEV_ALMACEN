using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SC_SolutionsSystem;


namespace DllProveedores.Usuarios_y_Permisos
{
    partial class FrmFileConfig : FrmBaseExt
    {
        clsFileConfig File;

        public string sRutaArchivo = "";
        public string sServer = "";
        public string sBD = "";
        public string sUsuario = "";
        public string sPassword = "";
        public string sTipoDBMS = "";
        public bool bAceptar = false;

        public FrmFileConfig()
        {
            InitializeComponent();
        }

        private void FrmFileConfig_Load(object sender, EventArgs e)
        {
            sServer = sServer.Replace("(", "");
            sServer = sServer.Replace(")", "");
            sServer = sServer.Replace(";", "");
            txtServer.Text = sServer;

            sBD = sBD.Replace(";", "");
            txtDB.Text = sBD;

            sUsuario = sUsuario.Replace(";", "");
            txtUser.Text = sUsuario;

            sPassword = sPassword.Replace(";", "");
            txtPass.Text = sPassword;


            cboTipoDBMS.AddItem("0", "<< Seleccione >>");
            cboTipoDBMS.AddItem("1", "Postgres");
            cboTipoDBMS.AddItem("2", "SqlServer");
            cboTipoDBMS.AddItem("3", "MySql");
            cboTipoDBMS.AddItem("4", "Access");

            if (sTipoDBMS.Trim() == "" )
                cboTipoDBMS.SetData("0");
            else
                sTipoDBMS = sTipoDBMS.Replace(";", "");

            cboTipoDBMS.SetData(sTipoDBMS);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                bAceptar = true;
                sServer = txtServer.Text.Trim();
                sBD = txtDB.Text.Trim();
                sUsuario = txtUser.Text.Trim();
                sPassword = txtPass.Text.Trim();
                sTipoDBMS = cboTipoDBMS.Data();


                sServer = sServer.Replace(";", "");
                sBD = sBD.Replace(";", "");
                sUsuario = sUsuario.Replace(";", "");
                sPassword = sPassword.Replace(";", "");
                sTipoDBMS = sTipoDBMS.Replace(";", "");


                if (sServer.Trim().ToUpper() == "LOCAL")
                    sServer = "(" + sServer + ")";

                this.Hide();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtServer.Text.Trim() == "")
            {
                bRegresa = false;
                MessageBox.Show("Falta proporcionar el nombre del servidor");
                txtServer.Focus();
            }

            if (txtDB.Text.Trim() == "" && bRegresa)
            {
                bRegresa = false;
                MessageBox.Show("Falta proporcionar el nombre de la base de datos");
                txtDB.Focus();
            }

            // Access solo usa el Admin
            if (txtUser.Text.Trim() == "" && bRegresa && cboTipoDBMS.Data() != "4" )
            {
                bRegresa = false;
                MessageBox.Show("Falta proporcionar el nombre del usuario");
                txtUser.Focus();
            }

            if (txtPass.Text.Trim() == "" && bRegresa && cboTipoDBMS.Data() != "4")
            {
                bRegresa = false;
                MessageBox.Show("Falta proporcionar la contraseña");
                txtPass.Focus();
            }

            if (cboTipoDBMS.Data() == "0" && bRegresa)
            {
                bRegresa = false;
                MessageBox.Show("Falta proporcionar el tipo de servidor");
            }

            return bRegresa;
        }

        private void FrmFileConfig_KeyDown(object sender, KeyEventArgs e)
        {
            switch ((int)e.KeyValue)
            {
                case (int)Keys.Enter:
                    SendKeys.Send("{TAB}");
                    break;

                case (int)Keys.Escape:
                        SendKeys.Send("+{TAB}");
                    break;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            openFile.ShowDialog();
            sRutaArchivo = openFile.FileName;

            if (sRutaArchivo != "")
            {
                File = new clsFileConfig(sRutaArchivo);
                File.CargaDatos();

                // Recargar los datos
                sServer = File.Servidor;
                sBD = File.BaseDeDatos;
                sUsuario = File.Usuario;
                sPassword = File.Password;
                sTipoDBMS = File.TipoDBMS;


                sServer = sServer.Replace("(", "");
                sServer = sServer.Replace(")", "");
                sServer = sServer.Replace(";", "");
                txtServer.Text = sServer;

                sBD = sBD.Replace(";", "");
                txtDB.Text = sBD;

                sUsuario = sUsuario.Replace(";", "");
                txtUser.Text = sUsuario;

                sPassword = sPassword.Replace(";", "");
                txtPass.Text = sPassword;

                if (sTipoDBMS.Trim() == "")
                    cboTipoDBMS.SetData("0");
                else
                    sTipoDBMS = sTipoDBMS.Replace(";", "");

                cboTipoDBMS.SetData(sTipoDBMS);

                // Destruir la clase
                File = null;

            }

        }
    }
}