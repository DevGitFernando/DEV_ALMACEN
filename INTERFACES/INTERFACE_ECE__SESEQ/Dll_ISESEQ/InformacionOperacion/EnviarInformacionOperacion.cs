using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
using System.Collections.Generic;
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
using Dll_ISESEQ.wsInformacionOperativa_SESEQ; 


namespace Dll_ISESEQ.InformacionOperacion
{
    public class EnviarInformacionOperacion
    {
        enum TipoDeMovimiento
        {
            Ninguno = 0,
            EntradasDeConsignacion = 1, 
            TransferenciasDeSalida = 2, 
            SalidasCentrosDeSalud = 3,
            SalidasRecetaElectronica = 4,
            SalidasDeColectivos = 5,
            TransfereciasDeEntrada = 6, 
            Devoluciones = 7, 
            AjustesPositivos = 8, 
            AjustesNegativos = 9  
        }

        clsDatosConexion datosDeConexion;
        clsConexionSQL cnn;
        clsLeer leer;
        clsLeer leer_Auxiliar;
        clsLeer leerFolios;
        clsLeer leerFolios_Detalles; 
        clsLeer leerXML;
        clsLeer leerFechaMovimiento; 
        clsGrabarError Error;

        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";

        string sIdEmpresa_Aux = "";
        string sIdEstado_Aux = "";
        string sIdFarmacia_Aux = "";

        string sRespuesta_Recepcion = ""; 

        string sIdUMedica = "";
        string sInformacion_XML = "";
        string sFolio_SESEQ = "";
        string sFolioRegistro = "";
        string sMensajesError = "";
        TipoProcesoReceta tpProceso = TipoProcesoReceta.Ninguno;
        string sTipoDeProceso = "";
        string sURL_EnvioInformacion = ""; 

        DataSet dtsInformacion = new DataSet();
        TipoDeMovimiento tpTipoMovimientoEnProceso = TipoDeMovimiento.Ninguno;
        string sSQL_Enviado = ""; 

        basGenerales Fg = new basGenerales();
        string sEncabezado = "";


        int iErroresEnvioAcusesReceta = 0;
        int iErroresEnvioAcusesCancelacionReceta = 0;

        wsInformacionOperativa_SESEQ.WebServiceSESEQ  web = new wsInformacionOperativa_SESEQ.WebServiceSESEQ();

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

        public EnviarInformacionOperacion(clsDatosConexion DatosConexion, string Empresa, string Estado, string Farmacia)
        {
            datosDeConexion = DatosConexion;
            cnn = new clsConexionSQL(datosDeConexion);
            leer = new clsLeer(ref cnn);
            leer_Auxiliar = new clsLeer(ref cnn); 
            leerFolios = new clsLeer(ref cnn);
            leerFolios_Detalles = new clsLeer(ref cnn); 
            leerXML = new clsLeer();
            leerFechaMovimiento = new clsLeer(ref cnn);

            Error = new clsGrabarError(datosDeConexion, GnDll_SII_SESEQ.DatosApp, "EnviarInformacionOperacion");
            Error.NombreLogErorres = "INT_SESEQ__CtlErrores";

            sEncabezado = string.Format("<?xml version={0}1.0{0} encoding={0}UTF-8{0}?>", Fg.Comillas());

            sIdEmpresa = Fg.PonCeros(Empresa, 3);
            sIdEstado = Fg.PonCeros(Estado, 2);
            sIdFarmacia = Fg.PonCeros(Farmacia, 4);

            sIdEmpresa_Aux = sIdEmpresa;
            sIdEstado_Aux = sIdEstado;
            sIdFarmacia_Aux = sIdFarmacia;


            web = new wsInformacionOperativa_SESEQ.WebServiceSESEQ();

            GnDll_SII_SESEQ.ObtenerURL_Interface(IdEmpresa, IdEstado, IdFarmacia);
            //web.Url = GnDll_SII_SESEQ.URL_Interface;
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
        public bool EnviarInformacion()
        {
            bool bRegresa = true;
            string sSql =
                "Select \n" +
                "\tI.IdEmpresa, I.IdEstado, I.IdFarmacia, I.Referencia_SESEQ, I.URL_Interface, I.URL_InformacionOperacion, I.CapturaInformacion \n" +
                "From INT_SESEQ__CFG_Farmacias_UMedicas  I (NoLock) \n" +
                "Inner Join \n" +
                "(\n" +
                "\tSelect IdEmpresa, IdEstado, IdFarmacia \n" +
                "\tFrom FarmaciaProductos F (NoLock) \n" +
                "\tGroup by IdEmpresa, IdEstado, IdFarmacia \n" + 
                ") as F " + 
                "On ( I.IdEmpresa = F.IdEmpresa and I.IdEstado = F.IdEstado and I.IdFarmacia = F.IdFarmacia ) \n\n" + 
                "";

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

                    sURL_EnvioInformacion = leer_Auxiliar.Campo("URL_InformacionOperacion");

                    EnviarInformacionUnidad(sIdEmpresa, sIdEstado, sIdFarmacia ); 
                }
            }

            sIdEmpresa_Aux = sIdEmpresa;
            sIdEstado_Aux = sIdEstado;
            sIdFarmacia_Aux = sIdFarmacia; 

            return bRegresa; 
        }

        public bool EnviarInformacionUnidad(string IdEmpresa, string IdEstado, string IdFarmacia) 
        {
            bool bRegresa = true;
            string sFecha = "";
            int iTipo = 0;
            string sSql = string.Format("Exec spp_INT_SESEQ__Informacion_0101_GetInformacion \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IntegrarInformacion = 0 \n", 
                IdEmpresa, IdEstado, IdFarmacia);

            iErroresEnvioAcusesReceta = 0;
            if (!leerFolios.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerFolios, "EnviarInformacionUnidad()"); 
            }
            else
            {
                sRespuesta_Acuse_Respuestas = "";
                while (leerFolios.Leer())
                {
                    sFecha = leerFolios.Campo("Fecha");
                    iTipo = leerFolios.CampoInt("Tipo");

                    bRegresa = EnviarInformacionUnidad_x_Fecha_x_TipoMovimiento(IdEmpresa, IdEstado, IdFarmacia, sFecha, iTipo);
                    //if(bRegresa)
                    //{
                    //    iErroresEnvioAcusesReceta++;
                    //}
                }
            }

            ///// Registrar las fechas procesadas por completo 
            sSql = string.Format("Exec spp_INT_SESEQ__Informacion_0101_GetInformacion \n" +
                "@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IntegrarInformacion = 1 \n",
                IdEmpresa, IdEstado, IdFarmacia);

            leerFolios.Exec(sSql); 

            return bRegresa; 
        }

        public bool EnviarInformacionUnidad_x_Fecha_x_TipoMovimiento( string IdEmpresa, string IdEstado, string IdFarmacia, string Fecha, int Tipo )
        {
            bool bRegresa = true;
            TipoDeMovimiento tipoMovto = (TipoDeMovimiento)Tipo; 
            string sFolioSurtido = "";
            string sSql = string.Format("Exec spp_INT_SESEQ__Informacion " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FechaValidacion = '{3}', @Opcion = '{4}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fecha, Tipo);

            iErroresEnvioAcusesReceta = 0;
            sSQL_Enviado = sSql; 
            if(!leerFechaMovimiento.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerFechaMovimiento, "EnviarInformacionUnidad_x_Fecha_x_TipoMovimiento()");
            }
            else
            {
                sRespuesta_Recepcion = ""; 
                if(!leerFechaMovimiento.Leer())
                {
                    RegistrarMovimientoProcesado(IdEmpresa, IdEstado, IdFarmacia, Fecha, Tipo, 0, 0);
                }
                else 
                {
                    bRegresa = ProcesarMovimiento(tipoMovto, leerFechaMovimiento.DataSetClase);
                    //if(bRegresa)
                    {
                        iErroresEnvioAcusesReceta++;
                        RegistrarMovimientoProcesado(IdEmpresa, IdEstado, IdFarmacia, Fecha, Tipo, 1, Convert.ToInt32(!bRegresa)); 
                    }
                }
            }

            return bRegresa;
        }

        private bool RegistrarMovimientoProcesado( string IdEmpresa, string IdEstado, string IdFarmacia, string Fecha, int Tipo, int ExistenDatos, int HuboError )
        {
            bool bRegresa = false;

            string sSql = string.Format("Exec spp_INT_SESEQ__Informacion_0111_RegistrarEnvio \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FechaValidacion = '{3}', \n" +
                "\t@Opcion = '{4}', @ExistenDatos = '{5}', @HuboError = '{6}', @Respuesta_Integracion = '{7}' \n",
                IdEmpresa, IdEstado, IdFarmacia, Fecha, Tipo, ExistenDatos, HuboError, sRespuesta_Recepcion);

            bRegresa = leer.Exec(sSql); 

            return bRegresa;  
        }

        private bool ProcesarMovimiento( TipoDeMovimiento Tipo, DataSet Informacion )
        {
            bool bRegresa = false;
            tpTipoMovimientoEnProceso = Tipo;

            switch(Tipo)
            {
                case TipoDeMovimiento.EntradasDeConsignacion:
                    bRegresa = Procesar_001_EntradasConsignacion(Informacion);
                    break;

                case TipoDeMovimiento.TransferenciasDeSalida:
                    bRegresa = Procesar_002_TransferenciaDeSalida(Informacion);
                    break;

                case TipoDeMovimiento.SalidasCentrosDeSalud:
                    bRegresa = Procesar_003_SalidaCentroDeSalud(Informacion);
                    break;

                case TipoDeMovimiento.SalidasRecetaElectronica:
                    bRegresa = Procesar_004_SalidaRecetaElectronica(Informacion);
                    break;

                case TipoDeMovimiento.SalidasDeColectivos:
                    bRegresa = Procesar_005_SalidaColectivo(Informacion);
                    break;

                case TipoDeMovimiento.TransfereciasDeEntrada:
                    bRegresa = Procesar_006_TransrefenciaDeEntrada(Informacion);
                    break;

                case TipoDeMovimiento.Devoluciones:
                    bRegresa = Procesar_007_Devoluciones(Informacion);
                    break;

                case TipoDeMovimiento.AjustesPositivos:
                    bRegresa = Procesar_008_AjustesPositivos(Informacion);
                    break;

                case TipoDeMovimiento.AjustesNegativos:
                    bRegresa = Procesar_009_AjustesNegativos(Informacion);
                    break;
            }

            return bRegresa;  
        }

        private bool EnviarInformacion_Interno( datos_entrada[] Datos_a_Enviar )
        {
            bool bRegresa = false;
            string sRegresa = "";
            string sXML_Envio = "";

            List<datos_entrada> listaDeSurtido = new List<datos_entrada>();
            datos_entrada[] datosDeSurtido = null;
            datos_entrada datos = new datos_entrada();
            datos_salida respuesta = new datos_salida();
            string sTipo = "";
            string[] sRespuesta = null;
            string sRespuesta_Codigo = "";
            string sRespuesta_Mensaje = ""; 

            //ResponseGeneral empacado = new ResponseGeneral();
            //empacado.Respuesta = Informacion;
            //sXML_Envio = empacado.GetResponse(false);

            web = new WebServiceSESEQ();
            web.Url = sURL_EnvioInformacion;

            try
            {
                respuesta = web.recepcionMovInv(Datos_a_Enviar);

                sTipo = tpTipoMovimientoEnProceso.ToString();
                sRespuesta = respuesta.mensaje.Split('|');
                sRespuesta_Codigo = sRespuesta[0];
                sRespuesta_Mensaje = sRespuesta[1];
                sRespuesta_Recepcion = sRespuesta_Mensaje;

                if(Convert.ToInt32("0" + sRespuesta_Codigo) == 0)
                {
                    sTipo = "";
                    bRegresa = true;
                }
                else 
                {
                    if (sRespuesta_Mensaje.ToUpper().Contains("CARGA") )
                    {
                        bRegresa = true;
                    }
                    else 
                    {
                        sSQL_Enviado = sSQL_Enviado.Replace("'", "" + Fg.Comillas() + "");
                        Error.GrabarError(string.Format("{0}  {1}", sRespuesta_Mensaje, sSQL_Enviado), string.Format("EnviarInformacion_Interno: {0}", sTipo));
                    } 
                }

                //foreach(datos_entrada item in Datos_a_Enviar)
                //{
                //    respuesta = web.recepcionMovInv(item);
                //    break; 
                //}
            }
            catch (Exception ex)
            {
                sRespuesta_Recepcion = ex.Message;
            }
            return bRegresa;
        }

        private bool RegistrarAcuseDeSurtido_Enviado(string Folio_SESEQ, string Informacion_XML)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0008_RecetasEnviar_Procesado " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio_SESEQ = '{3}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, Folio_SESEQ);
            try
            {

                sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0008_RecetasEnviar_ActualizarNumeroDeEnvios " +
                    "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio_SESEQ = '{3}' ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, Folio_SESEQ );

                // Actualizar el numero de intentos de envio de la respuesta 
                leer.Exec(sSql); 


                string[] sRespuesta = Informacion_XML.Split('|');

                if (sRespuesta != null)
                {
                    if (sRespuesta[0] == "0")
                    {
                        bRegresa = true;

                        sSql = string.Format("Exec spp_INT_SESEQ__RecetasElectronicas_0008_RecetasEnviar_Procesado " +
                            "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio_SESEQ = '{3}' ",
                            sIdEmpresa, sIdEstado, sIdFarmacia, Folio_SESEQ);

                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            Error.GrabarError(leer, "EnviarAcusesCancelacionReceta()");
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
        #endregion Funciones y Procedimientos Publicos

        #region Procesar movimientos 
        private bool Procesar_001_EntradasConsignacion( DataSet Informacion )
        {
            bool bRegresa = false;
            List<datos_entrada> listaDeEntrada = new List<datos_entrada>();
            datos_entrada[] datosDeEntrada = null;
            datos_entrada datos = new datos_entrada();

            clsLeer leerProceso = new clsLeer();
            leerProceso.DataSetClase = Informacion;

            if(leerProceso.Registros > 0)
            {
                while(leerProceso.Leer())
                {
                    datos = new datos_entrada();

                    datos.tipoMov = leerProceso.CampoInt("tipoMov").ToString();
                    datos.folio = leerProceso.Campo("folio");
                    datos.ccc1 = leerProceso.Campo("ccc1");
                    datos.clave = leerProceso.Campo("clave");
                    datos.lote = leerProceso.Campo("lote");
                    datos.caducidad = leerProceso.Campo("caducidad");
                    datos.cantidad = leerProceso.CampoDec("Cantidad").ToString("######0.###0");
                    datos.fechaMov = leerProceso.Campo("fechaMov");

                    listaDeEntrada.Add(datos);
                }

                datosDeEntrada = listaDeEntrada.ToArray();

                //bRegresa = EnviarInformacion_Interno(datosDeEntrada);
                bRegresa = EnviarInformacion_Interno(datosDeEntrada);
            }

            return bRegresa;
        }

        private bool Procesar_002_TransferenciaDeSalida( DataSet Informacion )
        {
            bool bRegresa = false;
            List<datos_entrada> listaDeEntrada = new List<datos_entrada>();
            datos_entrada[] datosDeEntrada = null;
            datos_entrada datos = new datos_entrada();

            clsLeer leerProceso = new clsLeer();
            leerProceso.DataSetClase = Informacion;

            if(leerProceso.Registros > 0)
            {
                while(leerProceso.Leer())
                {
                    datos = new datos_entrada();

                    datos.tipoMov = leerProceso.CampoInt("tipoMov").ToString();
                    datos.folio = leerProceso.Campo("folio");
                    datos.ccc1 = leerProceso.Campo("ccc1");
                    datos.ccc2 = leerProceso.Campo("ccc2");
                    datos.clave = leerProceso.Campo("clave");
                    datos.lote = leerProceso.Campo("lote");
                    datos.caducidad = leerProceso.Campo("caducidad");
                    datos.cantidad = leerProceso.CampoDec("Cantidad").ToString("######0.###0");
                    datos.fechaMov = leerProceso.Campo("fechaMov");

                    listaDeEntrada.Add(datos);
                }

                datosDeEntrada = listaDeEntrada.ToArray();

                //bRegresa = EnviarInformacion_Interno(datosDeEntrada);
                bRegresa = EnviarInformacion_Interno(datosDeEntrada);
            }

            return bRegresa;
        }

        private bool Procesar_003_SalidaCentroDeSalud( DataSet Informacion )
        {
            bool bRegresa = false;
            List<datos_entrada> listaDeEntrada = new List<datos_entrada>();
            datos_entrada[] datosDeEntrada = null;
            datos_entrada datos = new datos_entrada();

            clsLeer leerProceso = new clsLeer();
            leerProceso.DataSetClase = Informacion;

            if(leerProceso.Registros > 0)
            {
                while(leerProceso.Leer())
                {
                    datos = new datos_entrada();

                    datos.tipoMov = leerProceso.CampoInt("tipoMov").ToString();
                    datos.ordenReposicion = leerProceso.Campo("ordenReposicion");
                    datos.folio = leerProceso.Campo("folio");
                    datos.ccc1 = leerProceso.Campo("ccc1");
                    datos.cccjr = leerProceso.Campo("cccjr");
                    datos.clave = leerProceso.Campo("clave");
                    datos.lote = leerProceso.Campo("lote");
                    datos.caducidad = leerProceso.Campo("caducidad");
                    datos.cantidad = leerProceso.CampoDec("Cantidad").ToString("######0.###0");
                    datos.perteneceSESEQ = leerProceso.Campo("PerteneceSESEQ");
                    datos.fechaMov = leerProceso.Campo("fechaMov");

                    listaDeEntrada.Add(datos);
                }

                datosDeEntrada = listaDeEntrada.ToArray();

                //bRegresa = EnviarInformacion_Interno(datosDeEntrada);
                bRegresa = EnviarInformacion_Interno(datosDeEntrada);
            }

            return bRegresa;
        }

        private bool Procesar_004_SalidaRecetaElectronica( DataSet Informacion )
        {
            bool bRegresa = false;
            List<datos_entrada> listaDeEntrada = new List<datos_entrada>();
            datos_entrada[] datosDeEntrada = null;
            datos_entrada datos = new datos_entrada();

            clsLeer leerProceso = new clsLeer();
            leerProceso.DataSetClase = Informacion;

            if(leerProceso.Registros > 0)
            {
                while(leerProceso.Leer())
                {
                    datos = new datos_entrada();

                    datos.tipoMov = leerProceso.CampoInt("tipoMov").ToString();
                    datos.folio = leerProceso.Campo("folio");
                    datos.ccc1 = leerProceso.Campo("ccc1");
                    datos.noReceta = leerProceso.Campo("NoReceta");
                    datos.clave = leerProceso.Campo("clave");
                    datos.lote = leerProceso.Campo("lote");
                    datos.caducidad = leerProceso.Campo("caducidad");
                    datos.cantidad = leerProceso.CampoDec("Cantidad").ToString("######0.###0");
                    datos.perteneceSESEQ = leerProceso.Campo("PerteneceSESEQ");
                    datos.fechaMov = leerProceso.Campo("fechaMov");

                    listaDeEntrada.Add(datos);
                }

                datosDeEntrada = listaDeEntrada.ToArray();

                //bRegresa = EnviarInformacion_Interno(datosDeEntrada);
                bRegresa = EnviarInformacion_Interno(datosDeEntrada);
            }

            return bRegresa;
        }

        private bool Procesar_005_SalidaColectivo( DataSet Informacion )
        {
            bool bRegresa = false;
            List<datos_entrada> listaDeEntrada = new List<datos_entrada>();
            datos_entrada[] datosDeEntrada = null;
            datos_entrada datos = new datos_entrada();

            clsLeer leerProceso = new clsLeer();
            leerProceso.DataSetClase = Informacion;

            if(leerProceso.Registros > 0)
            {
                while(leerProceso.Leer())
                {
                    datos = new datos_entrada();

                    datos.tipoMov = leerProceso.CampoInt("tipoMov").ToString();
                    datos.folio = leerProceso.Campo("folio");
                    datos.ccc1 = leerProceso.Campo("ccc1");
                    datos.clave = leerProceso.Campo("clave");
                    datos.lote = leerProceso.Campo("lote");
                    datos.caducidad = leerProceso.Campo("caducidad");
                    datos.cantidad = leerProceso.CampoDec("Cantidad").ToString("######0.###0");
                    datos.perteneceSESEQ = leerProceso.Campo("PerteneceSESEQ");
                    datos.fechaMov = leerProceso.Campo("fechaMov");
                    datos.responsable = leerProceso.Campo("Responsable");
                    datos.servicio = leerProceso.Campo("Servicio");

                    listaDeEntrada.Add(datos);
                }

                datosDeEntrada = listaDeEntrada.ToArray();

                //bRegresa = EnviarInformacion_Interno(datosDeEntrada);
                bRegresa = EnviarInformacion_Interno(datosDeEntrada);
            }

            return bRegresa;
        }

        private bool Procesar_006_TransrefenciaDeEntrada( DataSet Informacion )
        {
            bool bRegresa = false;
            List<datos_entrada> listaDeEntrada = new List<datos_entrada>();
            datos_entrada[] datosDeEntrada = null;
            datos_entrada datos = new datos_entrada();

            clsLeer leerProceso = new clsLeer();
            leerProceso.DataSetClase = Informacion;

            if(leerProceso.Registros > 0)
            {
                while(leerProceso.Leer())
                {
                    datos = new datos_entrada();

                    datos.tipoMov = leerProceso.CampoInt("tipoMov").ToString();
                    datos.folio = leerProceso.Campo("folio");
                    datos.ccc1 = leerProceso.Campo("ccc1");
                    datos.ccc2 = leerProceso.Campo("ccc2");
                    datos.clave = leerProceso.Campo("clave");
                    datos.lote = leerProceso.Campo("lote");
                    datos.caducidad = leerProceso.Campo("caducidad");
                    datos.cantidad = leerProceso.CampoDec("Cantidad").ToString("######0.###0");
                    datos.fechaMov = leerProceso.Campo("fechaMov");

                    listaDeEntrada.Add(datos);
                }

                datosDeEntrada = listaDeEntrada.ToArray();

                //bRegresa = EnviarInformacion_Interno(datosDeEntrada);
                bRegresa = EnviarInformacion_Interno(datosDeEntrada);
            }

            return bRegresa;
        }

        private bool Procesar_007_Devoluciones( DataSet Informacion )
        {
            bool bRegresa = false;
            List<datos_entrada> listaDeEntrada = new List<datos_entrada>();
            datos_entrada[] datosDeEntrada = null;
            datos_entrada datos = new datos_entrada();

            clsLeer leerProceso = new clsLeer();
            leerProceso.DataSetClase = Informacion;

            if(leerProceso.Registros > 0)
            {
                while(leerProceso.Leer())
                {
                    datos = new datos_entrada();

                    datos.tipoMov = leerProceso.CampoInt("tipoMov").ToString();
                    datos.folio = leerProceso.Campo("folio");
                    datos.ccc1 = leerProceso.Campo("ccc1");
                    datos.clave = leerProceso.Campo("clave");
                    datos.lote = leerProceso.Campo("lote");
                    datos.caducidad = leerProceso.Campo("caducidad");
                    datos.cantidad = leerProceso.CampoDec("Cantidad").ToString("######0.###0");
                    datos.fechaMov = leerProceso.Campo("fechaMov");
                    datos.observacion = leerProceso.Campo("Observaciones");

                    listaDeEntrada.Add(datos);
                }

                datosDeEntrada = listaDeEntrada.ToArray();

                //bRegresa = EnviarInformacion_Interno(datosDeEntrada);
                bRegresa = EnviarInformacion_Interno(datosDeEntrada);
            }

            return bRegresa;
        }

        private bool Procesar_008_AjustesPositivos( DataSet Informacion )
        {
            bool bRegresa = false;
            List<datos_entrada> listaDeEntrada = new List<datos_entrada>();
            datos_entrada[] datosDeEntrada = null;
            datos_entrada datos = new datos_entrada();

            clsLeer leerProceso = new clsLeer();
            leerProceso.DataSetClase = Informacion;

            if(leerProceso.Registros > 0)
            {
                while(leerProceso.Leer())
                {
                    datos = new datos_entrada();

                    datos.tipoMov = leerProceso.CampoInt("tipoMov").ToString();
                    datos.folio = leerProceso.Campo("folio");
                    datos.ccc1 = leerProceso.Campo("ccc1");
                    datos.clave = leerProceso.Campo("clave");
                    datos.lote = leerProceso.Campo("lote");
                    datos.caducidad = leerProceso.Campo("caducidad");
                    datos.cantidad = leerProceso.CampoDec("Cantidad").ToString("######0.###0");
                    datos.fechaMov = leerProceso.Campo("fechaMov");
                    datos.observacion = leerProceso.Campo("Observaciones");

                    listaDeEntrada.Add(datos);
                }

                datosDeEntrada = listaDeEntrada.ToArray();

                //bRegresa = EnviarInformacion_Interno(datosDeEntrada);
                bRegresa = EnviarInformacion_Interno(datosDeEntrada);
            }

            return bRegresa;
        }

        private bool Procesar_009_AjustesNegativos( DataSet Informacion )
        {
            bool bRegresa = false;
            List<datos_entrada> listaDeEntrada = new List<datos_entrada>();
            datos_entrada[] datosDeEntrada = null;
            datos_entrada datos = new datos_entrada();

            clsLeer leerProceso = new clsLeer();
            leerProceso.DataSetClase = Informacion;

            if(leerProceso.Registros > 0)
            {
                while(leerProceso.Leer())
                {
                    datos = new datos_entrada();

                    datos.tipoMov = leerProceso.CampoInt("tipoMov").ToString();
                    datos.folio = leerProceso.Campo("folio");
                    datos.ccc1 = leerProceso.Campo("ccc1");
                    datos.clave = leerProceso.Campo("clave");
                    datos.lote = leerProceso.Campo("lote");
                    datos.caducidad = leerProceso.Campo("caducidad");
                    datos.cantidad = leerProceso.CampoDec("Cantidad").ToString("######0.###0");
                    datos.fechaMov = leerProceso.Campo("fechaMov");
                    datos.observacion = leerProceso.Campo("Observaciones");

                    listaDeEntrada.Add(datos);
                }

                datosDeEntrada = listaDeEntrada.ToArray();

                //bRegresa = EnviarInformacion_Interno(datosDeEntrada);
                bRegresa = EnviarInformacion_Interno(datosDeEntrada);
            }

            return bRegresa;
        }
        #endregion Procesar movimientos 

        #region Funciones y Procedimientos Privados
        private string getTabs(int Tabs)
        {
            return GnDll_SII_SESEQ.getTabs(Tabs);
        }
        #endregion Funciones y Procedimientos Privados
    }
}
