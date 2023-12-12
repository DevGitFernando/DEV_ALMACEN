using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.IO;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.QRCode;
using SC_SolutionsSystem.QRCode.Codec;

using DllFarmaciaSoft;
using Farmacia; 
using Farmacia.Ventas;
using Farmacia.Digitalizacion; 

namespace Farmacia.Catalogos
{
    public partial class FrmBeneficiarios_Identificacion : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        string sIdCliente = "";
        string sIdSubCliente = "";
        string sIdBeneficiario = "";

        int iIdImagen = 0;
        private byte[] bufferImagen = null;
        public bool bImagenDigitalizada = false;
        public Image imgDigitizacion;
        public string sImg_Original_01_Frontal = "";
        public string sImg_Comprimida_01_Frontal = "";

        public string sImg_Original_02_Reverso = "";
        public string sImg_Comprimida_02_Reverso = "";


        string sNombreCamara = "CamaraDigitalizacion";
        SC_SolutionsSystem.QRCode.Cam_Reader reader;
        bool bExisteLector = false; //DtGeneral.Camaras.ExisteCamara("CamaraDigitalizacion");
        bool bExisteDB_Digitalizacion = false;

        Dictionary<int, ImagenDigitalizada> listaDeImagenes = new Dictionary<int, ImagenDigitalizada>();
        bool bImplementaDigitalizacionDepurar = GnFarmacia.ImplementaDigitalizacionDepurarDirectorio;
        string sDirectorio = Application.StartupPath + @"\Digitalizacion";

        string sQuery_Actualizacion_Identificacion = ""; 

        public FrmBeneficiarios_Identificacion()
        {
            InitializeComponent();
            GetCamaras();

            leer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            chkHabilitarZoom.BackColor = General.BackColorBarraMenu; 
            panel_01_Frontal.Dock = DockStyle.Fill;
            panel_02_Reverso.Dock = DockStyle.Fill;



            //panel_01_Frontal.SizeToFit = true;
            //panel_02_Reverso.SizeToFit = true;
            panel_01_Frontal.GridDisplayMode = SC_ControlsCS.ImageBoxGridDisplayMode.Image;
            panel_02_Reverso.GridDisplayMode = SC_ControlsCS.ImageBoxGridDisplayMode.Image;

            panel_01_Frontal.Zoom = 40;
            panel_02_Reverso.Zoom = 40;

            panel_01_Frontal.ZoomIncrement = 10;
            panel_02_Reverso.ZoomIncrement = 10;

            ////panel_01_Frontal.SizeMode = PictureBoxSizeMode.Zoom;
            ////panel_02_Reverso.SizeMode = PictureBoxSizeMode.Zoom;

            //////sIdCliente = "0002";
            //////sIdSubCliente = "0006";
            //////sIdBeneficiario = "00000004"; 
        }

        #region Form 
        public string Query_Actualizacion_Identificacion
        {
            get { return sQuery_Actualizacion_Identificacion; } 
        }

        private void FrmBeneficiarios_Identificacion_Load( object sender, EventArgs e )
        {
            IniciarPantalla();
            GetInformacion_Beneficiario();
        }

        private void GetCamaras()
        {
            SC_SolutionsSystem.SistemaOperativo.clsCamaras camaras = new SC_SolutionsSystem.SistemaOperativo.clsCamaras();

            cboCamaras.Clear();
            cboCamaras.Add("0", "<< Camaras >>");

            foreach(string camara in camaras.Camaras)
            {
                cboCamaras.Add(camara, camara);
            }


            cboCamaras.SelectedIndex = 0;
            if(cboCamaras.NumeroDeItems == 2)
            {
                cboCamaras.SelectedIndex = 1;
                cboCamaras.Enabled = false;
                sNombreCamara = cboCamaras.Data; 
            }
        }
        #endregion Form

        #region Botones
        private void IniciarPantalla()
        {
            panel_01_Frontal.Image = null;
            panel_02_Reverso.Image = null; 
            Fg.IniciaControles();

            chkHabilitarZoom.Checked = false; 

            if(cboCamaras.NumeroDeItems == 2)
            {
                cboCamaras.SelectedIndex = 1;
                cboCamaras.Enabled = false;
                sNombreCamara = cboCamaras.Data;
            }
        }
        private void btnNuevo_Click( object sender, EventArgs e )
        {
            IniciarPantalla(); 
        }

        private void btnGuardar_Click( object sender, EventArgs e ) 
        {
            sQuery_Actualizacion_Identificacion = String.Format("Exec spp_Mtto_CatBeneficiarios_Identificacion_Identificacion \n" +
                " @IdEstado = '{0}', @IdFarmacia = '{1}', @IdCliente = '{2}', @IdSubCliente = '{3}', @IdBeneficiario = '{4}', @IMG_01_Frontal = '{5}', @IMG_02_Reverso = '{6}' ",
                sEstado, sFarmacia, sIdCliente, sIdSubCliente, sIdBeneficiario, sImg_Comprimida_01_Frontal, sImg_Comprimida_02_Reverso
                );

            if(sIdBeneficiario != "*")
            {
                sQuery_Actualizacion_Identificacion = "";
                if(GuardarInformacion())
                {
                    this.Hide();  
                }
            }
            else
            {
                this.Hide(); 
            }
        }

        private bool GuardarInformacion()
        {
            bool bRegresa = false; 
            string sSql = "", sMensaje = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion


            if(!ConexionLocal.Abrir())
            {
                Error.LogError(ConexionLocal.MensajeError);
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                ConexionLocal.IniciarTransaccion();

                sSql = String.Format("Exec spp_Mtto_CatBeneficiarios_Identificacion_Identificacion \n" +
                    " @IdEstado = '{0}', @IdFarmacia = '{1}', @IdCliente = '{2}', @IdSubCliente = '{3}', @IdBeneficiario = '{4}', @IMG_01_Frontal = '{5}', @IMG_02_Reverso = '{6}' ",
                    sEstado, sFarmacia, sIdCliente, sIdSubCliente, sIdBeneficiario, sImg_Comprimida_01_Frontal, sImg_Comprimida_02_Reverso
                    );

                if(leer.Exec(sSql))
                {
                    if(leer.Leer())
                    {
                        sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                    }

                    ConexionLocal.CompletarTransaccion();
                    bRegresa = true;
                    General.msjUser("Información guardada satisfactoriamente"); //Este mensaje lo genera el SP
                    btnNuevo_Click(null, null);
                }
                else
                {
                    ConexionLocal.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnGuardar_Click");
                    General.msjError("Ocurrió un error al guardar la información.");
                    //btnNuevo_Click(null, null);

                }

                ConexionLocal.Cerrar();
            }

            return bRegresa; 
        }

        private void btnCancelar_Click( object sender, EventArgs e )
        {

        }
        #endregion Botones

        #region Funciones y Procedimientos Publicos 
        public void Mostrar_Identificacion( string IdEstado, string IdFarmacia, string IdCliente, string IdSubCliente, string IdBeneficiario )
        {
            sEstado = IdEstado;
            sFarmacia = IdFarmacia;
            sIdCliente = IdCliente;
            sIdSubCliente = IdSubCliente;
            sIdBeneficiario = IdBeneficiario;

            this.ShowInTaskbar = false;
            this.ShowDialog();
        }
        #endregion Funciones y Procedimientos Publicos

        #region Botones 
        private void chkHabilitarZoom_CheckedChanged( object sender, EventArgs e )
        {
            panel_01_Frontal.SizeToFit = !chkHabilitarZoom.Checked;
            panel_02_Reverso.SizeToFit = !chkHabilitarZoom.Checked;

            //chkHabilitarZoom.Text = chkHabilitarZoom.Checked ? "" : "";
        }

        private void btnDigitalizar_01_Frente_Click( object sender, EventArgs e )
        {
            if(validar_CamaraSeleccionada())
            {
                Digitalizar(0, 1);
            }
        }

        private void btnDigitalizar_02_Reverso_Click( object sender, EventArgs e )
        {
            if(validar_CamaraSeleccionada())
            {
                Digitalizar(0, 2);
            }
        }

        private bool validar_CamaraSeleccionada()
        {
            bool bRegresa = true;
            sNombreCamara = ""; 

            if(cboCamaras.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjError("No ha seleccionado un Dispositivo de digitalización."); 
            }

            sNombreCamara = cboCamaras.Data; 

            return bRegresa; 
        }
        #endregion Botones 

        #region Funciones y Procedimientos Privados 
        private void GetInformacion_Beneficiario()
        {
            string sSql = string.Format("Select * "+
                "From CatBeneficiarios_Identificacion (NoLock) \n" + 
                "Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdCliente = '{2}' and IdSubCliente = '{3}' and IdBeneficiario = '{4}' ", 
                sEstado, sFarmacia, sIdCliente, sIdSubCliente, sIdBeneficiario 
                );


            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener los documentos del beneficiario.");
            }
            else
            {
                if(leer.Leer())
                {
                    sImg_Comprimida_01_Frontal = leer.Campo("IMG_01_Frontal");
                    sImg_Comprimida_02_Reverso = leer.Campo("IMG_02_Reverso");


                    MemoryStream ms = new MemoryStream(leer.CampoByte("IMG_01_Frontal"));
                    //Image returnImage = Image.FromStream(ms);

                    panel_01_Frontal.Image = Image.FromStream(new MemoryStream(leer.CampoByte("IMG_01_Frontal"))); // leer.CampoImagen("IMG_01_Frontal");
                    panel_02_Reverso.Image = Image.FromStream(new MemoryStream(leer.CampoByte("IMG_02_Reverso"))); // leer.CampoImagen("IMG_02_Reverso");


                    ////panel_01_Frontal.Image  = GetImagen(leer.CampoByte("IMG_01_Frontal"), panel_01_Frontal.Width, panel_01_Frontal.Height);
                    ////panel_02_Reverso.Image = GetImagen(leer.CampoByte("IMG_02_Reverso"), panel_02_Reverso.Width, panel_02_Reverso.Height);

                    ////panel_01_Frontal.Image = GetImagen(leer.CampoByte("IMG_01_Frontal"), 100, 75);
                    ////panel_02_Reverso.Image = GetImagen(leer.CampoByte("IMG_02_Reverso"), 100, 75);

                }
            }
        }

        #region Digitalizado
        private Image GetImagen( byte[] ImagenB64, int Ancho, int Alto )
        {
            Image img = null;
            string sFile = Application.StartupPath + @"\tmpIco.jpg";

            try
            {
                MemoryStream ms = new MemoryStream(ImagenB64);
                img = Image.FromStream(ms);

                Bitmap bitmap = new Bitmap(ms);
                bitmap.SetResolution(Ancho, Alto);
            }
            catch(Exception ex)
            {
            }

            return img;
        }

        private void VisualizarImagen( int Partida )
        {
            imgDigitizacion = null;
            if(listaDeImagenes.ContainsKey(Partida))
            {
                ImagenDigitalizada imgDig = new ImagenDigitalizada();
                imgDig = (ImagenDigitalizada)listaDeImagenes[Partida];

                imgDigitizacion = imgDig.Imagen;
                imgDigitizacion = Fg.ConvertirBase64ToImage(imgDig.Img_Original);


                //listaDeImagenes.Values
            }

            if(imgDigitizacion != null)
            {
                FrmVisorReceta visor = new FrmVisorReceta(imgDigitizacion, imgDigitizacion.Width, imgDigitizacion.Height);
                visor.ShowDialog();
            }
        }

        private void Remover( int Renglon )
        {
            bool bBorrar = false;

            try
            {
                if(listaDeImagenes.ContainsKey(Renglon))
                {
                    listaDeImagenes.Remove(Renglon);
                }

                bBorrar = true;
            }
            catch
            {
            }

            ////if(bBorrar)
            ////{
            ////    grid.DeleteRow(Renglon);
            ////}
        }

        private void Digitalizar( int Renglon, int TipoImagen )
        {
            int iCompresion = 0;
            int iPartida = Renglon;
            Image imgDigitizacion_Aux = null;
            string sFile_X = "";
            string sFile = "";
            string sEncode = "";
            Bitmap imgCamara = null;
            int imageQuality = 25; 

            bImagenDigitalizada = false;
            imgDigitizacion = null;




            reader = new SC_SolutionsSystem.QRCode.Cam_Reader();
            // reader.Camara = "Chicony USB 2.0 Camera";
            reader.Camara = sNombreCamara; // DtGeneral.Camaras.GetCamara("QReader");
            reader.Show();

            imgDigitizacion = null; 
            if(reader.ImagenDigitalizada)
            {
                bImagenDigitalizada = reader.ImagenDigitalizada;
                imgDigitizacion = reader.Imagen;

                using(System.Drawing.Image img = System.Drawing.Image.FromFile(reader.FileName))
                {
                    imgCamara = (Bitmap)img;

                    if(TipoImagen == 1)
                    {
                        sImg_Original_01_Frontal = "";
                        sImg_Comprimida_01_Frontal = "";


                        sImg_Original_01_Frontal = Fg.ConvertirArchivoEnStringB64(reader.FileName);
                        panel_01_Frontal.Image = System.Drawing.Image.FromFile(reader.FileName);

                        sEncode = "image/jpeg";
                        CompressImage(reader.FileName, imageQuality, reader.FileNameCompress, sEncode);

                        sImg_Comprimida_01_Frontal = Fg.ConvertirArchivoEnStringB64(reader.FileNameCompress);
                    }

                    if(TipoImagen == 2)
                    {
                        sImg_Original_02_Reverso = "";
                        sImg_Comprimida_02_Reverso = "";

                        
                        sImg_Original_02_Reverso = Fg.ConvertirArchivoEnStringB64(reader.FileName);
                        panel_02_Reverso.Image = System.Drawing.Image.FromFile(reader.FileName);

                        sEncode = "image/jpeg";
                        CompressImage(reader.FileName, imageQuality, reader.FileNameCompress, sEncode);

                        sImg_Comprimida_02_Reverso = Fg.ConvertirArchivoEnStringB64(reader.FileNameCompress);

                    }
                }

                //// Forzar que se tome la imagen generada desde la camara 
                imgDigitizacion = reader.Imagen;

                sFile_X = "";
            }
        }

        private Image CompressImage( string ImageSource, int imageQuality, string savePath, string Encode )
        {
            Image imgReturn = null;
            bool bRegresa = false;
            Image sourceImage = null;

            try
            {
                using(sourceImage = Image.FromFile(reader.FileName))
                {
                    ////Create an ImageCodecInfo-object for the codec information
                    ImageCodecInfo jpegCodec = null;

                    ////Set quality factor for compression
                    EncoderParameter imageQualitysParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, imageQuality);

                    ////List all avaible codecs (system wide)
                    ImageCodecInfo[] alleCodecs = ImageCodecInfo.GetImageEncoders();

                    EncoderParameters codecParameter = new EncoderParameters(1);
                    codecParameter.Param[0] = imageQualitysParameter;

                    ////Find and choose JPEG codec
                    for(int i = 0; i < alleCodecs.Length; i++)
                    {
                        if(alleCodecs[i].MimeType.ToUpper() == Encode.ToUpper()) //"image/jpeg")
                        {
                            jpegCodec = alleCodecs[i];
                            break;
                        }
                    }

                    ////Save compressed image
                    sourceImage.Save(savePath, jpegCodec, codecParameter);
                    bRegresa = true;
                }
            }
            catch(Exception e)
            {
            }


            return imgReturn;
        }

        private Image ComprimirImagen( Image ImagenDigitilizada )
        {
            Image imgReturn = null;
            IntPtr intr = new IntPtr(0);
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            PictureBox picture = new PictureBox();
            picture.Name = "picCompresion";
            picture.Width = 800;
            picture.Height = 600;

            imgReturn = PictureBoxZoom(ImagenDigitilizada, picture.Width, picture.Height);

            imgReturn.Save(reader.FileNameCompress, System.Drawing.Imaging.ImageFormat.Png);

            return imgReturn;
        }

        public Image PictureBoxZoom( Image imgP, int With, int Height )
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

        private bool ThumbnailCallback()
        {
            return false;
        }

        private byte[] ConvertirImagenABytes( System.Drawing.Image imageIn, FormatosImagen formato )
        {
            MemoryStream ms = new MemoryStream();

            //formato = FormatosImagen.Jpeg; 

            try
            {
                switch(formato)
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
            catch(Exception ex)
            {
                ex = null;
            }

            return ms.ToArray();
        }

        private Image CargarImagenDigitalizada( byte[] bytes, int Ancho, int Alto )
        {
            IntPtr intr = new IntPtr(0);

            MemoryStream ms = new MemoryStream(bytes);
            Image returnImage = Image.FromStream(ms);
            //pbReceta.Image = new Bitmap(returnImage, Ancho, Alto);

            if(Ancho != 0 && Alto != 0)
            {
                returnImage = new Bitmap(returnImage, Ancho, Alto);
            }
            else
            {
                returnImage = new Bitmap(returnImage);
            }

            return returnImage;

        }
        #endregion Digitalizado

        #endregion Funciones y Procedimientos Privados


    }
}
