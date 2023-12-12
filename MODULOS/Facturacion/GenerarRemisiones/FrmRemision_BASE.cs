﻿using System;
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
    public partial class FrmRemision_BASE : FrmBaseExt
    {
        enum Cols
        {
            IdJurisdiccion = 1, Jurisdiccion = 2, IdFarmacia = 3, Farmacia = 4,
            FechaRegistroCierre = 5, Folio = 6,
            FechaCorte = 7, FechaCierreMinima = 8, FechaCierreMaxima = 9,
            Facturar = 10
        }

        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer, myLeerDetalles;
        clsGrid myGrid;
        clsGrid gridUnidades;
        clsGrid gridFolios; 
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


        DataSet dtsProgramas_SubProgramas = new DataSet();
        string sListaDeSeleccion = "";

        public FrmRemision_BASE(eTipoRemision TipoDeRemisionGeneral)
        {
            InitializeComponent();

            tpTipoDeRemision = TipoDeRemisionGeneral; 

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

            gridUnidades = new clsGrid(ref grdUnidades, this);
            gridUnidades.AjustarAnchoColumnasAutomatico = true; 
            
            gridFolios = new clsGrid(ref grdFolios, this);
            gridFolios.AjustarAnchoColumnasAutomatico = true; 


            SetEsquemaDeFacturacion();
            SetTipoDeRemision(); 

            ObtenerFarmacias();
            ObtenerTipoUnidades();
            ObtenerJurisdicciones();

        }

        private void SetTipoDeRemision()
        {
            if (tpTipoDeRemision == eTipoRemision.Insumo)
            {
                this.Text = "Remisiones de insumos";
            }

            if (tpTipoDeRemision == eTipoRemision.Administracion)
            {
                this.Text = "Remisiones de servicio";
            }

            if (tpTipoDeRemision == eTipoRemision.VentaDirecta)
            {
                this.Text = "Remisiones de venta directa";

                bFrameUnidades = false;
                bFrameFolios = true;
            }


            if (tpTipoDeRemision != eTipoRemision.VentaDirecta)
            {
                grdUnidades.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top; 
                FrameUnidades.Width = FrameTiposDeUnidades.Width;
                //grdUnidades.Width = 1400; 

                FrameFolios.Visible = false;
                rdoInsumoAmbos.Enabled = false;
                rdoInsumoAmbos.Visible = false;

                rdoInsumoMedicamento.Left += 10;
                rdoInsumoMaterialDeCuracion.Left += 10;
            }
        }

        private void FrmRemision_BASE_Load(object sender, EventArgs e)
        {
            InicializarPantalla();
            Obtener_ListaProgramasAtencion();
        }

        #region Esquema de facturacion
        private void SetEsquemaDeFacturacion()
        {
            tpFacturacion = DtIFacturacion.EsquemaDeFacturacion;
            string sTituloFacturacion = "";

            switch (tpFacturacion)
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
        private void txtRubro_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";
            myLeer = new clsLeer(ref ConexionLocal);

            bEsExcedente_Rubro = false;
            if (txtRubro.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.FuentesDeFinanciamiento_Encabezado(txtRubro.Text.Trim(), "txtRubro_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosRubro();
                    ObtenerMontoPorAplicar();
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
            txtRubro.Text = myLeer.Campo("IdFuenteFinanciamiento");
            lblRubro.Text = myLeer.Campo("Estado") + " -- " + myLeer.Campo("NumeroDeContrato");  

            lblIdCliente.Text = myLeer.Campo("IdCliente"); 
            lblCliente.Text = myLeer.Campo("Cliente"); 
            lblIdSubCliente.Text = myLeer.Campo("IdSubCliente");
            lblSubCliente.Text = myLeer.Campo("SubCliente");

            if (myLeer.Campo("Status") == "C")
            {
                General.msjUser("El Rubro seleccionado se encuentra cancelado. Verifique");
                txtRubro.Text = "";
                lblRubro.Text = "";
            }

            Obtener_ListaProgramasAtencion();
        }

        private void txtRubro_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Encabezado("txtPrograma_KeyDown");

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosRubro();
                }
            }
        }
        #endregion Buscar Rubro

        #region Buscar Concepto
        private void txtConcepto_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";
            myLeer = new clsLeer(ref ConexionLocal);

            dMontoFinanciamiento = 0.0000;
            bEsExcedente_Concepto = false;
            if (txtConcepto.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.FuentesDeFinanciamiento_Detalle(txtRubro.Text.Trim(), txtConcepto.Text.Trim(), "txtRubro_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosConcepto();
                    ObtenerMontoPorAplicar();
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
            txtConcepto.Text = myLeer.Campo("IdFinanciamiento");
            lblConcepto.Text = myLeer.Campo("Financiamiento");
            dMontoFinanciamiento = myLeer.CampoDouble("MontoDetalle");
            if (myLeer.Campo("Status") == "C")
            {
                General.msjUser("El Financiamiento seleccionado se encuentra cancelado. Verifique");
                txtConcepto.Text = "";
                lblConcepto.Text = "";
            }

            if (ValidarClaves_Financiamiento())
            {
                btnEjecutar.Enabled = false;
            }
        }

        private void txtConcepto_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Detalle("txtPrograma_KeyDown", txtRubro.Text.Trim());

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosConcepto();
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

            if (myLeer.Campo("Status") == "C")
            {
                General.msjUser("El Programa seleccionado se encuentra cancelado. Verifique");
                txtPrograma.Text = "";
                lblPrograma.Text = "";
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
                myLeer.DataSetClase = Consultas.SubProgramas(txtPrograma.Text.Trim(), txtSubPrograma.Text.Trim(), "txtSubPrograma_Validating");
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

            if (myLeer.Campo("Status") == "C")
            {
                General.msjUser("El SubPrograma seleccionado se encuentra cancelado. Verifique");
                txtSubPrograma.Text = "";
                lblSubPrograma.Text = "";
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
        private void FrmRemision_BASE_Paint(object sender, PaintEventArgs e)
        {
            int iVar = 1;
            int iVarX = 1;

            Point posicion = new Point(tabControlFacturacion.Left - 0, tabControlFacturacion.Top - 0);
            Size size = new Size(tabControlFacturacion.Width + 0, tabControlFacturacion.Height + 0);
            Rectangle rec = new Rectangle(posicion, size);
            Rectangle rect = new Rectangle(0, 0, 200, 200);

            ////rec.Width = this.tabPagParametros.Width;
            ////rec.Height = this.tabPagParametros.Height; 

            Graphics g = e.Graphics;
            // g = this.CreateGraphics();
            Pen p = new Pen(Color.Black, 2);
            // g.DrawRectangle(p, 20, 20, 50, 50);
            g.DrawRectangle(p, rec);
            // g.DrawRectangle(p, this.tabPagParametros.Bounds); 
        }

        private void tabControlFacturacion_DrawItem(object sender, DrawItemEventArgs e)
        {

        }
        #endregion Dibujar Cuadro

        #region Limpiar
        private void InicializarPantalla()
        {
            Fg.IniciaControles();

            gridFolios.Limpiar(false);
            gridUnidades.Limpiar(false); 
            myGrid.Limpiar(false);

            //txtClaveSSA.Enabled = false;

            tabPagParametros.Focus();
            dMontoFinanciamiento = 0.0000;
            //nmMontoAFacturar.Enabled = true;
            //nmMontoAFacturar.Value = 0;
            sFolio = "";
            sMensaje = "";
            bEsExcedente_Rubro = false;
            bEsExcedente_Concepto = false;

            //lblPorAplicarRubro.BackColor = Color.Transparent;
            //lblPorAplicarConcepto.BackColor = Color.Transparent;

            cboTipoUnidades.SelectedIndex = 0;
            cboJurisdicciones.SelectedIndex = 0;
            cboFarmacias.SelectedIndex = 0;


            rdoProducto.Checked = true;
            rdoServicio.Checked = false;           
            rdoVenta.Checked = true;
            rdoConsignacion.Checked = false; 
            rdoInsumoMedicamento.Checked = true;
            rdoFechaCaptura.Checked = true; 

            ////// Manipular el campo Montos 
            //if (tpFacturacion != eEsquemaDeFacturacion.Montos)
            //{
            //    nmMontoAFacturar.Enabled = false;
            //}

            btnGuardarGrid.Enabled = false;
            IniciarToolBar(true, false, false, false);

            txtPrograma.Enabled = false;
            txtSubPrograma.Enabled = false;


            FrameProceso.Visible = false;
            //grdUnidades.Enabled = bFrameUnidades;
            //grdFolios.Enabled = bFrameFolios;
            //FrameUnidades.Enabled = bFrameUnidades;
            //FrameFolios.Enabled = bFrameFolios; 


            SendKeys.Send("{TAB}");
            tabControlFacturacion.SelectTab(0);
            tabControlFacturacion.SelectedIndex = 0;
            tabControlFacturacion.SelectNextControl(txtRubro, true, false, false, false);
            txtRubro.Focus();
            txtRubro.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 

        }
        #endregion Limpiar

        #region Ejecutar
        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                bSeEncontroInformacion = false;
                btnNuevo.Enabled = false;
                btnEjecutar.Enabled = false; 

                ActivarControles(false);

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

            if (!bEsquemaDeFacturacionValido)
            {
                btnEjecutar.Enabled = false;
            }
        }

        private void ObtenerTipoUnidades()
        {
            cboTipoUnidades.Clear();
            cboTipoUnidades.Add("0", "<< TODAS >>");
            //cboTipoUnidades.Filtro = " IdTipoUnidad not in ( '000', '005' ) ";
            cboTipoUnidades.Filtro = tpTipoDeRemision == eTipoRemision.VentaDirecta ? " IdTipoUnidad = '006' " : " IdTipoUnidad not in ( '000', '005', '006' ) "; 
            cboTipoUnidades.Add(Consultas.TipoUnidades("", "ObtenerTipoUnidades"), true, "IdTipoUnidad", "NombreTipoUnidad");
            cboTipoUnidades.SelectedIndex = 0;
        }

        private void ObtenerJurisdicciones()
        {
            cboJurisdicciones.Clear();
            cboJurisdicciones.Add("0", "<< TODAS >>");
            cboJurisdicciones.Add(Consultas.Jurisdicciones(sIdEstado, "ObtenerTipoUnidades"), true, "IdJurisdiccion", "NombreJurisdiccion");
            cboJurisdicciones.SelectedIndex = 0;
        }

        private void ObtenerFarmacias()
        {
            ////dtsFarmacias = new DataSet();
            ////dtsFarmacias = Consultas.Farmacias(sIdEstado, "ObtenerFarmacias");
            ////cboFarmacias.Clear();
            ////cboFarmacias.Add("0", "<< TODAS >>");

            string sSql = "";
            string sFiltro = tpTipoDeRemision == eTipoRemision.VentaDirecta ? " And IdTipoUnidad = '006' " : " And IdTipoUnidad not in ( '006' ) "; 

            sSql = string.Format("Select IdFarmacia, Farmacia, 0, 0, * " +
                " From vw_Farmacias (NoLock) " +
                "Where IdEstado = '{0}' and Status = 'A' and IdTipoUnidad not in ( '000', '005' )  {1} ",
                DtGeneral.EstadoConectado, sFiltro );

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "CargarFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            }
            else
            {
                dtsFarmacias = myLeer.DataSetClase; 
            }
        }

        private void CargarFarmacias()
        {
            string sFiltro = string.Format("IdEstado = '{0}' ", sIdEstado);
            clsLeer filtroFarmacias = new clsLeer(); 

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< TODAS >>");

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
                filtroFarmacias.DataRowsClase = dtsFarmacias.Tables[0].Select(sFiltro); 
                cboFarmacias.Add(dtsFarmacias.Tables[0].Select(sFiltro, "NombreFarmacia"), true, "IdFarmacia", "NombreFarmacia");
            }
            catch { }


            gridUnidades.Limpiar(false);
            gridUnidades.LlenarGrid(filtroFarmacias.DataSetClase);

            cboFarmacias.SelectedIndex = 0;
        }

        ////private void CargarFarmacias()
        ////{
        ////    bool bRegresa = false;
        ////    string sSql = "";

        ////    sSql = string.Format("Select IdFarmacia, Farmacia, 0, 0, * " +
        ////        " From vw_Farmacias (NoLock) " +
        ////        "Where IdEstado = '{0}' and Status = 'A' ",
        ////        DtGeneral.EstadoConectado, cboJurisdicciones.Data);


        ////    if (!myLeer.Exec(sSql))
        ////    {
        ////        Error.GrabarError(myLeer, "CargarFarmacias()");
        ////        General.msjError("Ocurrió un error al obtener la lista de farmacias.");
        ////    }
        ////    else
        ////    {
        ////        bRegresa = myLeer.Leer();
        ////        myGrid.LlenarGrid(myLeer.DataSetClase);
        ////    }
        ////}

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtRubro.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Rubro inválida, verifique.");
                txtRubro.Focus();
            }

            if (bRegresa && txtConcepto.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Concepto inválida, verifique.");
                txtConcepto.Focus();
            }

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
                    tabControlFacturacion.SelectTab(1);
                }

                if (!bSeEncontroInformacion)
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

            if (ValidaDatos())
            {
                ActivarControles(false);
                if (cboFarmacias.Data != "0")
                {
                    sFarmacia = cboFarmacias.Data;
                }

                cboFarmacias.Enabled = false;
                dtpFechaInicial.Enabled = false;
                dtpFechaFinal.Enabled = false;

                CargarGrid();
            }

            bEjecutando = false;
            this.Cursor = Cursors.Default;
        }

        private void ActivarControles(bool Activar)
        {
            txtRubro.Enabled = Activar;
            txtConcepto.Enabled = Activar;
            txtPrograma.Enabled = Activar;
            txtSubPrograma.Enabled = Activar;
            cboTipoUnidades.Enabled = Activar;
            cboJurisdicciones.Enabled = Activar;
            cboFarmacias.Enabled = Activar;
            //nmMontoAFacturar.Enabled = Activar;
            dtpFechaInicial.Enabled = Activar;
            dtpFechaFinal.Enabled = Activar;
            //txtClaveSSA.Enabled = Activar;
            btnEjecutar.Enabled = Activar;
            rdoInsumoMedicamento.Enabled = Activar;
            rdoInsumoMaterialDeCuracion.Enabled = Activar;
            //chkRemisionarClave.Enabled = Activar;
        }

        private void ObtenerMontoPorAplicar()
        {
            ////double dDisponibleRubro = 0, dDisponibleConcepto = 0;
            ////bEsExcedente_Rubro = false;

            ////string sSql = string.Format("Exec spp_FACT_Remisiones_Motos_Por_Aplicar '{0}', '{1}' ",
            ////    txtRubro.Text.Trim(), txtConcepto.Text.Trim());

            ////if (!myLeer.Exec(sSql))
            ////{
            ////    Error.GrabarError(myLeer, "ObtenerDisponibleRubro()");
            ////    General.msjError("Ocurrió un error al obtener el Monto Disponible del Rubro.");
            ////}
            ////else
            ////{
            ////    if (myLeer.Leer())
            ////    {
            ////        dDisponibleRubro = myLeer.CampoDouble("Disponible_Rubro");
            ////        dDisponibleConcepto = myLeer.CampoDouble("Disponible_Concepto");

            ////        lblPorAplicarRubro.Text = dDisponibleRubro.ToString(sFormato);
            ////        if (tpFacturacion == eEsquemaDeFacturacion.Montos)
            ////        {
            ////            if (dDisponibleRubro <= 0)
            ////            {
            ////                bEsExcedente_Rubro = true;
            ////                lblPorAplicarRubro.BackColor = Color.Red;
            ////                General.msjUser("Este Rubro ya ha excedido su monto disponible. Verifique.");
            ////            }

            ////            if (txtConcepto.Text.Trim() != "")
            ////            {
            ////                lblPorAplicarConcepto.Text = dDisponibleConcepto.ToString(sFormato);
            ////                if (dDisponibleConcepto <= 0)
            ////                {
            ////                    bEsExcedente_Rubro = true;
            ////                    lblPorAplicarConcepto.BackColor = Color.Red;
            ////                    General.msjUser("Este Concepto ya ha excedido su monto disponible. Verifique.");
            ////                }
            ////            }
            ////        }
            ////    }
            ////}
        }

        #region Eventos
        private void txtRubro_TextChanged(object sender, EventArgs e)
        {
            lblRubro.Text = "";
            txtConcepto.Text = "";
            lblIdCliente.Text = "";
            lblCliente.Text = "";
            lblIdSubCliente.Text = "";
            lblSubCliente.Text = "";
        }

        private void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            lblConcepto.Text = "";
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

        private void cboTipoUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFarmacias();
        }

        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFarmacias();
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion Eventos
        #endregion Funciones

        #region Grid
        private void CargarGrid()
        {
            string sSql = ""; 
            string sTipoUnidad = "*", sJurisdiccion = "*", sFarmacia = "*", sClaveSSA = "*";
            int iTipoDispensacion = 0; 

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

            if (rdoConsignacion.Checked)
            {
                iTipoDispensacion = 1; 
            }

            sSql = string.Format("Exec spp_FACT_Unidades_Facturacion_Pendiente_Administracion " + 
                " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', [ {6} ], '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado,
                txtRubro.Text, txtConcepto.Text, txtPrograma.Text, txtSubPrograma.Text, sListaDeSeleccion, 
                sClaveSSA,sTipoUnidad, sJurisdiccion, sFarmacia,
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value), iTipoDispensacion);

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
                    btnGuardarGrid.Enabled = true;
                    myGrid.LlenarGrid(myLeer.DataSetClase);
                    tabInformacionFacturar.Focus();
                    //tabControlFacturacion.SelectTab(1); 
                }
                else
                {
                    bSeEjecuto = true;
                }
            }
        }

        private bool PrepararInformacion_Facturar()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdFarmacia = "";
            int iFolio = 0;

            sIdentificador = General.MacAddress + General.FechaSistema;

            sSql = string.Format("Delete From FACT_Informacion_Proceso_Facturacion Where HostName  = '{0}' and  Identificador = '{1}' ", General.NombreEquipo, sIdentificador);
            myLeer.Exec(sSql);

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                if (myGrid.GetValueBool(i, (int)Cols.Facturar))
                {
                    sIdFarmacia = myGrid.GetValue(i, (int)Cols.IdFarmacia);
                    iFolio = myGrid.GetValueInt(i, (int)Cols.Folio);

                    sSql = string.Format("Exec spp_Mtto_FACT_Informacion_Proceso_Facturacion '{0}', '{1}' , '{2}', '{3}', '{4}' ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia, iFolio, sIdentificador);
                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        General.msjError("Ocurrió un error al preparar la información para remisionar.");
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private bool ValidarFacturacion()
        {
            bool bRegresa = false;
            bool bContinuar = true;
            string sMensaje = "El Monto a remisionar ha rebasado el Monto del Financiamiento, ¿Desea Continuar con la generacion de la Remisión ?";

            if (myGrid.Rows == 0)
            {
                General.msjUser("No existe informacion para Remisionar, verifique.");
            }
            else
            {
                ////if (tpFacturacion == eEsquemaDeFacturacion.Montos)
                ////{
                ////    double dDisponibleConcepto = Convert.ToDouble(lblPorAplicarConcepto.Text.Replace(",", ""));

                ////    if ((double)nmMontoAFacturar.Value > dDisponibleConcepto)
                ////    {
                ////        if (General.msjCancelar(sMensaje) == DialogResult.Yes)
                ////        {
                ////            bContinuar = true;
                ////        }
                ////        else
                ////        {
                ////            bContinuar = false;
                ////        }
                ////    }
                ////}

                if (bContinuar)
                {
                    for (int i = 1; i <= myGrid.Rows; i++)
                    {
                        if (myGrid.GetValueBool(i, (int)Cols.Facturar))
                        {
                            bRegresa = true;
                            break;
                        }
                    }

                    if (!bRegresa)
                    {
                        General.msjUser("Debe seleccionar al menos un Folio para Remisionar, verifique");
                        grdReporte.Focus();
                    }
                }
            }

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

                    ////if (PrepararInformacion_Facturar())
                    {
                        bContinua = GenerarRemision();
                    }

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        InicializarPantalla();
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
            int iTipoRemision = 2;
            int iEsExcedente = 0;
            string sIdTipoInsumo = "01"; 
            string sObservaciones = "";
            int iTipoDispensacion = 0;
            string sSql = ""; 

            if (rdoConsignacion.Checked)
            {
                iTipoDispensacion = 1; 
            }

            if (rdoInsumoMedicamento.Checked)
            {
                sIdTipoInsumo = "02";
            }

            if (bEsExcedente_Rubro)
            {
                iEsExcedente = 1;
            }

            if (bEsExcedente_Concepto)
            {
                iEsExcedente = 2;
            }

            if (bEsExcedente_Rubro && bEsExcedente_Concepto)
            {
                iEsExcedente = 3;
            }

            
            sSql = string.Format("Exec spp_FACT_GenerarRemisiones_General " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', @TipoDeRemision = '{3}', @IdFarmacia = '{4}', " + 
                " @IdCliente = '{5}', @IdSubCliente = '{6}', @IdFuenteFinanciamiento = '{7}', @IdFinanciamiento = '{8}', " + 
                " @Criterio_ProgramasAtencion = '{9}', @FechaInicial = '{10}', @FechaFinal = '{11}', @iMontoFacturar = '{12}', @IdPersonalFactura = '{13}', " + 
                " @Observaciones = '{14}', @IdTipoProducto = '{15}', @EsExcedente = '{16}', @Identificador = '{17}', @TipoDispensacion = '{18}', @ClaveSSA = '{19}', " + 
                " @FechaDeRevision = '{20}', @FoliosVenta = '{21}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iTipoRemision,
                lblIdCliente.Text, lblIdSubCliente.Text,
                txtRubro.Text.Trim(), txtConcepto.Text.Trim(), sListaDeSeleccion,
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value),
                0, DtGeneral.IdPersonal, sObservaciones, sIdTipoInsumo,
                iEsExcedente, sIdentificador, iTipoDispensacion, "");

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
        #endregion Guardar Informacion

        #region Programas de Atencion
        private void btnAgregarProgramasDeAtencion_Click(object sender, EventArgs e)
        {
            CargarProgramasDeAtencion();
        }

        private void Obtener_ListaProgramasAtencion()
        {
            string sSql = string.Format(
                "Select " +
                "   ROW_NUMBER() OVER (order by IdPrograma, IdSubPrograma)as Renglon, \n" +
                "   IdPrograma, Programa, IdSubPrograma, SubPrograma, 0 as Procesar \n" +
                "From vw_Clientes_Programas_Asignados_Unidad (NoLock) " +
                "Where IdEstado = '{0}' and IdCliente = '{1}' and IdSubCliente = '{2}' " +
                "Group by IdPrograma, Programa, IdSubPrograma, SubPrograma ",
                sIdEstado, lblIdCliente.Text, lblIdSubCliente.Text);

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

        #region ValidarClavesFinanciamiento
        private bool ValidarClaves_Financiamiento()
        {
            bool bRegresa = false;
            int iOpcion = 3;
            string sSql = string.Format("Exec spp_FACT_Rpt_ClavesFinanciamiento '{0}', '{1}', '{2}'  ", txtRubro.Text, txtConcepto.Text, iOpcion);

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(myLeer, "ValidarClaves_Financiamiento()");
                General.msjError("Ocurrió un erorr al validar las claves.");
            }
            else
            {
                if (myLeer.Leer())
                {
                    bRegresa = true;
                    FrmListaClavesFinanciamiento f = new FrmListaClavesFinanciamiento();
                    f.Rubro = txtRubro.Text;
                    f.RubroDesc = lblRubro.Text;
                    f.Concepto = txtConcepto.Text;
                    f.ConceptoDesc = lblConcepto.Text;
                    f.ClavesFinanciamiento = myLeer.DataSetClase;
                    f.MostrarClavesSinPrecio(2);
                }
            }

            return bRegresa;
        }
        #endregion ValidarClavesFinanciamiento
    }
}
