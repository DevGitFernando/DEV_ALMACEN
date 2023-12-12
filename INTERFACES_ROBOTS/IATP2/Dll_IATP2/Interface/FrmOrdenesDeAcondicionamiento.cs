using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using Dll_IATP2.Protocolos;

namespace Dll_IATP2.Interface
{
    public partial class FrmOrdenesDeAcondicionamiento : FrmBaseExt
    {
        enum Cols
        {
            Ninguna = 0, 
            IdPaciente = 1, NombrePaciente, NumeroDeCama, NombreDeQuienPreescribe 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);


        //clsConexionSQL cnn; //= new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consulta;
        clsConsultas_IATP2 Consulta_IATP2;
        clsAyudas Ayuda;

        clsGrid gridClaves;
        clsGrid gridProductos;
        clsListView lst; 
        DataSet dtsProductos = new DataSet();
        clsDatosCliente DatosCliente;
        clsInformacion_OrdenDeAcondicionamiento InfVtas;
        clsATP2_GenerarOrdenDeAcondicionamiento OrdenDeAcondicionamiento;

        DataSet dtsOrdenDeAcondicionamiento = new DataSet(); 

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFolioSolicitud = ""; 

        string sIdPublicoGral = GnFarmacia.PublicoGral;
        string sIdSeguroPopular = GnFarmacia.SeguroPopular;
        bool bEsSeguroPopular = false;

        bool bFolioGuardado = false; 
        string sFolio = "";
        string sFolioVenta = ""; 

        public FrmOrdenesDeAcondicionamiento()
        {
            InitializeComponent();

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);


            Consulta = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            Consulta_IATP2 = new clsConsultas_IATP2(General.DatosConexion, IATP2.DatosApp, this.Name); 

            lst = new clsListView(listView_Beneficiarios);
            lst.PermitirAjusteDeColumnas = DtGeneral.EsAdministrador; 
        }

        #region Form 
        private void FrmOrdenesDeAcondicionamiento_Load(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }
        #endregion Form

        #region Botones 
        private void InicializarPantalla()
        {
            Fg.IniciaControles();

            lst.LimpiarItems(); 
            IniciaToolBar(false, false);
            IniciaToolBar_Peticiones(false, false, false, false); 

            dtpFechaRegistro.Enabled = false; 

            txtFolio.Focus(); 
        }

        private void IniciaToolBar(bool Guardar, bool GenerarTXT)
        {
            btnGuardar.Enabled = Guardar;
            btnGenerarArchivoDeSalida.Enabled = GenerarTXT; 
        }

        private void IniciaToolBar_Peticiones(bool Agregar, bool Editar, bool View, bool Eliminar)
        {
            btnAdd.Enabled = Agregar;
            btnEdit.Enabled = Editar;
            btnView.Enabled = View; 
            btnDelete.Enabled = Eliminar; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                GuardarInformacion(); 
            }
        }

        private void btnGenerarArchivoDeSalida_Click(object sender, EventArgs e)
        {
            OrdenDeAcondicionamiento = new clsATP2_GenerarOrdenDeAcondicionamiento();

            if (OrdenDeAcondicionamiento.GenerarArchivo(dtsOrdenDeAcondicionamiento))
            {
                General.msjUser("Archivo de salida generado satisfactoriamente.");

                OrdenDeAcondicionamiento.AbrirDirectorio(); 
                
                InicializarPantalla(); 
            }
        }

        private void btnLeerCodigo_Click(object sender, EventArgs e)
        {
            FrmDecodificacionSNK f = new FrmDecodificacionSNK();

            f.ShowInTaskbar = false;
            f.ShowDialog(); 
        }
        #endregion Botones

        #region Botones detalles 
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int iItem = lst.RenglonActivo - 1;

            try
            {
                InfVtas = new clsInformacion_OrdenDeAcondicionamiento(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);                 
                InfVtas.Show("", txtCte.Text, lblCte.Text, txtSubCte.Text, lblSubCte.Text);

                if (InfVtas.InformacionGuardada)
                {
                    lst.AddRow();
                    iItem = lst.Registros - 1;
                    listView_Beneficiarios.Items[iItem].SubItems[0].Tag = InfVtas;

                    iItem++;
                    lst.SetValue(iItem, (int)Cols.IdPaciente, InfVtas.Beneficiario);
                    lst.SetValue(iItem, (int)Cols.NombrePaciente, InfVtas.Beneficiario_Nombre);
                    lst.SetValue(iItem, (int)Cols.NumeroDeCama, InfVtas.NumeroDeCama);
                    lst.SetValue(iItem, (int)Cols.NombreDeQuienPreescribe, InfVtas.Medico_Nombre);
                }
            }
            catch { }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            VisualizarInformacion(true);

            ////int iItem = lst.RenglonActivo -1; 
            
            ////try 
            ////{
            ////    InfVtas = (clsInformacion_OrdenDeAcondicionamiento)listView_Beneficiarios.Items[iItem].SubItems[0].Tag;
            ////    InfVtas.Show("", txtCte.Text, lblCte.Text, txtSubCte.Text, lblSubCte.Text);
            ////    listView_Beneficiarios.Items[iItem].SubItems[0].Tag = InfVtas;

            ////    if (InfVtas.InformacionGuardada)
            ////    {
            ////        iItem++;
            ////        lst.SetValue(iItem, (int)Cols.IdPaciente, InfVtas.Beneficiario);
            ////        lst.SetValue(iItem, (int)Cols.NombrePaciente, InfVtas.Beneficiario_Nombre);
            ////        lst.SetValue(iItem, (int)Cols.NumeroDeCama, InfVtas.NumeroDeCama);
            ////        lst.SetValue(iItem, (int)Cols.NombreDeQuienPreescribe, InfVtas.Medico_Nombre);
            ////    }

            ////}catch{}

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            VisualizarInformacion(false);
        }

        private void VisualizarInformacion(bool Editar)
        {
            int iItem = lst.RenglonActivo - 1;

            try
            {
                InfVtas = (clsInformacion_OrdenDeAcondicionamiento)listView_Beneficiarios.Items[iItem].SubItems[0].Tag;
                InfVtas.Show("", txtCte.Text, lblCte.Text, txtSubCte.Text, lblSubCte.Text);

                if (Editar && InfVtas.InformacionGuardada)
                {
                    listView_Beneficiarios.Items[iItem].SubItems[0].Tag = InfVtas;

                    iItem++;
                    lst.SetValue(iItem, (int)Cols.IdPaciente, InfVtas.Beneficiario);
                    lst.SetValue(iItem, (int)Cols.NombrePaciente, InfVtas.Beneficiario_Nombre);
                    lst.SetValue(iItem, (int)Cols.NumeroDeCama, InfVtas.NumeroDeCama);
                    lst.SetValue(iItem, (int)Cols.NombreDeQuienPreescribe, InfVtas.Medico_Nombre);
                }

            }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            lst.EliminarRenglonSeleccionado(); 
        }
        #endregion Botones detalles 

        #region Eventos
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                IniciaToolBar(true, false);
                txtFolio.Text = "*";
                txtFolio.Enabled = false;
                txtNumeroDeDocumento.Focus();
            }
            else
            {
                leer.DataSetClase = Consulta_IATP2.OrdenesDeProduccion(Fg.PonCeros(txtFolio.Text, 10), sEmpresa, sEstado, sFarmacia, "txtFolio_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Folio de Orden de Acondicionamiento no encontrado, verifique.");
                    txtFolio.Text = "";
                    txtFolio.Focus();
                }
                else
                {
                    bFolioGuardado = true;
                    IniciaToolBar(false, true);

                    CargarInformacion_OrdenAcondicionamiento();

                    IniciaToolBar_Peticiones(false, false, true, false); 
                }
            }
        }

        private void CargarInformacion_OrdenAcondicionamiento()
        {
            bool bExistenDatos = false;
            int iItem = 0; 
            clsLeer Datos_01_Encabezado = new clsLeer();
            clsLeer Datos_02_Beneficiarios = new clsLeer();
            clsLeer Datos_03_Productos = new clsLeer();
            clsLeer Datos_04_OrdenDeAcondicionamiento = new clsLeer();


            leer.RenombrarTabla(1, "Datos_01_Encabezado");
            leer.RenombrarTabla(2, "Datos_02_Beneficiarios");
            leer.RenombrarTabla(3, "Datos_03_Productos");
            leer.RenombrarTabla(4, "Datos_04_OrdenDeAcondicionamiento");
            dtsOrdenDeAcondicionamiento = leer.DataSetClase;            

            Datos_01_Encabezado.DataTableClase = leer.Tabla("Datos_01_Encabezado");
            Datos_02_Beneficiarios.DataTableClase = leer.Tabla("Datos_02_Beneficiarios");
            Datos_03_Productos.DataTableClase = leer.Tabla("Datos_03_Productos");
            Datos_04_OrdenDeAcondicionamiento.DataTableClase = leer.Tabla("Datos_04_OrdenDeAcondicionamiento");


            if (Datos_01_Encabezado.Leer())
            {
                bExistenDatos = true; 
                sFolio = Datos_01_Encabezado.Campo("FolioSolicitud");
                sFolioVenta = sFolio;
                txtFolio.Enabled = false; 
                txtFolio.Text = sFolio;

                txtNumeroDeDocumento.Enabled = false; 
                txtNumeroDeDocumento.Text = Datos_01_Encabezado.Campo("NumeroDeDocumento");
                txtObservaciones.Enabled = false; 
                txtObservaciones.Text = Datos_01_Encabezado.Campo("Observaciones");

                dtpFechaRegistro.Value = Datos_01_Encabezado.CampoFecha("FechaRegistro");

                txtCte.Enabled = false; 
                txtCte.Text = Datos_01_Encabezado.Campo("IdCliente");
                lblCte.Text = Datos_01_Encabezado.Campo("NombreCliente");

                txtSubCte.Enabled = false;
                txtSubCte.Text = Datos_01_Encabezado.Campo("IdSubCliente");
                lblSubCte.Text = Datos_01_Encabezado.Campo("NombreSubCliente");

                txtPro.Enabled = false; 
                txtPro.Text = Datos_01_Encabezado.Campo("IdPrograma");
                lblPro.Text = Datos_01_Encabezado.Campo("Programa");

                txtSubPro.Enabled = false;
                txtSubPro.Text = Datos_01_Encabezado.Campo("IdSubPrograma");
                lblSubPro.Text = Datos_01_Encabezado.Campo("SubPrograma");


                if (Datos_01_Encabezado.Campo("Status") == "C")
                {
                    lblCancelado.Visible = true;
                }
            }

            //// Detalles de Beneficirios 
            while (Datos_02_Beneficiarios.Leer())
            {
                InfVtas = new clsInformacion_OrdenDeAcondicionamiento(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
                InfVtas.CargarInformacion(Datos_02_Beneficiarios.RowActivo, Datos_03_Productos.DataSetClase);


                lst.AddRow();
                iItem = lst.Registros - 1;
                listView_Beneficiarios.Items[iItem].SubItems[0].Tag = InfVtas;

                iItem++;
                lst.SetValue(iItem, (int)Cols.IdPaciente, Datos_02_Beneficiarios.Campo("IdBeneficiario"));
                lst.SetValue(iItem, (int)Cols.NombrePaciente, Datos_02_Beneficiarios.Campo("NombreDeBeneficiario"));
                lst.SetValue(iItem, (int)Cols.NumeroDeCama, Datos_02_Beneficiarios.Campo("NumeroDeCama"));
                lst.SetValue(iItem, (int)Cols.NombreDeQuienPreescribe, Datos_02_Beneficiarios.Campo("NombrePreescribe"));



                //InfVtas.Show("", txtCte.Text, lblCte.Text, txtSubCte.Text, lblSubCte.Text);
            }

        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, "txtCte_KeyDown");
                if (leer.Leer())
                {
                    txtCte.Text = leer.Campo("IdCliente");
                    lblCte.Text = leer.Campo("NombreCliente");
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
                    leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, "txtSubCte_KeyDown_1");
                    if (leer.Leer())
                    {
                        txtSubCte.Text = leer.Campo("IdSubCliente");
                        lblSubCte.Text = leer.Campo("NombreSubCliente");
                        txtPro.Focus();
                    }
                }
            }
        }

        private void txtPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "")
                {
                    //leer2.DataSetClase = Ayuda.Programas("txtPro_KeyDown");
                    leer.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, "txtPro_KeyDown");
                    if (leer.Leer())
                    {
                        txtPro.Text = leer.Campo("IdPrograma");
                        lblPro.Text = leer.Campo("Programa");
                        txtSubPro.Focus();
                    }
                }
            }
        }

        private void txtSubPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "")
                {
                    leer.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtPro.Text, "txtPro_KeyDown");
                    if (leer.Leer())
                    {
                        txtSubPro.Text = leer.Campo("IdSubPrograma");
                        lblSubPro.Text = leer.Campo("SubPrograma");
                    }
                }
            }
        }

        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() == "")
            {
                txtCte.Text = "";
                lblCte.Text = "";
                txtSubCte.Text = "";
                lblSubCte.Text = "";
                txtPro.Text = "";
                lblPro.Text = "";
                txtSubPro.Text = "";
                lblSubCte.Text = "";
            }
            else
            {
                if (Fg.PonCeros(txtCte.Text, 4) == sIdPublicoGral)
                {
                    General.msjAviso("El Cliente Público General es exclusivo de Venta a Contado, no puede ser utilizado en Venta a Crédito");
                    txtCte.Text = "";
                    lblCte.Text = "";
                    e.Cancel = true;
                }
                else
                {
                    leer.DataSetClase = Consulta.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, "txtCte_Validating");
                    if (!leer.Leer())
                    {
                        General.msjUser("Clave de Cliente no encontrada, ó el Cliente no pertenece a la Farmacia.");
                        e.Cancel = true;
                    }
                    else
                    {
                        txtCte.Enabled = false;
                        txtCte.Text = leer.Campo("IdCliente");
                        lblCte.Text = leer.Campo("NombreCliente");
                        txtSubCte.Text = "";
                        lblSubCte.Text = "";
                        txtPro.Text = "";
                        lblPro.Text = "";
                        txtSubPro.Text = "";
                        lblSubCte.Text = "";

                        //// Exigir la informacion de Seguro Popular solo si esta activo.
                        //if (bValidarSeguroPopular)
                        {
                            if (sIdSeguroPopular == txtCte.Text.Trim())
                            {
                                bEsSeguroPopular = true;
                            }
                        }
                    }
                }
            }
        }

        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCte.Text.Trim() == "")
            {
                txtSubCte.Text = "";
                lblSubCte.Text = "";
                txtPro.Text = "";
                lblPro.Text = "";
                txtSubPro.Text = "";
                lblSubCte.Text = "";
            }
            else
            {
                leer.DataSetClase = Consulta.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, "txtCte_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Sub-Cliente no encontrada, ó el Sub-Cliente no pertenece a la Farmacia.");
                    e.Cancel = true;
                }
                else
                {
                    ////// Obtener datos de IMach 
                    ////sFolioSolicitud = IMachPtoVta.ObtenerFolioSolicitud(); 

                    txtSubCte.Enabled = false;
                    txtSubCte.Text = leer.Campo("IdSubCliente");
                    lblSubCte.Text = leer.Campo("NombreSubCliente");

                    ////bPermitirCapturaBeneficiariosNuevos = leer.CampoBool("PermitirCapturaBeneficiarios");
                    ////bImportarBeneficiarios = leer.CampoBool("PermitirImportaBeneficiarios");
                    ////bPermitirCapturaBeneficiariosNuevos = GnFarmacia.ValidarCapturaBeneficiariosNuevos(bPermitirCapturaBeneficiariosNuevos);


                    txtPro.Text = "";
                    lblPro.Text = "";
                    txtSubPro.Text = "";
                    lblSubPro.Text = "";

                    //if (txtFolio.Text.Trim() == "*" && !bEsEDM)
                    //{
                    //    btnCodificacion.Enabled = btnGuardar.Enabled;
                    //}

                    //btnRecetasElectronicas.Enabled = GnFarmacia.ImplementaInterfaceExpedienteElectronico;


                    ////// Exclusivo Seguro Popular 
                    ////if (bEsSeguroPopular)
                    ////    MostrarInfoVenta(); 

                    //////// Inicializar el Grid 
                    //////myGrid.Limpiar(true); 

                }
            }

        }

        private void txtPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "")
            {
                leer.DataSetClase = Consulta.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtPro.Text, "txtPro_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Programa no encontrada, ó el Programa no pertenece al Cliente ó Farmacia.");
                    txtPro.Text = "";
                    lblPro.Text = "";
                    txtSubPro.Text = "";
                    lblSubPro.Text = "";
                    e.Cancel = true;
                }
                else
                {
                    txtPro.Enabled = false;
                    txtPro.Text = leer.Campo("IdPrograma");
                    lblPro.Text = leer.Campo("Programa");
                    txtSubPro.Text = "";
                    lblSubPro.Text = "";
                }
            }
            else
            {
                txtPro.Text = "";
                lblPro.Text = "";
                txtSubPro.Text = "";
                lblSubPro.Text = "";
            }
        }

        private void txtSubPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "" && txtSubPro.Text.Trim() != "")
            {
                leer.DataSetClase = Consulta.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtPro.Text, txtSubPro.Text, "txtPro_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Sub-Programa no encontrada, ó el Sub-Programa no pertenece al Cliente ó Farmacia.");
                    e.Cancel = true;
                }
                else
                {
                    txtSubPro.Enabled = false;
                    txtSubPro.Text = leer.Campo("IdSubPrograma");
                    lblSubPro.Text = leer.Campo("SubPrograma");

                    IniciaToolBar_Peticiones(true, true, false, true); 

                    //bForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma = leer.CampoBool("Dispensacion_CajasCompletas");

                    ////// Obtener datos de IMach 
                    //sFolioSolicitud = IMachPtoVta.ObtenerFolioSolicitud();

                    //// Exclusivo Seguro Popular 
                    //if (bEsSeguroPopular)
                    //{
                    //    MostrarInfoVenta();
                    //}

                    //if (!bEsSurtimientoPedido && !bEsEDM)
                    //{
                    //    myGrid.Limpiar(true);
                    //    btnCodificacion.Enabled = (bImplementaCodificacion || bImplementaReaderDM);

                    //    if (btnCodificacion.Enabled)
                    //    {
                    //        myGrid.BloqueaGrid(true);
                    //    }
                    //}
                }
            }
            else
            {
                txtSubPro.Text = "";
                lblSubPro.Text = "";
            }
        }
        #endregion Eventos    

        #region Guardado de informacion 
        private bool validarDatos()
        {
            bool bRegresa = true;

            if ( txtNumeroDeDocumento.Text == "" )
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Número de Documento de Orden de Acondicionamiento, verifique.");
                txtNumeroDeDocumento.Focus();
            }

            if ( bRegresa && txtObservaciones.Text == "") 
            {
                bRegresa = false;
                General.msjUser("No ha capturado las Observaciones de la Orden de Acondicionamiento, verifique.");
                txtObservaciones.Focus();
            }

            return bRegresa; 
        }

        private bool GuardarInformacion()
        {
            bool bRegresa = true;

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion(); 
            }
            else
            {
                cnn.IniciarTransaccion();

                bRegresa = GuardarInformacion_01_Encabezado(); 


                if (!bRegresa)
                {
                    cnn.DeshacerTransaccion();
                    General.msjError("");
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamete."); 
                }

                cnn.Cerrar();

                if (bRegresa)
                {
                    InicializarPantalla(); 
                }
            }


            return bRegresa; 
        }

        private bool GuardarInformacion_01_Encabezado()
        {
            bool bRegresa = true;
            string sSql = ""; 

            sSql = string.Format("Exec spp_Mtto_IATP2_OrdenesDeProduccion " + 
                "  @FolioSolicitud = '{0}', @IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', @IdCliente = '{4}', @IdSubCliente = '{5}', " +
                " @IdPrograma = '{6}', @IdSubPrograma = '{7}', @NumeroDeDocumento = '{8}', @Observaciones = '{9}', @Status = '{10}' ", 
                txtFolio.Text, DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                txtCte.Text, txtSubCte.Text, txtPro.Text, txtSubPro.Text, txtNumeroDeDocumento.Text, txtObservaciones.Text, "A");

            if (!leer.Exec(sSql))
            {
                bRegresa = false; 
            }
            else
            {
                if (leer.Leer())
                {
                    sFolioSolicitud = leer.Campo("FolioSolicitud"); 
                }
            }

            if (bRegresa)
            {
                bRegresa = GuardarInformacion_02_InformacionAdicional();
            }

            return bRegresa;
        }

        private bool GuardarInformacion_02_InformacionAdicional()
        {
            bool bRegresa = true;
            int iConsecutivo = 0;
            int iPartida = 0;
            DateTime dtFecha = DateTime.Now;
            DateTime dtHora = DateTime.Now; 
            clsLeer detallesSolicitud = new clsLeer();
            string sFechaHora = ""; 
            string sSql = "";

            sFolioVenta = ""; 
            for (int i = 0; i <= lst.Registros - 1; i++)
            {
                iConsecutivo++;
                InfVtas = (clsInformacion_OrdenDeAcondicionamiento)listView_Beneficiarios.Items[i].SubItems[0].Tag;

                sSql = string.Format(" EXEC spp_Mtto_IATP2_OrdenesDeProduccion_InformacionAdicional " +
                    " @FolioSolicitud = '{0}', @Consecutivo = '{1}', @IdEmpresa = '{2}', @IdEstado = '{3}', @IdFarmacia = '{4}', @FolioVenta = '{5}', " + 
                    " @IdBeneficiario = '{6}', @IdTipoDeDispensacion = '{7}', @NumeroDeHabitacion = '{8}', @NumeroDeCama = '{9}', @NumReceta = '{10}', @FechaReceta = '{11}', " + 
                    " @IdMedico = '{12}', @IdBeneficio = '{13}', @IdDiagnostico = '{14}', @IdUMedica = '{15}', @IdServicio = '{16}', @IdArea = '{17}', " + 
                    " @RefObservaciones = '{18}', @Status = '{19}' \n\n",
                    sFolioSolicitud, iConsecutivo, DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), sFolioVenta,
                    InfVtas.Beneficiario, InfVtas.TipoDispensacion, InfVtas.NumeroDeHabitacion, InfVtas.NumeroDeCama, InfVtas.Receta, General.FechaYMD(InfVtas.FechaReceta, "-"),
                    InfVtas.Medico, InfVtas.IdBeneficio, InfVtas.Diagnostico, InfVtas.CluesRecetasForaneas, InfVtas.Servicio, InfVtas.Area, InfVtas.ReferenciaObservaciones, "A");

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
                else
                {
                    detallesSolicitud.DataSetClase = InfVtas.ListaProductos_Solicitud;
                    iPartida = 0;
                    while (detallesSolicitud.Leer())
                    {
                        dtFecha = detallesSolicitud.CampoFecha("FechaAdmin");
                        dtHora = detallesSolicitud.CampoFecha("HoraAdmin");
                        dtFecha = new DateTime(dtFecha.Year, dtFecha.Month, dtFecha.Day, dtHora.Hour, dtHora.Minute, dtHora.Second);
                        sFechaHora = string.Format("{0}", General.FechaHora(dtFecha) ); 

                        iPartida++;
                        sSql = string.Format(" EXEC spp_Mtto_IATP2_OrdenesDeProduccion_Productos " +
                            " @FolioSolicitud = '{0}', @Consecutivo = '{1}', @Partida = '{2}', @IdProducto = '{3}', @CodigoEAN = '{4}', @CantidadSolicitada = '{5}', " +
                            " @FechaHora_De_Administracion = '{6}', @Status = '{7}'  ",
                            sFolioSolicitud, iConsecutivo, iPartida, 
                            detallesSolicitud.Campo("IdProducto"), detallesSolicitud.Campo("CodigoEAN"), detallesSolicitud.CampoInt("Cantidad"), 
                            sFechaHora, 
                            "A"
                            );
                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }


            ////if (bRegresa)
            ////{
            ////    bRegresa = GuardarInformacion_03_Detalles(); 
            ////}

            return bRegresa;
        }

        private bool GuardarInformacion_03_Detalles()
        {
            bool bRegresa = true;
            string sSql = ""; 


            return bRegresa;
        }
        #endregion Guardado de informacion
    }
}
