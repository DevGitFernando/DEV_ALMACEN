using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft; 

namespace Configuracion.Configuracion
{
    public partial class FrmConfigUnidadesALMJ : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        // bool bValidado = false;

        public FrmConfigUnidadesALMJ()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn); 
        }

        private void FrmConfigUnidadesALMJ_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();

            //FrameCSGN.Enabled = false;
            //FrameVenta.Enabled = false;

            //tmRevisar.Enabled = true;
            //tmRevisar.Start(); 
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            cboEmpresaCSGN.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format(" Delete From CFGC_AlmacenesJurisdiccionales \n\n" +
                " Insert Into CFGC_AlmacenesJurisdiccionales ( IdEmpresa, IdEstado, IdFarmacia, IdEmpresaCSGN, IdEstadoCSGN, IdFarmaciaCSGN ) " + 
                " Values ( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' )  ", 
                cboEmpresa.Data, cboEstado.Data, cboFarmacia.Data, 
                cboEmpresaCSGN.Data, cboEstadoCSGN.Data, cboFarmaciaCSGN.Data );

            if (validarDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    if (!leer.Exec(sSql))
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Información guardada satisfactoriamente.");
                        LimpiarPantalla(); 
                     }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No fue posible establecer comunicación con el servidor, intente de nuevo.");
                }
            }
        }

        private bool validarDatos()
        {
            bool bRegresa = true;
            return bRegresa; 
        }
        #endregion Botones

        private void tmRevisar_Tick(object sender, EventArgs e)
        {
            tmRevisar.Stop();
            tmRevisar.Enabled = false;
            //if (!DtGeneral.EsEmpresaDeConsignacion)
            //{
            //    General.msjAviso("La Empresa-Farmacia conectada No es de Consignación,\nesta configuración es solo para Empresa-Farmacia de Consignación.");
            //    this.Close();
            //}
            //else 
            //{
            //    FrameCSGN.Enabled = true;
            //    FrameVenta.Enabled = true; 
            //}
        }

    }
}
