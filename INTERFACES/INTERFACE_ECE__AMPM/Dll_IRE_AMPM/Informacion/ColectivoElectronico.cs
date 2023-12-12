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
    public class ColectivoElectronico
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
        string sFolio_SIADISSEP = "";
        string sExpediente_SIADISSEP = "";
        string sFolioReceta_SIADISSEP = ""; 
        string sFolioRegistro = "";
        string sMensajesError = "";
        TipoProcesoReceta tpProceso = TipoProcesoReceta.Ninguno;
        string sTipoDeProceso = "";
        string sCLUES = "";
        string sCLUES_NombreUnidad = ""; 

        DataSet dtsInformacion = new DataSet();

        basGenerales Fg = new basGenerales();
        string sEncabezado = "";
        Dictionary<string, string> listaProcesos = new Dictionary<string, string>();

        #region Constructor de Clase 
        public ColectivoElectronico(clsDatosConexion DatosConexion)
        {
            datosDeConexion = DatosConexion;
            cnn = new clsConexionSQL(datosDeConexion);
            leer = new clsLeer(ref cnn);
            leerXML = new clsLeer();

            Error = new clsGrabarError(datosDeConexion, GnDll_SII_AMPM.DatosApp, "ColectivoElectronico");
            Error.NombreLogErorres = "INT_AMPM__CtlErrores";

            sEncabezado = string.Format("<?xml version={0}1.0{0} encoding={0}UTF-8{0}?>", Fg.Comillas());


            listaProcesos = new Dictionary<string, string>();
            listaProcesos.Add(TipoProcesoReceta.ColectivoMedicamentos.ToString().ToUpper(), TipoProcesoReceta.ColectivoMedicamentos.ToString().ToUpper());
        }
        #endregion Constructor de Clase

        #region Propiedades Publicas 
        public string CLUES_Default
        {
            get { return sCLUES; }
            set { sCLUES = value; } 
        }

        public string CLUES_NombreUnidad_Default
        {
            get { return sCLUES_NombreUnidad; }
            set { sCLUES_NombreUnidad = value; } 
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
            sFolio_SIADISSEP = datos.Campo("idPeticion");
            sTipoDeProceso = datos.Campo("tipoPeticion").ToUpper();

            if (sTipoDeProceso == TipoProcesoReceta.SurteReceta.ToString().ToUpper()) proceso = TipoProcesoReceta.SurteReceta;
            if (sTipoDeProceso == TipoProcesoReceta.AcuseSurteReceta.ToString().ToUpper()) proceso = TipoProcesoReceta.AcuseSurteReceta;
            if (sTipoDeProceso == TipoProcesoReceta.CancelaReceta.ToString().ToUpper()) proceso = TipoProcesoReceta.CancelaReceta;
            if (sTipoDeProceso == TipoProcesoReceta.ColectivoMedicamentos.ToString().ToUpper()) proceso = TipoProcesoReceta.ColectivoMedicamentos; 

            return proceso; 
        }

        public ResponseRecetaElectronica Guardar(string Informacion_XML)
        {
            ResponseRecetaElectronica respuesta = new ResponseRecetaElectronica();
            clsLeer datos = new clsLeer();
            tpProceso = TipoProcesoReceta.ColectivoMedicamentos;


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
            sIdUMedica = sIdUMedica == "" ? sCLUES : sIdUMedica; 
            sFolio_SIADISSEP = datos.Campo("idPeticion");
            sTipoDeProceso = datos.Campo("tipoPeticion");


            //if (tpProceso.ToString().ToUpper() != sTipoDeProceso.ToUpper())
            if ( !listaProcesos.ContainsKey(sTipoDeProceso.ToUpper()) )
            {
                respuesta.Estatus = 101;
                respuesta.Error = "Tipo de petición no valida para el proceso solicitado.";
            }
            else
            {
                sIdEmpresa = "";
                sIdEstado = "";
                sIdFarmacia = "";
                sFolioRegistro = "";
                Guardar_001_XML__Log(respuesta);

                if (!cnn.Abrir())
                {
                    respuesta.Estatus = 1;
                    respuesta.Error = "Error de conexión con el servidor de datos.";
                }
                else
                {
                    cnn.IniciarTransaccion();

                    if (Guardar_001_XML(respuesta))
                    {
                        respuesta.ResetItemsRespuesta();
                        respuesta.AddItemRespuesta("idPeticion", sFolio_SIADISSEP);
                        //respuesta.AddItemRespuesta("expediente", sExpediente_SIADISSEP);
                        respuesta.AddItemRespuesta("idColectivo", sFolioReceta_SIADISSEP); 

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

            return respuesta; 
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
            bool bRegresa = false;
            string sSql = "";
            ///string sFolio_SIADISSEP = "";


            sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0000_XML__Log " +
                " @UMedica = '{0}', @Folio_SIADISSEP = '{1}', @InformacionXML = '{2}', @TipoDeProceso = '{3}' ",
                sIdUMedica, sFolio_SIADISSEP, sInformacion_XML, (int)tpProceso);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Guardar_001_XML__Log"); 
                Respuesta.Estatus = 201;
                Respuesta.Error = "Error al registrar la información en el log";
            }
            else
            {
                if (!leer.Leer())
                {
                    Respuesta.Estatus = 3;
                    Respuesta.Error = "Folio interno no asignado";
                }
                else
                {
                    sIdEmpresa = leer.Campo("IdEmpresa");
                    sIdEstado = leer.Campo("IdEstado");
                    sIdFarmacia = leer.Campo("IdFarmacia");
                    sFolioRegistro = leer.Campo("Folio");

                    ////bRegresa = Guardar_002_General(Respuesta);
                    bRegresa = true; 
                }
            }

            return bRegresa;
        }

        private bool Guardar_001_XML(ResponseRecetaElectronica Respuesta)
        {
            bool bRegresa = false;
            string sSql = "";
            ///string sFolio_SIADISSEP = "";


            sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_001_XML " +
                " @UMedica = '{0}', @Folio_SIADISSEP = '{1}', @InformacionXML = '{2}', @TipoDeProceso = '{3}' ",
                sIdUMedica, sFolio_SIADISSEP, sInformacion_XML, (int)tpProceso); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Guardar_001_XML"); 
                Respuesta.Estatus = 201;
                Respuesta.Error = "Error al registrar la información";
            }
            else
            {
                if (!leer.Leer())
                {
                    Respuesta.Estatus = 3;
                    Respuesta.Error = "Folio interno no asignado";
                }
                else
                {
                    sIdEmpresa = leer.Campo("IdEmpresa");
                    sIdEstado = leer.Campo("IdEstado");
                    sIdFarmacia = leer.Campo("IdFarmacia"); 
                    sFolioRegistro = leer.Campo("Folio");

                    bRegresa = Guardar_002_General(Respuesta);
                    ////bRegresa = true; 
                }
            }

            return bRegresa; 
        }

        private string ObtenerNombreUnidad()
        {
            string sRegresa = "";
            string sSql = string.Format(
                "Select F.IdEstado, F.IdFarmacia, F.NombreFarmacia as Farmacia, E.Referencia_SIADISSEP " + 
                "From CatFarmacias F (NoLock) " +
                "Inner Join INT_SIADISSEP__CFG_Farmacias_UMedicas E (NoLock) On ( F.IdEstado = E.IdEstado and F.IdFarmacia = E.IdFarmacia ) " + 
                "Where Referencia_SIADISSEP = '{0}' ", sIdUMedica);

            if (!leer.Exec(sSql))
            {
                sRegresa = sIdUMedica; 
            }
            else
            {
                leer.Leer();
                sRegresa = leer.Campo("Farmacia"); 
            }

            return sRegresa; 
        }

        private bool Guardar_002_General(ResponseRecetaElectronica Respuesta)
        {
            bool bRegresa = false;
            string sSql = "";
            DateTime dtVigencia = DateTime.Now; 

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

            clsLeer datos = new clsLeer();
            datos.DataTableClase = leerXML.Tabla("General");
            bRegresa = datos.Registros > 0;

            while (datos.Leer())
            {
                sCLUES_NombreUnidad = datos.Campo("nombreBeneficiario");
                sCLUES_NombreUnidad = sCLUES_NombreUnidad == "" ? ObtenerNombreUnidad() : sCLUES_NombreUnidad;


                sFolioReceta = datos.Campo("idColectivo");
                sFechaReceta = GetFecha(datos.Campo("fechaHoraElaboracion")) + " " + GetHora(datos.Campo("fechaHoraElaboracion"));
                sFechaEnvioReceta = GetFecha(datos.Campo("fechaHoraElaboracion")) + " " + GetHora(datos.Campo("fechaHoraElaboracion"));

                sFolioAfiliacionSPSS = sIdUMedica; // datos.Campo("polizaPaciente"); // folioAfiliacionSPSS
                sRangoVigencia = datos.Campo("vigenciaDerechos");

                sFechaTerminaVigencia = GetFecha(datos.Campo("vigenciaPolizaPaciente"));
                sFechaIniciaVigencia = GetFechaIniciaVigencia(sFechaTerminaVigencia);


                if ( sFechaTerminaVigencia == "" || sFechaIniciaVigencia == "")
                {
                    dtVigencia = DateTime.Now;
                    dtVigencia = dtVigencia.AddYears(10); 
                    sFechaTerminaVigencia = GetFecha(General.FechaYMD(dtVigencia));
                    

                    dtVigencia = DateTime.Now;
                    sFechaIniciaVigencia = GetFechaIniciaVigencia(General.FechaYMD(dtVigencia));
                }


                sExpediente = sIdUMedica; // datos.Campo("expediente"); 
                sBeneficiarioNombre = sCLUES_NombreUnidad; // datos.Campo("nombrePaciente");
                sBeneficiarioApPaterno = sIdUMedica; // datos.Campo("apPaternoPaciente");
                sBeneficiarioApMaterno = ""; // datos.Campo("apMaternoPaciente");
                sBeneficiarioSexo = "M"; // datos.Campo("sexo");
                sFechaNacimiento = GetFecha(datos.Campo("fechaNacimiento"));


                if (sFechaNacimiento == "")
                {
                    dtVigencia = DateTime.Now;
                    dtVigencia = dtVigencia.AddYears(-3);
                    sFechaNacimiento = GetFecha(General.FechaYMD(dtVigencia));
                }


                sFolioOportunidades = sIdUMedica; // datos.Campo("oportunidades");
                sEsPoblacionAbierta = datos.Campo("poblacionAbierta");

                sMedicoClave = sIdUMedica; // datos.Campo("idMedico");
                sMedicoNombre = sCLUES_NombreUnidad;  //datos.Campo("nombreMedico");
                sMedicoApPaterno = sIdUMedica; // datos.Campo("apPatMed");
                sMedicoApMaterno = ""; // datos.Campo("apMatMed");
                sMedicoCedula = sIdUMedica; // datos.Campo("cedulaProfesional");



                sExpediente_SIADISSEP = sExpediente;
                sFolioReceta_SIADISSEP = sFolioReceta; 

                ////sIntervencionCauses = datos.Campo("noIntervCause");
                sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0001_General " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @FolioReceta = '{4}', " + 
                    " @FechaReceta = '{5}', @FechaEnvioReceta = '{6}', @FolioAfiliacionSPSS = '{7}', @FechaIniciaVigencia = '{8}', @FechaTerminaVigencia = '{9}', " + 
                    " @Expediente = '{10}', @NombreBeneficiario = '{11}', @ApPaternoBeneficiario = '{12}', @ApMaternoBeneficiario = '{13}', @Sexo = '{14}', " +
                    " @FechaNacimientoBeneficiario = '{15}', @FolioAfiliacionOportunidades = '{16}', @EsPoblacionAbierta = '{17}', " + 
                    " @ClaveDeMedico = '{18}', @NombreMedico = '{19}', @ApPaternoMedico = '{20}', @ApMaternoMedico = '{21}', @CedulaDeMedico = '{22}'  ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sFolioRegistro, 

                    sFolioReceta, sFechaReceta, sFechaEnvioReceta, 
                    sFolioAfiliacionSPSS, sFechaIniciaVigencia, sFechaTerminaVigencia,

                    sExpediente, sBeneficiarioNombre, sBeneficiarioApPaterno, sBeneficiarioApMaterno, sBeneficiarioSexo, sFechaNacimiento, 
                    sFolioOportunidades, sEsPoblacionAbierta,
                    sMedicoClave, sMedicoNombre, sMedicoApPaterno, sMedicoApMaterno, sMedicoCedula
                    ); 

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Guardar_002_General"); 
                    Respuesta.Estatus = 202;
                    Respuesta.Error = "Error al registrar la información";
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

        private bool Guardar_003_Causes(ResponseRecetaElectronica Respuesta)
        {
            bool bRegresa = false;
            string sSql = "";
            string sIntervencionCauses = "";


            clsLeer datos = new clsLeer();
            datos.DataTableClase = leerXML.Tabla("Causes");
            bRegresa = datos.Registros > 0;

            while (datos.Leer())
            {
                sIntervencionCauses = datos.Campo("noIntervCause");
                sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0002_Causes " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @NoIntervencionCause = '{4}'  ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sFolioRegistro, sIntervencionCauses);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Guardar_003_Causes"); 
                    Respuesta.Estatus = 203;
                    Respuesta.Error = "Error al registrar la información";
                    bRegresa = false;
                    break;
                }
            }


            if (bRegresa)
            {
                bRegresa = Guardar_004_Diagnosticos(Respuesta);
            }

            return bRegresa;
        }

        private bool Guardar_004_Diagnosticos(ResponseRecetaElectronica Respuesta)
        {
            bool bRegresa = false;
            string sSql = "";
            string sCIE10 = "";
            string sDescripcionDiagnostico = "";


            clsLeer datos = new clsLeer();
            datos.DataTableClase = leerXML.Tabla("Diagnostico");
            bRegresa = datos.Registros > 0;

            while (datos.Leer())
            {
                sCIE10 = datos.Campo("idDiagnostico");
                sDescripcionDiagnostico = datos.Campo("descDiagnostico");

                sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0003_Diagnosticos " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @CIE10 = '{4}', @DescripcionDiagnostico = '{5}' ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sFolioRegistro, sCIE10, sDescripcionDiagnostico);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Guardar_004_Diagnosticos"); 
                    Respuesta.Estatus = 204;
                    Respuesta.Error = "Error al registrar la información";
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

        private bool Guardar_005_Medicamentos(ResponseRecetaElectronica Respuesta)
        {
            bool bRegresa = true;
            string sSql = "";
            string sClaveSSA = "";
            int iCantidad = 0;


            clsLeer datos = new clsLeer();
            datos.DataTableClase = leerXML.Tabla("Medicamento");
            bRegresa = datos.Registros > 0;

            while (datos.Leer())
            {
                sClaveSSA = datos.Campo("ClaveMed");
                iCantidad = datos.CampoInt("cantidadSolicitadaMed");

                sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0004_Insumos " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @ClaveSSA = '{4}', @CantidadRequerida = '{5}', @TipoDeInsumo = 1 ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sFolioRegistro, sClaveSSA, iCantidad);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Guardar_005_Medicamentos"); 
                    Respuesta.Estatus = 205;
                    Respuesta.Error = "Error al registrar la información";
                    bRegresa = false;
                    break; 
                }
            }

            if (bRegresa)
            {
                bRegresa = Guardar_006_Soluciones(Respuesta);
            }

            return bRegresa;
        }

        private bool Guardar_006_Soluciones(ResponseRecetaElectronica Respuesta)
        {
            bool bRegresa = true;
            string sSql = "";
            string sClaveSSA = "";
            int iCantidad = 0;


            clsLeer datos = new clsLeer();
            datos.DataTableClase = leerXML.Tabla("Solucion");
            bRegresa = datos.Registros > 0;

            while (datos.Leer())
            {
                sClaveSSA = datos.Campo("ClaveSol");
                iCantidad = datos.CampoInt("cantidadSolicitadaSol");

                sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0004_Insumos " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @ClaveSSA = '{4}', @CantidadRequerida = '{5}', @TipoDeInsumo = 2 ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sFolioRegistro, sClaveSSA, iCantidad);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Guardar_006_Soluciones"); 
                    Respuesta.Estatus = 205;
                    Respuesta.Error = "Error al registrar la información";
                    bRegresa = false;
                    break;
                }
            }

            return bRegresa;
        }
        #endregion Guardar Informacion
    }
}
