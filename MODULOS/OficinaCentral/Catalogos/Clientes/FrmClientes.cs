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
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using OficinaCentral;

namespace OficinaCentral.Catalogos
{
    public partial class FrmClientes : FrmBaseExt
    {
        /// <summary>
        /// Lista de Columnas 
        /// </summary>
        private enum Cols
        {
            Id = 1, Nombre = 2, Utilidad = 3,
            CapturaBeneficiarios = 4, ImportaBeneficiarios = 5, Status = 6
        }

        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        clsGrid myGridSubClientes;

        bool bInicioPantalla = true;
        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();

        public FrmClientes()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version,false);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            grdSubClientes.EditModeReplace = false;
            myGridSubClientes = new clsGrid(ref grdSubClientes, this);
            myGridSubClientes.EstiloGrid(eModoGrid.ModoRow);

            Inicializa();
        }

        #region Inicializa

        private void Inicializa()
        {
            cboMunicipios.Add("0", "<< Seleccione >>");
            cboColonia.Add("0", "<< Seleccione >>");
            LlenaEstados();

            CargarTiposDeCliente();
            btnNuevo_Click(null, null);
        }

        private void LlenaEstados()
        {
            myLlenaDatos = new clsLeer(ref ConexionLocal);

            cboEstados.Add("0", "<< Seleccione >>");
            myLlenaDatos.DataSetClase = Consultas.ComboEstados("LlenaEstados"); ;
            if (myLlenaDatos.Leer())
            {
                cboEstados.Add(myLlenaDatos.DataSetClase, true);
                cboEstados.SelectedIndex = 0;
            }
        }

        private void LlenaMunicipios()
        {
            string sIdEstado = "";
            myLlenaDatos = new clsLeer(ref ConexionLocal);

            sIdEstado = cboEstados.Data.Trim();
            myLlenaDatos.DataSetClase = Consultas.ComboMunicipios(sIdEstado, "LlenaMunicipios");
            if (myLlenaDatos.Leer())
            {
                cboMunicipios.Clear();
                cboMunicipios.Add("0", "<< Seleccione >>");
                cboMunicipios.Add(myLlenaDatos.DataSetClase, true);
                cboMunicipios.SelectedIndex = 0;
            }

        }

        private void LlenaColonias()
        {
            string sIdEstado = "", sIdMunicipio = "";
            myLlenaDatos = new clsLeer(ref ConexionLocal);

            sIdEstado = cboEstados.Data.Trim();
            sIdMunicipio = cboMunicipios.Data.Trim();

            myLlenaDatos.DataSetClase = Consultas.ComboColonias(sIdEstado, sIdMunicipio, "LlenaColonias");
            if (myLlenaDatos.Leer())
            {
                cboColonia.Clear();
                cboColonia.Add("0", "<< Seleccione >>");
                cboColonia.Add(myLlenaDatos.DataSetClase, true);
                cboColonia.SelectedIndex = 0;
            }

        }

        #endregion Inicializa

        #region Limpiar
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true, FrameDatosCliente);
            Fg.IniciaControles(this, true, FrameSubClientes);
            Fg.IniciaControles(this, true, FrameDatosContables);

            //Fg.IniciaControles(this, true, tabClientes ); //MARCA ERROR NEO

            // Se tuvo que poner a pie debido a que la funcion Inicia Controles marca error con los Tabs. 10-Abril-09.
            Fg.BloqueaControles(this, true);

            txtId.Enabled = true;
            txtId.Text = "";
            txtNombre.Text = "";
            txtRFC.Text = "";
            txtNombre.Text = "";

            //Se posicionan el Combo Estado en la primer opcion.            
            cboEstados.SelectedIndex = 0;

            //Se limpia el combo Municipios para que solo muestre los del Estado seleccionado.
            cboMunicipios.Clear();
            cboMunicipios.Add("0", "<< Seleccione >>");
            cboMunicipios.SelectedIndex = 0;

            //Se limpia el combo Colonias para que solo muestre los del Estado seleccionado.
            cboColonia.Clear();
            cboColonia.Add("0", "<< Seleccione >>");
            cboColonia.SelectedIndex = 0;

            txtDomicilio.Text = "";
            txtCodigoPostal.Text = "";
            txtTelefonos.Text = "";

            //Se limpian los checkbox
            chkCredito.Checked = false;
            chkSuspendido.Checked = false;

            txtCredito.Text = "0.00";
            txtSaldoActual.Text = "0.00";
            txtCtaMay.Text = "";
            txtSubCta.Text = "";
            txtSSbCta.Text = "";
            txtSSSCta.Text = "";

            myGridSubClientes.Limpiar(true);
            // myGridSubClientes.AddRow();
            limpiaChecksGridSubClientes();

            lblCancelado.Visible = false;
            tabClientes.SelectTab(0);

            if (bInicioPantalla)
            {
                bInicioPantalla = false;
                SendKeys.Send("{TAB}");
            }
            txtId.Focus();
        }

        private void limpiaChecksGridSubClientes()
        {
            int i = 0, iRenglones = 0;

            iRenglones = myGridSubClientes.Rows;
            for (i = 1; i <= iRenglones; i++)
            {
                myGridSubClientes.SetValue(i, (int)Cols.Id, 0);
            }

        }
        #endregion Limpiar


        #region Buscar Cliente 
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtId.Text.Trim() == "")
            {
                txtId.Enabled = false;
                txtId.Text = "*";
            }
            else
            {
                myLeer.DataSetClase = Consultas.Clientes(txtId.Text.Trim(), "txtId_Validating");
                if (myLeer.Leer())
                    CargaDatos();
                else
                    btnNuevo_Click(null, null);
            }
        }

        private void CargaDatos()
        {
            // Correcion, en los combos se debe usar la propiedad .Data para asiganar un valor de la lista
            // la propiedad .SelectIndex no es confiable, no siempre se tendra el numero de Items en la lista

            //Se hace de esta manera para la ayuda.
            txtId.Text = myLeer.Campo("IdCliente");
            txtNombre.Text = myLeer.Campo("Nombre");
            txtRFC.Text = myLeer.Campo("RFC");
            cboTipoCliente.Data = myLeer.Campo("IdTipoCliente");
            cboEstados.Data = myLeer.Campo("IdEstado");

            LlenaMunicipios(); //Se llena el combo de Municipios de acuerdo al Estado.  
            cboMunicipios.Data = myLeer.Campo("IdMunicipio"); //Se selecciona el Municipios del Cliente.

            LlenaColonias(); //Se llena el combo de Colonias de acuerdo al Municipio.  
            cboColonia.Data= myLeer.Campo("IdColonia"); //Se selecciona la Colonia del Cliente.

            txtDomicilio.Text = myLeer.Campo("Domicilio");
            txtCodigoPostal.Text = myLeer.Campo("CodigoPostal");
            txtTelefonos.Text = myLeer.Campo("Telefonos");

            if (myLeer.CampoBool("TieneLimiteDeCredito"))
            {
                chkCredito.Checked = true;
                txtCredito.Text = myLeer.Campo("LimiteDeCredito");
            }

            if (myLeer.CampoBool("CreditoSuspendido"))
                chkSuspendido.Checked = true;

            txtSaldoActual.Text = myLeer.Campo("SaldoActual");
            txtCtaMay.Text = myLeer.Campo("CtaMay");
            txtSubCta.Text = myLeer.Campo("SubCta");
            txtSSbCta.Text = myLeer.Campo("SSbCta");
            txtSSSCta.Text = myLeer.Campo("SSSCta");

            CargaSubClientes();

            txtId.Enabled = false;

            if (myLeer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                Fg.BloqueaControles(this, false);
            }
 
        }

        private void CargaSubClientes()
        {
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            myLlenaDatos.DataSetClase = Consultas.SubClientes(txtId.Text.Trim(), "CargaSubClientes");
            {
                myGridSubClientes.Limpiar(true);
                if (myLlenaDatos.Leer())
                {
                    myGridSubClientes.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
                }
            }

        }

        private void CargarTiposDeCliente()
        {
            cboTipoCliente.Clear();
            cboTipoCliente.Add("0", "<< Seleccione >>");
            cboTipoCliente.Add(Consultas.TiposDeClientes("CargarTiposDeCliente"), true, "IdTipoCliente", "Descripcion");
            cboTipoCliente.SelectedIndex = 0;
        }

        #endregion Buscar Cliente

        #region Guardar/Actualizar Cliente

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;
            string sSql = "", sMensaje = "", sIdCliente = "";
            int iTieneLimiteDeCredito = 0, iCreditoSuspendido = 0;
            float fCredito = 0, fSaldoActual = 0;
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                if (chkCredito.Checked)
                    iTieneLimiteDeCredito = 1;
                if (chkSuspendido.Checked)
                    iCreditoSuspendido = 1;

                fCredito = float.Parse(txtCredito.Text);
                fSaldoActual = float.Parse(txtSaldoActual.Text);

                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CatClientes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', " +
                        "'{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}' ",
                        txtId.Text.Trim(), txtNombre.Text.Trim(), txtRFC.Text.Trim(), cboTipoCliente.Data,
                        cboEstados.Data, cboMunicipios.Data, cboColonia.Data, txtDomicilio.Text.Trim(), 
                        txtCodigoPostal.Text.Trim(), txtTelefonos.Text.Trim(), iTieneLimiteDeCredito, fCredito, 
                        iCreditoSuspendido, fSaldoActual, txtCtaMay.Text, txtSubCta.Text, txtSSbCta.Text, 
                        txtSSSCta.Text, iOpcion );

                    if (myLeer.Exec(sSql))
                    {
                        if (myLeer.Leer())
                        {
                            sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                            sIdCliente = String.Format("{0}", myLeer.Campo("Clave"));

                            bContinua = GuardarSubClientes(sIdCliente);
                        }

                    }

                    if( bContinua )
                    {
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

        private bool GuardarSubClientes(string sIdCliente)
        {
            bool bRegresa = true, bCheck = false;
            int i = 0, iRenglones = 0, iStatus = 0;
            string sNombre = "", sIdSubCliente = "", sSql = ""; //sMensaje = "", 

            iRenglones = myGridSubClientes.Rows;
            for (i = 1; i <= iRenglones; i++)
            {
                bCheck = myGridSubClientes.GetValueBool(i, (int)Cols.Status);
                if (bCheck)
                    iStatus = 1;
                else
                    iStatus = 0;

                sNombre = myGridSubClientes.GetValue(i, (int)Cols.Nombre).Trim();
                sIdSubCliente = myGridSubClientes.GetValue(i, (int)Cols.Id);

                if (sIdSubCliente == "")
                {
                    sIdSubCliente = "*";
                    iStatus = 1; //Si es nuevo se pone activo aun cuando el check este apagado.
                }

                sSql = String.Format("Exec spp_Mtto_CatSubClientes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                        sIdCliente, sIdSubCliente, sNombre, 
                        myGridSubClientes.GetValueDou(i, (int)Cols.Utilidad),
                        myGridSubClientes.GetValueInt(i, (int)Cols.CapturaBeneficiarios),
                        myGridSubClientes.GetValueInt(i, (int)Cols.ImportaBeneficiarios), 
                        iStatus);

                if (sNombre != "")
                {
                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        Error.GrabarError(myLeer, "GuardarSubClientes()");
                        break;
                    }
                }

            }

            return bRegresa;
        }

        #endregion Guardar/Actualizar Cliente

        #region Eliminar Cliente

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar el Cliente seleccionado ?";

            //Se verifica que no este cancelada.
            if (lblCancelado.Visible == false)
            {
                txtId_Validating(txtId.Text, null);//Se manda llamar este evento para validar que exista el Cliente.
                if (txtNombre.Text.Trim() != "") //Si no esta vacio, significa que si existe.
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if (ConexionLocal.Abrir())
                        {
                            ConexionLocal.IniciarTransaccion();

                            sSql = String.Format("Exec spp_Mtto_CatClientes '{0}', '', '', '', '', '', " + 
                                "'', '', '', '', '0', '0', '0', '0', '', '', '', '', '{1}' ",
                                txtId.Text.Trim(), iOpcion);

                            //sSql = String.Format("Exec spp_Mtto_CatClientes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', " +
                            //    "'{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}' ",
                            //    txtId.Text.Trim(), txtNombre.Text.Trim(), txtRFC.Text.Trim(), cboTipoCliente.Data,
                            //    cboEstados.Data, cboMunicipios.Data, cboColonia.Data, txtDomicilio.Text.Trim(),
                            //    txtCodigoPostal.Text.Trim(), txtTelefonos.Text.Trim(), iTieneLimiteDeCredito, fCredito,
                            //    iCreditoSuspendido, fSaldoActual, txtCtaMay.Text, txtSubCta.Text, txtSSbCta.Text,
                            //    txtSSSCta.Text, iOpcion);

                            if (myLeer.Exec(sSql))
                            {
                                if (myLeer.Leer())
                                    sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));

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
                }
            }
            else
            {
                General.msjUser("Este Cliente ya esta cancelado");
            }


        }

        #endregion Eliminar Cliente

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            int i = 0;

            for (i = 0; i <= 1; i++)
            {
                if (txtId.Text == "")
                {
                    General.msjUser("Ingrese la Clave Cliente por favor");
                    txtId.Focus();
                    bRegresa = false;
                    break;
                }

                if (txtNombre.Text == "")
                {
                    General.msjUser("Ingrese el Nombre por favor");
                    txtNombre.Focus();
                    bRegresa = false;
                    break;
                }

                if (txtRFC.Text == "")
                {
                    General.msjUser("Ingrese el RFC por favor");
                    txtRFC.Focus();
                    bRegresa = false;
                    break;
                }

                if (cboEstados.Data == "0")
                {
                    General.msjUser("Seleccione un Estado por favor");
                    cboEstados.Focus();
                    bRegresa = false;
                    break;
                }

                if (cboMunicipios.Data == "0")
                {
                    General.msjUser("Seleccione un Municipio por favor");
                    cboMunicipios.Focus();
                    bRegresa = false;
                    break;
                }

                if (cboColonia.Data == "0")
                {
                    General.msjUser("Seleccione una Colonia por favor");
                    cboColonia.Focus();
                    bRegresa = false;
                    break;
                }

                if (txtDomicilio.Text == "")
                {
                    General.msjUser("Ingrese el Domicilio por favor");
                    txtDomicilio.Focus();
                    bRegresa = false;
                    break;
                }

                if (txtCodigoPostal.Text == "")
                {
                    General.msjUser("Ingrese el Codigo Postal por favor");
                    txtCodigoPostal.Focus();
                    bRegresa = false;
                    break;
                }

                if (txtTelefonos.Text == "")
                {
                    General.msjUser("Ingrese el Telefono por favor");
                    txtTelefonos.Focus();
                    bRegresa = false;
                    break;
                }

                if (chkCredito.Checked)
                {
                    if (double.Parse(txtCredito.Text) <= 0)
                    {
                        General.msjUser("El Limite de Credito debe ser mayor a cero");
                        txtCredito.Focus();
                        bRegresa = false;
                        break;
                    }
                }

                //if (double.Parse(txtSaldoActual.Text) <= 0)
                //{
                //    General.msjUser("El Saldo Actual debe ser mayor a cero");
                //    txtSaldoActual.Focus();
                //    bRegresa = false;
                //    break;
                //}

                //if (txtCtaMay.Text == "")
                //{
                //    General.msjUser("Ingrese la Cuenta Mayor por favor");
                //    txtCtaMay.Focus();
                //    bRegresa = false;
                //    break;
                //}

                //if (txtSSbCta.Text == "")
                //{
                //    General.msjUser("Ingrese la SSB Cuenta por favor");
                //    txtSSbCta.Focus();
                //    bRegresa = false;
                //    break;
                //}
               
                //if (txtSubCta.Text == "")
                //{
                //    General.msjUser("Ingrese la SubCuenta por favor");
                //    txtSubCta.Focus();
                //    bRegresa = false;
                //    break;
                //}

                //if (txtSSSCta.Text == "")
                //{
                //    General.msjUser("Ingrese la SSS Cuenta por favor");
                //    txtSSSCta.Focus();
                //    bRegresa = false;
                //    break;
                //}

            }
            return bRegresa;
        }

        #endregion Validaciones de Controles

        #region Eventos

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Clientes("txtId_KeyDown");

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
            }

        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboMunicipios.Clear();
            cboColonia.Clear();

            cboMunicipios.Add("0", "<< Seleccione >>");
            cboColonia.Add("0", "<< Seleccione >>");
            cboMunicipios.SelectedIndex = 0;
            cboColonia.SelectedIndex = 0;

            if (cboEstados.Data != "0")
                LlenaMunicipios();
        }

        private void cboMunicipios_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboColonia.Clear();            
            cboColonia.Add("0", "<< Seleccione >>");
            cboColonia.SelectedIndex = 0;

            if (cboMunicipios.Data != "0")
                LlenaColonias();
        }

        private void chkCredito_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCredito.Checked)
            {
                txtCredito.Enabled = true;
            }
            else
            {
                txtCredito.Text = "0.00";
                txtCredito.Enabled = false;
            }
        }

        #endregion Eventos

        #region Validaciones Grid

        private void grdSubClientes_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            string sValor = "";

            if ((myGridSubClientes.ActiveRow == myGridSubClientes.Rows) && e.AdvanceNext)
            {
                sValor = myGridSubClientes.GetValue(myGridSubClientes.ActiveRow, (int)Cols.Nombre);
                if (sValor != "")
                {
                    myGridSubClientes.Rows = myGridSubClientes.Rows + 1;
                    myGridSubClientes.ActiveRow = myGridSubClientes.Rows;
                    myGridSubClientes.SetActiveCell(myGridSubClientes.Rows, (int)Cols.Nombre);
                }
            }
        }

        #endregion Validaciones Grid

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //if (txtId.Text.Trim() != "" || txtId.Text.Trim() != "*")
            //{
            //    General.msjUser("Clave de Cliente inválida, verifique.");
            //}
            //else
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;
                bool bRegresa = false; 

                myRpt.RutaReporte = GnOficinaCentral.RutaReportes;
                myRpt.NombreReporte = "Central_ListadoDeClientes";

                bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);

                ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////DataSet datosC = DatosCliente.DatosCliente();

                ////btReporte = conexionWeb.Reporte(InfoWeb, datosC);

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                } 
            }
        }
    
    } //Llaves de la clase
}
