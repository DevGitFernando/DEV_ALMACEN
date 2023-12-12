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

using DllFarmaciaSoft;

namespace Farmacia.Vales
{
    public partial class FrmValeCantidadesExcedidas : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        // clsConsultas query;

        DataSet dtsProductosDiferencias;
        clsListView lst;

        string sTabla = "";
        public bool ErrorAlValidarSalida = false;
        string sFolio = "";

        public FrmValeCantidadesExcedidas()
        {
            InitializeComponent();

            lst = new clsListView(lstProductos);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;
        }

        private void FrmValeCantidadesExcedidas_Load(object sender, EventArgs e)
        {

        }

        public bool VerificarCantidadesConExceso(DataSet Lotes, string FolioVale)
        {
            bool bRegresa = false;
            ErrorAlValidarSalida = false;

            sFolio = FolioVale;

            cnn = new clsConexionSQL(General.DatosConexion);
            myLeer = new clsLeer(ref cnn);
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

            BorrarTabla();

            return bRegresa;
        }

        private void BorrarTabla()
        {
            string sSql = string.Format("Drop Table {0} ", sTabla);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "BorrarTabla()");
            }

        }

        private bool VerificarDatos()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_ValidaCantidadesExcedidas_RegistroDeVales '{0}', '{1}', '{2}', '{3}', '{4}' ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Fg.PonCeros(sFolio, 8), sTabla);

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(myLeer, "VerificarDatos()");
                General.msjError("Ocurrió un error al Revisar la lista de productos.");
            }
            else
            {
                if (myLeer.Leer())
                {
                    ErrorAlValidarSalida = true;
                    //myGrid.LlenarGrid(leer.DataSetClase);
                    lst.CargarDatos(myLeer.DataSetClase, true, true);
                }
            }

            return bRegresa;
        }

        private bool GenerarTabla()
        {
            bool bRegresa = true;
            string sSql = string.Format(" Select Top 0 IdEmpresa, IdEstado, IdFarmacia, space(30) As ClaveSSA,  " +
                " IdProducto, CodigoEAN, space(100) As Descripcion, Existencia as Cantidad " +
                " Into {0} From FarmaciaProductos_CodigoEAN ", GetTabla());

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "GenerarTabla()");
                General.msjError("Ocurrió un error al Revisar la lista de productos.");
            }
            else
            {
                bRegresa = CargarDatos();
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
                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }
            return bRegresa;
        }


        private string GetTabla()
        {
            string sRegresa = "tmpCantidadesVale_" + General.MacAddress + "_" + MarcaTiempo();
            sTabla = sRegresa;
            return sRegresa;
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
    }
}
