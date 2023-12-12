using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.EnterpriseServices.Internal;
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft;

using Microsoft.VisualBasic;

namespace AuditorFarmacia
{
    public class CheckVersion
    {
        #region Declaracion de Variables 
        private bool bGetUrl = false;
        private string sModulo = "";
        private string sVersion = "";
        private string sUrlCentral = "";
        private string sServidorLocal = "0";

        private string sModuloUpdater = "Updater Oficina Central.exe";
        private string sVersionUpdater = "0.0.0.0";

        private wsUpdater.wsUpdater update = new wsUpdater.wsUpdater();

        #endregion Declaracion de Variables 

        #region Constructores y Destructor de Clase
        public CheckVersion(string Modulo, string Version, bool ServidorLocal)
        {
            sModulo = Modulo;
            sVersion = Version;

            if (ServidorLocal)
                sServidorLocal = "1";  
        }
        #endregion Constructores y Destructor de Clase

        #region Funciones y Procedimientos Publicos 
        public bool CheckVersionModulo()
        {
            bool bRegresa = false;

            if (GetUrl())
            {
                RevisarVersionUpdater(); 
                bRegresa = RevisarVersion();
            }

            return bRegresa; 
        }

        public void DescargarActualizacion()
        {
            string sFile = Path.Combine(Application.StartupPath, "Updater Oficina Central.exe");
            string sParametros = "M" + sModulo + " " + "V" + sVersion + " " + "S" + sServidorLocal + " " + "CS"; 

            if (File.Exists(sFile))
            {
                Process proceso = new Process();
                proceso.StartInfo.FileName = sFile;
                proceso.StartInfo.Arguments = sParametros;
                proceso.Start();
                Application.Exit(); 
            }
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private bool GetUrl()
        {
            bool bRegresa = false;

            try
            {
                if (bGetUrl)
                {
                    bRegresa = true; 
                }
                else 
                {
                    string sXml = General.UnidadSO + ":\\SII_Update.xml";

                    clsLeer leer = new clsLeer();
                    DataSet dts = new DataSet();

                    dts.ReadXml(sXml);
                    leer.DataSetClase = dts;

                    if (leer.Leer())
                    {
                        sUrlCentral = "http://" + leer.Campo("Servidor") + "/";
                        sUrlCentral += leer.Campo("WebService") + "/";
                        sUrlCentral += leer.Campo("PaginaAsmx") + ".asmx";

                        update.Url = sUrlCentral;
                        bRegresa = true;
                        bGetUrl = true;
                    }
                }
            }
            catch { }

            return bRegresa; 
        }

        private bool RevisarVersion()
        {
            bool bRegresa = false;
            clsLeer leer = new clsLeer(); 

            try
            {
                //wsUpdater.wsUpdater update = new Farmacia.wsUpdater.wsUpdater();
                //update.Url = sUrlCentral;

                leer.DataSetClase = update.RevisarVersion(sModulo, sVersion);
                if (!leer.SeEncontraronErrores())
                {
                    bRegresa = leer.Leer(); 
                }
            }
            catch { }

            return bRegresa; 
        }

        private bool RevisarVersionUpdater()
        {
            bool bRegresa = false;
            string sFileUpdater = Path.Combine(Application.StartupPath, sModuloUpdater);
            string sSql = "";

            byte[] Buffer;
            clsLeer leer = new clsLeer();
            clsDatosCliente datosDeCliente = new clsDatosCliente(new clsDatosApp("",""), "", "");

            try
            {
                try
                {
                    if (File.Exists(sFileUpdater))
                    {
                        FileVersionInfo f = FileVersionInfo.GetVersionInfo(sFileUpdater);
                        sVersionUpdater = f.ProductVersion;
                    }
                }
                catch { }

                leer.DataSetClase = update.RevisarVersion(sModuloUpdater, sVersionUpdater);
                if (!leer.SeEncontraronErrores())
                {
                    bRegresa = leer.Leer();

                    if (bRegresa)
                    {
                        sSql = string.Format(" Exec sp_Net_CheckUpdateVersion '{0}', '{1}', '{2}'  ", sModuloUpdater, sVersionUpdater, 1);
                        leer.DataSetClase = update.GetVersion(datosDeCliente.DatosCliente(), sSql);

                        if (leer.Leer())
                        {
                            sModuloUpdater = leer.Campo("Nombre");
                            Buffer = System.Convert.FromBase64String(leer.Campo("EmpacadoModulo"));
                            Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(Application.StartupPath + "\\" + sModuloUpdater, Buffer, false);
                        }
                    }
                }
            }
            catch { }

            return bRegresa;
        }

        #endregion Funciones y Procedimientos Privados
    }
}
