using System;
using System.Data; 
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Dll_IFacturacion.CFDI.EDAS
{
    public class eField
    {
        private int mindex;
        private bool misDirty;
        private string mname;
        private object moldValue;
        private object moriginalValue;
        private bool mrequired;
        private long msize;
        private SqlDbType mtype;
        private object mvalue;

        public eField()
        {
            this.mindex = -1;
        }

        public eField(string vname, SqlDbType vtype, long vsize, object vvalue, bool vrequired)
        {
            this.name = vname;
            this.type = vtype;
            this.size = vsize;
            this.value = vvalue;
            this.required = vrequired;
            this.moriginalValue = vvalue;
            this.isDirty = false;
            this.mindex = -1;
        }

        public void clear()
        {
            this.mvalue = null;
            this.misDirty = false;
        }

        public bool isOK()
        {
            if (this.name == null)
            {
                return false;
            }

            if (this.name == "")
            {
                return false;
            }

            if (this.type == SqlDbType.VarChar && (this.size <= 0L))
            {
                return false;
            }
            return true;
        }

        public void reset()
        {
            this.value = this.moriginalValue;
            this.isDirty = false;
        }

        public void unDo()
        {
            if (this.isDirty)
            {
                this.value = this.moldValue;
            }
        }

        public int index
        {
            get
            {
                return this.mindex;
            }
            set
            {
                this.mindex = value;
            }
        }

        public bool isDirty
        {
            get
            {
                return this.misDirty;
            }
            set
            {
                this.misDirty = value;
            }
        }

        public string name
        {
            get
            {
                return this.mname;
            }
            set
            {
                this.mname = value;
            }
        }

        private object oldValue
        {
            get
            {
                return this.moldValue;
            }
        }

        public object originalValue
        {
            get
            {
                return this.moriginalValue;
            }
        }

        public bool required
        {
            get
            {
                return this.mrequired;
            }
            set
            {
                this.mrequired = value;
            }
        }

        public long size
        {
            get
            {
                return this.msize;
            }
            set
            {
                this.msize = value;
            }
        }

        public SqlDbType type
        {
            get
            {
                return this.mtype;
            }
            set
            {
                this.mtype = value;
            }
        }

        public object value
        {
            get
            {
                return this.mvalue;
            }
            set
            {
                this.moldValue = this.mvalue;
                this.mvalue = value;
                this.isDirty = true;
            }
        }
    }
}

