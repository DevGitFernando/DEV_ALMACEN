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
    public class clsCliente
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
        bool bEsProcesoDeCentral = false;

        protected int iTipoDeTablasEnvio = 1;
        // bool bEnviandoArchivos = false;
        protected string sIdEstadoEnvio = "";
        protected string sIdFarmaciaEnvio = "";

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

        string sFTP_Servidor = "";
        string sFTP_Usuario = "";
        string sFTP_Password = "";

        protected string sRutaPedidosCedis = "";
        protected string sIdEmpresaPedido = "";
        protected string sIdEstadoPedido = "";
        protected string sIdFarmaciaPedido = "";
        protected string sFolioPedido = "";
        protected bool bEsProcesoDePedidos = false;

        string sFolioTS = "";
        bool bTSConfirmacion = false;

        bool bReplicacionPorPeriodos = false;
        int iDiasRevision_FechaControl = 0;
        int iTotalDeRegistros = 0;
        int iMB_File = 5;
        TamañoFiles tpTamañoFiles = TamañoFiles.MB;

        /// <summary> 
        /// Controla la Ejecución de los Procesos que se ejecutan en Hilos, 
        /// se asegura de no dar por terminado el envio de información. 
        /// </summary>
        private int iNumProcesosEnEjecucion = 0;
        #endregion Declaracion de variables

        #region Constructor y Destructor
        public clsCliente(string ArchivoCfg, clsDatosConexion DatosConexion)
        {
            this.sArchivoCfg = ArchivoCfg;
            this.datosCnn = DatosConexion;
            cnn = new clsConexionSQL(this.datosCnn);
            Error = new clsGrabarError(Transferencia.Modulo + ".ObtenerInformacion", Transferencia.Version, "clsCliente");
            Error.MostrarErrorAlGrabar = false;

            // Preparar acceso a datos 
            leerCat = new clsLeer(ref cnn);
            leerDet = new clsLeer(ref cnn);
            leerExec = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            leerDestinos = new clsLeer(ref cnn);
            leerTransferencias = new clsLeer(ref cnn);
        }
        #endregion Constructor y Destructor

        #region Obtener Configuraciones
        private bool ConfigObtencion_FTP()
        {
            bool bRegresa = false;
            int iLargo = 0;
            clsCriptografo crypto = new clsCriptografo();

            string sSql = string.Format("Select * From CFGC_ConfigurarConexion (NoLock) ");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ConfigObtencion_FTP()");
            }
            else
            {
                bRegresa = leer.Leer();
                {
                    // Informacion de FTP 
                    sFTP_Servidor = leer.Campo("ServidorFTP");
                    sFTP_Usuario = leer.Campo("UserFTP");
                    sFTP_Password = leer.Campo("PasswordFTP");

                    try
                    {
                        sFTP_Password = crypto.PasswordDesencriptar(leer.Campo("PasswordFTP")).Substring(iLargo);
                    }
                    catch
                    {
                        sFTP_Password = "";
                    }
                }
            }
            return bRegresa;
        }

        private bool GetRutaObtencion()
        {
            bool bRegresa = true;
            string sSql = "Select * From CFGC_ConfigurarObtencion (NoLock) ";

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GetRutaObtencion()");
                bRegresa = false;
            }
            else
            {
                if (!leer.Leer())
                {

                    if (bEsProcesoDeCentral)
                    {
                        sRutaObtencion = Application.StartupPath + @"\\RUTA_PAQUETE_DE_DATOS\" + sRutaTransferencias;
                        sRutaEnviados = sRutaObtencion; // Application.StartupPath + @"\\RUTA_TRANSFERENCIAS\"; 
                    }
                    else
                    {
                        bRegresa = false;
                        General.msjError("No se encontro la información de configuración de envio de información, reportarlo al departamento de sistemas.");
                    }
                }
                else
                {
                    bReplicacionPorPeriodos = leer.CampoBool("TipoReplicacion");
                    iDiasRevision_FechaControl = leer.CampoInt("DiasRevision");

                    tpTamañoFiles = (TamañoFiles)leer.CampoInt("TipoDePaquete");
                    iMB_File = leer.CampoInt("TamañoDePaquete");

                    if (!bEsProcesoDeTranserencias)
                    {
                        sRutaObtencion = leer.Campo("RutaUbicacionArchivos") + @"\\" + sRutaTransferencias;
                        sRutaEnviados = leer.Campo("RutaUbicacionArchivosEnviados") + @"\\";
                    }
                    else
                    {
                        sRutaObtencion = Application.StartupPath + @"\\RUTA_TRANSFERENCIAS\" + sRutaTransferencias;
                        sRutaEnviados = sRutaObtencion; // Application.StartupPath + @"\\RUTA_TRANSFERENCIAS\"; 
                    }


                    if (bEsProcesoDeCentral)
                    {
                        sRutaObtencion = Application.StartupPath + @"\\RUTA_PAQUETE_DE_DATOS\" + sRutaTransferencias;
                        sRutaEnviados = sRutaObtencion; // Application.StartupPath + @"\\RUTA_TRANSFERENCIAS\"; 
                    }

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

        protected string GeneraNombreArchivoTabla(string Tabla, int Orden)
        {
            string sRegresa = "Datos_" + Fg.PonCeros(Orden, 4) + "_" + Tabla;
            return sRegresa.ToUpper().Trim();
        }

        private bool ExistenTablasEnvio(int Tipo)
        {
            bool bRegresa = true;
            string sTipo = " CFGC_EnvioDetalles ";

            iTipoDeTablasEnvio = Tipo;
            if (Tipo == 2)
                sTipo = " CFGC_EnvioDetallesTrans ";

            string sSql = string.Format(" Select * From {0} (NoLock) Where Status = 'A' Order By IdOrden, NombreTabla ", sTipo);

            // leerCat = new clsLeer(ref cnn);
            if (!leerCat.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerCat, "ExistenTablasEnvio()");
            }

            return bRegresa;
        }

        protected bool ExisteTabla_A_Procesar(string Tabla)
        {
            bool bRegresa = false;
            string sSql = string.Format("Select * From Sysobjects (NoLock) Where Name = '{0}' and xType = 'U' ", Tabla);

            bRegresa = leer.Exec(sSql);
            bRegresa = leer.Leer();
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
                DateTime dtInicio = General.FechaSistema.AddDays(-1 * iDiasRevision_FechaControl);
                DateTime dtFin = General.FechaSistema;

                sFiltro = string.Format(" Where convert(varchar(10), FechaControl, 120) between '{0}' and '{1}' ",
                    General.FechaYMD(dtInicio, "-"), General.FechaYMD(dtFin, "-"));
                sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ {1} ], '0' ", Tabla, sFiltro);

            }

            ////// Las transferencias se envian con Actualizado = 0 para su envio inmediato 
            // if ( iTipoDeTablasEnvio == 2 ) 
            //     sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ Where Actualizado = 2 ], '0' ", Tabla);

            ////// Las transferencias se envian con Actualizado = 0 para su envio inmediato 
            if (iTipoDeTablasEnvio == 2)
            {
                if (bTSConfirmacion)
                {
                    sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ Where Actualizado = 2 and IdEstadoRecibe = '{1}' and IdFarmaciaRecibe = '{2}' and FolioTransferencia = '{3}' ], '0' ",
                        Tabla, sIdEstadoEnvio, sIdFarmaciaEnvio, sFolioTS);
                }
                else
                {
                    sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ Where Actualizado = 2 and IdEstadoRecibe = '{1}' and IdFarmaciaRecibe = '{2}' ], '0' ",
                        Tabla, sIdEstadoEnvio, sIdFarmaciaEnvio);
                }
            }

            if (!leerExec.Exec(Tabla, sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerExec, "ObtenerDatosTabla()");
            }
            return bRegresa;
        }

        private bool ObtenerDestino()
        {
            bool bRegresa = true;
            string sSql = string.Format("Select C.Servidor, C.WebService, C.PaginaWeb, C.Status, C.IdEstado, C.IdFarmacia, " +
                " (IsNull(E.ClaveRenapo, '00') + C.IdFarmacia) as FarmaciaOrigen, cast(0 as bit) as EnLinea  " +
                " From CFGC_ConfigurarConexion C (NoLock) " +
                " Left Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) " +
                " Where C.Status = 'A' and C.IdEstado = '{0}' ",
                DtGeneral.EstadoConectado);

            if (iTipoDeTablasEnvio == 2 || iTipoDeTablasEnvio == 3)
            {
                sSql = string.Format("Select Distinct C.IdEstado, C.IdFarmacia, C.Servidor, C.WebService, C.PaginaWeb, " +
                                " E.ClaveRenapo, (E.ClaveRenapo + C.IdFarmacia) as DirDestino, cast(0 as bit) as EnLinea  " +
                                " From CFGS_ConfigurarConexiones C (NoLock) " +
                                " Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) " +
                                " Where C.IdEstado = '{0}' and C.IdFarmacia = '{1}' and C.Status = 'A' Order by C.IdEstado, C.IdFarmacia ",
                                sIdEstadoEnvio, sIdFarmaciaEnvio);

                sSql = string.Format("Select Distinct C.IdEstado, C.IdFarmacia, C.Servidor, C.WebService, C.PaginaWeb, " +
                                " E.ClaveRenapo, (E.ClaveRenapo + C.IdFarmacia) as DirDestino, cast(0 as bit) as EnLinea  " +
                                " From CFGS_ConfigurarConexiones C (NoLock) " +
                                " Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) " +
                                " Where C.Status = 'A' and C.IdEstado = '{0}' and C.IdFarmacia = '{1}' Order by C.IdEstado, C.IdFarmacia ",
                                DtGeneral.EstadoConectado, sIdFarmaciaEnvio);
            }

            // Para envio de Informacion 
            if (iTipoDeTablasEnvio == 4)
            {
                sSql = string.Format("Select Distinct C.IdEstado, C.IdFarmacia, C.Servidor, C.WebService, C.PaginaWeb, " +
                                " E.ClaveRenapo, (E.ClaveRenapo + C.IdFarmacia) as DirDestino, cast(0 as bit) as EnLinea  " +
                                " From CFGS_ConfigurarConexiones C (NoLock) " +
                                " Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) " +
                                " Where C.Status = 'A' and C.IdEstado = '{0}' and C.IdFarmacia = '{1}' Order by C.IdEstado, C.IdFarmacia ",
                                DtGeneral.EstadoConectado, sIdServer);
            }

            if (!leerDestinos.Exec(sSql))
            {
                bRegresa = false;
                General.Error.GrabarError(leerDestinos, "ObtenerDestino()");
            }
            else
            {
                if (iTipoDeTablasEnvio == 3 || iTipoDeTablasEnvio == 4)
                {
                    DataTable dtPaso = leerDestinos.DataTableClase;
                    object[] obj = { DtGeneral.ClaveRENAPO, sIdServer,
                                       sServidor, sWebService, sPaginaWeb, sCveRenapo, DtGeneral.ClaveRENAPO + sIdServer };
                    dtPaso.Rows.Add(obj);

                    leerDestinos.DataTableClase = dtPaso;
                    ////while (leerDestinos.Leer())
                    ////{ 
                    ////}

                }
                else
                {
                    leerDestinos.Leer();
                    // Se obtiene la ruta destino, es unica 
                    sServidor = leerDestinos.Campo("Servidor");
                    sWebService = leerDestinos.Campo("WebService");
                    sPaginaWeb = leerDestinos.Campo("PaginaWeb");

                    sUrlDestino = "http://" + sServidor + "/" + sWebService + "/" + sPaginaWeb;
                    sUrlDestino = sUrlDestino.ToLower().Replace(".asmx", "") + ".asmx";
                    leerDestinos.RegistroActual = 1;

                    sCveRenapo = DtGeneral.ClaveRENAPO;
                    sCveOrigenArchivo = sIdServer;
                }
            }

            return bRegresa;
        }

        #endregion Funciones y Procedimientos Privados

        #region Generar archivos
        public bool GenerarArchivos()
        {
            bool bRegresa = true;
            bExistenArchivos = false;

            iTotalDeRegistros = 0;
            ObtenerDestino();
            if (GetRutaObtencion())
            {
                this.DetallesInformacion(1); // Tablas de Detalles de movimentos 
                if (bExistenArchivos)
                {
                    Empacar();
                    GenerarArchivosServidor(DestinoArchivos.Farmacia_A_OficinaCentral);
                }

                this.Transferencias();
                ////bExistenArchivos = false;
                ////this.DetallesInformacion(2); // Tablas de Detalles de movimentos de Transferencias de Salida ( Proceso Especial )
                ////if (bExistenArchivos)
                ////{
                ////    Empacar();
                ////    GenerarArchivosServidor(DestinoArchivos.Farmacia_A_Farmacia);
                ////}
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
            bool bRegresa = false;
            //string sFile = "";
            string sTabla = "";
            int iOrden = 0;

            //////////////// 2011-01-27_1250 Posible error de Obtencion detectado, cada tabla es transaccional.... 
            ExistenTablasEnvio(Tipo);
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

        public bool AltaProductos(string IdProducto_Inicial, string IdProducto_Final, int Tipo)
        {
            bool bRegresa = false;
            bEsProcesoDeTranserencias = false;
            bEsProcesoDeCentral = true;
            sRutaTransferencias = @"\\Catalogos\\";

            if (GetRutaObtencion())
            {
                bRegresa = AltaProductos_GetDatos(IdProducto_Inicial, IdProducto_Final, Tipo);
            }

            bEsProcesoDeCentral = false;

            return bRegresa;
        }

        private bool AltaProductos_GetDatos(string IdProducto_Inicial, string IdProducto_Final, int Tipo)
        {
            string sSql = "";
            bool bRegresa = true;
            int iReg = 0, iVueltas = 0; // , iRegistros = 0;
            int iPaquete = 0;
            string sArchivoDestSql = ""; // sRutaObtencion + "" + Nombre + Fg.PonCeros(iPaquete, 6) + ".sql";
            StreamWriter f = null;
            DataSet dtsResultado = new DataSet();
            DataTable dtResultado = new DataTable();
            int iConfigurado = 0;

            sSql = string.Format("Exec spp_PRCS_GenerarInformacion_AltaProductos @IdProducto_Inicial = '{0}', @IdProducto_Final = '{1}', @Tipo = '{2}' ",
                IdProducto_Inicial, IdProducto_Final, Tipo);

            bExistenArchivos = false;
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                try
                {
                    foreach (DataTable dt in leer.DataSetClase.Tables)
                    {
                        if (iConfigurado == 0)
                        {
                            iConfigurado++;
                            dtsResultado.Tables.Add(dt.Clone());
                        }

                        dtsResultado.Tables[0].Merge(dt);
                    }

                    leerExec.DataSetClase = dtsResultado;
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
                                sArchivoDestSql = sRutaObtencion + "" + "ALTA_PRODUCTOS" + "_" + Fg.PonCeros(iPaquete, 6) + ".sql";
                                File.Delete(sArchivoDestSql);
                                f = new StreamWriter(sArchivoDestSql, false, Encoding.UTF8);
                            }

                            f.WriteLine(leerExec.Campo(1));
                            iReg++;
                            iTotalDeRegistros++;

                            ////// Agregar el separador de Registros 
                            if (iReg >= Transferencia.RegistrosSQL)
                            {
                                f.WriteLine(Transferencia.SQL);
                                f.WriteLine("");
                                iReg = 0;
                                iVueltas++;
                            }

                            ////// Generar archivos de 200 Registros ==> 300-450 Kb 
                            if (iVueltas >= 5)
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

            if (bExistenArchivos)
            {
                bRegresa = true;
                Empacar(); // DestinoArchivos.Farmacia_A_Farmacia
            }

            return bRegresa;
        }
        public void TransferenciasAutomaticas(string IdEstadoDestino, string IdFarmaciaDestino)
        {
            TransferenciasAutomaticas(IdEstadoDestino, IdFarmaciaDestino, false);
        }

        public void TransferenciasAutomaticas(string IdEstadoDestino, string IdFarmaciaDestino, bool EsDevolucion)
        {
            bEsProcesoDeTranserencias = true;
            sRutaTransferencias = @"\\Transferencias\\";

            ObtenerDestino();
            sCveOrigenArchivo = IdFarmaciaDestino; // Poner la Farmacia Destino 

            if (GetRutaObtencion())
            {
                Transferencias(IdEstadoDestino, IdFarmaciaDestino, EsDevolucion);
            }

            bEsProcesoDeTranserencias = false;
        }

        private bool Transferencias()
        {
            return Transferencias("", "");
        }

        private bool Transferencias(string IdEstadoDestino, string IdFarmaciaDestino)
        {
            return Transferencias(IdEstadoDestino, IdFarmaciaDestino, false);
        }

        private bool Transferencias(string IdEstadoDestino, string IdFarmaciaDestino, bool EsDevolucion)
        {
            bool bRegresa = false;
            // string sFile = "";
            string sTabla = "";
            int iOrden = 0;

            iTipoDeTablasEnvio = 2;
            BuscarTransferenciasPendientes(IdEstadoDestino, IdFarmaciaDestino, EsDevolucion);
            while (leerTransferencias.Leer())
            {
                sIdEstadoEnvio = leerTransferencias.Campo("IdEstadoRecibe");
                sIdFarmaciaEnvio = leerTransferencias.Campo("IdFarmaciaRecibe");

                sIdEstadoOrigen = leerTransferencias.Campo("IdEstadoEnvia");
                sIdFarmaciaOrigen = leerTransferencias.Campo("IdFarmaciaEnvia");
                sClaveRenapoOrigen = leerTransferencias.Campo("ClaveRenapo");

                ////////iTipoDeTablasEnvio = 2;
                ////////ExistenTablasEnvioTransferencia();
                ////////while (leerCat.Leer())
                ////////{
                ////////    sTabla = leerCat.Campo("NombreTabla");
                ////////    iOrden = leerCat.CampoInt("IdOrden");
                ////////    GenerarTabla(sTabla, iOrden);
                ////////    bRegresa = true;
                ////////}

                //////////////// 2011-01-27_1250 Posible error de Obtencion detectado, cada tabla es transaccional.... 
                iTipoDeTablasEnvio = 2;
                ExistenTablasEnvioTransferencia(EsDevolucion);
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

                iTipoDeTablasEnvio = 3;
                if (bExistenArchivos)
                {
                    Empacar(); // DestinoArchivos.Farmacia_A_Farmacia
                    GenerarArchivosClientes(DestinoArchivos.Farmacia_A_Farmacia);
                }
            }

            // Asegurar que al Enviar los archivos estos se envien a su Destino Respectivo 
            iTipoDeTablasEnvio = 1;

            return bRegresa;
        }

        private bool BuscarTransferenciasPendientes(string IdEstadoDestino, string IdFarmaciaDestino)
        {
            return BuscarTransferenciasPendientes(IdEstadoDestino, IdFarmaciaDestino, false);
        }

        private bool BuscarTransferenciasPendientes(string IdEstadoDestino, string IdFarmaciaDestino, bool EsDevolucion)
        {
            bool bRegresa = true;
            string sFiltroFarmaciaDestino = "";
            string sTabla = "TransferenciasEnvioEnc";

            if (EsDevolucion)
            {
                sTabla = "DevolucionTransferenciasEnvioEnc";
            }

            if (IdFarmaciaDestino != "")
            {
                if (IdEstadoDestino != "")
                {
                    sFiltroFarmaciaDestino += string.Format(" and T.IdEstadoRecibe = '{0}' ", IdEstadoDestino);
                }

                sFiltroFarmaciaDestino += string.Format(" and T.IdFarmaciaRecibe = '{0}' ", IdFarmaciaDestino);
            }

            string sSql = string.Format(" Select Distinct T.IdEstadoRecibe, T.IdFarmaciaRecibe, " +
                " T.IdEstadoEnvia, T.IdFarmaciaEnvia, F.ClaveRenapo " +
                " From {3} T (NoLock) " +
                " Inner Join vw_Farmacias F (NoLock) On ( T.IdEstadoEnvia = F.IdEstado and T.IdFarmaciaEnvia = F.IdFarmacia )  " +
                " Where T.Status in ( 'A',  'T' ) and T.IdEstadoEnvia = '{0}' and T.IdFarmaciaEnvia = '{1}' {2} ",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFiltroFarmaciaDestino, sTabla);

            if (!leerTransferencias.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerTransferencias, "BuscarTransferenciasPendientes()");
            }

            return bRegresa;
        }

        private bool ExistenTablasEnvioTransferencia()
        {
            return ExistenTablasEnvioTransferencia(false);
        }

        private bool ExistenTablasEnvioTransferencia(bool EsDevolucion)
        {
            bool bRegresa = true;
            string sSql = "Select * From CFGC_EnvioDetallesTrans (NoLock) Where Status = 'A' Order By IdOrden, NombreTabla ";

            if (EsDevolucion)
            {
                sSql = "Select * From CFGC_EnvioDetallesTransDev (NoLock) Where Status = 'A' Order By IdOrden, NombreTabla ";
            }

            // leerCat = new clsLeer(ref cnn);
            if (!leerCat.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerCat, "ExistenTablasEnvioTransferencia()");
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

        protected bool GenerarTabla(string Tabla, int Orden)
        {
            ////  
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
            string sArchivoDestSql = sRutaObtencion + "" + Nombre + Fg.PonCeros(iPaquete, 6) + ".sql";
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
                                sArchivoDestSql = sRutaObtencion + "" + Nombre + "_" + Fg.PonCeros(iPaquete, 6) + ".sql";
                                File.Delete(sArchivoDestSql);
                                f = new StreamWriter(sArchivoDestSql, false, Encoding.UTF8);
                            }

                            f.WriteLine(leerExec.Campo(1));
                            iReg++;
                            iTotalDeRegistros++;

                            ////// Agregar el separador de Registros 
                            if (iReg >= Transferencia.RegistrosSQL)
                            {
                                f.WriteLine(Transferencia.SQL);
                                f.WriteLine("");
                                iReg = 0;
                                iVueltas++;
                            }

                            ////// Generar archivos de 200 Registros ==> 300-450 Kb 
                            if (iVueltas >= 5)
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
            bool bRegresa = false;
            // int iTipo = (int)Destino; 
            string sNombrePaquete = "IntermedPtoVta." + //Fg.PonCeros(iTipo, 2) + sCveRenapo + sCveOrigenArchivo + "Test." + 
                    Transferencia.ExtArchivosGenerados;

            string sFileZip = sRutaObtencion + @"\\" + "IntermedPtoVta." + Transferencia.ExtArchivosGenerados;
            string sFileOut = sRutaObtencion + @"\\" + "IntermedPtoVta." + Transferencia.ExtArchivosGenerados;

            string[] sFiles = Directory.GetFiles(sRutaObtencion, "*.sql");
            clsCriptografia Cripto = new clsCriptografia();
            ZipUtil zip = new ZipUtil();

            if (bEsProcesoDeCentral)
            {
                string sMarcaTiempo = "";
                DateTime dtFechaHora = DateTime.Now;
                sMarcaTiempo = General.FechaYMD(dtFechaHora).Replace("/", "");
                sMarcaTiempo = sMarcaTiempo.Replace("-", "");
                sMarcaTiempo += "_" + General.Hora(dtFechaHora, "");

                sFileZip = string.Format(@"{0}\{1}_{2}.{3}", sRutaObtencion, "ALTA_DE_PRODUCTOS__", sMarcaTiempo, Transferencia.ExtArchivosGenerados);
            }

            sArchivoGenerado = sNombrePaquete;
            bRegresa = zip.Comprimir(sFiles, sFileZip, true);

            //if (zip.Comprimir(sFiles, sFileZip, true))
            //{
            //    bRegresa = Cripto.EncriptarArchivo(sFileZip, sFileOut, true);
            //}

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
            //string sURL = "";

            sMarcaTiempo = General.FechaYMD(General.FechaSistema).Replace("/", "");
            sMarcaTiempo = sMarcaTiempo.Replace("-", "");
            sMarcaTiempo += "-" + General.FechaSistemaHora.Substring(0, 5).Replace(":", "");

            if (ObtenerDestino())
            {
                pFiles = new ArrayList();
                while (leerDestinos.Leer())
                {
                    try
                    {
                        //// Se obtiene la ruta destino, es unica 
                        sUrlDestino = "http://" + leerDestinos.Campo("Servidor") + "/" + leerDestinos.Campo("WebService") + "/" + leerDestinos.Campo("PaginaWeb");

                        //// sURL = sURL.ToLower().Replace(".asmx", "") + ".asmx";
                        sFileDestino = sRutaObtencion + @"\" + leerDestinos.Campo("FarmaciaOrigen") + "-" + sNombre; // +"-" + sCveRenapo + sCveOrigenArchivo;
                        sFileDestino += "-" + sTipo + "-" + sMarcaTiempo;
                        sFileDestino += "." + Transferencia.ExtArchivosGenerados;

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

        /// <summary>
        /// Envio de Transferencias 
        /// </summary>
        /// <param name="Destino"></param>
        /// <returns></returns>
        protected bool GenerarArchivosClientes(DestinoArchivos Destino)
        {
            bool bRegresa = false;
            string sFileOrigen = sRutaObtencion + @"\" + sArchivoGenerado;
            string sFileDestino = "";

            // string sNombre = sCveRenapo + sCveOrigenArchivo;            
            string sNombre = sClaveRenapoOrigen + sIdFarmaciaOrigen;

            string sTipo = Fg.PonCeros((int)Destino, 2);
            string sMarcaTiempo = "";  // General.FechaYMD(General.FechaSistema).Replace("/", "") + General.FechaSistemaHora.Substring(0, 5).Replace(":", "");              
            // string sURL = "";
            string sDirUnidad = "";

            if (bTSConfirmacion)
            {
                sFolioTS = sFolioTS + "-";
            }

            General.FechaSistemaObtener();
            sMarcaTiempo = General.FechaYMD(General.FechaSistema).Replace("/", "");
            sMarcaTiempo = sMarcaTiempo.Replace("-", "");
            sMarcaTiempo += "-" + General.FechaSistemaHora.Substring(0, 5).Replace(":", "");


            // Retrasar la marca de tiempo 
            if (sRutaTransferencias != "")
            {
                sMarcaTiempo += "_" + Fg.PonCeros(General.FechaSistema.Millisecond.ToString(), 4);
                System.Threading.Thread.Sleep(1500);
            }

            if (ObtenerDestino())
            {
                pFiles = new ArrayList();
                // while (leerDestinos.Leer())
                if (leerDestinos.Leer())
                {
                    try
                    {
                        //sURL = "http://" + leerDestinos.Campo("Servidor") + "/" + leerDestinos.Campo("WebService") + "/" + leerDestinos.Campo("PaginaWeb");
                        //sURL = sURL.ToLower().Replace(".asmx", "") + ".asmx"; 

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

                        if (Destino == DestinoArchivos.Farmacia_A_Almacen)
                        {
                            //// Copia para el Servidor Central 
                            sFileDestino = sRutaObtencion + @"\" + leerDestinos.Campo("ClaveRenapo") +
                                                            leerDestinos.Campo("IdFarmacia");
                        }


                        if (Destino == DestinoArchivos.Almacen_A_Farmacia)
                        {
                            sFileDestino = sRutaObtencion + @"\" + leerDestinos.Campo("ClaveRenapo") +
                                                            leerDestinos.Campo("IdFarmacia");
                            sFolioTS = "PD-" + sFolioPedido + "-";
                        }


                        sFileDestino += "-" + sTipo + "-" + sFolioTS + sMarcaTiempo;
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
                Cliente.Timeout = 1000 * (45);

                dtPing.Rows[iRow]["EnLinea"] = Cliente.TestConection();
            }
            catch { }
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
            while (iRevisandoConexiones != 0)
            {
                Thread.Sleep(50);
            }

            leerDestinos.DataTableClase = dtPing;
        }
        #endregion Revision de Comunicaciones

        private bool EnviarDetalles(string ArchivoCfg) //, string RutaArchivo, string NombreDestino )
        {
            bool bRegresa = false;
            ////string sDestino = "";
            ////pFiles = new ArrayList();

            ////if (ObtenerDestino())
            ////{
            ////    string sURL, sFile;
            ////    EnviarCliente = new DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion(TipoServicio.OficinaCentral);

            ////    // sRutaObtencion
            ////    foreach (string f in Directory.GetFiles(sRutaObtencion))
            ////    {
            ////        pFiles.Add(new clsDestinoFiles(f, sUrlDestino));
            ////    }

            ////    foreach (clsDestinoFiles d in pFiles)
            ////    {
            ////        try
            ////        {
            ////            sDestino = sRutaEnviados + @"\\" + d.ArchivoDestino;
            ////            EnviarCliente.Url = d.UrlDestino;
            ////            bRegresa = EnviarCliente.Enviar(ArchivoCfg, d.ArchivoOrigen, d.ArchivoDestino, d.Farmacia);

            ////            try
            ////            {
            ////                if (bRegresa)
            ////                {
            ////                    File.Delete(sDestino);
            ////                    File.Move(d.ArchivoOrigen, sDestino);
            ////                }
            ////            }
            ////            catch { }
            ////        }
            ////        catch { }
            ////    }

            ////}

            return bRegresa;
        }

        private bool EnviarDetallesServidorCliente(string ArchivoCfg) //, string RutaArchivo, string NombreDestino )
        {
            bool bRegresa = false;
            string sDestino = "";
            string sPatron = "*." + Transferencia.ExtArchivosGenerados;
            // string sURL = "", sFile = "";

            pFiles = new ArrayList();
            iTipoDeTablasEnvio = 4;     //// Enviar informacion a Servidor y Clientes  
            ConfigObtencion_FTP();
            if (ObtenerDestino())
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
                        if (!Directory.Exists(sDestino))
                        {
                            Directory.CreateDirectory(sDestino);
                        }

                        d.ArchivoCfg = ArchivoCfg;
                        Thread t = new Thread(this.ThreadEnviarArchivo);
                        t.Name = d.ArchivoDestino;
                        iNumProcesosEnEjecucion++;
                        t.Start(d);
                    }
                    else
                    {
                        Error.GrabarError("No fue posible establecer comunicación con : " + d.UrlDestino + "..." + d.ArchivoDestino, "EnviarDetallesServidorCliente");
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
            string sFile = DtGeneral.CfgIniOficinaCentral;
            //bRegresa = true; 

            //iNumProcesosEnEjecucion++; 
            try
            {
                if (!d.ClaveDestino.Contains(DtGeneral.ClaveRENAPO + sIdServer))
                {
                    EnviarInformacion.Destino = TipoServicio.Cliente;
                    sFile = DtGeneral.CfgIniPuntoDeVenta;
                }

                // sDestino = sRutaEnviados + @"\" + d.ArchivoDestino;
                sDestino = sRutaEnviados + @"\" + d.ClaveDestino + @"\" + d.ArchivoDestino;
                EnviarInformacion.Url = d.UrlDestino;
                //EnviarInformacion.Url = "http://LapJesus:8080/wsTest/wsoficinacentral.asmx"; 

                EnviarInformacion.FTP_Server = sFTP_Servidor;
                EnviarInformacion.FTP_Usuario = sFTP_Usuario;
                EnviarInformacion.FTP_Password = sFTP_Password;
                EnviarInformacion.FTP_Medida_Files = tpTamañoFiles;
                EnviarInformacion.FTP_Tamaño_File = iMB_File;

                bRegresa = EnviarInformacion.Enviar(sFile, d.ArchivoOrigen, d.ArchivoDestino, DtGeneral.EstadoConectado, d.Farmacia);
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

        #region Transferencias_Integradas
        private bool BuscarTransferenciasIntegradas(string IdEstadoDestino, string IdFarmaciaDestino, string IdEstadoRecibe, string IdFarmaciaRecibe)
        {
            return BuscarTransferenciasIntegradas(IdEstadoDestino, IdFarmaciaDestino, IdEstadoRecibe, IdFarmaciaRecibe, false);
        }

        private bool BuscarTransferenciasIntegradas(string IdEstadoDestino, string IdFarmaciaDestino, string IdEstadoRecibe, string IdFarmaciaRecibe, bool EsDevolucion)
        { 
            bool bRegresa = true;

            string sTabla = "TransferenciasEnvioEnc";

            if (EsDevolucion)
            {
                sTabla = "DevolucionTransferenciasEnvioEnc";
            }

            string sSql = string.Format(" Select Distinct T.IdEstadoRecibe, T.IdFarmaciaRecibe, " +
                " T.IdEstadoEnvia, T.IdFarmaciaEnvia, F.ClaveRenapo " +
                " From {5} T (NoLock) " +
                " Inner Join vw_Farmacias F (NoLock) On ( T.IdEstadoEnvia = F.IdEstado and T.IdFarmaciaEnvia = F.IdFarmacia )  " +
                " Where T.Status = 'I' and T.IdEstadoEnvia = '{0}' and T.IdFarmaciaEnvia = '{1}' " +
                " and T.IdEstadoRecibe = '{2}' and T.IdFarmaciaRecibe = '{3}' and T.FolioTransferencia = '{4}' ",
                IdEstadoDestino, IdFarmaciaDestino, IdEstadoRecibe, IdFarmaciaRecibe, sFolioTS, sTabla);

            if (!leerTransferencias.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerTransferencias, "BuscarTransferenciasIntegradas()");
            }

            return bRegresa;
        }

        private bool TransferenciasIntegradas(string IdEstadoDestino, string IdFarmaciaDestino, string IdEstadoRecibe, string IdFarmaciaRecibe)
        {
            return TransferenciasIntegradas(IdEstadoDestino, IdFarmaciaDestino, IdEstadoRecibe, IdFarmaciaRecibe, false);
        }
       
        private bool TransferenciasIntegradas(string IdEstadoDestino, string IdFarmaciaDestino, string IdEstadoRecibe, string IdFarmaciaRecibe, bool EsDevolucion)
        {
            bool bRegresa = false;
            // string sFile = "";
            string sTabla = "";
            int iOrden = 0;

            iTipoDeTablasEnvio = 2;
            BuscarTransferenciasIntegradas(IdEstadoDestino, IdFarmaciaDestino, IdEstadoRecibe, IdFarmaciaRecibe, EsDevolucion);
            while (leerTransferencias.Leer())
            {
                sIdEstadoEnvio = leerTransferencias.Campo("IdEstadoRecibe");
                sIdFarmaciaEnvio = leerTransferencias.Campo("IdFarmaciaRecibe");

                sIdEstadoOrigen = leerTransferencias.Campo("IdEstadoEnvia");
                sIdFarmaciaOrigen = leerTransferencias.Campo("IdFarmaciaEnvia");
                sClaveRenapoOrigen = leerTransferencias.Campo("ClaveRenapo");

                ////////iTipoDeTablasEnvio = 2;
                ////////ExistenTablasEnvioTransferencia();
                ////////while (leerCat.Leer())
                ////////{
                ////////    sTabla = leerCat.Campo("NombreTabla");
                ////////    iOrden = leerCat.CampoInt("IdOrden");
                ////////    GenerarTabla(sTabla, iOrden);
                ////////    bRegresa = true;
                ////////}

                //////////////// 2011-01-27_1250 Posible error de Obtencion detectado, cada tabla es transaccional.... 
                iTipoDeTablasEnvio = 2;
                ExistenTablasEnvioTransferencia(EsDevolucion);
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

                iTipoDeTablasEnvio = 3;
                if (bExistenArchivos)
                {
                    Empacar(); // DestinoArchivos.Farmacia_A_Farmacia
                    GenerarArchivosClientes(DestinoArchivos.Farmacia_A_Farmacia);
                }
            }

            // Asegurar que al Enviar los archivos estos se envien a su Destino Respectivo 
            iTipoDeTablasEnvio = 1;

            return bRegresa;
        }

        public void TransferenciasAutomaticasIntegradas(string IdEstadoDestino, string IdFarmaciaDestino, string IdEstadoRecibe, string IdFarmaciaRecibe, string FolioTS)
        {
            TransferenciasAutomaticasIntegradas(IdEstadoDestino, IdFarmaciaDestino, IdEstadoRecibe, IdFarmaciaRecibe, FolioTS, false);
        }

        public void TransferenciasAutomaticasIntegradas(string IdEstadoDestino, string IdFarmaciaDestino, string IdEstadoRecibe, string IdFarmaciaRecibe, string FolioTS, bool EsDevolucion)
        {
            sFolioTS = FolioTS;

            bTSConfirmacion = true;

            bEsProcesoDeTranserencias = true; 
            sRutaTransferencias = @"\\Transferencias_Integradas\\";

            ObtenerDestino();
            sCveOrigenArchivo = IdFarmaciaDestino; // Poner la Farmacia Destino 

            if (GetRutaObtencion())
            {
                TransferenciasIntegradas(IdEstadoDestino, IdFarmaciaDestino, IdEstadoRecibe, IdFarmaciaRecibe, EsDevolucion);
            }

            bEsProcesoDeTranserencias = false; 
        }
        #endregion Transferencias_Integradas

        #region Pedidos_Cedis
        public void Pedidos_Cedis(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido)
        {
            bEsProcesoDePedidos = true;
            sRutaPedidosCedis = @"\\Pedidos_Cedis\\";

            sIdEmpresaPedido = IdEmpresa;
            sIdEstadoPedido = IdEstado;
            sIdFarmaciaPedido = IdFarmacia;
            sIdFarmaciaEnvio = IdFarmacia;
            sFolioPedido = FolioPedido;

            if (GetRutaObtencion_Pedidos())
            {
                PedidosCedis(IdEmpresa, IdEstado, IdFarmacia, FolioPedido);
            }

            bEsProcesoDePedidos = false;
        }

        private bool PedidosCedis(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido)
        {
            bool bRegresa = false;
            // string sFile = "";
            string sTabla = "";
            int iOrden = 0;

            iTipoDeTablasEnvio = 2;
            ExistenTablasEnvioPedidos();
            if (leerCat.Registros > 0)
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    while (leerCat.Leer())
                    {
                        sTabla = leerCat.Campo("NombreTabla");
                        iOrden = leerCat.CampoInt("IdOrden");
                        bRegresa = GenerarTablaPedidos(sTabla, iOrden);
                        if (!bRegresa)
                        {
                            break;
                        }                        
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

            iTipoDeTablasEnvio = 3;
            if (bExistenArchivos)
            {
                Empacar(); // DestinoArchivos.Farmacia_A_Farmacia
                GenerarArchivosClientes(DestinoArchivos.Farmacia_A_Almacen);
            }


            iTipoDeTablasEnvio = 1;
            return bRegresa;
        }

        private bool ExistenTablasEnvioPedidos()
        {
            bool bRegresa = true;
            string sSql = "Select * From CFGSC_EnvioPedidosCedis (NoLock) Where Status = 'A' Order By IdOrden, NombreTabla ";

            // leerCat = new clsLeer(ref cnn);
            if (!leerCat.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerCat, "ExistenTablasEnvioPedidos()");
            }

            return bRegresa;
        }

        private bool GenerarTablaPedidos(string Tabla, int Orden)
        {
            ////  
            string sFile = this.GeneraNombreArchivoTabla(Tabla, Orden);
            bool bExito = true;

            if (ExisteTabla_A_Procesar(Tabla))
            {
                if (PreparaDatosTablaPedidos(Tabla, Datos.Obtener))
                {
                    if (CrearArchivoPedidos(Tabla, sFile))
                    {
                        bExito = PreparaDatosTablaPedidos(Tabla, Datos.Procesado);
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

        private bool PreparaDatosTablaPedidos(string Tabla, Datos Efecto)
        {
            // Prepara los datos de la tabla seleccionada para solo copiar los datos necesarios
            bool bRegresa = true;
            string sSql = "";
            string sEfecto = "0";
            string sWhere = "0";
            string sWherePedido = "";

            if (Efecto == Datos.Obtener)
            {
                sEfecto = "2";
            }
            else if (Efecto == Datos.Procesado)
            {
                sEfecto = "1";
                sWhere = "2";
            }

            sWherePedido = string.Format(", [ IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioPedido = '{3}' ] ",
                                            sIdEmpresaPedido, sIdEstadoPedido, sIdFarmaciaPedido, sFolioPedido);            

            //sSql = string.Format(" Exec spp_CFG_PrepararDatos '{0}', '{1}', '{2}' ", Tabla, sEfecto, sWhere);
            sSql = string.Format(" Exec spp_CFG_PrepararDatos '{0}', '{1}', '{2}' {3} ", Tabla, sEfecto, sWhere, sWherePedido);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "PreparaDatosTablaPedidos");
            }

            return bRegresa;
        }

        private bool CrearArchivoPedidos(string Tabla, string Nombre)
        {
            bool bRegresa = true;
            int iReg = 0, iVueltas = 0; // , iRegistros = 0;
            int iPaquete = 0;
            string sArchivoDestSql = sRutaObtencion + "" + Nombre + Fg.PonCeros(iPaquete, 4) + ".sql";
            StreamWriter f = null;

            if (ObtenerDatosTablaPedidos(Tabla))
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
                                sArchivoDestSql = sRutaObtencion + "" + Nombre + "_" + Fg.PonCeros(iPaquete, 4) + ".sql";
                                File.Delete(sArchivoDestSql);
                                f = new StreamWriter(sArchivoDestSql, false, Encoding.UTF8);
                            }

                            f.WriteLine(leerExec.Campo(1));
                            iReg++;

                            // Agregar el separador de Registros 
                            if (iReg >= Transferencia.RegistrosSQL)
                            {
                                f.WriteLine(Transferencia.SQL);
                                f.WriteLine("");
                                iReg = 0;
                                iVueltas++;
                            }

                            // Generar archivos de 200 Registros ==> 300-450 Kb 
                            if (iVueltas >= 5)
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

        private bool ObtenerDatosTablaPedidos(string Tabla)
        {
            bool bRegresa = true;

            // Asegurar que los Clientes envien la informacion al Servidor Regional para Reenvio a Servidor Central 
            string sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ Where Actualizado = 2 ], '0' ", Tabla);

            sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [  Where IdEmpresa = '{1}' and IdEstado = '{2}' and IdFarmacia = '{3}' " +
                                " and FolioPedido = '{4}' ], '0' ", Tabla, sIdEmpresaPedido, sIdEstadoPedido, sIdFarmaciaPedido, sFolioPedido);
            

            if (!leerExec.Exec(Tabla, sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerExec, "ObtenerDatosTablaPedidos()");
            }
            return bRegresa;
        }

        private bool GetRutaObtencion_Pedidos()
        {
            bool bRegresa = true;
            string sSql = "Select * From CFGC_ConfigurarObtencion (NoLock) ";

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
                    General.msjError("No se encontro la información de configuración de envio de información, reportarlo al departamento de sistemas.");
                }
                else
                {
                    
                    sRutaObtencion = leer.Campo("RutaUbicacionArchivos") + @"\\" + sRutaPedidosCedis;
                    sRutaEnviados = leer.Campo("RutaUbicacionArchivosEnviados") + @"\\";                   

                    // Revisar que existen las Rutas 
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
        #endregion Pedidos_Cedis
    }
}
