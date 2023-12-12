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

namespace Dll_IGPI.Configuracion
{
    public partial class FrmClientesTerminales : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        clsConsultas Consultas;
        clsAyudas Ayuda;

        private enum Cols
        {
            Ninguna = 0,
            IdTerminal = 1, Nombre = 2, Asignar = 3, Habilitar = 4, Ventanilla = 5
        }

        public FrmClientesTerminales()
        {
            InitializeComponent();

            cnn.SetConnectionString();
            Consultas = new clsConsultas(General.DatosConexion, IGPI.Modulo, this.Name, IGPI.Version);
            Ayuda = new clsAyudas(General.DatosConexion, IGPI.Modulo, this.Name, IGPI.Version);

            grdTerminales.EditModeReplace = false;
            Grid = new clsGrid(ref grdTerminales, this);
            Grid.EstiloGrid(eModoGrid.Normal);
        }

        private void FrmClientesTerminales_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            tmIGPI.Enabled = true;
            tmIGPI.Start();  
        }

        #region Buscar Cliente 
        private void txtIdCliente_Validating(object sender, CancelEventArgs e)
        {
            leer = new clsLeer(ref cnn);

            if (txtIdCliente.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Clientes(txtIdCliente.Text.Trim(), "txtIdCliente_Validating");
                if (!leer.Leer())
                {
                    txtIdCliente.Text = "";
                    e.Cancel = true; 
                }
                else 
                {
                    CargarDatosCliente();
                    LlenarGrid();
                }
            }
        }

        private void CargarDatosCliente()
        {
            //Se hace de esta manera para la ayuda.
            txtIdCliente.Text = leer.Campo("IdCliente");
            lblCliente.Text = leer.Campo("Farmacia");
            txtIdCliente.Enabled = false;
        }
        #endregion Buscar Cliente 

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(true);
            Grid.Limpiar(true);
            txtIdCliente.Focus();
            IniciarToolbar(true, false, false, false);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sMensaje = String.Format("La información del Cliente '{0}' se guardó satisfactoriamente", lblCliente.Text);

            EliminarRenglonesVacios();
            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();
                    if (GuardaTerminales())
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la Información.");
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }

            }

        }

        private bool GuardaTerminales()
        {
            bool bRegresa = true;
            string sSql = "", sTerminal = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            int iAsignar = 0, iHabilitar = 0, iVentanilla = 0;

            for (int i = 1; i <= Grid.Rows; i++)
            {
                sTerminal = Grid.GetValue(i, (int)Cols.IdTerminal);
                iVentanilla = Grid.GetValueInt(i, (int)Cols.Ventanilla);
                iAsignar = 0;
                iHabilitar = 0;

                if (Grid.GetValueBool(i, (int)Cols.Asignar))
                {
                    iAsignar = 1;
                }

                if (Grid.GetValueBool(i, (int)Cols.Habilitar))
                {
                    iHabilitar = 1;
                }

                sSql = String.Format("Exec spp_Mtto_IGPI_CFGC_Clientes_Terminales " + 
                    "  @IdCliente = '{0}', @IdTerminal = '{1}', @Asignada = '{2}', @Activa = '{3}', @PuertoDispensacion = '{4}', @iOpcion = '{5}' ",
                    txtIdCliente.Text.Trim(), sTerminal, iAsignar, iHabilitar, iVentanilla, iOpcion);

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
            }

            return bRegresa;
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void IniciarToolbar(bool bNuevo, bool bGuardar, bool bCancelar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
            btnImprimir.Enabled = bImprimir;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtIdCliente.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el IdCliente por favor");
                txtIdCliente.Focus();
            }

            if (bRegresa )
            {
                bRegresa = validarCapturaTerminales();
            }

            return bRegresa;
        }
        #endregion Funciones 

        #region Grid

        #region Buscar Terminal 
        private void grdTerminales_EditModeOff(object sender, EventArgs e)
        {
            string sTerminal = "";

            switch (Grid.ActiveCol)
            {
                case 1:
                    {
                        sTerminal = Grid.GetValue(Grid.ActiveRow, (int)Cols.IdTerminal);

                        if (sTerminal != "")
                        {

                            leer.DataSetClase = Consultas.Terminales(sTerminal, "grdTerminales_EditModeOff");
                            if (leer.Leer())
                            {
                                CargaDatosTerminal();
                            }
                            else
                            {
                                Grid.LimpiarRenglon(Grid.ActiveRow);
                                Grid.SetActiveCell(Grid.ActiveRow, (int)Cols.IdTerminal);
                            }                            
                        }
                    }

                    break;
            }
        }

        private void CargaDatosTerminal()
        {
            int iRowActivo = Grid.ActiveRow;

            if (!Grid.BuscaRepetido(leer.Campo("IdTerminal"), iRowActivo, 1))
            {
                Grid.SetValue(iRowActivo, (int)Cols.IdTerminal, leer.Campo("IdTerminal"));
                Grid.SetValue(iRowActivo, (int)Cols.Nombre, leer.Campo("Nombre"));
                Grid.SetValue(iRowActivo, (int)Cols.Asignar, 0);
                Grid.SetValue(iRowActivo, (int)Cols.Habilitar, 0);

                //Grid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.IdTerminal);
                Grid.SetActiveCell(iRowActivo, (int)Cols.Asignar);
                IniciarToolbar(true, true, false, false);
            }
            else
            {
                General.msjUser("Esta Terminal ya se encuentra capturada en otro renglon.");
                Grid.SetValue(Grid.ActiveRow, (int)Cols.IdTerminal, "");
                limpiarColumnas();
                Grid.SetActiveCell(Grid.ActiveRow, 1);
                Grid.EnviarARepetido();
            }
        }

        private void grdTerminales_KeyDown(object sender, KeyEventArgs e)
        {
            if (Grid.ActiveCol == (int)Cols.IdTerminal)
            {
                if (e.KeyCode == Keys.F1)
                {
                    leer.DataSetClase = Ayuda.Terminales("grdTerminales_KeyDown");
                    if (leer.Leer())
                    {
                        CargaDatosTerminal();
                    }
                }
            }
        }

        #endregion Buscar Terminal

        private void LlenarGrid()
        {
            string sSql = String.Format("Select IdTerminal, Nombre, Asignada, Activa, PuertoDispensacion " + 
                " From vw_IGPI_CFGC_Clientes_Terminales (NoLock) " +
                " Where IdCliente = '{0}' ", txtIdCliente.Text.Trim());

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener las Terminales del Cliente.");
            }
            else
            {
                if (leer.Leer())
                {
                    Grid.LlenarGrid(leer.DataSetClase, false, false);
                    IniciarToolbar(true, true, false, false);

                    //Se bloquean las terminales que ya estan asignadas para que no sean modificadas.
                    for (int i = 1; i <= Grid.Rows; i++)
                    {
                        Grid.BloqueaCelda(true, i, (int)Cols.IdTerminal);
                    }
                }
            }            
        }

        private void grdTerminales_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {            
            if ((Grid.ActiveRow == Grid.Rows) && e.AdvanceNext)
            {
                if (Grid.GetValue(Grid.ActiveRow, (int)Cols.IdTerminal) != "" && Grid.GetValue(Grid.ActiveRow, (int)Cols.Nombre) != "")
                {
                    Grid.Rows = Grid.Rows + 1;
                    Grid.ActiveRow = Grid.Rows;
                    Grid.SetActiveCell(Grid.Rows, (int)Cols.IdTerminal);
                }
            }
        }

        private void grdTerminales_EditModeOn(object sender, EventArgs e)
        {
            switch (Grid.ActiveCol)
            {
                case 1: // Si se cambia el Codigo, se limpian las columnas
                    {
                        limpiarColumnas();
                    }
                    break;
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
                if (Grid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                    Grid.DeleteRow(i);
            }

            if (Grid.Rows == 0) // Si No existen renglones, se inserta 1.
            {
                IniciarToolbar(true, false, false, false);
                Grid.AddRow();
            }
        }

        private bool validarCapturaTerminales()
        {
            bool bRegresa = true;

            if (Grid.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                if (Grid.GetValue(1, (int)Cols.Nombre) == "")
                {
                    bRegresa = false;
                }
            }

            if (!bRegresa)
                General.msjUser("Debe capturar al menos una Terminal.");

            return bRegresa;

        }

        #endregion Grid

        private void tmIGPI_Tick(object sender, EventArgs e)
        {
            tmIGPI.Stop();
            tmIGPI.Enabled = false;
            if (!IGPI.GPI_Instalado)
            {
                General.msjAviso("La Unidad conectada no puede ser configurada para GPI.");
                this.Close();
            }
        }        
    }
}
