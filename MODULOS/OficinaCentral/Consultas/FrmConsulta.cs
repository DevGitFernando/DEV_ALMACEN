using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft; 

namespace OficinaCentral.Consultas
{
    public partial class FrmConsulta : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsListView lw;
        clsGrid grid; 
        clsConsultas query;
        clsAyudas ayuda; 


        public FrmConsulta()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false);
            ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false); 

            Error = new clsGrabarError(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name); 
            lw = new clsListView(lwResultado);
            
            grid = new clsGrid(ref grdResultado, this);
            grid.EstiloGrid(eModoGrid.ModoRow); 

        }

        private void FrmConsulta_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            lw.Limpiar();
            grdResultado.Sheets[0].Columns.Count = 0; 
            grdResultado.Sheets[0].Rows.Count = 0;    

            Fg.IniciaControles();

            rdoClaveSSA.Checked = false; 
            rdoProductos.Checked = true; 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            lw.Limpiar();
            grdResultado.Sheets[0].DataMember = "";
            grdResultado.Sheets[0].DataSource = null; 
            grdResultado.Sheets[0].Columns.Count = 0;
            grdResultado.Sheets[0].Rows.Count = 0;    

            string sSql = "";
            string sFiltro = ""; // Fg.PonCeros(txtId.Text, txtId.MaxLength); 

            if (rdoProductos.Checked)
            {
                if ( txtId.Text.Trim() != "" ) 
                    sFiltro = string.Format(" Where IdProducto = '{0}' ", Fg.PonCeros(txtId.Text, txtId.MaxLength));

                sSql = " Select * From vw_Productos (nolock) " + sFiltro + " Order by IdProducto " ; 
            }

            if (rdoClaveSSA.Checked)
            {
                if (txtId.Text.Trim() != "")
                    sFiltro = string.Format(" Where IdClaveSSA_Sal = '{0}' ", Fg.PonCeros(txtId.Text, txtId.MaxLength));

                sSql = " Select * From vw_ClavesSSA_Sales (nolock) " + sFiltro + " Order by IdClaveSSA_Sal ";
            }

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al obtener la información."); 
            }
            else
            {
                lw.CargarDatos(leer.DataSetClase);
                lw.AjustarColumnas();

                grdResultado.Sheets[0].DataSource = leer.DataSetClase;
                grdResultado.Sheets[0].DataMember = leer.DataTableClase.TableName;

                int i = 0; 
                foreach (ColumnHeader col in lwResultado.Columns)
                {
                    grdResultado.Sheets[0].Columns[i].Width = (float)col.Width * (float)1.15;
                    grdResultado.Sheets[0].Columns[i].AllowAutoFilter = true;
                    grdResultado.Sheets[0].Columns[i].AllowAutoSort = true;
                    grdResultado.Sheets[0].Columns[i].ShowSortIndicator = true; 
                    i++; 
                } 

                grid.BloqueaGrid(true); 
                // grid.LlenarGrid(leer.DataSetClase, true, true); 
            }
        }

        private void rdoProductos_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoProductos.Checked)
                FormatoCaptura(8); 
        }

        private void rdoClaveSSA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoClaveSSA.Checked)
                FormatoCaptura(4); 
        }

        private void FormatoCaptura(int Max)
        {
            txtId.Text = "";
            txtId.MaxLength = Max;
            lblDescripcion.Text = "";
            txtId.Focus(); 
        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text.Trim() != "")
            {
                if (rdoProductos.Checked)
                {
                    leer.DataSetClase = query.Productos(txtId.Text, "txtId_Validating"); 
                    if ( leer.Leer() )
                    {
                        txtId.Text = leer.Campo("IdProducto");
                        lblDescripcion.Text = leer.Campo("Descripcion"); 
                    }
                }

                if (rdoClaveSSA.Checked)
                {
                    leer.DataSetClase = query.ClavesSSA_Sales(txtId.Text, "txtId_Validating");
                    if (leer.Leer())
                    {
                        txtId.Text = leer.Campo("IdClaveSSA_Sal");
                        lblDescripcion.Text = leer.Campo("Descripcion");
                    }
                } 
            } 
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.KeyCode == Keys.F1 )
            {
                if (rdoProductos.Checked)
                {
                    leer.DataSetClase = ayuda.Productos("txtId_Validating");
                    if (leer.Leer())
                    {
                        txtId.Text = leer.Campo("IdProducto");
                        lblDescripcion.Text = leer.Campo("Descripcion");
                    }
                }

                if (rdoClaveSSA.Checked)
                {
                    leer.DataSetClase = ayuda.ClavesSSA_Sales("txtId_Validating");
                    if (leer.Leer())
                    {
                        txtId.Text = leer.Campo("IdClaveSSA_Sal");
                        lblDescripcion.Text = leer.Campo("Descripcion");
                    }
                }
            }
        }
    }
}
