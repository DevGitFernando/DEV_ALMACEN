using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales; 

namespace Dll_MA_IFacturacion.CFDI.CFDFunctionsEx
{
    #region Retencion 
    public class clsRetencion
    {
        #region Declaracion de Variables 
        double dImporte = 0;
        RetencionImpuestos tipoRetencion = RetencionImpuestos.Ninguna; 
        #endregion Declaracion de Variables 

        #region Constructores y Destructor de Clase 
        public clsRetencion()
        {
        }
        #endregion Constructores y Destructor de Clase 

        #region Propiedades
        public double Importe
        {
            get { return dImporte; }
            set { dImporte = value; }
        }

        public RetencionImpuestos Impuesto
        {
            get { return tipoRetencion; }
            set { tipoRetencion = value; }
        }
        #endregion Propiedades
    }
    #endregion Retencion

    #region Retenciones
    public class clsRetenciones
    {
        List<Dll_MA_IFacturacion.CFDI.geCFD.clsRetencion> pListaRetenciones; //  = new List<clsConcepto>();
        DataSet dtsRetenciones;
        double dImporteRetenciones = 0;

        #region Constructores y Destructor de Clase 
        public clsRetenciones()
        {
            pListaRetenciones = new List<Dll_MA_IFacturacion.CFDI.geCFD.clsRetencion>();
        }

        public clsRetenciones(DataSet Retenciones)
        {
            pListaRetenciones = new List<Dll_MA_IFacturacion.CFDI.geCFD.clsRetencion>();
            dtsRetenciones = Retenciones; 
        }
        #endregion Constructores y Destructor de Clase 

        #region Funciones y Procedimientos Privados 
        private void CargarDatos()
        {
            clsLeer leer = new clsLeer();
            clsLeer leerRetenciones = new clsLeer();

            leer.DataSetClase = dtsRetenciones;
            leerRetenciones.DataTableClase = leer.Tabla("Retenciones");
            while (leerRetenciones.Leer())
            {
                Dll_MA_IFacturacion.CFDI.geCFD.clsRetencion retencion = new Dll_MA_IFacturacion.CFDI.geCFD.clsRetencion();

                retencion.importe = leerRetenciones.CampoDouble("Importe");
                retencion.tasa = leerRetenciones.CampoDouble("TasaIva");
                retencion.impuesto = TrasladoImpuestos.IVA.ToString().ToUpper();

                retencion.Impuesto_Importe = leerRetenciones.CampoDouble("Importe");
                retencion.Impuesto_ImpuestoClave = leerRetenciones.Campo("Impuesto_ImpuestoClave");
                retencion.Impuesto_Impuesto = leerRetenciones.Campo("Impuesto_Impuesto");
                retencion.Impuesto_TasaOCuota = leerRetenciones.CampoDouble("Impuestos_TasaOCuota");
                retencion.Impuesto_TipoFactor = leerRetenciones.Campo("Impuestos_TipoFactor");

                pListaRetenciones.Add(retencion);
            }
        } 
        #endregion Funciones y Procedimientos Privados

        #region Funciones y Procedimientos Publicos
        public bool Add(Dll_MA_IFacturacion.CFDI.geCFD.clsRetencion Retencion)
        {
            bool bRegresa = true;

            try
            {
                pListaRetenciones.Add(Retencion); 
            }
            catch 
            {
                bRegresa = false; 
            }

            return bRegresa;
        }

        public bool Add(double Importe, RetencionImpuestos Impuesto)
        {
            bool bRegresa = true;

            ////try
            ////{
            ////    Dll_MA_IFacturacion.CFDI.geCFD.clsRetencion Retencion = new Dll_MA_IFacturacion.CFDI.geCFD.clsRetencion();
            ////    Retencion.importe = Importe;
            ////    Retencion.impuesto = Impuesto;

            ////    pListaRetenciones.Add(Retencion);
            ////}
            ////catch
            ////{
            ////    bRegresa = false;
            ////}

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Propiedades 
        public List<Dll_MA_IFacturacion.CFDI.geCFD.clsRetencion> ListaRetenciones
        {
            get { return pListaRetenciones; }
        }

        public double ImporteRetenciones
        {
            get { return dImporteRetenciones; }
        }
        #endregion Propiedades 
    }
    #endregion Retenciones
}
