using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Reporteador;
using DllFarmaciaSoft.ExportarExcel;

using ClosedXML.Excel;

namespace Inventarios.InventariosCiclicos
{
    public partial class FrmFoliosOrdenesCompras : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        clsConsultas Consultas;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        bool bCargarPantalla = false;
        string sFolio = "";

        private enum Cols
        {
            Ninguna = 0,
            Folio = 1, Fecha = 2, Total = 3
        }

        public string FolioConteo
        {
            get { return sFolio; }
        }


        public FrmFoliosOrdenesCompras()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.SeleccionSimple);
            myGrid.AjustarAnchoColumnasAutomatico = true;

            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);

        }

        private void FrmComprasFarmacia_Load(object sender, EventArgs e)
        {
        }

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.N:
                    if (btnNuevo.Enabled)
                    {
                        btnNuevo_Click(null, null);
                    }
                    break;

                case Keys.P:
                    //if (btnImprimir.Enabled)
                    //{
                    //    btnImprimir_Click(null, null);
                    //}
                    break;

                default:
                    break;
            }
        }

        #region Limpiar

        private void IniciarToolBar(bool Nuevo, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ////if (!bOpcionExterna)
            ////{
            ////    IniciarToolBar(true, false);
            ////    Fg.IniciaControles(this, false);
            ////    myGrid.Limpiar(false);
            ////    txtFolio.Enabled = true;
            ////    txtFolio.Focus();
            ////}
            sFolio = "*"; 
            this.Close();
        }

        #endregion Limpiar

        #region Buscar Folio
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);
            string sSql = "";

            if (txtFolio.Text.Trim() != "")
            {
                sSql = string.Format("Select FolioConteo, Convert( varchar(10), FechaRegistro, 120 ) as FechaRegistro " +
                    " From Inv_Ciclicos_Conteos_Enc (NoLock) " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioCiclico = '{3}' And Status = 'A' ",
                    DtGeneral.EmpresaConectada, sEstado, sFarmacia, txtFolio.Text.Trim() );

                myGrid.Limpiar(false);
                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "");
                    General.msjError("Ocurrió un error al obtener la información del Folio.");
                }
                else
                {
                    bCargarPantalla = true;

                    if (myLeer.Leer())
                    {
                        //CargaEncabezadoFolio(); 
                        myGrid.LlenarGrid(myLeer.DataSetClase);

                        IniciarToolBar(true, true); 
                    }
                }
            }
        }

        //private void CargaEncabezadoFolio()
        //{
        //    //Se hace de esta manera para la ayuda.
        //    txtFolio.Enabled = false;
        //    txtFolio.Text = myLeer.Campo("FolioConteo");  
        //}

        #endregion Buscar Folio

        #region Eventos 
        private void grdProductos_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            sFolio = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Folio);
            // bCargarOC = true; 
            this.Close();
        }
        #endregion Eventos

        #region Funciones
        public void MostrarPantalla(string Folio)
        {            
            txtFolio.Text = Fg.PonCeros(Folio, 8);
            txtFolio.Enabled = false;
            bCargarPantalla = false; 
            txtFolio_Validating(null, null);

            if (!bCargarPantalla)
            {
                this.Hide(); 
            }
            else 
            { 
                this.ShowDialog();
            }
        }

        private bool GenerarInformacionExcel()
        {
            string sSql = "";
            bool bRegresa = false;

            string sFolioCilico = txtFolio.Text.Trim();
            string sFolioConteo = "";

            sFolioConteo = sFolioConteo == "*" ? "" : sFolioConteo;


            sSql = string.Format("Exec spp_Mtto_Inv_Ciclicos____Listado \n" +
                "@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " +
                "@FolioCiclico = '{3}', @FolioConteo = '{4}' ",

                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                sFolioCilico, sFolioConteo
                );

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "GenerarInformacionExcel()");
                General.msjError("Ocurrió un error al obtener la información de inventario.");
            }
            else
            {
                if (!myLeer.Leer())
                {
                    General.msjUser("No se encontró información.");
                }
                else
                {
                    bRegresa = true;
                }
            }

            return bRegresa;
        }

        private void ExportarExcel()
        {
            string sAño = "", sNombre = "Reporte de inventario ciclico", sNombreHoja = "Hoja1";

            int iRow = 2, iColBase = 2, iColsEncabezado = 0, iRenglon = 0;

            // bloqueo principal 
            Cursor.Current = Cursors.Default;

            clsGenerarExcel generarExcel = new clsGenerarExcel();

            myLeer.RegistroActual = 1;


            iColsEncabezado = iRow + myLeer.Columnas.Length - 1;
            iColsEncabezado = iRow + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = sNombre;
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombre))
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);


                iRenglon = 8;
                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, myLeer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }
        }


        #endregion Funciones   

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (GenerarInformacionExcel())
            {
                ExportarExcel();
            }
        }

        private void btnExportarPrevios_Click(object sender, EventArgs e)
        {
            FrmImpresionFoliosConteosCiclicos D = new FrmImpresionFoliosConteosCiclicos();

            D.MostrarPantalla();
        }
    } // Llaves de la Clase
} // Llaves del NameSpace
