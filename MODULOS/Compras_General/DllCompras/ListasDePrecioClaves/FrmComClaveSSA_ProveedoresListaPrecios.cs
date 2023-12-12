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

using DllFarmaciaSoft;
using DllCompras;

namespace DllCompras.ListasDePrecioClaves
{
    public partial class FrmComClaveSSA_ProveedoresListaPrecios : FrmBaseExt
    {
        clsConexionSQL myCnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente DatosCliente;
        clsLeer myLeer;
        clsGrid myGrid;
        clsAyudas myAyuda = new clsAyudas();        
        clsConsultas myQuery;

        public FrmComClaveSSA_ProveedoresListaPrecios()
        {
            InitializeComponent();
            myLeer = new clsLeer(ref myCnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DllCompras.GnCompras.DatosApp, this.Name);
            myAyuda = new clsAyudas(General.DatosConexion, GnCompras.Modulo, this.Name, GnCompras.Version);
            myQuery = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "FrmComClaveSSA_ProveedoresListaPrecios()");           

            myGrid = new clsGrid(ref grdListaDePrecios, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.FrozenColumnas = 4;
            myGrid.AjustarAnchoColumnasAutomatico = true; 
        }

        private void FrmComClaveSSA_ProveedoresListaPrecios_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Teclas de Acceso Rápido
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control)
            {
                TeclasRapidas(e);
            }
        }

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.N:
                    if (btnNuevo.Enabled)
                    {
                        btnNuevo_Click(null, null);
                    }
                    break;
                case Keys.E:
                    if (btnEjecutar.Enabled)
                    {
                        btnEjecutar_Click(null, null);
                    }
                    break;
                case Keys.P:
                    if (btnImprimir.Enabled)
                    {
                        btnImprimir_Click(null, null);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion Teclas de Acceso Rápido
       
        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            myGrid.Limpiar(false);
            txtIdProveedor.Focus();
            InicializarToolBar(true, false, false);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarDatosClavesSAL();
            InicializarToolBar(true, true, true);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // int iTipoRpt = 0;
            bool bRegresa = false;

            if (myGrid.Rows == 0)
            {
                General.msjUser("No existe información en pantalla para generar la impresión.");
            }
            else
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnCompras.RutaReportes; // "F:/PROYECTO SC-SOFT/SISTEMA_INTERMED/REPORTES"; 
                myRpt.NombreReporte = "COM_OCEN_ListadoClaves_Proveedor";

                myRpt.Add("IdProveedor", txtIdProveedor.Text);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void InicializarToolBar(bool bNuevo, bool bEjecutar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnEjecutar.Enabled = bEjecutar;
            btnImprimir.Enabled = bImprimir;
        }
        #endregion Botones

        #region Métodos y Funciones
        private void CargarDatosClavesSAL()
        {
            txtIdProveedor_Validating(null, null);

            string sSql = string.Format(" Select ClaveSSA_Base, DescripcionSal, StatusPrecioAux, Precio,  Descuento, TasaIva, Iva, PrecioUnitario, " + 
                " Convert(varchar(10), FechaRegistro, 120), Convert (varchar(10), FechaFinVigencia, 120) From vw_COM_OCEN_ListaDePrecios_ClavesSSA (NoLock) " +
                " Where IdProveedor = '{0}' Order By DescripcionSal ", Fg.PonCeros(txtIdProveedor.Text, 4));

            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    myGrid.LlenarGrid(myLeer.DataSetClase);
                    btnImprimir.Enabled = true;
                }
                else
                {
                    General.msjUser("Clave no encontrada para Proveedor.");
                    HabilitarCampos(true);
                    txtIdProveedor.Focus();
                }
            }
            else
            {
                Error.GrabarError(myLeer, "CargarDatosClavesSAL");
                General.msjError("Error al buscar las ClavesSAL");
            }
        }

        private void HabilitarCampos(bool bTipo)
        {
            txtIdProveedor.Enabled = bTipo;
        }
        #endregion Métodos y Funciones       

        #region Eventos
        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdProveedor.Text != "")
            {
                string sSql = String.Format(" SELECT * FROM dbo.vw_Proveedores ( NOLOCK ) " +
                                            " WHERE IdProveedor = '{0}'", Fg.PonCeros(txtIdProveedor.Text, 4));

                if (myLeer.Exec(sSql))
                {
                    if (myLeer.Leer())
                    {
                        // Se llenan los datos del Proveedor.
                        txtIdProveedor.Text = Fg.PonCeros(myLeer.Campo("IdProveedor"), 4);
                        lblNombreProveedor.Text = myLeer.Campo("Nombre");
                        HabilitarCampos(false);
                        InicializarToolBar(true, true, false);
                    }
                    else
                    {
                        General.msjUser("Clave de Provedor no encontrada, verifique.");
                        txtIdProveedor.Focus();
                    }
                }
                else
                {
                    Error.GrabarError(myLeer, "txtId_Validating");
                    General.msjError("Error al buscar el Proveedor");
                }
            }
            else
            {
                txtIdProveedor.Focus();
            }
        }
       
        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            clsLeer myLeerProveedores = new clsLeer(ref myCnn);

            if (e.KeyCode == Keys.F1)
            {
                myLeerProveedores.DataSetClase = myAyuda.Proveedores("txtIdProveedor_KeyDown");

                if (myLeerProveedores.Leer())
                {
                    txtIdProveedor.Text = myLeerProveedores.Campo("IdProveedor");
                    lblNombreProveedor.Text = myLeerProveedores.Campo("Nombre");
                    txtIdProveedor.Enabled = false;
                    InicializarToolBar(true, true, false);
                }
            }
        }
        #endregion Eventos              
    }
}
