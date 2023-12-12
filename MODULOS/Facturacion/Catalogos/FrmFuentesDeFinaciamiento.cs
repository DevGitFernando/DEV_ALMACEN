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

namespace Facturacion.Catalogos
{
    public partial class FrmFuentesDeFinaciamiento : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion); 
        clsLeer myLeer, myLeerDetalles;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;
        FrmFuentesDeFinaciamiento_Claves Claves;

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

        public FrmFuentesDeFinaciamiento()
        {
            InitializeComponent();

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");
            myLeer = new clsLeer(ref ConexionLocal);
            myLeerDetalles = new clsLeer(ref ConexionLocal);
            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);

            myGrid = new clsGrid(ref grdClaves, this);
            myGrid.BackColorColsBlk = Color.White;
            myGrid.EstiloDeGrid = eModoGrid.ModoRow;
            myGrid.AjustarAnchoColumnasAutomatico = true; 
            grdClaves.EditModeReplace = true;

            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Claves = new FrmFuentesDeFinaciamiento_Claves();
            CargarEstados();
           
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

            IniciarToolBar(false, false, false);
            myGrid.Limpiar(false);
            bCapturaDeClavesHabilitada = false; 

            rdoExclusivoControlados.Enabled = true;
            rdoExclusivoLibres.Enabled = true;
            rdoTodos.Enabled = true;
            rdoExclusivoControlados.Checked = false;
            rdoExclusivoLibres.Checked = false;
            rdoTodos.Checked = false;

            rdoTipoFF_01_Ordinario.Enabled = true;
            rdoTipoFF_02_Excedente.Enabled = true;
            rdoTipoFF_01_Ordinario.Checked = false;
            rdoTipoFF_02_Excedente.Checked = false;

            rdoTipoUnidades_01_Ordinarias.Enabled = true;
            rdoTipoUnidades_01_Ordinarias.Enabled = true;
            rdoTipoUnidades_02_DosisUnitaria.Checked = false;
            rdoTipoUnidades_02_DosisUnitaria.Checked = false;


            // bFolioGuardado = false;
            lblMonto.Text = "0.0000";
            lblPiezas.Text = "0";
            lblMensajes.Text = "";

            if (!DtGeneral.EsAdministrador)
            {
                cboEstados.Data = DtGeneral.EstadoConectado;
                cboEstados.Enabled = false; 
            }

            bInicioDeModulo = false; 
            txtFolio.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;
            EliminarRenglonesVacios();

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

                    if (GuardarEncabezado())
                    {
                        bContinua = GuardarDetalle();                      
                    }

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
                        Error.GrabarError(myLeer, "btnGuardar_Click");
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
            FrmFuentesDeFinaciamiento_ImportarConfiguraciones f = new FrmFuentesDeFinaciamiento_ImportarConfiguraciones();

            f.ShowInTaskbar = false; 
            f.ShowDialog(this);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            DatosCliente.Funcion = "btnImprimir_Click()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "FACT_FuentesDeFinanciamiento.rpt";

            myRpt.Add("Folio", txtFolio.Text);

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (bRegresa)
            {
                btnNuevo_Click(null, null);
            }
            else
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
            
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

                //myGrid.AddRow();
                //myGrid.SetValue(1, (int)Cols.IdClave, "*");
            }
            else
            {
                myLeer.DataSetClase = Consultas.FuentesDeFinanciamiento_Encabezado(txtFolio.Text.Trim(), "txtFolio_Validating");
                if (!myLeer.Leer())
                {
                    txtFolio.Text = "";
                    txtFolio.Focus();
                }
                else 
                {
                    if (!DtGeneral.EsAdministrador)
                    {
                        if (myLeer.Campo("IdEstado") != DtGeneral.EstadoConectado)
                        {
                            bCargarInformacion = false;
                            General.msjUser("La fuente de financiamiento no pertenece al Estado conectado, verifique."); 
                        }
                    }

                    if (bCargarInformacion)
                    {
                        // bFolioGuardado = true;
                        lblMensajes.Text = "<F5>Ver Claves del Financiamiento";
                        CargarDatosEncabezado();
                        CargarDatosDetalle();
                    }
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
            txtFolio.Text = myLeer.Campo("IdFuenteFinanciamiento");
            sFolio = txtFolio.Text;
            txtFolio.Enabled = false;
            cboEstados.Data = myLeer.Campo("IdEstado");
            txtCliente.Text = myLeer.Campo("IdCliente");
            lblCte.Text = myLeer.Campo("Cliente");
            txtSubCliente.Text = myLeer.Campo("IdSubCliente");
            lblSubCte.Text = myLeer.Campo("SubCliente");
            txtNumeroDeContrato.Text = myLeer.Campo("NumeroDeContrato");
            txtNumeroDeLicitacion.Text = myLeer.Campo("NumeroDeLicitacion");
            txtAlias.Text = myLeer.Campo("ALIAS");
            dtpFechaInicial.Value = myLeer.CampoFecha("FechaInicial"); 
            dtpFechaFinal.Value = myLeer.CampoFecha("FechaFinal");

            bCapturaDeClavesHabilitada = true;
            //TipoClasificacionSSA 

            iTipoDeUnidades = myLeer.CampoInt("TipoDeUnidades"); 
            rdoTipoUnidades_01_Ordinarias.Enabled = iTipoDeUnidades == 1 ? true : false;
            rdoTipoUnidades_01_Ordinarias.Checked = rdoTipoUnidades_01_Ordinarias.Enabled;
            rdoTipoUnidades_02_DosisUnitaria.Enabled = iTipoDeUnidades == 2 ? true : false;
            rdoTipoUnidades_02_DosisUnitaria.Checked = rdoTipoUnidades_02_DosisUnitaria.Enabled;

            iTipoClasificacionSSA = myLeer.CampoInt("TipoClasificacionSSA"); 
            rdoExclusivoControlados.Enabled = iTipoClasificacionSSA == 1 ? true : false;
            rdoExclusivoControlados.Checked = rdoExclusivoControlados.Enabled;
            rdoExclusivoLibres.Enabled = iTipoClasificacionSSA == 2 ? true : false;
            rdoExclusivoLibres.Checked = rdoExclusivoLibres.Enabled;
            rdoTodos.Enabled = iTipoClasificacionSSA == 3 ? true : false;
            rdoTodos.Checked = rdoTodos.Enabled;

            iTipoDeFuenteDeFinanciamiento = Convert.ToInt32(myLeer.CampoBool("EsParaExcedente"));
            rdoTipoFF_01_Ordinario.Enabled = iTipoDeFuenteDeFinanciamiento == 0 ? true : false;
            rdoTipoFF_01_Ordinario.Checked = rdoTipoFF_01_Ordinario.Enabled;
            rdoTipoFF_02_Excedente.Enabled = iTipoDeFuenteDeFinanciamiento == 1 ? true : false;
            rdoTipoFF_02_Excedente.Checked = rdoTipoFF_02_Excedente.Enabled;

            chkEsDiferencial.Enabled = false; 
            chkEsDiferencial.Checked = myLeer.CampoBool("EsDiferencial");


            dMonto = Math.Round(myLeer.CampoDouble("Monto"), 4, MidpointRounding.ToEven);
            lblMonto.Text = dMonto.ToString(sFormatoMonto);

            iPiezas = myLeer.CampoInt("Piezas");
            lblPiezas.Text = iPiezas.ToString(sFormatoPiezas);

            IniciarToolBar(true, false, true);
            if (myLeer.Campo("Status") == "C")
            {                
                General.msjUser("El Cliente seleccionado se encuentra cancelado, verifique");
            }

            Fg.BloqueaControles(this, false, FramePrincipal);
            txtAlias.Enabled = true; 
            bRevisandoSeleccion = false; 
        }

        private void CargarDatosDetalle()
        {
            myLeerDetalles.DataSetClase = Consultas.FuentesDeFinanciamiento_Detalle(txtFolio.Text.Trim(), "txtFolio_Validating");
            if (myLeerDetalles.Leer())
            {
                myGrid.LlenarGrid(myLeerDetalles.DataSetClase, false, false);                
            } 
        }
        #endregion Buscar Folio

        #region Buscar Cliente
        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtCliente.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Clientes(txtCliente.Text.Trim(), "txtCte_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosCliente();
                }
                else
                {
                    txtCliente.Text = "";
                    txtCliente.Focus();
                }
            }            
        }
        private void CargarDatosCliente()
        {
            txtCliente.Text = myLeer.Campo("IdCliente");
            lblCte.Text = myLeer.Campo("Nombre");

            if (myLeer.Campo("Status") == "C")
            {
                General.msjUser("El Cliente seleccionado se encuentra cancelado. Verifique");
                txtCliente.Text = "";
                lblCte.Text = "";
            }
        }
        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayudas.Clientes("txtCte_KeyDown");

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosCliente();
                }
            }
        }
        #endregion Buscar Cliente

        #region Buscar SubCliente
        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtSubCliente.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.SubClientes(txtCliente.Text.Trim(), txtSubCliente.Text.Trim(),"txtCte_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosSubCliente();
                }
                else
                {
                    txtSubCliente.Text = "";
                    txtSubCliente.Focus();
                }
            }
        }
        private void CargarDatosSubCliente()
        {
            txtSubCliente.Text = myLeer.Campo("IdSubCliente");
            lblSubCte.Text = myLeer.Campo("Nombre");

            if (myLeer.Campo("Status") == "C")
            {
                General.msjUser("El SubCliente seleccionado se encuentra cancelado. Verifique");
                txtSubCliente.Text = "";
                lblSubCte.Text = "";
            }
        }
        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayudas.SubClientes_Buscar("txtSubCte_KeyDown", txtCliente.Text.Trim());

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargarDatosSubCliente();
                }
            }
        }
        #endregion Buscar SubCliente 

        #region Funciones
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnImportarConfiguraciones.Enabled = Guardar; 
        }

        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(Consultas.Estados("CargarEstados"), true, "IdEstado", "Nombre");
            cboEstados.SelectedIndex = 0;
        }

        private string FormatearValorNumerico(string Valor)
        {
            string sRegresa = Valor;

            sRegresa = sRegresa.Replace(" ", "");
            sRegresa = sRegresa.Replace(",", "");

            return sRegresa;
        }

        private bool GuardarEncabezado()
        {
            bool bRegresa = true;
            int iOpcion = 1;
            int iTipoClasificacionSSA = 1;
            int iTipoDeUnidades = rdoTipoUnidades_01_Ordinarias.Checked ? 1 : 2;


            if (rdoExclusivoControlados.Checked) iTipoClasificacionSSA = 1;
            if (rdoExclusivoLibres.Checked) iTipoClasificacionSSA = 2;
            if (rdoTodos.Checked) iTipoClasificacionSSA = 3;

            string sSql = string.Format("Exec spp_Mtto_FACT_Fuentes_De_Financiamiento \n" + // '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, {8}, '{9}'  ",
                "\t@IdFuenteFinanciamiento = '{0}', @IdEstado = '{1}', @IdCliente = '{2}', @IdSubCliente = '{3}', @NumeroDeContrato = '{4}', @NumeroDeLicitacion = '{5}', \n" +
                "\t@FechaInicial = '{6}', @FechaFinal = '{7}', @iMonto = '{8}', @iPiezas = '{9}', @iOpcion = '{10}', \n" + 
                "\t@TipoClasificacionSSA = '{11}', @Alias = '{12}', @EsParaExcedente = '{13}', \n" +
                "\t@EsDiferencial = '{14}', @TipoDeUnidades = '{15}' ", 
                txtFolio.Text.Trim(), cboEstados.Data, txtCliente.Text.Trim(), txtSubCliente.Text.Trim(), txtNumeroDeContrato.Text.Trim(), txtNumeroDeLicitacion.Text.Trim(), 
                dtpFechaInicial.Text.Trim(), dtpFechaFinal.Text.Trim(),
                FormatearValorNumerico(lblMonto.Text.Trim()), FormatearValorNumerico(lblPiezas.Text.Trim()), iOpcion, iTipoClasificacionSSA, txtAlias.Text.Trim(), 
                Convert.ToInt32(rdoTipoFF_02_Excedente.Checked), Convert.ToInt32(chkEsDiferencial.Checked), iTipoDeUnidades
                );

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


            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdFinanciamiento = myGrid.GetValue(i, (int)Cols.IdClave);
                sDescripcion = myGrid.GetValue(i, (int)Cols.Descripcion);
                sAlias = myGrid.GetValue(i, Cols.Alias); 
                dMonto = myGrid.GetValueDou(i, (int)Cols.Monto);
                iPiezas = myGrid.GetValueInt(i, (int)Cols.Piezas);
                iPrioridad = myGrid.GetValueInt(i, (int)Cols.Prioridad);
                bActivar = myGrid.GetValueBool(i, (int)Cols.Activar);


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
                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            lblCte.Text = "";
            txtSubCliente.Text = "";
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCte.Text = "";
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (cboEstados.Data == "0")
            {
                bRegresa = false;
                General.msjUser("Debe seleccionar un Estado, verifique.");
                cboEstados.Focus();
            }

            if (bRegresa && txtCliente.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Cliente inválida, verifique.");
                txtCliente.Focus();
            }

            if (bRegresa && txtSubCliente.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de SubCliente inválida, verifique.");
                txtSubCliente.Focus();
            }  

            if( bRegresa && ( dtpFechaInicial.Value > dtpFechaFinal.Value) )
            {
                bRegresa = false;
                General.msjUser("La Fecha Final no puede ser mayor que la Fecha Inicial. Verifique");
                dtpFechaFinal.Focus();
            }

            if (bRegresa && txtNumeroDeContrato.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Número de Contrato, verifique.");
                txtNumeroDeContrato.Focus();
            }

            ////if (bRegresa && txtNumeroDeLicitacion.Text.Trim() == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha capturado el Número de Licitación, verifique.");
            ////    txtNumeroDeLicitacion.Focus();
            ////}


            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }



            return bRegresa;
        }

        private void TotalizarCantidades()
        {
            double dMonto = 0;
            int iPiezas = 0;

            //Se totaliza el Monto
            dMonto = Math.Round(myGrid.TotalizarColumnaDou((int)Cols.Monto), 4, MidpointRounding.ToEven);
            lblMonto.Text = dMonto.ToString(sFormatoMonto);

            //Se Totalizan las Piezas
            iPiezas = myGrid.TotalizarColumna((int)Cols.Piezas);
            lblPiezas.Text = iPiezas.ToString(sFormatoPiezas);
        }

        #endregion Funciones         

        #region Grid 
        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                if (myGrid.GetValue(i, (int)Cols.Descripcion).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                {
                    myGrid.DeleteRow(i);
                }
            }

            if (myGrid.Rows == 0) // Si No existen renglones, se inserta 1.
            {
                myGrid.AddRow();
                myGrid.SetValue(1, (int)Cols.IdClave, "*");
            }
        }

        private void grdClaves_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (lblCancelado.Visible == false)
            {
                ////if (rdoExclusivoControlados.Checked || rdoExclusivoLibres.Checked)
                {
                    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                    {
                        if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Descripcion) != "" && myGrid.GetValueDou(myGrid.ActiveRow, (int)Cols.Monto) != 0 && myGrid.GetValueDou(myGrid.ActiveRow, (int)Cols.Piezas) != 0)
                        {
                            myGrid.Rows = myGrid.Rows + 1;
                            myGrid.SetValue(myGrid.Rows, (int)Cols.IdClave, "*");
                            myGrid.ActiveRow = myGrid.Rows;
                            myGrid.SetActiveCell(myGrid.Rows, (int)Cols.Descripcion);
                        }
                    }
                }
            }            
        }

        private void grdClaves_EditModeOff(object sender, EventArgs e)
        {
            TotalizarCantidades();
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
                // Se verifica el Monto
                if (myGrid.GetValue(1, (int)Cols.Descripcion) == "")
                {
                    bRegresa = false;
                }
                else
                {
                    for (int i = 1; i <= myGrid.Rows; i++)
                    {
                        if (myGrid.GetValue(i, (int)Cols.Descripcion) != "" && myGrid.GetValueInt(i, (int)Cols.Monto) == 0)
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }

                // Se verifican las Piezas.
                if (bRegresa)
                {
                    if (myGrid.GetValue(1, (int)Cols.Descripcion) == "")
                    {
                        bRegresa = false;
                    }
                    else
                    {
                        for (int i = 1; i <= myGrid.Rows; i++)
                        {
                            if (myGrid.GetValue(i, (int)Cols.Descripcion) != "" && myGrid.GetValueInt(i, (int)Cols.Piezas) == 0)
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
                General.msjUser("Debe capturar al menos un Financiamiento \n y/o capturar monto y cantidades para al menos un Financiamiento, verifique.");
            }

            return bRegresa;

        }

        private void grdClaves_KeyDown(object sender, KeyEventArgs e)
        {
            int iRenglon = myGrid.ActiveRow;
            int iColumna = myGrid.ActiveCol;

            if (!bCapturaDeClavesHabilitada)
            {
                General.msjUser("No ha especificado el manejo de controlados para la Fuente de Financiamiento."); 
            }

            // Todo el renglon invoca la pantalla 
            if (bCapturaDeClavesHabilitada &&  e.KeyCode == Keys.F5)
            {
                if (myGrid.GetValueBool(iRenglon, (int)Cols.Guardado))
                {
                    Claves = new FrmFuentesDeFinaciamiento_Claves();
                    Claves.IdEstado = cboEstados.Data;
                    Claves.IdCliente = txtCliente.Text;
                    Claves.IdSubCliente = txtSubCliente.Text; 

                    Claves.FuenteFinanciamiento = txtFolio.Text.Trim();
                    Claves.IdFinanciamiento = myGrid.GetValue(iRenglon, (int)Cols.IdClave);
                    Claves.Financiamiento = myGrid.GetValue(iRenglon, (int)Cols.Descripcion);
                    Claves.ValidarPoliza = myGrid.GetValueBool(iRenglon, (int)Cols.ValidarPoliza); 
                    Claves.LongitudMinimaPoliza = myGrid.GetValueInt(iRenglon, (int)Cols.LargoMinimo);
                    Claves.LongitudMaximaPoliza = myGrid.GetValueInt(iRenglon, (int)Cols.LargoMaximo);
                    Claves.TipoClasificacionSSA = iTipoClasificacionSSA; 
                    Claves.MostrarDetalle();

                    myGrid.SetValue(iRenglon, (int)Cols.ValidarPoliza, Claves.ValidarPoliza);
                    myGrid.SetValue(iRenglon, (int)Cols.LargoMinimo, Claves.LongitudMinimaPoliza);
                    myGrid.SetValue(iRenglon, (int)Cols.LargoMaximo, Claves.LongitudMaximaPoliza); 
                }
            }


            if (e.KeyCode == Keys.Delete)
            {
                if (iColumna == (int)Cols.IdClave)
                {
                    if (!myGrid.GetValueBool(iRenglon, (int)Cols.Guardado))
                    {
                        try
                        {
                            myGrid.DeleteRow(iRenglon);
                            TotalizarCantidades();
                        }
                        catch { }

                        if (myGrid.Rows == 0)
                        {
                            myGrid.Limpiar(true);
                            myGrid.SetValue(1, (int)Cols.IdClave, "*");
                        }
                    }
                }
            }
        }
        #endregion Grid

        #region Manejo de Clasificaciones SSA  
        private void Cambiar_Status(int Tipo, object sender, EventArgs e)
        {
            if (!bRevisandoSeleccion)
            {
                bRevisandoSeleccion = true; 
                Cambiar_Status(Tipo);
                bRevisandoSeleccion = false; 
            }
        }

        private void Cambiar_Status(int Tipo)
        {
            bool bRegresa = false;
            string sMsj = ""; 

            if (Tipo == 1)
            {
                sMsj = "La Fuente de finaciamiento se creara para uso exclusivo de claves de controlados, ";
                sMsj += "una vez generado el registro no será posible modificar este dato. \n\n";
                sMsj += "¿ Desea continuar ?";

                if (General.msjConfirmar(sMsj, "Confirmar :  Exclusivo controlados") == DialogResult.Yes)
                {
                    bRegresa = true;
                    rdoExclusivoLibres.Enabled = false;
                    rdoExclusivoLibres.Checked = false;

                    rdoExclusivoControlados.Enabled = true;
                    rdoExclusivoControlados.Checked = true;

                    rdoTodos.Enabled = false;
                    rdoTodos.Checked = false; 
                }
            }

            if (Tipo == 2)
            {
                sMsj = "La Fuente de finaciamiento se creara para uso exclusivo de claves libres, ";
                sMsj += "una vez generado el registro no será posible modificar este dato. \n\n";
                sMsj += "¿ Desea continuar ?";

                if (General.msjConfirmar(sMsj, "Confirmar :  Claves libres") == DialogResult.Yes)
                {
                    bRegresa = true;
                    rdoExclusivoLibres.Enabled = true;
                    rdoExclusivoLibres.Checked = true;

                    rdoExclusivoControlados.Enabled = false;
                    rdoExclusivoControlados.Checked = false;

                    rdoTodos.Enabled = false;
                    rdoTodos.Checked = false; 
                }
            }

            if (Tipo == 3)
            {
                sMsj = "La Fuente de finaciamiento se creara para uso de todo tipo de claves, ";
                sMsj += "una vez generado el registro no será posible modificar este dato. \n\n";
                sMsj += "¿ Desea continuar ?";

                if (General.msjConfirmar(sMsj, "Confirmar :  Todo tipo de claves") == DialogResult.Yes)
                {
                    bRegresa = true;
                    rdoExclusivoLibres.Enabled = false;
                    rdoExclusivoLibres.Checked = false;

                    rdoExclusivoControlados.Enabled = false;
                    rdoExclusivoControlados.Checked = false;

                    rdoTodos.Enabled = true;
                    rdoTodos.Checked = true;
                }
            }


            bCapturaDeClavesHabilitada = bRegresa;
            if (bRegresa)
            { 
                myGrid.AddRow();
                myGrid.SetValue(1, (int)Cols.IdClave, "*");
            }
            else 
            {
                rdoExclusivoLibres.Enabled = true;
                rdoExclusivoControlados.Enabled = true;
                rdoTodos.Enabled = true; 
                rdoExclusivoControlados.Checked = false;
                rdoExclusivoLibres.Checked = false;
                rdoTodos.Checked = false; 
            }
        }

        private void rdoExclusivoControlados_CheckedChanged(object sender, EventArgs e)
        {
            if (!bInicioDeModulo)
            {
                Cambiar_Status(1, sender, e);
            }
        }

        private void rdoExceptoControlados_CheckedChanged(object sender, EventArgs e)
        {
            if (!bInicioDeModulo)
            {
                Cambiar_Status(2, sender, e);
            }
        }

        private void rdoTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (!bInicioDeModulo)
            {
                Cambiar_Status(3, sender, e);
            }
        }
        #endregion Manejo de Clasificaciones SSA
    }
}
