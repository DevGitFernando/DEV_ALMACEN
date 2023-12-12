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

using Dll_IRE_SIGHO;
using Dll_IRE_SIGHO.wsClases;

namespace Dll_IRE_SIGHO.Informacion
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
        string sFolio_SIADISSEP = "";
        string sExpediente_SIADISSEP = "";
        string sFolioReceta_SIADISSEP = ""; 
        string sFolioRegistro = "";
        string sMensajesError = "";
        TipoProcesoReceta tpProceso = TipoProcesoReceta.Ninguno;
        string sTipoDeProceso = "";

        DataSet dtsInformacion = new DataSet();

        basGenerales Fg = new basGenerales();
        string sEncabezado = "";

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

        public RecetaElectronica(clsDatosConexion DatosConexion)
        {
            datosDeConexion = DatosConexion;
            cnn = new clsConexionSQL(datosDeConexion);
            leer = new clsLeer(ref cnn);
            leerXML = new clsLeer(); 

            Error = new clsGrabarError(datosDeConexion, GnDll_SII_RE_SIGHO.DatosApp, "RecetaElectronica");
            Error.NombreLogErorres = "INT_RE_SIGHO__CtlErrores";

            sEncabezado = string.Format("<?xml version={0}1.0{0} encoding={0}UTF-8{0}?>", Fg.Comillas());
        }

        public DataSet GetRecetas(string Clues, string TipoDocumento)
        {
            string sSql = string.Format("Exec spp_INT_RE_SIGHO__GetRecetas @Clues = '{0}', @TipoDocumento = '{1}'", Clues, TipoDocumento);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GetRecetas()");
            }

            leer.RenombrarTabla(1, "General");
            leer.RenombrarTabla(2, "Causes");
            leer.RenombrarTabla(3, "Diagnosticos");
            leer.RenombrarTabla(4, "Insumos");

            return leer.DataSetClase;
        }

        //public DataSet GetRecetaElectronica(DataSet dtsReceta)
        //{
        //    //string sSql = string.Format("Exec spp_INT_RE_SIGHO__GetRecetas_Especifico @Clues = '{0}', @FolioDeReceta = '{1}'", Clues, FolioDeReceta);

        //    leer.DataSetClase = dtsReceta;

        //    //if (!leer.Exec(sSql))
        //    //{
        //    //    Error.GrabarError(leer, "GetRecetaElectronica()");
        //    //}

        //    leer.RenombrarTabla(1, "General");
        //    leer.RenombrarTabla(2, "Causes");
        //    leer.RenombrarTabla(3, "Diagnosticos");
        //    leer.RenombrarTabla(4, "Insumos");

        //    return leer.DataSetClase;
        //}

        public ResponseRecetaElectronica Guardar(string Clues, DataSet Informacion)
        {
            ResponseRecetaElectronica respuesta = new ResponseRecetaElectronica();
            tpProceso = TipoProcesoReceta.SurteReceta;


            leer.DataSetClase = Informacion;

            //if (!leer.Exec(sSql))
            //{
            //    Error.GrabarError(leer, "GetRecetaElectronica()");
            //}

            leer.RenombrarTabla(1, "General");
            //leer.RenombrarTabla(2, "Causes");
            leer.RenombrarTabla(3, "Diagnosticos");
            leer.RenombrarTabla(5, "Insumos");

            dtsInformacion = new DataSet();
            dtsInformacion = Informacion;

            leerXML.DataSetClase = Informacion;


            if (!cnn.Abrir())
            {
                respuesta.HuboError = true;
                respuesta.Estatus = 1;
                respuesta.Error = "Error de conexión con el servidor de datos.";
            }
            else
            {
                cnn.IniciarTransaccion();

                if (GuardarConfirmacion(respuesta))
                {
                    respuesta.ResetItemsRespuesta();
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

            return respuesta; 
        }

        #region Funciones y Procedimientos privados
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
        #endregion Funciones y Procedimientos privados

        #region Guardar Informacion


        private bool GuardarConfirmacion(ResponseRecetaElectronica Respuesta)
        {
            bool bRegresa = false;
            string sSql = "";

            string sFolioReceta = "";
            string sClues = "";
            int iCantidadEntregada = 0;
            string sClaveSSA = "";

            clsLeer datos = new clsLeer();
            datos.DataSetClase = leerXML.DataSetClase; 
            bRegresa = datos.Registros > 0;

            while (datos.Leer())
            {
                sFolioReceta = datos.Campo("folioReceta");
                sClues = datos.Campo("CLUES");
                iCantidadEntregada = datos.CampoInt("CantidadEntregada");
                sClaveSSA = datos.Campo("ClaveSSA");


                ////sIntervencionCauses = datos.Campo("noIntervCause");
                sSql = string.Format("Update R Set CantidadSurtida = (R.CantidadSurtida + {2} ) " + 
                    "From REC_Recetas_ClavesSSA R " + 
                    "Inner Join CatUMedicas U (NoLock) On (U.IdEstado = R.IdEstado And U.IdUMedica = R.IdUMedica) " +
                    "Where FolioReceta = '{0}' And CLUES = '{1}' And ClaveSSA = '{3}' ",
                    sFolioReceta, sClues, iCantidadEntregada, sClaveSSA); 

                if (!leer.Exec(sSql))
                {
                    Respuesta.HuboError = true;
                    Respuesta.Estatus = 202;
                    Respuesta.Error = "Error al registrar la información";
                    bRegresa = false;
                    break;
                }
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
                sSql = string.Format("Exec spp_INT_RE_SIGHO__RecetasElectronicas_0002_Causes " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @NoIntervencionCause = '{4}'  ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sFolioRegistro, sIntervencionCauses);

                if (!leer.Exec(sSql))
                {
                    Respuesta.HuboError = true;
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

                sSql = string.Format("Exec spp_INT_RE_SIGHO__RecetasElectronicas_0003_Diagnosticos " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @CIE10 = '{4}', @DescripcionDiagnostico = '{5}' ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sFolioRegistro, sCIE10, sDescripcionDiagnostico);

                if (!leer.Exec(sSql))
                {
                    Respuesta.HuboError = true;
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
                sClaveSSA = datos.Campo("Clave");
                iCantidad = datos.CampoInt("cantidadRecetada"); 

                sSql = string.Format("Exec spp_INT_RE_SIGHO__RecetasElectronicas_0004_Insumos " + 
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @ClaveSSA = '{4}', @CantidadRequerida = '{5}' ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sFolioRegistro, sClaveSSA, iCantidad);

                if (!leer.Exec(sSql))
                {
                    Respuesta.HuboError = true;
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
