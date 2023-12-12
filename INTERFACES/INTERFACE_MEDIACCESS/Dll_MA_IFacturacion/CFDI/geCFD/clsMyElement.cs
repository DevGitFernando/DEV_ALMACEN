using System;
using System.Collections;
using System.Text;
using System.Xml;

using Dll_MA_IFacturacion;
using Dll_MA_IFacturacion.CFDI;

namespace Dll_MA_IFacturacion.CFDI.geCFD
{
    public class clsMyElement
    {
        public bool allowPipes = false;
        public ArrayList Attributes = new ArrayList();
        public ArrayList Elements = new ArrayList();
        private string mName = "";
        private string mPrefix = "";
        private string mURI = "";
        private string mValue = "";
        public bool textNode = false;
        private UTF8Encoding utf8 = new UTF8Encoding();

        public clsMyAttribute getAttribute(string attName)
        {
            foreach (clsMyAttribute attribute in this.Attributes)
            {
                if (attribute.Name == attName)
                {
                    return attribute;
                }
            }
            return null;
        }

        public string getAttributeValue(string attName)
        {
            clsMyAttribute attribute = this.getAttribute(attName);
            if (attribute != null)
            {
                return attribute.Value;
            }
            return "";
        }

        public XmlElement getElement(XmlDocument cfd)
        {
            XmlElement element;
            if (this.textNode)
            {
                return null;
            }
            if ((this.Prefix == "") && (this.URI == ""))
            {
                element = cfd.CreateElement(this.Name);
            }
            else
            {
                element = cfd.CreateElement(this.Prefix, this.Name, this.URI);
            }
            foreach (clsMyAttribute attribute in this.Attributes)
            {
                if ((attribute.NamespaceURI != "") && (attribute.NamespaceURI != null))
                {
                    element.SetAttribute(attribute.Name, attribute.NamespaceURI, this.toUTF8(attribute.Value));
                }
                else
                {
                    element.SetAttribute(attribute.Name, this.toUTF8(attribute.Value));
                }
            }
            foreach (clsMyElement element3 in this.Elements)
            {
                XmlElement element2;
                XmlText text;
                if (element3.textNode)
                {
                    text = element3.getTextNode(cfd);
                    element.AppendChild(text);
                }
                else
                {
                    element2 = element3.getElement(cfd);
                    element.AppendChild(element2);
                }
                element2 = null;
                text = null;
            }
            return element;
        }

        public clsMyElement getMyElement(string elementName)
        {
            foreach (clsMyElement element in this.Elements)
            {
                if (element.Name.ToLower() == elementName.ToLower())
                {
                    return element;
                }
            }
            return null;
        }

        public clsMyElement getMyElementByIndex(int index)
        {
            try
            {
                return (clsMyElement) this.Elements[index];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public XmlText getTextNode(XmlDocument cfd)
        {
            if (!this.textNode)
            {
                return null;
            }
            return cfd.CreateTextNode(this.toUTF8(this.Value));
        }

        public void SetMyAttribute(string attName, string attValue)
        {
            clsMyAttribute attribute = new clsMyAttribute();
            attribute.allowPipes = this.allowPipes;
            attribute.Name = attName.Trim();
            attribute.Value = attValue.Trim();
            this.Attributes.Add(attribute);
            attribute = null;
        }

        public string toUTF8(string stext)
        {
            byte[] bytes = this.utf8.GetBytes(stext);
            return this.utf8.GetString(bytes);
        }

        public string Name
        {
            get
            {
                return this.mName;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                this.mName = value.Trim();
            }
        }

        public string Prefix
        {
            get
            {
                return this.mPrefix;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                this.mPrefix = value.Trim();
            }
        }

        public string URI
        {
            get
            {
                return this.mURI;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                this.mURI = value.Trim();
            }
        }

        public string Value
        {
            get
            {
                return this.mValue;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                this.mValue = value;
            }
        }
    }
}

