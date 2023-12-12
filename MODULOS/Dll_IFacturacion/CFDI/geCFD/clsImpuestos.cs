using System;
using System.Collections;
using System.Xml;

namespace Dll_IFacturacion.CFDI.geCFD
{

    public class clsImpuestos
    {
        private string mlastError;
        private double mtotalImpuestosRetenidos;
        private double mtotalImpuestosTrasladados;
        public ArrayList Retenciones = new ArrayList();
        public ArrayList Traslados = new ArrayList();

        public bool isOK()
        {
            return true;
        }

        public bool loadFromXML(XmlNode mainNode)
        {
            try
            {
                clsFromXML mxml = new clsFromXML();
                this.lastError = "";
                this.totalImpuestosRetenidos = mxml.doubleAttValue(mainNode, "totalImpuestosRetenidos");
                this.totalImpuestosTrasladados = mxml.doubleAttValue(mainNode, "totalImpuestosTrasladados");
                XmlNode node = mxml.getNode("Retenciones", mainNode.ChildNodes);
                if (node != null)
                {
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        clsRetencion retencion = new clsRetencion();
                        retencion.loadFromXML(node2);
                        this.Retenciones.Add(retencion);
                        retencion = null;
                    }
                    node = null;
                }

                XmlNode node3 = mxml.getNode("Traslados", mainNode.ChildNodes);
                if (node3 != null)
                {
                    foreach (XmlNode node4 in node3.ChildNodes)
                    {
                        clsTraslado traslado = new clsTraslado();
                        traslado.loadFromXML(node4);
                        this.Traslados.Add(traslado);
                        traslado = null;
                    }
                    node3 = null;
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

        public double totalImpuestosRetenidos
        {
            get
            {
                return this.mtotalImpuestosRetenidos;
            }
            set
            {
                this.mtotalImpuestosRetenidos = this.t_importe(value);
            }
        }

        public double totalImpuestosTrasladados
        {
            get
            {
                return this.mtotalImpuestosTrasladados;
            }
            set
            {
                this.mtotalImpuestosTrasladados = this.t_importe(value);
            }
        }
    }
}

