using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

using System.Drawing;
using System.Drawing.Imaging;

using SC_SolutionsSystem; 

namespace Dll_IFacturacion.SAT
{
    internal partial class FrmValidarCFDI : FrmBaseExt
    {

        string sUUID = "";
        string sRFC_Emisor = "";
        string sRFC_Receptor = "";
        double dImporte = 0;
        string sSegmentoSello = "";

        bool isImageLoaded = false;
        bool hasResults = false;
        int consecutiveErrorCount = 0;
        bool hasInternetAccess = true;
        bool hasRemoteAccess = false;

        string sURL = "";
        string sURL_Base = @"https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx";

        public FrmValidarCFDI() : this("", "", "", "", "", 0, "")
        {
        }
        public FrmValidarCFDI( string UUID, string Serie, string Folio, string RFC_Emisor, string RFC_Receptor, double Importe, string SegmentoSello )
        {
            InitializeComponent();

            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Visible = false;
            captchaImg.BackgroundImageLayout = ImageLayout.Stretch; 

            Fg.IniciaControles();
            LimpiarResultados();

            SetTimer();

            sUUID = UUID;
            sRFC_Emisor = RFC_Emisor;
            sRFC_Receptor = RFC_Receptor;
            dImporte = Importe;
            sSegmentoSello = Fg.Right(SegmentoSello, 8);

            txtFolioFiscal.Text = sUUID;
            txtRFC_Emisor.Text = sRFC_Emisor;
            txtReceptor.Text = sRFC_Receptor;
            lbl_012_Serie.Text = Serie;
            lbl_013_Folio.Text = Folio; 

            if(UUID == "")
            {
                this.webBrowser.Navigate(sURL_Base);
            }
            else
            {
                sURL = string.Format(@"https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id={0}&re={1}&rr={2}&tt={3}&fe={4}",
                sUUID, sRFC_Emisor, sRFC_Receptor, dImporte, sSegmentoSello);
                this.webBrowser.Navigate(sURL);
            }

            getWebData();

        }

        #region Botones 

        private void SetTimer()
        {
            ////////////////////////////////////////// 
            mainTimer.Stop();
            mainTimer.Enabled = false;

            isImageLoaded = false;
            hasResults = false;

            mainTimer.Enabled = true;
            mainTimer.Start();
            //////////////////////////////////////////
        }
        private void btnLimpiarDatos_Click( object sender, EventArgs e )
        {
            Fg.IniciaControles();
            LimpiarResultados();

            isImageLoaded = false;
            hasResults = false;
            consecutiveErrorCount = 0;
            hasInternetAccess = true;
            hasRemoteAccess = false;

            this.webBrowser.Navigate(sURL_Base);
            SetTimer();
        }

        private void LimpiarResultados()
        {
            ////lbl_012_Serie.Text = "";
            ////lbl_013_Folio.Text = ""; 

            lbl_01_Emisor.Text = "";
            lbl_02_Receptor.Text = "";
            lbl_03_FolioFiscal.Text = "";
            lbl_04_FechaDeExpedicion.Text = "";
            lbl_05_FechaCertificacionSAT.Text = "";

            lbl_06_PAC_Certifico.Text = "";
            lbl_07_TotalCFDI.Text = "";
            lbl_08_EfectoDeComprobante.Text = "";
            lbl_09_EstadoDelCFDI.Text = "";
            lbl_10_EstatusDeCancelacion.Text = "";
            lbl_11_FechaDeCancelacion.Text = ""; 
        }

        private void btnValidar_Click( object sender, EventArgs e )
        {
            bool flag = true;
            LimpiarResultados();

            Application.DoEvents();
            this.Refresh();

            ////////////////////////////////////////////// 
            ////mainTimer.Stop();
            ////mainTimer.Enabled = false;

            ////////isImageLoaded = false;
            hasResults = false;

            ////mainTimer.Enabled = true;
            ////mainTimer.Start();
            //////////////////////////////////////////////

            if(this.txtFolioFiscal.Text.Trim().Replace("-", "").Length != 0x20)
            {
                //this.setTextErrorStoryBoard(this.txtFolioFiscal);
                flag = false;
            }

            if(this.txtFolioFiscal.Text.Trim().Replace("-", "").Length == 0x24)
            {
                flag = true;
            }

            if(this.txtRFC_Emisor.Text.Trim().Replace("-", "").Length < 12)
            {
                //this.setTextErrorStoryBoard(this.txtRFC_Emisor);
                flag = false;
            }

            if(this.txtReceptor.Text.Trim().Replace("-", "").Length < 12)
            {
                //this.setTextErrorStoryBoard(this.txtReceptor);
                flag = false;
            }

            if(this.txtCaptcha.Text.Trim().Replace("-", "").Length == 0)
            {
                //this.setTextErrorStoryBoard(this.txtCaptcha);
                //flag = false;
            }

            ////AQUI
            if(!flag)
            { 
            }
            else 
            {
                VBMath.Randomize();
                Array instance = this.generador_captchas((uint)((int)Math.Round((double)Conversion.Int((float)((50f * VBMath.Rnd()) + 1f)))));
                object[] arguments = new object[] { 2 };
                this.webBrowser.Document.GetElementById("ctl00_MainContent_ImgCaptcha").SetAttribute("src", "GeneraCaptcha.aspx?Data=" + NewLateBinding.LateIndexGet(instance, arguments, null).ToString());
                object[] objArray2 = new object[] { 1 };
                this.webBrowser.Document.GetElementById("__VIEWSTATE").SetAttribute("value", NewLateBinding.LateIndexGet(instance, objArray2, null).ToString());
                
                HtmlElement elementById = this.webBrowser.Document.GetElementById("ctl00_MainContent_TxtUUID");
                HtmlElement element2 = this.webBrowser.Document.GetElementById("ctl00_MainContent_TxtRfcEmisor");
                HtmlElement element3 = this.webBrowser.Document.GetElementById("ctl00_MainContent_TxTCaptchaNumbers");
                HtmlElement element4 = this.webBrowser.Document.GetElementById("ctl00_MainContent_BtnBusqueda");
                elementById.SetAttribute("value", txtFolioFiscal.Text);
                element2.SetAttribute("value", txtRFC_Emisor.Text);
                this.webBrowser.Document.GetElementById("ctl00_MainContent_TxtRfcReceptor").SetAttribute("value", txtReceptor.Text);
                
                if( txtFolioFiscal.Text != "" )
                {
                    System.Threading.Thread.Sleep(0x3e8);
                    object[] objArray3 = new object[] { 0 };
                    element3.SetAttribute("value", NewLateBinding.LateIndexGet(instance, objArray3, null).ToString());
                    element4.InvokeMember("click");
                }

                ///////////// Metodo inicial 
                ////if(this.webBrowser.Document.GetElementById("ctl00_MainContent_TxtUUID") != null)
                ////{
                ////    this.webBrowser.Document.GetElementById("ctl00_MainContent_TxtUUID").InnerText = txtFolioFiscal.Text; //this.txtUUID.Text;
                ////}

                ////if(this.webBrowser.Document.GetElementById("ctl00_MainContent_TxtRfcEmisor") != null)
                ////{
                ////    this.webBrowser.Document.GetElementById("ctl00_MainContent_TxtRfcEmisor").InnerText = txtRFC_Emisor.Text; //this.txtEmisor.Text;
                ////}

                ////if(this.webBrowser.Document.GetElementById("ctl00_MainContent_TxtRfcReceptor") != null)
                ////{
                ////    this.webBrowser.Document.GetElementById("ctl00_MainContent_TxtRfcReceptor").InnerText = txtReceptor.Text; //this.txtReceptor.Text;
                ////}

                ////if(this.webBrowser.Document.GetElementById("ctl00_MainContent_TxTCaptchaNumbers") != null)
                ////{
                ////    this.webBrowser.Document.GetElementById("ctl00_MainContent_TxTCaptchaNumbers").InnerText = this.txtCaptcha.Text;
                ////}

                ////if(this.webBrowser.Document.GetElementById("ctl00_MainContent_BtnBusqueda") != null)
                ////{
                ////    ////if(this.checkRFC(this.txtReceptor.Text, this.txtEmisor.Text))
                ////    ////{
                ////    this.webBrowser.Document.GetElementById("ctl00_MainContent_BtnBusqueda").InvokeMember("click");
                ////    ////}
                ////    ////else
                ////    ////{
                ////    ////    this.noRFCMatch();
                ////    ////}
                ////}
                ///////////// Metodo inicial 
            }
        }

        private void btnImprimir_Click( object sender, EventArgs e )
        {
            string printerName = "";
            PrinterSettings settings = new PrinterSettings();
            printerName = settings.PrinterName;
            //this.webBrowser.Print();

            this.webBrowser.Document.GetElementById("BtnImprimir").InvokeMember("Click");
        }
        #endregion Botones 

        #region Timer 
        private void mainTimer_Tick( object sender, EventArgs e )
        {
            //mainTimer.Stop();

            this.getWebData();

            //mainTimer.Start(); 
        }
        #endregion Timer 

        #region Funciones y Procedimientos Privados 
        private Image DownloadImage( string fromUrl )
        {
            using(System.Net.WebClient webClient = new System.Net.WebClient())
            {
                using(Stream stream = webClient.OpenRead(fromUrl))
                {
                    return Image.FromStream(stream);
                }
            }
        }

        private bool setCaptchaImg()
        {
            bool bRegresa = false;
            //// AQUI 
            try
            {
                txtCaptcha.Text = ""; 
                if(this.webBrowser.Document.GetElementById("ctl00_MainContent_ImgCaptcha") != null)
                {
                    string str = this.webBrowser.Document.GetElementById("ctl00_MainContent_ImgCaptcha").OuterHtml.ToString();
                    str = str.Substring(str.IndexOf("src=\"") + 5, str.Length - (str.IndexOf("src=\"") + 7));
                    //this.captchaImg = Image.FromFile(new Uri("https://verificacfdi.facturaelectronica.sat.gob.mx/" + str));

                    //captchaImg.Image = DownloadImage("https://verificacfdi.facturaelectronica.sat.gob.mx/" + str);

                    captchaImg.BackgroundImage = DownloadImage("https://verificacfdi.facturaelectronica.sat.gob.mx/" + str);

                    bRegresa = true;
                }
            }
            catch(Exception)
            {
            }

            return bRegresa;
        }

        private void getWebData()
        {
            //mainTimer.Stop();

            if(!this.isImageLoaded)
            {
                this.isImageLoaded = this.setCaptchaImg();
                //mainTimer.Start();
            }

            if(!this.hasResults)
            {
                ////AQUI
                this.hasResults = this.look4WebResults();

                //if(!this.hasResults)
                //{
                //    mainTimer.Start();
                //}
            }
            else
            {
                //this.cleanWebVars();
            }
        }

        private string getValue( string Item )
        {
            string sRegresa = "";

            if(this.webBrowser.Document.GetElementById(Item) != null)
            {
                try
                {
                    sRegresa = string.Format("{0}: {1}\n", Item, this.webBrowser.Document.GetElementById(Item).InnerText);
                }
                catch
                {
                    sRegresa = string.Format("{0}: ", Item);
                }
            }

            return sRegresa;
        }

        private bool look4WebResults()
        {
            string sRespuesta = "";

            //// AQUI
            if(this.webBrowser == null)
            {
                return false;
            }

            if((this.consecutiveErrorCount < 10) && this.hasInternetAccess)
            {
                string[] first = new string[0x10];
                try
                {
                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_PnlNoResultados") != null)
                    {
                        if(System.Windows.Forms.MessageBox.Show(this.webBrowser.Document.GetElementById("ctl00_MainContent_PnlNoResultados").InnerText + "\n\x00bfDesea consultar el procedimiento?", "Comprobante NO Registrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            try
                            {
                                //Process.Start("http://www.sat.gob.mx/sitio_internet/6_1360.html");
                            }
                            catch(Exception)
                            {
                            }
                        }
                        this.webBrowser.Refresh();
                        return true;
                    }

                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_pnlErrorCaptcha") != null)
                    {
                        this.webBrowser.Refresh();
                        this.isImageLoaded = false;
                        this.txtCaptcha.SelectAll();
                        this.txtCaptcha.Focus();
                        return false;
                    }

                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_VsResumenErrores") != null)
                    {
                        string innerHtml = this.webBrowser.Document.GetElementById("ctl00_MainContent_VsResumenErrores").InnerHtml;
                        if((innerHtml != null) && (innerHtml.Trim().Length > 0))
                        {
                            System.Windows.Forms.MessageBox.Show(this.webBrowser.Document.GetElementById("ctl00_MainContent_VsResumenErrores").InnerText, "ScanCFDI", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            this.webBrowser.Refresh();
                            this.isImageLoaded = false;
                            return false;
                        }
                    }

                    sRespuesta = "";
                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_LblRfcEmisor") != null)
                    {
                        first[2] = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblRfcEmisor").InnerText;
                        sRespuesta += string.Format("{0}\n", getValue("ctl00_MainContent_LblRfcEmisor"));

                        lbl_01_Emisor.Text = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblRfcEmisor").InnerText; 
                        //this.txtRFE.Text = first[2];
                    }
                    else
                    {
                        return false;
                    }

                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_LblNombreEmisor") != null)
                    {
                        first[3] = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblNombreEmisor").InnerText;
                        sRespuesta += string.Format("{0}\n", getValue("ctl00_MainContent_LblNombreEmisor"));

                        lbl_01_Emisor.Text += " - " + this.webBrowser.Document.GetElementById("ctl00_MainContent_LblNombreEmisor").InnerText;

                        //this.txtRSE.Text = first[3];
                    }
                    else
                    {
                        return false;
                    }

                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_LblRfcReceptor") != null)
                    {
                        first[4] = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblRfcReceptor").InnerText;
                        sRespuesta += string.Format("{0}\n", getValue("ctl00_MainContent_LblRfcReceptor"));

                        lbl_02_Receptor.Text = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblRfcReceptor").InnerText;
                        //this.txtRFR.Text = first[4];
                    }
                    else
                    {
                        return false;
                    }

                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_LblNombreReceptor") != null)
                    {
                        first[5] = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblNombreReceptor").InnerText;
                        sRespuesta += string.Format("{0}\n", getValue("ctl00_MainContent_LblNombreReceptor"));

                        lbl_02_Receptor.Text += " - " + this.webBrowser.Document.GetElementById("ctl00_MainContent_LblNombreReceptor").InnerText;
                        //this.txtRSR.Text = first[5];
                    }
                    else
                    {
                        return false;
                    }

                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_LblUuid") != null)
                    {
                        first[1] = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblUuid").InnerText;
                        sRespuesta += string.Format("{0}\n", getValue("ctl00_MainContent_LblUuid"));

                        lbl_03_FolioFiscal.Text = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblUuid").InnerText; 
                        //this.txtFF.Text = first[1];
                    }
                    else
                    {
                        return false;
                    }

                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_LblFechaEmision") != null)
                    {
                        first[6] = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblFechaEmision").InnerText.Replace("/", "-").Replace(@"\", "-");
                        sRespuesta += string.Format("{0}\n", getValue("ctl00_MainContent_LblFechaEmision"));

                        lbl_04_FechaDeExpedicion.Text = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblFechaEmision").InnerText; //.Replace("/", "-").Replace(@"\", "-"); 
                        //this.txtFExp.Text = first[6];
                    }
                    else
                    {
                        return false;
                    }

                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_LblFechaCertificacion") != null)
                    {
                        first[7] = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblFechaCertificacion").InnerText.Replace("/", "-").Replace(@"\", "-");
                        sRespuesta += string.Format("{0}\n", getValue("ctl00_MainContent_LblFechaCertificacion"));

                        lbl_05_FechaCertificacionSAT.Text = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblFechaCertificacion").InnerText; //.Replace("/", "-").Replace(@"\", "-");
                        //this.txtFCer.Text = first[7];
                    }
                    else
                    {
                        return false;
                    }

                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_LblRfcPac") != null)
                    {
                        first[8] = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblRfcPac").InnerText;
                        sRespuesta += string.Format("{0}\n", getValue("ctl00_MainContent_LblRfcPac"));

                        lbl_06_PAC_Certifico.Text = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblRfcPac").InnerText; 
                        //this.txtPAC.Text = first[8];
                    }
                    else
                    {
                        return false;
                    }

                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_LblMonto") != null)
                    {
                        first[9] = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblMonto").InnerText;
                        sRespuesta += string.Format("{0}\n", getValue("ctl00_MainContent_LblMonto"));
                        //this.txtTotal.Text = first[9];
                        first[9] = first[9].Replace(",", "").Replace(" ", "").Replace("$", "");

                        lbl_07_TotalCFDI.Text = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblMonto").InnerText; 
                    }
                    else
                    {
                        return false;
                    }

                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_LblEfectoComprobante") != null)
                    {
                        first[10] = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblEfectoComprobante").InnerText;
                        sRespuesta += string.Format("{0}\n", getValue("ctl00_MainContent_LblEfectoComprobante"));
                        //this.txtEfecto.Text = first[10];

                        lbl_08_EfectoDeComprobante.Text = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblEfectoComprobante").InnerText; 
                    }
                    else
                    {
                        return false;
                    }

                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_LblEstado") != null)
                    {
                        first[11] = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblEstado").InnerText;
                        sRespuesta += string.Format("{0}\n", getValue("ctl00_MainContent_LblEstado"));
                        //this.txtEstado.Text = first[11];

                        lbl_09_EstadoDelCFDI.Text = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblEstado").InnerText; 
                    }
                    else
                    {
                        return false;
                    }


                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_LblEsCancelable") != null)
                    {
                        //first[11] = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblEstado").InnerText;
                        sRespuesta += string.Format("{0}\n", getValue("ctl00_MainContent_LblEsCancelable"));
                        //this.txtEstado.Text = first[11];

                        lbl_10_EstatusDeCancelacion.Text = webBrowser.Document.GetElementById("ctl00_MainContent_LblEsCancelable").InnerText; 
                    }
                    else
                    {
                        return false;
                    }


                    if(this.webBrowser.Document.GetElementById("ctl00_MainContent_LblFechaCancelacion") != null)
                    {
                        try
                        {
                            //first[12] = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblFechaCancelacion").InnerText.Trim().Replace("/", "-").Replace(@"\", "-");
                            first[12] = this.webBrowser.Document.GetElementById("ctl00_MainContent_LblFechaCancelacion").InnerText.Trim();
                            DateTime time = Convert.ToDateTime(first[12].ToString());
                            first[12] = time.ToString("yyyy-MM-dd HH:mm:ss").Replace(" ", "T");
                            //this.txtFCancelacion.Text = first[12];

                            lbl_11_FechaDeCancelacion.Text = first[12]; 
                        }
                        catch(Exception)
                        {
                            first[12] = "1899-01-01T00:00:00";
                        }
                    }
                    else
                    {
                        first[12] = "";
                    }

                    txtRespuesta.Text = sRespuesta;


                    ////first[14] = conf.Station;
                    ////first[13] = "1";
                    ////string[] strArray2 = genericScanCFDIQueries.getParameters();
                    ////Console.WriteLine(this.txtRFR.Text + "----" + this.txtReceptor.Text);
                    ////string query = genericScanCFDIQueries.OnHistory(this.txtRFR.Text, this.txtRSR.Text);
                    ////myConn = new dbMySQL(strArray2[0], strArray2[1], strArray2[2], strArray2[3], strArray2[4]);
                    ////myConn.executableQuery(query);
                    ////int vecesLeido = this.checkIfUUIDExists(first[1], !isRemoteConnected);
                    ////if(vecesLeido == 0)
                    ////{
                    ////    string[] second = this.checkExtraColumns();
                    ////    first = first.Concat<string>(second).ToArray<string>();
                    ////}
                    ////if(conf.PrintConfig > 0)
                    ////{
                    ////    if(conf.PrintConfig == 2)
                    ////    {
                    ////        string printerName = "";
                    ////        PrinterSettings settings = new PrinterSettings();
                    ////        printerName = settings.PrinterName;
                    ////        this.webBrowser.Print();
                    ////    }
                    ////    else if((conf.PrintConfig == 1) && (System.Windows.Forms.MessageBox.Show("\x00bfDesea imprimir el comprobante?", "ScanCFDI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                    ////    {
                    ////        this.webBrowser.Document.GetElementById("BtnImprimir").InvokeMember("Click");
                    ////    }
                    ////}
                    ////if(vecesLeido > 0)
                    ////{
                    ////    GradientStop bColor = new GradientStop(Color.FromArgb(0xff, 0xc7, 0xff, 0xb2), 1.0);
                    ////    this.setButtonImage("repetido.png", this.txtEstado.Text + "(Repetido)", Brushes.OrangeRed, bColor);
                    ////    this.Repetido.IsOpen = true;
                    ////}
                    ////else if(this.txtEstado.Text == "Vigente")
                    ////{
                    ////    GradientStop stop2 = new GradientStop(Color.FromArgb(0xff, 0xc7, 0xff, 0xb2), 1.0);
                    ////    this.setButtonImage("correcto.png", "Vigente", Brushes.Green, stop2);
                    ////}
                    ////else if(this.txtEstado.Text == "Cancelado")
                    ////{
                    ////    GradientStop stop3 = new GradientStop(Color.FromArgb(0xff, 0xff, 0xb2, 0xb2), 1.0);
                    ////    this.setButtonImage("incorrecto.png", "Cancelado", Brushes.Red, stop3);
                    ////}
                    ////if(((this.cboxCategorias.Items != null) && (this.cboxCategorias.Items.Count > 0)) && conf.UseCategories)
                    ////{
                    ////    first[0] = this.cboxCategorias.Items[this.cboxCategorias.SelectedIndex].ToString();
                    ////    if((conf.UseSubcategories && (this.cboxSubcategorias.Items != null)) && (this.cboxSubcategorias.Items.Count > 0))
                    ////    {
                    ////        first[15] = (this.cboxSubcategorias.Items[this.cboxSubcategorias.SelectedIndex] as DataRowView).Row.ItemArray[0].ToString();
                    ////    }
                    ////    else
                    ////    {
                    ////        first[15] = "N/A";
                    ////    }
                    ////}
                    ////else
                    ////{
                    ////    first[0] = "N/A";
                    ////    first[15] = "N/A";
                    ////}
                    ////if(conf.FindsXML > 0)
                    ////{
                    ////    this.ask4Docs(conf.FindsXML);
                    ////}
                    ////if((this.isLicensed || this.isLicensedWithRemoteAccess) && (vecesLeido != -1))
                    ////{
                    ////    if(vecesLeido == 0)
                    ////    {
                    ////        this.ingresaFactura(first, !isRemoteConnected);
                    ////    }
                    ////    else
                    ////    {
                    ////        this.actualizaFactura(first[1], first[0], first[15], vecesLeido, first[3], first[5], first[6], first[7], first[8], first[10], first[12], !isRemoteConnected);
                    ////    }
                    ////}
                }
                catch(Exception exception3)
                {
                    ////this.consecutiveErrorCount++;
                    ////ErrorSet.add(errorTypes.Generico, "No se pueden guardar los datos leidos de las facturas, o existe un error de datos.\n" + exception3.Message, 10);
                    return false;
                }

                this.isImageLoaded = false; 
                //this.webBrowser.Refresh();
                return true;
            }

            ////if(this.consecutiveErrorCount == 10)
            ////{
            ////    System.Windows.Forms.MessageBox.Show("Se ha detectado un error grave al intentar obtener los datos, por favor reinicie la aplicaci\x00f3n.\n", "ScanCFDI", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            ////}
            ////if(this.consecutiveErrorCount < 20)
            ////{
            ////    this.consecutiveErrorCount++;
            ////}
            return true;
        }

        #endregion Funciones y Procedimientos Privados 

        #region Captcha 
        private Array generador_captchas( uint numero )
        {
            string str = "";
            string str2 = "";
            string str3 = "";

            switch(numero)
            {
                case 0:
                    str = "94973";
                    str2 = "tRQk7/eJX4MjepJMTimY8p8o82j5nzZACoRls4u6rnso+ycsrngP937Pzsb10XYZidA+r3SLwWb1CBKEXiZFYIs7IfPUswkMT/57fw20pFleBScYg+Asss8sWAS1iPOlN5uBvNSsvn46UsW8XBCaR03FrOX/JBfJVSAXF4ETQO1NDVOHQHrOfLwpGtcVnOpD3qIwPii6Cs63MHSCUN/NA9XhX7CmlhwaaWMe/3a1GL0ihbV57JsOmVRq3KjZUbEi5dzcYCW/N8+5gwEXR495aKdbBXnGVX3bjKM47KaC77lFD4ID1SCZWdI96mCzLS5PPdrzQ8MST3MNuv8J0BlpgFYCCIeouIttKSN4DNMV+CZpHXb8";
                    str3 = "5d5lY5QaP0OQBHhxNTlFzA1F5p6I9kaGYepMbCst5r62U2EbHgoJyTQ6/poVQQ81W+qEH0wNqKsRtLB2zwDVdlA7Ya8h2A9iJHUtwIUNz+I=";
                    break;

                case 1:
                    str = "51256";
                    str2 = "n9Zyk605d8vs+eMYMvMDimab7LmubhUlt5cCl2pn/q/TzqOtJx4NxI3s8WZVnq6sqS3LagXWU3p4y1BXQjr7+u/JYQTyC56qPCgcWzaH7QoZuYSQJ5t1kFC7UA48DeecRbyqz00P29VLInZ6vhaWXsOlkKzO0t2Udyw1f0apk2GL35DKuh0tXhH1NmzknQKUB5GGA3U5vclimSLzmdeQekOzqyR4WbcyMb704ahuj0XLEUxwSiCrETILgeTzTmrKdQQb+d/BNjPj5Rtsn+V1VEU2nXZkYcK+sN++YsCLrWLi1/cFByZ1aNSu9Z53mCDSRIznTDPY5BNkyLveFeavhgZna/0AdnoYcijUuJ4jq2mZWsSd";
                    str3 = "JMu1BWcbpUDfsPmk3WbhsCPgnZ0xpV3fKIZVbzOmdFD+U9cakI/YycoYpCiYegH6ieSPwL9qM8kU2extgWnIj1ote8ZT04fMangrObA5vw8=";
                    break;

                case 2:
                    str = "59002";
                    str2 = "OirH4evFsP3BvhHWEnGXRZKRDFqQGyfwEzfe62I37g16ytGUrjnbjuhv6I+Ok+BELdwh/e7T2tcq2dl4zglJvVyVdnPGBQVSXXOcINOWshX3aXhcFy4jp2eRQNq+zYGH2rSOhQN72iex78lcPfB35RnBh2K5IrDV4C/hTSC0vuKeil2wiYAXrMp/zOnBMqAO9p39PJn+YPZkeQ7EJHVPK8lKLYBe2bY+s6EU0mKOMm+pNO3YTWhaHTMGNtP5Jns4sJhgctjVcvdeja4PVhOhzdA66qloU4RPMLT1KGFFdSUf3OZyHOhb1wXGZ8l1Wx1/YhykBePdfz9w35YAV+A8+CPaUyLnXojJ5He9kNsA5ksc7obG";
                    str3 = "CTfkJh4Yg61egu6NG7+KTcLhNLEer4kiCIiiUKS3uHcOI44JCzIfzxullv6voS5VSUFWjDs8Nj083Kn0fa7cIFm5gJGrMW0CzUnvA2S5fOo=";
                    break;

                case 3:
                    str = "4698";
                    str2 = "fgsuOOzwaGE+qQqBsBrTc+jyXDJyiSnCF/bQE++UyI/JTqoMCGEtlQW5uTzQZ+pKCMwkOMkpZKuPYLE6lqm6I2+W10V4Kv8QUjfXaf7dIoisrnIu8XZH54ZhmldyQUy4tnZPzj4I3o80Pv6UYsv6g2vBHC+MOwPOzP6X19LOqvNKa7mLIftjyGhufo09e38uwsCF0Sb+5FpmY82bDiwAUmLWOPef1PUkzhZ3HonraStF/UZoXC4pHNi8DuOTNPYgjk8pDi24XxqNGPpZQkPlvfhI6KOUDywzsiuyWoxnToXQQ8RcgH1Pxe+lEstAQaw/d/38Ww==";
                    str3 = "f7Cvh6oAXT0WeCxRxqJFP1fel9s/avxu7cU8o1a3nQnYSTZutRr25or5bXmaCXB7t8U1j+0N9GqebYFNBoy+0w==";
                    break;

                case 4:
                    str = "84526";
                    str2 = "ZvAgVFGA6iDfTEddNyiosskjku+7yufijRbQ6SwkOSTo9VadlXs4Df3waei646gYevHGbQVXMyNcMlSIGp3IK8TP6uCz6DgPxz6cJSsgkOEceWlO8DAcVhaMKud2xvj8ZFRiqQayYMJCmSZZ4ijlMgqiw0CEJ9xcIfRGRe3Vz0RymcdPL3yvSMySpzoVDWmVjo6nGIgrZENXTCEAMDZwZTd+g5CFWc4YC0jezfj8v2h7KgFPAQcGm+d5yN0ywurkMVoDW5ySQqNgkIHuyhapJYPezpIGl9yE3JrAsGm398GDLlxHui19YbELd9Y7m8CN7I/YM8XkOUPObm5YSQ+QrEVZReiACibL14Yf3HzP0+L3WOJE";
                    str3 = "B6/B6/PaC06WWwpg+wRo9xf1S0CHT/k1tjPgdOueQCuhqKuH8orQ5fb5Ert10EKqO5u6bBCDOd/DdYuNahzMldX2NqBuRqDyuwnXH/d3lnE=";
                    break;

                case 5:
                    str = "41351";
                    str2 = "jXWpdChy2ljHB/VT+rqJkksWcEVCdY9Ph0APTjT8vTN1Hwbveg+Ntt1HMIm9gQoAHnk9P70t8P4SYoKXiaVvpIsyHksLiuMLu90NnAm4oU22Kne6N8XMCQitNulTFg0VeJ7u7s3nv7rmAB7w4OZGEkIDpN5Vw4F5t6vOvxd5DKmUGxexLSo19Acf0ntamg24q1eqAeQ2LiyKwZ6AvzbESUotQfjDiwZaOYQ1cv8HOFHfAynPFiJ+x/zUNHFBxjz0dUPpgh2Ysr0qoPAVOXZJHN8SOo8ZszNo+Hby36Km7TsP/quSEwu8eJ51N6P/QF00Bn6dIsYvgCxtdGId2j7n7Sh0A6OL3/PrJ59aRSDnR3lUKzA9";
                    str3 = "Ow0NWUsm2dgxIKlv4cGkjDd2VKO9tkQHQB6votZncQTZ1iYXKC1E5SVDJAj5+bi3RP+ElKnSP8KUKM5cSokGhluPLSF49OzAc2QzfHudrN4=";
                    break;

                case 6:
                    str = "6565";
                    str2 = "H2Dym8F4xBDJBTPqbL/iW6PAoXUb1kU3alaBvgiOTFCF/LppprLhQKMQnM8YTmrk3dsMaVCSqpTjmasK2gkGlhEJFstCc5L6DOfd3JpaRgKzzO70J4sHdAPeR6oCdbVs0utkwF6f+RMd7nQIdwnXabWA8Z32LlqFphMBZav8K0LuDYI9lSJI41Spb/YvDUup2DcQDdPc6VFxlmjOUGNIQ1GlGrMBQgDPdq3MvBPCdcj9KS75uK0highPkj4avkrjvvUMwLofUos8p28NAVutlZtjCuyK00wEcSwQCjBv15PgRFvAyqNFpHTJQotXvJt12vqJGw==";
                    str3 = "W3Ba/rhSKX3IxedInxwZ6hlUHTnxEhYvl9tSmX0+62fGEF8e9576gJjSx6bPbQeV2P3RZc0hY3l7Mq0TeLWRJw==";
                    break;

                case 7:
                    str = "62728";
                    str2 = "93rI75T2kPaXM6gyBjL4A8A/3BfoQDfvjc4ZRvePUz3t2cFfln6Kplv86BnAffXV/n5u6DCAtZQDtSN9L8rvjfBvepSXGcQRrgaCRz9tS5qWFJh2vN2RSr1qyEv/M6/XdgpmXvdbhs3Z5MU4AKg03MvYys04VGgb/PLVLlMQiQD6drj+7IU7x2u9NelwsdBHXg2kfKKxH1VJ86Bm5iXzhOovDD3l3wppcNEJ0VtMs5In9xTaNQDthNZMx6/HlPNXDm7detNRZGLA6u+hOkv6jLvmMOVRQRcAAmv86H1jlc7J+kVTMPwq7OPsC9i1NyVDTnNKd4zDUAgLN1D4kfOilt4CEAMm9OX3ewaRP+3enMSsZ+Yx";
                    str3 = "iEMxQc0n154IoC+mJTx1ahqnoxGi3RCXtFkpJMXQUQR4s5utR8jADkfg5fpCZYUG3ca06Pi8z4nacDHSFFMPhuMLmZ7tU5bZ3wRbhosMzQ4=";
                    break;

                case 8:
                    str = "60336";
                    str2 = "yik/s518mXeGWnjUBRVlC0VGK3tLgiiLMP5o8twiSlw12e0EvoADPzudpu/8DVntxrh73ksQje6u6jMgiWBVJYhJJBFWL4FiMXmX46I1iyRbS4VknVCc5+jqwR6pz6NrDfTYnyE36RZZcJvd7+1WvZeESlvHmBRVBsD0kD7ag1Rpv8mM4GwurD0GfnHqn9Bd0lBYbTCUmsGxxJT7nLJ14wtt+aOyvV3pF6VjJHPw8y0AQ/zwY6bgMpxvVc77cuECiVIhzGNPbe5w95TjiYgqG/yWRjVHG0Jt2AmeJ8l2BZIfLuPFHPMBfCDefJd6JCeYY/WPglHGzaanIUTEO9rRCi6+FXOUK4JPesDcU6Hgx2ETWMaW";
                    str3 = "NNyUrP5KNUM4jpENCwpvcRLqYeYFD7nrPatURp0W2g5vtrmMmtDkLQQ+i7TcYiEfWLb0HCe2FdtRT62Zs78wNCz6GaZ61H31XhcOds3xxr8=";
                    break;

                case 9:
                    str = "11266";
                    str2 = "R6qaeSD6pyhcU1EgnrwcfuqlMSKzPjq3hAF5GgFCIa1boPMSGue+LA7IonCNlQs+tOUt40hrMkd+x11beL/JW3YUwi5gXdj17OWrc4ysNQhnNHZB28t96WgF+G4GoGRNDNKa+rO7Ltgc3StRCVh+AA4N4iifjTq0Nh3ijr8+czTViaYZk3HC10mhkGMN6KHG7LG4Q77G8fM46qJQ3qhB1sgSCev+bWJ46naudQbgzoB9gZ9qseo/QIPqjYVjvVHMLJjKllNZ5gnYqzkftxqntZURUogRFr/cWns6kqFJnLDFErddmSzDyn/QoqOMdz4b3c65Kqmg15yYp/hH2/olHIi7EicfVVBz4mpRMqcj/x/1MquV";
                    str3 = "0R0USh+rbWb+sJ3dJFKuh73hoIqli7YXGlSxkncEVjcI2JTWu3wU9d/Ggs89DxWQ8PwTg9LINmoDOUSOz99n5AniyaMZiUVcUP3/Nyw/m1g=";
                    break;

                case 10:
                    str = "35366";
                    str2 = "Vv5iVJUZ4MAJoELFLPPLaZ8tQWVjCzyQ3C8OI4xSGAA3mH2T8v8KXs4Vd3PL7eK22gJuS8S54V0zoNE/dVo/GBHOt33hVXWk4DghMOzNWlKaYawC/6OprghxwrqYqeM2S3/Ys00GaNsw7WP1at774fHZIAow5fk+30xboZ7vGEHdATozBoSFYpqDs3n0FAwoHZatfHEYLofEOnqC2fJOL/I1aQ5scY0aLDzC6kKFzYNlROHKYQSF5+8zklptHLX0QQwhjVSHCGvk/3EFnHwBYTTs6WXd8Svhvwy1xol8xPUTqFolPZ6eI0/PC9LPzC45c6xqsZen8Zwu3R/XlSrg6YCIqPQzWEZll/+gyI2F4G/0kdgQ";
                    str3 = "qryV0UhhJezPjyP7kONnqw6cCRursRXYi/HPvUTLMPUZVuXOTDZHL1d+i0PBuNg3OxNJ/KMSf59oCsgJsUahiEMkkwzQeL4/fQhRsPqza7s=";
                    break;

                case 11:
                    str = "25340";
                    str2 = "VYN13aNkuXLBb6IDNRitD91tXDM0B4k4hJBFgabpB1JrhalDCiR2wR9HNULmJVvbNEkNoPgacr4Hmtl7DPlaiQS2CZdTxAh+B/kphe2S9f8dkaiFeEGUxiFc8sQ9HKzZOUipQfDKGgfpaon+Pyhu7Z/RdjWTHCC97ewSDVdqZOEMhBGGwkw8IU8a6EHpkFg31kygZeXiAkMVZgN8fGtj0EHjoubN26g8Pyo6Ob6/mJnL5/WtlKRKPhQbDHd+XhkXyLOrhBoJuHseqLe+23GhgaobR8t0mVAjgwR6gz0oI3l1+fyDeY7krukDwHs7FD2kB4dcGrD+EnnzN1nL9fRZTHVCGc2KxnAdA37CnUcVF/hkEEOd";
                    str3 = "hFfYr3wcu6wh4kUQdvZhQoNosuOtNEV+qtRhdnJefnqcOYKluUDisi6uhb4I6GMzOx2V3tnPveRs5XKJCuLs13gMO4+1AoVlS+w68J2HrQI=";
                    break;

                case 12:
                    str = "27852";
                    str2 = "FySx9NZzvltZYVELzJ1Tt+MSf7hN1aOIclW02zuM0oASRZYB20MrUupdd5ZM2XKoslnKKsGf4Q7fdhFjpqLwNEjTob4gIjQTimy6Grki6IktyxIEEDQ1NGSjD1/E3/UoUJb3nzzn0MZMCaerxtnrSgz0sUuEYQctYwkZpi81mgmujNTyuMabmBN+AwDb12mlyHYMUWEmCmXXjujGYTHt5K/rFNex2ix6shUqoweqy26SsMSkLJ4tMh3/C6lFi84S37dOdpKUAyGocyyIXiSmKlWWbBh4wk16Nuu2qpwc/CZURRX0yGksCdN7juYXPB8CrURdhhaOQfwncmcFbYm3PPQ3Vc5+Y2CRSaQvgDoNDGslNnpf";
                    str3 = "p9F1zxHBIr1j63pOmYBfFhGY2PYdWxOB+rIBHt4Hqj/VuLSdPE9N7+avjXe7X/3UCjTh8XtD9/6a7K/A+4GTaExi6rEBPoRkzntlDkOJBjo=";
                    break;

                case 13:
                    str = "86092";
                    str2 = "/GB/lYmjkoOV+rfhJHSbzMLItqYjBq8C4rtJTSuW6CteRWjNfqDDJ+iFg/TsiRq4l41Tx4mq9cEeO6wrtD2nTaJ9ai+gb7Wt6GUIbLA8SYpnt6zbfBURY44DzAd52qxHEZGOZwBFZVjlcAdPEAPgHOEvM0eAh7JhqlEbCJ5Zppyl3tXP6wSIBbgDCM+0uOWfcDYL57YQf1NE1wuWSPGsVFFVM7jaWZGy8zdIKsPsOVW8JfL4UG/VgBqnQ3CdDTv0iHmqInE24wRsUZOARURcxt+A7yc3uTlfF2WZxCJ5Cd08qFcCOKD1SULLLP4/nxx2j5vRv3y2Q000LZOvLzB9Iys81P5HbCDje3oWFwqeUuYStLeN";
                    str3 = "ddM1EPEgf5F7VdL5lInmvYLSas9n++dJx/TnKvNJOTwm9HNwMpI95zbIuFN9wiqCsBZdzsRCgQzzfg28TYiDZcaBRLur95H7pmDMpbqDsq0=";
                    break;

                case 14:
                    str = "40509";
                    str2 = "7I2ekPNo4gLHsf+YbMCP4d4hIPfQhoyks5TmBWx7xQa/BzXWHzzlDoA4DDUR52D8l1doGKL9L48lHuboIHA8u5PW14NMojOPKlo5qu8UQvWNNHgwzQD463k2Ei9OXPD9MRkWSglClEwUThi0EfCFhORo89Ib7sULwEwMefVPfsmsXbPjVAJ9+A7TU6iDnq0JfBW8NI0zAzVilk5ayZnkh6mZBDsOdiHJzBROImiy/htaruqsrHNBK6DJgtTBakXNq0K/rrg9SLWbvGGWcUn1nUsdNhhXiL9m1fzKBK6PR/BBqFMaYlTUKJbquFH5Z71lHwYY0d6NMv/qxuOU3LrPMhq6BhfuqZYLXdCPtR3bE4qTs/BG";
                    str3 = "6jCIMndDG6FAbXn0R9HgdlyV9/6XGMOpn5ZTW5Tqv9ZDSzHNgKttmw/bWYlBry0GgNhymeol9xkq5akjq0ADg2+jrsC1+G7z42RX142BlEE=";
                    break;

                case 15:
                    str = "16851";
                    str2 = "PhSSGoR64xwUAZgoD9O3qKw3rpqlYQ8PGiP1yKEPfLYWZbaEDedxZYFaF9k/g2v7/GzhGGLD6v22cuvhS+g+XSnsR/C1Hd40h3AFNVVcOOJvzu6UT7JKxZeCpWWgs7P5dc6F3Qx6o+gz5LNLDadvfczewoTKbm9a4puJxhSeXRQi54aKdkv+bVDdTj4gUzwOwb5jylgEcwpzmCPZ4kfuZAGLbqHoNGxXMC3t8iylDhtA4AlGXTpxUVCFeYQWN5IS1MQus/1ofL32yfK1Q355wXre4G9Y8I4k3u5SbCAKAxn5vlat1TlfmQziodqO3zbDOKvU3Uesrf1+GmLM8hb+x9OkE7IyplPUkVqvkfds5Y6W4OiB";
                    str3 = "wpQgdHtVya/9TM7e5UZQxBIbemm9PYGMYXk6ZxvEoKg9rhbn/gdFtvYlaWqHSH4vdXmc/NGdE3P+lNhLJMaikAmwBvP9/YFHG95C5ee/yQQ=";
                    break;

                case 0x10:
                    str = "36279";
                    str2 = "MjcyDNkR/MCPZLw87DudDaJBIJfgArZQwybxy8rYeuREobmlvG9DIlD7zt8XRstU1aeI1yyGBZhD7ey9nSDVG5KdKB/Dy8lKsKYNq2a0JpIKNc4uAJfsEFdA7Wg64tkni40PyboOk8tN1pBOeH2QvFrcHc/HPNSEDdOUkudxuoYTzmZEjKEHeGDUCjkweLesd/9mcWT0hOXP74WwJuJYdAFyYhlXk5wyC/iA28OVGHI5V4yxuaIGiozbrhr4e8/fAfhIsQ3idonn8aeLsWdyOvcpztD1djHHbI5slnUQ1xvvzyhcsFsp5Gght+AHgVRi9Ga7csO3sU6UqhcGiFvr/J7+ZzFme+F6SnBB0+nDVu7OoOfy";
                    str3 = "HiuHTHmGx9R40bWYQ1p76EUYRo8t33rqkVv+buESeGUrd7HvvQscDP1g7PcBsLHgHB9e4I6Ba0I2v1dkChkAtiCoLG/tSuSO4sIJHwQjdSw=";
                    break;

                case 0x11:
                    str = "13498";
                    str2 = "F6SUSxAiF4/aOx9GDiZE9VaMiJduYydQJgSg0MDaSDVl1zFOeBMhGrxzwGIWgjw+y5qt1gv/Zv2L1oxUAMUSboM2xOlw0K/p9FepXPSQkujQnERiBYKjdvwNEO+vL6mC+WsjTQMLQoqlizde2MdU+EQG7DPcwolWJCR9ZSc7tJKj+C8eLO28nisHADaNXHyLCTqsJuAb/3zHlbCRmrsoasVJdwsiPgkwvdUjE5nFTP76RGOIXaUHo4+h7Q8belgRF8UTl2vKvKnTIQiL5F2wGon/sas4MG2CutlD7enDCBYl/RE27bm+Cvv8otGYylVjxMtU8B1I6FPsYNhR5ICS2PDUIjAn9yWgBSsqbKnSdB8L1IMc";
                    str3 = "RH0SG+WId4uVhrr3G4lwx0NlHSOwwyI/1YZ6JIir3Wc15cu7XOdGBsg01rDN0zOEUo2ca698+oF2MtmsjeWgyCIZah8866KrKJXvjRUsrMY=";
                    break;

                case 0x12:
                    str = "67913";
                    str2 = "snuzerbBTFAEm/shMrcqWbAcli3gFbPCMu4bV1hvmP4x3A/xPVEaV7LucpdH2SNQubixEgNMvIXeGKkHht3GGr31qxZGf+At7eL9RtWC2opgEtAT+YmjmXF9Ec5Fb8mPFs9QIxOyNmiM3Oj2dF13PCZbtR0I6BKqNxRf5nYsshwBu9NzWXANwOKClqhWwHL53Ks6L1ORizpZxonhchA4/rbvCrjJDFdX+6TQH87xcPRxdiWBWFY7FAnjdZtSFUXsl14y0lJc1PIAOUPhPNbHPllqX6nl/OHO1ipe1a4XDpBLc4hVm8Z2cB7zKUvmJDSpOd0thqxKzT3/POcQdTm8BBQ8RqmDFeUCc/QAUAWrt6OKHIb0";
                    str3 = "d1hJHIotlhzb2bqMqZXcZZMn8fhoflpq9SXlXgIy7T9vePRROSR0iRNlR134QUSRPBSMhZIaTwLROziMtD3JvubdqA4kznkeIzuQb3YAfK0=";
                    break;

                case 0x13:
                    str = "49490";
                    str2 = "y0gaCBKGuUNlmX2/T1J8RLZbTGxAEpMQzddouK6c5XPzalutMtSGuPBDAjd8tBG7KyLQfevNbBRlwcPchWEpf/FF1YrS2/2N6WFMmhyF/WpaoXu42aJQC/3kzKyHhXP72gLA/XRUf0Mz8EId9hP3hUnIFzCn2xRQasLixtmvV/l2KTg8KlLwbRWo4HoBy+6HkrV3XR+6y9dPbRHWr1YVLFruHIroSHgOqgJyFvX3JpD+TcWNGoP5NSuUfToyTVm0bsIIzbw+ZHHLnNDhLYHkbsSLijq4g+WYgoWNUJDspCd/aq3U6oJPjyM3leKbBU8ZMdulbCSpvHVYfXm4P6Mx04yUMRZSW6R2LBsietDfGDW0iQed";
                    str3 = "mceJhkQ8zWd2xfmzU3TVgPT+Ihwx/tswQXVzaNwRXfS2WvM5SN43dH74vNgD2GQgiouuzuJtmi5/VOwmOqz3qQdaFkp+8qofwE8H/AeXtFA=";
                    break;

                case 20:
                    str = "80901";
                    str2 = "pJ/+fmGGIEFqXrGoU3ibJ9jSrsTFfBj3bkZ92ftLl3MD8G1hGBEGLtX+td0xc/vu/vPJ5HrkdF2pCxsuFY/kFuYf5frjk9CG91rfn+p/zM7W3EPiPFzllMj1d35v0JybDb1aP8xtgD/MkZB99tKrMw1r4qQjJzDm13SU4Qz3JLsG/am8WfTrER3JqKhlJCdBxVm/wCuLzZJHc+WHMG+5lRTG0RLpMLTGvdd/30eUUqEI47lEFvr1i7AFtp8v3JQ1gvl1+kWmFpIxV1udXey6LREpqf0bglENqTCzbkj5np3/6jPkwiEIeZx61ixxjw/SPey8P6V0Xt09FeSyVSYhK5oxSSDPmE+XVfRyN4G9n2MVeyUv";
                    str3 = "XcFmbqT0bJdPcLRVwAbQwcdBTIVBAH/6ny7qtQrqzkLcKd9PD20sLmRjFq2lxM152sfWB+wiW7Xo/c9Zo31euC+1x75XwZjCcGZQZJ//iyw=";
                    break;

                case 0x15:
                    str = "40881";
                    str2 = "+B+a5osfs3pw6hzQ5WplXtICla/FJAd5yWVIshTvnPMplqgXRVMXNkeDwLOoTLORqEw09+lMngNhW0n4JmugxjhShfXP6riYWOX5J721PPglen4i4AGDPg6OKGSL4ai3LTX2jMabDElp8m1PCtQSKv7CYX1cAwi6vH56h7gg5GwNq27gGGBaYzA35MXoUXQM7BkM2dIB7/f7HgtQ3QizjVU+ety890jyE9BfBgl0Bzbx9zuUXrH+58wdKqFZOMwz9YFk0DyU0uBNyBJM4znwDzzyGG0reh2OoGq1qTD8A6RhQjAEHu0BrBWkT42Fd5mDmo2Up+5AyzicHh+4nkhdPSEUISt5ySO9rYodlF1rTGUh2ssz";
                    str3 = "+M7gDHU5rDsUjFpad19kOdiPCUcV5TKgEa5cLKmaVZGEMR4TGfrG9fnlxvNOj/zA88ls30Ff1AHrh8IGVuo/p0ErBlxTFuL9cqYbkcBJm9w=";
                    break;

                case 0x16:
                    str = "52121";
                    str2 = "xdJzezKmHncHNGjaM6mGCMRO3SIioDisI+kAeBkhFFHh77qoESyzBINVwcrpY1f4DObCGoNF74Q0BEq/a87YHG7rFr74ZPEpn03XmaM1xZaprZxSOiJz60RSMgminFwcCZZ6xjaX6BQi/iJbC0KB/lVi4MB9dJLa0iQycqF2ndWoaCkjJWjjXxb91WD/2hUJHbDR4EUKw4gNgL1a0iAnAVA25BJry6VM6GNQ67sv3xXSLF3TgosV1I/LfI5YC9iqOw9Jt7Noy2nXdwnhGKYa+GHTMXbV1Ko+KT+NMqRdica7uvzG5AqQQhNVeshI3KsO4ayQQb2RgpSyIbwWX2FerdHRZn7ZoKqouPfFedjAFzC5EIGR";
                    str3 = "oNo1DNYBERbskkJa5Z9GZeYfOOhEzUqxlXeGFLvEnTyvspMxjVNRygFJLPaSGcksiWNNxTP9tt/NYGDIBdNFttc9eAIW22cY1LfF7Tpw1VM=";
                    break;

                case 0x17:
                    str = "6538";
                    str2 = "xTqoRmmU2eLv3yHsqOHV4RR95LTzI8d5Hg9Tq/TNZyi3X90LSMfbXEx2l42W9G91c5OSdOidKQYk+3L98sa/woR1Tt9LarluNM6kv87ZqrZ7fWgSh9Fi90be4sKbvl2aYAlR8gYIA/c8I0WteVf9CNeV6CT+ZaZtG1jRaPmGX2vaKGwKrTPeOBbLgtS0EfAIgK9tT1E58h/GyZkt0+smOyoQZOh3h3ksUu4zY5TBsFPbExGVmWjkgzB8TU8G9hgoNKTY7tDfp5AXHAjjj3r9VAxe+DhuDUkWx9lWeFfQadxe+CNr6zGdIn2QN/My/9+K18Y1kQ==";
                    str3 = "k4rcSCyAK5HPqB8gZk7gewFhSM5m7I6IbtK94ytb/zxeIavA5NQtoFCV1JRKaU2a8BQWJwBUnnuoKAEfJCI6IQ==";
                    break;

                case 0x18:
                    str = "29228";
                    str2 = "cWwL/laFqkbY3283AeWpU6n0BAx7UUAHg62bWtIYF5Io+tinixtNdJo2MXpxrwMEGDXSbILjbQixwjFbMHu0XzuuB5mvEkmuYX+sNaj+cTeBuvvmtzknRFeW/bVxJaM3eot5+Xjtx5qsg6EVS4lHl+aXidpuvoNUgw02FBJ6KVnhbXXpmQEx8jxL90fj7W5GOBPztL4yEzzzj/nbH4c5TNOL5uEG1LxWAkeIrKyD3c8cLePwMFvQahxLIp5m+m7N7gUQzraQAwuhgRKouyK0tgRgnv/NDq4t0iK6zB9FtH9UPhYa7fGp9d6+874NcSZ9QT3gZkmwu2cXN96sM+tDWeZXm4cbWrtULxurNEE054X+28u3";
                    str3 = "d93oARS+MTFZJqeQVsIkFXGJK8k7G1WJXWDICC7+3LD2Cdt7qoni/Ew9qnqIHkmfW+polyF31hps3O6Mr1cix1N6vBrXaXPvj8M9M2HckbA=";
                    break;

                case 0x19:
                    str = "5683";
                    str2 = "52OUjRTt2/TbHidHKPI15jVUxMkFiyCtbIwujaxBNQAkOei4Pd0azw3f21ffNAIhpEEfr3WdrLL2WIDP5UJoS3tTworXOj8yupAn6lH6kNB9vKFwZfjfrBQ3cGwB1Uwh/slRP43XQ0fDhBNiV0W/yfkvrd2qyfGQlLH3VbgsStIdwvkwhKv3E0nCL259Q1QTxvouR8sZ42LmNWWaIK7ZAM1pVgz27RJ4/FUlxqT2BA5XhKjB/QIrGO0qvP3avfKaabkNwPff8gtvS0zDYJcix1CBziWaf3mwF55cQEu0S2JJVvZXPzF4Xdt/cFpdwchivnVBcA==";
                    str3 = "nrO/IW9kni/cEuy9yXU42SAHoS9YW0VHgMVupxqMY+VlZ9KRQcIoU+4ja8iO3s32zikqV/pSACXRdd+UU69rqg==";
                    break;

                case 0x1a:
                    str = "99465";
                    str2 = "4d6Uk4MZmEdAMUm+bvK3RiWaxBmQ6JhXzDGVbgw7tiuxcfML21PIyeOy9wqJ4ZDjMElBQevSSZ8v8xsdwpCPUH4tj4kq7gh9nFjN0T3II6J7WU01eVwnpTzCZ8JX2cLMxCRbljHaYcumCSCFr/0bO4JAfJCkISnqDWQB57Wz89bJA49hl8eN1eH/WnMKCL/GxVkiDFw1pqVEcFbtkKE4NVhtdsOKGBj5NaofU0ESJ2oC+Q8OAhBdo8CJ9qlT8MePuYXLlhCO0jdqrh7NqliGXMG6Fob5c/Vr2y680GNbVblmo0FaSwS9vjqmNWVADtTboJalmgMAlJTAAyYWAzbbF6zB4drZ1apbGPm2arKGRfblIo7e";
                    str3 = "edClX5p8R3uZaZGaAipQ4deoPz6huDaDZ1V2aMtWjPVwn8J4azoht2G07NWzRI+x5hQ/PfJyxGZaYrFLwygBdmEgm7FG/EIdahHOD+h4Ng8=";
                    break;

                case 0x1b:
                    str = "80711";
                    str2 = "PVM4ueDv4cbuFZoGMGGCJ2LzrQ/58kAIDgKT/GR663v1qZEMLbMfeCdvFxVY+2MJOyoIHkzZvsMMdQLvdL0QV2mt6jvVP5kVvio8WrTYRs6Lbk4CfuETr/S7erHhNUpQrwJS5Gw6ikpbjO9Xh5vBn1qdwbXNqVoYnD5qvk4FjnbXotm4Xzcrmqn24j5BYTWJNMkDtfiMR+43224NfHZnNDiwrFc36QRYJT9/lHLQRsQzbDB4mIu0aS8wPoFUxDyLPiJT3j6Nyt8S9VNZNTnFa+z8j/7mMOrQbx8Oyz0kvQ1MVevH5FCVQD17ibrvF7jg8sxf3vf9jeykZvLDpUXez+RYfj+7myJkRORYBdYl40zU9vZ5";
                    str3 = "PSC9oj3u/PonRm148+O3ruqfy3N4ohqdrm/YSy+D2LsRWc4jT40X+Xwl2OMYk//quQp57ePzCEGdz8QUrbGEh7XN5T9n9Eg+O1QeqrECUPY=";
                    break;

                case 0x1c:
                    str = "92604";
                    str2 = "OG3ywfsr5D8iX4dm5DrIfXyWuXFGWVrqyzDvjtMPeA+r65fdvt9D6vy3uUGiPryuexCviIfb11wMrLFQU7XU7frqmCxC+BuhaVP8iVi7pp+byIET02+D4I7G/TGDBpdS9XTQXpZjNKiR+rWuGPXcpgOXYJTPF98QGsUksnJ4bDF9PoDucdP4ugb5OeBoyzJKTGADZr1X2lMN61jVPIQD7vse1gNaN63X+74yKxQy0vQa1PKeedtFy1oFgVRr0mKWO18chC12Tj1wBz4d1xUEkxKYy/P6Uq6p3jcrXRGVWVetarUEi6sgYRzpavBQyhJAG8aCbB1RTYqtiEKVnsB9eYmDp1udua4F5MoiikBr8pqIogYz";
                    str3 = "9aDKNwmWbu6fXvz1PqUzxoVVB+ufL+q5f9ImBH1HsnMLsPxHn4ETGIPubnwj/R3/ZL9iq5dHXCD3FbO+54D+npIPcrwHafsD/iu5ch6U8XE=";
                    break;

                case 0x1d:
                    str = "2750";
                    str2 = "9cJ4V6QyQrQ6PlyKIYKxn83qvu+YgP/WHmJT+J5VQGRNOb38XG+iVmnorZFn8Y9iOjqxldk7zUiSYILy8WkYoniENRp1zPa1+wsNPLbfZL/ZhE4ovvHjvVe6PSIjr2D4q8UtEfOG0IE9deRMKHHwtpQTacpRfGuvTjk22ylbrp9osApRTNgVGwTvPIwQOP9C9EU7LgudCedrpaLi8O5fbKzGZy6H97vbZbcA0u2FAidrwDkQeu207Vr/zdz/2hMm/TcERXiuh3+OUIJqy10UKpCBmgMQuUHsGA8mORh6b03q9WJz3Pxv/XtRsbCkjYnjog0jMg==";
                    str3 = "bkqzqLKPOSG+b7NhPILTQTd/yA4c9dbcqV7EpFilTzFPHLUUheMTRZStqc3E2vFvTSVTKnax4heTiEG8+gdcTA==";
                    break;

                case 30:
                    str = "78761";
                    str2 = "31ksb5Jx6QkIRe0lohhp6e72TnNdFe0rjQb2LjBJ8pt3AwsjF1AOrQvlLRkJJx/1oDLi4I6mn/M7De+j5D26+1D9J/s1W8DZwKXCHSn82U997eng+XKDdUeuyBahOiDIgQRTIxOWNm9m081LaoHC01QywZgqR0S1Qcz05qmdIigwSy85RyE1VjjwNDc+zQNTrHvF+cMkWWY2eprhalabde3q1PFwB3Uz7e93KoFbXja+hRI3njWHzRIzvgtvCdERZWIK4wUhPlTKlLRngdls++mH80wCZzG/DioA/yJGsMOhIqOEG94BwueLM2TMAz7oURazsx+MqUYQfUarRRXGJgiKKsRvS85EEnKEGrh631FyWeX4";
                    str3 = "VXdvZxgGnXR/n+ESnYxZuJXwzGI8rqOX+iza2X52ld6q3NTjvWL1hPJ/sG3Xb3jA7Ds6MicLoIXQy51aouT4lcwBN0FK594vCQwxyfsHY5w=";
                    break;

                case 0x1f:
                    str = "91088";
                    str2 = "fJydyChf1Piu+8M5wFwkZ9r150dzkmfC0y+i/hAapu0x0/039rYo1Gwr3JM+IOUcVz0GRg3vd2UAeqzf487v4BQnHBfmqEdoFOw/ZdZSO3wES+nTEoOKe2k2iwiAjOI1JXzu3KCzOnUbX479gFAUdJZYuWbFJCK0dXcRq890QZsL/PVbCwwUtR6C0qUO9Uu5sFzRFyIbHMDrYhYeRBf6x1QBrKWkIKJL24/QHWnuN5vMKr8SfM9KzAoGogh1XagDiKZYXbNS6CfaLEhQ4wfqZV62tPULjuZ7sM/YMuODkre88DpSrsZbUq9dq96sWegna0kaxkRPIetIugLimIXKMcZ1OUcakDM4ar2jLsvPKBedL887";
                    str3 = "tKfjQzB6Jl+mNOzvHiokx81wXrwrmUrEqnpVF6lGtQoYA7pSgS9pTmS6/mFcwEqxTtg8WNcRKeePtdC1VyT6WAawMuYQfLbhCzzjdxIn2M4=";
                    break;

                case 0x20:
                    str = "28064";
                    str2 = "m2iJJOujikUje3LkXOTPaTByAzIzoT3V7pJ42hlko1igjfhacyf3raXrD07CUByHQe94dpb9tJ+E6FATKnILRJELdnQJwj12Pt2f3G26nGpjaRxUnBih2vVDJ+ng//Fs0fq2MdvxRrxMi59wksfVPW4KTrPoKuV/HshMhsk7ZD/WvlzfJ7Ndm8KuvSkzqv1i2r7wLhXP+y/cy2/1QL9Z2aRy579hPxjsi55/mM06AqU9zmZPv3KrD7mY1HdvCQCgJEi4LHXZEBUNoZLXpPZTNMSpSEE+BPcEphYnftwXphQtlLv7bhpAUBtyEg9ue4pvx5LqfJ8L8Ii+NxU6LPiGywo1Uo8HQI5cuoUnTJ6AJTSRYB9w";
                    str3 = "AKXl0hIiDoc0E7SdOl4mCPl+/x4KvFhUabOmYCiI7N9s+qjC09VUBph3V/MCxYjDi0c7EnLsa03rnqFZoK1eBrNMj2yuswg0jUJklA6nYkA=";
                    break;

                case 0x21:
                    str = "2329";
                    str2 = "DDvhADBPAxuGB+QePsVUEuRUuPNp0kopeENrnBte5s4wWBIlpZ/QadJV4ZBC8f8KiZQ4r10MOygJwmOuH8sTak6jhNPeC6dIIfePn9yQX1XTpPpLqDTcPfhJVuOdwAbe2BsQHIDNg5d17sv9TAao5cLO94K4Ihnut68/lrF5/q1YtnRgyOytdADahpjvcQ3GOFDlQT1nUozrbF8pHWumvWHFQT+otX9JD/kvgsIs9rZ0vsipaoLfMMbKxGCLn57xbMlr4YPaT9csHfY/HBw9KYTKqMvAHdawh0KWtL6l/wjFlWHSQqSEZyl+Y3ph+RKPPMM8JA==";
                    str3 = "L7dtkcjyXE0Gd66ZPz31nkMcff+CIseE5ZeRLpjxdEnqQTyrcACng7rwAs6eQUb0INI4iY3orbrv+/Q4CqbWlg==";
                    break;

                case 0x22:
                    str = "46277";
                    str2 = "f773dWi3JJuW1R/uUPy0PVC0jGnM2BlX2y0lYBgvPj8A/O14LH/ZcgDYuIxAT2vdH9thsyCjIz2XMIQj87JerGzh9WhZ8GJH9+pE6yC7gUrmdwoNzRTuLnVCw8R1XNDrf2XTiRTus1VjkXgJV714kAdStXx4UaAwd75WSmpPkFRynft50ZK3z8TX6v2SLIWbcRlcO4WqAGrB7k0oX3bqPcqQXTVLEUyXHH0+gCG1Nxa/3ETWhRV+5f11wPkV/U3bzAKyAqqtG+6LHDbXQP2TmHFasCmcYVQ1i8bMD/jpae69US/zkw4bhuytdQvU9dYevtBLeQ8GvZNUDJBUccHKEuVoWjlhGNI1aNdZ/46M7wu53nM5";
                    str3 = "kI/jds0GHqwRcv4AZ+7IXV4XGJH+LZnePxWp+oGCQ/asMm1xBtM0Ymx2s2FzXcX14IBjw+TBSG18eD/fkXPIHWNPeDA/VYgMA1UVjbjFmp8=";
                    break;

                case 0x23:
                    str = "3102";
                    str2 = "42kI8uCBKJGFpSEKpTvK7MAVAU6zCHTCUhky5Gt3wxMR2Q+LZtHLdJVhUp4BxwcewtkgS/Atf7q6Wt5ztqsZd4jt6FBMpIyEeDxkBisJmJE2c4PXwms574fC4RHT93UC2bL4/3K+/cEn1MZVchRedJH0UJ3OdYaw4wX9wCvUOEeQ8MbyIQrsKY0J70mzPilSi+i1/ByMRHQqf1FqXzqRPdKNyDGZban0fMtnHUngh4A8l4KYT2e/PMn6fzU9PudNLKaWS7atGYyjIbWL0nN9wvD9eEmxX06J6svtYrj5WDjwcDfSjC5yL8JuL1JQP1TWhTZ1lw==";
                    str3 = "53VxLBUWne47ZYXmmg8e8c/KYS0ZjtjHtVvHc6T0IYMc8rDh3rg07yvD39EanXCi2zpfsqbZXjwZSK6x9gjlzw==";
                    break;

                case 0x24:
                    str = "5936";
                    str2 = "+HewHFOfSdF0E7Aq/V4bENve3bhEIOxWhdIRoQnK3U05M2g7qR+mtcdm5F/h/brIqz88finIJ2PGuxAdxTX8jVHWRq29YISAPR+dfWak/f5lJRZO3mXbSNM0ZpENvUpeD8ogvq0sE4Q2oHE4i4xmJIwgx9Xd4DQgG82d8NG7EEPhwS/pVQoVDH+HKHvXkWqnVkEbI1CAK9AOSGhxPVA1vitq9BXPP+8Lwek5QM4nUwaPjijcgCZO7nJNcweAYHAydvWWTHx6+uXJLYPbraGx8DTRwk1R024njoMuVrPvHz1b/FpJjmIZwFWdz0a8Jk0zv2Ni8g==";
                    str3 = "W3N8wWLYQmPeR6nbEh5/OEo8mFwpXfv28o0fGlOExwNmDYgAOctALHyrwU9+hm00aWlMZ3Nvfu9IgGjwzzCYYg==";
                    break;

                case 0x25:
                    str = "51961";
                    str2 = "55qWZOYlvMdMa6r5ymFOas/yDZfq5xVTEtmpLyVQPKWeFcJQNRvQ5lIDpkCF9r285f3+X2vIVzygqFjcld39U/r5zCtL+xx7BUkmw2WODBKKuPt0JiPo3fIwivzezYaQCRUfzsSvfJV6MDGXLJ4Zx1zMWYhMvWBa4vO2+apeweMjclmpJIlEe75HKF6sIOMBMIMf3JZ4+27auid9VevM0uH4uy6liJuj3Voyy2kPv1hr6R9aQaiKE2kJjyjTzaqJV4wxwu9LG99c4AhGCwWswAb6cch2oHYkcLujPaoOlpfDx8CGn24UDHOQhVSzOgZMkBzdefP992wis3kg7WDdLM2OEKorjhbE/oBCauVLOeTP3jUO";
                    str3 = "qfWnixu3gTOTqR/sjynXfKiSMMxH0u2UBCxuyEGZP5UnkVE5xLNYbj4RiIR86D/suidKyGTo5lxgwBe/4OIzye3QRm6xeqCB4U+U7RU62AQ=";
                    break;

                case 0x26:
                    str = "6378";
                    str2 = "sBPI3GhZZMi1KqSp3Ab8azhFb4Br9qmbiz9MfQz44RO/aMqLFKv1LPDhkKLsYzPSx5/5ku7AM0gcVgFdZ/RdngPZe3L9pwkccrn2HRchUx+bcdhzlKXegCU8EV3mWIOfgrR8vLqtY73j79St3jr0yWy7LKU30cIBD/foGIIh5bdHel91mXI97460nBrtUNJ9eZA6FAskY/iWt6iIJJtu+4eiyW8upHStbZZmknxP2YpC3w2AuhqSnP4bDkD3v1c5Vbwoyu0THEERw2hJjxZIaCIJkw2zUkCsy7W2vSIdf45BLW6Gvfha8t+9R0IbZFDoNMWb9Q==";
                    str3 = "NT9Q7OQlz7iE2RacDmif4q4AfYyNcBMO74quMpKgpP/bBYKauO54YYpUH1uRUxdWWQwMl95Tfej+Uso+XxOLlA==";
                    break;

                case 0x27:
                    str = "96673";
                    str2 = "j2h5Dh2GO4zudH3+J7UhaAX6KYJw5soBe2qP9pKtPtk7PjtgBPKptFp0roO37zyEH1Jo2x6590J9FTvEbF0rAp3nOynnFIJjr5rKm9zJM7rs3aGvzYJioQV0AlY/n5yDGi1aW/mHPP+/6GwbYnh0OmvoCCBTaT2J2R13JBt3HrMBiVHt6N8Kp4ivs3CaPSwwTZcQhFM5iJ7a1p9IYMR8bK7g3JWB7OZ5U/YPPiyFli+HrOhFC4NTCfiMwQdmSj1AvPtHeUPA01hfB7ZtLaYkT/WGi6Iegrfb0M5mFHc1LFow7PjtANVJ6/G7n2/IBXhtfKy+4qWdIf/mRzOaj9pe9Khq/bklJfzb9KBsSBiPgsfP9CtC";
                    str3 = "8p9+9IWJFqBJzLjM4RrxCAv7gjmKs/VSEFvgPsSq83gUt+CNow4zMrcF4FixFRrdHiUr+BE1rL+73IMEMDUp9EKvhgCuzFPGSolwACgNQMU=";
                    break;

                case 40:
                    str = "69199";
                    str2 = "7Gm1eK2Rv9hpwPYwfLUIGokikUXzdkh4P4LswgVIA1TRx8chMB/hfgYbQnFZuGge0XXwn/usj+A3quYGwxXSHqd5PAC2jmXq3JaIV3X/Lk53wSNGZ899aeNr0EXik+h8DBo8FhEkx4DcquLthuzlBnsytuXa5P8cgHwPCkssRA+ij7gdINQxSqFCF2zrDovX75TFwnofCAqcgRNp0Z4X4P/qn2OKsqK8o5xwn8H2qIrKGzmA3CKHAB/IN9xmo0CQmNtE5FVC7Wms3BNpiEH2FRyti+W8Qx3LlIP+5sc5AVePDYMOMVeFIp/yhwaMeVQtlB1cHw384pRgLT4AXDSoD9zno9B0wvoVOseHwvBeat7OauNp";
                    str3 = "4gijTaIuX+/5sP/7Jx5+ZfhyNLjsXgm+S13IDxD5KAiPBFMM3Xn7NLxN8ZvNSJauKYCHvd3WsmkKlsO6Hd34tDH00X7lm6jk/j/VtRsnwkU=";
                    break;

                case 0x29:
                    str = "68216";
                    str2 = "8B8C5Ml/agKHcRQtk7YjqYcffsshirB3jFsXJ2jcDi+r7tbdmY1G7CjCXlYYw91714bCxt/h+AG1oSTI0+RLHRdKETWjCbo0Z1En970hHMorxfqQVh9CQPCacByTAOyREcwp7YpQ83SeOoBmcISX7gbCjJOLO36KukQIsUHrBLOpY/wbbOq0YIscokjGSgXypP56M5GW0i9I6YEnh/dG52DJnStf3wMVhbHvo1nB1dwL40ABuRFhYM2Dcl677hV4DoBtzafeSmWhTJptVBe4mXTS5Z1CZlAPTiSL1QCO5EOYDKmr5cJr0H1yfcvyg5Yt7OCUcfj8aO4lWOlWnVdJZEnWNNtmw62dd88OLo0xKH4+Fvbk";
                    str3 = "pmASrrCBqip55R4r8bCRAtp3WqPsCoe4bHEUZsX6CX2auvdKTL2q9VvVK+PHes3azvR1zCIzSEE899ClwZ+OWjBCUWxA8NVA+k8ClsiFwB0=";
                    break;

                case 0x2a:
                    str = "89160";
                    str2 = "Ez9NUWvNOIYfyvyfTKvl5XnIMidFX21amCAiVQdoVmvE3UOb6+FyrJzaSJWgqmncOwYt6H7LjJr9uB9OUYTU8fFpm8KUOq2lUeezFA5q+A1QQpU8zCTEti6swZ9a6wXRlyfRHzit5w4DdS9mgLMBsz/TH/ziW53RuPzTXQJrJFWYyFTZsrh7/tPEgvdCuwfwwYR4WmCfWNvVLbclR509ZgkYAPLwEoRD1Mmaw8lUsIrZByhc6Mb8s9h1QlcCoOZHpNDy7aYZ+PqD/jZQZzRf0JWjPLMMLVh7fX5Ps9DShttez1YCMGT0yGEEdxtjTIlT1TQ46ud8yd98IOW8cIfGtuuHUZqxPfhm8bJUarmsburAHdwz";
                    str3 = "EZkpApQTUqz0eC5BuJwiBemteiIxoPMA3W4/T/VJJ//OOslLz+pnJNiXDbtaBIjy8b028uHs2xJWKOtcinTdxM08sM7xDleYPZhDc2Iwong=";
                    break;

                case 0x2b:
                    str = "98652";
                    str2 = "uDeX1klytUyHv+dLBtGubJCureGFvfagUbbV+HRyVZn57+n9/Xrj1cHFC44A9rApt3EszRlaF5WK+EeTtEykXP/NmlO6+6aR4GlBehAFB8FvnBommceqe+Vz75ozo+Ro8c6jmZnZrlHHYh0MnKN11T9iwS5bFMgHwmvVO9Rl7zfBI566/UJWP72IYw1QLFTeFcNxJqPBd2Db4e4bt58yVz51ERCUcWOk5FrIjUG0vwrFIXsMSiaGlhJannGoNNIySZ2iAIbP/ehEvTQoOTezsbqxyhykKyg3rPERIIpuSvQXD+I6PK5JXmjWeSRtKrAtKDTI5rxZEhyXCBr2C9TXJbva5SEDQ64UK/WJDlWgTeWtFXA8";
                    str3 = "fqOAqFqKUtK4UjbWraluGKjPCQ+fLZCLESDD7BzbpfHZYaro22z09jWFdyuq0oRaiTPvOPzEmdZ7rfK3H/dhEs7dWmSMVntrEIxXq+y36pQ=";
                    break;

                case 0x2c:
                    str = "60380";
                    str2 = "m+nRZ1mMMPmTa8wilx8WZQqhUq65xnHTm1tEQb3/5DZ/UwAp6/XGNeEsP3R8Q/7qapiaPLwTFlOEeIy5HHrOPWrTNwylQP8OW08eHWApmhNvueoF1Mh0J1xxR5drn8qSC4y8rJNMMVqVgIl/EE5NCxe7BWKtL36Ay/AvN8xQb9rV63hZiZx5KP3I7dBrT870tWe265ywlUzsfhTOpF3627K5x9l4MiTI/AYAttAfHd5JG3MSIpM7azNGrJ+Ask/zQH1rd2+OIUEu3ZylKjizFYM9/8UtpTttZLloh8Op2WfDiH0cLQBG2+RV+T7o5uvVHfPXst61RnqOPPK/dt+pqIZtloTqdg3xG6eO6DXnoXwJJxC1";
                    str3 = "gijyiLM87O4DUvCUqOfrMXCTvfmmLb7UIiOfDNigsPTAA8gZ2ztX5MP9YX3c1vTEjG5o0NhD/2C83gCpRjY4UzWVanzqwTcnncvunhoIgj0=";
                    break;

                case 0x2d:
                    str = "45443";
                    str2 = "tCVFP7A1qm1OFQOdspWH6Up4a1y6Mqe7NwKvmeyftlsPCZrI/mztRpP/I85KodL0SlCHSXqyjNJOXy6yAgkFhm6+fhp9yLm/EvpeRIqOprsjM9/iGY81VR/e9GcbrgTsvWxI+sRuJeMg/1NIxfw0XrlLCpz4v2gvfuWgMYPDT7AkHTuv2J+jrWqnZHVe6O1bODRRqFDwvnfO5CH00HWMYE7+iRH83TSe7i3BL3AJxnWHYr+9dgigPX4eV3+3kx2NDEX8f8JKt5OBJpeBt2i9DUhtuDyrsyy7MQWPUVz6bTpEp1ms/nBlf00RufcVdRcf4b20J9n2hHAnIMc/xhHWli78T6nNCACZR/p/2PgUbNUABH1y";
                    str3 = "ApOWO94WgrBZfMFSvO9esrC1bmtkIbe3OZ5CigaELPy5Rdsfkg1GZgbRdxOaYmOKie1beWUZxk+QKUr+mA/l44EAeSzeXCRts6nwsHlxJvM=";
                    break;

                case 0x2e:
                    str = "87111";
                    str2 = "zwzYZhpHvRrr35MqwjnNLmbrr6hU//YfEJPBm8bd4Cn5L1FtIcRbqLNgS5k80Qsc6AoO5rSoKBMCk588mb2LVENEL5aE6/ITB8NU7lhxVsSSC9rjA9ewmEWeFhwLhd3tk4jtKczxqroNS5VqGy7oHFVKeI1ASYlEPa2H6ATfrZJG4FIsqO9WQEjZMpL6A/8cI8zODwSC+/SWuOhEavHMzu1d+01m52RcXynK5TKWE+2Fc0z8mnWxI0jOb/Jf5ErYc7lClMydHqjRpuextzE/ISx/ly3PMiAt7LWDrePFoEQ9Fw+sIpdmNgb4qmqC5LDxv43rZes7M0KwpTqJmYLfEqJQaFFUyQTC2jhmH5PLxEx4+LXZ";
                    str3 = "gXcs8d28jVTCJq2LDM5tfvXZhqT1qzCXFli5BeQJQKehcAa9QoHAyTwV80MDyGYfIPXiKulRhx0guLaaLT3b5bon1QzZbh/o+fuGNiSgrWw=";
                    break;

                case 0x2f:
                    str = "71522";
                    str2 = "dsi+sR1OlK+kta3JCXs0Fz6lGafw3F+ClU7BPzKzh6VJEC68rYobv51HeLU2u2I60FcRPmVW1jsFW/P1nRffHZxtLtOYfwY/5tdnCBjvvZAHBpR2pCXSIs1QhwdiNbHjoy8LxXoq2/WDol2aqehRJmUdhxjjk8HposUX9Z2MH4115tQsA9RiOWYhXK9No0pNehgO6R9JkkKl50bL6hgsGC6IAvJPWf3G/pK1GmLQcJmLpIRvobVIlVcJEbfnjwbfEIGKmpssOSKL0G6tu47R8DqJ/EpQgWqdNZy827eHBIpH7L+SW3K7B0p6NTDeYEzsES+RbuPBIXYGteeuOlDknsrpzkBI5mmpA4XffZ2r13EfJGxQ";
                    str3 = "nHs3FdFYryQ7GFnWevbUhsUTwtQkD5xgqdg7CGK+gDlrLQFCd3RivJHd1xNuj1mOmD06jeZiI2Egd7lGnsx/4lN77tqzWBhDG1UsLAoN78Q=";
                    break;

                case 0x30:
                    str = "18965";
                    str2 = "UIwaqhOYgjC13i6iu2v8875JESe6G9QeqbN7FR6q2lHbDuEDs88OmiKF1sEwLQ5eFuWY5s14iZrc7TnqknsXVnOuWvy+YoGG8/4GNBmyjqRkoXdWtACUebq5HTTCN+pC8eu/uAQPrWXyVRXioKmTGTLRS8bXfrs4FxXPyBbHI3eef5iUc/8Vg0n8IQf8n4Mmb6m1vc/cae+O8mLBL9yL9bVylWy10WzDX5u+BhpoMXDRXjgHkzw0GtQ7IUBXx/guNFxsggOg4eyU+tAo2BGxAS+6q9vzRZ2eiPsFivc3YOagCyjAflS8LI6CC14t6u/mnTcIKpEjTIkhjrfzY1yQbfR3wQ/sOXfpptEC8eNWlDqrfu5G";
                    str3 = "B+8ZMEebc2jcWdZRnkMD7hd2m295lAawWKzug2MDNR4aAviLdFcLFfQkYYRn989gUbreP0rgW/wMlg6lWUj3Zv6TGVNgWPOIM1PKEmc10dM=";
                    break;

                case 0x31:
                    str = "39578";
                    str2 = "eDEb8xYDdeEwVKAMWsW5gWPWGK30twkJFTMfBf+eenEtGL4nL667Iq82Axz+r5U7rA5mbiy7GEAlOCx2sAo/VAZFlSCFPGB90O8QHnvlTJvd4sGCCiDHvm53tER6ATGJYvKazCDA1gkp3fyGnscPgF99700Xvh0XMLycvRqaOFEbG+brV7CufMYw281rCgj+f1JdoEFrlsjUCqjFNLovKLGHlTpm2AtrbfxyPm0Z+F/vjQ0tKGuD2P5f3R9CW1USUsbiHZJT4/EQE6K+0G5GwEqg4iPWbENgu5FEis+VF55VCwEf8RW8nd+44l2cord5YawlB/BRmv76Z7pDUv7gcYm5zS8cVBZe2r+Vm2V2eKLjTVW5";
                    str3 = "4JZpZkPS3CU8eu4tvauNen1rKgIUBnM63+UQgPGGSeq11yWrv79iREAZFBWsMaNuu22GpXYrmBMtPgmbSbqBSg5kZ6Io9YQflsTYhqJxgp4=";
                    break;

                case 50:
                    str = "90837";
                    str2 = "uFrAUlLaQKD0s9N+hDw25PTHueVS3RVTIqmoeu2q6hs3GzKA9L+CTt1x9of0xnygLK70ZPhZzGXKypuMm7xfP/K+lo9ou5ozFGB7dKyhkCW2C5gxAHvhBL2tN8ZIppdoyS1oEninK9w2cv+h9LKc4aRlVfxV84Hpy69TiXM84lqlGa6ZHFuieDVQjsr/ZkBTFeBOaqGnkQka/IsapWccO1UQwWzOVcj6ctPyFga0wxVjkxUU5hRQQ6MJMnX8Ndz5ubrdvUm14fHf0uXrbkRBR44Nj6MJ7P3ZBhrLsiBSBQLrkU6YeC5lvRXu/OHQhHDYveNlT4/3nCJZO3q3s/HbuNFMsKXs++yFmqMaCIRVsWPjtvXX";
                    str3 = "zGGenYijdnFPAlGO7NvhQApCUAPVu65FBBywJmJ7idvW9YpujEb4K0W7FkmV7GOy6EDG+RmshZhz2arEENM18AnnmV0igYaMY0xbb/5d+0U=";
                    break;

                case 0x33:
                    str = "18102";
                    str2 = "clD1ZibPRdXFIYKp0qhQ30FsrZJEvjNnHCu6zoHgRXtLYxgCpb/ssNr1RY+xmRQplh6l/aVUuBx+DrHqbsYrnqP3TUn5efQ3OKzwF1/FRe0lvEpxY9mqIy5aH/rF+NTtKqtaElkAFdVinYp8pCWPrDsmqDypNOp+5MSDXRJY666wA086cAB0C4blyhxEbKPjB2YAfksY74t2ocmYu1P6g1LrjSTn7EmphWJRUKPwoBNm10MnO24SN+YUlU9TWPa+Ip/gTwjjbQXXTUNOvbXclXUqJ6ntKdmMZqIzcYtI5ptSMvZFJyvDCQi1bYVX8D5WdcZuCAmqcicDz1thAdfX1na4p/kiKaajSPJexf3zgBhbjbIK";
                    str3 = "ECY0gWOU8hvoPBTZ8stbHgeVjVmOJXEDrJ0G7tKgm3Nuf63NiQ59AkaQ4RV9GQSzTaaQ7EKPgxE8QrlycrqrjXV46ImrnmONj3r1WMKJkok=";
                    break;
            }

            return new string[] { str, str2, str3 };
        }
        #endregion Captchat 
    }
}
