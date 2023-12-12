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

using Dll_IFacturacion;


namespace Facturacion.GenerarRemisiones
{
    public partial class FrmRemisionesGenerales_04_FolioVentaDirecta : FrmBaseExt
    {
        enum cols 
        { 
            ninguno, 
            CodigoEAN, Idproducto, Descripcion, CantidadVendida, CantidadRemision_Insumo, CantidadRemision_Admon, 
            PorRemisionar_Insumo, PorRemisionar_Admon 
        }

        enum ColsRemisiones
        {
            Ninguno = 0, 
            FolioRemision = 1, 
            Fecha, 
            FuenteFinancimiento, 
            ClaveFinanciamiento, 
            Financiamiento, 
            ClaveRemision,
            TipoDeRemision, 
            Importe,
            Seleccionar  
        }

        enum Cols_FF
        {
            Ninguna = 0,
            IdFuenteFinanciento = 1,
            TipoDeFuente,
            EsDiferencial,
            IdFinanciamiento,
            Financiamiento,
            IdCliente, Cliente,
            IdSubCliente, SubCliente
        }

        enum Cols_FoliosVenta
        {
            Ninguna = 0,
            IdAlmacen = 1, 
            Folio,
            Fecha,
            IdCliente, Cliente,
            IdSubCliente, SubCliente
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leerDetalles;
        clsLeer leerRemisiones; 
        clsGrid myGrid;
        clsGrid gridUnidades;
        clsGrid gridFolios;
        clsGrid gridFF; 

        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;
        DataSet dtsFarmacias;
        DataSet dtsDatos = new DataSet();
        eEsquemaDeFacturacion tpFacturacion = DtIFacturacion.EsquemaDeFacturacion;
        eTipoRemision tpTipoDeRemision = eTipoRemision.Ninguno;
        bool bEsquemaDeFacturacionValido = false;

        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sFolio = "", sMensaje = "";
        double dMontoFinanciamiento = 0;
        bool bEsExcedente_Rubro = false;
        bool bEsExcedente_Concepto = false;
        string sFormato = "#,###,###,##0.###0";
        string sIdentificador = "";

        //Para Auditoria
        clsAuditoria auditoria;

        //Hilos
        Thread _workerThread;
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;
        bool bFrameUnidades = true;
        bool bFrameFolios = false;

        string sListaDeSeleccion = "";
        DataSet dtsProgramas_SubProgramas = new DataSet();

        string sTipoDe_FuenteDeFinanciamiento = "";
        bool bEsDiferencial = false; 

        FrmDescargarVenta info;
        List<string> listaGuids = new List<string>();
        string sGUID = "";
        string sUrl = ""; 

        public FrmRemisionesGenerales_04_FolioVentaDirecta()
        {
            InitializeComponent();

            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");
            leer = new clsLeer(ref cnn);
            leerDetalles = new clsLeer(ref cnn);
            leerRemisiones = new clsLeer(ref cnn); 

            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);


            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);


            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.AjustarAnchoColumnasAutomatico = true; 

            gridUnidades = new clsGrid(ref grdUnidades, this);
            gridUnidades.AjustarAnchoColumnasAutomatico = true;

            gridFF = new clsGrid(ref grdFuentesDeFinanciamiento, this);
            gridFF.AjustarAnchoColumnasAutomatico = true;


            gridFolios = new clsGrid(ref grdListaDeFoliosDeVenta, this);
            gridFolios.AjustarAnchoColumnasAutomatico = true;


            CargarFormatosDeImpresion(); 
            CargarAlmacenes();
            CargarNivelDeInformacion();
            CargarTiposDeBeneficiarios();
            CargarOrigenDeDispensacion(); 
         }

        private void FrmRemisionesGenerales_04_FolioVentaDirecta_Load( object sender, EventArgs e )
        {
            InicializarPantalla();
        }


        #region Botones 
        private void InicializarPantalla()
        {
            Fg.IniciaControles();
            listaGuids = new List<string>();
            gridFF.Limpiar(false);
            gridFolios.Limpiar(); 


            btnEjecutar.Enabled = true;
            btnGenerarRemisiones.Enabled = true;
            btnExportarPDF.Enabled = false;
            //Frame_01_InformacionVenta.Enabled = true;

            rdo_TP_01_FolioEspecifico.Checked = true; 
            chkVentaDirecta.Checked = false;
            nmPorcentaje.Text = "0.00";

            BloquearControles(true, Frame_01_InformacionVenta);
            txtFactura_Folio.Enabled = false;
            txtFactura_Serie.Enabled = false;

            txtFactura_Folio.Text = "";
            txtFactura_Serie.Text = "";



            myGrid.Limpiar(false);
            gridUnidades.Limpiar(false);

            SetFrames(true);
            cboAlmacen.Focus();
        }

        private void SetFrames( bool Habilitar )
        {
            //Frame_02_FuentesFinanciamiento.Enabled = Habilitar;
            //Frame_03_InformacionGeneral.Enabled = Habilitar;
            //Frame_04_TipoRemision.Enabled = Habilitar;
            //Frame_05_OrigenInsumo.Enabled = Habilitar;
            //Frame_06_TipoInsumo.Enabled = Habilitar;
            //Frame_07_OrigenDispensacion.Enabled = Habilitar;
            //Frame_08_FacturasAnticipadas.Enabled = Habilitar;

            BloquearControles(Habilitar, Frame_02_FuentesFinanciamiento);
            BloquearControles(Habilitar, Frame_03_InformacionGeneral);
            BloquearControles(Habilitar, Frame_04_TipoRemision);
            BloquearControles(Habilitar, Frame_05_OrigenInsumo);
            BloquearControles(Habilitar, Frame_06_TipoInsumo);
            BloquearControles(Habilitar, Frame_07_OrigenDispensacion);
            BloquearControles(Habilitar, Frame_08_FacturasAnticipadas);
            BloquearControles(Habilitar, Frame_09_VentaDirecta);
            BloquearControles(Habilitar, Frame_10_FormatosDeImpresion); 
        }

        private void BloquearControles( bool Habilitar, GroupBox Frame )
        {
            foreach(Control obj in Frame.Controls)
            {
                if(obj is TextBox)
                {
                    ((TextBox)obj).ReadOnly = !Habilitar;
                }

                if(obj is ComboBox)
                {
                    ((ComboBox)obj).Enabled = Habilitar;
                }

                if(obj is CheckBox)
                {
                    ((CheckBox)obj).Enabled = Habilitar;
                }

                if(obj is DateTimePicker)
                {
                    ((DateTimePicker)obj).Enabled = Habilitar;
                }
                if (obj is NumericUpDown)
                {
                    ((NumericUpDown)obj).Enabled = Habilitar;
                }
            }
        }

        private void btnNuevo_Click( object sender, EventArgs e )
        {
            InicializarPantalla();
        }


        private void btnEjecutar_Click( object sender, EventArgs e )
        {
            if(validarInformacion_AlmacenFolioDeVenta())
            {
                txtFolio.Text = Fg.PonCeros(txtFolio.Text, 8);
                info = new FrmDescargarVenta(sUrl, sIdEmpresa, sIdEstado, cboAlmacen.Data, txtFolio.Text);
                if (info.Descargar())
                {
                    if (InformacioDeVenta())
                    {
                        SetFrames(true);
                        btnEjecutar.Enabled = false;
                        btnGenerarRemisiones.Enabled = true;
                        chkEsComplemento.Checked = chkOrigenInformacion.Checked;
                        chkEsComplemento.Enabled = false; 
                        //Frame_01_InformacionVenta.Enabled = false;
                        BloquearControles(false, Frame_01_InformacionVenta);
                    }
                }
                else
                {
                    sMensaje = info.sMensaje;
                }
            }
        }

        private void btnGenerarRemisiones_Click( object sender, EventArgs e )
        {
            bool bContinua = true;

            if (!chkVentaDirecta.Checked)
            {
                bContinua = ValidarDatos();
            }
            else
            {
                bContinua = ValidarDatos_VentaDirecta();
            }




            if (bContinua)
            {
                if(GenerarRemisiones())
                {
                    if(rdo_TP_01_FolioEspecifico.Checked)
                    {
                        bContinua = InformacioDeVenta__FoliosDeRemision();
                    }


                    General.msjUser("Remisiones generadas satisfactoriamente.");


                    if(chkGenerarDocumentos.Checked)
                    {
                        Imprimir();
                    }
                }
            }


            if(bContinua)
            {
                if(!chkVentaDirecta.Checked)
                {
                    if(InformacioDeVenta())
                    {
                        if(ValidarTotales())
                        {
                            General.msjAviso("Se encontraron claves pendientes de remisionar.");
                        }
                    }
                }
            }
        }

        private void btnExportarPDF_Click( object sender, EventArgs e )
        {
            bool bregresa = true;

            if(chkGenerarDocumentos.Checked)
            {
                if(cboFormatosDeImpresion.SelectedIndex == 0)
                {
                    bregresa = false;
                    General.msjUser("No ha seleccionado un formato de impresión, verifique.");
                    cboFormatosDeImpresion.Focus();
                }
            }

            if(bregresa)
            {
                Imprimir();
            }
        }
        #endregion Botones 

        #region Combos 
        private void CargarFormatosDeImpresion()
        {
            string sSql = string.Format(
                "Select NombreFormato, DescripcionDeUso \n" +
                "From FACT_Remisiones_FormatosDeImpresion (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' \n" +
                "Order by Orden ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            cboFormatosDeImpresion.Clear();
            cboFormatosDeImpresion.Add("0", "<< Seleccione >>");

            if(leer.Exec(sSql))
            {
                if(leer.Leer())
                {
                    cboFormatosDeImpresion.Add(leer.DataSetClase, true, "NombreFormato", "DescripcionDeUso");
                }
            }

            cboFormatosDeImpresion.SelectedIndex = 0;
        }

        private void CargarAlmacenes()
        {
            string sFiltro = "EsAlmacen = '1' ";

            dtsFarmacias = new DataSet();
            dtsFarmacias = Consultas.Farmacias_Urls(sIdEstado, "ObtenerFarmacias");

            cboAlmacen.Clear();
            cboAlmacen.Add("0", "<< Seleccione >>");
            cboAlmacen.Add(dtsFarmacias.Tables[0].Select(sFiltro, "NombreFarmacia"), true, "IdFarmacia", "NombreFarmacia");
            cboAlmacen.SelectedIndex = 0;

            cboAlmacen_MultiplesVentas.Clear();
            cboAlmacen_MultiplesVentas.Add("0", "<< Seleccione >>");
            cboAlmacen_MultiplesVentas.Add(dtsFarmacias.Tables[0].Select(sFiltro, "NombreFarmacia"), true, "IdFarmacia", "NombreFarmacia");
            cboAlmacen_MultiplesVentas.SelectedIndex = 0;

        }

        private void CargarNivelDeInformacion()
        {
            //  ----   1 => General (Primer nivel de informacion) | 2 ==> Farmacia FF (Segundo nivel de informacion) | 3 ==> Ventas directas por Jurisdiccion  

            cboNivelDeInformacion.Clear();
            cboNivelDeInformacion.Add("0", "<< Seleccione >>");
            cboNivelDeInformacion.Add("1", "General (Primer nivel de informacion)");
            cboNivelDeInformacion.Add("2", "Farmacia FF (Segundo nivel de informacion)");
            cboNivelDeInformacion.Add("3", "Ventas directas por Jurisdiccion");

            cboNivelDeInformacion.SelectedIndex = 0;
        }

        private void CargarTiposDeBeneficiarios()
        {
            // ----   0 ==> Todos | 1 ==> General <Solo farmacia> | 2 ==> Hospitales <Solo almacén> | 3 ==> Jurisdicciones <Solo almacén>

            cboTipoDeBeneficiarios.Clear();
            cboTipoDeBeneficiarios.Add("00", "<< Seleccione >>");
            cboTipoDeBeneficiarios.Add("0", "Todos");
            cboTipoDeBeneficiarios.Add("1", "General <Solo farmacia>");
            cboTipoDeBeneficiarios.Add("2", "Hospitales <Solo almacén>");
            cboTipoDeBeneficiarios.Add("3", "Jurisdicciones <Solo almacén>");

            cboTipoDeBeneficiarios.SelectedIndex = 0;
        }

        private void CargarOrigenDeDispensacion()
        {
            // ---- 0 ==> Todo | 1 ==> Ventas (Excluir Vales) | 2 ==> Vales ( Ventas originadas de un vale )  

            cboOrigenDispensacion.Clear();
            cboOrigenDispensacion.Add("00", "<< Seleccione >>");
            cboOrigenDispensacion.Add("0", "Dispensación y Vales");
            cboOrigenDispensacion.Add("1", "Dispensación (Excluir Vales)");
            cboOrigenDispensacion.Add("2", "Vales ( Ventas originadas de un vale ) ");

            cboOrigenDispensacion.SelectedIndex = 0;
        }
        #endregion Combos 

        #region Controles 
        #region Buscar Rubro
        private void txtRubro_TextChanged( object sender, EventArgs e )
        {
            lblRubro.Text = "";
            txtConcepto.Text = "";
            lblConcepto.Text = "";
            lblIdCliente.Text = "";
            lblCliente.Text = "";
            lblIdSubCliente.Text = "";
            lblSubCliente.Text = "";
        }

        private void txtRubro_Validating( object sender, CancelEventArgs e )
        {
            string sCadena = "";
            leer = new clsLeer(ref cnn);

            bEsExcedente_Rubro = false;
            if(txtRubro.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Encabezado(txtRubro.Text.Trim(), "txtRubro_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if(leer.Leer())
                {
                    CargarDatosRubro();
                    ////ObtenerMontoPorAplicar();
                }
                else
                {
                    txtRubro.Text = "";
                    lblRubro.Text = "";
                    txtRubro.Focus();
                }
            }
        }

        private void CargarDatosRubro()
        {
            txtRubro.Text = leer.Campo("IdFuenteFinanciamiento");
            lblRubro.Text = leer.Campo("Estado") + " -- " + leer.Campo("NumeroDeContrato");

            lblIdCliente.Text = leer.Campo("IdCliente");
            lblCliente.Text = leer.Campo("Cliente");
            lblIdSubCliente.Text = leer.Campo("IdSubCliente");
            lblSubCliente.Text = leer.Campo("SubCliente");

            sTipoDe_FuenteDeFinanciamiento = leer.Campo("EsParaExcedente_Descripcion");
            bEsDiferencial = leer.CampoBool("EsDiferencial");

            if(leer.Campo("Status") == "C")
            {
                General.msjUser("El Rubro seleccionado se encuentra cancelado. Verifique");
                txtRubro.Text = "";
                lblRubro.Text = "";
            }

            ////Obtener_ListaProgramasAtencion();
        }

        private void txtRubro_KeyDown( object sender, KeyEventArgs e )
        {
            string sCadena = "";
            if(e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Encabezado("txtPrograma_KeyDown");

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if(leer.Leer())
                {
                    CargarDatosRubro();
                }
            }
        }
        #endregion Buscar Rubro

        #region Buscar Concepto
        private void txtConcepto_Validating( object sender, CancelEventArgs e )
        {
            string sCadena = "";
            leer = new clsLeer(ref cnn);

            dMontoFinanciamiento = 0.0000;
            bEsExcedente_Concepto = false;
            if(txtConcepto.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Detalle(txtRubro.Text.Trim(), txtConcepto.Text.Trim(), "txtRubro_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if(leer.Leer())
                {
                    CargarDatosConcepto();
                    ////ObtenerMontoPorAplicar();
                }
                else
                {
                    txtConcepto.Text = "";
                    lblConcepto.Text = "";
                    txtConcepto.Focus();
                }
            }
        }

        private void CargarDatosConcepto()
        {
            txtConcepto.Text = leer.Campo("IdFinanciamiento");
            lblConcepto.Text = leer.Campo("Financiamiento");
            dMontoFinanciamiento = leer.CampoDouble("MontoDetalle");
            if(leer.Campo("Status") == "C")
            {
                General.msjUser("El Financiamiento seleccionado se encuentra cancelado. Verifique");
                txtConcepto.Text = "";
                lblConcepto.Text = "";
            }

            ////if(ValidarClaves_Financiamiento())
            ////{
            ////    btnEjecutar.Enabled = false;
            ////}
        }

        private void txtConcepto_TextChanged( object sender, EventArgs e )
        {
            lblConcepto.Text = ""; 
        }

        private void txtConcepto_KeyDown( object sender, KeyEventArgs e )
        {
            string sCadena = "";
            if(e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Detalle("txtPrograma_KeyDown", txtRubro.Text.Trim());

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if(leer.Leer())
                {
                    CargarDatosConcepto();
                }
            }
        }
        #endregion Buscar Concepto

        #region Buscar Programa
        #endregion Buscar Programa

        #region Buscar SubPrograma
        #endregion Buscar SubPrograma
        #endregion Controles 

        #region Generar remisiones 
        private bool validarInformacion_AlmacenFolioDeVenta()
        {
            bool bRegresa = true;

            if(bRegresa && cboAlmacen.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjAviso("No ha seleccionado un Almacén válido, verifique.");
                cboAlmacen.Focus();
            }
            
            if(bRegresa && txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado un folio de venta válido, verifique.");
                txtFolio.Focus();
            }

            if(bRegresa)
            {
                if(General.msjConfirmar("La información actualizada de Venta se descargará directamente del Almacén, \n\n¿ Desea continuar ?") != DialogResult.Yes)
                {
                    bRegresa = false; 
                }
            }

            return bRegresa;  
        }
        private bool GenerarRemisiones()
        {
            bool bRegresa = false;
            string sMensaje = "";
            string sSql = ""; 

            btnGenerarRemisiones.Enabled = false;

            if(!cnn.Abrir())
            {
                btnGenerarRemisiones.Enabled = true;
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                cnn.IniciarTransaccion();
                sGUID = Guid.NewGuid().ToString();   // Referencia unica para todas las remisiones generadas

                listaGuids = new List<string>(); 
                //listaGuids.Add(sGUID);

                //bRegresa = leer.Exec(sSql);
                bRegresa = GenerarRemisiones_01_IniciarProceso(); 

                if(!bRegresa)
                {
                    Error.GrabarError(leer, "GenerarRemisiones");
                    cnn.DeshacerTransaccion(); 
                    General.msjError("Ocurrió un error al generar las remisiones.");
                    btnGenerarRemisiones.Enabled = true;
                }
                else
                {
                    if(!VerificarRemisionesGeneradas())
                    {
                        cnn.DeshacerTransaccion();
                        General.msjAviso("No se generaron remisiones con los criterios especificados.");
                        btnGenerarRemisiones.Enabled = true;
                        bRegresa = false; 
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                    }
                }

                cnn.Cerrar(); 
            }

            return bRegresa; 

        }

        private bool GenerarRemisiones_01_IniciarProceso()
        {
            bool bRegresa = false;

            if(rdo_TP_01_FolioEspecifico.Checked)
            {
                bRegresa = GenerarRemisiones_02_FolioEspecifico();
            }

            if(rdo_TP_02_MultiplesFolios.Checked)
            {
                bRegresa = GenerarRemisiones_03_MultiplesFolios();
            }

            return bRegresa;  
        }

        private bool GenerarRemisiones_02_FolioEspecifico()
        {
            bool bRegresa = true;
            string sSql = "";
            string sDato_01 = "";
            string sDato_02 = "";
            string sDato_03 = "";
            string sDato_04 = "";
            string sDato_05 = "";

            string sDato_06 = cboAlmacen.Data;
            string sDato_07 = txtFolio.Text;
            string sDato_08 = General.FechaYMD(dtpFechaVenta.Value);

            for(int i = 1; i <= gridFF.Rows; i++)
            {
                sDato_01 = gridFF.GetValue(i, Cols_FF.IdFuenteFinanciento);
                sDato_02 = gridFF.GetValue(i, Cols_FF.IdFinanciamiento);
                sDato_03 = gridFF.GetValue(i, Cols_FF.IdCliente);
                sDato_04 = gridFF.GetValue(i, Cols_FF.IdSubCliente);
                sDato_05 = gridFF.GetValueInt(i, Cols_FF.EsDiferencial).ToString();

                if(chkVentaDirecta.Checked)
                {
                    sSql = PrepararParametrosRemisiones_VentaDirecta(sDato_01, sDato_02, sDato_06, sDato_07, sDato_08);
                }
                else
                {
                    sSql = PrepararParametrosRemisiones(sDato_01, sDato_02, Convert.ToInt32(sDato_05), sDato_03, sDato_04, sDato_06, sDato_07, sDato_08, sDato_08);
                }

                if(!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break; 
                }
            }

            return bRegresa;
        }

        private bool GenerarRemisiones_03_MultiplesFolios()
        {
            bool bRegresa = true;
            string sSql = "";
            string sDato_01 = "";
            string sDato_02 = "";
            string sDato_03 = "";
            string sDato_04 = "";
            string sDato_05 = "";

            string sDato_06 = "";
            string sDato_07 = "";
            string sDato_08 = "";

            for(int j = 1; j <= gridFolios.Rows; j++)
            {
                sDato_06 = gridFolios.GetValue(j, Cols_FoliosVenta.IdAlmacen); 
                sDato_07 = gridFolios.GetValue(j, Cols_FoliosVenta.Folio); 
                sDato_08 = gridFolios.GetValue(j, Cols_FoliosVenta.Fecha); 

                for(int i = 1; i <= gridFF.Rows; i++)
                {
                    sDato_01 = gridFF.GetValue(i, Cols_FF.IdFuenteFinanciento);
                    sDato_02 = gridFF.GetValue(i, Cols_FF.IdFinanciamiento);
                    sDato_03 = gridFF.GetValue(i, Cols_FF.IdCliente);
                    sDato_04 = gridFF.GetValue(i, Cols_FF.IdSubCliente);
                    sDato_05 = gridFF.GetValueInt(i, Cols_FF.EsDiferencial).ToString();

                    if(chkVentaDirecta.Checked)
                    {
                        sSql = PrepararParametrosRemisiones_VentaDirecta(sDato_01, sDato_02, sDato_06, sDato_07, sDato_08);
                    }
                    else
                    {
                        sSql = PrepararParametrosRemisiones(sDato_01, sDato_02, Convert.ToInt32(sDato_05), sDato_03, sDato_04, sDato_06, sDato_07, sDato_08, sDato_08);
                    }

                    if(!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }

                if(!bRegresa)
                {
                    break;  
                }
            }

            return bRegresa;
        }

        private void Imprimir()
        { 
            clsRemision_GenerarDocumentos documentos = new clsRemision_GenerarDocumentos();
            string sFolioRemision = "";
            string sDescripcion = "";
            string sFarmaciaDispensacion = "";
            string sBeneficiario = "";
            string sRutaDestino = "";
            clsLeer leerDatos = new clsLeer();
            int iDocumentosGenerados = 0;
            bool bDocumentoGenerado = false; 

            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += string.Format(@"\DOCUMENTOS_REMISIONES\{0}", General.FechaYMD(General.FechaSistema, ""));

            bEjecutando = true;
            documentos.GenerarDirectorio_Farmacia = true;
            documentos.Generar_EXCEL = false;
            documentos.Generar_PDF = true;
            documentos.RutaDestinoReportes = sRutaDestino;

            leerRemisiones.RegistroActual = 0;
            //leer.RegistroActual = 0;

            for(int i = 1; i <= gridUnidades.Rows; i++)
            {
                if(gridUnidades.GetValueBool(i, ColsRemisiones.Seleccionar))
                {
                    leerDatos.DataRowsClase = leerRemisiones.DataTableClase.Select( string.Format(" FolioRemision = '{0}' ", gridUnidades.GetValue(i, ColsRemisiones.FolioRemision) ));
                    if(leerDatos.Leer())
                    {
                        sFolioRemision = leerDatos.Campo("FolioRemision");  // grid.GetValue(i, (int)Cols.FolioRemision);
                        sFarmaciaDispensacion = "___SV" + txtFolio.Text + "__" + leerDatos.Campo("IdFarmaciaDispensacion") + "__" + leerDatos.Campo("FarmaciaDispensacion");
                        sBeneficiario = leerDatos.Campo("Referencia_Beneficiario") + "__" + leerDatos.Campo("Referencia_NombreBeneficiario");

                        sDescripcion = sFarmaciaDispensacion;

                        if(sBeneficiario != "__")
                        {
                            sDescripcion = sFarmaciaDispensacion + "_" + sBeneficiario;
                        }

                        sDescripcion = sDescripcion.Replace(" ", "_").Replace("-", "_");
                        bDocumentoGenerado = documentos.GenerarDocumentos(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioRemision, sDescripcion, cboFormatosDeImpresion.Data);
                        iDocumentosGenerados += bDocumentoGenerado ? 1 : 0;
                    }
                }
            }

            bEjecutando = false;

            // bloqueo principal 
            Cursor.Current = Cursors.Default;
            //IniciarToolBar(true, true, true);

            //BloquearControles(false);

            //MostrarEnProceso(false);

            if(iDocumentosGenerados > 0)
            {
                General.AbrirDirectorio(sRutaDestino);
            }
        }

        private void cboAlmacen_SelectedIndexChanged( object sender, EventArgs e )
        {
            //cboAlmacen.Data
            if(cboAlmacen.SelectedIndex != 0)
            {
                sUrl = ((DataRow)cboAlmacen.ItemActual.Item)["UrlFarmacia"].ToString();
            }
        }

        private void chkEsRelacionFacturaPrevia_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkEsRelacionFacturaPrevia.Checked)
            {
                txtFactura_Serie.Text = "";
                txtFactura_Folio.Text = "";
                txtFactura_Serie.Enabled = false;
                txtFactura_Folio.Enabled = false;
            }
            else
            {
                txtFactura_Serie.Enabled = true;
                txtFactura_Folio.Enabled = true;
            }
        }

        private string PrepararParametrosRemisiones(string IdFuenteFinanciamiento, string IdFinanciamiento, int EsDiferencial, string IdCliente, string IdSubCliente, 
            string IdFarmacia, string FolioDeVenta, string FechaInicial, string FechaFinal
        )
        {
            bool bRegresa = false;
            string sSql = "";
            int iMostrarResultado = 0;

            int iBeneficiarios_x_Jurisdiccion = chkBeneficiarios_x_Jurisdiccion.Checked ? 1 : 0;
            int iProcesarParcialidades = chkProcesarParcialidades.Checked ? 1 : 0;
            int iProcesarCantidadesExcedentes = chkProcesarCantidadesExcedentes.Checked ? 1 : 0;
            int iAsignarReferencias = chkAsignarReferencias.Checked ? 1 : 0;

            int iProcesar_Producto = chkTipoDeRemision_01_Producto.Checked ? 1 : 0;
            int iProcesar_Servicio = chkTipoDeRemision_02_Servicio.Checked ? 1 : 0;
            int iProcesar_Servicio_Consigna = chkTipoDeRemision_02_Servicio.Checked && chkOrigenInsumo_02_Consignacion.Checked ? 1 : 0;

            int iProcesar_Medicamento = chkTipoDeInsumo_01_Medicamento.Checked ? 1 : 0;
            int iProcesar_MaterialDeCuracion = chkTipoDeInsumo_02_MaterialDeCuracion.Checked ? 1 : 0;
            int iProcesar_Venta = chkOrigenInsumo_01_Venta.Checked ? 1 : 0;
            int iProcesar_Consigna = chkOrigenInsumo_02_Consignacion.Checked ? 1 : 0;

            int iTipoDeRemision = 0;
            string sCriterio_ProgramasAtencion = "";
            int iMontoAFacturar = 0;
            int iIdTipoProducto = 0; // Todo, se revisa en base a Procesar X ITEM 
            int iEsExcedente = 0;
            int iTipoDispensacion = 0;
            int iFechaRevision = 3;
            string sClaveSSA = "";
            string sListaFoliosDeVenta = FolioDeVenta;
            int iAplicarDocumentos = chkAplicarDocumentos.Checked ? 1 : 0;
            int iEsProgramasEspeciales = chkEsProgramaEspecial.Checked ? 1 : 0;
            string sIdBeneficiario = "";
            string sIdBeneficiario_01_Menor = "";
            string sIdBeneficiario_02_Mayor = "";
            int iRemision_General = chkEsRemisionGeneral.Checked ? 1 : 0;
            string sListaClavesSSA_Excluidas = "";
            int iEsRelacionFacturaPrevia = chkEsRelacionFacturaPrevia.Checked ? 1 : 0;
            int iEsFacturaPreviaEnCajas = chkEsFacturaPreviaEnCajas.Checked ? 1 : 0;
            string sSerie = chkEsRelacionFacturaPrevia.Checked ? txtFactura_Serie.Text.Trim() : "";
            string sFolio = chkEsRelacionFacturaPrevia.Checked ? txtFactura_Folio.Text.Trim() : "";
            int iEsRelacionMontos = chkEsRelacionDeMontos.Checked ? 1 : 0;

            int iProcesar_SoloClavesReferenciaRemisiones = chkProcesar_SoloClavesReferenciaRemisiones.Checked ? 1 : 0;
            int iExcluirCantidadesConDecimales = chkExcluirCantidadesConDecimales.Checked ? 1 : 0;
            int iSeparar__Venta_y_Vales = chkSeparar__Venta_y_Vales.Checked ? 1 : 0;
            int iEsRemision_Complemento = EsDiferencial;//chkEsComplemento.Checked ? 1 : 0;

            sGUID = Guid.NewGuid().ToString(); // Referencia unica para todas las remisiones generadas 
            listaGuids.Add(sGUID);

            sSql =
                string.Format("Exec spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen " +
                    " \n" +
                    " \t@NivelInformacion_Remision = '{0}', @Beneficiarios_x_Jurisdiccion = '{1}', \n" +
                    " \t@ProcesarParcialidades = '{2}', @ProcesarCantidadesExcedentes = '{3}', @AsignarReferencias = '{4}', \n" +
                    " \t@Procesar_Producto = '{5}', @Procesar_Servicio = '{6}', @Procesar_Servicio_Consigna = '{7}', \n" +
                    " \t@Procesar_Medicamento = '{8}', @Procesar_MaterialDeCuracion = '{9}', @Procesar_Venta = '{10}', @Procesar_Consigna = '{11}', 	\n" +
                    " \n" +
                    " \t@IdEmpresa = '{12}', @IdEstado = '{13}', @IdFarmaciaGenera = '{14}', @TipoDeRemision = '{15}', @IdFarmacia = '{16}', \n" +
                    " \t@IdCliente = '{17}', @IdSubCliente = '{18}', @IdFuenteFinanciamiento = '{19}', @IdFinanciamiento = '{20}', @Criterio_ProgramasAtencion = '{21}', \n" +
                    " \n" +
                    " \t@FechaInicial = '{22}', @FechaFinal = '{23}', @iMontoFacturar = '{24}', @IdPersonalFactura = '{25}', @Observaciones = '{26}', \n" +
                    " \t@IdTipoProducto = '{27}', @EsExcedente = '{28}', @Identificador = '{29}', @TipoDispensacion = '{30}', @ClaveSSA = '{31}', @FechaDeRevision = '{32}', @FoliosVenta = '{33}', \n" +
                    " \n" +
                    " \t@TipoDeBeneficiario = '{34}', @Aplicar_ImporteDocumentos = '{35}', @EsProgramasEspeciales = '{36}', \n" +
                    " \t@IdBeneficiario = '{37}', @IdBeneficiario_MayorIgual = '{38}', @IdBeneficiario_MenorIgual = '{39}', @Remision_General = '{40}', @ClaveSSA_ListaExclusion = '{41}', \n" +
                    " \t@EsRelacionFacturaPrevia = '{42}', @FacturaPreviaEnCajas = '{43}', @Serie = '{44}', @Folio = '{45}', @EsRelacionMontos = '{46}', \n" +
                    " \t@Procesar_SoloClavesReferenciaRemisiones = '{47}', @ExcluirCantidadesConDecimales = '{48}', @Separar__Venta_y_Vales = '{49}', @TipoDispensacion_Venta = '{50}', @EsRemision_Complemento = '{51}', \n" +
                    " \t@MostrarResultado = '{52}' ",


                    cboNivelDeInformacion.Data, iBeneficiarios_x_Jurisdiccion,
                    iProcesarParcialidades, iProcesarCantidadesExcedentes, iAsignarReferencias,
                    iProcesar_Producto, iProcesar_Servicio, iProcesar_Servicio_Consigna,

                    iProcesar_Medicamento, iProcesar_MaterialDeCuracion, iProcesar_Venta, iProcesar_Consigna,
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iTipoDeRemision, IdFarmacia,
                    IdCliente, IdSubCliente, IdFuenteFinanciamiento, IdFinanciamiento, sCriterio_ProgramasAtencion,


                    FechaInicial, FechaFinal, iMontoAFacturar, DtGeneral.IdPersonal, "",
                    iIdTipoProducto, iEsExcedente, sGUID, iTipoDispensacion, sClaveSSA, iFechaRevision, sListaFoliosDeVenta,

                    cboTipoDeBeneficiarios.Data, iAplicarDocumentos, iEsProgramasEspeciales,
                    sIdBeneficiario, sIdBeneficiario_01_Menor, sIdBeneficiario_02_Mayor, iRemision_General, sListaClavesSSA_Excluidas,

                    iEsRelacionFacturaPrevia, iEsFacturaPreviaEnCajas, sSerie, sFolio, iEsRelacionMontos,

                    iProcesar_SoloClavesReferenciaRemisiones, iExcluirCantidadesConDecimales, iSeparar__Venta_y_Vales, cboOrigenDispensacion.Data, iEsRemision_Complemento,

                    iMostrarResultado
                );

            ////sSql += string.Format(
            ////    "\n\n" +
            ////    "Select distinct R.*, F.IdFarmacia As IdFarmaciaDispensacion, F.Farmacia As FarmaciaDispensacion\n" +
            ////    "From vw_FACT_Remisiones R (NoLock)\n" +
            ////    "Inner Join FACT_Remisiones_Detalles D (NoLock)\n" +
            ////    "   On (R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmacia = D.IdFarmaciaGenera and R.FolioRemision = D.FolioRemision)\n" +
            ////    "Inner Join vw_Farmacias F (NoLock) On (R.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia)\n" +
            ////    "Where R.IdEmpresa = '{0}' and R.IdEstado = '{1}' and R.IdFarmacia = '{2}' and GUID = '{3}'  ",
            ////    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sGUID);

            return sSql;
        }

        private bool VerificarRemisionesGeneradas()
        {
            bool bRegresa = false;
            string sSql = "";

            sSql += string.Format(
                "\n\n" +
                "Select distinct R.*, F.IdFarmacia As IdFarmaciaDispensacion, F.Farmacia As FarmaciaDispensacion\n" +
                "From vw_FACT_Remisiones R (NoLock)\n" +
                "Inner Join FACT_Remisiones_Detalles D (NoLock)\n" +
                "   On (R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmacia = D.IdFarmaciaGenera and R.FolioRemision = D.FolioRemision)\n" +
                "Inner Join vw_Farmacias F (NoLock) On (R.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia)\n" +
                "Where R.IdEmpresa = '{0}' and R.IdEstado = '{1}' and R.IdFarmacia = '{2}' and GUID = '{3}'  ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sGUID);

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "VerificarRemisionesGeneradas");
            }
            else
            {
                bRegresa = leer.Leer();  
            }

            return bRegresa;  
        }

        private string PrepararParametrosRemisiones_VentaDirecta( string IdFuenteFinanciamiento, string IdFinanciamiento, string IdFarmacia, string FolioDeVenta, string Fecha)
        {
            string sSql = "";


            sGUID = Guid.NewGuid().ToString(); // Referencia unica para todas las remisiones generadas 
            listaGuids.Add(sGUID);

            sSql =
                string.Format("Exec spp_FACT_GenerarRemisiones_VentaDirecta " +
                    " \n" +
                    " \t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', @IdFarmacia = '{3}', @FolioVenta = '{4}', \n" +
                    " \t@IdFuenteFinanciamiento = '{5}', @IdFinanciamiento = '{6}', \n" +
                    " \t@IdPersonalRemision = '{7}', @FechaVenta = '{8}', @GUID = '{9}', @Porcentaje = {10}	\n",


                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, IdFarmacia, FolioDeVenta,
                    IdFuenteFinanciamiento, IdFinanciamiento,
                    DtGeneral.IdPersonal, Fecha, sGUID, nmPorcentaje.Value
                );

            //sSql += string.Format(
            //    "\n\n" +
            //    "Select distinct R.*, F.IdFarmacia As IdFarmaciaDispensacion, F.Farmacia As FarmaciaDispensacion\n" +
            //    "From vw_FACT_Remisiones R(NoLock)\n" +
            //    "Inner Join FACT_Remisiones_Detalles D (NoLock)\n" +
            //    "   On (R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmacia = D.IdFarmaciaGenera and R.FolioRemision = D.FolioRemision)\n" +
            //    "Inner Join vw_Farmacias F (NoLock) On (R.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia)\n" +
            //    "Where R.IdEmpresa = '{0}' and R.IdEstado = '{1}' and R.IdFarmacia = '{2}' and GUID = '{3}'  ",
            //    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sGUID);

            return sSql;
        }
        #endregion Generar Remisiones


        private void txtFolio_Validating( object sender, CancelEventArgs e )
        {
            if(txtFolio.Text.Trim() != "")
            {
                txtFolio.Text = Fg.PonCeros(txtFolio.Text, 8);
                InformacioDeVenta__FoliosDeRemision(); 
            }
        }


        private bool ValidarDatos_VentaDirecta()
        {
            bool bregresa = true;


            ////if (txtRubro.Text.Trim() == "")
            ////{
            ////    bregresa = false;
            ////    General.msjUser("No selecciono un rubro, verifique.");
            ////    txtRubro.Focus();
            ////}

            ////if (txtConcepto.Text.Trim() == "" && bregresa)
            ////{
            ////    bregresa = false;
            ////    General.msjUser("No selecciono un Concepto, verifique.");
            ////    txtConcepto.Focus();
            ////}


            if(gridFF.Rows == 0)
            {
                bregresa = false;
                General.msjUser("No ha seleccionado las Fuentes de financiamiento a procesar, verifique.");
                txtRubro.Focus();
            }

            if (rdo_TP_01_FolioEspecifico.Checked && myGrid.TotalizarColumnaDou(cols.PorRemisionar_Insumo) == 0 && bregresa)
            {
                bregresa = false;
                General.msjUser("La venta directa no contiene cantidad disponible de producto para remisionar, verifique.");
            }

            if (Convert.ToDouble(nmPorcentaje.Text) == 0.00 && bregresa)
            {
                bregresa = false;
                General.msjUser("El porcentaje no puede ser igual cero, verifique.");
                txtConcepto.Focus();
            }

            if(bregresa && cboFormatosDeImpresion.SelectedIndex == 0)
            {
                bregresa = false;
                General.msjUser("No ha seleccionado un formato de impresión, verifique.");
                cboFormatosDeImpresion.Focus();
            }

            return bregresa;
        }

        private bool ValidarDatos()
        {
            bool bregresa = true;


            if (gridFF.Rows == 0)
            {
                bregresa = false;
                General.msjUser("No ha seleccionado las Fuentes de financiamiento a procesar, verifique.");
                txtRubro.Focus();
            }

            if (cboNivelDeInformacion.SelectedIndex == 0 && bregresa)
            {
                bregresa = false;
                General.msjUser("Nivel de información inválida, verifique.");
                cboNivelDeInformacion.Focus();
            }

            if (cboTipoDeBeneficiarios.SelectedIndex == 0 && bregresa)
            {
                bregresa = false;
                General.msjUser("Tipo de beneficiario inválido, verifique.");
                cboTipoDeBeneficiarios.Focus();
            }

            if (!(chkTipoDeRemision_01_Producto.Checked || chkTipoDeRemision_02_Servicio.Checked) && bregresa)
            {
                bregresa = false;
                General.msjUser("Seleccione un tipo de remisión, verifique.");
                chkTipoDeRemision_01_Producto.Focus();
            }

            if (!(chkOrigenInsumo_01_Venta.Checked || chkOrigenInsumo_02_Consignacion.Checked) && bregresa)
            {
                bregresa = false;
                General.msjUser("Seleccione un origen de insumo, verifique.");
                chkOrigenInsumo_01_Venta.Focus();
            }

            if (!(chkTipoDeInsumo_01_Medicamento.Checked || chkTipoDeInsumo_02_MaterialDeCuracion.Checked) && bregresa)
            {
                bregresa = false;
                General.msjUser("Seleccione un tipo de insumo, verifique.");
                chkTipoDeInsumo_01_Medicamento.Focus();
            }

            if (cboOrigenDispensacion.SelectedIndex == 0 && bregresa)
            {
                bregresa = false;
                General.msjUser("Origen de dispensación inválida, verifique.");
                cboOrigenDispensacion.Focus();
            }

            if (chkTipoDeRemision_01_Producto.Checked && bregresa)
            {
                if(rdo_TP_01_FolioEspecifico.Checked)
                {
                    if(myGrid.TotalizarColumnaDou(cols.PorRemisionar_Insumo) == 0)
                    {
                        bregresa = false;
                        General.msjUser("La venta directa no contiene cantidad disponible de producto para remisionar, verifique.");
                    }
                }
            }

            if (chkTipoDeRemision_02_Servicio.Checked && bregresa)
            {
                if(rdo_TP_01_FolioEspecifico.Checked)
                {
                    if(myGrid.TotalizarColumnaDou(cols.PorRemisionar_Admon) == 0.0)
                    {
                        bregresa = false;
                        General.msjUser("La venta directa no contiene cantidad disponible de servicio para remisionar, verifique.");
                    }
                }
            }

            if (chkEsRelacionFacturaPrevia.Checked && bregresa)
            {
                if (txtFactura_Folio.Text == "" || txtFactura_Serie.Text == "")
                {
                    bregresa = false;
                    General.msjUser("Indique el folio y la serie de la factura previa.");
                    txtFactura_Serie.Focus();
                }
            }

            if(bregresa && chkGenerarDocumentos.Checked && cboFormatosDeImpresion.SelectedIndex == 0)
            {
                bregresa = false;
                General.msjUser("No ha seleccionado un formato de impresión, verifique.");
                cboFormatosDeImpresion.Focus();
            }

            return bregresa;
        }


        private bool ValidarTotales()
        {
            bool bregresa = true;

            if (chkTipoDeRemision_01_Producto.Checked && bregresa)
            {
                if (myGrid.TotalizarColumnaDou(cols.PorRemisionar_Insumo) == 0)
                {
                    bregresa = false;
                    General.msjUser("La venta directa no contiene cantidad disponible de producto para remisionar, verifique.");
                }
            }

            if (chkTipoDeRemision_02_Servicio.Checked && bregresa)
            {
                if (myGrid.TotalizarColumnaDou(cols.PorRemisionar_Admon) == 0.0)
                {
                    bregresa = false;
                    General.msjUser("La venta directa no contiene cantidad disponible de servicio para remisionar, verifique.");
                }
            }

            return bregresa;
        }

        private void btnAgregarFuentes_Click( object sender, EventArgs e )
        {
            if(txtRubro.Text.Trim() == "")
            {
                General.msjUser("No ha capturado una Configuración válida, verifique.");
            }
            else
            {
                int iRenglon = gridFF.Rows + 1;
                
                gridFF.AddRow();
                gridFF.SetValue(iRenglon, Cols_FF.IdFuenteFinanciento, txtRubro.Text);
                gridFF.SetValue(iRenglon, Cols_FF.TipoDeFuente, sTipoDe_FuenteDeFinanciamiento);
                gridFF.SetValue(iRenglon, Cols_FF.EsDiferencial, bEsDiferencial);


                gridFF.SetValue(iRenglon, Cols_FF.IdFinanciamiento, txtConcepto.Text);
                gridFF.SetValue(iRenglon, Cols_FF.Financiamiento, lblConcepto.Text);
                gridFF.SetValue(iRenglon, Cols_FF.IdCliente, lblIdCliente.Text);
                gridFF.SetValue(iRenglon, Cols_FF.Cliente, lblCliente.Text);
                gridFF.SetValue(iRenglon, Cols_FF.IdSubCliente, lblIdSubCliente.Text);
                gridFF.SetValue(iRenglon, Cols_FF.SubCliente, lblSubCliente.Text);


                sTipoDe_FuenteDeFinanciamiento = "";
                bEsDiferencial = false;

                Fg.IniciaControles(this, true, Frame_02_FuentesFinanciamiento);
                txtRubro.Focus(); 
            }
        }

        private void btnLimpiarFF_Click( object sender, EventArgs e )
        {
            gridFF.Limpiar(false); 
        }

        private void txtFolio_MultiplesVentas_Validating( object sender, CancelEventArgs e )
        {
            if(txtFolio_MultiplesVentas.Text.Trim() != "")
            {
                InformacioDeVenta_MultiplesVentas(); 
            }
        }

        private bool InformacioDeVenta()
        {
            bool bRegresa = false;
            string sTablaBase = chkOrigenInformacion.Checked ? "FACT_Incremento___VentasDet_Lotes" : "VentasDet_Lotes";

            string sSql = string.Format(
                "Select * From vw_VentasEnc (NoLock) Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' " +
                "\n\n" + 
                "Select L.CodigoEAN, L.IdProducto, DescripcionCorta, CantidadVendida, CantidadRemision_Insumo, CantidadRemision_Admon, " +
                "   (Case When(CantidadVendida >= CantidadRemision_Insumo) Then CantidadVendida - CantidadRemision_Insumo else 0 End) As PorRemisionar_Insumo,  " +
                "   (Case When(CantidadVendida >= CantidadRemision_Admon) Then CantidadVendida - CantidadRemision_Admon else 0 End) As PorRemisionar_Admon " +
                "From {4} L(NoLock) " +
                "Inner Join vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) " +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioVenta = '{3}' \n\n",
                    sIdEmpresa, sIdEstado, cboAlmacen.Data, txtFolio.Text.Trim(), sTablaBase);

            sSql += ""; 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "InformacioDeVenta()");
                General.msjError("Error al obtener la información .");
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true;

                    dtpFechaVenta.Value = leer.CampoFecha("FechaRegistro");
                    lblVTA_IdCliente.Text = leer.Campo("Idcliente");
                    lblVTA_NombreCliente.Text = leer.Campo("NombreCliente");
                    lblVTA_IdSubCliente.Text = leer.Campo("IdSubCliente");
                    lblVTA_NombreSubCliente.Text = leer.Campo("NombreSubCliente");

                    lblVTA_IdPrograma.Text = leer.Campo("IdPrograma");
                    lblVTA_Programa.Text = leer.Campo("Programa");
                    lblVTA_IdSubPrograma.Text = leer.Campo("IdSubPrograma");
                    lblVTA_SubPrograma.Text = leer.Campo("SubPrograma");


                    leer.DataTableClase = leer.Tabla(2);
                    if(leer.Leer())
                    {
                        myGrid.LlenarGrid(leer.DataSetClase, false, false);
                    }
                }

            }


            return bRegresa;
        }

        private bool InformacioDeVenta__FoliosDeRemision()
        {
            bool bRegresa = false;
            string sSql = ""; 

            sSql = string.Format(
                "Select \n" +
                "\tR.FolioRemision, convert(varchar(10), R.FechaRemision, 120) as FechaRemision, R.IdFuenteFinanciamiento, R.IdFinanciamiento, R.Financiamiento, R.TipoDeRemision, R.TipoDeRemisionDesc, R.Total \n" +
                "From vw_FACT_Remisiones R (NoLock) \n" +
                "Where R.IdEmpresa = '{0}' And R.IdEstado = '{1}' And R.IdFarmacia = '{2}' and R.Status = 'A' \n" +
                "\tand exists \n"  +
                "\t( \n" +
                "\t\t Select * \n" +
                "\t\t From FACT_Remisiones_Detalles D (NoLock) \n" +
                "\t\t Where R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmacia = D.IdFarmaciaGenera and R.FolioRemision = D.FolioRemision \n" +
                "\t\t     and D.IdFarmacia = '{3}' and D.FolioVenta = '{4}' \n" +
                "\t) \n", 
                sIdEmpresa, sIdEstado, DtGeneral.FarmaciaConectada, cboAlmacen.Data, txtFolio.Text.Trim() );

            sSql += "";

            leerRemisiones = new clsLeer(); 
            gridUnidades.Limpiar();
            lblRemisionesAsociadas.Text = "0";
            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "InformacioDeVenta__FoliosDeRemision()");
                General.msjError("Error al obtener la información .");
            }
            else
            {
                if(leer.Leer())
                {
                    leerRemisiones.DataSetClase = leer.DataSetClase;
                    
                    bRegresa = true;
                    gridUnidades.LlenarGrid(leer.DataSetClase, false, false);
                    gridUnidades.SetValue(ColsRemisiones.Seleccionar, true); 
                }

            }

            lblRemisionesAsociadas.Text = gridUnidades.Rows.ToString();

            btnExportarPDF.Enabled = gridUnidades.Rows > 0;
            BloquearControles(btnExportarPDF.Enabled, Frame_10_FormatosDeImpresion);

            return bRegresa;
        }



        #region Multiples ventas 
        private void btnAgregar_FolioDeVenta_Click( object sender, EventArgs e )
        {

            if(txtFolio_MultiplesVentas.Text.Trim() == "")
            {
                General.msjUser("No ha capturado un Folio de Venta válido, verifique.");
            }
            else
            {
                string sIdFarmacia = cboAlmacen_MultiplesVentas.Data; 
                int iRenglon = gridFolios.Rows + 1;

                gridFolios.AddRow();
                gridFolios.SetValue(iRenglon, Cols_FoliosVenta.IdAlmacen, sIdFarmacia);
                gridFolios.SetValue(iRenglon, Cols_FoliosVenta.Folio, txtFolio_MultiplesVentas.Text);
                gridFolios.SetValue(iRenglon, Cols_FoliosVenta.Fecha, General.FechaYMD(dtpFechaVenta_MultiplesVentas.Value));

                gridFolios.SetValue(iRenglon, Cols_FoliosVenta.IdCliente, lblVTA_Multiple_IdCliente.Text);
                gridFolios.SetValue(iRenglon, Cols_FoliosVenta.Cliente, lblVTA_Multiple_NombreCliente.Text);
                gridFolios.SetValue(iRenglon, Cols_FoliosVenta.IdSubCliente, lblVTA_Multiple_IdSubCliente.Text);
                gridFolios.SetValue(iRenglon, Cols_FoliosVenta.SubCliente, lblVTA_Multiple_NombreSubCliente.Text);


                sTipoDe_FuenteDeFinanciamiento = "";
                bEsDiferencial = false;

                Fg.IniciaControles(this, true, Frame12_MultiplesFolios);
                cboAlmacen_MultiplesVentas.Data = sIdFarmacia; 
                cboAlmacen_MultiplesVentas.Focus();
            }
        }

        private void btnLimpiarListaFoliosDeVenta_Click( object sender, EventArgs e )
        {

            gridFolios.Limpiar(false);
            txtFolio_MultiplesVentas.Focus(); 
        }

        private bool InformacioDeVenta_MultiplesVentas()
        {
            bool bRegresa = false;
            string sTablaBase = "VentasDet_Lotes";

            string sSql = string.Format(
                "Select * From vw_VentasEnc (NoLock) Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' " +
                "\n\n" +
                "Select L.CodigoEAN, L.IdProducto, DescripcionCorta, CantidadVendida, CantidadRemision_Insumo, CantidadRemision_Admon, " +
                "   (Case When(CantidadVendida >= CantidadRemision_Insumo) Then CantidadVendida - CantidadRemision_Insumo else 0 End) As PorRemisionar_Insumo,  " +
                "   (Case When(CantidadVendida >= CantidadRemision_Admon) Then CantidadVendida - CantidadRemision_Admon else 0 End) As PorRemisionar_Admon " +
                "From {4} L(NoLock) " +
                "Inner Join vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) " +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioVenta = '{3}' \n\n",
                    sIdEmpresa, sIdEstado, cboAlmacen.Data, txtFolio_MultiplesVentas.Text.Trim(), sTablaBase);


            sSql = string.Format(
                "Select * From vw_VentasEnc (NoLock) Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' " +
                "\n\n", 
                    sIdEmpresa, sIdEstado, cboAlmacen_MultiplesVentas.Data, Fg.PonCeros(txtFolio_MultiplesVentas.Text.Trim(), 8) );
            sSql += "";

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "InformacioDeVenta()");
                General.msjError("Error al obtener la información .");
            }
            else
            {
                if(leer.Leer())
                {
                    bRegresa = true;

                    txtFolio_MultiplesVentas.Text = leer.Campo("Folio");
                    dtpFechaVenta_MultiplesVentas.Value = leer.CampoFecha("FechaRegistro");
                    lblVTA_Multiple_IdCliente.Text = leer.Campo("Idcliente");
                    lblVTA_Multiple_NombreCliente.Text = leer.Campo("NombreCliente");
                    lblVTA_Multiple_IdSubCliente.Text = leer.Campo("IdSubCliente");
                    lblVTA_Multiple_NombreSubCliente.Text = leer.Campo("NombreSubCliente");

                    lblVTA_Multiple_IdPrograma.Text = leer.Campo("IdPrograma");
                    lblVTA_Multiple_Programa.Text = leer.Campo("Programa");
                    lblVTA_Multiple_IdSubPrograma.Text = leer.Campo("IdSubPrograma");
                    lblVTA_Multiple_SubPrograma.Text = leer.Campo("SubPrograma");


                    //leer.DataTableClase = leer.Tabla(1);
                    //if(leer.Leer())
                    {
                        ////myGrid.LlenarGrid(leer.DataSetClase, false, false);
                    }
                }

            }


            return bRegresa;
        }
        #endregion Multiples ventas 
    }
}
