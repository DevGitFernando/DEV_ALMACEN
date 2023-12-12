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
using Dll_MA_IFacturacion;

namespace MA_Facturacion.Catalogos
{
    public partial class FrmConceptosEspeciales : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda; 
        clsGrid Grid;
        clsListView lst; 

        DataSet dtsFarmacias;
        string sIdEstado = "";
        string sIdFarmacia = DtGeneral.FarmaciaConectada;
        string sModulo = DtGeneral.ArbolModulo; 

        public FrmConceptosEspeciales()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtIFacturacion.DatosApp, this.Name);

            ayuda = new clsAyudas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name); 
            query = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            lst = new clsListView(lstConceptos);

            CargarEstados(); 
        }

        private void FrmConceptosEspeciales_Load(object sender, EventArgs e)
        {
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            lst.LimpiarItems();

            IniciaToolBar(false, false); 

            if (!DtGeneral.EsAdministrador)
            {
                cboEstados.Data = DtGeneral.EstadoConectado;
                cboEstados.Enabled = false;
                CargarConceptos(); 
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelarRemision_Click(object sender, EventArgs e)
        {

        }

        private void IniciaToolBar(bool Guardar, bool Cancelar)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar; 
        }
        #endregion Botones

        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.Add(query.EstadosConFarmacias("CargarEstados()"), true, "IdEstado", "NombreEstado"); 
            cboEstados.SelectedIndex = 0; 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            if (cboEstados.SelectedIndex != 0)
            {
                CargarConceptos(); 
            }
        }

        private void CargarConceptos()
        {
            string sSql = string.Format("Select 'Concepto' = IdConcepto, 'Descripción' = Descripcion " +
                " From FACT_CFD_ConceptosEspeciales " + 
                " Where IdEstado = '{0}' Order by IdConcepto ", cboEstados.Data);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarConceptos()");
                General.msjError("Ocurrió un error al cargar la lista de conceptos.");
            }
            else
            {
                lst.CargarDatos(leer.DataSetClase, true, true);
                cboEstados.Enabled = !leer.Leer(); 
            }
        }

        #region Concepto 
        private void DatosConceptoEspecial()
        {
            txtFolio.Text = leer.Campo("IdConcepto");
            txtConcepto.Text = leer.Campo("Descripcion");
        }

        private void txtFolio_TextChanged(object sender, EventArgs e)
        {
            txtConcepto.Text = ""; 
        }

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolio.Text != "")
            {
                leer.DataSetClase = query.CFDI_Conceptos_Especiales(cboEstados.Data, txtFolio.Text, "txtFolio_Validating");
                if (leer.Leer())
                {
                    DatosConceptoEspecial();
                }
            }
        }

        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.CFDI_Conceptos_Especiales(cboEstados.Data, "txtFolio_KeyDown");
                if (leer.Leer())
                {
                    DatosConceptoEspecial();
                }
            }
        }
        #endregion Concepto

        private void txtConcepto_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
