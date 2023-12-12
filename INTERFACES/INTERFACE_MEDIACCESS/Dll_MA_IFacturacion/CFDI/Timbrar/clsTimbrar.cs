using System;
using System.Collections.Generic;
using System.Text;

using Dll_MA_IFacturacion;
using Dll_MA_IFacturacion.PACs.FD;
using Dll_MA_IFacturacion.PACs.PAX;
using Dll_MA_IFacturacion.VirtualSoft;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data; 

namespace Dll_MA_IFacturacion.CFDI.Timbrar
{
    public static class clsTimbrar
    {
        #region Declaracion de variables
        static bool bEnProduccion = false;
        static string sUrlTimbrado = "";
        static string sUsuarioTimbrado = "";
        static string sPasswordTimbrado = "";
        static string sRFCEmisor = "";

        static string sCertificadoPKCS12_Base64 = "";
        static string sPasswordPKCS12 = "";

        static PACs_Timbrado tpPAC = PACs_Timbrado.Ninguno;

        static string sTipoDeDocumento = "";
        static string sXML_Timbrar = "";
        static string sXmlConTimbreFiscal = "";
        static string sXmlAcuseCancelacion = "";
        static string sMensajeDeError = "";
        static bool bOcurrioError_AlGenerar = false;
        static int iCreditos_Disponibles = 0;

        static paxwsGenerarDocumento paxTimbrar; // = new paxwsGenerarDocumento(General.DatosConexion); 
        static fdGenerarDocumento fdTimbrar; // = new paxwsGenerarDocumento(General.DatosConexion); 
        static vswsGenerarDocumento vsTimbrar; 

        #endregion Declaracion de variables

        #region Constructor y Destructor de Clase
        static clsTimbrar()
        {
        }
        #endregion Constructor y Destructor de Clase

        #region Propiedades Publicas
        public static bool EnProduccion
        {
            get { return bEnProduccion; }
            set { bEnProduccion = value; }
        }

        public static string Url
        {
            get { return sUrlTimbrado; }
            set { sUrlTimbrado = value; }
        }

        public static string Usuario
        {
            get { return sUsuarioTimbrado; }
            set { sUsuarioTimbrado = value; }
        }

        public static string Password
        {
            get { return sPasswordTimbrado; }
            set { sPasswordTimbrado = value; }
        }

        public static PACs_Timbrado PAC
        {
            get { return tpPAC; }
            set { tpPAC = value; }
        }

        public static string RFC_Emisor
        {
            get { return sRFCEmisor; }
            set { sRFCEmisor = value; }
        }

        public static string TipoDeDocumento
        {
            get { return sTipoDeDocumento; }
            set { sTipoDeDocumento = value; }
        }

        public static string XmlConTimbreFiscal
        {
            get { return sXmlConTimbreFiscal; }
        }

        public static string XmlAcuseCancelacion
        {
            get { return sXmlAcuseCancelacion; }
        }

        public static string CertificadoPKCS12
        {
            get { return sCertificadoPKCS12_Base64; }
            set { sCertificadoPKCS12_Base64 = value; }
        }

        public static string PasswordPKCS12
        {
            get { return sPasswordPKCS12; }
            set { sPasswordPKCS12 = value; }
        }

        public static string XML_Timbrar
        {
            get { return sXML_Timbrar; }
            set { sXML_Timbrar = value; }
        }

        public static bool Error_AlGenerar
        {
            get { return bOcurrioError_AlGenerar; }
        }

        public static string MensajeError
        {
            get
            {
                string sMsj = "";
                if (bOcurrioError_AlGenerar)
                {
                    sMsj = sMensajeDeError;
                }
                return sMsj;
            }
        }

        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        public static void Preparar_ConexionTimbrado(string RFC, PAC_Info PAC_Informacion)
        {
            clsTimbrar.EnProduccion = PAC_Informacion.EnProduccion;
            clsTimbrar.RFC_Emisor = RFC;
            clsTimbrar.Url = PAC_Informacion.Url;
            clsTimbrar.Usuario = PAC_Informacion.Usuario;
            clsTimbrar.Password = PAC_Informacion.Password;
            clsTimbrar.CertificadoPKCS12 = PAC_Informacion.CertificadoPKCS12;
            clsTimbrar.PasswordPKCS12 = PAC_Informacion.PasswordPKCS12;
            clsTimbrar.PAC = PAC_Informacion.PAC;
        }

        public static int ConsultarCreditos()
        {
            int iRegresa = 0;

            switch (tpPAC)
            {
                case PACs_Timbrado.FoliosDigitales:
                    iRegresa = ConsultarCreditos_FD();
                    break;

                case PACs_Timbrado.VirtualSoft:
                    iRegresa = ConsultarCreditos_VS();
                    break;

                case PACs_Timbrado.PAX:
                    iRegresa = ConsultarCreditos_PAX();
                    break;
            }

            return iRegresa;
        }

        public static bool Timbrar()
        {
            return Timbrar(sXML_Timbrar);
        }

        public static bool Timbrar(string XML)
        {
            bool bRegresa = false;

            switch (tpPAC)
            {
                case PACs_Timbrado.FoliosDigitales:
                    bRegresa = Timbar_FD(XML);
                    break;

                case PACs_Timbrado.VirtualSoft:
                    bRegresa = Timbar_VS(XML);
                    break;

                case PACs_Timbrado.PAX:
                    bRegresa = Timbar_PAX(XML);
                    break;
            }

            return bRegresa;
        }

        public static bool CancelarCFDI(string UUID)
        {
            bool bRegresa = false;

            switch (tpPAC)
            {
                case PACs_Timbrado.FoliosDigitales:
                    bRegresa = Cancelar_FD(UUID);
                    break;

                case PACs_Timbrado.VirtualSoft:
                    bRegresa = Cancelar_VS(UUID);
                    break;

                case PACs_Timbrado.PAX:
                    bRegresa = Cancelar_PAX(UUID);
                    break;
            }

            return bRegresa;
        }

        public static bool DescargarXML(string UUID)
        {
            bool bRegresa = false;

            switch (tpPAC)
            {
                case PACs_Timbrado.FoliosDigitales:
                    bRegresa = DescargarXML_FD(UUID);
                    break;

                case PACs_Timbrado.PAX:
                    bRegresa = DescargarXML_PAX(UUID);
                    break;
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        public static string toUTF8(string Cadena)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(Cadena);
            return encoding.GetString(bytes);
        }

        #region FD
        private static int ConsultarCreditos_FD()
        {
            int iRegresa = 0;
            bool bRegresa = false;

            fdTimbrar = new fdGenerarDocumento(General.DatosConexion);
            fdTimbrar.RFC_Emisor = sRFCEmisor;
            bRegresa = fdTimbrar.ConsultarCreditos();

            bOcurrioError_AlGenerar = fdTimbrar.Error_AlGenerar;
            sMensajeDeError = fdTimbrar.MensajeError;
            iCreditos_Disponibles = fdTimbrar.Creditos_Disponibles;

            return iCreditos_Disponibles;
        }

        private static bool Timbar_FD(string XML)
        {
            bool bRegresa = false;

            XML = toUTF8(XML);
            fdTimbrar = new fdGenerarDocumento(General.DatosConexion);
            fdTimbrar.RFC_Emisor = sRFCEmisor;
            bRegresa = fdTimbrar.GenerarDocumento(XML, sTipoDeDocumento);

            sXmlConTimbreFiscal = fdTimbrar.XmlConTimbreFiscal;
            bOcurrioError_AlGenerar = fdTimbrar.Error_AlGenerar;
            sMensajeDeError = fdTimbrar.MensajeError;

            return bRegresa;
        }

        private static bool Cancelar_FD(string UUID)
        {
            bool bRegresa = false;

            fdTimbrar = new fdGenerarDocumento(General.DatosConexion);
            fdTimbrar.RFC_Emisor = sRFCEmisor;
            bRegresa = fdTimbrar.CancelarDocumento(UUID);

            sXmlAcuseCancelacion = fdTimbrar.XmlAcuseCancelacion;
            bOcurrioError_AlGenerar = fdTimbrar.Error_AlGenerar;
            sMensajeDeError = fdTimbrar.MensajeError;

            return bRegresa;
        }

        private static bool DescargarXML_FD(string UUID)
        {
            bool bRegresa = false;

            fdTimbrar = new fdGenerarDocumento(General.DatosConexion);
            fdTimbrar.RFC_Emisor = sRFCEmisor;
            bRegresa = fdTimbrar.ObtenerXML(UUID);

            sXmlConTimbreFiscal = fdTimbrar.XmlConTimbreFiscal;
            bOcurrioError_AlGenerar = fdTimbrar.Error_AlGenerar;
            sMensajeDeError = fdTimbrar.MensajeError;

            return bRegresa;
        }
        #endregion FD

        #region PAX
        private static int ConsultarCreditos_PAX()
        {
            int iRegresa = 0;
            bool bRegresa = false;

            //////fdTimbrar = new fdGenerarDocumento(General.DatosConexion);
            //////fdTimbrar.RFC_Emisor = sRFCEmisor;
            //////bRegresa = fdTimbrar.ConsultarCreditos();

            //////bOcurrioError_AlGenerar = fdTimbrar.Error_AlGenerar;
            //////sMensajeDeError = fdTimbrar.MensajeError;
            //////iCreditos_Disponibles = fdTimbrar.Creditos_Disponibles;

            return iCreditos_Disponibles;
        }

        private static bool Timbar_PAX(string XML)
        {
            bool bRegresa = false;

            XML = toUTF8(XML);
            paxTimbrar = new paxwsGenerarDocumento(General.DatosConexion);
            bRegresa = paxTimbrar.GenerarDocumento(XML, sTipoDeDocumento);

            sXmlConTimbreFiscal = paxTimbrar.XmlConTimbreFiscal;
            bOcurrioError_AlGenerar = paxTimbrar.Error_AlGenerar;
            sMensajeDeError = paxTimbrar.MensajeError;

            return bRegresa;
        }

        private static bool Cancelar_PAX(string UUID)
        {
            bool bRegresa = false;

            paxTimbrar = new paxwsGenerarDocumento(General.DatosConexion);
            bRegresa = paxTimbrar.CancelarDocumento(UUID, sRFCEmisor);

            sXmlAcuseCancelacion = paxTimbrar.XmlAcuseCancelacion;
            bOcurrioError_AlGenerar = paxTimbrar.Error_AlGenerar;
            sMensajeDeError = paxTimbrar.MensajeError;

            return bRegresa;
        }

        private static bool DescargarXML_PAX(string UUID)
        {
            bool bRegresa = false;

            paxTimbrar = new paxwsGenerarDocumento(General.DatosConexion);
            ////bRegresa = paxTimbrar.CancelarDocumento(UUID, sRFCEmisor);

            sXmlAcuseCancelacion = paxTimbrar.XmlAcuseCancelacion;
            bOcurrioError_AlGenerar = paxTimbrar.Error_AlGenerar;
            sMensajeDeError = paxTimbrar.MensajeError;

            return bRegresa;
        }

        #endregion PAX

        #region VirtualSoft
        private static int ConsultarCreditos_VS()
        {
            int iRegresa = 0;
            bool bRegresa = false;

            vsTimbrar = new vswsGenerarDocumento(General.DatosConexion);
            vsTimbrar.RFC_Emisor = sRFCEmisor;
            bRegresa = vsTimbrar.ConsultarCreditos();

            bOcurrioError_AlGenerar = vsTimbrar.Error_AlGenerar;
            sMensajeDeError = vsTimbrar.MensajeError;
            iCreditos_Disponibles = vsTimbrar.Creditos_Disponibles;

            return iCreditos_Disponibles;
        }

        private static bool Timbar_VS(string XML)
        {
            bool bRegresa = false;

            XML = toUTF8(XML);
            vsTimbrar = new vswsGenerarDocumento(General.DatosConexion);
            bRegresa = vsTimbrar.GenerarDocumento(XML, sTipoDeDocumento);

            sXmlConTimbreFiscal = vsTimbrar.XmlConTimbreFiscal;
            bOcurrioError_AlGenerar = vsTimbrar.Error_AlGenerar;
            sMensajeDeError = vsTimbrar.MensajeError;

            return bRegresa;
        }

        private static bool Cancelar_VS(string UUID)
        {
            bool bRegresa = false;
            PAC_Info info = new PAC_Info();

            info.PAC = PACs_Timbrado.VirtualSoft;
            info.Url = sUrlTimbrado;
            info.Usuario = sUsuarioTimbrado;
            info.Password = sPasswordTimbrado;

            vsTimbrar = new vswsGenerarDocumento(General.DatosConexion, info);
            bRegresa = vsTimbrar.CancelarDocumento(UUID, sRFCEmisor);

            sXmlAcuseCancelacion = vsTimbrar.XmlAcuseCancelacion;
            bOcurrioError_AlGenerar = vsTimbrar.Error_AlGenerar;
            sMensajeDeError = vsTimbrar.MensajeError;

            return bRegresa;
        }

        private static bool DescargarXML_VS(string UUID)
        {
            bool bRegresa = false;

            ////paxTimbrar = new paxwsGenerarDocumento(General.DatosConexion);
            ////////bRegresa = paxTimbrar.CancelarDocumento(UUID, sRFCEmisor);

            ////sXmlAcuseCancelacion = paxTimbrar.XmlAcuseCancelacion;
            ////bOcurrioError_AlGenerar = paxTimbrar.Error_AlGenerar;
            ////sMensajeDeError = paxTimbrar.MensajeError;

            return bRegresa;
        }
        #endregion VirtualSoft
        #endregion Funciones y Procedimientos Privados

    }
}
