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

namespace DllFarmaciaSoft.Procesos
{
    public partial class FrmFechaSistema : FrmBaseExt
    {
        //////clsParametrosPtoVta Param = new clsParametrosPtoVta(General.DatosConexion, DtGeneral.DatosApp, 
        //////    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "PFAR");

        clsParametrosPtoVta Param = null; //= new clsParametrosPtoVta(General.DatosConexion, DtGeneral.DatosApp,
            //DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.ArbolModulo);

        string sFecha = "";
        public bool Exito = false;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer; 

        public FrmFechaSistema()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);

            Param= new clsParametrosPtoVta(General.DatosConexion, DtGeneral.DatosApp, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.ArbolModulo);
        }

        private void FrmFechaSistema_Load(object sender, EventArgs e)
        {
            dtpFecha.MinDate = General.FechaSistema;
        }

        public void ValidarFechaSistema()
        {
            ////Param.CargarParametros();
            ////sFecha = Param.GetValor("FechaOperacionSistema");

            try
            {
                sFecha = ObtenerFechaSistema(); 

                dtpFecha.Value = Convert.ToDateTime(sFecha);
                Exito = true;
            }
            catch 
            {
                this.ShowDialog();
            }
        }

        private string ObtenerFechaSistema()
        {
            string sRegresa = "";
            string sSql = string.Format(
                "Select * "+
                "From Net_CFGC_Parametros (NoLock) " + 
                "Where IdEstado = '{0}' and IdFarmacia = '{1}' and NombreParametro = '{2}' and ArbolModulo = '{3}' ",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "FechaOperacionSistema", DtGeneral.ArbolModulo); 

            //if (DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Ninguno)
            //{
            //    Param = new clsParametrosPtoVta(General.DatosConexion, DtGeneral.DatosApp,
            //                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.ArbolModulo);

            //    Param.CargarParametros();
            //    sRegresa = Param.GetValor("FechaOperacionSistema");
            //}
            //else
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "ObtenerFechaSistema()");
                    General.msjError("Error al consultar Fecha de Sistema.");
                }
                else
                {
                    leer.Leer(); 
                    sRegresa = leer.Campo("Valor");
                }
            }

            return sRegresa; 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Exito = false;
            this.Hide();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Param.GrabarParametro(DtGeneral.ArbolModulo, "FechaOperacionSistema", General.FechaYMD(dtpFecha.Value, "-"), "", false, false, 1))
            { 
                Exito = true; 
                this.Hide();
            }
        }
    }
}
