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

using DllFarmaciaSoft; 

using Dll_ISIADISSEP;
using Dll_ISIADISSEP.wsClases;
using Dll_ISIADISSEP.wsAcuseProcesos_cnnInterna; 

namespace Dll_ISIADISSEP.wsClases
{
    public class ResponseAcuseXML
    {
        clsDatosConexion datosDeConexion;
        clsConexionSQL cnn;
        clsLeer leer;
        clsLeer leer_Auxiliar;
        clsLeer leerFolios;
        clsLeer leerFolios_Detalles; 
        clsLeer leerXML;
        clsGrabarError Error;

        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";

        string sIdEmpresa_Aux = "";
        string sIdEstado_Aux = "";
        string sIdFarmacia_Aux = "";


        string sIdUMedica = "";
        string sInformacion_XML = "";
        string sFolio_SIADISSEP = "";
        string sFolioRegistro = "";
        string sMensajesError = "";
        TipoProcesoReceta tpProceso = TipoProcesoReceta.Ninguno;
        string sTipoDeProceso = "";

        DataSet dtsInformacion = new DataSet();

        basGenerales Fg = new basGenerales();
        string sEncabezado = "";


        int iErroresEnvioAcusesReceta = 0;
        int iErroresEnvioAcusesCancelacionReceta = 0;

        wsAcuseProcesos_cnnInterna.wsISIADISSEP web = new wsAcuseProcesos_cnnInterna.wsISIADISSEP();

        clsLeer leerDatosGenerales = new clsLeer(); 
        string sTB_General = "General";
        string sTB_Medicamentos = "Medicamentos";
        string sTB_Soluciones = "Soluciones";
        string sTB_Vale = "Vale";
        string sTB_SurteOtraUnidad = "SurteOtraUnidad";
        string sTB_SurteOtraUnidad_Medicamentos = "SurteOtraUnidad_Medicamentos";
        string sXML_Generado = "";
        string sRespuesta_Acuse = "";
        string sRespuesta_Acuse_Respuestas = "";

        int iTab_00 = (int)Tabuladores.T00;
        int iTab_01 = (int)Tabuladores.T01;
        int iTab_02 = (int)Tabuladores.T02;
        int iTab_03 = (int)Tabuladores.T03;
        int iTab_04 = (int)Tabuladores.T04;
        int iTab_05 = (int)Tabuladores.T05;

        public ResponseAcuseXML(clsDatosConexion DatosConexion, string Empresa, string Estado, string Farmacia)
        {
            datosDeConexion = DatosConexion;
            cnn = new clsConexionSQL(datosDeConexion);
            leer = new clsLeer(ref cnn);
            leer_Auxiliar = new clsLeer(ref cnn); 
            leerFolios = new clsLeer(ref cnn);
            leerFolios_Detalles = new clsLeer(ref cnn); 
            leerXML = new clsLeer();

            Error = new clsGrabarError(datosDeConexion, GnDll_SII_SIADISSEP.DatosApp, "ResponseAcuseXML");
            Error.NombreLogErorres = "INT_SIADISSEP__CtlErrores";

            sEncabezado = string.Format("<?xml version={0}1.0{0} encoding={0}UTF-8{0}?>", Fg.Comillas());

            sIdEmpresa = Fg.PonCeros(Empresa, 3);
            sIdEstado = Fg.PonCeros(Estado, 2);
            sIdFarmacia = Fg.PonCeros(Farmacia, 4);

            sIdEmpresa_Aux = sIdEmpresa;
            sIdEstado_Aux = sIdEstado;
            sIdFarmacia_Aux = sIdFarmacia; 


            web = new wsAcuseProcesos_cnnInterna.wsISIADISSEP();

            GnDll_SII_SIADISSEP.ObtenerURL_Interface(IdEmpresa, IdEstado, IdFarmacia);
            web.Url = GnDll_SII_SIADISSEP.URL_Interface;
        }

        #region Propiedades Publicas 
        public string XML_Generado
        {
            get { return sXML_Generado;  }
        }

        public string Respuesta_Acuse
        {
            get { return sRespuesta_Acuse_Respuestas; }
        }

        public string IdEmpresa
        {
            get { return sIdEmpresa; }
            set { sIdEmpresa = value; }
        }

        public string IdEstado
        {
            get { return sIdEstado; }
            set { sIdEstado = value; }
        }

        public string IdFarmacia
        {
            get { return sIdFarmacia; }
            set { sIdFarmacia = value; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos 
        #region Acuses de surtido de recetas 
        public bool GeneralEnviarAcusesReceta()
        {
            bool bRegresa = true;
            string sSql = 
                " Select IdEmpresa, IdEstado, IdFarmacia, count(*) as Registros "+ 
                " From INT_SIADISSEP__RecetasElectronicas_0001_General (NoLock)  " +
                " Where Procesado = 0 " +
                " Group by IdEmpresa, IdEstado, IdFarmacia ";

            if (!leer_Auxiliar.Exec(sSql))
            {
                GnDll_SII_SIADISSEP.MensajePantalla("ERROR: " + leer_Auxiliar.MensajeError); 
            }
            else
            {
                while (leer_Auxiliar.Leer())
                {
                    sIdEmpresa = Fg.PonCeros(leer_Auxiliar.Campo("IdEmpresa"), 3);
                    sIdEstado = Fg.PonCeros(leer_Auxiliar.Campo("IdEstado"), 2);
                    sIdFarmacia = Fg.PonCeros(leer_Auxiliar.Campo("IdFarmacia"), 4);

                    EnviarAcusesReceta(true); 
                }
            }

            sIdEmpresa_Aux = sIdEmpresa;
            sIdEstado_Aux = sIdEstado;
            sIdFarmacia_Aux = sIdFarmacia; 

            return bRegresa; 
        }

        public bool EnviarAcusesReceta()
        {
            return EnviarAcusesReceta(true);
        }

        public bool EnviarAcusesReceta(bool EnviarXml) 
        {
            bool bRegresa = true;
            string sFolioSurtido = ""; 
            string sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0006_RecetasEnviarAcuse " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TipoDeProceso = '{3}' ", 
                sIdEmpresa, sIdEstado, sIdFarmacia, (int)TipoProcesoReceta.SurteReceta);

            iErroresEnvioAcusesReceta = 0;
            if (!leerFolios.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerFolios, "EnviarAcusesReceta()"); 
            }
            else
            {
                ///////// Renombrar las tablas 
                ////leer.RenombrarTabla(1, sTB_General);
                ////leer.RenombrarTabla(2, sTB_Medicamentos);
                ////leer.RenombrarTabla(3, sTB_Vale);
                ////leer.RenombrarTabla(4, sTB_SurteOtraUnidad);
                ////leer.RenombrarTabla(5, sTB_SurteOtraUnidad_Medicamentos);

                ////dtsInformacion = leer.DataSetClase;
                ////leerDatosGenerales.DataSetClase = dtsInformacion;

                sRespuesta_Acuse_Respuestas = "";
                while (leerFolios.Leer())
                {
                    sFolioSurtido = leerFolios.Campo("FolioVenta");
                    bRegresa = EnviarAcusesReceta(sFolioSurtido, EnviarXml);
                    if (bRegresa)
                    {
                        iErroresEnvioAcusesReceta++;
                    }
                }
            }

            return bRegresa; 
        }

        public bool EnviarAcusesReceta(string FolioVenta)
        {
            return EnviarAcusesReceta(FolioVenta, true); 
        }

        public bool EnviarAcusesReceta(string FolioVenta, bool EnviarXml)
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0006_RecetasEnviarAcuse_Detalles " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, FolioVenta);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "EnviarAcusesReceta()");
            }
            else
            {
                ///// Renombrar las tablas 
                leer.RenombrarTabla(1, sTB_General);
                leer.RenombrarTabla(2, sTB_Medicamentos);
                leer.RenombrarTabla(3, sTB_Vale);
                leer.RenombrarTabla(4, sTB_SurteOtraUnidad);
                leer.RenombrarTabla(5, sTB_SurteOtraUnidad_Medicamentos);

                dtsInformacion = leer.DataSetClase;
                leerDatosGenerales.DataSetClase = dtsInformacion;

                while (leer.Leer())
                {
                    bRegresa = EnviarAcusesReceta_Interno(leer.Campo("FolioVenta"), EnviarXml);
                    if (bRegresa)
                    {
                        iErroresEnvioAcusesReceta++;
                    }
                }
            }

            return bRegresa; 
        }

        private bool EnviarAcusesReceta_Interno(string FolioVenta, bool EnviarXml)
        {
            bool bRegresa = false;
            string sRegresa = ""; 
            string sSql = " ";
            string sFiltro = string.Format(" FolioVenta = '{0}' ", FolioVenta);
            string sFiltro_Medicamentos = ""; // string.Format(" FolioVenta = '{0}' ", FolioVenta);
            bool bXML_Generado = false;
            string Folio_SIADISSEP = "";
            string sFechaHoraSurtido = ""; 


            clsLeer leerGeneral = new clsLeer();
            clsLeer leerMedicamentos = new clsLeer();
            clsLeer leerMedicamentos_Vale = new clsLeer();
            clsLeer leerVale = new clsLeer();
            clsLeer leerSurteOtraUnidad = new clsLeer();
            clsLeer leerSurteOtraUnidad_Medicamentos = new clsLeer();

            Medicamento listaMedicamentos = new Medicamento();
            Medicamento listaMedicamentos_Vales = new Medicamento(); 
            Medicamento medicamento = new Medicamento();
            Vale vales = new Vale();
            SurteOtraUnidad surteOtraUnidad = new SurteOtraUnidad(); 

            leerGeneral.DataRowsClase = leerDatosGenerales.Tabla(sTB_General).Select(sFiltro);
            leerMedicamentos.DataRowsClase = leerDatosGenerales.Tabla(sTB_Medicamentos).Select(sFiltro);
            leerMedicamentos_Vale.DataRowsClase = leerDatosGenerales.Tabla(sTB_Vale).Select(sFiltro);
            leerVale.DataRowsClase = leerDatosGenerales.Tabla(sTB_Vale).Select(sFiltro);
            leerSurteOtraUnidad.DataRowsClase = leerDatosGenerales.Tabla(sTB_SurteOtraUnidad).Select(sFiltro);
            leerSurteOtraUnidad_Medicamentos.DataRowsClase = leerDatosGenerales.Tabla(sTB_SurteOtraUnidad_Medicamentos).Select(sFiltro);

            sXML_Generado = "";
            listaMedicamentos.Tipo = Medicamento.TipoDeMedicamento.MedicamentoSurtido;
            listaMedicamentos_Vales.Tipo = Medicamento.TipoDeMedicamento.MedicamentoVale; 

            if (leerGeneral.Leer())
            {
                bXML_Generado = true;
                tpProceso = TipoProcesoReceta.AcuseSurteReceta;
                Folio_SIADISSEP = leerGeneral.Campo("Folio_SIADISSEP");

                sFechaHoraSurtido = General.FechaYMD(leerGeneral.CampoFecha("FechaDeSurtimiento"), "/") + " ";
                //sFechaHoraSurtido += General.Hora(leerGeneral.CampoFecha("FechaDeSurtimiento"), ":");


                sXML_Generado += string.Format("{0}<{1}>\n", getTabs(iTab_00), "SurteReceta");

                sXML_Generado += string.Format("{0}<{1}>\n", getTabs(iTab_01), "General");
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "idPeticion", leerGeneral.Campo("Folio_SIADISSEP"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "tipoPeticion", tpProceso.ToString().ToUpper());
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "folioReceta", leerGeneral.Campo("FolioReceta"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "fechaReceta", General.FechaYMD(leerGeneral.CampoFecha("FechaReceta"), "/"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "fechaSurtimiento", sFechaHoraSurtido);
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "expediente", leerGeneral.Campo("Expediente"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "idPersonaSurte", leerGeneral.Campo("IdPersonalSurte"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "nombrePersonaSurte", leerGeneral.Campo("NombrePersonaSurte"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "apPatPersonaSurte", leerGeneral.Campo("ApPaternoPersonaSurte"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "apMatMedPersonaSurte", leerGeneral.Campo("ApMaternoPersonaSurte"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "observaciones", leerGeneral.Campo("Observaciones"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "nombreRecibe", leerGeneral.Campo("NombrePersonaRecibe"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "apPatRecibe", leerGeneral.Campo("ApPaternoPersonaRecibe"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "apMatRecibe", leerGeneral.Campo("ApMaternoPersonaRecibe"));
                sXML_Generado += string.Format("{0}</{1}>\n", getTabs(iTab_01), "General");


                leerMedicamentos.RegistroActual = 1;
                while (leerMedicamentos.Leer())
                {
                    medicamento = new Medicamento();

                    medicamento.ClaveLote = leerMedicamentos.Campo("ClaveLote");
                    medicamento.Caducidad = leerMedicamentos.Campo("Caducidad");
                    medicamento.ClaveSSA = leerMedicamentos.Campo("ClaveSSA");
                    medicamento.CantidadRecetada = leerMedicamentos.CampoInt("CantidadRecetada");
                    medicamento.CantidadSurtida = leerMedicamentos.CampoInt("CantidadSurtida");
                    //medicamento.ValeEmitido = vales;
                    //medicamento.SurtidoEnOtraUnidad = surteOtraUnidad;

                    listaMedicamentos.AgregarMedicamento(medicamento);
                }


                leerMedicamentos_Vale.RegistroActual = 1;
                while (leerMedicamentos_Vale.Leer())
                {
                    medicamento = new Medicamento();
                    vales = new Vale();
                    surteOtraUnidad = new SurteOtraUnidad();


                    vales.SurtidoConVale = "NO";
                    vales.FolioVale = "";
                    vales.Piezas = 0;

                    sFiltro_Medicamentos = string.Format(" {0} and ClaveSSA = '{1}' ", sFiltro, leerMedicamentos_Vale.Campo("ClaveSSA"));
                    leerVale.DataRowsClase = leerDatosGenerales.Tabla(sTB_Vale).Select(sFiltro_Medicamentos);
                    while (leerVale.Leer())
                    {
                        vales.SurtidoConVale = leerVale.Campo("SurtidoConVale");
                        vales.FolioVale = leerVale.Campo("FolioVale");
                        vales.Piezas = leerVale.CampoInt("CantidadVale");
                    }


                    surteOtraUnidad.SurteEnOtraUnidad = "NO";
                    surteOtraUnidad.CodigoUnidad = "";
                    leerSurteOtraUnidad.DataRowsClase = leerDatosGenerales.Tabla(sTB_SurteOtraUnidad).Select(sFiltro_Medicamentos);
                    while (leerSurteOtraUnidad.Leer())
                    {
                        surteOtraUnidad.SurteEnOtraUnidad = leerSurteOtraUnidad.Campo("SurteEnOtraUnidad"); // "SI";
                        surteOtraUnidad.CodigoUnidad = leerSurteOtraUnidad.Campo("ClaveUnidad");

                        leerSurteOtraUnidad_Medicamentos.DataRowsClase = leerDatosGenerales.Tabla(sTB_SurteOtraUnidad_Medicamentos).Select(sFiltro_Medicamentos);
                        while (leerSurteOtraUnidad_Medicamentos.Leer())
                        {
                            surteOtraUnidad.AddMedicamento(leerSurteOtraUnidad_Medicamentos.Campo("ClaveSSA"));
                        }
                    }


                    medicamento.ClaveLote = leerMedicamentos_Vale.Campo("");
                    medicamento.Caducidad = leerMedicamentos_Vale.Campo("");
                    medicamento.ClaveSSA = leerMedicamentos_Vale.Campo("ClaveSSA");
                    medicamento.CantidadRecetada = leerMedicamentos_Vale.CampoInt("CantidadRecetada");
                    medicamento.CantidadSurtida = leerMedicamentos_Vale.CampoInt("CantidadSurtida");
                    medicamento.ValeEmitido = vales;
                    medicamento.SurtidoEnOtraUnidad = surteOtraUnidad;

                    listaMedicamentos_Vales.AgregarMedicamento(medicamento);
                }


                ////sXML_Generado += itemMedicamento.valeMedicamento.GetString();
                ////sXML_Generado += itemMedicamento.SurtidoEnOtraUnidad.GetString();

                sXML_Generado += listaMedicamentos.GetString();
                sXML_Generado += listaMedicamentos_Vales.GetString(); 
                sXML_Generado += string.Format("{0}</{1}>\n", getTabs(iTab_00), "SurteReceta");

            }



            if (bXML_Generado)
            {
                try
                {
                    sXML_Generado = string.Format("{0}\n{1}", sEncabezado, sXML_Generado);
                    sRegresa = "";


                    bRegresa = !EnviarXml;
                    if (EnviarXml)
                    {
                        sRespuesta_Acuse = web.AcuseSurtidoDeRecetaElectronica(sXML_Generado);
                        bRegresa = RegistrarAcuseDeSurtido_Enviado(Folio_SIADISSEP, sRespuesta_Acuse); 
                    }
                }
                catch (Exception ex)
                {
                    bRegresa = false;
                    sRegresa = ex.Message;
                }
            }

            sRespuesta_Acuse = string.Format("{0}\n\n\n{1}", sRespuesta_Acuse, sXML_Generado);
            sRespuesta_Acuse_Respuestas += string.Format("{0}\n\n", sRespuesta_Acuse); 

            return bRegresa;
        }

        private bool RegistrarAcuseDeSurtido_Enviado(string Folio_SIADISSEP, string Informacion_XML)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0008_RecetasEnviar_Procesado " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio_SIADISSEP = '{3}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, Folio_SIADISSEP);

            clsLeer datos = new clsLeer();

            Informacion_XML = Informacion_XML.Replace("\n", "");
            dtsInformacion = new DataSet();
            dtsInformacion.ReadXml(new XmlTextReader(new StringReader(Informacion_XML)));

            leerXML.DataSetClase = dtsInformacion;
            sInformacion_XML = Informacion_XML.Replace(sEncabezado, "");


            datos.DataTableClase = leerXML.Tabla("Acuse");
            if (datos.Leer())
            {
                if (datos.Campo("estatus") != "0")
                {
                    bRegresa = false; 
                }
                else 
                {
                    bRegresa = true; 
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        Error.GrabarError(leer, "EnviarAcusesCancelacionReceta()");
                    }
                }
            }

            return bRegresa;
        }
        #endregion Acuses de surtido de recetas

        #region Acuses de cancelacion de recetas
        public bool EnviarAcusesCancelacionReceta()
        {
            return EnviarAcusesCancelacionReceta(true);
        }

        public bool EnviarAcusesCancelacionReceta(bool EnviarXml)
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0007_ObtenerCancelacionRecetasEnviarAcuse " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia);

            iErroresEnvioAcusesReceta = 0;
            if (!leerFolios.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerFolios, "EnviarAcusesCancelacionReceta()");
            }
            else
            {
                ///// Renombrar las tablas 
                leerFolios.RenombrarTabla(1, sTB_General);
                ////leer.RenombrarTabla(2, sTB_Medicamentos);
                ////leer.RenombrarTabla(3, sTB_Vale);
                ////leer.RenombrarTabla(4, sTB_SurteOtraUnidad);
                ////leer.RenombrarTabla(5, sTB_SurteOtraUnidad_Medicamentos);

                dtsInformacion = leerFolios.DataSetClase;
                leerDatosGenerales.DataSetClase = dtsInformacion;

                sRespuesta_Acuse_Respuestas = "";
                while (leerFolios.Leer())
                {
                    bRegresa = EnviarAcusesCancelacionReceta(leerFolios.Campo("FolioInterface"), EnviarXml);
                    if (bRegresa)
                    {
                        iErroresEnvioAcusesReceta++;
                    }
                }
            }

            return bRegresa;
        }

        public bool EnviarAcusesCancelacionReceta(string Folio_SIADISSEP, bool EnviarXml)
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0007_ObtenerCancelacionRecetasEnviarAcuse_Detalles " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioInterface = '{3}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, Folio_SIADISSEP);

            if (!leerFolios_Detalles.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerFolios_Detalles, "EnviarAcusesCancelacionReceta()");
            }
            else
            {
                ///// Renombrar las tablas 
                leerFolios_Detalles.RenombrarTabla(1, sTB_General);
                ////leer.RenombrarTabla(2, sTB_Medicamentos);
                ////leer.RenombrarTabla(3, sTB_Vale);
                ////leer.RenombrarTabla(4, sTB_SurteOtraUnidad);
                ////leer.RenombrarTabla(5, sTB_SurteOtraUnidad_Medicamentos);

                dtsInformacion = leerFolios_Detalles.DataSetClase;
                leerDatosGenerales.DataSetClase = dtsInformacion;

                while (leerFolios_Detalles.Leer())
                {
                    bRegresa = EnviarAcusesCancelacionReceta_Interno(leerFolios_Detalles.Campo("FolioInterface"), EnviarXml);
                    if (bRegresa)
                    {
                        iErroresEnvioAcusesReceta++;
                    }
                }
            }

            return bRegresa; 
        }

        private bool EnviarAcusesCancelacionReceta_Interno(string Folio_SIADISSEP, bool EnviarXml)
        {
            bool bRegresa = false;
            string sRegresa = "";
            string sStatus_Cancelacion = "";
            string sSql = " ";
            string sFiltro = string.Format(" FolioInterface = '{0}' ", Folio_SIADISSEP);
            string sFiltro_Medicamentos = ""; // string.Format(" FolioVenta = '{0}' ", FolioVenta);
            bool bXML_Generado = false;
            clsLeer leerGeneral = new clsLeer();

            leerGeneral.DataRowsClase = leerDatosGenerales.Tabla(sTB_General).Select(sFiltro);

            sXML_Generado = "";
            if (leerGeneral.Leer())
            {
                bXML_Generado = true;
                tpProceso = TipoProcesoReceta.CancelaReceta;
                sStatus_Cancelacion = leerGeneral.Campo("StatusCancelacion"); 


                sXML_Generado += string.Format("{0}<{1}>\n", getTabs(iTab_00), "CancelaRecetaAcuse");

                sXML_Generado += string.Format("{0}<{1}>\n", getTabs(iTab_01), "General");
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "idPeticion", leerGeneral.Campo("Folio_SIADISSEP"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "tipoPeticion", tpProceso.ToString().ToUpper());
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "folioReceta", leerGeneral.Campo("FolioReceta"));
                ////sXML_Generado += string.Format("{0}<{1}>{2} {3}</{1}>\n", getTabs(iTab_02), "fechaReceta", General.FechaYMD(leerGeneral.CampoFecha("FechaReceta"), "/"), General.Hora(leerGeneral.CampoFecha("FechaReceta")));
                sXML_Generado += string.Format("{0}<{1}>{2} {3}</{1}>\n", getTabs(iTab_02), "fechaCancelacionReceta", General.FechaYMD(leerGeneral.CampoFecha("FechaCancelacionReceta"), "/"), General.Hora(leerGeneral.CampoFecha("FechaCancelacionReceta")));
                sXML_Generado += string.Format("{0}<{1}>{2} {3}</{1}>\n", getTabs(iTab_02), "fechaEnvio", General.FechaYMD(leerGeneral.CampoFecha("FechaEnvioReceta"), "/"), General.Hora(leerGeneral.CampoFecha("fechaEnvio")));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "expediente", leerGeneral.Campo("Expediente"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "estatus", sStatus_Cancelacion);
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "mensaje", leerGeneral.Campo("StatusCancelacionDescripcion"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "unidadMedica", leerGeneral.Campo("UMedica"));
                sXML_Generado += string.Format("{0}</{1}>\n", getTabs(iTab_01), "General");

                sXML_Generado += string.Format("{0}</{1}>\n", getTabs(iTab_00), "CancelaRecetaAcuse");

            }

            if (bXML_Generado)
            {
                try
                {
                    sXML_Generado = string.Format("{0}\n{1}", sEncabezado, sXML_Generado);
                    sRegresa = "";

                    bRegresa = !EnviarXml;
                    if (EnviarXml)
                    {
                        sRespuesta_Acuse = web.AcuseDeCancelacionDeRecetaElectronica(sXML_Generado);
                        bRegresa = RegistrarAcuseDeCancelacion_Enviado(Folio_SIADISSEP, sRespuesta_Acuse, sStatus_Cancelacion == "1"); 
                    }

                    //bRegresa = true;
                }
                catch (Exception ex)
                {
                    bRegresa = false;
                    sRegresa = ex.Message;
                }
            }

            if (bRegresa)
            {
                sRespuesta_Acuse = string.Format("{0}\n\n\n{1}", sRespuesta_Acuse, sXML_Generado);
                sRespuesta_Acuse_Respuestas += string.Format("{0}\n\n", sRespuesta_Acuse);
            }
            else
            {
                sRespuesta_Acuse_Respuestas += string.Format("{0}\n\n", sRegresa);
            }

            return bRegresa;
        }

        private bool RegistrarAcuseDeCancelacion_Enviado(string Folio_SIADISSEP, string Informacion_XML, bool ValidarRespuesta)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0009_CancelacionRecetas_Procesado " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio_SIADISSEP = '{3}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, Folio_SIADISSEP);

            clsLeer datos = new clsLeer();

            dtsInformacion = new DataSet();
            Informacion_XML = Informacion_XML.Replace("\n", "");
            dtsInformacion.ReadXml(new XmlTextReader(new StringReader(Informacion_XML)));

            leerXML.DataSetClase = dtsInformacion;
            sInformacion_XML = Informacion_XML.Replace(sEncabezado, "");

            if (!ValidarRespuesta)
            {
                bRegresa = true;
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leer, "EnviarAcusesCancelacionReceta()");
                }
            }
            else
            {
                datos.DataTableClase = leerXML.Tabla("Acuse");
                if (datos.Leer())
                {
                    if (datos.Campo("estatus") != "0")
                    {
                        bRegresa = false;
                    }
                    else
                    {
                        bRegresa = true;
                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            Error.GrabarError(leer, "EnviarAcusesCancelacionReceta()");
                        }
                    }
                }
            }

            return bRegresa; 
        }
        #endregion Acuses de cancelacion de recetas

        #region Acuses de surtido de colectivos
        public bool GeneralEnviarAcusesColectivo()
        {
            bool bRegresa = true;
            string sSql =
                " Select IdEmpresa, IdEstado, IdFarmacia, count(*) as Registros " +
                " From INT_SIADISSEP__RecetasElectronicas_0001_General (NoLock)  " +
                " Where Procesado = 0 " +
                " Group by IdEmpresa, IdEstado, IdFarmacia ";

            if (!leer_Auxiliar.Exec(sSql))
            {
                GnDll_SII_SIADISSEP.MensajePantalla("ERROR: " + leer_Auxiliar.MensajeError);
            }
            else
            {
                while (leer_Auxiliar.Leer())
                {
                    sIdEmpresa = Fg.PonCeros(leer_Auxiliar.Campo("IdEmpresa"), 3);
                    sIdEstado = Fg.PonCeros(leer_Auxiliar.Campo("IdEstado"), 2);
                    sIdFarmacia = Fg.PonCeros(leer_Auxiliar.Campo("IdFarmacia"), 4);

                    EnviarAcusesColectivo(true);
                }
            }

            sIdEmpresa_Aux = sIdEmpresa;
            sIdEstado_Aux = sIdEstado;
            sIdFarmacia_Aux = sIdFarmacia;

            return bRegresa;
        }

        public bool EnviarAcusesColectivo()
        {
            return EnviarAcusesColectivo(true);
        }

        public bool EnviarAcusesColectivo(bool EnviarXml)
        {
            bool bRegresa = true;
            string sFolioSurtido = "";
            string sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0006_RecetasEnviarAcuse " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TipoDeProceso = '{3}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, (int)TipoProcesoReceta.ColectivoMedicamentos);

            iErroresEnvioAcusesReceta = 0;
            if (!leerFolios.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerFolios, "EnviarAcusesReceta()");
            }
            else
            {
                sRespuesta_Acuse_Respuestas = "";
                while (leerFolios.Leer())
                {
                    sFolioSurtido = leerFolios.Campo("FolioVenta");
                    sIdUMedica = leerFolios.Campo("UMedica");
                    bRegresa = EnviarAcusesColectivo(sFolioSurtido, EnviarXml);
                    if (bRegresa)
                    {
                        iErroresEnvioAcusesReceta++;
                    }
                }
            }

            return bRegresa;
        }

        public bool EnviarAcusesColectivo(string FolioVenta)
        {
            return EnviarAcusesColectivo(FolioVenta, true);
        }

        public bool EnviarAcusesColectivo(string FolioVenta, bool EnviarXml)
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_INT_SIADISSEP__RecetasElectronicas_0006_ColectivosEnviarAcuse_Detalles " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, FolioVenta);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "EnviarAcusesReceta()");
            }
            else
            {
                ///// Renombrar las tablas 
                leer.RenombrarTabla(1, sTB_General);
                leer.RenombrarTabla(2, sTB_Medicamentos);
                leer.RenombrarTabla(3, sTB_Soluciones);
                //leer.RenombrarTabla(4, sTB_Vale);
                //leer.RenombrarTabla(5, sTB_SurteOtraUnidad);
                //leer.RenombrarTabla(6, sTB_SurteOtraUnidad_Medicamentos);

                dtsInformacion = leer.DataSetClase;
                leerDatosGenerales.DataSetClase = dtsInformacion;

                while (leer.Leer())
                {
                    bRegresa = EnviarAcusesColectivo_Interno(leer.Campo("FolioVenta"), EnviarXml);
                    if (bRegresa)
                    {
                        iErroresEnvioAcusesReceta++;
                    }
                }
            }

            return bRegresa;
        }

        private bool EnviarAcusesColectivo_Interno(string FolioVenta, bool EnviarXml)
        {
            bool bRegresa = false;
            string sRegresa = "";
            string sSql = " ";
            string sFiltro = string.Format(" FolioVenta = '{0}' ", FolioVenta);
            string sFiltro_Medicamentos = ""; // string.Format(" FolioVenta = '{0}' ", FolioVenta);
            bool bXML_Generado = false;
            string Folio_SIADISSEP = "";
            string sFechaHoraSurtido = "";
            string sFechaRespuesta = "";


            clsLeer leerGeneral = new clsLeer();
            clsLeer leerMedicamentos = new clsLeer();
            clsLeer leerSoluciones = new clsLeer();
            clsLeer leerMedicamentos_Vale = new clsLeer();
            clsLeer leerVale = new clsLeer();
            clsLeer leerSurteOtraUnidad = new clsLeer();
            clsLeer leerSurteOtraUnidad_Medicamentos = new clsLeer();

            Medicamento listaMedicamentos = new Medicamento();
            Medicamento listaMedicamentos_Vales = new Medicamento();
            Medicamento listaSoluciones = new Medicamento();
            Medicamento medicamento = new Medicamento();            
            Vale vales = new Vale();
            SurteOtraUnidad surteOtraUnidad = new SurteOtraUnidad();

            leerGeneral.DataRowsClase = leerDatosGenerales.Tabla(sTB_General).Select(sFiltro);
            leerMedicamentos.DataRowsClase = leerDatosGenerales.Tabla(sTB_Medicamentos).Select(sFiltro);
            leerSoluciones.DataRowsClase = leerDatosGenerales.Tabla(sTB_Soluciones).Select(sFiltro);
            //leerMedicamentos_Vale.DataRowsClase = leerDatosGenerales.Tabla(sTB_Vale).Select(sFiltro);
            //leerVale.DataRowsClase = leerDatosGenerales.Tabla(sTB_Vale).Select(sFiltro);
            //leerSurteOtraUnidad.DataRowsClase = leerDatosGenerales.Tabla(sTB_SurteOtraUnidad).Select(sFiltro);
            //leerSurteOtraUnidad_Medicamentos.DataRowsClase = leerDatosGenerales.Tabla(sTB_SurteOtraUnidad_Medicamentos).Select(sFiltro);

            sXML_Generado = "";
            listaMedicamentos.Tipo = Medicamento.TipoDeMedicamento.Colectivo_Medicamento;
            listaSoluciones.Tipo = Medicamento.TipoDeMedicamento.Colectivo_Solucion;
            listaMedicamentos_Vales.Tipo = Medicamento.TipoDeMedicamento.MedicamentoVale;

            if (leerGeneral.Leer())
            {
                bXML_Generado = true;
                tpProceso = TipoProcesoReceta.SurtimientoColectivoMedicamentos;
                Folio_SIADISSEP = leerGeneral.Campo("Folio_SIADISSEP");

                sFechaHoraSurtido = General.FechaHora(leerGeneral.CampoFecha("FechaDeSurtimiento"));
                sFechaRespuesta = General.FechaHora(leerGeneral.CampoFecha("FechaDeSurtimiento"));
                //sFechaHoraSurtido += General.Hora(leerGeneral.CampoFecha("FechaDeSurtimiento"), ":");


                sXML_Generado += string.Format("{0}<{1}>\n", getTabs(iTab_00), "SurteColectivoMedSol");

                sXML_Generado += string.Format("{0}<{1}>\n", getTabs(iTab_01), "Generales");
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "idPaciente", leerGeneral.Campo("idPaciente"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "Expediente", leerGeneral.Campo("Expediente"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "idClues", sIdUMedica);
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "idEpisodio", leerGeneral.Campo("idEpisodio"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "idTipoServicio", leerGeneral.Campo("idTipoServicio"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "idServicio", leerGeneral.Campo("idServicio"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "idColectivo", leerGeneral.Campo("FolioColectivo"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "idPeticion", leerGeneral.Campo("FolioReceta"));

                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "tipoPeticion", tpProceso.ToString().ToUpper());
                //sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "fechaReceta", General.FechaYMD(leerGeneral.CampoFecha("FechaReceta"), "/"));
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "fechaSurtido", sFechaHoraSurtido);
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "fechaRespuesta", sFechaRespuesta);
                //sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "idPersonaSurte", leerGeneral.Campo("IdPersonalSurte"));
                //sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "nombrePersonaSurte", leerGeneral.Campo("NombrePersonaSurte"));
                //sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "apPatPersonaSurte", leerGeneral.Campo("ApPaternoPersonaSurte"));
                //sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "apMatMedPersonaSurte", leerGeneral.Campo("ApMaternoPersonaSurte"));
                //sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "observaciones", leerGeneral.Campo("Observaciones"));
                //sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "nombreRecibe", leerGeneral.Campo("NombrePersonaRecibe"));
                //sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "apPatRecibe", leerGeneral.Campo("ApPaternoPersonaRecibe"));
                //sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "apMatRecibe", leerGeneral.Campo("ApMaternoPersonaRecibe"));
                sXML_Generado += string.Format("{0}</{1}>\n", getTabs(iTab_01), "Generales");

                //     <idPaciente>1</idPaciente>
                //    <Expediente>210930003425</Expediente>
                //<idClues>PLSSA009394</idClues>
                //<idEpisodio>EMER</idEpisodio>
                //<idTipoServicio>EMER001</idTipoServicio>
                //<idServicio>EMER00101</idServicio>
                //<idColectivo>340</idColectivo>
                //<idPeticion>CMS179</idPeticion>
                //<tipoPeticion>SURTIMIENTOCOLECTIVOMEDICAMENTOS</tipoPeticion>
                //<fechaHoraElaboracion>2018-05-31 11:38:40.82</fechaHoraElaboracion>
                //<fecha_surtimiento>2018-05-31 13:18:12</fecha_surtimiento>

                
                leerMedicamentos.RegistroActual = 1;
                while (leerMedicamentos.Leer())
                {
                    medicamento = new Medicamento();


                    medicamento.Expediente = leerGeneral.Campo("Expediente");
                    medicamento.NombreBeneficiario = leerGeneral.Campo("NombreBeneficiario");
                    medicamento.ApPaternoBeneficiario = leerGeneral.Campo("ApPaternoBeneficiario");
                    medicamento.ApMaternoBeneficiario = leerGeneral.Campo("ApMaternoBeneficiario");
                    medicamento.CamaPaciente = leerGeneral.Campo("CamaPaciente");
                    medicamento.FechaNacimientoPaciente = General.FechaYMD(leerGeneral.CampoFecha("FechaNacimientoBeneficiario"));
                    medicamento.PolizaPaciente = leerGeneral.Campo("FolioAfiliacionSPSS");
                    medicamento.VigenciaPolizaPaciente = General.FechaYMD(leerGeneral.CampoFecha("FechaTerminaVigencia"));

                    medicamento.ClaveLote = leerMedicamentos.Campo("ClaveLote");
                    medicamento.Caducidad = leerMedicamentos.Campo("Caducidad");
                    medicamento.ClaveSSA = leerMedicamentos.Campo("ClaveSSA");
                    medicamento.DecripcionSal = leerMedicamentos.Campo("DescripcionSal");
                    medicamento.CantidadRecetada = leerMedicamentos.CampoInt("CantidadRecetada");
                    medicamento.CantidadSurtida = leerMedicamentos.CampoInt("CantidadSurtida");
                    //medicamento.ValeEmitido = vales;
                    //medicamento.SurtidoEnOtraUnidad = surteOtraUnidad;

                    listaMedicamentos.AgregarMedicamento(medicamento);
                }


                leerSoluciones.RegistroActual = 1;
                while (leerSoluciones.Leer())
                {
                    medicamento = new Medicamento();

                    medicamento.Expediente = leerGeneral.Campo("Expediente");
                    medicamento.NombreBeneficiario = leerGeneral.Campo("NombreBeneficiario");
                    medicamento.ApPaternoBeneficiario = leerGeneral.Campo("ApPaternoBeneficiario");
                    medicamento.ApMaternoBeneficiario = leerGeneral.Campo("ApMaternoBeneficiario");
                    medicamento.CamaPaciente = leerGeneral.Campo("CamaPaciente");
                    medicamento.FechaNacimientoPaciente = General.FechaYMD(leerGeneral.CampoFecha("FechaNacimientoBeneficiario"));
                    medicamento.PolizaPaciente = leerGeneral.Campo("FolioAfiliacionSPSS");
                    medicamento.VigenciaPolizaPaciente = General.FechaYMD(leerGeneral.CampoFecha("FechaTerminaVigencia"));

                    medicamento.ClaveLote = leerSoluciones.Campo("ClaveLote");
                    medicamento.Caducidad = leerSoluciones.Campo("Caducidad");
                    medicamento.ClaveSSA = leerSoluciones.Campo("ClaveSSA");
                    medicamento.DecripcionSal = leerMedicamentos.Campo("DescripcionSal");
                    medicamento.CantidadRecetada = leerSoluciones.CampoInt("CantidadRecetada");
                    medicamento.CantidadSurtida = leerSoluciones.CampoInt("CantidadSurtida");
                    //medicamento.ValeEmitido = vales;
                    //medicamento.SurtidoEnOtraUnidad = surteOtraUnidad;

                    listaSoluciones.AgregarMedicamento(medicamento);
                }
                
                //leerMedicamentos_Vale.RegistroActual = 1;
                //while (leerMedicamentos_Vale.Leer())
                //{
                //    medicamento = new Medicamento();
                //    vales = new Vale();
                //    surteOtraUnidad = new SurteOtraUnidad();


                //    vales.SurtidoConVale = "NO";
                //    vales.FolioVale = "";
                //    vales.Piezas = 0;

                //    sFiltro_Medicamentos = string.Format(" {0} and ClaveSSA = '{1}' ", sFiltro, leerMedicamentos_Vale.Campo("ClaveSSA"));
                //    leerVale.DataRowsClase = leerDatosGenerales.Tabla(sTB_Vale).Select(sFiltro_Medicamentos);
                //    while (leerVale.Leer())
                //    {
                //        vales.SurtidoConVale = leerVale.Campo("SurtidoConVale");
                //        vales.FolioVale = leerVale.Campo("FolioVale");
                //        vales.Piezas = leerVale.CampoInt("CantidadVale");
                //    }


                //    surteOtraUnidad.SurteEnOtraUnidad = "NO";
                //    surteOtraUnidad.CodigoUnidad = "";
                //    leerSurteOtraUnidad.DataRowsClase = leerDatosGenerales.Tabla(sTB_SurteOtraUnidad).Select(sFiltro_Medicamentos);
                //    while (leerSurteOtraUnidad.Leer())
                //    {
                //        surteOtraUnidad.SurteEnOtraUnidad = leerSurteOtraUnidad.Campo("SurteEnOtraUnidad"); // "SI";
                //        surteOtraUnidad.CodigoUnidad = leerSurteOtraUnidad.Campo("ClaveUnidad");

                //        leerSurteOtraUnidad_Medicamentos.DataRowsClase = leerDatosGenerales.Tabla(sTB_SurteOtraUnidad_Medicamentos).Select(sFiltro_Medicamentos);
                //        while (leerSurteOtraUnidad_Medicamentos.Leer())
                //        {
                //            surteOtraUnidad.AddMedicamento(leerSurteOtraUnidad_Medicamentos.Campo("ClaveSSA"));
                //        }
                //    }


                //    medicamento.ClaveLote = leerMedicamentos_Vale.Campo("");
                //    medicamento.Caducidad = leerMedicamentos_Vale.Campo("");
                //    medicamento.ClaveSSA = leerMedicamentos_Vale.Campo("ClaveSSA");
                //    medicamento.CantidadRecetada = leerMedicamentos_Vale.CampoInt("CantidadRecetada");
                //    medicamento.CantidadSurtida = leerMedicamentos_Vale.CampoInt("CantidadSurtida");
                //    medicamento.ValeEmitido = vales;
                //    medicamento.SurtidoEnOtraUnidad = surteOtraUnidad;

                //    listaMedicamentos_Vales.AgregarMedicamento(medicamento);
                //}


                ////sXML_Generado += itemMedicamento.valeMedicamento.GetString();
                ////sXML_Generado += itemMedicamento.SurtidoEnOtraUnidad.GetString();
                sXML_Generado += string.Format("{0}<{1}>\n", getTabs(iTab_01), "Detalles");
                sXML_Generado += listaMedicamentos.GetString();
                sXML_Generado += listaSoluciones.GetString();
                sXML_Generado += string.Format("{0}</{1}>\n", getTabs(iTab_01), "Detalles");
                //sXML_Generado += listaMedicamentos_Vales.GetString();
                sXML_Generado += string.Format("{0}</{1}>\n", getTabs(iTab_00), "SurteColectivoMedSol");

            }



            if (bXML_Generado)
            {
                try
                {
                    sXML_Generado = string.Format("{0}\n{1}", sEncabezado, sXML_Generado);
                    sRegresa = "";


                    bRegresa = !EnviarXml;
                    if (EnviarXml)
                    {
                        sRespuesta_Acuse = web.AcuseSurtidoDeRecetaElectronica(sXML_Generado);
                        bRegresa = RegistrarAcuseDeSurtido_Enviado(Folio_SIADISSEP, sRespuesta_Acuse);
                    }
                }
                catch (Exception ex)
                {
                    bRegresa = false;
                    sRegresa = ex.Message;
                }
            }

            sRespuesta_Acuse = string.Format("{0}\n\n\n{1}", sRespuesta_Acuse, sXML_Generado);
            sRespuesta_Acuse_Respuestas += string.Format("{0}\n\n", sRespuesta_Acuse);

            return bRegresa;
        }
        #endregion Acuses de surtido de colectivos
        #endregion Funciones y Procedimientos Publicos


        #region Funciones y Procedimientos Privados
        private string getTabs(int Tabs)
        {
            return GnDll_SII_SIADISSEP.getTabs(Tabs);
        }
        #endregion Funciones y Procedimientos Privados
    }
}
