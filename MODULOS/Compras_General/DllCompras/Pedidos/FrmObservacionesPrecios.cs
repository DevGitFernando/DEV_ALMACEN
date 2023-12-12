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
    public partial class FrmObservacionesPrecios : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsDatosCliente DatosCliente;
        

        string sProveedor = "", sNomProveedor = "", sCodigoEAN = "", sDescripcionEAN = "", IdClaveSSA = "";
        double dPrecio = 0, dPrecioMin = 0, dPrecioMax = 0;
        string sFormato = "#,###,###,##0.###0";

        bool bGuardo = false;
        public bool bEjecuto = false;

        public FrmObservacionesPrecios()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, "");
            leer = new clsLeer(ref cnn);
        }

        private void FrmObservacionesPrecios_Load(object sender, EventArgs e)
        {
            Inicializa();
        }

        #region Funciones
        public void MostrarPantalla(string Proveedor, string sNomProv, string CodigoEAN, string DescripcionEAN, 
                                    double Precio, double PrecioMin, double PrecioMax, int Cantidad, string sIdClaveSSA)
        {
            Fg.IniciaControles();

            sProveedor = Proveedor;
            sNomProveedor = sNomProv;
            sCodigoEAN = CodigoEAN;
            sDescripcionEAN = DescripcionEAN;
            dPrecio = Precio;
            dPrecioMin = PrecioMin;
            dPrecioMax = PrecioMax;
            IdClaveSSA = sIdClaveSSA;

            this.ShowDialog();
        }

        private void Inicializa()
        {
            lblProv.Text = sProveedor;
            lblNomProv.Text = sNomProveedor;
            lblCodigoEAN.Text = sCodigoEAN;
            lblDescripcionEAN.Text = sDescripcionEAN;
            lblPrecio.Text = dPrecio.ToString(sFormato);
            lblPrecioMin.Text = dPrecioMin.ToString(sFormato);

            txtObservaciones.Focus();
        }

        private bool GuardaObservaciones()
        {
            string sSql = "";
            bool bRegresa = true;
            
            sSql = String.Format("Exec spp_Mtto_COM_OCEN_ObservacionesPrecios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                            IdClaveSSA, sCodigoEAN, sProveedor, dPrecioMin.ToString(sFormato), dPrecioMax.ToString(sFormato), 
                            txtObservaciones.Text, GnCompras.GUID);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.LogError(leer.MensajeError);
                //General.msjError("Ocurrió un error al Grabar las Observaciones.");                
            }            

            return bRegresa;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado las Observaciones de Precios, verifique.");
                txtObservaciones.Focus();
            }

            return bRegresa;
        }
        #endregion Funciones

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {                
                if (GuardaObservaciones())
                {                        
                    bGuardo = true;
                    this.Hide();
                }
                else
                {
                    bGuardo = false;
                    General.msjError("Ocurrió un error al guardar la información.");
                }                
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {            
            bEjecuto = bGuardo;
            this.Hide();            
        }
    }
}
