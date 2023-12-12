using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;
using SC_SolutionsSystem.SistemaOperativo; 

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;
////using DllTransferenciaSoft;
////using DllTransferenciaSoft.Servicio;

using Dll_IGPI.Interface;
using Dll_IGPI.Protocolos;

using System.IO; 
using System.IO.Ports;
using Microsoft.VisualBasic;

using Dll_IGPI.Configuracion; 

namespace Dll_IGPI
{
    public partial class FrmMain : Form
    {
        internal const int STX = 0x0002;
        internal const int ETX = 0x0003;
        internal const int DLE = 0x0010;

        // 14 
        string sNombrePuerto = "COM1"; 


        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login;
        bool bConexionWeb = false;
        bool bUsuarioLogeado = false;

        clsIniciarConSO SO; 
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        FrmNavegador Navegador;
        FrmParametros f;

        Thread thConexion;
        bool bConectando = false;
        bool bConexionEstablecida = false;
        bool bInicioTerminado = false; 

        // Arrancar servicio de transmision de informacion
        // FrmServicio Demonio;

        public FrmMain()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            btnLog.Visible = General.EsEquipoDeDesarrollo;
            this.rtxtLog.Clear();
            this.rtxtLog.Text = "";
            rtxtLog.Visible = btnLog.Visible;
            rtxtLog.Width = (int)((int)General.Pantalla.Ancho * 0.40);

            panel1.Width = (int)((int)General.Pantalla.Ancho * 0.40);

            groupBox_Protocolos.Visible = btnLog.Visible;
            panel1.Visible = btnLog.Visible; 


            clsAbrirForma.AssemblyActual("IGPI");
            GnFarmacia.CargarModulo("Dll_IGPI");
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo);

            General.IconoSistema = this.Icon;

            //CheckForIllegalCrossThreadCalls = false; 
            //General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo);
        }

        #region Funciones y Procedimientos Standar 
        private void IniciarIndicador_Conexion()
        {
            this.MinimumSize = new System.Drawing.Size(800, 400);
            pcStatusComunicacion.Height = toolStrip.Height - 1;
            pcStatusComunicacion.Width = toolStrip.Height - 1;
            pcStatusComunicacion.BackColor = Color.Red;

            pcStatusComunicacion.Left = (this.Width - (int)((double)pcStatusComunicacion.Width * 1.50)) - 2;
            pcStatusComunicacion.Top = toolStrip.Top + 1;

            ////pcStatusComunicacion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            pcStatusComunicacion.Anchor = AnchorStyles.Top| AnchorStyles.Right;
            pcStatusComunicacion.Visible = false;

            pcStatusComunicacion.Visible = IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP; 
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            rtxtLog.Visible = false;
            panel1.Visible = false;
            groupBox_Protocolos.Visible = false;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {


            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "IGPI";
            // MessageBox.BackColor = Global.FormaBackColor;

            // Ajustar el Tiempo de Espera para Conexion 
            General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.Limite30;

            IniciarIndicador_Conexion(); 
            if (!bUsuarioLogeado)
            {
                if (pfLoginServidor())
                {
                    // Cargar los Parametros del sistema 
                    IGPI.Parametros = new clsParametrosIGPI(General.DatosConexion, IGPI.DatosApp);
                    IGPI.Parametros.CargarParametros(); 

                    //IMach4_SerialPort.PuertoSerial = 2; 
                    IGPI.ServidorTPC_IP = IGPI.Parametros.GetValor("ServidorGPI");
                    IGPI.PuertoTCP_IP_Remoto = IGPI.Parametros.GetValorInt("PuertoTCP_IP_Remoto");
                    IGPI.PuertoTCP_IP_Local = IGPI.Parametros.GetValorInt("PuertoTCP_IP_Local");
                    IGPI.ProtocolVersion = IGPI.Parametros.GetValor("VersionGPI");
                    IGPI.EsMultipicking = IGPI.Parametros.GetValorBool("Multipicking");
                    IGPI.Protocolo_Comunicacion = (ProtocoloConexion_IGPI)IGPI.Parametros.GetValorInt("ProtocoloConexion");
                    IGPI.HabilitarLogDeTexto = IGPI.Parametros.GetValorBool("HabilitarLog");
                    IGPI.Habilitar_B__Al_Dispensar = IGPI.Parametros.GetValorBool("HabilitarB_AlDispensar");


                    IGPI.CountryCode = IGPI.Parametros.GetValor("CountryPhoneCode");
                    IGPI.TypeCode = IGPI.Parametros.GetValor("TypeCode");


                    //IMach4_Winsock.LongitudMensaje = IGPI.Parametros.GetValorInt("BloqueDeDatos");
                    IGPI_Winsock.Bloque__TCP_IP = (BloqueDeDatos_TCP_IP)IGPI.Parametros.GetValorInt("BloqueDeDatos");
                    IGPI_SerialPort.PuertoSerial = IGPI.Parametros.GetValorInt("PuertoCOM");
                    // IMach4_SerialPort.PuertoSerial = 1; 
                    IniciarIndicador_Conexion(); 

                    
                    if (!DtGeneral.EsEquipoDeDesarrollo)
                    {
                        FileInfo f = new FileInfo(Application.ExecutablePath); 
                        SO = new clsIniciarConSO(f.Name.Substring(0, f.Name.Length - 4), f.FullName);
                        if (IGPI.Parametros.GetValorBool("IniciarSO"))
                        {
                            SO.Agregar();
                        }
                        else
                        {
                            SO.Remover();
                        }
                    }

                    //// Poner la conexion en un hilo para evitar que se quede pasmado.
                    thAbrirConexion(); 
                }
            }
            General.ServidorEnRedLocal = true; 
        }

        private void thAbrirConexion()
        {
            bConectando = true;
            tmConexion.Enabled = true;
            tmConexion.Interval = 1500;
            tmConexion.Start();

            thConexion = new Thread(this.AbrirConexion);
            thConexion.Name = "Establecer_Comunicacion";
            thConexion.Start();
        }

        private void AbrirConexion()
        {
            bool bRegresa = false; 

            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.NINGUNO)
            {
                General.msjAviso("No se ha especificado un tipo válido de protocolo de comunicación.");
            }
            else
            {
                if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.SERIAL)
                {
                    bRegresa = IGPI_SerialPort.AbrirPuerto();
                }

                if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP)
                {
                    IGPI_Winsock.IndicadorDeComunicacion = pcStatusComunicacion;
                    IGPI_Winsock.Log_Comunicacion = rtxtLog; 
                    bRegresa = IGPI_Winsock.AbrirPuerto(IGPI.ServidorTPC_IP, IGPI.PuertoTCP_IP_Remoto, IGPI.PuertoTCP_IP_Local);
                }
            }

            bConectando = false;
            if (!bRegresa)
            {
                bConexionEstablecida = false;
                //General.msjError("No fue posible establecer comunicación con el Equipo Interface.\nComuniquese a Sistemas.");
                //Application.Exit();
            }
            else
            {
                bConexionEstablecida = true;

                // RevisarVersionModulos();

                //////if (!IGPI.EsInterface())
                //////{
                //////    Application.Exit();
                //////}
                //////else
                //////{
                //////    // CargarMenu(); 
                //////    tmHabiliar_R.Enabled = true;
                //////    tmHabiliar_R.Start();
                //////}
            }

            //return bRegresa; 
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
            string[] sModulos = { "IGPI", "Dll_IGPI" };
            DtGeneral.RevisarVersion(sModulos); 
        }

        private bool pfLoginServidor()
        {
            bool bRegresa = false;
            Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            Login.Arbol = "IM4";

            // Configuración Repositorio Central

            ActivarSysTray(); 
            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + IGPI.DatosApp.Modulo.Replace("Dll_", "") + " " + IGPI.DatosApp.Version;

            if (Login.AutenticarServicio())
            {
                DtGeneral.NombrePersonal = "ADMINISTRADOR";
                // BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + Application.ProductVersion;
                // BarraDeStatus.Panels[lblFarmacia.Name].Text = "Estado : " + DtGeneral.EstadoConectado + " -- " + DtGeneral.EstadoConectadoNombre + "      " + " Farmacia : " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
                BarraDeStatus.Panels[lblServidor.Name].Text = "Servidor : " + General.DatosConexion.Servidor;
                BarraDeStatus.Panels[lblBaseDeDatos.Name].Text = "Base de Datos : " + General.DatosConexion.BaseDeDatos;
                BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneral.NombrePersonal;

                icoSystray.Visible = false;

                bRegresa = true;

                // Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.Limite30; 

                IGPI.ReiniciarConexion(); 
                General.ArbolDeNavegacion = IGPI.Permisos(); 

                
                Navegador = new FrmNavegador();
                Navegador.MdiParent = this;
                Navegador.Permisos = General.ArbolDeNavegacion;
                //Navegador.ListaIconos = imgNavegacion_2;
                Navegador.ListaIconos = imgNavegacion;
                Navegador.Posicion = ePosicion.Izquierda;
                Login = null;
                Navegador.Show();

                this.Text = " Módulo : " + Navegador.NombreModulo + "         " + DtGeneral.EmpresaConectadaNombre;

            }
            else
            {
                Application.Exit(); // this.Close();
            }

            return bRegresa;
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

        private void btnLog_Click(object sender, EventArgs e)
        {
            rtxtLog.Visible = !rtxtLog.Visible;
            panel1.Visible = !panel1.Visible;
            groupBox_Protocolos.Visible = !groupBox_Protocolos.Visible; 
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (bInicioTerminado)
            {
                IniciarIndicador_Conexion();

                if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP)
                {
                    IGPI_Winsock.UpdateIndicadorComunicacion();
                }
            }


            if (this.WindowState == FormWindowState.Minimized)
            {
                mnOpciones.Items[btnAbrir.Name].Enabled = true;
                mnOpciones.Items[btnMinimizar.Name].Enabled = false;
                //// this.ShowInTaskbar = false;
            }
        }

        private void ActivarSysTray()
        {
            //icoSystray.Text = "SII_Dll_IGPI";
            //icoSystray.Icon = General.IconoSistema;

            //mnOpciones.Items[btnAbrir.Name].Enabled = false;
            //mnOpciones.Items[btnMinimizar.Name].Enabled = true;

            //icoSystray.Visible = false;
        }

        private void Restaurar()
        {
            this.Activate();
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = true;
            mnOpciones.Items[btnAbrir.Name].Enabled = false;
            mnOpciones.Items[btnMinimizar.Name].Enabled = true; 
        }

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
            ////if (!Transferencia.EjecutandoProcesos)
            ////{
            ////    ////Demonio.Close();
            ////    ////Demonio = null;
            ////    Application.Exit();
            ////}
        }

        private void icoSystray_DoubleClick(object sender, EventArgs e)
        {
            Restaurar();
        }

        private void tmHabiliar_R_Tick(object sender, EventArgs e)
        {
            tmHabiliar_R.Stop();
            tmHabiliar_R.Enabled = false;
            IGPI.Habilitar_R = true; 
            IGPI.Habilitar_A = true; 
            // IGPI.Habilitar_R = false;

            // Habilitar el Probar Comunicacion automaticamente 
            clsProbarConexion.TiempoEnSegundos = 1;
            clsProbarConexion.Iniciar();

            System.Threading.Thread.Sleep(1100);
            SolicitudProductos.TiempoEnSegundosDecimales = 0.5;
            SolicitudProductos.Iniciar();

            //clsProbarConexion.TiempoEnSegundos = 30;
            //clsProbarConexion.Iniciar();
        }        
        #endregion Funciones y Procedimientos Standar 

        #region Botones 

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool bTerminar = false;
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //if (!IMach4_SerialPort.Terminar())

                if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.SERIAL)
                {
                    bTerminar = IGPI_SerialPort.Terminar();
                }

                if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP)
                {
                    bTerminar = IGPI_Winsock.Terminar();
                }


                if (!bTerminar)
                {
                    e.Cancel = true;
                }
                else
                {
                    try
                    {
                        General.TerminarProceso("IGPI.exe");
                        Application.Exit();
                    }
                    catch
                    {
                    }
                    finally
                    {
                        Application.Exit();
                    }
                }
            }
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    ResetLog(); 
                    break; 

                case Keys.F10:
                    CargarParametros(); 
                    break; 

                case Keys.F12:
                    Enviar_R();
                    break; 
                
                default:
                    break;
            }
        }
        #endregion Botones 

        #region Log de Eventos
        private void ResetLog()
        {
            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.SERIAL)
            {
                IGPI_SerialPort.ResetLog();
            }

            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP)
            {
                IGPI_Winsock.ResetLog();
            }

            rtxtLog.Clear(); 
        }
        #endregion Log de Eventos 

        #region Parametros
        private void CargarParametros()
        {
            if (f == null)
            {
                f = new FrmParametros();
                f.SoloLectura = true;
                f.MdiParent = this; 
                f.Show(); 
            }
            else
            {
                General.CargarPantalla("FrmParametros", "Dll_IGPI", this);  
            } 
        }
        #endregion Parametros

        private void tmConexion_Tick(object sender, EventArgs e)
        {
            tmConexion.Stop();
            tmConexion.Enabled = false;

            if (!bConectando)
            {
                if (!bConexionEstablecida)
                {
                    General.msjError("No fue posible establecer comunicación con el Equipo Interface.\nComuniquese a Sistemas.");
                    Application.Exit();
                }
                else
                {
                    //// CargarMenu();
                    if (!IGPI.EsInterface())
                    {
                        Application.Exit();
                    }
                    else
                    {
                        // CargarMenu(); 
                        tmHabiliar_R.Enabled = true;
                        tmHabiliar_R.Start();
                    }
                }
            }
            else
            {
                tmConexion.Enabled = true;
                tmConexion.Start();
            }
        }

        #region Botones de Comando 
        private void btbRequest_A_Click(object sender, EventArgs e)
        {
            clsI_A_Request A = new clsI_A_Request();

            A.Dialogo = "A";
            A.Parametros.OrderNumber = txtOrden.Text;
            A.Parametros.RequestLocationNumber = txtPtoVta.Text;
            A.Parametros.DeliveryPort = txtPuerto.Text;
            A.Parametros.Priority = txtPrioridad.Text;
            A.Parametros.ProductCode = txtCodigoEAN.Text;
            A.Parametros.Quantity = txtCantidad.Text;
            A.Parametros.Flag = txtFlag.Text;
            A.Parametros.ID = txtID.Text;

            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.SERIAL)
            {
                IGPI_SerialPort.EnviarDatos(A.Solicitud);
            }

            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP)
            {
                IGPI_Winsock.EnviarDatos(A.Solicitud);
            }

            //IMach4_SerialPort.EnviarDatos(A.Solicitud); 

        }

        private void btbRequest_B_Click(object sender, EventArgs e)
        {
            clsI_B_Request B = new clsI_B_Request();

            B.Dialogo = "B";
            B.Parametros.RequestLocationNumber = txtPtoVta.Text;
            B.Parametros.ProductCode = txtCodigoEAN.Text;


            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.SERIAL)
            {
                IGPI_SerialPort.EnviarDatos(B.Solicitud);
            }

            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP)
            {
                IGPI_Winsock.EnviarDatos(B.Solicitud);
            }
        }

        private void btnRequest_S_Click(object sender, EventArgs e)
        {
            clsI_S_Request S = new clsI_S_Request("1");


            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.SERIAL)
            {
                IGPI_SerialPort.EnviarDatos(S.Solicitud);
            }

            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP)
            {
                IGPI_Winsock.EnviarDatos(S.Solicitud);
            }
        }

        private void btnRequest_O_Click(object sender, EventArgs e)
        {
            clsI_O_Request O = new clsI_O_Request("1");
            O.Parametros.OrderNumber = "1";

            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.SERIAL)
            {
                IGPI_SerialPort.EnviarDatos(O.Solicitud);
            }

            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP)
            {
                IGPI_Winsock.EnviarDatos(O.Solicitud);
            }
        }

        private void Enviar_R()
        {
            Dll_IGPI.Protocolos.clsI_R_Request r = new clsI_R_Request();
            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.SERIAL)
            {
                IGPI_SerialPort.EnviarDatos(r.Solicitud);
            }

            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP)
            {
                IGPI_Winsock.EnviarDatos(r.Solicitud);
            }
        }

        private void btnRequest_R_Click(object sender, EventArgs e)
        {
            Enviar_R(); 
        }
        #endregion Botones de Comando
    }
}
