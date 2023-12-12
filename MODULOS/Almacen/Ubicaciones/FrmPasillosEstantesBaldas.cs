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
    public partial class FrmPasillosEstantesBaldas : FrmBaseExt
    {
        private enum Cols
        {
            IdEntrepano = 1, Descripcion = 2, Orden = 3, EsDePickeo = 4, 
            Status = 5, StatusAux = 6, Bloqueo = 7, DescripcionAux = 8   
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayudas;
        clsGrid grid; 

        clsDatosCliente DatosCliente;
        // Almacen.wsCnnCliente conexionWeb;

        string sEmpresa = DtGeneral.EmpresaConectada;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        // string sMensaje = "";

        public FrmPasillosEstantesBaldas()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            // conexionWeb = new Almacen.wsAlmacen.wsCnnCliente();
            // conexionWeb.Url = General.Url;

            leer = new clsLeer(ref cnn);
            grid = new clsGrid(ref grdBaldas, this);
            grdBaldas.EditModeReplace = true; 

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
        }

        private void FrmAlmacenPasillosEstantesBaldas_Load(object sender, EventArgs e)
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
            int bEsDePicking = 0; 

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

                        bEsDePicking = grid.GetValueBool(i, (int)Cols.EsDePickeo) ? 1: 0; 


                        sSql = string.Format(" Exec spp_Mtto_CatPasillos_Estantes_Entrepaño '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtPasillo.Text, txtEstante.Text,
                            grid.GetValueInt(i, (int)Cols.IdEntrepano), grid.GetValue(i, (int)Cols.Descripcion), sStatus, bEsDePicking, grid.GetValueInt(i, (int)Cols.Orden));

                        ////if (grid.GetValueInt(i, (int)Cols.IdEntrepano) != 0)
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
                        General.msjError("Error al generar información de Posiciones.");
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

        private void CargarPasillo_Estante_Entrepanos()
        {
            string sSql = string.Format(" Select  IdEntrepaño, DescripcionEntrepaño, IdOrden, EsDePickeo, " + 
                " (case when status = 'A' then 1 else 0 end) as Status, " +
                " (case when status = 'A' then 1 else 0 end) as StatusAux, " +
                " 1 as Bloqueo, DescripcionEntrepaño " +
                " From CatPasillos_Estantes_Entrepaños (NoLock) " + 
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And IdPasillo = '{3}' And IdEstante = '{4}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtPasillo.Text, txtEstante.Text );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarPasillo_Estante_Entrepanos()");
                General.msjError("Error al realizar consulta de Posiciones."); 
            }
            else
            {
                grid.Limpiar(true);
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                    for (int i = 1; i <= grid.Rows; i++)
                    {
                        grid.BloqueaCelda(true, i, (int)Cols.IdEntrepano);
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

            if (bRegresa && txtEstante.Text.Trim() == "")
            {
                General.msjAviso("Capturar Nivel. Favor de verificar.");
                bRegresa = false;
                txtEstante.Focus();
            }

            if (bRegresa)
            {
                bRegresa = ValidarCapturaEntrepaños();
            }

            return bRegresa;
        }

        private bool ValidarCapturaEntrepaños()
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
                        if (grid.GetValue(i, (int)Cols.IdEntrepano) != "" && grid.GetValue(i, (int)Cols.Descripcion) == "")
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Capturar alguna Posición. Favor de verificar.");
            }

            return bRegresa;

        }

        #endregion Funciones

        #region Eventos

        private void grdPasillos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((grid.ActiveRow == grid.Rows) && e.AdvanceNext)
            {
                if (grid.GetValueInt(grid.ActiveRow, (int)Cols.IdEntrepano) >= 0 && grid.GetValue(grid.ActiveRow, (int)Cols.Descripcion) != "")
                {
                    grid.Rows = grid.Rows + 1;
                    grid.ActiveRow = grid.Rows;
                    grid.SetValue(grid.Rows, (int)Cols.Status, 1); 
                    grid.SetActiveCell(grid.Rows, (int)Cols.IdEntrepano);
                }
            }
        }

        private void grdPasillos_EditModeOff(object sender, EventArgs e)
        {
            int iRow = grid.ActiveRow;
            string sIdEntrepano = grid.GetValueInt(iRow, (int)Cols.IdEntrepano).ToString();

            if (grid.BuscaRepetido(sIdEntrepano, iRow, (int)Cols.IdEntrepano, true))
            {
                General.msjUser("Posición capturada en otro renglon. Favor de verificar.");
                grid.LimpiarRenglon(iRow);
                grid.SetActiveCell(iRow, (int)Cols.IdEntrepano);
                grid.EnviarARepetido(); 
            }
            else
            {
                if (grid.GetValue(iRow, (int)Cols.Descripcion) == "")
                {
                    grid.SetValue(iRow, (int)Cols.Descripcion, "POSICION #" + sIdEntrepano);
                }
                else
                {
                    grid.SetActiveCell(grid.Rows, (int)Cols.Status);
                }                
            }
        }

        private void txtPasillo_Validating(object sender, CancelEventArgs e)
        {
            if (txtPasillo.Text.Trim() != "")
            {
                string sSql = string.Format(" Select * From CatPasillos (Nolock) Where IdEmpresa = '{0}' And IdEstado = '{1}' " +
                                            " And IdFarmacia = '{2}' And IdPasillo = {3} ", sEmpresa, sEstado, sFarmacia, txtPasillo.Text);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtPasillo_Validating()");
                    General.msjError("Error al consultar Rack.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtPasillo.Text = leer.Campo("IdPasillo");
                        lblPasillo.Text = leer.Campo("DescripcionPasillo");

                        txtEstante.Focus();
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

        private void txtEstante_Validating(object sender, CancelEventArgs e)
        {
            if (txtPasillo.Text.Trim() != "")
            {
                if (txtEstante.Text.Trim() != "")
                {
                    string sSql = string.Format(" Select * From CatPasillos_Estantes (Nolock) Where IdEmpresa = '{0}' And IdEstado = '{1}' " +
                                                " And IdFarmacia = '{2}' And IdPasillo = {3} And IdEstante = '{4}' ", sEmpresa, sEstado, sFarmacia, 
                                                txtPasillo.Text, txtEstante.Text);

                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "txtEstante_Validating()");
                        General.msjError("Error al consultar Nivel.");
                    }
                    else
                    {
                        if (leer.Leer())
                        {
                            txtEstante.Text = leer.Campo("IdEstante");
                            lblEstante.Text = leer.Campo("DescripcionEstante");

                            CargarPasillo_Estante_Entrepanos();
                        }
                        else
                        {
                            General.msjAviso("Nivel No Encontrado. Favor de verificar.");
                            txtEstante.Focus();
                        }
                    }
                }
            }
            else
            {
                txtEstante.Focus();
            }
        }

        #endregion Eventos      

        
    }
}
