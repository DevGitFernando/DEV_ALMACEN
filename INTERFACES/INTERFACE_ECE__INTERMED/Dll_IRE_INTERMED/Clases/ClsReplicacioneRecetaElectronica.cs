using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
// using SC_SolutionsSystem.Criptografia;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
//using DllTransferenciaSoft;
//using DllTransferenciaSoft.EnviarInformacion;
//using DllTransferenciaSoft.Zip;
//using DllTransferenciaSoft.ObtenerInformacion;

namespace Dll_IRE_INTERMED.Clases
{
    public class ClsReplicacioneRecetaElectronica
    {

        protected clsConexionSQL cnn;
        protected clsDatosConexion datosCnn;
        protected clsLeer leerCat;
        protected clsLeer leerDet;
        protected clsLeer leerExec;
        protected clsLeer leer;
        protected clsLeer leerDestinos;
        protected clsLeer leerTransferencias;

        //private ArrayList pFiles;


        ws_IRE_INTERMED.wsIRE_INTERMED IRE_INTERMED;

        // DllTransferenciaSoft.EnviarInformacion.clsEnviarInformacion EnviarCliente;

        protected string sRutaObtencion = @"C:\\";
        protected string sRutaEnviados = @"C:\\";
        string sUrlDestino = "";
        string sClues_Emisor = "", sTipo = "", sIdEmpresa = "", sIdEstado = "", sIdFarmacia = "";

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
        //TamañoFiles tpTamañoFiles = TamañoFiles.MB;

        int iTipoDeProceso = 1;
        int iTipoDeEnvio = 1;
        bool bReplicarDirecto_SQL = false;
        bool bRevisionPorFechaServidor = false;

        bool bTemporizador_Inicializado = false;
        DateTime dtInicio_Replicacion = DateTime.Now;
        DateTime dtFin_Replicacion = DateTime.Now;
        public bool bRecetaEncontrada = false;

        public ClsReplicacioneRecetaElectronica()
        {
            cnn = new clsConexionSQL(General.DatosConexion);
            Error = new clsGrabarError("Replicacion", "", "ClsReplicacioneVales");
            IRE_INTERMED = new ws_IRE_INTERMED.wsIRE_INTERMED();
            Error.MostrarErrorAlGrabar = false;

            // Preparar acceso a datos 
            leerCat = new clsLeer(ref cnn);
            leerDet = new clsLeer(ref cnn);
            leerExec = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            leerDestinos = new clsLeer(ref cnn);
            leerTransferencias = new clsLeer(ref cnn);

            ObtenerDestino();
        }

        public bool ObtenerRecetasElectronicas()
        {
            bool bRegresa = true;
            string sSSL = "";


            try
            {
                leerDestinos.RegistroActual = 1;
                while (leerDestinos.Leer() && bRegresa)
                {
                    sIdEmpresa = leerDestinos.Campo("IdEmpresa");
                    sIdEstado = leerDestinos.Campo("IdEstado");
                    sIdFarmacia = leerDestinos.Campo("IdFarmacia");
                    sClues_Emisor = leerDestinos.Campo("Referencia_SIADISSEP");
                    sTipo = leerDestinos.Campo("TipoDocumento");
                    sSSL = leerDestinos.CampoBool("SSL") ? "s" : "";
                    sUrlDestino = string.Format("http{0}://{1}/{2}/{3}.asmx", sSSL, leerDestinos.Campo("Servidor"), leerDestinos.Campo("WebService"), leerDestinos.Campo("PaginaWeb"));

                    System.Console.WriteLine("Clues : " + sClues_Emisor);
                    System.Console.WriteLine(sUrlDestino);

                    IRE_INTERMED.Url = sUrlDestino;
                    leer.DataSetClase = IRE_INTERMED.GetRecetasElectronicas(sClues_Emisor, sTipo);

                    if (!leer.SeEncontraronErrores())
                    {
                        if (leer.Leer())
                        {
                            bRegresa = Guardar_General(leer.DataSetClase);
                        }
                    }
                    else
                    {
                        bRegresa = false;
                    }
                }

            }
            catch (Exception ex)
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        public bool ObtenerRecetasElectronica_Especifica(string CLUES, string FolioDeReceta)
        {
            bool bRegresa = true;
            bRecetaEncontrada = false;
            string sSSL = "";

            sClues_Emisor = CLUES;

            try
            {
                ObtenerDestino(); 

                leerDestinos.RegistroActual = 1; 
                if (leerDestinos.Leer() && bRegresa)
                {
                    sIdEmpresa = leerDestinos.Campo("IdEmpresa");
                    sIdEstado = leerDestinos.Campo("IdEstado");
                    sIdFarmacia = leerDestinos.Campo("IdFarmacia");
                    sSSL = leerDestinos.CampoBool("SSL") ? "s" : "";
                    sUrlDestino = string.Format("http{0}://{1}/{2}/{3}.asmx", sSSL, leerDestinos.Campo("Servidor"), leerDestinos.Campo("WebService"), leerDestinos.Campo("PaginaWeb"));

                    System.Console.WriteLine("Clues : " + sClues_Emisor);
                    System.Console.WriteLine(sUrlDestino);

                    IRE_INTERMED.Url = sUrlDestino;
                    leer.DataSetClase = IRE_INTERMED.GetRecetaElectronica(sClues_Emisor, FolioDeReceta);

                    if (!leer.SeEncontraronErrores())
                    {
                        if (leer.Leer())
                        {
                            bRecetaEncontrada = Guardar_General(leer.DataSetClase);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        public bool EnviarRecetasElectronicasAtendidas()
        {
            bool bRegresa = true;
            string sSSL = "";
            string sClues_Ant = "";
            clsLeer LeerErrores = new clsLeer();
            string sIdEmpresa, sIdEstado, sIdFarmacia, sFolio;

            try
            {
                leerDestinos.RegistroActual = 1;
                while (leerDestinos.Leer() && bRegresa)
                {
                    sIdEmpresa = leerDestinos.Campo("IdEmpresa");
                    sIdEstado = leerDestinos.Campo("IdEstado");
                    sIdFarmacia = leerDestinos.Campo("IdFarmacia");
                    sClues_Emisor = leerDestinos.Campo("Referencia_SIADISSEP");
                    sTipo = leerDestinos.Campo("TipoDocumento");
                    sSSL = leerDestinos.CampoBool("SSL") ? "s" : "";
                    sUrlDestino = string.Format("http{0}://{1}/{2}/{3}.asmx", sSSL, leerDestinos.Campo("Servidor"), leerDestinos.Campo("WebService"), leerDestinos.Campo("PaginaWeb"));

                    IRE_INTERMED.Url = sUrlDestino;

                    if (sClues_Emisor != sClues_Ant)
                    {
                        sClues_Ant = sClues_Emisor;
                        leer.DataSetClase = ObetenerRecetasElectronicasAtendidas();

                        if (leer.Leer())
                        {
                            LeerErrores.DataSetClase = IRE_INTERMED.SendAcuseRecetasElectronicas(sClues_Emisor, leer.DataSetClase);

                            if (LeerErrores.SeEncontraronErrores())
                            {
                                Error.GrabarError(LeerErrores, "EnviarValesAtendidos()");
                                bRegresa = false;
                            }
                            else
                            {
                                leer.RegistroActual = 1;
                                while (leer.Leer())
                                {
                                    sIdEmpresa = leer.Campo("IdEmpresa");
                                    sIdEstado = leer.Campo("IdEstado");
                                    sIdFarmacia = leer.Campo("IdFarmacia");
                                    sFolio = leer.Campo("Folio");

                                    sSSL = string.Format("Update G Set Procesado = 1 From INT_RE_INTERMED__RecetasElectronicas_0001_General G " +
                                        "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}'",
                                        sIdEmpresa, sIdEstado, sIdFarmacia, sFolio);

                                    if (!leerExec.Exec(sSSL))
                                    {
                                        Error.GrabarError(leerExec, "EnviarRecetasElectronicasAtendidas()");
                                        bRegresa = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                bRegresa = false;
            }

            return bRegresa;
        }  

        #region funciones y procedimientos privados
        private DataSet ObetenerRecetasElectronicasAtendidas()
        {
            DataSet dtsRegresa = new DataSet();

            string sSql = string.Format("Exec spp_INT_RE_INTERMED__RecetasElectronicas_0012_ObetenerRecetasAtendidas");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObetenerRecetasElectronicasAtendidas()");
            }
            else
            {
                dtsRegresa = leer.DataSetClase;
            }

            return dtsRegresa;
        }

        private bool ObtenerDestino()
        {
            return ObtenerDestino("");
        }

        private bool ObtenerDestino(string CLUES)
        {
            bool bRegresa = true;
            string sSql = string.Format("Select * From INT_RE_INTERMED__CFG_Farmacias_UMedicas (NoLock) Order By Referencia_SIADISSEP");

            if (CLUES != "")
            {
                sSql = string.Format("Select * From INT_RE_INTERMED__CFG_Farmacias_UMedicas (NoLock) Where Referencia_SIADISSEP = '{0}' ", CLUES);
            }

            System.Console.WriteLine("Obteniendo CLUES.");
            if (!leerDestinos.Exec(sSql))
            {
                bRegresa = false;
                General.Error.GrabarError(leerDestinos, "ObtenerClues()");
                System.Console.WriteLine(string.Format("        Error : {0}", leerDestinos.MensajeError));
            }

            return bRegresa;
        }

        private bool Guardar_General(DataSet Informacion)
        {
            bool bRegresa = true;
            string sSql = "";
            clsLeer LeerInformacion = new clsLeer();
            LeerInformacion.DataSetClase = Informacion;

            string sFolioReceta = "";
            string sFechaReceta = "";
            string sFechaEnvioReceta = "";

            string sFolioAfiliacionSPSS = "";
            string sFechaIniciaVigencia = "";
            string sFechaTerminaVigencia = "";
            string[] sVigencias = null;
            string sRangoVigencia = "";

            string sBeneficiarioNombre = "";
            string sBeneficiarioApPaterno = "";
            string sBeneficiarioApMaterno = "";
            string sBeneficiarioSexo = "";
            string sExpediente = "";
            string sFechaNacimiento = "";

            string sFolioOportunidades = "";
            string sEsPoblacionAbierta = "";

            string sMedicoClave = "";
            string sMedicoNombre = "";
            string sMedicoApPaterno = "";
            string sMedicoApMaterno = "";
            string sMedicoCedula = "";
             string sInformacionXML = "";

            clsLeer datos = new clsLeer();
            datos.DataTableClase = LeerInformacion.Tabla("General");
            //bRegresa = datos.Registros > 0;

            while (datos.Leer())
            {
                //sClues_Emisor = datos.Campo("CLUES");
                sFolioReceta = datos.Campo("Folio");
                sFechaReceta = datos.Campo("fechaReceta");
                sFechaEnvioReceta = DateTime.Now.ToString("yyyy-MM-dd");

                sFolioAfiliacionSPSS = datos.Campo("NumeroReferencia");
                sRangoVigencia = datos.Campo("vigenciaDerechos");
                ////sVigencias = sRangoVigencia.Split('-'); 
                ////sFechaIniciaVigencia = GetFecha(sVigencias[0]);
                ////sFechaTerminaVigencia = GetFecha(sVigencias[1]);

                //sFechaIniciaVigencia = GetFecha(datos.Campo("vigenciaDerechos"));
                sFechaTerminaVigencia = datos.Campo("FechaTerminaVigencia");
                sFechaIniciaVigencia = datos.Campo("FechaIniciaVigencia");


                //sExpediente = datos.Campo("expediente");
                sBeneficiarioNombre = datos.Campo("NombreBeneficiario");
                sBeneficiarioApPaterno = datos.Campo("apPaternoBeneficiario");
                sBeneficiarioApMaterno = datos.Campo("apMaternoBeneficiario");
                sBeneficiarioSexo = datos.Campo("sexo");
                sFechaNacimiento = datos.Campo("FechaNacimientoBeneficiario");

                sFolioOportunidades = datos.Campo("FolioAfiliacionOportunidades");
                sEsPoblacionAbierta = datos.Campo("EsPoblacionAbierta");

                sMedicoClave = datos.Campo("idMedico");
                sMedicoNombre = datos.Campo("NombreMedico");
                sMedicoApPaterno = datos.Campo("apPatMed");
                sMedicoApMaterno = datos.Campo("apMatMed");
                sMedicoCedula = datos.Campo("CedulaDeMedico");


                sSql = string.Format("Exec spp_INT_RE_INTERMED__RecetasElectronicas_001_XML " +
                    " @UMedica = '{0}', @Folio_SIADISSEP = '{1}', @InformacionXML = '{2}', @TipoDeProceso= {3}   ",
                    sClues_Emisor, sFolioReceta, sInformacionXML, 0);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Guardar_General()");
                    bRegresa = false;
                    break;
                }

                ////sIntervencionCauses = datos.Campo("noIntervCause");
                sSql = string.Format("Exec spp_INT_RE_INTERMED__RecetasElectronicas_0001_General " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @CLUES_Emisora = '{3}', @FolioReceta = '{4}', " + 
                    " @FechaReceta = '{5}', @FechaEnvioReceta = '{6}', @FolioAfiliacionSPSS = '{7}', @FechaIniciaVigencia = '{8}', @FechaTerminaVigencia = '{9}', " + 
                    " @Expediente = '{10}', @NombreBeneficiario = '{11}', @ApPaternoBeneficiario = '{12}', @ApMaternoBeneficiario = '{13}', @Sexo = '{14}', " + 
                    " @FechaNacimientoBeneficiario = '{15}', @FolioAfiliacionOportunidades = '{16}', @EsPoblacionAbierta = '{17}', " + 
                    " @ClaveDeMedico = '{18}', @NombreMedico = '{19}', @ApPaternoMedico = '{20}', @ApMaternoMedico = '{21}', @CedulaDeMedico = '{22}'  ",
                    sIdEmpresa, sIdEstado, sIdFarmacia,
                    sClues_Emisor, sFolioReceta, sFechaReceta, sFechaEnvioReceta,
                    sFolioAfiliacionSPSS, sFechaIniciaVigencia, sFechaTerminaVigencia,

                    sExpediente, sBeneficiarioNombre, sBeneficiarioApPaterno, sBeneficiarioApMaterno, sBeneficiarioSexo, sFechaNacimiento,
                    sFolioOportunidades, sEsPoblacionAbierta,
                    sMedicoClave, sMedicoNombre, sMedicoApPaterno, sMedicoApMaterno, sMedicoCedula
                    );

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Guardar_General()");
                    bRegresa = false;
                    break;
                }
            }

            if (bRegresa)
            {
                bRegresa = Guardar_002_Causes(Informacion);
            }

            return bRegresa;
        }

        private bool Guardar_002_Causes(DataSet Informacion)
        {
            bool bRegresa = true;
            string sSql = "";
            string sIntervencionCauses = "";
            string sFolio = "";

            clsLeer datos = new clsLeer();
            datos.DataSetClase = Informacion;
            datos.DataTableClase = datos.Tabla("Causes");
            //bRegresa = datos.Registros > 0;

            while (datos.Leer())
            {
                sFolio = datos.Campo("Folio");
                sIntervencionCauses = datos.Campo("NoIntervencionCause");
                sSql = string.Format("Exec spp_INT_RE_INTERMED__RecetasElectronicas_0002_Causes " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioReceta = '{3}', @NoIntervencionCause = '{4}', @Clues_Emisor = '{5}'  ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sFolio, sIntervencionCauses, sClues_Emisor);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Guardar_002_Causes()");
                    bRegresa = false;
                    break;
                }
            }


            if (bRegresa)
            {
                bRegresa = Guardar_003_Diagnosticos(Informacion);
            }

            return bRegresa;
        }

        private bool Guardar_003_Diagnosticos(DataSet Informacion)
        {
            bool bRegresa = true;
            string sSql = "";
            string sCIE10 = "";
            string sDescripcionDiagnostico = "";
            string sFolio = "";

            clsLeer datos = new clsLeer();
            datos.DataSetClase = Informacion;
            datos.DataTableClase = datos.Tabla("Diagnosticos");
            //bRegresa = datos.Registros > 0;

            while (datos.Leer())
            {
                sFolio = datos.Campo("Folio");
                sCIE10 = datos.Campo("CIE10");
                sDescripcionDiagnostico = datos.Campo("descripcion");

                sSql = string.Format("Exec spp_INT_RE_INTERMED__RecetasElectronicas_0003_Diagnosticos " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioReceta = '{3}', @CIE10 = '{4}', @DescripcionDiagnostico = '{5}', @Clues_Emisor = '{6}' ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sFolio, sCIE10, sDescripcionDiagnostico, sClues_Emisor);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Guardar_003_Diagnosticos()");
                    bRegresa = false;
                    break;
                }
            }


            if (bRegresa)
            {
                bRegresa = Guardar_004_Medicamentos(Informacion);
            }

            return bRegresa;
        }

        private bool Guardar_004_Medicamentos(DataSet Informacion)
        {
            bool bRegresa = true;
            string sSql = "";
            string sClaveSSA = "";
            int iCantidad = 0;
            int ICantidadEntregada = 0;
            string sFolio = "";


            clsLeer datos = new clsLeer();
            datos.DataSetClase = Informacion;
            datos.DataTableClase = datos.Tabla("Insumos");
            //bRegresa = datos.Registros > 0;

            while (datos.Leer())
            {
                sClaveSSA = datos.Campo("ClaveSSA");
                iCantidad = datos.CampoInt("CantidadRequerida");
                ICantidadEntregada = datos.CampoInt("CantidadEntregada");
                sFolio = datos.Campo("Folio");

                sSql = string.Format("Exec spp_INT_RE_INTERMED__RecetasElectronicas_0004_Insumos " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioReceta = '{3}', @ClaveSSA = '{4}', " +
                    " @CantidadRequerida = '{5}', @CantidadEntregada = '{6}', @Clues_Emisor = '{7}' ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sFolio, sClaveSSA, iCantidad, ICantidadEntregada, sClues_Emisor);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Guardar_004_Medicamentos()");
                    bRegresa = false;
                    break;
                }
            }

            return bRegresa;
        }

        #endregion funciones y procedimientos privados

    }
}
