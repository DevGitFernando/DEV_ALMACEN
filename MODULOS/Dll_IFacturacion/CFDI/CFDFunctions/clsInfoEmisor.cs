using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Dll_IFacturacion.CFDI.EDOInvoice;

namespace Dll_IFacturacion.CFDI.CFDFunctions
{
    public class clsInfoEmisor
    {
        public Ciudad CiudadExpedicion = new Ciudad();
        public Ciudad CiudadFiscal = new Ciudad();
        public Domicilio DomicilioExpedicion = new Domicilio();
        public Domicilio DomicilioFiscal = new Domicilio();
        public EDOInvoice.Emisor Emisor = new EDOInvoice.Emisor();
        public Estado EstadoExpedicion = new Estado();
        public Estado EstadoFiscal = new Estado();
        public string lastError = "";
        public Pais PaisExpedicion = new Pais();
        public Pais PaisFiscal = new Pais();
        public ArrayList RegimenesFiscales = new ArrayList();

        public void addNew(SqlConnection connection)
        {
            this.Emisor.connection = connection;
            this.Emisor.addNew();
            this.DomicilioFiscal.connection = connection;
            this.DomicilioFiscal.addNew();
            this.DomicilioExpedicion.connection = connection;
            this.DomicilioExpedicion.addNew();
            this.CiudadFiscal.connection = connection;
            this.CiudadFiscal.addNew();
            this.CiudadExpedicion.connection = connection;
            this.CiudadExpedicion.addNew();
            this.EstadoFiscal.connection = connection;
            this.EstadoFiscal.addNew();
            this.EstadoExpedicion.connection = connection;
            this.EstadoExpedicion.addNew();
            this.PaisFiscal.connection = connection;
            this.PaisFiscal.addNew();
            this.PaisExpedicion.connection = connection;
            this.PaisExpedicion.addNew();
            this.RegimenesFiscales = new ArrayList();
        }

        public bool loadInfo(SqlConnection connection, string rfcEmisor)
        {
            try
            {
                this.addNew(connection);
                if (!this.Emisor.load("RFC='" + rfcEmisor + "'"))
                {
                    this.lastError = this.Emisor.lastError;
                    return false;
                }
                if (!this.DomicilioFiscal.loadBySys_PK(this.Emisor.DomicilioFiscal))
                {
                    this.lastError = this.DomicilioFiscal.lastError;
                    return false;
                }
                if (!this.CiudadFiscal.loadBySys_PK(this.DomicilioFiscal.ICiudad))
                {
                    this.lastError = this.CiudadFiscal.lastError;
                    return false;
                }
                if (!this.EstadoFiscal.loadBySys_PK(this.CiudadFiscal.Estado))
                {
                    this.lastError = this.EstadoFiscal.lastError;
                    return false;
                }
                if (!this.PaisFiscal.loadBySys_PK(this.EstadoFiscal.IPais))
                {
                    this.lastError = this.PaisFiscal.lastError;
                    return false;
                }
                if (!this.DomicilioExpedicion.loadBySys_PK(this.Emisor.DomicilioExpedicion))
                {
                    this.lastError = this.DomicilioExpedicion.lastError;
                    return false;
                }
                if (!this.CiudadExpedicion.loadBySys_PK(this.DomicilioExpedicion.ICiudad))
                {
                    this.lastError = this.CiudadExpedicion.lastError;
                    return false;
                }
                if (!this.EstadoExpedicion.loadBySys_PK(this.CiudadExpedicion.Estado))
                {
                    this.lastError = this.EstadoExpedicion.lastError;
                    return false;
                }
                if (!this.PaisExpedicion.loadBySys_PK(this.EstadoExpedicion.IPais))
                {
                    this.lastError = this.PaisExpedicion.lastError;
                    return false;
                }
                this.loadInfoRegimenFiscal();
                return true;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        public void loadInfoRegimenFiscal()
        {
            this.loadInfoRegimenFiscal(null);
        }

        public void loadInfoRegimenFiscal(SqlTransaction trans)
        {
            this.RegimenesFiscales.Clear();
            SqlCommand selectCommand = this.Emisor.connection.CreateCommand();
            if (trans != null)
            {
                selectCommand.Transaction = trans;
            }
            selectCommand.CommandType = CommandType.Text;
            selectCommand.CommandText = "SELECT Sys_PK,Regimen FROM RegimenEmisor ORDER BY Regimen";
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                this.RegimenesFiscales.Add(new clsInfoRegimenFiscal(row["Regimen"].ToString(), Convert.ToInt64(row["Sys_PK"])));
            }
            dataTable = null;
            adapter = null;
            selectCommand = null;
        }
    }
}

