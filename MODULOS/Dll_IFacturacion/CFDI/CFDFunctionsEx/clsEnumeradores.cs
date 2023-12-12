using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales; 

namespace Dll_IFacturacion.CFDI
{
    internal static class CfdGeneral 
    {
        public static string FormatoDecimal = "########0.#0"; 
    }

    public enum FormatoFecha
    {
        YMD = 1, DMY = 2
    }

    public enum Esquema
    {
        Ninguno = 0, 
        ComprobantesFiscalesDigitales = 1,
        ComprobantesSolicitados = 2 
    }

    public enum EstadoComprobante
    {
        Ninguno = 0, 
        Vigente = 1,
        Cancelado = 2
    }

    public enum TipoDeComprobanteSAT
    {
        Ninguno = 0, Ingreso = 1, Egreso = 2, Traslado = 3
    }

    public enum TipoDeDocumentoElectronico
    {
        Ninguno = 0, 
        Factura = 1, 
        NotaDeCredito = 2, 
        ComplementoDePago = 7, 
        Traslado = 4, 
        Anticipo = 5 
    }

    public enum TipoDeDomicilio
    {
        Ninguno  = 0, Domicilio = 1, DomicilioFiscal = 2, ExpedidoEn = 3
    }

    public enum RetencionImpuestos
    {
        Ninguna = 0, IVA = 1, ISR = 2
    }

    public enum TrasladoImpuestos 
    {
        Ninguna = 0, IEPS = 1, IVA = 2 
    }
}
