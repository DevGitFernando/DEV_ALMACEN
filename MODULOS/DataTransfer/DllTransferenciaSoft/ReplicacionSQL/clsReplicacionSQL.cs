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
using DllTransferenciaSoft.ObtenerInformacion; 

namespace DllTransferenciaSoft.ReplicacionSQL
{
    public class clsReplicacionSQL
    {
        #region Declaracion de variables
        protected clsConexionSQL cnn;
        protected clsDatosConexion datosCnn;
        protected clsLeer leerCat;
        protected clsLeer leerDet;
        protected clsLeer leerExec;
        protected clsLeer leer;
        protected clsLeer leerDestinos;
        protected clsLeer leerTransferencias;

        private ArrayList pFiles;

        wsCliente.wsCnnCliente conWebSQL = new wsCliente.wsCnnCliente();

        // DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion EnviarCliente;

        protected string sRutaObtencion = @"C:\\";
        protected string sRutaEnviados = @"C:\\";
        string sUrlDestino = "";

        protected bool bExistenArchivos = false;
        string sCveRenapo = DtGeneral.IdOficinaCentral; // "FF";
        string sCveOrigenArchivo = DtGeneral.IdFarmaciaCentral; // "00FF";
        string sArchivoGenerado = "";
        string sArchivoCfg = "";
        string sRutaTransferencias = "";
        bool bEsProcesoDeTranserencias = false;

        protected int iTipoDeTablasEnvio = 1;
        // bool bEnviandoArchivos = false;
        protected string sIdEstadoEnvio = "";
        protected string sIdFarmaciaEnvio = "";
        protected string sRutaArchivo = "";

        string sIdServer = "0001";
        string sIdEstadoOrigen = "";
        string sIdFarmaciaOrigen = "";
        string sClaveRenapoOrigen = "";

        //// Variables de funciones generales
        protected basGenerales Fg = new basGenerales();
        protected clsGrabarError Error;
        string sServidor = "";
        string sWebService = "";
        string sPaginaWeb = "";
        string sSqlConsultaFecha;

        bool bSoloEstadoEspecificado = false;
        bool bReplicacionPorPeriodos = true;
        int iDiasRevision_FechaControl = 30;
        int iDiasRevision_FechaControl_Default = 0;
        int iTotalDeRegistros = 0;
        int iMB_File = 5;
        TamañoFiles tpTamañoFiles = TamañoFiles.MB;

        int iTipoDeProceso = 1;
        int iTipoDeEnvio = 1;
        bool bReplicarDirecto_SQL = false;
        bool bRevisionPorFechaServidor = false;

        bool bTemporizador_Inicializado = false; 
        DateTime dtInicio_Replicacion = DateTime.Now;
        DateTime dtFin_Replicacion = DateTime.Now;


        /// <summary> 
        /// Controla la Ejecución de los Procesos que se ejecutan en Hilos, 
        /// se asegura de no dar por terminado el envio de información. 
        /// </summary>
        private int iNumProcesosEnEjecucion = 0;
        #endregion Declaracion de variables

        #region Constructor y Destructor
        public clsReplicacionSQL(): this(1, 1, 0)
        { 
        }

        public clsReplicacionSQL(int TipoDeEnvio, int TipoDeProceso, int DiasDeRevision):
            this(TipoDeEnvio, TipoDeProceso, DiasDeRevision, General.FechaYMD(DateTime.Now.AddDays(-1 * DiasDeRevision), "-"), General.FechaYMD(DateTime.Now, "-"), false)
        { 
        }

        public clsReplicacionSQL(int TipoDeEnvio, int TipoDeProceso, string FechaInicial, string FechaFinal)
            : this(TipoDeEnvio, TipoDeProceso, 0, FechaInicial, FechaFinal, false) 
        {
        }

        public clsReplicacionSQL(int TipoDeEnvio, int TipoDeProceso, bool bRevisionPorFechaServidor)
            : this(TipoDeEnvio, TipoDeProceso, 0, General.FechaYMD(DateTime.Now.AddDays(-1 * 3), "-"), General.FechaYMD(DateTime.Now, "-"), bRevisionPorFechaServidor)
        {
        }

        public clsReplicacionSQL(int TipoDeEnvio, int TipoDeProceso, int DiasDeRevision, string FechaInicial, string FechaFinal, bool RevisionPorFechaServidor)
        {
            this.datosCnn = General.DatosConexion;
            this.iDiasRevision_FechaControl_Default = DiasDeRevision;
            this.iTipoDeProceso = TipoDeProceso;
            this.iTipoDeEnvio = TipoDeEnvio;
            this.bReplicarDirecto_SQL = this.iTipoDeEnvio > 1;
            this.bRevisionPorFechaServidor = RevisionPorFechaServidor;


            cnn = new clsConexionSQL(this.datosCnn);
            Error = new clsGrabarError(Transferencia.Modulo + ".Replicacion", Transferencia.Version, "clsReplicacionSQL");
            Error.MostrarErrorAlGrabar = false;

            // Preparar acceso a datos 
            leerCat = new clsLeer(ref cnn);
            leerDet = new clsLeer(ref cnn);
            leerExec = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            leerDestinos = new clsLeer(ref cnn);
            leerTransferencias = new clsLeer(ref cnn);

            if (!bTemporizador_Inicializado)
            {
                bTemporizador_Inicializado = true;

                DateTimePicker dt_I = new DateTimePicker();
                DateTimePicker dt_F = new DateTimePicker();
                dt_I.Value = Convert.ToDateTime(FechaInicial);
                dt_F.Value = Convert.ToDateTime(FechaFinal); 

                dtInicio_Replicacion = dtInicio_Replicacion.AddDays(-1 * iDiasRevision_FechaControl);
                dtFin_Replicacion = DateTime.Now;

                dtInicio_Replicacion = dt_I.Value;
                dtFin_Replicacion = dt_F.Value; 

            }
        }
        #endregion Constructor y Destructor

        #region Propiedades Publicas 
        public bool SoloEstadoEspecificado
        {
            get { return bSoloEstadoEspecificado; }
            set { bSoloEstadoEspecificado = value; }
        }

        public string IdEstadoEnvio
        {
            get { return sIdEstadoEnvio; }
            set { sIdEstadoEnvio = value; }
        }

        public string RutaArchivo
        {
            get { return sRutaArchivo; }
            set { sRutaArchivo = value; }
        }
        #endregion Propiedades Publicas

        #region Obtener Configuraciones
        private bool GetRutaObtencion()
        {
            bool bRegresa = true;
            string sSql = "Select * From CFGC_ConfigurarObtencion (NoLock) ";
            string sGUID = Guid.NewGuid().ToString();

            System.Console.WriteLine("");
            System.Console.WriteLine(""); 
            System.Console.WriteLine("Obteniendo rutas de archivos.");
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GetRutaObtencion()");
                System.Console.WriteLine(string.Format("        Error : {0}", leer.MensajeError)); 
                bRegresa = false;
            }
            else
            {
                if (!leer.Leer())
                {
                    bRegresa = false;
                    System.Console.WriteLine(string.Format("        Error : {0}", "No se encontro la información de configuración de envio de información, reportarlo al departamento de sistemas.")); 
                    //General.msjError("No se encontro la información de configuración de envio de información, reportarlo al departamento de sistemas.");
                }
                else
                {
                    ////bReplicacionPorPeriodos = leer.CampoBool("TipoReplicacion");
                    iDiasRevision_FechaControl = leer.CampoInt("DiasRevision");

                    if (iDiasRevision_FechaControl_Default > 0)
                    {
                        iDiasRevision_FechaControl = iDiasRevision_FechaControl_Default; 
                    }

                    tpTamañoFiles = (TamañoFiles)leer.CampoInt("TipoDePaquete");
                    iMB_File = leer.CampoInt("TamañoDePaquete");

                    sRutaObtencion = leer.Campo("RutaUbicacionArchivos") + @"\Replicacion SQL" + @"\\";
                    sRutaEnviados = leer.Campo("RutaUbicacionArchivosEnviados") + @"\Replicacion SQL" + @"\\";

                    if (sRutaArchivo != "")
                    {
                        sRutaObtencion = sRutaArchivo + @"\\";
                    }

                    if (bReplicarDirecto_SQL)
                    {
                        sRutaObtencion += string.Format(@"{0}\\", sGUID);
                        sRutaEnviados += string.Format(@"{0}\\", sGUID); 
                    }

                    System.Console.WriteLine(string.Format("Días de revisión de replicación : {0}", iDiasRevision_FechaControl));
                    System.Console.WriteLine(string.Format("    {0}", "Directorios de trabajo"));
                    System.Console.WriteLine(string.Format("        {0}", sRutaObtencion));
                    System.Console.WriteLine(string.Format("        {0}", sRutaEnviados));
                    System.Console.WriteLine(""); 


                    //// Revisar que existen las Rutas 
                    if (!Directory.Exists(sRutaObtencion))
                    {
                        Directory.CreateDirectory(sRutaObtencion);
                    }

                    if (!Directory.Exists(sRutaEnviados))
                    {
                        Directory.CreateDirectory(sRutaEnviados);
                    }
                }
            }
            return bRegresa;
        }
        #endregion Obtener Configuraciones

        #region Funciones y Procedimientos Publicos
        public void Abrir_Directorio_Transferencias()
        {
            General.AbrirDirectorio(sRutaObtencion);
        }
        #endregion Funciones y Procedimientos Publicos  

        #region Funciones y Procedimientos Privados
        private string GenerarNombre()
        {
            string sRegresa = "XX_Prueba";
            return sRegresa;
        }

        protected string GeneraNombreArchivoTabla(string Tabla, int Orden, int Orden_Aux)
        {
            string sRegresa = string.Format("Datos_{0}_{1}_{2}", Fg.PonCeros(Orden, 4), Fg.PonCeros(Orden_Aux, 4), Tabla);
            return sRegresa.ToUpper().Trim();
        }

        private string GetTablaProceso()
        {
            string sRegresa = " CFG_RP_EnvioInformacion ";

            switch (iTipoDeProceso)
            {
                case 2:
                    sRegresa = " CFGS_RP_EnvioInformacionCatalogos ";
                    break;

                default:
                    break;
            }

            return sRegresa; 
        }

        private bool ExistenTablasEnvio()
        {
            bool bRegresa = true;
            string sTipo = " CFG_RP_EnvioInformacion ";
            sTipo = GetTablaProceso(); 


            string sSql = string.Format(" Select * From {0} (NoLock) Where Status = 'A' Order By IdOrden, NombreTabla ", sTipo);

            // leerCat = new clsLeer(ref cnn);
            if (!leerCat.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerCat, "ExistenTablasEnvio()");
                System.Console.WriteLine(string.Format("        Error : {0}", leerCat.MensajeError));
            }

            return bRegresa;
        }

        private bool ExistenTablasEnvioDetalle(string TablaControl)
        {
            bool bRegresa = true;
            string sTipo = string.Format(" {0} ", TablaControl);


            string sSql = string.Format(" Select * From {0} (NoLock) Where Status = 'A' Order By IdOrden, NombreTabla ", sTipo);

            // leerCat = new clsLeer(ref cnn);
            if (!leerDet.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerDet, "ExistenTablasEnvioDetalle()");
                System.Console.WriteLine(string.Format("        Error : {0}", leerDet.MensajeError));
            }

            return bRegresa;
        }

        protected bool ExisteTabla_A_Procesar(string Tabla)
        {
            bool bRegresa = false;
            string sSql = string.Format("Select * From Sysobjects (NoLock) Where Name = '{0}' and xType = 'U' ", Tabla);

            //System.Console.WriteLine(string.Format("Validando exista tabla : {0}", Tabla));

            bRegresa = leer.Exec(sSql);
            bRegresa = leer.Leer();
            if (!bRegresa)
            {
                sSql = string.Format("El objeto tabla [[ {0} ]] no existe", Tabla); ;
                Error.GrabarError(sSql, "ExisteTabla_A_Procesar");
                System.Console.WriteLine(sSql);
            }

            return bRegresa;
        }

        private bool PreparaDatosTabla(string Tabla, Datos Efecto)
        {
            // Prepara los datos de la tabla seleccionada para solo copiar los datos necesarios
            bool bRegresa = true;
            string sSql = "";
            string sEfecto = "0";
            string sWhere = "0";
            string sWhereTransf = "";
            bool bEjecutar = !bReplicacionPorPeriodos;

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
                // Las Transferencias no incluyen el ELSE para el Update de datos 
                sWhereTransf = string.Format(", [ IdEstadoRecibe = '{0}' and IdFarmaciaRecibe = '{1}' ], '', 0 ", sIdEstadoEnvio, sIdFarmaciaEnvio);
                sWhereTransf = string.Format(", [ IdEstadoRecibe = '{0}' and IdFarmaciaRecibe = '{1}' ] ", sIdEstadoEnvio, sIdFarmaciaEnvio);

                sSql = string.Format(" Exec spp_CFG_PrepararDatos '{0}', '{1}', '{2}' ", Tabla, sEfecto, sWhere);
                sSql = string.Format(" Exec spp_CFG_PrepararDatos '{0}', '{1}', '{2}' {3} ", Tabla, sEfecto, sWhere, sWhereTransf);
                bEjecutar = true;
            }
            else
            {
                sSql = string.Format(" Exec spp_CFG_PrepararDatos '{0}', '{1}', '{2}' ", Tabla, sEfecto, sWhere);
                sSql = string.Format(" Exec spp_CFG_PrepararDatos '{0}', '{1}', '{2}' {3} ", Tabla, sEfecto, sWhere, sWhereTransf);
            }

            if (bEjecutar)
            {
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leer, "PreparaDatosTabla");
                    System.Console.WriteLine(string.Format("        Error : {0}", leer.MensajeError));
                }
            }

            return bRegresa;
        }

        private bool ObtenerDatosTabla(string Tabla)
        {
            bool bRegresa = true;

            ////// Asegurar que los Clientes envien la informacion al Servidor Regional para Reenvio a Servidor Central 
            string sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ Where Actualizado = 2 ], '0' ", Tabla);
            string sFiltro = "";

            if (bReplicacionPorPeriodos)
            {
                ////// Final 
                sFiltro = string.Format(" Where convert(varchar(10), FechaControl, 120) between '{0}' and '{1}' ",
                    General.FechaYMD(dtInicio_Replicacion, "-"), General.FechaYMD(dtFin_Replicacion, "-"));

                switch (iTipoDeProceso)
                {
                    case 2:
                        sFiltro = ""; 
                        break;

                    default:
                        break; 
                }

                //if (Tabla == "CatBeneficiarios" || Tabla == "CatMedicos" ||
                //    Tabla == "FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones" ||
                //    Tabla == "FarmaciaProductos_CodigoEAN_Lotes" ||
                //    Tabla == "FarmaciaProductos_CodigoEAN" ||
                //    Tabla == "FarmaciaProductos")
                //{
                //    sFiltro = "";
                //}

                if (Tabla == "Ctl_Replicaciones")
                {
                    sSql = string.Format("Select Distinct IdEmpresa, IdEstado, IdFarmacia, " + 
                        "Cast(0 As Varchar(100)) As RegistrosVentasAdicional, Cast(0 As Varchar(100)) As RegistrosVentasLotes, CAST('' As Varchar(100)) As VersionBD " + 
                        "Into #TempFar " + 
	                    "From MovtosInv_Det_CodigosEAN (NoLock) " +

                        "Select IdEmpresa, IdEstado, IdFarmacia, cast(COUNT(*) As Varchar(100)) As Registros " +
                        "Into #VentasAdicional " +
	                    "From VentasInformacionAdicional A " +
                        "Group By IdEmpresa, IdEstado, IdFarmacia " +

                        "Update T Set RegistrosVentasAdicional = Registros " +
                        "From #TempFar T " +
                        "Inner Join #VentasAdicional A On (T.IdEmpresa = A.IdEmpresa And T.IdEstado = A.IdEstado And T.IdFarmacia = A.IdFarmacia) " +

                        "Select IdEmpresa, IdEstado, IdFarmacia, cast(COUNT(*) As Varchar(100)) As Registros " +
                        "Into #VentasLotes " +
                        "From VentasDet_Lotes A " +
                        "Group By IdEmpresa, IdEstado, IdFarmacia " +

                        "Update T Set RegistrosVentasLotes = Registros " +
                        "From #TempFar T " +
                        "Inner Join #VentasLotes A On (T.IdEmpresa = A.IdEmpresa And T.IdEstado = A.IdEstado And T.IdFarmacia = A.IdFarmacia) " +
	
	                    "Update #TempFar Set VersionBD = (Select Max(dbo.fg_FormatoVersion(Version)) From Net_Versiones Where Tipo = 1) " +
	
	                    "Select 'Exec spp_Ctl_Replicaciones @IdEmpresa  = ' + CHAR(39) + IdEmpresa  + CHAR(39) + ', @IdEstado = ' + CHAR(39) + IdEstado  + CHAR(39) +  " +
			            "        ', @IdFarmacia = ' + CHAR(39) + IdFarmacia + CHAR(39) + ', @FechaIncial = '  + CHAR(39) + '{0}' + CHAR(39) +  " +
			            "        ', @FechaFinal = '  + CHAR(39) + '{1}' + CHAR(39) +  ', @BaseDeDatos = '  + CHAR(39) + DB_NAME() + CHAR(39) +  " +
                        "        ', @Host = '  + CHAR(39) + @@SERVERNAME + CHAR(39) + " +
                        "	  	 ', @RegistrosVentasAdicional = ' + RegistrosVentasAdicional + " +
                        "   	 ', @RegistrosVentasLotes = ' + RegistrosVentasLotes + " +
                        "        ', @VersionBD = ' + Char(39) + VersionBD  + Char(39) +  ', @VersionExe = '+ Char(39) +  '{2}'   + Char(39)  " +
                        "From #TempFar", General.FechaYMD(dtInicio_Replicacion, "-"), General.FechaYMD(dtFin_Replicacion, "-"), DtGeneral.Version);           
                }
                else
                {
                    if (!bSoloEstadoEspecificado)
                    {
                        sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ {1} ], '0' ", Tabla, sFiltro);
                        sSql = string.Format(" Exec spp_CFG_ObtenerDatos @Tabla = '{0}', @Criterio = [ {1} ], @sValorActualizado = '0' ", Tabla, sFiltro);
                    }
                    else
                    {
                        sSql = string.Format(" Exec spp_CFG_ObtenerDatos @Tabla = '{0}', @Criterio = [ {1} ], @sValorActualizado = '0', @IdEstado = '{2}'  ",
                            Tabla, sFiltro, sIdEstadoEnvio);
                    }
                }
            }

            if (!leerExec.Exec(Tabla, sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerExec, "ObtenerDatosTabla()");
                System.Console.WriteLine(string.Format("        Error al procesar : {0}", Tabla));
            }
            return bRegresa;
        }

        private bool ObtenerDestino()
        {
            bool bRegresa = true;

            string sFiltro = "";

            if (bSoloEstadoEspecificado)
            {
                sFiltro = string.Format(" And IdEstado = '{0}'", sIdEstadoEnvio);
            }

            string sSql = string.Format("Select IdEmpresa, IdEstado, IdFarmacia, SSL, Servidor, WebService, PaginaWeb, IdOrden, Status, " +
                " (IdEstado + IdFarmacia) as DirDestino, cast(0 as bit) as EnLinea " + 
                " From CFG_RP_ConfigurarConexiones (NoLock) " +  
                " Where Status = 'A' {0} " + 
                " Order by IdOrden ", sFiltro);

            if (leerDestinos.Registros == 0)
            {
                System.Console.WriteLine("Obteniendo listado de servidores destino.");
                if (!leerDestinos.Exec(sSql))
                {
                    bRegresa = false;
                    General.Error.GrabarError(leerDestinos, "ObtenerDestino()");
                    System.Console.WriteLine(string.Format("        Error : {0}", leerDestinos.MensajeError));
                }
            }
            else
            {
                leerDestinos.RegistroActual = 0;
            }

            return bRegresa;
        }

        private void LimpiarDirectorio(string Ruta, string Extencion)
        {
            try
            {
                foreach (string f in Directory.GetFiles(Ruta, "*." + Extencion))
                {
                    File.Delete(f);
                }
            }
            catch { }
        }
        #endregion Funciones y Procedimientos Privados

        #region Generar archivos
        public bool GenerarArchivos()
        {
            bool bRegresa = true;
            bExistenArchivos = false;

            System.Console.WriteLine("Iniciando generación de archivos.");

            iTotalDeRegistros = 0;
            ObtenerDestino();
            if (GetRutaObtencion() && bRegresa)
            {
                LimpiarDirectorio(sRutaObtencion, "*");

                ////while (dtInicio_Replicacion <= dtFin_Replicacion)
                {
                    System.Console.WriteLine("");
                    System.Console.WriteLine("");
                    System.Console.WriteLine(string.Format("    Procesando {0}", General.FechaYMD(dtInicio_Replicacion)));
                    LimpiarDirectorio(sRutaObtencion, "sql");

                    bExistenArchivos = false;

                    bRegresa = this.DetallesInformacion(1); // Tablas de Detalles de movimentos 
                    if (bRegresa)
                    {
                        if (bExistenArchivos)
                        {
                            if (bReplicarDirecto_SQL)
                            {
                                Enviar_SQL();
                            }
                            else
                            {
                                Empacar();
                                GenerarArchivosServidor(DestinoArchivos.Farmacia_A_OficinaCentral);
                            }
                        }
                    }

                    dtInicio_Replicacion = dtInicio_Replicacion.AddDays(1); 
                }
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

            //// bRegresa = EnviarDetalles(sArchivoCfg); //, "", "");
            bRegresa = EnviarDetallesServidorCliente(sArchivoCfg);

            return bRegresa;
        }

        private bool DetallesInformacion(int Tipo)
        {
            bool bRegresa = true;
            //string sFile = "";
            string sTabla = "";
            string sTabla_Detalle = "";
            int iOrden = 0;
            int iOrden_Aux = 0;

            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("Validando catálogos de control");
            ExistenTablasEnvio();


            if (ObtenerDestino())
            {
                if (bRevisionPorFechaServidor)
                {
                    bRegresa = ObtenerFecha();
                }
            }
            else
            {
                bRegresa = false;
            }

            System.Console.WriteLine(string.Format("FechaInicial : {0}      FechaFinal : {1}", dtInicio_Replicacion.ToShortDateString(), dtFin_Replicacion.ToShortDateString()));

            if (leerCat.Registros > 0 && bRegresa)
            {
                while (leerCat.Leer() && bRegresa)
                {
                    sTabla = leerCat.Campo("NombreTabla");
                    iOrden = leerCat.CampoInt("IdOrden");

                    System.Console.WriteLine(""); 
                    System.Console.WriteLine(string.Format("Validando catálogo de control : {0} ", sTabla));
                    if (ExistenTablasEnvioDetalle(sTabla))
                    {
                        while (leerDet.Leer() && bRegresa)
                        {
                            sTabla_Detalle = leerDet.Campo("NombreTabla");
                            iOrden_Aux = leerDet.CampoInt("IdOrden");
                            bRegresa = GenerarTabla(sTabla_Detalle, iOrden, iOrden_Aux);
                        }
                    }
                    else
                    {
                        bRegresa = false;
                    }
                }
            }

            return bRegresa;
        }


        protected bool ObtenerFecha()
        {
            bool bRegresa = true;
            string sSSL = "";
            TimeSpan tDif;

            if (leerDestinos.Leer())
            {
                sSSL = leerDestinos.CampoBool("SSL") ? "s" : "";
                sUrlDestino = string.Format("http{0}://{1}/{2}/{3}.asmx", sSSL, leerDestinos.Campo("Servidor"), leerDestinos.Campo("WebService"), leerDestinos.Campo("PaginaWeb"));
                sUrlDestino = sUrlDestino.ToLower().Replace(".asmx", "") + ".asmx";

                conWebSQL = new wsCliente.wsCnnCliente();
                conWebSQL.Url = sUrlDestino;

                if (ObtenerConsulta())
                {
                    leer.DataSetClase = conWebSQL.ExecuteExt(new DataSet(), DtGeneral.CfgIniReplicacionSQL, sSqlConsultaFecha);

                    if (leer.Leer())
                    {
                        if (leer.CampoBool("Correcto"))
                        {
                            dtInicio_Replicacion = leer.CampoFecha("Fecha");
                            dtFin_Replicacion = DateTime.Now;

                            tDif = dtFin_Replicacion - dtInicio_Replicacion;
                        }
                        else
                        {
                            bRegresa = false;
                            System.Console.WriteLine(string.Format("        Error : {0}", leer.Campo("Mensaje")));
                        }
                    }
                    else
                    {
                        bRegresa = false;
                        System.Console.WriteLine(string.Format("        Error : {0}", leer.MensajeError));
                    }
                }
                else
                {
                    bRegresa = false;
                }
            }
            else
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        protected bool ObtenerConsulta()
        {
            bool bRegresa = false;


            string sSql = "Exec Spp_GenerarConsultaUltimaFechaReplicacion";

            if (!leerExec.Exec(sSql))
            {
                bRegresa = false;
                General.Error.GrabarError(leerExec, "ObtenerConsulta()");
                System.Console.WriteLine(string.Format("        Error : {0}", leerExec.MensajeError));
            }
            else
            {
                if (leerExec.Leer())
                {
                    sSqlConsultaFecha = leerExec.Campo("Consulta");
                    bRegresa = true;
                }
            }

            return bRegresa;
        }


        protected bool GenerarTabla(string Tabla, int Orden, int Orden_Aux)
        {
            ////  
            string sFile = this.GeneraNombreArchivoTabla(Tabla, Orden, Orden_Aux);
            bool bExito = true;

            if (ExisteTabla_A_Procesar(Tabla))
            {
                ////if (PreparaDatosTabla(Tabla, Datos.Obtener))
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
                ////else
                ////{
                ////    bExito = false;
                ////}
            }
            else
            {
                bExito = false;
            }
            return bExito;
        }

        private bool CrearArchivo(string Tabla, string Nombre)
        {
            bool bRegresa = true;
            int iReg = 0, iVueltas = 0; // , iRegistros = 0;
            int iPaquete = 0;
            int iRegistrosBloque = Transferencia.RegistrosSQL;
            int iBloques = 5; 
            string sArchivoDestSql = sRutaObtencion + "" + Nombre + Fg.PonCeros(iPaquete, 8) + ".sql";
            StreamWriter f = null;

            ////if (bReplicarDirecto_SQL)
            {
                iRegistrosBloque = 100;
                iBloques = 5; 
            }

            bRegresa = ObtenerDatosTabla(Tabla);

            if (bRegresa)
            {
                try
                {
                    // File.Delete(sArchivoDestSql);
                    if (leerExec.Registros > 0)
                    {
                        System.Console.WriteLine(string.Format("    Procesando tabla : {0}", Tabla));
                        
                        bRegresa = true;
                        bExistenArchivos = true;
                        while (leerExec.Leer())
                        {
                            if (iVueltas == 0)
                            {
                                iVueltas++;
                                iPaquete++;
                                sArchivoDestSql = sRutaObtencion + "" + Nombre + "_" + Fg.PonCeros(iPaquete, 8) + ".sql";
                                File.Delete(sArchivoDestSql);
                                f = new StreamWriter(sArchivoDestSql, false, Encoding.UTF8); 
                            }

                            f.WriteLine(leerExec.Campo(1));
                            iReg++;
                            iTotalDeRegistros++;

                            ////// Agregar el separador de Registros 
                            if (iReg >= iRegistrosBloque)
                            {
                                f.WriteLine(Transferencia.SQL);
                                f.WriteLine("");
                                iReg = 0;
                                iVueltas++;
                            }

                            ////// Generar archivos de 200 Registros ==> 300-450 Kb 
                            if (iVueltas >= iBloques)
                            {
                                ////// Cerrar el archivo con los Bloques Completos 
                                f.WriteLine(Transferencia.SQL);
                                f.WriteLine("");
                                f.Close();
                                iVueltas = 0;
                            }
                        }

                        if (iVueltas != 0)
                        {
                            ////// Cerrar el archivo en caso de no completar los bloques de Registros 
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

        protected bool Empacar()
        {
            System.Console.WriteLine(""); 
            System.Console.WriteLine("      Generando archivo comprimido.");

            bool bRegresa = false;
            // int iTipo = (int)Destino; 
            string sNombrePaquete = "IntermedPtoVta." + //Fg.PonCeros(iTipo, 2) + sCveRenapo + sCveOrigenArchivo + "Test." + 
                    Transferencia.ExtArchivosGenerados;

            string sFileZip = sRutaObtencion + @"\\" + "IntermedPtoVta." + Transferencia.ExtArchivosGenerados;
            string sFileOut = sRutaObtencion + @"\\" + "IntermedPtoVta." + Transferencia.ExtArchivosGenerados;

            string[] sFiles = Directory.GetFiles(sRutaObtencion, "*.sql");
            clsCriptografia Cripto = new clsCriptografia();
            ZipUtil zip = new ZipUtil();

            sArchivoGenerado = sNombrePaquete;
            bRegresa = zip.Comprimir(sFiles, sFileZip, true);

            System.Console.WriteLine("      Archivo comprimido terminado.");
            return bRegresa;
        }

        private bool GenerarArchivosServidor(DestinoArchivos Destino)
        {
            bool bRegresa = false;
            string sFileOrigen = sRutaObtencion + @"\" + sArchivoGenerado;
            string sFileDestino = "";
            string sNombre = sCveRenapo + sCveOrigenArchivo;
            string sTipo = Fg.PonCeros((int)Destino, 2);
            string sMarcaTiempo = "";   // General.FechaYMD(General.FechaSistema).Replace("/", "") + General.FechaSistemaHora.Substring(0, 5).Replace(":", "");
            string sSSL = "";
            //string sURL = "";

            sMarcaTiempo = General.FechaYMD(General.FechaSistema).Replace("/", "");
            sMarcaTiempo = sMarcaTiempo.Replace("-", "");
            sMarcaTiempo += "-" + General.FechaSistemaHora.Substring(0, 5).Replace(":", "");
            sMarcaTiempo += "___" + General.FechaYMD(dtInicio_Replicacion, "");

            System.Console.WriteLine("");
            System.Console.WriteLine("");
            if (ObtenerDestino())
            {
                pFiles = new ArrayList();
                while (leerDestinos.Leer())
                {
                    try
                    {
                        //// Se obtiene la ruta destino, es unica 
                        sUrlDestino = "http://" + leerDestinos.Campo("Servidor") + "/" + leerDestinos.Campo("WebService") + "/" + leerDestinos.Campo("PaginaWeb");

                        sUrlDestino = string.Format("http{0}://{1}/{2}/{3}.asmx", 
                            sSSL, leerDestinos.Campo("Servidor"), leerDestinos.Campo("WebService"), leerDestinos.Campo("PaginaWeb"));


                        sFileDestino = leerDestinos.Campo("IdEstado") + leerDestinos.Campo("IdFarmacia");
                        sFileDestino += "-" + sTipo + "-" + sMarcaTiempo;
                        sFileDestino += "." + Transferencia.ExtArchivosGenerados;
                        System.Console.WriteLine(string.Format("    Generando archivo : {0} ", sFileDestino));

                        sFileDestino = sRutaObtencion + @"\" + sFileDestino; //// +"-" + sNombre; // +"-" + sCveRenapo + sCveOrigenArchivo;


                        //pFiles.Add(new clsDestinoFiles(leerDestinos.Campo("IdFarmacia"), sURL, sFileDestino));

                        File.Copy(sFileOrigen, sFileDestino);
                        bRegresa = true;
                    }
                    catch (Exception ex)
                    {
                        ex.Source = ex.Source;
                        bRegresa = false;
                        break;
                    }
                }

                if (bRegresa)
                {
                    // Borrar el archivo Origen si se pudo generar una copia por cada Destino disponible. 
                    File.Delete(sFileOrigen);
                }
            }

            return bRegresa;
        }

        protected bool GenerarArchivosClientes(DestinoArchivos Destino)
        {
            bool bRegresa = false;
            string sFileOrigen = sRutaObtencion + @"\" + sArchivoGenerado;
            string sFileDestino = "";

            // string sNombre = sCveRenapo + sCveOrigenArchivo;            
            string sNombre = sClaveRenapoOrigen + sIdFarmaciaOrigen;

            string sTipo = Fg.PonCeros((int)Destino, 2);
            string sMarcaTiempo = "";  // General.FechaYMD(General.FechaSistema).Replace("/", "") + General.FechaSistemaHora.Substring(0, 5).Replace(":", "");              
            string sDirUnidad = "";


            General.FechaSistemaObtener();
            sMarcaTiempo = General.FechaYMD(General.FechaSistema).Replace("/", "");
            sMarcaTiempo = sMarcaTiempo.Replace("-", "");
            sMarcaTiempo += "-" + General.FechaSistemaHora.Substring(0, 5).Replace(":", "");

            if (ObtenerDestino())
            {
                pFiles = new ArrayList();
                if (leerDestinos.Leer())
                {
                    try
                    {

                        // Generar un directorio por cada Unidad 
                        sDirUnidad = sRutaEnviados + @"\" + leerDestinos.Campo("DirDestino");
                        if (!Directory.Exists(sDirUnidad))
                        {
                            Directory.CreateDirectory(sDirUnidad);
                        }

                        if (Destino == DestinoArchivos.Farmacia_A_Farmacia)
                        {
                            //// Copia para el Servidor Central 
                            sFileDestino = sRutaObtencion + @"\" + sNombre + "-" + sCveRenapo + sCveOrigenArchivo + "-" + leerDestinos.Campo("ClaveRenapo") +
                                                            leerDestinos.Campo("IdFarmacia");
                        }


                        //////sFileDestino += "-" + sTipo + "-" + sFolioTS + sMarcaTiempo;
                        sFileDestino += "." + Transferencia.ExtArchivosGenerados;
                        File.Copy(sFileOrigen, sFileDestino);


                        bRegresa = true;
                    }
                    catch (Exception ex)
                    {
                        ex.Source = ex.Source;
                        bRegresa = false;
                        // break;
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
        #region SQL 
        private bool Enviar_SQL()
        {
            bool bRegresa = false;

            GetListaArchivos();

            if (!IntegrarArchivos())
            {
                bRegresa = false; 
            }
            else 
            {
                bRegresa = true; 
            }

            return bRegresa; 
        }

        private bool IntegrarArchivos()
        {
            bool bExito = true;
            string[] sDatos = Directory.GetFiles(sRutaObtencion, "*.sql");
            string sSSL = "";
            string sHost = "";
            FileInfo f; 

            ObtenerDestino();

            System.Console.WriteLine(string.Format(""));
            System.Console.WriteLine(string.Format(""));
            System.Console.WriteLine(string.Format("    Enviando informacion SQL"));


            while (leerDestinos.Leer())
            {
                sSSL = leerDestinos.CampoBool("SSL") ? "s" : "";
                sUrlDestino = string.Format("http{0}://{1}/{2}/{3}.asmx", sSSL, leerDestinos.Campo("Servidor"), leerDestinos.Campo("WebService"), leerDestinos.Campo("PaginaWeb"));
                sUrlDestino = sUrlDestino.ToLower().Replace(".asmx", "") + ".asmx";
                sHost = leerDestinos.Campo("Servidor");

                conWebSQL = new wsCliente.wsCnnCliente();
                conWebSQL.Url = sUrlDestino;

                foreach (string sFile in sDatos)
                {
                    f = new FileInfo(sFile);
                    System.Console.WriteLine(string.Format(" {1} : {0} ", f.Name, sHost));
                    
                    if (!IntegrarArchivo(sFile))
                    {
                        bExito = false;
                        break;
                    }
                    //else
                    //{
                    //    File.Delete(sFile);
                    //}
                }
            }

            return bExito;
        }

        private bool IntegrarArchivo(string Archivo)
        {
            bool bExito = false;
            bool bContinuar = true;
            string sValorExec = "";
            int iPosInicial = 0;
            int iLargoExec = -1;

            try
            {
                string sValor = "";
                StreamReader reader = new StreamReader(Archivo);
                //sValor = " Set DateFormat YMD " + reader.ReadToEnd() + ""; 
                sValor = reader.ReadToEnd() + "";
                reader.Close();

                //////if (cnn.Abrir())
                //////{
                //////    cnn.IniciarTransaccion();

                    bExito = true;
                    while (bContinuar)
                    {
                        //// Cortar la cadena a Exjecutar 
                        iLargoExec = -1;
                        if (sValor.Contains(Transferencia.SQL))
                        {
                            iLargoExec = sValor.IndexOf(Transferencia.SQL, iPosInicial) + Transferencia.SQL.Length + 2;
                        }

                        // Asegurar la funcionalidad con versiones anteriores 
                        if (iLargoExec < 0)
                        {
                            iLargoExec = sValor.Length;
                            bContinuar = false;
                        }

                        sValorExec = " Set DateFormat YMD " + Fg.Left(sValor, iLargoExec);
                        sValor = Fg.Mid(sValor, iLargoExec).Trim();

                        leer.DataSetClase = conWebSQL.ExecuteExt(new DataSet(), DtGeneral.CfgIniReplicacionSQL, sValorExec); 
                        if (leer.SeEncontraronErrores())
                        {
                            bExito = false;
                            break;
                        }

                        if (sValor.Length <= 0)
                        {
                            bContinuar = false;
                        }
                    }

                    //////if (!bExito)
                    //////{
                    //////    cnn.DeshacerTransaccion();
                    //////    Error.GrabarError(leer, Archivo, "IntegrarArchivo");
                    //////}
                    //////else
                    //////{
                    //////    cnn.CompletarTransaccion();
                    //////}
                    //////cnn.Cerrar();
                //////}

                reader = null;
                GC.Collect();
            }
            catch { }

            return bExito;
        }

        private bool GetListaArchivos()
        {
            bool bRegresa = false;
            string[] sFiles = Directory.GetFiles(sRutaObtencion, "*.sql");
            clsLeer listaFiles = new clsLeer();
            pFiles = new ArrayList();

            foreach (string sFile in sFiles)
            {
                FileInfo f = new FileInfo(sFile); 
                object[] obj = { f.Name, f.Length, f.LastWriteTime, f.FullName };
                pFiles.Add(sFile);
            }

            bRegresa = pFiles.Count > 0;

            return bRegresa;
        }
        #endregion SQL 

        #region Revision de Comunicaciones
        DataTable dtPing;
        int iRevisandoConexiones = 0;

        private void ThreadRevisarConexion(object Registro)
        {
            clsPing ping = new clsPing();
            DllTransferenciaSoft.wsCliente.wsCnnCliente Cliente = new DllTransferenciaSoft.wsCliente.wsCnnCliente();
            clsLeer leerRow = new clsLeer();
            string sURL = "";
            int iRow = (int)Registro;
            string sSSL = ""; 
            string sError = "";
            bool bConexion = false; 

            try
            {
                DataRow dtRow = dtPing.Rows[iRow];
                leerRow.DataRowClase = dtRow;
                leerRow.Leer();

                // sSvr = dtPing.Rows[iRow]["Servidor"].ToString();
                //sURL = "http://" + dtRow["Servidor"].ToString() + "/" + dtRow["WebService"].ToString() + "/" + dtRow["PaginaWeb"].ToString();
                sSSL = leerRow.CampoBool("SSL") ? "s" : "";

                sURL = string.Format("http{0}://{1}/{2}/{3}.asmx", sSSL, leerRow.Campo("Servidor"), leerRow.Campo("WebService"), leerRow.Campo("PaginaWeb"));
                sURL = sURL.ToLower().Replace(".asmx", "") + ".asmx";

                Cliente.Url = sURL;
                Cliente.Timeout = 1000 * (45);

                System.Console.WriteLine(string.Format("Verificando conexión con : {0}", sURL));

                bConexion = Cliente.TestConection();

                dtPing.Rows[iRow]["EnLinea"] = bConexion;

                System.Console.WriteLine(string.Format("Conexión con : {0}, {1}", sURL, bConexion.ToString()));
            }
            catch (Exception ex)
            {
                sError = ex.Message;
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
            leerDestinos.RegistroActual = 0;
            foreach (DataRow row in leerDestinos.DataTableClase.Rows)
            {
                Thread t = new Thread(this.ThreadRevisarConexion);
                iRevisandoConexiones++;
                t.Name = "File_" + (iReg + 1).ToString();
                t.Start(iReg);
                iReg++;
            }

            // Esperar a que se revisen todoas las conexiones 
            while (iRevisandoConexiones != 0)
            {
                Thread.Sleep(50);
            }

            leerDestinos.DataTableClase = dtPing;
        }
        #endregion Revision de Comunicaciones

        private bool EnviarDetallesServidorCliente(string ArchivoCfg) //, string RutaArchivo, string NombreDestino )
        {
            bool bRegresa = false;
            string sDestino = "";
            string sPatron = "*." + Transferencia.ExtArchivosGenerados;
            string sError = ""; 
            // string sURL = "", sFile = "";

            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("Iniciando proceso de envió.......");

            pFiles = new ArrayList();
            iTipoDeTablasEnvio = 4;     //// Enviar informacion a Servidor y Clientes  
            if (ObtenerDestino())
            {
                RevisarConexiones();
                // EnviarCliente = new DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion(TipoServicio.Cliente);

                // sRutaObtencion 
                foreach (string f in Directory.GetFiles(sRutaObtencion, sPatron))
                {
                    pFiles.Add(new clsDestinoFiles(f, leerDestinos, true));
                }

                iNumProcesosEnEjecucion = 0;
                foreach (clsDestinoFiles d in pFiles)
                {
                    //////System.Console.WriteLine(string.Format("Inicia proceso de envió de : {0}", d.ArchivoDestino));
                    // Asegurar el envio a Unidades que se encuentren en Linea 
                    if (d.EnLinea)
                    {
                        // Generar un directorio por cada Unidad 
                        sDestino = sRutaEnviados + @"\" + d.ClaveDestino;
                        if (!Directory.Exists(sDestino))
                        {
                            Directory.CreateDirectory(sDestino);
                        }

                        d.ArchivoCfg = ArchivoCfg;
                        ////Thread t = new Thread(this.ThreadEnviarArchivo);
                        ////t.Name = d.ArchivoDestino;
                        ////iNumProcesosEnEjecucion++;
                        ////t.Start(d);

                        iNumProcesosEnEjecucion++;
                        ThreadEnviarArchivo(d); 
                    }
                    else
                    {
                        sError ="No fue posible establecer comunicación con : " + d.UrlDestino + "..." + d.ArchivoDestino;
                        System.Console.WriteLine(sError);
                        Error.GrabarError(sError, "EnviarDetallesServidorCliente");
                    }
                }

                // Mantener el proceso detenido hasta que finalizen todos los envios de información. 
                while (iNumProcesosEnEjecucion != 0)
                {
                    Thread.Sleep(100);
                }
            }
            iTipoDeTablasEnvio = 1;
            return bRegresa;
        }

        private void ThreadEnviarArchivo(object Archivo)
        {
            clsDestinoFiles d = (clsDestinoFiles)Archivo;
            DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion
                EnviarInformacion = new DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion(TipoServicio.OficinaCentralRegional);

            string sDestino = "";
            bool bRegresa = false;
            string sFile = DtGeneral.CfgIniReplicacionSQL;
            //bRegresa = true; 

            //iNumProcesosEnEjecucion++; 
            try
            {
                // sDestino = sRutaEnviados + @"\" + d.ArchivoDestino;
                sDestino = sRutaEnviados + @"\" + d.ClaveDestino + @"\" + d.ArchivoDestino;
                EnviarInformacion.Url = d.UrlDestino;
                //EnviarInformacion.Url = "http://LapJesus:8080/wsTest/wsoficinacentral.asmx"; 

                //////EnviarInformacion.FTP_Server = sFTP_Servidor;
                //////EnviarInformacion.FTP_Usuario = sFTP_Usuario;
                //////EnviarInformacion.FTP_Password = sFTP_Password;
                EnviarInformacion.FTP_Medida_Files = tpTamañoFiles;
                EnviarInformacion.FTP_Tamaño_File = iMB_File;

                bRegresa = EnviarInformacion.Enviar_SQL(sFile, d.ArchivoOrigen, d.ArchivoDestino, d.Estado, d.Farmacia);
                //

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
