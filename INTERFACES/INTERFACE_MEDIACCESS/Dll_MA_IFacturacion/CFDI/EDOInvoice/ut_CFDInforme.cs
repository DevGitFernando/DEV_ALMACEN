using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_MA_IFacturacion.CFDI.EDAS;

namespace Dll_MA_IFacturacion.CFDI.EDOInvoice
{
    public class ut_CFDInforme : eTable
    {
        public ut_CFDInforme()
        {
            base.tableName = "ut_CFDInforme";
            base.addField("uf_Anio", SqlDbType.Int, 0L, null, true);
            base.addField("uf_Contenido", SqlDbType.VarChar, 0x186a0L, null, false);
            base.addField("uf_Fecha", SqlDbType.Timestamp, 0L, null, false);
            base.addField("uf_Mes", SqlDbType.Int, 0L, null, true);
            base.addField("uf_Nombre", SqlDbType.VarChar, 50L, null, false);
        }

        public int uf_Anio
        {
            get
            {
                return Convert.ToInt32(base.field("uf_Anio").value);
            }
            set
            {
                base.field("uf_Anio").value = value;
            }
        }

        public string uf_Contenido
        {
            get
            {
                return Convert.ToString(base.field("uf_Contenido").value);
            }
            set
            {
                base.field("uf_Contenido").value = value;
            }
        }

        public DateTime uf_Fecha
        {
            get
            {
                return Convert.ToDateTime(base.field("uf_Fecha").value);
            }
            set
            {
                base.field("uf_Fecha").value = value;
            }
        }

        public int uf_Mes
        {
            get
            {
                return Convert.ToInt32(base.field("uf_Mes").value);
            }
            set
            {
                base.field("uf_Mes").value = value;
            }
        }

        public string uf_Nombre
        {
            get
            {
                return Convert.ToString(base.field("uf_Nombre").value);
            }
            set
            {
                base.field("uf_Nombre").value = value;
            }
        }
    }
}

