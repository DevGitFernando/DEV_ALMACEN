using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.ExportarDatos;


using SC_SolutionsSystem.OfficeOpenXml; 
using SC_SolutionsSystem.OfficeOpenXml.Data; 

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace OficinaCentral.CuadrosBasicos
{
    public partial class FrmCB_ProgramacionesDeConsumo : FrmBaseExt
    {
        clsConsultas query;
        DataSet dtsDatos = new DataSet();
        DataSet dtsGrupos = new DataSet(), dtsUsuariosGrupo = new DataSet();
        DataSet dtsClientes = new DataSet();
        DataSet dtsSubClientes = new DataSet();
        clsDatosCliente DatosCliente;

        string sPermisoPerfiles = "MODIFICAR_PERFILES";
        bool bPermisoPerfiles = false;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer reader, reader2, leer;


        clsListView lst;

        //clsLeerExcel excel;
        clsLeerExcelOpenOficce excel;
        clsExportarExcelPlantilla xpExcel;
        OpenFileDialog openExcel = new OpenFileDialog();
        Thread thLoadFile;

        string sNombreHoja = "Programacion".ToUpper();
        bool bExisteHoja = false; 

        int iRegistrosHoja = 0;
        int iRegistrosProcesados = 0; 

        string sTitulo = ""; 
        string sFile_In = "";
        string sFormato = "###, ###, ###, ##0";
        int iFolioCargaMasiva = 0;
        string sMensaje = "";
        string sMsjGuardar = "";
        string sMsjError = "";

        bool bValidandoInformacion = false;
        bool bSeEncontraronIndicencias = false;
        bool bActivarProceso = false;
        bool bErrorAlValidar = false;

        int iPosicion_Oculta = 0;
        int iPosicion_Mostrar_Top = 0;
        int iPosicion_Mostrar_Left = 0;

        public FrmCB_ProgramacionesDeConsumo()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.Puerto = General.DatosConexion.Puerto;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;



            leer = new clsLeer(ref cnn);
            //excel = new clsLeerExcel(); 
            excel = new clsLeerExcelOpenOficce();


            lst = new clsListView(lstVwInformacion);
            lst.OrdenarColumnas = false;

            //FrameResultado.Height = 554;
            //FrameResultado.Width = 1314;

            //FrameProceso.Top = 350;
            FrameProceso.Left = 288;

            iPosicion_Oculta = (int)General.Pantalla.Ancho;
            iPosicion_Mostrar_Top = (FrameResultado.Height / 2) - (FrameProceso.Height / 2);
            iPosicion_Mostrar_Left = (FrameResultado.Width / 2) - (FrameProceso.Width / 2);

            MostrarEnProceso(false, 0);


            SolicitarPermisosUsuario(); 
            CargarEstados(); 

        }

        #region Permisos de Usuario
        private void SolicitarPermisosUsuario()
        {
            ////// Valida si el usuario conectado tiene permiso sobre las opcione especiales 
            ////Permisos = new clsOperacionesSupervizadas(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            ////Permisos.Personal = DtGeneral.IdPersonal; 
            bPermisoPerfiles = DtGeneral.PermisosEspeciales.TienePermiso(sPermisoPerfiles);
        }
        #endregion Permisos de Usuario

        private void FrmCB_ProgramacionesDeConsumo_Load(object sender, EventArgs e)
        {
            IniciarPantalla();
        }

        #region Botones 
        private void IniciarPantalla()
        {
            lst.Limpiar(); 
            Fg.IniciaControles();

            lblProcesados.Text = "";
            lblProcesados.Visible = false; 

            sTitulo = "Información ";
            IniciaToolBar(); 
        }

        private void IniciaToolBar()
        {
            IniciaToolBar(true, true, true, false); 
        }

        private void IniciaToolBar(bool Nuevo, bool Exportar, bool Abrir, bool Guardar)
        {
            btnNuevo.Enabled = Nuevo;
            btnExportarExcel.Enabled = Exportar;
            btnAbrir.Enabled = Abrir;
            btnGuardar.Enabled = Guardar; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            IniciarPantalla();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (validarParametros())
            {
                openExcel.Title = "Archivos de programación y ampliación de consumos";
                openExcel.Filter = "Archivos de Excel (*.xls;*.xlsx)| *.xls;*.xlsx";
                //openExcel.Filter = "Archivos de Texto (*.txt)| *.txt"; 
                openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
                openExcel.AddExtension = true;
                lblProcesados.Visible = false;

                // if (openExcel.FileName != "")
                if (openExcel.ShowDialog() == DialogResult.OK)
                {
                    sFile_In = openExcel.FileName;

                    BloqueaControles(true); 
                    IniciaToolBar(false, false, false, false);
                    
                    tmValidacion.Enabled = true;
                    tmValidacion.Interval = 1000;
                    tmValidacion.Start();


                    thLoadFile = new Thread(this.CargarArchivo);
                    thLoadFile.Name = "LeerArchivo";
                    thLoadFile.Start();
                }
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sMensaje = "¿ Desea integrar la información de programación de consumos, este proceso no se podra deshacer ?";

            if (General.msjConfirmar(sMensaje) == System.Windows.Forms.DialogResult.Yes)
            {
                IntegrarInformacion();
            }
        }

        private bool validarParametros()
        {
            bool bRegresa = true;

            if (cboSubClientes.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Sub-Cliente válido."); 
                cboSubClientes.Focus(); 
            }

            return bRegresa; 
        }
        
        private void MostrarEnProceso(bool Mostrar, int Proceso)
        {
            string sTituloProceso = "";

            iPosicion_Oculta = (int)General.Pantalla.Ancho;
            iPosicion_Mostrar_Top = (FrameResultado.Height / 2) - (FrameProceso.Height / 2);
            iPosicion_Mostrar_Left = (FrameResultado.Width / 2) - (FrameProceso.Width / 2);

            FrameProceso.Top = iPosicion_Mostrar_Top;
            if(Mostrar)
            {
                FrameProceso.Left = iPosicion_Mostrar_Left;
            }
            else
            {
                FrameProceso.Left = iPosicion_Oculta * 2;
            }

            if (Proceso == 1)
            {
                sTituloProceso = "Leyendo estructura del documento";
            }

            if (Proceso == 2)
            {
                sTituloProceso = "Leyendo información de hoja seleccionada";
            }

            if (Proceso == 3)
            {
                sTituloProceso = "Guardando información de hoja seleccionada";
            }

            if (Proceso == 4)
            {
                sTituloProceso = "Verificando información a integrar";
            }

            if (Proceso == 5)
            {
                sTituloProceso = "Integrando información ..... ";
            }

            FrameProceso.Text = sTituloProceso;

        }
        #endregion Botones 

        #region Combos
        private void CargarEstados()
        {
            string sSql = " Select Distinct IdEstado, Estado, ( IdEstado + ' - ' + Estado ) as Descripcion From vw_Farmacias_Urls (NoLock) Order by IdEstado ";
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");

            cboClientes.Clear();
            cboClientes.Add("0", "<< Seleccione >>");

            cboSubClientes.Clear();
            cboSubClientes.Add("0", "<< Seleccione >>");


            if (!DtGeneral.EsAdministrador)
            {
                sSql = string.Format(" Select Distinct IdEstado, Estado From vw_Farmacias_Urls (NoLock) " +
                    " Where IdEstado = '{0}' Order by IdEstado ", DtGeneral.EstadoConectado);
            }

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEstados()");
                General.msjError("Ocurrió un error al Cargar la Lista de Estados.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Descripcion");
                }
            }

            //reader.Exec("Select *, (IdCliente + ' - ' + NombreCliente) as NombreCliente From vw_Claves_Precios_Asignados (NoLock) Order By IdEstado, IdCliente ");
            leer.Exec("Select Distinct IdEstado, IdCliente, (IdCliente + ' - ' + NombreCliente) as NombreCliente From vw_Claves_Precios_Asignados (NoLock) Order By IdEstado, IdCliente ");
            dtsClientes = leer.DataSetClase;

            // SE OBTIENE LOS SUB-CLIENTES
            leer.Exec("Select Distinct IdEstado, IdCliente, IdSubCliente, (IdSubCliente + ' - ' + NombreSubCliente) as NombreSubCliente From vw_Claves_Precios_Asignados (NoLock) Order By IdEstado, IdCliente, IdSubCliente ");
            dtsSubClientes = leer.DataSetClase;

            cboEstados.SelectedIndex = 0;
            if (!DtGeneral.EsAdministrador)
            {
                if (!bPermisoPerfiles)
                {
                    cboEstados.Data = DtGeneral.EstadoConectado;
                    cboEstados.Enabled = false;
                }
            }

        }

        private void CargarClientes()
        {
            cboClientes.Clear();
            cboClientes.Add("0", "<< Seleccione >>");

            if (cboEstados.SelectedIndex != 0)
            {
                try
                {
                    cboClientes.Add(dtsClientes.Tables[0].Select("IdEstado = '" + cboEstados.Data + "'"), true, "IdCliente", "NombreCliente");
                }
                catch { }
            }
            cboClientes.SelectedIndex = 0;
        }

        private void CargarSubClientes()
        {
            cboSubClientes.Clear();
            cboSubClientes.Add("0", "<< Seleccione >>");

            if (cboClientes.SelectedIndex != 0)
            {
                try
                {
                    cboSubClientes.Add(dtsSubClientes.Tables[0].Select("IdEstado = '" + cboEstados.Data + "'" + " And IdCliente = '" + cboClientes.Data + "'"), true, "IdSubCliente", "NombreSubCliente");
                }
                catch { }
            }
            cboSubClientes.SelectedIndex = 0;
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboClientes.SelectedIndex = 0;
            cboSubClientes.SelectedIndex = 0;

            CargarClientes();
        }

        private void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSubClientes.SelectedIndex = 0;
            if (cboClientes.SelectedIndex != 0)
            {
                CargarSubClientes();
            }
        }

        private void cboSubClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        #endregion Combos 

        #region Funciones y Procedimientos Privados 
        private void Inicializa()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");

            cboClientes.Clear();
            cboClientes.Add("0", "<< Seleccione >>");

            cboSubClientes.Clear();
            cboSubClientes.Add("0", "<< Seleccione >>");

            CargarEstados();

            cboEstados.SelectedIndex = 0;
            cboClientes.SelectedIndex = 0;
            cboSubClientes.SelectedIndex = 0;

            cboEstados.Focus();
        } 

        private void CargarArchivo()
        {
            string sHoja = "";
            bool bRegresa = false;

            bValidandoInformacion = true; 
            MostrarEnProceso(true, 1);
            FrameResultado.Text = sTitulo;

            //excel = new clsLeerExcel(sFile_In); 
            excel = new clsLeerExcelOpenOficce(sFile_In);
            excel.GetEstructura();

            lst.Limpiar();
            Thread.Sleep(1000);

            //bRegresa = excel.Hojas.Registros > 0;
            while (excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja").ToUpper() ;
                if (sHoja == sNombreHoja)
                {
                    bRegresa = true;
                    break; 
                }
            }

            bExisteHoja = bRegresa;
            if (bRegresa)
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

            FrameResultado.Text = sTitulo;
            lst.Limpiar();
            excel.LeerHoja(sNombreHoja);

            FrameResultado.Text = sTitulo;
            if (excel.Leer())
            {
                bRegresa = true;
                iRegistrosHoja = excel.Registros;
                FrameResultado.Text = string.Format("{0}: {1} registros ", sTitulo, iRegistrosHoja.ToString(sFormato));
                lst.CargarDatos(excel.DataSetClase, true, true);
            }

            if (bRegresa)
            {
                bRegresa = CargarInformacionDeHoja(); 
            }

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
            bulk.DestinationTableName = "CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva";
            bulk.AddColumn("IdEstado", "IdEstado");
            bulk.AddColumn("IdFarmacia", "IdFarmacia");
            bulk.AddColumn("Clave SSA", "ClaveSSA");
            bulk.AddColumn("Año", "Año");
            bulk.AddColumn("Mes", "Mes");
            bulk.AddColumn("Cantidad", "Cantidad");


            leer.Exec("Truncate table CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva ");

            bRegresa = bulk.WriteToServer(excel.DataSetClase); //, System.Data.SqlClient.SqlBulkCopyOptions.Default); 


            if (bRegresa)
            {
                bRegresa = validarInformacion(); 
            }

            return bRegresa; 
        }

        private void bulk_RowsCopied(RowsCopiedEventArgs e)
        {
            lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato), excel.Registros.ToString(sFormato)); 
        }

        private void bulk_Compled(RowsCopiedEventArgs e)
        {
            lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato), excel.Registros.ToString(sFormato)); 
        }

        private void bulk_Error(RowsCopiedEventArgs e)
        {
            lblProcesados.Text = string.Format("Ocurrio un error al procesar {0} de {1}", e.RowsCopied.ToString(sFormato), excel.Registros.ToString(sFormato));
            Error.GrabarError(e.Error, "bulk_Error"); 
        }

        private bool validarInformacion()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_CFG_CB_CuadroBasico_Claves_Programacion__ValidarDatosDeEntrada " +
                "  @IdEstado = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdEstado_Registra = '{3}', @IdFarmacia_Registra = '{4}', @IdPersonal_Registra = '{5}' ", 
                cboEstados.Data, cboClientes.Data, cboSubClientes.Data, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "validarInformacion()");
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
            leer.RenombrarTabla(1, "Resultados");
            leerResultado.DataTableClase = leer.Tabla(1);
            while (leerResultado.Leer())
            {
                leer.RenombrarTabla(leerResultado.CampoInt("Orden"), leerResultado.Campo("NombreTabla"));
            }

            dtsResultados = leer.DataSetClase;
            dtsResultados.Tables.Remove("Resultados");
            leer.DataSetClase = dtsResultados;

            foreach (DataTable dt in leer.DataSetClase.Tables)
            {
                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = dt.Copy();
                    bActivarProceso = leerValidacion.Registros > 0;
                }
            }

            bRegresa = !bActivarProceso;

            return bRegresa;
        }

        private bool IntegrarInformacion()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_CFG_CB_CuadroBasico_Claves_Programacion__IntegrarInformacion ");

            btnGuardar.Enabled = false; 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "IntegrarInformacion()");
                btnGuardar.Enabled = true; 
                General.msjError("Ocurrió un error al integrar la información.");
            }
            else
            {
                General.msjUser("Programación de consumos integrada satisfactoriamente.");
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Privados

        #region Eventos 
        private void BloqueaControles(bool Bloquear)
        {
            bool bBloquear = !Bloquear;

            cboEstados.Enabled = bBloquear;
            cboClientes.Enabled = bBloquear;
            cboSubClientes.Enabled = bBloquear; 
        }

        private void tmValidacion_Tick(object sender, EventArgs e)
        {
            tmValidacion.Stop();
            tmValidacion.Enabled = false;

            if (!bValidandoInformacion)
            {
                if (!bExisteHoja)
                {
                    General.msjAviso("El archivo cargado no contiene la hoja llamada Programación."); 
                }
                else 
                {
                    if (bActivarProceso)
                    {
                        BloqueaControles(true);
                        IniciaToolBar(true, true, false, true);
                    }
                    else
                    {
                        BloqueaControles(false);
                        IniciaToolBar(true, true, true, false);
                        if (!bErrorAlValidar)
                        {
                            FrmIncidencias f = new FrmIncidencias("Programación de consumos", leer.DataSetClase, DtGeneral.EsAdministrador);
                            f.ShowDialog(this);

                            if (DtGeneral.EsAdministrador & f.ExcluirIncidencias)
                            {
                                IniciaToolBar(true, true, false, true);
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
        #endregion Eventos
    }
}
