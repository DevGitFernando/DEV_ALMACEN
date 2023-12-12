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

using Dll_ISESEQ;
using Dll_ISESEQ.wsClases;
using Dll_ISESEQ.wsAcuseProcesos_cnnInterna; 

namespace Dll_ISESEQ.wsClases
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
        string sFolio_SESEQ = "";
        string sFolioRegistro = "";
        string sMensajesError = "";
        TipoProcesoReceta tpProceso = TipoProcesoReceta.Ninguno;
        string sTipoDeProceso = "";

        DataSet dtsInformacion = new DataSet();

        basGenerales Fg = new basGenerales();
        string sEncabezado = "";


        int iErroresEnvioAcusesReceta = 0;
        int iErroresEnvioAcusesCancelacionReceta = 0;

        wsAcuseProcesos_cnnInterna.wsISESEQ web = new wsAcuseProcesos_cnnInterna.wsISESEQ();

        clsLeer leerDatosGenerales = new clsLeer(); 
        string sTB_General = "General";
        string sTB_Medicamentos = "Medicamentos";
        string sTB_Imagenes = "Imagenes";

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

            Error = new clsGrabarError(datosDeConexion, GnDll_SII_SESEQ.DatosApp, "ResponseAcuseXML");
            Error.NombreLogErorres = "INT_SESEQ__CtlErrores";

            sEncabezado = string.Format("<?xml version={0}1.0{0} encoding={0}UTF-8{0}?>", Fg.Comillas());

            sIdEmpresa = Fg.PonCeros(Empresa, 3);
            sIdEstado = Fg.PonCeros(Estado, 2);
            sIdFarmacia = Fg.PonCeros(Farmacia, 4);

            sIdEmpresa_Aux = sIdEmpresa;
            sIdEstado_Aux = sIdEstado;
            sIdFarmacia_Aux = sIdFarmacia;


            web = new wsAcuseProcesos_cnnInterna.wsISESEQ();

            GnDll_SII_SESEQ.ObtenerURL_Interface(IdEmpresa, IdEstado, IdFarmacia);
            web.Url = GnDll_SII_SESEQ.URL_Interface;
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
                "Select \n"+
                "\tIdEmpresa, IdEstado, IdFarmacia, count(*) as Registros \n" +
                "From INT_SESEQ__RecetasElectronicas_0001_General (NoLock) \n" +
                "Where Procesado = 0 and TipoDeProceso = 1 \n" +
                "Group by IdEmpresa, IdEstado, IdFarmacia \n";

            if (!leer_Auxiliar.Exec(sSql))
            {
                GnDll_SII_SESEQ.MensajePantalla("ERROR: " + leer_Auxiliar.MensajeError); 
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
            string sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0006_RecetasEnviarAcuse \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TipoDeProceso = '{3}' \n", 
                sIdEmpresa, sIdEstado, sIdFarmacia, (int)TipoProcesoReceta.Receta);

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
            //string sFolio = "";
            string sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0006_RecetasEnviarAcuse_Detalles \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}' ",
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
                ////leer.RenombrarTabla(3, sTB_Vale);
                ////leer.RenombrarTabla(4, sTB_SurteOtraUnidad);
                ////leer.RenombrarTabla(5, sTB_SurteOtraUnidad_Medicamentos);

                dtsInformacion = leer.DataSetClase;
                leerDatosGenerales.DataSetClase = dtsInformacion;

                while (leer.Leer())
                {
                    FolioVenta = leer.Campo("FolioVenta");
                    bRegresa = EnviarAcuses_Dispensacion(FolioVenta, TipoProcesoReceta.Receta, EnviarXml);
                    if (bRegresa)
                    {
                        iErroresEnvioAcusesReceta++;
                    }
                }
            }

            return bRegresa; 
        }

        private bool EnviarAcuses_Dispensacion(string FolioVenta, TipoProcesoReceta Proceso, bool EnviarXml)
        {
            bool bRegresa = EnviarAcuses_Interno_Proceso(1, FolioVenta, Proceso, EnviarXml);
            return bRegresa;
        }

        private bool EnviarAcuses_Devoluciones(string FolioDevolucion, TipoProcesoReceta Proceso, bool EnviarXml)
        {
            bool bRegresa = EnviarAcuses_Interno_Proceso(2, FolioDevolucion, Proceso, EnviarXml);
            return bRegresa; 
        }


        private bool EnviarAcuses_Interno_Proceso(int Tipo, string Folio, TipoProcesoReceta Proceso, bool EnviarXml)
        {
            bool bRegresa = false;
            string sRegresa = ""; 
            string sSql = " ";
            string sFiltro = string.Format(" FolioVenta = '{0}' ", Folio);
            string sFiltro_Medicamentos = ""; // string.Format(" FolioVenta = '{0}' ", FolioVenta);
            bool bXML_Generado = false;
            string Folio_SESEQ = "";
            string FolioInterface = "";
            string sFechaHoraSurtido = "";
            int iPartida = 0;

            if (Tipo == 1)
            {
                sFiltro = string.Format(" FolioVenta = '{0}' ", Folio);
            }

            if (Tipo == 2)
            {
                sFiltro = string.Format(" FolioDevolucion = '{0}' ", Folio);
            }


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
            ////leerMedicamentos_Vale.DataRowsClase = leerDatosGenerales.Tabla(sTB_Vale).Select(sFiltro);
            ////leerVale.DataRowsClase = leerDatosGenerales.Tabla(sTB_Vale).Select(sFiltro);
            ////leerSurteOtraUnidad.DataRowsClase = leerDatosGenerales.Tabla(sTB_SurteOtraUnidad).Select(sFiltro);
            ////leerSurteOtraUnidad_Medicamentos.DataRowsClase = leerDatosGenerales.Tabla(sTB_SurteOtraUnidad_Medicamentos).Select(sFiltro);

            sXML_Generado = ""; 
            listaMedicamentos.Tipo = Medicamento.TipoDeMedicamento.MedicamentoSurtido; 
            ////listaMedicamentos_Vales.Tipo = Medicamento.TipoDeMedicamento.MedicamentoVale; 

            if (leerGeneral.Leer()) 
            { 
                bXML_Generado = true; 
                tpProceso = TipoProcesoReceta.AcuseSurteReceta; 
                FolioInterface = leerGeneral.Campo("FolioInterface"); 
                Folio_SESEQ = leerGeneral.Campo("Folio_SESEQ"); 
                iPartida = leerGeneral.CampoInt("Partida"); 

                leerMedicamentos.RegistroActual = 1; 
                while (leerMedicamentos.Leer()) 
                {
                    medicamento = new Medicamento(); 
                    medicamento.TipoDeProceso = Proceso; 
                    medicamento.NumeroDeReceta = Folio_SESEQ; 
                    medicamento.FechaDeSurtido = leerMedicamentos.Campo("fechaSurtido"); 

                    medicamento.ClaveSSA = leerMedicamentos.Campo("ClaveSSA"); 
                    medicamento.CantidadRecetada = leerMedicamentos.CampoInt("CantidadRecetada"); 
                    medicamento.CantidadSurtida = leerMedicamentos.CampoInt("CantidadSurtida"); 
                    medicamento.ClaveLote = leerMedicamentos.Campo("ClaveLote"); 
                    medicamento.Caducidad = leerMedicamentos.Campo("Caducidad"); 
                    listaMedicamentos.AgregarMedicamento(medicamento); 
                } 


                sXML_Generado = ""; 
                sXML_Generado += string.Format("{0}<{1}>\n", getTabs(iTab_00), "SurteReceta"); 
                sXML_Generado += listaMedicamentos.GetString(); 
                ////sXML_Generado += listaMedicamentos_Vales.GetString(); 
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
                        bRegresa = RegistrarAcuseDeSurtido_Enviado(Tipo, FolioInterface, Folio, iPartida, Proceso, sRespuesta_Acuse); 
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

        private bool RegistrarAcuseDeSurtido_Enviado(int Tipo, string Folio_SESEQ, string Folio, int Partida, TipoProcesoReceta Proceso, string Informacion_XML)
        {
            bool bRegresa = false;
            string sSP_01 = Tipo == 1 ? " spp_INT_SESEQ__RecetasElectronicas_0008_RecetasEnviar_ActualizarNumeroDeEnvios " : " spp_INT_SESEQ__RecetasElectronicas_0025_DevolucionesEnviar_ActualizarNumeroDeEnvios ";
            string sSP_02 = Tipo == 1 ? " spp_INT_SESEQ__RecetasElectronicas_0008_RecetasEnviar_Procesado " : " spp_INT_SESEQ__RecetasElectronicas_0025_DevolucionesEnviar_Procesado "; 
            string sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0008_RecetasEnviar_Procesado " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio_SESEQ = '{3}', @TipoDeProceso = '{4}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, Folio_SESEQ, 1);

            Folio_SESEQ = Tipo == 1 ? Folio_SESEQ : Folio; 


            try
            {

                sSql = string.Format("Exec {0} \n" + 
                    "\t@IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', @Folio_SESEQ = '{4}', @Partida = '{5}', @TipoDeProceso = '{6}' \n", 
                    sSP_01, sIdEmpresa, sIdEstado, sIdFarmacia, Folio_SESEQ, Partida, 1); 

                // Actualizar el numero de intentos de envio de la respuesta 
                leer.Exec(sSql); 


                string[] sRespuesta = Informacion_XML.Split('|'); 

                if (sRespuesta != null)
                {
                    if (sRespuesta[0] == "0")
                    {
                        bRegresa = true;

                        sSql = string.Format("Exec {0} \n" +
                            "\t@IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', @Folio_SESEQ = '{4}', @Partida = '{5}', @TipoDeProceso = '{6}' \n",
                            sSP_02, sIdEmpresa, sIdEstado, sIdFarmacia, Folio_SESEQ, Partida, 1);

                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            Error.GrabarError(leer, "RegistrarAcuseDeSurtido_Enviado()");
                        }
                    }
                    else
                    {
                        Error.GrabarError(leer, string.Format("{0}|{1}", Informacion_XML, sXML_Generado), "EnviarAcusesCancelacionRecetaXML");
                    }
                }
            }
            catch { }

            return bRegresa;
        }
        #endregion Acuses de surtido de recetas

        #region Acuses de devoluciones  
        public bool GeneralEnviarAcuses_Devoluciones(TipoProcesoReceta TipoDeProceso)
        {
            bool bRegresa = true;
            string sSql =
                string.Format(
                "Select \n" +
                "\tIdEmpresa, IdEstado, IdFarmacia, count(*) as Registros \n" +
                "From INT_SESEQ__RecetasElectronicas_0007_Devoluciones (NoLock) \n" +
                "Where Procesado = 0 and TipoDeProceso = {0} \n" +
                "Group by IdEmpresa, IdEstado, IdFarmacia \n", (int)TipoDeProceso);

            if (!leer_Auxiliar.Exec(sSql)) 
            {
                GnDll_SII_SESEQ.MensajePantalla("ERROR: " + leer_Auxiliar.MensajeError);
            }
            else
            {
                while (leer_Auxiliar.Leer())
                {
                    sIdEmpresa = Fg.PonCeros(leer_Auxiliar.Campo("IdEmpresa"), 3);
                    sIdEstado = Fg.PonCeros(leer_Auxiliar.Campo("IdEstado"), 2);
                    sIdFarmacia = Fg.PonCeros(leer_Auxiliar.Campo("IdFarmacia"), 4);

                    EnviarAcuses_Devoluciones(true, TipoDeProceso);
                }
            }

            sIdEmpresa_Aux = sIdEmpresa;
            sIdEstado_Aux = sIdEstado;
            sIdFarmacia_Aux = sIdFarmacia;

            return bRegresa;
        }

        public bool EnviarAcuses_Devoluciones(bool EnviarXml, TipoProcesoReceta TipoDeProceso)
        {
            bool bRegresa = true;
            string sFolioSurtido = "";
            string sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0024_DevolucionesEnviarAcuse \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TipoDeProceso = '{3}' \n",
                sIdEmpresa, sIdEstado, sIdFarmacia, (int)TipoDeProceso);

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
                    sFolioSurtido = leerFolios.Campo("FolioDevolucion");
                    bRegresa = EnviarAcuses_Devoluciones(sFolioSurtido, EnviarXml, TipoDeProceso);
                    if (bRegresa)
                    {
                        iErroresEnvioAcusesReceta++;
                    }
                }
            }

            return bRegresa;
        }

        public bool EnviarAcuses_Devoluciones(string FolioDevolucion, bool EnviarXml, TipoProcesoReceta TipoDeProceso)
        {
            bool bRegresa = true;
            //string sFolio = "";
            string sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0024_DevolucionesEnviarAcuse_Detalles \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioDevolucion = '{3}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, FolioDevolucion);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "EnviarAcuses_Devoluciones()");
            }
            else
            {
                leer.RenombrarTabla(1, sTB_General);
                leer.RenombrarTabla(2, sTB_Medicamentos);

                dtsInformacion = leer.DataSetClase;
                leerDatosGenerales.DataSetClase = dtsInformacion;

                while (leer.Leer())
                {
                    FolioDevolucion = leer.Campo("FolioDevolucion");
                    bRegresa = EnviarAcuses_Devoluciones(FolioDevolucion, TipoDeProceso, EnviarXml);
                    if (bRegresa)
                    {
                        iErroresEnvioAcusesReceta++;
                    }
                }
            }

            return bRegresa;
        }
        #endregion Acuses de devoluciones  

        #region Acuses de digitalizacion surtido de recetas 
        /// <summary>
        /// Enviar las imagenes registradas de Recetas - Colectivos - Ticket de dispensación
        /// </summary>
        /// <returns></returns>
        public bool GeneralEnviarAcusesDigitalizacionReceta()
        {
            bool bRegresa = true;
            string sSql =
                string.Format(
                "Select \n" + 
                "\tIdEmpresa, IdEstado, IdFarmacia, count(*) as Registros \n" +
                "From INT_SESEQ__RecetasElectronicas_0001_General (NoLock)  \n" +
                "Where Procesado = 1 and Procesado_Digitalizacion = 0 and TipoDeProceso = '{0}' \n" +
                "Group by IdEmpresa, IdEstado, IdFarmacia \n", (int)TipoProcesoReceta.Receta);

            if(!leer_Auxiliar.Exec(sSql))
            {
                GnDll_SII_SESEQ.MensajePantalla("ERROR: " + leer_Auxiliar.MensajeError);
            }
            else
            {
                while(leer_Auxiliar.Leer())
                {
                    sIdEmpresa = Fg.PonCeros(leer_Auxiliar.Campo("IdEmpresa"), 3);
                    sIdEstado = Fg.PonCeros(leer_Auxiliar.Campo("IdEstado"), 2);
                    sIdFarmacia = Fg.PonCeros(leer_Auxiliar.Campo("IdFarmacia"), 4);

                    EnviarAcusesDigitalizacionReceta(true);
                }
            }

            sIdEmpresa_Aux = sIdEmpresa;
            sIdEstado_Aux = sIdEstado;
            sIdFarmacia_Aux = sIdFarmacia;

            return bRegresa;
        }

        public bool EnviarAcusesDigitalizacionReceta()
        {
            return EnviarAcusesDigitalizacionReceta(true);
        }

        public bool EnviarAcusesDigitalizacionReceta( bool EnviarXml )
        {
            bool bRegresa = true;
            string sFolioSurtido = "";
            string sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0014_ObtenerDigitalizacionEnviarAcuse \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TipoDeProceso = '{3}' \n",
                sIdEmpresa, sIdEstado, sIdFarmacia, (int)TipoProcesoReceta.Receta);

            iErroresEnvioAcusesReceta = 0;
            if(!leerFolios.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerFolios, "EnviarAcusesDigitalizacionReceta()");
            }
            else
            {
                sRespuesta_Acuse_Respuestas = "";
                while(leerFolios.Leer())
                {
                    sFolioSurtido = leerFolios.Campo("FolioVenta");
                    bRegresa = EnviarAcusesDigitalizacionReceta(sFolioSurtido, EnviarXml);
                    if(bRegresa)
                    {
                        iErroresEnvioAcusesReceta++;
                    }
                }
            }

            return bRegresa;
        }

        public bool EnviarAcusesDigitalizacionReceta( string FolioVenta )
        {
            return EnviarAcusesDigitalizacionReceta(FolioVenta, true);
        }

        public bool EnviarAcusesDigitalizacionReceta( string FolioVenta, bool EnviarXml )
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0014_ObtenerDigitalizacionEnviarAcuse_Detalles \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, FolioVenta);

            if(!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "EnviarAcusesDigitalizacionReceta()");
            }
            else
            {
                ///// Renombrar las tablas 
                leer.RenombrarTabla(1, sTB_Imagenes);
                //leer.RenombrarTabla(2, sTB_Medicamentos);
                ////leer.RenombrarTabla(3, sTB_Vale);
                ////leer.RenombrarTabla(4, sTB_SurteOtraUnidad);
                ////leer.RenombrarTabla(5, sTB_SurteOtraUnidad_Medicamentos);

                dtsInformacion = leer.DataSetClase;
                leerDatosGenerales.DataSetClase = dtsInformacion;

                while(leer.Leer())
                {
                    FolioVenta = leer.Campo("FolioVenta");
                    bRegresa = EnviarAcusesDigitalizacionReceta_Interno(FolioVenta, EnviarXml);
                    if(bRegresa)
                    {
                        iErroresEnvioAcusesReceta++;
                    }
                }
            }

            return bRegresa;
        }

        private bool EnviarAcusesDigitalizacionReceta_Interno( string FolioVenta, bool EnviarXml )
        {
            bool bRegresa = false;
            string sRegresa = "";
            string sSql = " ";
            string sFiltro = string.Format(" FolioVenta = '{0}' ", FolioVenta);
            string sFiltro_Medicamentos = ""; // string.Format(" FolioVenta = '{0}' ", FolioVenta);
            bool bXML_Generado = false;
            string Folio_SESEQ = "";
            string FolioInterface = "";
            string sFechaHoraSurtido = "";
            int iPartida = 0;

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

            leerGeneral.DataRowsClase = leerDatosGenerales.Tabla(sTB_Imagenes).Select(sFiltro);
            leerMedicamentos.DataRowsClase = leerDatosGenerales.Tabla(sTB_Imagenes).Select(sFiltro);


            sXML_Generado = "";
            listaMedicamentos.Tipo = Medicamento.TipoDeMedicamento.MedicamentoSurtido;
            ////listaMedicamentos_Vales.Tipo = Medicamento.TipoDeMedicamento.MedicamentoVale; 

            if(leerGeneral.Leer())
            {
                bXML_Generado = true;
                tpProceso = TipoProcesoReceta.AcuseSurteReceta;
                tpProceso = (TipoProcesoReceta)leerGeneral.CampoInt("TipoDeProceso");

                FolioInterface = leerGeneral.Campo("FolioInterface");
                Folio_SESEQ = leerGeneral.Campo("Folio_SESEQ");
                iPartida = leerGeneral.CampoInt("Partida");

                leerMedicamentos.RegistroActual = 1;
                while(leerMedicamentos.Leer())
                {
                    medicamento = new Medicamento();
                    medicamento.TipoDeProceso = tpProceso;
                    medicamento.NumeroDeReceta = Folio_SESEQ;

                    medicamento.IdImagen = leerMedicamentos.CampoInt("IdImagen");
                    medicamento.TipoDeImagen = leerMedicamentos.CampoInt("TipoDeImagen");
                    medicamento.ImagenB64 = leerMedicamentos.Campo("Imagen");
                    listaMedicamentos.AgregarMedicamento(medicamento);
                }


                sXML_Generado = "";
                sXML_Generado += string.Format("{0}<{1}>\n", getTabs(iTab_00), "SurteReceta");
                sXML_Generado += listaMedicamentos.GetString__Digitalizacion();
                ////sXML_Generado += listaMedicamentos_Vales.GetString(); 
                sXML_Generado += string.Format("{0}</{1}>\n", getTabs(iTab_00), "SurteReceta");

            }



            if(bXML_Generado)
            {
                try
                {
                    sXML_Generado = string.Format("{0}\n{1}", sEncabezado, sXML_Generado);
                    sRegresa = "";


                    bRegresa = !EnviarXml;
                    if(EnviarXml)
                    {
                        sRespuesta_Acuse = web.AcuseDigitalizacion(sXML_Generado);
                        bRegresa = RegistrarAcuseDeDigitalizacion_Enviado(FolioInterface, iPartida, sRespuesta_Acuse);
                    }
                }
                catch(Exception ex)
                {
                    bRegresa = false;
                    sRegresa = ex.Message;
                }
            }

            sRespuesta_Acuse = string.Format("{0}\n\n\n{1}", sRespuesta_Acuse, sXML_Generado);
            sRespuesta_Acuse_Respuestas += string.Format("{0}\n\n", sRespuesta_Acuse);

            return bRegresa;
        }

        private bool RegistrarAcuseDeDigitalizacion_Enviado( string Folio_SESEQ, int Partida, string Informacion_XML )
        {
            bool bRegresa = false;
            string sSql = "";

            try
            {

                sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0008_RecetasEnviar_ActualizarNumeroDeEnvios \n" +
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio_SESEQ = '{3}', @Partida = '{4}', @TipoDeProceso = {5} \n",
                    sIdEmpresa, sIdEstado, sIdFarmacia, Folio_SESEQ, Partida, 2);

                // Actualizar el numero de intentos de envio de la respuesta 
                leer.Exec(sSql);


                string[] sRespuesta = Informacion_XML.Split('|');

                if(sRespuesta != null)
                {
                    if(sRespuesta[0] == "0")
                    {
                        bRegresa = true;

                        sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0008_RecetasEnviar_Procesado \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio_SESEQ = '{3}', @Partida = '{4}', @TipoDeProceso = '{5}' \n",
                            sIdEmpresa, sIdEstado, sIdFarmacia, Folio_SESEQ, Partida, 2);

                        if(!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            Error.GrabarError(leer, "RegistrarAcuseDeDigitalizacion_Enviado()");
                        }
                    }
                    else
                    {
                        Error.GrabarError(leer, string.Format("{0}|{1}", Informacion_XML, sXML_Generado), "RegistrarAcuseDeDigitalizacion_Enviado");
                    }
                }
            }
            catch { }

            return bRegresa;
        }
        #endregion Acuses de digitalizacion surtido de recetas

        #region Acuses de cancelacion de recetas
        public bool EnviarAcusesCancelacionReceta()
        {
            return EnviarAcusesCancelacionReceta(true);
        }

        public bool EnviarAcusesCancelacionReceta(bool EnviarXml)
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0007_ObtenerCancelacionRecetasEnviarAcuse " +
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

        public bool EnviarAcusesCancelacionReceta(string Folio_SESEQ, bool EnviarXml)
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0007_ObtenerCancelacionRecetasEnviarAcuse_Detalles " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioInterface = '{3}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, Folio_SESEQ);

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

        private bool EnviarAcusesCancelacionReceta_Interno(string Folio_SESEQ, bool EnviarXml)
        {
            bool bRegresa = false;
            string sRegresa = "";
            string sStatus_Cancelacion = "";
            string sSql = " ";
            string sFiltro = string.Format(" FolioInterface = '{0}' ", Folio_SESEQ);
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
                sXML_Generado += string.Format("{0}<{1}>{2}</{1}>\n", getTabs(iTab_02), "idPeticion", leerGeneral.Campo("Folio_SESEQ"));
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
                        bRegresa = RegistrarAcuseDeCancelacion_Enviado(Folio_SESEQ, sRespuesta_Acuse, sStatus_Cancelacion == "1"); 
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

        private bool RegistrarAcuseDeCancelacion_Enviado(string Folio_SESEQ, string Informacion_XML, bool ValidarRespuesta)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0009_CancelacionRecetas_Procesado " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio_SESEQ = '{3}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, Folio_SESEQ);

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
        /// <summary>
        /// Enviar las imagenes registradas de Recetas - Colectivos - Ticket de dispensación
        /// </summary>
        /// <returns></returns>
        public bool GeneralEnviarAcusesDigitalizacionColectivo()
        {
            bool bRegresa = true;
            string sSql =
                string.Format(
                "Select \n" +
                "\tIdEmpresa, IdEstado, IdFarmacia, count(*) as Registros \n" +
                "From INT_SESEQ__RecetasElectronicas_0001_General (NoLock)  \n" +
                "Where Procesado = 1 and Procesado_Digitalizacion = 0 and TipoDeProceso = '{0}' \n" +
                "Group by IdEmpresa, IdEstado, IdFarmacia \n", (int)TipoProcesoReceta.Colectivo);

            if (!leer_Auxiliar.Exec(sSql))
            {
                GnDll_SII_SESEQ.MensajePantalla("ERROR: " + leer_Auxiliar.MensajeError);
            }
            else
            {
                while (leer_Auxiliar.Leer())
                {
                    sIdEmpresa = Fg.PonCeros(leer_Auxiliar.Campo("IdEmpresa"), 3);
                    sIdEstado = Fg.PonCeros(leer_Auxiliar.Campo("IdEstado"), 2);
                    sIdFarmacia = Fg.PonCeros(leer_Auxiliar.Campo("IdFarmacia"), 4);

                    EnviarAcusesDigitalizacionColectivo(true);
                }
            }

            sIdEmpresa_Aux = sIdEmpresa;
            sIdEstado_Aux = sIdEstado;
            sIdFarmacia_Aux = sIdFarmacia;

            return bRegresa;
        }

        public bool EnviarAcusesDigitalizacionColectivo(bool EnviarXml)
        {
            bool bRegresa = true;
            string sFolioSurtido = "";
            string sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0014_ObtenerDigitalizacionEnviarAcuse \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TipoDeProceso = '{3}' \n",
                sIdEmpresa, sIdEstado, sIdFarmacia, (int)TipoProcesoReceta.Colectivo);

            iErroresEnvioAcusesReceta = 0;
            if (!leerFolios.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerFolios, "EnviarAcusesDigitalizacionReceta()");
            }
            else
            {
                sRespuesta_Acuse_Respuestas = "";
                while (leerFolios.Leer())
                {
                    sFolioSurtido = leerFolios.Campo("FolioVenta");
                    bRegresa = EnviarAcusesDigitalizacionReceta(sFolioSurtido, EnviarXml);
                    if (bRegresa)
                    {
                        iErroresEnvioAcusesReceta++;
                    }
                }
            }

            return bRegresa;
        }

        public bool GeneralEnviarAcusesColectivo()
        {
            bool bRegresa = true;
            string sSql =string.Format(
                "Select \n" +
                "\tIdEmpresa, IdEstado, IdFarmacia, count(*) as Registros \n" +
                "From INT_SESEQ__RecetasElectronicas_0001_General (NoLock) \n" +
                "Where Procesado = 0 and TipoDeProceso = {0} \n" +
                "Group by IdEmpresa, IdEstado, IdFarmacia \n", (int)TipoProcesoReceta.Colectivo);

            if (!leer_Auxiliar.Exec(sSql))
            {
                GnDll_SII_SESEQ.MensajePantalla("ERROR: " + leer_Auxiliar.MensajeError);
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
            string sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0006_RecetasEnviarAcuse \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TipoDeProceso = '{3}' \n",
                sIdEmpresa, sIdEstado, sIdFarmacia, (int)TipoProcesoReceta.Colectivo);

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
            string sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0006_RecetasEnviarAcuse_Detalles \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}' \n",
                sIdEmpresa, sIdEstado, sIdFarmacia, FolioVenta);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "EnviarAcusesColectivo()");
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
                    //bRegresa = EnviarAcusesColectivo_Interno(leer.Campo("FolioVenta"), EnviarXml);
                    FolioVenta = leer.Campo("FolioVenta");
                    bRegresa = EnviarAcuses_Dispensacion(FolioVenta, TipoProcesoReceta.Colectivo, EnviarXml);
                    if (bRegresa)
                    {
                        iErroresEnvioAcusesReceta++;
                    }
                }
            }

            return bRegresa;
        }

        public bool ValidarFirmaDeEntregaColectivo(string Folio_SESEQ, string FolioDeColectivo, string Firma)
        {
            bool bRegresa = true;

            sRespuesta_Acuse = web.ValidarFirmaDeEntregaColectivo(FolioDeColectivo, Firma);

            bRegresa = RegistrarEntregaColectivo(Folio_SESEQ, FolioDeColectivo, sRespuesta_Acuse); 

            return bRegresa;
        }

        private bool RegistrarEntregaColectivo(string Folio_SESEQ, string FolioDeColectivo, string Informacion_XML)
        {
            bool bRegresa = false;
            string sSql = "";

            try
            {
                string[] sRespuesta = Informacion_XML.Split('|');

                if (sRespuesta != null)
                {
                    if (sRespuesta[0] == "0")
                    {
                        bRegresa = true;

                        sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0032_Registrar_Colectivos_Entregados \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio_SESEQ = '{3}'  \n",
                            sIdEmpresa, sIdEstado, sIdFarmacia, Folio_SESEQ );

                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            Error.GrabarError(leer, "RegistrarEntregaColectivo()");
                        }
                    }
                    else
                    {
                        Error.GrabarError(leer, string.Format("{0}|{1}", Informacion_XML, FolioDeColectivo), "RegistrarEntregaColectivo");
                    }
                }
            }
            catch { }

            return bRegresa;
        }
        #endregion Acuses de surtido de colectivos
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private string getTabs(int Tabs)
        {
            return GnDll_SII_SESEQ.getTabs(Tabs);
        }
        #endregion Funciones y Procedimientos Privados
    }
}
