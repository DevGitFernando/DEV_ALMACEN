using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;

namespace Farmacia.Inventario
{
    public partial class FrmAjustesInventario_Codigos_Sin_Ajuste : FrmBaseExt
    {
        private enum Cols 
        {
            Ninguna = 0, 
            CodEAN = 1, Codigo = 2, Descripcion = 3
        }

        #region variables 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leerProductos;
        clsGrid myGrid;

        clsConsultas query;
        clsAyudas ayuda;
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        DataSet dtsCodigos;
        DataSet dtsLotes;
        DataSet dtsUbicaciones;
        FrmAjustesInventario_Seleccion Seleccion;

        #endregion variables

        public FrmAjustesInventario_Codigos_Sin_Ajuste()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myGrid = new clsGrid( ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.BackColorColsBlk = Color.White; 
            grdProductos.EditModeReplace = true;
            myGrid.AjustarAnchoColumnasAutomatico = true;

            leer = new clsLeer(ref cnn);
            leerProductos = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
        }

        private void FrmAjustesInventario_Codigos_Sin_Ajuste_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            myGrid.Limpiar(false);
            Fg.IniciaControles();
            IniciarToolBar(true, true, false);
            btnExportar.Enabled = false;
            btnExportar.Visible = false;
            txtFolioInicio.Focus();            
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            LlenaGrid();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;

            if (ValidarImpresion())
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_AjustesInv_Codigos_Sin_Ajuste.rpt";

                if (General.ImpresionViaWeb)
                {
                    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                    DataSet datosC = DatosCliente.DatosCliente();

                    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                }
                else
                {
                    myRpt.CargarReporte(true);
                    bRegresa = !myRpt.ErrorAlGenerar;
                }


                if (bRegresa)
                {
                    btnNuevo_Click(null, null);
                }
                else
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            AsignarCodigos();
        }

        #endregion Botones

        #region Buscar Folios
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolioInicio.Text.Trim() != "")
            {
                leer.DataSetClase = query.AjusteInventario(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolioInicio.Text, 8), "txtFolio_Validating");
                if (leer.Leer())
                {
                    txtFolioInicio.Text = leer.Campo("Poliza");
                    txtFolioInicio.Enabled = false;
                }
                else
                {
                    txtFolioInicio.Text = "";
                    txtFolioInicio.Focus();
                }
            }

        }

        private void txtFolioFinal_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolioFinal.Text.Trim() != "")
            {
                leer.DataSetClase = query.AjusteInventario(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolioFinal.Text, 8), "txtFolio_Validating");
                if (leer.Leer())
                {
                    txtFolioFinal.Text = leer.Campo("Poliza");
                    txtFolioFinal.Enabled = false;
                }
                else
                {
                    txtFolioFinal.Text = "";
                    txtFolioFinal.Focus();
                }
            }
        }

        #endregion Buscar Folios
        
        #region Manejo Grid
        private bool LlenaGrid()
        {
            bool bRegresa = false;

            //Si los Folios estan deshabilitados significa que ya han sido validados.
            if (ValidarEjecucion())
            {
                string sSql = string.Format(
                    "Exec spp_Rpt_AjustesInv_Codigos_Sin_Ajuste @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @PolizaInicio = '{3}', @PolizaFinal = '{4}' \n" + 
                    "\n" +
                    "Select CodigoEAN, IdProducto, Descripcion, Sum( Existencia ) as Existencia \n" +
                    "From tmpAjustesInventarioCodigos (NoLock) \n" +
                    "Group By CodigoEAN, IdProducto, Descripcion \n" +
                    "Having Sum( Existencia ) > 0 \n" +
                    "Order By Descripcion \n",
                    sEmpresa, sEstado, sFarmacia, txtFolioInicio.Text, txtFolioFinal.Text);

                myGrid.Limpiar();
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "LlenaGrid()");
                    General.msjError("Ocurrió un error al obtener los Codigos.");
                }
                else
                {
                    if (!leer.Leer())
                    {
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados");
                    }
                    else 
                    {
                        dtsCodigos = leer.DataSetClase;
                        myGrid.LlenarGrid(leer.DataSetClase);
                        bRegresa = true;
                        IniciarToolBar(true, false, true);

                        btnExportar.Visible = true;
                        btnExportar.Enabled = true;

                        ////if (DtGeneral.EsAdministrador)
                        ////{
                        ////    btnExportar.Visible = true;
                        ////    btnExportar.Enabled = true;
                        ////}
                        ////else
                        ////{
                        ////    btnExportar.Visible = false;
                        ////    btnExportar.Enabled = false;
                        ////}
                    }
                }
            }
            return bRegresa;
        }

        #endregion Manejo Grid 

        #region Eventos de Formulario

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.N:
                    if (btnNuevo.Enabled)
                        btnNuevo_Click(null, null);
                    break;

                case Keys.E:
                    if (btnEjecutar.Enabled)
                        btnEjecutar_Click(null, null);
                    break;

                case Keys.P:
                    if (btnImprimir.Enabled)
                        btnImprimir_Click(null, null);
                    break;

                default:
                    break;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control)
                TeclasRapidas(e);

            switch (e.KeyCode)
            {
                //case Keys.F7:
                //    mostrarOcultarLotes();
                //    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        #endregion Eventos de Formulario 

        #region Funciones 

        private void IniciarToolBar(bool Nuevo, bool Ejecutar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }

        private bool ValidarEjecucion()
        {
            bool bRegresa = true;
            int iFolioInicio = 0, iFolioFinal = 0;

            if (txtFolioInicio.Enabled == true || txtFolioFinal.Enabled == true)
            {
                bRegresa = false;
                General.msjUser("Necesita ingresar una Póliza Inicial y Póliza Final");
            }

            if (bRegresa)
            {
                iFolioInicio = int.Parse(txtFolioInicio.Text.Trim());
                iFolioFinal = int.Parse(txtFolioFinal.Text.Trim());
                if (iFolioInicio > iFolioFinal)
                {
                    bRegresa = false;
                    General.msjAviso("La Póliza Inicial no puede ser mayor que la Póliza Final");
                }
            }
            return bRegresa;
        }

        private bool ValidarImpresion()
        {
            bool bRegresa = false;

            if (myGrid.Rows > 0)
            {
                bRegresa = true;
            }

            return bRegresa;
        }

        private bool ObtenerLotesCodigos()
        {
            bool bRegresa = true;
            clsLeer leerInformacion; 

            //Si los Folios estan deshabilitados significa que ya han sido validados.
            if (myGrid.Rows > 0)
            {
                string sSql = "";

                sSql = string.Format(
                    "Select \n" +
                    "\tIdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, MesesCad, FechaReg, \n" +
                    "\tFechaCad, Status, Existencia, Cantidad \n" +
                    "From tmpAjustesInventarioCodigos (NoLock) \n" +
                    "Order by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote \n\n");

                sSql += string.Format(
                    "Select \n" +
                    "\tIdSubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                    "\tIdPasillo, IdEstante, IdEntrepaño as IdEntrepano, Status, Existencia, ExistenciaDisponible, Cantidad \n" +
                    "From tmpAjustesInventarioCodigos__Ubicaciones (NoLock) \n" +
                    "Order by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote \n\n");


                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leer, "ObtenerLotesCodigos()");
                    General.msjError("Ocurrió un error al obtener los Lotes de los Codigos.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        leerInformacion = new clsLeer();

                        leerInformacion.DataTableClase = leer.Tabla(1);
                        dtsLotes = leerInformacion.DataSetClase;

                        leerInformacion.DataTableClase = leer.Tabla(2);
                        dtsUbicaciones = leerInformacion.DataSetClase;
                    }


                    ////else
                    ////{
                    ////    General.msjUser("Los Codigos seleccionados no tienen Lotes, verifique.");
                    ////}
                }
            }
            else
            {
                bRegresa = false;
                General.msjUser("Necesita ingresar una Póliza Inicial y Póliza Final");
            }
            return bRegresa;
        }

        private void AsignarCodigos()
        {
            if (ObtenerLotesCodigos())
            {
                Seleccion = new FrmAjustesInventario_Seleccion();
                Seleccion.MostrarDetalle(dtsCodigos, dtsLotes, dtsUbicaciones);
            }
        }

        #endregion Funciones

        

    }
}
