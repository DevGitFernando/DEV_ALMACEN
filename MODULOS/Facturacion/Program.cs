using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;

//using System.ServiceModel; 

using Facturacion;
using Dll_IFacturacion.Configuracion;
using Dll_IFacturacion.XSA;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using Facturacion.paxsvrRecepcionService;

using Dll_IFacturacion; 
using Dll_IFacturacion.CFDI;
using Dll_IFacturacion.CFDI.CFDFunctions;
using Dll_IFacturacion.CFDI.CFDFunctionsEx;
using Dll_IFacturacion.CFDI.geCFD;
using Dll_IFacturacion.CFDI.geCFD_33;

using Dll_IFacturacion.SAT; 

namespace Facturacion
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


            General.DatosConexion.Servidor = @"LapJesus\SQL_2K14";
            General.DatosConexion.BaseDeDatos = "SII_Facturacion_OP21_Puebla"; // "SII_OficinaCentral_Test";
            General.DatosConexion.Usuario = "sa";
            General.DatosConexion.Password = "1234";
            General.DatosConexion.Puerto = "1733";
            General.DatosConexion.ForzarImplementarPuerto = true;  
            General.DatosConexion.ConexionDeConfianza = false;

            ////clsValidarCFDI validar = new clsValidarCFDI();

            ////validar.Validar("DE5B845E-74EE-4383-BBC4-7683F891266F"); 

            ////Test___NotaDeCredito(); 



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


        private static void Test___NotaDeCredito()
        {
            bool bRegresa = false;
            long pkResult = 0;
            string sSql = string.Format("Exec {0} @Identificador = {1} ", " spp_FACT_CFDI_NotaDeCredito_ObtenerDatos ", 4);

            clsGenCFDI_33_NotaDeCredito CFDI_FD = null; //new clsGenCFDI_Ex();
            clsGenCFDI_33_NotaDeCredito myCFDI = null; //new clsGenCFDI_Ex();

            clsConexionSQL cnnCFDI = new clsConexionSQL(General.DatosConexion);
            clsLeer leerCFDI = new clsLeer(ref cnnCFDI);

            bRegresa = leerCFDI.Exec(sSql);
            if (!bRegresa)
            {
                sSql = leerCFDI.MensajeError;
            }


            if (bRegresa)
            {
                CFDI_FD = new clsGenCFDI_33_NotaDeCredito();
                CFDI_FD.Conexion = cnnCFDI;
                CFDI_FD.myComprobante.DatosComprobante = leerCFDI.DataSetClase;

                if (bRegresa)
                {
                    CFDI_FD.EsVistaPrevia = false; ;
                    CFDI_FD.PAC = PACs_Timbrado.VirtualSoft;
                    pkResult = CFDI_FD.obtenerFacturaElectronica(CFDI_FD.myComprobante);
                    bRegresa = pkResult > 0;
                }
            }
        }
    }
}
