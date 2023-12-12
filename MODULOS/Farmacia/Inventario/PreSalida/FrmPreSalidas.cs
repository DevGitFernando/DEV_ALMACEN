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

namespace Farmacia.Inventario
{
    public partial class FrmPreSalidas : FrmBaseExt
    {

        private enum Cols
        {
            Ninguno = 0, IdClaveSSA = 1, ClaveSSA = 2, Descripcion = 3, Cantidad = 4
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        clsConsultas query;
        clsAyudas ayuda;        

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();

        private bool bPermitirCaptura = false;
        private string sValorGrid = "", sFolioPreSalida = "", sMensaje = "";

        private string sEmpresa = DtGeneral.EmpresaConectada;
        private string sEstado = DtGeneral.EstadoConectado;
        private string sFarmacia = DtGeneral.FarmaciaConectada;
        private string sIdPersonal = DtGeneral.IdPersonal;

        public FrmPreSalidas()
        {
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            Grid = new clsGrid(ref grdClavesSSA, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            //Grid.Limpiar(false);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            // conexionWeb.Url = General.Url;
        }

        private void FrmPreSalidas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }       

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                if (cnn.Abrir())
                {
                    bool bExito = false; 
                    cnn.IniciarTransaccion();

                    if (GuardarEncabezado(1))
                    {
                        bExito = GrabaDistribucionPreSalida();
                    } 

                    if (!bExito)
                    {
                        cnn.DeshacerTransaccion(); 
                        Error.GrabarError(leer, "btnGuardar_Click"); 
                        General.msjError("Ocurrió un error al grabar la PreSalida.");     
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje);
                        Imprimir(true);
                        txtFolio_Validating(null, null);
                        //LimpiaPantalla();
                    }

                    cnn.Cerrar();
                }
                else
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                if (cnn.Abrir())
                {
                    bool bExito = true;
                    cnn.IniciarTransaccion(); 
                    bExito = ActualizarStatus(2);

                    if (!bExito)
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar la PreSalida."); 
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje);
                        txtFolio_Validating(null, null);
                        //LimpiaPantalla();
                    }

                    cnn.Cerrar();
                }
                else
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(false);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (cnn.Abrir())
            {
                bool bExito = true;

                cnn.IniciarTransaccion(); 
                bExito = ActualizarStatus(4); 
 
                if (!bExito)
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnGuardar_Click");
                    General.msjError("Ocurrió un error al grabar la PreSalida."); 
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser(sMensaje);
                    txtFolio_Validating(null, null);
                    //LimpiaPantalla();
                }

                cnn.Cerrar();
            }
            else
            {
                Error.LogError(cnn.MensajeError);
                General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
            }
        }

        #endregion Botones

        #region Funciones

        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            dtpFechaRegistro.Enabled = false;
            Grid.Limpiar(true);

            validaCaptura();

            CargaSubFarmacias();
            IniciarToolBar(true, true, false, false, false);
            txtFolio.Focus();
        }

        private void CargaSubFarmacias()
        {
            cboSubFarmacias.Clear();
            cboSubFarmacias.Add("0", "<< Seleccione >>");

            leer.DataSetClase = query.SubFarmacias(sEstado, sFarmacia, "", "CargaSubFarmacias()");

            if (leer.Leer())
            {
                cboSubFarmacias.Add(leer.DataSetClase, true);
            }

            cboSubFarmacias.SelectedIndex = 0;
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de PreSalida inválido, verifique.");
                txtFolio.Focus();
            }

            if (bRegresa && cboSubFarmacias.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser(" No Selecciono la SubFarmacia, Verifique.... ");
                cboSubFarmacias.Focus();
            }

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las observaciones para la PreSalida, verifique.");
                txtObservaciones.Focus();
            }

            if (bRegresa)
            {
                bRegresa = ValidarCapturaClaves();
            }           

            return bRegresa;
        }

        private bool ValidarCapturaClaves()
        {
            bool bRegresa = true;

            if (Grid.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                if (Grid.GetValue(1, (int)Cols.Descripcion) == "")
                {
                    bRegresa = false;
                }
                else
                {
                    for (int i = 1; i <= Grid.Rows; i++)
                    {
                        if (Grid.GetValue(i, (int)Cols.ClaveSSA) != "" && Grid.GetValueInt(i, (int)Cols.Cantidad) == 0)
                        {
                            bRegresa = false;
                            break;
                        }
                    }                   
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos un Clave para la PreSalida\n y/o capturar cantidades para al menos una Clave, Verifique....");
            }

            return bRegresa;

        }

        private bool GuardarEncabezado(int iOpcion)
        {
            bool bRegresa = false; 
            ////string sMsj = "Ocurrió un error al guardar la información";

            ////if (iOpcion == 2)
            ////{
            ////    sMsj = "Ocurrió un error al cancelar la información";
            ////}

            string sSql = string.Format(" Exec spp_Mtto_PreSalidasPedidosEnc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                                        sEmpresa, sEstado, sFarmacia, cboSubFarmacias.Data, txtFolio.Text, sIdPersonal,
                                        txtObservaciones.Text, iOpcion);

            if (!leer.Exec(sSql))
            {
                // Error.GrabarError(leer, "GuardarEncabezado()");
                // General.msjError(sMsj); 
            }
            else
            {
                leer.Leer();
                sMensaje = leer.Campo("Mensaje");
                sFolioPreSalida = leer.Campo("Clave");
                txtFolio.Text = sFolioPreSalida;
                bRegresa = GrabarPreSalidaDetalle();
            }

            return bRegresa;
        }

        private bool GrabarPreSalidaDetalle()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdClaveSSA = "";
            int iCantidad = 0;          


            for (int i = 1; i <= Grid.Rows; i++)
            {
                sIdClaveSSA = Grid.GetValue(i, (int)Cols.IdClaveSSA);                
                iCantidad = Grid.GetValueInt(i, (int)Cols.Cantidad);

                if (sIdClaveSSA != "")
                {
                    sSql = string.Format("Exec spp_Mtto_PreSalidasPedidosDet '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                              sEmpresa, sEstado, sFarmacia, sFolioPreSalida, sIdClaveSSA, iCantidad );

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    } 
                }
            }

            return bRegresa;
        }

        private bool GrabaDistribucionPreSalida()
        {
            bool bRegresa = false; 
            string sSql = string.Format(" Exec spp_Mtto_PreSalidasPedidosDet_Lotes_Ubicaciones '{0}', '{1}', '{2}', '{3}', '{4}' ",
                                        sEmpresa, sEstado, sFarmacia, cboSubFarmacias.Data, sFolioPreSalida );

            bRegresa = leer.Exec(sSql); 
            return bRegresa;
        }

        private void CargaDetalleClaves()
        {
            string sSql = string.Format(" Select P.IdClaveSSA, C.ClaveSSA, C.Descripcion, P.CantidadRequerida " +
	                                    " From PreSalidasPedidosDet P (Nolock)  " +
	                                    " Inner Join CatClavesSSA_Sales C (Nolock) " +
                                            " On ( P.IdClaveSSA = C.IdClaveSSA_Sal ) " +
	                                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioPreSalida = '{3}' ",
                                            sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8));
            Grid.Limpiar(false); 
            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un Error al Consultar la Información");
            }
            else
            {
                if (leer.Leer())
                {
                    Grid.LlenarGrid(leer.DataSetClase);
                    Grid.BloqueaGrid(true);
                    bPermitirCaptura = false;
                }
            }
        }

        private bool ActualizarStatus(int iStatus)
        {
            bool bContinua = true;

            string sSql = string.Format(" Exec spp_Mtto_Status_PreSalidasPedidos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                                        sEmpresa, sEstado, sFarmacia, cboSubFarmacias.Data, txtFolio.Text, iStatus);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ActualizarStatus(int iStatus)");
                General.msjError("Ocurrió un Error al actualizar el Status");
                bContinua = false;
            }
            else
            {
                leer.Leer();
                sMensaje = leer.Campo("Mensaje");
            }

            return bContinua;
        }

        private void IniciarToolBar(bool bNuevo, bool bGuardar, bool bCancelar, bool bImprimir, bool bEjecutar)
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
            btnImprimir.Enabled = bImprimir;
            btnEjecutar.Enabled = bEjecutar;
        }
        #endregion Funciones

        #region Eventos

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            //string sFolio = "";

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Text = "*";
                txtFolio.Enabled = false;
                cboSubFarmacias.Focus();
                IniciarToolBar(true, true, false, false, false);
            }
            else
            {
                string sSql = string.Format(" Select * From PreSalidasPedidosEnc (Nolock) " + 
                                            " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioPreSalida = '{3}' ",
                                            sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8) );

                if (!leer.Exec(sSql))
                {
                    General.msjError("Ocurrió un Error al Consultar la Información");                  
                }
                else
                {
                    if (!leer.Leer())
                    {
                        General.msjAviso(" Clave de Folio No Encontrada, Verifique... ");
                        txtFolio.Focus();
                    }
                    else 
                    {
                        txtFolio.Text = leer.Campo("FolioPreSalida");
                        txtFolio.Enabled = false;
                        cboSubFarmacias.Data = leer.Campo("IdSubFarmacia");
                        cboSubFarmacias.Enabled = false;
                        txtObservaciones.Text = leer.Campo("Observaciones");
                        txtObservaciones.Enabled = false;

                        if (leer.Campo("Status") == "C")
                        {
                            lblCancelado.Visible = true;
                            //lblCancelado.Enabled = true;
                            lblCancelado.Text = "CANCELADO";
                            IniciarToolBar(true, false, false, true, false);
                        }
                        if (leer.Campo("Status") == "P")
                        {
                            lblCancelado.Visible = true;
                            //lblCancelado.Enabled = true;
                            lblCancelado.Text = "PROCESADO";
                            IniciarToolBar(true, false, true, true, true);
                        }

                        if (leer.Campo("Status") == "T")
                        {
                            lblCancelado.Visible = true;
                            //lblCancelado.Enabled = true;
                            lblCancelado.Text = "TERMINADO";
                            IniciarToolBar(true, false, false, true, false);
                        }

                        CargaDetalleClaves();
                    }
                }
            }
        }

        private void cboSubFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////if (cboSubFarmacias.SelectedIndex != 0)
            ////{
            ////    cboSubFarmacias.Enabled = false;
            ////    txtObservaciones.Focus();
            ////}
        }

        #endregion Eventos

        #region Eventos_Grid

        private void validaCaptura()
        {
            bPermitirCaptura = lblCancelado.Visible == false;
        }

        private void grdClavesSSA_KeyDown(object sender, KeyEventArgs e)
        {
            string sClaveSSA = "";
            int iRenglon = Grid.ActiveRow;

            if (e.KeyCode == Keys.F1 & bPermitirCaptura)
            {
                leer.DataSetClase = ayuda.ClavesSSA_Sales("grdClavesSSA_KeyDown");
                if (leer.Leer())
                {
                    sClaveSSA = leer.Campo("ClaveSSA");
                    if (!Grid.BuscaRepetido(sClaveSSA, iRenglon, (int)Cols.ClaveSSA))
                    {
                        Grid.SetValue(iRenglon, (int)Cols.IdClaveSSA, leer.Campo("IdClaveSSA_Sal"));
                        Grid.SetValue(iRenglon, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                        Grid.SetValue(iRenglon, (int)Cols.Descripcion, leer.Campo("Descripcion"));
                        Grid.SetActiveCell(iRenglon, (int)Cols.Cantidad);
                    }
                    else
                    {
                        General.msjAviso(" La Clave ya se encuentra capturada en otro renglon.");
                        Grid.SetValue(iRenglon, (int)Cols.ClaveSSA, "");
                        Grid.SetActiveCell(iRenglon, (int)Cols.ClaveSSA);
                    }
                }
            }
        }

        private void grdClavesSSA_EditModeOff(object sender, EventArgs e)
        {
            Cols columna = (Cols)Grid.ActiveCol;
            switch (columna)
            {
                case Cols.ClaveSSA:
                    {
                        CargaClavesSSA();
                    }
                    break;
            }
        }

        private void grdClavesSSA_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = Grid.GetValue(Grid.ActiveRow, (int)Cols.ClaveSSA);
        }

        private void grdClavesSSA_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (bPermitirCaptura)
            {
                if ((Grid.ActiveRow == Grid.Rows) && e.AdvanceNext)
                {
                    if (Grid.GetValue(Grid.ActiveRow, (int)Cols.ClaveSSA) != "" && Grid.GetValueInt(Grid.ActiveRow, (int)Cols.Cantidad) != 0)
                    {
                        Grid.Rows = Grid.Rows + 1;
                        Grid.ActiveRow = Grid.Rows;
                        Grid.SetActiveCell(Grid.Rows, (int)Cols.ClaveSSA);
                    }
                }
            }
        }

        #endregion Eventos_Grid

        #region Funciones_Grid

        private void CargaClavesSSA()
        {
            string sDato = "", sClaveSSA = "";
            int iRenglon = Grid.ActiveRow;

            sDato = Grid.GetValue(iRenglon, (int)Cols.ClaveSSA);

            if (sDato != "")
            {
                leer.DataSetClase = query.ClavesSSA_Sales(sDato, true, "CargaClavesSSA");
                if (leer.Leer())
                {
                    sClaveSSA = leer.Campo("ClaveSSA");
                    if (!Grid.BuscaRepetido(sClaveSSA, iRenglon, (int)Cols.ClaveSSA))
                    {
                        Grid.SetValue(iRenglon, (int)Cols.IdClaveSSA, leer.Campo("IdClaveSSA_Sal"));
                        Grid.SetValue(iRenglon, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                        Grid.SetValue(iRenglon, (int)Cols.Descripcion, leer.Campo("Descripcion"));
                        Grid.SetActiveCell(iRenglon, (int)Cols.Cantidad);
                    }
                    else
                    {
                        General.msjAviso(" La Clave ya se encuentra capturada en otro renglon.");
                        Grid.SetValue(iRenglon, (int)Cols.ClaveSSA, "");
                        Grid.SetActiveCell(iRenglon, (int)Cols.ClaveSSA);
                    }
                }
            }
        }
        #endregion Funciones_Grid

        #region Impresion

        private bool validarImpresion(bool Confirmar)
        {
            bool bRegresa = true;

            if (Confirmar)
            {
                if (General.msjConfirmar(" ¿ Desea imprimir la información en pantalla ? ") == DialogResult.No)
                {
                    bRegresa = false;
                }
            }

            if (bRegresa)
            {
                if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
                {
                    bRegresa = false;
                    General.msjUser("Folio de PreSalida inválido, verifique.");
                }
            }

            return bRegresa;
        }

        private void Imprimir(bool ConfirmarImpresion)
        {
            bool bRegresa = false;
            if (validarImpresion(ConfirmarImpresion))
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_PreSalidas_UbicacionesAsignadas.rpt";                

                myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("IdSubFarmacia", cboSubFarmacias.Data);
                myRpt.Add("Folio", txtFolio.Text);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

                ////if (General.ImpresionViaWeb)
                ////{
                ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////    DataSet datosC = DatosCliente.DatosCliente();

                ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                ////}
                ////else
                ////{
                ////    myRpt.CargarReporte(true);
                ////    bRegresa = !myRpt.ErrorAlGenerar;
                ////}

                if (!bRegresa)
                {                    
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        #endregion Impresion        
        
    }
}
