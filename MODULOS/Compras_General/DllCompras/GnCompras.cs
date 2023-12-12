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

using DllFarmaciaSoft;

namespace DllCompras
{
    public static class GnCompras
    {

        #region Declaracion de variables
        //private static string sModulo = "Compras";
        //private static string sVersion = "";

        private static clsDatosApp dpDatosApp = new clsDatosApp("Compras", "");
        private static clsParametrosOficinaCentral pParametros;
        private static clsParametrosCompras pParametrosCompras;
        private static string sRutaReportes = "";
        private static bool bLectorDeHuellas = false;
        private static bool bConfirmacionConHuellas = false;

        private static int iNumCompras = 0;
        private static int iPorcMaxCompras = 10;
        private static DateTime dtpFechaOperacionSistema = DateTime.Now;

        public static string[] ListaFormas = { "FrmMain", "FrmNavegador" }; 
        #endregion Declaracion de variables

        static GnCompras()
        {
            clsAbrirForma.AssemblyActual("Compras");
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
                    sRutaReportes = pParametros.GetValor("RutaReportes");
                }
                return sRutaReportes;
            }
            set { sRutaReportes = value; }
        }

        public static int NumCompras 
        {
            get
            {
                if (iNumCompras == 0)
                {
                    int sValor = pParametrosCompras.GetValorInt("NumeroDeCompras");
                    if (sValor != 0)
                        iNumCompras = sValor;
                }
                return iNumCompras;
            }
        }

        public static int PorcMaxCompras
        {
            get
            {
                if (iPorcMaxCompras == 0)
                {
                    int sValor = pParametrosCompras.GetValorInt("PorcMaxCompras");
                    if (sValor != 0)
                    {
                        iPorcMaxCompras = sValor;
                    }

                    if (iPorcMaxCompras < 0 || iPorcMaxCompras > 100)
                    {
                        iPorcMaxCompras = 10;
                    }
                }
                return iPorcMaxCompras;
            }
        }

        /// <summary>
        /// Determina si se solicita la firma biométrica.
        /// </summary>
        public static bool ConfirmacionConHuellas
        {
            get
            {
                try
                {
                    string sValor = pParametros.GetValor("ConfirmacionConHuellas");
                    if (sValor != "")
                    {
                        bConfirmacionConHuellas = Convert.ToBoolean(sValor);
                    }
                }
                catch { }

                return bConfirmacionConHuellas;
            }
        } 


        ////////public static bool LectorDeHuellas
        ////////{
        ////////    get
        ////////    {
        ////////        try
        ////////        {
        ////////            string sValor = pParametros.GetValor("LectorDeHuella");
        ////////            if (sValor != "")
        ////////            {
        ////////                bLectorDeHuellas = Convert.ToBoolean(sValor);
        ////////            }
        ////////        }
        ////////        catch { }

        ////////        return bLectorDeHuellas;
        ////////    }
        ////////} 

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

        public static clsParametrosOficinaCentral Parametros
        {
            get { return pParametros; }
            set { pParametros = value; }
        }

        public static clsParametrosCompras ParametrosCompras
        {
            get { return pParametrosCompras; }
            set { pParametrosCompras = value; }
        }


        #endregion Propiedades  

        #region COMPRAS 
        //private static string sTablaPedidosCompras = "";
        private static string sGUID = "";
        private static clsConexionSQL cnn; 
        private static clsLeer leer;


        public static void GenerarNuevoGUID()
        {
            sGUID = Guid.NewGuid().ToString().Replace("-", "");
            GenerarTablaPedidosClaves();
        }

        public static string GUID
        {
            get
            {
                if (sGUID == "")
                {
                    sGUID = Guid.NewGuid().ToString().Replace("-", "");
                    GenerarTablaPedidosClaves();
                }

                return sGUID;
            }
        }

        //public static string NombreTablaCompras
        //{
        //    get 
        //    {
        //        if (sTablaPedidosCompras == "")
        //        {
        //            GenerarTablaPedidosClaves();
        //        }

        //        return sTablaPedidosCompras; 
        //    }
        //}

        public static void GenerarTablaPedidosClaves() 
        {
            General.FechaSistemaObtener(); 
            DateTime fecha = General.FechaSistema;
            string sSql = ""; 

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn); 

            //sTablaPedidosCompras = ""; 
            cnn.Cerrar(); 
            if ( cnn.Abrir() )
            {
                //sTablaPedidosCompras = "##" + Guid.NewGuid().ToString().Replace("-","") + "_" + 
                //    fecha.Year.ToString() + fecha.Month.ToString("00") + fecha.Day.ToString("00") + "_" +
                //    fecha.Hour.ToString("00") + fecha.Minute.ToString("00") + fecha.Second.ToString("00");

                ////sSql = string.Format("If Exists ( Select Name From tempdb..Sysobjects (NoLock) where Name = '{0}' and xType = 'U' ) Drop Table tempdb..{0} " + "\n\n", sTablaPedidosCompras); 
                ////sSql += string.Format(" Select top 0 IdClaveSSA_Sal as IdClaveSSA, ClaveSSA, DescripcionSal as DescripcionClave, " +
                ////            " CodigoEAN, Descripcion as Producto, space(4) as IdProveedor, space(100) as NombreProveedor, cast(0 as Float) as Precio, " + 
                ////            " cast(0 as Float) as PorcSurtimiento, cast(0 as Float) as TiempoDeEntrega, 0 as Cant_A_Pedir   " + 
                ////    " Into {0} " +
                ////    " From vw_Productos_CodigoEAN (NoLock) Where 1 = 0 ", sTablaPedidosCompras);

                sSql = string.Format("If Not Exists ( Select Name From Sysobjects (NoLock) where Name = 'Com_Pedidos_Compras' and xType = 'U' )  \n" +
                            "Begin \n" +
                            " Select top 0 space (300) As GUID, space(6) As FolioPedido, IdClaveSSA_Sal as IdClaveSSA, ClaveSSA, DescripcionSal as DescripcionClave, \n" +
                            " CodigoEAN, Descripcion as Producto, space(4) as IdProveedor, space(100) as NombreProveedor, cast(0 as numeric(14, 4) ) as Precio, \n" +
                            " 0 as Cant_A_Pedir, cast(0 as numeric(14, 4)) as PrecioMin, cast(0 as numeric(14, 4)) as PrecioMax, \n" +
                            " space(100) as ObservacionesPrecios, 0 As EsSobrePrecio \n " +
                            " Into Com_Pedidos_Compras \n" +
                            " From vw_Productos_CodigoEAN (NoLock) Where 1 = 0 \n" +
                            "End");

                if (!leer.Exec(sSql))
                {
                    General.msjError("Ocurriór un error al inicializar el listado de Claves para Pedido."); 
                }
                /// cnn.Cerrar(); 
            }

        }
        #endregion COMPRAS

        #region Parametros 
        private static bool bValidarClavesEnPerfilOperacion = false;
        private static string sValidarClavesEnPerfilOperacion = "";

        private static bool bValidarClavesEnPerfilDeComprador = false;
        private static string sValidarClavesEnPerfilDeComprador = "";

        public static bool ValidarClavesEnPerfilOperacion
        {
            get
            {
                if (sValidarClavesEnPerfilOperacion == "")
                {
                    bValidarClavesEnPerfilOperacion = pParametrosCompras.GetValorBool("CompraLibreDeProductos");
                    sValidarClavesEnPerfilOperacion = bValidarClavesEnPerfilOperacion.ToString();
                }
                return bValidarClavesEnPerfilOperacion;
            }
            set { bValidarClavesEnPerfilOperacion = value; }
        }

        public static bool ValidarClavesEnPerfilDeComprador
        {
            get
            {
                if (sValidarClavesEnPerfilDeComprador == "")
                {
                    bValidarClavesEnPerfilDeComprador = pParametrosCompras.GetValorBool("CompraLibreDeProductos_Compradores");
                    sValidarClavesEnPerfilDeComprador = bValidarClavesEnPerfilDeComprador.ToString();
                }
                return bValidarClavesEnPerfilDeComprador;
            }
            set { bValidarClavesEnPerfilDeComprador = value; }
        }
        #endregion Parametros
    }
}
