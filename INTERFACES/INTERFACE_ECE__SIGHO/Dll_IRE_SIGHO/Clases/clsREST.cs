using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dll_IRE_SIGHO.Clases
{
    public enum Method { GET, POST }

    public class clsREST
    {
        #region Funciones

        /*

        ////// Ejemplo de uso
         
        public void EnviarDatos()
        {
            string UrlBase = "http://201.116.48.114:8081/sighoreceta//interfacesigho.php";
            Method metodo = radioButton1.Checked ? Method.GET : Method.POST;   

            Dictionary<string, string> Parametros = new Dictionary<string, string>();
            Parametros.Add("CLUES", txtCLUES.Text.Trim());
            Parametros.Add("FolioDocumento", txtReceta.Text);

            string respuestaServidor = GetResponse(UrlBase, Parametros, metodo);

            txtResultado.Text = respuestaServidor;
        }
         
        */






        /// <summary>
        /// Realiza el envio de parametros a un servicio web utilizando el metodo GET o POST
        /// </summary>
        /// <param name="urlBase">url del servicio</param>
        /// <param name="parameters">pares clave-valor que se enviaran</param>
        /// <param name="method">GET | POST</param>
        /// <returns>devuelve una cadena con la respuesta del servidor, o excepción si no funcionó</returns>
        /// <author>Findemor http://findemor.porExpertos.es</author>
        /// <history>Creado 17/02/2012</history>
        public string GetResponse(string urlBase, Dictionary<string, string> parameters, Method method)
        {
            switch (method)
            {
                case Method.GET:
                    return GetResponse_GET(urlBase, parameters);
                case Method.POST:
                    return GetResponse_POST(urlBase, parameters);
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Concatena los parámetros a una cadena de texto compatible con el API Rest
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>Parametros concatenados en formato URL, no establece el caracter "?" al principio
        /// pero sí los "&" separadores</returns>
        /// <author>Findemor http://findemor.porExpertos.es</author>
        /// <history>Creado 17/02/2012</history>
        private static string ConcatParams(Dictionary<string, string> parameters)
        {
            bool FirstParam = true;
            StringBuilder Parametros = null;

            if (parameters != null)
            {
                Parametros = new StringBuilder();
                foreach (KeyValuePair<string, string> param in parameters)
                {
                    Parametros.Append(FirstParam ? "" : "&");
                    Parametros.Append(param.Key + "=" + System.Web.HttpUtility.UrlEncode(param.Value));
                    FirstParam = false;
                }
            }

            return Parametros == null ? string.Empty : Parametros.ToString();
        }


        /// <summary>
        /// Realiza la peticion utilizando el método GET y devuelve la respuesta del servidor
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <author>Findemor http://findemor.porExpertos.es</author>
        /// <history>Creado 17/02/2012</history>
        public static string GetResponse_GET(string url, Dictionary<string, string> parameters)
        {
            try
            {
                ////Concatenamos los parametros, OJO: antes del primero debe estar el caracter "?"
                string parametrosConcatenados = ConcatParams(parameters);
                string urlConParametros = url + "?" + parametrosConcatenados;

                System.Net.WebRequest wr = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(urlConParametros);
                wr.Method = "GET";

                wr.ContentType = "application/x-www-form-urlencoded";

                System.IO.Stream newStream;

                //// Obtiene la respuesta
                System.Net.WebResponse response = wr.GetResponse();

                //// Stream con el contenido recibido del servidor
                newStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(newStream);

                //// Leemos el contenido
                string responseFromServer = reader.ReadToEnd();

                //// Cerramos los streams
                reader.Close();
                newStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch (System.Web.HttpException ex)
            {
                if (ex.ErrorCode == 404)
                {
                    throw new Exception("Not found remote service " + url);
                }
                else
                {
                    throw ex;
                }
            }
        }


        /// <summary>
        /// Realiza la petición utilizando el método POST y devuelve la respuesta del servidor
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <author>Findemor http://findemor.porExpertos.es</author>
        /// <history>Creado 17/02/2012</history>
        public static string GetResponse_POST(string url, Dictionary<string, string> parameters)
        {
            try
            {
                ////Concatenamos los parametros, OJO: NO se añade el caracter "?"
                string parametrosConcatenados = ConcatParams(parameters);

                System.Net.WebRequest wr = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                wr.Method = "POST";

                wr.ContentType = "application/x-www-form-urlencoded";

                System.IO.Stream newStream;

                ////Codificación del mensaje
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] byte1 = encoding.GetBytes(parametrosConcatenados);
                wr.ContentLength = byte1.Length;

                ////Envio de parametros
                newStream = wr.GetRequestStream();
                newStream.Write(byte1, 0, byte1.Length);

                //// Obtiene la respuesta
                System.Net.WebResponse response = wr.GetResponse();

                //// Stream con el contenido recibido del servidor
                newStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(newStream);

                //// Leemos el contenido
                string responseFromServer = reader.ReadToEnd();

                // Cerramos los streams
                reader.Close();
                newStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch (System.Web.HttpException ex)
            {
                if (ex.ErrorCode == 404)
                {
                    throw new Exception("Not found remote service " + url);
                }
                else
                {
                    throw ex;
                }
            }
        }
        #endregion Funciones 
    }
}
