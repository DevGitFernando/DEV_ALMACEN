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
    public partial class FrmEstadosFarmacias_MovtosInv : FrmBaseExt
    {
        private enum Cols
        {
            IdTipoMovto = 1, Descripcion = 2, Efecto = 3, Status = 4, StatusAux = 5
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsGrid gridAreas;

        DataSet dtsFarmacias;  // , dtsAreas;

        public FrmEstadosFarmacias_MovtosInv()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn); 
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, false); 
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            gridAreas = new clsGrid(ref grdAreas, this);
            gridAreas.EstiloGrid(eModoGrid.ModoRow);
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            gridAreas.Limpiar(false); 
            Fg.IniciaControles();

            cboEstados.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bExito = true;
            string sSql = "";
            string sIdMovto = "", sStatus = "";

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                for (int i = 1; i <= gridAreas.Rows; i++)
                {
                    sStatus = "A";
                    sIdMovto = gridAreas.GetValue(i, (int)Cols.IdTipoMovto);
                    if (!gridAreas.GetValueBool(i, (int)Cols.Status))
                    {
                        sStatus = "C";
                    }

                    sSql = string.Format(" Update Movtos_Inv_Tipos_Farmacia Set Status = '{3}', Actualizado = 0 " +
                        " where IdEstado = '{0}' and IdFarmacia = '{1}' and IdTipoMovto_Inv = '{2}' ",
                            cboEstados.Data, cboFarmacias.Data, sIdMovto, sStatus);
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
                    Error.GrabarError(leer, "btnGuardar_Click");
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
                "Select distinct C.IdEstado, E.Nombre, ( C.IdEstado + ' - ' + E.Nombre ) as NombreEstado " +
                "From CFG_EmpresasFarmacias C (NoLock) " +
                "Inner Join CatEstados E (NoLock) On ( C.IdEstado = E.IdEstado ) " + 
                "Order by C.IdEstado "); 

            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            if ( leer.Exec(sSql ) )
            {
                cboEstados.Add(leer.DataSetClase, true, "IdEstado", "NombreEstado");
            }
            cboEstados.SelectedIndex = 0;

            sSql = string.Format(
                "Select distinct C.IdEstado, E.IdFarmacia, E.Farmacia, \n" +
                "\t(E.IdFarmacia + ' -- ' + E.Farmacia) as NombreFarmacia \n" +
                "From CFG_EmpresasFarmacias C (NoLock) \n" +
                "Inner Join vw_Farmacias E (NoLock) On ( C.IdEstado = E.IdEstado and C.IdFarmacia = E.IdFarmacia ) \n"+ 
                "Order by C.IdEstado, E.IdFarmacia ");
            
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            if (leer.Exec(sSql))
            {
                dtsFarmacias = leer.DataSetClase;
            }
            cboFarmacias.SelectedIndex = 0;
        }

        private void CargarMovimientosAsignados()
        {
            string sSql = string.Format(" Select E.TipoMovto, E.DescMovimiento, E.Efecto, " + 
                "   (case when IsNull(D.Status, '') = '' Then 0 else case when D.Status = 'A' then 1 else 0 end end) as Status, " + 
                "   (case when IsNull(D.Status, '') = '' Then 0 else case when D.Status = 'A' then 1 else 0 end end) as StatusAux " + 
                " From vw_MovtosInv_Tipos  E " + 
                " Left Join Movtos_Inv_Tipos_Farmacia D (NoLock) " + 
                "   On ( E.TipoMovto = D.IdTipoMovto_Inv and D.IdEstado = '{0}' and D.IdFarmacia = '{1}' ) " +
                " Where E.EsMovtoGral = 1 ", cboEstados.Data, cboFarmacias.Data );


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
            ////cboServicios.Clear();
            ////cboServicios.Add("0", "<< Seleccione >>");
            ////cboServicios.Add(query.Servicios("CargarServicios()"), true, "IdServicio", "NombreServicio");
            ////cboServicios.SelectedIndex = 0;
            ////cboServicios.Enabled = false;
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
            // cboServicios.Enabled = false;
            if (cboFarmacias.SelectedIndex != 0)
            {
                cboFarmacias.Enabled = false;
                //CargarClientes();
                // cboServicios.Enabled = true;
                CargarMovimientosAsignados(); 
            }
        }

        private void btnAsignarProgramas_Click(object sender, EventArgs e)
        {
        }

        private void cboServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////gridAreas.Limpiar();
            ////btnAsignarProgramas.Enabled = false;
            ////if (cboServicios.SelectedIndex != 0)
            ////{
            ////    CargarAreas();
            ////    btnAsignarProgramas.Enabled = true;
            ////}
        } 
    }
}
