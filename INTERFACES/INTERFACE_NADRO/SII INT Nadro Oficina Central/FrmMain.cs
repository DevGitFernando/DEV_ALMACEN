﻿using System;
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

using Dll_SII_INadro; 

namespace SII_INT_Nadro_Oficina_Central
{
    public partial class FrmMain : Form
    {
        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login;
        //bool bConexionWeb = false;
        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        FrmNavegador Navegador;

        ////CheckVersion buscarUpdate; 
        System.Timers.Timer tmUpdaterModulo;
        Thread hilo;
        bool bBuscandoUpdate = false;
 
        //Para Auditoria
        clsAuditoria auditoria;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Servidor Regional, solo cuando se conecta a un Servidor Regional  
            DtGeneral.ModuloEnEjecucion = TipoModulo.Regional;


            clsAbrirForma.AssemblyActual("INadro Oficina Central");
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo); 

            General.IconoSistema = this.Icon;

            DtGeneral.ConexionOficinaCentral = true;
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "INadro Oficina Central";
            // MessageBox.BackColor = Global.FormaBackColor;


            if (!bUsuarioLogeado)
            {
                pfLoginServidor();
            }

            General.ServidorEnRedLocal = true;

        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.F11:
                        // CargarParametros();
                        break;

                    case Keys.F12: 
                        DtGeneral.InformacionConexion();
                        break;

                    default:
                        break;
                }
            }
        } 

        private bool pfLoginServidor()
        {
            bool bRegresa = false;
            Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            Login.Arbol = "ISNO";

            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + GnDll_SII_INadro.DatosApp.Version;
            if (Login.AutenticarUsuario())
            {
                BarraDeStatus.Panels[lblFarmacia.Name].Text = "Estado : " + DtGeneral.EstadoConectado + " -- " + DtGeneral.EstadoConectadoNombre + "      " + " Unidad : " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
                BarraDeStatus.Panels[lblServidor.Name].Text = "Servidor : " + General.DatosConexion.Servidor;
                BarraDeStatus.Panels[lblBaseDeDatos.Name].Text = "Base de Datos : " + General.DatosConexion.BaseDeDatos;
                BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneral.NombrePersonal;

                bRegresa = true;
                Navegador = new FrmNavegador();
                Navegador.MdiParent = this;
                Navegador.Permisos = Login.Permisos;
                //Navegador.ListaIconos = imgNavegacion_2;
                Navegador.ListaIconos = imgNavegacion;
                Navegador.Posicion = ePosicion.Izquierda;
                Login = null;
                Navegador.Show();

                this.Text = " Módulo : " + Navegador.NombreModulo + "         " + DtGeneral.EmpresaConectadaNombre;

                // Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.SinLimite; 

                ///// Cargar los Parametros del sistema 
                GnDll_SII_INadro.Parametros_OficinaCentral = new clsParametros_SII_INadro(General.DatosConexion, GnOficinaCentral.DatosApp, "ISNO");
                GnDll_SII_INadro.Parametros_OficinaCentral.CargarParametros(); 

                //// Pasar la ruta de reportes al General 
                DtGeneral.RutaReportes = GnDll_SII_INadro.RutaReportes;


                ////// Checar la version instalada 
                ////string[] sModulos = { "Configuracion", "OficinaCentral", "Farmacia", "Compras", "Proveedores", "DllProveedores", 
                ////                      "Servicio Oficina Central", "Servicio Oficina Central Regional", 
                ////                      "Servicio Cliente Regional", "Servicio Cliente", 
                ////                      "Configuración Servicio Oficina Central", 
                ////                      "Configuración Servicio Oficina Central Regional", 
                ////                      "Configuración Servicio Cliente Regional", 
                ////                      "Configuración Servicio Cliente"};
                ////DtGeneral.RevisarVersion(sModulos);
                RevisarVersionModulos();

                FileVersionInfo f = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
                ////buscarUpdate = new CheckVersion(f.OriginalFilename, f.FileVersion, false); 

                tmUpdaterModulo = new System.Timers.Timer((1000 * 30) * 2);
                tmUpdaterModulo.Elapsed += new ElapsedEventHandler(this.tmUpdaterModulo_Elapsed);
                tmUpdaterModulo.Enabled = true;
                tmUpdaterModulo.Start();

                // Crear la instacia para el objeto de la clase de Auditoria
                auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                                             DtGeneral.IdPersonal, DtGeneral.IdSesion, General.Modulo, this.Name, General.Version);

                auditoria.GuardarAud_LoginUni();
                DtGeneral.IdSesion = clsAuditoria.Sesion;
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
            string[] sModulos = { "Configuracion", "OficinaCentral", "Farmacia", "Compras", "Proveedores", "DllProveedores", 
                                      "Servicio Oficina Central", "Servicio Oficina Central Regional", 
                                      "Servicio Cliente Regional", "Servicio Cliente", 
                                      "Configuración Servicio Oficina Central", 
                                      "Configuración Servicio Oficina Central Regional", 
                                      "Configuración Servicio Cliente Regional", 
                                      "Configuración Servicio Cliente"};
            DtGeneral.RevisarVersion(sModulos); 
        }

        private void btnNavegador_Click(object sender, EventArgs e)
        {
            if (!General.NavegadorCargado)
            {
                Navegador = new FrmNavegador();
                Navegador.MdiParent = this;
                Navegador.Permisos = General.ArbolDeNavegacion;
                Navegador.ListaIconos = General.IconosNavegacion;
                Navegador.Show();
            }
        }

        private void bntRegistroErrores_Click(object sender, EventArgs e)
        {
            DtGeneral.MostrarLogErrores();

            //FrmListadoFarmacias f = new FrmListadoFarmacias();
            //f.ShowDialog(); 
        }


        private void btnCambiarPassword_Click(object sender, EventArgs e)
        {
            DtGeneral.MostrarCambiarPasswordUsuario();
        }

        private void tmUpdaterModulo_Elapsed(object sender, ElapsedEventArgs e)
        {
            tmUpdaterModulo.Stop();
            tmUpdaterModulo.Enabled = false;

            hilo = new Thread(this.ChecarVersion);
            hilo.Name = "UpdateOficinaCentral";
            hilo.Start();
        }

        private void ChecarVersion()
        {
            bBuscandoUpdate = true; 
            ////if (!DtGeneral.EsEquipoDeDesarrollo)
            ////{
            ////    if (buscarUpdate.CheckVersionModulo())
            ////    {
            ////        if (General.msjConfirmar("Se encontro una actualización para el Módulo de Oficina Central. \n\n" +
            ////            " ¿ Desea descargarla en este momento ?") == DialogResult.Yes)
            ////        {
            ////            buscarUpdate.DescargarActualizacion();
            ////        }
            ////    }
            ////}

            ////HabilitarActualizaciones(); 
            bBuscandoUpdate = true; 
        }

        private void HabilitarActualizaciones()
        {
            // Se detecto probable carga de trabajo para el Servidor de Actualizaciones. 
            // Los tiempos de consulta son variables, buscando disminuir la cargar para el servidor.

            Random x = new Random(30);
            int i = x.Next(10, 20);

            tmUpdaterModulo.Enabled = true;
            tmUpdaterModulo.Interval = (1000 * 60) * i;
            tmUpdaterModulo.Start();
        }

        private void btnBuscarActualizaciones_Click(object sender, EventArgs e)
        {
            if (!bBuscandoUpdate)
            {
                tmUpdaterModulo_Elapsed(null, null); 
            }
        }
    }
}
