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

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Devoluciones;

using Farmacia.Procesos;
using Farmacia.Vales;

using DllFarmaciaSoft.Ayudas;
using Farmacia.Catalogos;

namespace Farmacia.VentasDispensacion
{
    public partial class FrmPDD_02_Datos_Beneficiario : FrmBaseExt
    {
        clsConexionSQL cnn;
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;

        FrmHelpBeneficiarios helpBeneficiarios;

        private string sIdEmpresa = DtGeneral.EmpresaConectada;
        private string sIdEstado = "";
        private string sIdFarmacia = "";        

        public bool bCapturaCompletaBenef = false;
        public bool bEsVale = false;
        public bool bVale_FolioVenta = false;
        public bool bVigenciaValida = true;
        public bool bEsActivo = false;

        private bool bVentanaActiva = false;
        public bool bCerrarInformacionAdicionalAutomaticamente = false;

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
        public string sReferenciaObserv = "";
       
        public FrmPDD_02_Datos_Beneficiario()
        {
            InitializeComponent();
        }

        public FrmPDD_02_Datos_Beneficiario(string IdEstado, string IdFarmacia, string FolioVenta, string IdCliente, string IdSubCliente)
        {
            InitializeComponent();

            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
            this.sFolioVenta = FolioVenta;
            this.sIdCliente = IdCliente;            
            this.sIdSubCliente = IdSubCliente;
            

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);
        }

        private void FrmPDD_02_Datos_Beneficiario_Load(object sender, EventArgs e)
        {            
            Fg.IniciaControles();

            txtIdBenificiario.Text = sIdBeneficiario;
            txtIdBenificiario_Validating(null, null);
            txtIdBenificiario.Focus();

            txtRefObservaciones.Text = sReferenciaObserv;
            ///// Algunos Clientes-SubClientes pueden dar de alta Beneficiarios nuevos 
            btnRegistrarBeneficiarios.Enabled = bPermitirCapturaBeneficiariosNuevos;
            btnRegistrarBeneficiarios.Visible = bPermitirCapturaBeneficiariosNuevos;

            // Cargar la informacion Guardada 
            CargarInformacionAdicionalDeVentas();           
        }

        #region Eventos        

        private void txtIdBenificiario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                helpBeneficiarios = new FrmHelpBeneficiarios();
                leer.DataSetClase = helpBeneficiarios.ShowHelp(sIdCliente, sIdSubCliente, bPermitirImportarBeneficiarios);
                if (leer.Leer())
                {
                    CargarDatosBenefiario();                    
                }
            }
        }
        #endregion Eventos

        #region Botones
        private void btnRegistrarBeneficiarios_Click(object sender, EventArgs e)
        {
            FrmBeneficiarios f = new FrmBeneficiarios();
            f.MostrarDetalle(sIdCliente, sIdClienteNombre, sIdSubCliente, sIdSubClienteNombre);
        }
        #endregion Botones

        #region Funciones
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

        private void CargarInformacionAdicionalDeVentas()
        {
            
            if (sFolioVenta == "*")
            {
                
            }
            else
            {
                DateTimePicker dtFechaActual = new DateTimePicker();
                
                if (!bEsVale)
                {
                    leer.DataSetClase = query.VentaDispensacion_InformacionAdicional(sIdEmpresa, sIdEstado, sIdFarmacia, sFolioVenta, "CargarInformacionAdicionalDeVentas()");
                }
                else
                {
                    leer.DataSetClase = query.ValesEmision_InformacionAdicional(sIdEmpresa, sIdEstado, sIdFarmacia, sFolioVenta, "CargarInformacionAdicionalDeVentas()");
                }

                if (leer.Leer())
                {
                    txtIdBenificiario.Text = leer.Campo("IdBeneficiario");
                    lblNombre.Text = leer.Campo("Beneficiario");

                    lblFechaNac.Text = General.FechaYMD(leer.CampoFecha("FechaNacimiento"), "-");
                    lblSexo.Text = leer.Campo("SexoAux");
                    lblEdad.Text = leer.Campo("Edad");
                    lblFolioReferencia.Text = leer.Campo("FolioReferencia");
                    lblFechaVigencia.Text = General.FechaYMD(leer.CampoFecha("FechaInicioVigencia"), "-");
                    txtRefObservaciones.Text = leer.Campo("RefObservaciones");

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

                    // Cargar el resto de la Informacion                    

                    FrameBeneficiario.Enabled = false;                    
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

            if (bRegresa && txtRefObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
            }                    
            
            // Marcar la captura como incompleta, para evitar que se guarden datos incompletos 
            bCapturaCompletaBenef = bRegresa;
            if (!bRegresa)
            {              
                 if (General.msjConfirmar("La información requerida en esta pantalla esta incompleta.\n\n¿ Desea cerrar la pantalla, no es posible generar la venta sin esta información ?") == DialogResult.Yes)
                    bRegresa = true;                
            }

            // Asignar los valores de salida 
            
            sIdBeneficiario = txtIdBenificiario.Text;
            sReferenciaObserv = txtRefObservaciones.Text;

            return bRegresa;
        }
        #endregion Funciones

        #region Eventos_Forma
        private void FrmPDD_02_Datos_Beneficiario_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    if (ValidarInformacion())
                    {
                        this.Close();
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion Eventos_Forma

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
                }
                else
                {
                    General.msjUser("Clave de Beneficiario no encontrada, verifique.");
                    Fg.IniciaControles(this, true, FrameBeneficiario);
                    txtIdBenificiario.Focus();
                }
            }
            else
            {
                Fg.IniciaControles(this, true, FrameBeneficiario);
                txtIdBenificiario.Focus();
            }
        }

        private void lblCerrar_Click(object sender, EventArgs e)
        {
            if (ValidarInformacion())
            {
                this.Close();
            }
        }
    }
}
