using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Microsoft.VisualBasic;

namespace DllFarmaciaSoft.Procesos
{
    public enum TipoCodigoEAN
    {
        EAN_Invalido = -1,
        Ninguno = 0,
        EAN_8 = 1,
        EAN_13 = 2
    }

    public class clsCodigoEAN
    {
        #region Declaracion de variables 
        string sCodigoEAN = "";
        bool bMostrarMsjInvalido = true;
        string sMsjInvalido = "Codigo EAN inválido";
        TipoCodigoEAN tpEAN = TipoCodigoEAN.Ninguno;

        #endregion Declaracion de variables

        #region Contructor y Destructor de la Clase
        public clsCodigoEAN()
        { 
        }

        public clsCodigoEAN(string CodigoEAN)
        {
            sCodigoEAN = CodigoEAN;
        }

        public clsCodigoEAN(string CodigoEAN, string MensajeInvalido)
        {
            sCodigoEAN = CodigoEAN;
            sMsjInvalido = MensajeInvalido;
        }

        public clsCodigoEAN(string CodigoEAN, bool MostrarMsjEanInvalido, string MensajeInvalido)
        {
            sCodigoEAN = CodigoEAN;
            sMsjInvalido = MensajeInvalido;
            bMostrarMsjInvalido = MostrarMsjEanInvalido;
        }

        ~clsCodigoEAN()
        { 
        }
        #endregion Contructor y Destructor de la Clase

        #region Propiedades 
        public string CodigoEAN
        {
            get { return sCodigoEAN; }
            set { sCodigoEAN = value; }
        }

        public bool MostrarMsjEanInvalido
        {
            get { return bMostrarMsjInvalido; }
            set { bMostrarMsjInvalido = value; }
        }

        public string MensajeEanInvalido 
        {
            get { return sMsjInvalido; }
            set { sMsjInvalido = value; }
        }

        public TipoCodigoEAN Tipo
        {
            get { return tpEAN; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public bool EsValido()
        {
            return EsValido(sCodigoEAN);
        }

        public bool EsValido(string CodigoEAN)
        {
            bool bRegresa = true;
            CodigoEAN = Formatear(CodigoEAN);

            /////////// Jesús Díaz 2K120827.1718 
            //////if (1 == 0 && ValidarLongitud(CodigoEAN))
            //////{
            //////    int pares = 0;
            //////    int impares = 0;

            //////    //Recorrer toda la cadena excluyendo el último lugar
            //////    for (int i = 0; i <= (CodigoEAN.Length - 2); i++)
            //////    {
            //////        if (i % 2 == 0) //Si lugar impar (empezamos por 0)
            //////        {
            //////            impares += int.Parse(CodigoEAN.Substring(i, 1));
            //////        }
            //////        else
            //////        {
            //////            pares += int.Parse(CodigoEAN.Substring(i, 1));
            //////        }
            //////    }

            //////    if (tpEAN == TipoCodigoEAN.EAN_13)
            //////    {
            //////        pares *= 3;
            //////    }
            //////    else if (tpEAN == TipoCodigoEAN.EAN_8)
            //////    {
            //////        impares *= 3;
            //////    }

            //////    int checksum = pares + impares;
            //////    int digitoControl = 10 - (checksum % 10);

            //////    //Si el digito de control es 10, entendemos 0
            //////    if (digitoControl == 10)
            //////    {
            //////        digitoControl = 0;
            //////    }

            //////    //Comprobar que el digito de control obtenido y el
            //////    //de la cadena ean sean el mismo.
            //////    bRegresa = ( digitoControl == int.Parse(CodigoEAN.Substring(CodigoEAN.Length - 1, 1)));
            //////}

            //////if (!bRegresa)
            //////{
            //////    if (bMostrarMsjInvalido)
            //////    {
            //////        MessageBox.Show(sMsjInvalido, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //////    }
            //////}

            return bRegresa;
        }

        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private string Formatear(string CodigoEAN)
        {
            string sCadena = CodigoEAN.Trim();
            string sRegresa = CodigoEAN.Trim();
            int iLargo = 0;

            if (CodigoEAN.Length >= 8 && CodigoEAN.Length <= 13)
            {
                iLargo = 13;
            }
            else if (CodigoEAN.Length >= 7 && CodigoEAN.Length <= 8) 
            {
                iLargo = 8;
            }

            try
            {
                sCadena = Strings.StrDup(iLargo, "0").ToString() + sCadena.Trim();   //"000000000000000000000000000000";
            }
            catch { }
            sRegresa = Strings.Right(sCadena, iLargo);

            return sRegresa;
        }
        
        private bool SoloDigitos(string Dato)
        {
            bool bRegresa = false;

            for (int i = 0; i < Dato.Length; i++)
            {
                bRegresa = true;
                if (!Char.IsDigit(Strings.Chr( Strings.AscW(Dato.Substring(i,1)) )))
                {
                    bRegresa = false;
                    break;
                }
            }

            return bRegresa;
        }

        private bool ValidarLongitud(string Dato)
        {
            bool bRegresa = true;

            if (!SoloDigitos(Dato))
            {
                bRegresa = false;
                tpEAN = TipoCodigoEAN.EAN_Invalido;
            }
            else
            {
                if (Dato.Length == 8)
                {
                    tpEAN = TipoCodigoEAN.EAN_8;
                }
                else if (Dato.Length == 13)
                {
                    tpEAN = TipoCodigoEAN.EAN_13;
                }
                else
                {
                    bRegresa = false;
                    tpEAN = TipoCodigoEAN.EAN_Invalido;
                }
            }


            return bRegresa;
        }
        #endregion Funciones y Procedimientos Privados

    }
}
