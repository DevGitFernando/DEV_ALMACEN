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
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;

namespace Almacen.Ubicaciones
{
    public partial class FrmListadoUbicacionesEstandar : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leer2;

        clsConsultas Consulta;
        clsAyudas Ayuda;
        clsGrid grid;

        //clsExportarExcelPlantilla xpExcel;

        clsDatosCliente DatosCliente;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;

        private enum Cols
        {
            Ninguna = 0, Posicion = 1, Desc = 2, Rack = 3, Nivel = 4, Entrepaño = 5
        }

        public FrmListadoUbicacionesEstandar()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            Consulta = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);

            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);

            grid = new clsGrid(ref grdPosiciones, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            grid.AjustarAnchoColumnasAutomatico = true; 
        }

        private void FrmListadoUbicacionesEstandar_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            Cargar_Posiciones();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            //ExportarExcel();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            IniciaBotones(true, false);
            grid.Limpiar(true);
        }

        private void IniciaBotones(bool Ejecutar, bool Exportar)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnExportarExcel.Enabled = Exportar;
        }

        private void Cargar_Posiciones()
        {
            string sSql = "";

            sSql = string.Format(" Select Posicion, DescripcionPosicion, IdRack, IdNivel, IdEntrepaño From vw_CFG_ALMN_Ubicaciones_Estandar " +
		                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' ",
                                    sEmpresa, sEstado, sFarmacia);

            grid.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrio un error al consultar el listado de Posiciones.");
            }
            else
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                    IniciaBotones(true, true);
                }
                else
                {
                    grid.Limpiar(true);
                }
            }
        }
        #endregion Funciones

        #region Exportar_Excel
        //private void ExportarExcel()
        //{
        //    string sRutaPlantilla = Application.StartupPath + @"\Plantillas\ALMN_Rpt_Posiciones_Estandar.xls";
        //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, false, "ALMN_Rpt_Posiciones_Estandar.xls", DatosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = true;

        //        this.Cursor = Cursors.Default;
        //        if (xpExcel.PrepararPlantilla())
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            ExportarPosiciones();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //        this.Cursor = Cursors.Default;
        //    }
        //}       

        //private void ExportarPosiciones()
        //{
        //    DateTime dtpFecha = General.FechaSistema;
        //    int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
        //    int iHoja = 1;
        //    string sNomEmpresa = DtGeneral.EmpresaConectadaNombre;
        //    string sNomEstado = DtGeneral.EstadoConectadoNombre;
        //    string sNomFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
        //    string sFechaImpresion = General.FechaSistemaFecha.ToString();

            
        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(sNomEmpresa, 1, 1);
        //    xpExcel.Agregar(sNomEstado, 2, 1);
        //    xpExcel.Agregar(sNomFarmacia, 3, 1);
        //    xpExcel.Agregar(sFechaImpresion, 6, 2);

        //    leer.RegistroActual = 1;

        //    for (int iRenglon = 9; leer.Leer(); iRenglon++)
        //    {
        //        xpExcel.Agregar(leer.Campo("Posicion"), iRenglon, (int)Cols.Posicion);
        //        xpExcel.Agregar(leer.Campo("DescripcionPosicion"), iRenglon, (int)Cols.Desc);
        //        xpExcel.Agregar(leer.CampoInt("IdRack"), iRenglon, (int)Cols.Rack);
        //        xpExcel.Agregar(leer.CampoInt("IdNivel"), iRenglon, (int)Cols.Nivel);
        //        xpExcel.Agregar(leer.CampoInt("IdEntrepaño"), iRenglon, (int)Cols.Entrepaño);

        //    }
        //    xpExcel.CerrarDocumento();
        //}
        #endregion Exportar_Excel
    }
}
