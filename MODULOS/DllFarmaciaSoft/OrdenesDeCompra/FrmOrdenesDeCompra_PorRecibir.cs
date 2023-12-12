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

namespace DllFarmaciaSoft.OrdenesDeCompra
{
    public partial class FrmOrdenesDeCompra_PorRecibir : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL();

        clsLeer leer;
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();
        //wsFarmacia.wsCnnCliente OrdenesWeb;

        DataSet dtsDatosOC;
        clsGrid Grid;
        //clsListView lst;

        string sUrlServidorOrdenesDeCompra = "";
        bool bServidorCompras_EnLinea = false;
        string sMensajeNoConexion_ServidorCompras = "No fue posible establecer conexión con el Servidor de Ordenes de Compra";

        string sIdEmpresa = DtGeneral.EmpresaConectada; 
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;

        public FrmOrdenesDeCompra_PorRecibir()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            dtsDatosOC = new DataSet();

            Grid = new clsGrid(ref grdOrdenes, this);
            Grid.EstiloGrid(eModoGrid.SeleccionSimple);
            Grid.Limpiar(false);
            Grid.AjustarAnchoColumnasAutomatico = true;

            //lst = new clsListView(lstOC);
            //lst.OrdenarColumnas = true;
            //lst.PermitirAjusteDeColumnas = true;
        }

        private void FrmOrdenesDeCompra_PorRecibir_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            GetUrl_ServidorCompras();
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (ObtenerOC_A_Recepcionar())
            {
                Mostrar_Listado_a_Recepcionar();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            Grid.Limpiar(true);
            
        }
        #endregion Funciones

        #region UrlServidor
        private void GetUrl_ServidorCompras()
        {
            try
            {
                // DtGeneral.UrlServidorCentral = "http://LapJesus/wsAlmacenOX/wsOficinaCentral.asmx";            
                sUrlServidorOrdenesDeCompra = DtGeneral.UrlServidorCentral;
                //OrdenesWeb.Url = sUrlServidorOrdenesDeCompra; //  DtGeneral.UrlServidorCentral;
                //OrdenesWeb.Url = "http://lapfernando/wsPuebla/wsOficinaCentral.asmx"; 

                bServidorCompras_EnLinea = true;
            }
            catch
            {
                bServidorCompras_EnLinea = false;
                General.msjAviso(sMensajeNoConexion_ServidorCompras);
            }

        }
        #endregion UrlServidor

        #region Obtener_OC_Recepcion_Diaria
        private bool ObtenerOC_A_Recepcionar()
        {
            bool bOCDescargadas = true;

            try
            {
                conexionWeb = new wsFarmacia.wsCnnCliente();
                conexionWeb.Url = sUrlServidorOrdenesDeCompra;
                conexionWeb.Timeout = 500000;

                dtsDatosOC = conexionWeb.InformacionRecepcionDiariaOrdenesCompras(sIdEmpresa, sIdEstado, sIdFarmacia);
                
            }
            catch (Exception ex)
            {
                Error.GrabarError(ex.Message, "ObtenerOC_A_Recepcionar");
                General.msjError("Ocurrió un error al Obtener información de Ordenes de Compras.");
                bOCDescargadas = false;
            }

            return bOCDescargadas;
        }

        private void Mostrar_Listado_a_Recepcionar()
        {
            leer.DataSetClase = dtsDatosOC;

            if (leer.Leer())
            {
                Grid.Limpiar(false);
                Grid.LlenarGrid(leer.DataSetClase);
                //lst.CargarDatos(leer.DataSetClase, true, true);
            }
            else
            {
                General.msjAviso("No se encontro información de Ordenes de compras a recepcionar el dia de hoy.");
            }
        }
        #endregion Obtener_OC_Recepcion_Diaria

        #region Impresion
        private void Imprimir()
        {
            if (Grid.Rows > 0)
            {
                Grid.ExportarExcel(true);
            }
            else
            {
                General.msjAviso("No se encontro información para imprimir");
            }
        }
        #endregion Impresion
    }
}
