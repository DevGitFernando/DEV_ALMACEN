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

using Dll_ISESEQ;

namespace ISESEQ_SERVICIOS
{
    public partial class FrmMain : Form
    {
        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login;
        //bool bConexionWeb = false;
        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        FrmNavegador Navegador;

        System.Timers.Timer tmUpdaterModulo;
        Thread hilo;
        bool bBuscandoUpdate = false;
 
        //Para Auditoria
        clsAuditoria auditoria;

        public FrmMain()
        {
            InitializeComponent();
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo);

            btnOpciones_Almacen.Visible = DtGeneral.EsAlmacen;
            btnOpciones_Farmacia.Visible = !DtGeneral.EsAlmacen;
            btnOpciones_General.Visible = false;

            btnOpciones_Almacen.Visible = false;
            btnOpciones_Farmacia.Visible = false;

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Servidor Regional, solo cuando se conecta a un Servidor Regional  
            DtGeneral.ModuloEnEjecucion = TipoModulo.Ninguno;


            clsAbrirForma.AssemblyActual("ISESEQ SERVICIOS");
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo); 

            General.IconoSistema = this.Icon;

            DtGeneral.ConexionOficinaCentral = true;
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            //General.ArchivoIni = "IMach4";
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
            string sArbol = ""; 
            Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            Login.Arbol = "CIM4";
            Login.XmlEnDirectorioApp = false; 

            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName;
            //BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + IMach4.DatosApp.Modulo.Replace("Dll_", "") + " " + IMach4.DatosApp.Version;
            if (Login.AutenticarUsuario())
            {
                BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + GnDll_SII_SESEQ.DatosApp.Version;
                //// BarraDeStatus.Panels[lblFarmacia.Name].Text = "Estado : " + DtGeneral.EstadoConectado + " -- " + DtGeneral.EstadoConectadoNombre + "      " + " Farmacia : " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
                BarraDeStatus.Panels[lblServidor.Name].Text = "Servidor : " + General.DatosConexion.Servidor;
                BarraDeStatus.Panels[lblBaseDeDatos.Name].Text = "Base de Datos : " + General.DatosConexion.BaseDeDatos;
                BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneral.NombrePersonal;

                bRegresa = true;
                ////IMach4.EstadoConectado = DtGeneral.EstadoConectado;
                ////IMach4.FarmaciaConectada = DtGeneral.FarmaciaConectada; 



                btnOpciones_Almacen.Visible = DtGeneral.EsAlmacen;
                btnOpciones_Farmacia.Visible = !DtGeneral.EsAlmacen;
                btnOpciones_General.Visible = true;


                Application.DoEvents();
                btnMenuDeOpciones.Visible = false; 


                // Cargar los Parametros del sistema 
                sArbol = DtGeneral.EsAlmacen ? "ALMN" : "PFAR";

                GnFarmacia.Parametros = new clsParametrosPtoVta(General.DatosConexion, GnFarmacia.DatosApp,
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sArbol);
                GnFarmacia.Parametros.CargarParametros();
                GnFarmacia.CargarDatosPublicoGeneral();

                DtGeneral.RutaReportes = GnFarmacia.RutaReportes;  


                // Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.Limite30;

                ////// Cargar los Parametros del sistema 
                ////IMach4.Parametros = new clsParametrosIMach(General.DatosConexion, IMach4.DatosApp);
                ////IMach4.Parametros.CargarParametros(); 


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
            string[] sModulos = { "ISESEQ_SERVICIOS" };
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

        #region Botones
        private void btnAlmacen_01__GenerarTXT_Click(object sender, EventArgs e)
        {
            General.CargarPantalla("FrmDocumentos_SIAM", "Dll_ISESEQ", this);
        }

        private void btnAlmacen_02__GenerarTXT_Pedidos_Click(object sender, EventArgs e)
        {
            General.CargarPantalla("FrmDocumentos_Pedidos_SIAM", "Dll_ISESEQ", this);
        }

        private void btnAlmacen_03__GenerarTXT_Transferencias_Click( object sender, EventArgs e )
        {
            General.CargarPantalla("FrmDocumentos_Transferencias_SIAM", "Dll_ISESEQ", this);
        }

        private void btnAlmacen_04__GenerarTXT_PedidosTransferencias_Click( object sender, EventArgs e )
        {
            General.CargarPantalla("FrmDocumentos_TransferenciasPedidos_SIAM", "Dll_ISESEQ", this);
        }
        #endregion Botones

        private void btnEstadisticas_Click( object sender, EventArgs e )
        {
            General.CargarPantalla("FrmEstadisticas", "Dll_ISESEQ", this);
            
        }

        private void btnEnviarInformacion_Click( object sender, EventArgs e )
        {
            General.CargarPantalla("FrmEnviarInformacionOperativa", "Dll_ISESEQ", this);
        }

        private void btnFarmacia_01_EnviarAcuses_Click(object sender, EventArgs e)
        {

        }

        private void btnColectivos_Click(object sender, EventArgs e)
        {
            General.CargarPantalla("FrmListadoDeColectivos", "Dll_ISESEQ", this);
        }
    }
}
