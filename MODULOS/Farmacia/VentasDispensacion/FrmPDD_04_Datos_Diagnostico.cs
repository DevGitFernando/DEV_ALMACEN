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


namespace Farmacia.VentasDispensacion
{
    public partial class FrmPDD_04_Datos_Diagnostico : FrmBaseExt 
    {
        clsConexionSQL cnn;
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;

        private string sIdEmpresa = DtGeneral.EmpresaConectada;
        private string sIdEstado = "";
        private string sIdFarmacia = "";

        public string sFolioVenta = "";
        public string sIdDiagnostico = "";
        public string sIdDiagnosticoClave = "";
        public string sIdBeneficioSeguroPopular = "";

        public bool bEsVale = false;

        public FrmPDD_04_Datos_Diagnostico()
        {
            InitializeComponent();
        }

        public FrmPDD_04_Datos_Diagnostico(string IdEstado, string IdFarmacia, string FolioVenta)
        {
            InitializeComponent();

            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
            this.sFolioVenta = FolioVenta;

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);
        }

        private void FrmPDD_04_Datos_Diagnostico_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();

            sIdDiagnostico = sIdDiagnostico == "" ? GnFarmacia.ClaveDiagnostico : sIdDiagnostico;
            txtIdDiagnostico.Text = sIdDiagnostico;
            txtIdDiagnostico_Validating(null, null);

            sIdBeneficioSeguroPopular = sIdBeneficioSeguroPopular == "" ? GnFarmacia.ClaveBeneficio : sIdBeneficioSeguroPopular;
            txtBeneficio.Text = sIdBeneficioSeguroPopular;
            txtBeneficio_Validating(null, null);            

            CargarInformacionAdicionalDeVentas();
        }

        #region Eventos
        private void txtIdDiagnostico_Validating(object sender, CancelEventArgs e)
        {
            //sIdDiagnosticoClave = "";
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

        private void txtBeneficio_Validating(object sender, CancelEventArgs e)
        {
            //sIdBeneficioSeguroPopular = "";
            if (txtBeneficio.Text.Trim() != "")
            {
                leer.DataSetClase = query.BeneficiosSP(txtBeneficio.Text, "txtIdDiagnostico_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Beneficio no encontrada, verifique.");
                    e.Cancel = true;
                }
                else
                {
                    sIdBeneficioSeguroPopular = leer.Campo("IdBeneficio"); ;
                    txtBeneficio.Text = leer.Campo("IdBeneficio");
                    lblBeneficio.Text = leer.Campo("Descripcion");
                }
            }
        }

        private void txtBeneficio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.BeneficiosSP("txtIdDiagnostico_Validating");
                if (leer.Leer())
                {
                    sIdBeneficioSeguroPopular = leer.Campo("IdBeneficio"); ;
                    txtBeneficio.Text = leer.Campo("IdBeneficio");
                    lblBeneficio.Text = leer.Campo("Descripcion");
                }
            } 
        }
        #endregion Eventos

        #region Funciones
        private void CargarInformacionAdicionalDeVentas()
        {
            
            if (sFolioVenta == "*")
            {
                
            }
            else
            {                

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
                    // Cargar el resto de la Informacion
                    txtIdDiagnostico.Text = leer.Campo("IdDiagnostico");
                    lblDiagnostico.Text = leer.Campo("Diagnostico");

                    txtBeneficio.Text = leer.Campo("IdBeneficio");
                    lblBeneficio.Text = leer.Campo("Beneficio");                    
                    
                    FrameDatosAdicionales.Enabled = false;
                }
            }
        }

        private bool ValidarInformacion()
        {
            bool bRegresa = true;

            if (txtIdDiagnostico.Text.Trim() == "")
            {
                bRegresa = false;
                sIdDiagnostico = GnFarmacia.ClaveDiagnostico;
                txtIdDiagnostico.Text = sIdDiagnostico;
            }

            if (bRegresa && txtBeneficio.Text.Trim() == "")
            {
                bRegresa = false;
                sIdBeneficioSeguroPopular = GnFarmacia.ClaveBeneficio;
                txtBeneficio.Text = sIdBeneficioSeguroPopular;
            }
            
            if (!bRegresa)
            {
                bRegresa = true;                 
            }

            // Asignar los valores de salida            
            sIdDiagnostico = txtIdDiagnostico.Text;            
            sIdBeneficioSeguroPopular = txtBeneficio.Text.Trim();

            return bRegresa;
        }
        #endregion Funciones

        #region Eventos_Forma
        private void FrmPDD_04_Datos_Diagnostico_KeyDown(object sender, KeyEventArgs e)
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

        private void lblCerrar_Click(object sender, EventArgs e)
        {
            if (ValidarInformacion())
            {
                this.Close();
            }
        }
    }
}
