using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Devoluciones;
using DllFarmaciaSoft.CodificacionDM;


using ZXing; 

//using Dll_IMach4;
//using Dll_IGPI.Interface;

using DllFarmaciaSoft.Ayudas;

namespace DllFarmaciaSoft
{
    public partial class FrmLotesSNK : FrmBaseExt 
    {
        #region Declaracion de Variables 
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerLotes;
        clsLeer leer2;
        clsConsultas query;
        clsAyudas ayuda;

        clsLotes Lotes;
        clsListView lst;
        clsValidarLote validarLote; 

        DataSet dtsLotes;
        public DataSet dtsLotesUbicacionesRegistradas = new DataSet();

        ItemCodificacion itemEncode = new ItemCodificacion();
        ItemCodificacion itemEncode_Aux = new ItemCodificacion(); 

        public string sCodigoEAN = "";
        public string sIdProducto = "";
        string sDescProducto = "";
        bool bManejaUbicaciones = false;
        bool bEsTransferenciaEntrada = false;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sCadena = "";

        public bool bProceso = false, bEsIdProducto_Ctrl = false;
        //public bool bLecturaCorrecta = true;
        //string sMsjLecturaCorrecta = "Lectura Correcta  ";
        string sMsjLoteCaducado = "Lote Caducado...  ";
        string sMsjLoteEnCero = "Existencia de Lote en ceros...  ";

        public int iCant_Lote = 0;
        FrmLotesSubFarmacias f;
        #endregion Declaracion de Variables

        #region Propiedades
        public clsLotes LotesCodigos
        {
            get { return Lotes; }
        }
        #endregion Propiedades

        public FrmLotesSNK()
        {
            InitializeComponent();

            con = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref con);
            leerLotes = new clsLeer(ref con);
            leer2 = new clsLeer(ref con);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);


            ////validarLote = new clsValidarLote(txtCodigoLote);
            ////validarLote.NoLetrasAlInicio = 3;
            ////validarLote.ValidarLetrasAlInicio = true;

            ///// Ocultar la lectura del codigo 
            txtCodigo.PasswordChar = '*'; 


            lst = new clsListView(lstLotes);
            lst.PermitirAjusteDeColumnas = false;
            lst.AnchoColumna(1, 170);
            lst.AnchoColumna(2, 160);
            lst.AnchoColumna(3, 80);
            lst.AnchoColumna(4, 80);

            PosicionMensajes();

            lector = new ReaderCam(sCamara, BarcodeFormat.DATA_MATRIX, new Size(320, 240));
            lector.OnLectura += new EventHandler<ProcessArgs>(scanner_OnLectura);
            Application.DoEvents();
            System.Threading.Thread.Sleep(20);
        }        

        private void FrmLotesSNK_Load(object sender, EventArgs e)
        {
            LimpiaPantalla();

            gpoUbicaciones.Left = lstLotes.Left;
            gpoUbicaciones.Top = lstLotes.Top;
            gpoUbicaciones.Height = lstLotes.Height;
            gpoUbicaciones.Width = lstLotes.Width;
            gpoUbicaciones.Visible = false;

            if (bManejaUbicaciones && !bEsTransferenciaEntrada)
            {
                lblMensajes.Text = "<F5> Cambiar Ubicación                                                                                                                                       <F12> Cerrar   ";
                CargarUbicacionesRegistradas();
                gpoUbicaciones.Visible = true;
                txtCodigoLote.Enabled = false;
                txtPasillo.Focus();
            }
            else
            {
                lector.IniciarCaptura(); 
            }
        }

        private void FrmLotesSNK_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lector != null)
            {
                lector.DetenerCaptura();
                lector.DetenerCaptura();
            }
        }

        #region SCANER WEB CAM
        bool bExisteLector = DtGeneral.Camaras.ExisteCamara("QReader");
        string sCamara = DtGeneral.Camaras.GetCamara("QReader");
        ZXing.ReaderCam lector;

        private void scanner_OnLectura(object sender, ProcessArgs e)
        {
            //////General.msjAviso(e.Resultado.Text); 
            /////itemEncode = CodificacionSNK.Decodificar(e.Resultado.Text);
            Decodificar(e.Resultado.Text); 

        }
        #endregion SCANER WEB CAM


        #region Funciones
        private void PosicionMensajes()
        {
            lblMensajes.Dock = DockStyle.Bottom; 
            //////lblMensajes.Width = (this.Width / 2) - 2;
            //////lblVerLotes.Width = lblMensajes.Width;

            //////lblMensajes.Top = (FrameLotes.Height + FrameLotes.Top + 5);
            //////lblVerLotes.Top = lblMensajes.Top;

            //////lblMensajes.Left = 0;
            //////lblVerLotes.Left = lblMensajes.Width + 2;

            //////lblVacio.Top = lblMensajes.Top;
            //////lblVacio.SendToBack(); 
            
        }

        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            /////Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento);
            lblLecturaLote.Text = "";

            //// Revisar implementacion de estas dos propiedades 
            //Lotes.EsLoteCaducado = false;
            //Lotes.LoteEnCero = false;

            //bLecturaCorrecta = true;
            txtCodigoLote.Focus();
            MostrarLotesCapturados(sIdProducto, sCodigoEAN);
            MostrarLotesCapturados("", "");
        }

        public void MostrarPantalla(string IdProducto, string CodigoEAN, string Descripcion, bool EsIdProducto_Ctrl, clsLotes Lote)
        {
            MostrarPantalla(IdProducto, CodigoEAN, Descripcion, EsIdProducto_Ctrl, Lote, false);
        }

        public void MostrarPantalla(string IdProducto, string CodigoEAN, string Descripcion, bool EsIdProducto_Ctrl, clsLotes Lote, bool ManejaUbicaciones)
        {
            MostrarPantalla(IdProducto, CodigoEAN, Descripcion, EsIdProducto_Ctrl, Lote, ManejaUbicaciones, false);
        }

        public void MostrarPantallaTE(clsLotes Lote)
        {
            MostrarPantalla("", "", "", false, Lote, false, true);
        }

        public void MostrarPantalla(string IdProducto, string CodigoEAN, string Descripcion, bool EsIdProducto_Ctrl, clsLotes Lote, bool ManejaUbicaciones, bool EsTransferenciaEntrada)
        {
            sIdProducto = IdProducto;
            sCodigoEAN = CodigoEAN;
            sDescProducto = Descripcion;
            bEsIdProducto_Ctrl = EsIdProducto_Ctrl;
            bManejaUbicaciones = ManejaUbicaciones;
            bEsTransferenciaEntrada = EsTransferenciaEntrada;
            this.Lotes = Lote;

            this.ShowDialog();
        }

        private void MostrarLotesCapturados(string IdProducto, string CodigoEAN )
        {
            string sSelect = "";
            dtsLotes = new DataSet();

            sSelect = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' and Cantidad > 0 ", IdProducto, CodigoEAN);
            dtsLotes = Lotes.DataSetLotes.Copy();
            
            //dtsLotes.Tables[0].Columns.Remove("MesesCad");           
            leerLotes.DataRowsClase = dtsLotes.Tables[0].Select(sSelect);            

            string []columnas = { "SubFarmacia", "ClaveLote", "Existencia", "Cantidad" };
            leerLotes.FiltrarColumnas(1, columnas);

            lst.LimpiarItems();
            lst.CargarDatos(leerLotes.DataSetClase);

            lst.AnchoColumna(1, 170);
            lst.AnchoColumna(2, 160);
            lst.AnchoColumna(3, 80);
            lst.AnchoColumna(4, 80);
            
        }
        #endregion Funciones

        #region Eventos_Lotes
        private void txtCodigoLote_Validating(object sender, CancelEventArgs e)
        {
            string sCadenaLocal = txtCodigoLote.Text;

            if (sCadena != "")
            {
                sCadenaLocal = sCadena;
            }


            sCadena = ""; 
            //lblLecturaLote.Text = ""; 
            if (sCadenaLocal != "")
            {
                Decodificar(sCadenaLocal);
            }

            try
            {
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                sCadenaLocal = ex.Message; 
            }

        }

        private void txtCodigoLote_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.ControlKey)
            //{
            //    sCadena += "|";
            //}
        }

        private void txtCodigoLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Encoding.ASCII.GetBytes(e.KeyChar.ToString())[0] != 13)
            {
                sCadena += e.KeyChar.ToString();
            }
            else
            {
                txtCodigo.ReadOnly = true;
                ////txtCodigoLote.Enabled = false;
                txtCodigoLote_Validating(null, new CancelEventArgs() );
                
                ////txtCodigoLote.Enabled = true;
                txtCodigo.ReadOnly = false;
                txtCodigoLote.Focus(); 
            }
        }

        private void X()
        {
            string IdSubFarmacia = "", sLote = "", sCadena = "";
            int iIndicador = 0;
            bool bCargaNuevoLote = true;

            lblLecturaLote.Text = "";
            ////Lotes.EsLoteCaducado = false;
            ////Lotes.LoteEnCero = false;

            sCadena = txtCodigoLote.Text;
            if (sCadena.Trim() != "")
            {

                IdSubFarmacia = sCadena.Substring(0, 2);
                iIndicador = Convert.ToInt32(sCadena.Substring(2, 1));
                sLote = sCadena.Substring(3, (sCadena.Length - 3));

                if (iIndicador == 0 || iIndicador == 1)
                {
                    if (iIndicador == 1)
                    {
                        sLote = "*" + sLote;
                    }

                    clsLotes[] ListaLotes = Lotes.Lotes(sIdProducto, sCodigoEAN);

                    foreach (clsLotes L in ListaLotes)
                    {
                        if (L.IdSubFarmacia == IdSubFarmacia && L.ClaveLote.ToUpper() == sLote.ToUpper())
                        {
                            Lotes.Incrementar_Cantidad(L.Codigo, L.CodigoEAN, L.IdSubFarmacia, L.ClaveLote);
                            
                            //////if (Lotes.EsLoteCaducado)
                            //////{
                            //////    lblLecturaLote.Text = sMsjLoteCaducado + sLote;
                            //////}

                            //////if (Lotes.LoteEnCero)
                            //////{
                            //////    lblLecturaLote.Text = sMsjLoteEnCero + sLote;
                            //////}

                            bCargaNuevoLote = false;
                            break;
                        }
                    }

                    if (bCargaNuevoLote)
                    {
                        CargaLoteCodigoEAN(sIdProducto, sCodigoEAN, IdSubFarmacia, sLote);
                    }

                    txtCodigoLote.Text = "";
                    txtCodigoLote.Focus();

                    if (Lotes.Cantidad > 0)
                    {
                        bProceso = true;
                    }
                    MostrarLotesCapturados(sIdProducto, sCodigoEAN);
                    
                }
                else
                {
                    //General.msjAviso("Captura de Lote incorrecta. Verifique...!");
                    lblLecturaLote.Text = "Captura de Lote incorrecta...  " + sCadena;
                    txtCodigoLote.Focus();
                }
            }
            else
            {
                txtCodigoLote.Focus();
            }
        }
        #endregion Eventos_Lotes

        #region Cerrar_Forma
        private void lblMensajes_Click(object sender, EventArgs e)
        {      
           this.Close();           
        }

        private void FrmLotesSNK_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F7:
                    ////lblVerLotes_Click(null, null);
                    break;

                case Keys.F5:
                    if (bManejaUbicaciones)
                        Ubicacion();
                ////lblVerLotes_Click(null, null);
                    break;

                case Keys.F12:                    
                       this.Close();                    
                    break;

                default:
                    break;
            }
        }
        #endregion Cerrar_Forma

        #region Manejo de lotes
        private void CargaLoteCodigoEAN(string IdProducto, string CodigoEAN, string IdSubFarmacia, string Lote)
        {
            string sCodigo = IdProducto;
            string sCodEAN = CodigoEAN;

            leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, Lotes.TipoDeInventario, Lotes.EsEntrada, "CargaLoteCodigoEAN()");
            if (query.Ejecuto)
            {
                //// Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                if (ValidarLote(sCodigo, sCodEAN, IdSubFarmacia, Lote))
                {
                    Lotes.AddLotes(leer.DataSetClase);

                    if (GnFarmacia.ManejaUbicaciones)
                    {
                        leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, Lotes.TipoDeInventario, Lotes.EsEntrada, "CargarLotesCodigoEAN()");
                        if (query.Ejecuto)
                        {
                            leer.Leer();
                            Lotes.AddLotesUbicaciones(leer.DataSetClase);
                        }
                    } 

                    //////////// se incrementa en automatico cuando se valida el lote 
                    //////Lotes.Incrementar_Cantidad(sCodigo, sCodEAN, IdSubFarmacia, Lote);
                    //mostrarOcultarLotes();                    
                }
                //else
                //{
                //    bLecturaCorrecta = false;
                //}
            }
        }

        private void CargarLotesCodigoEAN()
        {            
            string sCodigo = sIdProducto;
            string sCodEAN = sCodigoEAN;

            leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, Lotes.TipoDeInventario, Lotes.EsEntrada, "CargarLotesCodigoEAN()");
            if (query.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                leer.Leer();
                Lotes.AddLotes(leer.DataSetClase);

                ////if (GnFarmacia.ManejaUbicaciones)
                ////{
                ////    leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, Lotes.TipoDeInventario, "CargarLotesCodigoEAN()");
                ////    if (query.Ejecuto)
                ////    {
                ////        leer.Leer();
                ////        Lotes.AddLotesUbicaciones(leer.DataSetClase);
                ////    }
                ////}

                mostrarOcultarLotes();
            }
        }

        private void mostrarOcultarLotes()
        {                            
            Lotes.Codigo = sIdProducto;
            Lotes.CodigoEAN = sCodigoEAN;
            Lotes.Descripcion = sDescProducto;
            Lotes.EsEntrada = false;// para las ventas
            Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

            // Si el movimiento ya fue aplicado no es posible agregar lotes 
            Lotes.CapturarLotes = false;
            Lotes.ModificarCantidades = bEsIdProducto_Ctrl ? false : true;
            
            //Configurar Encabezados 
            Lotes.Encabezados = EncabezadosManejoLotes.Default;
            // Lotes.ManejoLotes = OrigenManejoLotes.Ventas_Dispensacion; 

            // Mostrar la Pantalla de Lotes 
            Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;
            
            {
                Lotes.Show();
            }

            iCant_Lote = iCant_Lote + Lotes.Cantidad;                    
            
            
        }

        private bool ValidarLote(string IdProducto, string CodigoEAN, string IdSubFarmacia, string Lote)
        {
            bool bRegresa = true;
            string sCodigo = IdProducto;
            string sCodEAN = CodigoEAN;

            leer2.DataSetClase = query.Codigo_CodigoEAN_Lotes(sEmpresa, sEstado, sFarmacia, IdSubFarmacia, sCodigo, sCodEAN, Lote, "ValidarLote()");
            if (query.Ejecuto)
            {
                if (!leer2.Leer())
                {
                    bRegresa = false;
                    //General.msjAviso("No se encontro el Lote del Producto. Verifique...");
                    lblLecturaLote.Text = "Lote no encontrado...  " + Lote;
                }
                else
                {
                    if (leer2.CampoInt("Existencia") == 0)
                    {
                        bRegresa = false;
                        //General.msjAviso("La Existencia del lote se encuentra en ceros...");
                        lblLecturaLote.Text = sMsjLoteEnCero + Lote;
                    }

                    if (leer2.CampoInt("MesesCad") < 0)
                    {
                        bRegresa = false;
                        lblLecturaLote.Text = sMsjLoteCaducado + Lote;
                    }
                }
            }
            else
            {
                bRegresa = false;
            }

            return bRegresa;
        }
        #endregion Manejo de lotes

        #region Ubicacion
        private void CargarUbicacionesRegistradas()
        {
            string sSql = string.Format("Select * " +
                " From vw_Pasillos_Estantes_Entrepaños (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' ",
                sEmpresa, sEstado, sFarmacia);

                if (leer.Exec(sSql))
                {
                    dtsLotesUbicacionesRegistradas = leer.DataSetClase;
                }
        }

        #endregion Ubicacion

        #region Ver_Lotes
        private void lblVerLotes_Click(object sender, EventArgs e)
        {
            //////bool bCargaNuevoLote = true;

            //////clsLotes[] ListaLotes = Lotes.Lotes(itemEncode.IdProducto, itemEncode.CodigoEAN);

            //////foreach (clsLotes L in ListaLotes)
            //////{
            //////    if (L.Codigo == sIdProducto && L.CodigoEAN == sCodigoEAN)
            //////    {                    
            //////        mostrarOcultarLotes();
            //////        bCargaNuevoLote = false;
            //////        break;
            //////    }
            //////}

            //////if (bCargaNuevoLote)
            //////{
            //////    CargarLotesCodigoEAN();
            //////}

            //////if (Lotes.Cantidad > 0)
            //////{
            //////    bProceso = true;
            //////}
            //////MostrarLotesCapturados(itemEncode.IdProducto, itemEncode.CodigoEAN);
            
        }
        #endregion Ver_Lotes

        #region CODIFICACION 
        private void Decodificar(string UUID)
        {
            if (Lotes.UUID_Exists(UUID))
            {
                lblLecturaLote.Text = "El producto ya fue registrado anteriormente.";
                Console.Beep();

                if (!txtCodigoLote.Enabled) txtCodigoLote.Enabled = true;

                txtCodigoLote.Text = "";
                txtCodigoLote.Focus();
            }
            else
            {
                Decodificar_Registrar(UUID);

                if (!txtCodigoLote.Enabled) txtCodigoLote.Enabled = true;
                txtCodigoLote.Focus();
            }
        }

        private void Decodificar_Registrar(string UUID)
        {
            lblLecturaLote.Text = "";

            itemEncode.IdEmpresa_Local = DtGeneral.EmpresaConectada;
            itemEncode.IdEstado_Local = DtGeneral.EstadoConectado;
            itemEncode.IdFarmacia_Local = DtGeneral.FarmaciaConectada;

            itemEncode = CodificacionSNK.Decodificar_Segmentos(UUID, true);

            if ((!itemEncode.UUID_Valido && GnFarmacia.ImplementaCodificacion_DM) && !bEsTransferenciaEntrada)
            {
                lblLecturaLote.Text = itemEncode.Resultado;
                lblLecturaLote.ForeColor = itemEncode.ColorResultado;
            }
            else
            {
                if (!bEsTransferenciaEntrada)
                {
                    Decodificar_CargarInformacion();
                }
                else
                {
                    Decodificar_CargarInformacion_TE();
                }
            }
        }


        private void Decodificar_CargarInformacion_TE()
        {
            clsLotes_ItemUUID itemUUID = new clsLotes_ItemUUID(); 

            Lotes.Incrementar_CantidadTE(itemEncode.IdSubFarmacia, itemEncode.SubFarmacia, itemEncode.IdProducto, itemEncode.CodigoEAN, itemEncode.ClaveLote );

            itemUUID.IdSubFarmacia = itemEncode.IdSubFarmacia;
            itemUUID.Codigo = itemEncode.IdProducto;
            itemUUID.CodigoEAN = itemEncode.CodigoEAN; 
            itemUUID.ClaveLote = itemEncode.ClaveLote; 
 
            MostrarLotesCapturados(itemEncode.IdProducto, itemEncode.CodigoEAN);
            Lotes.UUID_Add(itemEncode.UUID, itemUUID);

            txtCodigoLote.Text = "";
            txtCodigoLote.Focus();
        }

        private void Decodificar_CargarInformacion()
        {
            clsLotes[] existeEAN; 
            string sSql = "", IdSubFarmacia = "", sLote = "", sCadena = "";
            int iIndicador = 0;
            bool bCargaNuevoLote = true, bContinua = true;
            clsLotes_ItemUUID itemUUID = new clsLotes_ItemUUID(); 


            if (itemEncode.CodigoEAN == "")
            {
                sSql = string.Format("Select F.IdProducto, F.CodigoEAN, F.IdSubFarmacia, S.Descripcion As SubFarmacia, ClaveLote, Existencia " +
                    "From FarmaciaProductos_CodigoEAN_Lotes F (NoLock) " +
                    "Inner Join vw_Productos_CodigoEAN P (NoLock) On (F.CodigoEAN = P.CodigoEAN) " +
                    "Inner Join CatFarmacias_SubFarmacias S (NoLock) On (F.IdEstado = S.IdEstado And F.IdFarmacia = S.IdFarmacia And F.IdSubFarmacia = S.IdSubFarmacia) " +
                    "Where F.IdEmpresa = '{0}' And F.IdEstado = '{1}' And F.IdFarmacia = '{2}' And F.ClaveLote = '{3}' And P.ClaveSSA = '{4}'",
                     DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, itemEncode.ClaveLote, itemEncode.ClaveSSA);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Decodificar_CargarInformacion()");
                    General.msjError("Ocurrio un error al obtener el código EAN");
                    bContinua = false;
                }
                else
                {
                    if (!leer.Leer())
                    {
                        bContinua = false;
                        General.msjAviso("No se encontro ningun Código EAN, verifique.");
                    }
                    else 
                    {
                        if (leer.Registros == 1)
                        {
                            //// Existe solo un registro 
                            itemEncode.IdProducto = leer.Campo("IdProducto");
                            itemEncode.CodigoEAN = leer.Campo("CodigoEAN");
                            itemEncode.IdSubFarmacia = leer.Campo("IdSubFarmacia");
                        }
                        else
                        {
                            //// Existen diversos registros, se muestra pantalla para seleccionar 
                            f = new FrmLotesSubFarmacias();
                            f.show(leer);
                            itemEncode.IdProducto = f.sIdproducto;
                            itemEncode.CodigoEAN = f.sCodigoEAN;
                            itemEncode.IdSubFarmacia = f.sIdSubFarmacia;
                        }
                    }
                }

            }

            if (bContinua)
            {
                clsLotes[] ListaLotes = Lotes.Lotes(itemEncode.IdProducto, itemEncode.CodigoEAN);
                if (ListaLotes.Length == 0)
                {
                    CargaLoteCodigoEAN(itemEncode.IdProducto, itemEncode.CodigoEAN, itemEncode.IdSubFarmacia, itemEncode.ClaveLote_SubFarmacia);
                    ListaLotes = Lotes.Lotes(itemEncode.IdProducto, itemEncode.CodigoEAN);
                }

                //// Buscar el Lote para incrementar las cantidades 
                foreach (clsLotes L in ListaLotes)
                {
                    if (L.IdSubFarmacia == itemEncode.IdSubFarmacia && L.ClaveLote.ToUpper() == itemEncode.ClaveLote_SubFarmacia.ToUpper())
                    {
                        if (GnFarmacia.ManejaUbicaciones)
                        {
                            itemEncode.ExistenciaUbicacion = Lotes.Incrementar_Cantidad_Ubicaion(L.Codigo, L.CodigoEAN, L.IdSubFarmacia, L.ClaveLote, txtEstante.Text, txtPasillo.Text, txtEntrepaño.Text);
                        }

                        if (itemEncode.ExistenciaUbicacion)
                        {
                            Lotes.Incrementar_Cantidad(L.Codigo, L.CodigoEAN, L.IdSubFarmacia, L.ClaveLote);
                        }

                        //////if (Lotes.EsLoteCaducado) 
                        //////{ 
                        //////    lblLecturaLote.Text = sMsjLoteCaducado + sLote; 
                        //////} 

                        //////if (Lotes.LoteEnCero)  
                        //////{ 
                        //////    lblLecturaLote.Text = sMsjLoteEnCero + sLote; 
                        //////} 

                        bCargaNuevoLote = false;
                        break;
                    }
                }

                if (itemEncode.ExistenciaUbicacion)
                {
                    itemUUID.IdSubFarmacia = itemEncode.IdSubFarmacia;
                    itemUUID.Codigo = itemEncode.IdProducto;
                    itemUUID.CodigoEAN = itemEncode.CodigoEAN;
                    itemUUID.ClaveLote = itemEncode.ClaveLote; 

                    Lotes.UUID_Add(itemEncode.UUID, itemUUID);
                }

                MostrarLotesCapturados(itemEncode.IdProducto, itemEncode.CodigoEAN);
            }


            if (bContinua && GnFarmacia.ImplementaReaderDM)
            {
                itemEncode.Existe_UUID = true; 
            }

            ////if (itemEncode.Existe_UUID)
            {
                lblLecturaLote.Text = itemEncode.Resultado;
                lblLecturaLote.ForeColor = itemEncode.ColorResultado;
            }
            ////else
            ////{
            ////    if (!GnFarmacia.ImplementaReaderDM)
            ////    {
            ////        lblLecturaLote.Text = itemEncode.Resultado;
            ////        lblLecturaLote.ForeColor = itemEncode.ColorResultado;
            ////    }
            ////    else
            ////    {
            ////        lblLecturaLote.Text = "UUID decodificado";
            ////        lblLecturaLote.ForeColor = Color.Black; 
            ////    }
            ////}


            txtCodigoLote.Text = "";
            txtCodigoLote.Focus();
        }
        #endregion CODIFICACION 

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

        #region Busqueda de Ubicaciones
        private string Pasillo()
        {
            return DatoUbicacion(string.Format(" IdPasillo = '{0}' ", txtPasillo.Text), 1);
        }

        private string Estante()
        {
            return DatoUbicacion(string.Format(" IdPasillo = '{0}' and IdEstante = '{1}' ",
                txtPasillo.Text, txtEstante.Text), 2);
        }

        private string Entrepano()
        {
            return DatoUbicacion(string.Format(" IdPasillo = '{0}' and IdEstante = '{1}' and IdEntrepaño = '{2}' ",
                txtPasillo.Text, txtEstante.Text, txtEntrepaño.Text), 3);
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
                        sDato = "ENTREPAÑO # " + txtEntrepaño.Text;
                        sRegresa = ubica.Campo("DescripcionEntrepaño");
                        break;
                }
            }

            sRegresa = sRegresa != "" ? sRegresa : sDato;

            return sRegresa;
        }
        #endregion Busqueda de Ubicaciones

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            lector.IniciarCaptura();
            gpoUbicaciones.Visible = false;
            txtCodigoLote.Enabled = true;
            txtCodigoLote.Focus();
        }

        private void Ubicacion()
        {
            gpoUbicaciones.Visible = true;
            txtPasillo.Text = "";
            txtCodigo.Enabled = false;
            txtPasillo.Focus();
            lector.DetenerCaptura();
        }
    }
}
