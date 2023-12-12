using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.SQL; 

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using DllTransferenciaSoft;
using DllTransferenciaSoft.IntegrarInformacion;
using DllTransferenciaSoft.Zip; 

using Dll_SII_INadro; 

namespace Dll_SII_INadro.Informacion
{
    public partial class FrmINF_IntegrarCatalogos_Unidades : FrmBaseExt
    {
        OpenFileDialog openSII = new OpenFileDialog();

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        string sScriptFile_Datos = "";
        string sDirectorio = ""; 

        public FrmINF_IntegrarCatalogos_Unidades()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, GnDll_SII_INadro.DatosApp, this.Name); 
        }

        private void FrmINF_IntegrarCatalogos_Unidades_Load(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void InicializarPantalla()
        {
            Fg.IniciaControles();
            btnEsquema.Enabled = true;
            btnIntegrarDocumento.Enabled = false; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnIntegrarDocumento_Click(object sender, EventArgs e)
        {
            ZipUtil zip = new ZipUtil();
            FileInfo xf = new FileInfo(lblDocumentoEsquema.Text);

            if (!zip.Descomprimir(xf.Directory.FullName, openSII.FileName))
            {
                General.msjError("No fue posible desempacar el archivo de datos, intente de nuevo.");
            }
            else
            {
                EjecutarScripts();
            }
        }

        private void btnEsquema_Click(object sender, EventArgs e)
        {
            openSII.Title = "Archivos de Datos";
            openSII.Filter = "Archivos SII(*.SII)| *.SII";
            openSII.InitialDirectory = Application.StartupPath;
            openSII.AddExtension = true;

            if (openSII.ShowDialog() == DialogResult.OK)
            {
                FileInfo docto = new FileInfo(openSII.FileName);
                lblDocumentoEsquema.Text = openSII.FileName; 
                ///LeerEsquema();

                ////if (LeerPaquete())
                {
                    btnEsquema.Enabled = false;
                    btnIntegrarDocumento.Enabled = true;
                }
            }
        }

        private bool LeerPaquete()
        {
            bool bRegresa = false;
            string sFile = ""; 
            StreamReader fScript;
            ZipUtil zip = new ZipUtil();
            FileInfo xf = new FileInfo(lblDocumentoEsquema.Text);


            foreach(string sF in Directory.GetFiles(xf.Directory.FullName, "*.sql")) 
            {
                File.Delete(sF); 
            }

            //////if (!zip.Descomprimir(xf.Directory.FullName, openSII.FileName))
            //////{
            //////    General.msjError("No fue posible desempacar el archivo de datos, intente de nuevo.");
            //////}
            //////else
            //////{
            //////    string[] sF = Directory.GetFiles(xf.Directory.FullName, "*.sql"); 
            //////    sFile = sF[0];

            //////    fScript = new StreamReader(sFile, Encoding.Default);
            //////    sScriptFile_Datos = fScript.ReadToEnd();
            //////    fScript.Close();
            //////    File.Delete(sFile); 
            //////    bRegresa = true; 
            //////}

            return bRegresa; 
        }

        private bool EjecutarScripts()
        {
            bool bRegresa = true;
            bool bExito = true;
            string sSql = "";

            FileInfo xf = new FileInfo(lblDocumentoEsquema.Text); 
            string sFile = ""; 
            StreamReader fScript;
            clsScritpSQL Script;

            try
            {

                leer = new clsLeer(ref cnn);
                cnn.FormatoDeFecha = FormatoDeFecha.Ninguno;
                cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
                cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbriConexion(); 
                }
                else 
                {
                    cnn.IniciarTransaccion();
                    sSql = "Exec spp_Mtto_INT_ND_PrepararTablas ";

                    if (!leer.Exec(sSql))
                    {
                        bExito = false;
                    }
                    else 
                    {                       
                        string[] sF = Directory.GetFiles(xf.Directory.FullName, "*.sql"); 
                        foreach (string sFileX in sF)
                        {
                            fScript = new StreamReader(sFileX, Encoding.Default);
                            sScriptFile_Datos = fScript.ReadToEnd();
                            fScript.Close();

                            Script = new clsScritpSQL(sScriptFile_Datos, General.WithOutEncryption);
                            foreach (string sFragmento in Script.ListaScripts)
                            {
                                sSql = "Set DateFormat YMD \n";
                                sSql += "SET QUOTED_IDENTIFIER OFF \n";
                                sSql += sFragmento + "\n";

                                if (!leer.Exec(sSql))
                                {
                                    bExito = false;
                                    break;
                                }
                            }

                            if (bExito)
                            {
                                File.Delete(sFileX);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    
                    if (bExito)
                    {
                        sSql = "Exec spp_INT_ND_Generar_Tabla_INT_ND_CFG__ManejoDeClaves_OPM "; 
                        bExito = leer.Exec(sSql); 
                    }


                    ////// Asegurar que se hayan integrado todos los Scripts 
                    if (bExito)
                    {
                        cnn.CompletarTransaccion();
                        bRegresa = true;
                        General.msjUser("Información integrada correctamente.");
                    }
                    else
                    {
                        Error.GrabarError(leer, "EjecutarScripts()"); 
                        cnn.DeshacerTransaccion();
                        bRegresa = false;

                        General.msjError("Ocurrió un error al integrar la información de catalogos."); 
                    }

                    cnn.Cerrar();
                }
            }
            catch
            {
                Error.GrabarError(leer, "EjecutarScripts()");
                cnn.DeshacerTransaccion();
                bRegresa = false;

                General.msjError("Ocurrió un error al integrar la información de catalogos.");
                cnn.Cerrar();
            }

            return bRegresa;
        }
    }
}
