using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO; 

using SC_SolutionsSystem;

using DllFarmaciaSoft; 

namespace Farmacia.Ventas
{
    public partial class FrmVisorReceta : FrmBaseExt  
    {

        byte[] buffer = null;
        int iAncho = 0;
        int iAlto = 0;

        public FrmVisorReceta(Image Imagen, int Ancho, int Alto)
        {
            InitializeComponent();

            buffer = ConvertirImagenABytes(Imagen, FormatosImagen.Jpeg);
            iAncho = Ancho;
            iAlto = iAlto; 
        }

        public FrmVisorReceta(byte[] bytes, int Ancho, int Alto)
        {
            InitializeComponent();

            buffer = bytes;
            iAncho = Ancho;
            iAlto = iAlto; 
        }

        private void FrmVisorReceta_Load(object sender, EventArgs e)
        {
            panel.Dock = DockStyle.Fill;
            pcImage.Dock = DockStyle.Fill; 

            CargarReceta(buffer, iAncho, iAlto);
            pcImage.ZoomToFit(); 
            UpdateStatusBar(); 

        }

        private void UpdateStatusBar()
        {
            positionToolStripStatusLabel.Text = pcImage.AutoScrollPosition.ToString();
            imageSizeToolStripStatusLabel.Text = pcImage.GetImageViewPort().ToString();
            zoomToolStripStatusLabel.Text = string.Format("{0}%", pcImage.Zoom);
        }

        private void pcImage_ZoomChanged(object sender, EventArgs e)
        {
            UpdateStatusBar(); 
        }

        private void pcImage_Resize(object sender, EventArgs e)
        {
            UpdateStatusBar(); 
        }


        ////private void FrmVisorReceta_Shown(object sender, EventArgs e)
        ////{
        ////}

        private void CargarReceta(byte[] bytes, int Ancho, int Alto)
        {
            IntPtr intr = new IntPtr(0);

            MemoryStream ms = new MemoryStream(bytes);
            Image returnImage = Image.FromStream(ms);
            //pbReceta.Image = new Bitmap(returnImage, Ancho, Alto);

            if (Ancho != 0 && Alto != 0)
            {
                pcImage.Image = new Bitmap(returnImage, Ancho, Alto);
            }
            else
            {
                pcImage.Image = new Bitmap(returnImage); 
            }
        }

        private byte[] ConvertirImagenABytes(System.Drawing.Image imageIn, FormatosImagen formato)
        {
            MemoryStream ms = new MemoryStream();

            //formato = FormatosImagen.Jpeg; 

            try
            {
                switch (formato)
                {
                    case FormatosImagen.Jpeg:
                        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case FormatosImagen.Bmp:
                        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case FormatosImagen.Gif:
                        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                        break;

                    case FormatosImagen.Png:
                        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }
            }
            catch (Exception ex)
            {
                ex = null;
            }

            return ms.ToArray();
        }

        private void FrmVisorReceta_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.F4:
                    this.Hide(); 
                    break;

                default:
                    // base.OnKeyDown(e);
                    break;
            }

        }
    }
}
