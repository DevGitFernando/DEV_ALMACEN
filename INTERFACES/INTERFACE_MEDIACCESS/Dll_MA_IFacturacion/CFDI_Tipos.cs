using System;
using System.Collections.Generic;
using System.Text;

namespace Dll_MA_IFacturacion
{
    public enum PACs_Timbrado
    {
        Ninguno = 0, 
        Tralix = 1, 
        PAX = 2,
        FoliosDigitales = 3,
        VirtualSoft = 4 
    }

    public enum Modulo_CFDI
    {
        Ninguno = 0,
        Facturacion_Centralizada = 1,
        Facturacion_Sucursal = 2, 
        Nomina = 99 
    }

    public enum NM_TipoDetalle
    {
        Ninguno = 0,
        Percepcion = 1,
        Deduccion = 2,
        Incapacidad = 3,
        Horas_Extras = 4
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

    #region Interface de usuario
    public enum ColsDireccion
    {
        IdDireccion = 1, IdEstado = 2, Estado = 3, IdMunicipio = 4, Municipio = 5, IdColonia = 6, Colonia = 7,
        Direccion = 8, CodigoPostal = 9, Status = 10
    }

    public enum ColsEmails
    {
        IdEmail = 1, IdTipoEMail = 2, TipoMail = 3, Email = 4, Status = 5
    }

    public enum ColsEmails_Sucursales
    {
        IdEmail = 1, IdTipoEMail = 2, TipoMail = 3, Email = 4, DescripcionUso = 5, Status = 6,
        Servidor = 7, Puerto = 8, TiempoDeEspera = 9,
        Usuario = 10, Password = 11, EnableSSL = 12, EmailRespuesta = 13, NombreParaMostrar = 14, CC = 15, Asunto = 16, Cuerpo_Correo = 17
    }

    public enum ColsTelefonos
    {
        IdTelefono = 1, IdTipoTelefono = 2, TipoTelefono = 3, Telefono = 4, Status = 5
    }

    public enum EnvioDeCorreos
    {
        Ninguno = 0,
        Clientes = 1 
    }
    #endregion Interface de usuario
}

