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
//using Dll_IMach4;

using DllFarmaciaSoft.Reporteador;

namespace Farmacia.Vales
{
    public partial class FrmListaValesNoRegistrados : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;        
        DllFarmaciaSoft.clsAyudas Ayuda;
        
        DllFarmaciaSoft.clsConsultas Consultas;
        clsGrid Grid;
        

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; 
        
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        private enum Cols
        {
            Ninguna = 0,
            Folio = 1, Fecha = 2, IdBeneficiario = 3, Beneficiario = 4
        }

        public FrmListaValesNoRegistrados()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            leer = new clsLeer(ref ConexionLocal);            

            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            Grid = new clsGrid(ref grdVales, this);
            Grid.BackColorColsBlk = Color.White;
            grdVales.EditModeReplace = true;
            Grid.AjustarAnchoColumnasAutomatico = true; 

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
        }

        private void FrmListaValesNoRegistrados_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            LlenarGrid();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            Grid.Limpiar(true);
        }

        private void LlenarGrid()
        {
            string sSql = string.Format(" Select V.FolioVale, Convert(varchar(10), V.FechaRegistro , 120) As FechaRegistro, " + 
                                        " I.IdBeneficiario, I.Beneficiario " +
	                                    " From Vales_EmisionEnc V (Nolock) " +
	                                    " Inner Join vw_Vales_Emision_InformacionAdicional I (Nolock) " +
		                                    " On( V.IdEmpresa = I.IdEmpresa And V.IdEstado = I.IdEstado And V.IdFarmacia = I.IdFarmacia And V.FolioVale = I.Folio ) " +	
	                                    " Where V.IdEmpresa = '{0}' And V.IdEstado = '{1}' And V.IdFarmacia = '{2}' And V.Status = 'A' " +
	                                    " Order By V.FolioVale, V.FechaRegistro ", sEmpresa, sEstado, sFarmacia );

            Grid.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "LlenarGrid");
                General.msjError("Ocurrió un error al Buscar la información.");
            }
            else
            {
                if (leer.Leer())
                {
                    Grid.LlenarGrid(leer.DataSetClase);
                }
            }

        }            
        #endregion Funciones
    }
}
