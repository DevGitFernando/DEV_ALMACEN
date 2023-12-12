using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Ayudas;

using Farmacia.Catalogos;


namespace DllFarmaciaAuditor.Registros
{
    #region Form 
    public partial class Frm_Adt_InformacionVentas : FrmBaseExt
    {
        clsConexionSQL cnn; //= new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;
        DataSet dtsAreas;
        FrmHelpBeneficiarios helpBeneficiarios;


        public bool bCapturaCompleta = false;
        public bool bVigenciaValida = true;
        public bool bEsActivo = false;

        private string sIdEmpresa = DtGeneral.EmpresaConectada; 
        private string sIdEstado = "";
        private string sIdFarmacia = "";

        public bool bEsSeguroPopular = false;
        public bool bValidarBeneficioSeguroPopular = false; 
        public bool bPermitirCapturaBeneficiariosNuevos = false;
        public bool bPermitirImportarBeneficiarios = false; 
        public string sFolioVenta = "";
        public string sIdCliente = "";
        public string sIdClienteNombre = "";        
        public string sIdSubCliente = "";
        public string sIdSubClienteNombre = "";

        public string sIdBeneficiario = "";
        public string sNumReceta = "";
        public string sTipoDispensacion = "";
        public string sUnidadMedica = ""; 
        public DateTime dtpFechaReceta = DateTime.Now;
        public string sIdMedico = "";
        public string sIdDiagnostico = "";
        public string sIdDiagnosticoClave = "";
        public string sIdBeneficioSeguroPopular = ""; 

        public string sIdServicio = "";
        public string sIdArea = "";
        public string sReferenciaObserv = "";


        public string sCLUES_Foranea = "";
        string sIdUMedica_Base = "000000";
        string sCLUES_Base = "SSA000000";

        private string sClaveDispensacionRecetasVales = GnFarmacia.ClaveDispensacionRecetasVales;
        private string sClaveDispensacionRecetasForaneas_Vales = GnFarmacia.ClaveDispensacionRecetasValesForaneos;
        private string sClaveDispensacionRecetasForaneas = GnFarmacia.ClaveDispensacionRecetasForaneas;
        private string sClaveDispensacionUnidadesNoAdministradas = GnFarmacia.ClaveDispensacionUnidadesNoAdministradas;
        
        
        private bool bPermitirFechasRecetAñosAnteriores = false; //GnFarmacia.PermitirFechaRecetas_AñosAnteriores;
        private int iMesesFechasRecetaAñosAnteriores = 0; //GnFarmacia.MesesFechaRecetas_AñosAnteriores;
        private int iMesesAtras_FechaRecetas = GnFarmacia.MesesAtras_FechaRecetas;
        private bool bValidarFoliosUnicosDeRecetas = GnFarmacia.ValidarFoliosDeRecetaUnicos;

        private int iDiasAtras_FechaRecetas = GnFarmacia.DiasHaciaAtras_FechaReceta;
        private int iDiasAdelante_FechaRecetas = GnFarmacia.DiasHaciaAdelante_FechaReceta;

        public Frm_Adt_InformacionVentas()
        {
            InitializeComponent();
        }

        public Frm_Adt_InformacionVentas(string IdEstado, string IdFarmacia, string IdCliente, string NombreCliente, string IdSubCliente, string NombreSubCliente)
        {
            InitializeComponent();

            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
            this.sIdCliente = IdCliente;
            this.sIdClienteNombre = NombreCliente;
            this.sIdSubCliente = IdSubCliente;
            this.sIdSubClienteNombre = NombreSubCliente;

            cnn = new clsConexionSQL(General.DatosConexion); 
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, General.DatosApp, this.Name, false);
            ayuda = new clsAyudas(General.DatosConexion, General.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosApp, this.Name);

        }

        private void Frm_Adt_InformacionVentas_Load(object sender, EventArgs e)
        {
            CargarServicios();
            Fg.IniciaControles();

            txtIdBenificiario.Text = sIdBeneficiario;
            txtIdBenificiario_Validating(null, null);

            txtNumReceta.Text = sNumReceta;
            dtpFechaDeReceta.Value = dtpFechaReceta;
            
            txtIdMedico.Text = sIdMedico;
            txtIdMedico_Validating(null, null);

            txtIdDiagnostico.Text = sIdDiagnostico;
            txtIdDiagnostico_Validating(null, null);

            cboServicios.Data = sIdServicio;
            cboAreas.Data = sIdArea;

            txtRefObservaciones.Text = sReferenciaObserv;

            txtIdBenificiario.Focus();
           
            ///// Algunos Clientes-SubClientes pueden dar de alta Beneficiarios nuevos 
            btnRegistrarBeneficiarios.Enabled = bPermitirCapturaBeneficiariosNuevos;
            btnRegistrarBeneficiarios.Visible = bPermitirCapturaBeneficiariosNuevos;

            //// Cargar la informacion Guardada 
            CargarInformacionAdicionalDeVentas();
        }

        #region Cargar informacion 
        private void CargarServicios()
        {
            cboServicios.Clear();
            cboServicios.Add("0", "<< Seleccione >>");
            cboServicios.Add(query.Farmacia_Servicios(sIdEstado, sIdFarmacia, "CargarServicios()"), true, "IdServicio", "Servicio");
            cboServicios.SelectedIndex = 0;

            cboAreas.Clear();
            cboAreas.Add("0", "<< Seleccione >>");
            dtsAreas = query.Farmacia_ServiciosAreas(sIdEstado, sIdFarmacia, "CargarServicios()");
            cboAreas.SelectedIndex = 0;

            cboTipoDeSurtimiento.Clear();
            cboTipoDeSurtimiento.Add("0", "<< Seleccione >>");
            cboTipoDeSurtimiento.Filtro = " Status = 'A' ";
            ////cboTipoDeSurtimiento.Add(query.TiposDeDispensacion("", GnFarmacia.ClaveDispensacionRecetasVales, "CargarServicios()"), true, "IdTipoDeDispensacion", "Dispensacion");
            cboTipoDeSurtimiento.Add(query.TiposDeDispensacion("", "", "CargarServicios()"), true, "IdTipoDeDispensacion", "Dispensacion");
            cboTipoDeSurtimiento.SelectedIndex = 0; 


        } 
        #endregion Cargar informacion

        #region Eventos privados 
        private void CargarInformacionAdicionalDeVentas() 
        {
            dtpFechaDeReceta.MaxDate = GnFarmacia.FechaOperacionSistema; 
            if (sFolioVenta == "*")
            {
                dtpFechaDeReceta.MinDate = dtpFechaDeReceta.MaxDate.AddMonths(-12); 
            }
            else
            {
                leer.DataSetClase = query.VentaDispensacion_InformacionAdicional(sIdEmpresa, sIdEstado, sIdFarmacia, sFolioVenta, "CargarInformacionAdicionalDeVentas()");
                if (leer.Leer())
                {
                    txtIdBenificiario.Text = leer.Campo("IdBeneficiario"); 
                    lblNombre.Text = leer.Campo("Beneficiario");

                    lblFechaNac.Text = General.FechaYMD(leer.CampoFecha("FechaNacimiento"), "-");
                    lblSexo.Text = leer.Campo("SexoAux");
                    lblEdad.Text = leer.Campo("Edad");
                    lblFolioReferencia.Text = leer.Campo("FolioReferencia");
                    lblFechaVigencia.Text = General.FechaYMD(leer.CampoFecha("FechaInicioVigencia"), "-");

                    bVigenciaValida = leer.CampoBool("EsVigente");
                    if (!bVigenciaValida)
                    {
                        General.msjAviso("La Vigencia del Beneficiario ha expirado, no es posible modificar la venta.");
                    }

                    bEsActivo = true;
                    if (leer.Campo("Status").ToUpper() == "C")
                    {
                        bEsActivo = false;
                        lblStatus.Visible = true;
                        General.msjUser("El Beneficiario se encuentra cancelado, no es posible modificar la venta.");
                    }

                    //// Cargar el resto de la Informacion 
                    dtpFechaDeReceta.Value = leer.CampoFecha("FechaReceta");
                    dtpFechaDeReceta.MinDate = dtpFechaDeReceta.Value.AddDays(-1 * iDiasAtras_FechaRecetas);
                    dtpFechaDeReceta.MaxDate = dtpFechaDeReceta.Value.AddDays(iDiasAdelante_FechaRecetas);


                    txtNumReceta.Text = leer.Campo("NumReceta");
                    cboTipoDeSurtimiento.Data = leer.Campo("IdTipoDeDispensacion");

                    //// Tipo de dispensacion no modificables 
                    if (cboTipoDeSurtimiento.Data == sClaveDispensacionRecetasForaneas ||
                        cboTipoDeSurtimiento.Data == sClaveDispensacionUnidadesNoAdministradas ||
                        cboTipoDeSurtimiento.Data == sClaveDispensacionRecetasForaneas_Vales||
                        cboTipoDeSurtimiento.Data == sClaveDispensacionRecetasVales)
                    {
                        cboTipoDeSurtimiento.Enabled = false; 
                    }


                    txtUMedica.Text = leer.Campo("IdUMedica");
                    lblUnidadMedica.Text = leer.Campo("NombreUMedica"); 

                    txtIdMedico.Text = leer.Campo("IdMedico");
                    lblMedico.Text = leer.Campo("Medico");
                    txtIdDiagnostico.Text = leer.Campo("IdDiagnostico");
                    lblDiagnostico.Text = leer.Campo("Diagnostico");
                    cboServicios.Data = leer.Campo("IdServicio");
                    cboAreas.Data = leer.Campo("IdArea");
                    txtRefObservaciones.Text = leer.Campo("RefObservaciones");

                    //FrameBeneficiario.Enabled = false;
                    //FrameDatosAdicionales.Enabled = false;                    
                }
            }

            btnRegistrarBeneficiarios.Enabled = true;
            btnRegistrarBeneficiarios.Visible = true;
        }

        private void txtIdBenificiario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                helpBeneficiarios = new FrmHelpBeneficiarios();
                leer.DataSetClase = helpBeneficiarios.ShowHelp(sIdCliente, sIdSubCliente, bPermitirImportarBeneficiarios);
                if (leer.Leer())
                {
                    CargarDatosBenefiario();
                    //txtIdBenificiario.Text = leer.Campo("IdBeneficiario");
                    //lblNombre.Text = leer.Campo("NombreCompleto");
                }
            }
        }

        private void CargarDatosBenefiario()
        {
            txtIdBenificiario.Text = leer.Campo("IdBeneficiario");
            lblNombre.Text = leer.Campo("NombreCompleto");

            lblFechaNac.Text = General.FechaYMD(leer.CampoFecha("FechaNacimiento"), "-");
            lblSexo.Text = leer.Campo("SexoAux");
            lblEdad.Text = leer.Campo("Edad");
            lblFolioReferencia.Text = leer.Campo("FolioReferencia");
            lblFechaVigencia.Text = General.FechaYMD(leer.CampoFecha("FechaFinVigencia"), "-");

            bVigenciaValida = leer.CampoBool("EsVigente");
            if (!bVigenciaValida)
            {
                General.msjAviso("La Vigencia del Beneficiario ha expirado, no es posible generar la venta.");
            }

            bEsActivo = true;
            if (leer.Campo("Status").ToUpper() == "C")
            {
                bEsActivo = false;
                lblStatus.Visible = true;
                General.msjUser("El Beneficiario se encuentra cancelado, no es posible generar la venta.");
            } 
        }

        private void txtIdBenificiario_Validating(object sender, CancelEventArgs e)
        {
            bEsActivo = false;
            lblStatus.Visible = false;
            lblStatus.Text = "CANCELADO";

            if (txtIdBenificiario.Text.Trim() != "")
            {
                leer.DataSetClase = query.Beneficiarios(sIdEstado, sIdFarmacia, sIdCliente, sIdSubCliente, txtIdBenificiario.Text, "txtIdBenificiario_Validating");
                if (leer.Leer())
                {
                    CargarDatosBenefiario(); 
                    //txtIdBenificiario.Text = leer.Campo("IdBeneficiario");
                    //lblNombre.Text = leer.Campo("NombreCompleto");

                    //lblFechaNac.Text = General.FechaYMD(leer.CampoFecha("FechaNacimiento"), "-");
                    //lblSexo.Text = leer.Campo("SexoAux");
                    //lblEdad.Text = leer.Campo("Edad");
                    //lblFolioReferencia.Text = leer.Campo("FolioReferencia");
                    //lblFechaVigencia.Text = General.FechaYMD(leer.CampoFecha("FechaVigencia"), "-");

                    //bVigenciaValida = leer.CampoBool("EsVigente");
                    //if (!bVigenciaValida)
                    //{
                    //    General.msjAviso("La Vigencia del Beneficiario ha expirado, no es posible generar la venta."); 
                    //}

                    //bEsActivo = true;
                    //if (leer.Campo("Status").ToUpper() == "C")
                    //{
                    //    bEsActivo = false;
                    //    lblStatus.Visible = true;
                    //    General.msjUser("El Beneficiario se encuentra cancelado, no es posible generar la venta.");
                    //}
                }
                else
                {
                    General.msjUser("Clave de Beneficiario no encontrada, verifique.");
                    Fg.IniciaControles(this, true, FrameBeneficiario);
                    txtIdBenificiario.Text = ""; 
                    txtIdBenificiario.Focus(); 
                }
            }
            else
            {
                Fg.IniciaControles(this, true, FrameBeneficiario);
                txtIdBenificiario.Focus();
            }
        }

        private void cboTipoDeSurtimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUMedica.Enabled = false;
            txtUMedica.Text = sIdUMedica_Base;
            lblUnidadMedica.Text = sCLUES_Base;

            if (cboTipoDeSurtimiento.Data == sClaveDispensacionRecetasForaneas ||
                cboTipoDeSurtimiento.Data == sClaveDispensacionUnidadesNoAdministradas || 
                cboTipoDeSurtimiento.Data == sClaveDispensacionRecetasForaneas_Vales)
            {
                txtUMedica.Enabled = true;
                txtUMedica.Text = "";
                lblUnidadMedica.Text = "";
            }
        }

        private void btnRegistrarBeneficiarios_Click(object sender, EventArgs e)
        {
            FrmBeneficiarios f = new FrmBeneficiarios();
            f.MostrarDetalle(sIdCliente, sIdClienteNombre, sIdSubCliente, sIdSubClienteNombre);

        }

        private void txtIdMedico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Medicos(sIdEstado, sIdFarmacia, "txtIdMedico_KeyDown");
                if (leer.Leer())
                {
                    txtIdMedico.Text = leer.Campo("IdMedico");
                    lblMedico.Text = leer.Campo("NombreCompleto");
                }
            }
        }

        private void txtIdMedico_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdMedico.Text.Trim() == "")
            {
                txtIdMedico.Text = "";
                lblMedico.Text = ""; 
            }
            else 
            {
                leer.DataSetClase = query.Medicos(sIdEstado, sIdFarmacia, txtIdMedico.Text, "txtIdMedico_Validating");
                if (leer.Leer())
                {
                    txtIdMedico.Text = leer.Campo("IdMedico");
                    lblMedico.Text = leer.Campo("NombreCompleto");
                }
                else
                {
                    General.msjUser("Clave de Médico no encontrada, verifique."); 
                    txtIdMedico.Text = "";
                    lblMedico.Text = "";
                    txtIdMedico.Focus(); 
                } 
            }
        }

        private void txtIdDiagnostico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.DiagnosticosCIE10("txtIdDiagnostico_Validating");
                if (leer.Leer())
                {
                    txtIdDiagnostico.Text = leer.Campo("ClaveDiagnostico");
                    lblDiagnostico.Text = leer.Campo("Descripcion");
                }
            }            
        }

        private void txtIdDiagnostico_Validating(object sender, CancelEventArgs e)
        {
            sIdDiagnosticoClave = ""; 
            if (txtIdDiagnostico.Text.Trim() != "")
            {
                if (txtIdDiagnostico.Text.Trim().Length != 4)
                {
                    General.msjUser("Formato de Diagnóstico incorrecto el diagnóstico debe ser de 4 caracteres, verifique.");
                    e.Cancel = true;
                }
                else
                {
                    leer.DataSetClase = query.DiagnosticosCIE10(txtIdDiagnostico.Text, "txtIdDiagnostico_Validating");
                    if (!leer.Leer())
                    {
                        General.msjUser("Clave de Diagnóstico no encontrada, verifique.");
                        e.Cancel = true;
                    }
                    else
                    {
                        sIdDiagnosticoClave = leer.Campo("IdDiagnostico"); ; 
                        txtIdDiagnostico.Text = leer.Campo("ClaveDiagnostico");
                        lblDiagnostico.Text = leer.Campo("Descripcion");
                    }
                }
            }
        }

        private void btnRegistrarMedicos_Click(object sender, EventArgs e)
        {
            FrmMedicos f = new FrmMedicos();
            f.ShowDialog();
        }

        private void cboServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboAreas.Clear();
            cboAreas.Add("0", "<< Seleccione >>");
            if (cboServicios.SelectedIndex != 0)
            {
                try
                {
                    string sWhere = string.Format(" IdServicio = '{0}' ", cboServicios.Data);
                    cboAreas.Add(dtsAreas.Tables[0].Select(sWhere), true, "IdArea", "Area_Servicio");
                }
                catch { }
            }
            cboAreas.SelectedIndex = 0;
        }

        private void txtUMedica_TextChanged(object sender, EventArgs e)
        {
            lblUnidadMedica.Text = "";
        }

        private void txtUMedica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.UnidadesMedicasJurisccion(DtGeneral.EstadoConectado, DtGeneral.Jurisdiccion, "txtUMedica_KeyDown");
                if (leer.Leer())
                {
                    sCLUES_Foranea = leer.Campo("IdUmedica");
                    txtUMedica.Text = sCLUES_Foranea;  // leer.Campo("IdBeneficio");
                    lblUnidadMedica.Text = leer.Campo("CLUES") + " -- " + leer.Campo("NombreUMedica");
                }
            }
        }

        private void txtUMedica_Validating(object sender, CancelEventArgs e)
        {
            // sCLUES_Foranea = "";
            if (txtUMedica.Text.Trim() != "")
            {
                leer.DataSetClase = query.UnidadesMedicasJurisccion(DtGeneral.EstadoConectado, DtGeneral.Jurisdiccion, txtUMedica.Text.Trim(), "txtIdDiagnostico_Validating");
                if (!leer.Leer())
                {
                    // e.Cancel = true;
                    General.msjUser("Clave de Unidad Medica  no encontrada, verifique.");
                }
                else
                {
                    sCLUES_Foranea = leer.Campo("IdUmedica");
                    txtUMedica.Text = sCLUES_Foranea;  // leer.Campo("IdBeneficio");
                    lblUnidadMedica.Text = leer.Campo("CLUES") + " -- " + leer.Campo("NombreUMedica");
                }
            }
        }
        #endregion Eventos privados 

        private void Frm_Adt_InformacionVentas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    if ( ValidarInformacion() )
                        this.Close();
                    break;
                default:
                    break;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            ////if (e.CloseReason == CloseReason.UserClosing)
            ////{
            ////    if (!ValidarInformacion())
            ////        e.Cancel = true;
            ////}
        }

        private void Frm_Adt_InformacionVentas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (!ValidarInformacion())
                {
                    e.Cancel = true;
                }
            }
        }

        private bool ValidarInformacion()
        {
            bool bRegresa = true;

            if (txtIdBenificiario.Text.Trim() == "")
            {
                bRegresa = false;
            }

            if (bRegresa && txtNumReceta.Text.Trim() == "")
            {
                bRegresa = false;
            }

            if (bRegresa && txtIdMedico.Text.Trim() == "")
            {
                bRegresa = false;
            }

            if (bRegresa && txtIdDiagnostico.Text.Trim() == "")
            {
                bRegresa = false;
            }

            if (bRegresa && cboServicios.SelectedIndex == 0)
            {
                bRegresa = false;
            }

            if (bRegresa && cboAreas.SelectedIndex == 0)
            {
                bRegresa = false;
            }

            if (bRegresa && txtRefObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
            }

            if (bRegresa && cboTipoDeSurtimiento.SelectedIndex == 0)
            {
                bRegresa = false;
            }

            if (bRegresa && txtUMedica.Text.Trim() == "")
            {
                bRegresa = false;
            }

            //////////// Queda pendiente para la Siguiente version 2010-03-13 10:35
            ////// Cada Hospital y/o Centro de Salud decide si se pide ó no este dato en las recetas. 
            ////if (bValidarBeneficioSeguroPopular)
            ////{
            ////    if (bRegresa &&  txtBeneficio.Text.Trim() == "")
            ////        bRegresa = false; 
            ////}

            // Marcar la captura como incompleta, para evitar que se guarden datos incompletos 
            bCapturaCompleta = bRegresa;
            if (!bRegresa)
            {
                ////if (bEsSeguroPopular)
                ////{ 
                ////    General.msjAviso("La información requerida en esta pantalla esta incompleta,\nno es posible generar la venta sin esta información."); 
                ////} 
                ////else 
                {
                    if (General.msjConfirmar("La información requerida en esta pantalla esta incompleta.\n\n¿ Desea cerrar la pantalla, no es posible generar la venta sin esta información ?") == DialogResult.Yes)
                    {
                        bRegresa = true;
                    }
                }
            }

            // Asignar los valores de salida 
            //sIdCliente = sIdCliente;
            //sIdSubCliente = sIdSubCliente;
            sIdBeneficiario = txtIdBenificiario.Text;
            sNumReceta = txtNumReceta.Text;
            sTipoDispensacion = cboTipoDeSurtimiento.Data;
            sUnidadMedica = txtUMedica.Text; 
            dtpFechaReceta = dtpFechaDeReceta.Value;
            sIdMedico = txtIdMedico.Text;
            sIdDiagnostico = txtIdDiagnostico.Text;
            sIdServicio = cboServicios.Data;
            sIdArea = cboAreas.Data;
            sReferenciaObserv = txtRefObservaciones.Text;
            sIdBeneficioSeguroPopular = txtBeneficio.Text.Trim() ; 


            return bRegresa;
        } 

        //private void rdoFolio_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtIdBenificiario.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
        //    txtIdBenificiario.MaxLength = 15;
        //    txtIdBenificiario.TextAlign = HorizontalAlignment.Center;
        //}

        //private void rdoCodigo_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtIdBenificiario.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
        //    txtIdBenificiario.MaxLength = 8;
        //    txtIdBenificiario.TextAlign = HorizontalAlignment.Center;
        //}
    }
    #endregion Form

    public class clsInformacionVentas
    {
        private string sIdEmpresa = "";
        private string sIdEstado = "";
        private string sIdFarmacia = "";

        Frm_Adt_InformacionVentas f;
        basGenerales Fg = new basGenerales();

        private bool bCapturaCompleta = false;
        private bool bVigenciaValida = false;
        private bool bEsActivo = false;

        bool bEsSeguroPopular = false;
        bool bPermitirCapturaBeneficiariosNuevos = false;
        bool bPermitirImportarBeneficiarios = false;

        string sIdCliente = "";
        string sIdSubCliente = "";
        string sIdBeneficiario = "";
        string sNumReceta = "";
        string sTipoDispensacion = "";
        string sUnidadMedica = "";
        DateTime dtpFechaReceta = DateTime.Now;
        string sIdMedico = "";
        string sIdDiagnostico = "";
        string sIdDiagnosticoClave = "";
        string sIdServicio = "";
        string sIdArea = "";
        string sReferenciaObserv = "";
        string sIdBeneficioSeguroPopular = "";
        string sCLUES_Foranea = ""; 

        public clsInformacionVentas(string IdEmpresa, string IdEstado, string IdFarmacia)
        {
            this.sIdEmpresa = IdEmpresa; 
            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
        }

        #region Propiedades
        public bool ClienteSeguroPopular
        {
            get { return bEsSeguroPopular; }
            set { bEsSeguroPopular = value; }
        }

        public bool PermitirBeneficiariosNuevos
        {
            get { return bPermitirCapturaBeneficiariosNuevos; }
            set { bPermitirCapturaBeneficiariosNuevos = value; }
        }

        public bool PermitirImportarBeneficiarios
        {
            get { return bPermitirImportarBeneficiarios; }
            set { bPermitirImportarBeneficiarios = value; }
        }

        public bool PermitirGuardar
        {
            get { return bCapturaCompleta; }
        }

        public bool BeneficiarioVigente
        {
            get { return bVigenciaValida; }
        }

        public bool BeneficiarioActivo
        {
            get { return bEsActivo; }
        }

        public string Cliente
        {
            get { return Fg.PonCeros(sIdCliente, 4); }
        }

        public string SubCliente
        {
            get { return Fg.PonCeros(sIdSubCliente, 4); }
        }

        public string Beneficiario
        {
            get { return Fg.PonCeros(sIdBeneficiario, 8); }
        }

        public string Receta
        {
            get { return sNumReceta; }
        }

        public string TipoDispensacion
        {
            get { return sTipoDispensacion; }
        }

        public string CluesRecetasForaneas
        {
            get { return Fg.PonCeros(sCLUES_Foranea, 6); }
        }

        public string UnidadMedica
        {
            get { return sUnidadMedica; }
        }

        public DateTime FechaReceta
        {
            get { return dtpFechaReceta; }
        }

        public string Medico
        {
            get { return Fg.PonCeros(sIdMedico, 6); }
        }

        public string IdDiagnostico
        {
            get { return sIdDiagnosticoClave; }
        }

        public string Diagnostico
        {
            get { return sIdDiagnostico; }
        }

        public string Servicio
        {
            get { return Fg.PonCeros(sIdServicio, 3); }
        }

        public string Area
        {
            get { return Fg.PonCeros(sIdArea, 3); }
        }

        public string ReferenciaObservaciones
        {
            get { return sReferenciaObserv; }
        }
        #endregion Propiedades

        public void Show(string FolioVenta, string IdCliente, string NombreCliente, string IdSubCliente, string NombreSubCliente)
        {
            this.sIdCliente = IdCliente;
            this.sIdSubCliente = IdSubCliente;

            f = new Frm_Adt_InformacionVentas(sIdEstado, sIdFarmacia, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente);

            f.bEsSeguroPopular = bEsSeguroPopular;
            f.bPermitirCapturaBeneficiariosNuevos = bPermitirCapturaBeneficiariosNuevos;
            f.bPermitirImportarBeneficiarios = bPermitirImportarBeneficiarios;

            f.sFolioVenta = FolioVenta;
            f.sIdCliente = sIdCliente;
            f.sIdSubCliente = sIdSubCliente;
            f.sIdBeneficiario = sIdBeneficiario;
            f.sNumReceta = sNumReceta;
            f.sTipoDispensacion = sTipoDispensacion;
            f.sCLUES_Foranea = sCLUES_Foranea == "" ? "000000" : sCLUES_Foranea; 
            f.sUnidadMedica = sUnidadMedica;
            f.dtpFechaReceta = dtpFechaReceta;
            f.sIdMedico = sIdMedico;
            f.sIdDiagnostico = sIdDiagnostico;
            f.sIdServicio = sIdServicio;
            f.sIdArea = sIdArea;
            f.sReferenciaObserv = sReferenciaObserv;
            f.sIdBeneficioSeguroPopular = sIdBeneficioSeguroPopular;
            f.ShowDialog(); // Mostrar la pantalla 


            sIdCliente = f.sIdCliente;
            sIdSubCliente = f.sIdSubCliente;
            sIdBeneficiario = f.sIdBeneficiario;
            sNumReceta = f.sNumReceta;
            sTipoDispensacion = f.sTipoDispensacion;
            sCLUES_Foranea = f.sCLUES_Foranea == "" ? "000000" : f.sCLUES_Foranea; 
            sUnidadMedica = f.sUnidadMedica;
            dtpFechaReceta = f.dtpFechaReceta;
            sIdMedico = f.sIdMedico;
            sIdDiagnostico = f.sIdDiagnostico;
            sIdDiagnosticoClave = f.sIdDiagnosticoClave;
            sIdServicio = f.sIdServicio;
            sIdArea = f.sIdArea;
            sReferenciaObserv = f.sReferenciaObserv;
            bCapturaCompleta = f.bCapturaCompleta;
            bVigenciaValida = f.bVigenciaValida;
            bEsActivo = f.bEsActivo;
            sIdBeneficioSeguroPopular = f.sIdBeneficioSeguroPopular;

            f.Close(); f = null;
        }
    }
}
