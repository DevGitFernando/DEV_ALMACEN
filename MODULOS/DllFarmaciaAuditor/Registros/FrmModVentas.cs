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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;


using DllFarmaciaAuditor;
using DllFarmaciaSoft;


namespace DllFarmaciaAuditor.Registros
{
    public partial class FrmModVentas : FrmBaseExt
    {        
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);        
        clsLeer leer;
        clsLeer leer2;
        clsLeer leer3;
        clsLeer leer4;
        clsDatosCliente DatosCliente;

        DllFarmaciaSoft.clsConsultas consultas;
        DllFarmaciaSoft.clsAyudas Ayuda;

        clsInformacionVentas InfVtas;

        #region Variables
        string sEmpresa = DllFarmaciaSoft.DtGeneral.EmpresaConectada;
        string sEstado = DllFarmaciaSoft.DtGeneral.EstadoConectado;
        string sFarmacia = DllFarmaciaSoft.DtGeneral.FarmaciaConectada;

        string sIdPublicoGral = "0001"; //DllFarmaciaSoft.DtGeneral.PublicoGral;

        string sIdPersonal = DllFarmaciaSoft.DtGeneral.IdPersonal;

        bool bPermitirCapturaBeneficiariosNuevos = true;
        bool bImportarBeneficiarios = true;
        bool bEsSeguroPopular = true;
        string sFolioVta = "", sFolioMovto = "", sMensaje = "";
        bool bNuevo = false;
        #endregion Variables

        #region Permisos Especiales 
        string sPermisoVentas = "ADT_MODIFICAR_INF_VENTAS";
        bool bPermisoVentas = false;
        #endregion Permisos Especiales

        public FrmModVentas()
        {            
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            leer3 = new clsLeer(ref cnn);
            leer4 = new clsLeer(ref cnn);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");

            consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, General.DatosApp, this.Name, false);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, General.DatosApp, this.Name, false);
        }

        private void FrmModVentas_Load(object sender, EventArgs e)
        {
            SolicitarPermisosUsuario();
            btnNuevo_Click(null, null); 
        }

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            bPermisoVentas = DtGeneral.PermisosEspeciales.TienePermiso(sPermisoVentas);
        }
        #endregion Permisos de Usuario

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            IniciarToolBar(false, false, false);
            grVta.Enabled = true;
            txtFolio.Focus();
            FrameBeneficiario.Enabled = false;
            lblCorregido.Visible = false;

            // Informacion detallada de la venta 
            InfVtas = new clsInformacionVentas(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            bPermitirCapturaBeneficiariosNuevos = false;
            bImportarBeneficiarios = false;
            bNuevo = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    if (ActualizarVenta())
                    {
                        if (ActualizaVtaInformacionAdicional())
                        {
                            bContinua = true;
                        }
                    }

                    if (bContinua)
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje);
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer3, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la Información.");
                    }
                    cnn.Cerrar();
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
        }

        #endregion Botones

        #region Eventos
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolio.Text.Trim() != "")
            {
                //if (ValidarFolio())
                //{
                    leer.DataSetClase = consultas.FolioEnc_Ventas(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");

                    if (!leer.Leer())
                    {
                        General.msjUser("Folio de Venta no encontrado, verifique.");
                        txtFolio.Text = "";
                        txtFolio.Focus();
                    }
                    else
                    {
                        if (leer.CampoInt("TipoDeVenta") != 2)
                        {
                            General.msjUser("El folio de venta capturado no es de venta de credito, verifique.");
                            txtFolio.Text = "";
                            txtFolio.Focus();
                        }
                        else
                        {
                            txtFolio.Text = leer.Campo("Folio");
                            txtCte.Text = leer.Campo("IdCliente");
                            lblCte.Text = leer.Campo("NombreCliente");
                            txtSubCte.Text = leer.Campo("IdSubCliente");
                            lblSubCte.Text = leer.Campo("NombreSubCliente");
                            txtPro.Text = leer.Campo("IdPrograma");
                            lblPro.Text = leer.Campo("Programa");
                            txtSubPro.Text = leer.Campo("IdSubPrograma");
                            lblSubPro.Text = leer.Campo("SubPrograma");

                            //toolTip.SetToolTip(lblCte, lblCte.Text);
                            //toolTip.SetToolTip(lblSubCte, lblSubCte.Text);
                            //toolTip.SetToolTip(lblPro, lblPro.Text);
                            //toolTip.SetToolTip(lblSubPro, lblSubPro.Text);

                            if (leer.Campo("Status") == "C")
                            {
                                lblCancelado.Visible = true;
                            }
                            else
                            {
                                IniciarToolBar(true, false, false);
                                grVta.Enabled = false;
                                CargarInformacionAdicionalDeVentas();
                                txtCteMod.Focus();
                                ValidarFolio();


                                // 2K110813.1153 Habilitar el cierre de periodos 
                                // 2K111214.1900 Habilitar el cierre de periodos 
                                if (leer.CampoInt("FolioCierre") != 0) 
                                {
                                    IniciarToolBar(false, false, true);
                                    General.msjAviso("El folio pertenece a un periodo cerrado, NO es posible realizar cambios.");
                                }
                            }
                        }
                    }
                //}
            }
        }

        private void txtCteMod_Validating(object sender, CancelEventArgs e)
        {
            if (txtCteMod.Text.Trim() == "")
            {
                txtCteMod.Text = "";
                lblCteMod.Text = "";
                txtSubCteMod.Text = "";
                lblSubCteMod.Text = "";
                txtProMod.Text = "";
                lblProMod.Text = "";
                txtSubProMod.Text = "";
                lblSubProMod.Text = "";
                //toolTip.SetToolTip(lblCteMod, "");
                //toolTip.SetToolTip(lblSubCteMod, "");
                //toolTip.SetToolTip(lblProMod, "");
                //toolTip.SetToolTip(lblSubProMod, "");
            }
            else
            {
                if (Fg.PonCeros(txtCteMod.Text, 4) == sIdPublicoGral)
                {
                    General.msjAviso("El Cliente Publico General es exclusivo de Venta a Contado, no puede ser utilizado en Venta a Credito");
                    txtCteMod.Text = "";
                    lblCteMod.Text = "";
                    //toolTip.SetToolTip(lblCteMod, "");
                    e.Cancel = true;
                }
                else
                {
                    leer.DataSetClase = consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCteMod.Text, "txtCteMod_Validating");
                    if (!leer.Leer())
                    {
                        General.msjUser("Clave de Cliente no encontrada, ó el Cliente no pertenece a la Farmacia.");
                        e.Cancel = true;
                    }
                    else
                    {
                        txtCteMod.Enabled = true;
                        txtCteMod.Text = leer.Campo("IdCliente");
                        lblCteMod.Text = leer.Campo("NombreCliente");
                        txtSubCteMod.Text = "";
                        lblSubCteMod.Text = "";
                        txtProMod.Text = "";
                        lblProMod.Text = "";
                        txtSubProMod.Text = "";
                        lblSubCteMod.Text = "";

                        //toolTip.SetToolTip(lblCteMod, lblCteMod.Text);

                        //// Exigir la informacion de Seguro Popular solo si esta activo.
                        //if (bValidarSeguroPopular)
                        //{
                        //    if (sIdSeguroPopular == txtCte.Text.Trim())
                        //        bEsSeguroPopular = true;
                        //}
                    }

                }
            }
        }

        private void txtSubCteMod_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCteMod.Text.Trim() == "")
            {
                txtSubCteMod.Text = "";
                lblSubCteMod.Text = "";
                txtProMod.Text = "";
                lblProMod.Text = "";
                txtSubProMod.Text = "";
                lblSubProMod.Text = "";
                //toolTip.SetToolTip(lblSubCte, "");
                //toolTip.SetToolTip(lblPro, "");
                //toolTip.SetToolTip(lblSubPro, "");
            }
            else
            {
                leer.DataSetClase = consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCteMod.Text, txtSubCteMod.Text, "txtSubCteMod_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Sub-Cliente no encontrada, ó el Sub-Cliente no pertenece a la Farmacia.");
                    e.Cancel = true;
                }
                else
                {
                    ////// Obtener datos de IMach 
                    ////sFolioSolicitud = IMachPtoVta.ObtenerFolioSolicitud(); 

                    txtSubCteMod.Enabled = true;
                    txtSubCteMod.Text = leer.Campo("IdSubCliente");
                    lblSubCteMod.Text = leer.Campo("NombreSubCliente");
                    bPermitirCapturaBeneficiariosNuevos = leer.CampoBool("PermitirCapturaBeneficiarios");
                    bImportarBeneficiarios = leer.CampoBool("PermitirImportaBeneficiarios");

                    txtProMod.Text = "";
                    lblProMod.Text = "";
                    txtSubProMod.Text = "";
                    lblSubProMod.Text = "";
                    //toolTip.SetToolTip(lblSubCte, lblSubCte.Text);
                    if (txtCte.Text.Trim() == txtCteMod.Text.Trim() && txtSubCte.Text.Trim() == txtSubCteMod.Text.Trim())
                    {
                        bNuevo = false;
                    }
                    else
                    {
                        bNuevo = true;
                        MostrarInfoVenta();
                    }
                    

                }
            }
        }

        private void txtProMod_Validating(object sender, CancelEventArgs e)
        {
            if (txtCteMod.Text.Trim() != "" && txtSubCteMod.Text.Trim() != "" && txtProMod.Text.Trim() != "")
            {
                leer.DataSetClase = consultas.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCteMod.Text, txtSubCteMod.Text, txtProMod.Text, "txtProMod_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Programa no encontrada, ó el Programa no pertenece al Cliente ó Farmacia.");
                    txtProMod.Text = "";
                    lblProMod.Text = "";
                    txtSubProMod.Text = "";
                    lblSubProMod.Text = "";
                    //toolTip.SetToolTip(lblPro, "");
                    //toolTip.SetToolTip(lblSubPro, "");
                    e.Cancel = true;
                }
                else
                {
                    txtProMod.Enabled = true;
                    txtProMod.Text = leer.Campo("IdPrograma");
                    lblProMod.Text = leer.Campo("Programa");
                    txtSubProMod.Text = "";
                    lblSubProMod.Text = "";
                    //toolTip.SetToolTip(lblPro, lblPro.Text);
                    //toolTip.SetToolTip(lblSubPro, "");
                }
            }
            else
            {
                txtProMod.Text = "";
                lblProMod.Text = "";
                txtSubProMod.Text = "";
                lblSubProMod.Text = "";
                //toolTip.SetToolTip(lblPro, "");
                //toolTip.SetToolTip(lblSubPro, "");
            }
        }

        private void txtSubProMod_Validating(object sender, CancelEventArgs e)
        {
            if (txtCteMod.Text.Trim() != "" && txtSubCteMod.Text.Trim() != "" && txtProMod.Text.Trim() != "" && txtSubProMod.Text.Trim() != "")
            {
                leer.DataSetClase = consultas.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCteMod.Text, txtSubCteMod.Text, txtProMod.Text, txtSubProMod.Text, "txtSubProMod_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Sub-Programa no encontrada, ó el Sub-Programa no pertenece al Cliente ó Farmacia.");
                    e.Cancel = true;
                }
                else
                {
                    txtSubProMod.Enabled = true;
                    txtSubProMod.Text = leer.Campo("IdSubPrograma");
                    lblSubProMod.Text = leer.Campo("SubPrograma");
                    //toolTip.SetToolTip(lblSubPro, lblSubPro.Text);

                    // Obtener datos de IMach 
                    //sFolioSolicitud = IMachPtoVta.ObtenerFolioSolicitud();

                    // Exclusivo Seguro Popular 
                    //if (bEsSeguroPopular)
                    //    MostrarInfoVenta();

                    //myGrid.Limpiar(true);
                }
            }
            else
            {
                txtSubProMod.Text = "";
                lblSubProMod.Text = "";
                //toolTip.SetToolTip(lblSubPro, "");
            }
        }

        private void txtCteMod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {

                leer2.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, "txtCteMod_KeyDown");
                if (leer2.Leer())
                {
                    txtCteMod.Text = leer2.Campo("IdCliente");
                    lblCteMod.Text = leer2.Campo("NombreCliente");
                    //toolTip.SetToolTip(lblCte, lblCte.Text);
                    txtSubCteMod.Focus();
                }
            }
        }

        private void txtSubCteMod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCteMod.Text.Trim() != "")
                {
                    leer2.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCteMod.Text, "txtSubCteMod_KeyDown");
                    if (leer2.Leer())
                    {
                        txtSubCteMod.Text = leer2.Campo("IdSubCliente");
                        lblSubCteMod.Text = leer2.Campo("NombreSubCliente");
                        //toolTip.SetToolTip(lblSubCte, lblSubCte.Text);
                        txtProMod.Focus();
                    }
                }
            }
        }

        private void txtProMod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCteMod.Text.Trim() != "" && txtSubCteMod.Text.Trim() != "")
                {

                    leer2.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCteMod.Text, txtSubCteMod.Text, "txtProMod_KeyDown");
                    if (leer2.Leer())
                    {
                        txtProMod.Text = leer2.Campo("IdPrograma");
                        lblProMod.Text = leer2.Campo("Programa");
                        //toolTip.SetToolTip(lblPro, lblPro.Text);
                        txtSubProMod.Focus();
                    }
                }
            }
        }

        private void txtSubProMod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {

                if (txtCteMod.Text.Trim() != "" && txtSubCteMod.Text.Trim() != "" && txtProMod.Text.Trim() != "")
                {
                    leer2.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCteMod.Text, txtSubCteMod.Text, txtProMod.Text, "txtSubProMod_KeyDown");
                    if (leer2.Leer())
                    {
                        txtSubProMod.Text = leer2.Campo("IdSubPrograma");
                        lblSubProMod.Text = leer2.Campo("SubPrograma");
                        //toolTip.SetToolTip(lblSubProMod, lblSubProMod.Text);
                    }
                }
            }
        }

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.G:
                    if (btnGuardar.Enabled)
                        btnGuardar_Click(null, null);
                    break;

                case Keys.N:
                    if (btnNuevo.Enabled)
                        btnGuardar_Click(null, null);
                    break;

                case Keys.P:
                    if (btnImprimir.Enabled)
                        btnImprimir_Click(null, null);
                    break;

                default:
                    break;
            }
        }

        private void FrmModVentas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                TeclasRapidas(e);

            switch (e.KeyCode)
            {
                case Keys.F5:
                    MostrarInfoVenta();
                    break;
                default:
                    // base.OnKeyDown(e);
                    break;
            }
        }
        #endregion Eventos

        #region Funciones
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private void MostrarInfoVenta()
        {
            //bool bRegresa = true;

            if (bNuevo)
            {
                if (txtCteMod.Text.Trim() != "" && txtSubCteMod.Text.Trim() != "")
                {
                    sFolioVta = "*";
                    InfVtas.ClienteSeguroPopular = bEsSeguroPopular;
                    InfVtas.PermitirBeneficiariosNuevos = bPermitirCapturaBeneficiariosNuevos;
                    InfVtas.PermitirImportarBeneficiarios = bImportarBeneficiarios;
                    InfVtas.Show(sFolioVta, txtCteMod.Text, lblCteMod.Text, txtSubCteMod.Text, lblSubCteMod.Text);
                }
            }
            else
            {
                if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "")
                {
                    sFolioVta = txtFolio.Text;
                    InfVtas.ClienteSeguroPopular = bEsSeguroPopular;
                    InfVtas.PermitirBeneficiariosNuevos = bPermitirCapturaBeneficiariosNuevos;
                    InfVtas.PermitirImportarBeneficiarios = bImportarBeneficiarios;
                    InfVtas.Show(sFolioVta, txtCte.Text, lblCte.Text, txtSubCte.Text, lblSubCte.Text);
                }
            }
            
            
        }

        private void CargarInformacionAdicionalDeVentas()
        {
            sFolioVta = txtFolio.Text;

            leer4.DataSetClase = consultas.VentaDispensacion_InformacionAdicional(sEmpresa, sEstado, sFarmacia, sFolioVta, "CargarInformacionAdicionalDeVentas()");
            if (leer4.Leer())
            {
                txtIdBenificiario.Text = leer4.Campo("IdBeneficiario");
                lblNombre.Text = leer4.Campo("Beneficiario");

                lblFechaNac.Text = General.FechaYMD(leer4.CampoFecha("FechaNacimiento"), "-");
                lblSexo.Text = leer4.Campo("SexoAux");
                lblEdad.Text = leer4.Campo("Edad");
                lblFolioReferencia.Text = leer4.Campo("FolioReferencia");
                lblFechaVigencia.Text = General.FechaYMD(leer4.CampoFecha("FechaInicioVigencia"), "-");

                if (leer4.Campo("Status").ToUpper() == "C")
                {                    
                    lblStatus.Visible = true;                    
                }

                //// Cargar el resto de la Informacion 
                //txtNumReceta.Text = leer.Campo("NumReceta");
                //txtIdMedico.Text = leer.Campo("IdMedico");
                //lblMedico.Text = leer.Campo("Medico");
                //txtIdDiagnostico.Text = leer.Campo("IdDiagnostico");
                //lblDiagnostico.Text = leer.Campo("Diagnostico");
                //cboServicios.Data = leer.Campo("IdServicio");
                //cboAreas.Data = leer.Campo("IdArea");
                //txtRefObservaciones.Text = leer.Campo("RefObservaciones");                                     
                
            }           
        }

        private bool ActualizarVenta()
        {
            bool bRegresa = false;
            string Cte = "", SubCte = "", Pro = "", SubPro = "", sFolMovto = "*";
            int iOpcion = 0;

            if (txtCteMod.Text.Trim() == "" || txtSubCteMod.Text.Trim() == "")
            {
                Cte = txtCte.Text;
                SubCte = txtSubCte.Text;
                Pro = txtPro.Text;
                SubPro = txtSubPro.Text;
            }
            else
            {
                Cte = txtCteMod.Text;
                SubCte = txtSubCteMod.Text;
                Pro = txtProMod.Text;
                SubPro = txtSubProMod.Text;
            }

            string sSql = string.Format(" Set DateFormat YMD EXEC spp_Mtto_Adt_VentasEnc '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}' ",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text, Cte, SubCte, Pro, SubPro, iOpcion, sFolMovto, sIdPersonal);

            if (leer3.Exec(sSql))
            {
                if (leer3.Leer())
                {
                    sFolioMovto = leer3.Campo("FolioMovto");
                    sMensaje = leer3.Campo("Mensaje");
                    bRegresa = true;
                }
            }

            return bRegresa;
        }

        private bool ActualizaVtaInformacionAdicional()
        {
            bool bRegresa = true;

            string sSql = string.Format(" EXEC spp_Mtto_Adt_VentasInformacionAdicional " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}', @IdBeneficiario = '{4}', " + 
                " @NumReceta = '{5}', @FechaReceta = '{6}', @IdMedico = '{7}', @IdDiagnostico = '{8}', @IdServicio = '{9}', @IdArea = '{10}', " + 
                " @RefObservaciones = '{11}', @iOpcion = '{12}', @FolioMovto = '{13}', @IdPersonal = '{14}', @IdTipoDeDispensacion = '{15}', @IdUMedica = '{16}' ",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text, InfVtas.Beneficiario, InfVtas.Receta, General.FechaYMD(InfVtas.FechaReceta, "-"),
                InfVtas.Medico, InfVtas.Diagnostico, InfVtas.Servicio, InfVtas.Area, InfVtas.ReferenciaObservaciones, 1, sFolioMovto, sIdPersonal,
                InfVtas.TipoDispensacion, InfVtas.CluesRecetasForaneas);

            if (!leer3.Exec(sSql))
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            

            if (txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Venta inválido, verifique.");
                txtFolio.Focus();
            }

            if (bRegresa && txtCte.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Cliente inválida, verifique.");
                txtCte.Focus();
            }

            if (bRegresa && txtSubCte.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de SubCliente inválida, verifique.");
                txtSubCte.Focus();
            }

            if (bRegresa && txtPro.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Programa inválida, verifique.");
                txtPro.Focus();
            }

            if (bRegresa && txtSubPro.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de SubPrograma inválida, verifique.");
                txtSubPro.Focus();
            }

            if (bRegresa && lblCteMod.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Cliente inválida, verifique.");
                txtCteMod.Focus();
            }

            if (bRegresa && lblSubCteMod.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de SubCliente inválida, verifique.");
                txtSubCteMod.Focus();
            }

            if (bRegresa && lblProMod.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Programa inválida, verifique.");
                txtProMod.Focus();
            }

            if (bRegresa && lblSubProMod.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de SubPrograma inválida, verifique.");
                txtSubProMod.Focus();
            }

            if (bRegresa)
            {
                bRegresa = validarInfAdicional_FarmaciasAlmacen();               
            }

            return bRegresa;
        }
                
        private bool validarInfAdicional_FarmaciasAlmacen()
        {
            bool bRegresa = true;
            if (bRegresa && !InfVtas.PermitirGuardar)
            {
                bRegresa = false;
                General.msjUser("La información adicional de la venta no esta capturada, verifique.");
            }

            if (bRegresa && !InfVtas.BeneficiarioVigente)
            {
                bRegresa = false;
                General.msjUser("La Vigencia del Beneficiario expiro, verifique.");
            }

            if (bRegresa && !InfVtas.BeneficiarioActivo)
            {
                bRegresa = false;
                General.msjUser("El Beneficiario se encuentra cancelado, no es posible realizar cambios.");
            }
            return bRegresa;
        }

        private bool ValidarFolio()             
        {
            bool bRetorno = false;

            leer.DataSetClase = consultas.Adt_Folio_VentasEnc(sEmpresa, sEstado, sFarmacia, txtFolio.Text, "ValidarFolio");

            if (leer.Leer())
            {
                if (DllFarmaciaSoft.DtGeneral.EsAdministrador || bPermisoVentas)
                {
                    bRetorno = true;
                    lblCorregido.Visible = true;
                    lblCorregido.Text = "CORREGIDO";
                }
                else
                {
                    General.msjUser("El folio de venta ya tiene correcciones aplicadas, verifique.");
                    btnNuevo_Click(null, null);
                }
            }
            else
            {
                bRetorno = true;
            }

            return bRetorno;
        }

        #endregion Funciones        

        #region Eventos_TextChanged
        private void txtCteMod_TextChanged(object sender, EventArgs e)
        {
            lblCteMod.Text = "";
        }

        private void txtSubCteMod_TextChanged(object sender, EventArgs e)
        {
            lblSubCteMod.Text = "";
        }

        private void txtProMod_TextChanged(object sender, EventArgs e)
        {
            lblProMod.Text = "";
        }

        private void txtSubProMod_TextChanged(object sender, EventArgs e)
        {
            lblSubProMod.Text = "";
        }
        #endregion Eventos_TextChanged
    }
}
