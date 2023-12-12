namespace Dll_IFacturacion.CFDI.CFDFunctions
{
    using System;

    public class clsFieldValue
    {
        public object key;
        public object tag;
        public object value;

        public clsFieldValue()
        {
        }

        public clsFieldValue(object oKey, object oValue)
        {
            this.key = oKey;
            this.value = oValue;
        }

        public clsFieldValue(object oKey, object oValue, object oTag)
        {
            this.key = oKey;
            this.value = oValue;
            this.tag = oTag;
        }

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}

