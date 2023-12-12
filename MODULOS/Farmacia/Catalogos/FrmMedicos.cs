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

using DllFarmaciaSoft;

namespace Farmacia.Catalogos
{
    public partial class FrmMedicos : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer Leer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        string sMensaje = ""; 

        public FrmMedicos()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            Leer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            LlenaEspecialidades();
        }

        private void FrmMedicos_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            IniciaToolBar(true, false, false, false);

            if (DtGeneral.ModuloMA_EnEjecucion != TipoModulo_MA.Ninguno)
            {
                txtPaterno.Enabled = false;
                txtMaterno.Enabled = false;
            }

            txtId.Focus();
        }


        #region Buscar Medico

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            Leer = new clsLeer(ref ConexionLocal);

            if (txtId.Text.Trim() == "")
            {
                IniciaToolBar(true, true, false, false);
                txtId.Enabled = false;
                txtId.Text = "*";
            }
            else
            {
                Leer.DataSetClase = Consultas.Medicos( DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtId.Text.Trim(), "txtId_Validating");
                if (Leer.Leer())
                {
                    CargaDatos();
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtId.Text = Leer.Campo("IdMedico");
            txtNombre.Text = Leer.Campo("Nombre");
            txtPaterno.Text = Leer.Campo("ApPaterno");
            txtMaterno.Text = Leer.Campo("ApMaterno");
            txtCedula.Text = Leer.Campo("NumCedula");
            cboEspecialidad.Data = Leer.Campo("IdEspecialidad");
            txtD_Pais.Text = Leer.Campo("PAIS");
            txtIdMunicipio.Text = Leer.Campo("DireccionIdMunicipio");
            lblMunicipio.Text = Leer.Campo("DireccionMunicipio");
            txtIdColonia.Text = Leer.Campo("DireccionIdColonia");
            lblColonia.Text = Leer.Campo("DireccionColonia");
            txtCalle.Text = Leer.Campo("CALLE");
            txtD_NoExterior.Text = Leer.Campo("NumeroExterior");
            txtD_NoInterior.Text = Leer.Campo("NumeroInterior");
            txtCodigoPostal.Text = Leer.Campo("CodigoPostal");

            if (DtGeneral.ModuloMA_EnEjecucion != TipoModulo_MA.Ninguno)
            {
                txtNombre.Text = Leer.Campo("NombreCompleto");
                txtPaterno.Text = "";
                txtMaterno.Text = "";
            }


            txtId.Enabled = false;
            IniciaToolBar(true, true, true, false);

            if (Leer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                txtNombre.Enabled = false;
                txtPaterno.Enabled = false;
                txtMaterno.Enabled = false;
                txtCedula.Enabled = false;
                cboEspecialidad.Enabled = false;
                IniciaToolBar(true, true, false, false);
            }
 
        }

        #endregion Buscar Medico

        #region Guardar/Actualizar Medico 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensajex = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                if (!ConexionLocal.Abrir())
                {
                    Error.LogError(ConexionLocal.MensajeError);
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    ConexionLocal.IniciarTransaccion();

                    //sSql = String.Format("Exec spp_Mtto_CatMedicos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                    //        DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtId.Text.Trim(), txtNombre.Text.Trim(), 
                    //        txtPaterno.Text.Trim(), txtMaterno.Text.Trim(), txtCedula.Text.Trim(), cboEspecialidad.Data, DtGeneral.IdPersonal, iOpcion );

                    if (Guardar_01_Informacion(iOpcion))
                    {
                        //if (Leer.Leer())
                        //{
                        //    sMensaje = String.Format("{0}", Leer.Campo("Mensaje"));
                        //}

                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(Leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                        //btnNuevo_Click(null, null);
                        
                    }

                    ConexionLocal.Cerrar();
                }
            } 
        }

        private bool Guardar_01_Informacion(int Opcion)
        {
            bool bRegresa = true;
            string sSql = "";

            string sIdDireccion = "";
            string sIdEstado = "";
            string sEstado = "";
            string sIdMunipicio = "";
            string sMunipicio = "";
            string sIdColonia = "";
            string sColonia = "";
            string sCodigoPostal = "";
            string sCalle = "";
            string sStatus = "";


            ////for (int i = 1; i <= lstDirs.Registros; i++)
            {
                ////sIdDireccion = lstDirs.GetValue(i, (int)ColsDireccion.IdDireccion);
                ////sIdEstado = lstDirs.GetValue(i, (int)ColsDireccion.IdEstado);
                ////sEstado = lstDirs.GetValue(i, (int)ColsDireccion.Estado);
                ////sIdMunipicio = lstDirs.GetValue(i, (int)ColsDireccion.IdMunicipio);
                ////sMunipicio = lstDirs.GetValue(i, (int)ColsDireccion.Municipio);
                ////sIdColonia = lstDirs.GetValue(i, (int)ColsDireccion.IdColonia);
                ////sColonia = lstDirs.GetValue(i, (int)ColsDireccion.Colonia);
                ////sDireccion = lstDirs.GetValue(i, (int)ColsDireccion.Direccion);
                ////sCodigoPostal = lstDirs.GetValue(i, (int)ColsDireccion.CodigoPostal);
                ////sStatus = Fg.Mid(lstDirs.GetValue(i, (int)ColsDireccion.Status), 1, 1);
                ////iOpcion = sStatus == "A" ? 1 : 0;

                sIdDireccion = "01";
                sIdMunipicio = txtIdMunicipio.Text.Trim();
                sIdColonia = txtIdColonia.Text.Trim();
                sCalle = txtCalle.Text.Trim();
                //sDireccion = txtCalle.Text.Trim();
                sCodigoPostal = txtCodigoPostal.Text.Trim();
                ////iOpcion = 1;
                ////iOpcion = Opcion == 2 ? 0 : iOpcion;

                sSql = String.Format("Exec spp_Mtto_CatMedicos " + 
                    " @IdEstado = '{0}', @IdFarmacia = '{1}', @IdMedico = '{2}', @Nombre = '{3}', @ApPaterno = '{4}', @ApMaterno = '{5}', " + 
                    " @NumCedula = '{6}', @IdEspecialidad = '{7}', @IdPersonal = '{8}', @iOpcion = '{9}'  ",
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtId.Text.Trim(), txtNombre.Text.Trim(),
                    txtPaterno.Text.Trim(), txtMaterno.Text.Trim(), txtCedula.Text.Trim(), cboEspecialidad.Data, DtGeneral.IdPersonal, Opcion);

                if (!Leer.Exec(sSql))
                {
                    bRegresa = false;
                }
                else
                {
                    Leer.Leer();
                    txtId.Text = Leer.Campo("Clave");
                    sMensaje = String.Format("{0}", Leer.Campo("Mensaje"));

                    sSql = string.Format("Exec spp_Mtto_CatMedicos_Direccion " +
                        " @IdEstado = '{0}', @IdFarmacia = '{1}', @IdMedico = '{2}', @IdDireccion = '{3}', @Pais = '{4}', @IdMunicipio = '{5}', @IdColonia = '{6}', " +
                        " @Calle = '{7}', @NumeroExterior = '{8}', @NumeroInterior = '{9}', @CodigoPostal = '{10}', @Referencia = '{11}', @iOpcion = '{12}' ",
                        DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtId.Text, sIdDireccion, txtD_Pais.Text.Trim(), sIdMunipicio, sIdColonia,
                        sCalle, txtD_NoExterior.Text.Trim(), txtD_NoInterior.Text.Trim(), sCodigoPostal, "", Opcion);

                    if (!Leer.Exec(sSql))
                    {
                        bRegresa = false;
                        //break;
                    }
                }
            }

            return bRegresa;
        }
        #endregion Guardar/Actualizar Medico

        #region Eliminar Medico

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar el Médico seleccionado ?";

            //Se verifica que no este cancelada.
            if (lblCancelado.Visible == false)
            {
                txtId_Validating(txtId.Text, null);//Se manda llamar este evento para validar que exista el Medico.
                if (txtNombre.Text.Trim() != "") //Si no esta vacio, significa que si existe.
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if (ConexionLocal.Abrir())
                        {
                            ConexionLocal.IniciarTransaccion();

                            sSql = String.Format("Exec spp_Mtto_CatMedicos '{0}', '{1}', '{2}', '', '', '', '', '', {3} ",
                                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtId.Text.Trim(), iOpcion);

                            if (Leer.Exec(sSql))
                            {
                                if (Leer.Leer())
                                    sMensaje = String.Format("{0}", Leer.Campo("Mensaje"));

                                ConexionLocal.CompletarTransaccion();
                                General.msjUser(sMensaje); //Este mensaje lo genera el SP
                                btnNuevo_Click(null, null);
                            }
                            else
                            {
                                ConexionLocal.DeshacerTransaccion();
                                Error.GrabarError(Leer, "btnCancelar_Click");
                                General.msjError("Ocurrió un error al eliminar el Médico.");
                                //btnNuevo_Click(null, null);
                            }

                            ConexionLocal.Cerrar();
                        }
                        else
                        {
                            General.msjAviso("No hay conexión al Servidor. Intente de nuevo por favor");
                        }

                    }
                }
            }
            else
            {
                General.msjUser("Este Médico ya fue cancelado");
            }


        }

        #endregion Eliminar Medico

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            // int i = 0;
                        
            if (txtId.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Médico inválida, verifique.");
                txtId.Focus();
            }

            if (bRegresa && txtNombre.Text.Trim() == "" )
            {
                bRegresa = false;
                General.msjUser("No capturado el Nombre, verifique.");
                txtNombre.Focus();
            }


            if (DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Ninguno)
            {
                if (bRegresa && txtPaterno.Text.Trim() == "")
                {
                    bRegresa = false;
                    General.msjUser("No ha capturado el Apellido Paterno, verifique.");
                    txtPaterno.Focus();
                }
            }


            // Se omite hay casos en que la Persona solo tiene un Apellido 
            //if (bRegresa && txtMaterno.Text.Trim() == "")
            //{
            //    bRegresa = false;
            //    General.msjUser("Ingrese el Apellido Materno por favor");
            //    txtMaterno.Focus();
            //}

            if (bRegresa && txtCedula.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado del Número de Cedula Profesional, verifque.");
                txtCedula.Focus();
            }

            if (bRegresa && cboEspecialidad.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No se ha seleccionado una Especialidad, verifque.");
                cboEspecialidad.Focus();
            }

            return bRegresa;
        }

        private bool validarDatosDomicilio()
        {
            bool bRegresa = true;

            if (bRegresa && txtD_Pais.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El País no debe ser vacio, verifique.");
                txtD_Pais.Focus();
            }

            if (bRegresa && txtIdMunicipio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El Municipio no debe ser vacio, verifique.");
                txtIdMunicipio.Focus();
            }

            if (bRegresa && txtIdColonia.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("La Colonia no debe ser vacia, verifique.");
                txtIdColonia.Focus();
            }

            if (bRegresa && txtCalle.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("La Calle no debe ser vacia, verifique.");
                txtCalle.Focus();
            }

            if (bRegresa && txtD_NoExterior.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El Número exterior no debe ser vacio, verifique.");
                txtD_NoExterior.Focus();
            }

            return bRegresa; 
        }

        #endregion Validaciones de Controles

        #region Eventos

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    leer.DataSetClase = ayuda.Medicos(sIdEstado, sIdFarmacia, "txtIdMedico_KeyDown");
            //    if (leer.Leer())
            //    {
            //        txtIdMedico.Text = leer.Campo("IdMedico");
            //        lblMedico.Text = leer.Campo("NombreCompleto");
            //    }
            //}

            if (e.KeyCode == Keys.F1)
            {
                Leer.DataSetClase = Ayuda.Medicos(  DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "txtId_KeyDown");

                if (Leer.Leer())
                {
                    CargaDatos();
                }
            }

        }

        #endregion Eventos

        #region Funciones 
        private void IniciaToolBar(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }
        private void LlenaEspecialidades()
        {
            Leer = new clsLeer(ref ConexionLocal);

            cboEspecialidad.Add("0", "<< Seleccione >>");

            Leer.DataSetClase = Consultas.Especialidades("", "LlenaEspecialidades()");
            if (Leer.Leer())
            {
                cboEspecialidad.Add(Leer.DataSetClase, true);
            }
            cboEspecialidad.SelectedIndex = 0;
        }
        #endregion Funciones

        #region Geograficos

        private void CargarInfMunicipio()
        {
            txtIdMunicipio.Text = Leer.Campo("IdMunicipio");
            lblMunicipio.Text = Leer.Campo("Descripcion");
        }

        private void CargarInfColonia()
        {
            txtIdColonia.Text = Leer.Campo("IdColonia");
            lblColonia.Text = Leer.Campo("Descripcion");
            txtCodigoPostal.Text = Leer.Campo("CodigoPostal");
        }

        private void txtIdMunicipio_TextChanged(object sender, EventArgs e)
        {
            lblMunicipio.Text = "";
            txtIdColonia.Text = "";
            lblColonia.Text = "";
        }

        private void txtIdMunicipio_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdMunicipio.Text.Trim() != "")
            {
                Leer.DataSetClase = Consultas.Municipios(DtGeneral.EstadoConectado, txtIdMunicipio.Text, "txtIdMunicipio_Validating");
                if (!Leer.Leer())
                {
                    txtIdMunicipio.Text = "";
                    txtIdMunicipio.Focus();
                }
                else
                {
                    CargarInfMunicipio();
                }
            }
        }

        private void txtIdMunicipio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Leer.DataSetClase = Ayuda.Municipios("txtIdEstado_KeyDown", DtGeneral.EstadoConectado);
                if (Leer.Leer())
                {
                    CargarInfMunicipio();
                }
            }
        }

        private void txtIdColonia_TextChanged(object sender, EventArgs e)
        {
            lblColonia.Text = "";
            txtCodigoPostal.Text = "";
        }

        private void txtIdColonia_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdMunicipio.Text.Trim() != "" && txtIdColonia.Text != "")
            {
                Leer.DataSetClase = Consultas.Colonias(DtGeneral.EstadoConectado, txtIdMunicipio.Text, txtIdColonia.Text, "txtIdColonia_Validating");
                if (!Leer.Leer())
                {
                    txtIdColonia.Text = "";
                    txtIdColonia.Focus();
                }
                else
                {
                    CargarInfColonia();
                }
            }
        }

        private void txtIdColonia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtIdMunicipio.Text != "")
                {
                    Leer.DataSetClase = Ayuda.Colonias("txtIdEstado_KeyDown", DtGeneral.EstadoConectado, txtIdMunicipio.Text);
                    if (Leer.Leer())
                    {
                        CargarInfColonia();
                    }
                }
            }
        }
        #endregion Geograficos

    } //Llaves de la clase
}
