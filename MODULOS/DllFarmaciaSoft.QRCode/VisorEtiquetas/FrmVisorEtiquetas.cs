using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Neodynamic.SDK.Printing;

namespace DllFarmaciaSoft.QRCode.VisorEtiquetas
{
    public partial class FrmVisorEtiquetas : Form
    {

        PrintDialog printDialog;

        int _copies = 1;
        double _dpi = 96;
        int _currentDemoIndex = -1;

        ThermalLabel _currentThermalLabel = null;
        ImageSettings _imgSettings = new ImageSettings();

        PrinterSettings _printerSettings = new PrinterSettings();
        PrintOrientation _printOrientation = PrintOrientation.Portrait;

        public FrmVisorEtiquetas():this(null) 
        { 
        }

        public FrmVisorEtiquetas(ThermalLabel Etiqueta)
        {
            InitializeComponent();
            this.Icon = SC_SolutionsSystem.General.IconoSistema; 

            _currentThermalLabel = Etiqueta; 
        }

        #region Form  
        private void FrmVisorEtiquetas_Load(object sender, EventArgs e)
        {
            this.cboDpi.SelectedIndex = 0;
        }

        private void FrmVisorEtiquetas_Shown(object sender, EventArgs e)
        {
            VisualizarEtiqueta();
        }
        #endregion Form
        
        #region Botones
        private void cboDpi_Click(object sender, EventArgs e)
        {
            double tmpDPI = 96;

            if (cboDpi.SelectedItem.ToString() != "Pantalla")
            {
                tmpDPI = double.Parse(cboDpi.SelectedItem.ToString());
            }

            if (tmpDPI != _dpi)
            {
                _dpi = tmpDPI;
                this.VisualizarEtiqueta();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.VisualizarEtiqueta();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Display Print Job dialog...           
            PrintDialog printDialog = new PrintDialog();
            printDialog.UseEXDialog = true; 

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                _printerSettings.PrinterName = printDialog.PrinterSettings.PrinterName;
                _printerSettings.ProgrammingLanguage = ProgrammingLanguage.ZPL; 

                //create a PrintJob object
                using (PrintJob pj = new PrintJob(_printerSettings))
                {
                    pj.Copies = printDialog.PrinterSettings.Copies; // set copies
                    pj.PrintOrientation = _printOrientation; //set orientation
                    pj.ThermalLabel = _currentThermalLabel; // set the ThermalLabel object
                    pj.Print(); // print the ThermalLabel object                    
                }
            }
        }

        private void btnExportToPdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Adobe PDF|*.pdf";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //create a PrintJob object
                using (PrintJob pj = new PrintJob())
                {
                    pj.ThermalLabel = _currentThermalLabel; // set the ThermalLabel object
                    pj.Copies = 1; 

                    pj.ExportToPdf(sfd.FileName, _dpi); //export to pdf
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
        }

        private void btnXmlTemplate_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML Template|*.xml";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //save ThermalLabel to XML template
                System.IO.File.WriteAllText(sfd.FileName, _currentThermalLabel.GetXmlTemplate());
               
                if (SC_SolutionsSystem.General.msjConfirmar("Plantilla XML guardada, ¿ Desea abrirla ?") == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(sfd.FileName);
                }

            }
        }

        private void btnToImagePng_Click(object sender, EventArgs e)
        {
            _imgSettings.ImageFormat = ImageFormat.Png;
            this.ExportToImage();
        }

        private void btnToImageJpeg_Click(object sender, EventArgs e)
        {
            _imgSettings.ImageFormat = ImageFormat.Jpeg;
            this.ExportToImage();
        }

        private void btnToImageTiff_Click(object sender, EventArgs e)
        {
            _imgSettings.ImageFormat = ImageFormat.Tiff;
            this.ExportToImage();
        }

        private void btnToImageGif_Click(object sender, EventArgs e)
        {
            _imgSettings.ImageFormat = ImageFormat.Gif;
            this.ExportToImage();
        }

        private void btnToImageBmp_Click(object sender, EventArgs e)
        {
            _imgSettings.ImageFormat = ImageFormat.Bmp;
            this.ExportToImage();
        }
        #endregion Botones

        #region Funciones y Procedimientos Privados 
        private void VisualizarEtiqueta()
        {
            //Display ThermalLabel as a TIFF image
            if (_currentThermalLabel != null)
            {
                try
                {
                    using (PrintJob pj = new PrintJob())
                    {
                        pj.ThermalLabel = _currentThermalLabel;
                        pj.Copies = 1;
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();

                        ImageSettings imgSett = new ImageSettings();
                        imgSett.ImageFormat = ImageFormat.Tiff;
                        //imgSett.PixelFormat = PixelFormat.BGRA32;
                        imgSett.AntiAlias = true;
                        //imgSett.TransparentBackground = true;

                        pj.ExportToImage(ms, imgSett, _dpi);

                        this.imageViewer.LoadImage(ms);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ExportToImage()
        {
            SaveFileDialog sfd = new SaveFileDialog();

            if (_imgSettings.ImageFormat == ImageFormat.Png)
                sfd.Filter = "PNG|*.png";
            else if (_imgSettings.ImageFormat == ImageFormat.Gif)
                sfd.Filter = "GIF|*.gif";
            else if (_imgSettings.ImageFormat == ImageFormat.Jpeg)
                sfd.Filter = "JPEG|*.jpg";
            else if (_imgSettings.ImageFormat == ImageFormat.Tiff)
                sfd.Filter = "TIFF|*.tif";
            else if (_imgSettings.ImageFormat == ImageFormat.Bmp)
                sfd.Filter = "BMP|*.bmp";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //create a PrintJob object
                using (PrintJob pj = new PrintJob())
                {
                    pj.ThermalLabel = _currentThermalLabel; // set the ThermalLabel object
                    pj.Copies = 1; 

                    pj.ExportToImage(sfd.FileName, _imgSettings, _dpi); //export to image file

                    //Open folder where image file was created
                    System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(sfd.FileName));

                }
            }
        }
        #endregion Funciones y Procedimientos Privados

    }
}
