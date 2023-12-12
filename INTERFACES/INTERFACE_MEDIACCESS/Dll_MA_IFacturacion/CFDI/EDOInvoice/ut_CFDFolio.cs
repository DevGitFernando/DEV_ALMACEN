using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_MA_IFacturacion.CFDI.EDAS;

namespace Dll_MA_IFacturacion.CFDI.EDOInvoice
{
    public class ut_CFDFolio : eTable
    {
        public ut_CFDFolio()
        {
            base.tableName = "ut_CFDFolio";
            base.addField("uf_anoAprobacion", SqlDbType.Int, 0L, null, true);
            base.addField("uf_CFDFolioAnterior", SqlDbType.BigInt, 0L, null, false);
            base.addField("uf_CFDInfo", SqlDbType.BigInt, 0L, null, true);
            base.addField("uf_FolioFinal", SqlDbType.BigInt, 0L, null, true);
            base.addField("uf_FolioInicial", SqlDbType.BigInt, 0L, null, true);
            base.addField("uf_noAprobacion", SqlDbType.Int, 0L, null, true);
            base.addField("uf_Serie", SqlDbType.VarChar, 10L, null, false);
        }

        public int uf_anoAprobacion
        {
            get
            {
                return Convert.ToInt32(base.field("uf_anoAprobacion").value);
            }
            set
            {
                base.field("uf_anoAprobacion").value = value;
            }
        }

        public long uf_CFDFolioAnterior
        {
            get
            {
                return Convert.ToInt64(base.field("uf_CFDFolioAnterior").value);
            }
            set
            {
                base.field("uf_CFDFolioAnterior").value = value;
            }
        }

        public long uf_CFDInfo
        {
            get
            {
                return Convert.ToInt64(base.field("uf_CFDInfo").value);
            }
            set
            {
                base.field("uf_CFDInfo").value = value;
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

        public int uf_noAprobacion
        {
            get
            {
                return Convert.ToInt32(base.field("uf_noAprobacion").value);
            }
            set
            {
                base.field("uf_noAprobacion").value = value;
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

