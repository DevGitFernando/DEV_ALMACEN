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
    public partial class FrmCaducarPorClaveSSA_Estado : FrmBaseExt
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
        FrmCaducarPorClaveSSA_EstadoFarmaciasCodigos FarmaciasCodigos;

        private bool bOpcionCargada = false;

        //Manejo de Hilos
        Thread _workerThread;
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        public FrmCaducarPorClaveSSA_Estado()
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
        }

        private void FrmCaducarPorClaveSSA_Estado_Load(object sender, EventArgs e)
        {
            if (!bOpcionCargada)
            {
                btnNuevo_Click(null, null);
            }
        }

        #region Limpiar 
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

            txtId.Focus();
            query.MostrarMsjSiLeerVacio = true;
            lblClaveSSA.Visible = false;
        }
        #endregion Limpiar 

        #region Buscar Clave
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text.Trim() != "")
            {
                leer.DataSetClase = query.ClavesSSA_Sales(txtId.Text, "txtId_Validating");
                if (leer.Leer())
                {
                    txtId.Enabled = false;
                    txtId.Text = leer.Campo("ClaveSSA");
                    lblClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblDes.Text = leer.Campo("Descripcion");
                    //LlenarGrid();
                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.ClavesSSA_Sales("txtId_KeyDown");
                if (leer.Leer())
                {
                    txtId.Text = leer.Campo("ClaveSSA");
                    txtId_Validating(null, null);
                }
            }
        }

        #endregion Buscar Clave

        #region Imprimir
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
                myRpt.NombreReporte = "Central_CaducarSales_Estados";

                myRpt.Add("@IdEmpresa", "");
                myRpt.Add("@IdEstado", "");
                myRpt.Add("@IdFarmacia", "");
                myRpt.Add("@IdClaveSSA_Sal", txtId.Text);
                myRpt.Add("@IdProducto", "");
                myRpt.Add("@CodigoEAN", "");
                myRpt.Add("@Mostrar", 1);
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
        #endregion Imprimir

        #region Grid   
        private void LlenarGridFarmacias(int Renglon)
        {
            myGrid.Limpiar(false);
            try
            {
                leer.DataSetClase = dtsExistenciasEstados;
                string sEstado = Grid.GetValue(Renglon, 1);
                myGrid.AgregarRenglon(leer.DataTableClase.Select(string.Format("IdEstado = '{0}'", sEstado)), 4, false);
            }
            catch //( Exception ex )
            {
                //General.msjUser(ex.Message);
            }
            lblTotalFarmacias.Text = myGrid.TotalizarColumna(4).ToString();
        }

        private void grdExistenciaFarmacias_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sEstado = Grid.GetValue(Grid.ActiveRow, 1);
            string sFarmacia = myGrid.GetValue(e.Row + 1, 2);

            if (sEstado != "" && sFarmacia != "")
            {
                FarmaciasCodigos = new FrmCaducarPorClaveSSA_EstadoFarmaciasCodigos();
                FarmaciasCodigos.MostrarDetalle(sEstado, sFarmacia, txtId.Text, dtpFechaInicial.Text, dtpFechaFinal.Text);
            }
        }

        private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //Codigo.ShowDialog();
            string sEstado = Grid.GetValue(e.Row + 1, 1);
            if (sEstado != "")
            {
                Codigo = new FrmCaducarPorClaveSSA_EstadoFarmacias();
                Codigo.MostrarDetalle(sEstado, txtId.Text, dtpFechaInicial.Text, dtpFechaFinal.Text );
            }
        }
        #endregion Grid        

        #region Obtener Informacion 
        private void btnEjecutar_Click(object sender, EventArgs e)
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

        private void LlenarGrid()
        {
            string sSql = string.Format(" Select IdEstado, Estado, Sum(Existencia) as Existencia " +
                " From SVR_INV_Generar_Existencia_Detallado (NoLock) " +
                " Where ClaveSSA = '{0}' and IdFarmacia <> '' " +
                " And Convert( varchar(10), FechaCaducidad, 120 )  Between '{1}' And '{2}'  " +
                " Group by IdEstado, Estado " +
                " Having Sum(Existencia) > 0 " +
                " Order by IdEstado ", txtId.Text,
                General.FechaYMD(dtpFechaInicial.Value, "-"),
                General.FechaYMD(dtpFechaFinal.Value.AddMonths(3), "-")); //Se le agregan 3 meses a la fecha Final

            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            Grid.Limpiar(false);
            myGrid.Limpiar(false);

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

                    Grid.LlenarGrid(leer.DataSetClase);
                    ObtenerExistenciaEstados();
                    LlenarGridFarmacias(1);

                    tmExistencias.Start();
                    tmExistencias.Enabled = true;
                }
                else
                {
                    bSeEncontroInformacion = false;
                    IniciaToolBar(true, true, false);
                    //General.msjUser("No existe información para mostrar bajo los criterios seleccionados");
                }
            }
            lblTotal.Text = Grid.TotalizarColumna(3).ToString();

            bEjecutando = false;
            this.Cursor = Cursors.Default;
        }

        private void ObtenerExistenciaEstados()
        {
            string sSql = string.Format(" Select IdEstado, IdFarmacia, Farmacia, Sum(Existencia) as Existencia " +
                " From SVR_INV_Generar_Existencia_Detallado (NoLock) " +
                " Where ClaveSSA = '{0}' and IdFarmacia <> '' " +
                " And Convert( varchar(10), FechaCaducidad, 120 )  Between '{1}' And '{2}'  " +
                " Group by IdEstado, IdFarmacia, Farmacia " +
                " Having Sum(Existencia) > 0 " +
                " Order by IdEstado, IdFarmacia ", txtId.Text,
                General.FechaYMD(dtpFechaInicial.Value, "-"),
                General.FechaYMD(dtpFechaFinal.Value.AddMonths(3), "-")); //Se le agregan 3 meses a la fecha Final

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
        #endregion Obtener Informacion

        #region Funciones 
        private void IniciaToolBar(bool Nuevo, bool Ejecutar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }

        private void ActivarControles(bool Activar)
        {
            txtId.Enabled = Activar;
            dtpFechaFinal.Enabled = Activar;
            dtpFechaInicial.Enabled = Activar;
            //grdExistencia.Enabled = Activar;
        }

        public void MostrarDetalle(string ClaveInternaSal, string FechaInicial, string FechaFinal)
        {
            btnNuevo_Click(this, null);
            bOpcionCargada = true;
            btnNuevo.Enabled = false;
            txtId.Text = ClaveInternaSal;

            dtpFechaInicial.Value = DateTime.Parse(FechaInicial);
            dtpFechaFinal.Value = DateTime.Parse(FechaFinal);
            dtpFechaInicial.Enabled = false;
            dtpFechaFinal.Enabled = false;

            txtId_Validating(null, null);
            LlenarGrid();

            this.ShowDialog();
        }

        private void tmExistencias_Tick(object sender, EventArgs e)
        {
            if (this.ActiveControl.Name.ToUpper() == grdExistencia.Name.ToUpper())
            {
                if (Grid.GetValue(Grid.ActiveRow, 1) != "")
                {
                    LlenarGridFarmacias(Grid.ActiveRow);
                }
            }
        }
        #endregion Funciones


    }//LLAVES DE LA CLASE
}
