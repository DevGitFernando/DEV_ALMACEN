using System;
using System.Xml;

using Dll_MA_IFacturacion;
using Dll_MA_IFacturacion.CFDI;

namespace Dll_MA_IFacturacion.CFDI.geCFD
{
    public class clsReceptor
    {
        public clsUbicacion Domicilio = new clsUbicacion();
        private string mlastError;
        private string mnombre;
        private string mrfc;
        private bool bNomina = false;
        private string mcurp;
        private string mnss;
        private string sTelefono = "";

        private string mUsoDeCFDI = "";
        private string mUsoDeCFDI_Descripcion = "";

        private void LogProceso(string Funcion, string Mensaje)
        {
            clsLog_CFDI.Log("clsReceptor", Funcion, Mensaje);
        }

        private bool containsPipe(string fieldName, string fieldValue)
        {
            if ((fieldValue != null) && fieldValue.Contains("|"))
            {
                this.lastError = "Atributo " + fieldName + "incorrecto. Ning\x00fan elemento puede contener el caracter | (pipe).";
                LogProceso("isOK", this.lastError);
                return true;
            }
            return false;
        }

        public bool isOK()
        {
            if (!this.t_RFC(this.rfc))
            {
                this.lastError = "Atributo requerido incorrecto: rfc";
                LogProceso("isOK", this.lastError);
                return false;
            }
            if ((DateTime.Now.Year < 0x7db) && !this.Domicilio.isOK())
            {
                this.lastError = this.Domicilio.lastError;
                LogProceso("isOK", this.lastError);
                return false;
            }
            if (this.containsPipe("rfc", this.rfc))
            {
                return false;
            }
            if (this.containsPipe("nombre", this.nombre))
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
                this.rfc = mxml.stringAttValue(mainNode, "rfc");
                this.nombre = mxml.stringAttValue(mainNode, "nombre");
                XmlNode node = mxml.getNode("Domicilio", mainNode.ChildNodes);
                if (node != null)
                {
                    this.Domicilio.loadFromXML(node);
                    node = null;
                }
                mxml = null;
                return true;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                LogProceso("loadFromXML", this.lastError);
                return false;
            }
        }

        private bool t_RFC(string rfcvalue)
        {
            if (rfcvalue == null)
            {
                return false;
            }
            if (((rfcvalue == "") || (rfcvalue.Length < 12)) || (rfcvalue.Length > 13))
            {
                return false;
            }
            return true;
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

        public string nombre
        {
            get
            {
                return this.mnombre;
            }
            set
            {
                this.mnombre = value.Trim();
            }
        }

        public string rfc
        {
            get
            {
                return this.mrfc;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                value = value.Trim();
                if ((value.Length < 12) || (value.Length > 13))
                {
                    value = "";
                }
                this.mrfc = value;
            }
        }

        public string curp
        {
            get { return mcurp; }
            set { mcurp = value; }
        }

        public string nss
        {
            get { return mnss; }
            set { mnss = value; }
        }

        public string Telefono
        {
            get { return sTelefono; }
            set { sTelefono = value; }
        }

        public string UsoDeCFDI
        {
            get { return mUsoDeCFDI; }
            set { mUsoDeCFDI = value; }
        }

        public string UsoDeCFDI_Descripcion
        {
            get { return mUsoDeCFDI_Descripcion; }
            set { mUsoDeCFDI_Descripcion = value; }
        }
    }
}

