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
    public partial class FrmRelacion_CodigosEAN : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmRelacion_CodigosEAN()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, General.Modulo, this.Name, General.Version);
            Ayuda = new clsAyudas(General.DatosConexion, General.Modulo, this.Name, General.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.Modulo, General.Version, this.Name);         
        }

        private void FrmRelacion_CodigosEAN_Load(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        #region Botones 
        private void InicializarPantalla()
        {
            Fg.IniciaControles();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones
    }
}
