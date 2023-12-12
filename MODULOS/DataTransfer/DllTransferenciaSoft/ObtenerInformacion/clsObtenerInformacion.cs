using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;

using System.Threading; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
// using SC_SolutionsSystem.Criptografia;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft; 

using DllTransferenciaSoft;
using DllTransferenciaSoft.EnviarInformacion;
using DllTransferenciaSoft.Zip;

namespace DllTransferenciaSoft.ObtenerInformacion
{

    class clsDestinoFiles
    {
        public string Estado = "";
        public string Farmacia = "";
        public string UrlDestino = "";
        public string ArchivoOrigen = "";
        public string ArchivoDestino = "";
        public bool EnLinea = false; 
        basGenerales Fg = new basGenerales();
        public string ArchivoCfg = "";

        public string ClaveDestino = ""; 

        public clsDestinoFiles(string NombreArchivo, string UrlDestino)
        {
            FileInfo f = new FileInfo(NombreArchivo);

            this.ArchivoOrigen = NombreArchivo;
            this.UrlDestino = UrlDestino;
            this.ArchivoDestino = f.Name; 
        }

        public clsDestinoFiles(string NombreArchivo, clsLeer ListaDestinos):this(NombreArchivo, ListaDestinos, false)
        {
        }

        public clsDestinoFiles(string NombreArchivo, clsLeer ListaDestinos, bool DestinoCentralizado)
        {
            GetUrlDestino(NombreArchivo, ListaDestinos, DestinoCentralizado); 
        }

        private void GetUrlDestino(string NombreArchivo, clsLeer ListaDestinos, bool DestinoCentralizado)
        {
            string sURL = "";
            FileInfo f = new FileInfo(NombreArchivo);
            string sSSL = "";
            string sError = "";
            clsLeer leerRow = new clsLeer();

            string sClaveRenapo = Fg.Mid(f.Name, 8, 2);

            this.ClaveDestino = Fg.Mid(f.Name, 8, 6); 
            this.ArchivoOrigen = NombreArchivo;
            this.ArchivoDestino = f.Name;
            this.EnLinea = false;

            if (DestinoCentralizado)
            {
                this.ClaveDestino = Fg.Mid(f.Name, 1, 6);
            }

            try
            {
                // DataRow[] dtRow = ListaDestinos.DataSetClase.Tables[0].Select(string.Format("ClaveRenapo = '{0}'", sClaveRenapo));
                DataRow[] dtRow = ListaDestinos.DataSetClase.Tables[0].Select(string.Format("DirDestino = '{0}'", ClaveDestino));
                leerRow.DataRowsClase = dtRow;
                leerRow.Leer(); 

                if (dtRow.Length > 0)
                {
                    this.Estado = dtRow[0]["IdEstado"].ToString();
                    this.Farmacia = dtRow[0]["IdFarmacia"].ToString();
                    this.ClaveDestino = dtRow[0]["DirDestino"].ToString();

                    this.Estado = leerRow.Campo("IdEstado");
                    this.Farmacia = leerRow.Campo("IdFarmacia");
                    this.ClaveDestino = leerRow.Campo("DirDestino");                    
                    sSSL = leerRow.CampoBool("SSL") ? "s" : "";

                    try
                    {
                        this.EnLinea = leerRow.CampoBool("EnLinea");
                    }
                    catch { }

                    ////sURL = "http://" + dtRow[0]["Servidor"].ToString() + "/" + dtRow[0]["WebService"].ToString() + "/" + dtRow[0]["PaginaWeb"].ToString();
                    ////sURL = sURL.ToLower().Replace(".asmx", "") + ".asmx";

                    
                    sURL = string.Format("http{0}://{1}/{2}/{3}.asmx", sSSL, leerRow.Campo("Servidor"), leerRow.Campo("WebService"), leerRow.Campo("PaginaWeb"));
                    sURL = sURL.ToLower().Replace(".asmx", "") + ".asmx";



                    UrlDestino = sURL;
                }
            }
            catch { }
        }
    }

    public class clsObtenerInformacion
    {
        #region Declaracion de variables 
        clsConexionSQL cnn;
        clsDatosConexion datosCnn;
        clsLeer leerCat;
        clsLeer leerDet;
        clsLeer leerExec;
        clsLeer leer;
        clsLeer leerDestinos;
        clsLeer leerTransferencias; 


        private ArrayList pFiles;

        // DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion EnviarCliente;

        string sRutaObtencion = @"C:\\";
        string sRutaEnviados = @"C:\\";
        TipoServicio tpTipoObtencion = TipoServicio.Ninguno;
        TipoServicio tpTipoEnvio = TipoServicio.Ninguno; 
        
        string sTablaConfiguracion = "";
        string sTablaCatalogos = "";
        string sTablaTransfencias = "";
        string sTablaDestinos = "";
        string sEstadoDestino = "";
        string sClaveRenapoCteRegional = "";
        string sValorActualizado = "0"; 


        bool bExistenArchivos = false;
        string sCveRenapo = "FF";
        string sCveOrigenArchivo = "00FF";
        string sArchivoGenerado = "";
        string sArchivoCfg = "";

        int iTipoDeTablasEnvio = 1;
        bool bEnviandoArchivos = false;
        int iLargoFormatoNombre = 6;

        // Variables de funciones generales
        basGenerales Fg = new basGenerales();
        clsGrabarError Error;
        clsDatosApp datosApp = new clsDatosApp(Transferencia.Modulo + ".ObtenerInformacion", Transferencia.Version);

        string sArchivoSql = "";

        bool bEsEnvioMasivo = true;
        bool bEsEnvioADestino = false; 
        string sIdEstadoEnvio = "";
        string sIdFarmaciaEnvio = "";
        string sIdFarmaciaEnvioSolicita = "";

        bool bListaCatalogos = false;
        DataSet dtsListaCatalogos = new DataSet();
        string sArchivoGenerado_Solicitud = "";


        private bool bSoloEstadoEspecificado = false;
        private bool bEnviarA_FTP = false;
        private string sRutaFTP = "";
        string sArchivoGenerado_Solicitud_FTP = "";

        /// <summary>
        /// Controla la Ejecución de los Procesos que se ejecutan en Hilos, 
        /// se asegura de no dar por terminado el envio de información. 
        /// </summary>
        private int iNumProcesosEnEjecucion = 0;  
        #endregion Declaracion de variables 

        #region Constructor y Destructor 
        public clsObtenerInformacion(string ArchivoCfg, clsDatosConexion DatosConexion, 
            string ClaveRenapo, string Centro, TipoServicio TipoDeObtencion)
        {
            this.bEsEnvioMasivo = true;
            this.sCveRenapo = ClaveRenapo;
            this.sCveOrigenArchivo = Centro;
            this.sArchivoCfg = ArchivoCfg; 


            this.datosCnn = DatosConexion;
            this.tpTipoObtencion = TipoDeObtencion;
            cnn = new clsConexionSQL(this.datosCnn);
            sEstadoDestino = "";
            sValorActualizado = "0";

            sClaveRenapoCteRegional = " (E.ClaveRenapo + C.IdFarmacia) " ; 
            if (tpTipoObtencion == TipoServicio.OficinaCentral)
            {
                tpTipoEnvio = TipoServicio.OficinaCentralRegional; 
                sTablaConfiguracion = " CFGSC_ConfigurarObtencion ";
                sTablaCatalogos = " CFGSC_EnvioCatalogos ";
                sTablaTransfencias = " CFGSC_EnvioDetallesTrans ";
                sTablaDestinos = " CFGSC_ConfigurarConexiones "; 
                Error = new clsGrabarError(General.DatosConexion, datosApp, "clsOficinaCentral");
            }
            else if (tpTipoObtencion == TipoServicio.OficinaCentralRegional)
            {
                sValorActualizado = "1";
                tpTipoEnvio = TipoServicio.Cliente; 
                sTablaConfiguracion = " CFGS_ConfigurarObtencion ";
                sTablaCatalogos = " CFGS_EnvioCatalogos ";
                sTablaTransfencias = " CFGS_EnvioDetallesTrans ";
                sTablaDestinos = " CFGS_ConfigurarConexiones ";
                sEstadoDestino = string.Format(" and C.IdEstado = '{0}' ", DtGeneral.EstadoConectado );
                Error = new clsGrabarError(General.DatosConexion, datosApp, "clsOficinaCentralRegional");
            }
            else if (tpTipoObtencion == TipoServicio.ClienteOficinaCentralRegional)
            {
                ///// Revisar datos a Enviar desde Regionales al Central 
                sValorActualizado = "1";
                tpTipoEnvio = TipoServicio.OficinaCentral; 
                sTablaConfiguracion = " CFGCR_ConfigurarObtencion ";
                sTablaCatalogos = " CFGCR_EnvioDetalles ";
                sTablaTransfencias = " CFGCR_EnvioDetallesTrans ";
                sTablaDestinos = " CFGSC_ConfigurarConexion ";
                sEstadoDestino = string.Format(" and C.IdEstado = '{0}' ", DtGeneral.EstadoConectado);
                sClaveRenapoCteRegional = string.Format("'{0}'", DtGeneral.IdOficinaCentral + DtGeneral.IdFarmaciaCentral);
                Error = new clsGrabarError(General.DatosConexion, datosApp, "clsOficinaCentralRegional");
            }
            else if (tpTipoObtencion == TipoServicio.Cliente)
            {
                sTablaConfiguracion = " CFGC_ConfigurarIntegracion ";
                Error = new clsGrabarError(General.DatosConexion, datosApp, "clsCliente");
            }
            else
            {
                Error = new clsGrabarError(General.DatosConexion, datosApp, "SinAsignar");
            }

            // Preparar acceso a datos 
            leerCat = new clsLeer(ref cnn);
            leerDet = new clsLeer(ref cnn);
            leerExec = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            leerDestinos = new clsLeer(ref cnn);
            leerTransferencias = new clsLeer(ref cnn);

            if (General.DatosConexion == null)
            {
                General.DatosConexion = DatosConexion;
                General.FechasConsultarWeb = false;
            }

            Error.MostrarErrorAlGrabar = false; 
        }

        public clsObtenerInformacion(string ArchivoCfg, clsDatosConexion DatosConexion, string ClaveRenapo, string Centro, 
            TipoServicio TipoDeObtencion, string IdEstado, string IdFarmacia )
        {
            bEsEnvioMasivo = false;
            sIdEstadoEnvio = IdEstado;
            sIdFarmaciaEnvio = IdFarmacia; 

            this.sCveRenapo = ClaveRenapo;
            this.sCveOrigenArchivo = Centro;
            this.sArchivoCfg = ArchivoCfg;

            this.datosCnn = DatosConexion;
            this.tpTipoObtencion = TipoDeObtencion;
            cnn = new clsConexionSQL(this.datosCnn);
            sValorActualizado = "0"; 

            sClaveRenapoCteRegional = " (E.ClaveRenapo + C.IdFarmacia) "; 
            if (tpTipoObtencion == TipoServicio.OficinaCentral)
            {
                tpTipoEnvio = TipoServicio.OficinaCentralRegional; 
                sTablaConfiguracion = " CFGSC_ConfigurarObtencion "; 
                sTablaCatalogos = " CFGSC_EnvioCatalogos ";
                sTablaTransfencias = " CFGSC_EnvioDetallesTrans ";
                sTablaDestinos = " CFGSC_ConfigurarConexiones ";
                Error = new clsGrabarError(General.DatosConexion, datosApp, "clsOficinaCentral");
            }
            else if (tpTipoObtencion == TipoServicio.OficinaCentralRegional)
            {
                sValorActualizado = "1";
                tpTipoEnvio = TipoServicio.Cliente; 
                sTablaConfiguracion = " CFGS_ConfigurarObtencion ";
                sTablaCatalogos = " CFGS_EnvioCatalogos ";
                sTablaTransfencias = " CFGS_EnvioDetallesTrans ";
                sTablaDestinos = " CFGS_ConfigurarConexiones ";
                Error = new clsGrabarError(General.DatosConexion, datosApp, "clsOficinaCentralRegional");
            }
            else if (tpTipoObtencion == TipoServicio.ClienteOficinaCentralRegional)
            {
                sValorActualizado = "1";
                tpTipoEnvio = TipoServicio.OficinaCentral; 
                sTablaConfiguracion = " CFGCR_ConfigurarObtencion ";
                sTablaCatalogos = " CFGC_EnvioDetalles ";
                sTablaTransfencias = " CFGC_EnvioDetallesTrans ";
                sTablaDestinos = " CFGSC_ConfigurarConexion ";
                sEstadoDestino = string.Format(" and C.IdEstado = '{0}' ", DtGeneral.EstadoConectado);
                sClaveRenapoCteRegional = string.Format("'{0}'", DtGeneral.IdOficinaCentral + DtGeneral.IdFarmaciaCentral);
                Error = new clsGrabarError(General.DatosConexion, datosApp, "clsOficinaCentralRegional");
            }
            else
            {
                Error = new clsGrabarError(General.DatosConexion, datosApp, "SinAsignar");
            }

            // Preparar acceso a datos 
            leerCat = new clsLeer(ref cnn);
            leerDet = new clsLeer(ref cnn);
            leerExec = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            leerDestinos = new clsLeer(ref cnn);
            leerTransferencias = new clsLeer(ref cnn);

            if (General.DatosConexion == null)
            {
                General.DatosConexion = DatosConexion;
                General.FechasConsultarWeb = false;
            }

            Error.MostrarErrorAlGrabar = false; 
        }

        public clsObtenerInformacion(string ArchivoCfg, clsDatosConexion DatosConexion, string ClaveRenapo, string Centro,
            TipoServicio TipoDeObtencion, string IdEstado, string IdFarmacia, string IdFarmaciaSolicita, DataSet ListaCatalogos)
        {
            bEsEnvioMasivo = false;
            sIdEstadoEnvio = IdEstado;
            sIdFarmaciaEnvio = IdFarmacia;
            sIdFarmaciaEnvioSolicita = IdFarmaciaSolicita; 

            this.sCveRenapo = ClaveRenapo;
            this.sCveOrigenArchivo = Centro;
            this.sArchivoCfg = ArchivoCfg;

            this.datosCnn = DatosConexion;
            this.tpTipoObtencion = TipoDeObtencion;
            cnn = new clsConexionSQL(this.datosCnn);
            sValorActualizado = "0";

            bListaCatalogos = true; 
            dtsListaCatalogos = ListaCatalogos; 


            sClaveRenapoCteRegional = " (E.ClaveRenapo + C.IdFarmacia) ";
            if (tpTipoObtencion == TipoServicio.OficinaCentral)
            {
                tpTipoEnvio = TipoServicio.OficinaCentralRegional;
                sTablaConfiguracion = " CFGSC_ConfigurarObtencion ";
                sTablaCatalogos = " CFGSC_EnvioCatalogos ";
                sTablaTransfencias = " CFGSC_EnvioDetallesTrans ";
                sTablaDestinos = " CFGSC_ConfigurarConexiones ";
                Error = new clsGrabarError(General.DatosConexion, datosApp, "clsOficinaCentral");
            }
            else if (tpTipoObtencion == TipoServicio.OficinaCentralRegional)
            {
                sValorActualizado = "1";
                tpTipoEnvio = TipoServicio.Cliente;
                sTablaConfiguracion = " CFGS_ConfigurarObtencion ";
                sTablaCatalogos = " CFGS_EnvioCatalogos ";
                sTablaTransfencias = " CFGS_EnvioDetallesTrans ";
                sTablaDestinos = " CFGS_ConfigurarConexiones ";
                Error = new clsGrabarError(General.DatosConexion, datosApp, "clsOficinaCentralRegional");
            }
            else if (tpTipoObtencion == TipoServicio.ClienteOficinaCentralRegional)
            {
                sValorActualizado = "1";
                tpTipoEnvio = TipoServicio.OficinaCentral;
                sTablaConfiguracion = " CFGCR_ConfigurarObtencion ";
                sTablaCatalogos = " CFGC_EnvioDetalles ";
                sTablaTransfencias = " CFGC_EnvioDetallesTrans ";
                sTablaDestinos = " CFGSC_ConfigurarConexion ";
                sEstadoDestino = string.Format(" and C.IdEstado = '{0}' ", DtGeneral.EstadoConectado);
                sClaveRenapoCteRegional = string.Format("'{0}'", DtGeneral.IdOficinaCentral + DtGeneral.IdFarmaciaCentral);
                Error = new clsGrabarError(General.DatosConexion, datosApp, "clsOficinaCentralRegional");
            }
            else
            {
                Error = new clsGrabarError(General.DatosConexion, datosApp, "SinAsignar");
            }

            // Preparar acceso a datos 
            leerCat = new clsLeer(ref cnn);
            leerDet = new clsLeer(ref cnn);
            leerExec = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            leerDestinos = new clsLeer(ref cnn);
            leerTransferencias = new clsLeer(ref cnn);

            if (General.DatosConexion == null)
            {
                General.DatosConexion = DatosConexion;
                General.FechasConsultarWeb = false; 
            }

            Error.MostrarErrorAlGrabar = false;
        }  
        #endregion Constructor y Destructor

        #region Propiedades 
        public string ArchivoGenerado
        {
            get { return sArchivoGenerado_Solicitud; }
        }

        public string RutaObtencion
        {
            get { return sRutaObtencion; }
        }

        public bool ResultadoEnvio
        {
            get { return bEsEnvioADestino; }
        }

        public bool SoloEstadoEspecificado
        {
            get { return bSoloEstadoEspecificado; }
            set { bSoloEstadoEspecificado = value; }
        }

        public bool EnviarA_FTP
        {
            get { return bEnviarA_FTP; }
            set { bEnviarA_FTP = value; }
        }

        public string RutaFTP 
        {
            get { return sRutaFTP; }
            set { sRutaFTP = value; }
        }

        public string ArchivoGenerado_FTP
        {
            get { return sArchivoGenerado_Solicitud_FTP; }
        }
        #endregion Propiedades

        #region Obtener Configuraciones
        private bool GetRutaObtencion()
        {
            bool bRegresa = true;
            // string sSql = " Select * From CFGS_ConfigurarObtencion (NoLock) ";
            string sSql = string.Format("Select * From {0} (NoLock) ", sTablaConfiguracion);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GetRutaObtencion()");
                bRegresa = false;
            }
            else
            {
                if (!leer.Leer())
                {
                    bRegresa = false;
                    General.msjError("No se encontro la información de confuguración de envio de información, reportarlo al departamento de sistemas.");
                }
                else
                {
                    sRutaObtencion = leer.Campo("RutaUbicacionArchivos") + @"\\";
                    sRutaEnviados = leer.Campo("RutaUbicacionArchivosEnviados") + @"\\";
                    sArchivoSql = sRutaObtencion + @"\\Intermed_xxx.sql";

                    // Modificar la ruta de Archivos, generar el paquete en una Ruta Alterna 
                    if (!bEsEnvioMasivo)
                    {
                        sRutaObtencion += @"\\EnviarCatalogos\\";
                        if (bListaCatalogos)
                        {
                            // Catalogos solicitados por las Farmacias 
                            sRutaObtencion += Fg.PonCeros(sIdEstadoEnvio, 2) + @"\\" + Fg.PonCeros(sIdFarmaciaEnvioSolicita, 4) + @"\\"; 
                        }
                    }

                    if (leer.CampoInt("NumBloques") > 0)
                    {
                        Transferencia.BloquesRegistros = leer.CampoInt("NumBloques");
                    }

                    if (leer.CampoInt("TamañoBloque") > 0)
                    {
                        Transferencia.RegistrosSQL = leer.CampoInt("TamañoBloque");
                    }

                    //// Revisar que existen las Rutas 
                    CrearDirectorio(sRutaObtencion);
                    CrearDirectorio(sRutaEnviados); 
                }
            }
            return bRegresa;
        }
        #endregion Obtener Configuraciones

        #region Funciones y Procedimientos Privados
        private string GenerarNombre()
        {
            string sRegresa = "XX_Prueba";
            return sRegresa;
        }

        private string GeneraNombreArchivoTabla(string Tabla, int Orden)
        {
            string sRegresa = "Datos_" + Fg.PonCeros(Orden, 4) + "_" + Tabla;
            return sRegresa.ToUpper().Trim();
        }

        private bool ExistenTablasEnvio()
        {
            bool bRegresa = true;
            string sSql = string.Format("Select IdEnvio, NombreTabla, IdOrden, Status, Actualizado " + 
                " From {0} (NoLock) " + 
                " Where Status = 'A' {1} " + 
                " Order By IdOrden, NombreTabla ", sTablaCatalogos, ListaDeCatalogosSolicitados());

            // leerCat = new clsLeer(ref cnn);
            if (!leerCat.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerCat, "ExistenTablasEnvio()");
            }

            return bRegresa;
        }

        private string ListaDeCatalogosSolicitados()
        {
            string sRegresa = "";

            if (bListaCatalogos)
            {
                string sLista = "";
                string sTabla = ""; 
                clsLeer leerLista = new clsLeer();

                leerLista.DataSetClase = dtsListaCatalogos;
                if (leerLista.Registros > 0)
                {
                    while (leerLista.Leer())
                    {
                        sTabla = leerLista.Campo("NombreTabla");
                        sLista += "'" + sTabla + "', ";
                    }

                    sRegresa = " and NombreTabla in (  " + sLista + "'" + sTabla + "'  )";
                }
            }

            return sRegresa; 
        }


        private bool ExistenTablasEnvioTransferencia()
        {
            bool bRegresa = true;
            string sSql = string.Format("Select * From {0} (NoLock) " +
                " Where Status = 'A' Order By IdOrden, NombreTabla ", sTablaTransfencias);


            // leerCat = new clsLeer(ref cnn);
            if (!leerCat.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerCat, "ExistenTablasEnvioTransferencia()");
            }

            return bRegresa;
        }

        private bool ExisteTabla_A_Procesar(string Tabla)
        {
            bool bRegresa = false;
            string sSql = string.Format("Select * From Sysobjects (NoLock) Where Name = '{0}' and xType = 'U' ", Tabla);

            bRegresa = leer.Exec(sSql);
            if (!bRegresa)
            {
                sSql = string.Format("El objeto tabla [[ {0} ]] no existe", Tabla); ;
                Error.GrabarError(sSql, "ExisteTabla_A_Procesar");
            }

            return bRegresa;
        }

        private bool PreparaDatosTabla(string Tabla, Datos Efecto)
        {
            // Prepara los datos de la tabla seleccionada para solo copiar los datos necesarios
            bool bRegresa = true;
            string sEfecto = "0";
            string sWhere = "0";
            string sWhereTransf = ""; 

            if (Efecto == Datos.Obtener)
            {
                sEfecto = "2"; 
            }
            else if (Efecto == Datos.Procesado)
            {
                sEfecto = "1";
                sWhere = "2";
            }

            if (iTipoDeTablasEnvio == 2)
            {
                sWhereTransf = string.Format(", [ IdEstadoRecibe = '{0}' and IdFarmaciaRecibe = '{1}' ]", sIdEstadoEnvio, sIdFarmaciaEnvio);
            }

            string sSql = string.Format(" Exec spp_CFG_PrepararDatos '{0}', '{1}', '{2}' {3} ", Tabla, sEfecto, sWhere, sWhereTransf);


            // Cambiar el Status Actualizado solo cuando sea una Replicacion automatica 
            if (bEsEnvioMasivo)
            {
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leer, "PreparaDatosTabla");
                }
            }

            return bRegresa;
        }

        private bool ObtenerDatosTabla(string Tabla)
        {
            bool bRegresa = true;

            if (!bSoloEstadoEspecificado)
            {
                bRegresa = ObtenerDatosTabla_GENERAL(Tabla);
            }
            else
            {
                switch (tpTipoObtencion)
                {
                    case TipoServicio.OficinaCentral:
                    case TipoServicio.OficinaCentralRegional:
                        bRegresa = ObtenerDatosTabla_CENTRAL_REGIONAL(Tabla);
                        break;

                    case TipoServicio.Cliente:
                        bRegresa = ObtenerDatosTabla_GENERAL(Tabla);
                        break;
                }
            }

            return bRegresa; 
        }



        private bool ObtenerDatosTabla_CENTRAL_REGIONAL(string Tabla)
        {
            // sValorActualizado = "0"; 
            bool bRegresa = true;
            string sSql = string.Format(" Exec spp_CFG_ObtenerDatos @Tabla = '{0}', @Criterio = '', @sValorActualizado = '{1}', @IdEstado = '{2}' ", 
                Tabla, sValorActualizado, sIdEstadoEnvio ); 

            // Solo tomar la informacion marcada para Envio automatico 
            if (bEsEnvioMasivo)
            {
                sSql = string.Format(" Exec spp_CFG_ObtenerDatos @Tabla = '{0}', @Criterio = [ Where Actualizado = 2 ], @sValorActualizado = '{1}', @IdEstado = '{2}' ",
                    Tabla, sValorActualizado, sIdEstadoEnvio);

                // Las transferencias se envian con Actualizado = 0 para su envio inmediato 
                if (iTipoDeTablasEnvio == 2)
                {
                    sSql = string.Format(" Exec spp_CFG_ObtenerDatos  @Tabla = '{0}', @Criterio = [ Where Actualizado = 2 and IdEstadoRecibe = '{1}' and IdFarmaciaRecibe = '{2}' ], @sValorActualizado = '{3}', @IdEstado = '{4}' ", 
                        Tabla, sIdEstadoEnvio, sIdFarmaciaEnvio, sValorActualizado, sIdEstadoEnvio);
                }
            }

            if (!leerExec.Exec(Tabla, sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerExec, "ObtenerDatosTabla()");
            }
            return bRegresa;
        }

        private bool ObtenerDatosTabla_GENERAL(string Tabla)
        {
            // sValorActualizado = "0"; 
            bool bRegresa = true;
            string sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', '', '{1}' ", Tabla, sValorActualizado);

            // Solo tomar la informacion marcada para Envio automatico 
            if (bEsEnvioMasivo)
            {
                sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ Where Actualizado = 2 ], '{1}' ", Tabla, sValorActualizado);

                // Las transferencias se envian con Actualizado = 0 para su envio inmediato 
                if (iTipoDeTablasEnvio == 2)
                {
                    sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ Where Actualizado = 2 and IdEstadoRecibe = '{1}' and IdFarmaciaRecibe = '{2}' ], '{1}' ",
                        Tabla, sIdEstadoEnvio, sIdFarmaciaEnvio, sValorActualizado);
                }
            }

            if (!leerExec.Exec(Tabla, sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerExec, "ObtenerDatosTabla()");
            }
            return bRegresa;
        }

        private bool ObtenerDestinos()
        {
            bool bRegresa = true;
            string sSql = string.Format("Select Distinct C.*, E.ClaveRenapo, (E.ClaveRenapo + C.IdFarmacia) as DirDestino, " + 
                " cast(0 as bit) as EnLinea " + 
                " From {0} C (NoLock) " +
                " Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) " + 
                " Where C.IdEstado = '{1}' and C.IdFarmacia = '{2}' and C.Status = 'A' Order by C.IdEstado, C.IdFarmacia ", 
                sTablaDestinos, sIdEstadoEnvio, sIdFarmaciaEnvio );

            if (bEsEnvioMasivo)
            {
                sSql = string.Format("Select Distinct C.*, E.ClaveRenapo, {2} as DirDestino, cast(0 as bit) as EnLinea  " +
                       " From {0} C (NoLock) " +
                       " Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) " +
                       " Where C.Status = 'A'  {1}  " +
                       " Order by C.IdEstado, C.IdFarmacia ", sTablaDestinos, sEstadoDestino, sClaveRenapoCteRegional);

                if (!bEnviandoArchivos)
                {
                    // En caso de Transferencias se envian como si fuera un envio especial de información. 
                    if (iTipoDeTablasEnvio == 2)
                    {   // (E.ClaveRenapo + C.IdFarmacia)
                        // sSql = string.Format("Select Distinct C.*, E.ClaveRenapo, (E.ClaveRenapo + C.IdFarmacia) as DirDestino " +
                        sSql = string.Format("Select Distinct C.*, E.ClaveRenapo, {3} as DirDestino, cast(0 as bit) as EnLinea  " +
                                        " From {0} C (NoLock) " +
                                        " Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) " +
                                        " Where C.IdEstado = '{1}' and C.IdFarmacia = '{2}' and C.Status = 'A' Order by C.IdEstado, C.IdFarmacia ",
                                        sTablaDestinos, sIdEstadoEnvio, sIdFarmaciaEnvio, sClaveRenapoCteRegional);
                    }
                } 
            }

            if (!leerDestinos.Exec(sSql))
            {
                bRegresa = false;
                General.Error.GrabarError(leerDestinos, "ObtenerDestinos()"); 
            }

            return bRegresa;
        } 
        #endregion Funciones y Procedimientos Privados
        
        #region Generar archivos
        public bool GenerarArchivos()
        {
            bool bRegresa = true;
            bExistenArchivos = false;
            bEnviandoArchivos = false; 

            if (GetRutaObtencion())
            {
                this.Catalogos();
                if (bExistenArchivos)
                {
                    Empacar(DestinoArchivos.TodasLasFarmacias);
                    GenerarArchivosClientes(DestinoArchivos.TodasLasFarmacias);
                    //EnviarCatalogos(sArchivoCfg, sRutaObtencion + @"\\" + sArchivoGenerado, sArchivoGenerado);
                }

                this.Transferencias();
                //if (bExistenArchivos)
                //{
                //    Empacar(DestinoArchivos.Farmacia_A_Farmacia);
                //    GenerarArchivosClientes();
                //}
            }
            else
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        public bool EnviarArchivos()
        {
            bool bRegresa = true;
            bEsEnvioADestino = false; 

            bEnviandoArchivos = true; 
            bRegresa = EnviarCatalogos(sArchivoCfg); //, "", "");
            bEnviandoArchivos = false; 

            return bRegresa;
        }

        private bool Catalogos()
        {
            bool bRegresa = false;
            // string sFile = "";
            string sTabla = "";
            int iOrden = 0;

            iTipoDeTablasEnvio = 1; 
            ExistenTablasEnvio();
            ////while (leerCat.Leer())
            ////{
            ////    sTabla = leerCat.Campo("NombreTabla");
            ////    iOrden = leerCat.CampoInt("IdOrden");
            ////    GenerarTabla(sTabla, iOrden);
            ////    bRegresa = true;
            ////}

            if (leerCat.Registros > 0)
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    while (leerCat.Leer())
                    {
                        sTabla = leerCat.Campo("NombreTabla");
                        iOrden = leerCat.CampoInt("IdOrden");
                        bRegresa = GenerarTabla(sTabla, iOrden);
                        if (!bRegresa)
                        {
                            break;
                        }
                        // bRegresa = true;
                    }

                    if (bRegresa)
                    {
                        cnn.CompletarTransaccion();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                    }

                    cnn.Cerrar();
                }
            }

            return bRegresa;
        }

        private bool Transferencias()
        {
            bool bRegresa = false;

            ////// 2K100822-1938 
            ////// Se deshabilitan las transferencias, todas las transferencias las solicitan las farmacias. 

            //////string sFile = "";
            //////string sTabla = "";
            //////int iOrden = 0;

            //////iTipoDeTablasEnvio = 2; 
            //////BuscarTransferenciasPendientes(); 
            //////while (leerTransferencias.Leer())
            //////{
            //////    sIdEstadoEnvio = leerTransferencias.Campo("IdEstadoRecibe");
            //////    sIdFarmaciaEnvio = leerTransferencias.Campo("IdFarmaciaRecibe"); 
            //////    ExistenTablasEnvioTransferencia(); 
            //////    while (leerCat.Leer())
            //////    {
            //////        sTabla = leerCat.Campo("NombreTabla");
            //////        iOrden = leerCat.CampoInt("IdOrden");
            //////        GenerarTabla(sTabla, iOrden);
            //////        bRegresa = true; 
            //////    }

            //////    if (bExistenArchivos)
            //////    {
            //////        Empacar(DestinoArchivos.Farmacia_A_Farmacia);
            //////        GenerarArchivosClientes(DestinoArchivos.Farmacia_A_Farmacia);
            //////    }

            //////}

            //////// Asegurar que al Enviar los archivos estos se envien a su Destino Respectivo 
            //////iTipoDeTablasEnvio = 1; 

            return bRegresa;
        }

        private bool BuscarTransferenciasPendientes()
        {
            bool bRegresa = true;
            string sSql = string.Format(" Select Distinct IdEstadoRecibe, IdFarmaciaRecibe " + 
                " From TransferenciasEnvioEnc (NoLock) " + 
                " Where Status = 'A' and Actualizado in ( 0, 2 ) ");

            if (!leerTransferencias.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerTransferencias, "BuscarTransferenciasPendientes()"); 
            }

            return bRegresa; 
        }

        //////////////// 2011-01-27_1250 Posible error de Obtencion detectado, cada tabla es transaccional.... 
        ////////private void GenerarTabla(string Tabla, int Orden)
        ////////{
        ////////    string sFile = this.GeneraNombreArchivoTabla(Tabla, Orden);
        ////////    bool bExito = true;

        ////////    if (cnn.Abrir())
        ////////    {
        ////////        // Iniciar transaccion por cada tabla que se generara
        ////////        cnn.IniciarTransaccion();

        ////////        if (PreparaDatosTabla(Tabla, Datos.Obtener))
        ////////        {
        ////////            if (CrearArchivo(Tabla, sFile))
        ////////            {
        ////////                bExito = PreparaDatosTabla(Tabla, Datos.Procesado);
        ////////            }
        ////////        }

        ////////        if (!bExito)
        ////////            cnn.DeshacerTransaccion();
        ////////        else
        ////////            cnn.CompletarTransaccion();

        ////////        cnn.Cerrar();
        ////////    }
        ////////}

        private bool GenerarTabla(string Tabla, int Orden)
        {
            string sFile = this.GeneraNombreArchivoTabla(Tabla, Orden);
            bool bExito = true;

            if (ExisteTabla_A_Procesar(Tabla))
            {
                if (PreparaDatosTabla(Tabla, Datos.Obtener))
                {
                    if (CrearArchivo(Tabla, sFile))
                    {
                        bExito = PreparaDatosTabla(Tabla, Datos.Procesado);
                    }
                    else
                    {
                        bExito = false;
                    }
                }
                else
                {
                    bExito = false;
                }
            }

            return bExito;
        }

        private bool CrearArchivo(string Tabla, string Nombre)
        {
            bool bRegresa = true;
            int iReg = 0, iVueltas = 0; // , iRegistros = 0; 
            int iPaquete = 0;             
            string sArchivoDestSql = sRutaObtencion + "" + Nombre + Fg.PonCeros(iPaquete, iLargoFormatoNombre) + ".sql";
            StreamWriter f = null;

            if (ObtenerDatosTabla(Tabla)) 
            {
                try
                {
                    // File.Delete(sArchivoDestSql);
                    if (leerExec.Registros > 0)
                    {
                        bRegresa = true;
                        bExistenArchivos = true; 
                        while (leerExec.Leer())
                        {
                            if (iVueltas == 0)
                            {
                                iVueltas++; 
                                iPaquete++;
                                sArchivoDestSql = sRutaObtencion + "" + Nombre + "_" + Fg.PonCeros(iPaquete, iLargoFormatoNombre) + ".sql";
                                File.Delete(sArchivoDestSql);
                                f = new StreamWriter(sArchivoDestSql, false, Encoding.UTF8); 
                            }

                            f.WriteLine(leerExec.Campo(1));
                            iReg++;
                            // iRegistros++; 

                            // Agregar el separador de Registros 
                            if (iReg >= Transferencia.RegistrosSQL)
                            {
                                f.WriteLine(Transferencia.SQL);
                                f.WriteLine(""); 
                                iReg = 0;
                                iVueltas++; 
                            }

                            // Generar archivos de 200 Registros ==> 300-450 Kb
                            if (iVueltas >= Transferencia.BloquesRegistros)
                            {
                                // Cerrar el archivo con los Bloques Completos 
                                f.WriteLine(Transferencia.SQL);
                                f.WriteLine("");
                                f.Close();
                                iVueltas = 0;
                            }
                        }

                        if (iVueltas != 0)
                        {
                            // Cerrar el archivo en caso de no completar los bloques de Registros 
                            f.WriteLine(Transferencia.SQL);
                            f.WriteLine("");
                            f.Close();
                        }
                    }
                }
                catch //( Exception ex )
                {
                    //General.msjError(ex.Message);
                    bRegresa = false; 
                } 
            }

            return bRegresa;
        }

        private bool Empacar(DestinoArchivos Destino)
        {
            clsGrabarError.LogFileError("Iniciando generando paquete de datos."); 


            bool bRegresa = false;
            int iTipo = (int)Destino;
            string sNombrePaquete = "Intermed." + Transferencia.ExtArchivosGenerados;

            string sFileZip = sRutaObtencion + @"\\" + "Intermed." + Transferencia.ExtArchivosGenerados;
            string sFileOut = sRutaObtencion + @"\\" + "Intermed." + Transferencia.ExtArchivosGenerados;

            string []sFiles = Directory.GetFiles(sRutaObtencion, "*.sql");
            clsCriptografia Cripto = new clsCriptografia();
            ZipUtil zip = new ZipUtil();

            sArchivoGenerado = sNombrePaquete;
            bRegresa = zip.Comprimir(sFiles, sFileZip, true); 

            return bRegresa;
        }

        private void CrearDirectorio(string RutaDirectorio)
        {
            if (!Directory.Exists(RutaDirectorio))
            {
                Directory.CreateDirectory(RutaDirectorio);
            }
        }

        private bool GenerarArchivosClientes(DestinoArchivos Destino)
        {
            bool bRegresa = false; 
            string sFileOrigen = sRutaObtencion + @"\" + sArchivoGenerado;
            string sFileDestino = "";
            string sNombre = sCveRenapo + sCveOrigenArchivo;
            string sTipo = Fg.PonCeros((int)Destino, 2);
            string sMarcaTiempo = "";  // General.FechaYMD(General.FechaSistema).Replace("/", "") + General.FechaSistemaHora.Substring(0, 5).Replace(":", "");              
            // string sURL = "";
            string sDirUnidad = "";
            DateTime dtmMarcaDeTiempo = DateTime.Now;

            sMarcaTiempo = string.Format("{0}{1}{2}-{3}{4}",
                Fg.PonCeros(dtmMarcaDeTiempo.Year, 4), Fg.PonCeros(dtmMarcaDeTiempo.Month, 2), Fg.PonCeros(dtmMarcaDeTiempo.Day, 2),
                Fg.PonCeros(dtmMarcaDeTiempo.Hour, 2), Fg.PonCeros(dtmMarcaDeTiempo.Minute, 2) 
                );


            //////sMarcaTiempo = General.FechaYMD(General.FechaSistema).Replace("/", "");
            //////sMarcaTiempo = sMarcaTiempo.Replace("-", "");
            //////sMarcaTiempo += "-" + General.FechaSistemaHora.Substring(0, 5).Replace(":", "");  


            if (ObtenerDestinos())
            {
                pFiles = new ArrayList();
                while (leerDestinos.Leer())
                {
                    try
                    {
                        //sURL = "http://" + leerDestinos.Campo("Servidor") + "/" + leerDestinos.Campo("WebService") + "/" + leerDestinos.Campo("PaginaWeb");
                        //sURL = sURL.ToLower().Replace(".asmx", "") + ".asmx"; 
                        
                        // Generar un directorio por cada Unidad 
                        sDirUnidad = sRutaEnviados + @"\" + leerDestinos.Campo("DirDestino");
                        CrearDirectorio(sDirUnidad);

                        //// Crear directorio personalizado para el FTP 
                        if (bEnviarA_FTP)
                        {
                            CrearDirectorio(sRutaFTP); 
                        }

                        sFileDestino = sRutaObtencion + @"\" + sNombre + "-" + leerDestinos.Campo("ClaveRenapo") + leerDestinos.Campo("IdFarmacia");
                        sRutaFTP = sRutaFTP + @"\" + sNombre + "-" + leerDestinos.Campo("ClaveRenapo") + leerDestinos.Campo("IdFarmacia");
                        sArchivoGenerado_Solicitud_FTP = sNombre + "-" + leerDestinos.Campo("ClaveRenapo") + leerDestinos.Campo("IdFarmacia"); 
                        
                        if (tpTipoObtencion == TipoServicio.ClienteOficinaCentralRegional)
                        {
                            sFileDestino = sRutaObtencion + @"\" + sNombre + "-" + leerDestinos.Campo("DirDestino");
                            sRutaFTP = sRutaFTP + @"\" + sNombre + "-" + leerDestinos.Campo("DirDestino");
                            sArchivoGenerado_Solicitud_FTP = sNombre + "-" + leerDestinos.Campo("DirDestino");  
                        }

                        
                        sFileDestino += "-" + sTipo + "-" + sMarcaTiempo;
                        sFileDestino += "." + Transferencia.ExtArchivosGenerados;

                        sRutaFTP += "-" + sTipo + "-" + sMarcaTiempo;
                        sRutaFTP += "." + Transferencia.ExtArchivosGenerados;
                        sArchivoGenerado_Solicitud_FTP += "-" + sTipo + "-" + sMarcaTiempo;
                        sArchivoGenerado_Solicitud_FTP += "." + Transferencia.ExtArchivosGenerados;

                        // Generar el nombre del archivo de catalogos solicitado por las farmacias 
                        sArchivoGenerado_Solicitud = sFileDestino; 


                        File.Copy(sFileOrigen, sFileDestino);
                        if (bEnviarA_FTP)
                        {
                            File.Copy(sFileOrigen, sRutaFTP);

                            try
                            {
                                File.Delete(sFileOrigen);
                            }
                            catch { } 
                        }

                        bRegresa = true;
                    }
                    catch (Exception ex)
                    {
                        Error.LogError(ex.Message); 
                        bRegresa = false; break; 
                    }
                }

                //if (bRegresa)
                {
                    // Borrar el archivo Origen si se pudo generar una copia por cada Destino disponible. 
                    File.Delete(sFileOrigen); 
                }
            }

            return bRegresa;
        } 
        #endregion Generar archivos

        #region Enviar información 
        #region Revision de Comunicaciones 
        DataTable dtPing;
        int iRevisandoConexiones = 0; 

        private void ThreadRevisarConexion(object Registro)
        {
            clsPing ping = new clsPing();
            DllTransferenciaSoft.wsCliente.wsCnnCliente Cliente = new DllTransferenciaSoft.wsCliente.wsCnnCliente();
            string sURL = ""; 
            int iRow = (int)Registro;

            try
            {
                DataRow dtRow = dtPing.Rows[iRow];
                // sSvr = dtPing.Rows[iRow]["Servidor"].ToString();
                sURL = "http://" + dtRow["Servidor"].ToString() + "/" + dtRow["WebService"].ToString() + "/" + dtRow["PaginaWeb"].ToString();
                sURL = sURL.ToLower().Replace(".asmx", "") + ".asmx";

                Cliente.Url = sURL;
                Cliente.Timeout = 1000 * (20); 

                dtPing.Rows[iRow]["EnLinea"] = Cliente.TestConection();
            }
            catch (Exception ex1)
            {
                ex1.Source = ex1.Source; 
            }
            finally 
            {
                Cliente = null; 
            }
            iRevisandoConexiones--;
        }

        private void RevisarConexiones()
        {
            dtPing = leerDestinos.DataTableClase;
            // bool bEnLinea = false;
            int iReg = 0; 
            clsPing ping = new clsPing();

            iRevisandoConexiones = 0; 
            foreach (DataRow row in leerDestinos.DataTableClase.Rows)
            {
                Thread t = new Thread(this.ThreadRevisarConexion);
                iRevisandoConexiones++; 
                t.Name = "File_" + (iReg + 1).ToString();
                t.Start(iReg);
                iReg++; 
            }

            // Esperar a que se revisen todoas las conexiones 
            while( iRevisandoConexiones != 0 )
            {
                Thread.Sleep(50); 
            }

            leerDestinos.DataTableClase = dtPing;
        }
        #endregion Revision de Comunicaciones

        public byte[] ArchivoDeCatalogos()
        {
            byte[] btArchivo = Fg.ConvertirArchivoEnBytes(sArchivoGenerado_Solicitud);

            return btArchivo; 
        }

        private bool EnviarCatalogos(string ArchivoCfg) //, string RutaArchivo, string NombreDestino )
        {
            bool bRegresa = false;
            string sDestino = "";
            string sPatron = "*." + Transferencia.ExtArchivosGenerados;
            // string sURL = "", sFile = ""; 

            pFiles = new ArrayList(); 
            if (ObtenerDestinos())
            {
                RevisarConexiones(); 
                // EnviarCliente = new DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion(TipoServicio.Cliente);

                // sRutaObtencion 
                foreach (string f in Directory.GetFiles(sRutaObtencion, sPatron))
                {
                    pFiles.Add(new clsDestinoFiles(f, leerDestinos));
                }

                iNumProcesosEnEjecucion = 0; 
                foreach (clsDestinoFiles d in pFiles)
                {
                    // Asegurar el envio a Unidades que se encuentren en Linea 
                    if (d.EnLinea)
                    {
                        // Generar un directorio por cada Unidad 
                        sDestino = sRutaEnviados + @"\" + d.ClaveDestino;
                        CrearDirectorio(sDestino); 

                        d.ArchivoCfg = ArchivoCfg;
                        Thread t = new Thread(this.ThreadEnviarArchivo);
                        t.Name = d.ArchivoDestino;
                        iNumProcesosEnEjecucion++;
                        t.Start(d);
                    }
                    else
                    {
                        Error.GrabarError("No fue posible establecer comunicación con : " + d.UrlDestino + "..." + d.ArchivoDestino, "EnviarCatalogos");
                    }
                } 

                // Mantener el proceso detenido hasta que finalizen todos los envios de información. 
                while (iNumProcesosEnEjecucion != 0)
                {
                    Thread.Sleep(100); 
                }
            } 
            return bRegresa;
        }

        private void ThreadEnviarArchivo(object Archivo)
        {
            clsDestinoFiles d = (clsDestinoFiles)Archivo;
            //DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion 
            //    EnviarInformacion = new DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion(TipoServicio.Cliente);

            DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion
                EnviarInformacion = new DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion(tpTipoEnvio); 


            string sDestino = ""; 
            bool bRegresa = false; 
            //bRegresa = true; 

            //iNumProcesosEnEjecucion++; 
            try
            {
                // sDestino = sRutaEnviados + @"\" + d.ArchivoDestino;
                sDestino = sRutaEnviados + @"\" + d.ClaveDestino + @"\" + d.ArchivoDestino; 
                EnviarInformacion.Url = d.UrlDestino; 
                bRegresa = EnviarInformacion.Enviar(d.ArchivoCfg, d.ArchivoOrigen, d.ArchivoDestino, d.Estado, d.Farmacia);

                // Asegurar de Marcar como Exito el Envio 
                if (!bEsEnvioMasivo)
                {
                    bEsEnvioADestino = bRegresa; 
                }

                try
                {
                    if (bRegresa) 
                    {
                        File.Delete(sDestino); 
                        File.Move(d.ArchivoOrigen, sDestino); 
                    }
                }
                catch { }
            }
            catch { }
            iNumProcesosEnEjecucion--; 
        } 
        #endregion Enviar información 
    }
}

