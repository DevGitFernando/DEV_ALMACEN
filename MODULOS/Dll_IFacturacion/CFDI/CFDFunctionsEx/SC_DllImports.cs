using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Resources;
using System.IO;

using System.Threading; 
using System.Security.Permissions;
using System.Security.Policy;

using Microsoft.VisualBasic.FileIO;

namespace Dll_IFacturacion.CFDI
{
    internal class SC_DllImports 
    {
        // string sFile = Application.StartupPath + "\\diCrPKI.dll";
        string sFile = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\diCrPKI.dll";
        string sRuta = Environment.GetFolderPath(Environment.SpecialFolder.System); 
        // string sFile = @"C:\Windows\SysWOW64\diCrPKI.dll"; 

        byte[] byFile;
        IntPtr pDll; 

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        private static extern IntPtr DeleteFile(string dllToDel);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);

        // public static bool VerificaLibrerias = false; 

        public SC_DllImports()
        { 
        }

        ~SC_DllImports()
        {
            LiberarLibreria(); 
        }

        public void VerificaLibrerias(string RutaDestino)
        {
            descargarLibreria(sRuta, Dll_IFacturacion.Properties.Resources.diCrPKI, "diCrPKI.dll");

            descargarLibreria(RutaDestino, Dll_IFacturacion.Properties.Resources.libeay32, "libeay32.dll");
            descargarLibreria(RutaDestino, Dll_IFacturacion.Properties.Resources.ssleay32, "ssleay32.dll");
            descargarLibreria(RutaDestino, Dll_IFacturacion.Properties.Resources.openssl, "openssl.exe"); 

        }

        private void descargarLibreria(string RutaDestino, byte[] FileUnLoad, string FileName)
        {
            string sFileDownload = Path.Combine(RutaDestino, FileName); 

            try
            {
                if (!File.Exists(sFileDownload))
                {
                    //byFile = Dll_IFacturacion.Properties.Resources.diCrPKI;
                    Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sFileDownload, FileUnLoad, false);
                }
            }
            catch { }
        }

        public void CargarDll()
        {
            // Assembly AssemblyCargado = Assembly.Load(byFile);

            // IntPtr pDll = NativeMethods.LoadLibrary(@"PathToYourDll.DLL");
            pDll = LoadLibrary(@"" + sFile); 
        }

        public void LiberarLibreria()
        {
            try
            {
                FreeLibrary(pDll); 
                System.Threading.Thread.Sleep(100);
                // DeleteFile(sFile);
            }
            catch (Exception ex1)
            {
                string s = "";
            } 
        }
    }
}
