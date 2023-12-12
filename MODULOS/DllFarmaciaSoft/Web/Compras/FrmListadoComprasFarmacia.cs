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
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Conexiones;

namespace DllFarmaciaSoft.Web.Compras
{
    public partial class FrmListadoComprasFarmacia : FrmBaseExt
    {
        clsDatosConexion DatosDeConexion;
        clsConexionSQL cnn;  // = new clsConexionSQL(General.DatosConexion); 
        // clsConexionSQL cnnUnidad;
        clsLeer leer;
        clsLeer leerLocal;
        clsLeerWebExt leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        clsGrid Grid;

        string sSqlFarmacias = "";
        string sUrl;
        string sHost = "";
        // string sUrl_RutaReportes = "";
        string sFormato = "#,###,##0.###0";
        int iValor_0 = 0;

        string sUrl_Regional = ""; 
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        // Thread _workerThread;

        // bool bEjecutando = false;
        // bool bSeEncontroInformacion = false;
        // bool bSeEjecuto = false;

        public FrmListadoComprasFarmacia()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");
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
            
            Grid = new clsGrid(ref grdCompras, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.SetOrder(true); 

        }

        private void FrmListadoComprasFarmacia_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;

            lblTotal.Text = iValor_0.ToString(sFormato); 
            Grid.Limpiar(false);            

            CargarEstados();
            cboEstados.SelectedIndex = 0;
            cboFarmacias.SelectedIndex = 0; 

        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                CargaDetallesCompras();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Cargar Combos

        private void CargarEstados()
        {            
            cboEstados.Clear();
            cboEstados.Add();

            cboFarmacias.Clear(); 
            cboFarmacias.Add();


            string sSql = ""; //  "Select distinct IdEstado, Estado, EdoStatus From vw_Farmacias (NoLock) Where EdoStatus = 'A' Order By IdEstado ";

            sSql = " Select distinct E.IdEstado, E.NombreEstado as Estado, E.IdEmpresa, E.StatusEdo, U.UrlFarmacia as UrlRegional " +
                  " From vw_EmpresasEstados E (NoLock) " +
                  " Inner Join vw_Regionales_Urls U (NoLock) On ( E.IdEmpresa = U.IdEmpresa and E.IdEstado = U.IdEstado and U.IdFarmacia = '0001' ) " +
                  " Order By E.IdEmpresa ";

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEstados()");
                General.msjError("Ocurrió un error al obtener la lista de Estados.");
            }
            else
            {
                cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Estado");
            }
            cboEstados.SelectedIndex = 0;
            cboFarmacias.SelectedIndex = 0; 
        }

        private void CargarFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add();           

            sSqlFarmacias = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                            " From vw_Farmacias_Urls U (NoLock) " +
                            " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                            " Where U.IdEstado = '{0}' and ( U.IdFarmacia <> '{1}' ) " +
                            " and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                            cboEstados.Data, DtGeneral.FarmaciaConectada);

            if (!leer.Exec(sSqlFarmacias)) 
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
            }
            else
            {
                cboFarmacias.Add(leer.DataSetClase, true, "IdFarmacia", "Farmacia");
                
            }
            cboFarmacias.SelectedIndex = 0;
        }

        #endregion Cargar Combos 

        #region Funciones 
        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true;
            btnEjecutar.Enabled = true;            
        }

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

        private void CargaDatosProveedor()
        {
            //Se hace de esta manera para la ayuda. 

            if (leerLocal.Campo("Status").ToUpper() == "A")
            {
                txtProveedor.Text = leerLocal.Campo("IdProveedor");
                lblProveedor.Text = leerLocal.Campo("Nombre");
            }
            else
            {
                General.msjUser("El Proveedor " + leerLocal.Campo("Nombre") + " actualmente se encuentra cancelado, verifique. ");
                txtProveedor.Text = "";
                lblProveedor.Text = "";
                txtProveedor.Focus();
            }
        }

        private void CargaDetallesCompras()
        {
            string sWhere = ""; // , sRef = "";

            if (txtProveedor.Text.Trim() != "")            
            {
                sWhere = " And IdProveedor = '" + txtProveedor.Text + "'";
            }
           
            if (txtReferencia.Text.Trim() != "")
            {
                sWhere += " And ReferenciaDocto like '%" + txtReferencia.Text + "%'";
            }


            string sSql = 
                string.Format(" Select IdEmpresa, IdProveedor, Proveedor, Folio, " +
                    " convert(varchar(10), FechaDocto, 120) As FechaDocto, " + 
                    " convert(varchar(10), FechaRegistro, 120) As FechaRegistro, " + 
                    " ReferenciaDocto, Total " +
                    " From vw_ComprasEnc (Nolock) " + 
                    " Where IdEstado = '{0}' And " +
                    " IdFarmacia = '{1}' And FechaRegistro between convert(varchar(10), '{2}', 120) " +
                    " and convert(varchar(10), '{3}', 120) {4} " + 
                    " Order By Folio, FechaRegistro ",
                    cboEstados.Data, cboFarmacias.Data, 
                    General.FechaYMD(dtpFechaInicial.Value), 
                    General.FechaYMD(dtpFechaFinal.Value), 
                    sWhere );

            //// 
            lblTotal.Text = iValor_0.ToString(sFormato); 
            Grid.Limpiar(); 

            // if (validarDatosDeConexion())
            {
                ////cnnUnidad = new clsConexionSQL(DatosDeConexion);
                ////cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                ////cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite300; 

                // leer = new clsLeer(ref cnnUnidad);
                leer = new clsLeer(); 
                clsConexionClienteUnidad conecionCte = new clsConexionClienteUnidad();
                
                conecionCte.Empresa = DtGeneral.EmpresaConectada;
                conecionCte.Estado = cboEstados.Data;
                conecionCte.Farmacia = cboFarmacias.Data;
                conecionCte.Sentencia = sSql; 

                conecionCte.ArchivoConexionCentral = DtGeneral.CfgIniOficinaCentral;
                conecionCte.ArchivoConexionUnidad = DtGeneral.CfgIniPuntoDeVenta;

                try
                {
                    // sUrl_Regional = General.Url; 
                    conexionWeb.Url = sUrl_Regional;
                    leer.DataSetClase = conexionWeb.ExecuteRemoto(conecionCte.dtsInformacion, DatosCliente.DatosCliente());
                }
                catch (Exception ex)
                {
                    Error.LogError(ex.Message); 
                }


                if ( leer.SeEncontraronErrores() )
                {
                    Error.GrabarError(leer, "CargaDetallesCompras()");
                    General.msjError("Ocurrió un error al obtener la información de las compras.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        btnImprimir.Enabled = true;
                        // bSeEncontroInformacion = true;
                        Grid.LlenarGrid(leer.DataSetClase, false, false);
                        lblTotal.Text = Grid.TotalizarColumnaDou(8).ToString(sFormato); 
                    }
                    else
                    {
                        // bSeEncontroInformacion = false;
                        General.msjUser("No se encontro información con los criterios especificados, verifique."); 
                    }                   
                }
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = false;

            if (cboEstados.SelectedIndex != 0)
            {
                bRegresa = true;
            }

            if (bRegresa)
            {
                if (cboFarmacias.SelectedIndex != 0)
                {
                    bRegresa = true;
                }
                else
                {
                    bRegresa = false;
                }
            }

            if (!bRegresa)
            {
                General.msjAviso("Faltan Datos por Capturar, Verifique !!");
            }

            return bRegresa;
        }

        #endregion Funciones

        #region Eventos 
        private void txtProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtProveedor.Text.Trim() != "")
            {
                leerLocal.DataSetClase = Consultas.Proveedores(txtProveedor.Text.Trim(), "txtProveedor_Validating");
                if (leerLocal.Leer())
                {
                    CargaDatosProveedor();
                }
                else
                {
                    txtProveedor.Focus();
                }
            }
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add();

            sUrl_Regional = ""; 
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;
                sUrl_Regional = cboEstados.ItemActual.GetItem("UrlRegional"); 
                CargarFarmacias();
            }
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
                sHost = ((DataRow)cboFarmacias.ItemActual.Item)["Servidor"].ToString();
            }
        }

        private void txtProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leerLocal.DataSetClase = Ayuda.Proveedores("txtProveedor_KeyDown");

                if (leerLocal.Leer())
                {
                    CargaDatosProveedor();
                }
            }
        }

        private void txtProveedor_TextChanged(object sender, EventArgs e)
        {
            lblProveedor.Text = "";
        }

        private void grdCompras_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // FrmComprasDeFarmacia f = new FrmComprasDeFarmacia(DatosDeConexion, conexionWeb.Url);
            ////f.MostrarFolioCompra(Grid.GetValue(Grid.ActiveRow, 1), cboEstados.Data, cboFarmacias.Data, 
            ////    Grid.GetValue(Grid.ActiveRow, 4), DatosDeConexion);

            FrmComprasDeFarmacia f = new FrmComprasDeFarmacia();
            f.MostrarFolioCompra(Grid.GetValue(Grid.ActiveRow, 1), cboEstados.Data, cboFarmacias.Data,
                Grid.GetValue(Grid.ActiveRow, 4), sUrl_Regional); 
        } 
        #endregion Eventos          
               
    }
}
