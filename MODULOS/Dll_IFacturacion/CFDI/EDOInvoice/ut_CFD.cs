using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_IFacturacion.CFDI.EDAS;

namespace Dll_IFacturacion.CFDI.EDOInvoice
{
    public class ut_CFD : eTable
    {
        public ut_CFD()
        {
            base.tableName = "ut_CFD";
            base.addField("uf_CadenaOriginal", SqlDbType.VarChar, 0x186a0L, null, true);
            base.addField("uf_CFDFolio", SqlDbType.BigInt, 0L, null, false);
            base.addField("uf_IVenta", SqlDbType.BigInt, 0L, null, true);
            base.addField("uf_SelloDigital", SqlDbType.VarChar, 0x186a0L, null, true);
            base.addField("uf_CFDI_Info", SqlDbType.BigInt, 0L, null, false);
            base.addField("uf_Tipo", SqlDbType.BigInt, 0L, 2, false);
            base.addField("uf_CanceladoSAT", SqlDbType.Int, 0L, 0, false);
            base.addField("uf_CadenaOriginalSAT", SqlDbType.VarChar, 0x186a0L, null, false);
            base.addField("uf_SelloDigitalSAT", SqlDbType.VarChar, 0x186a0L, null, false);
            base.addField("uf_FolioSAT", SqlDbType.VarChar, 50L, null, false);
            base.addField("uf_NoCertificadoSAT", SqlDbType.VarChar, 20L, null, false);
            base.addField("uf_FechaHoraCerSAT", SqlDbType.Timestamp, 0L, null, false);
            base.addField("uf_CBB", SqlDbType.VarBinary, 0L, null, false);
        }

        public string uf_CadenaOriginal
        {
            get
            {
                return Convert.ToString(base.field("uf_CadenaOriginal").value);
            }
            set
            {
                base.field("uf_CadenaOriginal").value = value;
            }
        }

        public string uf_CadenaOriginalSAT
        {
            get
            {
                return Convert.ToString(base.field("uf_CadenaOriginalSAT").value);
            }
            set
            {
                base.field("uf_CadenaOriginalSAT").value = value;
            }
        }

        public int uf_CanceladoSAT
        {
            get
            {
                return Convert.ToInt32(base.field("uf_CanceladoSAT").value);
            }
            set
            {
                base.field("uf_CanceladoSAT").value = value;
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

        public DateTime uf_FechaHoraCerSAT
        {
            get
            {
                return Convert.ToDateTime(base.field("uf_FechaHoraCerSAT").value);
            }
            set
            {
                base.field("uf_FechaHoraCerSAT").value = value;
            }
        }

        public string uf_FolioSAT
        {
            get
            {
                return Convert.ToString(base.field("uf_FolioSAT").value);
            }
            set
            {
                base.field("uf_FolioSAT").value = value;
            }
        }

        public long uf_IVenta
        {
            get
            {
                return Convert.ToInt64(base.field("uf_IVenta").value);
            }
            set
            {
                base.field("uf_IVenta").value = value;
            }
        }

        public string uf_NoCertificadoSAT
        {
            get
            {
                return Convert.ToString(base.field("uf_NoCertificadoSAT").value);
            }
            set
            {
                base.field("uf_NoCertificadoSAT").value = value;
            }
        }

        public string uf_SelloDigital
        {
            get
            {
                return Convert.ToString(base.field("uf_SelloDigital").value);
            }
            set
            {
                base.field("uf_SelloDigital").value = value;
            }
        }

        public string uf_SelloDigitalSAT
        {
            get
            {
                return Convert.ToString(base.field("uf_SelloDigitalSAT").value);
            }
            set
            {
                base.field("uf_SelloDigitalSAT").value = value;
            }
        }

        public long uf_Tipo
        {
            get
            {
                return Convert.ToInt64(base.field("uf_Tipo").value);
            }
            set
            {
                base.field("uf_Tipo").value = value;
            }
        }
    }
}

