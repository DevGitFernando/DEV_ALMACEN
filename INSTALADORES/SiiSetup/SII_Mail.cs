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

namespace SiiSetup
{
    [RunInstaller(true)]
    public class SII_Mail : Installer
    {
        ////string sRoot = "";
        ////string sDestinoCnn = "";

        string sLogo = "SII_Logo.jpg";
        string sLogoINT = "INT_Logo.jpg";
        string sLogoPFA = "PFA_Logo.jpg";

        string sDirectorio = "";

        string sRoot = "";
        string sXmlModulo = "SII-Mail.xml";
        string sDestinoCnn = "";
        string sServer = "";
        string sWebService = "";
        string sPagina = "";

        string targetCompany = ""; 
        string targerCliente = ""; 

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            //string targetDir = Context.Parameters["targetDir"];
            //string targetCliente = Context.Parameters["targetCliente"];
            //bool bEsRegional = targetCliente == "1";

            // Retrieve configuration settings
            targetCompany = Context.Parameters["Company"]; 
            targerCliente = Context.Parameters["targetCliente"];  
            sRoot = Context.Parameters["targetDir"]; // System.Environment.GetEnvironmentVariable("HOMEDRIVE");

            //// Parametros iniciales 
            sXmlModulo = Context.Parameters["XmlFile"]; 
            sServer = Context.Parameters["Server"]; 
            sWebService = Context.Parameters["WebService"]; 
            sPagina = Context.Parameters["Pagina"]; 

            // MessageBox.Show(string.Format(" {0}/{1}/{2} ", sServer, sWebService, sPagina)); 

            sDirectorio = @"SII-MAIL\"; 

            // Determinar las Rutas de Acceso 
            sRoot = sRoot + @"\" + sDirectorio; 
            sLogo = sRoot + @"\" + sLogo;
            sLogoINT = sRoot + @"\" + sLogoINT;
            sLogoPFA = sRoot + @"\" + sLogoPFA;

            //MessageBox.Show(sRoot);
            //MessageBox.Show(sLogo + "   " + sLogoINT + "   " + sLogoPFA); 


            // Copiar los archivos de Fondo 
            try
            {
                // Borrar el Logo Base 
                File.Delete(sLogo);

                if (targetCompany == "1") 
                { 
                    File.Copy(sLogoINT, sLogo); 
                } 
                else 
                { 
                    File.Copy(sLogoPFA, sLogo);  
                } 

                File.Delete(sLogoINT); 
                File.Delete(sLogoPFA); 
            }
            catch { }


            sRoot = System.Environment.GetEnvironmentVariable("HOMEDRIVE");
            sDestinoCnn = Path.Combine(sRoot, "\\" + sXmlModulo);

            // MessageBox.Show(string.Format("{0}   {1}", sRoot, "")); 
            GenerarConexion(); 
        }

        void GenerarConexion()
        {
            DataSet pDataSetConfig = new DataSet("SC_Solutions");
            DataTable myDataTable = new DataTable("Configuracion");
            DataColumn myDataColumn = new DataColumn();

            //myDataTable = pDataSetConfig.Tables["Configuracion"]; 
            myDataTable.Columns.Add("ConexionWeb", System.Type.GetType("System.String"));
            myDataTable.Columns.Add("Servidor", System.Type.GetType("System.String"));
            myDataTable.Columns.Add("WebService", System.Type.GetType("System.String"));
            myDataTable.Columns.Add("PaginaAsmx", System.Type.GetType("System.String"));
            myDataTable.Columns.Add("UltimoUsuarioConectado", System.Type.GetType("System.String")); 

            object[] objValues = { "SI", sServer, sWebService, sPagina, "" };
            myDataTable.Rows.Add(objValues);

            pDataSetConfig.Tables.Add(myDataTable);
            pDataSetConfig.WriteXml(sDestinoCnn);
        }
    }
}
