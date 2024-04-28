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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using Farmacia.Ventas;

using DllFarmaciaSoft;
//using DllFarmaciaSoft.QRCode;
using DllFarmaciaSoft.QRCode.GenerarEtiquetas;
//using Almacen.Pedidos.Validacion;
using Farmacia.Transferencias;
using Farmacia.EntradasConsignacion;

namespace Farmacia.Inventario
{
    public partial class FrmFoliosInventarioInicial : FrmBaseExt 
    {
        enum Cols
        {
            Folio = 1, Fuente = 2, Licitacion = 3, Orden = 4, FolioPresup = 5
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeer leer;
        clsConsultas query;
        clsListView lst;
        DataSet dsFolios;

        string sIdFarmacia = ""; 
        string sFarmacia = ""; 
        


        public FrmFoliosInventarioInicial()
        {
            InitializeComponent();

            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmFoliosInventarioInicial");

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            lst = new clsListView(listvwPedidos);
            //dsFolios = new DataSet();

            sIdFarmacia = DtGeneral.FarmaciaConectada;
            sFarmacia = DtGeneral.FarmaciaConectadaNombre;                  
                        
        }        

        private void FrmListaDeSurtidosPedido_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();

            this.Text += " " + "Licitado.";
            
            InicializarPantalla(); 
            //CargarFoliosSurtido(); 
        }
        
        public void CargarFolios(DataSet ds)
        {            
            this.dsFolios = ds;
            this.ShowDialog();
        }
        #region Funciones 
        private void CargarFoliosInv()
        {
            string sSql = "";

            sSql = string.Format("SELECT M.Folio, S.Descripcion AS FuenteFinan, L.Licitacion, M.Orden, M.FolioPresup FROM vw_MovtosInv_Enc M (NOLOCK)\r\n\t\t" +
                "INNER JOIN Ctrl_Licitaciones L (NOLOCK) ON (L.IdLicitacion = M.IdLicitacion)\r\n\t\t" +
                "INNER JOIN CatFarmacias_SubFarmacias S (NOLOCK)\r\n\t\t\t" +
                "ON (S.IdEstado = M.IdEstado AND S.IdFarmacia = M.IdFarmacia AND S.IdSubFarmacia = M.IdFuente)\r\n\t\t" +
                "WHERE M.IdEmpresa = '{0}' AND M.IdEstado = '{1}' AND M.IdFarmacia = '{2}' \r\n\t\t" +
                "AND M.TipoMovto = 'IIC' AND M.MovtoAplicado = 'N'", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia);

            lst.LimpiarItems();

            if (this.dsFolios == null)
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CargarFoliosInv()");
                    General.msjError("Error al consultar folios Inventario Inicial Licitado.");
                    //this.Close();
                }
                else
                {
                    if (leer.Leer())
                    {
                        lst.CargarDatos(leer.DataSetClase);                  
                    }
                }
                
            }
            else
            {                
                lst.CargarDatos(dsFolios);
            }           
        }        
        #endregion Funciones 

        #region Botones Menu 
        private void InicializarPantalla()
        {
            CargarFoliosInv();
        }
        private void btnNuevo_Click( object sender, EventArgs e )
        {
            InicializarPantalla();
        }
        
        private void btnSalir_Click( object sender, EventArgs e )
        {
            this.Close(); 
        }        
        #endregion Botones Menu 

        #region Botones 
        private void btnVerHistorial_Click(object sender, EventArgs e)
        {
            string sFolioInventario = "";
            sFolioInventario = lst.GetValue((int)Cols.Folio);
            FrmInventarioInicialConsignacion f = new FrmInventarioInicialConsignacion();
            f.FolioInventario = Fg.PonCeros(Fg.Right(sFolioInventario, 8), 8);
            f.CargarFolio();

            dsFolios = null;
            InicializarPantalla();
        }     
          
        #endregion Botones 
        
    }
}
