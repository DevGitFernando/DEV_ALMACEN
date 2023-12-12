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
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft; 
using DllFarmaciaSoft.Lotes;

namespace Farmacia.Transferencias
{
    #region Form 
    public partial class FrmClavesSolicitadasTransferencias : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsConsultas Consultas; 
        clsAyudas Ayuda;
        clsGrid myGrid;
        // string sFolioPedido = "", sMensaje = "", 
        string sValorGrid = "";
        string sEstado = DtGeneral.EstadoConectado;

        public string sFolio = ""; 
        public string sObservaciones = "";
        public DataSet dtsClavesCargadas = clsLotes.PreparaDtsClaves(); 

        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, IdClaveSSA = 2, Descripcion = 3, Cantidad = 4
        }

        public FrmClavesSolicitadasTransferencias() // string  Folio, string Observaciones, DataSet DatosClaves)
        {
            InitializeComponent();
            
            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White;

            ////this.sFolio = Folio;
            ////this.sObservaciones = Observaciones;
            ////this.dtsClavesCargadas = DatosClaves; 
        }

        private void FrmClavesSolicitadas_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Limpiar 
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;

            btnImprimir.Enabled = false;
        }

        private void LimpiarPantalla()
        {
            myGrid.Limpiar(true);
            Fg.IniciaControles();
            IniciarToolBar(true, false, false);
 
            ////lblStatus.Text = "CANCELADA"; //Se pone aqui ya que el IniciaControles le borra el texto.
            ////lblStatus.Visible = false;
            ////lblStatus.Text = "";
            ////lblStatus.Visible = false;

            lblUnidades.Text = "0";

            if (sFolio.Trim() != "*" && sFolio.Trim() != "")
            {
                MostrarDetalleClaves();
            }
            else
            {
                // Cargar datos Folio y Claves previamente cargadas 
                this.txtObservaciones.Text = sObservaciones;
                myGrid.LlenarGrid(dtsClavesCargadas);
                lblUnidades.Text = myGrid.TotalizarColumna((int)Cols.Cantidad).ToString();

                grdProductos.Focus();
                myGrid.SetActiveCell(1, 1);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }         
        #endregion Limpiar 

        #region Guardar Informacion 
        ////public DataSet MostrarCapturaClaves(DataSet ClavesCargadas)
        ////{
        ////    this.ShowDialog(); 
        ////    return dtsClavesCargadas; 
        ////} 

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DataSet dtsPaso = clsLotes.PreparaDtsClaves();

            if( ValidaDatos())
            {
                try
                {
                    for (int i = 1; i <= myGrid.Rows; i++)
                    {
                        if (myGrid.GetValue(i, (int)Cols.Descripcion) != "")//Esto es para que no guarde los renglones vacios.
                        {
                            object[] obj = 
                            {
                                myGrid.GetValue(i, (int)Cols.ClaveSSA),
                                myGrid.GetValue(i, (int)Cols.IdClaveSSA),
                                myGrid.GetValue(i, (int)Cols.Descripcion),
                                myGrid.GetValueInt(i, (int)Cols.Cantidad)
                            };
                            dtsPaso.Tables[0].Rows.Add(obj);
                        }
                    }
                    sObservaciones = txtObservaciones.Text;
                    dtsClavesCargadas = dtsPaso;
                    this.Hide();
                }
                catch (Exception ex)
                {
                    ex.Source = ex.Source; 
                }
            }

        } 
        #endregion Guardar Informacion 

        #region Grid 
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (myGrid.ActiveCol == 1)
            {
                if (e.KeyCode == Keys.F1)
                {
                    myLeer.DataSetClase = Ayuda.ClavesSSA_Sales("grdProductos_KeyDown");
                    if (myLeer.Leer())
                    {
                        myGrid.SetValue(myGrid.ActiveRow, 1, myLeer.Campo("IdClaveSSa_Sal"));
                        CargaDatosSal();
                    }
                }

                if (e.KeyCode == Keys.Delete)
                {
                    myGrid.DeleteRow(myGrid.ActiveRow);

                    if (myGrid.Rows == 0)
                    {
                        myGrid.Limpiar(true);
                    }
                    lblUnidades.Text = myGrid.TotalizarColumna((int)Cols.Cantidad).ToString(); 
                }

            }
            lblUnidades.Text = myGrid.TotalizarColumna(4).ToString();
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            // if (lblStatus.Visible == false)
            {
                if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                {
                    if (myGrid.GetValue(myGrid.ActiveRow, 1) != "" && myGrid.GetValue(myGrid.ActiveRow, 3) != "")
                    {
                        myGrid.Rows = myGrid.Rows + 1;
                        myGrid.ActiveRow = myGrid.Rows;
                        myGrid.SetActiveCell(myGrid.Rows, 1);
                    }
                }
            }
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA);
            lblUnidades.Text = myGrid.TotalizarColumna((int)Cols.Cantidad).ToString(); 
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            switch (myGrid.ActiveCol)
            {
                case 1:
                    {
                        ObtenerDatosSal();
                    }

                    break;
            }
            lblUnidades.Text = myGrid.TotalizarColumna(4).ToString();
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= myGrid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                myGrid.SetValue(myGrid.ActiveRow, i, "");
            }
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                if (myGrid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                    myGrid.DeleteRow(i);
            }

            if (myGrid.Rows == 0) // Si No existen renglones, se inserta 1.
                myGrid.AddRow();
        }

        private void ObtenerDatosSal()
        {
            string sCodigo = "", sSql = "";
            // int iCantidad = 0;

            sCodigo = myGrid.GetValue(myGrid.ActiveRow, 1);

            if ( sCodigo.Trim() == "" )
            {
                General.msjUser("Sal no encontrada ó no esta Asignada a la Farmacia.");
                myGrid.LimpiarRenglon(myGrid.ActiveRow);
            }
            else
            {
                //sSql = string.Format("Exec spp_SalesPedidosSecretaria '{0}', '{1}', '{2}' ", sIdClienteEstado, Fg.PonCeros(sCodigo, 4), sCodigo);
                sSql = String.Format("Select ClaveSSA, IdClaveSSA_Sal, DescripcionSal, 0.0000 as Cantidad " + 
                    " From vw_ClavesSSA_Sales(NoLock) " + 
                    " Where ClaveSSA = '{0}' ", sCodigo);

                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "ObtenerDatosSal()");
                    General.msjError("Ocurrió un error al obtener la información de la Sal.");
                }
                else
                {
                    if (!myLeer.Leer())
                    {
                        General.msjUser("Sal no encontrada ó no esta Asignada al Cliente.");
                        myGrid.LimpiarRenglon(myGrid.ActiveRow);
                    }
                    else
                    {
                        CargaDatosSal();
                    }
                }
            }
            lblUnidades.Text = myGrid.TotalizarColumna((int)Cols.Cantidad).ToString(); 
        }

        private void CargaDatosSal()
        {
            int iRowActivo = myGrid.ActiveRow;

            //if (lblStatus.Visible == false)
            {
                if (!myGrid.BuscaRepetido(myLeer.Campo("ClaveSSA"), iRowActivo, 1))
                {
                    myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));
                    myGrid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, myLeer.Campo("IdClaveSSA_Sal"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("DescripcionSal"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, myLeer.Campo("Cantidad"));
                    myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                    myGrid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);
                }
                else
                {
                    General.msjUser("Este Producto ya se encuentra capturado en otro renglon.");
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Cantidad, "");
                    limpiarColumnas();
                    myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                }

            }
        }

        private void grdProductos_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
        }

        private void MostrarDetalleClaves()
        {
            string sSql = String.Format(" Select C.ClaveSSA, V.IdClaveSSA, C.DescripcionSal, V.CantidadRequerida, V.Observaciones " +
                                        " From TransferenciasEstadisticaClavesDispensadas V (NoLock) " +
	                                    " Inner Join vw_ClavesSSA_Sales C (NoLock) " +
		                                  "   On( V.IdClaveSSA = C.IdClaveSSA_Sal ) " +
	                                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
                                        " and RIGHT(FolioTransferencia, 8) = '{3}' and EsCapturada = 1  " +
	                                    " Order By C.DescripcionSal ",
                                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolio);
            
            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "MostrarDetalleClaves()");
                General.msjError("Ocurrió un error al obtener la información de las Claves.");
            }
            else
            {
                if (!myLeer.Leer())
                {
                    General.msjUser("No se encontró Información de las Claves.");
                    myGrid.BloqueaGrid(true);
                    btnGuardar.Enabled = false;
                    btnNuevo.Enabled = false;
                    txtObservaciones.Enabled = false;
                }
                else
                {
                    myGrid.LlenarGrid(myLeer.DataSetClase, false, false);
                    myGrid.BloqueaGrid(true);
                    btnGuardar.Enabled = false;
                    btnNuevo.Enabled = false;
                    txtObservaciones.Text = myLeer.Campo("Observaciones");
                    txtObservaciones.Enabled = false;
                    lblUnidades.Text = myGrid.TotalizarColumna((int)Cols.Cantidad).ToString();
                }
            }
        }
        #endregion Grid

        #region Validaciones de Controles 
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese las observaciones por favor.");
                txtObservaciones.Focus();
            }

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            return bRegresa;
        }

        private bool validarCapturaProductos()
        {
            bool bRegresa = true;

            if (myGrid.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                if (myGrid.GetValue(1, (int)Cols.Descripcion) == "")
                {
                    bRegresa = false;
                }
                else
                {
                    if ( int.Parse( lblUnidades.Text ) == 0 )
                    {
                        bRegresa = false;
                    }
                    else
                    {
                        for (int i = 1; i <= myGrid.Rows; i++)
                        {
                            if (myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (!bRegresa)
                General.msjUser("Debe capturar al menos una Clave para el Pedido\n y/o capturar cantidades para al menos una Clave, verifique.");

            return bRegresa;

        } 
        #endregion Validaciones de Controles 
    }
    #endregion Form
    
    public class clsClavesSolicitadasTransferencia
    {
        private string sEmpresa = ""; 
        private string sIdEstado = ""; 
        private string sIdFarmacia = "";

        FrmClavesSolicitadasTransferencias f; 
        basGenerales Fg = new basGenerales();
        DataSet dtsClavesCargadas;

        clsLeer leerClaves = new clsLeer(); 

        ////private bool bCapturaCompleta = false;
        ////private bool bVigenciaValida = false;
        ////private bool bEsActivo = false;

        ////bool bEsSeguroPopular = false; 
        ////bool bPermitirCapturaBeneficiariosNuevos = false;
        ////bool bPermitirImportarBeneficiarios = false; 

        // string sFolioVenta = "";
        string sObservaciones = "";

        string sClaveSSA = "";
        string sIdClaveSSA = "";
        string sDescripcion = "";
        int iCantidadSolicitada = 0; 

        #region Constructor y Destructor de Clase 
        public clsClavesSolicitadasTransferencia(string IdEmpresa, string IdEstado, string IdFarmacia)
        {
            this.sEmpresa = IdEmpresa; 
            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
            dtsClavesCargadas = clsLotes.PreparaDtsClaves();
            leerClaves.DataSetClase = dtsClavesCargadas; 
        }
        #endregion Constructor y Destructor de Clase 

        #region Propiedades 
        public string IdClaveSSA
        {
            get { return sIdClaveSSA; }
        }

        public string ClaveSSA
        {
            get { return sClaveSSA; }
        }

        public string Descripcion
        {
            get { return sDescripcion; }
        }

        public int Cantidad
        {
            get { return iCantidadSolicitada; }
        }

        public DataSet ClavesCapturadas
        {
            get { return leerClaves.DataSetClase; }
        }

        public string Observaciones
        {
            get { return sObservaciones; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public void Show(string FolioTransferencia)
        {
            f = new FrmClavesSolicitadasTransferencias();

            f.sFolio = FolioTransferencia;
            f.sObservaciones = sObservaciones;
            f.dtsClavesCargadas = dtsClavesCargadas; 
            f.ShowDialog();

            sObservaciones = f.sObservaciones;
            dtsClavesCargadas = f.dtsClavesCargadas;
            leerClaves.DataSetClase = dtsClavesCargadas; 
        }

        public bool Claves()
        {
            bool bRegresa = false;

            sIdClaveSSA = "";
            iCantidadSolicitada = 0;
             
            while (leerClaves.Leer())
            {
                bRegresa = true;
                sClaveSSA = leerClaves.Campo("ClaveSSA");
                sIdClaveSSA = leerClaves.Campo("IdClaveSSA");
                sDescripcion = leerClaves.Campo("Descripcion");
                iCantidadSolicitada = leerClaves.CampoInt("Cantidad");

                // General.msjAviso(string.Format("{0}    {1}", sIdClaveSSA, iCantidadSolicitada));
            }

            return bRegresa; 
        }

        #endregion Funciones y Procedimientos Publicos 
    }
}
