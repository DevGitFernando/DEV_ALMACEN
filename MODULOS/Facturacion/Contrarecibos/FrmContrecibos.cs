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

namespace Facturacion.Contrarecibos
{
    public partial class FrmContrecibos : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerFactura;

        clsAyudas Ayudas;
        clsConsultas Consultas;

        clsGrid Grid;
        clsDatosCliente DatosCliente;

        enum Cols
        {
            FolioFactura = 1, NumFactura = 2, Fecha = 3, TipoFactura = 4, Importe = 5 
        }

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        string sFolio = "";
        string sMensaje = "";
        string sValorGrid = "";
        bool bBloquearGrid = false;

        public FrmContrecibos()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerFactura = new clsLeer(ref cnn);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "ContraRecibos");

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);

            Grid = new clsGrid(ref grdFacturas, this);
            Grid.BackColorColsBlk = Color.White;
            grdFacturas.EditModeReplace = true;

            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);
        }

        private void FrmContrecibos_Load(object sender, EventArgs e)
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
            bool bContinua = false;

            EliminarRenglonesVacios();
            
            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardaEncabezado();

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        txtFolio.Text = sFolio;
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        //Imprimir();
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

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            Grid.BloqueaGrid(false);
            Grid.Limpiar(true);
            dtpFechaRegistro.Enabled = false;
            HabilitarBotones(true, false, false);
            bBloquearGrid = false;
            txtFolio.Focus();
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Contrarecibo inválido, verifique.");
                txtFolio.Focus();
            }            

            if (bRegresa && txtContraRecibo.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Contrarecibo, verifique.");
                txtContraRecibo.Focus();
            }

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las Observaciones, verifique.");
                txtObservaciones.Focus();
            }

            return bRegresa;
        }

        private void HabilitarControles(bool bValor)
        {
            txtFolio.Enabled = bValor;
            txtContraRecibo.Enabled = bValor;
            txtObservaciones.Enabled = bValor;
            //dtpFechaRegistro.Enabled = bValor;
            dtpFechaDoc.Enabled = bValor;
        }

        private void HabilitarBotones(bool Nuevo, bool Guardar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnImprimir.Enabled = Imprimir;
        }
        #endregion Funciones

        #region Guardar
        private bool GuardaEncabezado()
        {
            bool bRegresa = true;
            int iOpcion = 0;

            iOpcion = 1;

            string sSql = string.Format("Exec spp_Mtto_FACT_Contrarecibos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
                                        sEmpresa, sEstado, sFarmacia, txtFolio.Text, txtContraRecibo.Text, General.FechaYMD(dtpFechaDoc.Value, "-"), 
                                        txtObservaciones.Text, DtGeneral.IdPersonal, iOpcion);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    sFolio = leer.Campo("Folio");                    
                    sMensaje = leer.Campo("Mensaje");
                    bRegresa = GuardaDetalles();
                }
            }
            return bRegresa;
        }

        private bool GuardaDetalles()
        {
            bool bRegresa = true;
            string sSql = "";
            string sFolioFactura = "";            

            for (int i = 1; i <= Grid.Rows; i++)
            {
                // Se obtienen los datos para la insercion.
                sFolioFactura = Grid.GetValue(i, (int)Cols.FolioFactura);

                if (sFolioFactura != "")
                {
                    sSql = String.Format(" Exec spp_Mtto_FACT_Contrarecibos_Detalles '{0}', '{1}', '{2}', '{3}', '{4}' ",
                                        sEmpresa, sEstado, sFarmacia, sFolio, sFolioFactura);

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

        #region Eventos_Grid
        private void grdFacturas_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((Grid.ActiveRow == Grid.Rows) && e.AdvanceNext)
            {
                if (!bBloquearGrid)
                {
                    if (Grid.GetValue(Grid.ActiveRow, (int)Cols.FolioFactura) != "" && Grid.GetValueDou(Grid.ActiveRow, (int)Cols.Importe) != 0)
                    {
                        Grid.Rows = Grid.Rows + 1;
                        Grid.ActiveRow = Grid.Rows;
                        Grid.SetActiveCell(Grid.Rows, (int)Cols.FolioFactura);
                    }
                }
            }
        }

        private void grdFacturas_KeyDown(object sender, KeyEventArgs e)
        {
            if (Grid.ActiveCol == (int)Cols.FolioFactura )
            {
                // Habilitar la Ayuda en todo el Renglon 
                if (e.KeyCode == Keys.F1)
                {
                    leer.DataSetClase = Ayudas.FolioFacturas(sEmpresa, sEstado, sFarmacia, "grdFacturas_KeyDown");
                    if (leer.Leer())
                    {
                        Grid.SetValue(Grid.ActiveRow, (int)Cols.FolioFactura, leer.Campo("FolioFactura"));
                        CargaDatosFactura();
                    }

                }
            }

            if (e.KeyCode == Keys.Delete)
            {
                Grid.DeleteRow(Grid.ActiveRow);

                if (Grid.Rows == 0)
                    Grid.Limpiar(true);
            }
        }

        private void grdFacturas_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = Grid.GetValue(Grid.ActiveRow, (int)Cols.FolioFactura);
        }

        private void grdFacturas_EditModeOff(object sender, EventArgs e)
        {
            Cols iCol = (Cols)Grid.ActiveCol;

            switch (iCol)
            {
                case Cols.FolioFactura:
                    ObtenerDatosFactura();
                    break;
            }
        }

        private void ObtenerDatosFactura()
        {
            string sFactura = "";

            sFactura = Grid.GetValue(Grid.ActiveRow, (int)Cols.FolioFactura);

            if (sFactura.Trim() != "")
            {
                leer.DataSetClase = Consultas.FolioFacturas(sEmpresa, sEstado, sFarmacia, sFactura, "ObtenerDatosFactura()");
                {
                    if (!leer.Leer())
                    {
                        //General.msjUser("El Folio de Factura no Existe...");
                        Grid.LimpiarRenglon(Grid.ActiveRow);
                    }
                    else
                    {
                        if (!RevisarFolioFactura())
                        {
                            CargaDatosFactura();
                        }
                        else
                        {
                            Grid.LimpiarRenglon(Grid.ActiveRow);
                        }
                    }
                }
            }
        }

        private void CargaDatosFactura()
        {
            int iRowActivo = Grid.ActiveRow;

            if (!Grid.BuscaRepetido(leer.Campo("FolioFactura"), iRowActivo, (int)Cols.FolioFactura))
            {
                Grid.SetValue(iRowActivo, (int)Cols.FolioFactura, leer.Campo("FolioFactura"));
                Grid.SetValue(iRowActivo, (int)Cols.NumFactura, leer.Campo("FolioFacturaElectronica"));
                Grid.SetValue(iRowActivo, (int)Cols.Fecha, leer.Campo("FechaRegistro"));
                Grid.SetValue(iRowActivo, (int)Cols.TipoFactura, leer.Campo("TipoDeFacturaDesc"));
                Grid.SetValue(iRowActivo, (int)Cols.Importe, leer.Campo("Total"));
                Grid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.FolioFactura);
                //Grid.SetActiveCell(iRowActivo, (int)Cols.FolioFactura);

                Grid.Rows = Grid.Rows + 1;
                Grid.ActiveRow = Grid.Rows;
                Grid.SetActiveCell(Grid.Rows, (int)Cols.FolioFactura);

            }
            else
            {
                General.msjUser("Este Folio de Factura ya se encuentra capturado en otro renglón.");
                Grid.SetValue(Grid.ActiveRow, (int)Cols.FolioFactura, "");
                limpiarColumnas();
                Grid.SetActiveCell(Grid.ActiveRow, 1);
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
                if (Grid.GetValue(i, 1).Trim() == "") //Si la columna Folio Factura esta vacia se elimina.
                    Grid.DeleteRow(i);
            }

            if (Grid.Rows == 0) // Si No existen renglones, se inserta 1.
                Grid.AddRow();
        }

        private bool RevisarFolioFactura()
        {
            bool bRegresa = false;
            string sSql = "";
            string sFactura = "", sMsj = "";

            sFactura = Grid.GetValue(Grid.ActiveRow, (int)Cols.FolioFactura);

            sSql = string.Format(" Select * From vw_FACT_Contrarecibos_Detalles (Nolock) " +
                                 " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioFactura = '{3}' ",
                                 sEmpresa, sEstado, sFarmacia, Fg.PonCeros(sFactura, 10));
            if (!leerFactura.Exec(sSql))
            {
                Error.GrabarError(leerFactura, "RevisarFolioFactura");
                General.msjError("Ocurrió un error al obtener datos del Folio Factura.");
            }
            else
            {
                if (leerFactura.Leer())
                {
                    bRegresa = true;
                    sMsj = string.Format("El Folio de Factura : {0},\n\n" + 
                                        " Ya fue capturado en el ContraRecibo : {1}",
                                        leerFactura.Campo("FolioFactura"), leerFactura.Campo("Folio"));

                    General.msjAviso(sMsj);
                }                
            }

            return bRegresa;

        }
        #endregion Eventos_Grid        

        #region BuscarFolioContraRecibo
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "";

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Text = "*";
                txtFolio.Enabled = false;
                HabilitarBotones(true, true, true);
                txtContraRecibo.Focus();
            }
            else
            {
                sSql = string.Format(" Select * " +
                                     " From vw_FACT_Contrarecibos (Nolock) " +
                                     " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}'",
                                     sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 10));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtFolio_Validating");
                    General.msjError("Ocurrió un error al obtener datos del ContraRecibo.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        CargaEncContraRecibo();
                        CargaDetContraRecibo();
                        HabilitarBotones(true, false, true);
                        HabilitarControles(false);
                    }
                    else
                    {
                        General.msjAviso("No se encontro el ContraRecibo.. Verifique");
                        txtFolio.Focus();
                    }
                }
            }
        }

        private void CargaEncContraRecibo()
        {
            txtFolio.Text = leer.Campo("Folio");
            txtContraRecibo.Text = leer.Campo("Contrarecibo");
            dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");
            dtpFechaDoc.Value = leer.CampoFecha("FechaDocumento");
            txtObservaciones.Text = leer.Campo("Observaciones");
        }

        private void CargaDetContraRecibo()
        {
            string sSql = "";

            sSql = string.Format(" Select FolioFactura, NumFactura, FechaRegistro, TipoDeFacturaDesc, Importe " +
                                " From vw_FACT_Contrarecibos_Detalles (Nolock) " +	                            
                                     " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}'",
                                     sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 10));
            
            Grid.Limpiar(false);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargaDetContraRecibo");
                General.msjError("Ocurrió un error al obtener el detalle del ContraRecibo.");
            }
            else
            {
                if (leer.Leer())
                {
                    Grid.LlenarGrid(leer.DataSetClase);
                    Grid.BloqueaGrid(true);
                    bBloquearGrid = true;
                }
            }
        }
        #endregion BuscarFolioContraRecibo

        #region Impresion
        private void Imprimir()
        {
            bool bRegresa = true;

            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;            

            myRpt.RutaReporte = DtGeneral.RutaReportes;

            myRpt.NombreReporte = "Rpt_FACT_ContraRecibos";

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("Folio", txtFolio.Text);

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);


            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Impresion
    }
}
