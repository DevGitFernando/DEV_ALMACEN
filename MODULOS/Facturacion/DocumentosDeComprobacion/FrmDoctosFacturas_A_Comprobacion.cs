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
using DllFarmaciaSoft; 

namespace Facturacion.DocumentosDeComprobacion
{
    public partial class FrmDoctosFacturas_A_Comprobacion : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer, leerDetalles;
        clsGrid grid;
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;
        //FrmFuentesDeFinaciamiento_Claves Claves;

        string sFormatoMonto = "###, ###, ###, ###, ###, ##0.###0";
        string sFormatoPiezas = "###, ###, ###, ###, ###, ##0"; 
        string sFolio = "";
        string sMensaje = "";
        bool bInicioDeModulo = true;
        bool bRevisandoSeleccion = false;
        bool bCapturaDeClavesHabilitada = false;
        int iTipoClasificacionSSA = 0; 
        // bool bFolioGuardado = false;
        
        //Para Auditoria
        clsAuditoria auditoria;

        private enum Cols
        {
            Ninguna = 0,
            //IdClave = 1, Descripcion = 2, Monto = 3, Piezas = 4, Activar = 5, Guardado = 6, 
            //ValidarPoliza = 7, LargoMinimo = 8, LargoMaximo = 9 

            IdClave = 1, Descripcion, 
            Alias, 
            Prioridad, Monto, Piezas, Activar, Guardado, 
            ValidarPoliza, LargoMinimo, LargoMaximo
        }

        public FrmDoctosFacturas_A_Comprobacion()
        {
            InitializeComponent();

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");
            leer = new clsLeer(ref ConexionLocal);
            leerDetalles = new clsLeer(ref ConexionLocal);
            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);

            grid = new clsGrid(ref grdClaves, this);
            grid.BackColorColsBlk = Color.White;
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            grid.AjustarAnchoColumnasAutomatico = true; 
            grdClaves.EditModeReplace = true;
           
        }

        private void FrmFuentesDeFinaciamiento_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            bInicioDeModulo = false; 
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bInicioDeModulo = true; 
            Fg.IniciaControles();
            FrameTipoDeUnidades.Enabled = true; 

            rdoTipoUnidades_01_Ordinarias.Checked = true; 
            txtSerie.ReadOnly = false; 
            txtFolio_CFDI.ReadOnly = false;
            txtFolio_CFDI_Relacionado.ReadOnly = false;

            IniciarToolBar(false, false, false);
            grid.Limpiar(false);
            bCapturaDeClavesHabilitada = false; 

            bInicioDeModulo = false; 
            txtFolio.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (ValidaDatos())
            {
                if (!ConexionLocal.Abrir())
                {
                    Error.LogError(ConexionLocal.MensajeError);
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    ConexionLocal.IniciarTransaccion();

                    bContinua = GuardarEncabezado();

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        txtFolio.Text = sFolio;
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        IniciarToolBar(false, false, true);
                        btnImprimir_Click(null, null);
                    }
                    else
                    {
                        txtFolio.Text = "*";
                        txtFolio.Text = sFolio; 
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la Información.");
                        IniciarToolBar(true, false, false);  

                    }

                    ConexionLocal.Cerrar();
                }
            }            

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ////FrmFuentesDeFinaciamiento_ImportarConfiguraciones f = new FrmFuentesDeFinaciamiento_ImportarConfiguraciones();

            ////f.ShowInTaskbar = false;
            ////f.ShowDialog(this);
        }
        private void btnImportarConfiguraciones_Click( object sender, EventArgs e )
        {
            ////FrmFuentesDeFinaciamiento_ImportarConfiguraciones f = new FrmFuentesDeFinaciamiento_ImportarConfiguraciones();

            ////f.ShowInTaskbar = false; 
            ////f.ShowDialog(this);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ////bool bRegresa = false;
            ////DatosCliente.Funcion = "btnImprimir_Click()";
            ////clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            ////myRpt.RutaReporte = DtGeneral.RutaReportes;
            ////myRpt.NombreReporte = "FACT_FuentesDeFinanciamiento.rpt";

            ////myRpt.Add("Folio", txtFolio.Text);

            ////bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            ////if (bRegresa)
            ////{
            ////    btnNuevo_Click(null, null);
            ////}
            ////else
            ////{
            ////    General.msjError("Ocurrió un error al cargar el reporte.");
            ////}
            
        }
        #endregion Botones

        #region Buscar Folio
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bCargarInformacion = true; 

            if (txtFolio.Text.Trim() == "")
            {
                sFolio = "*";
                txtFolio.Text = "*";
                txtFolio.Enabled = false;
                IniciarToolBar(true, false, false);

                //grid.AddRow();
                //grid.SetValue(1, (int)Cols.IdClave, "*");
            }
            else
            {
                leer.DataSetClase = Consultas.Comprobacion_Facturas(txtFolio.Text.Trim(), "txtFolio_Validating");
                if (!leer.Leer())
                {
                    txtFolio.Text = "";
                    txtFolio.Focus();
                }
                else 
                {
                    CargarDatosEncabezado();
                }
            }

        }

        private void CargarDatosEncabezado()
        {
            double dMonto = 0;
            int iPiezas = 0;
            int iTipoDeFuenteDeFinanciamiento = 0;
            int iTipoDeUnidades = 0;
            iTipoClasificacionSSA = 0;

            bRevisandoSeleccion = true; 
            txtFolio.Text = leer.Campo("FolioRelacion");
            sFolio = txtFolio.Text;
            txtFolio.Enabled = false;

            chkFacturaEnCajas.Checked = leer.CampoBool("EsFacturaEnCajas");
            txtSerie.Text = leer.Campo("Serie");
            txtFolio_CFDI.Text = leer.Campo("Folio");
            txtFolio_CFDI_Relacionado.Text = leer.Campo("Folio_Relacionado");

            iTipoDeUnidades = leer.CampoInt("TipoDeUnidades");
            rdoTipoUnidades_01_Ordinarias.Checked = iTipoDeUnidades == 1 ? true : false;
            rdoTipoUnidades_02_DosisUnitaria.Checked = iTipoDeUnidades == 2 ? true : false;

            lblCFDI_FechaExpedicion.Text = string.Format("{0}", leer.CampoFecha("FechaRegistro"));
            lblCFDI_FuenteFinanciamiento.Text = leer.Campo("FuenteFinanciamiento");
            lblCFDI_Financiamiento.Text = leer.Campo("Financiamiento");
            lblCFDI_ClienteNombre.Text = leer.Campo("Cliente");
            lblCFDI_SubClienteNombre.Text = leer.Campo("SubCliente");
            lblCFDI_TipoDocumentoDescripcion.Text = leer.Campo("TipoDocumentoDescripcion");
            lblCFDI_TipoDeInsumoDescripcion.Text = leer.Campo("TipoInsumoDescripcion");


            bCapturaDeClavesHabilitada = true;

            Validar_CFDI(); 

            IniciarToolBar(false, false, false);

            Fg.BloqueaControles(this, false, FramePrincipal);
            ////txtAlias.Enabled = true; 
            bRevisandoSeleccion = false; 
        }

        private void CargarDatosDetalle()
        {
            leerDetalles.DataSetClase = Consultas.FuentesDeFinanciamiento_Detalle(txtFolio.Text.Trim(), "txtFolio_Validating");
            if (leerDetalles.Leer())
            {
                grid.LlenarGrid(leerDetalles.DataSetClase, false, false); 
            } 
        }
        #endregion Buscar Folio

        #region Funciones
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnImportarConfiguraciones.Enabled = Guardar; 
        }




        private bool GuardarEncabezado()
        {
            bool bRegresa = true;
            int iOpcion = 1;
            int iTipoClasificacionSSA = 1;
            string sSql = "";
            int iTipoDeUnidades = rdoTipoUnidades_01_Ordinarias.Checked ? 1 : 2;


            ////if (rdoExclusivoControlados.Checked) iTipoClasificacionSSA = 1;
            ////if (rdoExclusivoLibres.Checked) iTipoClasificacionSSA = 2;
            ////if (rdoTodos.Checked) iTipoClasificacionSSA = 3;

            ////string sSql = string.Format("Exec spp_Mtto_FACT_Fuentes_De_Financiamiento \n" + // '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, {8}, '{9}'  ",
            ////    "\t@IdFuenteFinanciamiento = '{0}', @IdEstado = '{1}', @IdCliente = '{2}', @IdSubCliente = '{3}', @NumeroDeContrato = '{4}', @NumeroDeLicitacion = '{5}', \n" +
            ////    "\t@FechaInicial = '{6}', @FechaFinal = '{7}', @iMonto = '{8}', @iPiezas = '{9}', @iOpcion = '{10}', @TipoClasificacionSSA = '{11}', @Alias = '{12}', @EsParaExcedente = '{13}', \n" + 
            ////    "\t@EsDiferencial = '{14}' ", 
            ////    txtFolio.Text.Trim(), cboEstados.Data, txtCliente.Text.Trim(), txtSubCliente.Text.Trim(), txtNumeroDeContrato.Text.Trim(), txtNumeroDeLicitacion.Text.Trim(), 
            ////    dtpFechaInicial.Text.Trim(), dtpFechaFinal.Text.Trim(),
            ////    FormatearValorNumerico(lblMonto.Text.Trim()), FormatearValorNumerico(lblPiezas.Text.Trim()), iOpcion, iTipoClasificacionSSA, txtAlias.Text.Trim(), 
            ////    Convert.ToInt32(rdoTipoFF_02_Excedente.Checked), Convert.ToInt32(chkEsDiferencial.Checked) 
            ////    );

            if (txtFolio_CFDI_Relacionado.Text.Trim() == "")
            {
                txtFolio_CFDI_Relacionado.Text = txtFolio_CFDI.Text.Trim(); 
            }

            sSql = string.Format("Exec spp_FACT_Comprobacion_Facturas \n" +
                "\t@FolioRelacion = '{0}', @IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', " +
                "\t@Serie = '{4}', @Folio = '{5}', @Serie_Relacionada = '{6}', @Folio_Relacionado = '{7}', @FacturaEnCajas = '{8}', \n" +
                "\t@TipoDeUnidades = '{9}' \n",
                txtFolio.Text.Trim(), 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                txtSerie.Text.Trim(), txtFolio_CFDI.Text.Trim(),
                txtSerie.Text.Trim(), txtFolio_CFDI_Relacionado.Text.Trim(), 
                Convert.ToInt32(chkFacturaEnCajas.Checked),
                iTipoDeUnidades
                );

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sFolio = leer.Campo("Folio");
                sMensaje = leer.Campo("Mensaje");
            }

            return bRegresa;
        }

        private bool GuardarDetalle()
        {
            bool bRegresa = true; 
            bool bActivar = true;
            string sSql = "";
            string sIdFinanciamiento = "";
            string sDescripcion = "";
            string sAlias = ""; 
            int iPiezas = 0; 
            int iOpcion = 0;
            double dMonto = 0;
            int iPrioridad = 0; 


            for (int i = 1; i <= grid.Rows; i++)
            {
                sIdFinanciamiento = grid.GetValue(i, (int)Cols.IdClave);
                sDescripcion = grid.GetValue(i, (int)Cols.Descripcion);
                sAlias = grid.GetValue(i, Cols.Alias); 
                dMonto = grid.GetValueDou(i, (int)Cols.Monto);
                iPiezas = grid.GetValueInt(i, (int)Cols.Piezas);
                iPrioridad = grid.GetValueInt(i, (int)Cols.Prioridad);
                bActivar = grid.GetValueBool(i, (int)Cols.Activar);


                iOpcion = 1;
                if (!bActivar)
                {
                    //Si no esta activo, significa que es una cancelacion.
                    iOpcion = 2;
                }

                if (sDescripcion != "") 
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FACT_Fuentes_De_Financiamiento_Detalles \n" +
                        "\t@IdFuenteFinanciamiento = '{0}', @IdFinanciamiento = '{1}', @Descripcion = '{2}', \n" +
                        "\t@iMonto = '{3}', @iPiezas = '{4}', @Prioridad = '{5}', @Alias = '{6}', @iOpcion = '{7}' \n",
                        sFolio, sIdFinanciamiento, sDescripcion, dMonto, iPiezas, iPrioridad, sAlias, iOpcion);
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }


        private bool ValidaDatos()
        {
            bool bRegresa = true;



            return bRegresa;
        }
        #endregion Funciones  

        #region Documentos de Facturas 
        private void txtSerie_TextChanged(object sender, EventArgs e)
        {
            //txtFolio_CFDI.Text = "";
            limpiarDatos_CFDI();
        }
        private void txtSerie_Validating(object sender, CancelEventArgs e)
        {

        }
        private void txtFolio_CFDI_TextChanged(object sender, EventArgs e)
        {
            limpiarDatos_CFDI(); ;
        }
        private void txtFolio_CFDI_Validating(object sender, CancelEventArgs e)
        {
            if (txtSerie.Text.Trim() != "" && txtFolio_CFDI.Text.Trim() != "")
            {
                e.Cancel = !validarInformacionDeCFDI();
            }
        }

        private bool validarInformacionDeCFDI()
        {
            bool bRegresa = false;
            string sSql = string.Format("Select * \n" +
                    "From vw_FACT_CFD_DocumentosElectronicos (NoLock) \n" +
                    "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Serie = '{3}' And Folio = '{4}' \n",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtSerie.Text.Trim(), txtFolio_CFDI.Text.Trim()
                    ); // + Fg.PonCeros(IdCliente, 4) + "'";

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "validarInformacionDeCFDI");
                General.msjError("Ocurrió un error al validar la información del Documento Electrónico.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("Información de Documento CFDI no encontrada, verifique.");
                }
                else
                {
                    bRegresa = true;

                    if (bRegresa && leer.Campo("IdTipoDocumento") != "001")
                    {
                        bRegresa = false;
                        General.msjAviso("El documento electrónico no del tipo Factura, verifique.");
                        txtSerie.Focus();
                    }

                    if (bRegresa && leer.Campo("StatusDocto").ToUpper() != "A")
                    {
                        bRegresa = false;
                        General.msjAviso("El documento electrónico no cuenta con Status Activo, verifique.");
                        txtSerie.Focus();
                    }

                    if (bRegresa && leer.CampoBool("EsRelacionConRemisiones"))
                    {
                        bRegresa = false;
                        General.msjAviso("El documento electrónico fue generado con relación de remisiones, es inválido para su procesamiento, verifique.");
                        txtSerie.Focus();
                    }

                    if (bRegresa)
                    {
                        txtSerie.Enabled = false;
                        txtFolio_CFDI.Enabled = false; 

                        lblCFDI_FechaExpedicion.Text = string.Format("{0}", leer.CampoFecha("FechaRegistro"));
                        lblCFDI_ClienteNombre.Text = leer.Campo("NombreReceptor");
                        lblCFDI_FuenteFinanciamiento.Text = leer.Campo("FuenteFinanciamiento");
                        lblCFDI_Financiamiento.Text = leer.Campo("Financiamiento");
                        lblCFDI_TipoDocumentoDescripcion.Text = leer.Campo("TipoDocumentoDescripcion");
                        lblCFDI_TipoDeInsumoDescripcion.Text = leer.Campo("TipoInsumoDescripcion");

                        Validar_CFDI(); 
                    }
                }
            }

            return bRegresa;
        }

        private bool Validar_CFDI()
        {
            bool bRegresa = true;

            if (txtSerie.Text.Trim() != "" && txtFolio_CFDI.Text.Trim() != "")
            {
                bRegresa = Validar_CFDI_Procesar(); 
            }

            return bRegresa; 
        }
        private bool Validar_CFDI_Procesar()
        {
            bool bRegresa = false;
            string sFolioCFDI = txtFolio_CFDI_Relacionado.Text.Trim() != "" ? txtFolio_CFDI_Relacionado.Text.Trim() : txtFolio_CFDI.Text.Trim();

            ////sFolioCFDI = txtFolio_CFDI.Text.Trim(); 
            ////if (txtFolio_CFDI_Relacionado.Text.Trim() != "")
            ////{
            ////    if (txtFolio_CFDI_Relacionado.Text.Trim() != txtFolio_CFDI.Text.Trim())
            ////    {
            ////        sFolioCFDI = txtFolio_CFDI_Relacionado.Text.Trim();
            ////    }
            ////}

            string sSql = string.Format("Exec spp_FACT_Comprobacion_Facturas__Validar \n" +
                "\t@IdEstado = '{0}', @IdFarmacia = '{1}', @Serie = '{2}', @Folio = '{3}', \n" +
                "\t@Serie_Relacionada = '{4}', @Folio_Relacionado = '{5}'," + 
                "\t@FacturaEnCajas = '{6}', @FolioRelacion = '{7}', @Integrar = '{8}' \n",
                //DtGeneral.EmpresaConectada, 
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                txtSerie.Text.Trim(), txtFolio_CFDI.Text.Trim(),
                txtSerie.Text.Trim(), sFolioCFDI,
                Convert.ToInt32(chkFacturaEnCajas.Checked), txtFolio.Text.Trim(), 0 
                );

            grid.Limpiar(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Validar_CFDI()");
                General.msjError("Ocurrió un error al validar los datos de la Factura.");
            }
            else
            {
                if (leer.Leer())
                {
                    if (leer.CampoBool("EsRegistrada"))
                    {
                        btnGuardar.Enabled = false;
                        chkFacturaEnCajas.Enabled = false;
                        txtFolio.Text = leer.Campo("FolioRelacion");
                    }

                    grid.LlenarGrid(leer.DataSetClase);
                }
            }

            return bRegresa; 
        }

        private void chkFacturaEnCajas_CheckedChanged(object sender, EventArgs e)
        {
            Validar_CFDI(); 
        }

        private void txtFolio_CFDI_Relacionado_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolio_CFDI_Relacionado.Text.Trim() != "")
            {
                txtFolio_CFDI_Relacionado.Enabled = false; 
            }
        }

        private void limpiarDatos_CFDI()
        {
            lblCFDI_FechaExpedicion.Text = "";
            lblCFDI_ClienteNombre.Text = "";
            lblCFDI_FuenteFinanciamiento.Text = "";
            lblCFDI_Financiamiento.Text = "";
            lblCFDI_TipoDocumentoDescripcion.Text = "";
            lblCFDI_FechaExpedicion.Text = "";
            lblCFDI_TipoDeInsumoDescripcion.Text = "";
        }
        #endregion Documentos de Facturas 
    }
}
