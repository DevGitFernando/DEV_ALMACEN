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
    public partial class FrmCaducarPorClaveSSA_EstadoFarmacias : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        clsConsultas query;
        clsAyudas ayuda;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();

        FrmCaducarPorClaveSSA_EstadoFarmaciasCodigos Codigo;
        private bool bOpcionCargada = false;

        //Manejo de Hilos
        Thread _workerThread;
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        public FrmCaducarPorClaveSSA_EstadoFarmacias()
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

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;

            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(query.EstadosConFarmacias("FrmCaducarPorClaveSSA_EstadoFarmacias"), true, "IdEstado", "Estado");
            cboEstados.SelectedIndex = 0;
        }

        #region Eventos

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            // string sSql = "";

            if (txtId.Text.Trim() != "")
            {
                leer.DataSetClase = query.ClavesSSA_Sales(txtId.Text, "txtId_Validating");
                if (leer.Leer())
                {
                    txtId.Enabled = false;
                    txtId.Text = leer.Campo("ClaveSSA");
                    lblClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblDes.Text = leer.Campo("Descripcion");
                    //btnEjecutar_Click(null, null);
                    //LlenarGrid();
                }
            }
        }

        #endregion Eventos

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
            query.MostrarMsjSiLeerVacio = false;
            Fg.IniciaControles(this, true);
            //grdExistencia.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            Grid.Limpiar(false);

            ActivarControles(true);
            IniciaToolBar(true, true, false);

            cboEstados.Focus();
            query.MostrarMsjSiLeerVacio = true;
            lblClaveSSA.Visible = false;
        }
        #endregion Botones

        #region Grid       


        private void LlenarGrid()
        {
            DataSet dtsExistencias = new DataSet("Existencias");

            string sSql = string.Format(" Select IdFarmacia, Farmacia, Sum(Existencia) as Existencia " +
                " From SVR_INV_Generar_Existencia_Detallado (NoLock) " +
                " Where IdEstado = '{0}' and ClaveSSA = '{1}' and IdFarmacia <> '' " +
                " And Convert( varchar(10), FechaCaducidad, 120 )  Between '{2}' And '{3}'  " +
                " Group by IdFarmacia, Farmacia " +
                " Having Sum(Existencia) > 0 " +
                " Order by IdFarmacia ", cboEstados.Data, txtId.Text,
                General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value.AddMonths(3), "-")); //Se le agregan 3 meses a la fecha Final.

            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            Grid.Limpiar(false);
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
                cboEstados.Enabled = false;
                dtpFechaInicial.Enabled = false;
                dtpFechaFinal.Enabled = false;

                if (leer.Leer())
                {
                    bSeEncontroInformacion = true;
                    IniciaToolBar(true, false, true);
                    if (bOpcionCargada)
                    {
                        IniciaToolBar(false, false, true);
                    }

                    Grid.LlenarGrid(leer.DataSetClase);
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

        private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //Codigo.ShowDialog();
            string sFarmacia = Grid.GetValue(e.Row + 1, 1);
            if (sFarmacia != "")
            {
                Codigo = new FrmCaducarPorClaveSSA_EstadoFarmaciasCodigos();
                Codigo.MostrarDetalle(cboEstados.Data, sFarmacia, txtId.Text, dtpFechaInicial.Text, dtpFechaFinal.Text );
            }
        }
        #endregion Grid        

        public void MostrarDetalle(string IdEstado, string ClaveInternaSal, string FechaInicial, string FechaFinal )
        {
            btnNuevo_Click(this, null);
            bOpcionCargada = true;
            btnNuevo.Enabled = false;

            cboEstados.Data = IdEstado;
            cboEstados.Enabled = false;

            dtpFechaInicial.Value = DateTime.Parse( FechaInicial);
            dtpFechaFinal.Value = DateTime.Parse(FechaFinal);
            dtpFechaInicial.Enabled = false;
            dtpFechaFinal.Enabled = false;

            txtId.Text = ClaveInternaSal;
            txtId_Validating(null, null);
            btnEjecutar_Click(null, null);

            this.ShowDialog();
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.ClavesSSA_Sales("txtId_KeyDown");
                if ( leer.Leer() )
                {
                    txtId.Text = leer.Campo("ClaveSSA");
                    txtId_Validating(null, null);
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
                byte[] btReporte = null;

                myRpt.RutaReporte = GnOficinaCentral.RutaReportes;
                myRpt.NombreReporte = "Central_CaducarSales_Estados_Farmacias";

                myRpt.Add("@IdEmpresa", "");
                myRpt.Add("@IdEstado", cboEstados.Data);
                myRpt.Add("@IdFarmacia", "");
                myRpt.Add("@IdClaveSSA_Sal", txtId.Text);
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
            if (ValidaDatos())
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
            txtId.Enabled = Activar;
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

            if (bRegresa && txtId.Text == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese una Clave Interna valida por favor.");
                txtId.Focus();
            }


            return bRegresa;
        }
        #endregion Funciones 


    }//LLAVES DE LA CLASE
}
