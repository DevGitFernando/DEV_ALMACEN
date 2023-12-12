using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;

using DllFarmaciaSoft;

namespace Dll_IFacturacion.XSA
{
    public partial class FrmConceptos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer; 

        clsAyudas Ayudas;
        clsConsultas Consultas; 


        clsDatosCliente DatosCliente;
        string sFolio = "";
        string sMensaje = "";
        bool bExterno = false;
        public string Clave = ""; 
        public bool Guardo = false; 

        public FrmConceptos()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "Conceptos");

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version, false);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version, false);

            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);
        }

        private void FrmConceptos_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        public void ShowConceptos()
        {
            bExterno = true;
            this.ShowDialog(); 
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardarInformacion(1);

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        //txtId.Text = sFolio;
                        Clave = txtId.Text;
                        Guardo = true; 
                        cnn.CompletarTransaccion();

                        if (bExterno)
                        {
                            cnn.Cerrar();
                            this.Hide(); 
                        }
                        else 
                        {
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP 
                            btnNuevo_Click(null, null);
                        }
                    }
                    else
                    {
                        //Error.GrabarError(leer, "btnGuardar_Click");
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

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardarInformacion(2);

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        txtId.Text = sFolio;
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP                        
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        //Error.GrabarError(leer, "btnGuardar_Click");
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
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            IniciarToolBar(false, false, false);
            txtId.Focus();
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtId.Text.Trim() == "")
            {
                General.msjUser("Ingrese la Clave de Concepto por favor"); 
                txtId.Focus();
                bRegresa = false;
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                General.msjUser("Ingrese la Descripción por favor");
                txtDescripcion.Focus();
                bRegresa = false;
            }

            return bRegresa;
        }

        private void CargaDatos()
        {
            txtId.Enabled = false;
            txtId.Text = leer.Campo("IdConcepto");
            txtDescripcion.Text = leer.Campo("Descripcion");

            txtUnidadMedidaSAT.Text = leer.Campo("SAT_UnidadDeMedida"); 
            lblUnidadMedidaSAT.Text = leer.Campo("Descripcion_SAT_UnidadDeMedida");
            txtProductoSAT.Text = leer.Campo("SAT_ClaveProducto_Servicio"); 
            lblProductoSAT.Text = leer.Campo("Descripcion_SAT_ClaveDeProducto_Servicio"); 


            IniciarToolBar(true, true, false);
            if (leer.Campo("Status") == "C")
            {
                IniciarToolBar(true, false, false);
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                //txtDescripcion.Enabled = false;
            }
        }
        #endregion Funciones

        #region Eventos 
        private void txtId_Validating(object sender, CancelEventArgs e)
        {

            if (txtId.Text.Trim() != "")            
            {
                leer.DataSetClase = Consultas.CFDI_Conceptos(txtId.Text, "txtId_Validating");
                if (leer.Leer())
                {
                    CargaDatos();
                }
                else
                {
                    IniciarToolBar(true, false, false);                    
                    txtId.Enabled = false;                    
                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.CFDI_Conceptos("txtId_KeyDown");

                if (leer.Leer())
                {
                    CargaDatos(); 
                }
            }
        }        
        #endregion Eventos 

        #region Guardar/Cancelar_Informacion
        private bool GuardarInformacion(int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_Mtto_FACT_CFD_Conceptos @IdConcepto = '{0}', @Descripcion = '{1}', @SAT_ProductoServicio = '{2}', @SAT_UnidadDeMedida = '{3}', @iOpcion = '{4}' ", 
                txtId.Text, txtDescripcion.Text, txtProductoSAT.Text.Trim(), txtUnidadMedidaSAT.Text.Trim(), 
                iOpcion);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "GuardarInformacion");
            }
            else
            {
                if (leer.Leer())
                {
                    sFolio = leer.Campo("Folio");
                    sMensaje = leer.Campo("Mensaje");
                }
            }

            return bRegresa;
        }
        #endregion Guardar/Cancelar_Informacion        
     
   
        #region SAT 
        private void txtUnidadMedidaSAT_Validating(object sender, CancelEventArgs e)
        {
            if (txtUnidadMedidaSAT.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.CFD_UnidadesDeMedida(txtUnidadMedidaSAT.Text.Trim(), "txtUnidadMedidaSAT");
                if (leer.Leer())
                {
                    CargaDatosUnidadDeMedida();
                }
            }
        }

        private void txtUnidadMedidaSAT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.CFD_UnidadesDeMedida("txtUnidadMedidaSAT_KeyDown");
                if (leer.Leer())
                {
                    CargaDatosUnidadDeMedida();
                }
            }
        }

        private void txtUnidadMedidaSAT_TextChanged(object sender, EventArgs e)
        {
            lblUnidadMedidaSAT.Text = "";
        }

        private void CargaDatosUnidadDeMedida()
        {
            txtUnidadMedidaSAT.Text = leer.Campo("IdUnidad");
            lblUnidadMedidaSAT.Text = leer.Campo("Descripcion");
        }

        private void txtProductoSAT_Validating(object sender, CancelEventArgs e)
        {
            if (txtProductoSAT.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.CFDI_Productos_Servicios(txtProductoSAT.Text.Trim(), "txtUnidadMedidaSAT");
                if (leer.Leer())
                {
                    CargaDatosProductoSAT();
                }
                else
                {
                    txtProductoSAT.Text = "";
                }
            }
        }

        private void txtProductoSAT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.CFDI_Productos_Servicios("txtUnidadMedidaSAT_KeyDown");
                if (leer.Leer())
                {
                    CargaDatosProductoSAT();
                }
            }
        }

        private void txtProductoSAT_TextChanged(object sender, EventArgs e)
        {
            lblProductoSAT.Text = "";
        }

        private void CargaDatosProductoSAT()
        {
            txtProductoSAT.Text = leer.Campo("Clave");
            lblProductoSAT.Text = leer.Campo("Descripcion");
        }
        #endregion SAT 
    }
}
