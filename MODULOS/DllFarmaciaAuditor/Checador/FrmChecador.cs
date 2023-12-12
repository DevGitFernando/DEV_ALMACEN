using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.FP;
using SC_SolutionsSystem.FP.Huellas;

using DllFarmaciaAuditor;
using DllFarmaciaSoft;
using DllTransferenciaSoft;

namespace DllFarmaciaAuditor.Checador
{
    public partial class FrmChecador : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
        clsLeer leer;
        clsLeer leerChecador;
        clsLeer leerHuellas;
        clsLeer leerIntegrar;

        clsConsultas Consultas;       

        clsDatosCliente DatosCliente;
        DllFarmaciaSoft.wsFarmacia.wsCnnCliente checadorWeb = null;            

        string sUrlChecador = "";
        string sHost = "";
        string sIdPersonal = "";
        bool bGuardo = false;
        int iTipoRegistro = 0;
       
        public FrmChecador()
        {
            InitializeComponent();           

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            checadorWeb = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente();
            checadorWeb.Url = General.Url;
            leerChecador = new clsLeer(ref cnn);         

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            leerChecador = new clsLeer(ref cnn);            
            Consultas = new clsConsultas(General.DatosConexion, General.DatosApp, this.Name, false);
        }

        private void FrmChecador_Load(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        #region Botones
        private void btnEntrada_Click(object sender, EventArgs e)
        {
            if (Obtener_Url_Checador())
            {
                if (validarDatosDeConexion())
                {
                    ConexionChecador();
                    bGuardo = false;
                    clsVerificarHuella f = new clsVerificarHuella();
                    f.MostrarMensaje = false;
                    f.Show();

                    if (FP_General.HuellaCapturada)
                    {
                        if (FP_General.ExisteHuella)
                        {                            
                            iTipoRegistro = 1;
                            sIdPersonal = FP_General.Referencia_Huella;
                            GuardaInformacion(iTipoRegistro);

                            if (bGuardo)
                            {
                                btnEntrada.Enabled = false;
                                btnSalida.Enabled = false;
                                tmCerrarForma.Enabled = true;
                            }
                        }
                    }
                }
            }

            ConexionLocal();
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            if (Obtener_Url_Checador())
            {
                if (validarDatosDeConexion())
                {
                    ConexionChecador();
                    bGuardo = false;
                    clsVerificarHuella f = new clsVerificarHuella();
                    f.MostrarMensaje = false;
                    f.Show();

                    if (FP_General.HuellaCapturada)
                    {
                        if (FP_General.ExisteHuella)
                        {
                            iTipoRegistro = 2;
                            sIdPersonal = FP_General.Referencia_Huella;
                            GuardaInformacion(iTipoRegistro);

                            if (bGuardo)
                            {
                                btnEntrada.Enabled = false;
                                btnSalida.Enabled = false;
                                tmCerrarForma.Enabled = true;
                            }
                        }
                    }
                }
            }

            ConexionLocal();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            FrameFotografia.Visible = false;
            FrameDatos.Visible = false;
            pcFotografia.Image = null;
            tmCerrarForma.Enabled = false;
            iTipoRegistro = 0;
            bGuardo = false;
            sIdPersonal = "";
        }

        private bool Obtener_Url_Checador()
        {
            bool bRegresa = true;

            leer.DataSetClase = Consultas.Url_ChecadorUnidad("Obtener_Url_Checador");

            if (leer.Leer())
            {
                sUrlChecador = leer.Campo("UrlFarmacia");
                sHost = leer.Campo("Servidor");
            }
            else
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {                
                //leerWeb = new clsLeerWebExt(sUrlChecador, DtGeneral.CfgIniChecadorPersonal, DatosCliente);
                checadorWeb.Url = sUrlChecador;
                DatosDeConexion = new clsDatosConexion(checadorWeb.ConexionEx(DtGeneral.CfgIniChecadorPersonal));
                //DatosDeConexion = new clsDatosConexion(AbrirConexionEx(DtGeneral.CfgIniChecadorPersonal));
                DatosDeConexion.Servidor = sHost;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");                
            }

            return bRegresa;
        }               
        #endregion Funciones  
 
        #region Cargar_DatosPersonal
        private void CargarDatosPersonal()
        {
            string sSql = "", sNombreFoto = "", sFotoPersonal = "";
            DateTime fechaNac = General.FechaSistema, fechaActual = General.FechaSistema;
            byte[] bytes;
            

            sSql = string.Format(" Select *, GetDate() as FechaActual From vw_Personal (Nolock) Where IdPersonal = '{0}' ", sIdPersonal);

            if (!leerChecador.Exec(sSql))
            {
                Error.GrabarError(leerChecador, "CargarDatosPersonal()");
                General.msjError("Ocurrió un error al obtener los datos de Personal");
            }
            else
            {
                if (leerChecador.Leer())
                {
                    FrameFotografia.Visible = true;
                    FrameDatos.Visible = true;

                    sNombreFoto = leerChecador.Campo("NombreFotoPersonal");
                    sFotoPersonal = leerChecador.Campo("FotoPersonal");
                    lblPersonal.Text = leerChecador.Campo("NombreCompleto");
                    lblDepartamento.Text = leerChecador.Campo("Departamento");
                    lblPuesto.Text = leerChecador.Campo("Puesto");
                    fechaNac = leerChecador.CampoFecha("FechaNacimiento");
                    fechaActual = leerChecador.CampoFecha("FechaActual");

                    bytes = leerChecador.CampoByte("FotoPersonal");

                    if (sNombreFoto.Trim() != "")
                    {
                        CargarFotoPersonal(bytes);
                    }

                    if (iTipoRegistro == 1)
                    {
                        lblMensaje.Text = "BIENVENIDO";
                    }
                    else
                    {
                        lblMensaje.Text = "HASTA LUEGO";
                    }

                    if (fechaNac.Month == fechaActual.Month && fechaNac.Day == fechaActual.Day)
                    {
                        lblAños.Visible = true;
                        lblAños.Text = "FELIZ CUMPLEAÑOS";
                    }
                }
            }
        }       

        private void CargarFotoPersonal(byte[] bytes)
        {
            IntPtr intr = new IntPtr(0);

            MemoryStream ms = new MemoryStream(bytes);
            Image returnImage = Image.FromStream(ms);

            pcFotografia.Image = returnImage;
        }
        #endregion Cargar_DatosPersonal

        #region GuardaInformacion
        private void GuardaInformacion(int TipoRegistro)
        {
            bool bContinua = true;
            string sSql = "";

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                sSql = string.Format(" Exec spp_Mtto_ChecadorPersonal '{0}', '{1}' ", sIdPersonal, TipoRegistro);

                if (!leerChecador.Exec(sSql))
                {
                    bContinua = false;
                }

                if (!bContinua)
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leerChecador, "GuardarInformacion");
                    //General.msjError("Ocurrió un error al guardar la Información.");
                }
                else
                {
                    cnn.CompletarTransaccion();
                    //General.msjUser(""); //Este mensaje lo genera el SP
                    CargarDatosPersonal();
                    bGuardo = true;
                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso(General.MsjErrorAbrirConexion);
            }

        }
        #endregion GuardaInformacion

        #region Conexiones
        private void ConexionChecador()
        {
            cnn = new clsConexionSQL(DatosDeConexion);
            leerChecador = new clsLeer(ref cnn);
            FP_General.Conexion = DatosDeConexion;
        }

        private void ConexionLocal()
        {
            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
        }
        #endregion Conexiones

        #region Evento_Timer
        private void tmCerrarForma_Tick(object sender, EventArgs e)
        {
            LimpiaPantalla();
            btnEntrada.Enabled = true;
            btnSalida.Enabled = true;
        }
        #endregion Evento_Timer        
    }
}
