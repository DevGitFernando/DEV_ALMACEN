using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft; 

namespace Configuracion.ConfigurarPadron 
{
    public partial class FrmPadronBeneficiarios : FrmBaseExt  
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;

        clsConsultas Consulta;

        public FrmPadronBeneficiarios()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            grid = new clsGrid(ref grdPadrones, this);
            grdPadrones.EditModeReplace = true;

            Consulta = new clsConsultas(General.DatosConexion, GnConfiguracion.DatosApp, this.Name); 
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grid.Limpiar(); 
            Fg.IniciaControles();
            cboBasesDeDatos.Focus(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    if (!GuardarPadron())
                    {
                        Error.GrabarError(leer, "GuardarPadron()");
                        cnn.DeshacerTransaccion(); 
                        General.msjError("Ocurrió un error al grabar la información"); 
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjAviso("Información guardada satisfactoriamente.");
                        btnNuevo_Click(null, null); 
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

        private void FrmPadronBeneficiarios_Load(object sender, EventArgs e)
        {
            CargarBasesDeDatos(); 
        }

        #region Procedimientos y Funciones Privados 
        private bool validarDatos()
        {
            bool bRegresa = true;
            string sSql = "";
            string sMsj = "";
            string sPadron = ""; 

            for (int i = 1; i <= grid.Rows; i++)
            {
                sPadron = grid.GetValue(i, 4); 
                sSql = string.Format("Select Name From {0}.dbo.sysobjects So (NoLock) Where So.Name = '{1}' ",
                    cboBasesDeDatos.Data, sPadron);
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    sMsj = string.Format("Ocurrió un error al validad el Padrón [{0}.{1}], verifique.", cboBasesDeDatos.Data, sPadron);
                    General.msjError(sMsj);
                    break;
                }
                else
                {
                    if (!leer.Leer())
                    {
                        bRegresa = false;
                        sMsj = string.Format("El Padrón [{0}.{1}] no existe , verifique.", cboBasesDeDatos.Data, sPadron);
                        General.msjError(sMsj);  
                    }
                }
            }

            return bRegresa;
        }

        private bool GuardarPadron()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_Mtto_CFGS_BD_PADRONES '{0}', '{1}' ", cboBasesDeDatos.Data, chkBD.Checked);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else 
            {
                for (int i = 1; i <= grid.Rows; i++)
                {
                    sSql = string.Format("Exec spp_Mtto_CFGS_PADRON_ESTADOS '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                        Fg.PonCeros(grid.GetValue(i, 1),2), 
                        Fg.PonCeros(grid.GetValue(i, 2), 4), 
                        cboBasesDeDatos.Data, 
                        grid.GetValue(i, 4), 
                        grid.GetValueBool(i, 5), grid.GetValueBool(i, 6)); 
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break; 
                    }
                }
            }

            return bRegresa;
        }

        private void CargarBasesDeDatos()
        {
            string sBdSistemas = " 'master', 'model', 'msdb', 'tempdb' ";
            string sSql = string.Format(" Select Name as DataName, Name " +
                " From sys.databases " +
                " where name not in ( {0} ) order by Name ", sBdSistemas);

            cboBasesDeDatos.Clear();
            cboBasesDeDatos.Add();

            if (leer.Exec(sSql))
            {
                cboBasesDeDatos.Add(leer.DataSetClase, true, "DataName", "Name");
            }

            cboBasesDeDatos.SelectedIndex = 0;
        }

        private void CargarPadrones()
        {
            string sSql = string.Format(" Select IdEstado, IdCliente, Cliente, Padron, StatusAux, EsLocal " +
                " From vw_Padrones (NoLock) " + 
                " Where NombreBD = '{0}' " + 
                " Order By IdEstado, IdCliente ", cboBasesDeDatos.Data);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarPadrones()"); 
            }
            else 
            {
                grid.LlenarGrid(leer.DataSetClase);
            } 
        }
        #endregion Procedimientos y Funciones Privados

        private void cboBasesDeDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid.Limpiar();

            if (cboBasesDeDatos.SelectedIndex != 0)
            {
                CargarPadrones(); 
            }
        }

        private void grdPadrones_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (grid.GetValue(grid.ActiveRow, 1) != "" && grid.GetValue(grid.ActiveRow, 2) != ""
                && grid.GetValue(grid.ActiveRow, 3) != "" && grid.GetValue(grid.ActiveRow, 4) != "")
            {
                grid.Rows = grid.Rows + 1;
                grid.ActiveRow = grid.Rows;
                grid.SetActiveCell(grid.Rows, 1); 
            }
        }

        private void grdPadrones_EditModeOff(object sender, EventArgs e)
        {
            int iActiveRow = grid.ActiveRow; 
            int iActiveCol = grid.ActiveCol;

            if (iActiveCol == 1)
            {
                ValidarEstado(iActiveRow, 1); 
            }

            if (iActiveCol == 2)
            {
                ValidarCliente(iActiveRow, 2); 
            } 
        }

        private void ValidarEstado(int Row, int Col)
        {
            string sValor = grid.GetValue(Row, Col);

            leer.DataSetClase = Consulta.Estados(sValor, "ValidarEstado()");
            if (!leer.Leer())
            {
                grid.SetValue(Row, Col, "");
                General.msjError("El estado capturado no existe, verifique.");
            }
            else
            {
                grid.SetValue(Row, Col, leer.Campo("IdEstado")); 
            }
        }

        private void ValidarCliente(int Row, int Col)
        {
            string sValor = grid.GetValue(Row, Col);

            leer.DataSetClase = Consulta.Clientes(sValor, "ValidarCliente()");
            if (!leer.Leer())
            {
                grid.SetValue(grid.ActiveCol, Col, "");
                General.msjError("El cliente capturado no existe, verifique.");
            }
            else 
            {
                grid.SetValue(Row, Col, leer.Campo("IdCliente"));
                grid.SetValue(Row, Col + 1, leer.Campo("Nombre"));
            }

        }
    }
}
