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
using SC_SolutionsSystem.FuncionesGenerales; 
using SC_SolutionsSystem.Reportes;

// Implementacion de hilos 
using System.Threading;

using DllFarmaciaSoft;

namespace OficinaCentral.Inventario
{
    public partial class FrmExistenciaGralClaveSSA : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        clsConsultas query;
        clsAyudas ayuda;
        clsListView lst; 

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();

        FrmExistenciaPorClaveSSA_Estado Sales;

        string sFormato = "#,###,###,##0";

        Thread _workerThread;

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false; 

        public FrmExistenciaGralClaveSSA()
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

            lst = new clsListView(lstExistencia);
            lst.PermitirAjusteDeColumnas = false; 

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;


            CheckForIllegalCrossThreadCalls = false;

        }

        private void FrmExistenciaPorClaveSSA_Load(object sender, EventArgs e)
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
            lst.LimpiarItems(); 

            query.MostrarMsjSiLeerVacio = true;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoValidacion";
            _workerThread.Start();
            
        }

        private void ObtenerInformacion()
        {
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            string sFiltro = " Where 1  =  1  ";
            string sExistencia = "";

            if (rdoRptTodos.Checked)
                sExistencia = "WHERE Existencia >= 0";

            if (rdoRptConExist.Checked)
                sExistencia = " WHERE Existencia > 0 ";

            if (rdoRptSinExist.Checked)
                sExistencia = " WHERE Existencia = 0 ";

            if (rdoClavesTodas.Checked)
                sFiltro = "";

            if (rdoClavesUtilizadas.Checked)
                sFiltro = " and IdFarmacia <> '' ";

            if (rdoClavesSinUtilizar.Checked)
                sFiltro = " and IdFarmacia = '' ";


            string sSql = string.Format(" Select IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Sum(Existencia) as Existencia " +
                " From SVR_INV_Generar_Existencia_Concentrado (NoLock) " +
                " {0} {1} " +
                " Group by IdClaveSSA_Sal, ClaveSSA, DescripcionSal " +
                " Order by IdClaveSSA_Sal ", sExistencia, sFiltro);

            Grid.Limpiar(false);
            lst.LimpiarItems(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de existencias.");
            }
            else
            {
                if (leer.Leer())
                {
                    bSeEncontroInformacion = true;
                    Grid.LlenarGrid(leer.DataSetClase);
                    //lst.CargarDatos(leer.DataSetClase, false, false); 
                }
                else
                {
                    bSeEncontroInformacion = false;
                    //General.msjUser("No se encontro información para mostrar.");
                }
            }

            bSeEjecuto = true;
            bEjecutando = false; // Cursor.Current

            lblTotal.Text = Grid.TotalizarColumna(4).ToString(sFormato);
            CargarDescripcion(1);

            this.Cursor = Cursors.Default;
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
                int iMostrar = 1;

                if (rdoClavesSinUtilizar.Checked)
                    iMostrar = 2;

                if ( rdoClavesTodas.Checked )
                    iMostrar = 3;

                myRpt.RutaReporte = GnOficinaCentral.RutaReportes;
                myRpt.NombreReporte = "Central_ExistenciasSales_General";

                if ( rdoRptDetalladoEstados.Checked )
                    myRpt.NombreReporte = "Central_ExistenciasSales_Estados";

                if (rdoRptDetalladoFarmacia.Checked)
                    myRpt.NombreReporte = "Central_ExistenciasSales_Estados_Farmacias";


                myRpt.Add("@IdEmpresa", "");
                myRpt.Add("@IdEstado", "");             
                myRpt.Add("@IdFarmacia", "");
                myRpt.Add("@IdClaveSSA_Sal", "");
                myRpt.Add("@IdProducto", "");
                myRpt.Add("@CodigoEAN", "");
                myRpt.Add("@Mostrar", iMostrar);
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
        #endregion Botones

        #region Grid 
        private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sClaveSSA = Grid.GetValue(e.Row + 1, 2);
            int iTipoRpt = 0;

            if (rdoRptTodos.Checked) 
                iTipoRpt = 0;

            if (rdoRptConExist.Checked)
                iTipoRpt = 1; 

            if (rdoRptSinExist.Checked)
                iTipoRpt = 2;

            if (sClaveSSA != "")
            {
                Sales = new FrmExistenciaPorClaveSSA_Estado();
                Sales.MostrarDetalle(sClaveSSA, iTipoRpt);
            }
        }
        #endregion Grid        

        private void CargarDescripcion(int Renglon)
        {
            lblDescripcionSal.Text = Grid.GetValue(Renglon, 3);
        }

        private void grdExistencia_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            CargarDescripcion(e.NewRow + 1);
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
