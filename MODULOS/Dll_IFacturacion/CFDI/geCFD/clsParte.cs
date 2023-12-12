using System;
using System.Collections;
using System.Xml;

namespace Dll_IFacturacion.CFDI.geCFD
{
    public class clsParte
    {
        public ArrayList InformacionAduanera = new ArrayList();
        private double mcantidad;
        private string mdescripcion;
        private double mimporte;
        private string mlastError;
        private string mnoIdentificacion;
        private string munidad;
        private double mvalorUnitario;

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
            if ((this.descripcion == "") || (this.descripcion == null))
            {
                this.lastError = "Falta atributo requerido: descripcion";
                return false;
            }
            if (this.containsPipe("unidad", this.unidad))
            {
                return false;
            }
            if (this.containsPipe("noIdentificacion", this.noIdentificacion))
            {
                return false;
            }
            if (this.containsPipe("descripcion", this.descripcion))
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
                this.cantidad = mxml.doubleAttValue(mainNode, "cantidad");
                this.unidad = mxml.stringAttValue(mainNode, "unidad");
                this.noIdentificacion = mxml.stringAttValue(mainNode, "noIdentificacion");
                this.descripcion = mxml.stringAttValue(mainNode, "descripcion");
                this.valorUnitario = mxml.doubleAttValue(mainNode, "valorUnitario");
                this.importe = mxml.doubleAttValue(mainNode, "importe");
                foreach (XmlNode node in mainNode.ChildNodes)
                {
                    if (node.LocalName == "InformacionAduanera")
                    {
                        clsInformacionAduanera aduanera = new clsInformacionAduanera();
                        aduanera.loadFromXML(node);
                        this.InformacionAduanera.Add(aduanera);
                        aduanera = null;
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

        private double t_importe(double v)
        {
            v = Math.Round(v, 6);
            return v;
        }

        public double cantidad
        {
            get
            {
                return this.mcantidad;
            }
            set
            {
                this.mcantidad = value;
            }
        }

        public string descripcion
        {
            get
            {
                return this.mdescripcion;
            }
            set
            {
                this.mdescripcion = value.Trim();
            }
        }

        public double importe
        {
            get
            {
                return this.mimporte;
            }
            set
            {
                this.mimporte = this.t_importe(value);
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

        public string noIdentificacion
        {
            get
            {
                return this.mnoIdentificacion;
            }
            set
            {
                this.mnoIdentificacion = value.Trim();
            }
        }

        public string unidad
        {
            get
            {
                return this.munidad;
            }
            set
            {
                this.munidad = value.Trim();
            }
        }

        public double valorUnitario
        {
            get
            {
                return this.mvalorUnitario;
            }
            set
            {
                this.mvalorUnitario = this.t_importe(value);
            }
        }
    }
}

