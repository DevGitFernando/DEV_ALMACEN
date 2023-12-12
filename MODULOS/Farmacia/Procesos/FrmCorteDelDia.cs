using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
//using SC_SolutionsSystem.ExportarDatos; 
using SC_SolutionsSystem.Reportes;
using Farmacia.Pedidos;
using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace Farmacia.Procesos
{
    public partial class FrmCorteDelDia : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsConsultas Consultas;
        FrmCorte_UsuariosConSesionAbierta Usuarios;
        // FrmPedido_VentaContado PedidoContado;
        FrmPedidoAutomatico_Productos PedidoCredito;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
        string sIdPersonal = DtGeneral.IdPersonal;
        string sObservaciones = "";

        string sTipoPedidoContado = "01";
        string sTipoPedidoCredito = "02"; 

        // Manejo de reportes  
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();
        //clsExportarExcelPlantilla xpExcel; 

        string sFolioInv = "";
        string sMensajeInv = "";
        string sMensajeInv_Productos = "";

        public FrmCorteDelDia()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);
        }

        private void FrmCorteDelDia_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();

            dtpFechaSistema.Value = GnFarmacia.FechaOperacionSistema;
            dtpFechaSistema.Enabled = false;

            dtpNuevaFechaSistema.MinDate = dtpFechaSistema.Value.AddDays(1);
            dtpNuevaFechaSistema.MaxDate = dtpFechaSistema.Value.AddDays(1);

            if (ValidaUsuarioCorte())
            {
                //ValidaPedidoAutomaticoProductos();
            }

        } 
        #region Verifica si existen sesiones abiertas

        private bool ValidaUsuarioCorte()
        {
            bool bRegresa = true; // , bMostrarVacio = false;
            myLeer = new clsLeer(ref ConexionLocal);

            myLeer.DataSetClase = Consultas.CortesDiarios_Status(sEmpresa, sEstado, sFarmacia, dtpFechaSistema.Text.ToString(), "ValidaUsuarioCorte"); 
            if (myLeer.Leer())
            {
                //Si lee, significa que encontro una sesion abierta. Por lo tanto se envia mensaje advirtiendo al usuario
                bRegresa = false;
                //General.msjUser("Existen sesiones de corte abierta. Cierre estas sesiones en la opcion Cortes Parciales para continuar");
                btnAceptar.Enabled = false;
                Usuarios = new FrmCorte_UsuariosConSesionAbierta();
                Usuarios.ShowDialog(this);

                btnAceptar.Enabled = Usuarios.SesionesCerradas;
            }

            return bRegresa;
        }
        #endregion Verifica si existen sesiones abiertas

        #region Verifica Pedido Automatico
        private bool ValidaPedidoAutomaticoContado()
        {
            // sTipoPedidoContado = Fg.PonCeros((int)TipoPedidosFarmacia.Pedido_Contado,2); 
            bool bContinua = true;
            ////string sSql = String.Format("Set Dateformat YMD Select FolioPedido From COM_FAR_Pedidos (NoLock) " +
            ////"Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdTipoPedido = '{3}' " +
            ////"And Convert( varchar(10), FechaRegistro, 120 ) = Convert( varchar(10), GetDate(), 120) ",
            ////sEmpresa, sEstado, sFarmacia, sTipoPedidoContado);

            ////if (!myLeer.Exec(sSql))
            ////{
            ////    Error.GrabarError(myLeer, "");
            ////    General.msjError("Ocurrió un error al verificar el Pedido Automatico para Venta de Contado de la Farmacia");
            ////}
            ////else
            ////{
            ////    if (!myLeer.Leer())
            ////    {
            ////        //Si no lee, significa que la farmacia no ha hecho su pedido automatico.
            ////        bContinua = false;
            ////        btnAceptar.Enabled = false;
            ////        General.msjUser("Usted no ha confirmado su Pedido Automatico para Venta de Contado. Confirme su pedido para continuar.");

            ////        PedidoContado = new FrmPedido_VentaContado();
            ////        PedidoContado.MostrarPantalla();

            ////        if (PedidoContado.bPedidoGuardado)
            ////        {
            ////            bContinua = true;
            ////            btnAceptar.Enabled = true;
            ////        }
                    

            ////    }
            ////}

            return bContinua;
        }

        private bool ValidaPedidoAutomaticoProductos()
        {            
            //sTipoPedidoContado = Fg.PonCeros((int)TipoPedidosFarmacia.Pedido_Contado, 2);
            //sTipoPedidoCredito = Fg.PonCeros((int)TipoPedidosFarmacia.Pedido_Credito, 2);
            sTipoPedidoCredito = ""; 
            bool bContinua = true;
            string sSql = String.Format("Set Dateformat YMD Select FolioPedido From COM_FAR_Pedidos(NoLock) " +
            "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdTipoPedido In ( '{3}', '{4}' ) " +
            "And Convert( varchar(10), FechaRegistro, 120 ) = Convert( varchar(10), GetDate(), 120) ",
            sEmpresa, sEstado, sFarmacia, sTipoPedidoContado, sTipoPedidoCredito);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "");
                General.msjError("Ocurrió un error al verificar el Pedido Automático para Venta de Crédito de la Farmacia.");
            }
            else
            {
                if (!myLeer.Leer())
                {
                    //Si no lee, significa que la farmacia no ha hecho su pedido automatico.
                    bContinua = false;
                    btnAceptar.Enabled = false;
                    General.msjUser("Usted no ha confirmado su Pedido Automático para Venta de Crédito. Confirme su pedido para continuar.");

                    PedidoCredito = new FrmPedidoAutomatico_Productos();
                    PedidoCredito.MostrarPantalla();

                    if (PedidoCredito.bPedidoGuardado)
                    {
                        bContinua = true;
                        btnAceptar.Enabled = true;
                    } 
                }
            }

            return bContinua;
        }
        #endregion Verifica Pedido Automatico

        #region Guardar Informacion
        private bool validarSaldoDia()
        {
            bool bRegresa = true; 

            if (DtGeneral.ManejaVentaPublico)
            {
                bRegresa = validarSaldoDia_VentaPublico(); 
            }

            return bRegresa; 
        }

        private bool validarSaldoDia_VentaPublico()
        {
            bool bRegresa = true;
            string sSql = "", sFechaSistemaNueva = "";  // sMensaje = "", 

            sFechaSistemaNueva = dtpNuevaFechaSistema.Text.Trim();
            sObservaciones = "Cambio de dia: " + sFechaSistemaNueva + ".";

            sSql = String.Format("Set Dateformat YMD Exec spp_Mtto_CtlCortesDiarios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'  ",
                    sEmpresa, sEstado, sFarmacia, sIdPersonal, sFechaSistema, sFechaSistemaNueva);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "validarSaldoDia()");
                bRegresa = false;
                General.msjError("Ocurrió un error al obtener el saldo del dia.");
            }
            else
            {
                myLeer.Leer();

                if (myLeer.CampoDouble(1) <= 0)
                {
                    General.msjUser("El saldo del dia es menor ó igual a cero, explique a que se debe.");

                    clsObservaciones ob = new clsObservaciones();
                    ob.Encabezado = "Observaciones de Cambio de dia";
                    ob.MaxLength = 200;
                    ob.Show();
                    sObservaciones += "    " + ob.Observaciones;
                    bRegresa = ob.Exito;

                    if (!bRegresa)
                    {
                        General.msjUser("No se capturaron las observaciones para el Cambio de dia, verifique.");
                    }
                }
            }

            return bRegresa;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sFechaSistemaNueva = "";
            bool bContinua = false;

            if (validarSaldoDia())
            {
                sFechaSistemaNueva = dtpNuevaFechaSistema.Text.Trim();
                if (!ConexionLocal.Abrir())
                {
                    Error.LogError(ConexionLocal.MensajeError); 
                    General.msjErrorAlAbrirConexion(); 
                }
                else
                {
                    ConexionLocal.IniciarTransaccion(); 

                    sSql = String.Format("Set Dateformat YMD Exec spp_Mtto_CtlCortesDiarios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', 1, '{6}', '{7}' ",
                            sEmpresa, sEstado, sFarmacia, sIdPersonal, sFechaSistema, sFechaSistemaNueva, sObservaciones, DtGeneral.ArbolModulo);

                    if (myLeer.Exec(sSql))
                    {
                        bContinua = true; 
                        if (myLeer.Leer())
                        {
                            sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                        }

                        if (GnFarmacia.GeneraInventariosAleatoriosAutomaticos)
                        {
                            //// Solo aplica para las farmacias 
                            if (!DtGeneral.EsAlmacen)
                            {
                                bContinua = GeneraInventarioAleatorio(1);
                            }
                        }

                        if (GnFarmacia.GeneraInventariosAleatoriosAutomaticos_Productos)
                        {
                            //// Solo aplica para las farmacias 
                            if (!DtGeneral.EsAlmacen)
                            {
                                bContinua = GeneraInventarioAleatorio(2);
                            }
                        }

                        if (bContinua)
                        {
                            bContinua = GenerarCierreGeneral();
                        }

                        if (bContinua)
                        {
                            ConexionLocal.CompletarTransaccion();
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP 

                            if (GnFarmacia.GeneraInventariosAleatoriosAutomaticos)
                            {
                                General.msjUser(sMensajeInv);// mensaje del folio de invetario aleatorio.
                            }

                            if (GnFarmacia.GeneraInventariosAleatoriosAutomaticos_Productos)
                            {
                                General.msjUser(sMensajeInv_Productos);// mensaje del folio de invetario aleatorio.
                            }

                            GnFarmacia.Parametros.CargarParametros(); //Se actualiza la fecha del sistema. 

                            // Imprimir Corte 
                            btnAceptar.Enabled = false;
                            ImprimirCorteDiario();
                            ImprimirTiraDeAuditoria();

                            if (GnFarmacia.GeneraReporteDispensacionPersonal)
                            {
                                ImprimirCorteParcialDetallado();
                            }

                            if (GnFarmacia.GeneraReporteDispensacionValesPersonal)
                            {
                                ImprimirCorteParcialDetallado_Vales();
                            }

                            this.Close();
                        }
                    }
                    else
                    {
                        bContinua = false;
                    }

                    if(!bContinua)
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");

                    }

                    ConexionLocal.Cerrar();
                }
            } 
        }

        private bool GenerarCierreGeneral()
        {            
            string sSql = "";

            sSql = String.Format("Set Dateformat YMD Exec spp_Mtto_Ctl_CierresGeneral '{0}', '{1}', '{2}', '', '{3}', '{1}', '{2}', '{3}' ",
                            sEmpresa, sEstado, sFarmacia, sIdPersonal );

            return myLeer.Exec(sSql);
        }
        #endregion Guardar Informacion

        #region Impresion 
        private void ImprimirCorteDiario()
        {
            DatosCliente.Funcion = "ImprimirCorteDiario()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_CorteDiario.rpt";  // Tira de Auditoria 

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("@FechaDeSistema", General.FechaYMD(dtpFechaSistema.Value, "-"));
            myRpt.Add("@IdPersonal", "");

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

            ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            ////DataSet datosC = DatosCliente.DatosCliente();

            ////btReporte = conexionWeb.Reporte(InfoWeb, datosC);

            if (!bRegresa)
            {
                if(!DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void ImprimirTiraDeAuditoria()
        {
            DatosCliente.Funcion = "ImprimirTiraDeAuditoria()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_CorteTiraDeAutoria.rpt";  // Tira de Auditoria 

            myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("FechaDeSistemaCorte", General.FechaYMD(dtpFechaSistema.Value, "-"));

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

            ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            ////DataSet datosC = DatosCliente.DatosCliente();

            ////btReporte = conexionWeb.Reporte(InfoWeb, datosC);

            if (!bRegresa)
            {
                if(!DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void ImprimirCorteParcialDetallado()
        {
            DatosCliente.Funcion = "ImprimirCorteParcialDetallado()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_Admon_Validacion_CortesDiarios.rpt";

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);

            myRpt.Add("@FechaDeSistema", General.FechaYMD(dtpFechaSistema.Value, "-"));
            myRpt.Add("@EsReporteGeneral", 1);
            myRpt.Add("@IdPersonal", DtGeneral.IdPersonal);

            // myRpt.Add("Folio", "II" + txtFolio.Text); 

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                if(!DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
            else
            {
                ImprimirCorteParcialDetallado_Excel(); 
            }
        }

        private void ImprimirCorteParcialDetallado_Excel()
        {
            string sSql = string.Format("Exec spp_Rpt_Administrativos_CortesDiarios " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FechaDeSistema = '{3}', @EsReporteGeneral = '{4}', @IdPersonal = '{5}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                General.FechaYMD(dtpFechaSistema.Value, "-"), 1, DtGeneral.IdPersonal);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "ImprimirCorteParcialDetallado_Excel()");
                General.msjError("Ocurrió un error al generar el reporte excel del detallado de dispensación.");
            }
            else
            {
                ImprimirCorteParcialDetallado_ExcelExportar();
            }
        }

        private void ImprimirCorteParcialDetallado_ExcelExportar()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            //string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            string sNombre = string.Format("Reporte de dispensación del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-"));
            //string sNombreFile = "COM_Rpt_ConsumoEdo_Concentrado" + "_" + cboEdo.Data;

            clsLeer leer = new clsLeer();

            leer.DataSetClase = myLeer.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla())
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, "Todos los dispensadores");
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

        //private void ImprimirCorteParcialDetallado_ExcelExportar()
        //{
        //    //// int iRenglon = 8; 
        //    bool bRegresa = false;
        //    int iRow = 10;
        //    int iCol = 0; 
        //    string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion_CortesDiarios.xlsx";

        //    this.Cursor = Cursors.WaitCursor;
        //    bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Admon_Validacion_CortesDiarios.xlsx", DatosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = true;

        //        if (xpExcel.PrepararPlantilla())
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            xpExcel.GeneraExcel();

        //            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
        //            xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
        //            xpExcel.Agregar("Todos los dispensadores", 4, 2);
        //            xpExcel.Agregar(string.Format("Reporte de dispensación del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-")), 5, 2);
        //            xpExcel.Agregar(string.Format("Fecha de impresión : {0} ", DateTime.Now.ToString()), 7, 2);

        //            while (myLeer.Leer())
        //            {
        //                string sStatus = "Activo";
        //                if (myLeer.Campo("Status") == "C")
        //                {
        //                    sStatus = "Cancelado";
        //                }

        //                ////xpExcel.Agregar(myLeer.Campo("NombrePersonal"), iRow, 2);   
        //                ////xpExcel.Agregar(myLeer.Campo("Folio"), iRow, 3);
        //                ////xpExcel.Agregar(myLeer.Campo("NumReceta"), iRow, 5);
        //                ////xpExcel.Agregar(myLeer.Campo("FechaReceta"), iRow, 6);
        //                ////xpExcel.Agregar(myLeer.Campo("FolioReferencia"), iRow, 7);
        //                ////xpExcel.Agregar(myLeer.Campo("Beneficiario"), iRow, 8);
        //                ////xpExcel.Agregar(myLeer.Campo("CodigoEAN"), iRow, 9);
        //                ////xpExcel.Agregar(myLeer.Campo("ClaveSSA"), iRow, 10);
        //                ////xpExcel.Agregar(myLeer.Campo("DescripcionCortaClave"), iRow, 11);
        //                ////xpExcel.Agregar(myLeer.Campo("Cantidad"), iRow, 12);
        //                ////xpExcel.Agregar(myLeer.Campo("PrecioLicitacion"), iRow, 13);
        //                ////xpExcel.Agregar(myLeer.Campo("Importe"), iRow, 14);
        //                ////xpExcel.Agregar(sStatus, iRow, 15);

        //                iCol = 2;
        //                xpExcel.Agregar(DtGeneral.NombrePersonal, iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("Folio"), iRow, iCol++);
        //                iCol++;
        //                //xpExcel.Agregar(myLeer.Campo("Folio"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("Medico"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("NumReceta"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("FechaReceta"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("FolioReferencia"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("Beneficiario"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("CodigoEAN"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("ClaveSSA"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("DescripcionCortaClave"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("Cantidad"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("PrecioLicitacion"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("Importe"), iRow, iCol++);
        //                xpExcel.Agregar(sStatus, iRow, iCol++);

        //                iRow++;
        //            }

        //            //// Finalizar el Proceso 
        //            xpExcel.CerrarDocumento();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //    }

        //    this.Cursor = Cursors.Default;
        //}

        private void ImprimirCorteParcialDetallado_Vales()
        {
            DatosCliente.Funcion = "ImprimirCorteParcialDetallado_Vales()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_Admon_Validacion_CortesDiarios_Vales.rpt";

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);

            myRpt.Add("@FechaDeSistema", General.FechaYMD(dtpFechaSistema.Value, "-"));
            myRpt.Add("@EsReporteGeneral", 0);
            myRpt.Add("@IdPersonal", DtGeneral.IdPersonal);

            // myRpt.Add("Folio", "II" + txtFolio.Text); 

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                if(!DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
            else
            {
                ImprimirCorteParcialDetallado_Vales_Excel(); 
            }
        }

        private void ImprimirCorteParcialDetallado_Vales_Excel()
        {
            string sSql = string.Format("Exec spp_Rpt_Administrativos_CortesDiarios_Vales " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FechaDeSistema = '{3}', @EsReporteGeneral = '{4}', @IdPersonal = '{5}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                General.FechaYMD(dtpFechaSistema.Value, "-"), 1, DtGeneral.IdPersonal);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "ImprimirCorteParcialDetallado_Vales_Excel()");
                General.msjError("Ocurrió un error al generar el reporte excel de vales generados.");
            }
            else
            {
                ImprimirCorteParcialDetallado_Vales_ExcelExportar();
            }
        }

        private void ImprimirCorteParcialDetallado_Vales_ExcelExportar()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            //string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            string sNombre = string.Format("Reporte de dispensación del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-"));
            //string sNombreFile = "COM_Rpt_ConsumoEdo_Concentrado" + "_" + cboEdo.Data;

            clsLeer leer = new clsLeer();

            leer.DataSetClase = myLeer.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla())
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, "Todos los dispensadores");
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

        //private void ImprimirCorteParcialDetallado_Vales_ExcelExportar()
        //{            //// int iRenglon = 8; 
        //    bool bRegresa = false;
        //    int iRow = 10;
        //    string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion_CortesDiarios_Vales.xlsx";

        //    this.Cursor = Cursors.WaitCursor;
        //    bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Admon_Validacion_CortesDiarios_Vales.xlsx", DatosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = true;

        //        if (xpExcel.PrepararPlantilla())
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            xpExcel.GeneraExcel();

        //            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
        //            xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
        //            xpExcel.Agregar("Todos los dispensadores", 4, 2);
        //            xpExcel.Agregar(string.Format("Reporte de emisión de vales del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-")), 5, 2);
        //            xpExcel.Agregar(string.Format("Fecha de impresión : {0} ", DateTime.Now.ToString()), 7, 2);

        //            while (myLeer.Leer())
        //            {
        //                string sStatus = "Activo";
        //                if (myLeer.Campo("Status") == "C")
        //                {
        //                    sStatus = "Cancelado";
        //                }
        //                xpExcel.Agregar(myLeer.Campo("NombrePersonal"), iRow, 2);
        //                xpExcel.Agregar(myLeer.Campo("FolioVenta"), iRow, 3);
        //                xpExcel.Agregar(myLeer.Campo("Folio"), iRow, 4);
        //                xpExcel.Agregar(myLeer.Campo("NumReceta"), iRow, 5);
        //                xpExcel.Agregar(myLeer.Campo("FechaReceta"), iRow, 6);
        //                xpExcel.Agregar(myLeer.Campo("FolioReferencia"), iRow, 7);
        //                xpExcel.Agregar(myLeer.Campo("Beneficiario"), iRow, 8);
        //                xpExcel.Agregar(myLeer.Campo("CodigoEAN"), iRow, 9);
        //                xpExcel.Agregar(myLeer.Campo("ClaveSSA"), iRow, 10);
        //                xpExcel.Agregar(myLeer.Campo("DescripcionCortaClave"), iRow, 11);
        //                xpExcel.Agregar(myLeer.Campo("Cantidad"), iRow, 12);
        //                xpExcel.Agregar(myLeer.Campo("PrecioLicitacion"), iRow, 13);
        //                xpExcel.Agregar(myLeer.Campo("Importe"), iRow, 14);
        //                xpExcel.Agregar(sStatus, iRow, 15);

        //                iRow++;
        //            }

        //            //// Finalizar el Proceso 
        //            xpExcel.CerrarDocumento();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //    }

        //    this.Cursor = Cursors.Default;
        //}

        #endregion Impresion 

        #region Genera_INV_Aleatorio
        private bool GeneraInventarioAleatorio(int Tipo)
        {
            bool bRegresa = true;
            string sSql = "";
            int iTipoInv = 0, iNumClaves = 0;

            iTipoInv = 2;
            iNumClaves = Tipo == 1 ? GnFarmacia.Claves_Inventario_Aleatorio__Encargado : GnFarmacia.Productos_Inventario_Aleatorio__Encargado ;
            sSql = string.Format("Exec {0} ", Tipo == 1 ? " spp_INV_Aleatorios " : " spp_INV_Aleatorios_Productos "); 

            sSql += String.Format("  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TipoInventario = '{3}', @Claves = '{4}', @IdPersonal = '{5}', @FechaSistema = '{6}' ",
                            sEmpresa, sEstado, sFarmacia, iTipoInv, iNumClaves, DtGeneral.IdPersonal, General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-"));

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (myLeer.Leer())
                {
                    sFolioInv = myLeer.Campo("Folio");
                    if (Tipo == 1) sMensajeInv = myLeer.Campo("Mensaje");
                    if (Tipo == 2) sMensajeInv_Productos = myLeer.Campo("Mensaje"); 
                }
            }

            return bRegresa;
        }
        #endregion Genera_INV_Aleatorio
    }
}
