using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft; 

namespace OficinaCentral.Catalogos.Productos
{
    public partial class FrmProductos_Imagenes : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerImagen;
        clsLeer leer2;

        string sIdProducto = "";
        string sEAN = "";
        string sIdPersonal = DtGeneral.IdPersonal;

        int iMin = 0;
        int iMax = 0;
        int iCont_Imagen = 0;

        #region Imagenes
        OpenFileDialog file = new OpenFileDialog();
        FolderBrowserDialog Folder = new FolderBrowserDialog();

        string sImagen = "";
        string sNombreImagen = "";
        string sNomImagen = "";
        #endregion Imagenes

        bool bUpdate = false; 

        ////public enum FormatosImagen
        ////{
        ////    Ninguno = 0, Jpeg = 1, Bmp = 2, Gif = 3, Png = 4
        ////}

        public FrmProductos_Imagenes()
        {
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            leerImagen = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
        }

        private void FrmProductos_Imagenes_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            iMin = 0;
            iMax = 0;
            iCont_Imagen = 0;
            Habilitar_Botones(false, false);
            bUpdate = false;
            btnUpdateImagen.Visible = false;
            btnUpdateImagen.Enabled = false;
            Buscar_Producto();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = true;

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                bContinua = GuardaImagen();

                if (bContinua)
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("La Información se guardo satisfactoriamente."); 
                    btnNuevo_Click(null, null);
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnGuardar_Click");
                    General.msjError("Ocurrió un error al guardar la información.");                    
                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso(General.MsjErrorAbrirConexion);
            }
        }
        #endregion Botones

        #region Mostrar_Pantalla
        public void MostrarPantalla(string IdProducto, string EAN)
        {
            this.sIdProducto = IdProducto;
            this.sEAN = EAN;

            this.ShowDialog();
        }

        private void Buscar_Producto()
        {
            string sSql = "";

            sSql = string.Format(" Select * From vw_Productos_CodigoEAN  " +
                                    " Where IdProducto = '{0}' and CodigoEAN = '{1}' ", sIdProducto, sEAN);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Buscar_Producto()");
                General.msjError("Ocurrio un error al buscar el Producto.");
            }
            else
            {
                if (leer.Leer())
                {
                    lblIdProducto.Text = leer.Campo("IdProducto");
                    lblEAN.Text = leer.Campo("CodigoEAN");
                    lblDesProducto.Text = leer.Campo("Descripcion");

                    Cargar_Imagen_Productos();
                }
            }
        }

        private void Cargar_Imagen_Productos()
        {
            string sSql = "";
            byte[] bytes;

            iMin = 0;
            iMax = 0;

            sSql = string.Format(" Select IdProducto, CodigoEAN, Consecutivo, NombreImagen, Imagen, " +
                                " ( Select Min(Consecutivo) From CatProductos_Imagenes Where IdProducto = '{0}' and CodigoEAN = '{1}' ) as Minimo, " +
                                " ( Select Max(Consecutivo) From CatProductos_Imagenes Where IdProducto = '{0}' and CodigoEAN = '{1}' ) as Maximo " +
                                " From CatProductos_Imagenes (Nolock)  " +
                                " Where IdProducto = '{0}' and CodigoEAN = '{1}' " +
                                " Order By Consecutivo ", sIdProducto, sEAN);

            if (!leerImagen.Exec(sSql))
            {
                Error.GrabarError(leerImagen, "Cargar_Imagen_Productos()");
                General.msjError("Ocurrio un error al buscar las Imagenes del Producto.");
            }
            else
            {
                if (leerImagen.Leer())
                {
                    btnGuardar.Enabled = true;

                    btnUpdateImagen.Visible = true;
                    btnUpdateImagen.Enabled = true;

                    iCont_Imagen = leerImagen.CampoInt("Minimo");
                    iMin = leerImagen.CampoInt("Minimo");
                    iMax = leerImagen.CampoInt("Maximo");
                    bytes = leerImagen.CampoByte("Imagen");
                    CargarImagen(bytes);

                    if (iMin == iMax)
                    {
                        Habilitar_Botones(false, false);
                    }
                    else
                    {
                        Habilitar_Botones(false, true);
                    }

                    if (leerImagen.CampoInt("Maximo") >= 4)
                    {
                        btnGuardar.Enabled = false;
                    }
                }
                else
                {
                    General.msjAviso("No se encontraron imagenes para el producto : " + sIdProducto );
                }
            }
        }

        private void Habilitar_Botones(bool Anterior, bool Siguiente)
        {
            btnAnterior.Enabled = Anterior;
            btnAnterior.Visible = Anterior;
            btnSiguiente.Enabled = Siguiente;
            btnSiguiente.Visible = Siguiente;
        }

        private void CargarImagen(byte[] bytes)
        {
            IntPtr intr = new IntPtr(0);

            MemoryStream ms = new MemoryStream(bytes);
            Image returnImage = Image.FromStream(ms);
            pbImagen.Image = returnImage;

        }
        #endregion Mostrar_Pantalla

        #region Botones_Ant--Sig
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            byte[] bytes;
            string sFiltro = "";
            iCont_Imagen--;

            leer2 = new clsLeer(ref cnn);

            sFiltro = string.Format(" Consecutivo = {0} ", iCont_Imagen);

            if (iCont_Imagen == iMin)
            {
                Habilitar_Botones(false, true);
            }
            else
            {
                Habilitar_Botones(true, true);
            }

            leer2.DataRowsClase = leerImagen.Tabla(1).Select(sFiltro);

            if (leer2.Leer())
            {
                bytes = leer2.CampoByte("Imagen");
                CargarImagen(bytes);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            byte[] bytes;
            string sFiltro = "";
            iCont_Imagen++;

            leer2 = new clsLeer(ref cnn);

            sFiltro = string.Format(" Consecutivo = {0} ", iCont_Imagen);

            if (iCont_Imagen == iMax)
            {
                Habilitar_Botones(true, false);
            }
            else
            {
                Habilitar_Botones(true, true);
            }

            leer2.DataRowsClase = leerImagen.Tabla(1).Select(sFiltro);

            if (leer2.Leer())
            {
                bytes = leer2.CampoByte("Imagen");
                CargarImagen(bytes);
            }
        }
        #endregion Botones_Ant--Sig

        #region Agregar_Imagen
        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            FormatosImagen formato = FormatosImagen.Ninguno;
            byte[] byteimagen;
            string sFiltro = "(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            sImagen = "";
            sNombreImagen = "";

            file = new OpenFileDialog();
            file.Multiselect = false;
            file.Title = "Seleccione la Imagen a cargar";
            file.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            file.Filter = sFiltro;

            if (file.ShowDialog() == DialogResult.OK)
            {
                FileInfo imagen = new FileInfo(file.FileName);
                sNombreImagen = imagen.Name;
                formato = (FormatosImagen)file.FilterIndex;

                MostrarImagen(file.FileName);

                byteimagen = ConvertirImagenABytes(pbImagen.Image, formato);

                sImagen = Convert.ToBase64String(byteimagen, 0, byteimagen.Length);

                btnGuardar.Enabled = true;

                Habilitar_Botones(false, false);
            }
        }

        private void MostrarImagen(string RutaImagen)
        {
            IntPtr intr = new IntPtr(0);
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                       
            pbImagen.Image = Image.FromFile(RutaImagen);

            pbImagen.Image = pbImagen.Image.GetThumbnailImage(pbImagen.Width, pbImagen.Height, myCallback, intr);
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
        #endregion Agregar_Imagen

        #region Guardar_Imagen
        private bool GuardaImagen()
        {
            bool bRegresa = true;
            string sSql = "";
            string sConsecutivo = "*";

            if (bUpdate)
            {
                sConsecutivo = iCont_Imagen.ToString();
            }

            sSql = string.Format(" Exec spp_Mtto_CatProductos_Imagenes  " + 
                " @IdProducto = '{0}', @CodigoEAN = '{1}', @Consecutivo = '{2}', @NombreImagen = '{3}', @Imagen = '{4}', @IdPersonal = '{5}' ",
                sIdProducto, sEAN, sConsecutivo, sNombreImagen, sImagen, sIdPersonal);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;                
            }

            return bRegresa;
        }
        #endregion Guardar_Imagen

        #region Actualizar_Imagen_Producto
        private void btnUpdateImagen_Click(object sender, EventArgs e)
        {
            FormatosImagen formato = FormatosImagen.Ninguno;
            byte[] byteimagen;
            string sFiltro = "(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            sImagen = "";
            sNombreImagen = "";

            file = new OpenFileDialog();
            file.Multiselect = false;
            file.Title = "Seleccione la Imagen a cargar";
            file.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            file.Filter = sFiltro;

            if (file.ShowDialog() == DialogResult.OK)
            {
                FileInfo imagen = new FileInfo(file.FileName);
                sNombreImagen = imagen.Name;
                formato = (FormatosImagen)file.FilterIndex;

                MostrarImagen(file.FileName);

                byteimagen = ConvertirImagenABytes(pbImagen.Image, formato);

                sImagen = Convert.ToBase64String(byteimagen, 0, byteimagen.Length);

                Actualizar_Imagen();
            }
        }

        private void Actualizar_Imagen()
        {
            bUpdate = true;

            bool bContinua = true;

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                bContinua = GuardaImagen();

                if (bContinua)
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("La Información se actualizo satisfactoriamente."); 
                    btnNuevo_Click(null, null);
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "Actualizar_Imagen");
                    General.msjError("Ocurrió un error al guardar la información.");
                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso(General.MsjErrorAbrirConexion);
            }
        }
        #endregion Actualizar_Imagen_Producto
    }
}
