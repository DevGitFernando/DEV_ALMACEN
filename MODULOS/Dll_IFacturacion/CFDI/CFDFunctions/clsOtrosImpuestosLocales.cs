using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

using Dll_IFacturacion.CFDI.EDOInvoice;
using Dll_IFacturacion.CFDI.geCFD;

namespace Dll_IFacturacion.CFDI.CFDFunctions
{
    public class clsOtrosImpuestosLocales
    {
        public SqlConnection connection;
        public string lastErr = "";
        private ArrayList mRetencionesLocales = new ArrayList();
        private double mTotaldeRetenciones;
        private double mTotaldeTraslados;
        private ArrayList mTrasladosLocales = new ArrayList();
        private double mversion = 1.0;

        public void add_and_sum_Retencion(clsRetencionLocal r)
        {
            this.RetencionesLocales.Add(r);
            this.TotaldeRetenciones += r.Importe;
        }

        public void add_and_sum_Traslado(clsTrasladoLocal t)
        {
            this.TrasladosLocales.Add(t);
            this.TotaldeTraslados += t.Importe;
        }

        public double def_decimales(double v)
        {
            return Math.Round(v, 2);
        }

        public bool load(long comprobante_pk)
        {
            try
            {
                this.TotaldeRetenciones = 0.0;
                this.TotaldeTraslados = 0.0;
                this.RetencionesLocales = new ArrayList();
                this.TrasladosLocales = new ArrayList();
                SqlCommand selectCommand = this.connection.CreateCommand();
                selectCommand.CommandType = CommandType.Text;
                selectCommand.CommandText = "SELECT * FROM OtrosImpuestosLocales WHERE FK_Comprobante=" + comprobante_pk.ToString();
                SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                double num = 0.0;
                double num2 = 0.0;
                foreach (DataRow row in dataTable.Rows)
                {
                    if (Convert.ToDouble(row["Importe"]) == 0.0)
                    {
                        this.lastErr = "El importe no puede ser cero en el impuesto local.";
                        return false;
                    }
                    if (Convert.ToDouble(row["Importe"]) > 0.0)
                    {
                        clsTrasladoLocal local2 = new clsTrasladoLocal();
                        local2.ImpLocTrasladado = row["Nombre"].ToString();
                        local2.TasadeTraslado = Convert.ToDouble(row["Tasa"]);
                        local2.Importe = Convert.ToDouble(row["Importe"]);
                        this.TrasladosLocales.Add(local2);
                        num += local2.Importe;
                        local2 = null;
                    }
                    else
                    {
                        clsRetencionLocal local = new clsRetencionLocal();
                        local.ImpLocRetenido = row["Nombre"].ToString();
                        local.TasadeRetencion = Convert.ToDouble(row["Tasa"]);
                        local.Importe = Convert.ToDouble(row["Importe"]) * -1.0;
                        this.RetencionesLocales.Add(local);
                        num2 += local.Importe;
                        local = null;
                    }
                }
                this.TotaldeTraslados = num;
                this.TotaldeRetenciones = num2;
                dataTable.Dispose();
                adapter.Dispose();
                return true;
            }
            catch (Exception exception)
            {
                this.lastErr = exception.Message;
                return false;
            }
        }

        public bool save(long comprobante_pk, SqlConnection cnn, SqlTransaction trans)
        {
            SqlTransaction transaction = (trans == null) ? cnn.BeginTransaction() : trans;
            try
            {
                SqlCommand command = cnn.CreateCommand();
                command.Transaction = transaction;
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM OtrosImpuestosLocales WHERE FK_Comprobante=" + comprobante_pk.ToString();
                command.ExecuteNonQuery();
                foreach (clsRetencionLocal local in this.RetencionesLocales)
                {
                    command.CommandText = string.Concat(new object[] { "INSERT INTO OtrosImpuestosLocales(Nombre,Tasa,Importe,FK_Comprobante) VALUES('", local.ImpLocRetenido, "',", local.TasadeRetencion.ToString(), ",", Convert.ToString((double) (local.Importe * -1.0)), ",", comprobante_pk, ")" });
                    command.ExecuteNonQuery();
                }
                foreach (clsTrasladoLocal local2 in this.TrasladosLocales)
                {
                    command.CommandText = string.Concat(new object[] { "INSERT INTO OtrosImpuestosLocales(Nombre,Tasa,Importe,FK_Comprobante) VALUES('", local2.ImpLocTrasladado, "',", local2.TasadeTraslado.ToString(), ",", local2.Importe.ToString(), ",", comprobante_pk, ")" });
                    command.ExecuteNonQuery();
                }
                if (trans == null)
                {
                    transaction.Commit();
                }
                return true;
            }
            catch (Exception)
            {
                if (trans == null)
                {
                    transaction.Rollback();
                }
                return false;
            }
        }

        public ArrayList RetencionesLocales
        {
            get
            {
                return this.mRetencionesLocales;
            }
            set
            {
                this.mRetencionesLocales = value;
            }
        }

        public double TotaldeRetenciones
        {
            get
            {
                return this.mTotaldeRetenciones;
            }
            set
            {
                this.mTotaldeRetenciones = this.def_decimales(value);
            }
        }

        public double TotaldeTraslados
        {
            get
            {
                return this.mTotaldeTraslados;
            }
            set
            {
                this.mTotaldeTraslados = this.def_decimales(value);
            }
        }

        public ArrayList TrasladosLocales
        {
            get
            {
                return this.mTrasladosLocales;
            }
            set
            {
                this.mTrasladosLocales = value;
            }
        }

        public double version
        {
            get
            {
                return this.mversion;
            }
            set
            {
                this.mversion = value;
            }
        }
    }
}

