using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;

using DllFarmaciaSoft;

namespace Facturacion.GenerarRemisiones
{
    public partial class FrmRemision_Por_Venta : FrmBaseExt
    {
        DataSet dtsFarmacias;

        clsConsultas Consultas;
        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;
        string sUrl = "";

        FrmDescargarVenta info;


        public FrmRemision_Por_Venta()
        {
            InitializeComponent();

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            
        }

        private void FrmRemision_Por_Venta_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }


        private void ObtenerFarmacias()
        {
            string sFiltro = "EsAlmacen = '1' ";

            dtsFarmacias = new DataSet();
            dtsFarmacias = Consultas.Farmacias_Urls(sIdEstado, "ObtenerFarmacias");
            cboAlmacen.Clear();
            cboAlmacen.Add("0", "<< Seleccione >>");
            cboAlmacen.Add(dtsFarmacias.Tables[0].Select(sFiltro, "NombreFarmacia"), true, "IdFarmacia", "NombreFarmacia");
            cboAlmacen.SelectedIndex = 0;
        }

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolio.Text != "" && cboAlmacen.SelectedIndex != 0)
            {
                txtFolio.Text = Fg.PonCeros(txtFolio.Text, 8);
                info = new FrmDescargarVenta(sUrl, sIdEmpresa, sIdEstado, cboAlmacen.Data, txtFolio.Text);
                if (info.Descargar())
                {

                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ObtenerFarmacias();
            txtFolio.Text = "";
        }

        private void cboAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cboAlmacen.Data
            if (cboAlmacen.SelectedIndex != 0)
            {
                sUrl = ((DataRow)cboAlmacen.ItemActual.Item)["UrlFarmacia"].ToString();
            }
        }
    }
}
