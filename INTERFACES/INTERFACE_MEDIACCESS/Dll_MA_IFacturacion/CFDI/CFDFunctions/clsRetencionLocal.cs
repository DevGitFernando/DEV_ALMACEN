using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

using Dll_MA_IFacturacion.CFDI.EDOInvoice;
using Dll_MA_IFacturacion.CFDI.geCFD;

namespace Dll_MA_IFacturacion.CFDI.CFDFunctions
{
    public class clsRetencionLocal
    {
        private string mImpLocRetenido = "";
        private double mImporte;
        private double mTasadeRetencion;

        public double def_decimales(double v)
        {
            return Math.Round(v, 2);
        }

        public string ImpLocRetenido
        {
            get
            {
                return this.mImpLocRetenido;
            }
            set
            {
                this.mImpLocRetenido = value;
            }
        }

        public double Importe
        {
            get
            {
                return this.mImporte;
            }
            set
            {
                this.mImporte = this.def_decimales(value);
            }
        }

        public double TasadeRetencion
        {
            get
            {
                return this.mTasadeRetencion;
            }
            set
            {
                this.mTasadeRetencion = this.def_decimales(value);
            }
        }
    }
}

