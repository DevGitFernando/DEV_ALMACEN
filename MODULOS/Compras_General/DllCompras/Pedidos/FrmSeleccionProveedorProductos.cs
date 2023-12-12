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

using DllFarmaciaSoft; 

namespace DllCompras.Pedidos
{
    public partial class FrmSeleccionProveedorProductos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;

        // clsAyudas Ayuda;
        // clsConsultas Consultas;
        // clsConsultas Query;

        bool bGuardado = false;
        bool bEjecuto = true;
        string sClaveSSA = ""; 
        public int TotalUnidades = 0;

        string sFormato = "#,###,###,##0.###0";
        string sFormatoInt = "#,###,###,##0";
        string sFolioPedido = "";
        // string sUnidad = "";        

        private enum Cols
        {
            Ninguna = 0,
            IdProveedor = 1, Proveedor = 2, CodigoEAN = 3, DescEAN = 4, Presentacion = 5, ContenidoPaq = 6,
            Precio = 7, Cantidad = 8, Importe = 9, PrecioMin = 10, PrecioMax = 11, ProveedorSancionado = 12
        }

        public FrmSeleccionProveedorProductos() 
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnCompras.DatosApp, this.Name);

            grdProveedoresClaves.EditModeReplace = true; 
            Grid = new clsGrid(ref grdProveedoresClaves, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
        }

        private void FrmCom_CriteriosParaPedidos_Load(object sender, EventArgs e)
        {
            ObtieneDatos();
            CreaEliminaTemporal(1);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Grid.SetValue((int)Cols.Cantidad, 0);
            lblCantidadSolicitadaProv.Text = "0";
            lblTotalImpte.Text = "0";
            TotalUnidades = 0; 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();
                    if (GuardarCantidades())
                    {
                        cnn.CompletarTransaccion();
                        bGuardado = true;
                        CreaEliminaTemporal(2);
                        this.Hide();
                    }
                    else
                    {
                        Error.GrabarError(leer, "btnGuardar_Click");
                        cnn.DeshacerTransaccion();
                        if (!bEjecuto)
                        {
                            General.msjError("Terminó el proceso, el usuario NO guardó las Observaciones de Precios.");
                        } 
                        else
                        {
                            General.msjError("Ocurrió un error al guardar la información.");
                        }
                    }
                }
                else
                {
                    General.msjUser(General.MsjErrorAbrirConexion);
                }
            }
        }

        #region Funciones 
        private bool validaDatos()
        {
            bool bRegresa = true;

            if (Convert.ToInt32("0" + lblCantidadSolicitadaProv.Text.Replace(",", "")) > Convert.ToInt32("0" + lblCantidadRequerida.Text.Replace(",", "")))
            {
                bRegresa = false;
                General.msjUser("La cantidad asignada es mayor a la cantidad requerida, verifique."); 
            }

            return bRegresa; 
        }

        private void ObtieneDatos()
        {
            string sSql = String.Format("Exec spp_COM_SeleccionProveedorProductos '{0}', '{1}', '{2}' ",
                lblClaveSSA.Text, GnCompras.GUID, sFolioPedido);

            if (leer.Exec(sSql))
            {
                if (!leer.Leer())
                {
                    General.msjAviso("No existen Proveedores registrados para la Clave-CodigoEAN solicitados, verifique.");
                    this.Hide(); 
                }
                else 
                {
                    CargaDatos();
                    InicializarToolbar(true, true);
                }
            }
            else
            {
                General.msjUser("Ocurrió un error al obtener los datos.");
            }
        }

        private void CargaDatos()
        {
            // lblClaveSSA.Text = leer.Campo("IdClaveSSA");
            lblClaveSSA.Text = leer.Campo("ClaveSSA");
            lblDescripcionSSA.Text = leer.Campo("DescripcionClave");
            //lblCodigoEAN.Text = leer.Campo("CodigoEAN");
            //lblArticulo.Text = leer.Campo("Producto");

            Grid.LlenarGrid(leer.DataSetClase);
            lblCantidadSolicitadaProv.Text = Grid.TotalizarColumna((int)Cols.Cantidad).ToString(sFormatoInt);
            BloquearProveedoresSancionados();

            TotalUnidades = Grid.TotalizarColumna((int)Cols.Cantidad);
        }

        private bool GuardarCantidades()
        {
            bool bRegresa = true;
            string sSql = ""; 
            string sCodigoEAN = "", sIdProveedor = "";
            int iRenglones = Grid.Rows, iCantidad = 0;
            double dPrecioMin = 0, dPrecio = 0, dPrecioMax = 0;
            string sNomProveedor = "", sDescripcionEAN = "";

            for (int i = 1; i <= iRenglones; i++)
            {
                sIdProveedor = Grid.GetValue(i, (int)Cols.IdProveedor);
                sCodigoEAN = Grid.GetValue(i, (int)Cols.CodigoEAN);
                iCantidad = Grid.GetValueInt(i, (int)Cols.Cantidad);

                if (sCodigoEAN.Trim() != "")
                {                    
                    sNomProveedor = Grid.GetValue(i, (int)Cols.Proveedor);                    
                    sDescripcionEAN = Grid.GetValue(i, (int)Cols.DescEAN);
                    dPrecioMin = Grid.GetValueDou(i, (int)Cols.PrecioMin);
                    dPrecioMax = Grid.GetValueDou(i, (int)Cols.PrecioMax);
                    dPrecio = Grid.GetValueDou(i, (int)Cols.Precio);

                    if ( iCantidad > 0 && dPrecio > dPrecioMin)
                    {
                        FrmObservacionesPrecios precios = new FrmObservacionesPrecios();
                        precios.MostrarPantalla(sIdProveedor, sNomProveedor, sCodigoEAN, sDescripcionEAN, dPrecio, dPrecioMin, dPrecioMax, iCantidad, sClaveSSA);
                        precios.bEjecuto = bEjecuto;
                        
                    }

                    if (bEjecuto)
                    {
                        sSql = String.Format("Exec spp_Mtto_COM_OCEN_CriteriosParaPedidos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                                    sClaveSSA, sCodigoEAN, sIdProveedor, iCantidad, GnCompras.GUID, sFolioPedido);
                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                    else
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }       

        public void MostrarProveedoresPorProducto(string IdClaveSSA, int CantidadRequerida, string FolioPedido)
        {
            Fg.IniciaControles(); 

            bGuardado = false;//Esta variable cambia de valor en el guardado.
            Grid.Limpiar(false);
            sClaveSSA = IdClaveSSA;
            lblClaveSSA.Text = IdClaveSSA;
            sFolioPedido = FolioPedido;
            lblCantidadSolicitadaProv.Text = "0";
            lblCantidadRequerida.Text = CantidadRequerida.ToString(sFormatoInt);
            TotalUnidades = 0;
            
            this.ShowDialog();

            //Si el usuario cerro la pantalla sin guardar, entonces se envia un cero.
            if (!bGuardado)
            {
                TotalUnidades = 0;
            }
        }

        private void InicializarToolbar(bool bNuevo, bool bGuardar)
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
        }

        private void CreaEliminaTemporal(int iOpcion)
        {
            string sSql = String.Format("Exec spp_CreaEliminaTmpObservacionesPrecios '{0}' ", iOpcion);

            if (!leer.Exec(sSql))
            {
                General.msjUser("Ocurrió un error al Crear/Eliminar Temporal.");
            }
        }

        private void BloquearProveedoresSancionados()
        {
            bool bSancionado = false;
            int iRenglones = Grid.Rows;

            for (int i = 1; i <= iRenglones; i++)
            {
                bSancionado = Grid.GetValueBool(i, (int)Cols.ProveedorSancionado);
                if (bSancionado)
                {
                    Grid.BloqueaRenglon(true, i);
                    Grid.ColorRenglon(i, Color.Red);
                }
            }

        }

        #endregion Funciones

        private void grdProveedoresClaves_EditModeOff(object sender, EventArgs e)
        {
            double dImpteTotal = 0;
            dImpteTotal = Grid.TotalizarColumnaDou((int)Cols.Importe);
            lblTotalImpte.Text = dImpteTotal.ToString(sFormato);

            TotalUnidades = Grid.TotalizarColumna((int)Cols.Cantidad);
            lblCantidadSolicitadaProv.Text = TotalUnidades.ToString(sFormatoInt); 

            
        }

        private void FrmSeleccionProveedorProductos_FormClosing(object sender, FormClosingEventArgs e)
        {
            CreaEliminaTemporal(2);
        }

        

    }
}
