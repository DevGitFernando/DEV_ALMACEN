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
    public partial class FrmExistenciaPorEstado : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid, myGrid;
        clsConsultas query;
        clsAyudas ayuda;

        DataSet dtsExistenciasEstados;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();

        FrmExistenciaPorClaveSSA_EstadoFarmacias Codigo;
        FrmExistenciaPorClaveSSA_EstadoFarmaciasCodigos FarmaciaCodigos;

        private bool bOpcionCargada = false;
        string sFormato = "#,###,###,##0";

        //Manejo de Hilos
        Thread _workerThread;
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;


        public FrmExistenciaPorEstado()
        {
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            Grid = new clsGrid(ref grdExistencia, this);
            Grid.EstiloGrid(eModoGrid.SeleccionSimple);
            Grid.Limpiar(false);

            myGrid = new clsGrid(ref grdExistenciaFarmacias, this);
            myGrid.EstiloGrid(eModoGrid.SeleccionSimple);
            myGrid.Limpiar(false);

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;

            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(query.EstadosConFarmacias("FrmExistenciaPorClaveSSA_EstadoFarmacias"), true, "IdEstado", "Estado");
            cboEstados.SelectedIndex = 0;
        }

        private void FrmExistenciaPorClaveSSA_Load(object sender, EventArgs e)
        {
            if (!bOpcionCargada)
            {
                btnNuevo_Click(null, null);
            }
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            tmExistencias.Stop();
            tmExistencias.Enabled = false;

            ActivarControles(true);
            IniciaToolBar(true, true, false);

            query.MostrarMsjSiLeerVacio = false;
            Fg.IniciaControles(this, true);
            //grdExistencia.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            Grid.Limpiar(false);
            myGrid.Limpiar(false);

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
            return bRegresa;
        }

        private void LlenarGrid()
        {
            string sExistencia = "";

            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            if (rdoRptTodos.Checked)
                sExistencia = "AND Existencia >= 0"; 

            if (rdoRptConExist.Checked)
                sExistencia = " AND Existencia > 0 "; 

            if (rdoRptSinExist.Checked)
                sExistencia = " AND Existencia = 0 ";

            string sSql = string.Format(" Select '' as IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Sum(Existencia) as Existencia " +
                " From SVR_INV_Generar_Existencia_Concentrado (NoLock) " +
                " Where IdEstado = '{0}'  {1} " +
                " Group by ClaveSSA, DescripcionSal " +
                " Order by ClaveSSA ", cboEstados.Data, sExistencia);

            ActivarControles(false);
            Grid.Limpiar(false);
            tmExistencias.Stop();
            tmExistencias.Enabled = false;

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de existencias.");
            }
            else
            {
                cboEstados.Enabled = false;
                Grid.LlenarGrid(leer.DataSetClase);
                CargarInformacionClave(1);

                ObtenerExistenciaEstados();
                LlenarGridFarmacias(1);

                if (leer.Leer())
                {
                    if (!bOpcionCargada)
                    {
                        IniciaToolBar(true, false, true);
                    }

                    tmExistencias.Enabled = true;
                    tmExistencias.Start();
                }
            }

            lblTotal.Text = Grid.TotalizarColumna(4).ToString(sFormato);
            

            bEjecutando = false;
            this.Cursor = Cursors.Default;
        }

        private void ObtenerExistenciaEstados()
        {
            string sExistencia = "";

            if (rdoRptTodos.Checked)
                sExistencia = "AND Existencia >= 0";

            if (rdoRptConExist.Checked)
                sExistencia = " AND Existencia > 0 ";

            if (rdoRptSinExist.Checked)
                sExistencia = " AND Existencia = 0 ";

            string sSql = string.Format(" Select IdEstado, IdFarmacia, Farmacia, Sum(Existencia) as Existencia, IdClaveSSA_Sal " +
                " From SVR_INV_Generar_Existencia_Concentrado (NoLock) " +
                " Where IdEstado = '{0}' {1} " +
                " Group by IdEstado, IdFarmacia, Farmacia, IdClaveSSA_Sal " +
                " Order by IdEstado, IdFarmacia, IdClaveSSA_Sal ", cboEstados.Data, sExistencia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de existencias.");
            }
            else
            {
                dtsExistenciasEstados = leer.DataSetClase;
            }

        }

        private void LlenarGridFarmacias(int Renglon)
        {
            myGrid.Limpiar(false);
            try
            {
                leer.DataSetClase = dtsExistenciasEstados;
                string sClave = Grid.GetValue(Renglon, 2);
                myGrid.AgregarRenglon(leer.DataTableClase.Select(string.Format("IdEstado = '{0}' and ClaveSSA = '{1}' ", cboEstados.Data, sClave)), 4, false);
            }
            catch //( Exception ex )
            {
                //General.msjUser(ex.Message);
            }
            lblTotalFarmacias.Text = myGrid.TotalizarColumna(4).ToString(sFormato);
        }

        private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //Codigo.ShowDialog();
            int iTipoRpt = 0;

            if ( rdoRptTodos.Checked)
                iTipoRpt = 0; 

            if (rdoRptConExist.Checked)
                iTipoRpt = 1; 

            if (rdoRptSinExist.Checked)
                iTipoRpt = 2;

            string sFarmacia = Grid.GetValue(e.Row + 1, 1);
            if (sFarmacia != "")
            {
                Codigo = new FrmExistenciaPorClaveSSA_EstadoFarmacias();
                Codigo.MostrarDetalle(cboEstados.Data, sFarmacia, iTipoRpt);
            }
        }

        private void grdExistenciaFarmacias_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sFarmacia = myGrid.GetValue(myGrid.ActiveRow, 2);
            int iTipoRpt = 0;

            if (rdoRptTodos.Checked)
                iTipoRpt = 0;
            if (rdoRptConExist.Checked)
                iTipoRpt = 1;
            if (rdoRptSinExist.Checked)
                iTipoRpt = 2;

            if (sFarmacia != "")
            {
                FarmaciaCodigos = new FrmExistenciaPorClaveSSA_EstadoFarmaciasCodigos();
                FarmaciaCodigos.MostrarDetalle(cboEstados.Data, sFarmacia, txtClaveSSA.Text, iTipoRpt);
            }
        }
        #endregion Grid        

        #region Imprimir 
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
                myRpt.Add("@IdClaveSSA_Sal", "");
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
        #endregion Imprimir

        #region Obtener Informacion 
        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                //LlenarGrid();
                bSeEncontroInformacion = false;
                IniciaToolBar(false, false, false);

                ActivarControles(false);

                bSeEjecuto = false;
                tmEjecuciones.Enabled = true;
                tmEjecuciones.Start();


                Cursor.Current = Cursors.WaitCursor;
                System.Threading.Thread.Sleep(1000);

                _workerThread = new Thread(this.LlenarGrid);
                _workerThread.Name = "ObteniendoExistecias";
                _workerThread.Start();

                if (bOpcionCargada)
                {
                    IniciaToolBar(false, false, true);
                }
            }
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                //btnNuevo.Enabled = true;
                //btnEjecutar.Enabled = false;

                if (!bSeEncontroInformacion)
                {
                    _workerThread.Interrupt();
                    _workerThread = null;

                    ActivarControles(false);

                    if (bSeEjecuto)
                    {
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                    }
                }
            }
        }
        #endregion Obtener Informacion

        private void cboEstados_Validating(object sender, CancelEventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
                cboEstados.Enabled = false;
        }

        private void CargarInformacionClave(int Renglon)
        {
            txtClaveSSA.Text = Grid.GetValue(Renglon, 2);
            lblClaveSSA.Text = Grid.GetValue(Renglon, 2);
            lblDescripcionClaveSSA.Text = Grid.GetValue(Renglon, 3);
        }       

        #region Funciones 
        private void IniciaToolBar(bool Nuevo, bool Ejecutar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }

        private void ActivarControles(bool Activar)
        {
            rdoRptTodos.Enabled = Activar;
            rdoRptConExist.Enabled = Activar;
            rdoRptSinExist.Enabled = Activar;
            cboEstados.Enabled = Activar;
            //grdExistencia.Enabled = Activar;
        }

        public void MostrarDetalle(string IdEstado, string ClaveInternaSal)
        {
            bOpcionCargada = true;
            btnNuevo.Enabled = false;

            cboEstados.Data = IdEstado;
            cboEstados.Enabled = false;
            btnEjecutar_Click(null, null);

            this.ShowDialog();
        }

        private void tmExistencias_Tick(object sender, EventArgs e)
        {
            if (this.ActiveControl.Name.ToUpper() == grdExistencia.Name.ToUpper())
            {
                if (Grid.GetValue(Grid.ActiveRow, 1) != "")
                {
                    CargarInformacionClave(Grid.ActiveRow);
                    LlenarGridFarmacias(Grid.ActiveRow);
                }
            }
        }

        private void grdExistencia_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            int iRow = e.NewRow + 1; 
            if (this.ActiveControl.Name.ToUpper() == grdExistencia.Name.ToUpper())
            {
                if (Grid.GetValue(iRow, 2) != "")
                {
                    CargarInformacionClave(iRow);
                    LlenarGridFarmacias(iRow);
                }
            }
        }
        #endregion Funciones 

    }//LLAVES DE LA CLASE
}
