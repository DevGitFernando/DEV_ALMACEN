using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales; 

//using Dll_IMach4; 

namespace DllPedidosClientes.Usuarios_y_Permisos
{
    internal partial class FrmEdoLogin : FrmBaseExt 
    {
        public bool bUsuarioLogeado = false;
        public string sEmpresa = "";
        public string sEstado = "";
        public string sSucursal = "";
        public string sQuery = "";
        public string sTabla = "";
        public string sArbol = "";
        public DataSet dtsPermisos;

        private DataSet dtsEstados;
        // private DataSet dtsFarmacias;
        private bool bCargando = false;

        //basGenerales Fg = new basGenerales();
        DllPedidosClientes.Usuarios_y_Permisos.clsEdoLogin LoginUser = new DllPedidosClientes.Usuarios_y_Permisos.clsEdoLogin();
        //wsConexion.wsConexionDB wsDb = new wsConexion.wsConexionDB();
        clsCriptografo Cryp = new clsCriptografo();

        // int iAltoFormNormal = 200;
        // int iAltoFormDebug = 0;
        int iDesplazamiento = 48;
        int iDesplazamientoUp = 20;
        // int iDesAncho = 50;

        Thread thrAutenticar;
        bool bAutenticando = false;
        bool bAutentificacionCancelada = false; 

        clsAuditoria auditoria;
        public string sSesion = "*";

        clsLeerWeb Query; // = new clsLeerWeb(General.Url, General.DatosConexion, new SC_SolutionsSystem.Data.clsDatosCliente("DllFarmaciaSoft", this.Name, "FrmEdoLogin_Load"));
        clsDatosCliente datosCliente; 

        public FrmEdoLogin()
        {
            InitializeComponent();
        }

        private void FrmEdoLogin_Load(object sender, EventArgs e)
        {
            ////clsLeerWeb Query; // = new clsLeerWeb(General.Url, General.DatosConexion, new SC_SolutionsSystem.Data.clsDatosCliente("DllFarmaciaSoft", this.Name, "FrmEdoLogin_Load"));
            ////clsDatosCliente datosCliente;            

            string sIdSucursal = ""; //General.XmlConfig.GetValues("IdSucursal").Trim(); 
            string sIdEstado = "";
            // string sIdEmpresa = "";
            // string sSql = "";
            Fg.IniciaControles(this, true);
            lblAutenticando.Visible = false; // 2K110718.1500 Jesus Diaz 


            DllPedidosClientes.Usuarios_y_Permisos.FrmConect Conect = new DllPedidosClientes.Usuarios_y_Permisos.FrmConect();
            Fg.CentrarForma(Conect);
            Conect.ShowDialog();

            bCargando = true;
            if (Conect.bExisteFileConfig)
            {
                if (Conect.bConexionEstablecida)
                {
                    datosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, ""); 
                    Query = new clsLeerWeb(General.Url, General.ArchivoIni, datosCliente); 

                    //LoginUser.CargarUsuarios();
                    DataSet dtsListaSucursales = new DataSet();

                    txtUsuario.Text = DtGeneralPedidos.XmlEdoConfig.GetValues("UltimoUsuarioConectado").ToUpper();
                    sIdEstado = DtGeneralPedidos.XmlEdoConfig.GetValues("IdEstado").Trim();
                    sIdSucursal = DtGeneralPedidos.XmlEdoConfig.GetValues("IdSucursal").Trim();
                    

                    if (General.MultiplesEntidades)
                    {
                        dtsEstados = Conect.Estados; 
                        cboEstados.Add("0", "<< Seleccione >>");
                        cboEstados.Add(dtsEstados, true);
                        cboEstados.SelectedIndex = 0; 

                        cboEstados.Data = sIdEstado; 
                    }
                    else
                    {
                        ////cboSucursales.Add("0", "<< Seleccione >>");
                        ////cboSucursales.Add(General.EntidadConectada, "Entidad unica");
                        ////cboSucursales.SelectedIndex = 1;
                    }

                    cboEstados.Enabled = false; 
                    if (sIdEstado != "")
                    {
                        cboEstados.Data = sIdEstado;
                        cboEstados_SelectedIndexChanged(null, null);
                    }

                    if (txtUsuario.Text.Trim() != "")
                    {
                        txtPassword.Focus();
                        this.ActiveControl = txtPassword;
                    }

                    //AjustaPantalla();
                    //AjustaPantallaDebug();

                    AjustarPantallaEdo();
                }
                else
                {
                    //Application.Exit();
                    this.Close();
                }
            }
            else
            {
                //Application.Exit();
                this.Close();
            }
            bCargando = false;
        } 

        private void AjustarPantallaEdo()
        {
            ////if (DtGeneral.ConexionOficinaCentral)
            ////{
            ////    ////lblEstado.Visible = false;
            ////    ////cboEstados.Visible = false;
            ////    ////lblEmpresa.Visible = false;
            ////    ////cboSucursales.Visible = false;

            ////    ////btnAceptar.Top = txtPassword.Top - iDesplazamientoUp;
            ////    ////btnCancelar.Top = txtPassword.Top - iDesplazamientoUp;

            ////    ////lblUsuario.Top = lblEstado.Top;
            ////    ////txtUsuario.Top = lblEstado.Top;

            ////    ////lblPassword.Top = lblEmpresa.Top;
            ////    ////txtPassword.Top = lblEmpresa.Top;

            ////    ////iDesAncho = 85;
            ////    ////btnAceptar.Left = btnAceptar.Left - iDesAncho;
            ////    ////btnCancelar.Left = btnCancelar.Left - iDesAncho;

            ////    ////this.Width = this.Width - iDesAncho;
            ////    ////this.Height = btnAceptar.Top + btnAceptar.Height + (int)(iDesplazamientoUp * 2.25);
            ////    ////FrameLogin.Width = FrameLogin.Width - iDesAncho;
            ////    ////FrameLogin.Height = this.Height - (int)(iDesplazamientoUp * 1.7);
            ////}
        }

        private void AjustaPantalla()
        {
            if (!General.MultiplesEntidades)
            {
                //lblEmpresa.Top = 0;
                //lblEmpresa.Visible = false;

                //cboSucursales.Top = 0;
                //cboSucursales.Visible = false;

                lblUsuario.Top = lblUsuario.Top - iDesplazamientoUp;
                txtUsuario.Top = txtUsuario.Top - iDesplazamientoUp;

                lblPassword.Top = lblPassword.Top - iDesplazamientoUp;
                txtPassword.Top = txtPassword.Top - iDesplazamientoUp;

                btnAceptar.Top = btnAceptar.Top - iDesplazamientoUp;
                btnCancelar.Top = btnCancelar.Top - iDesplazamientoUp;
                this.Height = this.Height - iDesplazamientoUp;
                FrameLogin.Height = this.Height - (int)(iDesplazamientoUp * 1.5 );
                this.Refresh();
            }
        }

        private void AjustaPantallaDebug()
        {
            if (General.bModoDesarrollo)
            {
                ////lblEmpresa.Visible = true;
                ////lblEmpresa.Top = 23;
                //cboSucursales.Visible = true;
                //cboSucursales.Top = 23;
                // cboSucursales.MostrarComboItem = true;

                if (General.MultiplesEntidades)
                {
                    iDesplazamiento = 22;
                }

                lblPassword.Top = lblPassword.Top + iDesplazamiento;
                txtPassword.Top = txtPassword.Top + iDesplazamiento;

                lblUsuario.Top = lblUsuario.Top + iDesplazamiento;
                txtUsuario.Top = txtUsuario.Top + iDesplazamiento;

                btnAceptar.Top = btnAceptar.Top + iDesplazamiento;
                btnCancelar.Top = btnCancelar.Top + iDesplazamiento;
                this.Height = this.Height + iDesplazamiento;
                FrameLogin.Height = this.Height - (int)(iDesplazamientoUp * 1.5);
                this.Refresh();

            }
        }

        private bool validarUsuario()
        {
            bool bRegresa = true;

            if (txtUsuario.Text.Trim().ToUpper() != "Administrador".ToUpper())
            {
                if (bRegresa && cboEstados.SelectedIndex == 0)
                {
                    bRegresa = false;
                    General.msjUser("No ha seleccionado un Estado, verifique.");
                    cboEstados.Focus();
                }

                ////if (bRegresa && cboSucursales.SelectedIndex == 0)
                ////{
                ////    bRegresa = false;
                ////    General.msjUser("No ha seleccionado una farmacia, verifique.");
                ////    cboSucursales.Focus();
                ////}


                if (bRegresa && txtUsuario.Text.Trim() == "" )
                {
                    bRegresa = false;
                    General.msjUser("No ha capturado un usuario para el sistema.");
                    txtUsuario.Focus();
                }

                if (bRegresa && txtPassword.Text.Trim() == "")
                {
                    bRegresa = false;
                    General.msjUser("No ha especificado el password de usuario.");
                    txtPassword.Focus();
                }

            }
            else
            {
                // CFGN
                //foreach (string sMod in DtGeneral.ArbolesConfiguracion)
                {
                    if (sArbol.ToUpper().Trim() != "CFGN")
                    {
                        if (!General.bModoDesarrollo)
                        {
                            bRegresa = false;
                            General.msjError("El usuario Administrador sólo tiene permisos en el módulo de configuración.");
                        }
                    }
                    //else
                    //{
                    //    break;
                    //}
                }
            }

            return bRegresa;
        }

        private void HabilitarLogin(bool Enable)
        {
            //cboEstados.Enabled = Enable;
            txtUsuario.Enabled = Enable;
            txtPassword.Enabled = Enable;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            btnAceptar.Enabled = false;
            lblAutenticando.Visible = true; // 2K110718.1500 Jesus Diaz 
            bAutenticando = true;
            bAutentificacionCancelada = false;
            HabilitarLogin(false);
            this.Refresh();

            // Retrasae la ejecucion medio seg 
            Thread.Sleep(500);

            thrAutenticar = new Thread(this.AutenticarUsuario);
            thrAutenticar.Name = "AutenticarUsuario";
            thrAutenticar.Start();

            // this.AutenticarUsuario(); 
        }

        private void AutenticarUsuario()
        {
            try
            {
                if (!validarUsuario())
                {
                    lblAutenticando.Visible = false; // 2K110718.1500 Jesus Diaz 
                    bAutenticando = false;
                    btnAceptar.Enabled = true;
                    HabilitarLogin(true);
                }
                else
                {
                    LoginUser = new clsEdoLogin(); 
                    LoginUser.Estado = cboEstados.Data;
                    // LoginUser.Sucursal = cboSucursales.Data;
                    sSucursal = LoginUser.Sucursal;
                    sEstado = cboEstados.Data;
                    LoginUser.Usuario = txtUsuario.Text;
                    LoginUser.Password = txtPassword.Text;
                    LoginUser.Arbol = sArbol;
                    LoginUser.Permisos = dtsPermisos;

                    DtGeneralPedidos.EstadoConectado = cboEstados.Text;
                    DtGeneralPedidos.ClaveRENAPO = "OC";
                    try
                    {
                        DtGeneralPedidos.ClaveRENAPO = ((DataRow)cboEstados.ItemActual.Item)["ClaveRenapo"].ToString();
                    }
                    catch { }
                    // DtGeneralPedidos.FarmaciaConectadaNombre = cboSucursales.Text;                

                    if (LoginUser.AutenticarUsuarioLogin())
                    { 
                        dtsPermisos = LoginUser.Permisos;
                        bUsuarioLogeado = true;
                        CargarDatosDeUsuarioConectado();

                        btnCancelar.Enabled = false;
                        dtsPermisos = LoginUser.Permisos;
                        lblAutenticando.Visible = false; // 2K110718.1500 Jesus Diaz 


                        ////General.FechaSistemaObtener();

                        ////// Crear la instacia para el objeto de la clase de Auditoria
                        ////auditoria = new clsAuditoria(General.DatosConexion, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada,
                        ////                             DtGeneralPedidos.IdPersonal, DtGeneralPedidos.IdSesion, General.Modulo, this.Name, General.Version);

                        auditoria = new clsAuditoria(General.Url, General.ArchivoIni, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada,
                                 DtGeneralPedidos.IdPersonal, DtGeneralPedidos.IdSesion, General.Modulo, this.Name, General.Version);

                        auditoria.GuardarAud_LoginReg();
                        DtGeneralPedidos.IdSesion = clsAuditoria.Sesion;

                        this.Hide();
                    }
                    else
                    {
                        LoginUser.Usuario = "";
                        LoginUser.Password = "";
                        txtUsuario.Text = "";
                        txtPassword.Text = "";

                        bAutenticando = false;
                        btnAceptar.Enabled = true;
                        lblAutenticando.Visible = false; // 2K110718.1500 Jesus Diaz 
                        HabilitarLogin(true); 

                        txtUsuario.Focus();
                    }
                }
            }
            catch { }
        }

        private void CargarDatosDeUsuarioConectado()
        {
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsConsultas query = new clsConsultas(General.DatosConexion, "", "", "", false);
            clsLeer leer = new clsLeer(ref cnn);
            string sSql = string.Format(" Select IdEstado, IdUsuario, Nombre, Login " +
                " From Net_Regional_Usuarios (NoLock) " + 
                " Where IdEstado = '{0}' and Login = '{1}'  ", cboEstados.Data, txtUsuario.Text);  

            if (!Query.Exec(sSql))
            { 
            }
            else
            {
                if (Query.Leer())
                {
                    DtGeneralPedidos.EstadoConectado = cboEstados.Data;
                    DtGeneralPedidos.EstadoConectadoNombre = cboEstados.Text;

                    DtGeneralPedidos.IdPersonal = Query.Campo("IdUsuario");
                    DtGeneralPedidos.NombrePersonal = Query.Campo("Nombre");
                    DtGeneralPedidos.LoginUsuario = Query.Campo("Login");

                    ////DtGeneral.IdPersonal = LoginUser.Personal;
                    ////DtGeneral.LoginUsuario = LoginUser.Usuario;
                    ////DtGeneral.NombrePersonal = leer.Campo("NombreCompleto");
                }
            }

            ////leer.DataSetClase = query.PersonalAdmistrador(cboEstados.Data, cboSucursales.Data, LoginUser.Personal, "CargarDatosDeUsuarioConectado()");
            ////if (leer.Leer())
            ////    DtGeneral.EsAdministrador = true;
        }

        private void GetCancelacion()
        {
            lblAutenticando.Visible = false; // 2K110718.1500 Jesus Diaz 
            bAutenticando = false;
            bAutentificacionCancelada = true;

            LoginUser.CancelarAutenticacion();
            LoginUser = null;
            thrAutenticar.Interrupt();
            thrAutenticar = null;

            btnAceptar.Enabled = true;
            HabilitarLogin(true);
            txtUsuario.Focus();

            //////// Solo cancelar la autenticacion 
            //// this.Close(); 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (bAutenticando)
            {
                try
                {
                    GetCancelacion(); 
                }
                catch (Exception ex)
                {
                    ex.Source = ex.Source;
                }
            }
            else
            {
                // Cerrar la aplicacion
                // Application.Exit();
                this.Close();
            }
        }

        private void FrmEdoLogin_KeyDown(object sender, KeyEventArgs e)
        {
            switch ((int)e.KeyValue)
            {
                case (int)Keys.Enter:
                    SendKeys.Send("{TAB}");
                    break;

                case (int)Keys.Escape:
                    SendKeys.Send("+{TAB}");
                    break;
            }
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////cboSucursales.Clear();
            ////cboSucursales.Add("0", "<< Seleccione >>");

            if (!bCargando)
            {
                try
                {
                    ////if (dtsFarmacias != null)
                    {
                        ////string sFiltro = string.Format("IdEstado = '{0}' ", cboEstados.Data);

                        ////if (dtsFarmacias.Tables.Count > 0)
                        ////    cboSucursales.Add(dtsFarmacias.Tables[0].Select(sFiltro), true, "IdFarmacia", "NombreFarmacia");

                        if (!bCargando)
                        {
                            txtUsuario.Text = "";
                            txtPassword.Text = "";
                        }
                    }
                }
                catch { }
            }
            ////cboSucursales.SelectedIndex = 0;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                btnAceptar_Click(null, null);       
        }

        private void FrmEdoLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Forzar la terminacion del hilo 
                thrAutenticar.Interrupt();
                thrAutenticar = null;
            }
            catch { }
        }

        ////private void cboSucursales_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    if (!bCargando)
        ////    {
        ////        if (sSucursal != cboSucursales.Data)
        ////        {
        ////            sSucursal = cboSucursales.Data;
        ////            txtUsuario.Text = "";
        ////            txtPassword.Text = "";
        ////        }
        ////    }
        ////}

        ////private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    cboEstados.Clear();
        ////    cboEstados.Add("0", "<< Seleccione >>");

        ////    try
        ////    {
        ////        if (dtsEstados != null)
        ////        {
        ////            if (dtsEstados.Tables.Count > 0)
        ////                cboEstados.Add(dtsEstados.Tables[0].Select("IdEmpresa = '" + cboEmpresas.Data + "'"), true, "IdEstado", "NombreEstado");

        ////            if (!bCargando)
        ////            {
        ////                txtUsuario.Text = "";
        ////                txtPassword.Text = "";
        ////            }
        ////        }
        ////    }
        ////    catch { }
        ////    cboEstados.SelectedIndex = 0;
        ////}
    }
}