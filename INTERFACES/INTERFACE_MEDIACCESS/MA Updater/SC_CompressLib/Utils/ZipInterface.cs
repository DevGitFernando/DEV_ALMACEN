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

namespace  SC_CompressLib.Utils
{
    public class ZipInterface
    {
        string Encoding = "utf-8"; 
        public string Type = "";

        Exception exError = new Exception("Sin Error"); 

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

        public Exception Error
        {
            get { return exError;  }
        }

        public bool Comprimir(string fileName, string ZipDeSalida, bool Borrar)
        {
            string []files = {fileName};
            return Comprimir(files, ZipDeSalida, "", "");
        }

        public bool Comprimir(string[] fileNames, string ZipDeSalida, string Password, string Comentarios)
        {
            bool bRegresa = false;

            try
            {
                if (File.Exists(ZipDeSalida))
                {
                    File.Delete(ZipDeSalida);
                } 

                //var options = new SaveWorkerOptions();

                //options.ZipName = ZipDeSalida;
                //options.Password = GenerarMD5(Transferencia.Modulo);
                //options.Encoding = "utf-8";
                ZipFile zip = new ZipFile(ZipDeSalida);

                zip.ProvisionalAlternateEncoding = System.Text.Encoding.GetEncoding(Encoding);
                zip.Comment = Comentarios;

                if (Password != "")
                {
                    zip.Password = Password;
                }

                zip.Encryption = EncryptionAlgorithm.WinZipAes256;

                foreach (string sFile in fileNames)
                {
                    zip.AddItem(sFile, "");
                }

                zip.TempFileFolder = System.IO.Path.GetDirectoryName(ZipDeSalida);
                zip.UseZip64WhenSaving = Zip64Option.Always;
                zip.CompressionLevel = CompressionLevel.Level6;

                zip.Save(ZipDeSalida);
                zip.Dispose(); 
                bRegresa = true; 

            }
            catch ( Exception ex )
            {
                exError = ex ; 
            }

            return bRegresa;
        }

        public bool Descomprimir(string DirectorioDestino, string ZipDeEntrada, string Password)
        {
            bool bRegresa = false;

            try
            {
                ZipFile zip = ZipFile.Read(ZipDeEntrada); 
                try
                {
                    if (zip.Entries.Count > 0)
                    {
                        if (zip.Entries[0]._Encryption != EncryptionAlgorithm.None)
                        {
                            if (Password != "")
                            {
                                zip.Password = GenerarMD5(Password);
                            } 
                        }
                    }
                }
                catch (Exception ex)
                {
                    exError = ex;
                } 

                zip.ExtractAll(DirectorioDestino, ExtractExistingFileAction.OverwriteSilently);
                zip.Dispose(); 

                bRegresa = true;
            }
            catch (Exception ex)
            {
                exError = ex; 
            }

            return bRegresa;
        } 
    }
}
