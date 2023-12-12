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

using DllFarmaciaSoft;

namespace Dll_SII_INadro.Configuracion
{
    public partial class FrmClientes : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leer2;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        string sEstado = DtGeneral.EstadoConectado;

        public FrmClientes()
        {
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, General.Modulo, this.Name, General.Version);
            Ayuda = new clsAyudas(General.DatosConexion, General.Modulo, this.Name, General.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.Modulo, General.Version, this.Name);            
        }
        
        private void FrmClientes_Load(object sender, EventArgs e)
        {            
            btnNuevo_Click(null, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            IniciaToolBar(true, false, false, false);
            CargarFarmacias();
            txtCliente.Focus();
        }

        #region Funciones
        private void IniciaToolBar(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private void CargarFarmacias()
        {
            string sSql = "";          
            cboFarmacias.Clear();
            cboFarmacias.Add();

            sSql = string.Format(" Select F.IdFarmacia, F.IdFarmacia + ' -- ' + F.Farmacia As Farmacia From vw_Farmacias F " +
                                " Where IdEstado = '{0}' and F.IdTipoUnidad NOT IN ('005', '006') and Not Exists " +
                                " (Select C.IdFarmacia From INT_ND_Clientes C (Nolock) Where C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) " +
                                " Order By F.IdFarmacia ", sEstado);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Ocurrio un error al buscar las Farmacias");
            }
            else
            {
                if (leer.Leer())
                {
                    cboFarmacias.Add(leer.DataSetClase);
                }
            }

            cboFarmacias.SelectedIndex = 0;
        }

        private void CargaDatosFarmacia()
        {
            string sSql = ""; 
            sSql = string.Format(" Select C.IdFarmacia, C.IdFarmacia + ' -- ' + F.Farmacia As Farmacia From INT_ND_Clientes C (Nolock) " +
                " Inner Join vw_Farmacias F (nolock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) " +
                " Where C.IdCliente = '{0}' ",  Fg.PonCeros(txtCliente.Text, 4));

            cboFarmacias.Clear();
            cboFarmacias.Add("0000", "NO ADMINISTRADA");
            cboFarmacias.SelectedIndex = 0;

            if (!leer2.Exec(sSql))
            {
                Error.GrabarError(leer2, "CargaDatosFarmacia()");
                General.msjError("Ocurrio un error al buscar las Farmacias");
            }
            else
            {
                if (leer2.Leer())
                {
                    cboFarmacias.Add(leer2.DataSetClase);
                    cboFarmacias.SelectedIndex = 1;
                }
            }
        }
        #endregion Funciones

        #region Buscar Cliente   
        private void CargaDatosCliente()
        {
            if (leer.Campo("Status").ToUpper() == "A")
            {
                txtCliente.Enabled = false;
                txtCliente.Text = leer.Campo("IdCliente");
                txtCodigoCliente.Text = leer.Campo("CodigoCliente");
                txtNombreCliente.Text = leer.Campo("Nombre"); 

                CargaDatosFarmacia();
                IniciaToolBar(true, true, true, true);
            }
            else
            {
                General.msjUser("El Cliente " + leer.Campo("NombreCliente") + " actualmente se encuentra cancelado, verifique.");
                txtCodigoCliente.Text = "";
                txtNombreCliente.Text = "";
                txtCodigoCliente.Focus();

                IniciaToolBar(true, true, false, true);
            }
        }        
        #endregion Buscar Cliente

        #region Guardar/Actualizar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 1, iSurtimiento = 0; //La opcion 1 indica que es una insercion/actualizacion

            if (chkSurtimiento.Checked)
            {
                iSurtimiento = 1;
                cboFarmacias.SelectedIndex = 0;
            }

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_INT_ND_Clientes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                                        txtCliente.Text, txtCodigoCliente.Text, txtNombreCliente.Text, sEstado, cboFarmacias.Data, iSurtimiento, iOpcion);

                    if (leer.Exec(sSql))
                    {
                        if (leer.Leer())
                        {
                            sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                        }

                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrio un error al guardar la informacion.");
                        //btnNuevo_Click(null, null);

                    }

                    cnn.Cerrar();
                }
                else
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                }
            } 
        }
        #endregion Guardar/Actualizar

        #region Cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 2, iSurtimiento = 0; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea Cancelar el Cliente ?";

            //Se verifica que no este cancelada.
            if (General.msjCancelar(message) == DialogResult.Yes)
            {
                if (lblCancelado.Visible == false)
                {
                    if (cnn.Abrir())
                    {
                        cnn.IniciarTransaccion();

                        sSql = String.Format("Exec spp_Mtto_INT_ND_Clientes '{0}', '{1}', '{2}', '{3}', '{4}' , '{5}', '{6}' ",
                                            txtCliente.Text, txtCodigoCliente.Text, txtNombreCliente.Text, sEstado, cboFarmacias.Data, iSurtimiento, iOpcion);

                        if (leer.Exec(sSql))
                        {
                            if (leer.Leer())
                            {
                                sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                            }

                            cnn.CompletarTransaccion();
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            btnNuevo_Click(null, null);
                        }
                        else
                        {
                            cnn.DeshacerTransaccion();
                            Error.GrabarError(leer, "btnCancelar_Click");
                            General.msjError("Ocurrio un error al eliminar el Cliente.");
                            //btnNuevo_Click(null, null);
                        }

                        cnn.Cerrar();
                    }
                    else
                    {
                        General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                    }

                }
                else
                {
                    General.msjUser("Este Cliente ya esta cancelado");
                }
            }

        }
        #endregion Cancelar

        #region Validaciones de Controles
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtCliente.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Cliente inválida, verifique.");
                txtCliente.Focus();
            }

            if ( bRegresa && txtCodigoCliente.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Codigo de Cliente inválido, verifique.");
                txtCodigoCliente.Focus();
            }

            if (bRegresa && txtNombreCliente.Text.Trim() == "" )
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Nombre, verifique.");
                txtNombreCliente.Focus();
            }   
            

            return bRegresa;
        }
        #endregion Validaciones de Controles                               

        #region Eventos
        private void txtCliente_Validating(object sender, CancelEventArgs e)
        {
            if (txtCliente.Text.Trim() == "")
            {
                txtCliente.Text = "*";
                txtCliente.Enabled = true;
                IniciaToolBar(true, true, false, false);
            }
            else
            {
                leer.DataSetClase = Consultas.Clientes(txtCliente.Text, "txtCliente_Validating");

                if (leer.Leer())
                {
                    CargaDatosCliente();
                }
            }
        }

        private void txtCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Clientes("txtCliente_KeyDown");

                if (leer.Leer())
                {
                    CargaDatosCliente();
                }
            }
        }
        #endregion Eventos

    } //Llaves de la clase
}
