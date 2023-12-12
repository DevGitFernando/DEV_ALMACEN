using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Neodynamic.SDK.Printing;

namespace DllFarmaciaSoft.QRCode.VisorEtiquetas
{
    internal partial class ImageViewer : UserControl
    {
        Image currentImage = null; 
        int iCurrPage = 1, iPages = 1; 
        
        public ImageViewer()
        {
            InitializeComponent();
        }

        public void LoadImage(System.IO.Stream imgStream)
        {
            picLabel.Image = null;
            if (currentImage != null)
            {
                currentImage.Dispose();
                currentImage = null;
            }

            currentImage = Image.FromStream(imgStream);
            iCurrPage = 1;
            this.RefreshImage();
        }

        public void ResetImage()
        {
            picLabel.Image = null; 
        }

        public void RefreshImage()    
        {      
            iPages = currentImage.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);
            lblNumOfLabels.Text = "Etiqueta " + iCurrPage.ToString() + " de " + iPages.ToString(); 
            currentImage.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, iCurrPage - 1);
            picLabel.Image = new Bitmap(currentImage);
            this.SetImageLocation();     
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (iCurrPage > 1)
            { 
                iCurrPage--; 
                this.RefreshImage(); 
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (iCurrPage < iPages)
            {
                iCurrPage++;
                this.RefreshImage();
            }
        }

        private void SetImageLocation()
        {
            int x = 0;
            int y = 0;

            if (picLabel.Width > pnlContainer.ClientRectangle.Width)
            {
                x = 0;
            }
            else
            {
                x = (pnlContainer.ClientRectangle.Width - picLabel.Width) / 2;
            }

            if (picLabel.Height > pnlContainer.ClientRectangle.Height)
            {
                y = 0;
            }
            else
            {
                y = (pnlContainer.ClientRectangle.Height - picLabel.Height) / 2;
            }

            picLabel.Location = new Point(x, y);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.SetImageLocation();
        }
    }
}
