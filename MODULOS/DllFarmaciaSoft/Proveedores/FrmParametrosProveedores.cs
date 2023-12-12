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

using DllFarmaciaSoft;

namespace DllFarmaciaSoft.Proveedores
{
    internal partial class FrmParametrosProveedores : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        string sIdProveedor = ""; 

        public FrmParametrosProveedores(string IdProveedor)
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Grid = new clsGrid(ref grdParametros, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);
            sIdProveedor = IdProveedor; 
        }

        private void FrmParametrosProveedores_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            // Cargar los Parametros del sistema 
            clsParametrosProveedores Parametros = new clsParametrosProveedores(General.DatosConexion, DtGeneral.DatosApp, sIdProveedor);
            Parametros.CargarParametros();

            string sSql = "Select IdProveedor, NombreParametro, Valor, Descripcion " +
                " From Net_Prov_Parametros (NoLock) " +
                " Order by IdProveedor, NombreParametro ";
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargandoDatos");
                General.msjError("Ocurrió un error al obtener la lista de parametros.");
            }
            else
            {
                Grid.Limpiar(false);
                Grid.LlenarGrid(leer.DataSetClase);
                CargaDescripcion(1);
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool bExito = true;
            string sSql = "";
            string sArbol = "", sParametro = "", sValor = "";

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                for (int i = 1; i <= Grid.Rows; i++)
                {
                    sArbol = Grid.GetValue(i, 1);
                    sParametro = Grid.GetValue(i, 2);
                    sValor = Grid.GetValue(i, 3);

                    sSql = string.Format(" Exec spp_Mtto_Net_CFGS_Parametros '{0}', '{1}', '{2}', '', 1 ", sArbol, sParametro, sValor);
                    if (!leer.Exec(sSql))
                    {
                        bExito = false;
                        break;
                    }
                }


                if (bExito)
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamente.");
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnAceptar_Click");
                    General.msjError("Ocurrió un error al grabar la información.");
                }

                cnn.Cerrar();
            }
            else
            {
                bExito = false;
                General.msjAviso("No se pudo establecer conexión con el servidor. Intente de nuevo");
            }

            if (bExito)
            {
                this.Hide(); 
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide(); 
        }

        private void grdParametros_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            CargaDescripcion(e.NewRow + 1);
        }

        private void CargaDescripcion(int Renglon)
        {
            lblDescripcion.Text = Grid.GetValue(Renglon, 4);
        }
    }
}
