using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SC_SolutionsSystem.GAC;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.Criptografia;

using OficinaCentral.Catalogos;
using OficinaCentral.Configuraciones;
using OficinaCentral.Inventario;

using OficinaCentral.CuadrosBasicos; 
using OficinaCentral.CfgPrecios;
using DllFarmaciaSoft;
using DllFarmaciaSoft.Procesos;

namespace OficinaCentral
{
    static class Program 
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            General.DatosConexion.Servidor = "JOB";
            General.DatosConexion.BaseDeDatos = "SII_OficinaCentral____2K161103_0800"; // SII_OficinaCentral 
            General.DatosConexion.Usuario = "sa"; 
            General.DatosConexion.Password = "1234";
            General.DatosConexion.Puerto = "1433";

            General.DatosConexion.Servidor = "intermed.homeip.net";
            General.DatosConexion.BaseDeDatos = "SII_OficinaCentral"; // SII_OficinaCentral 
            General.DatosConexion.Usuario = "sa";
            General.DatosConexion.Password = "6F8770d1b9332a9d19299d8bf616ee16";

            DtGeneral.EsAdministrador = true;
            DtGeneral.EstadoConectado = "25";
            DtGeneral.FarmaciaConectada = "0001";
            DtGeneral.IdPersonal = "0001"; 
            string x = "Mañana será otro dia".Normalize( System.Text.NormalizationForm.FormD) ;


            //////// Punto de Entrada        
            if (ConfiguracionRegional.Revisar())
            {
                Application.Run(new FrmMain());
                //Application.Run(new FrmCB_ProgramacionesDeConsumo());
            } 

            ////Application.Run(new FrmMain());
            // Application.Run(new Form1());


        }
    }
}
