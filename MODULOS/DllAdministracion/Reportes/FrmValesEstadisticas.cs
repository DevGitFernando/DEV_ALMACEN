using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DllFarmaciaSoft; 

using SC_SolutionsSystem; 
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.ExportarDatos;


namespace DllAdministracion.Reportes
{
    public partial class FrmValesEstadisticas : FrmBaseExt 
    {
        #region Enums
        private enum Cols_Concentrado
        {
            Ninguna = 0,
            ValesEmitidos = 2, ValesRegistrados = 3, PiezasEmitidas = 4, PiezasRegistradas = 5, ImporteRegistrado = 6
        }

        private enum Cols_Farmacia
        {
            Ninguna = 0,
            IdFarmacia = 2, Farmacia = 3, ValesEmitidos = 4, ValesRegistrados = 5, PiezasEmitidas = 6, PiezasRegistradas = 7,
            ImporteRegistrado = 8
        }

        private enum Cols_Clave
        {
            Ninguna = 0,
            ClaveSSA = 2, DescripcionSal = 3, ValesEmitidos = 4, ValesRegistrados = 5, PiezasEmitidas = 6, PiezasRegistradas = 7,
            ImporteRegistrado = 8
        }

        private enum Cols_Perdidas
        {
            Ninguna = 0,
            ClaveSSA = 2, DescripcionSal = 3, PrecioLicitacion = 4, CostoMinimo = 5, CostoMaximo = 6
        }

        private enum Cols_Proveedores
        {
            Ninguna = 0,
            IdProveedor = 2, Proveedor = 3, ValesRegistrados = 4, PiezasRegistradas = 5, ImporteRegistrado = 6
        }

        #endregion Enums

        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leerColumnas;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        clsDatosCliente DatosCliente;
        wsAdministracion.wsCnnOficinaCentral conexionWeb;
        DataSet dtsEstados = new DataSet();        
        DataSet dtsConcentrado = new DataSet();
        DataSet dtsFarmacias = new DataSet();
        DataSet dtsProveedores = new DataSet();
        DataSet dtsClaves = new DataSet();
        DataSet dtsPerdidas = new DataSet();
        clsListView lstConcentrado, lstFarmacias, lstClaves, lstPerdidas, lstProveedores;
        clsExportarExcelPlantilla xpExcel;
        int iAñoReporte = 0;
        string sMesReporte = "";

        Color colorBack_01 = Color.LightBlue;
        Color colorBack_02 = Color.Lavender; 

        public FrmValesEstadisticas()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            leer = new clsLeer(ref ConexionLocal);
            leerColumnas = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnAdministracion.Modulo, this.Name, GnAdministracion.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnAdministracion.Modulo, this.Name, GnAdministracion.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnAdministracion.Modulo, GnAdministracion.Version, this.Name);

            DatosCliente = new clsDatosCliente(GnAdministracion.DatosApp, this.Name, "");
            conexionWeb = new DllAdministracion.wsAdministracion.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;

            lstConcentrado = new clsListView(lstConcentrado_Vales);
            lstConcentrado.OrdenarColumnas = false;

            lstFarmacias = new clsListView(lstFarmacias_Vales);
            lstFarmacias.OrdenarColumnas = false;

            lstClaves = new clsListView(lstClaves_Vales);
            lstClaves.OrdenarColumnas = false;

            lstPerdidas = new clsListView(lstPerdidas_Vales);
            lstPerdidas.OrdenarColumnas = false;

            lstProveedores = new clsListView(lstProveedores_Vales);
            lstProveedores.OrdenarColumnas = false;

            LlenarEmpresas();
        }

        private void FrmValesEstadisticas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(true);
            tabConcentrado.SelectTab(0);
            numBusqueda.Value = numBusqueda.Minimum;

            lstConcentrado.Limpiar();
            lstFarmacias.Limpiar();
            lstClaves.Limpiar();
            lstPerdidas.Limpiar();
            lstProveedores.Limpiar();

            dtsConcentrado = new DataSet();
            dtsFarmacias = new DataSet();
            dtsClaves = new DataSet();
            dtsPerdidas = new DataSet();
            dtsProveedores = new DataSet();
            
            IniciaToolBar(true, true, false);
            ActivarControles(true);            
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            int iMes = dtpFecha.Value.Month;
            int iAño = dtpFecha.Value.Year;

            iAñoReporte = iAño;
            sMesReporte = General.FechaNombreMes(dtpFecha.Value);

            if (ValidaControles())
            {
                ActivarControles(false);
                dtpFecha.Enabled = false;

                ObtenerInformacion(iAño, iMes);
            }
        }

        #endregion Botones

        #region Funciones y Eventos 
        private void IniciaToolBar(bool Nuevo, bool Ejecutar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }

        private void LlenarEmpresas()
        {
            cboEmpresas.Clear();
            cboEmpresas.Add();

            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            string sSql = "Select Distinct IdEmpresa, NombreEmpresa From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEmpresas()");
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
            else
            {
                cboEmpresas.Add(leer.DataSetClase, true, "IdEmpresa", "NombreEmpresa");
                sSql = "Select distinct IdEstado, NombreEstado, IdEmpresa, StatusEdo From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "LlenarEmpresas()");
                    General.msjError("Ocurrió un error al obtener la lista de Estados por Empresas.");
                }
                else
                {
                    dtsEstados = leer.DataSetClase;
                }

            }
            cboEmpresas.SelectedIndex = 0;
        }

        private void LlenarEstados()
        {
            string sFiltro = string.Format(" IdEmpresa = '{0}' and StatusEdo = '{1}' ", cboEmpresas.Data, "A");
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.Add(dtsEstados.Tables[0].Select(sFiltro), true, "IdEstado", "NombreEstado");
            cboEstados.SelectedIndex = 0;
        }

        private void ObtenerInformacion( int iAño, int iMes)
        {
            string sSql = string.Format("Exec spp_ADMI_Vales_Estadisticas_Datos '{0}', '{1}', {2}, {3}, {4} ",
                                        cboEmpresas.Data, cboEstados.Data, iAño, iMes, numBusqueda.Value.ToString());

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener el Concentrado de los vales.");
            }
            else
            {
                if (leer.Leer())
                {
                    IniciaToolBar(true, false, true);

                    //Se obtienen las tablas con los resultados  
                    dtsConcentrado.Tables.Add(leer.DataSetClase.Tables[0].Copy());//Tabla Concentrado
                    dtsFarmacias.Tables.Add(leer.DataSetClase.Tables[1].Copy());//Tabla Farmacias
                    dtsClaves.Tables.Add(leer.DataSetClase.Tables[2].Copy());//Tabla Claves
                    dtsPerdidas.Tables.Add(leer.DataSetClase.Tables[3].Copy());//Tabla Perdidas
                    dtsProveedores.Tables.Add(leer.DataSetClase.Tables[4].Copy());//Tabla Proveedores

                    //Se obtiene el Concentrado del Mes.                    
                    lstConcentrado.CargarDatos(dtsConcentrado, true, true);
                    lstConcentrado.AnchoColumna((int)Cols_Concentrado.ImporteRegistrado - 1, 150);
                    lstConcentrado.AlternarColorRenglones(colorBack_01, colorBack_02);

                    //Se obtiene el Concentrado por Farmacia
                    lstFarmacias.CargarDatos(dtsFarmacias, true, true);
                    lstFarmacias.AnchoColumna((int)Cols_Farmacia.ImporteRegistrado - 1, 150);
                    lstFarmacias.AlternarColorRenglones(colorBack_01, colorBack_02);

                    //Se obtiene el Concentrado por Clave
                    lstClaves.CargarDatos(dtsClaves, true, true);
                    lstClaves.AnchoColumna((int)Cols_Clave.DescripcionSal - 1, 300);
                    lstClaves.AnchoColumna((int)Cols_Clave.ImporteRegistrado - 1, 150);
                    lstClaves.AlternarColorRenglones(colorBack_01, colorBack_02);

                    //Se obtiene el Concentrado de Perdidas
                    lstPerdidas.CargarDatos(dtsPerdidas, true, true);
                    lstPerdidas.AnchoColumna((int)Cols_Perdidas.DescripcionSal - 1, 300);
                    lstPerdidas.AlternarColorRenglones(colorBack_01, colorBack_02);

                    //Se obtiene el Concentrado por Proveedor
                    lstProveedores.CargarDatos(dtsProveedores, true, true);
                    lstProveedores.AnchoColumna((int)Cols_Proveedores.ImporteRegistrado - 1, 150);
                    lstProveedores.AlternarColorRenglones(colorBack_01, colorBack_02);

                }
                else
                {
                    IniciaToolBar(true, true, false);
                    ActivarControles(true);
                    General.msjUser("No se encontro información para mostrar.");
                }
            }
        }

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEmpresas.SelectedIndex != 0)
            {
                LlenarEstados();
            } 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ActivarControles(bool Activar)
        {
            cboEmpresas.Enabled = Activar;
            cboEstados.Enabled = Activar;            
            dtpFecha.Enabled = Activar;
            numBusqueda.Enabled = Activar;
        }

        private bool ValidaControles()
        {
            bool bRegresa = true;

            if (cboEmpresas.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione una Empresa");
                cboEmpresas.Focus();
            }

            if (bRegresa && cboEstados.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione el Estado");
                cboEstados.Focus();
            }

            if (bRegresa && numBusqueda.Value == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione el Top de Busqueda ");
                numBusqueda.Focus();
            }

            return bRegresa;
        }

        #endregion Funciones y Eventos

        #region Impresion
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            GenerarExcel();
        } 

        private void GenerarExcel()
        {
            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\ADMIN_Vales_Estadisticas.xls";
            bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "ADMIN_Vales_Estadisticas.xls", DatosCliente);

            if (bRegresa)
            {
                xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                xpExcel.AgregarMarcaDeTiempo = true;

                this.Cursor = Cursors.Default;
                if (xpExcel.PrepararPlantilla())
                {
                    this.Cursor = Cursors.WaitCursor;

                    ExportarConcentrado();
                    ExportarFarmacias();
                    ExportarClaves();
                    ExportarPerdidas();
                    ExportarProveedores();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }
                }

                this.Cursor = Cursors.Default;
            }
        }

        private void ExportarConcentrado()
        {
            int iHoja = 1, iRenglon = 9;
            string sPeriodo = "", sNombreColumna = "";

            leer.DataSetClase = dtsConcentrado;
            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(cboEmpresas.Text, 2, 2);
            xpExcel.Agregar(cboEstados.Text, 3, 2);

            sPeriodo = string.Format("Reporte Concentrado de Vales del mes de {0} del {1} ", sMesReporte, iAñoReporte);
            xpExcel.Agregar(sPeriodo, 4, 2);

            xpExcel.Agregar("Fecha de impresión : " + leer.CampoFecha("FechaImpresion").ToString(), 6, 2);

            while (leer.Leer())
            {
                xpExcel.Agregar(leer.Campo("Vales Emitidos"), iRenglon, (int)Cols_Concentrado.ValesEmitidos);
                xpExcel.Agregar(leer.Campo("Vales Registrados"), iRenglon, (int)Cols_Concentrado.ValesRegistrados);
                xpExcel.Agregar(leer.Campo("Piezas Emitidas"), iRenglon, (int)Cols_Concentrado.PiezasEmitidas);
                xpExcel.Agregar(leer.Campo("Piezas Registradas"), iRenglon, (int)Cols_Concentrado.PiezasRegistradas);
                xpExcel.Agregar(leer.Campo("Importe Registrado"), iRenglon, (int)Cols_Concentrado.ImporteRegistrado);

                iRenglon++;
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();
        }

        private void ExportarFarmacias()
        {
            int iHoja = 2, iRenglon = 9;
            string sPeriodo = "", sNombreColumna = "";

            leer.DataSetClase = dtsFarmacias;
            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(cboEmpresas.Text, 2, 2);
            xpExcel.Agregar(cboEstados.Text, 3, 2);

            sPeriodo = string.Format("Reporte de Farmacias que Emitieron mas Vales en el mes de {0} del {1} ", sMesReporte, iAñoReporte);
            xpExcel.Agregar(sPeriodo, 4, 2);

            //// xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            xpExcel.Agregar("Fecha de impresión : " + leer.CampoFecha("FechaImpresion").ToString(), 6, 2);

            while (leer.Leer())
            {
                xpExcel.Agregar(leer.Campo("IdFarmacia"), iRenglon, (int)Cols_Farmacia.IdFarmacia);
                xpExcel.Agregar(leer.Campo("Farmacia"), iRenglon, (int)Cols_Farmacia.Farmacia);
                xpExcel.Agregar(leer.Campo("Vales Emitidos"), iRenglon, (int)Cols_Farmacia.ValesEmitidos);
                xpExcel.Agregar(leer.Campo("Vales Registrados"), iRenglon, (int)Cols_Farmacia.ValesRegistrados);
                xpExcel.Agregar(leer.Campo("Piezas Emitidas"), iRenglon, (int)Cols_Farmacia.PiezasEmitidas);
                xpExcel.Agregar(leer.Campo("Piezas Registradas"), iRenglon, (int)Cols_Farmacia.PiezasRegistradas);
                xpExcel.Agregar(leer.Campo("Importe Registrado"), iRenglon, (int)Cols_Farmacia.ImporteRegistrado);

                iRenglon++;
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();
        }

        private void ExportarClaves()
        {
            int iHoja = 3, iRenglon = 9;
            string sPeriodo = "", sNombreColumna = "";

            leer.DataSetClase = dtsClaves;
            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(cboEmpresas.Text, 2, 2);
            xpExcel.Agregar(cboEstados.Text, 3, 2);

            sPeriodo = string.Format("Reporte de Claves que Emitieron mas Vales en el mes de {0} del {1} ", sMesReporte, iAñoReporte);
            xpExcel.Agregar(sPeriodo, 4, 2);

            //// xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            xpExcel.Agregar("Fecha de impresión : " + leer.CampoFecha("FechaImpresion").ToString(), 6, 2);

            while (leer.Leer())
            {
                xpExcel.Agregar(leer.Campo("ClaveSSA"), iRenglon, (int)Cols_Clave.ClaveSSA);
                xpExcel.Agregar(leer.Campo("Descripción Clave"), iRenglon, (int)Cols_Clave.DescripcionSal);
                xpExcel.Agregar(leer.Campo("Vales Emitidos"), iRenglon, (int)Cols_Clave.ValesEmitidos);
                xpExcel.Agregar(leer.Campo("Vales Registrados"), iRenglon, (int)Cols_Clave.ValesRegistrados);
                xpExcel.Agregar(leer.Campo("Piezas Emitidas"), iRenglon, (int)Cols_Clave.PiezasEmitidas);
                xpExcel.Agregar(leer.Campo("Piezas Registradas"), iRenglon, (int)Cols_Clave.PiezasRegistradas);
                xpExcel.Agregar(leer.Campo("Importe Registrado"), iRenglon, (int)Cols_Clave.ImporteRegistrado);

                iRenglon++;
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();
        }

        private void ExportarPerdidas()
        {
            int iHoja = 4, iRenglon = 9;
            string sPeriodo = "", sNombreColumna = "";

            leer.DataSetClase = dtsPerdidas;
            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(cboEmpresas.Text, 2, 2);
            xpExcel.Agregar(cboEstados.Text, 3, 2);

            sPeriodo = string.Format("Reporte de Perdidas en Claves Compradas por Vales del mes de {0} del {1} ", sMesReporte, iAñoReporte);
            xpExcel.Agregar(sPeriodo, 4, 2);

            //// xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            xpExcel.Agregar("Fecha de impresión : " + leer.CampoFecha("FechaImpresion").ToString(), 6, 2);

            while (leer.Leer())
            {
                xpExcel.Agregar(leer.Campo("ClaveSSA"), iRenglon, (int)Cols_Perdidas.ClaveSSA);
                xpExcel.Agregar(leer.Campo("Descripción Clave"), iRenglon, (int)Cols_Perdidas.DescripcionSal);
                xpExcel.Agregar(leer.Campo("Precio de Licitacion"), iRenglon, (int)Cols_Perdidas.PrecioLicitacion);
                xpExcel.Agregar(leer.Campo("Costo Minimo de Compra por Vale"), iRenglon, (int)Cols_Perdidas.CostoMinimo);
                xpExcel.Agregar(leer.Campo("Costo Maximo de Compra por Vale"), iRenglon, (int)Cols_Perdidas.CostoMaximo);

                iRenglon++;
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();
        }

        private void ExportarProveedores()
        {
            int iHoja = 5, iRenglon = 9;
            string sPeriodo = "", sNombreColumna = "";

            leer.DataSetClase = dtsProveedores;
            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(cboEmpresas.Text, 2, 2);
            xpExcel.Agregar(cboEstados.Text, 3, 2);

            sPeriodo = string.Format("Reporte de Proveedores que Emitieron mas Vales en el mes de {0} del {1} ", sMesReporte, iAñoReporte);
            xpExcel.Agregar(sPeriodo, 4, 2);

            //// xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            xpExcel.Agregar("Fecha de impresión : " + leer.CampoFecha("FechaImpresion").ToString(), 6, 2);

            while (leer.Leer())
            {
                xpExcel.Agregar(leer.Campo("IdProveedor"), iRenglon, (int)Cols_Proveedores.IdProveedor);
                xpExcel.Agregar(leer.Campo("Proveedor"), iRenglon, (int)Cols_Proveedores.Proveedor);
                xpExcel.Agregar(leer.Campo("Vales Registrados"), iRenglon, (int)Cols_Proveedores.ValesRegistrados);
                xpExcel.Agregar(leer.Campo("Piezas Registradas"), iRenglon, (int)Cols_Proveedores.PiezasRegistradas);
                xpExcel.Agregar(leer.Campo("Importe Registrado"), iRenglon, (int)Cols_Proveedores.ImporteRegistrado);

                iRenglon++;
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();
        }


        #endregion Impresion

    } //Llaves de la clase
}
