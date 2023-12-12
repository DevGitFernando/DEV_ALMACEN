using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales; 

using SC_SolutionsSystem.ExportarDatos;
using DllFarmaciaSoft; 

namespace DllFarmaciaSoft.ReporteadorGenerico
{
    public partial class Frm_RptListaGenerica : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsDatosCliente DatosCliente;
        clsListView lst;
        clsExportarExcelPlantilla xpExcel;

        DataSet dtsDatos;

        string sEncabezado = "Listado";


        public Frm_RptListaGenerica(DataSet Datos, string Encabezado )
        {
            InitializeComponent();

            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, General.DatosApp, this.Name);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");

            lst = new clsListView(lstResultado);

            dtsDatos = Datos;
            sEncabezado = Encabezado;
            this.Text = sEncabezado;
        }

        private void Frm_RptListaGenerica_Load(object sender, EventArgs e)
        {
            lst.CargarDatos(dtsDatos, true, true);
        }


        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            lst.LimpiarItems();
        }


    }
}
