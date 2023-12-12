using System;
using System.Collections.Generic;
using System.Text;

namespace Dll_IFacturacion
{
    public enum PACs_Timbrado
    {
        Ninguno = 0, 
        Tralix = 1, 
        PAX = 2,
        FoliosDigitales = 3,
        VirtualSoft = 4, 
        Desarrolaldores = 5 
    }

    public enum FormatosImagen
    {
        Ninguno = 0, Jpeg = 1, Bmp = 2, Gif = 3, Png = 4
    }

    public enum cfdTipoDePlantilla 
    {
        Ninguno = 0, 
        FAC = 1, NCR = 2, NDD = 3, CDI = 4, CPO = 5, NCC = 6 
    }

    public enum cfdMoneda 
    {
        Ninguno = 0, MXN = 1, USD = 2, EUR = 3 
    }

    public enum cfdImpuestosTrasladados 
    {
        Ninguna = 0, IEPS = 1, IVA = 2 
    }

    public enum cfdImpuestosRetenidos 
    {
        Ninguna = 0, IVA = 1, ISR = 2
    }

    public enum cfdFormatoDocumento 
    {
        Ninguna = 0, XML = 1, PDF = 2, AMBOS = 3 
    }

    public enum cfdTipoDeDomicilio 
    {
        Ninguno = 0, Domicilio = 1, DomicilioFiscal = 2, ExpedidoEn = 3
    }

    public enum TiposDeEstablecimiento 
    {
        Ninguno = 0,
        Emisor = 1,
        Receptor = 2
    }
}

