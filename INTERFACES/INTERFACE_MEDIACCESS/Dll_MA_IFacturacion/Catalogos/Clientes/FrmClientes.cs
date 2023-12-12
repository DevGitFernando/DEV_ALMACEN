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
using SC_SolutionsSystem.FuncionesGenerales; 
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using Dll_MA_IFacturacion;

namespace Dll_MA_IFacturacion.Catalogos
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

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer myLlenaDatos;
        clsAyudas_CFDI Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas_CFDI Consultas; 

        bool bInicioPantalla = true;
        clsDatosCliente DatosCliente;

        clsListView lstDirs;
        clsListView lstEmails;
        clsListView lstTels;

        string sIdentificador = "";
        string sMensaje = "";
        bool bCancelado = false; 

        string sIdDireccion = "";
        string sIdEstado = "";
        string sEstado = "";
        string sIdMunipicio = "";
        string sMunipicio = "";
        string sIdColonia = "";
        string sColonia = "";
        string sDireccion = "";
        string sCodigoPostal = "";
        string sStatus = "";

        string sIdEmail = "";
        string sIdTipoEMail = "";
        string sTipoMail = "";
        string sEmail = "";
        string sStatusEmail = "";

        string sIdTelefono = "";
        string sIdTipoTelefono = "";
        string sTipoTelefono = "";
        string sTelefono = "";
        string sStatusTelefono = "";

        string sIdCliente = "";
        string sRFC = ""; 
        string sNombreCliente = "";
        //string sMensaje = "";
        bool bExisteRFC = false;

        bool bLlamadaFacturacion = false;
        bool bClienteNuevo = false; 

        public FrmClientes()
        {
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas_CFDI(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Ayuda = new clsAyudas_CFDI(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");

            lstEmails = new clsListView(listvEmails);
            lstTels = new clsListView(listvTelefonos);  
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);

            if (bLlamadaFacturacion)
            {
                if (sIdCliente != "")
                {
                    txtId.Text = sIdCliente;
                    txtId_Validating(null, null);
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    if (btnNuevo.Enabled)
                    {
                        btnNuevo_Click(null, null);
                    }
                    break;

                case Keys.F6:
                    if (btnGuardar.Enabled)
                    {
                        btnGuardar_Click(null, null);
                    }
                    break;

                case Keys.F7:
                    if (btnCancelar.Enabled)
                    {
                        btnCancelar_Click(null, null);
                    }
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        #region Propiedades
        public string Cliente
        {
            get { return sIdCliente; }
        }

        public string RFC
        {
            get { return sRFC; }
        }

        public string ClienteNombre
        {
            get { return sNombreCliente; }
        }

        public bool ClienteNuevo
        {
            get { return bClienteNuevo; }
        }
        #endregion Propiedades

        #region Limpiar
        private void InicializaPantalla()
        {
            lblCancelado.Visible = false; 
            Fg.IniciaControles();
            InicializaToolBar();
            InicialiarToolsBarAuxiliares(false);

            ////rdoPublicoGeneral.Checked = true; 
            txtCodigoPostal.Enabled = false; 
            lstEmails.LimpiarItems();
            lstTels.LimpiarItems();

            txtId.Focus();
        }

        private void InicializaToolBar()
        {
            InicializaToolBar(false, false);
        }

        private void InicializaToolBar(bool Guardar, bool Cancelar)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;

            if (bLlamadaFacturacion)
            {
                btnCancelar.Enabled = false;
                btnImprimir.Enabled = false;
            }
        }

        private void InicialiarToolsBarAuxiliares(bool Habilitar)
        {
            ////btnDir_Add.Enabled = Habilitar;
            ////btnDir_Edit.Enabled = Habilitar;
            ////btnDir_Delete.Enabled = Habilitar;

            btnEmail_Add.Enabled = Habilitar;
            btnEmail_Edit.Enabled = Habilitar;
            btnEmail_Delete.Enabled = Habilitar;

            btnTel_Add.Enabled = Habilitar;
            btnTel_Edit.Enabled = Habilitar;
            btnTel_Delete.Enabled = Habilitar;

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializaPantalla(); 
        }
        #endregion Limpiar

        #region Funciones y Procedimientos Publicos
        public bool MostrarClientes(string IdCliente)
        {
            bLlamadaFacturacion = true;
            sIdCliente = IdCliente;
            this.ShowDialog();

            return bClienteNuevo;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Buscar Cliente 
        private void txtId_Enter(object sender, EventArgs e)
        {
            lblCancelado.Visible = false;
        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            lblCancelado.Visible = false;

            if (txtId.Text.Trim() == "" | txtId.Text.Trim() == "*")
            {
                InicializaToolBar(true, false);
                InicialiarToolsBarAuxiliares(true); 
                txtId.Enabled = false;
                txtId.Text = "*";
                PrepararListViews(); 
            }
            else
            {
                leer.DataSetClase = Consultas.CFDI_Clientes(txtId.Text.Trim(), false, "txtId_Validating");
                if (leer.Leer())
                {
                    InicializaToolBar(true, true);
                    InicialiarToolsBarAuxiliares(true); 
                    CargarInf_Cliente(); 
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.CFDI_Clientes("txtId_KeyDown");
                if (leer.Leer())
                {
                    CargarInf_Cliente();
                }
            }

        }

        private void CargarInf_Cliente()
        {
            txtId.Enabled = false;
            txtId.Text = leer.Campo("IdCliente");
            txtNombre.Text = leer.Campo("Nombre");
            txtNombreComercial.Text = leer.Campo("NombreComercial");
            txtRFC.Text = leer.Campo("RFC");
            bCancelado = leer.Campo("Status").ToUpper() == "C";

            ////rdoPublicoGeneral.Checked = leer.CampoInt("TipoDeCliente") == 1;
            ////rdoOrtopedia.Checked = leer.CampoInt("TipoDeCliente") == 2;
            ////rdoGobierno.Checked = leer.CampoInt("TipoDeCliente") == 3; 

            CargarInf_Cliente_01_Direcciones(); 
            CargarInf_Cliente_02_Emails();
            CargarInf_Cliente_03_Telefonos();

            if (bCancelado)
            {
                InicializaToolBar(true, false);
                InicialiarToolsBarAuxiliares(false); 
                lblCancelado.Text = "CANCELADO";
                lblCancelado.Visible = true;
            }

        }

        private void PrepararListViews()
        {
            CargarInf_Cliente_02_Emails();
            CargarInf_Cliente_03_Telefonos(); 
        }

        private void CargarInf_Cliente_01_Direcciones()
        {
            clsLeer leerDir = new clsLeer();
            leerDir.DataSetClase = Consultas.CFDI_Clientes_Direcciones(txtId.Text, "CargarInf_Paciente_01_Direcciones");
            if (leerDir.Leer())
            {

                //IdDireccion, IdEstado, Estado, IdMunicipio, Municipio, IdColonia, Colonia, " +
                //" 'Dirección' = Direccion, 'Codigo Postal' = CodigoPostal, 'Status' 

                txtD_Pais.Text = leerDir.Campo("Pais"); 
                txtIdEstado.Text = leerDir.Campo("IdEstado");
                lblEstado.Text = leerDir.Campo("Estado"); 
                txtIdMunicipio.Text = leerDir.Campo("IdMunicipio");
                lblMunicipio.Text = leerDir.Campo("Municipio");
                txtIdColonia.Text = leerDir.Campo("IdColonia");
                lblColonia.Text = leerDir.Campo("Colonia");
                txtCalle.Text = leerDir.Campo("Calle");
                txtD_NoExterior.Text = leerDir.Campo("NumeroExterior");
                txtD_NoInterior.Text = leerDir.Campo("NumeroInterior");
                txtCodigoPostal.Text = leerDir.Campo("CodigoPostal");  
            }
        }

        private void CargarInf_Cliente_02_Emails()
        {
            lstEmails.LimpiarItems();
            leer.DataSetClase = Consultas.CFDI_Clientes_Emails(txtId.Text, "CargarInf_Cliente_02_Emails");
            lstEmails.CargarDatos(leer.DataSetClase, true, true);

            lstEmails.AnchoColumna((int)ColsEmails.IdEmail, 0);
            lstEmails.AnchoColumna((int)ColsEmails.IdTipoEMail, 0);
        }

        private void CargarInf_Cliente_03_Telefonos()
        {
            lstTels.LimpiarItems();
            leer.DataSetClase = Consultas.CFDI_Clientes_Telefonos(txtId.Text, "CargarInf_Cliente_03_Telefonos");
            lstTels.CargarDatos(leer.DataSetClase, true, true);

            lstTels.AnchoColumna((int)ColsTelefonos.IdTelefono, 0);
            lstTels.AnchoColumna((int)ColsTelefonos.IdTipoTelefono, 0);
        }
        #endregion Buscar Cliente 

        #region Guardar/Actualizar Cliente 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                GuardarInformacion(1);

                if (bLlamadaFacturacion)
                {
                    this.Close();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                GuardarInformacion(2);
            }
        }
        #endregion Guardar/Actualizar Cliente

        #region Funciones y Procedimientos de Guardado
        private bool validarDatos()
        {
            bool bRegresa = true;
            int iTipoCliente = 0; //// Convert.ToInt32(rdoPublicoGeneral.Checked) + Convert.ToInt32(rdoOrtopedia.Checked) + Convert.ToInt32(rdoGobierno.Checked); 

            if ( bRegresa && txtId.Text.Trim() == "" )
            {
                bRegresa = false;
                General.msjUser("Clave de Cliente inválida, verifique.");
                txtId.Focus();
            }

            if ( bRegresa && txtNombre.Text.Trim() == "" )
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Nombre de Cliente, verifique."); 
                txtNombre.Focus(); 
            }

            if ( bRegresa && txtRFC.Text.Trim()== "" )
            {
                bRegresa = false;
                General.msjUser("No ha capturado un RFC válido, verifique.");
                txtRFC.Focus(); 
            }

            if (bRegresa)
            {
                if (!DtIFacturacion.RFC_Valido(txtRFC.Text))
                {
                    bRegresa = false;
                    General.msjError("Formato de RFC inválido, vefirique.");
                }
            }

            //////if ( bRegresa && iTipoCliente == 0 )
            //////{
            //////    bRegresa = false;
            //////    General.msjUser("No ha seleccionado el Tipo de Cliente, verifique."); 
            //////    rdoPublicoGeneral.Focus();
            //////}

            if (bRegresa && txtD_Pais.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El País no debe ser vacio, verifique.");
                txtD_Pais.Focus(); 
            }

            if (bRegresa && txtIdEstado.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El Estado no debe ser vacio, verifique."); 
                txtIdEstado.Focus(); 
            }

            if (bRegresa && txtIdMunicipio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El Municipio no debe ser vacio, verifique.");
                txtIdMunicipio.Focus();
            }

            if (bRegresa && txtIdColonia.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("La Colonia no debe ser vacia, verifique.");
                txtIdColonia.Focus();
            }

            if (bRegresa && txtCalle.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("La Calle no debe ser vacia, verifique.");
                txtCalle.Focus();
            }

            if (bRegresa && txtD_NoExterior.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El Número exterior no debe ser vacio, verifique.");
                txtD_NoExterior.Focus();
            }

            return bRegresa;
        }
        private bool GuardarInformacion(int Opcion)
        {
            bool bRegresa = true;

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion(); 
            }
            else
            {
                cnn.IniciarTransaccion();

                bRegresa = GuardarCliente(Opcion); 

                if (!bRegresa)
                {
                    Error.GrabarError(leer, "GuardarInformacion()"); 
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al guardar la información del cliente.");
                }
                else 
                {
                    txtId.Text = sIdentificador; 
                    cnn.CompletarTransaccion();
                    General.msjUser(sMensaje);
                    InicializaPantalla(); 
                }

                cnn.Cerrar(); 
            }

            return bRegresa; 
        }

        private bool GuardarCliente(int Opcion)
        {
            bool bRegresa = true;
            int iOpcion = Opcion;
            int iTipoDeCliente = 1;
            string sSql = ""; 

            ////////if (rdoOrtopedia.Checked)
            ////////{
            ////////    iTipoDeCliente = 2; 
            ////////}

            ////////if (rdoGobierno.Checked)
            ////////{
            ////////    iTipoDeCliente = 3;
            ////////}

            sSql = string.Format("Exec spp_Mtto_CFDI_Clientes @IdCliente = '{0}', @Nombre = '{1}', @NombreComercial = '{2}', @RFC = '{3}', " +
                " @TipoDeCliente = '{4}', @iOpcion = '{5}' ",
                txtId.Text.Trim(), txtNombre.Text.Trim(), txtNombreComercial.Text.Trim(), DtIFacturacion.RFC_Formato(txtRFC.Text), iTipoDeCliente, iOpcion);

            if (!leer.Exec(sSql))
            {
                bRegresa = false; 
            }
            else
            {
                if (!leer.Leer())
                {
                    bRegresa = false; 
                }
                else
                {
                    sIdentificador = leer.Campo("IdCliente");
                    sMensaje = leer.Campo("Mensaje"); 
                }
            }

            if (bRegresa)
            {
                bRegresa = Guardar_02_Direcciones(Opcion); 
            }

            return bRegresa;
        }

        private bool Guardar_02_Direcciones(int Opcion) 
        {
            bool bRegresa = true;
            string sSql = "";
            int iOpcion = 0; 

            sIdDireccion = "";
            sIdEstado = "";
            sEstado = "";
            sIdMunipicio = "";
            sMunipicio = "";
            sIdColonia = "";
            sColonia = "";
            sDireccion = "";
            sCodigoPostal = "";
            sStatus = "";
            

            ////for (int i = 1; i <= lstDirs.Registros; i++)
            {                
                ////sIdDireccion = lstDirs.GetValue(i, (int)ColsDireccion.IdDireccion);
                ////sIdEstado = lstDirs.GetValue(i, (int)ColsDireccion.IdEstado);
                ////sEstado = lstDirs.GetValue(i, (int)ColsDireccion.Estado);
                ////sIdMunipicio = lstDirs.GetValue(i, (int)ColsDireccion.IdMunicipio);
                ////sMunipicio = lstDirs.GetValue(i, (int)ColsDireccion.Municipio);
                ////sIdColonia = lstDirs.GetValue(i, (int)ColsDireccion.IdColonia);
                ////sColonia = lstDirs.GetValue(i, (int)ColsDireccion.Colonia);
                ////sDireccion = lstDirs.GetValue(i, (int)ColsDireccion.Direccion);
                ////sCodigoPostal = lstDirs.GetValue(i, (int)ColsDireccion.CodigoPostal);
                ////sStatus = Fg.Mid(lstDirs.GetValue(i, (int)ColsDireccion.Status), 1, 1);
                ////iOpcion = sStatus == "A" ? 1 : 0;

                sIdDireccion = "01" ;
                sIdEstado = txtIdEstado.Text.Trim();
                sIdMunipicio = txtIdMunicipio.Text.Trim();
                sIdColonia = txtIdColonia.Text.Trim();
                sDireccion = txtCalle.Text.Trim();
                sCodigoPostal = txtCodigoPostal.Text.Trim();
                iOpcion = 1;
                iOpcion = Opcion == 2 ? 0 : iOpcion;

                sSql = string.Format("Exec spp_Mtto_CFDI_Clientes_Direcciones " +
                    " @IdCliente = '{0}', @IdDireccion = '{1}', @Pais = '{2}', @IdEstado = '{3}', @IdMunicipio = '{4}', @IdColonia = '{5}', " + 
                    " @Calle = '{6}', @NumeroExterior = '{7}', @NumeroInterior = '{8}', @CodigoPostal = '{9}', " + 
                    " @Referencia = '{10}', @iOpcion = '{11}' ",
                    sIdentificador, sIdDireccion, txtD_Pais.Text.Trim(), 
                    sIdEstado, sIdMunipicio, sIdColonia, sDireccion, txtD_NoExterior.Text.Trim(), txtD_NoInterior.Text.Trim(), 
                    sCodigoPostal, "", iOpcion); 

                if (!leer.Exec(sSql))
                {
                    bRegresa = false; 
                    //break;
                }
            }

            if (bRegresa)
            {
                bRegresa = Guardar_03_Emails(Opcion);
            }

            return bRegresa;
        }

        private bool Guardar_03_Emails(int Opcion) 
        {
            bool bRegresa = true;
            string sSql = "";
            int iOpcion = 0;

            sIdEmail = "";
            sIdTipoEMail = "";
            sTipoMail = "";
            sEmail = ""; 
            sStatusEmail = "";

            for (int i = 1; i <= lstEmails.Registros; i++)
            {
                sIdEmail = lstEmails.GetValue(i, (int)ColsEmails.IdEmail);
                sIdTipoEMail = lstEmails.GetValue(i, (int)ColsEmails.IdTipoEMail);
                sTipoMail = lstEmails.GetValue(i, (int)ColsEmails.TipoMail);
                sEmail = lstEmails.GetValue(i, (int)ColsEmails.Email);
                sStatusEmail = Fg.Mid(lstEmails.GetValue(i, (int)ColsEmails.Status), 1, 1);
                iOpcion = sStatusEmail == "A" ? 1 : 0;
                iOpcion = Opcion == 2 ? 0 : iOpcion;

                sSql = string.Format("Exec spp_Mtto_CFDI_Clientes_EMails " +
                    " @IdCliente = '{0}', @IdEmail = '{1}', @IdTipoEMail = '{2}', @Email = '{3}', @iOpcion = '{4}' ",
                    sIdentificador, sIdEmail, sIdTipoEMail, sEmail, iOpcion);

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
            }

            if (bRegresa)
            {
                bRegresa = Guardar_04_Telefonos(Opcion);
            }

            return bRegresa;
        }

        private bool Guardar_04_Telefonos(int Opcion) 
        {
            bool bRegresa = true;
            string sSql = "";
            int iOpcion = 0;

            sIdTelefono = "";
            sIdTipoTelefono = "";
            sTipoTelefono = "";
            sTelefono = "";
            sStatusTelefono = "";

            for (int i = 1; i <= lstTels.Registros; i++)
            {
                sIdTelefono = lstTels.GetValue(i, (int)ColsTelefonos.IdTelefono);
                sIdTipoTelefono = lstTels.GetValue(i, (int)ColsTelefonos.IdTipoTelefono);
                sTipoTelefono = lstTels.GetValue(i, (int)ColsTelefonos.TipoTelefono);
                sTelefono = lstTels.GetValue(i, (int)ColsTelefonos.Telefono);
                sStatusTelefono = Fg.Mid(lstTels.GetValue(i, (int)ColsTelefonos.Status), 1, 1);
                iOpcion = sStatusTelefono == "A" ? 1 : 0;
                iOpcion = Opcion == 2 ? 0 : iOpcion;

                sSql = string.Format("Exec spp_Mtto_CFDI_Clientes_Telefonos " +
                    " @IdCliente = '{0}', @IdTelefono = '{1}', @IdTipoTelefono = '{2}', @Telefono = '{3}', @iOpcion = '{4}' ",
                    sIdentificador, sIdTelefono, sIdTipoTelefono, sTelefono, iOpcion);

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos de Guardado 
       

        #region Validaciones de Controles 
        //////private bool ValidaDatos()
        //////{
        //////    bool bRegresa = true;

        //////    if (txtId.Text == "")
        //////    {
        //////        General.msjUser("Ingrese la Clave Cliente por favor");
        //////        txtId.Focus();
        //////        bRegresa = false;
        //////    }

        //////    if (bRegresa && txtNombre.Text == "")
        //////    {
        //////        bRegresa = false;
        //////        General.msjUser("Ingrese el Nombre por favor");
        //////        txtNombre.Focus();
        //////    }

        //////    if (bRegresa && txtRFC.Text == "")
        //////    {
        //////        bRegresa = false;
        //////        General.msjUser("Ingrese el RFC por favor");
        //////        txtRFC.Focus();                
        //////    }

        //////    //if (bRegresa && cboEstados.Data == "0")
        //////    //{
        //////    //    bRegresa = false;
        //////    //    General.msjUser("Seleccione un Estado por favor");
        //////    //    cboEstados.Focus();
        //////    //}

        //////    //if (bRegresa && cboMunicipios.Data == "0")
        //////    //{
        //////    //    bRegresa = false;
        //////    //    General.msjUser("Seleccione un Municipio por favor");
        //////    //    cboMunicipios.Focus();;
        //////    //}

        //////    //if (bRegresa && cboColonia.Data == "0")
        //////    //{
        //////    //    bRegresa = false;
        //////    //    General.msjUser("Seleccione una Colonia por favor");
        //////    //    cboColonia.Focus();
        //////    //}

        //////    if (bRegresa && txtCalle.Text == "")
        //////    {
        //////        bRegresa = false;
        //////        General.msjUser("Ingrese el Domicilio por favor");
        //////        txtCalle.Focus();
        //////    }

        //////    if (bRegresa && txtCodigoPostal.Text == "")
        //////    {
        //////        bRegresa = false;
        //////        General.msjUser("Ingrese el Codigo Postal por favor");
        //////        txtCodigoPostal.Focus();
        //////    }

        //////    ////if (bRegresa && txtTelefonos.Text == "")
        //////    ////{
        //////    ////    bRegresa = false;
        //////    ////    General.msjUser("Ingrese el Telefono por favor");
        //////    ////    txtTelefonos.Focus();
        //////    ////}

        //////    ////if (bRegresa && txtEMail.Text == "")
        //////    ////{
        //////    ////    bRegresa = false;
        //////    ////    General.msjUser("Ingrese el Correo Electrónico por favor");
        //////    ////    txtEMail.Focus();
        //////    ////}
            
        //////    return bRegresa;
        //////}

        #endregion Validaciones de Controles

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DatosCliente.Funcion = "btnImprimir_Click()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false; 

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "Central_ListadoDeClientes";

            bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            } 
        }

        #region Geograficos
        private void CargarInfEstado()
        {
            txtIdEstado.Text = leer.Campo("IdEstado");
            lblEstado.Text = leer.Campo("Descripcion");
        }

        private void CargarInfMunicipio()
        {
            txtIdMunicipio.Text = leer.Campo("IdMunicipio");
            lblMunicipio.Text = leer.Campo("Descripcion");
        }

        private void CargarInfColonia()
        {
            txtIdColonia.Text = leer.Campo("IdColonia");
            lblColonia.Text = leer.Campo("Descripcion");
            txtCodigoPostal.Text = leer.Campo("CodigoPostal"); 
        }

        private void txtIdEstado_TextChanged(object sender, EventArgs e)
        {
            lblEstado.Text = "";
            txtIdMunicipio.Text = "";
            lblMunicipio.Text = "";
            lblColonia.Text = "";
        }

        private void txtIdEstado_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdEstado.Text != "")
            {
                leer.DataSetClase = Consultas.CFDI_Estados(txtIdEstado.Text, "txtIdEstado_KeyDown");
                if (!leer.Leer())
                {
                    txtIdEstado.Text = ""; 
                    txtIdEstado.Focus();
                }
                else
                {
                    CargarInfEstado();
                }
            }
        }

        private void txtIdEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.CFDI_Estados("txtIdEstado_KeyDown");
                if (leer.Leer())
                {
                    CargarInfEstado();
                }
            }
        }

        private void txtIdMunicipio_TextChanged(object sender, EventArgs e)
        {
            lblMunicipio.Text = "";
            txtIdColonia.Text = "";
            lblColonia.Text = "";
        }

        private void txtIdMunicipio_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdEstado.Text.Trim() != "" && txtIdMunicipio.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.CFDI_Municipios(txtIdEstado.Text, txtIdMunicipio.Text, "txtIdMunicipio_Validating");
                if (!leer.Leer())
                {
                    txtIdMunicipio.Text = ""; 
                    txtIdMunicipio.Focus();
                }
                else
                {
                    CargarInfMunicipio();
                }
            }
        }

        private void txtIdMunicipio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtIdEstado.Text != "")
                {
                    leer.DataSetClase = Ayuda.CFDI_Municipios(txtIdEstado.Text, "txtIdEstado_KeyDown");
                    if (leer.Leer())
                    {
                        CargarInfMunicipio();
                    }
                }
            }
        }

        private void txtIdColonia_TextChanged(object sender, EventArgs e)
        {
            lblColonia.Text = "";
            txtCodigoPostal.Text = ""; 
        }

        private void txtIdColonia_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdMunicipio.Text.Trim() != "" && txtIdColonia.Text != "")
            {
                leer.DataSetClase = Consultas.CFDI_Colonias(txtIdEstado.Text, txtIdMunicipio.Text, txtIdColonia.Text, "txtIdColonia_Validating");
                if (!leer.Leer())
                {
                    txtIdColonia.Text = ""; 
                    txtIdColonia.Focus();
                }
                else
                {
                    CargarInfColonia();
                }
            }
        }

        private void txtIdColonia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtIdMunicipio.Text != "")
                {
                    leer.DataSetClase = Ayuda.CFDI_Colonias(txtIdEstado.Text, txtIdMunicipio.Text, "txtIdEstado_KeyDown");
                    if (leer.Leer())
                    {
                        CargarInfColonia();
                    }
                }
            }
        }
        #endregion Geograficos 

        #region Emails
        private void btnEmail_Add_Click(object sender, EventArgs e)
        {
            MostrarEMails(1);
        }

        private void btnEmail_Edit_Click(object sender, EventArgs e)
        {
            if (EMailSeleccionado())
            {
                MostrarEMails(2);
            }
        }

        private void btnEmail_Delete_Click(object sender, EventArgs e)
        {
            if (EMailSeleccionado())
            {
                lstEmails.SetValue((int)ColsEmails.Status, "CANCELADO");
            }
        }

        private bool EMailSeleccionado()
        {
            bool bRegresa = lstEmails.Registros > 0;

            if (lstEmails.Registros > 0)
            {
                bRegresa = lstEmails.GetValue(lstEmails.RenglonActivo, (int)ColsEmails.IdEmail) != "";
            }

            return bRegresa;
        }

        private void MostrarEMails(int TipoProceso)
        {
            //IdEmail = 1, IdTipoEMail = 2, TipoMail = 3, Email = 4, Status = 5 

            int iRow = lstEmails.RenglonActivo;
            sIdEmail = lstEmails.GetValue(iRow, (int)ColsEmails.IdEmail);
            sIdTipoEMail = lstEmails.GetValue(iRow, (int)ColsEmails.IdTipoEMail);
            sTipoMail = lstEmails.GetValue(iRow, (int)ColsEmails.TipoMail);
            sEmail = lstEmails.GetValue(iRow, (int)ColsEmails.Email);
            sStatusEmail = Fg.Mid(lstEmails.GetValue(iRow, (int)ColsEmails.Status), 1, 1);

            FrmEmail f = new FrmEmail();
            if (f.MostrarInterface(TipoProceso, sIdTipoEMail, sTipoMail, sEmail, sStatusEmail))
            {
                if (TipoProceso == 1)
                {
                    lstEmails.AddRow();
                    iRow = lstEmails.Registros;

                    lstEmails.SetValue(iRow, (int)ColsEmails.IdEmail, "*");
                    lstEmails.SetValue(iRow, (int)ColsEmails.IdTipoEMail, f.sIdTipoMail);
                    lstEmails.SetValue(iRow, (int)ColsEmails.TipoMail, f.sTipoMail);
                    lstEmails.SetValue(iRow, (int)ColsEmails.Email, f.sEMails);
                    lstEmails.SetValue(iRow, (int)ColsEmails.Status, f.sStatus);
                }

                if (TipoProceso == 2)
                {
                    //lstlstEmailsDirs.SetValue(iRow, (int)ColsEmails.IdEmail, ""); 
                    lstEmails.SetValue(iRow, (int)ColsEmails.IdTipoEMail, f.sIdTipoMail);
                    lstEmails.SetValue(iRow, (int)ColsEmails.TipoMail, f.sTipoMail);
                    lstEmails.SetValue(iRow, (int)ColsEmails.Email, f.sEMails);
                    lstEmails.SetValue(iRow, (int)ColsEmails.Status, f.sStatus);
                }
            }
        }
        #endregion Emails

        #region Telefonos
        private void btnTel_Add_Click(object sender, EventArgs e)
        {
            MostrarTelefonos(1);
        }

        private void btnTel_Edit_Click(object sender, EventArgs e)
        {
            if (TelefonoSeleccionado())
            {
                MostrarTelefonos(2);
            }
        }

        private void btnTel_Delete_Click(object sender, EventArgs e)
        {
            if (EMailSeleccionado())
            {
                lstTels.SetValue((int)ColsTelefonos.Status, "CANCELADO");
            }
        }

        private bool TelefonoSeleccionado()
        {
            bool bRegresa = lstTels.Registros > 0;

            if (lstTels.Registros > 0)
            {
                bRegresa = lstTels.GetValue(lstTels.RenglonActivo, (int)ColsTelefonos.IdTelefono) != "";
            }

            return bRegresa;
        }

        private void MostrarTelefonos(int TipoProceso)
        {
            //IdEmail = 1, IdTipoEMail = 2, TipoMail = 3, Email = 4, Status = 5 

            int iRow = lstTels.RenglonActivo;
            sIdTelefono = lstTels.GetValue(iRow, (int)ColsTelefonos.IdTelefono);
            sIdTipoTelefono = lstTels.GetValue(iRow, (int)ColsTelefonos.IdTipoTelefono);
            sTipoTelefono = lstTels.GetValue(iRow, (int)ColsTelefonos.TipoTelefono);
            sTelefono = lstTels.GetValue(iRow, (int)ColsTelefonos.Telefono);
            sStatusTelefono = Fg.Mid(lstTels.GetValue(iRow, (int)ColsTelefonos.Status), 1, 1);

            FrmTelefono f = new FrmTelefono();
            if (f.MostrarInterface(TipoProceso, sIdTipoTelefono, sTipoTelefono, sTelefono, sStatusTelefono))
            {
                if (TipoProceso == 1)
                {
                    lstTels.AddRow();
                    iRow = lstTels.Registros;

                    lstTels.SetValue(iRow, (int)ColsTelefonos.IdTelefono, "*");
                    lstTels.SetValue(iRow, (int)ColsTelefonos.IdTipoTelefono, f.sIdTipoTelefono);
                    lstTels.SetValue(iRow, (int)ColsTelefonos.TipoTelefono, f.sTipoTelefono);
                    lstTels.SetValue(iRow, (int)ColsTelefonos.Telefono, f.sTelefono);
                    lstTels.SetValue(iRow, (int)ColsTelefonos.Status, f.sStatus);
                }

                if (TipoProceso == 2)
                {
                    //lstTels.SetValue(iRow, (int)ColsTelefonos.IdTelefono, "*");
                    lstTels.SetValue(iRow, (int)ColsTelefonos.IdTipoTelefono, f.sIdTipoTelefono);
                    lstTels.SetValue(iRow, (int)ColsTelefonos.TipoTelefono, f.sTipoTelefono);
                    lstTels.SetValue(iRow, (int)ColsTelefonos.Telefono, f.sTelefono);
                    lstTels.SetValue(iRow, (int)ColsTelefonos.Status, f.sStatus);
                }
            }
        }
        #endregion Telefonos

        #region Botones auxiliares
        private void btnEstados_Click(object sender, EventArgs e)
        {
            FrmEstados f = new FrmEstados();
            f.ShowDialog();
        }

        private void btnMunicipios_Click(object sender, EventArgs e)
        {
            FrmMunicipios f = new FrmMunicipios();
            f.ShowDialog();
        }

        private void btnColonias_Click(object sender, EventArgs e)
        {
            FrmColonias f = new FrmColonias();
            f.ShowDialog();
        }
        #endregion Botones auxiliares

    } //Llaves de la clase
}
