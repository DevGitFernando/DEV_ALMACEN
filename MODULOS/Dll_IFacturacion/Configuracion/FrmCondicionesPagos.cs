using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;

using DllFarmaciaSoft; 

namespace Dll_IFacturacion.Configuracion
{
    public partial class FrmCondicionesPagos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        
        clsAyudas Ayudas;
        clsConsultas Consultas;

        clsDatosCliente DatosCliente;
        string sFolio = "";
        string sMensaje = "";

        public FrmCondicionesPagos()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "CondicionesPagos");

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);

            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);
        }

        private void FrmCondicionesPagos_Load(object sender, EventArgs e)
        {
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
                        
            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardarInformacion(1);

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        txtId.Text = sFolio;
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP                        
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        //Error.GrabarError(leer, "btnGuardar_Click");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la Información.");

                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardarInformacion(2);

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        txtId.Text = sFolio;
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP                        
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        //Error.GrabarError(leer, "btnGuardar_Click");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al Cancelar la Información.");

                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }

            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            txtId.Focus();
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtId.Text.Trim() == "")
            {
                General.msjUser("Ingrese la Clave Condición de Pago por favor");
                txtId.Focus();
                bRegresa = false;
            }

            if ( bRegresa && txtDescripcion.Text.Trim() == "")
            {
                General.msjUser("Ingrese la Descripción por favor");
                txtDescripcion.Focus();
                bRegresa = false;                
            }

            return bRegresa;
        }

        private void CargaDatos()
        {
            txtId.Text = leer.Campo("IdCondicionPago");
            txtDescripcion.Text = leer.Campo("Descripcion");
            txtId.Enabled = false;

            if (leer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                //txtDescripcion.Enabled = false;
            }
        }
        #endregion Funciones
        
        #region Eventos_CondicionPago
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "";

            if (txtId.Text.Trim() == "")
            {
                txtId.Text = "*";
                txtId.Enabled = false;
                txtDescripcion.Focus();
            }
            else
            {
                sSql = string.Format(" Select * From FACT_CFD_CatCondicionesPago (Nolock) Where IdCondicionPago = '{0}' ",
                                        Fg.PonCeros(txtId.Text, 2));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtId_Validating");
                    General.msjError("Ocurrió un error al obtener datos de la Condición de Pago.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        CargaDatos();
                    }
                    else
                    {
                        General.msjAviso("No se encontro la Condición de Pago.. Verifique..");
                    }
                }
            }
        }
        #endregion Eventos_CondicionPago

        #region Guardar/Cancelar_Informacion
        private bool GuardarInformacion(int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_Mtto_FACT_CFD_CatCondicionesPago '{0}', '{1}', '{2}' ",txtId.Text, txtDescripcion.Text, iOpcion );

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "GuardarInformacion");
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
        #endregion Guardar/Cancelar_Informacion
    }
}
