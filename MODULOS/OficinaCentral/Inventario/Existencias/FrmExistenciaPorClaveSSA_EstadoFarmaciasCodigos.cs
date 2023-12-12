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

// Implementacion de hilos 
using System.Threading;

using DllFarmaciaSoft;

namespace OficinaCentral.Inventario
{
    public partial class FrmExistenciaPorClaveSSA_EstadoFarmaciasCodigos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        clsConsultas query;
        clsAyudas ayuda;
        DataSet dtsFarmacias;


        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();

        //FrmExistenciaPorCodigoInterno Codigo;
        private bool bLimpiar = true;

        string sFormato = "#,###,###,##0";
        Thread _workerThread;

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        public FrmExistenciaPorClaveSSA_EstadoFarmaciasCodigos()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            Grid = new clsGrid(ref grdExistencia, this);
            Grid.EstiloGrid(eModoGrid.SeleccionSimple);
            Grid.Limpiar(false);

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;

            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(query.EstadosConFarmacias("FrmExistenciaPorClaveSSA_EstadoFarmacias"), true, "IdEstado", "Estado");
            cboEstados.SelectedIndex = 0;

            CargarFarmacias();
        }

        #region Eventos 
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            // string sSql = "";

            if (txtId.Text.Trim() == "")
            {
                //General.msjError("Favor de Capturar la Clave Sal");
                txtId.Focus();
            }
            else
            {
                leer.DataSetClase = query.ClavesSSA_Sales(txtId.Text, true, "txtId_Validating");
                if (leer.Leer())
                {
                    txtId.Enabled = false;
                    txtId.Text = leer.Campo("ClaveSSA");
                    lblClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblDes.Text = leer.Campo("Descripcion");
                    LlenarGrid();
                }
            }
        }

        #endregion Eventos

        private void FrmExistenciaPorClaveSSA_Load(object sender, EventArgs e)
        {
            if (bLimpiar) 
                btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            query.MostrarMsjSiLeerVacio = false;
            Fg.IniciaControles(this, true);
            //grdExistencia.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            Grid.Limpiar(false);

            rdoRptTodos.Focus();        
            rdoRptTodos.Checked = true;
            query.MostrarMsjSiLeerVacio = true;
        }
        #endregion Botones

        #region Grid       

        private bool validarDatos()
        {
            bool bRegresa = true;

            if (cboEstados.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Estado válido, verifique.");
            }

            if (bRegresa && cboFarmacias.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Farmacia válida, verifique.");
            }

            if (bRegresa && txtId.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Clave SSA a consultar, verifique.");
            }

            return bRegresa;
        }

        private void LlenarGrid()
        {
            if (validarDatos())
            {
                bEjecutando = true;
                this.Cursor = Cursors.WaitCursor;

                DataSet dtsExistencias = new DataSet("Existencias");
                // DataTable dtEstados;
                // DataTable dtFarmacias;

                string sSql = string.Format(" Select IdProducto, DescripcionProducto, Sum(Existencia) as Existencia " +
                    " From SVR_INV_Generar_Existencia_Concentrado (NoLock) " +
                    " Where IdEstado = '{0}' and ClaveSSA = '{1}' and IdFarmacia = '{2}' " +
                    " Group by IdProducto, DescripcionProducto " +
                    " Order by IdProducto ", cboEstados.Data, txtId.Text, cboFarmacias.Data);

                Grid.Limpiar(false);
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "");
                    General.msjError("Ocurrió un error al obtener la información de existencias.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        cboEstados.Enabled = false;
                        cboFarmacias.Enabled = false;
                        Grid.LlenarGrid(leer.DataSetClase);
                        bSeEncontroInformacion = true;
                    }
                    else
                    {
                        bSeEncontroInformacion = false;
                    }
                }
                lblTotal.Text = Grid.TotalizarColumna(3).ToString(sFormato);

                bSeEjecuto = true;
                bEjecutando = false; // Cursor.Current

                this.Cursor = Cursors.Default;
            }
        }

        private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            ////Codigo.ShowDialog();
            //if (1 == 0)
            //{
            //    string sDato = Grid.GetValue(e.Row + 1, 1);
            //    Codigo = new FrmExistenciaPorCodigoInterno();
            //    Codigo.MostrarDetalle(sDato);
            //}
        }
        #endregion Grid        

        private void CargarFarmacias()
        {
            dtsFarmacias = new DataSet();

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            cboFarmacias.SelectedIndex = 0;

            dtsFarmacias = query.Farmacias_Status("CargarFarmacias()", "");
        }

        public void MostrarDetalle(string IdEstado, string IdFarmacia, string ClaveInternaSal, int iTipoRpt )
        {
            if (iTipoRpt == 0)
                rdoRptTodos.Checked = true; 

            if (iTipoRpt == 1) 
                rdoRptConExist.Checked = true; 

            if (iTipoRpt == 2)
                rdoRptSinExist.Checked = true;

            bLimpiar = false;
            btnNuevo.Enabled = false;

            cboEstados.Data = IdEstado;
            cboEstados.Enabled = false;

            CargarFarmaciasEstados();
            cboFarmacias.Enabled = false;
            cboFarmacias.Data = IdFarmacia;

            txtId.Text = ClaveInternaSal;
            txtId_Validating(null, null);

            this.ShowDialog();
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.ClavesSSA_Sales("txtId_KeyDown");
                if ( leer.Leer() )
                {
                    // txtId.Enabled = false;
                    txtId.Text = leer.Campo("ClaveSSA");
                    lblClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblDes.Text = leer.Campo("Descripcion");
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (Grid.Rows == 0)
            {
                General.msjUser("No ha información en pantalla para generar la impresión.");
            }
            else
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnOficinaCentral.RutaReportes;
                myRpt.NombreReporte = "PtoVta_ExistenciasSalesDetallado";

                myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("@IdClaveSSA_Sal", txtId.Text);
                myRpt.Add("@IdProducto", "");
                myRpt.Add("@CodigoEAN", "");

                ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////DataSet datosC = DatosCliente.DatosCliente();

                ////btReporte = conexionWeb.Reporte(InfoWeb, datosC);

                ////if (!myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true))
                ////{
                ////    General.msjError("Ocurrió un error al cargar el reporte.");
                ////}

                if (!DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente))
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.LlenarGrid);
            _workerThread.Name = "GenerandoValidacion";
            _workerThread.Start();
            //LlenarGrid();
        }

        private void cboEstados_Validating(object sender, CancelEventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;
            }
        }

        private void cboFarmacias_Validating(object sender, CancelEventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
                cboFarmacias.Enabled = false;
        }

        private void CargarFarmaciasEstados()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            if (cboEstados.SelectedIndex != 0)
            {
                try
                {
                    cboFarmacias.Add(dtsFarmacias.Tables[0].Select(string.Format("IdEstado = '{0}'", cboEstados.Data)), true, "IdFarmacia", "NombreFarmacia");
                }
                catch(Exception ex)
                {
                    ex.Source = ex.Source; 
                }
            }
            cboFarmacias.SelectedIndex = 0;
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFarmaciasEstados();
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                //FrameListaReportes.Enabled = true;
                btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;

                if (!bSeEncontroInformacion)
                {
                    _workerThread.Interrupt();
                    _workerThread = null;

                    //ActivarControles();

                    if (bSeEjecuto)
                    {
                        General.msjUser("No existe informacion para mostrar .");
                    }
                }
            }
        }
    }
}
