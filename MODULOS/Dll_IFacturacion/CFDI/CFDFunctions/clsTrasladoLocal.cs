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
    public class clsTrasladoLocal
    {
        private string mImpLocTrasladado = "";
        private double mImporte;
        private double mTasadeTraslado;

        public double def_decimales(double v)
        {
            return Math.Round(v, 2);
        }

        public string ImpLocTrasladado
        {
            get
            {
                return this.mImpLocTrasladado;
            }
            set
            {
                this.mImpLocTrasladado = value;
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

        public double TasadeTraslado
        {
            get
            {
                return this.mTasadeTraslado;
            }
            set
            {
                this.mTasadeTraslado = this.def_decimales(value);
            }
        }
    }
}

