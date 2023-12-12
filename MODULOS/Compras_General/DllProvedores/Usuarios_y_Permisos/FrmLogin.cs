using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllProveedores.Usuarios_y_Permisos
{
    internal partial class FrmLogin : FrmBaseExt
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
        DllProveedores.Usuarios_y_Permisos.clsLogin LoginUser = new DllProveedores.Usuarios_y_Permisos.clsLogin("", "");
        //wsConexion.wsConexionDB wsDb = new wsConexion.wsConexionDB();
        clsCriptografo Cryp = new clsCriptografo();

        // int iAltoFormNormal = 200;
        // int iAltoFormDebug = 0;
        int iDesplazamiento = 48;
        int iDesplazamientoUp = 20;
        int iDesAncho = 50;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmEdoLogin_Load(object sender, EventArgs e)
        {
            this.Height = 144; 
            FrameLogin.Height = 109;


            string sIdSucursal = ""; //General.XmlConfig.GetValues("IdSucursal").Trim(); 
            string sIdEstado = "";
            string sIdEmpresa = "";
            string sSql = "";
            Fg.IniciaControles(this, true);

            DllProveedores.Usuarios_y_Permisos.FrmConect Conect = new DllProveedores.Usuarios_y_Permisos.FrmConect();
            Fg.CentrarForma(Conect);
            Conect.ShowDialog();

            bCargando = true;
            if (Conect.bExisteFileConfig)
            {
                if (Conect.bConexionEstablecida)
                {
                    //LoginUser.CargarUsuarios();
                    DataSet dtsListaSucursales = new DataSet();

                    // datosCliente = new clsDatosCliente(GnProveedores.DatosApp, this.Name, "FrmLogin_Load");
                    // Query = new clsLeerWeb(General.Url, datosCliente);
                    //General.msjUser(Query.DatosConexion.Servidor + " " + Query.DatosConexion.BaseDeDatos + " " + Query.DatosConexion.Usuario + " " + Query.DatosConexion.Password);

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

        private bool validarUsuario()
        {
            bool bRegresa = true;

            if (txtUsuario.Text.Trim().ToUpper() != "Administrador".ToUpper())
            {
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarUsuario())
            {
                ////LoginUser.Empresa = "001";  // cboEmpresas.Data;
                ////LoginUser.Estado = "25"; // cboEstados.Data;
                ////LoginUser.Sucursal = "0001"; //cboSucursales.Data;
                ////sEmpresa = "001";  // cboEmpresas.Data;
                ////sSucursal = LoginUser.Sucursal;
                ////sEstado = "25"; // cboEstados.Data;

                LoginUser.Usuario = txtUsuario.Text;
                LoginUser.Password = txtPassword.Text;
                LoginUser.Arbol = sArbol;
                LoginUser.Permisos = dtsPermisos;

                btnAceptar.Enabled = false; 
                btnCancelar.Enabled = false; 

                if (LoginUser.AutenticarUsuarioLogin())
                {
                    dtsPermisos = LoginUser.Permisos;
                    bUsuarioLogeado = true;
                    CargarDatosDeUsuarioConectado();
                    General.FechaSistemaObtener(); 

                    this.Refresh(); 
                    System.Threading.Thread.Sleep(500);
                    this.Refresh(); 

                    GnProveedores.IniciarCatalogos(); 

                    this.Hide();
                }
                else
                {
                    btnAceptar.Enabled = true;
                    btnCancelar.Enabled = true;

                    LoginUser.Usuario = "";
                    LoginUser.Password = "";
                    txtUsuario.Text = "";
                    txtPassword.Text = "";
                    txtUsuario.Focus();
                }
            }
        }

        private void CargarDatosDeUsuarioConectado()
        {
            clsLeerWeb Query; // = new clsLeerWeb(General.Url, General.DatosConexion, new SC_SolutionsSystem.Data.clsDatosCliente("DllProveedores", this.Name, "FrmEdoLogin_Load"));
            clsDatosCliente datosCliente;

            datosCliente = new clsDatosCliente(GnProveedores.DatosApp, this.Name, "FrmLogin_Load");
            Query = new clsLeerWeb(General.Url, datosCliente);

            if (Query.Exec(string.Format("Select P.*, N.LoginProv, N.Password " + 
                " From CatProveedores P (NoLock) " + 
                " Left Join Net_Proveedores N (NoLock) On ( P.IdProveedor = N.IdProveedor ) " + 
                " Where P.IdProveedor = '{0}' ", GnProveedores.IdProveedor)))
            {
                if (Query.Leer())
                {
                    GnProveedores.NombreProveedor = Query.Campo("Nombre");
                    GnProveedores.Usuario = Query.Campo("LoginProv");
                    GnProveedores.Password = Query.Campo("Password");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cerrar la aplicacion
            // Application.Exit();
            this.Close();
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

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                btnAceptar_Click(null, null);       
        }

    }
}