using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;

namespace Almacen.ControlDistribucion
{
    public partial class FrmCartaCanje : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeer leer;        
        clsConsultas query;
        clsAyudas ayuda;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmCartaCanje()
        {
            InitializeComponent();

            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmGruposPerfilAtencionDist");

            leer = new clsLeer(ref cnn);            
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);
        }

        private void FrmCartaCanje_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            BuscarDatosCarta();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardaInformacion();

                    if (bContinua)
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("La Información se guardó satisfactoriamente..."); 
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjErrorAlAbrirConexion();
                }
            }
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles();
            IniciaToolBar(true, true);
            txtExpEn.Focus();
        }

        private void IniciaToolBar(bool Ejecutar, bool Guardar)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnGuardar.Enabled = Guardar;
        }

        private void BuscarDatosCarta()
        {
            string sSql = "";

            sSql = string.Format(" Select * From CFGC_Titulos_CartaCanje  Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' ",
                                sEmpresa, sEstado, sFarmacia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "BuscarDatosCarta");
                General.msjError("Ocurrió un error al buscar los datos de la carta..");
            }
            else
            {
                if (leer.Leer())
                {
                    CargarDatos();
                }
            }

        }

        private void CargarDatos()
        {
            IniciaToolBar(true, true);

            txtExpEn.Text = leer.Campo("ExpedidoEn");
            txtAquienCo.Text = leer.Campo("AQuienCorresponda");

            nudMeses.Value = leer.CampoInt("MesesCaducar");

            txtTitulo1.Text = leer.Campo("Titulo_01");
            txtTitulo2.Text = leer.Campo("Titulo_02");
            txtTitulo3.Text = leer.Campo("Titulo_03");

            txtFirma.Text = leer.Campo("Firma");
        }
        #endregion Funciones

        #region Guardar_Informacion
        private bool GuardaInformacion()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_Mtto_CFGC_Titulos_CartaCanje '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                                sEmpresa, sEstado, sFarmacia, txtExpEn.Text, txtAquienCo.Text, nudMeses.Value, txtTitulo1.Text, txtTitulo2.Text,
                                txtTitulo3.Text, txtFirma.Text);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }            

            return bRegresa;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtExpEn.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("Favor de capturar Expendido en... Verifique !!");
                txtExpEn.Focus();                
            }

            if (bRegresa && txtAquienCo.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("Favor de capturar A quien corresponda... Verifique !!");
                txtAquienCo.Focus();
            }

            if (bRegresa && nudMeses.Value == 0)
            {
                bRegresa = false;
                General.msjAviso("Favor de capturar Meses Caducar... Verifique !!");
                nudMeses.Focus();
            }

            if (bRegresa && txtTitulo1.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("Favor de capturar el Titulo 1... Verifique !!");
                txtTitulo1.Focus();
            }

            if (bRegresa && txtTitulo2.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("Favor de capturar el Titulo 2... Verifique !!");
                txtTitulo2.Focus();
            }

            if (bRegresa && txtTitulo3.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("Favor de capturar el Titulo 3... Verifique !!");
                txtTitulo3.Focus();
            }

            if (bRegresa && txtFirma.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("Favor de capturar la Firma... Verifique !!");
                txtFirma.Focus();
            }

            return bRegresa;
        }
        #endregion Guardar_Informacion
    }
}
