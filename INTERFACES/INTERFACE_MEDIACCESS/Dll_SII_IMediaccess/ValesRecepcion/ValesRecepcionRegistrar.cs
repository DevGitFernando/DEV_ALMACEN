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

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using DllFarmaciaSoft;

using Dll_SII_IMediaccess;

namespace Dll_SII_IMediaccess.ValesRecepcion
{
    public class ValesRecepcionRegistrar
    {
        Dll_SII_IMediaccess.wsEPharmaInformacionDeVales.wsEPharma_RecepcionVales wsRecepcion_cnn = new Dll_SII_IMediaccess.wsEPharmaInformacionDeVales.wsEPharma_RecepcionVales();
        ValesRecepcionRegistrar wsRecepcion_cnn_Local;
        ResponseLogin login = new ResponseLogin();
        clsConexionSQL cnn;
        clsLeer leer;
        clsLeer LeerInformacion;
        clsGrabarError Error;
        clsDatosConexion datosConexion;
        ValesRecepcionRegistrarInformacion info;
        clsLeer leerDestinos;

        basGenerales Fg = new basGenerales();

        string sIdEmpresa = "";
        string sFolioSolicitud = "";
        string sIdCliente = "";
        string sIdSucursal = "";
        string sMensajeError = "";


        public ValesRecepcionRegistrar(clsDatosConexion DatosDeConexion)
        {
            datosConexion = DatosDeConexion;
            cnn = new clsConexionSQL(datosConexion);
            leer = new clsLeer(ref cnn);
            leerDestinos = new clsLeer(ref cnn);
            LeerInformacion = new clsLeer(ref cnn);
            Error = new clsGrabarError(datosConexion, GnDll_SII_IMediaccess.DatosApp, "ValesRepcionRegistrar");
        }

        public ResponseSolicitud RegistrarSolicitud(ValesRecepcionRegistrarInformacion Informacion)
        {
            ResponseSolicitud respuesta = new ResponseSolicitud();
            info = Informacion;

            if (!cnn.Abrir())
            {
                respuesta.Estatus = 1;
                respuesta.Mensaje = "Error de conexión con el servidor de datos.";
                respuesta.Error = true;
            }
            else
            {
                cnn.IniciarTransaccion();

                if (Guardar_001_Solicitud(respuesta))
                {
                    respuesta.Mensaje = "Información integrada correctamente.";
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

        private bool Guardar_001_Solicitud(ResponseSolicitud Respuesta)
        {
            bool bRegresa = false;
            string sSql = "";
            string sDatos;


            //clsConexionSQL dd = new clsConexionSQL();
            //dd.DatosConexion.CadenaConexion = cnn.DatosConexion.CadenaConexion;
            //dd.DatosConexion.BaseDeDatos = "SII_MEDIACCESS_Produccion_2k20161117";
            //dd.DatosConexion.Servidor = cnn.DatosConexion.Servidor;
            //dd.DatosConexion.Usuario = cnn.DatosConexion.Usuario;
            //dd.DatosConexion.Password = cnn.DatosConexion.Password;
            //dd = dd;

            //leer = new clsLeer(ref dd);

            sSql = string.Format("Set DateFormat YMD Exec spp_INT_IME__Vales_001_Encabezado " +
                   " @IdSocioComercial = '{0}', @IdSucursal = '{1}', @Folio_Vale = '{2}', @FechaEmision_Vale = '{3}', @EsValeManual = '{4}', " +
                   " @IdEmpresa = '{5}', @IdEstado = '{6}', @IdFarmacia = '{7}', @IdPersonal = '{8}', " +
                   " @Beneficiario_Nombre = '{9}', @Beneficiario_ApPaterno = '{10}', @Beneficiario_ApMaterno = '{11}', @Beneficiario_Sexo = '{12}', @Beneficiario_FechaNacimiento = '{13}', " +
                   " @Beneficiario_FolioReferencia = '{14}', @Beneficiario_FechaFinVigencia = '{15}', @IdTipoDeDispensacion = '{16}', @NumReceta = '{17}', " +
                   " @FechaReceta = '{18}', @Medico_Nombre = '{19}', @Medico_ApPaterno = '{20}', @Medico_ApMaterno = '{21}', @Medico_NumCedula = '{22}', " +
                   " @IdBeneficio = '{23}', @IdDiagnostico = '{24}', @RefObservaciones = '{25}', @IdEmpresa_IME = '{26}', @IdEstado_IME = '{27}', @IdFarmacia_IME = '{28}'",
                info.IdSocioComercial, info.IdSucursal, info.Folio_Vale, Fg.FechaYMD(info.FechaEmision_Vale.ToShortDateString(), "-"), 0, "", "", "", "",
                info.Beneficiario.Nombre, info.Beneficiario.ApPaterno, info.Beneficiario.ApMaterno, info.Beneficiario.Sexo, Fg.FechaYMD(info.Beneficiario.FechaNacimiento.ToShortDateString(), "-"),
                info.Beneficiario.Referencia, Fg.FechaYMD(info.Beneficiario.FechaFinVigencia.ToShortDateString(), "-"), info.IdTipoDeDispensacion, info.NumReceta,
                Fg.FechaYMD(info.FechaReceta.ToShortDateString(), "-"), info.Medico.Nombre, info.Medico.ApPaterno, info.Medico.ApMaterno, info.Medico.Referencia, info.IdBeneficio,
                info.IdDiagnostico, info.RefObservaciones, info.IdEmpresa, info.IdEstado, info.IdFarmacia);

            if (!leer.Exec(sSql))
            {
                Respuesta.Error = true;
                Respuesta.Estatus = 201;
                Respuesta.Mensaje = "Error al registrar la información";
            }
            else
            {
                if (!leer.Leer())
                {
                    Respuesta.Error = true;
                    Respuesta.Estatus = 3;
                    Respuesta.Mensaje = "Folio interno no asignado";
                }
                else
                {
                    sIdEmpresa = leer.Campo("Empresa");
                    sFolioSolicitud = leer.Campo("FolioSolicitud");
                    sIdCliente = leer.Campo("Cliente");
                    info.IdSocioComercial = leer.Campo("IdSocioComercial");
                    info.IdSucursal = leer.Campo("IdSucursal");


                    bRegresa = Guardar_002_Solicitud_Insumos(Respuesta);
                }
            }

            return bRegresa;
        }

        private bool Guardar_002_Solicitud_Insumos(ResponseSolicitud Respuesta)
        {
            bool bRegresa = true;
            string sSql = "";
            int iPartida = 0;


            foreach (ItemInsumo item in info.Insumos)
            {
                iPartida++;
                sSql = string.Format("Exec spp_INT_IME__Vales__002_ClavesSSA " +
                                " @IdSocioComercial = '{0}', @IdSucursal = '{1}', @Folio_Vale = '{2}', @Partida = '{3}', " +
                                " @ClaveSSA = '{4}', @CantidadSolicitada = '{5}', @CantidadSurtida = '{6}' ",
                                info.IdSocioComercial, info.IdSucursal, info.Folio_Vale,
                                iPartida, item.ClaveInsumo, item.Piezas, 0);

                if (!leer.Exec(sSql))
                {
                    Respuesta.Error = true;
                    Respuesta.Estatus = 202;
                    Respuesta.Mensaje = "Error al registrar la información";
                    bRegresa = false;
                    break;

                    ////Respuesta.Estatus = 201;
                    ////Respuesta.Mensaje = "Error al registrar la información";
                }
            }

            return bRegresa;
        }
    }
}
