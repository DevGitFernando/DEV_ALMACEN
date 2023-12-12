using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using System.Threading;
using System.Timers;



using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Seguridad;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllFarmaciaSoft
{
    public static class Sesion
    {
        #region variables
        enum Movimiento
        {
            Ninguna = 0,
            Iniciar = 1, Validar = 2, CerrarManual = 3, CerrarForzado = 4
        }

        static string sArbol = "";
        static bool bTablaPorValidar = true;
        static bool bValidarSesion = true;
        static bool bErrorAlValidar = false;
        static int iAncho = 0;
        static int iAnchoMax = 0;
        static Color cColor;

        static int iIdStatus = 0;
        static int iMinDifRefresco = 0;
        static bool bSesionActiva = false;

        static DateTime dtRefresh;
        static DateTime dtFechaServidor;
        static string sMensaje = "";
        static int iSegRefresco = 0;
        static int iSegDesconexion = 0;
        static string sLlaveAnterior = "";
        static bool bEsmismallave = false;

        static System.Timers.Timer tmRefresh;
        static Thread hilo;

        static clsConexionSQL cnn;
        static clsLeer leer;
        static clsGrabarError Error;

        static Form frMain;
        static PictureBox pcStatusComunicacion;
        static ToolStrip tstoolStrip;
        static Button btnRefrescar;

        static basGenerales Fg = new basGenerales();

        static string BaseBoar = "";
        static string sLlave = "";

        static bool bInicializado = false;

        public static bool ActualizandoModulo = false; 
        #endregion variables

        #region Eventos

        private static void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!GnFarmacia.Sesion_MultiplesConexiones)
            {
                if (Sesion.ValidarSesion)
                {
                    if (Sesion.bSesionActiva)
                    {
                        if(ActualizandoModulo)
                        {
                            //// Forzar el cerrado al detectar una actualización 
                            Sesion.CerradoManual();
                        }
                        else
                        {
                            if(General.msjConfirmar("Desea cerrar la sesion") == DialogResult.Yes)
                            {
                                Sesion.CerradoManual();
                            }
                            else
                            {
                                e.Cancel = true;
                            }
                        }
                    }
                }
            }
        }

        private static void btnRegistrarBeneficiarios_Click(object sender, EventArgs e)
        {
            DifFecha(true);
        }
        #endregion Eventos

        #region funciones y procedimientos publicos

        public static void iniciar(Form Main, ref PictureBox StatusComunicacion, ref ToolStrip tlStrp, ref Button Refrescar, string Arbol)
        {
            frMain = Main;
            pcStatusComunicacion = StatusComunicacion;
            tstoolStrip = tlStrp;
            btnRefrescar = Refrescar;

            

            Main.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmMain_FormClosing);
            Refrescar.Click += new System.EventHandler(btnRegistrarBeneficiarios_Click);


            //if (DtGeneral.EsEquipoDeDesarrollo)
            //{
            //    /// Variable local 
            //    bValidarSesion = false;
            //}

            if (Sesion.ValidarSesion)
            {
                sArbol = Arbol;

                Ejecutar((int)Movimiento.Iniciar);

                if (bErrorAlValidar)
                {
                    General.msjAviso("Ocurrio un errror al validar la sesión.");
                    
                    ///Forzar el cierre de la aplicacion 
                    Application.Exit();
                }
                else
                {
                    bSesionActiva = true;

                    switch (Sesion.iIdStatus)
                    {
                        case 2: //Usuario bloqueado por falta de uso
                            Sesion.bSesionActiva = false;
                            General.msjAviso("Usuario bloqueado.");
                            Application.Exit();
                            break;


                        case 3:
                            Sesion.bSesionActiva = false;

                            if (Sesion.iMinDifRefresco >= GnFarmacia.Sesion_MinDesconexion)
                            {

                                if (General.msjConfirmar("Ya se encuentra una sesión caduca aun abierta. desea cerrarla?") == DialogResult.Yes)
                                {
                                    Sesion.CerradoForzado();
                                }

                                Application.Exit();
                            }
                            else
                            {
                                if (!Sesion.bEsmismallave)
                                {
                                    General.msjAviso("Se encuentra sesión activa en otro equipo, intentar mas tarde.");
                                    Application.Exit();

                                }
                                else
                                {
                                    if (!GnFarmacia.Sesion_MultiplesConexiones)
                                    {
                                        //if (General.msjConfirmar("Se encuentra sesión activa, ¿Desea cerrarla?.") == DialogResult.Yes)
                                        //{
                                        //    Sesion.CerradoForzado();
                                        //    Application.Restart();
                                        //}
                                        General.msjAviso("Se encuentra sesión activa, intentar mas tarde.");
                                        Application.Exit();
                                    }
                                    else
                                    {
                                        Refresh();
                                    }
                                }
                            }
                            break;

                        default:
                            Refresh();
                            break;
                    }
                }
            }
        }
        #endregion funciones y procedimientos publicos

        #region funciones y procedimientos privados

        private static void Inicializar()
        {
            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);

            leer.Conexion.TiempoDeEsperaConexion = TiempoDeEspera.Limite30;
            leer.Conexion.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite30;

            Error = new clsGrabarError(General.DatosApp, "Sesion");

            iAncho = iAnchoMax = 200;
            cColor = Color.Green;

            if (sLlave == "")
            {
                SystemInfo.Caracteristicas(SystemInfoItem.UseBaseBoardProduct, 1);
                SystemInfo.Caracteristicas(SystemInfoItem.UseWindowsSerialNumber, 1);

                BaseBoar = SystemInfo.GetSystemInfoItem(SystemInfoItem.UseBaseBoardProduct);
                sLlave = SystemInfo.GetSystemInfoItems() + General.NombreEquipo;
            }
        }

        private static bool ValidarSesion
        {
            get
            {
                bool bRegresa = false;
                if (!DtGeneral.EsEquipoDeDesarrollo)
                {
                    bRegresa = bValidarSesion; 
                    if (bTablaPorValidar)
                    {
                        ValidarTabla_Sesion();
                    }
                    bRegresa = bValidarSesion;
                }

                return bRegresa; // bValidarSesion;
            }
        }

        public static void CerradoManual()
        {
            if (ValidarSesion)
            {
                Ejecutar((int)Movimiento.CerrarManual);

                bSesionActiva = false;
            }
        }

        private static void CerradoForzado()
        {
            Ejecutar((int)Movimiento.CerrarForzado);

            bSesionActiva = false;
        }

        public static void CerradoSesionForzado(string EstadoConectado, string FarmaciaConectada, string IdPersonal, string Arbol,
          string Llave, string BaseBoard, string NombreEquipo, int Sesion_DiasInactivo)
        {
            Ejecutar((int)Movimiento.CerrarForzado, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, sArbol=Arbol, sLlave=Llave, BaseBoar=BaseBoard, 
                General.NombreEquipo, 0);
        }

        private static void Ejecutar(int iOpcion)
        {
            Ejecutar(iOpcion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, sArbol, sLlave, BaseBoar, General.NombreEquipo, GnFarmacia.Sesion_DiasInactivo);
        }

        private static void Ejecutar(int iOpcion, string EstadoConectado ,string FarmaciaConectada, string IdPersonal, string Arbol,
           string Llave, string BaseBoard, string NombreEquipo, int Sesion_DiasInactivo)
        {
            Inicializar();


            string sSql = string.Format("Exec spp_Sesion_ControlDeAcceso @IdEstado = '{0}', @IdFarmacia = '{1}', @IdPersonal = '{2}', " +
                " @Arbol = '{3}', @Llave = '{4}', @MotherBoard = '{5}', @NombreMaquina = '{6}', @iOpcion = '{7}', @DiasInactivos = '{8}'",
                EstadoConectado, FarmaciaConectada, IdPersonal, Arbol, Llave, BaseBoard, NombreEquipo, 
                iOpcion, Sesion_DiasInactivo );
            
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Entrar()");
                //General.msjAviso("Ocurrio un errror al validar la sesión.");
                bErrorAlValidar = true;
                btnRefrescar.Text = "ERROR";

                //Application.Exit();
            }
            else
            {
                if (leer.Leer())
                {
                    bErrorAlValidar = false;

                    iIdStatus = leer.CampoInt("Status_Salida");
                    sMensaje = leer.Campo("Mensaje");
                    dtRefresh = leer.CampoFecha("FRefresco");
                    dtFechaServidor = leer.CampoFecha("FechaServidor");
                    iMinDifRefresco = leer.CampoInt("MinDifRefresco");
                    sLlaveAnterior = leer.Campo("Llave");

                    bEsmismallave = sLlave == sLlaveAnterior ? true : false;
                    //Refresh();
                }
            }
        }

        private static void ValidarTabla_Sesion()
        {
            Inicializar();

            string sSql = string.Format(" Select Name From Sysobjects (NoLock) Where Name = 'Sesion_ControlDeAcceso' and xType = 'U' ");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidarTabla_Sesion()");
            }
            else
            {
                bTablaPorValidar = false;

                bValidarSesion = leer.Leer();
            }
        }

        private static void Refresh()
        {
            

            if (ValidarSesion)
            {
                pcStatusComunicacion.Visible = true;
                btnRefrescar.Visible = true;

                iSegRefresco = GnFarmacia.Sesion_MinRefresco * 60;
                iSegDesconexion = GnFarmacia.Sesion_MinDesconexion * 60;

                IniciarIndicador_picture();

                hilo = new Thread(Refresh_Hilo);
                hilo.IsBackground = true;
                hilo.Name = "sesion";
                hilo.Start();             
            }
        }

        private static void Refresh_Hilo()
        {
            tmRefresh = new System.Timers.Timer((1000));
            tmRefresh.Elapsed += new System.Timers.ElapsedEventHandler(tmdtRefresh_Elapsed);
            tmRefresh.Enabled = true;
            tmRefresh.Start();
        }

        private static void tmdtRefresh_Elapsed(object sender, ElapsedEventArgs e)
        {
            //tmRefresh.Stop();
            //tmRefresh.Enabled = false;

            try
            {
                btnRefrescar.Text = "Refrescar (" + iSegRefresco + ")";
            }
            catch { }

            DifFecha();

            //General.CargarPantalla("Form1", "Almacen", frMain, true);

            //tmRefresh.Enabled = true;
            //tmRefresh.Start();
            
        }

        private static void DifFecha()
        {
            DifFecha(false);
        }

        private static void DifFecha(bool bManual)
        {
            TimeSpan TimeDiff = DateTime.Now - dtRefresh;
            //int iSegDif = GnFarmacia.Sesion_MinRefresco + 60 - TimeDiff.Seconds;


            iSegRefresco -= 1;
            iSegDesconexion -= 1;


            if (iSegRefresco < 0)
            {
                iSegRefresco = 0;
            }

            if (iSegDesconexion < 0)
            {
                iSegDesconexion = 0;
            }

            try
            {
                btnRefrescar.Text = "Refrescar (" + iSegRefresco + ")";
            }
            catch { }

            IniciarIndicador_picture();

            if (iSegRefresco <= 0 || bManual)
            {
                iSegRefresco = GnFarmacia.Sesion_MinRefresco * 60;

                Refresh_ejecuta();

                if (!bErrorAlValidar)
                {
                    iSegDesconexion = GnFarmacia.Sesion_MinDesconexion * 60;
                }
            }

            if (iSegDesconexion <= 0)
            {
                bSesionActiva = false;

                tmRefresh.Enabled = false;
                tmRefresh.Close();
                tmRefresh.Stop();

                General.msjAviso("Sesión Caducada.");
                System.Windows.Forms.Application.DoEvents();

                Application.Exit();

            }
        }

        private static void IniciarIndicador_picture()
        {
            double dPorc = 0;

            if(!bInicializado)
            {
                bInicializado = true;
                frMain.MinimumSize = new System.Drawing.Size(800, 400);
            }

            try
            {
                btnRefrescar.Top = tstoolStrip.Top + 1;
            }
            catch { }

            try 
            {
                btnRefrescar.Height = tstoolStrip.Height - 2;
            }
            catch { }


            //btnRefrescar.Left = (frMain.Width - (int)((double)btnRefrescar.Width)) - 25;
            //btnRefrescar.Top = tstoolStrip.Top + 1;
            try 
            {
                btnRefrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }
            catch { }


            try
            {
                pcStatusComunicacion.Top = btnRefrescar.Top;
            }
            catch { }

            try
            {
                pcStatusComunicacion.Height = btnRefrescar.Height;
            }
            catch { }

            try 
            {
                pcStatusComunicacion.Width = iAncho;//tstoolStrip.Height - 1;
            }
            catch { }

            try 
            {
                pcStatusComunicacion.BackColor = cColor;//Color.Red;
            }
            catch { }

            try 
            { 
                pcStatusComunicacion.Left = (frMain.Width - (int)((double)btnRefrescar.Width) - (int)((double)pcStatusComunicacion.Width)) - 35;
            }
            catch { }

            //pcStatusComunicacion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            //pcStatusComunicacion.Anchor = AnchorStyles.Top | AnchorStyles.Right;



            //iAncho -= 25;

            dPorc = (iSegDesconexion * 1.00) / (GnFarmacia.Sesion_MinDesconexion * 60.00);

            iAncho = (Int32)((double)iAnchoMax * dPorc);

            cColor = Color.Green;

            if(dPorc < .75)
            {
                cColor = Color.Orange;
            }

            if (dPorc < .50)
            {
                cColor = Color.Yellow;
            }

            if (dPorc < .25)
            {
                cColor = Color.Red;
            }

        }

        private static void Refresh_ejecuta()
        {
            btnRefrescar.Enabled = false;
            tmRefresh.Enabled = false;
            tmRefresh.Stop();

            btnRefrescar.Enabled = false;
            btnRefrescar.Text = "Validando...";

            Ejecutar((int)Movimiento.Validar);

            btnRefrescar.Enabled = false;

            tmRefresh.Enabled = true;
            tmRefresh.Start();

            btnRefrescar.Enabled = true;
        }
        #endregion funciones y procedimientos privados
    }
}
