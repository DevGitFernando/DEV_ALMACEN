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
using DllFarmaciaSoft.Usuarios_y_Permisos;
//using SC_SolutionsSystem.ExportarDatos;

namespace DllPedidosClientes.Reportes
{
    public partial class FrmKardexControladosAntibioticos : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente DatosCliente;
        clsGrid myGrid;
        clsLeer leer;
        clsAyudas Ayuda;
        clsConsultas Consulta;
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;

        string sIdClaveSSA = "";
        //clsExportarExcelPlantilla xpExcel;

        public FrmKardexControladosAntibioticos()
        {
            InitializeComponent();

            // Esperar hasta que la consulta se ejecute. 
            ConexionLocal = new clsConexionSQL();
            ConexionLocal.DatosConexion.Servidor = General.DatosConexion.Servidor;
            ConexionLocal.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            ConexionLocal.DatosConexion.Usuario = General.DatosConexion.Usuario;
            ConexionLocal.DatosConexion.Password = General.DatosConexion.Password;
            ConexionLocal.DatosConexion.Puerto = General.DatosConexion.Puerto; 

            ConexionLocal.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 



            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            conexionWeb.Url = General.Url;
            leer = new clsLeer(ref ConexionLocal);

            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Consulta = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdMovtos, this);
            myGrid.EstiloGrid(eModoGrid.SoloLectura);
        }

        private void FrmSalesControladosAntibioticos_Load(object sender, EventArgs e)
        {
            CargarFarmacias();
            btnNuevo_Click(null, null);
        }

        #region Botones 

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex == 0)
            {
                General.msjAviso("Seleccione una farmacia por favor....");
                cboFarmacias.Focus();
            }
            else
            {
                ObtenerInformacion();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Impresion();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            /*
            bool bRegresa = true;
            string sCons = ""; 
            string sAntCon = "ANTIBIOTICOS";
            string sSql = "Select * From tmpKardex_Antibioticos_Farmacia Order By OrdenReporte, IdProducto, CodigoEAN, Keyx";

            if (rdoControlados.Checked)
            {
                sAntCon = "CONTROLADOS";
                sSql = "Select * From tmpKardex_Controlados_Farmacia Order By OrdenReporte, IdProducto, CodigoEAN, Keyx";
            }

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnExportarExcel_Click()");
                General.msjError("Ocurrió un error al obtener la información de movimientos.");
            }

            if (leer.Registros == 0)
            {
                General.msjAviso("No existe información para exportar, verifique.");
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Kardex_Antibioticos_Controlados_Farmacia.xls";
                //bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Kardex_Antibioticos_Controlados_Farmacia.xls", DatosCliente);
                bRegresa = GnPlantillas.GenerarPlantilla(ListaPlantillas.Kardex_Antibioticos_Controlados_Farmacia, "Kardex_Antibioticos_Controlados_Farmacia.xls");

                if (!bRegresa)
                {
                    this.Cursor = Cursors.Default;
                    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
                }
                else
                {
                    xpExcel = new clsExportarExcelPlantilla(GnPlantillas.Documento);
                    xpExcel.AgregarMarcaDeTiempo = false;

                    if (xpExcel.PrepararPlantilla())
                    {
                        string sConcepto = string.Format("REPORTE DE MOVIMIENTOS DE {0} DEL {1} AL {2}",
                                        sAntCon, General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
                        xpExcel.GeneraExcel();
                        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
                        xpExcel.Agregar(cboFarmacias.Text, 3, 2);
                        xpExcel.Agregar(sConcepto, 4, 2);
                        //xpExcel.Agregar(leer.Campo("ClaveSSA") + " -- " + leer.Campo("DescripcionCLave"), 5, 2);
                        xpExcel.Agregar("Fecha impresión : " + General.FechaSistemaObtener(), 6, 2);
                        //xpExcel.Agregar(sClave + "--" + sDesc, 6, 3);
                        //xpExcel.Agregar(sDescTipo, 8, 3);


                        for (int iRow = 9; leer.Leer(); iRow++)
                        {
                            int iCol = 2;

                            sCons = leer.Campo("Consecutivo");

                            if (sCons == "0")
                            {
                                sCons = "";
                            }

                            xpExcel.Agregar(leer.Campo("Keyx"), iRow, iCol++);
                            xpExcel.Agregar(sCons, iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("Folio"), iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("FechaRegistro"), iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("Entrada"), iRow, iCol++); 
                            xpExcel.Agregar(leer.Campo("Salida"), iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("Existencia"), iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("DescMovimiento"), iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("ClaveSSA"), iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("DescripcionCLave"), iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("CodigoEAN"), iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("DescProducto"), iRow, iCol++);

                            xpExcel.Agregar(leer.Campo("ClaveLote"), iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("IdSubFarmacia"), iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("FechaCaducidad"), iRow, iCol++);

                            xpExcel.Agregar(leer.Campo("Presentacion"), iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("Laboratorio"), iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("NumReceta"), iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("Beneficiario"), iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("Prescribe"), iRow, iCol++);
                            xpExcel.Agregar(leer.Campo("Cedula"), iRow, iCol++);
                        }

                        xpExcel.CerrarDocumento();

                        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                        {
                            xpExcel.AbrirDocumentoGenerado();
                        }
                        // General.msjUser("Exportacion finalizada."); 
                    }
                }
                this.Cursor = Cursors.Default;
            }
            //btnExportarExcel.Enabled = false;
            */
        }

        #endregion Botones

        #region Funciones

        private void CargarFarmacias()
        {
            cboEstados.Add(DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.EstadoConectado + " - " + DtGeneralPedidos.EstadoConectadoNombre);

            string sSqlFarmacias = "";

            sSqlFarmacias = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                            " From vw_Farmacias_Urls U (NoLock) " +
                            " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                            " Where U.IdEstado = '{0}' and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                            DtGeneralPedidos.EstadoConectado);


            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");

            if (!leer.Exec(sSqlFarmacias))
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            }
            else
            {
                cboFarmacias.Add(leer.DataRowsClase, true, "IdFarmacia", "Farmacia");
            }
            cboFarmacias.SelectedIndex = 0;
        }

        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);

            myGrid.Limpiar(false);
            IniciaToolBar(true, true, false);            
            
            chkBuscarClave.Visible = false;
            chkBuscarClave.Checked = false; 
            txtCodigo.Enabled = false;

            HabilitarControles();
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
                sSql = string.Format("Set Dateformat YMD Exec spp_Kardex_Antibioticos_Farmacia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                       DtGeneral.EmpresaConectada, cboEstados.Data, cboFarmacias.Data, sIdClaveSSA, txtCodigo.Text,
                       General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), iTipoReporte);

                sSql += "\n " + string.Format(" Select Convert(varchar(10), FechaRegistro, 120) As FechaRegistro, Folio, " +
                    " DescMovimiento, ClaveSSA, DescProducto, Entrada, Salida, Existencia  " +
                    " From tmpKardex_Antibioticos_Farmacia (Nolock) " +
                    " Order By IdClaveSSA_Sal, IdProducto, FechaRegistro  ");
            }

            if (rdoControlados.Checked)
            {
                sSql = string.Format("Set Dateformat YMD Exec spp_Kardex_Controlados_Farmacia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                       DtGeneral.EmpresaConectada, cboEstados.Data, cboFarmacias.Data, sIdClaveSSA, txtCodigo.Text,
                       General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), iTipoReporte);

                sSql += "\n " + string.Format(" Select Convert(varchar(10), FechaRegistro, 120) As FechaRegistro, Folio, " +
                    " DescMovimiento, ClaveSSA, DescProducto, Entrada, Salida, Existencia, ClaveLote, IdSubFarmacia, FechaCaducidad  " +
                    " From tmpKardex_Controlados_Farmacia (Nolock) " +
                    " Order By IdClaveSSA_Sal, IdProducto, FechaRegistro  ");
            }

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
                    myGrid.LlenarGrid(leer.DataSetClase);
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

                myRpt.RutaReporte = DtGeneralPedidos.RutaReportes;

                if (rdoAntibioticos.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_Kardex_Antibioticos_Farmacia";  
                }
                if (rdoControlados.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_Kardex_Controlados_Farmacia";
                }
                DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                DataSet datosC = DatosCliente.DatosCliente();

                //bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
                bRegresa = DtGeneralPedidos.GenerarReporte(General.Url, myRpt, cboEstados.Data, cboFarmacias.Data, InfoWeb, datosC); 

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
