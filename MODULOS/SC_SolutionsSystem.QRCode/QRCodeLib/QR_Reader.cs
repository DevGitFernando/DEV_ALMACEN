using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices; 
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging; 

using AForge.Video;
using AForge.Imaging;
using AForge.Video.DirectShow;

using ZXing; 
using SC_SolutionsSystem.QRCode.Codec; 

namespace SC_SolutionsSystem.QRCode
{
    public class QR_Reader
    {
        #region Declaracion de Variables 
        // QRCamControl camControl;
        // QRProcess process;

        FrmQR_Reader f; 

        string sCamaraProceso = "";
        string sResultado = "";
        bool bDatosLeidos = false;
        public Result Resultado;

        #endregion Declaracion de Variables 

        #region Constructores y Destructores de Clase  
        public QR_Reader()
        {
            ////camControl = new QRCamControl(); 
            ////process = new QRProcess();
        }

        ~QR_Reader()
        {
            f = null; 
        }
        #endregion Constructores y Destructores de Clase 

        #region Propiedades Publicas 
        public string Camara
        {
            get { return sCamaraProceso; }
            set { sCamaraProceso = value; }
        }

        public string DatosDecodificados
        {
            get { return sResultado; }
        }

        public bool DatosLeidos
        {
            get { return bDatosLeidos; }
        } 
        #endregion Propiedades Publicas 

        #region Funciones y Procedimientos Publicos 
        public void Show()
        {
            bDatosLeidos = false;
            sResultado = "";

            try
            {
                f = new FrmQR_Reader(sCamaraProceso);
                f.ShowReader();

                bDatosLeidos = f.DatosLeidos;
                sResultado = f.DatosDecodificados;
                Resultado = f.Resultado; 
            }
            catch (Exception ex)  
            {
                ex.Source = ex.Source; 
            }

            //////camControl = new QRCamControl();
            //////process = new QRProcess();
            //////process.ResultFound += new EventHandler<ProcessArgs>(process_ResultFound);
            //////process.Start();

            //////camControl.SetCamera(sCamaraProceso);
            //////camControl.SelectedDevice.NewFrame += new NewFrameEventHandler(captureDevice_NewFrame); 
            //////camControl.SelectedDevice.Start();

        } 
        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados 
        #endregion Funciones y Procedimientos Privados 
    } 
}
