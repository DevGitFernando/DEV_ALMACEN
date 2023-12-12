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

namespace Dll_IATP2.Configuracion
{
    public partial class FrmProductosClientes : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas_IATP2 Consultas;
        clsAyudas_IATP2 Ayudas;
        clsGrid Grid;

        private enum Cols
        {
            Ninguna = 0,
            IdProducto = 1, CodigoEAN = 2, Descripcion = 3, Status = 4, StatusActual = 5, Asignar = 6
        }

        public FrmProductosClientes()
        {
            InitializeComponent();

            cnn.SetConnectionString();
            Consultas = new clsConsultas_IATP2(General.DatosConexion, IATP2.Modulo, this.Name, IATP2.Version);
            Ayudas = new clsAyudas_IATP2(General.DatosConexion, IATP2.Modulo, this.Name, IATP2.Version);

            grdProductos.EditModeReplace = false;
            Grid = new clsGrid(ref grdProductos, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.SetOrder((int)Cols.IdProducto, 6, true); 
        }

        private void FrmProductosClientes_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            tmIMach.Enabled = true;
            tmIMach.Start();  
        }

        
        #region Buscar Cliente
        private void txtIdCliente_Validating(object sender, CancelEventArgs e)
        {
            leer = new clsLeer(ref cnn);

            if (txtIdCliente.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Clientes(txtIdCliente.Text.Trim(), "txtIdCliente_Validating");
                if (leer.Leer())
                {
                    CargarDatosCliente();
                    LlenarGrid();
                }
                else
                    btnNuevo_Click(null, null);
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
        }
        #endregion Buscar Codigo
        
        #region Asignar Producto
        private void btnAsignarProducto_Click(object sender, EventArgs e)
        {
            string sSql = String.Format("Select IdProducto, CodigoEAN, Descripcion, 'A' as Status, 0 as StatusActual, 1 Asignado" + 
                " From vw_Productos_CodigoEAN (NoLock) Where CodigoEAN = '{0}' ", txtCodigoEAN.Text); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener los datos del Producto.");
            }
            else
            {
                if (leer.Leer())
                {
                    //Grid.AgregarRenglon(leer.DataTableClase.Select(String.Format("CodigoEAN = '{0}'", txtCodigoEAN.Text) ), 6, false);
                    CargaDatosProducto();
                    IniciarToolbar(true, true, false, false);
                    txtCodigoEAN.Text = "";                   
                    txtCodigoEAN.Focus();
                }
                else
                {
                    General.msjError("El Producto ingresado no existe");
                }
            }
        }

        private void CargaDatosProducto()
        {
            int iRowActivo = Grid.ActiveRow;
            int iRenglon = 0;

            if (!Grid.BuscaRepetido(leer.Campo("IdProducto"), iRowActivo, 1))
            {
                Grid.AddRow(1);
                iRenglon = Grid.Rows;
                iRenglon = 1; 

                Grid.SetValue(iRenglon, (int)Cols.IdProducto, leer.Campo("IdProducto"));
                Grid.SetValue(iRenglon, (int)Cols.CodigoEAN, leer.Campo("CodigoEAN"));
                Grid.SetValue(iRenglon, (int)Cols.Descripcion, leer.Campo("Descripcion"));
                Grid.SetValue(iRenglon, (int)Cols.Status, leer.Campo("Status"));
                Grid.SetValue(iRenglon, (int)Cols.StatusActual, 0);
                Grid.SetValue(iRenglon, (int)Cols.Asignar, 0);

                //Grid.SetActiveCell(iRenglon, (int)Cols.Asignar);
                IniciarToolbar(true, true, false, false);
            }
            else
            {
                General.msjUser("El Producto se encuentra capturada en otro renglon."); 
                // Grid.SetValue(Grid.ActiveRow, (int)Cols.IdProducto, "");
                //limpiarColumnas();
                //Grid.SetActiveCell(Grid.ActiveRow, 1);
                Grid.EnviarARepetido();
            }
        }
        #endregion Asignar Producto

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(true);
            Grid.Limpiar(false);
            IniciarToolbar(true, false, false, false);
            txtIdCliente.Focus();
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
                    if (GuardaProductos())
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

        private bool GuardaProductos()
        {
            bool bRegresa = true;
            string sSql = "", sIdProducto = "", sCodigoEAN = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            int iStatusActual = 0, iAsignar = 0;

            for (int i = 1; i <= Grid.Rows; i++)
            {
                sIdProducto = Grid.GetValue(i, (int)Cols.IdProducto);
                sCodigoEAN = Grid.GetValue(i, (int)Cols.CodigoEAN);
                iStatusActual = 0;
                iAsignar = 0;

                if (Grid.GetValueBool(i, (int)Cols.StatusActual))
                    iStatusActual = 1;

                if (Grid.GetValueBool(i, (int)Cols.Asignar))
                    iAsignar = 1;

                // Si el Status Actual es diferente del Asignar se guarda.
                if (iStatusActual != iAsignar)
                {
                    sSql = String.Format("Exec spp_Mtto_IMach_CFGC_Clientes_Productos '{0}', '{1}', '{2}', '{3}', '{4}' ",
                                txtIdCliente.Text.Trim(), sIdProducto, sCodigoEAN, iAsignar, iOpcion);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
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

        #region Grid 
        private void LlenarGrid()
        {
            string sSql = String.Format("Select IdProducto, CodigoEAN, Descripcion, " +
                " (Case When Status = 1 Then 'A' Else 'C' End) as Status, " + 
                " StatusAsignacion as StatusActual, Status as Asignar " + 
                " From vw_IMach_CFGC_Clientes_Productos (NoLock) " + 
                " Where IdCliente = '{0}' ", txtIdCliente.Text.Trim());

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
                General.msjUser("Debe capturar al menos un Producto.");

            return bRegresa;

        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= Grid.Rows; i++) //Renglones.
            {
                if (Grid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                    Grid.DeleteRow(i);
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

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtIdCliente.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el IdCliente por favor");
                txtIdCliente.Focus();
            }

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

        private void tmIMach_Tick(object sender, EventArgs e)
        {
            tmIMach.Stop();
            tmIMach.Enabled = false;
            if (!IATP2.ATP2_Instalado)
            {
                IATP2.Mensaje__ATP2_NoInstalado(); 
                this.Close();
            }
        }    
    }
}
