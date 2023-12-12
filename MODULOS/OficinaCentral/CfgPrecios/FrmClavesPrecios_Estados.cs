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

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace OficinaCentral.CfgPrecios
{
    public partial class FrmClavesPrecios_Estados : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid; 

        private enum Cols
        {
            IdClave = 1, ClaveSSA = 2, Descripcion = 3
        }

        public FrmClavesPrecios_Estados()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);

            grid = new clsGrid(ref grdClaves, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            grid.AjustarAnchoColumnasAutomatico = true;
        }

        private void FrmClavesPrecios_Estados_Load(object sender, EventArgs e)
        {
            DefinirAnchosBase(true); 
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            grid.Limpiar();
            grid.Columns = 3;

            DefinirAnchosBase(true); 
        }

        private void DefinirAnchosBase(bool Inicializar)
        {
            if (Inicializar)
            {
                grid.Limpiar();
                grid.Columns = 3;
            }

            grdClaves.Sheets[0].Columns[((int)Cols.IdClave) - 1].Width = 60;
            grdClaves.Sheets[0].Columns[((int)Cols.ClaveSSA) - 1].Width = 100;
            grdClaves.Sheets[0].Columns[((int)Cols.Descripcion) - 1].Width = 400;

            grid.FrozenColumnas = 3;
            grid.BloqueaGrid(true); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = " Exec spp_EXE_ClavesPrecios_Estados ";
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al solicitar la información.");
            }
            else
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase, true, true);
                    DefinirAnchosBase(false); 
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            grid.ExportarExcel(); 
        }
        #endregion Botones 
    }
}
