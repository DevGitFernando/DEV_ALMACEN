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

namespace Dll_ISESEQ
{
    #region Enumeradores 
    public enum TipoProcesoReceta
    {
        Ninguno = 0,

        Receta = 1, 
        Colectivo = 2, 

        SurteReceta = 3,
        CancelaReceta = 4,
        AcuseSurteReceta = 5, 
        
        ColectivoMedicamentos = 6, 
        SurtimientoColectivoMedicamentos = 7 
    }

    public enum Tabuladores
    {
        T00 = 0,
        T01 = 1,
        T02 = 2,
        T03 = 3,
        T04 = 4,
        T05 = 5,
        T06 = 6,
        T07 = 7,
        T08 = 8,
        T09 = 9, 
        T10 = 10  
    }
    #endregion Enumeradores
    
    public static class GnDll_SII_SESEQ
    {
        #region Declaracion de Variables        
        private static clsDatosApp dpDatosApp = new clsDatosApp("Dll_ISESEQ", "");
        private static basGenerales Fg = new basGenerales(); 

        private static string sReferencia_SESEQ = ""; 
        private static string sURL_Interface = ""; //@"http://localhost:21734/wsSII_INT_ISESEQ/wsISESEQ.asmx";


        private static bool bMostrarInterface = false;
        private static bool bEsRespuestaWeb = true; 
        #endregion Declaracion de Variables

        #region Constructor 
        static GnDll_SII_SESEQ()
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
        public static string Referencia_SESEQ
        {
            get { return sReferencia_SESEQ; }
            set { sReferencia_SESEQ = value; }
        }

        public static string URL_Interface
        {
            get 
            { 
                if ( sURL_Interface  == "" ) 
                {
                    //ObtenerURL_Interface();
                }

                return sURL_Interface; 
            }
            set { sURL_Interface = value; }
        }

        public static bool MostrarInterface
        {
            get { return bMostrarInterface; }
            set { bMostrarInterface = value; }
        }

        public static bool EsRespuestaWeb
        {
            get { return bEsRespuestaWeb; }
            set { bEsRespuestaWeb = value; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos 
        public static void MensajePantalla()
        {
            Console.WriteLine(""); 
        }

        public static void MensajePantalla(string Mensaje)
        {
            if (!GnDll_SII_SESEQ.bMostrarInterface)
            {
                if (GnDll_SII_SESEQ.EsRespuestaWeb)
                {
                    Console.WriteLine(Mensaje); 
                }
            }
        }

        public static void ObtenerURL_Interface(string IdEmpresa, string IdEstado, string IdFarmacia)
        {
            ObtenerURL_Interface(IdEmpresa, IdEstado, IdFarmacia, ""); 
        }

        public static void ObtenerURL_Interface(string IdEmpresa, string IdEstado, string IdFarmacia, string ValidarURL)  
        {
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);
            clsGrabarError Error = new clsGrabarError(General.DatosConexion, GnDll_SII_SESEQ.DatosApp, "GnDll_SII_SESEQ");
            string sSql = "";
            string sFiltro = " Where URL_Interface <> '' "; 


            if (IdFarmacia != "")
            {
                sFiltro = string.Format("Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}'   And URL_Interface <> ''  ", 
                    IdEmpresa, IdEstado, IdFarmacia); 
            }


            sSql = string.Format("Select  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface " +
                "From INT_SESEQ__CFG_Farmacias_UMedicas (NoLock) {0} " , 
                sFiltro);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerURL_Interface()"); 
            }
            else
            {
                if ( leer.Leer())
                {
                    sURL_Interface = leer.Campo("URL_Interface");
                    sReferencia_SESEQ = leer.Campo("Referencia_SESEQ");
                    ////System.Console.WriteLine(string.Format("Url Interface : {0} ", sReferencia_SESEQ));
                }
            }

        }

        public static string getTabs(Tabuladores Tabs)
        {
            return getTabs((int)Tabs); 
        }

        public static string getTabs(int Tabs)
        {
            string sRegresa = "";

            if (Tabs > 0)
            {
                sRegresa = Fg.PonFormato("", "\t", Tabs);
            }

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Publicos
    }
}
