using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;

namespace DllPedidosClientes.Usuarios_y_Permisos
{
    internal partial class FrmFileConfigXml : FrmBaseExt
    {
        public string sUsarWebService = "";
        public string sServer = "";
        public string sWebService = "";
        public string sPaginaASMX = "";
        public bool bAceptar = false;

        public FrmFileConfigXml()
        {
            InitializeComponent();
        }

        private void FrmFileConfigXml_Load(object sender, EventArgs e)
        {
            cboUsarWebService.Add("", "");
            cboUsarWebService.Add("SI", "Si");
            cboUsarWebService.Add("NO", "No");
            cboUsarWebService.SelectedIndex = 1;
            cboUsarWebService.Enabled = false;

            txtServidor.Text = sServer;
            txtWebService.Text = sWebService;

            //cboUsarWebService.Data = sUsarWebService;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                sServer = txtServidor.Text.Trim();
                sWebService = txtWebService.Text.Trim();
                sUsarWebService = cboUsarWebService.Data;
                sPaginaASMX = txtPagina.Text.Trim();
                bAceptar = true;
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

            if ( cboUsarWebService.Data.ToUpper() == "Si".ToUpper() )
            {
                if (txtServidor.Text.Trim() == "")
                {
                    bRegresa = false;
                    MessageBox.Show("Falta proporcionar el nombre del servidor");
                    txtServidor.Focus();
                }

                if (txtWebService.Text.Trim() == "" && bRegresa)
                {
                    bRegresa = false;
                    MessageBox.Show("Falta proporcionar el webservice");
                    txtWebService.Focus();
                }

                if (txtPagina.Text.Trim() == "" && bRegresa)
                {
                    bRegresa = false;
                    MessageBox.Show("Falta proporcionar la pagina del webservice");
                    txtPagina.Focus();
                }

            }

            return bRegresa;
        }

        private void FrmFileConfigXml_KeyDown(object sender, KeyEventArgs e)
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
    }
}