using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic;


namespace Dll_MA_IFacturacion.CFDI 
{
    public class clsArchivo
    {
        string sFile = "";
        string sMD5 = "";
        string sFormato = "#,###,##0.0###";
        string base64String = "";

        #region Constructores y Destructor de Clase 
        public clsArchivo()
        { 
        }

        public clsArchivo(string File)
        {
            sFile = File;
        }

        ~clsArchivo()
        {
        }
        #endregion Constructores y Destructor de Clase 

        #region Funciones y Procedimientos Publicos 
        public string Codificar()
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

        public bool Decodificar(string Archivo, string RutaDestino, string PaqueteDatos)
        {
            return Decodificar(Archivo, RutaDestino, PaqueteDatos, false); 
        }

        public bool Decodificar(string Archivo, string RutaDestino, string PaqueteDatos, bool RevisarExistencia)
        {
            bool bRegresa = true;
            bool bGuardar = true; 
            string sArchivoOrigen = ""; 
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
