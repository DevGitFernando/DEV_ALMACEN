using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DllFarmaciaSoft.QRCode;

using DllFarmaciaSoft.QRCode.GenerarEtiquetas;

using Dll_IATP2;
using Dll_IATP2.Protocolos; 

namespace IATP2
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

            clsDataMatrix dm = new clsDataMatrix();
            dm.CodificarImagen("00209001101007503006916038001*01142127*161015102712051001O100", @"D:\Img_DM_OC.jpeg");


            clsATP2_GenerarOrdenDeAcondicionamiento x = new clsATP2_GenerarOrdenDeAcondicionamiento();
            x.LeerArchivo(@"D:\ATP2\new_20180228_102150_42.DAT"); 

            Application.Run(new FrmMain());
        }   
    }
}
