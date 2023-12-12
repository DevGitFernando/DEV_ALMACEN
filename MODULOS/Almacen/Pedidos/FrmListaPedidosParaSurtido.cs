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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;

using ClosedXML.Excel;

namespace Almacen.Pedidos
{
    public partial class FrmListaPedidosParaSurtido : FrmBaseExt
    {
        ////enum Cols
        ////{
        ////    IdJurisdiccion = 1, Jurisdiccion = 1, 
        ////    IdFarmacia = 2, Farmacia = 3, FarmaciaSolicita = 4, Folio = 5, Surtimiento = 6, Fecha = 7, Status = 8, StatusDescripcion = 9  
        ////}

        enum Cols
        {
            Ninguno = 0,
            IdJurisdiccion = 1, Jurisdiccion, TipoDePedido, TipoDePedidoDesc,
            IdFarmacia, Farmacia, IdFarmaciaSolicita, FarmaciaSolicita, Folio, FechaEntrega, Surtimiento, Fecha, Status, StatusDescripcion
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente; 
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda; 
        clsListView lst; 

        DataSet dtsFarmacias = new DataSet();

        string sPermisos_Surtir_Excluir_Transito = "PED_SURTIR_PEDIDOS_EXCLUIR_TRANSITO";
        string sPermisos_GenerarSalida__Transferencia_Venta = "PED_GENERAR_SALIDA";

        bool bPermisos_Surtir_Excluir_Transito = false;
        bool bPermitirSurtido_Automatico = false;
        bool bPermiso_Terminar_Pedido = false;
        bool bPermisos_GenerarSalida__Transferencia_Venta = false;
        bool bAjustarColumnas = true;


        string sIdFarmacia = "";
        string sIdFarmaciaSolicita = "";
        string sFarmacia = ""; 
        string sFolioPedido = "";
        int iTipoDePedido = 0;
        int iAnchoPantalla = 0;

        TipoDePedidoElectronico TipoPedido = TipoDePedidoElectronico.Ninguno;

        string sIdCliente = "";
        string sIdSubCliente = "";

        public FrmListaPedidosParaSurtido()
        {
            InitializeComponent();

            //iAnchoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Width * 0.95);
            //this.Width = iAnchoPantalla;
            General.Pantalla.AjustarTamaño(this, 90, 80); 


            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmListaPedidosParaSurtido");

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name); 

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            lst = new clsListView(listvwPedidos);

            SolicitarPermisosUsuario(); 
        }

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            bool bHabilitar = true;
            ////// Valida si el usuario conectado tiene permiso sobre las opcione especiales  
            //bPermisos_Surtir_Excluir_Transito = DtGeneral.PermisosEspeciales.TienePermiso(sPermisos_Surtir_Excluir_Transito);
            bPermisos_Surtir_Excluir_Transito = true;
            ////// Valida si el usuario conectado tiene permiso sobre las opcione especiales  
            bPermiso_Terminar_Pedido = DtGeneral.PermisosEspeciales.TienePermiso("sPed");
            bPermisos_GenerarSalida__Transferencia_Venta = DtGeneral.PermisosEspeciales.TienePermiso(sPermisos_GenerarSalida__Transferencia_Venta);


            terminarPedidoToolStripMenuItem.Enabled = bPermiso_Terminar_Pedido;
            btnGenerarSalida.Enabled = bPermisos_GenerarSalida__Transferencia_Venta;

            if (DtGeneral.EsAdministrador)
            {
                bHabilitar = true;

                terminarPedidoToolStripMenuItem.Enabled = bHabilitar;
                btnGenerarSalida.Enabled = bHabilitar;
            }

        }
        #endregion Permisos de Usuario

        private void FrmListaPedidosParaSurtido_Load(object sender, EventArgs e)
        {
            CargarJurisdicciones();
            CargarStatusPedidos();
            CargarRutas();
            InicializarPantalla();
        }

        #region Botones 
        private void InicializarPantalla()
        {
            Fg.IniciaControles();
            lst.LimpiarItems();
            lst.Limpiar(); 

            chkFechas.Checked = true;
            cboJurisdicciones.Focus();
            bAjustarColumnas = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarListaDePedidos(); 
        }
        #endregion Botones

        #region CargarCombos 
        private void CargarJurisdicciones()
        {
            if (cboJurisdicciones.NumeroDeItems == 0)
            {
                cboJurisdicciones.Clear();
                cboJurisdicciones.Add("*", "<< Todas las jurisdicciones >>");

                cboJurisdicciones.Add(query.Jurisdicciones(DtGeneral.EstadoConectado, "CargarJurisdicciones"), true, "IdJurisdiccion", "NombreJurisdiccion"); 
                dtsFarmacias = query.Farmacias(DtGeneral.EstadoConectado, "CargarFarmacias()"); 
            }

            cboJurisdicciones.SelectedIndex = 0;

            cboFarmacias.Clear();
            cboFarmacias.Add("*", "<<Seleccione>>");
            cboFarmacias.SelectedIndex = 0; 
        }

        private void CargarRutas()
        {
            cboRuta.Clear();
            cboRuta.Add("*", "<< Todas las Rutas >>");
            cboRuta.Add("0000", "<< Sin Ruta Asignada >>");

            cboRuta.Add(query.Rutas(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "CargarRutas()"), true, "IdRuta", "Descripcion");

            cboRuta.SelectedIndex = 0;

        }

        private void CargarFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("*", "<<Seleccione>>");
            string sFiltro = string.Format(" IdJurisdiccion = '{0}' ", cboJurisdicciones.Data); 

            if ( cboJurisdicciones.SelectedIndex != 0 ) 
            {
                cboFarmacias.Filtro = sFiltro;
                cboFarmacias.Add(dtsFarmacias, true, "IdFarmacia", "NombreFarmacia"); 
            } 

            cboFarmacias.SelectedIndex = 0; 
        } 
        #endregion CargarCombos 

        #region Eventos 
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
        #endregion Eventos

        #region Funciones y Procedimientos Privados 
        private void CargarStatusPedidos()
        {
            cboStatusPedidos.Clear();
            cboStatusPedidos.Add("0", "Todo");
            cboStatusPedidos.Add("1", "Pendientes de surtir");
            cboStatusPedidos.Add("2", "En proceso de surtido"); 
        }

        //private void CargarFechaDistribucion()
        //{
        //    string sSql =
        //        "Select top 1 FechaGeneracion, (case when datediff(dd, FechaGeneracion, getdate()) > 1 then 0 else 1 end) as ActivarSurtidoAutomatico " + 
        //        "From FarmaciaProductos_ALM_Distribucion (NoLock) ";
        //    DateTime dtpFecha = DateTime.Now; 

        //    lblFechaDistribucion.Text = "";
        //    bPermitirSurtido_Automatico = false; 
        //    if (!leer.Exec(sSql)) 
        //    { 
        //        Error.GrabarError(leer, "CargarFechaDistribucion()"); 
        //    } 
        //    else 
        //    {
        //        if (leer.Leer())
        //        {
        //            dtpFecha = leer.CampoFecha("FechaGeneracion"); 
        //            lblFechaDistribucion.Text = leer.CampoFecha("FechaGeneracion").ToString();
        //            lblFechaDistribucion.Text = General.FechaHora(dtpFecha);
        //            bPermitirSurtido_Automatico = leer.CampoBool("ActivarSurtidoAutomatico");

        //            if (!bPermitirSurtido_Automatico)
        //            {
        //                lblFechaDistribucion.Text += "\nEspejo de existencia invalido."; 
        //            }
        //        }
        //    }

        //    btnSurtirPedido.Enabled = bPermitirSurtido_Automatico;
        //}

        private void CargarListaDePedidos()
        {
            string sSql = "";

            ////string.Format("Exec spp_Mtto_Pedidos_Cedis___ListaPedidosParaSurtido \n" +
            ////    " \t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdJurisdiccion = '{2}', @IdFarmacia = '{3}', \n" +
            ////    " \t@IdCliente = '{4}', @IdSubCliente = '{5}', @IdBeneficiario = '{6}', \n" +
            ////    " \t@Filtro_Folios = '{7}', @Folio_Inicial = '{8}', @Folio_Final = '{9}', \n" +
            ////    " \t@Filtro_Fechas = '{10}', @FechaInicial = '{11}', @FechaFinal = '{12}', \n",
            ////    " \t@Filtro_Fechas_Entrega = '{13}', @FechaInicial_Entrega = '{14}', @FechaFinal_Entrega = '{15}', @StatusDePedido = '{16}'  \n");


            sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis___ListaPedidosParaSurtido \n" +
                "@IdEmpresa = '{0}', @IdEstado = '{1}', @IdJurisdiccion = '{2}', @IdFarmacia = '{3}', " + 
                "@IdCliente = '{4}', @IdSubCliente = '{5}', @IdBeneficiario = '{6}', " + 
                "@Filtro_Folios = '{7}', @Folio_Inicial = '{8}', @Folio_Final = '{9}', " + 
                "@Filtro_Fechas = '{10}', @FechaInicial = '{11}', @FechaFinal = '{12}', " + 
                "@Filtro_Fechas_Entrega = '{13}', @FechaInicial_Entrega = '{14}', @FechaFinal_Entrega = '{15}', " + 
                "@StatusDePedido = '{16}', @IdRuta = '{17}' ", 

                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, cboJurisdicciones.Data, cboFarmacias.Data, 
                sIdCliente, sIdSubCliente, txtIdBenificiario.Text.Trim(), 
                Convert.ToInt32(chkFolios.Checked), txtFolioInicial.Text.Trim(), txtFolioFinal.Text.Trim(), 
                Convert.ToInt32(chkFechas.Checked), General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value),
                Convert.ToInt32(chkFiltro_FechaEntrega.Checked), General.FechaYMD(dtpFechaInicial_Entrega.Value), General.FechaYMD(dtpFechaFinal_Entrega.Value),
                cboStatusPedidos.Data, cboRuta.Data 

                ); 

            lst.LimpiarItems(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarListaDePedidos()");
                General.msjError("Ocurrió un error al obtener la lista de los pedidos."); 
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontró información con los criterios especificados."); 
                }
                else 
                {
                    lst.CargarDatos(leer.DataSetClase, bAjustarColumnas, true);

                    //bAjustarColumnas = false;
                }
            }
        }
        #endregion Funciones y Procedimientos Privados 

        #region Beneficiarios 
        private void txtIdBenificiario_TextChanged( object sender, EventArgs e )
        {
            //txtIdBenificiario.Text = "";
            lblNombre.Text = "";
            //sIdCliente = "";
            //sIdSubCliente = ""; 
        }

        private void txtIdBenificiario_Validating( object sender, CancelEventArgs e )
        {
            if(txtIdBenificiario.Text.Trim() != "")
            {
                leer.DataSetClase = query.Beneficiarios(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtIdBenificiario.Text, "txtIdBenificiario_Validating");
                if(leer.Leer())
                {
                    CargarDatosBenefiario();
                }
                else
                {
                    General.msjUser("Clave de Beneficiario no encontrada, verifique.");
                    txtIdBenificiario.Text = "";
                    lblNombre.Text = "";
                }
            }
        }

        private void txtIdBenificiario_KeyDown( object sender, KeyEventArgs e )
        {
            if(e.KeyCode == Keys.F1)
            {
                //helpBeneficiarios = new FrmHelpBeneficiarios();
                leer.DataSetClase = ayuda.Beneficiarios(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "txtIdBenificiario_KeyDown");
                if(leer.Leer())
                {
                    CargarDatosBenefiario();
                }
            }
        }


        private void CargarDatosBenefiario()
        {
            sIdCliente = leer.Campo("IdCliente");
            sIdSubCliente = leer.Campo("IdSubCliente");
            txtIdBenificiario.Text = leer.Campo("IdBeneficiario");
            lblNombre.Text = leer.Campo("NombreCompleto");
        }

        #endregion Beneficiarios 

        #region Menu 
        private void menuPedidos_Opened(object sender, EventArgs e)
        {
            btnSurtirPedido_DescontarEnTransito.Enabled = bPermisos_Surtir_Excluir_Transito;
        }
        
        private void GetValores()
        {
            sIdFarmacia = lst.GetValue((int)Cols.IdFarmacia);
            sIdFarmaciaSolicita = lst.GetValue((int)Cols.IdFarmaciaSolicita);
            sFarmacia = lst.GetValue((int)Cols.Farmacia);
            iTipoDePedido = lst.GetValueInt(lst.RenglonActivo, (int)Cols.TipoDePedido);
            TipoPedido = (TipoDePedidoElectronico)iTipoDePedido;


            sIdFarmacia = lst.LeerItem().Campo("IdFarmacia");
            sFarmacia = lst.LeerItem().Campo("Farmacia");
            sIdFarmaciaSolicita = lst.LeerItem().Campo("IdFarmaciaSolicita");
            iTipoDePedido = lst.LeerItem().CampoInt("TipoPedido");
            TipoPedido = (TipoDePedidoElectronico)iTipoDePedido;

            sFolioPedido = lst.LeerItem().Campo("Folio");

            //switch (iTipoDePedido)
            //{
            //    case "VENTA":
            //        TipoPedido = TipoDePedidoElectronico.Ventas;
            //        break;

            //    case "TRANSFERENCIA":
            //        TipoPedido = TipoDePedidoElectronico.Transferencias;
            //        break;

            //    case "TRANSFERENCIA INTERESTATAL":
            //        TipoPedido = TipoDePedidoElectronico.Transferencias_InterEstatales;
            //        break;

            //    case "SOCIO COMERCIAL":
            //        TipoPedido = TipoDePedidoElectronico.SociosComerciales;
            //        break;
            //}

            if (sFarmacia != lst.GetValue((int)Cols.FarmaciaSolicita))
            {
                sFarmacia += " -- " + lst.GetValue((int)Cols.FarmaciaSolicita);
            }

            sFolioPedido = lst.GetValue((int)Cols.Folio);  
        }

        private void btnSurtirPedido_Click(object sender, EventArgs e)
        {
            SurtirPedido(false); 
        }

        private void btnSurtirPedido_DescontarEnTransito_Click(object sender, EventArgs e)
        {
            SurtirPedido(true); 
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SurtirPedido(true, true);
        }

        private void SurtirPedido(bool DescontarEnTransito)
        {
            SurtirPedido(DescontarEnTransito, false);
        }

        private void SurtirPedido(bool DescontarEnTransito, bool EsManual)
        {
            string sStatus = lst.GetValue((int)Cols.Status);
            
            sStatus = lst.LeerItem().Campo("Status");
            GetValores();

            if (sStatus.ToUpper() == "F")
            {
                General.msjAviso("El pedido ya fue surtido por completo, no es posible generar un nuevo surtido.");
            }
            else
            {
                if (sFolioPedido != "")
                {
                    FrmCEDIS_SurtidoPedidos f = new FrmCEDIS_SurtidoPedidos(DescontarEnTransito, EsManual);
                    if (f.CargarPedidoNuevo(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia, sIdFarmaciaSolicita, sFolioPedido, TipoPedido))
                    {
                        if(chkActualizarAlCerrarSurtimientos.Checked)
                        {
                            CargarListaDePedidos();
                        }
                    }
                }
            }
        }

        private void btnListadoDeSurtidos_Click(object sender, EventArgs e)
        {
            GetValores(); 

            if (sFolioPedido != "")
            {
                FrmListaDeSurtidosPedido f = new FrmListaDeSurtidosPedido(sIdFarmacia, sFarmacia, sFolioPedido);
                f.ShowDialog();

                if(chkActualizarAlCerrarSurtimientos.Checked)
                {
                    CargarListaDePedidos();
                }
            }
        }

        private void btnImprimirPedido_Click(object sender, EventArgs e)
        {
            Imprimir(false); 
        }

        private void btnImprimirPedidoSurtido_Click(object sender, EventArgs e)
        {
            Imprimir(true); 
        }

        private void excedentesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool bRegresa = true;
            string sPrefijo = "@";
            GetValores();

            if (sFolioPedido != "")
            {
                datosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = DtGeneral.RutaReportes;

                myRpt.NombreReporte = "PtoVta_Pedidos_Cedis_CantidadesExcedentes";
                myRpt.Add(sPrefijo + "IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add(sPrefijo + "IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add(sPrefijo + "IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add(sPrefijo + "IdFarmaciaPedido", sIdFarmacia);
                myRpt.Add(sPrefijo + "FolioPedido", sFolioPedido);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, datosCliente);
                // bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, @"PRUEBA.pdf", FormatosExportacion.PortableDocFormat); 

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void Imprimir(bool MostrarSurtido)
        {
            bool bRegresa = true;
            string sPrefijo = ""; 
            GetValores();

            if (sFolioPedido != "")
            {
                datosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = DtGeneral.RutaReportes;

                myRpt.NombreReporte = "PtoVta_Pedidos_CEDIS";
                if (MostrarSurtido)
                {
                    myRpt.NombreReporte = "PtoVta_Pedidos_CEDIS__Surtido";
                    sPrefijo = "@";
                    myRpt.Add(sPrefijo + "FolioPedido", sFolioPedido);
                }
                else
                {
                    myRpt.Add("Folio", sFolioPedido); 
                }

                myRpt.Add(sPrefijo + "IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add(sPrefijo + "IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add(sPrefijo + "IdFarmacia", sIdFarmacia);



                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, datosCliente);
                // bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, @"PRUEBA.pdf", FormatosExportacion.PortableDocFormat); 

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        } 
        #endregion Menu  

        private void listvwPedidos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void terminarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if(General.msjConfirmar("Esta operación dara por terminado el Folio de pedido, ¿ Desea continuar ?") == DialogResult.Yes)
            {
                TerminarPedido();
            }
        }

        private void TerminarPedido()
        {
            GetValores(); 
            //sFolioPedido = lst.GetValue((int)Cols.Folio);

            string sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Enc_ModificarStatus @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioPedido = '{3}',  @Status = '{4}'",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada , sFolioPedido, "F");

            
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "terminarPedidoToolStripMenuItem_Click()");
                General.msjError("Ocurrió un error al intentar terminar el pedido.");
            }
            else
            {
                if(leer.Leer())
                {
                    General.msjUser(leer.Campo("Mensaje"));
                }
            }

            CargarListaDePedidos();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string sAño = "", sNombre = "Reporte de Pedidos", sNombreHoja = "Hoja1";

            int iRow = 2, iColBase = 2, iColsEncabezado = 0, iRenglon = 0;

            // bloqueo principal 
            Cursor.Current = Cursors.Default;

            clsGenerarExcel generarExcel = new clsGenerarExcel();

            leer.RegistroActual = 1;


            iColsEncabezado = iRow + leer.Columnas.Length - 1;
            iColsEncabezado = iRow + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = sNombre;
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombre))
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);


                iRenglon = 8;
                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }
        }

        private void btnGenerarSalida_Click(object sender, EventArgs e)
        {
            GetValores();

            FrmFoliosSurtidosAProcesar FoliosSurtido = new FrmFoliosSurtidosAProcesar();
            FoliosSurtido.ListaSurtidos(sFolioPedido, TipoPedido);
        }
    }
}
