using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;

namespace Almacen.PedidosEspeciales
{
    public partial class FrmRegistroPedidosEspeciales_ModificarReferencia : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer Leer, LeerExcel;
        clsExportarExcelPlantilla xpExcel;
        clsDatosCliente datosCliente;

        bool bReferenciaActualizada = false; 
        string sFolioDePedido = "";
        string sReferenciaDePedido = "";
        TiposDePedidosCEDIS tipoDePedido = TiposDePedidosCEDIS.Ninguno; 

        public bool ReferenciaActualizada
        {
            get { return bReferenciaActualizada; }
        }

        public string Referencia
        {
            get { return sReferenciaDePedido; }
        }

        public FrmRegistroPedidosEspeciales_ModificarReferencia(TiposDePedidosCEDIS TipoDePedido, string FolioDePedido, string ReferenciaDePedido )
        {
            InitializeComponent();

            tipoDePedido = TipoDePedido; 
            sFolioDePedido = FolioDePedido;
            sReferenciaDePedido = ReferenciaDePedido;

            Leer = new clsLeer(ref cnn);
            LeerExcel = new clsLeer(ref cnn);
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmRegistroPedidosEspeciales_ModificarReferencia");
        }

        private void FrmRegistroPedidosEspeciales_ModificarReferencia_Load(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        private void InicializarPantalla()
        {
            Fg.IniciaControles();
            txtReferenciaPedido.ReadOnly = true; 
            txtReferenciaPedido.Text = sReferenciaDePedido;

            txtReferenciaPedido_Nueva.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                GuardarInformacion();
            }
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            if (txtReferenciaPedido_Nueva.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjError("La referencia del pedido no puede ser vaciá, verifique.");
                txtReferenciaPedido_Nueva.Focus(); 
            }

            return bRegresa; 
        }

        private bool GuardarInformacion()
        {
            bool bRegresa = false;
            string sSql = string.Format(
                "Exec spp_Mtto_Pedidos_Cedis_ActualizarReferenciaDePedido \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioPedido = '{3}', @ReferenciaDeInterna = '{4}' \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioDePedido, txtReferenciaPedido_Nueva.Text.Trim());

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                cnn.IniciarTransaccion();

                bRegresa = Leer.Exec(sSql);

                if (!bRegresa)
                {
                    Error.GrabarError(Leer, "GuardarInformacion");
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al actualizar la referencia del pedido.");
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Referencia de pedido actualizada satisfactoriamente.");
                }

                cnn.Cerrar(); 
            }

            if (bRegresa)
            {
                bReferenciaActualizada = true; 
                sReferenciaDePedido = txtReferenciaPedido_Nueva.Text;

                this.Hide();
            }

            return bRegresa;
        }
    }
}
