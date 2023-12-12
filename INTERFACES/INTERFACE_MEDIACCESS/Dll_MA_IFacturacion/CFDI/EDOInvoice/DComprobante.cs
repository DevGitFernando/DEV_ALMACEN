using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_MA_IFacturacion.CFDI.EDAS;

namespace Dll_MA_IFacturacion.CFDI.EDOInvoice
{
    public class DComprobante : eTable
    {
        public DComprobante()
        {
            base.tableName = "DComprobante";
            base.addField("Cantidad", SqlDbType.Float, 0L, null, true);
            base.addField("Descuentos", SqlDbType.Float, 0L, null, true);
            base.addField("Impuesto1", SqlDbType.Float, 0L, null, true);
            base.addField("Impuesto2", SqlDbType.Float, 0L, null, true);
            base.addField("Notas", SqlDbType.VarChar, 0x400L, null, false);
            base.addField("Precio", SqlDbType.Float, 0L, null, true);
            base.addField("RetencionISR", SqlDbType.Float, 0L, null, true);
            base.addField("RetencionIVA", SqlDbType.Float, 0L, null, true);
            base.addField("FKComprobante", SqlDbType.BigInt, 0L, null, true);
            base.addField("IProducto", SqlDbType.BigInt, 0L, null, true);
            base.addField("Unidad", SqlDbType.VarChar, 15L, null, false);
        }

        public double Cantidad
        {
            get
            {
                return Convert.ToDouble(base.field("Cantidad").value);
            }
            set
            {
                base.field("Cantidad").value = value;
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

        public long FKComprobante
        {
            get
            {
                return Convert.ToInt64(base.field("FKComprobante").value);
            }
            set
            {
                base.field("FKComprobante").value = value;
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

        public long IProducto
        {
            get
            {
                return Convert.ToInt64(base.field("IProducto").value);
            }
            set
            {
                base.field("IProducto").value = value;
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

        public double Precio
        {
            get
            {
                return Convert.ToDouble(base.field("Precio").value);
            }
            set
            {
                base.field("Precio").value = value;
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

        public string Unidad
        {
            get
            {
                return Convert.ToString(base.field("Unidad").value);
            }
            set
            {
                base.field("Unidad").value = value;
            }
        }
    }
}

