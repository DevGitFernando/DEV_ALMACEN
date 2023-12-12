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

using DllFarmaciaSoft;


namespace DllFarmaciaSoft.Productos
{
    public partial class FrmProducto_KarruselImagenes : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerImagenes;
        clsAyudas Ayuda = new clsAyudas();
        clsConsultas Consultas;

        bool bHabilitarCapturaDeImeges = false; 
        bool bExito = false; 
        string sIdProducto = "";
        string sCodigoEAN = ""; 
        byte[] byteimagen;

        string sDirectorioDeImagenes = Application.StartupPath + @"\Imagenes_de_productos\";

        bool bEsConsultaExterna = false; 

        public FrmProducto_KarruselImagenes()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerImagenes = new clsLeer(ref cnn); 

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            //sIdProducto = IdProducto;
            //leer.DataSetClase = Consultas.Productos_Imagenes(sIdProducto, "FrmProducto_KarruselImagenes"); 

            bHabilitarCapturaDeImeges = DtGeneral.ModuloEnEjecucion == TipoModulo.Regional;
            btnCargarImagenes.Enabled = false;
            btnCargarImagenes.Visible = bHabilitarCapturaDeImeges;
            separadorCargarImagenes.Visible = bHabilitarCapturaDeImeges;

            ////if (bHabilitarCapturaDeImeges)
            ////{
            ////    this.Text = "Captura de imagenes de producto";
            ////}

            if (!Directory.Exists(sDirectorioDeImagenes))
            {
                Directory.CreateDirectory(sDirectorioDeImagenes); 
            }
        }

        #region Form 
        private void FrmProducto_KarruselImagenes_Load(object sender, EventArgs e)
        {
            InicializaPantalla();
        }

        private void FrmProducto_KarruselImagenes_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Imagen_Atras();
                    break; 

                case Keys.Right:
                    Imagen_Adelante(); 
                    break; 
            }
        }
        #endregion Form

        #region Botones 
        private void InicializaPantalla()
        {
            Fg.IniciaControles();
            picImagen.Image = null;
            picImagen.Refresh();
            lblNombreImagen.Text = "";
            lblNdeM.Text = "";

            InicializarToolbar(false); 

            txtCodigoEAN.Focus();

            if (bEsConsultaExterna)
            {
                txtCodigoEAN.Text = sCodigoEAN;
                txtCodigoEAN_Validating(txtCodigoEAN, null); 
            }
        }

        private void InicializarToolbar(bool Imagenes) 
        {
            btnImg_Adelante.Enabled = Imagenes;
            btnImg_Atras.Enabled = Imagenes;
            btnCargarImagenes.Enabled = Imagenes;
            btnRotarDerecha.Enabled = Imagenes;
            btnRotarIzquierda.Enabled = Imagenes; 

            if (!bHabilitarCapturaDeImeges)
            {
                btnCargarImagenes.Enabled = false;
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializaPantalla(); 
        }

        private void btnCargarImagenes_Click(object sender, EventArgs e)
        {
            FrmProductos_AgregarImagenes f = new FrmProductos_AgregarImagenes(sIdProducto, txtCodigoEAN.Text);
            f.ShowDialog(); 
        }

        private void btnRotarIzquierda_Click(object sender, EventArgs e)
        {
            Image img = picImagen.Image;
            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            picImagen.Image = img;
        }

        private void btnRotarDerecha_Click(object sender, EventArgs e)
        {
            Image img = picImagen.Image;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            picImagen.Image = img;
        }
        #endregion Botones  

        #region Funciones y Procedimientos Publicos 
        public void MostrarPantalla(string IdProducto, string CodigoEAN)
        {
            sIdProducto = IdProducto;
            sCodigoEAN = CodigoEAN;
            bEsConsultaExterna = true; 

            this.ShowDialog();
        }
        #endregion Funciones y Procedimientos Publicos

        #region Imagenes
        private bool ThumbnailCallback()
        {
            return false;
        }

        private void CargarImagen(string NombreImagen, string SourceFile)
        {
            lblNombreImagen.Text = NombreImagen;

            try
            {
                string RutaImagen = sDirectorioDeImagenes + @"\" + SourceFile;
                IntPtr intr = new IntPtr(0);
                Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

                ////picImagen.Image = null;
                ////MemoryStream ms = new MemoryStream(bytes);
                ////Image returnImage = Image.FromStream(ms);

                ////picImagen.Image = returnImage;

                ////picImagen.Image = picImagen.Image.GetThumbnailImage(picImagen.Width, picImagen.Height, myCallback, intr);

                picImagen.Tag = RutaImagen; 
                picImagen.Image = Image.FromFile(RutaImagen);
                //picImagen.Image = picImagen.Image.GetThumbnailImage(picImagen.Width, picImagen.Height, myCallback, intr);

                picImagen.Image = PictureBoxZoom(picImagen.Image); 
            }
            catch (Exception ex)
            {
            }
        }

        public Image PictureBoxZoom(Image imgP)
        {
            Bitmap bm = new Bitmap(imgP, imgP.Width, imgP.Height);

            try
            {

                int dw = imgP.Width;
                int dh = imgP.Height;
                int tw = 180;
                int th = 180;
                double zw = (tw / (double)dw);
                double zh = (th / (double)dh);
                double z = (zw <= zh) ? zw : zh;
                dw = (int)(dw * z);
                dh = (int)(dh * z);

                dw = picImagen.Width;
                dh = picImagen.Height;

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

        private void CargarImagen(string NombreImagen, byte[] bytes)
        {
            lblNombreImagen.Text = NombreImagen;

            try
            {
                IntPtr intr = new IntPtr(0);
                Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

                picImagen.Image = null;
                MemoryStream ms = new MemoryStream(bytes);
                Image returnImage = Image.FromStream(ms);

                picImagen.Image = returnImage;

                picImagen.Image = picImagen.Image.GetThumbnailImage(picImagen.Width, picImagen.Height, myCallback, intr);
            }
            catch (Exception ex)
            { 
            }
        }

        private void MostrarImagen()
        {
            leerImagenes.Leer();
            lblNdeM.Text = string.Format("Imagen {0} de {1} ", leerImagenes.RegistroActual, leerImagenes.Registros);

            string sNombreArchivo = string.Format("{0}_{1}___{2}_{3}",
                    sIdProducto, txtCodigoEAN.Text,
                    Fg.PonCeros(leerImagenes.Campo("Id Imagen"), 4),
                    leerImagenes.Campo("Nombre"));

            CargarImagen(leerImagenes.Campo("Nombre"), sNombreArchivo); 

        }

        private void btnImg_Atras_Click(object sender, EventArgs e)
        {
            Imagen_Adelante(); 
        }
    
        private void Imagen_Atras() 
        {
            if (leerImagenes.Registros > 0)
            {
                if ((leerImagenes.RegistroActual - 1) < 1)
                {
                    ////Si la posicion actual menos uno es menor que cero entonces se pone en la ultima posicion. Cola Circular
                    leerImagenes.RegistroActual = leerImagenes.Registros;
                }
                else
                {
                    leer.RegistroActual = leer.RegistroActual - 1;
                }
                MostrarImagen(); 
            }
        }

        private void btnImg_Adelante_Click(object sender, EventArgs e)
        {
            Imagen_Adelante(); 
        }

        private void Imagen_Adelante()
        {
            if (leerImagenes.Registros > 0)
            {
                if ((leerImagenes.RegistroActual + 1) > leerImagenes.Registros)
                {
                    ////Si la posicion actual mas uno sobrepasa el total de registros entonces se pone en la primer posicion. Cola Circular
                    leerImagenes.RegistroActual = 1;
                }
                else
                {
                    leerImagenes.RegistroActual = leerImagenes.RegistroActual + 1;
                }
                MostrarImagen(); 
            }
        }
        #endregion Imagenes

        #region Controles 
        private void txtCodigoEAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Productos_CodigoEAN_Imagenes("");
                if (leer.Leer())
                {
                    txtCodigoEAN.Enabled = false;
                    txtCodigoEAN.Text = leer.Campo("CodigoEAN");

                    chkCodigoEAN.Enabled = false;
                    chkDescomponer.Enabled = false;
                    chkEsSectorSalud.Enabled = false;
                    chkMedicamento.Enabled = false;

                    sIdProducto = leer.Campo("IdProducto");

                    leer.DataSetClase = Consultas.Productos_CodigosEAN_Datos(txtCodigoEAN.Text, sIdProducto, "txtCodigoEAN_Validating");
                    if (leer.Leer())
                    {
                        CargaDatos();
                    }

                    InicializarToolbar(true);
                }
            }
        }

        private void txtCodigoEAN_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodigoEAN.Text.Trim() != "")
            {
                string sSql = string.Format(" Select IdProducto, CodigoEAN, CodigoEAN_Interno " +
                    " From CatProductos_CodigosRelacionados (NoLock) " + 
                    " Where CodigoEAN = '{0}' and Status = 'A' ", txtCodigoEAN.Text);
               
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtCodigoEAN_Validating");
                    General.msjError("Ocurrió un error al válidar el CodigoEAN");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtCodigoEAN.Enabled = false;
                        chkCodigoEAN.Enabled = false;
                        chkDescomponer.Enabled = false;
                        chkEsSectorSalud.Enabled = false;
                        chkMedicamento.Enabled = false; 

                        sIdProducto = leer.Campo("IdProducto");

                        leer.DataSetClase = Consultas.Productos_CodigosEAN_Datos(txtCodigoEAN.Text, sIdProducto, "txtCodigoEAN_Validating"); 
                        if (leer.Leer())
                        {
                            CargaDatos(); 
                        }

                        InicializarToolbar(true); 
                    }
                    else
                    {
                        General.msjUser("Código EAN no encontrado, verifique.");
                        if (!bEsConsultaExterna)
                        {
                            e.Cancel = true;
                        }
                        txtCodigoEAN.SelectAll();
                    }
                }
            }
        }

        private void CargarImagenes()
        {
            string sSql = "";
            sSql = string.Format(" Select IdProducto, CodigoEAN, 'Id Imagen' = Consecutivo, Nombre = NombreImagen, 'Imagen' = Imagen, " +
                                " ( Select Min(Consecutivo) From CatProductos_Imagenes Where IdProducto = '{0}' and CodigoEAN = '{1}' ) as Minimo, " +
                                " ( Select Max(Consecutivo) From CatProductos_Imagenes Where IdProducto = '{0}' and CodigoEAN = '{1}' ) as Maximo " +
                                " From CatProductos_Imagenes (Nolock)  " +
                                " Where IdProducto = '{0}' and CodigoEAN = '{1}' " +
                                " Order By Consecutivo ", sIdProducto, txtCodigoEAN.Text);

            sSql = string.Format(" Select IdProducto, CodigoEAN, 'Id Imagen' = Consecutivo, Nombre = NombreImagen, 'Imagen' = Imagen " +
                                " From CatProductos_Imagenes (Nolock)  " +
                                " Where IdProducto = '{0}' and CodigoEAN = '{1}' and Status = 'A' " +
                                " Order By Consecutivo ", sIdProducto, txtCodigoEAN.Text);

            if (!leerImagenes.Exec(sSql))
            {
                Error.GrabarError(leerImagenes, "CargarImagenes()");
                General.msjError("Ocurrió un error al obtener las imagenes del producto.");
            }
            else
            {
                if (leerImagenes.Registros == 0)
                {
                    lblNdeM.Visible = false;
                    lblNombreImagen.Text = "Producto sin imagenes asociadas.";
                    btnImg_Atras.Enabled = false;
                    btnImg_Adelante.Enabled = false;
                }
                else
                {
                    DescargarImagenes(); 
                    btnImg_Adelante_Click(null, null);
                }
            }
        }

        private void DescargarImagenes()
        {
            string sNombreArchivo = ""; 

            string sSql = string.Format(" Select IdProducto, CodigoEAN, 'Id Imagen' = Consecutivo, Nombre = NombreImagen, 'Imagen' = Imagen " +
                " From CatProductos_Imagenes (Nolock)  " +
                " Where IdProducto = '{0}' and CodigoEAN = '{1}' " +
                " Order By Consecutivo ", sIdProducto, txtCodigoEAN.Text);


            leerImagenes.RegistroActual = 1;
            while (leerImagenes.Leer())
            {
                sNombreArchivo = string.Format("{0}_{1}___{2}_{3}", 
                    sIdProducto, txtCodigoEAN.Text,
                    Fg.PonCeros(leerImagenes.Campo("Id Imagen"), 4), 
                    leerImagenes.Campo("Nombre"));

                if (!Fg.ConvertirBytesEnArchivo(sNombreArchivo, sDirectorioDeImagenes, leerImagenes.CampoByte("Imagen"), true))
                {
                    Fg.ConvertirStringB64EnArchivo(sNombreArchivo, sDirectorioDeImagenes, leerImagenes.Campo("Imagen"), true);
                }
            }
        }

        private void CargaDatos()
        {
            //////Se hace de esta manera para la ayuda.
            //bInformacionGuardada = true;
            //txtId.Enabled = false;
            //txtClaveInternaSal.Enabled = false;     // Una vez asignada la sal no es posible cambiarla 


            //txtId.Text = leer.Campo("IdProducto");
            txtClaveInternaSal.Text = leer.Campo("IdClaveSSA_Sal");
            lblClaveSal.Text = leer.Campo("ClaveSSA");
            lblDescripcionSal.Text = leer.Campo("DescripcionSal");

            lblTipoDeProducto.Text = leer.Campo("TipoDeProducto");
            txtDescripcion.Text = leer.Campo("Descripcion");
            txtDescripcionCorta.Text = leer.Campo("DescripcionCorta");
            lblClasificacionSSA.Text = leer.Campo("Clasificacion");
            lblFamilia.Text = leer.Campo("Familia");
            lblSubFamilia.Text = leer.Campo("SubFamilia");
            lblSegmento.Text = leer.Campo("Segmento");
            lblLaboratorio.Text = leer.Campo("Laboratorio");
            lblPresentacion.Text = leer.Campo("Presentacion");

            txtContenido.Text = leer.CampoInt("ContenidoPaquete").ToString();

            chkMedicamento.Checked = leer.CampoBool("EsControlado");
            chkCodigoEAN.Checked = leer.CampoBool("ManejaCodigosEAN"); // No es modificable // 2K90906-1220 
            chkEsSectorSalud.Checked = leer.CampoBool("EsSectorSalud");


            if (leer.CampoBool("Descomponer"))
            {
                chkDescomponer.Checked = true;
                chkDescomponer.Enabled = false;
            }

            if (leer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                lblCancelado.Text = "CANCELADO";
            }

            Application.DoEvents();
            this.Refresh();
            CargarImagenes(); 

        }
        #endregion Controlees

        private void picImagen_Click(object sender, EventArgs e)
        {
        }

        private void picImagen_DoubleClick(object sender, EventArgs e)
        {
            FrmZoom f = new FrmZoom((string)(((System.Windows.Forms.PictureBox)sender).Tag));
            f.ShowDialog();
        }

        private void btnMostrarTodasLasImagenes_Click(object sender, EventArgs e)
        {
            FrmProductos_AgregarImagenes f = new FrmProductos_AgregarImagenes(sIdProducto, txtCodigoEAN.Text, leerImagenes);
            f.ShowDialog(); 
        }

        private void btnDirectorioDeImagenes_Click(object sender, EventArgs e)
        {
            General.AbrirDirectorio(sDirectorioDeImagenes); 
        }
    }
}
