using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DllFarmaciaSoft;


using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;


namespace DllFarmaciaSoft.Lotes
{
    internal static class SubFarmacias
    {
        #region Declaracion de variables 
        static DataSet pDtsSubFarmacias;
        static basGenerales Fg = new basGenerales();

        static clsConexionSQL pCnn = new clsConexionSQL(General.DatosConexion);
        static clsLeer leer = new clsLeer();
        static clsGrabarError Error;
        static bool bError = false; 

        #endregion Declaracion de variables

        #region Constructor de Clase 
        static SubFarmacias()
        {
            leer = new clsLeer(ref pCnn); 
            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, "SubFarmacias");

            CargarSubFarmacias(); 
        }
        #endregion Constructor de Clase

        #region Funciones y Procedimientos Publicos 
        public static void Iniciar()
        { 
        }

        public static bool EmulaVenta(int IdSubFarmacia)
        {
            bool bRegresa = false;

            bRegresa = EmulaVenta(IdSubFarmacia.ToString()); 

            return bRegresa;
        }

        public static bool EmulaVenta(string IdSubFarmacia)
        {
            bool bRegresa = false;

            if (!bError)
            {
                clsLeer buscar = new clsLeer();
                buscar.DataRowsClase = leer.DataTableClase.Select(string.Format(" IdSubFarmacia = '{0}' ", Fg.PonCeros(IdSubFarmacia, 2)));

                bRegresa = buscar.Leer(); 
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados
        private static void CargarSubFarmacias()
        {
            string sSql = string.Format("Select IdEstado, IdFarmacia, IdSubFarmacia, SubFarmacia, EsConsignacion, EmulaVenta " +
                    " From vw_Farmacias_SubFarmacias (NoLock) " +
                    " Where IdEstado = '{0}' and IdFarmacia = '{1}' and EsConsignacion = 1 and EmulaVenta = 1 ",
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada ); 

            if (!leer.Exec(sSql))
            {
                bError = true; 
                Error.GrabarError(leer, "CargarSubFarmacias()");
            }
            else
            {
                bError = false; 
                pDtsSubFarmacias = leer.DataSetClase; 
            }
        }
        #endregion Funciones y Procedimientos Privados
    }
}
