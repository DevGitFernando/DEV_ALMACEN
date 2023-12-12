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
//using DllFarmaciaSoft.wsFarmacia;

namespace DllFarmaciaAuditor.OrdenesDeCompra
{
    public partial class FrmModOrdenesDeCompra : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        clsConsultas Consultas;
        clsDatosCliente DatosCliente;

        //Para Auditoria
        clsAuditoria auditoria;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sOrigen = GnFarmacia.UnidadComprasCentrales;
        string sPersonal = DtGeneral.IdPersonal;

        #region Permisos Especiales
        string sPermisoOrdenesDeCompra = "ADT_ORDENES_DE_COMPRAS";
        bool bPermisoOrdenesDeCompra = false;
        #endregion Permisos Especiales

        public FrmModOrdenesDeCompra()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            
            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
        }

        private void FrmModOrdenesDeCompra_Load(object sender, EventArgs e)
        {
            tmSesion.Enabled = true;
            tmSesion.Start();
        }

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            bPermisoOrdenesDeCompra = DtGeneral.PermisosEspeciales.TienePermiso(sPermisoOrdenesDeCompra);
        }
        #endregion Permisos de Usuario

        #region Buscar Folio de Orden de Compra 
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtFolio.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.OrdenesCompras_Enc(sEmpresa, sEstado, sOrigen, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
                if (myLeer.Leer())
                {
                    CargaEncabezadoFolio();
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }

        }

        private void CargaEncabezadoFolio()
        {
            // Inicializar el Control 
            DateTimePicker dtpPaso = new DateTimePicker();
            dtpFechaDocto.MinDate = dtpPaso.MinDate;
            dtpFechaDocto.MaxDate = dtpPaso.MaxDate;

            //Se hace de esta manera para la ayuda.
            txtFolio.Text = myLeer.Campo("Folio"); // FolioCompra
            txtOrden.Text = myLeer.Campo("FolioOrdenCompraReferencia");
            //sEstadoGenera_OC = myLeer.Campo("EstadoGenera");
            //sFarmaciaGenera_OC = myLeer.Campo("FarmaciaGenera");

            txtIdProveedor.Text = myLeer.Campo("IdProveedor");
            lblProveedor.Text = myLeer.Campo("Proveedor");
            txtReferenciaDocto.Text = myLeer.Campo("ReferenciaDocto");

            chkEsFacturaOriginal.Checked = myLeer.CampoBool("EsFacturaOriginal");

            txtSubTotal.Text = myLeer.CampoDouble("SubTotal").ToString();
            txtIva.Text = myLeer.CampoDouble("Iva").ToString();
            txtTotal.Text = myLeer.CampoDouble("Total").ToString();
            txtTotalFactura.Text = myLeer.CampoDouble("ImporteFactura").ToString();

            txtObservaciones.Text = myLeer.Campo("Observaciones");
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");
            dtpFechaPromesaEntrega.Value = myLeer.CampoFecha("FechaPromesaEntrega");
            dtpFechaDocto.Value = myLeer.CampoFecha("FechaDocto");
            dtpFechaVenceDocto.Value = myLeer.CampoFecha("FechaVenceDocto");

            //Se cargan los campos nuevos
            txtReferenciaDoctoNueva.Text = myLeer.Campo("ReferenciaDocto");
            dtpFechaDoctoNueva.Value = myLeer.CampoFecha("FechaDocto");
            dtpFechaVenceDoctoNueva.Value = myLeer.CampoFecha("FechaVenceDocto");
            txtTotalFacturaNueva.Text = myLeer.CampoDouble("ImporteFactura").ToString();

            //Se bloquea el encabezado del Folio 
            Fg.BloqueaControles(this, false, FrameEncabezado);
            dtpFechaDocto.Enabled = false;
            lblRecibida.Text = "RECIBIDA";
            lblRecibida.Visible = true;

            IniciarToolBar(true, false);
            if (myLeer.Campo("Status") == "C")
            {
                IniciarToolBar(false, false);
                lblCancelado.Visible = true;
            }

        }
        #endregion Buscar Folio de Orden de Compra

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            IniciarToolBar(false, false);
            lblRecibida.Text = "RECIBIDA";
            lblRecibida.Visible = false;

            Fg.IniciaControles(this, true);
            Fg.BloqueaControles(this, false, FrameEncabezado);

            dtpFechaVenceDocto.Value.AddMonths(1);
            txtSubTotal.Text = "0.0000";
            txtIva.Text = "0.0000";
            txtTotal.Text = "0.0000";

            txtFolio.Enabled = true;
            txtFolio.Focus();
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sCadena = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_ADT_OrdenesDeComprasEnc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                            sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(),
                            txtReferenciaDoctoNueva.Text.Trim(), dtpFechaDoctoNueva.Text, dtpFechaVenceDoctoNueva.Text, txtTotalFacturaNueva.Text.Trim().Replace(",", ""),
                            txtReferenciaDocto.Text.Trim(), dtpFechaDocto.Text, dtpFechaVenceDocto.Text, txtTotalFactura.Text.Trim().Replace(",", ""), 
                            sPersonal);

                    sCadena = sSql.Replace("'", "\"");
                    auditoria.GuardarAud_MovtosUni("*", sCadena);

                    if (myLeer.Exec(sSql))
                    {
                        if (myLeer.Leer())
                        {
                            sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                        }

                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                        //btnNuevo_Click(null, null);

                    }

                    ConexionLocal.Cerrar();
                }
                else
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }

            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        #endregion Botones

        #region Funciones 
        private void IniciarToolBar(bool Guardar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnImprimir.Enabled = Imprimir;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            // string sIdProducto = "";

            if (txtReferenciaDoctoNueva.Text == "")
            {
                bRegresa = false;
                General.msjUser("Referencia de Documento inválida, verifique.");
                txtReferenciaDoctoNueva.Focus();
            }

            if (bRegresa && dtpFechaDoctoNueva.Value > General.FechaSistema)
            {
                bRegresa = false;
                General.msjUser("La Fecha del Documento NO puede ser mayor a la fecha actual, verifique.");
                dtpFechaDoctoNueva.Focus();
            }

            if (bRegresa && dtpFechaDoctoNueva.Value > dtpFechaVenceDoctoNueva.Value)
            {
                bRegresa = false;
                General.msjUser("La Fecha del Documento NO puede ser mayor a la Fecha de Vencimiento del Documento, verifique.");
                dtpFechaDoctoNueva.Focus();
            }

            return bRegresa;
        }
        #endregion Funciones 

        private void tmSesion_Tick(object sender, EventArgs e)
        {
            tmSesion.Stop();
            tmSesion.Enabled = false;

            GnFarmacia.ValidarSesionUsuario = true;
            if (!DtGeneral.EsAlmacen)
            {
                General.msjUser("Esta opción se utiliza unicamente en el modulo de Almacen");
                this.Close();
            }
            else
            {
                btnNuevo_Click(null, null);
            }            
        }

        

    }//LLAVES DE LA CLASE
}
