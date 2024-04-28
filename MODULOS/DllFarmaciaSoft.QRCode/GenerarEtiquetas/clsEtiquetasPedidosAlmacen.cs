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

namespace DllFarmaciaSoft.QRCode.GenerarEtiquetas
{
    internal class EtiquetaPedido
    {
        string sEtiqueta = "";

        public EtiquetaPedido()
        {
        }

        public EtiquetaPedido( string Texto )
        {
            sEtiqueta = Texto;
        }

        public string Etiqueta
        {
            get { return sEtiqueta; }
            set { sEtiqueta = value; }
        }
    }

    public class clsEtiquetasPedidosAlmacen
    {
        FrmVisorEtiquetas visor;
        clsConexionSQL cnn;
        clsGrabarError Error; 

        int _copies = 1;
        double _dpi = 96;
        int _currentDemoIndex = -1;

        ThermalLabel _currentThermalLabel = null;
        ImageSettings _imgSettings = new ImageSettings();

        PrinterSettings _printerSettings = new PrinterSettings();
        PrintOrientation _printOrientation = PrintOrientation.Landscape90;

        double dCornerRadius = 0.1;

        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sNombreEmpresa = DtGeneral.EstadoConectadoNombre; 
        string sIdEstado = DtGeneral.EstadoConectado;
        string sNombreEstado = DtGeneral.EstadoConectadoNombre; 
        string sIdFarmacia = DtGeneral.FarmaciaConectada;
        string sNombreFarmacia = DtGeneral.FarmaciaConectadaNombre;

        bool bExisteImpresoraEtiquetas = DtGeneral.Impresoras.ExisteImpresora("etiquetas");
        string sImpresoraEtiquetas = DtGeneral.Impresoras.GetImpresora("etiquetas");

        int iEtiquetadoManual = 0;
        int iFolioETQ_Inicial = 0;
        int iFolioETQ_Final = 0; 


        #region Constructor y Destructor 
        public clsEtiquetasPedidosAlmacen()
        {
            visor = new FrmVisorEtiquetas();
            cnn = new clsConexionSQL(General.DatosConexion);

            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, "clsEtiquetasPedidosAlmacen");
        }

        ~clsEtiquetasPedidosAlmacen()
        {
        }
        #endregion Constructor y Destructor

        #region Propiedades 
        public bool ExisteImpresoraDeEtiquetas
        {
            get
            {
                return bExisteImpresoraEtiquetas;
            }
        }
        #endregion Propiedades 

        #region Funciones y Procedimientos Publicos 
        #region ETIQUETAS PEDIDOS 
        public bool GenerarEtiquetaSurtido(Form FormaPadre, string Folio, bool VistaPrevia)
        { 
            bool bRegresa = false;

            if(GetNumeroDeEtiquetas(FormaPadre))
            {
                bRegresa = GenerarEtiquetaSurtido(FormaPadre, sIdEmpresa, sIdEstado, sIdFarmacia, Folio, iEtiquetadoManual, iFolioETQ_Inicial, iFolioETQ_Final, VistaPrevia);
            }

            return bRegresa;
        }

        public bool GenerarEtiquetaSurtido( Form FormaPadre, string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, bool VistaPrevia )
        {
            bool bRegresa = false;

            if(GetNumeroDeEtiquetas(FormaPadre))
            {
                bRegresa = GenerarEtiqueta(FormaPadre, IdEmpresa, IdEstado, IdFarmacia, Folio, iEtiquetadoManual, iFolioETQ_Inicial, iFolioETQ_Final, VistaPrevia);
            }

            return bRegresa;
        }

        public bool GenerarEtiquetaSurtido(Form FormaPadre, string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, int EtiquetadoManual, int FolioETQ_Inicial, int FolioETQ_Final, bool VistaPrevia )
        {
            bool bRegresa = false;

            //if(GetNumeroDeEtiquetas())
            {
                bRegresa = GenerarEtiqueta(FormaPadre, IdEmpresa, IdEstado, IdFarmacia, Folio, EtiquetadoManual, FolioETQ_Inicial, FolioETQ_Final, VistaPrevia);
            }

            return bRegresa; 
        }

        private bool GetNumeroDeEtiquetas( Form FormaPadre )
        {
            bool bRegresa = false;
            FrmET_SeleccionarEtiquetas f = new FrmET_SeleccionarEtiquetas();
            
            f.ShowInTaskbar = false;
            f.ShowDialog(FormaPadre);

            if(f.GenerarEtiquetas)
            {
                bRegresa = true;
                iEtiquetadoManual = f.EtiquetadoManual;
                iFolioETQ_Inicial = f.FolioInicial;
                iFolioETQ_Final = f.FolioFinal;
            } 


            return bRegresa;  
        }

        private bool GenerarEtiqueta(Form FormaPadre, string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, int EtiquetadoManual, int FolioETQ_Inicial, int FolioETQ_Final, bool VistaPrevia)
        {
            clsLeer leerDatos = new clsLeer(ref cnn);
            bool bRegresa = false;
            string sSql = ""; 

            sSql = string.Format("Exec spp_ETQ_Informacion_EtiquetasPedidos  \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', \n" +
                "\t@EtiquetadoManual = '{4}', @FolioETQ_Inicial = '{5}', @FolioETQ_Final = '{6}' \n", 
                IdEmpresa, IdEstado, IdFarmacia, Folio, EtiquetadoManual, FolioETQ_Inicial, FolioETQ_Final); 
            if (!leerDatos.Exec(sSql))
            {
                Error.GrabarError(leerDatos, "GenerarTransferencias()"); 
            }
            else
            {
                bRegresa = leerDatos.Leer(); 
            }

            if (bRegresa)
            {
                bRegresa = GenerarEtiqueta___4x3(FormaPadre, VistaPrevia, leerDatos.DataSetClase);
            }

            return bRegresa; 
        }

        private bool GenerarEtiqueta___4x3( Form FormaPadre, bool VistaPrevia, DataSet Datos )
        {
            Neodynamic.SDK.Printing.Font fuente = new Neodynamic.SDK.Printing.Font();
            UnitType unidad = UnitType.Cm;
            clsLeer info = new clsLeer();
            bool bRegresa = false;
            bool bGenerar = true;

            DateTime dtFecha = DateTime.Now;
            string sFechaImpresion = "";

            RectangleShapeItem rectItem;
            TextItem txtEmpresa;
            LineShapeItem lineItem;
            LineShapeItem lineItem_Seccion_01;
            LineShapeItem lineItem_Seccion_00;
            LineShapeItem lineItem_Seccion_02_FolioReferencia;
            LineShapeItem lineItem_Seccion_02_FolioReferencia_Pie;
            TextItem txtET_02_Origen;
            TextItem txtET_03_Destino;
            TextItem txtET_04_FolioReferencia;
            TextItem txtET_04_FolioReferencia_Titulo;
            BarcodeItem txtET_05_Barcode;
            TextItem txtET_06_Surtio;
            TextItem txtET_06_Surtio_Leyenda;
            TextItem txtET_07_Verifico;
            TextItem txtET_07_Verifico_Leyenda;
            TextItem txtET_08_FechaEtiquetado;
            TextItem txtET_08_FechaEtiquetado_Leyenda;

            TextItem txtET_09_Antibioticos;
            TextItem txtET_10_Controlados;
            TextItem txtET_11_Refrigerados;
            TextItem txtET_12_Leyenda_PNO;

            TextItem txtET_13_EtiquetaCajas;
            TextItem txtET_14_EtiquetaCajas_Leyenda;
            TextItem txtET_15_Programa;
            TextItem txtET_15_Programa_Leyenda;
            TextItem txtET_16_Referencia;
            TextItem txtET_16_Referencia_Leyenda;
            TextItem txtET_16_ReferenciaFF;
            TextItem txtET_16_Referencia_LeyendaFF;

            LineShapeItem lineItem_Division_01;
            LineShapeItem lineItem_Division_02;
            LineShapeItem lineItem_Division_03;
            LineShapeItem lineItem_Division_04;
            LineShapeItem lineItem_Division_05;
            LineShapeItem lineItem_Division_06_Vertical;


            LineShapeItem lineItem_Division_07_NoEtiquetas;
            LineShapeItem lineItem_Division_08_NoEtiquetas;
            LineShapeItem lineItem_Division_09_Programa;
            LineShapeItem lineItem_Division_10_Referencia;
            LineShapeItem lineItem_Division_11_Referencia;
            LineShapeItem lineItem_Division_12_Referencia;
            LineShapeItem lineItem_Division_13_Referencia;


            double dFactor = 2.50;
            double dAncho = 4 * dFactor;
            double dAlto = 3 * dFactor;
            double dMargen = 0.05 * dFactor;
            double dAlto_Encabezado = 1;
            double dSeparador = 0.075 * dFactor;
            double dSeparadorDivisiones = 0.6;
            double dBorde = 0.00;
            double dBorde_Lineas = 0.05;
            double dPadding = 0.01;

            //dFactor = 1; 

            unidad = dFactor == 1 ? UnitType.Inch : UnitType.Cm;
            dAlto_Encabezado = unidad == UnitType.Inch ? dFactor * 0.25 : 1;

            dCornerRadius = 0.1 * dFactor;

            //// Descargar los datos del enbezado 
            info.DataSetClase = Datos;
            _printOrientation = PrintOrientation.Portrait;

            if(VistaPrevia)
            {
                bExisteImpresoraEtiquetas = true;
            }

            while(info.Leer() && bExisteImpresoraEtiquetas && bGenerar)
            {
                fuente.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
                fuente.Unit = unidad == UnitType.Inch ? FontUnit.Inch : FontUnit.Millimeter;
                fuente.Unit = FontUnit.Point;
                fuente.Name = "Arial";

                List<ThermalLabel> listaEtiquetas = new List<ThermalLabel>();
                bool bExistenEtiquetas = false;
                int iNumero_Etiqueta_Inicial = 0;
                int iNumeroDeEtiquetas = 0;
                int iContadorEtiquetas = 0;

                //Define a ThermalLabel object and set unit to inch and label size
                ThermalLabel tLabel = new ThermalLabel(unidad, dAncho, dAlto);
                tLabel.GapLength = 0.2;


                rectItem = new RectangleShapeItem();
                rectItem.CornerRadius.BottomLeft = dCornerRadius;
                rectItem.CornerRadius.BottomRight = dCornerRadius;
                rectItem.CornerRadius.TopLeft = dCornerRadius;
                rectItem.CornerRadius.TopRight = dCornerRadius;
                rectItem.X = dMargen;
                rectItem.Y = dMargen;
                rectItem.Height = dAlto - (dMargen * 2);
                rectItem.Width = dAncho - (dMargen * 2);
                rectItem.StrokeThickness = 0.02 * dFactor;


                ////Define a TextItem object for product name
                //TextItem txtEmpresa = new TextItem(dMargen * 2, dMargen * 2, dAncho - (dMargen * 4), 0.3, "INTERCONTINENTAL DE MEDICAMENTOS S.A. DE C.V.");

                txtEmpresa = new TextItem(); // new TextItem(dMargen * 2, dMargen * 2, dAncho - (dMargen * 4), dAlto_Encabezado, info.Campo("Empresa"));
                txtEmpresa.Font.Name = fuente.Name;
                txtEmpresa.Font.Unit = fuente.Unit;
                txtEmpresa.Text = info.Campo("Empresa");
                txtEmpresa.Font.Size = 10;
                txtEmpresa.Font.Bold = true;
                //txtEmpresa.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
                //txtEmpresa.Font.Unit = FontUnit.Point;
                txtEmpresa.TextAlignment = TextAlignment.Center;
                txtEmpresa.TextPadding = new FrameThickness(dPadding);
                txtEmpresa.Height = 0.75;
                //txtEmpresa.Width = dAncho - (dMargen * 5);
                txtEmpresa.Width = rectItem.Width - (dMargen * 2);
                txtEmpresa.X = dMargen * 2;
                txtEmpresa.Y = dMargen * 2.02;
                txtEmpresa.BorderThickness = new FrameThickness(dBorde);

                lineItem = new LineShapeItem();
                lineItem.Orientation = LineOrientation.Horizontal;
                lineItem.X = rectItem.X;
                lineItem.Y = txtEmpresa.Height + dMargen;
                lineItem.Width = rectItem.Width;
                lineItem.Height = 0.01;
                lineItem.StrokeThickness = dBorde_Lineas;

                txtET_02_Origen = new TextItem(); // dMargen * 2, dMargen * 2, dAncho - (dMargen * 4), 0.3, info.Campo("EtiquetasCajas"));
                txtET_02_Origen.Font.Name = fuente.Name;
                txtET_02_Origen.Font.Unit = fuente.Unit;
                txtET_02_Origen.Font.Size = 8;
                txtET_02_Origen.Font.Bold = true;
                //txtET_02_Origen.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
                //txtET_02_Origen.Font.Unit = FontUnit.Point;
                txtET_02_Origen.TextAlignment = TextAlignment.Center;
                txtET_02_Origen.TextPadding = new FrameThickness(dPadding);
                txtET_02_Origen.Text = info.Campo("Farmacia");
                txtET_02_Origen.Height = dMargen * 5;
                //txtET_02_Origen.Width = dAncho - (dMargen * 5);
                txtET_02_Origen.Width = rectItem.Width - (dMargen * 2);
                txtET_02_Origen.X = txtEmpresa.X; // dMargen * 2;
                txtET_02_Origen.Y = txtEmpresa.Y + txtEmpresa.Height;
                txtET_02_Origen.BorderThickness = new FrameThickness(dBorde);
                txtET_02_Origen.Width = txtEmpresa.Width;

                txtET_03_Destino = new TextItem(); // dMargen * 2, dMargen * 2, dAncho - (dMargen * 4), 0.3, info.Campo("EtiquetasCajas"));
                txtET_03_Destino.Font.Name = fuente.Name;
                txtET_03_Destino.Font.Unit = fuente.Unit;
                txtET_03_Destino.Font.Size = 8;
                txtET_03_Destino.Font.Bold = true;
                //txtET_03_Destino.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
                //txtET_03_Destino.Font.Unit = FontUnit.Point;
                txtET_03_Destino.TextAlignment = TextAlignment.Center;
                txtET_03_Destino.TextPadding = new FrameThickness(dPadding);
                txtET_03_Destino.Text = info.Campo("FarmaciaPedido");
                txtET_03_Destino.Height = dMargen * 4;
                //txtET_03_Destino.Width = dAncho - (dMargen * 5);
                txtET_03_Destino.Width = rectItem.Width;
                txtET_03_Destino.X = txtEmpresa.X; // dMargen * 2;
                txtET_03_Destino.Y = txtET_02_Origen.Y + txtET_02_Origen.Height + .05;
                txtET_03_Destino.BorderThickness = new FrameThickness(dBorde);
                txtET_03_Destino.Width = txtEmpresa.Width;


                lineItem_Seccion_01 = new LineShapeItem();
                lineItem_Seccion_01.Orientation = LineOrientation.Horizontal;
                lineItem_Seccion_01.X = rectItem.X;
                lineItem_Seccion_01.Y = txtET_03_Destino.Y + txtET_03_Destino.Height + dMargen;
                lineItem_Seccion_01.Width = rectItem.Width;
                lineItem_Seccion_01.Height = 0.01;
                lineItem_Seccion_01.StrokeThickness = dBorde_Lineas;


                lineItem_Seccion_00 = new LineShapeItem();
                lineItem_Seccion_00.Orientation = LineOrientation.Horizontal;
                lineItem_Seccion_00.X = lineItem_Seccion_01.X;
                lineItem_Seccion_00.Y = txtET_03_Destino.Y - (dMargen * .0); 
                lineItem_Seccion_00.Width = lineItem_Seccion_01.Width;
                lineItem_Seccion_00.Height = lineItem_Seccion_01.Height;
                lineItem_Seccion_00.StrokeThickness = dBorde_Lineas;



                lineItem_Seccion_02_FolioReferencia = new LineShapeItem();
                lineItem_Seccion_02_FolioReferencia.Orientation = LineOrientation.Vertical;
                lineItem_Seccion_02_FolioReferencia.X = rectItem.Width - 3;
                lineItem_Seccion_02_FolioReferencia.Y = lineItem_Seccion_01.Y;
                lineItem_Seccion_02_FolioReferencia.Width = 0.01; // rectItem.Width;
                lineItem_Seccion_02_FolioReferencia.Height = (rectItem.Height - lineItem_Seccion_02_FolioReferencia.Y) + dMargen;
                lineItem_Seccion_02_FolioReferencia.StrokeThickness = 0.02 * dFactor;



                txtET_04_FolioReferencia_Titulo = new TextItem();
                txtET_04_FolioReferencia_Titulo.Font.Name = fuente.Name;
                txtET_04_FolioReferencia_Titulo.Font.Unit = fuente.Unit;
                txtET_04_FolioReferencia_Titulo.Font.Size = 8;
                txtET_04_FolioReferencia_Titulo.Font.Bold = true;
                txtET_04_FolioReferencia_Titulo.RotationAngle = 90;
                //txtET_02_Origen.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
                //txtET_02_Origen.Font.Unit = FontUnit.Point;
                txtET_04_FolioReferencia_Titulo.TextAlignment = TextAlignment.Center;
                txtET_04_FolioReferencia_Titulo.TextPadding = new FrameThickness(dPadding);
                txtET_04_FolioReferencia_Titulo.Text = info.Campo("TituloReferencia");
                txtET_04_FolioReferencia_Titulo.Height = dMargen * 4;
                txtET_04_FolioReferencia_Titulo.X = rectItem.Width - (dMargen * 3.5); // dMargen * 2;
                txtET_04_FolioReferencia_Titulo.Y = lineItem_Seccion_02_FolioReferencia.Y + (lineItem_Seccion_02_FolioReferencia.Width * 2);
                txtET_04_FolioReferencia_Titulo.BorderThickness = new FrameThickness(0);
                //txtET_04_FolioReferencia_Titulo.Width = lineItem_Seccion_02_FolioReferencia.Height - (lineItem_Seccion_02_FolioReferencia.Width * 6);
                txtET_04_FolioReferencia_Titulo.Width = lineItem_Seccion_02_FolioReferencia.Height - (dMargen * 1.2);
                //txtET_04_FolioReferencia_Titulo.Text = txtET_04_FolioReferencia_Titulo.Width.ToString();


                txtET_04_FolioReferencia = new TextItem();
                txtET_04_FolioReferencia.Font.Name = fuente.Name;
                txtET_04_FolioReferencia.Font.Unit = fuente.Unit;
                txtET_04_FolioReferencia.Font.Size = 7;
                txtET_04_FolioReferencia.Font.Bold = true;
                txtET_04_FolioReferencia.RotationAngle = 90;
                //txtET_02_Origen.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
                //txtET_02_Origen.Font.Unit = FontUnit.Point;
                txtET_04_FolioReferencia.TextAlignment = TextAlignment.Center;
                txtET_04_FolioReferencia.TextPadding = new FrameThickness(dPadding);
                txtET_04_FolioReferencia.Text = info.Campo("FolioReferencia");
                txtET_04_FolioReferencia.Height = dMargen * 4;
                txtET_04_FolioReferencia.X = (txtET_04_FolioReferencia_Titulo.X * 1) - ((txtET_04_FolioReferencia_Titulo.Height * 4) + (dMargen / 2)); // dMargen * 2;
                txtET_04_FolioReferencia.Y = txtET_04_FolioReferencia_Titulo.Y;
                txtET_04_FolioReferencia.BorderThickness = new FrameThickness(.03);
                txtET_04_FolioReferencia.Width = txtET_04_FolioReferencia_Titulo.Width;
                //txtET_04_FolioReferencia.Text = txtET_04_FolioReferencia.Width.ToString();


                txtET_05_Barcode = new BarcodeItem();
                txtET_05_Barcode.Symbology = BarcodeSymbology.Code128;
                txtET_05_Barcode.RotationAngle = 90;
                txtET_05_Barcode.Font.Name = fuente.Name;
                txtET_05_Barcode.Font.Unit = fuente.Unit;
                txtET_05_Barcode.Font.Size = 7.5;
                txtET_05_Barcode.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
                //disable checksum
                txtET_05_Barcode.AddChecksum = false;
                //hide human readable text
                txtET_05_Barcode.DisplayCode = true;
                //set border
                txtET_05_Barcode.BorderThickness = new FrameThickness(0.01);
                //align barcode
                txtET_05_Barcode.BarcodeAlignment = BarcodeAlignment.MiddleCenter;
                //txtET_05_Barcode.CodeAlignment = BarcodeTextAlignment.BelowJustify;
                //txtET_05_Barcode.TextAlignment = BarcodeTextAlignment.BelowJustify; 
                txtET_05_Barcode.Sizing = BarcodeSizing.Fill;

                //txtET_05_Barcode.Text = info.Campo("FolioReferencia");
                //txtET_05_Barcode.Code = info.Campo("Folio");

                //txtET_05_Barcode.Text = info.Campo("Folio");
                txtET_05_Barcode.Code = info.Campo("FolioReferencia");


                //txtET_05_Barcode.Sizing = BarcodeSizing.Fill; 


                //txtET_05_Barcode.Height = dMargen * 4;
                txtET_05_Barcode.Width = txtET_04_FolioReferencia.Width;
                txtET_05_Barcode.Height = txtET_04_FolioReferencia_Titulo.Height * 4;
                txtET_05_Barcode.X = (txtET_04_FolioReferencia_Titulo.X * 1) - ((txtET_04_FolioReferencia_Titulo.Height * 4) + (dMargen / 2)); // dMargen * 2;
                txtET_05_Barcode.Y = txtET_04_FolioReferencia.Y;

                //Set bars height to .3inch
                txtET_05_Barcode.BarHeight = txtET_05_Barcode.Height * .40;
                //Set bars width to 0.02inch
                txtET_05_Barcode.BarWidth = 0.04;


                lineItem_Seccion_02_FolioReferencia.X = txtET_05_Barcode.X - (dMargen / 2);
                lineItem_Seccion_02_FolioReferencia_Pie = new LineShapeItem();
                lineItem_Seccion_02_FolioReferencia_Pie.Orientation = LineOrientation.Vertical;
                lineItem_Seccion_02_FolioReferencia_Pie.Width = 0.01; // rectItem.Width;
                lineItem_Seccion_02_FolioReferencia_Pie.Height = lineItem_Seccion_02_FolioReferencia.Height; // (rectItem.Height - lineItem_Seccion_02_FolioReferencia.Y) + dMargen;
                lineItem_Seccion_02_FolioReferencia_Pie.StrokeThickness = dBorde_Lineas;
                lineItem_Seccion_02_FolioReferencia_Pie.X = lineItem_Seccion_02_FolioReferencia.X + txtET_05_Barcode.Height + dMargen;
                lineItem_Seccion_02_FolioReferencia_Pie.Y = lineItem_Seccion_01.Y;


                lineItem_Division_01 = new LineShapeItem();
                lineItem_Division_01.Orientation = LineOrientation.Vertical;
                lineItem_Division_01.Width = 0.01; // rectItem.Width;
                lineItem_Division_01.Height = lineItem_Seccion_02_FolioReferencia_Pie.Height + 0;
                lineItem_Division_01.Y = lineItem_Seccion_02_FolioReferencia_Pie.Y;
                lineItem_Division_01.X = lineItem_Seccion_02_FolioReferencia.X - (1.5); // -(dMargen / 2);


                lineItem_Division_02 = new LineShapeItem();
                lineItem_Division_02.Orientation = LineOrientation.Horizontal;
                lineItem_Division_02.Width = rectItem.Width;
                lineItem_Division_02.Width = lineItem_Seccion_02_FolioReferencia.X - dMargen;
                //lineItem_Division_02.Width = lineItem_Division_01.X - dMargen; 
                lineItem_Division_02.Height = 0.01; // lineItem_Seccion_02_FolioReferencia_Pie.Height;
                lineItem_Division_02.X = (dMargen);
                lineItem_Division_02.Y = 5.15;


                lineItem_Division_03 = new LineShapeItem();
                lineItem_Division_03.Orientation = LineOrientation.Horizontal;
                lineItem_Division_03.Width = rectItem.Width;
                lineItem_Division_03.Width = lineItem_Division_02.Width - 1.5;
                //lineItem_Division_03.Width = lineItem_Division_01.X - dMargen; 
                lineItem_Division_03.Height = 0.01; // lineItem_Seccion_02_FolioReferencia_Pie.Height;
                lineItem_Division_03.X = (dMargen);
                lineItem_Division_03.Y = lineItem_Division_02.Y + dSeparadorDivisiones;

                lineItem_Division_04 = new LineShapeItem();
                lineItem_Division_04.Orientation = LineOrientation.Horizontal;
                lineItem_Division_04.Width = lineItem_Division_03.Width;
                lineItem_Division_04.Height = 0.01; // lineItem_Seccion_02_FolioReferencia_Pie.Height;
                lineItem_Division_04.X = (dMargen);
                lineItem_Division_04.Y = lineItem_Division_03.Y + dSeparadorDivisiones;

                lineItem_Division_05 = new LineShapeItem();
                lineItem_Division_05.Orientation = LineOrientation.Horizontal;
                lineItem_Division_05.Width = lineItem_Division_03.Width;
                lineItem_Division_05.Height = 0.01; // lineItem_Seccion_02_FolioReferencia_Pie.Height;
                lineItem_Division_05.X = (dMargen);
                lineItem_Division_05.Y = lineItem_Division_04.Y + dSeparadorDivisiones;


                lineItem_Division_06_Vertical = new LineShapeItem();
                lineItem_Division_06_Vertical.Orientation = LineOrientation.Vertical;
                lineItem_Division_06_Vertical.Y = lineItem_Division_02.Y;
                lineItem_Division_06_Vertical.X = (dMargen * 2) + 2.7;
                lineItem_Division_06_Vertical.X = lineItem_Division_06_Vertical.X * .80;
                lineItem_Division_06_Vertical.X = 1.75; 
                lineItem_Division_06_Vertical.Width = 0.01;
                lineItem_Division_06_Vertical.Height = (rectItem.Height - lineItem_Division_02.Y) + dMargen;
                //lineItem_Division_06_Vertical.Height = lineItem_Division_06_Vertical.Height * .80;


                //////////////////////////////////////////////////////////////////////////////////  
                txtET_06_Surtio = new TextItem();
                txtET_06_Surtio.Font.Name = fuente.Name;
                txtET_06_Surtio.Font.Unit = fuente.Unit;
                txtET_06_Surtio.Font.Size = 8;
                txtET_06_Surtio.Font.Bold = true;
                txtET_06_Surtio.TextAlignment = TextAlignment.Left;
                txtET_06_Surtio.TextPadding = new FrameThickness(dPadding);
                txtET_06_Surtio.Text = info.Campo("TituloSurtio");
                txtET_06_Surtio.Width = lineItem_Division_06_Vertical.X - (dMargen * 2);
                txtET_06_Surtio.Height = dMargen * 4;
                txtET_06_Surtio.X = dMargen * 1.75;
                txtET_06_Surtio.Y = lineItem_Division_02.Y + (lineItem_Division_02.Height + (dMargen * .70));
                txtET_06_Surtio.Y = 5.2;
                txtET_06_Surtio.BorderThickness = new FrameThickness(.00);

                txtET_06_Surtio_Leyenda = new TextItem();
                txtET_06_Surtio_Leyenda.Font.Name = fuente.Name;
                txtET_06_Surtio_Leyenda.Font.Unit = fuente.Unit;
                txtET_06_Surtio_Leyenda.Font.Size = 6;
                txtET_06_Surtio_Leyenda.Font.Bold = true;
                txtET_06_Surtio_Leyenda.TextAlignment = TextAlignment.Left;
                txtET_06_Surtio_Leyenda.TextPadding = new FrameThickness(dPadding);
                txtET_06_Surtio_Leyenda.Width = lineItem_Division_01.X - (lineItem_Division_06_Vertical.X + (dMargen * 0.8));
                txtET_06_Surtio_Leyenda.Height = txtET_06_Surtio.Height;
                txtET_06_Surtio_Leyenda.X = lineItem_Division_06_Vertical.X + (dMargen * .5);
                txtET_06_Surtio_Leyenda.Y = txtET_06_Surtio.Y;
                txtET_06_Surtio_Leyenda.BorderThickness = new FrameThickness(.00);
                txtET_06_Surtio_Leyenda.Text = info.Campo("LeyendaSurtio");

                //////////////////////////////////////////////////////////////////////////////////  


                //////////////////////////////////////////////////////////////////////////////////  
                txtET_07_Verifico = new TextItem();
                txtET_07_Verifico.Font.Name = fuente.Name;
                txtET_07_Verifico.Font.Unit = fuente.Unit;
                txtET_07_Verifico.Font.Size = 8;
                txtET_07_Verifico.Font.Bold = true;
                txtET_07_Verifico.TextAlignment = TextAlignment.Left;
                txtET_07_Verifico.TextPadding = new FrameThickness(dPadding);
                txtET_07_Verifico.Text = info.Campo("TituloValido");
                txtET_07_Verifico.Width = txtET_06_Surtio.Width;
                ;
                txtET_07_Verifico.Height = txtET_06_Surtio.Height;
                txtET_07_Verifico.X = txtET_06_Surtio.X;
                txtET_07_Verifico.Y = txtET_06_Surtio.Y + txtET_06_Surtio.Height + 0.1; // (lineItem_Division_03.Height + (dMargen * .70));
                txtET_07_Verifico.BorderThickness = new FrameThickness(.00);

                txtET_07_Verifico_Leyenda = new TextItem();
                txtET_07_Verifico_Leyenda.Font.Name = fuente.Name;
                txtET_07_Verifico_Leyenda.Font.Unit = fuente.Unit;
                txtET_07_Verifico_Leyenda.Font.Size = 6;
                txtET_07_Verifico_Leyenda.Font.Bold = true;
                txtET_07_Verifico_Leyenda.TextAlignment = TextAlignment.Left;
                txtET_07_Verifico_Leyenda.TextPadding = new FrameThickness(dPadding);
                txtET_07_Verifico_Leyenda.Text = info.Campo("LeyendaValido");
                txtET_07_Verifico_Leyenda.Width = txtET_06_Surtio_Leyenda.Width;
                txtET_07_Verifico_Leyenda.Height = txtET_06_Surtio_Leyenda.Height;
                txtET_07_Verifico_Leyenda.X = txtET_06_Surtio_Leyenda.X;
                txtET_07_Verifico_Leyenda.Y = txtET_07_Verifico.Y;
                ;
                txtET_07_Verifico_Leyenda.BorderThickness = new FrameThickness(.00);
                //////////////////////////////////////////////////////////////////////////////////  


                //////////////////////////////////////////////////////////////////////////////////  
                txtET_08_FechaEtiquetado = new TextItem();
                txtET_08_FechaEtiquetado.Font.Name = fuente.Name;
                txtET_08_FechaEtiquetado.Font.Unit = fuente.Unit;
                txtET_08_FechaEtiquetado.Font.Size = 7;
                txtET_08_FechaEtiquetado.Font.Bold = true;
                txtET_08_FechaEtiquetado.TextAlignment = TextAlignment.Left;
                txtET_08_FechaEtiquetado.TextPadding = new FrameThickness(dPadding);
                txtET_08_FechaEtiquetado.Text = info.Campo("TituloFecha");
                txtET_08_FechaEtiquetado.Width = txtET_06_Surtio.Width;
                ;
                txtET_08_FechaEtiquetado.Height = txtET_06_Surtio.Height + 0;
                txtET_08_FechaEtiquetado.X = txtET_06_Surtio.X;
                txtET_08_FechaEtiquetado.Y = lineItem_Division_04.Y + (lineItem_Division_04.Height + (dMargen * .1));
                txtET_08_FechaEtiquetado.BorderThickness = new FrameThickness(.00);


                txtET_08_FechaEtiquetado_Leyenda = new TextItem();
                txtET_08_FechaEtiquetado_Leyenda.Font.Name = fuente.Name;
                txtET_08_FechaEtiquetado_Leyenda.Font.Unit = fuente.Unit;
                txtET_08_FechaEtiquetado_Leyenda.Font.Size = 6;
                txtET_08_FechaEtiquetado_Leyenda.Font.Bold = true;
                txtET_08_FechaEtiquetado_Leyenda.TextAlignment = TextAlignment.Center;
                txtET_08_FechaEtiquetado_Leyenda.TextPadding = new FrameThickness(dPadding);
                txtET_08_FechaEtiquetado_Leyenda.Width = txtET_06_Surtio_Leyenda.Width; // lineItem_Division_01.X - (lineItem_Division_06_Vertical.X + (dMargen * 2));
                txtET_08_FechaEtiquetado_Leyenda.Height = txtET_08_FechaEtiquetado.Height;
                txtET_08_FechaEtiquetado_Leyenda.X = txtET_06_Surtio_Leyenda.X; // lineItem_Division_06_Vertical.X + (dMargen * 1);
                txtET_08_FechaEtiquetado_Leyenda.Y = txtET_08_FechaEtiquetado.Y;
                txtET_08_FechaEtiquetado_Leyenda.BorderThickness = new FrameThickness(.00);
                txtET_08_FechaEtiquetado_Leyenda.Text = DateTime.Now.ToString();

                dtFecha = info.CampoFecha("FechaImpresion");
                sFechaImpresion = string.Format("{0}, {1} de {2} del {3}  {4}:{5}",
                    General.FechaNombreDia(dtFecha), dtFecha.Day, General.FechaNombreMes(dtFecha), dtFecha.Year,
                    General.Fg.PonCeros(dtFecha.Hour, 2), General.Fg.PonCeros(dtFecha.Minute, 2)
                    );
                txtET_08_FechaEtiquetado_Leyenda.Text = sFechaImpresion;
                //////////////////////////////////////////////////////////////////////////////////  


                txtET_09_Antibioticos = new TextItem();
                txtET_09_Antibioticos.Text = info.Campo("LeyendaAntibioticos");
                txtET_09_Antibioticos.Font.Name = fuente.Name;
                txtET_09_Antibioticos.Font.Unit = fuente.Unit;
                txtET_09_Antibioticos.Font.Size = 6;
                txtET_09_Antibioticos.Font.Bold = true;
                txtET_09_Antibioticos.RotationAngle = 90;
                txtET_09_Antibioticos.Font.Underline = info.CampoBool("ContieneAntibioticos");
                txtET_09_Antibioticos.TextAlignment = TextAlignment.Left;
                txtET_09_Antibioticos.TextPadding = new FrameThickness(dPadding);
                txtET_09_Antibioticos.BorderThickness = new FrameThickness(.00);
                txtET_09_Antibioticos.Y = lineItem_Division_02.Y + ( dMargen *.5); // + (txtET_06_Surtio.Height );
                //txtET_09_Leyenda_PNO.X = 10;
                //txtET_09_Leyenda_PNO.X = lineItem_Division_06_Vertical.X;
                txtET_09_Antibioticos.X = lineItem_Division_01.X + (dMargen * .5);
                txtET_09_Antibioticos.Height = txtET_08_FechaEtiquetado_Leyenda.Height;
                txtET_09_Antibioticos.Width = lineItem_Seccion_02_FolioReferencia.X - (lineItem_Division_01.X + (dMargen * 2));

                txtET_09_Antibioticos.Height = .35;
                txtET_09_Antibioticos.Width = rectItem.Height -  (lineItem_Division_01.Y + .5);


                txtET_10_Controlados = new TextItem();
                txtET_10_Controlados.Text = info.Campo("LeyendaControlados");
                txtET_10_Controlados.Font.Name = fuente.Name;
                txtET_10_Controlados.Font.Unit = fuente.Unit;
                txtET_10_Controlados.Font.Size = 5;
                txtET_10_Controlados.Font.Bold = true;
                txtET_10_Controlados.RotationAngle = 90;
                txtET_10_Controlados.Font.Underline = info.CampoBool("ContieneControlados");
                txtET_10_Controlados.TextAlignment = TextAlignment.Left;
                txtET_10_Controlados.TextPadding = new FrameThickness(dPadding);
                txtET_10_Controlados.BorderThickness = new FrameThickness(.00);
                txtET_10_Controlados.Y = txtET_09_Antibioticos.Y;
                //txtET_09_Leyenda_PNO.X = 10;
                //txtET_09_Leyenda_PNO.X = lineItem_Division_06_Vertical.X;
                txtET_10_Controlados.X = txtET_09_Antibioticos.X + txtET_09_Antibioticos.Height;
                txtET_10_Controlados.Height = txtET_09_Antibioticos.Height;
                txtET_10_Controlados.Width = txtET_09_Antibioticos.Width;

                txtET_11_Refrigerados = new TextItem();
                txtET_11_Refrigerados.Text = info.Campo("LeyendaRefrigerados");
                txtET_11_Refrigerados.Font.Name = fuente.Name;
                txtET_11_Refrigerados.Font.Unit = fuente.Unit;
                txtET_11_Refrigerados.Font.Size = 5;
                txtET_11_Refrigerados.Font.Bold = true;
                txtET_11_Refrigerados.RotationAngle = 90;
                txtET_11_Refrigerados.Font.Underline = info.CampoBool("ContieneRefrigerados");
                txtET_11_Refrigerados.TextAlignment = TextAlignment.Left;
                txtET_11_Refrigerados.TextPadding = new FrameThickness(dPadding);
                txtET_11_Refrigerados.BorderThickness = new FrameThickness(.00);
                txtET_11_Refrigerados.Y = txtET_10_Controlados.Y;
                //txtET_09_Leyenda_PNO.X = 10;
                //txtET_09_Leyenda_PNO.X = lineItem_Division_06_Vertical.X;
                txtET_11_Refrigerados.X = txtET_10_Controlados.X + txtET_10_Controlados.Height;
                txtET_11_Refrigerados.Height = txtET_10_Controlados.Height;
                txtET_11_Refrigerados.Width = txtET_10_Controlados.Width;


                txtET_12_Leyenda_PNO = new TextItem();
                txtET_12_Leyenda_PNO.Text = info.Campo("LeyendaPNO");
                txtET_12_Leyenda_PNO.Font.Name = fuente.Name;
                txtET_12_Leyenda_PNO.Font.Unit = fuente.Unit;
                txtET_12_Leyenda_PNO.Font.Size = 7;
                txtET_12_Leyenda_PNO.Font.Bold = true;
                txtET_12_Leyenda_PNO.TextAlignment = TextAlignment.Center;
                txtET_12_Leyenda_PNO.TextPadding = new FrameThickness(dPadding);
                txtET_12_Leyenda_PNO.BorderThickness = new FrameThickness(.00);
                txtET_12_Leyenda_PNO.Y = txtET_11_Refrigerados.Y + (txtET_11_Refrigerados.Height + dMargen);
                txtET_12_Leyenda_PNO.X = txtET_10_Controlados.X;
                txtET_12_Leyenda_PNO.Height = txtET_10_Controlados.Height;
                txtET_12_Leyenda_PNO.Width = txtET_10_Controlados.Width;

                txtET_12_Leyenda_PNO.X = txtET_08_FechaEtiquetado_Leyenda.X; 
                txtET_12_Leyenda_PNO.Y = txtET_08_FechaEtiquetado_Leyenda.Y + txtET_08_FechaEtiquetado_Leyenda.Height + (dMargen * .75);
                txtET_12_Leyenda_PNO.Width = txtET_08_FechaEtiquetado_Leyenda.Width;
                txtET_12_Leyenda_PNO.Height = .3; 



                //////////////////////////////////////////////////////////////////////////////////  
                lineItem_Division_07_NoEtiquetas = new LineShapeItem();
                lineItem_Division_07_NoEtiquetas.Orientation = LineOrientation.Horizontal;
                lineItem_Division_07_NoEtiquetas.X = lineItem_Division_01.X + (0);
                lineItem_Division_07_NoEtiquetas.Y = 6;
                lineItem_Division_07_NoEtiquetas.Width = 3.5;
                lineItem_Division_07_NoEtiquetas.Height = 0.01;
                //lineItem_Division_07_NoEtiquetas.StrokeThickness = dBorde_Lineas;

                txtET_13_EtiquetaCajas = new TextItem(); // dMargen * 2, dMargen * 2, dAncho - (dMargen * 4), 0.3, info.Campo("EtiquetasCajas"));
                txtET_13_EtiquetaCajas.Font.Name = fuente.Name;
                txtET_13_EtiquetaCajas.Font.Unit = fuente.Unit;
                txtET_13_EtiquetaCajas.Font.Size = 11;
                txtET_13_EtiquetaCajas.Font.Bold = true;
                txtET_13_EtiquetaCajas.RotationAngle = 90;
                txtET_13_EtiquetaCajas.TextAlignment = TextAlignment.Center;
                txtET_13_EtiquetaCajas.TextPadding = new FrameThickness(dPadding);
                txtET_13_EtiquetaCajas.X = lineItem_Division_07_NoEtiquetas.X + (dMargen);
                txtET_13_EtiquetaCajas.Y = lineItem_Division_07_NoEtiquetas.Y + (dMargen);
                txtET_13_EtiquetaCajas.BorderThickness = new FrameThickness(0.0);  //dBorde
                txtET_13_EtiquetaCajas.Width = txtET_10_Controlados.Width + (dMargen);
                txtET_13_EtiquetaCajas.Height = txtET_10_Controlados.Height + .2;
                txtET_13_EtiquetaCajas.Text = info.Campo("TituloEtiquetas");

                txtET_13_EtiquetaCajas.Y = lineItem_Seccion_02_FolioReferencia.Y + ( dMargen *.5 ); 
                txtET_13_EtiquetaCajas.X = lineItem_Seccion_02_FolioReferencia.X - (txtET_13_EtiquetaCajas.Height + ( dMargen * 1 ) ) ;
                txtET_13_EtiquetaCajas.Width = lineItem_Division_02.Y - ( txtET_13_EtiquetaCajas.Y + (dMargen * .5)); 




                lineItem_Division_08_NoEtiquetas = new LineShapeItem();
                lineItem_Division_08_NoEtiquetas.Orientation = LineOrientation.Horizontal;
                lineItem_Division_08_NoEtiquetas.X = lineItem_Division_07_NoEtiquetas.X;
                lineItem_Division_08_NoEtiquetas.Y = txtET_13_EtiquetaCajas.Y + txtET_13_EtiquetaCajas.Height;
                lineItem_Division_08_NoEtiquetas.Width = lineItem_Division_07_NoEtiquetas.Width;
                lineItem_Division_08_NoEtiquetas.Height = 0.01;
                //lineItem_Division_08_NoEtiquetas.StrokeThickness = dBorde_Lineas;


                txtET_14_EtiquetaCajas_Leyenda = new TextItem(); // dMargen * 2, dMargen * 2, dAncho - (dMargen * 4), 0.3, info.Campo("EtiquetasCajas"));
                txtET_14_EtiquetaCajas_Leyenda.Font.Name = fuente.Name;
                txtET_14_EtiquetaCajas_Leyenda.Font.Unit = fuente.Unit;
                txtET_14_EtiquetaCajas_Leyenda.Font.Size = 10;
                txtET_14_EtiquetaCajas_Leyenda.RotationAngle = 90;
                txtET_14_EtiquetaCajas_Leyenda.TextAlignment = TextAlignment.Center;
                txtET_14_EtiquetaCajas_Leyenda.TextPadding = new FrameThickness(dPadding);
                txtET_14_EtiquetaCajas_Leyenda.Width = rectItem.Width - (dMargen * 2) - 4;
                txtET_14_EtiquetaCajas_Leyenda.X = lineItem_Division_01.X + (dMargen);
                txtET_14_EtiquetaCajas_Leyenda.Y = txtET_13_EtiquetaCajas.Y + (txtET_13_EtiquetaCajas.Height + dMargen);
                txtET_14_EtiquetaCajas_Leyenda.BorderThickness = new FrameThickness(0.0);  //dBorde
                txtET_14_EtiquetaCajas_Leyenda.Width = txtET_13_EtiquetaCajas.Width - (dMargen);
                txtET_14_EtiquetaCajas_Leyenda.Height = txtET_13_EtiquetaCajas.Height + (dMargen);
                txtET_14_EtiquetaCajas_Leyenda.Text = lineItem_Seccion_02_FolioReferencia.Height.ToString();

                txtET_14_EtiquetaCajas_Leyenda.Y = txtET_13_EtiquetaCajas.Y;
                txtET_14_EtiquetaCajas_Leyenda.X = txtET_13_EtiquetaCajas.X - (txtET_13_EtiquetaCajas.Height + (dMargen * 2));
                txtET_14_EtiquetaCajas_Leyenda.Width = txtET_13_EtiquetaCajas.Width;



                txtET_14_EtiquetaCajas_Leyenda.Text = info.Campo("LeyandaEtiquetas");
                if(info.CampoBool("EtiquetadoManual"))
                {
                    txtET_14_EtiquetaCajas_Leyenda.DataField = "Etiqueta";
                }




                //txtET_13_EtiquetaCajas = txtET_14_EtiquetaCajas_Leyenda; 
                //////////////////////////////////////////////////////////////////////////////////  


                //////////////////////////////////////////////////////////////////////////////////                  
                txtET_15_Programa = new TextItem();
                txtET_15_Programa.Font.Name = fuente.Name;
                txtET_15_Programa.Font.Unit = fuente.Unit;
                txtET_15_Programa.Font.Size = 7;
                txtET_15_Programa.Font.Bold = true;
                txtET_15_Programa.TextAlignment = TextAlignment.Center;
                txtET_15_Programa.TextPadding = new FrameThickness(dPadding);
                txtET_15_Programa.X = rectItem.X + dMargen;
                txtET_15_Programa.Y = lineItem_Seccion_01.Y + (dMargen * .25);
                txtET_15_Programa.BorderThickness = new FrameThickness(0.0);  //dBorde
                txtET_15_Programa.Width = lineItem_Division_01.X - (dMargen * 2.5);
                txtET_15_Programa.Height = txtET_10_Controlados.Height;
                txtET_15_Programa.Height = dMargen * 3.0;
                txtET_15_Programa.Text = info.Campo("TituloPrograma");


                lineItem_Division_09_Programa = new LineShapeItem();
                lineItem_Division_09_Programa.Orientation = LineOrientation.Horizontal;
                lineItem_Division_09_Programa.X = rectItem.X;
                lineItem_Division_09_Programa.Y = txtET_15_Programa.Y + txtET_15_Programa.Height; // + (dMargen * .5);
                lineItem_Division_09_Programa.Width = lineItem_Division_01.X - dMargen;                
                lineItem_Division_09_Programa.Height = 0.01;





                lineItem_Division_10_Referencia = new LineShapeItem();
                lineItem_Division_10_Referencia.Orientation = LineOrientation.Horizontal;
                lineItem_Division_10_Referencia.X = lineItem_Division_09_Programa.X;
                //lineItem_Division_10_Referencia.Y = lineItem_Division_09_Programa.Y + 1.0;
                lineItem_Division_10_Referencia.Y = lineItem_Division_09_Programa.Y + 1.0;
                lineItem_Division_10_Referencia.Width = lineItem_Division_09_Programa.Width;
                lineItem_Division_10_Referencia.Height = 0.01;


                txtET_16_Referencia = new TextItem();
                txtET_16_Referencia.Font.Name = fuente.Name;
                txtET_16_Referencia.Font.Unit = fuente.Unit;
                txtET_16_Referencia.Font.Size = 7;
                txtET_16_Referencia.Font.Bold = true;
                txtET_16_Referencia.TextAlignment = TextAlignment.Center;
                txtET_16_Referencia.TextPadding = new FrameThickness(dPadding);
                txtET_16_Referencia.X = txtET_15_Programa.X;
                txtET_16_Referencia.Y = lineItem_Division_10_Referencia.Y + (dMargen * .5);
                txtET_16_Referencia.BorderThickness = new FrameThickness(0.0);  //dBorde
                txtET_16_Referencia.Width = txtET_15_Programa.Width;
                txtET_16_Referencia.Height = txtET_15_Programa.Height;
                txtET_16_Referencia.Text = info.Campo("TituloReferencia_ETQ");


                lineItem_Division_11_Referencia = new LineShapeItem();
                lineItem_Division_11_Referencia.Orientation = LineOrientation.Horizontal;
                lineItem_Division_11_Referencia.X = lineItem_Division_09_Programa.X;
                lineItem_Division_11_Referencia.Y = lineItem_Division_10_Referencia.Y + txtET_16_Referencia.Height + (dMargen * .5);
                lineItem_Division_11_Referencia.Width = lineItem_Division_09_Programa.Width;
                lineItem_Division_11_Referencia.Height = 0.01;

                //FAV
                lineItem_Division_12_Referencia = new LineShapeItem();
                lineItem_Division_12_Referencia.Orientation = LineOrientation.Horizontal;
                lineItem_Division_12_Referencia.X = lineItem_Division_11_Referencia.X;
                lineItem_Division_12_Referencia.Y = lineItem_Division_11_Referencia.Y + .50;
                lineItem_Division_12_Referencia.Width = lineItem_Division_11_Referencia.Width;
                lineItem_Division_12_Referencia.Height = 0.01;


                txtET_16_ReferenciaFF = new TextItem();
                txtET_16_ReferenciaFF.Font.Name = fuente.Name;
                txtET_16_ReferenciaFF.Font.Unit = fuente.Unit;
                txtET_16_ReferenciaFF.Font.Size = 7;
                txtET_16_ReferenciaFF.Font.Bold = true;
                txtET_16_ReferenciaFF.TextAlignment = TextAlignment.Center;
                txtET_16_ReferenciaFF.TextPadding = new FrameThickness(dPadding);
                txtET_16_ReferenciaFF.X = txtET_16_Referencia.X;
                txtET_16_ReferenciaFF.Y = lineItem_Division_12_Referencia.Y + (dMargen * .5);
                txtET_16_ReferenciaFF.BorderThickness = new FrameThickness(0.0);  //dBorde
                txtET_16_ReferenciaFF.Width = txtET_16_Referencia.Width;
                txtET_16_ReferenciaFF.Height = txtET_16_Referencia.Height;
                txtET_16_ReferenciaFF.Text = info.Campo("TituloFuente");


                lineItem_Division_13_Referencia = new LineShapeItem();
                lineItem_Division_13_Referencia.Orientation = LineOrientation.Horizontal;
                lineItem_Division_13_Referencia.X = lineItem_Division_12_Referencia.X;
                lineItem_Division_13_Referencia.Y = lineItem_Division_12_Referencia.Y + txtET_16_ReferenciaFF.Height + (dMargen * .5);
                lineItem_Division_13_Referencia.Width = lineItem_Division_12_Referencia.Width;
                lineItem_Division_13_Referencia.Height = 0.01;
                //FAV



                txtET_15_Programa_Leyenda = new TextItem();
                txtET_15_Programa_Leyenda.Font.Name = fuente.Name;
                txtET_15_Programa_Leyenda.Font.Unit = fuente.Unit;
                txtET_15_Programa_Leyenda.Font.Size = 6;
                txtET_15_Programa_Leyenda.Font.Bold = true;
                txtET_15_Programa_Leyenda.TextAlignment = TextAlignment.Center;
                txtET_15_Programa_Leyenda.TextPadding = new FrameThickness(dPadding);
                txtET_15_Programa_Leyenda.X = txtET_15_Programa.X;
                txtET_15_Programa_Leyenda.Y = lineItem_Division_09_Programa.Y + (dMargen * .5);
                txtET_15_Programa_Leyenda.BorderThickness = new FrameThickness(0.00);  //dBorde
                txtET_15_Programa_Leyenda.Width = txtET_15_Programa.Width;
                txtET_15_Programa_Leyenda.Height = 1.0 - (dMargen * 1);
                txtET_15_Programa_Leyenda.Text = info.Campo("LeyendaPrograma");


                txtET_16_Referencia_Leyenda = new TextItem();
                txtET_16_Referencia_Leyenda.Font.Name = fuente.Name;
                txtET_16_Referencia_Leyenda.Font.Unit = fuente.Unit;
                txtET_16_Referencia_Leyenda.Font.Size = 12;
                txtET_16_Referencia_Leyenda.Font.Bold = true;
                txtET_16_Referencia_Leyenda.TextAlignment = TextAlignment.Center;
                txtET_16_Referencia_Leyenda.TextPadding = new FrameThickness(dPadding);
                txtET_16_Referencia_Leyenda.X = txtET_15_Programa.X;
                txtET_16_Referencia_Leyenda.Y = lineItem_Division_11_Referencia.Y + (dMargen * .5);
                txtET_16_Referencia_Leyenda.BorderThickness = new FrameThickness(0.00);  //dBorde
                txtET_16_Referencia_Leyenda.Width = txtET_15_Programa.Width;
                txtET_16_Referencia_Leyenda.Height = 1.0 - (dMargen * 1);
                //txtET_16_Referencia_Leyenda.Text = info.Campo("LeyendaReferencia");
                txtET_16_Referencia_Leyenda.Text = info.Campo("ReferenciaInterna");

                txtET_16_Referencia_LeyendaFF = new TextItem();
                txtET_16_Referencia_LeyendaFF.Font.Name = fuente.Name;
                txtET_16_Referencia_LeyendaFF.Font.Unit = fuente.Unit;
                txtET_16_Referencia_LeyendaFF.Font.Size = 6;
                txtET_16_Referencia_LeyendaFF.Font.Bold = true;
                txtET_16_Referencia_LeyendaFF.TextAlignment = TextAlignment.Center;
                txtET_16_Referencia_LeyendaFF.TextPadding = new FrameThickness(dPadding);
                txtET_16_Referencia_LeyendaFF.X = txtET_16_Referencia.X;
                txtET_16_Referencia_LeyendaFF.Y = lineItem_Division_12_Referencia.Y + (dMargen * .5);
                txtET_16_Referencia_LeyendaFF.BorderThickness = new FrameThickness(0.00);  //dBorde
                txtET_16_Referencia_LeyendaFF.Width = txtET_16_Referencia.Width;
                txtET_16_Referencia_LeyendaFF.Height = 1.0 - (dMargen * 1);
                txtET_16_Referencia_LeyendaFF.Text = info.Campo("Fuente");

                //////////////////////////////////////////////////////////////////////////////////  



                ////////////////Add items to ThermalLabel object... 
                tLabel.Items.Add(rectItem);

                tLabel.Items.Add(txtEmpresa);
                tLabel.Items.Add(lineItem);

                tLabel.Items.Add(txtET_02_Origen);
                tLabel.Items.Add(lineItem_Seccion_00);
                tLabel.Items.Add(txtET_03_Destino);
                tLabel.Items.Add(lineItem_Seccion_01);

                tLabel.Items.Add(txtET_04_FolioReferencia_Titulo);
                tLabel.Items.Add(lineItem_Seccion_02_FolioReferencia_Pie);
                tLabel.Items.Add(txtET_05_Barcode);
                tLabel.Items.Add(lineItem_Seccion_02_FolioReferencia);

                tLabel.Items.Add(lineItem_Division_01);
                tLabel.Items.Add(lineItem_Division_02);
                tLabel.Items.Add(lineItem_Division_03);
                tLabel.Items.Add(lineItem_Division_04);
                tLabel.Items.Add(lineItem_Division_05);
                tLabel.Items.Add(lineItem_Division_06_Vertical);

                tLabel.Items.Add(txtET_06_Surtio);
                tLabel.Items.Add(txtET_06_Surtio_Leyenda);
                tLabel.Items.Add(txtET_07_Verifico);
                tLabel.Items.Add(txtET_07_Verifico_Leyenda);
                tLabel.Items.Add(txtET_08_FechaEtiquetado);
                tLabel.Items.Add(txtET_08_FechaEtiquetado_Leyenda);
                tLabel.Items.Add(txtET_09_Antibioticos);
                tLabel.Items.Add(txtET_10_Controlados);
                tLabel.Items.Add(txtET_11_Refrigerados);
                tLabel.Items.Add(txtET_12_Leyenda_PNO);



                ////tLabel.Items.Add(lineItem_Division_07_NoEtiquetas);
                tLabel.Items.Add(txtET_13_EtiquetaCajas);
                ////tLabel.Items.Add(lineItem_Division_08_NoEtiquetas);
                tLabel.Items.Add(txtET_14_EtiquetaCajas_Leyenda);


                tLabel.Items.Add(txtET_15_Programa);
                tLabel.Items.Add(lineItem_Division_09_Programa);
                tLabel.Items.Add(txtET_15_Programa_Leyenda); 

                tLabel.Items.Add(lineItem_Division_10_Referencia);
                tLabel.Items.Add(txtET_16_Referencia);
                tLabel.Items.Add(lineItem_Division_11_Referencia);
                tLabel.Items.Add(txtET_16_Referencia_Leyenda);


                ////tLabel.Items.Add(lineItem_Division_12_Referencia);
                ////tLabel.Items.Add(txtET_16_ReferenciaFF);
                ////tLabel.Items.Add(lineItem_Division_13_Referencia);
                //tLabel.Items.Add(txtET_16_Referencia_LeyendaFF); //FAV 19/02/2024


                ////////////////Add items to ThermalLabel object... 


                if (info.CampoBool("EtiquetadoManual"))
                {
                    //// Multiples etiquetas 
                    string sTextoEtiqueta = "";
                    iNumero_Etiqueta_Inicial = info.CampoInt("ETQ_Inicial");
                    iNumeroDeEtiquetas = info.CampoInt("ETQ_Final");
                    List<EtiquetaPedido> etiquetas = new List<EtiquetaPedido>();

                    for(int i = iNumero_Etiqueta_Inicial; i <= iNumeroDeEtiquetas; i++)
                    {
                        sTextoEtiqueta = string.Format("{0} DE {1}", i, iNumeroDeEtiquetas);
                        etiquetas.Add(new EtiquetaPedido(sTextoEtiqueta));
                    }

                    tLabel.DataSource = etiquetas;
                    //// Multiples etiquetas 
                }


                if(VistaPrevia)
                {
                    bGenerar = false;
                    visor = new FrmVisorEtiquetas(tLabel);

                    if(FormaPadre != null)
                    {
                        visor.ShowInTaskbar = false;
                        visor.ShowDialog(FormaPadre);
                    }
                    else
                    {
                        visor.ShowDialog();
                    }
                }
                else
                {
                    //_currentThermalLabel = tLabel;
                    Imprimir(tLabel, sImpresoraEtiquetas, 1);
                }
            }

            return bRegresa;
        }

        private bool GenerarEtiqueta___4x6(Form FormaPadre, bool VistaPrevia, DataSet Datos)
        {
            Neodynamic.SDK.Printing.Font fuente = new Neodynamic.SDK.Printing.Font();
            UnitType unidad = UnitType.Cm; 
            clsLeer info = new clsLeer();
            bool bRegresa = false;
            bool bGenerar = true;

            DateTime dtFecha = DateTime.Now; 
            string sFechaImpresion = ""; 

            RectangleShapeItem rectItem; 
            TextItem txtEmpresa;
            LineShapeItem lineItem;
            LineShapeItem lineItem_Seccion_01;
            LineShapeItem lineItem_Seccion_02_FolioReferencia;
            LineShapeItem lineItem_Seccion_02_FolioReferencia_Pie;
            TextItem txtET_02_Origen;
            TextItem txtET_03_Destino;
            TextItem txtET_04_FolioReferencia;
            TextItem txtET_04_FolioReferencia_Titulo;
            BarcodeItem txtET_05_Barcode;
            TextItem txtET_06_Surtio;
            TextItem txtET_06_Surtio_Leyenda;
            TextItem txtET_07_Verifico;
            TextItem txtET_07_Verifico_Leyenda;
            TextItem txtET_08_FechaEtiquetado;
            TextItem txtET_08_FechaEtiquetado_Leyenda;

            TextItem txtET_09_Antibioticos;
            TextItem txtET_10_Controlados;
            TextItem txtET_11_Refrigerados; 
            TextItem txtET_12_Leyenda_PNO; 

            TextItem txtET_13_EtiquetaCajas;
            TextItem txtET_14_EtiquetaCajas_Leyenda;
            TextItem txtET_15_Programa;
            TextItem txtET_15_Programa_Leyenda;
            TextItem txtET_16_Referencia;
            TextItem txtET_16_Referencia_Leyenda;

            LineShapeItem lineItem_Division_01;
            LineShapeItem lineItem_Division_02;
            LineShapeItem lineItem_Division_03;
            LineShapeItem lineItem_Division_04;
            LineShapeItem lineItem_Division_05;
            LineShapeItem lineItem_Division_06_Vertical;


            LineShapeItem lineItem_Division_07_NoEtiquetas;
            LineShapeItem lineItem_Division_08_NoEtiquetas;
            LineShapeItem lineItem_Division_09_Programa;
            LineShapeItem lineItem_Division_10_Referencia;
            LineShapeItem lineItem_Division_11_Referencia;




            double dFactor = 2.50;            
            double dAncho = 6 * dFactor;
            double dAlto = 4 * dFactor;
            double dMargen = 0.05 * dFactor;
            double dAlto_Encabezado = 1;
            double dSeparador = 0.075 * dFactor;
            double dSeparadorDivisiones = 0.6;
            double dBorde = 0.00;
            double dBorde_Lineas = 0.05;
            double dPadding = 0.01; 

            //dFactor = 1; 

            unidad = dFactor == 1 ? UnitType.Inch : UnitType.Cm;
            dAlto_Encabezado = unidad == UnitType.Inch ? dFactor * 0.25 : 1;

            dCornerRadius = 0.1 * dFactor;

            //// Descargar los datos del enbezado 
            info.DataSetClase = Datos;


            while (info.Leer() && bExisteImpresoraEtiquetas && bGenerar)
            {
                fuente.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
                fuente.Unit = unidad == UnitType.Inch ? FontUnit.Inch : FontUnit.Millimeter;
                fuente.Unit = FontUnit.Point;
                fuente.Name = "Arial";

                List<ThermalLabel> listaEtiquetas = new List<ThermalLabel>();
                bool bExistenEtiquetas = false;
                int iNumero_Etiqueta_Inicial = 0;
                int iNumeroDeEtiquetas = 0;
                int iContadorEtiquetas = 0;

                //Define a ThermalLabel object and set unit to inch and label size
                ThermalLabel tLabel = new ThermalLabel(unidad, dAncho, dAlto);
                tLabel.GapLength = 0.2;


                rectItem = new RectangleShapeItem();
                rectItem.CornerRadius.BottomLeft = dCornerRadius;
                rectItem.CornerRadius.BottomRight = dCornerRadius;
                rectItem.CornerRadius.TopLeft = dCornerRadius;
                rectItem.CornerRadius.TopRight = dCornerRadius;
                rectItem.X = dMargen;
                rectItem.Y = dMargen;
                rectItem.Height = dAlto - (dMargen * 2);
                rectItem.Width = dAncho - (dMargen * 2);
                rectItem.StrokeThickness = 0.02 * dFactor;


                ////Define a TextItem object for product name
                //TextItem txtEmpresa = new TextItem(dMargen * 2, dMargen * 2, dAncho - (dMargen * 4), 0.3, "INTERCONTINENTAL DE MEDICAMENTOS S.A. DE C.V.");

                txtEmpresa = new TextItem(); // new TextItem(dMargen * 2, dMargen * 2, dAncho - (dMargen * 4), dAlto_Encabezado, info.Campo("Empresa"));
                txtEmpresa.Font.Name = fuente.Name;
                txtEmpresa.Font.Unit = fuente.Unit;
                txtEmpresa.Text = info.Campo("Empresa"); 
                txtEmpresa.Font.Size = 14;
                txtEmpresa.Font.Bold = true; 
                //txtEmpresa.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
                //txtEmpresa.Font.Unit = FontUnit.Point;
                txtEmpresa.TextAlignment = TextAlignment.Center;
                txtEmpresa.TextPadding = new FrameThickness(dPadding);
                txtEmpresa.Height = 0.75;
                //txtEmpresa.Width = dAncho - (dMargen * 5);
                txtEmpresa.Width = rectItem.Width - (dMargen * 2); 
                txtEmpresa.X = dMargen * 2;
                txtEmpresa.Y = dMargen * 2.02;
                txtEmpresa.BorderThickness = new FrameThickness(dBorde);  

                lineItem = new LineShapeItem();
                lineItem.Orientation = LineOrientation.Horizontal;
                lineItem.X = rectItem.X;
                lineItem.Y = txtEmpresa.Height+dMargen;
                lineItem.Width = rectItem.Width;
                lineItem.Height = 0.01;
                lineItem.StrokeThickness = dBorde_Lineas; 

                txtET_02_Origen = new TextItem(); // dMargen * 2, dMargen * 2, dAncho - (dMargen * 4), 0.3, info.Campo("EtiquetasCajas"));
                txtET_02_Origen.Font.Name = fuente.Name;
                txtET_02_Origen.Font.Unit = fuente.Unit;
                txtET_02_Origen.Font.Size = 12;
                txtET_02_Origen.Font.Bold = true; 
                //txtET_02_Origen.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
                //txtET_02_Origen.Font.Unit = FontUnit.Point;
                txtET_02_Origen.TextAlignment = TextAlignment.Center;
                txtET_02_Origen.TextPadding = new FrameThickness(dPadding);
                txtET_02_Origen.Text = info.Campo("Farmacia");
                txtET_02_Origen.Height = dMargen * 4;
                //txtET_02_Origen.Width = dAncho - (dMargen * 5);
                txtET_02_Origen.Width = rectItem.Width - (dMargen * 2);
                txtET_02_Origen.X = txtEmpresa.X; // dMargen * 2;
                txtET_02_Origen.Y = txtEmpresa.Y + txtEmpresa.Height;
                txtET_02_Origen.BorderThickness = new FrameThickness(dBorde);
                txtET_02_Origen.Width = txtEmpresa.Width; 

                txtET_03_Destino = new TextItem(); // dMargen * 2, dMargen * 2, dAncho - (dMargen * 4), 0.3, info.Campo("EtiquetasCajas"));
                txtET_03_Destino.Font.Name = fuente.Name;
                txtET_03_Destino.Font.Unit = fuente.Unit;
                txtET_03_Destino.Font.Size = 10;
                txtET_03_Destino.Font.Bold = true;
                //txtET_03_Destino.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
                //txtET_03_Destino.Font.Unit = FontUnit.Point;
                txtET_03_Destino.TextAlignment = TextAlignment.Center;
                txtET_03_Destino.TextPadding = new FrameThickness(dPadding);
                txtET_03_Destino.Text = info.Campo("FarmaciaPedido");
                txtET_03_Destino.Height = dMargen * 4;
                //txtET_03_Destino.Width = dAncho - (dMargen * 5);
                txtET_03_Destino.Width = rectItem.Width;
                txtET_03_Destino.X = txtEmpresa.X; // dMargen * 2;
                txtET_03_Destino.Y = txtET_02_Origen.Y + txtET_02_Origen.Height;
                txtET_03_Destino.BorderThickness = new FrameThickness(dBorde);
                txtET_03_Destino.Width = txtEmpresa.Width;


                lineItem_Seccion_01 = new LineShapeItem();
                lineItem_Seccion_01.Orientation = LineOrientation.Horizontal;
                lineItem_Seccion_01.X = rectItem.X;
                lineItem_Seccion_01.Y = txtET_03_Destino.Y + txtET_03_Destino.Height + dMargen;
                lineItem_Seccion_01.Width = rectItem.Width;
                lineItem_Seccion_01.Height = 0.01;
                lineItem_Seccion_01.StrokeThickness = dBorde_Lineas;


                lineItem_Seccion_02_FolioReferencia = new LineShapeItem();
                lineItem_Seccion_02_FolioReferencia.Orientation = LineOrientation.Vertical;
                lineItem_Seccion_02_FolioReferencia.X = rectItem.Width - 3;
                lineItem_Seccion_02_FolioReferencia.Y = lineItem_Seccion_01.Y;
                lineItem_Seccion_02_FolioReferencia.Width = 0.01; // rectItem.Width;
                lineItem_Seccion_02_FolioReferencia.Height = (rectItem.Height - lineItem_Seccion_02_FolioReferencia.Y) + dMargen;
                lineItem_Seccion_02_FolioReferencia.StrokeThickness = 0.02 * dFactor;
               


                txtET_04_FolioReferencia_Titulo = new TextItem();
                txtET_04_FolioReferencia_Titulo.Font.Name = fuente.Name;
                txtET_04_FolioReferencia_Titulo.Font.Unit = fuente.Unit;
                txtET_04_FolioReferencia_Titulo.Font.Size = 10;
                txtET_04_FolioReferencia_Titulo.Font.Bold = true;
                txtET_04_FolioReferencia_Titulo.RotationAngle = 90;
                //txtET_02_Origen.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
                //txtET_02_Origen.Font.Unit = FontUnit.Point;
                txtET_04_FolioReferencia_Titulo.TextAlignment = TextAlignment.Center;
                txtET_04_FolioReferencia_Titulo.TextPadding = new FrameThickness(dPadding);
                txtET_04_FolioReferencia_Titulo.Text = info.Campo("TituloReferencia");
                txtET_04_FolioReferencia_Titulo.Height = dMargen * 4;
                txtET_04_FolioReferencia_Titulo.X = rectItem.Width - (dMargen * 3.5); // dMargen * 2;
                txtET_04_FolioReferencia_Titulo.Y = lineItem_Seccion_02_FolioReferencia.Y + (lineItem_Seccion_02_FolioReferencia.Width * 2);
                txtET_04_FolioReferencia_Titulo.BorderThickness = new FrameThickness(0);
                //txtET_04_FolioReferencia_Titulo.Width = lineItem_Seccion_02_FolioReferencia.Height - (lineItem_Seccion_02_FolioReferencia.Width * 6);
                txtET_04_FolioReferencia_Titulo.Width = lineItem_Seccion_02_FolioReferencia.Height - (dMargen * 1.2);
                //txtET_04_FolioReferencia_Titulo.Text = txtET_04_FolioReferencia_Titulo.Width.ToString();


                txtET_04_FolioReferencia = new TextItem();
                txtET_04_FolioReferencia.Font.Name = fuente.Name;
                txtET_04_FolioReferencia.Font.Unit = fuente.Unit;
                txtET_04_FolioReferencia.Font.Size = 10;
                txtET_04_FolioReferencia.Font.Bold = true;
                txtET_04_FolioReferencia.RotationAngle = 90;
                //txtET_02_Origen.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
                //txtET_02_Origen.Font.Unit = FontUnit.Point;
                txtET_04_FolioReferencia.TextAlignment = TextAlignment.Center;
                txtET_04_FolioReferencia.TextPadding = new FrameThickness(dPadding);
                txtET_04_FolioReferencia.Text = info.Campo("FolioReferencia");
                txtET_04_FolioReferencia.Height = dMargen * 4;
                txtET_04_FolioReferencia.X = (txtET_04_FolioReferencia_Titulo.X * 1) - ((txtET_04_FolioReferencia_Titulo.Height * 4) + (dMargen / 2)); // dMargen * 2;
                txtET_04_FolioReferencia.Y = txtET_04_FolioReferencia_Titulo.Y;
                txtET_04_FolioReferencia.BorderThickness = new FrameThickness(.03);
                txtET_04_FolioReferencia.Width = txtET_04_FolioReferencia_Titulo.Width;
                //txtET_04_FolioReferencia.Text = txtET_04_FolioReferencia.Width.ToString();


                txtET_05_Barcode = new BarcodeItem();
                txtET_05_Barcode.Symbology = BarcodeSymbology.Code128; 
                txtET_05_Barcode.RotationAngle = 90;
                txtET_05_Barcode.Font.Name = fuente.Name;
                txtET_05_Barcode.Font.Unit = fuente.Unit;
                txtET_05_Barcode.Font.Size = 12;
                txtET_05_Barcode.Font.Name = Neodynamic.SDK.Printing.Font.NativePrinterFontA;
                //disable checksum
                txtET_05_Barcode.AddChecksum = false;
                //hide human readable text
                txtET_05_Barcode.DisplayCode = true;
                //set border
                txtET_05_Barcode.BorderThickness = new FrameThickness(0.00);
                //align barcode
                txtET_05_Barcode.BarcodeAlignment = BarcodeAlignment.MiddleCenter;
                //txtET_05_Barcode.CodeAlignment = BarcodeTextAlignment.BelowJustify;
                //txtET_05_Barcode.TextAlignment = BarcodeTextAlignment.BelowJustify; 
                txtET_05_Barcode.Sizing = BarcodeSizing.Fill; 

                //txtET_05_Barcode.Text = info.Campo("FolioReferencia");
                //txtET_05_Barcode.Code = info.Campo("Folio");

                //txtET_05_Barcode.Text = info.Campo("Folio");
                txtET_05_Barcode.Code = info.Campo("FolioReferencia");


                //txtET_05_Barcode.Sizing = BarcodeSizing.Fill; 


                //txtET_05_Barcode.Height = dMargen * 4;
                txtET_05_Barcode.Width = txtET_04_FolioReferencia.Width;
                txtET_05_Barcode.Height = txtET_04_FolioReferencia_Titulo.Height * 4;
                txtET_05_Barcode.X = (txtET_04_FolioReferencia_Titulo.X * 1) - ((txtET_04_FolioReferencia_Titulo.Height * 4) + (dMargen / 2)); // dMargen * 2;
                txtET_05_Barcode.Y = txtET_04_FolioReferencia.Y;

                //Set bars height to .3inch
                txtET_05_Barcode.BarHeight = txtET_05_Barcode.Height * .60;
                //Set bars width to 0.02inch
                txtET_05_Barcode.BarWidth = 0.04;


                lineItem_Seccion_02_FolioReferencia.X = txtET_05_Barcode.X - (dMargen / 2 );
                lineItem_Seccion_02_FolioReferencia_Pie = new LineShapeItem();
                lineItem_Seccion_02_FolioReferencia_Pie.Orientation = LineOrientation.Vertical;
                lineItem_Seccion_02_FolioReferencia_Pie.Width = 0.01; // rectItem.Width;
                lineItem_Seccion_02_FolioReferencia_Pie.Height = lineItem_Seccion_02_FolioReferencia.Height; // (rectItem.Height - lineItem_Seccion_02_FolioReferencia.Y) + dMargen;
                lineItem_Seccion_02_FolioReferencia_Pie.StrokeThickness = dBorde_Lineas;
                lineItem_Seccion_02_FolioReferencia_Pie.X = lineItem_Seccion_02_FolioReferencia.X + txtET_05_Barcode.Height + dMargen;
                lineItem_Seccion_02_FolioReferencia_Pie.Y = lineItem_Seccion_01.Y;


                lineItem_Division_01 = new LineShapeItem();
                lineItem_Division_01.Orientation = LineOrientation.Vertical;
                lineItem_Division_01.Width = 0.01; // rectItem.Width;
                lineItem_Division_01.Height = lineItem_Seccion_02_FolioReferencia_Pie.Height;
                lineItem_Division_01.Y = lineItem_Seccion_02_FolioReferencia_Pie.Y;
                lineItem_Division_01.X = lineItem_Seccion_02_FolioReferencia.X - (3.5); // -(dMargen / 2);


                lineItem_Division_02 = new LineShapeItem();
                lineItem_Division_02.Orientation = LineOrientation.Horizontal;
                lineItem_Division_02.Width = rectItem.Width;
                lineItem_Division_02.Width = lineItem_Seccion_02_FolioReferencia.X - dMargen;
                //lineItem_Division_02.Width = lineItem_Division_01.X - dMargen; 
                lineItem_Division_02.Height = 0.01; // lineItem_Seccion_02_FolioReferencia_Pie.Height;
                lineItem_Division_02.X = (dMargen);
                lineItem_Division_02.Y = 7.5;


                lineItem_Division_03 = new LineShapeItem();
                lineItem_Division_03.Orientation = LineOrientation.Horizontal;
                lineItem_Division_03.Width = rectItem.Width;
                lineItem_Division_03.Width = lineItem_Seccion_02_FolioReferencia.X - dMargen;
                //lineItem_Division_03.Width = lineItem_Division_01.X - dMargen; 
                lineItem_Division_03.Height = 0.01; // lineItem_Seccion_02_FolioReferencia_Pie.Height;
                lineItem_Division_03.X = (dMargen);
                lineItem_Division_03.Y = lineItem_Division_02.Y + dSeparadorDivisiones;

                lineItem_Division_04 = new LineShapeItem();
                lineItem_Division_04.Orientation = LineOrientation.Horizontal;
                lineItem_Division_04.Width = lineItem_Division_03.Width;
                lineItem_Division_04.Height = 0.01; // lineItem_Seccion_02_FolioReferencia_Pie.Height;
                lineItem_Division_04.X = (dMargen);
                lineItem_Division_04.Y = lineItem_Division_03.Y + dSeparadorDivisiones;

                lineItem_Division_05 = new LineShapeItem();
                lineItem_Division_05.Orientation = LineOrientation.Horizontal;
                lineItem_Division_05.Width = lineItem_Division_03.Width;
                lineItem_Division_05.Height = 0.01; // lineItem_Seccion_02_FolioReferencia_Pie.Height;
                lineItem_Division_05.X = (dMargen);
                lineItem_Division_05.Y = lineItem_Division_04.Y + dSeparadorDivisiones;


                lineItem_Division_06_Vertical = new LineShapeItem();
                lineItem_Division_06_Vertical.Orientation = LineOrientation.Vertical;
                lineItem_Division_06_Vertical.Y = lineItem_Division_02.Y;
                lineItem_Division_06_Vertical.X = (dMargen * 2) + 2.7;
                lineItem_Division_06_Vertical.X = lineItem_Division_06_Vertical.X * .80; 
                lineItem_Division_06_Vertical.Width = 0.01;
                lineItem_Division_06_Vertical.Height = (rectItem.Height - lineItem_Division_02.Y) + dMargen;
                //lineItem_Division_06_Vertical.Height = lineItem_Division_06_Vertical.Height * .80;


                //////////////////////////////////////////////////////////////////////////////////  
                txtET_06_Surtio = new TextItem();
                txtET_06_Surtio.Font.Name = fuente.Name;
                txtET_06_Surtio.Font.Unit = fuente.Unit;
                txtET_06_Surtio.Font.Size = 10;
                txtET_06_Surtio.Font.Bold = true;
                txtET_06_Surtio.TextAlignment = TextAlignment.Left;
                txtET_06_Surtio.TextPadding = new FrameThickness(dPadding);
                txtET_06_Surtio.Text = info.Campo("TituloSurtio");
                txtET_06_Surtio.Width = lineItem_Division_06_Vertical.X - (dMargen * 2);
                txtET_06_Surtio.Height = dMargen * 3.5;
                txtET_06_Surtio.X = dMargen * 1.75;
                txtET_06_Surtio.Y = lineItem_Division_02.Y + (lineItem_Division_02.Height + (dMargen * .70));
                txtET_06_Surtio.BorderThickness = new FrameThickness(.00);

                txtET_06_Surtio_Leyenda = new TextItem();
                txtET_06_Surtio_Leyenda.Font.Name = fuente.Name;
                txtET_06_Surtio_Leyenda.Font.Unit = fuente.Unit;
                txtET_06_Surtio_Leyenda.Font.Size = 10;
                txtET_06_Surtio_Leyenda.Font.Bold = true;
                txtET_06_Surtio_Leyenda.TextAlignment = TextAlignment.Left;
                txtET_06_Surtio_Leyenda.TextPadding = new FrameThickness(dPadding);
                txtET_06_Surtio_Leyenda.Width = lineItem_Division_01.X - (lineItem_Division_06_Vertical.X + (dMargen * 2));
                txtET_06_Surtio_Leyenda.Height = txtET_06_Surtio.Height;
                txtET_06_Surtio_Leyenda.X = lineItem_Division_06_Vertical.X + (dMargen * 1);
                txtET_06_Surtio_Leyenda.Y = txtET_06_Surtio.Y;
                txtET_06_Surtio_Leyenda.BorderThickness = new FrameThickness(.00);
                txtET_06_Surtio_Leyenda.Text = info.Campo("LeyendaSurtio");

                //////////////////////////////////////////////////////////////////////////////////  


                //////////////////////////////////////////////////////////////////////////////////  
                txtET_07_Verifico = new TextItem();
                txtET_07_Verifico.Font.Name = fuente.Name;
                txtET_07_Verifico.Font.Unit = fuente.Unit;
                txtET_07_Verifico.Font.Size = 10;
                txtET_07_Verifico.Font.Bold = true;
                txtET_07_Verifico.TextAlignment = TextAlignment.Left;
                txtET_07_Verifico.TextPadding = new FrameThickness(dPadding);
                txtET_07_Verifico.Text = info.Campo("TituloValido");
                txtET_07_Verifico.Width = txtET_06_Surtio.Width;;
                txtET_07_Verifico.Height = txtET_06_Surtio.Height;
                txtET_07_Verifico.X = txtET_06_Surtio.X;
                txtET_07_Verifico.Y = lineItem_Division_03.Y + (lineItem_Division_03.Height + (dMargen * .70));
                txtET_07_Verifico.BorderThickness = new FrameThickness(.00);

                txtET_07_Verifico_Leyenda = new TextItem();
                txtET_07_Verifico_Leyenda.Font.Name = fuente.Name;
                txtET_07_Verifico_Leyenda.Font.Unit = fuente.Unit;
                txtET_07_Verifico_Leyenda.Font.Size = 7;
                txtET_07_Verifico_Leyenda.Font.Bold = true;
                txtET_07_Verifico_Leyenda.TextAlignment = TextAlignment.Left;
                txtET_07_Verifico_Leyenda.TextPadding = new FrameThickness(dPadding);
                txtET_07_Verifico_Leyenda.Text = info.Campo("LeyendaValido");
                txtET_07_Verifico_Leyenda.Width = txtET_06_Surtio.Width;
                txtET_07_Verifico_Leyenda.Height = txtET_06_Surtio.Height;
                txtET_07_Verifico_Leyenda.X = txtET_06_Surtio_Leyenda.X;
                txtET_07_Verifico_Leyenda.Y = txtET_07_Verifico.Y;;
                txtET_07_Verifico_Leyenda.BorderThickness = new FrameThickness(.00);
                //////////////////////////////////////////////////////////////////////////////////  


                //////////////////////////////////////////////////////////////////////////////////  
                txtET_08_FechaEtiquetado = new TextItem();
                txtET_08_FechaEtiquetado.Font.Name = fuente.Name;
                txtET_08_FechaEtiquetado.Font.Unit = fuente.Unit;
                txtET_08_FechaEtiquetado.Font.Size = 10;
                txtET_08_FechaEtiquetado.Font.Bold = true;
                txtET_08_FechaEtiquetado.TextAlignment = TextAlignment.Left;
                txtET_08_FechaEtiquetado.TextPadding = new FrameThickness(dPadding);
                txtET_08_FechaEtiquetado.Text = info.Campo("TituloFecha");
                txtET_08_FechaEtiquetado.Width = txtET_06_Surtio.Width; ;
                txtET_08_FechaEtiquetado.Height = txtET_06_Surtio.Height;
                txtET_08_FechaEtiquetado.X = txtET_06_Surtio.X;
                txtET_08_FechaEtiquetado.Y = lineItem_Division_04.Y + (lineItem_Division_04.Height + (dMargen * .70));
                txtET_08_FechaEtiquetado.BorderThickness = new FrameThickness(.00);


                txtET_08_FechaEtiquetado_Leyenda = new TextItem();
                txtET_08_FechaEtiquetado_Leyenda.Font.Name = fuente.Name;
                txtET_08_FechaEtiquetado_Leyenda.Font.Unit = fuente.Unit;
                txtET_08_FechaEtiquetado_Leyenda.Font.Size = 7;
                txtET_08_FechaEtiquetado_Leyenda.Font.Bold = true;
                txtET_08_FechaEtiquetado_Leyenda.TextAlignment = TextAlignment.Center;
                txtET_08_FechaEtiquetado_Leyenda.TextPadding = new FrameThickness(dPadding);
                txtET_08_FechaEtiquetado_Leyenda.Width = txtET_06_Surtio_Leyenda.Width; // lineItem_Division_01.X - (lineItem_Division_06_Vertical.X + (dMargen * 2));
                txtET_08_FechaEtiquetado_Leyenda.Height = txtET_06_Surtio.Height;
                txtET_08_FechaEtiquetado_Leyenda.X = txtET_06_Surtio_Leyenda.X; // lineItem_Division_06_Vertical.X + (dMargen * 1);
                txtET_08_FechaEtiquetado_Leyenda.Y = txtET_08_FechaEtiquetado.Y;
                txtET_08_FechaEtiquetado_Leyenda.BorderThickness = new FrameThickness(.00);
                txtET_08_FechaEtiquetado_Leyenda.Text = DateTime.Now.ToString();

                dtFecha = info.CampoFecha("FechaImpresion");
                sFechaImpresion = string.Format("{0}, {1} de {2} del {3}  {4}:{5}",
                    General.FechaNombreDia(dtFecha), dtFecha.Day, General.FechaNombreMes(dtFecha), dtFecha.Year,
                    General.Fg.PonCeros(dtFecha.Hour, 2), General.Fg.PonCeros(dtFecha.Minute, 2)
                    ); 
                txtET_08_FechaEtiquetado_Leyenda.Text = sFechaImpresion;
                //////////////////////////////////////////////////////////////////////////////////  


                txtET_09_Antibioticos = new TextItem();
                txtET_09_Antibioticos.Text = info.Campo("LeyendaAntibioticos");
                txtET_09_Antibioticos.Font.Name = fuente.Name;
                txtET_09_Antibioticos.Font.Unit = fuente.Unit;
                txtET_09_Antibioticos.Font.Size = 8;
                txtET_09_Antibioticos.Font.Bold = true;
                txtET_09_Antibioticos.Font.Underline = info.CampoBool("ContieneAntibioticos");
                txtET_09_Antibioticos.TextAlignment = TextAlignment.Left;
                txtET_09_Antibioticos.TextPadding = new FrameThickness(dPadding);
                txtET_09_Antibioticos.BorderThickness = new FrameThickness(.00);
                txtET_09_Antibioticos.Y = txtET_06_Surtio.Y; // + (txtET_06_Surtio.Height );
                //txtET_09_Leyenda_PNO.X = 10;
                //txtET_09_Leyenda_PNO.X = lineItem_Division_06_Vertical.X;
                txtET_09_Antibioticos.X = lineItem_Division_01.X + (dMargen * 1);
                txtET_09_Antibioticos.Height = txtET_08_FechaEtiquetado_Leyenda.Height;
                txtET_09_Antibioticos.Width = lineItem_Seccion_02_FolioReferencia.X - (lineItem_Division_01.X + (dMargen * 2));



                txtET_10_Controlados = new TextItem();
                txtET_10_Controlados.Text = info.Campo("LeyendaControlados");
                txtET_10_Controlados.Font.Name = fuente.Name;
                txtET_10_Controlados.Font.Unit = fuente.Unit;
                txtET_10_Controlados.Font.Size = 8;
                txtET_10_Controlados.Font.Bold = true;
                txtET_10_Controlados.Font.Underline = info.CampoBool("ContieneControlados"); 
                txtET_10_Controlados.TextAlignment = TextAlignment.Left;
                txtET_10_Controlados.TextPadding = new FrameThickness(dPadding);
                txtET_10_Controlados.BorderThickness = new FrameThickness(.00);
                txtET_10_Controlados.Y = txtET_07_Verifico.Y;
                //txtET_09_Leyenda_PNO.X = 10;
                //txtET_09_Leyenda_PNO.X = lineItem_Division_06_Vertical.X;
                txtET_10_Controlados.X = txtET_09_Antibioticos.X;
                txtET_10_Controlados.Height = txtET_09_Antibioticos.Height;
                txtET_10_Controlados.Width = txtET_09_Antibioticos.Width;

                txtET_11_Refrigerados = new TextItem();
                txtET_11_Refrigerados.Text = info.Campo("LeyendaRefrigerados");
                txtET_11_Refrigerados.Font.Name = fuente.Name;
                txtET_11_Refrigerados.Font.Unit = fuente.Unit;
                txtET_11_Refrigerados.Font.Size = 8;
                txtET_11_Refrigerados.Font.Bold = true;
                txtET_11_Refrigerados.Font.Underline = info.CampoBool("ContieneRefrigerados"); 
                txtET_11_Refrigerados.TextAlignment = TextAlignment.Left;
                txtET_11_Refrigerados.TextPadding = new FrameThickness(dPadding);
                txtET_11_Refrigerados.BorderThickness = new FrameThickness(.00);
                txtET_11_Refrigerados.Y = txtET_08_FechaEtiquetado.Y;
                //txtET_09_Leyenda_PNO.X = 10;
                //txtET_09_Leyenda_PNO.X = lineItem_Division_06_Vertical.X;
                txtET_11_Refrigerados.X = txtET_10_Controlados.X;
                txtET_11_Refrigerados.Height = txtET_10_Controlados.Height;
                txtET_11_Refrigerados.Width = txtET_10_Controlados.Width; 


                txtET_12_Leyenda_PNO = new TextItem();
                txtET_12_Leyenda_PNO.Text = info.Campo("LeyendaPNO");
                txtET_12_Leyenda_PNO.Font.Name = fuente.Name;
                txtET_12_Leyenda_PNO.Font.Unit = fuente.Unit;
                txtET_12_Leyenda_PNO.Font.Size = 9;
                txtET_12_Leyenda_PNO.Font.Bold = true;
                txtET_12_Leyenda_PNO.TextAlignment = TextAlignment.Center;
                txtET_12_Leyenda_PNO.TextPadding = new FrameThickness(dPadding);
                txtET_12_Leyenda_PNO.BorderThickness = new FrameThickness(.00);
                txtET_12_Leyenda_PNO.Y = txtET_11_Refrigerados.Y + (txtET_11_Refrigerados.Height + dMargen);
                //txtET_09_Leyenda_PNO.X = 10;
                //txtET_09_Leyenda_PNO.X = lineItem_Division_06_Vertical.X;
                txtET_12_Leyenda_PNO.X = txtET_10_Controlados.X;
                txtET_12_Leyenda_PNO.Height = txtET_10_Controlados.Height;
                txtET_12_Leyenda_PNO.Width = txtET_10_Controlados.Width;



                //////////////////////////////////////////////////////////////////////////////////  
                lineItem_Division_07_NoEtiquetas = new LineShapeItem();
                lineItem_Division_07_NoEtiquetas.Orientation = LineOrientation.Horizontal;
                lineItem_Division_07_NoEtiquetas.X = lineItem_Division_01.X + (0);
                lineItem_Division_07_NoEtiquetas.Y = 6;
                lineItem_Division_07_NoEtiquetas.Width = 3.5;
                lineItem_Division_07_NoEtiquetas.Height = 0.01;
                //lineItem_Division_07_NoEtiquetas.StrokeThickness = dBorde_Lineas;

                txtET_13_EtiquetaCajas = new TextItem(); // dMargen * 2, dMargen * 2, dAncho - (dMargen * 4), 0.3, info.Campo("EtiquetasCajas"));
                txtET_13_EtiquetaCajas.Font.Name = fuente.Name;
                txtET_13_EtiquetaCajas.Font.Unit = fuente.Unit;
                txtET_13_EtiquetaCajas.Font.Size = 10;
                txtET_13_EtiquetaCajas.Font.Bold = true;
                txtET_13_EtiquetaCajas.TextAlignment = TextAlignment.Center;
                txtET_13_EtiquetaCajas.TextPadding = new FrameThickness(dPadding);
                txtET_13_EtiquetaCajas.X = lineItem_Division_07_NoEtiquetas.X + (dMargen);
                txtET_13_EtiquetaCajas.Y = lineItem_Division_07_NoEtiquetas.Y + (dMargen);
                txtET_13_EtiquetaCajas.BorderThickness = new FrameThickness(0.0);  //dBorde
                txtET_13_EtiquetaCajas.Width = txtET_10_Controlados.Width + (dMargen);
                txtET_13_EtiquetaCajas.Height = txtET_10_Controlados.Height;
                txtET_13_EtiquetaCajas.Text = info.Campo("TituloEtiquetas");
                //txtET_13_EtiquetaCajas.Text = "SEPALA";

                lineItem_Division_08_NoEtiquetas = new LineShapeItem();
                lineItem_Division_08_NoEtiquetas.Orientation = LineOrientation.Horizontal;
                lineItem_Division_08_NoEtiquetas.X = lineItem_Division_07_NoEtiquetas.X;
                lineItem_Division_08_NoEtiquetas.Y = txtET_13_EtiquetaCajas.Y + txtET_13_EtiquetaCajas.Height;
                lineItem_Division_08_NoEtiquetas.Width = lineItem_Division_07_NoEtiquetas.Width;
                lineItem_Division_08_NoEtiquetas.Height = 0.01;
                //lineItem_Division_08_NoEtiquetas.StrokeThickness = dBorde_Lineas;


                txtET_14_EtiquetaCajas_Leyenda = new TextItem(); // dMargen * 2, dMargen * 2, dAncho - (dMargen * 4), 0.3, info.Campo("EtiquetasCajas"));
                txtET_14_EtiquetaCajas_Leyenda.Font.Name = fuente.Name;
                txtET_14_EtiquetaCajas_Leyenda.Font.Unit = fuente.Unit;
                txtET_14_EtiquetaCajas_Leyenda.Font.Size = 14;
                txtET_14_EtiquetaCajas_Leyenda.TextAlignment = TextAlignment.Center;
                txtET_14_EtiquetaCajas_Leyenda.TextPadding = new FrameThickness(dPadding);
                txtET_14_EtiquetaCajas_Leyenda.Width = rectItem.Width - (dMargen * 2) - 4;
                txtET_14_EtiquetaCajas_Leyenda.X = lineItem_Division_01.X + (dMargen);
                txtET_14_EtiquetaCajas_Leyenda.Y = txtET_13_EtiquetaCajas.Y + (txtET_13_EtiquetaCajas.Height + dMargen);
                txtET_14_EtiquetaCajas_Leyenda.BorderThickness = new FrameThickness(0.0);  //dBorde
                txtET_14_EtiquetaCajas_Leyenda.Width = txtET_13_EtiquetaCajas.Width - (dMargen);
                txtET_14_EtiquetaCajas_Leyenda.Height = txtET_13_EtiquetaCajas.Height + (dMargen);
                txtET_14_EtiquetaCajas_Leyenda.Text = lineItem_Seccion_02_FolioReferencia.Height.ToString();
                txtET_14_EtiquetaCajas_Leyenda.Text = info.Campo("LeyandaEtiquetas");
                txtET_14_EtiquetaCajas_Leyenda.DataField = "Etiqueta";

                //txtET_13_EtiquetaCajas = txtET_14_EtiquetaCajas_Leyenda; 
                //////////////////////////////////////////////////////////////////////////////////  


                //////////////////////////////////////////////////////////////////////////////////                  
                txtET_15_Programa = new TextItem();
                txtET_15_Programa.Font.Name = fuente.Name;
                txtET_15_Programa.Font.Unit = fuente.Unit;
                txtET_15_Programa.Font.Size = 8;
                txtET_15_Programa.Font.Bold = true;
                txtET_15_Programa.TextAlignment = TextAlignment.Center;
                txtET_15_Programa.TextPadding = new FrameThickness(dPadding);
                txtET_15_Programa.X = rectItem.X + dMargen;
                txtET_15_Programa.Y = lineItem_Seccion_01.Y + (dMargen * .25);
                txtET_15_Programa.BorderThickness = new FrameThickness(0.0);  //dBorde
                txtET_15_Programa.Width = lineItem_Division_01.X - (dMargen * 2.5);
                txtET_15_Programa.Height = txtET_10_Controlados.Height;
                txtET_15_Programa.Height = dMargen * 3.0;
                txtET_15_Programa.Text = info.Campo("TituloPrograma");


                lineItem_Division_09_Programa = new LineShapeItem();
                lineItem_Division_09_Programa.Orientation = LineOrientation.Horizontal;
                lineItem_Division_09_Programa.X = rectItem.X;
                lineItem_Division_09_Programa.Y = txtET_15_Programa.Y + txtET_15_Programa.Height; // + (dMargen * .5);
                lineItem_Division_09_Programa.Width = lineItem_Division_01.X - dMargen;
                lineItem_Division_09_Programa.Height = 0.01;





                lineItem_Division_10_Referencia = new LineShapeItem();
                lineItem_Division_10_Referencia.Orientation = LineOrientation.Horizontal;
                lineItem_Division_10_Referencia.X = lineItem_Division_09_Programa.X;
                lineItem_Division_10_Referencia.Y = lineItem_Division_09_Programa.Y + 2;
                lineItem_Division_10_Referencia.Width = lineItem_Division_09_Programa.Width;
                lineItem_Division_10_Referencia.Height = 0.01;


                txtET_16_Referencia = new TextItem();
                txtET_16_Referencia.Font.Name = fuente.Name;
                txtET_16_Referencia.Font.Unit = fuente.Unit;
                txtET_16_Referencia.Font.Size = 8;
                txtET_16_Referencia.Font.Bold = true;
                txtET_16_Referencia.TextAlignment = TextAlignment.Center;
                txtET_16_Referencia.TextPadding = new FrameThickness(dPadding);
                txtET_16_Referencia.X = txtET_15_Programa.X;
                txtET_16_Referencia.Y = lineItem_Division_10_Referencia.Y + (dMargen * .5);
                txtET_16_Referencia.BorderThickness = new FrameThickness(0.0);  //dBorde
                txtET_16_Referencia.Width = txtET_15_Programa.Width;
                txtET_16_Referencia.Height = txtET_15_Programa.Height;
                txtET_16_Referencia.Text = info.Campo("TituloReferencia_ETQ");


                lineItem_Division_11_Referencia = new LineShapeItem();
                lineItem_Division_11_Referencia.Orientation = LineOrientation.Horizontal;
                lineItem_Division_11_Referencia.X = lineItem_Division_09_Programa.X;
                lineItem_Division_11_Referencia.Y = lineItem_Division_10_Referencia.Y + txtET_16_Referencia.Height + (dMargen * .5);
                lineItem_Division_11_Referencia.Width = lineItem_Division_09_Programa.Width;
                lineItem_Division_11_Referencia.Height = 0.01;



                txtET_15_Programa_Leyenda = new TextItem();
                txtET_15_Programa_Leyenda.Font.Name = fuente.Name;
                txtET_15_Programa_Leyenda.Font.Unit = fuente.Unit;
                txtET_15_Programa_Leyenda.Font.Size = 12;
                txtET_15_Programa_Leyenda.Font.Bold = true;
                txtET_15_Programa_Leyenda.TextAlignment = TextAlignment.Center;
                txtET_15_Programa_Leyenda.TextPadding = new FrameThickness(dPadding);
                txtET_15_Programa_Leyenda.X = txtET_15_Programa.X;
                txtET_15_Programa_Leyenda.Y = lineItem_Division_09_Programa.Y + (dMargen * .5);
                txtET_15_Programa_Leyenda.BorderThickness = new FrameThickness(0.0);  //dBorde
                txtET_15_Programa_Leyenda.Width = txtET_15_Programa.Width;
                txtET_15_Programa_Leyenda.Height = 2.0 - ( dMargen * 1 );
                txtET_15_Programa_Leyenda.Text = info.Campo("LeyendaPrograma");


                txtET_16_Referencia_Leyenda = new TextItem();
                txtET_16_Referencia_Leyenda.Font.Name = fuente.Name;
                txtET_16_Referencia_Leyenda.Font.Unit = fuente.Unit;
                txtET_16_Referencia_Leyenda.Font.Size = 16;
                txtET_16_Referencia_Leyenda.Font.Bold = true;
                txtET_16_Referencia_Leyenda.TextAlignment = TextAlignment.Center;
                txtET_16_Referencia_Leyenda.TextPadding = new FrameThickness(dPadding);
                txtET_16_Referencia_Leyenda.X = txtET_15_Programa.X;
                txtET_16_Referencia_Leyenda.Y = lineItem_Division_11_Referencia.Y + (dMargen * .5);
                txtET_16_Referencia_Leyenda.BorderThickness = new FrameThickness(0.0);  //dBorde
                txtET_16_Referencia_Leyenda.Width = txtET_15_Programa.Width;
                txtET_16_Referencia_Leyenda.Height = 2.5 - ( dMargen * 1 );
                txtET_16_Referencia_Leyenda.Text = info.Campo("LeyendaReferencia");

                //////////////////////////////////////////////////////////////////////////////////  



                ////////////////Add items to ThermalLabel object... 
                tLabel.Items.Add(rectItem);

                tLabel.Items.Add(txtEmpresa);
                tLabel.Items.Add(lineItem);

                tLabel.Items.Add(txtET_02_Origen);
                tLabel.Items.Add(txtET_03_Destino);
                tLabel.Items.Add(lineItem_Seccion_01);
                tLabel.Items.Add(txtET_04_FolioReferencia_Titulo);
                tLabel.Items.Add(lineItem_Seccion_02_FolioReferencia_Pie);
                tLabel.Items.Add(txtET_05_Barcode);
                tLabel.Items.Add(lineItem_Seccion_02_FolioReferencia);

                tLabel.Items.Add(lineItem_Division_01);
                tLabel.Items.Add(lineItem_Division_02);
                tLabel.Items.Add(lineItem_Division_03);
                tLabel.Items.Add(lineItem_Division_04);
                tLabel.Items.Add(lineItem_Division_05);
                tLabel.Items.Add(lineItem_Division_06_Vertical);

                tLabel.Items.Add(txtET_06_Surtio);
                tLabel.Items.Add(txtET_06_Surtio_Leyenda);
                tLabel.Items.Add(txtET_07_Verifico);
                tLabel.Items.Add(txtET_07_Verifico_Leyenda);
                tLabel.Items.Add(txtET_08_FechaEtiquetado);
                tLabel.Items.Add(txtET_08_FechaEtiquetado_Leyenda);
                tLabel.Items.Add(txtET_09_Antibioticos);
                tLabel.Items.Add(txtET_10_Controlados);
                tLabel.Items.Add(txtET_11_Refrigerados);
                tLabel.Items.Add(txtET_12_Leyenda_PNO);



                tLabel.Items.Add(lineItem_Division_07_NoEtiquetas);
                tLabel.Items.Add(txtET_13_EtiquetaCajas);
                tLabel.Items.Add(lineItem_Division_08_NoEtiquetas); 
                tLabel.Items.Add(txtET_14_EtiquetaCajas_Leyenda);


                tLabel.Items.Add(txtET_15_Programa); 
                tLabel.Items.Add(lineItem_Division_09_Programa);
                tLabel.Items.Add(txtET_15_Programa_Leyenda);

                tLabel.Items.Add(lineItem_Division_10_Referencia);
                tLabel.Items.Add(txtET_16_Referencia);
                tLabel.Items.Add(lineItem_Division_11_Referencia);
                tLabel.Items.Add(txtET_16_Referencia_Leyenda);


                ////////////////Add items to ThermalLabel object... 


                if( info.CampoBool("EtiquetadoManual") )
                {
                    //// Multiples etiquetas 
                    string sTextoEtiqueta = "";
                    iNumero_Etiqueta_Inicial = info.CampoInt("ETQ_Inicial"); 
                    iNumeroDeEtiquetas = info.CampoInt("ETQ_Final");
                    List<EtiquetaPedido> etiquetas = new List<EtiquetaPedido>();

                    for(int i = iNumero_Etiqueta_Inicial; i <= iNumeroDeEtiquetas; i++)
                    {
                        sTextoEtiqueta = string.Format("{0} DE {1}", i, iNumeroDeEtiquetas);
                        etiquetas.Add(new EtiquetaPedido(sTextoEtiqueta));
                    }

                    tLabel.DataSource = etiquetas;
                    //// Multiples etiquetas 
                }


                if(VistaPrevia)
                {
                    bGenerar = false; 
                    visor = new FrmVisorEtiquetas(tLabel);

                    if (FormaPadre != null)
                    {
                        visor.ShowInTaskbar = false; 
                        visor.ShowDialog(FormaPadre);
                    }
                    else
                    {
                        visor.ShowDialog();
                    }
                }
                else
                {
                    //_currentThermalLabel = tLabel;
                    Imprimir(tLabel, sImpresoraEtiquetas, 1);
                }
            }

            return bRegresa;
        }
        #endregion ETIQUETAS PEDIDOS
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Publicos
        public void Imprimir()
        {
            if (_currentThermalLabel != null)
            {
                PrintDialog printDialog = new PrintDialog();
                printDialog.UseEXDialog = true; 
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    Imprimir(_currentThermalLabel, printDialog.PrinterSettings.PrinterName, printDialog.PrinterSettings.Copies);
                }
            }
        }

        public void Imprimir(ThermalLabel Etiqueta, string Impresora, int Copias)
        {
            //Display Print Job dialog...           
            //PrintDialog printDialog = new PrintDialog();
            //if (printDialog.ShowDialog() == DialogResult.OK)

           // _printOrientation = PrintOrientation.Landscape90;


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

        public void Exportar_a_Pdf()
        {
            Exportar_a_Pdf(_currentThermalLabel, 1); 
        }

        public void Exportar_a_Pdf(ThermalLabel Etiqueta, int Copias)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Adobe PDF|*.pdf";

            if (Etiqueta != null)
            {
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //create a PrintJob object
                    using (PrintJob pj = new PrintJob())
                    {
                        pj.ThermalLabel = Etiqueta; // set the ThermalLabel object
                        pj.Copies = Copias;

                        pj.ExportToPdf(sfd.FileName, _dpi); //export to pdf
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
        }

        public void ExportToImage()
        {
            if (_currentThermalLabel != null)
            {
                ExportToImage(_currentThermalLabel, 1); 
            }
        }

        public void ExportToImage(ThermalLabel Etiqueta, int Copias)
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

            if (Etiqueta != null)
            {
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //create a PrintJob object
                    using (PrintJob pj = new PrintJob())
                    {
                        pj.ThermalLabel = Etiqueta; // set the ThermalLabel object
                        pj.Copies = Copias;

                        pj.ExportToImage(sfd.FileName, _imgSettings, _dpi); //export to image file

                        //Open folder where image file was created
                        System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(sfd.FileName));

                    }
                }
            }
        }
        #endregion Funciones y Procedimientos Publicos 
    }
}
