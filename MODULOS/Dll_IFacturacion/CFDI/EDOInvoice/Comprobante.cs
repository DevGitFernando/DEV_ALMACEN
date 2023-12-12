using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_IFacturacion.CFDI.EDAS;

namespace Dll_IFacturacion.CFDI.EDOInvoice
{
    public class Comprobante : eTable
    {
        public Comprobante()
        {
            base.tableName = "Comprobante";
            base.addField("Descuentos", SqlDbType.Float, 0L, null, true);
            base.addField("Documento", SqlDbType.BigInt, 0L, null, true);
            base.addField("FechaHora", SqlDbType.Date, 0L, null, true);
            base.addField("Folio", SqlDbType.BigInt, 0L, null, true);
            base.addField("FormaPago", SqlDbType.Int, 0L, null, true);
            base.addField("Impuesto1", SqlDbType.Float, 0L, null, true);
            base.addField("Impuesto2", SqlDbType.Float, 0L, null, true);
            base.addField("Notas", SqlDbType.VarChar, 0x800L, null, false);
            base.addField("RetencionISR", SqlDbType.Float, 0L, null, false);
            base.addField("RetencionIVA", SqlDbType.Float, 0L, null, true);
            base.addField("Status", SqlDbType.BigInt, 0L, null, true);
            base.addField("Subtotal", SqlDbType.Float, 0L, null, true);
            base.addField("Block", SqlDbType.BigInt, 0L, null, true);
            base.addField("Cliente", SqlDbType.BigInt, 0L, null, true);
            base.addField("IEPS", SqlDbType.Float, 0L, 0, false);
            base.addField("ISH", SqlDbType.Float, 0L, 0, false);
        }

        public long Block
        {
            get
            {
                return Convert.ToInt64(base.field("Block").value);
            }
            set
            {
                base.field("Block").value = value;
            }
        }

        public long Cliente
        {
            get
            {
                return Convert.ToInt64(base.field("Cliente").value);
            }
            set
            {
                base.field("Cliente").value = value;
            }
        }

        public double Descuentos
        {
            get
            {
                return Convert.ToDouble(base.field("Descuentos").value);
            }
            set
            {
                base.field("Descuentos").value = value;
            }
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

        public DateTime FechaHora
        {
            get
            {
                return Convert.ToDateTime(base.field("FechaHora").value);
            }
            set
            {
                base.field("FechaHora").value = value;
            }
        }

        public long Folio
        {
            get
            {
                return Convert.ToInt64(base.field("Folio").value);
            }
            set
            {
                base.field("Folio").value = value;
            }
        }

        public int FormaPago
        {
            get
            {
                return Convert.ToInt32(base.field("FormaPago").value);
            }
            set
            {
                base.field("FormaPago").value = value;
            }
        }

        public double IEPS
        {
            get
            {
                return Convert.ToDouble(base.field("IEPS").value);
            }
            set
            {
                base.field("IEPS").value = value;
            }
        }

        public double Impuesto1
        {
            get
            {
                return Convert.ToDouble(base.field("Impuesto1").value);
            }
            set
            {
                base.field("Impuesto1").value = value;
            }
        }

        public double Impuesto2
        {
            get
            {
                return Convert.ToDouble(base.field("Impuesto2").value);
            }
            set
            {
                base.field("Impuesto2").value = value;
            }
        }

        public double ISH
        {
            get
            {
                return Convert.ToDouble(base.field("ISH").value);
            }
            set
            {
                base.field("ISH").value = value;
            }
        }

        public string Notas
        {
            get
            {
                return Convert.ToString(base.field("Notas").value);
            }
            set
            {
                base.field("Notas").value = value;
            }
        }

        public double RetencionISR
        {
            get
            {
                return Convert.ToDouble(base.field("RetencionISR").value);
            }
            set
            {
                base.field("RetencionISR").value = value;
            }
        }

        public double RetencionIVA
        {
            get
            {
                return Convert.ToDouble(base.field("RetencionIVA").value);
            }
            set
            {
                base.field("RetencionIVA").value = value;
            }
        }

        public long Status
        {
            get
            {
                return Convert.ToInt64(base.field("Status").value);
            }
            set
            {
                base.field("Status").value = value;
            }
        }

        public double Subtotal
        {
            get
            {
                return Convert.ToDouble(base.field("Subtotal").value);
            }
            set
            {
                base.field("Subtotal").value = value;
            }
        }
    }
}

