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
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using SC_CompressLib;

namespace DllFarmaciaSoft.SesionDeUsuarios
{
    public partial class FrmCerrarSesionUsuario : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            IdEstado = 1, IdFarmacia, IdPersonal, Personal, Arbol, Id, Llave, MotherBoard, NombreMaquina, Loguin, Refresco, FLogOut, LogOut, ValidarDiasInactivos, Status
        }

        clsGrid grid;
        clsLeer leer;
        clsLeer leer2;
        clsConsultas Consulta;
        clsAyudas Ayuda;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        //clsExportarExcelPlantilla xpExcel;

        clsDatosCliente DatosCliente;

        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;
        string sArbol = DtGeneral.ArbolModulo;

        public FrmCerrarSesionUsuario()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            Consulta = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);

            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);

            grid = new clsGrid(ref grdSesiones, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            grid.AjustarAnchoColumnasAutomatico = true;
        }

        private void FrmCerrarSesionUsuario_Load(object sender, EventArgs e)
        {
            btnNuevo_Click_1(null, null);
            btnCerrarSesion.Enabled = false;
        }


        public void ObtenerSesiones()
        {
            string sSql = "";

            sSql = string.Format(" Select S.IdEstado, S.IdFarmacia, S.IdPersonal, CP.NombreCompleto, S.Arbol, S.Id, S.Llave, S.MotherBoard, S.NombreMaquina, Convert( varchar(19), S.FLogin, 120 ) as FLogin, \n" +
                "Convert( varchar(19), S.FRefresco, 120 ) as FRefresco, Convert( varchar(19), FLogOut, 120 ) as FLogOut, S.LogOut, S.ValidarDiasInactivos, " + "S.Status \n" +
                "From Sesion_ControlDeAcceso S (nolock) \n" +
                "Inner Join vw_Personal CP (NoLock) On(S.IdEstado = CP.IdEstado and S.IdFarmacia = CP.IdFarmacia and S.IdPersonal = CP.IdPersonal) \n" +
                "Where S.IdEstado = '{0}' And S.IdFarmacia = '{1}' and S.Arbol IN('PFAR', 'ALMN') \n", 
                    sEstado, sFarmacia, sPersonal);

            grid.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de las sesiones.");
            }
            else
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                    IniciaBotones(true, true);
                }
                else
                {
                    grid.Limpiar(true);
                }
            }
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            LimpiaPantalla();
            btnCerrarSesion.Enabled = false;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            ObtenerSesiones();
        }

        //private void btnExportarExcel_Click(object sender, EventArgs e)
        //{
        //    ExportarExcel();
        //}

        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            IniciaBotones(true, false);
            grid.Limpiar(true);
        }
        private void IniciaBotones(bool Ejecutar, bool Exportar)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnCerrarSesion.Enabled = Ejecutar;
            //btnExportarExcel.Enabled = Exportar;
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {

            if (grid.GetValue(grid.ActiveRow, Cols.Status) != "A")
            {
                General.msjAviso("No hay sesiones activas");
            }
            else
            {
                if (General.msjConfirmar("¿Desea cerrar la sesion?") == DialogResult.Yes)
                {
                    Sesion.CerradoSesionForzado(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, IdPersonal: grid.GetValue(grid.ActiveRow, Cols.IdPersonal),
                        Arbol: grid.GetValue(grid.ActiveRow, Cols.Arbol), Llave: grid.GetValue(grid.ActiveRow, Cols.Llave), BaseBoard: grid.GetValue(grid.ActiveRow, Cols.MotherBoard),
                        NombreEquipo: grid.GetValue(grid.ActiveRow, Cols.NombreMaquina), Sesion_DiasInactivo: 0);
                    General.msjAviso("La sesion ha sido cerrada");
                    ObtenerSesiones();
                }
            }

        }
    }
}
