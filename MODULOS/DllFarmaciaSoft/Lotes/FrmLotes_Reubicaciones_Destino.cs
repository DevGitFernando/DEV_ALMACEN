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

using DllFarmaciaSoft; 

namespace DllFarmaciaSoft.Lotes
{
    public partial class FrmLotes_Reubicaciones_Destino : FrmBaseExt 
    {
        private enum Cols
        {
            Ninguno = 0, 
            IdSubFarmacia = 1,
            Codigo, CodigoEAN,
            SKU, ClaveLote,
            Pasillo, Estante, Entrepano,
            Status, Existencia, Existencia_Disponible, Cantidad, AddColumna,

            Ordenamiento
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayudas;
        clsDatosCliente DatosCliente;

        public string sIdEmpresa = DtGeneral.EmpresaConectada;
        public string sIdEstado = DtGeneral.EstadoConectado;
        public string sIdFarmacia = DtGeneral.FarmaciaConectada; 
        public string sIdSubFarmacia = "";
        public string sIdProducto = "";
        public string sCodigoEAN = "";
        public string sSKU = ""; 
        public string sClaveLote = "";
        public string sDescripcion = "";
        public string sDispensarPor = "";
        public string sContenidoPaquete = "";
        public string sClaveSSA = "";
        public string sClaveSSA_Descripcion = "";

        public int iPasilloActual = 0;
        public int iEstanteActual = 0;
        public int iEntrepanoActual = 0; 
        public int iTotalCantidad = 0;
        public int iExistenciaActual = 0;

        clsGrid grid;
        public DataSet dtsLotesUbicaciones = clsLotes_Reubicaciones_Destino.PreparaDtsLotesUbicaciones(); 

        public bool bPermitirCapturaUbicacionesNuevas = false;
        public bool bModificarCaptura = true;
        public bool bCapturandoUbicaciones = false;
        public bool bEsEntrada = false;
        public int iMesesCadudaMedicamento = 12;
        public int iTipoCaptura = 0;
        public bool bEsTransferenciaDeEntrada = false;
        public bool bEsCancelacionCompras = false;
        public bool bEsConsignacion = false;
        public bool bPermitirLotesNuevosConsignacion = true;
        public bool bEsInventarioActivo = false;
        public bool bCantidadesValidadas = false;

        // private bool bBloqueaPorInventario = false;

        /// <summary>
        /// Esta opcion solo se debe activar para los movimientos de Inventario que por su
        /// naturaleza necesiten dar Salida a Caducados 
        /// </summary>
        public bool bPermitirSacarCaducados = false;

        public FrmLotes_Reubicaciones_Destino(DataRow[] Rows)
        {
            InitializeComponent();

            clsLeer leerRows = new clsLeer();
            leerRows.DataRowsClase = Rows; 

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            grdUbicaciones.EditModeReplace = true;
            grid = new clsGrid(ref grdUbicaciones, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            grid.AjustarAnchoColumnasAutomatico = true;

            lblTotal.Text = "0";

            if (leerRows.Leer())
            {
            }

            grid.Limpiar(false);
            if (Rows.Length > 0)
            {
                grid.AgregarRenglon(Rows, (int)Cols.Cantidad, false, true);
            }

            if (grid.Rows == 0) // Si No existen renglones, se inserta 1.
            {
                grid.AddRow();
            }

            lblTotal.Text = grid.TotalizarColumna((int)Cols.Cantidad).ToString();
        }

        #region Form 
        private void FrmReubicacionProductos_Lotes_Load(object sender, EventArgs e)
        {
            bCantidadesValidadas = false;
            CargarInformacion();

            lblAyudaAux.AutoSize = false;
            lblAyudaAux.Height = lblAyuda.Height;

            lblAyuda.Dock = DockStyle.Bottom;
            lblAyudaAux.Dock = DockStyle.Bottom; 

            AjustarMensajeTeclasRapidas();
            grdUbicaciones.Focus();
            grid.SetActiveCell(1, (int)Cols.Pasillo);

            // 2K120306.1356  Jesús Diaz 
            if (!bModificarCaptura)
            {
                grid.BloqueaGrid(true);  // BloqueaColumna(true, (int)Cols.Cantidad);
            }

        }

        private void FrmReubicacionProductos_Lotes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                ////if (!ValidaCantidades())
                ////{
                ////    e.Cancel = true;
                ////}
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //case Keys.Delete:
                //    EliminarRenglon();
                //    break;
                case Keys.F5:
                    break;

                case Keys.F8:
                    //if (bPermitirCapturaUbicacionesNuevas)
                    {
                        //MostrarCapturaUbicaciones();
                    }
                    break;

                case Keys.F12:
                    if (ValidaCantidades())
                    {
                        this.Close();
                    }
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }
        #endregion Form 

        #region Funciones y Procedimientos Privados 
        private void CargarInformacion()
        {
            lblPasilloActual.Text = iPasilloActual.ToString();
            lblEstanteActual.Text = iEstanteActual.ToString();
            lblEntrepanoActual.Text = iEntrepanoActual.ToString();
            lblExistencia.Text = iExistenciaActual.ToString();
        }

        private void AjustarMensajeTeclasRapidas()
        {
            // Ajustar el tamaño de la pantalla 
            if (!bPermitirCapturaUbicacionesNuevas)
            {
                //this.Height = FrameLotes.Height + (50);
                lblAyuda.Visible = false;
                lblAyudaAux.Visible = true;
            }
            else
            {
                lblAyuda.Visible = true;
                lblAyudaAux.Visible = false;
            }
        }

        private void DevolverInformacion()
        {
            dtsLotesUbicaciones = clsLotes_Reubicaciones_Destino.PreparaDtsLotesUbicaciones();

            for (int i = 1; i <= grid.Rows; i++)
            {
                object[] objRow = 
                {
                    //grid.GetValue(i, (int)Cols.IdSubFarmacia),  
                    sIdSubFarmacia, 
                    sIdProducto,  
                    sCodigoEAN,
                    ////grid.GetValueInt(i, (int)Cols.SKU),
                    sSKU, 
                    sClaveLote,  

                    grid.GetValueInt(i, (int)Cols.Pasillo),
                    grid.GetValueInt(i, (int)Cols.Estante),
                    grid.GetValueInt(i, (int)Cols.Entrepano),

                    grid.GetValue(i, (int)Cols.Status),
                    iExistenciaActual, 
                    iExistenciaActual, 
                    grid.GetValueInt(i, (int)Cols.Cantidad),
                    lblPasilloActual.Text, 
                    lblEstanteActual.Text,
                    lblEntrepanoActual.Text
                };

                dtsLotesUbicaciones.Tables[0].Rows.Add(objRow);
            }
            iTotalCantidad = grid.TotalizarColumna((int)Cols.Cantidad); 
        }

        private bool ValidaCantidades()
        {
            bool bRegresa = true;
            int iTotal = grid.TotalizarColumna((int)Cols.Cantidad);

            EliminarRenglonesVacios();
            if (iTotal > iExistenciaActual)
            {
                bRegresa = false;
                General.msjUser("El Total Cantidad no puede ser mayor ó existencia actual del lote del producto. Verifique.");
            }
            else
            {
                bCantidadesValidadas = true;
                DevolverInformacion();
            }
            return bRegresa;
        }

        private bool ValidaUbicacion()
        {
            bool bRegresa = true;

            //if (lblPasillo.Text.Trim() == "")
            //{
            //    bRegresa = false;
            //    General.msjUser("Ingrese el Pasillo por favor");
            //}

            //if ( bRegresa && lblEstante.Text.Trim() == "")
            //{
            //    bRegresa = false;
            //    General.msjUser("Ingrese el Estante por favor");
            //}

            //if (bRegresa && lblEntrepaño.Text.Trim() == "")
            //{
            //    bRegresa = false;
            //    General.msjUser("Ingrese el Entrepaño por favor");
            //}

            return bRegresa;
        }

        #endregion Funciones y Procedimientos Privados

        #region Grid 
        private void grdUbicaciones_EditModeOff(object sender, EventArgs e)
        {
            int iRow = grid.ActiveRow;
            int iExistencia = iExistenciaActual;
            int iCantidad = grid.GetValueInt(iRow, (int)Cols.Cantidad);
            string sIdPasillo = "", sIdEstante = "", sIdEntrepano = "";

            switch (grid.ActiveCol)
            {
                case (int)Cols.Pasillo:
                    {
                        sIdPasillo = grid.GetValue(grid.ActiveRow, (int)Cols.Pasillo);

                        if (sIdPasillo != "")
                        {
                            leer.DataSetClase = Consultas.Pasillos(sIdEmpresa, sIdEstado, sIdFarmacia, sIdPasillo, "grdUbicaciones_EditModeOff");                
                            if (leer.Leer())
                            {
                                CargaDatosPasillo();
                            }
                            else
                            {
                                grid.SetValue(grid.ActiveRow, (int)Cols.Pasillo, "");
                                grid.SetActiveCell(grid.ActiveRow, (int)Cols.Pasillo);
                            }                            
                        }
                    }
                    break;

                case (int)Cols.Estante:
                    {
                        sIdPasillo = grid.GetValue(grid.ActiveRow, (int)Cols.Pasillo);
                        sIdEstante = grid.GetValue(grid.ActiveRow, (int)Cols.Estante);

                        if (sIdPasillo != "" && sIdEstante != "")
                        {
                            leer.DataSetClase = Consultas.Pasillos_Estantes(sIdEmpresa, sIdEstado, sIdFarmacia, sIdPasillo, sIdEstante, "grdUbicaciones_EditModeOff");
                            if (leer.Leer())
                            {
                                CargaDatosEstante();
                            }
                            else
                            {
                                grid.SetValue(grid.ActiveRow, (int)Cols.Estante, "");
                                grid.SetActiveCell(grid.ActiveRow, (int)Cols.Estante);
                            }
                        }
                    }
                    break;

                case (int)Cols.Entrepano:
                    {
                        sIdPasillo = grid.GetValue(grid.ActiveRow, (int)Cols.Pasillo);
                        sIdEstante = grid.GetValue(grid.ActiveRow, (int)Cols.Estante);
                        sIdEntrepano = grid.GetValue(grid.ActiveRow, (int)Cols.Entrepano);
                       
                        if (sIdPasillo != "" && sIdEstante != "" && sIdEntrepano != "")
                        {
                            leer.DataSetClase = Consultas.Pasillos_Estantes_Entrepaños(sIdEmpresa, sIdEstado, sIdFarmacia, sIdPasillo, sIdEstante, sIdEntrepano, "grdUbicaciones_EditModeOff");
                            if (leer.Leer())
                            {
                                CargaDatosEntrepano();
                            }
                            else
                            {
                                grid.SetValue(grid.ActiveRow, (int)Cols.Entrepano, "");
                                grid.SetActiveCell(grid.ActiveRow, (int)Cols.Entrepano);
                            }
                        }
                        
                    }
                    break;

                case (int)Cols.Cantidad:
                    {
                        if (iCantidad > iExistencia)
                        {
                            grid.SetValue(iRow, (int)Cols.Cantidad, iExistencia);
                            General.msjUser("La existencia no es suficiente para cubrir la cantidad solicitada.");
                        }
                    }
                    break;
            }

            lblTotal.Text = grid.TotalizarColumna((int)Cols.Cantidad).ToString();
        }

        private void CargaDatosPasillo()
        {
            int iRowActivo = grid.ActiveRow;
            grid.SetValue(iRowActivo, (int)Cols.Pasillo, leer.Campo("IdPasillo"));
            grid.SetActiveCell(iRowActivo, (int)Cols.Estante);
            
        }

        private void CargaDatosEstante()
        {
            int iRowActivo = grid.ActiveRow;
            grid.SetValue(iRowActivo, (int)Cols.Estante, leer.Campo("IdEstante"));           
            grid.SetActiveCell(iRowActivo, (int)Cols.Entrepano);            
        }

        private void CargaDatosEntrepano()
        {
            string sUbicacionBuscar = "";
            int iRowActivo = grid.ActiveRow;
            string sPasillo = grid.GetValue(iRowActivo, (int)Cols.Pasillo);
            string sEstante = grid.GetValue(iRowActivo, (int)Cols.Estante);
            string sEntrepano = leer.Campo("IdEntrepaño");
            string sPosicionActualLote = lblPasilloActual.Text.Trim() + "-" + lblEstanteActual.Text.Trim() + "-" + lblEntrepanoActual.Text.Trim();

            sUbicacionBuscar = sPasillo + "-" + sEstante + "-" + sEntrepano;

            if (sUbicacionBuscar != sPosicionActualLote)
            {
                if (EstanteRepetido(sUbicacionBuscar, iRowActivo))
                {
                    grid.SetValue(grid.ActiveRow, (int)Cols.Entrepano, "");
                    grid.SetValue(grid.ActiveRow, (int)Cols.Cantidad, "0");
                    grid.SetActiveCell(grid.ActiveRow, (int)Cols.Entrepano);
                    General.msjAviso("La Posición de el Lote ya se encuentra registrada, verifique.");
                }
                else
                {
                    grid.SetValue(iRowActivo, (int)Cols.Entrepano, leer.Campo("IdEntrepaño"));
                    grid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);
                }
            }
            else
            {
                grid.SetValue(grid.ActiveRow, (int)Cols.Entrepano, "");
                grid.SetValue(grid.ActiveRow, (int)Cols.Cantidad, "0");
                grid.SetActiveCell(grid.ActiveRow, (int)Cols.Entrepano);
                General.msjUser("La nueva Ubicacion del Lote no puede ser igual a la posicion actual. Verifique.");
            }



            //if (!grid.BuscaRepetido(leer.Campo("IdEntrepaño"), iRowActivo, (int)Cols.Entrepano))
            //{
            //    grid.SetValue(iRowActivo, (int)Cols.Entrepano, leer.Campo("IdEntrepaño"));
            //    grid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);
            //}
            //else
            //{
            //    General.msjUser("Este Entrepaño ya se encuentra capturado en otro renglon.");
            //    grid.SetValue(grid.ActiveRow, (int)Cols.Entrepano, "");
            //    grid.SetValue(grid.ActiveRow, (int)Cols.Cantidad, "0");
            //    grid.SetActiveCell(grid.ActiveRow, (int)Cols.Entrepano);
            //    grid.EnviarARepetido();
            //}

        }

        private void grdUbicaciones_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int iRow = grid.ActiveRow;

            switch (grid.ActiveCol)
            {
                case (int)Cols.Pasillo:
                    {
                        grid.SetValue(iRow, (int)Cols.Estante, "");
                        grid.SetValue(iRow, (int)Cols.Entrepano, "");
                        grid.SetValue(iRow, (int)Cols.Cantidad, 0);
                    }
                    break;

                case (int)Cols.Estante:
                    {
                        grid.SetValue(iRow, (int)Cols.Entrepano, "");
                        grid.SetValue(iRow, (int)Cols.Cantidad, 0);
                    }
                    break;

                case (int)Cols.Entrepano:
                    {
                        grid.SetValue(iRow, (int)Cols.Cantidad, 0);
                    }
                    break;

                case (int)Cols.Cantidad:
                    break;
            }

            lblTotal.Text = grid.TotalizarColumna((int)Cols.Cantidad).ToString();
        }

        private void grdUbicaciones_KeyDown(object sender, KeyEventArgs e)
        {
            string sIdPasillo = grid.GetValue(grid.ActiveRow, (int)Cols.Pasillo);
            string sIdEstante = grid.GetValue(grid.ActiveRow, (int)Cols.Estante);
            string sIdEntrepano = grid.GetValue(grid.ActiveRow, (int)Cols.Entrepano);

            if (e.KeyCode == Keys.F1)
            {
                switch (grid.ActiveCol)
                {
                    case (int)Cols.Pasillo:
                        {
                            leer.DataSetClase = Ayudas.Pasillos(sIdEmpresa, sIdEstado, sIdFarmacia, "grdUbicaciones_KeyDown");
                            if (leer.Leer())
                            {
                                CargaDatosPasillo();
                            }
                            break;
                        }

                    case (int)Cols.Estante:
                        {
                            leer.DataSetClase = Ayudas.Estantes(sIdEmpresa, sIdEstado, sIdFarmacia, sIdPasillo, "grdUbicaciones_KeyDown");
                            if (leer.Leer())
                            {
                                CargaDatosEstante();
                            }
                            break;
                        }
                    case (int)Cols.Entrepano:
                        {
                            leer.DataSetClase = Ayudas.Entrepaños(sIdEmpresa, sIdEstado, sIdFarmacia, sIdPasillo, sIdEstante, "grdUbicaciones_KeyDown");
                            if (leer.Leer())
                            {
                                CargaDatosEntrepano();
                            }
                            break;
                        }
                }
            }

            if (e.KeyCode == Keys.Delete)
            { }
        }

        private bool EstanteRepetido(string sValorBuscar, int iRenglonActual)
        {
            bool bRegresa = false;
            string sPasillo = "", sEstante = "", sEntrepano = "", sValorGrid = "";

            for (int i = 1; i <= grid.Rows; i++)
            {
                sPasillo = grid.GetValue(i, (int)Cols.Pasillo);
                sEstante = grid.GetValue(i, (int)Cols.Estante);
                sEntrepano = grid.GetValue(i, (int)Cols.Entrepano);
                sValorGrid = sPasillo + "-"+ sEstante + "-" +  sEntrepano;

                if (sValorGrid == sValorBuscar)
                {
                    if (i != iRenglonActual)
                    {
                        bRegresa = true;
                    }
                }                
            }
            return bRegresa;
        }

        private void grdUbicaciones_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((grid.ActiveRow == grid.Rows) && e.AdvanceNext)
            {
                if (grid.GetValue(grid.ActiveRow, (int)Cols.Entrepano).Trim() != "" && grid.GetValueDou(grid.ActiveRow, (int)Cols.Cantidad) != 0)
                {
                    grid.Rows = grid.Rows + 1;
                    grid.ActiveRow = grid.Rows;
                    grid.SetActiveCell(grid.Rows, (int)Cols.Pasillo);
                }
            }
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= grid.Rows; i++) //Renglones.
            {
                if (grid.GetValueInt(i, (int)Cols.Cantidad) == 0) //Si la columna oculta Cantidad esta vacia o es icual a cero.
                    grid.DeleteRow(i);
            }

            //if (grid.Rows == 0) // Si No existen renglones, se inserta 1.
            //    grid.AddRow();
        }
        #endregion Grid 

        

    }
}
