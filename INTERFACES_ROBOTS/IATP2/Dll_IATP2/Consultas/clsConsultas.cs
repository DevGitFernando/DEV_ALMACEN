using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

//using Dll_IATP2.Protocolos; 

namespace Dll_IATP2
{
    public class clsConsultas_IATP2
    {
        #region Declaración de variables
        //private wsConexion.wsControlObras cnnWebServ; // = new wsConexion.wsConexionDB();
        private clsErrorManager error = new clsErrorManager();
        private clsLogError errorLog = new clsLogError();
        private DialogResult myResult = new DialogResult();

        private DataSet dtsError = new DataSet();
        private DataSet dtsClase = new DataSet();
        private string strCnnString = General.CadenaDeConexion;
        private bool bUsarCnnRedLocal = true, bExistenDatos = false, bEjecuto = false;
        private DataSet myDataset;
        string sQuery = "";
        string sInicio = "Set DateFormat YMD ";

        string sNameDll = "";
        string sPantalla = "";
        string sVersion = "";
        bool bMostrarMsjLeerVacio = true;
        bool bEsPublicoGeneral = false; 

        private basGenerales Fg = new basGenerales();
        private clsCriptografo Cryp = new clsCriptografo();
        private Cls_Acceso_a_Datos_Sql Datos = new Cls_Acceso_a_Datos_Sql();
        private clsDatosConexion DatosConexion;
        private clsConexionSQL ConexionSql;

        #endregion
        
        #region Constructores de clase y destructor
        private clsConsultas_IATP2()
        {
            bUsarCnnRedLocal = General.ServidorEnRedLocal;
            strCnnString = General.CadenaDeConexion;
        }

        public clsConsultas_IATP2(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla)
        {
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
        }

        public clsConsultas_IATP2(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla, bool MostrarMsjLeerVacio)
        {
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
        }

        public clsConsultas_IATP2(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version)
        {
            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
        }

        public clsConsultas_IATP2(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version, bool MostrarMsjLeerVacio)
        {
            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
        }
        #endregion

        #region Propiedades publicas
        public bool ExistenDatos
        {
            get { return bExistenDatos; }
        }

        public bool Ejecuto
        {
            get { return bEjecuto; }
        }

        public bool MostrarMsjSiLeerVacio
        {
            get { return bMostrarMsjLeerVacio; }
            set { bMostrarMsjLeerVacio = value; }
        }

        public bool EsPublicoGeneral
        {
            get { return bEsPublicoGeneral; }
            set { bEsPublicoGeneral = value; }
        } 
        #endregion

        #region Modulos Dll_IATP2
        public DataSet Clientes(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Cliente";
            string sMsjNoEncontrado = "Clave de Cliente no encontrado, verifique.";

            sQuery = sInicio + " Select * From vw_IATP2_CFGC_Clientes (NoLock) Where Status = 'A' ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Clientes(string IdCliente, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Cliente";
            string sMsjNoEncontrado = "Clave de Cliente no encontrado, verifique.";

            sQuery = sInicio + " Select * " + 
                " From vw_IATP2_CFGC_Clientes (NoLock) " + 
                " Where IdCliente = '" + Fg.PonCeros(IdCliente, 4) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Estados(string IdEstado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Farmacia";
            string sMsjNoEncontrado = "Clave de Estado no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select distinct IdEstado, Estado " + 
                "From vw_Farmacias (NoLock) " + 
                "Where IdEstado = '{0}' and Status = 'A'", Fg.PonCeros(IdEstado, 2));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }


        public DataSet Farmacias(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Farmacias";
            string sMsjNoEncontrado = "No se encontrarón Farmacias, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_Farmacias (NoLock) Where Status = 'A' ");
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Farmacias(string IdEstado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Farmacia";
            string sMsjNoEncontrado = "Clave de Farmacia no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_Farmacias (NoLock) Where IdEstado = '{0}' and Status = 'A'", Fg.PonCeros(IdEstado, 2));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Farmacias(string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Farmacia";
            string sMsjNoEncontrado = "Clave de Farmacia no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_Farmacias (NoLock) " + 
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' ", 
                Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Parametros()
        {
            myDataset = new DataSet();

            sQuery = sInicio + " Select Parametro, Valor, Descripcion From Parametros (nolock) " +
                " Where IdSucursal = '" + General.EntidadConectada + "'";
            myDataset = (DataSet)EjecutarQuery(sQuery, "Parametros");

            return myDataset;
        }

        public DataSet Terminales(string IdTerminal, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Terminal";
            string sMsjNoEncontrado = "Clave de Terminal no encontrado, verifique.";

            sQuery = sInicio + " Select * From IATP2_CFGC_Terminales (NoLock) " + 
                " Where IdTerminal = '" + Fg.PonCeros(IdTerminal, 3) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Clientes_Terminales(string IdCliente, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener las Terminales del Cliente";
            string sMsjNoEncontrado = "Clave de Terminal no encontrado, verifique.";

            sQuery = sInicio + " Select * From vw_IATP2_CFGC_Clientes_Terminales (NoLock) Where IdCliente = '" + Fg.PonCeros(IdCliente, 4) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        } 

        public DataSet Clientes_Terminales(string IdCliente, string IdTerminal, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Terminal";
            string sMsjNoEncontrado = "Clave de Terminal no encontrado, verifique.";

            sQuery = sInicio + " Select * From IATP2_CFGC_Terminales (NoLock) " + 
                "Where IdCliente = '" + Fg.PonCeros(IdCliente, 4) + "'" + " IdTerminal = '" + Fg.PonCeros(IdTerminal, 4) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Productos_CodigoEAN(string CodigoEAN, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Producto.";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select P.*, " +  
                " IsNull(I.StatusIMach, -1) as StatusIMach, IsNull(I.EsMultipicking, 0) as EsMultipicking " +
                " From vw_Productos_CodigoEAN P (NoLock) " + 
                " Left Join vw_IATP2_CFGC_Productos I (NoLock) On ( P.CodigoEAN = I.CodigoEAN )  " +  
                " Where P.CodigoEAN = '{0}' ", CodigoEAN);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        ////public DataSet StatusSolicitud_Mach4(string FolioSolicitud, string IdProducto, string CodigoEAN, string Funcion)
        ////{
        ////    myDataset = new DataSet();
        ////    string sMsjError = "Ocurrió un error al obtener los datos de Atencion de la Solicitud a Mach4.";
        ////    string sMsjNoEncontrado = "No se encontro información con los criterios especificados.";
        ////    bool bInter = bMostrarMsjLeerVacio; 

        ////    bMostrarMsjLeerVacio = false; 
        ////    sQuery = sInicio +
        ////        string.Format(" Select FolioSolicitud, IdProducto, CodigoEAN, sum(CantidadSolicitada) as CantidadRequerida, " +
        ////            " ( Select sum(CantidadSurtida) as CantidadDispensada " + 
        ////            "   From IMach_SolicitudesProductos (NoLock) " + 
        ////            "   Where FolioSolicitud = '{0}' and IdProducto = '{1}' and CodigoEAN = '{2}' and StatusIMach = '{3}' " + 
        ////            " ) as CantidadDispensada " +
        ////        " From IMach_SolicitudesProductos (NoLock) " +
        ////        " Where FolioSolicitud = '{0}' and IdProducto = '{1}' and CodigoEAN = '{2}' " + 
        ////        " Group by FolioSolicitud, IdProducto, CodigoEAN ",
        ////        FolioSolicitud, IdProducto, CodigoEAN, (int)IATP2_StatusRespuesta_A.AcknowledgmentMessage );
        ////    myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
        ////    bMostrarMsjLeerVacio = bInter;
        ////    return myDataset; 
        ////}

        ////public DataSet StatusDeProductos(string Funcion)
        ////{
        ////    myDataset = new DataSet();
        ////    string sMsjError = "Ocurrió un error al obtener los datos de Status de Productos.";
        ////    string sMsjNoEncontrado = "No se encontro información, verifique.";

        ////    sQuery = sInicio + string.Format(" Select StatusIMach, (cast(StatusIMach as varchar) + ' ' + Descripcion) as Descripcion " +
        ////        " From IMach_CFGC_Productos_Status (NoLock) " +
        ////        " Order By StatusIMach ");
        ////    myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

        ////    return myDataset;
        ////}
        #endregion Modulos Dll_IATP2

        #region Ordenes de Acondicionamiento 
        public DataSet OrdenesDeProduccion(string FolioSolicitud, string IdEmpresa, string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Orden de Acondicionamiento.";
            string sMsjNoEncontrado = "Orden de Acondicionamiento no encontrada, verifique.";

            sQuery = sInicio +
                string.Format("Exec spp_GET_IATP2_OrdenesDeProduccion   @FolioSolicitud = '{0}', @IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}' ", 
                FolioSolicitud, IdEmpresa, IdEstado, IdFarmacia);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Ordenes de Acondicionamiento

        #region Funciones y procedimientos privados
        private DataSet EjecutarQuery(string Funcion, string prtQuery, string MensajeError, string MensajeNoEncontrado )
        {
            clsLeer Leer = new clsLeer( ref ConexionSql );
            DataSet dtsResultados = new DataSet();

            bEjecuto = false;
            Leer.Conexion.SetConnectionString();
            if (!Leer.Exec(prtQuery))
            {
                General.Error.GrabarError(Leer.Error, ConexionSql.DatosConexion, this.sNameDll, this.sVersion, this.sPantalla, Funcion, Leer.QueryEjecutado);
                General.msjError(MensajeError);
            }
            else
            {
                bEjecuto = true;
                if (!Leer.Leer())
                {
                    if (bMostrarMsjLeerVacio)
                        General.msjUser(MensajeNoEncontrado);
                }
                else
                {
                    dtsResultados = Leer.DataSetClase;
                }
                
            }

            return dtsResultados;
        }

        private object EjecutarQuery(string prtQuery, string prtTabla)
        {
            object objRetorno = null;
            DataSet dtsRetorno = new DataSet("Vacio");
            Datos.CadenaDeConexion = strCnnString;

            // Si ocurre algun error evitar que traten de accesar un dataset vacio
            bExistenDatos = false;

            try
            {
                if (bUsarCnnRedLocal)
                {
                    objRetorno = (object)Datos.ObtenerDataset(prtQuery, prtTabla);
                }
                else
                {
                    //cnnWebServ = new wsConexion.wsControlObras();
                   // cnnWebServ.Url = General.Url;
                    //objRetorno = (object)cnnWebServ.ObtenerDataset(Cryp.Encriptar(strCnnString), prtQuery, prtTabla);
                }

                dtsRetorno = (DataSet)objRetorno;
                if (error.ExistenErrores(dtsRetorno))
                {
                    // Buscar en el dataset la tabla de errores                    
                    myResult = error.MostrarVentanaError(true, false, dtsRetorno);
                    dtsRetorno = new DataSet("Vacio");
                    objRetorno = (object)dtsRetorno;
                }

                bExistenDatos = ExistenDatosEnDataset(dtsRetorno);

            }
            catch (Exception e)
            {
                if ( objRetorno != null)
                    e = (Exception)objRetorno;


                dtsRetorno = new DataSet("Vacio");
                objRetorno = (object)dtsRetorno;

                errorLog = new clsLogError(e);
                error = new clsErrorManager(errorLog.ListaErrores);
                myResult = error.MostrarVentanaError(true, false, errorLog.ListaErrores);
            }

            return objRetorno;
        }

        private bool ExistenDatosEnDataset(DataSet dtsRevisar)
        {
            bool bRegresa = false;

            if (dtsRevisar.Tables.Count > 0)
            {
                if (dtsRevisar.Tables[0].Rows.Count > 0)
                    bRegresa = true;
            }

            return bRegresa;
        }
        #endregion
    }
}
