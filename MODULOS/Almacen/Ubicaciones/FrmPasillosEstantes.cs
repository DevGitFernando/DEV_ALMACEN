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
    public partial class FrmPasillosEstantes : FrmBaseExt
    {
        private enum Cols
        {
            IdEstante = 1, Descripcion = 2, Status = 3, StatusAux = 4, Bloqueo = 5, DescripcionAux = 6  
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
        // string sMensaje = "";

        public FrmPasillosEstantes()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            // conexionWeb = new Almacen.wsAlmacen.wsCnnCliente();
            // conexionWeb.Url = General.Url;

            leer = new clsLeer(ref cnn);
            grid = new clsGrid(ref grdEstantes, this);
            grdEstantes.EditModeReplace = true;
            grid.AjustarAnchoColumnasAutomatico = true; 

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
        }

        private void FrmAlmacenPasillosEstantes_Load(object sender, EventArgs e)
        {
            LimpiaPantalla(); 
        }

        #region Botones
 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bRegresa = true;
            string sSql = "";
            string sStatus = "";

            if (ValidaDatos())
            {
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


                        sSql = string.Format(" Exec spp_Mtto_CatPasillos_Estantes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtPasillo.Text,
                            grid.GetValueInt(i, (int)Cols.IdEstante), grid.GetValue(i, (int)Cols.Descripcion), sStatus);

                        ////if (grid.GetValueInt(i, (int)Cols.IdEstante) != 0)
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
                        LimpiaPantalla();
                    }
                    else
                    {
                        Error.GrabarError(leer, "btnGuardar_Click");
                        cnn.DeshacerTransaccion();
                        General.msjError("Error al generar Niveles.");
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }
            }
        }

        #endregion Botones                

        #region Funciones

        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            grid.Limpiar(true);
            txtPasillo.Focus();
        }

        private void CargarPasillos_Estantes()
        {
            string sSql = string.Format(" Select  IdEstante, DescripcionEstante, " +
                " (case when status = 'A' then 1 else 0 end) as Status, " +
                " (case when status = 'A' then 1 else 0 end) as StatusAux, " +
                " 1 as Bloqueo, DescripcionEstante " +
                " From CatPasillos_Estantes (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
                " And IdPasillo = {3} ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtPasillo.Text);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarPasillos()");
                General.msjError("Error al realizar consulta de niveles.");
            }
            else
            {
                grid.Limpiar(true);
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                    for (int i = 1; i <= grid.Rows; i++)
                    {
                        grid.BloqueaCelda(true, i, (int)Cols.IdEstante);
                    }
                }
                
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtPasillo.Text.Trim() == "")
            {
                General.msjAviso("Capturar Rack. Favor de verificar.");
                bRegresa = false;
                txtPasillo.Focus();
            }

            if (bRegresa)
            {
                bRegresa = ValidarCapturaEstantes();
            }

            return bRegresa;
        }

        private bool ValidarCapturaEstantes()
        {
            bool bRegresa = true;

            if (grid.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                if (grid.GetValue(1, (int)Cols.Descripcion) == "")
                {
                    bRegresa = false;
                }
                else
                {
                    for (int i = 1; i <= grid.Rows; i++)
                    {
                        if (grid.GetValue(i, (int)Cols.IdEstante) != "" && grid.GetValue(i, (int)Cols.Descripcion) == "")
                        {
                            bRegresa = false;
                            break;
                        }
                    }                    
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Capturar algun Nivel. Favor de verificar.");
            }

            return bRegresa;

        }

        #endregion Funciones

        #region Eventos
        private void txtPasillo_Validating(object sender, CancelEventArgs e)
        {
            if (txtPasillo.Text.Trim() != "")
            {
                string sSql = string.Format(" Select * From CatPasillos (Nolock) Where IdEmpresa = '{0}' And IdEstado = '{1}' " +
                                            " And IdFarmacia = '{2}' And IdPasillo = {3} ", sEmpresa, sEstado, sFarmacia, txtPasillo.Text  );

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtPasillo_Validating()");
                    General.msjError("Error al realizar consulta de rack.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtPasillo.Text = leer.Campo("IdPasillo");
                        lblPasillo.Text = leer.Campo("DescripcionPasillo");

                        CargarPasillos_Estantes();
                    }
                    else
                    {
                        General.msjAviso("Rack No Encontrado. Favor de verificar.");
                        txtPasillo.Focus();
                    }
                }
            }
            else
            {
                txtPasillo.Focus();
            }

        }

        private void grdPasillos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((grid.ActiveRow == grid.Rows) && e.AdvanceNext)
            {
                if (grid.GetValueInt(grid.ActiveRow, (int)Cols.IdEstante) >= 0 && grid.GetValue(grid.ActiveRow, (int)Cols.Descripcion) != "")
                {
                    grid.Rows = grid.Rows + 1;
                    grid.ActiveRow = grid.Rows;
                    grid.SetValue(grid.Rows, (int)Cols.Status, 1); 
                    grid.SetActiveCell(grid.Rows, (int)Cols.IdEstante);
                }
            }
        }

        private void grdPasillos_EditModeOff(object sender, EventArgs e)
        {
            int iRow = grid.ActiveRow;
            string sIdEstante = grid.GetValueInt(iRow, (int)Cols.IdEstante).ToString();

            if (grid.BuscaRepetido(sIdEstante, iRow, (int)Cols.IdEstante, true))
            {
                General.msjUser("Nivel capturado en otro renglon. Favor de verificar.");
                grid.LimpiarRenglon(iRow);
                grid.SetActiveCell(iRow, (int)Cols.IdEstante);
                grid.EnviarARepetido();
            }
            else
            {
                if (grid.GetValue(iRow, (int)Cols.Descripcion) == "")
                {
                    grid.SetValue(iRow, (int)Cols.Descripcion, "NIVEL #" + sIdEstante);
                }
                else
                {
                    grid.SetActiveCell(grid.Rows, (int)Cols.Status);
                }                
            }
        }

        #endregion Eventos
    }
}
