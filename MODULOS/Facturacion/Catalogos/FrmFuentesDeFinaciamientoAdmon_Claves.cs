using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using DllFarmaciaSoft;

namespace Facturacion.Catalogos
{
    public partial class FrmFuentesDeFinaciamientoAdmon_Claves : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer, myLeerDetalles;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;
        
        string sFolio = "";
        string sMensaje = "";
        string sIdFinanciamiento = "";
        string sFinanciamiento = "";
        bool bValidarPoliza = false;
        int iLongitudMinima = 0;
        int iLongitudMaxima = 0; 
        // bool bFolioGuardado = false;
        
        //Para Auditoria
        clsAuditoria auditoria;

        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, IdClaveSSA = 2, Descripcion = 3, 
            Presentacion = 4, Costo = 5, Agrupacion = 6, 
            CostoUnitario = 7, TasaIva= 8, Iva = 9, Importe = 10, Activar = 11, Guardado = 12 
        }
        
        #region Propiedades 
        public string FuenteFinanciamiento
        {
            get { return sFolio; }
            set { sFolio = value; }
        }

        public string IdFinanciamiento
        {
            get { return sIdFinanciamiento;}
            set { sIdFinanciamiento = value; }
        }

        public string Financiamiento
        {
            get { return sFinanciamiento; }
            set { sFinanciamiento = value; }
        }

        public bool ValidarPoliza
        {
            get { return bValidarPoliza; }
            set { bValidarPoliza = value; }
        }

        public int LongitudMinimaPoliza
        {
            get { return iLongitudMinima; }
            set { iLongitudMinima = value; }
        }

        public int LongitudMaximaPoliza
        {
            get { return iLongitudMaxima; }
            set { iLongitudMaxima = value; }
        }
        #endregion Propiedades 

        public FrmFuentesDeFinaciamientoAdmon_Claves()
        {
            InitializeComponent();

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");
            myLeer = new clsLeer(ref ConexionLocal);
            myLeerDetalles = new clsLeer(ref ConexionLocal);
            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);

            myGrid = new clsGrid(ref grdClaves, this);
            myGrid.BackColorColsBlk = Color.White;
            myGrid.EstiloDeGrid = eModoGrid.ModoRow; 
            grdClaves.EditModeReplace = true;

            myGrid.SetOrder(true);

            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            
        }

        private void FrmFuentesDeFinaciamiento_Claves_Load(object sender, EventArgs e)
        {
            //btnNuevo_Click(null, null);
            lblFinanciamiento.Text = sIdFinanciamiento;
            lblDescripcion.Text = sFinanciamiento;

            ////chkValidarReferencia.Checked = bValidarPoliza;
            ////nmDesde.Value = (decimal)iLongitudMinima;
            ////nmHasta.Value = (decimal)iLongitudMaxima;
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            IniciarToolBar(false, true, false, false);
            myGrid.Limpiar(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;
            EliminarRenglonesVacios();

            if (ValidaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    bContinua = GuardarClaves();                    

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        IniciarToolBar(false, false, false, true);
                        this.Hide();
                    }
                    else
                    {
                        //txtFolio.Text = "*";
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la Información.");
                        IniciarToolBar(false, true, false, false);                            

                    }

                    ConexionLocal.Cerrar();
                }
                else
                {
                    Error.LogError(ConexionLocal.MensajeError);
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                }

            }            

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {            
        }
        #endregion Botones

        #region Funciones
        private void IniciarToolBar(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        ////private bool Guardar_ValidarPoliza()
        ////{
        ////    bool bRegresa = false;
        ////    int iValidarPoliza = chkValidarReferencia.Checked ? 1 : 0;

        ////    bValidarPoliza = chkValidarReferencia.Checked;
        ////    iLongitudMinima = (int)nmDesde.Value;
        ////    iLongitudMaxima = (int)nmHasta.Value; 

        ////    string sSql = string.Format("Exec spp_Mtto_FACT_Fuentes_De_Financiamiento_Detalles_Poliza '{0}', '{1}', '{2}', '{3}', '{4}' ",
        ////                                sFolio, sIdFinanciamiento, iValidarPoliza, (int)nmDesde.Value, (int)nmHasta.Value); 

        ////    bRegresa = myLeer.Exec(sSql); 
        ////    if (bRegresa)
        ////    {
        ////        bRegresa = GuardarClaves(); 
        ////    }

        ////    return bRegresa; 
        ////} 

        private bool GuardarClaves()
        {
            bool bRegresa = true, bActivar = true;
            string sSql = "";
            string sClaveSSA = "";
            int iOpcion = 0;

            double iCosto = 0;
            int iAgrupacion = 0; 
            double iCostoUnitario = 0;
            double iTasaIva = 0;
            double iIva = 0;
            double iImporteNeto = 0; 

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sClaveSSA = myGrid.GetValue(i, (int)Cols.ClaveSSA);
                bActivar = myGrid.GetValueBool(i, (int)Cols.Activar);

                iCosto = myGrid.GetValueDou(i, (int)Cols.Costo);
                iAgrupacion = myGrid.GetValueInt(i, (int)Cols.Agrupacion);
                iCostoUnitario = myGrid.GetValueDou(i, (int)Cols.CostoUnitario);
                iTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                iIva = myGrid.GetValueDou(i, (int)Cols.Iva);
                iImporteNeto = myGrid.GetValueDou(i, (int)Cols.Importe); 


                iOpcion = 1;
                if (!bActivar)
                {
                    //Si no esta activo, significa que es una cancelacion.
                    iOpcion = 2;
                }

                if (sClaveSSA != "")
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                                         sFolio, sIdFinanciamiento, sClaveSSA, iCosto, iAgrupacion, iCostoUnitario, iTasaIva, iIva, iImporteNeto, iOpcion);
                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        sMensaje = "La información del Financiamiento ha sido guardada exitosamente";
                    }
                }
            }

            return bRegresa;
        } 

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            return bRegresa;
        }

        public void MostrarDetalle()
        {
            btnNuevo_Click(null, null);
            ObtenerClavesFinanciamiento();
            this.ShowDialog();
        }
        private void ObtenerClavesFinanciamiento()
        {
            string sSql = string.Format("Select ClaveSSA, IdClaveSSA, DescripcionClaveSSA, Presentacion, " +
                " Costo, Agrupacion, CostoUnitario, TasaIva, Iva, ImporteNeto, " + 
                " Case When StatusClave = 'A' Then 1 Else 0 End as Activar, 1 as Guardado " +
                "From vw_FACT_FuentesDeFinanciamiento_Admon_Claves (NoLock) " +
                "Where IdFuenteFinanciamiento = '{0}' And IdFinanciamiento = '{1}' " +
                "Order By DescripcionClaveSSA ", sFolio, sIdFinanciamiento);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "");
                General.msjError("Ocurrió un error al obtener las Claves SSA del Financiamiento.");
            }
            else
            {
                if (myLeer.Leer())
                {
                    // bFolioGuardado = true;
                    myGrid.LlenarGrid(myLeer.DataSetClase);
                    BloquearClavesGuardadas();
                }
            }

        }
        #endregion Funciones         

        #region Grid 
        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                if (myGrid.GetValue(i, (int)Cols.IdClaveSSA).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                {
                    myGrid.DeleteRow(i);
                }
            }

            if (myGrid.Rows == 0) // Si No existen renglones, se inserta 1.
            {
                myGrid.AddRow();
            }
        }

        private void grdClaves_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
            {
                if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.IdClaveSSA) != "")
                {
                    myGrid.Rows = myGrid.Rows + 1; 
                    myGrid.ActiveRow = myGrid.Rows; 
                    myGrid.SetActiveCell(myGrid.Rows, (int)Cols.ClaveSSA);
                }
            }  
        }

        private void grdClaves_EditModeOff(object sender, EventArgs e)
        {
            Cols iCol = (Cols)myGrid.ActiveCol;
            switch (iCol)
            {
                case Cols.ClaveSSA:
                    ObtenerDatosClave();
                    break;
            }
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
                // Se verifican las Claves
                //if (myGrid.GetValue(1, (int)Cols.IdClaveSSA) == "")
                //{
                //    bRegresa = false;
                //}
                //else
                //{
                    for (int i = 1; i <= myGrid.Rows; i++)
                    {
                        if (myGrid.GetValue(i, (int)Cols.IdClaveSSA) == "")
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                //}
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos una Clave SSA, verifique.");
            }

            return bRegresa;

        }

        private void grdClaves_KeyDown(object sender, KeyEventArgs e)
        {
            int iRow = myGrid.ActiveRow;
            int iCol = myGrid.ActiveCol;

            // El borrado aplica sobre cualquier columna 
            if (e.KeyCode == Keys.Delete)
            {
                if (!myGrid.GetValueBool(iRow, (int)Cols.Guardado))
                {
                    try
                    {
                        myGrid.DeleteRow(iRow);
                    }
                    catch { }

                    if (myGrid.Rows == 0)
                    {
                        myGrid.Limpiar(true);
                    }
                }
            }

            if (iCol == (int)Cols.ClaveSSA)
            {
                if (e.KeyCode == Keys.F1)
                {
                    myLeer.DataSetClase = Ayudas.ClavesSSA_Sales(2, true, "grdClaves_KeyDown");
                    if (myLeer.Leer())
                    {
                        myGrid.SetValue(myGrid.ActiveRow, 1, myLeer.Campo("ClaveSSA"));
                        ObtenerDatosClave();
                    }
                }
            }

        }

        private void grdClaves_EditModeOn(object sender, EventArgs e)
        {
            int iColumna = myGrid.ActiveCol;
            int iRenglon = myGrid.ActiveRow;

            if (iColumna == (int)Cols.ClaveSSA)
            {
                myGrid.SetValue(iRenglon, (int)Cols.IdClaveSSA, "");
                myGrid.SetValue(iRenglon, (int)Cols.Descripcion, "");
                myGrid.SetValue(iRenglon, (int)Cols.Activar, 0);
            }
        }

        private void ObtenerDatosClave()
        {
            string sCodigo = "";

            sCodigo = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA);

            if (sCodigo != "")
            {
                myLeer.DataSetClase = Consultas.ClavesSSA_Sales(sCodigo, true, "ObtenerDatosClave()");

                if (!myLeer.Leer())
                {
                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
                    myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.ClaveSSA);
                }
                else
                {
                    CargarDatosClave();
                }
            }
        }

        private void CargarDatosClave()
        {
            int iRenglonActivo = myGrid.ActiveRow;
            int iColClave = (int)Cols.ClaveSSA;
            string sClaveSSA = myLeer.Campo("ClaveSSA");

            if (!myGrid.BuscaRepetido(sClaveSSA, iRenglonActivo, iColClave))
            {
                myGrid.SetValue(iRenglonActivo, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));
                myGrid.SetValue(iRenglonActivo, (int)Cols.IdClaveSSA, myLeer.Campo("IdClaveSSA_Sal"));
                myGrid.SetValue(iRenglonActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
                myGrid.SetValue(iRenglonActivo, (int)Cols.Presentacion, myLeer.Campo("Presentacion"));

                myGrid.SetValue(iRenglonActivo, (int)Cols.Costo, 0); 
                myGrid.SetValue(iRenglonActivo, (int)Cols.Agrupacion, 1);
                myGrid.SetValue(iRenglonActivo, (int)Cols.CostoUnitario, 0);
                myGrid.SetValue(iRenglonActivo, (int)Cols.TasaIva, 0); 

                myGrid.SetActiveCell(iRenglonActivo, (int)Cols.Costo);
            }
            else
            {
                General.msjUser("La clave ya se encuentra capturado en otro renglon.");
                myGrid.SetValue(myGrid.ActiveRow, 1, "");
                myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                myGrid.EnviarARepetido();
            }

            grdClaves.EditMode = false;
        }

        private void BloquearClavesGuardadas()
        {
            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                if (myGrid.GetValueBool(i, (int)Cols.Guardado)) //Si la columna oculta Guardado esta activada
                {
                    myGrid.BloqueaCelda(true, Color.WhiteSmoke, i, (int)Cols.ClaveSSA);
                }
            }
        }

        #endregion Grid    
        
    }
}
