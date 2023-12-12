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
    public partial class FrmTipoDeCambio : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        public FrmTipoDeCambio()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnConfiguracion.DatosApp, this.Name);

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("Delete From Net_CFGC_TipoCambio  " + 
                " Insert Into Net_CFGC_TipoCambio ( TipoDeCambio, Status, Actualizado ) values ( '{0}', 'A', '0' ) ", nmTipoDeCambio.Value.ToString() );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnAceptar_Click");
                General.msjError("Ocurrió un error al guardar el Tipo de Cambio");
            }
            else
            {
                this.Close();
            }
        }

        private void FrmTipoDeCambio_Load(object sender, EventArgs e)
        {
            // btnAceptar.Enabled = GnConfiguracion.EsOficinaCentral;
            btnAceptar.Enabled = DtGeneral.EsAdministrador; 

            string sSql = "Select * From Net_CFGC_TipoCambio (NoLock) ";
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarTipoDeCambio()");
                General.msjError("Ocurrió un error al obtener el Tipo de Cambio.");
            }
            else
            {
                if ( leer.Leer() )
                    nmTipoDeCambio.Value = leer.CampoDec("TipoDeCambio");
            }
        }
    }
}
