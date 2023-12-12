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
    public partial class FrmRptUbicacionesClavesDetalleLotes : FrmBaseExt
    {
        private enum Cols
        {
            Pasillo = 1, Estante = 2, Entrepano = 3, 
            IdProducto = 4, CodigoEAN = 5, 
            ClaveLote = 6, FechaCaducidad = 7, Existencia = 8  
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;
        string sSql = ""; 

        public FrmRptUbicacionesClavesDetalleLotes(string SQL)
        {
            InitializeComponent();

            //General.Pantalla.AjustarTamaño(this, 80, 70);
            General.Pantalla.AjustarTamaño(this, 70, 60);

            leer = new clsLeer(ref cnn);
            grid = new clsGrid(ref grdProductos, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            grid.AjustarAnchoColumnasAutomatico = true;
            grid.SetOrder((int)Cols.Existencia, 1, true); 

            sSql = SQL; 
        }

        private void FrmRptUbicacionesClavesDetalleLotes_Load(object sender, EventArgs e)
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

                    grid.SetValue(iRenglon, (int)Cols.Pasillo, leer.Campo("IdPasillo"));
                    grid.SetValue(iRenglon, (int)Cols.Estante, leer.Campo("IdEstante"));
                    grid.SetValue(iRenglon, (int)Cols.Entrepano, leer.Campo("IdEntrepaño"));

                    grid.SetValue(iRenglon, (int)Cols.IdProducto, leer.Campo("IdProducto"));
                    grid.SetValue(iRenglon, (int)Cols.CodigoEAN, leer.Campo("CodigoEAN"));                    
                    
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
