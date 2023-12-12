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
using Farmacia.Ventas; 


namespace Farmacia.Digitalizacion
{
    public partial class FrmVentas_Documentos : FrmBaseExt
    {
        enum Cols
        {
            Partida = 1, 
            Tipo = 2, 
            TipoDescripcion = 3,
            Remover = 4, 
            Actualizar = 5, 
            Visualizar = 6 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);        
        clsLeer leer;
        clsLeer leer2;
        clsLeer leer3;
        clsLeer leer4;
        clsDatosCliente DatosCliente;
        clsGrid grid; 

        DllFarmaciaSoft.clsConsultas consultas;
        DllFarmaciaSoft.clsAyudas Ayuda;

        clsInformacionVentas InfVtas;

        string sNombreCamara = "CamaraDigitalizacion";
        SC_SolutionsSystem.QRCode.Cam_Reader reader;
        bool bExisteLector = false; //DtGeneral.Camaras.ExisteCamara("CamaraDigitalizacion");
        bool bExisteDB_Digitalizacion = false; 

        int iIdImagen = 0; 
        private byte[] bufferImagen = null;
        public bool bImagenDigitalizada = false;
        public Image imgDigitizacion;
        public string sImg_Original = "";
        public string sImg_Comprimida = "";

        Dictionary<int, ImagenDigitalizada> listaDeImagenes = new Dictionary<int, ImagenDigitalizada>();
        bool bImplementaDigitalizacionDepurar = GnFarmacia.ImplementaDigitalizacionDepurarDirectorio;
        string sDirectorio = Application.StartupPath + @"\Digitalizacion";

        #region Variables
        string sEmpresa = DllFarmaciaSoft.DtGeneral.EmpresaConectada;
        string sEstado = DllFarmaciaSoft.DtGeneral.EstadoConectado;
        string sFarmacia = DllFarmaciaSoft.DtGeneral.FarmaciaConectada;

        string sIdPublicoGral = "0001"; //DllFarmaciaSoft.DtGeneral.PublicoGral;

        string sIdPersonal = DllFarmaciaSoft.DtGeneral.IdPersonal;

        bool bPermitirCapturaBeneficiariosNuevos = true;
        bool bImportarBeneficiarios = true;
        bool bEsSeguroPopular = true;
        string sFolioVta = "", sFolioMovto = "", sMensaje = "";
        bool bNuevo = false;
        #endregion Variables

        #region Permisos Especiales 
        string sPermisoVentas = "ADT_MODIFICAR_INF_VENTAS";
        bool bPermisoVentas = false;
        #endregion Permisos Especiales

        public FrmVentas_Documentos()
        {            
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            leer3 = new clsLeer(ref cnn);
            leer4 = new clsLeer(ref cnn);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");

            consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, General.DatosApp, this.Name, false);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, General.DatosApp, this.Name, false);

            grid = new clsGrid(ref grdDigitalizacion, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            grid.AjustarAnchoColumnasAutomatico = true;

            DepurarDirectorioDeDigitalizacion(); 
        }

        private void DepurarDirectorioDeDigitalizacion()
        {
            if (bImplementaDigitalizacionDepurar && !DtGeneral.EsEquipoDeDesarrollo)
            {
                if (Directory.Exists(sDirectorio))
                {
                    try
                    {
                        string[] sFiles = Directory.GetFiles(sDirectorio, "*.*");
                        foreach (string delFile in sFiles)
                        {
                            try
                            {
                                File.Delete(delFile); 
                            }
                            catch { }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void FrmVentas_Documentos_Load(object sender, EventArgs e)
        {
            SolicitarPermisosUsuario();

            if (GnFarmacia.ImplementaDigitalizacion)
            {
                //DtGeneral.Camaras.Actualizar();  
                bExisteLector = DtGeneral.Camaras.ExisteCamara(sNombreCamara);

                if (bExisteLector)
                {
                    sNombreCamara = DtGeneral.Camaras.GetCamara(sNombreCamara);
                }

                //btnDigitalizar.Enabled = bExisteLector;
                //btnDigitalizarReceta.Enabled = bExisteLector;
                //sNombreCamara = DtGeneral.Camaras.GetCamara(sNombreCamara);
            }


            InicializarPantalla(); 
        }

        private void FrmVentas_Documentos_Shown(object sender, EventArgs e)
        {
            if (!GnFarmacia.ImplementaDigitalizacion)
            {
                //btnDigitalizar.Enabled = false;
                //btnVisor.Enabled = false;
                IniciarToolBar(false, false);

                General.msjAviso("La farmacia actual no cuenta con la configuración requerida para digitalizar documentos.");
            }
            else
            {
                if (!validarExiste__BD_Digitalizacion())
                {
                    General.msjAviso("No se encontro la Base de Datos de Digitalización."); 
                }
            }
        }

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            bPermisoVentas = DtGeneral.PermisosEspeciales.TienePermiso(sPermisoVentas);
        }
        #endregion Permisos de Usuario

        #region Botones
        private void InicializarPantalla()
        {
            iIdImagen = 0; 
            listaDeImagenes = new Dictionary<int, ImagenDigitalizada>(); 
            grid.Limpiar(false);
            Fg.IniciaControles(this, true);
            IniciarToolBar(true, false);
            lblCorregido.Visible = false;

            dtpFechaRegistro.Enabled = false; 

            //// Informacion detallada de la venta 
            InfVtas = new clsInformacionVentas(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            //btnDigitalizar.Enabled = bExisteLector;
            //btnDigitalizarReceta.Enabled = bExisteLector;

            bPermitirCapturaBeneficiariosNuevos = false;
            bImportarBeneficiarios = false;
            bNuevo = false;

            txtCte.Enabled = false;
            txtSubCte.Enabled = false;
            txtPro.Enabled = false;
            txtSubPro.Enabled = false;

            txtFolio.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (ValidaDatos())
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    cnn.IniciarTransaccion();

                    bContinua = ActualizaVtaInformacionDigitalizacion();

                    if (bContinua)
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Información de digitalización guardada satisfactoriamente.");
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la Información.");
                    }
                    cnn.Cerrar();
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
        }

        #endregion Botones

        #region Eventos
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolio.Text.Trim() != "")
            {
                leer.DataSetClase = consultas.FolioEnc_Ventas(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Folio de Venta no encontrado, verifique.");
                    txtFolio.Text = "";
                    txtFolio.Focus();
                }
                else
                {
                    //btnDigitalizar.Enabled = bExisteLector;
                    //btnDigitalizarReceta.Enabled = bExisteLector;

                    if (leer.CampoInt("TipoDeVenta") != 2)
                    {
                        General.msjUser("El folio de venta capturado no es de venta de credito, verifique.");
                        txtFolio.Text = "";
                        txtFolio.Focus();
                    }
                    else
                    {
                        txtFolio.Enabled = false; 
                        txtFolio.Text = leer.Campo("Folio");
                        txtCte.Text = leer.Campo("IdCliente");
                        lblCte.Text = leer.Campo("NombreCliente");
                        txtSubCte.Text = leer.Campo("IdSubCliente");
                        lblSubCte.Text = leer.Campo("NombreSubCliente");
                        txtPro.Text = leer.Campo("IdPrograma");
                        lblPro.Text = leer.Campo("Programa");
                        txtSubPro.Text = leer.Campo("IdSubPrograma");
                        lblSubPro.Text = leer.Campo("SubPrograma");


                        //toolTip.SetToolTip(lblCte, lblCte.Text);
                        //toolTip.SetToolTip(lblSubCte, lblSubCte.Text);
                        //toolTip.SetToolTip(lblPro, lblPro.Text);
                        //toolTip.SetToolTip(lblSubPro, lblSubPro.Text);

                        if (leer.Campo("Status") == "C")
                        {
                            lblCancelado.Visible = true;
                        }
                        else
                        {
                            IniciarToolBar(true, true);
                            DescargarImagenes(); 


                            if (leer.CampoInt("FolioCierre") != 0) 
                            {
                                IniciarToolBar(true, false);
                                General.msjAviso("El folio pertenece a un periodo cerrado, NO es posible realizar cambios.");
                            }
                        }
                    }
                }
            }
        }

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                ////case Keys.G:
                ////    if (btnGuardar.Enabled)
                ////        btnGuardar_Click(null, null);
                ////    break;

                ////case Keys.N:
                ////    if (btnNuevo.Enabled)
                ////        btnGuardar_Click(null, null);
                ////    break;


                default:
                    break;
            }
        }

        private void FrmVentas_Documentos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                TeclasRapidas(e); 
            }


            switch (e.KeyCode)
            {
                case Keys.F4:
                    if ( btnDigitalizar.Enabled ) Digitalizar(0, 1);  
                    break;

                case Keys.F6:
                    if (btnDigitalizarReceta.Enabled) Digitalizar(0, 2);  
                    break;

                default:
                    // base.OnKeyDown(e);
                    break;
            }
        }
        #endregion Eventos

        #region Funciones
        private void IniciarToolBar(bool Nuevo, bool Guardar)
        {
            btnNuevo.Enabled = Nuevo; 
            btnGuardar.Enabled = Guardar;
            btnDigitalizar.Enabled = false;
            btnDigitalizarReceta.Enabled = false; 

            if (Guardar)
            {
                btnDigitalizar.Enabled = bExisteLector;
                btnDigitalizarReceta.Enabled = bExisteLector; 
            }

            if (!bExisteDB_Digitalizacion)
            {
                btnGuardar.Enabled = false;
                btnDigitalizar.Enabled = false;
                btnDigitalizarReceta.Enabled = false; 
            }
        }

        private bool ActualizaVtaInformacionDigitalizacion()
        {
            bool bRegresa = true;
            string sSql = "";
            int iImagen = 0;
            
            sSql = string.Format("Delete From SII_Digitalizacion.dbo.VentasDigitalizacion  " + 
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioVenta = '{3}' ", 
                DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), txtFolio.Text); 
            if (!leer.Exec(sSql))
            {
                bRegresa = false; 
            }
            else
            {
                ////clsGrabarError.LogFileError(string.Format("Digitalizando folio: {0}", txtFolio.Text));
                foreach (ImagenDigitalizada img in listaDeImagenes.Values)
                {
                    iImagen++;
                    imgDigitizacion = Fg.ConvertirBase64ToImage(img.Img_Original);

                    sSql = string.Format("Exec spp_Mtto_VentasDigitalizacion " +
                        " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}', @IdImagen = '{4}', @TipoDeImagen = '{5}', " +
                        " @ImagenComprimida = '{6}', @ImagenOriginal = '{7}', @Ancho = '{8}', @Alto = '{9}', @iOpcion = '{10}' ",
                         DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                        txtFolio.Text, iImagen, img.TipoDeImagen, img.Img_Comprimida, img.Img_Original, imgDigitizacion.Width, imgDigitizacion.Height, 1);

                    ////clsGrabarError.LogFileError(string.Format("Folio: {0}, Imagen : {1} ", txtFolio.Text, iImagen));
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
                ////clsGrabarError.LogFileError(string.Format("Termino digitalización folio: {0}", txtFolio.Text));
            }

            return bRegresa;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            

            if (txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Venta inválido, verifique.");
                txtFolio.Focus();
            }

            if (bRegresa && txtCte.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Cliente inválida, verifique.");
                txtCte.Focus();
            }

            if (bRegresa && txtSubCte.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de SubCliente inválida, verifique.");
                txtSubCte.Focus();
            }

            if (bRegresa && txtPro.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Programa inválida, verifique.");
                txtPro.Focus();
            }

            if (bRegresa && txtSubPro.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de SubPrograma inválida, verifique.");
                txtSubPro.Focus();
            }

            if (bRegresa)
            {
                if (grid.Rows < 2)
                {
                    bRegresa = false;
                }
                else
                {
                    bRegresa = validarSecuenciaDeImagenes();
                }
            }

            return bRegresa;
        }

        private bool validarSecuenciaDeImagenes()
        {
            bool bRegresa = true;
            int iTipo = 0;
            int iValor = grid.GetValueInt(1, (int)Cols.Tipo);
            bool bValidado = false;


            bRegresa = iValor == 1; 



            //for (int i = 1; i <= grid.Rows; i++)
            //{
                ////iValor = grid.GetValueInt(i, (int)Cols.Tipo);
                ////bValidado = false; 

                ////if (iTipo == 0)
                ////{
                ////    if (iValor == 1)
                ////    {
                ////        iTipo = 1;
                ////        bValidado = true;
                ////        bRegresa = true; 
                ////    }
                ////    else
                ////    {
                ////        bRegresa = false;
                ////        break; 
                ////    }
                ////}


                ////if (!bValidado)
                ////{
                ////    if (iTipo == 1)
                ////    {
                ////        if (iValor == 2)
                ////        {
                ////            iTipo = 2;
                ////            bValidado = true; 
                ////        }
                ////        else
                ////        {
                ////            bRegresa = false;
                ////            break;
                ////        }
                ////    }
                ////}

                ////if (!bValidado)
                ////{
                ////    if (iTipo == 2)
                ////    {
                ////        if (iValor == 1 || iValor == 2)
                ////        {
                ////            iTipo = iValor;
                ////            bValidado = true; 
                ////        }
                ////        else
                ////        {
                ////            bRegresa = false;
                ////            break;
                ////        }
                ////    }
                ////}
            //}

            if (!bRegresa)
            {
                General.msjUser("Secuencia de imagenes incorrecta, la primer imagen digitalizada debe ser un ticket, verifique.");
            }

            return bRegresa; 
        }

        private bool DescargarImagenes()             
        {
            bool bRetorno = false;
            int iRegistro = 0;
            int iTipoImagen = 0;
            byte []byImagen = null;
            int iAncho = 0;
            int iAlto = 0;
            Image imgDown = null; 

            string sSql = string.Format("Select  IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdImagen, TipoDeImagen, FechaDigitalizacion, " + 
                " ImagenComprimida, ImagenOriginal, Ancho, Alto, Status, Actualizado, FechaControl " +
                " From SII_Digitalizacion..VentasDigitalizacion (NoLock) " + 
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioVenta = '{3}' ",
                DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), txtFolio.Text);

            listaDeImagenes = new Dictionary<int, ImagenDigitalizada>();
            grid.Limpiar(false);
            iIdImagen = 0; 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrio un error al descargar las digitalizaciones");
            }
            else
            {
                while (leer.Leer())
                {
                    iAncho = leer.CampoInt("Ancho");
                    iAlto = leer.CampoInt("Alto");

                    sImg_Comprimida = leer.Campo("ImagenComprimida");
                    sImg_Original = leer.Campo("ImagenOriginal");
                    iTipoImagen = leer.CampoInt("TipoDeImagen");

                    byImagen = leer.CampoByte("ImagenOriginal");
                    imgDigitizacion = GetImagen(byImagen, iAncho, iAlto);  
                    //imgDigitizacion = 

                    iRegistro++;
                    iIdImagen++;
                    grid.AgregarRenglon();
                    
                    //iPartida = iIdImagen;

                    grid.SetValue(iRegistro, (int)Cols.Partida, iIdImagen);
                    grid.SetValue(iRegistro, (int)Cols.Tipo, iTipoImagen);
                    grid.SetValue(iRegistro, (int)Cols.TipoDescripcion, iTipoImagen == 1 ? "Ticket" : "Receta");

                    ImagenDigitalizada itemImage = new ImagenDigitalizada();
                    itemImage.Imagen = imgDigitizacion;
                    itemImage.TipoDeImagen = iTipoImagen;
                    itemImage.Img_Comprimida = sImg_Comprimida;
                    itemImage.Img_Original = sImg_Original;

                    listaDeImagenes.Add(iIdImagen, itemImage);

                    
                }
            }

            return bRetorno;
        }

        #endregion Funciones        

        #region Digitalizado
        private bool validarExiste__BD_Digitalizacion() 
        {
            bool bRegresa = false;
            string sSql = " Select Name as BaseDeDatos from master..sysdatabases where name = 'SII_Digitalizacion' ";

            if (leer.Exec(sSql))
            {
                bRegresa = leer.Leer(); 
            }

            bExisteDB_Digitalizacion = bRegresa;
 
            return bRegresa; 
        }

        private Image GetImagen(byte[] ImagenB64, int Ancho, int Alto)
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
            catch (Exception ex)
            {
            }

            return img;
        }

        private void VisualizarImagen(int Partida)
        {
            imgDigitizacion = null;
            if (listaDeImagenes.ContainsKey(Partida))
            {
                ImagenDigitalizada imgDig = new ImagenDigitalizada();
                imgDig = (ImagenDigitalizada)listaDeImagenes[Partida];

                imgDigitizacion = imgDig.Imagen;
                imgDigitizacion = Fg.ConvertirBase64ToImage(imgDig.Img_Original); 


                //listaDeImagenes.Values
            }

            if (imgDigitizacion != null)
            {
                FrmVisorReceta visor = new FrmVisorReceta(imgDigitizacion, imgDigitizacion.Width, imgDigitizacion.Height);
                visor.ShowDialog();
            }
        }

        private void btnDigitalizar_Click(object sender, EventArgs e)
        {
            Digitalizar(0, 1);  
        }
        
        private void btnDigitalizarReceta_Click(object sender, EventArgs e)
        {
            Digitalizar(0, 2);  
        }

        private void Remover(int Renglon)
        {
            bool bBorrar = false; 

            try
            {
                if (listaDeImagenes.ContainsKey(Renglon))
                {
                    listaDeImagenes.Remove(Renglon); 
                }

                bBorrar = true; 
            }
            catch
            { 
            }

            if (bBorrar)
            {
                grid.DeleteRow(Renglon); 
            }
        }

        private void Digitalizar(int Renglon, int TipoImagen)
        {
            int iCompresion = 0;
            int iPartida = Renglon; 
            Image imgDigitizacion_Aux = null;
            string sFile_X = "";
            string sFile = "";
            string sEncode = "";
            bImagenDigitalizada = false;
            imgDigitizacion = null;
            sImg_Original = "";
            sImg_Comprimida = "";

            reader = new SC_SolutionsSystem.QRCode.Cam_Reader();
            // reader.Camara = "Chicony USB 2.0 Camera";
            reader.Camara = sNombreCamara; // DtGeneral.Camaras.GetCamara("QReader");
            reader.Show();

            if (reader.ImagenDigitalizada)
            {
                bImagenDigitalizada = reader.ImagenDigitalizada;
                imgDigitizacion = reader.Imagen;

                ////using (imgDigitizacion = Image.FromFile(reader.FileName))
                ////{
                ////    //imgDigitizacion_Aux = img;
                ////}


                ////bufferImagen = ConvertirImagenABytes(imgDigitizacion, FormatosImagen.Png);
                ////sImg_Original = Convert.ToBase64String(bufferImagen);
                sImg_Original = Fg.ConvertirArchivoEnStringB64(reader.FileName); 


                sEncode = "image/jpeg";
                CompressImage(reader.FileName, 80, reader.FileNameCompress, sEncode);

                //using (imgDigitizacion_Aux = Image.FromFile(reader.FileNameCompress))
                //{
                //    //imgDigitizacion_Aux = img;
                //}


                //bufferImagen = ConvertirImagenABytes(ComprimirImagen(imgDigitizacion), FormatosImagen.Png);
                //bufferImagen = ConvertirImagenABytes(imgDigitizacion_Aux, FormatosImagen.Png);
                //sImg_Comprimida = Convert.ToBase64String(bufferImagen);
                sImg_Comprimida = Fg.ConvertirArchivoEnStringB64(reader.FileNameCompress); 


                //// Forzar que se tome la imagen generada desde la camara 
                imgDigitizacion = reader.Imagen;




                sFile_X = "";
            }


            //////
            if (reader.ImagenDigitalizada)
            {
                if (Renglon == 0)
                {
                    grid.AddRow();
                    iPartida = grid.Rows;
                    iIdImagen++;
                    //iPartida = iIdImagen;

                    grid.SetValue(iPartida, (int)Cols.Partida, iIdImagen);
                    grid.SetValue(iPartida, (int)Cols.Tipo, TipoImagen);
                    grid.SetValue(iPartida, (int)Cols.TipoDescripcion, TipoImagen == 1 ? "Ticket" : "Receta");

                    ImagenDigitalizada itemImage = new ImagenDigitalizada();
                    itemImage.Imagen = imgDigitizacion;
                    itemImage.TipoDeImagen = TipoImagen;
                    itemImage.Img_Comprimida = sImg_Comprimida;
                    itemImage.Img_Original = sImg_Original;

                    listaDeImagenes.Add(iIdImagen, itemImage);
                }
                else
                {
                    iPartida = Renglon;

                    ImagenDigitalizada itemImage = new ImagenDigitalizada();
                    itemImage.Imagen = imgDigitizacion;
                    itemImage.TipoDeImagen = TipoImagen;
                    itemImage.Img_Comprimida = sImg_Comprimida;
                    itemImage.Img_Original = sImg_Original;

                    listaDeImagenes.Remove(iPartida);
                    listaDeImagenes.Add(iPartida, itemImage);
                }
            }

        }

        private Image CompressImage(string ImageSource, int imageQuality, string savePath, string Encode)
        {
            Image imgReturn = null;
            bool bRegresa = false;
            Image sourceImage = null; 

            try
            {
                using (sourceImage = Image.FromFile(reader.FileName))
                {
                    ////Create an ImageCodecInfo-object for the codec information
                    ImageCodecInfo jpegCodec = null;

                    ////Set quality factor for compression
                    EncoderParameter imageQualitysParameter = new EncoderParameter( System.Drawing.Imaging.Encoder.Quality, imageQuality);

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
            }
            catch (Exception e)
            {
            }


            return imgReturn;
        }

        private Image ComprimirImagen(Image ImagenDigitilizada)
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

        private void grdDigitalizacion_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

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

        private void grdDigitalizacion_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            Cols columna = (Cols)e.Column + 1;
            int row = e.Row + 1;
            int iPartida_Local = grid.GetValueInt(row, (int)Cols.Partida);
            int iTipo_Local = grid.GetValueInt(row, (int)Cols.Tipo); 

            switch (columna)
            {
                case Cols.Remover:
                    Remover(iPartida_Local);
                    break;

                case Cols.Actualizar:
                    Digitalizar(iPartida_Local, iTipo_Local);
                    break;

                case Cols.Visualizar:
                    VisualizarImagen(iPartida_Local); 
                    break;

                default:
                    break;
            }
        }

    }

    internal class ImagenDigitalizada
    {
        private int iTipo = 0;
        private Image imgDigitizacion;
        private string sImg_Original = "";
        private string sImg_Comprimida = "";

        public int TipoDeImagen
        {
            get { return iTipo; }
            set { iTipo = value; }
        }

        public Image Imagen
        {
            get { return imgDigitizacion; }
            set { imgDigitizacion = value; }
        }

        public string Img_Original
        {
            get { return sImg_Original; }
            set { sImg_Original = value; }
        }

        public string Img_Comprimida
        {
            get { return sImg_Comprimida; }
            set { sImg_Comprimida = value; }
        }
    }
}
