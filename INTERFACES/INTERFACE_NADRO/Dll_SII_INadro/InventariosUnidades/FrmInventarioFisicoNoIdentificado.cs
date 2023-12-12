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

using SC_SolutionsSystem.ExportarDatos; 

using DllFarmaciaSoft;

namespace Dll_SII_INadro.InventariosUnidades
{
    public partial class FrmInventarioFisicoNoIdentificado : FrmBaseExt 
    {
        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, DescripcionClave = 2, CodigEAN = 3, NombreComercial = 4, Laboratorio = 5, Cantidad = 6
        }

        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsLeer leer;
        DllFarmaciaSoft.clsConsultas Consultas;
        DllFarmaciaSoft.clsAyudas ayuda;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;
        clsLeerWebExt leerWeb;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
        string sFolio = "", sMensaje = "", sValorGrid = "", sStatus = "";
        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\INT_ND_Inv_Fisico_NoIdentificado.xls";
        int iOpcion;
        clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;    
       
        public FrmInventarioFisicoNoIdentificado()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            
            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            leerExportarExcel = new clsLeer(ref ConexionLocal); 
            leerWeb = new clsLeerWebExt(General.Url, General.ArchivoIni, DatosCliente);
            leer = new clsLeer(ref ConexionLocal);

            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White;
            myGrid.FrozenColumnas = (int)Cols.CodigEAN; 

        }

        private void FrmInventarioInicialConsignacion_NoEnCatalogo_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Limpiar 
        private void IniciarToolBar(bool Guardar, bool Imprimir, bool ExportarExcel)
        {
            btnGuardar.Enabled = Guardar;
            btnImprimir.Enabled = Imprimir;
            btnExportarExcel.Enabled = ExportarExcel; 
        }

        private void LimpiarPantalla()
        {
            myGrid.Limpiar(true);
            Fg.IniciaControles();
            chkTerminarCaptura.BackColor = toolStripBarraMenu.BackColor; 
            IniciarToolBar(false, false, false);

            lblStatus.Text = "TERMINADO"; //Se pone aqui ya que el IniciaControles le borra el texto.
            lblStatus.Visible = false;

            dtpFechaRegistro.Enabled = false; 
            chkTerminarCaptura.Enabled = true;
            lblTituloInventario.Text = "INVENTARIO FISICO";             

            txtFolio.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }        
        #endregion Limpiar

        #region Buscar Folio 
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = false;
            IniciarToolBar(false, false, false);

            myLeer = new clsLeer(ref ConexionLocal);

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                IniciarToolBar(true, false, false);
            }
            else
            {
                myLeer.DataSetClase = Folio_Inv_OperacionMaquila_NoIdentificado_Enc(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim());
                if (myLeer.Leer())
                {
                    bContinua = true;
                    //sStatus = myLeer.Campo("MovtoAplicado");
                    CargaEncabezadoFolio();
                }
                else
                {
                    General.msjAviso("No se encontro información del Folio capturado.");
                }

                if (bContinua)
                {
                    bContinua = CargaDetallesFolio();

                    if (!chkTerminarCaptura.Checked)
                    {
                        IniciarToolBar(true, true, true);
                    }

                }
            }

            if (!bContinua)
            {
                txtFolio.Focus();
            }
        }

        private DataSet Folio_Inv_OperacionMaquila_NoIdentificado_Enc(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio)
        {
            DataSet myDataset = new DataSet();
            
            string sQuery = string.Format(" Select *  " +
                " From INT_ND_Inv_OperacionMaquila_NoIdentificado_Enc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(Folio, 8));


            if (!myLlenaDatos.Exec(sQuery))
            {
                Error.GrabarError(myLlenaDatos, "Folio_Inv_OperacionMaquila_NoIdentificado_Enc");
                General.msjError("Ocurrio un error al consultar el folio");
            }
            else
            {
                if (myLlenaDatos.Leer())
                {
                    myDataset = myLlenaDatos.DataSetClase;
                }
            }

            return myDataset;
        }

        private DataSet Folio_Inv_OperacionMaquila_NoIdentificado_Det(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio)
        {
            DataSet myDataset = new DataSet();            

            string sQuery = string.Format(" Select ClaveSSA, Descripcion, CodigoEAN, " +
                "NombreComercial, Laboratorio, Cantidad " +
                " From INT_ND_Inv_OperacionMaquila_NoIdentificado_Det (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(Folio, 8));

            if (!myLlenaDatos.Exec(sQuery))
            {
                Error.GrabarError(myLlenaDatos, "Folio_Inv_OperacionMaquila_NoIdentificado_Det");
                General.msjError("Ocurrio un error al consultar los detalles del folio");
            }
            else
            {
                if (myLlenaDatos.Leer())
                {
                    myDataset = myLlenaDatos.DataSetClase;
                }
            }

            return myDataset;
        }

        private void CargaEncabezadoFolio()
        {            
            //Se hace de esta manera para la ayuda.
            txtFolio.Text = myLeer.Campo("Folio");
            sFolio = txtFolio.Text;
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");
            
            txtObservaciones.Text = myLeer.Campo("Observaciones"); 
            
            //Se bloquea el encabezado del Folio 
            Fg.BloqueaControles(this, false, FrameEncabezado);
            
            IniciarToolBar(false, true, true);
            
            if (myLeer.Campo("MovtoAplicado") == "S")
            {   
                IniciarToolBar(false, true, true);
                lblStatus.Text = "TERMINADO";
                lblStatus.Visible = true;
                
                chkTerminarCaptura.Checked = true;
                chkTerminarCaptura.Enabled = false;

                General.msjUser("La captura de Inventario No Identificado ya fue terminado,\n no es posible hacer modificaciones.");
            }
        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = false;

            myLlenaDatos.DataSetClase = Folio_Inv_OperacionMaquila_NoIdentificado_Det(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim());

            if (myLlenaDatos.Leer())
            {
                bRegresa = true; 
                myGrid.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
                CargarDatosExportarExcel(); 
            }

            // Bloquear grid completo 
            if (sStatus == "S")
                myGrid.BloqueaGrid(true);
            
            return bRegresa;
        } 
        #endregion Buscar Folio
        
        #region Guardar Informacion 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            iOpcion = 0;

            if (txtFolio.Text != "*")
                iOpcion = 1;
            
            EliminarRenglonesVacios();
            if (ValidaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();
                    bContinua = GrabarEncabezado();
                    
                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        txtFolio.Text = sFolio;
                        ConexionLocal.CompletarTransaccion();

                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        IniciarToolBar(false, true, true);
                        Imprimir(true);
                        btnNuevo_Click(this, null);
                    }
                    else
                    {
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        ConexionLocal.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                    ConexionLocal.Cerrar();
                }
                else
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }
            }
        }

        private bool GrabarEncabezado()
        {
            bool bRegresa = true;
            string sAplicado = chkTerminarCaptura.Checked ? "S" : "N";

            string sSql = string.Format("Set DateFormat YMD Exec spp_Mtto_INT_ND_Inv_OperacionMaquila_NoIdentificado_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8} ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                txtFolio.Text.Trim(), DtGeneral.IdPersonal, txtObservaciones.Text, sAplicado, "A", iOpcion); 

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolio = myLeer.Campo("Folio");
                sMensaje = myLeer.Campo("Mensaje");

                if (iOpcion == 1)
                {
                    if (EliminarDetalles())
                    {
                        bRegresa = GrabarDetalle();
                    }
                    else
                    {
                        bRegresa = false;
                    }
                }
                else
                {
                    bRegresa = GrabarDetalle();
                }
            }
            return bRegresa;
        }

        private bool GrabarDetalle()
        {
            bool bRegresa = true;
            string sSql = "";
            string sClaveSSA = "", sNombreComercial = "", sCodigoEAN = "", sDescripcion = "", sLaboratorio = "";
            int iCantidad = 0;

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sClaveSSA = myGrid.GetValue(i, (int)Cols.ClaveSSA);
                sDescripcion = myGrid.GetValue(i, (int)Cols.DescripcionClave);
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodigEAN);
                sNombreComercial = myGrid.GetValue(i, (int)Cols.NombreComercial);
                sLaboratorio = myGrid.GetValue(i, (int)Cols.Laboratorio);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                
                if (sClaveSSA != "")
                {
                    sSql = string.Format("Set DateFormat YMD Exec spp_Mtto_INT_ND_Inv_OperacionMaquila_NoIdentificado_Det '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                        sFolio, sClaveSSA, sDescripcion, sCodigoEAN, sNombreComercial, sLaboratorio, iCantidad, "A");

                    //sSql = sSql.ToUpper();

                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private bool EliminarDetalles()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format("Delete INT_ND_Inv_OperacionMaquila_NoIdentificado_Det " +
            "Where IdEmpresa ='{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}'",
            sEmpresa, sEstado, sFarmacia, sFolio);

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }

            return bRegresa;
        }
        #endregion Guardar Informacion

        #region Imprimir Informacion
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(false);
        }

        private bool validarImpresion(bool Confirmar)
        {
            bool bRegresa = true;

            if (Confirmar)
            {
                if (General.msjConfirmar(" ¿ Desea imprimir la información en pantalla ? ") == DialogResult.No)
                {
                    bRegresa = false;
                }
            }

            if (bRegresa)
            {
                if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
                {
                    bRegresa = false;
                    General.msjUser("Folio de Pedido Inicial inválido, verifique.");
                }
            }

            return bRegresa;
        }

        private void Imprimir(bool Confirmacion)
        {
            bool bRegresa = true;
            //dImporte = Importe; 

            if (validarImpresion(Confirmacion))
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = DtGeneral.RutaReportes; 

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", General.Fg.PonCeros(txtFolio.Text, 8));
                myRpt.NombreReporte = "INT_ND_InventarioFisico_NoIdentificado";

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
                // bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, @"PRUEBA.pdf", FormatosExportacion.PortableDocFormat); 

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Imprimir Informacion

        #region Grid 
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (myGrid.ActiveCol == (int)Cols.ClaveSSA)
            {
                if (e.KeyCode == Keys.F1)
                {
                    myLeer.DataSetClase = ayuda.ClavesSSA_Sales("grdProductos_KeyDown");
                    if (myLeer.Leer())
                    {
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA, myLeer.Campo("IdClaveSSa_Sal"));
                        CargaDatosSal();
                    }
                }
            }

            if (myGrid.ActiveCol == (int)Cols.Laboratorio)
            {
                if (e.KeyCode == Keys.F1)
                {
                    myLeer.DataSetClase = ayuda.Laboratorios("grdProductos_KeyDown");
                    if (myLeer.Leer())
                    {
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Laboratorio, myLeer.Campo("Descripcion"));
                    }

                }
            }


            if (e.KeyCode == Keys.Delete)
            {
                myGrid.DeleteRow(myGrid.ActiveRow);

                if (myGrid.Rows == 0)
                    myGrid.Limpiar(true);
            }
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (lblStatus.Visible == false)
            {
                if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                {
                    if (myGrid.GetValue(myGrid.ActiveRow, 1) != "" && myGrid.GetValue(myGrid.ActiveRow, 2) != ""
                        && myGrid.GetValue(myGrid.ActiveRow, 3) != "" && myGrid.GetValue(myGrid.ActiveRow, 4) != ""
                        && myGrid.GetValue(myGrid.ActiveRow, 5) != "" && myGrid.GetValue(myGrid.ActiveRow, 6) != ""
                        && myGrid.GetValueFecha(myGrid.ActiveRow, 7) != null && myGrid.GetValueInt(myGrid.ActiveRow, 8) > 0)
                    {
                        myGrid.Rows = myGrid.Rows + 1;
                        myGrid.ActiveRow = myGrid.Rows;
                        myGrid.SetActiveCell(myGrid.Rows, 1);
                    }
                }
            }
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA); 
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            switch (myGrid.ActiveCol)
            {
                case 1:
                    {
                        ObtenerDatosSal();
                    }

                    break;
            }
        }

        private void limpiarColumnas()
        {
            for (int i = 1; i <= myGrid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                myGrid.SetValue(myGrid.ActiveRow, i, "");
            }
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                if (myGrid.GetValue(i, 1).Trim() == "") //Si la columna oculta ClaveSSA esta vacia se elimina.
                    myGrid.DeleteRow(i);
            }

            if (myGrid.Rows == 0) // Si No existen renglones, se inserta 1.
                myGrid.AddRow();
        }

        private void ObtenerDatosSal()
        {
            string sCodigo = "";

            sCodigo = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA);

            if (sCodigo.Trim() == "")
            {
                General.msjUser("Sal no encontrada ó no esta Asignada a la Farmacia.");
                myGrid.LimpiarRenglon(myGrid.ActiveRow);
            }
            else
            {
                myLeer.DataSetClase = Consultas.ClavesSSA_Sales(sCodigo, true, "ObtenerDatosSal()");
                if (!myLeer.Leer())
                {
                    General.msjUser("Sal no encontrada ó no esta Asignada a la Farmacia.");
                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
                }
                else
                {
                    CargaDatosSal();
                }
            }
        }

        private void CargaDatosSal()
        {
            int iRowActivo = myGrid.ActiveRow;

            if (lblStatus.Visible == false)
            {
                myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));
                myGrid.SetValue(iRowActivo, (int)Cols.DescripcionClave, myLeer.Campo("Descripcion"));
            }
        }
        
        #endregion Grid

        #region Validaciones de Controles 
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio inválido, verifique.");
                txtFolio.Focus();
            } 

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            return bRegresa;
        }

        private bool validarCapturaProductos()
        {
            bool bRegresa = true;

            if (myGrid.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                if (myGrid.GetValue(1, (int)Cols.DescripcionClave) == "")
                {
                    bRegresa = false;
                }
                else
                {
                    ////if ( int.Parse( lblUnidades.Text ) == 0 )
                    ////{
                    ////    bRegresa = false;
                    ////}
                    ////else
                    {
                        for (int i = 1; i <= myGrid.Rows; i++)
                        {
                            if (myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos una Clave para el Folio\n y/o capturar cantidades para al menos una Clave, verifique.");
            }

            return bRegresa;

        } 
        #endregion Validaciones de Controles

        #region Exportar a Excel 
        private void CargarDatosExportarExcel()
        {
            string sSql = string.Format(" Select * "+
                " From vw_INT_ND_Impresion_InventarioFisico_NoIdentificado (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text.Trim(), 8));

            if (!leerExportarExcel.Exec(sSql))
            {
            } 
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            int iRow = 10;
            string sNombreFile = "INT_ND_InventarioFisico_NoIdentificado_" + DtGeneral.ClaveRENAPO + sFarmacia + "_" + Fg.PonCeros(txtFolio.Text, 6) + ".xls";
            string sUnidad = string.Empty;

            this.Cursor = Cursors.WaitCursor;
            sRutaPlantilla = Application.StartupPath + @"\\Plantillas\INT_ND_Inv_Fisico_NoIdentificado.xls";
            bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "INT_ND_Inv_Fisico_NoIdentificado.xls", DatosCliente);

            if (!bRegresa)
            {
                this.Cursor = Cursors.Default;
                General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            }
            else
            {
                xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                xpExcel.AgregarMarcaDeTiempo = false;

                if (xpExcel.PrepararPlantilla(sNombreFile))
                {
                    xpExcel.GeneraExcel();
                    leerExportarExcel.RegistroActual = 1;

                    leerExportarExcel.Leer();

                    xpExcel.Agregar(leerExportarExcel.Campo("Empresa"), 2, 2);

                    sUnidad = leerExportarExcel.Campo("IdFarmacia") + "-" +
                    leerExportarExcel.Campo("Farmacia") + ", " + leerExportarExcel.Campo("Estado");
                    xpExcel.Agregar(sUnidad, 3, 2);

                    xpExcel.Agregar("Folio : " + leerExportarExcel.Campo("Folio"), 4, 2);
                    xpExcel.Agregar("Fecha : " + leerExportarExcel.CampoFecha("FechaRegistro"), 4, 3);
                    xpExcel.Agregar(leerExportarExcel.Campo("MovtoAplicado") == "S" ? "Status : Terminado" : "Status : Abierto", 4, 4);

                    xpExcel.Agregar("Folio : " + leerExportarExcel.Campo("Folio"), 4, 2);
                    xpExcel.Agregar("Elaboró : " + leerExportarExcel.Campo("IdPersonal") + "-" + leerExportarExcel.Campo("NombrePersonal"), 5, 2);

                    xpExcel.Agregar(DateTime.Now, 5, 5);

                    xpExcel.Agregar("Observaciones : " + leerExportarExcel.Campo("Observaciones"), 7, 2);

                    leerExportarExcel.RegistroActual = 1;
                    while (leerExportarExcel.Leer())
                    {
                        xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRow, 2);
                        xpExcel.Agregar(leerExportarExcel.Campo("Descripcion"), iRow, 3);
                        xpExcel.Agregar(leerExportarExcel.Campo("CodigoEAN"), iRow, 4);
                        xpExcel.Agregar(leerExportarExcel.Campo("NombreComercial"), iRow, 5);
                        xpExcel.Agregar(leerExportarExcel.Campo("Laboratorio"), iRow, 6);
                        xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRow, 7);
                        xpExcel.Agregar(leerExportarExcel.Campo("Costo"), iRow, 8);
                        xpExcel.Agregar(leerExportarExcel.Campo("ImporteLote"), iRow, 9);

                        iRow++;
                    }

                    // Finalizar el Proceso 
                    xpExcel.CerrarDocumento();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }
                }
                this.Cursor = Cursors.Default;
            }
        }
        #endregion Exportar a Excel
    }   
}