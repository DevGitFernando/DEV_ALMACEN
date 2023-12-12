using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Dll_HitachiEncoder;

using DllFarmaciaSoft.QRCode.GenerarEtiquetas;
using DllFarmaciaSoft.QRCode;

namespace SII_INT_Hitachi_Encoder_Farmacia
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

            Application.Run(new FrmMain());
        }
    }
}
