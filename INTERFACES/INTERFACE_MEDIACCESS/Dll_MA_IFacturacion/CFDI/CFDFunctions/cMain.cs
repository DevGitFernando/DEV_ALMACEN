using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

//using Dll_MA_IFacturacion.CFDI.CFDFunctions.wsapptools;
using Dll_MA_IFacturacion.CFDI.EDOInvoice;
using Dll_MA_IFacturacion.CFDI.geCBB;
using Dll_MA_IFacturacion.CFDI.geCFD;
////using Dll_MA_IFacturacion.CFDI.HiddenKey;
////using psecfdi.edicom;

namespace Dll_MA_IFacturacion.CFDI.CFDFunctions
{
    public class cMain
    {
        public enum tipoComprobante
        {
            cCBB = 3,
            cCFD = 1,
            cCFDI = 2,
            cCFDI_CBB = 4,
            cPapelSimple = 5
        }

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
                return data;
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

        #region Envio de Correo electrónico 
        private frmPB oPB;

        public void InitAutoPB(string title)
        {
            try
            {
                this.oPB = new frmPB();
                this.oPB.setTitle(title);
                this.oPB.Auto();
                this.oPB.Show();
            }
            catch (Exception)
            {
            }
        }

        public void FinishAutoPB(bool complete)
        {
            try
            {
                this.oPB.FinishAuto(complete);
                this.oPB.Dispose();
                this.oPB = null;
            }
            catch (Exception)
            {
            }
        }

        public bool enviarCorreoElectronico(string Destinatario, string Mensaje, string Archivo, string RutaArchivos)
        {
            bool bComplete = false;

            try
            {
                this.proccess.lastError = ""; 

                string path = "";
                string str15 = "";
                string str16 = RutaArchivos;
                string def = Destinatario;

                if (def == "")
                {
                    this.showMsg("Envío cancelado. No se se agregó el destinario.", MessageBoxIcon.Asterisk);
                    bComplete = false;
                    return bComplete;
                }

                string fromMail = DtIFacturacion.DatosEmail.Usuario;  // this.getVarStrValue(clsVars.varMail());
                string replayTo = DtIFacturacion.DatosEmail.EmailRespuesta;
                string displayName = DtIFacturacion.DatosEmail.NombreParaMostrar;  // this.getVarStrValue(clsVars.varMailNombrePMostrar());
                string subject = DtIFacturacion.DatosEmail.Asunto + " :  " + Archivo;  // this.getVarStrValue(clsVars.varMailAsunto());
                string ccMail = DtIFacturacion.DatosEmail.CC;  // this.getVarStrValue(clsVars.varMailCC());
                string body = Mensaje;  // this.getVarStrValue(clsVars.varMailMensaje());
                string smtpServer = DtIFacturacion.DatosEmail.Servidor;  // this.getVarStrValue(clsVars.varMailSMPT());
                bool useSSL = DtIFacturacion.DatosEmail.EnableSSL;  // this.getVarBoolValue(clsVars.varMailSSL());
                string user = DtIFacturacion.DatosEmail.Usuario;  // this.getVarStrValue(clsVars.varMailUser());
                string data = DtIFacturacion.DatosEmail.Password;  // this.getVarStrValue(clsVars.varMailPWD());
                int timeout = DtIFacturacion.DatosEmail.TimeOut;  // Convert.ToInt32(this.getVarStrValue(clsVars.varMailRemainingTime()));
                int port = DtIFacturacion.DatosEmail.Puerto;  // Convert.ToInt32(this.getVarStrValue(clsVars.varMailPort()));                
                string extbody = "";

                path = str16 + ".xml";
                if (!File.Exists(path))
                {
                    this.showMsg("Imposible enviar. El archivo XML no existe.", MessageBoxIcon.Exclamation);
                    bComplete = false;
                    return bComplete;
                }

                str15 = str16 + ".pdf";
                if (!File.Exists(path))
                {
                    this.showMsg("Imposible enviar. No se encontró el archivo de presentación (pdf) del CFDI.", MessageBoxIcon.Exclamation);
                    bComplete = false;
                    return bComplete;
                }

                string attachFiles = path + ";" + str15;
                this.InitAutoPB("Enviado correo ...");
                this.oPB.SetProgress("Enviando ...", 0);
                this.oPB.pb.Value = 0x1d;

                bComplete = this.proccess.sendeMail2(smtpServer, port, timeout, useSSL, user, this.DecryptString(data), fromMail, displayName, def, ccMail, subject, body, extbody, attachFiles, replayTo);

                if (this.proccess.lastError != "")
                {
                    this.showMsg(this.proccess.lastError, MessageBoxIcon.Exclamation);
                    bComplete = false;
                }
                this.FinishAutoPB(bComplete);

                if (bComplete)
                {
                    this.showMsg("Mensaje enviado correctamente a :  " + def, MessageBoxIcon.Asterisk);
                }

            }
            catch (Exception exception)
            {
                this.showMsg(exception.Message, MessageBoxIcon.Exclamation);
                bComplete = false;
                return bComplete;
            }

            return bComplete;
        }
        #endregion Envio de Correo electrónico
    }
}

