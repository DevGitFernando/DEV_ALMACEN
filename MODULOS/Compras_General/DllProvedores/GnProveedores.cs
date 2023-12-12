using System;
using System.Collections;
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
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllProveedores.Consultas; 
using DllProveedores.Usuarios_y_Permisos; 

namespace DllProveedores
{
    public static class GnProveedores
    {

        #region Declaracion de variables
        private static string sModulo = "GnProveedores";
        private static string sVersion = "";

        private static clsDatosApp dpDatosApp = new clsDatosApp("GnProveedores", "");
        private static clsParametrosProveedores pParametros;
        private static string sRutaReportes = "";
        private static DateTime dtpFechaOperacionSistema = DateTime.Now;
        private static DateTime dtpFechaServidor = DateTime.Now;

        private static DllProveedores.Usuarios_y_Permisos.clsIniManager myXmlEdoInformacion;
        private static clsDatosCliente datosCliente;

        private static string sIdProveedor = "";
        private static string sNombreProveedor = "";
        private static string sUserProveedor = "";
        private static string sPassProveedor = ""; 


        public static string[] ListaFormas = { "FrmMain", "FrmNavegador" };
        #endregion Declaracion de variables

        static GnProveedores()
        {
            clsAbrirForma.AssemblyActual("Proveedores");
            dpDatosApp = clsAbrirForma.DatosApp;
            datosCliente = new clsDatosCliente(dpDatosApp, "", ""); 
        }

        #region Propieades Dll
        public static clsDatosApp DatosApp
        {
            get { return dpDatosApp; }
            set { dpDatosApp = value; }
        }
        public static string Modulo
        {
            get { return dpDatosApp.Modulo; }
        }
        public static string Version
        {
            get { return dpDatosApp.Version; }
        }
        #endregion Propieades Dll

        #region Propiedades 
        public static clsDatosCliente DatosDelCliente
        {
            get { return datosCliente; }
        }

        public static string RutaReportes
        {
            get
            {
                if (sRutaReportes == "")
                {
                    sRutaReportes = pParametros.GetValor("RutaReportes");
                }
                return sRutaReportes;
            }
            set { sRutaReportes = value; }
        }

        public static DateTime FechaOperacionSistema
        {
            get
            {
                DateTime dt = General.FechaSistema;
                try
                {
                    dt = Convert.ToDateTime(pParametros.GetValor("FechaOperacionSistema"));
                }
                catch
                {
                }
                return dt;
            }
        }

        public static DateTime FechaServidor
        {
            get 
            { 
                return dtpFechaServidor; 
            }
        }

        public static int DiasPlazoMaximoEntregaPedido
        {
            get
            {
                // return Convert.ToInt32(pParametros.GetValor("DiasPlazoMaximoEntregaPedido")); 
                return 20; 
            }
        }

        public static clsParametrosProveedores Parametros
        {
            get { return pParametros; }
            set { pParametros = value; }
        } 

        public static DllProveedores.Usuarios_y_Permisos.clsIniManager XmlConfig
        {
            get
            {
                if (myXmlEdoInformacion == null)
                    myXmlEdoInformacion = new DllProveedores.Usuarios_y_Permisos.clsIniManager();

                return myXmlEdoInformacion;
            }
            set { myXmlEdoInformacion = value; }
        }
        #endregion Propiedades

        #region Datos de Proveedor 
        public static string IdProveedor
        {
            get { return sIdProveedor; }
            set { sIdProveedor = value;  }
        }

        public static string NombreProveedor
        {
            get { return sNombreProveedor; }
            set { sNombreProveedor = value; }
        }

        public static string Usuario 
        {
            get { return sUserProveedor; }
            set { sUserProveedor = value; }
        }

        public static string Password 
        {
            get { return sPassProveedor; }
            set { sPassProveedor = value; }
        }

        public static void MostrarCambiarPasswordUsuario()
        {
            FrmCambiarPassword pass = new FrmCambiarPassword();
            pass.ShowDialog();
        }

        #endregion Datos de Proveedor

        #region CATALOGOS
        private static clsProductosCodigosEAN codigosEAN;
        private static clsClavesSSA clavesSSA;

        public static DateTime ObtenerFechaServidor()
        {
            DateTime dtReturn = DateTime.Now; 
            clsLeerWeb myLeer = new clsLeerWeb(General.Url, GnProveedores.datosCliente);
            string sSql = " Select getdate() as FechaServidor ";

            if (myLeer.Exec(sSql))
            {
                myLeer.Leer();
                dtpFechaServidor = myLeer.CampoFecha("FechaServidor");
                dtReturn = dtpFechaServidor; 
            }

            return dtReturn; 
        }

        /// <summary>
        /// Carga los Catalogos de Claves y Codigos EAN que se utilizaran en el módulo. 
        /// </summary>
        public static void IniciarCatalogos()
        {
            // Pruebas de velocidad de respuesta 
            datosCliente = new clsDatosCliente(dpDatosApp, "clsClavesSSA", "Cargar"); 
            clavesSSA = new clsClavesSSA(General.Url, datosCliente);
            // clavesSSA.Cargar();

            datosCliente = new clsDatosCliente(dpDatosApp, "clsProductosCodigosEAN", "Cargar"); 
            codigosEAN = new clsProductosCodigosEAN(General.Url, datosCliente); 
            // codigosEAN.Cargar(); 
        }

        /// <summary>
        /// Administra el Catálogo de Claves que se maneja 
        /// </summary>
        public static clsClavesSSA Claves
        {
            get { return clavesSSA; }
        }

        /// <summary>
        /// Administra el Catálogo de Códigos EAN que se maneja 
        /// </summary>
        public static clsProductosCodigosEAN CodigosEAN
        {
            get { return codigosEAN; }
        } 
        #endregion CATALOGOS 

    }
}
