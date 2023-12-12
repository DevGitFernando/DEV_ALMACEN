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

namespace Farmacia.Vales_Servicio_A_Domicilio
{
    public partial class FrmValesTipoSurtimiento : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        FrmRegistroVales_ServicioDomicilio fr;

        string sFolioServicioDomicilio = "";
        int iTipoSurtimiento = 0;
        
        public FrmValesTipoSurtimiento()
        {
            InitializeComponent();
            cnn.SetConnectionString();
                   
            leer = new clsLeer(ref cnn);
            fr = new FrmRegistroVales_ServicioDomicilio();

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
        }

        private void FrmValesTipoSurtimiento_Load(object sender, EventArgs e)
        {

        }

        #region Levantar_Pantalla
        public void MostrarPantalla(string Folio)
        {
            this.sFolioServicioDomicilio = Folio;
            this.ShowDialog();
        }
        #endregion Levantar_Pantalla

        #region Botones
        private void btnTransf_Click(object sender, EventArgs e)
        {
            fr.MostrarPantalla(sFolioServicioDomicilio, 1);
            //iTipoSurtimiento = 1;
            this.Close();
        }

        private void btnCompra_Click(object sender, EventArgs e)
        {
            fr.MostrarPantalla(sFolioServicioDomicilio, 2);
            //iTipoSurtimiento = 2;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion Botones

        #region Propiedades
        public int TipoSurtimiento
        {
            get { return iTipoSurtimiento; }
            set { iTipoSurtimiento = value; }
        }
        #endregion Propiedades
    }
}
