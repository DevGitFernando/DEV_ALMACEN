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
    public partial class FrmExistenciaPorClaveSSA_EstadoFarmacias : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        clsConsultas query;
        clsAyudas ayuda;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();

        FrmExistenciaPorClaveSSA_EstadoFarmaciasCodigos Codigo;
        private bool bLimpiar = true;

        string sFormato = "#,###,###,##0";

        Thread _workerThread;

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;


        public FrmExistenciaPorClaveSSA_EstadoFarmacias()
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
                    //LlenarGrid();
                    btnEjecutar_Click(null, null);
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

            cboEstados.Focus();
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

            if (bRegresa && txtId.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Clave SSA a consultar, verifique."); 
            }

            return bRegresa;
        }

        private void LlenarGrid()
        {

            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            string sExistencia = "";

            if (rdoRptTodos.Checked)
                sExistencia = "AND Existencia >= 0"; 

            if (rdoRptConExist.Checked)
                sExistencia = " AND Existencia > 0 "; 

            if (rdoRptSinExist.Checked)
                sExistencia = " AND Existencia = 0 "; 

            if (validarDatos())
            {
                DataSet dtsExistencias = new DataSet("Existencias");
                // DataTable dtEstados;
                // DataTable dtFarmacias;

                string sSql = string.Format(" Select IdFarmacia, Farmacia, Sum(Existencia) as Existencia " +
                    " From SVR_INV_Generar_Existencia_Concentrado (NoLock) " +
                    " Where IdEstado = '{0}' and ClaveSSA = '{1}' and IdFarmacia <> '' {2} " +
                    " Group by IdFarmacia, Farmacia " +
                    " Order by IdFarmacia ", cboEstados.Data, txtId.Text, sExistencia );

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
            }

            this.Cursor = Cursors.Default;
        }

        private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //Codigo.ShowDialog();
            int iTipoRpt = 0;

            if (rdoRptTodos.Checked)
                iTipoRpt = 0; 

            if (rdoRptConExist.Checked)
                iTipoRpt = 1;
             
            if (rdoRptSinExist.Checked)
                iTipoRpt = 2;

            string sFarmacia = Grid.GetValue(e.Row + 1, 1);
            if (sFarmacia != "")
            {
                Codigo = new FrmExistenciaPorClaveSSA_EstadoFarmaciasCodigos();
                Codigo.MostrarDetalle(cboEstados.Data, sFarmacia, txtId.Text, iTipoRpt );
            }
        }
        #endregion Grid        

        public void MostrarDetalle(string IdEstado, string ClaveInternaSal, int iTipoRpt)
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
            int iTipoRpt = 0;

            if (rdoRptTodos.Checked)
                iTipoRpt = 0;
            if (rdoRptConExist.Checked)
                iTipoRpt = 1;
            if (rdoRptSinExist.Checked)
                iTipoRpt = 2;

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
                myRpt.NombreReporte = "Central_ExistenciasSales_Estados_Farmacias";

                myRpt.Add("@IdEmpresa", "");
                myRpt.Add("@IdEstado", cboEstados.Data);
                myRpt.Add("@IdFarmacia", "");
                myRpt.Add("@IdClaveSSA_Sal", txtId.Text);
                myRpt.Add("@IdProducto", "");
                myRpt.Add("@CodigoEAN", "");
                myRpt.Add("@Mostrar", 1); // Solo se muestran las sales que han tenido movimiento
                myRpt.Add("@iTipoRpt", iTipoRpt);       

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
                cboEstados.Enabled = false;
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
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                    }
                }
            }
        }
    }
}
