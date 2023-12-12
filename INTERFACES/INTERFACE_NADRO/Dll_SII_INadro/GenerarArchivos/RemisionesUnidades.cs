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

using Dll_SII_INadro;

namespace Dll_SII_INadro.GenerarArchivos
{
    public class RemisionesUnidades
    {
        #region Declaracion de Variables
        string sPrefijo = "RS";
        string sGUID = "";
        Label lblFechaProcesando = null; 

        basGenerales Fg = new basGenerales();
        string sClaveCliente = "";
        DataSet dtsPedido;
        DateTime dtMarcaTiempo = DateTime.Now;
        string sMarcaTiempo = "";
        int iEsDeSurtimiento = 0;
        string sFile_PDF = "";
        clsDatosCliente DatosCliente;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerInf;
        clsLeer leerExec;
        clsGrabarError Error;
        string sRutaDestino = "";
        clsExportarExcelPlantilla xpExcel;


        string sIdCliente = "";
        string sIdSubCliente = ""; 
        string sIdPrograma = "";
        string sIdSubPrograma = "";
        string sSubFarmacias = "";

        int iTipoDeDispensacion = 0;
        int iTipoDeInsumo = 0;
        int iTipoDeDocumento_A_Generar = 0;
        int iCauses = 2012; 
        bool bGenerarDirectorio_Farmacia = false; 

        bool bGenerarDocto_Excel = false;
        bool bGenerarDocto_Pdf = false;
        bool bGenerarDocto_Pdf_Concentrado = false;
        bool bGenerarDocto_Todos = true;

        bool bGenerarDoctos_EnResguardo = false;
        bool bSecuenciarDocumentos = false;

        bool bProcesar_x_Dia = false;
        bool bGenerarHistorico = false;
        bool bSeparar_x_Causes = false;
        bool bConsolidarMeses = false; 

        string sFormato = "#######################0.#0";
        #endregion Declaracion de Variables

        #region Constructor de Clase
        public RemisionesUnidades()
        {
            dtMarcaTiempo = General.FechaSistema;
            sMarcaTiempo = string.Format("", dtMarcaTiempo.Year, dtMarcaTiempo.Month, dtMarcaTiempo.Day);

            DatosCliente = new clsDatosCliente(GnDll_SII_INadro.DatosApp, "RemisionesUnidades()", "GenerarPDF()");

            leer = new clsLeer(ref cnn);
            leerInf = new clsLeer(ref cnn);
            leerExec = new clsLeer(ref cnn);


            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += @"\DOCUMENTOS_NADRO\REMISIONES\";
            CrearDirectorio(sRutaDestino); 

            Error = new clsGrabarError(General.DatosConexion, GnDll_SII_INadro.DatosApp, "Dll_SII_INadro.GenerarArchivos.RemisionesUnidades");
        }

        ~RemisionesUnidades()
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

        public string RutaDestinoReportes
        {
            get { return sRutaDestino; }
            set
            {
                sRutaDestino = value;
                if (sRutaDestino != "")
                {
                    sRutaDestino += @"\REMISIONES_IMPRESIONES\";
                    CrearDirectorio(sRutaDestino);
                }
            }
        }

        public int Causes
        {
            get { return iCauses; }
            set { iCauses = value; }
        }

        public string IdCliente 
        {
            get { return sIdCliente; }
            set { sIdCliente = value; }
        }

        public string IdSubCliente
        {
            get { return sIdSubCliente; }
            set { sIdSubCliente = value; }
        }

        public string IdPrograma
        {
            get { return sIdPrograma; }
            set { sIdPrograma = value; }
        }

        public string IdSubPrograma
        {
            get { return sIdSubPrograma; }
            set { sIdSubPrograma = value; }
        }

        public int TipoDeDispensacion
        {
            get { return iTipoDeDispensacion; }
            set { iTipoDeDispensacion = value; }
        }

        public string SubFarmacias
        {
            get { return sSubFarmacias; }
            set { sSubFarmacias = value; }
        }

        public int TipoDeInsumo
        {
            get { return iTipoDeInsumo; }
            set { iTipoDeInsumo = value; }
        }

        public int TipoDeDocumento_A_Generar
        {
            get { return iTipoDeDocumento_A_Generar; }
            set { iTipoDeDocumento_A_Generar = value;  } 
        }

        public bool Pdf_Concentrado
        {
            get { return bGenerarDocto_Pdf_Concentrado; }
            set { bGenerarDocto_Pdf_Concentrado = value; } 
        }

        public bool GenerarDirectorio_Farmacia
        {
            get { return bGenerarDirectorio_Farmacia; }
            set { bGenerarDirectorio_Farmacia = value; }
        }

        public bool GenerarDoctos_EnResguardo
        {
            get { return bGenerarDoctos_EnResguardo; }
            set { bGenerarDoctos_EnResguardo = value; }
        }

        public bool SecuenciarDocumentos
        {
            get { return bSecuenciarDocumentos; }
            set { bSecuenciarDocumentos = value; }
        }

        public bool Procesar_x_Dia
        {
            get { return bProcesar_x_Dia; }
            set { bProcesar_x_Dia = value; }
        }

        public bool GenerarHistorico
        {
            get { return bGenerarHistorico; }
            set { bGenerarHistorico = value; }
        }

        public bool Separar_x_Causes
        {
            get { return bSeparar_x_Causes; }
            set { bSeparar_x_Causes = value; }
        }

        public bool ConsolidarMeses
        {
            get { return bConsolidarMeses; }
            set { bConsolidarMeses = value; }
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
            General.msjUser("Archivos de remisión generados satisfactoriamente.");
        }

        public void AbrirDirectorioRemisiones()
        {
            if (General.msjConfirmar("¿ Desea abrir el directorio de las remisiones generadas ?") == DialogResult.Yes)
            {
                General.AbrirDirectorio(sRutaDestino); 
            }
        }

        public bool GenerarRemisiones(string IdFarmacia, string Cliente, DateTime FechaInicial, DateTime FechaFinal)
        {
            bool bRegresa = false;

            bGenerarDocto_Excel = iTipoDeDocumento_A_Generar == 2;
            bGenerarDocto_Pdf = iTipoDeDocumento_A_Generar == 1;
            bGenerarDocto_Todos = iTipoDeDocumento_A_Generar == 0;

            if (bGenerarDocto_Todos)
            {
                bGenerarDocto_Excel = true;
                bGenerarDocto_Pdf = true;
            }

            if (bGenerarDirectorio_Farmacia)
            {
                sRutaDestino = sRutaDestino + @"\" + IdFarmacia + @"\";
                CrearDirectorio(sRutaDestino);
            }


            if (bGenerarHistorico)
            {
                bRegresa = GenerarRemisiones__Historico(IdFarmacia, Cliente, FechaInicial, FechaFinal);
            }
            else 
            {
                bRegresa = GenerarRemisiones__Ejecucion(IdFarmacia, Cliente, FechaInicial, FechaFinal);
            }

            return bRegresa; 
        }

        private bool GenerarRemisiones__Historico(string IdFarmacia, string Cliente, DateTime FechaInicial, DateTime FechaFinal)
        {
            bool bRegresa = true;
            string sFecha = ""; //  General.FechaYMD(FechaAProcesar); 
            string sFechaFinal = "";
            DateTime dtpFecha = FechaInicial;
            DateTime dtpFecha_Aux = FechaInicial;
            DateTime dtpFecha_Final_Aux = FechaInicial;

            while (dtpFecha <= FechaFinal)
            {
                sFecha = General.FechaYMD(dtpFecha);
                sFechaFinal = sFecha;

                if (bConsolidarMeses)
                {
                    dtpFecha_Aux = new DateTime(dtpFecha.Year, dtpFecha.Month, 1);
                    dtpFecha_Final_Aux = new DateTime(dtpFecha.Year, dtpFecha.Month, General.FechaDiasMes(dtpFecha));

                    sFecha = General.FechaYMD(dtpFecha_Aux);
                    sFechaFinal = General.FechaYMD(dtpFecha_Final_Aux); 
                } 

                if (lblFechaProcesando != null)
                {
                    lblFechaProcesando.Text = sFecha;
                    if (bConsolidarMeses)
                    {
                        lblFechaProcesando.Text = string.Format("{0}", General.FechaNombreMes(dtpFecha_Aux));
                    }
                }


                bRegresa = GenerarRemisiones(Cliente, sFecha, sFechaFinal); 


                if (bConsolidarMeses)
                {
                    dtpFecha = dtpFecha_Final_Aux.AddDays(1);
                }
                else
                {
                    dtpFecha = dtpFecha.AddDays(1);
                }
            }

            return bRegresa; 
        }

        private bool GenerarRemisiones__Ejecucion(string IdFarmacia, string Cliente, DateTime FechaInicial, DateTime FechaFinal)
        {
            bool bRegresa = false;
            sFile_PDF = sRutaDestino + @"\" + string.Format("{0}__{1}___DEL_{2}_AL_{3}.pdf", "REMISION", 
                Cliente, General.FechaYMD(FechaInicial, ""), General.FechaYMD(FechaFinal, ""));


            ////bGenerarDocto_Excel = iTipoDeDocumento_A_Generar == 2;
            ////bGenerarDocto_Pdf = iTipoDeDocumento_A_Generar == 1;
            ////bGenerarDocto_Todos = iTipoDeDocumento_A_Generar == 0;

            ////if (bGenerarDocto_Todos)
            ////{
            ////    bGenerarDocto_Excel = true;
            ////    bGenerarDocto_Pdf = true; 
            ////}

            if (lblFechaProcesando != null)
            {
                lblFechaProcesando.Text = "Consultando";
            }

            if (sGUID == null) sGUID = ""; 
            if (sGUID == "")
            {
                General.msjUser("No se encontró el Identificador Unico del Proceso.");
            }
            else
            {
                if (bProcesar_x_Dia)
                {
                    //////// Cambio de proceso 
                    string sFecha = ""; //  General.FechaYMD(FechaAProcesar); 
                    DateTime dtpFecha = FechaInicial;

                    while (dtpFecha <= FechaFinal)
                    {
                        sFecha = General.FechaYMD(dtpFecha);

                        if (lblFechaProcesando != null)
                        {
                            lblFechaProcesando.Text = sFecha;
                        }

                        ////bRegresa = GenerarRemisiones(Cliente, sFecha); 
                        if (GenerarConcentradoDeInformacion(IdFarmacia, dtpFecha, dtpFecha))
                        {
                            if (GenerarRemisiones_PrepararInformacion(IdFarmacia, Cliente))
                            {
                                bRegresa = GenerarRemisiones(Cliente, dtpFecha, dtpFecha);
                            }
                        }

                        dtpFecha = dtpFecha.AddDays(1);
                    }
                    //////// Cambio de proceso 
                }
                else
                {
                    if (GenerarConcentradoDeInformacion(IdFarmacia, FechaInicial, FechaFinal))
                    {
                        bRegresa = GenerarRemisiones_PrepararInformacion(IdFarmacia, Cliente);
                    }

                    if (bRegresa)
                    {
                        bRegresa = GenerarRemisiones(Cliente, FechaInicial, FechaFinal);
                        //////bRegresa = GenerarPDF();
                    }
                }

            }

            if (!bRegresa)
            {
                ////General.msjError("No fue posible generar las remisiones del periodo solicitado."); 
            }
            else 
            {
                if (!bGenerarDirectorio_Farmacia)
                {
                    this.MsjFinalizado();
                    this.AbrirDirectorioRemisiones();
                }
            }

            return bRegresa; 
        }

        private bool GenerarConcentradoDeInformacion(string IdFarmacia, DateTime FechaInicial, DateTime FechaFinal)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_Rpt_Administrativos " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FechaInicial = '{3}', @FechaFinal = '{4}', " + 
                " @IdCliente = '{5}', @IdSubCliente = '{6}',  @IdPrograma = '{7}', @IdSubPrograma = '{8}', @TipoDispensacion = '{9}', " + 
                " @TipoInsumo = '{10}', @TipoInsumoMedicamento = '0', @SubFarmacias = '{11}',  @MostrarPrecios = 0 ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, IdFarmacia, 
                General.FechaYMD(FechaInicial), General.FechaYMD(FechaFinal), 
                sIdCliente, sIdSubCliente, sIdPrograma, sIdSubPrograma, iTipoDeDispensacion, iTipoDeInsumo, sSubFarmacias);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GenerarConcentradoDeInformacion()"); 
            }
            else
            {
                bRegresa = true;
            }

            return bRegresa; 
        }

        private bool GenerarRemisiones_PrepararInformacion(string IdFarmacia, string Cliente)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_INT_ND_Unidad_GenerarRemisiones_PrepararInformacion " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @CodigoCliente = '{3}', @GUID = '{4}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, IdFarmacia, Cliente, sGUID );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GenerarRemisiones_PrepararInformacion()");
            }
            else
            {
                bRegresa = true;  
            }

            return bRegresa;
        }

        private bool GenerarRemisiones(string Cliente, DateTime FechaInicial, DateTime FechaFinal)
        {
            bool bRegresa = false;
            string sFecha = ""; //  General.FechaYMD(FechaAProcesar); 
            DateTime dtpFecha = FechaInicial;

            while (dtpFecha <= FechaFinal)
            {
                sFecha = General.FechaYMD(dtpFecha);

                if (lblFechaProcesando != null)
                {
                    lblFechaProcesando.Text = sFecha;
                }

                bRegresa = GenerarRemisiones(Cliente, sFecha, sFecha);
                dtpFecha = dtpFecha.AddDays(1);
            }

            return bRegresa;
        }

        private bool GenerarRemisiones(string Cliente, string FechaAProcesar_Inicial, string FechaAProcesar_Final)
        {
            bool bRegresa = false;
            int iTipoExec = bGenerarHistorico ? 2 : 1;
            int iSepararCauses = bSeparar_x_Causes ? 1 : 0;

            string sSql = string.Format("Exec spp_INT_ND_Unidad_GenerarRemisiones " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @CodigoCliente = '{2}', @FechaDeProceso_Inicial = '{3}', @FechaDeProceso_Final = '{4}', " + 
                " @GUID = '{5}', @Año_Causes = '{6}', @TipoDeProceso = '{7}', @SepararCauses = '{8}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, Cliente, FechaAProcesar_Inicial, FechaAProcesar_Final, sGUID, iCauses, iTipoExec, iSepararCauses);

            leer = new clsLeer(ref cnn);
            leerInf = new clsLeer();


            ////if (!cnn.Abrir())
            ////{
            ////    General.msjErrorAlAbriConexion();
            ////}
            ////else
            {
                ////cnn.IniciarTransaccion();
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "GenerarRemisiones()");
                }
                else
                {
                    leerInf.DataTableClase = leer.Tabla(1);
                    if (leerInf.Registros > 0)
                    {
                        leerInf.DataSetClase = leer.DataSetClase;
                        bRegresa = GenerarDocumentos(leerInf.DataSetClase, Cliente, FechaAProcesar_Inicial, FechaAProcesar_Final);
                    }
                }

                ////if (!bRegresa)
                ////{
                ////    General.msjError("Ocurrió un error al generar la remisión");
                ////    ////cnn.DeshacerTransaccion(); 
                ////}
                ////else
                ////{
                ////    ////cnn.CompletarTransaccion();
                ////    ////General.msjUser("Archivo de pedido generado satisfactoriamente.");
                ////}

                ////cnn.Cerrar();
            }
            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private bool GenerarDocumentos(DataSet DatosExportar, string Cliente, string FechaAProcesar, string FechaAProcesar_Final)
        {
            bool bRegresa = true;
            string sFolioRemision = ""; 
            clsLeer leerResultado = new clsLeer(); 
            clsLeer leerListaRemisiones = new clsLeer();
            clsLeer leerRemisiones = new clsLeer();

            leerResultado.DataSetClase = DatosExportar;


            if (bGenerarDocto_Pdf && bGenerarDocto_Pdf_Concentrado)
            {
                //////  Proceso especial 
                leerListaRemisiones.DataTableClase = leerResultado.Tabla(2);
                leerRemisiones.DataTableClase = leerResultado.Tabla(3);
                leerResultado.DataSetClase = leerRemisiones.DataSetClase; 
                while (leerListaRemisiones.Leer())
                {
                    sGUID = leerListaRemisiones.Campo("GUID");
                    if (leerListaRemisiones.Campo("TipoDispensacion").ToUpper() == "R")
                    {
                        bRegresa = GenerarExcel__Receta(leerRemisiones.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, 1);
                        bRegresa = GenerarExcel__Receta(leerRemisiones.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, 0);
                    }
                    else
                    {
                        bRegresa = GenerarExcel__Colectivo(leerRemisiones.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, 1);
                        bRegresa = GenerarExcel__Colectivo(leerRemisiones.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, 0);
                    }
                }
            }


            if (!bGenerarDocto_Pdf_Concentrado)
            {
                leerListaRemisiones.DataTableClase = leerResultado.Tabla(1);
                leerRemisiones.DataTableClase = leerResultado.Tabla(3);
                leerResultado.DataSetClase = leerRemisiones.DataSetClase;
                while (leerListaRemisiones.Leer())
                {
                    sGUID = leerListaRemisiones.Campo("GUID");
                    sFolioRemision = leerListaRemisiones.Campo("FolioRemision");

                    leerRemisiones.DataRowsClase = leerResultado.DataTableClase.Select(string.Format(" FolioRemision = '{0}' ", sFolioRemision));
                    leerRemisiones.Leer();

                    if (leerRemisiones.Registros > 0)
                    {
                        if (leerRemisiones.Campo("TipoDispensacion").ToUpper() == "R")
                        {
                            bRegresa = GenerarExcel__Receta(leerRemisiones.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, 1);
                            bRegresa = GenerarExcel__Receta(leerRemisiones.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, 0);
                        }
                        else
                        {
                            bRegresa = GenerarExcel__Colectivo(leerRemisiones.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, 1);
                            bRegresa = GenerarExcel__Colectivo(leerRemisiones.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, 0);
                        }
                    }
                }
            }

            return bRegresa; 
        }

        private bool GenerarExcel__Receta(DataSet DatosExportar, string Cliente, string FechaAProcesar, string FechaAProcesar_Final, int EsCauses)
        {
            bool bRegresa = true;
            clsLeer generarExcel = new clsLeer();
            clsLeer leerFiltro = new clsLeer();
            string sFiltro = "";
            string sFiltro_Fecha = "";  

            leerFiltro.DataSetClase = DatosExportar;

            sFiltro_Fecha = string.Format(" FechaRegistro = '{0}' ", FechaAProcesar); 
            if (bConsolidarMeses)
            {
                sFiltro_Fecha = string.Format(" FechaRegistro >= '{0}' and FechaRegistro <= '{1}' ", FechaAProcesar, FechaAProcesar_Final); 
            }


            ////////////////////////////  Venta  
            sFiltro = string.Format(" {0} and TipoDispensacion = '{1}' and TipoInsumo = '{2}' and EsEnResguardo = '{3}' and EsCauses = '{4}' ",
                sFiltro_Fecha, "R", 1, 0, EsCauses); 
            generarExcel.DataRowsClase = leerFiltro.DataTableClase.Select(sFiltro);
            if (generarExcel.Leer())
            {
                bRegresa = GenerarExcel__Receta(generarExcel.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, "M", 0, EsCauses);
            }

            sFiltro = string.Format(" {0} and TipoDispensacion = '{1}' and TipoInsumo = '{2}' and EsEnResguardo = '{3}' and EsCauses = '{4}'  ",
                sFiltro_Fecha, "R", 2, 0, EsCauses); 
            generarExcel.DataRowsClase = leerFiltro.DataTableClase.Select(sFiltro);
            if (generarExcel.Leer())
            {
                bRegresa = GenerarExcel__Receta(generarExcel.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, "MC", 0, EsCauses);
            }

            if (bGenerarDoctos_EnResguardo)
            {
                ////////////////////////////  Consignacion 
                sFiltro = string.Format(" {0} and TipoDispensacion = '{1}' and TipoInsumo = '{2}' and EsEnResguardo = '{3}' and EsCauses = '{4}' ",
                    sFiltro_Fecha, "R", 1, 1, EsCauses);
                generarExcel.DataRowsClase = leerFiltro.DataTableClase.Select(sFiltro);
                if (generarExcel.Leer())
                {
                    bRegresa = GenerarExcel__Receta(generarExcel.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, "M", 1, EsCauses);
                }

                sFiltro = string.Format(" {0} and TipoDispensacion = '{1}' and TipoInsumo = '{2}' and EsEnResguardo = '{3}' and EsCauses = '{4}' ",
                    sFiltro_Fecha, "R", 2, 1, EsCauses);
                generarExcel.DataRowsClase = leerFiltro.DataTableClase.Select(sFiltro);
                if (generarExcel.Leer())
                {
                    bRegresa = GenerarExcel__Receta(generarExcel.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, "MC", 1, EsCauses);
                }
            }


            return bRegresa;
        }

        private bool GenerarExcel__Receta(DataSet DatosExportar, string Cliente, string FechaAProcesar, string FechaAProcesar_Final, string TipoInsumo, int EsEnResguardo, int EsCauses)
        {
            bool bRegresa = false; 
            clsLeer generarExcel = new clsLeer();

            generarExcel.DataSetClase = DatosExportar; 
            if (generarExcel.Leer())
            {
                bRegresa = GenerarExcel__RecetaProcesar(generarExcel.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, TipoInsumo, EsEnResguardo, EsCauses); 
            }

            return bRegresa; 
        }

        private bool GenerarExcel__RecetaProcesar(DataSet DatosExportar, string Cliente, string FechaAProcesar, string FechaAProcesar_Final, string TipoInsumo, int EsEnResguardo, int EsCauses)
        {
            bool bRegresa = false;
            string sFile = "";
            string sFile_Auxiliar = "";
            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\INT_ND_Remisiones_Unidad.xlsx";
            int iRow = 10;
            int iCol = 2;
            string sFolioRemision = "";
            string sFolioRemision_Aux = ""; 
            string sAnexo = "";
            string sTipo = TipoInsumo == "M" ? "MEDICAMENTO" : "MATERIAL_CURACION";
            string sTipo_Titulo = TipoInsumo == "M" ? "MEDICAMENTO" : "MATERIAL DE CURACIÓN";
            string sTipoInventario = EsEnResguardo == 0 ? "" : "__EnResguardo";
            string sTipoDeInsumo = "";
            double dSubTotal = 0;
            double dIva = 0;
            double dTotal = 0;
            double dCantidad = 0;
            string sFile_Causes_NoCauses = EsCauses == 1 ? "" : "_N";
            string sTitulo_Causes_NoCauses = EsCauses == 1 ? "" : " NO CAUSES";
            sTitulo_Causes_NoCauses = "";
            DateTime dtProceso = new DateTime(); 

            //sTipo_Titulo += sFile_Causes_NoCauses = EsCauses == 1 ? "" : " NO CAUSES";

            clsLeer leerDatos = new clsLeer();
            leerDatos.DataSetClase = DatosExportar;
            leerDatos.Leer();
            dtProceso = GetFecha(leerDatos.Campo("FechaRegistro")); 

            sTipoDeInsumo = leerDatos.CampoInt("TipoInsumo").ToString();
            sAnexo = string.Format("{0} - {1}", leerDatos.Campo("IdAnexo"), leerDatos.Campo("NombreAnexo")); 
            sFolioRemision = leerDatos.Campo("FolioRemision").Replace(" ", "__");
            sFolioRemision = bConsolidarMeses ? dtProceso.Year.ToString() + "_" + General.FechaNombreMes(dtProceso).ToUpper() + "_____" + sFolioRemision : sFolioRemision;
            sFolioRemision_Aux = sFolioRemision; 
            sFolioRemision = sFolioRemision.Replace("/", "_");
            sFolioRemision = sFolioRemision.Replace(@"\", "_");
            sFolioRemision = sFolioRemision != "" ? sFolioRemision : "NO_ESPECIFICADO"; 
            sFile = string.Format("{0}{1}{2}___{3}___RECETA__{4}{5}{6}",
                sPrefijo, Cliente, Fg.Right(leerDatos.Campo("FechaGeneracion"), 6), sFolioRemision, sTipo, sTipoInventario, sFile_Causes_NoCauses);
            sFile_Auxiliar = sFile; 


            if (bGenerarDocto_Pdf)
            {
                if (bGenerarDocto_Pdf_Concentrado)
                {
                    sFile_Auxiliar = string.Format("{0}{1}{2}___RECETA__CONCENTRADO__{3}{4}{5}",
                        sPrefijo, Cliente, Fg.Right(leerDatos.Campo("FechaGeneracion"), 6), sTipo, sTipoInventario, sFile_Causes_NoCauses);
                }
                bRegresa = GenerarPDF__Receta(sFile_Auxiliar, FechaAProcesar, FechaAProcesar_Final, sFolioRemision_Aux, sTipoDeInsumo, EsEnResguardo, EsCauses); 
            }

            if (bGenerarDocto_Excel)
            {
                bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "INT_ND_Remisiones_Unidad.xlsx", DatosCliente);
                if (!bRegresa)
                {
                    ////General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
                }
                else
                {
                    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                    xpExcel.AgregarMarcaDeTiempo = false;

                    if (xpExcel.PrepararPlantilla(sRutaDestino, sFile + ".xlsx"))
                    {
                        xpExcel.GeneraExcel();

                        xpExcel.Agregar(leerDatos.Campo("Farmacia").ToUpper(), 2, 2);
                        xpExcel.Agregar(leerDatos.Campo("ExpedidoEn").ToUpper(), 3, 2);
                        xpExcel.Agregar(string.Format("Dispensación de recetas del día :   {0}", leerDatos.Campo("FechaRegistro")).ToUpper(), 4, 2);
                        xpExcel.Agregar(string.Format("{0}{1}".ToUpper(), sTipo_Titulo, sTitulo_Causes_NoCauses), 5, 2);
                        xpExcel.Agregar(string.Format("Anexo :  {0}", sAnexo).ToUpper(), 7, 2);
                        xpExcel.Agregar(string.Format("Folio :  {0}", leerDatos.Campo("FolioRemision")).ToUpper(), 7, 9);

                        leerDatos.RegistroActual = 1;
                        while (leerDatos.Leer())
                        {
                            iCol = 2;
                            ////xpExcel.Agregar(iRow - 1, iRow, 2);
                            xpExcel.Agregar(leerDatos.Campo("NumReceta"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.Campo("FechaRegistro"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.Campo("FolioReferencia"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.Campo("Beneficiario"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.Campo("IdMedico"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.Campo("Medico"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.Campo("ClaveSSA_Mascara"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.Campo("Descripcion_Mascara"), iRow, iCol++);

                            xpExcel.Agregar(leerDatos.CampoDouble("Cantidad"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.CampoDouble("PrecioVenta"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.CampoDouble("SubTotal"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.CampoDouble("Iva"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.CampoDouble("ImporteTotal"), iRow, iCol++);

                            dCantidad += leerDatos.CampoDouble("Cantidad");
                            dSubTotal += leerDatos.CampoDouble("SubTotal");
                            dIva += leerDatos.CampoDouble("Iva");
                            dTotal += leerDatos.CampoDouble("ImporteTotal");

                            iRow++;
                        }

                        iCol = 9;
                        xpExcel.Agregar("TOTALES :  ", iRow, iCol);
                        iCol ++; 
                        xpExcel.Agregar(dCantidad, iRow, iCol++);
                        iCol++; 
                        xpExcel.Agregar(dSubTotal, iRow, iCol++);
                        xpExcel.Agregar(dIva, iRow, iCol++);
                        xpExcel.Agregar(dTotal, iRow, iCol++);

                        // Finalizar el Proceso 
                        xpExcel.CerrarDocumento();
                    }
                }
            }

            return bRegresa;
        }

        private bool GenerarExcel__Colectivo(DataSet DatosExportar, string Cliente, string FechaAProcesar, string FechaAProcesar_Final, int EsCauses)
        {
            bool bRegresa = true;
            clsLeer generarExcel = new clsLeer();
            clsLeer leerFiltro = new clsLeer();
            string sFiltro = "";
            string sFiltro_Fecha = "";  

            leerFiltro.DataSetClase = DatosExportar;

            sFiltro_Fecha = string.Format(" FechaRegistro = '{0}' ", FechaAProcesar);
            if (bConsolidarMeses)
            {
                sFiltro_Fecha = string.Format("  FechaRegistro >= '{0}' and FechaRegistro <= '{1}' ", FechaAProcesar, FechaAProcesar_Final);
            }

            ////////////////////////////  Venta  
            sFiltro = string.Format(" {0} and TipoDispensacion = '{1}' and TipoInsumo = '{2}' and EsEnResguardo = '{3}' and EsCauses = '{4}' ",
                sFiltro_Fecha, "C", 1, 0, EsCauses);
            generarExcel.DataRowsClase = leerFiltro.DataTableClase.Select(sFiltro);
            if (generarExcel.Leer())
            {
                 bRegresa = GenerarExcel__Colectivo(generarExcel.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, "M", 0, EsCauses);
            }

            sFiltro = string.Format(" {0} and TipoDispensacion = '{1}' and TipoInsumo = '{2}' and EsEnResguardo = '{3}' and EsCauses = '{4}' ",
                sFiltro_Fecha, "C", 2, 0, EsCauses);
            generarExcel.DataRowsClase = leerFiltro.DataTableClase.Select(sFiltro);
            if (generarExcel.Leer())
            {
                bRegresa = GenerarExcel__Colectivo(generarExcel.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, "MC", 0, EsCauses);
            }


            if (bGenerarDoctos_EnResguardo)
            {
                ////////////////////////////  Consignacion   
                sFiltro = string.Format(" {0} and TipoDispensacion = '{1}' and TipoInsumo = '{2}' and EsEnResguardo = '{3}' and EsCauses = '{4}' ",
                    sFiltro_Fecha, "C", 1, 1, EsCauses);
                generarExcel.DataRowsClase = leerFiltro.DataTableClase.Select(sFiltro);
                if (generarExcel.Leer())
                {
                    bRegresa = GenerarExcel__Colectivo(generarExcel.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, "M", 1, EsCauses);
                }

                sFiltro = string.Format(" {0} and TipoDispensacion = '{1}' and TipoInsumo = '{2}' and EsEnResguardo = '{3}' and EsCauses = '{4}' ",
                    sFiltro_Fecha, "C", 2, 1, EsCauses);
                generarExcel.DataRowsClase = leerFiltro.DataTableClase.Select(sFiltro);
                if (generarExcel.Leer())
                {
                    bRegresa = GenerarExcel__Colectivo(generarExcel.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, "MC", 1, EsCauses);
                }
            }


            return bRegresa;
        }

        private bool GenerarExcel__Colectivo(DataSet DatosExportar, string Cliente, string FechaAProcesar, string FechaAProcesar_Final, string TipoInsumo, int EsEnResguardo, int EsCauses)
        {
            bool bRegresa = false;
            clsLeer generarExcel = new clsLeer();

            generarExcel.DataSetClase = DatosExportar;
            if (generarExcel.Leer())
            {
                bRegresa = GenerarExcel__ColectivoProcesar(generarExcel.DataSetClase, Cliente, FechaAProcesar, FechaAProcesar_Final, TipoInsumo, EsEnResguardo, EsCauses);
            }

            return bRegresa;
        }

        private bool GenerarExcel__ColectivoProcesar(DataSet DatosExportar, string Cliente,
            string FechaAProcesar, string FechaAProcesar_Final, string TipoInsumo, int EsEnResguardo, int EsCauses)
        {
            bool bRegresa = false;
            string sFile = "";
            string sFile_Auxiliar = "";
            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\INT_ND_Remisiones_Unidad_Colectivos.xlsx";
            int iRow = 10;
            int iCol = 2;
            string sFolioRemision = "";
            string sFolioRemision_Aux = ""; 
            string sAnexo = "";
            string sTipo = TipoInsumo == "M" ? "MEDICAMENTO" : "MATERIAL_CURACION";
            string sTipo_Titulo = TipoInsumo == "M" ? "MEDICAMENTO" : "MATERIAL DE CURACIÓN";
            string sTipoInventario = EsEnResguardo == 0 ? "" : "__EnResguardo";
            string sTipoDeInsumo = "";
            double dSubTotal = 0;
            double dIva = 0;
            double dTotal = 0;
            double dCantidad = 0;
            string sFile_Causes_NoCauses = EsCauses == 1 ? "" : "_N";
            string sTitulo_Causes_NoCauses = EsCauses == 1 ? "" : " NO CAUSES";
            DateTime dtProceso = new DateTime(); 
            ////sTipo_Titulo += sFile_Causes_NoCauses = EsCauses == 1 ? "" : " NO CAUSES";


            clsLeer leerDatos = new clsLeer();
            leerDatos.DataSetClase = DatosExportar;
            leerDatos.Leer();
            dtProceso = GetFecha(leerDatos.Campo("FechaRegistro")); 

            sTipoDeInsumo = leerDatos.CampoInt("TipoInsumo").ToString();
            sAnexo = string.Format("{0} - {1}", leerDatos.Campo("IdAnexo"), leerDatos.Campo("NombreAnexo"));
            sFolioRemision = leerDatos.Campo("FolioRemision").Replace(" ", "__");
            sFolioRemision = bConsolidarMeses ? dtProceso.Year.ToString() + "_" + General.FechaNombreMes(dtProceso).ToUpper() + "_____" + sFolioRemision : sFolioRemision;
            sFolioRemision_Aux = sFolioRemision; 
            sFolioRemision = sFolioRemision.Replace("/", "_");
            sFolioRemision = sFolioRemision.Replace(@"\", "_");
            sFolioRemision = sFolioRemision != "" ? sFolioRemision : "NO_ESPECIFICADO";
            sFile = string.Format("{0}{1}{2}___{3}___COLECTIVO__{4}{5}{6}",
                sPrefijo, Cliente, Fg.Right(leerDatos.Campo("FechaGeneracion"), 6), sFolioRemision, sTipo, sTipoInventario, sFile_Causes_NoCauses);
            sFile_Auxiliar = sFile; 

            if (bGenerarDocto_Pdf)
            {
                if (bGenerarDocto_Pdf_Concentrado)
                {
                    sFile_Auxiliar = string.Format("{0}{1}{2}___COLECTIVO__CONCENTRADO__{3}{4}{5}",
                        sPrefijo, Cliente, Fg.Right(leerDatos.Campo("FechaGeneracion"), 6), sTipo, sTipoInventario, sFile_Causes_NoCauses);
                }
                bRegresa = GenerarPDF__Colectivo(sFile_Auxiliar, FechaAProcesar, FechaAProcesar_Final, sFolioRemision_Aux, sTipoDeInsumo, EsEnResguardo, EsCauses);
            }

            if (bGenerarDocto_Excel)
            {
                bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "INT_ND_Remisiones_Unidad_Colectivos.xlsx", DatosCliente);
                if (!bRegresa)
                {
                    ////General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
                }
                else
                {
                    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                    xpExcel.AgregarMarcaDeTiempo = false;

                    if (xpExcel.PrepararPlantilla(sRutaDestino, sFile + ".xlsx"))
                    {
                        xpExcel.GeneraExcel();

                        xpExcel.Agregar(leerDatos.Campo("Farmacia").ToUpper(), 2, 2);
                        xpExcel.Agregar(leerDatos.Campo("ExpedidoEn").ToUpper(), 3, 2);
                        xpExcel.Agregar(string.Format("Dispensación de colectivos del día :   {0}", leerDatos.Campo("FechaRegistro")).ToUpper(), 4, 2);
                        xpExcel.Agregar(string.Format("{0}{1}".ToUpper(), sTipo_Titulo, sTitulo_Causes_NoCauses), 5, 2);
                        xpExcel.Agregar(string.Format("Anexo :  {0}", sAnexo).ToUpper(), 7, 2);
                        xpExcel.Agregar(string.Format("Folio :  {0}", leerDatos.Campo("FolioRemision")).ToUpper(), 7, 6);

                        leerDatos.RegistroActual = 1;
                        while (leerDatos.Leer())
                        {
                            iCol = 2;
                            ////xpExcel.Agregar(iRow - 1, iRow, 2);
                            xpExcel.Agregar(leerDatos.Campo("Departamento"), iRow, iCol++);

                            xpExcel.Agregar(leerDatos.Campo("Folio"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.Campo("ClaveSSA_Mascara"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.Campo("Descripcion_Mascara"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.Campo("Presentacion"), iRow, iCol++);

                            xpExcel.Agregar(leerDatos.CampoDouble("Cantidad"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.CampoDouble("PrecioVenta"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.CampoDouble("SubTotal"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.CampoDouble("Iva"), iRow, iCol++);
                            xpExcel.Agregar(leerDatos.CampoDouble("ImporteTotal"), iRow, iCol++);

                            dCantidad += leerDatos.CampoDouble("Cantidad");
                            dSubTotal += leerDatos.CampoDouble("SubTotal");
                            dIva += leerDatos.CampoDouble("Iva");
                            dTotal += leerDatos.CampoDouble("ImporteTotal");

                            iRow++;
                        }

                        iCol = 6;
                        xpExcel.Agregar("TOTALES :  ", iRow, iCol);
                        iCol ++; 
                        xpExcel.Agregar(dCantidad, iRow, iCol++);
                        iCol++; 
                        xpExcel.Agregar(dSubTotal, iRow, iCol++);
                        xpExcel.Agregar(dIva, iRow, iCol++);
                        xpExcel.Agregar(dTotal, iRow, iCol++);

                        // Finalizar el Proceso 
                        xpExcel.CerrarDocumento();
                    }
                }
            }

            return bRegresa;
        }

        private bool GenerarPDF()
        {
            bool bRegresa = false;
            string sFile = sFile_PDF;
            
            clsImprimir Reporte = new clsImprimir(General.DatosConexion);
            clsReporteador Reporteador;  // = new clsReporteador(Reporte, DatosTerminal);

            Reporte.RutaReporte = DtGeneral.RutaReportes;
            Reporte.NombreReporte = "INT_ND_Remisiones_Unidad";
            Reporte.Add("GUID", sGUID);


            Reporteador = new clsReporteador(Reporte, DatosCliente);
            Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
            Reporteador.Url = General.Url;
            Reporteador.MostrarInterface = false;
            Reporteador.MostrarMensaje_ReporteSinDatos = false;

            bRegresa = Reporteador.ExportarReporte(sFile, FormatosExportacion.PortableDocFormat, true);


            return bRegresa;
        }

        private bool GenerarPDF__Receta(string Nombre_PDF, string FechaInicialProceso, string FechaFinalProceso, string FolioRemision, string TipoInsumo, int EsEnResguardo, int EsCauses)
        {
            string sFile = sRutaDestino + Nombre_PDF + ".pdf";
            string sReporte = !bGenerarDocto_Pdf_Concentrado ? "INT_ND_Remisiones_Unidad_Recetas" : "INT_ND_Remisiones_Unidad_Recetas_Concentrado";
            sReporte = GnDll_SII_INadro.RPT_Remisiones_Receta; 


            if (bSecuenciarDocumentos) 
            {
                sFile = string.Format("{0}{1}__{2}.pdf", sRutaDestino, ObtenerSecuenciaDocumento(), Nombre_PDF);
            }

            if (bConsolidarMeses)
            {
                sReporte = GnDll_SII_INadro.RPT_Remisiones_Receta_Consolidado;
            }

            bool bRegresa = GenerarPDF_Detallado(sFile, sReporte, FechaInicialProceso, FechaFinalProceso, FolioRemision, "R", TipoInsumo, EsEnResguardo, EsCauses);

            return bRegresa;
        }

        private bool GenerarPDF__Colectivo(string Nombre_PDF, string FechaInicialProceso, string FechaFinalProceso, string FolioRemision, string TipoInsumo, int EsEnResguardo, int EsCauses)
        {
            string sFile = sRutaDestino + Nombre_PDF + ".pdf";
            string sReporte = !bGenerarDocto_Pdf_Concentrado ? "INT_ND_Remisiones_Unidad_Colectivos" : "INT_ND_Remisiones_Unidad_Colectivos_Concentrado";
            sReporte = GnDll_SII_INadro.RPT_Remisiones_Colectivo; 

            if (bSecuenciarDocumentos)
            {
                sFile = string.Format("{0}{1}__{2}.pdf", sRutaDestino, ObtenerSecuenciaDocumento(), Nombre_PDF);
            }

            if (bConsolidarMeses)
            {
                sReporte = GnDll_SII_INadro.RPT_Remisiones_Colectivo_Consolidado; 
            }

            bool bRegresa = GenerarPDF_Detallado(sFile, sReporte, FechaInicialProceso, FechaFinalProceso, FolioRemision, "C", TipoInsumo, EsEnResguardo, EsCauses);

            return bRegresa;
        }

        private string ObtenerSecuenciaDocumento()
        {
            string sRegresa = "";

            if (GnDll_SII_INadro.Consecutivo_Docuemento_Generado <= 0)
            {
                GnDll_SII_INadro.Consecutivo_Docuemento_Generado++; 
            }

            sRegresa = Fg.PonCeros(GnDll_SII_INadro.Consecutivo_Docuemento_Generado, 8);
            GnDll_SII_INadro.Consecutivo_Docuemento_Generado++; 

            return sRegresa; 
        }

        private bool GenerarPDF_Detallado(string Nombre_PDF, string NombreReporte,
            string FechaInicialProceso, string FechaFinalProceso, 
            string FolioRemision, string TipoDispensacion, string TipoInsumo, int EsEnResguardo, int EsCauses)
        {
            bool bRegresa = false;
            string sFile = Nombre_PDF;


            clsImprimir Reporte = new clsImprimir(General.DatosConexion);
            clsReporteador Reporteador;  // = new clsReporteador(Reporte, DatosTerminal);

            Reporte.RutaReporte = DtGeneral.RutaReportes;
            Reporte.NombreReporte = NombreReporte;

            if (!bConsolidarMeses)
            {
                Reporte.Add("GUID", sGUID);
                Reporte.Add("FechaProceso", FechaInicialProceso);
                Reporte.Add("TipoDispensacion", TipoDispensacion);
                Reporte.Add("TipoInsumo", TipoInsumo);
                Reporte.Add("EsEnResguardo", EsEnResguardo);
                Reporte.Add("EsCauses", EsCauses);

                if (!bGenerarDocto_Pdf_Concentrado)
                {
                    Reporte.Add("FolioRemision", FolioRemision);
                }

            }
            else 
            {
                Reporte.Add("GUID", sGUID);
                Reporte.Add("FechaProceso", FechaInicialProceso);
                Reporte.Add("FechaProcesoFinal", FechaFinalProceso);
                Reporte.Add("TipoDispensacion", TipoDispensacion);
                Reporte.Add("TipoInsumo", TipoInsumo);
                Reporte.Add("EsEnResguardo", EsEnResguardo);
                Reporte.Add("EsCauses", EsCauses);

            }
           

            Reporteador = new clsReporteador(Reporte, DatosCliente);
            Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
            Reporteador.Url = General.Url;
            Reporteador.MostrarInterface = false;
            Reporteador.MostrarMensaje_ReporteSinDatos = false;

            bRegresa = Reporteador.ExportarReporte(sFile, FormatosExportacion.PortableDocFormat, true);


            return bRegresa;
        }

        private DateTime GetFecha(string Fecha)
        {
            DateTime dt = new DateTime();
            int iAño = 0;
            int iMes = 0;
            int iDia = 0;

            try
            {
                Fecha = Fecha.Replace("-", ""); 
                iAño = Convert.ToInt32(Fg.Mid(Fecha, 1, 4));
                iMes = Convert.ToInt32(Fg.Mid(Fecha, 5, 2)); ;
                iDia = Convert.ToInt32(Fg.Mid(Fecha, 7, 2)); ;

                dt = new DateTime(iAño, iMes, iDia);
            }
            catch { } 

            return dt;
        }
        #endregion Funciones y Procedimientos Privados
    }
}
