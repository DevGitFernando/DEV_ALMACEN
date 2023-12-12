using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;

using Microsoft.VisualBasic;

using System.Text;
using System.IO;
using System.Configuration;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;


namespace TEST_ISESEQ
{
    static class Program
    {
        static string key_generica = "1nt3rf4c3_3c4fr3tn1";

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

            GetType(); 

            Application.Run(new Form1());
        }

        private  static string GetType()
        {
            string sRegresa = "";
            DateTime dt = DateTime.Now;
            ////AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();

            

            sRegresa = string.Format("{3}{0}{1}{2}{3}{1}{0}{2}{3}{2}{0}{1}{3}",
                dt.Year, dt.Month, dt.Day, key_generica);

            sRegresa = Encrypt(sRegresa, true);

            return sRegresa;
        }

        private static string Encrypt(string toEncrypt, bool useHashing)
        {
            string sRegresa = "";

            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                string key = key_generica;

                //System.Windows.Forms.MessageBox.Show(key);
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();

                sRegresa = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                sRegresa = "";
            }

            return sRegresa;
        }
    }
}
