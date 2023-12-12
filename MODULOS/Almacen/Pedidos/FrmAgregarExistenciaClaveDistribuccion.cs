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
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;

namespace Almacen.Pedidos
{
    public partial class FrmAgregarExistenciaClaveDistribuccion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsDatosCliente DatosCliente;

        clsConsultas consulta;
        clsAyudas ayuda;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        string sFormato = "#,###,###,##0";

        public FrmAgregarExistenciaClaveDistribuccion()
        {
            InitializeComponent();

            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            consulta = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
        }

        private void FrmReprocesarExistenciaClaveDistribuccion_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnGenerarExistencia_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                Generar_Existencia();
            }
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            txtClaveSSA.Focus();
        }
        #endregion Funciones

        #region Eventos_ClaveSSA
        private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "";

            if (txtClaveSSA.Text.Trim() != "")
            {                
                sSql = string.Format(" Select IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Presentacion_ClaveSSA, ContenidoPaquete_ClaveSSA, " +
	                                " sum(ExistenciaAux) as Existencia, sum(ExistenciaEnTransito) as ExistenciaEnTransito " +
	                                " From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones " +
                                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and ClaveSSA = '{3}' " +
	                                " Group BY IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Presentacion_ClaveSSA, ContenidoPaquete_ClaveSSA", 
                                    sEmpresa, sEstado, sFarmacia, txtClaveSSA.Text);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtClaveSSA_Validating");
                    General.msjError("Ocurrió un error al obtener los datos de la Clave");
                }
                else
                {
                    if (leer.Leer())
                    {
                        CargarDatosClave();
                    }
                }
                
            }
        }

        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.ClavesSSA_Sales("txtClaveSSA_KeyDown");

                if (leer.Leer())
                {
                    txtClaveSSA.Text = leer.Campo("ClaveSSA");
                    txtClaveSSA_Validating(null, null);
                }
            }
        }

        private void CargarDatosClave()
        {
            txtClaveSSA.Text = leer.Campo("ClaveSSA");
            lblIdClaveSSA.Text = leer.Campo("IdClaveSSA_Sal");
            lblDescripcion.Text = leer.Campo("DescripcionSal");
            lblPresentacion.Text = leer.Campo("Presentacion_ClaveSSA");
            lblContPte.Text = leer.Campo("ContenidoPaquete_ClaveSSA");

            lblExistencia.Text = leer.CampoDouble("Existencia").ToString(sFormato);
            lblExisTransito.Text = leer.CampoDouble("ExistenciaEnTransito").ToString(sFormato);

        }
        #endregion Eventos_ClaveSSA

        #region Generar_Existencia_ClaveSSA
        private void Generar_Existencia()
        {
            string sSql = "";
            bool bContinua = true;

            if (cnn.Abrir())
            {               
                cnn.IniciarTransaccion();

                sSql = string.Format(" Exec spp_ALM_AgregarExistenciaClaveDistribucion '{0}', '{1}', '{2}', '{3}' ",
                                    sEmpresa, sEstado, sFarmacia, txtClaveSSA.Text);

                bContinua = leer.Exec(sSql);

                if (!bContinua)
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "Generar_Existencia");
                    General.msjError("Ocurrió un error al grabar la información.");                   
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("La información se proceso satisfactoriamente.");
                    btnNuevo_Click(null, null);
                }

                cnn.Cerrar();
            }
            else
            {
                Error.LogError(cnn.MensajeError);
                General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
            }

        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtClaveSSA.Text.Trim() == "")
            {
                General.msjAviso("No ha capturado una clave. Verifique!!");
                bRegresa = false;
                txtClaveSSA.Focus();
            }

            return bRegresa;
        }
        #endregion Generar_Existencia_ClaveSSA
    }
}
