using System;
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
    public class RecetaElectronica_Cancelacion
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

        DataSet dstInformacion = new DataSet();

        basGenerales Fg = new basGenerales();
        string sEncabezado = "";


        public RecetaElectronica_Cancelacion(clsDatosConexion DatosConexion)
        {
            datosDeConexion = DatosConexion;
            cnn = new clsConexionSQL(datosDeConexion);
            leer = new clsLeer(ref cnn);
            leerXML = new clsLeer(); 

            Error = new clsGrabarError(datosDeConexion, GnDll_SII_AMPM.DatosApp, "RecetaElectronica");
            Error.NombreLogErorres = "INT_AMPM__CtlErrores";

            sEncabezado = string.Format("<?xml version={0}1.0{0} encoding={0}UTF-8{0}?>", Fg.Comillas());
        }

        public ResponseRecetaElectronica Guardar(string Informacion_XML)
        {
            ResponseRecetaElectronica respuesta = new ResponseRecetaElectronica();
            clsLeer datos = new clsLeer();
            tpProceso = TipoProcesoReceta.CancelaReceta; 

            dstInformacion = new DataSet();
            dstInformacion.ReadXml(new XmlTextReader(new StringReader(Informacion_XML)));

            leerXML.DataSetClase = dstInformacion; 
            sInformacion_XML = Informacion_XML.Replace(sEncabezado, "");


            datos.DataTableClase = leerXML.Tabla("General");
            datos.Leer();
            sIdUMedica = datos.Campo("unidadMedica");
            sFolio_SIADISSEP = datos.Campo("idPeticion");
            sTipoDeProceso = datos.Campo("tipoPeticion");


            if (tpProceso.ToString().ToUpper() != sTipoDeProceso.ToUpper())
            {
                respuesta.Estatus = 101;
                respuesta.Error = "Tipo de petición no valida para el proceso solicitado.";
            }
            else
            {
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
                        respuesta.AddItemRespuesta("expediente", sExpediente_SIADISSEP);
                        respuesta.AddItemRespuesta("folioReceta", sFolioReceta_SIADISSEP); 


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

        #region Funciones y Procedimientos 
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
            Valor = Fg.Right(Valor, 8);
            string[] sFecha = Valor.Split(':');

            try
            {
                sRegresa = string.Format("{0}:{1}:{2}", sFecha[0], sFecha[1], sFecha[2]);
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

        private bool Guardar_002_General(ResponseRecetaElectronica Respuesta)
        {
            bool bRegresa = false;
            string sSql = "";

            string sFolioReceta = "";
            string sFechaReceta = "";
            string sFechaEnvioReceta = "";
            string sExpediente = "";

            clsLeer datos = new clsLeer();
            datos.DataTableClase = leerXML.Tabla("General");
            bRegresa = datos.Registros > 0;

            while (datos.Leer())
            {
                sFolioReceta = datos.Campo("folioReceta");
                //sFechaReceta = GetFecha(datos.Campo("fechaReceta"));
                //sFechaEnvioReceta = GetFecha(datos.Campo("fechaEnvio"));
                sFechaReceta = GetFecha(datos.Campo("fechaReceta")) + " " + GetHora(datos.Campo("fechaReceta"));
                sFechaEnvioReceta = GetFecha(datos.Campo("fechaEnvio")) + " " + GetHora(datos.Campo("fechaEnvio"));

                sExpediente = datos.Campo("expediente");


                sExpediente_SIADISSEP = sExpediente;
                sFolioReceta_SIADISSEP = sFolioReceta; 

                ////sIntervencionCauses = datos.Campo("noIntervCause");
                sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0005_CancelacionRecetas " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', " + 
                    " @FolioReceta = '{4}', @FechaReceta = '{5}', @FechaEnvioReceta = '{6}', @Expediente = '{7}' ", 
                    sIdEmpresa, sIdEstado, sIdFarmacia, sFolioRegistro,
                    sFolioReceta, sFechaReceta, sFechaEnvioReceta, sExpediente); 

                if (!leer.Exec(sSql))
                {
                    Respuesta.Estatus = 202;
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
