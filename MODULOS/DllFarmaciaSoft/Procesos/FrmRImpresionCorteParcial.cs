using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using DllFarmaciaSoft;

namespace DllFarmaciaSoft.Procesos
{
    public partial class FrmRImpresionCorteParcial : FrmBaseExt
    {
        clsLeer leer;
        clsDatosConexion DatosDeConexion;
        clsConexionSQL cnn;  // = new clsConexionSQL(General.DatosConexion); 
        clsConexionSQL cnnUnidad;

        // Manejo de reportes  
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        string sSqlFarmacias = "";
        string sUrl;
        string sHost = "";

        DataSet dtsEstados = new DataSet();

        clsLeer leerLocal;
        clsLeerWebExt leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        clsGrid grid; 

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;
        public bool bOpcionExterna = false; //Esta variable se utiliza para el cambio de cajero y corte del dia.
        public bool bCorteRealizado = false; //Esta variable se utiliza para el cambio de cajero y corte del dia.
        // string sObservaciones = "";
        // double fTotalCorteParcial = 0;
        // string sFormato = "#,###,###,##0.###0";

        ////// Mensaje para el Corte 
        // string sMsjNoEncontrado = "Usted no puede realizar el Corte Parcial debido a que ya ha realizado su Corte Parcial o No ha efectuado ninguna venta.";

        public FrmRImpresionCorteParcial()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            grid = new clsGrid(ref grdPersonalCortes, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            // dtpFechaInicial.MaxDate = GnFarmacia.FechaOperacionSistema; 
            
        }

        private void FrmRImpresionCorteParcial_Load(object sender, EventArgs e)
        {
            CargarEmpresas();            
            btnNuevo_Click(null, null); 
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnImprimir.Enabled = true;         
            grid.Limpiar(); 
            Fg.IniciaControles();
            dtpFechaInicial.Focus();

            cboEmpresas.Data = DtGeneral.EmpresaConectada;
            cboEstados.Data = DtGeneral.EstadoConectado;
            cboFarmacias.Data = DtGeneral.FarmaciaConectada; 

            if (!DtGeneral.EsAdministrador)
            {
                cboEmpresas.Enabled = false;
                cboEstados.Enabled = false;
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (validarDatosDeConexion())
            {
                cnnUnidad = new clsConexionSQL(DatosDeConexion);
                cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite300;

                leer = new clsLeer(ref cnnUnidad); 

                string sSql = string.Format(" Select C.IdPersonal, P.NombreCompleto as Nombre " +
                    " From CtlCortesParciales C (NoLock) " +
                    " Inner Join vw_Personal P (NoLock) On ( C.IdEstado = P.IdEstado and C.IdFarmacia = P.IdFarmacia and C.IdPersonal = P.IdPersonal ) " +
                    " Where C.IdEmpresa = '{0}' and C.IdEstado = '{1}' and C.IdFarmacia = '{2}' " +
                    " and Convert(varchar(10), C.FechaSistema, 120) = '{3}' and C.Status = 'C' ",
                    cboEmpresas.Data, cboEstados.Data, cboFarmacias.Data, General.FechaYMD(dtpFechaInicial.Value));

                grid.Limpiar();
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "btnEjecutar_Click");
                    General.msjError("Ocurrió un error al obtener la lista de Cortes a reeimprimir.");
                }
                else
                {
                    if (!leer.Leer())
                    {
                        General.msjUser("No se encontro información para la fecha solicitada, verifique.");
                    }
                    else
                    {
                        dtpFechaInicial.Enabled = false;
                        grid.LlenarGrid(leer.DataSetClase);
                    }
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {            
            if (validarSeleccion())
            {
                btnImprimir.Enabled = false;
                ImprimirCorteParcial();
            }            
        }
        #endregion Botones

        private bool validarSeleccion()
        {
            bool bRegresa = true;

            sPersonal = grid.GetValue(grid.ActiveRow, 1);
            if (sPersonal == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Personal inválida, verifique."); 
            }

            return bRegresa; 
        }

        private void ImprimirCorteParcial()
        {
            bool bRegresa = false;

            if (validarDatosDeConexion())
            {
                DatosCliente.Funcion = "ImprimirCorteParcial()";
                clsImprimir myRpt = new clsImprimir(DatosDeConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = DtGeneral.RutaReportes;
                myRpt.NombreReporte = "PtoVta_CorteParcial.rpt";

                myRpt.Add("@IdEmpresa", cboEmpresas.Data);
                myRpt.Add("@IdEstado", cboEstados.Data);
                myRpt.Add("@IdFarmacia", cboFarmacias.Data);

                myRpt.Add("@FechaDeSistema", General.FechaYMD(dtpFechaInicial.Value, "-"));
                myRpt.Add("@IdPersonal", sPersonal);

                bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente); 

                ////////if (General.ImpresionViaWeb)
                ////////{
                ////////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////////    DataSet datosC = DatosCliente.DatosCliente();

                ////////    conexionWeb.Url = General.Url;
                ////////    conexionWeb.Timeout = 300000;
                ////////    //////myRpt.CargarReporte(true); 

                ////////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                ////////}
                ////////else
                ////////{
                ////////    myRpt.CargarReporte(true);
                ////////    bRegresa = !myRpt.ErrorAlGenerar;
                ////////}

            }
        }

        private void grdPersonalCortes_DoubleClick(object sender, EventArgs e)
        {
            if (validarSeleccion())
                ImprimirCorteParcial(); 
        }

        #region Cargar Combos
        private void CargarEmpresas()
        {
            cboEmpresas.Clear();
            cboEmpresas.Add();

            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            cboFarmacias.Clear();
            cboFarmacias.Add();
            cboFarmacias.SelectedIndex = 0;

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
                    Error.GrabarError(leer, "CargarEmpresas()");
                    General.msjError("Ocurrió un error al obtener la lista de Estados por Empresas.");
                }
                else
                {
                    dtsEstados = leer.DataSetClase;
                }

            } 
        }

        private void CargarEstados()
        {
            string sFiltro = string.Format(" IdEmpresa = '{0}' and StatusEdo = '{1}' ", cboEmpresas.Data, "A");
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.Add(dtsEstados.Tables[0].Select(sFiltro), true, "IdEstado", "NombreEstado"); 
        }

        private void CargarFarmacias()
        {
            sSqlFarmacias = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                            " From vw_Farmacias_Urls U (NoLock) " +
                            " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                            " Where U.IdEmpresa = '{0}' and U.IdEstado = '{1}' and ( U.IdFarmacia <> '{2}' ) " +
                            "   and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                            cboEmpresas.Data, cboEstados.Data, DtGeneral.FarmaciaConectada);

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");

            if (!leer.Exec(sSqlFarmacias))
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            }
            else
            {
                cboFarmacias.Add(leer.DataRowsClase, true, "IdFarmacia", "Farmacia");
            }
            cboFarmacias.SelectedIndex = 0;
        }

        #endregion Cargar Combos

        #region Funciones

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

                conexionWeb.Url = sUrl;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

                DatosDeConexion.Servidor = sHost;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
                ActivarControles();
            }

            return bRegresa;
        }

        //private bool validaFechaCorte()
        //{
        //    bool bRegresa = true;

        //    return bRegresa;
        //}

        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true;
            btnImprimir.Enabled = true;
        }

        #endregion Funciones

        #region Eventos
        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEstados.Clear();
            cboEstados.Add(); 

            if (cboEmpresas.SelectedIndex != 0)
            {
                cboEmpresas.Enabled = false;
                cboEstados.Enabled = true;
                CargarEstados();
            }

            cboEstados.SelectedIndex = 0; 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add(); 

            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;
                cboFarmacias.Enabled = true;
                CargarFarmacias();
            }

            cboFarmacias.SelectedIndex = 0; 
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
                sHost = ((DataRow)cboFarmacias.ItemActual.Item)["Servidor"].ToString();
                // cboFarmacias.Enabled = false;
            }
        }
        #endregion Eventos
    }
}
