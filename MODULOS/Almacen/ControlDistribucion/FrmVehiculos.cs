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
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;

namespace Almacen.ControlDistribucion
{
    public partial class FrmVehiculos : FrmBaseExt
    {
        clsConsultas Consultas;

        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        clsAyudas Ayuda;

        public FrmVehiculos()
        {
            InitializeComponent();

            leer = new clsLeer(ref con);
            Consultas = new clsConsultas(General.DatosConexion, "OficinaCentral", this.Name, Application.ProductVersion);
            Ayuda = new clsAyudas(General.DatosConexion, "OficinaCentral", this.Name, Application.ProductVersion);

        }

        private void FrmVehiculos_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            dtpModelo.MaxDate = General.FechaSistemaObtener();
            lblCancelado.Visible = false;
            btnCancelar.Enabled = false;
            txtId.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                Guardar(1);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Guardar(2);
        }

        #endregion Botones

        #region Eventos

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text.Trim() == "" || txtId.Text.Trim() == "*")
            {
                txtId.Text = "*";
                txtId.Enabled = false;
            }
            else
            {
                leer.DataSetClase = Consultas.Vehiculos( txtId.Text, "txtIdFar_Validating");
                if (leer.Leer())
                {
                    CargarDatosVehiculo();
                }
                else
                {
                    btnNuevo_Click(this, null);
                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Vehiculos("txtId_KeyDown");
                if (leer.Leer())
                {
                    CargarDatosVehiculo();
                }
            }
        }

        #endregion Eventos

        #region Funciones y procedimientos

        private void Guardar(int i)
        {
            string sMensaje = "";
            if (con.Abrir())
            {
                string sSql = string.Format("Exec spp_Mtto_Vehiculos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8}",
                                            DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtId.Text, txtMarca.Text, txtDescripcion.Text,
                                            General.FechaYMD(dtpModelo.Value, "-"), txtNumSerie.Text, txtPlacas.Text, i);

                if (leer.Exec(sSql))
                {
                    if (leer.Leer())
                    {
                        sMensaje = leer.Campo("Mensaje");
                    }

                    con.CompletarTransaccion();
                    General.msjUser(sMensaje); //Este mensaje lo genera el SP
                    btnNuevo_Click(null, null);
                }
                else
                {
                    con.DeshacerTransaccion();
                    General.Error.GrabarError(leer.Error, con.DatosConexion, "Almacen", "1", this.Name, "", leer.QueryEjecutado);
                    General.msjError("Ocurrió un error al guardar la Información.");

                }

                con.Cerrar();
            }
            else
            {
                General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
            }
        }

        private bool Validar()
        {
            bool bRegresa = true;

            if (bRegresa && txtMarca.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("La marca es invalido, verifique.");
                txtMarca.Focus();
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("La Descripción es invalida, verifique.");
                txtDescripcion.Focus();
            }

            if (bRegresa && txtNumSerie.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El número de serie es invalido, verifique.");
                txtNumSerie.Focus();
            }

            if (bRegresa && txtPlacas.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Las Placas son invalidas, verifique.");
                txtPlacas.Focus();
            }

            return bRegresa;
        }

        private void CargarDatosVehiculo()
        {
            txtId.Text = leer.Campo("IdVehiculo");
            txtId.Enabled = false;

            if (txtDescripcion.Text == "")
            {
                txtDescripcion.Text = leer.Campo("Descripcion");
                txtMarca.Text = leer.Campo("Marca");
                txtNumSerie.Text = leer.Campo("NumSerie");
                txtPlacas.Text = leer.Campo("PLacas");
                dtpModelo.Value = leer.CampoFecha("Modelo");

                if (leer.Campo("Status") == "C")
                {
                    lblCancelado.Visible = true;
                    btnCancelar.Enabled = false;
                    General.msjUser("El Vehículo actualmente se encuentra cancelado.");
                }
                else
                {
                    btnCancelar.Enabled = true;
                }
            }
        }

        #endregion Funciones y procedimientos
    }
}
