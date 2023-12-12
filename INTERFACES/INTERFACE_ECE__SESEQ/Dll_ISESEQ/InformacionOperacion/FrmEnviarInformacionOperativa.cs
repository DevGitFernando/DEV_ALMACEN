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


namespace Dll_ISESEQ.InformacionOperacion
{
    public partial class FrmEnviarInformacionOperativa : FrmBaseExt 
    {

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        ////clsErrorManager error; 

        clsListView lst; 
        public FrmEnviarInformacionOperativa()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(GnDll_SII_SESEQ.DatosApp, this.Name); 
        }

        private void FrmEnviarInformacionOperativa_Load( object sender, EventArgs e )
        {
            IniciarPantalla();
        }

        #region Botones 
        private void IniciarPantalla()
        {
            Fg.IniciaControles(true);
            pgBar.Visible = false; 
            lblMensaje.Text = "Preparado para envio de información";
        }
        private void btnNuevo_Click( object sender, EventArgs e )
        {
            IniciarPantalla();
        }

        private void btnEnviarInformacion_Click( object sender, EventArgs e )
        {
            if(General.msjConfirmar("¿ Desea hacer el envio de la información operativa ?") == DialogResult.Yes)
            {
                thEnviarInformacion();
            }
        }
        #endregion Botones 

        #region Funciones y Procedimientos Privados 
        private void thEnviarInformacion()
        {
            System.Threading.Thread thEnviar = new System.Threading.Thread(EnviarInformacion);
            thEnviar.Name = "EnvioDeInformacionSESEQ";
            thEnviar.Start(); 
        }

        private void EnviarInformacion()
        {
            this.ControlBox = false;
            btnNuevo.Enabled = false;
            btnEnviarInformacion.Enabled = false; 

            lblMensaje.Text = "Enviando información";
            pgBar.Visible = true; 
            Application.DoEvents();

            EnviarInformacionOperacion responses = new EnviarInformacionOperacion(General.DatosConexion, "", "", "");
            responses.EnviarInformacion();

            lblMensaje.Text = "Envio de información concluido";
            pgBar.Visible = false;
            Application.DoEvents();

            btnNuevo.Enabled = true;
            btnEnviarInformacion.Enabled = true;
            this.ControlBox = true;

            General.msjAviso("Proceso terminado.");
        }
        #endregion Funciones y Procedimientos Privados 
    }
}
