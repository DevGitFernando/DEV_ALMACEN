using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace Dll_IFacturacion
{
    internal static class Certificado_Key
    {
        static string sFile = "";
        //static string sMD5 = "";
        //static string sFormato = "#,###,##0.0###";
        static string base64String = "";

        #region Constructores y Destructor de Clase 
        static Certificado_Key()
        {
        }

        ////Certificado_Key(string File) 
        ////{
        ////    sFile = File;
        ////}

        ////~Certificado_Key()
        ////{
        ////}
        #endregion Constructores y Destructor de Clase 

        #region Propiedades 
        public static string FileName
        {
            get { return sFile; }
            set { sFile = value;  }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public static string Codificar()
        {
            base64String = "";
            byte[] Buffer;
            long bytesRead = 0;

            try
            {
                FileStream inFile = new FileStream(sFile, FileMode.Open, FileAccess.Read);
                Buffer = new Byte[inFile.Length];
                bytesRead = inFile.Read(Buffer, 0, (int)inFile.Length);
                inFile.Close();

                base64String = Convert.ToBase64String(Buffer, 0, Buffer.Length); 
            }
            catch { }

            return base64String; 
        }

        public static bool Decodificar(string Archivo, string RutaDestino, string PaqueteDatos)
        {
            return Decodificar(Archivo, RutaDestino, PaqueteDatos, false); 
        }

        public static bool Decodificar(string Archivo, string RutaDestino, string PaqueteDatos, bool RevisarExistencia)
        {
            bool bRegresa = true;
            bool bGuardar = true; 
            //string sArchivoOrigen = ""; 
            byte[] Buffer;
            string sFile = ""; 

            try
            {
                // while (Version.Leer())
                {
                    sFile = RutaDestino + "\\" + Archivo;
                    if (RevisarExistencia)
                    {
                        if (File.Exists(sFile))
                        {
                            bGuardar = false; 
                        }
                    }

                    if (bGuardar)
                    {
                        // sArchivoOrigen = Archivo;
                        Buffer = System.Convert.FromBase64String(PaqueteDatos);
                        Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sFile, Buffer, false);
                    }
                }
            }
            catch 
            {
                bRegresa = false; 
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        #endregion Funciones y Procedimientos Privados
    }
}
