using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;

namespace Almacen.Pedidos.Validacion
{
    public partial class FrmPedidosValidacionCiegaLotes : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna,
            Codigo, CodigoEAN, ClaveLote, Cantidad
        }
        Cols ColActiva = Cols.Ninguna;

        public string sIdEmpresa = "";
        public string sIdEstado = "";
        public string sIdFarmacia = "";
        public string sIdArticulo = "";
        public string sCodigoEAN = "";
        public string sDescripcion = "";

        public int iTotalCantidad = 0;

        clsGrid myGrid;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerSF;
        clsConsultas query;

        public DataSet dtsLotes = clsLotes.PreparaDtsLotes();

        public FrmPedidosValidacionCiegaLotes()
        {
            InitializeComponent();

            myGrid = new clsGrid(ref grdLotes, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
        }

        private void FrmPedidosValidacionCiegaLotes_Load(object sender, EventArgs e)
        {
            CargarInformacionDelProducto();
            myGrid.SetActiveCell(1, (int)Cols.ClaveLote);
            //myGrid.LlenarGrid(dtsLotes);

        }

        public FrmPedidosValidacionCiegaLotes(DataRow[] Rows)
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerSF = new clsLeer();
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);


            grdLotes.EditModeReplace = true;
            myGrid = new clsGrid(ref grdLotes, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);


            lblTotal.Text = "0";

            leer.DataRowsClase = Rows;

            myGrid.LlenarGrid(leer.DataSetClase, false, true);

            for (int i = 1; myGrid.Rows >= i; i++)
            {
                myGrid.BloqueaCelda(true, i, (int)Cols.ClaveLote);
            }

            if (myGrid.Rows == 0 ) myGrid.Limpiar(true);

            lblTotal.Text = myGrid.TotalizarColumna((int)Cols.Cantidad).ToString();
            
            //myGrid.AgregarRenglon(Rows, (int)Cols.Cantidad, false, true);


        }

        private void FrmPedidosValidacionCiegaLotes_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    Salir_ValidarCaptura_Actualizar_Informacion();
                    break;

                default:
                    break;
            }
        }

        private void Salir_ValidarCaptura_Actualizar_Informacion()
        {
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                if (myGrid.GetValue(i, (int)Cols.ClaveLote) == "")
                    myGrid.DeleteRow(i);
            }

            Actualizar_Datos_De_Salida();

            this.Close();
        }

        private void Actualizar_Datos_De_Salida()
        {
            dtsLotes = ClsValidarLotes.PreparaDtsLotes();

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                object[] objRow = {
                    sIdArticulo,
                    sCodigoEAN,
                    myGrid.GetValue(i, (int)Cols.ClaveLote),
                    myGrid.GetValueInt(i, (int)Cols.Cantidad) };
                dtsLotes.Tables[0].Rows.Add(objRow);
            }
            iTotalCantidad = myGrid.TotalizarColumna((int)Cols.Cantidad);
            lblTotal.Text = iTotalCantidad.ToString();
        }

        private void grdLotes_EditModeOff(object sender, EventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            bool bEsEAN_Unico = true;
            string sLote = "";
            int iRow = 0;
            int iCol = 0;

            switch (ColActiva)
            {
                case Cols.ClaveLote:
                    sLote = myGrid.GetValue(myGrid.ActiveRow, Cols.ClaveLote);
                    iRow = myGrid.ActiveRow;
                    iCol = (int)Cols.ClaveLote;

                    if (myGrid.BuscaRepetido(sLote, iRow, iCol))
                    {
                        General.msjUser("El Lote ya fue capturado en otro renglon, verifique.");
                        myGrid.LimpiarRenglon(iRow);
                        myGrid.SetActiveCell(iRow, iCol);
                        myGrid.EnviarARepetido();
                    }
                    break;
                case Cols.Cantidad:
                    lblTotal.Text = myGrid.TotalizarColumna((int)Cols.Cantidad).ToString();
                    break;
            }
        }

        private void CargarInformacionDelProducto()
        {
            leer.DataSetClase = query.Productos_CodigosEAN_Datos(sCodigoEAN, "CargarInformacionDelProducto()");
            if (leer.Leer())
            {
                lblCodigo.Text = leer.Campo("IdProducto");
                lblCodigoEAN.Text = leer.Campo("CodigoEAN");
                lblArticulo.Text = leer.Campo("Descripcion");
                lblClaveSSA.Text = leer.Campo("ClaveSSA_Aux");
                lblDescripcionSSA.Text = leer.Campo("DescripcionSal");

                lblPresentacion.Text = leer.Campo("Presentacion");
                lblContenido.Text = leer.Campo("ContenidoPaquete");


                lblPresentacion.Text = leer.Campo("Presentacion");
                lblContenido.Text = leer.Campo("ContenidoPaquete");
            }

            //CargarInformacion_ProgramaSubPrograma__ControlDispensacion();
        }

        private void grdLotes_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveLote) != "")
            {
                myGrid.BloqueaCelda(true, myGrid.ActiveRow, (int)Cols.ClaveLote);
                myGrid.Rows = myGrid.Rows + 1;
                myGrid.ActiveRow = myGrid.Rows;
                myGrid.SetActiveCell(myGrid.Rows, (int)Cols.ClaveLote);
            }
        }
    }
}
