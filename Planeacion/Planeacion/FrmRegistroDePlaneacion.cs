using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;


namespace Planeacion.ObtenerInformacion
{
    public partial class FrmRegistroDePlaneacion : FrmBaseExt
    {
        enum Cols
        {
            Ninguno, IdEstado, IdFarmacia, Folio, IdClaveSSA, ClaveSSA, Descripcion, Presentación, Contenido, Piezas, Rotacion, PCM, Consumo, Existencia, Existencia_Almacen, PiezasSugeridas, CajasSugeridas, CantidadRequerida, CantidadCompras, Mayor, Diferencia
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConsultas Consultas;
        clsLeer leer;
        clsLeer leerRegionales;
        clsGrid grid;

        bool bValidadoEstado = false;
        bool bValidadoCompra = false;

        public FrmRegistroDePlaneacion()
        {
            InitializeComponent();
            //General.Pantalla.AjustarTamaño(this, 90, 80);

            //lblDiasStockSeguridasd.Width = 58;
            //lblMesesCaducidad.Width = 58;
            //lblMesesInformacion.Width = 58;
            //lblMesesStockAlta.Width = 58;


            leer = new clsLeer(ref cnn);
            leerRegionales = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            grid = new clsGrid(ref grdConsumos, this);
            grid.AjustarAnchoColumnasAutomatico = true;
            grid.EstiloDeGrid = eModoGrid.ModoRow; 
            grid.SetOrder(true);

            grdConsumos.EditModeReplace = true; 

        }

        private void FrmRegistroDePlaneacion_Load(object sender, EventArgs e)
        {
            limpiar();
        }


        private void CargarConsumos()
        {
            
            bValidadoEstado = false;
            bValidadoCompra = false;

            

            string sSql = string.Format(" Exec spp_PLN_OCEN_CargarPedido @IdEstado = '{0}', @IdFarmacia = '{1}', @Folio = '{2}'",
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text.Trim());


            limpiar();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarConsumos()");
                General.msjError("Ocurrió un error al obtener la información.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("Folio no encontrado, Verifique.");
                    txtFolio.Text = "";
                    txtFolio.Focus();
                }
                else
                {
                    IniciarToolBar(true, true);

                    txtFolio.Text = leer.Campo("Folio");
                    txtFolio.Enabled = false;
                    lblMesesStockAlta.Text = leer.CampoInt("Meses_Stock_AltaRotacion").ToString();
                    lblMesesStockMedia.Text = leer.CampoInt("Meses_Stock_MediaRotacion").ToString();
                    lblMesesStockBaja.Text = leer.CampoInt("Meses_Stock_BajaRotacion").ToString();
                    lblDiasStockSeguridasd.Text = leer.CampoInt("Dias_StockSeguridad").ToString();

                    lblMesesCaducidad.Text = leer.CampoInt("Meses_Caducidad").ToString();

                    lblMesesInformacion.Text = leer.CampoInt("Meses_A_Promediar").ToString();

                    dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");
                    bValidadoEstado = leer.CampoBool("EstaAutorizaEstado");
                    bValidadoCompra = leer.CampoBool("EstaAutorizaCompras");

                    lblStatus.Text = "ACTIVO";

                    if (bValidadoEstado)
                    {
                        lblStatus.Text = "VALIDADO POR ESTADO";
                    }

                    if (bValidadoCompra)
                    {
                        lblStatus.Text = "VALIDADO POR COMPRAS";
                        IniciarToolBar(false, false);
                    }


                    leer.DataTableClase = leer.Tabla(2);

                    grid.Limpiar(false);
                    chkCantidades.Checked = true;

                    grid.LlenarGrid(leer.DataSetClase, false, false);

                    grid.OcultarColumna(!bValidadoEstado, Cols.CantidadCompras);

                    grid.AjustarAnchoColumnasAutomatico = true;


                    for (int i = 1; grid.Rows >= i; i++)
                    {
                        ColorRenglon(i);
                    }
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {

        }

        private void limpiar()
        {
            Fg.IniciaControles();
            grid.Limpiar(false);

            int iIncremento = 280;

            Point e = new Point(groupBox1.Right - iIncremento, chkCantidades.Location.Y);
            chkCantidades.Location = e;

            chkCantidades.Checked = true;


            txtFolio.Text = "";
            txtFolio.Enabled = true;

            lblMesesCaducidad.Text = "";
            lblMesesStockAlta.Text = "";
            lblDiasStockSeguridasd.Text = "";
            lblMesesInformacion.Text = "";
            

            dtpFechaRegistro.Enabled = false;

            IniciarToolBar(false, false);
        }

        private void IniciarToolBar(bool Guardar, bool Autorizar)
        {
            btnGuardar.Enabled = Guardar;
            btnAutorizar.Enabled = Autorizar;
        }

        private void grdConsumos_EditModeOff(object sender, EventArgs e)
        {
            ColorRenglon(grid.ActiveRow);
        }

        private void ColorRenglon(int Renglon)
        {
            int iMayor = 0;
            double dDiferencia = grid.GetValueDou(Renglon, Cols.CajasSugeridas) - grid.GetValueDou(Renglon, Cols.CantidadRequerida);
            

            grid.SetValue(Renglon, Cols.Diferencia, dDiferencia);

            if (dDiferencia > 0)
            {
                grid.ColorCelda(Renglon, Cols.CantidadRequerida, Color.Yellow);
                grid.ColorCelda(Renglon, Cols.Diferencia, Color.Yellow);
            }
            else
            {
                if (dDiferencia < 0)
                {
                    grid.ColorCelda(Renglon, Cols.CantidadRequerida, Color.Orange);
                    grid.ColorCelda(Renglon, Cols.Diferencia, Color.Orange);
                    iMayor = 1;
                }
                else
                {
                    grid.ColorCelda(Renglon, Cols.CantidadRequerida, Color.LightGreen);
                    grid.ColorCelda(Renglon, Cols.Diferencia, Color.LightGreen);
                }
            }


            if (bValidadoEstado)
            {
                dDiferencia = grid.GetValueInt(Renglon, Cols.CantidadRequerida) - grid.GetValueInt(Renglon, Cols.CantidadCompras);


                grid.SetValue(Renglon, Cols.Diferencia, dDiferencia);

                if (dDiferencia > 0)
                {
                    grid.ColorCelda(Renglon, Cols.CantidadCompras, Color.Yellow);
                    grid.ColorCelda(Renglon, Cols.Diferencia, Color.Yellow);
                }
                else
                {
                    if (dDiferencia < 0)
                    {
                        grid.ColorCelda(Renglon, Cols.CantidadCompras, Color.Orange);
                        grid.ColorCelda(Renglon, Cols.Diferencia, Color.Orange);
                        iMayor = 1;
                    }
                    else
                    {
                        grid.ColorCelda(Renglon, Cols.CantidadCompras, Color.LightGreen);
                        grid.ColorCelda(Renglon, Cols.Diferencia, Color.LightGreen);
                    }
                }
            }

            grid.SetValue(Renglon, Cols.Mayor, iMayor);

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar(false);
        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            Guardar(true);
        }

        private bool ValidaDatos()
        {
            bool bReregresa = false;
            bool bMayor = false;

            for (int i = 1; i <= grid.Rows && !bMayor; i++)
            {
                bMayor = grid.GetValueBool(i, Cols.Mayor) ? true : bMayor;
            }

            if (General.msjConfirmar("Se encontraron claves con cantidad mayor a la sugerida.\nDesea continuar ?") == DialogResult.Yes)
            {
                bReregresa = true;
            }

            return bReregresa;
            
        }

        private void Guardar(bool bAutoriza)
        {
            bool bContinua = true;

            string sSql = "";
            string sIdClaveSSA = "";
            int iCantidad = 0;

            string sIdPersonalAutoriza = string.Format("IdPersonalAutorizaEstado = '{0}', FechaAutorizaEstado = GETDATE(), EstaAutorizaEstado = 1 ", DtGeneral.IdPersonal);
            string sUpdateCantidadCompras = "";

            if (bValidadoEstado)
            {
                sIdPersonalAutoriza = string.Format("IdPersonalAutorizaCompras = '{0}', FechaAutorizaCompras = GETDATE(), EstaAutorizaCompras = 1 ", DtGeneral.IdPersonal);
            }

            if (ValidaDatos())
            {
                if (!cnn.Abrir())
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    cnn.IniciarTransaccion();

                    for (int i = 1; i <= grid.Rows && bContinua; i++)
                    {
                        sIdClaveSSA = grid.GetValue(i, Cols.IdClaveSSA);
                        iCantidad = grid.GetValueInt(i, Cols.CantidadRequerida);

                        if (bAutoriza)
                        {
                            sUpdateCantidadCompras = string.Format(", CantidadCompras = '{0}' ", iCantidad);
                        }


                        sSql = string.Format("Update D Set CantidadRequerida = {4}{5} From PLN_OCEN_PedidoDet D Where IdEstado = '{0}' And IdFarmacia = '{1}' And Folio = '{2}' And IdClaveSSA = '{3}' ",
                                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text, sIdClaveSSA, iCantidad, sUpdateCantidadCompras);

                        if (bValidadoEstado)
                        {
                            iCantidad = grid.GetValueInt(i, Cols.CantidadCompras);

                            sSql = string.Format("Update D Set CantidadCompras = {4} From PLN_OCEN_PedidoDet D Where IdEstado = '{0}' And IdFarmacia = '{1}' And Folio = '{2}' And IdClaveSSA = '{3}' ",
                                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text, sIdClaveSSA, iCantidad);
                        }

                        if (!leer.Exec(sSql))
                        {
                            bContinua = false;
                        }

                    }

                    if(bAutoriza && bContinua)
                    {
                        sSql = string.Format("Update D Set {3} From PLN_OCEN_PedidoEnc D Where IdEstado = '{0}' And IdFarmacia = '{1}' And Folio = '{2}' ", DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text, sIdPersonalAutoriza);

                        if (!leer.Exec(sSql))
                        {
                            bContinua = false;
                        }
                    }

                    if (bContinua)
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Información guardada exitosamente.");
                        limpiar();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrio un error al guardar la información.");

                    }

                    cnn.Cerrar();
                }

            }
        }

        private void chkCantidades_CheckedChanged(object sender, EventArgs e)
        {

            int iCantidad = 0;


            if (!bValidadoEstado)
            {
                for (int i = 1; i <= grid.Rows; i++)
                {
                    if (!chkCantidades.Checked)
                    {
                        iCantidad = 0;
                    }
                    else
                    {
                        iCantidad = grid.GetValueInt(i, Cols.CajasSugeridas);
                    }

                    grid.SetValue(i, Cols.CantidadRequerida, iCantidad);

                    ColorRenglon(i);
                }
            }
            else
            {
                for (int i = 1; i <= grid.Rows; i++)
                {
                    if (!chkCantidades.Checked)
                    {
                        iCantidad = 0;
                    }
                    else
                    {
                        iCantidad = grid.GetValueInt(i, Cols.CantidadRequerida);
                    }

                    grid.SetValue(i, Cols.CantidadCompras, iCantidad);

                    ColorRenglon(i);
                }
            }

        }

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolio.Text == "")
            {
                General.msjAviso("No ha seleccionado un estado, verifique por favor.");
            }
            else
            {
                CargarConsumos();
            }
        }

        private void nmMesesStock_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
