using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using DllFarmaciaSoft.Reporteador;

using Dll_MA_IFacturacion; 

namespace MA_Facturacion.GenerarDocumentos
{
    internal class clsFacturas_GenerarDocumentos
    {
        #region Declaracion de Variables
        string sGUID = "";
        Label lblFechaProcesando = null;

        basGenerales Fg = new basGenerales();
        DateTime dtMarcaTiempo = DateTime.Now;
        string sMarcaTiempo = "";
        string sFile_PDF = "";
        clsDatosCliente DatosCliente;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerInf;
        clsLeer leerExec;
        clsGrabarError Error;
        string sRutaDestino = "";
        clsExportarExcelPlantilla xpExcel;

        bool bGenerar_TXT = false;
        bool bGenerar_FACTURAS = false;
        bool bGenerar_EXCEL = false;
        bool bGenerar_EXCEL_Contrarecibo = false;
        bool bGenerarDirectorio_Farmacia = false;

        string sFormato = "#######################0.#0";
        #endregion Declaracion de Variables

        #region Constructor de Clase
        public clsFacturas_GenerarDocumentos()
        {
            dtMarcaTiempo = General.FechaSistema;
            sMarcaTiempo = string.Format("", dtMarcaTiempo.Year, dtMarcaTiempo.Month, dtMarcaTiempo.Day);
            DatosCliente = new clsDatosCliente(DtIFacturacion.DatosApp, "clsFacturas_GenerarDocumentos", ""); 

            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += @"\DOCUMENTOS_MEDIACCESS\";
            CrearDirectorio(sRutaDestino);

            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, "MA_Facturacion.GenerarDocumentos.clsFacturas_GenerarDocumentos");
        }

        ~clsFacturas_GenerarDocumentos()
        {
        }
        #endregion Constructor de Clase

        #region Propiedades
        public string GUID
        {
            get
            {
                if (sGUID == null) sGUID = "";
                return sGUID;
            }
            set { sGUID = value; }
        }

        public Label EtiquetaFechaEnProceso
        {
            set { lblFechaProcesando = value; }
        }

        public bool Generar_TXT
        {
            get { return bGenerar_TXT; }
            set { bGenerar_TXT = value; }
        }

        public bool Generar_FACTURAS
        {
            get { return bGenerar_FACTURAS; }
            set { bGenerar_FACTURAS = value; }
        }

        public bool Generar_EXCEL
        {
            get { return bGenerar_EXCEL; }
            set { bGenerar_EXCEL = value; }
        }

        public bool Generar_EXCEL_Contrarecibo
        {
            get { return bGenerar_EXCEL_Contrarecibo; }
            set { bGenerar_EXCEL_Contrarecibo = value; }
        }

        public bool GenerarDirectorio_Farmacia
        {
            get { return bGenerarDirectorio_Farmacia; }
            set { bGenerarDirectorio_Farmacia = value; }
        }

        public string RutaDestinoReportes
        {
            get { return sRutaDestino; }
            set
            {
                sRutaDestino = value;
                if (sRutaDestino != "")
                {
                    sRutaDestino += @"\";
                    CrearDirectorio(sRutaDestino);
                }
            }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        private void CrearDirectorio(string Directorio)
        {
            if (!Directory.Exists(Directorio))
            {
                Directory.CreateDirectory(Directorio);
            }
        }

        public void MsjFinalizado()
        {
            General.msjUser("Archivos de generados satisfactoriamente.");
        }

        public void AbrirDirectorioDeDocumentos()
        {
            if (General.msjConfirmar("¿ Desea abrir el directorio de archivos generados ?") == DialogResult.Yes)
            {
                General.AbrirDirectorio(sRutaDestino);
            }
        }
        #endregion Funciones y Procedimientos Publicos


        #region Funciones y Procedimientos Publicos
        public bool GenerarDocumentos(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string FolioFactura)
        {
            bool bRegresa = false;

            cnn = new clsConexionSQL(General.DatosConexion); 
            leer = new clsLeer(ref cnn);
            leerInf = new clsLeer();


            if (bGenerar_TXT)
            {
                bRegresa = GenerarDocumentos_TXT(IdEmpresa, IdEstado, IdFarmaciaGenera, FolioFactura); 
            }

            if (bGenerar_FACTURAS)
            {
                bRegresa = GenerarDocumentos_FACTURAS(IdEmpresa, IdEstado, IdFarmaciaGenera, FolioFactura);
            }

            if (bGenerar_EXCEL)
            {
                bRegresa = GenerarDocumentos_Excel(IdEmpresa, IdEstado, IdFarmaciaGenera, FolioFactura);
            }

            return bRegresa; 
        }

        public bool GenerarContrarecibo(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string FolioContrarecibo)
        {
            bool bRegresa = false;
            clsLeer leer_Informacion = new clsLeer();
            string sRutaDestino_TXT = sRutaDestino + @"\CONTRARECIBOS\";

            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\INT_MA__FACT_CONTRARECIBO.xls";
            bool bContinua = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "INT_MA__FACT_CONTRARECIBO.xls", DatosCliente);

            string sFileName = "";
            string sFileName_Aux = "";
            string sCadena = "";

            DateTime dtFechaFactura = DateTime.Now;
            DateTime dtFechaVenta = DateTime.Now;

            string sNumeroDeContrarecibo = "";
            string sClaveProveedor_Farmacia = "";
            string sFechaContrarecibo = "";
            string sFechaImpresion = ""; 

            int iRow = 9; 
            int iCol = 2; 


            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            leerInf = new clsLeer();

            string sSql = string.Format("Exec spp_INT_MA__FACT_GetInformacion_ExportarExcel_Contrarecibo " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', @FolioContraRecibo = '{3}' ",
                IdEmpresa, IdEstado, IdFarmaciaGenera, FolioContrarecibo);


            if (!bContinua)
            {
                General.msjError(" No fue posible generar la plantilla solicitada.");
            }
            else
            {
                CrearDirectorio(sRutaDestino_TXT);
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "GenerarContrarecibo");
                    General.msjError("Ocurrió un error al obtener la información del Contrarecibo solicitado.");
                }
                else
                {
                    if (!leer.Leer())
                    {
                        General.msjUser("No se encontro información del Contrarecibo solicitado.");
                    }
                    else
                    {
                        leer_Informacion.DataSetClase = leer.DataSetClase;
                        leer_Informacion.Leer();
                        sNumeroDeContrarecibo = leer_Informacion.Campo("FolioContrarecibo");

                        dtFechaFactura = leer_Informacion.CampoFecha("FechaContrarecibo");
                        sFechaContrarecibo = string.Format("{0}{1}{2}", Fg.PonCeros(dtFechaFactura.Day, 2), Fg.PonCeros(dtFechaFactura.Month, 2), Fg.PonCeros(dtFechaFactura.Year, 4));

                        iRow = 2;


                        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                        xpExcel.AgregarMarcaDeTiempo = false;

                        if (xpExcel.PrepararPlantilla(sRutaDestino_TXT, string.Format(@"Contrarecibo__{0}.xls", FolioContrarecibo)))
                        {
                            leer_Informacion.RegistroActual = 1;

                            xpExcel.GeneraExcel(1, true);
                            xpExcel.NumeroDeRenglonesAProcesar = leer_Informacion.Registros > 0 ? leer_Informacion.Registros - 1 : 0;
                            

                            iRow = 2;
                            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
                            xpExcel.Agregar(string.Format("RELACIÓN DE FACTURAS EN CONTRARECIBO NÚMERO : {0}", FolioContrarecibo), 3, 2);
                            sFechaImpresion = string.Format("Fecha de Impresión: {0} ", General.FechaSistema);
                            xpExcel.Agregar(sFechaImpresion, 5, 2);


                            iRow = 8;
                            while (leer_Informacion.Leer())
                            {
                                iCol = 2;
                                xpExcel.Agregar(leer_Informacion.Campo("Proveedor_MA"), iRow, iCol++);
                                xpExcel.Agregar(leer_Informacion.Campo("Farmacia"), iRow, iCol++);
                                xpExcel.Agregar(leer_Informacion.Campo("FechaContrarecibo"), iRow, iCol++);
                                xpExcel.Agregar(leer_Informacion.Campo("FolioContrarecibo"), iRow, iCol++);
                                xpExcel.Agregar(leer_Informacion.Campo("FolioFactura"), iRow, iCol++);
                                xpExcel.Agregar(leer_Informacion.CampoDouble("SubTotal"), iRow, iCol++);
                                xpExcel.Agregar(leer_Informacion.CampoDouble("Iva"), iRow, iCol++);
                                xpExcel.Agregar(leer_Informacion.CampoDouble("Importe"), iRow, iCol++);

                                xpExcel.Agregar(leer_Informacion.Campo("Programa"), iRow, iCol++);
                                xpExcel.Agregar(leer_Informacion.Campo("Estado_Factura"), iRow, iCol++);

                                iRow++;
                                xpExcel.NumeroRenglonesProcesados++;
                            }

                            xpExcel.CerrarDocumento(); 
                            bRegresa = true;
                        }

                    }
                }
            }


            return bRegresa; 
        }

        private bool GenerarDocumentos_TXT(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string FolioFactura)
        {
            bool bRegresa = false;
            clsLeer leer_Informacion = new clsLeer(); 
            string sRutaDestino_TXT = sRutaDestino + @"\TXT\";

            string sFileName = "";
            string sFileName_Aux = "";
            string sCadena = "";

            DateTime dtFechaFactura = DateTime.Now;
            DateTime dtFechaVenta = DateTime.Now;

            string sNumeroDeFactura = "";
            string sClaveProveedor_Farmacia = "";
            string sNumReceta = "";
            string sElegibilidad = "";
            string sFechaFactura = ""; 
            string sFechaVenta = "";
            string sCodigoEAN = "";
            string sCantidad = "";
            string sImporte = "";
            string sIVA = "";
            string sTotal = "";
            string sDescuento = "";
            string sCIE10_01 = "";
            string sCIE10_02 = "";
            string sCIE10_03 = "";
            string sCIE10_04 = ""; 

            string sSql = string.Format("Exec spp_INT_MA__FACT_GetInformacion_ExportarTXT_Factura " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', @FolioFactura = '{3}' ", 
                IdEmpresa, IdEstado, IdFarmaciaGenera, FolioFactura);


            CrearDirectorio(sRutaDestino_TXT);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GenerarDocumentos_TXT"); 
            }
            else
            {
                if (!leer.Leer())
                {
                }
                else
                {
                    leer_Informacion.DataSetClase = leer.DataSetClase;
                    leer_Informacion.Leer();
                    sNumeroDeFactura = leer_Informacion.Campo("FolioFactura");
                    sClaveProveedor_Farmacia = leer_Informacion.Campo("Proveedor_MA");
                    dtFechaFactura = leer_Informacion.CampoFecha("FechaFactura");
                    sFechaFactura = string.Format("{0}{1}{2}", Fg.PonCeros(dtFechaFactura.Day, 2), Fg.PonCeros(dtFechaFactura.Month, 2), Fg.PonCeros(dtFechaFactura.Year, 4));

                    ////sFechaFactura = string.Format("{0}_{1}{2}", 
                    ////    //Fg.PonCeros(dtFechaFactura.Year, 4), 
                    ////    dtFechaFactura.Day.ToString(), 
                    ////    //Fg.PonCeros(dtFechaFactura.Day, 2),
                    ////    Fg.PonCeros(dtFechaFactura.Hour, 2), Fg.PonCeros(dtFechaFactura.Minute, 2));


                    sFileName_Aux = sRutaDestino_TXT;
                    //sFileName = string.Format("{0}_TXT_{1}_{2}.txt", sNumeroDeFactura, sFechaFactura, sClaveProveedor_Farmacia);
                    sFileName = string.Format("{0}{1}{2}.txt", sClaveProveedor_Farmacia, sFechaFactura, sNumeroDeFactura);

                    StreamWriter fileOut = new StreamWriter(Path.Combine(sFileName_Aux, sFileName));

                    leer_Informacion.RegistroActual = 1; 
                    while (leer_Informacion.Leer())
                    {
                        sNumeroDeFactura = "";
                        sClaveProveedor_Farmacia = "";
                        sNumReceta = "";
                        sElegibilidad = "";
                        sFechaFactura = ""; 
                        sFechaVenta = "";
                        sCodigoEAN = "";
                        sCantidad = "";
                        sImporte = "";
                        sIVA = "";
                        sTotal = "";
                        sDescuento = "";
                        sCIE10_01 = "";
                        sCIE10_02 = "";
                        sCIE10_03 = "";
                        sCIE10_04 = ""; 

                        sNumeroDeFactura = leer_Informacion.Campo("FolioFactura");
                        sClaveProveedor_Farmacia = leer_Informacion.Campo("Proveedor_MA");
                        sNumReceta = leer_Informacion.Campo("NumeroReceta");
                        sElegibilidad = leer_Informacion.Campo("Elegibilidad");

                        sFechaVenta = Fg.Right(leer_Informacion.Campo("FechaVenta"), 8);
                        sCodigoEAN = leer_Informacion.Campo("CodigoEAN");
                        sCantidad = leer_Informacion.CampoInt("Cantidad").ToString();

                        sImporte = leer_Informacion.CampoDouble("SubTotal").ToString(sFormato);
                        sIVA = leer_Informacion.CampoDouble("Iva").ToString(sFormato);
                        sTotal = leer_Informacion.CampoDouble("Importe").ToString(sFormato);
                        sDescuento = leer_Informacion.CampoDouble("ImporteDescuento").ToString(sFormato);

                        sCIE10_01 = leer_Informacion.Campo("CIE10_01");
                        sCIE10_02 = leer_Informacion.Campo("CIE10_02"); ;
                        sCIE10_03 = leer_Informacion.Campo("CIE10_03"); ;
                        sCIE10_04 = leer_Informacion.Campo("CIE10_04"); ;



                        //////sNumeroDeFactura = FormatoCampos.Formato_Caracter_Derecha(sNumeroDeFactura, 20, " ");
                        //////sClaveProveedor_Farmacia = FormatoCampos.Formato_Caracter_Derecha(sClaveProveedor_Farmacia, 10, " ");
                        //////sNumReceta = FormatoCampos.Formato_Caracter_Derecha(sNumReceta, 20, " ");
                        //////sElegibilidad = FormatoCampos.Formato_Caracter_Derecha(sElegibilidad, 20, " ");

                        //////dtFechaVenta = leer_Informacion.CampoFecha("FechaVenta");
                        //////sFechaVenta = string.Format("{0}/{1}/{2}", Fg.PonCeros(dtFechaVenta.Day, 2), Fg.PonCeros(dtFechaVenta.Month, 2), Fg.PonCeros(dtFechaVenta.Year, 4));

                        //////sCodigoEAN = FormatoCampos.Formato_Caracter_Derecha(sCodigoEAN, 20, " ");
                        //////sCantidad = FormatoCampos.Formato_Digitos_Derecha(sCantidad, 5, " ");
                        //////sImporte = FormatoCampos.Formato_Digitos_Derecha(sImporte, 15, " ");
                        //////sIVA = FormatoCampos.Formato_Digitos_Derecha(sIVA, 15, " ");
                        //////sTotal = FormatoCampos.Formato_Digitos_Derecha(sTotal, 15, " ");
                        //////sDescuento = FormatoCampos.Formato_Digitos_Derecha(sDescuento, 10, " ");



                        sNumeroDeFactura = FormatoCampos.Formato_Caracter_Izquierda(sNumeroDeFactura, 20, " ");
                        sClaveProveedor_Farmacia = FormatoCampos.Formato_Caracter_Izquierda(sClaveProveedor_Farmacia, 10, " ");
                        sNumReceta = FormatoCampos.Formato_Caracter_Izquierda(sNumReceta, 20, " ");
                        sElegibilidad = FormatoCampos.Formato_Caracter_Izquierda(sElegibilidad, 20, " ");

                        dtFechaVenta = leer_Informacion.CampoFecha("FechaVenta");
                        sFechaVenta = string.Format("{0}/{1}/{2}", Fg.PonCeros(dtFechaVenta.Day, 2), Fg.PonCeros(dtFechaVenta.Month, 2), Fg.PonCeros(dtFechaVenta.Year, 4));

                        sCodigoEAN = FormatoCampos.Formato_Caracter_Izquierda(sCodigoEAN, 20, " ");
                        sCantidad = FormatoCampos.Formato_Caracter_Izquierda(sCantidad, 5, " ");
                        sImporte = FormatoCampos.Formato_Caracter_Izquierda(sImporte, 15, " ");
                        sIVA = FormatoCampos.Formato_Caracter_Izquierda(sIVA, 15, " ");
                        sTotal = FormatoCampos.Formato_Caracter_Izquierda(sTotal, 15, " ");
                        sDescuento = FormatoCampos.Formato_Caracter_Izquierda(sDescuento, 10, " ");

                        sCIE10_01 = FormatoCampos.Formato_Caracter_Izquierda(sCIE10_01, 5, " ");
                        sCIE10_02 = FormatoCampos.Formato_Caracter_Izquierda(sCIE10_02, 5, " ");
                        sCIE10_03 = FormatoCampos.Formato_Caracter_Izquierda(sCIE10_03, 5, " ");
                        sCIE10_04 = FormatoCampos.Formato_Caracter_Izquierda(sCIE10_04, 5, " ");

                        // "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}" 
                        sCadena = string.Format
                            (
                                "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}",
                                sNumeroDeFactura, sClaveProveedor_Farmacia, sNumReceta, sElegibilidad, sFechaVenta,
                                sCodigoEAN, sCantidad, sImporte, sIVA, sTotal, sDescuento, sCIE10_01, sCIE10_02, sCIE10_03, sCIE10_04
                            );
                        fileOut.WriteLine(sCadena);
                    }
                    fileOut.Close();
                    bRegresa = true;
                }
            }

            return bRegresa; 
        }

        private bool GenerarDocumentos_FACTURAS(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string FolioFactura)
        {
            bool bRegresa = false;
            return bRegresa; 
        }

        private bool GenerarDocumentos_Excel(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string FolioFactura)
        {
            bool bRegresa = false;
            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos
    }
}
