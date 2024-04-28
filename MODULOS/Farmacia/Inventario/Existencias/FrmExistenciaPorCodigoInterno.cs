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
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;

namespace Farmacia.Inventario
{
    public partial class FrmExistenciaPorCodigoInterno : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        //clsGrid Grid;
        clsConsultas query;
        clsAyudas ayuda;
        FrmListaDeSubFarmacias SubFarmacias;
        string sSubFarmacias = "";
        bool bOpcionExterna = false;

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        FrmExistenciaPorCodigoEAN Ean;

        // private string Codigo = "";
        // private string Tipo = "";
        private bool bLimpiar = true;

        clsListView lst;
        string sFormato = "###,###,###,###,##0";

        public FrmExistenciaPorCodigoInterno()
        {
            InitializeComponent();

            cnn.SetConnectionString();
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            //Grid = new clsGrid(ref grdExistencia, this);
            //Grid.EstiloGrid(eModoGrid.SeleccionSimple);
            //Grid.Limpiar(false);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            lst = new clsListView(lstEAN);
            //lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = false;
            AnchoColumnas();

        }

        private void FrmExistenciaPorCodigoInterno_Load(object sender, EventArgs e)
        {
            if ( bLimpiar ) 
                btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)     
        {
            query.MostrarMsjSiLeerVacio = false;
            Fg.IniciaControles(this, true);
            //grdExistencia.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            //Grid.Limpiar(false);
            lst.LimpiarItems();

            txtClaveSSA.Enabled = false;

            rdoRptTodos.Focus();
            query.MostrarMsjSiLeerVacio = true;
        }
        #endregion Botones

        #region Eventos
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            // string sSql = "";

            if (txtId.Text.Trim() == "")
            {
                txtId.Focus();
            }
            else
            {
                //Grid.Limpiar(false);

                lst.LimpiarItems();
                leer.DataSetClase = query.Productos(txtId.Text, "txtId_Validating");
                {
                    if (leer.Leer())
                    {
                        txtId.Enabled = false;
                        txtClaveSSA.Enabled = false;
                        txtId.Text = leer.Campo("IdProducto");
                        lblDescripcion.Text = leer.Campo("Descripcion");
                        txtClaveSSA.Text = leer.Campo("ClaveSSA");
                        lblDescripcionClave.Text = leer.Campo("DescripcionSal"); 
                        LlenarGrid();
                    }
                    else
                    {
                        txtId.Text = "";
                        txtId.Focus();
                    }
                }
            }
        }
        #endregion Eventos

        #region Grid 
        private bool validarDatos()
        {
            bool bRegresa = true;

            if (txtClaveSSA.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Clave SSA a consultar, verifique."); 
            }

            if (txtId.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Código Interno a consultar, verifique."); 
            }

            return bRegresa;
        }

        private void LlenarGrid()
        {
            string sCondicion = "";
            double itotal = 0;

            if (rdoRptTodos.Checked)
                sCondicion = ">= 0";

            if (rdoRptConExist.Checked)
                sCondicion = "> 0";

            if (rdoRptSinExist.Checked)
                sCondicion = "= 0";

            if (validarDatos())
            {
                string sSql = "";
                //sSql = string.Format(" SELECT CodigoEAN, Existencia FROM vw_ExistenciaPorCodigoEAN (nolock) " +
                //    " WHERE IdEmpresa = '{0}' and IdEstado = '{1}' AND IdFarmacia = '{2}' AND IdProducto = '{3}' " + 
                //    " AND Existencia {4}",
                //    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                //    Fg.PonCeros(txtId.Text, 8), sCondicion );

                sSql = string.Format(
                    "Select \n" +
                    "\tIsNull(v.CodigoEAN, '') as CodigoEAN, Cast((sum(IsNull(V.Existencia, 0))) as int) as ExistenciaActual, \n" +
                    "\tCast((sum(IsNull(V.ExistenciaEnTransito, 0))) as int) as ExistenciaTransito, \n" +
                    "\tCast((sum(IsNull(V.ExistenciaSurtidos, 0))) as int) as ExistenciaSurtidos, \n" +
                    "\tCast((sum(IsNull(V.ExistenciaAux, 0))) as int) as ExistenciaTotal \n" +
                    "From vw_Productos_CodigoEAN vwP (NoLock) \n" +
                    "Left Join vw_ExistenciaPorCodigoEAN_Lotes v (NoLock) On ( vwP.IdProducto = v.IdProducto and vwP.CodigoEAN = v.CodigoEAN ) \n" +
                    "Where v.IdEmpresa = '{0}' And v.IdEstado = '{1}' And v.IdFarmacia = '{2}' {3} And v.IdProducto = '{4}' \n" +
                    "And Existencia {5} \n" +
                    "Group by IsNull(v.CodigoEAN, '') \n",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sSubFarmacias, 
                    Fg.PonCeros(txtId.Text, 8), sCondicion);

                //Grid.Limpiar(false);
                lst.LimpiarItems();
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "LlenarGrid");
                    General.msjError("Error al consultar los datos");
                }
                else
                {
                    if (leer.Leer())
                    {
                        //Grid.LlenarGrid(leer.DataSetClase);
                        lst.CargarDatos(leer.DataSetClase);
                    }
                    else
                    {
                        General.msjUser("No Existe Información Para Mostrar");
                        btnNuevo_Click(this, null);
                    }
                }

                itotal = lst.TotalizarColumnaDouble(5);
                lblTotal.Text = itotal.ToString(sFormato);
            }

            if(!bLimpiar)
            {
                rdoRptConExist.Enabled = false;
                rdoRptSinExist.Enabled = false;
                rdoRptTodos.Enabled = false;
            }

            AnchoColumnas();
        }

        ////private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        ////{
        ////    string sCodigoEAN = Grid.GetValue(e.Row + 1, 1);
        ////    int iTipoRpt = 0;

        ////    if (rdoRptTodos.Checked)
        ////        iTipoRpt = 0;
        ////    if (rdoRptConExist.Checked)
        ////        iTipoRpt = 1;
        ////    if (rdoRptSinExist.Checked)
        ////        iTipoRpt = 2;

        ////    if (sCodigoEAN != "")
        ////    {
        ////        Ean = new FrmExistenciaPorCodigoEAN();
        ////        Ean.MostrarDetalle(txtId.Text, sCodigoEAN, iTipoRpt, sSubFarmacias);
        ////    }
        ////}

        #endregion Grid

        public void MostrarDetalle( Form Owner, string IdProducto, int iTipoRpt, string sCondicionSubFarmacias )
        {
            if (iTipoRpt == 0)
                rdoRptTodos.Checked = true;
            if (iTipoRpt == 1)
                rdoRptConExist.Checked = true;      
            if (iTipoRpt == 2)
                rdoRptSinExist.Checked = true;

            bOpcionExterna = true;
            bLimpiar = false;
            btnNuevo.Enabled = false;
            txtId.Text = IdProducto;
            sSubFarmacias = sCondicionSubFarmacias;
            txtId_Validating(null, null);

            this.ShowInTaskbar = false; 
            this.ShowDialog(Owner);
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Productos("txtId_KeyDown");
                if (leer.Leer())
                {
                    txtId.Text = leer.Campo("IdProducto");
                    txtId_Validating(null, null);
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            int iTipoRpt = 0;
            bool bRegresa = false;

            if (lst.Registros == 0)
            {
                General.msjUser("No hay información en pantalla para generar la impresión.");
            }
            else
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_ExistenciasSalesDetallado";

                if (rdoRptTodos.Checked)
                {
                    iTipoRpt = 0;
                }

                if (rdoRptConExist.Checked)
                {
                    iTipoRpt = 1;
                }

                if (rdoRptSinExist.Checked)
                {
                    iTipoRpt = 2;
                }

                myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("@IdClaveSSA_Sal", txtClaveSSA.Text);
                myRpt.Add("@IdProducto", txtId.Text);
                myRpt.Add("@CodigoEAN", "");
                myRpt.Add("@SubFarmacias", sSubFarmacias);
                myRpt.Add("@TipoRpt", iTipoRpt);

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

                if (!bRegresa && !DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            LlenarGrid();
        }

        #region Funciones
        private void CargarSubFarmacias()
        {
            if (!bOpcionExterna)
            {
                SubFarmacias = new FrmListaDeSubFarmacias(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
                SubFarmacias.MostrarDetalle();
                sSubFarmacias = SubFarmacias.CondicionSubFarmacias;
            }
            else
            {
                General.msjUser("Ya ha seleccionado las SubFarmacias");
            }
        }

        private void FrmExistenciaPorCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                CargarSubFarmacias();
            }
        }

        private void AnchoColumnas()
        {
            lst.AnchoColumna(1, 110);
            lst.AnchoColumna(2, 105);
            lst.AnchoColumna(3, 120);
            lst.AnchoColumna(4, 110);            
        }
        #endregion Funciones 

        private void lstEAN_DoubleClick(object sender, EventArgs e)
        {
            string sCodigoEAN = lst.LeerItem().Campo("CodigoEAN");
            int iTipoRpt = 0;

            if (rdoRptTodos.Checked)
                iTipoRpt = 0;
            if (rdoRptConExist.Checked)
                iTipoRpt = 1;
            if (rdoRptSinExist.Checked)
                iTipoRpt = 2;

            if (sCodigoEAN != "")
            {
                Ean = new FrmExistenciaPorCodigoEAN();
                Ean.MostrarDetalle(this, txtId.Text, sCodigoEAN, iTipoRpt, sSubFarmacias);
            }
        }

    }
}
