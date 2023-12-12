using System;
using System.Xml;

namespace Dll_IFacturacion.CFDI.geCFD
{
    public class clsFromXML
    {
        private string mlastError;

        public clsFromXML()
        {
            this.lastError = "";
        }

        public DateTime dateTimeAttValue(XmlNode node, string attributeName)
        {
            try
            {
                return Convert.ToDateTime(node.Attributes.GetNamedItem(attributeName).Value);
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return DateTime.Now;
            }
        }

        public double doubleAttValue(XmlNode node, string attributeName)
        {
            try
            {
                return Convert.ToDouble(node.Attributes.GetNamedItem(attributeName).Value);
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return 0.0;
            }
        }

        public XmlNode getNode(string nodeName, XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                if (node.LocalName.ToLower() == nodeName.ToLower())
                {
                    return node;
                }
            }
            this.lastError = "No se pudo obtener el elemento " + nodeName;
            return null;
        }

        public int intAttValue(XmlNode node, string attributeName)
        {
            try
            {
                return Convert.ToInt32(node.Attributes.GetNamedItem(attributeName).Value);
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return 0;
            }
        }

        public clsMyElement loadElementDefinition(XmlElement element, bool allowPipes)
        {
            return this.loadNodeDefinition(element, allowPipes);
        }

        public clsMyElement loadNodeDefinition(XmlNode node, bool allowPipes)
        {
            try
            {
                clsMyElement element = new clsMyElement();
                element.allowPipes = allowPipes;
                element.Name = node.LocalName;
                element.Prefix = node.Prefix;
                element.URI = node.NamespaceURI;
                element.Value = node.Value;
                foreach (XmlAttribute attribute2 in node.Attributes)
                {
                    clsMyAttribute attribute = new clsMyAttribute();
                    attribute.allowPipes = allowPipes;
                    attribute.Name = attribute2.Name;
                    attribute.NamespaceURI = attribute2.NamespaceURI;
                    attribute.Value = attribute2.Value;
                    element.Attributes.Add(attribute);
                    attribute = null;
                }
                foreach (XmlNode node2 in node.ChildNodes)
                {
                    element.Elements.Add(this.loadNodeDefinition(node2, allowPipes));
                }
                return element;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return null;
            }
        }

        public long longAttValue(XmlNode node, string attributeName)
        {
            try
            {
                return Convert.ToInt64(node.Attributes.GetNamedItem(attributeName).Value);
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return 0L;
            }
        }

        public string stringAttValue(XmlNode node, string attributeName)
        {
            try
            {
                return node.Attributes.GetNamedItem(attributeName).Value;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return "";
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
    }
}

