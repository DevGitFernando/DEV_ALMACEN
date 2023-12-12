using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Dll_ISIADISSEP.wsClases
{


    public class ResponseRecetaElectronicaItem
    {
        string sCampo = "";
        string sValor = "";

        public ResponseRecetaElectronicaItem(string Campo, string Valor)
        {
            sCampo = Campo;
            sValor = Valor;
        }

        public string Campo
        {
            get { return sCampo; }
            set { sCampo = value; }
        }

        public string Valor
        {
            get { return sValor; }
            set { sValor = value; }
        }
    }

    public class ResponseRecetaElectronica
    {
        int iEstatus = 0;
        string sError = "";
        List<ResponseRecetaElectronicaItem> itemsRespuesta = new List<ResponseRecetaElectronicaItem>();

        public ResponseRecetaElectronica()
        {
        }

        #region Propiedades Publicas
        public int Estatus
        {
            get { return iEstatus; }
            set { iEstatus = value; }
        }

        public string Error
        {
            get { return sError; }
            set { sError = value; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        public void AddItemRespuesta(string Campo, string Valor)
        {
            itemsRespuesta.Add(new ResponseRecetaElectronicaItem(Campo, Valor));
        }

        public void ResetItemsRespuesta()
        {
            itemsRespuesta = new List<ResponseRecetaElectronicaItem>();
        }

        public string GetString()
        {
            string sRegresa = "";

            if (itemsRespuesta != null)
            {
                foreach (ResponseRecetaElectronicaItem item in itemsRespuesta)
                {
                    sRegresa += string.Format("<{0}>{1}</{0}>", item.Campo, item.Valor);
                }
            }

            sRegresa += string.Format("<{0}>{1}</{0}>", "Estatus", iEstatus);
            sRegresa += string.Format("<{0}>{1}</{0}>", "Mensaje", sError);

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Publicos 
    }
}
