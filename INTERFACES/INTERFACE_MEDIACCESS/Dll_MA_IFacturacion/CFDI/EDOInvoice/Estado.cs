using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_MA_IFacturacion.CFDI.EDAS;

namespace Dll_MA_IFacturacion.CFDI.EDOInvoice
{
    public class Estado : eTable
    {
        public Estado()
        {
            base.tableName = "EdoProv";
            base.addField("Codigo", SqlDbType.VarChar, 8L, null, true);
            base.addField("IPais", SqlDbType.BigInt, 0L, null, true);
            base.addField("Nombre", SqlDbType.VarChar, 50L, null, true);
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

        public long IPais
        {
            get
            {
                return Convert.ToInt64(base.field("IPais").value);
            }
            set
            {
                base.field("IPais").value = value;
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

