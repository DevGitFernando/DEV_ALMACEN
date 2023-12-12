using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace UpdateFarmacia.FuncionesGenerales
{
    public class clsCriptografo
    {
        private string strKey = "SC_Solutions".ToUpper();
        private string strIV = "snoituloS_CS ".ToUpper();
        private int iLargoKey = 20;

        private basGenerales Fg = new basGenerales();

        #region Constructores y destructor de la clase
        public clsCriptografo()
        {           
        }

        ~clsCriptografo()
        {
            strKey = null;
            strIV = null;
        }
        #endregion Constructores y destructor de la clase

        #region Propiedades publicas
        //public string Key
        //{
        //    get
        //    {
        //        return ConvierteByteToStringKey();
        //    }
        //}

        //public string IV
        //{
        //    get
        //    {
        //        return ConvierteByteToStringKey();
        //    }
        //}
        #endregion

        #region Funciones y procedimientos privados
        private string Encripta512(string prtKey)
        {
            return Encripta512(prtKey, iLargoKey);
        }

        private string Encripta512(string prtKey, int LargoClave)
        {
            string sRegresa = "";
            SHA512Managed myHash = new SHA512Managed();
            byte[] bytValue;
            byte[] bytHash;

            // Cadena clave para encriptar
            bytValue = System.Text.Encoding.UTF8.GetBytes(prtKey);
            bytHash = myHash.ComputeHash(bytValue);

            myHash.Clear();
            sRegresa = Convert.ToBase64String(bytHash);
            sRegresa = Fg.Left(sRegresa, LargoClave);

            return sRegresa;
        }        
        #endregion Funciones y procedimientos privados 


        #region Generacion de Password Usuarios 
        public string PasswordEncriptar(string strCadena)
        {
            string strFinal = "", strKey512 = "", strIV512 = "";
            int iLargo = 0;

            strKey512 = Encripta512(strCadena, iLargoKey);
            strIV512 = Encripta512(Fg.Reverse(strCadena), iLargoKey);


            // Asignar las claves con Hash512
            strCadena = strKey512 + strKey + strCadena.Trim() + strIV512;
            iLargo = strCadena.Length + 1;

            string[] strCaracteres = new string[iLargo];

            for (int i = 1; i < iLargo; i++)
            {
                strCaracteres[i] = Fg.Mid(strCadena, i, 1);
                strCaracteres[i] = Fg.Char(Fg.Asc(strCaracteres[i]) + 65);
                strFinal = strFinal + strCaracteres[i];
            }

            return strFinal; 
        }

        public string PasswordDesencriptar(string strCadena)
        {
            string strRegresa = "", strFinal = "";
            int iLargo = 0;

            try
            {
                // Quitar las claves asignadas con Hash512
                strCadena = Fg.Left(strCadena, strCadena.Length - iLargoKey);
                strCadena = Fg.Right(strCadena, strCadena.Length - iLargoKey);

                iLargo = strCadena.Length + 1;

                string[] strCaracteres = new string[iLargo];

                for (int i = 1; i < iLargo; i++)
                {
                    strCaracteres[i] = Fg.Mid(strCadena, i, 1);
                    strCaracteres[i] = Fg.Char(Fg.Asc(strCaracteres[i]) - 65);
                    strFinal = strFinal + strCaracteres[i];
                }
                strRegresa = strFinal.Replace(strKey, " ");
            }
            catch { }

            return strRegresa.Trim(); 
        }
        #endregion Generacion de Password Usuarios


        #region Funciones y procedimientos publicos
        private string EncriptarPassword(string Cadena)
        {
            string sRegresa = "";
            SHA512Managed myHash = new SHA512Managed();
            byte[] bytValue;
            byte[] bytHash;

            // Cadena clave para encriptar
            bytValue = System.Text.Encoding.UTF8.GetBytes(Cadena);
            bytHash = myHash.ComputeHash(bytValue);

            myHash.Clear();
            sRegresa = Convert.ToBase64String(bytHash);

            return sRegresa;
        }

        /// <summary>
        /// Genera el valor MD5 para el archivo especificado.
        /// </summary>
        /// <param name="Archivo">Archivo al cual se le desea calcular el MD5</param>
        /// <returns></returns>
        public string GenerarMD5(string Archivo)
        {
            string sMD5 = "";
            FileStream bufferArchivo;
            byte[] bytesArchivo;
            MD5CryptoServiceProvider MD5Crypto = new MD5CryptoServiceProvider();

            bufferArchivo = File.Open(Archivo, FileMode.Open, FileAccess.Read);
            bytesArchivo = MD5Crypto.ComputeHash(bufferArchivo);
            bufferArchivo.Close();

            sMD5 = BitConverter.ToString(bytesArchivo);
            sMD5 = sMD5.Replace("-", "");

            return sMD5;
        }

        public string Encriptar(string strCadena)
        {
            return Encriptar(strCadena, iLargoKey);
        }

        /// <summary>
        /// Encripta una cadena, implementa Hash512 como método de encriptacion
        /// </summary>
        /// <param name="strCadena">Cadena que se encriptara</param>
        /// <returns></returns>
        public string Encriptar(string strCadena, int LargoClave)
        {
            string strFinal = "", strKey512 = "", strIV512 = "";
            int iLargo = 0;

            strKey512 = Encripta512(strCadena, LargoClave);
            strIV512 = Encripta512(Fg.Reverse(strCadena), LargoClave);


            // Asignar las claves con Hash512
            strCadena = strKey512 + strKey + strCadena.Trim() + strIV512;
            iLargo = strCadena.Length + 1;

            string[] strCaracteres = new string[iLargo];

            for (int i = 1; i < iLargo; i++)
            {
                strCaracteres[i] = Fg.Mid(strCadena, i, 1);
                strCaracteres[i] = Fg.Char(Fg.Asc(strCaracteres[i]) + 32);
                strFinal = strFinal + strCaracteres[i];
            }

            return strFinal;
        }

        public string s(string strCadena)
        {
            string strFinal = "";
            int iLargo = strCadena.Length + 1; ;
            int iDes = strCadena.Length;

            string[] strCaracteres = new string[iLargo];

            for (int i = 1; i < iLargo; i++)
            {
                strCaracteres[i] = Fg.Mid(strCadena, i, 1);
                strCaracteres[i] = Fg.Char(Fg.Asc(strCaracteres[i]) + iDes);
                strFinal = strFinal + strCaracteres[i];
            }

            return strFinal; 
        }

        public string Desencriptar(string strCadena) 
        {
            return Desencriptar(strCadena, iLargoKey);
        }

        /// <summary>
        /// Desencripta una cadena, implementa Hash512 como método de encriptacion
        /// </summary>
        /// <param name="strCadena">Cadena que se desencriptara</param>
        /// <returns></returns>
        public string Desencriptar(string strCadena, int LargoClave)
        {
            string strRegresa = "", strFinal = "";
            int iLargo = 0;

            // Quitar las claves asignadas con Hash512
            strCadena = Fg.Left(strCadena, strCadena.Length - LargoClave);
            strCadena = Fg.Right(strCadena, strCadena.Length - LargoClave);
            
            iLargo = strCadena.Length + 1;

            string[] strCaracteres = new string[iLargo];

            for (int i = 1; i < iLargo; i++)
            {
                strCaracteres[i] = Fg.Mid(strCadena, i, 1);                
                strCaracteres[i] = Fg.Char(Fg.Asc(strCaracteres[i]) - 32);
                strFinal = strFinal + strCaracteres[i];
            }
            strRegresa = strFinal.Replace(strKey, " ");

            return strRegresa.Trim();
        }
        #endregion Funciones y procedimientos publicos
    }
}
