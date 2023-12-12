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
using DllFarmaciaSoft;

namespace MA_Facturacion.GenerarRemisiones
{
    public partial class FrmRemision_ProgramasDeAtencion : FrmBaseExt
    {
        enum cols
        { 
            Renglon = 1, IdPrograma = 2, Programa = 3, IdSubPrograma = 4, SubPrograma = 5, Procesar = 6 
        }

        clsGrid grid; 
        DataSet dtsProgramas_SubProgramas = new DataSet();
        string sListaDeSeleccion = ""; 

        public FrmRemision_ProgramasDeAtencion(DataSet ListaDeProgramas_SubProgramas)
        {
            InitializeComponent();

            dtsProgramas_SubProgramas = ListaDeProgramas_SubProgramas;

            grid = new clsGrid(ref grdProgramasSubProgramas, this);
            grid.EstiloDeGrid = eModoGrid.Normal; 
        }

        private void FrmRemision_ProgramasDeAtencion_Load(object sender, EventArgs e)
        {
            grid.LlenarGrid(dtsProgramas_SubProgramas); 
        }

        public string Seleccion
        {
            get { return sListaDeSeleccion; } 
        }

        public DataSet SeleccionDataSet 
        {
            get { return dtsProgramas_SubProgramas; }
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grid.LlenarGrid(dtsProgramas_SubProgramas); 
        }

        private void btnGuardarGrid_Click(object sender, EventArgs e)
        {
            clsLeer leer = new clsLeer();
            leer.DataSetClase = dtsProgramas_SubProgramas; 

            for (int i = 1; i <= grid.Rows; i++)
            {
                leer.RegistroActual = i;
                leer.GuardarDatos((int)cols.Procesar, grid.GetValueBool(i, (int)cols.Procesar));
            }

            GenerarListaDeSeleccion(); 
            dtsProgramas_SubProgramas = leer.DataSetClase; 
            this.Hide(); 
        }
        #endregion Botones

        #region Funciones y Procedimientos Privados
        private string GenerarListaDeSeleccion()
        {
            string sItem = ""; 
            sListaDeSeleccion = "";

            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, (int)cols.Procesar))
                {
                    sItem = grid.GetValue(i, (int)cols.IdPrograma);
                    sItem += grid.GetValue(i, (int)cols.IdSubPrograma);
                    sListaDeSeleccion += string.Format("'{0}', ", sItem); 
                }
            }

            // Cortar el exceso de la cadena 
            if (sListaDeSeleccion != "")
            {
                sListaDeSeleccion = sListaDeSeleccion.Trim();
                sListaDeSeleccion = Fg.Mid(sListaDeSeleccion, 1, sListaDeSeleccion.Length - 1);
            }

            return sListaDeSeleccion; 
        }
        #endregion Funciones y Procedimientos Privados

        private void chkMarcarDesmarcar_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)cols.Procesar, chkMarcarDesmarcar.Checked); 
        }
    }
}
