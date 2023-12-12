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
    public partial class FrmGruposPerfilesAtencion : FrmBaseExt
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

        string sIdPublicoGral = GnFarmacia.PublicoGral;        

        public FrmGruposPerfilesAtencion()
        {
            InitializeComponent();
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmGruposPerfilesAtencion");

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);
        }

        private void FrmGruposPerfilesAtencion_Load(object sender, EventArgs e)
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
                            General.msjAviso("La información se grabó satisfactoriamente..");
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
                            General.msjAviso("La información se canceló satisfactoriamente..");
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

        #region Eventos_Cliente
        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            leer2 = new clsLeer(ref cnn);
            if (txtCte.Text.Trim() == "")
            {
                txtCte.Text = "";
                lblCliente.Text = "";
                txtSubCte.Text = "";
                lblSubCliente.Text = "";
                txtPro.Text = "";
                lblPrograma.Text = "";
                txtSubPro.Text = "";
                lblSubPrograma.Text = "";
                //txtCte.Focus();
            }
            else
            {
                leer2.DataSetClase = query.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, "txtCte_Validating");
                if (!leer2.Leer())
                {
                    txtCte.Focus();
                }
                else
                {
                    txtCte.Enabled = false;
                    txtCte.Text = leer2.Campo("IdCliente");
                    lblCliente.Text = leer2.Campo("NombreCliente");
                }
            }
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer = new clsLeer(ref cnn);

                leer.DataSetClase = ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, "txtCte_KeyDown");
                if (leer.Leer())
                {
                    txtCte.Enabled = false;
                    txtCte.Text = leer.Campo("IdCliente");
                    lblCliente.Text = leer.Campo("NombreCliente");
                }
                else
                {
                    txtCte.Focus();
                }
            }
        }

        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            lblCliente.Text = "";
            txtSubCte.Text = "";
            lblSubCliente.Text = "";
            txtPro.Text = "";
            lblPrograma.Text = "";
            txtSubPro.Text = "";
            lblSubPrograma.Text = "";
        }
        #endregion Eventos_Cliente        

        #region Eventos_SubCliente
        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCte.Text.Trim() == "")
            {
                txtSubCte.Text = "";
                lblSubCliente.Text = "";
                txtPro.Text = "";
                lblPrograma.Text = "";
                txtSubPro.Text = "";
                lblSubPrograma.Text = "";
                //txtSubCte.Focus();
            }
            else
            {
                leer2 = new clsLeer(ref cnn);

                leer2.DataSetClase = query.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, "txtSubCte_Validating");
                if (!leer2.Leer())
                {
                    txtSubCte.Focus();
                }
                else
                {
                    txtSubCte.Enabled = false;
                    txtSubCte.Text = leer2.Campo("IdSubCliente");
                    lblSubCliente.Text = leer2.Campo("NombreSubCliente");
                    //IniciaToolBar(true, false);
                }
            }
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "")
                {
                    leer = new clsLeer(ref cnn);

                    leer.DataSetClase = ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, "txtSubCte_KeyDown");
                    if (leer.Leer())
                    {
                        txtSubCte.Enabled = false;
                        txtSubCte.Text = leer.Campo("IdSubCliente");
                        lblSubCliente.Text = leer.Campo("NombreSubCliente");
                        //IniciaToolBar(true, false);
                    }
                    else
                    {
                        txtSubCte.Focus();
                    }
                }
            }
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCliente.Text = "";
            txtPro.Text = "";
            lblPrograma.Text = "";
            txtSubPro.Text = "";
            lblSubPrograma.Text = "";
        }
        #endregion Eventos_SubCliente        

        #region Eventos_Programa
        private void txtPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "")
            {
                leer2 = new clsLeer(ref cnn);

                leer2.DataSetClase = query.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtPro.Text, "txtPro_Validating");
                if (!leer2.Leer())
                {
                    txtPro.Focus();
                }
                else
                {
                    txtPro.Text = leer2.Campo("IdPrograma");
                    lblPrograma.Text = leer2.Campo("Programa");
                    txtSubPro.Text = "";
                    lblSubPrograma.Text = "";
                    txtSubPro.Focus();
                }
            }
            else
            {
                txtPro.Text = "";
                lblPrograma.Text = "";
                txtSubPro.Text = "";
                lblSubPrograma.Text = "";
            }
        }

        private void txtPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "")
                {
                    leer = new clsLeer(ref cnn);

                    leer.DataSetClase = ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, "txtPro_KeyDown");
                    if (leer.Leer())
                    {
                        txtPro.Text = leer.Campo("IdPrograma");
                        lblPrograma.Text = leer.Campo("Programa");
                        txtSubPro.Text = "";
                        lblSubPrograma.Text = "";
                        txtSubPro.Focus();
                    }
                }
            }
        }

        private void txtPro_TextChanged(object sender, EventArgs e)
        {
            lblPrograma.Text = "";
            txtSubPro.Text = "";
            lblSubPrograma.Text = "";
        }
        #endregion Eventos_Programa        

        #region Eventos_SubPrograma
        private void txtSubPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "" && txtSubPro.Text.Trim() != "")
            {
                leer2 = new clsLeer(ref cnn);

                leer2.DataSetClase = query.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtPro.Text, txtSubPro.Text, "txtSubPro_Validating");
                if (!leer2.Leer())
                {
                    txtSubPro.Focus();
                }
                else
                {
                    txtSubPro.Text = leer2.Campo("IdSubPrograma");
                    lblSubPrograma.Text = leer2.Campo("SubPrograma");
                }
            }
            else
            {
                txtSubPro.Text = "";
                lblSubPrograma.Text = "";
            }
        }

        private void txtSubPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "")
                {
                    leer = new clsLeer(ref cnn);

                    leer.DataSetClase = ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtPro.Text, "txtPro_KeyDown");
                    if (leer.Leer())
                    {
                        txtSubPro.Text = leer.Campo("IdSubPrograma");
                        lblSubPrograma.Text = leer.Campo("SubPrograma");
                    }
                }
            }
        }

        private void txtSubPro_TextChanged(object sender, EventArgs e)
        {
            lblSubPrograma.Text = "";
        }
        #endregion Eventos_SubPrograma        

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
                leer.DataSetClase = query.PerfilesAtencion(sEmpresa, sEstado, sFarmacia, txtPerfilAtencion.Text, "txtPerfilAtencion_Validating");

                if (leer.Leer())
                {
                    txtPerfilAtencion.Enabled = false; 
                    txtPerfilAtencion.Text = leer.Campo("IdPerfilAtencion");
                    txtDesc.Text = leer.Campo("PerfilDeAtencion");
                    txtCte.Text = leer.Campo("IdCliente");
                    lblCliente.Text = leer.Campo("NombreCliente");
                    txtSubCte.Text = leer.Campo("IdSubCliente");
                    lblSubCliente.Text = leer.Campo("NombreSubCliente");
                    txtPro.Text = leer.Campo("IdPrograma");
                    lblPrograma.Text = leer.Campo("Programa");
                    txtSubPro.Text = leer.Campo("IdSubPrograma");
                    lblSubPrograma.Text = leer.Campo("SubPrograma");

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
                leer.DataSetClase = ayuda.PerfilesDeAtencion(sEmpresa, sEstado, sFarmacia, "txtPerfilAtencion_KeyDown");
                if (leer.Leer())
                {
                    txtPerfilAtencion.Text = leer.Campo("IdPerfilAtencion");
                    txtDesc.Text = leer.Campo("PerfilDeAtencion");
                    txtCte.Text = leer.Campo("IdCliente");
                    lblCliente.Text = leer.Campo("NombreCliente");
                    txtSubCte.Text = leer.Campo("IdSubCliente");
                    lblSubCliente.Text = leer.Campo("NombreSubCliente");
                    txtPro.Text = leer.Campo("IdPrograma");
                    lblPrograma.Text = leer.Campo("Programa");
                    txtSubPro.Text = leer.Campo("IdSubPrograma");
                    lblSubPrograma.Text = leer.Campo("SubPrograma");

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Visible = true;
                        IniciaToolBar(true, false);
                    }
                }
            }
        }
        #endregion Eventos_PerfilAtencion       
              
        #region Validar_Datos
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtCte.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado el Cliente... Verifique!!");
                txtCte.Focus();
            }

            if (bRegresa && txtSubCte.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado el Sub-Cliente... Verifique!!");
                txtSubCte.Focus();
            }

            if (bRegresa && txtPro.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado el Programa... Verifique!!");
                txtPro.Focus();
            }

            if (bRegresa && txtSubPro.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado el Sub-Programa... Verifique!!");
                txtSubPro.Focus();
            }

            return bRegresa;
        }

        private bool ValidarPerfilAtencion()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Select * From CFGC_ALMN_CB_NivelesAtencion (NoLock)  " +
                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' AND IdFarmacia = '{2}'  " +
							    " and IdCliente = '{3}' and IdSubCliente = '{4}' AND IdPrograma = '{5}' AND IdSubPrograma = '{6}' and Descripcion = '{7}' ", 
                                sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4), 
                                Fg.PonCeros(txtPro.Text, 4), Fg.PonCeros(txtSubPro.Text, 4), txtDesc.Text);

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

        #region GuardaR_Cancelar_Infomarcion
        private bool GuardarInformacion(int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_Mtto_CFGC_ALMN_CB_NivelesAtencion '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ", 
                                  sEmpresa, sEstado, sFarmacia, txtPerfilAtencion.Text, Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4),
                                  Fg.PonCeros(txtPro.Text, 4), Fg.PonCeros(txtSubPro.Text, 4), txtDesc.Text, iOpcion);

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }
        #endregion GuardaR_Cancelar_Infomarcion

        private void txtPerfilAtencion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
