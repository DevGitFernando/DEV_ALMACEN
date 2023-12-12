using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;

namespace Inventarios.InventariosAleatorios
{
    public partial class FrmClavesBloqueadas : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);       
        clsLeer leer;
        clsListView lst;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmClavesBloqueadas()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            lst = new clsListView(lstClaves);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;
        }

        private void FrmClavesBloqueadas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarClaves();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            IniciaToolBar(true, true);
            lst.Limpiar();
        }

        private void IniciaToolBar(bool Nuevo, bool Ejecuta)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Nuevo;
        }

        private void CargarClaves()
        {
            string sSql = "";       
            
            sSql = string.Format(" Select Distinct I.Folio, 'Clave SSA' = I.ClaveSSA, " + 
                                " 'Descripción' = ( Select top 1 DescripcionSal From vw_ProductosExistenEnEstadoFarmacia S (Nolock) " + 
                                         "    Where S.ClaveSSA = I.ClaveSSA  )  " + 
                                " From INV_AleatoriosDet I (Nolock) " + 
                                " Inner Join vw_ProductosExistenEnEstadoFarmacia P (Nolock) On (P.ClaveSSA = I.ClaveSSA) " +
                                " Where I.IdEstado = '{0}' and I.IdFarmacia = '{1}' and P.StatusDeProducto = 'S' " + 
                                " Order By I.Folio, I.ClaveSSA ", sEstado, sFarmacia);

            lst.Limpiar();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargaClaves()");
                General.msjError("Ocurrió un error al obtener las Claves.");
            }
            else
            {
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                }
                else
                {
                    General.msjAviso("No se encontraron Claves Bloqueadas.");
                }
            }
        }
        #endregion Funciones
    }
}
