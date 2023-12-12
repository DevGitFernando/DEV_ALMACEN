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
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using Dll_MA_IFacturacion; 

namespace MA_Facturacion.GenerarRemisiones
{
    internal partial class FrmListaClavesFinanciamiento : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer; 
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;
        clsListView lst;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;

        string sRubro = "";
        string sDescRubro = "";
        string sConcepto = "";
        string sDescConcepto = "";

        DataSet dtsClaves = new DataSet();

        public FrmListaClavesFinanciamiento()
        {
            InitializeComponent();

            lst = new clsListView(lstClaves);
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name); 
        }

        #region Form 
        private void FrmValidarRemisiones_FoliosInvalidos_Load(object sender, EventArgs e)
        {
            lblRubro.Text = sRubro;
            lblDescRubro.Text = sDescRubro;
            lblConcepto.Text = sConcepto;
            lblDescConcepto.Text = sDescConcepto;
            lst.CargarDatos(dtsClaves, true, true); 
        }
        #endregion Form

        #region Propiedades
        public string Rubro
        {
            get { return sRubro; }
            set { sRubro = value; }
        }

        public string RubroDesc
        {
            get { return sDescRubro; }
            set { sDescRubro = value; }
        }

        public string Concepto
        {
            get { return sConcepto; }
            set { sConcepto = value; }
        }

        public string ConceptoDesc
        {
            get { return sDescConcepto; }
            set { sDescConcepto = value; }
        }

        public DataSet ClavesFinanciamiento
        {
            get { return dtsClaves; }
            set { dtsClaves = value; }
        } 
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public void MostrarClavesSinPrecio(int Tipo)
        {
            if (Tipo == 1)
            {
                this.Text = "Listado de Claves sin precio licitación.";
            }

            if (Tipo == 2)
            {
                this.Text = "Listado de Claves sin precio de administración.";
            }

            this.ShowDialog(); 
        }
        #endregion Funciones y Procedimientos Publicos      
        
    }
}
