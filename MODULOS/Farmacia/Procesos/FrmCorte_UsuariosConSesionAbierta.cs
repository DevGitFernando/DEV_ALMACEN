using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGrid;
using DllFarmaciaSoft;

namespace Farmacia.Procesos
{
    public partial class FrmCorte_UsuariosConSesionAbierta : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsConsultas Consultas;
        clsGrid Grid;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
        string sIdPersonal = DtGeneral.IdPersonal;
        bool bSesionesDeCorteParcialCerradas = false; 

        // Manejo de reportes  
        clsDatosCliente DatosCliente; 
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        public FrmCorte_UsuariosConSesionAbierta()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);
            
            Grid = new clsGrid(ref grdUsuarios, this);
            Grid.EstiloGrid(eModoGrid.SeleccionSimple);
            Grid.AjustarAnchoColumnasAutomatico = true;
        }

        private void FrmCorte_UsuariosConSesionAbierta_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            LlenaGridUsuarios(false);
        }

        #region Propiedades 
        public bool SesionesCerradas
        {
            get { return bSesionesDeCorteParcialCerradas; }
        }
        #endregion Propiedades

        #region Grid
        private void LlenaGridUsuarios(bool Cerrar) 
        {

            string sSql = string.Format("Select C.IdPersonal, P.NombreCompleto, P.LoginUser " +
                " From CtlCortesParciales C(NoLock) " +
                " Inner Join vw_Personal P (NoLock) On ( C.IdEstado = P.IdEstado And C.IdFarmacia = P.IdFarmacia And C.IdPersonal = P.IdPersonal ) " +
                " Where C.IdEmpresa = '{0}' And C.IdEstado = '{1}' And C.IdFarmacia = '{2}' " +
                " And Convert( varchar(10), C.FechaSistema, 120 ) = '{3}' " + 
                " And C.Status = 'A' ", sEmpresa, sEstado, sFarmacia, sFechaSistema );

            bSesionesDeCorteParcialCerradas = false; 
            Grid.Limpiar(false);
            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "");
                General.msjError("Ocurrió un error al obtener la información del Personal.");
            }
            else
            {
                if (myLeer.Leer())
                {
                    Grid.LlenarGrid(myLeer.DataSetClase);
                }
                else
                {
                    if (Cerrar)
                    {
                        bSesionesDeCorteParcialCerradas = true; 
                        this.Close(); 
                    }
                    else
                    {
                        General.msjUser("No existe información para mostrar."); 
                    }
                }
            }
        }

        private void grdUsuarios_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sIdPersonal = Grid.GetValue(Grid.ActiveRow, 1) ;

            if (sIdPersonal != "")
            {
                FrmCorteParcial f = new FrmCorteParcial();
                f.GenerarCorteParcial_Externo(sIdPersonal);

                if (f.CorteRealizado)
                {
                    LlenaGridUsuarios(true); 
                }
            }
        }
        #endregion Grid 
    }
}
