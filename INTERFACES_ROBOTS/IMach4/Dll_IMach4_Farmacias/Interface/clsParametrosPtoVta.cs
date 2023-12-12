using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

namespace Dll_IMach4
{
    public class clsParametrosIMach
    {
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;

        string sArbol = "";
        string sParam = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        DataSet dtsParametros;

        public clsParametrosIMach(clsDatosConexion DatosConexion, clsDatosApp DatosApp)
        {
            this.cnn = new clsConexionSQL(DatosConexion);
            this.leer = new clsLeer(ref cnn);
            this.Error = new clsGrabarError(DatosApp, "clsParametrosIMach");

            PreparaDtsParametros();
        }

        #region Funciones y Procedimientos publicos 
        public bool CargarParametros()
        {
            bool bRegresa = true;
            GenerarParametros();

            string sSql = string.Format("Select NombreParametro, Valor, Descripcion " +
                " From IMach_Net_Parametros (NoLock) ");
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "CargarParametros");
            }
            else
            {
                dtsParametros = leer.DataSetClase;
                //while (leer.Leer())
                //{
                //}
            }

            return bRegresa;
        }

        public string GetValor(string Parametro)
        {
            string sRegresa = "";
            string sDatos = string.Format("NombreParametro = '{0}' ", Parametro);

            DataRow[] dt = dtsParametros.Tables[0].Select(sDatos);
            if (dt.Length > 0)
            {
                sRegresa = Convert.ToString(dt[0]["Valor"].ToString());
            }

            leer.DataSetClase = dtsParametros;
            //while (leer.Leer())
            //{ 
            //}

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

            // dtParam.Columns.Add("ArbolModulo", GetType(TypeCode.String));
            dtParam.Columns.Add("NombreParametro", GetType(TypeCode.DateTime));
            dtParam.Columns.Add("Valor", GetType(TypeCode.DateTime));

            dtParam.Columns.Add("Descripcion", GetType(TypeCode.String));
            // dtParam.Columns.Add("EsDeSistema", GetType(TypeCode.Int32));
            dtsParametros.Tables.Add(dtParam);
        }
        #endregion Funciones y Procedimientos 

        #region Funciones y Procedimientos privados 
        public bool GrabarParametro(string NombreParametro, string Valor)
        {
            return GrabarParametro(NombreParametro, Valor, "", 1);
        }

        private bool GrabarParametro(string NombreParametro, string Valor, string Descripcion)
        {
            return GrabarParametro(NombreParametro, Valor, Descripcion, 0);
        }

        private bool GrabarParametro(string NombreParametro, string Valor, string Descripcion, int Actualizar )
        {
            bool bRegresa = true;
            string sSql = string.Format(" Exec spp_Mtto_IMach_Net_Parametros '{0}', '{1}', '{2}', '{3}' ",
                NombreParametro, Valor, Descripcion, Actualizar.ToString());
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GrabarParametro");
                bRegresa = false;
            }
            return bRegresa;
        }

        private void GenerarParametros()
        {
            RutaDeReportes();
        }

        #region Parametros 
        private void RutaDeReportes()
        {
            GrabarParametro("RutaReportes", @"", "Determina ruta donde se encuentran los reportes del Punto de Venta.");
        }
        #endregion Parametros
        #endregion Funciones y Procedimientos privados

    }
}
