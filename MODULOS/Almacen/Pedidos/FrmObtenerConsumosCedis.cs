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
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft; 

namespace Almacen.Pedidos
{
    public partial class FrmObtenerConsumosCedis : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsDatosCliente DatosCliente;
        clsListView lst;
        clsExportarExcelPlantilla xpExcel;
        DataSet dtsConsumos;
        bool bExisteTablaDeConsumos = false; 

        public FrmObtenerConsumosCedis()
        {
            InitializeComponent();

            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            lst = new clsListView(lstClaves);
            validarExiste_TablaDeConsumos();
        }

        private void FrmObtenerConsumosCedis_Load(object sender, EventArgs e)
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
            MostrarConsumoCedis();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            if (ProcesarConsumoCedis())
            {
                MostrarConsumoCedis();
            }
        }
        #endregion Botones

        #region Funciones
        private void validarExiste_TablaDeConsumos()
        {
            bExisteTablaDeConsumos = false;
            string sSql = string.Format(" Select * From sysobjects Where Name = 'ALMN_Consumos_Estatales' and xType = 'U' ");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
            }
            else
            {
                bExisteTablaDeConsumos = leer.Leer();
            }
        }

        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            lst.LimpiarItems();
        }

        private void MostrarConsumoCedis()
        {
            int iAño = 0, iMes = 0;
            string sSql = "";

            iAño = General.FechaSistema.Year;
            iMes = General.FechaSistema.Month;

            if (iMes == 1)
            {
                iMes = 12;
            }
            else
            {
                iMes--;
            }

            if (iMes == 12)
            {
                iAño--;
            }

            ////para prueba
            //iAño = 2014;
            //iMes = 2;
            ////

            sSql = string.Format(
                " Select P.ClaveSSA, P.DescripcionSal, P.Presentacion, P.ContenidoPaquete, Cast(C.Piezas_Mes as Int) PiezasMes, " +
                "  Cast(C.Piezas_Semana as Int) PiezasSemana, C.Piezas_Dia " +
		        " From ALMN_Consumos_Estatales C (Nolock) " +
		        " Inner Join vw_ClavesSSA_Sales P (Nolock) On ( C.IdClaveSSA = P.IdClaveSSA_Sal ) " +
                " Where C.IdEmpresa = '{0}' and C.IdEstado = '{1}' and C.IdFarmacia = '{2}'  ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada );

            lst.LimpiarItems();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "MostrarConsumoCedis()");
                General.msjError("Ocurrió un error al obtener los consumos de cedis");
            }
            else
            {
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase, false, false);
                }
                else
                {
                    General.msjAviso("No se encontraron consumos del mes anterior..");
                }
            }

        }

        private bool ProcesarConsumoCedis()
        {
            bool bRegresa = true;
            int iAño = 0, iMes = 0;
            string sSql = "";

            iAño = General.FechaSistema.Year;
            iMes = General.FechaSistema.Month;

            if (iMes == 1)
            {
                iMes = 12;
            }
            else
            {
                iMes--;
            }

            if (iMes == 12)
            {
                iAño--;
            }

            sSql = string.Format(" Exec spp_Procesar_ALMN_Consumos_Estatales @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}' ",
                                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ProcesarConsumoCedis()");
                General.msjError("Ocurrió un error al procesar los consumos de cedis");
                bRegresa = false;
            }            

            return bRegresa;
        }
        #endregion Funciones

        private void btnReSurtido_Click(object sender, EventArgs e)
        {
            FrmRptReSurtidoCedis f = new FrmRptReSurtidoCedis();

            f.ShowDialog();
        }

        private void lstClaves_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
