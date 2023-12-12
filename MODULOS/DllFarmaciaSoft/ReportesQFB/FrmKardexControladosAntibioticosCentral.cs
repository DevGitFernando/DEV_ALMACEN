using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllFarmaciaSoft.Conexiones;

namespace DllFarmaciaSoft.ReportesQFB
{
    public partial class FrmKardexControladosAntibioticosCentral : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente DatosCliente;
        clsGrid myGrid;
        clsLeer leer;
        
        clsAyudas Ayuda;
        clsConsultas Consulta;
        wsFarmaciaSoftGn.wsConexion conexionWeb;

        clsConexionClienteUnidad Cliente;

        string sIdClaveSSA = "";
        string sUrl = "";
        string sHost = "";
        string sUrlRegional = "";
        
        DataSet dtsEstados = new DataSet();

        public FrmKardexControladosAntibioticosCentral()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            // Esperar hasta que la consulta se ejecute. 
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmaciaSoftGn.wsConexion();
            conexionWeb.Url = General.Url;
            leer = new clsLeer(ref cnn);

            Cliente = new clsConexionClienteUnidad();

            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Consulta = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdMovtos, this);
            myGrid.EstiloGrid(eModoGrid.SoloLectura);
        }

        private void FrmSalesControladosAntibioticos_Load(object sender, EventArgs e)
        {
            CargarEmpresas();
            btnNuevo_Click(null, null);
        }

        #region Botones 

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            ObtenerInformacion();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Impresion();
        }

        #endregion Botones

        #region Cargar_Combos
        private void CargarEmpresas()
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
                //sSql = "Select IdEstado, NombreEstado, IdEmpresa, StatusEdo From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";

                sSql = " Select distinct E.IdEstado, E.NombreEstado, E.IdEmpresa, E.StatusEdo, U.UrlFarmacia as UrlRegional " +
                      " From vw_EmpresasEstados E (NoLock) " +
                      " Inner Join vw_Regionales_Urls U (NoLock) On ( E.IdEmpresa = U.IdEmpresa and E.IdEstado = U.IdEstado and U.IdFarmacia = '0001' ) " +
                      " Order By E.IdEmpresa ";

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
            cboEmpresas.SelectedIndex = 0;
        }

        private void CargarEstados()
        {
            string sFiltro = string.Format(" IdEmpresa = '{0}' and StatusEdo = '{1}' ", cboEmpresas.Data, "A");
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.Add(dtsEstados.Tables[0].Select(sFiltro), true, "IdEstado", "NombreEstado");
            cboEstados.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            string sSqlFarmacias = "";

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
        #endregion Cargar_Combos

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);

            myGrid.Limpiar(false);
            IniciaToolBar(true, true, false);

            rdoAntibioticos.Checked = true;
            chkBuscarClave.Visible = false;
            chkBuscarClave.Checked = false; 
            txtCodigo.Enabled = false;

            HabilitarControles();

            cboEstados.Enabled = false;
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            cboFarmacias.Enabled = false;
            cboFarmacias.Clear();
            cboFarmacias.Add();
            cboFarmacias.SelectedIndex = 0;
            chkBuscarClave.Checked = false;
            txtClaveLote.Enabled = false;

            BloquearControles(false);
            FrameTipoSales.Enabled = true;
            FrameTipoReporte.Enabled = true;
            FrameFechas.Enabled = true;
        }

        private void IniciaToolBar(bool bNuevo, bool bEjecutar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnEjecutar.Enabled = bEjecutar;
            // btnImprimir.Enabled = bImprimir;
        }

        private void HabilitarControles()
        {
            bool bActivar = false; 

            rdoControlados.Checked = bActivar;

            rdoTodasClaves.Checked = true;
            rdoPorClave.Checked = bActivar;
            rdoPorProducto.Checked = bActivar;
        }

        private void BloquearControles(bool Bloquear)
        {
            rdoAntibioticos.Enabled = !Bloquear;
            rdoControlados.Enabled = !Bloquear;

            rdoTodasClaves.Enabled = !Bloquear;
            rdoPorClave.Enabled = !Bloquear;
            rdoPorProducto.Enabled = !Bloquear;

            dtpFechaInicial.Enabled = !Bloquear;
            dtpFechaFinal.Enabled = !Bloquear; 
        }

        private void ObtenerInformacion()
        {
            DataSet dtsInformacion = null;
            DataSet datosC = DatosCliente.DatosCliente();

            string sSql = "";
            int iTipoReporte = 0;

            BloquearControles(true);


            if(rdoPorClave.Checked)
            {
                iTipoReporte = 1;
            }
            if(rdoPorProducto.Checked)
            {
                iTipoReporte = 2;
            }

            if (rdoAntibioticos.Checked)
            {
                if (txtClaveLote.Text.Trim() == "")
                {
                    sSql = string.Format("Set Dateformat YMD Exec spp_Kardex_Antibioticos_Farmacia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                           cboEmpresas.Data, cboEstados.Data, cboFarmacias.Data, sIdClaveSSA, txtCodigo.Text,
                           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), iTipoReporte);

                    sSql += "\n " + string.Format(" Select Convert(varchar(10), FechaRegistro, 120) As FechaRegistro, Folio, " +
                        " DescMovimiento, ClaveSSA, DescProducto, Entrada, Salida, Existencia  " +
                        " From tmpKardex_Antibioticos_Farmacia (Nolock) " +
                        " Order By IdClaveSSA_Sal, IdProducto, FechaRegistro  ");
                }
                else
                {
                    sSql = string.Format("Set Dateformat YMD Exec spp_Kardex_Antibioticos_Farmacia_Lotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'  ",
                           cboEmpresas.Data, cboEstados.Data, cboFarmacias.Data, sIdClaveSSA, txtCodigo.Text,
                           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), 
                           iTipoReporte, txtClaveLote.Text.Trim());

                    sSql += "\n " + string.Format(" Select Convert(varchar(10), FechaRegistro, 120) As FechaRegistro, Folio, " +
                        " DescMovimiento, ClaveSSA, DescProducto, Entrada, Salida, Existencia  " +
                        " From tmpKardex_Antibioticos_Farmacia_Lotes (Nolock) " +
                        " Order By IdClaveSSA_Sal, IdProducto, FechaRegistro  ");
                }
            }

            if (rdoControlados.Checked)
            {
                if (txtClaveLote.Text.Trim() == "")
                {
                    sSql = string.Format("Set Dateformat YMD Exec spp_Kardex_Controlados_Farmacia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                           cboEmpresas.Data, cboEstados.Data, cboFarmacias.Data, sIdClaveSSA, txtCodigo.Text,
                           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), iTipoReporte);

                    sSql += "\n " + string.Format("Select Convert(varchar(10), FechaRegistro, 120) As FechaRegistro, Folio, " +
                        " DescMovimiento, ClaveSSA, DescProducto, Entrada, Salida, Existencia  " +
                        " From tmpKardex_Controlados_Farmacia (Nolock) " +
                        " Order By IdClaveSSA_Sal, IdProducto, FechaRegistro  ");
                }
                else
                {
                    sSql = string.Format("Set Dateformat YMD Exec spp_Kardex_Controlados_Farmacia_Lotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'  ",
                           cboEmpresas.Data, cboEstados.Data, cboFarmacias.Data, sIdClaveSSA, txtCodigo.Text,
                           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), 
                           iTipoReporte, txtClaveLote.Text.Trim());

                    sSql += "\n " + string.Format("Select Convert(varchar(10), FechaRegistro, 120) As FechaRegistro, Folio, " +
                        " DescMovimiento, ClaveSSA, DescProducto, Entrada, Salida, Existencia  " +
                        " From tmpKardex_Controlados_Farmacia_Lotes (Nolock) " +
                        " Order By IdClaveSSA_Sal, IdProducto, FechaRegistro  "); 
                }
            }        

            myGrid.Limpiar(false); 
            dtsInformacion = ObtenerDataSetInformacion(sSql);

            try
            {
                conexionWeb = new wsFarmaciaSoftGn.wsConexion();
                conexionWeb.Url = sUrlRegional;

                leer.Reset();
                leer.DataSetClase = conexionWeb.ExecuteRemoto(dtsInformacion, datosC);
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source;
            }

            if (leer.SeEncontraronErrores())
            {
                Error.GrabarError(leer, "ObtenerInformacion");
                General.msjError("Ocurrió un error al obtener la información de movimientos.");
                BloquearControles(false); 
            }
            else
            {
                if (leer.Leer())
                {
                    myGrid.LlenarGrid(leer.DataSetClase);
                    IniciaToolBar(true, false, true); 
                }
                else
                {
                    General.msjUser("No se encontro información para los criterios especificados.");
                    BloquearControles(false); 
                }
            }
        }

        private void Impresion()
        {
            bool bRegresa = false;

            DataSet dtsInformacion = null;
            //if (validarImpresion())
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                dtsInformacion = ObtenerDataSetInformacion("");

                //Ruta de reportes PARA PRUEBAS
                // DtGeneral.RutaReportes = @"D:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES"; 
                myRpt.RutaReporte = DtGeneral.RutaReportes;


                if (rdoAntibioticos.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_Kardex_Antibioticos_Farmacia";  
                }
                if (rdoControlados.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_Kardex_Controlados_Farmacia";
                }

                // bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, ref myRpt, ref DatosCliente);
                bRegresa = DtGeneral.GenerarReporteRemoto(sUrlRegional, true, Cliente, myRpt, DatosCliente);

                //////if (General.ImpresionViaWeb)
                //////{
                //////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                //////    DataSet datosC = DatosCliente.DatosCliente();

                //////    conexionWeb = new DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion(); 
                //////    conexionWeb.Url = sUrlRegional; 
                //////    btReporte = conexionWeb.ReporteRemoto(dtsInformacion, datosC, InfoWeb);
                //////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                //////}
                //////else
                //////{
                //////    myRpt.CargarReporte(true);
                //////    bRegresa = !myRpt.ErrorAlGenerar;
                //////}

                if (!bRegresa &&!DtGeneral.CanceladoPorUsuario )
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }           
        #endregion Funciones

        #region Eventos

        #region Eventos_TipoReporte
        private void rdoTodasClaves_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTodasClaves.Checked)
            {
                txtCodigo.Enabled = false;
                chkBuscarClave.Visible = false;
            }
        }

        private void rdoPorClave_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPorClave.Checked)
            {
                txtCodigo.Enabled = true;
                chkBuscarClave.Visible = true;
            }
        }

        private void rdoPorProducto_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPorProducto.Checked)
            {
                txtCodigo.Enabled = true;
                chkBuscarClave.Visible = false;
            }
        } 
        #endregion Eventos_TipoReporte

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodigo.Text.Trim() != "")
            {
                leer.DataSetClase = Consulta.Sales_Antibioticos_Controlados(txtCodigo.Text, chkBuscarClave.Checked, rdoPorProducto.Checked,
                                                                            rdoControlados.Checked, rdoAntibioticos.Checked, "txtCodigo_Validating");
                if (leer.Leer())
                {
                    if (rdoPorClave.Checked)
                    {
                        txtCodigo.Text = leer.Campo("ClaveSSA");
                        lblDescripcion.Text = leer.Campo("DescripcionSal");
                    }
                    if (rdoPorProducto.Checked)
                    {
                        txtCodigo.Text = leer.Campo("IdProducto");
                        lblDescripcion.Text = leer.Campo("Descripcion");
                    }

                    sIdClaveSSA = leer.Campo("IdClaveSSA_Sal");
                }
                else
                {
                    e.Cancel = true;
                    //General.msjUser("Clave SSA ó Producto no encontrada, verifique.");
                }
            }
        }
        #endregion Eventos

        #region Eventos_Combos
        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEmpresas.SelectedIndex != 0)
            {
                cboEmpresas.Enabled = false;
                cboEstados.Enabled = true;
                CargarEstados();
            } 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                sUrlRegional = cboEstados.ItemActual.GetItem("UrlRegional");

                cboEstados.Enabled = false;
                cboFarmacias.Enabled = true;
                CargarFarmacias();
            } 
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
                sHost = ((DataRow)cboFarmacias.ItemActual.Item)["Servidor"].ToString();
                cboFarmacias.Enabled = false;
            }
        }

        private void chkLote_CheckedChanged(object sender, EventArgs e)
        {
            txtClaveLote.Text = "";

            if (chkLote.Checked)
            {
                txtClaveLote.Enabled = true;
            }
            else
            {
                txtClaveLote.Enabled = false;
            }
        }

        #endregion Eventos_Combos

        #region Funciones_Web
        private DataSet ObtenerDataSetInformacion(string sSentencia)
        {
            DataSet dtsInformacion;

            //clsConexionClienteUnidad Cliente;
            //Cliente = new clsConexionClienteUnidad();

            Cliente.Empresa = cboEmpresas.Data;
            Cliente.Estado = cboEstados.Data;
            Cliente.Farmacia = cboFarmacias.Data;
            Cliente.Sentencia = sSentencia;
            Cliente.ArchivoConexionCentral = DtGeneral.CfgIniOficinaCentral;
            Cliente.ArchivoConexionUnidad = DtGeneral.CfgIniPuntoDeVenta;

            dtsInformacion = Cliente.dtsInformacion;

            return dtsInformacion;

        }
        #endregion Funciones_Web

        
    }
}
