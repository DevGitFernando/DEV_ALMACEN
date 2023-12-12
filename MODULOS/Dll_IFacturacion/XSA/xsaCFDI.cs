#region USING
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;
using System.Windows.Forms; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
#endregion USING

namespace Dll_IFacturacion.XSA
{
    public enum xsaTiposDeRegistro
    {
        CFDI_Inicio = 0, 
        Encabezado = 1, EncabezadoExt = -1, 
        Pago = 2, Receptor = 3, DireccionEmbarque = 4, 
        Conceptos = 5, ImpuestosTrasladados = 6, ImpuestossRetenidos = 7, 
        InformacionAduana = 8, EnvioAutomatico = 9, 
        CFDI_Fin = 99 
    }

    internal static class xsaTipoDocumento
    {
        ////public enum cfdTipoDePlantilla
        ////{
        ////    Ninguno = 0,
        ////    FAC = 1, NCR = 2, NDD = 3, CDI = 4, CPO = 5, NCC = 6
        ////}

        public static cfdTipoDePlantilla TipoDePlantilla (string TipoDocto)
        {
            cfdTipoDePlantilla cfdPlantilla = cfdTipoDePlantilla.Ninguno;

            switch (TipoDocto)
            {
                case "FAC":
                    cfdPlantilla = cfdTipoDePlantilla.FAC;
                    break;

                case "NCR":
                    cfdPlantilla = cfdTipoDePlantilla.NCR;
                    break;

                case "NDD":
                    cfdPlantilla = cfdTipoDePlantilla.NDD;
                    break;

                case "CDI":
                    cfdPlantilla = cfdTipoDePlantilla.CDI;
                    break;

                case "CPO":
                    cfdPlantilla = cfdTipoDePlantilla.CPO;
                    break;

                case "NCC":
                    cfdPlantilla = cfdTipoDePlantilla.NCC;
                    break; 
            }

            return cfdPlantilla; 
        }

        public static string TipoDeDocumento(cfdTipoDePlantilla Tipo)
        {
            string sRegresa = "";

            switch (Tipo)
            {
                case cfdTipoDePlantilla.FAC:
                    sRegresa = "Factura"; 
                    break;

                case cfdTipoDePlantilla.NCR:
                    sRegresa = "Nota de crédito";
                    break;

                case cfdTipoDePlantilla.NDD:
                    sRegresa = "Nota de Débito";
                    break;

                case cfdTipoDePlantilla.CDI:
                    sRegresa = "Comprobante de Ingresos";
                    break;

                case cfdTipoDePlantilla.CPO:
                    sRegresa = "Carta Porte";
                    break;

                case cfdTipoDePlantilla.NCC:
                    sRegresa = "Nota de Cargo";
                    break; 
            }

            return sRegresa;
        }
    }

    internal static class xsaTipoDeRegistro 
    {
        private static basGenerales Fg = new basGenerales(); 
        public static string TipoDeRegistro(xsaTiposDeRegistro Tipo)
        {
            string sRegresa = Fg.PonCeros((int)Tipo, 2);

            if (Tipo == xsaTiposDeRegistro.EncabezadoExt)
            {
                sRegresa = Fg.PonCeros((int)xsaTiposDeRegistro.Encabezado, 2) + "A"; 
            }

            return sRegresa; 
        }
    } 

    public sealed class xsaCFDI
    {
        #region Declaración de Variables 
        //xsaTiposDeRegistro tipoSeccion = xsaTiposDeRegistro.CFDI_Inicio;
        string sIdentificadorDocumento = "";    // Identificador del Documento generado 
        string sDocumento_XML = "";
        string sDocumento_PDF = "";    

        cfdTipoDePlantilla etiquetaPlantilla = cfdTipoDePlantilla.Ninguno;
        //string sNumLineas = ""; 

        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        string sRutaCFDI = DtIFacturacion.RutaCFDI;
        string sRutaCFDI_Documentos = DtIFacturacion.RutaCFDI_Documentos;
        string sPlantillaB64 = ""; 

        xsaCfdiEncabezado xsaEncabezado = new xsaCfdiEncabezado();
        xsaCfdiEncabezadoExt xsaEncabezadoExt = new xsaCfdiEncabezadoExt();
        xsaCfdiPago xsaPago = new xsaCfdiPago();
        xsaCfdiReceptor xsaReceptor = new xsaCfdiReceptor();
        xsaCfdiEmbarque xsaEmbarque = new xsaCfdiEmbarque();
        xsaCfdiConceptos xsaConceptos = new xsaCfdiConceptos();
        xsaCfdiImpuestosTrasladados xsaImpuestosTrasladados = new xsaCfdiImpuestosTrasladados();
        xsaCfdiImpuestosRetenidos xsaImpuestosRetenidos = new xsaCfdiImpuestosRetenidos();
        xsaCfdiAduana xsaAduana = new xsaCfdiAduana();
        xsaCfdiEnvioDocumentos xsaEnvio = new xsaCfdiEnvioDocumentos();

        string sSeccion_00 = "";
        string sSeccion_01 = "";
        string sSeccion_01A = "";
        string sSeccion_02 = "";
        string sSeccion_03 = "";
        string sSeccion_04 = "";
        string sSeccion_05 = "";
        string sSeccion_06 = "";
        string sSeccion_07 = "";
        string sSeccion_08 = "";
        string sSeccion_09 = "";
        string sSeccion_99 = "";
        int iRenglones = 0; 

        #endregion Declaración de Variables 

        #region Constructores y Destructor de Clase 
        public xsaCFDI(string IdEmpresa, string IdEstado, string IdFarmacia)
        {
            this.sIdEmpresa = IdEmpresa;
            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;

            xsaEncabezado.IdEmpresa = IdEmpresa;
            xsaEncabezado.IdEstado = IdEstado;
            xsaEncabezado.IdFarmacia = IdFarmacia;

            DirectoriosDeTrabajo(); 

        }
        #endregion Constructores y Destructor de Clase 

        #region Propiedades 
        public string IdentificadorDocumento 
        {
            get { return sIdentificadorDocumento; }
            set { sIdentificadorDocumento = value; }
        }

        public string Documento_XML 
        {
            get { return sDocumento_XML; }
            set { sDocumento_XML = value; }
        }

        public string Documento_PDF
        {
            get { return sDocumento_PDF; }
            set { sDocumento_PDF = value; }
        } 

        public cfdTipoDePlantilla TipoDocumento
        {
            get { return etiquetaPlantilla; }
            set { etiquetaPlantilla = value; }
        }

        public int Renglones
        {
            get { return iRenglones; }
        }
        #endregion Propiedades 

        #region Secciones de Documento 
        public xsaCfdiEncabezado Encabezado
        {
            get { return xsaEncabezado; }
            set { xsaEncabezado = value; }
        }

        public xsaCfdiEncabezadoExt EncabezadoAdicional
        {
            get { return xsaEncabezadoExt; }
            set { xsaEncabezadoExt = value; }
        }

        public xsaCfdiPago Pago
        {
            get { return xsaPago; }
            set { xsaPago = value; }
        }

        public xsaCfdiReceptor Receptor
        {
            get { return xsaReceptor; }
            set { xsaReceptor = value; }
        }

        public xsaCfdiEmbarque Embarque
        {
            get { return xsaEmbarque; }
            set { xsaEmbarque = value; }
        }

        public xsaCfdiConceptos Conceptos
        {
            get { return xsaConceptos; }
            set { xsaConceptos = value; }
        }

        public xsaCfdiImpuestosTrasladados ImpuestosTrasladados
        {
            get { return xsaImpuestosTrasladados; }
            set { xsaImpuestosTrasladados = value; }
        }

        public xsaCfdiImpuestosRetenidos ImpuestosRetenidos
        {
            get { return xsaImpuestosRetenidos; }
            set { xsaImpuestosRetenidos = value; }
        }

        public xsaCfdiAduana Aduana
        {
            get { return xsaAduana; }
            set { xsaAduana = value; }
        }

        public xsaCfdiEnvioDocumentos EnvioDocumentos
        {
            get { return xsaEnvio; }
            set { xsaEnvio = value; }
        } 
        #endregion Secciones de Documento

        #region Funciones y Procedimientos Publicos
        public string GenerarPlantilla()
        {
            string sRegresa = GenerarCadena(); 
            return sRegresa; 
        }

        public string PlantillaB64 
        {
            get { return sPlantillaB64; }
        }

        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados 
        private void DirectoriosDeTrabajo()
        {
            //CrearDirectorio(sRutaCFDI);
            //CrearDirectorio(sRutaCFDI_Documentos); 
        }

        private void CrearDirectorio(string Directorio)
        {
            ////if (!Directory.Exists(Directorio))
            ////{
            ////    Directory.CreateDirectory(Directorio); 
            ////}
        }

        private string GenerarCadena()
        {
            string sRegresa = "";
            string sSalto = "\n"; 

            // Iniciar el Documento 
            sIdentificadorDocumento = xsaEncabezado.Identificador_CFDI;
            sDocumento_PDF = string.Format("{0}.{1}", sIdentificadorDocumento, cfdFormatoDocumento.PDF);
            sDocumento_XML = string.Format("{0}.{1}", sIdentificadorDocumento, cfdFormatoDocumento.XML);
            sDocumento_PDF = "";
            sDocumento_XML = ""; 


            sSeccion_00 = xsaTipoDeRegistro.TipoDeRegistro(xsaTiposDeRegistro.CFDI_Inicio) + "|";
            sSeccion_00 += sIdentificadorDocumento + "|";
            sSeccion_00 += etiquetaPlantilla.ToString() + "|"; 

            //// Generar la cadena completa 
            sSeccion_01 = xsaEncabezado.Cadena;
            sSeccion_01A = xsaEncabezadoExt.Cadena;
            sSeccion_02 = xsaPago.Cadena;
            sSeccion_03 = xsaReceptor.Cadena;
            sSeccion_04 = xsaEmbarque.Cadena;
            sSeccion_05 = xsaConceptos.Cadena;
            sSeccion_06 = xsaImpuestosTrasladados.Cadena;
            sSeccion_07 = xsaImpuestosRetenidos.Cadena;
            sSeccion_08 = xsaAduana.Cadena;
            sSeccion_09 = xsaEnvio.Cadena;

            iRenglones = 2;
            iRenglones += xsaEncabezado.Renglones;
            iRenglones += xsaEncabezadoExt.Renglones;
            iRenglones += xsaPago.Renglones;
            iRenglones += xsaReceptor.Renglones;
            iRenglones += xsaEmbarque.Renglones;
            iRenglones += xsaConceptos.Renglones;
            iRenglones += xsaImpuestosTrasladados.Renglones;
            iRenglones += xsaImpuestosRetenidos.Renglones;
            iRenglones += xsaAduana.Renglones;
            iRenglones += xsaEnvio.Renglones; 


            // Cerrar el Documento 
            sSeccion_99 = string.Format("{0}|{1}|", xsaTipoDeRegistro.TipoDeRegistro(xsaTiposDeRegistro.CFDI_Fin), iRenglones.ToString()); 

            sRegresa += sSeccion_00 + "\n";
            sRegresa += sSeccion_01 + "\n";
            sRegresa += sSeccion_01A + "\n";            
            sRegresa += sSeccion_02 + "\n";
            sRegresa += sSeccion_03 + "\n";
            sRegresa += sSeccion_04 + "\n";
            sRegresa += sSeccion_05 + "\n";
            sRegresa += sSeccion_06 + "\n";
            sRegresa += sSeccion_07 + "\n";
            sRegresa += sSeccion_08 + "\n";
            sRegresa += sSeccion_09 + "\n";
            sRegresa += sSeccion_99 + sSalto;

            ///////////////////// Cambio 
            sRegresa = ConcatenarSeccion(sSeccion_00);
            sRegresa += ConcatenarSeccion(sSeccion_01);
            sRegresa += ConcatenarSeccion(sSeccion_01A);
            sRegresa += ConcatenarSeccion(sSeccion_02);
            sRegresa += ConcatenarSeccion(sSeccion_03);
            sRegresa += ConcatenarSeccion(sSeccion_04);
            sRegresa += ConcatenarSeccion(sSeccion_05);
            sRegresa += ConcatenarSeccion(sSeccion_06);
            sRegresa += ConcatenarSeccion(sSeccion_07);
            sRegresa += ConcatenarSeccion(sSeccion_08);
            sRegresa += ConcatenarSeccion(sSeccion_09);
            sRegresa += ConcatenarSeccion(sSeccion_99) + sSalto; 



            CrearDocumento(); 

            return sRegresa;
        }

        private string ConcatenarSeccion(string Seccion) 
        {
            string sRegresa = "";

            if (Seccion != "")
            {
                sRegresa += Seccion + "\n"; 
            }

            return sRegresa;

        }

        /// <summary>
        /// Crear el Documento-Plantilla
        /// </summary>
        /// <param name="Cadena"></param>
        private void CrearDocumento()
        {
            try
            {
                string sFile = Path.Combine(sRutaCFDI_Documentos, sIdentificadorDocumento + ".txt");
                StreamWriter sw = new StreamWriter(sFile);

                if (sSeccion_00 != "")
                {
                    sw.WriteLine(sSeccion_00);
                }

                if (sSeccion_01 != "")
                {
                    sw.WriteLine(sSeccion_01);
                }

                if (sSeccion_01A != "")
                {
                    sw.WriteLine(sSeccion_01A);
                }

                if (sSeccion_02 != "")
                {
                    sw.WriteLine(sSeccion_02);
                }

                if (sSeccion_03 != "")
                {
                    sw.WriteLine(sSeccion_03);
                }

                if (sSeccion_04 != "")
                {
                    sw.WriteLine(sSeccion_04);
                }

                if (sSeccion_05 != "")
                {
                    sw.WriteLine(sSeccion_05);
                }

                if (sSeccion_06 != "")
                {
                    sw.WriteLine(sSeccion_06);
                }

                if (sSeccion_07 != "")
                {
                    sw.WriteLine(sSeccion_07);
                }

                if (sSeccion_08 != "")
                {
                    sw.WriteLine(sSeccion_08);
                }

                if (sSeccion_09 != "")
                {
                    sw.WriteLine(sSeccion_09);
                }

                if (sSeccion_99 != "")
                {
                    sw.WriteLine(sSeccion_99);
                }


                sw.Close();

                basGenerales Fg = new basGenerales();
                sPlantillaB64 = Fg.ConvertirArchivoEnStringB64(sFile); 
            }
            catch ( Exception ex ) 
            {
                ex.Source = ex.Source; 
            }  
        }
        #endregion Funciones y Procedimientos Privados 
    }
}
