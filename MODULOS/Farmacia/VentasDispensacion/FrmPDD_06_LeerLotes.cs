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

using Farmacia.Procesos;
using Farmacia.Vales;

////using Dll_IMach4;
////using Dll_IMach4.Interface;

using DllFarmaciaSoft.Ayudas;

using Farmacia.Ventas; 

namespace Farmacia.VentasDispensacion
{
    public partial class FrmPDD_06_LeerLotes : FrmBaseExt 
    {
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerLotes;
        clsLeer leer2;
        clsConsultas query;
        clsAyudas ayuda;

        clsLotesExt Lotes;
        clsListView lst;
        clsValidarLote validarLote; 

        DataSet dtsLotes;

        public string sCodigoEAN = "";
        public string sIdProducto = "";
        string sDescProducto = "";        
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public bool bProceso = false, bEsIdProducto_Ctrl = false;
        //public bool bLecturaCorrecta = true;
        //string sMsjLecturaCorrecta = "Lectura Correcta  ";
        string sMsjLoteCaducado = "Lote Caducado...  ";
        string sMsjLoteEnCero = "Existencia de Lote en ceros...  ";

        public int iCant_Lote = 0;

        #region Propiedades
        public clsLotesExt LotesCodigos
        {
            get { return Lotes; }
        }
        #endregion Propiedades

        public FrmPDD_06_LeerLotes()
        {
            InitializeComponent();

            con = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref con);
            leerLotes = new clsLeer(ref con);
            leer2 = new clsLeer(ref con);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);


            validarLote = new clsValidarLote(txtCodigoLote);
            validarLote.NoLetrasAlInicio = 3;
            validarLote.ValidarLetrasAlInicio = true; 


            lst = new clsListView(lstLotes);
            lst.PermitirAjusteDeColumnas = false;
            lst.AnchoColumna(1, 170);
            lst.AnchoColumna(2, 160);
            lst.AnchoColumna(3, 80);
            lst.AnchoColumna(4, 80);

            PosicionMensajes();
        }        

        private void FrmPDD_06_LeerLotes_Load(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        #region Funciones
        private void PosicionMensajes()
        {
            lblMensajes.Width = (this.Width / 2) - 2;
            lblVerLotes.Width = lblMensajes.Width;

            lblMensajes.Top = (FrameLotes.Height + FrameLotes.Top + 5);
            lblVerLotes.Top = lblMensajes.Top;

            lblMensajes.Left = 0;
            lblVerLotes.Left = lblMensajes.Width + 2;

            lblVacio.Top = lblMensajes.Top;
            lblVacio.SendToBack(); 
            
        }

        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            //Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento);
            lblLecturaLote.Text = "";
            Lotes.EsLoteCaducado = false;
            Lotes.LoteEnCero = false;
            //bLecturaCorrecta = true;
            txtCodigoLote.Focus();
            MostrarLotesCapturados();
        }

        public void MostrarPantalla(string IdProducto, string CodigoEAN, string Descripcion, bool EsIdProducto_Ctrl, clsLotesExt Lote)
        {
            sIdProducto = IdProducto;
            sCodigoEAN = CodigoEAN;
            sDescProducto = Descripcion;
            bEsIdProducto_Ctrl = EsIdProducto_Ctrl;
            this.Lotes = Lote;        

            this.ShowDialog();
        }

        private void MostrarLotesCapturados()
        {
            string sSelect = "";
            dtsLotes = new DataSet();

            sSelect = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}' and Cantidad > 0 ", sIdProducto, sCodigoEAN);
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
            string IdSubFarmacia = "", sLote = "", sCadena = "";
            int iIndicador = 0;
            bool bCargaNuevoLote = true;

            lblLecturaLote.Text = "";
            Lotes.EsLoteCaducado = false;
            Lotes.LoteEnCero = false;

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
                            //mostrarOcultarLotes();
                            if (Lotes.EsLoteCaducado)
                            {
                                lblLecturaLote.Text = sMsjLoteCaducado + sLote;
                                //bLecturaCorrecta = false;
                            }

                            if (Lotes.LoteEnCero)
                            {
                                lblLecturaLote.Text = sMsjLoteEnCero + sLote;
                                //bLecturaCorrecta = false;
                            }
                            bCargaNuevoLote = false;
                            break;
                        }
                    }

                    if (bCargaNuevoLote)
                    {
                        CargaLoteCodigoEAN(IdSubFarmacia, sLote);
                    }

                    txtCodigoLote.Text = "";
                    txtCodigoLote.Focus();

                    if (Lotes.Cantidad > 0)
                    {
                        bProceso = true;
                    }
                    MostrarLotesCapturados();
                    
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

        private void FrmPDD_06_LeerLotes_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F7:
                    lblVerLotes_Click(null, null);
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
        private void CargaLoteCodigoEAN(string IdSubFarmacia, string Lote)
        {
            string sCodigo = sIdProducto;
            string sCodEAN = sCodigoEAN;

            leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Todos, false, "CargaLoteCodigoEAN()");
            if (query.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                if (ValidarLote(IdSubFarmacia, Lote))
                {
                    Lotes.AddLotes(leer.DataSetClase);

                    ////if (GnFarmacia.ManejaUbicaciones)
                    ////{
                    ////    leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Todos, "CargarLotesCodigoEAN()");
                    ////    if (query.Ejecuto)
                    ////    {
                    ////        leer.Leer();
                    ////        Lotes.AddLotesUbicaciones(leer.DataSetClase);
                    ////    }
                    ////}
                    Lotes.Incrementar_Cantidad(sCodigo, sCodEAN, IdSubFarmacia, Lote);
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

            leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Todos, false, "CargarLotesCodigoEAN()");
            if (query.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                leer.Leer();
                Lotes.AddLotes(leer.DataSetClase);

                ////if (GnFarmacia.ManejaUbicaciones)
                ////{
                ////    leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Todos, "CargarLotesCodigoEAN()");
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

        private bool ValidarLote(string IdSubFarmacia, string Lote)
        {
            bool bRegresa = true;
            string sCodigo = sIdProducto;
            string sCodEAN = sCodigoEAN;

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

        #region Ver_Lotes
        private void lblVerLotes_Click(object sender, EventArgs e)
        {
            bool bCargaNuevoLote = true;

            clsLotes[] ListaLotes = Lotes.Lotes(sIdProducto, sCodigoEAN);

            foreach (clsLotes L in ListaLotes)
            {
                if (L.Codigo == sIdProducto && L.CodigoEAN == sCodigoEAN)
                {                    
                    mostrarOcultarLotes();
                    bCargaNuevoLote = false;
                    break;
                }
            }

            if (bCargaNuevoLote)
            {
                CargarLotesCodigoEAN();
            }

            if (Lotes.Cantidad > 0)
            {
                bProceso = true;
            }
            MostrarLotesCapturados();
            
        }
        #endregion Ver_Lotes
    }
}
