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
using DllFarmaciaSoft.Ubicaciones; 

namespace DllFarmaciaSoft.Lotes
{
    public partial class FrmCaptura_LotesUbicaciones : FrmBaseExt 
    {
        private enum Cols
        {
            Ninguna = 0,
            IdSubFarmacia = 1, 
            Codigo, CodigoEAN, 
            SKU,
            ClaveLote,
            Pasillo, Estante, Entrepano,
            Status, Existencia, Existencia_Disponible, 
            Cantidad, AddColumna,

            Ordenamiento
        }

        //////// Semaroforos e indicadores 
        Color colorCaducados = IndicadoresLotes.colorCaducados;
        Color colorPrecaucion = IndicadoresLotes.colorPrecaucion;
        Color colorStatusOk = IndicadoresLotes.colorStatusOk;
        Color colorBloqueaCaducados = IndicadoresLotes.colorBloqueaCaducados;
        Color colorSalidaCaducados = IndicadoresLotes.colorSalidaCaducados;
        Color colorConsignacion = IndicadoresLotes.colorConsignacion;  

        public string sIdEmpresa = "";
        public string sIdEstado = "";
        public string sIdFarmacia = ""; 
        public string sIdSubFarmacia = "";

        public string sSKU = "";        
        public string sIdProducto = "";
        public string sCodigoEAN = "";
        public string sClaveLote = ""; 
        public string sDescripcion = ""; 
        public string sDispensarPor = "";
        public string sContenidoPaquete = "";
        public string sClaveSSA = "";
        public string sClaveSSA_Descripcion = "";
        public int iTotalCantidad = 0; 

        clsGrid grid;
        public DataSet dtsLotesUbicaciones = clsLotesUbicaciones.PreparaDtsLotesUbicaciones();
        public DataSet dtsLotesUbicacionesRegistradas = new DataSet(); 

        public bool bPermitirCapturaUbicacionesNuevas = false;
        public bool bModificarCaptura = true;
        public bool bCapturandoUbicaciones = false;
        public bool bEsEntrada = false;
        public int iMesesCadudaMedicamento = 12;
        public int iTipoCaptura = 0;
        public int iExistenciaDisponible = 0;
        public bool bEsTransferenciaDeEntrada = false;
        public bool bEsCancelacionCompras = false;
        public bool bEsConsignacion = false;
        public bool bPermitirLotesNuevosConsignacion = true;
        public bool bEsInventarioActivo = false;

        public bool bBloqueaPorInventario = false; 
        public bool bEsCaducadudo = false;
        public bool bEsDevolucion = false; 

        int iPasillo = 0;
        int iEstante = 0;
        int iEntrepano = 0; 

        /// <summary>
        /// Esta opcion solo se debe activar para los movimientos de Inventario que por su
        /// naturaleza necesiten dar Salida a Caducados 
        /// </summary>
        public bool bPermitirSacarCaducados = false;
        public string sPosicionEstandar = "";

        public FrmCaptura_LotesUbicaciones(DataRow[] Rows)
        {
            InitializeComponent();

            grdUbicaciones.EditModeReplace = true;
            grid = new clsGrid(ref grdUbicaciones, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow;

            lblTotal.Text = "0";

            grid.Limpiar(false);
            if (Rows.Length > 0)
            {
                grid.AgregarRenglon(Rows, (int)Cols.Cantidad, false, true);
            }
            lblTotal.Text = grid.TotalizarColumna((int)Cols.Cantidad).ToString();
        }

        #region Form 
        private void FrmCaptura_LotesUbicaciones_Load(object sender, EventArgs e)
        {
            CargarInformacion();
            ConfigurarColumnas(); 

            gpoUbicaciones.Left = grdUbicaciones.Left;
            gpoUbicaciones.Top = grdUbicaciones.Top;
            gpoUbicaciones.Height = grdUbicaciones.Height;
            gpoUbicaciones.Width = grdUbicaciones.Width;
            gpoUbicaciones.Visible = false;

            lblAyudaAux.AutoSize = false;
            lblAyudaAux.Height = lblAyuda.Height;

            lblAyuda.Dock = DockStyle.Bottom;
            lblAyudaAux.Dock = DockStyle.Bottom;                

            AjustarMensajeTeclasRapidas(); 
            if (bModificarCaptura) 
            {
                if (!bEsCaducadudo)
                {
                    if (bPermitirCapturaUbicacionesNuevas)
                    {
                        if (grid.GetValue(1, 1) == "")
                        {
                            grid.Limpiar(false);
                            General.msjAviso("LOTE sin Ubicaciones asignadas. Asignar Ubicaciones.");
                            // General.msjAviso("El Codigo EAN seleccionado no tiene registrados Lotes, es necesario registrar Lotes.");
                            if (sPosicionEstandar.Trim() == "")
                            {
                                MostrarCapturaUbicaciones();
                            }
                            else
                            {
                                Carga_PosicionEstandar();
                            }
                        }
                    }
                }
            }
            else
            {
                grid.BloqueaColumna(true, (int)Cols.Cantidad);
            }
            grid.SetActiveCell(1, (int)Cols.Cantidad);
            lblTotal.Text = grid.TotalizarColumna((int)Cols.Cantidad).ToString();

            if (!bPermitirSacarCaducados)
            {
                if (bEsCaducadudo)
                {
                    //grid.BloqueaCelda(true, colorBloqueaCaducados, i, (int)Cols.Cantidad);
                    grid.BloqueaColumna(true, (int)Cols.Cantidad);
                    grid.ColorColumna((int)Cols.Cantidad, colorBloqueaCaducados); 
                } 
            }

            if (bEsEntrada && bEsDevolucion)
            {
                lblCantMax.Visible = true;
                lblTotalMax.Visible = true;
                lblTotalMax.Text = iExistenciaDisponible.ToString();
            }

            if (sPosicionEstandar.Trim() != "")
            {
                lblAyuda.Visible = false;
                lblAyudaAux.Visible = true;
            }
        }

        private void ConfigurarColumnas()
        {
            int iAncho = ((int)grdUbicaciones.Sheets[0].Columns[(int)Cols.Existencia_Disponible - 1].Width) / 3;

            if (!bEsDevolucion)
            {
                grdUbicaciones.Sheets[0].Columns[(int)Cols.Existencia_Disponible - 1].Width = 0;
                grdUbicaciones.Sheets[0].Columns[(int)Cols.Pasillo - 1].Width += iAncho;
                grdUbicaciones.Sheets[0].Columns[(int)Cols.Estante - 1].Width += iAncho;
                grdUbicaciones.Sheets[0].Columns[(int)Cols.Entrepano - 1].Width += iAncho;
            }
            else
            {
                grid.PonerEncabezado((int)Cols.Existencia, "Cantidad Recibida");
            }
        }

        private void FrmCaptura_LotesUbicaciones_FormClosing(object sender, FormClosingEventArgs e)
        {
            ////DevolverInformacion(); 
        }

        private void Salir_ValidarCaptura()
        {
            bool bRegresa = true;

            for (int i = 1; i <= grid.Rows; i++)
            {
                if (!validarCaptura_De_Cantidades(i))
                {
                    lblTotal.Text = grid.TotalizarColumna((int)Cols.Cantidad).ToString();
                    bRegresa = false;
                    break;
                }
            }

            if (bRegresa)
            {
                Actualizar_Datos_De_Salida();
                this.Close();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //case Keys.Delete:
                //    EliminarRenglon();
                //    break;
                case Keys.F4:
                    if (bModificarCaptura & bEsEntrada)
                    {
                        if (sPosicionEstandar.Trim() == "")
                        {
                            MostrarUbicacionesVacias();
                        }
                    }
                    break;

                case Keys.F8:
                    if (bPermitirCapturaUbicacionesNuevas)
                    {
                        if (!bEsCaducadudo)
                        {
                            if (sPosicionEstandar.Trim() == "")
                            {
                                MostrarCapturaUbicaciones();
                            }
                        }
                    }
                    break;

                case Keys.F12:
                    Salir_ValidarCaptura(); 
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }
        #endregion Form 

        #region Botones 
        private void btnAgregar_Click(object sender, EventArgs e) 
        {
            bool bAgregar = true;
            string sValorBuscar = ""; 

            // if (sLote != "" && bAgregar) 
            if (!bAgregar)
            {
                // txtClaveLote.Focus(); 
            }
            else
            {
                sValorBuscar = sIdSubFarmacia + lblCodigo.Text + lblCodigoEAN.Text + lblClaveLote.Text + txtPasillo.Text + txtEstante.Text + txtEntrepaño.Text;
                int[] Columnas = { (int)Cols.IdSubFarmacia, (int)Cols.Codigo, (int)Cols.CodigoEAN, (int)Cols.ClaveLote, (int)Cols.Pasillo, (int)Cols.Estante, (int)Cols.Entrepano };

                if (grid.BuscarRepetidosColumnas(sValorBuscar, Columnas) != 0)
                {
                    General.msjAviso("Ubicación de LOTE esta asignada, Favor de verificar.");
                }
                else
                {
                    grid.AddRow();
                    int iActiveRow = grid.Rows;

                    grid.SetValue(iActiveRow, (int)Cols.IdSubFarmacia, sIdSubFarmacia);

                    grid.SetValue(iActiveRow, (int)Cols.Codigo, lblCodigo.Text);
                    grid.SetValue(iActiveRow, (int)Cols.CodigoEAN, lblCodigoEAN.Text);
                    grid.SetValue(iActiveRow, (int)Cols.ClaveLote, lblClaveLote.Text);

                    grid.SetValue(iActiveRow, (int)Cols.Pasillo, txtPasillo.Text);
                    grid.SetValue(iActiveRow, (int)Cols.Estante, txtEstante.Text);
                    grid.SetValue(iActiveRow, (int)Cols.Entrepano, txtEntrepaño.Text); 

                    grid.SetValue(iActiveRow, (int)Cols.Status, "Activo");
                    grid.SetValue(iActiveRow, (int)Cols.Existencia, 0);    //Existencia
                    grid.SetValue(iActiveRow, (int)Cols.Existencia_Disponible, 0);    //Existencia
                    grid.SetValue(iActiveRow, (int)Cols.Cantidad, 0);    //Cantidad 
                    grid.SetValue(iActiveRow, (int)Cols.AddColumna, 1);    // Ubicación agregada 

                    //// Revisar la Caducidad del nuevo lote 
                    //Ordernar_RevisarFechaCaducidad();

                    bCapturandoUbicaciones = false;
                    gpoUbicaciones.Visible = false;
                    grdUbicaciones.Enabled = true;
                    grdUbicaciones.Focus();
                    lblTotal.Text = grid.TotalizarColumna((int)Cols.Cantidad).ToString();
                    grid.SetActiveCell(iActiveRow, (int)Cols.Cantidad);
                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            bCapturandoUbicaciones = false;
            gpoUbicaciones.Visible = false;
            grdUbicaciones.Enabled = true;
        }
        #endregion Botones

        #region Funciones y Procedimientos Privados 
        private void CargarInformacion()
        {
            lblCodigo.Text = sIdProducto;
            lblCodigoEAN.Text = sCodigoEAN;
            lblClaveLote.Text = sClaveLote;
            lblArticulo.Text = sDescripcion;

            lblPresentacion.Text = sDispensarPor;
            lblContenido.Text = sContenidoPaquete;
            lblClaveSSA.Text = sClaveSSA;
            lblDescripcionSSA.Text = sClaveSSA_Descripcion;
        }

        private void MostrarCapturaUbicaciones()
        {
            if (!bCapturandoUbicaciones)
            {
                Fg.IniciaControles(this, true, gpoUbicaciones);
                txtPasillo.Focus();
                //txtClaveLote.Focus();
            }

            bCapturandoUbicaciones = !bCapturandoUbicaciones;
            gpoUbicaciones.Visible = bCapturandoUbicaciones;
            grdUbicaciones.Enabled = !bCapturandoUbicaciones;
        }

        private void MostrarUbicacionesVacias()
        {
            FrmUbicacionesVacias f = new FrmUbicacionesVacias();
            if (f.MostrarUbicacioensVacias())
            {
                iPasillo = f.Pasillo;
                iEstante = f.Estante;
                iEntrepano = f.Entrepano;

                ////txtPasillo.Text = f.Pasillo.ToString();
                ////txtEstante.Text = f.Estante.ToString();
                ////txtEntrepaño.Text = f.Entrepano.ToString(); 
                txtPasillo.Text = iPasillo.ToString();
                txtEstante.Text = iEstante.ToString();
                txtEntrepaño.Text = iEntrepano.ToString();
                btnAgregar_Click(null, null); 


                //txtPasillo.Text + txtEstante.Text + txtEntrepaño.Text  

            }
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

        private void Actualizar_Datos_De_Salida() 
        {
            dtsLotesUbicaciones = clsLotesUbicaciones.PreparaDtsLotesUbicaciones();

            for (int i = 1; i <= grid.Rows; i++)
            {
                object[] objRow = 
                {
                    //grid.GetValue(i, (int)Cols.IdSubFarmacia),  
                    sIdSubFarmacia, 
                    grid.GetValue(i, (int)Cols.Codigo), 
                    grid.GetValue(i, (int)Cols.CodigoEAN), 
                    grid.GetValue(i, (int)Cols.SKU), 
                    grid.GetValue(i, (int)Cols.ClaveLote), 

                    grid.GetValueInt(i, (int)Cols.Pasillo), 
                    grid.GetValueInt(i, (int)Cols.Estante), 
                    grid.GetValueInt(i, (int)Cols.Entrepano), 

                    grid.GetValue(i, (int)Cols.Status), 
                    grid.GetValueInt(i, (int)Cols.Existencia), 
                    grid.GetValueInt(i, (int)Cols.Existencia_Disponible), 
                    grid.GetValueInt(i, (int)Cols.Cantidad)
                };
                dtsLotesUbicaciones.Tables[0].Rows.Add(objRow);
            }
            iTotalCantidad = grid.TotalizarColumna((int)Cols.Cantidad); 
        }
        #endregion Funciones y Procedimientos Privados

        #region GRID 
        private bool validarCaptura_De_Cantidades(int Renglon)
        {
            bool bRegresa = true;
            int iRow = Renglon;
            int iExistencia = 0;
            int iCantidad = 0;
            bool bAplicarLimite = !bEsEntrada;
            bool bSePaso = false;

            if (Renglon > 0)
            {
                iExistencia = grid.GetValueInt(iRow, (int)Cols.Existencia_Disponible);
                iCantidad = grid.GetValueInt(iRow, (int)Cols.Cantidad);

                if (bEsDevolucion)
                {
                    bAplicarLimite = true;
                }

                // if (!bEsEntrada)
                if( bModificarCaptura )
                {
                    if( bAplicarLimite )
                    {
                        if( iCantidad > iExistencia )
                        {
                            grid.SetValue(iRow, (int)Cols.Cantidad, iExistencia);
                            bSePaso = true;
                        }

                        if( bEsEntrada & grid.TotalizarColumna((int)Cols.Cantidad) > iExistenciaDisponible )
                        {
                            grid.SetValue(iRow, (int)Cols.Cantidad, 0);
                            bSePaso = true;
                        }

                        if( bSePaso )
                        {
                            if( bEsTransferenciaDeEntrada )
                            {
                                General.msjUser("La cantidad recibida no puede ser mayor a la cantidad enviada, verifique.");
                            }
                            else
                            {
                                General.msjUser("La cantidad disponible no es suficiente para cubrir la cantidad solicitada.");
                            }
                        }
                    }
                }
            }

            bRegresa = !bSePaso; 
            return bRegresa; 
        }

        private void grdUbicaciones_EditModeOff(object sender, EventArgs e)
        {
            int iRow = grid.ActiveRow;
            //////int iExistencia = grid.GetValueInt(iRow, (int)Cols.Existencia_Disponible);
            //////int iCantidad = grid.GetValueInt(iRow, (int)Cols.Cantidad);
            bool bAplicarLimite = !bEsEntrada;
            bool bSePaso = false;

            validarCaptura_De_Cantidades(iRow);
            lblTotal.Text = grid.TotalizarColumna((int)Cols.Cantidad).ToString();

            ////if (bEsDevolucion)
            ////{
            ////    bAplicarLimite = true;
            ////}

            ////// if (!bEsEntrada)
            ////if (bAplicarLimite)
            ////{
            ////    if (iCantidad > iExistencia)
            ////    {
            ////        grid.SetValue(iRow, (int)Cols.Cantidad, iExistencia);
            ////        bSePaso = true;
            ////    }

            ////    if (bEsEntrada & grid.TotalizarColumna((int)Cols.Cantidad) > iExistenciaDisponible)
            ////    {
            ////        grid.SetValue(iRow, (int)Cols.Cantidad, 0);
            ////        bSePaso = true;
            ////    }

            ////    if (bSePaso)
            ////    {
            ////        if (bEsTransferenciaDeEntrada)
            ////        {
            ////            General.msjUser("La cantidad recibida no puede ser mayor a la cantidad enviada, verifique.");
            ////        }
            ////        else
            ////        {
            ////            General.msjUser("La cantidad disponible no es suficiente para cubrir la cantidad solicitada.");
            ////        }
            ////    }
            ////}
        }
        #endregion GRID 

        #region Busqueda de Ubicaciones 
        private string Pasillo()
        {
            string sRegresa = "";

            if( txtPasillo.Text.Trim() != "" )
            {
                sRegresa = DatoUbicacion(string.Format(" IdPasillo = '{0}' ", txtPasillo.Text), 1);
            }

            return sRegresa; 
        }

        private string Estante()
        {
            string sRegresa = "";

            if( txtPasillo.Text.Trim() != "" && txtEstante.Text.Trim() != "" )
            {
                sRegresa = DatoUbicacion(string.Format(" IdPasillo = '{0}' and IdEstante = '{1}' ", txtPasillo.Text, txtEstante.Text), 2);
            }

            return sRegresa;
        }

        private string Entrepano()
        {
            string sRegresa = "";

            if( txtPasillo.Text.Trim() != "" && txtEstante.Text.Trim() != "" && txtEntrepaño.Text.Trim() != "" )
            {
                sRegresa = DatoUbicacion( string.Format(" IdPasillo = '{0}' and IdEstante = '{1}' and IdEntrepaño = '{2}' ", txtPasillo.Text, txtEstante.Text, txtEntrepaño.Text), 3);
            }

            return sRegresa;
        }

        private string DatoUbicacion(string Filtro, int Tipo)
        {
            string sRegresa = ""; 
            string sDato = ""; 
            clsLeer ubica = new clsLeer(); 


            ubica.DataRowsClase = dtsLotesUbicacionesRegistradas.Tables[0].Select(Filtro);

            //if (ubica.Leer())
            ubica.Leer(); 
            {
                switch (Tipo) 
                {
                    case 1:
                        sDato = "RACK # " + txtPasillo.Text;
                        sRegresa = ubica.Campo("DescripcionPasillo"); 
                        break;
                    case 2:
                        sDato = "NIVEL # " + txtEstante.Text;
                        sRegresa = ubica.Campo("DescripcionEstante"); 
                        break;
                    case 3:
                        sDato = "POSICION # " + txtEntrepaño.Text;
                        sRegresa = ubica.Campo("DescripcionEntrepaño"); 
                        break; 
                }
            }

            sRegresa = sRegresa != "" ? sRegresa : sDato;

            return sRegresa;
        }
        #endregion Busqueda de Ubicaciones

        #region Captura de Ubicacion 
        private void txtPasillo_TextChanged(object sender, EventArgs e)
        {
            txtEstante.Text = "";
            txtEntrepaño.Text = ""; 

            lblPasillo.Text = "";
            lblEstante.Text = "";
            lblEntrepaño.Text = ""; 
        }

        private void txtEstante_TextChanged(object sender, EventArgs e)
        {
            txtEntrepaño.Text = "";

            lblEstante.Text = "";
            lblEntrepaño.Text = ""; 
        }

        private void txtEntrepaño_TextChanged(object sender, EventArgs e)
        {
            lblEntrepaño.Text = ""; 
        }

        private void txtPasillo_Validating(object sender, CancelEventArgs e)
        {
            if (txtPasillo.Text.Trim() != "")
            {
                lblPasillo.Text = Pasillo(); 
            }
        }

        private void txtEstante_Validating(object sender, CancelEventArgs e)
        {
            if (txtEstante.Text.Trim() != "")
            {
                lblEstante.Text = Estante();
            }
        }

        private void txtEntrepaño_Validating(object sender, CancelEventArgs e)
        {
            if (txtEntrepaño.Text.Trim() != "")
            {
                lblEntrepaño.Text = Entrepano();
            }
        }
        #endregion Captura de Ubicacion

        #region Cargar_PosicionEstandar
        private void Carga_PosicionEstandar()
        {            
            string sFiltro = "";
            DataSet dtsPosicion = new DataSet();
            clsLeer ubica = new clsLeer();

            sFiltro = string.Format(" NombrePosicion = '{0}' ", sPosicionEstandar);

            dtsPosicion = DtGeneral.CFG_UBI_Estandar;

            ubica.DataRowsClase = dtsPosicion.Tables[0].Select(sFiltro);

            if (ubica.Leer())
            {
                txtPasillo.Text = ubica.Campo("IdRack");
                txtPasillo_Validating(null, null);

                txtEstante.Text = ubica.Campo("IdNivel");
                txtEstante_Validating(null, null);

                txtEntrepaño.Text = ubica.Campo("IdEntrepaño");
                txtEntrepaño_Validating(null, null);

                btnAgregar_Click(null, null);
            }
           
        }
        #endregion Cargar_PosicionEstandar
    }
}
