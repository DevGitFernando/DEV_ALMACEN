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
using SC_SolutionsSystem.Reportes; 
using SC_SolutionsSystem.Errores; 
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGenerales;
using DllFarmaciaSoft;

namespace Facturacion.GenerarRemisiones
{
    public partial class FrmValidarRemisiones : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer, myLeerEncabezado, myLeerDetalles;
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;
        clsListView lst;
        clsExportarExcelPlantilla xpExcel;
        DataSet dtsRemision = new DataSet();

        int default_0 = 0; 
        string sFormatoMonto = "#,###,##0.#0";
        string sFormatoPiezas = "#,###,##0";
        string sFolio = "";
        string sMensaje = "";
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;
        bool bValidarPolizasBeneficiarios = false;
        string sObservacionesCancelacion = "";
        bool bPermitirCancelacionTotal = true;
        bool bEsAdministracion = false;
        string sUnidadAtendida = ""; 

        //Para Auditoria
        clsAuditoria auditoria;

        private enum Cols
        {
            Ninguna = 0,
            IdFarmacia = 2, Farmacia = 3, IdPrograma = 4, Programa = 5, IdSubPrograma = 6, SubPrograma = 7,
            ClaveSSA = 8, ClaveSSA_Descripcion = 9, Precio = 10, Cantidad = 11, TasaIva = 12, 
            SubTotalSinGrabar = 13, SubTotalGrabado = 14, Iva = 15, Importe = 16
        }

        public FrmValidarRemisiones()
        {
            InitializeComponent();

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");
            myLeer = new clsLeer(ref ConexionLocal);
            myLeerEncabezado = new clsLeer(ref ConexionLocal);
            myLeerDetalles = new clsLeer(ref ConexionLocal);
            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);
            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            lst = new clsListView(lstDetalles);
            lst.OrdenarColumnas = false;
            
        }

        private void FrmValidarRemisiones_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Buscar Folio
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            lblCancelado.Visible = false;
            lblTipoRemision.Visible = false;

            if (txtFolio.Text.Trim() != "")
            {
                string sSql = string.Format("Exec spp_FACT_Remisiones_Validar \n" +
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', @FolioRemision = '{3}' \n",
                sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text.Trim(), 10));

                dtsRemision = new DataSet();
                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "txtFolio_Validating");
                    General.msjError("Ocurrió un error al obtener la información del Folio de Remision.");
                }
                else
                {
                    dtsRemision.Tables.Add(myLeer.DataSetClase.Tables[0].Copy());
                    myLeerEncabezado.DataTableClase = myLeer.DataSetClase.Tables[0];                    
                    if (myLeerEncabezado.Leer())
                    {
                        CargarDatosEncabezado();
                        CargarDatosDetalle();
                    }
                    else
                    {
                        General.msjUser("El Folio de Remisión ingresado no existe. Verifique");
                        txtFolio.Text = "";
                        txtFolio.Focus();
                    }
                }
            }

        }

        private void CargarDatosEncabezado()
        {
            double dMonto = 0; 

            txtFolio.Text = myLeerEncabezado.Campo("FolioRemision");
            txtFolio.Enabled = false;
            bValidarPolizasBeneficiarios = myLeerEncabezado.CampoBool("ValidarPolizaBeneficiario");

            bEsAdministracion = myLeerEncabezado.CampoInt("TipoDeRemision") == 2;
            lblTipoRemision.Text = myLeerEncabezado.Campo("TipoDeRemision_Descripcion").ToUpper(); 
            lblTipoRemision.Visible = true;

            sUnidadAtendida = myLeerEncabezado.Campo("FarmaciaDispensacion"); 
            lblRubro.Text = myLeerEncabezado.Campo("IdFuenteFinanciamiento");
            lblEstado.Text = myLeerEncabezado.Campo("Estado");
            lblIdConcepto.Text = myLeerEncabezado.Campo("IdFinanciamiento");
            lblConcepto.Text = myLeerEncabezado.Campo("Financiamiento");
            lblIdCliente.Text = myLeerEncabezado.Campo("IdCliente");
            lblCliente.Text = myLeerEncabezado.Campo("Cliente");
            lblIdSubCliente.Text = myLeerEncabezado.Campo("IdSubCliente");
            lblSubCliente.Text = myLeerEncabezado.Campo("SubCliente");
            dtpFechaRemision.Value = myLeerEncabezado.CampoFecha("FechaRemision");            

            dMonto = Math.Round(myLeerEncabezado.CampoDouble("SubTotalSinGrabar_Remision"), 4, MidpointRounding.ToEven);
            lblSubTotalSinGrabar.Text = dMonto.ToString(sFormatoMonto);

            dMonto = Math.Round(myLeerEncabezado.CampoDouble("SubTotalGrabado_Remision"), 4, MidpointRounding.ToEven);
            lblSubTotalGrabado.Text = dMonto.ToString(sFormatoMonto);

            dMonto = Math.Round(myLeerEncabezado.CampoDouble("Iva_Remision"), 4, MidpointRounding.ToEven);
            lblIva.Text = dMonto.ToString(sFormatoMonto);

            dMonto = Math.Round(myLeerEncabezado.CampoDouble("Total_Remision"), 4, MidpointRounding.ToEven);
            lblTotal.Text = dMonto.ToString(sFormatoMonto);

            IniciarToolBar(true, bPermitirCancelacionTotal, true, bValidarPolizasBeneficiarios, true);
            if (myLeerEncabezado.Campo("Status") == "C")
            {
                IniciarToolBar(false, false, false, false, false);
                lblCancelado.Text = "CANCELADO";
                lblCancelado.Visible = true;
                General.msjUser("El Folio de Remisión ingresado se encuentra cancelado. Verifique");
            } 

            if (myLeerEncabezado.CampoBool("EsFacturada") && myLeerEncabezado.Campo("Status") != "C")
            {
                IniciarToolBar(false, false, true, false, true);
                lblCancelado.Text = "FACTURABLE";
                lblCancelado.Visible = true;

                if(DtGeneral.EsAdministrador)
                {
                    IniciarToolBar(false, true, true, false, true);
                }

                General.msjUser("El Folio de Remisión ingresado ya se ha marcado como Facturable. Verifique");
            }
        }

        private void CargarDatosDetalle() 
        {
            lst.LimpiarItems(); 
            myLeerDetalles.DataTableClase = myLeer.DataSetClase.Tables[1];
            if (myLeerDetalles.Leer())
            {
                lst.CargarDatos(myLeerDetalles.DataSetClase, true, true);
                //lst.AlternarColorRenglones(Color.Lavender, Color.LightBlue);
            }
        }
        #endregion Buscar Folio

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bValidarPolizasBeneficiarios = false;
            sObservacionesCancelacion = ""; 

            Fg.IniciaControles();
            IniciarToolBar(false, false, false, false, false);
            lst.Limpiar();
            dtpFechaRemision.Enabled = false;
            lblCancelado.Visible = false;
            lblTipoRemision.Visible = false;

            bEsAdministracion = false;

            lblSubTotalSinGrabar.Text = default_0.ToString(sFormatoMonto);
            lblSubTotalGrabado.Text = default_0.ToString(sFormatoMonto);
            lblIva.Text = default_0.ToString(sFormatoMonto);
            lblTotal.Text = default_0.ToString(sFormatoMonto);
            txtFolio.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (validarDatos())
            {
                if (!ConexionLocal.Abrir())
                {
                    Error.LogError(ConexionLocal.MensajeError);
                    //General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    ConexionLocal.IniciarTransaccion();

                    bContinua = ActualizarRemision();                    

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        General.msjUser("El Folio de Remisión ha sido marcado como facturable satisfactoriamente.");
                        ConexionLocal.CompletarTransaccion();
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la Información."); 
                        IniciarToolBar(true, false, false, bValidarPolizasBeneficiarios, false);                            

                    }

                    ConexionLocal.Cerrar();
                }
            } 
        }

        private bool validarDatos()
        {
            bool bRegresa = false;
            string message = "Este proceso marcará este folio de remisión como Facturable.\n\n¿Desea continuar ?";

            if (General.msjCancelar(message) == DialogResult.Yes)
            {
                bRegresa = true; 
            }

            if (bRegresa)
            {
                if (!bEsAdministracion && bValidarPolizasBeneficiarios)
                {
                    bRegresa = validarFolios_Referencia();
                }
            }

            return bRegresa; 
        }

        private bool validarDatos_Cancelacion()
        {
            bool bRegresa = false;
            string message = "Este proceso cancelara este folio de remisión y enviará todos los registros al repositorio para remisiones.\n\n " + 
                "¿ Desea continuar con la cancelación ?";

            if (General.msjCancelar(message) == DialogResult.Yes)
            {
                bRegresa = true;
            }

            if (bRegresa)
            {
                clsObservaciones ob = new clsObservaciones();
                ob.Encabezado = "Observaciones de Cancelación total de Remisión";
                ob.MaxLength = 200;
                ob.Show();
                sObservacionesCancelacion += "    " + ob.Observaciones;
                bRegresa = ob.Exito;

                if (!bRegresa)
                {
                    General.msjUser("No se capturaron las observaciones para la Cancelación de la remisión, verifique.");
                }
            }

            return bRegresa;
        }

        private bool validarFolios_Referencia()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_FACT_Remisiones_Validar__PolizaBeneficiario '{0}', '{1}', '{2}', '{3}', 1, 0 ",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim());

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(myLeer, "validarFolios_Referencia()");
                General.msjError("Ocurrió un erorr al validar las Polizas."); 
            }
            else
            {
                if (!myLeer.Leer())
                {
                    bRegresa = true; 
                }
                else
                {
                    FrmValidarRemisiones_FoliosInvalidos f = new FrmValidarRemisiones_FoliosInvalidos();
                    f.FolioDeRemision = txtFolio.Text.Trim();
                    f.FoliosInvalidos = myLeer.DataSetClase; 
                    f.MostrarFoliosInvalidos();

                    bRegresa = f.SeRealizaronAjustes; 

                    if ( !bRegresa )
                    {
                        General.msjUser("No se detectarón cambios en la remisión, no es posible marcarla como Facturable."); 
                    }
                }
            }

            return bRegresa; 
        }

        private bool validarFolios_Referencia_Revision()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_FACT_Remisiones_Validar__PolizaBeneficiario '{0}', '{1}', '{2}', '{3}', 1, 0 ",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim());

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(myLeer, "validarFolios_Referencia_Revision()");
                General.msjError("Ocurrió un erorr al validar las Pólizas.");
            }
            else
            {
                if (!myLeer.Leer())
                {
                    General.msjUser("No se encontrarón registros con pólizas inválidas."); 
                }
                else
                {
                    FrmValidarRemisiones_FoliosInvalidos f = new FrmValidarRemisiones_FoliosInvalidos();
                    f.FolioDeRemision = txtFolio.Text.Trim();
                    f.FoliosInvalidos = myLeer.DataSetClase; 
                    f.ShowDialog();

                    if (f.SeRealizaronAjustes)
                    {
                        txtFolio_Validating(null, null); 
                    }
                }
            }

            return bRegresa;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            GenerarExcel();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(false);
            //bool bRegresa = true;

            //DatosCliente.Funcion = "Imprimir()";
            //clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            //myRpt.RutaReporte = DtGeneral.RutaReportes;
            //myRpt.NombreReporte = "FACT_REMISIONES_VALIDAR";

            //myRpt.Add("@IdEmpresa", sEmpresa);
            //myRpt.Add("@IdEstado", sEstado);
            //myRpt.Add("@IdFarmaciaGenera", sFarmacia);
            //myRpt.Add("@FolioRemision", Fg.PonCeros(txtFolio.Text, 10));

            //bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            //if (!bRegresa)
            //{
            //    General.msjError("Ocurrió un error al cargar el reporte.");
            //}
        }


        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            Imprimir(false, true);
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

            //if (bRegresa)
            //{
            //    if (txtPedido.Text.Trim() == "" || txtPedido.Text.Trim() == "*")
            //    {
            //        bRegresa = false;
            //        General.msjUser("Folio de pedido inválido, verifique.");
            //    }
            //}

            return bRegresa;
        }

        private void Imprimir(bool Confirmacion)
        {
            Imprimir(Confirmacion, false);
        }

        private void Imprimir(bool Confirmacion, bool Exportar)
        {
            bool bRegresa = true;
            //dImporte = Importe; 

            if (validarImpresion(Confirmacion))
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;
                string sNombre = string.Format("RM-{0}-{1}.pdf", txtFolio.Text, sUnidadAtendida.Replace(" ", "_"));

                myRpt.RutaReporte = DtGeneral.RutaReportes;
                myRpt.NombreReporte = "FACT_REMISIONES__28_SST";

                myRpt.Add("@IdEmpresa", sEmpresa);
                myRpt.Add("@IdEstado", sEstado);
                myRpt.Add("@IdFarmaciaGenera", sFarmacia);
                myRpt.Add("@FolioRemision", Fg.PonCeros(txtFolio.Text, 10));

                //bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
                if (Exportar)
                {
                    bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, sNombre, FormatosExportacion.PortableDocFormat);
                }
                else
                {
                    // bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, sNombre, FormatosExportacion.PortableDocFormat); 
                    bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
                }

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void btnValidarPolizasBeneficiarios_Click(object sender, EventArgs e)
        {
            validarFolios_Referencia_Revision();

            //FrmValidarRemisiones_FoliosInvalidos f = new FrmValidarRemisiones_FoliosInvalidos();
            //f.FolioDeRemision = txtFolio.Text.Trim(); 
            //f.ShowDialog(); 
        }

        private void btnCancelarRemision_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (validarDatos_Cancelacion())
            {
                if (!ConexionLocal.Abrir())
                {
                    Error.LogError(ConexionLocal.MensajeError);
                    //General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    ConexionLocal.IniciarTransaccion();

                    bContinua = CancelarRemisionCompleta(); 

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        General.msjUser("El Folio de Remisión ha sido cancelado satisfactoriamente.");
                        ConexionLocal.CompletarTransaccion();
                        //btnNuevo_Click(null, null);
                        txtFolio_Validating(null, null); 
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnCancelarRemision_Click");
                        General.msjError("Ocurrió un error al cancelar la remisión.");
                        IniciarToolBar(true, false, false, bValidarPolizasBeneficiarios, false);

                    }

                    ConexionLocal.Cerrar();
                }
            }            
        }
        #endregion Botones

        #region Funciones
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Exportar, bool ValidarPolizas, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelarRemision.Enabled = Cancelar;
            btnExportar.Enabled = Exportar;
            btnValidarPolizasBeneficiarios.Enabled = ValidarPolizas;
            btnImprimir.Enabled = Imprimir;
            btnExportarPDF.Enabled = Imprimir; 

            if (bEsAdministracion)
            {
                btnValidarPolizasBeneficiarios.Enabled = false; 
            }
        }

        private bool ActualizarRemision()
        {
            bool bRegresa = true;            
            string sSql = string.Format("Update FACT_Remisiones " + 
                " Set EsFacturable = 1, FechaValidacion = GetDate(), IdPersonalValida = '{0}', Actualizado = 0 " +
                " Where IdEmpresa = '{1}' And IdEstado = '{2}' And IdFarmaciaGenera = '{3}' And FolioRemision = '{4}'  ",
                sPersonal, sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim());

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolio = myLeer.Campo("Folio");
                sMensaje = myLeer.Campo("Mensaje");
            }

            return bRegresa;
        }

        private bool CancelarRemisionCompleta()
        {
            bool bRegresa = false;
            string sSql = ""; 

            sSql = string.Format("Exec spp_FACT_Remisiones_Cancelacion_Completa  " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', @FolioRemision = '{3}', @IdPersonalCancela = '{4}', @Observaciones = '{5}' ",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), sPersonal, sObservacionesCancelacion);

            bRegresa = myLeer.Exec(sSql); 

            return bRegresa;
        }
        private void GenerarExcel()
        {
            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\FACT_REMISIONES_VALIDAR.xls";
            bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "FACT_REMISIONES_VALIDAR.xls", DatosCliente);

            if (bRegresa)
            {
                xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                xpExcel.AgregarMarcaDeTiempo = true;
                myLeer.DataSetClase = dtsRemision;

                this.Cursor = Cursors.Default;
                if (xpExcel.PrepararPlantilla())
                {
                    this.Cursor = Cursors.WaitCursor;
                    ExportarMovimientos();

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
            int iHoja = 1, iRenglon = 15;            
            string sFechaImpresion = "", sDetalle = "";

            myLeer.DataSetClase = dtsRemision;
            xpExcel.GeneraExcel(iHoja);

            //Encabezado Reporte.
            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
            xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, 3, 2);

            sFechaImpresion = string.Format("Fecha de Impresión: {0} ", General.FechaSistema);
            xpExcel.Agregar(sFechaImpresion, 5, 2);

            //Encabezado Remision 
            xpExcel.Agregar(txtFolio.Text.Trim(), 7, 3);

            sDetalle = string.Format("{0} - {1} ",lblRubro.Text.Trim(), lblEstado.Text.Trim());
            xpExcel.Agregar(sDetalle, 8, 3);

            sDetalle = string.Format("{0} - {1} ", lblIdConcepto.Text.Trim(), lblConcepto.Text.Trim());
            xpExcel.Agregar(sDetalle, 9, 3);

            sDetalle = string.Format("{0} - {1} ", lblIdCliente.Text.Trim(), lblCliente.Text.Trim());
            xpExcel.Agregar(sDetalle, 10, 3);

            sDetalle = string.Format("{0} - {1} ", lblIdSubCliente.Text.Trim(), lblSubCliente.Text.Trim());
            xpExcel.Agregar(sDetalle, 11, 3);

            xpExcel.Agregar(lblSubTotalSinGrabar.Text.Trim(), 8, 5);
            xpExcel.Agregar(lblSubTotalGrabado.Text.Trim(), 9, 5);
            xpExcel.Agregar(lblIva.Text.Trim(), 10, 5);
            xpExcel.Agregar(lblTotal.Text.Trim(), 11, 5);

            //Detalles Remision
            while (myLeer.Leer())
            {
                xpExcel.Agregar(myLeer.Campo("IdFarmacia"), iRenglon, (int)Cols.IdFarmacia);
                xpExcel.Agregar(myLeer.Campo("Farmacia"), iRenglon, (int)Cols.Farmacia);
                xpExcel.Agregar(myLeer.Campo("IdPrograma"), iRenglon, (int)Cols.IdPrograma);
                xpExcel.Agregar(myLeer.Campo("Programa"), iRenglon, (int)Cols.Programa);
                xpExcel.Agregar(myLeer.Campo("IdSubPrograma"), iRenglon, (int)Cols.IdSubPrograma);
                xpExcel.Agregar(myLeer.Campo("SubPrograma"), iRenglon, (int)Cols.SubPrograma);
                xpExcel.Agregar(myLeer.Campo("ClaveSSA"), iRenglon, (int)Cols.ClaveSSA);
                xpExcel.Agregar(myLeer.Campo("ClaveSSA_Descripcion"), iRenglon, (int)Cols.ClaveSSA_Descripcion);
                xpExcel.Agregar(myLeer.Campo("PrecioLicitado"), iRenglon, (int)Cols.Precio);
                xpExcel.Agregar(myLeer.Campo("Cantidad"), iRenglon, (int)Cols.Cantidad);
                xpExcel.Agregar(myLeer.Campo("TasaIva"), iRenglon, (int)Cols.TasaIva);
                xpExcel.Agregar(myLeer.Campo("SubTotalSinGrabar"), iRenglon, (int)Cols.SubTotalSinGrabar);
                xpExcel.Agregar(myLeer.Campo("SubTotalGrabado"), iRenglon, (int)Cols.SubTotalGrabado);
                xpExcel.Agregar(myLeer.Campo("Iva"), iRenglon, (int)Cols.Iva);
                xpExcel.Agregar(myLeer.Campo("Importe"), iRenglon, (int)Cols.Importe);

                iRenglon++;
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();

        }
        #endregion Funciones      
    }
}
