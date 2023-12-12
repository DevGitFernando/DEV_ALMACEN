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
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using Farmacia.Procesos;

//using Dll_IMach4;
//using Dll_IMach4.Interface; 

namespace Farmacia.Ventas
{
    public partial class FrmValesCanjeados : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, Descripcion = 2, Cantidad = 3
        }

        DllFarmaciaSoft.Ventas.clsImprimirVentas VtasImprimir;
        clsDatosCliente DatosCliente;
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leer2;
        clsLeer leer3;
        clsCodigoEAN EAN = new clsCodigoEAN();

        clsGrid myGrid;
        // Variables Globales  ****************************************************
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado; 
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sMensaje = "";
        bool bContinua = true;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");

       //***************************************************************************

        DllFarmaciaSoft.clsAyudas Ayuda = new DllFarmaciaSoft.clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        DllFarmaciaSoft.clsConsultas Consultas;
        
        public FrmValesCanjeados()
        {
            InitializeComponent();
            con.SetConnectionString();
            leer = new clsLeer(ref con);
            leer2 = new clsLeer(ref con);
            leer3 = new clsLeer(ref con);

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, false);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, ""); 
            VtasImprimir = new DllFarmaciaSoft.Ventas.clsImprimirVentas(General.DatosConexion, DatosCliente, 
                sEmpresa, sEstado, sFarmacia, General.Url, GnFarmacia.RutaReportes, TipoReporteVenta.Contado); 

            myGrid = new clsGrid(ref grdClaves, this); 
            myGrid.EstiloGrid(eModoGrid.SoloLectura); 
            grdClaves.EditModeReplace = true; 
            myGrid.BackColorColsBlk = Color.White;
            myGrid.AjustarAnchoColumnasAutomatico = true;
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
            txtIdPersonal.Text = DtGeneral.IdPersonal;
            lblPersonal.Text = DtGeneral.NombrePersonal;

            //Para obtener Empresam Estado y Farmacia
            sEmpresa = DtGeneral.EmpresaConectada;
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;
        }

        #region Buscar Vale 
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            string sFolioVale = Fg.PonCeros(txtFolio.Text.Trim(), 8);

            if (txtFolio.Text.Trim() != "")
            {
                string sSql = string.Format("Select D.ClaveSSA, D.DescripcionCortaClave, D.Cantidad, E.FolioVale, E.FolioVenta, E.FechaRegistro, E.FechaCanje, E.Status " +
                    " From Ventas_ValesEnc E(NoLock) " +
                    " Inner Join vw_Ventas_ValesDet D(NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioVale = D.Folio ) " +
                    " Where E.IdEmpresa = '{0}' And E.IdEstado = '{1}' And E.IdFarmacia = '{2}' And E.FolioVale = '{3}' ",
                    sEmpresa, sEstado, sFarmacia, sFolioVale);

                myGrid.Limpiar(false);
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "");
                    General.msjError("Ocurrió un error al obtener la información del Vale.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtFolio.Text = leer.Campo("FolioVale");
                        txtFolio.Enabled = false;
                        txtVenta.Text = leer.Campo("FolioVenta");
                        dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");

                        dtpFechaCanje.MinDate = leer.CampoFecha("FechaCanje");
                        dtpFechaCanje.MaxDate = General.FechaSistema;
                        dtpFechaCanje.Value = leer.CampoFecha("FechaCanje");

                        myGrid.LlenarGrid(leer.DataSetClase);
                        IniciarToolBar(true, true, false, false);

                        if (leer.Campo("Status") == "C")
                        {
                            dtpFechaCanje.Enabled = false;
                            IniciarToolBar(true, false, false, false);
                            General.msjUser("El Folio de Vale ingresado esta Cancelado");
                        }
                        else if (leer.Campo("Status") == "R")
                        {
                            dtpFechaCanje.Enabled = false;
                            IniciarToolBar(true, false, false, false);
                            General.msjUser("El Folio de Vale ingresado ya ha sido Registrado");
                        }
                    }
                    else
                    {
                        General.msjUser("El Folio de Vale ingresado no existe. Verifique");
                        btnNuevo_Click(null, null);
                    }
                }
            }
        }
        #endregion Buscar Vale 

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            txtVenta.Enabled = false;
            myGrid.BloqueaColumna(false, 1); 
            myGrid.Limpiar(false); 

            dtpFechaRegistro.Enabled = false;
            dtpFechaRegistro.Value = GnFarmacia.FechaOperacionSistema;
            dtpFechaCanje.MinDate = GnFarmacia.FechaOperacionSistema;
            dtpFechaCanje.Value = GnFarmacia.FechaOperacionSistema;

            txtIdPersonal.Enabled = false; // Debe estar inhabilitado todo el tiempo 
            txtIdPersonal.Text = DtGeneral.IdPersonal;
            lblPersonal.Text = DtGeneral.NombrePersonal;

            IniciarToolBar(true, false, false, false);
            //chkMostrarImpresionEnPantalla.Checked = false;

            txtFolio.Focus();
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bContinua = true;
                
            if (ValidaDatos())
            {
                if (con.Abrir())
                {
                    con.IniciarTransaccion();

                    bContinua = GuardaVenta();

                    if (bContinua)
                    {
                        con.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP 
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        con.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                    }

                    con.Cerrar();
                }
                else
                {
                    Error.LogError(con.MensajeError);
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                }
            }            
            
        }

        private bool GuardaVenta()
        {
            bool bRegresa = true;
            string sFechaCanje = General.FechaYMD(dtpFechaCanje.Value, "-"); ; 
            string sFolioVale = txtFolio.Text.Trim(), sFolioVenta = "";
            string sSql = "";
            int iOpcion = 1;

            if (bContinua)
            {
                if (sFolioVale != "")
                {
                    sSql = string.Format(" Exec spp_Mtto_Ventas_Vales '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', " +
                        "'{6}', '{7}' ",
                        sEmpresa, sEstado, sFarmacia, sFolioVale, sFolioVenta, sFechaCanje, txtIdPersonal.Text, iOpcion);

                    if (leer.Exec(sSql))
                    {
                        if (leer.Leer())
                        {
                            sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                        }
                    }
                    else
                    {
                        bRegresa = false;
                    }
                }
            }

            return bRegresa;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion();
        }

        private void ImprimirInformacion()
        {
            //sFolioVenta = Fg.PonCeros(txtFolio.Text, 8);
            //VtasImprimir.MostrarVistaPrevia = chkMostrarImpresionEnPantalla.Checked;
            //VtasImprimir.MostrarImpresionDetalle = GnFarmacia.ImpresionDetalladaTicket; 
            //if (VtasImprimir.Imprimir(sFolioVenta, fTotal))
            //    btnNuevo_Click(null, null);
        }

        #endregion Botones

        #region Funciones
        private void IniciarToolBar(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Vale inválido, verifique.");
                txtFolio.Focus();
            }

            //if (bRegresa && dtpFechaCanje.Value > General.FechaSistema)
            //{
            //    bRegresa = false;
            //    General.msjUser("La Fecha de Canje no puede ser mayor que la Fecha de Sistema, verifique.");
            //    txtFolio.Focus();
            //}

            return bRegresa;
        }

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.G:
                    if (btnGuardar.Enabled)
                        btnGuardar_Click(null, null);
                    break;

                case Keys.N:
                    if (btnNuevo.Enabled)
                        btnNuevo_Click(null, null);
                    break;

                case Keys.P:
                    if (btnImprimir.Enabled)
                        btnImprimir_Click(null, null);
                    break;

                default:
                    break;
            }
        }

        private void FrmValesCanjeados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                TeclasRapidas(e);

            switch (e.KeyCode)
            {
                default:
                    // base.OnKeyDown(e);
                    break;
            }
        }
        #endregion Funciones           
    }
}
