﻿using System;
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

namespace DllFarmaciaSoft.OrdenesDeCompra
{
    public partial class FrmVerificarIngresosOrdenDeCompra : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsDatosCliente DatosCliente;

        clsListView lstCon;
        clsListView lstDet;

        DataSet dtsClaves;
        DataSet dtsDetalles;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sOrigen = GnFarmacia.UnidadComprasCentrales;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        bool bExterno = false;

        public FrmVerificarIngresosOrdenDeCompra() 
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            dtsClaves = new DataSet();
            dtsDetalles = new DataSet();

            lstCon = new clsListView(lstClaves);
            lstCon.OrdenarColumnas = true;
            lstCon.PermitirAjusteDeColumnas = true;

            lstDet = new clsListView(lstDetalles);
            lstDet.OrdenarColumnas = true;
            lstDet.PermitirAjusteDeColumnas = true;
        }

        private void FrmVerificarIngresosOrdenDeCompra_Load(object sender, EventArgs e)
        {
            if (!bExterno)
            {
                btnNuevo_Click(null, null);
            }
        }

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);

            IniciaToolBar(false, false);
            lblRecibida.Visible = false;
            lstCon.Limpiar();
            lstDet.Limpiar();

            txtOrden.Focus();
        }

        private void IniciaToolBar(bool Ejecutar, bool Imprimir)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }

        private void CargarDatos()
        {
            string sSql = "";

            dtsClaves = new DataSet();
            dtsDetalles = new DataSet();

            sSql = string.Format(" Exec spp_VerificarDiferenciasOrdenCompra  " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioOrden = '{3}' ", 
                sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtOrden.Text, 8));

            lstCon.Limpiar();
            lstDet.Limpiar();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDatos()");
                General.msjError("Ocurrió un error al cargar los datos de la Orden de Compra.");
            }
            else
            {
                if (leer.Leer())
                {
                    dtsClaves.Tables.Add(leer.Tabla(1).Copy());
                    dtsDetalles.Tables.Add(leer.Tabla(2).Copy());

                    lstCon.CargarDatos(dtsClaves, true, true);
                    lstDet.CargarDatos(dtsDetalles, true, true);
                }
                else
                {
                    General.msjAviso("No se encontraron diferencias de la Orden de Compra.");
                }
            }
        }

        public void Verificar(string Folio)
        {
            bExterno = true;
            txtOrden.Text = Folio;
            CargarDatos();
            btnNuevo.Enabled = false;
            txtOrden.Enabled = false;
            IniciaToolBar(false, true);
            this.ShowDialog();
        }

        #endregion Funciones

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnRecepcionesOC_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirVerificacion();
        }
        #endregion Botones

        #region Eventos_Orden
        private void txtOrden_Validating(object sender, CancelEventArgs e)
        {
            
            if (txtOrden.Text.Trim() != "") 
            {
                leer.DataSetClase = Consultas.OrdenesCompras_Ingresadas(sEmpresa, sEstado, sOrigen, sFarmacia, txtOrden.Text, "txtOrden_Validating");

                if (leer.Leer())
                {
                    txtOrden.Text = leer.Campo("FolioOrdenCompraReferencia");
                    lblIdProveedor.Text = leer.Campo("IdProveedor");
                    lblNombreProveedor.Text = leer.Campo("Proveedor");
                    lblRecibida.Visible = true;
                    lblRecibida.Text = "RECIBIDA";
                    IniciaToolBar(true, true);
                    btnRecepcionesOC_Click(null, null);
                }
                else
                {
                    txtOrden.Focus();
                }
            }
        }
        #endregion Eventos_Orden

        #region Impresion
        private void ImprimirVerificacion()
        {
            bool bRegresa = false;
            DatosCliente.Funcion = "ImprimirVerificacion()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);            

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_RecepcionesOrdenesDeCompra.rpt";

            myRpt.Add("@IdEmpresa", sEmpresa);
            myRpt.Add("@IdEstado", sEstado);
            myRpt.Add("@IdFarmacia", sFarmacia);
            myRpt.Add("@FolioOrden", Fg.PonCeros(txtOrden.Text, 8));

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Impresion
    }
}
