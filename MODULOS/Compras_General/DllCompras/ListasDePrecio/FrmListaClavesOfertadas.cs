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

namespace DllCompras.ListasDePrecio
{
    public partial class FrmListaClavesOfertadas : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;

        clsAuditoria auditoria;

        public FrmListaClavesOfertadas()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DllCompras.GnCompras.DatosApp, this.Name);            

            grid = new clsGrid(ref grdListaDeClaves, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            grid.AjustarAnchoColumnasAutomatico = true; 


            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                                        DtGeneral.IdPersonal, DtGeneral.IdSesion, GnCompras.Modulo, this.Name, GnCompras.Version);
        }

        private void FrmListaClavesOfertadas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            grid.Limpiar(false);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            LlenaListadoClaves();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        #endregion Botones

        #region Funciones

        private void LlenaListadoClaves()
        {
            string sCadena = "";

            string sSql =
                string.Format(" Select Distinct IdClaveSSA, ClaveSSA, DescripcionClave " +
                              " From vw_COM_OCEN_ListaDePreciosClaves ( Nolock ) " +
                              " Order By DescripcionClave " );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "LlenaListadoClaves");
                General.msjError("Error al buscar el Listado De Claves");
            }
            else 
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontraron Claves ofertadas.");   
                }
                else
                {
                    grid.LlenarGrid(leer.DataSetClase);
                }

                sCadena = sSql.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);
            }
        }

        #endregion Funciones

        private void grdListaDeClaves_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string IdClaveSSA = "";

            IdClaveSSA = grid.GetValue(grid.ActiveRow, 1);

            FrmCom_ListaPrecioClaves Claves = new FrmCom_ListaPrecioClaves();

            Claves.MostrarClaveProveedores(IdClaveSSA);

        }
    }
}
