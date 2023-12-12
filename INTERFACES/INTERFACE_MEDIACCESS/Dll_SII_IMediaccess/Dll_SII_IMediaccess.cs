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
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllFarmaciaSoft;

namespace Dll_SII_IMediaccess
{
    public enum TipoDeCopago
    {
        Ninguno = 0,
        Monto = 1, Porcentaje = 2
    }

    public enum TipoDeSurtido
    {
        Ninguno = 0,
        Mediaccess, Intermed, AMPM
    }

    public static class GnDll_SII_IMediaccess
    {
        #region Declaracion de Variables 
        private static clsDatosApp dpDatosApp = new clsDatosApp("Dll_SII_IMediaccess", "");
        private static string sMa_IdClinica = "";
        #endregion Declaracion de Variables

        #region Constructor 
        static GnDll_SII_IMediaccess()
        {
            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());          
        }
        #endregion Constructor

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
        public static string IdFarmacia_MA
        {
            get { return DtGeneral.EmpresaConectada + DtGeneral.EstadoConectado + DtGeneral.FarmaciaConectada; }
        }


        public static string Ma_IdClinica
        {
            get
            {
                if (sMa_IdClinica == "")
                {
                    sMa_IdClinica = Obtener_clinica();
                }

                return sMa_IdClinica;
            }
        }


        #endregion Propiedades

        #region Servicios Web
        static string sURL_Validaciones = "";

        public static string URL_Validaciones
        {
            get 
            {
                if (sURL_Validaciones == "")
                {
                    sURL_Validaciones = ObtenerURL_ValidacionEligibilidad(); 
                }
                return sURL_Validaciones; 
            }
            set { sURL_Validaciones = value; }
        }

        private static string ObtenerURL_ValidacionEligibilidad()
        {
            string sURL = ""; 
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            clsGrabarError Error = new clsGrabarError(General.DatosConexion, GnDll_SII_IMediaccess.dpDatosApp, "GnDll_SII_IMediaccess"); 
            string sSql = "";

            sSql = string.Format("Select IdEmpresa, IdEstado, IdFarmacia, URL_Produccion, URL_Pruebas " +
                " From INT_MA__CFG_ValidarElegibilidad (NoLock) " + 
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerURL_ValidacionElegibilidad()");
                General.msjError("Ocurrió un error al obtener la información para validación de eligibilidad.");
            }
            else
            {
                if (leer.Leer())
                {
                    sURL = leer.Campo("URL_Produccion");
                }
            }

            return sURL; 
        }
        #endregion Servicios Web 

        #region Atencion de recetas manuales 
        static string sAtencionRecetasManuales = "";
        static bool bAtencionRecetasManuales = false;
        static bool bCargaAutomaticaProductos = false; 

        public static bool AtencionRecetasManuales
        {
            get
            {
                if (sAtencionRecetasManuales == "")
                {
                    Obtener_AtencionRecetasManuales();
                    sAtencionRecetasManuales = bAtencionRecetasManuales.ToString();
                }
                return bAtencionRecetasManuales;
            }
            set { bAtencionRecetasManuales  = value; }
        }

        public static bool CargaAutomaticaProductosReceta
        {
            get
            {
                if (sAtencionRecetasManuales == "")
                {
                    Obtener_AtencionRecetasManuales();
                    sAtencionRecetasManuales = bCargaAutomaticaProductos.ToString();
                }
                return bCargaAutomaticaProductos;
            }
            set { bCargaAutomaticaProductos = value; }
        }

        private static bool Obtener_AtencionRecetasManuales()
        {
            bool bAtencionDeRecetas = false;
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            clsGrabarError Error = new clsGrabarError(General.DatosConexion, GnDll_SII_IMediaccess.dpDatosApp, "GnDll_SII_IMediaccess");
            string sSql = "";

            sSql = string.Format("Select  IdEmpresa, IdEstado, IdFarmacia, Referencia_MA, AtiendeRecetasManuales, CargaAutomaticaProductos " +
                " From INT_MA__CFG_FarmaciasClinicas (NoLock) " +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            bAtencionRecetasManuales = false;
            bCargaAutomaticaProductos = false;

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Obtener_AtencionRecetasManuales()");
                General.msjError("Ocurrió un error al obtener la información para validación de eligibilidad.");
            }
            else
            {
                if (leer.Leer())
                {
                    bAtencionRecetasManuales = leer.CampoBool("AtiendeRecetasManuales");
                    bCargaAutomaticaProductos = leer.CampoBool("CargaAutomaticaProductos");
                }
            }

            return bAtencionRecetasManuales;
        }
        #endregion Atencion de recetas manuales
         
        #region Reportes 
        #endregion Reportes 

        #region Funciones y Procedimientos Privados 
        private static string Obtener_clinica()
        {
            clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref con);
            clsGrabarError Error = new clsGrabarError(General.DatosConexion, GnDll_SII_IMediaccess.dpDatosApp, "GnDll_SII_IMediaccess");

            string sClinica = "";

            string sSql = string.Format("Select * From INT_MA__CFG_FarmaciasClinicas Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}'",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Obtener_AtencionRecetasManuales()");
            }
            else
            {
                if(leer.Leer())
                {
                    sClinica = leer.Campo("Referencia_MA");
                }
            }

            return sClinica;
        }
        #endregion Funciones y Procedimientos Privados

        #region Envio de informacion de Catalogos 
        public static bool Enviar_Catalogo_Precios = false;
        public static bool Enviar_Catalogo_Productos = true;
        #endregion Envio de informacion de Catalogos
    }

}
