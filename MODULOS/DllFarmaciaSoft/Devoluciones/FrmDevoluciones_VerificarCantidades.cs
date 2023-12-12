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

namespace DllFarmaciaSoft.Lotes
{
    internal partial class FrmDevoluciones_VerificarCantidades : FrmBaseExt 
    {
        enum Cols
        { 
            IdProducto = 1, CodigoEAN = 2, Descripcion = 3, EAN = 4, Lotes = 5, Ubicaciones = 6 
        } 

        DataSet dtsProductos; 
        clsGrid myGrid;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        // clsConsultas query;

        string sTabla = "";
        public bool ErrorAlValidarSalida = false;

        public FrmDevoluciones_VerificarCantidades(DataSet ListaDeProductos)
        {
            InitializeComponent();

            dtsProductos = ListaDeProductos; 
            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow); 

            if ( !DtGeneral.EsAlmacen ) 
            {
                int iAncho = (int)grdProductos.Sheets[0].Columns[((int)Cols.Ubicaciones)-1].Width;
                grdProductos.Sheets[0].Columns[((int)Cols.Ubicaciones)-1].Width = 0;
                grdProductos.Sheets[0].Columns[((int)Cols.Descripcion) - 1].Width += iAncho; 
            }
        }

        private void FrmDevoluciones_VerificarCantidades_Load(object sender, EventArgs e)
        {
            myGrid.LlenarGrid(dtsProductos); 
        }

        public void MostrarProductosConIncidencias()
        {
            this.ShowDialog(); 
        } 

        #region Funciones y Procedimientos Privados        
        #endregion Funciones y Procedimientos Privados

    }
}
