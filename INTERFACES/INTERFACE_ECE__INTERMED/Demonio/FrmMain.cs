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

using Dll_IRE_INTERMED;
using Dll_IRE_INTERMED.Clases;
using Dll_IRE_INTERMED.Informacion;

namespace ISIIECE_SERVICIOS
{
    public partial class FrmMain : Form
    {
        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login;
        //bool bConexionWeb = false;
        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        FrmNavegador Navegador;
        FrmRecetaEspecifica Especifica;

        ClsReplicacioneRecetaElectronica replicacion;

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
            // Servidor Regional, solo cuando se conecta a un Servidor Regional  
            DtGeneral.ModuloEnEjecucion = TipoModulo.Ninguno;


            clsAbrirForma.AssemblyActual("ISIIECE_SERVICIOS");
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo); 

            General.IconoSistema = this.Icon;

            DtGeneral.ConexionOficinaCentral = true;
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "ISIIECE SERVICIOS";


            // MessageBox.BackColor = Global.FormaBackColor;



            if (!bUsuarioLogeado)
            {
                pfLoginServidor();
            }

            General.ServidorEnRedLocal = true;

            replicacion = new ClsReplicacioneRecetaElectronica();

        }

        private bool pfLoginServidor()
        {
            bool bRegresa = false;
            Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            Login.Arbol = "CIM4";
            Login.XmlEnDirectorioApp =  !DtGeneral.EsEquipoDeDesarrollo; 

            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName;
            //BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + IMach4.DatosApp.Modulo.Replace("Dll_", "") + " " + IMach4.DatosApp.Version;
            if (Login.AutenticarUsuario())
            {
                BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + GnDll_SII_RE_INTERMED.DatosApp.Version;
                //// BarraDeStatus.Panels[lblFarmacia.Name].Text = "Estado : " + DtGeneral.EstadoConectado + " -- " + DtGeneral.EstadoConectadoNombre + "      " + " Farmacia : " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
                BarraDeStatus.Panels[lblServidor.Name].Text = "Servidor : " + General.DatosConexion.Servidor;
                BarraDeStatus.Panels[lblBaseDeDatos.Name].Text = "Base de Datos : " + General.DatosConexion.BaseDeDatos;
                BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneral.NombrePersonal;

                bRegresa = true;
                ////IMach4.EstadoConectado = DtGeneral.EstadoConectado;
                ////IMach4.FarmaciaConectada = DtGeneral.FarmaciaConectada; 


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
            string[] sModulos = { "ISIIECE_SERVICIOS" };
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
        private void btnEnviarRecetas_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            //bRegresa = replicacion.EnviarRecetasElectronicasAtendidas();

            FrmInformacionDeRecetas receta = new FrmInformacionDeRecetas(Dll_IRE_INTERMED.TipoProcesoReceta.EnviarInformacionSurtido);
            bRegresa = receta.ProcesarSolicitudDeInformacion();

            if(bRegresa)
            {
                General.msjAviso("Información Enviada.");
            }
            else
            {
                General.msjError("Ocurrió un error.");
            }
        }

        private void btnObtenerRecetas_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            //bRegresa = replicacion.ObtenerRecetasElectronicas();

            FrmInformacionDeRecetas receta = new FrmInformacionDeRecetas(Dll_IRE_INTERMED.TipoProcesoReceta.DescargaRecetasMasivo);
            bRegresa = receta.ProcesarSolicitudDeInformacion(); 

            if (bRegresa)
            {
                General.msjAviso("Información Obtenida.");
            }
            else
            {
                General.msjError("Ocurrió un error.");
            }
        }
        #endregion Botones

        private void btnRecetaEspecifico_Click(object sender, EventArgs e)
        {
            Especifica = new FrmRecetaEspecifica();
            Especifica.ShowDialog();
        }
    }
}
