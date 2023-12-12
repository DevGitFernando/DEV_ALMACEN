using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;

namespace OficinaCentral.StatusProveedor
{
    public partial class FrmStatusProveedor : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsConsultas Consultas;
        clsAyudas Ayuda;

        public FrmStatusProveedor()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version, false);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

        }

        private void FrmStatusProveedor_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Buscar Proveedor
        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtIdProveedor.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Proveedores(txtIdProveedor.Text.Trim(), "txtIdProveedor_Validating");
                if (myLeer.Leer())
                {
                    CargarDatosProveedor();
                    BuscarStatus();
                }
            }
        }

        private void CargarDatosProveedor()
        {
            //Se hace de esta manera por la ayuda.
            txtIdProveedor.Text = myLeer.Campo("IdProveedor");
            lblProveedor.Text = myLeer.Campo("Nombre");
            txtIdProveedor.Enabled = false;
        }

        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Proveedores("txtIdProveedor_KeyDown");

                if (myLeer.Leer())
                {
                    CargarDatosProveedor();
                }
            }
        }
        #endregion Buscar Proveedor

        #region Buscar Status 
        private void BuscarStatus()
        {
            string sSql = String.Format("Select * From COM_OCEN_Proveedores_Status(NoLock) Where IdProveedor = '{0}' ", txtIdProveedor.Text.Trim());

            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    CargarDatos();
                }
            }
        }

        private void CargarDatos()
        {
            txtObservaciones.Text = myLeer.Campo("ObservacionesOCEN");

            if (myLeer.Campo("StatusOCEN") == "C")
            {
                //Si esta cancelado, se permite activar.
                lblStatus.Text = "CANCELADO";
                InicializaToolBar(true, true, false, false);
            }
            else
            {
                //Si esta activo, se permite cancelar.
                lblStatus.Text = "ACTIVO";
                InicializaToolBar(true, false, true, false);
            }
        }
        #endregion Buscar Status

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            //lblStatus.Text = "ACTIVO";
            InicializaToolBar(true, false, false, false);
            dtpFechaRegistro.Enabled = false;
            txtIdProveedor.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int iOpcion = 1;
            string sMensaje = "", sSql = "";

            if( ValidaDatos())
            {
                sSql = String.Format("Exec spp_Mtto_COM_OCEN_Proveedores_Status '{0}', '{1}', '{2}', '', '{3}' ",
                    txtIdProveedor.Text.Trim(), DtGeneral.IdPersonal, txtObservaciones.Text.Trim(), iOpcion);

                if (myLeer.Exec(sSql))
                {
                    if (myLeer.Leer())
                    {
                        sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                }
                else
                {
                    // Error.GrabarError(myLeerProducto, "btnGuardar_Click");
                    General.msjUser("Ocurrió un error al guardar la información");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            int iOpcion = 2;
            string sMensaje = "", sSql = "";

            if (ValidaDatos())
            {
                sSql = String.Format("Exec spp_Mtto_COM_OCEN_Proveedores_Status '{0}', '{1}', '{2}', '', '{3}' ",
                    txtIdProveedor.Text.Trim(), DtGeneral.IdPersonal, txtObservaciones.Text.Trim(), iOpcion);

                if (myLeer.Exec(sSql))
                {
                    if (myLeer.Leer())
                    {
                        sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                }
                else
                {
                    // Error.GrabarError(myLeerProducto, "btnGuardar_Click");
                    General.msjUser("Ocurrió un error al guardar la información");
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        #endregion Botones

        #region Funciones
        private void InicializaToolBar(bool bNuevo, bool bGuardar, bool bCancelar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
            btnImprimir.Enabled = bImprimir;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese las Observaciones por favor");
            }

            return bRegresa;
        }

        #endregion Funciones       

    }
}
