using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

namespace Almacen
{
    public class clsParametrosCompras
    {
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;

        string sArbol = "";
        // string sParam = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        DataSet dtsParametros;

        public clsParametrosCompras(clsDatosConexion DatosConexion, clsDatosApp DatosApp, string IdEstado, string IdFarmacia, string Arbol)
        {
            this.cnn = new clsConexionSQL(DatosConexion);
            this.leer = new clsLeer(ref cnn);
            this.Error = new clsGrabarError(DatosApp, "clsParametrosCompras");

            this.sArbol = Arbol;
            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
            PreparaDtsParametros();
        }

        #region Funciones y Procedimientos publicos 
        public bool CargarParametros()
        {
            return CargarParametros(true); 
        }

        public bool CargarParametros(bool RegistrarParametros)
        {
            bool bRegresa = true;

            if (RegistrarParametros)
            {
                GenerarParametros();
            }

            string sSql = string.Format("Select ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema, EsEditable " +
                " From Net_CFG_Parametros_Compras (NoLock) " + 
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' ", sIdEstado, sIdFarmacia);
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "CargarParametros");
            }
            else
            {
                dtsParametros = leer.DataSetClase;
                ////while (leer.Leer())
                ////{
                ////}
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
        ////public bool GrabarParametro(string ArbolModulo, string NombreParametro, string Valor)
        ////{
        ////    return GrabarParametro(ArbolModulo, NombreParametro, Valor, "", false, false, 1);
        ////} 

        public bool GrabarParametro(string ArbolModulo, string NombreParametro, string Valor, string Descripcion, bool EsDeSistema, bool EsEditable)
        {
            return GrabarParametro(ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema, EsEditable, 0);
        }

        public bool GrabarParametro(string ArbolModulo, string NombreParametro, string Valor, string Descripcion, bool EsDeSistema, bool EsEditable, int Actualizar )
        {
            bool bRegresa = true;
            string sSql = string.Format(" Exec spp_Mtto_Net_CFG_Parametros_Compras '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'  ",
                sIdEstado, sIdFarmacia, ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema.ToString(), EsEditable.ToString(), Actualizar.ToString());
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GrabarParametro");
                bRegresa = false;
            }
            return bRegresa;
        }

        private void GenerarParametros()
        {
            CompraLibreDeProductos();
            CompraLibreDeProductos_Compradores();
            LectorHuella();
            ComprasManual();
        }

        #region Parametros        
        private void CompraLibreDeProductos()
        {
            GrabarParametro(sArbol, "CompraLibreDeProductos", @"FALSE", "Determina si las Ordenes de Compras se basan en el perfil de la operación.", false, true);
        }

        private void CompraLibreDeProductos_Compradores()
        {
            GrabarParametro(sArbol, "CompraLibreDeProductos_Compradores", @"FALSE", "Determina si las Ordenes de Compras generadas por cada Comprador se basan en el perfil del Claves del Comprador.", false, true);
        }

        private void LectorHuella()
        {
            GrabarParametro(sArbol, "LectorDeHuella", @"FALSE", "Determina si se usara el lector de huellas dactilares.", false, true);

        }

        private void ComprasManual()
        {
            GrabarParametro(sArbol, "NumeroDeCompras", "3", "Determina el número de compras que se van a procesar en las compras manuales", false, true);
            GrabarParametro(sArbol, "PorcMaxCompras", "10", "Determina el porcentaje de piezas que no sobre pasaran en las compras manuales", false, true);

        } 
        #endregion Parametros  
        #endregion Funciones y Procedimientos privados

    }
}

