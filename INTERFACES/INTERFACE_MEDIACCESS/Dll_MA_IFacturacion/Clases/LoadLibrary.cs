
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

namespace Dll_MA_IFacturacion
{
    internal class LoadLibrary_diCrPKI 
    {
        // string sFile = Application.StartupPath + "\\diCrPKI.dll";
        string sFile = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\diCrPKI.dll";
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

        public LoadLibrary_diCrPKI()
        { 
        }

        ~LoadLibrary_diCrPKI()
        {
            LiberarLibreria(); 
        }

        public void VerificaLibrerias()
        {
            try
            {
                if (!File.Exists(sFile))
                {
                    // byte[] byFile = SC_CFD.Properties.Resources.diCrPKI;
                    byFile =  Dll_MA_IFacturacion.Properties.Resources.diCrPKI;
                    Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sFile, byFile, false);
                }

                //FileInfo f = new FileInfo(sFile);
                //f.Attributes = FileAttributes.Hidden; 
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
                ex1.Source = ex1.Source; 
            } 
        }
    }
}

