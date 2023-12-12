using System;
using System.Xml;

using Dll_MA_IFacturacion;
using Dll_MA_IFacturacion.CFDI;

namespace Dll_MA_IFacturacion.CFDI.geCFD
{

    public class clsComplementoConcepto
    {
        private XmlElement mcomplemento;
        private string mlastError;

        private void LogProceso(string Funcion, string Mensaje)
        {
            clsLog_CFDI.Log("clsComplementoConcepto", Funcion, Mensaje);
        }

        public bool isOK()
        {
            if (this.complemento == null)
            {
                this.lastError = "Falta atributo: complemento";
                LogProceso("isOK", lastError); 
                return false;
            }
            return true;
        }

        public bool loadFromXML(XmlNode mainNode)
        {
            try
            {
                this.complemento = (XmlElement) mainNode;
                return true;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                LogProceso("loadFromXML", lastError); 
                return false;
            }
        }

        public XmlElement complemento
        {
            get
            {
                return this.mcomplemento;
            }
            set
            {
                this.mcomplemento = value;
            }
        }

        public string lastError
        {
            get
            {
                return this.mlastError;
            }
            set
            {
                this.mlastError = value.Trim();
            }
        }
    }
}

