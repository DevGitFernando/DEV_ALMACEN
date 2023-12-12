using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
using SC_SolutionsSystem.OfficeOpenXml;
using SC_SolutionsSystem.OfficeOpenXml.Data;

using DllFarmaciaSoft;

namespace Dll_IFacturacion.CargaDeParametros
{
    public partial class FrmCargaDeDocumentos : FrmBaseExt
    {

        string sFile_In = "";
        string sGUID = Guid.NewGuid().ToString();

        bool bValidandoInformacion = false;
        bool bSeEncontraronIndicencias = false;
        bool bActivarProceso = false;
        bool bErrorAlValidar = false;

        clsLeerExcelOpenOficce excel;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        OpenFileDialog openExcel = new OpenFileDialog();
        Thread thLoadFile;
        Thread thReadFile;
        Thread thGuardarInformacion;
        Thread thValidarInformacion;
        Thread thGeneraFolios;

        string sFormato = "###, ###, ###, ##0";
        string sFormato_INT = "###, ###, ###, ##0";

        TiposDePedidosCEDIS tpTipoDePedido = TiposDePedidosCEDIS.Ninguno;

        //clsCargarParametros Carga;
        //clsBulkCopy bulk;

        public FrmCargaDeDocumentos()
        {
            InitializeComponent();
        }

        private void FrmCargaDeDocumentos_Load(object sender, EventArgs e)
        {
            //bulk = new clsBulkCopy(General.DatosConexion);
            //Carga = new clsCargarParametros(General.DatosConexion, General.DatosApp, DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            
        }

        private void IniciaToolBar(bool Nuevo, bool Abrir, bool Ejecutar, bool Guardar, bool Validar, bool Procesar, bool Salir)
        {
            btnNuevo.Enabled = Nuevo;
            btnAbrir.Enabled = Abrir;
            btnEjecutar.Enabled = Ejecutar;
            btnGuardar.Enabled = Guardar;
            btnValidarDatos.Enabled = Validar;
            btnProcesarRemisiones.Enabled = Procesar;
            btnSalir.Enabled = Salir;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            sGUID = Guid.NewGuid().ToString();

            sFile_In = "";
            cboHojas.Clear();
            cboHojas.Add();
            
            Fg.IniciaControles();

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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //thGuardarInformacion = new Thread(Carga.GuardarInformacion_Pedidos);
            thGuardarInformacion.Name = "Guardar información seleccionada";
            thGuardarInformacion.Start();
        }

        private void btnValidarDatos_Click(object sender, EventArgs e)
        {
            tmValidacion.Enabled = true;
            tmValidacion.Interval = 1000;
            tmValidacion.Start();

            thValidarInformacion = new Thread(this.ValidarInformacion);
            thValidarInformacion.Name = "Validar informacion";
            thValidarInformacion.Start();
            System.Threading.Thread.Sleep(200);
        }

        private void btnProcesarRemisiones_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

        }


        #region Funciones y Procedimientos Privados
        

        public void bulk_RowsCopied(RowsCopiedEventArgs e)
        {
            //lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private static DataSet AgregarColumna(DataSet Datos, string Tabla, string Columna, string TipoDeDatos, string ValorDefault)
        {
            DataSet dts = Datos.Copy();
            DataTable dtConceptos;
            DataColumn dtColumnaNueva;
            clsLeer leer = new clsLeer();

            leer.DataSetClase = Datos;
            if (leer.ExisteTabla(Tabla))
            {
                dtConceptos = leer.Tabla(Tabla);
                if (!leer.ExisteTablaColumna(Tabla, Columna))
                {
                    dtColumnaNueva = new DataColumn(Columna, System.Type.GetType(string.Format("System.{0}", TipoDeDatos)));
                    dtColumnaNueva.DefaultValue = ValorDefault;
                    dtConceptos.Columns.Add(dtColumnaNueva);

                    ////leer.DataTableClase = dtConceptos;
                    ////while(leer.Leer())
                    ////{
                    ////    leer.GuardarDatos(Columna, ValorDefault);
                    ////}
                    ////dtConceptos = leer.DataTableClase;


                    dts.Tables.Remove(Tabla);
                    dts.Tables.Add(dtConceptos.Copy());
                }
            }

            return dts.Copy();
        }

        private void CargarArchivo()
        {
            string sHoja = "";
            bool bHabilitar = false;

            BloqueaHojas(true);
            MostrarEnProceso(true, 1);

            excel = new clsLeerExcelOpenOficce(sFile_In);
            excel.GetEstructura();

            cboHojas.Clear();
            cboHojas.Add();
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

            LeerHoja();

            BloqueaHojas(false);
            MostrarEnProceso(false, 2);
        }

        private bool LeerHoja()
        {
            bool bRegresa = false;

            IniciaToolBar(false, false, false, bRegresa, false, false, false);
            excel.LeerHoja(cboHojas.Data);

            // btnGuardar.Enabled = bRegresa;
            IniciaToolBar(true, true, true, bRegresa, false, false, true);
            return bRegresa;
        }

        private void BloqueaHojas(bool Bloquear)
        {
            cboHojas.Enabled = !Bloquear;
        }

        private void ValidarInformacion()
        {
            int iTipo;

            bValidandoInformacion = true;
            bActivarProceso = false;
            bErrorAlValidar = false;
            clsLeer leerValidacion = new clsLeer();
            clsLeer leerRows = new clsLeer();
            leer = new clsLeer(ref cnn);
            DataSet dtsResultados = new DataSet();

            IniciaToolBar(false, false, false, false, false, bActivarProceso, false);
            BloqueaHojas(true);
            MostrarEnProceso(true, 4);

            //iTipo = rdoVenta.Checked ? 1 : 2;
            iTipo = (int)tpTipoDePedido;

            string sSql = string.Format("Exec sp_Proceso_Pedidos_Cedis__CargaMasiva_000_Validar_Datos_De_Entrada  " +
            " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Tipo = '{3}', @GUID = '{4}' ",
            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iTipo, sGUID);


            if (!leer.Exec(sSql))
            {
                bErrorAlValidar = true;
                bActivarProceso = !bActivarProceso;

                Error.GrabarError(leer, "ValidarInformacion()");
                General.msjError("Ocurrió un error al verificar el Pedido a integrar.");
            }
            else
            {
                leer.RenombrarTabla(1, "Resultados");

                leerValidacion.DataTableClase = leer.Tabla("Resultados");

                dtsResultados = leer.DataSetClase;
                dtsResultados.Tables.Remove("Resultados");
                leer.DataSetClase = dtsResultados;

                for (int i = 1; leerValidacion.Leer(); i++)
                {
                    leer.RenombrarTabla(i, leerValidacion.Campo("Descripcion"));

                    leerRows.DataTableClase = leer.Tabla(leerValidacion.Campo("Descripcion"));

                    if (!bActivarProceso)
                        bActivarProceso = leerRows.Registros > 0 ? true : false;

                }

            }


            bValidandoInformacion = false;
            bActivarProceso = !bActivarProceso;
            BloqueaHojas(false);
            MostrarEnProceso(false, 4);

        }

        private void MostrarEnProceso(bool Mostrar, int Proceso)
        {
            string sTituloProceso = "";

            if (Mostrar)
            {
                FrameProceso.Left = 116;
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
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
                sTituloProceso = "Integrando Pedidos ..... ";
            }

            FrameProceso.Text = sTituloProceso;

        }

        private void tmValidacion_Tick(object sender, EventArgs e)
        {
            tmValidacion.Stop();
            tmValidacion.Enabled = false;


            if (!bValidandoInformacion)
            {
                if (bActivarProceso)
                {
                    IniciaToolBar(true, false, false, false, false, true, true);
                }
                else
                {
                    IniciaToolBar(true, false, false, false, true, false, true);
                    if (!bErrorAlValidar)
                    {
                        DllFarmaciaSoft.Inventario.FrmIncidencias f = new DllFarmaciaSoft.Inventario.FrmIncidencias(leer.DataSetClase);
                        f.ShowDialog();
                    }
                }
            }
            else
            {
                tmValidacion.Enabled = true;
                tmValidacion.Start();
            }
        }
        #endregion Funciones y Procedimientos Privados 
    }
}
