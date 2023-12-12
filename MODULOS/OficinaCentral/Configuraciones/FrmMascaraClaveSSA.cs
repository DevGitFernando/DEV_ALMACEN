using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.OfficeOpenXml;
using SC_SolutionsSystem.OfficeOpenXml.Data;
using SC_SolutionsSystem.SQL;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using DllFarmaciaSoft.Inventario;

namespace OficinaCentral.Configuraciones
{
    public partial class FrmMascaraClaveSSA : FrmBaseExt
    {
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        clsLeer reader, reader2;

        //clsLeerExcel excel;
        clsLeerExcelOpenOficce excel;
        //clsExportarExcelPlantilla xpExcel;
        OpenFileDialog openExcel = new OpenFileDialog();
        Thread thLoadFile;

        string sNombreHoja = "CuadroMascaras".ToUpper();
        bool bExisteHoja = false;

        int iRegistrosHoja = 0;
        int iRegistrosProcesados = 0;

        string sTitulo = "";
        string sFile_In = "";
        string sFormato_INT = "###, ###, ###, ##0";
        int iFolioCargaMasiva = 0;

        bool bValidandoInformacion = false;
        bool bSeEncontraronIndicencias = false;
        bool bActivarProceso = false;
        bool bErrorAlValidar = false;
        bool bErrorAlCargarPlantilla = false;
        string sMensajeError_CargarPlantilla = "";

        public FrmMascaraClaveSSA()
        {
            InitializeComponent();
            con.SetConnectionString();

            leer = new clsLeer(ref con);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

            reader = new clsLeer(ref con);
            reader2 = new clsLeer(ref con);

            FrameProceso.Left = 12000;
            MostrarEnProceso(false, 0);
            //this.Width = 1206;

            // Permitir ordenar Columnas 

            //if (DtGeneral.EsAdministrador)
            {
                btnExportarExcel.Visible = DtGeneral.EsAdministrador;
                btnAbrir.Visible = DtGeneral.EsAdministrador;
                btnGuardarMasivo.Visible = DtGeneral.EsAdministrador;
            }
        }

        private void FrmMascaraClaveSSA_Load(object sender, EventArgs e)
        {
            LlenaEstados();
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void IniciarToolBar()
        {
            IniciarToolBar(true, true, false);
        }

        private void IniciarToolBar( bool Guardar, bool Cancelar, bool Ejecutar )
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            ////btnEjecutar.Enabled = Ejecutar;

            ////if(!bHabilitarGuardar)
            ////{
            ////    btnGuardar.Enabled = bHabilitarGuardar;
            ////    btnCancelar.Enabled = bHabilitarGuardar;
            ////}

            btnExportarExcel.Enabled = btnGuardar.Enabled;
            btnAbrir.Enabled = btnGuardar.Enabled;
            btnGuardarMasivo.Enabled = btnGuardar.Enabled;
        }

        private void LimpiarPantalla()
        {
            Fg.IniciaControles(this, true);

            lblClaveSSA.Visible = false;

            cboSubCliente.Clear();
            cboSubCliente.Add("0", "<< Seleccione >>");
            cboSubCliente.SelectedIndex = 0;

            lblCancelado.Text = "CANCELADO";
            lblCancelado.Visible = false;
            HabilitarCaptura(false);
            cboEdo.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Guardar(2);
        }

        private void btnExportarExcel_Click( object sender, EventArgs e )
        {
            string sSql = string.Format("Exec spp_PRCS_OCEN__Plantilla_CargaDePrecios  @IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @Tipo = '{3}' ",
                cboEdo.Data, cboCliente.Data, cboSubCliente.Data, 4);

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnExportarExcel_Click");
                General.msjError("Ocurrió un error al obtener la información para generar la plantilla de Carga de Cuadro Básico.");
            }
            else
            {
                Generar_Excel();
            }
        }

        private void Generar_Excel()
        {
            DllFarmaciaSoft.ExportarExcel.clsGenerarExcel excel = new DllFarmaciaSoft.ExportarExcel.clsGenerarExcel();
            string sNombreDocumento = string.Format("Plantilla_Mascaras___{0}", cboEdo.ItemActual.GetItem("Nombre"));
            string sNombreHoja = "Mascaras";
            string sConcepto = "";

            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;

            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
            //int iHoja = 1;
            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            excel = new DllFarmaciaSoft.ExportarExcel.clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;

            if(excel.PrepararPlantilla(sNombreDocumento))
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                iRenglon = 1;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }
        }

        private void btnAbrir_Click( object sender, EventArgs e )
        {
            if(validarParametros())
            {
                openExcel.Title = "Archivos de Cuadro de Mascaras";
                openExcel.Filter = "Archivos de Excel (*.xls;*.xlsx)| *.xls;*.xlsx";
                //openExcel.Filter = "Archivos de Texto (*.txt)| *.txt"; 
                openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
                openExcel.AddExtension = true;
                lblProcesados.Visible = false;

                // if (openExcel.FileName != "")
                if(openExcel.ShowDialog() == DialogResult.OK)
                {
                    sFile_In = openExcel.FileName;

                    BloqueaControles(true);
                    IniciarToolBar(false, false, false);

                    tmValidacion.Enabled = true;
                    tmValidacion.Interval = 1000;
                    tmValidacion.Start();


                    thLoadFile = new Thread(this.CargarArchivo);
                    thLoadFile.Name = "LeerArchivo";
                    thLoadFile.Start();
                }
            }
        }

        private void btnGuardarMasivo_Click( object sender, EventArgs e )
        {
            string sMensaje = "¿ Desea integrar la información de Cuadro Básico de Mascaras, este proceso no se podra deshacer ?";

            if(General.msjConfirmar(sMensaje) == System.Windows.Forms.DialogResult.Yes)
            {
                IntegrarInformacion();
            }
        }
        #endregion Botones

        #region Eventos

        private void txtIdClaveSSA_TextChanged(object sender, EventArgs e)
        {
            lblClaveSSA.Text = "";
            txtMascara.Text = "";
            lblDescripcion.Text = "";
            txtDescripcion.Text = "";
            txtDescripcionCorta.Text = "";
            lblCancelado.Visible = false;
        }

        private void txtIdClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdClaveSSA.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.ClavesSSA_Sales(txtIdClaveSSA.Text.Trim(), true, "txtIdClaveSSA_Validating()");
                if (leer.Leer())
                {
                    CargarInformacion(); 
                }
            }
        }

        private void txtIdClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.ClavesSSA_Sales("txtId_KeyDown");

                if (leer.Leer())
                {
                    CargarInformacion(); 
                }
            }
        }

        private void CargarInformacion()
        {
            txtIdClaveSSA.Enabled = false; 
            txtIdClaveSSA.Text = leer.Campo("ClaveSSA");
            lblClaveSSA.Text = leer.Campo("IdClaveSSA_Sal");
            lblDescripcion.Text = leer.Campo("DescripcionCortaClave");


            if (leer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
            }

            CargarMascara();
        }

        private void cboCliente_Validating(object sender, CancelEventArgs e)
        {
            if (cboCliente.Data != "0")
            {
                string sIdEdo = "";
                leer = new clsLeer(ref con);
                sIdEdo = cboEdo.Data;
                leer.DataSetClase = Consultas.ComboSubCliente(cboCliente.Data, "cboCliente_Validating");
                if (leer.Leer())
                {
                    LlenaComboSubCliente();
                }
            }
        }

        private void cboSubCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEdo.Data != "0")
            {
                if (cboSubCliente.Data != "0")
                {
                    HabilitarCaptura(true);

                    cboEdo.Enabled = false;
                    cboCliente.Enabled = false;
                    cboSubCliente.Enabled = false; 
                }
            }
        }

        private void cboEdo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEdo.Data != "0")
            {
                if (cboSubCliente.Data != "0")
                {
                    HabilitarCaptura(true);
                }
            }
        }

        #endregion Eventos

        #region Funciones y Procedimientos

        private void HabilitarCaptura(bool bValor)
        {
            txtIdClaveSSA.Enabled = bValor;
            txtMascara.Enabled = bValor;
            txtDescripcion.Enabled = bValor;
            txtDescripcionCorta.Enabled = bValor;
            txtPresentacion.Enabled = bValor; 

            btnGuardar.Enabled = bValor;
            btnCancelar.Enabled = bValor;
            btnExportarExcel.Enabled = bValor;
            btnAbrir.Enabled = bValor;
            btnGuardarMasivo.Enabled = bValor;

            btnNuevo_Clave.Enabled = bValor;
        }

        private void Guardar(int iOpcion)
        {
             string sMensaje = "";

            if (ValidaDatos())
            {
                if (!con.Abrir())
                {
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    con.IniciarTransaccion();

                    string sSql = String.Format("EXEC spp_Mtto_ClaveSSA_Mascara  \n" +
                        "\t@IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdClaveSSA = '{3}', @Mascara = '{4}', @iOpcion = '{5}', \n" +
                        "\t@Descripcion = '{6}', @DescripcionCorta = '{7}', @Presentacion = '{8}' \n",
                        cboEdo.Data, cboCliente.Data, cboSubCliente.Data, lblClaveSSA.Text, txtMascara.Text, iOpcion,
                        txtDescripcion.Text.Trim(), txtDescripcionCorta.Text.Trim(), txtPresentacion.Text.Trim() 
                        );

                    if (leer.Exec(sSql))
                    {
                        if (leer.Leer())
                        {
                            sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                        }

                        con.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        con.DeshacerTransaccion();
                        Error.GrabarError(leer, "Guardar()");
                        General.msjError("Ocurrió un error al guardar la información.");
                        //btnNuevo_Click(null, null);

                    }

                    con.Cerrar();
                }
            }
        }

        private bool LlenaEstados()
        {
            bool bRegresa = false;
            leer = new clsLeer(ref con);

            leer.DataSetClase = Consultas.ComboEstados("LlenaEstados");
            if (leer.Leer())
            {
                bRegresa = true;
                LlenaComboEstados();
                LlenaCliente();
            }
            else
            {
                this.Close();
            }
            return bRegresa;
        }

        private void LlenaComboEstados()
        {
            //Se hace de esta manera para la ayuda.
            cboEdo.Add("0", "<< Seleccione >>");
            cboEdo.Add(leer.DataSetClase, true, "IdEstado", "EstadoNombre");
            cboEdo.SelectedIndex = 0;

            cboCliente.Clear();
            cboCliente.Add("0", "<< Seleccione >>");
            cboCliente.SelectedIndex = 0;

            cboSubCliente.Clear();
            cboSubCliente.Add("0", "<< Seleccione >>");
            cboSubCliente.SelectedIndex = 0;

            txtIdClaveSSA.Text = "";
            lblClaveSSA.Text = "";
            txtMascara.Text = "";
        }

        private void LlenaCliente()
        {
            string sIdEdo = "";
            leer = new clsLeer(ref con);
            sIdEdo = cboEdo.Data;
            leer.DataSetClase = Consultas.ComboCliente("LlenaCliente");
            if (leer.Leer())
            {
                LlenaComboCliente();
            }
        }

        private void LlenaComboCliente()
        {
            //Se hace de esta manera para la ayuda.
            cboCliente.Clear();
            cboCliente.Add("0", "<< Seleccione >>");
            cboCliente.Add(leer.DataSetClase, true, "IdCliente", "NombreCliente");
            cboCliente.SelectedIndex = 0;

            cboSubCliente.Clear();
            cboSubCliente.Add("0", "<< Seleccione >>");
            cboSubCliente.SelectedIndex = 0;

            txtIdClaveSSA.Text = "";
            lblClaveSSA.Text = "";
            txtMascara.Text = "";
        }

        private void LlenaComboSubCliente()
        {
            //Se hace de esta manera para la ayuda.
            cboSubCliente.Clear();
            cboSubCliente.Add("0", "<< Seleccione >>");
            cboSubCliente.Add(leer.DataSetClase, true, "IdSubCliente", "NombreSubCliente");
            cboSubCliente.SelectedIndex = 0;

            txtIdClaveSSA.Text = "";
            lblClaveSSA.Text = "";
            txtMascara.Text = "";
            lblDescripcion.Text = "";
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (cboEdo.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado el Estado, verifique.");
                cboEdo.Focus();
            }

            if (bRegresa && cboCliente.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un cliente, verifique.");
                cboCliente.Focus();
            }

            if (bRegresa && cboSubCliente.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Sub-cliente, verifique.");
                cboSubCliente.Focus();
            }

            if (bRegresa && txtIdClaveSSA.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado una claveSSA, verifique.");
                txtIdClaveSSA.Focus();
            }

            if (bRegresa && txtMascara.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Mascara, verifique.");
                txtMascara.Focus();
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Descripción, verifique.");
                txtDescripcion.Focus();
            }

            if (bRegresa && txtDescripcionCorta.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Descripción corta, verifique.");
                txtDescripcionCorta.Focus();
            }

            if (bRegresa && txtPresentacion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Presentación, verifique.");
                txtPresentacion.Focus();
            }

            return bRegresa;
        }

        private void CargarMascara()
        {
            string sSql = string.Format(" Select * From vw_ClaveSSA_Mascara (NoLock)" +
                    "Where IdEstado = '{0}' And IdCliente = '{1}' And IdSubCliente = '{2}' And IdClaveSSA = '{3}' ",
                    cboEdo.Data, cboCliente.Data, cboSubCliente.Data, lblClaveSSA.Text);

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    txtMascara.Text = leer.Campo("Mascara");
                    txtDescripcion.Text = leer.Campo("DescripcionMascara");
                    txtDescripcionCorta.Text = leer.Campo("DescripcionCorta");
                    txtPresentacion.Text = leer.Campo("Presentacion");

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Visible = true;
                    }
                }
            }
            else
            {
                Error.GrabarError(leer, "txtIdClaveSSA_Validating()");
                General.msjError("Ocurrió un error al obtener la mascara la información.");
            }
        }

        #endregion Funciones y Procedimientos

        #region Procesamiento Masivo 
        private void BloqueaControles( bool Bloquear )
        {
            bool bBloquear = !Bloquear;

            cboEdo.Enabled = bBloquear;
            cboCliente.Enabled = bBloquear;
            cboSubCliente.Enabled = bBloquear;
        }

        private bool validarParametros()
        {
            bool bRegresa = true;

            if(bRegresa && cboCliente.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Cliente válido.");
                cboCliente.Focus();
            }

            if(bRegresa && cboSubCliente.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Sub-Cliente válido.");
                cboSubCliente.Focus();
            }

            return bRegresa;
        }

        private void MostrarEnProceso( bool Mostrar, int Proceso )
        {
            string sTituloProceso = "";

            if(Mostrar)
            {
                FrameProceso.Left = 316;
            }
            else
            {
                FrameProceso.Left = this.Width + 800;
            }

            if(Proceso == 1)
            {
                sTituloProceso = "Leyendo estructura del documento";
            }

            if(Proceso == 2)
            {
                sTituloProceso = "Leyendo información de hoja seleccionada";
            }

            if(Proceso == 3)
            {
                sTituloProceso = "Guardando información de hoja seleccionada";
            }

            if(Proceso == 4)
            {
                sTituloProceso = "Verificando información a integrar";
            }

            if(Proceso == 5)
            {
                sTituloProceso = "Integrando información ..... ";
            }

            FrameProceso.Text = sTituloProceso;

        }

        private void CargarArchivo()
        {
            string sHoja = "";
            bool bRegresa = false;

            bValidandoInformacion = true;
            bErrorAlCargarPlantilla = false;
            sMensajeError_CargarPlantilla = "";
            bErrorAlValidar = false;

            MostrarEnProceso(true, 1);
            //FrameResultado.Text = sTitulo;

            //excel = new clsLeerExcel(sFile_In); 
            excel = new clsLeerExcelOpenOficce(sFile_In);
            excel.GetEstructura();

            //lst.Limpiar();
            Thread.Sleep(1000);

            //bRegresa = excel.Hojas.Registros > 0;
            while(excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja").ToUpper();
                if(sHoja == sNombreHoja)
                {
                    bRegresa = true;
                    break;
                }
            }

            bExisteHoja = bRegresa;
            if(bRegresa)
            {
                bRegresa = LeerHoja();
            }

            MostrarEnProceso(false, 1);
            bValidandoInformacion = false;
            bActivarProceso = bRegresa;


            ////IniciaToolBar(true, true, true, false); 


            //////if (!bRegresa)
            //////{
            //////    IniciaToolBar(true, true, true, false); 
            //////}
            //////else
            //////{
            //////    IniciaToolBar(true, true, false, true); 
            //////    //General.msjUser("Información precargada");
            //////}

            //return bRegresa;
        }

        private bool LeerHoja()
        {
            bool bRegresa = false;

            //FrameResultado.Text = sTitulo;
            //lst.Limpiar();
            excel.LeerHoja(sNombreHoja);

            //FrameResultado.Text = sTitulo;
            if(excel.Leer())
            {
                bRegresa = true;
                iRegistrosHoja = excel.Registros;
                //FrameResultado.Text = string.Format("{0}: {1} registros ", sTitulo, iRegistrosHoja.ToString(sFormato));
                //lst.CargarDatos(excel.DataSetClase, true, true);
            }

            if(bRegresa)
            {
                bRegresa = false;
                if(validarColumnas())
                {
                    bRegresa = CargarInformacionDeHoja();
                }
            }

            return bRegresa;
        }

        private bool validarColumnas()
        {
            bool bRegresa = true;

            return bRegresa;  
        }

        private bool CargarInformacionDeHoja()
        {
            bool bRegresa = false;
            lblProcesados.Visible = true;


            clsBulkCopy bulk = new clsBulkCopy(General.DatosConexion);

            bulk.NotifyAfter = 500;
            bulk.RowsCopied += new clsBulkCopy.RowsCopiedEventHandler(bulk_RowsCopied);
            bulk.OnCompled += new clsBulkCopy.RowsCopiedEventHandler(bulk_Compled);
            bulk.OnError += new clsBulkCopy.RowsCopiedEventHandler(bulk_Error);

            bulk.ClearColumns();
            bulk.DestinationTableName = "CFG_ClavesSSA_Mascaras__CargaMasiva";
            
            bulk.AddColumn("IdEstado", "IdEstado");
            bulk.AddColumn("IdCliente", "IdCliente");
            bulk.AddColumn("IdSubCliente", "IdSubCliente");
            bulk.AddColumn("ClaveSSA", "ClaveSSA");
            bulk.AddColumn("Mascara", "Mascara");
            bulk.AddColumn("Descripcion", "Descripcion");
            bulk.AddColumn("DescripcionCorta", "DescripcionCorta");
            bulk.AddColumn("Presentacion", "Presentacion");
            bulk.AddColumn("Status", "Status");


            reader.Exec("Truncate table CFG_ClavesSSA_Mascaras__CargaMasiva ");
            bRegresa = bulk.WriteToServer(excel.DataSetClase); //, System.Data.SqlClient.SqlBulkCopyOptions.Default); 


            if(bRegresa)
            {
                bRegresa = validarInformacion();
            }
            else
            {
                bErrorAlCargarPlantilla = true;
                sMensajeError_CargarPlantilla = bulk.Error.Message;
            }

            return bRegresa;
        }

        private void bulk_RowsCopied( RowsCopiedEventArgs e )
        {
            lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Compled( RowsCopiedEventArgs e )
        {
            lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Error( RowsCopiedEventArgs e )
        {
            lblProcesados.Text = string.Format("Ocurrio un error al procesar {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
            Error.GrabarError(e.Error, "bulk_Error");
        }

        private bool validarInformacion()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_PRCS_OCEN__IntegracionDeMascaras_000_Validar_Datos  @IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}'   ",
                cboEdo.Data, cboCliente.Data, cboSubCliente.Data);

            if(!reader.Exec(sSql))
            {
                bErrorAlValidar = true;
                sMensajeError_CargarPlantilla = "Ocurrió un error al validar los datos de la plantilla.";
                Error.GrabarError(reader, "validarInformacion()");
            }
            else
            {
                bRegresa = ValidarInformacion_Entrada();
            }

            return bRegresa;
        }

        private bool ValidarInformacion_Entrada()
        {
            bool bRegresa = true;
            clsLeer leerValidacion = new clsLeer();
            clsLeer leerResultado = new clsLeer();
            DataSet dtsResultados = new DataSet();

            ////leer.RenombrarTabla(1, "IMSS no registrados");
            ////leer.RenombrarTabla(2, "Condiciones de pago");
            ////leer.RenombrarTabla(3, "Método de pago");
            ////leer.RenombrarTabla(4, "Cuenta de Pago");
            reader.RenombrarTabla(1, "Resultados");
            leerResultado.DataTableClase = reader.Tabla(1);
            while(leerResultado.Leer())
            {
                reader.RenombrarTabla(leerResultado.CampoInt("Orden"), leerResultado.Campo("NombreTabla"));
            }

            dtsResultados = reader.DataSetClase;
            dtsResultados.Tables.Remove("Resultados");
            reader.DataSetClase = dtsResultados;

            foreach(DataTable dt in reader.DataSetClase.Tables)
            {
                if(!bActivarProceso)
                {
                    leerValidacion.DataTableClase = dt.Copy();
                    bActivarProceso = leerValidacion.Registros > 0;
                }
            }

            bRegresa = !bActivarProceso;

            return bRegresa;
        }

        private void tmValidacion_Tick( object sender, EventArgs e )
        {
            tmValidacion.Stop();
            tmValidacion.Enabled = false;

            if(!bValidandoInformacion)
            {
                if(!bExisteHoja)
                {
                    General.msjAviso("El archivo cargado no contiene la hoja llamada CuadroMascaras.");
                }
                else
                {
                    if(bActivarProceso)
                    {
                        BloqueaControles(true);
                        IniciarToolBar(true, true, true);
                    }
                    else
                    {
                        BloqueaControles(false);
                        IniciarToolBar(true, true, false);
                        if(bErrorAlCargarPlantilla)
                        {
                            General.msjAviso(sMensajeError_CargarPlantilla);
                        }
                        else
                        {
                            if(!bErrorAlValidar)
                            {
                                FrmIncidencias f = new FrmIncidencias("Cuadro Básico Mascaras", reader.DataSetClase);
                                f.ShowDialog(this);
                            }
                            else
                            {
                                General.msjError(sMensajeError_CargarPlantilla);
                            }
                        }
                    }
                }
            }
            else
            {
                tmValidacion.Enabled = true;
                tmValidacion.Start();
            }
        }

        private void btnNuevo_Clave_Click( object sender, EventArgs e )
        {
            txtIdClaveSSA.Text = "";
            txtIdClaveSSA.Enabled = true;
            txtIdClaveSSA.Focus(); 
        }

        private bool IntegrarInformacion()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_PRCS_OCEN__IntegracionDeMascaras  @IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}' ",
                cboEdo.Data, cboCliente.Data , cboSubCliente.Data);

            btnGuardar.Enabled = false;

            if(!con.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                con.IniciarTransaccion();

                if(!reader.Exec(sSql))
                {
                    Error.GrabarError(reader, "IntegrarInformacion()");
                    btnGuardar.Enabled = true;
                }
                else
                {
                    bRegresa = true;
                }


                if(bRegresa)
                {
                    con.CompletarTransaccion();
                    General.msjUser("Cuadro Básico de Mascasras integrado satisfactoriamente.");
                    ////btnEjecutar_Click(null, null);
                }
                else
                {
                    con.DeshacerTransaccion();
                    Error.GrabarError(reader, "IntegrarInformacion()");
                    General.msjError("Ocurrió un error al integrar la información.");
                }

                con.Cerrar();
            }

            return bRegresa;
        }
        #endregion Procesamiento Masivo 
    }
}
