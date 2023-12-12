using System;
using System.Collections;
using System.Collections.Generic; 
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;

using Microsoft.VisualBasic;

using System.Text;
using System.IO;
using System.Configuration;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using Dll_IRE_AMPM;
using Dll_IRE_AMPM.wsClases;

namespace Dll_IRE_AMPM.Informacion
{
    public class RecetaElectronica
    {
        clsDatosConexion datosDeConexion;
        clsConexionSQL cnn;
        clsLeer leer;
        clsLeer leerXML; 
        clsGrabarError Error;

        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = ""; 

        string sIdUMedica = "";
        string sInformacion_XML = "";
        string sInformacion_XML_Converted = "";
        string sFolio_AMPM = "";
        string sExpediente_AMPM = "";
        string sFolioReceta_AMPM = ""; 
        string sFolioRegistro = "";
        string sMensajesError = "";
        TipoProcesoReceta tpProceso = TipoProcesoReceta.Ninguno;
        string sTipoDeProceso = "";
        string sCLUES = ""; 

        DataSet dtsInformacion = new DataSet();

        basGenerales Fg = new basGenerales();
        string sEncabezado = "";
        Dictionary<string, string> listaProcesos = new Dictionary<string, string>();

        #region Constructor de Clase 
        public RecetaElectronica(clsDatosConexion DatosConexion)
        {
            datosDeConexion = DatosConexion;
            cnn = new clsConexionSQL(datosDeConexion);
            leer = new clsLeer(ref cnn);
            leerXML = new clsLeer();

            Error = new clsGrabarError(datosDeConexion, GnDll_SII_AMPM.DatosApp, "RecetaElectronica");
            Error.NombreLogErorres = "INT_AMPM__CtlErrores";

            sEncabezado = string.Format("<?xml version={0}1.0{0} encoding={0}UTF-8{0}?>", Fg.Comillas());


            listaProcesos = new Dictionary<string, string>();
            listaProcesos.Add(TipoProcesoReceta.SurteReceta.ToString().ToUpper(), TipoProcesoReceta.SurteReceta.ToString().ToUpper());
        }
        #endregion Constructor de Clase

        #region Propiedades Publicas
        public string CLUES_Default
        {
            get { return sCLUES; }
            set { sCLUES = value; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        public string toUTF8(string Cadena)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(Cadena);
            return encoding.GetString(bytes);
        }

        public string Normalizar(string Cadena)
        {
            string sRegresa = Cadena; 

            string consignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
            string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";

            if (Cadena != "")
            {
                for (int i = 0; i <= consignos.Length - 1; i++)
                {
                    sRegresa = sRegresa.Replace(consignos[i], sinsignos[i]);
                }
            }

            return sRegresa; 
        }

        public DataSet PrepararInformacion(DataSet Informacion)
        {
            DataSet dtsRetorno = new DataSet();
            clsLeer leer = new clsLeer();

            leer.DataSetClase = Informacion;

            if (leer.ExisteTabla("Generales"))
            {
                leer.RenombrarTabla("Generales", "General"); 
            }

            return leer.DataSetClase.Copy(); 
        }

        public TipoProcesoReceta TipoDeDocumento(string Informacion_XML)
        {
            TipoProcesoReceta proceso = TipoProcesoReceta.Ninguno;
            clsLeer datos = new clsLeer();

            Informacion_XML = toUTF8(Fg.QuitarSaltoDeLinea(Informacion_XML.Replace("\t", "")));
            dtsInformacion = new DataSet();
            dtsInformacion.ReadXml(new XmlTextReader(new StringReader(Informacion_XML)));

            dtsInformacion = PrepararInformacion(dtsInformacion); 

            Informacion_XML = Normalizar(Informacion_XML);
            leerXML.DataSetClase = dtsInformacion;
            sInformacion_XML = Informacion_XML.Replace(sEncabezado, "");


            datos.DataTableClase = leerXML.Tabla("General");
            datos.Leer();
            sIdUMedica = datos.Campo("unidadMedica");
            sFolio_AMPM = datos.Campo("idPeticion");
            sTipoDeProceso = datos.Campo("tipoPeticion").ToUpper();

            if (sTipoDeProceso == TipoProcesoReceta.SurteReceta.ToString().ToUpper()) proceso = TipoProcesoReceta.SurteReceta;
            if (sTipoDeProceso == TipoProcesoReceta.AcuseSurteReceta.ToString().ToUpper()) proceso = TipoProcesoReceta.AcuseSurteReceta;
            if (sTipoDeProceso == TipoProcesoReceta.CancelaReceta.ToString().ToUpper()) proceso = TipoProcesoReceta.CancelaReceta;
            if (sTipoDeProceso == TipoProcesoReceta.ColectivoMedicamentos.ToString().ToUpper()) proceso = TipoProcesoReceta.ColectivoMedicamentos; 

            return proceso; 
        }

        public ResponseGeneral Guardar(string Referencia_IdClinica, string IdFarmacia, string Informacion_XML)
        {
            ResponseRecetaElectronica respuesta = new ResponseRecetaElectronica();
            ResponseGeneral response = new ResponseGeneral();
            Error = new clsGrabarError(General.DatosApp, "Guardar()");

            clsLeer datos = new clsLeer();
            tpProceso = TipoProcesoReceta.SurteReceta;

            XmlDocument xDoc = new XmlDocument();
            xDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(Informacion_XML, "DtsInformacion");
            sInformacion_XML_Converted = xDoc.InnerXml; 

            Informacion_XML = toUTF8(Fg.QuitarSaltoDeLinea(Informacion_XML.Replace("\t", ""))); 
            dtsInformacion = new DataSet();
            //dtsInformacion.ReadXml(new XmlTextReader(new StringReader(Informacion_XML)));
            dtsInformacion.ReadXml(new XmlTextReader(new StringReader(xDoc.InnerXml)));
            sInformacion_XML_Converted = dtsInformacion.GetXml(); 


            dtsInformacion = PrepararInformacion(dtsInformacion); 

            Informacion_XML = Normalizar(Informacion_XML); 
            leerXML.DataSetClase = dtsInformacion; 
            sInformacion_XML = Informacion_XML.Replace(sEncabezado, "");


            datos.DataTableClase = leerXML.Tabla("General");
            datos.Leer();
            sIdUMedica = Referencia_IdClinica; 



            //////////if (tpProceso.ToString().ToUpper() != sTipoDeProceso.ToUpper())
            ////if ( !listaProcesos.ContainsKey(sTipoDeProceso.ToUpper()) )
            ////{
            ////    respuesta.Estatus = 101;
            ////    respuesta.Error = "Tipo de petición no valida para el proceso solicitado.";
            ////}
            ////else
            {
                sIdEmpresa = "";
                sIdEstado = "";
                sIdFarmacia = "";
                sFolioRegistro = "";
                ////Guardar_001_XML__Log(respuesta);

                if (!cnn.Abrir())
                {
                    respuesta.Estatus = 1;
                    respuesta.Error = "Error de conexión con el servidor de datos.";
                }
                else
                {
                    cnn.IniciarTransaccion();

                    if (Guardar_001_XML(response))
                    {
                        response.Estatus = 0;
                        response.Mensaje = "Información integrada correctamente.";
 
                        respuesta.ResetItemsRespuesta();
                        respuesta.AddItemRespuesta("idPeticion", sFolio_AMPM);
                        respuesta.AddItemRespuesta("expediente", sExpediente_AMPM);
                        respuesta.AddItemRespuesta("folioReceta", sFolioReceta_AMPM); 

                        respuesta.Error = "Información integrada correctamente.";
                        cnn.CompletarTransaccion();
                    }
                    else
                    {
                        Error.GrabarError(leer, "");
                        cnn.DeshacerTransaccion();
                    }

                    cnn.Cerrar();
                }
            }

            return response; 
        }
        #endregion Funciones y Procedimientos Publicos
        
        #region Funciones y Procedimientos
        private string GetFechaIniciaVigencia(string Valor)
        {
            string sRegresa = "";

            try
            {
                sRegresa = General.FechaYMD(Convert.ToDateTime(Valor).AddYears(1*-3)); 
            }
            catch 
            { 
            }

            return sRegresa; 
        }

        private string GetFecha(string Valor)
        {
            string sRegresa = "";
            string[] sFecha = null; // Valor.Split('/');
            int EqualPosition = 0;

            try
            {
                Valor = Fg.Left(Valor, 10);
                //sFecha = Valor.Split('/');

                if (Valor.Contains("/") || Valor.Contains("-"))
                {
                    if (Valor.Contains("/"))
                    {
                        sFecha = Valor.Split('/');
                    }
                    else
                    {
                        sFecha = Valor.Split('-');
                    }
                }

                sRegresa = string.Format("{0}-{1}-{2}", sFecha[0], sFecha[1], sFecha[2]);
            }
            catch (Exception ex)
            {
                sRegresa = ex.Message;
                sRegresa = "";
            }

            return sRegresa;
        }

        private string GetHora(string Valor)
        {
            string sRegresa = "";
            //Valor = Fg.Right(Valor, 8);
            string[] sFecha = Valor.Split(' ');
            string[] sHora = null; 

            try
            {
                if (sFecha.Length == 2)
                {
                    sHora = sFecha[1].Split(':');
                    //sRegresa = string.Format("{0}:{1}:{2}", sFecha[0], sFecha[1], sFecha[2]);
                    sRegresa = string.Format("{0}:{1}:{2}", sHora[0], sHora[1], sHora[2]);
                }
            }
            catch (Exception ex)
            {
                sRegresa = ex.Message;
                sRegresa = "";
            }

            return sRegresa;
        }
        #endregion Funciones y Procedimientos

        #region Guardar Informacion
        public bool Guardar_001_XML__Log(ResponseRecetaElectronica Respuesta)
        {
            bool bRegresa = true;
            string sSql = "";
            ///string sFolio_AMPM = "";


            ////sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0000_XML__Log " +
            ////    " @UMedica = '{0}', @Folio_SIADISSEP = '{1}', @InformacionXML = '{2}', @TipoDeProceso = '{3}' ",
            ////    sIdUMedica, sFolio_AMPM, sInformacion_XML, (int)tpProceso);

            ////if (!leer.Exec(sSql))
            ////{
            ////    Respuesta.Estatus = 201;
            ////    Respuesta.Error = "Error al registrar la información en el log";
            ////}
            ////else
            ////{
            ////    if (!leer.Leer())
            ////    {
            ////        Respuesta.Estatus = 3;
            ////        Respuesta.Error = "Folio interno no asignado";
            ////    }
            ////    else
            ////    {
            ////        sIdEmpresa = leer.Campo("IdEmpresa");
            ////        sIdEstado = leer.Campo("IdEstado");
            ////        sIdFarmacia = leer.Campo("IdFarmacia");
            ////        sFolioRegistro = leer.Campo("Folio");

            ////        ////bRegresa = Guardar_002_General(Respuesta);
            ////        bRegresa = true; 
            ////    }
            ////}

            return bRegresa;
        }

        private bool Guardar_001_XML(ResponseGeneral Respuesta)
        {
            bool bRegresa = false;
            string sSql = "";
            ///string sFolio_AMPM = "";


            sSql = string.Format("Exec spp_INT_AMPM__RecetasElectronicas_001_XML " +
                "  @Referencia_IdClinica = '{0}', @Folio_AMPM = '{1}', @InformacionXML_Base = '{2}', @InformacionXML = '{3}', @TipoDeProceso = '{4}' ",
                sIdUMedica, sFolio_AMPM, sInformacion_XML_Converted, sInformacion_XML, (int)tpProceso); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Guardar_001_XML"); 
                Respuesta.Estatus = 201;
                Respuesta.Mensaje = "Error al registrar la información";
            }
            else
            {
                if (!leer.Leer())
                {
                    Respuesta.Estatus = 3;
                    Respuesta.Mensaje = "Folio interno no asignado";
                }
                else
                {
                    sIdEmpresa = leer.Campo("IdEmpresa");
                    sIdEstado = leer.Campo("IdEstado");
                    sIdFarmacia = leer.Campo("IdFarmacia"); 
                    sFolioRegistro = leer.Campo("Folio");
                    bRegresa = true;

                    bRegresa = Guardar_002_General(Respuesta);
                }
            }

            return bRegresa; 
        }

        private bool Guardar_002_General(ResponseGeneral Respuesta)
        {
            bool bRegresa = false;
            string sSql = "";

            string sFolioReceta = "";
            string sFechaReceta = "";
            string sFechaEnvioReceta = "";

            string sFolioConsulta = "";
            string sIdUsuario = "";
            string sIdEstudiosPaciente = "";
            string sIndicaciones = "";
            string sDiagnostico = "";
            string sCIE10 = "";

            string sFolioAfiliacionSPSS = "";
            string sFechaIniciaVigencia = "";
            string sFechaTerminaVigencia = "";

            string sBeneficiarioNombre = "";
            string sBeneficiarioApPaterno = "";
            string sBeneficiarioApMaterno = "";
            string sBeneficiarioSexo = "";
            string sExpediente = "";
            string sFechaNacimiento = "";
            string sNHC = "";

            string sMedicoClave = "";
            string sMedicoNombre = "";
            string sMedicoApPaterno = "";
            string sMedicoApMaterno = "";
            string sMedicoCedula = "";
            string sLicenciaturaDeMedico = "";
            string sFirmaImagen = "";
            string sprocedencia = "";



            clsLeer Datos_Informacion = new clsLeer();
            clsLeer DatosPaciente = new clsLeer();
            clsLeer DatosMedico = new clsLeer();


            Datos_Informacion.DataTableClase = leerXML.Tabla("DtsInformacion");
            DatosPaciente.DataTableClase = leerXML.Tabla("paciente");
            DatosMedico.DataTableClase = leerXML.Tabla("datos_medicos");

            bRegresa = Datos_Informacion.Registros > 0;
            while (Datos_Informacion.Leer())
            {

                //sFechaReceta = General.FechaSistemaObtener().ToString();

                sFolioReceta = Datos_Informacion.Campo("id_receta");
                sFolioAfiliacionSPSS = Datos_Informacion.Campo("folioAfiliacionSPSS");
                sFechaTerminaVigencia = GetFecha(Datos_Informacion.Campo("vigenciaDerechos"));
                sFechaIniciaVigencia = GetFechaIniciaVigencia(sFechaTerminaVigencia);
                sIdUsuario = Datos_Informacion.Campo("id_usuario");
                sExpediente = Datos_Informacion.Campo("id_expediente");
                sIdEstudiosPaciente = Datos_Informacion.Campo("id_estudios_paciente");
                sIndicaciones = Datos_Informacion.Campo("indicaciones");
                sDiagnostico = Datos_Informacion.Campo("diagnostico");
                sCIE10 = Datos_Informacion.Campo("cie10");


                DatosMedico.Leer();
                sMedicoClave = DatosMedico.Campo("idMedico");
                sMedicoNombre = DatosMedico.Campo("nombre_usuario");
                sMedicoApPaterno = DatosMedico.Campo("apaterno_usuario");
                sMedicoApMaterno = DatosMedico.Campo("amaterno_usuario");
                sMedicoCedula = DatosMedico.Campo("cedula_profesional");
                sLicenciaturaDeMedico = DatosMedico.Campo("licenciatura");
                sFirmaImagen = DatosMedico.Campo("firma");
                sprocedencia = DatosMedico.Campo("procedencia");

                DatosPaciente.Leer();
                sBeneficiarioNombre = DatosPaciente.Campo("nombre_paciente");
                sBeneficiarioApPaterno = DatosPaciente.Campo("apaterno_paciente");
                sBeneficiarioApMaterno = DatosPaciente.Campo("amaterno_paciente");
                sBeneficiarioSexo = DatosPaciente.Campo("id_sexo") == "2" ? "M" : "H";
                sFechaNacimiento = GetFecha(DatosPaciente.Campo("fecha_nacimiento"));
                sNHC = DatosPaciente.Campo("nhc");
                

                sExpediente_AMPM = sExpediente;
                sFolioReceta_AMPM = sFolioReceta; 

                ////sIntervencionCauses = datos.Campo("noIntervCause");
                sSql = string.Format(
                    "Exec spp_INT_AMPM__RecetasElectronicas_0001_General " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @FolioReceta = '{4}', " + 
                    " @FechaReceta = '{5}', @FechaEnvioReceta = '{6}', @FolioConsulta = '{7}', @IdUsuario = '{8}', " + 
	                " @IdEstudiosPaciente = '{9}', @Indicaciones = '{10}', @Diagnostico = '{11}', " +
                    " @FolioAfiliacionSPSS = '{12}', @FechaIniciaVigencia = '{13}', @FechaTerminaVigencia = '{14}', " + 
                    " @Expediente = '{15}', @NombreBeneficiario = '{16}', @ApPaternoBeneficiario = '{17}', " +
                    " @ApMaternoBeneficiario = '{18}', @Sexo = '{19}', @FechaNacimientoBeneficiario = '{20}',"  +
                    " @ClaveDeMedico = '{21}', @NombreMedico = '{22}', @ApPaternoMedico = '{23}', @ApMaternoMedico = '{24}', @CedulaDeMedico = '{25}',  "+
                    " @LicenciaturaDeMedico = '{26}', @FirmaImagen = '{27}', @procedencia = '{28}', @CIE10 =  '{29}', @NHC = '{30}' ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sFolioRegistro, sFolioReceta,
                    sFechaReceta, sFechaEnvioReceta, sFolioConsulta, sIdUsuario,
                    sIdEstudiosPaciente, sIndicaciones, sDiagnostico,
                    sFolioAfiliacionSPSS, sFechaIniciaVigencia, sFechaTerminaVigencia,
                    sExpediente, sBeneficiarioNombre, sBeneficiarioApPaterno,
                    sBeneficiarioApMaterno, sBeneficiarioSexo, sFechaNacimiento,
                    sMedicoClave, sMedicoNombre, sMedicoApPaterno, sMedicoApMaterno, sMedicoCedula,
                    sLicenciaturaDeMedico, sFirmaImagen, sprocedencia, sCIE10, sNHC
                    ); 

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Guardar_002_General"); 
                    Respuesta.Estatus = 202;
                    Respuesta.Mensaje = leer.MensajeError;// "Error al registrar la información";
                    bRegresa = false;
                    break;
                }
            }

            if (bRegresa)
            {
                bRegresa = Guardar_003_Somatometria(Respuesta);
            }

            return bRegresa; 
        }

        private bool Guardar_003_Somatometria(ResponseGeneral Respuesta)
        {
            bool bRegresa = false;
            string sSql = "";

            string sIdEnfermeria = "";
            double dEstatura = 0.0000;
            double dPeso = 0.0000;
            double dCintura = 0.0000;
            double dPerimetroCadera = 0.0000;
            double dDiametroCefalico = 0.0000;
            double dDiametroAbdominal = 0.0000;

            double dPresionSistolica = 0.0000;
            double dPresionDiastolica = 0.0000;
            double dFrecuenciaCardiaca = 0.0000;
            double dFrecuenciaRespiratoria = 0.0000;
            double dTemperatura = 0.0000;
            string sIdUsuario = "";
            string sFechaEnfermeria = "";
            string sHoraEnfermeria = "";

            string sGlucosa = "";
            string sTipoGlucosa = "";

            string sDiagnostico = "";
            string sObservaciones = "";

            clsLeer Datos_somatometria = new clsLeer();


            Datos_somatometria.DataTableClase = leerXML.Tabla("somatometria");

            bRegresa = Datos_somatometria.Registros > 0;

            while (Datos_somatometria.Leer())
            {
                sIdEnfermeria = Datos_somatometria.Campo("id_enfermeria"); ;
                dEstatura = Datos_somatometria.CampoDouble("estatura");
                dPeso = Datos_somatometria.CampoDouble("peso");
                dCintura = Datos_somatometria.CampoDouble("cintura");
                dPerimetroCadera = Datos_somatometria.CampoDouble("perimetro_cadera");
                dDiametroCefalico = Datos_somatometria.CampoDouble("diametro_cefalico");
                dDiametroAbdominal = Datos_somatometria.CampoDouble("diametro_abdominal");

                dPresionSistolica = Datos_somatometria.CampoDouble("presion_sistolica");
                dPresionDiastolica = Datos_somatometria.CampoDouble("presion_diastolica");
                dFrecuenciaCardiaca = Datos_somatometria.CampoDouble("frecuencia_cardiaca");
                dFrecuenciaRespiratoria = Datos_somatometria.CampoDouble("frecuencia_respiratoria");
                dTemperatura = Datos_somatometria.CampoDouble("temperatura");
                sIdUsuario = Datos_somatometria.Campo("id_usuario");
                sFechaEnfermeria = Datos_somatometria.Campo("fecha_enfermeria");
                sHoraEnfermeria = Datos_somatometria.Campo("hora_enfermeria");

                sGlucosa = Datos_somatometria.Campo("glucosa");
                sTipoGlucosa = Datos_somatometria.Campo("tipo_glucosa");

                sDiagnostico = Datos_somatometria.Campo("diagnostico");
                sObservaciones = Datos_somatometria.Campo("observaciones");

                ////sIntervencionCauses = datos.Campo("noIntervCause");
                sSql = string.Format("Exec spp_INT_AMPM__RecetasElectronicas_0002_Somatometria " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @IdEnfermeria = '{4}', " +
	                " @Estatura = '{5}', @Peso = '{6}', @Cintura = '{7}', @PerimetroCadera = '{8}', @DiametroCefalico = '{9}', " +
                    " @DiametroAbdominal = '{10}', @PresionSistolica = '{11}', @PresionDiastolica = '{12}', @FrecuenciaCardiaca = '{13}', " +
                    " @FrecuenciaRespiratoria = '{14}', @Temperatura = '{15}', @IdUsuario = '{16}', @FechaEnfermeria = '{17}', @HoraEnfermeria = '{18}', " +
                    " @Glucosa = '{19}', @TipoGlucosa = '{20}', @Diagnostico = '{21}', @Observaciones = '{22}' ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sFolioRegistro, sIdEnfermeria,
                    dEstatura, dPeso, dCintura, dPerimetroCadera, dDiametroCefalico,
                    dDiametroAbdominal, dPresionSistolica, dPresionDiastolica, dFrecuenciaCardiaca,
                    dFrecuenciaRespiratoria, dTemperatura, sIdUsuario, sFechaEnfermeria, sHoraEnfermeria,
                    sGlucosa, sTipoGlucosa, sDiagnostico, sObservaciones
                    );

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Guardar_003_Somatometria");
                    Respuesta.Estatus = 202;
                    Respuesta.Mensaje = leer.MensajeError;// "Error al registrar la información";
                    bRegresa = false;
                    break;
                }
            }

            if (bRegresa)
            {
                bRegresa = Guardar_005_Medicamentos(Respuesta);
            }

            return bRegresa;
        }

        //private bool Guardar_004_Diagnosticos(ResponseGeneral Respuesta)
        //{
        //    bool bRegresa = false;
        //    string sSql = "";
        //    string sCIE10 = "";
        //    string sDescripcionDiagnostico = "";

        //    clsLeer datos = new clsLeer();
        //    datos.DataTableClase = leerXML.Tabla("Diagnostico");
        //    bRegresa = datos.Registros > 0;

        //    while (datos.Leer())
        //    {
        //        sCIE10 = datos.Campo("idDiagnostico");
        //        sDescripcionDiagnostico = datos.Campo("descDiagnostico");

        //        sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0003_Diagnosticos " +
        //            " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @CIE10 = '{4}', @DescripcionDiagnostico = '{5}' ",
        //            sIdEmpresa, sIdEstado, sIdFarmacia, sFolioRegistro, sCIE10, sDescripcionDiagnostico);

        //        if (!leer.Exec(sSql))
        //        {
        //            Error.GrabarError(leer, "Guardar_004_Diagnosticos"); 
        //            Respuesta.Estatus = 204;
        //            Respuesta.Mensaje = "Error al registrar la información";
        //            bRegresa = false;
        //            break;
        //        }
        //    }


        //    if (bRegresa)
        //    {
        //        bRegresa = Guardar_005_Medicamentos(Respuesta);
        //    }

        //    return bRegresa;
        //}

        private bool Guardar_005_Medicamentos(ResponseGeneral Respuesta)
        {
            bool bRegresa = true;
            string sSql = "";


            string sCodigoEAN = "";

	        int iCantidadRequerida = 0;
            int iCantidadEntregada = 0;
	
	        string sIdMedicina = "";
	        string sVia = "";
	        string sDosis = "";
	        string sUnidades = "";
	        string sFrecuencia = "";
	        string sFechaInicio = "";
	        string sFechaFin = "";
	        string sObservaciones = "";
            int iExistencia = 0;
            bool bTextoLibre = false;
            string sDescripcionGenerica = "";


            clsLeer datos = new clsLeer();
            datos.DataTableClase = leerXML.Tabla("medicamentos");
            bRegresa = datos.Registros > 0;

            while (datos.Leer())
            {
                sCodigoEAN = datos.Campo("eancode");

                iCantidadRequerida = datos.CampoInt("cantidad");
                iCantidadEntregada = 0;

                sIdMedicina = datos.Campo("id_medicina");
                sVia = datos.Campo("via");
                sDosis = datos.Campo("dosis");
                sUnidades = datos.Campo("unidades");
                sFrecuencia = datos.Campo("frecuencia");
                sFechaInicio = datos.Campo("inicio");
                sFechaFin = datos.Campo("fin");
                sObservaciones = datos.Campo("observaciones");
                iExistencia = datos.CampoInt("existencia");
                bTextoLibre = datos.Campo("textoLibre") == "1" ? true:false;
                sDescripcionGenerica = datos.Campo("medicina");

                sSql = string.Format("Exec spp_INT_AMPM__RecetasElectronicas_0004_Insumos " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', " +
                    " @CodigoEAN = '{4}', @CantidadRequerida = '{5}', @CantidadEntregada = '{6}', @IdMedicina = '{7}', @Via = '{8}', @Dosis = '{9}', " +
                    " @Unidades = '{10}', @Frecuencia = '{11}', @FechaInicio = '{12}', @FechaFin = '{13}', @Observaciones = '{14}', " +
                    " @Existencia = {15}, @TextoLibre = '{16}', @DescripcionGenerica = '{17}' ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sFolioRegistro,
                    sCodigoEAN, iCantidadRequerida, iCantidadEntregada, sIdMedicina, sVia, sDosis, 
                    sUnidades, sFrecuencia, sFechaInicio, sFechaFin, sObservaciones,
                    iExistencia, bTextoLibre, sDescripcionGenerica
                    );

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Guardar_005_Medicamentos"); 
                    Respuesta.Estatus = 205;
                    Respuesta.Mensaje = "Error al registrar la información";
                    bRegresa = false;
                    break; 
                }
            }

            return bRegresa;
        }
        #endregion Guardar Informacion
    }
}
