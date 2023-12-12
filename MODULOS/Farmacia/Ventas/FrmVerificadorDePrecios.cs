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
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;

namespace Farmacia.Ventas
{
    public partial class FrmVerificadorDePrecios : FrmBaseExt
    {
        private enum Cols
        {
            IdProducto = 1, 
            CodigoEAN = 2,
            Descripcion = 3, 
            Presentacion = 4, 
            TasaIva = 5, 
            PrecioBase = 6, 
            ImporteIva = 7, 
            Precio = 8, 
            Existencia = 9  
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer myLlenaDatos;
        clsGrid Grid;
        clsConsultas query;
        clsAyudas ayuda;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();
        string sFormato = "$ #,###,###,##0.#0";
        string sFormatoExistencia = "#,###,###,##0";

        double dExistencia = 0;
        double dPrecio = 0; 

        // string Codigo = "", Tipo = "0";
        private bool bLimpiar = true;
        bool bSesionIniciada = false; 

        public FrmVerificadorDePrecios()
        {
            InitializeComponent();

            cnn.SetConnectionString();
            leer = new clsLeer(ref cnn);
            myLlenaDatos = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            Grid = new clsGrid(ref grdExistencia, this);
            Grid.EstiloGrid(eModoGrid.SeleccionSimple);
            Grid.Limpiar(false);
            Grid.AjustarAnchoColumnasAutomatico = true;

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;
        }

        private void FrmVerificadorDePrecios_Load(object sender, EventArgs e)
        {
            if (bLimpiar)
            {
                btnNuevo_Click(null, null); 
            }

            tmSesion.Enabled = true;
            tmSesion.Start();   
        }

        #region Eventos
        private void txtCodEAN_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodEAN.Text.Trim() != "" & txtCodEAN.Enabled )
            //{
            //    General.msjUser("Ingrese el CodigoEAN por favor");
            //    txtCodEAN.Focus();
            //}
            //else
            {
                leer.DataSetClase = query.VerificaPrecio(1, sEmpresa, sEstado, sFarmacia, txtCodEAN.Text, "txtCodEAN_Validating" );
                if (!leer.Leer())
                {
                    txtCodEAN.Text = ""; 
                }
                else 
                {
                    CargaDatos();
                    LlenarGrid();
                }                
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtCodEAN.Text = leer.Campo("CodigoEAN");
            txtClaveSSA.Tag = leer.Campo("IdClaveSSA_SAL");
            txtClaveSSA.Text = leer.Campo("IdClaveSSA_SAL");
            txtClaveSSA.Text = leer.Campo("ClaveSSA"); 


            lblDescripcionClave.Text = leer.Campo("ClaveSSA");
            lblDescripcionClave.Text += " ===> " + leer.Campo("DescripcionSal");

            txtIdProducto.Enabled = false; 
            txtIdProducto.Text = leer.Campo("IdProducto");
            lblDescripcionProducto.Text = leer.Campo("DescripcionProducto");
            ////lblPrecioPublico.Text = leer.CampoDouble("PrecioVenta").ToString(sFormato);
            ////lblExistenciaProducto.Text = leer.CampoDouble("Existencia").ToString();


            dPrecio = leer.CampoDouble("PrecioVenta");
            dExistencia = leer.CampoDouble("Existencia"); 
            lblPrecioPublico.Text = dPrecio.ToString(sFormato);
            lblExistenciaProducto.Text = dExistencia.ToString(sFormatoExistencia);

            txtCodEAN.Enabled = false;
            txtIdProducto.Enabled = false; 
        }

        #endregion Eventos

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            tmSesion.Enabled = false;
            tmSesion.Stop();

            Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento);
            Lotes.ManejoLotes = OrigenManejoLotes.Default;

            Fg.IniciaControles();            
            Grid.Limpiar(false);

            txtClaveSSA.Enabled = false; 
            // txtIdProducto.Enabled = true; 
            // txtCodEAN.Enabled = true;

            txtCodEAN.Focus();
            query.MostrarMsjSiLeerVacio = true;
        }

        #endregion Botones        

        #region Grid 
        private bool validarDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtClaveSSA.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Clave SSA a consultar, verifique.");
            }

            if (bRegresa && txtIdProducto.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Código Interno a consultar, verifique.");
            }

            if (bRegresa && txtCodEAN.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Código EAN a consultar, verifique.");
            }

            return bRegresa;
        }

        private void LlenarGrid()
        {
            tmSesion.Enabled = false;
            tmSesion.Stop();

            Grid.Limpiar(false);
            query.MostrarMsjSiLeerVacio = false;

            myLlenaDatos.DataSetClase = query.VerificaPrecioSales(1, sEmpresa, sEstado, sFarmacia, txtClaveSSA.Tag.ToString(), txtCodEAN.Text, "LlenarGrid");
            if (myLlenaDatos.Leer())
            {
                txtCodEAN.Enabled = false; 
                Grid.LlenarGrid(myLlenaDatos.DataSetClase); 
                grdExistencia.Focus();

                tmSesion.Enabled = true;
                tmSesion.Start(); 
            }
            lblTotal.Text = Grid.TotalizarColumna((int)Cols.Existencia).ToString();
        }
        #endregion Grid

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            txtCodEAN_Validating(this,null);
        }

        private bool ValidarPuntoDeVenta()
        {
            bool bRegresa = true;
            leer.DataSetClase = query.Farmacia_Clientes(GnFarmacia.PublicoGral, true,
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.PublicoGral, 
                GnFarmacia.PublicoGralSubCliente, "ValidarPuntoDeVenta()");
            if (!leer.Leer())
            {
                // bRegresa = false;
                // General.msjUser("El Cliente Publico General no esta asignado a la Farmacia, consultar con el Departamento de Sistemas.");
            }
            else
            {
                ////if (!leer.CampoBool("ManejaVtaPubGral"))
                ////{
                ////    bRegresa = false;
                ////    General.msjUser("La farmacia no esta configurada para Manejar Venta al Publico, consulta con el Departamento de Sistemas.");
                ////}
            }

            AjustarPantalla(leer.CampoBool("ManejaVtaPubGral"));
            
            return bRegresa;
        }

        private void AjustarPantalla(bool ManejaVentaPublico)
        {
            if (!DtGeneral.EsAdministrador)
            {
                lblPrecioPublico.Visible = ManejaVentaPublico;
                lblPrecioPublicoAux.Visible = ManejaVentaPublico;

                lblExistenciaProducto.Visible = ManejaVentaPublico;
                lblExistenciaProductoAux.Visible = ManejaVentaPublico;

                if (!ManejaVentaPublico)
                {
                    int iAnchoColDescripcion = 0;

                    iAnchoColDescripcion = (int)grdExistencia.Sheets[0].Columns[(int)Cols.Precio - 1].Width; 
                    grdExistencia.Sheets[0].Columns[(int)Cols.Precio - 1].Visible = false;
                    grdExistencia.Sheets[0].Columns[(int)Cols.Descripcion - 1].Width += (float)iAnchoColDescripcion; 
                }
            }
        }

        private void tmSesion_Tick(object sender, EventArgs e)
        {
            if (!bSesionIniciada)
            {
                tmSesion.Enabled = false;
                tmSesion.Stop();

                bSesionIniciada = ValidarPuntoDeVenta();
                if (!bSesionIniciada)
                {
                    this.Close();
                }
            }
            else
            {
                if (Grid.Rows > 0)
                {
                    CargarInfoProducto(Grid.ActiveRow);
                }
            }
        }

        private void txtIdProducto_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdProducto.Text.Trim() != "" & txtIdProducto.Enabled )
            {
                //leer.DataSetClase = query.Productos(txtIdProducto.Text, "txtIdProducto_Validating");

                string sSql = string.Format( "Select * From vw_Productos_CodigoEAN (NoLock) " + 
                    " Where IdProducto = '{0}' ", Fg.PonCeros(txtIdProducto.Text, 8 ) ) ;

                if( !leer.Exec(sSql) )
                {
                    Error.GrabarError(leer, "");
                    General.msjError("Ocurrió un error al obtener la información de existencias.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        CargarDatosDeProducto();
                    }
                    else
                    {
                        General.msjUser("Clave de Producto no encontrada, verifique.");
                    }
                }                
            } 
        }

        private void CargarDatosDeProducto()
        {
            txtClaveSSA.Tag = leer.Campo("IdClaveSSA_SAL");
            txtClaveSSA.Text = leer.Campo("ClaveSSA"); 
            lblDescripcionClave.Text = leer.Campo("ClaveSSA"); 
            lblDescripcionClave.Text += " ===> " + leer.Campo("DescripcionSal");


            txtIdProducto.Enabled = false; 
            txtIdProducto.Text = leer.Campo("IdProducto");
            lblDescripcionProducto.Text = leer.Campo("Descripcion");
            txtCodEAN.Text = leer.Campo("CodigoEAN");

            dPrecio = leer.CampoDouble("PrecioVenta");
            dExistencia = leer.CampoDouble("Existencia"); 
            lblPrecioPublico.Text = dPrecio.ToString(sFormato);
            lblExistenciaProducto.Text = dExistencia.ToString(sFormatoExistencia);

            LlenarGrid(); 
        }

        private void grdExistencia_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            CargarInfoProducto(e.Row + 1); 
        }

        private void CargarInfoProducto(int Renglon)
        {
            try
            {
                txtIdProducto.Text = Grid.GetValue(Renglon, (int)Cols.IdProducto);
                lblDescripcionProducto.Text = Grid.GetValue(Renglon, (int)Cols.Descripcion);
                lblDescripcionProducto.Text += " === > " + Grid.GetValue(Renglon, (int)Cols.Presentacion);

                txtCodEAN.Text = Grid.GetValue(Renglon, (int)Cols.CodigoEAN);
                //lblPrecioPublico.Text = Grid.GetValueDou(Renglon, (int)Cols.Precio).ToString(sFormato);
                //lblExistenciaProducto.Text = Grid.GetValueInt(Renglon, (int)Cols.Existencia).ToString();
                dPrecio = Grid.GetValueDou(Renglon, (int)Cols.Precio);
                dExistencia = Grid.GetValueInt(Renglon, (int)Cols.Existencia); 

            }
            catch { }

            lblPrecioPublico.Text = dPrecio.ToString(sFormato);
            lblExistenciaProducto.Text = dExistencia.ToString(sFormatoExistencia);

        }

        private void txtIdProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Productos("txtIdProducto_KeyDown");

                if (leer.Leer())
                {
                    CargarDatosDeProducto();
                }
            }

        }

        private void txtCodEAN_KeyDown(object sender, KeyEventArgs e)
        {
            //Se busca solo por Producto
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Productos("txtIdProducto_KeyDown");

                if (leer.Leer())
                {
                    CargarDatosDeProducto();
                }
            }

        }

        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            //Se busca solo por Producto
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Productos("txtIdProducto_KeyDown");

                if (leer.Leer())
                {
                    CargarDatosDeProducto();
                }
            }
        }

        private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sIdProducto = Grid.GetValue(e.Row + 1, 1);
            string sCodigoEAN = Grid.GetValue(e.Row + 1, 2);
            string sDescripcion = Grid.GetValue(e.Row + 1, 3);

            if (sIdProducto != "" && sCodigoEAN != "")
            {
                leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sIdProducto, sCodigoEAN, false, "CargarLotesCodigoEAN()");
                if (query.Ejecuto)
                {
                    // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                    leer.Leer();

                    Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento);
                    Lotes.ManejoLotes = OrigenManejoLotes.Default;
                    Lotes.AddLotes(leer.DataSetClase);
                    mostrarOcultarLotes(sIdProducto, sDescripcion, sCodigoEAN);
                }
            }
        }

        #region Manejo de lotes
        //private void CargarLotesCodigoEAN()
        //{
        //    int iRow = myGrid.ActiveRow;
        //    string sCodigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
        //    string sCodEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);

        //    leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEstado, sFarmacia, sCodigo, sCodEAN, "CargarLotesCodigoEAN()");
        //    if (query.Ejecuto)
        //    {
        //        // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
        //        leer.Leer();
        //        Lotes.AddLotes(leer.DataSetClase);
        //        mostrarOcultarLotes(sCodigo, sCodEAN);
        //    }
        //}

        private void mostrarOcultarLotes(string IdProducto, string Descripcion, string sCodigoEAN)
        {
            // Asegurar que el Grid tenga el Foco.
            // if (this.ActiveControl.Name.ToUpper() == grdProductos.Name.ToUpper())
            {
                //int iRow = myGrid.ActiveRow;

                // if (myGrid.GetValue(iRow, (int)Cols.Codigo) != "")
                {
                    Lotes.Codigo = IdProducto;
                    Lotes.CodigoEAN = sCodigoEAN;
                    Lotes.Descripcion = Descripcion;
                    Lotes.EsEntrada = false;// para las ventas
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = false;
                    Lotes.ModificarCantidades = false;

                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.Default;

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;
                    Lotes.Show();

                }
                //else
                //{
                //    myGrid.SetActiveCell(iRow, (int)Cols.CodEAN);
                //}
            }
        }
        #endregion Manejo de lotes
    }
}
