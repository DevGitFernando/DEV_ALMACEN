using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

namespace DllPedidosClientes
{
    public class clsConexionCliente
    {
        //clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
        clsDatosConexion DatosDeConexionRegional; 
        clsConexionSQL cnn ;
        clsConexionSQL cnnUnidad;
        clsLeer leer;
        clsLeer leerLocal;
        clsLeerWeb leerWeb;

        clsGrabarError Error;

        clsDatosCliente DatosCliente = new clsDatosCliente();
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;

        DataSet dtsRetorno = new DataSet();
        // byte[] btReporte = null;

        #region Declaracion Variables  
        string Estado = "", Farmacia = "", Sql = "", NombreRpt = "";
        string TablaFarmacia = "";
        string sUrl = "";
        string sHost = "";
        string Name = "clsConexionCliente";
        string sRutaReportes_Regional = "";

        string sArbol = "CTER";
        string sParametro = "RutaReportes";

        #endregion Declaracion Variables

        #region Constructor 
        public clsConexionCliente(clsDatosConexion DatosConexion, string Estado, string Farmacia, string Sql, string NombreRpt, string TablaFarmacia)
        {           

            cnn = new clsConexionSQL(DatosConexion);
            DatosDeConexionRegional = DatosConexion; 

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DatosConexion, DtGeneralPedidos.DatosApp, this.Name);

            GetRutaReportesRegional(); 

            this.Estado = Estado;
            this.Farmacia = Farmacia;
            this.Sql = Sql;
            this.NombreRpt = NombreRpt;
            this.TablaFarmacia = TablaFarmacia;

            //Inicializar la conexion web para obtener los datos de conexion 
            conexionWeb = new DllPedidosClientes.wsCnnClienteAdmin.wsCnnClientesAdmin(); 
        } 
        #endregion Constructor      

        #region Funciones
        public clsDatosConexion DatosDeConexionCliente
        {
            get 
            {
                if (DatosDeConexion == null)
                {
                    DatosDeConexion = new clsDatosConexion(); 
                    DatosDeConexion = DatosDeConexionRegional; 
                }

                return DatosDeConexion; 
            }
        }

        public string RutaReportes
        {
            get { return sRutaReportes_Regional; }
        }
        #endregion Funciones

        #region Funciones_Nuevas
        private void GetRutaReportesRegional()
        {
            string sSql = string.Format(" Select * From Net_CFGS_Parametros (NoLock) " +
                " Where ArbolModulo = '{0}' and NombreParametro = '{1}' ", sArbol, sParametro);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GetRutaReportesRegional()");
            }
            else
            {
                if (leer.Leer())
                {
                    sRutaReportes_Regional = leer.Campo("Valor");
                }
            }
        }

        private bool CargarUrlFarmacia()
        {
            bool bRegresa = false;

            string sSqlFarmacias = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                                " From vw_Farmacias_Urls U (NoLock) " +
                                " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                                " Where U.IdEstado = '{0}' and ( U.IdFarmacia = '{1}' ) " +
                                " and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                                Estado, Farmacia);

            if (!leer.Exec("CargarUrlFarmacia",sSqlFarmacias))
            {
                Error.GrabarError(leer.Error, "CargarUrlFarmacia()");
                dtsRetorno = leer.ListaDeErrores();                
            }
            else
            {
                if (leer.Leer())
                {
                    sUrl = leer.Campo("UrlFarmacia");
                    sHost = leer.Campo("Servidor");

                    bRegresa = true;
                }                
            }

            return bRegresa;
        }

        private bool FarmaciasAProcesar()
        {
            bool bReturn = false;            

            string sSql = string.Format("Delete From {0} ", TablaFarmacia);

            if (!leer.Exec("CTE_FarmaciasProcesar", sSql))
            {
                Error.GrabarError(leer.Error, "FarmaciasAProcesar()");
                dtsRetorno = leer.ListaDeErrores();             
            }
            else
            {              

                string sQuery = string.Format(" Insert Into {0} " +
                                     " Select '{1}','{2}','A',0 ", TablaFarmacia, Estado, Farmacia);
                if (!leer.Exec("CTE_FarmaciasProcesar", sQuery))
                {
                    Error.GrabarError(leer.Error, "FarmaciasAProcesar()");
                    dtsRetorno = leer.ListaDeErrores();                  
                }
                else
                {
                    bReturn = true;
                }
            }

            return bReturn;
        } 

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWeb(sUrl, DtGeneralPedidos.CfgIniPuntoDeVenta, DatosCliente);

                conexionWeb.Url = sUrl; 
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneralPedidos.CfgIniPuntoDeVenta));

                DatosDeConexion.Servidor = sHost;
                bRegresa = true;
                
            }
            catch (Exception ex1)
            {
                Error.GrabarError(ex1, "validarDatosDeConexion()");
                leer.Error = ex1;
                dtsRetorno = leer.ListaDeErrores();               
            }

            return bRegresa;
        }

        public DataSet ObtenerInformacion()
        {            

            if (CargarUrlFarmacia())
            {
                if (validarDatosDeConexion())
                {
                    cnnUnidad = new clsConexionSQL(DatosDeConexion);
                    cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                    cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite300;

                    leer = new clsLeer(ref cnnUnidad);

                    if (FarmaciasAProcesar())
                    {
                        if (!leer.Exec("ObtenerInformacion", Sql))
                        {
                            Error.GrabarError(leer.Error, "ObtenerInformacion()");
                            dtsRetorno = leer.ListaDeErrores();
                        }
                        else
                        {
                            dtsRetorno = leer.DataSetClase; 
                            
                        }
                    }
                }
            }

            return dtsRetorno;
        }        

        public bool Impresion()
        {
            bool bReporte = false;

            if (CargarUrlFarmacia())
            {
                if (validarDatosDeConexion())
                {
                    bReporte = true;
                }
            }

            return bReporte;
        }

        private bool ClavesAProcesar(DataSet dtsClaves, string NombreTabla)
        {
            bool bReturn = false;
            string sEdo = "", sFar = "", sIdClaveSSA = "", sClaveSSA = "";

            string sSql = string.Format("Delete From {0} ", NombreTabla);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer.Error, "ClavesAProcesar()");
                dtsRetorno = leer.ListaDeErrores();
                bReturn = false;
            }
            else
            {
                leerLocal.DataSetClase = dtsClaves;
                while (leerLocal.Leer())
                {
                    sEdo = leerLocal.Campo("IdEstado");
                    sFar = leerLocal.Campo("IdFarmacia");
                    sIdClaveSSA = leerLocal.Campo("IdClaveSSA");
                    sClaveSSA = leerLocal.Campo("ClaveSSA");

                    string sQuery = string.Format(" Insert Into {0} " +
                                         " Select '{1}', '{2}', '{3}', '{4}', 'A',0 ", NombreTabla, sEdo, sFar, sIdClaveSSA, sClaveSSA);
                    if (!leer.Exec(sQuery))
                    {
                        Error.GrabarError(leer.Error, "ClavesAProcesar()");
                        dtsRetorno = leer.ListaDeErrores();
                        bReturn = false;
                        break;
                    }
                    else
                    {
                        bReturn = true;
                    }
                }
                
            }

            return bReturn;
        }

        public DataSet InformacionClavesSSA(DataSet dtsClaves, string NombreTabla)
        {

            if (CargarUrlFarmacia())
            {
                if (validarDatosDeConexion())
                {
                    cnnUnidad = new clsConexionSQL(DatosDeConexion);
                    cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                    cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite300;

                    leer = new clsLeer(ref cnnUnidad);

                    if (ClavesAProcesar(dtsClaves, NombreTabla))
                    {
                        if (!leer.Exec("InformacionClavesSSA", Sql))
                        {
                            Error.GrabarError(leer.Error, "ObtenerInformacion()");
                            dtsRetorno = leer.ListaDeErrores();
                        }
                        else
                        {
                            dtsRetorno = leer.DataSetClase;

                        }
                    }
                }
            }

            return dtsRetorno;
        }

        #endregion Funciones_Nuevas

    }
}
