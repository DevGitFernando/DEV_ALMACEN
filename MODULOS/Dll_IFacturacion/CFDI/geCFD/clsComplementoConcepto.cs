using System;
using System.Xml;

namespace Dll_IFacturacion.CFDI.geCFD
{

    public class clsComplementoConcepto
    {
        private XmlElement mcomplemento;
        private string mlastError;

        public bool isOK()
        {
            if (this.complemento == null)
            {
                this.lastError = "Falta atributo: complemento";
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

