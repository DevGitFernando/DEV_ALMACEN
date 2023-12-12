using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem;
using DllFarmaciaSoft;
using Farmacia.Ventas;

using DllTransferenciaSoft;

namespace Farmacia.Vales
{
    public partial class FrmCancelacionVales : FrmBaseExt
    {
        string sEmpresa = "";
        string sEstado = "";
        string sFarmacia = "";
        DllFarmaciaSoft.clsConsultas Consultas;
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        public FrmCancelacionVales(string FolioVale, bool PermiteModificacion)
        {
            InitializeComponent();
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, false);
            leer = new clsLeer(ref con);
            txtFolioCancelacion.Text = "*";
            txtFolioVale.Text = FolioVale;
            txtFolioCancelacion.Enabled = PermiteModificacion;
            txtFolioVale.Enabled = PermiteModificacion;
            IniciarToolBar(PermiteModificacion, true);
        }
        
        private void FrmCancelacionVales_Load(object sender, EventArgs e)
        {
            sEmpresa = DtGeneral.EmpresaConectada;
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            IniciarToolBar(true, true);
            txtFolioVale.Enabled = true;
            txtFolioVale.Text = "";
            txtFolioVale.Focus();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format(" Exec spp_Mtto_Vales_Cancelar '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'",
                    sEmpresa, sEstado, sFarmacia, txtFolioCancelacion.Text.Trim(), txtFolioVale.Text.Trim(), DtGeneral.IdPersonal,
                    General.FechaYMD(General.FechaSistema));

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al guardar la información");
            }
            else
            {
                if (leer.Leer())
                {
                    General.msjAviso(leer.Campo("Mensaje"));
                    this.Hide();
                }
            }
        } 

        private void IniciarToolBar(bool Nuevo, bool Guardar)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
        }

        #endregion Botones


    }
}
