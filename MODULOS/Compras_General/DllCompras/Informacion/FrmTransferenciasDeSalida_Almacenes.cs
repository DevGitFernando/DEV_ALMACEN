using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem; 
using SC_SolutionsSystem.Data; 
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.wsConexion;
using SC_SolutionsSystem.FuncionesGenerales;
//using SC_SolutionsSystem.ExportarDatos; 

using DllFarmaciaSoft;
using DllFarmaciaSoft.wsFarmacia;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;


namespace DllCompras.Informacion
{
    public partial class FrmTransferenciasDeSalida_Almacenes : FrmBaseExt
    {
        clsConexionSQL cnn; //= new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeerWebExt leerWeb;
        clsLeer leer;

        clsDatosConexion DatosDeConexion;
        wsCnnCliente conexionWeb;
        
        clsAyudas ayuda;
        clsConsultas query;
        clsGrid grid;
        clsListView lst; 

        clsLeerWebExt myWeb;

        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;
        bool bExisteInformacion = false;
        bool bSeEjecuto = false;

        string sSqlFarmacias = "";
        string sHost = "";
        DataSet dtsDatosOC;
        //DataSet dtsFoliosEntradasDet = new DataSet();

        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;
        clsDatosCliente DatosCliente;

        DataSet dtsEstados = new DataSet();
        DataSet dtsFarmacias = new DataSet(); 

        private enum Cols
        {
            Ninguna = 0,
            IdFarmacia = 1, Farmacia = 2, Url = 3, Servidor = 4, Folio = 5, Fecha = 6, Consultar = 7, Status = 8,
            FolioEntrada = 9, FechaEntrada = 10
        }

        private enum Cols_Exportar
        {
            Ninguna = 0,
            IdFarmacia = 2, Farmacia = 3, Folio = 4, Fecha = 5, Status = 6,
            FolioEntrada = 7, FechaEntrada = 8
        }

        public FrmTransferenciasDeSalida_Almacenes()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, ""); 

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            conexionWeb = new wsCnnCliente();
            conexionWeb.Url = General.Url;
            dtsDatosOC = new DataSet();
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");           

            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, datosCliente);
            leer = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(); 
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            lst = new clsListView(lstTransferencias); 

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);  

            Cargar_Empresas();
            Cargar_Estados();
            CargarFarmaciasDestino(); 
        }

        #region Botones 
        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                ObtenerInformacion();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grpDatos.Enabled = true;
            grpFechas.Enabled = true;

            Fg.IniciaControles(this, true); 
            iBusquedasEnEjecucion = 0;
            bExisteInformacion = false;
            bSeEjecuto = false; 
            btnExportar.Enabled = false;
            lst.Limpiar(); 

            grpDatos.Enabled = true;
            if (DtGeneral.Modulo_Compras_EnEjecucion != TipoModuloCompras.Central)
            {
                cboEmpresas.Data = DtGeneral.EmpresaConectada;
                cboEdo.Data = DtGeneral.EstadoConectado;

                if (!DtGeneral.EsAdministrador)
                {
                    cboEmpresas.Enabled = false;
                    cboEdo.Enabled = false;
                }
            }
        }
        #endregion Botones

        #region Eventos 
        private bool validarDatos()
        {
            bool bRegresa = true;

            if (cboEmpresas.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una empresa válida, verifique."); 
                cboEmpresas.Focus(); 
            }

            if (bRegresa && cboEdo.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un estado válido, verifique.");
                cboEdo.Focus();
            }

            if (bRegresa && cboFarmaciasDestino.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un almacén válido, verifique.");
                cboFarmaciasDestino.Focus();
            }

            return bRegresa; 
        }

        private void ObtenerInformacion()
        {
            iBusquedasEnEjecucion = 1;
            bSeEjecuto = false;
            bExisteInformacion = false; 

            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false; 
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start(); 

            Thread _workerThread = new Thread(this.Consultar_Transferencias);
            _workerThread.Name = "Obtener_Transferencias";
            _workerThread.Start();

        }

        private void Consultar_Transferencias()
        {
            string sUrl = cboFarmaciasDestino.ItemActual.GetItem("UrlFarmacia");
            string sFiltroFecha = string.Format(" convert(varchar(10), FechaRegistro, 120) Between '{0}' and '{1}' ",
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value)); 


            string sSql = string.Format("" + 
                "Select " +
                "    'Fecha de registro' = convert(varchar(10), FechaRegistro, 120), Folio, " + 
                "    'Farmacia' = IdFarmaciaRecibe, 'Farmacia destino' = FarmaciaRecibe,  " + 
                "    IdSubFarmaciaRecibe, SubFarmaciaRecibe, " + 
                "    'Jurisdicción' = IdJurisdiccionRecibe, 'Jurisdicción destino' = JurisdiccionRecibe, " + 
                "    'Clave SSA' = ClaveSSA, 'Descripción clave ssa' = DescripcionSal, " + 
                "    'Producto' = IdProducto, 'Código EAN' = CodigoEAN, 'Nombre comercial' = DescripcionProducto, " + 
                "    'Clave lote' = ClaveLote, 'Cantidad' = CantidadLote, 'Caducidad' = convert(varchar(7), FechaCaducidad ,120)  " +
                "From vw_Impresion_Transferencias (NoLock) " +
                "Where TipoTransferencia = 'TS' and {0} and IdEmpresa = '{1}' and IdEstado = '{2}' and IdFarmacia = '{3}' ", 
                sFiltroFecha, cboEmpresas.Data, cboEdo.Data, cboFarmaciasDestino.Data);

            lst.Limpiar();
            leerExportarExcel = new clsLeer(); 

            clsLeerWebExt myWeb = new clsLeerWebExt(ref cnn, sUrl, DtGeneral.CfgIniPuntoDeVenta, datosCliente);
            if (!myWeb.Exec(sSql))
            {
                Error.GrabarError(myWeb, "Consultar_Transferencias()", ""); 
            }
            else
            {
                bSeEjecuto = true;  
                if (myWeb.Leer())
                {
                    bExisteInformacion = true;
                    leerExportarExcel.DataSetClase = myWeb.DataSetClase;
                    lst.CargarDatos(myWeb.DataSetClase, true, true); 
                }
            }

            iBusquedasEnEjecucion = 0; 
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
                btnExportar.Enabled = leerExportarExcel.Registros > 0;

                if (!bExisteInformacion)
                {
                    if (!bSeEjecuto)
                    {
                        General.msjError("Ocurrió un error al obtener la información de las transferencias de salida."); 
                    }
                    else 
                    {
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                    }
                }
            }
        }

        private void FrmSeguimientoTransferenciaSalidas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }
        #endregion Eventos

        #region Carga_Combos
        private void Cargar_Empresas()
        {
            string sSql = "";
            cboEmpresas.Clear();
            cboEmpresas.Add();


            sSql = "Select IdEmpresa, Nombre, EsDeConsignacion From CatEmpresas (NoLock) Where Status = 'A' Order by IdEmpresa ";
            if (leer.Exec(sSql))
            {
                cboEmpresas.Add(leer.DataSetClase, true, "IdEmpresa", "Nombre");
            }
            else
            {
                Error.LogError(leer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }

            cboEmpresas.SelectedIndex = 0;
        }

        private void Cargar_Estados()
        {
            string sSql = "", sEmpresa = "";

            cboEdo.Clear();
            cboEdo.Add();

            sSql = string.Format("Select IdEstado, (IdEstado + ' - ' + NombreEstado) as NombreEstado, ClaveRenapo, IdEmpresa " + 
                " From vw_EmpresasEstados (NoLock) " + 
                " Where StatusEdo = 'A' Order by IdEstado " );
            if (leer.Exec(sSql))
            {
                dtsEstados = leer.DataSetClase; 
                //cboEdo.Add(leer.DataSetClase, true, "IdEstado", "NombreEstado");
            }
            else
            {
                Error.LogError(leer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Estados.");
            }
            cboEdo.SelectedIndex = 0;
        }

        private void CargarFarmaciasDestino()
        {
            cboFarmaciasDestino.Clear();
            cboFarmaciasDestino.Add(); 
            query.MostrarMsjSiLeerVacio = false;

            leer.DataSetClase = query.AlmacenesRegionales("CargarFarmaciasDestino");

            if (leer.Leer())
            {
                dtsFarmacias = leer.DataSetClase; 
                //cboFarmaciasDestino.Add(leer.DataSetClase, true, "IdFarmacia", "Farmacia");
            }

            cboFarmaciasDestino.SelectedIndex = 0; 

            query.MostrarMsjSiLeerVacio = true; 
        }
        #endregion Carga_Combos

        #region Eventos_Combos
        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEdo.Clear();
            cboEdo.Add();

            if (cboEmpresas.SelectedIndex != 0)
            {
                cboEdo.Filtro = string.Format(" IdEmpresa = '{0}' ", cboEmpresas.Data);
                cboEdo.Add(dtsEstados, true, "IdEstado", "NombreEstado"); 
            }

            cboEdo.SelectedIndex = 0; 
        }

        private void cboEdo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmaciasDestino.Clear();
            cboFarmaciasDestino.Add();

            if (cboEdo.SelectedIndex != 0)
            {
                cboFarmaciasDestino.Filtro = string.Format(" IdEstado = '{0}' ", cboEdo.Data);
                cboFarmaciasDestino.Add(dtsFarmacias, true, "IdFarmacia", "NombreFarmacia");
            }

            cboFarmaciasDestino.SelectedIndex = 0; 
        }
        #endregion Eventos_Combos

        private void CargarDatosConexion(string Url)
        {
            conexionWeb.Url = Url;
            DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

            DatosDeConexion.Servidor = sHost;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            GenerarReporteExcel();
        }

        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = string.Format("TRANSFERENCIAS DE SALIDA DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            string sNombreFile = "Transferencias de salida CEDIS";

            leer.DataSetClase = leerExportarExcel.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombreFile))
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, cboEmpresas.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, cboEdo.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, cboFarmaciasDestino.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }


        //private void Exportar()
        //{
        //    int iRow = 2;
        //    string sNombreFile = "", sRutaReportes = "", sRutaPlantilla = "", sPeriodo = "";
        //    string sFecha = ""; 

        //    sRutaReportes = GnCompras.RutaReportes;
        //    DtGeneral.RutaReportes = sRutaReportes;

        //    sNombreFile = "Transferencias de salida CEDIS";
        //    sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_Rpt_TransferenciasDeSalida__CEDIS.xls";
        //    DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_Rpt_TransferenciasDeSalida__CEDIS.xls", DatosCliente);

        //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //    xpExcel.AgregarMarcaDeTiempo = true;

        //    if (xpExcel.PrepararPlantilla(sNombreFile))
        //    {
        //        xpExcel.GeneraExcel();

        //        xpExcel.Agregar(cboEmpresas.Text, iRow, 2);
        //        iRow++;
        //        xpExcel.Agregar(cboEdo.Text, iRow, 2);
        //        iRow++;

        //        xpExcel.Agregar(cboFarmaciasDestino.Text, iRow, 2);
        //        iRow++;


        //        sPeriodo = string.Format("TRANSFERENCIAS DE SALIDA DEL {0} AL {1} ",
        //           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
        //        xpExcel.Agregar(sPeriodo, iRow, 2);

        //        iRow = 7;
        //        xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);

        //        // Se ponen los detalles
        //        //leerExportarExcel.RegistroActual = 1;
        //        iRow = 10;

        //        leerExportarExcel.RegistroActual = 1; 
        //        while (leerExportarExcel.Leer())
        //        {
        //            ////string sSql = string.Format("" +
        //            ////                "Select " +
        //            ////                "    'Fecha de registro' = convert(varchar(10), FechaRegistro, 120), Folio, " +
        //            ////                "    'Farmacia' = IdFarmaciaRecibe, 'Farmacia destino' = FarmaciaRecibe,  " +
        //            ////                "    IdSubFarmaciaRecibe, SubFarmaciaRecibe, " +
        //            ////                "    'Jurisdicción' = IdJurisdiccionRecibe, 'Jurisdicción destino' = JurisdiccionRecibe, " +
        //            ////                "    'Clave SSA' = ClaveSSA, 'Descripción clave ssa' = DescripcionSal, " +
        //            ////                "    'Producto' = IdProducto, 'Código EAN' = CodigoEAN, 'Nombre comercial' = DescripcionProducto, " +
        //            ////                "    'Clave lote' = ClaveLote, 'Cantidad' = CantidadLote, 'Caducidad' = convert(varchar(7), FechaCaducidad ,120)  " +
        //            ////                "From vw_Impresion_Transferencias (NoLock) " +
        //            ////                "Where TipoTransferencia = 'TS' and {0} and IdEmpresa = '{1}' and IdEstado = '{2}' and IdFarmacia = '{3}' ");

        //            xpExcel.Agregar(leerExportarExcel.CampoFecha("Fecha de registro"), iRow, 2);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRow, 3);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRow, 4);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Farmacia destino"), iRow, 5);
        //            xpExcel.Agregar(leerExportarExcel.Campo("IdSubFarmaciaRecibe"), iRow, 6);
        //            xpExcel.Agregar(leerExportarExcel.Campo("SubFarmaciaRecibe"), iRow, 7);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Jurisdicción"), iRow, 8);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Jurisdicción destino"), iRow, 9);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Clave SSA"), iRow, 10);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Descripción clave ssa"), iRow, 11);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Producto"), iRow, 12);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Código EAN"), iRow, 13);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Nombre comercial"), iRow, 14);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Clave lote"), iRow, 15);
        //            xpExcel.Agregar(leerExportarExcel.CampoDouble("Cantidad"), iRow, 16);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Caducidad"), iRow, 17); 
        //            iRow++;
        //        }

        //        // Finalizar el Proceso 
        //        xpExcel.CerrarDocumento();

        //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //        {
        //            xpExcel.AbrirDocumentoGenerado();
        //        }

        //    }
        //}   
    }
}
