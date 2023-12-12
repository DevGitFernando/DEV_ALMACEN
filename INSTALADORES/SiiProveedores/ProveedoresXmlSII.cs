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

namespace SiiProveedores
{
    [RunInstaller(true)]
    public class ProveedoresXmlSII : Installer
    {
        string sXmlUpdate = "SII_Update.xml";

        //string sDirReg = "SII_ADMINISTRACION_REGIONAL";
        //string sDirUni = "SII_ADMINISTRACION_UNIDAD";

        string sRoot = "";
        string sXmlModulo = "Proveedores.xml"; 
        string sDestinoCnn = ""; 
        string sServer = "";
        string sWebService = "";
        string sPagina = "";

        string sUpServer = "";
        string sUpWebService = "";
        string sUpPagina = ""; 

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
            // MessageBox.Show("Entre"); 

            // Retrieve configuration settings 
            sRoot = System.Environment.GetEnvironmentVariable("HOMEDRIVE");

            string targetDir = Context.Parameters["targetDir"];
            string targetCliente = Context.Parameters["targetCliente"]; 
            bool bEsRegional = targetCliente == "1";

            // sXmlModulo = Context.Parameters["XmlFile"];
            sServer = Context.Parameters["Server"];
            sWebService = Context.Parameters["WebService"];
            sPagina = Context.Parameters["Pagina"];


            sUpServer = Context.Parameters["uServer"];
            sUpWebService = Context.Parameters["uWebService"];
            sUpPagina = Context.Parameters["uPagina"];


            // MessageBox.Show(string.Format("{0}/{1}/{2}   {3}/{4}/{5}", sServer, sWebService, sPagina, sUpServer, sUpWebService, sUpPagina));

            ////if (bEsRegional)
            ////{
            ////    // SII_Update.xml 
            ////    targetDir = Path.Combine(targetDir, sDirReg);
            ////    sDestinoCnn = Path.Combine(sRoot, "\\" + sXmlRegional);
            ////}
            ////else
            ////{
            ////    targetDir = Path.Combine(targetDir, sDirUni);
            ////    sDestinoCnn = Path.Combine(sRoot, "\\" + sXmlUnidad);
            ////}


            // sDestinoCnn = Path.Combine(sRoot, "\\" + sXmlModulo);
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
                ////myDataTable.Columns.Add("ConexionWeb", System.Type.GetType("System.String"));
                ////myDataTable.Columns.Add("Servidor", System.Type.GetType("System.String"));
                ////myDataTable.Columns.Add("WebService", System.Type.GetType("System.String"));
                ////myDataTable.Columns.Add("PaginaAsmx", System.Type.GetType("System.String"));
                ////myDataTable.Columns.Add("IdEstado", System.Type.GetType("System.String"));
                ////myDataTable.Columns.Add("IdSucursal", System.Type.GetType("System.String"));

                object[] objValues = { "SI", sServer, sWebService, sPagina };
                myDataTable.Rows.Add(objValues);
                sDestinoCnn = Path.Combine(sRoot, "\\" + sXmlModulo);
            }

            if (Tipo == 2)
            {
                ////myDataTable.Columns.Add("ConexionWeb", System.Type.GetType("System.String"));
                ////myDataTable.Columns.Add("Servidor", System.Type.GetType("System.String"));
                ////myDataTable.Columns.Add("WebService", System.Type.GetType("System.String"));
                ////myDataTable.Columns.Add("PaginaAsmx", System.Type.GetType("System.String"));


                object[] objValues = { "SI", sUpServer, sUpWebService, sUpPagina };
                myDataTable.Rows.Add(objValues);

                sDestinoCnn = Path.Combine(sRoot, "\\" + sXmlUpdate);
                // MessageBox.Show(sUpServer + "   " + sUpWebService + "   " + sUpPagina);
            }

            pDataSetConfig.Tables.Add(myDataTable); 
            pDataSetConfig.WriteXml(sDestinoCnn); 
        }

        void Varaibles()
        {
            ////string sMsj = "";
            ////string sKey = "";

            ////////IDictionary x;
            ////////x = System.Environment.GetEnvironmentVariables();

            ////foreach (DictionaryEntry x in System.Environment.GetEnvironmentVariables())
            ////{
            ////    sKey = string.Format("Key: {0}   Value: {1}", x.Key, x.Value);
            ////    sMsj += sKey + "\t\n";
            ////}

            ////MessageBox.Show(sMsj);
        }
    }
}
