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

namespace Almacen.Ubicaciones
{
    public partial class FrmRptUbicacionesClavesDetalle : FrmBaseExt
    {
        private enum Cols
        {
            IdProducto = 1, CodigoEAN = 2, Descripcion = 3, ClaveLote = 4, FechaCaducidad = 5, Existencia = 6 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;
        string sSql = ""; 

        public FrmRptUbicacionesClavesDetalle(string SQL)
        {
            InitializeComponent();

            General.Pantalla.AjustarTamaño(this, 70, 60);

            leer = new clsLeer(ref cnn);
            grid = new clsGrid(ref grdProductos, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            grid.AjustarAnchoColumnasAutomatico = true;
            grid.SetOrder((int)Cols.Existencia, 1, true); 

            sSql = SQL; 
        }

        private void FrmRptUbicacionesClavesDetalle_Load(object sender, EventArgs e)
        {
            CargarInformacion(); 
        }

        private void CargarInformacion()
        {
            int iRenglon = 1;

            grid.Limpiar(false);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer.Error, "Cargargrid");
                General.msjError("Ocurrió Un Error al buscar la Información.");
            }
            else
            {
                while (leer.Leer())
                {
                    grid.AddRow();

                    grid.SetValue(iRenglon, (int)Cols.IdProducto, leer.Campo("IdProducto"));
                    grid.SetValue(iRenglon, (int)Cols.CodigoEAN, leer.Campo("CodigoEAN"));
                    grid.SetValue(iRenglon, (int)Cols.Descripcion, leer.Campo("DescripcionProducto"));
                    grid.SetValue(iRenglon, (int)Cols.ClaveLote, leer.Campo("ClaveLote"));
                    grid.SetValue(iRenglon, (int)Cols.FechaCaducidad, leer.Campo("FechaCaducidad"));
                    grid.SetValue(iRenglon, (int)Cols.Existencia, leer.Campo("Existencia"));
                    iRenglon++;
                }
            }

            lblTotal.Text = grid.TotalizarColumna((int)Cols.Existencia).ToString("###,###,###,##0"); 
        }
    }
}
