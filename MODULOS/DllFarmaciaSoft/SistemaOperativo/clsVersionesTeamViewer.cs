using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.Win32;

using System.Data;
using SC_SolutionsSystem; 

namespace DllFarmaciaSoft.SistemaOperativo 
{
    internal class clsVersionesTeamViewer
    {
        string sNombre = "";
        string sRuta = "";
        Exception myError = new Exception("Sin Error");

        //string sReg = @"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run";
        string sReg_x64 = @"Software\Wow6432Node\TeamViewer";
        string sReg_x86 = @"Software\TeamViewer";
        string sValueID = "ClientID".ToUpper();
        string sLastMAC_Used = "LastMACUsed".ToUpper();

        string sHostName = General.NombreEquipo; 
        string sMAC = General.MacAddress;

        DataSet dtsIDs; 

        #region Constructor y Destructor
        public clsVersionesTeamViewer()
        {
            GenerarTabla(); 
        }
        #endregion Constructor y Destructor 

        #region Propiedades 
        public DataSet ListaID_TV
        {
            get { return dtsIDs; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public void Obtener_IDS()
        {
            GenerarTabla(); 
            Obtener_x64();
            Obtener_x86();
        }
        #endregion Funciones y Procedimientos Publicos
        
        #region Funciones y Procedimientos Privados 
        private void GenerarTabla()
        {
            dtsIDs = new DataSet("ID_TeamViewer");
            DataTable dt = new DataTable("ID_MAC");

            dt.Columns.Add("Host_Name", typeof(string));
            dt.Columns.Add("MAC_Address", typeof(string));
            dt.Columns.Add("Version_TV", typeof(string)); 
            dt.Columns.Add("ID_TV", typeof(string)); 

            dtsIDs.Tables.Add(dt); 
        }

        private void Obtener_x64()
        {
            RegistryKey keyBase = Registry.LocalMachine.OpenSubKey(sReg_x64, true);
            SubKeys("", keyBase);
        }

        private void Obtener_x86()
        {
            RegistryKey keyBase = Registry.LocalMachine.OpenSubKey(sReg_x86, true);
            SubKeys("", keyBase);
        }

        private void SubKeys(string KeyBase, RegistryKey Key)
        {
            try
            {
                string[] keys = null;  //Key.GetSubKeyNames();
                string[] values = null; //Key.GetValueNames();
                string sKeyReg = null; //Key.Name;

                string sKeyActual = "";
                object obj_ID = null;
                object obj_MAC = null;


                if (Key != null)
                {
                    keys = Key.GetSubKeyNames();
                    values = Key.GetValueNames();
                    sKeyReg = Key.Name;


                    //object []obj_Item = null; 

                    foreach (string sKey in keys)
                    {
                        RegistryKey subKey = Key.OpenSubKey(sKey, true);
                        SubKeys(sKey, subKey);
                    }

                    foreach (string value in values)
                    {
                        if (sValueID == value.ToUpper())
                        {
                            obj_ID = Key.GetValue(value);
                            object[] obj_Item = { sHostName, sMAC, KeyBase, obj_ID.ToString() };
                            dtsIDs.Tables[0].Rows.Add(obj_Item);
                        }

                        if (sLastMAC_Used == value.ToUpper())
                        {
                            obj_MAC = Key.GetValue(value);
                        }
                    }
                }
            }
            catch { } 
        }
        #endregion Funciones y Procedimientos Privados
    }
}