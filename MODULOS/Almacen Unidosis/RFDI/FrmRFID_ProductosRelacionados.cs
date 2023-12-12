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
using SC_SolutionsSystem.Errores; 

using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.QRCode.GenerarEtiquetas; 

namespace Almacen_Unidosis.RFDI
{
    public partial class FrmRFID_ProductosRelacionados : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            CodEAN, Codigo, Descripcion, ClaveLote, Cantidad 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leer2;
        clsGrid grid;

        clsConsultas Consultas;
        clsAyudas Ayudas;
        clsDatosCliente DatosCliente;

        Cols ColActiva = Cols.Ninguna;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sValorGrid = "";
        string sIdProGrid = "";
        string sFolio_TAG = "";

        clsET_Tag etiqueta = new clsET_Tag(); 

        public FrmRFID_ProductosRelacionados()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            grid = new clsGrid(ref grdProductos, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            grid.BackColorColsBlk = Color.White;
            grdProductos.EditModeReplace = true;

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);

            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
        }

        private void FrmRFID_ProductosRelacionados_Load(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        #region Botones 
        private void InicializarPantalla()
        {
            lblStatus.Visible = false; 
            IniciarToolBar(false, false, false); 
            Fg.IniciaControles();

            lblStatus.Visible = false;
            lblStatus.Text = "NUEVA CAPTURA";

            grid.Limpiar(true);
            grid.BloqueaGrid(true); 
            dtpFechaRegistro.Enabled = false; 

            txtTAG.Focus(); 
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Guardar_000_Informacion(); 
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            etiqueta.GenerarEtiqueta(sEmpresa, sEstado, sFarmacia, sFolio_TAG, true); 
        }
        #endregion Botones

        #region Guardardo de informacion 
        private bool validarDatos()
        {
            bool bRegresa = true;

            return bRegresa;
        }

        private bool Guardar_000_Informacion()
        {
            bool bRegresa = true;

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion(); 
            }
            else           
            {
                cnn.IniciarTransaccion();

                bRegresa = Guardar_001__Encabezado();

                if (!bRegresa)
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "");
                    General.msjError("Ocurrió un error al guardar la información.");
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamente.");
                    etiqueta.GenerarEtiqueta(sEmpresa, sEstado, sFarmacia, sFolio_TAG, true);
                    InicializarPantalla();
                }

                cnn.Cerrar(); 
            }

            return bRegresa;
        }

        private bool Guardar_001__Encabezado()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_Mtto_RFID_Tags_Enc " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @TAG = '{4}', @IdPersonalRegistra = '{5}', @Opcion = '{6}' ", 
                sEmpresa, sEstado, sFarmacia, "*", txtTAG.Text.Trim(), DtGeneral.IdPersonal, 1);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true;
                    sFolio_TAG = leer.Campo("Clave");
                }
            }

            if (bRegresa)
            {
                bRegresa = Guardar_002__Detalles(); 
            }

            return bRegresa;
        }

        private bool Guardar_002__Detalles()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdProducto = "";
            string sCodigoEAN = "";
            string sClaveLote = "";
            int iCantidad = 0; 

            for (int i = 1; i <= grid.Rows; i++)
            {
                sIdProducto = grid.GetValue(i, (int)Cols.Codigo);
                sCodigoEAN = grid.GetValue(i, (int)Cols.CodEAN);
                sClaveLote = grid.GetValue(i, (int)Cols.ClaveLote); 
                iCantidad = grid.GetValueInt(i, (int)Cols.Cantidad); 

                sSql = string.Format("Exec spp_Mtto_spp_Mtto_RFID_Tags_Det " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', " + 
                    " @IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', @Renglon = '{7}', @Cantidad = '{8}', @iOpcion = '{9}' ",
                    sEmpresa, sEstado, sFarmacia, sFolio_TAG, sIdProducto, sCodigoEAN, sClaveLote, i, iCantidad, 1);

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break; 
                }
            }

            return bRegresa;
        }
        #endregion Guardardo de informacion

        #region Manejo Grid
        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {

            if ((grid.ActiveRow == grid.Rows) && e.AdvanceNext)
            {
                if (grid.GetValue(grid.ActiveRow, (int)Cols.CodEAN) != "" && grid.GetValueInt(grid.ActiveRow, (int)Cols.Cantidad) > 0)
                {
                    grid.Rows = grid.Rows + 1;
                    grid.ActiveRow = grid.Rows;

                    grid.SetActiveCell(grid.Rows, 1);
                }
            }
        }

        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)grid.ActiveCol;
            switch (ColActiva)
            {
                case Cols.CodEAN:
                case Cols.Descripcion:
                case Cols.Cantidad:
                {
                    if (e.KeyCode == Keys.F1)
                    {
                        sValorGrid = leer.Campo("CodigoEAN");
                        leer.DataSetClase = Ayudas.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, "grdProductos_KeyDown");
                        if (leer.Leer())
                        {
                            sValorGrid = leer.Campo("CodigoEAN");
                            grid.SetValue(grid.ActiveRow, (int)Cols.CodEAN, sValorGrid);
                            ObtenerInformacion(sValorGrid);
                        }
                    }

                    if (e.KeyCode == Keys.Delete)
                    {
                        ///removerLotes();
                    }
                }
                break;
            }
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            ColActiva = (Cols)grid.ActiveCol;
            bool bEsEAN_Unico = true;

            switch (ColActiva)
            {
                case Cols.CodEAN:
                    string sValor = grid.GetValue(grid.ActiveRow, (int)Cols.CodEAN);
                    if (sValor != "")
                    {
                        leer.DataSetClase = Consultas.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sValor, "grdProductos_EditModeOff");
                        if (leer.Leer())
                        {
                            if (!GnFarmacia.ValidarSeleccionCodigoEAN(sValor, ref sValor, ref bEsEAN_Unico))
                            {
                                grid.LimpiarRenglon(grid.ActiveRow);
                                grid.SetActiveCell(grid.ActiveRow, (int)Cols.CodEAN);
                            }
                            //else
                            //{
                            if (!bEsEAN_Unico)
                            {
                                leer.GuardarDatos(1, "CodigoEAN", sValor);
                                //leer.DataSetClase = query.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sValor, "grdProductos_EditModeOff");
                            }

                            ////    CargarDatosProducto();
                            ////}
                            sValor = leer.Campo("CodigoEAN");
                            ObtenerInformacion(sValor);
                        }
                        else
                        {
                            grid.LimpiarRenglon(grid.ActiveRow);
                            grid.SetActiveCell(grid.ActiveRow, (int)Cols.CodEAN);
                        }
                    }
                    else
                    {
                        grid.LimpiarRenglon(grid.ActiveRow);
                    }
                    break;

                case Cols.ClaveLote:
                    validarLote(); 
                    break; 
            }
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = grid.GetValue(grid.ActiveRow, (int)Cols.CodEAN);
        }

        private void ObtenerInformacion(string sValor)
        {
            bool bEsEAN_Unico = true;

            ////if (EAN.EsValido(sValor))
            {
                leer.DataSetClase = Consultas.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sValor, "grdProductos_EditModeOff");
                if (leer.Leer())
                {
                    if (!GnFarmacia.ValidarSeleccionCodigoEAN(sValor, ref sValor, ref bEsEAN_Unico))
                    {
                        grid.LimpiarRenglon(grid.ActiveRow);
                        grid.SetActiveCell(grid.ActiveRow, (int)Cols.CodEAN);
                    }
                    else
                    {
                        if (!bEsEAN_Unico)
                        {
                            leer.GuardarDatos(1, "CodigoEAN", sValor);
                            //leer.DataSetClase = query.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sValor, "grdProductos_EditModeOff");
                        }

                        CargarDatosProducto();
                    }
                }
                else
                {
                    grid.LimpiarRenglon(grid.ActiveRow);
                    grid.SetActiveCell(grid.ActiveRow, (int)Cols.CodEAN);
                }
            }
            ////else
            ////{
            ////    //General.msjError(sMsjEanInvalido);
            ////    grid.LimpiarRenglon(grid.ActiveRow);
            ////    grid.ActiveCelda(grid.ActiveRow, (int)Cols.CodEAN);
            ////    SendKeys.Send("");
            ////}
        }

        private bool CargarDatosProducto()
        {
            return CargarDatosProducto(grid.ActiveRow);
        }

        private bool CargarDatosProducto(int Renglon)
        {
            bool bRegresa = true;
            int iRow = Renglon;
            int iColEAN = (int)Cols.CodEAN;
            string sCodEAN = leer.Campo("CodigoEAN");

            //if (sValorGrid != sCodEAN)
            {
                if (!grid.BuscaRepetido(sCodEAN, iRow, (int)Cols.CodEAN))
                {
                    //// No modificar la informacion capturada en el renglon si este ya existia
                    grid.SetValue(iRow, (int)Cols.CodEAN, sCodEAN);
                    grid.SetValue(iRow, (int)Cols.Descripcion, leer.Campo("Descripcion"));

                    //if (sIdProGrid != leer.Campo("CodigoEAN"))
                    //if (sValorGrid != leer.Campo("CodigoEAN"))
                    {
                        sIdProGrid = leer.Campo("IdProducto");
                        grid.SetValue(iRow, (int)Cols.Codigo, sIdProGrid);
                        grid.SetValue(iRow, (int)Cols.Cantidad, 0);

                        grid.BloqueaCelda(false, Color.WhiteSmoke, iRow, (int)Cols.Cantidad);
                        grid.BloqueaCelda(true, Color.WhiteSmoke, iRow, (int)Cols.CodEAN);
                        grid.SetActiveCell(iRow, (int)Cols.ClaveLote);
                    }
                }
                else
                {
                    General.msjUser("El producto ya fue capturado en otro renglon, verifique.");
                    grid.LimpiarRenglon(iRow);
                    grid.SetActiveCell(iRow, iColEAN);
                    grid.EnviarARepetido();
                }
            }

            return bRegresa;
        }

        private bool validarLote()
        {
            bool bRegresa = true;
            int iRow = grid.ActiveRow ;
            int iColEAN = (int)Cols.CodEAN;
            string sClaveLote = grid.GetValue(iRow, (int)Cols.ClaveLote); 

            //if (sValorGrid != sCodEAN)
            {
                if (!grid.BuscaRepetido(sClaveLote, iRow, (int)Cols.ClaveLote))
                {
                    if (!BuscarDatosLote(sClaveLote))
                    {
                        grid.SetValue(iRow, (int)Cols.ClaveLote, "");
                        grid.SetActiveCell(iRow, (int)Cols.ClaveLote);
                    }
                    else 
                    {
                        //// No modificar la informacion capturada en el renglon si este ya existia
                        grid.SetValue(iRow, (int)Cols.ClaveLote, sClaveLote);

                        grid.BloqueaCelda(true, Color.WhiteSmoke, iRow, (int)Cols.ClaveLote);
                        grid.BloqueaCelda(false, Color.WhiteSmoke, iRow, (int)Cols.Cantidad);
                        grid.SetActiveCell(iRow, (int)Cols.ClaveLote);
                    }
                }
                else
                {
                    General.msjUser("La Clave Lote ya fue capturada en otro renglon, verifique.");
                    grid.LimpiarRenglon(iRow);
                    grid.SetActiveCell(iRow, iColEAN);
                    grid.EnviarARepetido();
                }
            }

            return bRegresa;
        }

        private bool BuscarDatosLote(string ClaveLote)
        {
            bool bRegresa = false;
            string sSql = string.Format(" Select IdSubFarmacia, ClaveLote " +
                   " From FarmaciaProductos_CodigoEAN_Lotes (NoLock) " +
                   " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and ClaveLote = '{3}' ",
                   DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, ClaveLote);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "BuscarDatosLote()");
                General.msjError("Ocurrió un error al validar la Clave de Lote.");
            }
            else
            {
                // bRegresa = myLeer.Leer(); 
                bRegresa = true;
                if (!leer.Leer())
                {
                    bRegresa = false;
                    General.msjUser(" No se encontro la Clave Lote solicitado, verifique.");
                }
            }

            return bRegresa;
        }
        #endregion Manejo Grid 

        #region TAG 
        private void txtTAG_Validating(object sender, CancelEventArgs e)
        {
            lblStatus.Visible = false; 

            if (txtTAG.Text.Trim() == "")
            {
            }
            else
            {
                if (!validarTAG())
                {
                    lblStatus.Visible = true;
                    lblStatus.Text = "NUEVA CAPTURA";

                    txtTAG.Enabled = false;
                    IniciarToolBar(true, false, false);
                    grid.BloqueaGrid(false);
                    grid.Limpiar(true);
                    grid.SetActiveCell(1, (int)Cols.CodEAN);
                }
            }
        }

        private bool validarTAG()
        {
            bool bRegresa = true;
            string sSql = string.Format(
                "Select  IdEmpresa, IdEstado, IdFarmacia, Folio, TAG, IdPersonalRegistra, FechaRegistro, Status " +
                "From RFID_Tags_Enc (NoLock) " + 
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and TAG = '{3}' ", 
                sEmpresa, sEstado, sFarmacia, txtTAG.Text.Trim() ) ;

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "validarTAG()");
                General.msjError("Ocurrió al obtener la información del TAG");
            }
            else
            {
                if (!leer.Leer())
                {
                    bRegresa = false;
                    ////General.msjUser("TAG no encontrado.");
                }
                else
                {
                    txtTAG.Enabled = false;
                    txtTAG.Text = leer.Campo("TAG");
                    sFolio_TAG = leer.Campo("Folio"); 
                    dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");

                    ObtenerDetalles_TAG();
                    IniciarToolBar(false, false, true); 
                }
            }

            return bRegresa; 
        }

        private bool ObtenerDetalles_TAG()
        {
            bool bRegresa = false;
            string sSql = string.Format(
                "Select T.CodigoEAN, T.IdProducto, P.Descripcion, T.ClaveLote, T.Cantidad  " +
                "From RFID_Tags_Det T (NoLock) " + 
                "Inner Join vw_Productos_CodigoEAN P (NoLock) On ( T.IdProducto = P.IdProducto and T.CodigoEAN = P.CodigoEAN ) " +
                "Where T.IdEmpresa = '{0}' and T.IdEstado = '{1}' and T.IdFarmacia = '{2}' and T.Folio = '{3}' " +
                "Order by T.Renglon ",
                sEmpresa, sEstado, sFarmacia, sFolio_TAG);
            
            grid.BloqueaGrid(true);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerDetalles_TAG()");
                General.msjError("Ocurrió un error al obtener los detalles del TAG");
            }
            else
            {
                if (leer.Leer())
                {
                    grid.BloqueaGrid(false);
                    grid.LlenarGrid(leer.DataSetClase);
                    grid.BloqueaRenglon(true); 
                }
            }

            return bRegresa;
        }
        #endregion TAG

        private void FrmRFID_ProductosRelacionados_Shown(object sender, EventArgs e)
        {
            if (!DtGeneral.EsModuloUnidosis())
            {
                this.Close();
            }
        }
    }
}
