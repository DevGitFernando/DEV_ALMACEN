using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

namespace DllPedidosClientes
{
    public class clsParametrosClienteRegional
    {
        clsConexionSQL cnn;
        clsLeer leer;
        clsLeerWeb leerWeb;
        clsGrabarError Error;

        string sArbol = "";
        // string sParam = "";
        DataSet dtsParametros;

        public clsParametrosClienteRegional(clsDatosConexion DatosConexion, clsDatosApp DatosApp, string Arbol)
        {
            this.cnn = new clsConexionSQL(DatosConexion);
            this.leer = new clsLeer(ref cnn);
            this.Error = new clsGrabarError(DatosApp, "clsParametrosClienteRegional");

            this.sArbol = Arbol;
            PreparaDtsParametros();
        }

        public clsParametrosClienteRegional(string Url, string ArchivoIni, clsDatosApp DatosApp, string Arbol)
        {
            ////this.cnn = new clsConexionSQL(DatosConexion);
            ////this.leer = new clsLeer(ref cnn);
            this.Error = new clsGrabarError(DatosApp, "clsParametrosClienteRegional");

            this.sArbol = Arbol;

            clsDatosCliente datosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, "clsParametrosClienteRegional", "clsParametrosClienteRegional");
            leerWeb = new clsLeerWeb(Url, ArchivoIni, datosCliente);
            PreparaDtsParametros();
        }


        #region Funciones y Procedimientos publicos
        public bool CargarParametros()
        {
            return CargarParametros(false);
        }

        public bool CargarParametros(bool GenerarListaParametros)
        {
            bool bRegresa = true;

            if (GenerarListaParametros)
            {
                GenerarParametros();
            }

            string sSql = string.Format("Select ArbolModulo, NombreParametro, Valor, Descripcion  " + // EsDeSistema
                " From Net_CFGS_Parametros (NoLock) " +
                " Where ArbolModulo =  '{0}' ", sArbol);
            if (!leerWeb.Exec(sSql))
            {
                bRegresa = false;
                //// Error.GrabarError(leerWeb, "CargarParametros");
            }
            else
            {
                dtsParametros = leerWeb.DataSetClase;
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

            DataRow[] dt = dtsParametros.Tables[0].Select(sDatos);
            if (dt.Length > 0)
            {
                sRegresa = Convert.ToString(dt[0]["Valor"].ToString());
            }

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
            if (!leerWeb.Exec(sSql))
            {
                //// Error.GrabarError(leer, "GrabarParametro");
            }
        }

        private void GenerarParametros()
        {
            RutaDeReportes();
            TipoConexion();
            Surtimiento();
            Cliente_SubCliente();
        }

        private void Cliente_SubCliente()
        {
            GrabarParametro("IdCliente", "0000", "Especifica el Id Cliente que se usara para la generación de los reportes.", false);
            GrabarParametro("IdSubCliente", "0000", "Especifica el Id Sub-Cliente que se usara para la generación de los reportes.", false);
        }

        private void RutaDeReportes()
        {
            GrabarParametro("RutaReportes", @"", "Determina ruta donde se encuentran los reportes para los Clientes Regionales.", false);
            GrabarParametro("EncReportesPrin", "SISTEMA INTEGRAL DE INFORMACIÓN", "Determina el encabezado principal de los reportes de Oficina Central", false);
            GrabarParametro("EncReportesSec", "ADMINISTRACION REGIONAL", "Determina el encabezado secundario de los reportes de Oficina Central", false);
        }

        private void TipoConexion()
        {
            GrabarParametro("TipoConexion", "2", "Determina el Tipo de Conexion de los reportes de Oficina Central", false);
        }

        private void Surtimiento()
        {
            GrabarParametro("MostrarUltimoSurtimiento", "False", "Determina si los reportes de surtimiento muestran el ultimo dato " +
                " almacenado en caso de no encontrar información en el periodo solicitado.", false);
        }

        #endregion Funciones y Procedimientos privados

    }
}
