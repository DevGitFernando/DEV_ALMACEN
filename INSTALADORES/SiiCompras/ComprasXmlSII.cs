using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Install;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Data;

using Microsoft.VisualBasic; 

namespace ComprasXmlSII
{
    [RunInstaller(true)]
    public class ProveedoresXmlSII : Installer
    {
        string sXmlUpdate = "SII_Update.xml";

        string sDirReg = "SII_COMPRAS"; 
        //string sDirUni = "SII_COMPRAS";

        string sRoot = "";
        string sXmlModulo = "Compras.xml";
        string sXmlModuloAux = "ComprasRegional.xml";

        string sEstado = "";
        string sUnidad = ""; 

        string sDestinoCnn = ""; 
        string sServer = "";
        string sWebService = "";
        string sPagina = "";

        string sUpServer = "";
        string sUpWebService = "";
        string sUpPagina = "";

        string sLogo = "SII_Logo.jpg";
        string sLogoINT = "INT_Logo.jpg";
        string sLogoPFA = "PFA_Logo.jpg";
        string sLogo_Fusion = "INT_PFA_Logo.jpg";

        //string sDirectorio = "";
        string targetDir = "";
        string targetCliente = "";
        bool bEsCentral = false;
        string targetCompany = "";
        //string targerCliente = ""; 
        // /targetDir="[TARGETDIR]\" /targetCliente="[TPOCOMPRAS]\" /Server="[SERVIDOR]" /WebService="[WEBSERVICE]" /Pagina="[PAGINA]"    /uServer="[USERVIDOR]" /uWebService="[UWEBSERVICE]" /uPagina="[UPAGINA]" /Estado="[ESTADO]" /Unidad="[UNIDAD]" 

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
            // MessageBox.Show("Entre"); 

            // Retrieve configuration settings 
            sRoot = System.Environment.GetEnvironmentVariable("HOMEDRIVE");

            targetDir = Context.Parameters["targetDir"];
            targetCompany = Context.Parameters["Company"];
            targetCliente = Context.Parameters["targetCliente"]; 
            bEsCentral = targetCliente == "1";  

            sEstado = PonCeros(Context.Parameters["Estado"], 2); 
            sUnidad = PonCeros(Context.Parameters["Unidad"], 4); 

            // sXmlModulo = Context.Parameters["XmlFile"];
            sServer = Context.Parameters["Server"];
            sWebService = Context.Parameters["WebService"];
            sPagina = Context.Parameters["Pagina"];


            sUpServer = Context.Parameters["uServer"];
            sUpWebService = Context.Parameters["uWebService"];
            sUpPagina = Context.Parameters["uPagina"]; 

            // MessageBox.Show(string.Format("{0}/{1}/{2}   {3}/{4}/{5}", sServer, sWebService, sPagina, sUpServer, sUpWebService, sUpPagina));

            if (bEsCentral)
            {
                sXmlModulo = "Compras.xml";
                sXmlModuloAux = "ComprasRegional.xml";
                // sDestinoCnn = Path.Combine(sRoot, "\\" + sXmlRegional); 
            }
            else
            {
                sXmlModulo = "ComprasRegional.xml";
                sXmlModuloAux = "Compras.xml"; 
            }

            //// Compras Central se habilitan ambos modulos 
            if (!bEsCentral)
            {
                // Quitar el Modulo que no se solicito 
                File.Delete(Path.Combine(targetDir, sXmlModuloAux));
            }

            // sDestinoCnn = Path.Combine(sRoot, "\\" + sXmlModulo);
            GenerarConexion(1);
            GenerarConexion(2);
            GenerarConexion(3);

            CrearLogo(); 
        }

        public string PonCeros(string prtDato, int prtLargo)
        {
            string sCadena = "";

            try
            {
                sCadena = Strings.StrDup(prtLargo, "0").ToString() + prtDato.Trim();   //"000000000000000000000000000000";
            }
            catch { }
            //string sValor = sCadena + prtDato.Trim();


            string sRegresa = Strings.Right(sCadena, prtLargo); // Right(sCadena, prtLargo);
            return sRegresa;
        }

        void CrearLogo()
        {
            //// Determinar las Rutas de Acceso 
            //sRoot = sRoot + @"\" + sDirectorio;
            sRoot = targetDir + @"\" + sDirReg; 
            sLogo = sRoot + @"\" + sLogo;
            sLogoINT = sRoot + @"\" + sLogoINT;
            sLogoPFA = sRoot + @"\" + sLogoPFA;
            sLogo_Fusion = sRoot + @"\" + sLogo_Fusion; 

            // Borrar el Logo Base 
            File.Delete(sLogo); 

            if (targetCompany == "1")
            {
                File.Copy(sLogoINT, sLogo);
            }

            if (targetCompany == "2")
            {
                File.Copy(sLogoPFA, sLogo);
            }

            if (targetCompany == "3")
            {
                File.Copy(sLogo_Fusion, sLogo);
            }

            File.Delete(sLogoINT);
            File.Delete(sLogoPFA);
            File.Delete(sLogo_Fusion);
        }

        void GenerarConexion(int Tipo)
        {
            DataSet pDataSetConfig = new DataSet("SC_Solutions");
            DataTable myDataTable = new DataTable("Configuracion");
            DataColumn myDataColumn = new DataColumn();

            //myDataTable = pDataSetConfig.Tables["Configuracion"]; 
            myDataTable.Columns.Add("ConexionWeb", System.Type.GetType("System.String"));
            myDataTable.Columns.Add("Servidor", System.Type.GetType("System.String"));
            myDataTable.Columns.Add("WebService", System.Type.GetType("System.String"));
            myDataTable.Columns.Add("PaginaAsmx", System.Type.GetType("System.String"));
            myDataTable.Columns.Add("IdEstado", System.Type.GetType("System.String"));
            myDataTable.Columns.Add("IdSucursal", System.Type.GetType("System.String"));


            if (Tipo == 1)
            {
                object[] objValues = { "SI", sServer, sWebService, sPagina, sEstado, sUnidad };
                myDataTable.Rows.Add(objValues);
                sDestinoCnn = Path.Combine(sRoot, "\\" + sXmlModulo);
            }

            if (Tipo == 2)
            {
                object[] objValues = { "SI", sServer, sWebService, sPagina, sEstado, sUnidad };
                myDataTable.Rows.Add(objValues);
                sDestinoCnn = Path.Combine(sRoot, "\\" + sXmlModuloAux);
            }

            if (Tipo == 3)
            {
                object[] objValues = { "SI", sUpServer, sUpWebService, sUpPagina, sEstado, sUnidad };
                myDataTable.Rows.Add(objValues);

                sDestinoCnn = Path.Combine(sRoot, "\\" + sXmlUpdate);
                // MessageBox.Show(sUpServer + "   " + sUpWebService + "   " + sUpPagina);
            }

            pDataSetConfig.Tables.Add(myDataTable); 
            pDataSetConfig.WriteXml(sDestinoCnn); 
        }
    }
}
