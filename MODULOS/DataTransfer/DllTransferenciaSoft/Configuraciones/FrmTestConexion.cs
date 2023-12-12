using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FTP;

namespace DllTransferenciaSoft.Configuraciones
{
    public partial class FrmTestConexion : FrmBaseExt
    {
        int iTipoConexion = 1; 
        string sDireccion = "";
        string sHost = "";
        string sUser = "";
        string sPassword = "";
        bool bModoActivoDeTransferencia = false; 

        public FrmTestConexion(string Direccion)
        {
            InitializeComponent();

            iTipoConexion = 1;
            this.Text += " web ";  
            this.sDireccion = Direccion;
        }
         
        public FrmTestConexion(string HostFTP, string UserFTP, string Password, bool ModoActivoDeTransferencia )
        {
            InitializeComponent();

            iTipoConexion = 2;
            this.Text += " ftp ";  

            sHost = HostFTP;
            sUser = UserFTP;
            sPassword = Password;
            bModoActivoDeTransferencia = ModoActivoDeTransferencia; 
        }

        private void TestConexion_WEB()
        {
            // string sDireccion = ""; // "http://" + txtServidor.Text.Trim() + "/" + txtWebService.Text.Trim() + "/" + txtPagina.Text.Trim() + ".asmx";
            DllTransferenciaSoft.wsFarmaciaSoftGn.wsConexion myCnn = new DllTransferenciaSoft.wsFarmaciaSoftGn.wsConexion();

            myCnn.Url = sDireccion.ToLower().Replace(".asmx", "") + ".asmx";
            myCnn.Timeout = 7500;

            try
            {
                myCnn.TestConection();
                General.msjUser("Conexión establecida satisfactoriamente.");
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source; 
                General.msjUser("Conexión no establecida.");
            }

            this.Hide();
        }

        private void TestConexion_FTP()
        {
            clsFTP ftp = new clsFTP(sHost, sUser, sPassword, false, bModoActivoDeTransferencia); 
            bool bRegresa = false;
            string sError = ""; 

            try
            {
                bRegresa = ftp.ProbarConexion(); 
            }
            catch (Exception ex)
            {
                sError = ex.Message; 
            }

            if (bRegresa)
            {
                General.msjUser("Conexión establecida satisfactoriamente.");
            }
            else
            {
                General.msjUser("Conexión no establecida.\n\n" + sError);  
            }

            this.Hide(); 
        }

        private void FrmTestConexion_Load(object sender, EventArgs e)
        {
            tmTest.Start();
        }

        private void tmTest_Tick(object sender, EventArgs e)
        {
            tmTest.Stop();
            tmTest.Enabled = false;

            if (iTipoConexion == 1)
            {
                TestConexion_WEB();
            }
            else 
            {
                TestConexion_FTP(); 
            }
        }        
    }
}
