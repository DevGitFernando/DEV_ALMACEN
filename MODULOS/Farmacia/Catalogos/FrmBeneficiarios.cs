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
    public partial class FrmBeneficiarios : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        string sIdPublicoGral = GnFarmacia.PublicoGral;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        bool bEsCurpGenerica = false;

        Size szTamForma;
        string sQuery_Actualizacion_Identificacion = ""; 
        public FrmBeneficiarios()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            leer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);


            Cargar_TiposDeIdentificacion(); 
            Cargar_TiposDeBeneficiario();
            Cargar_DerechoHabiencias();
            Cargar_EstadosDeResidencia();

            ConfigurarCaptura();
            btnNuevo_Click(this, null);
        }

        private void Cargar_TiposDeBeneficiario()
        {
            cboTipoDeBeneficiario.Clear();
            cboTipoDeBeneficiario.Add(Consultas.Beneficiarios_Tipos(""), true, "IdTipoBeneficiario", "TipoBeneficiario");

            if(!DtGeneral.EsAlmacen)
            {
                cboTipoDeBeneficiario.Data = "1";
                cboTipoDeBeneficiario.Enabled = false;
            }

            cboTipoDeBeneficiario.SelectedIndex = 0;
        }

        private void Cargar_DerechoHabiencias()
        {
            cboDerechoHabiencias.Clear();
            cboDerechoHabiencias.Add("0", "<< Seleccione >>");
            cboDerechoHabiencias.Add(Consultas.TiposDeDerechoHabiencias("", "Cargar_DerechoHabiencias()"), true, "IdTipoDerechoHabiencia", "DerechoHabiencia");
            cboDerechoHabiencias.SelectedIndex = 0;
        }

        private void Cargar_TiposDeIdentificacion()
        {
            cboTiposDeIdentificacion.Clear();
            cboTiposDeIdentificacion.Add("0", "<< Seleccione >>");
            cboTiposDeIdentificacion.Add(Consultas.TiposDeIdentificaciones("", "Cargar_TiposDeIdentificacion()"), true, "IdTipoDeIdentificacion", "TipoDeIdentificacion");
            cboTiposDeIdentificacion.SelectedIndex = 0;
        }

        private void Cargar_EstadosDeResidencia()
        {
            cboEstadosResidencia.Clear();
            cboEstadosResidencia.Add("0", "<< Seleccione >>");
            //cboEstadosResidencia.Filtro = " Status = 'A' ";
            cboEstadosResidencia.Add(Consultas.Estados("Cargar_EstadosDeResidencia()"), true, "IdEstado", "EstadoNombre");
            cboEstadosResidencia.SelectedIndex = 0;
        }

        private void ConfigurarCaptura()
        {
            bool bMostrar = false;
            bool bMostrar_ReferenciaAuxiliar = false;
            int iAlto_Frame_01 = 0;
            int iIncremento_01 = 10;
            int iIncremento_02 = 40;
            this.Height = 375;
            this.Height = 460;
            FrameDatosGenerales.Top = 290;
            FrameDatosPersonales.Height = 155;
            FrameDatosPersonales.Height = 180;
            FrameDatosPersonales.Height = 230;
            FrameDatosPersonales.Height = 260;

            szTamForma = new System.Drawing.Size(this.Width, this.Height);

            FrameReferenciaBeneficiario.Visible = false;
            if(DtGeneral.EsAlmacen)
            {
                bMostrar = true;
                bMostrar_ReferenciaAuxiliar = true;

                iAlto_Frame_01 = txtDomicilio.Top + txtDomicilio.Height + iIncremento_01;
                iAlto_Frame_01 = txtIdJuris.Top + txtIdJuris.Height + iIncremento_01;
                FrameDatosPersonales.Height = iAlto_Frame_01;

                iIncremento_01 = 5;
                FrameDatosGenerales.Top = FrameDatosPersonales.Top + FrameDatosPersonales.Height + iIncremento_01;

                this.Height = 430;
                this.Height = 665;
                this.Height = FrameDatosGenerales.Top + FrameDatosGenerales.Height + iIncremento_02;

                szTamForma = new System.Drawing.Size(this.Width, this.Height);
            }
            else
            {
                label13.Visible = false;
                cboTipoDeBeneficiario.Visible = false;

                lblDomicilio.Visible = false;
                txtDomicilio.Visible = false;

                label22.Visible = false;
                txtIdJuris.Visible = false;
                lblJuris.Visible = false;
            }

            lblDomicilio.Visible = bMostrar;
            txtDomicilio.Visible = bMostrar;


            FrameDatosGenerales.Top = FrameDatosPersonales.Top + FrameDatosPersonales.Height + (int)(iIncremento_01 * .5);

            this.Height = FrameDatosGenerales.Top + FrameDatosGenerales.Height + iIncremento_02;
            szTamForma = new System.Drawing.Size(this.Width, this.Height);

            if(GnFarmacia.ReferenciaAuxiliarBeneficiario || DtGeneral.EsAlmacen)
            {
                FrameReferenciaBeneficiario.Visible = true;
                bMostrar_ReferenciaAuxiliar = true;
                //if (bMostrar)
                //{
                //    this.Height += 100;
                //}
                //else
                //{
                //    FrameReferenciaBeneficiario.Top = FrameDatosGenerales.Top + FrameDatosGenerales.Height;
                //    this.Height = 665;
                //}

                //szTamForma = new System.Drawing.Size(this.Width, this.Height);
            }


            if(!DtGeneral.EsAlmacen)
            {
                if(!GnFarmacia.ReferenciaAuxiliarBeneficiario)
                {
                    bMostrar_ReferenciaAuxiliar = false;
                    FrameReferenciaBeneficiario.Visible = false;
                    txtId_BeneficiarioReferencia.Visible = false;
                    ////lblNombreBeneficiarioReferencia.Visible = false;
                    ////lblReferencia_BeneficiarioReferencia.Visible = false;
                }
            }

            if(bMostrar_ReferenciaAuxiliar)
            {
                FrameReferenciaBeneficiario.Visible = true;
                FrameReferenciaBeneficiario.Top = FrameDatosGenerales.Top + FrameDatosGenerales.Height + (int)(iIncremento_01 * .5);

                this.Height = FrameReferenciaBeneficiario.Top + FrameReferenciaBeneficiario.Height + iIncremento_02;
                szTamForma = new System.Drawing.Size(this.Width, this.Height);
            }

            this.Width = szTamForma.Width;
            this.Height = szTamForma.Height + 5;
        }

        private void FrmBeneficiarios_Load( object sender, EventArgs e )
        {
            Inicializa();
            //btnNuevo_Click(null, null);
        }
        private void btnIdentificacion_Click( object sender, EventArgs e )
        {
            FrmBeneficiarios_Identificacion f = new FrmBeneficiarios_Identificacion();
            f.Mostrar_Identificacion(sEstado, sFarmacia, txtCliente.Text, txtSubCliente.Text, txtBeneficiario.Text);

            sQuery_Actualizacion_Identificacion = f.Query_Actualizacion_Identificacion; 
        }

        private void btnNuevo_Click( object sender, EventArgs e )
        {
            string IdCliente = txtCliente.Text, NombreCliente = lblCliente.Text;
            string IdSubCliente = txtSubCliente.Text, NombreSubCliente = lblSubCliente.Text;

            Fg.IniciaControles(this, true);

            btnIdentificacion.Enabled = false; 
            bEsCurpGenerica = false;

            //Se asigna ya que el Cliente y SubCliente debe mostrarse.
            txtCliente.Text = IdCliente;
            lblCliente.Text = NombreCliente;
            txtSubCliente.Text = IdSubCliente;
            lblSubCliente.Text = NombreSubCliente;

            txtCliente.Enabled = false;
            lblCliente.Enabled = false;
            txtSubCliente.Enabled = false;
            lblSubCliente.Enabled = false;

            lblCancelado.Visible = false;
            lbl_TipoDeCurp.Visible = false;
            txtBeneficiario.Focus();

            if(!DtGeneral.EsAlmacen)
            {
                txtIdJuris.Text = DtGeneral.Jurisdiccion;
                lblJuris.Text = DtGeneral.JurisdiccionNombre;
            }
        }

        private void Inicializa()
        {
            cboSexo.Add("0", "<< Seleccione >>");
            cboSexo.Add("F", "Femenino");
            cboSexo.Add("M", "Masculino");
            cboSexo.SelectedIndex = 0;
        }

        #region Buscar Beneficiario

        private void txtBeneficiario_Validating( object sender, CancelEventArgs e )
        {
            leer = new clsLeer(ref ConexionLocal);

            if(txtBeneficiario.Text.Trim() == "")
            {
                txtBeneficiario.Enabled = false;
                txtBeneficiario.Text = "*";
                btnIdentificacion.Enabled = true; 
            }
            else
            {
                leer.DataSetClase = Consultas.Beneficiarios(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtCliente.Text, txtSubCliente.Text, txtBeneficiario.Text.Trim(), "txtBeneficiario_Validating");
                if(leer.Leer())
                {
                    CargaDatos();
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void txtId_BeneficiarioReferencia_Validating( object sender, CancelEventArgs e )
        {
            ////leer = new clsLeer(ref ConexionLocal);

            ////if (txtId_BeneficiarioReferencia.Text.Trim() != "")
            ////{
            ////    leer.DataSetClase = Consultas.Beneficiarios(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtCliente.Text, txtSubCliente.Text, txtId_BeneficiarioReferencia.Text.Trim(), "txtId_BeneficiarioReferencia_Validating");
            ////    if (leer.Leer())
            ////    {
            ////        CargaDatosReferencia();
            ////    }
            ////    else
            ////    {
            ////        txtId_BeneficiarioReferencia.Text = "";
            ////        ////lblNombreBeneficiarioReferencia.Text = "";
            ////        ////lblReferencia_BeneficiarioReferencia.Text = "";
            ////    }
            ////}
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtBeneficiario.Text = leer.Campo("IdBeneficiario");
            txtNombre.Text = leer.Campo("Nombre");
            txtPaterno.Text = leer.Campo("ApPaterno");
            txtMaterno.Text = leer.Campo("ApMaterno");
            cboSexo.Data = leer.Campo("Sexo");
            dtpFechaNacimiento.Value = leer.CampoFecha("FechaNacimiento");

            cboTiposDeIdentificacion.Data = leer.Campo("IdTipoDeIdentificacion"); 
            txtCURP.Text = leer.Campo("CURP");

            cboDerechoHabiencias.Data = leer.Campo("IdTipoDerechoHabiencia");
            cboEstadosResidencia.Data = leer.Campo("IdEstadoResidencia");

            cboTipoDeBeneficiario.Data = leer.Campo("TipoDeBeneficiario");
            txtDomicilio.Text = leer.Campo("Domicilio");
            txtReferencia.Text = leer.Campo("FolioReferencia");
            dtpFechaInicioVigencia.Value = leer.CampoFecha("FechaInicioVigencia");
            dtpFechaVigencia.Value = leer.CampoFecha("FechaFinVigencia");

            txtId_BeneficiarioReferencia.Text = leer.Campo("FolioReferenciaAuxiliar");
            ////lblNombreBeneficiarioReferencia.Text = leer.Campo("NombreBeneficiarioReferncia");
            ////lblReferencia_BeneficiarioReferencia.Text = leer.Campo("FolioBeneficiario");

            txtBeneficiario.Enabled = false;

            txtIdJuris.Text = leer.Campo("IdJurisdiccion");
            lblJuris.Text = leer.Campo("Jurisdiccion");

            btnIdentificacion.Enabled = true;

            if(leer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                Fg.BloqueaControles(this, false);
            }

            //// 
            validarCurpGenerica(); 
        }

        private void CargaDatosReferencia()
        {
            ////if (leer.CampoBool("EsPadre"))
            ////{
            ////    txtId_BeneficiarioReferencia.Text = leer.Campo("IdBeneficiario");
            ////    lblNombreBeneficiarioReferencia.Text = leer.Campo("ApPaterno") + " " + leer.Campo("ApMaterno") + " " + leer.Campo("Nombre");
            ////    lblReferencia_BeneficiarioReferencia.Text = leer.Campo("FolioReferencia");
            ////}
            ////else
            ////{
            ////    General.msjUser("El beneficiario seleccionado no es padre, no puede ser seleccionado en esta sección.");
            ////    txtId_BeneficiarioReferencia.Text = "";
            ////    lblNombreBeneficiarioReferencia.Text = "";
            ////    lblReferencia_BeneficiarioReferencia.Text = "";
            ////}
        }

        private void btnBusquedaCURP_Click( object sender, EventArgs e )
        {
            Mostrar_CurpsGenericas(); 
        }

        private void Mostrar_CurpsGenericas()
        {
            lbl_TipoDeCurp.Visible = false;
            bEsCurpGenerica = false;

            leer.DataSetClase = Ayuda.CURPS_Genericas(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "txtCURP_KeyDown");
            if(leer.Leer())
            {
                txtCURP.Text = leer.Campo("CURP");

                lbl_TipoDeCurp.Visible = true;
                bEsCurpGenerica = true;
            }
        }

        private void txtCURP_KeyDown( object sender, KeyEventArgs e )
        {
            if(e.KeyCode == Keys.F1)
            {
                Mostrar_CurpsGenericas();

                ////lbl_TipoDeCurp.Visible = false;
                ////bEsCurpGenerica = false;

                ////leer.DataSetClase = Ayuda.CURPS_Genericas(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "txtCURP_KeyDown");
                ////if(leer.Leer())
                ////{
                ////    txtCURP.Text = leer.Campo("CURP");

                ////    lbl_TipoDeCurp.Visible = true;
                ////    bEsCurpGenerica = true;
                ////}
            }
        }

        private void txtCURP_Validating( object sender, CancelEventArgs e )
        {
            lbl_TipoDeCurp.Visible = false;
            bEsCurpGenerica = false;

            if(txtCURP.Text != "" && txtCURP.Text.Trim().Length == 18)
            {
                validarCurpGenerica();
            }
        }

        private void validarCurpGenerica()
        {
            string sSql = string.Format(
            "Select * \n" +
            "From CFGC_CurpsGenericas (NoLock) \n" +
            "Where IdEstado = '{0}' and IdFarmacia = '{1}' and CURP = '{2}' \n", 
            DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtCURP.Text.Trim());

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "validarCurpGenerica()"); 
                General.msjError("Ocurrió un error al validar la CURP.");
            }
            else
            {
                bEsCurpGenerica = leer.Leer();
            }

            lbl_TipoDeCurp.Visible = bEsCurpGenerica; 
        }

        private void btnGenerarCURP_Click( object sender, EventArgs e )
        {
            ////string sSexo = cboSexo.Data == "0" ? "Hombre" : "Mujer";
            ////txtCURP.Text = CURP.Generar_CURP(txtPaterno.Text, txtMaterno.Text, txtNombre.Text, "", dtpFechaNacimiento.Value, sSexo, Convert.ToInt32("0" + cboEstadosResidencia.Text));
        }
        #endregion Buscar Beneficiario

        #region Guardar/Actualizar Beneficiario

        private void btnGuardar_Click( object sender, EventArgs e )
        {
            if(ValidaDatos())
            {
                GuardarInformacion();
            }
        }

        private void GuardarInformacion()
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            bool bEsPadre = false;
            
            //if (ValidaDatos())
            {
                if (!ConexionLocal.Abrir())
                {
                    Error.LogError(ConexionLocal.MensajeError);
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    ConexionLocal.IniciarTransaccion(); 

                    sSql = String.Format("Set Dateformat YMD Exec spp_Mtto_CatBeneficiarios \n" +
                        "\t@IdEstado = '{0}', @IdFarmacia = '{1}', @IdCliente = '{2}', @IdSubCliente = '{3}', \n" +
                        "\t@IdBeneficiario = '{4}', @Nombre = '{5}', @ApPaterno = '{6}', @ApMaterno = '{7}', @Sexo = '{8}', \n" +
                        "\t@FechaNacimiento = '{9}', @FolioReferencia = '{10}', @FechaInicioVigencia = '{11}', @FechaFinVigencia = '{12}', \n" +
                        "\t@iOpcion = '{13}', @Domicilio = '{14}', @FolioReferenciaAuxiliar = '{15}', @IdPersonal = '{16}', @TipoDeBenenficiario = '{17}', \n" +
                        "\t@IdJurisdiccion = '{18}', @CURP = '{19}', @IdEstadoResidencia = '{20}', @IdTipoDerechoHabiencia = '{21}' , @IdTipoDeIdentificacion = '{22}' \n",
                        DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtCliente.Text, txtSubCliente.Text,
                        txtBeneficiario.Text.Trim(), txtNombre.Text.Trim(), txtPaterno.Text.Trim(), txtMaterno.Text.Trim(),
                        cboSexo.Data, General.FechaYMD(dtpFechaNacimiento.Value), txtReferencia.Text.Trim(),
                        General.FechaYMD(dtpFechaInicioVigencia.Value), General.FechaYMD(dtpFechaVigencia.Value),
                        iOpcion, txtDomicilio.Text.Trim(), txtId_BeneficiarioReferencia.Text, DtGeneral.IdPersonal, cboTipoDeBeneficiario.Data,
                        txtIdJuris.Text.Trim(), txtCURP.Text.Trim(), cboEstadosResidencia.Data, cboDerechoHabiencias.Data, cboTiposDeIdentificacion.Data   
                        );

                    sSql = string.Format("{0}\n\n{1}", sSql, sQuery_Actualizacion_Identificacion);

                    if (leer.Exec(sSql))
                    {
                        if(leer.Leer())
                        {
                            sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                        }

                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                        //btnNuevo_Click(null, null);
                        
                    }

                    ConexionLocal.Cerrar();
                }
            }            
        }

        #endregion Guardar/Actualizar Beneficiario

        #region Eliminar Beneficiario

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar el Beneficiario seleccionado ?";

            //Se verifica que no este cancelada.
            if (lblCancelado.Visible == false)
            {
                txtBeneficiario_Validating(txtBeneficiario.Text, null);//Se manda llamar este evento para validar que exista el Beneficiario.
                if (txtNombre.Text.Trim() != "") //Si no esta vacio, significa que si existe.
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if (ConexionLocal.Abrir())
                        {
                            ConexionLocal.IniciarTransaccion();

                            sSql = String.Format("Set Dateformat YMD Exec spp_Mtto_CatBeneficiarios " + 
                                " @IdEstado = '{0}', @IdFarmacia = '{1}', @IdCliente = '{2}', @IdSubCliente = '{3}', @IdBeneficiario = '{4}', @iOpcion = '{5}' ",
                                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtCliente.Text, txtSubCliente.Text,
                                txtBeneficiario.Text.Trim(), iOpcion);



                            if (leer.Exec(sSql))
                            {
                                if (leer.Leer())
                                    sMensaje = String.Format("{0}", leer.Campo("Mensaje"));

                                ConexionLocal.CompletarTransaccion();
                                General.msjUser(sMensaje); //Este mensaje lo genera el SP
                                btnNuevo_Click(null, null);
                            }
                            else
                            {
                                ConexionLocal.DeshacerTransaccion();
                                Error.GrabarError(leer, "btnCancelar_Click");
                                General.msjError("Ocurrió un error al eliminar el Beneficiario.");
                                //btnNuevo_Click(null, null);
                            }

                            ConexionLocal.Cerrar();
                        }
                        else
                        {
                            General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                        }

                    }
                }
            }
            else
            {
                General.msjUser("Este Beneficiario ya esta cancelado");
            }


        }

        #endregion Eliminar Beneficiario

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            
            if (txtBeneficiario.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Beneficiario inválida, verifique.");
                txtBeneficiario.Focus();
            }

            if (bRegresa && txtNombre.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado del Nombre del Beneficiario, verifique.");
                txtNombre.Focus();
            }

            if (!DtGeneral.EsAlmacen)
            {
                // El Apellido parterno es opcional en los Almacenes 
                if (bRegresa && txtPaterno.Text.Trim() == "")
                {
                    bRegresa = false;
                    General.msjUser("No ha capturado el Apellido Paterno, verifique.");
                    txtPaterno.Focus();
                }
            }

            // Se omite hay casos en los que la Persona solo tiene un Apellido 
            //if (bRegresa && txtMaterno.Text.Trim() == "")
            //{
            //    bRegresa = false;
            //    General.msjUser("No ha capturado el Apellido Materno, verifique.");
            //    txtMaterno.Focus();
            //}

            if (DtGeneral.EsAlmacen)
            {
                // El Domicilio es opcional en los Almacenes 
            }

            if (bRegresa && cboSexo.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Sexo válido, verifique.");
                cboSexo.Focus();
            }

            if(bRegresa && cboDerechoHabiencias.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Derechohabiencia válida, verifique.");
                cboDerechoHabiencias.Focus(); 
            }

            if(bRegresa && cboEstadosResidencia.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Estado de Residencia válido, verifique.");
                cboEstadosResidencia.Focus();
            }

            if (bRegresa && lblJuris.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la jurisdiccion, verifique.");
                txtIdJuris.Focus();
            }

            if (!DtGeneral.EsAlmacen)
            {
                if (bRegresa && txtReferencia.Text.Trim() == "")
                {
                    bRegresa = false;
                    General.msjUser("No ha capturado la Referencia, verifique.");
                    txtReferencia.Focus();
                }
            }

            if( !DtGeneral.EsAlmacen && bRegresa)
            {
                if(cboTiposDeIdentificacion.Data != "002")
                {
                    if(txtCURP.Text.Trim() == "")
                    {
                        bRegresa = false;
                        General.msjUser("No ha capturado el Número de identificación del Beneficiario, verifique.");
                        txtCURP.Focus();
                    }
                }
                else
                {
                    if(!DtGeneral.ValidarEstructura_CURP(txtCURP.Text.Trim()))
                    {
                        bRegresa = false;
                        General.msjUser("CURP invalida, verifique.");
                        txtCURP.Focus();
                    }

                    if(bRegresa && !bEsCurpGenerica)
                    {
                        if(!validar__CURP_Unica())
                        {
                            bRegresa = false;
                            txtCURP.Focus();
                        }
                    }
                }
            }


            ////if (!chkEsTitular.Checked)
            ////{
            ////    if (bRegresa && (txtId_BeneficiarioReferencia.Text.Trim() == "" || txtId_BeneficiarioReferencia.Text.Trim() == "*"))
            ////    {
            ////        bRegresa = false;
            ////        General.msjUser("No ha capturado al beneficiario referencia, verifique.");
            ////        txtId_BeneficiarioReferencia.Focus();
            ////    }
            ////}

            return bRegresa;
        }

        private bool validar__CURP_Unica()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_PRCS_ValidarCURP \n" +
                "\t@IdEstado = '{0}', @IdFarmacia = '{1}', @IdCliente = '{2}', @IdSubCliente = '{3}', @IdBeneficiario = '{4}', @CURP = '{5}' ", 
                sEstado, sFarmacia, txtCliente.Text, txtSubCliente.Text, txtBeneficiario.Text, txtCURP.Text.Trim() 
                );

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al validar la CURP.");
            }
            else
            {
                bRegresa = !leer.Leer(); 

                if(!bRegresa)
                {
                    General.msjAviso(string.Format("La CURP {0} ya esta registrada a nombre de {1}", txtCURP.Text.Trim(), leer.Campo("NombreBeneficiario")));
                }
            }

            return bRegresa;  
        }
        #endregion Validaciones de Controles

        #region Eventos

        private void txtBeneficiario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Beneficiarios(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtCliente.Text, txtSubCliente.Text, "txtBeneficiario_KeyDown");

                if (leer.Leer())
                {
                    CargaDatos();
                }
            }
        }

        private void txtId_BeneficiarioReferencia_KeyDown(object sender, KeyEventArgs e)
        {
            ////if (e.KeyCode == Keys.F1)
            ////{
            ////    leer.DataSetClase = Ayuda.Beneficiarios(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtCliente.Text, txtSubCliente.Text, "txtId_BeneficiarioReferencia_KeyDown");

            ////    if (leer.Leer())
            ////    {
            ////        CargaDatosReferencia();
            ////    }
            ////}
        }

        #endregion Eventos

        #region Cliente y Sub-Cliente 
        public void MostrarDetalle( string IdCliente, string NombreCliente, string IdSubCliente, string NombreSubCliente )
        {

            txtCliente.Enabled = false;
            lblCliente.Enabled = false;
            txtSubCliente.Enabled = false;
            lblSubCliente.Enabled = false;

            txtCliente.Text = IdCliente;
            lblCliente.Text = NombreCliente;
            txtSubCliente.Text = IdSubCliente;
            lblSubCliente.Text = NombreSubCliente;

            txtBeneficiario.Focus();
            this.ShowDialog();
        }

        private void txtCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, "txtCte_KeyDown");
                if (leer.Leer())
                {
                    txtCliente.Enabled = false;
                    txtCliente.Text = leer.Campo("IdCliente");
                    lblCliente.Text = leer.Campo("NombreCliente");
                }
            }
        }

        private void txtCliente_Validating(object sender, CancelEventArgs e)
        {
            if (txtCliente.Text.Trim() == "")
            {
                e.Cancel = true; 
            }
            else 
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCliente.Text, "txtCliente_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Cliente no encontrada, ó el Cliente no pertenece a la Farmacia.");
                    e.Cancel = true;
                }
                else
                {
                    txtCliente.Enabled = false;
                    txtCliente.Text = leer.Campo("IdCliente");
                    lblCliente.Text = leer.Campo("NombreCliente");
                }
            }
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            lblCliente.Text = "";
            txtSubCliente.Text = "";
            lblSubCliente.Text = ""; 
        }

        private void txtSubCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCliente.Text.Trim() != "")
                {
                    leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCliente.Text, "txtSubCte_KeyDown_1");
                    if (leer.Leer())
                    {
                        txtSubCliente.Enabled = false;
                        txtSubCliente.Text = leer.Campo("IdSubCliente");
                        lblSubCliente.Text = leer.Campo("NombreSubCliente"); ;
                    }
                }
            }
        }

        private void txtSubCliente_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCliente.Text.Trim() == "")
            {
                e.Cancel = true;
            }
            else
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCliente.Text, txtSubCliente.Text, "txtCte_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Sub-Cliente no encontrada, ó el Sub-Cliente no pertenece a la Farmacia.");
                    e.Cancel = true;
                }
                else
                {
                    txtSubCliente.Enabled = false;
                    txtSubCliente.Text = leer.Campo("IdSubCliente");
                    lblSubCliente.Text = leer.Campo("NombreSubCliente");
                }
            }
        }

        private void txtSubCliente_TextChanged(object sender, EventArgs e)
        {
            lblSubCliente.Text = ""; 
        }
        #endregion Cliente y Sub-Cliente 

        private void txtIdJuris_TextChanged(object sender, EventArgs e)
        {
            lblJuris.Text = "";
        }

        private void txtIdJuris_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdJuris.Text.Trim() == "")
            {
                txtIdJuris.Text = "";
                lblJuris.Text = "";
            }
            else
            {
                leer.DataSetClase = Consultas.Jurisdicciones(sEstado, txtIdJuris.Text.Trim(), "txtIdJuris_Validating");
                if (leer.Leer())
                {
                    if (leer.Campo("Status") == "C")
                    {
                        General.msjAviso(" La Jurisdicción : " + leer.Campo("IdJurisdiccion") + " -- " + leer.Campo("Jurisdiccion") + ". Esta cancelado. ");
                        txtIdJuris.Text = "";
                        lblJuris.Text = "";
                        txtIdJuris.Focus();
                    }
                    else
                    {
                        txtIdJuris.Text = leer.Campo("IdJurisdiccion");
                        lblJuris.Text = leer.Campo("Jurisdiccion");
                    }
                }
            }
        }

        private void txtIdJuris_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Jurisdicciones("txtIdJuris_KeyDown", sEstado);
                if (leer.Leer())
                {
                    if (leer.Campo("Status") == "C")
                    {
                        General.msjAviso(" La Jurisdicción : " + leer.Campo("IdJurisdiccion") + " -- " + leer.Campo("Jurisdiccion") + ". Esta cancelado. ");
                        txtIdJuris.Text = "";
                        lblJuris.Text = "";
                        txtIdJuris.Focus();
                    }
                    else
                    {
                        txtIdJuris.Text = leer.Campo("IdJurisdiccion");
                        lblJuris.Text = leer.Campo("Jurisdiccion");
                    }
                }
            }
        }

        private void cboTiposDeIdentificacion_SelectedIndexChanged( object sender, EventArgs e )
        {
            lblMensaje_TipoDeIdentificacion.Text = cboTiposDeIdentificacion.ItemActual.GetItem("Informacion"); 
        }
    } //Llaves de la clase
}
