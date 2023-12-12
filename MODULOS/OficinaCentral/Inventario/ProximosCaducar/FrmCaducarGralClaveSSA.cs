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
    public partial class FrmCaducarGralClaveSSA : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        clsConsultas query;
        clsAyudas ayuda;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();

        FrmCaducarPorClaveSSA_Estado Sales;

        //Manejo de Hilos
        Thread _workerThread;
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        public FrmCaducarGralClaveSSA()
        {
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);

            Grid = new clsGrid(ref grdExistencia, this);
            Grid.EstiloGrid(eModoGrid.SeleccionSimple);

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;

        }

        private void FrmCaducarGralClaveSSA_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            query.MostrarMsjSiLeerVacio = false;
            Fg.IniciaControles(this, true);
            //grdExistencia.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            Grid.Limpiar(false);

            ActivarControles(true);
            IniciaToolBar(true, true, false);

            query.MostrarMsjSiLeerVacio = true;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            //if (validarDatos())
            {
                bSeEncontroInformacion = false;
                IniciaToolBar(false, false, false);

                ActivarControles(false);

                bSeEjecuto = false;
                tmEjecuciones.Enabled = true;
                tmEjecuciones.Start();


                Cursor.Current = Cursors.WaitCursor;
                System.Threading.Thread.Sleep(1000);

                _workerThread = new Thread(this.ObtenerInformacion);
                _workerThread.Name = "ObteniendoExistecias";
                _workerThread.Start();

                ////if (bOpcionCargada)
                ////{
                ////    IniciaToolBar(false, false, true);
                ////}
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
                byte[] btReporte = null;
                int iMostrar = 1;

                myRpt.RutaReporte = GnOficinaCentral.RutaReportes;
                //myRpt.NombreReporte = "Central_CaducarSales_Estados";

                if (rdoRptGeneral.Checked)
                    myRpt.NombreReporte = "Central_CaducarSales_Estados";

                if (rdoRptDetallado.Checked)
                    myRpt.NombreReporte = "Central_CaducarSales_Estados_Farmacias";


                myRpt.Add("@IdEmpresa", "");
                myRpt.Add("@IdEstado", "");
                myRpt.Add("@IdFarmacia", "");
                myRpt.Add("@IdClaveSSA_Sal", "");
                myRpt.Add("@IdProducto", "");
                myRpt.Add("@CodigoEAN", "");
                myRpt.Add("@Mostrar", iMostrar);
                myRpt.Add("@FechaInicial", dtpFechaInicial.Text);
                myRpt.Add("@FechaFinal", General.FechaYMD( dtpFechaFinal.Value.AddMonths(3), "-" ) ); //Se le agregan 3 meses a la Fecha Final.

                DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                DataSet datosC = DatosCliente.DatosCliente();

                btReporte = conexionWeb.Reporte(InfoWeb, datosC);

                if (!myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true))
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Botones

        #region Grid 
        private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sClaveSSA = Grid.GetValue(e.Row + 1, 2);
            if (sClaveSSA != "")
            {
                Sales = new FrmCaducarPorClaveSSA_Estado();
                Sales.MostrarDetalle(sClaveSSA, dtpFechaInicial.Text, dtpFechaFinal.Text);
            }
        }
        #endregion Grid        

        #region Funciones 
        private void IniciaToolBar(bool Nuevo, bool Ejecutar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }

        private void ActivarControles(bool Activar)
        {
            rdoRptGeneral.Enabled = Activar;
            rdoRptDetallado.Enabled = Activar;
            dtpFechaFinal.Enabled = Activar;
            dtpFechaInicial.Enabled = Activar;
            //grdExistencia.Enabled = Activar;
        }

        private void CargarDescripcion(int Renglon)
        {
            lblDescripcionSal.Text = Grid.GetValue(Renglon, 3);
        }

        private void grdExistencia_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            CargarDescripcion(e.NewRow + 1);
        }
        #endregion Funciones 

        #region Obtener Informacion
        private void ObtenerInformacion()
        {
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            string sSql = string.Format("Select IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Sum(Existencia) as Existencia " +
                " From SVR_INV_Generar_Existencia_Detallado (NoLock) " +
                " Where IdFarmacia <> '' " +
                " And Convert( varchar(10), FechaCaducidad, 120 )  Between '{0}' And '{1}'  " +
                " Group by IdClaveSSA_Sal, ClaveSSA, DescripcionSal " +
                " Having Sum(Existencia) > 0 " +
                " Order by IdClaveSSA_Sal ",
                General.FechaYMD(dtpFechaInicial.Value, "-"),
                General.FechaYMD(dtpFechaFinal.Value.AddMonths(3), "-")); //Se le agregan 3 meses a la fecha Final.

            ActivarControles(false);
            Grid.Limpiar(false);
            if (!leer.Exec(sSql))
            {
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
                    Grid.LlenarGrid(leer.DataSetClase);
                }
                else
                {
                    bSeEncontroInformacion = false;
                    IniciaToolBar(true, true, false);
                    //General.msjUser("No existe información para mostrar bajo los criterios seleccionados");
                }
            }
            lblTotal.Text = Grid.TotalizarColumna(4).ToString();
            CargarDescripcion(1);

            bEjecutando = false;
            this.Cursor = Cursors.Default;
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

    }//LLAVES DE LA CLASE
}
