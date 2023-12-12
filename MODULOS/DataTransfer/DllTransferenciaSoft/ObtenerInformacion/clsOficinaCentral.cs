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

using DllTransferenciaSoft;
using DllTransferenciaSoft.EnviarInformacion;
using DllTransferenciaSoft.Zip;

namespace DllTransferenciaSoft.ObtenerInformacion
{
    public class clsOficinaCentral : clsObtenerInformacion
    {
        public clsOficinaCentral(string ArchivoCfg, clsDatosConexion DatosConexion, string ClaveRenapo, string Centro)
            : base(ArchivoCfg, DatosConexion, ClaveRenapo, Centro, TipoServicio.OficinaCentral)
        {
        }

        public clsOficinaCentral(string ArchivoCfg, clsDatosConexion DatosConexion, string ClaveRenapo, string Centro, 
            string IdEstado, string IdFarmacia )
            :base   (ArchivoCfg, DatosConexion, ClaveRenapo, Centro, TipoServicio.OficinaCentral, IdEstado, IdFarmacia) 
        {
        }
    }
}

//namespace DllTransferenciaSoft.ObtenerInformacion
//{

//    class clsDestinoFiles
//    {
//        public string Estado = "";
//        public string Farmacia = "";
//        public string UrlDestino = "";
//        public string ArchivoOrigen = "";
//        public string ArchivoDestino = "";
//        basGenerales Fg = new basGenerales();
//        public string ArchivoCfg = "";

//        public string ClaveDestino = ""; 

//        public clsDestinoFiles(string NombreArchivo, string UrlDestino)
//        {
//            FileInfo f = new FileInfo(NombreArchivo);

//            this.ArchivoOrigen = NombreArchivo;
//            this.UrlDestino = UrlDestino;
//            this.ArchivoDestino = f.Name; 
//        }

//        public clsDestinoFiles(string NombreArchivo, clsLeer ListaDestinos)
//        {
//            GetUrlDestino(NombreArchivo, ListaDestinos);
//        }

//        private void GetUrlDestino(string NombreArchivo, clsLeer ListaDestinos)
//        {
//            string sURL = "";
//            FileInfo f = new FileInfo(NombreArchivo);
//            string sClaveRenapo = Fg.Mid(f.Name, 8, 2);

//            this.ClaveDestino = Fg.Mid(f.Name, 8, 6); 
//            this.ArchivoOrigen = NombreArchivo;
//            this.ArchivoDestino = f.Name;

//            try
//            {
//                // DataRow[] dtRow = ListaDestinos.DataSetClase.Tables[0].Select(string.Format("ClaveRenapo = '{0}'", sClaveRenapo));
//                DataRow[] dtRow = ListaDestinos.DataSetClase.Tables[0].Select(string.Format("DirDestino = '{0}'", ClaveDestino));

//                if (dtRow.Length > 0)
//                {
//                    this.Estado = dtRow[0]["IdEstado"].ToString();
//                    this.Farmacia = dtRow[0]["IdFarmacia"].ToString();
//                    this.ClaveDestino = dtRow[0]["DirDestino"].ToString();

//                    sURL = "http://" + dtRow[0]["Servidor"].ToString() + "/" + dtRow[0]["WebService"].ToString() + "/" + dtRow[0]["PaginaWeb"].ToString();
//                    sURL = sURL.ToLower().Replace(".asmx", "") + ".asmx";

//                    UrlDestino = sURL;
//                }
//            }
//            catch { }
//        }
//    }

//    public class clsOficinaCentral
//    {
//        #region Declaracion de variables 
//        clsConexionSQL cnn;
//        clsDatosConexion datosCnn;
//        clsLeer leerCat;
//        clsLeer leerDet;
//        clsLeer leerExec;
//        clsLeer leer;
//        clsLeer leerDestinos;
//        clsLeer leerTransferencias; 


//        private ArrayList pFiles;

//        DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion EnviarCliente;

//        string sRutaObtencion = @"C:\\";
//        string sRutaEnviados = @"C:\\";

//        bool bExistenArchivos = false;
//        string sCveRenapo = "FF";
//        string sCveOrigenArchivo = "00FF";
//        string sArchivoGenerado = "";
//        string sArchivoCfg = "";

//        int iTipoDeTablasEnvio = 1;
//        bool bEnviandoArchivos = false; 

//        // Variables de funciones generales
//        basGenerales Fg = new basGenerales();
//        clsGrabarError Error;

//        string sArchivoSql = "";

//        bool bEsEnvioMasivo = true;
//        string sIdEstadoEnvio = "";
//        string sIdFarmaciaEnvio = "";

//        /// <summary>
//        /// Controla la Ejecución de los Procesos que se ejecutan en Hilos, 
//        /// se asegura de no dar por terminado el envio de información. 
//        /// </summary>
//        private int iNumProcesosEnEjecucion = 0;  
//        #endregion Declaracion de variables 

//        #region Constructor y Destructor 
//        public clsOficinaCentral( string ArchivoCfg, clsDatosConexion DatosConexion, string ClaveRenapo, string Centro)
//        {
//            this.bEsEnvioMasivo = true;
//            this.sCveRenapo = ClaveRenapo;
//            this.sCveOrigenArchivo = Centro;
//            this.sArchivoCfg = ArchivoCfg;
//            this.datosCnn = DatosConexion;
//            cnn = new clsConexionSQL(this.datosCnn);
//            Error = new clsGrabarError(Transferencia.Modulo + ".ObtenerInformacion", Transferencia.Version, "clsOficinaCentral");
//            Error.MostrarErrorAlGrabar = false; 

//            // Preparar acceso a datos 
//            leerCat = new clsLeer(ref cnn);
//            leerDet = new clsLeer(ref cnn);
//            leerExec = new clsLeer(ref cnn);
//            leer = new clsLeer(ref cnn);
//            leerDestinos = new clsLeer(ref cnn);
//            leerTransferencias = new clsLeer(ref cnn); 
//        }

//        public clsOficinaCentral(string ArchivoCfg, clsDatosConexion DatosConexion, string ClaveRenapo, string Centro, 
//            string IdEstado, string IdFarmacia )
//        {
//            bEsEnvioMasivo = false;
//            sIdEstadoEnvio = IdEstado;
//            sIdFarmaciaEnvio = IdFarmacia; 

//            this.sCveRenapo = ClaveRenapo;
//            this.sCveOrigenArchivo = Centro;
//            this.sArchivoCfg = ArchivoCfg;
//            this.datosCnn = DatosConexion;
//            cnn = new clsConexionSQL(this.datosCnn);
//            Error = new clsGrabarError(Transferencia.Modulo + ".ObtenerInformacion", Transferencia.Version, "clsOficinaCentral");
//            Error.MostrarErrorAlGrabar = false; 

//            // Preparar acceso a datos 
//            leerCat = new clsLeer(ref cnn);
//            leerDet = new clsLeer(ref cnn);
//            leerExec = new clsLeer(ref cnn);
//            leer = new clsLeer(ref cnn);
//            leerDestinos = new clsLeer(ref cnn); 
//            leerTransferencias = new clsLeer(ref cnn); 
//        } 
//        #endregion Constructor y Destructor

//        #region Propiedades 
//        public string ArchivoGenerado
//        {
//            get { return sArchivoGenerado; }
//        }

//        public string RutaObtencion
//        {
//            get { return sRutaObtencion; }
//        }
//        #endregion Propiedades

//        #region Obtener Configuraciones
//        private bool GetRutaObtencion()
//        {
//            bool bRegresa = true;
//            string sSql = " Select * From CFGS_ConfigurarObtencion (NoLock) ";

//            if (!leer.Exec(sSql))
//            {
//                Error.GrabarError(leer, "GetRutaObtencion()");
//                bRegresa = false;
//            }
//            else
//            {
//                if (!leer.Leer())
//                {
//                    bRegresa = false;
//                    General.msjError("No se encontro la información de confuguración de envio de información, reportarlo al departamento de sistemas.");
//                }
//                else
//                {
//                    sRutaObtencion = leer.Campo("RutaUbicacionArchivos") + @"\\";
//                    sRutaEnviados = leer.Campo("RutaUbicacionArchivosEnviados") + @"\\";
//                    sArchivoSql = sRutaObtencion + @"\\Intermed_xxx.sql";

//                    // Modificar la ruta de Archivos, generar el paquete en una Ruta Alterna 
//                    if (!bEsEnvioMasivo)
//                        sRutaObtencion += @"\\EnviarCatalogos\\"; 

//                    // Revisar que existen las Rutas 
//                    if (!Directory.Exists(sRutaObtencion))
//                        Directory.CreateDirectory(sRutaObtencion);

//                    if (!Directory.Exists(sRutaEnviados))
//                        Directory.CreateDirectory(sRutaEnviados); 

//                }
//            }
//            return bRegresa;
//        }
//        #endregion Obtener Configuraciones

//        #region Funciones y Procedimientos Privados
//        private string GenerarNombre()
//        {
//            string sRegresa = "XX_Prueba";
//            return sRegresa;
//        }

//        private string GeneraNombreArchivoTabla(string Tabla, int Orden)
//        {
//            string sRegresa = "Datos_" + Fg.PonCeros(Orden, 4) + "_" + Tabla;
//            return sRegresa.ToUpper().Trim();
//        }

//        private bool ExistenTablasEnvio()
//        {
//            bool bRegresa = true;
//            string sSql = "Select * From CFGS_EnvioCatalogos (NoLock) Where Status = 'A' Order By IdOrden, NombreTabla ";

//            // leerCat = new clsLeer(ref cnn);
//            if (!leerCat.Exec(sSql))
//            {
//                bRegresa = false;
//                Error.GrabarError(leerCat, "ExistenTablasEnvio()");
//            }

//            return bRegresa;
//        }

//        private bool ExistenTablasEnvioTransferencia()
//        {
//            bool bRegresa = true;
//            string sSql = "Select * From CFGS_EnvioDetallesTrans (NoLock) Where Status = 'A' Order By IdOrden, NombreTabla ";

//            // leerCat = new clsLeer(ref cnn);
//            if (!leerCat.Exec(sSql))
//            {
//                bRegresa = false;
//                Error.GrabarError(leerCat, "ExistenTablasEnvioTransferencia()");
//            }

//            return bRegresa;
//        }

//        private bool PreparaDatosTabla(string Tabla, Datos Efecto)
//        {
//            // Prepara los datos de la tabla seleccionada para solo copiar los datos necesarios
//            bool bRegresa = true;
//            string sEfecto = "0";
//            string sWhere = "0";
//            string sWhereTransf = ""; 

//            if (Efecto == Datos.Obtener)
//            {
//                sEfecto = "2"; 
//            }
//            else if (Efecto == Datos.Procesado)
//            {
//                sEfecto = "1";
//                sWhere = "2";
//            }

//            if (iTipoDeTablasEnvio == 2)
//            {
//                sWhereTransf = string.Format(", [ IdEstadoRecibe = '{0}' and IdFarmaciaRecibe = '{1}' ]", sIdEstadoEnvio, sIdFarmaciaEnvio);
//            }

//            string sSql = string.Format(" Exec spp_CFG_PrepararDatos '{0}', '{1}', '{2}' {3} ", Tabla, sEfecto, sWhere, sWhereTransf);


//            // Cambiar el Status Actualizado solo cuando sea una Replicacion automatica 
//            if (bEsEnvioMasivo)
//            {
//                if (!leer.Exec(sSql))
//                {
//                    bRegresa = false;
//                    Error.GrabarError(leer, "PreparaDatosTabla");
//                }
//            }

//            return bRegresa;
//        }

//        private bool ObtenerDatosTabla(string Tabla)
//        {
//            bool bRegresa = true;
//            string sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}' ", Tabla );

//            // Solo tomar la informacion marcada para Envio automatico 
//            if (bEsEnvioMasivo)
//            {
//                sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ Where Actualizado = 2 ] ", Tabla);

//                // Las transferencias se envian con Actualizado = 0 para su envio inmediato 
//                if (iTipoDeTablasEnvio == 2)
//                {
//                    sSql = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', [ Where Actualizado = 2 and IdEstadoRecibe = '{1}' and IdFarmaciaRecibe = '{2}' ], '0' ", 
//                        Tabla, sIdEstadoEnvio, sIdFarmaciaEnvio);
//                }
//            }

//            if (!leerExec.Exec(Tabla, sSql))
//            {
//                bRegresa = false;
//                Error.GrabarError(leerExec, "ObtenerDatosTabla()");
//            }
//            return bRegresa;
//        }

//        private bool ObtenerDestinos()
//        {
//            bool bRegresa = true;
//            string sSql = string.Format("Select Distinct C.*, E.ClaveRenapo, (E.ClaveRenapo + C.IdFarmacia) as DirDestino " + 
//                " From CFGS_ConfigurarConexiones C (NoLock) " +
//                " Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) " + 
//                " Where C.IdEstado = '{0}' and C.IdFarmacia = '{1}' and C.Status = 'A' Order by C.IdEstado, C.IdFarmacia ", 
//                sIdEstadoEnvio, sIdFarmaciaEnvio );

//            if (bEsEnvioMasivo)
//            {
//                sSql = "Select Distinct C.*, E.ClaveRenapo, (E.ClaveRenapo + C.IdFarmacia) as DirDestino " +
//                       " From CFGS_ConfigurarConexiones C (NoLock) " +
//                       " Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) " +
//                       " Where C.Status = 'A' Order by C.IdEstado, C.IdFarmacia ";

//                if (!bEnviandoArchivos)
//                {
//                    // En caso de Transferencias se envian como si fuera un envio especial de información. 
//                    if (iTipoDeTablasEnvio == 2)
//                    {
//                        sSql = string.Format("Select Distinct C.*, E.ClaveRenapo, (E.ClaveRenapo + C.IdFarmacia) as DirDestino " +
//                                        " From CFGS_ConfigurarConexiones C (NoLock) " +
//                                        " Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) " +
//                                        " Where C.IdEstado = '{0}' and C.IdFarmacia = '{1}' and C.Status = 'A' Order by C.IdEstado, C.IdFarmacia ",
//                                        sIdEstadoEnvio, sIdFarmaciaEnvio); 
//                    }
//                } 
//            }

//            if (!leerDestinos.Exec(sSql))
//            {
//                bRegresa = false;
//                General.Error.GrabarError(leerDestinos, "ObtenerDestinos()"); 
//            }

//            return bRegresa;
//        } 
//        #endregion Funciones y Procedimientos Privados
        
//        #region Generar archivos
//        public bool GenerarArchivos()
//        {
//            bool bRegresa = true;
//            bExistenArchivos = false;
//            bEnviandoArchivos = false; 

//            if (GetRutaObtencion())
//            {
//                this.Catalogos();
//                if (bExistenArchivos)
//                {
//                    Empacar(DestinoArchivos.TodasLasFarmacias);
//                    GenerarArchivosClientes(DestinoArchivos.TodasLasFarmacias);
//                    //EnviarCatalogos(sArchivoCfg, sRutaObtencion + @"\\" + sArchivoGenerado, sArchivoGenerado);
//                }

//                this.Transferencias();
//                //if (bExistenArchivos)
//                //{
//                //    Empacar(DestinoArchivos.Farmacia_A_Farmacia);
//                //    GenerarArchivosClientes();
//                //}
//            }
//            else
//            {
//                bRegresa = false;
//            }

//            return bRegresa;
//        }

//        public bool EnviarArchivos()
//        {
//            bool bRegresa = true;

//            bEnviandoArchivos = true; 
//            bRegresa = EnviarCatalogos(sArchivoCfg); //, "", "");

//            return bRegresa;
//        }

//        private bool Catalogos()
//        {
//            bool bRegresa = false;
//            string sFile = "";
//            string sTabla = "";
//            int iOrden = 0;

//            iTipoDeTablasEnvio = 1; 
//            ExistenTablasEnvio();
//            while (leerCat.Leer())
//            {
//                sTabla = leerCat.Campo("NombreTabla");
//                iOrden = leerCat.CampoInt("IdOrden");
//                GenerarTabla(sTabla, iOrden);
//                bRegresa = true;
//            }

//            return bRegresa;
//        }

//        private bool Transferencias()
//        {
//            bool bRegresa = false;
//            string sFile = "";
//            string sTabla = "";
//            int iOrden = 0;

//            iTipoDeTablasEnvio = 2; 
//            BuscarTransferenciasPendientes(); 
//            while (leerTransferencias.Leer())
//            {
//                sIdEstadoEnvio = leerTransferencias.Campo("IdEstadoRecibe");
//                sIdFarmaciaEnvio = leerTransferencias.Campo("IdFarmaciaRecibe"); 
//                ExistenTablasEnvioTransferencia(); 
//                while (leerCat.Leer())
//                {
//                    sTabla = leerCat.Campo("NombreTabla");
//                    iOrden = leerCat.CampoInt("IdOrden");
//                    GenerarTabla(sTabla, iOrden);
//                    bRegresa = true; 
//                }

//                if (bExistenArchivos)
//                {
//                    Empacar(DestinoArchivos.Farmacia_A_Farmacia);
//                    GenerarArchivosClientes(DestinoArchivos.Farmacia_A_Farmacia);
//                }

//            }

//            // Asegurar que al Enviar los archivos estos se envien a su Destino Respectivo 
//            iTipoDeTablasEnvio = 1; 

//            return bRegresa;
//        }

//        private bool BuscarTransferenciasPendientes()
//        {
//            bool bRegresa = true;
//            string sSql = string.Format(" Select Distinct IdEstadoRecibe, IdFarmaciaRecibe From TransferenciasEnvioEnc (NoLock) Where Actualizado in ( 0, 2 ) ");

//            if (!leerTransferencias.Exec(sSql))
//            {
//                bRegresa = false;
//                Error.GrabarError(leerTransferencias, "BuscarTransferenciasPendientes()"); 
//            }

//            return bRegresa; 
//        }

//        private void GenerarTabla(string Tabla, int Orden)
//        {
//            string sFile = this.GeneraNombreArchivoTabla(Tabla, Orden);
//            bool bExito = true;

//            if (cnn.Abrir())
//            {
//                // Iniciar transaccion por cada tabla que se generara
//                cnn.IniciarTransaccion();

//                if (PreparaDatosTabla(Tabla, Datos.Obtener))
//                {
//                    if (CrearArchivo(Tabla, sFile))
//                    {
//                        bExito = PreparaDatosTabla(Tabla, Datos.Procesado);
//                    }
//                }

//                if (!bExito)
//                    cnn.DeshacerTransaccion();
//                else
//                    cnn.CompletarTransaccion();

//                cnn.Cerrar();
//            }

//        }

//        private bool CrearArchivo(string Tabla, string Nombre)
//        {
//            bool bRegresa = false;
//            int iReg = 0; 
//            string sArchivoDestSql = sRutaObtencion + "" + Nombre + ".sql";

//            if (ObtenerDatosTabla(Tabla))
//            {
//                try
//                {
//                    File.Delete(sArchivoDestSql);
//                    if (leerExec.Registros > 0)
//                    {
//                        bRegresa = true;
//                        bExistenArchivos = true;
//                        StreamWriter f = new StreamWriter(sArchivoDestSql);
//                        while (leerExec.Leer())
//                        {
//                            f.WriteLine(leerExec.Campo(1));
//                            iReg++;

//                            // Agregar el separador de Registros 
//                            if (iReg >= Transferencia.RegistrosSQL)
//                            {
//                                f.WriteLine(Transferencia.SQL);
//                                f.WriteLine(""); 
//                                iReg = 0;
//                            }
//                        }
//                        f.WriteLine(Transferencia.SQL);
//                        f.WriteLine(""); 
//                        f.Close(); 
//                    }
//                }
//                catch //( Exception ex )
//                {
//                    //General.msjError(ex.Message);
//                } 
//            }

//            return bRegresa;
//        }

//        private bool Empacar(DestinoArchivos Destino)
//        {
//            bool bRegresa = false;
//            int iTipo = (int)Destino;
//            string sNombrePaquete = "Intermed." + Transferencia.ExtArchivosGenerados;

//            string sFileZip = sRutaObtencion + @"\\" + "Intermed." + Transferencia.ExtArchivosGenerados;
//            string sFileOut = sRutaObtencion + @"\\" + "Intermed." + Transferencia.ExtArchivosGenerados;

//            string []sFiles = Directory.GetFiles(sRutaObtencion, "*.sql");
//            clsCriptografia Cripto = new clsCriptografia();
//            ZipUtil zip = new ZipUtil();

//            sArchivoGenerado = sNombrePaquete;
//            bRegresa = zip.Comprimir(sFiles, sFileZip, true); 

//            return bRegresa;
//        }

//        private bool GenerarArchivosClientes(DestinoArchivos Destino)
//        {
//            bool bRegresa = false; 
//            string sFileOrigen = sRutaObtencion + @"\" + sArchivoGenerado;
//            string sFileDestino = "";
//            string sNombre = sCveRenapo + sCveOrigenArchivo;
//            string sTipo = Fg.PonCeros((int)Destino, 2);
//            string sMarcaTiempo = "";  // General.FechaYMD(General.FechaSistema).Replace("/", "") + General.FechaSistemaHora.Substring(0, 5).Replace(":", "");              
//            string sURL = "";
//            string sDirUnidad = "";

//            sMarcaTiempo = General.FechaYMD(General.FechaSistema).Replace("/", "");
//            sMarcaTiempo = sMarcaTiempo.Replace("-", "");
//            sMarcaTiempo += "-" + General.FechaSistemaHora.Substring(0, 5).Replace(":", "");


//            if (ObtenerDestinos())
//            {
//                pFiles = new ArrayList();
//                while (leerDestinos.Leer())
//                {
//                    try
//                    {
//                        //sURL = "http://" + leerDestinos.Campo("Servidor") + "/" + leerDestinos.Campo("WebService") + "/" + leerDestinos.Campo("PaginaWeb");
//                        //sURL = sURL.ToLower().Replace(".asmx", "") + ".asmx"; 
                        
//                        // Generar un directorio por cada Unidad 
//                        sDirUnidad = sRutaEnviados +@"\" + leerDestinos.Campo("DirDestino");
//                        if (!Directory.Exists(sDirUnidad))
//                            Directory.CreateDirectory(sDirUnidad); 

//                        sFileDestino = sRutaObtencion + @"\" + sNombre + "-" + leerDestinos.Campo("ClaveRenapo") + leerDestinos.Campo("IdFarmacia");
//                        sFileDestino += "-" + sTipo + "-" + sMarcaTiempo;
//                        sFileDestino += "." + Transferencia.ExtArchivosGenerados;

//                        File.Copy(sFileOrigen, sFileDestino); 
//                        bRegresa = true;
//                    }
//                    catch (Exception ex)
//                    { 
//                        bRegresa = false; break; 
//                    }
//                }

//                //if (bRegresa)
//                {
//                    // Borrar el archivo Origen si se pudo generar una copia por cada Destino disponible. 
//                    File.Delete(sFileOrigen);
//                }
//            }

//            return bRegresa;
//        } 
//        #endregion Generar archivos

//        #region Enviar información 
//        private bool EnviarCatalogos(string ArchivoCfg) //, string RutaArchivo, string NombreDestino )
//        {
//            bool bRegresa = false;
//            string sDestino = "";
//            string sPatron = "*." + Transferencia.ExtArchivosGenerados;
//            string sURL = "", sFile = ""; 

//            pFiles = new ArrayList(); 
//            if (ObtenerDestinos())
//            {
//                // EnviarCliente = new DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion(TipoServicio.Cliente);

//                // sRutaObtencion 
//                foreach (string f in Directory.GetFiles(sRutaObtencion, sPatron))
//                {
//                    pFiles.Add(new clsDestinoFiles(f, leerDestinos));
//                }

//                iNumProcesosEnEjecucion = 0; 
//                foreach (clsDestinoFiles d in pFiles)
//                {
//                    // Generar un directorio por cada Unidad 
//                    sDestino = sRutaEnviados + @"\" + d.ClaveDestino;
//                    if (!Directory.Exists(sDestino))
//                        Directory.CreateDirectory(sDestino); 

//                    d.ArchivoCfg = ArchivoCfg;  
//                    Thread t = new Thread(this.ThreadEnviarArchivo);
//                    t.Name = d.ArchivoDestino;
//                    iNumProcesosEnEjecucion++; 
//                    t.Start(d); 
//                } 

//                // Mantener el proceso detenido hasta que finalizen todos los envios de información. 
//                while (iNumProcesosEnEjecucion != 0)
//                {
//                    Thread.Sleep(100); 
//                }
//            } 
//            return bRegresa;
//        }

//        private void ThreadEnviarArchivo(object Archivo)
//        {
//            clsDestinoFiles d = (clsDestinoFiles)Archivo;
//            DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion 
//                EnviarInformacion = new DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion(TipoServicio.Cliente); 

//            string sDestino = ""; 
//            bool bRegresa = false; 
//            //bRegresa = true; 

//            //iNumProcesosEnEjecucion++; 
//            try
//            {
//                // sDestino = sRutaEnviados + @"\" + d.ArchivoDestino;
//                sDestino = sRutaEnviados + @"\" + d.ClaveDestino + @"\" + d.ArchivoDestino; 
//                EnviarInformacion.Url = d.UrlDestino; 
//                bRegresa = EnviarInformacion.Enviar(d.ArchivoCfg, d.ArchivoOrigen, d.ArchivoDestino, d.Farmacia); 
//                //
 
//                try
//                {
//                    if (bRegresa) 
//                    {
//                        File.Delete(sDestino); 
//                        File.Move(d.ArchivoOrigen, sDestino); 
//                    }
//                }
//                catch { }
//            }
//            catch { }
//            iNumProcesosEnEjecucion--; 
//        } 
//        #endregion Enviar información 
//    }
//}
