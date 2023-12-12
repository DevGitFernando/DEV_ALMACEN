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
    public partial class FrmRptReSurtidoCedis : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsDatosCliente DatosCliente;

        public FrmRptReSurtidoCedis()
        {
            InitializeComponent();

            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
        }

        private void FrmRptReSurtidoCedis_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);

            nmMesesCad.Focus();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa = true;
            int iAño = 0, iMes = 0;           

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
            
            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            

            myRpt.RutaReporte = DtGeneral.RutaReportes;

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
            //myRpt.Add("@Año", iAño);
            //myRpt.Add("@Mes", iMes);
            myRpt.Add("@MesesParaCaducar", nmMesesCad.Value);
            myRpt.Add("@IdPersonal", DtGeneral.IdPersonal);
            myRpt.NombreReporte = "PtoVta_Resurtir_Ubicacion_CEDIS";

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
            else
            {
                this.Close();
            }
        }
        #endregion Botones
    }
}
