using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.ExportarDatos; 
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.SQL;

using DllFarmaciaSoft; 

namespace DllFarmaciaSoft.Inventario
{
    public partial class FrmIncidencias : FrmBaseExt
    {
        clsLeer leer = new clsLeer(); 
        DataSet dtsDatosIndicencias = new DataSet();
        clsListView lst;
        clsExportarExcel excel;

        public  bool ExcluirIncidencias = false; 

        public FrmIncidencias(DataSet Datos)
            : this("Incidencias encontradas en archivo de inventario", Datos, false) 
        { 
        }

        public FrmIncidencias(DataSet Datos, bool MostrarExcepcion)
            : this("Incidencias encontradas en archivo de inventario", Datos, MostrarExcepcion) 
        { 
        }

        public FrmIncidencias(string Titulo, DataSet Datos):this(Titulo, Datos, false) 
        {
        }

        public FrmIncidencias(string Titulo, DataSet Datos, bool MostrarExcepcion) 
        {
            InitializeComponent();

            this.Text = Titulo; 
            lst = new clsListView(lstDetalles); 
            dtsDatosIndicencias = Datos;

            if (!MostrarExcepcion)
            {
                toolStripBarraMenu.Visible = false;
                FrameIncidencias.Top = 5;
                FrameIncidencias.Height = 430;
                FrameDetalles.Top = FrameIncidencias.Top;
                FrameDetalles.Height = FrameIncidencias.Height; 
            }
        }

        private void FrmIncidencias_Load(object sender, EventArgs e)
        {
            TreeNode myNode;

            twIncidencias.Nodes.Clear();
            myNode = twIncidencias.Nodes.Add("Incidencia");
            myNode.Tag = "-1";
            //myNode.ImageIndex = 1;
            //myNode.SelectedImageIndex = 1;

            leer.DataSetClase = dtsDatosIndicencias;

            for (int i = 1; i <= dtsDatosIndicencias.Tables.Count; i++)
            {
                leer.DataTableClase = dtsDatosIndicencias.Tables[i - 1]; 
                TreeNode myNodeGrupo = myNode.Nodes.Add(leer.Tabla(1).TableName + "  ( " + leer.Tabla(1).Rows.Count.ToString() + " ) " );
                myNodeGrupo.ImageIndex = 1;
                myNodeGrupo.SelectedImageIndex = 1;
                myNodeGrupo.Tag = (i - 1).ToString(); 
            }

            lst.Limpiar(); 
            twIncidencias.EndUpdate();
            myNode.Expand();
        }

        private void twIncidencias_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lst.Limpiar();
            btnExportarAExcel.Enabled = false;

            try
            {
                leer.DataTableClase = dtsDatosIndicencias.Tables[Convert.ToInt32(twIncidencias.SelectedNode.Tag.ToString())];
                lst.CargarDatos(leer.DataSetClase, true, true);

                btnExportarAExcel.Enabled = leer.Registros > 0;

            }
            catch { }

        }

        private void btnExportarAExcel_Click(object sender, EventArgs e)
        {
            //////excel = new clsExportarExcel();
            //////string sRuta = Application.StartupPath + @"\\" + "Incidencias_Inventario.xls";

            //////excel.Exportar(dtsDatosIndicencias, sRuta);
            //////excel.AbrirDocumentoGenerado(); 
             
            FrmExportarExcel exportar = new FrmExportarExcel(leer.DataSetClase);

            exportar.AbrirDocumento = true; 
            if (exportar.Exportar())
            {
            }

        }

        private void btnExcluirErrores_Click(object sender, EventArgs e)
        {
            if (General.msjConfirmar("Desea continuar con el proceso excluyendo las indecidencias encontradas") == System.Windows.Forms.DialogResult.Yes)
            {
                ExcluirIncidencias = true;
                this.Hide(); 
            }
        }
    }
}
