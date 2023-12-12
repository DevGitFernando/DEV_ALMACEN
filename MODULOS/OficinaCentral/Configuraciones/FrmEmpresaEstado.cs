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
    public partial class FrmEmpresaEstado : FrmBaseExt
    {
        private enum cGrid
        {
            IdRel = 1, IdEmpresa = 2, IdEstado = 3, NombreEmp = 4, Activo = 5
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConsultas query;
        clsLeer leer;
        clsGrid Grid;

        //  string sValor = "1";
        // bool bValor = true;
        // int iColManeja = 4;

        public FrmEmpresaEstado()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            Grid = new clsGrid(ref grdEmpresas, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.AjustarAnchoColumnasAutomatico = true;

        }

        private void FrmEmpresaEstado_Load(object sender, EventArgs e)
        {
            CargarEstados();
            btnNuevo_Click(null, null);
        }

        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            try
            {
                cboEstados.Add(query.EstadosConFarmacias(""), true, "IdEstado", "NombreEstado");
            }
            catch { }
            cboEstados.SelectedIndex = 0;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            Grid.Limpiar(false);
            cboEstados.Focus();
        }

        private bool validaDatos()
        {
            bool bRegresa = true;
            return bRegresa;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("Update CatEmpresasEstados Set Status = 'C' Where IdEstado = '{0}'  ", cboEstados.Data);

            if (validaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    for(int i = 1; i<= Grid.Rows; i++)
                    {
                        if (Grid.GetValueBool(i, (int)cGrid.Activo))
                        {
                            sSql += "\n" + string.Format("Exec spp_Mtto_CatEmpresasEstados '{0}', '{1}', '{2}', '1' ",
                                Grid.GetValues(i, (int)cGrid.IdRel), Grid.GetValues(i, (int)cGrid.IdEmpresa), cboEstados.Data);
                            // break;
                        }
                    }

                    if (!leer.Exec(sSql))
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Información guardada satisfactoriamente.");
                        btnNuevo_Click(null, null);
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No fue posible establecer conexión con el servidor, intente de nuevo.");
                }
            }

        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid.Limpiar(false);
            if (cboEstados.SelectedIndex != 0)
            {
                CargarEmpresas();
            }
        }

        private void CargarEmpresas()
        {
            //string sSql = string.Format("Select IdEmpresa, NombreEmpresa, ( case when StatusEmp = 'A' then 1 else 0 end ) as Status " + 
            //    " From vw_EmpresasEstados (NoLock) Where IdEstado = '{0}' ", cboEstados.Data);

            // IsNull(C.IdEstado, '') as IdEstado, 
            Grid.Limpiar(false);

            string sSql = string.Format(" Select IsNull(C.IdEmpresaEdo, '*') as IdEmpresaEdo, " + 
                " S.IdEmpresa, IsNull(C.IdEstado, '') as IdEstado, S.Nombre, " +
                " ( case when IsNull(C.Status, 'C') = 'A' then 1 else 0 end ) as Status   " + 
	            " From CatEmpresas S (NoLock) 	" + 
	            " Left Join CatEmpresasEstados C (NoLock) On ( S.IdEmpresa = C.IdEmpresa and C.IdEstado = '{0}' ) ", cboEstados.Data );
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEmpresas()");
                General.msjError("Ocurrió un error al obtener la lista de empresas.");
            }
            else
            {
                Grid.LlenarGrid(leer.DataSetClase);
            }

            Grid.BloqueaColumna(true, (int)cGrid.IdRel);
            Grid.BloqueaColumna(true, (int)cGrid.IdEmpresa);
            Grid.BloqueaColumna(true, (int)cGrid.IdEstado);
            Grid.BloqueaColumna(true, (int)cGrid.NombreEmp);
        }

        private void grdEmpresas_EditModeOff(object sender, EventArgs e)
        {
        }

        private void grdEmpresas_EditModeOn(object sender, EventArgs e)
        {
            //if (Grid.ActiveCol == (int)cGrid.Activo)
            //{
            //    int iRow = Grid.ActiveRow;
            //    //bValor = Grid.GetValueBool(iRow, iColManeja);

            //    for (int i = 1; i <= Grid.Rows; i++)
            //    {
            //        if ( i != iRow )
            //        Grid.SetValue(i, (int)cGrid.Activo, "0");
            //    }

            //    // Grid.SetValue(iRow, 3, sValor);

            //}
        }
    }
}
