using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_MA_IFacturacion.CFDI.EDAS;

namespace Dll_MA_IFacturacion.CFDI.EDOInvoice
{
    public class Domicilio : eTable
    {
        public Domicilio()
        {
            base.tableName = "Domicilio";
            base.addField("Colonia", SqlDbType.VarChar, 150L, null, false);
            base.addField("CPostal", SqlDbType.VarChar, 5L, null, false);
            base.addField("DirCalle", SqlDbType.VarChar, 0x400L, null, false);
            base.addField("DirNoExt", SqlDbType.VarChar, 15L, null, false);
            base.addField("DirNoInt", SqlDbType.VarChar, 15L, null, false);
            base.addField("Notas", SqlDbType.VarChar, 0x200L, null, false);
            base.addField("ICiudad", SqlDbType.BigInt, 0L, null, true);
        }

        public string Colonia
        {
            get
            {
                return Convert.ToString(base.field("Colonia").value);
            }
            set
            {
                base.field("Colonia").value = value;
            }
        }

        public string CPostal
        {
            get
            {
                return Convert.ToString(base.field("CPostal").value);
            }
            set
            {
                base.field("CPostal").value = value;
            }
        }

        public string DirCalle
        {
            get
            {
                return Convert.ToString(base.field("DirCalle").value);
            }
            set
            {
                base.field("DirCalle").value = value;
            }
        }

        public string DirNoExt
        {
            get
            {
                return Convert.ToString(base.field("DirNoExt").value);
            }
            set
            {
                base.field("DirNoExt").value = value;
            }
        }

        public string DirNoInt
        {
            get
            {
                return Convert.ToString(base.field("DirNoInt").value);
            }
            set
            {
                base.field("DirNoInt").value = value;
            }
        }

        public long ICiudad
        {
            get
            {
                return Convert.ToInt64(base.field("ICiudad").value);
            }
            set
            {
                base.field("ICiudad").value = value;
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
    }
}

