using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace Almacen.PerfilesAtencion
{
    public partial class FrmGruposPerfilAtencionDist : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeer leer;
        clsLeer leer2;
        clsConsultas query;
        clsAyudas ayuda;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmGruposPerfilAtencionDist()
        {
            InitializeComponent();
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmGruposPerfilAtencionDist");

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);
        }

        private void FrmGruposPerfilAtencionDist_Load(object sender, EventArgs e)
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
            bool bExito = false;

            if (ValidaDatos())
            {
                if (ValidarPerfilAtencion())
                {
                    if (cnn.Abrir())
                    {
                        cnn.IniciarTransaccion();

                        bExito = GuardarInformacion(1);

                        if (bExito)
                        {
                            cnn.CompletarTransaccion();
                            General.msjAviso("La información se grabo satisfactoriamente..");
                            btnNuevo_Click(null, null);
                        }
                        else
                        {
                            cnn.DeshacerTransaccion();
                            Error.GrabarError(leer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al guardar la información..");
                        }
                    }
                    else
                    {
                        Error.LogError(cnn.MensajeError);
                        General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bool bExito = false;

            if (ValidaDatos())
            {
                //if (ValidarPerfilAtencion())
                {
                    if (cnn.Abrir())
                    {
                        cnn.IniciarTransaccion();

                        bExito = GuardarInformacion(2);

                        if (bExito)
                        {
                            cnn.CompletarTransaccion();
                            General.msjAviso("La información se cancelo satisfactoriamente..");
                            btnNuevo_Click(null, null);
                        }
                        else
                        {
                            cnn.DeshacerTransaccion();
                            Error.GrabarError(leer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al guardar la información..");
                        }
                    }
                    else
                    {
                        Error.LogError(cnn.MensajeError);
                        General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                    }
                }
            }
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            IniciaToolBar(false, false);
            lblCancelado.Visible = false;
            lblCancelado.Text = "CANCELADO";
            txtPerfilAtencion.Focus();
        }

        private void IniciaToolBar(bool Guardar, bool Cancelar)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
        }
        #endregion Funciones

        #region Validar_Datos
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtDesc.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado la descripción... Verifique!!");
                txtDesc.Focus();
            }            

            return bRegresa;
        }

        private bool ValidarPerfilAtencion()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Select * From CFGC_ALMN_DIST_CB_NivelesAtencion (NoLock)  " +
                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' AND IdFarmacia = '{2}'  " +
                                " and Descripcion = '{3}' ",
                                sEmpresa, sEstado, sFarmacia,  txtDesc.Text);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "ValidarPerfilAtencion");
                General.msjError("Ocurrió un error al validar el Perfil de Atención..");
            }
            else
            {
                if (leer.Leer())
                {
                    if (leer.Campo("Status") == "C")
                    {
                        bRegresa = true;
                    }
                    else
                    {
                        General.msjAviso("El Perfil de Atencion ya existe con los criterios especificados..");
                        bRegresa = false;
                    }
                }
            }



            return bRegresa;
        }
        #endregion Validar_Datos

        #region Guardar_Cancelar_Infomarcion
        private bool GuardarInformacion(int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_Mtto_CFGC_ALMN_DIST_CB_NivelesAtencion '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'  ",
                                  sEmpresa, sEstado, sFarmacia, txtPerfilAtencion.Text, txtDesc.Text, iOpcion);

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }
        #endregion Guardar_Cancelar_Infomarcion

        #region Eventos_PerfilAtencion
        private void txtPerfilAtencion_Validating(object sender, CancelEventArgs e)
        {
            if (txtPerfilAtencion.Text.Trim() == "")
            {
                txtPerfilAtencion.Text = "*";
                txtPerfilAtencion.Enabled = false;
                IniciaToolBar(true, true);
            }
            else
            {
                leer.DataSetClase = query.PerfilesAtencionDistribuidor(sEmpresa, sEstado, sFarmacia, txtPerfilAtencion.Text, "txtPerfilAtencion_Validating");

                if (leer.Leer())
                {
                    txtPerfilAtencion.Text = leer.Campo("IdPerfilAtencion");
                    txtDesc.Text = leer.Campo("PerfilDeAtencion");                    

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Visible = true;
                        IniciaToolBar(true, false);
                    }
                    else
                    {
                        IniciaToolBar(true, true);
                    }
                }
            }
        }

        private void txtPerfilAtencion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.PerfilesDeAtencionDistribuidor(sEmpresa, sEstado, sFarmacia, "txtPerfilAtencion_KeyDown");
                if (leer.Leer())
                {
                    txtPerfilAtencion.Text = leer.Campo("IdPerfilAtencion");
                    txtDesc.Text = leer.Campo("PerfilDeAtencion");
                    
                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Visible = true;
                        IniciaToolBar(true, false);
                    }
                }
            }
        }
        #endregion Eventos_PerfilAtencion
    }
}
