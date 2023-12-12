using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

using DllProveedores; 
using DllProveedores.wsProveedores; 


namespace DllProveedores
{
    public class clsParametrosProveedores
    {
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;

        wsProveedores.wsCnnProveedores conexionWeb;
        clsLeerWeb leerWeb; 

        string sUrl = ""; 
        string sArbol = "";
        string sParam = "";
        DataSet dtsParametros;

        public clsParametrosProveedores(string Url, clsDatosApp DatosApp, string Arbol)
        {
            //this.cnn = new clsConexionSQL(DatosConexion);
            //this.leer = new clsLeer(ref cnn);
            //this.Error = new clsGrabarError(DatosApp, "clsParametrosPtoVta");

            sUrl = Url; 
            conexionWeb = new DllProveedores.wsProveedores.wsCnnProveedores();
            conexionWeb.Url = Url;

            leerWeb = new clsLeerWeb(Url, GnProveedores.DatosDelCliente); 

            this.sArbol = Arbol;
            PreparaDtsParametros();
        }


        ////public clsParametrosProveedores(clsDatosConexion DatosConexion, clsDatosApp DatosApp, string Arbol)
        ////{
        ////    this.cnn = new clsConexionSQL(DatosConexion);
        ////    this.leer = new clsLeer(ref cnn);
        ////    this.Error = new clsGrabarError(DatosApp, "clsParametrosPtoVta");

        ////    this.sArbol = Arbol;
        ////    PreparaDtsParametros();
        ////}

        #region Funciones y Procedimientos publicos 
        public bool CargarParametros()
        {
            bool bRegresa = true;
            GenerarParametros();

            string sSql = string.Format("Select ArbolModulo, NombreParametro, Valor, Descripcion  " + // EsDeSistema
                " From Net_CFGS_Parametros (NoLock) Where ArbolModulo = '{0}' ", sArbol);
            if (!leerWeb.Exec(sSql))
            {
                bRegresa = false;
                // Error.GrabarError(leer, "CargarParametros");
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
                //Error.GrabarError(leer, "GrabarParametro");
            }
        }

        private void GenerarParametros()
        {
            // ClientePublicoGeneral();
            RutaDeReportes();
            DiasPlazoMaximoEntregaPedido(); 
            // ClasificacionSSA();
            // FamiliaPrincipal();
        }

        private void RutaDeReportes()
        {
            GrabarParametro("RutaReportes", @"", "Determina ruta donde se encuentran los reportes del Punto de Venta", false); 
            GrabarParametro("EncReportesPrin", "SISTEMA INTEGRAL INTERMED", "Determina el encabezado principal de los reportes de Oficina Central", false);
            GrabarParametro("EncReportesSec", "MÓDULO DE PROVEEDORES", "Determina el encabezado secundario de los reportes de Oficina Central", false); 
        }

        private void DiasPlazoMaximoEntregaPedido()
        {
            GrabarParametro("DiasPlazoMaximoEntregaPedido", @"20", "Determina la Fecha mayor para la Promesa de Entrega de Pedidos por parte de Proveedores.", false); 
        }

        private void ClientePublicoGeneral()
        {
            GrabarParametro("CtePubGeneral", "0001", "Determina el Id del Cliente definido como Publico General", true);
            GrabarParametro("CteSubPubGeneral", "0001", "Determina el Id del Sub Cliente definido como Publico General", true);
            GrabarParametro("ProgPubGeneral", "0001", "Determina el Id del Programa definido como Publico General", true);
            GrabarParametro("ProgSubPubGeneral", "0001", "Determina el Id del Sub Programa definido como Publico General", true);
        }

        private void ClasificacionSSA()
        {
            GrabarParametro("ClaseSSAGeneral", @"0004", "Especifica la Clave de Clasificación SSA estandar para los medicamentos y material de curación.", false);
        }

        private void FamiliaPrincipal()
        {
            GrabarParametro("FamiliaGeneral", @"02", "Especifica la Clave de Familia estandar para los medicamentos y material de curación.", false);
            GrabarParametro("SubFamiliaGeneral", @"03", "Especifica la Clave de Sub-Familia estandar para los medicamentos y material de curación.", false);
        }

        #endregion Funciones y Procedimientos privados

    }
}
