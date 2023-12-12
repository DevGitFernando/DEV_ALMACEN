using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;


namespace DllPedidosClientes
{
    public class clsParametrosClienteUnidad
    {
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;

        string sArbol = "";
        // string sParam = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        DataSet dtsParametros;

        public clsParametrosClienteUnidad(clsDatosConexion DatosConexion, clsDatosApp DatosApp, string IdEstado, string IdFarmacia, string Arbol)
        {
            this.cnn = new clsConexionSQL(DatosConexion);
            this.leer = new clsLeer(ref cnn);
            this.Error = new clsGrabarError(DatosApp, "clsParametrosPtoVta");

            this.sArbol = Arbol;
            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
            PreparaDtsParametros();
        }

        #region Funciones y Procedimientos publicos
        public bool CargarParametros()
        {
            bool bRegresa = true;
            GenerarParametros();

            string sSql = string.Format("Select ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema " +
                " From Net_CFGC_Parametros (NoLock) " +
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' and ArbolModulo = '{2}' ", sIdEstado, sIdFarmacia, sArbol);
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "CargarParametros");
            }
            else
            {
                dtsParametros = leer.DataSetClase;
                while (leer.Leer())
                {
                }
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
        public bool GrabarParametro(string ArbolModulo, string NombreParametro, string Valor)
        {
            return GrabarParametro(ArbolModulo, NombreParametro, Valor, "", false, 1);
        }

        private bool GrabarParametro(string ArbolModulo, string NombreParametro, string Valor, string Descripcion, bool EsDeSistema)
        {
            return GrabarParametro(ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema, 0);
        }

        private bool GrabarParametro(string ArbolModulo, string NombreParametro, string Valor, string Descripcion, bool EsDeSistema, int Actualizar)
        {
            bool bRegresa = true;
            string sSql = string.Format(" Exec spp_Mtto_Net_CFGC_Parametros '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'  ",
                sIdEstado, sIdFarmacia, ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema.ToString(), Actualizar.ToString());
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
            Cliente_SubCliente();
        }

        #region Parametros
        private void Cliente_SubCliente()
        {
            GrabarParametro(sArbol, "IdCliente", "0000", "Especifica el Id Cliente que se usara para la generación de los reportes.", false);
            GrabarParametro(sArbol, "IdSubCliente", "0000", "Especifica el Id Sub-Cliente que se usara para la generación de los reportes.", false);
        }

        ////private void PedidosAutomaticos_Y_Especiales()
        ////{
        ////    GrabarParametro("PFAR", "GeneraPedidosAutomaticos", @"FALSE", "Determina si la Unidad genera Pedidos Automaticos de Producto.", false);
        ////    GrabarParametro("PFAR", "GeneraPedidosEspeciales", @"FALSE", "Determina si la Unidad genera Pedidos Pedidos Especiales de Producto.", false);
        ////}

        private void ClienteSeguroPopular()
        {
            //////GrabarParametro("PFAR", "FechaOperacionSistema", @"2009-07-01", "Determina la Fecha de Operación del Sistema.", true);
            ////GrabarParametro("PFAR", "ClienteSeguroPopular", @"0002", "Determina la Clave del Cliente que se implementa como Cliente Seguro Popular.", true);
            ////GrabarParametro("PFAR", "ValidarInfoClienteSeguroPopular", @"TRUE", "Determina la Clave del Cliente que se implementa como Cliente Seguro Popular.", true);
            ////GrabarParametro("PFAR", "ValidarBeneficioClienteSeguroPopular", @"TRUE", "Determina si se Validará el Beneficio de Seguro Popular.", false); 
        }

        private void RutaDeReportes()
        {
            //GrabarParametro("PFAR", "RutaReportes", @"C:\Reportes\", "Determina ruta donde se encuentran los reportes del Punto de Venta", false);
            //GrabarParametro("PFAR", "RutaReportes", @"D:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES\", "Determina ruta donde se encuentran los reportes del Punto de Venta", false);
            GrabarParametro(sArbol, "RutaReportes", @"", "Determina ruta donde se encuentran los reportes de Cliente Unidad.", false);
            GrabarParametro(sArbol, "EncReportesPrin", "SISTEMA INTEGRAL DE INFORMACIÓN", "Determina el encabezado principal de los reportes de Administración Unidad", false);
            GrabarParametro(sArbol, "EncReportesSec", "ADMINISTRACION UNIDAD", "Determina el encabezado secundario de los reportes de Administración Unidad", false);
        }

        #endregion Parametros
        #endregion Funciones y Procedimientos privados

    }
}
