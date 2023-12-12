using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.ExportarDatos;

// Implementacion de hilos 
using System.Threading;

using DllFarmaciaSoft; 

namespace DllAdministracion.Reportes
{
    public partial class FrmProductosMovimientos : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leerColumnas;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        clsDatosCliente DatosCliente;
        wsAdministracion.wsCnnOficinaCentral conexionWeb;
        DataSet dtsEstados = new DataSet();
        DataSet dtsFarmacias = new DataSet();
        clsListView lst, lstG;
        clsExportarExcelPlantilla xpExcel;
        DataSet dtsMovimientos = new DataSet();
        DataSet dtsGlosario = new DataSet();

        Thread _workerThread;
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;


        private enum Cols
        {
            Ninguna = 0,
            IdFarmacia = 2, Farmacia = 3, IdSubFarmacia = 4, SubFarmacia = 5, IdClaveSSA = 6, ClaveSSA = 7,
            DescripcionSal = 8, IdProducto = 9, CodigoEAN = 10, ClaveLote = 11, EsConsignacion = 12, FechaCaducidad = 13,
            MesesPorCaducar = 14, FechaRegistro = 15, DescripcionProducto = 16, IdPresentacion = 17, Presentacion = 18, ContenidoPaquete = 19,
            Existencia = 20, CostoInicial = 21, II = 22, IIC = 23 
        }

        private enum Cols_Glosario
        {
            Ninguna = 0,
            Movimiento = 2, Descripcion = 3, Efecto = 4
        }

        public FrmProductosMovimientos()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            leer = new clsLeer(ref ConexionLocal);
            leerColumnas = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnAdministracion.Modulo, this.Name, GnAdministracion.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnAdministracion.Modulo, this.Name, GnAdministracion.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnAdministracion.Modulo, GnAdministracion.Version, this.Name);

            DatosCliente = new clsDatosCliente(GnAdministracion.DatosApp, this.Name, "");
            conexionWeb = new DllAdministracion.wsAdministracion.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;

            lst = new clsListView(lstMovimientos);
            lst.OrdenarColumnas = false;

            lstG = new clsListView(lstGlosario);
            lstG.OrdenarColumnas = false;

            LlenarEmpresas();
            ObtenerFarmacias();
        }

        private void FrmProductosMovimientos_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(true);
            rdoAmbos.Checked = true;
            tabMovimientos.SelectTab(0);
            lst.Limpiar();

            IniciaToolBar(true, true, false);
            ActivarControles(true);            
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false;

            ActivarControles(false);

            bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoMovimientos";
            _workerThread.Start();

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            GenerarExcel();
        }

        #endregion Botones

        #region Funciones y Eventos 
        private void IniciaToolBar(bool Nuevo, bool Ejecutar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }

        private void LlenarEmpresas()
        {
            cboEmpresas.Clear();
            cboEmpresas.Add();

            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            string sSql = "Select Distinct IdEmpresa, NombreEmpresa From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEmpresas()");
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
            else
            {
                cboEmpresas.Add(leer.DataSetClase, true, "IdEmpresa", "NombreEmpresa");
                sSql = "Select distinct IdEstado, NombreEstado, IdEmpresa, StatusEdo From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "LlenarEmpresas()");
                    General.msjError("Ocurrió un error al obtener la lista de Estados por Empresas.");
                }
                else
                {
                    dtsEstados = leer.DataSetClase;
                }

            }
            cboEmpresas.SelectedIndex = 0;
        }

        private void LlenarEstados()
        {
            string sFiltro = string.Format(" IdEmpresa = '{0}' and StatusEdo = '{1}' ", cboEmpresas.Data, "A");
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.Add(dtsEstados.Tables[0].Select(sFiltro), true, "IdEstado", "NombreEstado");
            cboEstados.SelectedIndex = 0;
        }

        private void ObtenerInformacion()
        {
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            int iTipoDispensacion = 0, iMostrarResultado = 1;
            string sFarmacia = "*";

            if (ValidaControles())
            {
                ActivarControles(false);
                if (cboFarmacias.Data != "0")
                {
                    sFarmacia = cboFarmacias.Data;
                }

                if (!rdoAmbos.Checked)
                {
                    if (rdoVenta.Checked)
                    {
                        iTipoDispensacion = 1;
                    }
                    else
                    {
                        iTipoDispensacion = 2;
                    }
                }

                cboFarmacias.Enabled = false;
                dtpFechaInicial.Enabled = false;
                dtpFechaFinal.Enabled = false;

                string sSql = string.Format(" Exec spp_ADMI_Impresion_Productos_Movimientos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                    cboEmpresas.Data, cboEstados.Data, sFarmacia,
                    General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), iTipoDispensacion,
                    iMostrarResultado);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "");
                    General.msjError("Ocurrió un error al obtener la información de los Movimientos.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        IniciaToolBar(true, false, true);
                        dtsMovimientos = leer.DataSetClase;
                        lst.CargarDatos(leer.DataSetClase, true, true);
                        lst.AlternarColorRenglones(Color.Lavender, Color.LightBlue);
                        ObtenerGlosario();
                    }
                    else
                    {
                        IniciaToolBar(true, true, false);
                        ActivarControles(true);
                        General.msjUser("No se encontro información para mostrar.");
                    }
                }
            }

            bEjecutando = false;
            this.Cursor = Cursors.Default;
        }
        private void ObtenerFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< TODAS LAS FARMACIAS >>");
            cboFarmacias.SelectedIndex = 0;

            dtsFarmacias = Consultas.Farmacias("", "", "ObtenerFarmacias()");
        }

        private void ObtenerGlosario()
        {
            string sSql = "Select IdTipoMovto_Inv as Movimiento, Descripcion, Efecto From tmpGlosarioMovimientos(NoLock) Order By IdTipoMovto_Inv ";

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener el Glosario de los Movimientos.");
            }
            else
            {
                if (leer.Leer())
                {
                    dtsGlosario = leer.DataSetClase;
                    lstG.CargarDatos(leer.DataSetClase, true, true);
                    lstG.AlternarColorRenglones(Color.Lavender, Color.LightBlue);
                }
            }
        }
        

        private void LlenarFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< TODAS LAS FARMACIAS >>");
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

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEmpresas.SelectedIndex != 0)
            {
                LlenarEstados();
            } 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                LlenarFarmacias();
            }
        }

        private void ActivarControles(bool Activar)
        {
            cboEmpresas.Enabled = Activar;
            cboEstados.Enabled = Activar;
            cboFarmacias.Enabled = Activar;
            dtpFechaInicial.Enabled = Activar;
            dtpFechaFinal.Enabled = Activar;
            rdoAmbos.Enabled = Activar;
            rdoVenta.Enabled = Activar;
            rdoConsignacion.Enabled = Activar;
        }

        private void GenerarExcel()
        {
            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\ADMIN_Movimientos_Productos.xls";
            bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "ADMIN_Movimientos_Productos.xls", DatosCliente);

            if (bRegresa)
            {
                xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                xpExcel.AgregarMarcaDeTiempo = true;
                leer.DataSetClase = dtsMovimientos;

                this.Cursor = Cursors.Default;
                if (xpExcel.PrepararPlantilla())
                {
                    this.Cursor = Cursors.WaitCursor;
                    ExportarMovimientos();
                    ExportarGlosario();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }
                }
                this.Cursor = Cursors.Default;
            } 
        }

        private void ExportarMovimientos()
        {
            int iHoja = 1, iRenglon = 9;
            int iColumnaActual = 0, iColumna = 22;
            string sPeriodo = "", sNombreColumna = ""; ;

            leer.DataSetClase = dtsMovimientos;
            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(leer.Campo("Empresa"), 2, 2);
            xpExcel.Agregar(leer.Campo("Estado"), 3, 2);

            sPeriodo = string.Format("Reporte de Movimientos de Productos del {0} al {1} ", leer.Campo("FechaInicial"), leer.Campo("FechaFinal"));
            xpExcel.Agregar(sPeriodo, 4, 2);

            xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);

            AgregarColumnasDinamicas();
            while (leer.Leer())
            {
                xpExcel.Agregar(leer.Campo("IdFarmacia"), iRenglon, (int)Cols.IdFarmacia);
                xpExcel.Agregar(leer.Campo("Farmacia"), iRenglon, (int)Cols.Farmacia);
                xpExcel.Agregar(leer.Campo("IdSubFarmacia"), iRenglon, (int)Cols.IdSubFarmacia);
                xpExcel.Agregar(leer.Campo("SubFarmacia"), iRenglon, (int)Cols.SubFarmacia);
                xpExcel.Agregar(leer.Campo("IdClaveSSA_Sal"), iRenglon, (int)Cols.IdClaveSSA);
                xpExcel.Agregar(leer.Campo("ClaveSSA"), iRenglon, (int)Cols.ClaveSSA);
                xpExcel.Agregar(leer.Campo("DescripcionSal"), iRenglon, (int)Cols.DescripcionSal);
                xpExcel.Agregar(leer.Campo("IdProducto"), iRenglon, (int)Cols.IdProducto);
                xpExcel.Agregar(leer.Campo("CodigoEAN"), iRenglon, (int)Cols.CodigoEAN);
                xpExcel.Agregar(leer.Campo("ClaveLote"), iRenglon, (int)Cols.ClaveLote);
                xpExcel.Agregar(leer.CampoInt("EsConsignacion"), iRenglon, (int)Cols.EsConsignacion);
                xpExcel.Agregar(leer.CampoFecha("FechaCaducidad"), iRenglon, (int)Cols.FechaCaducidad);
                xpExcel.Agregar(leer.CampoInt("MesesPorCaducar"), iRenglon, (int)Cols.MesesPorCaducar);
                xpExcel.Agregar(leer.CampoFecha("FechaRegistro"), iRenglon, (int)Cols.FechaRegistro);
                xpExcel.Agregar(leer.Campo("DescripcionProducto"), iRenglon, (int)Cols.DescripcionProducto);
                xpExcel.Agregar(leer.Campo("IdPresentacion"), iRenglon, (int)Cols.IdPresentacion);
                xpExcel.Agregar(leer.Campo("Presentacion"), iRenglon, (int)Cols.Presentacion);
                xpExcel.Agregar(leer.Campo("ContenidoPaquete"), iRenglon, (int)Cols.ContenidoPaquete);
                xpExcel.Agregar(leer.Campo("Existencia"), iRenglon, (int)Cols.Existencia);
                xpExcel.Agregar(leer.Campo("CostoInicial"), iRenglon, (int)Cols.CostoInicial);
                xpExcel.Agregar(leer.Campo("II"), iRenglon, (int)Cols.II);
                xpExcel.Agregar(leer.Campo("IIC"), iRenglon, (int)Cols.IIC);

                iColumnaActual = 0;
                iColumna = 22;
                //Se agregan los campos dinamicos.
                foreach (DataColumn dtCol in dtsMovimientos.Tables[0].Columns)
                {
                    iColumnaActual++;

                    if (iColumnaActual >= 28)
                    {
                        sNombreColumna = dtCol.ColumnName;
                        xpExcel.Agregar(leer.Campo(sNombreColumna), iRenglon, iColumna);
                        iColumna++;
                    }
                }

                iRenglon++;
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();
 
        }

        private void ExportarGlosario()
        {
            int iHoja = 2, iRenglon = 7;
            string sPeriodo = "", sNombreColumna = "";

            leer.DataSetClase = dtsGlosario;
            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(cboEmpresas.Text, 2, 2);
            xpExcel.Agregar(cboEstados.Text, 3, 2);            

            while (leer.Leer())
            {
                xpExcel.Agregar(leer.Campo("Movimiento"), iRenglon, (int)Cols_Glosario.Movimiento);
                xpExcel.Agregar(leer.Campo("Descripcion"), iRenglon, (int)Cols_Glosario.Descripcion);
                xpExcel.Agregar(leer.Campo("Efecto"), iRenglon, (int)Cols_Glosario.Efecto);
                iRenglon++;
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();
        }

        private void AgregarColumnasDinamicas()
        {
            int iColumnaActual = 0;
            int iColumna = 22;
            string sNombre = "";

            foreach (DataColumn dtCol in dtsMovimientos.Tables[0].Columns)
            {
                iColumnaActual++;

                if (iColumnaActual >= 28)
                {
                    sNombre = dtCol.ColumnName;
                    xpExcel.Agregar(sNombre, 8, iColumna);
                    iColumna++;
                }
            }

        }

        private bool ValidaControles()
        {
            bool bRegresa = true;

            if (cboEmpresas.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione una Empresa");
            }

            if (bRegresa && cboEstados.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione el Estado");
            }

            return bRegresa;
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnNuevo.Enabled = true;
                btnEjecutar.Enabled = false;

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

        #endregion Funciones y Eventos

        

    } //Llaves de la clase
}
