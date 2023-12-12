using System;
using System.Xml;

using Dll_MA_IFacturacion;
using Dll_MA_IFacturacion.CFDI;

namespace Dll_MA_IFacturacion.CFDI.geCFD
{
    public class clsCuentaPredial
    {
        private string mlastError;
        private string mnumero;

        private bool containsPipe(string fieldName, string fieldValue)
        {
            if ((fieldValue != null) && fieldValue.Contains("|"))
            {
                this.lastError = "Atributo " + fieldName + "incorrecto. Ning\x00fan elemento puede contener el caracter | (pipe).";
                return true;
            }
            return false;
        }

        public bool isOK()
        {
            if ((this.numero == "") || (this.numero == null))
            {
                this.lastError = "Falta atributo requerido: numero";
                return false;
            }
            if (this.containsPipe("numero", this.numero))
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
                mxml = null;
                return true;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return false;
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

