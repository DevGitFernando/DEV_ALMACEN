using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Reportes;


namespace DllAdministracion.Reportes
{
    public partial class FrmProductosCompras : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        clsDatosCliente DatosCliente;
        wsAdministracion.wsCnnOficinaCentral conexionWeb;
        DataSet dtsEstados = new DataSet();
        DataSet dtsFarmacias = new DataSet();
        DataSet dtsReporte = new DataSet();

        public FrmProductosCompras()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            leer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnAdministracion.Modulo, this.Name, GnAdministracion.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnAdministracion.Modulo, this.Name, GnAdministracion.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnAdministracion.Modulo, GnAdministracion.Version, this.Name);

            DatosCliente = new clsDatosCliente(GnAdministracion.DatosApp, this.Name, "");
            conexionWeb = new DllAdministracion.wsAdministracion.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;

            LlenarReportes();
            LlenarEmpresas();
            ObtenerFarmacias();
        }

        private void FrmProductosCompras_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(true);
            IniciaToolBar(true, true, false);
            FrameListaReportes.Enabled = false;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sFarmacia = cboFarmacias.Data;

            cboFarmacias.Enabled = false;
            dtpFechaInicial.Enabled = false;
            dtpFechaFinal.Enabled = false;
            if (sFarmacia == "0")
            {
                sFarmacia = "";
            }

            string sSql = string.Format(" Exec spp_ADMI_Impresion_Productos_Compras_Principal '{0}', '{1}', '{2}', '{3}', '{4}' " + 
                "Select Top 1 * From tmpADMI_Productos_Compras_Estado(NoLock) ",
                cboEmpresas.Data, cboEstados.Data, sFarmacia, 
                General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-") );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de existencias.");
            }
            else
            {
                if (leer.Leer())
                {
                    IniciaToolBar(true, false, true);
                    dtsReporte = leer.DataSetClase;//linea de prueba
                    FrameListaReportes.Enabled = true;
                }
                else
                {
                    General.msjUser("No se encontro información para mostrar.");
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //Exportar();
            ImprimirInformacion();
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

        private void ObtenerFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            cboFarmacias.SelectedIndex = 0;

            dtsFarmacias = Consultas.Farmacias("", "", "ObtenerFarmacias()");
        }

        private void LlenarFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            if (cboEstados.SelectedIndex != 0)
            {
                try
                {
                    cboFarmacias.Add(dtsFarmacias.Tables[0].Select(string.Format("IdEstado = '{0}'", cboEstados.Data)), true, "IdFarmacia", "NombreFarmacia");
                }
                catch { }
            }
            cboFarmacias.SelectedIndex = 0;
        }

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEmpresas.SelectedIndex != 0)
            {
                cboEmpresas.Enabled = false;
                cboEstados.Enabled = true;
                LlenarEstados();
            } 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;
                cboFarmacias.Enabled = true;
                LlenarFarmacias();
            }
        }

        private void Exportar()
        {
            if (dtsReporte != null)
            {
                if (cboReporte.Data == "0")
                {
                    General.msjUser("Seleccione un Reporte para impresión");
                    cboReporte.Focus();
                }
                else
                {
                    int iNumeroTabla = int.Parse(cboReporte.Data);
                    DataTable dtResultado = dtsReporte.Tables[iNumeroTabla];
                    DataTable dtImpresion = dtsReporte.Tables[iNumeroTabla].Clone();
                    string sFiltro = "";
                    string sNombreArchivo = "Movimientos Productos " + General.FechaSistemaFecha;

                    //Se aplica el filtro.
                    foreach (DataRow dtRow in dtResultado.Select(sFiltro))
                    {
                        dtImpresion.Rows.Add(dtRow.ItemArray);
                    }

                    if (dtImpresion.Rows.Count > 0)
                    {
                        //dtsReporte.Tables.Add(dtTabla1.Copy());
                        clsExportarExcel Exportar = new clsExportarExcel();
                        Exportar.Exportar(dtImpresion, sNombreArchivo);
                    }
                }
            }
        }

        private void LlenarReportes()
        {
            cboReporte.Add("0", "<< Seleccione >>");
            cboReporte.Add("Administracion_Productos_Compras", "Productos - Compras");
            cboReporte.Add("Administracion_Productos_Compras_Porcentajes", "Productos - Compras Porcentajes");
            cboReporte.Add("Administracion_Productos_Compras_Proveedor", "Productos - Compras por Proveedor");
        }

        private void ImprimirInformacion()
        {
            bool bRegresa = false;
            string sReporte = cboReporte.Data + ".rpt";

            if (ValidarImpresion())
            {
                DatosCliente.Funcion = "ImprimirCompra()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                byte[] btReporte = null;

                //myRpt.RutaReporte = @"C:\SII_OFICINA_CENTRAL\REPORTES"; //Linea de Prueba.
                myRpt.RutaReporte = GnAdministracion.RutaReportes;
                myRpt.NombreReporte = sReporte;

                if (General.ImpresionViaWeb)
                {
                    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                    DataSet datosC = DatosCliente.DatosCliente();

                    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                }
                else
                {
                    myRpt.CargarReporte(true);
                    bRegresa = !myRpt.ErrorAlGenerar;
                }


                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private bool ValidarImpresion()
        {
            bool bRegresa = true;

            if (cboReporte.Data == "0")
            {
                bRegresa = false;
                General.msjUser("Seleccione un Reporte para impresión");
                cboReporte.Focus();
            }

            return bRegresa;
        }
        #endregion Funciones y Eventos

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        

    } //Llaves de la clase
}
