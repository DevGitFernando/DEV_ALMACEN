using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft;

namespace Farmacia.Ventas
{
    public partial class FrmReimpresionDeTicketsDetallado : FrmBaseExt
    {
        clsDatosCliente DatosCliente;
        DllFarmaciaSoft.Ventas.clsImprimirVentas VtasImprimir;
        clsConsultas query;
        clsLeer leer = new clsLeer();
        double dImporte = 0;

        bool bPermitirAjusteDePrecios = DtGeneral.PermisosEspeciales.TienePermiso(""); 
        Size sizeNormal = new Size(340, 165);
        bool bInicializado = false; 

        public FrmReimpresionDeTicketsDetallado()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "Impresion");
            VtasImprimir = new DllFarmaciaSoft.Ventas.clsImprimirVentas(General.DatosConexion, DatosCliente,
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                General.Url, GnFarmacia.RutaReportes, TipoReporteVenta.Ninguno);

            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);

            this.Height = 155;
            sizeNormal = new Size(340, 132);

            lblOpciones.Dock = DockStyle.None;
            lblOpciones.Visible = false;
            menuPrecios.Visible = false;
            menuPrecios.Enabled = false; 
            btnAplicarPrecios.Enabled = false; 

            chkMostrarPrecios.Checked = false;
            chkMostrarPrecios.Enabled = false;
            FrameDatos.Height = 72;
            this.Height = FrameDatos.Height + toolStripBarraMenu.Height; 

            if (!DtGeneral.EsEquipoDeDesarrollo)
            {
                btnExportarExcel.Visible = false; 
            }


            if (!DtGeneral.EsAlmacen)
            { 

            }
            else 
            {
                this.Height = 165; 
                sizeNormal = new Size(340, 165); 
                chkMostrarPrecios.Enabled = true; 
                
                // Ajuste de precios 
                this.Height = 176;
                sizeNormal = new Size(340, 176);

                FrameDatos.Height = 94; 

                lblOpciones.Dock = DockStyle.Bottom;
                lblOpciones.Visible = true;
                menuPrecios.Visible = true;
                menuPrecios.Enabled = true; 
            }

            this.Size = sizeNormal;
            FrameDatos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left; 
        }

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            btnImprimir.Enabled = false;
            btnAplicarPrecios.Enabled = false; 
            // rdoVentaContado.Checked = false;
            // rdoVentaCredito.Checked = false;

            if (txtFolio.Text.Trim() != "")
            {
                leer.DataSetClase = query.FolioEnc_Ventas(DtGeneral.EmpresaConectada,
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text, "txtFolio_Validating");
                if (leer.Leer())
                {
                    btnImprimir.Enabled = true;
                    btnAplicarPrecios.Enabled = true; 

                    txtFolio.Enabled = false;
                    txtFolio.Text = leer.Campo("Folio");
                    txtFolioFinal.Text = leer.Campo("Folio"); 
                    dImporte = leer.CampoDouble("Total");


                    if ((TipoDeVenta)leer.CampoInt("TipoDeVenta") == TipoDeVenta.Publico)
                    {
                        rdoVentaContado.Checked = true;
                    }

                    if ((TipoDeVenta)leer.CampoInt("TipoDeVenta") == TipoDeVenta.Credito)
                    {
                        rdoVentaCredito.Checked = true;
                    }

                    rdoVentaContado.Enabled = false;
                    rdoVentaCredito.Enabled = false;

                }
                else
                {
                    e.Cancel = true;
                    General.msjUser("Folio de venta no encontrado, verifique.");                    
                }
            }
        }

        private void txtFolioFinal_Validating(object sender, CancelEventArgs e)
        {
            btnImprimir.Enabled = false;
            // rdoVentaContado.Checked = false;
            // rdoVentaCredito.Checked = false;

            if (txtFolio.Text.Trim() != "")
            {
                leer.DataSetClase = query.FolioEnc_Ventas(DtGeneral.EmpresaConectada,
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolioFinal.Text, "txtFolio_Validating");
                if (leer.Leer())
                {
                    btnImprimir.Enabled = true; 
                    txtFolioFinal.Enabled = false;
                    txtFolioFinal.Text = leer.Campo("Folio"); 
                }
                else
                {
                    e.Cancel = true;
                    General.msjUser("Folio de venta no encontrado, verifique.");
                }
            }
        } 

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnImprimir.Enabled = false;
            Fg.IniciaControles();

            rdoVentaContado.Enabled = true;
            rdoVentaCredito.Enabled = true;

            rdoVentaContado.Checked = false;
            rdoVentaCredito.Checked = false;

            rdoVentaContado.Checked = false;
            rdoVentaCredito.Checked = false;

            btnAplicarPrecios.Enabled = false; 

            //// chkMostrarImpresionEnPantalla.Checked = false;
            chkMostrarImpresionEnPantalla.Checked = DtGeneral.EsAlmacen;
            SendKeys.Send("{TAB}"); 
            txtFolio.Focus();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(1); 
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            Imprimir(2); 
        }

        private void Imprimir(int Tipo)
        {
            ////txtFolio_Validating(this, null); 
            ////txtFolioFinal_Validating(this, null);

            if (txtFolio.Text.Trim() == "")
            {
                General.msjUser("No ha capturado un Folio de Venta para reeimpresión, verifique.");
            }
            else
            {
                if (rdoVentaContado.Checked)
                {
                    VtasImprimir.TipoDeReporte = TipoReporteVenta.Contado;
                }


                if (rdoVentaCredito.Checked)
                {
                    VtasImprimir.TipoDeReporte = TipoReporteVenta.Credito;
                }

                //// Jesus Diaz 2K120110.1453 
                if (DtGeneral.EsAlmacen)
                {
                    VtasImprimir.TipoDeReporte = TipoReporteVenta.Credito;
                }

                VtasImprimir.MostrarImpresionDetalle = true;
                VtasImprimir.Importe = dImporte;
                VtasImprimir.MostrarVistaPrevia = chkMostrarImpresionEnPantalla.Checked;
                VtasImprimir.MostrarPrecios = chkMostrarPrecios.Checked;

                if (Tipo == 1)
                {
                    VtasImprimir.Imprimir(txtFolio.Text, txtFolio.Text);
                }
                else
                {
                    VtasImprimir.ImprimirExcel(txtFolio.Text, txtFolio.Text, dImporte); 
                }
            }  
        }

        private void tmImpresion_Tick(object sender, EventArgs e)
        {
            tmImpresion.Stop();
            tmImpresion.Enabled = false;

            //// Los almacenes estan habilitados por default 
            if (!DtGeneral.EsAlmacen)
            {
                if (!GnFarmacia.ImpresionDetalladaTicket)
                {
                    General.msjUser("La Unidad no se encuentra configurada para reimpresión detallada de tickets.");
                    this.Close();
                }
            }
        }

        private void FrmReimpresionDeTicketsDetallado_Load(object sender, EventArgs e)
        {
            tmImpresion.Enabled = true;
            tmImpresion.Start(); 
        }

        private void btnAplicarPrecios_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("Exec spp_Mtto_Ventas_AsignarPrecioLicitacion '{0}', '{1}', '{2}', '{3}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text);

            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnAplicarPrecios_Click"); 
                General.msjError("Ocurrió un error al actualizar los precios del folio de venta."); 
            }
            else
            {
                General.msjUser("Los precios del folio de venta especificado han sido actualizados satisfactoriamente."); 
            }
        }

        private void FrmReimpresionDeTicketsDetallado_Activated(object sender, EventArgs e)
        {
            ////if (!bInicializado)
            ////{
            ////    bInicializado = true; 
            ////    if (DtGeneral.EsAlmacen)
            ////    {
            ////        FrameDatos.ContextMenuStrip = null;
            ////        FrameDatos.ContextMenuStrip = menuPrecios; // Enlazar el menú de precios 
            ////    }
            ////}
        }
    }
}
