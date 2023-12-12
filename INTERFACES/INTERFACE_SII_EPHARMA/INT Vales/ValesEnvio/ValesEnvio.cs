using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
//using System.Web.Services.Protocols;
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

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using DllFarmaciaSoft;
using Dll_INT_EPharma;

namespace INT_Vales.ValesEnvio
{
    class ValesEnvio
    {
        Dll_INT_EPharma.wsEPharmaInformacionDeVales.wsEPharma_RecepcionVales wsRecepcion_cnn = new Dll_INT_EPharma.wsEPharmaInformacionDeVales.wsEPharma_RecepcionVales();
        //ValesEnvio wsRecepcion_cnn_Local;
        //ResponseLogin login = new ResponseLogin();
        clsConexionSQL cnn;
        clsLeer leer;
        clsLeer LeerInformacion;
        clsGrabarError Error;
        clsDatosConexion datosConexion;
        //Dll_INT_EPharma.wsEPharmaInformacionDeVales.ValesRecepcionRegistrarInformacion info;
        clsLeer leerDestinos;

        basGenerales Fg = new basGenerales();

        //string sIdEmpresa = "";
        //string sFolioSolicitud = "";
        //string sIdCliente = "";
        //string sIdSucursal = "";
        string sMensajeError = "";

        public ValesEnvio(clsDatosConexion DatosDeConexion)
        {
            datosConexion = DatosDeConexion;
            cnn = new clsConexionSQL(datosConexion);
            leer = new clsLeer(ref cnn);
            leerDestinos = new clsLeer(ref cnn);
            LeerInformacion = new clsLeer(ref cnn);
            Error = new clsGrabarError(datosConexion, GnFarmacia.DatosApp, "ValesEnvio()");
        }

        public bool ObtenerInformacion()
        {
            bool bRegresa = true;

            bRegresa = ObtenerDestino();

            if (bRegresa)
            {
                string sSql = string.Format("Exec spp_ValesPorEnviar");

                if (!LeerInformacion.Exec(sSql))
                {
                    bRegresa = false;
                    General.Error.GrabarError(LeerInformacion, "ObtenerInformacion()");
                    System.Console.WriteLine(string.Format("        Error : {0}", LeerInformacion.MensajeError));
                }
            }

            return bRegresa;
        }

        private bool ObtenerDestino()
        {
            bool bRegresa = true;
            string sSql = string.Format("Select IdEmpresa, IdEstado, IdFarmacia, SSL, Servidor, WebService, PaginaWeb, IdOrden, Status, " +
                " (IdEstado + IdFarmacia) as DirDestino, cast(0 as bit) as EnLinea " +
                " From CFG_Vales_ConfigurarConexiones " +
                " Where Status = 'A' " +
                " Order by IdOrden ", DtGeneral.EstadoConectado);

            if (leerDestinos.Registros == 0)
            {
                System.Console.WriteLine("Obteniendo listado de servidores destino.");
                if (!leerDestinos.Exec(sSql))
                {
                    bRegresa = false;
                    General.Error.GrabarError(leerDestinos, "ObtenerDestino()");
                    System.Console.WriteLine(string.Format("        Error : {0}", leerDestinos.MensajeError));
                }
            }
            else
            {
                leerDestinos.RegistroActual = 0;
            }

            return bRegresa;
        }

        public bool EnviarInformacion()
        {
            bool bRegresa = true;

            bRegresa = RevisarConexiones();

            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("Iniciando proceso de envió.......");

            leer = LeerInformacion;

            while (leer.Leer() && bRegresa)
            {
                bRegresa = EnviarVale(leer.Campo("IdEstado"), leer.Campo("IdFarmacia"), leer.Campo("Folio_Vale"));
            }

            return bRegresa;
        }

        public bool EnviarVale(string IdEstado, string IdFarmacia,  string Folio)
        {
            bool bRegresa = false;

            DataTable dtEnc = new DataTable();
            DataTable dtDetalles = new DataTable();
            DataSet dtsTemp = new DataSet();

            clsLeer leerFolio = new clsLeer();
            sMensajeError = "";

            try
            {
                //dtsPedidos = LeerInformacion.DataSetClase;

                leerFolio.DataRowsClase = LeerInformacion.DataSetClase.Tables[0].Select(string.Format(" IdEstado = '{0}' And IdFarmacia = '{1}' And Folio_Vale = '{2}' ",
                                                                                                        Fg.PonCeros(IdEstado, 2),  Fg.PonCeros(IdFarmacia, 4),  Fg.PonCeros(Folio, 8)));
                leerFolio.RenombrarTabla(1, "Encabezado");
                dtEnc = leerFolio.DataTableClase;

                leerFolio.DataRowsClase = LeerInformacion.DataSetClase.Tables[1].Select(string.Format(" IdEstado = '{0}' And IdFarmacia = '{1}' And Folio_Vale = '{2}' ",
                                                                                                        Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8)));
                leerFolio.RenombrarTabla(1, "Detalles");
                dtDetalles = leerFolio.DataTableClase;


                dtsTemp.Tables.Add(dtEnc.Copy());
                dtsTemp.Tables.Add(dtDetalles.Copy());
                leerFolio.DataSetClase = dtsTemp;

                bRegresa = EnviarFolio(leerFolio);
            }
            catch (Exception ex)
            {
                sMensajeError = string.Format("Error 01 : EnviarPedido({0})", Folio);
                Error.GrabarError(ex, string.Format("EnviarPedido({0})", Folio));
            }


            return bRegresa;
        }

        public bool EnviarFolio(clsLeer Pedido)
        {
            bool bRegresa = false;
            Dll_INT_EPharma.wsEPharmaInformacionDeVales.ResponseSolicitud respuesta = new Dll_INT_EPharma.wsEPharmaInformacionDeVales.ResponseSolicitud();
            Dll_INT_EPharma.wsEPharmaInformacionDeVales.ValesRecepcionRegistrarInformacion informacion = new Dll_INT_EPharma.wsEPharmaInformacionDeVales.ValesRecepcionRegistrarInformacion();
            List<Dll_INT_EPharma.wsEPharmaInformacionDeVales.ItemInsumo> insumos = new List<Dll_INT_EPharma.wsEPharmaInformacionDeVales.ItemInsumo>();
            Dll_INT_EPharma.wsEPharmaInformacionDeVales.ItemInsumo insumoItem = new Dll_INT_EPharma.wsEPharmaInformacionDeVales.ItemInsumo();
            int iRegistro = 0;

            clsLeer detalles = new clsLeer();

            try
            {
                Pedido.RegistroActual = 1;
                if (Pedido.Leer())
                {
                    detalles.DataTableClase = Pedido.Tabla("Detalles");

                    informacion.IdEmpresa = Pedido.Campo("IdEmpresa");
                    informacion.IdEstado = Pedido.Campo("IdEstado");
                    informacion.IdFarmacia = Pedido.Campo("IdFarmacia");
                    informacion.Folio_Vale = Pedido.Campo("Folio_Vale");
                    informacion.FechaEmision_Vale = Pedido.CampoFecha("FechaEmision_Vale");
                    informacion.IdPersonal = Pedido.Campo("IdPersonal");
                    informacion.IdTipoDeDispensacion = Pedido.Campo("IdTipoDeDispensacion");
                    informacion.NumReceta = Pedido.Campo("NumReceta");
                    informacion.IdBeneficio = Pedido.Campo("IdBeneficio");
                    informacion.IdDiagnostico = Pedido.Campo("IdDiagnostico");
                    informacion.RefObservaciones = Pedido.Campo("RefObservaciones");
                    informacion.FechaReceta = Pedido.CampoFecha("FechaReceta");


                    informacion.Beneficiario = new Dll_INT_EPharma.wsEPharmaInformacionDeVales.ItemBeneficiario();
                    informacion.Beneficiario.Nombre = Pedido.Campo("Beneficiario_Nombre");
                    informacion.Beneficiario.ApPaterno = Pedido.Campo("Beneficiario_ApPaterno");
                    informacion.Beneficiario.ApMaterno = Pedido.Campo("Beneficiario_ApMaterno");
                    informacion.Beneficiario.Referencia = Pedido.Campo("Beneficiario_FolioReferencia");
                    informacion.Beneficiario.Sexo = Pedido.Campo("Beneficiario_Sexo");
                    informacion.Beneficiario.FechaNacimiento = Pedido.CampoFecha("Beneficiario_FechaNacimiento");
                    informacion.Beneficiario.FechaFinVigencia = Pedido.CampoFecha("Beneficiario_FechaFinVigencia");
                    //informacion.Beneficiario.Direccion = Pedido.Campo("Domicilio");
                    //informacion.Beneficiario.Telefono = Pedido.Campo("Telefono");


                    informacion.Medico = new Dll_INT_EPharma.wsEPharmaInformacionDeVales.ItemMedico();
                    informacion.Medico.Nombre = Pedido.Campo("Medico_Nombre");
                    informacion.Medico.ApPaterno = Pedido.Campo("Medico_ApPaterno");
                    informacion.Medico.ApMaterno = Pedido.Campo("Medico_ApMaterno");
                    informacion.Medico.Referencia = Pedido.Campo("Medico_NumCedula");


                    detalles.RegistroActual = 1;
                    while (detalles.Leer())
                    {
                        insumoItem = new Dll_INT_EPharma.wsEPharmaInformacionDeVales.ItemInsumo();
                        insumoItem.ClaveInsumo = detalles.Campo("ClaveSSA");
                        insumoItem.Piezas = detalles.CampoInt("Piezas");
                        insumoItem.Descripcion = detalles.Campo("DescripcionClave");
                        insumos.Add(insumoItem);
                        iRegistro++;
                    }


                    informacion.Insumos = new Dll_INT_EPharma.wsEPharmaInformacionDeVales.ItemInsumo[iRegistro];
                    informacion.Insumos = insumos.ToArray();                    

                    respuesta = wsRecepcion_cnn.EnviarInformacionDeVale(informacion);


                    if (respuesta.Error)
                    {
                        bRegresa = false;
                        sMensajeError = respuesta.Mensaje;
                    }
                    else
                    {
                        bRegresa = true;
                        MarcarValeEnviado(informacion);
                    }
                }
            }
            catch (Exception ex)
            {
                Pedido.RegistroActual = 1;
                Pedido.Leer();
                sMensajeError = string.Format("Error 02 : EnviarPedido_C({0})", Pedido.Campo("UUID"));
                Error.GrabarError(ex, string.Format("EnviarPedido_C({0})", Pedido.Campo("UUID")));
            }

            return bRegresa;
        }

        private bool RevisarConexiones()
        {
            clsPing ping = new clsPing();
            bool bConexion = true;
            DllTransferenciaSoft.wsCliente.wsCnnCliente Cliente = new DllTransferenciaSoft.wsCliente.wsCnnCliente();
            string sURL = "";
            string sSSL = "";
            string sError = "";

            leerDestinos.RegistroActual = 0;
            if (leerDestinos.Leer())
            {
                try
                {
                    sSSL = leerDestinos.CampoBool("SSL") ? "s" : "";

                    sURL = string.Format("http{0}://{1}/{2}/{3}.asmx", sSSL, leerDestinos.Campo("Servidor"), leerDestinos.Campo("WebService"), leerDestinos.Campo("PaginaWeb"));
                    sURL = sURL.ToLower().Replace(".asmx", "") + ".asmx";

                    Cliente.Url = sURL;
                    Cliente.Timeout = 1000 * (45);

                    wsRecepcion_cnn.Url = sURL;

                    System.Console.WriteLine(string.Format("Verificando conexión con : {0}", sURL));

                    //bConexion = Cliente.TestConection();

                    System.Console.WriteLine(string.Format("Conexión con : {0}, {1}", sURL, bConexion.ToString()));
                }
                catch (Exception ex)
                {
                    sError = ex.Message;
                    bConexion = false;
                }
                finally
                {
                    Cliente = null;
                }
            }
            return bConexion;
        }

        private bool MarcarValeEnviado(Dll_INT_EPharma.wsEPharmaInformacionDeVales.ValesRecepcionRegistrarInformacion info)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_Mtto_Vales_Emision_Envio " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVale = '{3}' ",
                info.IdEmpresa, info.IdEstado, info.IdFarmacia, info.Folio_Vale);

            if (!leerDestinos.Exec(sSql))
            {
                Error.GrabarError(leerDestinos, "MarcarPedidoEnviado");
            }

            return bRegresa;
        }
    }
}
