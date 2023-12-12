using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using Dll_IMach4.Protocolos; 

namespace Dll_IMach4.Interface
{
    public class PuntoDeVenta 
    {
        #region Declaracion de Variables 
        string Name = "Dll_IMach4.Interface.PuntoDeVenta";
        clsConexionSQL cnn; 
        clsLeer leer;
        clsGrabarError Error;

        bool bPtoVtaOperable = false; 
        bool bEsClienteIMach4 = IMach4.EsClienteIMach4;
        string sFolioSolicitud = "";
        int iRequerimiento = 0;
        bool bRequisicionRegistrada = false;

        // string sTipoExpedicion = ""; 
        #endregion Declaracion de Variables 

        #region Constructores y Descructor de Clase 
        public PuntoDeVenta()
        {
            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, IMach4.DatosApp, Name);

            bEsClienteIMach4 = IMach4.EsClienteIMach4; 
        }

        ~PuntoDeVenta()
        {
        }
        #endregion Constructores y Descructor de Clase

        #region Propiedades Publicas 
        public string FolioSolicitud
        {
            get { return sFolioSolicitud; }
        }

        public bool PtoVtaOperable
        {
            get { return bPtoVtaOperable;  }
        }

        public bool RequisicionRegistrada
        {
            get { return bRequisicionRegistrada; } 
        } 
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos 
        public string ObtenerFolioSolicitud()
        {
            return ObtenerFolioSolicitud("*", ""); 
        }

        public bool TerminarSolicitud(string FolioVenta)
        {
            ObtenerFolioSolicitud(sFolioSolicitud, FolioVenta);
            return bPtoVtaOperable;
        }

        public bool TerminarSolicitud(string Folio, string FolioVenta)
        {
            ObtenerFolioSolicitud(Folio, FolioVenta);
            return bPtoVtaOperable;
        }

        public bool Show(string IdProducto, string CodigoEAN)
        {
            bool bRegresa = false;

            if (IMach4.EsClienteIMach4)
            {
                bRequisicionRegistrada = false; 
                FrmDetalleCodigoEAN Detalles = new FrmDetalleCodigoEAN();
                Detalles.MostrarPantalla(sFolioSolicitud, CodigoEAN);

                if (Detalles.RequisicionDeProducto)
                {
                    iRequerimiento = Detalles.CantidadSolicitada;
                    bRegresa= RegistrarSolicitud(IdProducto, CodigoEAN, Detalles.EsProducto_Multipicking); 
                }

                Detalles.Close();
                Detalles = null;
            }

            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private void PrepararConexiones(ref clsLeer Exec)
        {
            //leer = Exec;
            //Error = new clsGrabarError(Exec.DatosConexion, IMach4.DatosApp, Name);
        } 
        
        private string ObtenerFolioSolicitud(string Folio, string FolioVenta)
        {
            string sSql = string.Format("Exec spp_Mtto_IMach_Solicitudes '{0}', '{1}', '{2}', '{3}', '{4}'",
               Folio, IMach4.EmpresaConectada, IMach4.EstadoConectado, IMach4.FarmaciaConectada, FolioVenta);

            sFolioSolicitud = "";
            bPtoVtaOperable = false;

            if (IMach4.EsClienteIMach4)
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "ObtenerFolioSolicitud");
                    General.msjError("Ocurrió un error al Obtener el Folio de Solicitud de Mach4.");
                }
                else
                {
                    leer.Leer();
                    sFolioSolicitud = leer.Campo("FolioSolicitud");
                    bPtoVtaOperable = true;
                }
            }

            return sFolioSolicitud;
        }

        private bool RegistrarSolicitud_Normal(string IdProducto, string CodigoEAN)
        {
            bool bRegresa = true;
            string sSql = "";

            for (int i = 1; i <= iRequerimiento; i++)
            {
                sSql = string.Format(" Exec spp_Mtto_IMach_SolicitudesProductos " + 
                    " @IdCliente = '{0}', @FolioSolicitud = '{1}', @Consecutivo = '{2}', @IdTerminal = '{3}', @PuertoDeSalida = '{4}', " + 
                    " @IdProducto = '{5}', @CodigoEAN = '{6}', @CantidadSolicitada = '{7}', @StatusIMach = '{8}' ",
                    IMach4.IdCliente, sFolioSolicitud, 0, IMach4.Terminal, IMach4.PuertoDeDispensacion, IdProducto, CodigoEAN, 
                    1, (int)IMach4_StatusRespuesta_A.Registred);

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leer, "RegistrarSolicitud");
                    break;
                }
            }

            return bRegresa; 
        }

        private bool RegistrarSolicitud_Multipicking(string IdProducto, string CodigoEAN)
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_Mtto_IMach_SolicitudesProductos " +
                " @IdCliente = '{0}', @FolioSolicitud = '{1}', @Consecutivo = '{2}', @IdTerminal = '{3}', @PuertoDeSalida = '{4}', " +
                " @IdProducto = '{5}', @CodigoEAN = '{6}', @CantidadSolicitada = '{7}', @StatusIMach = '{8}' ",
                IMach4.IdCliente, sFolioSolicitud, 0, IMach4.Terminal, IMach4.PuertoDeDispensacion, IdProducto, CodigoEAN, 
                iRequerimiento, (int)IMach4_StatusRespuesta_A.Registred);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "RegistrarSolicitud_Multipicking");
            }

            return bRegresa;
        }

        private bool RegistrarSolicitud(string IdProducto, string CodigoEAN, bool EsProductoMultipicking)
        {
            bool bRegresa = false;
            string sSql = ""; 

            if ( cnn.Abrir() )
            {
                cnn.IniciarTransaccion();
                bRegresa = true;

                if (IMach4.EsMultipicking)
                {
                    if (EsProductoMultipicking)
                    {
                        bRegresa = RegistrarSolicitud_Multipicking(IdProducto, CodigoEAN);
                    }
                    else
                    {
                        bRegresa = RegistrarSolicitud_Normal(IdProducto, CodigoEAN); 
                    }

                    ////sSql = string.Format(" Exec spp_Mtto_IMach_SolicitudesProductos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
                    ////    IMach4.IdCliente, sFolioSolicitud, 0, IMach4.Terminal, IMach4.PuertoDeDispensacion, IdProducto, CodigoEAN, iRequerimiento, (int)IMach4_StatusRespuesta_A.Registred);

                    ////if (!leer.Exec(sSql))
                    ////{
                    ////    bRegresa = false;
                    ////    Error.GrabarError(leer, "RegistrarSolicitud_Multipicking");
                    ////}
                }
                else
                {
                    bRegresa = RegistrarSolicitud_Normal(IdProducto, CodigoEAN); 

                    ////for (int i = 1; i <= iRequerimiento; i++)
                    ////{
                    ////    sSql = string.Format(" Exec spp_Mtto_IMach_SolicitudesProductos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
                    ////        IMach4.IdCliente, sFolioSolicitud, 0, IMach4.Terminal, IMach4.PuertoDeDispensacion, IdProducto, CodigoEAN, 1, (int)IMach4_StatusRespuesta_A.Registred);

                    ////    if (!leer.Exec(sSql))
                    ////    {
                    ////        bRegresa = false;
                    ////        Error.GrabarError(leer, "RegistrarSolicitud");
                    ////        break;
                    ////    }
                    ////}
                }


                if ( bRegresa ) 
                {
                    cnn.CompletarTransaccion(); 
                }
                else 
                {
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al Registrar la solicitud de productos de Mach4."); 
                }
                cnn.Cerrar(); 
            }

            bRequisicionRegistrada = bRegresa; 
            return bRegresa; 
        }

        #endregion Funciones y Procedimientos Privados 

    }
}
