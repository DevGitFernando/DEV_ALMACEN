using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

namespace Dll_IGPI
{
    public class clsParametrosIGPI
    {
        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;

        // string sArbol = "";
        // string sParam = "";
        // string sIdEstado = "";
        // string sIdFarmacia = "";
        DataSet dtsParametros;

        public clsParametrosIGPI(clsDatosConexion DatosConexion, clsDatosApp DatosApp)
        {
            this.cnn = new clsConexionSQL(DatosConexion);
            this.leer = new clsLeer(ref cnn);
            this.Error = new clsGrabarError(DatosApp, "clsParametrosIGPI");

            PreparaDtsParametros();
        }

        #region Funciones y Procedimientos publicos 
        public bool CargarParametros()
        {
            bool bRegresa = true;

            if (ExisteGPI())
            {
                GenerarParametros();

                string sSql = string.Format("Select NombreParametro, Valor, Descripcion " +
                    " From IGPI_Net_Parametros (NoLock) ");
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
        private bool ExisteGPI()
        {
            bool bRegresa = false;
            string sSql = string.Format(" select * From sysobjects (NoLock) Where Name = 'spp_Mtto_IGPI_Net_Parametros' and xType = 'P' ");

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
            string sSql = string.Format(" Exec spp_Mtto_IGPI_Net_Parametros  " + 
                " @NombreParametro = '{0}', @Valor = '{1}', @Descripcion = '{2}', @Actualizar = '{3}' ",
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
            PuertoCOM();
            ProtocoloConexion();
            ServidorIMach();
            PuertoTCP_IP();

            VersionGPI();
            EsMultipicking(); 
            IniciarSO(); 
            RutaDeReportes();
            HabilitarLog();
            HabilitarB_AlDispensar();

            CodePhone__TypeCode(); 
        }

        #region Parametros 
        private void ProtocoloConexion()
        {
            /// TCP_IP por default 
            GrabarParametro("ProtocoloConexion", @"1", "Especifica el protocolo de comunicación para conectar con IGPI. ( 1 ==> SERIAL   |   2 ==> TCP/IP ) " );
        }

        private void ServidorIMach()
        {
            GrabarParametro("ServidorGPI", @"127.0.0.1", "Especifica la dirección de servidor GPI." );
        }

        private void PuertoTCP_IP()
        {
            GrabarParametro("PuertoTCP_IP_Remoto", @"5510", "Especifica el Puerto Remoto TCP/IP de comunicaciones implementado por IGPI." );
            GrabarParametro("PuertoTCP_IP_Local", @"5610", "Especifica el Puerto Local TCP/IP de comunicaciones implementado por IGPI." );
            GrabarParametro("BloqueDeDatos", @"1", "Especifica el tamaño del bloque de datos para intercambio de información." );
        }
        private void PuertoCOM()
        {
            GrabarParametro("PuertoCOM", @"1", "Especifica el Puerto de comunicaciones implementado por IGPI.");
        }

        private void VersionGPI()
        {
            GrabarParametro("VersionGPI", @"", "Especifica la versión del módulo Bust a Pick(GPI).");
        }

        private void EsMultipicking()
        {
            GrabarParametro("Multipicking", @"False", "Especificar si Medimat trabaja con multipicking.");
        }

        private void IniciarSO()
        {
            GrabarParametro("IniciarSO", @"TRUE", "Determina si la Interface inicia junto con el Sistema Operativo.");
        }

        private void RutaDeReportes()
        {
            GrabarParametro("RutaReportes", @"", "Determina ruta donde se encuentran los reportes de la Interface GPI.");
        }

        private void HabilitarLog()
        {
            GrabarParametro("HabilitarLog", @"FALSE", "Habilita el log de texto de comunicaciones.");
        }

        private void HabilitarB_AlDispensar()
        {
            GrabarParametro("HabilitarB_AlDispensar", @"FALSE", "Habilita la solicitud de Stock al confirmar una dispensación.");
        }

        private void CodePhone__TypeCode()
        {
            GrabarParametro("CountryPhoneCode", @"039", "Código teléfonico del Páis.");
            GrabarParametro("TypeCode", @"02", "Tipo de Código de Producto.");
        }
        #endregion Parametros
        #endregion Funciones y Procedimientos privados

    }
}
