using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;

namespace Dll_MA_IFacturacion.Configuracion
{
    public partial class FrmPAC_CFD : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer; 

        public FrmPAC_CFD()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
        }

        #region Form 
        private void FrmPAC_CFD_Load(object sender, EventArgs e)
        {
            InicilizarPantalla(); 
        }
        #endregion Form 

        #region Botones 
        private void InicializarToolBar(bool Guardar, bool Cancelar, bool Conectar)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnRevisarDisponibilidad.Enabled = Conectar; 
        }

        private void InicilizarPantalla()
        {
            InicializarToolBar(false, false, false); 
            Fg.IniciaControles();

            txtId.Enabled = false;
            txtId.Text = "01";
            txtNombre.Focus();  
        } 

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicilizarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnRevisarDisponibilidad_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones 
    }
}
