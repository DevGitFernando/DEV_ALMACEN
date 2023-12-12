using System;
using System.Xml;

using Dll_MA_IFacturacion;
using Dll_MA_IFacturacion.CFDI;

namespace Dll_MA_IFacturacion.CFDI.geCFD
{
    public class clsInformacionAduanera
    {
        private string maduana;
        private DateTime mfecha;
        private string mlastError;
        private string mnumero;

        private void LogProceso(string Funcion, string Mensaje)
        {
            clsLog_CFDI.Log("clsInformacionAduanera", Funcion, Mensaje);
        }

        private bool containsPipe(string fieldName, string fieldValue)
        {
            if ((fieldValue != null) && fieldValue.Contains("|"))
            {
                this.lastError = "Atributo " + fieldName + "incorrecto. Ning\x00fan elemento puede contener el caracter | (pipe).";
                LogProceso("isOK", lastError); 
                return true;
            }
            return false;
        }

        public bool isOK()
        {
            if ((this.numero == "") || (this.numero == null))
            {
                this.lastError = "Falta atributo requerido: numero";
                LogProceso("isOK", lastError); 
                return false;
            }
            if (this.containsPipe("numero", this.numero))
            {
                return false;
            }
            if (this.containsPipe("aduana", this.aduana))
            {
                return false;
            }
            return true;
        }

        public bool loadFromXML(XmlNode mainNode)
        {
            try
            {
                clsFromXML mxml = new clsFromXML();
                this.lastError = "";
                this.numero = mxml.stringAttValue(mainNode, "numero");
                this.fecha = mxml.dateTimeAttValue(mainNode, "fecha");
                this.aduana = mxml.stringAttValue(mainNode, "aduana");
                mxml = null;
                return true;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                LogProceso("loadFromXML", lastError); 
                return false;
            }
        }

        public string aduana
        {
            get
            {
                return this.maduana;
            }
            set
            {
                this.maduana = value.Trim();
            }
        }

        public DateTime fecha
        {
            get
            {
                return this.mfecha;
            }
            set
            {
                this.mfecha = value;
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

        public string numero
        {
            get
            {
                return this.mnumero;
            }
            set
            {
                this.mnumero = value.Trim();
            }
        }
    }
}

