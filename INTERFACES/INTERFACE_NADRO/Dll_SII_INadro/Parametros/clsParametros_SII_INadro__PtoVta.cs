using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

using DllFarmaciaSoft; 

namespace Dll_SII_INadro
{
    public class clsParametros_SII_INadro__PtoVta
    {
        #region Declaracion de Variables 
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;

        string sArbol = "";
        // string sParam = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        DataSet dtsParametros;
        #endregion Declaracion de Variables 

        #region Constructor y Destructor de Clase 
        public clsParametros_SII_INadro__PtoVta(clsDatosConexion DatosConexion, clsDatosApp DatosApp, string IdEstado, string IdFarmacia, string Arbol)
        {
            this.cnn = new clsConexionSQL(DatosConexion);
            this.leer = new clsLeer(ref cnn);
            this.Error = new clsGrabarError(DatosApp, "clsParametros_SII_INadro__PtoVta");

            this.sArbol = Arbol;
            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
            PreparaDtsParametros();
        }
        #endregion Constructor y Destructor de Clase

        #region Funciones y Procedimientos publicos
        public bool CargarParametros()
        {
            return CargarParametros(true);
        }

        public bool CargarParametros(bool EsPuntoDeVenta)
        {
            return CargarParametros(EsPuntoDeVenta, true);
        }

        public bool CargarParametros(bool EsPuntoDeVenta, bool RegistrarParametros)
        {
            bool bRegresa = true;

            if (RegistrarParametros)
            {
                if (EsPuntoDeVenta)
                {
                    GenerarParametros_PuntoDeVenta();
                }
                else
                {
                    GenerarParametros_Almacen();
                }
            }

            string sSql = string.Format("Select ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema, EsEditable " +
                " From Net_CFGC_Parametros (NoLock) Where IdEstado = '{0}' and IdFarmacia = '{1}' ", sIdEstado, sIdFarmacia);
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

        public bool GrabarParametro(string ArbolModulo, string NombreParametro, string Valor, string Descripcion, bool EsDeSistema, bool EsEditable, int Actualizar)
        {
            bool bRegresa = true;
            string sSql = string.Format(" Exec spp_Mtto_Net_CFGC_Parametros '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'  ",
                sIdEstado, sIdFarmacia, ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema.ToString(), EsEditable.ToString(), Actualizar.ToString());
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GrabarParametro");
                bRegresa = false;
            }
            return bRegresa;
        }

        private void GenerarParametros_PuntoDeVenta()
        {
        }

        private void GenerarParametros_Almacen()
        {
        }
        #endregion Funciones y Procedimientos privados

        #region Parametros
        private void RutaDeReportes()
        {
            GrabarParametro(sArbol, "RutaReportes", @"", "Determina ruta donde se encuentran los reportes del Punto de Venta.", false, true);
        } 
        #endregion Parametros
    }
}
