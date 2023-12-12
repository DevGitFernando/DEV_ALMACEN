using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_IFacturacion.CFDI.EDAS;

namespace Dll_IFacturacion.CFDI.EDOInvoice
{
    public class Producto : eTable
    {
        public Producto()
        {
            base.tableName = "Producto";
            base.addField("CA1", SqlDbType.VarChar, 0x400L, null, false);
            base.addField("CA2", SqlDbType.VarChar, 0x400L, null, false);
            base.addField("CA3", SqlDbType.VarChar, 0x400L, null, false);
            base.addField("Codigo", SqlDbType.VarChar, 15L, null, true);
            base.addField("Descripcion", SqlDbType.VarChar, 0x1400L, null, true);
            base.addField("Impuesto1", SqlDbType.Float, 0L, null, true);
            base.addField("Impuesto2", SqlDbType.Float, 0L, null, true);
            base.addField("Notas", SqlDbType.VarChar, 0x800L, null, false);
            base.addField("Precio", SqlDbType.Money, 0L, null, true);
            base.addField("Uf_Complemento", SqlDbType.Bit, 0L, null, true);
            base.addField("Unidad", SqlDbType.VarChar, 15L, null, false);
        }

        public string CA1
        {
            get
            {
                return Convert.ToString(base.field("CA1").value);
            }
            set
            {
                base.field("CA1").value = value;
            }
        }

        public string CA2
        {
            get
            {
                return Convert.ToString(base.field("CA2").value);
            }
            set
            {
                base.field("CA2").value = value;
            }
        }

        public string CA3
        {
            get
            {
                return Convert.ToString(base.field("CA3").value);
            }
            set
            {
                base.field("CA3").value = value;
            }
        }

        public string Codigo
        {
            get
            {
                return Convert.ToString(base.field("Codigo").value);
            }
            set
            {
                base.field("Codigo").value = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return Convert.ToString(base.field("Descripcion").value);
            }
            set
            {
                base.field("Descripcion").value = value;
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

        public bool Uf_Complemento
        {
            get
            {
                return Convert.ToBoolean(base.field("Uf_Complemento").value);
            }
            set
            {
                base.field("Uf_Complemento").value = value;
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

