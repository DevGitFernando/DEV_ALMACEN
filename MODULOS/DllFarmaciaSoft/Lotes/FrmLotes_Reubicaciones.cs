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
    public partial class FrmLotes_Reubicaciones : FrmBaseExt 
    {
        private enum Cols
        {
            Ninguno = 0, 
            IdSubFarmacia = 1, 
            Codigo, CodigoEAN, SKU, ClaveLote, 
            Pasillo, Estante, Entrepano,
            Status, Existencia, Existencia_Disponible, Cantidad, AddColumna,

            Ordenamiento
        }
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayudas;
        clsDatosCliente DatosCliente;
        clsLotes_Reubicaciones_Destino Destinos;

        public string sIdEmpresa = "";
        public string sIdEstado = "";
        public string sIdFarmacia = ""; 
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
        public int iTotalCantidad = 0;
        public int iExistenciaActual = 0;

        clsGrid grid;
        public DataSet dtsLotesUbicaciones = clsLotesUbicaciones.PreparaDtsLotesUbicaciones();
        public DataSet dtsLotesUbicaciones_Destinos = clsLotes_Reubicaciones.PreparaDtsLotesUbicaciones(); 

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

        public FrmLotes_Reubicaciones(DataRow[] Rows)
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

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
        private void FrmReubicacionProductos_Lotes_Load(object sender, EventArgs e)
        {
            bCantidadesValidadas = false;
            CargarInformacion();

            gpoUbicaciones.Left = grdUbicaciones.Left;
            gpoUbicaciones.Top = grdUbicaciones.Top;
            gpoUbicaciones.Height = grdUbicaciones.Height;
            gpoUbicaciones.Width = grdUbicaciones.Width;
            gpoUbicaciones.Visible = false;

            lblAyudaAux.AutoSize = false;
            lblAyudaAux.Height = lblAyuda.Height;

            lblAyuda.Dock = DockStyle.Bottom;
            lblAyudaAux.Dock = DockStyle.Bottom;

            // 2K120306.1356  Jesús Diaz 
            if (!bModificarCaptura)
            {
                grid.BloqueaColumna(true, (int)Cols.Cantidad); 
            }


            AjustarMensajeTeclasRapidas();

        }

        private void FrmReubicacionProductos_Lotes_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DevolverInformacion();
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
                    if (bPermitirCapturaUbicacionesNuevas)
                    {
                        MostrarCapturaDestinos();
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

        #region Botones 
        private void btnAgregar_Click(object sender, EventArgs e) 
        {
            bool bAgregar = true;
            string sValorBuscar = ""; 

            // if (sLote != "" && bAgregar) 

            if (ValidaUbicacion())
            {
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
                        General.msjAviso("La Posición de el Lote ya se encuentra registrada, verifique.");
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
                        grid.SetValue(iActiveRow, (int)Cols.Cantidad, txtCantidad.Text.Trim());    //Cantidad 
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

        private void MostrarCapturaDestinos()
        {
            int iRow = grid.ActiveRow;
            string sClaveLote = grid.GetValue(iRow, (int)Cols.ClaveLote);
            int iPasillo = grid.GetValueInt(iRow, (int)Cols.Pasillo);
            int iEstante = grid.GetValueInt(iRow, (int)Cols.Estante);
            int iEntrepano = grid.GetValueInt(iRow, (int)Cols.Entrepano);
            int iExistenciaLote = grid.GetValueInt(iRow, (int)Cols.Existencia_Disponible);

            if (sClaveLote.Trim() != "")
            {
                if (!bCapturandoUbicaciones)
                {
                    Destinos = new clsLotes_Reubicaciones_Destino(sIdEstado, sIdFarmacia, sSKU, sIdSubFarmacia, sIdProducto, sCodigoEAN, sClaveLote, dtsLotesUbicaciones_Destinos);
                    
                    Destinos.Codigo = sIdProducto;
                    Destinos.CodigoEAN = sCodigoEAN;
                    Destinos.ClaveLote = sClaveLote;
                    Destinos.DescripcionProducto = lblArticulo.Text;
                    Destinos.DispensarPor = lblPresentacion.Text;
                    Destinos.ContenidoPaquete = lblContenido.Text;
                    Destinos.ClaveSSA = lblClaveSSA.Text;
                    Destinos.ClaveSSA_Descripcion = lblDescripcionSSA.Text;
                    //Destinos.CapturarUbicaciones = bPermitirCapturaLotesNuevos;

                    Destinos.PasilloActual = iPasillo;
                    Destinos.EstanteActual = iEstante;
                    Destinos.EntrepanoActual = iEntrepano;
                    Destinos.ExistenciaActual = iExistenciaLote;

                    //Destinos.bPermitirCapturaUbicacionesNuevas = bPermitirCapturaLotesNuevos;
                    Destinos.bModificarCaptura = bModificarCaptura;
                    Destinos.bEsEntrada = bEsEntrada;
                    Destinos.bEsTransferenciaDeEntrada = bEsTransferenciaDeEntrada;
                    Destinos.bEsCancelacionCompras = bEsCancelacionCompras;
                    Destinos.bEsConsignacion = bEsConsignacion;
                    Destinos.bPermitirLotesNuevosConsignacion = bPermitirLotesNuevosConsignacion;
                    Destinos.bEsInventarioActivo = bEsInventarioActivo;

                    Destinos.Show();
                    this.dtsLotesUbicaciones_Destinos = Destinos.DataSetLotesUbicaciones;

                    grid.SetValue(iRow, (int)Cols.Cantidad, Destinos.CantidadTotal);
                    grid.SetActiveCell(iRow, (int)Cols.Cantidad);
                    lblTotal.Text = grid.TotalizarColumna((int)Cols.Cantidad).ToString();
                }
            }

            //bCapturandoUbicaciones = !bCapturandoUbicaciones;
            //gpoUbicaciones.Visible = bCapturandoUbicaciones;
            //grdUbicaciones.Enabled = !bCapturandoUbicaciones;

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
            dtsLotesUbicaciones = clsLotes_Reubicaciones.PreparaDtsLotesUbicaciones();

            for (int i = 1; i <= grid.Rows; i++)
            {
                object[] objRow = {
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
                    grid.GetValueInt(i, (int)Cols.Cantidad) };
                dtsLotesUbicaciones.Tables[0].Rows.Add(objRow);
            }
            iTotalCantidad = grid.TotalizarColumna((int)Cols.Cantidad); 
        }

        private bool ValidaCantidades()
        {
            bool bRegresa = true;
            int iTotal = grid.TotalizarColumna((int)Cols.Cantidad);

            if (iTotal > iExistenciaActual)
            {
                bRegresa = false;
                General.msjUser("El Total Cantidad no puede ser mayor que la Existencia Actual del Lote del producto. Verifique.");
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

            if (lblPasillo.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el Rack por favor");
            }

            if ( bRegresa && lblEstante.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el Nivel por favor");
            }

            if (bRegresa && lblEntrepaño.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el Entrepaño por favor");
            }

            return bRegresa;
        }

        #endregion Funciones y Procedimientos Privados

        #region Buscar Pasillo 
        private void txtPasillo_Validating(object sender, CancelEventArgs e)
        {
            if (txtPasillo.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Pasillos(sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(), "txtPasillo_Validating");                
                if (leer.Leer())
                {
                    CargaDatosPasillo();
                }
                else
                {
                    txtPasillo.Text = "";
                    txtPasillo.Focus();
                }
                
            }
        }

        private void CargaDatosPasillo()
        {
            //Se hace de esta manera para la ayuda.
            txtPasillo.Text = leer.Campo("IdPasillo");
            lblPasillo.Text = leer.Campo("DescripcionPasillo");

            if (leer.Campo("Status") == "C")
            {
                General.msjUser("El Rack ingresado se encuentra cancelado. Verifique");
                txtPasillo.Text = "";
                lblPasillo.Text = "";
                txtPasillo.Focus();
            }

        }

        private void txtPasillo_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Pasillos(sIdEmpresa, sIdEstado, sIdFarmacia, "txtId_KeyDown");
                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");

                if (leer.Leer())
                {
                    CargaDatosPasillo();
                }
            }
        }
        #endregion Buscar Pasillo

        #region Buscar Estante 
        private void txtEstante_Validating(object sender, CancelEventArgs e)
        {
            if (txtEstante.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Pasillos_Estantes(sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(), txtEstante.Text.Trim(), "txtPasillo_Validating");                
                if (leer.Leer())
                {
                    CargaDatosEstante();
                }
                else
                {
                    txtEstante.Text = "";
                    txtEstante.Focus();
                }
                
            }
        }

        private void CargaDatosEstante()
        {
            //Se hace de esta manera para la ayuda.
            txtEstante.Text = leer.Campo("IdEstante");
            lblEstante.Text = leer.Campo("DescripcionEstante");

            if (leer.Campo("Status") == "C")
            {
                General.msjUser("El Nivel ingresado se encuentra cancelado. Verifique");
                txtEstante.Text = "";
                lblEstante.Text = "";
                txtEstante.Focus();
            }

        }

        private void txtEstante_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Estantes(sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(),"txtId_KeyDown");
                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");

                if (leer.Leer())
                {
                    CargaDatosEstante();
                }
            }
        }

        #endregion Buscar Estante

        #region Buscar Entrepaño 
        private void txtEntrepaño_Validating(object sender, CancelEventArgs e)
        {
            if (txtEntrepaño.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Pasillos_Estantes_Entrepaños(sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(), txtEstante.Text.Trim(), txtEntrepaño.Text.Trim(), "txtPasillo_Validating");
                if (leer.Leer())
                {
                    CargaDatosEntrepaño();
                }
                else
                {
                    txtEntrepaño.Text = "";
                    txtEntrepaño.Focus();
                }

            }
        }

        private void CargaDatosEntrepaño()
        {
            //Se hace de esta manera para la ayuda.
            txtEntrepaño.Text = leer.Campo("IdEntrepaño");
            lblEntrepaño.Text = leer.Campo("DescripcionEntrepaño");

            if (leer.Campo("Status") == "C")
            {
                General.msjUser("El Entrepaño ingresado se encuentra cancelado. Verifique");
                txtEntrepaño.Text = "";
                lblEntrepaño.Text = "";
                txtEntrepaño.Focus();
            }

        }

        private void txtEntrepaño_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Entrepaños(sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(), txtEstante.Text.Trim(),"txtId_KeyDown");
                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");

                if (leer.Leer())
                {
                    CargaDatosEntrepaño();
                }
            }
        }
        #endregion Buscar Entrepaño

        #region Eventos
        private void txtPasillo_TextChanged(object sender, EventArgs e)
        {
            lblPasillo.Text = "";
            txtEstante.Text = "";
        }

        private void txtEstante_TextChanged(object sender, EventArgs e)
        {
            lblEstante.Text = "";
            txtEntrepaño.Text = "";
        }

        private void txtEntrepaño_TextChanged(object sender, EventArgs e)
        {
            lblEntrepaño.Text = "";
        }

        #endregion Eventos

        #region Grid 
        private void grdUbicaciones_EditModeOff(object sender, EventArgs e)
        {
            int iRow = grid.ActiveRow;
            int iExistencia = grid.GetValueInt(iRow, (int)Cols.Existencia_Disponible);
            int iCantidad = grid.GetValueInt(iRow, (int)Cols.Cantidad);

            //if (!bEsEntrada)
            {
                if (iCantidad > iExistencia)
                {
                    if (iExistencia != 0) // Si la existencia es cero, significa que es una hubicacion nueva.
                    {
                        grid.SetValue(iRow, (int)Cols.Cantidad, iExistencia);
                        if (bEsTransferenciaDeEntrada)
                        {
                            General.msjUser("La cantidad recibida no puede ser mayor a la cantidad enviada, verifique.");
                        }
                        else
                        {
                            General.msjUser("La existencia no es suficiente para cubrir la cantidad solicitada.");
                        }
                    }
                }
            }

            lblTotal.Text = grid.TotalizarColumna((int)Cols.Cantidad).ToString();
        } 
        #endregion Grid 

        private void txtCantidad_Validating(object sender, CancelEventArgs e)
        {
            string sCantidad = txtCantidad.Text.Trim().Replace(",", "");
            int iCantidad = int.Parse(sCantidad);

            if (iCantidad > iExistenciaActual)
            {
                General.msjUser("La Cantidad a mover no puede ser mayor que la Existencia Actual del Lote del Producto. Verifique");
                txtCantidad.Focus();
            }
        }
    }
}
