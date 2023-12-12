using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;


using SC_SolutionsSystem.Compresion;
using SC_SolutionsSystem.FuncionesGenerales;


namespace DllTransferenciaSoft.Zip
{
    public class clsCriptografia
    {
        internal class Archivos
        {
            string sFile = "";
            string sFileCom = "";

            public Archivos(string Archivo, string ArchivoCom)
            {
                sFile = Archivo;
                sFileCom = ArchivoCom;
            }

            public string File
            {
                get { return sFile; }
                set { sFile = value; }
            }

            public string FileCom
            {
                get { return sFileCom; }
                set { sFileCom = value; }
            }

        }

        #region Declaracion de variables 
        private basGenerales Fg = new basGenerales();

        private string strKey = "SC_Solutions".ToUpper();
        private string strIV = "snoituloS_CS ".ToUpper();

        // private ArrayList Files; 
        private byte []pKey = new byte[32];
        private byte []pIV = new byte[16];

        #endregion Declaracion de variables
        
        #region Constructores y destructor de la clase
        public clsCriptografia()
        {
            pKey = System.Text.Encoding.UTF8.GetBytes(this.FormateaCadena(strKey, 32));
            pIV = System.Text.Encoding.UTF8.GetBytes(this.FormateaCadena(strIV, 16));
        }

        ~clsCriptografia()
        {
            strKey = null;
            strIV = null;
            pKey = null;
            pIV = null;
        }
        #endregion Constructores y destructor de la clase

        #region Propiedades publicas 
        //public string Key
        //{
        //    get { return byKey.ToString(); }
        //}
        //public string IV
        //{
        //    get { return byIV.ToString(); }
        //}
        #endregion Propiedades publicas

        #region Claves de encripcion 
        //private string ConvertByteToStringKey()
        //{
        //    string sRegresa = "";
        //    return sRegresa;
        //}

        //private string ConvertByteToStringIV()
        //{
        //    string sRegresa = "";
        //    return sRegresa;
        //}
        private string FormateaCadena(string Valor, int Largo)
        {
            string sRegresa = Valor.Trim().ToUpper() + Fg.PonCeros(0, Largo);
            sRegresa = Fg.Left(sRegresa, Largo);
            return sRegresa;
        }

        #endregion Claves de encripcion

        public bool EncriptarArchivo(string ArchivoOrigen, string ArchivoDestino, bool BorrarOrigen)
        {
            bool bRegresa = false;

            try
            {
                if (File.Exists(ArchivoDestino))
                    File.Delete(ArchivoDestino);

                FileStream fStream = new FileStream(ArchivoDestino, FileMode.OpenOrCreate);
                StreamReader FileScr = new StreamReader(ArchivoOrigen);
                string sDataSource = FileScr.ReadToEnd();

                // Desocupar el archivo 
                FileScr.Close();
                FileScr = null;

                // Crear una instancia de Rijndael 
                Rijndael RijndaelAlg = Rijndael.Create();
                CryptoStream cStream = new CryptoStream(fStream, RijndaelAlg.CreateEncryptor(pKey, pIV), CryptoStreamMode.Write);

                // Generar el archivo encriptado 
                StreamWriter sWriter = new StreamWriter(cStream);

                try
                {
                    sWriter.WriteLine(sDataSource);
                    sWriter.Close();
                    cStream.Close();
                    fStream.Close();
                    bRegresa = true;

                    if (BorrarOrigen)
                        File.Delete(ArchivoOrigen);

                }
                catch { }
                finally
                {
                    FileScr = null;
                    sWriter = null;
                    cStream = null;
                    fStream = null;
                    GC.Collect();
                }
                bRegresa = true;
            }
            catch { }
            return bRegresa;
        }

        public bool DesencriptarArchivo(string ArchivoOrigen, string ArchivoDestino, bool Borrar)
        {
            bool bRegresa = false;

            try
            {
                if (File.Exists(ArchivoDestino))
                    File.Delete(ArchivoDestino);

                FileStream fStream = new FileStream(ArchivoOrigen, FileMode.OpenOrCreate);
                StreamWriter fDestino = new StreamWriter(ArchivoDestino);

                // Crear una instancia de Rijndael 
                Rijndael RijndaelAlg = Rijndael.Create();
                CryptoStream cStream = new CryptoStream(fStream, RijndaelAlg.CreateDecryptor(pKey, pIV), CryptoStreamMode.Read);

                // Generar el archivo encriptado 
                StreamReader sReader = new StreamReader(cStream);

                try
                {
                    string sDataSource = sReader.ReadToEnd();
                    fDestino.Write(sDataSource);

                    sReader.Close();
                    cStream.Close();
                    fDestino.Close();
                    fStream.Close();
                    bRegresa = true;

                    if (Borrar)
                        File.Delete(ArchivoOrigen);
                }
                catch { }
                finally
                {
                    sReader = null;
                    cStream = null;
                    fDestino = null;
                    fStream = null;
                    GC.Collect();
                }
                bRegresa = true;
            }
            catch { }

            return bRegresa;
        }

    }
}
