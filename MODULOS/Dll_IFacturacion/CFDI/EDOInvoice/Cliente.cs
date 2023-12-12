using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_IFacturacion.CFDI.EDAS;

namespace Dll_IFacturacion.CFDI.EDOInvoice
{
    public class Cliente : eTable
    {
        public Cliente()
        {
            base.tableName = "Cliente";
            base.addField("CA1", SqlDbType.VarChar, 0x400L, null, false);
            base.addField("CA2", SqlDbType.VarChar, 0x400L, null, false);
            base.addField("CA3", SqlDbType.VarChar, 0x400L, null, false);
            base.addField("Codigo", SqlDbType.VarChar, 15L, null, true);
            base.addField("Contacto", SqlDbType.VarChar, 250L, null, false);
            base.addField("CURP", SqlDbType.VarChar, 0x23L, null, false);
            base.addField("eMail", SqlDbType.VarChar, 100L, null, false);
            base.addField("Fax", SqlDbType.VarChar, 30L, null, false);
            base.addField("NombreComercial", SqlDbType.VarChar, 500L, null, false);
            base.addField("NombreFiscal", SqlDbType.VarChar, 500L, null, true);
            base.addField("Notas", SqlDbType.VarChar, 0xfd8L, null, false);
            base.addField("RFC", SqlDbType.VarChar, 13L, null, true);
            base.addField("Telefono", SqlDbType.VarChar, 50L, null, false);
            base.addField("UF_Adenda", SqlDbType.Bit, 0L, null, false);
            base.addField("Domicilio1", SqlDbType.BigInt, 0L, null, true);
        }

        public string CA1
        {
            get
            {
                return Convert.ToString(base.field("CA1").value);
            }
            set
            {
                base.field("CA1").value = value;
            }
        }

        public string CA2
        {
            get
            {
                return Convert.ToString(base.field("CA2").value);
            }
            set
            {
                base.field("CA2").value = value;
            }
        }

        public string CA3
        {
            get
            {
                return Convert.ToString(base.field("CA3").value);
            }
            set
            {
                base.field("CA3").value = value;
            }
        }

        public string Codigo
        {
            get
            {
                return Convert.ToString(base.field("Codigo").value);
            }
            set
            {
                base.field("Codigo").value = value;
            }
        }

        public string Contacto
        {
            get
            {
                return Convert.ToString(base.field("Contacto").value);
            }
            set
            {
                base.field("Contacto").value = value;
            }
        }

        public string Curp
        {
            get
            {
                return Convert.ToString(base.field("Curp").value);
            }
            set
            {
                base.field("Curp").value = value;
            }
        }

        public long Domicilio1
        {
            get
            {
                return Convert.ToInt64(base.field("Domicilio1").value);
            }
            set
            {
                base.field("Domicilio1").value = value;
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

        public bool UF_Adenda
        {
            get
            {
                return Convert.ToBoolean(base.field("UF_Adenda").value);
            }
            set
            {
                base.field("UF_Adenda").value = value;
            }
        }
    }
}

