using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_MA_IFacturacion.CFDI.EDAS;

namespace Dll_MA_IFacturacion.CFDI.EDOInvoice
{
    public class Plantilla : eTable
    {
        public Plantilla()
        {
            base.tableName = "Plantilla";
            base.addField("Nombre", SqlDbType.VarChar, 50L, null, true);
            base.addField("Archivo", SqlDbType.VarChar, 0x200L, null, true);
        }

        public string Archivo
        {
            get
            {
                return Convert.ToString(base.field("Archivo").value);
            }
            set
            {
                base.field("Archivo").value = value;
            }
        }

        public string Nombre
        {
            get
            {
                return Convert.ToString(base.field("Nombre").value);
            }
            set
            {
                base.field("Nombre").value = value;
            }
        }
    }
}

