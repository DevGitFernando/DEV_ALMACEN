using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

//using Dll_IFacturacion.CFDI.CFDFunctions.wsapptools;
using Dll_IFacturacion.CFDI.EDOInvoice;
using Dll_IFacturacion.CFDI.geCBB;
using Dll_IFacturacion.CFDI.geCFD;
////using Dll_IFacturacion.CFDI.HiddenKey;
////using psecfdi.edicom;

namespace Dll_IFacturacion.CFDI.CFDFunctions
{
    public class cMain
    {
        public string applicationName = "Facturaci\x00f3n";
        //private Service apptService;
        public ArrayList arrVars = new ArrayList();
        public SqlConnection connection;
        public int DecPreMontos = 6;
        public string folderFactura = "Factura";
        public string folderNCredito = "NCredito";
        public string folderRecibo = "Recibo";
        public string formatoFolios = "";
        //private cHKey HK = new cHKey("dkAndro53helI10x");
        public clsInfoEmisor infoEmisor;
        //private frmPB oPB;
        //public psecfdi.edicom.cMain PAC_edicom = new psecfdi.edicom.cMain();
        public clsProccess proccess = new clsProccess();
        public string resourceMacro = "";
        public string resourcePath = "";
        public string rootResourcePath = "";
        public int systemAccountType;
        public tipoComprobante tipoComprobanteAGenerar;
        public clsCBB uiCBB = new clsCBB();


        public string DecryptString(string data)
        {
            try
            {
                return "";
            }
            catch (Exception)
            {
                return data;
            }
        }

        public string EncrytString(string data)
        {
            try
            {
                return data; 
            }
            catch (Exception)
            {
                return data;
            }
        }

        public Configuration getVar(string name)
        {
            foreach (Configuration configuracion in this.arrVars)
            {
                if (configuracion.Nombre.ToLower() == name.ToLower())
                {
                    return configuracion;
                }
            }
            return null;
        }

        public bool getVarBoolValue(string name)
        {
            Configuration configuracion = this.getVar(name);
            bool flag = false;
            if (configuracion != null)
            {
                flag = configuracion.Valor != "0";
            }
            else
            {
                flag = false;
            }
            configuracion = null;
            return flag;
        }

        public string getVarStrValue(string name)
        {
            Configuration configuracion = this.getVar(name);
            string valor = "";
            if (configuracion != null)
            {
                valor = configuracion.Valor;
            }
            else
            {
                valor = "";
            }
            configuracion = null;
            return valor;
        }

        public bool isOnTable(string sTableName, string sFieldValue, string sFindFieldName)
        {
            try
            {
                SqlCommand command = this.connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT " + sFindFieldName + " FROM " + sTableName + " WHERE " + sFindFieldName + "=" + sFieldValue;
                SqlDataReader reader = command.ExecuteReader();
                if (reader == null)
                {
                    this.showMsg("Error al consultar base de datos.", MessageBoxIcon.Exclamation);
                    return true;
                }
                bool flag = false;
                if (reader.HasRows)
                {
                    flag = true;
                }
                reader.Close();
                reader.Dispose();
                reader = null;
                command = null;
                return flag;
            }
            catch (Exception exception)
            {
                this.showMsg(exception.Message, MessageBoxIcon.Exclamation);
                return true;
            }
        }

        private void loadVar(string name)
        {
            this.loadVar(name, "");
        }

        private void loadVar(string name, string defaultValue)
        {
            try
            {
                Configuration configuracion = new Configuration();
                configuracion.connection = this.connection;
                if (!configuracion.load("Nombre='" + name + "'"))
                {
                    configuracion.addNew();
                    configuracion.Nombre = name;
                    configuracion.Valor = defaultValue;
                }
                this.arrVars.Add(configuracion);
            }
            catch (Exception)
            {
            }
        }

        private void loadVarBool(string name)
        {
            this.loadVarBool(name, false);
        }

        private void loadVarBool(string name, bool defaltValue)
        {
            try
            {
                Configuration configuracion = new Configuration();
                configuracion.connection = this.connection;
                if (!configuracion.load("Nombre='" + name + "'"))
                {
                    configuracion.addNew();
                    configuracion.Nombre = name;
                    configuracion.Valor = defaltValue ? "1" : "0";
                }
                this.arrVars.Add(configuracion);
            }
            catch (Exception)
            {
            }
        }

        public void setObjects(SqlConnection cnn, clsInfoEmisor oInfoEmisor)
        {
            this.connection = cnn;
            this.infoEmisor = oInfoEmisor;
        }

        public void showMsg(string msg, MessageBoxIcon icon)
        {
            MessageBox.Show(msg, this.applicationName, MessageBoxButtons.OK, icon);
        }

        public string stringFieldValue(Comprobante Comprobante, string fieldName)
        {
            try
            {
                if ((Comprobante.field(fieldName) != null) && (Comprobante.field(fieldName).value != null))
                {
                    return Comprobante.field(fieldName).value.ToString();
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public enum tipoComprobante
        {
            cCBB = 3,
            cCFD = 1,
            cCFDI = 2,
            cCFDI_CBB = 4,
            cPapelSimple = 5
        }
    }
}

