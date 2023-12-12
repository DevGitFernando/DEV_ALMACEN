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

using DllProveedores;
using DllProveedores.Usuarios_y_Permisos;

namespace DllProveedores.StatusProveedor
{
    public partial class FrmStatusProveedor : FrmBaseExt
    {
        clsLeerWeb myLeer = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);

        public FrmStatusProveedor()
        {
            InitializeComponent();
        }

        private void FrmStatusProveedor_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            BuscarStatus();
        }

        #region Buscar Status 
        private void BuscarStatus()
        {
            string sSql = String.Format("Select * From COM_OCEN_Proveedores_Status(NoLock) Where IdProveedor = '{0}' ", GnProveedores.IdProveedor);

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
            txtObservaciones.Text = myLeer.Campo("ObservacionesProveedor");

            if (myLeer.Campo("StatusProveedor") == "C")
            {
                //Si esta cancelado, se permite activar.
                lblStatus.Text = "CANCELADO";
                InicializaToolBar(false, true, false, false);
            }
            else
            {
                //Si esta activo, se permite cancelar.
                lblStatus.Text = "ACTIVO";
                InicializaToolBar(false, false, true, false);
            }
        }
        #endregion Buscar Status

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            //lblStatus.Text = "ACTIVO";
            InicializaToolBar(false, false, false, false);
            dtpFechaRegistro.Enabled = false;
            txtObservaciones.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int iOpcion = 1;
            string sMensaje = "", sSql = "";

            if( ValidaDatos())
            {
                sSql = String.Format("Exec spp_Mtto_COM_OCEN_Proveedores_Status '{0}', '', '', '{1}', '{2}' ", 
                    GnProveedores.IdProveedor, txtObservaciones.Text.Trim(), iOpcion );

                if (myLeer.Exec(sSql))
                {
                    if (myLeer.Leer())
                    {
                        sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        this.Close();
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
                sSql = String.Format("Exec spp_Mtto_COM_OCEN_Proveedores_Status '{0}', '', '', '{1}', '{2}' ",
                    GnProveedores.IdProveedor, txtObservaciones.Text.Trim(), iOpcion);

                if (myLeer.Exec(sSql))
                {
                    if (myLeer.Leer())
                    {
                        sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        this.Close();
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
