using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

namespace DllFarmaciaSoft.Productos
{
    internal partial class FrmZoom : FrmBaseExt
    {
        Image imgLoad;

        public FrmZoom(string ImagenMostrar)
        {
            InitializeComponent();

            imgLoad = Image.FromFile(ImagenMostrar);

            this.Height = 500;
            this.Width = 500; 
        }

        public FrmZoom(Image ImagenMostrar)
        {
            InitializeComponent();

            imgLoad = ImagenMostrar;

            this.Height = 500;
            this.Width = 500; 
        }

        public Image Imagen
        {
            get { return imgDisplay.Image; }
        }

        private void FrmZoom_Load(object sender, EventArgs e)
        {
            imgDisplay.Image = imgLoad;
            // Center image
            //imgDisplay.Left = (this.Width - imgDisplay.Width) / 2;
        }

        private void btnRotarIzquierda_Click(object sender, EventArgs e)
        {
            Image img = imgDisplay.Image;
            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            imgDisplay.Image = img;
        }

        private void btnRotarDerecha_Click(object sender, EventArgs e)
        {
            Image img = imgDisplay.Image;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            imgDisplay.Image = img;
        }
    }
}
