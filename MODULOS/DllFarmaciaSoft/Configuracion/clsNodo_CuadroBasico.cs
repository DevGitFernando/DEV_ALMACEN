using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DllFarmaciaSoft
{
    public class clsNodo_CuadroBasico
    {
        public string Separador = ""; 
        public string IdCliente = "";
        public string IdSubCliente = "";
        public string IdClaveSSA = "";
        public string IdClaveSSA_Relacionada = "";
        public string ClaveSSA = "";
        public string Status = "";

        public int Multiplo = 1;
        public bool AfectaVenta = false;
        public bool AfectaConsigna = false; 
        public clsNodo_CuadroBasico()
        { 
        }
    }
}
