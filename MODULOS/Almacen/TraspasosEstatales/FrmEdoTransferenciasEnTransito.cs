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
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace Almacen.Transferencias
{
    public partial class FrmEdoTransferenciasEnTransito : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        DllFarmaciaSoft.clsConsultas Consultas;
        DllFarmaciaSoft.clsAyudas Ayudas;

        clsDatosCliente DatosCliente;
        clsListView lst;

        DataSet dtsFarmacias = new DataSet();
        //clsExportarExcelPlantilla xpExcel;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;
        string sMsjNoEncontrado = "";
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        string sFolioTransf = "";
        bool bMenu_Extra = false;

        public FrmEdoTransferenciasEnTransito()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            
            leer = new clsLeer(ref cnn);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Ayudas = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sPersonal);

            lst = new clsListView(lstFoliosTransf);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;

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

        private void FrmEdoTransferenciasEnTransito_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmEdoTransferenciasEnTransito_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
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
            sFolioTransf = "";
            lst.Limpiar();
            CargarEstados();
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

            if (cboEstados.Data != "*")
            {
                sFiltroInterno = string.Format(" And F.IdEstado = '{0}'", cboEstados.Data);

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
                "       Select 'Folio Transferencia' = Folio, {3} as Recepcionada, 'Fecha Registro' = FechaReg, \n" +
                "       'Núm. Farmacia Destino' = IdFarmaciaRecibe, \n" +
                "       'Farmacia Destino' = FarmaciaRecibe \n" +
                "       From vw_TransferenciasEnc T (Nolock) \n" +
                "       Inner Join CatFarmacias F (NoLock) On (T.IdEstadoRecibe = F.IdEstado And T.IdFarmaciaRecibe = F.IdFarmacia) \n" +
                "       Where T.IdEstadoRecibe <> T.IdEstado And TipoTransferencia = 'TS' and T.IdEmpresa = '{0}' and T.IdEstado = '{1}' {5}\n" +
                "           and T.IdFarmacia = '{2}' and TransferenciaAplicada = 0 and T.Status <> 'C' \n" +
                " ) as T \n" + 
                " {4} \n" + 
                " Order by 1 ",
                sEmpresa, sEstado, sFarmacia, sSql_Aux, sFiltro, sFiltroInterno);

            lst.LimpiarItems();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "BuscarTransferencias()");
                General.msjError("Ocurrió un error al obtener las Transferencias sin Aplicar.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("No se encontro información de Transferencias sin aplicar.");
                }
                else
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    btnExportarExcel.Enabled = true;
                }
            }
        }

        #region CargarCombos
        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("*", "<<Seleccione>>");

            cboEstados.Add(Consultas.EstadosConFarmacias("CargarJurisdicciones"), true, "IdEstado", "Estado");
            dtsFarmacias = Consultas.Farmacias("CargarEstados()");
            cboEstados.SelectedIndex = 0;

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

            string sFiltro = string.Format(" IdEstado = '{0}' and EsAlmacen = 1  {1}", cboEstados.Data, sWhere);

            if (cboEstados.SelectedIndex != 0)
            {
                cboFarmacias.Filtro = sFiltro;
                cboFarmacias.Add(dtsFarmacias, true, "IdFarmacia", "NombreFarmacia");
            }

            cboFarmacias.SelectedIndex = 0;
        }
        #endregion CargarCombos 
        #endregion Funciones

        #region Botones_Menu
        private void SetMenu(bool Visible)
        {
            toolStripSeparator_02.Visible = Visible;
            toolStripSeparator_03.Visible = Visible;

            btnStatus_Integrada.Visible = Visible;
            btnStatus_Integrada_Masivo.Visible = Visible;
        }

        private void btnAplicarTransf_Click(object sender, EventArgs e)
        {
            string sConfirmaMsj = "Se Aplicaran los movimientos de la Transferencia de Salida, Si los Aplica, ya no habra reversa a este movimiento ?";
            if (ValidaStatusIntegrada())
            {
                if (General.msjConfirmar(sConfirmaMsj) == System.Windows.Forms.DialogResult.Yes)
                {
                    AplicarTransferenciasSalida();
                }
            }
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
        private void AplicarTransferenciasSalida()
        {
            bool bExito = false;

            if (!cnn.Abrir())
            { 
                Error.LogError(cnn.MensajeError);
                General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
            }
            else 
            {
                cnn.IniciarTransaccion();

                if (GuardaMovtosTransferencias())
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
                    General.msjError("Ocurrió un error al grabar la información.");
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información Guardada Satisfactoriamente..");
                    btnNuevo_Click(null, null);
                    btnEjecutar_Click(null, null);
                }

                cnn.Cerrar();
            } 
        }

        private bool GuardaMovtosTransferencias()
        {
            sFolioTransf = lst.GetValue(1);

            string sSql = string.Format("Exec spp_Mtto_TransferenciasAplicarMovtos \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioTransferencia = '{3}', @IdPersonalRegistra = '{4}' \n",
                sEmpresa, sEstado, sFarmacia, sFolioTransf, sPersonal);

            bool bRegresa = leer.Exec(sSql);

            if (DtGeneral.ConfirmacionConHuellas && bRegresa)
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

            string sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                sEmpresa, sEstado, sFarmacia, sFolioTransf, (int)Inv, (int)Costo);

            bool bRegresa = leer.Exec(sSql);
            return bRegresa;
        }
        #endregion Aplicar_Transferencias

        #region AfectarExistenciaEnTransito
        private bool AfectarExistenciaEnTransito()
        {
            string sSql = string.Format("Exec spp_INV_AplicaDesaplicaExistenciaTransito '{0}', '{1}', '{2}', '{3}', '{4}' ",
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

        #region Validar_Status_Integracion
        private bool ValidaStatusIntegrada()
        {
            bool bRegresa = false;
            string sSql = "", Status = "";

            sFolioTransf = lst.GetValue(1);

            sSql = string.Format(" Select * From TransferenciasEnvioEnc (Nolock) Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia = '{2}' " + 
                                 " and FolioTransferencia = '{3}' ", sEmpresa, sEstado, sFarmacia, sFolioTransf);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidaStatusIntegrada()");
                General.msjError("Ocurrió un error al buscar el status.");
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
                        General.msjAviso(" La Transferencia no ha sido integrada en el destino, No es posible realizar la Aplicacion de la Transferencia ");
                    }
                }
            }

            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "El usuario no tiene permiso para aplicar una transferencia, verifique por favor.";
                ////bRegresa = opPermisosEspeciales.VerificarPermisos("APLICAR_TRANSFERENCIA", sMsjNoEncontrado);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("APLICAR_TRANSFERENCIA", sMsjNoEncontrado);
            }

            return bRegresa;
        }
        #endregion Validar_Status_Integracion

        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("*", "<<Seleccione>>");

            if (cboEstados.SelectedIndex != 0)
            {
                CargarFarmacias();
            }

            cboFarmacias.SelectedIndex = 0;
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            GenerarReporteExcel();
        }

        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = "Reporte de transferencias en transito";
            string sNombreFile = "Reporte de transferencias en transito";

            //leer.DataSetClase = leerExportarExcel.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombreFile))
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
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
        /*
            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_TransferenciasEnTransito.xls";
            bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_TransferenciasEnTransito.xls", DatosCliente);

            if (!bRegresa)
            {
                this.Cursor = Cursors.Default;
                General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            }
            else
            {
                xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                xpExcel.AgregarMarcaDeTiempo = false;

                if (xpExcel.PrepararPlantilla())
                {
                    xpExcel.GeneraExcel();

                    xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 1);
                    xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 1);
                    xpExcel.Agregar("Reporte de transferencias en transito", 4, 1);

                    xpExcel.Agregar(General.FechaSistemaObtener(), 6, 2);

                    leer.RegistroActual = 1;

                    for (int iRow = 9; leer.Leer(); iRow++)
                    {
                        int Col = 1;
                        xpExcel.Agregar(leer.Campo("Folio Transferencia"), iRow, Col++);
                        xpExcel.Agregar(leer.Campo("Recepcionada"), iRow, Col++);
                        xpExcel.Agregar(leer.Campo("Fecha Registro"), iRow, Col++);
                        xpExcel.Agregar(leer.Campo("Núm. Farmacia Destino"), iRow, Col++);
                        xpExcel.Agregar(leer.Campo("Farmacia Destino"), iRow, Col++);
                    }
                    xpExcel.CerrarDocumento();

                    if (General.msjConfirmar("Exportación finalidaza, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }
                }
            }
        }
        */
    }
}
