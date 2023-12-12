using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_MA_IFacturacion.CFDI.CFDFunctionsEx
{
    public class clsInformacionAdicional
    {
        #region Declaracion de Variables
        string sObservaciones_01 = "";
        string sObservaciones_02 = "";
        string sObservaciones_03 = "";
        string sObservaciones_04 = "";
        string sObservaciones_05 = "";
        string sObservaciones_06 = "";
        string sObservaciones_07 = "";
        string sObservaciones_08 = "";
        string sObservaciones_09 = "";
        string sObservaciones_10 = "";
        basGenerales Fg = new basGenerales(); 

        DataSet dtsInformacionAdicional; 

        #endregion Declaracion de Variables

        #region Constructores y Destructor de Clase 
        public clsInformacionAdicional()
        {
            dtsInformacionAdicional = new DataSet(); 
        }

        public clsInformacionAdicional(DataSet DatosInformacionAdicional)
        {
            dtsInformacionAdicional = DatosInformacionAdicional;
            CargarDatos(); 
        }
        #endregion Constructores y Destructor de Clase

        #region Propiedades 
        public string Observaciones_01
        {
            get { return sObservaciones_01; }
            set { sObservaciones_01 = value; }
        }

        public string Observaciones_02
        {
            get { return sObservaciones_02; }
            set { sObservaciones_02 = value; }
        }

        public string Observaciones_03
        {
            get { return sObservaciones_03; }
            set { sObservaciones_03 = value; }
        }

        public string Observaciones_04
        {
            get { return sObservaciones_04; }
            set { sObservaciones_04 = value; }
        }

        public string Observaciones_05
        {
            get { return sObservaciones_05; }
            set { sObservaciones_05 = value; }
        }

        public string Observaciones_06
        {
            get { return sObservaciones_06; }
            set { sObservaciones_06 = value; }
        }

        public string Observaciones_07
        {
            get { return sObservaciones_07; }
            set { sObservaciones_07 = value; }
        }

        public string Observaciones_08
        {
            get { return sObservaciones_08; }
            set { sObservaciones_08 = value; }
        }

        public string Observaciones_09
        {
            get { return sObservaciones_09; }
            set { sObservaciones_09 = value; }
        }

        public string Observaciones_10
        {
            get { return sObservaciones_10; }
            set { sObservaciones_10 = value; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Privados 
        private void CargarDatos()
        {
            clsLeer leer = new clsLeer();
            clsLeer leerEmisor = new clsLeer();

            leer.DataSetClase = dtsInformacionAdicional;
            leerEmisor.DataTableClase = leer.Tabla("InformacionAdicional");
            if (leerEmisor.Leer())
            {
                sObservaciones_01 = leerEmisor.Campo("Observaciones_01");
                sObservaciones_02 = leerEmisor.Campo("Observaciones_02");
                sObservaciones_03 = leerEmisor.Campo("Observaciones_03");
                sObservaciones_04 = leerEmisor.Campo("Observaciones_04");
                sObservaciones_05 = leerEmisor.Campo("Observaciones_05");
                sObservaciones_06 = leerEmisor.Campo("Observaciones_06");
                sObservaciones_07 = leerEmisor.Campo("Observaciones_07");
                sObservaciones_08 = leerEmisor.Campo("Observaciones_08");
                sObservaciones_09 = leerEmisor.Campo("Observaciones_09");
                sObservaciones_10 = leerEmisor.Campo("Observaciones_10");
            }

        }
        #endregion Funciones y Procedimientos Privados
    }
}
