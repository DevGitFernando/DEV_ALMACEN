using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_IFacturacion.CFDI.EDAS;

namespace Dll_IFacturacion.CFDI.EDOInvoice
{
    public class Ciudad : eTable
    {
        public Ciudad()
        {
            base.tableName = "Ciudad";
            base.addField("Codigo", SqlDbType.VarChar, 8L, null, true);
            base.addField("CodigoTel", SqlDbType.VarChar, 5L, null, false);
            base.addField("Estado", SqlDbType.BigInt, 0L, null, true);
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

        public string CodigoTel
        {
            get
            {
                return Convert.ToString(base.field("CodigoTel").value);
            }
            set
            {
                base.field("CodigoTel").value = value;
            }
        }

        public long Estado
        {
            get
            {
                return Convert.ToInt64(base.field("Estado").value);
            }
            set
            {
                base.field("Estado").value = value;
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

