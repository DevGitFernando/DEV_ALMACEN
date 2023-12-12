using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Windows.Forms;

using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;

using Microsoft.VisualBasic;

using System.Text;
using System.IO;
using System.Configuration;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;


using DllFarmaciaSoft; 
using DllFarmaciaSoft.ExportarExcel;

using Dll_ISESEQ;
using Dll_ISESEQ.wsClases;
using Dll_ISESEQ.InformacionOperacion;

namespace Dll_ISESEQ.InformacionOperacion
{
    public partial class FrmEntregaDeColectivos : FrmBaseExt 
    {

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        ////clsErrorManager error; 

        public bool EntregaValidada = false;
        string sFolioSESEQ = "";
        string sFolioRecetaColectivo = ""; 

        clsListView lst;
        public FrmEntregaDeColectivos(string FolioSESEQ, string FolioColectivo)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            sFolioSESEQ = FolioSESEQ;
            sFolioRecetaColectivo = FolioColectivo;

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(GnDll_SII_SESEQ.DatosApp, this.Name);

            lblMensaje.Visible = false;
            lblMensaje.Top = pgBar.Top;
            lblMensaje.Left = pgBar.Left;
            lblMensaje.Width = pgBar.Width; 
        }

        private void FrmEntregaDeColectivos_Load( object sender, EventArgs e )
        {
            IniciarPantalla();
        }

        private void IniciarPantalla()
        {
            Fg.IniciaControles();
            FrameProceso.Visible = false; 

            txtFolioRecetaColectivo.Focus(); 
        }

        #region Funciones y Procedimientos Privados 
        private void thEnviarInformacion()
        {
            System.Threading.Thread thEnviar = new System.Threading.Thread(SolicitarValidacionDeFirma);
            thEnviar.Name = "SolicitarValidacionDeFirmaSESEQ";
            thEnviar.Start(); 
        }

        private void SolicitarValidacionDeFirma()
        {
            this.ControlBox = false;
            bool bRegresa = false; 

            btnCancelar.Enabled = false;
            btnAceptar.Enabled = false;

            //lblMensaje.Text = "Enviando información";
            FrameProceso.Visible = true; 
            pgBar.Visible = true; 
            Application.DoEvents();

            //EnviarInformacionOperacion responses = new EnviarInformacionOperacion(General.DatosConexion, "", "", "");
            //responses.EnviarInformacion();

            ResponseAcuseXML responses = new ResponseAcuseXML(General.DatosConexion, DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            bRegresa = responses.ValidarFirmaDeEntregaColectivo(sFolioSESEQ, sFolioRecetaColectivo, txtFolioRecetaColectivo.Text.Trim());

            lblMensaje.Text = !bRegresa ? "Clave inválida" : "";

            //lblMensaje.Text = "Envio de información concluido";
            pgBar.Visible = false;
            FrameProceso.Visible = !bRegresa;
            lblMensaje.Visible = !bRegresa;
            Application.DoEvents();

            btnCancelar.Enabled = true;
            btnAceptar.Enabled = true;
            this.ControlBox = true;

            if (bRegresa)
            {
                EntregaValidada = true;
                this.Close();
            }

            ////General.msjAviso("Proceso terminado.");
        }
        #endregion Funciones y Procedimientos Privados 

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            thEnviarInformacion();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide(); 
        }
    }
}
