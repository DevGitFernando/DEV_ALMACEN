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

namespace DllCompras.ListasDePrecioClaves
{
    public partial class FrmComClaveSSA_CapturaPreciosNegociado : FrmBaseExt
    {
        private clsConexionSQL myCnn = new clsConexionSQL(General.DatosConexion);
        private clsConsultas Consulta;
        private clsLeer myLeer;
        private clsAyudas myAyuda;

        public FrmComClaveSSA_CapturaPreciosNegociado()
        {
            InitializeComponent();
            myLeer = new clsLeer(ref myCnn);
            myAyuda = new clsAyudas(General.DatosConexion, GnCompras.Modulo, this.Name, GnCompras.Version);
            Consulta = new clsConsultas(General.DatosConexion, GnCompras.DatosApp, this.Name);
        }

        private void FrmComClaveSSA_CapturaPreciosNegociado_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guadar(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Guadar(2);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {

        }

        private void InicializaToolBar(bool bGuardar, bool bCancelar, bool bEjecutar)
        {
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
            btnEjecutar.Enabled = bEjecutar;
        }

        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            InicializaToolBar(false, false, false);
            txtIdLaboratorio.Enabled = false;
            lblStatus.Text = "CANCELADA";
            lblStatus.Visible = false;
            txtCodClaveSSA.Focus();
        }
        #endregion Botones

        #region Eventos
        private void txtCodClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodClaveSSA.Text != "")
            {
                myLeer.DataSetClase = Consulta.ClavesSSA_Sales(txtCodClaveSSA.Text, true, "txtCodClaveSSA_Validating");

                if (myLeer.Leer())
                {
                    txtCodClaveSSA.Text = myLeer.Campo("ClaveSSA");
                    lbldClaveSSA.Text = myLeer.Campo("IdClaveSSA_Sal");
                    lblDescripcionClave.Text = myLeer.Campo("Descripcion");

                    txtCodClaveSSA.Enabled = false;
                    txtIdLaboratorio.Enabled = true;
                    txtIdLaboratorio.Focus();
                    InicializaToolBar(true, false, true);
                }
                else
                {
                    txtCodClaveSSA.Focus();
                }
            }
        }

        private void txtCodClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            clsLeer myLeerClavesSSA = new clsLeer(ref myCnn);

            if (e.KeyCode == Keys.F1)
            {
                myLeerClavesSSA.DataSetClase = myAyuda.ClavesSSA_Sales("txtCodClaveSSA_KeyDown()");

                if (myLeerClavesSSA.Leer())
                {
                    txtCodClaveSSA.Text = myLeerClavesSSA.Campo("ClaveSSA");
                    lbldClaveSSA.Text = myLeer.Campo("IdClaveSSA_Sal");
                    lblDescripcionClave.Text = myLeerClavesSSA.Campo("Descripcion");
                    txtCodClaveSSA.Enabled = false;
                    txtIdLaboratorio.Enabled = true;
                    txtIdLaboratorio.Focus();
                }
            }
        }

        private void txtCodClaveSSA_TextChanged(object sender, EventArgs e)
        {
            lblDescripcionClave.Text = "";
        }

        private void txtIdLaboratorio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = myAyuda.LaboratoriosPorClaveSSA(txtCodClaveSSA.Text, "txtIdLaboratorio_KeyDown()");

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
                else
                {
                    txtIdLaboratorio.Text = "";
                    txtIdLaboratorio.Focus();
                }
            }
        }

        private void txtIdLaboratorio_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref myCnn);

            if (txtIdLaboratorio.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consulta.LaboratoriosPorClaveSSA(txtCodClaveSSA.Text, txtIdLaboratorio.Text.Trim(), "txtIdLaboratorio_Validating()");

                if (myLeer.Leer())
                    CargaDatos();
                else
                {
                    txtIdLaboratorio.Text = "";
                    txtIdLaboratorio.Focus();
                }
                    
            }
        }

        private void txtIdLaboratorio_TextChanged(object sender, EventArgs e)
        {
            lblNombreLaboratorio.Text = "";
            txtPrecio.Text = "0.0000";
        }
        #endregion Eventos

        #region Funciones y Procedimientos
        private void CargaDatos()
        {
            txtIdLaboratorio.Text = myLeer.Campo("IdLaboratorio");
            lblNombreLaboratorio.Text = myLeer.Campo("Descripcion");
            txtIdLaboratorio.Enabled = false;

            string sSql = string.Format("Select * From COM_OCEN_ComisionNegociadoraPrecios (NoLock)	Where IdClaveSSA_Sal = '{0}' And IdLaboratorio = '{1}'",
                lbldClaveSSA.Text, txtIdLaboratorio.Text);

            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    txtPrecio.Text = myLeer.Campo("Precio");
                }
            }
        }

        private void Guadar(int iOpcion)
        {
            bool bContinua = true;
            string sMensaje = "";
            string sPrecio = "";

            sPrecio = txtPrecio.Text.Replace(",", "");

            if (ValidaDatos())
            {
                if (!myCnn.Abrir())
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }
                else
                {
                    myCnn.IniciarTransaccion();

                    string sSql = string.Format("Exec spp_Mtto_COM_OCEN_ComisionNegociadoraPrecios @IdClaveSSA = '{0}', @IdLaboratorio = '{1}', @Precio = '{2}', @iOpcion = {3}",
                    lbldClaveSSA.Text, txtIdLaboratorio.Text, sPrecio, iOpcion);

                    if (!myLeer.Exec(sSql))
                    {
                        bContinua = false;
                    }
                    else
                    {
                        myLeer.Leer();
                        sMensaje = myLeer.Campo("Mensaje");
                    }

                    if (bContinua) // Si no Ocurrió ningun error se llevan a cabo las transacciones.
                    {
                        myCnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        InicializaToolBar(false, false, false);
                        Imprimir();
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        myCnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la Información.");
                    }

                    myCnn.Cerrar();
                }
            }
        }

        private void Imprimir()
        {

        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtCodClaveSSA.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave SSA inválida, verifique.");
                txtCodClaveSSA.Focus();
            }

            if (bRegresa && txtIdLaboratorio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Laboratorio inválido, verifique.");
                txtIdLaboratorio.Focus();
            }

            return bRegresa;
        }


        #endregion Funciones y Procedimientos
    }
}
