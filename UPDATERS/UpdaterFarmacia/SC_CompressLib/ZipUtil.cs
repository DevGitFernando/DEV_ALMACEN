using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Security.Cryptography;
using Microsoft.VisualBasic;

using SC_CompressLib;
using SC_CompressLib.Utils;
using SC_CompressLib.Zip;
using SC_CompressLib.Zlib;

namespace SC_CompressLib 
{
    public class ZipUtil
    {
        string Encoding = "utf-8";

        public string Type = "";

        public ZipUtil(string SSID)
        {
            Type = SSID; 
        }

        private string GenerarMD5(string Cadena)
        {
            string sMD5 = "";
            byte[] bytesCadena, bytesRegresa;
            MD5CryptoServiceProvider MD5Crypto = new MD5CryptoServiceProvider();

            bytesCadena = System.Text.Encoding.UTF8.GetBytes(Cadena);
            bytesRegresa = MD5Crypto.ComputeHash(bytesCadena);

            sMD5 = BitConverter.ToString(bytesRegresa);
            sMD5 = sMD5.Replace("-", "").ToLower();

            return sMD5;
        }

        public bool Comprimir(string fileName, string ZipDeSalida, bool Borrar)
        {
            // string []files = Directory.GetFiles(fileName, "*.*");
            return Comprimir(fileName, "*.*", ZipDeSalida, Borrar);
        }

        public bool Comprimir(string fileName, string Tipo, string ZipDeSalida, bool Borrar)
        {
            string[] files = Directory.GetFiles(fileName, Tipo);
            return Comprimir(files, ZipDeSalida, Borrar);
        }

        public bool Comprimir(string[] fileNames, string ZipDeSalida, bool Borrar)
        {
            bool bRegresa = false;

            try
            {
                if (File.Exists(ZipDeSalida))
                    File.Delete(ZipDeSalida);

                //var options = new SaveWorkerOptions();

                //options.ZipName = ZipDeSalida;
                //options.Password = GenerarMD5(Transferencia.Modulo);
                //options.Encoding = "utf-8";
                ZipFile zip = new ZipFile(ZipDeSalida);

                zip.ProvisionalAlternateEncoding = System.Text.Encoding.GetEncoding(Encoding);
                zip.Comment = "";
                zip.Password = GenerarMD5(Type);
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;

                foreach (string sFile in fileNames)
                {
                    zip.AddItem(sFile, "");
                }

                zip.TempFileFolder = System.IO.Path.GetDirectoryName(ZipDeSalida);
                zip.UseZip64WhenSaving = Zip64Option.Always;
                zip.CompressionLevel = CompressionLevel.Level6;

                zip.Save(ZipDeSalida);
                bRegresa = true;

                if (Borrar)
                {
                    foreach (string sFile in fileNames)
                    {
                        File.Delete(sFile);
                    } 
                }

            }
            catch ( Exception ex )
            {
                ex.Source = ex.Source; 
            }

            return bRegresa;
        }

        public bool Descomprimir(string DirectorioDestino, string ZipDeEntrada)
        {
            bool bRegresa = false;

            try
            {
                ZipFile zip = ZipFile.Read(ZipDeEntrada);

                zip.Password = GenerarMD5(Type);
                zip.ExtractAll(DirectorioDestino, ExtractExistingFileAction.OverwriteSilently);
                bRegresa = true;
            }
            catch { }

            return bRegresa;
        }

    }
}
