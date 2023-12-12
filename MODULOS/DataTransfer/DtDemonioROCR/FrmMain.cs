using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllTransferenciaSoft;
using DllTransferenciaSoft.IntegrarInformacion;
using DllTransferenciaSoft.Servicio;

namespace DtDemonioRCR
{
    public partial class FrmMain : Form
    {
        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login;
        // bool bConexionWeb = false;
        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        // FrmNavegador Navegador;


        // Arrancar servicio de transmision de informacion
        FrmServicio Demonio;
        // int x = 0; 
        // StreamWriter f; //= new StreamWriter(@"C:\svrTest.txt", true); 


        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            clsAbrirForma.AssemblyActual("Servicio Oficina Central Regional");
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo);

            General.IconoSistema = this.Icon;

            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "OficinaCentralRIR";
            // MessageBox.BackColor = Global.FormaBackColor;
            //Transferencia.FrmMainServicio = this;

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
            Login.Arbol = "SPM";

            // Configuración Repositorio Central

            ActivarSysTray();
            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + Transferencia.DatosApp.Version; 
            if (Login.AutenticarServicio())
            {
                Transferencia.ServicioEnEjecucion = TipoServicio.OficinaCentralRegional; 
                Transferencia.PrepararConexion();
                Transferencia.ObtenerDatosOrigenRegional(); 

                //DtGeneral.EstadoConectado = DtGeneral.IdOficinaCentral;
                //DtGeneral.EstadoConectadoNombre = DtGeneral.IdOficinaCentralNombre;
                //DtGeneral.FarmaciaConectada = DtGeneral.IdFarmaciaCentral;
                //DtGeneral.FarmaciaConectadaNombre = DtGeneral.IdFarmaciaCentralNombre;

                DtGeneral.NombrePersonal = "ADMINISTRADOR";
                //BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + Application.ProductVersion;
                BarraDeStatus.Panels[lblFarmacia.Name].Text = "Estado : " + DtGeneral.EstadoConectado + " -- " + DtGeneral.EstadoConectadoNombre + "      " + " Unidad : " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
                BarraDeStatus.Panels[lblServidor.Name].Text = "Servidor : " + General.DatosConexion.Servidor;
                BarraDeStatus.Panels[lblBaseDeDatos.Name].Text = "Base de Datos : " + General.DatosConexion.BaseDeDatos;
                BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneral.NombrePersonal;

                icoSystray.Visible = true;

                // Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.Limite30; 

                Demonio = new FrmServicio(Transferencia.ServicioEnEjecucion);
                Demonio.MdiParent = this;
                Fg.CentrarForma(Demonio);
                Demonio.Show();

            }
            else 
            {
                Application.Exit(); // this.Close();
            } 

            return bRegresa;
        }

        private void btnNavegador_Click(object sender, EventArgs e)
        {
            //if (!General.NavegadorCargado)
            //{
            //    Navegador = new FrmNavegador();
            //    Navegador.MdiParent = this;
            //    Navegador.Permisos = General.ArbolDeNavegacion;
            //    Navegador.ListaIconos = General.IconosNavegacion;
            //    Navegador.Show();
            //}
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                mnOpciones.Items[btnAbrir.Name].Enabled = true;
                mnOpciones.Items[btnMinimizar.Name].Enabled = false;
                this.ShowInTaskbar = false;
            }
        }

        private void ActivarSysTray()
        {
            icoSystray.Text = "Servicio Oficina Central Regional";
            icoSystray.Icon = General.IconoSistema;

            mnOpciones.Items[btnAbrir.Name].Enabled = false;
            mnOpciones.Items[btnMinimizar.Name].Enabled = true;

            icoSystray.Visible = false;
        }

        private void Restaurar()
        {
            this.Activate();
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = true;
            mnOpciones.Items[btnAbrir.Name].Enabled = false;
            mnOpciones.Items[btnMinimizar.Name].Enabled = true;

            Demonio.Show();
            Demonio.Activate();
            this.ActivateMdiChild(Demonio);
        }

        private void icoSystray_DoubleClick(object sender, EventArgs e)
        {
            Restaurar();
        }

        #region Botones 
        private void btnAbrir_Click(object sender, EventArgs e)
        {
            Restaurar();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // bool bSalir = true;

            ////if (Transferencia.EjecutandoProcesos)
            ////{
            ////    if (General.msjConfirmar("Se estan ejecutando algunos procesos, ¿ Desea cerrar el Servicio ? ") == DialogResult.No)
            ////    {
            ////        bSalir = false; 
            ////    }
            ////} 

            if (!Transferencia.EjecutandoProcesos)
            {
                clsDatosIntegracion.ForzarCierre = true;         

                Demonio.CerrrLogs();
                Demonio.CerrrLogs(); 
                Demonio.Close();
                Demonio = null;
                Application.Exit();
            }
        }
        #endregion Botones

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }    
    }
}
