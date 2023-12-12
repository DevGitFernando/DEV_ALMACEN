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
    public partial class FrmFuentesDeFinaciamiento_Claves : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer, myLeerDetalles;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;

        string sIdEstado = "";
        string sIdCliente = "";
        string sIdSubCliente = ""; 

        string sFolio = "";
        string sMensaje = "";
        string sIdFinanciamiento = "";
        string sFinanciamiento = "";
        bool bValidarPoliza = false;
        int iLongitudMinima = 0;
        int iLongitudMaxima = 0;
        int iEsExclusivoControlados = 0; 
        bool bEsExclusivoControlados = false;
        int iTipoClasificacionSSA = 0;
        int iTipoClasificacionSSA_Aux = 0; 
        // bool bFolioGuardado = false;
        
        //Para Auditoria
        clsAuditoria auditoria;

        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA, IdClaveSSA, Descripcion, ContenidoPaquete, Cantidad, CajasEmpaques, Piezas, Activar, Guardado
        }
        
        #region Propiedades 
        public string IdEstado
        {
            get { return sIdEstado; }
            set { sIdEstado = value; }
        }

        public string IdCliente
        {
            get { return sIdCliente; }
            set { sIdCliente = value; }
        }

        public string IdSubCliente
        {
            get { return sIdSubCliente; }
            set { sIdSubCliente = value; }
        }

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

        public int TipoClasificacionSSA
        {
            get { return iTipoClasificacionSSA; }
            set { iTipoClasificacionSSA = value; }
        }
        #endregion Propiedades 

        public FrmFuentesDeFinaciamiento_Claves()
        {
            InitializeComponent();

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version, false);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");
            myLeer = new clsLeer(ref ConexionLocal);
            myLeerDetalles = new clsLeer(ref ConexionLocal);
            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);

            myGrid = new clsGrid(ref grdClaves, this);
            myGrid.BackColorColsBlk = Color.White;
            myGrid.EstiloDeGrid = eModoGrid.ModoRow;
            myGrid.AjustarAnchoColumnasAutomatico = true; 
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

            chkValidarReferencia.Checked = bValidarPoliza;
            nmDesde.Value = (decimal)iLongitudMinima;
            nmHasta.Value = (decimal)iLongitudMaxima;

            iEsExclusivoControlados = bEsExclusivoControlados ? 1 : 0;

            if (iTipoClasificacionSSA == 1) iTipoClasificacionSSA_Aux = 1;
            if (iTipoClasificacionSSA == 2) iTipoClasificacionSSA_Aux = 0;
            if (iTipoClasificacionSSA == 3) iTipoClasificacionSSA_Aux = 2; 
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
                if (!ConexionLocal.Abrir())
                { 
                    Error.LogError(ConexionLocal.MensajeError); 
                    General.msjErrorAlAbrirConexion();  
                }
                else 
                {
                    ConexionLocal.IniciarTransaccion();

                    bContinua = Guardar_ValidarPoliza();                    

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

        private bool Guardar_ValidarPoliza()
        {
            bool bRegresa = false;
            int iValidarPoliza = chkValidarReferencia.Checked ? 1 : 0;

            bValidarPoliza = chkValidarReferencia.Checked;
            iLongitudMinima = (int)nmDesde.Value;
            iLongitudMaxima = (int)nmHasta.Value; 

            string sSql = string.Format("Exec spp_Mtto_FACT_Fuentes_De_Financiamiento_Detalles_Poliza " +
                "  @IdFuenteFinanciamiento = '{0}', @IdFinanciamiento = '{1}', @ValidarPolizaBeneficiario = '{2}', @LongitudMinimaPoliza = '{3}', @LongitudMaximaPoliza = '{4}' ",
                sFolio, sIdFinanciamiento, iValidarPoliza, (int)nmDesde.Value, (int)nmHasta.Value); 

            bRegresa = myLeer.Exec(sSql); 
            if (bRegresa)
            {
                bRegresa = GuardarClaves(); 
            }

            return bRegresa; 
        } 

        private bool GuardarClaves()
        {
            bool bRegresa = true, bActivar = true;
            string sSql = "";
            string sClaveSSA = "";
            int iCantidad = 0;
            int iCantidadPresupuestada = 0; 
            int iOpcion = 0;

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sClaveSSA = myGrid.GetValue(i, (int)Cols.ClaveSSA);
                iCantidadPresupuestada = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Piezas);
                bActivar = myGrid.GetValueBool(i, (int)Cols.Activar);

                iOpcion = 1;
                if (!bActivar) 
                {
                    //Si no esta activo, significa que es una cancelacion.
                    iOpcion = 2;
                }

                if (sClaveSSA != "")
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA  " +
                            "  @IdFuenteFinanciamiento = '{0}', @IdFinanciamiento = '{1}', @ClaveSSA = '{2}', " + 
                            " @CantidadPresupuestadaPiezas = '{3}', @CantidadPresupuestada = '{4}', @iOpcion = '{5}' ",
                            sFolio, sIdFinanciamiento, sClaveSSA, iCantidad, iCantidadPresupuestada, iOpcion);
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
            string sSql = ""; 

            //sSql = string.Format("Select ClaveSSA, IdClaveSSA, DescripcionSal, ContenidoPaquete, CantidadPresupuestada, " + 
            //    " 0 as CajasEmpaques, 0 as Piezas, (Case When StatusClave = 'A' Then 1 Else 0 End) as Activar, 1 as Guardado " +
            //    "From vw_FACT_FuentesDeFinanciamiento_Claves (NoLock) " +
            //    "Where IdFuenteFinanciamiento = '{0}' And IdFinanciamiento = '{1}' " +
            //    "Order By DescripcionSal ", sFolio, sIdFinanciamiento);

            //sSql = string.Format(
            //"Select  " +
            //"    IsNull(C.ClaveSSA, P.ClaveSSA) as ClaveSSA, IsNull(C.IdClaveSSA, P.IdClaveSSA) as IdClaveSSA, " +
            //"    IsNull(C.DescripcionSal, P.DescripcionClave) as DescripcionClave, " +
            //"    IsNull(C.ContenidoPaquete, P.ContenidoPaquete) as ContenidoPaquete, IsNull(C.CantidadPresupuestada, 0) as CantidadPresupuestada,   " +
            //"    0 as CajasEmpaques, 0 as Piezas, (Case When IsNull(C.StatusClave, 'A') = 'A' Then 1 Else 0 End) as Activar, 1 as Guardado 	 " +
            //"From vw_Claves_Precios_Asignados P (NoLock) " +
            //"Left Join vw_FACT_FuentesDeFinanciamiento_Claves C (NoLock) " +
            //"    On ( P.IdEstado = C.IdEstado and P.IdCliente = C.IdCliente and P.IdSubCliente = C.IdSubCliente and P.ClaveSSA = C.ClaveSSA and " +
            //"       C.IdFuenteFinanciamiento = '{0}' and C.IdFinanciamiento = '{1}' ) " +
            //"Where P.IdEstado = '{2}' and P.IdCliente = '{3}' and P.IdSubCliente = '{4}' " +
            //"Order by DescripcionClave ", 
            //sFolio, sIdFinanciamiento, sIdEstado, sIdCliente, sIdSubCliente);


            sSql = string.Format(
            "Select  " +
            "    IsNull(C.ClaveSSA, C.ClaveSSA) as ClaveSSA, IsNull(C.IdClaveSSA, C.IdClaveSSA) as IdClaveSSA, " +
            "    IsNull(C.DescripcionClaveSSA, C.DescripcionClaveSSA) as DescripcionClave, " +
            "    IsNull(C.ContenidoPaquete, C.ContenidoPaquete) as ContenidoPaquete, IsNull(C.CantidadPresupuestada, 0) as CantidadPresupuestada,   " +
            "    0 as CajasEmpaques, 0 as Piezas, (Case When IsNull(C.StatusClave, 'A') = 'A' Then 1 Else 0 End) as Activar, 1 as Guardado 	 " +
            "From vw_FACT_FuentesDeFinanciamiento_Claves C (NoLock) " +
            "Where IdFuenteFinanciamiento = '{0}' And IdFinanciamiento = '{1}'  " + 
            "Order by DescripcionClave ", 
            sFolio, sIdFinanciamiento ); 

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
                    myLeer.DataSetClase = Ayudas.ClavesSSA_Sales(iTipoClasificacionSSA_Aux, 2, true, "grdClaves_KeyDown");
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
                myLeer.DataSetClase = Consultas.ClavesSSA_Sales(sCodigo, true, iTipoClasificacionSSA_Aux, 2, "ObtenerDatosClave()");

                if (!myLeer.Leer())
                {
                    General.msjUser("La clave indicada no existe ó no pertenece a la Clasificacion SSA especificada, verifique.");
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
                myGrid.SetActiveCell(iRenglonActivo, (int)Cols.Activar);
            }
            else
            {
                General.msjUser("La Clave ya se encuentra capturada en otro renglon.");
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
