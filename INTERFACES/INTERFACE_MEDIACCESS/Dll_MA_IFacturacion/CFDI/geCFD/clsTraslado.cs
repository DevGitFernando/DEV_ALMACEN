﻿using System;
using System.Xml;

namespace Dll_MA_IFacturacion.CFDI.geCFD
{
    public class clsTraslado
    {
        private double mimporte = 0;
        private string mimpuesto = "";
        private string mImpuestoClave = ""; 
        private string mlastError = "";
        private double mtasa = 0;


        string mImpuesto = "";
        string mTipoFactor = "";
        double mTasaOCuota = 0;
        double mImporteImpuesto = 0;

        private bool containsPipe(string fieldName, string fieldValue)
        {
            if ((fieldValue != null) && fieldValue.Contains("|"))
            {
                this.lastError = "Atributo " + fieldName + "incorrecto. Ning\x00fan elemento puede contener el caracter | (pipe).";
                return true;
            }
            return false;
        }

        public bool isOK()
        {
            if ((this.impuesto == "") || (this.impuesto == null))
            {
                this.lastError = "Falta atributo requerido: impuesto ( IVA o IEPS)";
                return false;
            }

            if (this.tasa < 0.0)
            {
                this.lastError = "Valor incorrecto en atributo: tasa";
                return false;
            }

            if (this.importe < 0.0)
            {
                this.lastError = "Valor incorrecto en atributo: importe";
                return false;
            }

            if (this.containsPipe("impuesto", this.impuesto))
            {
                return false;
            }

            return true;
        }

        public bool loadFromXML(XmlNode mainNode)
        {
            try
            {
                clsFromXML mxml = new clsFromXML();
                this.lastError = "";
                this.impuesto = mxml.stringAttValue(mainNode, "impuesto");
                this.tasa = mxml.doubleAttValue(mainNode, "tasa");
                this.importe = mxml.doubleAttValue(mainNode, "importe");
                mxml = null;
                return true;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        private double t_importe(double v)
        {
            v = Math.Round(v, 6);
            return v;
        }

        public double importe
        {
            get
            {
                return this.mimporte;
            }
            set
            {
                this.mimporte = this.t_importe(value);
            }
        }

        public string impuesto
        {
            get
            {
                return this.mimpuesto;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                value = value.Trim();

                if ((value.ToUpper() != "IVA") && (value.ToUpper() != "IEPS"))
                {
                    value = "";
                }
                this.mimpuesto = value.ToUpper();
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
                this.mlastError = value.Trim();
            }
        }

        public double tasa
        {
            get
            {
                return this.mtasa;
            }
            set
            {
                this.mtasa = this.t_importe(value);
            }
        }

        public string Impuesto_ImpuestoClave
        {
            get { return mImpuestoClave; }
            set { mImpuestoClave = value; }
        }

        public string Impuesto_Impuesto
        {
            get { return mImpuesto; }
            set { mImpuesto = value; }
        }

        public string Impuesto_TipoFactor
        {
            get { return mTipoFactor; }
            set { mTipoFactor = value; }
        }

        public double Impuesto_TasaOCuota
        {
            get { return mTasaOCuota; }
            set { mTasaOCuota = value; }
        }

        public double Impuesto_Importe
        {
            get { return mImporteImpuesto; }
            set { mImporteImpuesto = value; }
        }
    }
}

