using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem.QRCode.Codec;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.QRCode;
using DllFarmaciaSoft.QRCode.VisorEtiquetas; 


using SC_SolutionsSystem.QRCode;
using SC_SolutionsSystem.SistemaOperativo;

using Neodynamic.SDK.Printing;
using Neodynamic.Windows.Forms;
using Neodynamic.Windows.ThermalLabelEditor;
using ClosedXML.Excel;

namespace DllFarmaciaSoft.QRCode.Unidosis
{
    public partial class FrmETU_Unidosis : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion datosCnn;
        clsLeer leer;
        clsLeer leerLotes; 
        clsGrid myGrid;
        clsAyudas ayuda;
        clsConsultas consulta;
        clsLeer leerInformacion; 

        string sIdProducto = "";

        ItemCodificacion item_QR = new ItemCodificacion();
        DateTime dtMarcaDeTiempo = DateTime.Now; 

        int _copies = 1;
        double _dpi = 96;
        int _currentDemoIndex = -1;

        //ThermalLabel _currentThermalLabel = null;
        ImageSettings _imgSettings = new ImageSettings();

        PrinterSettings _printerSettings = new PrinterSettings();
        PrintOrientation _printOrientation = PrintOrientation.Portrait;

        double dCornerRadius = 0.1;

        public FrmETU_Unidosis()
        {
            QR_General.InicializarSDK(); 

            InitializeComponent();

            datosCnn = new clsDatosConexion();
            datosCnn.Servidor = General.DatosConexion.Servidor;
            datosCnn.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            datosCnn.Usuario = General.DatosConexion.Usuario;
            datosCnn.Password = General.DatosConexion.Password;
            datosCnn.Puerto = General.DatosConexion.Puerto;
            datosCnn.ConexionDeConfianza = General.DatosConexion.ConexionDeConfianza;

            cnn = new clsConexionSQL(datosCnn);
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            consulta = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            leerInformacion = new clsLeer(ref cnn);

        }

        private void FrmETU_Unidosis_Load(object sender, EventArgs e)
        {
            cboLotes.Clear();
            cboLotes.Add();
            cboLotes.SelectedIndex = 0;

            FormatosDeImpresion();
            CargarCondicionesDeAlmacenamiento(); 
            LimpiarPantalla(); 
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();

            btnImprimir.Enabled = false;
            btnExportar.Enabled = false;

            imgEtiqueta.ResetImage(); 
            
            txtCodigo.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            PrintDialog print = new PrintDialog();
            DialogResult respuesta = System.Windows.Forms.DialogResult.Yes;
            int iItem = cboFormatos.SelectedIndex;

            print.UseEXDialog = true;
            respuesta = print.ShowDialog();

            if (respuesta == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 1; i <= (int)nmNumeroDeCopias.Value; i++)
                {
                    GenerarEtiqueta(iItem, true); 
                    Imprimir(tLabel, print.PrinterSettings.PrinterName, 1);

                    System.Threading.Thread.Sleep(50); 
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            FrmVisorEtiquetas visor = new FrmVisorEtiquetas(tLabel);
            visor.ShowInTaskbar = false; 
            visor.ShowDialog(); 
        }

        private void btnReader_Click(object sender, EventArgs e)
        {
            FrmDecodificacionSNK f = new FrmDecodificacionSNK();

            f.ShowInTaskbar = false; 
            f.ShowDialog();
        }
        #endregion Botones

        #region Formatos 
        private void FormatosDeImpresion()
        {
            cboFormatos.Clear();
            cboFormatos.Add();

            cboFormatos.Add("1", "Estandar 4 x 3 pulgadas");
            cboFormatos.Add("2", "Estandar 3 x 2 pulgadas");

            cboFormatos.SelectedIndex = 0;
        }

        private void CargarCondicionesDeAlmacenamiento()
        {
            cboCondicionesDeAlmacenamiento.Clear();
            cboCondicionesDeAlmacenamiento.Add();

            cboCondicionesDeAlmacenamiento.Add(consulta.CondicionesDeAlmacenamiento("CargarCondicionesDeAlmacenamiento()"), true, "IdCondicion", "Descripcion"); 

            cboCondicionesDeAlmacenamiento.SelectedIndex = 0;
        }
        #endregion Formatos

        #region CodigoEAN
        private void IniciarCaptura()
        { 
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            IniciarCaptura(); 
        }

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            bool bActivar = true;
            sIdProducto = Fg.PonCeros(txtCodigo.Text.Trim(), 13);
            string sCodigoEAN_Seleccionado = "";


            if (txtCodigo.Text.Trim() != "")
            {
                if (!GnFarmacia.ValidarSeleccionCodigoEAN(sIdProducto, ref sCodigoEAN_Seleccionado))
                {
                    IniciarCaptura();
                }
                else
                {

                    leer.DataSetClase = consulta.Productos_CodigosEAN_Datos_Etiquetas(sCodigoEAN_Seleccionado, sCodigoEAN_Seleccionado, "txtCodigo_Validating");
                    if (!leer.Leer())
                    {
                        General.msjUser("Clave de Producto no encontrada, verifique.");
                        txtCodigo.Text = "";
                        txtCodigo.Focus();
                    }
                    else
                    {
                        CargarInformacion_Producto(); 
                    }
                }
            }
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Productos_CodigoEAN_Etiquetas("txtCodigo_KeyDown");
                if (leer.Leer())
                {
                    CargarInformacion_Producto(); 
                }
            }
        }

        private void CargarInformacion_Producto()
        {
            txtCodigo.Enabled = false;
            sIdProducto = leer.Campo("IdProducto");
            txtCodigo.Text = leer.Campo("CodigoEAN");
            lblNombreComercial.Text = leer.Campo("Descripcion_InformacionAdicional_Comercial") == "" ? leer.Campo("Descripcion") : leer.Campo("Descripcion_InformacionAdicional_Comercial"); 
            lblClaveSSA.Text = leer.Campo("ClaveSSA");
            lblDescripcionClaveSSA.Text = leer.Campo("Descripcion_InformacionAdicional") == "" ? leer.Campo("DescripcionSal") : leer.Campo("Descripcion_InformacionAdicional") + "  " + leer.Campo("Concentracion_InformacionAdicional"); 
            lblLaboratorio.Text = Fg.Left(leer.Campo("Laboratorio_InformacionAdicional") == "" ? leer.Campo("Laboratorio") : leer.Campo("Laboratorio_InformacionAdicional"), 30);
            lblRegistroSanitario.Text = leer.Campo("");
            lblPresentacion.Text = leer.Campo("Presentacion_InformacionAdicional") == "" ? leer.Campo("Presentacion") : leer.Campo("Presentacion_InformacionAdicional");


            Cargar_Lotes();


            btnImprimir.Enabled = true;
            btnExportar.Enabled = true;
            cboLotes.Focus(); 
        }

        private void Cargar_Lotes()
        {
            cboLotes.Clear();
            cboLotes.Add();
            cboLotes.SelectedIndex = 0;

            leerLotes = new clsLeer();
            leerLotes.DataSetClase = consulta.LotesDeCodigo_CodigoEAN(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                sIdProducto, txtCodigo.Text, false, "Cargar_Lotes()");

            if (leerLotes.Leer())
            {
                cboLotes.Add(leerLotes.DataSetClase, true, "ClaveLote", "ClaveLote");
                leerInformacion.DataSetClase = leerLotes.DataSetClase; 
            }

        }

        private void cboLotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSubFarmacia.Text = "";
            lblCaducidad.Text = "";

            if (cboLotes.SelectedIndex != 0)
            {
                lblSubFarmacia.Text = string.Format("{0} - {1}",  cboLotes.ItemActual.GetItem("IdSubFarmacia"), cboLotes.ItemActual.GetItem("SubFarmacia"));
                lblCaducidad.Text = string.Format("{0}", General.FechaYMD(Convert.ToDateTime(cboLotes.ItemActual.GetItem("FechaCad"))));
            }
        }
        #endregion CodigoEAN

        #region Etiquetas 
        double dItems_Alto = 0.4;
        double dItems_Ancho = 7;
        double dItems_Fuente_Empresa = 7.5;
        double dItems_Fuente = 8;
        double ditemBC_ET_01_DM_Informacion___Ancho = 2;
        double ditemBC_ET_01_DM_Informacion___X = 2;
        double ditemBC_ET_01_DM_Informacion___Y = 2; 

        double dFactor = 2.50;
        double dAncho = 6 * 1;
        double dAlto = 4 * 1;
        double dMargen = 0.05 * 1;
        double dAlto_Encabezado = 1;
        double dSeparador = 0.075 * 1;
        double dSeparadorDivisiones = 0.6;
        double dBorde = 0.00;
        double dBorde_Lineas = 0.05;
        double dPadding = 0.01;

        string sLeyenda_Almacenamiento = string.Format("C.A: Mantengase a no más de 30°C");
        string sLeyenda_Embarazo = string.Format("Prohibido su uso en caso embarazo ó lactancia").ToUpper();
        DateTime dtMT = DateTime.Now;


        ThermalLabel tLabel; 

        Neodynamic.SDK.Printing.Font fuente = new Neodynamic.SDK.Printing.Font();
        UnitType unidad = UnitType.Cm;

        ImageItem imgLogo;
        ImageItem imgRiesgoEmbarazo; 

        RectangleShapeItem rectItem;

        TextItem itemTxt_01_Empresa;
        TextItem itemTxt_02_ClaveSSA;
        TextItem itemTxt_02_DescripcionClave;
        TextItem itemTxt_02_DescripcionClaveCorta;
        TextItem itemTxt_05_RegistroSanitario;
        TextItem itemTxt_06_Presentacion;
        TextItem itemTxt_07_Laboratorio;
        TextItem itemTxt_08_CodigoEAN;
        TextItem itemTxt_09_Lote;
        TextItem itemTxt_10_Caducidad;
        TextItem itemTxt_11_Vigencia;
        TextItem itemTxt_12_NotasAlmacenamiento; 
        TextItem itemTxt_13_NotasEmbarazo;
        TextItem itemTxt_14_FechaProcesamiento;
        TextItem itemTxt_15_PersonalElaboro;


        BarcodeItem itemBC_ET_01_DM_Informacion;
        BarcodeItem itemBC_ET_02_DM_EAN;
        BarcodeItem itemBC_ET_03_EAN;


        LineShapeItem lineItem;
        LineShapeItem lineItem_Seccion_01;
        LineShapeItem lineItem_Seccion_02_FolioReferencia;
        LineShapeItem lineItem_Seccion_02_FolioReferencia_Pie;





        LineShapeItem lineItem_Division_01;
        LineShapeItem lineItem_Division_02;
        LineShapeItem lineItem_Division_03;
        LineShapeItem lineItem_Division_04;
        LineShapeItem lineItem_Division_05_Vertical;

        private void InicializarObjetos()
        {
            imgLogo = new ImageItem();
            imgRiesgoEmbarazo = new ImageItem(); 
            rectItem = new RectangleShapeItem();
            lineItem = new LineShapeItem();
            lineItem_Seccion_01 = new LineShapeItem();
            lineItem_Seccion_02_FolioReferencia = new LineShapeItem();
            lineItem_Seccion_02_FolioReferencia_Pie = new LineShapeItem();

            itemBC_ET_01_DM_Informacion = new BarcodeItem();


            itemTxt_01_Empresa = new TextItem();
            itemTxt_02_ClaveSSA = new TextItem();
            itemTxt_02_DescripcionClave = new TextItem();
            itemTxt_02_DescripcionClaveCorta = new TextItem();
            itemTxt_05_RegistroSanitario = new TextItem();
            itemTxt_06_Presentacion = new TextItem();
            itemTxt_07_Laboratorio = new TextItem();
            itemTxt_08_CodigoEAN = new TextItem();
            itemTxt_09_Lote = new TextItem();
            itemTxt_10_Caducidad = new TextItem();
            itemTxt_11_Vigencia = new TextItem(); 
            itemTxt_12_NotasAlmacenamiento = new TextItem(); 
            itemTxt_13_NotasEmbarazo = new TextItem();
            itemTxt_14_FechaProcesamiento = new TextItem(); 

            lineItem_Division_01 = new LineShapeItem();
            lineItem_Division_02 = new LineShapeItem();
            lineItem_Division_03 = new LineShapeItem();
            lineItem_Division_04 = new LineShapeItem();
            lineItem_Division_05_Vertical = new LineShapeItem();


            dFactor = 2.50;
            dAncho = 6 * dFactor;
            dAlto = 4 * dFactor;
            dMargen = 0.05 * dFactor;
            dAlto_Encabezado = 1;
            dSeparador = 0.075 * dFactor;
            dSeparadorDivisiones = 0.6;
            dBorde = 0.00;
            dBorde_Lineas = 0.05;
            dPadding = 0.01;


            fuente.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
            fuente.Unit = unidad == UnitType.Inch ? FontUnit.Inch : FontUnit.Millimeter;
            fuente.Unit = FontUnit.Point;
            fuente.Name = "Arial";

            ////Define a ThermalLabel object and set unit to inch and label size
            tLabel = new ThermalLabel(unidad, dAncho, dAlto);
            tLabel.GapLength = 0.2;
        }


        private void btnLimpiarEtiqueta_Click(object sender, EventArgs e)
        {
            imgEtiqueta.ResetImage(); 
        }

        private void btnGenerarEtiquetas_Click(object sender, EventArgs e)
        {
            bool bGenerar = false;
            int iItem = cboFormatos.SelectedIndex;

            InicializarObjetos();

            bGenerar = validarGeneracionDeEtiquetas(); // iItem > 0;

            if (bGenerar)
            {
                GenerarEtiqueta(iItem, false); 
            }
        }

        private bool validarGeneracionDeEtiquetas()
        {
            bool bRegresa = true;
            int iItem = cboFormatos.SelectedIndex;

            if ( bRegresa && txtRegistroSanitario.Text.Trim() == "")
            {
                General.msjUser("No ha capturado el Registro Sanitario del producto especificado, verifique.");
                bRegresa = false;
                txtRegistroSanitario.Focus(); 
            }

            if (bRegresa && cboLotes.SelectedIndex == 0)
            {
                General.msjUser("No ha seleccionado un Lote válido, verifique.");
                bRegresa = false;
                cboLotes.Focus();
            }

            if (bRegresa && iItem == 0)
            {
                General.msjUser("No ha seleccionado un Formato de etiqueta válido, verifique.");
                bRegresa = false;
                cboFormatos.Focus(); 
            }

            return bRegresa;
        }

        private void GenerarEtiqueta(int Item, bool Imprimir)
        {
            imgEtiqueta.ResetImage();

            switch (Item)
            {
                case 1:
                    Formato_01(Imprimir);
                    break;

                case 2:
                    Formato_02(Imprimir);
                    break;

                default:
                    break;
            }
        }

        private string GetUUID(int Tipo)
        {
            string sRegresa = "";
            string sMarcaDeTiempo = ""; 

            sMarcaDeTiempo = string.Format("{0}-{1}-{2} {3}:{4}:{5}", 
                Fg.PonCeros(dtMT.Year, 4), Fg.PonCeros(dtMT.Month, 2), Fg.PonCeros(dtMT.Day, 2),
                Fg.PonCeros(dtMT.Hour, 2), Fg.PonCeros(dtMT.Minute, 2), Fg.PonCeros(dtMT.Second, 2));

            sRegresa = sMarcaDeTiempo; 
            if (Tipo == 2)
            {
                sMarcaDeTiempo = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                    Fg.PonCeros(dtMT.Year, 4), Fg.PonCeros(dtMT.Month, 2), Fg.PonCeros(dtMT.Day, 2),
                    Fg.PonCeros(dtMT.Hour, 2), Fg.PonCeros(dtMT.Minute, 2), Fg.PonCeros(dtMT.Second, 2), Fg.PonCeros(dtMT.Millisecond, 4));

                sRegresa = string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                    txtCodigo.Text, cboLotes.Data, sMarcaDeTiempo);
            }

            return sRegresa; 
        }

        private void GenerarVistaPrevia(ThermalLabel _currentThermalLabel)
        {
            //Display ThermalLabel as a TIFF image
            if (_currentThermalLabel != null)
            {
                try
                {
                    using (PrintJob pj = new PrintJob())
                    {
                        pj.ThermalLabel = _currentThermalLabel;
                        pj.Copies = (int)nmNumeroDeCopias.Value;
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();

                        ImageSettings imgSett = new ImageSettings();
                        imgSett.ImageFormat = ImageFormat.Tiff;
                        //imgSett.PixelFormat = PixelFormat.BGRA32;
                        imgSett.AntiAlias = true;
                        //imgSett.TransparentBackground = true;

                        pj.ExportToImage(ms, imgSett, _dpi);

                        this.imgEtiqueta.LoadImage(ms);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void Imprimir(ThermalLabel Etiqueta, string Impresora, int Copias)
        {
            //Display Print Job dialog...           
            //PrintDialog printDialog = new PrintDialog();
            //if (printDialog.ShowDialog() == DialogResult.OK)

            if (Etiqueta != null)
            {
                _printerSettings.PrinterName = Impresora;
                _printerSettings.ProgrammingLanguage = ProgrammingLanguage.ZPL;

                //create a PrintJob object
                using (PrintJob pj = new PrintJob(_printerSettings))
                {
                    pj.Copies = Copias; // set copies
                    pj.PrintOrientation = _printOrientation; //set orientation
                    pj.ThermalLabel = Etiqueta; // set the ThermalLabel object
                    pj.Print(); // print the ThermalLabel object                    
                }
            }
        }

        private void Formato_01(bool Imprimir)
        {
            dItems_Alto = 0.4;
            dItems_Ancho = 7;
            dItems_Fuente_Empresa = 9;
            dItems_Fuente = 8;

            Formato_De_Impresion(Imprimir, true, 1, 4, 3); 
        }

        private void Formato_02(bool Imprimir)
        {
            dItems_Alto = 0.30;
            dItems_Ancho = 5.35;
            dItems_Fuente_Empresa = 7;
            dItems_Fuente = 6;

            Formato_De_Impresion(Imprimir, false, 2, 3, 2);
        }

        private void Formato_De_Impresion(bool Imprimir, bool MostrarBarrasEAN, int Formato, double Ancho, double Alto)
        {
            dtMT = DateTime.Now; 
            dFactor = 2.50;
            dAncho = 4 * dFactor;
            dAlto = 3 * dFactor;

            dAncho = Ancho * dFactor;
            dAlto = Alto * dFactor;


            dMargen = 0.05 * dFactor;
            dAlto_Encabezado = 1;
            dSeparador = 0.075 * dFactor;
            dSeparadorDivisiones = 0.6;
            dBorde = 0.00;
            dBorde_Lineas = 0.05;
            dPadding = 0.01;


            fuente.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
            fuente.Unit = unidad == UnitType.Inch ? FontUnit.Inch : FontUnit.Millimeter;
            fuente.Unit = FontUnit.Point;
            fuente.Name = "Arial";

            ////Define a ThermalLabel object and set unit to inch and label size
            tLabel = new ThermalLabel(unidad, dAncho, dAlto);
            tLabel.GapLength = 0.2;


            ////// Marco principal 
            rectItem = new RectangleShapeItem();
            rectItem.CornerRadius.BottomLeft = dCornerRadius;
            rectItem.CornerRadius.BottomRight = dCornerRadius;
            rectItem.CornerRadius.TopLeft = dCornerRadius;
            rectItem.CornerRadius.TopRight = dCornerRadius;
            rectItem.X = 0.05;
            rectItem.Y = 0.05; //dMargen;
            rectItem.Height = dAlto - (dMargen * 1);
            rectItem.Width = dAncho - (dMargen * 1);
            rectItem.StrokeThickness = 0.01 * dFactor;


            //// Reducir el tamaño de la fuente 
            dItems_Fuente_Empresa -= 1.5; 

            itemTxt_01_Empresa = new TextItem(); // new TextItem(dMargen * 2, dMargen * 2, dAncho - (dMargen * 4), dAlto_Encabezado, info.Campo("Empresa"));
            itemTxt_01_Empresa.Font.Name = fuente.Name;
            itemTxt_01_Empresa.Font.Unit = fuente.Unit;
            itemTxt_01_Empresa.Text = DtGeneral.EmpresaConectadaNombre;
            itemTxt_01_Empresa.Font.Size = dItems_Fuente_Empresa;
            itemTxt_01_Empresa.Font.Bold = true;
            itemTxt_01_Empresa.TextAlignment = TextAlignment.Center;
            itemTxt_01_Empresa.TextPadding = new FrameThickness(dPadding);
            itemTxt_01_Empresa.Height = dItems_Alto;
            itemTxt_01_Empresa.Width = rectItem.Width - (dMargen * 2);
            itemTxt_01_Empresa.X = dMargen * 2;
            itemTxt_01_Empresa.Y = dMargen * ( dAlto - 1.60);
            //itemTxt_01_Empresa.Y = dMargen * (dAlto - .80);
            itemTxt_01_Empresa.Y = (dAlto - 0.6);
            itemTxt_01_Empresa.BorderThickness = new FrameThickness(dBorde);

            if (Formato == 2)
            {
                itemTxt_01_Empresa.Y = (dAlto - 0.4);
            }

            itemTxt_02_DescripcionClave = new TextItem();
            itemTxt_02_DescripcionClave.Font.Name = fuente.Name;
            itemTxt_02_DescripcionClave.Font.Unit = fuente.Unit;
            itemTxt_02_DescripcionClave.Text = Fg.Left(lblDescripcionClaveSSA.Text, 50);
            itemTxt_02_DescripcionClave.Font.Size = dItems_Fuente;
            itemTxt_02_DescripcionClave.Font.Bold = false;
            itemTxt_02_DescripcionClave.TextAlignment = TextAlignment.Left;
            itemTxt_02_DescripcionClave.TextPadding = new FrameThickness(dPadding);
            itemTxt_02_DescripcionClave.Height = dItems_Alto;
            itemTxt_02_DescripcionClave.Width = rectItem.Width - (dMargen * 2);
            itemTxt_02_DescripcionClave.X = dMargen; // dMargen * 1;
            itemTxt_02_DescripcionClave.Y = dMargen * 2; // dMargen * 1;
            itemTxt_02_DescripcionClave.BorderThickness = new FrameThickness(dBorde);



            itemTxt_02_DescripcionClaveCorta = new TextItem();
            itemTxt_02_DescripcionClaveCorta.Font.Name = fuente.Name;
            itemTxt_02_DescripcionClaveCorta.Font.Unit = fuente.Unit;
            itemTxt_02_DescripcionClaveCorta.Text = Fg.Left(lblNombreComercial.Text, 50);
            itemTxt_02_DescripcionClaveCorta.Font.Size = dItems_Fuente;
            itemTxt_02_DescripcionClaveCorta.Font.Bold = false;
            itemTxt_02_DescripcionClaveCorta.TextAlignment = TextAlignment.Left;
            itemTxt_02_DescripcionClaveCorta.TextPadding = new FrameThickness(dPadding);
            itemTxt_02_DescripcionClaveCorta.Height = dItems_Alto;
            itemTxt_02_DescripcionClaveCorta.Width = dItems_Ancho; // 7;
            itemTxt_02_DescripcionClaveCorta.X = dMargen;
            itemTxt_02_DescripcionClaveCorta.Y = itemTxt_02_DescripcionClave.Y + (itemTxt_02_DescripcionClave.Height * 1.5);
            itemTxt_02_DescripcionClaveCorta.BorderThickness = new FrameThickness(dBorde);


            itemTxt_02_ClaveSSA = new TextItem();
            itemTxt_02_ClaveSSA.Font.Name = fuente.Name;
            itemTxt_02_ClaveSSA.Font.Unit = fuente.Unit;
            itemTxt_02_ClaveSSA.Text = Fg.Left(lblClaveSSA.Text, 50);
            itemTxt_02_ClaveSSA.Font.Size = dItems_Fuente;
            itemTxt_02_ClaveSSA.Font.Bold = false;
            itemTxt_02_ClaveSSA.TextAlignment = TextAlignment.Left;
            itemTxt_02_ClaveSSA.TextPadding = new FrameThickness(dPadding);
            itemTxt_02_ClaveSSA.Height = dItems_Alto;
            itemTxt_02_ClaveSSA.Width = dItems_Ancho; // 7;
            itemTxt_02_ClaveSSA.X = dMargen;
            itemTxt_02_ClaveSSA.Y = itemTxt_02_DescripcionClaveCorta.Y + (itemTxt_02_DescripcionClaveCorta.Height * 1);
            itemTxt_02_ClaveSSA.BorderThickness = new FrameThickness(dBorde);


            itemTxt_05_RegistroSanitario = new TextItem();
            itemTxt_05_RegistroSanitario.Font.Name = fuente.Name;
            itemTxt_05_RegistroSanitario.Font.Unit = fuente.Unit;
            itemTxt_05_RegistroSanitario.Text = string.Format("REG. SANITARIO: {0}", Fg.Left(txtRegistroSanitario.Text, 50));
            itemTxt_05_RegistroSanitario.Font.Size = dItems_Fuente;
            itemTxt_05_RegistroSanitario.Font.Bold = false;
            itemTxt_05_RegistroSanitario.TextAlignment = TextAlignment.Left;
            itemTxt_05_RegistroSanitario.TextPadding = new FrameThickness(dPadding);
            itemTxt_05_RegistroSanitario.Height = dItems_Alto;
            itemTxt_05_RegistroSanitario.Width = dItems_Ancho; // 7;
            itemTxt_05_RegistroSanitario.X = dMargen;
            itemTxt_05_RegistroSanitario.Y = itemTxt_02_ClaveSSA.Y + (itemTxt_02_ClaveSSA.Height * 1);
            itemTxt_05_RegistroSanitario.BorderThickness = new FrameThickness(dBorde);


            itemTxt_06_Presentacion = new TextItem();
            itemTxt_06_Presentacion.Font.Name = fuente.Name;
            itemTxt_06_Presentacion.Font.Unit = fuente.Unit;
            itemTxt_06_Presentacion.Text = Fg.Left(lblPresentacion.Text, 50);
            itemTxt_06_Presentacion.Font.Size = dItems_Fuente;
            itemTxt_06_Presentacion.Font.Bold = false;
            itemTxt_06_Presentacion.TextAlignment = TextAlignment.Left;
            itemTxt_06_Presentacion.TextPadding = new FrameThickness(dPadding);
            itemTxt_06_Presentacion.Height = dItems_Alto;
            itemTxt_06_Presentacion.Width = dItems_Ancho; // 7;
            itemTxt_06_Presentacion.X = dMargen;
            itemTxt_06_Presentacion.Y = itemTxt_05_RegistroSanitario.Y + (itemTxt_05_RegistroSanitario.Height * 1);
            itemTxt_06_Presentacion.BorderThickness = new FrameThickness(dBorde);


            itemTxt_07_Laboratorio = new TextItem();
            itemTxt_07_Laboratorio.Font.Name = fuente.Name;
            itemTxt_07_Laboratorio.Font.Unit = fuente.Unit;
            itemTxt_07_Laboratorio.Text = string.Format("LAB: {0}", Fg.Left(lblLaboratorio.Text, 50));
            itemTxt_07_Laboratorio.Font.Size = dItems_Fuente;
            itemTxt_07_Laboratorio.Font.Bold = false;
            itemTxt_07_Laboratorio.TextAlignment = TextAlignment.Left;
            itemTxt_07_Laboratorio.TextPadding = new FrameThickness(dPadding);
            itemTxt_07_Laboratorio.Height = dItems_Alto;
            itemTxt_07_Laboratorio.Width = dItems_Ancho; // 7;
            itemTxt_07_Laboratorio.X = dMargen;
            itemTxt_07_Laboratorio.Y = itemTxt_06_Presentacion.Y + (itemTxt_06_Presentacion.Height * 1);
            itemTxt_07_Laboratorio.BorderThickness = new FrameThickness(dBorde);

            itemTxt_08_CodigoEAN = new TextItem();
            itemTxt_08_CodigoEAN.Font.Name = fuente.Name;
            itemTxt_08_CodigoEAN.Font.Unit = fuente.Unit;
            itemTxt_08_CodigoEAN.Text = string.Format("EAN: {0}", Fg.Left(txtCodigo.Text, 50));
            itemTxt_08_CodigoEAN.Font.Size = dItems_Fuente;
            itemTxt_08_CodigoEAN.Font.Bold = false;
            itemTxt_08_CodigoEAN.TextAlignment = TextAlignment.Left;
            itemTxt_08_CodigoEAN.TextPadding = new FrameThickness(dPadding);
            itemTxt_08_CodigoEAN.Height = dItems_Alto;
            itemTxt_08_CodigoEAN.Width = dItems_Ancho; // 7;
            itemTxt_08_CodigoEAN.X = dMargen;
            itemTxt_08_CodigoEAN.Y = itemTxt_07_Laboratorio.Y + (itemTxt_07_Laboratorio.Height * 1);
            itemTxt_08_CodigoEAN.BorderThickness = new FrameThickness(dBorde);


            itemTxt_09_Lote = new TextItem();
            itemTxt_09_Lote.Font.Name = fuente.Name;
            itemTxt_09_Lote.Font.Unit = fuente.Unit;
            itemTxt_09_Lote.Text = string.Format("LOTE: {0}", Fg.Left(cboLotes.Data, 50));
            itemTxt_09_Lote.Font.Size = dItems_Fuente;
            itemTxt_09_Lote.Font.Bold = false;
            itemTxt_09_Lote.TextAlignment = TextAlignment.Left;
            itemTxt_09_Lote.TextPadding = new FrameThickness(dPadding);
            itemTxt_09_Lote.Height = dItems_Alto;
            itemTxt_09_Lote.Width = dItems_Ancho; // 7;
            itemTxt_09_Lote.X = dMargen;
            itemTxt_09_Lote.Y = itemTxt_08_CodigoEAN.Y + (itemTxt_08_CodigoEAN.Height * 1);
            itemTxt_09_Lote.BorderThickness = new FrameThickness(dBorde);


            itemTxt_10_Caducidad = new TextItem();
            itemTxt_10_Caducidad.Font.Name = fuente.Name;
            itemTxt_10_Caducidad.Font.Unit = fuente.Unit;
            itemTxt_10_Caducidad.Text = string.Format("CADUCIDAD: {0}", Fg.Left(lblCaducidad.Text, 10));
            itemTxt_10_Caducidad.Font.Size = dItems_Fuente;
            itemTxt_10_Caducidad.Font.Bold = false;
            itemTxt_10_Caducidad.TextAlignment = TextAlignment.Left;
            itemTxt_10_Caducidad.TextPadding = new FrameThickness(dPadding);
            itemTxt_10_Caducidad.Height = dItems_Alto;
            itemTxt_10_Caducidad.Width = dItems_Ancho / 2; // 7;
            itemTxt_10_Caducidad.X = dMargen;
            itemTxt_10_Caducidad.Y = itemTxt_09_Lote.Y + (itemTxt_09_Lote.Height * 1);
            itemTxt_10_Caducidad.BorderThickness = new FrameThickness(dBorde);


            itemTxt_11_Vigencia = new TextItem();
            itemTxt_11_Vigencia.Font.Name = fuente.Name;
            itemTxt_11_Vigencia.Font.Unit = fuente.Unit;
            itemTxt_11_Vigencia.Text = string.Format("VIGENCIA DU: {0}", Fg.Left(lblCaducidad.Text, 10));
            itemTxt_11_Vigencia.Font.Size = dItems_Fuente;
            itemTxt_11_Vigencia.Font.Bold = false;
            itemTxt_11_Vigencia.TextAlignment = TextAlignment.Left;
            itemTxt_11_Vigencia.TextPadding = new FrameThickness(dPadding);
            itemTxt_11_Vigencia.Height = dItems_Alto;
            itemTxt_11_Vigencia.Width = dItems_Ancho; // 7;
            itemTxt_11_Vigencia.X = dMargen + .05 + itemTxt_10_Caducidad.Width;
            itemTxt_11_Vigencia.Y = itemTxt_09_Lote.Y + (itemTxt_09_Lote.Height * 1);
            itemTxt_11_Vigencia.BorderThickness = new FrameThickness(dBorde);



            itemTxt_15_PersonalElaboro = new TextItem();
            itemTxt_15_PersonalElaboro.Font.Name = fuente.Name;
            itemTxt_15_PersonalElaboro.Font.Unit = fuente.Unit;
            itemTxt_15_PersonalElaboro.Text = string.Format("ELABORÓ: {0}", DtGeneral.NombrePersonal);
            itemTxt_15_PersonalElaboro.Font.Size = dItems_Fuente;
            itemTxt_15_PersonalElaboro.Font.Bold = false;
            itemTxt_15_PersonalElaboro.TextAlignment = TextAlignment.Left;
            itemTxt_15_PersonalElaboro.TextPadding = new FrameThickness(dPadding);
            itemTxt_15_PersonalElaboro.Height = dItems_Alto;
            itemTxt_15_PersonalElaboro.Width = dItems_Ancho; // 7;
            itemTxt_15_PersonalElaboro.X = dMargen;
            itemTxt_15_PersonalElaboro.Y = itemTxt_10_Caducidad.Y + (itemTxt_10_Caducidad.Height * 1);
            itemTxt_15_PersonalElaboro.BorderThickness = new FrameThickness(dBorde);





            itemTxt_12_NotasAlmacenamiento = new TextItem();
            itemTxt_12_NotasAlmacenamiento.Font.Name = fuente.Name;
            itemTxt_12_NotasAlmacenamiento.Font.Unit = fuente.Unit;

            if (chkCondicionesAlmacenamiento.Checked)
            {
                itemTxt_12_NotasAlmacenamiento.BackColor = Neodynamic.SDK.Printing.Color.Black;
                itemTxt_12_NotasAlmacenamiento.ForeColor = Neodynamic.SDK.Printing.Color.White;
            }

            ////itemTxt_12_NotasAlmacenamiento.Text = string.Format("C.A: Mantengase a no más de 30°C");
            sLeyenda_Almacenamiento = cboCondicionesDeAlmacenamiento.Text; 
            itemTxt_12_NotasAlmacenamiento.Text = sLeyenda_Almacenamiento;
            itemTxt_12_NotasAlmacenamiento.Font.Size = dItems_Fuente;
            itemTxt_12_NotasAlmacenamiento.Font.Bold = false;
            itemTxt_12_NotasAlmacenamiento.TextAlignment = TextAlignment.Left;
            itemTxt_12_NotasAlmacenamiento.TextPadding = new FrameThickness(dPadding);
            itemTxt_12_NotasAlmacenamiento.Height = dItems_Alto;
            itemTxt_12_NotasAlmacenamiento.Width = dItems_Ancho; // 7;
            itemTxt_12_NotasAlmacenamiento.X = dMargen;
            itemTxt_12_NotasAlmacenamiento.Y = itemTxt_15_PersonalElaboro.Y + (itemTxt_15_PersonalElaboro.Height * 1);
            itemTxt_12_NotasAlmacenamiento.BorderThickness = new FrameThickness(dBorde);


            itemTxt_13_NotasEmbarazo = new TextItem();
            itemTxt_13_NotasEmbarazo.Font.Name = fuente.Name;
            itemTxt_13_NotasEmbarazo.Font.Unit = fuente.Unit;


            itemTxt_13_NotasEmbarazo.BorderThickness = new FrameThickness(0.02);
            if (chkRiesgoEnElEmbarazoSombreado.Checked)
            {
                //itemTxt_13_NotasEmbarazo.BorderThickness = new FrameThickness(0);
                itemTxt_13_NotasEmbarazo.BackColor = Neodynamic.SDK.Printing.Color.Black;
                itemTxt_13_NotasEmbarazo.ForeColor = Neodynamic.SDK.Printing.Color.White;
            }

            ////itemTxt_13_NotasEmbarazo.Text = string.Format("NO SUMINISTRAR DURANTE EL EMBARAZO Ó LACTANCIA");
            ////itemTxt_13_NotasEmbarazo.Text = string.Format("En caso embarazo ó lactancia, consulte a su médico".ToUpper());
            ////itemTxt_13_NotasEmbarazo.Text = string.Format("Prohibido su uso en caso embarazo ó lactancia".ToUpper());
            itemTxt_13_NotasEmbarazo.Text = sLeyenda_Embarazo; 
            itemTxt_13_NotasEmbarazo.Font.Size = dItems_Fuente;
            itemTxt_13_NotasEmbarazo.Font.Bold = false;
            itemTxt_13_NotasEmbarazo.TextAlignment = TextAlignment.Center;
            itemTxt_13_NotasEmbarazo.TextPadding = new FrameThickness(dPadding);
            itemTxt_13_NotasEmbarazo.Height = 0.7;
            itemTxt_13_NotasEmbarazo.Width = dItems_Ancho - 1.5; // 5.5;
            itemTxt_13_NotasEmbarazo.X = dMargen + 1.5;
            itemTxt_13_NotasEmbarazo.Y = 0.25 + itemTxt_12_NotasAlmacenamiento.Y + (itemTxt_12_NotasAlmacenamiento.Height * 1);
            itemTxt_13_NotasEmbarazo.Y = 0.10 + itemTxt_12_NotasAlmacenamiento.Y + (itemTxt_12_NotasAlmacenamiento.Height * 1); 


            itemTxt_14_FechaProcesamiento = new TextItem();
            itemTxt_14_FechaProcesamiento.Font.Name = fuente.Name;
            itemTxt_14_FechaProcesamiento.Font.Unit = fuente.Unit;
            itemTxt_14_FechaProcesamiento.Text = string.Format("FECHA PREPARACIÓN: {0}", GetUUID(1) );
            itemTxt_14_FechaProcesamiento.Font.Size = dItems_Fuente;
            itemTxt_14_FechaProcesamiento.Font.Bold = false;
            itemTxt_14_FechaProcesamiento.TextAlignment = TextAlignment.Center;
            itemTxt_14_FechaProcesamiento.TextPadding = new FrameThickness(dPadding);
            itemTxt_14_FechaProcesamiento.Height = dItems_Alto;
            itemTxt_14_FechaProcesamiento.Width = dAlto - (dMargen * 2); // 7;
            itemTxt_14_FechaProcesamiento.X = dAncho - ( dMargen + dItems_Alto ) ;
            itemTxt_14_FechaProcesamiento.Y = dMargen;
            itemTxt_14_FechaProcesamiento.BorderThickness = new FrameThickness(dBorde);
            itemTxt_14_FechaProcesamiento.RotationAngle = 90; 



            imgRiesgoEmbarazo = new ImageItem();
            imgRiesgoEmbarazo.SourceBase64 = Get_Imagen_Embarazo(); 
            imgRiesgoEmbarazo.PrintAsGraphic = true;
            imgRiesgoEmbarazo.Width = 0.80;
            imgRiesgoEmbarazo.LockAspectRatio = LockAspectRatio.WidthBased;
            imgRiesgoEmbarazo.MonochromeSettings.DitherMethod = DitherMethod.Threshold;
            imgRiesgoEmbarazo.MonochromeSettings.Threshold = 50;
            imgRiesgoEmbarazo.MonochromeSettings.ReverseEffect = false;
            imgRiesgoEmbarazo.X = 0.5;
            imgRiesgoEmbarazo.Y = 4.55;
            imgRiesgoEmbarazo.Y = itemTxt_13_NotasEmbarazo.Y - 0.25;
            imgRiesgoEmbarazo.Y = itemTxt_13_NotasEmbarazo.Y - 0.05;
            imgRiesgoEmbarazo.IsGrayscaleOrBlackWhite = Imprimir;



            //////////////////// QR con información del producto 
            DateTime dtCaducidad = new DateTime();
            dtMarcaDeTiempo = DateTime.Now; 
            item_QR = new ItemCodificacion();

            dtCaducidad = Convert.ToDateTime(cboLotes.ItemActual.GetItem("FechaCad")); 
            item_QR.IdEmpresa = DtGeneral.EmpresaConectada;
            item_QR.IdEstado = DtGeneral.EstadoConectado;
            item_QR.IdFarmacia = DtGeneral.FarmaciaConectada;
            item_QR.Codificadora = "00";
            item_QR.CodigoEAN = txtCodigo.Text;
            item_QR.Multiplo = 1;
            item_QR.IdSubFarmacia = cboLotes.ItemActual.GetItem("IdSubFarmacia") ;
            item_QR.ClaveLote = cboLotes.Data;
            item_QR.Caducidad = string.Format("{0}{1}", Fg.PonCeros(dtCaducidad.Year, 2), Fg.PonCeros(dtCaducidad.Month, 2));
            item_QR.Año = Fg.Right(Fg.PonCeros(dtMarcaDeTiempo.Year, 4), 2);
            item_QR.Mes = Fg.PonCeros(dtMarcaDeTiempo.Month, 2);
            item_QR.Dia = Fg.PonCeros(dtMarcaDeTiempo.Day, 2);
            item_QR.Hora = Fg.PonCeros(dtMarcaDeTiempo.Hour, 2);
            item_QR.Minuto = Fg.PonCeros(dtMarcaDeTiempo.Minute, 2);
            item_QR.Segundo = Fg.PonCeros(dtMarcaDeTiempo.Second, 2);
            item_QR.Consecutivo = Fg.PonCeros(dtMarcaDeTiempo.Millisecond, 2); 
            item_QR.IdProveedor = "0000";
            item_QR.NumeroDeFactura = "0000";



            itemBC_ET_01_DM_Informacion = new BarcodeItem();
            itemBC_ET_01_DM_Informacion.Symbology = BarcodeSymbology.DataMatrix;
            itemBC_ET_01_DM_Informacion.RotationAngle = 0;
            itemBC_ET_01_DM_Informacion.Font.Name = fuente.Name;
            itemBC_ET_01_DM_Informacion.Font.Unit = fuente.Unit;
            itemBC_ET_01_DM_Informacion.Font.Size = 8;
            itemBC_ET_01_DM_Informacion.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
            itemBC_ET_01_DM_Informacion.AddChecksum = false;
            itemBC_ET_01_DM_Informacion.DisplayCode = true;
            itemBC_ET_01_DM_Informacion.BorderThickness = new FrameThickness(0.00);
            itemBC_ET_01_DM_Informacion.BarcodeAlignment = BarcodeAlignment.MiddleCenter;
            itemBC_ET_01_DM_Informacion.Sizing = BarcodeSizing.Fill;
            itemBC_ET_01_DM_Informacion.Code = "123456789A123456789B123456789C123456789D123456789E123456789F123456789G123456789H123456789I123456789J";
            itemBC_ET_01_DM_Informacion.Code = item_QR.Get_UUID();

          
            if (Formato == 1)
            {
                ditemBC_ET_01_DM_Informacion___Ancho = 2;
                ditemBC_ET_01_DM_Informacion___X = 7.8;
                ditemBC_ET_01_DM_Informacion___Y = 4.3; 
            }

            if (Formato == 2)
            {
                ditemBC_ET_01_DM_Informacion___Ancho = 1.5;
                ditemBC_ET_01_DM_Informacion___X = 5.8;
                ditemBC_ET_01_DM_Informacion___Y = 1.3;
            }


            itemBC_ET_01_DM_Informacion.Width = ditemBC_ET_01_DM_Informacion___Ancho;
            itemBC_ET_01_DM_Informacion.Height = ditemBC_ET_01_DM_Informacion___Ancho;
            itemBC_ET_01_DM_Informacion.X = ditemBC_ET_01_DM_Informacion___X - dItems_Alto;
            itemBC_ET_01_DM_Informacion.Y = ditemBC_ET_01_DM_Informacion___Y;
            //////////////////// QR con información del producto  




            itemBC_ET_03_EAN = new BarcodeItem();
            itemBC_ET_03_EAN.Symbology = BarcodeSymbology.Code128;
            itemBC_ET_03_EAN.RotationAngle = 0;
            itemBC_ET_03_EAN.Font.Name = fuente.Name;
            itemBC_ET_03_EAN.Font.Unit = fuente.Unit;
            itemBC_ET_03_EAN.Font.Size = 2;
            itemBC_ET_03_EAN.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
            itemBC_ET_03_EAN.AddChecksum = false;
            itemBC_ET_03_EAN.DisplayCode = true;
            itemBC_ET_03_EAN.EanUpcGuardBarHeight = 0.55;
            itemBC_ET_03_EAN.BorderThickness = new FrameThickness(dBorde + 0.00);
            itemBC_ET_03_EAN.BarcodeAlignment = BarcodeAlignment.MiddleCenter;
            itemBC_ET_03_EAN.Sizing = BarcodeSizing.None;
            itemBC_ET_03_EAN.Code = Fg.Left(txtCodigo.Text, 50);

            itemBC_ET_03_EAN.BarHeight = 0.8;
            itemBC_ET_03_EAN.BarWidth = 0.03;
            itemBC_ET_03_EAN.Width = 4.25;
            itemBC_ET_03_EAN.Height = 1;
            itemBC_ET_03_EAN.X = 2.5;
            itemBC_ET_03_EAN.Y = 5.5 + ( dItems_Alto * .5); // itemTxt_10_Caducidad.Y + (itemTxt_10_Caducidad.Height * 1);


            //////////////////// QR con número de serie 
            itemBC_ET_02_DM_EAN = new BarcodeItem();
            itemBC_ET_02_DM_EAN.Symbology = BarcodeSymbology.DataMatrix;
            itemBC_ET_02_DM_EAN.RotationAngle = 0;
            itemBC_ET_02_DM_EAN.Font.Name = fuente.Name;
            itemBC_ET_02_DM_EAN.Font.Unit = fuente.Unit;
            itemBC_ET_02_DM_EAN.Font.Size = 1;
            itemBC_ET_02_DM_EAN.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
            itemBC_ET_02_DM_EAN.AddChecksum = false;
            itemBC_ET_02_DM_EAN.DisplayCode = false;
            itemBC_ET_02_DM_EAN.EanUpcGuardBarHeight = 0.55;
            itemBC_ET_02_DM_EAN.BorderThickness = new FrameThickness(dBorde + 0.00);
            itemBC_ET_02_DM_EAN.BarcodeAlignment = BarcodeAlignment.MiddleCenter;
            itemBC_ET_02_DM_EAN.Sizing = BarcodeSizing.None;
            itemBC_ET_02_DM_EAN.Code = string.Format("{0}", Fg.Left(GetUUID(2), 50));
            //itemBC_ET_02_DM_EAN.Text = "UUID";
            itemBC_ET_02_DM_EAN.Width = 1.2;
            itemBC_ET_02_DM_EAN.Height = 1.2;
            itemBC_ET_02_DM_EAN.X = 0.2;
            itemBC_ET_02_DM_EAN.Y = 5.50 + dItems_Alto; // itemTxt_10_Caducidad.Y + (itemTxt_10_Caducidad.Height * 1);


            if (Formato == 1)
            {
                ditemBC_ET_01_DM_Informacion___Ancho = 1.2;
                ditemBC_ET_01_DM_Informacion___X = 0.2;
                ditemBC_ET_01_DM_Informacion___Y = 5.65;
            }

            if (Formato == 2)
            {
                ditemBC_ET_01_DM_Informacion___Ancho = 1.2;
                ditemBC_ET_01_DM_Informacion___X = itemBC_ET_01_DM_Informacion.X + (dMargen * 1.5);
                ditemBC_ET_01_DM_Informacion___Y = itemTxt_12_NotasAlmacenamiento.Y;
            }


            itemBC_ET_02_DM_EAN.Width = ditemBC_ET_01_DM_Informacion___Ancho;
            itemBC_ET_02_DM_EAN.Height = ditemBC_ET_01_DM_Informacion___Ancho;
            itemBC_ET_02_DM_EAN.X = ditemBC_ET_01_DM_Informacion___X;
            itemBC_ET_02_DM_EAN.Y = ditemBC_ET_01_DM_Informacion___Y + 0;
            //////////////////// QR con número de serie 



            //////////////////// LOGO de la empresa 
            imgLogo = new ImageItem();
            imgLogo.SourceBase64 = Get_Imagen_Logo(); 
            imgLogo.PrintAsGraphic = true;
            imgLogo.Width = 4.0;
            imgLogo.LockAspectRatio = LockAspectRatio.WidthBased;
            imgLogo.MonochromeSettings.DitherMethod = DitherMethod.Threshold;
            imgLogo.MonochromeSettings.Threshold = 50;
            imgLogo.MonochromeSettings.ReverseEffect = false;
            imgLogo.X = 5.75;
            imgLogo.Y = 0.5;
            imgLogo.IsGrayscaleOrBlackWhite = Imprimir;


            if (Formato == 1)
            {
                ditemBC_ET_01_DM_Informacion___Ancho = 2.75;
                ditemBC_ET_01_DM_Informacion___X = 7.25;
                ditemBC_ET_01_DM_Informacion___Y = 0.75;
            }

            if (Formato == 2)
            {
                ditemBC_ET_01_DM_Informacion___Ancho = 1.5;
                ditemBC_ET_01_DM_Informacion___X = 5.75;
                ditemBC_ET_01_DM_Informacion___Y = 0.3;
            }

            imgLogo.Width = ditemBC_ET_01_DM_Informacion___Ancho;
            imgLogo.Height = ditemBC_ET_01_DM_Informacion___Ancho;
            imgLogo.X = ditemBC_ET_01_DM_Informacion___X - dItems_Alto * 0.75;
            imgLogo.Y = ditemBC_ET_01_DM_Informacion___Y;
            //////////////////// LOGO de la empresa 


            ////imgLogo.SourceBase64 = Fg.ConvertirImageToBase64(DllFarmaciaSoft.QRCode.Properties.Resources.Embarazo__001, System.Drawing.Imaging.ImageFormat.Png) ; 



            ////////////////Add items to ThermalLabel object... 
            tLabel.Items.Add(rectItem);

            tLabel.Items.Add(itemTxt_01_Empresa);
            tLabel.Items.Add(itemTxt_02_DescripcionClave);
            
            if ( chkMostrarInformacionComercial.Checked ) tLabel.Items.Add(itemTxt_02_DescripcionClaveCorta);
            tLabel.Items.Add(itemTxt_02_ClaveSSA);
            tLabel.Items.Add(itemTxt_05_RegistroSanitario);
            tLabel.Items.Add(itemTxt_06_Presentacion);
            tLabel.Items.Add(itemTxt_07_Laboratorio);
            tLabel.Items.Add(itemTxt_08_CodigoEAN);
            tLabel.Items.Add(itemTxt_09_Lote);
            tLabel.Items.Add(itemTxt_10_Caducidad);
            tLabel.Items.Add(itemTxt_11_Vigencia);
            tLabel.Items.Add(itemTxt_15_PersonalElaboro); 


            if ( chkMostrarCA.Checked && cboCondicionesDeAlmacenamiento.SelectedIndex > 0 ) tLabel.Items.Add(itemTxt_12_NotasAlmacenamiento); 
            if ( chkMostrarLogo.Checked && Formato == 1) tLabel.Items.Add(imgLogo);

            tLabel.Items.Add(itemBC_ET_01_DM_Informacion);         
            if ( MostrarBarrasEAN ) tLabel.Items.Add(itemBC_ET_03_EAN);
            tLabel.Items.Add(itemBC_ET_02_DM_EAN);


            if (chkRiesgoEnElEmbarazo.Checked)
            {
                tLabel.Items.Add(itemTxt_13_NotasEmbarazo);
                tLabel.Items.Add(imgRiesgoEmbarazo);
            }

            tLabel.Items.Add(itemTxt_14_FechaProcesamiento);

            ////_currentThermalLabel = tLabel;
            if (!Imprimir)
            {
                GenerarVistaPrevia(tLabel);
            }
        }

        private string Get_Imagen_Embarazo()
        {
            string sRegresa = ""; 
            if (DtGeneral.EmpresaConectada == "001")
            {
                sRegresa = Fg.ConvertirImageToBase64(DllFarmaciaSoft.QRCode.Properties.Resources.Embarazo__004, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            if (DtGeneral.EmpresaConectada == "002")
            {
                sRegresa = Fg.ConvertirImageToBase64(DllFarmaciaSoft.QRCode.Properties.Resources.Embarazo__004, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            if (DtGeneral.EmpresaConectada == "004")
            {
                sRegresa = Fg.ConvertirImageToBase64(DllFarmaciaSoft.QRCode.Properties.Resources.Embarazo__004, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            return sRegresa;
        }

        private string Get_Imagen_Logo()
        {
            string sRegresa = "";
            if (DtGeneral.EmpresaConectada == "001")
            {
                sRegresa = Fg.ConvertirImageToBase64(DllFarmaciaSoft.QRCode.Properties.Resources.Logo_IME_Unidosis__002, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            if (DtGeneral.EmpresaConectada == "002")
            {
                sRegresa = Fg.ConvertirImageToBase64(DllFarmaciaSoft.QRCode.Properties.Resources.Logo_IME_Unidosis__002, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            if (DtGeneral.EmpresaConectada == "004")
            {
                sRegresa = Fg.ConvertirImageToBase64(DllFarmaciaSoft.QRCode.Properties.Resources.Logo_PHJ_Unidosis, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            return sRegresa;
        }

        #endregion Etiquetas
    }
}
