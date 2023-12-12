using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using System.Threading;
using System.Timers;
using System.IO;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Informacion;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllTransferenciaSoft;

using Dll_IATP2; 

namespace Configuracion_ATP2
{
    public partial class FrmMain : Form
    {
        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login;
        //bool bConexionWeb = false;
        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        FrmNavegador Navegador;
        string sVersion = Transferencia.Version;

        System.Timers.Timer tmUpdaterModulo;
        Thread hilo;
        bool bBuscandoUpdate = false;
 
        //Para Auditoria
        clsAuditoria auditoria;

        public FrmMain()
        {
            InitializeComponent();
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //// Servidor Regional, solo cuando se conecta a un Servidor Regional  
            DtGeneral.ModuloEnEjecucion = TipoModulo.FarmaciaUnidosis;


            ////clsAbrirForma.AssemblyActual("Configuracion IATP2");
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo); 

            General.IconoSistema = this.Icon;

            DtGeneral.ConexionOficinaCentral = true;
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "IATP2";
            // MessageBox.BackColor = Global.FormaBackColor;


            if (!bUsuarioLogeado)
            {
                pfLoginServidor();
            }

            General.ServidorEnRedLocal = true;

        }

        private bool pfLoginServidor()
        {
            bool bRegresa = false;
            Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            Login.Arbol = "IAPT2";

            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName;
            //BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + IMach4.DatosApp.Modulo.Replace("Dll_", "") + " " + IMach4.DatosApp.Version;
            if (Login.AutenticarUsuario())
            {
                BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + IATP2.DatosApp.Version;
                //// BarraDeStatus.Panels[lblFarmacia.Name].Text = "Estado : " + DtGeneral.EstadoConectado + " -- " + DtGeneral.EstadoConectadoNombre + "      " + " Farmacia : " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
                BarraDeStatus.Panels[lblServidor.Name].Text = "Servidor : " + General.DatosConexion.Servidor;
                BarraDeStatus.Panels[lblBaseDeDatos.Name].Text = "Base de Datos : " + General.DatosConexion.BaseDeDatos;
                BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneral.NombrePersonal;

                bRegresa = true;
                IATP2.EstadoConectado = DtGeneral.EstadoConectado;
                IATP2.FarmaciaConectada = DtGeneral.FarmaciaConectada; 
                //////Navegador = new FrmNavegador();
                //////Navegador.MdiParent = this;
                //////Navegador.Permisos = Login.Permisos;
                ////////Navegador.ListaIconos = imgNavegacion_2;
                //////Navegador.ListaIconos = imgNavegacion;
                //////Navegador.Posicion = ePosicion.Izquierda;
                //////Login = null;
                //////Navegador.Show();

                //////this.Text = " Módulo : " + Navegador.NombreModulo + "         " + DtGeneral.EmpresaConectadaNombre;

                // Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.Limite30;

                //////// Cargar los Parametros del sistema 
                //IATP2.Parametros = new clsParametrosIMach(General.DatosConexion, IATP2.DatosApp);
                //IATP2.Parametros.CargarParametros(); 


                RevisarVersionModulos();

            }
            else
            {
                Application.Exit(); // this.Close();
            }

            return bRegresa;
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
            string[] sModulos = { "IMach4", "Configuracion_ATP2" };
            DtGeneral.RevisarVersion(sModulos); 
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
            DtGeneral.MostrarLogErrores();

            //FrmListadoFarmacias f = new FrmListadoFarmacias();
            //f.ShowDialog(); 
        }

        private void CargarPantalla(string Pantalla)
        {
            General.CargarPantalla(Pantalla, "Dll_IATP2", this);  
        }

        #region Clientes 
        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarPantalla("FrmClientes"); 
        }

        private void terminalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarPantalla("FrmTerminales"); 
        }

        private void terminalesClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarPantalla("FrmClientesTerminales"); 
        }

        #endregion Clientes 

        #region Farmacia
        private void productosAlmancénToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarPantalla("FrmProductosIMach"); 
        }

        private void productosDispensaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarPantalla("FrmProductosClientes"); 
        }
        #endregion Farmacia

        #region Parametros 
        private void parametrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarPantalla("FrmParametros"); 
        }
        #endregion Parametros
    }
}
