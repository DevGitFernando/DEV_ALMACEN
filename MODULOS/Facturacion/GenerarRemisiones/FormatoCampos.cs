using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using SC_SolutionsSystem.FuncionesGenerales;

namespace Facturacion.GenerarRemisiones
{
    ////class FormatoCampos
    ////{
    ////    static basGenerales Fg = new basGenerales();

    ////    public static string Formatear_QuitarAsterisco(string Cadena)
    ////    {
    ////        string sRegresa = "";

    ////        sRegresa = Cadena.Replace("*", "");

    ////        return sRegresa;
    ////    }

    ////    public static string Formatear_QuitarPipes(string Cadena)
    ////    {
    ////        string sRegresa = Cadena;

    ////        if (Cadena.Contains("|"))
    ////        {
    ////            sRegresa = Cadena.Replace("|", "");
    ////        }

    ////        return sRegresa;
    ////    }

    ////    public static string Formatear_QuitarCaracteres(string Cadena)
    ////    {
    ////        string sRegresa = Cadena;

    ////        string consignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
    ////        string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";

    ////        try
    ////        {
    ////            for (int i = 0; i <= consignos.Length - 1; i++)
    ////            {
    ////                sRegresa = sRegresa.Replace(consignos[i], sinsignos[i]);
    ////            }
    ////        }
    ////        catch
    ////        {
    ////            sRegresa = Cadena;
    ////        }

    ////        sRegresa = Formatear_QuitarPipes(sRegresa);

    ////        return sRegresa;
    ////    }

    ////    public static string Formato_Digitos_Izquierda(string Valor, int Caracteres, string Caracter)
    ////    {
    ////        string sRegresa = "";

    ////        sRegresa = Fg.PonFormato("", Caracter, Caracteres) + Valor;
    ////        sRegresa = Fg.Right(sRegresa, Caracteres);

    ////        return sRegresa;
    ////    }

    ////    public static string Formato_Digitos_Derecha(string Valor, int Caracteres, string Caracter)
    ////    {
    ////        string sRegresa = Valor;

    ////        sRegresa = sRegresa + Fg.PonFormato("", Caracter, Caracteres);
    ////        sRegresa = Fg.Left(sRegresa, Caracteres);

    ////        return sRegresa;
    ////    }

    ////    public static string Formato_Caracter_Derecha(string Valor, int Caracteres, string Caracter)
    ////    {
    ////        string sRegresa = "";

    ////        sRegresa = Formatear_QuitarCaracteres(Valor);

    ////        ////if (Caracter == " " ) 
    ////        ////{
    ////        ////    Caracter = ""; 
    ////        ////}

    ////        sRegresa = sRegresa + Fg.PonFormato("", Caracter, Caracteres);
    ////        sRegresa = Fg.Left(sRegresa, Caracteres);

    ////        return sRegresa;
    ////    }

    ////    public static string Formato_Caracter_Izquierda(string Valor, int Caracteres, string Caracter)
    ////    {
    ////        string sRegresa = "";

    ////        sRegresa = Formatear_QuitarCaracteres(Valor);

    ////        sRegresa = Fg.PonFormato("", Caracter, Caracteres) + sRegresa;
    ////        sRegresa = Fg.Right(sRegresa, Caracteres);

    ////        return sRegresa;
    ////    }

    ////    public static string Formatear_Nombre(string Cadena)
    ////    {
    ////        string sRegresa = Cadena;

    ////        string consignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
    ////        string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";

    ////        sRegresa = sRegresa.Replace(Fg.Comillas(), "");
    ////        sRegresa = sRegresa.Replace("|", "");
    ////        sRegresa = sRegresa.Replace("/", "");
    ////        sRegresa = sRegresa.Replace("&", "");
    ////        sRegresa = sRegresa.Replace("%", "");
    ////        sRegresa = sRegresa.Replace("$", "");
    ////        sRegresa = sRegresa.Replace("#", "");
    ////        sRegresa = sRegresa.Replace("'", "");
    ////        sRegresa = sRegresa.Replace("{", "");
    ////        sRegresa = sRegresa.Replace("}", "");


    ////        try
    ////        {
    ////            for (int i = 0; i <= consignos.Length - 1; i++)
    ////            {
    ////                sRegresa = sRegresa.Replace(consignos[i], sinsignos[i]);
    ////            }
    ////        }
    ////        catch
    ////        {
    ////            sRegresa = Cadena;
    ////        }

    ////        return sRegresa;
    ////    }
    ////}
}
