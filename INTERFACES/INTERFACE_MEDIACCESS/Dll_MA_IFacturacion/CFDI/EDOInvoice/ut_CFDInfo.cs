using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_MA_IFacturacion.CFDI.EDAS;

namespace Dll_MA_IFacturacion.CFDI.EDOInvoice
{
    public class ut_CFDInfo : eTable
    {
        public ut_CFDInfo()
        {
            base.tableName = "ut_CFDInfo";
            base.addField("uf_Certificado", SqlDbType.VarChar, 0x800L, null, false);
            base.addField("uf_Clave", SqlDbType.VarChar, 0x800L, null, true);
            base.addField("uf_LlavePrivada", SqlDbType.VarChar, 0x800L, null, true);
            base.addField("uf_noCertificado", SqlDbType.VarChar, 20L, null, true);
            base.addField("uf_PFX", SqlDbType.VarChar, 0x800L, null, false);
        }

        public string uf_Certificado
        {
            get
            {
                return Convert.ToString(base.field("uf_Certificado").value);
            }
            set
            {
                base.field("uf_Certificado").value = value;
            }
        }

        public string uf_Clave
        {
            get
            {
                return Convert.ToString(base.field("uf_Clave").value);
            }
            set
            {
                base.field("uf_Clave").value = value;
            }
        }

        public string uf_LlavePrivada
        {
            get
            {
                return Convert.ToString(base.field("uf_LlavePrivada").value);
            }
            set
            {
                base.field("uf_LlavePrivada").value = value;
            }
        }

        public string uf_noCertificado
        {
            get
            {
                return Convert.ToString(base.field("uf_noCertificado").value);
            }
            set
            {
                base.field("uf_noCertificado").value = value;
            }
        }

        public string uf_PFX
        {
            get
            {
                return Convert.ToString(base.field("uf_PFX").value);
            }
            set
            {
                base.field("uf_PFX").value = value;
            }
        }
    }
}

