using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;

// Controles personalizados
using UpdaterAdminUnidad;

namespace UpdaterAdminUnidad.FuncionesGenerales
{
    /// <summary>
    /// Funciones y procedimientos generales
    /// </summary>
    public class basGenerales
    {
        Form pForma; // = new Form();
        //int iValor = 0;
        //string sFormato = "#,###,###,##0.";

        public basGenerales()
        { 
        }

        public basGenerales(Form Forma)
        {
            pForma = Forma;
        }

        #region Manejo de cadenas
        #region Formato
        private string ValidarDato(string Valor)
        {
            if (Valor == "" || Valor == null)
                Valor = "0";
            return Valor;
        }

        public string Format(string Valor, int Decimales)
        {
            string sFormato = "#,###,###,##0";
            string sValor = "0";

            if (Decimales > 0)
                sFormato = sFormato + "." + Strings.StrDup(Decimales-1, "#").ToString() + "0";

            sValor = "0" + Valor.Replace("$", "").Replace("%", "");

            string sRegresa = Convert.ToDouble(sValor).ToString(sFormato);
            return sRegresa;
        }

        //public string F_Moneda(string Valor, int Decimales)
        //{
        //    string sValor = ValidarDato(Valor).Replace("$", "");
        //    string sRegresa = Strings.FormatCurrency(sValor, Decimales, TriState.True, TriState.False, TriState.True).ToString();
        //    return sRegresa;
        //}

        //public string F_Numero(string Valor, int Decimales)
        //{
        //    string sValor = ValidarDato(Valor).Replace("", "");
        //    string sRegresa = Strings.FormatNumber(sValor, Decimales, TriState.True, TriState.False, TriState.True);
        //    return sRegresa;
        //}

        //public string F_Porcentaje(string Valor, int Decimales)
        //{
        //    string sValor = ValidarDato(Valor).Replace("%", "");
        //    string sRegresa = Strings.FormatPercent(sValor, Decimales, TriState.True, TriState.False, TriState.True);
        //    return sRegresa;
        //}
        #endregion Formato

        public string Encripta512(string Cadena)
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
        /// Regresa una cadena con la longitud especificada incluyendo el parametro recibido.
        /// </summary>
        /// <param name="prtDato">Cadena a acompletar</param>
        /// <param name="prtLargo">Largo de la cadena</param>
        /// <returns></returns>
        public string PonCeros(string prtDato, int prtLargo)
        {
            string sCadena = "";

            try
            {
                sCadena = Strings.StrDup(prtLargo, "0").ToString() + prtDato.Trim();   //"000000000000000000000000000000";
            }
            catch { }
            //string sValor = sCadena + prtDato.Trim();
            string sRegresa = Right(sCadena, prtLargo); 
            return sRegresa;
        }

        public string PonCeros(int prtDato, int prtLargo)
        {
            string sRegresa = PonCeros(Str(prtDato), prtLargo);
            return sRegresa;
        }

        /// <summary>
        /// Devuelve la cantidad de caracteres especificados por la izquierda
        /// </summary>
        /// <param name="prtDato">Cadena a extrer los caracteres</param>
        /// <param name="prtLargo">Cantidad de caracteres a extraer</param>
        /// <returns></returns>
        public string Left(string prtDato, int prtLargo)
        {
            string sRegresa = Strings.Left(prtDato, prtLargo);
            return sRegresa; 
        }

        /// <summary>
        /// Devuelve la cantidad de caracteres especificados por la derecha
        /// </summary>
        /// <param name="prtDato">Cadena a extrer los caracteres</param>
        /// <param name="prtLargo">Cantidad de caracteres a extraer</param>
        /// <returns></returns>
        public string Right(string prtDato, int prtLargo)
        {
            string sRegresa = Strings.Right(prtDato, prtLargo);
            return sRegresa;
        }

        /// <summary>
        /// Devuelve la cantidad de caracteres de una cadena
        /// </summary>
        /// <param name="prtDato">Cadena a extraer los caracteres</param>
        /// <param name="prtPosIni">Posicion inicial para la extraccion</param>
        /// <param name="prtLargo">Cantidad de caracteres a extraer</param>
        /// <returns></returns>
        public string Mid(string prtDato, int prtPosIni, int prtLargo)
        {
            string sRegresa = "";

            try
            {
                sRegresa = Strings.Mid(prtDato, prtPosIni, prtLargo);
            }
            catch { sRegresa = ""; }
            return sRegresa;
        }

        /// <summary>
        /// Devuelve la cantidad de caracteres de una cadena
        /// </summary>
        /// <param name="prtDato">Cadena a extraer los caracteres</param>
        /// <param name="prtPosIni">Posicion inicial para la extraccion</param>
        /// <returns></returns>
        public string Mid(string prtDato, int prtPosIni)
        {
            string sRegresa = Strings.Mid(prtDato, prtPosIni); 
            return sRegresa;
        }

        /// <summary>
        /// Convierte al número correspondiente la cadena de entrada
        /// </summary>
        /// <param name="prtDato">String a convertir a número</param>
        /// <returns></returns>
        public int Val(string prtDato)
        {
            int iRegresa = 0;
            iRegresa = (int)Conversion.Val(prtDato);
            return iRegresa;
        }

        /// <summary>
        /// Convierte al número correspondiente la cadena de entrada
        /// </summary>
        /// <param name="prtDato">String a convertir a número</param>
        /// <returns></returns>
        public double ValD(string prtDato)
        {
            double iRegresa = 0;
            iRegresa = (double)Conversion.Val((object)prtDato);
            return iRegresa;
        }

        /// <summary>
        /// Regresa una cadena al reves de la cadena de entrada
        /// </summary>
        /// <param name="strCadena">Cadena a invertir los caracteres</param>
        /// <returns></returns>
        public string Reverse(string strCadena)
        {
            string sRegresa = "";
            sRegresa = (string)Strings.StrReverse(strCadena);
            return sRegresa;
        }

        /// <summary>
        /// Convierte al string correspondiente el número de entrada
        /// </summary>
        /// <param name="prtDato">Número a convertir a string</param>
        /// <returns></returns>
        public string Str(int prtDato)
        {
            string sRegresa = "";
            sRegresa = (string)Conversion.Str(prtDato);
            return sRegresa;
        }

        public string Str(double prtDato)
        {
            string sRegresa = "";
            sRegresa = (string)Conversion.Str(prtDato);
            return sRegresa;
        }

        /// <summary>
        /// Devuelve el caracter asociado al número de entrada
        /// </summary>
        /// <param name="prtDato">Numero que se desea convetir a Char </param>
        /// <returns></returns>
        public string Char(int prtDato)
        {
            string sRegresa = "";
            sRegresa = Strings.Chr(prtDato).ToString(); 
            return sRegresa;
        } 
        /// <summary>
        /// Devuelve el número correspondiente al caracter de entrada
        /// </summary>
        /// <param name="prtDato">Caracter que se desea conocer su valor numerico</param>
        /// <returns></returns>
        public int Asc(string prtDato)
        {
            int iRegresa = 0;
            try
            {
                iRegresa = Strings.Asc(prtDato);
            }
            catch { iRegresa = 0; }
            return iRegresa;
        }

        public string FechaYMD(string Fecha, string Separador)
        {
            string sRegresa = "";
            string sYear = Right(Fecha, 4);
            string sMes = Mid(Fecha, 4, 2);
            string sDia = Left(Fecha, 2);

            sRegresa = sYear + Separador + sMes + Separador + sDia;

            return sRegresa;
        }

        public string Trim(string Cadena)
        {
            string sRegresa = "";

            try
            {
                sRegresa = Strings.Trim(Cadena);
            }
            catch
            { }

            return sRegresa;
        }

        public string Apostrofo()
        {
            return Strings.Chr(39).ToString();
        }

        public string Comillas()
        {
            return Strings.Chr(34).ToString(); 
        }
        #endregion Manejo de cadenas

        /// <summary>
        /// Centra una forma en la pantalla.
        /// </summary>
        /// <param name="Forma">Objeto Form que se desea centrar.</param>
        public void CentrarForma(Form Forma)
        {
            Forma.BackColor = General.FormaBackColor;
            Forma.StartPosition = FormStartPosition.CenterScreen; 
        }
    }
}
