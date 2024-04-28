using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

namespace DllFarmaciaSoft
{
    public class clsParametrosOficinaCentral
    {
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;

        string sArbol = "";
        // string sParam = "";
        DataSet dtsParametros;
        string sListaDeParametros_a_Ejecutar = ""; 

        public clsParametrosOficinaCentral(clsDatosConexion DatosConexion, clsDatosApp DatosApp, string Arbol)
        {
            this.cnn = new clsConexionSQL(DatosConexion);
            this.leer = new clsLeer(ref cnn);
            this.Error = new clsGrabarError(DatosApp, "clsParametrosOficinaCentral");

            sListaDeParametros_a_Ejecutar = ""; 
            this.sArbol = Arbol;
            PreparaDtsParametros();
        } 

        #region Funciones y Procedimientos publicos 
        public bool CargarParametros()
        {
            bool bRegresa = true;
            string sSql = "";
            leer = new clsLeer(ref cnn); 

            GenerarParametros();

            ////// Procesar en una sola ejecución todos los parámetros
            RegistrarListaDeParametros(); 

            sSql = string.Format("Select ArbolModulo, NombreParametro, Valor, Descripcion  " + // EsDeSistema
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
        public bool RegistrarListaDeParametros()
        {
            bool bRegresa = true;
            if (!leer.Exec(sListaDeParametros_a_Ejecutar))
            {
                Error.GrabarError(leer, "GrabarParametro");
                bRegresa = false;
            }

            return bRegresa;
        }

        private void GrabarParametro(string NombreParametro, string Valor, string Descripcion, bool EsDeSistema)
        {
            GrabarParametro(sArbol, NombreParametro, Valor, Descripcion, EsDeSistema);
        }

        private void GrabarParametro(string ArbolModulo, string NombreParametro, string Valor, string Descripcion, bool EsDeSistema)
        {
            string sSql = ""; 
            
            sSql = string.Format(" Exec spp_Mtto_Net_CFGS_Parametros '{0}', '{1}', '{2}', '{3}' ",
                ArbolModulo, NombreParametro, Valor, Descripcion); // , EsDeSistema.ToString()

            //if (!leer.Exec(sSql)) 
            //{
            //    Error.GrabarError(leer, "GrabarParametro");
            //}

            sListaDeParametros_a_Ejecutar += sSql + "\n ";  
        }

        private void GenerarParametros()
        {
            ClientePublicoGeneral();
            RutaDeReportes();
            ClasificacionSSA();
            FamiliaPrincipal();
            PermitirCambioDePreciosLicitacion();
            ConfirmacionConHuellas();

            if (sArbol.ToUpper() == "COMP" || sArbol.ToUpper() == "COMR")
            {
                NotasOrdenesDeCompras();                
            }
        }

        #region Parametros 
        private void RutaDeReportes()
        {
            GrabarParametro("RutaReportes", @"", "Determina ruta donde se encuentran los reportes del Punto de Venta", false); 
            GrabarParametro("EncReportesPrin", "SISTEMA INTEGRAL DE INFORMACIÓN", "Determina el encabezado principal de los reportes de Oficina Central", false);
            GrabarParametro("EncReportesSec", "OFICINA CENTRAL", "Determina el encabezado secundario de los reportes de Oficina Central", false); 
        }

        private void PermitirCambioDePreciosLicitacion()
        {
            GrabarParametro("PermitirCambioDePreciosLicitacion", @"FALSE", "Determina si está permitido Asignar/Modificar los precios de Licitación.", false);
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

        private void ConfirmacionConHuellas()
        {
            GrabarParametro("ConfirmacionConHuellas", @"FALSE", "Determina si se usara el firma Biométrica para firmas electrónicas.", false);
            
        } 
        
        #endregion Parametros   
      
        #region Parametros de Compras 
        private void NotasOrdenesDeCompras()
        {
            GrabarParametro("NotaOrdenDeCompra", @"", "Especifica las Notas para el Proveedor en las Ordenes de Compras.", true);
        }
        #endregion Parametros de Compras
        #endregion Funciones y Procedimientos privados

    }
}
