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
    public partial class FrmPedidosAProcesar : FrmBaseExt 
    {
        enum Cols
        {
            Folio = 1, IdFarmacia = 2, NombreFarmacia = 3, TipoDePedido = 4, TipoDeClaves = 5, DescripcionPedido = 6, Fecha = 7
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;

        clsAyudas Ayuda = new clsAyudas();
        clsConsultas Consultas;
        clsConsultas query;
                
        string sIdPersonal = DtGeneral.IdPersonal;


        FrmConcentradoPedidosOCEN Claves;
        

        public FrmPedidosAProcesar()
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

            //lblIdPersonal.Text = DtGeneral.IdPersonal;
            //lblPersonal.Text = DtGeneral.NombrePersonal;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarDatosPedidos();
        }
        #endregion Botones

        #region Menu 
        private void verDetallesDePedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sTipoPedido = "", sEmpresa = "", sEstado = "", sFolio = "", sUnidad = "";
            TipoPedidosFarmacia TipoPedido = TipoPedidosFarmacia.Ninguno;
            int iTipoPedido = 0;

            sEmpresa = cboEmpresas.Data;
            sEstado = cboEdo.Data;
            
            sTipoPedido = grid.GetValue(grid.ActiveRow, (int)Cols.TipoDePedido);
            iTipoPedido = grid.GetValueInt(grid.ActiveRow, (int)Cols.TipoDePedido);
            sFolio = grid.GetValue(grid.ActiveRow, (int)Cols.Folio);
            sUnidad = grid.GetValue(grid.ActiveRow, (int)Cols.IdFarmacia); 
            
            TipoPedido = (TipoPedidosFarmacia)iTipoPedido;

            switch (TipoPedido)
            {
                ////case TipoPedidosFarmacia.Pedido_Especial:
                ////case TipoPedidosFarmacia.Pedido_Automatico:
                case TipoPedidosFarmacia.Pedido_Especial_Claves:
                case TipoPedidosFarmacia.Pedido_Automatico_Claves:
                    if ( DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central ) 
                    {                       
                        Claves = new FrmConcentradoPedidosOCEN();
                        Claves.MostrarClaves(sEmpresa, sEstado, sUnidad, sFolio, sTipoPedido, true);
                        Claves = null;
                        btnEjecutar_Click(null, null);
                    }
                    else 
                    {
                        Claves = new FrmConcentradoPedidosOCEN();
                        Claves.MostrarClaves(sEmpresa, sEstado, sUnidad, sFolio, sTipoPedido, true);
                        Claves = null;
                        btnEjecutar_Click(null, null);
                    }                    
                    btnEjecutar_Click(null, null);
                    break;               

                default: 
                    break; 
            }
            
        }

        ////private void rechazarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        ////{

        ////}

        private void desabilitarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = " ¿ Esta seguro de DesHabilitar el Pedido de la Lista ? ";
            bool bContinua = true;
            int iOpcion = 1;
            string sTipoPedido = "", sEmpresa = "", sEstado = "", sFolio = "", sSql = "", sUnidad = "";            

            sEmpresa = cboEmpresas.Data;
            sEstado = cboEdo.Data;

            sTipoPedido = grid.GetValue(grid.ActiveRow, 4);            
            sFolio = grid.GetValue(grid.ActiveRow, 1);
            sUnidad = grid.GetValue(grid.ActiveRow, 2);

            if (General.msjConfirmar(message) == DialogResult.Yes)
            {  

                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
                    {
                        sSql = string.Format(" Exec spp_Mtto_COM_OCEN_REG_ActualizaStatus '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'",
                                            sEmpresa, sEstado, sFolio, sTipoPedido, DtGeneral.IdPersonal, iOpcion, sUnidad );
                    }
                    else
                    {
                        sSql = string.Format(" Exec spp_Mtto_COM_OCEN_ActualizaStatus '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                                            sEmpresa, sEstado, sFolio, sTipoPedido, DtGeneral.IdPersonal, iOpcion, sUnidad );
                    }

                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "desabilitarPedidoToolStripMenuItem_Click");
                        General.msjError(" Ocurrió Un Error al Actualizar Status del Pedido ");
                    }

                    if (bContinua)
                    {
                        cnn.CompletarTransaccion();
                        CargarDatosPedidos();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();                      
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
            string sEstado = "", sEmpresa = "", sSql = "";

            if (cboEmpresas.SelectedIndex > 0 && cboEdo.SelectedIndex > 0)
            {
                sEmpresa = cboEmpresas.Data;
                sEstado = cboEdo.Data;

                grid.Limpiar();
                if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
                {
                    sSql = string.Format(" Select Folio, IdFarmacia, Farmacia, IdTipoPedido, TipoDeClavesDePedido, TipoDeClavesDePedidoDescripcion, FolioPedidoUnidad, " + 
	                                    " Convert(varchar (10), FechaRegistro, 120) As Fecha  " +
                                        " From vw_COM_OCEN_REG_PedidosEnc (Nolock) " +
	                                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdPersonal = '{2}' And StatusPedido = 'S' ",
                                        cboEmpresas.Data, cboEdo.Data, DtGeneral.IdPersonal);
                }
                else
                {
                    sSql = string.Format(" Select Folio, IdFarmacia, Farmacia, IdTipoPedido, TipoDeClavesDePedido, TipoDeClavesDePedidoDescripcion, Folio, " +
                                        " Convert(varchar (10), FechaRegistro, 120) As Fecha  " + 
                                        " From vw_COM_OCEN_PedidosEnc (Nolock) " +
                                        " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdPersonal = '{2}' And StatusPedido = 'S' ",
                                        cboEmpresas.Data, cboEdo.Data, DtGeneral.IdPersonal);
                }

                if (!leer.Exec(sSql))
                {
                    Error.LogError(leer.MensajeError);
                    General.msjError("Ocurrió un error al obtener la lista de Pedidos.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        GnCompras.GenerarNuevoGUID();
                        grid.LlenarGrid(leer.DataSetClase, false, false);
                        grdPedidosDisponibles.Focus();
                        cboEmpresas.Enabled = false;
                        cboEdo.Enabled = false;
                    }
                    else
                    {
                        General.msjUser("No existe información de pedidos.");
                        bRegresa = false;
                        cboEdo.Focus();
                    }
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
