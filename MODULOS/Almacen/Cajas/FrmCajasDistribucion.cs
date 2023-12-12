using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft; 


namespace Almacen.Cajas
{
    public partial class FrmCajasDistribucion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leerGuardar;

        clsConsultas Consultas;
        clsAyudas Ayudas;

        clsListView lst;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmCajasDistribucion()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerGuardar = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
        }

        private void FrmCajasDistribucion_Load(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            txtIdCaja.Focus();
        }

        private bool ValidarCajas()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Select * From CatCajasDistribucion (Nolock) " +
                                 " Where IdEstado = '{0}' and IdFarmacia = '{1}' and Convert(int, IdCaja) = {2} ",
                                 sEstado, sFarmacia, txtIdCaja.Text);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidarCajas()");
                General.msjError("Ocurrió un error al validar la caja..");
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {                    
                    if (!leer.CampoBool("Habilitada"))
                    {
                        //General.msjAviso("La caja no esta Habilitada");
                        lblStatusCaja.Text = "La Caja : " + txtIdCaja.Text + "  no esta Habilitada ";
                        bRegresa = false;
                    }
                }
                else
                {
                    //General.msjAviso("El numero de caja no esta asignado a la unidad. Verifique..!!");
                    lblStatusCaja.Text = "La Caja : " + txtIdCaja.Text + "  no esta asignado a la unidad. Verifique..!! ";
                    bRegresa = false;
                }
            }

            return bRegresa;
        }
        #endregion Funciones

        #region Eventos
        private void txtIdCaja_Validating(object sender, CancelEventArgs e)
        {            
            if(txtIdCaja.Text.Trim() != "" )
            {
                if (ValidarCajas())
                {
                    if (ActualizaInformacion())
                    {
                        lblStatusCaja.Text = "Caja : " + txtIdCaja.Text + "  Disponible ";
                        txtIdCaja.Text = "";
                        txtIdCaja.Focus();                        
                    }
                }
            }

            txtIdCaja.Focus();
        }
        #endregion Eventos

        #region Actualiza_Información
        private bool ActualizaInformacion()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Update CatCajasDistribucion Set Disponible = 1, Habilitada = 1 " +
	                             " Where IdEstado = '{0}' and IdFarmacia = '{1}' and Convert(int, IdCaja) = {2}  ", sEstado, sFarmacia, txtIdCaja.Text);

            if (!leerGuardar.Exec(sSql))
            {
                Error.GrabarError(leerGuardar, "");
                General.msjError("Ocurrió un error al actualizar la información..");
                bRegresa = false;
            }            

            return bRegresa;
        }
        #endregion Actualiza_Información
    }
}
