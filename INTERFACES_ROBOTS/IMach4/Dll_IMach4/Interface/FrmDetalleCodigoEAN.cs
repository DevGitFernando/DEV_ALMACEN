using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using Dll_IMach4; 

namespace Dll_IMach4.Interface
{
    public partial class FrmDetalleCodigoEAN : FrmBaseExt
    {
        clsConexionSQL cnn;
        clsLeer leer;
        clsConsultas query; 

        public int CantidadSolicitada = 0;
        public bool RequisicionDeProducto = false;
        public bool EsProducto_Multipicking = false;

        double iContenido = 0;
        double iCantidad = 0;

        public FrmDetalleCodigoEAN()
        {
            InitializeComponent();

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, IMach4.DatosApp, this.Name, false); 
        }

        private void FrmDetalleCodigoEAN_Load(object sender, EventArgs e)
        {
            txtCantidad.Focus(); 
        }

        public void MostrarPantalla(string FolioSolicitud, string CodigoEAN)
        {
            RequisicionDeProducto = false;
            CantidadSolicitada = 0;

            leer.DataSetClase = query.Productos_CodigoEAN(CodigoEAN, "MostrarPantalla"); 
            if (!leer.Leer())
            {
                General.msjUser("CodigoEAN no encontrado verifique."); 
            }
            else
            {
                Fg.IniciaControles();
                txtCajas.Enabled = false;
                txtCantRequerida.Enabled = false; 
                txtCantDispensada.Enabled = false;

                EsProducto_Multipicking = leer.CampoBool("EsMultipicking");
                lblCodigo.Text = leer.Campo("IdProducto");
                lblCodigoEAN.Text = leer.Campo("CodigoEAN");
                lblArticulo.Text = leer.Campo("Descripcion");
                lblPresentacion.Text = leer.Campo("Presentacion");
                lblContenido.Text = leer.CampoInt("ContenidoPaquete").ToString();
                lblClaveSSA.Text = leer.Campo("ClaveSSA");
                lblDescripcionSSA.Text = leer.Campo("DescripcionSal");

                CargarDatosDeDispensacion(FolioSolicitud);
                txtCantidad.Focus(); 
                // this.ShowDialog();
            }

            lblEsMultipicking.Text = "Multipicking";
            lblEsMultipicking.Visible = EsProducto_Multipicking;

            this.ShowDialog();
        }

        private void CargarDatosDeDispensacion(string FolioSolicitud)
        {
            leer.DataSetClase = query.StatusSolicitud_Mach4(FolioSolicitud, lblCodigo.Text, lblCodigoEAN.Text, "CargarDatosDeDispensacion");
            leer.Leer();
            txtCantRequerida.Text = leer.CampoInt("CantidadRequerida").ToString(); ;
            txtCantDispensada.Text = leer.CampoInt("CantidadDispensada").ToString(); ; 
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            //RequisicionDeProducto = false; 
            //CantidadSolicitada = 0;
            this.Hide(); 
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                iContenido = Convert.ToDouble(lblContenido.Text);
                iCantidad = Convert.ToDouble(txtCantidad.Text);

                CantidadSolicitada = (int)Math.Ceiling(iCantidad / iContenido);
            }
            catch { CantidadSolicitada = 0; }

            // Asegurar solo solicitudes validas 
            // RequisicionDeProducto = CantidadSolicitada > 0 ? true : false;
            RequisicionDeProducto = CantidadSolicitada > 0;

            this.Hide(); 
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                iContenido = Convert.ToDouble(lblContenido.Text);
                iCantidad = Convert.ToDouble(txtCantidad.Text);

                txtCajas.Text = ((int)Math.Ceiling(iCantidad / iContenido)).ToString();
            }
            catch { txtCajas.Text = "0"; }
        }

        private void FrmDetalleCodigoEAN_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    btnAceptar_Click(null, null); 
                    break;

                case Keys.F12:
                    this.Hide(); 
                    break;

                default:
                    // base.OnKeyDown(e);
                    break;
            }
        }
    }
}
