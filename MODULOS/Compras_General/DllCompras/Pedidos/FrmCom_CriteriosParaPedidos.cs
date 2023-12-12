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
    public partial class FrmCom_CriteriosParaPedidos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;

        // clsAyudas Ayuda;
        // clsConsultas Consultas;
        // clsConsultas Query;

        bool bGuardado = false;
        string sClaveSSA = ""; 
        public int TotalUnidades = 0;

        string sFormato = "#,###,###,##0.###0";
        string sFormatoInt = "#,###,###,##0";


        private enum Cols
        {
            Ninguna = 0,
            IdProveedor = 1, Proveedor = 2, Precio = 3, Surtimiento = 4, Tiempo = 5, Cantidad = 6
        }

        public FrmCom_CriteriosParaPedidos() 
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);

            grdProveedoresClaves.EditModeReplace = true; 
            Grid = new clsGrid(ref grdProveedoresClaves, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
        }

        private void FrmCom_CriteriosParaPedidos_Load(object sender, EventArgs e)
        {
            ObtieneDatos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Grid.SetValue((int)Cols.Cantidad, 0);
            lblCantidadSolicitadaProv.Text = "0";
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
                        this.Hide();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la información.");
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

            if (Convert.ToInt32(lblCantidadSolicitadaProv.Text) > Convert.ToInt32(lblCantidadRequerida.Text))
            {
                bRegresa = false;
                General.msjUser("La cantidad asignada es mayor a la cantidad requerida, verifique."); 
            }

            return bRegresa; 
        }

        private void ObtieneDatos()
        {
            string sSql = String.Format("Exec spp_COM_OCEN_CriteriosParaPedidos_Datos '{0}', '{1}', '{2}' ",
                lblClaveSSA.Text, lblCodigoEAN.Text, GnCompras.GUID);

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
            lblCodigoEAN.Text = leer.Campo("CodigoEAN");
            lblArticulo.Text = leer.Campo("Producto");

            Grid.LlenarGrid(leer.DataSetClase);
            lblCantidadSolicitadaProv.Text = Grid.TotalizarColumna((int)Cols.Cantidad).ToString(sFormatoInt);

            TotalUnidades = Grid.TotalizarColumna((int)Cols.Cantidad);
        }

        private bool GuardarCantidades()
        {
            bool bRegresa = true;
            string sSql = ""; 
            string sCodigoEAN = lblCodigoEAN.Text, sIdProveedor = "";
            int iRenglones = Grid.Rows, iCantidad = 0;

            for (int i = 1; i <= iRenglones; i++)
            {
                sIdProveedor = Grid.GetValue(i, (int)Cols.IdProveedor);
                iCantidad = Grid.GetValueInt(i, (int)Cols.Cantidad);

                sSql = String.Format("Exec spp_Mtto_COM_OCEN_CriteriosParaPedidos '{0}', '{1}', '{2}', '{3}', '{4}' ",
                            sClaveSSA, sCodigoEAN, sIdProveedor, iCantidad, GnCompras.GUID); 
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break; 
                }
            }

            return bRegresa;
        }

        public void MostrarProveedoresPorClaveSSA(string IdClaveSSA, string CodigoEAN, int CantidadRequerida)
        {
            Fg.IniciaControles(); 

            bGuardado = false;//Esta variable cambia de valor en el guardado.
            Grid.Limpiar(false);
            sClaveSSA = IdClaveSSA;
            lblClaveSSA.Text = IdClaveSSA;
            lblCodigoEAN.Text = CodigoEAN;
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
        #endregion Funciones

        private void grdProveedoresClaves_EditModeOff(object sender, EventArgs e)
        {
            TotalUnidades = Grid.TotalizarColumna((int)Cols.Cantidad);
            lblCantidadSolicitadaProv.Text = TotalUnidades.ToString(sFormatoInt); 
        }
    }
}
