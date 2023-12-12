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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Conexiones;

namespace Compras_Cuentas_x_Pagar.Registros
{
    public partial class FrmHistoriales : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsListView lst;

        public FrmHistoriales()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            lst = new clsListView(lstDetalle);
        }

        private void FrmHistoriales_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();
        }

        public void CargarHistorialConciliacion(string IdEmpresa, string IdEstado, string OrdeDeCompra, string IdProveedor, string Factura)
        {
            this.Text = "Historial Conciliación";
            string sSql = string.Format("Select OrdenCompra, H.IdProveedor, P.Nombre As Proveedor, Factura, FechaRegistro As 'Fecha Registro', " +
		            "   FechaColocacion As 'Fecha Colocación', FechaDocto As 'Fecha Factura', FechaVenceDocto As 'Fecha Vence Factura', " +
		            "   SubTotal, Iva, Total, FechaUpdate As 'Fecha Actualización' " +
                    "From CPXP_Conciliacion_Historico H " +
                    "Inner Join Catproveedores P (NoLock) On (H.IdProveedor = P.IdProveedor) " +
                    "Where H.IdEmpresa = '{0}' And H.IdEstado = '{1}' And H.OrdenCompra = '{2}' And H.IdProveedor = '{3}' And H.Factura = '{4}'",
                    IdEmpresa, IdEstado, OrdeDeCompra, IdProveedor, Factura);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarHistorialConciliacion()");
                General.msjError("Ocurrio un error al cargar el historial");
            }
            else
            {
                lst.CargarDatos(leer.DataSetClase, true, true);
            }
        }

        public void CargarHistorialPagos(string IdEmpresa, string IdEstado, string OrdeDeCompra, string IdProveedor, string Factura)
        {
            this.Text = "Historial Pagos";
            string sSql = string.Format("Select " +
                "   H.Idproveedor, P.Nombre As Proveedor, H.IdEstado, E.Nombre As Estado, H.Folio, H.FolioOrdeneCompra As 'Folio Orden de Compra', " +
		        "H.TipoDeCompra, H.Factura, Pago, H.FechaRegistro " +
	            "From CPXP_PagosDet_Historico H (NoLock) " +
	            "Inner Join CatEmpresas M (NoLock) On (H.IdEmpresa = M.IdEmpresa) " +
	            "Inner Join CatEstados E (NoLock) On (H.IdEstado = E.IdEstado) " +
	            "Inner Join CatProveedores P (NoLock) On (H.IdProveedor = P.IdProveedor) " +
                "Where H.IdEmpresa = '{0}' And H.IdEstado = '{1}' And H.FolioOrdeneCompra = '{2}' And H.IdProveedor = '{3}' And H.Factura = '{4}'" +
	            "Order BY FechaRegistro", IdEmpresa, IdEstado, OrdeDeCompra, IdProveedor, Factura);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarHistorialPagos()");
                General.msjError("Ocurrio un error al cargar el historial");
            }
            else
            {
                lst.CargarDatos(leer.DataSetClase, true, true);
            }
        }
    }
}
