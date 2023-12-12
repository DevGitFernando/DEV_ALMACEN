using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.EnterpriseServices.Internal; 

using System.Web;
using System.Web.Services;
using System.Web.Services.Configuration;
using System.Web.Services.Description;
using System.Web.Services.Discovery;
using System.Web.Services.Protocols;

using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

using DllTransferenciaSoft; 
using DllTransferenciaSoft.wsCliente;

namespace DllTransferenciaSoft.EnviarInformacion
{
    /// <summary>
    /// Clase encargada de enviar información desde Oficina Central a Farmacias y de Farmacias a Oficina Central.
    /// Sólo es necesario especificar la URL del destino no importa si es Oficina Central ó Farmacia.
    /// </summary>
    public class clsEnviarInformacion
    {
        #region Declaracion de variables 
        string Name = "clsEnviarInformacion"; 

        DllTransferenciaSoft.wsCliente.wsCnnCliente Cliente;
        //////DllTransferenciaSoft.wsClienteOficinaCentralRegional.wsCnnOficinaCentralRegional ClienteRegional; 
        //////DllTransferenciaSoft.wsOficinaCentralRegional.wsCnnOficinaCentralRegional OficinaCentralRegional; 
        //////DllTransferenciaSoft.wsOficinaCentral.wsCnnOficinaCentral OficinaCentral;

        DllTransferenciaSoft.wsClienteOficinaCentralRegional.wsCnnClienteOficinaCentralRegional ClienteRegional;
        DllTransferenciaSoft.wsOficinaCentralRegional.wsCnnOficinaCentralRegional OficinaCentralRegional;
        DllTransferenciaSoft.wsOficinaCentral.wsCnnOficinaCentral OficinaCentral;

        TipoServicio tpEnviar = TipoServicio.Ninguno;
        string sUrl = "";

        clsGrabarError Error;

        string sFTP_Servidor = "";
        string sFTP_Usuario = "";
        string sFTP_Password = "";
        int iMB_File = 5;
        TamañoFiles tpMedida = TamañoFiles.MB; 

        #endregion Declaracion de variables

        #region Construtor y Destructor
        public clsEnviarInformacion(TipoServicio Enviar)
        {
            Cliente = new DllTransferenciaSoft.wsCliente.wsCnnCliente();
            OficinaCentral = new DllTransferenciaSoft.wsOficinaCentral.wsCnnOficinaCentral();
            OficinaCentralRegional = new DllTransferenciaSoft.wsOficinaCentralRegional.wsCnnOficinaCentralRegional();
            ClienteRegional = new DllTransferenciaSoft.wsClienteOficinaCentralRegional.wsCnnClienteOficinaCentralRegional(); 


            this.tpEnviar = Enviar; 
            Error = new clsGrabarError(General.DatosConexion, Transferencia.DatosApp, this.Name);
            Error.MostrarErrorAlGrabar = false; 
        }
        public clsEnviarInformacion(string URL, TipoServicio Enviar)
        {
            this.sUrl = URL;
            Cliente = new DllTransferenciaSoft.wsCliente.wsCnnCliente();
            Cliente.Url = URL;

            OficinaCentral = new DllTransferenciaSoft.wsOficinaCentral.wsCnnOficinaCentral();
            OficinaCentral.Url = URL;

            OficinaCentralRegional = new DllTransferenciaSoft.wsOficinaCentralRegional.wsCnnOficinaCentralRegional();
            OficinaCentralRegional.Url = URL;

            ClienteRegional = new DllTransferenciaSoft.wsClienteOficinaCentralRegional.wsCnnClienteOficinaCentralRegional();
            ClienteRegional.Url = URL; 

            this.tpEnviar = Enviar;
            Error = new clsGrabarError(General.DatosConexion, Transferencia.DatosApp, this.Name);
            Error.MostrarErrorAlGrabar = false; 
        }
        #endregion Construtor y Destructor

        #region Propiedades
        public string Url
        {
            get { return sUrl; }
            set 
            { 
                sUrl = value;
                Cliente.Url = sUrl;
                ClienteRegional.Url = sUrl; 
                OficinaCentral.Url = sUrl;
                OficinaCentralRegional.Url = sUrl;
            }
        }

        public TipoServicio Destino
        {
            get { return tpEnviar; }
            set { tpEnviar = value; }
        }

        public string FTP_Server
        {
            get { return sFTP_Servidor ; }
            set { sFTP_Servidor = value; }
        }

        public string FTP_Usuario
        {
            get { return sFTP_Usuario ; }
            set { sFTP_Usuario = value; }
        }

        public string FTP_Password
        {
            get { return sFTP_Password ; }
            set { sFTP_Password = value; }
        }

        public TamañoFiles FTP_Medida_Files
        {
            get { return tpMedida; }
            set { tpMedida = value; }
        }

        public int FTP_Tamaño_File
        {
            get { return iMB_File; }
            set 
            { 
                iMB_File = value;
                if (value <= 0)
                {
                    iMB_File = 5; 
                }
            }
        }
        #endregion Propiedades

        #region Enviar informacion 
        private bool validarTamañoArchivo(long Tamaño)
        {
            bool bRegresa = false;

            if (tpMedida == TamañoFiles.MB)
            {
                bRegresa = Tamaño >= ((long)LongFiles.MB * iMB_File); 
            }

            if (tpMedida == TamañoFiles.KB)
            {
                bRegresa = Tamaño >= ((long)LongFiles.KB * iMB_File);
            }

            return bRegresa; 
        }

        public bool Enviar(string ArchivoCfg, string RutaArchivo, string NombreDestino, string Estado, string Farmacia)
        {
            return Enviar(ArchivoCfg, RutaArchivo, NombreDestino, Estado, Farmacia, false); 
        }

        public bool Enviar_SQL(string ArchivoCfg, string RutaArchivo, string NombreDestino, string Estado, string Farmacia)
        {
            return Enviar(ArchivoCfg, RutaArchivo, NombreDestino, Estado, Farmacia, true);
        }

        private bool Enviar(string ArchivoCfg, string RutaArchivo, string NombreDestino, string Estado, string Farmacia, bool EsDestinoCentralizado)
        {
            bool bRegresa = false;

            FileInfo file = new FileInfo(RutaArchivo);

            if (EsDestinoCentralizado)
            {
                System.Console.WriteLine("Enviar via web");
                bRegresa = EnviarInformacionWeb(ArchivoCfg, RutaArchivo, NombreDestino, Estado, Farmacia, EsDestinoCentralizado);
            }
            else
            {
                //////if (file.Length >= ((long)LongFiles.MB * iMB_File))  
                if (validarTamañoArchivo(file.Length))
                {
                    System.Console.WriteLine("Enviar via FTP");
                    bRegresa = EnviarInformacionFTP(RutaArchivo, NombreDestino);
                }
                else
                {
                    System.Console.WriteLine("Enviar via web");
                    bRegresa = EnviarInformacionWeb(ArchivoCfg, RutaArchivo, NombreDestino, Estado, Farmacia);
                }
            }
            //// Liberar recurso 
            file = null; 

            return bRegresa; 
        }

        private bool EnviarInformacionFTP(string RutaArchivo, string NombreDestino)
        {
            bool bRegresa = false;

            SC_SolutionsSystem.FTP.clsFTP FTP =  new SC_SolutionsSystem.FTP.clsFTP();
            string sPath = @"/Integracion/";

            ////////// 
            //////FTP.Host = Transferencia.ServidorFTP;
            //////FTP.Usuario = "FtpUser";
            //////FTP.Password = SC_SolutionsSystem.Criptografia.clsPassword.FTP(FTP.Host, FTP.Usuario);

            FTP.Host = sFTP_Servidor;
            FTP.Usuario = sFTP_Usuario;
            FTP.Password = sFTP_Password; 

            FTP.PrepararConexion();
            FTP.CrearDirectorio(sPath);

            // sDestino = @"C:\SII_PUNTO_DE_VENTA\respaldo_de_base_de_datos" + "\\" + svrSql.BackUp.NombreArchivo + ".SII";
            bRegresa = FTP.SubirArchivo(RutaArchivo, sPath, NombreDestino);

            return bRegresa; 
        }

        private bool EnviarInformacionWeb(string ArchivoCfg, string RutaArchivo, string NombreDestino, string Estado, string Farmacia)
        {
            return EnviarInformacionWeb(ArchivoCfg, RutaArchivo, NombreDestino, Estado, Farmacia, false); 
        }

        private bool EnviarInformacionWeb(string ArchivoCfg, string RutaArchivo, string NombreDestino, string Estado, string Farmacia, bool EsDestinoCentralizado)
        {
            bool bRegresa = false;
            string sError = ""; 

            try
            {
                int iTimeOut = 1000 * (10); // 45 Seg 
                byte[] Buffer = ArchivoEnBytes(RutaArchivo);
                Cliente.Timeout = iTimeOut; //(int)Buffer.Length; 
                ClienteRegional.Timeout = iTimeOut; 
                OficinaCentral.Timeout = iTimeOut;
                OficinaCentralRegional.Timeout = iTimeOut;

                if (tpEnviar == TipoServicio.Cliente)
                {
                    if (Cliente.TestConection())
                    {
                        iTimeOut = 1000 * (60 * 10); // 10 Minutos 
                        Cliente.Timeout = iTimeOut;
                        bRegresa = Cliente.Informacion(ArchivoCfg, NombreDestino, Buffer, Estado, Farmacia);
                    }
                }

                if (tpEnviar == TipoServicio.ClienteOficinaCentralRegional)
                {
                    if (ClienteRegional.TestConection())
                    {
                        iTimeOut = 1000 * (60 * 10); // 10 Minutos 
                        ClienteRegional.Timeout = iTimeOut;
                        bRegresa = ClienteRegional.Informacion(ArchivoCfg, NombreDestino, Buffer);
                    }
                }


                if (tpEnviar == TipoServicio.OficinaCentral)
                {
                    if (OficinaCentral.TestConection())
                    {
                        iTimeOut = 1000 * (60 * 10); // 10 Minutos 
                        OficinaCentral.Timeout = iTimeOut; 
                        bRegresa = OficinaCentral.Informacion(ArchivoCfg, NombreDestino, Buffer);
                    }
                }

                if (tpEnviar == TipoServicio.OficinaCentralRegional)
                {
                    if (OficinaCentralRegional.TestConection())
                    {
                        iTimeOut = 1000 * (60 * 10); // 10 Minutos 
                        OficinaCentralRegional.Timeout = iTimeOut;

                        if (EsDestinoCentralizado)
                        {
                            iTimeOut = 1000 * (60 * 60); // 60 Minutos 
                            System.Console.WriteLine(string.Format("{0} enviando  {1}", DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"), NombreDestino));
                            bRegresa = OficinaCentralRegional.ReplicacionInformacion(NombreDestino, Buffer);
                            System.Console.WriteLine(string.Format("{0} envío  {1} finalizado", DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"), NombreDestino));
                            System.Console.WriteLine("");
                            System.Console.WriteLine(""); 

                            System.Console.WriteLine(string.Format("{0}", bRegresa ? "Información integrada satisfactoriamente" : "Ocurrió un error durante el proceso de integración."));

                        }
                        else
                        {
                            bRegresa = OficinaCentralRegional.Informacion(ArchivoCfg, NombreDestino, Buffer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); 

                sError = ex.Message + " ==> " + sUrl; 
                System.Console.WriteLine(sError);

                Error.GrabarError(sError, "Enviar"); 
                bRegresa = false;
            }

            return bRegresa;
        }
        #endregion Enviar informacion

        #region Procesar archivos 
        private byte[] ArchivoEnBytes(string RutaArchivo)
        {
            FileStream Filex;
            byte[] Buffer = null;

            if (File.Exists(RutaArchivo))
            {
                try
                {
                    Filex = new FileStream(RutaArchivo, FileMode.Open, FileAccess.Read);
                    Buffer = new byte[Filex.Length];
                    Filex.Read(Buffer, 0, (int)Filex.Length);
                    Filex.Close();
                }
                catch // (Exception ex ) 
                {
                    // ex = null;
                }
            }

            return Buffer;
        }
        #endregion Procesar archivos
    }
}
