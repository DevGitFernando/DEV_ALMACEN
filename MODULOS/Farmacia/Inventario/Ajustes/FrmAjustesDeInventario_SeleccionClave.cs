using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;

namespace Farmacia.Inventario
{
    public partial class FrmAjustesDeInventario_SeleccionClave : FrmBaseExt
    {
        #region Declaracion de variables
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas consulta;
        clsAyudas ayuda;


        clsListView lst;
        DataSet dtsListaDeClaves;
        clsLeer leerClaves = new clsLeer(); 

        public string ClaveSSA = ""; 
        public bool ClaveCapturada = false; 

        #endregion Declaracion de variables

        public FrmAjustesDeInventario_SeleccionClave()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            consulta = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);

            txtCambio.Left = lblDescripcion.Left;
            txtCambio.Top = lblDescripcion.Top + 20;

            leerClaves = new clsLeer();
            PrepararDataSet();
            lst = new clsListView(listClaves); 
        }

        private void FrmAjustesDeInventario_SeleccionClave_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this);

            lst.CargarDatos(leerClaves.DataSetClase, false, false); 
            lst.AnchoColumna(1, 100);
            lst.AnchoColumna(2, 600);
            txtClave.Focus();

            txtClave.Focus(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validaDatos())
            {
                ClaveCapturada = leerClaves.Registros > 0;
                ClaveSSA = txtClave.Text.Trim();
                this.Hide(); 
            }
        }

        private void btnEliminarClave_Click(object sender, EventArgs e)
        {
            EliminarClave();
        }

        private void listClaves_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                EliminarClave();
            }
        }

        private void btnAgregarClave_Click(object sender, EventArgs e)
        {
            Agregar(txtClave.Text, lblDescripcion.Text);
        }

        private bool validaDatos()
        {
            bool bRegresa = true; 

            //////if (txtClave.Text.Trim() == "")
            //////{
            //////    bRegresa = false;
            //////    General.msjUser("No capturado una Clave SSA válida, verifique."); 
            //////    txtClave.Focus(); 
            //////} 

            return bRegresa;
        } 
        #endregion Botones

        #region Eventos 
        private void txtClave_TextChanged(object sender, EventArgs e)
        {
            lblDescripcion.Text = "";
            // txtClave_Validating(null, null); 
        }

        private void txtClave_Validating(object sender, CancelEventArgs e)
        {
            if (txtClave.Text.Trim() != "")
            {
                leer.DataSetClase = consulta.ClavesSSA_Sales(txtClave.Text.Trim(), true, "txtClave_Validating");
                if (leer.Leer())
                {
                    CargarDatos();
                } 
            }
        }

        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.ClavesSSA_Sales("txtClave_KeyDown");
                if (leer.Leer())
                {
                    CargarDatos(); 
                }
            }
        }

        private void CargarDatos()
        {
            txtClave.Text = leer.Campo("ClaveSSA");
            lblDescripcion.Text = leer.Campo("Descripcion"); 
        } 
        #endregion Eventos

        #region Propiedades publicas
        public int NumClaves
        {
            get { return leer.Registros; }
        }

        public DataSet ClavesSSA
        {
            get 
            {
                if (dtsListaDeClaves == null)
                {
                    PrepararDataSet(); 
                }
                return dtsListaDeClaves; 
            }
            set
            {
                dtsListaDeClaves = value;
                leerClaves.DataSetClase = dtsListaDeClaves;
            }
        }

        public string ListaDeClavesSSA
        {
            get { return GetListaDeClaves(); }
            set { ; }
        }
        #endregion Propiedades publicas

        #region Funciones y Procedimientos Publicos
        public DataSet Agregar(string ClaveSSA, string Descripcion)
        {
            clsLeer leerAdd = new clsLeer();

            ClaveSSA = ClaveSSA.Trim();
            Descripcion = Descripcion.Trim();

            leerAdd.DataRowsClase = leerClaves.Tabla(1).Select(string.Format(" ClaveSSA = '{0}' ", ClaveSSA));

            if (!leerAdd.Leer())
            {
                object[] obj = { ClaveSSA, Descripcion };
                dtsListaDeClaves.Tables[0].Rows.Add(obj);
            }

            txtClave.Text = "";
            lblDescripcion.Text = ""; 

            leerClaves.DataSetClase = dtsListaDeClaves;            
            lst.LimpiarItems();
            lst.CargarDatos(dtsListaDeClaves, true, false);
            lst.AnchoColumna(1, 100);
            lst.AnchoColumna(2, 600);
            txtClave.Focus(); 

            return dtsListaDeClaves;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private void EliminarClave()
        {
            lst.EliminarRenglonSeleccionado();
            GuardarClaves();
        }

        private string GetListaDeClaves()
        {
            string sRegresa = "";

            leerClaves.RegistroActual = 1; 
            while (leerClaves.Leer())
            {
                sRegresa += string.Format("'{0}', ", leerClaves.Campo("ClaveSSA"));
            }

            if (sRegresa == "")
            {
                sRegresa = " '' "; 
            }
            else 
            {
                sRegresa = sRegresa.Trim();
                sRegresa = Fg.Mid(sRegresa, 1, sRegresa.Length - 1);
            }

            return sRegresa;
        }

        private void GuardarClaves()
        {
            string sClaveSSA = "";
            string sDescripcion = "";

            PrepararDataSet();

            for (int i = 1; i <= lst.Registros; i++)
            {
                sClaveSSA = lst.GetValue(i, 1);
                sDescripcion = lst.GetValue(i, 2);

                object[] obj = { sClaveSSA, sDescripcion };
                dtsListaDeClaves.Tables[0].Rows.Add(obj);
            }

            leerClaves.DataSetClase = dtsListaDeClaves;
        }

        private void PrepararDataSet()
        {
            dtsListaDeClaves = new DataSet("Dts_Claves");
            DataTable dt = new DataTable("Claves");

            dt.Columns.Add("ClaveSSA", typeof(string));
            dt.Columns.Add("Descripcion", typeof(string));

            dtsListaDeClaves.Tables.Add(dt.Clone());
            leerClaves.DataSetClase = dtsListaDeClaves; 
        }
        #endregion Funciones y Procedimientos Privados 
    }
}
