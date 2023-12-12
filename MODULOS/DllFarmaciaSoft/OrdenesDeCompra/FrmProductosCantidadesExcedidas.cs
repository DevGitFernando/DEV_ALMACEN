using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_ControlsCS;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllFarmaciaSoft.OrdenesDeCompra
{
    public partial class FrmProductosCantidadesExcedidas : FrmBaseExt  
    {

        // clsGrid myGrid;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        // clsConsultas query;

        DataSet dtsProductosDiferencias;
        clsListView lst;

        string sTabla = "";
        public bool ErrorAlValidarSalida = false;
        string sFolioOrden = "";

        public FrmProductosCantidadesExcedidas()
        {
            InitializeComponent();

            ////myGrid = new clsGrid(ref grdProductos, this);
            ////myGrid.EstiloGrid(eModoGrid.ModoRow);
            //dtsProductosDiferencias = DtsProductos;
            lst = new clsListView(lstProductos);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;
        }

        private void FrmProductosConDiferencias_Load(object sender, EventArgs e)
        {
            //myGrid.LlenarGrid(dtsProductosDiferencias); 
        }

        public bool VerificarCantidadesConExceso(DataSet Lotes, string FolioOrden)
        {
            sFolioOrden = FolioOrden;
            return VerificarCantidadesConExceso(Lotes, false);
        }

        public bool VerificarCantidadesConExceso(DataSet Lotes, bool MostrarMsj)
        {
            bool bRegresa = false;
            ErrorAlValidarSalida = false;

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            dtsProductosDiferencias = Lotes;
            //myGrid.Limpiar(false);

            if (GenerarTabla())
            {
                bRegresa = VerificarDatos();
            }

            // Mostrar los Lotes con Errores 
            if (bRegresa && ErrorAlValidarSalida)
            {
                bRegresa = false;
                this.ShowDialog();
            }
            

            return bRegresa;
        }

        #region Funciones y Procedimientos Privados
        private bool GenerarTabla()
        {
            bool bRegresa = true;
            string sSql = string.Format(" Select Top 0 IdEmpresa, IdEstado, IdFarmacia, space(30) As ClaveSSA,  " +
                " IdProducto, CodigoEAN, space(100) As Descripcion, Existencia as Cantidad " +
                " Into {0} From FarmaciaProductos_CodigoEAN ", GetTabla());

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GenerarTabla()");
                General.msjError("Ocurrió un error al Revisar la lista de productos.");
            }
            else
            {
                bRegresa = CargarDatos();
            }

            return bRegresa;
        }

        private bool VerificarDatos()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_ValidaCantidadesExcedidas  " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioOrdenCompraReferencia = '{3}', @Tabla = '{4}' ", // '{0}', '{1}', '{2}', '{3}', '{4}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Fg.PonCeros(sFolioOrden, 8), sTabla);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "VerificarDatos()");
                General.msjError("Ocurrió un error al Revisar la lista de productos.");
            }
            else
            {
                if (leer.Leer())
                {
                    ErrorAlValidarSalida = true;
                    //myGrid.LlenarGrid(leer.DataSetClase);
                    lst.CargarDatos(leer.DataSetClase, true, true);
                }
            }

            return bRegresa;
        }

        private bool CargarDatos()
        {
            bool bRegresa = true;
            string sSql = "";
            clsLeer leerLotes = new clsLeer();


            leerLotes.DataSetClase = dtsProductosDiferencias;
            while (leerLotes.Leer())
            {
                sSql = string.Format("Insert Into {0} ( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, Cantidad ) " +
                    "Select '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'  " +
                    "", sTabla,
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                    leerLotes.Campo("IdProducto"), leerLotes.Campo("CodigoEAN"),
                    leerLotes.Campo("Cantidad"));

                if (leerLotes.CampoInt("Cantidad") > 0)
                {
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private string MarcaTiempo()
        {
            string sRegresa = "";
            DateTime dt = DateTime.Now;

            sRegresa += Fg.PonCeros(dt.Year, 4);
            sRegresa += Fg.PonCeros(dt.Month, 2);
            sRegresa += Fg.PonCeros(dt.Day, 2);
            sRegresa += "_";
            sRegresa += Fg.PonCeros(dt.Hour, 2);
            sRegresa += Fg.PonCeros(dt.Minute, 2);
            sRegresa += Fg.PonCeros(dt.Second, 2);

            return sRegresa;
        }

        private string GetTabla()
        {
            string sRegresa = "tmpCantidadesOC_" + General.MacAddress + "_" + MarcaTiempo();
            sTabla = sRegresa;
            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }
}
