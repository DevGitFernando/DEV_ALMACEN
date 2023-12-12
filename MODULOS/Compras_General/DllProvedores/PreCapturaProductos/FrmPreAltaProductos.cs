using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.FuncionesGrid;

using DllProveedores;
using DllProveedores.Usuarios_y_Permisos;

namespace DllProveedores.PreCapturaProductos
{
    public partial class FrmPreAltaProductos : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeerWeb myLeerProducto = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsLeerWeb myLeer = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsLeerWeb leer = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);

        DataSet dtsCargarDatos = new DataSet();
        bool bInicioPantalla = true;


        //string sIdCliente = GnFarmacia.Parametros.GetValor("CtePubGeneral");  //"0001";

        public FrmPreAltaProductos()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnProveedores.Modulo, GnProveedores.Version, this.Name);
        }

        private void FrmPreAltaProductos_Load(object sender, EventArgs e)
        {
            Inicializa();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F4:
                    btnNuevo_Click(null, null);
                    break;

                case Keys.F6:
                    btnGuardar_Click(null, null);
                    break;

                case Keys.F8:
                    btnCancelar_Click(null, null);
                    break;

                case Keys.F10:
                    btnImprimir_Click(null, null);
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        #region Limpiar
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true, FrameProducto);
            lblCancelado.Visible = false;
            lblCancelado.Text = "CANCELADO";

            //Se limpian los checkbox
            chkMedicamento.Checked = false;
            chkRefrigerado.Checked = false;

            //Se limpian los Grids.
            tabCatProductos.SelectTab(0);
            tabCatProductos.Focus();

            if (bInicioPantalla)
            {
                bInicioPantalla = false;
                SendKeys.Send("{TAB}");
            }

            IniciaToolBar(true, true, false, false);
            txtCodigoEAN.Focus();
        }

        #endregion Limpiar

        #region Inicializa 
        private void Inicializa()
        {
            // Cargar todos los combos y los Estados 
            LlenaClasificaciones();
            LlenaTiposProducto();
            LlenaPresentaciones();

            btnNuevo_Click(null, null);
        }

        private void LlenaClasificaciones()
        {
            string sSql = "Select * From CatClasificacionesSSA (NoLock) Where Status = 'A' Order By Descripcion";

            cboClasificaciones.Clear();
            cboClasificaciones.Add("0", "<< Seleccione >>");

            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    cboClasificaciones.Add(myLeer.DataSetClase, true);
                }
            }
            cboClasificaciones.SelectedIndex = 0;
        }

        private void LlenaTiposProducto()
        {
            string sSql = "Select * From CatTiposDeProducto (NoLock) Where Status = 'A' Order By Descripcion";

            cboTipoProductos.Clear();
            cboTipoProductos.Add("0", "<< Seleccione >>");

            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    cboTipoProductos.Add(myLeer.DataSetClase, true);
                }
            }
            cboTipoProductos.SelectedIndex = 0;
        }

        private void LlenaPresentaciones()
        {
            string sSql = "Select * From CatPresentaciones (NoLock) Where Status = 'A' Order By Descripcion";

            cboPresentaciones.Clear();
            cboPresentaciones.Add("0", "<< Seleccione >>");

            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    cboPresentaciones.Add(myLeer.DataSetClase, true);
                }
            }
            cboPresentaciones.SelectedIndex = 0;
        }
        
        #endregion Inicializa

        #region Buscar Codigo EAN 
        private void txtCodigoEAN_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "Select * From CatPreAltaProductos (NoLock) Where CodigoEAN = '" + txtCodigoEAN.Text.Trim() + "' ";

            if (txtCodigoEAN.Text.Trim() != "")
            {
                if (!BuscaCodigoEAN_EnCatalogo())
                {
                    if (myLeerProducto.Exec(sSql))
                    {
                        if (myLeerProducto.Leer())
                        {
                            CargaDatos();
                        }

                    }
                }
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtCodigoEAN.Enabled = false;

            txtCodigoEAN.Text = myLeerProducto.Campo("CodigoEAN");
            txtIdClaveSSA.Text = myLeerProducto.Campo("ClaveSSA_Sal");
            txtClaveSSA.Text = myLeerProducto.Campo("DescripcionSal");
            cboTipoProductos.Data = myLeerProducto.Campo("IdTipoProducto");
            txtProducto.Text = myLeerProducto.Campo("Descripcion");
            cboClasificaciones.Data = myLeerProducto.Campo("IdClasificacion");

            if (myLeerProducto.CampoBool("Segmento") == true)
            {
                chkRefrigerado.Checked = true;
            }

            txtLaboratorio.Text = myLeerProducto.Campo("Laboratorio");
            cboPresentaciones.Data = myLeerProducto.Campo("IdPresentacion");
            chkMedicamento.Checked = myLeerProducto.CampoBool("EsMedicamentoControlado");
            chkEsSectorSalud.Checked = myLeerProducto.CampoBool("EsSectorSalud");
            txtPrecioMaximo.Text = myLeerProducto.CampoDouble("PrecioMaxPublico").ToString();

            IniciaToolBar(true, true, true, false);
            if (myLeerProducto.Campo("Status") == "C")
            {
                IniciaToolBar(true, false, false, false);
                lblCancelado.Visible = true;
                lblCancelado.Text = "CANCELADO";
                Fg.BloqueaControles(this, false);
            }

            if (myLeerProducto.Campo("Status") == "R")
            {
                IniciaToolBar(true, false, false, false);
                lblCancelado.Visible = true;
                lblCancelado.Text = "RECHAZADO";
                Fg.BloqueaControles(this, false);
            }

        }
        private bool BuscaCodigoEAN_EnCatalogo()
        {
            bool bRegresa = false;
            string sMensaje = "";
            string sSql = String.Format("Exec spp_BuscaCodigoEAN '{0}' ", txtCodigoEAN.Text.Trim());

            //En esta funcion, se verifica si el CodigoEAN ya se encuentra en nuestro catalogo.
            if (myLeerProducto.Exec(sSql))
            {
                if (myLeerProducto.Leer())
                {
                    bRegresa = true;
                    sMensaje = String.Format("Este Codigo EAN ya se encuentra dado de alta con la Clave SSA: {0} ", myLeerProducto.Campo("ClaveSSA"));
                    General.msjUser(sMensaje);
                    btnNuevo_Click(null,null);
                }
            }
            
            return bRegresa;
        }
        #endregion Buscar Codigo EAN

        #region Buscar Clave SSA 
        private void txtIdClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdClaveSSA.Text.Trim() == "")
            {
                txtIdClaveSSA.Text = "0000";
            }
        }

        #endregion Buscar Clave SSA 

        #region Guardar/Actualizar Producto
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sCodigoEAN = "";
            int iMedicamento = 0, iCodigoEAN = 1, iEsSector = 0, iSegmento = 0;
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            
            if (ValidaDatos())
            {
                if (!BuscaCodigoEAN_EnCatalogo())
                {
                    if (chkMedicamento.Checked)
                        iMedicamento = 1;
                    if (chkEsSectorSalud.Checked)
                        iEsSector = 1;
                    if (chkRefrigerado.Checked)
                        iSegmento = 1;

                    sSql = String.Format("Exec spp_Mtto_CatPreAltaProductos '{0}', '{1}', '{2}', '{3}', " +
                            " '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}' ",
                            txtCodigoEAN.Text.Trim(), GnProveedores.IdProveedor, txtIdClaveSSA.Text.Trim(), txtClaveSSA.Text.Trim(),
                            cboTipoProductos.Data, txtProducto.Text.Trim(), iMedicamento, iEsSector, cboClasificaciones.Data, iSegmento,
                            txtLaboratorio.Text.Trim(), cboPresentaciones.Data, txtPrecioMaximo.NumericText, iCodigoEAN, iOpcion);

                    if (myLeerProducto.Exec(sSql))
                    {
                        if (myLeerProducto.Leer())
                        {
                            sCodigoEAN = String.Format("{0}", myLeerProducto.Campo("Clave"));
                            sMensaje = String.Format("{0}", myLeerProducto.Campo("Mensaje"));

                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            btnNuevo_Click(null, null);
                        }

                    }
                    else
                    {
                        // Error.GrabarError(myLeerProducto, "btnGuardar_Click");
                        General.msjUser("Ocurrió un error al guardar la informacion");
                    }
                }

            }

        }
   
        #endregion Guardar/Actualizar Producto

        #region Imprimir 
        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Imprimir

        #region Eliminar Producto

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar el Producto seleccionado ?";


            //Se verifica que no este cancelada.
            if (lblCancelado.Visible == false)
            {
                
                if (General.msjCancelar(message) == DialogResult.Yes)
                {
                    sSql = String.Format("Exec spp_Mtto_CatPreAltaProductos '{0}', '{1}', '', '', " +
                        " '', '', '', '', '', '', '', '', '0.0000', '', '{2}' ", 
                        txtCodigoEAN.Text.Trim(), GnProveedores.IdProveedor, iOpcion);

                    if (myLeerProducto.Exec(sSql))
                    {
                        if (myLeerProducto.Leer())
                        {
                            sMensaje = String.Format("{0}", myLeerProducto.Campo("Mensaje"));

                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            btnNuevo_Click(null, null);
                        }
                    }
                    else
                    {
                        //Error.GrabarError(myLeerProducto, "btnCancelar_Click");
                        General.msjError("Ocurrió un error al eliminar el Producto.");
                        //btnNuevo_Click(null, null);
                    }                        

                }
                
            }
            else
            {
                General.msjUser("Este Producto ya esta cancelado");
            }


        }

        #endregion Eliminar Producto

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;  

            if (txtCodigoEAN.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Codigo EAN, verifique.");
                txtCodigoEAN.Focus();
            }

            if (bRegresa && txtIdClaveSSA.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado una Clave SSA, verifique.");
                txtIdClaveSSA.Focus();
            }

            if (bRegresa && txtClaveSSA.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Descripcion SSA, verifique.");
                txtClaveSSA.Focus();                    
            }

            if (bRegresa && txtProducto.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Descripción del Producto, verifique.");
                txtProducto.Focus();
            }

            if (bRegresa && txtLaboratorio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Laboratorio valido, verifique.");
                txtLaboratorio.Focus();
            }

            if (bRegresa && cboTipoProductos.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado el Tipo de Producto, verifique.");
                cboTipoProductos.Focus();
            }            

            if (bRegresa && cboClasificaciones.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Clasificación valida, verifique.");
                cboClasificaciones.Focus();
            }            

            if (bRegresa && cboPresentaciones.SelectedIndex  == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Presentación valida, verifique.");
                cboPresentaciones.Focus();
            }

            if (bRegresa && Convert.ToDouble(txtPrecioMaximo.NumericText) == 0 )
            {
                bRegresa = false;
                General.msjUser("El Precio Máximo Publico tiene que ser mayor a cero. Verifique");
                txtPrecioMaximo.Focus(); 
            }
            
            return bRegresa;
        }

        #endregion Validaciones de Controles

        #region Eventos
        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    myLeerProducto.DataSetClase = Ayuda.Productos("txtId_KeyDown");

            //    if (myLeerProducto.Leer())
            //    {
            //        CargaDatos();
            //    }
            //}

        }

        private void txtClaveInternaSal_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    myLeer.DataSetClase = Ayuda.ClavesSSA_Sales("txtId_KeyDown");

            //    if (myLeer.Leer())
            //    {
            //        CargaDatosSalInterna();
            //    }
            //}
        }

        private void txtClaveInternaSal_TextChanged(object sender, EventArgs e)
        {
        }


        private void chkCodigoEAN_CheckedChanged(object sender, EventArgs e)
        {
            
        }      
        #endregion Eventos

        #region Funciones
        private void IniciaToolBar(bool bNuevo, bool bGuardar, bool bCancelar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
            btnImprimir.Enabled = bImprimir;
        }
        #endregion Funciones



    } //Llaves de la clase
}
