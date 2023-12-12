using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

namespace DllFarmaciaSoft
{
    public class clsParametrosProveedores
    {
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;

        string sIdProveedor = "";
        // string sArbol = "";
        // string sParam = "";
        DataSet dtsParametros;

        public clsParametrosProveedores(clsDatosConexion DatosConexion, clsDatosApp DatosApp, string IdProveedor)
        {
            this.cnn = new clsConexionSQL(DatosConexion);
            this.leer = new clsLeer(ref cnn);
            this.Error = new clsGrabarError(DatosApp, "clsParametrosProveedores");

            this.sIdProveedor = IdProveedor;
            PreparaDtsParametros();
        }

        #region Funciones y Procedimientos publicos 
        public bool CargarParametros()
        {
            bool bRegresa = true;
            leer = new clsLeer(ref cnn); 

            GenerarParametros();

            string sSql = string.Format("Select IdProveedor, NombreParametro, Valor, Descripcion  " + // EsDeSistema
                " From Net_Prov_Parametros (NoLock) ");
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
            return GetValor(sIdProveedor, Parametro);
        }

        public string GetValor(string IdProveedor, string Parametro)
        {
            string sRegresa = "";
            string sDatos = string.Format("IdProveedor = '{0}' and NombreParametro = '{1}' ", IdProveedor, Parametro);

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

            dtParam.Columns.Add("IdProveedor", GetType(TypeCode.String));
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
            GrabarParametro(sIdProveedor, NombreParametro, Valor, Descripcion, EsDeSistema);
        }

        private void GrabarParametro(string IdProveedor, string NombreParametro, string Valor, string Descripcion, bool EsDeSistema)
        {
            string sSql = string.Format(" Exec spp_Mtto_Net_Prov_Parametros '{0}', '{1}', '{2}', '{3}' ",
                IdProveedor, NombreParametro, Valor, Descripcion); // , EsDeSistema.ToString()
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GrabarParametro");
            }
        }

        private void GenerarParametros()
        {
            GrabarParametro("ObligarCapturaLotes", @"TRUE", "Determina si el Proveedor debe capturar los lotes que entregara en el punto al momento de Finalizar la Orden de Compra(Embarque)", false); 
        }

        private void RutaDeReportes()
        {
            GrabarParametro("RutaReportes", @"", "Determina ruta donde se encuentran los reportes del Punto de Venta", false); 
            GrabarParametro("EncReportesPrin", "SISTEMA INTEGRAL DE INFORMACIÓN", "Determina el encabezado principal de los reportes de Oficina Central", false);
            GrabarParametro("EncReportesSec", "OFICINA CENTRAL", "Determina el encabezado secundario de los reportes de Oficina Central", false); 
        }
        #endregion Funciones y Procedimientos privados

    }
}
