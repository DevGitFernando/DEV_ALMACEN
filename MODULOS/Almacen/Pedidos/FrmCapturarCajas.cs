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

using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft; 

namespace Almacen.Pedidos
{
    public partial class FrmCapturarCajas : FrmBaseExt 
    {
        public bool AplicarCambio = false;
        clsGrid grid;

        int idUnique = 0;
        DataSet datosEmbalaje;
        DataSet dtsCaptura;
        DataTable dtFiltrados;
        DateTime dtFechaModificacion;

        public DataSet InformacionEmbalaje
        {
            get { return datosEmbalaje.Copy(); }
        }

        public FrmCapturarCajas(int Id, DataSet CajasEmbalaje )
        {
            InitializeComponent();

            idUnique = Id;
            datosEmbalaje = CajasEmbalaje;


            dtFechaModificacion = General.FechaSistemaObtener();

            grid = new clsGrid(ref grdCajas, this);
            grdCajas.EditModeReplace = true;
            grid.AjustarAnchoColumnasAutomatico = true;

        }

        private void FrmCapturarCajas_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            clsLeer datos = new clsLeer();
            datos.DataRowsClase = datosEmbalaje.Tables[0].Select(string.Format(" Id = '{0}' ", idUnique));

            ////txtCaja_01_Inicial.Minimum = 0;
            ////txtCaja_01_Inicial.Maximum = 1000;
            ////txtCaja_01_Inicial.Value = Caja_Inicial;

            ////txtCaja_02_Final.Minimum = 0;
            ////txtCaja_02_Final.Maximum = 1000;
            ////txtCaja_02_Final.Value = Caja_Final;


            grid.Limpiar(false);

            if(datos.Leer())
            {
                grid.LlenarGrid(datos.DataSetClase, false, false);
            }

            if(grid.Rows == 0)
            {
                AgregarRenglon();
            } 
        }

        private void FrmCapturarCajas_KeyDown( object sender, KeyEventArgs e )
        {
            switch(e.KeyCode)
            {
                case Keys.F5:
                    ActualizarResultado();
                    break;

                case Keys.F12:
                    this.Hide();
                    break;

                default:
                    break; 
            }
        }

        private void btnAceptar_Click( object sender, EventArgs e )
        {
            ActualizarResultado(); 
        }

        private void ActualizarResultado() 
        {
            AplicarCambio = true;
            clsLeer leerFiltro = new clsLeer();
            DataTable dtPaso = null;

            //dtsCaptura = datosEmbalaje.Clone();
            dtFechaModificacion = General.FechaSistemaObtener();

            leerFiltro.DataRowsClase = datosEmbalaje.Tables[0].Select(string.Format(" Id <> '{0}' ", idUnique));
            dtFiltrados = leerFiltro.DataTableClase;

            dtPaso = datosEmbalaje.Tables[0].Clone(); 

            for(int i = 1; i <= grid.Rows; i++)
            {
                object[] objRow =
                        {
                            grid.GetValueInt(i, 1),
                            grid.GetValueInt(i, 2),
                            dtFechaModificacion,
                            grid.GetValueInt(i, 3),
                            grid.GetValueInt(i, 4) 
                        };
                dtPaso.Rows.Add(objRow);
            }

            dtFiltrados.Merge(dtPaso.Copy());
            leerFiltro.DataTableClase = dtFiltrados;
            datosEmbalaje = leerFiltro.DataSetClase; 

            this.Hide(); 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide(); 
        }

        #region Grid 
        private void grdFolios_Advance( object sender, FarPoint.Win.Spread.AdvanceEventArgs e )
        {
            if((grid.ActiveRow == grid.Rows) && e.AdvanceNext)
            {
                if(grid.GetValueInt(grid.ActiveRow, 4) != 0 && grid.GetValueInt(grid.ActiveRow, 5) != 0)
                {
                    AgregarRenglon();
                }
            }
        }

        private void AgregarRenglon()
        {
            grid.Rows = grid.Rows + 1;
            grid.ActiveRow = grid.Rows;
            grid.SetValue(grid.Rows, 1, idUnique);
            grid.SetValue(grid.Rows, 2, grid.Rows);
            grid.SetActiveCell(grid.Rows, 4);
        }
        #endregion Grid 

        private void grdCajas_KeyDown( object sender, KeyEventArgs e )
        {
            if(e.KeyCode == Keys.Delete)
            {
                grid.DeleteRow(grid.ActiveRow);

                if(grid.Rows == 0)
                {
                    AgregarRenglon();
                }
            }
        }

        private void grdCajas_EditModeOff(object sender, EventArgs e)
        {
            int iInicio = grid.GetValueInt(grid.ActiveRow, 4);
            int iFinal = grid.GetValueInt(grid.ActiveRow, 5);

            if (iInicio > iFinal)
            {
                General.msjAviso("La cantidad inicial no puede ser menor a la final.");
                if (grid.ActiveCol == 4)
                {
                    grid.SetValue(grid.ActiveRow, 5, iInicio);
                }
                else
                {
                    grid.SetValue(grid.ActiveRow, 4, iFinal);
                }
            }
        }
    }
}
