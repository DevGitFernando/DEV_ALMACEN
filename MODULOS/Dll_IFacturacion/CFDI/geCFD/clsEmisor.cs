using System;
using System.Collections;
using System.Xml;

namespace Dll_IFacturacion.CFDI.geCFD
{
    public class clsEmisor
    {
        public clsUbicacionFiscal DomicilioFiscal = new clsUbicacionFiscal();
        public clsUbicacion ExpedidoEn = new clsUbicacion();
        private string mlastError;
        private string mnombre = "";
        private string mrfc = "";
        private clsRegimenFiscal mRegimenFiscal = new clsRegimenFiscal(); 

        public ArrayList RegimenFiscal = new ArrayList();

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
            if (!this.t_RFC(this.rfc))
            {
                this.lastError = "Atributo requerido incorrecto: rfc";
                return false;
            }

            if (mRegimenFiscal.Regimen == "")
            {
                this.lastError = "Falta nodo requerido: RegimenFiscal";
                return false;
            }

            //if (this.RegimenFiscal.Count < 1)
            //{
            //    this.lastError = "Falta nodo requerido: RegimenFiscal";
            //    return false;
            //}

            //foreach (clsRegimenFiscal fiscal in this.RegimenFiscal)
            //{
            //    if (!fiscal.isOK())
            //    {
            //        return false;
            //    }
            //}
            
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
                XmlNode node = mxml.getNode("DomicilioFiscal", mainNode.ChildNodes);
                if (node != null)
                {
                    this.DomicilioFiscal.loadFromXML(node);
                    node = null;
                }
                XmlNode node2 = mxml.getNode("ExpedidoEn", mainNode.ChildNodes);
                if (node2 != null)
                {
                    this.ExpedidoEn.loadFromXML(node2);
                    node2 = null;
                }

                this.RegimenFiscal.Clear();
                foreach (XmlNode node3 in mainNode.ChildNodes)
                {
                    if (node3.LocalName.ToLower() == "RegimenFiscal".ToLower())
                    {
                        clsRegimenFiscal fiscal = new clsRegimenFiscal();
                        fiscal.Regimen = mxml.stringAttValue(node3, "Regimen");
                        this.RegimenFiscal.Add(fiscal);
                        fiscal = null;
                    }
                }
                mxml = null;
                return true;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
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
                this.mrfc = value.Trim();
            }
        }

        public clsRegimenFiscal RegimenFiscal_33
        {
            get { return mRegimenFiscal; }
            set { mRegimenFiscal = value; }
        }
    }
}

