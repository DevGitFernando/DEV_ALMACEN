using System;
using System.Collections;
using System.Xml;

namespace Dll_MA_IFacturacion.CFDI.geCFD
{
    public class clsConcepto
    {
        public clsComplementoConcepto ComplementoConcepto = new clsComplementoConcepto();
        public clsCuentaPredial CuentaPredial = new clsCuentaPredial();
        public ArrayList InformacionAduanera = new ArrayList();
        public ArrayList Parte = new ArrayList();

        private double mcantidad;
        private string mdescripcion;
        private double mimporte;
        private string mlastError;
        private string mnoIdentificacion;
        private string munidad;
        private double mvalorUnitario;
        private string mNotas = "";

        private double mtasaiva;
        private double mimporteiva;

        private string mClaveProdServ = "";
        private string mClaveProdServ_Descripcion = "";
        private string mClaveUnidad = "";
        private string mClaveUnidad_Descripcion = "";

        private double mBase = 0;
        private string mImpuesto = "";
        private string mImpuestoClave = ""; 
        private string mTipoFactor = "";
        private double mTasaOCuota = 0;
        private double mImporteImpuesto = 0;
        private double mImporteDescuento = 0;

        private double mImporteImpuesto_Retencion = 0;
        private double mTasaOCuota_Retencion = 0;

        //Base="133331.03" Impuesto="002" TipoFactor="Tasa" TasaOCuota="0.160000" Importe="21332.96" 


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
            if (this.cantidad <= 0.0)
            {
                this.lastError = "Valor incorrecto en atributo requerido: cantidad";
                return false;
            }
            if ((this.unidad == "") || (this.unidad == null))
            {
                this.lastError = "Falta atributo requerido: unidad";
                return false;
            }
            if ((this.descripcion == "") || (this.descripcion == null))
            {
                this.lastError = "Falta atributo requerido: descripcion";
                return false;
            }
            if (this.containsPipe("unidad", this.unidad))
            {
                return false;
            }
            if (this.containsPipe("noIdentificacion", this.noIdentificacion))
            {
                return false;
            }
            if (this.containsPipe("descripcion", this.descripcion))
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
                this.cantidad = mxml.doubleAttValue(mainNode, "cantidad");
                this.unidad = mxml.stringAttValue(mainNode, "unidad");
                this.noIdentificacion = mxml.stringAttValue(mainNode, "noIdentificacion");
                this.descripcion = mxml.stringAttValue(mainNode, "descripcion");
                this.valorUnitario = mxml.doubleAttValue(mainNode, "valorUnitario");
                this.importe = mxml.doubleAttValue(mainNode, "importe");
                foreach (XmlNode node in mainNode.ChildNodes)
                {
                    clsParte parte;
                    string localName = node.LocalName;
                    if (localName != null)
                    {
                        if (!(localName == "InformacionAduanera"))
                        {
                            if (localName == "Parte")
                            {
                                goto Label_00ED;
                            }
                        }
                        else
                        {
                            clsInformacionAduanera aduanera = new clsInformacionAduanera();
                            aduanera.loadFromXML(node);
                            this.InformacionAduanera.Add(aduanera);
                            aduanera = null;
                        }
                    }
                    goto Label_010C;
                Label_00ED:
                    parte = new clsParte();
                    parte.loadFromXML(node);
                    this.Parte.Add(parte);
                    parte = null;
                Label_010C:;
                }
                XmlNode node2 = mxml.getNode("CuentaPredial", mainNode.ChildNodes);
                if (node2 != null)
                {
                    this.CuentaPredial.loadFromXML(node2);
                    node2 = null;
                }
                XmlNode node3 = mxml.getNode("ComplementoConcepto", mainNode.ChildNodes);
                if (node3 != null)
                {
                    this.ComplementoConcepto.loadFromXML(node3);
                    node3 = null;
                }
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

        public double cantidad
        {
            get
            {
                return this.mcantidad;
            }
            set
            {
                if (value < 0.0)
                {
                    value = 0.0;
                }
                this.mcantidad = value;
            }
        }

        public string descripcion
        {
            get
            {
                return this.mdescripcion;
            }
            set
            {
                this.mdescripcion = value.Trim();
            }
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

        public string noIdentificacion
        {
            get
            {
                return this.mnoIdentificacion;
            }
            set
            {
                this.mnoIdentificacion = value.Trim();
            }
        }

        public string unidad
        {
            get
            {
                return this.munidad;
            }
            set
            {
                this.munidad = value.Trim();
            }
        }

        public double valorUnitario
        {
            get
            {
                return this.mvalorUnitario;
            }
            set
            {
                this.mvalorUnitario = this.t_importe(value);
            }
        }

        public double ImporteDescuento
        {
            get { return mImporteDescuento; }
            set { mImporteDescuento = value; }
        }

        public string notas
        {
            get { return mNotas; }
            set { mNotas = value; } 
        }

        public double importeiva
        {
            get { return mimporteiva; }
            set { mimporteiva = value; }
        }

        public double tasaiva
        {
            get { return mtasaiva; }
            set { mtasaiva = value; }
        }

        public string ClaveProdServ
        {
            get { return mClaveProdServ; }
            set { mClaveProdServ = value; }
        }

        public string ClaveProdServ_Descripcion
        {
            get { return mClaveProdServ_Descripcion; }
            set { mClaveProdServ_Descripcion = value; }
        }

        public string ClaveUnidad
        {
            get { return mClaveUnidad; }
            set { mClaveUnidad = value; }
        }

        public string ClaveUnidad_Descripcion
        {
            get { return mClaveUnidad_Descripcion; }
            set { mClaveUnidad_Descripcion = value; }
        }

        public double Impuesto_Base
        {
            get { return mBase; }
            set { mBase = value; }
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

        public double Impuesto_ImporteRetencion
        {
            get { return mImporteImpuesto_Retencion; }
            set { mImporteImpuesto_Retencion = value; }
        }

        public double Impuesto_TasaOCuotaRetencion
        {
            get { return mTasaOCuota_Retencion; }
            set { mTasaOCuota_Retencion = value; }
        }
    }
}

