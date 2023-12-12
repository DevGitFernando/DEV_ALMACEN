using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_MA_IFacturacion.CFDI.EDAS;

namespace Dll_MA_IFacturacion.CFDI.EDOInvoice
{
    public class Usuario : eTable
    {
        public Usuario()
        {
            base.tableName = "Usuario";
            base.addField("Bloqueado", SqlDbType.Bit, 0L, null, false);
            base.addField("IDUsuario", SqlDbType.VarChar, 0x19L, null, true);
            base.addField("Nombre", SqlDbType.VarChar, 250L, null, true);
            base.addField("Notas", SqlDbType.VarChar, 0x1000L, null, false);
            base.addField("Pwd", SqlDbType.VarChar, 0x20L, null, true);
        }

        public bool Bloqueado
        {
            get
            {
                return Convert.ToBoolean(base.field("Bloqueado").value);
            }
            set
            {
                base.field("Bloqueado").value = value;
            }
        }

        public string IDUsuario
        {
            get
            {
                return Convert.ToString(base.field("IDUsuario").value);
            }
            set
            {
                base.field("IDUsuario").value = value;
            }
        }

        public string Nombre
        {
            get
            {
                return Convert.ToString(base.field("Nombre").value);
            }
            set
            {
                base.field("Nombre").value = value;
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

        public string Pwd
        {
            get
            {
                return Convert.ToString(base.field("Pwd").value);
            }
            set
            {
                base.field("Pwd").value = value;
            }
        }
    }
}

