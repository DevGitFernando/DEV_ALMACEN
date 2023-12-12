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

using DllFarmaciaSoft;

namespace Configuracion.Configuracion
{
    public partial class FrmParametrosOC : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;

        public FrmParametrosOC()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Grid = new clsGrid(ref grdParametros, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnConfiguracion.DatosApp, this.Name);
        }

        private void FrmParametrosOC_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            Grid.Limpiar(false); 
            CargarModulos(); 
        }

        private void CargarModulos()
        {
            string sSql = string.Format("Select distinct ArbolModulo as Arbol, M.Nombre as Modulo " +
                "From Net_CFGS_Parametros P (NoLock) " +
                "Inner Join Net_Arboles M (NoLock) On ( P.ArbolModulo = M.Arbol )");

            cboModulo.Clear();
            cboModulo.Add();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarModulos");
            }
            else
            {
                cboModulo.Add(leer.DataSetClase, true, "Arbol", "Modulo");
            }

            cboModulo.SelectedIndex = 0;
        }

        private void CargarParametros()
        {
            // Cargar los Parametros del sistema 
            clsParametrosOficinaCentral Parametros = new clsParametrosOficinaCentral(General.DatosConexion, GnConfiguracion.DatosApp, cboModulo.Data);
            Parametros.CargarParametros(); 

            string sSql = string.Format("Select ArbolModulo, NombreParametro, Valor, Descripcion " + 
                " From Net_CFGS_Parametros (NoLock) " + 
                " Where ArbolModulo = '{0}' " +
                " Order by ArbolModulo, NombreParametro ", cboModulo.Data); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargandoDatos"); 
                General.msjError("Ocurrió un error al obtener la lista de parametros.");
            }
            else
            {
                Grid.Limpiar(false);
                Grid.LlenarGrid(leer.DataSetClase);
                CargaDescripcion(1);
            } 
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool bExito = true;
            string sSql = "";
            string sArbol = "", sParametro = "", sValor = "";

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                for (int i = 1; i <= Grid.Rows; i++)
                {
                    sArbol = Grid.GetValue(i, 1);
                    sParametro = Grid.GetValue(i, 2);
                    sValor = Grid.GetValue(i, 3);

                    sSql = string.Format(" Exec spp_Mtto_Net_CFGS_Parametros '{0}', '{1}', '{2}', '', 1 ", sArbol, sParametro, sValor);
                    if (!leer.Exec(sSql))
                    {
                        bExito = false;
                        break;
                    }
                }


                if (bExito)
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamente.");
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnAceptar_Click");
                    General.msjError("Ocurrió un error al grabar la información.");
                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso("No se pudo establecer conexión con el servidor. Intente de nuevo");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdParametros_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            CargaDescripcion(e.NewRow + 1);
        }

        private void CargaDescripcion(int Renglon)
        {
            lblDescripcion.Text = Grid.GetValue(Renglon, 4);
        }

        private void cboModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboModulo.SelectedIndex != 0)
            {
                CargarParametros(); 
            }
        }
    }
}
