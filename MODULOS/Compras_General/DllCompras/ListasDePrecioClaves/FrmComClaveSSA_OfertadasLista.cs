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
using DllCompras.ListasDePrecio;

namespace DllCompras.ListasDePrecioClaves
{
    public partial class FrmComClaveSSA_OfertadasLista : FrmBaseExt
    {
        clsConexionSQL myCnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente DatosCliente;
        clsLeer myLeer;
        clsGrid myGrid;

        public FrmComClaveSSA_OfertadasLista()
        {
            InitializeComponent();
            myLeer = new clsLeer(ref myCnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DllCompras.GnCompras.DatosApp, this.Name);

            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, "FrmComClaveSSA_ProveedoresListaPrecios()");

            myGrid = new clsGrid(ref grdListaDeClaves, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.AjustarAnchoColumnasAutomatico = true; 
        }

        private void FrmCom_ListaClavesOfertadas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
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
            btnImprimir.Enabled = false; 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            LlenaListadoClaves();
        }
        #endregion Botones

        #region Métodos y Funciones
        private void LlenaListadoClaves()
        {

            string sSql =
                string.Format(" Select Distinct IdClaveSSA, ClaveSSA_Base, DescripcionSal " +
                              " From vw_COM_OCEN_ListaDePrecios_ClavesSSA ( Nolock ) " +
                              " Order By DescripcionSal " );

            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    myGrid.LlenarGrid(myLeer.DataSetClase);
                    btnImprimir.Enabled = true;
                }
                else
                {
                    General.msjUser("No se encontraron Claves ofertadas");                    
                }                
            }
            else
            {
                Error.GrabarError(myLeer, "LlenaListadoClaves");
                General.msjError("Error al buscar el Listado De Claves");
            }
        }
        #endregion Métodos y Funciones

        #region Eventos
        private void grdListaDeClaves_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string IdClaveSSA = "";

            IdClaveSSA = myGrid.GetValue(myGrid.ActiveRow, 1);

            FrmComClaveSSA_ListaPrecios Claves = new FrmComClaveSSA_ListaPrecios();

            Claves.MostrarClaveProveedores(IdClaveSSA);

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

                myRpt.RutaReporte = GnCompras.RutaReportes;//"F:/PROYECTO SC-SOFT/SISTEMA_INTERMED/REPORTES";
                myRpt.NombreReporte = "COM_OCEN_ListadoClaveSSA_Ofertadas";

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Eventos       

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
