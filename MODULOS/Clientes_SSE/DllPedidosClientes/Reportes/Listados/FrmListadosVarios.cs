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

using DllPedidosClientes;

namespace DllPedidosClientes.Reportes
{
    public partial class FrmListadosVarios : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente DatosCliente;
        // clsGrid myGrid;
        clsLeer myLeer;

        clsListView list; 
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();
        
        public FrmListadosVarios()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "");
            conexionWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            conexionWeb.Url = General.Url;
            myLeer = new clsLeer(ref ConexionLocal);
            
            list = new clsListView(lstProductos);
            list.OrdenarColumnas = true; 
        }

        private void FrmListadosVarios_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
            // ObtenerProductos(); 
        }

        #region Funciones 
        private void CargarListados()
        {
            cboListados.Clear();
            cboListados.Add();

            cboListados.Add("1", "Listado de productos de uso exclusivo sector salud");
            cboListados.Add("2", "Listado de productos con registro sanitario");
            ////cboListados.Add("3", "Listado de cuadros basicos de farmacia");
            cboListados.Add("4", "Listado de productos antibioticos");
            cboListados.Add("5", "Listado de productos controlados");
            cboListados.Add("6", "Listado de productos refrigerados");
            cboListados.SelectedIndex = 0;
        }

        private void cboListados_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnImprimir.Enabled = false;
            list.Limpiar();
        }

        #endregion Funciones        

        #region Botones  
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(); 
            btnImprimir.Enabled = false;
            list.Limpiar();
            list.LimpiarItems();

            CargarListados(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            list.Limpiar(); 
            list.LimpiarItems();
            ObtenerProductos();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DatosCliente.Funcion = "btnImprimir_Click()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false;
            string sNombreReporte = "";

            myRpt.RutaReporte = DtGeneralPedidos.RutaReportes;
            myRpt.NombreReporte = "CteReg_Admon_Listado_Productos_Sector_Salud";
            //myRpt.NombreReporte = Reporte();

            switch (cboListados.Data)
            {
                case "1":
                    sNombreReporte = "CteReg_Admon_Listado_Productos_Sector_Salud";
                    break;

                case "2":
                    sNombreReporte = "CteReg_Admon__Productos_Registros_Sanitarios";
                    break;

                case "3":
                    {
                        myRpt.Add("Empresa", DtGeneralPedidos.EncabezadoPrincipal);
                        myRpt.Add("IdEstado", DtGeneralPedidos.EstadoConectado);
                        sNombreReporte = "Ptovta_CB_PorEstado";
                    }
                    break;

                case "4":
                    sNombreReporte = "Central_Listado_Productos_Antibioticos";
                    break;

                case "5":
                    sNombreReporte = "Central_Listado_Productos_Controlados";
                    break;
                        
                case "6":
                    sNombreReporte = "Central_Listado_Productos_Refrigerados";
                    break;
            }

            myRpt.NombreReporte = sNombreReporte;

            DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            DataSet datosC = DatosCliente.DatosCliente();
            bRegresa = DtGeneralPedidos.GenerarReporte(General.Url, myRpt, "", "", InfoWeb, datosC, true); 

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Botones 

        #region Obtener Datos 
        private void ObtenerProductos()
        {
            string sSql = ArmarSelect();

            if (cboListados.SelectedIndex == 0)
            {
                General.msjAviso("No ha seleccionado un listado válido.");
            }
            else
            {
                if (!myLeer.Exec(sSql))
                {
                    General.msjUser("Ocurrió un error al obtener la información");
                }
                else
                {
                    if (!myLeer.Leer())
                    {
                        General.msjUser("No existe informacion para mostrar. Verifique");
                    }
                    else
                    {
                        btnImprimir.Enabled = true;
                        list.CargarDatos(myLeer.DataSetClase, true, true);
                    }
                }
            }
        }

        private string ArmarSelect()
        {
            string sRegresa = "";

            switch (cboListados.Data)
            {
                case "1": 
                    sRegresa = "Select 'Clave SSA' = ClaveSSA, 'Descripción Clave' = DescripcionClave, CodigoEAN, " + 
                        "'Nombre comercial' = Descripcion, Presentacion, Laboratorio " + 
                        "From vw_Productos_CodigoEAN " +  
                        "Where EsSectorSalud = 1 " + 
                        "Order By DescripcionClave "; 
                    break;

                case "2":
                    sRegresa = "Select 'Clave SSA' = ClaveSSA, CodigoEAN, RegistroSanitario, " + 
                        "'Nombre comercial' = Descripcion, Presentacion, Laboratorio " + 
                        "From vw_Productos_RegistrosSanitarios " + 
                        "Order By DescripcionClave ";
                    break; 

                case "3":
                    sRegresa = string.Format("Select 'Clave SSA' = ClaveSSA, 'Descripción' =  DescripcionClave, 'Presentación' = Presentacion " +
                    "From vw_CB_CuadroBasico_Claves CB (NoLock) " +
                    "Where StatusMiembro =  'A' and StatusClave = 'A' and CB.idestado = '{0}' " +
                    "Group by ClaveSSA, DescripcionClave, Presentacion " +
                    "Order by DescripcionClave ",
                    DtGeneralPedidos.EstadoConectado);
                    break;

                case "4":
                    sRegresa = "Select IdProducto, ClaveSSA, Descripcion " +
                    "From vw_Productos " +
                    "Where EsAntibiotico = 1 " +
                    "Order By ClaveSSA";
                    break;

                case "5":
                    sRegresa = "Select IdProducto, ClaveSSA, Descripcion " +
                    "From vw_Productos " +
                    "Where EsControlado = 1 " +
                    "Order By ClaveSSA";
                    break;

                case "6":
                    sRegresa = "Select IdProducto, ClaveSSA, Descripcion " +
                    "From vw_Productos " +
                    "Where IdSegmento = '01' " +
                    "Order By ClaveSSA";
                    break; 
            }

            return sRegresa; 
        }        

        #endregion Obtener Datos         

    }
}
