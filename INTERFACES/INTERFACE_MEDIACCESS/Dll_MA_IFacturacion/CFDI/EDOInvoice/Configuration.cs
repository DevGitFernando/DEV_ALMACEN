using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_MA_IFacturacion.CFDI.EDAS;

namespace Dll_MA_IFacturacion.CFDI.EDOInvoice
{
    public class Configuration : eTable
    {
        public Configuration()
        {
            base.tableName = "Configuracion";
            base.addField("Nombre", SqlDbType.VarChar, 0x19L, null, true);
            base.addField("Valor", SqlDbType.VarChar, 0xfd8L, null, true);
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

        public string Valor
        {
            get
            {
                return Convert.ToString(base.field("Valor").value);
            }
            set
            {
                base.field("Valor").value = value;
            }
        }
    }
}

