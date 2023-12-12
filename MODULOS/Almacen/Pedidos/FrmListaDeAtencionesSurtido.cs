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
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace Almacen.Pedidos
{
    public partial class FrmListaDeAtencionesSurtido : FrmBaseExt
    {
        enum Cols
        {
            Fecha = 1, IdPersonal = 2, Personal = 3, Status = 4, StatusSurtido = 5
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeer leer;
        clsConsultas query;
        clsListView lst;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFolioSurtido = "";

        public FrmListaDeAtencionesSurtido()
        {
            InitializeComponent();

            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmListaDeAtencionesSurtido");

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            lst = new clsListView(lstAtenciones);
            lst.PermitirAjusteDeColumnas = false;

            btnNuevo.Visible = false;
            btnGuardar.Visible = false;
            toolStripSeparator.Visible = false;
            toolStripSeparator1.Visible = false; 
            //TitulosAnchoColumna();
        }

        private void FrmListaDeAtencionesSurtido_Load(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnSalir_Click( object sender, EventArgs e )
        {
            this.Close();
        }

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            lblFolioSurtido.Text = sFolioSurtido;
            CargarAtencionesSurtido();
        }

        private void TitulosAnchoColumna()
        {
            lst.AnchoColumna(1, 136);
            lst.AnchoColumna(2, 90);
            lst.AnchoColumna(3, 338);
            lst.AnchoColumna(4, 55);
            lst.AnchoColumna(5, 140);
            lst.AnchoColumna(6, 150);
           
        }

        private void CargarAtencionesSurtido()
        {
            string sSql = string.Format(
                "  Select FechaRegistro, IdPersonal, NombrePersonal, Status, StatusSurtido, Observaciones " +
                " From vw_Pedidos_Cedis_Enc_Surtido_Atenciones (Nolock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioSurtido = '{3}' " +
                " Order by FolioSurtido, Keyx ", sEmpresa, sEstado, sFarmacia, sFolioSurtido );

            lst.LimpiarItems();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarAtencionesSurtido()");
                General.msjError("Ocurró un error al cargar la lista de atenciones de surtido.");
                this.Close();
            }
            else
            {
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase);
                    TitulosAnchoColumna();
                }
                else
                {
                    General.msjAviso("No se encontraron Atenciones del Folio de Surtido");
                    this.Close();
                }
            }
        }
        #endregion Funciones

        #region Funciones_Publicas
        public void LevantaPantalla(string FolioSurtido)
        {
            this.sFolioSurtido = FolioSurtido;
            this.ShowDialog();            
        }
        #endregion Funciones_Publicas

    }
}
