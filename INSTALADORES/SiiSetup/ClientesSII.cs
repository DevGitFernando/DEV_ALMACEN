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
    public class ClientesSII : Installer
    {
        ////string sRoot = "";
        ////string sDestinoCnn = "";

        string sLogo = "SII_Logo.jpg";
        string sLogoINT = "INT_Logo.jpg";
        string sLogoPFA = "PFA_Logo.jpg";

        string sDirectorio = "";



        string sXmlUpdate = "SII_Update.xml";
        //string sXmlRegional = "SII-Regional.xml";
        //string sXmlUnidad = "SII-Unidad.xml";

        //string sDirReg = "SII_ADMINISTRACION_REGIONAL";
        //string sDirUni = "SII_ADMINISTRACION_UNIDAD";

        //string sAdminReg = "Updater Admin Regional.exe";
        //string sAdminUni = "Updater Admin Unidad.exe";

        string sEstado = "00";
        string sUnidad = "0000";

        string sRoot = "";
        string sXmlModulo = "OficinaCentral.xml";
        string sDestinoCnn = "";
        string sServer = "";
        string sWebService = "";
        string sPagina = "";

        string sUpServer = "";
        string sUpWebService = "";
        string sUpPagina = "";

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

            sEstado = Context.Parameters["Estado"]; 
            sUnidad = Context.Parameters["Unidad"]; 
            ////sEstado = sEstado.Trim() == "" ? "00" : sEstado;
            ////sUnidad = sUnidad.Trim() == "" ? "0000" : sUnidad;

            // MessageBox.Show(string.Format(" {0}   {1} ", sEstado, sUnidad)); 

            sUpServer = Context.Parameters["uServer"]; 
            sUpWebService = Context.Parameters["uWebService"]; 
            sUpPagina = Context.Parameters["uPagina"]; 


            sDirectorio = @"SII_OFICINA_CENTRAL\"; 
            if (targerCliente == "2") 
            { 
                sDirectorio = @"SII_PUNTO_DE_VENTA\"; 
            }

            if (targerCliente == "3")
            {
                sDirectorio = @"SII_ALMACEN\";
            }

            if (targerCliente == "4")
            {
                sDirectorio = @"SII_FACTURACION\";
            }

            if (targerCliente == "5")
            {
                sDirectorio = @"SII_CHECADOR\";
            } 

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
            GenerarConexion(1);
            GenerarConexion(2); 
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


            if (Tipo == 1)
            {
                myDataTable.Columns.Add("IdEstado", System.Type.GetType("System.String"));
                myDataTable.Columns.Add("IdSucursal", System.Type.GetType("System.String"));
                object[] objValues = { "SI", sServer, sWebService, sPagina, "", "" };
                myDataTable.Rows.Add(objValues);
            }

            if (Tipo == 2)
            {
                object[] objValues = { "SI", sUpServer, sUpWebService, sUpPagina };
                myDataTable.Rows.Add(objValues);

                sDestinoCnn = Path.Combine(sRoot, "\\" + sXmlUpdate);
                // MessageBox.Show(sUpServer + "   " + sUpWebService + "   " + sUpPagina);
            }

            // MessageBox.Show(string.Format("{0}", sDestinoCnn)); 

            pDataSetConfig.Tables.Add(myDataTable);
            pDataSetConfig.WriteXml(sDestinoCnn);
        }
    }
}
