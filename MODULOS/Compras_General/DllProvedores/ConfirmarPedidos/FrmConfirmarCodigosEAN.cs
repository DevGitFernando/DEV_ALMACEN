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

using DllProveedores;
using DllProveedores.Consultas;
using DllProveedores.Clases;

namespace DllProveedores.ConfirmarPedidos
{
    public partial class FrmConfirmarCodigosEAN : FrmBaseExt
    {
        clsLeer LeerLocal;
        DataSet dtsCodigosEAN;
        DataTable dtTablaEAN;
        clsGrid Grid;
        clsConfirmarCodigosEAN CapturarEAN;
        clsProductosCodigosEAN Consultas;
        clsDatosCliente DatosCliente;

        private enum Cols
        {
            Ninguno = 0,
            IdClaveSSA = 1, CodigoEAN = 2, Descripcion = 3, Cantidad = 4
        }
        
        public FrmConfirmarCodigosEAN()
        {
            InitializeComponent();

            Grid = new clsGrid(ref grdProductos, this);
            Grid.EstiloGrid(eModoGrid.Normal);
            CapturarEAN = new clsConfirmarCodigosEAN();
            DatosCliente = new clsDatosCliente(GnProveedores.DatosApp, this.Name, "");
            //Consultas = new clsProductosCodigosEAN(General.Url, DatosCliente);
            Consultas = GnProveedores.CodigosEAN;
        }

        private void FrmConfirmarCodigosEAN_Load(object sender, EventArgs e)
        {
            Inicializa();
        }

        #region Funciones
        private void Inicializa()
        {
            string sDescripcion = "";

            Grid.Limpiar(false);
            Grid.LlenarGrid(dtsCodigosEAN);
            sDescripcion = Grid.GetValue(1, (int)Cols.Descripcion);

            //Si sDescripcion contiene algo se bloquean las columnas ya que significa que no es la primera vez que entra a la pantalla.
            if (sDescripcion != "")
            {
                //Se bloquean las columnas cargadas para que no puedan ser modificadas.
                for (int i = 1; i <= Grid.Rows; i++)
                {
                    Grid.BloqueaCelda(true, i, (int)Cols.CodigoEAN);
                }
            }

            lblCantidadSurtible.Text = Grid.TotalizarColumna(4).ToString();
        }
        public void MostrarDetalle(string sClaveSSA, string sDescripcion, int iCantidadRequerida, DataSet dtsCodigos)
        {
            lblClaveSSA.Text = sClaveSSA;
            lblDescripcionSSA.Text = sDescripcion;
            lblCantidadRequerida.Text = iCantidadRequerida.ToString();
            dtsCodigosEAN = dtsCodigos;
            this.ShowDialog();

        }

        private void GuardarCodigos()
        {
            string sCodigoEAN = "", sDescripcionProducto = "", sCantidadConfirmada = "";

            // Se guardan los CodigosEAN de la venta.
            for (int i = 1; i <= Grid.Rows; i++)
            {
                sCodigoEAN = Grid.GetValue(i, (int)Cols.CodigoEAN);
                sDescripcionProducto = Grid.GetValue(i, (int)Cols.Descripcion);
                sCantidadConfirmada = Grid.GetValueInt(i, (int)Cols.Cantidad).ToString();

                CapturarEAN.AgregarRenglon(lblClaveSSA.Text, sCodigoEAN, sDescripcionProducto, sCantidadConfirmada);
            }
            dtTablaEAN = CapturarEAN.ObtenerTablaEAN();
        }

        public DataTable ObtenerTablaEAN()
        {
            return dtTablaEAN;
        }

        #endregion Funciones

        #region Eventos 
        private void FrmConfirmarCodigosEAN_KeyDown(object sender, KeyEventArgs e)
        {
            int iRow = Grid.ActiveRow;
            int iCantidadRequerida = int.Parse(lblCantidadRequerida.Text), iCantidadSurtible = int.Parse(lblCantidadSurtible.Text);

            if (e.KeyCode == Keys.F12)
            {
                EliminarRenglonesVacios();

                if (iCantidadSurtible <= iCantidadRequerida)
                {
                    GuardarCodigos();
                    this.Hide();
                }
                else 
                {
                    General.msjUser("La Cantidad Surtible no puede ser mayor que la cantidad Requerida");
                }
            }

            //if (e.KeyCode == Keys.Delete)
            //{
            //    Grid.DeleteRow(iRow);

            //    if (Grid.Rows == 0)
            //    {
            //        Grid.AddRow();
            //    }
            //}

        }


        #endregion Eventos

        #region Grid 
        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            string sCodigoEAN = "";

            switch (Grid.ActiveCol)
            {
                case 2:
                    {
                        sCodigoEAN = Grid.GetValue(Grid.ActiveRow, (int)Cols.CodigoEAN);

                        if (sCodigoEAN != "")
                        {
                            Consultas.Buscar_CodigoEAN(lblClaveSSA.Text.Trim(), sCodigoEAN);
                            LeerLocal = Consultas.Local;
                            if (LeerLocal.Leer())
                            {
                                CargaDatosProducto();
                            }
                            else
                            {
                                General.msjUser("El CodigoEAN ingresado no existe ó no pertenece a la Clave SSA solicitada");
                                Grid.LimpiarRenglon(Grid.ActiveRow);
                                Grid.SetActiveCell(Grid.ActiveRow, (int)Cols.CodigoEAN);
                            }
                            
                        }
                    }

                    break;
            }
            lblCantidadSurtible.Text = Grid.TotalizarColumna((int)Cols.Cantidad).ToString();
        }

        private void CargaDatosProducto()
        {
            int iRowActivo = Grid.ActiveRow;
            
            if (!Grid.BuscaRepetido(LeerLocal.Campo("CodigoEAN"), iRowActivo, (int)Cols.CodigoEAN))
            {
                Grid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, LeerLocal.Campo("IdClaveSSA"));
                Grid.SetValue(iRowActivo, (int)Cols.CodigoEAN, LeerLocal.Campo("CodigoEAN"));
                Grid.SetValue(iRowActivo, (int)Cols.Descripcion, LeerLocal.Campo("Descripcion"));
                Grid.SetValue(iRowActivo, (int)Cols.Cantidad, 0);
                Grid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.CodigoEAN);
                Grid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);
            }
            else
            {
                General.msjUser("Este Producto ya se encuentra capturado en otro renglon.");
                Grid.SetValue(Grid.ActiveRow, (int)Cols.CodigoEAN, "");
                limpiarColumnas();
                Grid.SetActiveCell(Grid.ActiveRow, 1);
                Grid.EnviarARepetido();
            }

            
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= Grid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                Grid.SetValue(Grid.ActiveRow, i, "");
            }
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= Grid.Rows; i++) //Renglones.
            {
                if (Grid.GetValue(i, (int)Cols.Descripcion).Trim() == "") //Si la columna Descripcion esta vacia se elimina.
                    Grid.DeleteRow(i);
            }

            //if (Grid.Rows == 0) // Si No existen renglones, se inserta 1.
            //    Grid.AddRow();
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            //if ((Grid.ActiveRow == Grid.Rows) && e.AdvanceNext)
            //{
            //    if (Grid.GetValue(Grid.ActiveRow, (int)Cols.CodigoEAN) != "" )
            //    {
            //        Grid.Rows = Grid.Rows + 1;
            //        Grid.ActiveRow = Grid.Rows;

            //        Grid.SetActiveCell(Grid.Rows, (int)Cols.CodigoEAN);
            //    }
            //}
        }

        #endregion Grid
             

    }
}
