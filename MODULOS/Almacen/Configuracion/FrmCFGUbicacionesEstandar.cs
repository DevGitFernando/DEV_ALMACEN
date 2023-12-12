using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

using DllFarmaciaSoft;

namespace Almacen.Configuracion
{
    public partial class FrmCFGUbicacionesEstandar : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leer2;

        clsConsultas Consulta;
        clsAyudas Ayuda;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;

        bool bCancelar = false;

        public FrmCFGUbicacionesEstandar()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);

            Consulta = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);

            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name); 
        }

        private void FrmCFGUbicacionesEstandar_Load(object sender, EventArgs e)
        {
            CargarPosiciones();
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar_Informacion(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Guardar_Informacion(2);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            IniciaBotones(false, false, false);
            chkStatus.Visible = false;
            bCancelar = false;
            cboPosicion.SelectedIndex = 0;
            cboPosicion.Focus();
        }

        private void IniciaBotones(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private void CargarPosiciones()
        { 
            cboPosicion.Clear();
            cboPosicion.Add();

            leer.DataSetClase = Consulta.UbicacionesEstandar("CargarPosiciones");

            if (leer.Leer())
            {
                cboPosicion.Add(leer.DataSetClase, "NombrePosicion", "NombrePosicion", "Descripcion");
            }

            cboPosicion.SelectedIndex = 0;
        }

        private void Cargar_Ubicacion_Posicion()
        {
            Consulta.MostrarMsjSiLeerVacio = false;

            leer.DataSetClase = Consulta.UbicacionesEstandarALMN(sEmpresa, sEstado, sFarmacia, cboPosicion.Data, "Cargar_Ubicacion_Posicion");

            if (leer.Leer())
            {
                chkStatus.Visible = true;
                chkStatus.Enabled = false;

                txtRack.Text = leer.Campo("IdRack");
                lblRack.Text = leer.Campo("Rack");
                txtNivel.Text = leer.Campo("IdNivel");
                lblNivel.Text = leer.Campo("Nivel");
                txtEntrepaño.Text = leer.Campo("IdEntrepaño");
                lblEntrepaño.Text = leer.Campo("Entrepaño");

                chkStatus.Checked = leer.CampoBool("Status");

                IniciaBotones(true, true, false);

                if (!leer.CampoBool("Status"))
                {
                    IniciaBotones(true, false, false);
                }
            }
            else
            {
                LimpiaTextos();                
                IniciaBotones(true, false, false);
            }

            
        }

        private void LimpiaTextos()
        {
            txtRack.Text = "";
            lblRack.Text = "";
            txtNivel.Text = "";
            lblNivel.Text = "";
            txtEntrepaño.Text = "";
            lblEntrepaño.Text = "";
            chkStatus.Visible = false;
        }
        #endregion Funciones

        #region Evento_Combo
        private void cboPosicion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPosicion.SelectedIndex == 0)
            {
                LimpiaPantalla();
            }
            else
            {
                lblPosicion.Text = cboPosicion.ItemActual.Item.ToString();
                Cargar_Ubicacion_Posicion();
            }
        }
        #endregion Evento_Combo

        #region Eventos_Ubicacion
        private void txtRack_Validating(object sender, CancelEventArgs e)
        {
            if (txtRack.Text.Trim() != "")
            {
                leer2.DataSetClase = Consulta.Pasillos(sEmpresa, sEstado, sFarmacia, txtRack.Text, "txtRack_Validating");

                if (leer2.Leer())
                {
                    txtRack.Text = leer2.Campo("IdPasillo");
                    lblRack.Text = leer2.Campo("DescripcionPasillo");
                }
                else
                {
                    txtRack.Focus();
                }
            }
        }

        private void txtNivel_Validating(object sender, CancelEventArgs e)
        {
            if (txtNivel.Text.Trim() != "")
            {
                leer2.DataSetClase = Consulta.Pasillos_Estantes(sEmpresa, sEstado, sFarmacia, txtRack.Text, txtNivel.Text, "txtNivel_Validating");

                if (leer2.Leer())
                {
                    txtNivel.Text = leer2.Campo("IdEstante");
                    lblNivel.Text = leer2.Campo("DescripcionEstante");
                }
                else
                {
                    txtNivel.Focus();
                }
            }
        }

        private void txtEntrepaño_Validating(object sender, CancelEventArgs e)
        {
            if (txtEntrepaño.Text.Trim() != "")
            {
                leer2.DataSetClase = Consulta.Pasillos_Estantes_Entrepaños(sEmpresa, sEstado, sFarmacia, txtRack.Text, txtNivel.Text, txtEntrepaño.Text, "txtEntrepaño_Validating");

                if (leer2.Leer())
                {
                    txtEntrepaño.Text = leer2.Campo("IdEntrepaño");
                    lblEntrepaño.Text = leer2.Campo("DescripcionEntrepaño");
                }
                else
                {
                    txtEntrepaño.Focus();
                }
            }
        }
        #endregion Eventos_Ubicacion

        #region Guardar_Informacion_ubicacion
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (cboPosicion.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado la Posicion de Ubicación.");
                cboPosicion.Focus();
            }

            if (bRegresa && txtRack.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Rack de Ubicación. Verifique!!");
                txtRack.Focus();
            }

            if (bRegresa && txtNivel.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Nivel de Ubicación. Verifique!!");
                txtNivel.Focus();
            }

            if (bRegresa && txtEntrepaño.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Entrepaño de Ubicación. Verifique!!");
                txtEntrepaño.Focus();
            }

            if (!bCancelar)
            {
                if (bRegresa)
                {
                    bRegresa = ValidaUbicacionPosicion();
                }

                if (bRegresa)
                {
                    bRegresa = ValidarPosicion();
                }
            }

            return bRegresa;
        }

        private bool ValidaUbicacionPosicion()
        {
            bool bRegresa = true;
            string sSql = "";
            string sMsj = "La Ubicación seleccionada ya se encuentra en alguna(s) Posición. \n ¿ Desea Guardar la Información ?";

            sSql = string.Format(" Select * From CFG_ALMN_Ubicaciones_Estandar Where IdEmpresa = '{0}' and IdEstado = '{1}' " +
                                 " and IdFarmacia = '{2}' and IdRack = {3} and IdNivel = {4} and IdEntrepaño = {5} ",
                                 sEmpresa, sEstado, sFarmacia, txtRack.Text, txtNivel.Text, txtEntrepaño.Text);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "ValidaUbicacionPosicion");
                General.msjError("Ocurrio un error al validar la ubicación de Posición.");
            }
            else
            {
                if (leer.Leer())
                {
                    if (General.msjConfirmar(sMsj) == DialogResult.No)
                    {
                        bRegresa = false;
                    }
                }
            }

            return bRegresa;
        }

        private bool ValidarPosicion()
        {
            bool bRegresa = true;
            string sSql = "";
            string sMsj = ""; 

            sSql = string.Format(" Select * From CFG_ALMN_Ubicaciones_Estandar Where IdEmpresa = '{0}' and IdEstado = '{1}' " +
                                 " and IdFarmacia = '{2}' and NombrePosicion = '{3}' ",
                                 sEmpresa, sEstado, sFarmacia, cboPosicion.Data);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "ValidarPosicion");
                General.msjError("Ocurrio un error al validar la Posición.");
            }
            else
            {
                if (leer.Leer())
                {
                    sMsj = "La Posición : " + cboPosicion.Data + " ya cuenta con la ubicación : " + leer.Campo("IdRack") + " - " + leer.Campo("IdNivel") + " - " + leer.Campo("IdEntrepaño") + "\n" +
                        " ¿ Desea actualizarla con la ubicación : " + txtRack.Text + " - " + txtNivel.Text + " - " + txtEntrepaño.Text + " ?";

                    if (General.msjConfirmar(sMsj) == DialogResult.No)
                    {
                        bRegresa = false;
                    }
                }
            }

            return bRegresa;
        }

        private void Guardar_Informacion(int Opcion)
        {
            string sSql = "";
            string sMsj = "Ocurrió un error al guardar la información";

            if (Opcion == 2)
            {
                bCancelar = true;
                sMsj = "Ocurrió un error al cancelar la información";
            }

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    sSql = string.Format(" Exec spp_Mtto_CFG_ALMN_Ubicaciones_Estandar '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ", 
                                        sEmpresa, sEstado, sFarmacia, cboPosicion.Data, sPersonal, txtRack.Text, txtNivel.Text, txtEntrepaño.Text, Opcion);

                    if (!leer.Exec(sSql))
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "Guardar_Informacion");
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
                {
                    General.msjAviso("No fue posible establecer conexión con el servidor. Intente de nuevo.");
                }
            }

        }
        #endregion Guardar_Informacion_ubicacion
    }
}
