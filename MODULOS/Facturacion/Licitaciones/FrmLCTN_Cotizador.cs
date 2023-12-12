using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem; 
using SC_SolutionsSystem.Data; 
using SC_SolutionsSystem.FuncionesGrid; 
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos; 

using DllFarmaciaSoft;

namespace Facturacion.Licitaciones
{
    public partial class FrmLCTN_Cotizador : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        
        clsAyudas Ayudas;
        clsConsultas Consultas;

        clsGrid Grid;
        clsDatosCliente DatosCliente;

        clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        string sValorGrid = "";
        string sFormato = "#,###,###,##0.###0";
        string sFolioCotizacion = "", sMensaje = "";
        string sRutaPlantilla = "";

        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, Partida = 2, IdClaveSSA = 3, Descripcion = 4, TipoClave = 5, TipoClaveDesc = 6, Presentacion = 7, ContenidoPaq = 8,
            Check = 9, Admon = 10, CostoCompra = 11, CostoPaq = 12, CostoPza = 13, Porcentaje = 14, CantMin = 15, CantMax = 16,  
            SubTotalSinGrabarMin = 17, SubTotalSinGrabarMax = 18,
            TasaIva = 19, SubTotalGrabadoMin = 20, SubTotalGrabadoMax = 21, IvaMin = 22, IvaMax = 23, TotalMin = 24, TotalMax = 25, EsCause = 26
        }

        public FrmLCTN_Cotizador()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "Cotizador_Licitaciones");

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);

            Grid = new clsGrid(ref grdClaves, this);
            Grid.BackColorColsBlk = Color.White;
            grdClaves.EditModeReplace = true;
            

            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);
            
        }

        private void FrmLCTN_Cotizador_Load(object sender, EventArgs e)
        {
            CargarEmpresas();
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;
            
            EliminarRenglonesVacios();
            CalcularIva();
            CalcularTotales();
            TotalizarTipoClaves();
            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardaEncabezado();
                   
                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        txtCotizacion.Text = sFolioCotizacion;
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        DatosExportarExcel();
                        Imprimir(false);
                        btnNuevo_Click(null, null);                     
                    }
                    else
                    {
                        Error.GrabarError(leer, "btnGuardar_Click");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la Información.");

                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }

            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;
            
            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                bContinua = Cancelar_Bloquear_Cotizacion(2);

                if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    txtCotizacion.Text = sFolioCotizacion;
                    cnn.CompletarTransaccion();
                    General.msjUser(sMensaje); //Este mensaje lo genera el SP  
                    btnNuevo_Click(null, null);
                }
                else
                {
                    Error.GrabarError(leer, "btnGuardar_Click");
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al Cancelar la Información.");

                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso(General.MsjErrorAbrirConexion);
            }           
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(false);
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            Imprimir(true);
        }

        private void btnCerrarCotizacion_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                bContinua = Cancelar_Bloquear_Cotizacion(3);

                if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    txtCotizacion.Text = sFolioCotizacion;
                    cnn.CompletarTransaccion();
                    General.msjUser(sMensaje); //Este mensaje lo genera el SP  
                    btnNuevo_Click(null, null);
                }
                else
                {
                    Error.GrabarError(leer, "btnGuardar_Click");
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al Bloquear la Información.");

                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso(General.MsjErrorAbrirConexion);
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportarCotizacion();
        }
        #endregion Botones

        #region Eventos
        private void txtCotizacion_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "";

            if (txtCotizacion.Text.Trim() == "")
            {
                txtCotizacion.Text = "*";
                txtCotizacion.Enabled = false;
                IniciaToolbar(true, true, false, true, false, false);
            }
            else
            {
                sSql = string.Format( " Select Folio, IdEmpresa, NombreCliente, Licitacion, SubTotalSinGrabar_Min, " + 
	                                " SubTotalSinGrabar_Max, SubTotalGrabado_Min, SubTotalGrabado_Max, Iva_Min, Iva_Max, " +  
                                    " Total_Min, Total_Max, Tipo, Observaciones, FechaRegistro, Status " +
                                    " From vw_LCTCN_Cotizaciones (NoLock) " +                                    
                                    " Where Folio = '{0}' ", Fg.PonCeros(txtCotizacion.Text, 8));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtCotizacion_Validating");
                    General.msjError("Ocurrió un error al obtener la información del Folio.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        CargarEncabezadoCotizacion();
                        CargarDetallesCotizacion();
                        DatosExportarExcel();
                        //IniciaToolbar(true, false, false, true, true, false);
                        TotalizarTipoClaves();
                    }
                }

            }
        }

        private void rdoMaxMin_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoMaxMin.Checked)
            {
                FrameTipoCotizacion.Enabled = false;
            }
        }

        private void rdoCantidadesFijas_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCantidadesFijas.Checked)
            {
                FrameTipoCotizacion.Enabled = false;
                Grid.BloqueaColumna(true, (int)Cols.CantMin, true);
            }
        }
        #endregion Eventos

        #region Eventos_Grid
        private void grdClaves_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((Grid.ActiveRow == Grid.Rows) && e.AdvanceNext)
            {
                if (Grid.GetValue(Grid.ActiveRow, (int)Cols.ClaveSSA) != "" && Grid.GetValue(Grid.ActiveRow, (int)Cols.Descripcion) != "")
                {
                    Grid.Rows = Grid.Rows + 1;
                    Grid.ActiveRow = Grid.Rows;
                    Grid.SetActiveCell(Grid.Rows, (int)Cols.ClaveSSA);
                }
            }
           
        }

        private void grdClaves_KeyDown(object sender, KeyEventArgs e)
        {
            string sIdClaveSSA = "";

            if (Grid.ActiveCol == (int)Cols.ClaveSSA )
            {
                // Habilitar la Ayuda en todo el Renglon 
                if (e.KeyCode == Keys.F1)
                {
                    leer.DataSetClase = Ayudas.ClavesSSA_Sales_TipoClaves("grdClaves_KeyDown");
                    if (leer.Leer())
                    {
                        Grid.SetValue(Grid.ActiveRow, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                        CargaDatosSales();
                    }

                }
            }

            if (Grid.ActiveCol == (int)Cols.CostoCompra)
            {
                if (e.KeyCode == Keys.F1)
                {
                    sIdClaveSSA = Grid.GetValue(Grid.ActiveRow, (int)Cols.IdClaveSSA);
                    leer.DataSetClase = Ayudas.Costo_Compra_Licitaciones( sIdClaveSSA, "grdClaves_KeyDown");
                    if (leer.Leer())
                    {
                        Grid.SetValue(Grid.ActiveRow, (int)Cols.CostoCompra, leer.Campo("Importe"));
                    }
                }
            }

            // Es posible borrar el renglon desde cualquier columna 
            if (e.KeyCode == Keys.Delete)
            {
                Grid.DeleteRow(Grid.ActiveRow);

                if (Grid.Rows == 0)
                    Grid.Limpiar(true);
            }
        }

        private void grdClaves_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = Grid.GetValue(Grid.ActiveRow, (int)Cols.ClaveSSA);
        }

        private void grdClaves_EditModeOff(object sender, EventArgs e)
        {            
            Cols iCol = (Cols)Grid.ActiveCol;

            switch (iCol)
            {
                case Cols.ClaveSSA:
                    ObtenerDatosSales();
                    break;
            }

            if (rdoCantidadesFijas.Checked)
            {
                Grid.SetValue(Grid.ActiveRow, (int)Cols.CantMin, Grid.GetValueInt(Grid.ActiveRow, (int)Cols.CantMax));
            }
            CalcularIva();
            CalcularTotales();

            if (Grid.ActiveCol == (int)Cols.CantMax)
            {
                if (Grid.ActiveRow == Grid.Rows)
                {
                    if (Grid.GetValue(Grid.ActiveRow, (int)Cols.ClaveSSA) != "" && Grid.GetValue(Grid.ActiveRow, (int)Cols.Descripcion) != "")
                    {
                        Grid.Rows = Grid.Rows + 1;
                        Grid.ActiveRow = Grid.Rows;
                        Grid.SetActiveCell(Grid.Rows, (int)Cols.ClaveSSA);
                    }
                }                
            }

            TotalizarTipoClaves();
        }

        private void CalcularTotales()
        {
            double dSubTotSinGrabarMin = 0, dSubTotSinGrabarMax = 0, dSubTotGrabadoMin = 0, dSubTotGrabadoMax = 0;
            double dIvaMin = 0, dIvaMax = 0, dTotalMin = 0, dTotalMax = 0;
            int iPiezasMin = 0, iPiezasMax = 0;

            iPiezasMin = Grid.TotalizarColumna((int)Cols.CantMin);
            iPiezasMax = Grid.TotalizarColumna((int)Cols.CantMax);
            lblNumPiezasMin.Text = iPiezasMin.ToString();
            lblNumPiezasMax.Text = iPiezasMax.ToString();

            dSubTotSinGrabarMin = Grid.TotalizarColumnaDou((int)Cols.SubTotalSinGrabarMin);
            dSubTotSinGrabarMax = Grid.TotalizarColumnaDou((int)Cols.SubTotalSinGrabarMax);
            lblSubTotalSinGrabarMin.Text = dSubTotSinGrabarMin.ToString(sFormato);
            lblSubTotalSinGrabarMax.Text = dSubTotSinGrabarMax.ToString(sFormato);

            dSubTotGrabadoMin = Grid.TotalizarColumnaDou((int)Cols.SubTotalGrabadoMin);
            dSubTotGrabadoMax = Grid.TotalizarColumnaDou((int)Cols.SubTotalGrabadoMax);
            lblSubTotalGrabadoMin.Text = dSubTotGrabadoMin.ToString(sFormato);
            lblSubTotalGrabadoMax.Text = dSubTotGrabadoMax.ToString(sFormato);

            dIvaMin = Grid.TotalizarColumnaDou((int)Cols.IvaMin);
            dIvaMax = Grid.TotalizarColumnaDou((int)Cols.IvaMax);
            lblIvaMin.Text = dIvaMin.ToString(sFormato);
            lblIvaMax.Text = dIvaMax.ToString(sFormato);

            dTotalMin = Grid.TotalizarColumnaDou((int)Cols.TotalMin);
            dTotalMax = Grid.TotalizarColumnaDou((int)Cols.TotalMax);
            lblTotalMin.Text = dTotalMin.ToString(sFormato);
            lblTotalMax.Text = dTotalMax.ToString(sFormato);
        }

        private void CalcularIva()
        {
            double dSubTotSinGrabarMin = 0, dSubTotSinGrabarMax = 0, dSubTotGrabadoMin = 0, dSubTotGrabadoMax = 0;
            double dIvaMin = 0, dIvaMax = 0; //  dTotalMin = 0, dTotalMax = 0, 
            double dTasaIva = 0, dCostoPte = 0;
            int iPiezasMin = 0, iPiezasMax = 0;

            dTasaIva = Grid.GetValueDou(Grid.ActiveRow, (int)Cols.TasaIva);
            dCostoPte = Grid.GetValueDou(Grid.ActiveRow, (int)Cols.CostoPaq);
            iPiezasMax = Grid.GetValueInt(Grid.ActiveRow, (int)Cols.CantMax);
            iPiezasMin = Grid.GetValueInt(Grid.ActiveRow, (int)Cols.CantMin);

            if (dTasaIva == 0)
            {
                dSubTotSinGrabarMin = (iPiezasMin * dCostoPte);
                dSubTotSinGrabarMax = (iPiezasMax * dCostoPte);

                Grid.SetValue(Grid.ActiveRow, (int)Cols.SubTotalSinGrabarMin, dSubTotSinGrabarMin);
                Grid.SetValue(Grid.ActiveRow, (int)Cols.SubTotalSinGrabarMax, dSubTotSinGrabarMax);
            }
            else
            {
                dSubTotGrabadoMin = (dCostoPte * iPiezasMin);
                dSubTotGrabadoMax = (dCostoPte * iPiezasMax);
                dIvaMin = ((dCostoPte * (dTasaIva / 100)) * iPiezasMin);
                dIvaMax = ((dCostoPte * (dTasaIva / 100)) * iPiezasMax);

                Grid.SetValue(Grid.ActiveRow, (int)Cols.SubTotalGrabadoMin, dSubTotGrabadoMin);
                Grid.SetValue(Grid.ActiveRow, (int)Cols.SubTotalGrabadoMax, dSubTotGrabadoMax);
                Grid.SetValue(Grid.ActiveRow, (int)Cols.IvaMin, dIvaMin);
                Grid.SetValue(Grid.ActiveRow, (int)Cols.IvaMax, dIvaMax);
            }
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= Grid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                Grid.SetValue(Grid.ActiveRow, i, "");
            }
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= Grid.Rows; i++) //Renglones.
            {
                if (Grid.GetValue(i, (int)Cols.IdClaveSSA).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                    Grid.DeleteRow(i);
            }

            if (Grid.Rows == 0) // Si No existen renglones, se inserta 1.
                Grid.AddRow();
        }

        private void ObtenerDatosSales()
        {
            string sCodigo = ""; // , sSql = "";
            // int iCantidad = 0;

            sCodigo = Grid.GetValue(Grid.ActiveRow, (int)Cols.ClaveSSA);

            if (sCodigo.Trim() != "")
            {                
                leer.DataSetClase = Consultas.ClavesSSA_Sales_TipoClaves(sCodigo, true, "ObtenerDatosSales()");
                {
                    if (!leer.Leer())
                    {
                        //General.msjUser("La Clave no Existe.");
                        Grid.LimpiarRenglon(Grid.ActiveRow);
                    }
                    else
                    {
                        CargaDatosSales();
                    }
                }
            }
        }

        private void CargaDatosSales()
        {
            int iRowActivo = Grid.ActiveRow;
            double dPrecioPqte = 0;
            int iEsCause = 0;

            if (!Grid.BuscaRepetido(leer.Campo("ClaveSSA"), iRowActivo, (int)Cols.ClaveSSA))
            {
                dPrecioPqte = leer.CampoDouble("PrecioNeto");

                Grid.SetValue(iRowActivo, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                Grid.SetValue(iRowActivo, (int)Cols.Partida, iRowActivo);
                Grid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, leer.Campo("IdClaveSSA_Sal"));
                Grid.SetValue(iRowActivo, (int)Cols.Descripcion, leer.Campo("Descripcion"));
                Grid.SetValue(iRowActivo, (int)Cols.TipoClave, leer.Campo("TipoDeClave"));
                Grid.SetValue(iRowActivo, (int)Cols.TipoClaveDesc, leer.Campo("TipoDeClaveDescripcion"));
                Grid.SetValue(iRowActivo, (int)Cols.Presentacion, leer.Campo("Presentacion"));
                Grid.SetValue(iRowActivo, (int)Cols.ContenidoPaq, leer.Campo("ContenidoPaquete"));
                Grid.SetValue(iRowActivo, (int)Cols.CostoCompra, 0);
                Grid.SetValue(iRowActivo, (int)Cols.TasaIva, leer.Campo("TasaIva"));

                if (dPrecioPqte > 0)
                {
                    iEsCause = 1;
                    Grid.SetValue(iRowActivo, (int)Cols.CostoPaq, leer.Campo("PrecioNeto"));
                }

                Grid.SetValue(iRowActivo, (int)Cols.EsCause, iEsCause);

                Grid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                Grid.SetActiveCell(iRowActivo, (int)Cols.Check);

            }
            else
            {
                if (General.msjCancelar("Esta Clave ya se encuentra capturada en otro renglón. ¿ Desea Agregarla ?") == DialogResult.Yes)
                {
                    dPrecioPqte = leer.CampoDouble("PrecioNeto");

                    Grid.SetValue(iRowActivo, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                    Grid.SetValue(iRowActivo, (int)Cols.Partida, iRowActivo);
                    Grid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, leer.Campo("IdClaveSSA_Sal"));
                    Grid.SetValue(iRowActivo, (int)Cols.Descripcion, leer.Campo("Descripcion"));
                    Grid.SetValue(iRowActivo, (int)Cols.TipoClave, leer.Campo("TipoDeClave"));
                    Grid.SetValue(iRowActivo, (int)Cols.TipoClaveDesc, leer.Campo("TipoDeClaveDescripcion"));
                    Grid.SetValue(iRowActivo, (int)Cols.Presentacion, leer.Campo("Presentacion"));
                    Grid.SetValue(iRowActivo, (int)Cols.ContenidoPaq, leer.Campo("ContenidoPaquete"));
                    Grid.SetValue(iRowActivo, (int)Cols.CostoCompra, 0);
                    Grid.SetValue(iRowActivo, (int)Cols.TasaIva, leer.Campo("TasaIva"));

                    if (dPrecioPqte > 0)
                    {
                        iEsCause = 1;
                        Grid.SetValue(iRowActivo, (int)Cols.CostoPaq, leer.Campo("PrecioNeto"));
                    }

                    Grid.SetValue(iRowActivo, (int)Cols.EsCause, iEsCause);

                    Grid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                    Grid.SetActiveCell(iRowActivo, (int)Cols.Check);
                }
                else
                {
                    Grid.SetValue(Grid.ActiveRow, (int)Cols.Check, 0);
                    Grid.SetValue(Grid.ActiveRow, (int)Cols.Admon, 0);
                    Grid.SetValue(Grid.ActiveRow, (int)Cols.EsCause, 0);
                    limpiarColumnas();
                    Grid.SetActiveCell(Grid.ActiveRow, (int)Cols.ClaveSSA);
                }
                
            }
        }

        private void TotalizarTipoClaves()
        {
            
            int iMedicamento = 0, iMatCuracion = 0, iTipoClave = 0;
            int iCauses = 0, iNoCauses = 0, iTipoCauses = 0;

            for (int i = 1; i <= Grid.Rows; i++)
            {
                if (Grid.GetValue(Grid.ActiveRow, (int)Cols.ClaveSSA) != "" && Grid.GetValue(Grid.ActiveRow, (int)Cols.Descripcion) != "")
                {
                    iTipoClave = Grid.GetValueInt(i, (int)Cols.TipoClave);
                    iTipoCauses = Grid.GetValueInt(i, (int)Cols.EsCause);                   

                    if (iTipoClave == 1)
                    {
                        iMatCuracion++;
                    }
                    if (iTipoClave == 2)
                    {
                        iMedicamento++;
                    }

                    if (iTipoCauses == 0)
                    {
                        iNoCauses++;
                    }
                    else
                    {
                        iCauses++;
                    }
                }
            }

            lblMedicamento.Text = iMedicamento.ToString();
            lblMatCuracion.Text = iMatCuracion.ToString();
            lblCauses.Text = iCauses.ToString();
            lblNoCauses.Text = iNoCauses.ToString();
        }
        #endregion Eventos_Grid

        #region Funciones
        private void LimpiarPantalla()
        {
            Fg.IniciaControles(this, true);

            Grid.Limpiar(true);
            FrameDatos.Enabled = true;
            FrameTipoCotizacion.Enabled = true;
            lblStatus.Text = "";
            lblStatus.Visible = false;
            rdoMaxMin.Checked = false;
            rdoCantidadesFijas.Checked = false;
            dtpFechaRegistro.Enabled = false;
            cboEmpresas.SelectedIndex = 0;
            IniciaToolbar(true, false, false, false, false, false);
            lblSubTotalSinGrabarMin.Text = "";
            lblSubTotalSinGrabarMax.Text = "";
            lblSubTotalGrabadoMin.Text = "";
            lblSubTotalGrabadoMax.Text = "";
            lblIvaMin.Text = "";
            lblIvaMax.Text = "";
            lblTotalMin.Text = "";
            lblTotalMax.Text = "";
            lblMedicamento.Text = "";
            lblMatCuracion.Text = "";
            lblCauses.Text = "";
            lblNoCauses.Text = "";
            txtCotizacion.Focus();
        }

        private void IniciaToolbar(bool bNuevo, bool bGuarda, bool bCancela, bool bImprime, bool bExporta, bool bCierra)
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuarda;
            btnCancelar.Enabled = bCancela;
            btnImprimir.Enabled = bImprime;
            btnExportarPDF.Enabled = bExporta;
            btnCerrarCotizacion.Enabled = bCierra;
        }

        private void CargarEmpresas()
        {
            string sSql = "";
            cboEmpresas.Clear();
            cboEmpresas.Add();

            sSql = "Select IdEmpresa, Nombre, EsDeConsignacion From CatEmpresas (NoLock) Where Status = 'A' Order by IdEmpresa ";

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    cboEmpresas.Add(leer.DataSetClase, true, "IdEmpresa", "Nombre");
                }
            }
            else
            {
                Error.LogError(leer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
        }

        private void CargarEncabezadoCotizacion()
        {
            txtCotizacion.Text = leer.Campo("Folio");
            cboEmpresas.Data = leer.Campo("IdEmpresa");
            txtAtencionA.Text = leer.Campo("NombreCliente");
            txtLicitacion.Text = leer.Campo("Licitacion");
            txtObservaciones.Text = leer.Campo("Observaciones");
            dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");

            if (leer.CampoInt("Tipo") == 1)
            {
                rdoMaxMin.Checked = true;
            }
            if (leer.CampoInt("Tipo") == 2)
            {
                rdoCantidadesFijas.Checked = true;
            }

            lblSubTotalSinGrabarMin.Text = leer.CampoDouble("SubTotalSinGrabar_Min").ToString(sFormato);
            lblSubTotalSinGrabarMax.Text = leer.CampoDouble("SubTotalSinGrabar_Max").ToString(sFormato);
            lblSubTotalGrabadoMin.Text = leer.CampoDouble("SubTotalGrabado_Min").ToString(sFormato);
            lblSubTotalGrabadoMax.Text = leer.CampoDouble("SubTotalGrabado_Max").ToString(sFormato);
            lblIvaMin.Text = leer.CampoDouble("Iva_Min").ToString(sFormato);
            lblIvaMax.Text = leer.CampoDouble("Iva_Max").ToString(sFormato);
            lblTotalMin.Text = leer.CampoDouble("Total_Min").ToString(sFormato);
            lblTotalMax.Text = leer.CampoDouble("Total_Max").ToString(sFormato);

            IniciaToolbar(true, true, true, true, true, true);

            if (leer.Campo("Status") == "C")
            {
                lblStatus.Text = "CANCELADA";
                lblStatus.Visible = true;
                IniciaToolbar(true, true, false, true, true, false);
                Grid.BloqueaGrid(true);
            }
            if (leer.Campo("Status") == "B")
            {
                lblStatus.Text = "BLOQUEADA";
                lblStatus.Visible = true;
                IniciaToolbar(true, true, false, true, true, false);
                Grid.BloqueaGrid(true);
            }

            FrameDatos.Enabled = false;

        }

        private void CargarDetallesCotizacion()
        {
            string sSql = string.Format(" Select ClaveSSA, Partida, IdClaveSSA, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, Presentacion, " +
	                                " ContenidoPaquete, TipoManejo, Admon, CostoCompra, PrecioPaquete, PrecioPieza, Porcentaje, CantidadMinima, CantidadMaxima, " + 
	                                " SubTotalSinGrabarMin, SubTotalSinGrabarMax, TasaIva, SubTotalGrabadoMin, SubTotalGrabadoMax, IvaMin, IvaMax,  " +
                                    " 0 As TotalMin, 0 As TotalMax, EsCause " +
                                    " From vw_LCTCN_Cotizaciones_Claves (NoLock) " +	                                    
                                    " Where Folio = '{0}'  Order By Partida ", Fg.PonCeros(txtCotizacion.Text, 8) );

            Grid.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDetallesCotizacion");
                General.msjError("Ocurrió un error al obtener el detalle de la Cotización.");
            }
            else
            {
                if (leer.Leer())
                {
                    Grid.LlenarGrid(leer.DataSetClase);                    
                    lblNumPiezasMin.Text = Grid.TotalizarColumnaDou((int)Cols.CantMin).ToString(sFormato);
                    lblNumPiezasMax.Text = Grid.TotalizarColumnaDou((int)Cols.CantMax).ToString(sFormato);
                }
            }
        }

        private void Imprimir(bool bExportarPDF)
        {
            bool bRegresa = true;
                        
            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            string sNombre = "COTIZACION-" + txtCotizacion.Text.Trim() + ".pdf";

            myRpt.RutaReporte = DtGeneral.RutaReportes;
           
            myRpt.Add("Folio", Fg.PonCeros(txtCotizacion.Text, 8));
            myRpt.NombreReporte = "Rpt_Cotizacion_Licitaciones";

            if (bExportarPDF)
            {
                bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, sNombre, FormatosExportacion.PortableDocFormat);
            }
            else
            {
                 
                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
            }

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }            
        }
        #endregion Funciones

        #region Guardar
        private bool GuardaEncabezado()
        {
            bool bRegresa = true;
            int iTipoCotizacion = 0, iOpcion = 0;
            

            if (rdoMaxMin.Checked)
            {
                iTipoCotizacion = 1;
            }

            if (rdoCantidadesFijas.Checked)
            {
                iTipoCotizacion = 2;
            }

            iOpcion = 1;

            string sSql = string.Format("Exec spp_Mtto_LCTCN_Cotizaciones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', " +
                                        " '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}' ",
                txtCotizacion.Text, cboEmpresas.Data, txtAtencionA.Text, txtLicitacion.Text, lblSubTotalSinGrabarMin.Text.Trim().Replace(",",""),
                lblSubTotalSinGrabarMax.Text.Trim().Replace(",",""), lblSubTotalGrabadoMin.Text.Trim().Replace(",", ""),
                lblSubTotalGrabadoMax.Text.Trim().Replace(",", ""), lblIvaMin.Text.Trim().Replace(",", ""), lblIvaMax.Text.Trim().Replace(",", ""),
                lblTotalMin.Text.Trim().Replace(",", ""), lblTotalMax.Text.Trim().Replace(",", ""), iTipoCotizacion, txtObservaciones.Text,
                iOpcion);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    sFolioCotizacion = leer.Campo("Folio");
                    //txtCotizacion.Text = sFolioCotizacion;
                    sMensaje = leer.Campo("Mensaje");
                    bRegresa = GuardaClaves();
                }
            }
            return bRegresa;
        }

        private bool GuardaClaves()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdClaveSSA = "", sStatus = "A";
            int iCant_Max = 0, iCant_Min = 0, iTipoManejo = 0, iCont_Pte = 0, iPartida = 0;
            int iEsCause = 0, iAdmon = 0;
            double dCosto_Pte = 0, dCosto_Pza = 0, dCostoCompra = 0, dPorcentaje = 0;

            for (int i = 1; i <= Grid.Rows; i++)
            {
                // Se obtienen los datos para la insercion.
                sIdClaveSSA = Grid.GetValue(i, (int)Cols.IdClaveSSA);
                iPartida = Grid.GetValueInt(i, (int)Cols.Partida);
                iCant_Min = Grid.GetValueInt(i, (int)Cols.CantMin);
                iCant_Max = Grid.GetValueInt(i, (int)Cols.CantMax);
                iTipoManejo = Grid.GetValueInt(i, (int)Cols.Check);
                iCont_Pte = Grid.GetValueInt(i, (int)Cols.ContenidoPaq);
                dCosto_Pte = Grid.GetValueDou(i, (int)Cols.CostoPaq);
                dCosto_Pza = Grid.GetValueDou(i, (int)Cols.CostoPza);

                dCostoCompra = Grid.GetValueDou(i, (int)Cols.CostoCompra);
                dPorcentaje = Grid.GetValueDou(i, (int)Cols.Porcentaje);
                iAdmon = Grid.GetValueInt(i, (int)Cols.Admon);
                iEsCause = Grid.GetValueInt(i, (int)Cols.EsCause);
                
                if (sIdClaveSSA != "" && iCant_Max > 0)
                {
                    sSql = String.Format(" Exec spp_Mtto_LCTCN_Cotizaciones_Claves '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', " +
                            " '{8}', '{9}', '{10}', '{11}', '{12}', '{13}' ", sFolioCotizacion, sIdClaveSSA, iPartida, iCant_Min, iCant_Max, 
                            iTipoManejo, iCont_Pte, dCostoCompra, dCosto_Pte, dCosto_Pza, dPorcentaje, iEsCause, iAdmon, sStatus);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }                    
                }
            }

            return bRegresa;
        }
        #endregion Guardar

        #region Cancelar_BloquearCotizacion
        private bool Cancelar_Bloquear_Cotizacion(int iOpcion)
        {
            bool bRegresa = true;
            int iTipoCotizacion = 0;

            if (rdoMaxMin.Checked)
            {
                iTipoCotizacion = 1;
            }

            if (rdoCantidadesFijas.Checked)
            {
                iTipoCotizacion = 2;
            }           

            string sSql = string.Format("Exec spp_Mtto_LCTCN_Cotizaciones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', " +
                                        " '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}' ",
                txtCotizacion.Text, cboEmpresas.Data, txtAtencionA.Text, txtLicitacion.Text, lblSubTotalSinGrabarMin.Text.Trim().Replace(",", ""),
                lblSubTotalSinGrabarMax.Text.Trim().Replace(",", ""), lblSubTotalGrabadoMin.Text.Trim().Replace(",", ""),
                lblSubTotalGrabadoMax.Text.Trim().Replace(",", ""), lblIvaMin.Text.Trim().Replace(",", ""), lblIvaMax.Text.Trim().Replace(",", ""),
                lblTotalMin.Text.Trim().Replace(",", ""), lblTotalMax.Text.Trim().Replace(",", ""), iTipoCotizacion, txtObservaciones.Text,
                iOpcion);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    sFolioCotizacion = leer.Campo("Folio");
                    //txtCotizacion.Text = sFolioCotizacion;
                    sMensaje = leer.Campo("Mensaje");                    
                }
            }
            return bRegresa;
        }
        #endregion Cancelar_BloquearCotizacion

        #region Validaciones de Controles
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtCotizacion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Cotización inválido, verifique.");
                txtCotizacion.Focus();
            }

            if (bRegresa && cboEmpresas.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado la Empresa, verifique.");
                cboEmpresas.Focus();
            }

            if (bRegresa && txtAtencionA.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado con Atención A, verifique.");
                txtAtencionA.Focus();
            }

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las Observaciones, verifique.");
                txtObservaciones.Focus();
            }

            
            if (bRegresa)
            {
                if (!rdoMaxMin.Checked && !rdoCantidadesFijas.Checked)
                {
                    bRegresa = false;
                    General.msjUser("Debe Seleccionar el Tipo de Cotización, Verifique....");
                    FrameTipoCotizacion.Focus();
                }
            }           

            return bRegresa;
        }
        #endregion Validaciones de Controles

        #region Exportar_Excel
        private void DatosExportarExcel()
        {
            string sSql = string.Format(" Select * From vw_LCTCN_Cotizaciones_Claves_Impresion (NoLock) " +
                                    " Where Folio = '{0}'  Order By Partida ", Fg.PonCeros(txtCotizacion.Text, 8));
            

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "DatosExportarExcel");
                General.msjError("Ocurrió un error al obtener el detalle de la Cotización.");
            }
            else
            {
                if (leer.Leer())
                {
                    leerExportarExcel.DataSetClase = leer.DataSetClase;
                }
            }
        }

        private void ExportarCotizacion()
        {
            int iRow = 2;
            string sNombreFile = "";
            // string sPeriodo = "";
            string sRutaReportes = "";

            
            sRutaReportes = DtGeneral.RutaReportes;
            DtGeneral.RutaReportes = sRutaReportes;

            sNombreFile = "Rpt_Cotizacion_Licitaciones" + "_" + txtCotizacion.Text + ".xls";
            sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_Cotizacion_Licitaciones.xls";
            DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_Cotizacion_Licitaciones.xls", DatosCliente);

            xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            xpExcel.AgregarMarcaDeTiempo = false;

            if (xpExcel.PrepararPlantilla(sNombreFile))
            {
                xpExcel.GeneraExcel();

                //Se pone el encabezado
                leerExportarExcel.RegistroActual = 1;
                leerExportarExcel.Leer();
                xpExcel.Agregar(leerExportarExcel.Campo("Empresa"), iRow, 2);
                iRow++;
                xpExcel.Agregar(leerExportarExcel.Campo("NombreCliente"), iRow, 2);
                iRow++;

                //sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
                //   General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
                //xpExcel.Agregar(sPeriodo, iRow, 2);

                iRow = 6;

                xpExcel.Agregar(leerExportarExcel.Campo("Licitacion"), iRow, 3);
                xpExcel.Agregar(lblNumPiezasMin.Text, iRow, 10);
                xpExcel.Agregar(lblNumPiezasMax.Text, iRow, 13);
                xpExcel.Agregar(lblMedicamento.Text, iRow, 17);
                iRow++;

                xpExcel.Agregar(leerExportarExcel.Campo("DescripcionTipo"), iRow, 3);
                xpExcel.Agregar(lblSubTotalSinGrabarMin.Text, iRow, 10);
                xpExcel.Agregar(lblSubTotalSinGrabarMax.Text, iRow, 13);
                xpExcel.Agregar(lblMatCuracion.Text, iRow, 17);
                iRow++;

                xpExcel.Agregar(lblSubTotalGrabadoMin.Text, iRow, 10);
                xpExcel.Agregar(lblSubTotalGrabadoMax.Text, iRow, 13);
                xpExcel.Agregar(lblCauses.Text, iRow, 17);
                iRow++;

                xpExcel.Agregar(lblIvaMin.Text, iRow, 10);
                xpExcel.Agregar(lblIvaMax.Text, iRow, 13);
                xpExcel.Agregar(lblNoCauses.Text, iRow, 17);
                iRow++;
                
                xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);
                xpExcel.Agregar(lblTotalMin.Text, iRow, 10);
                xpExcel.Agregar(lblTotalMax.Text, iRow, 13);

                // Se ponen los detalles
                leerExportarExcel.RegistroActual = 1;
                iRow = 13;

                while (leerExportarExcel.Leer())
                {
                    xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRow, 2);
                    xpExcel.Agregar(leerExportarExcel.Campo("Partida"), iRow, 3);
                    xpExcel.Agregar(leerExportarExcel.Campo("DescripcionClave"), iRow, 4);
                    xpExcel.Agregar(leerExportarExcel.Campo("TipoDeClaveDescripcion"), iRow, 5);
                    xpExcel.Agregar(leerExportarExcel.Campo("Presentacion"), iRow, 6);
                    xpExcel.Agregar(leerExportarExcel.Campo("ContenidoPaquete"), iRow, 7);
                    xpExcel.Agregar(leerExportarExcel.Campo("CostoCompra"), iRow, 8);
                    xpExcel.Agregar(leerExportarExcel.Campo("PrecioPaquete"), iRow, 9);
                    xpExcel.Agregar(leerExportarExcel.Campo("PrecioPieza"), iRow, 10);
                    xpExcel.Agregar(leerExportarExcel.Campo("Porcentaje"), iRow, 11);
                    xpExcel.Agregar(leerExportarExcel.Campo("CantidadMinima"), iRow, 12);
                    xpExcel.Agregar(leerExportarExcel.Campo("CantidadMaxima"), iRow, 13);
                    xpExcel.Agregar(leerExportarExcel.Campo("SubTotalSinGrabarMin"), iRow, 14);
                    xpExcel.Agregar(leerExportarExcel.Campo("SubTotalSinGrabarMax"), iRow, 15);
                    xpExcel.Agregar(leerExportarExcel.Campo("TasaIva"), iRow, 16);
                    xpExcel.Agregar(leerExportarExcel.Campo("SubTotalGrabadoMin"), iRow, 17);
                    xpExcel.Agregar(leerExportarExcel.Campo("SubTotalGrabadoMax"), iRow, 18);
                    xpExcel.Agregar(leerExportarExcel.Campo("IvaMin"), iRow, 19);
                    xpExcel.Agregar(leerExportarExcel.Campo("IvaMax"), iRow, 20);
                    xpExcel.Agregar(leerExportarExcel.Campo("TotalMin"), iRow, 21);
                    xpExcel.Agregar(leerExportarExcel.Campo("TotalMax"), iRow, 22);

                    iRow++;
                }

                // Finalizar el Proceso 
                xpExcel.CerrarDocumento();

                if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                {
                    xpExcel.AbrirDocumentoGenerado();
                }
                //HabilitarControles(true);
            }
        }
        #endregion Exportar_Excel

    }
}
