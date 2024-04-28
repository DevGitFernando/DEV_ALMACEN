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

//using Dll_IMach4;
//using Dll_IMach4.Interface;
using SC_SolutionsSystem.ExportarDatos;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace Farmacia.Transferencias
{
    public partial class FrmTransferenciasEnTransito : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        DllFarmaciaSoft.clsConsultas Consultas;
        DllFarmaciaSoft.clsAyudas Ayudas;

        clsDatosCliente DatosCliente;
        clsListView lst;

        DataSet dtsFarmacias = new DataSet();

        //PuntoDeVenta IMachPtoVta = new PuntoDeVenta();
        //clsExportarExcelPlantilla xpExcel;
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;

        string sFolioTransf = "";
        string sMsjNoEncontrado = "";
        string sFormato = "###,###,###,###,##0";

        bool bMenu_Extra = false;
        public FrmTransferenciasEnTransito()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            
            leer = new clsLeer(ref cnn);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Ayudas = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);

            lst = new clsListView(lstFoliosTransf);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sPersonal);

            SetMenu(bMenu_Extra); 
        }

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                ////case Keys.F4:
                ////    VentasDispensacion.FrmPDD_01_Dispensacion f = new VentasDispensacion.FrmPDD_01_Dispensacion();
                ////    f.Show();
                ////    break;

                case Keys.F12:
                    if (DtGeneral.EsAdministrador && DtGeneral.EsEquipoDeDesarrollo)
                    {
                        bMenu_Extra = !bMenu_Extra;
                        SetMenu(bMenu_Extra); 
                    }
                    break;

                default:
                    break;
            }
        }

        private void FrmTransferenciasEnTransito_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift)
            {
                TeclasRapidas(e);
            }

            switch (e.KeyCode)
            {
                default:
                    // base.OnKeyDown(e);
                    break;
            }
        }

        private void FrmTransferenciasEnTransito_Load(object sender, EventArgs e)
        {
            LimpiaPantalla(); 
        }

        #region Botones
        private void SetMenu(bool Visible)
        {
            toolStripSeparator_02.Visible = Visible;
            toolStripSeparator_03.Visible = Visible;

            btnStatus_Integrada.Visible = Visible;
            btnStatus_Integrada_Masivo.Visible = Visible; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            BuscarTransferencias();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            btnExportarExcel.Enabled = false;

            btnAplicarTransfMasivo.Enabled = DtGeneral.EsAdministrador || DtGeneral.EsEquipoDeDesarrollo;
            btnAplicarTransfMasivo.Visible = btnAplicarTransfMasivo.Enabled;

            lblRemisiones.Text = "0";

            sFolioTransf = "";
            lst.Limpiar();
            CargarJurisdicciones();
            rdoTodas.Checked = true; 
        }

        private void BuscarTransferencias()
        {
            string sSql = "";
            string sSql_Aux = "";
            string sFiltro = " Where Recepcionada in ( 'SI', 'NO' ) ";
            String sFiltroInterno = "";

            btnExportarExcel.Enabled = false;

            if (rdoRecepcionadas.Checked)
            {
                sFiltro = " Where Recepcionada in ( 'SI' ) ";
            }

            if (rdoNoRecepcionadas.Checked)
            {
                sFiltro = " Where Recepcionada in ( 'NO' ) ";
            }

            if (cboJurisdicciones.Data != "*")
            {
                sFiltroInterno = string.Format(" And F.IdJurisdiccion = '{0}'", cboJurisdicciones.Data);

                if (cboFarmacias.Data != "*")
                {
                    sFiltroInterno += string.Format(" And T.IdFarmaciaRecibe = '{0}'", cboFarmacias.Data);
                }
            }

            sSql_Aux = string.Format(" IsNull((Select top 1 (case when TE.Status = 'I' Then 'SI' else 'NO' End) From TransferenciasEnvioEnc TE (Nolock) " +
                " Where TE.IdEmpresa = '{0}' and TE.IdEstadoEnvia = '{1}' and TE.IdFarmaciaEnvia = '{2}' " +
                     " and TE.FolioTransferencia = T.Folio ), 'NO') ", sEmpresa, sEstado, sFarmacia);
 
            sSql = string.Format(
                " Select * \n" +
                " From \n" + 
                " ( \n" +
                "       Select 'Folio Traspaso' = Folio, {3} as Recepcionada, 'Fecha Registro' = FechaReg, \n" +
                "       'Id Unidad Destino' = IdFarmaciaRecibe, \n" +
                "       'Unidad Destino' = FarmaciaRecibe \n" +
                "       From vw_TransferenciasEnc T (Nolock) \n" +
                "       Inner Join CatFarmacias F (NoLock) On (T.IdEstadoRecibe = F.IdEstado And T.IdFarmaciaRecibe = F.IdFarmacia) \n" +
                "       Where TipoTransferencia = 'TS' and T.IdEmpresa = '{0}' and T.IdEstado = '{1}' {5}\n" +
                "           and T.IdFarmacia = '{2}' and TransferenciaAplicada = 0 and T.Status <> 'C' \n" +
                " ) as T \n" + 
                " {4} \n" + 
                " Order by 1 ",
                sEmpresa, sEstado, sFarmacia, sSql_Aux, sFiltro, sFiltroInterno);

            lst.LimpiarItems();
            lblRemisiones.Text = lst.Registros.ToString(sFormato);
            Application.DoEvents();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "BuscarTransferencias()");
                General.msjError("Error en consulta de Traspasos.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("Sin Información de Traspasos.");
                }
                else
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    btnExportarExcel.Enabled = true;
                }
            }

            lblRemisiones.Text = lst.Registros.ToString(sFormato);
        }

        #region CargarCombos
        private void CargarJurisdicciones()
        {
            cboJurisdicciones.Clear();
            cboJurisdicciones.Add("*", "<<Seleccione>>");

            cboJurisdicciones.Add(Consultas.Jurisdicciones(DtGeneral.EstadoConectado, "CargarJurisdicciones"), true, "IdJurisdiccion", "NombreJurisdiccion");
            dtsFarmacias = Consultas.Farmacias(DtGeneral.EstadoConectado, "CargarFarmacias()");


            cboJurisdicciones.SelectedIndex = 0;

            cboFarmacias.Clear();
            cboFarmacias.Add("*", "<<Seleccione>>");
            cboFarmacias.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("*", "<<Seleccione>>");
            string sWhere = "";

            //if (rdoTransferencia.Checked)
            {
                sWhere = string.Format("And IdFarmacia <> '{0}'", DtGeneral.FarmaciaConectada);
            }
            string sFiltro = string.Format(" IdJurisdiccion = '{0}' {1}", cboJurisdicciones.Data, sWhere);

            if (cboJurisdicciones.SelectedIndex != 0)
            {
                cboFarmacias.Filtro = sFiltro;
                cboFarmacias.Add(dtsFarmacias, true, "IdFarmacia", "NombreFarmacia");
            }

            cboFarmacias.SelectedIndex = 0;
        }
        #endregion CargarCombos 
        #endregion Funciones

        #region Botones_Menu
        private void btnAplicarTransf_Click(object sender, EventArgs e)
        {
            bool bRegresa = true; 
            string sConfirmaMsj = "Se aplicara el Traspaso. No habra reversa una vez aplicado!";
            int iRenglon = lst.RenglonActivo;
            bool bEsProcesoMasivo = false;

            if (ValidaStatusIntegrada(bEsProcesoMasivo, iRenglon))
            {
                if (ValidaTransferenciaInterEstatal(bEsProcesoMasivo, iRenglon))
                {
                    if (General.msjConfirmar(sConfirmaMsj) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (DtGeneral.ConfirmacionConHuellas)
                        {
                            sMsjNoEncontrado = "Usuario sin permisos para aplicar Traspasos. Favor de verificar.";
                            ////bRegresa = opPermisosEspeciales.VerificarPermisos("APLICAR_TRANSFERENCIA", sMsjNoEncontrado);
                            bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("APLICAR_TRANSFERENCIA", sMsjNoEncontrado);
                        }

                        if (bRegresa)
                        {
                            AplicarTransferenciasSalida(bEsProcesoMasivo, iRenglon);
                        }
                    }
                } 
            }
        }

        private void btnAplicarTransfMasivo_Click( object sender, EventArgs e )
        {
            bool bRegresa = true;
            string sConfirmaMsj = "Se aplicara el Traspaso. No habra reversa una vez aplicado!";
            bool bEsProcesoMasivo = true;

            for(int i = 1; i <= lst.Registros; i++)
            {
                lst.ColorRowsTexto(i, Color.Black); 
            }

            for(int i = 1; i <= lst.Registros; i++)
            {
                if(ValidaStatusIntegrada(bEsProcesoMasivo, i))
                {
                    if(ValidaTransferenciaInterEstatal(bEsProcesoMasivo, i))
                    {
                        bRegresa = AplicarTransferenciasSalida(bEsProcesoMasivo, i);

                        lst.ColorRowsTexto(i, bRegresa ? Color.Blue: Color.Red);

                        Application.DoEvents();
                        //if(General.msjConfirmar(sConfirmaMsj) == System.Windows.Forms.DialogResult.Yes)
                        //{
                        //    if(DtGeneral.ConfirmacionConHuellas)
                        //    {
                        //        sMsjNoEncontrado = "El usuario no tiene permiso para aplicar una transferencia, verifique por favor.";
                        //        ////bRegresa = opPermisosEspeciales.VerificarPermisos("APLICAR_TRANSFERENCIA", sMsjNoEncontrado);
                        //        bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("APLICAR_TRANSFERENCIA", sMsjNoEncontrado);
                        //    }

                        //    if(bRegresa)
                        //    {
                        //        AplicarTransferenciasSalida();
                        //    }
                        //}
                    }
                }
            }

            ////Actualizar la lista de transferencias 
            BuscarTransferencias();
        }

        private void btnStatus_Integrada_Click(object sender, EventArgs e)
        {
            int iRenglon = lst.RenglonActivo;

            IntegrarTransferencia(iRenglon);
            BuscarTransferencias(); 
        }

        private void btnStatus_Integrada_Masivo_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= lst.Registros; i++)
            {
                IntegrarTransferencia(i); 
            }

            BuscarTransferencias(); 
        }

        private void IntegrarTransferencia(int Renglon)
        {
            sFolioTransf = lst.GetValue(Renglon, 1);

            string sSql = string.Format(
                "Update T Set Status = 'I' \n" +
                "From TransferenciasEnvioEnc T (NoLock)\n" +
                "Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia = '{2}' and FolioTransferencia = '{3}' \n",
                sEmpresa, sEstado, sFarmacia, sFolioTransf);

            bool bRegresa = leer.Exec(sSql);

        }
        #endregion Botones_Menu

        #region Aplicar_Transferencias
        private bool AplicarTransferenciasSalida( bool EsProcesoMasivo, int Renglon )
        {
            bool bExito = false;

            if (!cnn.Abrir())
            {
                Error.LogError(cnn.MensajeError);

                if(!EsProcesoMasivo)
                {
                    General.msjErrorAlAbrirConexion();
                }
            }
            else
            {
                cnn.IniciarTransaccion();

                if (GuardaMovtosTransferencias(!EsProcesoMasivo, Renglon))
                {
                    if (AfectarExistenciaEnTransito())
                    {
                        if (AfectarExistencia(true, false))
                        {
                            bExito = MarcarPedidoComoRegistrado();
                        }
                    }
                }

                if (!bExito)
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "AplicarTransferenciasSalida");

                    if(!EsProcesoMasivo)
                    {
                        General.msjError("Error al guardar información.");
                    }
                }
                else
                {
                    cnn.CompletarTransaccion();

                    if(!EsProcesoMasivo)
                    {
                        lst.EliminarRenglonSeleccionado();
                        ////IMach  // Enlazar el folio de inventario 
                        ////IMachPtoVta.TerminarSolicitud(sFolioTransf);
                        General.msjUser("Información guardada satisfactoriamente.");
                        //btnNuevo_Click(null, null);
                        ////btnEjecutar_Click(null, null);
                    }
                }

                cnn.Cerrar();
            }

            return bExito; 
        }

        private bool GuardaMovtosTransferencias(bool SolicitarHuella, int Renglon)
        {
            sFolioTransf = lst.GetValue(1);
            sFolioTransf = lst.GetValue(Renglon, 1);

            string sSql = string.Format("Exec spp_Mtto_TransferenciasAplicarMovtos \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioTransferencia = '{3}', @IdPersonalRegistra = '{4}' \n", 
                sEmpresa, sEstado, sFarmacia, sFolioTransf, sPersonal);

            bool bRegresa = leer.Exec(sSql);

            if (DtGeneral.ConfirmacionConHuellas && bRegresa && SolicitarHuella)
            {
                ////bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioTransf);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.GrabarPropietarioDeHuella(cnn, sFolioTransf);
            }

            return bRegresa;
        }

        private bool AfectarExistencia(bool Aplicar, bool AfectarCosto)
        {
            AfectarInventario Inv = AfectarInventario.Ninguno;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            if (Aplicar)
                Inv = AfectarInventario.Aplicar;

            if (AfectarCosto)
                Costo = AfectarCostoPromedio.Afectar;

            string sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}' ",
                sEmpresa, sEstado, sFarmacia, sFolioTransf, (int)Inv, (int)Costo);

            bool bRegresa = leer.Exec(sSql);
            return bRegresa;
        }
        #endregion Aplicar_Transferencias

        #region AfectarExistenciaEnTransito
        private bool AfectarExistenciaEnTransito()
        {
            string sSql = string.Format("Exec spp_INV_AplicaDesaplicaExistenciaTransito \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioTransferencia = '{3}', @TipoFactor = '{4}' \n",
                sEmpresa, sEstado, sFarmacia, sFolioTransf, 2);

            bool bRegresa = leer.Exec(sSql);
            return bRegresa;
        } 
        #endregion AfectarExistenciaEnTransito

        #region SURTIDO -- Terminar pedido 
        private bool MarcarPedidoComoRegistrado()
        {
            string sSql = string.Format(
                " Update Pedidos_Cedis_Enc_Surtido Set Status = 'R' " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioTransferenciaReferencia = '{3}' ",
                sEmpresa, sEstado, sFarmacia, sFolioTransf );

            bool bRegresa = leer.Exec(sSql); 

            return bRegresa;
        }
        #endregion SURTIDO -- Terminar pedido

        #region Validar
        private bool ValidaStatusIntegrada(bool EsProcesoMasivo, int Renglon)
        {
            bool bRegresa = false;
            string sSql = "", Status = "";

            sFolioTransf = lst.GetValue(1);
            sFolioTransf = lst.GetValue(Renglon, 1); 


            sSql = string.Format(" Select * From TransferenciasEnvioEnc (Nolock) Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia = '{2}' " + 
                                 " and FolioTransferencia = '{3}' ", sEmpresa, sEstado, sFarmacia, sFolioTransf);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidaStatusIntegrada()");

                if(!EsProcesoMasivo)
                {
                    General.msjError("Error al consultar estatus.");
                }
            }
            else
            {
                if (leer.Leer())
                {
                    Status = leer.Campo("Status");

                    if (Status == "I")
                    {
                        bRegresa = true;
                    }
                    else
                    {
                        if(!EsProcesoMasivo)
                        {
                            General.msjAviso("Traspaso no recepcionado en el destino. No se puede aplicar Traspaso.");
                        }
                    }
                }
            }

            //////if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            //////{
            //////    sMsjNoEncontrado = "El usuario no tiene permiso para aplicar una transferencia, verifique por favor.";
            //////    bRegresa = opPermisosEspeciales.VerificarPermisos("APLICAR_TRANSFERENCIA", sMsjNoEncontrado);
            //////}

            return bRegresa;
        }

        private bool ValidaTransferenciaInterEstatal( bool EsProcesoMasivo, int Renglon )
        {
            bool bRegresa = false;
            string sSql = "", sIdFarmaciaRecibe = "";

            sFolioTransf = lst.GetValue(1);
            sFolioTransf = lst.GetValue(Renglon, 1);

            sSql = string.Format(" Select IdEstadoRecibe From TransferenciasEnvioEnc (Nolock) Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia = '{2}' " +
                                 " and FolioTransferencia = '{3}' ", sEmpresa, sEstado, sFarmacia, sFolioTransf);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidaTransferenciaInterEstatal()");
                if(!EsProcesoMasivo)
                {
                    General.msjError("Error al validar el tipo de Traspaso.");
                }
            }
            else
            {
                if (leer.Leer())
                {
                    sIdFarmaciaRecibe = leer.Campo("IdEstadoRecibe");

                    bRegresa = true;
                    if (sIdFarmaciaRecibe != sEstado)
                    {
                        if (!GnFarmacia.Transferencias_Interestatales__Farmacias)
                        {
                            bRegresa = false;
                            if(!EsProcesoMasivo)
                            {
                                General.msjAviso(" Traspaso Inter-Estatal. Favor de consultarlo en la pantalla correspondiente.");
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }
        #endregion Validar

        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("*", "<<Seleccione>>");

            if (cboJurisdicciones.SelectedIndex != 0)
            {
                CargarFarmacias();
            }

            cboFarmacias.SelectedIndex = 0;
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_TransferenciasEnTransito.xls";
            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_TransferenciasEnTransito.xls", DatosCliente);

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            //{
            //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //    xpExcel.AgregarMarcaDeTiempo = false;

            //    if (xpExcel.PrepararPlantilla())
            //    {
            //        xpExcel.GeneraExcel();

            //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 1);
            //        xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 1);
            //        xpExcel.Agregar("Reporte de transferencias en transito", 4, 1);

            //        xpExcel.Agregar(General.FechaSistemaObtener(), 6, 2);

            //        leer.RegistroActual = 1;

            //        for (int iRow = 9; leer.Leer(); iRow++)
            //        {
            //            int Col = 1;
            //            xpExcel.Agregar(leer.Campo("Folio Transferencia"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Recepcionada"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Fecha Registro"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Núm. Farmacia Destino"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Farmacia Destino"), iRow, Col++);
            //        }
            //        xpExcel.CerrarDocumento();

            //        if (General.msjConfirmar("Exportación finalidaza, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //        {
            //            xpExcel.AbrirDocumentoGenerado();
            //        }
            //    }
            //}

            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRow = 0;
            string sNombreHoja = "";
            string sNombre = "Reporte: Traspasos en transito.";

            leer.RegistroActual = 1;


            int iColsEncabezado = iColBase + leer.Columnas.Length - 1;
            //iColsEncabezado = iRow + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla())
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 16, sNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 14, string.Format("Fecha generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);


                iRow = 8;
                generarExcel.InsertarTabla(sNombreHoja, iRow, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }
        }
    }
}
