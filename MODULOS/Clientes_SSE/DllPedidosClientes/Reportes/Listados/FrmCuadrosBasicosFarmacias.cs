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

namespace DllPedidosClientes.Reportes.Listados
{
    public partial class FrmCuadrosBasicosFarmacias : FrmBaseExt
    {
        clsConsultas query;
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente DatosCliente;
        clsLeer myLeer;
        clsListView list;
        DataSet dtsResultados;
        //clsExportarExcelPlantilla xpExcel;
        clsConsultas Consultas;
        clsAyudas Ayudas;

        DataSet dtsFarmacias = new DataSet();
        string sIdEstado = DtGeneralPedidos.EstadoConectado;

        // Se declara el objeto de la clase de Auditoria
        clsAuditoria auditoria;

        private enum cols
        {
            Ninguno = 0, ClaveSSA = 2, Descripcion = 3, Presentacion = 4
        }

        public FrmCuadrosBasicosFarmacias()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref ConexionLocal);
            query = new clsConsultas(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name, true);
            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "");

            Consultas = new clsConsultas(General.DatosConexion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);
            Ayudas = new clsAyudas(General.Url, General.ArchivoIni, DtGeneralPedidos.DatosApp, this.Name, false);

            // Se crea la instancia del objeto de la clase de Auditoria
            auditoria = new clsAuditoria(General.Url, General.ArchivoIni, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada,
                DtGeneralPedidos.IdPersonal, DtGeneralPedidos.IdSesion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name);

            list = new clsListView(lstClaves);
            list.PermitirAjusteDeColumnas = true;
            ObtenerFarmacias();
            CargarJurisdicciones();
        }

        private void FrmCuadrosBasicosFarmacias_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            list.Limpiar();
            lblClaves.Text = "0";
            cboJurisdicciones.SelectedIndex = 0;
            cboFarmacias.SelectedIndex = 0;
            IniciarToolBar(true, false);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format(
                    //"Select Row_Number() Over(Order By DescripcionClave) As Consecutivo, 'Clave SSA' = ClaveSSA, 'Descripción' =  DescripcionClave, 'Presentación' = Presentacion " +
                    "Select 'Clave SSA' = ClaveSSA, 'Descripción' =  DescripcionClave, 'Presentación' = Presentacion " +
                    "From vw_CB_CuadroBasico_Claves CB (NoLock) " +
                    "Where StatusMiembro = 'A' and StatusClave = 'A' and CB.IdEstado = {0} And IdNivel = {1} " +
                    "Group by ClaveSSA, DescripcionClave, Presentacion " +
                    "Order by DescripcionClave ",
                    DtGeneralPedidos.EstadoConectado, cboFarmacias.Data);

            list.Limpiar();
            dtsResultados = new DataSet();

            if( ValidarDatos())
            {
                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "btnEjecutar_Click");
                    General.msjError("Ocurrió un error al obtener la lista de Claves del Cuadro Basico.");
                }
                else
                {
                    if (myLeer.Leer())
                    {
                        dtsResultados = myLeer.DataSetClase;
                        lblClaves.Text = Convert.ToString(myLeer.Registros);
                        list.CargarDatos(myLeer.DataSetClase, true, true);
                        list.AnchoColumna((int)cols.Descripcion - 1, 750);
                        IniciarToolBar(false, true);
                        lstClaves.Focus();
                    }
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            /*
            // DllTransferenciaSoft.Properties.Resources 
            bool bGenerar = false;
            clsLeer leerToExcel = new clsLeer();

            leerToExcel.DataSetClase = dtsResultados;

            bGenerar = GnPlantillas.GenerarPlantilla(ListaPlantillas.CuadrosBasicos_Farmacias, "CTE_REG_Cuadros_Basicos_Farmacias");

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
                    xpExcel.Agregar("Reporte de Claves SSA del Cuadro Basico de " + cboFarmacias.Text, 3, 2);
                    xpExcel.Agregar("Fecha de reporte : " + General.FechaSistema.ToString(), 6, 2);

                    leerToExcel.RegistroActual = 1;
                    while (leerToExcel.Leer())
                    {
                        xpExcel.Agregar(leerToExcel.Campo("Clave SSA"), iRow, (int)cols.ClaveSSA);
                        xpExcel.Agregar(leerToExcel.Campo("Descripción"), iRow, (int)cols.Descripcion);
                        xpExcel.Agregar(leerToExcel.Campo("Presentación"), iRow, (int)cols.Presentacion);
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
        private void IniciarToolBar(bool Ejecutar, bool Exportar)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnExportar.Enabled = Exportar;

        }
        private void CargarJurisdicciones()
        {
            if (cboJurisdicciones.NumeroDeItems == 0)
            {
                cboJurisdicciones.Clear();
                cboJurisdicciones.Add("0", "<< Seleccione >>");

                cboJurisdicciones.Add(DtGeneralPedidos.Jurisdiscciones, true, "IdJurisdiccion", "NombreJurisdiccion");
            }
            cboJurisdicciones.SelectedIndex = 0;
        }

        private void ObtenerFarmacias()
        {
            string sSql = string.Format(
                    " Select F.IdJurisdiccion, M.* " +
                    " From vw_CB_NivelesAtencion_Miembros M(NoLock) " + 
                    " Inner Join CatFarmacias F(NoLock) On ( M.IdEstado = F.IdEstado And M.IdFarmacia = F.IdFarmacia ) " + 
                    " Where M.StatusMiembro = 'A' And F.Status = 'A' And M.IdEstado = '{0}' " +                    
                    " Order by Farmacia ", sIdEstado);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "ObtenerFarmacias");
                General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
            }
            else
            {
                if (myLeer.Leer())
                {
                    dtsFarmacias = new DataSet();
                    dtsFarmacias = myLeer.DataSetClase;
                    cboFarmacias.Clear();
                    cboFarmacias.Add("0", "<< Seleccione >>");
                }
            }
            
        }
        //private void ObtenerFarmacias2()
        //{
        //    dtsFarmacias = new DataSet();
        //    dtsFarmacias = Consultas.Farmacias(sIdEstado, "ObtenerFarmacias");
        //    cboFarmacias.Clear();
        //    cboFarmacias.Add("0", "<< TODAS LAS FARMACIAS >>");
        //}

        private void CargarFarmacias()
        {
            string sFiltro = string.Format("IdJurisdiccion = '{0}' ", cboJurisdicciones.Data);

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");

            try
            {
                cboFarmacias.Add(dtsFarmacias.Tables[0].Select(sFiltro, "Farmacia"), true, "IdNivel", "Farmacia");
            }
            catch { }

            cboFarmacias.SelectedIndex = 0;
        }
        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            list.Limpiar();
            CargarFarmacias();
            IniciarToolBar(true, false);
            lblClaves.Text = "0";

            //cboFarmacias.Enabled = true;
            //if (cboJurisdicciones.SelectedIndex == 0)
            //{
            //    cboFarmacias.Enabled = false;
            //}
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            list.Limpiar();
            IniciarToolBar(true, false);
            lblClaves.Text = "0";
        }

        private bool ValidarDatos()
        {
            bool bRegresa = true;

            if (cboJurisdicciones.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione una Jurisdicción por favor");
                cboJurisdicciones.Focus();                
            }

            if (bRegresa && cboFarmacias.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione una Farmacia por favor");
                cboFarmacias.Focus();
            }

            return bRegresa;
        }
        #endregion Funciones 

        
    }
}
