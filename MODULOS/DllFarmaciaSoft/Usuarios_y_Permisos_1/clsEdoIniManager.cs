using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Security.Permissions;

using SC_SolutionsSystem;

namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    /// <summary>
    /// Clase que se encarga del manejo del archivo de configuración de la aplicacion.
    /// </summary>
    // [FileIOPermissionAttribute(SecurityAction.Demand,  Name = "FullTrust")] 
    public class clsEdoIniManager
    {
        #region Declaracion de variables
        private DataSet pDataSetConfig = new DataSet();
        private string strFilePath = General.UnidadSO + "://" + General.ArchivoIni.Substring(0, General.ArchivoIni.Length - 4) + ".xml";
        private DataColumn myDataColumn; 
        private DataRow myDataRow;
        private DataTable myDataTable;
        private FrmFileConfigXml Frm;

        private bool blExisteArchivo = false;
        private bool bXmlEnDirectorioApp = false;
        private int iItem_Selected = 0; 
        #endregion

        #region Constructores de clase y destructor
        public clsEdoIniManager ()
        {
            pDataSetConfig = new DataSet("SC_Solutions");
            //ExisteArchivo();
        }

        ~clsEdoIniManager()
        {
            pDataSetConfig = null;
            myDataColumn = null;
            myDataRow = null;
            myDataTable = null;
        }
        #endregion

        /// <summary>
        /// Obtiene una clave del archivo de configuracion.
        /// </summary>
        /// <param name="strKey">Clave a buscar, si no existe la agrega al archivo de configuracion.</param>
        /// <returns></returns> 

        public string XmlFile
        {
            get { return strFilePath; } 
        }

        public bool XmlEnDirectorioApp
        {
            get { return bXmlEnDirectorioApp; }
            set { bXmlEnDirectorioApp = value; }
        }

        public int Item_Seleccionado
        {
            get { return iItem_Selected + 1; }
            set
            {
                iItem_Selected = value - 1;
                if (iItem_Selected < 0)
                {
                    iItem_Selected = 0; 
                }
            }
        }

        public string GetValues(string strKey)
        {
            return GetValues(strKey, ""); 
        }

        private void ValidarRows()
        {
            try
            {
                if (iItem_Selected > pDataSetConfig.Tables[0].Rows.Count - 1)
                {
                    iItem_Selected = pDataSetConfig.Tables[0].Rows.Count - 1; //.(myColumn.ColumnName);
                }
            }
            catch { }
        }

        public string GetValues(string strKey, string prtValue)
        {
            string sRegresa = "";
            bool bExiste = false;
            DataRow dtRow;

            ValidarRows(); 

            foreach (DataColumn myColumn in pDataSetConfig.Tables[0].Columns)
            {
                if (myColumn.ColumnName.ToString().ToUpper() == strKey.ToString().ToUpper())
                {
                    dtRow = pDataSetConfig.Tables[0].Rows[iItem_Selected]; //.(myColumn.ColumnName);
                    sRegresa = (string)dtRow[myColumn.ColumnName].ToString();
                    bExiste = true;
                    break;
                }
            }

            if (sRegresa == "" && (!bExiste))
            {
                // Se agregara la clave indicada
                object[] objValues = { "" };
                myDataTable = new DataTable();
                myDataTable = pDataSetConfig.Tables["Configuracion"];
                myDataColumn = myDataTable.Columns.Add(strKey, System.Type.GetType("System.String"));
                pDataSetConfig.Tables[0].Rows[iItem_Selected][strKey] = prtValue;
                sRegresa = prtValue;

                // Agregar la nueva columna 
                pDataSetConfig.WriteXml(strFilePath);
            }

            return sRegresa;
        }

        /// <summary>
        /// Estable una clave del archivo de configuracion.
        /// </summary>
        /// <param name="strKey">Clave a buscar, si no existe la agrega al archivo de configuracion.</param>
        /// <param name="prtValue">Valor por default de la clave a buscar</param>
        /// <returns></returns>
        public string SetValues(string strKey, string prtValue)
        {
            string sRegresa = "";
            //DataRow dtRow;

            ValidarRows(); 

            foreach (DataColumn myColumn in pDataSetConfig.Tables[0].Columns)
            {
                if (myColumn.ColumnName.ToString().ToUpper() == strKey.ToString().ToUpper())
                {
                    pDataSetConfig.Tables[0].Rows[iItem_Selected][myColumn.ColumnName] = prtValue; //.(myColumn.ColumnName);
                    sRegresa = prtValue;
                    break;
                }
            }

            if (sRegresa == "")
            {
                // Se agregara la clave indicada
                object[] objValues = { "" };
                myDataTable = new DataTable();
                myDataTable = pDataSetConfig.Tables["Configuracion"];
                myDataColumn = myDataTable.Columns.Add(strKey, System.Type.GetType("System.String"));
                pDataSetConfig.Tables[0].Rows[iItem_Selected][strKey] = prtValue;
            }

            pDataSetConfig.WriteXml(strFilePath);
            return sRegresa;
        }               

        /// <summary>
        /// Determina si existe el archivo de configuracion
        /// </summary>
        public bool ExisteXML
	    {
		    get
            {
                return blExisteArchivo;
            }
		    set
            {
            }
	    }

        /// <summary>
        /// Determina si existe el archivo de configuracion
        /// </summary> 
        public bool ExisteArchivo()
        {
            FileInfo Dir = null; /// new FileInfo(strFilePath);
            bool bRegresa = true;

            if (bXmlEnDirectorioApp)
            {
                strFilePath = Application.StartupPath + @"/" + General.ArchivoIni.Substring(0, General.ArchivoIni.Length - 4) + ".xml";
            }

            Dir = new FileInfo(strFilePath);
            if (! Dir.Exists )
            {
                Frm = new FrmFileConfigXml();
                string sUsarWebService = "", sServer = "", sWebService = "", sPaginaASMX = "";
                string sSSL = "";
                string sAlias = "Default";

                Frm.sAlias = sAlias; 
                Frm.sUsarWebService = sUsarWebService;
                Frm.sServer = sServer;
                Frm.sWebService = sWebService;
                Frm.sPaginaASMX = sPaginaASMX;
                Frm.bSSL = false; 
                if (General.SolicitarConfiguracionWeb)
                {
                    Frm.StartPosition = FormStartPosition.CenterScreen;
                    Frm.ShowDialog();
                }
                else
                {
                    Frm.sUsarWebService = "NO";
                    Frm.bAceptar = true;
                }

                if (Frm.bAceptar)
                {
                    sUsarWebService = Frm.sUsarWebService;
                    sServer = Frm.sServer;
                    sWebService = Frm.sWebService;
                    sPaginaASMX = Frm.sPaginaASMX;
                    sSSL = Frm.bSSL ? "1" : "0"; 
                    Frm.Close();

                    blExisteArchivo = false;
                    myDataTable = new DataTable();

                    myDataTable = pDataSetConfig.Tables.Add("Configuracion");
                    myDataColumn = myDataTable.Columns.Add("Alias", System.Type.GetType("System.String"));
                    myDataColumn = myDataTable.Columns.Add("ConexionWeb", System.Type.GetType("System.String"));
                    myDataColumn = myDataTable.Columns.Add("Servidor", System.Type.GetType("System.String"));
                    myDataColumn = myDataTable.Columns.Add("WebService", System.Type.GetType("System.String"));
                    myDataColumn = myDataTable.Columns.Add("PaginaAsmx", System.Type.GetType("System.String"));
                    myDataColumn = myDataTable.Columns.Add("IdEmpresa", System.Type.GetType("System.String"));
                    myDataColumn = myDataTable.Columns.Add("IdEstado", System.Type.GetType("System.String"));
                    myDataColumn = myDataTable.Columns.Add("IdSucursal", System.Type.GetType("System.String"));
                    myDataColumn = myDataTable.Columns.Add("UltimoUsuarioConectado", System.Type.GetType("System.String"));
                    myDataColumn = myDataTable.Columns.Add("TipoImpresion", System.Type.GetType("System.String"));
                    myDataColumn = myDataTable.Columns.Add("Lenguaje", System.Type.GetType("System.String"));
                    myDataColumn = myDataTable.Columns.Add("SSL", System.Type.GetType("System.String"));

                    AgregarValuesDefault(sAlias, sUsarWebService, sServer, sWebService, sPaginaASMX, "", "", "", "", "1", "es-mx", sSSL);
                    pDataSetConfig.WriteXml(strFilePath);
                }
                else
                    bRegresa = false;
            }
            else 
            {
                blExisteArchivo = true;
                pDataSetConfig.ReadXml(strFilePath);
            }

            return bRegresa;
        }

        /// <summary>
        /// Crea el archivo de configuracion con los valores por default.
        /// </summary>
        /// <param name="Servidor">Servidor</param>
        /// <param name="WebService">Nombre web servive</param>
        /// <param name="IdSucursal">Sucural que se conecto</param>
        /// <param name="Usuario">Usuario que se conecto</param>
        private void AgregarValuesDefault(string Alias, string UsarCnnWeb, string Servidor, string WebService, string PaginaAsmx, 
            string IdEmpresa, string IdEstado, string IdSucursal, string Usuario, string TipoImpresion, string Lenguaje, string SSL)
        {
            object[] objValues = { Alias, UsarCnnWeb, Servidor, WebService, PaginaAsmx, IdEmpresa, IdEstado, IdSucursal, Usuario, TipoImpresion, Lenguaje, SSL };
            myDataRow = pDataSetConfig.Tables["Configuracion"].Rows.Add(objValues);
        }
    }
}
