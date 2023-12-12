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
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;

using ClosedXML.Excel;

using SC_CompressLib;

namespace Almacen.Pedidos
{
    public partial class FrmPedidos_ListadoDeIncidencias : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            IdEmpresa = 1, IdEstado, IdFarmacia, FolioSurtido, IdSurtimiento, ClaveSSA, IdSubFarmacia, 
            IdProducto, CodigoEAN, Descripcion, ClaveLote, IdPasillo, IdEstante, IdEntrepaño, 
            SKU, IdPersonal, FechaRegistro, IdIncidencia, IncidenciaDesc, Observaciones, Actualizado, Liberar, ObservacionesLiberacion
        }

        clsGrid grid;
        clsLeer leer;
        clsConsultas Consulta;
        clsAyudas Ayuda;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        //clsExportarExcelPlantilla xpExcel;
        clsDatosCliente DatosCliente;

        string iEmpresa = DtGeneral.EmpresaConectada;
        string iEstado = DtGeneral.EstadoConectado;
        string iFarmacia = DtGeneral.FarmaciaConectada;
        

        public FrmPedidos_ListadoDeIncidencias()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            Consulta = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);

            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);

            grid = new clsGrid(ref grdIncidencias, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            grid.AjustarAnchoColumnasAutomatico = true;
        }

        private void FrmPedidos_ListadoDeIncidencias_Load(object sender, EventArgs e)
        {
            ObtenerIncidencias(false);
        }

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ObtenerIncidencias(false);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Liberar();
        }

        private void btnLiberarIncidencias_Click( object sender, EventArgs e )
        {
            int IdSurtimiento, iIdPasillo, iIdEstante, iIdEntrepaño;
            string sIdProducto, sCodigoEAN, sClaveLote, sSKU, sIdIncidencia, sFolioSurtido, sIdSubFarmacia, sObservacionesLiberacion;
            int i = grid.ActiveRow; 


            IdSurtimiento = grid.GetValueInt(i, (int)Cols.IdSurtimiento);
            iIdPasillo = grid.GetValueInt(i, (int)Cols.IdPasillo);
            iIdEstante = grid.GetValueInt(i, (int)Cols.IdEstante);
            iIdEntrepaño = grid.GetValueInt(i, (int)Cols.IdEntrepaño);

            sIdProducto = grid.GetValue(i, (int)Cols.IdProducto);
            sCodigoEAN = grid.GetValue(i, (int)Cols.CodigoEAN);
            sClaveLote = grid.GetValue(i, (int)Cols.ClaveLote);
            sSKU = grid.GetValue(i, (int)Cols.SKU);
            sIdIncidencia = grid.GetValue(i, (int)Cols.IdIncidencia);
            sFolioSurtido = grid.GetValue(i, (int)Cols.FolioSurtido);
            sIdSubFarmacia = grid.GetValue(i, (int)Cols.IdSubFarmacia);
            sObservacionesLiberacion = grid.GetValue(i, (int)Cols.ObservacionesLiberacion);

            if(sIdProducto == "")
            {
                General.msjAviso("No ha seleccionado un registro para liberar incidencias.");
            }
            else
            {
                FrmPedidos_LiberacionDeIncidencias f = new FrmPedidos_LiberacionDeIncidencias(sFolioSurtido, sIdSubFarmacia, IdSurtimiento, sIdProducto, sCodigoEAN, sClaveLote, sSKU,
                    iIdPasillo, iIdEstante, iIdEntrepaño);
                f.ShowInTaskbar = false;
                f.ShowDialog();

                if(f.InformacionGuardada)
                {
                    ObtenerIncidencias(false); 
                }
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ObtenerIncidencias(true);

            string sAño = "", sNombre = "Reporte de incidencia", sNombreHoja = "Hoja1";

            int iRow = 2, iColBase = 2, iColsEncabezado = 0, iRenglon = 0;

            // bloqueo principal 
            Cursor.Current = Cursors.Default;

            clsGenerarExcel generarExcel = new clsGenerarExcel();

            leer.RegistroActual = 1;


            iColsEncabezado = iRow + leer.Columnas.Length - 1;
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
                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }

        #endregion Botones 

        #region Funciones y Procedimientos

        public void ObtenerIncidencias(bool EsExcel)
        {
            string sSql = "";
            int iTipo = !EsExcel ? 0 : 1;

            sSql = string.Format(" Exec spp_RPT_Pedidos_Cedis_Det_Surtido_Incidencias \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Tipo = {3} \n",
                iEmpresa, iEstado, iFarmacia, iTipo);

            if (!EsExcel)
            {
                grid.Limpiar(false);
            }

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de las Incidencias.");
            }
            else
            {
                if(leer.Leer())
                {
                    if (!EsExcel)
                    {
                        grid.LlenarGrid(leer.DataSetClase);
                    }
                }
                else
                {
                    grid.Limpiar(false);
                }
            }
        }

        private void Liberar()
        {
            string sSql = "";
            bool bLiberar = false, bLimpiar = true;
            int IdSurtimiento, iIdPasillo, iIdEstante, iIdEntrepaño;
            string sIdProducto, sCodigoEAN, sClaveLote, sSKU, sIdIncidencia, sFolioSurtido, sObservacionesLiberacion;



            ////for (int i = 1; grid.Rows >= i; i++)
            ////{
            ////    bLiberar = grid.GetValueBool(i, (int)Cols.Liberar);

            ////    if (bLiberar)
            ////    {
            ////        IdSurtimiento = grid.GetValueInt(i, (int)Cols.IdSurtimiento);
            ////        iIdPasillo = grid.GetValueInt(i, (int)Cols.IdPasillo);
            ////        iIdEstante = grid.GetValueInt(i, (int)Cols.IdEstante);
            ////        iIdEntrepaño = grid.GetValueInt(i, (int)Cols.IdEntrepaño);

            ////        sIdProducto = grid.GetValue(i, (int)Cols.IdProducto);
            ////        sCodigoEAN = grid.GetValue(i, (int)Cols.CodigoEAN);
            ////        sClaveLote = grid.GetValue(i, (int)Cols.ClaveLote);
            ////        sSKU = grid.GetValue(i, (int)Cols.SKU);
            ////        sIdIncidencia = grid.GetValue(i, (int)Cols.IdIncidencia);
            ////        sFolioSurtido = grid.GetValue(i, (int)Cols.FolioSurtido);
            ////        sObservacionesLiberacion = grid.GetValue(i, (int)Cols.ObservacionesLiberacion);

            ////        sSql = string.Format(" Exec spp_Mtto_Pedidos_Cedis_Det_Surtido_Incidencias_LIberar @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}',\n " +
            ////            "@IdSurtimiento = {3}, @IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}',\n " +
            ////            "@IdPasillo = {7}, @IdEstante = {8}, @IdEntrepaño = {9}, @SKU = '{10}', @IdIncidencia = '{11}', @IdPersonalValida = '{12}', @Observaciones_Liberacion = '{13}'",
            ////             iEmpresa, iEstado, iFarmacia, IdSurtimiento, sIdProducto, sCodigoEAN, sClaveLote, iIdPasillo,
            ////             iIdEstante, iIdEntrepaño, sSKU, sIdIncidencia, DtGeneral.IdPersonal, sObservacionesLiberacion);

            ////        if (!leer.Exec(sSql))
            ////        {
            ////            Error.GrabarError(leer, "");
            ////            General.msjError("Ocurrió un error al obtener la información de las Incidencias.");
            ////            bLimpiar = false;
            ////            break;
            ////        }
            ////    }
            ////}

            ////if (bLimpiar)
            ////{
            ////    General.msjUser("La Información se guardo satisfactoriamente.");
            ////    ObtenerIncidencias(false);
            ////}

        }
        #endregion Funciones y Procedimientos 


    }
}
