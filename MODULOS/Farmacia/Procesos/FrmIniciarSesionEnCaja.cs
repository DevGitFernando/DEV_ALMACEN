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

namespace Farmacia.Procesos
{
    public partial class FrmIniciarSesionEnCaja : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;

        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;
        string sIdPersonal = DtGeneral.IdPersonal;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");

        private bool bIniciarSesion = false;

        public FrmIniciarSesionEnCaja()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

        }

        #region Propiedades 
        public bool AbrirVenta
        {
            get { return bIniciarSesion; }
        }
        #endregion Propiedades

        #region Funciones y Prodecimientos Publicos 
        public void VerificarSesion()
        {
            bIniciarSesion = false;

            // Buscar si existe un Corte registrado
            leer.DataSetClase = query.CortesParciales_Status_InicioSesion(sIdEmpresa, sIdEstado, sIdFarmacia, sFechaSistema, sIdPersonal, false, "VerificarSesion()");
            if (query.Ejecuto)
            {
                if (!leer.Leer())
                {
                    //General.msjUser("El usuario no puede iniciar sesión por que ya tiene registrado Corte parcial."); 
                    this.ShowDialog();
                }
                else
                {
                    // this.ShowDialog();
                    //bIniciarSesion = true; 

                    if (leer.Campo("Status").ToUpper() == "C")
                    {
                        General.msjUser("El usuario no puede iniciar sesión por que ya tiene registrado Corte parcial.");
                    }
                    else
                    {
                        bIniciarSesion = true;
                    }
                }
            }
        }
        #endregion Funciones y Prodecimientos Publicos

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("Set DateFormat YMD  Exec spp_Mtto_CtlCortesParciales '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, sIdPersonal, sFechaSistema, sIdPersonal, txtDotacionInicial.Text.Replace(",", ""));

            //if (validarSesion())
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "btnIniciarSesion_Click");
                    General.msjError("Ocurrió un error al Iniciar Sesión en Caja.");
                }
                else
                {
                    bIniciarSesion = true;
                    this.Hide();
                }
            }
            //else
            //{
            //    bIniciarSesion = false;
            //    this.Hide();
            //} 
        }

        private bool validarSesion()
        {
            bool bRegresa = false;

            string sSql = string.Format(" Select * From CtlCortesParciales (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " + 
                " and IdPersonal = '{3}' and convert(varchar(10), FechaSistema, 120) = '{4}' and Status = 'C' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, sIdPersonal, sFechaSistema );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "validarSesion()");
                General.msjError("Ocurrió un error al Iniciar Sesión en Caja.");
            }
            else
            {
                bRegresa = true; 
                if (leer.Leer())
                {
                    bRegresa = false;
                    General.msjUser("El usuario no puede iniciar sesión por que ya tiene registrado Corte parcial."); 
                }
            } 

            return bRegresa; 
        }

        private void FrmIniciarSesionEnCaja_Load(object sender, EventArgs e)
        {
            dtpFechaOpSistema.Enabled = false;
            dtpFechaOpSistema.Text = sFechaSistema;
            txtDotacionInicial.Text = "0";
        }

        private void txtDotacionInicial_Enter(object sender, EventArgs e)
        {
            txtDotacionInicial.SelectAll();
        }

        private void txtDotacionInicial_MouseEnter(object sender, EventArgs e)
        {
            txtDotacionInicial.SelectAll();
        }

    }
}
