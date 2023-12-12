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
using SC_SolutionsSystem.FuncionesGenerales; 

using DllFarmaciaSoft;
using Dll_IFacturacion;

namespace Facturacion.Catalogos
{
    public partial class FrmVehiculos : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leerCombos;
        clsConsultas Consultas;
        clsAyudas ayuda; 


        string sIdFarmacia = DtGeneral.FarmaciaConectada;
        string sModulo = DtGeneral.ArbolModulo;
        string sIdPublicoGral = GnFarmacia.PublicoGral;


        public FrmVehiculos()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerCombos = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtIFacturacion.DatosApp, this.Name);

            ayuda = new clsAyudas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            Consultas = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);

        }

        private void FrmVehiculos_Load(object sender, EventArgs e)
        {
            CargarClaveVehiculo();
            CargarPermisos();
            LimpiarPantalla();
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();

            dtpModelo.MaxDate = General.FechaSistemaObtener();

            //HabilitarControles(false);
            txtVehiculo.Focus();
            btnCancelar.Enabled = false;
            lblCancelado.Visible = false;

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if(ValidarDatos())
            {
                Guardar(1);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Guardar(2);
            }
        }


        private bool ValidarDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtVehiculo.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Número de operador inválido, verifique.");
                txtVehiculo.Focus();
            }

            if (bRegresa && txtMarca.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Marca inválido, verifique.");
                txtMarca.Focus();
            }

            if (bRegresa && txtNumSerie.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Número de serie inválido, verifique.");
                txtNumSerie.Focus();
            }

            if (bRegresa && txtPlacas.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Número de placa inválido, verifique.");
                txtPlacas.Focus();
            }


            if (bRegresa && cboClaveVehiculo.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado la clave de vehiculo, verifique.");
                cboClaveVehiculo.Focus();
            }

            if (bRegresa && cboPermSCT.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado el tipo de permiso, verifique.");
                cboPermSCT.Focus();
            }

            if (bRegresa && txtNumPermSCT.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Número de permiso inválido, verifique.");
                txtNumPermSCT.Focus();
            }

            if (bRegresa && txtAseguradora.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Aseguradora inválida, verifique.");
                txtAseguradora.Focus();
            }

            if (bRegresa && txtNumPoliza.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Número de poliza inválido, verifique.");
                txtNumPoliza.Focus();
            }

            if (bRegresa && txtAlias.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Alias inválido, verifique.");
                txtAlias.Focus();
            }

            return bRegresa;

        }

        private void txtVehiculo_Validating(object sender, CancelEventArgs e)
        {
            if (txtVehiculo.Text.Trim() == "" || txtVehiculo.Text.Trim() == "*")
            {
                txtVehiculo.Text = "*";
                txtVehiculo.Enabled = false;
            }
            else
            {
                BuscarVehiculo();
            }
        }

        private void BuscarVehiculo()
        {
            string sStatus = "C";

            DateTime Fecha;
            string sSql = string.Format("Select * From vw_FACT_CFDI_Vehiculos Where IdVehiculo = '{0}' ",
                                    Fg.PonCeros(txtVehiculo.Text, 4));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "BuscarDireccion()");
                General.msjError("Ocurrió un error al cargar el vehiculo.");
            }
            else
            {
                if (leer.Leer())
                {
                    btnCancelar.Enabled = true;

                    txtVehiculo.Text = leer.Campo("IdVehiculo");
                    txtVehiculo.Enabled = false;

                    txtMarca.Text = leer.Campo("Marca");

                    Fecha = new DateTime(leer.CampoInt("Modelo"), 1, 1);
                    dtpModelo.Value = Fecha;
                    txtPlacas.Text = leer.Campo("Placa");
                    txtNumSerie.Text = leer.Campo("NumeroDeSerie");


                    cboClaveVehiculo.Data = leer.Campo("ClaveVehiculoSAT");
                    cboPermSCT.Data = leer.Campo("PermSCT");
                    txtNumPermSCT.Text = leer.Campo("NumPermisoSCT");
                    txtAseguradora.Text = leer.Campo("NombreAseg");
                    txtNumPoliza.Text = leer.Campo("NumPolizaSeguro");

                    txtAlias.Text = leer.Campo("Alias");

                    sStatus = leer.Campo("Status");

                    if (sStatus == "C")
                    {
                        lblCancelado.Visible = true;
                    }

                }
                else
                {
                    General.msjUser("Vehiculo no encontrado, verifique.");
                    txtVehiculo.Text = "";
                }
            }
        }

        private void Guardar(int iOpcion)
        {
            string sSql = "", sMensaje = "";

            DateTime Dtm = dtpModelo.Value;

            if (!cnn.Abrir())
            {
                General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
            }
            else
            {
                cnn.IniciarTransaccion();

                sSql = String.Format("EXEC spp_Mtto_FACT_CFDI_Vehiculos \n" + // '{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}' ",
                        " \t@IdVehiculo = '{0}', @Marca = '{1}', @Modelo = '{2}', @Placa = '{3}', @NumeroDeSerie = '{4}', @ClaveVehiculoSAT = '{5}', \n " +
                        " \t@PermSCT = '{6}', @NumPermisoSCT = '{7}', @NombreAseg = '{8}', @NumPolizaSeguro = '{9}', @Alias = '{10}', @IOpcion = {11} \n",
                        txtVehiculo.Text, txtMarca.Text.Trim(), Dtm.Year, txtPlacas.Text.Trim(), txtNumSerie.Text.Trim(), cboClaveVehiculo.Data, 
                        cboPermSCT.Data, txtNumPermSCT.Text, txtAseguradora.Text, txtNumPoliza.Text, txtAlias.Text, iOpcion
                        );


                if (leer.Exec(sSql))
                {
                    if (leer.Leer())
                    {
                        sMensaje = leer.Campo("Mensaje");
                    }

                    cnn.CompletarTransaccion();
                    General.msjUser(sMensaje);
                    btnNuevo_Click(null, null);
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    General.Error.GrabarError(leer.Error, cnn.DatosConexion, "Facturacion", "1", this.Name, "", leer.QueryEjecutado);
                    General.msjError("Ocurrió un error al guardar la información.");

                }

                cnn.Cerrar();
            }
        }

        private void CargarClaveVehiculo()
        {
            string sSql = string.Format("Select Clave, Clave + '--' + Nombre As  Descripcion From FACT_CFDI_AutoTransporte Where Status = 'A' ");

            if (!leerCombos.Exec(sSql))
            {
                Error.GrabarError(leerCombos, "CargarClaveVehiculo()");
                General.msjError("Ocurrió un error al cargar la lista de Claves de Vehiculos SAT.");
            }
            else
            {
                if (leerCombos.Leer())
                {
                    //HabilitarControles(false);
                    cboClaveVehiculo.Clear();
                    cboClaveVehiculo.Add();
                    cboClaveVehiculo.Add(leerCombos.DataSetClase, true, "Clave", "Descripcion");
                    cboClaveVehiculo.SelectedIndex = 0;
                    cboClaveVehiculo.Focus();
                }
            }
        }

        private void CargarPermisos()
        {
            string sSql = string.Format("Select Clave, Clave + '--' + Nombre As Descripcion From FACT_CFDI_TiposDePermiso Where Status = 'A' ");

            if (!leerCombos.Exec(sSql))
            {
                Error.GrabarError(leerCombos, "CargarPermisos()");
                General.msjError("Ocurrió un error al cargar la lista de Permisos.");
            }
            else
            {
                if (leerCombos.Leer())
                {
                    //HabilitarControles(false);
                    cboPermSCT.Clear();
                    cboPermSCT.Add();
                    cboPermSCT.Add(leerCombos.DataSetClase, true, "Clave", "Descripcion");
                    cboPermSCT.SelectedIndex = 0;
                    cboPermSCT.Focus();
                }
            }
        }

        #endregion Botones

    }
}
