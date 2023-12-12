using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_MA_IFacturacion.CFDI.EDAS;

namespace Dll_MA_IFacturacion.CFDI.EDOInvoice
{
    public class DPlantilla : eTable
    {
        public DPlantilla()
        {
            base.tableName = "DPlantilla";
            base.addField("Nombre", SqlDbType.VarChar, 50L, null, true);
            base.addField("SqlInfo", SqlDbType.VarChar, 0x1770L, null, true);
            base.addField("IPlantilla", SqlDbType.BigInt, 0L, null, true);
        }

        public long IPlantilla
        {
            get
            {
                return Convert.ToInt64(base.field("IPlantilla").value);
            }
            set
            {
                base.field("IPlantilla").value = value;
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

        public string SqlInfo
        {
            get
            {
                return Convert.ToString(base.field("SqlInfo").value);
            }
            set
            {
                base.field("SqlInfo").value = value;
            }
        }
    }
}

