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

namespace Facturacion.CuentasPorPagar
{
    public partial class FrmChequeras : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmChequeras()
        {
            InitializeComponent();
            cnn.SetConnectionString();

            myLeer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);
        }

        private void FrmChequeras_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            InicializarBotones(true, false);
            lblCancelado.Visible = false;
            lblUltimo.Visible = false;
            lblUltimoFolio.Visible = false;
            txtId.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Validacion())
            {
                GrabarInformacion(1);
            } 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            GrabarInformacion(2);
        }

        private void InicializarBotones(bool Guadar, bool Cancelar)
        {
            btnGuardar.Enabled = Guadar;
            btnCancelar.Enabled = Cancelar;
        }
        #endregion Botones

        #region Eventos
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text.Trim() == "")
            {
                txtId.Enabled = false;
                txtId.Text = "*";
            }
            else
            {
                txtId.Text = Fg.PonCeros(txtId.Text, 6);

                myLeer.DataSetClase = Consultas.Chequera(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, txtId.Text, "txtId_Validating()");

                if (myLeer.Leer())
                {
                    CargaDatosChequera();
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Chequera(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, "txtChequera_KeyDown");

                if (myLeer.Leer())
                {
                    CargaDatosChequera();
                }
            }
        }

        private void txtBanco_Validating(object sender, CancelEventArgs e)
        {
            if (txtBanco.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Bancos(txtBanco.Text, "txtId_Validating()");

                myLeer.Leer();

                if (myLeer.Campo("Status") == "C")
                {
                    txtBanco.Text = "";
                    lblBanco.Text = "";
                    General.msjAviso("El banco seleccionado se encuentra actualmente cancelado.");
                }
                else
                {
                    txtBanco.Text = myLeer.Campo("IdBanco");
                    lblBanco.Text = myLeer.Campo("Descripcion");
                }
            }
        }

        private void txtBanco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Bancos("txtId_KeyDown");

                if (myLeer.Leer())
                {
                    txtBanco.Text = myLeer.Campo("IdBanco");
                    lblBanco.Text = myLeer.Campo("Descripcion");
                }
            }
        }
        #endregion Eventos

        #region Funcion y Procedimientos
        private void GrabarInformacion(int iOpcion)
        {
            string sMsjErr = "Ocurrió un error al guardar la información.";

            if (iOpcion != 1)
            {
                sMsjErr = "Ocurrió un error al cancelar la información.";
            }


            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else 
            {
                cnn.IniciarTransaccion();

                string sSql = String.Format("Exec spp_Mtto_CatChequeras '{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6}, {7}, {8}",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, txtId.Text.Trim(), txtDescripcion.Text.Trim(),
                        txtBanco.Text.Trim(), txtFolioInicio.Text.Trim(), txtFolioFin.Text.Trim(), txtNumDeSerie.Text.Trim(), iOpcion);

                if (myLeer.Exec(sSql))
                {
                    myLeer.Leer();
                    cnn.CompletarTransaccion();
                    General.msjUser(myLeer.Campo("Mensaje")); //Este mensaje lo genera el SP
                    btnNuevo_Click(null, null);
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(myLeer, "btnGuardar_Click");
                    General.msjError(sMsjErr);
                }

                cnn.Cerrar();
            }
        }

        private void CargaDatosChequera()
        {
            txtId.Text = myLeer.Campo("IdChequera");
            txtDescripcion.Text = myLeer.Campo("Descripcion");
            txtBanco.Text = myLeer.Campo("IdBanco");
            lblBanco.Text = myLeer.Campo("Banco");
            txtFolioInicio.Text = myLeer.Campo("FolioInicio");
            txtFolioFin.Text = myLeer.Campo("FolioFin");
            txtNumDeSerie.Text = myLeer.Campo("NumeroDeSerie");
            InicializarBotones(false, true);

            txtId.Enabled = false;
            if (myLeer.Campo("Status") == "C")
            {
                InicializarBotones(true, false);
                lblCancelado.Visible = true;
            }

            if (myLeer.Campo("UltimoFolio") != "")
            {
                lblUltimo.Visible = true;
                lblUltimoFolio.Visible = true;
                lblUltimoFolio.Text = myLeer.Campo("UltimoFolio");
            }
        }

        private bool Validacion()
        {
            bool bRegresa = true; 

            if (txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado una Descripción para la Chequera, verifique.");
                txtDescripcion.Focus();
            }

            if (bRegresa && txtBanco.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado una clave de banco, verifique.");
                txtBanco.Focus(); 
            }

            if (bRegresa && (txtFolioInicio.Text.Trim() == "" || txtFolioFin.Text.Trim() == ""))
            {
                bRegresa = false;
                General.msjAviso("Se requiere la captura de Folio inicio y Folio Final, verifique.");
                txtFolioInicio.Focus(); 
            }

            if (bRegresa && Convert.ToInt32("0" + txtFolioInicio.Text.Trim()) >= Convert.ToInt32("0" + txtFolioFin.Text.Trim()))
            {
                bRegresa = false;
                General.msjAviso("El folio de inicio debe ser mayor al folio final, verifique.");
                txtFolioInicio.Focus(); 
            }

            if (bRegresa && txtNumDeSerie.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado el numero de serie, verifique.");
                txtBanco.Focus();
            }

            return bRegresa;
        }
        #endregion Funcion y Procedimientos
    }
}
