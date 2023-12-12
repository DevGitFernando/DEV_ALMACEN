using System;
using System.Xml;

namespace Dll_IFacturacion.CFDI.geCFD
{
    public class clsUbicacion
    {
        private string mcalle = "";
        private string mcodigoPostal = "";
        private string mcolonia = "";
        private string mestado = "";
        private string mlastError = "";
        private string mlocalidad = "";
        private string mmunicipio = "";
        private string mnoExterior = "";
        private string mnoInterior = "";
        private string mpais = "";
        private string mreferencia = "";

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
            if ((this.pais == "") || (this.pais == null))
            {
                this.lastError = "Falta atributo requerido: pa\x00eds";
                return false;
            }
            if (this.containsPipe("calle", this.calle))
            {
                return false;
            }
            if (this.containsPipe("noExterior", this.noExterior))
            {
                return false;
            }
            if (this.containsPipe("noInterior", this.noInterior))
            {
                return false;
            }
            if (this.containsPipe("colonia", this.colonia))
            {
                return false;
            }
            if (this.containsPipe("localidad", this.localidad))
            {
                return false;
            }
            if (this.containsPipe("referencia", this.referencia))
            {
                return false;
            }
            if (this.containsPipe("municipio", this.municipio))
            {
                return false;
            }
            if (this.containsPipe("estado", this.estado))
            {
                return false;
            }
            if (this.containsPipe("pais", this.pais))
            {
                return false;
            }
            if (this.containsPipe("codigoPostal", this.codigoPostal))
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
                this.calle = mxml.stringAttValue(mainNode, "calle");
                this.noExterior = mxml.stringAttValue(mainNode, "noExterior");
                this.noInterior = mxml.stringAttValue(mainNode, "noInterior");
                this.colonia = mxml.stringAttValue(mainNode, "colonia");
                this.localidad = mxml.stringAttValue(mainNode, "localidad");
                this.referencia = mxml.stringAttValue(mainNode, "referencia");
                this.municipio = mxml.stringAttValue(mainNode, "municipio");
                this.estado = mxml.stringAttValue(mainNode, "estado");
                this.pais = mxml.stringAttValue(mainNode, "pais");
                this.codigoPostal = mxml.stringAttValue(mainNode, "codigoPostal");
                mxml = null;
                return true;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        public string calle
        {
            get
            {
                return this.mcalle;
            }
            set
            {
                this.mcalle = value.Trim();
            }
        }

        public string codigoPostal
        {
            get
            {
                return this.mcodigoPostal;
            }
            set
            {
                this.mcodigoPostal = value.Trim();
            }
        }

        public string colonia
        {
            get
            {
                return this.mcolonia;
            }
            set
            {
                this.mcolonia = value.Trim();
            }
        }

        public string estado
        {
            get
            {
                return this.mestado;
            }
            set
            {
                this.mestado = value.Trim();
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

        public string localidad
        {
            get
            {
                return this.mlocalidad;
            }
            set
            {
                this.mlocalidad = value.Trim();
            }
        }

        public string municipio
        {
            get
            {
                return this.mmunicipio;
            }
            set
            {
                this.mmunicipio = value.Trim();
            }
        }

        public string noExterior
        {
            get
            {
                return this.mnoExterior;
            }
            set
            {
                this.mnoExterior = value.Trim();
            }
        }

        public string noInterior
        {
            get
            {
                return this.mnoInterior;
            }
            set
            {
                this.mnoInterior = value.Trim();
            }
        }

        public string pais
        {
            get
            {
                return this.mpais;
            }
            set
            {
                this.mpais = value.Trim();
            }
        }

        public string referencia
        {
            get
            {
                return this.mreferencia;
            }
            set
            {
                this.mreferencia = value.Trim();
            }
        }
    }
}

