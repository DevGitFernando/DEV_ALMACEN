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

using DllProveedores; 
using DllProveedores.Consultas;
using DllProveedores.Clases;
using DllProveedores.Lotes;

namespace DllProveedores.OrdenesCompra
{
    public partial class FrmOrdComCodigosEAN_Lotes : FrmBaseExt
    {       
        clsGrid Grid;        
        clsDatosCliente DatosCliente;
                
        public string sCodigoEAN = "", sDescripcion = "", sIdClaveSSA = "";
        public int iCantidadReq = 0, iCant_EAN = 0;
        public bool bPrimerLote = false;
        bool bModo = false;

        public DataSet dtsLotes = clsEAN_Lotes.PreparaDtsLotes();

        private enum Cols
        {
            Ninguno = 0,
            IdClaveSSA = 1, CodigoEAN = 2, ClaveLote = 3, MesesCad = 4, FechaEnt = 5, FechaCad = 6, Cantidad = 7 
        }

        DateTime dtpFechaSistema = GnProveedores.FechaOperacionSistema;                

        //public bool bPermitirSacarCaducados = true;

        public FrmOrdComCodigosEAN_Lotes(DataRow[] Rows)
        {
            InitializeComponent();

            Grid = new clsGrid(ref grdLotes, this);
            grdLotes.EditModeReplace = true;
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.Limpiar(false); 
            Grid.AgregarRenglon(Rows, (int)Cols.Cantidad, false);
            DatosCliente = new clsDatosCliente(GnProveedores.DatosApp, this.Name, "");
                        
        }

        private void FrmOrdComCodigosEAN_Lotes_Load(object sender, EventArgs e)
        {
            int iCont = 0;

            bModo = bPrimerLote;
            IniciaPantalla();                   
            ////for (int i = 1; i <= Grid.Rows; i++)
            ////{
            ////    if (Grid.GetValue(i, (int)Cols.CodigoEAN) != "")
            ////        iCont++;
            ////}
            ////if (iCont >= 1)
            ////    DepurarGrid();      
        }

        #region Botones

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string sLote = txtClaveLote.Text.Trim();
            string sCodigoEAN = lblClaveSSA.Text.Trim();

            int dMonth = 0;
            //string sFecha = "";
            scDateTimePicker dtpDiff = new scDateTimePicker();
            //DateTime dtFecha;

            dMonth = (int)dtpDiff.DateDiff(DateInterval.Month, dtpFechaEntrada.Value, dtpFechaCaducidad.Value);

            if (dMonth >= 12)
            {       
                gpoDatosLotes.Visible = false;
                grdLotes.Enabled = true;

                Grid.AddRow();

                Grid.SetValue(Grid.Rows, (int)Cols.IdClaveSSA, sIdClaveSSA);
                Grid.SetValue(Grid.Rows, (int)Cols.CodigoEAN, sCodigoEAN);
                Grid.SetValue(Grid.Rows, (int)Cols.ClaveLote, sLote);
                Grid.SetValue(Grid.Rows, (int)Cols.FechaEnt, General.FechaDMY(dtpFechaEntrada.Value));
                Grid.SetValue(Grid.Rows, (int)Cols.FechaCad, General.FechaDMY(dtpFechaCaducidad.Value));

                ///////////// Poner los Meses a Caducar ////////////////////////////////////////////////////////////////////////////                       

                Grid.SetValue(Grid.Rows, (int)Cols.MesesCad, dMonth);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //Ordernar_RevisarFechaCaducidad();

                grdLotes.Focus();
                Grid.SetActiveCell(Grid.Rows, (int)Cols.Cantidad);
                DepurarGrid();      
                bModo = false;
            }
            else
            {
                General.msjUser("La Caducidad No Puede Ser Menor a 12 Meses");
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {               
            IniciaPantalla();       
        }

        #endregion Botones

        #region Funciones

        public void MostrarEncabezado()
        {
            lblClaveSSA.Text = sCodigoEAN;
            lblDescripcionSSA.Text = sDescripcion;
            lblCantidadRequerida.Text = Convert.ToString(iCantidadReq);            
        }             

        private void IniciaPantalla()
        {
            gpoDatosLotes.Left = grdLotes.Left;
            gpoDatosLotes.Top = grdLotes.Top;
            gpoDatosLotes.Height = grdLotes.Height;
            gpoDatosLotes.Width = grdLotes.Width;            

            if ( bModo )
            {            
                gpoDatosLotes.Visible = false;
                grdLotes.Enabled = true;                
                lblAyuda.Visible = true;                
                MostrarEncabezado();
                bModo = false;
            }
            else
            {
                Fg.IniciaControles(this, true, gpoDatosLotes);
                gpoDatosLotes.Visible = true;
                grdLotes.Enabled = false;       
                lblAyuda.Visible = true;  
                txtClaveLote.Focus();
                dtpFechaEntrada.Enabled = false;   
                MostrarEncabezado();
                bModo = true;
            }

        }
                
        //private void Ordernar_RevisarFechaCaducidad()
        //{
        //    Color colorCaducados = Color.Red;
        //    Color colorPrecaucion = Color.Yellow;
        //    Color colorStatusOk = Color.Green;
        //    Color colorBloqueaCaducados = Color.BurlyWood;
        //    Color colorSalidaCaducados = Color.LightGray;


        //    DateTime dFechaCad = DateTime.Now;
        //    //FarPoint.Win.Spread.SortInfo []mySort = {new FarPoint.Win.Spread.SortInfo((int)Cols.FechaCad, false)};

        //    //// ordernar los Lotes por el mas Próximo a caducar 
        //    //grdLotes.Sheets[0].SortRange( 0, (int)Cols.FechaEnt - 1, myGrid.Rows, 2, true, mySort); 

        //    grdLotes.Sheets[0].SortRows((int)Cols.FechaCad - 1, true, false);

        //    int iMeses = 0;
        //    //for (int i = 1; i <= Grid.Rows; i++)
        //    //{
        //        iMeses = Grid.GetValueInt(Grid.ActiveRow, (int)Cols.MesesCad);

        //        Grid.BloqueaCelda(false, colorStatusOk, Grid.ActiveRow, (int)Cols.Cantidad);                

        //        switch (iMeses)
        //        {
        //            case 0:
        //                Grid.ColorCelda(Grid.ActiveRow, (int)Cols.MesesCad, colorCaducados);
        //                dFechaCad = Grid.GetValueFecha(Grid.ActiveRow, (int)Cols.FechaCad);

        //                if (dtpFechaSistema > dFechaCad)
        //                {
        //                    Grid.ColorCelda(Grid.ActiveRow, (int)Cols.Cantidad, colorSalidaCaducados);
        //                    if (!bPermitirSacarCaducados)
        //                        Grid.BloqueaCelda(true, colorBloqueaCaducados, Grid.ActiveRow, (int)Cols.Cantidad);
        //                }
        //                break;

        //            case 1:     
        //            case 2:
        //            case 3:
        //                Grid.ColorCelda(Grid.ActiveRow, (int)Cols.MesesCad, colorCaducados);
        //                break;

        //            case 4:
        //            case 5:
        //            case 6:
        //                Grid.ColorCelda(Grid.ActiveRow, (int)Cols.MesesCad, colorPrecaucion);
        //                break;

        //            case 7:
        //            case 8:
        //            case 9:
        //                Grid.ColorCelda(Grid.ActiveRow, (int)Cols.MesesCad, colorStatusOk);
        //                break;

        //            default:
        //                if (iMeses < 0)
        //                {
        //                    Grid.ColorCelda(Grid.ActiveRow, (int)Cols.MesesCad, colorCaducados);
        //                    Grid.ColorCelda(Grid.ActiveRow, (int)Cols.Cantidad, colorSalidaCaducados);

        //                    Grid.BloqueaCelda(true, colorBloqueaCaducados, Grid.ActiveRow, (int)Cols.Cantidad);
        //                }
        //                break;
        //            }
        //       //}      
        //}

        private void DepurarGrid()
        {
            ////for (int i = 1; i <= Grid.Rows; i++)
            ////{
            ////    if (Grid.GetValue(i, (int)Cols.CodigoEAN) == "")
            ////        Grid.DeleteRow(i);
            ////}
        }

        #endregion Funciones

        #region Eventos
 
        private void FrmOrdComCodigosEAN_Lotes_KeyDown(object sender, KeyEventArgs e)
        {           

        }
                
        protected override void OnKeyDown(KeyEventArgs e)
        {
            int iCantGrid = 0, iCant = 0;

            switch (e.KeyCode)
            {               
                case Keys.F8:
                    //bPrimerLote = false;
                    IniciaPantalla();
                    break;

                case Keys.F12:
                    iCantGrid = Grid.TotalizarColumna((int)Cols.Cantidad);
                    iCant = Convert.ToInt32(lblCantidadRequerida.Text);
                    if (iCantGrid > iCant)
                    {
                        General.msjUser("No Puede Sobrepasar La Cantidad Requeridad");
                    }
                    else if (iCantGrid < iCant)
                    {
                        General.msjUser("No Esta Completa La Cantidad Requerida");
                    }
                    else
                    {
                        iCant_EAN = iCantGrid;
                        this.Hide();
                    }
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        private void FrmOrdComCodigosEAN_Lotes_FormClosing(object sender, FormClosingEventArgs e)
        {                   
            dtsLotes = clsEAN_Lotes.PreparaDtsLotes();           

            for (int i = 1; i <= Grid.Rows; i++)        
            {
                object[] objRow = {
                     
                    Grid.GetValue(i, (int)Cols.IdClaveSSA),
                    Grid.GetValue(i, (int)Cols.CodigoEAN),          
                    Grid.GetValue(i, (int)Cols.ClaveLote),                   
                    Grid.GetValueInt(i, (int)Cols.MesesCad),  
                    Grid.GetValueFecha(i, (int)Cols.FechaEnt),  
                    Grid.GetValueFecha(i, (int)Cols.FechaCad),  
                    Grid.GetValueInt(i, (int)Cols.Cantidad) };

                if ( Grid.GetValue(i, (int)Cols.CodigoEAN) != "" ) 
                    dtsLotes.Tables[0].Rows.Add(objRow);
                
            }
        }

        #endregion Eventos                    

        

    }
}
        