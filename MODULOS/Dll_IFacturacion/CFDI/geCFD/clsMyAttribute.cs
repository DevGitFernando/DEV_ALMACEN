using System;

namespace Dll_IFacturacion.CFDI.geCFD
{
    public class clsMyAttribute
    {
        public bool allowPipes = false;
        private string dafaultSchemaLocationURI = "http://www.w3.org/2001/XMLSchema-instance";
        private string mName = "";
        private string mNamespaceURI = "";
        private string mValue = "";

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
                if (!this.allowPipes)
                {
                    this.mName = this.mName.Replace("|", "");
                }
            }
        }

        public string NamespaceURI
        {
            get
            {
                return this.mNamespaceURI;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                this.mNamespaceURI = value.Trim();
                if (!this.allowPipes)
                {
                    this.mNamespaceURI = this.mNamespaceURI.Replace("|", "");
                }
            }
        }

        public bool schemaLocation
        {
            get
            {
                return (this.NamespaceURI == this.dafaultSchemaLocationURI);
            }
            set
            {
                if (value)
                {
                    this.NamespaceURI = this.dafaultSchemaLocationURI;
                }
                else
                {
                    this.NamespaceURI = "";
                }
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
                this.mValue = value.Trim();
                if (!this.allowPipes)
                {
                    this.mValue = this.mValue.Replace("|", "");
                }
            }
        }
    }
}

