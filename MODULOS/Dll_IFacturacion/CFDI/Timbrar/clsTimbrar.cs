using System;
using System.Collections.Generic;
using System.Text;
using System.IO; 

using Dll_IFacturacion;
using Dll_IFacturacion.FD;
using Dll_IFacturacion.PAX;
using Dll_IFacturacion.VirtualSoft;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

namespace Dll_IFacturacion.CFDI.Timbrar
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
        static devwsGenerarDocumento devTimbrar;

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

                case PACs_Timbrado.Desarrolaldores:
                    bRegresa = Timbar_DEV(XML);
                    break;

                case PACs_Timbrado.PAX:
                    bRegresa = Timbar_PAX(XML);
                    break;
            }

            return bRegresa;
        }

        public static bool CancelarCFDI( string RFC_Receptor, double Importe, string UUID, string ClaveMotivoCancelacion_SAT, string UUID_Relacionado )
        {
            bool bRegresa = false;

            switch (tpPAC)
            {
                case PACs_Timbrado.FoliosDigitales:
                    bRegresa = Cancelar_FD(RFC_Receptor, Importe, UUID, ClaveMotivoCancelacion_SAT, UUID_Relacionado);
                    break;

                case PACs_Timbrado.VirtualSoft:
                    bRegresa = Cancelar_VS(UUID, ClaveMotivoCancelacion_SAT, UUID_Relacionado);
                    break;

                case PACs_Timbrado.PAX:
                    bRegresa = Cancelar_PAX(UUID, ClaveMotivoCancelacion_SAT, UUID_Relacionado);
                    break;
            }

            return bRegresa;
        }

        public static bool DescargarXML(string Serie, string Folio, string UUID)
        {
            bool bRegresa = false;

            switch (tpPAC)
            {
                case PACs_Timbrado.FoliosDigitales:
                    bRegresa = DescargarXML_FD(UUID);
                    break;

                case PACs_Timbrado.VirtualSoft:
                    bRegresa = DescargarXML_VS(UUID);
                    break;

                case PACs_Timbrado.PAX:
                    bRegresa = DescargarXML_PAX(UUID);
                    break;
            }

            if(bRegresa)
            {
                string sRutaDescargaXML = Path.Combine(DtIFacturacion.RutaCFDI, "DescargaDeXML");
                string sFileName = string.Format("{0}__{1}_{2}___{3}", sRFCEmisor, Serie, Folio.Replace(",", ""), UUID); 
                DtIFacturacion.CrearDirectorio(sRutaDescargaXML);

                using(StreamWriter writer = new StreamWriter(string.Format(@"{0}", Path.Combine(sRutaDescargaXML, sFileName + ".xml"))))
                {
                    writer.Write(XmlConTimbreFiscal);
                    writer.Close();
                }

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

        private static bool Cancelar_FD( string RFC_Receptor, double Importe, string UUID, string ClaveMotivoCancelacion_SAT, string UUID_Relacionado )
        {
            bool bRegresa = false;

            fdTimbrar = new fdGenerarDocumento(General.DatosConexion);
            fdTimbrar.RFC_Emisor = sRFCEmisor;
            bRegresa = fdTimbrar.CancelarDocumento(RFC_Receptor, Importe, UUID, ClaveMotivoCancelacion_SAT, UUID_Relacionado);

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

        private static bool Cancelar_PAX(string UUID, string ClaveMotivoCancelacion_SAT, string UUID_Relacionado )
        {
            bool bRegresa = false;
            PAC_Info info = new PAC_Info();

            info.PAC = PACs_Timbrado.PAX; 
            info.Url = sUrlTimbrado;
            info.Usuario = sUsuarioTimbrado;
            info.Password = sPasswordTimbrado; 

            paxTimbrar = new paxwsGenerarDocumento(General.DatosConexion, info);
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

        private static bool Cancelar_VS(string UUID, string ClaveMotivoCancelacion_SAT, string UUID_Relacionado )
        {
            bool bRegresa = false;
            PAC_Info info = new PAC_Info();

            info.PAC = PACs_Timbrado.VirtualSoft;
            info.Url = sUrlTimbrado;
            info.Usuario = sUsuarioTimbrado;
            info.Password = sPasswordTimbrado;

            vsTimbrar = new vswsGenerarDocumento(General.DatosConexion, info);
            bRegresa = vsTimbrar.CancelarDocumento(UUID, sRFCEmisor, ClaveMotivoCancelacion_SAT, UUID_Relacionado);

            sXmlAcuseCancelacion = vsTimbrar.XmlAcuseCancelacion;
            bOcurrioError_AlGenerar = vsTimbrar.Error_AlGenerar;
            sMensajeDeError = vsTimbrar.MensajeError;

            return bRegresa;
        }

        private static bool DescargarXML_VS(string UUID)
        {
            bool bRegresa = false;
            PAC_Info info = new PAC_Info();

            info.PAC = PACs_Timbrado.VirtualSoft;
            info.Url = sUrlTimbrado;
            info.Usuario = sUsuarioTimbrado;
            info.Password = sPasswordTimbrado;
            //info.RFC_Emisor = sRFCEmisor;

            vsTimbrar = new vswsGenerarDocumento(General.DatosConexion, info);
            bRegresa = vsTimbrar.DescargarXML(sRFCEmisor, UUID);

            sXmlConTimbreFiscal = vsTimbrar.XmlConTimbreFiscal;
            bOcurrioError_AlGenerar = vsTimbrar.Error_AlGenerar;
            sMensajeDeError = vsTimbrar.MensajeError;

            return bRegresa;
        }
        #endregion VirtualSoft

        #region Develores 
        private static int ConsultarCreditos_DEV()
        {
            int iRegresa = 0;
            bool bRegresa = false;

            devTimbrar = new devwsGenerarDocumento(General.DatosConexion);
            devTimbrar.RFC_Emisor = sRFCEmisor;
            bRegresa = devTimbrar.ConsultarCreditos();

            bOcurrioError_AlGenerar = devTimbrar.Error_AlGenerar;
            sMensajeDeError = devTimbrar.MensajeError;
            iCreditos_Disponibles = devTimbrar.Creditos_Disponibles;

            return iCreditos_Disponibles;
        }

        private static bool Timbar_DEV(string XML)
        {
            bool bRegresa = false;

            XML = toUTF8(XML);
            devTimbrar = new devwsGenerarDocumento(General.DatosConexion);
            bRegresa = devTimbrar.GenerarDocumento(XML, sTipoDeDocumento);

            sXmlConTimbreFiscal = devTimbrar.XmlConTimbreFiscal;
            bOcurrioError_AlGenerar = devTimbrar.Error_AlGenerar;
            sMensajeDeError = devTimbrar.MensajeError;

            return bRegresa;
        }

        private static bool Cancelar_DEV(string UUID, string ClaveMotivoCancelacion_SAT, string UUID_Relacionado)
        {
            bool bRegresa = false;
            PAC_Info info = new PAC_Info();

            info.PAC = PACs_Timbrado.VirtualSoft;
            info.Url = sUrlTimbrado;
            info.Usuario = sUsuarioTimbrado;
            info.Password = sPasswordTimbrado;

            devTimbrar = new devwsGenerarDocumento(General.DatosConexion, info);
            bRegresa = devTimbrar.CancelarDocumento(UUID, sRFCEmisor, ClaveMotivoCancelacion_SAT, UUID_Relacionado);

            sXmlAcuseCancelacion = devTimbrar.XmlAcuseCancelacion;
            bOcurrioError_AlGenerar = devTimbrar.Error_AlGenerar;
            sMensajeDeError = devTimbrar.MensajeError;

            return bRegresa;
        }

        private static bool DescargarXML_DEV(string UUID)
        {
            bool bRegresa = false;
            PAC_Info info = new PAC_Info();

            info.PAC = PACs_Timbrado.VirtualSoft;
            info.Url = sUrlTimbrado;
            info.Usuario = sUsuarioTimbrado;
            info.Password = sPasswordTimbrado;
            //info.RFC_Emisor = sRFCEmisor;

            devTimbrar = new devwsGenerarDocumento(General.DatosConexion, info);
            bRegresa = devTimbrar.DescargarXML(sRFCEmisor, UUID);

            sXmlConTimbreFiscal = vsTimbrar.XmlConTimbreFiscal;
            bOcurrioError_AlGenerar = vsTimbrar.Error_AlGenerar;
            sMensajeDeError = vsTimbrar.MensajeError;

            return bRegresa;
        }
        #endregion Develores 
        #endregion Funciones y Procedimientos Privados

    }
}
