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
    public partial class FrmExistenciaPorClaveSSA_Estado : FrmBaseExt
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
        FrmExistenciaPorClaveSSA_EstadoFarmaciasCodigos FarmaciasCodigos;

        private bool bOpcionCargada = false;
        string sFormato = "#,###,###,##0";

        //Manejo de Hilos
        Thread _workerThread;
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        public FrmExistenciaPorClaveSSA_Estado()
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

        private void FrmExistenciaPorClaveSSA_Load(object sender, EventArgs e)
        {
            if (!bOpcionCargada)
            {
                btnNuevo_Click(null, null);
            }
        }

        #region Buscar Clave 
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            // string sSql = "";

            if (txtId.Text.Trim() == "")
            {
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

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.ClavesSSA_Sales("txtId_KeyDown");
                if (leer.Leer())
                {
                    // txtId.Enabled = false;
                    txtId.Text = leer.Campo("ClaveSSA");
                    lblClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblDes.Text = leer.Campo("Descripcion");
                }
            }
        }
        #endregion Buscar Clave        

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

            rdoRptTodos.Focus();
            rdoRptTodos.Checked = true;     
            query.MostrarMsjSiLeerVacio = true;
        }
        #endregion Limpiar 

        #region Grid 
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

            if (txtId.Text.Trim() == "")
            {
                General.msjUser("No ha capturado la Clave SSA a consultar.");
                ActivarControles(true);
                IniciaToolBar(true, true, true);
                txtId.Focus();
            }
            else
            {
                string sSql = string.Format(" Select IdEstado, Estado, Sum(Existencia) as Existencia " +
                    " From SVR_INV_Generar_Existencia_Concentrado (NoLock) " +
                    " Where ClaveSSA = '{0}' and IdFarmacia <> '' {1} " +
                    " Group by IdEstado, Estado " +
                    " Order by IdEstado ", txtId.Text, sExistencia );

                ActivarControles(false);
                Grid.Limpiar(false);
                myGrid.Limpiar(false);

                tmExistencias.Stop();
                tmExistencias.Enabled = false;

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "");
                    General.msjError("Ocurrió un error al obtener la información de existencias.");
                }
                else
                {
                    Grid.LlenarGrid(leer.DataSetClase);
                    ObtenerExistenciaEstados();
                    LlenarGridFarmacias(1);

                    if (leer.Leer())
                    {
                        if (!bOpcionCargada)
                        {
                            IniciaToolBar(true, false, true);
                        }
                        tmExistencias.Start();
                        tmExistencias.Enabled = true;                        
                    }
                    else
                    {
                        IniciaToolBar(true, true, false);
                        ActivarControles(true);
                        General.msjUser("No se encontro información para mostrar bajo los criterios seleccionados.");
                    }
                }
                lblTotal.Text = Grid.TotalizarColumna(3).ToString(sFormato);
            }

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

            string sSql = string.Format(" Select IdEstado, IdFarmacia, Farmacia, Sum(Existencia) as Existencia " +
                " From SVR_INV_Generar_Existencia_Concentrado (NoLock) " +
                " Where ClaveSSA = '{0}' and IdFarmacia <> '' {1} " +
                " Group by IdEstado, IdFarmacia, Farmacia " +
                " Order by IdEstado, IdFarmacia ", txtId.Text, sExistencia );
            
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
                string sEstado = Grid.GetValue(Renglon, 1);
                myGrid.AgregarRenglon(leer.DataTableClase.Select(string.Format("IdEstado = '{0}'", sEstado)), 4, false);
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

            if (rdoRptTodos.Checked)
                iTipoRpt = 0; 

            if (rdoRptConExist.Checked)
                iTipoRpt = 1; 

            if (rdoRptSinExist.Checked)
                iTipoRpt = 2;

            string sEstado = Grid.GetValue(e.Row + 1, 1);
            if (sEstado != "")
            {
                Codigo = new FrmExistenciaPorClaveSSA_EstadoFarmacias();
                Codigo.MostrarDetalle(sEstado, txtId.Text, iTipoRpt );
            }
        }

        private void grdExistenciaFarmacias_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sEstado = Grid.GetValue(Grid.ActiveRow, 1);
            string sFarmacia = myGrid.GetValue(e.Row + 1, 2);
            int iTipoRpt = 0;

            if (rdoRptTodos.Checked) 
                iTipoRpt = 0;
             
            if (rdoRptConExist.Checked)
                iTipoRpt = 1; 

            if (rdoRptSinExist.Checked)
                iTipoRpt = 2;


            if (sEstado != "" && sFarmacia != "")
            {
                FarmaciasCodigos = new FrmExistenciaPorClaveSSA_EstadoFarmaciasCodigos();
                FarmaciasCodigos.MostrarDetalle(sEstado, sFarmacia, txtId.Text, iTipoRpt);
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
                myRpt.NombreReporte = "Central_ExistenciasSales_Estados";

                myRpt.Add("@IdEmpresa", "");
                myRpt.Add("@IdEstado", "");
                myRpt.Add("@IdFarmacia", "");
                myRpt.Add("@IdClaveSSA_Sal", txtId.Text);
                myRpt.Add("@IdProducto", "");
                myRpt.Add("@CodigoEAN", "");
                myRpt.Add("@Mostrar", 1);
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
            rdoRptTodos.Enabled = Activar;
            rdoRptConExist.Enabled = Activar;
            rdoRptConExist.Enabled = Activar;
            txtId.Enabled = Activar;
            //grdExistencia.Enabled = Activar;
        }

        public void MostrarDetalle(string ClaveInternaSal, int iTipoRpt)
        {
            if (iTipoRpt == 0)
                rdoRptTodos.Checked = true;
            if (iTipoRpt == 1)
                rdoRptConExist.Checked = true;
            if (iTipoRpt == 2)
                rdoRptSinExist.Checked = true;

            bOpcionCargada = true;
            IniciaToolBar(false, false, false);

            txtId.Text = ClaveInternaSal;
            txtId_Validating(null, null);

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
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                }
            }
        }
        #endregion Funciones

    } //LLAVES DE LA CLASE
}
