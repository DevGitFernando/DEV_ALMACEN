using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Reporteador;

namespace Farmacia.Remisiones
{
    public partial class FrmRemisiones : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsConsultas Consultas;
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmRemisiones()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;
        }

        private void FrmRemisiones_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            dtpFechaDeSistema.Enabled = false;
            dtpFechaRegistro.Enabled = false;

            IniciaToolBar(true, false, false, false);
            txtFolio.Enabled = true;
            txtFolio.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sMensaje = "", sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
            string sFolioVenta = txtFolio.Text.Trim(), sFolioRemision = lblRemision.Text.Trim();
            string sSql = "";

            if (lblRemision.Text.Trim() == "*")
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();
                    sSql = String.Format("Exec spp_Mtto_Remisiones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                        sEmpresa, sEstado, sFarmacia, sFolioVenta, sFolioRemision, sFechaSistema);

                    if (myLeer.Exec(sSql))
                    {
                        if (myLeer.Leer())
                        {
                            sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                        }

                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP 
                        ImprimirRemision(); 
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                }
                else
                {
                    General.msjUser("No hay conexion al Servidor. Intente de nuevo por favor");
                }
            }
            else
            {
                General.msjUser("Ingrese un Folio de Venta valido.");
            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirRemision(); 
        }

        private void ImprimirRemision()
        {
            bool bRegresa = false;
            DatosCliente.Funcion = "btnImprimir_Click()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_Remisiones.rpt";

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("Folio", lblRemision.Text);

            //clsReporteador Reporteador = new clsReporteador(ref myRpt, ref DatosCliente); 
            //Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
            //Reporteador.Url = General.Url;
            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

            ////if (General.ImpresionViaWeb)
            ////{
            ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            ////    DataSet datosC = DatosCliente.DatosCliente();

            ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
            ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
            ////}
            ////else
            ////{
            ////    myRpt.CargarReporte(true);
            ////    bRegresa = !myRpt.ErrorAlGenerar;
            ////}


            if (bRegresa)
            {
                btnNuevo_Click(null, null);
            }
            else
            {
                if(!DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            } 
        }

        #endregion Botones

        #region Buscar Folio 
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            string sSql = String.Format("Select V.Folio as FolioVenta, V.IdCliente, V.NombreCliente, V.IdSubCliente, V.NombreSubCliente, " +
                " Ia.IdBeneficiario, Ia.Beneficiario, V.Total, V.FechaSistema, V.FechaRegistro, V.Status, " + 
                "( Select IsNull( FolioRemision, '' ) From RemisionesEnc(NoLock) " + 
                " Where IdEmpresa = V.IdEmpresa And IdEstado = V.IdEstado And IdFarmacia = V.IdFarmacia And FolioVenta = V.Folio ) as FolioRemision" + 
                " From vw_VentasEnc V (NoLock) " + 
                " Inner Join vw_VentasDispensacion_InformacionAdicional Ia(NoLock) On ( V.IdEmpresa = Ia.IdEmpresa And V.IdEstado = Ia.IdEstado And V.IdFarmacia = Ia.IdFarmacia And V.Folio = Ia.Folio ) " +
                " Where V.IdEmpresa = '{0}' And V.IdEstado = '{1}' And V.IdFarmacia = '{2}' And V.Folio = '{3}' ",
                sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text.Trim(),8));

            if (txtFolio.Text.Trim() != "")
            {
                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "");
                    General.msjError("Ocurrió un error al obtener la información del Folio.");
                }
                else
                {
                    if (myLeer.Leer())
                    {
                        CargaDatos();
                        if (GnFarmacia.SeguroPopular == myLeer.Campo("IdCliente")) 
                        {
                            General.msjUser("El folio de Venta no se puede remisionar, el Cliente no es valido para este proceso.");
                            btnNuevo_Click(null, null); 
                        }
                    }
                    else
                    {
                        General.msjUser("Folio de Venta no encontrado. Verifique que Folio ingresado sea de Credito");
                        txtFolio.Focus();
                    }
                }
                
            }

        }

        private void CargaDatos()
        {
            // IniciaToolBar(true, false, false, false);
            txtFolio.Text = myLeer.Campo("FolioVenta");
            lblCliente.Text = myLeer.Campo("IdCliente");
            lblNombreCliente.Text = myLeer.Campo("NombreCliente");
            lblSubCliente.Text = myLeer.Campo("IdSubCliente");
            lblNombreSubCliente.Text = myLeer.Campo("NombreSubCliente");
            lblBeneficiario.Text = myLeer.Campo("IdBeneficiario");
            lblNombreBeneficiario.Text = myLeer.Campo("Beneficiario");
            lblMonto.Text = myLeer.Campo("Total");
            dtpFechaDeSistema.Value = myLeer.CampoFecha("FechaSistema");
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");
            txtFolio.Enabled = false;

            if (myLeer.Campo("FolioRemision") == "")
            {
                lblRemision.Text = "*";
                IniciaToolBar(true, true, false, false);
            }
            else
            {
                lblRemision.Text = myLeer.Campo("FolioRemision");
                IniciaToolBar(true, false, false, true);
            }

            //Se verifica que el Status de la venta sea activo.
            if (myLeer.Campo("Status") != "A")
            {
                if (myLeer.Campo("FolioRemision") == "")
                {
                    IniciaToolBar(true, false, false, false);
                    General.msjUser("El Folio ingresado cuenta con una Devolucion. No es posible registrar una Remision.");
                }
            }
        }
        #endregion Buscar Folio

        #region Funciones 
        private void IniciaToolBar(bool bNuevo, bool bGuardar, bool bCancelar, bool bImprimir )
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
            btnImprimir.Enabled = bImprimir;
        }
        #endregion Funciones


    }
}
