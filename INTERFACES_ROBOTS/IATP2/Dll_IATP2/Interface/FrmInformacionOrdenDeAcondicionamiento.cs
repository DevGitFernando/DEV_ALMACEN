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
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos; 
using DllFarmaciaSoft.Ayudas;
using DllFarmaciaSoft.Usuarios_y_Permisos;

using Farmacia.Catalogos;
using DllRecetaElectronica.ECE;

namespace Dll_IATP2.Interface
{
    #region Form 
    public partial class FrmInformacionOrdenDeAcondicionamiento : FrmBaseExt
    {
        #region Variables
        private enum Cols
        {
            Ninguna = 0,
            //FolioSolicitud, Consecutivo, 
            CodigoEAN, Codigo, ClaveSSA, Descripcion, Cantidad, 
            FechaAdmin, HoraAdmin 
        }


        clsConexionSQL cnn; //= new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas Ayuda;
        DataSet dtsAreas;
        FrmHelpBeneficiarios helpBeneficiarios;
        clsGrid Grid;

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

        public bool bInformacionGuardada = false;

        public bool bEsSeguroPopular = false;
        public bool bValidarBeneficioSeguroPopular = false; 
        public bool bPermitirCapturaBeneficiariosNuevos = false;
        public bool bPermitirImportarBeneficiarios = false; 
        public string sFolioVenta = "";
        public string sIdCliente = "";
        public string sIdClienteNombre = "";        
        public string sIdSubCliente = "";
        public string sIdSubClienteNombre = "";

        public string sIdBeneficiario = "";
        public string sNumReceta = "";
        public string sFechaReceta = ""; 
        public DateTime dtpFechaReceta = DateTime.Now;
        public string sIdTipoDispensacion = ""; 
        public string sIdMedico = "";
        public string sIdDiagnostico = "";
        public string sIdDiagnosticoClave = "";
        public string sIdBeneficioSeguroPopular = ""; 

        public string sIdServicio = "";
        public string sIdArea = "";
        public string sReferenciaObserv = "";

        public string sNombre_Beneficiario = "";
        public string sNombre_Medico = "";
        public string sNumeroDeHabitacion = "";
        public string sNumeroDeCama = ""; 

        clsLeer leerDatos; 
        public DataSet dtsListaProductos_Solicitud = new DataSet(); 

        private byte[] bufferImagen = null;
        public bool bImagenDigitalizada = false;
        public Image imgDigitizacion;
        public string sImg_Original = "";
        public string sImg_Comprimida = ""; 


        string sIdUMedica_Base = "000000"; 
        string sCLUES_Base = "SSA000000";

        string sUrlChecador = "";
        //DllFarmaciaSoft.wsFarmacia.wsCnnCliente validarHuella = null;
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


        #region INFORMACION DE VENTAS 
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        string sIdPublicoGral = GnFarmacia.PublicoGral;
        string sIdSeguroPopular = GnFarmacia.SeguroPopular;
        bool bValidarSeguroPopular = GnFarmacia.ValidarInformacionSeguroPopular;
        //bool bEsSeguroPopular = false;
        //bool bValidarBeneficioSeguroPopular = GnFarmacia.ValidarBeneficioSeguroPopular;
        bool bDispensarSoloCuadroBasico = GnFarmacia.DispensarSoloCuadroBasico;
        bool bImplementaCodificacion = GnFarmacia.ImplementaCodificacion_DM;
        bool bImplementaReaderDM = GnFarmacia.ImplementaReaderDM;
        bool bValidarConsumoDeClaves_Programacion = GnFarmacia.ValidarConsumoClaves_Programacion;
        bool bValidarConsumoDeClaves_ProgramaAtencion = GnFarmacia.ValidarConsumoClaves_ProgramaAtencion;
        bool bForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma = false;
        int iNumeroDeCopias = GnFarmacia.NumeroDeCopiasTickets;
        string sValorGrid = "";
        bool bEsIdProducto_Ctrl = false;
        Cols ColActiva = Cols.Ninguna;
        string sCodigoEAN_Seleccionado = "";
        string sListaClavesSSA_RecetaElectronica = ""; 

        bool bFolioGuardado = false;
        TiposDeUbicaciones tpUbicacion = TiposDeUbicaciones.Todas;
        clsCodigoEAN EAN = new clsCodigoEAN();
        FrmRevisarCodigosEAN RevCodigosEAN = new FrmRevisarCodigosEAN();

        #endregion INFORMACION DE VENTAS

        #endregion Variables

        public FrmInformacionOrdenDeAcondicionamiento()
        {
            InitializeComponent();
        }

        public FrmInformacionOrdenDeAcondicionamiento(string IdEstado, string IdFarmacia, string IdCliente, string NombreCliente, string IdSubCliente, string NombreSubCliente)
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
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false); 
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);


            Grid = new clsGrid(ref grdProductos, this);
            Grid.EstiloDeGrid = eModoGrid.ModoRow;
            Grid.BackColorColsBlk = Color.White;
            Grid.AjustarAnchoColumnasAutomatico = true; 
            grdProductos.EditModeReplace = true;

            //validarHuella = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente();
            //validarHuella.Url = General.Url;
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, DtGeneral.EmpresaConectada, IdEstado, IdFarmacia, sPersonal);

        }

        private void FrmInformacionOrdenDeAcondicionamiento_Load(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        #region Botones 
        private void InicializarPantalla()
        {
            lblFolioDeRecetaUnico.Visible = bValidarFoliosUnicosDeRecetas; 
            iMesesMargen = (-1 * 12); 
            CargarServicios(); 
            Fg.IniciaControles();

            leerDatos = new clsLeer();
            leerDatos.DataSetClase = dtsListaProductos_Solicitud;

            Grid.Limpiar(true);
            if (leerDatos.Leer())
            {
                Grid.LlenarGrid(leerDatos.DataSetClase); 
            }



            txtIdBenificiario.Text = sIdBeneficiario;
            txtIdBenificiario_Validating(null, null);

            sIdBeneficioSeguroPopular = sIdBeneficioSeguroPopular == "" ? GnFarmacia.ClaveBeneficio : sIdBeneficioSeguroPopular; 
            txtBeneficio.Text = sIdBeneficioSeguroPopular;
            txtBeneficio_Validating(null, null);

            txtNumeroDeHabitacion.Text = sNumeroDeHabitacion; 
            txtNumeroDeCama.Text = sNumeroDeCama; 
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
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarInformacion())
            {
                bInformacionGuardada = true;
                this.Close();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            bInformacionGuardada = false;
            this.Close(); 
        }
        #endregion Botones

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
            ////if (sFolioVenta != "*")
            ////{
            ////    GetImagenDigitalizada();
            ////}


            ////if (imgDigitizacion == null)
            ////{
            ////    General.msjUser("No existe imagen de receta.");
            ////}
            ////else 
            ////{
            ////    FrmVisorReceta visor = new FrmVisorReceta(imgDigitizacion, imgDigitizacion.Width, imgDigitizacion.Height);
            ////    visor.ShowDialog();
            ////}
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
            cboServicios.Add(query.Farmacia_Servicios(sIdEstado, sIdFarmacia, "CargarServicios()"), true, "IdServicio", "NombreServicio");
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

                leer.DataSetClase = query.ValesEmision_InformacionAdicional(sIdEmpresa, sIdEstado, sIdFarmacia, sFolioVenta, "CargarInformacionAdicionalDeVentas()");


                if (leer.Leer())
                {
                    bValidarFoliosUnicosDeRecetas = false;  //// Solo cuando es información ya guardada 
                    txtIdBenificiario.Text = leer.Campo("IdBeneficiario"); 
                    lblNombre.Text = leer.Campo("Beneficiario");

                    lblFechaNac.Text = General.FechaYMD(leer.CampoFecha("FechaNacimiento"), "-");
                    lblSexo.Text = leer.Campo("SexoAux");
                    lblEdad.Text = leer.Campo("Edad");
                    lblFolioReferencia.Text = leer.Campo("FolioReferencia");
                    lblFechaVigencia.Text = General.FechaYMD(leer.CampoFecha("FechaInicioVigencia"), "-");

                    bVigenciaValida = leer.CampoBool("EsVigente");
                    if (!bVigenciaValida)
                    {
                        General.msjAviso("La Vigencia del Beneficiario ha expirado, no es posible generar la venta.");
                    }

                    bEsActivo = true;
                    if (leer.Campo("Status").ToUpper() == "C")
                    {
                        bEsActivo = false;
                        lblStatus.Visible = true;
                        General.msjUser("El Beneficiario se encuentra cancelado, no es posible generar la venta.");
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



                    FrameBeneficiario.Enabled = false;
                    FrameDatosAdicionales.Enabled = false;

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
                    CargarDatosBenefiario();
                    //txtIdBenificiario.Text = leer.Campo("IdBeneficiario");
                    //lblNombre.Text = leer.Campo("NombreCompleto");
                }
            }
        }

        private void CargarDatosBenefiario()
        {
            txtIdBenificiario.Text = leer.Campo("IdBeneficiario");
            lblNombre.Text = leer.Campo("NombreCompleto");

            lblFechaNac.Text = General.FechaYMD(leer.CampoFecha("FechaNacimiento"), "-");
            lblSexo.Text = leer.Campo("SexoAux");
            lblEdad.Text = leer.Campo("Edad");
            lblFolioReferencia.Text = leer.Campo("FolioReferencia");
            lblFechaVigencia.Text = General.FechaYMD(leer.CampoFecha("FechaFinVigencia"), "-");

            bVigenciaValida = leer.CampoBool("EsVigente");
            if (!bVigenciaValida)
            {
                General.msjAviso("La Vigencia del Beneficiario ha expirado, no es posible generar la venta.");
            }

            bEsActivo = true;
            if (leer.Campo("Status").ToUpper() == "C")
            {
                bEsActivo = false;
                lblStatus.Visible = true;
                General.msjUser("El Beneficiario se encuentra cancelado, no es posible generar la venta.");
            }

            if (bEsActivo)
            {
                if (bValidarBeneficariosAlertas)
                {
                    ValidarBeneficiarioCuentaConAlertas(); 
                }
            }
        }

        private void ValidarBeneficiarioCuentaConAlertas()
        {
            ////string sReferencia = lblFolioReferencia.Text.Replace(".", "-");
            ////int iPosicion = 0; 

            ////iPosicion = sReferencia.IndexOf("-", 0);
            ////sReferencia = sReferencia.Substring(0, iPosicion).Trim(); 


            ////string sSql = string.Format(
            ////    "Select 'Nombre de Beneficiario' = NombreBeneficiario " + 
            ////    "From CFG_VAL_BeneficiariosAlertas (NoLock) " +
            ////    "Where Status = 'A' and FolioReferencia like '%{0}%' ", sReferencia);

            ////if (!leer.Exec(sSql))
            ////{
            ////    Error.GrabarError(leer, "ValidarBeneficiarioCuentaConAlertas");
            ////    General.msjError("Ocurrió un error al validar las alertas de beneficiarios.");
            ////}
            ////else
            ////{
            ////    if (leer.Leer())
            ////    {
            ////        FrmBeneficiarios_Alertas alertas = new FrmBeneficiarios_Alertas(leer.DataSetClase);
            ////        alertas.ShowDialog(this);
            ////    }
            ////}
        }

        private void txtIdBenificiario_Validating(object sender, CancelEventArgs e)
        {
            bEsActivo = false;
            lblStatus.Visible = false;
            lblStatus.Text = "CANCELADO";

            if (txtIdBenificiario.Text.Trim() != "")
            {
                leer.DataSetClase = query.Beneficiarios(sIdEstado, sIdFarmacia, sIdCliente, sIdSubCliente, txtIdBenificiario.Text, "txtIdBenificiario_Validating");
                if (leer.Leer())
                {
                    CargarDatosBenefiario(); 
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
                    General.msjUser("Clave de Beneficiario no encontrada, verifique.");
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
        }

        private void txtIdMedico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Medicos(sIdEstado, sIdFarmacia, "txtIdMedico_KeyDown");
                if (leer.Leer())
                {
                    if (leer.Campo("Status") == "C")
                    {
                        General.msjAviso(" El Médico : " + leer.Campo("IdMedico") + " -- " + leer.Campo("NombreCompleto") + ". Esta cancelado. ");
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
                        General.msjAviso(" El Médico : " + leer.Campo("IdMedico") + " -- " + leer.Campo("NombreCompleto") + ". Esta cancelado. ");
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
                    General.msjUser("Clave de Médico no encontrada, verifique."); 
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
                leer.DataSetClase = Ayuda.DiagnosticosCIE10("txtIdDiagnostico_Validating");
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
                    General.msjUser(string.Format("Formato de Diagnóstico incorrecto, el diagnóstico debe ser de {0} caracteres, verifique.", GnFarmacia.ClaveDiagnosticoCaracteres));

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
                        General.msjUser("Clave de Diagnóstico no encontrada, verifique.");

                        ////if (!bCerrarInformacionAdicionalAutomaticamente)
                        ////{
                        ////    e.Cancel = true;
                        ////}
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
                    cboAreas.Add(dtsAreas.Tables[0].Select(sWhere), true, "IdArea", "NombreArea");
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
                leer.DataSetClase = Ayuda.BeneficiosSP("txtIdDiagnostico_Validating");
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
                leer.DataSetClase = Ayuda.UnidadesMedicasJurisccion(DtGeneral.EstadoConectado, DtGeneral.Jurisdiccion, "txtUMedica_KeyDown");
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
                    General.msjUser("Clave de Unidad Medica  no encontrada, verifique.");
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

        private void FrmInformacionOrdenDeAcondicionamiento_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmInformacionOrdenDeAcondicionamiento_FormClosing(object sender, FormClosingEventArgs e)
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

            if (bRegresa && txtNumeroDeHabitacion.Text.Trim() == "")
            {
                bRegresa = false; 
            }

            if (bRegresa && txtNumeroDeCama.Text.Trim() == "")
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

            //if (bRegresa && txtBeneficio.Text.Trim() == "")
            //{
            //    bRegresa = false;
            //}

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

            Actualizar_Datos_De_Salida(); 

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

            ////if (!bFolioValido)
            ////{
            ////    if (DtGeneral.ConfirmacionConHuellas)
            ////    {
            ////        if (General.msjConfirmar(string.Format("El folio de receta {0} ya fue registrado anteriormente.\n\n para continuar se requieren permisos especiales, ¿ Desea asignarlos ?", txtNumReceta.Text.Trim())) == DialogResult.Yes)
            ////        {
            ////            //bCapturaCompleta = VerificarPermisos();
            ////            sMsjNoEncontrado = "El usuario no tienen permisos para autorizar el uso de folio de receta mas de una ocasión, verifique por favor.";
            ////            ////bCapturaCompleta = opPermisosEspeciales.VerificarPermisos("FOLIO_RECETA_DUPLICADA", sMsjNoEncontrado);
            ////            bCapturaCompleta = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("FOLIO_RECETA_DUPLICADA", sMsjNoEncontrado);
            ////        }
            ////    }
            ////    else
            ////    {
            ////        General.msjAviso(string.Format("El folio de receta {0} ya fue registrado anteriormente, verifique.", txtNumReceta.Text.Trim()));
            ////    }
            ////}

            if (!bRegresa && bFolioValido)
            {
                if (General.msjConfirmar("La información requerida en esta pantalla esta incompleta.\n\n¿ Desea cerrar la pantalla, no es posible generar la venta sin esta información ?") == DialogResult.Yes)
                {
                    bRegresa = true;
                }
            } 


            // Asignar los valores de salida 
            //sIdCliente = sIdCliente;
            //sIdSubCliente = sIdSubCliente;
            sIdBeneficiario = txtIdBenificiario.Text;
            sNombre_Beneficiario = lblNombre.Text;

            sNumeroDeHabitacion = txtNumeroDeHabitacion.Text;
            sNumeroDeCama = txtNumeroDeCama.Text; 
            sNumReceta = txtNumReceta.Text;
            dtpFechaReceta = dtpFechaDeReceta.Value;
            sIdTipoDispensacion = cboTipoDeSurtimiento.Data; 
            sIdMedico = txtIdMedico.Text;
            sNombre_Medico = lblMedico.Text; 
            sIdDiagnostico = txtIdDiagnostico.Text;
            sIdServicio = cboServicios.Data;
            sIdArea = cboAreas.Data;
            sReferenciaObserv = txtRefObservaciones.Text;
            sIdBeneficioSeguroPopular = txtBeneficio.Text.Trim(); 

            return bRegresa;
        }

        private void FrmInformacionOrdenDeAcondicionamiento_Activated(object sender, EventArgs e)
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

        #region Grid
        private void validarSeleccionRecetaElectronica()
        {
            bool bActivar = false;

            //if (GnFarmacia.ImplementaInterfaceExpedienteElectronico)
            //{
            //    if (RecetaElectronica.Receta.InformacionCargada)
            //    {
            //        bActivar = Grid.GetValue(1, (int)Cols.CodigoEAN) == "";
            //    }
            //}

            //btnRecetasElectronicas.Enabled = bActivar;
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            try
            {
                Cols iCol = (Cols)Grid.ActiveCol;
                switch (iCol)
                {
                    case Cols.CodigoEAN:
                        ObtenerDatosProducto();
                        break;
                }
            }
            catch (Exception ex)
            {
                //General.msjError("01 "  + ex.Message); 
            }
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            try
            {
                sValorGrid = Grid.GetValue(Grid.ActiveRow, (int)Cols.CodigoEAN);
            }
            catch (Exception ex)
            {
                //General.msjError("02 " + ex.Message);
            }

            //switch (Grid.ActiveCol)
            //{
            //    case 1: // Si se cambia el Codigo, se limpian las columnas
            //        {
            //            limpiarColumnas();
            //        }
            //        break;
            //}
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            bool bAgregarRenglon = false;

            if (!bFolioGuardado)
            {
                if (lblCancelado.Visible == false)
                {
                    try
                    {
                        if ((Grid.ActiveRow == Grid.Rows) && e.AdvanceNext)
                        {
                            if (!bEsIdProducto_Ctrl)
                            {
                                if (!(bImplementaCodificacion && bImplementaReaderDM))
                                {
                                    bAgregarRenglon = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        General.msjError("03 " + ex.Message);
                    }
                }
            }

            ////// Jesus.Diaz 2K151029.1410 
            if (bAgregarRenglon)
            {
                if (Grid.GetValue(Grid.ActiveRow, 1) != "" && Grid.GetValue(Grid.ActiveRow, 3) != "")
                {
                    Grid.Rows = Grid.Rows + 1;
                    Grid.ActiveRow = Grid.Rows;
                    Grid.SetActiveCell(Grid.Rows, 1);
                    ObtenerDatosProducto();
                }
            }
        }

        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)Grid.ActiveCol;
            int iRowActivo = Grid.ActiveRow;

            switch (ColActiva)
            {
                case Cols.CodigoEAN:
                case Cols.Descripcion:
                case Cols.Cantidad:

                    if (e.KeyCode == Keys.F1)
                    {
                        if (!bEsIdProducto_Ctrl)
                        {
                            sValorGrid = Grid.GetValue(Grid.ActiveRow, (int)Cols.CodigoEAN);
                            leer.DataSetClase = Ayuda.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, bDispensarSoloCuadroBasico, "grdProductos_KeyDown_1");
                            if (leer.Leer())
                            {
                                Grid.SetValue(Grid.ActiveRow, 1, leer.Campo("CodigoEAN"));
                                ObtenerDatosProducto();
                                //CargarDatosProducto();
                            }
                        }
                    }

                    if (e.KeyCode == Keys.Delete)
                    {
                        Grid.DeleteRow(Grid.ActiveRow); 
                        ////if (!bEsIdProducto_Ctrl)
                        ////{
                        ////    removerLotes();
                        ////}
                        ////validarSeleccionRecetaElectronica();
                    }

                    //// Administracion de Mach4 
                    if (e.KeyCode == Keys.F11)
                    {
                        ActualizarColorFondo();
                    }
                    break;
            }
        }

        private bool ValidarSeleccionCodigoEAN(string Codigo)
        {
            bool bRegresa = true;

            sCodigoEAN_Seleccionado = Codigo;

            sCodigoEAN_Seleccionado = RevCodigosEAN.VerificarCodigosEAN(Codigo, false);
            bRegresa = RevCodigosEAN.CodigoSeleccionado;


            return bRegresa;
        }

        private void ObtenerDatosProducto()
        {
            ObtenerDatosProducto(Grid.ActiveRow, true);
        }

        private void ObtenerDatosProducto(int Renglon, bool BuscarInformacion)
        {
            string sCodigo = "", sSql = "";
            bool bCargarDatosProducto = true;
            string sMsj = "";

            sCodigo = Grid.GetValue(Renglon, (int)Cols.CodigoEAN);
            if (EAN.EsValido(sCodigo) && sCodigo != "")
            {
                if (!GnFarmacia.ValidarSeleccionCodigoEAN(sCodigo, ref sCodigoEAN_Seleccionado))
                {
                    Grid.LimpiarRenglon(Renglon);
                    Grid.SetActiveCell(Renglon, (int)Cols.CodigoEAN);
                }
                else
                {
                    sCodigo = sCodigoEAN_Seleccionado;
                    sSql = string.Format("Exec Spp_ProductoVentasFarmacia " +
                        " @Tipo = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdCodigo = '{3}', @CodigoEAN = '{4}', " +
                        " @IdEstado = '{5}', @IdFarmacia = '{6}', @EsSectorSalud = '{7}', @EsClienteIMach = '{8}', @ClavesRecetaElectronica = '{9}',  " +
                        " @INT_OPM_ProcesoActivo = '{10}' ",
                        (int)TipoDeVenta.Credito, sIdCliente, sIdSubCliente,
                        Fg.PonCeros(sCodigo, 13), sCodigo.Trim(),
                        Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), 1, 0, sListaClavesSSA_RecetaElectronica,
                        Convert.ToInt32(GnFarmacia.INT_OPM_ProcesoActivo));
                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "ObtenerDatosProducto()");
                        General.msjError("Ocurrió un error al obtener la información del Producto.");
                    }
                    else
                    {
                        if (!leer.Leer())
                        {
                            General.msjUser("Producto no encontrado ó no esta Asignado a la Farmacia.");
                            Grid.LimpiarRenglon(Renglon);
                        }
                        else
                        {
                            if (!leer.CampoBool("EsDeFarmacia"))
                            {
                                bCargarDatosProducto = false;
                                sMsj = "El Producto " + leer.Campo("Descripcion") + " no esta registrado en la Farmacia, verifique.";
                            }
                            else
                            {
                                if (bDispensarSoloCuadroBasico)
                                {
                                    if (!leer.CampoBool("DCB"))
                                    {
                                        bCargarDatosProducto = false;
                                        sMsj = "El Producto " + leer.Campo("Descripcion") + " no esta dentro del Cuadro Básico Autorizado, verifique.";
                                    }
                                }
                            }

                            if (GnFarmacia.ImplementaInterfaceExpedienteElectronico)
                            {
                                if (RecetaElectronica.Receta.InformacionCargada)
                                {
                                    if (!leer.CampoBool("EsDeRecetaElectronica"))
                                    {
                                        bCargarDatosProducto = false;
                                        sMsj = string.Format("La Clave SSA {0} no esta incluida en la receta electrónica.", leer.Campo("ClaveSSA"));
                                    }
                                }
                            }

                            if (!bCargarDatosProducto)
                            {
                                General.msjUser(sMsj);
                                Grid.LimpiarRenglon(Renglon);
                                Grid.SetActiveCell(Renglon, (int)Cols.CodigoEAN);
                            }
                            else
                            {
                                CargaDatosProducto(Renglon, BuscarInformacion);
                            }
                        }
                    }
                }
            }
            else
            {
                //General.msjError(sMsjEanInvalido);
                Grid.LimpiarRenglon(Renglon);
                Grid.ActiveCelda(Renglon, (int)Cols.CodigoEAN);
                SendKeys.Send("");
            }

            validarSeleccionRecetaElectronica();
        }

        private void ActualizarColorFondo()
        {
            ////if (IMach4.EsClienteIMach4)
            ////{
            ////    FrmColorProductosIMach myColor = new FrmColorProductosIMach();
            ////    myColor.ShowDialog();
            ////    Color colorBack = GnFarmacia.ColorProductosIMach;

            ////    for (int i = 1; i <= Grid.Rows; i++)
            ////    {
            ////        if (Grid.GetValueBool(i, (int)Cols.EsIMach4))
            ////        {
            ////            Grid.ColorRenglon(i, colorBack);
            ////        }
            ////    }
            ////}
        }

        private bool validarProductoCtrlVales(string CodigoEAN)
        {
            bool bRegresa = true;
            bool bEsCero = false;
            // string sDato = "";

            bEsCero = CodigoEAN == "0000000000000" ? true : false;
            if (bEsCero)
            {
                bEsIdProducto_Ctrl = true;
                if (!GnFarmacia.EmisionDeValesCompletos)
                {
                    bEsIdProducto_Ctrl = false;
                    bRegresa = false;
                    General.msjUser("La unidad no esta configurada para manejar este Producto, verifique.");
                }
            }

            return bRegresa;
        }

        private void CargaDatosProducto()
        {
            CargaDatosProducto(Grid.ActiveRow, true);
        }

        private void CargaDatosProducto(int Renglon, bool BuscarInformacion)
        {
            int iRowActivo = Renglon; //// Grid.ActiveRow;           
            int iColEAN = (int)Cols.CodigoEAN;
            bool bEsMach4 = false;
            string sCodEAN = leer.Campo("CodigoEAN");

            if (lblCancelado.Visible == false)
            {
                if (sValorGrid != sCodEAN)
                {
                    if (validarProductoCtrlVales(sCodEAN))
                    {
                        if (!Grid.BuscaRepetido(sCodEAN, iRowActivo, iColEAN))
                        {
                            Grid.SetValue(iRowActivo, iColEAN, sCodEAN);
                            Grid.SetValue(iRowActivo, (int)Cols.Codigo, leer.Campo("IdProducto"));
                            Grid.SetValue(iRowActivo, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                            Grid.SetValue(iRowActivo, (int)Cols.Descripcion, leer.Campo("Descripcion"));
                            Grid.SetValue(iRowActivo, (int)Cols.Cantidad, 0);

                            Grid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.CodigoEAN);

                            Application.DoEvents(); //// Asegurar que se refresque la pantalla 

                            // Grid.SetActiveCell(Grid.iRowActivo, 1);
                            Grid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);
                        }
                        else
                        {
                            General.msjUser("El artículo ya se encuentra capturado en otro renglón.");
                            Grid.SetValue(Grid.ActiveRow, 1, "");
                            Grid.SetActiveCell(Grid.ActiveRow, 1);
                            Grid.EnviarARepetido();
                        }
                    }
                }
                else
                {
                    // Asegurar que no cambie el CodigoEAN
                    Grid.SetValue(iRowActivo, iColEAN, sCodEAN);
                }
            }

            grdProductos.EditMode = false;
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= Grid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                Grid.SetValue(Grid.ActiveRow, i, "");
            }
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= Grid.Rows; i++) //Renglones.
            {
                if (Grid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                {
                    Grid.DeleteRow(i);
                }
            }

            if (Grid.Rows == 0) // Si No existen renglones, se inserta 1.
            {
                Grid.AddRow();
            }
        }
        #endregion Grid

        #region 
        private void Actualizar_Datos_De_Salida()
        {
            dtsListaProductos_Solicitud = clsInformacion_OrdenDeAcondicionamiento.PrepararDtsProductos();

            for (int i = 1; i <= Grid.Rows; i++)
            {
                object[] objRow = {
                    Grid.GetValue(i, (int)Cols.CodigoEAN),  
                    Grid.GetValue(i, (int)Cols.Codigo),  
                    Grid.GetValue(i, (int)Cols.ClaveSSA),
                    Grid.GetValue(i, (int)Cols.Descripcion),  
                    Grid.GetValueInt(i, (int)Cols.Cantidad),
                    Grid.GetValueFecha(i, (int)Cols.FechaAdmin), 
                    Grid.GetValueFecha(i, (int)Cols.HoraAdmin)
                                  };
                dtsListaProductos_Solicitud.Tables[0].Rows.Add(objRow);
            }
        }
        #endregion 
    }
    #endregion Form
    
    public class clsInformacion_OrdenDeAcondicionamiento
    {
        private string sEmpresa = ""; 
        private string sIdEstado = ""; 
        private string sIdFarmacia = "";

        FrmInformacionOrdenDeAcondicionamiento f; 
        basGenerales Fg = new basGenerales();

        ////FrmPDD_03_Datos_Documento Doctos;
        ////FrmPDD_04_Datos_Diagnostico Diagnosticos;
        ////FrmPDD_02_Datos_Beneficiario Beneficiarios;
        ////FrmPDD_05_Datos_ServiciosAreas Serv_Areas;

        private bool bInformacionGuardada = false;
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
        string sNombre_Beneficiario = "";
        string sNombre_Medico = "";
        string sNumeroDeHabitacion = ""; 
        string sNumeroDeCama = ""; 


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
        DataSet dtsListaProductos_Solicitud = new DataSet(); 

        bool bImagenDigitalizada = false; 
        Image imgDigitizacion; 
        string sImg_Original = "";
        string sImg_Comprimida = "";

        public clsInformacion_OrdenDeAcondicionamiento(string IdEmpresa, string IdEstado, string IdFarmacia)
        {
            this.sEmpresa = IdEmpresa; 
            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;

            dtsListaProductos_Solicitud = PrepararDtsProductos(); 
        }

        #region Propiedades 
        public bool InformacionGuardada
        {
            get { return bInformacionGuardada; }
        }

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
                if (bCapturaCompletaDocto && bCapturaCompletaBenef)
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
            get { return Fg.PonCeros(sIdBeneficiario, 8); }
        }

        public string Beneficiario_Nombre
        {
            get { return sNombre_Beneficiario; }
        }

        public string Receta
        {
            get { return sNumReceta; }
        }

        public string NumeroDeHabitacion
        {
            get { return sNumeroDeHabitacion; }
        }

        public string NumeroDeCama
        {
            get { return sNumeroDeCama; }
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

        public string Medico_Nombre
        {
            get { return sNombre_Medico; }
        }

        public string IdBeneficio
        {
            get 
            {
                if (sIdBeneficioSeguroPopular == "")
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
                if (sIdDiagnostico == "")
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
                if (sIdServicio == "")
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
                if (sIdArea == "")
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

        public DataSet ListaProductos_Solicitud
        {
            get { return dtsListaProductos_Solicitud; }
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
        #endregion Propiedades

        public void CargarInformacion(DataRow Datos, DataSet Detalles)
        {
            clsLeer datos_01_Informacion = new clsLeer();
            clsLeer datos_02_Productos = new clsLeer();
            clsLeer datos_02_Productos_Auxiliar = new clsLeer();

            datos_01_Informacion.DataRowClase = Datos;
            datos_02_Productos_Auxiliar.DataSetClase = Detalles; 
            dtsListaProductos_Solicitud = PrepararDtsProductos(); 

            if (datos_01_Informacion.Leer())
            {
                try
                {
                    datos_02_Productos.DataRowsClase = datos_02_Productos_Auxiliar.Tabla(1).Select(
                        string.Format(" FolioSolicitud = '{0}' and Consecutivo = '{1}' ",
                        datos_01_Informacion.Campo("FolioSolicitud"), datos_01_Informacion.Campo("Consecutivo"))
                        );
                }
                catch { 
                }

                sIdBeneficiario = datos_01_Informacion.Campo("IdBeneficiario");
                sIdMedico = datos_01_Informacion.Campo("IdMedico");

                sNumReceta = datos_01_Informacion.Campo("NumReceta");
                sFechaReceta = datos_01_Informacion.Campo("FechaReceta");

                sNumeroDeHabitacion = datos_01_Informacion.Campo("NumeroDeHabitacion");
                sNumeroDeCama = datos_01_Informacion.Campo("NumeroDeCama");
                sIdTipoDispensacion = datos_01_Informacion.Campo("IdTipoDeDispensacion");

                sCLUES_Foranea = datos_01_Informacion.Campo("IdUMedica");
                sIdDiagnostico = datos_01_Informacion.Campo("IdDiagnostico");
                sIdBeneficioSeguroPopular = datos_01_Informacion.Campo("IdBeneficio"); 

                sIdServicio = datos_01_Informacion.Campo("IdServicio");
                sIdArea = datos_01_Informacion.Campo("IdArea");
                sReferenciaObserv = datos_01_Informacion.Campo("RefObservaciones");


                while (datos_02_Productos.Leer())
                {
                    object[] obj = {
                        datos_02_Productos.Campo("CodigoEAN"),
                        datos_02_Productos.Campo("IdProducto"),
                        datos_02_Productos.Campo("ClaveSSA"),
                        datos_02_Productos.Campo("Descripcion"),
                        datos_02_Productos.CampoInt("CantidadSolicitada"), 
                        datos_02_Productos.CampoFecha("FechaHora_De_Administracion"),
                        datos_02_Productos.CampoFecha("FechaHora_De_Administracion") 
                                   };
                    dtsListaProductos_Solicitud.Tables[0].Rows.Add(obj);
                }
            }


        }

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

            f = new FrmInformacionOrdenDeAcondicionamiento(sIdEstado, sIdFarmacia, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente);

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
            f.sNumeroDeHabitacion = sNumeroDeHabitacion;
            f.sNumeroDeCama = sNumeroDeCama; 
            f.sNumReceta = sNumReceta;
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
            f.dtsListaProductos_Solicitud = dtsListaProductos_Solicitud; 
            f.ShowDialog(); // Mostrar la pantalla 


            bInformacionGuardada = f.bInformacionGuardada;
            sIdCliente = f.sIdCliente;
            sIdSubCliente = f.sIdSubCliente;
            sIdBeneficiario = f.sIdBeneficiario;
            sNombre_Beneficiario = f.sNombre_Beneficiario;

            sNumeroDeHabitacion = f.sNumeroDeHabitacion; 
            sNumeroDeCama = f.sNumeroDeCama; 
            sNumReceta = f.sNumReceta;
            dtpFechaReceta = f.dtpFechaReceta;
            sIdTipoDispensacion = f.sIdTipoDispensacion;
            sCLUES_Foranea = f.sCLUES_Foranea == "" ? "000000" : f.sCLUES_Foranea; 
            sIdMedico = f.sIdMedico;
            sNombre_Medico = f.sNombre_Medico; 

            sIdDiagnostico = f.sIdDiagnostico;
            sIdDiagnosticoClave = f.sIdDiagnosticoClave; 
            sIdServicio = f.sIdServicio;
            sIdArea = f.sIdArea;
            sReferenciaObserv = f.sReferenciaObserv;
            bCapturaCompleta = f.bCapturaCompleta;
            bVigenciaValida = f.bVigenciaValida;
            bEsActivo = f.bEsActivo;
            sIdBeneficioSeguroPopular = f.sIdBeneficioSeguroPopular;
            dtsListaProductos_Solicitud = f.dtsListaProductos_Solicitud; 

            bImagenDigitalizada = f.bImagenDigitalizada;
            imgDigitizacion = f.imgDigitizacion;
            sImg_Original = f.sImg_Original;
            sImg_Comprimida = f.sImg_Comprimida; 

            f.Close(); f = null;
        }

        private static Type GetType(TypeCode TipoDato)
        {
            return Type.GetType("System." + TipoDato.ToString());
        }

        public static DataSet PrepararDtsProductos()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtLote = new DataTable("Productos");

            dtLote.Columns.Add("CodigoEAN", GetType(TypeCode.String));
            dtLote.Columns.Add("IdProducto", GetType(TypeCode.String));
            dtLote.Columns.Add("ClaveSSA", GetType(TypeCode.String));
            dtLote.Columns.Add("Descripcion", GetType(TypeCode.String));
            dtLote.Columns.Add("Cantidad", GetType(TypeCode.Int32));

            dtLote.Columns.Add("FechaAdmin", GetType(TypeCode.DateTime));
            dtLote.Columns.Add("HoraAdmin", GetType(TypeCode.DateTime));

            dts.Tables.Add(dtLote);

            return dts.Clone();
        }    
    }
}
