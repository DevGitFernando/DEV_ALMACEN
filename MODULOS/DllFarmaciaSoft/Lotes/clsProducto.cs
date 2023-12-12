using System;
using System.Collections.Generic;
using System.Text;

namespace DllFarmaciaSoft.Lotes
{
    public class clsProducto
    {
        string sIdProducto = "";
        string sCodigoEAN = "";
        int iUnidad = 0;
        double dTasaIva = 0;
        double dCantidad = 0;
        double dValor = 0;
        double dSubTotal = 0;
        double dImporteIva = 0;
        double dTotal = 0;

        public clsProducto()
        { 
        }

        public clsProducto(string IdProducto, string CodigoEAN, int iUnidad, double TasaIva, double Cantidad, double Valor)
        {
            this.sIdProducto = IdProducto;
            this.sCodigoEAN = CodigoEAN;
            this.dTasaIva = TasaIva;
            this.dCantidad = Cantidad;
            this.dValor = Valor;
        }

        public string IdProducto
        {
            get { return sIdProducto; }
            set { sIdProducto = value; }
        }

        public string CodigoEAN
        {
            get { return sCodigoEAN; }
            set { sCodigoEAN = value; }
        }

        public int Unidad
        {
            get{ return iUnidad; }
            set{ iUnidad = value;}
        }

        public double TasaIva
        {
            get { return dTasaIva; }
            set { dTasaIva = value; }
        }

        public double Cantidad
        {
            get { return dCantidad; }
            set { dCantidad= value; }
        }

        public double Valor
        {
            get { return dValor; }
            set { dValor  = value; }
        }

        public double SubTotal
        {
            get 
            {
                dSubTotal = dCantidad * dValor; 
                return dSubTotal; 
            }
        }

        public double ImporteIva
        {
            get 
            {
                double dImporte = 0;

                if (dTasaIva > 0)
                {
                    dImporte = ((1 + (dTasaIva / 100)) * SubTotal) - SubTotal;
                }
                dImporteIva = dImporte; 

                return dImporteIva; 
            }
        }

        public double Total
        {
            get 
            {
                dTotal = this.SubTotal + this.ImporteIva; 
                return dTotal; 
            }
        } 
    }
}
