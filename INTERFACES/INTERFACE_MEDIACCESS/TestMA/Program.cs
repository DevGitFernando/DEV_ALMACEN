using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DllRecetaElectronica.Catalogos;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data; 


namespace TestMA
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


            General.DatosConexion = new clsDatosConexion();
            General.DatosConexion.Servidor = "intermedcom.cloudapp.net";
            General.DatosConexion.BaseDeDatos = "SII_MEDIACCESS_Produccion";
            General.DatosConexion.Usuario = "Comercial";
            General.DatosConexion.Password = "im3D1v1s10ncomercial";
            General.DatosConexion.Puerto = "11433"; 


            Application.Run(new FrmAMPM_FirmasDigitales_Medicos());
        }
    }
}
