using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_SII_IMediaccess.wsClases
{
    class ResponseSurtidos
    {
        clsDatosConexion datosDeConexion;
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;
        basGenerales Fg = new basGenerales(); 


        public ResponseSurtidos(clsDatosConexion DatosConexion)
        {
            datosDeConexion = DatosConexion;
            cnn = new clsConexionSQL(datosDeConexion);
            leer = new clsLeer(ref cnn);

            Error = new clsGrabarError(datosDeConexion, Dll_SII_IMediaccess.GnDll_SII_IMediaccess.DatosApp, "RecetaElectronica"); 
        }

        #region Funciones y Procedimentos Publicos

        public DataSet ListadoDeSurtido(string Referencia_MA, string Año, string Mes)
        {
            DataSet dtsRegresa = new DataSet();

            string sSql = string.Format("Exec spp_Rpt_ListadoDeSurtido @Referencia_MA = '{0}', @Año = '{1}', @Mes = '{2}' ",
                    Referencia_MA, Año, Mes);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ListadoDeSurtido()");
            }

            return dtsRegresa;
        }

        #endregion Funciones y Procedimentos Publicos
    }
}
