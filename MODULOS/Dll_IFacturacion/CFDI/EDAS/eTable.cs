using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using Dll_IFacturacion.CFDI; 

namespace Dll_IFacturacion.CFDI.EDAS
{
    public class eTable
    {
        private SqlConnection mconnection;
        private enumobjActions mcurrentAction;
        private ArrayList mfields = new ArrayList();
        private string mlastError;
        private string mtableName;
        private SqlTransaction mtransaction;

        public eTable()
        {
            this.lastError = "";
            this.tableName = "";
            this.includeSysFields();
        }

        public bool addField(string name, SqlDbType type, long size, object value, bool required)
        {
            try
            {
                this.lastError = "";
                eField field = new eField(name, type, size, value, required);
                if (field.isOK())
                {
                    this.mfields.Add(field);
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        public void addNew()
        {
            this.clear();
            this.field("sys_GUID").value = Guid.NewGuid().ToString("N");
            this.currentAction = enumobjActions.cAddNew;
        }

        private void afterUpdate()
        {
            try
            {                  
                SqlCommand command = new SqlCommand();
                command.Connection = this.connection;
                command.CommandType = CommandType.Text;
                command.CommandText = string.Concat(new object[] { "SELECT Sys_TimeStamp FROM ", this.tableName, " WHERE Sys_PK=", this.sys_PK });
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.HasRows)
                {
                    reader.Read();
                    this.field("Sys_TimeStamp").value = reader.GetDateTime(0);
                }
                else
                {
                    this.lastError = "No se pudo actualizar Sys_TimeStamp";
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();
            }
            catch (Exception exception)
            {
                this.lastError = "No se pudo actualizar Sys_TimeStamp" + exception.Message;
            }
            foreach (eField field in this.mfields)
            {
                field.isDirty = false;
            }
        }

        private void clear()
        {
            this.lastError = "";
            foreach (eField field in this.mfields)
            {
                field.clear();
            }
            this.currentAction = enumobjActions.cNothing;
        }

        public bool delete()
        {
            if (this.sys_PK < 1L)
            {
                this.lastError = "Clave primaria incorrecta.";
                return false;
            }
            if (!this.deleteWhere("Sys_PK=" + this.sys_PK))
            {
                return false;
            }
            return true;
        }

        public bool delete(long pk)
        {
            if (pk == this.sys_PK)
            {
                return this.delete();
            }
            if (pk < 1L)
            {
                this.lastError = "Clave primaria incorrecta.";
                return false;
            }
            return this.deleteWhere("Sys_PK=" + pk);
        }

        public bool delete(string where)
        {
            this.lastError = "";
            return this.deleteWhere(where);
        }

        private bool deleteWhere(string where)
        {
            try
            {
                if (!this.isConnectionOK())
                {
                    return false;
                }
                if (this.tableName == null)
                {
                    this.tableName = "";
                }
                if (this.tableName == "")
                {
                    this.lastError = "Nombre de tabla inv\x00e1lido";
                    return false;
                }
                SqlCommand command = new SqlCommand();
                command.Connection = this.connection;
                command.Transaction = this.transaction;
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM " + this.tableName + " WHERE " + where;
                if (command.ExecuteNonQuery() < 1)
                {
                    this.lastError = "Error al eliminar registro.";
                    return false;
                }
                this.field("Sys_PK").value = 0;
                this.currentAction = enumobjActions.cNothing;
                return true;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        public eField field(string name)
        {
            try
            {
                this.lastError = "";
                foreach (eField field in this.mfields)
                {
                    if (field.name.ToLower() == name.ToLower())
                    {
                        return field;
                    }
                }
                return null;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return null;
            }
        }

        private SqlCommand getAddNewCommand()
        {
            string str = "";
            string str2 = "";
            try
            {
                SqlCommand command = new SqlCommand();
                foreach (eField field in this.mfields)
                {
                    if (((field.name.ToLower() != "sys_pk") && (field.name.ToLower() != "sys_timestamp")) && (field.name.ToLower() != "sys_dtcreated"))
                    {
                        if (field.value != null)
                        {                           
                            str = str + "," + field.name;
                            SqlParameter parameter = new SqlParameter(field.name, field.type);
                            if ((((field.type == SqlDbType.VarChar) || (field.type == SqlDbType.VarChar)) || ((field.type == SqlDbType.VarChar) || (field.type == SqlDbType.VarChar))) || (field.type == SqlDbType.Char))
                            {
                                if ((field.value != null) && ((field.value.ToString().Length > field.size) && (field.size < 0xfa0L)))
                                {
                                    field.value = field.value.ToString().Substring(0, (int) field.size);
                                }
                                parameter.Value = field.value;
                            }
                            else
                            {
                                parameter.Value = field.value;
                            }
                            command.Parameters.Add(parameter);
                            str2 = str2 + ",?";
                        }
                        else if (field.required)
                        {
                            this.lastError = "El campo " + field.name + " no puede estar vac\x00edo.";
                            return null;
                        }
                    }
                }
                str = "Sys_TimeStamp,Sys_DTCreated" + str;
                str2 = "NOW(),NOW()" + str2;
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO " + this.tableName + "(" + str + ") VALUES(" + str2 + ");";
                return command;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return null;
            }
        }

        private SqlCommand getUpdateCommand()
        {
            string str = "";
            try
            {
                SqlCommand command = new SqlCommand();
                foreach (eField field in this.mfields)
                {
                    if (((((field.name.ToLower() != "sys_pk") && (field.name.ToLower() != "sys_timestamp")) && (field.name.ToLower() != "sys_dtcreated")) && (field.name.ToLower() != "sys_guid")) && field.isDirty)
                    {
                        if ((field.value == null) && field.required)
                        {
                            this.lastError = "Imposible continuar, el campo " + field.name + " no puede ser Nulo.";
                            return null;
                        }
                        SqlParameter parameter = new SqlParameter(field.name, field.type);
                        if ((((field.type == SqlDbType.VarChar) || (field.type == SqlDbType.VarChar)) || ((field.type == SqlDbType.VarChar) || (field.type == SqlDbType.VarChar))) || (field.type == SqlDbType.Char))
                        {
                            if ((field.value != null) && ((field.value.ToString().Length > field.size) && (field.size < 0xfa0L)))
                            {
                                field.value = field.value.ToString().Substring(0, (int) field.size);
                            }
                            parameter.Value = field.value;
                        }
                        else
                        {
                            parameter.Value = field.value;
                        }
                        command.Parameters.Add(parameter);
                        str = str + "," + field.name + "=?";
                    }
                }
                ////AQUI str = "Sys_TimeStamp=getdate()" + str;
                command.CommandType = CommandType.Text;
                DateTime time = Convert.ToDateTime(this.field("Sys_TimeStamp").value);
                command.CommandText = "UPDATE " + this.tableName + " SET " + str + " WHERE Sys_PK = " + this.field("Sys_PK").value.ToString();
                return command;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return null;
            }
        }

        private void includeSysFields()
        {
            ////this.addField("Sys_PK", SqlDbType.BigInt, 0L, 0, true);
            ////this.addField("Sys_TimeStamp", SqlDbType.Timestamp, 0L, null, true);
            ////this.addField("Sys_GUID", SqlDbType.VarChar, 0x20L, null, true);
            ////this.addField("Sys_DTCreated", SqlDbType.Timestamp, 0L, null, false);
            ////this.addField("Sys_User", SqlDbType.VarChar, 5L, null, false);
            ////this.addField("Sys_LastUser", SqlDbType.VarChar, 5L, null, false);
            ////this.addField("Sys_Exported", SqlDbType.Bit, 0L, null, false);
            ////this.addField("Sys_DTExported", SqlDbType.Timestamp, 0L, null, false);
            ////this.addField("Sys_Info", SqlDbType.VarChar, 0x20L, null, false);
        }

        private bool isConnectionOK()
        {
            if (this.connection == null)
            {
                this.lastError = "A\x00fan no se ha asignado una conexi\x00f3n a este objeto.";
                return false;
            }
            if (this.connection.State != ConnectionState.Open)
            {
                this.lastError = "La conexi\x00f3n esta cerrada";
                return false;
            }
            return true;
        }

        public bool load(string where)
        {
            this.lastError = "";
            if (where == null)
            {
                where = "";
            }
            if (where == "")
            {
                this.lastError = "par\x00e1metro incorrecto. Falta el filtro.";
                return false;
            }
            if (!this.loadBySql(where))
            {
                return false;
            }
            this.currentAction = enumobjActions.cEdit;
            return true;
        }

        private bool loadBySql(string where)
        {
            this.clear();
            if (this.tableName == null)
            {
                this.tableName = "";
            }
            if (this.tableName == "")
            {
                this.lastError = "Nombre de tabla inv\x00e1lido.";
                return false;
            }
            if (!this.isConnectionOK())
            {
                return false;
            }
            string str = "";
            int num = 0;
            foreach (eField field in this.mfields)
            {
                if (str != "")
                {
                    str = str + ", ";
                }
                str = str + field.name;
                field.index = num;
                num++;
            }
            if (str == "")
            {
                this.lastError = "No existen campos en el arreglo.";
                return false;
            }
            str = "SELECT " + str + " FROM " + this.tableName + " WHERE " + where;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = str;
                command.CommandType = CommandType.Text;
                command.Connection = this.connection;
                command.Transaction = this.transaction;
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
                if (!reader.HasRows)
                {
                    this.lastError = "No se encontr\x00f3 el registro.";
                    reader.Close();
                    return false;
                }
                if (!reader.Read())
                {
                    this.lastError = "Error en: SqlDataReader.Read()";
                    reader.Close();
                    return false;
                }
                foreach (eField field in this.mfields)
                {
                    if (!reader.IsDBNull(field.index))
                    {
                        field.value = reader.GetValue(field.index);
                        field.isDirty = false;
                    }
                }
                if (this.sys_PK < 1L)
                {
                    this.lastError = "Error al cargar.";
                    reader.Close();
                    return false;
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();
                return true;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        public bool loadBySys_GUID(string guid)
        {
            this.lastError = "";
            if (guid == null)
            {
                guid = "";
            }
            if (guid == "")
            {
                this.lastError = "par\x00e1metro incorrecto. Sys_GUID=Vac\x00edo";
                return false;
            }
            string where = "Sys_GUID='" + guid + "'";
            if (!this.loadBySql(where))
            {
                return false;
            }
            this.currentAction = enumobjActions.cEdit;
            return true;
        }

        public bool loadBySys_PK(long pk)
        {
            this.lastError = "";
            if (pk < 1L)
            {
                this.lastError = "par\x00e1metro incorrecto. Sys_PK=" + pk.ToString();
                return false;
            }
            string where = "Sys_PK=" + pk.ToString();
            if (!this.loadBySql(where))
            {
                return false;
            }
            this.currentAction = enumobjActions.cEdit;
            return true;
        }

        public bool update()
        {
            this.lastError = "";
            try
            {
                SqlCommand command;
                if (!this.isConnectionOK())
                {
                    return false;
                }
                if (this.tableName == null)
                {
                    this.tableName = "";
                }
                if (this.tableName == "")
                {
                    this.lastError = "Nombre de tabla inv\x00e1lido.";
                    return false;
                }
                if (this.currentAction == enumobjActions.cNothing)
                {
                    this.lastError = "Ninguna acci\x00f3n asociada. Ejecute addNew() antes de agregar un registro.";
                    return false;
                }
                if (this.currentAction == enumobjActions.cAddNew)
                {
                    command = this.getAddNewCommand();
                }
                else
                {
                    command = this.getUpdateCommand();
                }
                if (command == null)
                {
                    return false;
                }
                command.Connection = this.connection;
                command.Transaction = this.transaction;
                if (command.ExecuteNonQuery() < 1)
                {
                    this.lastError = "Error al actualizar registro.";
                    return false;
                }
                if (this.currentAction == enumobjActions.cAddNew)
                {
                    command.CommandText = "SELECT Sys_PK FROM " + this.tableName + " WHERE Sys_GUID='" + this.sys_GUID + "'";
                    object obj2 = command.ExecuteScalar();
                    long num2 = 0L;
                    if (obj2 != null)
                    {
                        num2 = Convert.ToInt64(obj2);
                    }
                    if (num2 < 1L)
                    {
                        this.lastError = "Error al obtener \x00faltimo identificador (Sys_PK) agregado.";
                    }
                    this.field("Sys_PK").value = num2;
                    this.currentAction = enumobjActions.cEdit;
                }
                this.afterUpdate();
                command.Dispose();
                return true;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        public SqlConnection connection
        {
            get
            {
                return this.mconnection;
            }
            set
            {
                this.mconnection = value;
            }
        }

        public enumobjActions currentAction
        {
            get
            {
                return this.mcurrentAction;
            }
            set
            {
                this.mcurrentAction = value;
            }
        }

        public string lastError
        {
            get
            {
                return this.mlastError;
            }
            set
            {
                this.mlastError = value;
            }
        }

        public DateTime sys_DTCreated
        {
            get
            {
                return Convert.ToDateTime(this.field("Sys_DTCreated").value);
            }
        }

        public DateTime sys_DTExported
        {
            get
            {
                return Convert.ToDateTime(this.field("Sys_DTExported").value);
            }
            set
            {
                this.field("Sys_DTExported").value = value;
            }
        }

        public bool sys_Exported
        {
            get
            {
                return Convert.ToBoolean(this.field("Sys_Exported").value);
            }
            set
            {
                this.field("Sys_Exported").value = value;
            }
        }

        public string sys_GUID
        {
            get
            {
                return Convert.ToString(this.field("Sys_GUID").value);
            }
        }

        public string sys_Info
        {
            get
            {
                return Convert.ToString(this.field("Sys_Info").value);
            }
            set
            {
                this.field("Sys_Info").value = value;
            }
        }

        public string Sys_LastUser
        {
            get
            {
                return Convert.ToString(this.field("Sys_LastUser").value);
            }
            set
            {
                this.field("Sys_LastUser").value = value;
            }
        }

        public long sys_PK
        {
            get
            {
                return Convert.ToInt64(this.field("Sys_PK").value);
            }
        }

        public DateTime sys_TimeStamp
        {
            get
            {
                return Convert.ToDateTime(this.field("Sys_TimeStamp").value);
            }
        }

        public string sys_User
        {
            get
            {
                return Convert.ToString(this.field("Sys_User").value);
            }
            set
            {
                this.field("Sys_User").value = value;
            }
        }

        public string tableName
        {
            get
            {
                return this.mtableName;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                this.mtableName = value;
            }
        }

        public SqlTransaction transaction
        {
            get
            {
                return this.mtransaction;
            }
            set
            {
                this.mtransaction = value;
            }
        }

        public enum enumobjActions
        {
            cNothing,
            cAddNew,
            cEdit
        }
    }
}

