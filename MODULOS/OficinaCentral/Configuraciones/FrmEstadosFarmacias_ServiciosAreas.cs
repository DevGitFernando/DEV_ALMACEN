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

using DllFarmaciaSoft;

namespace OficinaCentral.Configuraciones
{
    public partial class FrmEstadosFarmacias_ServiciosAreas : FrmBaseExt
    {
        private enum Cols
        {
            IdArea = 1, Descripcion = 2, Status = 3, StatusAux = 4
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsGrid gridAreas;

        DataSet dtsFarmacias;  // , dtsAreas;

        public FrmEstadosFarmacias_ServiciosAreas()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn); 
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false); 
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            gridAreas = new clsGrid(ref grdAreas, this);
            gridAreas.EstiloGrid(eModoGrid.ModoRow);
            gridAreas.AjustarAnchoColumnasAutomatico = true;
            gridAreas.SetOrder(true);

        }

        #region Botones 
        private void LimpiarPantalla()
        {
            gridAreas.Limpiar(false);

            tmSubClientes.Enabled = false;
            tmSubClientes.Stop();

            btnAsignarProgramas.Enabled = false;
            Fg.IniciaControles();
            cboServicios.Enabled = false;

            cboEstados.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        private void FrmEstadosFarmacias_ClientesProgramas_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
            CargarInformacion();
        }

        #region Obtener Informacion 
        private void CargarInformacion()
        {
            CargarEstados_Farmacias(); 
            CargarServicios();
        }

        private void CargarEstados_Farmacias()
        {
            string sSql = string.Format(
                "Select distinct C.IdEstado, E.Nombre, (C.IdEstado + ' - ' + E.Nombre) as NombreEstado \n" +
                "From CFG_EmpresasFarmacias C (NoLock) \n" +
                "Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) \n" +
                "Order by C.IdEstado \n"); 

            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            if ( leer.Exec(sSql ) )
            {
                cboEstados.Add(leer.DataSetClase, true, "IdEstado", "NombreEstado");
            }
            cboEstados.SelectedIndex = 0;

            sSql = string.Format(" Select distinct C.IdEstado, E.IdFarmacia, " +
                " (E.IdFarmacia + ' -- ' + E.Farmacia) as NombreFarmacia " +
                " From CFG_EmpresasFarmacias C (NoLock) " +
                " Inner Join vw_Farmacias E (NoLock) On ( C.IdEstado = E.IdEstado and C.IdFarmacia = E.IdFarmacia ) " + 
                " Order by C.idEstado, E.IdFarmacia");
            
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            if (leer.Exec(sSql))
            {
                dtsFarmacias = leer.DataSetClase;
            }
            cboFarmacias.SelectedIndex = 0;
        }

        private void CargarAreas()
        {
            string sSql = string.Format(
                "Select distinct E.IdArea, E.Area_Servicio, \n" +
                "\t(case when IsNull(C.Status, '') = '' Then 0 else case when C.Status = 'A' then 1 else 0 end end) as Status, \n" +
                "\t(case when IsNull(C.Status, '') = '' Then 0 else case when C.Status = 'A' then 1 else 0 end end) as StatusAux \n" +
                "From vw_Servicios_Areas E (NoLock) \n" +
                "Left Join CatServicios_Areas_Farmacias C (NoLock) On ( C.IdServicio = E.IdServicio and C.IdArea = E.IdArea and C.IdEstado = '{0}' and C.IdFarmacia = '{1}' ) \n" +
                "Where E.IdServicio = '{2}' \n" +
                "Order by E.IdArea \n", cboEstados.Data, cboFarmacias.Data, cboServicios.Data );

            gridAreas.Limpiar();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarAreas");
                General.msjError("Ocurrió un error al obtener la lista de Areas.");
            }
            else
            {
                gridAreas.LlenarGrid(leer.DataSetClase);
            }
        }

        private void CargarServicios()
        {
            cboServicios.Clear();
            cboServicios.Add("0", "<< Seleccione >>");
            cboServicios.Add(query.Servicios("CargarServicios()"), true, "IdServicio", "NombreServicio");
            cboServicios.SelectedIndex = 0;
            cboServicios.Enabled = false;
        }
        #endregion Obtener Informacion 

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            if (cboEstados.SelectedIndex != 0)
            {
                string sWhere = string.Format(" IdEstado = '{0}' ", cboEstados.Data);
                try
                {
                    cboFarmacias.Add(dtsFarmacias.Tables[0].Select(sWhere), true, "IdFarmacia", "NombreFarmacia");
                }
                catch (Exception ex1)
                {
                    ex1.Source = ex1.Source; 
                }
            }
            cboFarmacias.SelectedIndex = 0;
        }

        private void cboEstados_Validating(object sender, CancelEventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
                cboEstados.Enabled = false;
        }

        private void cboFarmacias_Validating(object sender, CancelEventArgs e)
        {
            cboServicios.Enabled = false;
            if (cboFarmacias.SelectedIndex != 0)
            {
                cboFarmacias.Enabled = false;
                //CargarClientes();
                cboServicios.Enabled = true;
            }
        }

        private void btnAsignarProgramas_Click(object sender, EventArgs e)
        {
            bool bExito = true;
            string sSql = "";
            string sIdArea = "", sStatus = "";

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                for (int i = 1; i <= gridAreas.Rows; i++)
                {
                    sStatus = "A";
                    sIdArea = gridAreas.GetValue(i, (int)Cols.IdArea);
                    if (!gridAreas.GetValueBool(i, (int)Cols.Status))
                    {
                        sStatus = "C";
                    }

                    sSql = string.Format(" Exec spp_Mtto_CatServicios_Areas_Farmacias '{0}', '{1}', '{2}', '{3}', '{4}' ",
                            cboEstados.Data, cboFarmacias.Data, cboServicios.Data, sIdArea, sStatus);
                    // Solo grabar los cambios detectados 
                    if (gridAreas.GetValueBool(i, (int)Cols.Status) != gridAreas.GetValueBool(i, (int)Cols.StatusAux))
                    {
                        if (!leer.Exec(sSql))
                        {
                            bExito = false;
                            break;
                        }
                    }
                }

                if (!bExito)
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnAsignarProgramas_Click");
                    General.msjError("Ocurrió un error al guardar la información.");
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamente.");
                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso("No fue posible establecer conexión con el servidor, intente de nuevo.");
            }
        }

        private void cboServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridAreas.Limpiar();
            btnAsignarProgramas.Enabled = false;
            if (cboServicios.SelectedIndex != 0)
            {
                CargarAreas();
                btnAsignarProgramas.Enabled = true;
            }
        }
    }
}
