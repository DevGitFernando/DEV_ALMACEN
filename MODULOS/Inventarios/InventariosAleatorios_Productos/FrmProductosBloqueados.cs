using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;

namespace Inventarios.InventariosAleatorios_Productos
{
    public partial class FrmProductosBloqueados : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);       
        clsLeer leer;
        clsListView lst;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmProductosBloqueados()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            lst = new clsListView(lstClaves);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;
        }

        private void FrmProductosBloqueados_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarProductos();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            IniciaToolBar(true, true);
            lst.LimpiarItems();
        }

        private void IniciaToolBar(bool Nuevo, bool Ejecuta)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Nuevo;
        }

        private void CargarProductos()
        {
            string sSql = "";       
            
            sSql = string.Format(" Select Distinct I.Folio, 'Código EAN' = I.CodigoEAN, " + 
                                " 'Descripción comercial' = ( Select top 1 DescripcionSal From vw_Productos_CodigoEAN S (Nolock) Where S.CodigoEAN = I.CodigoEAN )  " +
                                " From INV_Aleatorios_Productos_Det I (Nolock) " + 
                                " Inner Join vw_ProductosExistenEnEstadoFarmacia P (Nolock) On ( P.CodigoEAN = I.CodigoEAN ) " + 
                                " Where I.IdEstado = '{0}' and I.IdFarmacia = '{1}' and P.StatusDeProducto = 'S' " + 
                                " Order By 1, 3 ", sEstado, sFarmacia); 

            lst.LimpiarItems();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarProductos()");
                General.msjError("Ocurrió un error al obtener los productos.");
            }
            else
            {
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                }
                else
                {
                    General.msjAviso("No se encontraron productos bloqueados.");
                }
            }

            lst.AnchoColumna(1, 100);
            lst.AnchoColumna(2, 150);
            lst.AnchoColumna(3, 500);
        }
        #endregion Funciones
    }
}
