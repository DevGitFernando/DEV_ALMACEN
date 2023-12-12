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
    public partial class FrmProductosIGPI : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayudas;
        clsGrid Grid;

        private enum Cols
        {
            Ninguna = 0,
            IdProducto = 1, CodigoEAN = 2, Descripcion = 3, Status = 4, StatusDescripcion = 5, EsMultipicking = 6 
        }

        public FrmProductosIGPI()
        {
            InitializeComponent();

            cnn.SetConnectionString();
            leer = new clsLeer(ref cnn); 
            Consultas = new clsConsultas(General.DatosConexion, IGPI.Modulo, this.Name, IGPI.Version);
            Ayudas = new clsAyudas(General.DatosConexion, IGPI.Modulo, this.Name, IGPI.Version);

            grdProductos.EditModeReplace = false;
            Grid = new clsGrid(ref grdProductos, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.AjustarAnchoColumnasAutomatico = true; 
            Grid.SetOrder(true); 
        }

        private void FrmProductosIGPI_Load(object sender, EventArgs e)
        {
            CargarStatus(); 

            btnNuevo_Click(null, null); 
            tmIGPI.Enabled = true;
            tmIGPI.Start();  
        } 

        #region Buscar Codigo
        private void txtCodigoEAN_Validating(object sender, CancelEventArgs e)
        {
            leer = new clsLeer(ref cnn);

            if (txtCodigoEAN.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Productos_CodigoEAN(txtCodigoEAN.Text.Trim(), "txtIdCliente_Validating");
                if (leer.Leer())
                    CargarDatosProducto();
                else
                {                    
                    txtCodigoEAN.Text = "";
                    lblIdProducto.Text = "";
                    lblDescripcion.Text = "";
                    cboStatusIGPI.SelectedIndex = 0; 
                    txtCodigoEAN.Focus();
                }
            }
        }
        private void CargarDatosProducto()
        {
            //Se hace de esta manera para la ayuda.
            txtCodigoEAN.Text = leer.Campo("CodigoEAN");
            lblIdProducto.Text = leer.Campo("IdProducto");
            lblDescripcion.Text = leer.Campo("Descripcion");
            cboStatusIGPI.Data = leer.Campo("StatusIMach"); 

        }
        #endregion Buscar Codigo
        
        #region Asignar Producto
        ////private void btnAsignarProducto_Click(object sender, EventArgs e)
        ////{
        ////    string sSql = String.Format("Select IdProducto, CodigoEAN, Descripcion, 'C' as Status, 0 as StatusActual, 0 Asignado" + 
        ////        " From vw_Productos_CodigoEAN (NoLock) Where CodigoEAN = '{0}' ", txtCodigoEAN.Text);

        ////    if (!leer.Exec(sSql))
        ////    {
        ////        Error.GrabarError(leer, "");
        ////        General.msjError("Ocurrió un error al obtener los datos del Producto.");
        ////    }
        ////    else
        ////    {
        ////        if (leer.Leer())
        ////        {
        ////            //Grid.AgregarRenglon(leer.DataTableClase.Select(String.Format("CodigoEAN = '{0}'", txtCodigoEAN.Text) ), 6, false);
        ////            CargaDatosProducto();
        ////            IniciarToolbar(true, true, false, false);
        ////            txtCodigoEAN.Text = "";                   
        ////            txtCodigoEAN.Focus();
        ////        }
        ////        else
        ////        {
        ////            General.msjError("El Producto ingresado no existe");
        ////        }
        ////    }
        ////}

        ////private void CargaDatosProducto()
        ////{
        ////    int iRowActivo = Grid.ActiveRow;
        ////    int iRenglon = 0;

        ////    if (!Grid.BuscaRepetido(leer.Campo("IdProducto"), iRowActivo, 1))
        ////    {
        ////        Grid.AddRow();
        ////        iRenglon = Grid.Rows;

        ////        Grid.SetValue(iRenglon, (int)Cols.IdProducto, leer.Campo("IdProducto"));
        ////        Grid.SetValue(iRenglon, (int)Cols.CodigoEAN, leer.Campo("CodigoEAN"));
        ////        Grid.SetValue(iRenglon, (int)Cols.Descripcion, leer.Campo("Descripcion"));
        ////        Grid.SetValue(iRenglon, (int)Cols.Status, leer.Campo("Status"));
        ////        Grid.SetValue(iRenglon, (int)Cols.StatusActual, 0);
        ////        Grid.SetValue(iRenglon, (int)Cols.Asignar, 0);

        ////        //Grid.SetActiveCell(iRenglon, (int)Cols.Asignar);
        ////        IniciarToolbar(true, true, false, false);
        ////    }
        ////    else
        ////    {
        ////        General.msjUser("El Producto se encuentra capturada en otro renglon."); 
        ////        // Grid.SetValue(Grid.ActiveRow, (int)Cols.IdProducto, "");
        ////        //limpiarColumnas();
        ////        //Grid.SetActiveCell(Grid.ActiveRow, 1);
        ////        Grid.EnviarARepetido();
        ////    }
        ////}
        #endregion Asignar Producto

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(true);
            Grid.Limpiar(false);
            IniciarToolbar(true, false, false, false);

            LlenarGrid(); 
            txtCodigoEAN.Focus(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardaProductos("A"); 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            GuardaProductos("C");
        }

        private bool GuardaProductos(string Status)
        {
            bool bRegresa = false; 
            // string sMensaje = "";

            // EliminarRenglonesVacios();
            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    string sSql = string.Format("Exec spp_Mtto_IGPI_CFGC_Productos " +
                        " @IdProducto = '{0}', @CodigoEAN = '{1}', @Descripcion = '{2}', @Status = '{3}', @StatusIGPI = '{4}', @EsMultipicking = '{5}' ", 
                                lblIdProducto.Text.Trim(), txtCodigoEAN.Text.Trim(), lblDescripcion.Text.Trim(), Status, cboStatusIGPI.Data, Convert.ToInt32(chkEsMultipicking.Checked));

                    if (leer.Exec(sSql))
                    {
                        leer.Leer(); 

                        cnn.CompletarTransaccion();
                        General.msjUser(leer.Campo(2)); //Este mensaje lo genera el SP 
                        bRegresa = true; 
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
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor.");
                }
            }

            return bRegresa; 
        } 

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones 

        #region Grid 
        private void LlenarGrid()
        {
            string sSql = String.Format("Select IdProducto, CodigoEAN, Descripcion, " +
                " StatusIGPI, StatusIGPIAux, EsMultipicking " + 
                " From vw_IGPI_CFGC_Productos (NoLock) " +
                " Order by Descripcion "); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener los Productos del Cliente.");
            }
            else
            {
                if (leer.Leer())
                {
                    Grid.LlenarGrid(leer.DataSetClase, false, false);
                    IniciarToolbar(true, true, false, false);
                }
            }
        }

        private bool validarCapturaProductos()
        {
            bool bRegresa = true;

            if (Grid.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                if (Grid.GetValue(1, (int)Cols.Descripcion) == "")
                {
                    bRegresa = false;
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos un Producto.");
            }

            return bRegresa;

        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= Grid.Rows; i++) //Renglones.
            {
                if (Grid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                {
                    Grid.DeleteRow(i);
                }
            }
        }
        #endregion Grid
        
        #region Funciones
        private void IniciarToolbar(bool bNuevo, bool bGuardar, bool bCancelar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
            btnImprimir.Enabled = bImprimir;
        }

        private void CargarStatus()
        {
            cboStatusIGPI.Clear();
            cboStatusIGPI.Add("-1", "<< Seleccione >>");
            cboStatusIGPI.Add(Consultas.StatusDeProductos("CargarStatus()"), true); 
            cboStatusIGPI.SelectedIndex = 0; 
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            //if (txtIdCliente.Text.Trim() == "")
            //{
            //    bRegresa = false;
            //    General.msjUser("Ingrese el IdCliente por favor");
            //    txtIdCliente.Focus();
            //}

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            return bRegresa;
        }
        #endregion Funciones

        #region Eventos
        private void txtCodigoEAN_TextChanged(object sender, EventArgs e)
        {
            lblIdProducto.Text = "";
            lblDescripcion.Text = "";
        }
        #endregion Eventos

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

        private void grdProductos_DoubleClick(object sender, EventArgs e)
        {

        }

        private void grdProductos_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int iRow = Grid.ActiveRow;
            if (Grid.GetValue(iRow, (int)Cols.IdProducto) != "")
            {
                txtCodigoEAN.Text = Grid.GetValue(iRow, (int)Cols.CodigoEAN);
                lblIdProducto.Text = Grid.GetValue(iRow, (int)Cols.IdProducto);
                lblDescripcion.Text = Grid.GetValue(iRow, (int)Cols.Descripcion);
                cboStatusIGPI.Data = Grid.GetValueInt(iRow, (int)Cols.Status).ToString();
                chkEsMultipicking.Checked = Grid.GetValueBool(iRow, (int)Cols.EsMultipicking);
            }
        }

        private void FrmProductosIGPI_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    btnNuevo_Click(null, null); 
                    break; 

                case Keys.F10:
                    btnGuardar_Click(null, null); 
                    break; 

                case Keys.F12:
                    btnCancelar_Click(null, null); 
                    break; 
            }
        }    
    }
}
