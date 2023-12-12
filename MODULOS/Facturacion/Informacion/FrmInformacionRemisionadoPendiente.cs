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
using DllFarmaciaSoft.ExportarExcel;

namespace Facturacion.Informacion
{
    public partial class FrmInformacionRemisionadoPendiente : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leer2, leerExcel, leerExcelFact;
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;
        clsListView lst;
        clsExportarExcelPlantilla xpExcel;

        DataSet dtsExportarExcel = new DataSet();
        clsLeer leerExportarExcel = new clsLeer();

        Thread _workerThread;

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;

        clsAuditoria auditoria;

        private enum Cols
        {
            Ninguna = 0,
            Folio = 2, Factura = 3, Fecha = 4, TipoFactura = 5, Importe = 6, Status = 7, Insumo = 8
        }

        public FrmInformacionRemisionadoPendiente()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();


            CheckForIllegalCrossThreadCalls = false;

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.Puerto = General.DatosConexion.Puerto;
            cnn.DatosConexion.ForzarImplementarPuerto = General.DatosConexion.ForzarImplementarPuerto;
            cnn.DatosConexion.ConexionDeConfianza = General.DatosConexion.ConexionDeConfianza;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;


            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            leerExcel = new clsLeer(ref cnn);
            leerExcelFact = new clsLeer(ref cnn);

            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);
            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            lst = new clsListView(lstFacturas);
            lst.OrdenarColumnas = false;

            CargarTiposDeUnidades();

        }

        private void CargarTiposDeUnidades()
        {
            cboTiposDeUnidades.Clear();
            cboTiposDeUnidades.Add("0", "Todas las unidades");
            cboTiposDeUnidades.Add("1", "Farmacias");
            cboTiposDeUnidades.Add("2", "Farmacias de dosis unitaria");
            cboTiposDeUnidades.Add("3", "Almacenes");
            cboTiposDeUnidades.Add("4", "Almacenes de dosis unitaria");

            cboTiposDeUnidades.SelectedIndex = 0;
        }
        private void FrmInformacionRemisionadoPendiente_Load(object sender, EventArgs e)
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
            //Cargar_Folios_Facturas();
            //IniciarProcesamiento();

            if (validarDatos())
            {
                ObtenerInformacion();
            }
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            if (!chkOrigenVenta.Checked && !chkOrigenConsigna.Checked)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un origen de insumo, verifique.");
                chkOrigenConsigna.Focus();
            }

            return bRegresa; 
        }

        private void IniciarProcesamiento()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            IniciaToolBar(false, false);

            bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "ObteniendoDatos";
            _workerThread.Start();

            btnNuevo.Enabled = true;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            Generar_Excel();
        }

        private void FrameFechaDeProceso_Enter(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);

            IniciaToolBar(true, false);
            lst.Limpiar();

            tabControl_Resultado.TabPages.Clear();

        }

        private void ObtenerInformacion()
        {
            string sSql = "", sWhereFecha = "";
            bool bRegresa = true; 
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            IniciaToolBar(false, false);


            sSql = string.Format("Exec spp_FACT_RTP__ValidarPendienteRemisionar \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', \n" +  
                "\t@IdCliente = '{3}', @IdSubCliente = '{4}', \n" + 
                "\t@IdFuenteFinanciamiento = '{5}', @IdFinanciamiento = '{6}', \n" + 
                "\t@TipoDeUnidad = '{7}', @IdFarmacia = '{8}', \n" + 
                "\t@Validar_Producto = '{9}', @Validar_Servicio= '{10}', \n" + 
                "\t@Validar_Venta = '{11}', @Validar_Consigna = '{12}', \n" + 
                "\t@FechaInicial = '{13}', @FechaFinal = '{14}' \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                txtCte.Text, txtSubCte.Text, txtIdFuenteFinanciamiento.Text.Trim(), "", 
                cboTiposDeUnidades.Data, txtIdFinanciamiento.Text.Trim(), 
                Convert.ToInt32(chk_01_Insumo.Checked), Convert.ToInt32(chk_02_Servicio.Checked), 
                Convert.ToInt32(chkOrigenVenta.Checked), Convert.ToInt32(chkOrigenConsigna.Checked), 
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value)  
                );


            if (!leer.Exec(sSql))
            {
                bRegresa = false; 
                bSeEncontroInformacion = false;
                Error.GrabarError(leer, "ObtenerInformacion()");
                General.msjError("Ocurrió un error al obtener la información.");
            }
            else
            {
                leerExportarExcel.DataSetClase = leer.DataSetClase;
                ActualizarListaDeObjetos(); 
                ////if (leer.Leer())
                ////{
                ////    lst.CargarDatos(leer.DataSetClase, true, true);
                ////    IniciaToolBar(true, true);
                ////    bSeEncontroInformacion = true;
                ////}
                ////else
                ////{
                ////    bSeEncontroInformacion = false;
                ////    General.msjAviso("No se encontro información.");
                ////}
            }


            bEjecutando = false;

            IniciaToolBar(true, bRegresa);
            this.Cursor = Cursors.Default;
        }

        private bool ActualizarListaDeObjetos()
        {
            bool bRegresa = true;
            int iPage = 0;
            clsLeer leerValidacion = new clsLeer();
            clsLeer leerResultado = new clsLeer();
            DataSet dtsResultados = new DataSet();
            clsLeer leerObjetos = new clsLeer();

            TabPage tbNuevo;
            ListView lstNuevo;
            clsListView lstNuevoLoad;


            leerExportarExcel.RenombrarTabla(1, "Resultados");
            leerResultado.DataTableClase = leerExportarExcel.Tabla(1);
            while (leerResultado.Leer())
            {
                leerExportarExcel.RenombrarTabla(leerResultado.CampoInt("Orden"), leerResultado.Campo("NombreTabla"));
            }

            leerObjetos.DataRowsClase = leerExportarExcel.Tabla("Resultados").Select(" EsGeneral = 1 ");

            dtsResultados = leerExportarExcel.DataSetClase;
            dtsResultados.Tables.Remove("Resultados");
            dtsExportarExcel = dtsResultados;

            //leer.DataSetClase = dtsResultados;

            //foreach (DataTable dt in leer.DataSetClase.Tables)
            //{
            //    //// Crear los objetos 
            //}

            tabControl_Resultado.TabPages.Clear();
            while (leerObjetos.Leer())
            {
                leerValidacion.DataTableClase = leerExportarExcel.Tabla(leerObjetos.Campo("NombreTabla"));

                tbNuevo = new TabPage();
                tbNuevo.Location = new System.Drawing.Point(4, 22);
                tbNuevo.Name = "tabPage_" + leerObjetos.Campo("NombreTabla");
                tbNuevo.Padding = new System.Windows.Forms.Padding(3);
                tbNuevo.Size = new System.Drawing.Size(1135, 338);
                tbNuevo.TabIndex = iPage;
                tbNuevo.Text = leerObjetos.Campo("NombreTabla");
                tbNuevo.UseVisualStyleBackColor = true;


                lstNuevo = new ListView();
                lstNuevo.Location = new System.Drawing.Point(795, 7);
                lstNuevo.Name = "lst_rpt_" + leerObjetos.Campo("NombreTabla");
                lstNuevo.Size = new System.Drawing.Size(76, 18);
                lstNuevo.TabIndex = iPage;
                lstNuevo.UseCompatibleStateImageBehavior = false;
                lstNuevo.View = System.Windows.Forms.View.Details;
                lstNuevo.Dock = DockStyle.Fill;
                lstNuevo.Visible = true;


                lstNuevoLoad = new clsListView(lstNuevo);
                lstNuevoLoad.CargarDatos(leerValidacion.DataSetClase, true, true);


                tbNuevo.Controls.Add(lstNuevo); 
                tabControl_Resultado.Controls.Add(tbNuevo);
                iPage++;

            }



            return bRegresa;
        }

        private void IniciaToolBar(bool Ejecutar, bool Exportar)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnExportar.Enabled = Exportar;
        }
        #endregion Funciones

        #region Cliente -- Sub-Cliente
        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            lblCliente.Text = "";
            txtSubCte.Text = "";
        }

        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Clientes(txtCte.Text, "txtCliente_Validating");
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
                else
                {
                    General.msjUser("Clave de Cliente no encontrada.");
                    txtCte.Text = "";
                    lblCliente.Text = "";
                    txtCte.Focus();
                }
            }
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                leer.DataSetClase = Ayudas.Clientes("txtCte_KeyDown");
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
            }
        }

        private void CargarDatosCliente()
        {
            //txtCte.Enabled = false;
            txtCte.Text = leer.Campo("IdCliente");
            lblCliente.Text = leer.Campo("NombreCliente");
            //lblCte.Text = leer.Campo("Nombre");
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCliente.Text = "";
        }

        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCte.Text != "")
            {
                leer.DataSetClase = Consultas.SubClientes(txtCte.Text, txtSubCte.Text, "txtCte_Validating");
                if (leer.Leer())
                {
                    CargarDatosSubCliente();
                }
                else
                {
                    General.msjUser("Clave de Sub-Cliente no encontrada");
                    txtSubCte.Text = "";
                    lblSubCliente.Text = "";
                    txtSubCte.Focus();
                }
            }
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "")
                {
                    leer.DataSetClase = Ayudas.SubClientes("txtSubCte_KeyDown", txtCte.Text);
                    if (leer.Leer())
                    {
                        CargarDatosSubCliente();
                    }
                }
            }
        }

        private void CargarDatosSubCliente()
        {
            //txtSubCte.Enabled = false;
            txtSubCte.Text = leer.Campo("IdSubCliente");
            lblSubCliente.Text = leer.Campo("NombreSubCliente");
        }
        #endregion Cliente -- Sub-Cliente

        #region Exportar Excel 
        private void Generar_Excel()
        {
            clsGenerarExcel excel = new clsGenerarExcel();
            string sNombreDocumento = "PENDIENTE DE REMISIONAR";
            string sNombreHoja = "EXISTENCIAS";
            string sConcepto = "REPORTE DE EXISTENCIAS";

            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;

            clsLeer exportarExcel = new clsLeer();
            clsLeer dtsLocal = new clsLeer();

            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
            //int iHoja = 1;
            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            excel = new clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;

            if (excel.PrepararPlantilla(sNombreDocumento))
            {
                exportarExcel.DataTableClase = leerExportarExcel.Tabla("Resultados");
                while ( exportarExcel.Leer() )
                {

                    sNombreHoja = exportarExcel.Campo("NombreTabla");
                    dtsLocal.DataTableClase = leerExportarExcel.Tabla(sNombreHoja); 

                    excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                    excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                    excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, "REPORTE DE DISPENSACIÓN POR REMISIONAR");
                    excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 14, sNombreHoja);
                    excel.EscribirCeldaEncabezado(sNombreHoja, 5, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                    iRenglon = 8;
                    //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                    excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, dtsLocal.DataSetClase);

                    //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                    excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);
                }


                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }
        }
        #endregion Exportar Excel 

        #region Buscar Rubro
        private void txtIdFuenteFinanciamiento_TextChanged(object sender, EventArgs e)
        {
            lblFuenteFinanciamiento.Text = "";
            txtIdFinanciamiento.Text = "";
            lblFinanciamiento.Text = "";
            ////lblIdCliente.Text = "";
            ////lblCliente.Text = "";
            ////lblIdSubCliente.Text = "";
            ////lblSubCliente.Text = "";
        }

        private void txtIdFuenteFinanciamiento_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";
            leer = new clsLeer(ref cnn);

            if (txtIdFuenteFinanciamiento.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Encabezado(txtIdFuenteFinanciamiento.Text.Trim(), "txtIdFuenteFinanciamiento_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (leer.Leer())
                {
                    CargarDatosRubro();
                    ////ObtenerMontoPorAplicar();
                }
                else
                {
                    txtIdFuenteFinanciamiento.Text = "";
                    lblFuenteFinanciamiento.Text = "";
                    txtIdFuenteFinanciamiento.Focus();
                }
            }
        }

        private void CargarDatosRubro()
        {
            txtIdFuenteFinanciamiento.Text = leer.Campo("IdFuenteFinanciamiento");
            lblFuenteFinanciamiento.Text = leer.Campo("Estado") + " -- " + leer.Campo("NumeroDeContrato");

            ////lblIdCliente.Text = leer.Campo("IdCliente");
            ////lblCliente.Text = leer.Campo("Cliente");
            ////lblIdSubCliente.Text = leer.Campo("IdSubCliente");
            ////lblSubCliente.Text = leer.Campo("SubCliente");

            ////sTipoDe_FuenteDeFinanciamiento = leer.Campo("EsParaExcedente_Descripcion");
            ////bEsDiferencial = leer.CampoBool("EsDiferencial");

            if (leer.Campo("Status") == "C")
            {
                General.msjUser("El Rubro seleccionado se encuentra cancelado. Verifique");
                txtIdFuenteFinanciamiento.Text = "";
                lblFuenteFinanciamiento.Text = "";
            }

            ////Obtener_ListaProgramasAtencion();
        }

        private void txtIdFuenteFinanciamiento_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Encabezado("txtPrograma_KeyDown");

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (leer.Leer())
                {
                    CargarDatosRubro();
                }
            }
        }
        #endregion Buscar Rubro

        #region Buscar Concepto
        private void txtIdFinanciamiento_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";
            leer = new clsLeer(ref cnn);

            if (txtIdFinanciamiento.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Detalle(txtIdFuenteFinanciamiento.Text.Trim(), txtIdFinanciamiento.Text.Trim(), "txtIdFuenteFinanciamiento_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (leer.Leer())
                {
                    CargarDatosConcepto();
                    ////ObtenerMontoPorAplicar();
                }
                else
                {
                    txtIdFinanciamiento.Text = "";
                    lblFinanciamiento.Text = "";
                    txtIdFinanciamiento.Focus();
                }
            }
        }

        private void CargarDatosConcepto()
        {
            txtIdFinanciamiento.Text = leer.Campo("IdFinanciamiento");
            lblFinanciamiento.Text = leer.Campo("Financiamiento");
            if (leer.Campo("Status") == "C")
            {
                General.msjUser("El Financiamiento seleccionado se encuentra cancelado. Verifique");
                txtIdFinanciamiento.Text = "";
                lblFinanciamiento.Text = "";
            }

            ////if(ValidarClaves_Financiamiento())
            ////{
            ////    btnEjecutar.Enabled = false;
            ////}
        }

        private void txtIdFinanciamiento_TextChanged(object sender, EventArgs e)
        {
            lblFinanciamiento.Text = "";
        }

        private void txtIdFinanciamiento_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Detalle("txtPrograma_KeyDown", txtIdFuenteFinanciamiento.Text.Trim());

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (leer.Leer())
                {
                    CargarDatosConcepto();
                }
            }
        }
        #endregion Buscar Concepto
    }
}
