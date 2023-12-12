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

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace Farmacia.Inventario.Reubicaciones
{
    public partial class FrmReubicacionConfirmacion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid myGrid;
        clsConsultas query;
        clsAyudas ayuda;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;
        string sMsjNoEncontrado = "";
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;
        string sFolio_Reubicacion = "";

        bool bEsMayor24Horas = false; 
        string sFirma_Reubicacion = "";
        string sFirma_Reubicacion_Autorizacion = ""; 

        public FrmReubicacionConfirmacion()
        {
            InitializeComponent();

            myGrid = new clsGrid(ref grdDescripcion, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.BackColorColsBlk = Color.White;
            leer = new clsLeer(ref cnn);

            query = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);

            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sPersonal);
        }

        private void FrmReubicacionConfirmacion_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Botones
        private bool validarFirmas()
        {
            bool bRegresa = false;

            sMsjNoEncontrado = "El usuario no tiene permiso para confirmar una reubicación, verifique por favor.";
            ////bRegresa = opPermisosEspeciales.VerificarPermisos("CONFIRMACION_REUBICACION", sMsjNoEncontrado);
            bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("CONFIRMACION_REUBICACION", sMsjNoEncontrado);
            sFirma_Reubicacion = DtGeneral.PermisosEspeciales_Biometricos.ReferenciaHuella;

            if (bRegresa)
            {
                General.msjAviso("La reubicación tiene más de 24 horas naturales requiere permiso extraordinario.");
                sMsjNoEncontrado = "El usuario no tiene permiso para confirmar una reubicación extraordinaria, verifique por favor.";
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("CONFIRMACION_REUBICACION_EXTRAORDINARIA", sMsjNoEncontrado);
                sFirma_Reubicacion_Autorizacion = DtGeneral.PermisosEspeciales_Biometricos.ReferenciaHuella; 
            }

            return bRegresa; 
        }

        private void btnFirmar_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;

            if (validarFirmas())
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    cnn.IniciarTransaccion();

                    bRegresa = GrabarPropietarioDeHuella(txtFolio.Text);

                    if (bRegresa)
                    {
                        if (bEsMayor24Horas)
                        {
                            ////General.msjAviso("La reubicación tiene más de 24 horas naturales requiere permiso extraordinario.");
                            ////sMsjNoEncontrado = "El usuario no tiene permiso para confirmar una reubicación extraordinaria, verifique por favor.";
                            ////bRegresa = opPermisosEspeciales.VerificarPermisos("CONFIRMACION_REUBICACION_EXTRAORDINARIA", sMsjNoEncontrado);

                            ////if (bRegresa)
                            {
                                bRegresa = GrabarPropietarioDeHuellaExtraordinario(txtFolio.Text);
                            }
                        }
                    }

                    if (bRegresa)
                    {
                        General.msjUser("El Folio " + txtFolio.Text + " fue confirmado con éxito");
                        cnn.CompletarTransaccion();
                        LimpiarPantalla();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al confirmar la reubicación.");
                    }
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void LimpiarPantalla()
        {
            txtFolio.Text = "";
            txtFolio.Enabled = true;
            lblFolioReferencia.Text = "";
            lblConfirmado.Text = "CONFIRMADO";
            lblConfirmado.Visible = false;
            myGrid.Limpiar(false);
            myGrid.BloqueaGrid(true);
            btnFirmar.Enabled = false;
            txtFolio.Focus();
        }

        #endregion Botones

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolio.Text != "")
            {
                sFolio_Reubicacion = string.Format("SPR{0}", Fg.PonCeros(txtFolio.Text.Trim(), 8));
                leer.DataSetClase = query.Reubicacion_Confirmacion(sEmpresa, sEstado, sFarmacia, sFolio_Reubicacion, "txtFolio_Validating()");

                if (leer.Leer())
                {
                    CargarDatos();
                }
                else
                {
                    txtFolio.Text = "";
                }
            }
        }

        private void txtFolio_TextChanged(object sender, EventArgs e)
        {
            lblFolioReferencia.Text = "";
            myGrid.Limpiar(false);
        }

        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Reubicacion_Confirmacion(sEmpresa, sEstado, sFarmacia, "txtFolio_KeyDown()");
                if (leer.Leer())
                {
                    CargarDatos();
                }
            }
        }

        private void CargarDatos()
        {
            btnFirmar.Enabled = true;
            lblFolioReferencia.Text = leer.Campo("FolioMovto_Referencia");
            txtFolio.Text = leer.Campo("Folio_Inv");
            txtFolio.Enabled = false;
            myGrid.Limpiar(true);
            myGrid.SetValue(1, 1, leer.CampoInt("Claves"));
            myGrid.SetValue(1, 2, leer.CampoInt("Productos"));
            myGrid.SetValue(1, 3, leer.CampoInt("Cantidad"));

            if (leer.CampoBool("Confirmada") && !lblConfirmado.Visible)
            {
                lblConfirmado.Visible = true;
                btnFirmar.Enabled = false;
                General.msjAviso("La reubicación ya se encuentrada corfirmada.");
            }
        }

        private bool Mayor24Horas()
        {
            bool bRegresa = false;
            string sSql = string.Format("Select Max(DATEDIFF(hh, FechaRegistro, GETDATE())) As Horas " +
                            "From Ctrl_Reubicaciones C (NoLock) " + 
                            "Where C.IdEmpresa = '{0}' And C.IdEstado = '{1}' And C.IdFarmacia = '{2}' And C.Folio_Inv = '{3}'",
                            sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim());

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnFirmar_Click()");
                General.msjError("Ocurrió un error al obtener la información.");
            }
            else
            {
                if (leer.Leer())
                {
                    if (leer.CampoInt("Horas") > 24)
                    {
                        bRegresa = true;
                    }
                }
            }

            bEsMayor24Horas = bRegresa; 
            return bRegresa;
        }

        private bool GrabarPropietarioDeHuella(string sFolio)
        {
            bool bRegresa = false;

            string sSql = string.Format("Update Ctrl_Reubicaciones Set IdPersonal_Firma = '{0}', Status = 'T', FechaConfirmacion = GetDate() " +
                            "Where IdEmpresa = '{1}' And IdEstado = '{2}' And IdFarmacia = '{3}' And Folio_Inv = '{4}' ",
                            sFirma_Reubicacion, sEmpresa, sEstado, sFarmacia, sFolio);

            if (leer.Exec(sSql))
            {
                bRegresa = true;
            }

            return bRegresa;
        }

        private bool GrabarPropietarioDeHuellaExtraordinario(string sFolio)
        {
            bool bRegresa = false;

            string sSql = string.Format("Update Ctrl_Reubicaciones Set IdPersonal_Autoriza_Extraordinario = '{0}' " +
                            "Where IdEmpresa = '{1}' And IdEstado = '{2}' And IdFarmacia = '{3}' And Folio_Inv = '{4}' ",
                            sFirma_Reubicacion, sEmpresa, sEstado, sFarmacia, sFolio);

            if (leer.Exec(sSql))
            {
                bRegresa = true;
            }

            return bRegresa;
        }
    }
}
