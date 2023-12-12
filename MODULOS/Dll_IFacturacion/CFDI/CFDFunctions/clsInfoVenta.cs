using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_IFacturacion.CFDI.EDOInvoice;

namespace Dll_IFacturacion.CFDI.CFDFunctions
{
    public class clsInfoVenta
    {
        public ArrayList arrDComprobante = new ArrayList();
        public Ciudad CliCiudadFiscal = new Ciudad();
        public Domicilio CliDomicilioFiscal = new Domicilio();
        public EDOInvoice.Cliente Cliente = new EDOInvoice.Cliente();
        public Estado CliEstadoFiscal = new Estado();
        public Pais CliPaisFiscal = new Pais();
        public EDOInvoice.Comprobante Comprobante = new EDOInvoice.Comprobante();
        public string lastError = "";

        private void addExtraFields()
        {
            this.Comprobante.addField("IDivisa", SqlDbType.BigInt, 0L, null, true);
            this.Comprobante.addField("TipoCambio", SqlDbType.Decimal, 0L, null, true);
            this.Comprobante.addField("XMLFormaPago", SqlDbType.VarChar, 0xffL, null, false);
            this.Comprobante.addField("XMLMetodoPago", SqlDbType.VarChar, 100L, null, false);
            this.Comprobante.addField("XMLNumeroCuentaPago", SqlDbType.VarChar, 50L, null, false);
            this.Comprobante.addField("XMLCondicionesPago", SqlDbType.VarChar, 0xffL, null, false);
            this.Comprobante.addField("TipoRecibo", SqlDbType.BigInt, 0L, null, false);
            this.Comprobante.addField("NumeroCuentaPredial", SqlDbType.VarChar, 0xfde8L, null, false);
        }

        public bool loadInfo(SqlConnection connection, long pkComprobante)
        {
            try
            {
                this.arrDComprobante.Clear();
                this.Comprobante.connection = connection;
                this.Cliente.connection = connection;
                this.CliDomicilioFiscal.connection = connection;
                this.CliCiudadFiscal.connection = connection;
                this.CliEstadoFiscal.connection = connection;
                this.CliPaisFiscal.connection = connection;
                this.Comprobante.addNew();
                this.Cliente.addNew();
                this.CliDomicilioFiscal.addNew();
                this.CliCiudadFiscal.addNew();
                this.CliEstadoFiscal.addNew();
                this.CliPaisFiscal.addNew();
                this.addExtraFields();
                if (!this.Comprobante.loadBySys_PK(pkComprobante))
                {
                    this.lastError = this.Comprobante.lastError;
                    return false;
                }
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Sys_PK FROM DComprobante WHERE FKComprobante=" + this.Comprobante.sys_PK, connection);
                if (adapter == null)
                {
                    this.lastError = "No se encontraron conceptos en este comprobante.";
                    return false;
                }
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    DComprobante comprobante = new DComprobante();
                    comprobante.connection = connection;
                    if (!comprobante.loadBySys_PK(Convert.ToInt64(row["Sys_PK"])))
                    {
                        this.lastError = "Error al cargar conceptos. " + comprobante.lastError;
                        return false;
                    }
                    this.arrDComprobante.Add(comprobante);
                }
                if (this.arrDComprobante.Count < 1)
                {
                    this.lastError = "No se encontr\x00f3 ning\x00fan concepto cobrado en el documento actual.";
                    return false;
                }
                if (!this.Cliente.loadBySys_PK(this.Comprobante.Cliente))
                {
                    this.lastError = this.Cliente.lastError;
                    return false;
                }
                if (!this.CliDomicilioFiscal.loadBySys_PK(this.Cliente.Domicilio1))
                {
                    this.lastError = this.CliDomicilioFiscal.lastError;
                    return false;
                }
                if (!this.CliCiudadFiscal.loadBySys_PK(this.CliDomicilioFiscal.ICiudad))
                {
                    this.lastError = this.CliCiudadFiscal.lastError;
                    return false;
                }
                if (!this.CliEstadoFiscal.loadBySys_PK(this.CliCiudadFiscal.Estado))
                {
                    this.lastError = this.CliEstadoFiscal.lastError;
                    return false;
                }
                if (!this.CliPaisFiscal.loadBySys_PK(this.CliEstadoFiscal.IPais))
                {
                    this.lastError = this.CliPaisFiscal.lastError;
                    return false;
                }
                return true;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }
    }
}

