using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.Windows.Forms; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales; 

namespace Dll_IFacturacion.CFDI.CFDFunctionsEx
{
    public class clsCFDI
    {
        // public string FormatoDecimal = "########0.#0"; 
        // https://www.consulta.sat.gob.mx/SICOFI_WEB/ModuloECFD_Plus/ValidadorComprobantes/Validador.html 

        clsComprobante cComprobante = new clsComprobante();
        clsSelloDigital selloDigital;
        DataSet dtsConfiguracion; 
        //clsEmisor cEmisor = new clsEmisor();
        //clsReceptor cReceptor = new clsReceptor();
        //clsConceptos cConceptos = new clsConceptos();
        //clsTraslados cTraslados = new clsTraslados();
        //clsRetenciones cRetenciones = new clsRetenciones(); 

        string sCadenaOriginal = "";
        string sSello = "";
        string sCertificado = "";
        string sLlavePrivada = "";
        string sPassword = ""; 

        SC_DllImports dll_Imports; 

        #region Constructores y Destructor de Clase 
        public clsCFDI()
        {
        }

        public clsCFDI(DataSet Configuracion)
        {
            dll_Imports = new SC_DllImports();
            dll_Imports.VerificaLibrerias("");
            dll_Imports.CargarDll(); 

            //dtsConfiguracion = Configuracion;
            RenombrarTablas(Configuracion); 
            PrepararArchivos(); 

            selloDigital = new clsSelloDigital(sCertificado, sLlavePrivada, sPassword);
            selloDigital.ObtenerDatos();

            cComprobante.NoCertificado = selloDigital.NumeroDeCertificado;
            cComprobante.Certificado = selloDigital.Certificado; 
        }

        public clsCFDI(string Certificado, string LlavePrivada, string Password)
        {
            dll_Imports = new SC_DllImports();
            dll_Imports.VerificaLibrerias("");
            dll_Imports.CargarDll(); 

            selloDigital = new clsSelloDigital(Certificado, LlavePrivada, Password);
            selloDigital.ObtenerDatos();

            cComprobante.NoCertificado = selloDigital.NumeroDeCertificado;
            cComprobante.Certificado = selloDigital.Certificado; 
        }
        #endregion Constructores y Destructor de Clase

        ////#region Propiedades 
        public clsComprobante Comprobante
        {
            get { return cComprobante; }
            set { cComprobante = value; }
        }

        ////public clsEmisor Emisor
        ////{
        ////    get { return cEmisor; }
        ////    set { cEmisor = value; }
        ////}

        ////public clsReceptor Receptor
        ////{
        ////    get { return cReceptor; }
        ////    set { cReceptor = value; }
        ////}

        ////public clsConceptos Conceptos
        ////{
        ////    get { return cConceptos; }
        ////    set { cConceptos = value; }
        ////}

        ////public clsTraslados Traslados
        ////{
        ////    get { return cTraslados; }
        ////    set { cTraslados = value; }
        ////}

        ////public clsRetenciones Retenciones
        ////{
        ////    get { return cRetenciones; }
        ////    set { cRetenciones = value; }
        ////}
        ////#endregion Propiedades

        #region Funciones y Procedimientos Publicos   
        public string Sellar()
        {
            string sRegresa = "";

            sRegresa = selloDigital.GenerarSelloDigital(sCadenaOriginal);

            // cComprobante.NoCertificado = selloDigital.NumeroDeCertificado;
            cComprobante.Certificado = selloDigital.Certificado; 
            cComprobante.Sello = sRegresa; 

            return sRegresa; 
        }

        public string ObtenerCadenaOriginal()
        {
            string sRegresa = "";

            sRegresa = "|"; 
            sRegresa += cComprobante.Cadena;
            sRegresa += "||";

            sCadenaOriginal = sRegresa; 
            return sRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private void PrepararArchivos()
        {
            clsLeer leer = new clsLeer();
            clsLeer leerCfg = new clsLeer(); 
            string sRuta = Application.StartupPath;
            string sFile = "";
            string sNombre = ""; 
            clsArchivo file = new clsArchivo();

            leerCfg.DataSetClase = dtsConfiguracion;
            leer.DataTableClase = leerCfg.Tabla("Comprobante");
            if (leer.Leer())
            {
                sPassword = leer.Campo("PasswordPublico");

                sNombre = leer.Campo("NombreCertificado"); 
                file.Decodificar(sNombre, sRuta, leer.Campo("Certificado"), true);
                sCertificado = sRuta + "\\" + sNombre; 

                sNombre = leer.Campo("NombreLlavePrivada");
                file.Decodificar(sNombre, sRuta, leer.Campo("LlavePrivada"), true);
                sLlavePrivada = sRuta + "\\" + sNombre; 
            }
        }

        private void RenombrarTablas(DataSet Configuracion)
        {
            clsLeer leer = new clsLeer();
            leer.DataSetClase = Configuracion;

            leer.RenombrarTabla(1, "Comprobante");
            leer.RenombrarTabla(2, "Emisor");
            leer.RenombrarTabla(3, "EmisorDomicilioFiscal");
            leer.RenombrarTabla(4, "EmisorExpedidoEn");

            leer.RenombrarTabla(5, "Receptor");

            leer.RenombrarTabla(6, "Conceptos");
            leer.RenombrarTabla(7, "Traslados");

            // Cargar los datos Renombrados 
            dtsConfiguracion = leer.DataSetClase;
        }
        #endregion Funciones y Procedimientos Privados

    }
}
