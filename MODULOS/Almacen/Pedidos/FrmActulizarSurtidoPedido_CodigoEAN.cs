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

namespace Almacen.Pedidos
{
    public partial class FrmActulizarSurtidoPedido_CodigoEAN : FrmBaseExt
    {
        private enum ColsDetalles
        {
            Ninguna = 0,
            Id, IdSurtimiento, ClaveSSA, DescripcionSal, IdSubFarmacia, IdProducto, CodigoEAN, Descripcion, Presentacion, 
            SKU, 
            Lote, Caducidad,
            IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño,

            Caja, Caja_Final, 


            Cant_Requerida_Caja, Cant_Requerida, Cant_Asignada,
            Observaciones, Validado, Segmento

        }

        clsGrid GridDetalles;
        DataSet dtsDetalles;
        string sCodigoEAN;

        int iTipoDeCaptura = 0;
        Color cColor_Validado = Color.GreenYellow;
        Color cColor_NoValidado = Color.White;


        int idUnique = 0;
        DataSet datosEmbalaje;

        public DataSet InformacionEmbalaje
        {
            get { return datosEmbalaje.Copy(); } 
        }

        public FrmActulizarSurtidoPedido_CodigoEAN(DataSet dtsDatos, string CodigoEAN, int TipoDeCaptura, DataSet CajasEmbalaje )
        {
            InitializeComponent();

            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(1200, 400);

            General.Pantalla.AjustarTamaño(this, 90, 90);

            GridDetalles = new clsGrid(ref grdDetalles, this);
            GridDetalles.OcultarColumna(true, ColsDetalles.CodigoEAN);
            GridDetalles.AjustarAnchoColumnasAutomatico = true;



            txtCodifoEAN.Text = sCodigoEAN = CodigoEAN;
            dtsDetalles = dtsDatos;
            datosEmbalaje = CajasEmbalaje;

            iTipoDeCaptura = TipoDeCaptura; 


        }

        private void FrmActulizarSurtidoPedido_CodigoEAN_Load(object sender, EventArgs e)
        {
            Iniciar();
        }

        protected override void OnKeyDown( KeyEventArgs e )
        {
            switch(e.KeyCode)
            {
                case Keys.F12:
                    this.Hide(); 
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        private void Iniciar()
        {
            GridDetalles.Limpiar(false);
            txtCodifoEAN.Enabled = false;
            Cargar();
        }

        private void Cargar()
        {
            clsLeer datosClave = new clsLeer();

            datosClave.DataRowsClase = dtsDetalles.Tables[0].Select(string.Format(" CodigoEAN = '{0}'", sCodigoEAN));
            GridDetalles.Limpiar(false);
            //GridDetalles.LlenarGrid(datosClave.DataSetClase, false, true);
            
            try
            {
                GridDetalles.AgregarRenglon(datosClave.DataRowsClase, dtsDetalles.Tables[0].Columns.Count, false);
                VerificarValidado(); 
            }
            catch { }

            lblDescripcionSal.Text = GridDetalles.GetValue(1, (int)ColsDetalles.ClaveSSA) + " -- " + GridDetalles.GetValue(1, (int)ColsDetalles.DescripcionSal);

            GridDetalles.OcultarColumna(iTipoDeCaptura != 3, (int)ColsDetalles.Validado, (int)ColsDetalles.Validado);
        }

        private void grdDetalles_EditModeOff(object sender, EventArgs e)
        {
            int iRow = GridDetalles.ActiveRow;
            int iValor = 0;

            switch (GridDetalles.ActiveCol)
            {
                case (int)ColsDetalles.Cant_Asignada:
                    {
                        int iCant_Requerida = GridDetalles.GetValueInt(iRow, (int)ColsDetalles.Cant_Requerida);
                        int iCant_Asignada = GridDetalles.GetValueInt(iRow, (int)ColsDetalles.Cant_Asignada);
                        int iCant_Asignada_por_Sal = GridDetalles.TotalizarColumna((int)ColsDetalles.Cant_Asignada);

                        if (iCant_Asignada > iCant_Requerida)
                        {
                            General.msjAviso("Cantidad Asignada no puede ser mayor a la cantidad Requerida, verifique.");
                            GridDetalles.SetValue(iRow, (int)ColsDetalles.Cant_Asignada, iCant_Requerida);
                            GridDetalles.SetActiveCell(iRow, (int)ColsDetalles.Cant_Asignada);
                        }
                    }
                    break;

                case (int)ColsDetalles.Caja:
                    {
                        iValor = GridDetalles.GetValueInt(iRow, (int)ColsDetalles.Caja);
                        GridDetalles.SetValue(iRow, (int)ColsDetalles.Caja, iValor);
                        ////CargarCajas();
                    }
                    break;
            }
        }

        public DataSet ObtenerDatosDetalles()
        {
            dtsDetalles.Tables[0].Rows.Clear();
            int iCaja = 0;

            try
            {
                for (int i = 1; i <= GridDetalles.Rows; i++)
                {
                    iCaja = GridDetalles.GetValueInt(i, (int)ColsDetalles.Caja);
                    object[] objRow = 
                    {
                        GridDetalles.GetValueInt(i, (int)ColsDetalles.Id),
                        GridDetalles.GetValueInt(i, (int)ColsDetalles.IdSurtimiento),  
                        GridDetalles.GetValue(i, (int)ColsDetalles.ClaveSSA),  
                        GridDetalles.GetValue(i, (int)ColsDetalles.DescripcionSal),  
                        GridDetalles.GetValue(i, (int)ColsDetalles.IdSubFarmacia),  
                        GridDetalles.GetValue(i, (int)ColsDetalles.IdProducto), 
                        GridDetalles.GetValue(i, (int)ColsDetalles.CodigoEAN),
                        GridDetalles.GetValue(i, (int)ColsDetalles.Descripcion),
                        GridDetalles.GetValue(i, (int)ColsDetalles.Presentacion),
                        GridDetalles.GetValue(i, (int)ColsDetalles.SKU),
                        GridDetalles.GetValue(i, (int)ColsDetalles.Lote),  
                        GridDetalles.GetValue(i, (int)ColsDetalles.Caducidad), 

                        GridDetalles.GetValue(i, (int)ColsDetalles.IdPasillo), 
                        GridDetalles.GetValue(i, (int)ColsDetalles.Pasillo),
                        GridDetalles.GetValue(i, (int)ColsDetalles.IdEstante),  
                        GridDetalles.GetValue(i, (int)ColsDetalles.Estante),  
                        GridDetalles.GetValue(i, (int)ColsDetalles.IdEntrepaño), 
                        GridDetalles.GetValue(i, (int)ColsDetalles.Entrepaño),

                        GridDetalles.GetValueInt(i, (int)ColsDetalles.Caja),
                        GridDetalles.GetValueInt(i, (int)ColsDetalles.Caja_Final), 

                        GridDetalles.GetValueInt(i, (int)ColsDetalles.Cant_Requerida_Caja),
                        GridDetalles.GetValueInt(i, (int)ColsDetalles.Cant_Requerida), 
                        GridDetalles.GetValueInt(i, (int)ColsDetalles.Cant_Asignada), 
                        GridDetalles.GetValue(i, (int)ColsDetalles.Observaciones), 
                        GridDetalles.GetValueInt(i, (int)ColsDetalles.Validado),
                        GridDetalles.GetValueInt(i, (int)ColsDetalles.Segmento)
                    };

                    dtsDetalles.Tables[0].Rows.Add(objRow);
                }
            }
            catch { }

            return dtsDetalles;
        }

        private void grdDetalles_ButtonClicked( object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e )
        {
            MarcarValidado();
        }

        private void MarcarValidado()
        {
            int iRenglon = GridDetalles.ActiveRow;

            MarcarValidado(iRenglon); 
        }

        private void MarcarValidado(int Renglon)
        {
            bool bEsValidado = false; 
            Color color = cColor_NoValidado; 

            switch(GridDetalles.ActiveCol)
            {
                case (int)ColsDetalles.Validado:
                    bEsValidado = GridDetalles.GetValueBool(Renglon, ColsDetalles.Validado);
                    break;
            }

            color = bEsValidado ? cColor_Validado : cColor_NoValidado; 

            GridDetalles.ColorRenglon(Renglon, color);

        }

        private void VerificarValidado()
        {
            bool bEsValidado = false;
            Color color = cColor_NoValidado;

            for(int i = 1; i <= GridDetalles.Rows; i++)
            {
                bEsValidado = GridDetalles.GetValueBool(i, ColsDetalles.Validado);
                color = bEsValidado ? cColor_Validado : cColor_NoValidado;
                GridDetalles.ColorRenglon(i, color);
            }
        }

        private void grdDetalles_KeyDown( object sender, KeyEventArgs e )
        {
            if(e.KeyCode == Keys.F5)
            {
                int iRow = GridDetalles.ActiveRow;
                int idUnique = GridDetalles.GetValueInt(iRow, ColsDetalles.Id);


                ////int iCaja_Inicial = GridDetalles.GetValueInt(iRow, ColsDetalles.Caja);
                ////int iCaja_Final = GridDetalles.GetValueInt(iRow, ColsDetalles.Caja_Final);

                FrmCapturarCajas f = new FrmCapturarCajas(idUnique, datosEmbalaje);
                f.ShowInTaskbar = false;
                f.ShowDialog(this);

                datosEmbalaje = f.InformacionEmbalaje;

                //int iCaja_Inicial = GridDetalles.GetValueInt(iRow, ColsDetalles.Caja);
                //int iCaja_Final = GridDetalles.GetValueInt(iRow, ColsDetalles.Caja_Final);

                //FrmCapturarCajas f = new FrmCapturarCajas(iCaja_Inicial, iCaja_Final);
                //f.ShowInTaskbar = false;
                //f.ShowDialog(this);

                //if(f.AplicarCambio)
                //{
                //    GridDetalles.SetValue(iRow, ColsDetalles.Caja, f.Caja_Inicial);
                //    GridDetalles.SetValue(iRow, ColsDetalles.Caja_Final, f.Caja_Final);
                //}

                //f = null;
            }
        }
    }
}
