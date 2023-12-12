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

namespace DllFarmaciaSoft.Devoluciones
{
    public partial class FrmMotivosDevoluciones : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;
        
        string sModulo = GnOficinaCentral.Modulo;
        string sVersion = GnOficinaCentral.Version;

        //clsMotivosDevoluciones mo = new clsMotivosDevoluciones(General.DatosConexion, "DOC", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

        public FrmMotivosDevoluciones()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);

            query = new clsConsultas(General.DatosConexion, sModulo, this.Name, sVersion, true);
            ayuda = new clsAyudas(General.DatosConexion, sModulo, this.Name, sVersion, true);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(sModulo, sVersion, this.Name);

            CargarTipoMovtos();
        }

        private void FrmMotivosDevoluciones_Load(object sender, EventArgs e)
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
            //mo.MotivosDevolucion();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            lblStatus.Visible = false;
            cboIdTiposMovto.SelectedIndex = 0;
            cboIdTiposMovto.Focus();
        }

        private void CargarTipoMovtos()
        {
            string sSql = "";

            cboIdTiposMovto.Clear();
            cboIdTiposMovto.Add();

            sSql = string.Format(" Select Distinct IdTipoMovto_Inv, Descripcion From Movtos_Inv_Tipos Order By Descripcion");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarTipoMovtos");
                General.msjError("Ocurrio un error al buscar los tipos de movimientos");
            }
            else
            {
                if (leer.Leer())
                {
                    cboIdTiposMovto.Add(leer.DataSetClase, true, "IdTipoMovto_Inv", "Descripcion");
                }
            }

            cboIdTiposMovto.SelectedIndex = 0;
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
            }
            else
            {
                sSql = string.Format(" Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = '{0}' and IdMotivo = '{1}' ", 
                              cboIdTiposMovto.Data, Fg.PonCeros(txtMotivo.Text, 3));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtMotivo_Validating");
                    General.msjError("Ocurrio un error al consultar el motivo de devolución");
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
            txtMotivo.Text = leer.Campo("IdMotivo");
            txtDescripcion.Text = leer.Campo("Descripcion");

            if (leer.Campo("Status") == "C")
            {
                lblStatus.Visible = true;
            }
        }
        #endregion Evento_Motivos

        #region Guardar_Cancelar
        private void GrabarInformacion(int Opcion)
        {
            string sMsj = "Ocurrió un error al guardar la información";

            if (Opcion == 2)
                sMsj = "Ocurrió un error al cancelar la información";

            if (cnn.Abrir())
            {
                string sSql = string.Format(" Exec spp_Mtto_MovtosInv_Motivos_Dev '{0}', '{1}', '{2}', '{3}' ",
                    cboIdTiposMovto.Data, txtMotivo.Text, txtDescripcion.Text, Opcion);

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

            if (cboIdTiposMovto.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado el tipo de movimiento, verifique.");
                cboIdTiposMovto.Focus();
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
                    if (General.msjCancelar("¿ Desea cancelar la información del motivo de devolución ?") == DialogResult.No)
                    {
                        bRegresa = false;
                    }
                }
            }

            return bRegresa;
        }
        #endregion Guardar_Cancelar
    }
}
