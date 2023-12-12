using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data; 


namespace TEST_ISIADISSEP
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

            //Data Source=LapJesus\SQL_2K14; Initial Catalog=SII_21_3193_CSU_SAN_FRANCISCO_TOTI_20180309_0921; user=sa; pwd=1234; Connect Timeout=0; Max Pool Size=500;  

            General.DatosConexion = new clsDatosConexion();
            General.DatosConexion.Servidor = @"Intermed.homeip.net";
            General.DatosConexion.BaseDeDatos = "SII_21_2193_CSU_SAN_FRANCISCO_TOTI_20180309_092121";
            General.DatosConexion.Usuario = "sa";
            General.DatosConexion.Password = "6F8770d1b9332a9d19299d8bf616ee16";
            General.DatosConexion.Puerto = "1433";
            General.DatosConexion.ForzarImplementarPuerto = true; 

            Application.Run(new Frm_ReadXML());
        }
    }
}
