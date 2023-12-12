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
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;
using DllPedidosClientes;

namespace AdminRegional
{
    class GnAdminRegional
    {
        //private static string sModulo = "OficinaCentral";
        //private static string sVersion = ""; // Application.ProductVersion;

        private static clsDatosApp dpDatosApp = new clsDatosApp("Administracion Regional", "");
        private static clsParametrosClienteRegional pParametros;
        private static string sRutaReportes = "";
        private static string sEncPrincipal = "";
        private static string sEncSecundario = "";

        private static bool bInformacionCTE_Cargada = false;
        private static string sIdCliente = "";
        private static string sClienteNombre = "";
        private static string sIdSubCliente = "";
        private static string sSubClienteNombre = "";

        private static TipoDeConexion tpTipoConexion = TipoDeConexion.Ninguno;

        static GnAdminRegional()
        {
            clsAbrirForma.AssemblyActual("Administracion Regional");
            dpDatosApp = clsAbrirForma.DatosApp;
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
        public static string RutaReportes
        {
            get
            {
                if (sRutaReportes == "")
                {
                    try
                    {
                        sRutaReportes = pParametros.GetValor("RutaReportes");
                        sEncPrincipal = pParametros.GetValor("EncReportesPrin");
                        sEncSecundario = pParametros.GetValor("EncReportesSec");
                    }
                    catch { }
                }
                return sRutaReportes;
            }
            set { sRutaReportes = value; }
        }

        public static string EncabezadoPrincipal
        {
            get { return sEncPrincipal; }
            set { sEncPrincipal = value; }
        }

        public static string EncabezadoSecundario
        {
            get { return sEncSecundario; }
            set { sEncSecundario = value; }
        }

        public static TipoDeConexion TipoDeConexion
        {
            get
            {
                //if (tpTipoConexion == TipoDeConexion.Ninguno) 
                {
                    try
                    {
                        tpTipoConexion = (TipoDeConexion)pParametros.GetValorInt("TipoConexion");
                    }
                    catch { }
                }
                return tpTipoConexion;
            }
            set
            {
                if (tpTipoConexion != TipoDeConexion.Ninguno)
                {
                    tpTipoConexion = value;
                }
            }
        }

        public static clsParametrosClienteRegional Parametros
        {
            get { return pParametros; }
            set { pParametros = value; }
        }

        #region Informacion Cliente - SubCliente
        private static void ObtenerNombreCliente_SubCliente()
        {
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            basGenerales Fg = new basGenerales();

            try
            {
                sIdCliente = pParametros.GetValor("IdCliente");
                sIdSubCliente = pParametros.GetValor("IdSubCliente");
            }
            catch { }

            string sSql = string.Format("Select IdCliente, NombreCliente, IdSubCliente, NombreSubCliente " +
                "From vw_Clientes_SubClientes (NoLock) " +
                "Where IdCliente = '{0}' and IdSubCliente = '{1}' ",
                Fg.PonCeros(sIdCliente, 4), Fg.PonCeros(sIdSubCliente, 4));

            if (!leer.Exec(sSql))
            {
            }
            else
            {
                bInformacionCTE_Cargada = false;
                if (leer.Leer())
                {
                    sIdCliente = leer.Campo("IdCliente");
                    sClienteNombre = leer.Campo("NombreCliente");
                    sIdSubCliente = leer.Campo("IdSubCliente");
                    sSubClienteNombre = leer.Campo("NombreSubCliente");

                    DtGeneralPedidos.Cliente = sIdCliente;
                    DtGeneralPedidos.ClienteNombre = sClienteNombre;
                    DtGeneralPedidos.SubCliente = sIdSubCliente;
                    DtGeneralPedidos.SubClienteNombre = sSubClienteNombre;
                }
            }
        }

        /* 
        private static string sIdCliente = "";
        private static string sClienteNombre = "";
        private static string sIDSubCliente = "";
        private static string sSubClienteNombre = "";
        */

        public static string Cliente
        {
            get
            {
                if (!bInformacionCTE_Cargada)
                {
                    ObtenerNombreCliente_SubCliente();
                }
                return sIdCliente;
            }
            set
            {
                sIdCliente = value;
                DtGeneralPedidos.Cliente = value;
            }
        }

        public static string ClienteNombre
        {
            get
            {
                if (!bInformacionCTE_Cargada)
                {
                    ObtenerNombreCliente_SubCliente();
                }
                return sClienteNombre;
            }

            set
            {
                sClienteNombre = value;
                DtGeneralPedidos.ClienteNombre = value;
            }
        }

        public static string SubCliente
        {
            get
            {
                if (!bInformacionCTE_Cargada)
                {
                    ObtenerNombreCliente_SubCliente();
                }
                return sIdSubCliente;
            }

            set
            {
                sIdSubCliente = value;
                DtGeneralPedidos.SubCliente = value;
            }
        }

        public static string SubClienteNombre
        {
            get
            {
                if (!bInformacionCTE_Cargada)
                {
                    ObtenerNombreCliente_SubCliente();
                }
                return sSubClienteNombre;
            }

            set
            {
                sSubClienteNombre = value;
                DtGeneralPedidos.SubClienteNombre = value;
            }
        }
        #endregion Informacion Cliente - SubCliente

        #endregion Propiedades
    }
}
