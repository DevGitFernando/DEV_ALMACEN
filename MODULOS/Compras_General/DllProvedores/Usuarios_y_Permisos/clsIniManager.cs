using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Security.Permissions;

using SC_SolutionsSystem;

namespace DllProveedores.Usuarios_y_Permisos
{
    /// <summary>
    /// Clase que se encarga del manejo del archivo de configuración de la aplicacion.
    /// </summary>
    // [FileIOPermissionAttribute(SecurityAction.Demand,  Name = "FullTrust")] 
    public class clsIniManager
    {
        #region Declaracion de variables
        private DataSet pDataSetConfig = new DataSet();
        private string strFilePath = General.UnidadSO + "://" + General.ArchivoIni.Substring(0, General.ArchivoIni.Length - 4) + ".xml";
        private DataColumn myDataColumn; 
        private DataRow myDataRow;
        private DataTable myDataTable;
        private FrmFileConfigXml Frm;

        private bool blExisteArchivo = false;
        #endregion

        #region Constructores de clase y destructor
        public clsIniManager ()
        {
            pDataSetConfig = new DataSet("SC_Solutions");
            //ExisteArchivo();
        }

        ~clsIniManager()
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
        public string GetValues(string strKey)
        {
            string sRegresa = "";
            bool bExiste = false;
            DataRow dtRow;

            foreach (DataColumn myColumn in pDataSetConfig.Tables[0].Columns)
            {
                if (myColumn.ColumnName.ToString().ToUpper() == strKey.ToString().ToUpper())
                {
                    dtRow = pDataSetConfig.Tables[0].Rows[0]; //.(myColumn.ColumnName);
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
                pDataSetConfig.Tables[0].Rows[0][strKey] = "";
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

            foreach (DataColumn myColumn in pDataSetConfig.Tables[0].Columns)
            {
                if (myColumn.ColumnName.ToString().ToUpper() == strKey.ToString().ToUpper())
                {
                    pDataSetConfig.Tables[0].Rows[0][myColumn.ColumnName] = prtValue; //.(myColumn.ColumnName);
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
                pDataSetConfig.Tables[0].Rows[0][strKey] = prtValue;
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
            FileInfo Dir = new FileInfo(strFilePath);
            bool bRegresa = true;

            if (! Dir.Exists )
            {
                Frm = new FrmFileConfigXml();
                string sUsarWebService = "", sServer = "", sWebService = "", sPaginaASMX = "";

                Frm.sUsarWebService = sUsarWebService;
                Frm.sServer = sServer;
                Frm.sWebService = sWebService;
                Frm.sPaginaASMX = sPaginaASMX;

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
                    Frm.Close();

                    blExisteArchivo = false;
                    myDataTable = new DataTable();

                    myDataTable = pDataSetConfig.Tables.Add("Configuracion");
                    myDataColumn = myDataTable.Columns.Add("ConexionWeb", System.Type.GetType("System.String"));
                    myDataColumn = myDataTable.Columns.Add("Servidor", System.Type.GetType("System.String"));
                    myDataColumn = myDataTable.Columns.Add("WebService", System.Type.GetType("System.String"));
                    myDataColumn = myDataTable.Columns.Add("PaginaAsmx", System.Type.GetType("System.String"));
                    //myDataColumn = myDataTable.Columns.Add("IdEmpresa", System.Type.GetType("System.String"));
                    //myDataColumn = myDataTable.Columns.Add("IdEstado", System.Type.GetType("System.String"));
                    //myDataColumn = myDataTable.Columns.Add("IdSucursal", System.Type.GetType("System.String"));
                    //myDataColumn = myDataTable.Columns.Add("UltimoUsuarioConectado", System.Type.GetType("System.String"));

                    AgregarValuesDefault(sUsarWebService, sServer, sWebService, sPaginaASMX);
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
        private void AgregarValuesDefault(string UsarCnnWeb, string Servidor, string WebService, string PaginaAsmx )
        {
            object[] objValues = { UsarCnnWeb, Servidor, WebService, PaginaAsmx};
            myDataRow = pDataSetConfig.Tables["Configuracion"].Rows.Add(objValues);
        }
    }
}
