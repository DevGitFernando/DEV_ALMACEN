using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales; 

namespace Dll_IFacturacion.CFDI.CFDFunctionsEx
{
    public class clsInformacionAduanera
    {
        #region Declaracion de Variables
        string sAduana = "";
        DateTime dFecha = DateTime.Now; 
        string sNumero = ""; 
        #endregion Declaracion de Variables

        #region Constructores y Destructor de Clase 
        public clsInformacionAduanera()
        {
        }
        #endregion Constructores y Destructor de Clase 

        #region Propiedades
        public string Aduana
        {
            get { return sAduana; }
            set { sAduana = value; }
        }

        public DateTime Fecha
        {
            get { return dFecha; }
            set { dFecha = value; }
        }

        public string Numero
        {
            get { return sNumero; }
            set { sNumero = value; }
        } 
        #endregion Propiedades
    }
}
