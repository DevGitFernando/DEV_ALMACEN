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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.wsFarmacia;


namespace DllCompras.Informacion
{
    public partial class FrmListaClavesReporte : FrmBaseExt
    {
        clsListView lst; 
        DataSet dtsListaDeClaves;
        clsLeer leer = new clsLeer(); 

        public FrmListaClavesReporte()
        {
            InitializeComponent();
            PrepararDataSet();

            leer.DataSetClase = dtsListaDeClaves;
            lst = new clsListView(listClaves); 

        }

        private void FrmListaClavesReporte_Load(object sender, EventArgs e)
        {
            lst.CargarDatos(leer.DataSetClase, false, false); 
        }

        private void FrmListaClavesReporte_FormClosing(object sender, FormClosingEventArgs e)
        {
            //GuardarClaves(); 
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

        #region Propiedades publicas 
        public int NumClaves
        {
            get { return leer.Registros; }
        }

        public DataSet ClavesSSA
        {
            get { return dtsListaDeClaves; }
            set 
            {
                dtsListaDeClaves = value;
                leer.DataSetClase = dtsListaDeClaves; 
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

            leerAdd.DataRowsClase = leer.Tabla(1).Select(string.Format(" ClaveSSA = '{0}' ", ClaveSSA));

            if (!leerAdd.Leer())
            {
                object[] obj = { ClaveSSA, Descripcion };
                dtsListaDeClaves.Tables[0].Rows.Add(obj); 
            }

            leer.DataSetClase = dtsListaDeClaves; 
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

            leer.RegistroActual = 1;
            while (leer.Leer())
            {
                sRegresa += string.Format("'{0}', ", leer.Campo("ClaveSSA")); 
            }

            if (sRegresa != "")
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

            leer.DataSetClase = dtsListaDeClaves; 
        }

        private void PrepararDataSet()
        {
            dtsListaDeClaves = new DataSet("Dts_Claves"); 
            DataTable dt = new DataTable("Claves"); 

            dt.Columns.Add("ClaveSSA", typeof(string));
            dt.Columns.Add("Descripcion", typeof(string)); 

            dtsListaDeClaves.Tables.Add(dt.Clone()); 
        }
        #endregion Funciones y Procedimientos Privados 

    }
}
