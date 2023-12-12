using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Proveedores_Web
{
    public static class DtGeneral
    {
        static clsDatosConexion datosCnn = new clsDatosConexion();
        static DataSet dtsPermisos = new DataSet();
        static DataSet dtsConfiguracion = new DataSet("Configuracion");
        //static DataSet dtsOrdenesDeCompra = new DataSet("OrdenesDeCompra");
        static DataSet dtsEstadosPorOrdenes = new DataSet("EstadosPorOrdenes");

        //Variable para la autenticación
        //static string sEstadoConectado = string.Empty;
        static string sIdProveedor = string.Empty;
        static string sLoginUser = string.Empty;
        static string sSucursal = string.Empty;
        static string sPassword = string.Empty;
        static string sArbol = string.Empty;
        static string sEmpresa = string.Empty;
        static string sTituloNavegador = string.Empty;
        static bool bMsjCerrarDivVentanas;
        static string sNombreProveedor = string.Empty;
        static string sImgFondo = string.Empty;
        static string sImgFondoEmpresa001 = string.Empty;
        static string sImgFondoEmpresa002 = string.Empty;
        static string sNombreEmpresa = string.Empty;
        static string sNombreCortoEmpresa = string.Empty;
        static string sRutaReportes = string.Empty;

        //Ruta del proyecto
        static string filePath = General.UnidadSO + ":\\inetpub\\wwwroot\\WebService_Cnn\\" + General.ArchivoIni; // 
        static string BaseDir = AppDomain.CurrentDomain.BaseDirectory.ToString();
        static int lenWebServiceName = AppDomain.CurrentDomain.BaseDirectory.ToString().Length;
        static clsLeer leer;
        static clsConexionSQL cnn;

        static DtGeneral()
        {
            General.ArchivoIni = "SII-Provedores";
            //General.ArchivoIni = "SII-Provedores-Web";
            General.DatosConexion = DtGeneral.GetConexion(General.ArchivoIni);
            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
        }


        public static clsDatosConexion GetConexion(string ArchivoConexion)
        {
            clsCriptografo crypto = new clsCriptografo();
            basSeguridad fg = new basSeguridad(ArchivoConexion);

            datosCnn.Servidor = fg.Servidor;
            datosCnn.BaseDeDatos = fg.BaseDeDatos;
            datosCnn.Usuario = fg.Usuario;
            datosCnn.Password = fg.Password;
            datosCnn.TipoDBMS = fg.TipoDBMS;
            datosCnn.Puerto = fg.Puerto;
            datosCnn.NormalizarDatos();

            return datosCnn;
        }
        public static void ObtenerConfiguracion()
        {
            filePath = BaseDir.Substring(0, lenWebServiceName); 
            dtsConfiguracion.ReadXml(filePath + @"\Proveedores.xml");
            leer.DataSetClase = dtsConfiguracion;
            if (leer.Leer())
            {
                //sEmpresa = leer.Campo("IdEmpresa");
                //sEstadoConectado = leer.Campo("IdEstado");
                sSucursal = leer.Campo("IdSucursal");
                sArbol = leer.Campo("Arbol");
                sTituloNavegador = leer.Campo("TituloNavegador");
                bMsjCerrarDivVentanas = Convert.ToBoolean(leer.Campo("MsjCerrarDivVentanas"));
                sImgFondo = leer.Campo("ImagenFondo");
                sImgFondoEmpresa001 = leer.Campo("ImagenFondoEmpresa001");
                sImgFondoEmpresa002 = leer.Campo("ImagenFondoEmpresa002");
                sRutaReportes = GetRutaReportes();

                GetNombreEmpresa(sEmpresa);
            }
            //dtsConfiguracion = new DataSet("Configuracion");
        }

        public static string GetProvedor(string sIdProvedorWeb)
        {
            sIdProveedor = sIdProvedorWeb;

            string sSql = "";
            
            sSql =string.Format("Select * From CatProveedores (NoLock) Where IdProveedor = '{0}'", sIdProvedorWeb);

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    sNombreProveedor = leer.Campo("Nombre");
                    ObtenerEstadosPorOrdenes();
                }
            }
            return sNombreProveedor;
        }
        //public static void ObtenerOrdenesdeCompra()
        //{
        //    string sSql = "";

        //    sSql = string.Format("Select * From vw_OrdenesCompras_Claves_Enc (NoLock) " +
        //                        "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdProveedor = '{3}' And Status = '{4}' " +
        //                        "And FechaColocacion Between '{5}' And '{6}'", sEmpresa, sEstadoConectado, sSucursal, sIdProveedor, "OC", "2012-06-01", "2012-06-10");

        //    if (!leer.Exec(sSql))
        //    {
        //        // Grabar Error 
        //    }
        //    else
        //    {
        //        dtsOrdenesDeCompra = leer.DataSetClase;
        //    }
        //}
        private static DataSet ObtenerEstadosPorOrdenes()
        {
            string sSql = "";

            sSql = string.Format("Select Distinct(IdEstado), (IdEstado + ' -- '+ Estado) Estado From vw_OrdenesCompras_Claves_Enc (NoLock) " +
	                            "Where IdEmpresa = '{0}' And IdFarmacia = '{1}' And IdProveedor = '{2}' And Status = '{3}'", sEmpresa, sSucursal, sIdProveedor, "OC");

            if (!leer.Exec(sSql))
            {
                // Grabar Error 
            }
            else
            {
                dtsEstadosPorOrdenes = leer.DataSetClase;
            }
            return dtsEstadosPorOrdenes;
        }
        private static void GetNombreEmpresa(string IdEmpresa)
        {
            string sSql = "";

            sSql = string.Format("Select Nombre, NombreCorto From CatEmpresas (NoLock) " +
                                "Where IdEmpresa = '{0}'", IdEmpresa);

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    sNombreEmpresa = leer.Campo("Nombre");
                    sNombreCortoEmpresa = leer.Campo("NombreCorto");
                }
            }
        }

        private static string GetRutaReportes()
        {
            string sValor = string.Empty;
            string sSql = "";

            sSql = "Select Valor From Net_CFGS_Parametros (NoLock) " +
	                "Where ArbolModulo = 'COMP' And NombreParametro = 'RutaReportes'";

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    sValor = leer.Campo("Valor");
                }
            }
            return sValor;
        }
        //
        public static string GetTime()
        {
            return DateTime.Now.ToString();
        }
        public static DataSet Permisos
        {
            get { return dtsPermisos.Copy(); }
            set { dtsPermisos = value; }
        }
        public static string Empresa
        {
            get { return sEmpresa; }
            set { sEmpresa = value.ToUpper(); }
        }
        public static string Arbol
        {
            get { return sArbol; }
        }
        //public static string EstadoConectado
        //{
        //    get { return sEstadoConectado; }
        //}

        public static string LoginUser
        {
            get { return sLoginUser; }
            set { sLoginUser = value; }
        }

        public static string Sucursal
        {
            get { return sSucursal; }
        }
        public static string Password
        {
            get { return sPassword; }
            set { sPassword = value; }
        }
        public static string TituloNavegador
        {
            get { return sTituloNavegador; }
        }
        public static bool MsjCerrarDivVentanas
        {
            get { return bMsjCerrarDivVentanas; }
        }
        public static string ImagenFondo
        {
            get { return sImgFondo; }
        }
        public static string ImagenFondoEmpresa001
        {
            get { return sImgFondoEmpresa001; }
        }
        public static string ImagenFondoEmpresa002
        {
            get { return sImgFondoEmpresa002; }
        }
        public static string IdProveedor
        {
            set { sIdProveedor = value; }
            get { return sIdProveedor; }
        }
        public static DataSet EstadosPorOrdenes
        {
            get { return dtsEstadosPorOrdenes.Copy(); }
        }
        public static clsConexionSQL DatosConexion
        {
            get { return cnn; }
        }
        public static string RutaAplicacion
        {
            get { return filePath; }
        }
        public static string NombreProveedor
        {
            get { return sNombreProveedor; }
        }
        public static string NombreEmpresa
        {
            get { return sNombreEmpresa; }
        }
        public static string NombreCortoEmpresa
        {
            get { return sNombreCortoEmpresa; }
        }
        public static string RutaReportes
        {
            get { return sRutaReportes; }
        }
    }
}