using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
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

using DllFarmaciaSoft;


namespace DllFarmaciaSoft.Productos
{
    public partial class FrmProductos_AgregarImagenes : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerImagenes;
        clsAyudas Ayuda = new clsAyudas();
        clsConsultas Consultas;

        bool bHabilitarCapturaDeImeges = false;
        bool bExito = false;
        byte[] byteimagen;

        string sIdProducto = "";
        string sCodigoEAN = "";
        string sIdPersonal = DtGeneral.IdPersonal;

        int iImagen = 1;
        int iTamañoBloque = 4;
        int iImagenesCargadas = 0;
        int iLinea = 1;
        int iResolucion = 0;

        int iPosicion_X = 3;
        int iPosicion_Y = 3;
        int iSeparador = 0;

        ItemImagen itemImgActivo = new ItemImagen();

        OpenFileDialog file = new OpenFileDialog();
        FolderBrowserDialog Folder = new FolderBrowserDialog();

        string sDirectorioDeImagenes = Application.StartupPath + @"\Imagenes_de_productos\";
        clsLeer listaDeArchivos = new clsLeer(); 

        public FrmProductos_AgregarImagenes(string IdProducto, string CodigoEAN):this(IdProducto, CodigoEAN, new clsLeer()) 
        {
        }

        public FrmProductos_AgregarImagenes(string IdProducto, string CodigoEAN, clsLeer Archivos)
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerImagenes = new clsLeer(ref cnn);

            listaDeArchivos = Archivos; 


            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            sIdProducto = IdProducto;
            sCodigoEAN = CodigoEAN;

        }

        private void FrmProductos_AgregarImagenes_Load(object sender, EventArgs e)
        {
            LimpiarImagenes();
        }

        private void FrmProductos_AgregarImagenes_Shown(object sender, EventArgs e)
        {
            string sFileLoad = "";
            string sNombreArchivo = "";
            string sIdConsecutivo = "";
            bool bImagenesCargadas = false; 

            iImagen = 1;
            iTamañoBloque = 4;
            iImagenesCargadas = 0;
            iLinea = 1;

            iPosicion_X = 3;
            iPosicion_Y = 3;
            iSeparador = 0;

            bImagenesCargadas = !(listaDeArchivos.Registros > 0);
            listaDeArchivos.RegistroActual = 1;           
            while (listaDeArchivos.Leer())
            {
                sIdConsecutivo = listaDeArchivos.Campo("Id Imagen"); 
                sNombreArchivo = string.Format("{0}_{1}___{2}_{3}",
                sIdProducto, sCodigoEAN, Fg.PonCeros(listaDeArchivos.Campo("Id Imagen"), 4), listaDeArchivos.Campo("Nombre"));

                sFileLoad = string.Format(@"{0}\{1}", sDirectorioDeImagenes, sNombreArchivo);

                AgregarImagen(sFileLoad, sIdConsecutivo, true); 
            }

            btnNuevo.Enabled = bImagenesCargadas;
            btnGuardar.Enabled = bImagenesCargadas;
            btnLoadImagenes.Enabled = bImagenesCargadas;
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarImagenes(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarImagenes();
        }

        private bool GuardarImagenes()
        {
            string sImagenes = "";
            bool bRegresa = true;
            string sSql = "";
            int iImagenes = 0;

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion(); 
            }
            else
            {
                cnn.IniciarTransaccion();

                foreach (ItemImagen item in Imagenes)
                {
                    iImagenes++;
                    sImagenes += string.Format("{0}\t\n", item.Nombre);

                    sSql = string.Format(" Exec spp_Mtto_CatProductos_Imagenes  " +
                        " @IdProducto = '{0}', @CodigoEAN = '{1}', @Consecutivo = '{2}', @NombreImagen = '{3}', @Imagen = '{4}', @IdPersonal = '{5}' ",
                        item.IdProducto, item.CodigoEAN, item.Consecutivo, item.Nombre, item.Base64, sIdPersonal);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }

                if (!bRegresa)
                {
                    Error.GrabarError(leer, "GuardarImagenes()"); 
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al guardar las imagenes seleccionadas"); 
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Se guardaron satisfactoriamente las imagenes seleccionadas.");
                }
                cnn.Cerrar(); 
            }


            if (bRegresa)
            {
                LimpiarImagenes();
                this.Hide();
            }

            //General.msjUser(sImagenes);

            return bRegresa; 
        }

        private void btnLoadImagenes_Click(object sender, EventArgs e)
        {
            FormatosImagen formato = FormatosImagen.Ninguno;
            FileInfo imagenInfo; 
            //byte[] byteimagen;
            string sFiltro = "(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";

            file = new OpenFileDialog();
            file.Multiselect = true;
            file.Title = "Seleccionar la imagenes";
            file.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            file.Filter = sFiltro;

            iResolucion = (int)nmResolucion.Value;
            LimpiarImagenes();
            nmResolucion.Value = (decimal)iResolucion;

            if (file.ShowDialog() == DialogResult.OK)
            {
                FileInfo imagen = new FileInfo(file.FileName);
                formato = (FormatosImagen)file.FilterIndex;

                iImagen = 1;
                iTamañoBloque = 4;
                iImagenesCargadas = 0;
                iLinea = 1;

                iPosicion_X = 3;
                iPosicion_Y = 3;
                iSeparador = 0;


                foreach (string fileLoad in file.FileNames)
                {
                    // Create a PictureBox.
                    try
                    {
                        AgregarImagen(fileLoad, "*", false); 
                    }
                    catch (Exception ex)
                    {
                        ////// Could not load the image - probably related to Windows file system permissions.
                        ////MessageBox.Show("Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
                        ////    + ". You may not have permission to read the file, or " +
                        ////    "it may be corrupt.\n\nReported error: " + ex.Message);
                    }
                }
            }
        }

        private void AgregarImagen(string fileLoad, string Consecutivo, bool AgregarMenuContextual)
        {
            FileInfo imagenInfo; 

            try
            {
                imagenInfo = new FileInfo(fileLoad);

                ItemImagen itemImg = new ItemImagen();
                PictureBox pb = new PictureBox();
                CheckBox chk = new CheckBox();
                Label lbl = new Label();

                pb.Name = string.Format("pic_{0}", Fg.PonCeros(iImagen, 8));
                pb.Height = 180;
                pb.Width = 180;
                pb.BorderStyle = BorderStyle.None;
                pb.Top = iPosicion_Y;
                pb.Left = iPosicion_X;
                iPosicion_X += 185;


                chk.Name = string.Format("chk_{0}", Fg.PonCeros(iImagen, 8));
                chk.Height = 20;
                chk.Width = 20;
                chk.Top = pb.Top + pb.Height + 1;
                chk.Left = pb.Left + 10;
                chk.BackColor = Color.Transparent;
                chk.Text = imagenInfo.Name;


                lbl.Name = string.Format("lbl_{0}", Fg.PonCeros(iImagen, 8));
                lbl.Text = "";
                lbl.BorderStyle = BorderStyle.None;
                lbl.Height = 20;
                lbl.Width = 100;
                lbl.Top = chk.Top;
                lbl.Left = chk.Left + chk.Width + 1;
                lbl.Left = pb.Left;
                lbl.Width = pb.Width;
                lbl.Visible = true;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.BringToFront();


                iSeparador += 10;
                MostrarImagen(fileLoad, pb, itemImg);

                itemImg.Lbl = lbl;
                itemImg.Check = chk;
                itemImg.Nombre = imagenInfo.Name;
                itemImg.IdProducto = sIdProducto;
                itemImg.CodigoEAN = sCodigoEAN;
                itemImg.Consecutivo = Consecutivo;

                lbl.Tag = itemImg;
                pb.Tag = itemImg;
                chk.Tag = pb;
                chk.Tag = itemImg;


                eliminarToolStripMenuItem.Enabled = AgregarMenuContextual;
                modificarToolStripMenuItem.Enabled = AgregarMenuContextual;
                pb.ContextMenuStrip = menuImagenes;


                if (AgregarMenuContextual)
                {
                }


                ////pb.DoubleClick += new System.EventHandler(ClickImage);
                pb.Click += new System.EventHandler(SelectedImage);


                BackPanel.Controls.Add(pb);
                BackPanel.Controls.Add(chk);
                BackPanel.Controls.Add(lbl);

                iImagenesCargadas++;
                if (iImagenesCargadas == iTamañoBloque)
                {
                    iLinea++;
                    iPosicion_Y += 185 + 25;
                    iPosicion_X = 3;
                    iImagenesCargadas = 0;
                    iSeparador = 0;
                }
            }
            catch 
            { 
            }
        }


        public Image PictureBoxZoom(Image imgP)
        {
            return PictureBoxZoom(imgP, imgP.Width, imgP.Height); 
        }

        public Image PictureBoxZoom(Image imgP, PictureBox Pic)
        {
            return PictureBoxZoom(imgP, Pic.Width, Pic.Height);
        }

        public Image PictureBoxZoom(Image imgP, int With, int Height)
        {
            Bitmap bm = new Bitmap(imgP, imgP.Width, imgP.Height); 

            try
            {                

                int dw = imgP.Width;
                int dh = imgP.Height;
                int tw = With;
                int th = Height;
                double zw = (tw / (double)dw);
                double zh = (th / (double)dh);
                double z = (zw <= zh) ? zw : zh;
                dw = (int)(dw * z);
                dh = (int)(dh * z);

                ////dw = With;
                ////dh = Height;

                ///m_Image = new Bitmap(dw, dh); 
                bm = new Bitmap(imgP, dw, dh);
                ////Graphics grap = Graphics.FromImage(bm);
                ////grap.InterpolationMode = InterpolationMode.HighQualityBicubic;

            }
            catch 
            { 
            }

            return bm;
        }

        public Image PictureBoxZoom(Image imgP, Size size)
        {
            Bitmap bm = new Bitmap(imgP, Convert.ToInt32(imgP.Width * 180), Convert.ToInt32(imgP.Height * 180));
            Graphics grap = Graphics.FromImage(bm);
            grap.InterpolationMode = InterpolationMode.HighQualityBicubic;
            return bm;
        }

        private void btnRotarIzquierda_Click(object sender, EventArgs e)
        {
        ////    Image img = pbImagen.Image;
        ////    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
        ////    pbImagen.Image = img;
        }

        private void btnRotarDerecha_Click(object sender, EventArgs e)
        {
        ////    Image img = pbImagen.Image;
        ////    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
        ////    pbImagen.Image = img;
        }
        #endregion Botones

        #region Funciones y Procedimientos Publicos 
        public List<ItemImagen> Imagenes
        {
            get 
            {
                List<ItemImagen> listaItems = new List<ItemImagen>();

                try
                {
                    ItemImagen item = new ItemImagen();

                    try
                    {
                        foreach (Control c in BackPanel.Controls)
                        {

                            if (c is CheckBox)
                            {
                                if (((CheckBox)c).Checked)
                                {
                                    item = (ItemImagen)((CheckBox)c).Tag;
                                    listaItems.Add(item); 
                                }
                            }

                            ////if (c is PictureBox)
                            ////{
                            ////    ((PictureBox)c).BorderStyle = BorderStyle.FixedSingle;
                            ////}

                            ////if (c is Label)
                            ////{
                            ////    ((Label)c).Text = "";
                            ////}
                        }
                    }
                    catch { }

                }
                catch 
                {
                    listaItems = new List<ItemImagen>();
                }

                return listaItems;
            }
        }
        #endregion Funciones y Procedimientos Publicos
        
        #region Manipulacion de Imagen
        // The Event to click the image
        private void ClickImage(Object sender, System.EventArgs e)
        {
            // On Click: load (ImageToShow) with (Tag) of the image
            ///ImageToShow = ((System.Windows.Forms.PictureBox)sender).Tag.ToString();
            // then view this image on the form (frmView)
            FrmZoom f = new FrmZoom((((System.Windows.Forms.PictureBox)sender).Image));
            f.ShowDialog();

            ((System.Windows.Forms.PictureBox)sender).Image = f.Imagen; 
        }

        private void SelectedImage(object sender, EventArgs e)
        {
            string s = "";
            int iVueltas = 10;

            try
            {
                for (int i = 1; i <= iVueltas; i++)
                {
                    try
                    {
                        foreach (Control c in BackPanel.Controls)
                        {
                            if (c is PictureBox)
                            {
                                ((PictureBox)c).BorderStyle = BorderStyle.FixedSingle;
                            }

                            if (c is Label)
                            {
                                ((Label)c).Text = "";
                            }
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                s = ex.Message;
            }


            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;

            try
            {
                itemImgActivo = (ItemImagen)((PictureBox)sender).Tag;

                Label lbl = (Label)(itemImgActivo).Lbl;
                lbl.Text = "Seleccionado";
            }
            catch
            { 
            }
        }

        private void LimpiarImagenes()
        {
            string s = "";
            int iVueltas = 10;
            nmResolucion.Value = 800;

            try
            {
                for (int i = 1; i <= iVueltas; i++)
                {
                    try
                    {
                        BackPanel.Controls.Clear();
                    }
                    catch { }

                    try
                    {
                        foreach (Control c in BackPanel.Controls)
                        {
                            if (c is PictureBox || c is CheckBox)
                            {
                                BackPanel.Controls.Remove(c);
                            }
                        }
                    }
                    catch { }
                }
            }
            catch(Exception ex)
            {
                s = ex.Message;
            }

            this.Refresh(); 

        }

        private void MostrarImagen(string RutaImagen, PictureBox PictureImagen, ItemImagen Item)
        {
            IntPtr intr = new IntPtr(0);
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            PictureImagen.Image = Image.FromFile(RutaImagen);
          

            Item.Imagen = PictureImagen.Image;
            ////Item.Imagen = PictureImagen.Image.GetThumbnailImage(iAncho, iAlto, myCallback, intr);
            byteimagen = ConvertirImagenABytes(Item.Imagen, FormatosImagen.Jpeg);
            Item.Base64 = Convert.ToBase64String(byteimagen, 0, byteimagen.Length);

            ////PictureImagen.Image = PictureImagen.Image.GetThumbnailImage(PictureImagen.Width, PictureImagen.Height, myCallback, intr);

            PictureImagen.Image = PictureBoxZoom(Item.Imagen, PictureImagen);


            Item.Imagen = PictureBoxZoom(Item.Imagen, iResolucion, iResolucion);
            //Item.Imagen = PictureBoxZoom(Item.Imagen, Item.Imagen.Width, Item.Imagen.Height);
            byteimagen = ConvertirImagenABytes(Item.Imagen, FormatosImagen.Jpeg);
            Item.Base64 = Convert.ToBase64String(byteimagen, 0, byteimagen.Length);

        }

        private bool ThumbnailCallback()
        {
            return false;
        }

        private byte[] ConvertirImagenABytes(System.Drawing.Image imageIn, FormatosImagen formato)
        {
            MemoryStream ms = new MemoryStream();

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

            return ms.ToArray();
        }
        #endregion Manipulacion de Imagen

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sSql = "";

            sSql = string.Format(
                "Update P Set Status = 'C' " +
                " From CatProductos_Imagenes P " +
                " Where IdProducto = '{0}' and CodigoEAN = '{1}' and Consecutivo = '{2}' ",
                itemImgActivo.IdProducto, itemImgActivo.CodigoEAN, itemImgActivo.Consecutivo);

            if (leer.Exec(sSql))
            {
                itemImgActivo.Check.Enabled = false;
                itemImgActivo.Lbl.Text = "ELIMINADO";
                itemImgActivo.Pic.Click -= SelectedImage;
                itemImgActivo.Pic.Enabled = false; 
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmZoom f = new FrmZoom(itemImgActivo.Imagen);
                f.ShowDialog();

                ((System.Windows.Forms.PictureBox)sender).Image = f.Imagen;
            }
            catch (Exception ex)
            { 
            }
        }

    }

    public class ItemImagen
    {
        public string IdProducto = "";
        public string CodigoEAN = "";
        public string Consecutivo = "*";

        public string Nombre = "";
        public Image Imagen = null;
        public string Base64 = ""; 

        public CheckBox Check = new CheckBox();
        public PictureBox Pic = new PictureBox();
        public Label Lbl = new Label();

        ////public Image Imagen
        ////{
        ////    get { return imgImagen; }
        ////    set { imgImagen = value; }
        ////}

        ////public string Nombre
        ////{
        ////    get { return sNombre; }
        ////    set { sNombre = value; }
        ////}
    }
}
