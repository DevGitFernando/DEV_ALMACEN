using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using System.ServiceModel; 

using MA_Facturacion;
using Dll_MA_IFacturacion.Configuracion;
using Dll_MA_IFacturacion.XSA;

using SC_SolutionsSystem;

using MA_Facturacion.paxsvrRecepcionService; 

namespace MA_Facturacion
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


            General.DatosConexion.Servidor = "svrsii-central";
            General.DatosConexion.BaseDeDatos = "SII_Facturacion"; // "SII_OficinaCentral_Test";
            General.DatosConexion.Usuario = "sa";
            General.DatosConexion.Password = "1Bd660343964337fb81278a3a1d30b6f";
            General.DatosConexion.Puerto = "2433";
            General.DatosConexion.ConexionDeConfianza = false;           

            //////////////string xsaDireccionServicioTimbrado = "http://xsa5.factura-e.biz";
            //////////////////////xsaWebServices xsa = new xsaWebServices(xsaDireccionServicioTimbrado);
            //////////////Dll_IFacturacion.XSA.xsawsDescargarDocumentos docto = new xsawsDescargarDocumentos(xsaDireccionServicioTimbrado, General.DatosConexion);

            //////////////string sRFC = "PFA070614LD5";
            //////////////string sKey = "98706e390a7aeba1728ebeeb7ac2c488"; 

            //////////////docto.Descargar(sRFC, sKey, "GRO", "13"); 


            ////////try
            ////////{
            ////////    xsaFile.guardarDocumento("", "", "", "", ""); 
            ////////}
            ////////catch (Exception ex) 
            ////////{
            ////////    ex.Source = ex.Source;
            ////////}

            ////////xsawsCancelarDocumento docto = new xsawsCancelarDocumento(xsaDireccionServicioTimbrado, General.DatosConexion);
            ////////docto.CancelarDocumento("", "", "", "0"); 

            ////paxsvrRecepcionService.wcfRecepcionASMXSoapClient cliente = new paxsvrRecepcionService.wcfRecepcionASMXSoapClient();
            ////string retVal = cliente.fnEnviarXML("", "factura", 0, "WSDL_PAX", "wqfCr8O3xLfEhMOHw4nEjMSrxJnvv7bvvr4cVcKuKkBEM++/ke+/gCPvv4nvvrfvvaDvvb/vvqTvvoA=", "3.2");


            Application.Run(new FrmMain()); 
            
            ////FrmFactEmpresas f = new FrmFactEmpresas();
            ////////// Form1 f = new Form1();

            ////f.ShowInTaskbar = true;
            ////Application.Run(f); 

        }
    }
}
