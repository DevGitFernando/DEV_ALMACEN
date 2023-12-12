using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGrid; 
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.SistemaOperativo;

using DllTransferenciaSoft; 
using DllTransferenciaSoft.Zip;


namespace DllTransferenciaSoft.IntegrarBD
{
    public partial class FrmLogIntegracion : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid; 

        public FrmLogIntegracion()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, Transferencia.DatosApp, this.Name);

            grid = new clsGrid(ref grdResultados, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow; 
        }

        private void FrmLogIntegracion_Load(object sender, EventArgs e)
        {
            IniciarPantalla(); 
        }

        #region Botones 
        private void IniciarPantalla()
        {
            Fg.IniciaControles();
            grid.Limpiar();

            rdoIntegrado.Checked = true; 
            txtFiltro.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            IniciarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = "";
            string sFiltroResultado = "";
            string sFiltroFechas = string.Format(" and convert(varchar(10), FechaRegistro, 120) Between '{0}' and '{1}' ",
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value));
           

            if (rdoIntegrado.Checked)
            {
                sFiltroResultado = " and TipoResultado In ( 1 )  "; 
            }

            if (rdoNoIntegrado.Checked)
            {
                sFiltroResultado = " and TipoResultado Not In ( 1 )  ";
            }

            sSql = string.Format("Select  FechaRegistro, NombreBD, \n" +
                " ( \n" +
                " Case When TipoResultado = 1 Then 'Integración correcta' \n" +
                " 	\t When TipoResultado = 2 Then 'Previamente integrada'  \n" +
                " 	\t When TipoResultado = 3 Then 'Error de descompresión' \n" +
                " 	\t When TipoResultado = 4 Then 'Error al integrar' \n" + 
                " \t Else \n" +
                " \t 'NO IDENTIFICADO' \n" +
                " \t End \n" + 
		        " ) as TipoResultado \n" +
                "From CFG_RegistroIntegracionBD (NoLock) \n" + 
                "Where NombreBD Like '%{0}%'  {1}  {2}  \n" + 
                "Order by FechaRegistro Desc ", txtFiltro.Text.Replace(" ", "%"), sFiltroFechas, sFiltroResultado);


            clsGrabarError.LogFileError(sSql); 
            grid.Limpiar(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al obtener la información solicitada.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información con los criterios solicitados.");
                }
                else
                {
                    grid.LlenarGrid(leer.DataSetClase);
                }
            }
        }
        #endregion Botones
    }
}
