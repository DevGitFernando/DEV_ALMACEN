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
using Farmacia.Catalogos;
using DllFarmaciaSoft.Ayudas;

namespace Farmacia.VentasDispensacion
{
    public partial class FrmPDD_03_Datos_Documento : FrmBaseExt 
    {
        clsConexionSQL cnn; 
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;

        private string sIdEmpresa = DtGeneral.EmpresaConectada;
        private string sIdEstado = "";
        private string sIdFarmacia = "";


        public bool bCapturaCompletaDocto = false;
        public bool bEsVale = false;
        public bool bVale_FolioVenta = false;
        public bool bVigenciaValida = true;
        public bool bEsActivo = false;


        private bool bVentanaActiva = false;
        public bool bCerrarInformacionAdicionalAutomaticamente = false;
        public bool bEsSeguroPopular = false;

        public string sFolioVenta = "";
       
        public string sNumReceta = "";
        public DateTime dtpFechaReceta = DateTime.Now;
        public string sIdTipoDispensacion = "";
        public string sIdMedico = "";

        string sIdUMedica_Base = "000000";
        string sCLUES_Base = "SSA000000";

        public string sCLUES_Foranea = "";

        private string sClaveDispensacionRecetasForaneas = GnFarmacia.ClaveDispensacionRecetasForaneas;
        private bool bPermitirFechasRecetAñosAnteriores = false; //GnFarmacia.PermitirFechaRecetas_AñosAnteriores;
        private int iMesesFechasRecetaAñosAnteriores = 0; //GnFarmacia.MesesFechaRecetas_AñosAnteriores;
        private int iMesesAtras_FechaRecetas = GnFarmacia.MesesAtras_FechaRecetas; 
        private int iMesesMargen = 0;

        public FrmPDD_03_Datos_Documento()
        {
            InitializeComponent();
        }

        public FrmPDD_03_Datos_Documento(string IdEstado, string IdFarmacia, string FolioVenta)
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

        private void FrmPDD_03_Datos_Documento_Load(object sender, EventArgs e)
        {
            iMesesMargen = (-1 * 12);
            CargarServicios();
            Fg.IniciaControles();

            txtNumReceta.Text = sNumReceta;
            dtpFechaDeReceta.Value = dtpFechaReceta;
            cboTipoDeSurtimiento.Data = sIdTipoDispensacion;

            txtUMedica.Enabled = false;
            txtUMedica.Text = sCLUES_Foranea;
            txtUMedica_Validating(null, null);

            txtIdMedico.Text = sIdMedico;
            txtIdMedico_Validating(null, null);            

            // Cargar la informacion Guardada 
            CargarInformacionAdicionalDeVentas();

            if (txtUMedica.Text.Trim() == "")
            {
                txtUMedica.Text = sIdUMedica_Base;
                lblUnidadMedica.Text = sCLUES_Base;
            }
        }

        #region Cargar informacion
        private void CargarServicios()
        {
            cboTipoDeSurtimiento.Clear();
            cboTipoDeSurtimiento.Add("0", "<< Seleccione >>");
            // dtsAreas = query.Farmacia_ServiciosAreas(sIdEstado, sIdFarmacia, "CargarServicios()");
            cboTipoDeSurtimiento.Filtro = " Status = 'A' ";
            cboTipoDeSurtimiento.Add(query.TiposDeDispensacion("", GnFarmacia.ClaveDispensacionRecetasVales, "CargarServicios()"), true, "IdTipoDeDispensacion", "Dispensacion");
            cboTipoDeSurtimiento.SelectedIndex = 0;
        }

        private void CargarInformacionAdicionalDeVentas()
        {
            int iMesActual = GnFarmacia.FechaOperacionSistema.Month;
            DateTime dtFechaMinima = GnFarmacia.FechaOperacionSistema;  


            dtpFechaDeReceta.MaxDate = GnFarmacia.FechaOperacionSistema;
            if (sFolioVenta == "*")
            {
                //dtpFechaDeReceta.MinDate = dtpFechaDeReceta.MaxDate.AddMonths(iMesesMargen);

                if (iMesesAtras_FechaRecetas <= 0)
                {
                    dtpFechaDeReceta.MinDate = new DateTime(GnFarmacia.FechaOperacionSistema.Year, iMesActual, 1);
                }
                else
                {
                    iMesesMargen = (-1 * iMesesAtras_FechaRecetas);
                    dtFechaMinima = dtFechaMinima.AddMonths(iMesesMargen);
                    dtpFechaDeReceta.MinDate = new DateTime(dtFechaMinima.Year, dtFechaMinima.Month, 1);
                }

                //////if (!bPermitirFechasRecetAñosAnteriores)
                //////{
                //////    dtpFechaDeReceta.MinDate = new DateTime(GnFarmacia.FechaOperacionSistema.Year, 1, 1);  

                //////    MesesDelAño mesActual =(MesesDelAño)GnFarmacia.FechaOperacionSistema.Month;
                //////    if (mesActual == MesesDelAño.Enero)
                //////    {
                //////        iMesesMargen = (-1 * iMesesFechasRecetaAñosAnteriores);
                //////        dtpFechaDeReceta.MinDate = dtpFechaDeReceta.MaxDate.AddMonths(iMesesMargen);
                //////    }
                //////}
            }
            else
            {
                DateTimePicker dtFechaActual = new DateTimePicker();
                dtpFechaDeReceta.MinDate = dtFechaActual.MinDate;
                dtpFechaDeReceta.MaxDate = dtpFechaDeReceta.MaxDate;

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
                    txtNumReceta.Text = leer.Campo("NumReceta");
                    dtpFechaDeReceta.Value = leer.CampoFecha("FechaReceta");
                    cboTipoDeSurtimiento.Data = leer.Campo("IdTipoDeDispensacion");

                    txtUMedica.Text = leer.Campo("IdUMedica");
                    lblUnidadMedica.Text = leer.Campo("NombreUMedica");

                    txtIdMedico.Text = leer.Campo("IdMedico");
                    lblMedico.Text = leer.Campo("Medico");                    
                    FrameDatosAdicionales.Enabled = false;
                }
            }
        }
        #endregion Cargar informacion

        #region Eventos_Medico
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
        #endregion Eventos_Medico

        #region Eventos_UnidadMedica
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
        #endregion Eventos_UnidadMedica

        private void cboTipoDeSurtimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUMedica.Enabled = false;
            txtUMedica.Text = sIdUMedica_Base;
            lblUnidadMedica.Text = sCLUES_Base;

            if (cboTipoDeSurtimiento.Data == sClaveDispensacionRecetasForaneas)
            {
                txtUMedica.Enabled = true;
                txtUMedica.Text = "";
                lblUnidadMedica.Text = "";
            }
        }

        #region Eventos_Forma
        private void FrmPDD_03_Datos_Documento_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmPDD_03_Datos_Documento_Activated(object sender, EventArgs e)
        {
            if (!bVentanaActiva)
            {
                bVentanaActiva = true;
                if (bCerrarInformacionAdicionalAutomaticamente)
                {
                    if (ValidarInformacion())
                    {
                        this.Close();
                    }
                }
            }
        }

        private void FrmPDD_03_Datos_Documento_FormClosing(object sender, FormClosingEventArgs e)
        {
            ////if (e.CloseReason == CloseReason.UserClosing)
            ////{
            ////    if (!ValidarInformacion())
            ////    {
            ////        e.Cancel = true;
            ////    }
            ////}            
        }
        #endregion Eventos_Forma

        private bool ValidarInformacion()
        {
            bool bRegresa = true;            

            if (txtNumReceta.Text.Trim() == "")
                bRegresa = false;

            if (bRegresa && cboTipoDeSurtimiento.SelectedIndex == 0)
                bRegresa = false;

            if (bRegresa && txtUMedica.Text.Trim() == "")
                bRegresa = false;

            if (bRegresa && txtIdMedico.Text.Trim() == "")
                bRegresa = false;

           
            // Marcar la captura como incompleta, para evitar que se guarden datos incompletos 
            bCapturaCompletaDocto = bRegresa;
            if (!bRegresa)
            {
                if (General.msjConfirmar("La información requerida en esta pantalla esta incompleta.\n\n¿ Desea cerrar la pantalla, no es posible generar la venta sin esta información ?") == DialogResult.Yes)
                {
                    bRegresa = true;
                }
                
            }            
         
            sNumReceta = txtNumReceta.Text;
            dtpFechaReceta = dtpFechaDeReceta.Value;
            sIdTipoDispensacion = cboTipoDeSurtimiento.Data;
            sIdMedico = txtIdMedico.Text;           

            return bRegresa;
        }

        private void btnRegistrarMedicos_Click(object sender, EventArgs e)
        {
            FrmMedicos f = new FrmMedicos();
            f.ShowDialog();
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
