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
    public partial class FrmExistenciaPorEstadoFarmacia : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        clsConsultas query;
        clsAyudas ayuda;
        DataSet dtsFarmacias;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();

        FrmExistenciaPorClaveSSA_EstadoFarmaciasCodigos Codigo;
        private bool bOpcionCargada = false;

        string sFormato = "#,###,###,##0";
        Thread _workerThread;

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        public FrmExistenciaPorEstadoFarmacia()
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

            return bRegresa;
        }

        private void LlenarGrid()
        {
            string sExistencia = "";

            if (rdoRptTodos.Checked)
                sExistencia = "AND Existencia >= 0"; 

            if (rdoRptConExist.Checked)
                sExistencia = " AND Existencia > 0 "; 

            if (rdoRptSinExist.Checked)
                sExistencia = " AND Existencia = 0 ";

            if (validarDatos())
            {
                bEjecutando = true;
                this.Cursor = Cursors.WaitCursor;


                string sSql = string.Format(" Select '' as IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Sum(Existencia) as Existencia " +
                    " From SVR_INV_Generar_Existencia_Concentrado (NoLock) " +
                    " Where IdEstado = '{0}' and IdFarmacia = '{1}'  {2}  " +
                    " Group by ClaveSSA, DescripcionSal " +
                    " Order by ClaveSSA ", cboEstados.Data, cboFarmacias.Data, sExistencia);

                ActivarControles(false);
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
                        if (!bOpcionCargada)
                        {
                            IniciaToolBar(true, false, true);
                        }

                        cboEstados.Enabled = false;
                        cboFarmacias.Enabled = false;
                        Grid.LlenarGrid(leer.DataSetClase);
                        bSeEncontroInformacion = true;
                    }
                    else
                    {
                        IniciaToolBar(true, true, false);
                        ActivarControles(true);
                        bSeEncontroInformacion = false;
                        //General.msjUser("No se encontro información para mostrar bajo los criterios seleccionados.");
                    }
                }
                lblTotal.Text = Grid.TotalizarColumna(4).ToString(sFormato);

                bSeEjecuto = true;
                bEjecutando = false; // Cursor.Current

                this.Cursor = Cursors.Default;
            }
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

            string sDato = Grid.GetValue(e.Row + 1, 1);
            Codigo = new FrmExistenciaPorClaveSSA_EstadoFarmaciasCodigos();
            Codigo.MostrarDetalle(cboEstados.Data, cboFarmacias.Data, sDato, iTipoRpt);
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
                myRpt.NombreReporte = "Central_ExistenciasSales_Farmacias";

                myRpt.Add("@IdEmpresa", "");
                myRpt.Add("@IdEstado", cboEstados.Data);
                myRpt.Add("@IdFarmacia", cboFarmacias.Data);
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

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
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

                if (bOpcionCargada)
                {
                    IniciaToolBar(false, false, true);
                }
            }
        }

        private void cboEstados_Validating(object sender, CancelEventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
                cboEstados.Enabled = false;
        }

        private void CargarFarmacias()
        {
            dtsFarmacias = new DataSet();

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            cboFarmacias.SelectedIndex = 0;

            dtsFarmacias = query.Farmacias_Status("CargarFarmacias()", "");
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            if (cboEstados.SelectedIndex != 0)
            {
                try
                {
                    cboFarmacias.Add(dtsFarmacias.Tables[0].Select(string.Format("IdEstado = '{0}'", cboEstados.Data)), true, "IdFarmacia", "NombreFarmacia");
                }
                catch { }
            }
            cboFarmacias.SelectedIndex = 0;
        }

        private void cboFarmacias_Validating(object sender, CancelEventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
                cboFarmacias.Enabled = false;
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                //FrameListaReportes.Enabled = true;
                //btnEjecutar.Enabled = true;
                //btnNuevo.Enabled = true;

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
            cboFarmacias.Enabled = Activar;
            //grdExistencia.Enabled = Activar;
        }


    }//LAVES DE LA CLASE
}
