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
    public class clsCuentaPredial
    {
        #region Declaracion de Variables 
        string sNumero = ""; 
        #endregion Declaracion de Variables

        #region Constructores y Destructor de Clase 
        public clsCuentaPredial()
        {
        }
        #endregion Constructores y Destructor de Clase 

        #region Propiedades 
        public string Numero
        {
            get { return sNumero; }
            set { sNumero = value; }
        } 
        #endregion Propiedades
    }
}
