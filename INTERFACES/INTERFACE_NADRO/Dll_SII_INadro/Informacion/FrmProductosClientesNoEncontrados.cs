using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_ControlsCS;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;

using Dll_SII_INadro;

namespace Dll_SII_INadro.Informacion
{
    public partial class FrmProductosClientesNoEncontrados : Form
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        
        DataSet dtsProductos;
        DataSet dtsClientes;
        clsListView lstEAN;
        clsListView lstCTE;

        public FrmProductosClientesNoEncontrados()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);

            lstEAN = new clsListView(lstProductos);
            lstEAN.OrdenarColumnas = true;
            lstEAN.PermitirAjusteDeColumnas = true;

            lstCTE = new clsListView(lstClientes);
            lstCTE.OrdenarColumnas = true;
            lstCTE.PermitirAjusteDeColumnas = true;
        }

        private void FrmProductosClientesNoEncontrados_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones_Publicas
        public void Mostrar_Productos_Clientes(DataSet EAN, DataSet CTE)
        {
            dtsProductos = EAN;
            dtsClientes = CTE;

            lstEAN.LimpiarItems();
            lstCTE.LimpiarItems();

            LlenarProductos();
            LlenarClientes();

            this.ShowDialog();            

        }
        #endregion Funciones_Publicas

        #region Funciones
        private void LlenarProductos()
        {
            leer = new clsLeer(ref cnn);

            leer.DataSetClase = dtsProductos;

            if (leer.Leer())
            {
                lstEAN.CargarDatos(leer.DataSetClase, true, true);
            }
        }

        private void LlenarClientes()
        {
            leer = new clsLeer(ref cnn);

            leer.DataSetClase = dtsClientes;

            if (leer.Leer())
            {
                lstCTE.CargarDatos(leer.DataSetClase, true, true);
            }
        }
        #endregion Funciones
    }
}
