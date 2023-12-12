using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Windows.Forms;

using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;

using Microsoft.VisualBasic;

using System.Text;
using System.IO;
using System.Configuration;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;


using DllFarmaciaSoft; 
using DllFarmaciaSoft.ExportarExcel;


namespace DllRecetaElectronica.ECE
{
    public partial class FrmEstadisticas : FrmBaseExt 
    {

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        ////clsErrorManager error; 

        clsListView lst;
        ExpedienteElectronico_Interface tpInterface = ExpedienteElectronico_Interface.Ninguno;
        string sSPP = ""; 

        public FrmEstadisticas( ExpedienteElectronico_Interface Interface )
        {
            InitializeComponent();

            tpInterface = Interface; 

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(DtGeneral.DatosApp, this.Name); 


            lst = new clsListView(lstResultados);
            lst.PermitirAjusteDeColumnas = true;

            if(tpInterface == ExpedienteElectronico_Interface.SESEQ)
            {
                sSPP = " spp_INT_SESEQ__RPT_Estadisticas ";
            }
        }

        private void FrmEstadisticas_Load( object sender, EventArgs e )
        {
            IniciarPantalla();
        }

        #region Botones 
        private void IniciarPantalla()
        {
            Fg.IniciaControles(true); 
            btnExportarExcel.Enabled = false;
            dtpFechaInicial.Enabled = true;
            dtpFechaFinal.Enabled = true;
            lst.LimpiarItems();

        }
        private void btnNuevo_Click( object sender, EventArgs e )
        {
            IniciarPantalla();
        }

        private void btnEjecutar_Click( object sender, EventArgs e )
        {
            ObtenerInformacion(); 
        }
        private void btnExportarExcel_Click( object sender, EventArgs e )
        {
            Generar_Excel(false);
        }
        #endregion Botones 

        #region Funciones y Procedimientos Privados 
        private bool ObtenerInformacion() 
        {
            bool bRegresa = false;

            string sSql = string.Format("Exec {0} \n", sSPP);
            sSql += string.Format("" +
                "\t @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FechaInicial = '{3}', @FechaFinal = '{4}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value) 
            );

            dtpFechaInicial.Enabled = bRegresa;
            dtpFechaFinal.Enabled = bRegresa;
            btnExportarExcel.Enabled = bRegresa;
            lst.LimpiarItems();
            Application.DoEvents();
            System.Threading.Thread.Sleep(150);


            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerInformacion");
                General.msjError("Ocurrió un error al obtener la información solicitada.");
            }
            else
            {
                bRegresa = leer.Leer();
                lst.CargarDatos(leer.DataSetClase, true, true); 
            }

            btnExportarExcel.Enabled = bRegresa;
            dtpFechaInicial.Enabled = !bRegresa;
            dtpFechaFinal.Enabled = !bRegresa;

            return bRegresa; 
        }

        private void Generar_Excel( bool EsGeneral )
        {
            clsGenerarExcel excel = new clsGenerarExcel();
            string sNombreDocumento = "ANALISIS RECETA ELECTRÓNICA";
            string sNombreHoja = "Estadisticas";
            string sConcepto = string.Format("ESTADISTICA DE RECETA ELECTRÓNICA DEL {0} AL {1}", 
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value));

            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;


            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
            //int iHoja = 1;
            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            excel = new clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;

            if(excel.PrepararPlantilla(sNombreDocumento))
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 14, sConcepto);
                excel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                iRenglon = 8;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }
        }
        #endregion Funciones y Procedimientos Privados 
    }
}
