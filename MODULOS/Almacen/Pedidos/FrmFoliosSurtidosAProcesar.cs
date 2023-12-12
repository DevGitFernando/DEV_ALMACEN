using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;

using Farmacia.Transferencias;
using Farmacia.Ventas;
using Farmacia.EntradasConsignacion;

using DllFarmaciaSoft;

namespace Almacen.Pedidos
{
    public partial class FrmFoliosSurtidosAProcesar : FrmBaseExt
    {

        enum Cols
        {
            FolioSurtido = 1, FechaRegistro = 2, Procesar = 3
        }

        string sFolioPedido = "";
        string sFoliosSurtidos = "";

        TipoDePedidoElectronico tTipoDePedido = TipoDePedidoElectronico.Ninguno;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid myGrid;

        public FrmFoliosSurtidosAProcesar()
        {
            InitializeComponent();

            leer = new clsLeer(ref ConexionLocal);
            myGrid = new clsGrid(ref grdFolios, this);
            myGrid.AjustarAnchoColumnasAutomatico = true;

            txtFolio.Focus(); 
        }

        private void FrmFoliosSurtidosAProcesar_Load(object sender, EventArgs e)
        {
            CargarSurtido();
        }

        public void ListaSurtidos(string FolioPedido, TipoDePedidoElectronico TipoDePedido)
        {
            sFolioPedido = FolioPedido;
            tTipoDePedido = TipoDePedido;

            txtFolio.Text = FolioPedido;

            this.ShowDialog();
        }

        private bool CargarSurtido()
        {
            bool bRegresa = true; 
            string sSql = ""; 
            
            sSql = string.Format(
                "Select FolioSurtido, Convert(Varchar(10), FechaRegistro, 120) As FechaRegistro, 1 As Procesar \n" +
                "From Pedidos_Cedis_Enc_Surtido P(NoLock) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioPedido = '{3}' And Status = 'D' \n",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarSurtidos()");
                General.msjError("Ocurró un error al cargar los Folio de surtido.");
                this.Close();
            }
            else
            {
                myGrid.Limpiar(false);

                if(leer.Leer())
                {
                    myGrid.LlenarGrid(leer.DataSetClase);
                    myGrid.BloqueaGrid(!GnFarmacia.Pedidos_ModificarFoliosSurtidos);
                }
                else
                {
                    bRegresa = false;
                    General.msjUser("No se encontraron Folios de surtido disponibles para este proceso.");
                    this.Close();
                }
            }

            return bRegresa;
        }

        private void btnObtenerTransferencias_Click(object sender, EventArgs e)
        {
            sFoliosSurtidos = "";
            int iFolios = 0;

            for (int i = 1; myGrid.Rows >= i; i++)
            {
                if (myGrid.GetValueBool(i, Cols.Procesar))
                {
                    if (sFoliosSurtidos != "")
                    {
                        sFoliosSurtidos += ", ";
                    }

                    sFoliosSurtidos += myGrid.GetValue(i, Cols.FolioSurtido);
                    iFolios += 1;
                }
            }

            if (iFolios == 0)
            {
                General.msjAviso("Folio de Pedido inválido, verifique.");
            }
            else
            {
                switch (tTipoDePedido)
                {
                    case TipoDePedidoElectronico.Transferencias:
                    case TipoDePedidoElectronico.Transferencias_InterEstatales:
                        FrmTransferenciaSalidas_Base F = new FrmTransferenciaSalidas_Base();
                        F.CargarPedido(sFoliosSurtidos, sFolioPedido, iFolios);
                        break;

                    case TipoDePedidoElectronico.Ventas:
                        FrmVentas V = new FrmVentas();
                        V.CargaDetallesGeneraVenta(sFarmacia, sFolioPedido, sFoliosSurtidos, iFolios);
                        break;

                    case TipoDePedidoElectronico.SociosComerciales:
                        FrmSalidasVentas_Comerciales C = new FrmSalidasVentas_Comerciales();
                        C.CargaDetallesGeneraVenta(sFarmacia, sFolioPedido, sFoliosSurtidos, iFolios);
                        break;
                }

                if(!CargarSurtido())
                {
                    this.Close();
                }
            }
        }
    }
}
