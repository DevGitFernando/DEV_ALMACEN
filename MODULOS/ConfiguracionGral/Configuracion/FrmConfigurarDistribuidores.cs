using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft; 

namespace Configuracion.Configuracion
{
    public partial class FrmConfigurarDistribuidores : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;
        DataSet dtsFarmacias;

        public FrmConfigurarDistribuidores()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnConfiguracion.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnConfiguracion.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, GnConfiguracion.DatosApp, this.Name);

            ObtenerEstados();
            ObtenerFarmacias();
        }

        private void FrmConfigurarDistribuidores_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Buscar Distribuidor
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            lblCancelado.Visible = false;
            if (txtIdDistribuidor.Text.Trim() != "")
            {
                leer.DataSetClase = query.Distribuidores(txtIdDistribuidor.Text, "txtId_Validating");
                if (leer.Leer())
                {
                    CargarDatos();

                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Distribuidores("txtId_KeyDown");
                if (leer.Leer())
                {
                    CargarDatos();
                }
            }
        }

        private void CargarDatos()
        {
            txtIdDistribuidor.Text = leer.Campo("IdDistribuidor");
            lblDistribuidor.Enabled = false;
            lblDistribuidor.Text = leer.Campo("NombreDistribuidor");

            if (leer.Campo("Status").ToUpper() == "C")
            {
                General.msjUser("El Distribuidor actualmente se encuentra cancelado.");
                txtIdDistribuidor.Text = "";
            }

        }

        #endregion Buscar Distribuidor

        #region Buscar Codigo Cliente
        private void txtCodigoCliente_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodigoCliente.Text.Trim() != "")
            {
                IniciaToolBar(true, true, false);
                string sSql = string.Format("Select * From CFGC_ConfigurarDistribuidor(NoLock)" +
                    "Where IdEstado = '{0}' And IdFarmacia = '{1}' And IdDistribuidor = '{2}' And CodigoCliente = '{3}' ",
                    cboEstados.Data, cboFarmacias.Data, txtIdDistribuidor.Text.Trim(), txtCodigoCliente.Text.Trim());

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "LlenarParametros()");
                    General.msjError("Ocurrió un error al obtener la información de los Parametros.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        IniciaToolBar(true, true, true);
                        txtCodigoCliente.Text = leer.Campo("CodigoCliente");
                        txtCodigoCliente.Enabled = false;

                        txtNombre.Text = leer.Campo("Nombre");
                        txtServidor.Text = leer.Campo("Servidor");
                        txtWebService.Text = leer.Campo("WebService");
                        txtPaginaWeb.Text = leer.Campo("PaginaWeb");
                        chkDistribuidor.Checked = leer.CampoBool("EsDistribuidor");

                        if (leer.Campo("Status").ToUpper() == "C")
                        {
                            IniciaToolBar(true, true, false);
                            lblCancelado.Visible = true;
                            BloquearDatos(true);
                            General.msjUser("El Codigo Cliente actualmente se encuentra cancelado.");
                        }
                    }
                }
            }
        }
        #endregion Buscar Codigo Cliente

        #region Botones 
        private void LimpiarPantalla()
        {
            lblCancelado.Visible = false; 
            Fg.IniciaControles();
            IniciaToolBar(true, false, false);
            BloquearDatos(false);
            cboEstados.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                GuardarInformacion(1);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (validarCancelacion())
            {
                GuardarInformacion(2);
            }
        }

        private bool validarCancelacion()
        {
            bool bRegresa = true;

            if (txtIdDistribuidor.Text.Trim() == "" || txtIdDistribuidor.Text.Trim() == "*")
            {
                bRegresa = false;
                General.msjUser("Codigo de Cliente invalido, verifique."); 
                txtIdDistribuidor.Focus(); 
            }

            if (bRegresa)
            {
                if (General.msjConfirmar("¿ Desea cancelar la información en pantalla ?") == DialogResult.No)
                {
                    bRegresa = false;
                }
            }

            return bRegresa; 
        } 

        private void GuardarInformacion(int iOpcion)
        {
            string sCodigoCliente = txtCodigoCliente.Text.Trim(), sNombre = txtNombre.Text.Trim(), sServidor = txtServidor.Text.Trim(); 
            string sWebService = txtWebService.Text.Trim(), sPaginaWeb = txtPaginaWeb.Text.Trim();
            string sSql = "", sMensaje = "";
            int iEsDistribuidor = 0;

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                if (chkDistribuidor.Checked)
                {
                    iEsDistribuidor = 1;
                }

                sSql = String.Format("Exec spp_Mtto_CFGC_ConfigurarDistribuidor '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', " + 
                "'{7}', '{8}', '{9}' " , cboEstados.Data, cboFarmacias.Data, txtIdDistribuidor.Text.Trim(), 
                sCodigoCliente, sNombre, sServidor, sWebService, sPaginaWeb, iEsDistribuidor, iOpcion);

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
                    General.msjError("Ocurrió un error al guardar la Información.");
                    //btnNuevo_Click(null, null);

                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
            }
                      
        } 
        #endregion Botones         
        
        #region Funciones
        private void IniciaToolBar( bool Nuevo, bool Guardar, bool Cancelar )
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
        }

        private void ObtenerEstados()
        {
            leer.DataSetClase = query.EmpresasEstados("ObtenerEstados");

            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.SelectedIndex = 0;
            cboEstados.Add(leer.DataSetClase, true, "IdEstado", "NombreEstado");
            cboEstados.SelectedIndex = 0;
        }

        private void ObtenerFarmacias()
        {
            dtsFarmacias = new DataSet();

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            cboFarmacias.SelectedIndex = 0;

            dtsFarmacias = query.Farmacias("CargarFarmacias()");
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (cboEstados.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione el Estado por favor.");
            }

            if (bRegresa && cboFarmacias.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione la Farmacia por favor.");
            }

            if (bRegresa && lblDistribuidor.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el Id Distribuidor por favor.");
            }

            if (bRegresa && txtCodigoCliente.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el Codigo Cliente por favor.");
            }

            if (bRegresa && txtNombre.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el Nombre por favor.");
            }

            if (bRegresa && txtServidor.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el Servidor por favor.");
            }

            if (bRegresa && txtWebService.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el WebService por favor.");
            }

            if (bRegresa && txtPaginaWeb.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la Pagina por favor.");
            }

            return bRegresa;
        }


        private void BloquearDatos( bool Bloquear )
        {
            FrameCSGN.Enabled = !Bloquear;
            FrameDatos.Enabled = !Bloquear;
        }

        #endregion Funciones

        #region Eventos
        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            if (cboEstados.SelectedIndex != 0)
            {
                try
                {
                    cboFarmacias.Add(dtsFarmacias.Tables[0].Select(string.Format("IdEstado = '{0}'", cboEstados.Data)), true, "IdFarmacia", "Farmacia");
                }
                catch { }
            }
            cboFarmacias.SelectedIndex = 0;
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtIdDistribuidor_TextChanged(object sender, EventArgs e)
        {
            lblDistribuidor.Text = "";
        }

        #endregion Eventos

    } 

}
