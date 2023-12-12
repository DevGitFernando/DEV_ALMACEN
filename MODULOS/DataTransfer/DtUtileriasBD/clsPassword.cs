using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

using Microsoft.VisualBasic;

namespace DtUtileriasBD
{
    public enum TipoPassword
    {
        SistemaOperativo = 1,
        BaseDeDatos = 2,
        FTP = 3
    }

    static class clsPassword
    {
        public static string SistemaOperativo(string Servidor, string Usuario)
        {
            string sRegresa = GetCadena(Servidor, Usuario, TipoPassword.SistemaOperativo);
            return GenerarMD5(sRegresa);
        }

        public static string BaseDeDatos(string Servidor, string Usuario)
        {
            string sRegresa = GetCadena(Servidor, Usuario, TipoPassword.BaseDeDatos);
            return GenerarMD5(sRegresa);
        }

        public static string FTP(string Servidor, string Usuario)
        {
            string sRegresa = GetCadena(Servidor, Usuario, TipoPassword.FTP);
            return GenerarMD5(sRegresa);
        }

        private static string GetCadena(string Servidor, string Usuario, TipoPassword Tipo)
        {
            string sRegresa = "";

            if (Tipo == TipoPassword.BaseDeDatos)
            {
                sRegresa = Usuario + "z@z" + Strings.StrReverse(Servidor + Usuario);
            }

            if (Tipo == TipoPassword.SistemaOperativo)
            {
                sRegresa = Servidor + "x@-x@x-@x" + Strings.StrReverse(Usuario + Servidor);
            }

            if (Tipo == TipoPassword.FTP)
            {
                sRegresa = Servidor + "z@-z@z-@z" + Strings.StrReverse(Usuario + Servidor);
            }

            return sRegresa.ToLower();
        }

        private static string GenerarMD5(string Cadena)
        {
            string sMD5 = "";
            byte[] bytesCadena, bytesRegresa;
            MD5CryptoServiceProvider MD5Crypto = new MD5CryptoServiceProvider();

            bytesCadena = System.Text.Encoding.UTF8.GetBytes(Encripta512(Cadena));
            bytesRegresa = MD5Crypto.ComputeHash(bytesCadena);

            sMD5 = BitConverter.ToString(bytesRegresa);
            sMD5 = sMD5.Replace("-", "").ToLower();

            return sMD5;
        }

        private static string Encripta512(string Cadena)
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

    }
}
