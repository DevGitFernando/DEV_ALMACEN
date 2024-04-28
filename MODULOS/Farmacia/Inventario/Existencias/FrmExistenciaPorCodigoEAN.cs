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
    public partial class FrmExistenciaPorCodigoEAN : FrmBaseExt
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

        // string Codigo = "", Tipo = "0";
        private bool bLimpiar = true;

        clsListView lst;
        string sFormato = "###,###,###,###,##0";

        public FrmExistenciaPorCodigoEAN()
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

            lst = new clsListView(lstLotes);
            //lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = false;
            AnchoColumnas();
        }

        private void FrmExistenciaPorCodigoEAN_Load(object sender, EventArgs e)
        {
            if (bLimpiar)
                btnNuevo_Click(null, null);
        }

        #region Eventos
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            string sIdProducto = "";

            if (txtId.Text.Trim() == "")
            {
                lblDescripcion.Text = "";                
                txtId.Focus();
            }
            else
            {
                sIdProducto = Fg.PonCeros(txtId.Text, 8);
                leer.DataSetClase = query.Productos(sIdProducto, "txtId_Validating");
                if (leer.Leer())
                {
                    txtId.Enabled = false;
                    txtClaveSSA.Enabled = false;
                    txtId.Text = leer.Campo("IdProducto");
                    lblDescripcion.Text = leer.Campo("Descripcion");
                    txtClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblDescripcionClave.Text = leer.Campo("DescripcionSal"); 
                    txtCodEAN.Focus();
                }
            }
        }

        private void txtCodEAN_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "";            

            if (txtCodEAN.Text.Trim() == "")
            {
                //txtCodEAN.Focus();
            }
            else
            {
                if (txtId.Text.Trim() != "")
                {
                    sSql = string.Format(" Select * From vw_ExistenciaPorCodigoEAN (NoLock) " +
                        " Where IdEmpresa = '{0}' And IdEstado = '{1}' and IdFarmacia = '{2}' " +
                        " And IdProducto = '{3}' and CodigoEAN = '{4}' ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                        Fg.PonCeros(txtId.Text, 8), txtCodEAN.Text, 13);
                }
                else
                {
                    sSql = string.Format(" Select * From vw_ExistenciaPorCodigoEAN (NoLock) " +
                        " Where IdEmpresa = '{0}' And IdEstado = '{1}' and IdFarmacia = '{2}' " +
                        " And CodigoEAN = '{3}' ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, 
                        DtGeneral.FarmaciaConectada, txtCodEAN.Text); 
                }

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtCodEAN_Validating");
                    General.msjError("Error al consultar los datos");
                }
                else
                {
                    if (leer.Leer())        
                    {
                        txtId.Enabled = false; 
                        txtId.Text = leer.Campo("IdProducto");
                        lblDescripcion.Text = leer.Campo("DescripcionProducto");
                        txtClaveSSA.Text = leer.Campo("ClaveSSA");
                        lblDescripcionClave.Text = leer.Campo("DescripcionSal"); 


                        txtCodEAN.Enabled = false;
                        txtCodEAN.Text = leer.Campo("CodigoEAN");
                        LlenarGrid();
                    }
                    else
                    {
                        txtCodEAN.Text = "";
                        txtCodEAN.Focus();
                    }
                }
            }
        }
        #endregion Eventos

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            txtId.Enabled = true;
            txtId.Text = "";
            txtCodEAN.Text = "";
            lblDescripcion.Text = "";
            //Grid.Limpiar(false);
            lst.LimpiarItems();

            txtClaveSSA.Enabled = false;

            rdoRptTodos.Focus();
            query.MostrarMsjSiLeerVacio = true;
        }

        #endregion Botones        

        #region Grid 
        private bool validarDatos()
        {
            bool bRegresa = true;

            if (txtClaveSSA.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Clave SSA a consultar, verifique.");
            }

            if (bRegresa && txtId.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Código Interno a consultar, verifique.");
            }

            if (bRegresa && txtCodEAN.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Código EAN a consultar, verifique.");
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
                sSql = string.Format(
                    "Select \n" +
                    "\tSubFarmacia, ClaveLote, Convert(varchar(10), FechaRegistro, 120) as FechaReg, Convert(varchar(10), FechaCaducidad, 120) as FechaCad, \n" +
                    "\tCast((IsNull(Existencia, 0)) as int) as ExistenciaActual, \n" +
                    "\tCast((IsNull(ExistenciaEnTransito, 0)) as int) as ExistenciaTransito, \n" +
                    "\tCast((IsNull(ExistenciaSurtidos, 0)) as int) as ExistenciaSurtidos, \n" +
                    "\tCast((IsNull(ExistenciaAux, 0)) as int) as ExistenciaTotal \n" +
                    "From vw_ExistenciaPorCodigoEAN_Lotes (NoLock) \n" +
                    "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' {3} \n" +
                    "And IdProducto = '{4}' And CodigoEAN = '{5}' and Existencia {6} \n",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sSubFarmacias, 
                    Fg.PonCeros(txtId.Text, 8), txtCodEAN.Text, sCondicion );

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
                        //grdExistencia.Focus();
                        lst.CargarDatos(leer.DataSetClase);
                    }
                    else
                    {
                        General.msjUser("No Existe Información Para Mostrar");
                        btnNuevo_Click(this, null);     
                    }
                }

                itotal = lst.TotalizarColumnaDouble(7);
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
        #endregion Grid

        public void MostrarDetalle( Form Owner, string IdProducto, string CodigoEan, int iTipoRpt, string sCondicionSubFarmacias )
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
            txtId_Validating(null, null);

            txtCodEAN.Text = CodigoEan;
            sSubFarmacias = sCondicionSubFarmacias;
            txtCodEAN_Validating(null, null);

            this.ShowInTaskbar = false;
            this.ShowDialog(Owner);
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Productos("txtId_KeyDown", txtId.Text);
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
                myRpt.Add("@CodigoEAN", txtCodEAN.Text);
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

                if(!bRegresa && !DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{TAB}");
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
        private void FrmExistenciaPorCodigoEAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                CargarSubFarmacias();
            }
        }

        private void AnchoColumnas()
        {
            lst.AnchoColumna(1, 135);
            lst.AnchoColumna(2, 120);
            lst.AnchoColumna(3, 100);
            lst.AnchoColumna(4, 112);
            lst.AnchoColumna(5, 105);
            lst.AnchoColumna(6, 120);
            lst.AnchoColumna(7, 110);
        }
        #endregion Funciones 

        private void toolStripBarraMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

    }
}
