using SC_SolutionsSystem.FuncionesGenerales;
using System;
using System.Collections.Generic;

using System.Security.Cryptography;
using System.Text;

public class ClsDictionary
{
    private Dictionary<string, Object> dictionary = new Dictionary<string, object>();
    private clsCriptografo crypto = new clsCriptografo();
    private string clave = "SC_SolutionsSystem";

    public ClsDictionary()
    {
    }

    public ClsDictionary(string ClaveEncriptacion)
    {
        clave = ClaveEncriptacion;
    }

    public ClsDictionary(Dictionary<string, Object> d)
    {
        dictionary = d;
    }

    public void Add(string sTKey, string sTValue)
    {
        dictionary.Add(sTKey, sTValue);
    }

    public void Edit(string sTKey, string sTValue)
    {
        dictionary[sTKey] = sTValue;
    }

    public string Search(string sKey)
    {
        string value = "";

        if (dictionary.ContainsKey(sKey) == true)
        {
            value = dictionary[sKey].ToString();
        }

        return value;
    }

    private string DictToString()
    {
        StringBuilder builder = new StringBuilder();
        foreach (KeyValuePair<string, Object> pair in dictionary)
        {
            builder.Append(pair.Key).Append("=").Append(pair.Value).Append('|');
        }
        string result = builder.ToString();
        result = result.TrimEnd('|');
        return result;
    }

    public Dictionary<string, Object> StringToDict(string sDictionary)
    {
        string[] tokens = sDictionary.Split(new char[] { '=', '|' },
        StringSplitOptions.RemoveEmptyEntries);

        string[] items = sDictionary.TrimEnd('|').Split('|');
        foreach (string item in items)
        {
            string[] keyValue = item.Split('=');
            dictionary.Add(keyValue[0], keyValue[1]);
        }

        return dictionary;
    }

   public string Cifrar()
    {
        string cadena = DictToString();
        byte[] llave; //Arreglo donde guardaremos la llave para el cifrado 3DES.

        byte[] arreglo = UTF8Encoding.UTF8.GetBytes(cadena); //Arreglo donde guardaremos la cadena descifrada.

        // Ciframos utilizando el Algoritmo MD5.
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
        md5.Clear();

        //Ciframos utilizando el Algoritmo 3DES.
        TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider
        {
            Key = llave,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };

        ICryptoTransform convertir = tripledes.CreateEncryptor(); // Iniciamos la conversión de la cadena
        byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length); //Arreglo de bytes donde guardaremos la cadena cifrada.
        tripledes.Clear();

        return Convert.ToBase64String(resultado, 0, resultado.Length); // Convertimos la cadena y la regresamos.
    }

    public string Cifrar(string sclave)
    {
        string cadena = DictToString();
        clave = sclave;
        return Cifrar(cadena);
    }
    
    public string Descifrar(string cadena)
    {
        byte[] llave;

        byte[] arreglo = Convert.FromBase64String(cadena); // Arreglo donde guardaremos la cadena descovertida.

        // Ciframos utilizando el Algoritmo MD5.
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
        md5.Clear();

        //Ciframos utilizando el Algoritmo 3DES.
        TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider
        {
            Key = llave,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };

        ICryptoTransform convertir = tripledes.CreateDecryptor();
        byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length);
        tripledes.Clear();

        string cadena_descifrada = UTF8Encoding.UTF8.GetString(resultado); // Obtenemos la cadena

        StringToDict(cadena_descifrada);

        return cadena_descifrada; // Devolvemos la cadena
    }

    public string Descifrar(string cadena, string sclave)
    {
        clave = sclave;

        return Descifrar(cadena);
    }
}