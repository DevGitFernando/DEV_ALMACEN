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
using MA_Facturacion.GenerarDocumentos;

namespace MA_Facturacion.GenerarRemisiones
{
    internal class clsRemision_GenerarDocumentos
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
        bool bGenerar_EXCEL = false;
        bool bGenerar_EXCEL_Contrarecibo = false;
        bool bGenerarDirectorio_Farmacia = false;

        string sFormato = "#######################0.#0";
        #endregion Declaracion de Variables

        #region Constructor de Clase
        public clsRemision_GenerarDocumentos()
        {
            dtMarcaTiempo = General.FechaSistema;
            sMarcaTiempo = string.Format("", dtMarcaTiempo.Year, dtMarcaTiempo.Month, dtMarcaTiempo.Day);
            DatosCliente = new clsDatosCliente(DtIFacturacion.DatosApp, "clsFacturas_GenerarDocumentos", ""); 

            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += @"\DOCUMENTOS_MEDIACCESS\";
            CrearDirectorio(sRutaDestino);

            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, "MA_Facturacion.GenerarDocumentos.clsFacturas_GenerarDocumentos");
        }

        ~clsRemision_GenerarDocumentos()
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


            if (bGenerar_EXCEL)
            {
                bRegresa = GenerarDocumentos_Excel(IdEmpresa, IdEstado, IdFarmaciaGenera, FolioFactura);
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

            string sNumeroDeRemision = "";
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

            string sSql = string.Format("Exec spp_INT_MA__FACT_GetInformacion_ExportarTXT_Remision " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', @FolioRemision = '{3}' ", 
                IdEmpresa, IdEstado, IdFarmaciaGenera, FolioFactura);


            CrearDirectorio(sRutaDestino_TXT);
            if (!leer.Exec(sSql))
            {
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
                    sNumeroDeRemision = leer_Informacion.Campo("FolioRemision");
                    sClaveProveedor_Farmacia = leer_Informacion.Campo("Proveedor_MA");
                    dtFechaFactura = leer_Informacion.CampoFecha("FechaRemision");
                    sFechaFactura = string.Format("{0}{1}{2}", Fg.PonCeros(dtFechaFactura.Day, 2), Fg.PonCeros(dtFechaFactura.Month, 2), Fg.PonCeros(dtFechaFactura.Year, 4));

                    ////sFechaFactura = string.Format("{0}_{1}{2}", 
                    ////    //Fg.PonCeros(dtFechaFactura.Year, 4), 
                    ////    dtFechaFactura.Day.ToString(), 
                    ////    //Fg.PonCeros(dtFechaFactura.Day, 2),
                    ////    Fg.PonCeros(dtFechaFactura.Hour, 2), Fg.PonCeros(dtFechaFactura.Minute, 2));


                    sFileName_Aux = sRutaDestino_TXT;
                    //sFileName = string.Format("REM{0}_TXT_{1}_{2}.txt", sNumeroDeRemision, sFechaFactura, sClaveProveedor_Farmacia);
                    sFileName = string.Format("{0}{1}R{2}.txt", sClaveProveedor_Farmacia, sFechaFactura, sNumeroDeRemision);
                    StreamWriter fileOut = new StreamWriter(Path.Combine(sFileName_Aux, sFileName));

                    leer_Informacion.RegistroActual = 1; 
                    while (leer_Informacion.Leer())
                    {
                        sNumeroDeRemision = "";
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

                        sNumeroDeRemision = leer_Informacion.Campo("FolioRemision");
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



                        sNumeroDeRemision = FormatoCampos.Formato_Caracter_Izquierda(sNumeroDeRemision, 20, " ");
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
                                sNumeroDeRemision, sClaveProveedor_Farmacia, sNumReceta, sElegibilidad, sFechaVenta,
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

        //private bool GenerarDocumentos_FACTURAS(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string FolioFactura)
        //{
        //    bool bRegresa = false;
        //    return bRegresa; 
        //}

        private bool GenerarDocumentos_Excel(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string FolioFactura)
        {
            bool bRegresa = false;
            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos
    }
}
