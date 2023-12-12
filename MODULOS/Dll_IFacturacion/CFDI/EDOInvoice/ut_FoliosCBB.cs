using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_IFacturacion.CFDI.EDAS;

namespace Dll_IFacturacion.CFDI.EDOInvoice
{
    public class ut_FoliosCBB : eTable
    {
        public ut_FoliosCBB()
        {
            base.tableName = "ut_FoliosCBB";
            base.addField("uf_FolioInicial", SqlDbType.BigInt, 0L, null, true);
            base.addField("uf_FolioFinal", SqlDbType.BigInt, 0L, null, true);
            base.addField("uf_CBB", SqlDbType.VarBinary, 0L, null, true);
            base.addField("uf_BlockDoc", SqlDbType.BigInt, 0L, null, true);
            base.addField("uf_NumAprobacion", SqlDbType.BigInt, 0L, null, true);
            base.addField("uf_Serie", SqlDbType.VarChar, 0x19L, null, false);
            base.addField("uf_FechaAprobacion", SqlDbType.Timestamp, 0L, null, true);
        }

        public long uf_BlockDoc
        {
            get
            {
                return Convert.ToInt64(base.field("uf_BlockDoc").value);
            }
            set
            {
                base.field("uf_BlockDoc").value = value;
            }
        }

        public object uf_CBB
        {
            get
            {
                return base.field("uf_CBB").value;
            }
            set
            {
                base.field("uf_CBB").value = value;
            }
        }

        public DateTime uf_FechaAprobacion
        {
            get
            {
                return Convert.ToDateTime(base.field("uf_FechaAprobacion").value);
            }
            set
            {
                base.field("uf_FechaAprobacion").value = value;
            }
        }

        public long uf_FolioFinal
        {
            get
            {
                return Convert.ToInt64(base.field("uf_FolioFinal").value);
            }
            set
            {
                base.field("uf_FolioFinal").value = value;
            }
        }

        public long uf_FolioInicial
        {
            get
            {
                return Convert.ToInt64(base.field("uf_FolioInicial").value);
            }
            set
            {
                base.field("uf_FolioInicial").value = value;
            }
        }

        public long uf_NumAprobacion
        {
            get
            {
                return Convert.ToInt64(base.field("uf_NumAprobacion").value);
            }
            set
            {
                base.field("uf_NumAprobacion").value = value;
            }
        }

        public string uf_Serie
        {
            get
            {
                return Convert.ToString(base.field("uf_Serie").value);
            }
            set
            {
                base.field("uf_Serie").value = value;
            }
        }
    }
}

