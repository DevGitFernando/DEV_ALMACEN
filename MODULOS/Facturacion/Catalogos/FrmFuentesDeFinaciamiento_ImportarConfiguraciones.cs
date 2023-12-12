﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.OfficeOpenXml;
using SC_SolutionsSystem.OfficeOpenXml.Data;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllFarmaciaSoft.Informacion;

using Dll_IFacturacion;

namespace Facturacion.Catalogos
{
    public partial class FrmFuentesDeFinaciamiento_ImportarConfiguraciones : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeerExcelOpenOficce excel;

        clsDatosCliente DatosCliente;

        string sIdDistribuidor = ""; 
        string sFile_In = "";
        string sTitulo = "";

        int iRegistrosHoja = 0; 
        int iRegistrosProcesados = 0;  

        clsListView lst;

        OpenFileDialog openExcel = new OpenFileDialog();
        Thread thLoadFile;
        Thread thReadFile;
        Thread thGuardarInformacion;
        Thread thValidarInformacion; 
        Thread thGeneraFolios;

        string sFormato = "###, ###, ###, ##0";
        string sFormato_INT = "###, ###, ###, ##0";
        int iFolioCargaMasiva = 0;
        string sMensaje = "";

        bool bValidandoInformacion = false;
        bool bSeEncontraronIndicencias = false;
        //bool bActivarProceso = false; 
        bool bErrorAlValidar = false;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;
        string sMsjNoEncontrado = "";
        string sGUID = Guid.NewGuid().ToString();
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;
        TiposDePedidosCEDIS tpTipoDePedido = TiposDePedidosCEDIS.Ninguno;

        clsFFinaciamiento_Importar Importar;


        public FrmFuentesDeFinaciamiento_ImportarConfiguraciones( )
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


            this.sIdDistribuidor = Fg.PonCeros("", 4); 
            leer = new clsLeer(ref cnn);
            excel = new clsLeerExcelOpenOficce();

            lst = new clsListView(lstVwInformacion);
            lst.OrdenarColumnas = false; 

            //FrameResultado.Height = 350;
            //FrameResultado.Width = 800;

            FrameProceso.Top = 300;
            FrameProceso.Left = 190; 
            MostrarEnProceso(false, 0);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sPersonal);
        }

        private void FrmFuentesDeFinaciamiento_ImportarConfiguraciones_Load(object sender, EventArgs e)
        {
            Configuraciones_FF();

            btnNuevo_Click(null, null);
        }

        #region Botones 
        private void IniciaToolBar(bool Nuevo, bool Abrir, bool Ejecutar, bool Guardar, bool Validar, bool Procesar, bool Salir)
        {
            btnNuevo.Enabled = Nuevo;
            btnAbrir.Enabled = Abrir;
            btnEjecutar.Enabled = Ejecutar;
            btnGuardar.Enabled = Guardar;
            btnValidarDatos.Enabled = Validar; 
            btnProcesarRemisiones.Enabled = Procesar; 
            btnSalir.Enabled = Salir;

            Application.DoEvents();
            System.Threading.Thread.Sleep(50);
            Application.DoEvents();
        } 

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // int iRegistrosHoja = 0;
            // int iRegistrosProcesados = 0;
            Importar = new clsFFinaciamiento_Importar(General.DatosConexion, General.DatosApp);

            sGUID = Guid.NewGuid().ToString();

            lblProcesados.Visible = false;
            lblProcesados.Text = ""; 

            sFile_In = "";  
            cboHojas.Clear();
            cboHojas.Add();

            sTitulo = "Información "; 
            FrameResultado.Text = sTitulo;
            
            ////FrameTipoInv.Enabled = true;
            ////rdoTransferencia.Checked = false;
            ////rdoVenta.Checked = false;

            Fg.IniciaControles();
            lst.Limpiar();

            btnEjecutar.Enabled = false;
            btnGuardar.Enabled = false;

            IniciaToolBar(true, true, false, false, false, false, true);

        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            openExcel.Title = "Archivos de Pedidos";
            openExcel.Filter = "Archivos de Excel (*.xls;*.xlsx)| *.xls;*.xlsx";
            //openExcel.Filter = "Archivos de Texto (*.txt)| *.txt"; 
            openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openExcel.AddExtension = true;
            lblProcesados.Visible = false;

            // if (openExcel.FileName != "")
            if (openExcel.ShowDialog() == DialogResult.OK) 
            {
                sFile_In = openExcel.FileName;
                
                //CargarArchivo(); 
                IniciaToolBar(false, false, false, false, false, false, false); 
                thReadFile = new Thread(this.CargarArchivo);
                thReadFile.Name = "LeerArchivo";
                thReadFile.Start(); 
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            thLoadFile = new Thread(this.thLeerHoja);
            thLoadFile.Name = "LeerDocumentoExcel";
            thLoadFile.Start(); 
            //LeerHoja(); 
        }

        private void btnValidarDatos_Click(object sender, EventArgs e)
        {
            tmValidacion.Enabled = true;
            tmValidacion.Interval = 1000; 
            tmValidacion.Start();

            thValidarInformacion = new Thread(Importar.ValidarInformacion);
            thValidarInformacion.Name = "Validar informacion";
            thValidarInformacion.Start();
            System.Threading.Thread.Sleep(200);



        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            BloqueaHojas(true);
            MostrarEnProceso(true, 4);


            Importar.Tipo = cboConfiguracionesFuentesDeFinanciamiento.Data;
            Importar.excel = excel;
            Importar.sHoja = cboHojas.Data;

            tmGuardar.Enabled = true;
            tmGuardar.Interval = 1000;
            tmGuardar.Start();

            thGuardarInformacion = new Thread(Importar.GuardarInformacion);
            thGuardarInformacion.Name = "Guardar información seleccionada";
            thGuardarInformacion.Start();

            //BloqueaHojas(false);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion Botones 

        #region Configuraciones 
        ////enum FuentesDeFinanciamiento_Configuracion
        ////{
        ////    Ninguna = 0, 
            
        ////    Insumo = 1, 
        ////    Servicio, 
        ////    Documentos, 
            
        ////    Insumo_Clave_Farmacia, 
        ////    Servicio_Clave_Farmacia, 

        ////    Insumo_Clave_Jurisdiccion,
        ////    Servicio_Clave_Jurisdiccion, 

        ////    ExcepcionPrecios_Insumos,
        ////    ExcepcionPrecios_Servicio 
        ////}

        private string GetConfiguracion( FuentesDeFinanciamiento_Configuracion Configuracion)
        {
            string sRegresa = "";
            int iFF = (int)Configuracion;

            sRegresa = iFF.ToString(); 

            return sRegresa;  
        }

        private void Configuraciones_FF()
        {
            cboConfiguracionesFuentesDeFinanciamiento.Clear();
            cboConfiguracionesFuentesDeFinanciamiento.Add(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Ninguna), "<< Seleccione >>");


            cboConfiguracionesFuentesDeFinanciamiento.Add(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Insumo), string.Format("{0} -- Insumo", Fg.PonCeros(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Insumo), 2)));
            cboConfiguracionesFuentesDeFinanciamiento.Add(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Servicio), string.Format("{0} -- Servicio", Fg.PonCeros(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Servicio), 2)));
            cboConfiguracionesFuentesDeFinanciamiento.Add(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Documentos), string.Format("{0} -- Documentos", Fg.PonCeros(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Documentos), 2)));

            cboConfiguracionesFuentesDeFinanciamiento.Add(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.BeneficiariosJurisdiccion), string.Format("{0} -- Beneficiarios jurisdiccionales", Fg.PonCeros(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.BeneficiariosJurisdiccion), 2)));
            cboConfiguracionesFuentesDeFinanciamiento.Add(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.BeneficiariosRelacionados_Jurisdiccion), string.Format("{0} -- Beneficiarios por jurisdicción", Fg.PonCeros(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.BeneficiariosRelacionados_Jurisdiccion), 2)));

            cboConfiguracionesFuentesDeFinanciamiento.Add(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Insumo_Clave_Farmacia), string.Format("{0} -- Insumo a nivel Clave-Farmacia", Fg.PonCeros(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Insumo_Clave_Farmacia), 2)));
            cboConfiguracionesFuentesDeFinanciamiento.Add(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Servicio_Clave_Farmacia), string.Format("{0} -- Servicio a nivel Clave-Farmacia", Fg.PonCeros(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Servicio_Clave_Farmacia), 2)));

            cboConfiguracionesFuentesDeFinanciamiento.Add(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Grupos_De_Remisiones), string.Format("{0} -- Grupos de remisiones", Fg.PonCeros(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Grupos_De_Remisiones), 2)));

            cboConfiguracionesFuentesDeFinanciamiento.Add(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Insumo_Clave_Jurisdiccion), string.Format("{0} -- Insumo a nivel Clave-Jurisdicción", Fg.PonCeros(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Insumo_Clave_Jurisdiccion), 2)));
            cboConfiguracionesFuentesDeFinanciamiento.Add(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Servicio_Clave_Jurisdiccion), string.Format("{0} -- Servicio a nivel Clave-Jurisdicción", Fg.PonCeros(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.Servicio_Clave_Jurisdiccion), 2)));

            cboConfiguracionesFuentesDeFinanciamiento.Add(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.ExcepcionPrecios_Insumos), string.Format("{0} -- Insumo excepción de precios", Fg.PonCeros(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.ExcepcionPrecios_Insumos), 2)));
            cboConfiguracionesFuentesDeFinanciamiento.Add(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.ExcepcionPrecios_Servicio), string.Format("{0} -- Servicio excepción de precios", Fg.PonCeros(GetConfiguracion(FuentesDeFinanciamiento_Configuracion.ExcepcionPrecios_Servicio), 2)));



            //cboConfiguracionesFuentesDeFinanciamiento.Add("3", "03 -- Insumo a nivel Clave-Farmacia");
            //cboConfiguracionesFuentesDeFinanciamiento.Add("4", "04 -- Servicio a nivel Clave-Farmacia");


            //cboConfiguracionesFuentesDeFinanciamiento.Add("5", "05 -- Insumo a nivel Clave-Jurisdicción");
            //cboConfiguracionesFuentesDeFinanciamiento.Add("6", "06 -- Servicio a nivel Clave-Jurisdicción");


            //cboConfiguracionesFuentesDeFinanciamiento.Add("7", "07 -- Insumo excepción de precios");
            //cboConfiguracionesFuentesDeFinanciamiento.Add("8", "08 -- Servicio excepción de precios");


            cboConfiguracionesFuentesDeFinanciamiento.SelectedIndex = 0;
        }
        #endregion Configuraciones 

        #region Funciones y Procedimientos Privados 
        private void CargarArchivo()
        {
            string sHoja = "";
            bool bHabilitar = false;

            BloqueaHojas(true);
            MostrarEnProceso(true, 1);
            FrameResultado.Text = sTitulo;

            excel = new clsLeerExcelOpenOficce(sFile_In);
            excel.GetEstructura();

            cboHojas.Clear();
            cboHojas.Add();
            lst.Limpiar();
            Thread.Sleep(1000);

            bHabilitar = excel.Hojas.Registros > 0;
            while (excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja");
                cboHojas.Add(sHoja, sHoja);
            }

            cboHojas.SelectedIndex = 0;
            btnEjecutar.Enabled = bHabilitar;
            IniciaToolBar(true, true, bHabilitar, false, false, false, true);

            BloqueaHojas(false);
            MostrarEnProceso(false, 1);
            // btnGuardar.Enabled = bHabilitar;

        }

        private void LeerExcel()
        {
            string sHoja = "";
            bool bHabilitar = false;
            excel = new clsLeerExcelOpenOficce(sFile_In);
            excel.GetEstructura();

            cboHojas.Clear();
            cboHojas.Add();
            lst.Limpiar();
            Thread.Sleep(1000);

            bHabilitar = excel.Hojas.Registros > 0;
            while (excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja");
                cboHojas.Add(sHoja, sHoja);
            }

            cboHojas.SelectedIndex = 0;
            btnEjecutar.Enabled = bHabilitar;
        }

        private void thLeerHoja()
        {
            BloqueaHojas(true);
            MostrarEnProceso(true, 2);
            lblProcesados.Visible = false;
            
            LeerHoja();

            ////if (rdoVenta.Checked || rdoTransferencia.Checked)
            ////{
            ////    FrameTipoInv.Enabled = false;
            ////    LeerHoja();
            ////}
            ////else
            ////{
            ////    General.msjAviso("Seleccione un tipo de pedido... ");
            ////}

            BloqueaHojas(false);
            MostrarEnProceso(false, 2); 
        }

        private bool LeerHoja()
        {
            bool bRegresa = false;
            
            IniciaToolBar(false, false, false, bRegresa, false, false, false);
            FrameResultado.Text = sTitulo; 
            lst.Limpiar(); 
            excel.LeerHoja(cboHojas.Data);

            FrameResultado.Text = sTitulo; 
            if (excel.Leer())
            {
                bRegresa = true;
                iRegistrosHoja = excel.Registros; 
                FrameResultado.Text = string.Format("{0}: {1} registros ", sTitulo, iRegistrosHoja.ToString(sFormato));
                if (excel.Registros < 1000)
                    lst.CargarDatos(excel.DataSetClase, true, true);
                else
                    General.msjUser("Se superó la cantidad maxima de registros a mostrar en pantalla.");
            }

            // btnGuardar.Enabled = bRegresa;
            IniciaToolBar(true, true, true, bRegresa, false, false, true); 
            return bRegresa; 
        }

        private void BloqueaHojas(bool Bloquear)
        {
            cboHojas.Enabled = !Bloquear; 
        }

        //private void ValidarInformacion()
        //{
        //    int iTipo;
        //    ////tmValidacion.Enabled = true;
        //    ////tmValidacion.Interval = 1000;
        //    ////tmValidacion.Start(); 

        //    bValidandoInformacion = true; 
        //    bActivarProceso = false;
        //    bErrorAlValidar = false; 
        //    clsLeer leerValidacion = new clsLeer();
        //    clsLeer leerRows = new clsLeer();
        //    leer = new clsLeer(ref cnn);
        //    DataSet dtsResultados = new DataSet();

        //    IniciaToolBar(false, false, false, false, false, bActivarProceso, false); 
        //    BloqueaHojas(true);
        //    MostrarEnProceso(true, 4);
        //    lblProcesados.Visible = false;

        //    //iTipo = rdoVenta.Checked ? 1 : 2;
        //    iTipo = (int)tpTipoDePedido; 

        //    string sSql = string.Format("Exec sp_Proceso_Pedidos_Cedis__CargaMasiva_000_Validar_Datos_De_Entrada  " + 
        //    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Tipo = '{3}', @GUID = '{4}' ",
        //    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iTipo, sGUID);


        //    if (!leer.Exec(sSql))
        //    {
        //        bErrorAlValidar = true; 
        //        bActivarProceso = !bActivarProceso;

        //        Error.GrabarError(leer, "ValidarInformacion()"); 
        //        General.msjError("Ocurrió un error al verificar el Pedido a integrar."); 
        //    }
        //    else
        //    {
        //        leer.RenombrarTabla(1, "Resultados");

        //        leerValidacion.DataTableClase = leer.Tabla("Resultados");

        //        dtsResultados = leer.DataSetClase;
        //        dtsResultados.Tables.Remove("Resultados");
        //        leer.DataSetClase = dtsResultados;

        //        for (int i = 1; leerValidacion.Leer();i++)
        //        {
        //            leer.RenombrarTabla(i, leerValidacion.Campo("Descripcion"));

        //            leerRows.DataTableClase = leer.Tabla(leerValidacion.Campo("Descripcion"));

        //            if (!bActivarProceso)
        //            bActivarProceso = leerRows.Registros > 0 ? true : false;

        //        }

        //    }


        //    bValidandoInformacion = false; 
        //    bActivarProceso = !bActivarProceso; 
        //    BloqueaHojas(false);
        //    MostrarEnProceso(false, 4);
        //}

        private void MostrarEnProceso(bool Mostrar, int Proceso)
        {
            string sTituloProceso = ""; 

            if (Mostrar)
            {
                FrameProceso.Left = 190;
            }
            else
            {
                FrameProceso.Left = this.Width + 1000;
            }

            if (Proceso == 1)
            {
                sTituloProceso = "Leyendo estructura del documento."; 
            }
            
            if (Proceso == 2)
            {
                sTituloProceso = "Leyendo información de hoja seleccionada."; 
            }

            if (Proceso == 3)
            {
                sTituloProceso = "Guardando información de hoja seleccionada."; 
            }

            if (Proceso == 4)
            {
                sTituloProceso = "Verificando información a integrar.";
            }

            if (Proceso == 5)
            {
                sTituloProceso = "Integrando Configuración.";
            }

            FrameProceso.Text = sTituloProceso; 

        }
        #endregion Funciones y Procedimientos Privados 

        #region Guardar Informacion 
        //private void thGuardarInformacion_Pedidos()
        //{
        //    GuardarInformacion_Pedidos();
        //}

        private string DarFormato(string Valor)
        {
            string sRegresa = Valor.Trim();

            sRegresa = sRegresa.Replace("'", "");
            sRegresa = sRegresa.Replace(",", "");

            return sRegresa;
        }

        //private void GuardarInformacion_Pedidos()
        //{
        //    bool bRegresa = false;
        //    string sSql = "";
        //    int iTipo = 1;
        //    clsLeer leerGuardar = new clsLeer(ref cnn);

        //    BloqueaHojas(true);
        //    MostrarEnProceso(true, 3);
        //    IniciaToolBar(false, false, false, false, false, false, false); 

        //    lblProcesados.Visible = true;
        //    lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato));


        //    // leerGuardar.DataSetClase = excel.DataSetClase;
        //    excel.RegistroActual = 1;
        //    bRegresa = excel.Registros > 0;
        //    iRegistrosProcesados = 0;

        //    //iTipo = rdoTransferencia.Checked ? 1 : 0;
        //    iTipo = tpTipoDePedido == TiposDePedidosCEDIS.Transferencia ? 1 : 0;



        //    clsBulkCopy bulk = new clsBulkCopy(General.DatosConexion);

        //    bulk.NotifyAfter = 500;
        //    bulk.RowsCopied += new clsBulkCopy.RowsCopiedEventHandler(bulk_RowsCopied);
        //    bulk.OnCompled += new clsBulkCopy.RowsCopiedEventHandler(bulk_Compled);
        //    bulk.OnError += new clsBulkCopy.RowsCopiedEventHandler(bulk_Error);

        //    bulk.ClearColumns();
        //    bulk.DestinationTableName = "Pedidos_Cedis__CargaMasiva";

        //    lblProcesados.Text = string.Format("Agregando información de control...");
        //    //// Agregar columnas 
        //    if(!excel.ExisteTablaColumna(1, "IdEmpresa"))
        //    {
        //        excel.DataSetClase = AgregarColumna(excel.DataSetClase, cboHojas.Data, "IdEmpresa", "String", DtGeneral.EmpresaConectada);
        //    }

        //    if(!excel.ExisteTablaColumna(1, "IdEstado"))
        //    {
        //        excel.DataSetClase = AgregarColumna(excel.DataSetClase, cboHojas.Data, "IdEstado", "String", DtGeneral.EstadoConectado);
        //    }

        //    if(!excel.ExisteTablaColumna(1, "IdFarmacia"))
        //    {
        //        excel.DataSetClase = AgregarColumna(excel.DataSetClase, cboHojas.Data, "IdFarmacia", "String", DtGeneral.FarmaciaConectada);
        //    }

        //    if(!excel.ExisteTablaColumna(1, "IdPersonal"))
        //    {
        //        excel.DataSetClase = AgregarColumna(excel.DataSetClase, cboHojas.Data, "IdPersonal", "String", DtGeneral.IdPersonal);
        //    }

        //    if(!excel.ExisteTablaColumna(1, "EsTransferencia"))
        //    {
        //        excel.DataSetClase = AgregarColumna(excel.DataSetClase, cboHojas.Data, "EsTransferencia", "Boolean", iTipo.ToString());
        //    }

        //    if(!excel.ExisteTablaColumna(1, "TipoDeClavesDePedido"))
        //    {
        //        excel.DataSetClase = AgregarColumna(excel.DataSetClase, cboHojas.Data, "TipoDeClavesDePedido", "String", "5");
        //    }

        //    if(!excel.ExisteTablaColumna(1, "UUID_Unique"))
        //    {
        //        excel.DataSetClase = AgregarColumna(excel.DataSetClase, cboHojas.Data, "UUID_Unique", "String", DarFormato(sGUID)); 
        //    }
        //    //// Agregar columnas 


        //    //// Asignacion de Columnas 
        //    lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato));
        //    bulk.AddColumn("IdEmpresa", "IdEmpresa");
        //    bulk.AddColumn("IdEstado", "IdEstado");
        //    bulk.AddColumn("IdFarmacia", "IdFarmacia");
        //    bulk.AddColumn("IdPersonal", "IdPersonal");
        //    bulk.AddColumn("EsTransferencia", "EsTransferencia");
        //    bulk.AddColumn("TipoDeClavesDePedido", "TipoDeClavesDePedido");
        //    bulk.AddColumn("UUID_Unique", "GUID");

        //    bulk.AddColumn("IdCliente", "IdCliente");
        //    bulk.AddColumn("IdSubCliente", "IdSubCliente");
        //    bulk.AddColumn("IdPrograma", "IdPrograma");
        //    bulk.AddColumn("IdSubPrograma", "IdSubPrograma");
        //    bulk.AddColumn("Observaciones", "Observaciones");
        //    bulk.AddColumn("ReferenciaInterna", "ReferenciaInterna");
        //    bulk.AddColumn("IdBeneficiario", "IdBeneficiario");
        //    bulk.AddColumn("FechaEntrega", "FechaEntrega");
        //    bulk.AddColumn("ClaveSSA", "ClaveSSA");
        //    bulk.AddColumn("ContenidoPaquete", "ContenidoPaquete");
        //    bulk.AddColumn("Cantidad", "Cantidad");
        //    //// Asignacion de Columnas 


        //    leerGuardar.Exec(string.Format( "Delete From Pedidos_Cedis__CargaMasiva Where GUID = '{0}' ", sGUID));
        //    bRegresa = bulk.WriteToServer(excel.DataSetClase); //, System.Data.SqlClient.SqlBulkCopyOptions.Default); 




        //    BloqueaHojas(false);
        //    MostrarEnProceso(false, 3);

        //    if (!bRegresa)
        //    { 
        //        General.msjError("Ocurrió un error al cargar la información de los pedidos.");
        //        IniciaToolBar(true, false, false, false, false, false, true);
        //    } 
        //    else 
        //    {
        //        leerGuardar.Exec("Exec spp_FormatearTabla 'Pedidos_Cedis__CargaMasiva'  ");
        //        General.msjUser("Información de Pedidos cargada satisfactoriamente."); 
        //        IniciaToolBar(true, false, false, false, true, false, true);
        //    }
        //}

        private static DataSet AgregarColumna( DataSet Datos, string Tabla, string Columna, string TipoDeDatos, string ValorDefault )
        {
            DataSet dts = Datos.Copy();
            DataTable dtConceptos;
            DataColumn dtColumnaNueva; 
            clsLeer leer = new clsLeer();

            leer.DataSetClase = Datos;
            if(leer.ExisteTabla(Tabla))
            {
                dtConceptos = leer.Tabla(Tabla);
                if(!leer.ExisteTablaColumna(Tabla, Columna))
                {
                    dtColumnaNueva = new DataColumn(Columna, System.Type.GetType(string.Format("System.{0}", TipoDeDatos)));
                    dtColumnaNueva.DefaultValue = ValorDefault; 
                    dtConceptos.Columns.Add(dtColumnaNueva);
             
                    dts.Tables.Remove(Tabla);
                    dts.Tables.Add(dtConceptos.Copy());
                }
            }

            return dts.Copy();
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
        #endregion Guardar Informacion

        #region Boton_ProcesarRemisiones
        private void btnProcesarRemisiones_Click(object sender, EventArgs e)
        {
            tmProcesar.Enabled = true;
            tmProcesar.Interval = 1000;
            tmProcesar.Start();
            //if (validarIntegracionInventario())
            {
                thGeneraFolios = new Thread(Importar.IntegrarPedidos);
                thGeneraFolios.Name = "Integrar informacion";
                thGeneraFolios.Start();
            }
        }

        private void tmValidacion_Tick(object sender, EventArgs e)
        {
            tmValidacion.Stop();
            tmValidacion.Enabled = false;


            if (!Importar.bValidandoInformacion)
            {
                if (Importar.bActivarProceso)
                {
                    IniciaToolBar(true, false, false, false, false, true, true);
                }
                else
                {
                    IniciaToolBar(true, false, false, false, true, false, true);
                    if (!Importar.bErrorAlValidar)
                    {
                        DllFarmaciaSoft.Inventario.FrmIncidencias f = new DllFarmaciaSoft.Inventario.FrmIncidencias(Importar.LeerValidacionFinal.DataSetClase);
                        f.ShowDialog();
                    }
                    else
                    {
                        General.msjError("Ocurrió un error al verificar el Pedido a integrar.");
                    }
                }
            }
            else
            {
                tmValidacion.Enabled = true;
                tmValidacion.Start();
            }
        }

        #endregion Boton_ProcesarRemisiones 

        private void tmPantalla_Tick(object sender, EventArgs e)
        {
            //tmPantalla.Enabled = false;
            //if (!DtGeneral.ValidaTransferenciasTransito())
            //{
            //    this.Close();
            //}
        }

        private void tmGuardar_Tick(object sender, EventArgs e)
        {
            tmGuardar.Stop();
            tmGuardar.Enabled = false;

            if (!Importar.bGuardandoInformacion)
            {
                MostrarEnProceso(false, 4);
                if (!Importar.bErrorAlGuardar)
                {
                    IniciaToolBar(true, false, false, false, true, false, true);
                    General.msjUser("Información cargada satisfactoriamente.");
                }
                else
                {
                    IniciaToolBar(true, true, true, true, false, false, true);
                    General.msjError("Ocurrió un error al cargar la información.");
                }
            }
            else
            {
                tmGuardar.Enabled = true;
                tmGuardar.Start();
            }
        }

        private void tmProcesar_Tick(object sender, EventArgs e)
        {
            tmProcesar.Stop();
            tmProcesar.Enabled = false;

            if (!Importar.bProcesandoInformacion)
            {
                MostrarEnProceso(false, 4);
                if (!Importar.bErrorAlProcesar)
                {
                    IniciaToolBar(true, false, false, false, false, false, true);
                    General.msjUser("Información integrada satisfactoriamente.");
                }
                else
                {
                    IniciaToolBar(true, false, false, false, false, true, true);
                    General.msjError("Ocurrió un error al integrar la información.");
                }
            }
            else
            {
                tmProcesar.Enabled = true;
                tmProcesar.Start();
            }
        }
    }
}
