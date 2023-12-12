using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_IFacturacion.CFDI.EDAS;

namespace Dll_IFacturacion.CFDI.EDOInvoice
{
    public class ut_CFDBitacora : eTable
    {
        public ut_CFDBitacora()
        {
            base.tableName = "ut_CFDBitacora";
            base.addField("uf_CFD", SqlDbType.BigInt, 0L, null, true);
            base.addField("uf_Datos", SqlDbType.VarChar, 0x1770L, null, true);
            base.addField("uf_Reportado", SqlDbType.Bit, 0L, null, true);
        }

        public long uf_CFD
        {
            get
            {
                return Convert.ToInt64(base.field("uf_CFD").value);
            }
            set
            {
                base.field("uf_CFD").value = value;
            }
        }

        public string uf_Datos
        {
            get
            {
                return Convert.ToString(base.field("uf_Datos").value);
            }
            set
            {
                base.field("uf_Datos").value = value;
            }
        }

        public bool uf_Reportado
        {
            get
            {
                return Convert.ToBoolean(base.field("uf_Reportado").value);
            }
            set
            {
                base.field("uf_Reportado").value = value;
            }
        }
    }
}

