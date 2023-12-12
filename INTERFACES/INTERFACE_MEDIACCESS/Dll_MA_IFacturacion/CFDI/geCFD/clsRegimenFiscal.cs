using System;
using System.Xml;

namespace Dll_MA_IFacturacion.CFDI.geCFD
{
    public class clsRegimenFiscal
    {
        private string mlastError;
        private string mRegimen;
        private string mRegimen_Descripcion;

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
            if ((this.Regimen == "") || (this.Regimen == null))
            {
                this.lastError = "Falta atributo requerido: Regimen";
                return false;
            }
            if (this.containsPipe("Regimen", this.Regimen))
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
                this.Regimen = mxml.stringAttValue(mainNode, "Regimen");
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
                this.mlastError = value;
            }
        }

        public string Regimen
        {
            get
            {
                return this.mRegimen;
            }
            set
            {
                this.mRegimen = value;
            }
        }

        public string Descripcion
        {
            get { return mRegimen_Descripcion; }
            set { mRegimen_Descripcion = value; }
        }
    }
}

