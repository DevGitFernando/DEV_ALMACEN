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

namespace Farmacia.Inventario
{
    public partial class FrmKardexProducto : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion datosCnn; 
        clsLeer leer;
        clsGrid myGrid;
        clsAyudas ayuda;
        Boolean bExistenciaEnTransito = false;

        DataSet dtsEnTransito;
        string sIdProducto = ""; 

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        // int iAnchoColDescripcion = 0;

        public FrmKardexProducto()
        {
            InitializeComponent();

            datosCnn = new clsDatosConexion();
            datosCnn.Servidor = General.DatosConexion.Servidor;
            datosCnn.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            datosCnn.Usuario = General.DatosConexion.Usuario;
            datosCnn.Password = General.DatosConexion.Password;
            datosCnn.Puerto = General.DatosConexion.Puerto;
            datosCnn.ConexionDeConfianza = General.DatosConexion.ConexionDeConfianza;
            
            cnn = new clsConexionSQL(datosCnn);
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite; 

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdMovtos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.AjustarAnchoColumnasAutomatico = true;

            // cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            GnFarmacia.AjustaColumnasImportes(grdMovtos, 7, 8, 3 );
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            bExistenciaEnTransito = false;
            btnEnTransito.Enabled = false; 
            lblProductoBloqueadoPorInventario.Visible = false; 
            myGrid.Limpiar(false);
            Fg.IniciaControles();

            btnEjecutar.Enabled = true;
            btnImprimir.Enabled = false; 

            dtpFechaInicial.MinDate = DtGeneral.FechaMinimaSistema;
            dtpFechaFinal.MinDate = DtGeneral.FechaMinimaSistema;

            dtpFechaInicial.MaxDate = dtpFechaInicial.Value;
            dtpFechaFinal.MaxDate = dtpFechaFinal.Value;

            rdoConcentrado.Checked = true; 
            txtCodigo.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = ""; 
            string sFechas = string.Format(" Convert(varchar(10), FechaSistema, 120) Between '{0}' and '{1}' ",
                General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            
            bExistenciaEnTransito = false;
            sSql = string.Format(
                 "Declare \n " +
                 "   @Bloquedado int \n " +
                 "   Select @Bloquedado = (case when dbo.fg_INV_GetStatusProducto('{0}', '{1}', '{2}', '{4}') In ( 'I', 'S' ) Then 1 else 0 end) \n " +
                 "   Select Convert(varchar(10), FechaSistema, 120) as Fecha, Folio, \n " +
                 "       DescMovimiento, ---- Entrada, Salida, Existencia, \n " +
                 "       (case when @Bloquedado = 1 Then 0 else Entrada End) As Entrada, \n " +
                 "       (case when @Bloquedado = 1 Then 0 else Salida End) As Salida, \n " +
                 "       (case when @Bloquedado = 1 Then 0 else Existencia End) As Existencia, \n " +
                 "       (case when @Bloquedado = 1 Then 0 else Costo End) As Costo, \n " +
                 "       (case when @Bloquedado = 1 Then 0 else Importe End) As Importe, @Bloquedado as Bloqueado \n " +
                 "   From vw_Kardex_Producto (NoLock) \n " +
                 "   Where IdEstado = '{1}' and IdFarmacia = '{2}' and CodigoEAN = '{3}' and {5} \n " +
                 "   Order By FechaRegistro, Keyx ",
                 DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                 lblCodigoEAN.Text, sIdProducto, sFechas);


            sSql = string.Format("Exec spp_Kardex_Por_Producto " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @CodigoEAN = '{3}', " +
                " @FechaInicial = '{4}', @FechaFinal = '{5}', @TipoResultado = '0' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                lblCodigoEAN.Text, General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));  

            myGrid.Limpiar(false);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al obtener la información de movimientos.");
            }
            else
            {
                if (leer.Leer())
                {
                    btnImprimir.Enabled = !leer.CampoBool("Bloqueado"); 
                    myGrid.LlenarGrid(leer.DataSetClase);
                }
                else
                {
                    General.msjUser("No se encontro información para los criterios especificados.");
                }
            }

            ProductoEnTransitos();
            btnEnTransito.Enabled = bExistenciaEnTransito; 
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa = false; 
            //if (validarImpresion())
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;

                if (rdoConcentrado.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_KardexDeProducto";
                    myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                    myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                    myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                    myRpt.Add("IdProducto", lblCodigoEAN.Text);
                    myRpt.Add("FechaInicial", General.FechaYMD(dtpFechaInicial.Value));
                    myRpt.Add("FechaFinal", General.FechaYMD(dtpFechaFinal.Value));
                    myRpt.Add("ImpresoPor", DtGeneral.IdPersonal + " - " + DtGeneral.NombrePersonal);
                }

                if (rdoDetallado.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_KardexDeProducto_Detallado";
                    myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
                    myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
                    myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
                    myRpt.Add("@IdProducto", lblCodigoEAN.Text);
                    myRpt.Add("@FechaInicial", General.FechaYMD(dtpFechaInicial.Value));
                    myRpt.Add("@FechaFinal", General.FechaYMD(dtpFechaFinal.Value));
                    myRpt.Add("ImpresoPor", DtGeneral.IdPersonal + " - " + DtGeneral.NombrePersonal);
                }

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

                ////if (General.ImpresionViaWeb)
                ////{
                ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////    DataSet datosC = DatosCliente.DatosCliente();

                ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                ////}
                ////else
                ////{
                ////    myRpt.CargarReporte(true);
                ////    bRegresa = !myRpt.ErrorAlGenerar;
                ////}

                if (!bRegresa && !DtGeneral.CanceladoPorUsuario) 
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Botones

        private void FrmKardexDeProducto_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void IniciarCaptura()
        {
            lblCodigoEAN.Text = "";
            lblDescripcion.Text = ""; 
            myGrid.Limpiar();
        }

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            lblProductoBloqueadoPorInventario.Visible = false;
            bool bActivar = true;
            sIdProducto = Fg.PonCeros(txtCodigo.Text.Trim(), 8);
            string sCodigoEAN_Seleccionado = ""; 


            if (txtCodigo.Text.Trim() == "")
            {
                lblDescripcion.Text = "";
                myGrid.Limpiar();
                txtCodigo.Focus();
            }
            else 
            {
                if (!GnFarmacia.ValidarSeleccionCodigoEAN(sIdProducto, ref sCodigoEAN_Seleccionado))
                {
                    IniciarCaptura(); 
                }
                else
                {
                    string sSql = string.Format("Select * " +
                        " From vw_ProductosExistenEnEstadoFarmacia (NoLock) " +
                        " Where CodigoEAN_Interno = '{0}' or CodigoEAN = '{1}' ", sCodigoEAN_Seleccionado, sCodigoEAN_Seleccionado);
                    if (!leer.Exec(sSql)) 
                    {
                        Error.GrabarError(leer, "txtCodigo_Validating");
                        General.msjError("Ocurrió un error al obtener la información del producto.");
                    }
                    else
                    {
                        if (!leer.Leer())
                        {
                            General.msjUser("Clave de Producto no encontrada, verifique.");
                            txtCodigo.Text = "";
                            txtCodigo.Focus(); 
                        }
                        else
                        {
                            txtCodigo.Text = leer.Campo("IdProducto");
                            lblCodigoEAN.Text = leer.Campo("CodigoEAN");
                            lblDescripcion.Text = leer.Campo("Descripcion");

                            if (leer.Campo("StatusDeProducto") == "S")
                            {
                                lblProductoBloqueadoPorInventario.Visible = true;
                                bActivar = false;
                            }
                        }
                    }
                }
            }

            btnEjecutar.Enabled = bActivar;
            ////btnImprimir.Enabled = bActivar; 

        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Productos_CodigoEAN("txtCodigo_KeyDown");
                if (ayuda.ExistenDatos )
                {
                    leer.Leer();
                    txtCodigo.Text = leer.Campo("IdProducto");
                    txtCodigo_Validating(null, null);
                }
            }
        }

        private void dtpFechaFinal_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaFinal.Value < dtpFechaInicial.Value)
            {
                dtpFechaFinal.Value = dtpFechaInicial.Value;
            }
        }

        private void dtpFechaInicial_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaFinal.Value < dtpFechaInicial.Value)
            {
                dtpFechaFinal.Value = dtpFechaInicial.Value;
            }
        }

        private void dtpFechaInicial_Validating(object sender, CancelEventArgs e)
        {
            if (dtpFechaFinal.Value < dtpFechaInicial.Value)
            {
                dtpFechaFinal.Value = dtpFechaInicial.Value;
            }
        }

        private void dtpFechaFinal_Validating(object sender, CancelEventArgs e)
        {
            if (dtpFechaFinal.Value < dtpFechaInicial.Value)
            {
                dtpFechaFinal.Value = dtpFechaInicial.Value;
            }
        }

        private void btnEnTransito_Click(object sender, EventArgs e)
        {
            if (bExistenciaEnTransito)
            {
                FrmKardexProductoTransferenciasEnTransito F = new FrmKardexProductoTransferenciasEnTransito(dtsEnTransito);
                F.ShowDialog();
            }
            else
            {
                General.msjAviso("No existen productos en transito.");
            }
        }

        private void ProductoEnTransitos()
        {
            string sSql = string.Format("Select T.Folio, Convert(Varchar(10), T.FechaTransferencia, 120) As 'Fecha', " +
                    "'Farmacia recibe' = (T.IdFarmaciaRecibe + ' - ' + T.FarmaciaRecibe), Cast(Sum(T.Cantidad) As Int) As Piezas " +
                    "From vw_TransferenciasDet_CodigosEAN T (NoLock) " +
                    "Where Left(T.Folio,2) = 'TS' And T.TransferenciaAplicada = 'False' And T.IdProducto = '{0}' And Convert(Varchar(10), T.FechaTransferencia, 120) between '{1}' And '{2}' " +
                    "Group By T.Folio, Convert(Varchar(10), T.FechaTransferencia, 120), T.IdFarmaciaRecibe , T.FarmaciaRecibe ", 
                    Fg.PonCeros(txtCodigo.Text, 8), General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

            sSql = string.Format("Exec spp_Transferencias__TransferenciasTransito_Producto '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                Fg.PonCeros(txtCodigo.Text, 8), General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-")); 

            if (!leer.Exec(sSql))  
            {
                Error.GrabarError(leer, "ProductoEnTransitos()");
                General.msjError("Ocurrió un error al obtener la información del producto en tránsito.");
            }
            else 
            {
                if (leer.Leer())
                {
                    dtsEnTransito = leer.DataSetClase;
                    bExistenciaEnTransito = true;
                }
            }
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            IniciarCaptura(); 
        }
    }
}
