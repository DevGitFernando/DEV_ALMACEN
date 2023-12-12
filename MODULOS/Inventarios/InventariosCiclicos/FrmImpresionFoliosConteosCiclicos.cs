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
    public partial class FrmImpresionFoliosConteosCiclicos : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        clsConsultas Consultas;
        clsDatosCliente DatosCliente;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

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


        public FrmImpresionFoliosConteosCiclicos()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);

            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);

        }

        private void FrmComprasFarmacia_Load(object sender, EventArgs e)
        {
            IniciarToolBar(true, false);
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
            btnExportarExcel.Enabled = Imprimir;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtFolio.Text = "";
            txtFolio.Enabled = true;
            txtFolio.Focus();
        }

        #endregion Limpiar

        #region Buscar Folio
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);
            string sSql = "";
            string sFolio = Fg.PonCeros(txtFolio.Text.Trim(), 8);

            if (txtFolio.Text.Trim() != "")
            {
                sSql = string.Format("Select * " +
                    " From Inv_Ciclicos_Conteos_Enc (NoLock) " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioCiclico = '{3}' ",
                    DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolio );

                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "");
                    General.msjError("Ocurrió un error al obtener la información del Folio.");
                }
                else
                {
                    if (myLeer.Leer())
                    {
                        CargaEncabezadoFolio(); 

                        IniciarToolBar(true, true); 
                    }
                    else 
                    {
                        IniciarToolBar(true, false);
                        General.msjUser("El Folio ingresado no existe, intente de nuevo.");
                        txtFolio.Focus();
                        
                    }
                }
            }
        }

        private void CargaEncabezadoFolio()
        {
            //Se hace de esta manera para la ayuda.
            txtFolio.Enabled = false;
            txtFolio.Text = myLeer.Campo("FolioCiclico");  
        }

        #endregion Buscar Folio

        #region Eventos 
        #endregion Eventos

        #region Funciones

        public void MostrarPantalla()
        {
           this.ShowDialog();
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
    } // Llaves de la Clase
} // Llaves del NameSpace
