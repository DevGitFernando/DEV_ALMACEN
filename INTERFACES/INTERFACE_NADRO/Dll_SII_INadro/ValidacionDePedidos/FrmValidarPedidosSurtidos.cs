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
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;

using DllTransferenciaSoft;
using DllTransferenciaSoft.IntegrarInformacion; 

namespace Dll_SII_INadro.ValidacionDePedidos
{
    public partial class FrmValidarPedidosSurtidos : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, CantidadPedido = 4, CantidadRecibida = 5,
            DañadoMalEstado = 6, Caducado = 7, CantidadRecibidaFinal = 8,
            Excedente = 9, Validado = 10   
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer llenaDatos;
        clsLeer leerLotes;

        Dll_SII_INadro.clsConsultas Consultas; 
        Dll_SII_INadro.clsAyudas Ayuda; 

        DllFarmaciaSoft.clsConsultas ConsultasGeneral; 
        DllFarmaciaSoft.clsAyudas AyudaGeneral; 

        DataSet dtsCargarDatos = new DataSet();
        clsGrid grid;
        clsLotes Lotes;
        clsCodigoEAN EAN = new clsCodigoEAN();

        TiposDeInventario tpInventarioModulo = TiposDeInventario.Consignacion;

        clsDatosCliente DatosCliente;
        wsIntAlmacen.wsInterfaceAlmacen conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();
        wsIntAlmacen.wsInterfaceAlmacen OrdenesWeb;


        string sUrlServidorPedidosDeUnidades = "";
        bool bServidorCompras_EnLinea = false;
        string sMensajeNoConexion_ServidorCompras = "No fue posible establecer conexión con el Servidor de Pedidos";

        bool bEsConsultaExterna = false;
        bool bContinua = true;
        bool bModificarCaptura = true;
        string sFolioOrden = "", sMensaje = "";
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFolioMovto = "";
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
        string sEstadoGenera_OC = "";
        string sFarmaciaGenera_OC = "";

        // DataSet dtsOrdenCompra;
        // DataSet dtsOCLocal;

        string sIdTipoMovtoInv = "EOC";
        string sTipoES = "E";
        string sFormato = "#,###,##0.###0";
        string sFolioEntrada = "";
        bool bFolioGuardado = false;
        bool bExceso = false;
        bool bEsNoSolicitado = false;
        bool bModuloCargado = false; 

        public FrmValidarPedidosSurtidos()
        {
            InitializeComponent();

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.SetConnectionString();

            DatosCliente = new clsDatosCliente(GnDll_SII_INadro.DatosApp, this.Name, "");
            conexionWeb = new wsIntAlmacen.wsInterfaceAlmacen();
            conexionWeb.Url = General.Url;

            OrdenesWeb = new wsIntAlmacen.wsInterfaceAlmacen();


            leer = new clsLeer(ref cnn);
            llenaDatos = new clsLeer(ref cnn);
            leerLotes = new clsLeer(ref cnn);

            Consultas = new Dll_SII_INadro.clsConsultas(General.DatosConexion, GnDll_SII_INadro.DatosApp, this.Name);
            Ayuda = new Dll_SII_INadro.clsAyudas(General.DatosConexion, GnDll_SII_INadro.DatosApp, this.Name);

            ConsultasGeneral = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            AyudaGeneral = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);


            grid = new clsGrid(ref grdProductos, this);
            grid.BackColorColsBlk = Color.White;
            grdProductos.EditModeReplace = true;

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

        }

        #region Form 
        private void FrmValidarPedidosSurtidos_Load(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }
        #endregion Form 

        #region Botones
        private void InicializarToolBar()
        {
            InicializarToolBar(false, false, false); 
        }

        private void InicializarToolBar(bool Guardar, bool GenerarPaqueteDeDatos, bool Imprimir)  
        {
            btnGuardar.Enabled = Guardar;
            btnGenerarPaqueteDeDatos.Enabled = GenerarPaqueteDeDatos;
            btnImprimir.Enabled = Imprimir;
        }

        private void InicializarPantalla()
        {
            InicializarToolBar(); 
            Fg.IniciaControles();
            grid.Limpiar(false);

            dtpFechaRegistro.Enabled = false; 
            txtClaveCliente.Enabled = false;
            txtFolio.Enabled = false;

            txtReferenciaFolioPedido.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Guardar(); 
            }
        }

        private void btnGenerarPaqueteDeDatos_Click(object sender, EventArgs e)
        {
            Dll_SII_INadro.ObtenerInformacion.clsCliente_INadro f = new ObtenerInformacion.clsCliente_INadro(DtGeneral.CfgIniPuntoDeVenta, General.DatosConexion);
            f.Pedidos_Unidades(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text.Trim());

            General.msjAviso("Generación de Paquete de Datos terminada.");
            f.Abrir_Directorio_PedidosUnidades(); 
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string sMsj = "¿ Desea Imprimir los Documentos ?";
            
            if (General.msjConfirmar(sMsj) == DialogResult.Yes)
            {
                Imprimir("INT_ND_Pedidos_Remisiones_Recibido.rpt");
                Imprimir("INT_ND_Pedidos_Remisiones_Excedente.rpt");
                Imprimir("INT_ND_Pedidos_Remisiones_Caducado.rpt");
                Imprimir("INT_ND_Pedidos_Remisiones_MalEstado.rpt");
                Imprimir("INT_ND_Pedidos_Remisiones_Faltante.rpt");
            }

            InicializarPantalla(); 
        }
        #endregion Botones 

        #region Informacion de Pedido
        private void txtReferenciaFolioPedido_Validating(object sender, CancelEventArgs e)
        {
            if (txtReferenciaFolioPedido.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.ND_Validar_Pedidos_Integrados(sEmpresa, sEstado, sFarmacia, txtReferenciaFolioPedido.Text.Trim(), "txtReferenciaFolioPedido_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Referencia de Folio de Pedido no encontrada, verifique.");
                }
                else
                {
                    CargarInf_Pedido(); 
                }
            }
        }

        private void CargarInf_Pedido()
        {
            bool bImpresion = false;

            txtReferenciaFolioPedido.Enabled = false;

            txtFolio.Text = leer.Campo("Folio");
            txtClaveCliente.Text = leer.Campo("CodigoCliente");
            lblNombreCliente.Text = leer.Campo("NombreCliente");
            dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");
            bImpresion = leer.CampoBool("EsValidado");

            CargarInf_Pedido_Detalles();
            InicializarToolBar(true, true, bImpresion); 
        }

        private void CargarInf_Pedido_Detalles()
        {
            grid.Limpiar(false); 
            leer.DataSetClase = Consultas.ND_Validar_Pedidos_Integrados_Detalles(sEmpresa, sEstado, sFarmacia, txtReferenciaFolioPedido.Text.Trim(), "txtReferenciaFolioPedido_Validating");
            
            if (leer.Leer())
            {
                grid.LlenarGrid(leer.DataSetClase, false, false); 
            }
        }
        #endregion Informacion de Pedido

        #region Guardar Informacion 
        private bool validarDatos()
        {
            bool bRegresa = true;

            return bRegresa; 
        }

        private void Guardar()
        {
            bool bRegresa = true;

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbriConexion(); 
            }
            else
            {
                cnn.IniciarTransaccion();
                
                bRegresa = GuardarDetalles();                

                if (!bRegresa)
                {
                    Error.GrabarError(leer, "");
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al guardar la información del pedido."); 
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamente.");

                    btnImprimir_Click(null, null);
                }

                cnn.Cerrar(); 
            }

        }

        private bool GuardarDetalles()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdProducto = "";
            string sCodigoEAN = "";
            int iCantidad = 0;
            int iExcedente = 0;
            int iDañado = 0;
            int iCaducado = 0; 

            for (int i = 1; i <= grid.Rows; i++)
            {
                sIdProducto = grid.GetValue(i, (int)Cols.Codigo);
                sCodigoEAN = grid.GetValue(i, (int)Cols.CodEAN);
                
                ////iCantidad = grid.GetValueInt(i, (int)Cols.CantidadRecibida);
                iCantidad = grid.GetValueInt(i, (int)Cols.CantidadRecibidaFinal);

                iExcedente = grid.GetValueInt(i, (int)Cols.Excedente);
                iDañado = grid.GetValueInt(i, (int)Cols.DañadoMalEstado);
                iCaducado = grid.GetValueInt(i, (int)Cols.Caducado);


                sSql = string.Format("Exec spp_Mtto_INT_ND_Validacion_De_Pedidos " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @ReferenciaPedido = '{3}', @FolioPedido = '{4}', " + 
                    " @CodigoCliente = '{5}', @IdProducto = '{6}', @CodigoEAN = '{7}', @CantidadRecibida = '{8}', @CantidadExcedente = '{9}', " +
                    " @CantidadDañadoMalEstado = '{10}', @CantidadCaducado = '{11}' ", 
                    sEmpresa, sEstado, sFarmacia, txtReferenciaFolioPedido.Text.Trim(), txtFolio.Text.Trim(), txtClaveCliente.Text.Trim(),
                    sIdProducto, sCodigoEAN, iCantidad, iExcedente, iDañado, iCaducado); 

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
            }

            return bRegresa; 
        }        
        #endregion Guardar Informacion

        #region Impresion
        private void Imprimir(string Reporte)
        {
            bool bRegresa = false;

            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);            

            myRpt.RutaReporte = DtGeneral.RutaReportes; 
           
            myRpt.NombreReporte = Reporte;            

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("Folio", Fg.PonCeros(txtFolio.Text, 8));
            myRpt.Add("ReferenciaPedido", txtReferenciaFolioPedido.Text.Trim());

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }

        }
        #endregion Impresion
    }
}
