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

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
#endregion USING

namespace Dll_MA_IFacturacion.XSA
{
    public class xsaCfdiEncabezado
    {
        #region Declaración de Variables 
        basGenerales Fg = new basGenerales(); 
        xsaTiposDeRegistro tipoSeccion = xsaTiposDeRegistro.Encabezado;
        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = ""; 

        string sIdCFDI = "";
        string sSerie = "";
        string sFolio = "";
        string sFechaHora = "";
        DateTime dtFechaHora = DateTime.Now;

        double dSubTotal = 0.00;
        double dTotal = 0.00;
        double dTotalImpuestosTrasladados = 0.00;
        double dTotalImpuestosRetenidos = 0.00;
        double dDescuento = 0.00;
        string sMotivoDescuento = ""; 
        string sCantidadLetra = "";
        cfdMoneda tpMoneda = cfdMoneda.Ninguno;
        double dTipoDeCambio = 0.0000;
        string sReferencia = "";
        string sObservacion_01 = "";
        string sObservacion_02 = "";
        string sObservacion_03 = "";
        int iRenglones = 0; 
        ////string sFormato_02 = "###,###,###,###,##0.#0";
        ////string sFormato_04 = "###,###,###,###,##0.####0"; 

        #endregion Declaración de Variables 

        #region Constructores y Destructor de Clase 
        public xsaCfdiEncabezado()
        { 
        }

        ~xsaCfdiEncabezado()
        { 
        }
        #endregion Constructores y Destructor de Clase 

        #region Propiedades 
        public int Renglones
        {
            get { return iRenglones; }
        }

        public string Cadena
        {
            get { return GenerarCadena(); }
        }

        public string IdEmpresa 
        {
            get { return sIdEmpresa; }
            set { sIdEmpresa = value; }
        }

        public string IdEstado
        {
            get { return sIdEstado; }
            set { sIdEstado = value; }
        }

        public string IdFarmacia
        {
            get { return sIdFarmacia; }
            set { sIdFarmacia = value; }
        }

        public string Identificador_CFDI
        {
            get 
            {
                sIdCFDI = Genera_Idenfiticador();
                return sIdCFDI; 
            }
        }

        public string Serie
        {
            get { return sSerie; }
            set { sSerie = value; }
        }

        public string Folio 
        {
            get { return sFolio; }
            set { sFolio = value; }
        }

        public DateTime FechaHora
        {
            get { return dtFechaHora; }
            set 
            { 
                dtFechaHora = value;
                sFechaHora = General.FechaYMD(dtFechaHora) + "T" + General.Hora(dtFechaHora);
            }
        }

        public double SubTotal
        {
            get { return dSubTotal; }
            set { dSubTotal = value; }
        }

        public double Total
        {
            get { return dTotal; }
            set { dTotal = value; }
        }

        public double ImpuestosTrasladados
        {
            get { return dTotalImpuestosTrasladados; }
            set { dTotalImpuestosTrasladados = value; }
        }

        public double ImpuestosRetenidos 
        {
            get { return dTotalImpuestosRetenidos; }
            set { dTotalImpuestosRetenidos = value; }
        }

        public double Descuento
        {
            get { return dDescuento; }
            set { dDescuento = value; }
        }

        public string MotivoDescuento
        {
            get { return sMotivoDescuento; }
            set { sMotivoDescuento = value; }
        }

        public cfdMoneda Moneda
        {
            get { return tpMoneda; }
            set { tpMoneda = value; }
        }

        public double TipoDeCambio
        {
            get { return dTipoDeCambio; }
            set { dTipoDeCambio = value; }
        }

        public string Observaciones_01
        {
            get { return sObservacion_01; }
            set { sObservacion_01 = value; }
        }

        public string Observaciones_02
        {
            get { return sObservacion_02; }
            set { sObservacion_02 = value; }
        }

        public string Observaciones_03
        {
            get { return sObservacion_03; }
            set { sObservacion_03 = value; }
        }

        public string Referencia
        {
            get { return sReferencia; }
            set { sReferencia = value;  }
        }
        #endregion Propiedades 

        #region Funciones y Procedimientos Publicos 
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private string Genera_Idenfiticador()
        {
            string sRegresa = ""; 
            string sTmpSerie = Fg.Right(("0000000000_" + sSerie), 10);

            sRegresa = string.Format("{0}{1}{2}__{3}_{4}", sIdEmpresa, sIdEstado, sIdFarmacia, sTmpSerie, Fg.PonCeros(sFolio, 10));
            
            return sRegresa; 
        }

        private string GenerarCadena()
        {
            string sRegresa = "";
            clsLetraMoneda letraMoneda = new clsLetraMoneda();
            sCantidadLetra = letraMoneda.Letra(Math.Round(dTotal, 2)); 

            iRenglones = 1; 

            // Iniciar Seccion 
            sRegresa = xsaTipoDeRegistro.TipoDeRegistro(tipoSeccion);
            sRegresa += "|"; 

            //sIdCFDI = string.Format("{0}{1}{2}__{3}_{4}", sIdEmpresa, sIdEstado, sIdFarmacia, sTmpSerie, Fg.PonCeros(sFolio, 10));
            sIdCFDI = Genera_Idenfiticador(); 

            sRegresa += sIdCFDI + "|";
            sRegresa += sSerie + "|";
            sRegresa += sFolio + "|";
            sRegresa += sFechaHora + "|"; 
            
            sRegresa += dSubTotal.ToString(DtIFacturacion.sFormato_02) + "|";
            sRegresa += dTotal.ToString(DtIFacturacion.sFormato_02) + "|";
            
            sRegresa += dTotalImpuestosTrasladados.ToString(DtIFacturacion.sFormato_02) + "|";
            sRegresa += dTotalImpuestosRetenidos.ToString(DtIFacturacion.sFormato_02) + "|";


            sRegresa += dDescuento.ToString(DtIFacturacion.sFormato_02) + "|";
            sRegresa += sMotivoDescuento + "|";
            sRegresa += sCantidadLetra + "|";

            if ((tpMoneda == cfdMoneda.Ninguno) || (tpMoneda == cfdMoneda.MXN))
            {
                sRegresa += "|";
                sRegresa += "|";
            }
            else
            {
                sRegresa += tpMoneda.ToString() + "|";
                sRegresa += dTipoDeCambio.ToString(DtIFacturacion.sFormato_04) + "|";
            }

            sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sReferencia) + "|";
            sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sObservacion_01) + "|";
            sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sObservacion_02) + "|";
            sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sObservacion_03) + "|"; 

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }
}
