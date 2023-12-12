using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using Dll_SII_INadro; 

namespace Dll_SII_INadro.Configuracion
{
    public partial class FrmClienteVentaDirecta : FrmBaseExt
    {

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;
        clsConsultas Consultas;

        string sIdPublicoGral = GnFarmacia.PublicoGral;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmClienteVentaDirecta()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion,GnDll_SII_INadro.DatosApp, this.Name);
            Ayuda = new clsAyudas(General.DatosConexion, GnDll_SII_INadro.DatosApp, this.Name);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, GnDll_SII_INadro.DatosApp, this.Name);
        }

        #region Form 
        private void FrmClienteVentaDirecta_Load(object sender, EventArgs e)
        {
            IniciarPantalla(); 
        }
        #endregion Form

        #region Botones 
        private void IniciarPantalla()
        {
            Fg.IniciaControles(true);

            txtCliente.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            IniciarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Cliente Operacion 
        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            lblCliente.Text = "";
            txtSubCliente.Text = "";
            lblSubCliente.Text = "";
        }

        private void txtCliente_Validating(object sender, CancelEventArgs e)
        {
            ////if (txtCliente.Text.Trim() == "")
            ////{
            ////    e.Cancel = true;
            ////}
            ////else
            ////{
            ////    leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCliente.Text, "txtCliente_Validating");
            ////    if (!leer.Leer())
            ////    {
            ////        General.msjUser("Clave de Cliente no encontrada, ó el Cliente no pertenece a la Farmacia.");
            ////        e.Cancel = true;
            ////    }
            ////    else
            ////    {
            ////        txtCliente.Enabled = false;
            ////        txtCliente.Text = leer.Campo("IdCliente");
            ////        lblCliente.Text = leer.Campo("NombreCliente");
            ////    }
            ////}
        }

        private void txtSubCliente_KeyDown(object sender, KeyEventArgs e)
        {
            ////if (e.KeyCode == Keys.F1)
            ////{
            ////    if (txtCliente.Text.Trim() != "")
            ////    {
            ////        leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCliente.Text, "txtSubCte_KeyDown_1");
            ////        if (leer.Leer())
            ////        {
            ////            txtSubCliente.Enabled = false;
            ////            txtSubCliente.Text = leer.Campo("IdSubCliente");
            ////            lblSubCliente.Text = leer.Campo("NombreSubCliente"); ;
            ////        }
            ////    }
            ////}
        }
        #endregion Cliente Operacion
    }
}
