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
    public class clsDomicilio
    {
        #region Declaracion de Variables
        string sCalle = "";
        string sCodigoPostal = "";
        string sColonia = "";
        string sEstado = "";
        string sLocalidad = "";
        string sMunicipio = "";
        string sNoInterior = "";
        string sNoExterior = "";
        string sPais = "";
        string sReferencia = "";
        DataSet dtsDomicilio;
        TipoDeDomicilio tpDomicilio = TipoDeDomicilio.Ninguno;
        bool bExistenDatos = false;


        #endregion Declaracion de Variables

        #region Constructores y Destructor de Clase
        public clsDomicilio(TipoDeDomicilio Tipo)
        {
            dtsDomicilio = new DataSet();
            this.tpDomicilio = Tipo;
        }

        public clsDomicilio(DataSet DatosDomicilio, TipoDeDomicilio Tipo)
        {
            dtsDomicilio = DatosDomicilio;
            this.tpDomicilio = Tipo;
            CargarDatos();
        }
        #endregion Constructores y Destructor de Clase

        #region Propiedades
        public string Calle
        {
            get { return sCalle; }
            set { sCalle = value; }
        }

        public string CodigoPostal
        {
            get { return sCodigoPostal; }
            set { sCodigoPostal = value; }
        }

        public string Colonia
        {
            get { return sColonia; }
            set { sColonia = value; }
        }

        public string Estado
        {
            get { return sEstado; }
            set { sEstado = value; }
        }

        public string Localidad
        {
            get { return sLocalidad; }
            set { sLocalidad = value; }
        }

        public string Municipio
        {
            get { return sMunicipio; }
            set { sMunicipio = value; }
        }

        public string NoInterior
        {
            get { return sNoInterior; }
            set { sNoInterior = value; }
        }

        public string NoExterior
        {
            get { return sNoExterior; }
            set { sNoExterior = value; }
        }

        public string Pais
        {
            get { return sPais; }
            set { sPais = value; }
        }

        public string Referencia
        {
            get { return sReferencia; }
            set { sReferencia = value; }
        }

        public TipoDeDomicilio TipoDomicilio
        {
            get { return tpDomicilio; }
            // set { tpDomicilio = value; }
        }

        public string Cadena
        {
            get { return CadenaParcial(); }
        }

        public bool ExistenDatos
        {
            get { return bExistenDatos; }
        }

        public XmlElement Nodo(XmlDocument Documento, XmlElement NodoElemento)
        {
            return ElementoNodo(Documento, NodoElemento);
        }

        #endregion Propiedades

        #region Funciones y Procedimientos Privados
        private void CargarDatos()
        {
            clsLeer leer = new clsLeer();
            clsLeer leerDomicilio = new clsLeer();
            string sTabla = "";

            switch (tpDomicilio)
            {
                case TipoDeDomicilio.Domicilio:
                    sTabla = "Receptor";
                    break;

                case TipoDeDomicilio.DomicilioFiscal:
                    sTabla = "EmisorDomicilioFiscal";
                    break;

                case TipoDeDomicilio.ExpedidoEn:
                    sTabla = "EmisorExpedidoEn";
                    break;

                default:
                    break;
            }

            leer.DataSetClase = dtsDomicilio;
            leerDomicilio.DataTableClase = leer.Tabla(sTabla);
            if (leerDomicilio.Leer())
            {
                bExistenDatos = true;
                sCalle = leerDomicilio.Campo("Calle");
                sCodigoPostal = leerDomicilio.Campo("CodigoPostal");
                sColonia = leerDomicilio.Campo("Colonia");
                sEstado = leerDomicilio.Campo("Estado");
                sLocalidad = leerDomicilio.Campo("Localidad");
                sMunicipio = leerDomicilio.Campo("Municipio");
                sNoInterior = leerDomicilio.Campo("NoInterior");
                sNoExterior = leerDomicilio.Campo("NoExterior");
                sPais = leerDomicilio.Campo("Pais");
                sReferencia = leerDomicilio.Campo("Referencia");
            }

        }

        private XmlElement ElementoNodo(XmlDocument Documento, XmlElement NodoElemento)
        {
            // XmlDocument doc = Documento;
            XmlElement Elemento = Documento.CreateElement(tpDomicilio.ToString()); //"DomicilioFiscal");
            Elemento.SetAttribute("calle", sCalle);
            Elemento.SetAttribute("codigoPostal", sCodigoPostal);
            Elemento.SetAttribute("colonia", sColonia);
            Elemento.SetAttribute("estado", sEstado);

            if (sLocalidad != "")
            {
                Elemento.SetAttribute("localidad", sLocalidad);
            }

            Elemento.SetAttribute("municipio", sMunicipio);
            Elemento.SetAttribute("noExterior", sNoExterior);

            if (sNoInterior != "")
            {
                Elemento.SetAttribute("noInterior", sNoInterior);
            }

            Elemento.SetAttribute("pais", sPais);
            //////if (sReferencia!= "")
            //////{
            //////    Elemento.SetAttribute("Referencia", sReferencia);
            //////}

            NodoElemento.AppendChild(Elemento);
            return Elemento;
        }

        private string CadenaParcial()
        {
            string sRegresa = "";

            sRegresa = "|" + sCalle;
            sRegresa += "|" + sNoExterior;

            if (sNoInterior != "")
            {
                sRegresa += "|" + sNoInterior;
            }

            sRegresa += "|" + sColonia;

            if (sLocalidad != "")
            {
                sRegresa += "|" + sLocalidad;
            }

            sRegresa += "|" + sMunicipio;
            sRegresa += "|" + sEstado;
            sRegresa += "|" + sPais;

            sRegresa += "|" + sCodigoPostal;
            ////if (sReferencia != "")
            ////{
            ////    sRegresa += "|" + sReferencia;
            ////}

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }
}
