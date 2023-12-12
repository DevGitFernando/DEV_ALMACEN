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
using System.Windows.Forms; 

using Microsoft.VisualBasic;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

//using Dll_SII_IFarmatel.wsFarmatel_Caprepa;
using Dll_SII_IFarmatel.wsEPharma_Solicitudes; 

using DllFarmaciaSoft; 

namespace Dll_SII_IFarmatel
{
    public class IFarmatel
    {
        wsEPharma_Solicitudes.wsRecepcionDeSolicitudes iFarmatel_cnn = new wsEPharma_Solicitudes.wsRecepcionDeSolicitudes();
        ResponseLogin loginFarmatel = new ResponseLogin();
        string sLogin = "";
        string sPassword = "";
        string sURL = ""; 
        bool bConexionConfigurada = false;
        bool bEnvioHabilitado = false;
        string sMensajeError = ""; 

        clsConexionSQL cnn; 
        clsLeer leer;
        clsGrabarError Error;
        clsLeer leerPedidos;
        basGenerales Fg = new basGenerales(); 

        public IFarmatel()
        {
            InicializarConexiones(); 
        }

        public void Test()
        {
            //RequestPedido pedido = new RequestPedido();
            //ResponseLogin login = new ResponseLogin();

            //iFarmatel_cnn.EnviaPedido(pedido);
        }

        #region Propiedades 
        public bool EnvioHabilitado
        {
            get { return bEnvioHabilitado; }
        }

        public ResponseLogin Login
        {
            get { return loginFarmatel; }
        }

        public clsLeer ServiciosADomicilio
        {
            get 
            {
                if (leerPedidos == null)
                {
                    leerPedidos = new clsLeer(); 
                }
                return leerPedidos;  
            }
        }

        public string MensajeError
        {
            get 
            {
                return sMensajeError; 
            }
        }
        #endregion Propiedades 

        #region Funciones y Procedimientos Publicos
        public void Informacion()
        {
            string sMensaje = "";

            sMensaje = string.Format("URL: {0}\n", sURL);

            General.msjAviso(sMensaje);
        }

        public bool GetToken()
        {
            bool bRegresa = false;
            bEnvioHabilitado = false; 

            try
            {

                iFarmatel_cnn = new wsEPharma_Solicitudes.wsRecepcionDeSolicitudes(); 
                iFarmatel_cnn.Url = sURL;

                loginFarmatel = iFarmatel_cnn.Login(DtGeneral.EmpresaConectada + DtGeneral.EstadoConectado + DtGeneral.FarmaciaConectada, sLogin, sPassword);

                bRegresa = loginFarmatel.Error; 
            }
            catch (Exception ex)
            {
                loginFarmatel.Error = true; 
            }

            bEnvioHabilitado = !bRegresa;
            return bRegresa; 
        }

        public bool GetConfiguracion()
        {
            bool bRegresa = false;
            string sSql = string.Format(
                "Select Top 1 IdEstado, IdFarmacia, Login, Password, Url_Servicio, Status " + 
                "From INT_IFarmatel_Conexion (NoLock) " + 
                "Where IdEstado = '{0}' and IdFarmacia = '{1}' and Status = 'A' ", 
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (!leer.Exec(sSql))
            {
                bConexionConfigurada = false;
                sLogin = "";
                sPassword = "";

                Error.GrabarError(leer, "GetConfiguracion()");
                General.msjError("Ocurrió un error al obtener los datos de conexión.");
            }
            else
            {
                if (!leer.Leer())
                {
                    bConexionConfigurada = false;
                    sLogin = "";
                    sPassword = "";
                }
                else
                {
                    sLogin = leer.Campo("Login");
                    sPassword = leer.Campo("Password");
                    sURL = leer.Campo("Url_Servicio");
                    bConexionConfigurada = true;
                }
            }

            return bRegresa; 
        }

        public bool GetPedidosParaEnvio()
        {
            bool bRegresa = false; 
            string sSql = string.Format("Exec spp_INT_IFarmatel___GetServiciosADomicilio " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            leerPedidos = new clsLeer(); 
            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GetPedidosParaEnvio()");
                General.msjError("Ocurrió un error al obtener la lista de pedidos de servicios.");
            }
            else 
            {
                bRegresa = true;
                leer.RenombrarTabla(1, "Encabezado");
                leer.RenombrarTabla(2, "Pedidos");
                leer.RenombrarTabla(3, "PedidosDetalles"); 
                leerPedidos.DataSetClase = leer.DataSetClase; 

            }

            return bRegresa; 
        }

        public bool EnviarPedidos()
        {
            bool bRegresa = false;
            clsLeer leerRenglon = new clsLeer();

            leerPedidos.RegistroActual = 1; 
            while (leerPedidos.Leer())
            {
                leerRenglon.DataRowClase = leerPedidos.RowActivo;
                bRegresa = EnviarPedido(leerRenglon);

                ////if (!bRegresa)
                ////{
                ////    break; 
                ////}
            }

            return bRegresa; 
        }

        public bool EnviarPedido(string Folio)
        {
            bool bRegresa = false;
            DataSet dtsPedidos = new DataSet(); 
            DataSet dtsFolio = new DataSet();
            DataTable dtFolio = new DataTable();
            DataTable dtDetalles = new DataTable(); 
            
            clsLeer leerFolio = new clsLeer();
            sMensajeError = ""; 

            try 
            { 
                dtsPedidos = leerPedidos.DataSetClase;

                leerFolio.DataRowsClase = dtsPedidos.Tables[1].Select(string.Format(" FolioServicioDomicilio = '{0}' ", Fg.PonCeros(Folio, 8)));
                leerFolio.RenombrarTabla(1, "Pedido");
                dtFolio = leerFolio.DataTableClase;

                leerFolio.DataRowsClase = dtsPedidos.Tables[2].Select(string.Format(" FolioServicioDomicilio = '{0}' ", Fg.PonCeros(Folio, 8)));
                leerFolio.RenombrarTabla(1, "Detalles");
                dtDetalles = leerFolio.DataTableClase;


                dtsFolio.Tables.Add(dtFolio.Copy());
                dtsFolio.Tables.Add(dtDetalles.Copy());
                leerFolio.DataSetClase = dtsFolio; 

                bRegresa = EnviarPedido(leerFolio); 
            }
            catch(Exception ex)
            {
                sMensajeError = string.Format("Error 01 : EnviarPedido({0})", Folio);
                Error.GrabarError(ex, string.Format("EnviarPedido({0})", Folio)); 
            }


            return bRegresa; 
        }

        public bool EnviarPedido(clsLeer Pedido)
        {
            bool bRegresa = false;
            ResponseSolicitud respuesta = new ResponseSolicitud();
            SolicitudDeServicioInformacion informacionSolicitud = new SolicitudDeServicioInformacion();
            List<ItemInsumo> insumos = new List<ItemInsumo>();
            ItemInsumo insumoItem = new ItemInsumo();
            int iRegistro = 0;

            ////RequestPedido request = new RequestPedido();
            ////ResponsePedido response = new ResponsePedido();
            ////Producto[] listaProductos = new Producto[100];
            ////Producto producto = new Producto();


            clsLeer detalles = new clsLeer();

            try
            {
                Pedido.RegistroActual = 1;
                if (Pedido.Leer())
                {
                    detalles.DataTableClase = Pedido.Tabla(2);
                    ////listaProductos = new Producto[detalles.Registros];


                    informacionSolicitud.Cliente = loginFarmatel.Cliente;
                    informacionSolicitud.Sucursal = loginFarmatel.Sucursal; 
                    informacionSolicitud.ClaveSucursalByCliente = loginFarmatel.Identificador;
                    informacionSolicitud.FolioReferencia_Solicitud = Pedido.Campo("UUID");
                    informacionSolicitud.NumeroDeReferencia = Pedido.Campo("NumeroReceta");                    
                    informacionSolicitud.FechaSolicitud = Pedido.CampoFecha("FechaReceta");

                    ////            request.folioReceta = Pedido.Campo("NumeroReceta");
                    ////            request.fechaReceta = Pedido.Campo("FechaReceta"); 

                    informacionSolicitud.Beneficiario = new ItemBeneficiario();
                    informacionSolicitud.Beneficiario.Nombre = Pedido.Campo("Nombre");
                    informacionSolicitud.Beneficiario.ApPaterno = Pedido.Campo("ApPaterno");
                    informacionSolicitud.Beneficiario.ApMaterno = Pedido.Campo("ApMaterno");
                    informacionSolicitud.Beneficiario.Referencia = Pedido.Campo("ReferenciaAfiliacion");
                    informacionSolicitud.Beneficiario.Direccion = Pedido.Campo("Domicilio");
                    informacionSolicitud.Beneficiario.Telefono = Pedido.Campo("Telefono");


                    informacionSolicitud.Medico = new ItemMedico();
                    informacionSolicitud.Medico.Nombre = Pedido.Campo("NombreMedico");
                    informacionSolicitud.Medico.ApPaterno = Pedido.Campo("ApPaternoMedico");
                    informacionSolicitud.Medico.ApMaterno = Pedido.Campo("ApMaternoMedico");
                    informacionSolicitud.Medico.Referencia = Pedido.Campo("NumCedula");


                    detalles.RegistroActual = 1;
                    while (detalles.Leer())
                    {
                        insumoItem = new ItemInsumo();
                        insumoItem.ClaveInsumo = detalles.Campo("ClaveSSA");
                        insumoItem.Piezas = detalles.CampoInt("CantidadRequerida");
                        insumoItem.Descripcion = Fg.Left(detalles.Campo("DescripcionClaveSSA"), 1000) ;
                        insumos.Add(insumoItem);
                        iRegistro++;
                    }


                    informacionSolicitud.Insumos = new ItemInsumo[insumos.Count];
                    informacionSolicitud.Insumos = insumos.ToArray();


                    respuesta = iFarmatel_cnn.RecibirSolicitud(informacionSolicitud); 


                    ////response = iFarmatel_cnn.EnviaPedido(request);
                    ////bRegresa = !response.HuboError;

                    if (respuesta.Error)
                    {
                        bRegresa = false;
                        sMensajeError = respuesta.Mensaje;
                    }
                    else
                    {
                        bRegresa = true;
                        MarcarPedidoEnviado(Pedido.Campo("FolioServicioDomicilio"));
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

        ////private bool EnviarPedido_OLD(clsLeer Pedido)
        ////{
        ////    bool bRegresa = false;
        ////    RequestPedido request = new RequestPedido();
        ////    ResponsePedido response = new ResponsePedido();
        ////    Producto[] listaProductos = new Producto[100]; 
        ////    Producto producto = new Producto();
        ////    int iRegistro = 0;

        ////    clsLeer detalles = new clsLeer(); 

        ////    try
        ////    {
        ////        Pedido.RegistroActual = 1; 
        ////        if (Pedido.Leer())
        ////        {
        ////            detalles.DataTableClase = Pedido.Tabla(2);
        ////            listaProductos = new Producto[detalles.Registros]; 

        ////            request.token = loginFarmatel.Token;
        ////            request.idEstado = Pedido.Campo("IdEstado");
        ////            request.idFarmacia = Pedido.Campo("IdFarmacia");
        ////            request.idCliente = Pedido.Campo("IdCliente");
        ////            request.idSubCliente = Pedido.Campo("IdSubCliente"); 

        ////            request.folioServicio = Pedido.Campo("UUID_FolioServicioDomicilio");
        ////            request.numConsecutivo = Pedido.Campo("UUID");
        ////            request.idBeneficiario = Pedido.Campo("IdBeneficiario"); 
        ////            request.numPlaca = Pedido.Campo("ReferenciaAfiliacion");
        ////            request.folioReceta = Pedido.Campo("NumeroReceta");
        ////            request.fechaReceta = Pedido.Campo("FechaReceta"); 
        ////            request.cedula = Pedido.Campo("NumCedula"); 
        ////            request.tipoServicio = 0;  //// valor temporal 

        ////            request.persona = new Persona(); 
        ////            request.persona.Nombre = Pedido.Campo("Nombre");
        ////            request.persona.ApePaterno = Pedido.Campo("ApPaterno");
        ////            request.persona.ApeMaterno = Pedido.Campo("ApMaterno");
        ////            request.persona.TelefonoCasa = Pedido.Campo("TelefonoCasa");
        ////            request.persona.TelefonoMovil = Pedido.Campo("TelefonoMovil"); 
        ////            request.persona.CorreoElectronico = Pedido.Campo("CorreoElectronico");
        ////            request.persona.FechaNacimiento = Pedido.Campo("FechaNacimiento");
        ////            request.persona.Genero = Pedido.Campo("Sexo");

        ////            request.direccion = new Direccion(); 
        ////            request.direccion.CodigoPostal = Pedido.Campo("CodigoPostal");
        ////            request.direccion.Calle = Pedido.Campo("Calle");
        ////            request.direccion.NumeroExterior = Pedido.Campo("NumeroExterior");
        ////            request.direccion.NumeroInterior = Pedido.Campo("NumeroInterior");
        ////            request.direccion.EntreCalles = Pedido.Campo("EntreCalles");
        ////            request.direccion.Colonia = Pedido.Campo("Colonia");
        ////            request.direccion.Referencia = Pedido.Campo("Referencia");
        ////            request.direccion.Estado = Pedido.Campo("Estado");
        ////            request.direccion.DelegacioMunicipio = Pedido.Campo("DelegacionMunicipio");

        ////            detalles.RegistroActual = 1; 
        ////            while (detalles.Leer()) 
        ////            {
        ////                producto = new Producto(); 
        ////                producto.IdProducto = detalles.Campo("IdProducto"); 
        ////                producto.SKU = detalles.Campo("Sku"); 
        ////                producto.Clave = detalles.Campo("ClaveSSA"); 
        ////                producto.Descripcion = detalles.Campo("DescripcionClaveSSA"); 
        ////                producto.Piezas = detalles.CampoInt("CantidadRequerida");
        ////                producto.Faltante = "1";
        ////                producto.PiezasFaltantes = 0; 

        ////                listaProductos[iRegistro] = producto;
        ////                iRegistro++;
        ////            } 


        ////            request.productos = listaProductos;

        ////            response = iFarmatel_cnn.EnviaPedido(request);
        ////            bRegresa = !response.HuboError;

        ////            if (response.HuboError)
        ////            {
        ////                bRegresa = false; 
        ////                sMensajeError = response.MensajeError; 
        ////            }
        ////            else
        ////            { 
        ////                MarcarPedidoEnviado(Pedido.Campo("FolioServicioDomicilio"));
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Pedido.RegistroActual = 1;
        ////        Pedido.Leer();
        ////        sMensajeError = string.Format("Error 02 : EnviarPedido_C({0})", Pedido.Campo("UUID"));
        ////        Error.GrabarError(ex, string.Format("EnviarPedido_C({0})", Pedido.Campo("UUID"))); 
        ////    } 

        ////    return bRegresa; 
        ////}
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Private
        private void InicializarConexiones()
        {
            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            leerPedidos = new clsLeer(ref cnn); 
            Error = new clsGrabarError(General.DatosConexion, GnDll_SII_IFarmatel.DatosApp, "IFarmatel"); 
        }

        private bool MarcarPedidoEnviado(string Folio)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_IFarmatel___SetServiciosADomicilio_Enviado " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioServicioDomicilio = '{3}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Folio);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "MarcarPedidoEnviado"); 
            }

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Private 
    }
}
