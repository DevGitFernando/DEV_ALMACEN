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

namespace DllFarmaciaSoft.OrdenesDeCompra
{
    public partial class FrmCheckListRecepcion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        
        string sModulo = GnOficinaCentral.Modulo;
        string sVersion = GnOficinaCentral.Version;

        public FrmCheckListRecepcion()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(sModulo, sVersion, this.Name);

            CargarGrupos();

        }

        private void FrmCheckListRecepcion_Load(object sender, EventArgs e)
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
            if (validaDatos(1))
            {
                GrabarInformacion(1);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (validaDatos(2))
            {
                GrabarInformacion(2);
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
            lblStatus.Visible = false;
            cboGrupos.SelectedIndex = 0;
            IniciaBotones(false, false, false);
            cboGrupos.Focus();
        }

        private void IniciaBotones(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private void CargarGrupos()
        {
            string sSql = "";

            cboGrupos.Clear();
            cboGrupos.Add();

            sSql = string.Format(" Select Distinct IdGrupo, DescripcionGrupo From COM_Cat_Grupos_Recepcion Order By IdGrupo");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarGrupos");
                General.msjError("Ocurrio un error al buscar los grupos");
            }
            else
            {
                if (leer.Leer())
                {
                    cboGrupos.Add(leer.DataSetClase, true, "IdGrupo", "DescripcionGrupo");
                }
            }

            cboGrupos.SelectedIndex = 0;
        }
        #endregion Funciones

        #region Evento_Motivos
        private void txtMotivo_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "";

            if (txtMotivo.Text.Trim() == "")
            {
                txtMotivo.Text = "*";
                txtMotivo.Enabled = false;
                IniciaBotones(true, false, false);
            }
            else
            {
                sSql = string.Format(" Select * From COM_CheckList_Recepcion Where IdGrupo = '{0}' and IdMotivo = '{1}' ",
                              cboGrupos.Data, Fg.PonCeros(txtMotivo.Text, 3));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtMotivo_Validating");
                    General.msjError("Ocurrio un error al consultar el motivo del grupo.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        CargarDatos();                        
                    }
                    else
                    {
                        General.msjUser("No se encontraron datos con los criterios seleccionados. Verifique..");
                    }
                }
            }
        }

        private void CargarDatos()
        {
            IniciaBotones(true, true, false);
            txtMotivo.Text = leer.Campo("IdMotivo");
            txtDescripcion.Text = leer.Campo("DescripcionMotivo");

            chkSI.Checked = leer.CampoBool("Respuesta_SI");
            chkSI_Firma.Checked = leer.CampoBool("Respuesta_SI_RequiereFirma");
            chkNO.Checked = leer.CampoBool("Respuesta_NO");
            chkNO_Firma.Checked = leer.CampoBool("Respuesta_NO_RequiereFirma");
            chkRechazo.Checked = leer.CampoBool("Respuesta_Rechazo");
            chkRechazo_Firma.Checked = leer.CampoBool("Respuesta_Rechazo_RequiereFirma");

            if (leer.Campo("Status") == "C")
            {
                lblStatus.Visible = true;
                IniciaBotones(true, false, false);
            }
        }
        #endregion Evento_Motivos

        #region Guardar_Cancelar
        private void GrabarInformacion(int Opcion)
        {
            int iSI = 0, iSI_Firma = 0, iNO = 0, iNO_Firma = 0, iRechazo = 0, iRechazo_Firma = 0;
            string sMsj = "Ocurrió un error al guardar la información";

            if (Opcion == 2)
            {
                sMsj = "Ocurrió un error al cancelar la información";
            }

            iSI = Convert.ToInt32(chkSI.Checked);
            iSI_Firma = Convert.ToInt32(chkSI_Firma);
            iNO = Convert.ToInt32(chkNO.Checked);
            iNO_Firma = Convert.ToInt32(chkNO_Firma.Checked);
            iRechazo = Convert.ToInt32(chkRechazo.Checked);
            iRechazo_Firma = Convert.ToInt32(chkRechazo_Firma.Checked);

            if (cnn.Abrir())
            {
                string sSql = string.Format(" Exec spp_Mtto_COM_CheckList_Recepcion '{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, {7}, {8}, {9} ",
                    cboGrupos.Data, txtMotivo.Text, txtDescripcion.Text, iSI, iSI_Firma, iNO, iNO_Firma, iRechazo, iRechazo_Firma, Opcion);

                cnn.IniciarTransaccion();
                if (!leer.Exec(sSql))
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "GrabarInformacion");
                    General.msjError(sMsj);
                }
                else
                {
                    cnn.CompletarTransaccion();
                    leer.Leer();
                    General.msjUser(leer.Campo("Mensaje"));
                    LimpiaPantalla();
                }

                cnn.Cerrar();
            }
            else
                General.msjAviso("No fue posible establecer conexión con el servidor. Intente de nuevo.");
        }

        private bool validaDatos(int Opcion)
        {
            bool bRegresa = true;

            if (cboGrupos.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado el tipo de movimiento, verifique.");
                cboGrupos.Focus();
            }

            if (bRegresa && txtMotivo.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Motivo inválida, verifique.");
                txtMotivo.Focus();
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Descripción del motivo, verifique.");
                txtDescripcion.Focus();
            }

            if (Opcion == 2)
            {
                if (bRegresa)
                {
                    if (General.msjCancelar("¿ Desea cancelar la información del motivo del grupo ?") == DialogResult.No)
                    {
                        bRegresa = false;
                    }
                }
            }

            return bRegresa;
        }
        #endregion Guardar_Cancelar

        #region Evento_Combo
        private void cboGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiaControles();
        }

        private void LimpiaControles()
        {
            txtMotivo.Text = "";
            txtDescripcion.Text = "";
            chkSI.Checked = false;
            chkSI_Firma.Checked = false;
            chkNO.Checked = false;
            chkNO_Firma.Checked = false;
            chkRechazo.Checked = false;
            chkRechazo_Firma.Checked = false;
            lblStatus.Visible = false;
        }
        #endregion Evento_Combo
    }
}
