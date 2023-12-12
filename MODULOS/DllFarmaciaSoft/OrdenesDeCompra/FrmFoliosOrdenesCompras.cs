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

using DllFarmaciaSoft.Reporteador;

namespace DllFarmaciaSoft.OrdenesDeCompra
{
    public partial class FrmFoliosOrdenesCompras : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        clsConsultas Consultas;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        bool bCargarPantalla = false;
        bool bOpcionExterna = false;
        string sFolio = "";
        bool bCargarOC = false; 

        private enum Cols
        {
            Ninguna = 0,
            Folio = 1, Fecha = 2, Total = 3
        }

        public string Folio
        {
            get { return sFolio; }
        }

        public bool CargarOC
        {
            get { return bCargarOC; } 
        }

        public FrmFoliosOrdenesCompras()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.SeleccionSimple);
            myGrid.AjustarAnchoColumnasAutomatico = true;

            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);

        }

        private void FrmComprasFarmacia_Load(object sender, EventArgs e)
        {
            // btnNuevo_Click(this, null);
            // this.ControlBox = false;  
            if (bOpcionExterna)
            { 
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

        #region Limpiar

        private void IniciarToolBar(bool Nuevo, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnImprimir.Enabled = Imprimir;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ////if (!bOpcionExterna)
            ////{
            ////    IniciarToolBar(true, false);
            ////    Fg.IniciaControles(this, false);
            ////    myGrid.Limpiar(false);
            ////    txtFolio.Enabled = true;
            ////    txtFolio.Focus();
            ////}
            sFolio = "";
            bCargarOC = true; 
            this.Close();
        }

        #endregion Limpiar

        #region Buscar Folio
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);
            string sSql = "";

            if (txtFolio.Text.Trim() != "")
            {
                sSql = string.Format("Select FolioOrdenCompra, Convert( varchar(10), FechaRegistro, 120 ) as FechaRegistro, Total, FolioOrdenCompraReferencia " +
                    " From OrdenesDeComprasEnc (NoLock) " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioOrdenCompraReferencia = '{3}' ",
                    DtGeneral.EmpresaConectada, sEstado, sFarmacia, txtFolio.Text.Trim() );

                myGrid.Limpiar(false);
                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "");
                    General.msjError("Ocurrió un error al obtener la información del Folio.");
                }
                else
                {
                    if (myLeer.Leer())
                    {
                        bCargarPantalla = true; 
                        CargaEncabezadoFolio(); 
                        myGrid.LlenarGrid(myLeer.DataSetClase);

                        IniciarToolBar(true, true); 
                        ////if (!bOpcionExterna)
                        ////{
                        ////    IniciarToolBar(true, true);
                        ////}
                        ////else
                        ////{
                        ////    IniciarToolBar(true, false);
                        ////}
                    }
                    else 
                    {
                        ////IniciarToolBar(true, false); 
                        ////General.msjUser("El Folio ingresado no existe ó no contiene Entradas de Orden de Compras");
                        ////txtFolio.Focus();
                    }
                }
            }
        }

        private void CargaEncabezadoFolio()
        {
            //Se hace de esta manera para la ayuda.
            txtFolio.Enabled = false;
            txtFolio.Text = myLeer.Campo("FolioOrdenCompraReferencia");  //FolioOrdenCompraReferencia 
        }

        #endregion Buscar Folio

        #region Imprimir 
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            int iRenglon = myGrid.ActiveRow;
            string sFolio = "";
            double dImporte = 0.0000;

            sFolio = myGrid.GetValue(iRenglon, (int)Cols.Folio);
            dImporte = myGrid.GetValueDou(iRenglon, (int)Cols.Total);

            if (sFolio.Trim() == "")
            {
                General.msjUser("Seleccione un Folio para imprimir por favor");
            }
            else
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = DtGeneral.RutaReportes;
                
                {
                    myRpt.NombreReporte = "PtoVta_Recepcion_Orden_Compras.rpt";
                } 

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", sFolio);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                if (bRegresa)
                {
                    btnNuevo_Click(null, null);
                }
                else
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }

        }
        #endregion Imprimir

        #region Eventos 
        private void grdProductos_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            sFolio = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Folio);
            // bCargarOC = true; 
            this.Close();
        }
        #endregion Eventos

        #region Funciones
        public void MostrarPantalla(string Folio)
        {            
            txtFolio.Text = Fg.PonCeros(Folio, 8);
            bOpcionExterna = true;
            bCargarPantalla = false; 
            txtFolio_Validating(null, null);

            if (!bCargarPantalla)
            {
                this.Hide(); 
            }
            else 
            { 
                this.ShowDialog();
            }
        }
        #endregion Funciones   


    } // Llaves de la Clase
} // Llaves del NameSpace
