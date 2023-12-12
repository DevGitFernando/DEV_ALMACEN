using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

namespace Dll_SII_INadro
{
    public class clsParametros_SII_INadro
    {
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;

        string sArbol = "";
        // string sParam = "";
        DataSet dtsParametros;

        public clsParametros_SII_INadro(clsDatosConexion DatosConexion, clsDatosApp DatosApp, string Arbol)
        {
            this.cnn = new clsConexionSQL(DatosConexion);
            this.leer = new clsLeer(ref cnn);
            this.Error = new clsGrabarError(DatosApp, "clsParametros_SII_INadro");

            this.sArbol = Arbol;
            PreparaDtsParametros();
        } 

        #region Funciones y Procedimientos publicos 
        public bool CargarParametros()
        {
            bool bRegresa = true;
            leer = new clsLeer(ref cnn); 

            GenerarParametros();

            string sSql = string.Format("Select ArbolModulo, NombreParametro, Valor, Descripcion  " + // EsDeSistema
                " From Net_CFGS_Parametros (NoLock) " );
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "CargarParametros");
            }
            else
            {
                dtsParametros = leer.DataSetClase;
            }

            return bRegresa;
        }

        public string GetValor(string Parametro)
        {
            return GetValor(sArbol, Parametro);
        }

        public bool GetValorBool(string Parametro)
        {
            bool bRegresa = false;
            string sValor = GetValor(sArbol, Parametro);

            if (sValor != "")
            {
                try
                {
                    bRegresa = Convert.ToBoolean(sValor);
                }
                catch { }
            }

            return bRegresa;
        }

        public int GetValorInt(string Parametro)
        {
            int iRegresa = 0;
            string sValor = GetValor(sArbol, Parametro);

            if (sValor != "")
            {
                try
                {
                    iRegresa = Convert.ToInt32(sValor);
                }
                catch { }
            }

            return iRegresa;
        }

        public string GetValor(string Arbol, string Parametro)
        {
            string sRegresa = "";
            string sDatos = string.Format("ArbolModulo = '{0}' and NombreParametro = '{1}' ", Arbol, Parametro);

            try
            {
                DataRow[] dt = dtsParametros.Tables[0].Select(sDatos);
                if (dt.Length > 0)
                {
                    sRegresa = Convert.ToString(dt[0]["Valor"].ToString());
                }

                //leer.DataSetClase = dtsParametros;
                //while (leer.Leer())
                //{
                //}
            }
            catch { }
            return sRegresa;
        } 
        #endregion Funciones y Procedimientos publicos
        
        #region Funciones y Procedimientos
        private Type GetType(TypeCode TipoDato)
        {
            return Type.GetType("System." + TipoDato.ToString());
        }

        private void PreparaDtsParametros()
        {
            dtsParametros = new DataSet();
            DataTable dtParam = new DataTable("Parametros");

            dtParam.Columns.Add("ArbolModulo", GetType(TypeCode.String));
            dtParam.Columns.Add("NombreParametro", GetType(TypeCode.DateTime));
            dtParam.Columns.Add("Valor", GetType(TypeCode.DateTime));

            dtParam.Columns.Add("Descripcion", GetType(TypeCode.String));
            dtParam.Columns.Add("EsDeSistema", GetType(TypeCode.Int32));
            dtsParametros.Tables.Add(dtParam);
        }
        #endregion Funciones y Procedimientos 

        #region Funciones y Procedimientos privados 
        private void GrabarParametro(string NombreParametro, string Valor, string Descripcion, bool EsDeSistema)
        {
            GrabarParametro(sArbol, NombreParametro, Valor, Descripcion, EsDeSistema);
        }

        private void GrabarParametro(string ArbolModulo, string NombreParametro, string Valor, string Descripcion, bool EsDeSistema)
        {
            string sSql = string.Format(" Exec spp_Mtto_Net_CFGS_Parametros '{0}', '{1}', '{2}', '{3}' ",
                ArbolModulo, NombreParametro, Valor, Descripcion); // , EsDeSistema.ToString()
            if (!leer.Exec(sSql)) 
            {
                Error.GrabarError(leer, "GrabarParametro");
            }
        }

        private void GenerarParametros()
        {
            RutaDeReportes();
            PlatillasDeRemisiones(); 
        }

        #region Parametros 
        private void RutaDeReportes()
        {
            GrabarParametro("RutaReportes", @"", "Determina ruta donde se encuentran los reportes del Punto de Venta", false); 
        }

        private void PlatillasDeRemisiones()
        {
            GrabarParametro("RPT_Remisiones_Recetas", @"INT_ND_Remisiones_Unidad_Recetas", "Determina el nombre de la plantilla para impresión de Remisiones de Recetas", false);
            GrabarParametro("RPT_Remisiones_Colectivos", @"INT_ND_Remisiones_Unidad_Colectivos", "Determina el nombre de la plantilla para impresión de Remisiones de Colectivos", false);

            GrabarParametro("RPT_Remisiones_Recetas_Consolidado", @"INT_ND_Remisiones_Unidad_Recetas_Consolidado", "Determina el nombre de la plantilla para impresión de Remisiones de Recetas", false);
            GrabarParametro("RPT_Remisiones_Colectivos_Consolidado", @"INT_ND_Remisiones_Unidad_Colectivos_Consolidado", "Determina el nombre de la plantilla para impresión de Remisiones de Colectivos", false);
        }
        #endregion Parametros         
        #endregion Funciones y Procedimientos privados

    }
}
