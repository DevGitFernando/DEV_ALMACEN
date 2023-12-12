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

namespace Farmacia.PedidosDeDistribuidor
{
    public partial class FrmDistribuidoresClientes : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmDistribuidoresClientes()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            CargaFarmacias();
        }

        private void FrmMedicos_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            IniciaToolBar(true, true, true, false);
            cboFarmacias.SelectedIndex = 0;
            txtDistribuidor.Focus();
        }

        #region CargarCombos
        private void CargaFarmacias()
        {
            string sSql = "";
            cboFarmacias.Clear();
            cboFarmacias.Add();

            sSql = string.Format(" Select IdFarmacia, (IdFarmacia + ' -- ' + Farmacia) as NombreFarmacia " +
                                  " From vw_Farmacias (Nolock) Where IdEstado = '{0}' and Status = 'A' and IdTipoUnidad <> '005' ", 
                                  DtGeneral.EstadoConectado);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "CargaFarmacias");
                General.msjError("Ocurrió un error al buscar la Información de Farmacias.");
            }
            else
            {
                if (myLeer.Leer())
                {
                    cboFarmacias.Add("0000", "0000 -- SIN ESPECIFICAR");
                    cboFarmacias.Add(myLeer.DataSetClase);
                }
            }

            cboFarmacias.SelectedIndex = 0;
        }
        #endregion CargarCombos

        #region Buscar CodigoCliente
        private void txtCodigoCliente_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);

            string sSql = "";

            if (txtCodigoCliente.Text.Trim() != "")
            {
                sSql = string.Format(" Select * From vw_Distribuidores_Clientes (NoLock) " +
                       " Where IdEstado = '{0}' and IdDistribuidor = '{1}' and CodigoCliente = '{2}' ",
                       DtGeneral.EstadoConectado, Fg.PonCeros(txtDistribuidor.Text, 4), Fg.PonCeros(txtCodigoCliente.Text, 7));
                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "txtCodigoCliente_Validating");
                    General.msjError("Ocurrió un error al buscar la Información del Cliente.");
                }
                else
                {
                    if (myLeer.Leer())
                    {
                        CargaDatosCliente();
                    }
                    else
                    {
                        txtNombreCliente.Focus();
                    }
                }
            } 
        }

        private void txtCodigoCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Distribuidor_Clientes(DtGeneral.EstadoConectado, txtDistribuidor.Text.Trim(), "txtId_KeyDown");

                if (myLeer.Leer())
                {
                    CargaDatosCliente();
                }
            }
        }

        ////private void txtId_Validating(object sender, CancelEventArgs e)
        ////{
                       
        ////}

        private void CargaDatosCliente()
        {
            if (myLeer.Campo("Status").ToUpper() == "A")
            {
                string sNombre = "";
                sNombre = myLeer.Campo("NombreCliente");

                txtCodigoCliente.Text = myLeer.Campo("CodigoCliente");
                txtNombreCliente.Text = sNombre;
                cboFarmacias.Data = myLeer.Campo("IdFarmacia");
            }
            else
            {
                General.msjUser("El Cliente " + myLeer.Campo("NombreCliente") + " actualmente se encuentra cancelado, verifique. ");
                txtCodigoCliente.Text = "";
                txtNombreCliente.Text = "";
                txtCodigoCliente.Focus();
            }
        }

        ////private void txtId_KeyDown(object sender, KeyEventArgs e)
        ////{
            
        ////}
        #endregion Buscar CodigoCliente

        #region Guardar/Actualizar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CatDistribuidores_Clientes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                            DtGeneral.EstadoConectado, txtDistribuidor.Text, txtCodigoCliente.Text.Trim(), txtNombreCliente.Text.Trim(),
                            cboFarmacias.Data, iOpcion);

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
                    Error.LogError(ConexionLocal.MensajeError); 
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo."); 
                } 
            } 
        }
        #endregion Guardar/Actualizar

        #region Eliminar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            //string message = "¿ Desea eliminar el Medico seleccionado ?";

            //Se verifica que no este cancelada.
            if (lblCancelado.Visible == false)
            {                
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CatDistribuidores_Clientes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                    DtGeneral.EstadoConectado, txtDistribuidor.Text, txtCodigoCliente.Text.Trim(), txtNombreCliente.Text.Trim(),
                    cboFarmacias.Data, iOpcion);

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
                        Error.GrabarError(myLeer, "btnCancelar_Click");
                        General.msjError("Ocurrió un error al eliminar el Cliente.");
                        //btnNuevo_Click(null, null);
                    }

                    ConexionLocal.Cerrar();
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

        #endregion Eliminar

        #region Validaciones de Controles
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtDistribuidor.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Distribuidor inválida, verifique.");
                txtDistribuidor.Focus();
            }

            if ( bRegresa && txtCodigoCliente.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Médico inválida, verifique.");
                txtCodigoCliente.Focus();
            }

            if (bRegresa && txtNombreCliente.Text.Trim() == "" )
            {
                bRegresa = false;
                General.msjUser("No capturado el Nombre, verifique.");
                txtNombreCliente.Focus();
            }
           
            if (bRegresa && cboFarmacias.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No se ha seleccionado una Unidad, verifque.");
                cboFarmacias.Focus();
            }

            return bRegresa;
        }
        #endregion Validaciones de Controles       

        #region Funciones 
        private void IniciaToolBar(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }
        private void LlenaEspecialidades()
        {
            myLeer = new clsLeer(ref ConexionLocal);

            cboFarmacias.Add("0", "<< Seleccione >>");

            myLeer.DataSetClase = Consultas.Especialidades("", "LlenaEspecialidades()");
            if (myLeer.Leer())
            {
                cboFarmacias.Add(myLeer.DataSetClase, true);
            }
            cboFarmacias.SelectedIndex = 0;
        }
        #endregion Funciones

        #region Busca_Distribuidor
        private void txtDistribuidor_Validating(object sender, CancelEventArgs e)
        {
            if (txtDistribuidor.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Distribuidores(txtDistribuidor.Text.Trim(), "txtDistribuidor_Validating");
                if (myLeer.Leer())
                {
                    CargaDatosDistribuidor();                    
                }
                else
                {
                    txtDistribuidor.Focus();
                }
            }
        }

        private void txtDistribuidor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Distribuidores("txtDistribuidor_KeyDown");

                if (myLeer.Leer())
                {
                    CargaDatosDistribuidor();
                }
            }
        }

        private void CargaDatosDistribuidor()
        {
            //Se hace de esta manera para la ayuda. 

            if (myLeer.Campo("Status").ToUpper() == "A")
            {
                txtDistribuidor.Text = myLeer.Campo("IdDistribuidor");
                lblDistribuidor.Text = myLeer.Campo("NombreDistribuidor");
            }
            else
            {
                General.msjUser("El Distribuidor " + myLeer.Campo("NombreDistribuidor") + " actualmente se encuentra cancelado, verifique. ");
                txtDistribuidor.Text = "";
                lblDistribuidor.Text = "";
                txtDistribuidor.Focus();
            }
        }
        #endregion Busca_Distribuidor

        
    } //Llaves de la clase
}
