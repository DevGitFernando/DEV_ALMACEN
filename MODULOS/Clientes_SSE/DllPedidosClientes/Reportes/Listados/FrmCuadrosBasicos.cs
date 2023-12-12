using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
//using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

namespace DllPedidosClientes.Reportes
{
    public partial class FrmCuadrosBasicos : FrmBaseExt
    {
        clsConsultas query;
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente DatosCliente;
        clsLeer myLeer;
        clsListView list;
        DataSet dtsResultados;
        //clsExportarExcelPlantilla xpExcel;

        private enum cols
        {
            Ninguno = 0, Consecutivo = 1, ClaveSSA = 2, Descripcion = 3, Presentacion = 4
        }

        public FrmCuadrosBasicos()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref ConexionLocal);
            query = new clsConsultas(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name, true);
            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "");

            list = new clsListView(lstClaves);
            list.PermitirAjusteDeColumnas = true;
            LlenarReportes();

        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            list.Limpiar();
            lblClaves.Text = "0";
            cboReporte.SelectedIndex = 0;
            IniciarToolBar(true, false);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format(
                    "Select Row_Number() Over(Order By DescripcionClave) As Consecutivo, 'Clave SSA' = ClaveSSA, 'Descripción' =  DescripcionClave, 'Presentación' = Presentacion " +
                    "From vw_CB_CuadroBasico_Claves CB (NoLock) " +
                    "Where StatusMiembro = 'A' and StatusClave = 'A' and CB.IdEstado = {0} And IdNivel = {1} " +
                    "Group by ClaveSSA, DescripcionClave, Presentacion " +
                    "Order by DescripcionClave ",
                    DtGeneralPedidos.EstadoConectado, cboReporte.Data);

            list.Limpiar();
            dtsResultados = new DataSet();

            if (cboReporte.Data == "0")
            {
                General.msjAviso("Seleccione un Cuadro Basico por favor.");
            }
            else
            {
                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "btnEjecutar_Click");
                    General.msjError("Ocurrió un error al obtener la lista de Claves.");
                }
                else
                {
                    if (myLeer.Leer())
                    {
                        dtsResultados = myLeer.DataSetClase;
                        lblClaves.Text = Convert.ToString(myLeer.Registros);
                        list.CargarDatos(myLeer.DataSetClase, true, true);
                        list.AnchoColumna((int)cols.Descripcion, 500);
                        IniciarToolBar(false, true);
                    }
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            /*
            // DllTransferenciaSoft.Properties.Resources 
            bool bGenerar = false;
            clsLeer leerToExcel = new clsLeer();

            leerToExcel.DataSetClase = dtsResultados;

            bGenerar = GnPlantillas.GenerarPlantilla(ListaPlantillas.CuadrosBasicos, "CTE_REG_Cuadros_Basicos");

            if (bGenerar)
            {
                xpExcel = new clsExportarExcelPlantilla(GnPlantillas.Documento);
                xpExcel.AgregarMarcaDeTiempo = true;

                int iRow = 9;
                // int iRowInicial = 9; 

                if (xpExcel.PrepararPlantilla())
                {
                    xpExcel.GeneraExcel();
                    leerToExcel.Leer();
                    xpExcel.Agregar("Reporte de Claves SSA del Cuadro Basico: " + cboReporte.Text, 3, 2);
                    xpExcel.Agregar("Fecha de reporte : " + General.FechaSistema.ToString(), 6, 2);

                    leerToExcel.RegistroActual = 1;
                    while (leerToExcel.Leer())
                    {
                        xpExcel.Agregar(leerToExcel.CampoInt("Consecutivo"), iRow, 2);
                        xpExcel.Agregar(leerToExcel.Campo("Clave SSA"), iRow, 3);
                        xpExcel.Agregar(leerToExcel.Campo("Descripción"), iRow, 4);
                        xpExcel.Agregar(leerToExcel.Campo("Presentación"), iRow, 5);
                        iRow++;
                    }

                    // Finalizar el Proceso 
                    xpExcel.CerrarDocumento();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }
                }
            }
            */
        } 
        #endregion Botones 

        #region Funciones 
        private void IniciarToolBar(bool Ejecutar, bool Imprimir)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }

        private void LlenarReportes()
        {
            string sSql = string.Format( "Select * From CFG_CB_NivelesAtencion(NoLock) Where IdEstado = '{0}' And Status = 'A' Order By IdNivel",
                    DtGeneralPedidos.EstadoConectado);

            cboReporte.Clear();
            cboReporte.Add("0", "<< Seleccione >>");
            cboReporte.SelectedIndex = 0;

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al obtener la lista de Claves.");
            }
            else
            {
                if (myLeer.Leer())
                {
                    cboReporte.Add(myLeer.DataSetClase, true, "IdNivel", "Descripcion");
                }
            }
        }

        private void cboReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            list.Limpiar();
            IniciarToolBar(true, false);
            lblClaves.Text = "0";
        }
        #endregion Funciones 

        

        

    }
}
