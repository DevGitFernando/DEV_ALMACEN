using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.VisualBasic;

using UpdateAlmacen.FuncionesGenerales;

namespace UpdateAlmacen
{
    //public class Argumento
    //{
    //    public string Arg = "";
    //    public string Valor = "";

    //    public Argumento()
    //    {
    //    }

    //    public Argumento(string Arg, string Valor)
    //    {
    //        Arg = Arg;
    //        Valor = Valor;
    //    }
    //}

    public class Argumentos
    {
        //ArrayList listArgumentos;
        Dictionary<string, string> ListaArg = new Dictionary<string, string>();
        bool bArgumentosValidos = false;
        int iArgumentos = 0; 
        basGenerales Fg = new basGenerales(); 

        public Argumentos(string[] args)
        {
            ObtenerParametros(args);
        }

        public bool ArgumentosValidos
        {
            get { return bArgumentosValidos; }
        }

        #region Funciones y Procedimientos Privados 
        private void ObtenerParametros(string[] args)
        {
            // Lista = new Dictionary<string, string>();
            string sArg = "";
            string sValor = "";

            iArgumentos = args.Length > 0 ? args.Length : 1; 
            
            try
            {
                for (int i = 0; i <= args.Length - 1; i++)
                {
                    // Argumento Param = new Argumento();
                    sArg = Strings.Left(args[i].ToString().Trim(), 1);
                    sValor = Strings.Mid(args[i].ToString().Trim(), 2);
                    ListaArg.Add(sArg, sValor); 
                } 
            }
            catch
            {
            }

            bArgumentosValidos = ListaArg.Count >= 2 ? true : false; 
        }
        #endregion Funciones y Procedimientos Privados

        #region Funciones y Procedimientos Publicos 
        public string []GetParametros()
        {
            string[] sRegresa = new string[iArgumentos+1];
            int iArg = -1; 
            string sArg = "";

            foreach (KeyValuePair<string, string> argumento in ListaArg)
            {
                if (argumento.Key != "a")
                {
                    iArg++;
                    sArg = argumento.Key + argumento.Value;
                    sRegresa[iArg] = sArg; 
                }
            }

            // Forzar el parametro EsAlmacen 
            iArg++;
            sArg = "aS";
            sRegresa[iArg] = sArg;
            //sRegresa += string.Format("{0} ", sArg);

            return sRegresa; 
        }

        public string GetParametrosLista()
        {
            string sRegresa = "";
            string sArg = "";

            foreach (KeyValuePair<string, string> argumento in ListaArg)
            {
                if (argumento.Key != "a")
                {
                    sArg = argumento.Key + argumento.Value;
                    sRegresa += string.Format("{0} ", sArg);
                }
            }

            // Forzar el parametro EsAlmacen 
            sArg = "aS";
            sRegresa += string.Format("{0} ", sArg);

            return sRegresa;
        }            
        public string GetValor(string Argumento)
        {
            string sRegresa = "";

            if ( ListaArg.ContainsKey(Argumento))
            {
                try
                {
                    sRegresa = (string)ListaArg[Argumento]; 
                }
                catch { }
            }

            return sRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos

    }
}
