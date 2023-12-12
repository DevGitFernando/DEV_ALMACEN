using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Criptografia;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.FP;
using SC_SolutionsSystem.FP.Huellas;

namespace MA_Checador.Checador
{
    public partial class FrmChecador : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;        

        string sIdPersonal = "";
        bool bEntrada = false;
        bool bSalida = false;
        bool bGuardo = false;
        int iTipoRegistro = 0;

        public FrmChecador():this(0) 
        {
        }

        public FrmChecador(int TipoProceso)
        {
            InitializeComponent();

            this.ControlBox = false; 

            leer = new clsLeer(ref cnn);
            iTipoRegistro = TipoProceso;
        }

        private void FrmChecador_Load(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        #region Funciones
        private void LimpiaPantalla()
        {
            bEntrada = false;
            bSalida = false;            
        }

        private void FrmChecador_Shown(object sender, EventArgs e)
        {
            LevantaPantalla(iTipoRegistro); 
        }

        public void LevantaPantalla(int TipoRegistro)
        {
            if (iTipoRegistro == 1)
            {
                DtChecador.Entrada = 1; 
                tsmEntrada();
            }
            else
            {
                DtChecador.Salida = 1; 
                tsmSalida();
            }

            if (!bGuardo)
            {
                tmCerrarForma.Interval = 100;
                if (FP_General.HuellaCapturada)
                {
                    tmCerrarForma.Interval = 1500;
                }
            }
            else 
            {
                tmCerrarForma.Interval = 2500; 
            }

            tmCerrarForma.Enabled = true;
            tmCerrarForma.Start(); 
        }

        private void CargarDatosPersonal()
        {
            string sSql = "", sNombreFoto = "", sFotoPersonal = "";
            DateTime fechaNac = General.FechaSistema, fechaActual = General.FechaSistema;
            byte[] bytes;

            ////sIdPersonal = "00000004";
            sSql = string.Format(" Select *, GetDate() as FechaActual From vw_Personal (Nolock) Where IdPersonal = '{0}' ", sIdPersonal);
            
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDatosPersonal()");
                General.msjError("Ocurrio un error al obtener los datos de Personal");
            }
            else
            {
                if (leer.Leer())
                {
                    sNombreFoto = leer.Campo("NombreFotoPersonal");
                    sFotoPersonal = leer.Campo("FotoPersonal");
                    lblPersonal.Text = leer.Campo("NombreCompleto");
                    lblDepartamento.Text = leer.Campo("Departamento");
                    lblPuesto.Text = leer.Campo("Puesto");
                    fechaNac = leer.CampoFecha("FechaNacimiento");
                    fechaActual = leer.CampoFecha("FechaActual");

                    bytes = leer.CampoByte("FotoPersonal");

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
        #endregion Funciones

        #region Eventos_Menu 
        private void PersonalNoEncontrado()
        {
            bGuardo = false; 
            lblPersonal.Text = "Personal no encontrado";
        }

        private void tsmEntrada()
        {
            bGuardo = false;
            clsVerificarHuella f = new clsVerificarHuella();

            f.Titulo = "Registro de entrada"; 
            f.MostrarMensaje = false;
            f.Show();

            if (FP_General.HuellaCapturada)
            {
                if (!FP_General.ExisteHuella)
                {
                    PersonalNoEncontrado(); 
                }
                else 
                {
                    bEntrada = true;
                    iTipoRegistro = 1;
                    sIdPersonal = FP_General.Referencia_Huella;
                    GuardaInformacion(iTipoRegistro);
                }
            }           
                       
        }

        private void tsmSalida()
        {
            bGuardo = false;
            clsVerificarHuella f = new clsVerificarHuella();

            f.Titulo = "Registro de salida"; 
            f.MostrarMensaje = false;
            f.Show();
            
            if (FP_General.HuellaCapturada)
            {
                if (!FP_General.ExisteHuella)
                {
                    PersonalNoEncontrado(); 
                }
                else
                {
                    bSalida = true;
                    iTipoRegistro = 2;
                    sIdPersonal = FP_General.Referencia_Huella;
                    GuardaInformacion(iTipoRegistro);
                }
            }
           
        }
        #endregion Eventos_Menu

        #region GuardaInformacion
        private void GuardaInformacion(int TipoRegistro)
        {
            bool bContinua = true;
            string sSql = "";

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                sSql = string.Format(" Exec spp_Mtto_ChecadorPersonal '{0}', '{1}' ", sIdPersonal, TipoRegistro);

                if (!leer.Exec(sSql))
                {
                    bContinua = false;
                } 

                if (!bContinua)
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "GuardarInformacion");
                    //General.msjError("Ocurrio un error al guardar la informacion.");
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

        #region Timer
        private void tmCerrarForma_Tick(object sender, EventArgs e)
        {
            tmCerrarForma.Stop();
            tmCerrarForma.Enabled = false;

            if (iTipoRegistro == 1)
            {
                DtChecador.Entrada = 0;
            }
            else
            {
                DtChecador.Salida = 0;
            }

            this.Close();
        }
        #endregion Timer
    }
}
