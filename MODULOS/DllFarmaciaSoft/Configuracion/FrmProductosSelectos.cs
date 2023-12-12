using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Data;

namespace DllFarmaciaSoft.Configuracion
{
    public partial class FrmProductosSelectos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        //string sValorGrid = "";
        clsGrid myGrid;
        clsLeer leer;
        clsAyudas Ayuda;
        clsConsultas Consultas;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        private enum Cols
        {
            Ninguna = 0, ClaveSSA = 1, IdClaveSSA = 2, Descripcion = 3, IdPresentacion = 4, Presentacion = 5
        }

        public FrmProductosSelectos()
        {
            InitializeComponent();
            myGrid = new clsGrid(ref grdProductos, this);
            leer = new clsLeer(ref cnn);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, false);
        }

        private void FrmClavesAltoValor_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        #region Eventos

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            myGrid.Limpiar(true);
            CargarInformacion();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bRegresa = true;
            string sSql = "", sIdClaveSSA = "";

            EliminarRenglonesVacios();

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();


                sSql = string.Format("Update CFG_ProductosSelectos Set Status = 'C' Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}'",
                                        sEmpresa, sEstado, sFarmacia);

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                }

                for (int i = 1; i <= myGrid.Rows && bRegresa; i++)
                {
                    sIdClaveSSA = myGrid.GetValue(i, (int)Cols.IdClaveSSA);

                    if (sIdClaveSSA != "")
                    {
                        sSql = string.Format("Exec spp_Mtto_ProductosSelectos '{0}', '{1}', '{2}', '{3}'", sEmpresa, sEstado, sFarmacia, sIdClaveSSA);

                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                        }
                    }
                }

                if (!bRegresa)
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnGuardar_Click");
                    General.msjError("Ocurrió un error al grabar la información.");
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("La Información se guardó satisfactoriamente...");
                    btnNuevo_Click(null, null);
                }

                cnn.Cerrar();
            }
            else
            {
                Error.LogError(cnn.MensajeError);
                General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
            }
        }
        #endregion Botones

        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F1)
            {
                //sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA);
                leer.DataSetClase = Ayuda.ClavesSSA_Sales("grdProductos_KeyDown()");
                if (leer.Leer())
                {
                    //myGrid.SetValue(myGrid.ActiveRow, 1, leer.Campo("Codigo"));
                    ObtenerDatosClave();
                }
            }

            if (e.KeyCode == Keys.Delete)
            {
                myGrid.DeleteRow(myGrid.ActiveRow);

                if (myGrid.Rows == 0)
                {
                    myGrid.Limpiar(true);
                }
            }
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            string sValor = "";
            sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA);

            if (sValor != "")
            {
                // leer.DataSetClase = Consultas.ClavesSSA_Sales(sValor, true, "grdProductos_EditModeOff"); 
                leer.DataSetClase = Consultas.ClavesSSA_Sales(sValor, "grdProductos_EditModeOff");
                if (!leer.Leer())
                {
                    // General.msjUser("La Clave SSA ingresada no existe. Verifique."); 
                    General.msjUser("Clave no encontrado, verifique.");
                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
                    myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.ClaveSSA);
                }
                else
                {
                    ObtenerDatosClave();
                }
            }
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
            {
                if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.IdClaveSSA) != "" && myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Descripcion) != "")
                {
                    myGrid.Rows = myGrid.Rows + 1;
                    myGrid.ActiveRow = myGrid.Rows;
                    myGrid.SetActiveCell(myGrid.Rows, 1);
                }
            }
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            //sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA);
        }

        #endregion Eventos

        #region Funciones y procedimientos

        private void ObtenerDatosClave()
        {
            int iRowActivo = myGrid.ActiveRow;
            int iColActiva = (int)Cols.IdClaveSSA;
            string sIdClaveSSA = leer.Campo("IdClaveSSA_Sal");

            if (!myGrid.BuscaRepetido(sIdClaveSSA, iRowActivo, iColActiva))
            {
                myGrid.SetValue(iRowActivo, iColActiva, sIdClaveSSA);
                myGrid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, leer.Campo("IdClaveSSA_Sal"));
                myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, leer.Campo("DescripcionClave"));

                myGrid.SetValue(iRowActivo, (int)Cols.IdPresentacion, leer.Campo("IdPresentacion"));
                myGrid.SetValue(iRowActivo, (int)Cols.Presentacion, leer.Campo("Presentacion"));
            }
            else
            {
                General.msjUser("La Clave ya se encuentra capturada en otro renglon.");
                myGrid.SetValue(myGrid.ActiveRow, 1, "");
                myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                myGrid.EnviarARepetido();
            }

            grdProductos.EditMode = false;
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                if (myGrid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                {
                    myGrid.DeleteRow(i);
                }
            }

            if (myGrid.Rows == 0) // Si No existen renglones, se inserta 1.
            {
                myGrid.AddRow();
            }
        }

        private void CargarInformacion()
        {
            string sSql = string.Format("Select ClaveSSA, IdClaveSSA_Sal, DescripcionSal, IdPresentacion, Presentacion From vw_ProductosSelectos " +
                                        "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}'", sEmpresa, sEstado, sFarmacia);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarInformacion()");
                General.msjError("Ocurrió un error al obtener la información.");
            }
            else
            {
                myGrid.LlenarGrid(leer.DataSetClase);
            }
        }

        #endregion Funciones y procedimientos
    }
}
