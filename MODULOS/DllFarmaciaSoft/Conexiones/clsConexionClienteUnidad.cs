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

using DllFarmaciaSoft; 

namespace DllFarmaciaSoft.Conexiones
{
    public class clsConexionClienteUnidad
    {
        //clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
        clsDatosConexion datosCnnUnidad;
        clsConexionSQL cnn ;
        clsConexionSQL cnnUnidad;
        clsLeer leer;
        // clsLeer leerLocal;
        // clsLeerWebExt leerWeb;

        clsGrabarError Error;

        // clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;

        DataSet dtsRetorno = new DataSet();
        DataTable dtTablaInformacion = new DataTable("Informacion");
        // byte[] btReporte = null;

        #region Declaracion Variables  
        string sArchivoIniCentral = "";
        string sArchivoIniUnidad = ""; 

        string IdEmpresa = "";
        string IdEstado = "";
        string IdFarmacia = "";
        string Sql = "";
        string sRutaReportes_Regional = ""; 
        string NombreRpt = "";
        string TablaFarmacia = "";
        string sUrl = "";
        string sUrl_Aux = ""; 
        string sHost = "";
        string sHost_Aux = "";
        string Name = "clsConexionCliente";

        string sArbol = "OCEN";
        string sParametro = "RutaReportes";

        #endregion Declaracion Variables

        #region Constructor 
        public clsConexionClienteUnidad()
        {
            datosCnnUnidad = new clsDatosConexion();
            DatosDeConexion = new clsDatosConexion();
            Error = new clsGrabarError(); 
        }

        public clsConexionClienteUnidad(DataSet Informacion, DataSet InformacionCliente) 
        {
            //Se Inicializa la conexion web para obtener los datos de conexion 
            conexionWeb = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente(); 
            // cnn = new clsConexionSQL(DatosConexion);
            // leer = new clsLeer(ref cnn);           
 
            datosCnnUnidad = new clsDatosConexion();
            DatosDeConexion = new clsDatosConexion(); 
            DatosInformacion(Informacion.Tables[0]);
            GetDatosDeConexion(sArchivoIniCentral);

            cnn = new clsConexionSQL(DatosDeConexion);
            leer = new clsLeer(ref cnn);
            GetRutaReportesRegional(); 

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DatosDeConexion, DtGeneral.DatosApp, this.Name);
        }

        public clsConexionClienteUnidad(DataSet Informacion, DataSet InformacionCliente, DataSet InformacionReporteWeb)
        {
            //Se Inicializa la conexion web para obtener los datos de conexion 
            conexionWeb = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente();
            // cnn = new clsConexionSQL(DatosConexion);
            // leer = new clsLeer(ref cnn);

            datosCnnUnidad = new clsDatosConexion();
            DatosDeConexion = new clsDatosConexion(); 
            DatosInformacion(Informacion.Tables[0]); 
            GetDatosDeConexion(sArchivoIniCentral);

            cnn = new clsConexionSQL(DatosDeConexion);
            leer = new clsLeer(ref cnn);
            GetRutaReportesRegional(); 

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DatosDeConexion, DtGeneral.DatosApp, this.Name); 
        }

        //public clsConexionClienteUnidad(clsDatosConexion DatosConexion, 
        //    string Estado, string Farmacia, string Sql, string NombreRpt, string TablaFarmacia)
        //{           

        //    cnn = new clsConexionSQL(DatosConexion);

        //    leer = new clsLeer(ref cnn);
        //    leerLocal = new clsLeer(ref cnn);

        //    Error = new SC_SolutionsSystem.Errores.clsGrabarError(DatosConexion, DtGeneral.DatosApp, this.Name);

        //    this.IdEmpresa = Estado;
        //    this.IdEstado = Estado;
        //    this.IdFarmacia = Farmacia;
        //    this.Sql = Sql;
        //    this.NombreRpt = NombreRpt;
        //    this.TablaFarmacia = TablaFarmacia;

        //    //Inicializar la conexion web para obtener los datos de conexion 
        //    conexionWeb = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente(); 
        //} 
        #endregion Constructor      

        #region Propiedades
        public clsDatosConexion DatosDeConexionCliente
        {
            get { return DatosDeConexion; }
            set 
            { 
                DatosDeConexion = value; 
                cnn = new clsConexionSQL(DatosDeConexion);
                leer = new clsLeer(ref cnn);
            } 
        }

        public clsDatosConexion DatosDeConexionUnidad
        {
            get { return datosCnnUnidad; }
            set
            {
                datosCnnUnidad = value;
                cnn = new clsConexionSQL(datosCnnUnidad);
                leer = new clsLeer(ref cnn);
            }
        }


        public string ArchivoConexionCentral
        {
            get { return sArchivoIniCentral; }
            set { sArchivoIniCentral = value; }
        }

        public string ArchivoConexionUnidad
        {
            get { return sArchivoIniUnidad; }
            set { sArchivoIniUnidad = value; }
        }

        public string Empresa
        {
            set { IdEmpresa = value; }
            get { return IdEmpresa; }
        }

        public string Estado
        {
            set { IdEstado = value; }
            get { return IdEstado; }
        }

        public string Farmacia
        {
            set { IdFarmacia = value; }
            get { return IdFarmacia; }
        }

        public string Sentencia
        {
            set { Sql = value; }
            get { return Sql; }
        }

        public string Reporte
        {
            set { NombreRpt = value; }
            get { return NombreRpt; }
        }

        public string RutaReportes
        {
            get { return sRutaReportes_Regional;  }
        }

        public string Tabla
        {
            set { TablaFarmacia = value; }
            get { return TablaFarmacia; }
        }

        public DataSet dtsInformacion
        {
            get { return ObtenerDataSet(); }
        }

        #endregion Propiedades

        #region Funciones y Procedimientos Privados  
        private void GetDatosDeConexion(string ArchivoIni) 
        {
            DatosDeConexion = new clsDatosConexion();
            basSeguridad funciones = new basSeguridad(ArchivoIni);

            DatosDeConexion.Servidor = funciones.Servidor;
            DatosDeConexion.BaseDeDatos = funciones.BaseDeDatos;
            DatosDeConexion.Usuario = funciones.Usuario;
            DatosDeConexion.Password = funciones.Password;
            DatosDeConexion.TipoDBMS = funciones.TipoDBMS;

        }

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

            string sSqlFarmacias = string.Format(
            " Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor,  " +
            " (WebService + '/' + PaginaWeb + '.asmx') as UrlBase " +     
            " From vw_Farmacias_Urls U (NoLock) " +
            " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
            " Where U.IdEstado = '{0}' and ( U.IdFarmacia = '{1}' ) " +
            " and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ", Estado, Farmacia); 

            if (!leer.Exec("CargarUrlFarmacia", sSqlFarmacias))
            {
                Error.GrabarError(leer.Error, "CargarUrlFarmacia()");
                dtsRetorno = leer.ListaDeErrores();
            }
            else 
            {
                if (!leer.Leer())
                {
                    //Error.LogError("2 ==> " +  sSqlFarmacias); 
                }
                else 
                {
                    sUrl = leer.Campo("UrlFarmacia");
                    sHost = leer.Campo("Servidor");
                    sUrl_Aux = "http://localhost/" + leer.Campo("UrlBase");
                    sHost_Aux = "localhost";

                    ////Error.GrabarError(sUrl, "CargarUrlFarmacia");
                    ////Error.GrabarError(sUrl_Aux, "CargarUrlFarmacia");


                    ////if (sHost.Contains(":"))
                    ////{
                    ////    string[] sHostList = sHost.Split(':');
                    ////    sHost = sHostList[0]; 
                    ////} 

                    bRegresa = true;
                }
            }

            return bRegresa;
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            bRegresa = validarDatosDeConexion(sUrl, sHost);
            if (!bRegresa)
            {
                bRegresa = validarDatosDeConexion(sUrl_Aux, sHost_Aux);
            }

            ////try
            ////{
            ////    ////leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

            ////    Error.LogError("2 ==> " + sUrl); 
            ////    conexionWeb.Url = sUrl;
            ////    datosCnnUnidad = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

            ////    datosCnnUnidad.Servidor = sHost;
            ////    bRegresa = true;
            ////    Error.LogError("3 ==> " + datosCnnUnidad.CadenaDeConexion); 
            ////}
            ////catch (Exception ex1)
            ////{
            ////    Error.GrabarError(ex1 + "2 ==> " + sUrl, "validarDatosDeConexion()");
            ////    dtsRetorno = leer.ListaDeErrores();
            ////}

            return bRegresa;
        }

        private bool validarDatosDeConexion(string UrlConexion, string HostConexion)
        {
            bool bRegresa = false;

            try
            {
                ////leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

                ////Error.GrabarError("1 ==> " + UrlConexion, "validarDatosDeConexion()");

                conexionWeb = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente(); 
                conexionWeb.Url = UrlConexion;
                datosCnnUnidad = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

                datosCnnUnidad.Servidor = HostConexion;
                bRegresa = true;
                ////Error.GrabarError("3 ==> " + datosCnnUnidad.CadenaDeConexion, "validarDatosDeConexion()");
                ////Error.LogError("3 ==> " + datosCnnUnidad.CadenaDeConexion);
            }
            catch (Exception ex1)
            {
                Error.GrabarError(ex1 + "2 ==> " + UrlConexion, "validarDatosDeConexion()");
                dtsRetorno = leer.ListaDeErrores();
            }

            return bRegresa;
        }

        private void CrearTabla()
        {
            // Se agregan las columnas a la tabla Informacion 
            dtTablaInformacion = new DataTable("Informacion"); 
            dtTablaInformacion.Columns.Add("IdEmpresa", Type.GetType("System.String"));
            dtTablaInformacion.Columns.Add("IdEstado", Type.GetType("System.String"));
            dtTablaInformacion.Columns.Add("IdFarmacia", Type.GetType("System.String"));
            dtTablaInformacion.Columns.Add("Sql", Type.GetType("System.String"));
            dtTablaInformacion.Columns.Add("ArchivoIniCentral", Type.GetType("System.String"));
            dtTablaInformacion.Columns.Add("ArchivoIniUnidad", Type.GetType("System.String")); 
        }

        private DataSet ObtenerDataSet()
        {
            DataSet dtsInformacion = new DataSet();

            CrearTabla();
            object[] Datos = { IdEmpresa, IdEstado, IdFarmacia, Sql, sArchivoIniCentral, sArchivoIniUnidad };
            dtTablaInformacion.Rows.Add(Datos);
            dtsInformacion.Tables.Add(dtTablaInformacion.Copy());

            return dtsInformacion;
        }

        private void DatosInformacion(DataTable dtTabla)
        {
            foreach( DataRow dtRow in dtTabla.Rows)
            {
                this.IdEmpresa = dtRow["IdEmpresa"].ToString();
                this.IdEstado = dtRow["IdEstado"].ToString();
                this.IdFarmacia = dtRow["IdFarmacia"].ToString();
                this.Sql = dtRow["Sql"].ToString();
                this.sArchivoIniCentral = dtRow["ArchivoIniCentral"].ToString();
                this.sArchivoIniUnidad = dtRow["ArchivoIniUnidad"].ToString();
            }            
        }
        #endregion Funciones y Procedimientos Privados 

        #region Funciones y Procedimientos Publicos 
        public DataSet ObtenerInformacion()
        {            
            if (CargarUrlFarmacia())
            {
                if (validarDatosDeConexion())
                {
                    cnnUnidad = new clsConexionSQL(datosCnnUnidad);
                    cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                    cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite300;

                    leer = new clsLeer(ref cnnUnidad);

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

        #endregion Funciones y Procedimientos Publicos

    }
}
