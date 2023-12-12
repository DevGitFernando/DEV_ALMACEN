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
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Comun;

using DllFarmaciaSoft;


namespace Dll_SII_INadro.Configuracion
{
    public partial class FrmRelacion_ClavesSSA__ClavesSSA_ND : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        clsGrid grid;
        string sIdClaveSSA = ""; 

        public FrmRelacion_ClavesSSA__ClavesSSA_ND()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, General.Modulo, this.Name, General.Version);
            Ayuda = new clsAyudas(General.DatosConexion, General.Modulo, this.Name, General.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.Modulo, General.Version, this.Name);

            grid = new clsGrid(ref grdClaves, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow; 
        }

        private void FrmRelacion_ClavesSSA__ClavesSSA_ND_Load(object sender, EventArgs e)
        {
            CargarFiltros(); 
            InicializarPantalla();
        }

        #region Botones
        private void InicializarPantalla()
        {
            Fg.IniciaControles();
            btnGuardar.Enabled = false;
            btnBusqueda.Enabled = false;

            grid.Limpiar(false);
            txtIdEstado.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int iStatus = chkStatusRelacion.Checked ? 1 : 0; 
            string sSql = string.Format("Exec spp_Mtto_INT_ND_RelacionDeClavesSSA " + 
                " @IdEstado = '{0}', @IdClaveSSA = '{1}', @ClaveSSA = '{2}', @ClaveSSA_Relacionada = '{3}', @Status = '{4}' ",
                txtIdEstado.Text, sIdClaveSSA, txtClaveSSA.Text.Trim(), txtClaveSSA_Relacionda.Text.Trim(), iStatus ); 


            if (validarDatos())
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbriConexion();
                }
                else
                {
                    cnn.IniciarTransaccion();

                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "Guardar");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        leer.Leer();
                        General.msjUser(leer.Campo("Mensaje"));

                        txtClaveSSA.Text = "";
                        txtClaveSSA_Relacionda.Text = "";
                        chkStatusRelacion.Checked = false;
                        EjecutarBusqueda(); 
                    }
                    cnn.Cerrar(); 
                }
            }
        }

        private void CargarFiltros()
        {
            cboFiltros.Clear();
            cboFiltros.Add();

            cboFiltros.Add("ClaveSSA", "Clave SSA");
            cboFiltros.Add("DescripcionClaveSSA", "Descripción Clave SSA");
            cboFiltros.Add("ClaveSSA_ND", "Clave SSA ND");
            cboFiltros.Add("DescripcionClaveSSA_ND", "Descripción Clave SSA ND");

            cboFiltros.SelectedIndex = 0;
        }
        #endregion Botones

        #region Guardar Informacion 
        private bool validarDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtClaveSSA.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Clave SSA a relacionar, verifique.");
                txtClaveSSA.Focus(); 
            }

            if (bRegresa && txtClaveSSA_Relacionda.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Clave SSA relacionada, verifique.");
                txtClaveSSA_Relacionda.Focus();
            }

            return bRegresa; 
        }
        #endregion Guardar Informacion

        #region Estados
        private void txtIdEstado_TextChanged(object sender, EventArgs e)
        {
            lblEstado.Text = "";
            lblEstado.Text = ""; 
        }

        private void txtIdEstado_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdEstado.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Estados(txtIdEstado.Text, "");
                if (leer.Leer())
                {
                    Cargar_InformacionDelEstado();
                }
            }
        }

        private void txtIdEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Estados("txtIdEstado_KeyDown()");
                if (leer.Leer())
                {
                    Cargar_InformacionDelEstado();
                }
            }
        }

        private void Cargar_InformacionDelEstado()
        {
            txtIdEstado.Enabled = false;
            txtIdEstado.Text = leer.Campo("IdEstado");
            lblEstado.Text = leer.Campo("Nombre");

            btnGuardar.Enabled = true;
            btnBusqueda.Enabled = true; 
        }
        #endregion Estados 

        #region Información 
        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            EjecutarBusqueda(); 
        }

        private void EjecutarBusqueda()
        {
            string sSql = "";
            string sFiltro = "";

            if (cboFiltros.SelectedIndex != 0)
            {
                sFiltro = string.Format(" and {0} like '%{1}%' ", cboFiltros.Data, txtBusqueda.Text.Trim().Replace(" " , "%")) ;
            }

            sSql = string.Format("Select IdClaveSSA, ClaveSSA, DescripcionClaveSSA, ClaveSSA_ND, DescripcionClaveSSA_ND, StatusRelacion, StatusRelacionDescripcion  " +
                "From vw_INT_ND_Claves_Relacionadas (noLock) " +
                "Where IdEstado = '{0}'     {1} ", 
                Fg.PonCeros(txtIdEstado.Text.Trim(), 2), sFiltro);

            grid.Limpiar(false);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnBusqueda_Click");
                General.msjError("Ocurrió un error al cargar la información solicitada.");
            }
            else
            {
                grid.LlenarGrid(leer.DataSetClase, false, false);
            }
        }
        #endregion Información

        #region Claves SSA 
        private void txtClaveSSA_TextChanged(object sender, EventArgs e)
        {
            lblClaveSSA.Text = "";
        }

        private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveSSA.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.ClavesSSA(txtClaveSSA.Text, "txtClaveSSA_Validating()");
                if (leer.Leer())
                {
                    Cargar_InformacionDe_ClaveSSA();
                }
            }
        }

        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.ClavesSSA("txtClaveSSA_KeyDown()");
                if (leer.Leer())
                {
                    Cargar_InformacionDe_ClaveSSA();
                }
            }
        }

        private void Cargar_InformacionDe_ClaveSSA()
        {
            sIdClaveSSA = leer.Campo("IdClaveSSA_Sal");
            lblClaveSSA.Text = leer.Campo("DescripcionClave");
        }
        #endregion Claves SSA

        #region Claves SSA Relacionada
        private void txtClaveSSA_Relacionda_TextChanged(object sender, EventArgs e)
        {
            
            lblClaveSSA_Relacionada.Text = "";
        }

        private void txtClaveSSA_Relacionda_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveSSA_Relacionda.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.ClavesSSA_ND(txtClaveSSA_Relacionda.Text, "txtClaveSSA_Relacionda_Validating()");
                if (leer.Leer())
                {
                    Cargar_InformacionDe_ClaveSSA_Relacionada();
                }
            }
        }

        private void txtClaveSSA_Relacionda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.ClavesSSA_ND("txtClaveSSA_Relacionda_KeyDown()");
                if (leer.Leer())
                {
                    Cargar_InformacionDe_ClaveSSA_Relacionada();
                }
            }
        }

        private void Cargar_InformacionDe_ClaveSSA_Relacionada()
        {
            lblClaveSSA_Relacionada.Text = leer.Campo("DescripcionClave");
        }
        #endregion Claves SSA Relacionada 

        #region Grid 
        private void grdClaves_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int iRow = grid.ActiveRow;
            string sClaveSSA = grid.GetValue(iRow, 2);
            string sDescripcion_ClaveSSA = grid.GetValue(iRow, 3);
            string sClaveSSA_Relacionada = grid.GetValue(iRow, 4);
            string sDescripcion_ClaveSSA_Relacionada = grid.GetValue(iRow, 5);
            bool bStatus = grid.GetValueBool(iRow, 6);


            if (sClaveSSA != "")
            {
                txtClaveSSA.Text = sClaveSSA;
                txtClaveSSA_Relacionda.Text = sClaveSSA_Relacionada;
                lblClaveSSA.Text = sDescripcion_ClaveSSA;
                lblClaveSSA_Relacionada.Text = sDescripcion_ClaveSSA_Relacionada;
                chkStatusRelacion.Checked = bStatus; 
            }
        }
        #endregion Grid

        private void btnGenerarRelacionDeClaves_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("Exec spp_INT_ND_Generar_Tabla_INT_ND_CFG__ManejoDeClaves_OPM @IdEstado = '{0}' ", txtIdEstado.Text.Trim());

            if ( General.msjConfirmar("¿ Desea generar la información de Claves OPM ?") == DialogResult.Yes) 
            {
                if (!cnn.Abrir())
                {
                    General.msjErrorAlAbriConexion();
                }
                else
                {
                    cnn.IniciarTransaccion();

                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "GenerarTabla_OPM");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Se generó correctamente la configuración OPM.");
                    }
                    cnn.Cerrar();
                }
            }

        }
    }
}
