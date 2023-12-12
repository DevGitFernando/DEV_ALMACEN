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
    public partial class FrmKardexProductoGral : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid myGrid;
        clsAyudas ayuda;

        DataSet dtsFarmacias;

        clsDatosCliente DatosCliente;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb; // = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();

        //Manejo de Hilos
        Thread _workerThread;
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        public FrmKardexProductoGral()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdMovtos, this);
            myGrid.EstiloGrid(eModoGrid.SeleccionSimple);

            // cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            DatosCliente = new clsDatosCliente(GnOficinaCentral.DatosApp, this.Name, "");
            conexionWeb = new OficinaCentral.wsOficinaCentral.wsCnnOficinaCentral();
            //conexionWeb.Url = General.Url;

        }

        private void FrmKardexDeProducto_Load(object sender, EventArgs e)
        {
            CargarTiposReportes();
            CargarEstadosFarmacias();

            LimpiarPantalla();
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            myGrid.Limpiar(false);
            Fg.IniciaControles();

            dtpFechaInicial.MinDate = DtGeneral.FechaMinimaSistema;
            dtpFechaFinal.MinDate = DtGeneral.FechaMinimaSistema;

            dtpFechaInicial.MaxDate = dtpFechaInicial.Value;
            dtpFechaFinal.MaxDate = dtpFechaFinal.Value;

            ActivarControles(true);
            IniciaToolBar(true, true, false);

            txtClaveSSA.Enabled = false;
            txtCodigo.Enabled = false;
            txtCodigoEAN.Enabled = false; 
            cboEstados.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

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

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "ObteniendoExistecias";
            _workerThread.Start();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //if (validarImpresion())
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                byte[] btReporte = null;

                myRpt.RutaReporte = GnOficinaCentral.RutaReportes;
                myRpt.NombreReporte = "PtoVta_KardexDeProducto";

                myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("IdProducto", txtCodigo.Text);
                myRpt.Add("FechaInicial", dtpFechaInicial.Value);
                myRpt.Add("FechaFinal", dtpFechaFinal.Value);
                myRpt.Add("ImpresoPor", DtGeneral.IdPersonal + " - " + DtGeneral.NombrePersonal);

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

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodigo.Text.Trim() != "")
            {
                string sSql = string.Format("Select * From vw_Productos (NoLock) Where IdProducto = '{0}' ", Fg.PonCeros(txtCodigo.Text, 8));
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtCodigo_Validating");
                    General.msjError("Ocurrió un error al obtener la información del producto.");
                }
                else
                {
                    if (!leer.Leer())
                    {
                        General.msjUser("Clave de Producto no encontrada, verifique.");
                    }
                    else
                    {
                        txtCodigo.Text = leer.Campo("IdProducto");
                        lblDescripcion.Text = leer.Campo("Descripcion");
                        txtClaveSSA.Text = leer.Campo("ClaveSSA");
                        lblDescripcionSSA.Text = leer.Campo("DescripcionSal");
                    }
                }
            }
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Productos("txtCodigo_KeyDown");
                if (ayuda.ExistenDatos )
                {
                    leer.Leer();
                    txtCodigo.Text = leer.Campo("IdProducto");
                    txtCodigo_Validating(null, null);
                }
            }
        }

        private void dtpFechaFinal_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaFinal.Value < dtpFechaInicial.Value)
                dtpFechaFinal.Value = dtpFechaInicial.Value;
        }

        private void dtpFechaInicial_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaFinal.Value < dtpFechaInicial.Value)
                dtpFechaFinal.Value = dtpFechaInicial.Value;
        }

        private void dtpFechaInicial_Validating(object sender, CancelEventArgs e)
        {
            if (dtpFechaFinal.Value < dtpFechaInicial.Value)
                dtpFechaFinal.Value = dtpFechaInicial.Value;
        }

        private void dtpFechaFinal_Validating(object sender, CancelEventArgs e)
        {
            if (dtpFechaFinal.Value < dtpFechaInicial.Value)
                dtpFechaFinal.Value = dtpFechaInicial.Value;
        }


        #region Funciones y Procedimientos 
        private void IniciaToolBar(bool Nuevo, bool Ejecutar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }

        private void ActivarControles(bool Activar)
        {
            cboEstados.Enabled = Activar;
            cboFarmacias.Enabled = Activar;
            txtCodigo.Enabled = Activar;
            txtCodigoEAN.Enabled = Activar;
            cboTipoDeReporte.Enabled = Activar;
            dtpFechaInicial.Enabled = Activar;
            dtpFechaFinal.Enabled = Activar;
        }

        private void CargarTiposReportes()
        {
            cboTipoDeReporte.Clear();
            cboTipoDeReporte.Add("0", "<< Seleccione >>");
            cboTipoDeReporte.Add("1", "Codigo Interno");
            cboTipoDeReporte.Add("2", "Codigo EAN");
            cboTipoDeReporte.SelectedIndex = 0;

        }

        private void CargarEstadosFarmacias()
        {
            string sSql = " Select Distinct IdEstado, Estado From vw_Farmacias (NoLock) Order by IdEstado ";

            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEstadosFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de estados.");
            }
            else
            {
                cboEstados.Add(leer.DataSetClase, true);

                sSql = " Select Distinct IdEstado, IdFarmacia, Farmacia, ( IdFarmacia + ' -- ' +Farmacia ) as NombreFarmacia From vw_Farmacias (NoLock) Order by IdFarmacia ";
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CargarEstadosFarmacias()");
                    General.msjError("Ocurrió un error al obtener la lista de farmacias.");
                }
                else
                {
                    dtsFarmacias = leer.DataSetClase;
                }
            }

            cboEstados.SelectedIndex = 0;
            cboFarmacias.SelectedIndex = 0;
        }

        private void ObtenerInformacion()
        {
            string sTablaExistencia = " vw_Kardex_Producto ";
            string sWhereCodEAN = "";

            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            if (cboTipoDeReporte.Data == "2")
            {
                sTablaExistencia = " vw_Kardex_ProductoCodigoEAN ";
                sWhereCodEAN = string.Format(" and CodigoEAN = '{0}' ", txtCodigoEAN.Text);
            }

            string sFechas = string.Format(" Convert(varchar(10), FechaSistema, 120) Between '{0}' and '{1}' ",
                General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

            string sSql = string.Format("Select Convert(varchar(10), FechaSistema, 120) as Fecha, Folio, " +
                " DescMovimiento, Entrada, Salida, Existencia, Costo, Importe " +
                " From {4} (NoLock) " +
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdProducto = '{2}' and {3} {5} " +
                " Order By FechaSistema ", cboEstados.Data, cboFarmacias.Data,
                Fg.PonCeros(txtCodigo.Text, 8), sFechas, sTablaExistencia, sWhereCodEAN);

            ActivarControles(false);
            myGrid.Limpiar(false);
            if (!leer.Exec(sSql))
            {
                bSeEncontroInformacion = false;
                IniciaToolBar(true, true, false);
                ActivarControles(true);
                Error.GrabarError(leer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al obtener la información de movimientos.");
            }
            else
            {
                if (leer.Leer())
                {
                    IniciaToolBar(true, false, true);
                    bSeEncontroInformacion = true;
                    myGrid.LlenarGrid(leer.DataSetClase);
                }
                else
                {
                    bSeEncontroInformacion = false;
                    IniciaToolBar(true, true, false);
                    ActivarControles(true);
                    //General.msjUser("No se encontro información para los criterios especificados.");
                }
            }

            bSeEjecuto = true;
            bEjecutando = false; // Cursor.Current
            this.Cursor = Cursors.Default;
        }
        #endregion Funciones y Procedimientos

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
                catch ( Exception ex )
                {
                    ex.Source = ex.Source;
                }
            }

            cboFarmacias.SelectedIndex = 0;
        }

        private void txtCodigoEAN_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "";

            ////if (txtCodigo.Text.Trim() == "")
            ////{
            ////    General.msjUser("Falta capturar la clave de Codigo Interno.");
            ////    // e.Cancel = true;
            ////}
            ////else
            {
                if (txtCodigoEAN.Text.Trim() == "")
                {
                    //txtCodEAN.Focus();
                }
                else
                {
                    sSql = string.Format(" Select * From CatProductos_CodigosRelacionados (NoLock) " +
                        " Where CodigoEAN = '{0}' ", txtCodigoEAN.Text);

                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "txtCodEAN_Validating");
                        General.msjError("Ocurrió un error al obtener la información.");
                    }
                    else
                    {
                        if (leer.Leer())
                        {
                            txtCodigo.Text = leer.Campo("IdProducto");
                            txtCodigo_Validating(null, null);
                        }
                        else
                        {
                            General.msjUser("Codigo EAN no encontrado, verifique.");
                            // e.Cancel = true;
                        }
                    }
                }
            }
        }

        private void cboTipoDeReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            myGrid.Limpiar(false);

            txtCodigo.Enabled = false;
            txtCodigoEAN.Enabled = false;

            txtClaveSSA.Text = "";
            lblDescripcionSSA.Text = "";
            txtCodigo.Text = "";
            lblDescripcion.Text = "";
            txtCodigoEAN.Text = "";

            if (cboTipoDeReporte.Data == "1")
            {
                txtCodigo.Enabled = true;
                txtCodigo.Focus();
            }

            if (cboTipoDeReporte.Data == "2")
            {
                txtCodigoEAN.Enabled = true;
                txtCodigoEAN.Focus();
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
                        ActivarControles(true);
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                    }
                }
            }
        }


    }//LLAVES DE LA CLASE
}
