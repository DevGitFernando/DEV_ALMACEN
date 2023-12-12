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

namespace Facturacion.Licitaciones
{
    public partial class FrmLCTN_PreciosClavesSSA : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        clsAyudas Ayudas;
        clsConsultas Consultas;
        
        clsDatosCliente DatosCliente;
        clsGrid grid;

        string sFormato = "#,###,###,##0.#0";

        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, Descripcion = 2, Año = 3, PrecioAdmon = 4, PrecioBase = 5,
            Porcentaje = 6, PrecioNeto = 7, DescStatus = 8, Status = 9
        }

        public FrmLCTN_PreciosClavesSSA()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);            
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "Precios_ClavesSSA");

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);            

            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);

            grid = new clsGrid(ref grdClaves, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
        }

        private void FrmLCTN_PreciosClavesSSA_Load(object sender, EventArgs e)
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

            if (validaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardaInformacion(1);

                    if (!bContinua)
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Información guardada satisfactoriamente.");
                        btnNuevo_Click(null, null);
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No fue posible establecer conexión con el servidor, intente de nuevo.");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (validaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardaInformacion(2);

                    if (!bContinua)
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al Cancelar la información.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Información Cancelada satisfactoriamente.");
                        btnNuevo_Click(null, null);
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No fue posible establecer conexión con el servidor, intente de nuevo.");
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion();
        }
        #endregion Botones

        #region Eventos
        private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveSSA.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.ClavesSSA_Sales(txtClaveSSA.Text.Trim(), true, "txtClaveSSA_Validating");

                if (leer.Leer())
                {
                    CargaDatos();
                    BuscarDatosPrecios();
                    FrameClaveSSA.Enabled = false;
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.ClavesSSA_Sales("txtClaveSSA_KeyDown");

                if (leer.Leer())
                {
                    CargaDatos();
                    BuscarDatosPrecios();
                    FrameClaveSSA.Enabled = false;
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }

            if (e.KeyCode == Keys.F2)
            {
                leer.DataSetClase = Ayudas.ClavesSSA_Sales_PreciosCauses("txtClaveSSA_KeyDown");

                if (leer.Leer())
                {
                    CargaDatos();
                    BuscarDatosPrecios();
                    FrameClaveSSA.Enabled = false;
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void CargaDatos()
        {
            txtClaveSSA.Text = leer.Campo("ClaveSSA");            
            lblDescripcion.Text = leer.Campo("DescripcionSal");
            cboPresentaciones.Data = leer.Campo("IdPresentacion");
            txtContenido.Text = leer.Campo("ContenidoPaquete");

            ////if (leer.Campo("Status") == "C")
            ////{
            ////    lblCancelado.Visible = true;
            ////    lblCancelado.Text = "CANCELADA";
            ////}

            if (leer.CampoBool("EsControlado"))
            {
                chkMedicamento.Checked = true;
            }

            if (leer.CampoBool("EsAntibiotico"))
            {
                chkAntibiotico.Checked = true;
            }
        }

        private void txtPrecioBase_Validating(object sender, CancelEventArgs e)
        {
            CalcularPrecio();
        }

        private void txtPrecioBase_TextChanged(object sender, EventArgs e)
        {
            CalcularPrecio();
        }

        private void nudPorcentaje_Validating(object sender, CancelEventArgs e)
        {
            CalcularPrecio();
        }

        private void nudPorcentaje_ValueChanged(object sender, EventArgs e)
        {
            CalcularPrecio();
        }
        #endregion Eventos

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            FrameClaveSSA.Enabled = true;
            grid.Limpiar(true);
            LlenaPresentaciones();
            lblCancelado.Visible = false;
            txtPrecioNeto.Enabled = false;
            nudPorcentaje.Value = 0M;
            nudAño.Value = nudAño.Minimum;
            chkMedicamento.Checked = false;
            chkAntibiotico.Checked = false;
            txtClaveSSA.Focus();
        }

        private void LlenaPresentaciones()
        {
            cboPresentaciones.Clear();
            cboPresentaciones.Add("0", "<< Seleccione >>");
            leer.DataSetClase = Consultas.ComboPresentaciones("LlenaPresentaciones");

            if (leer.Leer())
            {
                cboPresentaciones.Add(leer.DataSetClase, true);
            }
            cboPresentaciones.SelectedIndex = 0;
        }

        private void CalcularPrecio()
        {
            double iPorcentaje = 0;
            double iPrecioBase = 0;
            double iPrecioNeto = 0;

            try
            {
                iPorcentaje = Convert.ToDouble(nudPorcentaje.Value) / 100;
            }
            catch
            {
                iPorcentaje = 0;
            }
            try
            {
                iPrecioBase = Convert.ToDouble(txtPrecioBase.Text);
            }
            catch
            {
                iPrecioBase = 0;
            }

            iPrecioNeto = iPrecioBase * (1 + iPorcentaje);

            txtPrecioNeto.Text = iPrecioNeto.ToString(sFormato);
            

        }

        private void BuscarDatosPrecios()
        {
                        
            string sSql = string.Format(" Select ClaveSSA, Descripcion, Año, PrecioAdmon, PrecioBase, Porcentaje, PrecioNeto, " +
                                        " DescStatus, Status " +
                                        " From vw_CatClavesSSA_Causes (Nolock) Where ClaveSSA = '{0}' Order By Año ", txtClaveSSA.Text);

            grid.Limpiar(false);
            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                }
            }
        }
        #endregion Funciones

        #region GuardaPrecios
        private bool GuardaInformacion(int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "";
            int iEsControlado = 0, iEsAntibiotico = 0;

            if (chkMedicamento.Checked)
            {
                iEsControlado = 1;
            }

            if (chkAntibiotico.Checked)
            {
                iEsAntibiotico = 1;
            }

            sSql = string.Format(" Exec spp_Mtto_CatClavesSSA_Causes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                                txtClaveSSA.Text, lblDescripcion.Text, cboPresentaciones.Data, txtContenido.Text, iEsControlado, iEsAntibiotico, 
                                nudAño.Value, txtPrecioBase.NumericText, nudPorcentaje.Value, txtPrecioAdmon.NumericText, txtPrecioNeto.NumericText, 
                                iOpcion);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        private bool validaDatos()
        {
            bool bRegresa = true;

            if (txtClaveSSA.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave SSA inválida, verifique.");
                txtClaveSSA.Focus();
            }

            return bRegresa;
        }
        #endregion GuardaPrecios

        #region Eventos_Grid
        private void grdClaves_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sStatus = "";

            txtPrecioBase.Text = grid.GetValueDou(grid.ActiveRow, (int)Cols.PrecioBase).ToString(sFormato);
            nudPorcentaje.Value = grid.GetValueInt(grid.ActiveRow, (int)Cols.Porcentaje);
            txtPrecioAdmon.Text = grid.GetValueDou(grid.ActiveRow, (int)Cols.PrecioAdmon).ToString(sFormato);
            nudAño.Value = grid.GetValueInt(grid.ActiveRow, (int)Cols.Año);
            sStatus = grid.GetValue(grid.ActiveRow, (int)Cols.Status);

            if (sStatus == "C")
            {
                lblCancelado.Visible = true;
                lblCancelado.Text = "CANCELADA";
            }
            else
            {
                lblCancelado.Visible = false;
            }

            txtPrecioBase_TextChanged(null, null);
            nudPorcentaje_ValueChanged(null, null);
        }
        #endregion Eventos_Grid

        #region Impresion
        private void ImprimirInformacion()
        {
            bool bRegresa = true;

            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;            

            myRpt.RutaReporte = DtGeneral.RutaReportes;

            myRpt.NombreReporte = "Rpt_Fact_PreciosClavesSSACauses";           

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
            

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Impresion
    }
}
