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
//using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllPedidosClientes;

namespace DllPedidosClientes.Reportes
{
    internal partial class FrmClavesSurtimiento : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        // clsListView lst;
        clsGrid grid; 

        clsDatosCliente DatosCliente; 
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;
        clsDatosConexion datosCnn; 

        //clsExportarExcelPlantilla xpExcel; 

        string sEncabezado = "";
        int iTipo = 0;
        string sIdEstado = "";
        string sIdFarmacia = "";

        string sTituloEmpresa = "";
        string sTituloFarmacia = "";
        string sTituloReporte = "";
        string sTituloFecha = "";
        string sFormato = "##,###,###,##0"; 

        public FrmClavesSurtimiento(string Encabezado, int Tipo, string IdEstado, string IdFarmacia, 
            clsDatosConexion DatosDeConexion, 
            string TituloEmpresa, string TituloFarmacia, string TituloReporte, string TituloFecha)
        {
            InitializeComponent();

            sIdEstado = IdEstado;
            sIdFarmacia = IdFarmacia;
            sEncabezado = Encabezado;
            iTipo = Tipo;
            datosCnn = DatosDeConexion; 


            sTituloEmpresa = TituloEmpresa;
            sTituloFarmacia = TituloFarmacia;
            sTituloReporte = TituloReporte;
            sTituloFecha = TituloFecha; 

            /// conexionWeb = Conexion; 
            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name);             

            grid = new clsGrid(ref grdClaves, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow; 

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "FrmClavesSurtimiento");

            conexionWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            conexionWeb.Url = General.Url;
        }

        private void FrmClavesSurtimiento_Load(object sender, EventArgs e)
        {
            lblClaves.Text = "0";
            lblCantidad.Text = "0"; 

            CargarInformacion(); 
        }

        private void CargarInformacion()
        {
            // string sTablaFarmacia = "CTE_FarmaciasProcesar"; 
            string sFiltro = iTipo == 0 ? "" : string.Format(" Where Tipo = '{0}' ", iTipo) ; 

            string sSql = //// "Select Top 1 Empresa, Farmacia, Periodo, FechaReporte From Rpt_NivelDeAbasto (NoLock) \n \n" + 
                string.Format(" Select " + 
                " ClaveSSA, 'Descripción Clave' = DescripcionClave, 'Cantidad dispensada' = CantidadSurtida " +
                " From Rpt_NivelDeAbasto_ClavesDispensadas (Nolock)  {0}   " +
                " Order by DescripcionClave ", sFiltro);

            sSql =
                string.Format(" Select ClaveSSA, DescripcionClave, CantidadSurtida " +
                " From Rpt_NivelDeAbasto_ClavesDispensadas (Nolock)  {0}   " +
                " Order by DescripcionClave ", sFiltro);

            if (iTipo == 0)
            {
                sSql = 
                    string.Format(" Select ClaveSSA, DescripcionClave, sum(CantidadSurtida) as CantidadSurtida " +
                    " From Rpt_NivelDeAbasto_ClavesDispensadas (Nolock)  {0}   " +
                    " Group by IdClaveSSA, ClaveSSA, DescripcionClave " + 
                    " Order by DescripcionClave ", sFiltro); 
            }

            //lst.LimpiarItems();
            grid.Limpiar();
            lblClaves.Text = grid.Rows.ToString();
            lblCantidad.Text = grid.TotalizarColumna(3).ToString(); 

            ////try
            ////{
            ////    leer.Reset(); 
            ////    leer.DataSetClase = conexionWeb.EjecutarSentencia(sIdEstado, sIdFarmacia, sSql, "reporte", sTablaFarmacia);
            ////}
            ////catch { } 

            leer.Reset();
            leer.DataSetClase = GetInformacion(sSql); 
            if (leer.SeEncontraronErrores()) 
            {
                Error.GrabarError(leer, "CargarInformacion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad solicitada, intente de nuevo."); 
            }
            else
            {
                if (leer.Leer())
                {
                    // lst.CargarDatos(leer.DataSetClase, true, true);
                    grid.LlenarGrid(leer.DataSetClase); 
                }
            }

            lblClaves.Text = grid.Rows.ToString();
            lblCantidad.Text = grid.TotalizarColumna(3).ToString(sFormato); 
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            /*
            // DllTransferenciaSoft.Properties.Resources 
            bool bGenerar = false;

            bGenerar = GnPlantillas.GenerarPlantilla(ListaPlantillas.SurmientoClavesDispensada, "PLANTILLA_001");

            if (bGenerar)
            {
                xpExcel = new clsExportarExcelPlantilla(GnPlantillas.Documento);
                xpExcel.AgregarMarcaDeTiempo = true;

                // int iRenglon = 8;
                int iRow = 10;
                // string sColFormula = "I";

                if (xpExcel.PrepararPlantilla())
                {
                    xpExcel.GeneraExcel();

                    xpExcel.Agregar(sTituloEmpresa, 2, 2);
                    xpExcel.Agregar(sTituloFarmacia, 3, 2);
                    xpExcel.Agregar("REPORTE DE CLAVES DISPENSADAS DE " + sTituloReporte, 4, 2);

                    xpExcel.Agregar("Fecha de reporte : " + sTituloFecha, 7, 2);

                    leer.RegistroActual = 1;
                    while (leer.Leer())
                    {
                        xpExcel.Agregar(leer.Campo("ClaveSSA"), iRow, 2);
                        xpExcel.Agregar(leer.Campo("DescripcionClave"), iRow, 3);
                        xpExcel.Agregar(leer.Campo("CantidadSurtida"), iRow, 4);
                        iRow++;
                    }


                    //////// Anexar SUMA DE CANTIDADES 
                    //////sColFormula = string.Format("=SUMA(I{0}:I{1})", iRenglon, iRenglon_Final-1);
                    //////xpExcel.Agregar("TOTAL", iRenglon_Final, 8); 
                    //////xpExcel.Agregar(sColFormula, iRenglon_Final, 9);

                    // Finalizar el Proceso 
                    xpExcel.CerrarDocumento();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }
                    // General.msjUser("Exportacion finalizada."); 
                }
            }
            */
        }

        #region Conexiones
        public DataSet GetInformacion(string Cadena)
        {
            DataSet dts = new DataSet();
            // DtGeneralPedidos.TipoDeConexion = TipoDeConexion.Unidad_Directo; 

            switch (DtGeneralPedidos.TipoDeConexion)
            {
                case TipoDeConexion.Regional:
                    dts = GetInformacionRegional(Cadena);
                    break;

                case TipoDeConexion.Unidad:
                    dts = GetInformacionUnidad(Cadena);
                    break;

                case TipoDeConexion.Unidad_Directo:
                    dts = GetInformacionUnidad_Directo(Cadena);
                    break;

                default:
                    break;
            }

            return dts;
        }

        private DataSet GetInformacionRegional(string Cadena)
        {
            DataSet dts = new DataSet();

            leer.Exec(Cadena);
            dts = leer.DataSetClase;

            return dts;
        }

        private DataSet GetInformacionUnidad(string Cadena)
        {
            DataSet dts = new DataSet();

            try
            {
                conexionWeb.Url = General.Url;
                dts = conexionWeb.EjecutarSentencia(sIdEstado, sIdFarmacia, Cadena, "reporte", "");
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source; 
            }

            return dts;
        }

        private DataSet GetInformacionUnidad_Directo(string Cadena)
        {
            DataSet dts = new DataSet();

            //if (validarDatosDeConexion())
            {
                clsConexionSQL cnnRemota = new clsConexionSQL(datosCnn);
                cnnRemota.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
                cnnRemota.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

                clsLeer leerDatos = new clsLeer(ref cnnRemota);

                leerDatos.Exec(Cadena);
                dts = leerDatos.DataSetClase;
            }

            return dts;
        }
        #endregion Conexiones
    }
}

