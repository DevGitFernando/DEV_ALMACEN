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

// Implementacion de hilos 
using System.Threading;

namespace OficinaCentral.Inventario
{
    public partial class FrmCaducarPorEstado : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid, myGrid;
        clsConsultas query;
        clsAyudas ayuda;

        DataSet dtsExistenciasEstados;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();

        FrmCaducarPorClaveSSA_EstadoFarmacias Codigo;
        FrmCaducarPorClaveSSA_EstadoFarmaciasCodigos FarmaciaCodigos;

        private bool bOpcionCargada = false;

        //Manejo de Hilos
        Thread _workerThread;
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        public FrmCaducarPorEstado()
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
            cboEstados.Add(query.EstadosConFarmacias("FrmCaducarPorEstado"), true, "IdEstado", "Estado");
            cboEstados.SelectedIndex = 0;
        }

        private void FrmExistenciaPorClaveSSA_Load(object sender, EventArgs e)
        {
            if (!bOpcionCargada)
            {
                btnNuevo_Click(null, null);
            }
        }

        private void FrmCaducarPorEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void FrmCaducarPorEstado_KeyDown(object sender, KeyEventArgs e)
        {
            //if (this.ActiveControl.Name.ToUpper() == grdExistencia.Name.ToUpper())
            //{
            //    CargarInformacionClave(Grid.ActiveRow);
            //    LlenarGridFarmacias(Grid.ActiveRow);
            //}
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

            cboEstados.Focus();
            query.MostrarMsjSiLeerVacio = true;
            txtClaveSSA.Visible = false;
        }
        #endregion Botones

        #region Grid       
        private void LlenarGrid()
        {
            string sSql = string.Format(" Select IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Sum(Existencia) as Existencia " +
                " From SVR_INV_Generar_Existencia_Detallado (NoLock) " +
                " Where IdEstado = '{0}' " +
                " And Convert( varchar(10), FechaCaducidad, 120 )  Between '{1}' And '{2}'  " +
                " Group by IdClaveSSA_Sal, ClaveSSA, DescripcionSal " +
                " Having Sum(Existencia) > 0 " +
                " Order by IdClaveSSA_Sal ", cboEstados.Data, 
                General.FechaYMD(dtpFechaInicial.Value,"-"), 
                General.FechaYMD(dtpFechaFinal.Value.AddMonths(3), "-") ); //Se le agregan 3 meses a la Fecha Final.

            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            Grid.Limpiar(false);
            tmExistencias.Stop();
            tmExistencias.Enabled = false;

            if (!leer.Exec(sSql))
            {
                bSeEncontroInformacion = false;
                IniciaToolBar(true, true, false);

                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de existencias.");
            }
            else
            {
                bSeEjecuto = true;
                if (leer.Leer())
                {
                    bSeEncontroInformacion = true;
                    IniciaToolBar(true, false, true);
                    if (bOpcionCargada)
                    {
                        IniciaToolBar(false, false, true);
                    }

                    tmExistencias.Enabled = true;
                    tmExistencias.Start();

                    cboEstados.Enabled = false;
                    dtpFechaInicial.Enabled = false;
                    dtpFechaFinal.Enabled = false;

                    Grid.LlenarGrid(leer.DataSetClase);
                    CargarInformacionClave(1);

                    ObtenerExistenciaEstados();
                    LlenarGridFarmacias(1);
                }
                else
                {
                    bSeEncontroInformacion = false;
                    IniciaToolBar(true, true, false);
                    //General.msjUser("No existe información para mostrar bajo los criterios seleccionados");
                }
            }
            lblTotal.Text = Grid.TotalizarColumna(4).ToString();

            bEjecutando = false;
            this.Cursor = Cursors.Default;
        }

        private void ObtenerExistenciaEstados()
        {
            string sSql = string.Format(" Select IdEstado, IdFarmacia, Farmacia, Sum(Existencia) as Existencia, ClaveSSA " +
                " From SVR_INV_Generar_Existencia_Detallado (NoLock) " +
                " Where IdEstado = '{0}'  " +
                " And Convert( varchar(10), FechaCaducidad, 120 )  Between '{1}' And '{2}'  " +
                " Group by IdEstado, IdFarmacia, Farmacia, ClaveSSA " +
                " Having Sum(Existencia) > 0 " +
                " Order by IdEstado, IdFarmacia, ClaveSSA ", cboEstados.Data, 
                General.FechaYMD(dtpFechaInicial.Value, "-"), 
                General.FechaYMD(dtpFechaFinal.Value.AddMonths(3), "-" ) );

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
            lblTotalFarmacias.Text = myGrid.TotalizarColumna(4).ToString();
        }

        private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //Codigo.ShowDialog();
            string sClaveSSA = Grid.GetValue(e.Row + 1, 2);
            if (sClaveSSA != "")
            {
                Codigo = new FrmCaducarPorClaveSSA_EstadoFarmacias();
                Codigo.MostrarDetalle(cboEstados.Data, sClaveSSA, dtpFechaInicial.Text, dtpFechaFinal.Text);
            }
        }
        #endregion Grid        

        public void MostrarDetalle(string IdEstado, string ClaveInternaSal)
        {
            bOpcionCargada = true;
            btnNuevo.Enabled = false;

            cboEstados.Data = IdEstado;
            cboEstados.Enabled = false;
            btnEjecutar_Click(null, null);

            this.ShowDialog();
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
                byte[] btReporte = null;

                myRpt.RutaReporte = GnOficinaCentral.RutaReportes;
                myRpt.NombreReporte = "Central_CaducarSales_Estados_Farmacias";

                myRpt.Add("@IdEmpresa", "");
                myRpt.Add("@IdEstado", cboEstados.Data);
                myRpt.Add("@IdFarmacia", "");
                myRpt.Add("@IdClaveSSA_Sal", "");
                myRpt.Add("@IdProducto", "");
                myRpt.Add("@CodigoEAN", "");
                myRpt.Add("@Mostrar", 1); // Solo se muestran las sales que han tenido movimiento 
                myRpt.Add("@FechaInicial", dtpFechaInicial.Text);
                myRpt.Add("@FechaFinal", General.FechaYMD(dtpFechaFinal.Value.AddMonths(3), "-")); //Se le agregan 3 meses a la Fecha Final.

                DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                DataSet datosC = DatosCliente.DatosCliente();

                btReporte = conexionWeb.Reporte(InfoWeb, datosC);

                if (!myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true))
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if(ValidaDatos())
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

        private void cboEstados_Validating(object sender, CancelEventArgs e)
        {
            //if (cboEstados.SelectedIndex != 0)
            //    cboEstados.Enabled = false;
        }

        private void CargarInformacionClave(int Renglon)
        {
            txtClaveSSA.Text = Grid.GetValue(Renglon, 1);
            lblClaveSSA.Text = Grid.GetValue(Renglon, 2);
            lblDescripcionClaveSSA.Text = Grid.GetValue(Renglon, 3);
        }

        private void grdExistencia_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            //CargarInformacionClave(e.NewRow + 1);
            //LlenarGridFarmacias(e.NewRow + 1);
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

        private void grdExistenciaFarmacias_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sFarmacia = myGrid.GetValue(myGrid.ActiveRow, 2);

            if (sFarmacia != "")
            {
                FarmaciaCodigos = new FrmCaducarPorClaveSSA_EstadoFarmaciasCodigos();
                FarmaciaCodigos.MostrarDetalle(cboEstados.Data, sFarmacia, lblClaveSSA.Text, dtpFechaInicial.Text, dtpFechaFinal.Text);
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

        #region Funciones
        private void IniciaToolBar(bool Nuevo, bool Ejecutar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }

        private void ActivarControles(bool Activar)
        {
            cboEstados.Enabled = Activar;
            dtpFechaFinal.Enabled = Activar;
            dtpFechaInicial.Enabled = Activar;
            //grdExistencia.Enabled = Activar;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            // string sIdProducto = "";

            if (cboEstados.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione un Estado por favor.");
                cboEstados.Focus();
            }

            return bRegresa;
        }
        #endregion Funciones 

    }//LLAVES DE LA CLASE
}
