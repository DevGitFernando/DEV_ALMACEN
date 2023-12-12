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

using Dll_MA_IFacturacion;

namespace MA_Facturacion.GenerarRemisiones
{
    public partial class FrmRemisionInsumos : FrmBaseExt
    {
        //////enum Cols
        //////{
        //////    IdJurisdiccion = 1, Jurisdiccion = 2, IdFarmacia = 3, Farmacia = 4,  
        //////    FechaRegistroCierre = 5, Folio = 6, 
        //////    FechaCorte = 7, FechaCierreMinima = 8, FechaCierreMaxima = 9, 
        //////    Facturar = 10 
        //////}

        enum Cols
        {
            Ninguna = 0,
            IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
            Fecha, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
            SubTotal_SinGravar, SubTotal_Gravado, Iva, Importe
        }

        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer, myLeerDetalles;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;
        DataSet dtsJurisdicciones; 
        DataSet dtsFarmacias;
        DataSet dtsDatos = new DataSet();
        eEsquemaDeFacturacion tpFacturacion = DtIFacturacion.EsquemaDeFacturacion;
        bool bEsquemaDeFacturacionValido = false; 

        string sIdEmpresa = DtGeneral.EmpresaConectada; 
        string sIdEstado = DtGeneral.EstadoConectado;
        string sFolio = "", sMensaje = "";
        double dMontoFinanciamiento = 0;
        bool bEsExcedente_Rubro = false;
        bool bEsExcedente_Concepto = false;


        string sValor_Cero = "0";
        string sFormato = "###,###,###,###,##0.###0";
        string sIdentificador = "";

        //Para Auditoria
        clsAuditoria auditoria;

        //Hilos
        Thread _workerThread;
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;


        DataSet dtsProgramas_SubProgramas = new DataSet();
        string sListaDeSeleccion = ""; 

        public FrmRemisionInsumos()
        {
            InitializeComponent();

            ConexionLocal.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            ConexionLocal.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            ConexionLocal.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");
            myLeer = new clsLeer(ref ConexionLocal);
            myLeerDetalles = new clsLeer(ref ConexionLocal);
            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);

            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            myGrid = new clsGrid(ref grdReporte, this);
            myGrid.BackColorColsBlk = Color.White;
            grdReporte.EditModeReplace = true;

            SetEsquemaDeFacturacion();

            ObtenerEstados(); 
            ObtenerFarmacias();
            ObtenerTipoUnidades();
            ObtenerJurisdicciones();            
        }

        private void FrmRemisionInsumos_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            //Obtener_ListaProgramasAtencion(); 
        }

        #region Esquema de facturacion 
        private void SetEsquemaDeFacturacion()
        {
            tpFacturacion = DtIFacturacion.EsquemaDeFacturacion;
            string sTituloFacturacion = ""; 

            switch ( tpFacturacion ) 
            {
                case eEsquemaDeFacturacion.Ninguno:
                    bEsquemaDeFacturacionValido = false;
                    sTituloFacturacion = "Esquema de facturación no determinado"; 
                    break; 

                case eEsquemaDeFacturacion.Libre:
                    bEsquemaDeFacturacionValido = true;
                    sTituloFacturacion = "Esquema de facturación LIBRE"; 
                    break;

                case eEsquemaDeFacturacion.Montos:
                    bEsquemaDeFacturacionValido = true;
                    sTituloFacturacion = "Esquema de facturación basado en MONTOS"; 
                    break;
            }

            this.Text += " : " + sTituloFacturacion;            
        }
        #endregion Esquema de facturacion

        #region Buscar Rubro
        private void txtcliente_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";
            myLeer = new clsLeer(ref ConexionLocal);

            bEsExcedente_Rubro = false;
            if (txtcliente.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Clientes(txtcliente.Text.Trim(), "txtcliente_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosCliente();
                    ObtenerMontoPorAplicar();
                }
                else
                {
                    txtcliente.Text = "";
                    ////lblRubro.Text = "";
                    txtcliente.Focus();
                }
            }
        }

        private void CargarDatosCliente()
        {
            //txtcliente.Text = myLeer.Campo("IdFuenteFinanciamiento");
            ////lblRubro.Text = myLeer.Campo("Estado");

            txtcliente.Text = myLeer.Campo("IdCliente");
            lblCliente.Text = myLeer.Campo("Nombre");
            txtcliente.Enabled = false;

            if (myLeer.Campo("Status") == "C")
            {
                General.msjUser("El cliente seleccionado se encuentra cancelado. Verifique");
                txtcliente.Text = "";
                ////lblRubro.Text = "";
                txtcliente.Enabled = true;
            }
        }

        private void CargarDatosSubCliente()
        {
            txtSubCliente.Text = myLeer.Campo("IdSubCliente");
            lblSubCliente.Text = myLeer.Campo("Nombre");
            txtSubCliente.Enabled = false;

            if (myLeer.Campo("Status") == "C")
            {
                General.msjUser("El cliente seleccionado se encuentra cancelado. Verifique");
                txtcliente.Text = "";
                ////lblRubro.Text = "";
                txtSubCliente.Enabled = true;
            }

            //Obtener_ListaProgramasAtencion();
        }

        private void txtcliente_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayudas.Clientes("txtcliente_KeyDown");

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosCliente();
                }
            }
        }
        #endregion Buscar Rubro

        #region Buscar Concepto
        private void txtSubCliente_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";
            myLeer = new clsLeer(ref ConexionLocal);

            dMontoFinanciamiento = 0.0000;            
            bEsExcedente_Concepto = false;
            if (txtSubCliente.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.SubClientes(txtcliente.Text.Trim(), txtSubCliente.Text.Trim(), "txtcliente_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosSubCliente();
                    ObtenerMontoPorAplicar();
                }
                else
                {
                    txtSubCliente.Text = "";
                    ////lblConcepto.Text = "";
                    txtSubCliente.Focus();
                }
            }
        }

        //private void CargarDatosConcepto()
        //{
        //    txtSubCliente.Text = myLeer.Campo("IdFinanciamiento");
        //    ////lblConcepto.Text = myLeer.Campo("Financiamiento");
        //    dMontoFinanciamiento = myLeer.CampoDouble("MontoDetalle"); 

        //    if (myLeer.Campo("Status") == "C")
        //    {
        //        General.msjUser("El Financiamiento seleccionado se encuentra cancelado. Verifique");
        //        txtSubCliente.Text = "";
        //        ////lblConcepto.Text = "";
        //    }

        //    if (ValidarClaves_Financiamiento())
        //    {
        //        btnEjecutar.Enabled = false;
        //    }
        //}

        private void txtSubCliente_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayudas.SubClientes("txtSubCliente_KeyDown", txtcliente.Text.Trim());

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosSubCliente();
                }
            }
        }
        #endregion Buscar Concepto

        #region Buscar Programa 
        private void txtPrograma_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtPrograma.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Programas(txtPrograma.Text.Trim(), "txtPrograma_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosPrograma();
                }
                else
                {
                    txtPrograma.Text = "";
                    lblPrograma.Text = "";
                    txtPrograma.Focus();
                }
            }
        }

        private void CargarDatosPrograma()
        {
            txtPrograma.Text = myLeer.Campo("IdPrograma");
            lblPrograma.Text = myLeer.Campo("Descripcion");
            txtPrograma.Enabled = false;

            if (myLeer.Campo("Status") == "C")
            {
                General.msjUser("El Programa seleccionado se encuentra cancelado. Verifique");
                txtPrograma.Text = "";
                lblPrograma.Text = "";
                txtPrograma.Enabled = true;
            }
        }

        private void txtPrograma_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayudas.Programas("txtPrograma_KeyDown");

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosPrograma();
                }
            }
        }
        #endregion Buscar Programa

        #region Buscar SubPrograma 
        private void txtSubPrograma_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtSubPrograma.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.SubProgramas(txtSubPrograma.Text.Trim(), txtPrograma.Text.Trim(), "txtSubPrograma_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosSubPrograma();
                }
                else
                {
                    txtSubPrograma.Text = "";
                    lblSubPrograma.Text = "";
                    txtSubPrograma.Focus();
                }
            }
        }

        private void CargarDatosSubPrograma()
        {
            txtSubPrograma.Text = myLeer.Campo("IdSubPrograma");
            lblSubPrograma.Text = myLeer.Campo("Descripcion");
            txtSubPrograma.Enabled = false;

            if (myLeer.Campo("Status") == "C")
            {
                General.msjUser("El SubPrograma seleccionado se encuentra cancelado. Verifique");
                txtSubPrograma.Text = "";
                lblSubPrograma.Text = "";
                txtSubPrograma.Enabled = true;
            }
        }

        private void txtSubPrograma_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayudas.SubProgramas("txtSubPrograma_KeyDown", txtPrograma.Text.Trim());

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosSubPrograma();
                }
            }
        }
        #endregion Buscar SubPrograma 

        #region Dibujar Cuadro
        private void FrmRemisionInsumos_Paint(object sender, PaintEventArgs e)
        {
            //////int iVar = 1;
            //////int iVarX = 1;

            //////Point posicion = new Point(tabControlFacturacion.Left - 0, tabControlFacturacion.Top - 0);
            //////Size size = new Size(tabControlFacturacion.Width + 0, tabControlFacturacion.Height + 0);
            //////Rectangle rec = new Rectangle(posicion, size);
            //////Rectangle rect = new Rectangle(0, 0, 200, 200);

            //////////rec.Width = this.tabPagParametros.Width;
            //////////rec.Height = this.tabPagParametros.Height; 

            //////Graphics g = e.Graphics;
            //////// g = this.CreateGraphics();
            //////Pen p = new Pen(Color.Black, 2);
            //////// g.DrawRectangle(p, 20, 20, 50, 50);
            //////g.DrawRectangle(p, rec);
            //////// g.DrawRectangle(p, this.tabPagParametros.Bounds); 
        }

        private void tabControlFacturacion_DrawItem(object sender, DrawItemEventArgs e)
        {

        }
        #endregion Dibujar Cuadro

        #region Buscar ClaveSSA
        private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            //////string sCadena = "";
            //////myLeer = new clsLeer(ref ConexionLocal);

            //////if (txtClaveSSA.Text.Trim() != "")
            //////{
            //////    myLeer.DataSetClase = Consultas.ClavesSSA_Sales(txtClaveSSA.Text.Trim(), true, "txtClaveSSA_Validating");
            //////    sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
            //////    auditoria.GuardarAud_MovtosUni("*", sCadena);

            //////    if (myLeer.Leer())
            //////    {
            //////        CargarDatosClave();
            //////    }
            //////    else
            //////    {
            //////        txtClaveSSA.Text = "";
            //////        lblClaveSSA.Text = "";
            //////        txtClaveSSA.Focus();
            //////    }
            //////}
        }

        private void CargarDatosClave()
        {
            //////txtClaveSSA.Text = myLeer.Campo("ClaveSSA");
            //////lblClaveSSA.Text = myLeer.Campo("DescripcionSal");

            //////if (myLeer.Campo("Status") == "C")
            //////{
            //////    General.msjUser("La ClaveSSA seleccionada se encuentra cancelada. Verifique");
            //////    txtClaveSSA.Text = "";
            //////    lblClaveSSA.Text = "";
            //////}
        }

        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayudas.ClavesSSA_Sales("txtClaveSSA_KeyDown");

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosClave();
                }
            }
        }
        #endregion Buscar ClaveSSA

        #region Limpiar 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            sValor_Cero = (0).ToString(sFormato);


            Fg.IniciaControles();
            ////txtClaveSSA.Enabled = false; 

            //tabPagParametros.Focus();
            dMontoFinanciamiento = 0.0000;
            ////nmMontoAFacturar.Enabled = true;
            ////nmMontoAFacturar.Value = 0;
            sFolio = "";
            sMensaje = "";
            bEsExcedente_Rubro = false;
            bEsExcedente_Concepto = false;

            ////lblPorAplicarRubro.BackColor = Color.Transparent;
            ////lblPorAplicarConcepto.BackColor = Color.Transparent;

            cboTipoUnidades.SelectedIndex = 0;
            cboJurisdicciones.SelectedIndex = 0;
            cboFarmacias.SelectedIndex = 0;

            rdoAmbos.Checked = true;
            myGrid.Limpiar(false);

            //////// Manipular el campo Montos 
            //////if (tpFacturacion != eEsquemaDeFacturacion.Montos)
            //////{
            //////    nmMontoAFacturar.Enabled = false; 
            //////}

            btnGuardarGrid.Enabled = false;
            IniciarToolBar(true, false, false, false);

            //txtPrograma.Enabled = false;
            //txtSubPrograma.Enabled = false; 
            btnAgregarProgramasDeAtencion.Enabled = false;
            btnAgregarProgramasDeAtencion.Visible = false;
            lblPrograma.Width = lblCliente.Width;
            lblSubPrograma.Width = lblCliente.Width;

            InicializarImportes(); 
            ////lblSubTotalSinGrabar.Text = sValor_Cero;
            ////lblSubTotalGrabado.Text = sValor_Cero;
            ////lblIva.Text = sValor_Cero;
            ////lblTotal.Text = sValor_Cero;  

            //SendKeys.Send("{TAB}"); 
            //tabControlFacturacion.SelectTab(0);
            //tabControlFacturacion.SelectedIndex = 0;
            //tabControlFacturacion.SelectNextControl(txtcliente, true, false, false, false);            
            txtcliente.Focus();
        }

        private void InicializarImportes()
        {
            lblSubTotalSinGrabar.Text = sValor_Cero;
            lblSubTotalGrabado.Text = sValor_Cero;
            lblIva.Text = sValor_Cero;
            lblTotal.Text = sValor_Cero;  
        }
        #endregion Limpiar 

        #region Ejecutar 
        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                InicializarImportes(); 

                bSeEncontroInformacion = false;
                btnNuevo.Enabled = false;
                btnEjecutar.Enabled = false;
                btnImprimir.Enabled = false;

                ActivarControles(false);

                btnEjecutar.Enabled = !(myGrid.Rows > 0);

                bSeEjecuto = false;
                tmEjecuciones.Enabled = true;
                tmEjecuciones.Start();


                Cursor.Current = Cursors.WaitCursor;
                System.Threading.Thread.Sleep(1000);

                _workerThread = new Thread(this.ObtenerInformacion);
                _workerThread.Name = "GenerandoRemision";
                _workerThread.Start();
            }

        }
        #endregion Ejecutar 

        #region Funciones
        private void IniciarToolBar(bool Ejecutar, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;

            if (!bEsquemaDeFacturacionValido)
            {
                btnEjecutar.Enabled = false; 
            }
        }

        private void ObtenerTipoUnidades()
        {
            cboTipoUnidades.Clear();
            cboTipoUnidades.Add("0", "<< TODAS >>");
            cboTipoUnidades.Add(Consultas.TipoUnidades("", "ObtenerTipoUnidades"), true, "IdTipoUnidad", "Descripcion");
            cboTipoUnidades.SelectedIndex = 0;
        }

        private void ObtenerEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(Consultas.EstadosConFarmacias("ObtenerEstados"), true, "IdEstado", "NombreEstado");
            cboEstados.SelectedIndex = 0;
        }

        private void ObtenerJurisdicciones()
        {
            dtsJurisdicciones = new DataSet();
            dtsJurisdicciones = Consultas.Jurisdicciones("", "ObtenerTipoUnidades");

            ////cboJurisdicciones.Clear();
            ////cboJurisdicciones.Add("0", "<< Seleccione >>");
            ////cboJurisdicciones.Add(Consultas.Jurisdicciones("", "ObtenerTipoUnidades"), true, "IdJurisdiccion", "NombreJurisdiccion");
            ////cboJurisdicciones.SelectedIndex = 0;
            cboJurisdicciones.Clear();
            cboJurisdicciones.Add("0", "<< Seleccione >>");
        }

        private void ObtenerFarmacias()
        {
            dtsFarmacias = new DataSet();
            dtsFarmacias = Consultas.Farmacias("ObtenerFarmacias");
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
        }

        private void CargarFarmacias()
        {
            string sFiltro = string.Format("IdEstado = '{0}' ", cboEstados.Data);

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");

            if (cboJurisdicciones.SelectedIndex > 0)
            {
                sFiltro = sFiltro + string.Format(" And IdJurisdiccion = '{0}' ", cboJurisdicciones.Data);
            }

            if (cboTipoUnidades.SelectedIndex > 0)
            {
                sFiltro = sFiltro + string.Format(" And IdTipoUnidad = '{0}' ", cboTipoUnidades.Data);
            }

            try
            {
                cboFarmacias.Add(dtsFarmacias.Tables[0].Select(sFiltro,"NombreFarmacia"), true, "IdFarmacia", "NombreFarmacia");
            }
            catch { }
                
            
            cboFarmacias.SelectedIndex = 0;
        }

        private void CargarJurisdicciones()
        {
            string sFiltro = string.Format("IdEstado = '{0}' ", cboEstados.Data);

            cboJurisdicciones.Clear();
            cboJurisdicciones.Add("0", "<< Seleccione >>");

            ////if (cboEstados.SelectedIndex > 0)
            ////{
            ////    sFiltro = sFiltro + string.Format(" And IdJurisdiccion = '{0}' ", cboJurisdicciones.Data);
            ////}

            try
            {
                //  "IdJurisdiccion", "NombreJurisdiccion");
                cboJurisdicciones.Add(dtsJurisdicciones.Tables[0].Select(sFiltro, "NombreJurisdiccion"), true, "IdJurisdiccion", "NombreJurisdiccion");
            }
            catch { }

            cboJurisdicciones.SelectedIndex = 0; 
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtcliente.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de cliente inválida, verifique.");
                txtcliente.Focus();
            }

            if (bRegresa && txtSubCliente.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Sub-Cliente inválida, verifique.");
                txtSubCliente.Focus();
            }

            ////if (bRegresa && txtPrograma.Text == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("Clave de Programa inválida, verifique.");
            ////    txtPrograma.Focus();
            ////}

            ////if (bRegresa && txtSubPrograma.Text == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("Clave de SubPrograma inválida, verifique.");
            ////    txtSubPrograma.Focus();
            ////}


            ////if (bRegresa && cboTipoUnidades.Data == "0")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("Debe seleccionar un Tipo de Unidad, verifique.");
            ////    cboTipoUnidades.Focus();
            ////}

            //////if (bRegresa && cboJurisdicciones.Data == "0")
            //////{
            //////    bRegresa = false;
            //////    General.msjUser("No ha seleccionado una Jurisdicción valida, verifique.");
            //////    cboJurisdicciones.Focus();
            //////}

            if (bRegresa && cboFarmacias.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Farmacia valida, verifique.");
                cboFarmacias.Focus();
            }


            if (bRegresa && tpFacturacion == eEsquemaDeFacturacion.Montos)
            {
                ////if (nmMontoAFacturar.Value == 0)
                ////{
                ////    bRegresa = false;
                ////    General.msjUser("El Monto a facturar debe ser mayor a cero, verifique.");
                ////    nmMontoAFacturar.Focus();
                ////}
            }

            ////if (bRegresa && (double)nmMontoAFacturar.Value > dMontoFinanciamiento )
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("El Monto a facturar ha rebasado el Monto del Financiamiento, verifique.");
            ////    nmMontoAFacturar.Focus();
            ////}

            if (bRegresa && (dtpFechaInicial.Value > dtpFechaFinal.Value))
            {
                bRegresa = false;
                General.msjUser("La Fecha Final no puede ser mayor que la Fecha Inicial. Verifique");
                dtpFechaFinal.Focus();
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

                if (myGrid.Rows > 0)
                {
                    //tabControlFacturacion.SelectTab(1); 
                }

                if ( !bSeEncontroInformacion ) 
                {
                    _workerThread.Interrupt();
                    _workerThread = null;

                    ActivarControles(true); 

                    if (bSeEjecuto)
                    {
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                        ActivarControles(true);
                    }
                }
            }
        }

        private void ObtenerInformacion()
        {
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;
            string sFarmacia = "*";

            ////if (ValidaDatos())
            {
                ActivarControles(false);
                if (cboFarmacias.Data != "0")
                {
                    sFarmacia = cboFarmacias.Data;
                }

                CargarGrid();
            }

            bEjecutando = false;
            this.Cursor = Cursors.Default;
        }

        private void ActivarControles(bool Activar)
        {
            txtcliente.Enabled = Activar;
            txtSubCliente.Enabled = Activar;
            txtPrograma.Enabled = Activar;
            txtSubPrograma.Enabled = Activar;

            cboEstados.Enabled = Activar; 
            cboJurisdicciones.Enabled = Activar;
            cboFarmacias.Enabled = Activar;
            cboTipoUnidades.Enabled = Activar;

            ////nmMontoAFacturar.Enabled = Activar;
            dtpFechaInicial.Enabled = Activar;
            dtpFechaFinal.Enabled = Activar;
            ////txtClaveSSA.Enabled = Activar;
            btnEjecutar.Enabled = Activar;
            rdoMedicamento.Enabled = Activar;
            rdoMaterialDeCuracion.Enabled = Activar;
            rdoAmbos.Enabled = Activar; 
            ////chkRemisionarClave.Enabled = Activar; 

            ////cboFarmacias.Enabled = false;
            ////dtpFechaInicial.Enabled = false;
            ////dtpFechaFinal.Enabled = false;
        }

        private void ObtenerMontoPorAplicar()
        {
            //////double dDisponibleRubro = 0, dDisponibleConcepto = 0;

            //////string sSql = string.Format("Exec spp_FACT_Remisiones_Motos_Por_Aplicar '{0}', '{1}' ", 
            //////    txtRubro.Text.Trim(), txtConcepto.Text.Trim());

            //////bEsExcedente_Rubro = false; 
            //////if (!myLeer.Exec(sSql))
            //////{
            //////    Error.GrabarError(myLeer, "ObtenerDisponibleRubro()");
            //////    General.msjError("Ocurrió un error al obtener el Monto Disponible del Rubro.");
            //////}
            //////else
            //////{
            //////    if (myLeer.Leer())
            //////    {
            //////        dDisponibleRubro = myLeer.CampoDouble("Disponible_Rubro");
            //////        dDisponibleConcepto = myLeer.CampoDouble("Disponible_Concepto");

            //////        lblPorAplicarRubro.Text = dDisponibleRubro.ToString(sFormato);
            //////        if (tpFacturacion == eEsquemaDeFacturacion.Montos)
            //////        {
            //////            if (dDisponibleRubro <= 0)
            //////            {
            //////                bEsExcedente_Rubro = true;
            //////                lblPorAplicarRubro.BackColor = Color.Red;
            //////                General.msjUser("Este Rubro ya ha excedido su monto disponible. Verifique.");
            //////            }

            //////            if (txtConcepto.Text.Trim() != "")
            //////            {
            //////                lblPorAplicarConcepto.Text = dDisponibleConcepto.ToString(sFormato);
            //////                if (dDisponibleConcepto <= 0)
            //////                {
            //////                    bEsExcedente_Rubro = true;
            //////                    lblPorAplicarConcepto.BackColor = Color.Red;
            //////                    General.msjUser("Este Concepto ya ha excedido su monto disponible. Verifique.");
            //////                }
            //////            }
            //////        }
            //////    }
            //////}
        }

        #region Eventos 
        private void txtcliente_TextChanged(object sender, EventArgs e)
        {
            ////lblRubro.Text = "";
            txtSubCliente.Text = "";
            ////lblIdCliente.Text = "";
            lblCliente.Text = "";
            ////lblIdSubCliente.Text = "";
            lblSubCliente.Text = "";
        }

        private void txtSubCliente_TextChanged(object sender, EventArgs e)
        {
            ////lblConcepto.Text = "";
        }

        private void txtPrograma_TextChanged(object sender, EventArgs e)
        {
            lblPrograma.Text = "";
            txtSubPrograma.Text = "";
            lblSubPrograma.Text = "";
        }

        private void txtSubPrograma_TextChanged(object sender, EventArgs e)
        {
            lblSubPrograma.Text = "";
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarJurisdicciones();
        }

        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFarmacias();
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboTipoUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFarmacias();
        }

        private void txtClaveSSA_TextChanged(object sender, EventArgs e)
        {
            ////lblClaveSSA.Text = "";
        }
        #endregion Eventos 
        #endregion Funciones

        #region Grid 
        private void CargarGrid()
        {
            clsLeer leerResultados = new clsLeer(); 
            string sTipoUnidad = "*";
            string sJurisdiccion = "*";
            string sFarmacia = "*";
            string sClaveSSA = "*";
            string sIdTipoInsumo = "00";
            string sPrograma = txtPrograma.Text.Trim() != "" ? txtPrograma.Text.Trim(): "*";
            string sSubPrograma = txtSubPrograma.Text.Trim() != "" ? txtSubPrograma.Text.Trim() : "*";

            if (cboTipoUnidades.Data != "0")
            {
                sTipoUnidad = cboTipoUnidades.Data;
            }

            if (cboJurisdicciones.Data != "0")
            {
                sJurisdiccion = cboJurisdicciones.Data;
            }

            if (cboFarmacias.Data != "0")
            {
                sFarmacia = cboFarmacias.Data;
            }

            ////if (txtClaveSSA.Text.Trim() != "")
            ////{
            ////    sClaveSSA = txtClaveSSA.Text.Trim();
            ////}

            if (rdoMaterialDeCuracion.Checked)
            {
                sIdTipoInsumo = "01";
            }

            if (rdoMedicamento.Checked)
            {
                sIdTipoInsumo = "02";
            }

            string sSql = string.Format("Exec spp_INT_MA__FACTURACION__GetRemision " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdJurisdiccion = '{2}', @IdFarmacia = '{3}', " + 
                " @FechaInicial = '{4}', @FechaFinal = '{5}', @IdCliente = '{6}', @IdSubCliente = '{7}', " + 
                " @IdPrograma = '{8}', @IdSubPrograma = '{9}', @IdTipoInsumo = '{10}', @IdFarmaciaGenera = '{11}', " + 
                " @IdPersonal = '{12}', @Remisionar = '{13}' ",
                DtGeneral.EmpresaConectada, cboEstados.Data, sJurisdiccion, sFarmacia,
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value),
                txtcliente.Text, txtSubCliente.Text, sPrograma, sSubPrograma, sIdTipoInsumo,
                DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, 0); 

            myGrid.Limpiar(false);
            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "CargarGrid()");
                General.msjError("Ocurrió un error al obtener las Unidades y periodos a facturar.");
            }
            else
            {
                if (myLeer.Leer())
                {
                    btnEjecutar.Enabled = false; 
                    btnGuardarGrid.Enabled = true;

                    myLeer.RenombrarTabla(1, "Detallado");
                    myLeer.RenombrarTabla(2, "Resumen");
                    myLeer.RenombrarTabla(3, "ResumenPrograma"); 

                    leerResultados.DataTableClase = myLeer.Tabla("Detallado");

                    myGrid.LlenarGrid(leerResultados.DataSetClase);
                    //tabInformacionFacturar.Focus();
                    //tabControlFacturacion.SelectTab(1); 
                }
                else
                {
                    bSeEjecuto = true;
                    ActivarControles(true);
                }
            }

            ////lblSubTotalSinGrabar.Text = myGrid.TotalizarColumnaDou((int)Cols.SubTotal_SinGravar).ToString(sFormato);
            ////lblSubTotalGrabado.Text = myGrid.TotalizarColumnaDou((int)Cols.SubTotal_Gravado).ToString(sFormato);
            ////lblIva.Text = myGrid.TotalizarColumnaDou((int)Cols.Iva).ToString(sFormato);
            ////lblTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.Importe).ToString(sFormato);


            leerResultados.DataTableClase = myLeer.Tabla("Resumen");
            leerResultados.Leer();
            lblSubTotalSinGrabar.Text = leerResultados.CampoDouble("SubTotal_NoGravado__Aseguradora").ToString(sFormato);
            lblSubTotalGrabado.Text = leerResultados.CampoDouble("SubTotal_Gravado__Aseguradora").ToString(sFormato);
            lblIva.Text = leerResultados.CampoDouble("Iva__Aseguradora").ToString(sFormato);
            lblTotal.Text = leerResultados.CampoDouble("ImporteTotal").ToString(sFormato); 
        }

        private bool PrepararInformacion_Facturar()
        {
            bool bRegresa = true;
            //////string sSql = "";
            //////string sIdFarmacia = "";
            //////int iFolio = 0;

            //////sIdentificador = General.MacAddress + General.FechaSistema;

            //////sSql = string.Format("Delete From FACT_Informacion_Proceso_Facturacion Where HostName  = '{0}' and  Identificador = '{1}' ", General.NombreEquipo, sIdentificador); 
            //////myLeer.Exec(sSql); 

            //////for (int i = 1; i <= myGrid.Rows; i++)
            //////{
            //////    if (myGrid.GetValueBool(i, (int)Cols.Facturar))
            //////    {
            //////        sIdFarmacia = myGrid.GetValue(i, (int)Cols.IdFarmacia);
            //////        iFolio = myGrid.GetValueInt(i, (int)Cols.Folio);

            //////        sSql = string.Format("Exec spp_Mtto_FACT_Informacion_Proceso_Facturacion '{0}', '{1}' , '{2}', '{3}', '{4}' ",
            //////            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia, iFolio, sIdentificador);
            //////        if (!myLeer.Exec(sSql))
            //////        {
            //////            bRegresa = false;
            //////            General.msjError("Ocurrió un error al preparar la información para remisionar.");
            //////            break;
            //////        }
            //////    }
            //////}

            return bRegresa;
        }

        private bool ValidarFacturacion()
        {
            bool bRegresa = true; 
            //bool bContinuar = true;            
            //string sMensaje = "El Monto a remisionar ha rebasado el Monto del Financiamiento, ¿Desea Continuar con la generacion de la Remisión ?";

            if (myGrid.Rows == 0)
            {
                General.msjUser("No existe informacion para Remisionar, verifique.");
                bRegresa = false;
            }
            //else
            //{
            //    ////if (tpFacturacion == eEsquemaDeFacturacion.Montos)
            //    ////{
            //    ////    double dDisponibleConcepto = Convert.ToDouble(lblPorAplicarConcepto.Text.Replace(",", ""));

            //    ////    if ((double)nmMontoAFacturar.Value > dDisponibleConcepto)
            //    ////    {
            //    ////        if (General.msjCancelar(sMensaje) == DialogResult.Yes)
            //    ////        {
            //    ////            bContinuar = true;
            //    ////        }
            //    ////        else
            //    ////        {
            //    ////            bContinuar = false;
            //    ////        }
            //    ////    }
            //    ////}

            //    //if( bContinuar )
            //    //{
            //    //    for (int i = 1; i <= myGrid.Rows; i++)
            //    //    {
            //    //        if (myGrid.GetValueBool(i, (int)Cols.Facturar))
            //    //        {
            //    //            bRegresa = true;
            //    //            break;
            //    //        }
            //    //    }

            //    //    if (!bRegresa)
            //    //    {
            //    //        General.msjUser("Debe seleccionar al menos un Folio para Remisionar, verifique");
            //    //        grdReporte.Focus();
            //    //    }
            //    //}
            //}

            return bRegresa;
        }
        #endregion Grid 

        #region Guardar Informacion 
        private void btnGuardarGrid_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (ValidarFacturacion())
            {
                if (!ConexionLocal.Abrir())
                {
                    Error.LogError(ConexionLocal.MensajeError);
                    General.msjErrorAlAbrirConexion();  
                }
                else 
                {
                    ConexionLocal.IniciarTransaccion();

                    //if (PrepararInformacion_Facturar())
                    {
                        bContinua = GenerarRemision();
                    }

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); 
                        btnNuevo_Click(null, null);//Se limpia la pantalla ya que en ocaciones el grid falla y asi se evitan errores.
                    }
                    else
                    {
                        //txtFolio.Text = "*";
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnGuardarGrid_Click");
                        General.msjError("Ocurrió un error al guardar la Información.");
                        IniciarToolBar(false, true, false, false);

                    }

                    ConexionLocal.Cerrar();
                }
            }
        }

        private bool GenerarRemision()
        {
            bool bRegresa = true;
            int iTipoRemision = 1, iEsExcedente = 0;
            string sIdTipoInsumo = "00", sObservaciones = "", sJurisdiccion = "*", sFarmacia = "*";
            int iTipoDispensacion = 0;
            string sPrograma = txtPrograma.Text.Trim() != "" ? txtPrograma.Text.Trim() : "*";
            string sSubPrograma = txtSubPrograma.Text.Trim() != "" ? txtSubPrograma.Text.Trim() : "*";
            string sSql = "";

            if (rdoMaterialDeCuracion.Checked)
            {
                sIdTipoInsumo = "01";
            }

            if (rdoMedicamento.Checked)
            {
                sIdTipoInsumo = "02";
            }

            if (cboJurisdicciones.Data != "0")
            {
                sJurisdiccion = cboJurisdicciones.Data;
            }

            if (cboFarmacias.Data != "0")
            {
                sFarmacia = cboFarmacias.Data;
            }

            //if (bEsExcedente_Rubro)
            //{
            //    iEsExcedente = 1;
            //}

            //if (bEsExcedente_Concepto)
            //{
            //    iEsExcedente = 2;
            //}

            //if (bEsExcedente_Rubro && bEsExcedente_Concepto)
            //{
            //    iEsExcedente = 3;
            //}

            ////sSql = string.Format("Exec spp_FACT_Remisiones_Periodos " + 
            ////    " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', [ {8} ], " + 
            ////    " '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}' ",
            ////    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iTipoRemision, 
            ////    lblIdCliente.Text, lblIdSubCliente.Text,
            ////    txtRubro.Text.Trim(), txtConcepto.Text.Trim(), sListaDeSeleccion, 
            ////    General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value),
            ////    nmMontoAFacturar.Value, DtGeneral.IdPersonal, sObservaciones, sIdTipoInsumo, 
            ////    iEsExcedente, sIdentificador, iTipoDispensacion, txtClaveSSA.Text.Trim());

            sSql = string.Format("Exec spp_INT_MA__FACTURACION__GetRemision " +
                " @IdEmpresa = '{0}', @IdEstadoGenera = '{1}', @IdFarmaciaGenera = '{2}', @IdPersonal = '{3}', " + 
                " @IdEstado = '{4}', @IdJurisdiccion = '{5}', @IdFarmacia = '{6}', @FechaInicial = '{7}', @FechaFinal = '{8}', " + 
                " @IdCliente = '{9}', @IdSubCliente = '{10}', @IdPrograma = '{11}', @IdSubPrograma = '{12}', @IdTipoInsumo = '{13}', @Remisionar = '{14}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, 
                cboEstados.Data, sJurisdiccion, sFarmacia, General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value),
                txtcliente.Text, txtSubCliente.Text, sPrograma, sSubPrograma, sIdTipoInsumo, 1);

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false; 
            }
            else
            {
                if (myLeer.Leer())
                {
                    sFolio = myLeer.Campo("Folio");
                    sMensaje = myLeer.Campo("Mensaje");
                }
            }

            return bRegresa;
        }
        #endregion Guardar Informacion 

        private void chkRemisionarClave_CheckedChanged(object sender, EventArgs e)
        {
            ////txtClaveSSA.Text = "";
            ////lblClaveSSA.Text = ""; 
            ////txtClaveSSA.Enabled = chkRemisionarClave.Checked;

            ////if (txtClaveSSA.Enabled)
            ////{
            ////    txtClaveSSA.Focus(); 
            ////}
        }

        #region Programas de Atencion 
        private void btnAgregarProgramasDeAtencion_Click(object sender, EventArgs e)
        {
            CargarProgramasDeAtencion();
        }

        private void Obtener_ListaProgramasAtencion()
        {
            string sSql = "";

            sSql = string.Format(
                "Select " +
                "   ROW_NUMBER() OVER (order by IdPrograma, IdSubPrograma)as Renglon, \n" +
                "   IdPrograma, Programa, IdSubPrograma, SubPrograma, 0 as Procesar \n" +
                "From vw_Clientes_Programas_Asignados_Unidad (NoLock) " +
                "Where IdEstado = '{0}' and IdCliente = '{1}' and IdSubCliente = '{2}' " +
                "Group by IdPrograma, Programa, IdSubPrograma, SubPrograma ",
                sIdEstado, txtcliente.Text, txtSubCliente.Text);

            dtsProgramas_SubProgramas = new DataSet();
            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "Obtener_ListaProgramasAtencion()");
                General.msjUser("Ocurrió un error al obtener la lista de programa de atención."); 
            }
            else
            {
                dtsProgramas_SubProgramas = myLeer.DataSetClase; 
            }

        }

        private void CargarProgramasDeAtencion()
        {
            FrmRemision_ProgramasDeAtencion f = new FrmRemision_ProgramasDeAtencion(dtsProgramas_SubProgramas);
            f.ShowInTaskbar = false; 
            f.ShowDialog();

            sListaDeSeleccion = f.Seleccion;
            dtsProgramas_SubProgramas = f.SeleccionDataSet;
        }
        #endregion Programas de Atencion

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        #region ValidarClavesFinanciamiento
        //private bool ValidarClaves_Financiamiento()
        //{
        //    bool bRegresa = false;
        //    int iOpcion = 1;
        //    string sSql = string.Format("Exec spp_FACT_Rpt_ClavesFinanciamiento '{0}', '{1}', '{2}'  ", txtcliente.Text, txtSubCliente.Text, iOpcion);

        //    if (!myLeer.Exec(sSql)) 
        //    {
        //        bRegresa = false;
        //        Error.GrabarError(myLeer, "ValidarClaves_Financiamiento()");
        //        General.msjError("Ocurrió un erorr al validar las claves.");
        //    }
        //    else
        //    {
        //        if (myLeer.Leer())
        //        {
        //            bRegresa = true;
        //            FrmListaClavesFinanciamiento f = new FrmListaClavesFinanciamiento();
                    
        //            f.Rubro = txtcliente.Text;
        //            ////f.RubroDesc = lblRubro.Text;
        //            f.Concepto = txtSubCliente.Text;
        //            ////f.ConceptoDesc = lblConcepto.Text;
        //            f.ClavesFinanciamiento = myLeer.DataSetClase;
        //            f.MostrarClavesSinPrecio(1);                    
        //        }
        //    }

        //    return bRegresa;
        //}
        #endregion ValidarClavesFinanciamiento
    }
}
