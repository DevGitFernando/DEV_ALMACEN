using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FP;
using SC_SolutionsSystem.FP.Huellas;

using SC_SolutionsSystem.QRCode; 
using SC_SolutionsSystem.QRCode.Codec; 

using DllFarmaciaSoft;
using DllFarmaciaSoft.Ayudas;
using DllFarmaciaSoft.Usuarios_y_Permisos;

using Farmacia.Catalogos;
using Farmacia.VentasDispensacion;
using DllRecetaElectronica.ECE; 

namespace Farmacia.Ventas
{
    #region Form 
    public partial class FrmInformacionVentas : FrmBaseExt
    {
        #region Variables
        clsConexionSQL cnn; //= new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;
        DataSet dtsAreas;
        FrmHelpBeneficiarios helpBeneficiarios;

        public bool bBloquearControles = false; 

        public bool bCapturaCompleta = false;
        public bool bEsVale = false;
        public bool bVale_FolioVenta = false; 
        public bool bVigenciaValida = true;
        public bool bEsActivo = false;

        private string sIdEmpresa = DtGeneral.EmpresaConectada; 
        private string sIdEstado = "";
        private string sIdFarmacia = "";


        private bool bVentanaActiva = false; 
        public bool bCerrarInformacionAdicionalAutomaticamente = false;
        public bool bRecetaEDM;


        public bool bEsSeguroPopular = false;
        public bool bValidarBeneficioSeguroPopular = false; 
        public bool bPermitirCapturaBeneficiariosNuevos = false;
        public bool bPermitirImportarBeneficiarios = false; 
        public string sFolioVenta = "";
        public string sIdCliente = "";
        public string sIdClienteNombre = "";        
        public string sIdSubCliente = "";
        public string sIdSubClienteNombre = "";

        public bool bEsPedido = false;
        public string sIdBeneficiario = "";
        public string sNumReceta = "";
        public string sFechaReceta = ""; 
        public DateTime dtpFechaReceta = DateTime.Now;
        public string sIdTipoDispensacion = ""; 
        public string sIdMedico = "";
        public string sIdDiagnostico = "";
        public string sIdDiagnosticoClave = "";
        public string sIdBeneficioSeguroPopular = "";
        public string sNumeroDeHabitacion = "";
        public string sNumeroDeCama = "";
        public string sIdEstadoResidencia = "";
        public string sIdTipoDerechoHaciencia = "";

        public string sIdServicio = "";
        public string sIdArea = "";
        public string sReferenciaObserv = "";

        private byte[] bufferImagen = null;
        public bool bImagenDigitalizada = false;
        public Image imgDigitizacion;
        public string sImg_Original = "";
        public string sImg_Comprimida = ""; 


        string sIdUMedica_Base = "000000"; 
        string sCLUES_Base = "SSA000000";

        string sUrlChecador = "";
        DllFarmaciaSoft.wsFarmacia.wsCnnCliente validarHuella = null;
        private clsConexionSQL CnnHuellas = new clsConexionSQL(General.DatosConexion);
        clsLeer leerChecador;
        clsDatosConexion DatosDeConexion;
        string sPersonal = DtGeneral.IdPersonal;
        string sMsjNoEncontrado = "";
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        public string sCLUES_Foranea = "";

        private string sClaveDispensacionRecetasForaneas_Vales = GnFarmacia.ClaveDispensacionRecetasValesForaneos;
        private string sClaveDispensacionRecetasForaneas = GnFarmacia.ClaveDispensacionRecetasForaneas; 
        private string sClaveDispensacionUnidadesNoAdministradas = GnFarmacia.ClaveDispensacionUnidadesNoAdministradas;
        private bool bPermitirFechasRecetAñosAnteriores = false; //GnFarmacia.PermitirFechaRecetas_AñosAnteriores;
        private int iMesesFechasRecetaAñosAnteriores = 0; //GnFarmacia.MesesFechaRecetas_AñosAnteriores;
        private int iMesesAtras_FechaRecetas = GnFarmacia.MesesAtras_FechaRecetas; 
        private bool bValidarFoliosUnicosDeRecetas = GnFarmacia.ValidarFoliosDeRecetaUnicos;
        private bool bValidarBeneficariosAlertas = GnFarmacia.ValidarBeneficariosAlertas; 

        private int iMesesMargen = 0;
        string sNombreCamara = "CamaraDigitalizacion";
        ////SC_SolutionsSystem.QRCode.Cam_Reader reader; 
        bool bExisteLector = false; //DtGeneral.Camaras.ExisteCamara("CamaraDigitalizacion");
        #endregion Variables

        public FrmInformacionVentas()
        {
            InitializeComponent();
        }

        public FrmInformacionVentas(string IdEstado, string IdFarmacia, string IdCliente, string NombreCliente, string IdSubCliente, string NombreSubCliente)
        {
            InitializeComponent();

            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
            this.sIdCliente = IdCliente;
            this.sIdClienteNombre = NombreCliente;
            this.sIdSubCliente = IdSubCliente;
            this.sIdSubClienteNombre = NombreSubCliente;

            cnn = new clsConexionSQL(General.DatosConexion); 
            leer = new clsLeer(ref cnn);
            leerChecador = new clsLeer(ref CnnHuellas);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false); 
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false); 
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            validarHuella = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente();
            validarHuella.Url = General.Url;
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, DtGeneral.EmpresaConectada, IdEstado, IdFarmacia, sPersonal);
            ///

            if (DtGeneral.EsAlmacen)
            {
                label8.Text = "No. Documento : ";
                label9.Text = "Fecha Envío : ";
                label22.Text = "CLUES : ";
            }

        }

        private void FrmInformacionVentas_Load(object sender, EventArgs e)
        {
            ////if (!GnFarmacia.ImplementaDigitalizacion)
            ////{
            ////    btnDigitalizar.Enabled = false;
            ////    btnVisor.Enabled = false; 
            ////}
            ////else 
            ////{
            ////    bExisteLector = DtGeneral.Camaras.ExisteCamara(sNombreCamara);
            ////    btnDigitalizar.Enabled = bExisteLector;
            ////    btnVisor.Enabled = true; 
            ////    sNombreCamara = DtGeneral.Camaras.GetCamara(sNombreCamara);
            ////}


            //if (DtGeneral.ConfirmacionConHuellas)
            //{
            //    if (Obtener_Url_Firma())
            //    {
            //        if (validarDatosDeConexion())
            //        {
            //            ConexionChecador();
            //        }
            //    }
            //}

            lblFolioDeRecetaUnico.Visible = bValidarFoliosUnicosDeRecetas; 
            iMesesMargen = (-1 * 12); 
            CargarServicios(); 
            Fg.IniciaControles();

            btnIdentificacion.Enabled = false; 

            txtIdBenificiario.Text = sIdBeneficiario;
            txtIdBenificiario_Validating(null, null);

            sIdBeneficioSeguroPopular = sIdBeneficioSeguroPopular == "" ? GnFarmacia.ClaveBeneficio : sIdBeneficioSeguroPopular; 
            txtBeneficio.Text = sIdBeneficioSeguroPopular;
            txtBeneficio_Validating(null, null); 

            txtNumReceta.Text = sNumReceta;
            txtNumReceta.Enabled = !bRecetaEDM;
            dtpFechaDeReceta.Value = dtpFechaReceta;
            cboTipoDeSurtimiento.Data = sIdTipoDispensacion;

            txtUMedica.Enabled = false; 
            txtUMedica.Text = sCLUES_Foranea; 
            txtUMedica_Validating(null, null); 

            txtIdMedico.Text = sIdMedico;
            txtIdMedico_Validating(null, null);

            sIdDiagnostico = sIdDiagnostico == "" ? GnFarmacia.ClaveDiagnostico : sIdDiagnostico;
            txtIdDiagnostico.Text = sIdDiagnostico;
            txtIdDiagnostico_Validating(null, null);

            cboServicios.Data = sIdServicio;
            cboAreas.Data = sIdArea;

            txtRefObservaciones.Text = sReferenciaObserv;

            txtIdBenificiario.Focus();
            txtNumeroDeCama.Text = sNumeroDeCama;
            txtNumeroDeHabitacion.Text = sNumeroDeHabitacion;

            txtNumeroDeCama.Enabled = GnFarmacia.CapturarNumeroDeCama;
            txtNumeroDeHabitacion.Enabled = GnFarmacia.CapturarNumeroDeHabitacion;
           
            ///// Algunos Clientes-SubClientes pueden dar de alta Beneficiarios nuevos 
            btnRegistrarBeneficiarios.Enabled = bPermitirCapturaBeneficiariosNuevos;
            btnRegistrarBeneficiarios.Visible = bPermitirCapturaBeneficiariosNuevos;

            // Cargar la informacion Guardada 
            CargarInformacionAdicionalDeVentas();

            if (txtUMedica.Text.Trim() == "")
            {
                txtUMedica.Text = sIdUMedica_Base;
                lblUnidadMedica.Text = sCLUES_Base;
            }

            if (sFechaReceta != "")
            {
                try
                {
                    dtpFechaDeReceta.Text = sFechaReceta; 
                }
                catch { }
            }

            if(bEsPedido)
            {
                if(!GnFarmacia.Pedidos_ModificarInformacionAdicional)
                {
                    txtIdBenificiario.Enabled = false;
                    txtNumReceta.Enabled = false;
                    btnRegistrarBeneficiarios.Enabled = false;
                }
            }

            txtBeneficio.Enabled = false; 
            if (bBloquearControles)
            {
                txtIdBenificiario.Enabled = false;
                btnRegistrarBeneficiarios.Enabled = false; 
                txtNumReceta.Enabled = false;
                txtIdDiagnostico.Enabled = false;
                txtUMedica.Enabled = false;
                txtIdMedico.Enabled = false;
                btnRegistrarMedicos.Enabled = false;
                txtBeneficio.Enabled = false;
                dtpFechaDeReceta.Enabled = false;
                txtRefObservaciones.Text = ".";

                if(GnFarmacia.ImplementaInterfaceExpedienteElectronico)
                {
                    if(RecetaElectronica.Receta.InformacionCargada && RecetaElectronica.Receta.CapturaInformacion)
                    {
                        txtIdBenificiario.Enabled = true;
                        btnRegistrarBeneficiarios.Enabled = true;
                        txtNumReceta.Enabled = false;
                        txtIdDiagnostico.Enabled = true;
                        txtUMedica.Enabled = true;
                        txtIdMedico.Enabled = true;
                        btnRegistrarMedicos.Enabled = true;
                        txtBeneficio.Enabled = true;
                        dtpFechaDeReceta.Enabled = false;
                    }
                }
            }
        }

        #region Digitalizado 
        private void GetImagenDigitalizada()
        {
            string sSql = string.Format("Select  IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FechaDigitalizacion, ImagenComprimida, ImagenOriginal, Ancho, Alto " + 
                " From VentasDigitalizacion (NoLock) " + 
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioVenta = '{3}'  ", 
                sIdEmpresa, sIdEstado, sIdFarmacia, sFolioVenta);
            imgDigitizacion = null;

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al obtener la imagen digitalizada.");
            }
            else
            { 
                if ( leer.Leer())
                {
                    try
                    {
                        imgDigitizacion = CargarImagenDigitalizada(leer.CampoByte("ImagenComprimida"), leer.CampoInt("Ancho"), leer.CampoInt("Alto"));
                    }
                    catch 
                    { 
                    }
                }
            }
        }

        private void btnVisor_Click(object sender, EventArgs e)
        {
            if (sFolioVenta != "*")
            {
                GetImagenDigitalizada();
            }


            if (imgDigitizacion == null)
            {
                General.msjUser("No existe imagen de receta.");
            }
            else 
            {
                FrmVisorReceta visor = new FrmVisorReceta(imgDigitizacion, imgDigitizacion.Width, imgDigitizacion.Height);
                visor.ShowDialog();
            }
        }

        private void btnDigitalizar_Click(object sender, EventArgs e)
        {
            ////int iCompresion = 0;
            ////Image imgDigitizacion_Aux = null;
            ////string sFile_X = "";
            ////string sFile = "";
            ////string sEncode = ""; 
            ////bImagenDigitalizada = false;
            ////imgDigitizacion = null; 
            ////sImg_Original = "";
            ////sImg_Comprimida = "";

            ////reader = new SC_SolutionsSystem.QRCode.Cam_Reader();
            ////// reader.Camara = "Chicony USB 2.0 Camera";
            ////reader.Camara = sNombreCamara; // DtGeneral.Camaras.GetCamara("QReader");
            ////reader.Show();

            ////if (reader.ImagenDigitalizada)
            ////{
            ////    bImagenDigitalizada = reader.ImagenDigitalizada;
            ////    imgDigitizacion = reader.Imagen;
                
            ////    bufferImagen = ConvertirImagenABytes(imgDigitizacion, FormatosImagen.Png);
            ////    sImg_Original = Convert.ToBase64String(bufferImagen);
            ////    sFile_X = Fg.Left(reader.FileNameCompress, reader.FileNameCompress.Length - 4);


            ////    sEncode = "image/jpeg";
            ////    CompressImage(imgDigitizacion, 80, reader.FileNameCompress, sEncode);
            ////    //imgDigitizacion_Aux = Image.FromFile(reader.FileNameCompress); 

            ////    using (imgDigitizacion_Aux = Image.FromFile(reader.FileNameCompress))
            ////    {
            ////        //imgDigitizacion_Aux = img;
            ////    }
                

            ////    //bufferImagen = ConvertirImagenABytes(ComprimirImagen(imgDigitizacion), FormatosImagen.Png);
            ////    bufferImagen = ConvertirImagenABytes(imgDigitizacion_Aux, FormatosImagen.Png);
            ////    sImg_Comprimida = Convert.ToBase64String(bufferImagen);


            ////    //// Forzar que se tome la imagen generada desde la camara 
            ////    imgDigitizacion = reader.Imagen;


            ////    ////sEncode = "image/bmp";
            ////    ////for (int i = 1; i <= 10; i++)
            ////    ////{
            ////    ////    sFile = string.Format("{0}__01_{1}.bmp", sFile_X, Fg.PonCeros(i * 10, 3));
            ////    ////    CompressImage(imgDigitizacion, i * 10, sFile, sEncode);
            ////    ////}


            ////    //sFile = "";
            ////    //sEncode = "image/jpeg"; 
            ////    //for (int i = 1; i <= 10; i++)
            ////    //{
            ////    //    sFile = string.Format("{0}__{1}.jpeg", sFile_X, Fg.PonCeros(i * 10, 3));
            ////    //    CompressImage(imgDigitizacion, i * 10, sFile, sEncode);
            ////    //}



            ////    ////sFile = "";
            ////    ////sEncode = "image/tiff";
            ////    ////for (int i = 1; i <= 10; i++)
            ////    ////{
            ////    ////    sFile = string.Format("{0}__03_{1}.tiff", sFile_X, Fg.PonCeros(i * 10, 3));
            ////    ////    CompressImage(imgDigitizacion, i * 10, sFile, sEncode);
            ////    ////}


            ////    //for (int i = 1; i <= 10; i++)
            ////    //{
            ////    //    sFile = string.Format("{0}__01_{1}.png", sFile_X, Fg.PonCeros(i * 10, 3));
            ////    //    CompressImage(imgDigitizacion, i * 10, sFile, "image/jpeg");
            ////    //}

            ////    //for (int i = 1; i <= 10; i++)
            ////    //{
            ////    //    sFile = string.Format("{0}__01_{1}.png", sFile_X, Fg.PonCeros(i * 10, 3));
            ////    //    CompressImage(imgDigitizacion, i * 10, sFile, "image/jpeg");
            ////    //}

            ////    sFile_X = "";
            ////}
            
        }

        private Image CompressImage(Image sourceImage, int imageQuality, string savePath, string Encode)
        {
            Image imgReturn = null;
            bool bRegresa = false; 

            try
            {
                ////Create an ImageCodecInfo-object for the codec information
                ImageCodecInfo jpegCodec = null;

                ////Set quality factor for compression
                EncoderParameter imageQualitysParameter = new EncoderParameter(
                            System.Drawing.Imaging.Encoder.Quality, imageQuality);

                ////List all avaible codecs (system wide)
                ImageCodecInfo[] alleCodecs = ImageCodecInfo.GetImageEncoders();

                EncoderParameters codecParameter = new EncoderParameters(1);
                codecParameter.Param[0] = imageQualitysParameter;

                ////Find and choose JPEG codec
                for (int i = 0; i < alleCodecs.Length; i++)
                {
                    if (alleCodecs[i].MimeType.ToUpper() == Encode.ToUpper()) //"image/jpeg")
                    {
                        jpegCodec = alleCodecs[i];
                        break;
                    }
                }

                ////Save compressed image
                sourceImage.Save(savePath, jpegCodec, codecParameter);
                bRegresa = true;
            }
            catch (Exception e)
            {
            }

            ////if (bRegresa)
            ////{
            ////    using (Bitmap bmp = new Bitmap(savePath))
            ////    {
            ////        imgReturn = bmp;
            ////    }
            ////}

            return imgReturn; 
        }

        private Image ComprimirImagen(Image ImagenDigitilizada)
        {
            Image imgReturn = null;
            IntPtr intr = new IntPtr(0);
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            ////PictureBox picture = new PictureBox();
            ////picture.Name = "picCompresion";
            ////picture.Width = 800;
            ////picture.Height = 600;

            ////imgReturn = PictureBoxZoom(ImagenDigitilizada, picture.Width, picture.Height);

            ////imgReturn.Save(reader.FileNameCompress, System.Drawing.Imaging.ImageFormat.Png); 

            return imgReturn; 
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

        private bool ThumbnailCallback()
        {
            return false;
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

        private Image CargarImagenDigitalizada(byte[] bytes, int Ancho, int Alto)
        {
            IntPtr intr = new IntPtr(0);

            MemoryStream ms = new MemoryStream(bytes);
            Image returnImage = Image.FromStream(ms);
            //pbReceta.Image = new Bitmap(returnImage, Ancho, Alto);

            if (Ancho != 0 && Alto != 0)
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

        #region Cargar informacion
        private void CargarServicios()
        {
            cboServicios.Clear();
            cboServicios.Add("0", "<< Seleccione >>");
            cboServicios.Add(query.Farmacia_Servicios(sIdEstado, sIdFarmacia, "CargarServicios()"), true, "IdServicio", "Servicio");
            cboServicios.SelectedIndex = 0;

            cboAreas.Clear();
            cboAreas.Add("0", "<< Seleccione >>");
            dtsAreas = query.Farmacia_ServiciosAreas(sIdEstado, sIdFarmacia, "CargarServicios()");
            cboAreas.SelectedIndex = 0;

            cboTipoDeSurtimiento.Clear();
            cboTipoDeSurtimiento.Add("0", "<< Seleccione >>");
            // dtsAreas = query.Farmacia_ServiciosAreas(sIdEstado, sIdFarmacia, "CargarServicios()");
            cboTipoDeSurtimiento.Filtro = " Status = 'A' ";
            cboTipoDeSurtimiento.Add(query.TiposDeDispensacion("", GnFarmacia.ClaveDispensacionRecetasVales, "CargarServicios()"), true, "IdTipoDeDispensacion", "Dispensacion"); 
            cboTipoDeSurtimiento.SelectedIndex = 0; 
        } 
        #endregion Cargar informacion

        #region Eventos privados 
        private void CargarInformacionAdicionalDeVentas() 
        {
            int iMesActual = GnFarmacia.FechaOperacionSistema.Month;
            DateTime dtFechaMinima = GnFarmacia.FechaOperacionSistema;  


            dtpFechaDeReceta.MaxDate = GnFarmacia.FechaOperacionSistema;  
            if (sFolioVenta == "*")
            {
                //dtpFechaDeReceta.MinDate = dtpFechaDeReceta.MaxDate.AddMonths(iMesesMargen);

                if (iMesesAtras_FechaRecetas <= 0)
                {
                    dtpFechaDeReceta.MinDate = new DateTime(GnFarmacia.FechaOperacionSistema.Year, iMesActual, 1);  
                }
                else
                {
                    iMesesMargen = (-1 * iMesesAtras_FechaRecetas); 
                    dtFechaMinima = dtFechaMinima.AddMonths(iMesesMargen); 
                    dtpFechaDeReceta.MinDate = new DateTime(dtFechaMinima.Year, dtFechaMinima.Month, 1);  
                }

                //////if (!bPermitirFechasRecetAñosAnteriores)
                //////{
                //////    dtpFechaDeReceta.MinDate = new DateTime(GnFarmacia.FechaOperacionSistema.Year, 1, 1);  

                //////    MesesDelAño mesActual =(MesesDelAño)GnFarmacia.FechaOperacionSistema.Month;
                //////    if (mesActual == MesesDelAño.Enero)
                //////    {
                //////        iMesesMargen = (-1 * iMesesFechasRecetaAñosAnteriores);
                //////        dtpFechaDeReceta.MinDate = dtpFechaDeReceta.MaxDate.AddMonths(iMesesMargen);
                //////    }
                //////}
            }
            else
            {
                DateTimePicker dtFechaActual = new DateTimePicker();
                dtpFechaDeReceta.MinDate = dtFechaActual.MinDate;
                dtpFechaDeReceta.MaxDate = dtpFechaDeReceta.MaxDate;

                if (!bEsVale)
                {
                    leer.DataSetClase = query.VentaDispensacion_InformacionAdicional(sIdEmpresa, sIdEstado, sIdFarmacia, sFolioVenta, "CargarInformacionAdicionalDeVentas()");
                }
                else
                {
                    leer.DataSetClase = query.ValesEmision_InformacionAdicional(sIdEmpresa, sIdEstado, sIdFarmacia, sFolioVenta, "CargarInformacionAdicionalDeVentas()");
                }

                if (leer.Leer())
                {
                    Fg.IniciaControles(this, false, FrameBeneficiario);
                    Fg.IniciaControles(this, false, FrameDatosAdicionales);
                    btnRegistrarMedicos.Enabled = false;
                    btnIdentificacion.Enabled = false; 

                    bValidarFoliosUnicosDeRecetas = false;  //// Solo cuando es información ya guardada 
                    txtIdBenificiario.Text = leer.Campo("IdBeneficiario"); 
                    lblNombre.Text = leer.Campo("Beneficiario");
                    lblCURP.Text = leer.Campo("CURP");

                    lblFechaNac.Text = General.FechaYMD(leer.CampoFecha("FechaNacimiento"), "-");
                    lblSexo.Text = leer.Campo("SexoAux");
                    lblEdad.Text = leer.Campo("Edad");
                    lblFolioReferencia.Text = leer.Campo("FolioReferencia");
                    lblFechaVigencia.Text = General.FechaYMD(leer.CampoFecha("FechaInicioVigencia"), "-");

                    bVigenciaValida = leer.CampoBool("EsVigente");
                    if (!bVigenciaValida)
                    {
                        General.msjAviso("Vigencia de Beneficiario expirada, Favor de verificar.");
                    }

                    bEsActivo = true;
                    if (leer.Campo("Status").ToUpper() == "C")
                    {
                        bEsActivo = false;
                        lblStatus.Visible = true;
                        General.msjUser("Beneficiario (a) cancelado.");
                    }

                    // Cargar el resto de la Informacion 
                    txtNumReceta.Text = leer.Campo("NumReceta");
                    dtpFechaDeReceta.Value = leer.CampoFecha("FechaReceta");
                    cboTipoDeSurtimiento.Data = leer.Campo("IdTipoDeDispensacion");

                    txtUMedica.Text = leer.Campo("IdUMedica");
                    lblUnidadMedica.Text = leer.Campo("NombreUMedica"); 

                    txtIdMedico.Text = leer.Campo("IdMedico");
                    lblMedico.Text = leer.Campo("Medico");
                    txtBeneficio.Text = leer.Campo("IdBeneficio");
                    lblBeneficio.Text = leer.Campo("Beneficio");

                    txtIdDiagnostico.Text = leer.Campo("IdDiagnostico");
                    lblDiagnostico.Text = leer.Campo("Diagnostico");
                    cboServicios.Data = leer.Campo("IdServicio");
                    cboAreas.Data = leer.Campo("IdArea");
                    txtRefObservaciones.Text = leer.Campo("RefObservaciones");
                    txtNumeroDeHabitacion.Text = leer.Campo("NumeroDeHabitacion");
                    txtNumeroDeCama.Text = leer.Campo("NumeroDeCama");

                    lbl_EstadoDeResidencia.Text = string.Format("{0} - {1}", leer.Campo("ClaveRENAPO__EstadoDeResidencia"), leer.Campo("EstadoDeResidencia"));
                    lbl_DerechoHabiencia.Text = leer.Campo("DerechoHabiencia");


                    //FrameBeneficiario.Enabled = false;
                    //FrameDatosAdicionales.Enabled = false;
                    //Fg.IniciaControles(this, false, FrameBeneficiario);
                    //Fg.IniciaControles(this, false, FrameDatosAdicionales);


                    //// Solo aplica para informacion guardada 
                    bImagenDigitalizada = true; 

                    btnDigitalizar.Enabled = false; 
                }
            }
        }

        private void txtIdBenificiario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                helpBeneficiarios = new FrmHelpBeneficiarios();
                leer.DataSetClase = helpBeneficiarios.ShowHelp(sIdCliente, sIdSubCliente, bPermitirImportarBeneficiarios);
                if (leer.Leer())
                {
                    CargarDatosBeneficiario();
                    //txtIdBenificiario.Text = leer.Campo("IdBeneficiario");
                    //lblNombre.Text = leer.Campo("NombreCompleto");
                }
            }
        }

        private void CargarDatosBeneficiario()
        {
            txtIdBenificiario.Text = leer.Campo("IdBeneficiario");
            lblNombre.Text = leer.Campo("NombreCompleto");

            lblFechaNac.Text = General.FechaYMD(leer.CampoFecha("FechaNacimiento"), "-");
            lblSexo.Text = leer.Campo("SexoAux");
            lblEdad.Text = leer.Campo("Edad");
            lblFolioReferencia.Text = leer.Campo("FolioReferencia");
            lblFechaVigencia.Text = General.FechaYMD(leer.CampoFecha("FechaFinVigencia"), "-");
            lblCURP.Text = leer.Campo("CURP");

            sIdEstadoResidencia = leer.Campo("IdEstadoResidencia");
            lbl_EstadoDeResidencia.Text = string.Format("{0} - {1}", leer.Campo("ClaveRENAPO__EstadoDeResidencia"), leer.Campo("EstadoDeResidencia"));
            sIdTipoDerechoHaciencia = leer.Campo("IdTipoDerechoHabiencia"); 
            lbl_DerechoHabiencia.Text = leer.Campo("DerechoHabiencia");

            bVigenciaValida = leer.CampoBool("EsVigente");
            if (!bVigenciaValida)
            {
                General.msjAviso("Vigencia de Beneficiario expirada, Favor de verificar.");
            }

            bEsActivo = true;
            if (leer.Campo("Status").ToUpper() == "C")
            {
                bEsActivo = false;
                lblStatus.Visible = true;
                General.msjUser("Beneficiario (a) cancelado.");
            }

            if (bEsActivo)
            {
                if (bValidarBeneficariosAlertas)
                {
                    ValidarBeneficiarioCuentaConAlertas(); 
                }
            }

            lblTieneIdentificacion.Visible = true;
            lblTieneIdentificacion.Text = leer.CampoBool("TieneIdentificacion") ? "Con identificación" : "Sin identificación"; 
            btnIdentificacion.Enabled = true; 
        }

        private void ValidarBeneficiarioCuentaConAlertas()
        {
            string sReferencia = lblFolioReferencia.Text.Replace(".", "-");
            int iPosicion = 0; 

            iPosicion = sReferencia.IndexOf("-", 0);
            sReferencia = sReferencia.Substring(0, iPosicion).Trim(); 


            string sSql = string.Format(
                "Select 'Nombre de Beneficiario' = NombreBeneficiario " + 
                "From CFG_VAL_BeneficiariosAlertas (NoLock) " +
                "Where Status = 'A' and FolioReferencia like '%{0}%' ", sReferencia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidarBeneficiarioCuentaConAlertas");
                General.msjError("Error al verificar alertas de beneficiario.");
            }
            else
            {
                if (leer.Leer())
                {
                    FrmBeneficiarios_Alertas alertas = new FrmBeneficiarios_Alertas(leer.DataSetClase);
                    alertas.ShowDialog(this);
                }
            }
        }

        private void txtIdBenificiario_Validating(object sender, CancelEventArgs e)
        {
            bEsActivo = false;
            lblStatus.Visible = false;
            lblStatus.Text = "CANCELADO";

            lblTieneIdentificacion.Visible = false; 
            btnIdentificacion.Enabled = false;

            if (txtIdBenificiario.Text.Trim() != "")
            {
                leer.DataSetClase = query.Beneficiarios(sIdEstado, sIdFarmacia, sIdCliente, sIdSubCliente, txtIdBenificiario.Text, "txtIdBenificiario_Validating");
                if (leer.Leer())
                {
                    CargarDatosBeneficiario(); 
                    //txtIdBenificiario.Text = leer.Campo("IdBeneficiario");
                    //lblNombre.Text = leer.Campo("NombreCompleto");

                    //lblFechaNac.Text = General.FechaYMD(leer.CampoFecha("FechaNacimiento"), "-");
                    //lblSexo.Text = leer.Campo("SexoAux");
                    //lblEdad.Text = leer.Campo("Edad");
                    //lblFolioReferencia.Text = leer.Campo("FolioReferencia");
                    //lblFechaVigencia.Text = General.FechaYMD(leer.CampoFecha("FechaVigencia"), "-");

                    //bVigenciaValida = leer.CampoBool("EsVigente");
                    //if (!bVigenciaValida)
                    //{
                    //    General.msjAviso("La Vigencia del Beneficiario ha expirado, no es posible generar la venta."); 
                    //}

                    //bEsActivo = true;
                    //if (leer.Campo("Status").ToUpper() == "C")
                    //{
                    //    bEsActivo = false;
                    //    lblStatus.Visible = true;
                    //    General.msjUser("El Beneficiario se encuentra cancelado, no es posible generar la venta.");
                    //}
                }
                else
                {
                    General.msjUser("Beneficiario no encontrado, Favor de verificar.");
                    Fg.IniciaControles(this, true, FrameBeneficiario);
                    txtIdBenificiario.Focus();
                }
            }
            else
            {
                Fg.IniciaControles(this, true, FrameBeneficiario);
                txtIdBenificiario.Focus();
            }
        }

        private void btnRegistrarBeneficiarios_Click(object sender, EventArgs e)
        {
            FrmBeneficiarios f = new FrmBeneficiarios();
            f.MostrarDetalle(sIdCliente, sIdClienteNombre, sIdSubCliente, sIdSubClienteNombre);

            if(txtIdBenificiario.Text.Trim() != "")
            {
                txtIdBenificiario_Validating(null, null); 
            }
        }

        private void txtIdMedico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Medicos(sIdEstado, sIdFarmacia, "txtIdMedico_KeyDown");
                if (leer.Leer())
                {
                    if (leer.Campo("Status") == "C")
                    {
                        General.msjAviso("Médico : " + leer.Campo("IdMedico") + " -- " + leer.Campo("NombreCompleto") + ". Cancelado. ");
                        txtIdMedico.Text = "";
                        lblMedico.Text = "";
                        txtIdMedico.Focus();
                    }                    
                    else
                    {
                        txtIdMedico.Text = leer.Campo("IdMedico");
                        lblMedico.Text = leer.Campo("NombreCompleto");
                    }
                }
            }
        }

        private void txtIdMedico_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdMedico.Text.Trim() == "")
            {
                txtIdMedico.Text = "";
                lblMedico.Text = ""; 
            }
            else 
            {
                leer.DataSetClase = query.Medicos(sIdEstado, sIdFarmacia, txtIdMedico.Text, "txtIdMedico_Validating");
                if (leer.Leer())
                {
                    if (leer.Campo("Status") == "C")
                    {
                        General.msjAviso("Médico : " + leer.Campo("IdMedico") + " -- " + leer.Campo("NombreCompleto") + ". Cancelado. ");
                        txtIdMedico.Text = "";
                        lblMedico.Text = "";
                        txtIdMedico.Focus();
                    }
                    else
                    {
                        txtIdMedico.Text = leer.Campo("IdMedico");
                        lblMedico.Text = leer.Campo("NombreCompleto");
                    }
                }
                else
                {
                    General.msjUser("Médico no encontrado, Favor de verificar."); 
                    txtIdMedico.Text = "";
                    lblMedico.Text = "";
                    txtIdMedico.Focus(); 
                } 
            }
        }

        private void txtIdMedico_TextChanged(object sender, EventArgs e)
        {
            lblMedico.Text = "";
        }

        private void txtIdDiagnostico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.DiagnosticosCIE10("txtIdDiagnostico_Validating");
                if (leer.Leer())
                {
                    txtIdDiagnostico.Text = leer.Campo("ClaveDiagnostico");
                    lblDiagnostico.Text = leer.Campo("Descripcion");
                }
            }            
        }

        private void txtIdDiagnostico_Validating(object sender, CancelEventArgs e)
        {
            sIdDiagnosticoClave = ""; 
            if (txtIdDiagnostico.Text.Trim() != "")
            {
                if (txtIdDiagnostico.Text.Trim().Length < GnFarmacia.ClaveDiagnosticoCaracteres) 
                {
                    General.msjUser(string.Format("Formato de Diagnóstico incorrecto, Diagnóstico debe contener {0} caracteres, Favor de verificar.", GnFarmacia.ClaveDiagnosticoCaracteres));

                    if (!bCerrarInformacionAdicionalAutomaticamente)
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    leer.DataSetClase = query.DiagnosticosCIE10(txtIdDiagnostico.Text, "txtIdDiagnostico_Validating");
                    if (!leer.Leer())
                    {
                        General.msjUser("Diagnóstico no encontrado, Favor de verificar.");

                        if (!bCerrarInformacionAdicionalAutomaticamente)
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        sIdDiagnosticoClave = leer.Campo("IdDiagnostico"); ; 
                        txtIdDiagnostico.Text = leer.Campo("ClaveDiagnostico");
                        lblDiagnostico.Text = leer.Campo("Descripcion");
                    }
                }
            }
        }

        private void txtIdDiagnostico_TextChanged(object sender, EventArgs e)
        {
            lblDiagnostico.Text = ""; 
        }

        private void btnRegistrarMedicos_Click(object sender, EventArgs e)
        {
            FrmMedicos f = new FrmMedicos();
            f.ShowDialog();
        }

        private void cboServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboAreas.Clear();
            cboAreas.Add("0", "<< Seleccione >>");
            if (cboServicios.SelectedIndex != 0)
            {
                try
                {
                    string sWhere = string.Format(" IdServicio = '{0}' ", cboServicios.Data);
                    cboAreas.Add(dtsAreas.Tables[0].Select(sWhere), true, "IdArea", "Area_Servicio");
                }
                catch { }
            }
            cboAreas.SelectedIndex = 0;
        }

        private void txtBeneficio_Validating(object sender, CancelEventArgs e)
        {
            sIdBeneficioSeguroPopular = txtBeneficio.Text;
            //////if (txtBeneficio.Text.Trim() != "")
            //////{
            //////    leer.DataSetClase = query.BeneficiosSP(txtBeneficio.Text, "txtIdDiagnostico_Validating");
            //////    if (!leer.Leer())
            //////    {
            //////        General.msjUser("Clave de Beneficio no encontrada, verifique.");
            //////        e.Cancel = true;
            //////    }
            //////    else
            //////    {
            //////        sIdBeneficioSeguroPopular = leer.Campo("IdBeneficio"); ;
            //////        txtBeneficio.Text = leer.Campo("IdBeneficio");
            //////        lblBeneficio.Text = leer.Campo("Descripcion");
            //////    }
            //////}
        }

        private void txtBeneficio_TextChanged(object sender, EventArgs e)
        {
            lblBeneficio.Text = ""; 
        } 

        private void txtBeneficio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.BeneficiosSP("txtIdDiagnostico_Validating");
                if (leer.Leer())
                {
                    sIdBeneficioSeguroPopular = leer.Campo("IdBeneficio"); ;
                    txtBeneficio.Text = leer.Campo("IdBeneficio");
                    lblBeneficio.Text = leer.Campo("Descripcion");
                }
            } 
        }

        private void cboTipoDeSurtimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUMedica.Enabled = false;
            txtUMedica.Text = sIdUMedica_Base;
            lblUnidadMedica.Text = sCLUES_Base;

            if (cboTipoDeSurtimiento.Data == sClaveDispensacionRecetasForaneas ||
                cboTipoDeSurtimiento.Data == sClaveDispensacionUnidadesNoAdministradas ||
                cboTipoDeSurtimiento.Data == sClaveDispensacionRecetasForaneas_Vales)
            {
                txtUMedica.Enabled = true;
                txtUMedica.Text = ""; 
                lblUnidadMedica.Text = "";
            }
        }

        private void txtUMedica_TextChanged(object sender, EventArgs e)
        {
            lblUnidadMedica.Text = "";
        }

        private void txtUMedica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.UnidadesMedicasJurisccion(DtGeneral.EstadoConectado, DtGeneral.Jurisdiccion, "txtUMedica_KeyDown");
                if (leer.Leer())
                {
                    sCLUES_Foranea = leer.Campo("IdUmedica");
                    txtUMedica.Text = sCLUES_Foranea;  // leer.Campo("IdBeneficio");
                    lblUnidadMedica.Text = leer.Campo("CLUES") + " -- " + leer.Campo("NombreUMedica");
                }
            } 
        }

        private void txtUMedica_Validating(object sender, CancelEventArgs e)
        {
            // sCLUES_Foranea = "";
            if (txtUMedica.Text.Trim() != "")
            {
                leer.DataSetClase = query.UnidadesMedicasJurisccion(DtGeneral.EstadoConectado, DtGeneral.Jurisdiccion, txtUMedica.Text.Trim(), "txtIdDiagnostico_Validating");
                if (!leer.Leer())
                {
                    // e.Cancel = true;
                    General.msjUser("Unidad Medica no encontrada, Favor de verificar.");
                }
                else
                {
                    sCLUES_Foranea = leer.Campo("IdUmedica");
                    txtUMedica.Text = sCLUES_Foranea;  // leer.Campo("IdBeneficio");
                    lblUnidadMedica.Text = leer.Campo("CLUES") + " -- " + leer.Campo("NombreUMedica");
                }
            }
        }
        #endregion Eventos privados 

        private void FrmInformacionVentas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    if (ValidarInformacion())
                    {
                        this.Close();
                    }
                    break;
                default:
                    break;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            ////if (e.CloseReason == CloseReason.UserClosing)
            ////{
            ////    if (!ValidarInformacion())
            ////        e.Cancel = true;
            ////}
        }

        private void FrmInformacionVentas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (!ValidarInformacion())
                {
                    e.Cancel = true;
                }
            }
        }

        private bool EsFolioDeRecetaUnico()
        {
            bool bRegresa = false;
            string sSql = string.Format(
                "Select Top 1 * From VentasInformacionAdicional (NoLock) " + 
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and NumReceta = '{3}' ",  
                sIdEmpresa, sIdEstado, sIdFarmacia, txtNumReceta.Text.Trim() ); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "EsFolioDeRecetaUnico()");
            }
            else
            {
                bRegresa = !leer.Leer(); 
            }

            return bRegresa; 
        }

        private bool ValidarInformacion()
        {
            return ValidarInformacion(true); 
        }

        private bool ValidarInformacion(bool FolioDeRecetaUnico)
        {
            bool bRegresa = true;
            bool bFolioValido = true;

            if (txtIdBenificiario.Text.Trim() == "")
            {
                bRegresa = false;
            }

            if (bRegresa && txtNumReceta.Text.Trim() == "")
            {
                bRegresa = false;
            }

            if (bRegresa && cboTipoDeSurtimiento.SelectedIndex == 0)
            {
                bRegresa = false;
            }

            if (bRegresa && txtUMedica.Text.Trim() == "")
            {
                bRegresa = false;
            }

            if (bRegresa && txtIdMedico.Text.Trim() == "")
            {
                bRegresa = false;
            }

            if (bRegresa && lblMedico.Text.Trim() == "")
            {
                bRegresa = false;
            }

            if (bRegresa && txtIdDiagnostico.Text.Trim() == "")
            {
                bRegresa = false;
            }

            if (bRegresa && txtBeneficio.Text.Trim() == "")
            {
                bRegresa = false;
            }

            if (bRegresa && cboServicios.SelectedIndex == 0)
            {
                bRegresa = false;
            }

            if (bRegresa && cboAreas.SelectedIndex == 0)
            {
                bRegresa = false;
            }

            if (bRegresa && txtRefObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
            }


            if (GnFarmacia.CapturarNumeroDeCama)
            {
                if (bRegresa && txtNumeroDeCama.Text.Trim() == "")
                {
                    bRegresa = false;
                }
            }

            if (GnFarmacia.CapturarNumeroDeHabitacion)
            {
                if (bRegresa && txtNumeroDeHabitacion.Text.Trim() == "")
                {
                    bRegresa = false;
                }
            }



            ////if (bRegresa)
            ////{
            ////    if (GnFarmacia.ImplementaDigitalizacion)
            ////    {
            ////        if (!bImagenDigitalizada)
            ////        {
            ////            bRegresa = false;
            ////        }
            ////    }
            ////}


            //////////// Queda pendiente para la Siguiente version 2010-03-13 10:35
            ////// Cada Hospital y/o Centro de Salud decide si se pide ó no este dato en las recetas. 
            ////if (bValidarBeneficioSeguroPopular)
            ////{
            ////    if (bRegresa &&  txtBeneficio.Text.Trim() == "")
            ////        bRegresa = false; 
            ////}

            ////// Marcar la captura como incompleta, para evitar que se guarden datos incompletos 
            bCapturaCompleta = bRegresa;
            if (FolioDeRecetaUnico)
            {
                if (bRegresa)
                {
                    if (bValidarFoliosUnicosDeRecetas)
                    {
                        bFolioValido = EsFolioDeRecetaUnico();
                        bCapturaCompleta = bFolioValido; 
                        txtNumReceta.Focus();
                    }
                }
            }

            if (!bFolioValido)
            {
                if (DtGeneral.ConfirmacionConHuellas)
                {
                    if (General.msjConfirmar(string.Format("Folio receta {0} existente.\n\n para continuar necesitara permisos especiales, ¿ Desea continuar ?", txtNumReceta.Text.Trim())) == DialogResult.Yes)
                    {
                        //bCapturaCompleta = VerificarPermisos();
                        sMsjNoEncontrado = "Usuario sin permisos para autorizar este movimiento, Favor de verificar.";
                        ////bCapturaCompleta = opPermisosEspeciales.VerificarPermisos("FOLIO_RECETA_DUPLICADA", sMsjNoEncontrado);
                        bCapturaCompleta = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("FOLIO_RECETA_DUPLICADA", sMsjNoEncontrado);
                    }
                }
                else
                {
                    General.msjAviso(string.Format("Folio Receta {0} existente, Favor de verificar.", txtNumReceta.Text.Trim()));
                }
            }

            if (!bRegresa && bFolioValido)
            {
                if (General.msjConfirmar("No ha completado los datos de esta pantalla.\n\n¿ Desea continuar, No se realizara la Dispersion sin estos datos ?") == DialogResult.Yes)
                {
                    bRegresa = true;
                }
            } 


            // Asignar los valores de salida 
            //sIdCliente = sIdCliente;
            //sIdSubCliente = sIdSubCliente;
            sIdBeneficiario = txtIdBenificiario.Text;
            sNumReceta = txtNumReceta.Text;
            dtpFechaReceta = dtpFechaDeReceta.Value;
            sIdTipoDispensacion = cboTipoDeSurtimiento.Data; 
            sIdMedico = txtIdMedico.Text;
            sIdDiagnostico = txtIdDiagnostico.Text;
            sIdServicio = cboServicios.Data;
            sIdArea = cboAreas.Data;
            sReferenciaObserv = txtRefObservaciones.Text;
            sIdBeneficioSeguroPopular = txtBeneficio.Text.Trim();
            sNumeroDeHabitacion = txtNumeroDeHabitacion.Text.Trim();
            sNumeroDeCama = txtNumeroDeCama.Text.Trim();

            return bRegresa;
        }

        private void FrmInformacionVentas_Activated(object sender, EventArgs e)
        {
            //// Jesús Díaz 2K120516.1405 
            if (!bVentanaActiva)
            {
                bVentanaActiva = true; 
                if (bCerrarInformacionAdicionalAutomaticamente) 
                {
                    if (ValidarInformacion(false))
                    {
                        this.Close();
                    }
                }
            }
        }

        private void txtIdBenificiario_TextChanged(object sender, EventArgs e)
        {
            btnIdentificacion.Enabled = false;
            lblTieneIdentificacion.Visible = false; 
            lblNombre.Text = "";
            lblCURP.Text = "";

            lblFechaNac.Text = "";
            lblSexo.Text = "";
            lblEdad.Text = "";
            lblFolioReferencia.Text = "";
            lblFechaVigencia.Text = General.FechaYMD(leer.CampoFecha("FechaInicioVigencia"), "-");

            lbl_EstadoDeResidencia.Text = "";
            lbl_DerechoHabiencia.Text = "";
        }

        private void btnIdentificacion_Click( object sender, EventArgs e )
        {
            FrmBeneficiarios_Identificacion f = new FrmBeneficiarios_Identificacion();
            f.Mostrar_Identificacion(sIdEstado, sIdFarmacia, sIdCliente, sIdSubCliente, txtIdBenificiario.Text);
        }

        //private void rdoFolio_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtIdBenificiario.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
        //    txtIdBenificiario.MaxLength = 15;
        //    txtIdBenificiario.TextAlign = HorizontalAlignment.Center;
        //}

        //private void rdoCodigo_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtIdBenificiario.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
        //    txtIdBenificiario.MaxLength = 8;
        //    txtIdBenificiario.TextAlign = HorizontalAlignment.Center;
        //}
    }
    #endregion Form

    public class clsInformacionVentas
    {
        private string sEmpresa = "";
        private string sIdEstado = "";
        private string sIdFarmacia = "";

        FrmInformacionVentas f;
        basGenerales Fg = new basGenerales();

        FrmPDD_03_Datos_Documento Doctos;
        FrmPDD_04_Datos_Diagnostico Diagnosticos;
        FrmPDD_02_Datos_Beneficiario Beneficiarios;
        FrmPDD_05_Datos_ServiciosAreas Serv_Areas;

        private bool bBloquearControles = false;

        private bool bCapturaCompleta = false;
        private bool bCapturaCompletaDocto = false;
        private bool bCapturaCompletaBenef = false;

        private bool bVigenciaValida = false;
        private bool bEsActivo = false;

        bool bEsSeguroPopular = false;
        bool bEsVale = false;
        bool bPermitirCapturaBeneficiariosNuevos = false;
        bool bPermitirImportarBeneficiarios = false;

        string sIdCliente = "";
        string sIdSubCliente = "";
        string sIdBeneficiario = "";
        string sNumReceta = "";
        string sFechaReceta = "";
        bool bRecetaEDM = false;
        DateTime dtpFechaReceta = DateTime.Now;
        string sIdTipoDispensacion = "";
        string sIdMedico = "";
        string sIdDiagnostico = "0001";
        string sIdDiagnosticoClave = "";
        string sIdServicio = "001";
        string sIdArea = "001";
        string sReferenciaObserv = "";
        string sIdBeneficioSeguroPopular = "";
        string sCLUES_Foranea = "";
        string sNumeroDeHabitacion = "";
        string sNumeroDeCama = "";
        string sIdEstadoResidencia = "";
        string sIdTipoDerechoHaciencia = "";

        bool bImagenDigitalizada = false;
        Image imgDigitizacion;
        string sImg_Original = "";
        string sImg_Comprimida = "";


        bool bEsPedido = false;

        public clsInformacionVentas( string IdEmpresa, string IdEstado, string IdFarmacia )
        {
            this.sEmpresa = IdEmpresa;
            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
        }

        #region Propiedades 
        public bool BloquearControles
        {
            get { return bBloquearControles; }
            set { bBloquearControles = value; }
        }

        public bool ClienteSeguroPopular
        {
            get { return bEsSeguroPopular; }
            set { bEsSeguroPopular = value; }
        }

        public bool EsVale
        {
            get { return bEsVale; }
            set { bEsVale = value; }
        }

        public bool PermitirBeneficiariosNuevos
        {
            get { return bPermitirCapturaBeneficiariosNuevos; }
            set { bPermitirCapturaBeneficiariosNuevos = value; }
        }

        public bool PermitirImportarBeneficiarios
        {
            get { return bPermitirImportarBeneficiarios; }
            set { bPermitirImportarBeneficiarios = value; }
        }

        public string NumReceta
        {
            set { sNumReceta = value; }
        }


        public bool RecetaEDM
        {
            set { bRecetaEDM = value; }
        }

        public bool PermitirGuardar
        {
            get
            {
                if(bCapturaCompletaDocto && bCapturaCompletaBenef)
                {
                    bCapturaCompleta = true;
                }
                return bCapturaCompleta;
            }
        }

        public bool BeneficiarioVigente
        {
            get { return bVigenciaValida; }
        }

        public bool BeneficiarioActivo
        {
            get { return bEsActivo; }
        }

        public string Cliente
        {
            get { return Fg.PonCeros(sIdCliente, 4); }
        }

        public string SubCliente
        {
            get { return Fg.PonCeros(sIdSubCliente, 4); }
        }

        public string Beneficiario
        {
            set { sIdBeneficiario = value; }
            get { return Fg.PonCeros(sIdBeneficiario, 8); }
        }

        public string Receta
        {
            get { return sNumReceta; }
        }

        public DateTime FechaReceta
        {
            get { return dtpFechaReceta; }
        }

        public string TipoDispensacion
        {
            get { return sIdTipoDispensacion; }
        }

        public string CluesRecetasForaneas
        {
            get { return Fg.PonCeros(sCLUES_Foranea, 6); }
        }

        public string Medico
        {
            get { return Fg.PonCeros(sIdMedico, 6); }
        }

        public string IdBeneficio
        {
            get
            {
                if(sIdBeneficioSeguroPopular == "")
                {
                    sIdBeneficioSeguroPopular = GnFarmacia.ClaveBeneficio;
                }
                return sIdBeneficioSeguroPopular;
            }
        }

        public string IdDiagnostico
        {
            get { return sIdDiagnosticoClave; }
        }

        public string Diagnostico
        {
            get
            {
                if(sIdDiagnostico == "")
                {
                    sIdDiagnostico = GnFarmacia.ClaveDiagnostico;
                }
                return sIdDiagnostico;
            }
        }

        public string Servicio
        {
            get
            {
                if(sIdServicio == "")
                {
                    sIdServicio = "001";
                }
                return Fg.PonCeros(sIdServicio, 3);
            }
        }

        public string Area
        {
            get
            {
                if(sIdArea == "")
                {
                    sIdArea = "001";
                }
                return Fg.PonCeros(sIdArea, 3);
            }
        }

        public string ReferenciaObservaciones
        {
            get { return sReferenciaObserv; }
        }

        public bool ImagenDigitalizada
        {
            get { return bImagenDigitalizada; }
        }

        public Image ImagenDigitalizacion
        {
            get { return imgDigitizacion; }
        }

        public string Imagen_Original
        {
            get { return sImg_Original; }
        }

        public string Imagen_Comprimida
        {
            get { return sImg_Comprimida; }
        }

        public string NumeroDeHabitacion
        {
            get { return sNumeroDeHabitacion; }
        }

        public string NumeroDeCama
        {
            get { return sNumeroDeCama; }
        } 
                
        public string IdEstadoResidencia
        {
            get { return sIdEstadoResidencia; }
        }

        public string IdTipoDerechoHaciencia
        {
            get { return sIdTipoDerechoHaciencia; }
        }
       
        public bool EsPedido
        {
            get { return bEsPedido; } 
            set { bEsPedido = value; }
        }

        #endregion Propiedades

        public void Show(string FolioVenta, string IdCliente, string NombreCliente, string IdSubCliente, string NombreSubCliente)
        {
            this.Show(FolioVenta, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, false, false); 
        }

        public void Show(string FolioVenta, string IdCliente, string NombreCliente, string IdSubCliente, string NombreSubCliente,
            string IdBeneficiario, string NumeroDeReceta, string FechaReceta, string IdMedico, string CIE_10, 
            string IdServicio, string IdArea, string TipoDispensacion, 
            bool CerrarInformacionAdicional)
        {
            sIdBeneficiario = IdBeneficiario;
            sNumReceta = NumeroDeReceta;
            sFechaReceta = FechaReceta; 
            sIdMedico = IdMedico;
            sIdDiagnostico = CIE_10;
            sIdServicio = IdServicio;
            sIdArea = IdArea;
            sIdTipoDispensacion = TipoDispensacion;

            this.Show(FolioVenta, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, false, CerrarInformacionAdicional);
        }

        public void Show(string FolioVenta, string IdCliente, string NombreCliente, string IdSubCliente, string NombreSubCliente, bool Vale_FolioVenta, bool CerrarInformacionAdicional)
        {
            this.sIdCliente = IdCliente;
            this.sIdSubCliente = IdSubCliente;

            f = new FrmInformacionVentas(sIdEstado, sIdFarmacia, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente);

            f.bRecetaEDM = bRecetaEDM;

            f.bCerrarInformacionAdicionalAutomaticamente = CerrarInformacionAdicional;
            f.bBloquearControles = bBloquearControles; 
            f.bEsVale = bEsVale;
            f.bVale_FolioVenta = Vale_FolioVenta; 
            f.bEsSeguroPopular = bEsSeguroPopular; 
            f.bPermitirCapturaBeneficiariosNuevos = bPermitirCapturaBeneficiariosNuevos;
            f.bPermitirImportarBeneficiarios = bPermitirImportarBeneficiarios; 

            f.sFolioVenta = FolioVenta;
            f.sIdCliente = sIdCliente;
            f.sIdSubCliente = sIdSubCliente;
            f.sIdBeneficiario = sIdBeneficiario;
            f.sNumReceta = sNumReceta;
            f.bEsPedido = bEsPedido; 

            f.sFechaReceta = sFechaReceta; 
            f.dtpFechaReceta = dtpFechaReceta;
            f.sIdTipoDispensacion = sIdTipoDispensacion;
            f.sCLUES_Foranea = sCLUES_Foranea == "" ? "000000" : sCLUES_Foranea; 
            f.sIdMedico = sIdMedico;
            f.sIdDiagnostico = sIdDiagnostico;
            f.sIdServicio = sIdServicio;
            f.sIdArea = sIdArea;
            f.sReferenciaObserv = sReferenciaObserv;
            f.sIdBeneficioSeguroPopular = sIdBeneficioSeguroPopular;
            f.sNumeroDeHabitacion = sNumeroDeHabitacion;
            f.sNumeroDeCama = sNumeroDeCama;
            f.sIdEstadoResidencia = sIdEstadoResidencia; 
            f.sIdTipoDerechoHaciencia = sIdTipoDerechoHaciencia; 
            f.ShowDialog(); // Mostrar la pantalla 


            sIdCliente = f.sIdCliente;
            sIdSubCliente = f.sIdSubCliente;
            sIdBeneficiario = f.sIdBeneficiario;
            sNumReceta = f.sNumReceta;
            dtpFechaReceta = f.dtpFechaReceta;
            sIdTipoDispensacion = f.sIdTipoDispensacion;
            sCLUES_Foranea = f.sCLUES_Foranea == "" ? "000000" : f.sCLUES_Foranea; 
            sIdMedico = f.sIdMedico;
            sIdDiagnostico = f.sIdDiagnostico;
            sIdDiagnosticoClave = f.sIdDiagnosticoClave; 
            sIdServicio = f.sIdServicio;
            sIdArea = f.sIdArea;
            sReferenciaObserv = f.sReferenciaObserv;
            bCapturaCompleta = f.bCapturaCompleta;
            bVigenciaValida = f.bVigenciaValida;
            bEsActivo = f.bEsActivo;
            sIdBeneficioSeguroPopular = f.sIdBeneficioSeguroPopular;
            sNumeroDeHabitacion = f.sNumeroDeHabitacion;
            sNumeroDeCama = f.sNumeroDeCama;
            sIdEstadoResidencia = f.sIdEstadoResidencia; 
            sIdTipoDerechoHaciencia = f.sIdTipoDerechoHaciencia; 
            bImagenDigitalizada = f.bImagenDigitalizada;
            imgDigitizacion = f.imgDigitizacion;
            sImg_Original = f.sImg_Original;
            sImg_Comprimida = f.sImg_Comprimida; 

            f.Close(); f = null;
        }

        public void ShowDocumento(string FolioVenta)
        {

            Doctos = new FrmPDD_03_Datos_Documento(sIdEstado, sIdFarmacia, FolioVenta);

            Doctos.bCerrarInformacionAdicionalAutomaticamente = false;
            Doctos.bEsVale = bEsVale;
            Doctos.bVale_FolioVenta = false;
            Doctos.bEsSeguroPopular = bEsSeguroPopular;


            Doctos.sFolioVenta = FolioVenta;
            Doctos.sNumReceta = sNumReceta;
            Doctos.dtpFechaReceta = dtpFechaReceta;
            Doctos.sIdTipoDispensacion = sIdTipoDispensacion;
            Doctos.sCLUES_Foranea = sCLUES_Foranea == "" ? "000000" : sCLUES_Foranea;
            Doctos.sIdMedico = sIdMedico;

            Doctos.ShowDialog(); // Mostrar la pantalla          

            sNumReceta = Doctos.sNumReceta;
            dtpFechaReceta = Doctos.dtpFechaReceta;
            sIdTipoDispensacion = Doctos.sIdTipoDispensacion;
            sCLUES_Foranea = Doctos.sCLUES_Foranea == "" ? "000000" : Doctos.sCLUES_Foranea;
            sIdMedico = Doctos.sIdMedico;
            bCapturaCompletaDocto = Doctos.bCapturaCompletaDocto;

            Doctos.Close(); Doctos = null;
        }

        public void ShowDiagnosticos(string FolioVenta)
        {         

            Diagnosticos = new FrmPDD_04_Datos_Diagnostico(sIdEstado, sIdFarmacia, FolioVenta);

            Diagnosticos.bEsVale = bEsVale;
            Diagnosticos.sFolioVenta = FolioVenta;
            Diagnosticos.sIdDiagnostico = sIdDiagnostico;
            Diagnosticos.sIdBeneficioSeguroPopular = sIdBeneficioSeguroPopular;
            Diagnosticos.ShowDialog(); // Mostrar la pantalla

            sIdDiagnostico = Diagnosticos.sIdDiagnostico;
            sIdDiagnosticoClave = Diagnosticos.sIdDiagnosticoClave;
            sIdBeneficioSeguroPopular = Diagnosticos.sIdBeneficioSeguroPopular;

            Diagnosticos.Close(); Diagnosticos = null;
        }

        public void ShowBeneficiarios(string FolioVenta, string IdCliente, string IdSubCliente)
        {
            this.sIdCliente = IdCliente;
            this.sIdSubCliente = IdSubCliente;

            Beneficiarios = new FrmPDD_02_Datos_Beneficiario(sIdEstado, sIdFarmacia, FolioVenta, IdCliente, IdSubCliente);

            Beneficiarios.bCerrarInformacionAdicionalAutomaticamente = false;
            Beneficiarios.bEsVale = bEsVale;
            Beneficiarios.bVale_FolioVenta = false;
            Beneficiarios.bEsSeguroPopular = bEsSeguroPopular;
            Beneficiarios.bPermitirCapturaBeneficiariosNuevos = bPermitirCapturaBeneficiariosNuevos;
            Beneficiarios.bPermitirImportarBeneficiarios = bPermitirImportarBeneficiarios;

            Beneficiarios.sFolioVenta = FolioVenta;
            Beneficiarios.sIdCliente = sIdCliente;
            Beneficiarios.sIdSubCliente = sIdSubCliente;
            Beneficiarios.sIdBeneficiario = sIdBeneficiario;
            Beneficiarios.sReferenciaObserv = sReferenciaObserv;

            Beneficiarios.ShowDialog(); // Mostrar la pantalla

            sIdCliente = Beneficiarios.sIdCliente;
            sIdSubCliente = Beneficiarios.sIdSubCliente;
            sIdBeneficiario = Beneficiarios.sIdBeneficiario;
            sReferenciaObserv = Beneficiarios.sReferenciaObserv;
            bCapturaCompletaBenef = Beneficiarios.bCapturaCompletaBenef;
            bVigenciaValida = Beneficiarios.bVigenciaValida;
            bEsActivo = Beneficiarios.bEsActivo;

            Beneficiarios.Close(); Beneficiarios = null;
        }

        public void ShowServiciosAreas(string FolioVenta)
        {
            Serv_Areas = new FrmPDD_05_Datos_ServiciosAreas(sIdEstado, sIdFarmacia, FolioVenta);

            Serv_Areas.bEsVale = bEsVale;
            Serv_Areas.sFolioVenta = FolioVenta;
            Serv_Areas.sIdServicio = sIdServicio;
            Serv_Areas.sIdArea = sIdArea;

            Serv_Areas.ShowDialog(); // Mostrar la pantalla 

            sIdServicio = Serv_Areas.sIdServicio;
            sIdArea = Serv_Areas.sIdArea;

            Serv_Areas.Close(); Serv_Areas = null;
        }
    }
}
