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
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace OficinaCentral.Catalogos
{
    public partial class FrmProveedores : FrmBaseExt
    {
        private enum Cols
        {
            Ninguno = 0, dias = 1, status = 2, Default = 3
        }

        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        clsGrid myGrid;
        bool bInicioPantalla = true;
        int iRenglonSeleccionado = 1;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb;

        //clsOperacionesSupervizadas Permisos;
        string sPermisoUsuariosProveedores = "CREAR_USUARIOS_PROVEEDORES";
        bool bPermisoUsuariosProveedores = false;
        string sMensaje = "";

        //Para Auditoria
        clsAuditoria auditoria;

        public FrmProveedores()
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

            myGrid = new clsGrid(ref grdDiasDePlazo, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.AjustarAnchoColumnasAutomatico = true; 

            SolicitarPermisosUsuario(); 

            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
        }

        private void FrmProveedores_Load(object sender, EventArgs e)
        {
            Inicializa();
        }

        #region Inicializa

        private void Inicializa()
        {
            cboMunicipios.Add("0", "<< Seleccione >>");
            cboColonia.Add("0", "<< Seleccione >>");
            LlenaEstados();
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

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            ////// Valida si el usuario conectado tiene permiso sobre las opcione especiales 
            ////Permisos = new clsOperacionesSupervizadas(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            ////Permisos.Personal = DtGeneral.IdPersonal;
            bPermisoUsuariosProveedores = DtGeneral.PermisosEspeciales.TienePermiso(sPermisoUsuariosProveedores);
            
            if (!DtGeneral.EsAdministrador)
            {
                btnProveedores.Visible = bPermisoUsuariosProveedores; 
            }

        }
        #endregion Permisos de Usuario

        #region Botones  
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool LoginProveedores)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir; 
            btnProveedores.Enabled = LoginProveedores; 
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            DllFarmaciaSoft.Proveedores.FrmCfgAccesoProveedores f = new DllFarmaciaSoft.Proveedores.FrmCfgAccesoProveedores();
            f.ShowLogin(txtId.Text); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //Fg.IniciaControles(this, true, tabProveedores ); //MARCA ERROR NEO

            // Se tuvo que poner a pie debido a que la funcion Inicia Controles marca error con los Tabs. 10-Abril-09.
            Fg.BloqueaControles(this, true);

            IniciarToolBar(false, false, false, false); 
            txtId.Enabled = true;
            txtId.Text = "";
            txtNombre.Text = "";
            txtRFC.Text = "";
            txtAliasNombre.Text = "";
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
            ////txtSaldoActual.Text = "0.00";
            ////txtCtaMay.Text = "";
            ////txtSubCta.Text = "";
            ////txtSSbCta.Text = "";
            ////txtSSSCta.Text = "";

            lblCancelado.Visible = false;
            ////tabProveedores.SelectTab(0);

            myGrid.Limpiar(true);
            myGrid.SetValue((int)Cols.status, 1);
            myGrid.SetValue((int)Cols.Default, 1);

            if (bInicioPantalla)
            {
                bInicioPantalla = false;
                SendKeys.Send("{TAB}");
            }
            txtId.Focus();
        }
        #endregion Botones

        #region Buscar Proveedor

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = ""; 
            myLeer = new clsLeer(ref ConexionLocal);
            IniciarToolBar(false, false, false, false); 

            if (txtId.Text.Trim() == "")
            {
                txtId.Enabled = false;
                txtId.Text = "*";
                IniciarToolBar(true, false, false, false); 
            }
            else
            {
                myLeer.DataSetClase = Consultas.Proveedores(txtId.Text.Trim(), "txtId_Validating");

                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    IniciarToolBar(true, true, true, true); 
                    CargaDatos();
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtId.Text = myLeer.Campo("IdProveedor");
            txtNombre.Text = myLeer.Campo("Nombre");
            txtRFC.Text = myLeer.Campo("RFC");
            txtAliasNombre.Text = myLeer.Campo("AliasNombre");
            cboEstados.Data = myLeer.Campo("IdEstado");

            LlenaMunicipios(); //Se llena el combo de Municipios de acuerdo al Estado.  
            cboMunicipios.Data = myLeer.Campo("IdMunicipio"); //Se selecciona el Municipios del Proveedor.

            LlenaColonias(); //Se llena el combo de Colonias de acuerdo al Municipio.  
            cboColonia.Data = myLeer.Campo("IdColonia"); //Se selecciona la Colonia del Proveedor.

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

            ////txtSaldoActual.Text = myLeer.Campo("SaldoActual");
            ////txtCtaMay.Text = myLeer.Campo("CtaMay");
            ////txtSubCta.Text = myLeer.Campo("SubCta");
            ////txtSSbCta.Text = myLeer.Campo("SSbCta");
            ////txtSSSCta.Text = myLeer.Campo("SSSCta");

            txtId.Enabled = false;

            if (myLeer.Campo("Status").ToUpper() == "C")
            {
                lblCancelado.Visible = true;
                Fg.BloqueaControles(this, false);
            }


            string sSql = string.Format("Select  Dias, Status, Predeterminado  " +
                "From CatProveedores_DiasDePlazo (NoLock) Where IdProveedor = '{0}' Order  BY Predeterminado Desc, Dias Desc", txtId.Text);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "CargaDatos()");
                General.msjError("Ocurrio un error al obetener los dias de plazo");
            }
            else
            {
                myGrid.LlenarGrid(myLeer.DataSetClase);

                for (int i = 1; myGrid.Rows >= i; i++)
                {
                    myGrid.BloqueaCelda(true, i, (int)Cols.dias);
                }
            }
 
        }

        #endregion Buscar Proveedor

        #region Guardar/Actualizar Proveedor

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sCadena = "";
            int iTieneLimiteDeCredito = 0, iCreditoSuspendido = 0;
            float fCredito = 0, fSaldoActual = 0;
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            limpiarRenglones();

            if (ValidaDatos())
            {
                if (chkCredito.Checked) iTieneLimiteDeCredito = 1;
                if (chkSuspendido.Checked) iCreditoSuspendido = 1;

                fCredito = float.Parse(txtCredito.Text);
                fSaldoActual = 0; // float.Parse(txtSaldoActual.Text);

                if (!ConexionLocal.Abrir())
                {
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    ConexionLocal.IniciarTransaccion();

                    if(GuardarEnc())
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
            }            

        }

        #endregion Guardar/Actualizar Proveedor

        #region Eliminar Proveedor

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sCadena = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar el Proveedor seleccionado ?";

            //Se verifica que no este cancelada.
            if (lblCancelado.Visible == false)
            {
                txtId_Validating(txtId.Text, null);//Se manda llamar este evento para validar que exista el Proveedor.
                if (txtNombre.Text.Trim() != "") //Si no esta vacio, significa que si existe.
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if (ConexionLocal.Abrir())
                        {
                            ConexionLocal.IniciarTransaccion();

                            sSql = String.Format("Exec spp_Mtto_CatProveedores '{0}', '', '', '', '', '', '', " + 
                                "'', '', '', '0', '0', '0', '0', '', '', '', '', '{1}', '{2}' ",
                                txtId.Text.Trim(), DtGeneral.IdPersonal, iOpcion);

                            sCadena = sSql.Replace("'", "\"");
                            auditoria.GuardarAud_MovtosUni("*", sCadena);

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
                                General.msjError("Ocurrió un error al eliminar el Proveedor.");
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
                General.msjAviso("Este Proveedor ya esta cancelado");
            }


        }

        #endregion Eliminar Proveedor

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            int i = 0;

            for (i = 1; i <= 1; i++)
            {
                if (txtId.Text == "")
                {
                    General.msjUser("Clave Proveedor inválida, verifique.");
                    txtId.Focus();
                    bRegresa = false;
                    break;
                }

                if (txtNombre.Text == "")
                {
                    General.msjUser("Nombre de Proveedor inválido, verifique.");
                    txtNombre.Focus();
                    bRegresa = false;
                    break;
                }

                if (txtRFC.Text == "")
                {
                    General.msjUser("RFC inválido, verifique.");
                    txtRFC.Focus();
                    bRegresa = false;
                    break;
                }

                if (txtAliasNombre.Text == "")
                {
                    General.msjUser("Alias inválido, verifique.");
                    txtAliasNombre.Focus();
                    bRegresa = false;
                    break;
                }

                if (cboEstados.Data == "0")
                {
                    General.msjUser("No ha seleccionado un Estado, verifique.");
                    cboEstados.Focus();
                    bRegresa = false;
                    break;
                }

                if (cboMunicipios.Data == "0")
                {
                    General.msjUser("No ha seleccionado un Municipip, verifique.");
                    cboMunicipios.Focus();
                    bRegresa = false;
                    break;
                }

                if (cboColonia.Data == "0")
                {
                    General.msjUser("No ha seleccionado una Colonia, verifique.");
                    cboColonia.Focus();
                    bRegresa = false;
                    break;
                }

                if (txtDomicilio.Text == "")
                {
                    General.msjUser("No ha capturado un Domicilio, verifique.");
                    txtDomicilio.Focus();
                    bRegresa = false;
                    break;
                }

                if (txtCodigoPostal.Text == "")
                {
                    General.msjUser("No ha capturado el Código Postal, verifique.");
                    txtCodigoPostal.Focus();
                    bRegresa = false;
                    break;
                }

                if (txtTelefonos.Text == "")
                {
                    General.msjUser("No ha capturado Telefono, verifique.");
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

                //if (myGrid.GetValueInt(1, (int)Cols.dias) == 0)
                //{
                //    General.msjUser("Ingrese al menos un dia de plazo.");
                //    //tabProveedores.SelectedIndex = 1;
                //    bRegresa = false;
                //    break;
                //}


                if (myGrid.TotalizarColumna((int)Cols.Default) != 1)
                {
                    General.msjUser("Seleccione un dia de plazo como default.");
                    //tabProveedores.SelectedIndex = 1;
                    bRegresa = false;
                    break;
                }


                for (int iRow = 1; myGrid.Rows >= iRow; iRow++)
                {
                    if (myGrid.GetValueBool(iRow, (int)Cols.Default))
                    {
                        if (!myGrid.GetValueBool(iRow, (int)Cols.status))
                        {
                            General.msjUser("El dia de plazo default debe estar activo.");
                            //tabProveedores.SelectedIndex = 1;
                            bRegresa = false;
                            break;
                        }
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
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Proveedores("txtId_KeyDown");

                sCadena = Ayuda.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

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

        #region Imprimir
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DatosCliente.Funcion = "btnImprimir_Click()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa; 

            myRpt.RutaReporte = GnOficinaCentral.RutaReportes;
            myRpt.NombreReporte = "Central_Listado_Proveedores";

            bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);

            ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            ////DataSet datosC = DatosCliente.DatosCliente();

            ////btReporte = conexionWeb.Reporte(InfoWeb, datosC);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }

            auditoria.GuardarAud_MovtosUni("*", myRpt.NombreReporte);
        }
        #endregion Imprimir


        private bool GuardarEnc()
        {
            string sSql = "", sCadena = "";
            int iTieneLimiteDeCredito = 0, iCreditoSuspendido = 0;
            float fCredito = 0, fSaldoActual = 0;
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion


            bool bRegresa = true;

            sSql = String.Format("Exec spp_Mtto_CatProveedores " + 
                " @IdProveedor = '{0}', @Nombre = '{1}', @RFC = '{2}', @AliasNombre = '{3}', @IdEstado = '{4}', @IdMunicipio = '{5}', @IdColonia = '{6}', " + 
                " @Domicilio = '{7}', @CodigoPostal = '{8}', @Telefonos = '{9}', @TieneLimiteDeCredito = '{10}', @LimiteDeCredito = '{11}', @CreditoSuspendido = '{12}', " + 
                " @SaldoActual = '{13}', @CtaMay = '{14}', @SubCta = '{15}', @SSbCta = '{16}', @SSSCta = '{17}', @IdPersonalRegistra = '{18}', @iOpcion = '{19}'",
                txtId.Text.Trim(), txtNombre.Text.Trim(), txtRFC.Text.Trim(), txtAliasNombre.Text, cboEstados.Data,
                cboMunicipios.Data, cboColonia.Data, txtDomicilio.Text.Trim(), txtCodigoPostal.Text.Trim(),
                txtTelefonos.Text.Trim(), iTieneLimiteDeCredito, fCredito, iCreditoSuspendido,
                fSaldoActual, "0", "0", "0", "0", DtGeneral.IdPersonal,
                iOpcion);

            sCadena = sSql.Replace("'", "\"");
            auditoria.GuardarAud_MovtosUni("*", sCadena);


            bRegresa = myLeer.Exec(sSql);

            if (myLeer.Leer())
            {
                txtId.Text = myLeer.Campo("IdProveedor");
                sMensaje = myLeer.Campo("Mensaje");
            }

            if (bRegresa)
            {
                bRegresa = GuardarDiasDePlazo();
            } 

            return bRegresa;
        }

        private bool GuardarDiasDePlazo()
        {
            bool bRegresa = true;
            string sSql = "";


            for (int iRow = 1; myGrid.Rows >= iRow && bRegresa; iRow++)
            {
                sSql = String.Format("Exec spp_Mtto_CatProveedores_DiasDePlazo @IdProveedor = '{0}', @Dias = {1}, @Status = {2}, @Predeterminado = {3} ",
                     txtId.Text.Trim(), myGrid.GetValueInt(iRow, (int)Cols.dias), myGrid.GetValueInt(iRow, (int)Cols.status), myGrid.GetValueInt(iRow, (int)Cols.Default));


                bRegresa = myLeer.Exec(sSql);
            }


            return bRegresa;
        }

        private void grdDiasDePlazo_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
            {
                int iValor = myGrid.GetValueInt(myGrid.ActiveRow, (int)Cols.dias);

                if (!BuscarRepetido(iValor, myGrid.ActiveRow))
                {
                    myGrid.Rows = myGrid.Rows + 1;
                    myGrid.ActiveRow = myGrid.Rows;
                    myGrid.SetActiveCell(myGrid.Rows, 1);

                    myGrid.SetValue(myGrid.Rows, (int)Cols.dias, 0);
                    myGrid.SetValue(myGrid.Rows, (int)Cols.status, 1);
                }
                else
                {
                    General.msjUser("la cantidad de dias selecionado ya se encuentra capturado en otro renglon.");
                }
            }
        }

        private void limpiarRenglones()
        {
            //int iDato;
            //for (int i = 2; i <= myGrid.Rows; i++) //Columnas. Nota: Inicia a partir de la 2da.
            //{
            //    iDato = myGrid.GetValueInt(i, (int)Cols.dias);

            //    if (iDato == 0)
            //    {
            //        myGrid.DeleteRow(i);
            //    }
            //}
        }

        private void grdDiasDePlazo_EditModeOff(object sender, EventArgs e)
        {
            
            
            
            int iCantidad = 0, iCant_SobreCompra = 0;

            switch (myGrid.ActiveCol)
            {
                case (int)Cols.dias:
                        {
                            int iValor = myGrid.GetValueInt(myGrid.ActiveRow, (int)Cols.dias);


                            //if (myGrid.BuscaRepetido(sValor, myGrid.ActiveRow, (int)Cols.dias))
                            if (BuscarRepetido(iValor, myGrid.ActiveRow))
                            {
                                General.msjUser("la cantidad de dias selecionado ya se encuentra capturado en otro renglon.");
                                myGrid.SetValue(myGrid.ActiveRow, (int)Cols.dias, "");
                                limpiarRenglones();
                                myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.dias);
                            }
                        }

                    break;
            }

        }

        private bool BuscarRepetido(int iValor, int iRow)
        {
            bool bRegresa = false;

            for (int i = 1; myGrid.Rows >= i; i++)
            {
                if (iRow != i)
                {
                    if (iValor == myGrid.GetValueInt(i, (int)Cols.dias))
                    {
                        bRegresa = true;
                    }
                }
            }
                return bRegresa;
        }

    } //Llaves de la clase
}
