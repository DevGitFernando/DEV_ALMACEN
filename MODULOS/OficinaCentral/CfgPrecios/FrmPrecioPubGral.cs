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

using DllFarmaciaSoft;

namespace OficinaCentral.CfgPrecios
{
    public partial class FrmPrecioPubGral : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;

        clsGrid myGrid;
        System.Threading.Thread _workerThread; 

        bool bExito = true;
        bool bGuardando = false;
        bool bLimpiarGrid = false;
        bool bInicioPantalla = true;
        bool bEjecucionEnHilo = false; 

        public FrmPrecioPubGral()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name); 
            ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name); 
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name); 

            myGrid = new clsGrid(ref grpProductos, this);
            myGrid.BackColorColsBlk = Color.White;
            grpProductos.EditModeReplace = true;
            myGrid.AjustarAnchoColumnasAutomatico = true; 
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            lblMensaje.BorderStyle = BorderStyle.None;
            lblMensaje.Text = "GUARDANDO INFORMACIÓN";
            lblMensaje.Visible = false;
            pgBar.Visible = false;

            // Conflicto con el Spread al Manejar Hilos 
            if (!bEjecucionEnHilo)
            {
                myGrid.Limpiar();  
            }
            else 
            {
                try
                {
                    grpProductos.Sheets[0].Rows.Count=0;
                }
                catch (Exception ex)
                {
                    ex.Source = ex.Source; 
                }
            }

            bEjecucionEnHilo = false; 
            // Detener 
            this.grpProductos.Refresh(); 
            Thread.Sleep(100); 

            // myGrid.Limpiar();
            Fg.IniciaControles(true);
            btnGuardar.Enabled = false;

            rdoTodos.Checked = false;
            rdoProducto.Checked = true; 
            // txtCodigoEAN.Enabled = true; 


            grpProductos.Sheets[0].Rows.Add(0, 1);
            myGrid.BloqueaRenglon(true); 

            if (bInicioPantalla)
            {
                bInicioPantalla = false;
                SendKeys.Send("{TAB}");
            }

            txtIdProducto.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            grpProductos.Enabled = false;
            lblMensaje.BorderStyle = BorderStyle.Fixed3D;
            lblMensaje.Visible = true;
            pgBar.Visible = true; 
            FrameProducto.Enabled = false;
            this.Refresh();

            System.Threading.Thread.Sleep(1500);

            bLimpiarGrid = false;
            bGuardando = true;
            tmEjecucion.Enabled = true;
            tmEjecucion.Start();

            System.Threading.Thread.Sleep(500);
            _workerThread = new Thread(this.GuardarInformacion);//new System.Threading.Thread(this.Guardar);
            _workerThread.Name = "GuardarPrecios";
            _workerThread.Start();


            //while (bGuardando)
            //{
            //    Thread.Sleep(500); 
            //}

            //if (bExito)
            //    LimpiarPantalla(); 
        }

        private bool validaDatos()
        {
            bool bRegresa = true;

            if (myGrid.Rows < 1)
            {
                bRegresa = false;
                General.msjUser("No hay información para guardar, vefirique.");
            }

            return bRegresa;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = " Select IdProducto, Descripcion, UtilidadProducto, PrecioMaxPublico, DescuentoGral, " +
                " UtilidadProducto as UtilidadProductoBase, PrecioMaxPublico as PrecioMaxPublicoBase, DescuentoGral as DescuentoGralBase " +
                " From CatProductos (NoLock) Order by IdProducto ";
            btnGuardar.Enabled = false;

            if (validaDatosExec())
            {
                if (rdoProducto.Checked)
                    sSql = string.Format(" Select IdProducto, Descripcion, UtilidadProducto, PrecioMaxPublico, DescuentoGral, " +
                        " UtilidadProducto as UtilidadProductoBase, PrecioMaxPublico as PrecioMaxPublicoBase, DescuentoGral as DescuentoGralBase " + 
                        " From CatProductos (NoLock) where IdProducto = '{0}' Order by IdProducto ", Fg.PonCeros(txtIdProducto.Text, 8));

                myGrid.Limpiar();
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "btnEjecutar_Click");
                    General.msjError("Ocurrió un error al obtener la lista de productos.");
                }
                else
                {
                    btnGuardar.Enabled = true;
                    txtIdProducto.Enabled = false;
                    myGrid.LlenarGrid(leer.DataSetClase);
                }
            }
        }

        private bool validaDatosExec()
        {
            bool bRegresa = true;

            if (rdoProducto.Checked)
            {
                if (txtIdProducto.Text.Trim() == "")
                {
                    bRegresa = false;
                    General.msjUser("No ha capturado la Clave de Producto, verifique.");
                    txtIdProducto.Focus();
                }
            }

            return bRegresa;
        }

//        private void Guardar(object BarraMenu)
        private void GuardarInformacion()
        {
            // ToolStrip Barra = (ToolStrip)BarraMenu;
            ToolStrip Barra = toolStripBarraMenu; 
            bExito = true;
            bool bSeGuardoInformacion = false;
            string sSql = "", sIdProducto = "";
            double dUtilidad = 0, dPrecioMax = 0, dDescuento = 0;

            if (validaDatos())
            {
                bEjecucionEnHilo = true; 
                pgBar.Visible = true; 
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnEjecutar.Enabled = false;

                this.Refresh(); 
                Thread.Sleep(1000); 

                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    for (int i = 1; i <= myGrid.Rows; i++)
                    {
                        sIdProducto = myGrid.GetValue(i, 1);
                        dUtilidad = myGrid.GetValueDou(i, 3);
                        dPrecioMax = myGrid.GetValueDou(i, 4);
                        dDescuento = myGrid.GetValueDou(i, 5);

                        sSql = string.Format(" Update CatProductos Set Actualizado = 0, UtilidadProducto = '{1}', PrecioMaxPublico = '{2}', DescuentoGral = '{3}' " +
                            " Where IdProducto = '{0}'  \n\n  " +
                            " Exec spp_Mtto_CatProductos_Historico '{0}', '{4}'  \n\n ", sIdProducto, dUtilidad, dPrecioMax, dDescuento, DtGeneral.IdPersonal);

                        if (DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Regional)
                        {
                            sSql += string.Format(" Update INT__MA_Productos_Marbetes Set MarbeteActualizado = 0, FechaUpdate = getdate() Where IdProducto = '{0}'", sIdProducto);
                        }

                        if (dUtilidad != myGrid.GetValueDou(i, 6) || dPrecioMax != myGrid.GetValueDou(i, 7) || dDescuento != myGrid.GetValueDou(i, 8))
                        {
                            bSeGuardoInformacion = true;
                            if (!leer.Exec(sSql))
                            {
                                bExito = false;
                                break;
                            }
                        }
                    }

                    lblMensaje.Visible = false; 
                    pgBar.Visible = false; 

                    if (!bSeGuardoInformacion)
                    {
                        General.msjUser("Información guardada satisfactoriamente.");
                        bLimpiarGrid = true;
                        LimpiarPantalla();
                    }
                    else
                    {
                        if (!bExito)
                        {
                            cnn.DeshacerTransaccion();
                            Error.GrabarError(leer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al guardar la información de precios.");
                        }
                        else
                        {
                            bLimpiarGrid = true;
                            LimpiarPantalla();
                            cnn.CompletarTransaccion();
                            lblMensaje.Visible = false;
                            General.msjUser("Información guardada satisfactoriamente."); 
                        }
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No fue posible establecer conexión al servidor, intente de nuevo.");
                }

                btnNuevo.Enabled = true;
                btnEjecutar.Enabled = true;
                lblMensaje.Visible = false;
                FrameProducto.Enabled = true;
                bGuardando = false;
            }
        }
        
        #endregion Botones

        private void FrmPrecioPubGral_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void rdoProducto_CheckedChanged(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            myGrid.Limpiar();
            txtIdProducto.Text = "";
            txtCodigoEAN.Text = "";
            lblDescripcion.Text = "";
            txtIdProducto.Enabled = rdoProducto.Checked;
            txtCodigoEAN.Enabled = rdoProducto.Checked;
            //txtCodigoEAN.Focus();
            txtIdProducto.Focus();
        }

        private void rdoTodos_CheckedChanged(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            myGrid.Limpiar();
            txtIdProducto.Text = "";
            txtCodigoEAN.Text = "";
            lblDescripcion.Text = "";
            txtIdProducto.Enabled = false;
            txtCodigoEAN.Enabled = false;
        }

        private void txtIdProducto_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdProducto.Text.Trim() != "")
            {
                leer.DataSetClase = query.Productos(txtIdProducto.Text, "txtIdProducto_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Producto no encontrada, verifique.");
                    e.Cancel = true;
                }
                else
                {
                    txtIdProducto.Text = leer.Campo("IdProducto");
                    lblDescripcion.Text = leer.Campo("Descripcion");
                    //txtCodigoEAN.Enabled = false;
                    txtIdProducto.Enabled = false;
                }
            }
            else
            {
                lblDescripcion.Text = "";
            }

        }

        private void txtIdProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Productos("txtIdProducto_Validating");
                if (leer.Leer())
                {
                    txtIdProducto.Text = leer.Campo("IdProducto");
                    lblDescripcion.Text = leer.Campo("Descripcion");
                } 
            }
        }

        private void tmEjecucion_Tick(object sender, EventArgs e)
        {
            if (!bGuardando)
            {
                tmEjecucion.Enabled = false;
                tmEjecucion.Stop();

                grpProductos.Enabled = true; 
                if (bLimpiarGrid)
                    myGrid.Limpiar();
            }
        }

        private void txtCodigoEAN_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodigoEAN.Text.Trim() != "")
            {
                string sSql = string.Format(" Select IdProducto, CodigoEAN, CodigoEAN_Interno " +
                    " From CatProductos_CodigosRelacionados (NoLock) Where CodigoEAN = '{0}' ", txtCodigoEAN.Text);
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtCodigoEAN_Validating");
                    General.msjError("Ocurrió un error al válidar el CodigoEAN");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtCodigoEAN.Enabled = false;
                        txtCodigoEAN.Text = "";

                        txtIdProducto.Text = leer.Campo("IdProducto");
                        txtIdProducto_Validating(null, null);
                        ////btnEjecutar_Click(this, null);
                    }
                    else
                    {
                        General.msjUser("CodigoEAN no encontrado, verifique.");
                        e.Cancel = true;
                        txtCodigoEAN.SelectAll();
                    }
                }
            }
        }


    }
}
