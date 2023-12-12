using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.VisualBasic;

namespace UpdateCompras
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
