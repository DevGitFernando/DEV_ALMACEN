using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using DllFarmaciaSoft;

namespace Dll_SII_IMediaccess.Marbetes
{
    public partial class FrmMarbetes : FrmBaseExt
    {
        clsConexionSQL cnn;
        clsLeer leer;

        clsLeer LeerProductos;

        string sIdProducto = "";

        public FrmMarbetes(DataSet dtsProductos)
        {
            InitializeComponent();

            LeerProductos = new clsLeer();
            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);

            LeerProductos.DataSetClase = dtsProductos;
        }

        private void FrmMarbetes_Load(object sender, EventArgs e)
        {
            SiguienteProducto();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("Update INT__MA_Productos_Marbetes Set 	MarbeteActualizado = 1, FechaUpdate = getdate() " +
                    " Where IdEstado = '{0}' And IdFarmacia = '{1}' And IdProducto = '{2}'",
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sIdProducto);

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrio un error al actualizar.");
                Error.GrabarError(leer, "btnGuardar_Click()");
            }
            else
            {
                SiguienteProducto();
            }
        }


        private void SiguienteProducto()
        {
            if (LeerProductos.Leer())
            {
                sIdProducto = LeerProductos.Campo("IdProducto");
                lblProducto.Text = LeerProductos.Campo("CodigoEAN") + " - " + LeerProductos.Campo("Descripcion");
                lblClaveSSA.Text = LeerProductos.Campo("ClaveSSA") + " - " + LeerProductos.Campo("DescripcionSal");
                lblPrecio.Text = LeerProductos.Campo("PrecioVenta");
            }
            else
            {
                this.Hide();
            }
        }
    }
}
