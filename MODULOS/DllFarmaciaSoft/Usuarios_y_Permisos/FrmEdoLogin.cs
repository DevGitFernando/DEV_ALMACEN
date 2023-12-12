using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales; 

//using Dll_IMach4; 

namespace DllFarmaciaSoft.Usuarios_y_Permisos
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
        private DataSet dtsFarmacias;
        private bool bCargando = false;

        //basGenerales Fg = new basGenerales();
        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin LoginUser = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("", "");
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
        int iItemSeleccionado = 1;

        private bool bEsIntercalcionCorrecta = false;
        string sIntercalacion_Standard = "Latin1-General, case-insensitive, accent-insensitive, kanatype-insensitive, width-insensitive";
        string sIntercalacion_Servidor = ""; 

        public FrmEdoLogin():this(1) 
        { 
        }

        public FrmEdoLogin(int ItemConexion)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            iItemSeleccionado = ItemConexion; 
        }

        private void FrmEdoLogin_Shown( object sender, EventArgs e )
        {
            if(!bEsIntercalcionCorrecta)
            {
                btnAceptar.Enabled = false;
                General.msjAviso(string.Format("La intercalación detectada :\n\n{0}\n\nno es válida, no es posible iniciar sesión.", sIntercalacion_Servidor));
            }
        }

        private void FrmEdoLogin_Load(object sender, EventArgs e)
        {
            // clsLeerWeb Query; // = new clsLeerWeb(General.Url, General.DatosConexion, new SC_SolutionsSystem.Data.clsDatosCliente("DllFarmaciaSoft", this.Name, "FrmEdoLogin_Load"));
            // clsDatosCliente datosCliente; 
            string sIdSucursal = ""; //General.XmlConfig.GetValues("IdSucursal").Trim(); 
            string sIdEstado = "";
            string sIdEmpresa = "";
            // string sSql = "";
            Fg.IniciaControles(this, true);
            lblAutenticando.Visible = false; // 2K110718.1500 Jesus Diaz 

            DllFarmaciaSoft.Usuarios_y_Permisos.FrmEdoConect Conect = new DllFarmaciaSoft.Usuarios_y_Permisos.FrmEdoConect(iItemSeleccionado);
            //Fg.CentrarForma(Conect);
            Conect.ShowDialog();

            bCargando = true;
            if (Conect.bExisteFileConfig)
            {
                if (Conect.bConexionEstablecida)
                {
                    //LoginUser.CargarUsuarios();
                    DataSet dtsListaSucursales = new DataSet();

                    txtUsuario.Text = DtGeneral.XmlEdoConfig.GetValues("UltimoUsuarioConectado").ToUpper();
                    sIdEmpresa = Fg.PonCeros(DtGeneral.XmlEdoConfig.GetValues("IdEmpresa").Trim(), 3);
                    sIdEstado = Fg.PonCeros(DtGeneral.XmlEdoConfig.GetValues("IdEstado").Trim(), 2);
                    sIdSucursal = Fg.PonCeros(DtGeneral.XmlEdoConfig.GetValues("IdSucursal").Trim(), 4);

                    if (General.MultiplesEntidades)
                    {
                        cboEmpresas.Add("0", "<< Seleccione >>"); 
                        cboEstados.Add("0", "<< Seleccione >>");
                        cboSucursales.Add("0", "<< Seleccione >>");

                        cboEmpresas.Add(Conect.Empresas, true, "IdEmpresa", "Nombre");
                        cboEmpresas.SelectedIndex = 0;

                        dtsEstados = Conect.Estados; 
                        dtsFarmacias = Conect.Farmacias; 
                                                
                        cboEmpresas.SelectedIndex = 0;
                        cboEstados.SelectedIndex = 0;
                        cboSucursales.SelectedIndex = 0;
                    }
                    else
                    {
                        cboSucursales.Add("0", "<< Seleccione >>");
                        cboSucursales.Add(General.EntidadConectada, "Entidad unica");
                        cboSucursales.SelectedIndex = 1;
                    }

                    if (sIdEmpresa != "") 
                    {
                        cboEmpresas.Data = sIdEmpresa;
                        cboEmpresas_SelectedIndexChanged(null, null);
                    }

                    if (sIdEstado != "")
                    {
                        cboEstados.Data = sIdEstado;
                        cboEstados_SelectedIndexChanged(null, null);
                    }

                    if (sIdSucursal != "")
                    {
                        cboSucursales.Data = sIdSucursal; 
                    }

                    //if (General.MultiplesEntidades || General.bModoDesarrollo)
                    {
                        //cboSucursales.Focus();
                        cboEmpresas.Focus();
                        //cboEstados.Focus();
                    }

                    if (txtUsuario.Text.Trim() != "") 
                    {
                        txtPassword.Focus();
                        this.ActiveControl = txtPassword;
                    }

                    //AjustaPantalla();
                    //AjustaPantallaDebug();

                    bEsIntercalcionCorrecta = Conect.EsIntercalacion_Valida;
                    sIntercalacion_Servidor = Conect.Intercalacion_Detectada; 

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

        private void FrmEdoLogin_KeyDown( object sender, KeyEventArgs e )
        {
            switch((int)e.KeyValue)
            {
                case (int)Keys.Enter:
                    SendKeys.Send("{TAB}");
                    break;

                case (int)Keys.Escape:
                    SendKeys.Send("+{TAB}");
                    break;
            }

            if(e.Modifiers == Keys.Control)
            {
                switch(e.KeyCode)
                {
                    case Keys.F12:
                        DtGeneral.InformacionConexion();
                        break;

                    default:
                        break;
                }
            }

            //////// Especial permisos 
            if(e.Control && e.Shift && e.KeyCode == Keys.F12)
            {
                if(DtGeneral.EsEquipoDeDesarrollo)
                {
                    DllFarmaciaSoft.Usuarios_y_Permisos.FrmGruposUsuarios f = new FrmGruposUsuarios(2);
                    f.ShowInTaskbar = false;
                    f.ShowDialog();
                }
            }
        }

        private void AjustarPantallaEdo()
        {
            if (DtGeneral.ConexionOficinaCentral)
            {
                ////lblEstado.Visible = false;
                ////cboEstados.Visible = false;
                ////lblEmpresa.Visible = false;
                ////cboSucursales.Visible = false;

                ////btnAceptar.Top = txtPassword.Top - iDesplazamientoUp;
                ////btnCancelar.Top = txtPassword.Top - iDesplazamientoUp;

                ////lblUsuario.Top = lblEstado.Top;
                ////txtUsuario.Top = lblEstado.Top;

                ////lblPassword.Top = lblEmpresa.Top;
                ////txtPassword.Top = lblEmpresa.Top;

                ////iDesAncho = 85;
                ////btnAceptar.Left = btnAceptar.Left - iDesAncho;
                ////btnCancelar.Left = btnCancelar.Left - iDesAncho;

                ////this.Width = this.Width - iDesAncho;
                ////this.Height = btnAceptar.Top + btnAceptar.Height + (int)(iDesplazamientoUp * 2.25);
                ////FrameLogin.Width = FrameLogin.Width - iDesAncho;
                ////FrameLogin.Height = this.Height - (int)(iDesplazamientoUp * 1.7);
            }
        }

        private void AjustaPantalla()
        {
            if (!General.MultiplesEntidades)
            {
                lblFarmacia.Top = 0;
                lblFarmacia.Visible = false;

                cboSucursales.Top = 0;
                cboSucursales.Visible = false;

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
                lblFarmacia.Visible = true;
                lblFarmacia.Top = 23;
                cboSucursales.Visible = true;
                cboSucursales.Top = 23;
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

                if (bRegresa && cboEmpresas.SelectedIndex == 0)
                {
                    bRegresa = false;
                    General.msjUser("No ha seleccionado una Empresa, verifique.");
                    cboEmpresas.Focus();
                }

                if (bRegresa && cboEstados.SelectedIndex == 0)
                {
                    bRegresa = false;
                    General.msjUser("No ha seleccionado un Estado, verifique.");
                    cboEstados.Focus();
                }

                if (bRegresa && cboSucursales.SelectedIndex == 0)
                {
                    bRegresa = false;
                    General.msjUser("No ha seleccionado una farmacia, verifique.");
                    cboSucursales.Focus();
                }


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
                bRegresa = false; 
                //// CFGN
                foreach (string sMod in DtGeneral.ArbolesConfiguracion)
                { 
                    if (sArbol.ToUpper().Trim() == sMod)
                    {
                        bRegresa = true;
                        break;
                    }
                }

                if (!bRegresa)
                {
                    if (!General.bModoDesarrollo)
                    {
                        bRegresa = false;
                        General.msjError("El usuario Administrador sólo tiene permisos en el módulo de configuración.");
                    }
                }
            }

            return bRegresa;
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (bAutenticando)
            {
                try
                {
                    btnAceptar.Enabled = true;
                    lblAutenticando.Visible = false; // 2K110718.1500 Jesus Diaz 
                    bAutenticando = false;
                    bAutentificacionCancelada = true;

                    LoginUser.CancelarAutenticacion();
                    LoginUser = null;
                    thrAutenticar.Interrupt();
                    thrAutenticar = null;

                    ////bAutenticando = false;
                    ////bAutentificacionCancelada = true;
                    btnAceptar.Enabled = true;
                    HabilitarLogin(true);
                    txtUsuario.Focus(); 
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

        private void HabilitarLogin(bool Enable)
        {
            cboEmpresas.Enabled = Enable; 
            cboEstados.Enabled = Enable; 
            cboSucursales.Enabled = Enable;
            txtUsuario.Enabled = Enable;
            txtPassword.Enabled = Enable; 
        }

        private void AutenticarUsuario()
        {
            // btnAceptar.Enabled = false; 
            bAutenticando = true;
            bAutentificacionCancelada = false;

            // HabilitarLogin(false); 
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
                    LoginUser.Empresa = cboEmpresas.Data;
                    LoginUser.Estado = cboEstados.Data;
                    LoginUser.Sucursal = cboSucursales.Data;
                    sEmpresa = cboEmpresas.Data;
                    sSucursal = LoginUser.Sucursal;
                    sEstado = cboEstados.Data;
                    LoginUser.Usuario = txtUsuario.Text;
                    LoginUser.Password = txtPassword.Text;
                    LoginUser.Arbol = sArbol;
                    LoginUser.Permisos = dtsPermisos;

                    DtGeneral.EmpresaConectadaNombre = cboEmpresas.Text;
                    DtGeneral.EstadoConectadoNombre = cboEstados.Text; 
                    DtGeneral.ClaveRENAPO = "OC";
                    //////try
                    //////{
                    //////    DtGeneral.ClaveRENAPO = ((DataRow)cboEstados.ItemActual.Item)["ClaveRenapo"].ToString();
                    //////}
                    //////catch { }

                    DtGeneral.ClaveRENAPO = cboEmpresas.ItemActual.GetItem("ClaveRenapo");
                    DtGeneral.FarmaciaConectadaNombre = cboSucursales.Text;


                    DtGeneral.EmpresaDatos.IdEmpresa = cboEmpresas.ItemActual.GetItem("IdEmpresa");
                    DtGeneral.EmpresaDatos.Nombre = cboEmpresas.ItemActual.GetItem("Nombre");
                    DtGeneral.EmpresaDatos.NombreCorto = cboEmpresas.ItemActual.GetItem("NombreCorto");

                    DtGeneral.EmpresaDatos.EsDeConsignacion = cboEmpresas.ItemActual.GetItem("EsDeConsignacion");
                    DtGeneral.EmpresaDatos.RFC = cboEmpresas.ItemActual.GetItem("RFC");
                    DtGeneral.EmpresaDatos.EdoCiudad = cboEmpresas.ItemActual.GetItem("EdoCiudad");
                    DtGeneral.EmpresaDatos.Colonia = cboEmpresas.ItemActual.GetItem("Colonia");
                    DtGeneral.EmpresaDatos.Domicilio = cboEmpresas.ItemActual.GetItem("Domicilio");
                    DtGeneral.EmpresaDatos.CodigoPostal = cboEmpresas.ItemActual.GetItem("CodigoPostal");
                    DtGeneral.EmpresaDatos.Status = cboEmpresas.ItemActual.GetItem("Status"); 



                    if ( ValidarAlmacen_Ubicaciones() )
                    {
                        if (!LoginUser.AutenticarUsuarioLogin())
                        {
                            LoginUser.Usuario = "";
                            LoginUser.Password = "";
                            txtUsuario.Text = "";
                            txtPassword.Text = "";
                            txtUsuario.Focus();
                            bAutenticando = false;
                            btnAceptar.Enabled = true;
                            lblAutenticando.Visible = false; // 2K110718.1500 Jesus Diaz 
                            HabilitarLogin(true);

                            if (DtGeneral.IntentosPassword >= 3)
                            {
                                General.msjAviso(string.Format("El usuario [ {0} ] ha superado el número de intentos de ingresos a la aplicación.\n\nLa aplicación se cerrara.", 
                                    DtGeneral.UsuarioConectando));
                                this.Close();
                            }
                        }
                        else
                        {
                            VerificarHost(); 
                            btnCancelar.Enabled = false; 
                            dtsPermisos = LoginUser.Permisos;
                            bUsuarioLogeado = true;

                            CargarDatosDeUsuarioConectado(); 
                            General.FechaSistemaObtener();

                            lblAutenticando.Visible = false; // 2K110718.1500 Jesus Diaz 


                            // Determinar si la Empresa es de Tipo Consignacion  
                            try
                            {
                                DtGeneral.EsEmpresaDeConsignacion = Convert.ToBoolean(((DataRow)cboEmpresas.ItemActual.Item)["EsDeConsignacion"]); 
                            }
                            catch { }


                            // Nombre de la Farmacia Conectada  
                            try
                            {
                                DtGeneral.FarmaciaConectadaNombre = Convert.ToString(((DataRow)cboSucursales.ItemActual.Item)["Farmacia"]);
                            }
                            catch { }

                            // ManejaVtaPubGral, ManejaControlados 
                            try
                            {
                                DtGeneral.ManejaVentaPublico = Convert.ToBoolean(((DataRow)cboSucursales.ItemActual.Item)["ManejaVtaPubGral"]);
                            }
                            catch { }
                            try
                            {
                                DtGeneral.ManejaControlados = Convert.ToBoolean(((DataRow)cboSucursales.ItemActual.Item)["ManejaControlados"]);
                            }
                            catch { }
                            try
                            {
                                DtGeneral.EsAlmacen = Convert.ToBoolean(((DataRow)cboSucursales.ItemActual.Item)["EsAlmacen"]);
                            }
                            catch { }

                            // Cargar datos de Jurisdiccion 
                            try
                            {
                                DtGeneral.Jurisdiccion = Convert.ToString(((DataRow)cboSucursales.ItemActual.Item)["IdJurisdiccion"]);
                                DtGeneral.JurisdiccionNombre = Convert.ToString(((DataRow)cboSucursales.ItemActual.Item)["Jurisdiccion"]);
                            }
                            catch { }

                            try
                            {
                                GnFarmacia.EsUnidadUnidosis = Convert.ToBoolean(((DataRow)cboSucursales.ItemActual.Item)["EsUnidosis"]);
                            }
                            catch { }

                            //// VerificarInterface(); 
                            if (!bAutentificacionCancelada)
                            {
                                this.Hide();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex = null; 
            }
        }

        private bool ValidarAlmacen_Ubicaciones()
        {
            bool bRegresa = true;

            switch (DtGeneral.ModuloEnEjecucion)
            { 
                case TipoModulo.Almacen:
                case TipoModulo.AlmacenUnidosis:
                    GnFarmacia.ManejaUbicaciones = true;
                    break; 

                default:
                    GnFarmacia.ManejaUbicaciones = false; 
                    break;
            }

            return bRegresa; 
        }

        /// <summary>
        /// Busca si se cuenta con un archivo Host y lo actualiza 
        /// </summary>
        private void VerificarHost()
        {
            string sFileOrigen = Application.StartupPath + @"\Hosts.txt";
            string sRuta = Environment.SystemDirectory + @"\drivers\etc\hosts";


            ////if (File.Exists(sFileOrigen)) 
            ////{
            ////    try
            ////    {
            ////        File.Copy(sFileOrigen, sRuta, true);
            ////        Directory.CreateDirectory(Application.StartupPath + @"\Hosts");
            ////        File.Move(sFileOrigen, Application.StartupPath + @"\Hosts\Hosts.txt"); 
            ////    }
            ////    catch { }
            ////}
        }

        private void CargarDatosDeUsuarioConectado() 
        {
            //General.msjAviso(General.DatosConexion.CadenaConexion, "EX"); 
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
            clsConsultas query = new clsConsultas(General.DatosConexion, "", "", "", false);
            clsLeer leer = new clsLeer(ref cnn);

            //General.msjAviso(General.DatosConexion.CadenaConexion, "EX-X"); 

            leer.DataSetClase = query.Personal(cboEstados.Data, cboSucursales.Data, LoginUser.Personal, "CargarDatosDeUsuarioConectado()"); 
            if (leer.Leer())
            {
                DtGeneral.IdPersonal = LoginUser.Personal;
                DtGeneral.LoginUsuario = LoginUser.Usuario;
                DtGeneral.NombrePersonal = leer.Campo("NombreCompleto");

                if(DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen)
                {
                    string sSql = string.Format(
                        "Select * \n" +
                        "From vw_PersonalCEDIS(NoLock) \n" +
                        "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdPersonal_Relacionado = '{3}'  \n",
                        cboEmpresas.Data, cboEstados.Data, cboSucursales.Data, LoginUser.Personal);
                    //leer.DataSetClase = query.PersonalCEDIS(cboEmpresas.Data, cboEstados.Data, cboSucursales.Data, LoginUser.Personal, "CargarDatosDeUsuarioConectado()");
                    if(leer.Exec(sSql))
                    {
                        if(leer.Leer())
                        {
                            DtGeneral.IdPersonalCEDIS = leer.Campo("IdPersonal");
                            DtGeneral.IdPersonalCEDIS_Relacionado = LoginUser.Personal;
                        }
                    }
                }
            }

            leer.DataSetClase = query.PersonalAdmistrador(cboEstados.Data, cboSucursales.Data, LoginUser.Personal, "CargarDatosDeUsuarioConectado()");
            if (leer.Leer())
            {
                DtGeneral.EsAdministrador = true;
            } 
        }
        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSucursales.Clear();
            cboSucursales.Add("0", "<< Seleccione >>");

            //if (!bCargando)
            {
                try
                {
                    if (dtsFarmacias != null)
                    {
                        string sFiltro = string.Format("IdEmpresa = '{0}' and IdEstado = '{1}' ", cboEmpresas.Data, cboEstados.Data );

                        if(dtsFarmacias.Tables.Count > 0)
                        {
                            cboSucursales.Add(dtsFarmacias.Tables[0].Select(sFiltro), true, "IdFarmacia", "NombreFarmacia");
                        }

                        if (!bCargando)
                        {
                            txtUsuario.Text = "";
                            txtPassword.Text = "";
                        }
                    }
                }
                catch { }
            }
            cboSucursales.SelectedIndex = 0;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                if(bEsIntercalcionCorrecta)
                {
                    btnAceptar_Click(null, null);
                }
            }
        }

        private void cboSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bCargando)
            {
                if (sSucursal != cboSucursales.Data)
                {
                    sSucursal = cboSucursales.Data;
                    txtUsuario.Text = "";
                    txtPassword.Text = "";
                }
            }
        }

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");

            try
            {
                if (dtsEstados != null)
                {
                    if (dtsEstados.Tables.Count > 0)
                    {
                        cboEstados.Add(dtsEstados.Tables[0].Select("IdEmpresa = '" + cboEmpresas.Data + "'"), true, "IdEstado", "NombreEstado");
                    }

                    if (!bCargando)
                    {
                        txtUsuario.Text = "";
                        txtPassword.Text = "";
                    }
                }
            }
            catch { }
            cboEstados.SelectedIndex = 0;
        }

        private void FrmEdoLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                thrAutenticar.Interrupt();
                thrAutenticar = null; 
            }
            catch { }
        }
    }
}