using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Resources;
using System.Windows.Forms;

using SC_SolutionsSystem.QRCode.Codec; 

namespace SC_SolutionsSystem.QRCode
{
    public class QRCodeViewer
    {
        #region Declaracion de Varibales 
        FrmQRCodeViewer f = new FrmQRCodeViewer(); 
        Bitmap picEncode_Image = new Bitmap(1, 1);
        #endregion Declaracion de Varibales

        #region Constructor y Destructor de Clase 
        public QRCodeViewer()
        { 
        }

        ~QRCodeViewer()
        {
            picEncode_Image = null;
            f = null; 
        }
        #endregion Constructor y Destructor de Clase

        #region Propiedades Publicas
        public Bitmap ImageView
        {
            get { return picEncode_Image; }
            set { picEncode_Image = value; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        public void Show()
        { 
            this.Show(picEncode_Image); 
        } 

        public void Show(Bitmap Image)
        { 
            f.picEncode.Image = Image;
            f.ShowDialog();
        }
        #endregion Funciones y Procedimientos Publicos
    }
}
