using System;
using System.Collections.Generic;
using System.Data; 
using System.DirectoryServices;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Drawing; 
using Microsoft.VisualBasic;


using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;

using Farmacia.Compras;
using Farmacia.Transferencias;
using Farmacia.Ventas;
using Farmacia.Inventario;
using Farmacia.AlmacenJuris;
using Farmacia.PedidosDeDistribuidor; 

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Reporteador;

using DllTransferenciaSoft;
using DllTransferenciaSoft.ExportarBD; 
using DllTransferenciaSoft.IntegrarBD;
using DllTransferenciaSoft.Zip;
using SC_SolutionsSystem.FTP;
using DllFarmaciaSoft.SistemaOperativo; 

//using System.Web.Util;
//using Chilkat;

using SC_SolutionsSystem.SQL; 

using System.Diagnostics;

using Farmacia.VentasDispensacion; 

namespace Farmacia
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string sMoneda = General.LetraMoneda(21.00); 

            ////////////////General.DatosConexion.Servidor = "localhost";
            ////////////////General.DatosConexion.BaseDeDatos = "SII_21_0191"; // "SII_OficinaCentral_Test";
            ////////////////General.DatosConexion.Usuario = "sa";
            ////////////////General.DatosConexion.Password = "1234";
            ////////////////General.DatosConexion.ConexionDeConfianza = false;           


            ////////////////DtGeneral.EmpresaConectada = "001"; 
            ////////////////DtGeneral.EstadoConectado = "25"; 
            ////////////////DtGeneral.FarmaciaConectada = "0008"; 
            ////////////////DtGeneral.ClaveRENAPO = "SL"; 
            ////////////////DtGeneral.IdPersonal = "0001";
            ////////////////DtGeneral.RutaReportes = @"D:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES";

            //SC_SolutionsSystem.SQL.clsSQL z = new clsSQL(General.DatosConexion, "PtoCenReg", @"C:\RESPALDOS");
            //z.BackUp.Respaldar();
            //z.LogBd.ReducirBD();
            //z.LogBd.ReducirLog(); 

            // General.AbrirDocumento("http://pl-consultas.dyndns-ip.com/wsCliente/Archivos/Rol.pdf"); 


            string s = "";
            s = GenerarMD5(Transferencia.Modulo + "Install");
            s = GenerarMD5(Transferencia.Modulo + "ServicioRemoto");
            s = GenerarMD5("Updater");

            //General.FormaBackColor = Color.FromArgb(62, 106, 170);
            //General.FormaBackColor = Color.FromArgb(160, 160, 160); 
            //General.FormaBackColor = Color.FromArgb(191, 219, 255); 
            //General.BackColorBarraMenu = Color.WhiteSmoke; 

            //////if (clsPing.InternetIsAlive())
            //////{
            //////    General.msjError("Conexion inactiva");
            //////}
            //////else
            //////{
            //////    General.msjUser("Conexion activa");
            //////}

            ////////FrmColorProductosIMach f = new FrmColorProductosIMach();
            ////////f.ShowInTaskbar = true;
            ////////f.ShowDialog(); 


            //////// Punto de Entrada        
            if (ConfiguracionRegional.Revisar())
            {
                Application.Run(new FrmMain());
            } 

            ////////////clsVersionesTeamViewer team = new clsVersionesTeamViewer();
            ////////////team.Obtener_IDS();
            ////////////DataSet dts = team.ListaID_TV; 


            //////////////////////FrmConfigIntegrarDB f = new FrmConfigIntegrarDB();
            ////////FrmExportarBD f = new FrmExportarBD();
            ////FrmPDD_01_Dispensacion f = new FrmPDD_01_Dispensacion();
            ////f.ShowInTaskbar = true; 
            ////f.ShowDialog();

        }


        private static string GenerarMD5(string Cadena)
        {
            string sMD5 = "";
            byte[] bytesCadena, bytesRegresa;
            MD5CryptoServiceProvider MD5Crypto = new MD5CryptoServiceProvider();

            bytesCadena = System.Text.Encoding.UTF8.GetBytes(Cadena);
            bytesRegresa = MD5Crypto.ComputeHash(bytesCadena);

            sMD5 = BitConverter.ToString(bytesRegresa);
            sMD5 = sMD5.Replace("-", "").ToLower();

            return sMD5;
        }

        public static int CreateWebsite(string webserver, string serverComment, string serverBindings, string homeDirectory)
        {
            DirectoryEntry w3svc = new DirectoryEntry("IIS://localhost/w3svc");

            //Create a website object array
            object[] newsite = new object[] { serverComment, new object[] { serverBindings }, homeDirectory };

            //invoke IIsWebService.CreateNewSite
            object websiteId = (object)w3svc.Invoke("CreateNewSite", newsite);

            return (int)websiteId;

        }
    }
}
