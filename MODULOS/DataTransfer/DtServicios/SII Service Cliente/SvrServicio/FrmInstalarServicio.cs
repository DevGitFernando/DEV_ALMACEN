using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;

// using SC_SolutionsSystem;

namespace SII_Servicio_Cliente.SvrServicio
{
    public partial class FrmInstalarServicio : Form
    {
        public int iResultado = 0;
        ServiceController svr;
        // ServiceControllerStatus servStatus;

        public FrmInstalarServicio()
        {
            InitializeComponent();
        }

        private void btnInstalar_Click(object sender, EventArgs e)
        {
            iResultado = 1;
            this.Hide();
        }

        private void btnDesinstalar_Click(object sender, EventArgs e)
        {
            iResultado = 2;
            this.Hide();
        }

        private void btnInicioNormal_Click(object sender, EventArgs e)
        {
            iResultado = 3;
            this.Hide();
        }

        private void FrmInstalarServicio_Load(object sender, EventArgs e)
        {
            RevisarEstado();
        }

        private void RevisarEstado()
        {
            try
            {
                btnInstalar.Enabled = false;
                btnDesinstalar.Enabled = false;
                btnInicioNormal.Enabled = true;

                svr = new ServiceController(SelfInstaller.Servicio);
                svr.Refresh();

                if (svr.Status == ServiceControllerStatus.Running)
                {
                    btnInstalar.Enabled = false;
                    btnInicioNormal.Enabled = false;
                }
                btnDesinstalar.Enabled = true;
            }
            catch
            {
                btnInstalar.Enabled = true;
                btnDesinstalar.Enabled = false;
                btnInicioNormal.Enabled = false;
            }
        }
    }
}
