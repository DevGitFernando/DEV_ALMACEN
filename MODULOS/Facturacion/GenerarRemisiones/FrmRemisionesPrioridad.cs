using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;


using DllFarmaciaSoft;

namespace Facturacion.GenerarRemisiones
{
    public partial class FrmRemisionesPrioridad : FrmBaseExt
    {
        private enum Cols
        {
            IdFarmacia = 1, Descripcion = 2, Prioridad = 3, Status = 4, StatusAux = 5
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConsultas query; // = new clsConsultas(General.DatosConexion, "Configuracion", "FrmPersonal", Application.ProductVersion, true);
        clsLeer leer;

        clsGrid grid;

        public FrmRemisionesPrioridad()
        {
            InitializeComponent();
        }

        private void FrmRemisionesPrioridad_Load(object sender, EventArgs e)
        {
            leer = new clsLeer(ref cnn);
            grid = new clsGrid(ref grdBaldas, this);
            grdBaldas.EditModeReplace = true;

            query = new clsConsultas(General.DatosConexion, "Facturacion", "FrmRemisionesPrioridad", Application.ProductVersion, true);

            CargarEstados();
            grid.Limpiar(false);
        }


        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFarmacias();
        }


        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(query.ComboEstados("CargarEstados()"), true, "IdEstado", "EstadoNombre");


            cboEstados.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            string sSql = "";

            if (cboEstados.SelectedIndex != 0)
            {

                sSql = string.Format("	Select F.IdFarmacia, NombreFarmacia As Farmacia, IsNull(O.Prioridad, 0) As Prioridad, (case when IsNull(O.Status, 'A') = 'A' then 1 else 0 end) As Status From CatFarmacias F(NoLock) " +
                            "Left Join FACT_CFG__FarmaciasRemisionado O(NoLock) On(F.IdEstado = O.IdEstado And F.IdFarmacia = O.IdFarmacia) " +
                            "Where F.IdEstado = '{0}'", cboEstados.Data);
                //cboSucursales.Add(query.Farmacias(cboEstados.Data, "CargarFarmacias()"), true, "IdFarmacia", "Farmacia");

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CargarFarmacias()");
                    General.msjError("Ocurrió un error al cargar la lista de farmacias.");
                }
                else
                {
                    grid.Limpiar(false);
                    if (leer.Leer())
                    {
                        grid.LlenarGrid(leer.DataSetClase);
                        for (int i = 1; i <= grid.Rows; i++)
                        {
                            grid.BloqueaCelda(true, i, (int)Cols.IdFarmacia);
                            grid.BloqueaCelda(true, i, (int)Cols.Descripcion);
                        }
                    }
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grid.Limpiar(false);
            cboEstados.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bRegresa = true;
            string sSql = "";
            string sStatus = "";


            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                for (int i = 1; i <= grid.Rows; i++)
                {
                    sStatus = "C";
                    if (grid.GetValueBool(i, (int)Cols.Status))
                    {
                        sStatus = "A";
                    }

                    sSql = string.Format(" Exec spp_Mtto_FACT_CFG__FarmaciasRemisionado @IdEstado= '{0}', @IdFarmacia = '{1}', @Prioridad = {2}, @Status = '{3}' ",
                            cboEstados.Data, grid.GetValueInt(i, (int)Cols.IdFarmacia), grid.GetValueInt(i, (int)Cols.Prioridad), sStatus);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }

                if (bRegresa)
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamente.");
                    btnNuevo_Click(this, null);
                }
                else
                {
                    Error.GrabarError(leer, "btnGuardar_Click");
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al guardar la información de Prioridades de remisiones.");
                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso(General.MsjErrorAbrirConexion);
            }
            
        }
    }
}
