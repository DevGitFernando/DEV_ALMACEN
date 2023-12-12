using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem.FuncionesGenerales; 

namespace DllFarmaciaSoft
{
    public class clsInformacionEmpresa
    {
        string sIdEmpresa = ""; 
        string sNombre = ""; 
        string sNombreCorto = ""; 
        string sEsDeConsignacion = ""; 
        string sRFC = ""; 
        string sEdoCiudad = ""; 
        string sColonia = ""; 
        string sDomicilio = "";             
        string sCodigoPostal = ""; 
        string sStatus = "";

        basGenerales Fg = new basGenerales(); 

        public clsInformacionEmpresa()
        {
        }

        #region Propiedades 
        public string IdEmpresa
        {
            get { return sIdEmpresa; }
            set { sIdEmpresa = Fg.PonCeros(value, 3); }
        }

        public string Nombre
        {
            get { return sNombre; }
            set { sNombre = value.ToUpper(); }
        }

        public string NombreCorto
        {
            get { return sNombreCorto; }
            set { sNombreCorto = value.ToUpper(); }
        }

        public string EsDeConsignacion
        {
            get { return sEsDeConsignacion; }
            set { sEsDeConsignacion = value.ToUpper(); }
        }

        public string RFC
        {
            get { return sRFC; } 
            set 
            { 
                sRFC = value.ToUpper();
                sRFC = sRFC.Replace("-", "").Replace(" ", ""); 
            }
        }

        public string EdoCiudad
        {
            get { return sEdoCiudad; }
            set { sEdoCiudad = value.ToUpper(); }
        }

        public string Colonia
        {
            get { return sColonia; }
            set { sColonia = value.ToUpper(); }
        }

        public string Domicilio
        {
            get { return sDomicilio; }
            set { sDomicilio = value.ToUpper(); }
        }

        public string CodigoPostal
        {
            get { return sCodigoPostal; }
            set { sCodigoPostal = value.ToUpper(); }
        }

        public string Status
        {
            get { return sStatus; }
            set { sStatus = value.ToUpper(); }
        }
        #endregion Propiedades
    }
}
