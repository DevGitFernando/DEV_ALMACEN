using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security;

using Microsoft.VisualBasic;

namespace Dll_IGPI
{
    public class Argumento
    {
        private string sArgumento = "";
        private string sValor = "";
        private bool bEsValido = false; 

        public Argumento()
        { 
        }

        public Argumento(string Nombre, string Valor)
        {
            sArgumento = Nombre;
            sValor = Valor;
        }

        public Argumento(string Nombre, string Valor, bool EsValido)
        {
            sArgumento = Nombre;
            sValor = Valor;
            bEsValido = EsValido; 
        }

        public string Nombre
        {
            get { return sArgumento; }
            set { sArgumento = value; }
        }

        public string Valor
        {
            get { return sValor; }
            set { sValor = value; }
        }

        public bool EsValido
        {
            get { return bEsValido; }
        }
    }

    public static class ArgumentosDeInicio
    {
        private static Dictionary<string, Argumento> pListaArgumentos = new Dictionary<string, Argumento>();

        static ArgumentosDeInicio()
        {
        }

        //static clsArgumentos(string[] args)
        //{
        //    ObtenerParametros(args);
        //}

        public static Argumento GetArgumento(string Parametro)
        {
            Argumento myArg = new Argumento();
            Argumento myArgX = new Argumento();

            if (pListaArgumentos.ContainsKey(Parametro))
            {
                myArg = pListaArgumentos[Parametro];
                myArgX = new Argumento(myArg.Nombre, myArg.Valor, true); 
            }

            return myArgX; 
        }

        #region Funciones y Procedimientos Publicos 
        public static void ObtenerParametros(string[] args)
        {
            pListaArgumentos = new Dictionary<string, Argumento>();

            for (int i = 0; i <= args.Length - 1; i++)
            {
                Argumento Param = new Argumento();
                Param.Nombre = Strings.Left(args[i].ToString().Trim(), 1).ToUpper();
                Param.Valor = Strings.Mid(args[i].ToString().Trim(), 2);
                pListaArgumentos.Add(Param.Nombre, Param);
            }
        }
        #endregion Funciones y Procedimientos Publicos
    } 
}
