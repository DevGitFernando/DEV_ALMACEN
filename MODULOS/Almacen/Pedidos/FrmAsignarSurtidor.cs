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
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.ExportarDatos;
using DllFarmaciaSoft; 


namespace Almacen.Pedidos
{
    public partial class FrmAsignarSurtidor : FrmBaseExt
    {
        int iTipo = 0;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFolioSurtido = "";

        public bool SurtidorAsignado = false;
        public string IdPersonalSurtido = ""; 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        public FrmAsignarSurtidor()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name); 
        }

        private void FrmAsignarSurtidor_Load(object sender, EventArgs e)
        {
            CargarSurtidores();
            InicializarPantalla();
        }

        #region Botones 
        private void InicializarPantalla()
        {
            Fg.IniciaControles();

            cboSurtidores.Data = DtGeneral.IdPersonalCEDIS;
            cboSurtidores.Focus();
            cboSurtidores.Enabled = DtGeneral.IdPersonalCEDIS == ""; 

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cboSurtidores.SelectedIndex == 0)
            {
                General.msjAviso("No ha seleccionado un chofer válido, verifique.");
                cboSurtidores.Focus();
            }
            else
            {
                SurtidorAsignado = true;
                IdPersonalSurtido = cboSurtidores.Data;
                this.Hide(); 
            }
        }

        private void btnSalir_Click( object sender, EventArgs e )
        {
            this.Close();
        }
        #endregion Botones

        #region Funciones y Procedimientos Privados 
        private void CargarSurtidores()
        {
            string sSql = string.Format(
                "Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdPersonal, IdPersonal_Relacionado, Personal, IdPuesto, Puesto, Status \n" + 
                "From vw_PersonalCEDIS (NoLock) \n" +
                "Where IdPuesto = '01' and Status = 'A' \n" +  
                "Order By Personal ");
            cboSurtidores.Add("0", "<< Seleccione >>");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarSurtidores()");
                General.msjError("Ocurrió un error al obtener la Lista de Surtidores.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboSurtidores.Add(leer.DataSetClase, true, "IdPersonal", "Personal");
                }
                cboSurtidores.SelectedIndex = 0; 
            }
        }
        #endregion Funciones y Procedimientos Privados


    }
}
