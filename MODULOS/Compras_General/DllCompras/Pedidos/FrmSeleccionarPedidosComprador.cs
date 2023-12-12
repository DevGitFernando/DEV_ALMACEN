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


namespace DllCompras.Pedidos
{
    public partial class FrmSeleccionarPedidosComprador : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;

        clsAyudas Ayuda = new clsAyudas();
        clsConsultas Consultas;
        clsConsultas query;
                
        string sIdPersonal = DtGeneral.IdPersonal;
        string sMensaje = "";

        // FrmCom_ConfirmarPedidoContado PedidosContado;
        //FrmCom_ConfirmarPedido ConfirmarPedido;
        //FrmCom_ConfirmarPedidoRegional ConfirmarPedidoRegional;
        FrmConfirmarPedidoOCEN ConfirmarPedido;
        FrmConfirmaPedidoRegClaves ConfirmarPedidoRegional;

        private enum Cols
        {
            Ninguna = 0,
            Farmacia = 1, Descripcion = 2, TipoPed = 3, TipoDeClaves = 4, DescTipoPed = 5, Folio = 6, FolioUnidad = 7, Fecha = 8
        }

        public FrmSeleccionarPedidosComprador()
        {
            InitializeComponent();

            // Inicializar las Variables Generales 
            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, GnCompras.DatosApp, this.Name);
            Ayuda = new clsAyudas(General.DatosConexion, GnCompras.Modulo, this.Name, GnCompras.Version);
            Consultas = new clsConsultas(General.DatosConexion, GnCompras.Modulo, this.Name, GnCompras.Version, false);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            
            grid = new clsGrid(ref grdPedidosDisponibles, this);
            grid.EstiloGrid(eModoGrid.ModoRow); 


            Cargar_Empresas();
            cboEmpresas.Focus();
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grid = new clsGrid(ref grdPedidosDisponibles, this);
            grid.Limpiar(false);
            Cargar_Empresas();
            cboEmpresas.Focus();
            cboEmpresas.Enabled = true;
            cboEdo.Enabled = true;

            if (DtGeneral.Modulo_Compras_EnEjecucion != TipoModuloCompras.Central)
            {
                cboEmpresas.Data = DtGeneral.EmpresaConectada;
                cboEdo.Data = DtGeneral.EstadoConectado;

                if (!DtGeneral.EsAdministrador)
                {
                    cboEmpresas.Enabled = false;
                    cboEdo.Enabled = false;
                }
            }

        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarDatosPedidos();
        }
        #endregion Botones

        #region Menu 
        private void verDetallesDePedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sTipoPedido = "", sEmpresa = "", sEstado = "", sFolio = "", sFarmacia = "", sFolioUnidad = "";
            string sTipoDeClaves = ""; 
            TipoPedidosFarmacia TipoPedido = TipoPedidosFarmacia.Ninguno;
            int iTipoPedido = 0;

            sEmpresa = cboEmpresas.Data;
            sEstado = cboEdo.Data;
            sFarmacia = grid.GetValue(grid.ActiveRow, (int)Cols.Farmacia );
            sTipoPedido = grid.GetValue(grid.ActiveRow, (int)Cols.TipoPed);
            iTipoPedido = grid.GetValueInt(grid.ActiveRow, (int)Cols.TipoPed);
            sFolio = grid.GetValue(grid.ActiveRow, (int)Cols.Folio);
            sFolioUnidad = grid.GetValue(grid.ActiveRow, (int)Cols.FolioUnidad);
            sTipoDeClaves = grid.GetValue(grid.ActiveRow, (int)Cols.TipoDeClaves);

            TipoPedido = (TipoPedidosFarmacia)iTipoPedido;

            switch (TipoPedido)
            {
                ////case TipoPedidosFarmacia.Pedido_Especial:
                ////case TipoPedidosFarmacia.Pedido_Automatico:
                case TipoPedidosFarmacia.Pedido_Especial_Claves:
                case TipoPedidosFarmacia.Pedido_Automatico_Claves:
                    if ( DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central ) 
                    { 
                        //ConfirmarPedido = new FrmCom_ConfirmarPedido();
                        ConfirmarPedido = new FrmConfirmarPedidoOCEN();
                        ConfirmarPedido.MostrarPedido(sEmpresa, sEstado, sFarmacia, sTipoPedido, sFolio, sFolioUnidad, sTipoDeClaves);
                        ConfirmarPedido = null;
                    }
                    else 
                    {
                        //ConfirmarPedidoRegional = new FrmCom_ConfirmarPedidoRegional();
                        ConfirmarPedidoRegional = new FrmConfirmaPedidoRegClaves();
                        ConfirmarPedidoRegional.MostrarPedido(sEmpresa, sEstado, sFarmacia, sTipoPedido, sFolio, sTipoDeClaves);
                        ConfirmarPedidoRegional = null;
                    }                    
                    btnEjecutar_Click(null, null);
                    break;

                //case TipoPedidosFarmacia.Pedido_Especial_Credito:
                //    PedidosCredito = new FrmCom_ConfirmarPedidoCredito();
                //    PedidosCredito.MostrarPedidoCredito(sEmpresa, sEstado, sFarmacia, sTipoPedido, sFolio);
                //    PedidosCredito = null;
                //    break; 

                default: 
                    break; 
            }


            //if (sTipoPedido == "01")
            //{
            //    PedidosContado = new FrmCom_ConfirmarPedidoContado();
            //    PedidosContado.MostrarPedidoContado(sEmpresa, sEstado, sFarmacia, sTipoPedido, sFolio);
            //}
            //if (sTipoPedido == "02")
            //{
            //    PedidosCredito = new FrmCom_ConfirmarPedidoCredito();
            //    PedidosCredito.MostrarPedidoCredito(sEmpresa, sEstado, sFarmacia, sTipoPedido, sFolio);
            //}
            //if (sTipoPedido == "03")
            //{
            //    PedidosContado = new FrmCom_ConfirmarPedidoContado();
            //    PedidosContado.MostrarPedidoContado(sEmpresa, sEstado, sFarmacia, sTipoPedido, sFolio);                
            //}
            //if (sTipoPedido == "04")
            //{
            //    PedidosCredito = new FrmCom_ConfirmarPedidoCredito();
            //    PedidosCredito.MostrarPedidoCredito(sEmpresa, sEstado, sFarmacia, sTipoPedido, sFolio);
            //}
        }

        private void rechazarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool bContinua = false;
            string sFolio = "";

            sFolio = grid.GetValue(grid.ActiveRow, (int)Cols.Folio);

            if (sFolio.Trim() != "")
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    bContinua = EliminarAsignacionPedido();

                    if (bContinua)
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        grid.DeleteRow(grid.ActiveRow);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "rechazarPedidoToolStripMenuItem_Click");
                        General.msjError("Ocurrió un error al borrar la Información.");
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }
            }
        }
        #endregion Menu        

        #region Funciones

        
        private bool CargarDatosPedidos()
        {
            bool bRegresa = true;
            string sEstado = "", sEmpresa = "";

            if (cboEmpresas.SelectedIndex > 0 && cboEdo.SelectedIndex > 0)
            {
                sEmpresa = cboEmpresas.Data;
                sEstado = cboEdo.Data;

                grid.Limpiar();
                if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
                {
                    leer.DataSetClase = Consultas.Pedidos_Comprador(sEmpresa, sEstado, sIdPersonal, "CargarDatosPedidos");
                }
                else
                {
                    leer.DataSetClase = Consultas.Pedidos_Comprador_Regional(sEmpresa, sEstado, sIdPersonal, "CargarDatosPedidos");
                }

                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase, false, false);
                    grdPedidosDisponibles.Focus();
                    cboEmpresas.Enabled = false;
                    cboEdo.Enabled = false;
                }
                else
                {
                    General.msjUser("No existe información de pedidos, verifique.");
                    bRegresa = false;
                    cboEdo.Focus();
                }
            }
            else
            {
                General.msjAviso("No ha seleccionado Empresa y Estado, verifique.");
                cboEmpresas.Focus();
            }

            grid.BloqueaColumna(true);
           
            return bRegresa;        
        }

        private void Cargar_Empresas()
        {
            string sSql = "";

            cboEmpresas.Add("0", "<< Seleccione >>");            

            sSql = "Select IdEmpresa, Nombre, EsDeConsignacion From CatEmpresas (NoLock) Where Status = 'A' Order by IdEmpresa ";
            if (leer.Exec(sSql))
            {
                cboEmpresas.Clear();
                cboEmpresas.Add();
                cboEmpresas.Add(leer.DataSetClase, true, "IdEmpresa", "Nombre");
                cboEmpresas.SelectedIndex = 0;                
            }
            else
            {
                Error.LogError(leer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
        }

        private void Cargar_Estados()
        {
            string sSql = "", sEmpresa = "";

            sEmpresa = cboEmpresas.Data;
            sEmpresa = Fg.PonCeros(sEmpresa, 3);
            cboEdo.Add("0", "<< Seleccione >>");

            sSql = string.Format( "Select IdEstado, NombreEstado, ClaveRenapo, IdEmpresa From vw_EmpresasEstados (NoLock) Where IdEmpresa = '{0}' AND StatusEdo = 'A' Order by IdEstado ", sEmpresa );
            if (leer.Exec(sSql))
            {
                cboEdo.Clear();
                cboEdo.Add();
                cboEdo.Add(leer.DataSetClase, true, "IdEstado", "NombreEstado");                
                cboEdo.SelectedIndex = 0;
            }
            else
            {
                Error.LogError(leer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Estados.");
            }
            
        }

        private bool EliminarAsignacionPedido()
        {
            bool bRegresa = true;
            string sEmpresa = cboEmpresas.Data;
            string sEstado = cboEdo.Data;
            string sFarmacia = grid.GetValue(grid.ActiveRow, (int)Cols.Farmacia);
            string sTipoPedido = grid.GetValue(grid.ActiveRow, (int)Cols.TipoPed);
            string sFolioPedido = grid.GetValue(grid.ActiveRow, (int)Cols.Folio);
            
            int iEsCentral = 0;

            if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
            {
                iEsCentral = 1;
            }

            string sSql = String.Format("Exec spp_Mtto_COM_EliminarAsignacionPedido '{0}','{1}','{2}','{3}','{4}','{5}','{6}' ",
                sEmpresa, sEstado, sFarmacia, sTipoPedido, sFolioPedido, sIdPersonal, iEsCentral);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                }
                else
                {
                    bRegresa = false;
                }
            }

            return bRegresa;
        }
        #endregion Funciones

        #region Eventos

        private void FrmSeleccionarPedidosComprador_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargar_Estados();
        }

        #endregion Eventos
    }
}
