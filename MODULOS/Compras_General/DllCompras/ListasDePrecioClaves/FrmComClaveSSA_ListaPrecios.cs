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

namespace DllCompras.ListasDePrecioClaves
{
    public partial class FrmComClaveSSA_ListaPrecios : FrmBaseExt
    {
        clsConexionSQL myCnn = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsGrid myGrid;
        clsAyudas myAyuda = new clsAyudas();
        clsDatosCliente DatosCliente;
        clsConsultas myQuery;

        // Esta variable se utiliza para saber si la manda llamar el listado de claves ofertadas
        bool bExterna = false;

        public FrmComClaveSSA_ListaPrecios()
        {
            InitializeComponent();

            // Inicializar las Variables Generales 
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

        private void FrmCom_ListaPreciosClaveSSA_Load(object sender, EventArgs e)
        {
            if (!bExterna)
            {
                btnNuevo_Click(null, null);
            }
            else
            {
                btnNuevo.Enabled = false;
            }
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
            LimpiarPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            txtClaveInterna_Validating(null, null);
            CargarDatosClaveSSA();
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

                myRpt.RutaReporte = GnCompras.RutaReportes;
                myRpt.NombreReporte = "COM_OCEN_ListadoProveedores_ClaveSSA";

                myRpt.Add("IdClaveSSA", txtClaveInterna.Text);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Botones

        #region Métodos y Funciones
        private void LimpiarPantalla()
        {
            Fg.IniciaControles(this, true);
            myGrid.Limpiar();
            txtClaveInterna.Focus();
            InicializarToolBar(true, false, false);
        }

        private void HabilitarCampos(bool bTipo)
        {
            txtClaveInterna.Enabled = bTipo;
        }

        private void CargarDatosClaveSSA()
        {
            // string sCadena = "";

            string sSql =
                string.Format(" Select IdProveedor, Nombre, StatusPrecioAux, Precio, " +
                    " Descuento, TasaIva, Iva, PrecioUnitario, " +
                    " Convert( Varchar(10), FechaRegistro, 120), Convert( VarChar(10), FechaFinVigencia, 120)" +
                    " From vw_COM_OCEN_ListaDePrecios_ClavesSSA (NoLock) " +
                    " Where IdClaveSSA = '{0}'" +
                    " Order by Precio Desc, IdProveedor ", Fg.PonCeros(txtClaveInterna.Text, 4));

            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    myGrid.LlenarGrid(myLeer.DataSetClase);
                    btnEjecutar.Enabled = false;
                    btnImprimir.Enabled = true;
                }
                else
                {
                    General.msjUser("No se encontraron proveedores para la Clave.");
                    HabilitarCampos(true);
                    txtClaveInterna.Focus();
                    btnImprimir.Enabled = false;
                }
            }
            else
            {
                Error.GrabarError(myLeer, "CargarDatosProveedores");
                General.msjError("Error al buscar los Proveedores");
            }
        }

        public void MostrarClaveProveedores(string IdClaveSSA)
        {
            txtClaveInterna.Text = IdClaveSSA;
            txtClaveInterna_Validating(null, null);
            CargarDatosClaveSSA();
            bExterna = true;
            this.ShowDialog();
        }

        private void InicializarToolBar(bool bNuevo, bool bEjecutar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnEjecutar.Enabled = bEjecutar;
            btnImprimir.Enabled = bImprimir;
        }
        #endregion Métodos  y Funciones
        
        #region  Eventos
        private void txtClaveInterna_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveInterna.Text != "")
            {
                string sSql = String.Format(" SELECT * FROM dbo.vw_ClavesSSA_Sales ( NOLOCK ) " +
                                            " WHERE ClaveSSA = '{0}'", txtClaveInterna.Text );

                if (myLeer.Exec(sSql))
                {
                    if (myLeer.Leer())
                    {
                        // Se llenan los datos de la clave interna.
                        txtClaveInterna.Text = myLeer.Campo("ClaveSSA");
                        lblClaveSSA.Text = myLeer.Campo("IdClaveSSA_Sal");
                        lblDescripcionClaveSSA.Text = myLeer.Campo("DescripcionSal");
                        HabilitarCampos(false);
                        InicializarToolBar(true, true, false);
                    }
                    else
                    {
                        General.msjUser("Clave interna no encontrada, verifique.");
                        txtClaveInterna.Focus();
                    }
                }
                else
                {
                    Error.GrabarError(myLeer, "txtClaveInterna_Validating");
                    General.msjError("Error al buscar la Clave");
                }
            }
            else
            {
                txtClaveInterna.Focus();
            }
        }       

        private void txtClaveInterna_KeyDown(object sender, KeyEventArgs e)
        {
            clsLeer myLeerClaves = new clsLeer(ref myCnn);

            if (e.KeyCode == Keys.F1)
            {
                myLeerClaves.DataSetClase = myAyuda.ClavesSSA_Sales("txtClaveInterna_KeyDown()");

                if (myLeerClaves.Leer())
                {
                    txtClaveInterna.Text = myLeerClaves.Campo("ClaveSSA");
                    lblClaveSSA.Text = myLeerClaves.Campo("IdClaveSSA_Sal");
                    lblDescripcionClaveSSA.Text = myLeerClaves.Campo("Descripcion");

                    txtClaveInterna.Enabled = false;
                }
            }
        }
        #endregion Eventos                        
    }
}
