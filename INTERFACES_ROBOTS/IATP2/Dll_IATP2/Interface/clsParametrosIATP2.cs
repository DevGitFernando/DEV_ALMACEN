using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

namespace Dll_IATP2
{
    public class clsParametrosIATP2
    {
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;

        // string sArbol = "";
        // string sParam = "";
        // string sIdEstado = "";
        // string sIdFarmacia = "";
        DataSet dtsParametros;

        public clsParametrosIATP2(clsDatosConexion DatosConexion, clsDatosApp DatosApp)
        {
            this.cnn = new clsConexionSQL(DatosConexion);
            this.leer = new clsLeer(ref cnn);
            this.Error = new clsGrabarError(DatosApp, "clsParametrosIATP2");

            PreparaDtsParametros();
        }

        #region Funciones y Procedimientos publicos 
        public bool CargarParametros()
        {
            bool bRegresa = true;

            if (ExisteATP2())
            {
                GenerarParametros();

                string sSql = string.Format("Select NombreParametro, Valor, Descripcion " +
                    " From IATP2_Net_Parametros (NoLock) ");
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
            }

            return bRegresa;
        }

        public int GetValorInt(string Parametro)
        {
            int iRegresa = 0;
            string sRegresa = "";

            try
            {
                string sDatos = string.Format("NombreParametro = '{0}' ", Parametro);

                DataRow[] dt = dtsParametros.Tables[0].Select(sDatos);
                if (dt.Length > 0)
                {
                    sRegresa = Convert.ToString(dt[0]["Valor"].ToString());
                    sRegresa = sRegresa == "" ? "0" : sRegresa;
                    iRegresa = Convert.ToInt32(sRegresa); 
                }
            }
            catch { }

            return iRegresa; 
        }

        public bool GetValorBool(string Parametro)
        {
            bool bRegresa = false;
            string sRegresa = "";

            try
            {
                string sDatos = string.Format("NombreParametro = '{0}' ", Parametro);

                DataRow[] dt = dtsParametros.Tables[0].Select(sDatos);
                if (dt.Length > 0)
                {
                    sRegresa = Convert.ToString(dt[0]["Valor"].ToString());
                    sRegresa = sRegresa == "" ? "FALSE" : sRegresa;
                    bRegresa = Convert.ToBoolean(sRegresa);
                }
            }
            catch { }

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
        private bool ExisteATP2()
        {
            bool bRegresa = false;
            string sSql = string.Format(" select * From sysobjects (NoLock) Where Name = 'spp_Mtto_IATP2_Net_Parametros' and xType = 'P' ");

            leer.Exec(sSql); 
            bRegresa = leer.Leer(); 

            return bRegresa;
        }

        public bool GrabarParametro(string NombreParametro, string Valor)
        {
            return GrabarParametro(NombreParametro, Valor, "", 0);
        }

        private bool GrabarParametro(string NombreParametro, string Valor, string Descripcion)
        {
            return GrabarParametro(NombreParametro, Valor, Descripcion, 0);
        }

        private bool GrabarParametro(string NombreParametro, string Valor, string Descripcion, int Actualizar )
        {
            bool bRegresa = true;
            string sSql = string.Format(" Exec spp_Mtto_IATP2_Net_Parametros  @NombreParametro = '{0}', @Valor = '{1}', @Descripcion = '{2}', @Actualizar = '{3}'  ",
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
            DirectoriosRemotos();

            IniciarSO(); 
            RutaDeReportes();
            HabilitarLog();
        }

        #region Parametros 
        private void DirectoriosRemotos()
        {
            GrabarParametro("RutaRepositorio_OrdenesAcondicionamiento", @"", "Especifica el directorio compartido en el cual se depositan las Ordenes de Acondionamiento.");
            GrabarParametro("RutaRepositorio_OrdenesAcondicionamiento_Respuesta", @"", "Especifica el directorio compartido en el cual se depositan las respuestas de las Ordenes de Acondionamiento");
        }

        private void IniciarSO()
        {
            GrabarParametro("IniciarSO", @"TRUE", "Determina si la Interface inicia junto con el Sistema Operativo.");
        }

        private void RutaDeReportes()
        {
            GrabarParametro("RutaReportes", @"", "Determina ruta donde se encuentran los reportes de la Interface ATP2.");
        }

        private void HabilitarLog()
        {
            GrabarParametro("HabilitarLog", @"FALSE", "Habilita el log de texto de comunicaciones.");
        }
        #endregion Parametros
        #endregion Funciones y Procedimientos privados

    }
}
