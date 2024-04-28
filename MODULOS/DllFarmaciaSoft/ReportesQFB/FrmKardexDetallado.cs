using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using SC_SolutionsSystem.ExportarDatos;

namespace DllFarmaciaSoft.ReportesQFB
{
    public partial class FrmKardexDetallado : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente DatosCliente;
        clsGrid myGrid;
        clsLeer leer;
        clsAyudas Ayuda;
        clsConsultas Consulta;
        DataTable dtDatosExcel;
        wsFarmacia.wsCnnCliente conexionWeb;

        string sIdClaveSSA = "";
        clsExportarExcelPlantilla xpExcel;

        int tpTipoDeKardex = 0;

        public FrmKardexDetallado(int TipoDeKardex)
        {
            InitializeComponent();

            tpTipoDeKardex = TipoDeKardex; 

            // Esperar hasta que la consulta se ejecute. 
            ConexionLocal = new clsConexionSQL();
            ConexionLocal.DatosConexion.Servidor = General.DatosConexion.Servidor;
            ConexionLocal.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            ConexionLocal.DatosConexion.Usuario = General.DatosConexion.Usuario;
            ConexionLocal.DatosConexion.Password = General.DatosConexion.Password;
            ConexionLocal.DatosConexion.Puerto = General.DatosConexion.Puerto; 

            ConexionLocal.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 




            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            //conexionWeb.Url = General.Url;
            leer = new clsLeer(ref ConexionLocal);

            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Consulta = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdMovtos, this);
            myGrid.EstiloGrid(eModoGrid.SoloLectura);
            myGrid.AjustarAnchoColumnasAutomatico = true;

            dtDatosExcel = new DataTable();

            ConfigurarInterface(); 
        }

        private void ConfigurarInterface()
        {
            rdoClavesLibres.Visible = false;

            if (tpTipoDeKardex == 1)
            {
                this.Text = "Kardex Controlados y Antibióticos";
            }

            if (tpTipoDeKardex == 2)
            {
                btnImprimir.Visible = false;
                toolStripSeparator2.Visible = false;

                this.Text = "Kardex General";
                FrameTipoSales.Left = 2000;
                FrameTipoReporte.Left = 12;
                FrameTipoReporte.Size = new Size(478, 51);
                rdoTodasClaves.Left = 50;
                rdoPorClave.Left = 200;
                rdoPorProducto.Left = 350;

                FrameFecha.Left = 498;
                FrameFecha.Size = new Size(478, 51);
                label4.Left = 75;
                dtpFechaInicial.Left = 120;
                label2.Left = 240;
                dtpFechaFinal.Left = 275;
            } 
        }

        private void FrmSalesControladosAntibioticos_Load(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

            #region Botones 

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            ObtenerInformacion();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Impresion();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            leer.DataTableClase = dtDatosExcel;

            if (leer.Registros == 0)
            {
                General.msjAviso("No existe información para exportar, verifique.");
            }
            else
            {
                ExportarExcel();
            }
        }

        private void ExportarExcel()
        {
            bool bRegresa = true;
            clsGenerarExcel excel = new clsGenerarExcel();
            string sNombreHoja = "ANTIBIOTICOS";
            string sConcepto = "";
            string sNombreDocumento = "";
            string sPNO = ""; 

            string sRutaPlantilla = ""; 
            string sAntCon = "DE ANTIBIOTICOS";
            string sCons = "";

            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;

            sNombreDocumento = string.Format("KARDEX_DE_ANTIBIOTICOS");
            if(rdoAntibioticos.Checked)
            {
                sPNO = "PN-DIS-11-1.1";
            }

            if (rdoControlados.Checked)
            {
                sAntCon = "DE CONTROLADOS";
                sNombreHoja = "CONTROLADOS";

                sNombreDocumento = string.Format("KARDEX_DE_CONTROLADOS");
            }

            if (tpTipoDeKardex == 2)
            {
                sAntCon = "GENERAL";
                sNombreHoja = "GENERAL";

                sNombreDocumento = string.Format("KARDEX_DE_GENERAL");
            }

            leer.DataTableClase = dtDatosExcel;
            if (leer.Registros == 0)
            {
                General.msjAviso("No existe información para exportar, verifique.");
            }
            else
            {
                this.Cursor = Cursors.WaitCursor; 
                sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Kardex_Antibioticos_Controlados_Farmacia.xlsx";


                sConcepto = string.Format("REPORTE DE MOVIMIENTOS {0} DEL {1} AL {2}",
                    sAntCon, General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));


                //for (int i = 0; i <= 60; i++)
                {

                    excel = new clsGenerarExcel(); 
                    excel.RutaArchivo = @"C:\\Excel";
                    excel.NombreArchivo = sNombreDocumento;
                    excel.AgregarMarcaDeTiempo = true;

                    if (excel.PrepararPlantilla(sNombreDocumento))
                    {
                        excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, DtGeneral.EstadoConectadoNombre);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 14, sConcepto);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                        iRenglon = 9;
                        //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                        excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);

                        //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                        excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                        if(sPNO != "")
                        {
                            iRenglon += 2;
                            iRenglon += leer.Registros;
                            iColBase += leer.Columnas.Length;
                            excel.EscribirCelda(sNombreHoja, iRenglon, iColBase, 14, sPNO, ClosedXML.Excel.XLAlignmentHorizontalValues.Right, "");
                        }


                        excel.CerraArchivo();
                        excel.AbrirDocumentoGenerado(true); 
                    }
                }
                this.Cursor = Cursors.Default;
            }
        }
        #endregion Botones

        #region Funciones
    private void LimpiaPantalla()
    {
        Fg.IniciaControles(this, true);

        myGrid.Limpiar(false);
        IniciaToolBar(true, true, false);            
            
        chkBuscarClave.Visible = false;
        chkBuscarClave.Checked = true; 
        txtCodigo.Enabled = false;

        HabilitarControles();

        rdoClavesLibres.Checked = tpTipoDeKardex == 2; 

        BloquearControles(false);
    }

    private void IniciaToolBar(bool bNuevo, bool bEjecutar, bool bImprimir)
    {
        btnNuevo.Enabled = bNuevo;
        btnEjecutar.Enabled = bEjecutar;
        btnImprimir.Enabled = bImprimir;
        btnExportarExcel.Enabled = bImprimir;
    }

    private void HabilitarControles()
    {
        bool bActivar = false; 

        rdoAntibioticos.Checked = bActivar;
        rdoControlados.Checked = bActivar;
        rdoClavesLibres.Checked = bActivar; 

        rdoTodasClaves.Checked = true;
        rdoPorClave.Checked = bActivar;
        rdoPorProducto.Checked = bActivar;
    }

    private void BloquearControles(bool Bloquear)
    {
        rdoAntibioticos.Enabled = !Bloquear;
        rdoControlados.Enabled = !Bloquear;

        rdoTodasClaves.Enabled = !Bloquear;
        rdoPorClave.Enabled = !Bloquear;
        rdoPorProducto.Enabled = !Bloquear;

        dtpFechaInicial.Enabled = !Bloquear;
        dtpFechaFinal.Enabled = !Bloquear; 
    }

    private void ObtenerInformacion()
    {
        string sSql = "";
        int iTipoReporte = 0;
        int iTipoDeClave = 0;

        BloquearControles(true);


        if(rdoPorClave.Checked)
        {
            iTipoReporte = 1;
        }

        if(rdoPorProducto.Checked)
        {
            iTipoReporte = 2;
        }

        if (rdoAntibioticos.Checked)
        {
            //sSql = string.Format("Set Dateformat YMD Exec spp_Kardex_Antibioticos_Farmacia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
            //       DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sIdClaveSSA, txtCodigo.Text,
            //       General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), iTipoReporte);

            //sSql += "\n " + string.Format(" Select Convert(varchar(10), FechaRegistro, 120) As FechaRegistro, Folio, " +
            //    " DescMovimiento, ClaveSSA, DescProducto, Entrada, Salida, Existencia  " +
            //    " From tmpKardex_Antibioticos_Farmacia (Nolock) " +
            //    " Order By IdClaveSSA_Sal, IdProducto, FechaRegistro  ");

            iTipoDeClave = 1;
        }

        if (rdoControlados.Checked)
        {
            //sSql = string.Format("Set Dateformat YMD Exec spp_Kardex_Controlados_Farmacia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
            //       DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sIdClaveSSA, txtCodigo.Text,
            //       General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), iTipoReporte);

            //sSql += "\n " + string.Format(" Select Convert(varchar(10), FechaRegistro, 120) As FechaRegistro, Folio, " +
            //    " DescMovimiento, ClaveSSA, DescProducto, Entrada, Salida, Existencia, ClaveLote, IdSubFarmacia, FechaCaducidad  " +
            //    " From tmpKardex_Controlados_Farmacia (Nolock) " +
            //    " Order By IdClaveSSA_Sal, IdProducto, FechaRegistro  ");

            iTipoDeClave = 2;
        }

        if (tpTipoDeKardex == 2)
        {
            iTipoDeClave = 0;  
        }

        sSql = string.Format("Set Dateformat YMD Exec spp_Kardex_Farmacia  " + 
            " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdClaveSSA = '{3}', @IdProducto = '{4}', @FechaInicial = '{5}', @FechaFinal = '{6}', " + 
            " @TipoReporte = '{7}', @ClaveLote = '{8}', @TipoDeClave = '{9}' ",
            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sIdClaveSSA, txtCodigo.Text,
            General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), iTipoReporte, "", iTipoDeClave);


        myGrid.Limpiar(false);  
        if (!leer.Exec(sSql))
        {
            Error.GrabarError(leer, "ObtenerInformacion");
            General.msjError("Ocurrió un error al obtener la información de movimientos.");
            BloquearControles(false); 
        }
        else
        {
            if (leer.Leer())
            {
                dtDatosExcel = leer.Tabla(2);
                if (leer.Registros <= 1000)
                {
                    myGrid.LlenarGrid(leer.DataSetClase);
                }
                else
                {
                    General.msjAviso("El número de registros encontrados excede el limite para mostrar en pantalla, se habilitara la descarga a Excel."); 
                }
                IniciaToolBar(true, false, true); 
            }
            else
            {
                General.msjUser("No se encontro información para los criterios especificados.");
                BloquearControles(false); 
            }
        }
    }

    private void Impresion()
    {
        bool bRegresa = false;
        //if (validarImpresion())
        {
            DatosCliente.Funcion = "btnImprimir_Click()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;

            if (rdoAntibioticos.Checked)
            {
                myRpt.NombreReporte = "PtoVta_Kardex_Antibioticos_Farmacia";  
            }

            if (rdoControlados.Checked)
            {
                myRpt.NombreReporte = "PtoVta_Kardex_Controlados_Farmacia";
            }

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

            ////if (General.ImpresionViaWeb)
            ////{
            ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            ////    DataSet datosC = DatosCliente.DatosCliente();

            ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
            ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
            ////}
            ////else
            ////{
            ////    myRpt.CargarReporte(true);
            ////    bRegresa = !myRpt.ErrorAlGenerar;
            ////}

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
    }

        #endregion Funciones

        #region Eventos
        #region Eventos_TipoReporte
    private void rdoTodasClaves_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoTodasClaves.Checked)
        {
            txtCodigo.Enabled = false;
            chkBuscarClave.Visible = false;
        }
    }

    private void rdoPorClave_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoPorClave.Checked)
        {
            txtCodigo.Enabled = true;
            chkBuscarClave.Visible = true;
        }
    }

    private void rdoPorProducto_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoPorProducto.Checked)
        {
            txtCodigo.Enabled = true;
            chkBuscarClave.Visible = false;
        }
    } 
        #endregion Eventos_TipoReporte

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodigo.Text.Trim() != "")
            {
                leer.DataSetClase = Consulta.Sales_Antibioticos_Controlados(txtCodigo.Text, chkBuscarClave.Checked, rdoPorProducto.Checked,
                                                                            rdoControlados.Checked, rdoAntibioticos.Checked, "txtCodigo_Validating");
                if (leer.Leer())
                {
                    if (rdoPorClave.Checked)
                    {
                        chkBuscarClave.Checked = true;
                        txtCodigo.Text = leer.Campo("ClaveSSA");
                        lblDescripcion.Text = leer.Campo("DescripcionSal");
                        //sIdClaveSSA = leer.Campo("ClaveSSA");
                    }
                    if (rdoPorProducto.Checked)
                    {
                        txtCodigo.Text = leer.Campo("IdProducto");
                        lblDescripcion.Text = leer.Campo("Descripcion");
                    }

                    sIdClaveSSA = leer.Campo("IdClaveSSA_Sal");                    
                }
                //else
                //{
                //    e.Cancel = true;
                //    General.msjUser("Clave SSA ó Producto no encontrada, verifique.");
                //}
            }
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Sales_Antibioticos_Controlados(chkBuscarClave.Checked, rdoPorProducto.Checked,
                                                                            rdoControlados.Checked, rdoAntibioticos.Checked, "txtCodigo_KeyDown()");
                if (leer.Leer())
                {
                    if (rdoPorClave.Checked)
                    {
                        chkBuscarClave.Checked = true;
                        txtCodigo.Text = leer.Campo("ClaveSSA");
                        lblDescripcion.Text = leer.Campo("DescripcionSal");
                    }
                    if (rdoPorProducto.Checked)
                    {
                        txtCodigo.Text = leer.Campo("IdProducto");
                        lblDescripcion.Text = leer.Campo("Descripcion");
                    }

                    sIdClaveSSA = leer.Campo("IdClaveSSA_Sal");
                }
            }
        }

        #endregion Eventos

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            lblDescripcion.Text = "";
        }
    }
}
