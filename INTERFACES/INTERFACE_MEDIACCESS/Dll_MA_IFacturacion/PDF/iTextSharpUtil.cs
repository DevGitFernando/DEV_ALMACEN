using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;


using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Dll_MA_IFacturacion
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct iTextOutline
    {
        public string Title;
        public string Action;
        public string Page;
        public string Named;
        public Point Position;
    }

    internal class PDFWordIndex
    {
        public int FontSize;
        public int Height;
        public string Text;
        public int Width;
        public int X;
        public int Y;
    }

    internal static class iTextSharpUtil
    {
        #region Constructor
        static iTextSharpUtil()
        {
        }
        #endregion Constructor 

        #region Manipular PDF 
        public static bool Encriptar(string pdfFile, string User, string Password)
        {
            bool bRegresa = false;
            string sFile_Out = "";
            FileInfo fileInfo = new FileInfo(pdfFile);

            if (fileInfo.Exists)
            {
                sFile_Out = string.Format(@"{0}__encrypted.pdf", fileInfo.FullName.Replace(fileInfo.Extension, ""));
                fileInfo = null;

                PdfReader oPdfReader = new PdfReader(pdfFile);
                Document oPdfDoc = new Document();
                PdfWriter oPdfWriter = PdfWriter.GetInstance(oPdfDoc, new FileStream(sFile_Out, FileMode.Create));
                iTextSharp.text.pdf.PdfContentByte oDirectContent = null;
                iTextSharp.text.pdf.PdfImportedPage oPdfImportedPage = null;
                int iNumberOfPages = 0;
                int iPage = 0;
                int iRotation = 0;

                oPdfWriter.SetEncryption(PdfWriter.STRENGTH40BITS, User, Password, PdfWriter.AllowCopy);
                oPdfDoc.Open();
                oPdfDoc.SetPageSize(iTextSharp.text.PageSize.LEDGER.Rotate());


                oDirectContent = oPdfWriter.DirectContent;
                iNumberOfPages = oPdfReader.NumberOfPages;
                iPage = 0;

                while (iPage < iNumberOfPages)
                {
                    iPage += 1;
                    oPdfDoc.SetPageSize(oPdfReader.GetPageSizeWithRotation(iPage));
                    oPdfDoc.NewPage();

                    oPdfImportedPage = oPdfWriter.GetImportedPage(oPdfReader, iPage);
                    iRotation = oPdfReader.GetPageRotation(iPage);

                    if (iRotation == 90 || iRotation == 270)
                    {
                        oDirectContent.AddTemplate(oPdfImportedPage, 0, -1.0F, 1.0F, 0, 0, oPdfReader.GetPageSizeWithRotation(iPage).Height);
                    }
                    else
                    {
                        oDirectContent.AddTemplate(oPdfImportedPage, 1.0F, 0, 0, 1.0F, 0, 0);
                    }
                }

                oPdfDoc.Close();

                oPdfReader.Close();
                oPdfReader = null;

                File.Delete(pdfFile);
                File.Copy(sFile_Out, pdfFile);
                File.Delete(sFile_Out);

            }

            return bRegresa;
        }

        public static bool AddImageWatermark(string sourceFilePath, string watermarkPath)
        {
            bool bRegresa = false;
            string sFile_Out = "";
            FileInfo fileInfo = new FileInfo(sourceFilePath);

            if (fileInfo.Exists)
            {
                try
                {
                    sFile_Out = string.Format(@"{0}__ImageWatermark.pdf", fileInfo.FullName.Replace(fileInfo.Extension, ""));
                    fileInfo = null;

                    byte[] _byte = File.ReadAllBytes(sourceFilePath);

                    PdfReader reader = new PdfReader(_byte);
                    PdfStamper pdfStamper = new PdfStamper(reader,
                        new FileStream(sFile_Out, FileMode.Create));
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(watermarkPath);
                    PdfContentByte content;

                    img.SetAbsolutePosition(25, 25);
                    img.RotationDegrees = 45f;

                    for (int i = 0; i < reader.NumberOfPages; i++)
                    {
                        //iTextSharp.text.Rectangle rect = reader.GetPageSize(i);
                        //img.SetAbsolutePosition(rect.Width / 2, rect.Height / 2);

                        content = pdfStamper.GetOverContent(i + 1);
                        content.AddImage(img);
                    }
                    pdfStamper.Close();

                    reader.Close();
                    reader = null;

                    File.Delete(sourceFilePath);
                    File.Copy(sFile_Out, sourceFilePath);
                    File.Delete(sFile_Out);

                    bRegresa = true;
                    
                }
                catch (Exception ex)
                {
                    ////WriteLog.Log(ex.ToString());
                    ////throw ex;
                }
            }

            return bRegresa;
        }

        public static bool AddTextWatermark(string sourceFilePath, string watermarkText)
        {
            return AddTextWatermark(sourceFilePath, watermarkText, true); 
        }

        //// Add watermark using a PDF file
        public static bool AddTextWatermark(string sourceFilePath, string watermarkText, bool SetWaterMark)
        {
            bool bRegresa = false;
            string sFile_Out = "";
            float fOpacity = 0.45f;
            float iPosX = 0;
            float iPosY = 0; 
            FileInfo fileInfo = new FileInfo(sourceFilePath);

            if (fileInfo.Exists)
            {
                //// Validar que existe marca de agua a incrustar 
                SetWaterMark = watermarkText == "" ? false : SetWaterMark;


                try
                {
                    sFile_Out = string.Format(@"{0}__TextWatermark.pdf", fileInfo.FullName.Replace(fileInfo.Extension, ""));
                    fileInfo = null;

                    //// Creating watermark on a separate layer
                    //// Creating iTextSharp.text.pdf.PdfReader object to read the Existing PDF Document produced by 1 no.
                    PdfReader reader1 = new PdfReader(sourceFilePath);
                    using (FileStream fs = new FileStream(sFile_Out, FileMode.Create, FileAccess.Write, FileShare.None))

                    //// Creating iTextSharp.text.pdf.PdfStamper object to write Data from iTextSharp.text.pdf.PdfReader object to FileStream object
                    using (PdfStamper stamper = new PdfStamper(reader1, fs))
                    {
                        //// Getting total number of pages of the Existing Document
                        int pageCount = reader1.NumberOfPages;

                        //// Create New Layer for Watermark
                        PdfLayer layer = new PdfLayer("WatermarkLayer", stamper.Writer);
                        //// Loop through each Page
                        for (int i = 1; i <= pageCount; i++)
                        {
                            //// Getting the Page Size
                            iTextSharp.text.Rectangle rect = reader1.GetPageSize(i); 

                            //// Get the ContentByte object 
                            PdfContentByte cb = stamper.GetOverContent(i);

                            //// Tell the cb that the next commands should be "bound" to this new layer
                            cb.BeginLayer(layer); 

                            if (SetWaterMark)
                            {

                                PdfGState gState = new PdfGState();
                                gState.FillOpacity = fOpacity; // 0.25f;
                                //gState.FillOpacity = 0.89f;
                                cb.SetGState(gState);

                                cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 150);
                                cb.SetColorFill(BaseColor.BLACK);
                                cb.SetColorFill(BaseColor.RED);


                                iPosX = (rect.Width / 100);
                                iPosY = (rect.Height / 100);

                                iPosX = iPosX * 50;
                                iPosY = iPosY * 2; 


                                cb.BeginText();

                                    //cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 8);                                    
                                    //cb.ShowTextAligned(PdfContentByte.TEXT_RENDER_MODE_FILL, watermarkText, iPosX, iPosY, 0); //Bottom 

                                cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 60);                                    
                                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, watermarkText, rect.Width / 2, rect.Height / 2, 45f);

                                  
                                //cb.ShowTextAligned(PdfContentByte.TEXT_RENDER_MODE_FILL, watermarkText, rect.Width - 2, (rect.Height / 2), 90f); //Right
                                //cb.ShowTextAligned(PdfContentByte.TEXT_RENDER_MODE_FILL, watermarkText, 25, (rect.Height / 2), 90f); //Left

                                //cb.ShowTextAligned(PdfContentByte.TEXT_RENDER_MODE_FILL, watermarkText, (rect.Width / 2), (int)(rect.Height - 30), 0); // Top 
                                //cb.ShowTextAligned(PdfContentByte.TEXT_RENDER_MODE_FILL, watermarkText, (rect.Width / 2), 10, 0); //Bottom 


                                cb.EndText();
                            }

                            //// Close the layer
                            cb.EndLayer();
                        }
                    }

                    reader1.Close();
                    reader1 = null;

                    File.Delete(sourceFilePath);
                    File.Copy(sFile_Out, sourceFilePath);
                    File.Delete(sFile_Out);

                    bRegresa = true;
                }
                catch (Exception ex)
                {
                    bRegresa = false; 
                }
            }

            return bRegresa;
        }
        #endregion Manipular PDF 
    }
}

