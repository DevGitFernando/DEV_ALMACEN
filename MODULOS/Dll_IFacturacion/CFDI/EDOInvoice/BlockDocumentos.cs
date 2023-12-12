using System;
using System.Data; 
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_IFacturacion.CFDI.EDAS;

namespace Dll_IFacturacion.CFDI.EDOInvoice
{
    public class BlockDocumentos : eTable
    {
        public BlockDocumentos()
        {
            base.tableName = "BlockDocumentos";
            base.addField("Documento", SqlDbType.BigInt, 0L, null, true);
            base.addField("FUltimo", SqlDbType.BigInt, 0L, null, true);
            base.addField("IDControl", SqlDbType.VarChar, 0x20L, null, false);
            base.addField("Serie", SqlDbType.VarChar, 10L, null, false);
            base.addField("uf_CFDFolio", SqlDbType.BigInt, 0L, null, false);
            base.addField("uf_CFDI_Info", SqlDbType.BigInt, 0L, null, false);
        }

        public long Documento
        {
            get
            {
                return Convert.ToInt64(base.field("Documento").value);
            }
            set
            {
                base.field("Documento").value = value;
            }
        }

        public long FUltimo
        {
            get
            {
                return Convert.ToInt64(base.field("FUltimo").value);
            }
            set
            {
                base.field("FUltimo").value = value;
            }
        }

        public string IDControl
        {
            get
            {
                return Convert.ToString(base.field("IDControl").value);
            }
            set
            {
                base.field("IDControl").value = value;
            }
        }

        public string Serie
        {
            get
            {
                return Convert.ToString(base.field("Serie").value);
            }
            set
            {
                base.field("Serie").value = value;
            }
        }

        public long uf_CFDFolio
        {
            get
            {
                return Convert.ToInt64(base.field("uf_CFDFolio").value);
            }
            set
            {
                base.field("uf_CFDFolio").value = value;
            }
        }

        public long uf_CFDI_Info
        {
            get
            {
                return Convert.ToInt64(base.field("uf_CFDI_Info").value);
            }
            set
            {
                base.field("uf_CFDI_Info").value = value;
            }
        }
    }
}

