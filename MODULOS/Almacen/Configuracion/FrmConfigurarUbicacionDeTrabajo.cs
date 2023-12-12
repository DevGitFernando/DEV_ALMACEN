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
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace Almacen.Configuracion
{
    internal partial class FrmConfigurarUbicacionDeTrabajo : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        // clsLeer leer;

        public FrmConfigurarUbicacionDeTrabajo() 
        {
            InitializeComponent();
        }

        private void FrmConfigurarUbicacionDeTrabajo_Load(object sender, EventArgs e)
        {

        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones 

        #region Busqueda de Ubicaciones
        private string Pasillo()
        {
            return DatoUbicacion(string.Format(" IdPasillo = '{0}' ", txtPasillo.Text), 1);
        }

        private string Estante()
        {
            return DatoUbicacion(string.Format(" IdPasillo = '{0}' and IdEstante = '{1}' ",
                txtPasillo.Text, txtEstante.Text), 2);
        }

        private string Entrepano()
        {
            return DatoUbicacion(string.Format(" IdPasillo = '{0}' and IdEstante = '{1}' and IdEntrepaño = '{2}' ",
                txtPasillo.Text, txtEstante.Text, txtEntrepaño.Text), 3);
        }

        private string DatoUbicacion(string Filtro, int Tipo)
        {
            string sRegresa = "";
            string sDato = "";
            clsLeer ubica = new clsLeer();


            // ubica.DataRowsClase = dtsLotesUbicacionesRegistradas.Tables[0].Select(Filtro);

            //if (ubica.Leer())
            ubica.Leer();
            {
                switch (Tipo)
                {
                    case 1:
                        sDato = "PASILLO # " + txtPasillo.Text;
                        sRegresa = ubica.Campo("DescripcionPasillo");
                        break;
                    case 2:
                        sDato = "ESTANTE # " + txtEstante.Text;
                        sRegresa = ubica.Campo("DescripcionEstante");
                        break;
                    case 3:
                        sDato = "ENTREPAÑO # " + txtEntrepaño.Text;
                        sRegresa = ubica.Campo("DescripcionEntrepaño");
                        break;
                }
            }

            sRegresa = sRegresa != "" ? sRegresa : sDato;

            return sRegresa;
        }
        #endregion Busqueda de Ubicaciones

        #region Captura de Ubicacion
        private void txtPasillo_TextChanged(object sender, EventArgs e)
        {
            txtEstante.Text = "";
            txtEntrepaño.Text = "";

            lblPasillo.Text = "";
            lblEstante.Text = "";
            lblEntrepaño.Text = "";
        }

        private void txtEstante_TextChanged(object sender, EventArgs e)
        {
            txtEntrepaño.Text = "";

            lblEstante.Text = "";
            lblEntrepaño.Text = "";
        }

        private void txtEntrepaño_TextChanged(object sender, EventArgs e)
        {
            lblEntrepaño.Text = "";
        }

        private void txtPasillo_Validating(object sender, CancelEventArgs e)
        {
            if (txtPasillo.Text.Trim() != "")
            {
                lblPasillo.Text = Pasillo();
            }
        }

        private void txtEstante_Validating(object sender, CancelEventArgs e)
        {
            if (txtEstante.Text.Trim() != "")
            {
                lblEstante.Text = Estante();
            }
        }

        private void txtEntrepaño_Validating(object sender, CancelEventArgs e)
        {
            if (txtEntrepaño.Text.Trim() != "")
            {
                lblEntrepaño.Text = Entrepano();
            }
        }
        #endregion Captura de Ubicacion 
    }
}
