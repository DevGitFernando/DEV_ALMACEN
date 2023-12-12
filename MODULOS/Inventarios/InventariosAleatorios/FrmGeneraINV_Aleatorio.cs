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


namespace Inventarios.InventariosAleatorios
{
    public partial class FrmGeneraINV_Aleatorio : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);       
        clsLeer leer;       

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        string sFolio = "";
        string sMensaje = "";
        int iNumClavesUnidad = 0;
        string sTexto = "Especificar el número de claves que se incluiran en el Inventario Aleatorio.";
        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, Descripcion = 2
        }

        public FrmGeneraINV_Aleatorio()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);         

        }

        private void FrmGeneraINV_Aleatorio_Load(object sender, EventArgs e)
        {
            ObtenerNumClavesUnidad();
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (!cnn.Abrir())
            {
                Error.LogError(cnn.MensajeError);
                General.msjErrorAlAbrirConexion();
            }
            else 
            {
                cnn.IniciarTransaccion();

                bContinua = GuardarInformacion();

                if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    cnn.CompletarTransaccion();
                    General.msjAviso(sMensaje);  
                    btnNuevo_Click(null, null);
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnGuardar_Click");
                    General.msjError("Ocurrió un error al guardar la Información.");
                }

                cnn.Cerrar();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);            
            IniciaToolBar(true, false, false);
            nudNumClaves.Maximum = iNumClavesUnidad;
            lblMensaje.Text = sTexto;
            nudNumClaves.Focus();
        }

        private void IniciaToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private void ObtenerNumClavesUnidad()
        {
            string sSql = "";

            sSql = string.Format(" Select Count(Distinct ClaveSSA) As Claves From vw_ExistenciaPorSales (Nolock) " +
	                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' ", sEmpresa, sEstado, sFarmacia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerNumClavesUnidad()");
                General.msjError("Ocurrió un error al obtener el numero de Claves");
            }
            else
            {
                if (leer.Leer())
                {
                    iNumClavesUnidad = leer.CampoInt("Claves");
                }
            }
        }
        #endregion Funciones

        #region Genera_INV_Aleatorio
        private bool GuardarInformacion()
        {
            bool bRegresa = true;
            string sSql = "";
            int iTipoInv = 0, iClaves = 0;

            iTipoInv = 4;

            iClaves =  Convert.ToInt32(nudNumClaves.Value);

            if (nudNumClaves.Value > iNumClavesUnidad)
            {
                iClaves = iNumClavesUnidad;
            }

            sSql = String.Format(" Exec spp_INV_Aleatorios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                            sEmpresa, sEstado, sFarmacia, iTipoInv, iClaves, DtGeneral.IdPersonal);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    sFolio = leer.Campo("Folio");
                    sMensaje = leer.Campo("Mensaje");
                }
            }

            return bRegresa;
        }
        #endregion Genera_INV_Aleatorio

        private void nudNumClaves_ValueChanged(object sender, EventArgs e)
        {
            //if (nudNumClaves.Value > iNumClavesUnidad)
            //{
            //    nudNumClaves.Value = iNumClavesUnidad;
            //}
        }

        private void grVta_Enter(object sender, EventArgs e)
        {

        }
    }
}
