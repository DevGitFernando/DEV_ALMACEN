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
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

//using DllFarmaciaSoft;
//using DllFarmaciaSoft.Usuarios_y_Permisos;
//using DllTransferenciaSoft;

namespace DtUtileriasBD
{
    public partial class FrmMain : Form
    {
        //DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login;
        // bool bConexionWeb = false;
        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        FrmNavegador Navegador;
        string sVersion = ""; //Transferencia.Version;

        string sNameSpace = "Utilerias BD";

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ////// Servidor Farmacia  
            ////DtGeneral.ModuloEnEjecucion = TipoModulo.Farmacia;

            clsAbrirForma.AssemblyActual("Utilerias BD");
            General.CargarImagenMDI(this, Color.White, "");

            General.IconoSistema = this.Icon;

            General.MultiplesEntidades = true;
            General.bModoDesarrollo = true; 
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "IntegradorBD"; 
            // MessageBox.BackColor = Global.FormaBackColor;

            ////// Tipo de Servicio en Ejecucion 
            ////Transferencia.ServicioEnEjecucion = TipoServicio.Cliente;

            if (!bUsuarioLogeado)
            {
                pfLoginServidor();
            }

            General.ServidorEnRedLocal = true;

        }

        private bool pfLoginServidor()
        {
            bool bRegresa = false;
            //////Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            //////Login.Arbol = "CIBD"; 

            //////// Configuración Repositorio Central

            //////BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + Transferencia.DatosApp.Version;
            //////if (Login.AutenticarUsuario())
            //////{
            //////    //BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + Application.ProductVersion;
            //////    BarraDeStatus.Panels[lblFarmacia.Name].Text = "Estado : " + DtGeneral.EstadoConectado + " -- " + DtGeneral.EstadoConectadoNombre + "      " + " Farmacia : " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            //////    BarraDeStatus.Panels[lblServidor.Name].Text = "Servidor : " + General.DatosConexion.Servidor;
            //////    BarraDeStatus.Panels[lblBaseDeDatos.Name].Text = "Base de Datos : " + General.DatosConexion.BaseDeDatos;
            //////    BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneral.NombrePersonal;

            //////    //MessageBox.Show(Login.Sucursal.ToString());
            //////    bRegresa = true;
            //////    Navegador = new FrmNavegador();
            //////    Navegador.MdiParent = this; 
            //////    // Navegador.Permisos = Login.Permisos;
            //////    Navegador.Permisos = MenuIntegradorBD(Login.Arbol); 
            //////    //Navegador.ListaIconos = imgNavegacion_2;
            //////    Navegador.ListaIconos = imgNavegacion;
            //////    Navegador.Posicion = ePosicion.Izquierda;
            //////    Login = null;
            //////    Navegador.Show();

            //////    // Ajustar el Tiempo de Espera para Conexion 
            //////    General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.Limite30;

            //////    ////// Checar la version instalada 
            //////    ////string[] sModulos = { "Configuracion", "Configuración Servicio Cliente", "Servicio Cliente" }; 
            //////    ////DtGeneral.RevisarVersion(sModulos);
            //////    RevisarVersionModulos(); 
            //////}
            //////else
            //////{
            //////    Application.Exit(); // this.Close();
            //////}

            return bRegresa;
        }

        public DataSet MenuIntegradorBD(string Arbol)
        {
            DataSet dtsPermisos = new DataSet();
            //////clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            //////clsLeer leer = new clsLeer(ref cnn); 
            //////string sSql = "";

            //////sSql = string.Format(" Exec sp_Navegacion '{0}' ", Arbol);

            //////if (leer.Exec("Arbol", sSql))
            //////{
            //////    dtsPermisos = leer.DataSetClase; 
            //////}

            return dtsPermisos;
        }

        private void RevisarVersionModulos()
        {
            Thread thVersion = new Thread(this.RevisarVersionInstaladaModulos);
            thVersion.Name = "RevisarVersionModulosInstalados";
            thVersion.Start();
        }

        private void RevisarVersionInstaladaModulos()
        {
            // Checar la version instalada 
            string[] sModulos = { "Configuracion", "Configuración Servicio Cliente", "Servicio Cliente" }; 
            //// DtGeneral.RevisarVersion(sModulos);
        }

        private void btnNavegador_Click(object sender, EventArgs e)
        {
            ////if (!General.NavegadorCargado)
            ////{
            ////    Navegador = new FrmNavegador();
            ////    Navegador.MdiParent = this;
            ////    Navegador.Permisos = General.ArbolDeNavegacion;
            ////    Navegador.ListaIconos = General.IconosNavegacion;
            ////    Navegador.Show();
            ////}
        }

        private void bntRegistroErrores_Click(object sender, EventArgs e)
        {
            ////DtGeneral.MostrarLogErrores();
        }

        private void btnCambiarPassword_Click(object sender, EventArgs e)
        {
            ////DtGeneral.MostrarCambiarPasswordUsuario();
        }

        #region Menu 
        private void datosDeConexionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General.CargarPantalla("FrmConexion", sNameSpace, this);
        }

        private void backUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General.CargarPantalla("FrmBackUp", sNameSpace, this);
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General.CargarPantalla("FrmRestore", sNameSpace, this);
        }

        private void btnExtraerBD_Click(object sender, EventArgs e)
        {
            General.CargarPantalla("FrmExtraerBD", sNameSpace, this);
        }

        private void btnIntegrarBD_Click(object sender, EventArgs e)
        {
            General.CargarPantalla("FrmIntegrarBD", sNameSpace, this);
        }
        #endregion Menu
    }
}
