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

namespace Almacen.Ubicaciones
{
    public partial class FrmPasillos : FrmBaseExt
    {
        private enum Cols
        {
            IdPasillo = 1, Descripcion = 2, Status = 3, StatusAux = 4, Bloqueo = 5, DescripcionAux = 6  
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayudas;
        clsGrid grid; 

        clsDatosCliente DatosCliente;
        // wsAlmacen.wsCnnCliente conexionWeb;

        string sEmpresa = DtGeneral.EmpresaConectada;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        //string sMensaje = "";

        public FrmPasillos()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            // conexionWeb = new Almacen.wsAlmacen.wsCnnCliente();
            // conexionWeb.Url = General.Url;

            leer = new clsLeer(ref cnn);
            grid = new clsGrid(ref grdPasillos, this);
            grdPasillos.EditModeReplace = true;
            grid.AjustarAnchoColumnasAutomatico = true;

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
        }

        private void FrmAlmacenPasillos_Load(object sender, EventArgs e)
        {
            CargarPasillos(); 
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            CargarPasillos(); 
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


                    sSql = string.Format(" Exec spp_Mtto_CatPasillos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ", 
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                        grid.GetValueInt(i, (int)Cols.IdPasillo), grid.GetValue(i, (int)Cols.Descripcion), sStatus);

                    ////if ( grid.GetValueInt(i,(int)Cols.IdPasillo) != 0 ) 
                    {
                    ////    if (grid.GetValueBool(i, (int)Cols.Status) != grid.GetValueBool(i, (int)Cols.StatusAux) ||
                    ////        grid.GetValue(i, (int)Cols.Descripcion) != grid.GetValue(i, (int)Cols.DescripcionAux))
                        {
                            if (!leer.Exec(sSql))
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }

                if (bRegresa)
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información generada satisfactoriamente.");
                    CargarPasillos(); 
                }
                else
                {
                    Error.GrabarError(leer, "btnGuardar_Click"); 
                    cnn.DeshacerTransaccion();
                    General.msjError("Error al generar Racks.");
                }

                cnn.Cerrar(); 
            }
            else
            {
                General.msjAviso(General.MsjErrorAbrirConexion); 
            }
        }
        #endregion Botones

        private void CargarPasillos()
        {
            string sSql = string.Format(" Select  IdPasillo, DescripcionPasillo, " + 
                " (case when status = 'A' then 1 else 0 end) as Status, " +
                " (case when status = 'A' then 1 else 0 end) as StatusAux, " +
                " 1 as Bloqueo, DescripcionPasillo " +
                " From CatPasillos (NoLock) " + 
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarPasillos()");
                General.msjError("Error al realizar consulta de racks."); 
            }
            else
            {
                grid.Limpiar(true);

                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);

                    for (int i = 1; i <= grid.Rows; i++)
                    {
                        grid.BloqueaCelda(true, i, (int)Cols.IdPasillo);
                    }
                }
                //else
                //{
                //    grid.AddRow();
                //}
            }
        }

        private void grdPasillos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((grid.ActiveRow == grid.Rows) && e.AdvanceNext)
            {
                if (grid.GetValueInt(grid.ActiveRow, (int)Cols.IdPasillo) >= 0 && grid.GetValue(grid.ActiveRow, (int)Cols.Descripcion) != "")
                {
                    grid.Rows = grid.Rows + 1;
                    grid.ActiveRow = grid.Rows;
                    grid.SetValue(grid.Rows, (int)Cols.Status, 1); 
                    grid.SetActiveCell(grid.Rows, (int)Cols.IdPasillo); 
                }
            }
        }

        private void grdPasillos_EditModeOff(object sender, EventArgs e)
        {
            int iRow = grid.ActiveRow;
            string sIdPasillo = grid.GetValueInt(iRow, (int)Cols.IdPasillo).ToString();

            if (grid.BuscaRepetido(sIdPasillo, iRow, (int)Cols.IdPasillo, true))
            {
                General.msjUser("Rack capturado en otro renglon. Favor de verificar.");
                grid.LimpiarRenglon(iRow);
                grid.SetActiveCell(iRow, (int)Cols.IdPasillo);
                grid.EnviarARepetido();
            }
            else
            {
                if (grid.GetValue(iRow, (int)Cols.Descripcion) == "")
                {
                    grid.SetValue(iRow, (int)Cols.Descripcion, "RACK #" + sIdPasillo);
                }
                else
                {
                    grid.SetActiveCell(grid.Rows, (int)Cols.Status);
                }

            }
        }
    }
}
