using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_MA_IFacturacion.CFDI.EDAS;

namespace Dll_MA_IFacturacion.CFDI.EDOInvoice
{
    public class Emisor : eTable
    {
        public Emisor()
        {
            base.tableName = "CFDI_Emisores";


            base.addField("CURP", SqlDbType.VarChar, 0x23L, null, false);
            base.addField("DFiscalComoDExpedicion", SqlDbType.Bit, 0L, null, true);
            base.addField("DomicilioExpedicion", SqlDbType.BigInt, 0L, null, false);
            base.addField("DomicilioFiscal", SqlDbType.BigInt, 0L, null, true);
            base.addField("eMail", SqlDbType.VarChar, 100L, null, false);
            base.addField("Fax", SqlDbType.VarChar, 50L, null, false);
            base.addField("NombreComercial", SqlDbType.VarChar, 500L, null, false);
            base.addField("NombreFiscal", SqlDbType.VarChar, 500L, null, true);
            base.addField("Notas", SqlDbType.VarChar, 0xfd8L, null, false);
            base.addField("RFC", SqlDbType.VarChar, 13L, null, true);
            base.addField("Telefono", SqlDbType.VarChar, 50L, null, false);
        }

        public string CURP
        {
            get
            {
                return Convert.ToString(base.field("CURP").value);
            }
            set
            {
                base.field("CURP").value = value;
            }
        }

        public bool DFiscalComoDExpedicion
        {
            get
            {
                return Convert.ToBoolean(base.field("DFiscalComoDExpedicion").value);
            }
            set
            {
                base.field("DFiscalComoDExpedicion").value = value;
            }
        }

        public long DomicilioExpedicion
        {
            get
            {
                return Convert.ToInt64(base.field("DomicilioExpedicion").value);
            }
            set
            {
                base.field("DomicilioExpedicion").value = value;
            }
        }

        public long DomicilioFiscal
        {
            get
            {
                return Convert.ToInt64(base.field("DomicilioFiscal").value);
            }
            set
            {
                base.field("DomicilioFiscal").value = value;
            }
        }

        public string eMail
        {
            get
            {
                return Convert.ToString(base.field("eMail").value);
            }
            set
            {
                base.field("eMail").value = value;
            }
        }

        public string Fax
        {
            get
            {
                return Convert.ToString(base.field("Fax").value);
            }
            set
            {
                base.field("Fax").value = value;
            }
        }

        public string NombreComercial
        {
            get
            {
                return Convert.ToString(base.field("NombreComercial").value);
            }
            set
            {
                base.field("NombreComercial").value = value;
            }
        }

        public string NombreFiscal
        {
            get
            {
                return Convert.ToString(base.field("NombreFiscal").value);
            }
            set
            {
                base.field("NombreFiscal").value = value;
            }
        }

        public string Notas
        {
            get
            {
                return Convert.ToString(base.field("Notas").value);
            }
            set
            {
                base.field("Notas").value = value;
            }
        }

        public string RFC
        {
            get
            {
                return Convert.ToString(base.field("RFC").value);
            }
            set
            {
                base.field("RFC").value = value;
            }
        }

        public string Telefono
        {
            get
            {
                return Convert.ToString(base.field("Telefono").value);
            }
            set
            {
                base.field("Telefono").value = value;
            }
        }
    }
}

