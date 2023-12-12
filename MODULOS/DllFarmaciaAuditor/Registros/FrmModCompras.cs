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

namespace DllFarmaciaAuditor.Registros
{
    public partial class FrmModCompras : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        DllFarmaciaSoft.clsConsultas Consultas;
        DllFarmaciaSoft.clsAyudas Ayuda;
        clsLeer myLeer;

        string sFolioCompra = "", sMensaje = "";
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        #region Permisos Especiales
        string sPermisoCompras = "ADT_COMPRAS";
        bool bPermisoCompras = false;
        #endregion Permisos Especiales

        public FrmModCompras()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, General.DatosApp, this.Name,true);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, General.DatosApp, this.Name, true);
        }

        private void FrmModCompras_Load(object sender, EventArgs e)
        {
            SolicitarPermisosUsuario();
            btnNuevo_Click(null, null); 
        }

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            bPermisoCompras = DtGeneral.PermisosEspeciales.TienePermiso(sPermisoCompras);
        }
        #endregion Permisos de Usuario

        #region Buscar Folio
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtFolio.Text.Trim() != "")
            {
                //Se verifica si el Folio de Compra ya ha sido modificado anteriormente.
                if (!Folio_Modificado_Anteriormente())
                {
                    myLeer.DataSetClase = Consultas.FolioEnc_Compras(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
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
        }

        private void CargaEncabezadoFolio()
        {
            // Inicializar el Control 
            DateTimePicker dtpPaso = new DateTimePicker();
            dtpFechaDocto.MinDate = dtpPaso.MinDate;
            dtpFechaDocto.MaxDate = dtpPaso.MaxDate;

            //Se hace de esta manera para la ayuda.
            txtFolio.Text = myLeer.Campo("Folio"); // FolioCompra
            txtIdProveedor.Text = myLeer.Campo("IdProveedor");
            lblProveedor.Text = myLeer.Campo("Proveedor");
            txtReferenciaDocto.Text = myLeer.Campo("ReferenciaDocto");
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");
            dtpFechaDocto.Value = myLeer.CampoFecha("FechaDocto");
            dtpFechaVenceDocto.Value = myLeer.CampoFecha("FechaVenceDocto");

            txtIdProveedor_Nuevo.Text = myLeer.Campo("IdProveedor");
            lblProveedor_Nuevo.Text = myLeer.Campo("Proveedor");
            txtReferenciaDocto_Nuevo.Text = myLeer.Campo("ReferenciaDocto");
            dtpFechaDocto_Nuevo.Value = myLeer.CampoFecha("FechaDocto");

            dtpFechaDocto_Nuevo.MaxDate = dtpFechaDocto_Nuevo.Value.AddMonths(2);
            dtpFechaDocto_Nuevo.MinDate = dtpFechaDocto_Nuevo.Value.AddMonths(-6);

            //Se bloquea el encabezado del Folio 
            Fg.BloqueaControles(this, false, FrameEncabezado);
            dtpFechaDocto.Enabled = false;

            IniciarToolBar(true, true, false, false);
            if (myLeer.Campo("Status") == "C")
            {
                IniciarToolBar(true, false, false, false);
                General.msjUser("Este Folio se encuentra Cancelado, por lo tanto no puede ser modificado");

                Fg.BloqueaControles(this, false, FrameCambio);
                //lblCancelado.Visible = true;
            }

        }

        private bool Folio_Modificado_Anteriormente()
        {
            bool bRegresa = false;
            myLeer = new clsLeer(ref ConexionLocal);

            Consultas.MostrarMsjSiLeerVacio = false;
            myLeer.DataSetClase = Consultas.Adt_ComprasEnc(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
            if (myLeer.Leer())
            {
                lblCorregido.Visible = true;

                //Si el usuario es administrador, puede hacer cambios a la compra.
                if (!(DtGeneral.EsAdministrador || bPermisoCompras))
                {
                    bRegresa = true;
                    btnNuevo_Click(null, null);
                    General.msjUser("Este Folio ya ha sido corregido. Verifique");
                }
            }            

            return bRegresa;
        }
        #endregion Buscar Folio

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);

            IniciarToolBar(true, false, false, false);
            lblCorregido.Text = "CORREGIDO";
            lblCorregido.Visible = false;

            //dtpFechaDocto_Nuevo.MaxDate = dtpFechaDocto_Nuevo.Value;
            //dtpFechaDocto_Nuevo.MinDate = dtpFechaDocto_Nuevo.Value.AddMonths(-6);

            txtFolio.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (ValidaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    if (GrabarEncabezado())
                    {
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la Información.");
                        IniciarToolBar(true, true, false, false);
                    }

                    ConexionLocal.Cerrar();
                }
                else
                {
                    Error.LogError(ConexionLocal.MensajeError);
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                }

            }
            

        }

        private bool GrabarEncabezado()
        {
            bool bRegresa = false;
            string sSql = "";

            sSql = String.Format("Set DateFormat YMD Exec spp_Mtto_Adt_ComprasEnc " +
                                "'{0}', '{1}', '{2}', " +
                                "'{3}', '{4}', '{5}', " +
                                "'{6}', '{7}' ",
                            sEmpresa, sEstado, sFarmacia, txtFolio.Text,
                            DtGeneral.IdPersonal, txtIdProveedor_Nuevo.Text, txtReferenciaDocto_Nuevo.Text, 
                            dtpFechaDocto_Nuevo.Text );
            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    sFolioCompra = myLeer.Campo("Clave");
                    sMensaje = myLeer.Campo("Mensaje");
                    bRegresa = true;
                }
            }

            return bRegresa;
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Buscar Proveedor

        private void txtIdProveedor_Nuevo_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtIdProveedor_Nuevo.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Proveedores(txtIdProveedor_Nuevo.Text.Trim(), "txtIdProveedor_Nuevo_Validating");
                if (myLeer.Leer())
                    CargaDatosProveedor();
                else
                    txtIdProveedor_Nuevo.Focus();
            }

        }

        private void CargaDatosProveedor()
        {
            //Se hace de esta manera para la ayuda. 

            if (myLeer.Campo("Status").ToUpper() == "A")
            {
                txtIdProveedor_Nuevo.Text = myLeer.Campo("IdProveedor");
                lblProveedor_Nuevo.Text = myLeer.Campo("Nombre");
            }
            else
            {
                General.msjUser("El Proveedor " + myLeer.Campo("Nombre") + " actualmente se encuentra cancelado, verifique. ");
                txtIdProveedor_Nuevo.Text = "";
                lblProveedor_Nuevo.Text = "";
                txtIdProveedor_Nuevo.Focus();
            }
        }

        private void txtIdProveedor_Nuevo_TextChanged(object sender, EventArgs e)
        {
            lblProveedor_Nuevo.Text = "";
        }

        private void txtIdProveedor_Nuevo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Proveedores("txtIdProveedor_KeyDown");

                if (myLeer.Leer())
                {
                    CargaDatosProveedor();
                }
            }
        }
        
        #endregion Buscar Proveedor 

        #region Funciones 
        private void IniciarToolBar(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            string sFechaDocto = General.FechaYMD(dtpFechaDocto_Nuevo.Value, "-");
            string sFechaActual = General.FechaYMD(General.FechaSistemaObtener(), "-");

            if (lblProveedor_Nuevo.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Proveedor inválida, verifique.");
                txtIdProveedor_Nuevo.Focus();
            }

            if (bRegresa && txtReferenciaDocto_Nuevo.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Referencia inválida, verifique.");
                txtReferenciaDocto_Nuevo.Focus();
            }

            if (bRegresa && dtpFechaDocto_Nuevo.Value <= dtpFechaDocto.Value.AddMonths(-2))
            {
                bRegresa = false;
                General.msjUser("La Fecha del documento no puede ser menor a dos meses la fecha de documento actual.");
                dtpFechaDocto_Nuevo.Focus();
            }

            if (bRegresa && DateTime.Parse(sFechaDocto) >= DateTime.Parse(sFechaActual))
            {
                bRegresa = false;
                General.msjUser("La Fecha del documento no puede ser mayor o igual a la fecha actual.");
                dtpFechaDocto_Nuevo.Focus();
            }
            
            return bRegresa;
        }
        #endregion Funciones

    }
}
