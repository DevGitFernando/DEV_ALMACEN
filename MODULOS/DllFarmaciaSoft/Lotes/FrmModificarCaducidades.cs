using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_ControlsCS;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Errores; 

namespace DllFarmaciaSoft.Lotes
{
    internal partial class FrmModificarCaducidades : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        public bool CaducidadModificada = false;
        public DateTime CaducidadNueva = new DateTime();
        public string CaducidadAnterior = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;
        string sIdSubFarmacia = "";
        string sSubFarmacia = "";
        string sIdProducto = "";
        string sCodigoEAN = "";
        string sClaveLote = "";
        string sCaducidad = "";
        int iAño = 2010;
        int iMes = 1;
        int iDia = 1; 


        //                    sEmpresa, sEstado, sFarmacia, cboSubFarmacias.Data, txtIdProducto.Text.Trim(), lblCodigoEAN.Text.Trim(),
        //                    txtClaveLote.Text.Trim(), sFechaCaducidad, sFechaCaducidad_Anterior, DtGeneral.IdPersonal);

        public FrmModificarCaducidades(string IdSubFarmacia, string SubFarmacia, string IdProducto, string CodigoEAN, string ClaveLote, string CaducidadActual)
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            sIdSubFarmacia = IdSubFarmacia;
            sSubFarmacia = SubFarmacia;
            sIdProducto = IdProducto;
            sCodigoEAN = CodigoEAN;
            sClaveLote = ClaveLote;
            sCaducidad = CaducidadActual;

            Fg.IniciaControles(); 

        }

        private void FrmModificarCaducidades_Load(object sender, EventArgs e)
        {
            iAño = Convert.ToInt32(Fg.Mid(sCaducidad, 1, 4));
            iMes = Convert.ToInt32(Fg.Mid(sCaducidad, 6, 2));
            DateTime dt = new DateTime(iAño, iMes, iDia);

            dtpCaducidadActual.Enabled = false;
            dtpCaducidadActual.Value = dt;
            dtpCaducidadNueva.Value = dt;

            lblIdProducto.Text = sIdProducto;
            lblCodigoEAN.Text = sCodigoEAN;
            lblSubFarmacia.Text = sSubFarmacia; 
            lblClaveLote.Text = sClaveLote; 
        }

        private void btnModificarCaducidad_Click(object sender, EventArgs e)
        {
            if (GrabarCambioCadudicadLote())
            {
                CaducidadModificada = true;
                ////CaducidadNueva = string.Format("{0}-{1}", 
                ////    Fg.PonCeros(dtpCaducidadNueva.Value.Year, 4), Fg.PonCeros(dtpCaducidadNueva.Value.Month, 2));
                CaducidadNueva = dtpCaducidadNueva.Value; 

                this.Hide(); 
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Hide(); 
        }

        private bool GrabarCambioCadudicadLote()
        {
            bool bRegresa = false;
            string sSql = "";
            string sFechaCaducidad = General.FechaYMD(dtpCaducidadNueva.Value); // Fg.FechaYMD(dtpFechaCaducidad.Value.ToShortDateString(), "-");
            string sFechaCaducidad_Anterior = General.FechaYMD(dtpCaducidadActual.Value); // Fg.FechaYMD(dFechaCaducidad_Anterior.ToShortDateString(), "-");

            sSql = String.Format("Set DateFormat YMD Exec spp_Mtto_Adt_FarmaciaProductos_CodigoEAN_Lotes " +
                                "'{0}', '{1}', '{2}', " +
                                "'{3}', '{4}', '{5}', " +
                                "'{6}', '{7}', '{8}', '{9}' ",
                            sEmpresa, sEstado, sFarmacia, sIdSubFarmacia, sIdProducto, sCodigoEAN,
                            sClaveLote, sFechaCaducidad, sFechaCaducidad_Anterior, sPersonal);

            if (!cnn.Abrir())
            {
                Error.GrabarError(cnn.DatosConexion, cnn.Error, "GrabarCambioCadudicadLote()");
                General.msjErrorAlAbrirConexion(); 
            }
            else
            {
                cnn.IniciarTransaccion();

                if (!leer.Exec(sSql))
                {
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al realizar el cambio de caducidad."); 
                }
                else 
                {
                    cnn.CompletarTransaccion();
                    if (leer.Leer()) 
                    {
                        bRegresa = true; 
                    }
                }

                cnn.Cerrar(); 
            }

            return bRegresa;
        } 
    }
}
